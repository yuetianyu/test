Imports EventSakusei.ShisakuBuhinEdit.Selector.Logic
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports FarPoint.Win.Spread
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd

Namespace ShisakuBuhinEdit.Selector.Ui


    Public Class SpdSelectorOptionObserver : Implements Observer

#Region "各列のTAG"
        Private Const TAG_CHECK As String = "CHECK"
        Private Const TAG_NAME As String = "NAME"
#End Region
        Private Const START_ROW_COUNT As Integer = 1

        Private ReadOnly spread As FpSpread
        Private ReadOnly sheet As SheetView
        Private subject As BuhinEditSelectorOptionSubject

        Public Sub New(ByVal spread As FpSpread, ByVal subject As BuhinEditSelectorOptionSubject)
            Me.spread = spread
            If spread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            Me.sheet = spread.Sheets(0)
            Me.subject = subject

            subject.addObserver(Me)
        End Sub

        Public Sub Initialize()

            SpreadUtil.Initialize(spread)

            Dim index As Integer = 0

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_CHECK
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_NAME

            ' 表示値
            If subject.OptionCount = 0 Then
                sheet.RowCount = 0
                spread.Enabled = False
            Else
                sheet.RowCount = 1
                sheet.RowCount = 1 + subject.OptionCount
                For i As Integer = 0 To subject.OptionCount - 1
                    '大区分、中区分の有無チェック
                    Dim strDaiKbn As String = ""
                    Dim strChuKbn As String = ""
                    If StringUtil.IsNotEmpty(subject.OptionNameDai(i)) Then
                        strDaiKbn = "・"
                    End If
                    If StringUtil.IsNotEmpty(subject.OptionNameChu(i)) Then
                        strChuKbn = "・"
                    End If
                    sheet.Cells(START_ROW_COUNT + i, sheet.Columns(TAG_NAME).Index).Value = subject.OptionNameDai(i) _
                                                                                            & strDaiKbn & subject.OptionNameChu(i) _
                                                                                            & strChuKbn & subject.OptionName(i)
                Next
            End If

            'InitializeValidator()

            'AddHandler spread.VisibleChanged, AddressOf Spread_VisibleChangedEventHandlable
            '' 通常の Spread_Changed()では、CTRL+V/CTRL+ZでChengedイベントが発生しない
            ''（編集モードではない状態で変更された場合は発生しない仕様とのこと。）
            '' CTRL+V/CTRL+Zでもイベントが発生するハンドラを設定する
            SpreadUtil.AddHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
        End Sub

        Private Sub Spread_ChangeEventHandlable(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs)
            OnChange(e.Row, e.Column)
        End Sub

        Public Sub OnChange(ByVal row As Integer, ByVal column As Integer)
            '0番目の時は全て選択の項目なのでスルーさせておく'
            If Not row = 0 Then
                Dim rowNo As Integer = row - START_ROW_COUNT
                Dim value As Object = sheet.Cells(row, column).Value
                Select Case Convert.ToString(sheet.Columns(column).Tag)
                    Case TAG_CHECK
                        subject.OptionCheck(rowNo) = CBool(value)
                End Select
                subject.NotifyObservers(rowNo)
            End If

        End Sub

        Public Sub Update(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update
            If arg Is Nothing Then
                For i As Integer = 0 To subject.OptionCount - 1
                    Update(observable, i)
                Next
            ElseIf IsNumeric(arg) Then
                Dim rowNo As Integer = CInt(arg)
                If rowNo < 0 OrElse subject.OptionCount < rowNo Then
                    Throw New ArgumentOutOfRangeException("arg", arg, "範囲外")
                End If
                sheet.Cells(START_ROW_COUNT + rowNo, sheet.Columns(TAG_CHECK).Index).Value = subject.OptionCheck(rowNo)
            End If
        End Sub


    End Class
End Namespace