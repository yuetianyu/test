Imports FarPoint.Win.Spread.CellType
Imports EventSakusei.EventEdit.Logic
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports FarPoint.Win.Spread
Imports EventSakusei.EventEdit.Ui
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Valid
Imports ShisakuCommon.Ui.Spd
Imports EBom.Common

Namespace EventEdit
    Public Class SpdBaseTenkaiCarObserver : Implements Frm9SpdObserver, Frm9SpdSetter(Of EventEditBaseTenkaiCar)
#Region "各列のTAG"
        Private Const TAG_SHUBETSU As String = "SHUBETSU"
        Private Const TAG_GOSHA As String = "GOSHA"
        Private Const TAG_SEKKEI_TENKAI As String = "SEKKEI_TENKAI"
        Private Const TAG_KAIHATSU_FUGO As String = "KAIHATSU_FUGO"
        Private Const TAG_SHIYO_JOHO_NO As String = "SHIYO_JOHO_NO"
        Private Const TAG_APPLIED_NO As String = "APPLIED_NO"
        Private Const TAG_KATASHIKI As String = "KATASHIKI"
        Private Const TAG_SHIMUKE As String = "SHIMUKE"
        Private Const TAG_OP As String = "OP"
        Private Const TAG_GAISO_SHOKU As String = "GAISO_SHOKU"
        Private Const TAG_GAISO_SHOKU_NAME As String = "GAISO_SHOKU_NAME"
        Private Const TAG_NAISO_SHOKU As String = "NAISO_SHOKU"
        Private Const TAG_NAISO_SHOKU_NAME As String = "NAISO_SHOKU_NAME"
        Private Const TAG_BASE_EVENT_CODE As String = "BASE_EVENT_CODE"
        Private Const TAG_BASE_GOSHA As String = "BASE_GOSHA"

#End Region

        Private Shared ReadOnly DEFAULT_LOCKED_BASE_TAGS As String() = {TAG_SHUBETSU, TAG_SEKKEI_TENKAI, TAG_KAIHATSU_FUGO, _
                                                                        TAG_SHIYO_JOHO_NO, TAG_APPLIED_NO, TAG_KATASHIKI, _
                                                                        TAG_SHIMUKE, TAG_OP, TAG_GAISO_SHOKU, TAG_GAISO_SHOKU_NAME, _
                                                                        TAG_NAISO_SHOKU, TAG_NAISO_SHOKU_NAME, TAG_BASE_GOSHA}
        Private Shared ReadOnly UNLOCKABLE_TAGS As String() = DEFAULT_LOCKED_BASE_TAGS

        Private ReadOnly spread As FpSpread
        Private ReadOnly sheet As SheetView
        Private subject As EventEditBaseTenkaiCar
        Private ReadOnly titleRows As Integer
        Private validator As SpreadValidator
        '号車DELETEﾁｪｯｸ用に追加
        Private GousyaDeleteCheck As String = ""

        Public Sub New(ByVal spread As FpSpread, ByVal subject As EventEditBaseTenkaiCar)
            Me.spread = spread
            If spread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            Me.sheet = spread.Sheets(0)
            Me.subject = subject

            Me.titleRows = EventSpreadUtil.GetTitleRows(sheet)

            subject.AddObserver(Me)
        End Sub

        Private Sub Spread_ChangeEventHandlable(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs)
            OnChange(e.Row, e.Column)
        End Sub

        Private Sub Spread_VisibleChangedEventHandlable(ByVal sender As Object, ByVal e As System.EventArgs)
            If spread.Visible Then
                subject.NotifyObservers()
            End If
        End Sub

        Public Sub SupersedeSubject(ByVal subject As EventEditBaseTenkaiCar) Implements Frm9SpdSetter(Of EventEditBaseTenkaiCar).SupersedeSubject
            Me.subject = subject
            Me.subject.AddObserver(Me)
            ClearSheetData()
            ReInitialize()
        End Sub

        Public Sub ClearSheetBackColor() Implements Frm9SpdObserver.ClearSheetBackColor
            sheet.Cells(titleRows, 0, sheet.RowCount - 1, sheet.ColumnCount - 1).ResetBackColor()
            sheet.Cells(titleRows, 0, sheet.RowCount - 1, sheet.ColumnCount - 1).ResetForeColor()
        End Sub

        Public Sub ClearSheetData() Implements Frm9SpdObserver.ClearSheetData

            ''''最大行を定数指定に修正
            sheet.RowCount = titleRows + 200
            sheet.ClearRange(titleRows, 0, sheet.RowCount - titleRows, sheet.ColumnCount, False)
        End Sub

        Public Sub Initialize() Implements Frm9SpdObserver.Initialize

            EventSpreadUtil.InitializeFrm9(spread)

            Dim index As Integer = 0

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHUBETSU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GOSHA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SEKKEI_TENKAI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAIHATSU_FUGO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHIYO_JOHO_NO

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_APPLIED_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KATASHIKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHIMUKE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_OP
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GAISO_SHOKU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GAISO_SHOKU_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_NAISO_SHOKU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_NAISO_SHOKU_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BASE_EVENT_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BASE_GOSHA

            ' 設計展開列は、使用しなくなった。非表示にする。
            sheet.Columns(TAG_SEKKEI_TENKAI).Visible = False

            AddHandler spread.VisibleChanged, AddressOf Spread_VisibleChangedEventHandlable
        End Sub


#Region "一意の設計展開CheckBox"

        Private _emptySekkeiTenkaiCheckBoxCell As CheckBoxCellType
        Private ReadOnly Property EmptySekkeiTenkaiCheckBoxCell() As CheckBoxCellType
            Get
                If _emptySekkeiTenkaiCheckBoxCell Is Nothing Then
                    _emptySekkeiTenkaiCheckBoxCell = ShisakuSpreadUtil.NewGeneralCheckBoxCellType
                    SpreadUtil.BindTextToCheckBoxCellType(_emptySekkeiTenkaiCheckBoxCell, String.Empty, String.Empty)
                End If
                Return _emptySekkeiTenkaiCheckBoxCell
            End Get
        End Property
        Private _sekkeiTenkaiCheckBoxCell As CheckBoxCellType
        Private ReadOnly Property SekkeiTenkaiCheckBoxCell() As CheckBoxCellType
            Get
                If _sekkeiTenkaiCheckBoxCell Is Nothing Then
                    _sekkeiTenkaiCheckBoxCell = ShisakuSpreadUtil.NewGeneralCheckBoxCellType
                    SpreadUtil.BindTextToCheckBoxCellType(_sekkeiTenkaiCheckBoxCell, _
                        TShisakuEventBaseSeisakuIchiranVoHelper.SekkeiTenkaiKbnName.JURYO_GO, _
                        TShisakuEventBaseSeisakuIchiranVoHelper.SekkeiTenkaiKbnName.JURYO_MAE)
                End If
                Return _sekkeiTenkaiCheckBoxCell
            End Get
        End Property
#End Region

        '2012/01/09
        ''' <summary>
        ''' 行背景色をグレーに変更する(種別が "D" の場合のみ)
        ''' </summary>
        ''' <param name="activeSheet">色を設定するSpreadSheet</param>
        ''' <param name="targetRow">対象の行位置</param>
        ''' <param name="targetColumn">種別のカラム位置</param>
        ''' <remarks></remarks>
        Private Sub SetActiveSpreadColor(ByVal activeSheet As SheetView, ByVal targetRow As Long, ByVal targetColumn As Long)
            Const DEF_TYPE_D As String = "D"
            Dim nColumn As Long = 0
            Dim nColumn2 As Long = 0

            If activeSheet.ColumnCount - 1 > 0 Then
                nColumn2 = activeSheet.ColumnCount - 1
            End If

            If activeSheet.Cells(targetRow, targetColumn, targetRow, targetColumn).Text = DEF_TYPE_D Then
                activeSheet.Cells(targetRow, nColumn, targetRow, nColumn2).BackColor = Color.DimGray       'ダークグレー
                activeSheet.Cells(targetRow, nColumn, targetRow, nColumn2).ForeColor = Color.LightGray
            Else
                activeSheet.Cells(targetRow, nColumn, targetRow, nColumn2).ResetBackColor()
                activeSheet.Cells(targetRow, nColumn, targetRow, nColumn2).ResetForeColor()
            End If
        End Sub

        Private backIsViewer As Boolean?

        Public Sub ReInitialize() Implements Frm9SpdObserver.ReInitialize
            ' nop
        End Sub

        Public Sub OnChange(ByVal row As Integer, ByVal column As Integer) Implements Frm9SpdObserver.OnChange
            Dim rowNo As Integer = row - titleRows
            Dim value As String = Convert.ToString(sheet.Cells(row, column).Value)

            Select Case Convert.ToString(sheet.Columns(column).Tag)
                Case TAG_SHUBETSU
                    subject.ShisakuSyubetu(rowNo) = Convert.ToString(value)
                Case TAG_GOSHA
                    'DELETEされたのか？チェックする。
                    If Convert.ToString(value) = "" And subject.ShisakuGousya(rowNo) <> "" Then
                        '確認を促し、OKなら処理続行。
                        If GousyaDeleteCheck = "" _
                           AndAlso frm01Kakunin.ConfirmOkCancel("号車を削除して宜しいですか？") = MsgBoxResult.Ok Then
                            subject.ShisakuGousya(rowNo) = Convert.ToString(value)
                        Else
                            '１回だけ画面を出すようにフラグをチェンジ
                            If GousyaDeleteCheck = "" Then
                                GousyaDeleteCheck = "Back"
                            Else
                                GousyaDeleteCheck = ""
                            End If
                            '確認を促し、CANCELなら号車をDELETE前に戻す。
                            sheet.Cells(row, column).Text = subject.ShisakuGousya(rowNo)
                        End If
                    Else
                        '通常処理。
                        subject.ShisakuGousya(rowNo) = Convert.ToString(value)
                    End If

                Case TAG_KAIHATSU_FUGO
                    subject.BaseKaihatsuFugo(rowNo) = Convert.ToString(value)

                Case TAG_SHIYO_JOHO_NO
                    subject.BaseShiyoujyouhouNo(rowNo) = Convert.ToString(value)

                Case TAG_APPLIED_NO
                    subject.BaseAppliedNo(rowNo) = Convert.ToString(value)

                Case TAG_KATASHIKI
                    subject.BaseKatashiki(rowNo) = Convert.ToString(value)

                Case TAG_SHIMUKE
                    subject.BaseShimuke(rowNo) = Convert.ToString(value)

                Case TAG_OP
                    subject.BaseOp(rowNo) = Convert.ToString(value)

                Case TAG_GAISO_SHOKU
                    subject.BaseGaisousyoku(rowNo) = Convert.ToString(value)

                Case TAG_NAISO_SHOKU
                    subject.BaseNaisousyoku(rowNo) = Convert.ToString(value)

                Case TAG_BASE_EVENT_CODE
                    subject.ShisakuBaseEventCode(rowNo) = Convert.ToString(value)

                Case TAG_BASE_GOSHA
                    subject.ShisakuBaseGousya(rowNo) = Convert.ToString(value)

            End Select
            '
            subject.NotifyObservers(rowNo)
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
                sheet.Cells(row, sheet.Columns(TAG_KAIHATSU_FUGO).Index).Value = subject.BaseKaihatsuFugo(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SHIYO_JOHO_NO).Index).Value = subject.BaseShiyoujyouhouNo(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_APPLIED_NO).Index).Value = subject.BaseAppliedNo(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_KATASHIKI).Index).Value = subject.BaseKatashiki(rowNo)
                If StringUtil.IsEmpty(subject.BaseShimuke(rowNo)) And StringUtil.IsNotEmpty(subject.BaseKatashiki(rowNo)) Then
                    sheet.Cells(row, sheet.Columns(TAG_SHIMUKE).Index).Value = TShisakuEventBaseSeisakuIchiranVoHelper.BaseShimukeName.KOKUNAI
                Else
                    sheet.Cells(row, sheet.Columns(TAG_SHIMUKE).Index).Value = subject.BaseShimuke(rowNo)
                End If
                sheet.Cells(row, sheet.Columns(TAG_OP).Index).Value = subject.BaseOp(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_GAISO_SHOKU).Index).Value = subject.BaseGaisousyoku(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_NAISO_SHOKU).Index).Value = subject.BaseNaisousyoku(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_BASE_EVENT_CODE).Index).Value = subject.ShisakuBaseEventCode(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_BASE_GOSHA).Index).Value = subject.ShisakuBaseGousya(rowNo)
                '外装色名を取得して挿入する'
                If Not StringUtil.IsEmpty(subject.BaseGaisousyoku(rowNo)) Then
                    Dim getGaisoName As New EventEdit.Dao.EventEditBaseCarDaoImpl
                    Dim GaisoName = getGaisoName.FindGaisouColorName(subject.BaseGaisousyoku(rowNo))
                    '外装色名があれば設定する。
                    If Not StringUtil.IsEmpty(GaisoName) Then
                        sheet.Cells(row, sheet.Columns(TAG_GAISO_SHOKU_NAME).Index).Value = Trim(GaisoName.ColorName)
                    End If
                Else
                    sheet.Cells(row, sheet.Columns(TAG_GAISO_SHOKU_NAME).Index).Value = ""
                End If
                '内装色名を取得して挿入する'
                If Not StringUtil.IsEmpty(subject.BaseNaisousyoku(rowNo)) Then
                    Dim getNaisoName As New EventEdit.Dao.EventEditBaseCarDaoImpl
                    Dim NaisoName = getNaisoName.FindNaisouColorName(subject.BaseNaisousyoku(rowNo))
                    If NaisoName Is Nothing Then
                    Else
                        sheet.Cells(row, sheet.Columns(TAG_NAISO_SHOKU_NAME).Index).Value = Trim(NaisoName.ColorName)
                    End If
                Else
                    sheet.Cells(row, sheet.Columns(TAG_NAISO_SHOKU_NAME).Index).Value = ""
                End If
            End If
        End Sub

    End Class
End Namespace