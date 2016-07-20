Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports FarPoint.Win.Spread.CellType
Imports EventSakusei.ShisakuBuhinEdit.Ikkatsu.Logic
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util
Imports EventSakusei.ShisakuBuhinEdit.Ui
Imports ShisakuCommon.Ui.Spd
''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END

Namespace ShisakuBuhinEdit.Ikkatsu
    ''' <summary>
    ''' 部品構成表示画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm48DispShisakuBuhinEditIkkatsu : Implements Observer

#Region "TAG"
        Private Const TAG_INSTL_HINBAN As String = "INSTL_HINBAN"
        Private Const TAG_INSTL_HINBAN_KBN As String = "INSTL_HINBAN_KBN"
        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(5)) (TES)張 ADD BEGIN
        Private Const TAG_INSTL_DATA_KBN As String = "INSTL_DATA_KBN"
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(5)) (TES)張 ADD END

        Private Const TAG_BASE_HINBAN As String = "BASE_HINBAN"
        Private Const TAG_BASE_HINBAN_KBN As String = "BASE_HINBAN_KBN"
        '2012/01/25
        Private Const TAG_YOBIDASHI_MOTO As String = "TAG_YOBIDASHI_MOTO"
#End Region
#Region "各列のCellType"
        Private baseHinbanCellType As TextCellType
        Private baseHinbanKbnCellType As ComboBoxCellType
        Private yobidashiMotoCellType As ComboBoxCellType
        ''' <summary>
        ''' 「ベースとなる品番」セルを返す
        ''' </summary>
        ''' <returns>「ベースとなる品番」セル</returns>
        ''' <remarks></remarks>
        Public Function GetBaseHinbanCellType() As TextCellType
            If baseHinbanCellType Is Nothing Then
                baseHinbanCellType = BuhinEditSpreadUtil.NewGeneralTextCellType()
                baseHinbanCellType.MaxLength = 13
            End If
            Return baseHinbanCellType
        End Function
        ''' <summary>
        ''' 「ベースとなる品番区分」セルを返す
        ''' </summary>
        ''' <returns>「ベースとなる品番区分」セル</returns>
        ''' <remarks></remarks>
        Public Function GetBaseHinbanKbnCellType() As ComboBoxCellType
            If baseHinbanKbnCellType Is Nothing Then
                baseHinbanKbnCellType = NewBaseHinbanKbnCellType(Nothing)
            End If
            Return baseHinbanKbnCellType
        End Function
        Private Function NewBaseHinbanKbnCellType(ByVal rowIndex As Integer?) As ComboBoxCellType
            Dim result As ComboBoxCellType = BuhinEditSpreadUtil.NewGeneralComboBoxCellType()
            result.MaxLength = 5
            If IsNumeric(rowIndex) Then
                SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.BaseHinbanKbnLabelValues(rowIndex), False)
            End If
            Return result
        End Function
        ''' <summary>
        ''' 「呼出し元」セルを返す
        ''' </summary>
        ''' <returns>「呼出し元」セル</returns>
        ''' <remarks></remarks>
        Public Function GetYobhidashiMotoCellType() As ComboBoxCellType
            If yobidashiMotoCellType Is Nothing Then
                yobidashiMotoCellType = NewYobidashiMotoCellType(Nothing)
            End If
            Return yobidashiMotoCellType
        End Function
        Private Function NewYobidashiMotoCellType(ByVal rowIndex As Integer?) As ComboBoxCellType
            Dim result As ComboBoxCellType = BuhinEditSpreadUtil.NewGeneralComboBoxCellType()
            'result.MaxLength = 5
            If IsNumeric(rowIndex) Then
                SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.YobidashiMotoLabelValues(rowIndex), False)
                '2012/02/08 コンボボックスを編集不可に
                result.Editable = False
            End If
            Return result

        End Function

#End Region

        Private Shared ReadOnly UNLOCKABLE_TAGS As String() = {TAG_BASE_HINBAN, TAG_BASE_HINBAN_KBN, TAG_YOBIDASHI_MOTO}

        Private _resultOk As Boolean
        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(6)) (TES)張 ADD BEGIN
        Private strGetValue As String() = {"10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25"}
        Private strSetValue As String() = {"MA", "MB", "MC", "MD", "ME", "MF", "MG", "MH", "MI", "MJ", "MK", "ML", "MM", "MN", "MO", "MP"}
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(6)) (TES)張 ADD END
        Private _validatedHinbanIndexes As New List(Of Integer)

        Private ReadOnly subject As BuhinEditIkkatsuSubject
        Private ReadOnly instlRowCount As Integer
        ''↓↓2014/09/08 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥ 酒井 ADD BEGIN 
        Private shisakuEventCode As String
        ''↑↑2014/09/08 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥ 酒井 ADD END 

        Public Sub New(ByVal subject As BuhinEditIkkatsuSubject, ByVal instlRowCount As Integer, Optional ByVal shisakuEventCode As String = "")
            ''↓↓2014/09/08 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥ 酒井 ADD BEGIN 
            'Public Sub New(ByVal subject As BuhinEditIkkatsuSubject, ByVal instlRowCount As Integer)
            Me.shisakuEventCode = shisakuEventCode
            ''↑↑2014/09/08 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥ 酒井 ADD END 

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.setTitleVersion(Me)

            Me.subject = subject
            Me.subject.AddObserver(Me)

            Me.instlRowCount = instlRowCount

            ' TODO 部品構成表示ツールは11月末
            btnHyoujiKouseiTool.Enabled = False

            InitializeSpread()

            Me.subject.NotifyObservers()
        End Sub

        Private Sub InitializeSpread()

            ' RowModeだと、Delキーが効かない. Delキーの設定前に Normalにすれば効く
            spdParts_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.Normal

            BuhinEditSpreadUtil.InitializeFrm41(spdParts)

            spdParts_Sheet1.RowCount = instlRowCount

            Dim index As Integer = 0

            ''↓↓2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(4)) 酒井 ADD BEGIN
            spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_INSTL_DATA_KBN
            ''↑↑2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(4)) 酒井 ADD END
            spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_INSTL_HINBAN
            spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_INSTL_HINBAN_KBN
            ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(4)) (TES)張 ADD BEGIN
            ''↓↓2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(4)) 酒井 DEL BEGIN
            'spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_INSTL_DATA_KBN
            ''↑↑2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(4)) 酒井 DEL END
            ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(4)) (TES)張 ADD END

            spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_BASE_HINBAN
            spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_BASE_HINBAN_KBN
            '2012/01/25
            spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOBIDASHI_MOTO

            ' *** 列に設定 ***
            SpreadUtil.LockAllColumns(spdParts_Sheet1)
            spdParts_Sheet1.Columns(TAG_BASE_HINBAN).CellType = GetBaseHinbanCellType()
            spdParts_Sheet1.Columns(TAG_BASE_HINBAN_KBN).CellType = GetBaseHinbanKbnCellType()
            spdParts_Sheet1.Columns(TAG_YOBIDASHI_MOTO).CellType = GetYobhidashiMotoCellType()

            'AddHandler spread.VisibleChanged, AddressOf Spread_VisibleChangedEventHandlable
            '' 通常の Spread_Changed()では、CTRL+V/CTRL+ZでChengedイベントが発生しない
            ''（編集モードではない状態で変更された場合は発生しない仕様とのこと。）
            '' CTRL+V/CTRL+Zでもイベントが発生するハンドラを設定する
            SpreadUtil.AddHandlerSheetDataModelChanged(spdParts_Sheet1, AddressOf Spread_ChangeEventHandlable)
        End Sub

#Region "結果"
        ''' <summary>
        ''' 「表示」押下かを返す
        ''' </summary>
        Public ReadOnly Property ResultOk() As Boolean
            Get
                Return _resultOk
            End Get
        End Property

        Public ReadOnly Property Results() As IndexedList(Of StructureResult)
            Get
                Return subject.GetValidatedStructureResult
            End Get
        End Property
#End Region

        Private Sub ClearSheet()
            spdParts_Sheet1.ClearRange(0, 0, spdParts_Sheet1.GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data), spdParts_Sheet1.GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data), True)
        End Sub

        Public Sub UpdateObserver(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update
            If arg Is Nothing Then
                ClearSheet()
                For Each rowIndex As Integer In subject.GetRowIndexes
                    UpdateObserver(observable, rowIndex)
                Next
            ElseIf IsNumeric(arg) Then
                Dim rowIndex As Integer = CInt(arg)
                spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_INSTL_HINBAN).Index).Value = subject.InstlHinban(rowIndex)
                spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_INSTL_HINBAN_KBN).Index).Value = subject.InstlHinbanKbn(rowIndex)
                'spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_INSTL_DATA_KBN).Index).Value = subject.InstlDataKbn(rowIndex)
                ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(6)) (TES)張 ADD BEGIN 
                ''↓↓2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(6)) 酒井 ADD BEGIN 
                'If subject.InstlHinbanKbn(rowIndex) = "0" Then
                If StringUtil.IsEmpty(subject.InstlDataKbn(rowIndex)) Or subject.InstlDataKbn(rowIndex) = "0" Then
                    ''↑↑2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(6)) 酒井 ADD END 
                    spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_INSTL_DATA_KBN).Index).Value = ""
                    ''↓↓2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(6)) 酒井 ADD BEGIN 
                    'ElseIf subject.InstlHinbanKbn(rowIndex) >= 10 AndAlso subject.InstlHinbanKbn(rowIndex) <= 25 Then
                    ''↓↓2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(6)) 酒井 ADD BEGIN 
                    ''↓↓2014/09/08 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(6)) 酒井 ADD BEGIN 
                    'ElseIf CInt(subject.InstlHinbanKbn(rowIndex)) >= 10 AndAlso CInt(subject.InstlHinbanKbn(rowIndex)) <= 25 Then
                ElseIf CInt(subject.InstlDataKbn(rowIndex)) >= 10 AndAlso CInt(subject.InstlDataKbn(rowIndex)) <= 25 Then
                    ''↑↑2014/09/08 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(6)) 酒井 ADD END 
                    ''↑↑2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(6)) 酒井 ADD END 
                    For index = 0 To strGetValue.Length - 1
                        If strGetValue(index).Equals(subject.BaseHinban(rowIndex)) Then
                            spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_INSTL_DATA_KBN).Index).Value = strSetValue(index)
                        End If
                    Next
                Else
                    ''↓↓2014/09/08 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(6)) 酒井 ADD BEGIN
                    'spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_INSTL_DATA_KBN).Index).Value = "M" & subject.InstlHinbanKbn(rowIndex)
                    spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_INSTL_DATA_KBN).Index).Value = "M" & subject.InstlDataKbn(rowIndex)
                    ''↑↑2014/09/08 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(6)) 酒井 ADD END
                End If
                ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(6)) (TES)張 ADD END

                spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_BASE_HINBAN).Index).Value = subject.BaseHinban(rowIndex)

                spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_BASE_HINBAN_KBN).Index).Value = subject.BaseHinbanKbn(rowIndex)
                SpreadUtil.BindCellTypeToCell(spdParts_Sheet1, rowIndex, TAG_BASE_HINBAN_KBN, NewBaseHinbanKbnCellType(rowIndex))
                '2012/01/25
                spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_YOBIDASHI_MOTO).Index).Value = subject.YobidashiMoto(rowIndex)
                SpreadUtil.BindCellTypeToCell(spdParts_Sheet1, rowIndex, TAG_YOBIDASHI_MOTO, NewYobidashiMotoCellType(rowIndex))

                OnRowLock(rowIndex)

                ''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                '該当イベント取得
                Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
                Dim eventVo As TShisakuEventVo
                eventVo = eventDao.FindByPk(shisakuEventCode)

                If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                    For i As Integer = 0 To instlRowCount - 1
                        '↓↓2014/10/20 酒井 ADD BEGIN
                        'If StringUtil.IsNotEmpty(subject.InstlDataKbn(i)) Then
                        'If CInt(subject.InstlDataKbn(i)) > 0 Then
                        If StringUtil.IsNotEmpty(subject.BaseInstlFlg(i)) Then
                            If CInt(subject.BaseInstlFlg(i)) > 0 Then
                                '↑↑2014/10/20 酒井 ADD END
                                spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns(TAG_INSTL_DATA_KBN).Index).Locked = True
                                spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns(TAG_INSTL_DATA_KBN).Index).BackColor = Color.DarkGray
                                spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns(TAG_INSTL_HINBAN).Index).Locked = True
                                spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns(TAG_INSTL_HINBAN).Index).BackColor = Color.DarkGray
                                spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns(TAG_INSTL_HINBAN_KBN).Index).Locked = True
                                spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns(TAG_INSTL_HINBAN_KBN).Index).BackColor = Color.DarkGray
                                spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns(TAG_BASE_HINBAN).Index).Locked = True
                                spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns(TAG_BASE_HINBAN).Index).BackColor = Color.DarkGray
                                spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns(TAG_BASE_HINBAN_KBN).Index).Locked = True
                                spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns(TAG_BASE_HINBAN_KBN).Index).BackColor = Color.DarkGray
                                spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns(TAG_YOBIDASHI_MOTO).Index).Locked = True
                                spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns(TAG_YOBIDASHI_MOTO).Index).BackColor = Color.DarkGray
                            End If
                        End If
                    Next
                End If
                ''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END

            End If
        End Sub

        ''' <summary>
        ''' Spreadの変更イベントハンドラ
        ''' </summary>
        ''' <param name="sender">イベントハンドラに従う</param>
        ''' <param name="e">イベントハンドラに従う</param>
        ''' <remarks></remarks>
        Private Sub Spread_ChangeEventHandlable(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs)
            OnChange(e.Row, e.Column)
        End Sub

        ''' <summary>
        ''' 入力データ変更時の処理
        ''' </summary>
        ''' <param name="row">Spread行index</param>
        ''' <param name="column">spread列index</param>
        ''' <remarks></remarks>
        Private Sub OnChange(ByVal row As Integer, ByVal column As Integer)
            Dim value As Object = spdParts_Sheet1.Cells(row, column).Value
            Select Case Convert.ToString(spdParts_Sheet1.Columns(column).Tag)
                Case TAG_BASE_HINBAN
                    subject.BaseHinban(row) = spdParts_Sheet1.Cells(row, column).Value
                Case TAG_BASE_HINBAN_KBN
                    subject.BaseHinbanKbn(row) = spdParts_Sheet1.Cells(row, column).Value
                Case TAG_YOBIDASHI_MOTO
                    subject.YobidashiMoto(row) = spdParts_Sheet1.Cells(row, column).Value
            End Select
            subject.NotifyObservers(row)
        End Sub

        Private Sub LockRowByRule(ByVal row As Integer, ByVal IsLocked As Boolean)
            For Each Tag As String In UNLOCKABLE_TAGS
                spdParts_Sheet1.Cells(row, spdParts_Sheet1.Columns(Tag).Index).Locked = IsLocked
            Next
        End Sub
        Private Sub OnRowLock(ByVal row As Integer)
            If subject.IsEditModes(row) Then
                LockRowByRule(row, False)
            Else
                LockRowByRule(row, True)
            End If
        End Sub

        Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
            Me.Close()
        End Sub

        Private Sub btnDISP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDISP.Click
            For i As Integer = 0 To spdParts_Sheet1.RowCount - 1
                If Not StringUtil.IsEmpty(spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns(TAG_BASE_HINBAN).Index).Value) And StringUtil.IsEmpty(spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns(TAG_YOBIDASHI_MOTO).Index).Value) Then
                    MsgBox("抽出元を選択してください。")
                    Return
                End If
            Next
            _resultOk = True
            Me.Close()
        End Sub

        Private Sub Frm48DispShisakuBuhinEditIkkatsu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            ' 同一インスタンスを再表示する事があるので、その時の初期化
            _resultOk = False
        End Sub

    End Class
End Namespace