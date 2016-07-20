Imports FarPoint.Win.Spread
Imports EventSakusei.EventEdit.Logic
Imports EventSakusei.EventEdit.Ui
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd

Namespace EventEdit
    Public Class SpdReferenceCarObserver : Implements Frm9SpdObserver, Frm9SpdSetter(Of EventEditReferenceCar)
#Region "各列のTag"
        ' TODO SpdCompleteCar と同じ
        Private Const TAG_SHUBETSU As String = "SHUBETSU"
        Private Const TAG_GOSHA As String = "GOSHA"
        Private Const TAG_KATASHIKI As String = "KATASHIKI"
        Private Const TAG_SHIMUKE As String = "SHIMUKE"
        Private Const TAG_OP As String = "OP"
        Private Const TAG_HANDLE As String = "HANDLE"
        Private Const TAG_SHAGATA As String = "SHAGATA"
        Private Const TAG_GRADE As String = "GRADE"
        Private Const TAG_SHADAI_NO As String = "SHADAI_NO"
        Private Const TAG_GAISO_SHOKU As String = "GAISO_SHOKU"
        Private Const TAG_NAISO_SHOKU As String = "NAISO_SHOKU"
        Private Const TAG_GROUP As String = "GROUP"
        Private Const TAG_KOSHI_NO As String = "KOSHI_NO"
        Private Const TAG_KANSEIBI As String = "KANSEIBI"
        Private Const TAG_EG_KATASHIKI As String = "EG_KATASHIKI"
        Private Const TAG_EG_HAIKIRYO As String = "EG_HAIKIRYO"
        Private Const TAG_EG_SYSTEM As String = "EG_SYSTEM"
        Private Const TAG_EG_KAKYUKI As String = "EG_KAKYUKI"
        Private Const TAG_TM_KUDO As String = "TM_KUDO"
        Private Const TAG_TM_HENSOKUKI As String = "TM_HENSOKUKI"
        Private Const TAG_TM_FUKU_HENSOKUKI As String = "TM_FUKU_HENSOKUKI"
        Private Const TAG_SHIYO_BUSHO As String = "SHIYO_BUSHO"
        Private Const TAG_SHIKEN_MOKUTEKI As String = "SHIKEN_MOKUTEKI"
#End Region

        Private ReadOnly spread As FpSpread
        Private ReadOnly sheet As SheetView
        Private subject As EventEditReferenceCar
        Private ReadOnly titleRows As Integer

        Public Sub New(ByVal spread As FpSpread, ByVal subject As EventEditReferenceCar)
            Me.spread = spread
            If spread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            Me.sheet = spread.Sheets(0)

            Me.subject = subject

            Me.titleRows = EventSpreadUtil.GetTitleRows(sheet)

            subject.addObserver(Me)
        End Sub

        Private Sub Spread_ChangeEventHandlable(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs)
            OnChange(e.Row, e.Column)
        End Sub

        Private Sub Spread_VisibleChangedEventHandlable(ByVal sender As Object, ByVal e As System.EventArgs)
            If spread.Visible Then
                subject.Apply()
                subject.notifyObservers()
            End If
        End Sub

        Public Sub SupersedeSubject(ByVal subject As EventEditReferenceCar) Implements Frm9SpdSetter(Of EventEditReferenceCar).SupersedeSubject
            Me.subject = subject
            Me.subject.addObserver(Me)
            SpreadUtil.RemoveHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
            ClearSheetData()
            ReInitialize()
            SpreadUtil.AddHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
        End Sub

        Public Sub ClearSheetBackColor() Implements Frm9SpdObserver.ClearSheetBackColor
            sheet.Cells(titleRows, 0, sheet.RowCount - 1, sheet.ColumnCount - 1).ResetBackColor()
        End Sub
        Public Sub ClearSheetData() Implements Frm9SpdObserver.ClearSheetData

            ''''最大行を定数指定に修正
            sheet.RowCount = titleRows + 200

            sheet.ClearRange(titleRows, 0, sheet.RowCount - titleRows, sheet.ColumnCount, False)
        End Sub

        Public Sub Initialize() Implements Frm9SpdObserver.Initialize

            EventSpreadUtil.InitializeFrm9(spread)

            ' TODO SpdCompleteCar と同じ
            Dim index As Integer = 0
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHUBETSU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GOSHA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KATASHIKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHIMUKE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_OP
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_HANDLE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHAGATA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GRADE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHADAI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GAISO_SHOKU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_NAISO_SHOKU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GROUP
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KOSHI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KANSEIBI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EG_KATASHIKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EG_HAIKIRYO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EG_SYSTEM
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EG_KAKYUKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TM_KUDO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TM_HENSOKUKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TM_FUKU_HENSOKUKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHIYO_BUSHO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHIKEN_MOKUTEKI

            '' 全列Lock
            For Each aColumn As Column In sheet.Columns
                aColumn.Locked = True
            Next

            AddHandler spread.VisibleChanged, AddressOf Spread_VisibleChangedEventHandlable
            '' 通常の Spread_Changed()では、CTRL+V/CTRL+ZでChengedイベントが発生しない
            ''（編集モードではない状態で変更された場合は発生しない仕様とのこと。）
            '' CTRL+V/CTRL+Zでもイベントが発生するハンドラを設定する
            SpreadUtil.AddHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
        End Sub

        Public Sub ReInitialize() Implements Frm9SpdObserver.ReInitialize
            ' nop
        End Sub


        Public Sub Update(ByVal observable As ShisakuCommon.Util.Observable, ByVal args As Object) Implements Observer.Update
            ' TODO 最後のOnRowLockがコメントになっている
            '  ConvYyyymmddToSlashYyyymmdd になっている
            '  上記以外、SpdCompleteCar と同じ
            If args Is Nothing Then
                For Each key As Integer In subject.GetInputRowNos
                    Update(Nothing, key)
                Next
            Else
                If Not IsNumeric(args) Then
                    Return
                End If
                Dim rowNo As Integer = Convert.ToInt32(args)
                Dim row As Integer = titleRows + rowNo
                sheet.Cells(row, sheet.Columns(TAG_SHUBETSU).Index).Value = subject.ShisakuSyubetu(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_GOSHA).Index).Value = subject.ShisakuGousya(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_KATASHIKI).Index).Value = subject.ShisakuKatashiki(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SHIMUKE).Index).Value = subject.ShisakuShimuke(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_OP).Index).Value = subject.ShisakuOp(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_HANDLE).Index).Value = subject.ShisakuHandoru(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SHAGATA).Index).Value = subject.ShisakuSyagata(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_GRADE).Index).Value = subject.ShisakuGrade(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SHADAI_NO).Index).Value = subject.ShisakuSyadaiNo(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_GAISO_SHOKU).Index).Value = subject.ShisakuGaisousyoku(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_NAISO_SHOKU).Index).Value = subject.ShisakuNaisousyoku(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_GROUP).Index).Value = subject.ShisakuGroup(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_KOSHI_NO).Index).Value = subject.ShisakuKoushiNo(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_KANSEIBI).Index).Value = DateUtil.ConvYyyymmddToSlashYyyymmdd(subject.ShisakuKanseibi(rowNo))
                sheet.Cells(row, sheet.Columns(TAG_EG_KATASHIKI).Index).Value = subject.ShisakuEgKatashiki(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_EG_HAIKIRYO).Index).Value = subject.ShisakuEgHaikiryou(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_EG_SYSTEM).Index).Value = subject.ShisakuEgSystem(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_EG_KAKYUKI).Index).Value = subject.ShisakuEgKakyuuki(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_TM_KUDO).Index).Value = subject.ShisakuTmKudou(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_TM_HENSOKUKI).Index).Value = subject.ShisakuTmHensokuki(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_TM_FUKU_HENSOKUKI).Index).Value = subject.ShisakuTmFukuHensokuki(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SHIYO_BUSHO).Index).Value = subject.ShisakuSiyouBusyo(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SHIKEN_MOKUTEKI).Index).Value = subject.ShisakuShikenMokuteki(rowNo)
                ''OnRowLock(row)
            End If
        End Sub
        Public Sub OnChange(ByVal row As Integer, ByVal column As Integer) Implements Frm9SpdObserver.OnChange
            ' nop
        End Sub
    End Class
End Namespace