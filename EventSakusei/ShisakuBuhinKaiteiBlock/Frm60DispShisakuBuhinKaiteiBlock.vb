Imports FarPoint.Win
Imports FarPoint.Win.Spread.CellType
Imports System.Reflection
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinKaiteiBlock.Dao
Imports EventSakusei.ShisakuBuhinKaiteiBlock.Ui
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Ui.Spd
Imports EventSakusei.ShisakuBuhinEditEvent.Dao
Imports EBom.Data
Imports EBom.Common
Imports System.Text
Imports EventSakusei.ShisakuBuhinEditSekkei.Dao

Namespace ShisakuBuhinKaiteiBlock
    ''' <summary>
    ''' 試作部品表 編集・改訂編集(ブロック)の通知
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm60DispShisakuBuhinKaiteiBlock
#Region "local member"
        ''' <summary>FpSpread共通メソッド</summary>
        Private m_spCom As SpreadCommon

        Private isKachouKengen As Boolean

        Private ToolTipRange As FarPoint.Win.Spread.Model.CellRange

#End Region

#Region "Construct"
        ''' <summary>
        ''' Construct
        ''' </summary>
        Public Sub New()
            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ShisakuFormUtil.Initialize(Me)
        End Sub
#End Region

#Region "起動"
        ''' <summary>
        ''' 起動
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Execute()
            spdParts.FocusRenderer = New FarPoint.Win.Spread.MarqueeFocusIndicatorRenderer(Color.Blue, 1)
            m_spCom = New SpreadCommon(spdParts)
            'ヘッダーを設定する
            InitializeHeader()
            'スプレッドデータを初期化する
            initSpreadDataClr()
        End Sub
#End Region

#Region "初期化"
        ''' <summary>
        ''' 画面ヘーダ部分の初期化
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitializeHeader()

            ShisakuFormUtil.setTitleVersion(Me)
            Label2.Text = "PG-ID：EVENT_EDIT60"
            ShisakuFormUtil.SetDateTimeNow(Me.LblDateNow, Me.LblTimeNow)
            ShisakuFormUtil.SetIdAndBuka(Me.LblCurrUserId, Me.LblCurrBukaName)

            'イベントコードコンボボックスに値を追加
            Dim dtEventCode As DataTable = GetBlockEventCodeData()
            FormUtil.ComboBoxBind(cmbEventCode, dtEventCode, "SHISAKU_EVENT_CODE", "SHISAKU_EVENT_CODE")
            '設計課コンボボックスに値を追加
            Dim dtBuka As DataTable = GetBlockBukaData()

            Dim viw As New DataView(dtBuka)
            Dim isDistinct As Boolean = True
            Dim dtFilter As DataTable = viw.ToTable(isDistinct, "KA_RYAKU_NAME")



            FormUtil.ComboBoxBind(cmbBuka, dtFilter, "KA_RYAKU_NAME", "KA_RYAKU_NAME")
            'ブロック№01コンボボックスに値を追加
            Dim dtBlockNo01 As DataTable = GetBlockNoData()
            FormUtil.ComboBoxBind(cmbBlockNo01, dtBlockNo01, "SHISAKU_BLOCK_NO", "SHISAKU_BLOCK_NO") 'ブロック№FROM
            'ブロック№02コンボボックスに値を追加
            Dim dtBlockNo02 As DataTable = GetBlockNoData()
            FormUtil.ComboBoxBind(cmbBlockNo02, dtBlockNo02, "SHISAKU_BLOCK_NO", "SHISAKU_BLOCK_NO") 'ブロック№To

        End Sub


#Region " イベントコード、部課、ブロックのコンボボックスを作成 "
        ''' <summary>
        ''' 試作設計ブロック情報よりイベントコードを設定する。
        ''' </summary>
        ''' <returns>全部ユーザーのデータテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetBlockEventCodeData() As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                Dim sql As New StringBuilder
                db.Fill(DataSqlCommon.GetBlockShisakuEventCodeSql(), dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' 試作設計ブロック情報より部課を設定。    '''※同一番号は除く。
        ''' </summary>
        ''' <returns>全部部課の略称のデータテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetBlockBukaData() As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                Dim sql As New StringBuilder
                db.Fill(DataSqlCommon.GetBlockBukaSql, dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' 試作設計ブロック情報よりブロック№を設定。
        ''' </summary>
        ''' <returns>全部ブロック№のデータテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetBlockNoData() As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                Dim sql As New StringBuilder
                db.Fill(DataSqlCommon.GetBlockNoSql, dtData)
            End Using
            Return dtData
        End Function

#End Region



        ''' <summary>
        ''' スプレッドデータを設定する
        ''' </summary>
        Private Sub initSpreadDataClr()
            KaiteiBlockSpreadUtil.InitializeFrm9(spdParts)
            Dim sheet = spdParts_Sheet1
            Dim index As Integer = 0
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EVENT
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_FUYOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_JYOUTAI1
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_JYOUTAI2
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAITEI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_UNIT
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEISHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TANTOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TEL
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_UPDATETIME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TANTO_SYOUNIN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TANTO_JYOUTAI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TANTO_SYOZOKU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TANTO_SYONINSYA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TANTO_SYONIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KACHOU_SYOUNIN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KACHOU_JYOUTAI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KACHOU_SYOZOKU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KACHOU_SYONINSYA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KACHOU_SYONIN_HI

            '2012/02/08 担当承認、課長承認ボタン非表示、ヘッダークリックイベントで対応する改修
            sheet.ColumnHeader.Cells.Get(0, m_spCom.GetColFromTag(TAG_TANTO_SYOUNIN).Index).Value = "担当 ►►"
            sheet.ColumnHeader.Cells.Get(0, m_spCom.GetColFromTag(TAG_KACHOU_SYOUNIN).Index).Value = "課長主査 ►►"
            '
            btnDisplayK.Visible = False
            btnDisplayT.Visible = False

            unvisibleTantoKuwashii()
            unvisibleKACHOUKuwashii()

            'スプレッドデータを設定する。（いらないかも・・・）
            initSpreadData()

        End Sub
#End Region

        ''' <summary>
        ''' スプレッドデータを設定する
        ''' </summary>
        Private Sub initSpreadData()

            '初期設定
            Dim intSaisyuKoushinbi01 As Integer
            Dim intSaisyuKoushinbi02 As Integer
            Dim strBlockNo01 As String
            Dim strBlockNo02 As String

            '最終更新日を数値に変換
            '   日付チェックするorしないのラジオボタンにより以下の様に変換する。
            If rbtDate01.Checked = True Then
                intSaisyuKoushinbi01 = DateUtil.ConvDateToIneteger(dtpSaisyuKoushinbi01.Value)
                intSaisyuKoushinbi02 = DateUtil.ConvDateToIneteger(dtpSaisyuKoushinbi02.Value)
            Else
                intSaisyuKoushinbi01 = 0        '最低値
                intSaisyuKoushinbi02 = 99999999 '最大値
            End If


            'ブロック№１がブランクなら最低値を設定する。
            If StringUtil.IsEmpty(cmbBlockNo01.Text) Then
                strBlockNo01 = "0000"
            Else
                strBlockNo01 = cmbBlockNo01.Text
            End If
            'ブロック№２がブランクなら最大値を設定する。
            If StringUtil.IsEmpty(cmbBlockNo02.Text) Then
                strBlockNo02 = "ZZZZ"
            Else
                strBlockNo02 = cmbBlockNo02.Text
            End If

            'イベントコードの最後尾に＊を付ける。
            Dim strEventCode As String = cmbEventCode.Text & "%"

            '課略称から部課コードを取得する。
            '   あいまい検索は不可とする。（部課コード、課略称が混在するため不可能）
            Dim BuKaComonFunc As New ShisakuBuhinEditBlocktxtDaoImpl
            Dim strBukaCode = BuKaComonFunc.GetBuKaCode(cmbBuka.Text)

            Dim strBuka As String = cmbBuka.Text & "%"

            Dim blockDao As IShisakuBuhinKaiteiBlockDao = New ShisakuBuhinKaiteiBlockDaoImpl()
            Dim blockList = blockDao.GetBlockSpreadList(strEventCode, strBuka, _
                                                        strBlockNo01, strBlockNo02, _
                                                        intSaisyuKoushinbi01, intSaisyuKoushinbi02)
            KaiteiBlockSpreadUtil.setRowCount(spdParts, blockList.Count)
            updateSpreadData(blockList)

        End Sub


#Region "コンポーネント状態"
        ''' <summary>担当承認詳細表示</summary>
        Private Sub visibleTantoKuwashii()
            m_spCom.GetColFromTag(TAG_TANTO_SYOZOKU).Visible = True
            m_spCom.GetColFromTag(TAG_TANTO_SYONINSYA).Visible = True
            m_spCom.GetColFromTag(TAG_TANTO_SYONIN_HI).Visible = True
            btnDisplayT.Text = "担当承認詳細非表示"
        End Sub
        ''' <summary>担当承認詳細非表示</summary>
        Private Sub unvisibleTantoKuwashii()
            m_spCom.GetColFromTag(TAG_TANTO_SYOZOKU).Visible = False
            m_spCom.GetColFromTag(TAG_TANTO_SYONINSYA).Visible = False
            m_spCom.GetColFromTag(TAG_TANTO_SYONIN_HI).Visible = False
            btnDisplayT.Text = "担当承認詳細表示"
        End Sub

        ''' <summary>
        ''' 「状態」がチェックボックスをチェックしている場合
        ''' </summary>
        ''' <param name="rowNo">行番</param>
        ''' <remarks></remarks>
        Private Sub jyoutai1Finished(ByVal rowNo As Integer)
            Dim chkType As New CheckBoxCellType()
            chkType.Caption = "完了"
            m_spCom.GetCell(TAG_JYOUTAI1, rowNo).CellType = chkType
            m_spCom.GetCell(TAG_JYOUTAI1, rowNo).Locked = True
            If (Not IsBlockFuyouRow(rowNo)) Then
                m_spCom.GetCell(TAG_TANTO_SYOUNIN, rowNo).Locked = False
            End If
        End Sub

        ''' <summary>
        ''' 「状態」がチェックボックスをチェックしない場合
        ''' </summary>
        ''' <param name="rowNo">行番</param>
        ''' <remarks></remarks>
        Private Sub jyoutai1NotFinished(ByVal rowNo As Integer)
            Dim chkType As New CheckBoxCellType()
            chkType.Caption = ""
            m_spCom.GetCell(TAG_JYOUTAI1, rowNo).CellType = chkType
            If Not IsBlockFuyouRow(rowNo) Then
                m_spCom.GetCell(TAG_TANTO_SYOUNIN, rowNo).Locked = True
            Else
                m_spCom.GetCell(TAG_TANTO_SYOUNIN, rowNo).Locked = False
            End If
        End Sub

        ''' <summary>
        ''' 「担当承認」がチェックボックスをチェックしている場合
        ''' </summary>
        ''' <param name="rowNo">行番</param>
        ''' <remarks></remarks>
        Private Sub tantoShouninApproval(ByVal rowNo As Integer)
            setJyoutai2Cell(TShisakuSekkeiBlockVoHelper.TantoJyoutai.APPROVAL, m_spCom.GetCell(TAG_JYOUTAI2, rowNo))
            m_spCom.GetCell(TAG_TANTO_SYOUNIN, rowNo).Locked = True
            If (isKachouKengen) AndAlso (Not IsBlockFuyouRow(rowNo)) Then
                m_spCom.GetCell(TAG_KACHOU_SYOUNIN, rowNo).Locked = False
            End If
        End Sub
        ''' <summary>
        ''' 「担当承認」がチェックボックスをチェックしない場合
        ''' </summary>
        ''' <param name="rowNo">行番</param>
        ''' <remarks></remarks>
        Private Sub tantoShouninNotApproval(ByVal rowNo As Integer)
            m_spCom.GetCell(TAG_KACHOU_SYOUNIN, rowNo).Locked = True
        End Sub
        ''' <summary>
        ''' 「課長承認」がチェックボックスをチェックしている場合
        ''' </summary>
        ''' <param name="rowNo">行番</param>
        ''' <remarks></remarks>
        Private Sub kachouShouninApproval(ByVal rowNo As Integer)
            setJyoutai2Cell(TShisakuSekkeiBlockVoHelper.KachouJyoutai.APPROVAL, m_spCom.GetCell(TAG_JYOUTAI2, rowNo))
            m_spCom.GetCell(TAG_KACHOU_SYOUNIN, rowNo).Locked = True
        End Sub
        ''' <summary>
        ''' 「課長承認」がチェックボックスをチェックしない場合
        ''' </summary>
        ''' <param name="rowNo">行番</param>
        ''' <remarks></remarks>
        Private Sub kachouShouninNotApproval(ByVal rowNo As Integer)
            ''今までの仕様が要らないですけれども、保留
        End Sub


        ''' <summary>
        ''' 「ブロック不要」が必要の場合（チェックボックスにチェックがない場合）
        ''' </summary>
        ''' <param name="rowNo">行番</param>
        ''' <remarks></remarks>
        Private Sub blockFuyouNecessary(ByVal rowNo As Integer)
            Dim chkType As New CheckBoxCellType()
            chkType.Caption = ""
            m_spCom.GetCell(TAG_FUYOU, rowNo).CellType = chkType
            If m_spCom.GetCell(TAG_JYOUTAI2, rowNo).Text.Equals(TShisakuSekkeiBlockVoHelper.JyoutaiMoji.REGISTER) _
                AndAlso (Not IsJyoutaiCheckedRow(rowNo)) Then
                m_spCom.GetCell(TAG_JYOUTAI1, rowNo).Locked = False
            End If
            If IsJyoutaiCheckedRow(rowNo) AndAlso (Not IsTantoSyouninCheckedRow(rowNo)) Then
                m_spCom.GetCell(TAG_TANTO_SYOUNIN, rowNo).Locked = False
            End If
            If (IsTantoSyouninCheckedRow(rowNo)) AndAlso (isKachouKengen) AndAlso (Not IsKachouSyouninCheckedRow(rowNo)) Then
                m_spCom.GetCell(TAG_KACHOU_SYOUNIN, rowNo).Locked = False
            End If
            m_spCom.GetCell(TAG_FUYOU, rowNo).Row.BackColor = Nothing

        End Sub
        ''' <summary>
        ''' 「ブロック不要」が必要の場合（チェックボックスにチェックがある場合）
        ''' </summary>
        ''' <param name="rowNo">行番</param>
        ''' <remarks></remarks>
        Private Sub blockFuyouUnnecessary(ByVal rowNo As Integer)
            Dim chkType As New CheckBoxCellType()
            chkType.Caption = "不要"
            m_spCom.GetCell(TAG_FUYOU, rowNo).CellType = chkType
            m_spCom.GetCell(TAG_JYOUTAI1, rowNo).Locked = True
            m_spCom.GetCell(TAG_TANTO_SYOUNIN, rowNo).Locked = False
            If isKachouKengen Then
                If IsTantoSyouninCheckedRow(rowNo) Then
                    m_spCom.GetCell(TAG_KACHOU_SYOUNIN, rowNo).Locked = False
                End If
            End If
            m_spCom.GetCell(TAG_FUYOU, rowNo).Row.BackColor = Color.Gray
        End Sub

        ''' <summary>課長承認詳細表示</summary>
        Private Sub visibleKACHOUKuwashii()
            m_spCom.GetColFromTag(TAG_KACHOU_SYOZOKU).Visible = True
            m_spCom.GetColFromTag(TAG_KACHOU_SYONINSYA).Visible = True
            m_spCom.GetColFromTag(TAG_KACHOU_SYONIN_HI).Visible = True
            btnDisplayK.Text = "課長承認詳細非表示"
        End Sub
        ''' <summary>課長承認詳細非表示</summary>
        Private Sub unvisibleKACHOUKuwashii()
            m_spCom.GetColFromTag(TAG_KACHOU_SYOZOKU).Visible = False
            m_spCom.GetColFromTag(TAG_KACHOU_SYONINSYA).Visible = False
            m_spCom.GetColFromTag(TAG_KACHOU_SYONIN_HI).Visible = False
            btnDisplayK.Text = "課長承認詳細表示"
        End Sub

#End Region

#Region "ボタンアクション"
        ''' <summary>
        ''' 「アプリケーション終了」ボタン
        ''' </summary>
        ''' <param name="sender">Form</param>
        ''' <param name="e">ボタンクリックのイベント</param>
        ''' <remarks></remarks>
        Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
            Application.Exit()
            System.Environment.Exit(0)
        End Sub
        ''' <summary>
        ''' 「戻る」ボタン
        ''' </summary>
        ''' <param name="sender">Form</param>
        ''' <param name="e">ボタンクリックのイベント</param>
        ''' <remarks></remarks>
        Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
            Me.Close()
        End Sub

        ''' <summary>
        ''' 担当承認詳細表示
        ''' </summary>
        ''' <param name="sender">担当承認詳細表示</param>
        ''' <param name="e">担当承認詳細表示</param>
        ''' <remarks></remarks>
        Private Sub btnDisplayT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisplayT.Click
            If btnDisplayT.Text = "担当承認詳細表示" Then
                visibleTantoKuwashii()
            ElseIf btnDisplayT.Text = "担当承認詳細非表示" Then
                unvisibleTantoKuwashii()
            End If
        End Sub

        ''' <summary>
        ''' 課長承認詳細表示
        ''' </summary>
        ''' <param name="sender">課長承認詳細表示</param>
        ''' <param name="e">課長承認詳細表示</param>
        ''' <remarks></remarks>
        Private Sub btnDisplayK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisplayK.Click
            If btnDisplayK.Text = "課長承認詳細表示" Then
                visibleKACHOUKuwashii()
            ElseIf btnDisplayK.Text = "課長承認詳細非表示" Then
                unvisibleKACHOUKuwashii()
            End If
        End Sub


#End Region


#Region "SpreadのColumnのTag"
        ''' <summary>イベントコード</summary>
        Private ReadOnly TAG_EVENT As String = "EVENT_Column"
        ''' <summary>設計課</summary>
        Private ReadOnly TAG_BUKA As String = "BUKA_Column"
        ''' <summary>ブロック不要</summary>
        Private ReadOnly TAG_FUYOU As String = "FUYOU_Column"
        ''' <summary>状態</summary>
        Private ReadOnly TAG_JYOUTAI1 As String = "JYOUTAI1_Column"
        ''' <summary>状態ラベル</summary>
        Private ReadOnly TAG_JYOUTAI2 As String = "JYOUTAI2_Column"
        ''' <summary>ブロックNo</summary>
        Private ReadOnly TAG_NO As String = "NO_Column"
        ''' <summary>改訂</summary>
        Private ReadOnly TAG_KAITEI As String = "KAITEI_Column"
        ''' <summary>ユニード</summary>
        Private ReadOnly TAG_UNIT As String = "UNIT_Column"
        ''' <summary>ブロック名称</summary>
        Private ReadOnly TAG_MEISHOU As String = "MEISHOU_Column"
        ''' <summary>担当者</summary>
        Private ReadOnly TAG_TANTOU As String = "TANTOU_Column"
        ''' <summary>TEL</summary>
        Private ReadOnly TAG_TEL As String = "TEL_Column"
        ''' <summary>最終更新日</summary>
        Private ReadOnly TAG_UPDATETIME As String = "UPDATETIME_Column"
        ''' <summary>担当承認</summary>
        Private ReadOnly TAG_TANTO_SYOUNIN As String = "TANTO_SYOUNIN_Column"
        ''' <summary>担当承認状態</summary>
        Private ReadOnly TAG_TANTO_JYOUTAI As String = "TANTO_SYONIN_JOUTAI_Column"
        ''' <summary>担当所属</summary>
        Private ReadOnly TAG_TANTO_SYOZOKU As String = "TANTO_SYOZOKU_Column"
        ''' <summary>担当承認者</summary>
        Private ReadOnly TAG_TANTO_SYONINSYA As String = "TANTO_SYONINSYA_Column"
        ''' <summary>担当承認日</summary>
        Private ReadOnly TAG_TANTO_SYONIN_HI As String = "TANTO_SYONIN_HI_Column"
        ''' <summary>課長承認</summary>
        Private ReadOnly TAG_KACHOU_SYOUNIN As String = "KACHOU_SYOUNIN_Column"
        ''' <summary>担当承認状態</summary>
        Private ReadOnly TAG_KACHOU_JYOUTAI As String = "KACHOU_SYONIN_JOUTAI_Column"
        ''' <summary>課長所属</summary>
        Private ReadOnly TAG_KACHOU_SYOZOKU As String = "KACHOU_SYOZOKU_Column"
        ''' <summary>課長承認者</summary>
        Private ReadOnly TAG_KACHOU_SYONINSYA As String = "KACHOU_SYONINSYA_Column"
        ''' <summary>課長承認日</summary>
        Private ReadOnly TAG_KACHOU_SYONIN_HI As String = "KACHOU_SYONIN_HI_Column"

#End Region

#Region "spread　一覧設定"
        ''' <summary>
        ''' spread　一覧を表示する
        ''' </summary>
        ''' <param name="blockList">試作設計ブロック一覧</param>
        ''' <remarks></remarks>
        Private Sub updateSpreadData(ByVal blockList As List(Of ShisakuBuhinBlockVo))
            Dim i As Integer
            For i = 0 To blockList.Count - 1
                Dim shisakuBuhinBlock = blockList.Item(i)
                UpdateSpreadRow(shisakuBuhinBlock, i)
            Next
        End Sub
        ''' <summary>
        ''' spread　行を表示する
        ''' </summary>
        ''' <param name="shisakuBuhinBlock">試作設計ブロック</param>
        ''' <param name="rowNo">spread行の番号</param>
        ''' <remarks></remarks>
        Private Sub UpdateSpreadRow(ByVal shisakuBuhinBlock As ShisakuBuhinBlockVo, ByVal rowNo As Integer)
            Dim shisakuBuhinBlockHelp = New TShisakuSekkeiBlockVoHelper(shisakuBuhinBlock)
            Dim KARyakuName = New KaRyakuNameDaoImpl
            m_spCom.GetCell(TAG_EVENT, rowNo).Text = shisakuBuhinBlock.ShisakuEventCode
            m_spCom.GetCell(TAG_BUKA, rowNo).Text = shisakuBuhinBlock.KaRyakuName
            setBlockFuyouCell(shisakuBuhinBlock.BlockFuyou, m_spCom.GetCell(TAG_FUYOU, rowNo))
            setJyoutai1Cell(shisakuBuhinBlock.Jyoutai, m_spCom.GetCell(TAG_JYOUTAI1, rowNo))
            setJyoutai2Cell(shisakuBuhinBlock.Jyoutai, m_spCom.GetCell(TAG_JYOUTAI2, rowNo))
            m_spCom.GetCell(TAG_NO, rowNo).Text = shisakuBuhinBlock.ShisakuBlockNo
            m_spCom.GetCell(TAG_KAITEI, rowNo).Text = shisakuBuhinBlock.ShisakuBlockNoKaiteiNo
            m_spCom.GetCell(TAG_UNIT, rowNo).Text = shisakuBuhinBlock.UnitKbn
            m_spCom.GetCell(TAG_MEISHOU, rowNo).Text = shisakuBuhinBlock.ShisakuBlockName
            m_spCom.GetCell(TAG_TANTOU, rowNo).Text = shisakuBuhinBlock.SyainName
            m_spCom.GetCell(TAG_TEL, rowNo).Text = shisakuBuhinBlock.TelNo

            'ブロック不要の場合には課長承認日時をセットする。
            If m_spCom.GetCell(TAG_FUYOU, rowNo).Value = True Then
                m_spCom.GetCell(TAG_UPDATETIME, rowNo).Text = shisakuBuhinBlockHelp.KachouSyouninHi & " " & _
                                          shisakuBuhinBlockHelp.KachouSyouninJikan
            Else
                m_spCom.GetCell(TAG_UPDATETIME, rowNo).Text = shisakuBuhinBlockHelp.SaisyuuKoushinbi & " " & _
                                          shisakuBuhinBlockHelp.SaisyuuKoushinjikan
            End If

            setTantoShouninCell(shisakuBuhinBlock.TantoSyouninJyoutai, m_spCom.GetCell(TAG_TANTO_SYOUNIN, rowNo))


            '仕様変更のため追加'
            If IsTantoSyouninCheckedRow(rowNo) Then
                m_spCom.GetCell(TAG_TANTO_JYOUTAI, rowNo).Text = TShisakuSekkeiBlockVoHelper.TantoJyoutai2.OK
            Else
                m_spCom.GetCell(TAG_TANTO_JYOUTAI, rowNo).Text = ""
            End If
            '担当承認課の部課コードから、部課略名を表示する処理を追加'
            If Not StringUtil.IsEmpty(shisakuBuhinBlock.TantoSyouninKa) Then
                If Not KARyakuName.GetKa_Ryaku_Name(shisakuBuhinBlock.TantoSyouninKa) Is Nothing Then
                    m_spCom.GetCell(TAG_TANTO_SYOZOKU, rowNo).Text = KARyakuName.GetKa_Ryaku_Name(shisakuBuhinBlock.TantoSyouninKa).KaRyakuName
                Else
                    m_spCom.GetCell(TAG_TANTO_SYOZOKU, rowNo).Text = ""
                End If
            Else
                m_spCom.GetCell(TAG_TANTO_SYOZOKU, rowNo).Text = ""
            End If
            m_spCom.GetCell(TAG_TANTO_SYONINSYA, rowNo).Text = shisakuBuhinBlock.TantoName
            m_spCom.GetCell(TAG_TANTO_SYONIN_HI, rowNo).Text = shisakuBuhinBlockHelp.TantoSyouninHi & " " & _
                                                      shisakuBuhinBlockHelp.TantoSyouninJikan
            setKachouShouninCell(shisakuBuhinBlock.KachouSyouninJyoutai, m_spCom.GetCell(TAG_KACHOU_SYOUNIN, rowNo))
            '仕様変更のため追加'
            If IsKachouSyouninCheckedRow(rowNo) Then
                m_spCom.GetCell(TAG_KACHOU_JYOUTAI, rowNo).Text = TShisakuSekkeiBlockVoHelper.KachouJyoutai2.OK
            Else
                m_spCom.GetCell(TAG_KACHOU_JYOUTAI, rowNo).Text = ""
            End If
            '課長承認課の部課コードから、部課略名を表示する処理を追加'
            If Not StringUtil.IsEmpty(shisakuBuhinBlock.KachouSyouninKa) Then
                If Not KARyakuName.GetKa_Ryaku_Name(shisakuBuhinBlock.KachouSyouninKa) Is Nothing Then
                    m_spCom.GetCell(TAG_KACHOU_SYOZOKU, rowNo).Text = KARyakuName.GetKa_Ryaku_Name(shisakuBuhinBlock.KachouSyouninKa).KaRyakuName
                Else
                    m_spCom.GetCell(TAG_KACHOU_SYOZOKU, rowNo).Text = ""
                End If

            Else
                m_spCom.GetCell(TAG_KACHOU_SYOZOKU, rowNo).Text = ""
            End If
            m_spCom.GetCell(TAG_KACHOU_SYONINSYA, rowNo).Text = shisakuBuhinBlock.KachouName

            If StringUtil.IsEmpty(shisakuBuhinBlockHelp.KachouSyouninHi) Then
                m_spCom.GetCell(TAG_KACHOU_SYONIN_HI, rowNo).Text = ""
            Else
                m_spCom.GetCell(TAG_KACHOU_SYONIN_HI, rowNo).Text = shisakuBuhinBlockHelp.KachouSyouninHi & " " & _
                                          shisakuBuhinBlockHelp.KachouSyouninJikan
            End If

            '全ての項目を使用不可（ロック）する）
            m_spCom.GetCell(TAG_EVENT, rowNo).Locked = True
            m_spCom.GetCell(TAG_BUKA, rowNo).Locked = True
            m_spCom.GetCell(TAG_FUYOU, rowNo).Locked = True
            m_spCom.GetCell(TAG_JYOUTAI1, rowNo).Locked = True
            m_spCom.GetCell(TAG_JYOUTAI2, rowNo).Locked = True
            m_spCom.GetCell(TAG_NO, rowNo).Locked = True
            m_spCom.GetCell(TAG_KAITEI, rowNo).Locked = True
            m_spCom.GetCell(TAG_UNIT, rowNo).Locked = True
            m_spCom.GetCell(TAG_MEISHOU, rowNo).Locked = True
            m_spCom.GetCell(TAG_TANTOU, rowNo).Locked = True
            m_spCom.GetCell(TAG_TEL, rowNo).Locked = True
            m_spCom.GetCell(TAG_UPDATETIME, rowNo).Locked = True
            m_spCom.GetCell(TAG_TANTO_SYOUNIN, rowNo).Locked = True
            m_spCom.GetCell(TAG_TANTO_JYOUTAI, rowNo).Locked = True
            m_spCom.GetCell(TAG_TANTO_SYOZOKU, rowNo).Locked = True
            m_spCom.GetCell(TAG_TANTO_SYONINSYA, rowNo).Locked = True
            m_spCom.GetCell(TAG_TANTO_SYONIN_HI, rowNo).Locked = True
            m_spCom.GetCell(TAG_KACHOU_SYOUNIN, rowNo).Locked = True
            m_spCom.GetCell(TAG_KACHOU_JYOUTAI, rowNo).Locked = True
            m_spCom.GetCell(TAG_KACHOU_SYOZOKU, rowNo).Locked = True
            m_spCom.GetCell(TAG_KACHOU_SYONINSYA, rowNo).Locked = True
            m_spCom.GetCell(TAG_KACHOU_SYONIN_HI, rowNo).Locked = True


        End Sub

        ''' <summary>
        ''' ブロック不要列のチェックボックスの設定
        ''' </summary>
        ''' <param name="blockFuyouValue">ブロック不要の値</param>
        ''' <param name="blockFuyouCell">目的セル</param>
        ''' <remarks></remarks>
        Private Sub setBlockFuyouCell(ByVal blockFuyouValue As String, ByVal blockFuyouCell As Spread.Cell)
            Dim chkType As New CheckBoxCellType()
            Select Case blockFuyouValue
                Case TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY
                    blockFuyouCell.Value = False
                    blockFuyouNecessary(blockFuyouCell.Row.Index)
                Case " "
                    blockFuyouCell.Value = False
                    blockFuyouNecessary(blockFuyouCell.Row.Index)
                Case TShisakuSekkeiBlockVoHelper.BlockFuyou.UNNECESSARY
                    blockFuyouCell.Value = True
                    blockFuyouUnnecessary(blockFuyouCell.Row.Index)
            End Select
        End Sub
        ''' <summary>
        ''' 状態列と状態ラベル列のセルの設定
        ''' </summary>
        ''' <param name="jyoutaiValue">状態の値</param>
        ''' <param name="jyoutai1Cell">目的セル</param>
        ''' <remarks></remarks>
        Private Sub setJyoutai1Cell(ByVal jyoutaiValue As String, ByVal jyoutai1Cell As Spread.Cell)
            '2013/06/24　AL再展開用追加
            Select Case jyoutaiValue
                Case TShisakuSekkeiBlockVoHelper.Jyoutai.REALBUHIN
                    jyoutai1Cell.Value = False
                    jyoutai1NotFinished(jyoutai1Cell.Row.Index)
                    If (Not IsBlockFuyouRow(jyoutai1Cell.Row.Index)) Then
                        m_spCom.GetCell(TAG_JYOUTAI1, jyoutai1Cell.Row.Index).Locked = False
                    End If

                Case TShisakuSekkeiBlockVoHelper.Jyoutai.FINISHED
                    jyoutai1Cell.Value = True
                    jyoutai1Finished(jyoutai1Cell.Row.Index)
                Case TShisakuSekkeiBlockVoHelper.Jyoutai.REGISTER
                    jyoutai1Cell.Value = False
                    jyoutai1NotFinished(jyoutai1Cell.Row.Index)
                    If (Not IsBlockFuyouRow(jyoutai1Cell.Row.Index)) Then
                        m_spCom.GetCell(TAG_JYOUTAI1, jyoutai1Cell.Row.Index).Locked = False
                    End If
                Case "  "
                    jyoutai1Cell.Value = False
                    jyoutai1NotFinished(jyoutai1Cell.Row.Index)
                    If (Not IsBlockFuyouRow(jyoutai1Cell.Row.Index)) Then
                        m_spCom.GetCell(TAG_JYOUTAI1, jyoutai1Cell.Row.Index).Locked = False
                    End If
                Case Else
                    jyoutai1Cell.Value = False
                    jyoutai1NotFinished(jyoutai1Cell.Row.Index)
                    m_spCom.GetCell(TAG_JYOUTAI1, jyoutai1Cell.Row.Index).Locked = True
            End Select

        End Sub
        ''' <summary>
        ''' 状態ラベル列のセルの設定
        ''' </summary>
        ''' <param name="jyoutaiValue"></param>
        ''' <param name="jyoutai2Cell"></param>
        ''' <remarks></remarks>
        Private Sub setJyoutai2Cell(ByVal jyoutaiValue As String, ByVal jyoutai2Cell As Spread.Cell)
            '2013/06/24　ＡＬ再展開用追加
            Select Case jyoutaiValue
                Case TShisakuSekkeiBlockVoHelper.Jyoutai.REALBUHIN
                    jyoutai2Cell.Text = TShisakuSekkeiBlockVoHelper.JyoutaiMoji.REALBUHIN

                Case TShisakuSekkeiBlockVoHelper.Jyoutai.EDIT
                    jyoutai2Cell.Text = TShisakuSekkeiBlockVoHelper.JyoutaiMoji.EDIT
                Case TShisakuSekkeiBlockVoHelper.Jyoutai.SAVE
                    jyoutai2Cell.Text = TShisakuSekkeiBlockVoHelper.JyoutaiMoji.SAVE
                Case TShisakuSekkeiBlockVoHelper.Jyoutai.REGISTER
                    jyoutai2Cell.Text = TShisakuSekkeiBlockVoHelper.JyoutaiMoji.REGISTER
                Case TShisakuSekkeiBlockVoHelper.Jyoutai.FINISHED
                    jyoutai2Cell.Text = ""
                Case TShisakuSekkeiBlockVoHelper.TantoJyoutai.APPROVAL
                    jyoutai2Cell.Text = TShisakuSekkeiBlockVoHelper.TantoJyoutaiMoji.APPROVAL
                Case TShisakuSekkeiBlockVoHelper.KachouJyoutai.APPROVAL
                    jyoutai2Cell.Text = TShisakuSekkeiBlockVoHelper.KachouJyoutaiMoji.APPROVAL
                Case Else
                    jyoutai2Cell.Text = ""
            End Select
        End Sub

        ''' <summary>
        ''' 担当承認列のチェックボックスの設定
        ''' </summary>
        ''' <param name="tantoShouninValue">担当承認の値</param>
        ''' <param name="tantoShouninCell">目的セル</param>
        ''' <remarks></remarks>
        Private Sub setTantoShouninCell(ByVal tantoShouninValue As String, ByVal tantoShouninCell As Spread.Cell)
            Select Case tantoShouninValue
                Case TShisakuSekkeiBlockVoHelper.TantoJyoutai.APPROVAL
                    tantoShouninCell.Value = True
                    tantoShouninApproval(tantoShouninCell.Row.Index)
                Case Else
                    tantoShouninCell.Value = False
                    tantoShouninNotApproval(tantoShouninCell.Row.Index)
            End Select
        End Sub

        ''' <summary>
        ''' 課長承認列のチェックボックスの設定
        ''' </summary>
        ''' <param name="kachouShouninValue">課長承認の値</param>
        ''' <param name="kachouShouninCell">目的セル</param>
        ''' <remarks></remarks>
        Private Sub setKachouShouninCell(ByVal kachouShouninValue As String, ByVal kachouShouninCell As Spread.Cell)
            Select Case kachouShouninValue
                Case TShisakuSekkeiBlockVoHelper.KachouJyoutai.APPROVAL
                    kachouShouninCell.Value = True
                    kachouShouninApproval(kachouShouninCell.Row.Index)
                Case Else
                    kachouShouninCell.Value = False
                    kachouShouninNotApproval(kachouShouninCell.Row.Index)
            End Select
        End Sub

#End Region

#Region "チェック"
        ''' <summary>
        ''' 行の「ブロック不要」状態を判断
        ''' </summary>
        ''' <param name="rowNo">行番号</param>
        ''' <returns>「ブロック不要」状態</returns>
        ''' <remarks></remarks>
        Private Function IsBlockFuyouRow(ByVal rowNo As Integer) As Boolean
            If m_spCom.GetCell(TAG_FUYOU, rowNo).Value = True Then
                Return True
            End If
            Return False
        End Function

        ''' <summary>
        ''' 行の「状態」状態を判断
        ''' </summary>
        ''' <param name="rowNo">行番号</param>
        ''' <returns>「状態」の状態</returns>
        ''' <remarks></remarks>
        Private Function IsJyoutaiCheckedRow(ByVal rowNo As Integer) As Boolean
            If rowNo = -1 Then
                Return False
            End If
            If m_spCom.GetCell(TAG_JYOUTAI1, rowNo).Value = True Then
                Return True
            End If
            Return False
        End Function

        ''' <summary>
        ''' 行の「担当承認」状態を判断
        ''' </summary>
        ''' <param name="rowNo">行番号</param>
        ''' <returns>「担当承認」の状態</returns>
        ''' <remarks></remarks>
        Private Function IsTantoSyouninCheckedRow(ByVal rowNo As Integer) As Boolean
            If rowNo = -1 Then
                Return False
            End If
            If m_spCom.GetCell(TAG_TANTO_SYOUNIN, rowNo).Value = True Then
                Return True
            End If
            Return False
        End Function

        ''' <summary>
        ''' 行の「課長承認」状態を判断
        ''' </summary>
        ''' <param name="rowNo">行番号</param>
        ''' <returns>「課長承認」の状態</returns>
        ''' <remarks></remarks>
        Private Function IsKachouSyouninCheckedRow(ByVal rowNo As Integer) As Boolean
            If rowNo = -1 Then
                Return False
            End If
            If m_spCom.GetCell(TAG_KACHOU_SYOUNIN, rowNo).Value = True Then
                Return True
            End If
            Return False
        End Function
#End Region


        ''' <summary>
        ''' ブロックNoからrowNoを取得する
        ''' </summary>
        ''' <param name="txt">ブロックNo</param>
        ''' <returns>rowNo</returns>
        ''' <remarks></remarks>
        Private Function rowNoFind(ByVal txt As String) As Integer
            '引数に入る値txtBlockNo.Text()
            Dim result As Integer = -1
            For i As Integer = 0 To spdParts_Sheet1.RowCount() - 1
                For j As Integer = 0 To spdParts_Sheet1.ColumnCount() - 1
                    If txt.Equals(spdParts_Sheet1.Cells(i, j).Value) Then
                        result = i
                    End If
                Next
            Next
            Return result
        End Function

        Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        End Sub


        Private Sub btnCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCall.Click
            'スプレッドデータを設定する
            initSpreadData()
        End Sub


        'ＥＸＣＥＬ出力
        Private Sub btnExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelExport.Click

            ExcelCommon.SaveExcelFile("試作部品表 改訂通知（ブロック）", spdParts, "改訂通知")

        End Sub

        Private Sub spdParts_CellClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellClick
            '-------------------------------------------------------------------------------------------------------
            '２次改修
            '   ヘッダー部を制御する。
            If e.ColumnHeader Then
                '2012/02/08 担当承認、課長承認ボタン非表示、ヘッダークリックイベントで対応する改修
                If (e.Row = 0) Then
                    If (e.Column = m_spCom.GetColFromTag(TAG_TANTO_SYOUNIN).Index) Then
                        If m_spCom.GetColFromTag(TAG_TANTO_SYOZOKU).Visible = False Then
                            '非表示の場合＝＞表示に
                            spdParts_Sheet1.ColumnHeader.Cells.Get(0, m_spCom.GetColFromTag(TAG_TANTO_SYOUNIN).Index).Value = "担当 ◄◄"
                            visibleTantoKuwashii()
                        Else
                            '表示の場合＝＞非表示に
                            spdParts_Sheet1.ColumnHeader.Cells.Get(0, m_spCom.GetColFromTag(TAG_TANTO_SYOUNIN).Index).Value = "担当 ►►"
                            unvisibleTantoKuwashii()
                        End If
                    End If
                    If (e.Column = m_spCom.GetColFromTag(TAG_KACHOU_SYOUNIN).Index) Then
                        If m_spCom.GetColFromTag(TAG_KACHOU_SYOZOKU).Visible = False Then
                            '非表示の場合＝＞表示に
                            spdParts_Sheet1.ColumnHeader.Cells.Get(0, m_spCom.GetColFromTag(TAG_KACHOU_SYOUNIN).Index).Value = "課長主査 ◄◄"
                            visibleKACHOUKuwashii()
                        Else
                            '表示の場合＝＞非表示に
                            spdParts_Sheet1.ColumnHeader.Cells.Get(0, m_spCom.GetColFromTag(TAG_KACHOU_SYOUNIN).Index).Value = "課長主査 ►►"
                            unvisibleKACHOUKuwashii()
                        End If
                    End If

                End If
            End If

            '　改訂№がクリックされたら改訂履歴の情報をメッセージボックスへ表示する。
            If (e.Column = m_spCom.GetColFromTag(TAG_KAITEI).Index) Then

                '改訂内容を取得
                Dim blockKaiteiDao As ShisakuBuhinEditBlock.Dao.IShisakuBuhinEditBlockDao = _
                                        New ShisakuBuhinEditBlock.Dao.ShisakuBuhinEditBlockDaoImpl()
                Dim blockKaiteiList = blockKaiteiDao.GetBlockKaiteiList(m_spCom.GetValue(TAG_EVENT, e.Row), m_spCom.GetValue(TAG_NO, e.Row))
                Dim i As Integer = 0
                Dim KaiteiNo As String
                Dim Hi As String
                Dim Naiyou As String

                '改訂内容をセット
                Dim strMsg As String
                strMsg = "ブロック№：" & m_spCom.GetValue(TAG_NO, e.Row) & vbCrLf & vbCrLf & _
                         "[改訂]" & Space(2) & "[更新日]" & Space(3) & "[内容] "
                For i = 0 To blockKaiteiList.Count - 1

                    '改訂№をセット
                    KaiteiNo = blockKaiteiList.Item(i).ShisakuBlockNoKaiteiNo

                    '更新日をセット
                    Hi = ShisakuComFunc.moji8Convert2Date(blockKaiteiList.Item(i).SaisyuKoushinbi)
                    '最終更新日が無ければ更新日、作成日と見ていく。
                    If StringUtil.IsEmpty(Hi) Then
                        Hi = ShisakuComFunc.moji8Convert2Date(Integer.Parse(blockKaiteiList.Item(i).UpdatedDate.Replace("-", "")))
                    End If
                    If StringUtil.IsEmpty(Hi) Then
                        Hi = ShisakuComFunc.moji8Convert2Date(Integer.Parse(blockKaiteiList.Item(i).CreatedDate.Replace("-", "")))
                    End If
                    '   但し、イニシャル以降で、課長承認まで済んでいない場合にはブランクをセットする。
                    If KaiteiNo >= "001" And _
                        blockKaiteiList.Item(i).KachouSyouninJyoutai <> ShishakuSekkeiBlockStatusShounin2 Then
                        Hi = Space(12)
                    End If

                    '改訂内容をセット
                    '   改訂№が000で内容がブランクの場合、固定値をセット。
                    If KaiteiNo = "000" Then
                        Naiyou = "イニシャル"
                    Else
                        Naiyou = blockKaiteiList.Item(i).KaiteiNaiyou
                    End If

                    '本文
                    strMsg += vbCrLf & Space(2) & KaiteiNo & _
                                Space(4) & Hi & _
                                Space(2) & Naiyou
                Next

                '改訂内容を表示
                MsgBox(strMsg, MsgBoxStyle.Information, "【改訂履歴】")

            End If

        End Sub

        ''' <summary>
        ''' spread中身tooltip（チェックボックスのtooltipがスペシャルので）
        ''' </summary>
        ''' <param name="sender">spread</param>
        ''' <param name="e">tooltip event</param>
        ''' <remarks></remarks>
        Private Sub spdParts_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles spdParts.MouseMove
            Dim range As FarPoint.Win.Spread.Model.CellRange = spdParts.GetCellFromPixel(0, 0, e.X, e.Y)
            Dim col As Spread.Column = spdParts_Sheet1.Columns(range.Column)

            '2013/05/20　Windows7対応
            '   カーソル位置を求めて、変更がある（位置が変わった）時だけ動くようにしてみた。
            If StringUtil.IsEmpty(ToolTipRange) OrElse _
                ToolTipRange.Column <> range.Column OrElse ToolTipRange.Row <> range.Row Then

                Dim tipText As String = ""
                Select Case col.Tag
                    Case TAG_KAITEI
                        tipText = "ブロックの改訂内容を確認する場合、クリックして下さい。"
                    Case Else
                        tipText = ""
                End Select
                Me.ToolTip1.SetToolTip(spdParts, tipText)
            End If
            ToolTipRange = range

        End Sub

        Private Sub rbtDate01_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtDate01.CheckedChanged
            If rbtDate01.Checked = True Then
                dtpSaisyuKoushinbi01.Enabled = True
                dtpSaisyuKoushinbi02.Enabled = True
                rbtDate02.Checked = False
            Else
                dtpSaisyuKoushinbi01.Enabled = False
                dtpSaisyuKoushinbi02.Enabled = False
                rbtDate02.Checked = True
            End If
        End Sub

        Private Sub rbtDate02_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtDate02.CheckedChanged
            If rbtDate02.Checked = True Then
                dtpSaisyuKoushinbi01.Enabled = False
                dtpSaisyuKoushinbi02.Enabled = False
                rbtDate01.Checked = False
            Else
                rbtDate01.Checked = True
                dtpSaisyuKoushinbi01.Enabled = True
                dtpSaisyuKoushinbi02.Enabled = True
            End If
        End Sub
    End Class
End Namespace
