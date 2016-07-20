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
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao.Impl


Namespace EventEdit
    Public Class SpdBaseCarObserver : Implements Frm9SpdObserver, Frm9SpdSetter(Of EventEditBaseCar)
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

        '製作一覧追加タグ
        Private Const TAG_SEISAKU_SYASYU As String = "SEISAKU_SYASYU"
        Private Const TAG_SEISAKU_GRADE As String = "SEISAKU_GRADE"
        Private Const TAG_SEISAKU_SHIMUKE As String = "SEISAKU_SHIMUKE"
        Private Const TAG_SEISAKU_HANDORU As String = "SEISAKU_HANDORU"
        Private Const TAG_SEISAKU_EG_HAIKIRYOU As String = "SEISAKU_EG_HAIKIRYOU"
        Private Const TAG_SEISAKU_EG_KATASHIKI As String = "SEISAKU_EG_KATASHIKI"
        Private Const TAG_SEISAKU_EG_KAKYUUKI As String = "SEISAKU_EG_KAKYUUKI"
        Private Const TAG_SEISAKU_TM_KUDOU As String = "SEISAKU_TM_KUDOU"
        Private Const TAG_SEISAKU_TM_HENSOKUKI As String = "SEISAKU_TM_HENSOKUKI"
        Private Const TAG_SEISAKU_SYATAI_NO As String = "SEISAKU_SYATAI_NO"

#End Region

        Private Shared ReadOnly DEFAULT_LOCKED_BASE_TAGS As String() = {TAG_SHUBETSU, TAG_SEKKEI_TENKAI, TAG_KAIHATSU_FUGO, _
                                                                        TAG_SHIYO_JOHO_NO, TAG_APPLIED_NO, TAG_KATASHIKI, _
                                                                        TAG_SHIMUKE, TAG_OP, TAG_GAISO_SHOKU, TAG_GAISO_SHOKU_NAME, _
                                                                        TAG_NAISO_SHOKU, TAG_NAISO_SHOKU_NAME, TAG_BASE_GOSHA, _
                                                                        TAG_SEISAKU_SYASYU, TAG_SEISAKU_GRADE, TAG_SEISAKU_SHIMUKE, _
                                                                        TAG_SEISAKU_HANDORU, TAG_SEISAKU_EG_HAIKIRYOU, TAG_SEISAKU_EG_KATASHIKI, _
                                                                        TAG_SEISAKU_EG_KAKYUUKI, TAG_SEISAKU_TM_KUDOU, TAG_SEISAKU_TM_HENSOKUKI, _
                                                                        TAG_SEISAKU_SYATAI_NO}
        Private Shared ReadOnly UNLOCKABLE_TAGS As String() = DEFAULT_LOCKED_BASE_TAGS

        Private ReadOnly spread As FpSpread
        Private ReadOnly sheet As SheetView
        Private subject As EventEditBaseCar
        Private ReadOnly titleRows As Integer
        Private validator As SpreadValidator
        '号車DELETEﾁｪｯｸ用に追加
        Private GousyaDeleteCheck As String = ""

        Public Sub New(ByVal spread As FpSpread, ByVal subject As EventEditBaseCar)
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
                subject.NotifyObservers()
            End If
        End Sub

        Public Sub SupersedeSubject(ByVal subject As EventEditBaseCar) Implements Frm9SpdSetter(Of EventEditBaseCar).SupersedeSubject
            Me.subject = subject
            Me.subject.addObserver(Me)
            SpreadUtil.RemoveHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
            ClearSheetData()
            ReInitialize()
            SpreadUtil.AddHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
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
            ''製作一覧追加タグ
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SEISAKU_SYASYU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SEISAKU_GRADE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SEISAKU_SHIMUKE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SEISAKU_HANDORU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SEISAKU_EG_HAIKIRYOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SEISAKU_EG_KATASHIKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SEISAKU_EG_KAKYUUKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SEISAKU_TM_KUDOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SEISAKU_TM_HENSOKUKI

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

            '製作一覧追加タグ
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SEISAKU_SYATAI_NO

            SpreadUtil.BindCellTypeToColumn(sheet, TAG_SHUBETSU, EventControlFactory.GetShubetsuCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_GOSHA, EventControlFactory.GetGoshaCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_SEKKEI_TENKAI, EmptySekkeiTenkaiCheckBoxCell)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_KAIHATSU_FUGO, NewKaihatsuFugoCellType())
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_BASE_EVENT_CODE, NewBaseEventCodeCellType)

            ' 設計展開列は、使用しなくなった。非表示にする。
            sheet.Columns(TAG_SEKKEI_TENKAI).Visible = False

            'カラムヘッダーをクリア
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_SHUBETSU).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_GOSHA).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_SEKKEI_TENKAI).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_KAIHATSU_FUGO).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_SHIYO_JOHO_NO).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_SEISAKU_SYASYU).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_SEISAKU_GRADE).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_SEISAKU_SHIMUKE).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_SEISAKU_HANDORU).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_SEISAKU_EG_HAIKIRYOU).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_SEISAKU_EG_KATASHIKI).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_SEISAKU_EG_KAKYUUKI).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_SEISAKU_TM_KUDOU).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_SEISAKU_TM_HENSOKUKI).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_APPLIED_NO).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_KATASHIKI).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_SHIMUKE).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_OP).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_GAISO_SHOKU).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_GAISO_SHOKU_NAME).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_NAISO_SHOKU).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_NAISO_SHOKU_NAME).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_BASE_EVENT_CODE).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_BASE_GOSHA).Index).Value = " "
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_SEISAKU_SYATAI_NO).Index).Value = " "

            '参考情報はデフォルト非表示
            Call referenceCarUnVisible()


            'InitializeValidator()

            AddHandler spread.VisibleChanged, AddressOf Spread_VisibleChangedEventHandlable
            '' 通常の Spread_Changed()では、CTRL+V/CTRL+ZでChengedイベントが発生しない
            ''（編集モードではない状態で変更された場合は発生しない仕様とのこと。）
            '' CTRL+V/CTRL+Zでもイベントが発生するハンドラを設定する
            SpreadUtil.AddHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
        End Sub

        Public Sub referenceCarInfo(ByVal intColumn As Integer)

            If (intColumn = sheet.Columns(TAG_SHIYO_JOHO_NO).Index) Then
                If sheet.Columns(TAG_SEISAKU_SYASYU).Visible = False Then
                    '非表示の場合＝＞表示に
                    Call referenceCarVisible()
                Else
                    '表示の場合＝＞非表示に
                    Call referenceCarUnVisible()
                End If
            End If

        End Sub

        Public Sub referenceCarVisible()

            '以下の列はデフォルト非表示にする。
            sheet.Columns(TAG_SEISAKU_SYASYU).Visible = True
            sheet.Columns(TAG_SEISAKU_GRADE).Visible = True
            sheet.Columns(TAG_SEISAKU_SHIMUKE).Visible = True
            sheet.Columns(TAG_SEISAKU_HANDORU).Visible = True
            sheet.Columns(TAG_SEISAKU_EG_HAIKIRYOU).Visible = True
            sheet.Columns(TAG_SEISAKU_EG_KATASHIKI).Visible = True
            sheet.Columns(TAG_SEISAKU_EG_KAKYUUKI).Visible = True
            sheet.Columns(TAG_SEISAKU_TM_KUDOU).Visible = True
            sheet.Columns(TAG_SEISAKU_TM_HENSOKUKI).Visible = True
            'ラベルを変更



            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_SHIYO_JOHO_NO).Index).Value = "参考情報 ◀◀"

        End Sub

        Public Sub referenceCarUnVisible()

            '以下の列はデフォルト非表示にする。
            sheet.Columns(TAG_SEISAKU_SYASYU).Visible = False
            sheet.Columns(TAG_SEISAKU_GRADE).Visible = False
            sheet.Columns(TAG_SEISAKU_SHIMUKE).Visible = False
            sheet.Columns(TAG_SEISAKU_HANDORU).Visible = False
            sheet.Columns(TAG_SEISAKU_EG_HAIKIRYOU).Visible = False
            sheet.Columns(TAG_SEISAKU_EG_KATASHIKI).Visible = False
            sheet.Columns(TAG_SEISAKU_EG_KAKYUUKI).Visible = False
            sheet.Columns(TAG_SEISAKU_TM_KUDOU).Visible = False
            sheet.Columns(TAG_SEISAKU_TM_HENSOKUKI).Visible = False
            'ラベルを変更
            sheet.ColumnHeader.Cells.Get(0, sheet.Columns(TAG_SHIYO_JOHO_NO).Index).Value = "参考情報 ►►"

        End Sub

        Public Sub ReInitialize() Implements Frm9SpdObserver.ReInitialize
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_KAIHATSU_FUGO, NewKaihatsuFugoCellType())
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_BASE_EVENT_CODE, NewBaseEventCodeCellType)
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
#Region "Base Car CellType Factory Method"

        Private Function NewSekkeiTenkaiCellType() As CheckBoxCellType
            Dim result As CheckBoxCellType = ShisakuSpreadUtil.NewGeneralCheckBoxCellType()
            Return result
        End Function
        Private Function NewKaihatsuFugoCellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.GetLabelValues_ShisakuKaihatuFugo)
            result.MaxLength = 4
            Return result
        End Function
        Private Function NewShiyoJohoNoCellTypes(ByVal rowNo As Integer) As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.BaseShiyoujyouhouNoLabelValues(rowNo))
            result.MaxLength = 4
            Return result
        End Function
        Private Function NewAppliedNoCellTypes(ByVal rowNo As Integer) As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.BaseAppliedNoLabelValues(rowNo))
            result.MaxLength = 3
            Return result
        End Function
        Private Function NewKatashikiCellTypes(ByVal rowNo As Integer) As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.BaseKatashikiLabelValues(rowNo))
            result.MaxLength = 7
            Return result
        End Function
        Private Function NewShimukeCellTypes(ByVal rowNo As Integer) As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.BaseShimukeLabelValues(rowNo), False)
            result.MaxLength = 2
            Return result
        End Function
        Private Function NewOpCellTypes(ByVal rowNo As Integer) As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.BaseOpLabelValues(rowNo))
            result.MaxLength = 3
            Return result
        End Function
        Private Function NewGaisoShokuCellTypes(ByVal rowNo As Integer) As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.BaseGaisousyokuLabelValues(rowNo))
            result.MaxLength = 3
            Return result
        End Function
        Private Function NewNaisoShokuCellTypes(ByVal rowNo As Integer) As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.BaseNaisousyokuLabelValues(rowNo))
            result.MaxLength = 3
            Return result
        End Function
        Private Function NewBaseEventCodeCellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.GetLabelValues_ShisakuEventCode)
            result.MaxLength = 12
            Return result
        End Function
        Private Function NewBaseGoshaCellTypes(ByVal rowNo As Integer) As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.ShisakuBaseGousyaLabelValues(rowNo))
            result.MaxLength = 8
            Return result
        End Function

#End Region

        Public Sub Update(ByVal observable As Observable, ByVal args As Object) Implements Observer.Update
            If args Is Nothing Then
                LockSheetIfViewerChange()
                For Each key As Integer In subject.GetInputRowNos
                    Update(Nothing, key)
                Next
            Else
                If Not IsNumeric(args) Then
                    Return
                End If
                Dim rowNo As Integer = CType(args, Integer)
                Dim row As Integer = titleRows + rowNo
                If subject.ShisakuSyubetu(rowNo) Is Nothing OrElse StringUtil.IsEmpty(subject.ShisakuSyubetu(rowNo).Trim) Then
                    sheet.Cells(row, sheet.Columns(TAG_SHUBETSU).Index).Value = Nothing
                Else
                    sheet.Cells(row, sheet.Columns(TAG_SHUBETSU).Index).Value = subject.ShisakuSyubetu(rowNo)
                End If
                sheet.Cells(row, sheet.Columns(TAG_SHUBETSU).Index).Locked = False      '2012/01/09
                sheet.Cells(row, sheet.Columns(TAG_GOSHA).Index).Value = subject.ShisakuGousya(rowNo)

                If subject.IsEditModes(rowNo) Then
                    SpreadUtil.BindCellTypeToCell(sheet, row, TAG_SEKKEI_TENKAI, SekkeiTenkaiCheckBoxCell)
                End If
                If subject.SekkeiTenkaiKbn(rowNo) Is Nothing Then
                    sheet.Cells(row, sheet.Columns(TAG_SEKKEI_TENKAI).Index).Value = Nothing
                Else
                    sheet.Cells(row, sheet.Columns(TAG_SEKKEI_TENKAI).Index).Value = Convert.ToBoolean(subject.SekkeiTenkaiKbn(rowNo))
                End If

                sheet.Cells(row, sheet.Columns(TAG_KAIHATSU_FUGO).Index).Value = subject.BaseKaihatsuFugo(rowNo)

                sheet.Cells(row, sheet.Columns(TAG_SHIYO_JOHO_NO).Index).Value = subject.BaseShiyoujyouhouNo(rowNo)
                SpreadUtil.BindCellTypeToCell(sheet, row, TAG_SHIYO_JOHO_NO, NewShiyoJohoNoCellTypes(rowNo))

                sheet.Cells(row, sheet.Columns(TAG_APPLIED_NO).Index).Value = subject.BaseAppliedNo(rowNo)
                SpreadUtil.BindCellTypeToCell(sheet, row, TAG_APPLIED_NO, NewAppliedNoCellTypes(rowNo))

                sheet.Cells(row, sheet.Columns(TAG_KATASHIKI).Index).Value = subject.BaseKatashiki(rowNo)
                SpreadUtil.BindCellTypeToCell(sheet, row, TAG_KATASHIKI, NewKatashikiCellTypes(rowNo))

                sheet.Cells(row, sheet.Columns(TAG_SHIMUKE).Index).Value = subject.BaseShimuke(rowNo)
                SpreadUtil.BindCellTypeToCell(sheet, row, TAG_SHIMUKE, NewShimukeCellTypes(rowNo))

                sheet.Cells(row, sheet.Columns(TAG_OP).Index).Value = subject.BaseOp(rowNo)
                SpreadUtil.BindCellTypeToCell(sheet, row, TAG_OP, NewOpCellTypes(rowNo))

                sheet.Cells(row, sheet.Columns(TAG_GAISO_SHOKU).Index).Value = subject.BaseGaisousyoku(rowNo)
                SpreadUtil.BindCellTypeToCell(sheet, row, TAG_GAISO_SHOKU, NewGaisoShokuCellTypes(rowNo))

                '外装色名と内装色名をクリア'
                sheet.Cells(row, sheet.Columns(TAG_GAISO_SHOKU_NAME).Index).Value = Nothing
                sheet.Cells(row, sheet.Columns(TAG_NAISO_SHOKU_NAME).Index).Value = Nothing
                '外装色名を取得して挿入する'
                If Not StringUtil.IsEmpty(subject.BaseGaisousyoku(rowNo)) Then
                    Dim getGaisoName As New EventEdit.Dao.EventEditBaseCarDaoImpl
                    Dim GaisoName = getGaisoName.FindGaisouColorName(subject.BaseGaisousyoku(rowNo))
                    '外装色名があれば設定する。
                    If Not StringUtil.IsEmpty(GaisoName) Then
                        sheet.Cells(row, sheet.Columns(TAG_GAISO_SHOKU_NAME).Index).Value = Trim(GaisoName.ColorName)
                        SpreadUtil.BindCellTypeToCell(sheet, row, TAG_GAISO_SHOKU, NewGaisoShokuCellTypes(rowNo))
                    End If
                End If

                sheet.Cells(row, sheet.Columns(TAG_NAISO_SHOKU).Index).Value = subject.BaseNaisousyoku(rowNo)
                SpreadUtil.BindCellTypeToCell(sheet, row, TAG_NAISO_SHOKU, NewNaisoShokuCellTypes(rowNo))

                '内装色名を取得して挿入する'
                If Not StringUtil.IsEmpty(subject.BaseNaisousyoku(rowNo)) Then
                    Dim getNaisoName As New EventEdit.Dao.EventEditBaseCarDaoImpl
                    Dim NaisoName = getNaisoName.FindNaisouColorName(subject.BaseNaisousyoku(rowNo))
                    If NaisoName Is Nothing Then
                    Else
                        sheet.Cells(row, sheet.Columns(TAG_NAISO_SHOKU_NAME).Index).Value = Trim(NaisoName.ColorName)
                        SpreadUtil.BindCellTypeToCell(sheet, row, TAG_GAISO_SHOKU, NewGaisoShokuCellTypes(rowNo))
                    End If
                End If

                sheet.Cells(row, sheet.Columns(TAG_BASE_EVENT_CODE).Index).Value = subject.ShisakuBaseEventCode(rowNo)

                sheet.Cells(row, sheet.Columns(TAG_BASE_GOSHA).Index).Value = subject.ShisakuBaseGousya(rowNo)
                SpreadUtil.BindCellTypeToCell(sheet, row, TAG_BASE_GOSHA, NewBaseGoshaCellTypes(rowNo))

                '車体№
                sheet.Cells(row, sheet.Columns(TAG_SEISAKU_SYATAI_NO).Index).Value = subject.SeisakuSyataiNo(rowNo)

                '2012/2/19　下期改修でロックは解除
                '設計展開時にはロック'
                'If Not subject.IsSekkeiTenkaiIkou Then
                OnRowLock(row)
                'End If
                '2012/01/09
                SetActiveSpreadColor(sheet, row, sheet.Columns(TAG_SHUBETSU).Index)

                '参考情報のセット
                sheet.Cells(row, sheet.Columns(TAG_SEISAKU_SYASYU).Index).Value = subject.SeisakuSyasyu(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SEISAKU_GRADE).Index).Value = subject.SeisakuGrade(rowNo)

                '仕向地・仕向け変換
                sheet.Cells(row, sheet.Columns(TAG_SEISAKU_SHIMUKE).Index).Value = _
                        subject.ShimukechiShimukeHenkan(subject.SeisakuShimuke(rowNo), _
                                                        subject.BaseShimuke(rowNo))

                sheet.Cells(row, sheet.Columns(TAG_SEISAKU_HANDORU).Index).Value = ""
                If StringUtil.IsNotEmpty(subject.SeisakuHandoru(rowNo)) And StringUtil.Equals(subject.SeisakuHandoru(rowNo), "L") Or _
                    StringUtil.IsNotEmpty(subject.SeisakuHandoru(rowNo)) And StringUtil.Equals(subject.SeisakuHandoru(rowNo), "R") Then
                    sheet.Cells(row, sheet.Columns(TAG_SEISAKU_HANDORU).Index).Value = subject.SeisakuHandoru(rowNo) & "HD"   'HDを付ける。
                Else
                    sheet.Cells(row, sheet.Columns(TAG_SEISAKU_HANDORU).Index).Value = subject.SeisakuHandoru(rowNo)
                End If
                sheet.Cells(row, sheet.Columns(TAG_SEISAKU_EG_HAIKIRYOU).Index).Value = subject.SeisakuEgHaikiryou(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SEISAKU_EG_KATASHIKI).Index).Value = subject.SeisakuEgKatashiki(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SEISAKU_EG_KAKYUUKI).Index).Value = subject.SeisakuEgKakyuuki(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SEISAKU_TM_KUDOU).Index).Value = subject.SeisakuTmKudou(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SEISAKU_TM_HENSOKUKI).Index).Value = subject.SeisakuTmHensokuki(rowNo)

            End If
        End Sub

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
        Private Sub LockSheetIfViewerChange()

            If Not subject.IsViewerMode.Equals(backIsViewer) Then
                backIsViewer = subject.IsViewerMode
                If subject.IsViewerMode OrElse subject.IsSekkeiTenkaiIkou Then
                    LockAllRowsByRule(True)
                    SpreadUtil.LockAllColumns(sheet)
                Else
                    SpreadUtil.UnlockAllColumns(sheet)
                    '以下のロックは不要？
                    'InitializeColumnsLock()
                    LockAllRowsByRule(False)
                End If
            End If
        End Sub

        Private Sub InitializeColumnsLock()

            For Each Tag As String In DEFAULT_LOCKED_BASE_TAGS
                sheet.Columns(Tag).Locked = True
            Next
        End Sub

        Private Sub LockAllRowsByRule(ByVal isLocked As Boolean)

            For Each rowNo As Integer In subject.GetInputRowNos
                Dim row As Integer = titleRows + rowNo
                LockRowByRule(row, isLocked)
            Next
        End Sub

        Private Sub LockRowByRule(ByVal row As Integer, ByVal IsLocked As Boolean)
            '          Dim aaa As New Frm9DispEventEdit
            For Each Tag As String In UNLOCKABLE_TAGS
                sheet.Cells(row, sheet.Columns(Tag).Index).Locked = IsLocked
                '号車をデリートした場合、全項目削除する。 但し、設計展開以外　※コピー、EXCEL取込時は以下のイベントを無効にする。
                'If IsLocked = True And Tag <> TAG_SEKKEI_TENKAI Then
                '    sheet.Cells(row, sheet.Columns(Tag).Index).Value = Nothing
                'End If
                If sheet.Cells(row, sheet.Columns(TAG_GOSHA).Index).Value = Nothing And Tag <> TAG_SEKKEI_TENKAI Then
                    sheet.Cells(row, sheet.Columns(Tag).Index).Value = Nothing
                End If
            Next
        End Sub
        Private Sub OnRowLock(ByVal row As Integer)
            Dim rowNo As Integer = row - titleRows
            If subject.IsEditModes(rowNo) Then
                LockRowByRule(row, False)
            Else
                LockRowByRule(row, True)
            End If
            '外装色名と内装色名は編集させない'
            sheet.Cells(row, sheet.Columns(TAG_GAISO_SHOKU_NAME).Index).Locked = True
            sheet.Cells(row, sheet.Columns(TAG_NAISO_SHOKU_NAME).Index).Locked = True

            '製作一覧追加分は編集させない
            sheet.Cells(row, sheet.Columns(TAG_SEISAKU_SYASYU).Index).Locked = True
            sheet.Cells(row, sheet.Columns(TAG_SEISAKU_GRADE).Index).Locked = True
            sheet.Cells(row, sheet.Columns(TAG_SEISAKU_SHIMUKE).Index).Locked = True
            sheet.Cells(row, sheet.Columns(TAG_SEISAKU_HANDORU).Index).Locked = True
            sheet.Cells(row, sheet.Columns(TAG_SEISAKU_EG_HAIKIRYOU).Index).Locked = True
            sheet.Cells(row, sheet.Columns(TAG_SEISAKU_EG_KATASHIKI).Index).Locked = True
            sheet.Cells(row, sheet.Columns(TAG_SEISAKU_EG_KAKYUUKI).Index).Locked = True
            sheet.Cells(row, sheet.Columns(TAG_SEISAKU_TM_KUDOU).Index).Locked = True
            sheet.Cells(row, sheet.Columns(TAG_SEISAKU_TM_HENSOKUKI).Index).Locked = True
            '   但し、車体№だけは編集ＯＫ
            sheet.Cells(row, sheet.Columns(TAG_SEISAKU_SYATAI_NO).Index).Locked = False

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
                Case TAG_SEKKEI_TENKAI
                    subject.SekkeiTenkaiKbn(rowNo) = Convert.ToString(value)

                Case TAG_KAIHATSU_FUGO
                    subject.BaseKaihatsuFugo(rowNo) = Convert.ToString(value)
                    '参考情報再取得
                    subject.Apply(rowNo)

                Case TAG_SHIYO_JOHO_NO
                    subject.BaseShiyoujyouhouNo(rowNo) = Convert.ToString(value)
                    '参考情報再取得
                    subject.Apply(rowNo)

                Case TAG_APPLIED_NO
                    subject.BaseAppliedNo(rowNo) = Convert.ToString(value)
                    '参考情報再取得
                    subject.Apply(rowNo)

                Case TAG_KATASHIKI
                    subject.BaseKatashiki(rowNo) = Convert.ToString(value)
                    '参考情報再取得
                    subject.Apply(rowNo)

                Case TAG_SHIMUKE
                    subject.BaseShimuke(rowNo) = Convert.ToString(value)
                    '参考情報再取得
                    subject.Apply(rowNo)

                Case TAG_OP
                    subject.BaseOp(rowNo) = Convert.ToString(value)
                    '参考情報再取得
                    subject.Apply(rowNo)

                Case TAG_GAISO_SHOKU
                    subject.BaseGaisousyoku(rowNo) = Convert.ToString(value)
                    '参考情報再取得
                    subject.Apply(rowNo)

                Case TAG_NAISO_SHOKU
                    subject.BaseNaisousyoku(rowNo) = Convert.ToString(value)
                    '参考情報再取得
                    subject.Apply(rowNo)

                Case TAG_BASE_EVENT_CODE
                    subject.ShisakuBaseEventCode(rowNo) = Convert.ToString(value)

                Case TAG_BASE_GOSHA
                    subject.ShisakuBaseGousya(rowNo) = Convert.ToString(value)

                Case TAG_SEISAKU_SYATAI_NO
                    subject.SeisakuSyataiNo(rowNo) = Convert.ToString(value)

            End Select
            '
            subject.NotifyObservers(rowNo)
        End Sub

        Public Sub AssertValidateRegister()
            ''2014/06/30 追加 E.Ubukata
            Dim daoEvent As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim daoEventBase As TShisakuEventBaseDao = New TShisakuEventBaseDaoImpl

            For Each rowNo As Integer In subject.GetInputRowNos

                Dim row As Integer = titleRows + rowNo
                '号車があるときだけチェックします。
                If Not StringUtil.IsEmpty(sheet.Cells(row, sheet.Columns(TAG_GOSHA).Index).Value) Then

                    'エラーチェック
                    Dim ErrCheckBase01 As String = ""
                    Dim ErrCheckBase02 As String = ""
                    Dim ErrCheckEvent01 As String = ""
                    Dim ErrCheckEvent02 As String = ""
                    Dim ErrMsg As String = ""

                    If Not StringUtil.IsEmpty(sheet.Cells(row, sheet.Columns(TAG_KAIHATSU_FUGO).Index).Value) Then
                        ErrCheckBase01 = "OK"
                    End If
                    If Not StringUtil.IsEmpty(sheet.Cells(row, sheet.Columns(TAG_SHIYO_JOHO_NO).Index).Value) _
                        And Not StringUtil.IsEmpty(sheet.Cells(row, sheet.Columns(TAG_APPLIED_NO).Index).Value) _
                        And Not StringUtil.IsEmpty(sheet.Cells(row, sheet.Columns(TAG_KATASHIKI).Index).Value) _
                        And Not StringUtil.IsEmpty(sheet.Cells(row, sheet.Columns(TAG_SHIMUKE).Index).Value) _
                        And Not StringUtil.IsEmpty(sheet.Cells(row, sheet.Columns(TAG_OP).Index).Value) _
                        And Not StringUtil.IsEmpty(sheet.Cells(row, sheet.Columns(TAG_GAISO_SHOKU).Index).Value) _
                        And Not StringUtil.IsEmpty(sheet.Cells(row, sheet.Columns(TAG_NAISO_SHOKU).Index).Value) Then
                        ErrCheckBase02 = "OK"
                    End If

                    If Not StringUtil.IsEmpty(sheet.Cells(row, sheet.Columns(TAG_BASE_EVENT_CODE).Index).Value) Then
                        ErrCheckEvent01 = "OK"
                    End If
                    If Not StringUtil.IsEmpty(sheet.Cells(row, sheet.Columns(TAG_BASE_GOSHA).Index).Value) Then
                        ErrCheckEvent02 = "OK"
                    End If

                    'バリデーターNEW
                    validator = New SpreadValidator(spread)
                    ''必須チェック
                    Dim validA As New SpreadValidator(spread)
                    validA.CheckAll = True

                    ''エラーメッセージ 及び　エラーチェック
                    If ErrCheckBase01 = "OK" And ErrCheckBase02 = "" And ErrCheckEvent01 = "" And ErrCheckEvent02 = "" Then
                        ErrMsg = "ベース情報を正しく入力して下さい。"
                        validA.Add(TAG_SHIYO_JOHO_NO).Required()
                        validA.Add(TAG_APPLIED_NO).Required()
                        validA.Add(TAG_KATASHIKI).Required()
                        'validA.Add(TAG_SHIMUKE).Required()
                        validA.Add(TAG_OP).Required()
                        validA.Add(TAG_GAISO_SHOKU).Required()
                        validA.Add(TAG_NAISO_SHOKU).Required()
                    ElseIf ErrCheckBase01 = "" And ErrCheckBase02 = "" And ErrCheckEvent01 = "OK" And ErrCheckEvent02 = "" Then
                        ErrMsg = "試作情報を正しく入力して下さい。"
                        validA.Add(TAG_BASE_GOSHA).Required()

                        ''2015/04/29　チェックが必要になったので復帰させました。E.Ubukata
                        ''↓↓2014/08/13 1 ベース部品表作成表機能増強_Ⅰ①) (ダニエル上海)柳沼 ADD BEGIN
                    ElseIf ErrCheckBase01 = "OK" And ErrCheckEvent01 = "OK" Then
                        ErrMsg = "ベース車情報はどちらかのみ入力してください。"
                        validA.Add(TAG_KAIHATSU_FUGO).Empty()
                        validA.Add(TAG_BASE_EVENT_CODE).Empty()
                        ''↑↑2014/08/13 1 ベース部品表作成表機能増強_Ⅰ①) (ダニエル上海)柳沼 ADD END

                    End If

                    ''イベントコード指定の際に設計展開で不具合が発生するパターンの場合エラーとする
                    ''2015/06/30 追加 E.Ubukata
                    If StringUtil.IsEmpty(ErrMsg) AndAlso ErrCheckEvent01 = "OK" Then
                        Dim voEvent As TShisakuEventVo = daoEvent.FindByPk(sheet.Cells(row, sheet.Columns(TAG_BASE_EVENT_CODE).Index).Value)
                        If voEvent.BlockAlertKind = "2" Then
                            ErrMsg = "前回、移管車改修を指定したイベントは設定できません。"
                            validA.Add(TAG_BASE_EVENT_CODE).Empty()
                        Else
                            Dim voEventBasePar As New TShisakuEventBaseVo
                            voEventBasePar.ShisakuEventCode = sheet.Cells(row, sheet.Columns(TAG_BASE_EVENT_CODE).Index).Value
                            'voEventBasePar.ShisakuGousya = sheet.Cells(row, sheet.Columns(TAG_BASE_GOSHA).Index).Value
                            For Each voEventBase As TShisakuEventBaseVo In daoEventBase.FindBy(voEventBasePar)
                                If StringUtil.IsNotEmpty(voEventBase.ShisakuBaseEventCode) Then
                                    ErrMsg = "前回、試作手配システムから作成されたイベントは設定できません。"
                                    validA.Add(TAG_BASE_EVENT_CODE).Empty()
                                End If
                            Next
                        End If
                    End If



                    Dim validAorBorC As New SpreadValidator(spread, ErrMsg)
                    validAorBorC.Add(validA)

                    validator.Add(TAG_GOSHA, "号車").Required()
                    validator.Add(validAorBorC)

                    'これは止める
                    validator.AssertValidate(row)

                End If

                'TODO №132の改修対応:スプレッドの値を[ベース][試作]の列範囲に分け、評価を行なう
                'ここで画面の値を取得し、下記条件を判定して評価を行なう

                '|BASE |PROTO|
                '|A |B |X |Y |

                '|○|○|×|×|  ⇒OK
                '|×|×|○|○|  ⇒OK
                '|×|×|×|×|  ⇒OK

                '|○|×|×|×|  ⇒エラー①「ベース情報を正しく入力して下さい」
                '|×|×|○|×|  ⇒エラー②「試作情報を正しく入力して下さい」
                '|○|×|○|×|  ⇒エラー③「ベース車情報はどちらかのみ入力して下さい」

                'SpdAlObserverの#395　そこを参考にエラーメッセージを生成する仕組みで
                ''実装してください。

            Next
        End Sub
    End Class
End Namespace