'Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
Imports ShisakuCommon
'Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util
'Imports EventSakusei.ShisakuBuhinEdit.SourceSelector.Logic
Imports FarPoint.Win.Spread.CellType
Imports ShisakuCommon.Ui.Spd
Imports EventSakusei.ShisakuBuhinEdit.Ui
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.ShisakuBuhinMenu.Dao
'↓↓2014/10/21 酒井 ADD BEGIN
Imports EventSakusei.ShisakuBuhinMenu.Logic.Impl.Tenkai
'↑↑2014/10/21 酒井 ADD END
Public Class Frm8DispSekkeika

    Private _resultOk As Boolean
    Private subject As New List(Of TShisakuBlockSekkeikaTmpVo)
    Private subjectAlart As New List(Of String)

    Private _ShisakuEventCode As String
#Region "TAG"
    Private Const TAG_BLOCK_NO As String = "BLOCK_NO"
    Private Const TAG_BLOCK_NAME As String = "BLOCK_NAME"
    Private Const TAG_SEKKEIKA As String = "SEKKEIKA"
#End Region

#Region "結果"
    ''' <summary></summary>
    Public ReadOnly Property ResultOk() As Boolean
        Get
            Return _resultOk
        End Get
    End Property
#End Region

    ''' <summary></summary>
    ''' <param name="shisakuEventCode"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal shisakuEventCode As String, ByRef ShisakuEventFlg As Boolean)

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ShisakuFormUtil.Initialize(Me)

        _ShisakuEventCode = shisakuEventCode

        'T_SHISAKU_EVENTから、shisakuEventCodeのSHISAKU_KAIHATSU_FUGOを取得する
        Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
        Dim eventVo As TShisakuEventVo = eventDao.FindByPk(shisakuEventCode)

        'T_SHISAKU_EVENT_BASEから、ShisakuEventCodeのSHISAKU_BASE_EVENT_CODEとSHISAKU_BASE_GOUSYAを取得する
        Dim eventBaseDao As TShisakuEventBaseDao = New TShisakuEventBaseDaoImpl
        Dim tempBaseVo As New TShisakuEventBaseVo
        tempBaseVo.ShisakuEventCode = shisakuEventCode
        Dim eventBaseVos As List(Of TShisakuEventBaseVo) = eventBaseDao.FindBy(tempBaseVo)

        Dim blockInstlDao As ShisakuSekkeiBlockInstlDao = New ShisakuSekkeiBlockInstlDaoImpl

        Dim SekkeiBlockDao As New SekkeiBlockDaoImpl

        Dim blockDao As TShisakuSekkeiBlockDao = New TShisakuSekkeiBlockDaoImpl

        Dim sbVos As New List(Of TShisakuSekkeiBlockVo)
        Dim unitKbnDictionary As New Dictionary2(Of String, String, String)()
        Dim sekkeiBlock As New SekkeiBlockSupplier(eventVo, unitKbnDictionary)
        

        For Each eventBaseVo As TShisakuEventBaseVo In eventBaseVos
            '↓↓2014/10/21 酒井 ADD BEGIN
            ''T_SHISAKU_SEKKEI_BLOCK_INSTLから、上記SHISAKU_BASE_EVENT_CODEとSHISAKU_BASE_GOUSYA、かつ、INSU_SURYO > 0の試作ブロックNoと試作ブロックNo改定Noを取得する	
            'blockInstlVos = blockInstlDao.FindByEventCodeAndGousya(eventBaseVo.ShisakuBaseEventCode, eventBaseVo.ShisakuBaseGousya)

            ' ''↓↓2014/08/27 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD BEGIN
            'If blockInstlVos.Count = 0 Then
            '    '↓↓2014/10/01 酒井 ADD BEGIN
            '    'ShisakuEventFlg = False
            '    'Exit Sub
            '    Continue For
            '    '↑↑2014/10/01 酒井 ADD END
            'End If
            ' ''↑↑2014/08/27 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD END

            'For Each blockInstlVo As TShisakuSekkeiBlockInstlVo In blockInstlVos
            '    '試作ブロックNoが既にsubjectに存在する場合
            '    Dim isExist As Boolean = False
            '    For Each vo As TShisakuBlockSekkeikaTmpVo In subject
            '        If vo.ShisakuBlockNo.Equals(blockInstlVo.ShisakuBlockNo) Then
            '            isExist = True
            '        End If
            '    Next
            '    If isExist Then
            '        Continue For
            '    End If
            '    'RHAC0080から上記SHISAKU_KAIHATSU_FUGOと試作ブロックNoと試作ブロックNo改訂Noで、TANTO_BUSHOを取得する
            '    ''↓↓2014/08/26 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD BEGIN
            '    'r0080Vo = r0080Dao.FindByPk(eventVo.ShisakuKaihatsuFugo, blockInstlVo.ShisakuBlockNo, blockInstlVo.ShisakuBlockNoKaiteiNo)
            '    r0080Vo = SekkeiBlockDao.FindTantoBushoByBlock(eventVo.ShisakuKaihatsuFugo, blockInstlVo.ShisakuBlockNo)
            '    ''↑↑2014/08/26 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD END
            '    ''↓↓2014/09/18 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD BEGIN
            '    If r0080Vo Is Nothing Then
            '        '該当イベント開発符号取得
            '        Dim tmpVos As List(Of TShisakuEventBaseVo)
            '        tmpVos = SekkeiBlockDao.FindByShisakuEventBase(shisakuEventCode)
            '        For Each tmpVo In tmpVos
            '            If Not StringUtil.IsEmpty(tmpVo.BaseKaihatsuFugo) Then
            '                r0080Vo = SekkeiBlockDao.FindTantoBushoByBlock(tmpVo.BaseKaihatsuFugo, blockInstlVo.ShisakuBlockNo)
            '                If Not r0080Vo Is Nothing Then
            '                    Exit For
            '                End If
            '            End If
            '        Next
            '    End If
            '    ''↑↑2014/09/18 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD END
            '    'T_SHISAKU_SEKKEI_BLOCKから試作ブロック名称を取得する。
            '    blockVo = blockDao.FindByPk(blockInstlVo.ShisakuEventCode, blockInstlVo.ShisakuBukaCode, blockInstlVo.ShisakuBlockNo, blockInstlVo.ShisakuBlockNoKaiteiNo)

            '    'subjectに、試作ブロックNoと試作ブロック名称とTANTO_BUSHOを追加する
            '    Dim blockSekkeikaTmpVo As New TShisakuBlockSekkeikaTmpVo
            '    blockSekkeikaTmpVo.ShisakuBlockNo = blockInstlVo.ShisakuBlockNo
            '    ''↓↓2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD BEGIN
            '    'blockSekkeikaTmpVo.ShisakuBlockName = blockVo.ShisakuBlockName
            '    'blockSekkeikaTmpVo.ShisakuBukaCode = r0080Vo.TantoBusho
            '    If Not blockVo Is Nothing Then
            '        blockSekkeikaTmpVo.ShisakuBlockName = blockVo.ShisakuBlockName
            '    Else
            '        blockSekkeikaTmpVo.ShisakuBlockName = ""
            '    End If
            '    If Not r0080Vo Is Nothing Then
            '        blockSekkeikaTmpVo.ShisakuBukaCode = r0080Vo.TantoBusho
            '    Else
            '        ''↓↓2014/09/18 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD BEGIN
            '        'blockSekkeikaTmpVo.ShisakuBukaCode = ""
            '        blockSekkeikaTmpVo.ShisakuBukaCode = blockInstlVo.ShisakuBukaCode
            '        ''↑↑2014/09/18 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD END
            '    End If
            '    ''↑↑2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD END

            '    subject.Add(blockSekkeikaTmpVo)
            'Next

            Dim vos As List(Of TShisakuSekkeiBlockVo) = sekkeiBlock.MakeRegisterValues(eventBaseVo, True)
            sbVos.AddRange(vos)
            '↑↑2014/10/21 酒井 ADD END
        Next
        Dim BukaBlockVos As List(Of TShisakuBlockSekkeikaTmpVo) = sekkeiBlock.ConvSB2BukaBlock(sbVos)
        Dim maeShisakuBukaCode As String = ""
        Dim maeShisakuBlockNo As String = ""
        Dim cnt As Integer = 0

        For index As Integer = 0 To BukaBlockVos.Count - 1
            If BukaBlockVos(index).ShisakuBlockNo = maeShisakuBlockNo Then
                '↓↓2014/10/30 酒井 ADD BEGIN
                If BukaBlockVos(index).ShisakuBukaCode = maeShisakuBukaCode Then
                Else
                    subjectAlart(cnt - 1) = "1"
                End If
            Else
                Dim vo As New TShisakuBlockSekkeikaTmpVo
                vo.ShisakuBlockNo = BukaBlockVos(index).ShisakuBlockNo
                vo.ShisakuBlockName = BukaBlockVos(index).ShisakuBlockName
                vo.ShisakuBukaCode = BukaBlockVos(index).ShisakuBukaCode
                subject.Add(vo)
                subjectAlart.Add("0")
                cnt = cnt + 1
                maeShisakuBlockNo = vo.ShisakuBlockNo
                maeShisakuBukaCode = vo.ShisakuBukaCode
            End If
        Next

        InitializeSpread()

        'subjectの内容を画面に表示する
        SetSpread()
        For Each vo As TShisakuBlockSekkeikaTmpVo In subject
        Next

        'ブロックNoとブロック名称列をlockする
        spdParts_Sheet1.Columns(TAG_BLOCK_NO).Locked = True
        spdParts_Sheet1.Columns(TAG_BLOCK_NAME).Locked = True
        spdParts_Sheet1.SortRows(0, True, True)

        If subject.Count > 0 Then
            ShisakuEventFlg = True
        End If

    End Sub

    ''' <summary></summary>
    ''' <remarks></remarks>
    Private Sub InitializeSpread()
        spdParts_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.Normal

        BuhinEditSpreadUtil.InitializeFrm41(spdParts)

        spdParts_Sheet1.RowCount = subject.Count

        Dim index As Integer = 0
        spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_BLOCK_NO
        spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_BLOCK_NAME
        spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_SEKKEIKA

        '' 通常の Spread_Changed()では、CTRL+V/CTRL+ZでChengedイベントが発生しない
        ''（編集モードではない状態で変更された場合は発生しない仕様とのこと。）
        '' CTRL+V/CTRL+Zでもイベントが発生するハンドラを設定する
        SpreadUtil.AddHandlerSheetDataModelChanged(spdParts_Sheet1, AddressOf Spread_ChangeEventHandlable)
    End Sub

    Private Sub Spread_ChangeEventHandlable(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs)
        Dim changeValue As String = spdParts_Sheet1.ActiveCell.Value
        Dim rowIndex As Integer = spdParts_Sheet1.ActiveRowIndex
        'アクティブセル.value <> subjectの対応するデータ
        If spdParts_Sheet1.Columns(spdParts_Sheet1.ActiveColumnIndex).Tag = TAG_SEKKEIKA Then

            For i As Integer = 0 To subject.Count - 1
                If subject(i).ShisakuBlockNo = spdParts_Sheet1.Cells(rowIndex, 0).Value Then
                    subject(i).ShisakuBukaCode = changeValue
                End If
            Next
        End If
    End Sub

    ''' <summary></summary>
    ''' <remarks></remarks>
    Private Sub SetSpread()
        Dim rowIndex As Integer = 0
        For index As Integer = 0 To subject.Count - 1
            spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_BLOCK_NO).Index).Value = subject(index).ShisakuBlockNo
            spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_BLOCK_NAME).Index).Value = subject(index).ShisakuBlockName
            spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(TAG_SEKKEIKA).Index).Value = subject(index).ShisakuBukaCode
            If subjectAlart(index) = "1" Then
                spdParts_Sheet1.Rows(rowIndex).BackColor = Color.Red
                spdParts_Sheet1.Rows(rowIndex).ForeColor = Color.White
            End If
            rowIndex = rowIndex + 1
        Next
    End Sub

    Private Sub spdParts_Change(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdParts.Change
        Dim changeValue As String = spdParts_Sheet1.ActiveCell.Value
        Dim rowIndex As Integer = spdParts_Sheet1.ActiveRowIndex

        spdParts_Sheet1.ActiveCell.Value = spdParts_Sheet1.ActiveCell.Text.ToUpper
        For i As Integer = 0 To subject.Count - 1
            If subject(i).ShisakuBlockNo = spdParts_Sheet1.Cells(rowIndex, 0).Value Then
                subject(i).ShisakuBukaCode = changeValue.ToUpper
            End If
        Next
    End Sub

#Region "ボタン処理"
    Private Sub btnKoushin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKoushin.Click
        Me._resultOk = True

        Dim deleteDao As ShisakuBlockSekkeikaTmpDao = New ShisakuBlockSekkeikaTmpDaoImpl
        deleteDao.DeleteByEventCode(_ShisakuEventCode)

        'subjectをT_SHISAKU_BLOCK_SEKKEIKA_TMPにINSERTする
        Dim aDate As New ShisakuDate
        Dim insertDao As TShisakuBlockSekkeikaTmpDao = New TShisakuBlockSekkeikaTmpDaoImpl
        For Each insertVo As TShisakuBlockSekkeikaTmpVo In subject
            insertVo.ShisakuEventCode = _ShisakuEventCode
            insertVo.CreatedUserId = LoginInfo.Now.UserId
            insertVo.CreatedDate = aDate.CurrentDateDbFormat
            insertVo.CreatedTime = aDate.CurrentTimeDbFormat
            insertVo.UpdatedUserId = LoginInfo.Now.UserId
            insertVo.UpdatedDate = aDate.CurrentDateDbFormat
            insertVo.UpdatedTime = aDate.CurrentTimeDbFormat

            insertDao.InsertBy(insertVo)
        Next

        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me._resultOk = False
        Me.Close()
    End Sub
#End Region
End Class