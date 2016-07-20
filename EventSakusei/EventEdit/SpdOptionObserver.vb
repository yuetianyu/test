Imports FarPoint.Win.Spread.CellType
Imports EventSakusei.EventEdit.Logic
Imports EBom.Common
Imports FarPoint.Win.Spread
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports EventSakusei.EventEdit.Ui
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd

Namespace EventEdit
    Public Class SpdOptionObserver : Implements Frm9SpdObserver, Frm9SpdSetter(Of EventEditOption)

#Region "各列のTAG"
        Private Const TAG_SHUBETSU As String = "SHUBETSU"
        Private Const TAG_GOSHA As String = "GOSHA"
#End Region

        Private Const TITLE_ROW As Integer = 2
        Private Const TITLE_ROW_DAI As Integer = 0
        Private Const TITLE_ROW_CHU As Integer = 1
        Private Const LOCKED_TITLE_COLUMN_COUNT As Integer = 2
        Private Const LOCK_CONTROL_START_COLUMN_NO As Integer = 2
        Private Shared ReadOnly DEFAULT_LOCK_TAGS As String() = {TAG_SHUBETSU, TAG_GOSHA}
        Private Shared ReadOnly UNLOCKABLE_TAGS As String() = DEFAULT_LOCK_TAGS

        Private ReadOnly spread As FpSpread
        Private ReadOnly sheet As SheetView
        Private subject As EventEditOption
        Private ReadOnly titleRows As Integer
        Private columnCount As Integer
        '号車DELETEﾁｪｯｸ用に追加
        Private GousyaDeleteCheck As String = ""
        '列挿入用'
        Private ReadOnly aOptionColumns As OptionColumns

        Public Sub New(ByVal spread As FpSpread, ByVal subject As EventEditOption, ByVal columnCount As Integer)
            Me.spread = spread
            If spread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            Me.sheet = spread.Sheets(0)
            Me.subject = subject

            Me.titleRows = EventSpreadUtil.GetTitleRows(sheet)
            Me.columnCount = columnCount

            Dim spdBorderFactory As New SpreadBorderFactory()

            'aOptionColumns = New OptionColumns(sheet, spdBorderFactory, AddressOf GetStartColumn)
            aOptionColumns = New OptionColumns(sheet, spdBorderFactory)
            aOptionColumns.Initialize(columnCount)


            subject.addObserver(Me)
        End Sub
        ''' <summary>
        ''' 装備仕様のタイトルが重複しているかどうかをチェック
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function AssertValidateRegister() As Boolean
            '2012/03/13 メソッド追加
            Dim ht As New Hashtable
            Dim row As Integer = 0
            For i As Integer = 2 To columnCount
                Dim value As String = ""
                Dim valueDai As String = ""
                Dim valueChu As String = ""

                If StringUtil.IsNotEmpty(sheet.Cells(row + 2, i).Value()) Then
                    value = sheet.Cells(row + 2, i).Value().ToString.TrimEnd
                End If
                If StringUtil.IsNotEmpty(sheet.Cells(row, i).Value()) Then
                    valueDai = sheet.Cells(row, i).Value().ToString.TrimEnd
                End If
                If StringUtil.IsNotEmpty(sheet.Cells(row + 1, i).Value()) Then
                    valueChu = sheet.Cells(row + 1, i).Value().ToString.TrimEnd
                End If

                If Not StringUtil.IsEmpty(value) Then
                    If ht.ContainsKey(valueDai & valueChu & value) Then
                        For j As Integer = 2 To i

                            Dim Hvalue As String = ""
                            Dim HvalueDai As String = ""
                            Dim HvalueChu As String = ""
                            If StringUtil.IsNotEmpty(sheet.Cells(row + 2, j).Value()) Then
                                Hvalue = sheet.Cells(row + 2, j).Value().ToString.TrimEnd
                            End If
                            If StringUtil.IsNotEmpty(sheet.Cells(row, j).Value()) Then
                                HvalueDai = sheet.Cells(row, j).Value().ToString.TrimEnd
                            End If
                            If StringUtil.IsNotEmpty(sheet.Cells(row + 1, j).Value()) Then
                                HvalueChu = sheet.Cells(row + 1, j).Value().ToString.TrimEnd
                            End If

                            If Hvalue = value And _
                                HvalueDai = valueDai And _
                                HvalueChu = valueChu And i <> j Then
                                sheet.Cells(row, j, row + 2, j).BackColor = ERROR_COLOR
                            End If
                        Next
                        ComFunc.ShowErrMsgBox("装備仕様が重複しています。")
                        Return False
                    Else
                        ht.Add(valueDai & valueChu & value, "")
                    End If
                End If
            Next
            Return True
        End Function

        Private Sub Spread_ChangeEventHandlable(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs)
            '-1の時はエラーがでるので回避させておく'
            If e.Row >= 0 Then
                OnChange(e.Row, e.Column)
            End If
        End Sub

        Private Sub Spread_VisibleChangedEventHandlable(ByVal sender As Object, ByVal e As System.EventArgs)
            If spread.Visible Then
                subject.NotifyObservers()
            End If
        End Sub

        Public Sub SupersedeSubject(ByVal subject As EventEditOption) Implements Frm9SpdSetter(Of EventEditOption).SupersedeSubject
            Me.subject = subject
            Me.subject.AddObserver(Me)
            SpreadUtil.RemoveHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
            ClearSheetData()
            ClearSheetBackColor()
            ReInitialize()
            SpreadUtil.AddHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
        End Sub

        Public Sub ClearSheetBackColor() Implements Frm9SpdObserver.ClearSheetBackColor
            sheet.Cells(titleRows, 0, sheet.RowCount - 1, sheet.ColumnCount - 1).ResetBackColor()
            sheet.Cells(0, LOCKED_TITLE_COLUMN_COUNT, sheet.RowCount - 1, sheet.ColumnCount - 1).ResetBackColor()
        End Sub
        Public Sub ClearSheetData() Implements Frm9SpdObserver.ClearSheetData
            ''''最大行を定数指定に修正
            sheet.RowCount = titleRows + 200
            sheet.ClearRange(titleRows, 0, sheet.RowCount - titleRows, sheet.ColumnCount, True)
            sheet.ClearRange(0, LOCKED_TITLE_COLUMN_COUNT, sheet.RowCount - titleRows, sheet.ColumnCount, True)
        End Sub



        Public Sub Initialize() Implements Frm9SpdObserver.Initialize

            EventSpreadUtil.InitializeFrm9(spread)

            sheet.ColumnCount = LOCKED_TITLE_COLUMN_COUNT
            sheet.ColumnCount = LOCKED_TITLE_COLUMN_COUNT + columnCount

            Dim aComplexBorder As FarPoint.Win.ComplexBorder = New FarPoint.Win.ComplexBorder(New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine))
            For columnNo As Integer = LOCK_CONTROL_START_COLUMN_NO To sheet.ColumnCount - 1
                sheet.Cells(0, columnNo).Border = aComplexBorder
                'sheet.Cells(0, columnNo).RowSpan = 2
                sheet.Columns(columnNo).Width = 21.0!
            Next
            sheet.SetColumnSizeable(sheet.ColumnCount, False)

            Dim index As Integer = 0
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHUBETSU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GOSHA
            ''Spread列号車のFont設定
            sheet.GetColumnFromTag(Nothing, TAG_GOSHA).Font = New Font("MS Gothic", 9)

            SpreadUtil.BindCellTypeToColumn(sheet, TAG_SHUBETSU, EventControlFactory.GetShubetsuCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_GOSHA, EventControlFactory.GetGoshaCellType)

            sheet.Rows(TITLE_ROW_DAI).CellType = TitleInputCellType
            sheet.Rows(TITLE_ROW_CHU).CellType = TitleInputCellType
            sheet.Rows(TITLE_ROW).CellType = TitleInputCellType
            For columnNo As Integer = LOCK_CONTROL_START_COLUMN_NO To sheet.ColumnCount - 1
                SpreadUtil.BindCellTypeToColumn(sheet, columnNo, DataInputCellType)
            Next

            AddHandler spread.LeaveCell, AddressOf OnLeaveCell

            AddHandler spread.VisibleChanged, AddressOf Spread_VisibleChangedEventHandlable
            '' 通常の Spread_Changed()では、CTRL+V/CTRL+ZでChengedイベントが発生しない
            ''（編集モードではない状態で変更された場合は発生しない仕様とのこと。）
            '' CTRL+V/CTRL+Zでもイベントが発生するハンドラを設定する
            SpreadUtil.AddHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
        End Sub

        Public Sub ReInitialize() Implements Frm9SpdObserver.ReInitialize
            ' nop
        End Sub

        ''' <summary>
        ''' Spread.LeaveCellEventHandler IME制御
        ''' </summary>
        ''' <param name="sender">LeaveCellEventHandlerに従う</param>
        ''' <param name="e">LeaveCellEventHandlerに従う</param>
        ''' <remarks></remarks>
        Private Sub OnLeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)
            'If e.NewColumn <> sheet.Columns(TAG_SHUBETSU).Index _
            '        AndAlso e.NewColumn <> sheet.Columns(TAG_GOSHA).Index _
            '        AndAlso Not sheet.GetStyleInfo(e.NewRow, e.NewColumn).Locked Then
            '    spread.ImeMode = Windows.Forms.ImeMode.Hiragana
            'Else
            '    spread.ImeMode = Windows.Forms.ImeMode.Disable
            'End If
        End Sub

#Region "装備で使用するセル"
        Private _titleInputCellType As TextCellType
        Private ReadOnly Property TitleInputCellType() As TextCellType
            Get
                If _titleInputCellType Is Nothing Then
                    _titleInputCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    '↓↓2014/10/29 酒井 ADD BEGIN
                    'Ver6_2 1.95以降の修正内容の展開
                    '_titleInputCellType.MaxLength = Convert.ToInt32(122 / 2)
                    ' 2014/07/14 122バイトで制限をかけるよう変更
                    '            実装はFrm9DispEventEdit.vb内にdatamodel_Chageイベントを設定しました
                    '_titleInputCellType.MaxLength = Convert.ToInt32(122 / 2)
                    _titleInputCellType.MaxLength = 122
                    '↑↑2014/10/29 酒井 ADD END
                    '_titleInputCellType.CharacterSet = CharacterSet.AllIME
                    _titleInputCellType.TextOrientation = FarPoint.Win.TextOrientation.TextVertical '横書き
                End If
                Return _titleInputCellType
            End Get
        End Property
        Private _dataInputCellType As TextCellType
        Private ReadOnly Property DataInputCellType() As TextCellType
            Get
                If _dataInputCellType Is Nothing Then
                    _dataInputCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _dataInputCellType.MaxLength = 2
                    '_dataInputCellType.MaxLength = Convert.ToInt32(2 / 2)

                    '_dataInputCellType.CharacterSet = CharacterSet.KanjiOnlyIME
                End If
                Return _dataInputCellType
            End Get
        End Property
#End Region

        Public Sub Update(ByVal observable As Observable, ByVal args As Object) Implements Observer.Update
            If args Is Nothing Then
                LockSheetIfViewerChange()
                Update(Nothing, -1)
                For Each key As Integer In subject.GetInputRowNos
                    Update(Nothing, key)
                Next
            Else
                If Not IsNumeric(args) Then
                    Return
                End If
                Dim rowNo As Integer = Convert.ToInt32(args)
                If IsTitleRow(rowNo) Then
                    '' タイトル欄
                    If subject.IsSekkeiTenkaiIkou Then
                        For Each columnNo As Integer In subject.GetInputTitleNameColumnNos
                            sheet.Cells(TITLE_ROW, GetColumn(columnNo)).Value = subject.TitleName(columnNo)
                            sheet.Cells(TITLE_ROW_DAI, GetColumn(columnNo)).Value = subject.TitleNameDai(columnNo)
                            sheet.Cells(TITLE_ROW_CHU, GetColumn(columnNo)).Value = subject.TitleNameChu(columnNo)

                            '------------------------------------------------------------------------------
                            '２次改修
                            If StringUtil.IsEmpty(ParaBasicOptionFlg) And StringUtil.IsEmpty(ParaOptionFlg) Then
                                ParaBasicOptionCol = ParaBasicOptionCol + 1
                            ElseIf StringUtil.IsEmpty(ParaOptionFlg) Then
                                ParaOptionCol = ParaOptionCol + 1
                            End If
                            '------------------------------------------------------------------------------

                            If StringUtil.IsEmpty(ParaOptionFlg) Then
                                '------------------------------------------------------------------------------
                                '２次改修
                                '   設計展開後、登録済の装備仕様の変更は不可とする。
                                If StringUtil.IsNotEmpty(sheet.Cells(TITLE_ROW, GetColumn(columnNo)).Value) Then
                                    sheet.Cells(TITLE_ROW, GetColumn(columnNo)).Locked = True
                                    sheet.Cells(TITLE_ROW_DAI, GetColumn(columnNo)).Locked = True
                                    sheet.Cells(TITLE_ROW_CHU, GetColumn(columnNo)).Locked = True
                                End If
                                'sheet.Cells(TITLE_ROW, GetColumn(columnNo)).Locked = False
                                '------------------------------------------------------------------------------
                            End If

                            ''------------------------------------------------------------------------------
                            ''２次改修
                            ''   設計展開後、登録済の装備仕様の変更は不可とする。
                            'If StringUtil.IsNotEmpty(sheet.Cells(TITLE_ROW, GetColumn(columnNo)).Value) Then
                            '    sheet.Cells(TITLE_ROW, GetColumn(columnNo)).Locked = True
                            'End If
                            ''sheet.Cells(TITLE_ROW, GetColumn(columnNo)).Locked = False
                            ''------------------------------------------------------------------------------

                        Next

                        '------------------------------------------------------------------------------
                        '２次改修
                        If StringUtil.IsEmpty(ParaBasicOptionFlg) And StringUtil.IsEmpty(ParaOptionFlg) Then
                            ParaBasicOptionFlg = "OK"
                        ElseIf StringUtil.IsEmpty(ParaOptionFlg) Then
                            ParaOptionFlg = "OK"
                        End If
                        '------------------------------------------------------------------------------

                    Else
                        For Each columnNo As Integer In subject.GetInputTitleNameColumnNos
                            '最初にやたら多いときがあるからこの辺りで列の追加がいる'
                            Try
                                sheet.Cells(TITLE_ROW, GetColumn(columnNo)).Value = subject.TitleName(columnNo)
                                sheet.Cells(TITLE_ROW_DAI, GetColumn(columnNo)).Value = subject.TitleNameDai(columnNo)
                                sheet.Cells(TITLE_ROW_CHU, GetColumn(columnNo)).Value = subject.TitleNameChu(columnNo)
                            Catch ex As IndexOutOfRangeException
                                InsertColumns(GetColumn(columnNo), 1)
                                sheet.Cells(TITLE_ROW, GetColumn(columnNo)).Value = subject.TitleName(columnNo)
                                sheet.Cells(TITLE_ROW_DAI, GetColumn(columnNo)).Value = subject.TitleNameDai(columnNo)
                                sheet.Cells(TITLE_ROW_CHU, GetColumn(columnNo)).Value = subject.TitleNameChu(columnNo)
                            End Try
                        Next
                    End If
                Else
                    Dim row As Integer = titleRows + Convert.ToInt32(rowNo)
                    '' 種別・号車
                    If subject.ShisakuSyubetu(rowNo) Is Nothing OrElse StringUtil.IsEmpty(subject.ShisakuSyubetu(rowNo).Trim) Then
                        sheet.Cells(row, sheet.Columns(TAG_SHUBETSU).Index).Value = Nothing
                    Else
                        sheet.Cells(row, sheet.Columns(TAG_SHUBETSU).Index).Value = subject.ShisakuSyubetu(rowNo)
                    End If
                    sheet.Cells(row, sheet.Columns(TAG_GOSHA).Index).Value = subject.ShisakuGousya(rowNo)
                    '行で対応すればロックははずせる'
                    If Not StringUtil.IsEmpty(subject.ShisakuGousya(rowNo)) Then
                        'sheet.Rows(row).Locked = False
                        sheet.Rows(row).Locked = True
                    End If

                    '' 適用欄

                    For Each columnNo As Integer In subject.GetInputColumnNos(rowNo)
                        Try
                            '2012/02/06 全角入力エリアに半角が入ってしまった場合、スプレッドに
                            '表示されない現象を防ぐための対応。char(2)属性の列の為、TRIMをかけて空白を削る
                            sheet.Cells(row, GetColumn(columnNo)).Value = Trim(subject.ShisakuTekiyou(rowNo, columnNo))
                        Catch ex As IndexOutOfRangeException

                            Dim insertColumn As Integer = GetColumn(columnNo) - sheet.ColumnCount + 1

                            InsertColumns(sheet.ColumnCount - 1, insertColumn + 1)
                            'sheet.Cells(row, GetColumn(columnNo)).Value = subject.ShisakuTekiyou(rowNo, columnNo)
                            If sheet.ColumnCount >= columnNo - 1 Then
                                sheet.AddColumns(columnNo, 1)
                            End If

                            sheet.Cells(row, GetColumn(columnNo)).Locked = False
                        End Try
                    Next

                    OnRowLock(row)
                End If
            End If
        End Sub

        Private backIsViewer As Boolean?
        Private Sub LockSheetIfViewerChange()

            If Not subject.IsViewerMode.Equals(backIsViewer) Then
                backIsViewer = subject.IsViewerMode
                If subject.IsViewerMode Then
                    LockAllRowsByRule(True)
                    sheet.Rows(TITLE_ROW).Locked = True
                    sheet.Rows(TITLE_ROW_DAI).Locked = True
                    sheet.Rows(TITLE_ROW_CHU).Locked = True
                    SpreadUtil.LockAllColumns(sheet)
                Else
                    SpreadUtil.UnlockAllColumns(sheet)
                    InitializeColumnsLock()
                    LockAllRowsByRule(False)
                End If
            End If
        End Sub

        Private Sub InitializeColumnsLock()

            '' タイトル行のロック初期設定
            For columnNo As Integer = 0 To LOCKED_TITLE_COLUMN_COUNT - 1
                sheet.Cells(TITLE_ROW, columnNo).Locked = True
                sheet.Cells(TITLE_ROW_DAI, columnNo).Locked = True
                sheet.Cells(TITLE_ROW_CHU, columnNo).Locked = True
            Next
            sheet.Rows(TITLE_ROW).Locked = False
            sheet.Rows(TITLE_ROW_DAI).Locked = False
            sheet.Rows(TITLE_ROW_CHU).Locked = False

            '' 入力欄のロック初期設定
            sheet.Columns(TAG_GOSHA).Locked = subject.IsSekkeiTenkaiIkou
            '2012/03/07 号車と種別はデフォルトで入力禁止にする'
            sheet.Columns(TAG_GOSHA).Locked = True
            sheet.Columns(TAG_SHUBETSU).Locked = True
            For Each columnTag As String In DEFAULT_LOCK_TAGS
                sheet.Columns(columnTag).Locked = True
            Next

            For columnNo As Integer = LOCK_CONTROL_START_COLUMN_NO To sheet.ColumnCount - 1
                sheet.Columns(columnNo).Locked = True
            Next

        End Sub

        Private Sub LockAllRowsByRule(ByVal isLocked As Boolean)

            For Each rowNo As Integer In subject.GetInputRowNos
                Dim row As Integer = titleRows + rowNo
                LockRowByRule(row, isLocked)
            Next
        End Sub

        Private Function IsTitleRow(ByVal rowNo As Integer) As Boolean
            Return rowNo < 0
        End Function

        Private Function GetColumnNo(ByVal column As Integer) As Integer
            Return column - LOCKED_TITLE_COLUMN_COUNT
        End Function

        Private Function GetColumn(ByVal columnNo As Integer) As Integer
            Return columnNo + LOCKED_TITLE_COLUMN_COUNT
        End Function

        Private Sub LockRowByRule(ByVal row As Integer, ByVal IsLocked As Boolean)
            '2012/03/07 種別は常に使えないようにする'
            'For Each columnTag As String In UNLOCKABLE_TAGS
            '    sheet.Cells(row, sheet.Columns(columnTag).Index).Locked = IsLocked
            'Next
            For column As Integer = LOCK_CONTROL_START_COLUMN_NO To sheet.Columns.Count - 1
                sheet.Cells(row, column).Locked = IsLocked
                '号車をデリートした場合、全項目削除する。※コピー、EXCEL取込時は以下のイベントを無効にする。
                'If IsLocked = True Then
                '    sheet.Cells(row, column).Value = Nothing
                'End If
                If sheet.Cells(row, sheet.Columns(TAG_GOSHA).Index).Value = Nothing Then
                    sheet.Cells(row, column).Value = Nothing
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
        End Sub

        Public Sub OnChange(ByVal row As Integer, ByVal column As Integer) Implements Frm9SpdObserver.OnChange

            Dim rowNo As Integer = row - titleRows
            Dim value As String = Convert.ToString(sheet.Cells(row, column).Value)
            Select Case Convert.ToString(sheet.Columns(column).Tag)
                Case TAG_SHUBETSU
                    subject.ShisakuSyubetu(rowNo) = value

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

                Case Else
                    If IsTitleRow(rowNo) Then
                        Select Case row
                            Case TITLE_ROW
                                subject.TitleName(GetColumnNo(column)) = value
                            Case TITLE_ROW_DAI
                                subject.TitleNameDai(GetColumnNo(column)) = value
                            Case TITLE_ROW_CHU
                                subject.TitleNameChu(GetColumnNo(column)) = value
                            Case Else
                                subject.TitleName(GetColumnNo(column)) = value
                        End Select
                    Else
                        subject.ShisakuTekiyou(rowNo, GetColumnNo(column)) = value
                    End If
            End Select
            subject.NotifyObservers(rowNo)
        End Sub





















        ''' <summary>
        ''' Spread列indexを、(Subject)列indexにして返す
        ''' </summary>
        ''' <param name="spreadColumn">Spread列index</param>
        ''' <returns>(Subject)列index</returns>
        ''' <remarks></remarks>
        Private Function ConvSpreadColumn(ByVal spreadColumn As Integer) As Integer
            Return spreadColumn - GetStartSpreadColumn()
        End Function

        ''' <summary>
        ''' 列が始まるSpread列indexを返す
        ''' </summary>
        ''' <returns>Spread列index</returns>
        ''' <remarks></remarks>
        Private Function GetStartSpreadColumn() As Integer

            Return LOCK_CONTROL_START_COLUMN_NO
        End Function



        ''' <summary>
        ''' 空列を挿入する
        ''' </summary>
        ''' <param name="spreadColumn">Spread列index</param>
        ''' <param name="insertCount">挿入行数</param>
        ''' <remarks></remarks>
        Public Sub InsertColumns(ByVal spreadColumn As Integer, ByVal insertCount As Integer)
            If insertCount = 0 Then
                Return
            End If

            Dim subjectColumn As Integer = ConvSpreadColumn(spreadColumn)

            'タイトル列は挿入させない'
            If subjectColumn < 0 Then
                Return
            End If

            aOptionColumns.Insert(subjectColumn, insertCount)
            subject.InsertColumnInMemo(subjectColumn, insertCount)

            For index As Integer = 2 To sheet.RowCount - 1
                If Not StringUtil.IsEmpty(sheet.Cells(index, sheet.Columns(TAG_GOSHA).Index).Value) Then
                    sheet.Cells(index, spreadColumn, index, spreadColumn).Locked = False
                Else
                    sheet.Cells(index, spreadColumn, index, spreadColumn).Locked = True
                End If
            Next
            'columnCountを増やす'
            columnCount = columnCount + insertCount



            'IsSuspend_SpreadChangeEvent = True
            'Try
            '    If IsMemoSpreadColumn(spreadColumn) Then
            '        Dim subjectColumn As Integer = ConvSpreadColumnToAlMemo(spreadColumn)
            '        aMemoColumns.Insert(subjectColumn, insertCount)
            '        subject.InsertColumnInMemo(subjectColumn, insertCount)
            '    ElseIf IsInstlSpreadColumn(spreadColumn) Then
            '        Dim subjectColumn As Integer = ConvSpreadColumnToAlInstl(spreadColumn)
            '        anInstlColumns.Insert(subjectColumn, insertCount)
            '        subject.InsertColumnInInstl(subjectColumn, insertCount)
            '    Else
            '        Throw New NotSupportedException("メモ欄・INSTL品番欄以外の列挿入は不可")
            '    End If
            '    subject.NotifyObservers()
            'Finally
            '    IsSuspend_SpreadChangeEvent = False
            'End Try
        End Sub

        Private Delegate Function GetStartColumn() As Integer
        Private Const OPTION_ROW_INDEX As Integer = 1
        Private Const OPTION_MIDASHI_ROW_INDEX As Integer = 0
        ''' <summary>
        ''' メモ列の挿入・初期化
        ''' </summary>
        ''' <remarks>
        ''' MemoColumnCountを隠蔽する<br/>
        ''' けれど「CellTypeは行に設定済み」とか、結構依存している. 上手に切り分けが出来ていない.
        ''' </remarks>
        Private Class OptionColumns
            'とりあえず1000'
            Public Const MAX_COLUMN_COUNT As Integer = 1000
            Private ReadOnly sheet As SheetView
            Private ReadOnly borderFactory As SpreadBorderFactory
            Private ReadOnly startColumnDelegate As GetStartColumn
            Private _count As Integer = 0

            Public ReadOnly Property Count() As Integer
                Get
                    Return _count
                End Get
            End Property

            Public Sub New(ByVal sheet As SheetView, ByVal borderFactory As SpreadBorderFactory) ', ByVal startColumnDelegate As GetStartColumn)
                Me.sheet = sheet
                Me.borderFactory = borderFactory
                'Me.startColumnDelegate = startColumnDelegate
            End Sub

            ''' <summary>
            ''' 列を初期設定する
            ''' </summary>
            ''' <param name="columnCount">メモ列の初期列数</param>
            ''' <remarks></remarks>
            Public Sub Initialize(ByVal columnCount As Integer)
                Insert(0, columnCount)
            End Sub
            ''' <summary>
            ''' 列を追加する
            ''' </summary>
            ''' <param name="columnCount">追加列数</param>
            ''' <remarks></remarks>
            Public Sub Add(ByVal columnCount As Integer)
                Insert(_count, columnCount)
            End Sub
            ''' <summary>
            ''' 列を挿入する
            ''' </summary>
            ''' <param name="baseColumnIndex">基点となるメモ列index (0 start)</param>
            ''' <param name="columnCount">挿入列数</param>
            ''' <remarks></remarks>
            Public Sub Insert(ByVal baseColumnIndex As Integer, ByVal columnCount As Integer)
                UpdateColumn(baseColumnIndex, columnCount, False)
            End Sub
            ''' <summary>
            ''' 列を削除する
            ''' </summary>
            ''' <param name="baseColumnIndex">基点となるメモ列index (0 start)</param>
            ''' <param name="columnCount">削除列数</param>
            ''' <remarks></remarks>
            Public Sub Remove(ByVal baseColumnIndex As Integer, ByVal columnCount As Integer)
                UpdateColumn(baseColumnIndex, columnCount, True)
            End Sub

            ''' <summary>
            ''' 列の挿入・削除を行う
            ''' </summary>
            ''' <param name="baseColumnIndex">基点となるメモ列index (0 start)</param>
            ''' <param name="columnCount">挿入列数or削除列数</param>
            ''' <param name="isRemove">削除の場合、true</param>
            ''' <remarks></remarks>
            Private Sub UpdateColumn(ByVal baseColumnIndex As Integer, ByVal columnCount As Integer, ByVal isRemove As Boolean)

                Dim startColumnIndex As Integer = LOCK_CONTROL_START_COLUMN_NO
                Dim spreadStartColumn As Integer = startColumnIndex + baseColumnIndex
                'Dim spreadStartColumn As Integer = startColumnIndex + baseColumnIndex



                If Me._count < baseColumnIndex Then
                    'Throw New ArgumentOutOfRangeException("baseColumnIndex", baseColumnIndex, Me._count & " より大きい値は、指定出来ません")
                    Dim a As Integer = 0
                End If

                Dim oldColumnCount As Integer = Me._count

                If isRemove Then
                    columnCount = Math.Min(oldColumnCount, baseColumnIndex + columnCount) - baseColumnIndex
                    RemoveColumns(spreadStartColumn, columnCount)
                Else
                    columnCount = Math.Min(oldColumnCount + columnCount, MAX_COLUMN_COUNT) - oldColumnCount
                    If columnCount = 0 Then
                        Return  ' 列は100列までにとりあえず設定
                    End If

                    If 0 < oldColumnCount Then
                        ' 一番右の列の二重線をクリア
                        'SettingMostRightColumnBorder(startColumnIndex + oldColumnCount - 1, True)
                    End If
                    InsertColumns(spreadStartColumn, columnCount)

                    ' ** 挿入した列に新規設定 **
                    For columnIndex As Integer = 0 To columnCount - 1
                        Dim spreadColumn As Integer = spreadStartColumn + columnIndex
                        ' 列
                        With sheet.Columns(spreadColumn)
                            .CellType = GetMemoColumnCellType()
                            .Width = 20.0!
                            .HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
                        End With

                        '2015/06/11 大区分、中区分を結合する意味がわからないため廃止
                        'With sheet.Cells(OPTION_MIDASHI_ROW_INDEX, spreadColumn)
                        '    '.RowSpan = 2
                        'End With


                        ' セル（メモ）
                        'With sheet.Cells(OPTION_ROW_INDEX, spreadColumn)
                        '    .CellType = GetMemoTitleCellType()
                        '    .VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
                        'End With
                    Next
                    '削除されても消えないように線を引く'
                    sheet.Rows(OPTION_ROW_INDEX).Border = borderFactory.GetUnderLine()

                    '2015/06/11 大区分、中区分を結合を廃止したため追加
                    sheet.Rows(OPTION_ROW_INDEX - 1).Border = borderFactory.GetUnderLine()

                End If

                Dim scaleColumnCount As Integer = IIf(isRemove, -columnCount, columnCount)

                ' 一番右の列だけ、右側に二重線
                'SettingMostRightColumnBorder(startColumnIndex + oldColumnCount + scaleColumnCount - 1, False)

                ' ** メモ欄のタイトル **
                'With sheet.Cells(OPTION_MIDASHI_ROW_INDEX, startColumnIndex)
                '    ' .CellTypeは行に設定済み
                '    .Border = borderFactory.GetUnderLineAndRightWLine
                '    .ColumnSpan = oldColumnCount + scaleColumnCount
                '    .Value = ""
                'End With

                Me._count += scaleColumnCount
            End Sub

            Private Sub SettingMostRightColumnBorder(ByVal mostRightColumn As Integer, ByVal isClear As Boolean)
                If isClear Then
                    With sheet.Columns(mostRightColumn)
                        .Border = Nothing
                    End With
                    With sheet.Cells(OPTION_ROW_INDEX, mostRightColumn)
                        .Border = borderFactory.GetUnderLine
                    End With
                Else
                    With sheet.Columns(mostRightColumn)
                        .Border = borderFactory.GetRightWLine
                    End With
                    With sheet.Cells(OPTION_ROW_INDEX, mostRightColumn)
                        .Border = borderFactory.GetUnderLineAndRightWLine
                    End With
                End If
            End Sub

            Private MemoTitleCellType As TextCellType
            ''' <summary>
            ''' INSTL品番セルを返す
            ''' </summary>
            ''' <returns>INSTL品番セル</returns>
            ''' <remarks></remarks>
            Public Function GetMemoTitleCellType() As TextCellType
                If MemoTitleCellType Is Nothing Then
                    MemoTitleCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    MemoTitleCellType.MaxLength = 122
                    MemoTitleCellType.TextOrientation = FarPoint.Win.TextOrientation.TextTopDown
                    MemoTitleCellType.CharacterSet = CharacterSet.AllIME
                End If
                Return MemoTitleCellType
            End Function
            Private MemoColumnCellType As TextCellType
            ''' <summary>
            ''' メモ列CellTypeを返す
            ''' </summary>
            ''' <returns>メモ列CellType</returns>
            ''' <remarks></remarks>
            Private Function GetMemoColumnCellType() As TextCellType
                If MemoColumnCellType Is Nothing Then

                    MemoColumnCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    MemoColumnCellType.MaxLength = 2
                    MemoColumnCellType.CharacterSet = CharacterSet.AllIME
                End If
                Return MemoColumnCellType
            End Function

            Private Sub RemoveColumns(ByVal baseColumnNo As Integer, ByVal removeCount As Integer)
                If removeCount = 0 Then
                    Return
                End If
                sheet.RemoveColumns(baseColumnNo, removeCount)
            End Sub
            Private Sub InsertColumns(ByVal baseColumnNo As Integer, ByVal insertCount As Integer)
                If insertCount = 0 Then
                    Return
                End If
                sheet.AddColumns(baseColumnNo, insertCount)
            End Sub
        End Class

        ''' <summary>
        ''' 列を削除する
        ''' </summary>
        ''' <param name="spreadColumn">Spread列index</param>
        ''' <param name="removeCount">削除行数</param>
        ''' <remarks></remarks>
        Public Sub RemoveColumns(ByVal spreadColumn As Integer, ByVal removeCount As Integer)
            If removeCount = 0 Then
                Return
            End If

            Dim subjectColumn As Integer = ConvSpreadColumn(spreadColumn)
            If subjectColumn < 0 Then
                Return
            End If

            aOptionColumns.Remove(subjectColumn, removeCount)
            subject.RemoveColumnInMemo(subjectColumn, removeCount)

            columnCount = columnCount - removeCount

            'IsSuspend_SpreadChangeEvent = True
            'Try
            '    If IsMemoSpreadColumn(spreadColumn) Then
            '        Dim subjectColumn As Integer = ConvSpreadColumnToAlMemo(spreadColumn)
            '        aOptionColumns.Remove(subjectColumn, removeCount)
            '        subject.RemoveColumnInMemo(subjectColumn, removeCount)
            '    ElseIf IsInstlSpreadColumn(spreadColumn) Then
            '        Dim subjectColumn As Integer = ConvSpreadColumnToAlInstl(spreadColumn)
            '        anInstlColumns.Remove(subjectColumn, removeCount)
            '        subject.RemoveColumnInInstl(subjectColumn, removeCount)
            '    Else
            '        Throw New NotSupportedException("メモ欄・INSTL品番欄以外の列削除は不可")
            '    End If
            '    subject.NotifyObservers()
            'Finally
            '    IsSuspend_SpreadChangeEvent = False
            'End Try
        End Sub

    End Class
End Namespace