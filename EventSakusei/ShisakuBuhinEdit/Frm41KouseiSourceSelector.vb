Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
Imports ShisakuCommon
Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util
Imports EventSakusei.ShisakuBuhinEdit.SourceSelector.Logic
Imports FarPoint.Win.Spread.CellType
Imports ShisakuCommon.Ui.Spd
Imports EventSakusei.ShisakuBuhinEdit.Ui

''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END





Public Class Frm41KouseiSourceSelector : Implements Observer
    Private ReadOnly subject As SourceSelectorSubject
    Private _resultOk As Boolean
    '↓↓2014/10/23 酒井 ADD BEGIN
    Private _Sabun As Boolean
    '↑↑2014/10/23 酒井 ADD END
    Private instlRowCount As Integer
    Private sw As Integer   '0:構成再展開　1:最新化
    ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(7)) (TES)張 ADD BEGIN
    Private strGetValue As String() = {"10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25"}
    Private strSetValue As String() = {"MA", "MB", "MC", "MD", "ME", "MF", "MG", "MH", "MI", "MJ", "MK", "ML", "MM", "MN", "MO", "MP"}
    ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(7)) (TES)張 ADD END
    '↓↓2014/10/22 酒井 ADD BEGIN
    Private shisakuEventCode As String
    '↑↑2014/10/22 酒井 ADD END
    Private Shared ReadOnly UNLOCKABLE_TAGS As String() = {TAG_YOBIDASHI_MOTO}



#Region "TAG"
    Private Const TAG_DUMMY As String = "DUMMY"
    Private Const TAG_BASE_HINBAN As String = "BASE_HINBAN"
    Private Const TAG_BASE_HINBAN_KBN As String = "BASE_HINBAN_KBN"
    ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(5)) (TES)張 ADD BEGIN
    Private Const TAG_BASE_DATA_KBN As String = "BASE_DATA_KBN"
    ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(5)) (TES)張 ADD END
    Private Const TAG_YOBIDASHI_MOTO As String = "TAG_YOBIDASHI_MOTO"
#End Region
#Region "各列のCellType"
    Private instlHinbanCellType As TextCellType
    Private instlHinbanKbnCellType As TextCellType
    ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(5)) (TES)張 ADD BEGIN
    Private instlDataKbnCellType As TextCellType
    ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(5)) (TES)張 ADD END
    Private yobidashiMotoCellType As ComboBoxCellType


    ''' <summary>
    ''' 「INSTL品番」セルを返す
    ''' </summary>
    ''' <returns>「INSTL品番」セル</returns>
    ''' <remarks></remarks>
    Public Function GetInstlHinbanCellType() As TextCellType
        Return instlHinbanCellType
    End Function
    ''' <summary>
    ''' 「INSTL品番区分」セルを返す
    ''' </summary>
    ''' <returns>「INSTL品番区分」セル</returns>
    ''' <remarks></remarks>
    Public Function GetInstlHinbanKbnCellType() As TextCellType
        Return instlHinbanKbnCellType
    End Function
    '''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(6)) (TES)張 ADD BEGIN
    ''' <summary>
    ''' 「INSTLデータ区分」セルを返す
    ''' </summary>
    ''' <returns>「INSTLデータ区分」セル</returns>
    ''' <remarks></remarks>
    Public Function GetInstlDataKbnCellType() As TextCellType
        Return instlDataKbnCellType
    End Function
    '''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(6)) (TES)張 ADD END
    ''' <summary>
    ''' 呼出し元のセルタイプを返す
    ''' </summary>
    ''' <param name="rowIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NewYobidashiMotoCellType(ByVal rowIndex As Integer) As ComboBoxCellType
        Dim result As ComboBoxCellType = BuhinEditSpreadUtil.NewGeneralComboBoxCellType()
        If IsNumeric(rowIndex) Then
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.YobidashiMotoLabelValues(rowIndex), False)
            '2012/02/08 コンボボックスを編集不可に
            result.Editable = False
        End If
        Return result

    End Function


#End Region



    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sourceSubject"></param>
    ''' <param name="instlRowCount"></param>
    ''' <param name="sw">0:構成再展開　1:最新化</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal sourceSubject As SourceSelectorSubject, ByVal instlRowCount As Integer, ByVal sw As Integer, Optional ByVal shisakuEventCode As String = "")
        '↓↓2014/10/22 酒井 ADD BEGIN
        'Public Sub New(ByVal sourceSubject As SourceSelectorSubject, ByVal instlRowCount As Integer, ByVal sw As Integer)
        '↑↑2014/10/22 酒井 ADD END

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ShisakuFormUtil.Initialize(Me)
        Me.sw = sw
        Me.subject = sourceSubject
        Me.subject.AddObserver(Me)
        '↓↓2014/10/22 酒井 ADD BEGIN
        Me.shisakuEventCode = shisakuEventCode
        '↑↑2014/10/22 酒井 ADD END

        Me.instlRowCount = instlRowCount

        InitializeSpread()

        Me.subject.NotifyObservers()


        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        'ShisakuFormUtil.Initialize(Me)

        'SpdAlObserverの以下のメソッドをパクれば・・・()
        'SetInstlColumnColor()
        '量産EBOMに存在するINSTL品番か確認'
        'For i As Integer = 0 To instlRowCount - 1
        '    Me.OnChange(i, 0)
        'Next
        'Me.subject.NotifyObservers()


        For i As Integer = 0 To instlRowCount - 1
            If spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns(TAG_BASE_HINBAN).Index).Value = "" Then
                spdParts_Sheet1.Rows(i).Visible = False
            End If
        Next


    End Sub

    Private Sub Frm41KouseiSourceSelector_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ShisakuFormUtil.setTitleVersion(Me)
        If sw = 0 Then
            '構成再展開
            Me.lblKakunin2.Text = "構成再展開"
            Me.btnTenkai.Text = "再展開"
        Else
            '最新化
            Me.lblKakunin2.Text = "最新化"
            Me.btnTenkai.Text = "最新化"
        End If

    End Sub
    Private Sub InitializeSpread()

        'subject.re()
        ' RowModeだと、Delキーが効かない. Delキーの設定前に Normalにすれば効く
        spdParts_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.Normal

        BuhinEditSpreadUtil.InitializeFrm41(spdParts)

        spdParts_Sheet1.RowCount = instlRowCount

        Dim index As Integer = 0
        ''↓↓2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(4)) 酒井 ADD BEGIN
        'spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_DUMMY
        spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_BASE_DATA_KBN
        ''↑↑2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(4)) 酒井 ADD END
        spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_BASE_HINBAN
        spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_BASE_HINBAN_KBN
        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(4)) (TES)張 ADD BEGIN
        ''↓↓2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(4)) 酒井 DEL BEGIN
        'spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_BASE_DATA_KBN
        ''↑↑2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(4)) 酒井 DEL END
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(4)) (TES)張 ADD END
        spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOBIDASHI_MOTO

        'spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_INSTL_HINBAN
        'spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_INSTL_HINBAN_KBN
        'spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_BASE_HINBAN
        'spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_BASE_HINBAN_KBN
        ''2012/01/25
        'spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOBIDASHI_MOTO

        '' *** 列に設定 ***
        SpreadUtil.LockAllColumns(spdParts_Sheet1)
        ''↓↓2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(4)) 酒井 DEL BEGIN
        'spdParts_Sheet1.Columns(TAG_DUMMY).Visible = False
        ''↑↑2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(4)) 酒井 DEL END
        spdParts_Sheet1.Columns(TAG_BASE_HINBAN).CellType = GetInstlHinbanCellType()
        spdParts_Sheet1.Columns(TAG_BASE_HINBAN_KBN).CellType = GetInstlHinbanKbnCellType()
        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(4)) (TES)張 ADD BEGIN
        spdParts_Sheet1.Columns(TAG_BASE_DATA_KBN).CellType = GetInstlDataKbnCellType()
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(4)) (TES)張 ADD END
        spdParts_Sheet1.Columns(TAG_YOBIDASHI_MOTO).CellType = GetYobhidashiMotoCellType()

        'spdParts_Sheet1.Columns(TAG_YOBIDASHI_MOTO).CellType = NewYobidashiMotoCellType()

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
    '↓↓2014/10/23 酒井 ADD BEGIN
    Public ReadOnly Property Sabun() As Boolean
        Get
            Return _Sabun
        End Get
    End Property

    '↑↑2014/10/23 酒井 ADD END

    Public ReadOnly Property Results() As IndexedList(Of StructureResult)
        Get
            Return subject.GetValidatedStructureResult
        End Get
    End Property
    ''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
    Public ReadOnly Property Results4Base() As IndexedList(Of StructureResult)
        Get
            Return subject.GetValidatedStructureResult4Base
        End Get
    End Property
    ''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
#End Region

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        '↓↓2014/10/23 酒井 ADD BEGIN
        _Sabun = False
        '↑↑2014/10/23 酒井 ADD END
        Me._resultOk = False
        Me.Close()

    End Sub

    Private Sub btnTenkai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTenkai.Click
        '↓↓2014/10/23 酒井 ADD BEGIN
        _Sabun = False
        '↑↑2014/10/23 酒井 ADD END

        Me._resultOk = True
        Me.Close()
    End Sub
    '↓↓2014/10/23 酒井 ADD BEGIN
    Private Sub btnSabun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSabun.Click
        '↓↓2014/10/23 酒井 ADD BEGIN
        _Sabun = True
        '↑↑2014/10/23 酒井 ADD END
        Me._resultOk = True
        Me.Close()
    End Sub
    '↑↑2014/10/23 酒井 ADD END
    Public Sub UpdateObserver(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update
        If arg Is Nothing Then
            ClearSheet()
            For Each rowIndex As Integer In subject.GetRowIndexes
                UpdateObserver(observable, rowIndex)
            Next
        ElseIf IsNumeric(arg) Then
            Dim rowIndex As Integer = CInt(arg)
            spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_BASE_HINBAN).Index).Value = subject.BaseHinban(rowIndex)
            spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_BASE_HINBAN_KBN).Index).Value = subject.BaseHinbanKbn(rowIndex)
            ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(7)) (TES)張 ADD BEGIN
            ''↓↓2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(7)) 酒井 ADD BEGIN
            'spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_BASE_DATA_KBN).Index).Value = subject.BaseDataKbn(rowIndex)
            'If subject.BaseHinban(rowIndex) = "0" Then
            If StringUtil.IsEmpty(subject.BaseDataKbn(rowIndex)) Or subject.BaseDataKbn(rowIndex) = "0" Then
                ''↑↑2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(7)) 酒井 ADD END
                spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_BASE_DATA_KBN).Index).Value = ""
                ''↓↓2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(7)) 酒井 ADD BEGIN
                'ElseIf subject.BaseHinban(rowIndex) >= 10 AndAlso subject.BaseHinban(rowIndex) <= 25 Then
            ElseIf CInt(subject.BaseDataKbn(rowIndex)) >= 10 AndAlso CInt(subject.BaseDataKbn(rowIndex)) <= 25 Then
                ''↑↑2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(7)) 酒井 ADD END
                For index = 0 To strGetValue.Length - 1
                    If strGetValue(index).Equals(subject.BaseHinban(rowIndex)) Then
                        spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_BASE_DATA_KBN).Index).Value = strSetValue(index)
                    End If
                Next
            Else
                spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_BASE_DATA_KBN).Index).Value = "M" & subject.BaseDataKbn(rowIndex)
            End If
            ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(7)) (TES)張 ADD END

            '2012/01/25
            spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_YOBIDASHI_MOTO).Index).Value = subject.YobidashiMoto(rowIndex)
            SpreadUtil.BindCellTypeToCell(spdParts_Sheet1, rowIndex, TAG_YOBIDASHI_MOTO, NewYobidashiMotoCellType(rowIndex))

            OnRowLock(rowIndex)

            '↓↓2014/10/09 酒井 ADD BEGIN
            '↓↓2014/10/22 酒井 ADD BEGIN
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventvo As TShisakuEventVo = eventDao.FindByPk(Me.shisakuEventCode)
            If eventvo.BlockAlertKind = 2 And eventvo.KounyuShijiFlg = "0" Then
                '↑↑2014/10/22 酒井 ADD END
                If subject.BaseInstlFlg(rowIndex) = "1" Then
                    spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_BASE_HINBAN).Index).Locked = True
                    spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_BASE_HINBAN_KBN).Index).Locked = True
                    spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_BASE_DATA_KBN).Index).Locked = True
                    spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_YOBIDASHI_MOTO).Index).Locked = True

                    spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_BASE_HINBAN).Index).BackColor = Color.DarkGray
                    spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_BASE_HINBAN_KBN).Index).BackColor = Color.DarkGray
                    spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_BASE_DATA_KBN).Index).BackColor = Color.DarkGray
                    spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_YOBIDASHI_MOTO).Index).BackColor = Color.DarkGray

                    spdParts_Sheet1.Rows(rowIndex).Visible = False
                End If
            End If
            '↑↑2014/10/09 酒井 ADD END
            End If

    End Sub
    Private Sub ClearSheet()
        spdParts_Sheet1.ClearRange(0, 0, spdParts_Sheet1.GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data), spdParts_Sheet1.GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data), True)
    End Sub
    Private Sub LockRowByRule(ByVal row As Integer, ByVal IsLocked As Boolean)
        For Each Tag As String In UNLOCKABLE_TAGS
            spdParts_Sheet1.Cells(row, spdParts_Sheet1.Columns(Tag).Index).Locked = IsLocked
        Next
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
                ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(8)) (TES)張 ADD BEGIN
                ''↓↓2014/09/04 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(8)) 酒井 DEL BEGIN
                'Case TAG_BASE_DATA_KBN
                'subject.BaseDataKbn(row) = spdParts_Sheet1.Cells(row, column).Value
                ''↓↓2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(8)) 酒井 ADD BEGIN
                'If spdParts_Sheet1.Cells(row, column).Value = "0" Then
                'If StringUtil.IsEmpty(spdParts_Sheet1.Cells(row, column).Value) Or spdParts_Sheet1.Cells(row, column).Value = "0" Then
                ''↑↑2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(8)) 酒井 ADD END
                '    subject.BaseDataKbn(row) = ""
                ''↓↓2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(8)) 酒井 ADD BEGIN
                'ElseIf spdParts_Sheet1.Cells(row, column).Value >= 10 AndAlso spdParts_Sheet1.Cells(row, column).Value <= 25 Then
                'ElseIf CInt(spdParts_Sheet1.Cells(row, column).Value) >= 10 AndAlso CInt(spdParts_Sheet1.Cells(row, column).Value) <= 25 Then
                ''↑↑2014/09/02 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(8)) 酒井 ADD END
                '    For index = 0 To strGetValue.Length - 1
                '        If strGetValue(index).Equals(spdParts_Sheet1.Cells(row, column).Value) Then
                '            subject.BaseDataKbn(row) = strSetValue(index)
                '        End If
                '    Next
                'Else
                '    subject.BaseDataKbn(row) = "M" & spdParts_Sheet1.Cells(row, column).Value
                'End If
                ''↑↑2014/09/04 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(8)) 酒井 DEL END
                ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(8)) (TES)張 ADD END
            Case TAG_YOBIDASHI_MOTO
                subject.YobidashiMoto(row) = spdParts_Sheet1.Cells(row, column).Value
        End Select
        subject.NotifyObservers(row)
    End Sub
    Private Sub OnRowLock(ByVal row As Integer)
        If subject.IsEditModes(row) Then
            LockRowByRule(row, False)
        Else
            LockRowByRule(row, True)
        End If
    End Sub
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

End Class