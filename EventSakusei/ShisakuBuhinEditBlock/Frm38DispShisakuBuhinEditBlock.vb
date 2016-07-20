Imports FarPoint.Win
Imports FarPoint.Win.Spread.CellType
Imports System.Reflection
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinEdit
Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports EventSakusei.ShisakuBuhinEditBlock.Ui
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports EventSakusei.ShisakuBuhinEditBlock.Export2Excel
Imports ShisakuCommon.Ui.Spd
Imports EventSakusei.ShisakuBuhinEditEvent.Dao
Imports EBom.Common
Imports EventSakusei.ShisakuBuhinEditBlock.Logic.Tenkai

Namespace ShisakuBuhinEditBlock
    ''' <summary>
    ''' 試作部品表 編集・改訂編集(ブロック)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm38DispShisakuBuhinEditBlock
#Region "local member"
        ''' <summary>モード</summary>
        Private _StrMode As String
        ''' <summary>イベントコード</summary>
        Private _StrEventCode As String
        ''' <summary>開発符号</summary>
        Private _StrFugo As String
        ''' <summary>イベント名</summary>
        Private _StrEventName As String
        ''' <summary>処置期限</summary>
        Private _StrDate As String
        ''' <summary>あとXX日</summary>
        Private _StrPeriod As String
        ''' <summary>設計課</summary>
        Private _StrDept As String
        ''' <summary>設計課表示順</summary>
        Private _StrDeptNo As String
        ''' <summary>SKE1最終抽出日</summary>
        Private _StrOutDate As String
        ''' <summary>権限</summary>
        Private _StrAuthority As String
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
            '↓↓2012/02/08 これを実装するとチェックボックスがチェック及び解除が効かなくなるのでやめる
            '代わりにスプレッドのクリックイベントで実装
            '2012/02/06 Ctrl+クリックで複数行の選択が可能に修正'
            'spdParts_Sheet1.OperationMode = Spread.OperationMode.ExtendedSelect
            InitializeHeader()
            '画面オープン時進度状況を設定する
            initShindo()
            ''ユーザ権限をチェック
            initUserKengen()
            'スプレッドデータを設定する
            initSpreadData()
        End Sub
#End Region

#Region "初期化"
        ''' <summary>
        ''' 画面ヘーダ部分の初期化
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitializeHeader()

            ShisakuFormUtil.setTitleVersion(Me)
            Label2.Text = "PG-ID：EVENT_EDIT38"
            ShisakuFormUtil.SetDateTimeNow(Me.LblDateNow, Me.LblTimeNow)
            ShisakuFormUtil.SetIdAndBuka(Me.LblCurrUserId, Me.LblCurrBukaName)
            '画面41番から戻ってきたときにどうするかわからないので一旦コメントアウト
            '   ShisakuFormUtil.SettingDefaultProperty(txtBlockNo)
            Me.lblKaihatuFugou.Text = StrFugo
            Me.lblIbentoName.Text = StrEventName
            Me.lblSyochikigen.Text = StrDate

            Me.lblSyochikigen2.Text = StrPeriod

            'ブロック№の設定
            ShisakuFormUtil.SettingDefaultProperty(txtBlockNo)
            txtBlockNo.MaxLength = 4

            '以下の文字なら太字にする。
            If lblSyochikigen2.Text = "もう過ぎてます。" Then
                lblSyochikigen2.Font = New Font("MS Gothic", 11, FontStyle.Bold)
            End If

            Me.lblSekkeika.Text = StrDeptNo
            Me.lblSKE1Saisyubi.Text = StrOutDate
            If StringUtil.IsEmpty(txtBlockNo.Text) Then
                btnCallDisable()
                btnViewDisable() 'add s.ota 2012-01-09 #2-1 閲覧機能
                btnExcelExportDisable() '使えない
                btnAlSaiTenkaiDisable() '使えない 2013/06/11  追加
            Else
                btnCallEnable()
                btnViewEnable() 'add s.ota 2012-01-09 #2-1 閲覧機能
                btnExcelExportEnable()  '使える
                'btnAlSaiTenkaiEnable()  '使える 2013/06/11  追加
            End If

            '*****完了閲覧モード対応*****
            If StrMode = ShishakuKanryoViewMode Then
                btnCall.Visible = False
                txtBlockNo.Enabled = False
            Else
                btnCall.Visible = True
                txtBlockNo.Enabled = True
            End If

        End Sub

        ''' <summary>
        ''' 画面オープン時進度状況を設定する
        ''' </summary>
        Private Sub initShindo()
            Dim kabetuJyoutaiDao As New ShisakuBuhinEditBlockDaoImpl
            Dim kabetuJyoutai = kabetuJyoutaiDao.GetKabetuJyoutai(StrEventCode, StrDept)

            If Not kabetuJyoutai Is Nothing Then
                Dim shisakuSekkeika As String = kabetuJyoutai.ShisakuSekkeika
                Dim totalBlock As Decimal = kabetuJyoutai.TotalBlock
                Dim totalJyouTai As Decimal = kabetuJyoutai.TotalJyoutai
                Dim totalSyouNinJyouTai As Decimal = kabetuJyoutai.TotalSyouninJyoutai
                Dim totalKaChouSyouNinJyouTai As Decimal = kabetuJyoutai.TotalKachouSyouninJyoutai

                lblBurokusu.Text = StrConv(totalBlock, VbStrConv.Wide)
                lblSyochi_kan.Text = StrConv(totalJyouTai, VbStrConv.Wide)
                lblNokori_kan.Text = StrConv(totalBlock - totalJyouTai, VbStrConv.Wide)
                lblSyochi_syounin1.Text = StrConv(totalSyouNinJyouTai, VbStrConv.Wide)
                lblNokori_syounin1.Text = StrConv(totalBlock - totalSyouNinJyouTai, VbStrConv.Wide)
                lblSyochi_syounin2.Text = StrConv(totalKaChouSyouNinJyouTai, VbStrConv.Wide)
                lblNokori_syounin2.Text = StrConv(totalBlock - totalKaChouSyouNinJyouTai, VbStrConv.Wide)
                If Not (totalJyouTai = 0) Then
                    lblkanryou.Text = Decimal.Round(totalJyouTai / totalBlock * 100, 0).ToString() + "%"
                    lblShindo_Kan.Text = StrConv(Decimal.Round(totalJyouTai / totalBlock * 100, 0).ToString(), VbStrConv.Wide) + "%"
                    lblShindo_syounin1.Text = StrConv(Decimal.Round(totalSyouNinJyouTai / totalBlock * 100, 0).ToString(), VbStrConv.Wide) + "%"
                    lblShindo_syounin2.Text = StrConv(Decimal.Round(totalKaChouSyouNinJyouTai / totalBlock * 100, 0).ToString(), VbStrConv.Wide) + "%"
                Else
                    lblkanryou.Text = "0%"
                    lblShindo_Kan.Text = "０%"
                    lblShindo_syounin1.Text = "０%"
                    lblShindo_syounin2.Text = "０%"
                End If

            End If
        End Sub
        ''' <summary>ユーザ権限をチェック</summary>
        Private Sub initUserKengen()
            Dim userKengenDao As IAuthorityUserDao = New AuthorityUserDaoImpl()
            isKachouKengen = userKengenDao.IsKachouShouninKengen(LoginInfo.Now.UserId)
        End Sub

        ''' <summary>
        ''' スプレッドデータを設定する
        ''' </summary>
        Private Sub initSpreadData()
            BlockSpreadUtil.InitializeFrm9(spdParts)
            Dim sheet = spdParts_Sheet1
            Dim index As Integer = 0
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
            Dim blockDao As IShisakuBuhinEditBlockDao = New ShisakuBuhinEditBlockDaoImpl()
            Dim blockList = blockDao.GetBlockSpreadList(StrEventCode, StrDept)
            BlockSpreadUtil.setRowCount(spdParts, blockList.Count)
            updateSpreadData(blockList)

        End Sub
#End Region

#Region "コンポーネント状態"
        ''' <summary>「呼び出し」ボタンが利用できる</summary>
        Private Sub btnCallEnable()
            btnCall.BackColor = Color.LightCyan
            btnCall.Enabled = True
        End Sub
        ''' <summary>「呼び出し」ボタンが利用できない</summary>
        Private Sub btnCallDisable()
            btnCall.BackColor = Color.White
            btnCall.Enabled = False
        End Sub
        ''' <summary>「閲覧」ボタンが利用できる</summary>
        Private Sub btnViewEnable()
            btnView.BackColor = Color.LightCyan
            btnView.Enabled = True
        End Sub
        ''' <summary>「閲覧」ボタンが利用できない</summary>
        Private Sub btnViewDisable()
            btnView.BackColor = Color.White
            btnView.Enabled = False
        End Sub
        ''' <summary>「指定ブロックEXCEL出力」ボタンが利用できる</summary>
        Private Sub btnExcelExportEnable()
            btnExcelExport.BackColor = Nothing
            btnExcelExport.Enabled = True
        End Sub
        ''' <summary>「指定ブロックEXCEL出力」ボタンが利用できない</summary>
        Private Sub btnExcelExportDisable()
            btnExcelExport.BackColor = Color.White
            btnExcelExport.Enabled = False
        End Sub

        ''' <summary>「ＡＬ再展開（仮名）」ボタンが利用できる</summary>
        Private Sub btnAlSaiTenkaiEnable()
            btnAlSaiTenkai.BackColor = Color.LightSalmon
            btnAlSaiTenkai.Enabled = True
        End Sub
        ''' <summary>「ＡＬ再展開（仮名）」ボタンが利用できない</summary>
        Private Sub btnAlSaiTenkaiDisable()
            btnAlSaiTenkai.BackColor = Color.White
            btnAlSaiTenkai.Enabled = False
        End Sub

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
            If (Not isKachouKengen) Then
                nothasKachouKengen(rowNo)
            End If

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
        ''' <summary>
        ''' 課長権限有るの場合
        ''' </summary>
        ''' <param name="rowNo">行番</param>
        ''' <remarks></remarks>
        Private Sub hasKachouKengen(ByVal rowNo As Integer)
            If m_spCom.GetCell(TAG_TANTO_SYOUNIN, rowNo).Text = TShisakuSekkeiBlockVoHelper.TantoJyoutai.APPROVAL Then
                m_spCom.GetCell(TAG_KACHOU_SYOUNIN, rowNo).Locked = False
                m_spCom.GetCell(TAG_KACHOU_SYOZOKU, rowNo).Locked = False
                m_spCom.GetCell(TAG_KACHOU_SYONINSYA, rowNo).Locked = False
                m_spCom.GetCell(TAG_KACHOU_SYONIN_HI, rowNo).Locked = False

                m_spCom.GetCell(TAG_KACHOU_SYOUNIN, rowNo).BackColor = Nothing
                m_spCom.GetCell(TAG_KACHOU_SYOZOKU, rowNo).BackColor = Nothing
                m_spCom.GetCell(TAG_KACHOU_SYONINSYA, rowNo).BackColor = Nothing
                m_spCom.GetCell(TAG_KACHOU_SYONIN_HI, rowNo).BackColor = Nothing
            End If
        End Sub

        ''' <summary>
        ''' 課長権限ない場合「課長承認」と「課長詳細」をロック
        ''' </summary>
        ''' <param name="rowNo">行番</param>
        ''' <remarks></remarks>
        Private Sub nothasKachouKengen(ByVal rowNo As Integer)
            m_spCom.GetCell(TAG_KACHOU_SYOUNIN, rowNo).Locked = True
            m_spCom.GetCell(TAG_KACHOU_SYOZOKU, rowNo).Locked = True
            m_spCom.GetCell(TAG_KACHOU_SYONINSYA, rowNo).Locked = True
            m_spCom.GetCell(TAG_KACHOU_SYONIN_HI, rowNo).Locked = True

            m_spCom.GetCell(TAG_KACHOU_SYOUNIN, rowNo).BackColor = Color.DarkGreen
            m_spCom.GetCell(TAG_KACHOU_SYOZOKU, rowNo).BackColor = Color.DarkGreen
            m_spCom.GetCell(TAG_KACHOU_SYONINSYA, rowNo).BackColor = Color.DarkGreen
            m_spCom.GetCell(TAG_KACHOU_SYONIN_HI, rowNo).BackColor = Color.DarkGreen
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
            setBlockFuyouCell(shisakuBuhinBlock.BlockFuyou, m_spCom.GetCell(TAG_FUYOU, rowNo))
            setJyoutai1Cell(shisakuBuhinBlock.Jyoutai, m_spCom.GetCell(TAG_JYOUTAI1, rowNo))
            setJyoutai2Cell(shisakuBuhinBlock.Jyoutai, m_spCom.GetCell(TAG_JYOUTAI2, rowNo))
            m_spCom.GetCell(TAG_NO, rowNo).Text = shisakuBuhinBlock.ShisakuBlockNo
            m_spCom.GetCell(TAG_KAITEI, rowNo).Text = shisakuBuhinBlock.ShisakuBlockNoKaiteiNo
            m_spCom.GetCell(TAG_UNIT, rowNo).Text = shisakuBuhinBlock.UnitKbn
            m_spCom.GetCell(TAG_MEISHOU, rowNo).Text = shisakuBuhinBlock.ShisakuBlockName
            m_spCom.GetCell(TAG_TANTOU, rowNo).Text = shisakuBuhinBlock.SyainName
            m_spCom.GetCell(TAG_TEL, rowNo).Text = shisakuBuhinBlock.TelNo
            m_spCom.GetCell(TAG_UPDATETIME, rowNo).Text = shisakuBuhinBlockHelp.SaisyuuKoushinbi & " " & _
                                                      shisakuBuhinBlockHelp.SaisyuuKoushinjikan
            setTantoShouninCell(shisakuBuhinBlock.TantoSyouninJyoutai, m_spCom.GetCell(TAG_TANTO_SYOUNIN, rowNo))

            '状態が完了以降ならブロック不要は使用不可にする。　2011/03/09 柳沼
            '   ただし課長承認済、ブロック不要＝TRUEは使用可にする。　2011/10/10　柳沼
            If shisakuBuhinBlock.Jyoutai = ShishakuSekkeiBlockStatusShouchiKanryou Or _
               shisakuBuhinBlock.TantoSyouninJyoutai = ShishakuSekkeiBlockStatusShounin1 Or _
               shisakuBuhinBlock.KachouSyouninJyoutai = ShishakuSekkeiBlockStatusShounin2 Then
                '課長承認までされていて、ブロック不要＝trueの場合、ブロック不要のチェックを外す。
                'ブロック不要を解除できるように　By　柳沼
                If shisakuBuhinBlock.KachouSyouninJyoutai = ShishakuSekkeiBlockStatusShounin2 And _
                    m_spCom.GetCell(TAG_FUYOU, rowNo).Value = True Then
                    m_spCom.GetCell(TAG_FUYOU, rowNo).Locked = False
                Else
                    m_spCom.GetCell(TAG_FUYOU, rowNo).Locked = True
                End If
            Else
                m_spCom.GetCell(TAG_FUYOU, rowNo).Locked = False
            End If

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




            checkKachouKengen(rowNo)
            'ブロック名称を入力可能にするため'
            m_spCom.GetCell(TAG_MEISHOU, rowNo).Locked = False
            '状態の隣のセルが書き込み可能になっていたため'
            m_spCom.GetCell(TAG_JYOUTAI2, rowNo).Locked = True

            '編集モードのときはチェックできない'
            If Not StrMode.Equals(ShishakuKaiteiHensyuMode) Then
                'm_spCom.GetCell(TAG_JYOUTAI1, rowNo).Locked = True
                'm_spCom.GetCell(TAG_JYOUTAI2, rowNo).Locked = True
                'm_spCom.GetCell(TAG_TANTO_SYOUNIN, rowNo).Locked = True
                'm_spCom.GetCell(TAG_KACHOU_SYOUNIN, rowNo).Locked = True
            End If

            '*****完了閲覧モード対応*****
            If StrMode = ShishakuKanryoViewMode Then
                m_spCom.GetCell(TAG_FUYOU, rowNo).Locked = True
                m_spCom.GetCell(TAG_JYOUTAI1, rowNo).Locked = True
                m_spCom.GetCell(TAG_JYOUTAI2, rowNo).Locked = True
                m_spCom.GetCell(TAG_TANTO_SYOUNIN, rowNo).Locked = True
                m_spCom.GetCell(TAG_KACHOU_SYOUNIN, rowNo).Locked = True
            End If

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
            '2013/06/24　AL再展開用の設定を追加
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
            '2013/06/24　AL再展開用の設定を追加
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

        ''' <summary>
        ''' 課長権限無ければ、課長承認がロック
        ''' </summary>
        ''' <param name="rowNo">spread行の番号</param>
        ''' <remarks></remarks>
        Private Sub checkKachouKengen(ByVal rowNo As Integer)
            If isKachouKengen = False Then
                nothasKachouKengen(rowNo)
            Else
                hasKachouKengen(rowNo)
            End If
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
        ''' 次画面オーブン前の処理（改訂Noに関して）
        ''' </summary>
        ''' <param name="blockNo">当り前Spread行のブロックNo</param>
        ''' <param name="dispMode">画面表示モード</param>
        ''' <remarks></remarks>
        Private Sub ShowFrm41ShisakuBuhinEdit(ByVal blockNo, ByVal dispMode)
			'2013/10/31 chg t.kakinuma 
			'対応概要：クリックイベント終了まえにダブルクリックイベントが呼び出される不具合に対応
			'対応内容：クリックイベントでスプレッドの値をtxtBlockNoに設定する処理が遅延するため
			'          ダブルクリックにて使用するメソッドのパラメータ取得先をtxtBlockNoから
			'           blockNo.ToStringに変更.

            Dim msgStr1 As String = ""
            Dim msgStr2 As String = ""
            Dim jyouTai As String = ""
            Dim isFinishedJyoutai As Boolean = False

            'TODO 強引すぎるのでちゃんとした処理にする'
            Dim blockDao As IShisakuBuhinEditBlockDao = New ShisakuBuhinEditBlockDaoImpl()
            Dim blockList = blockDao.GetBlockSpreadList(StrEventCode, StrDept)

            'ブロック情報がないとこの処理は走らないようにする'
            If Not blockList.Count = 0 Then

                '新規ブロック追加できるようにする'
                Dim rowNo As Integer = rowNoFind(blockNo.ToString)

                '2012/02/08 「rowNo > 0」ではrowNo=0のブロック（１行目のブロック）が対象外となっていた為修正
                If rowNo >= 0 Then
                    '2013/05/14ブロック不要かつ編集モード時にはRETURNする。
                    If IsBlockFuyouRow(rowNoFind(blockNo.ToString)) And dispMode = EDIT_MODE Then
                        Return
                    Else
                        If IsJyoutaiCheckedRow(rowNoFind(blockNo.ToString)) Then
                            msgStr1 = "該当ブロック№は完了しています。"
                            msgStr2 = "再編集しますか？"
                            jyouTai = TShisakuSekkeiBlockVoHelper.Jyoutai.FINISHED
                            isFinishedJyoutai = True
                        End If
                        If IsTantoSyouninCheckedRow(rowNoFind(blockNo.ToString)) Then
                            msgStr1 = "該当ブロック№は承認されています。"
                            msgStr2 = "承認を解除して再編集しますか？"
                            jyouTai = TShisakuSekkeiBlockVoHelper.TantoJyoutai.APPROVAL
                            isFinishedJyoutai = True
                        End If
                        If IsKachouSyouninCheckedRow(rowNoFind(blockNo.ToString)) Then
                            msgStr1 = "該当ブロック№は承認されています。"
                            msgStr2 = "承認を解除して再編集しますか？"
                            jyouTai = TShisakuSekkeiBlockVoHelper.KachouJyoutai.APPROVAL
                            isFinishedJyoutai = True
                        End If
                    End If
                End If


                '処理が長いとき用にカーソルを砂時計に変更'
                'Me.Cursor = Cursors.WaitCursor
                If dispMode = EDIT_MODE Then

                    If isFinishedJyoutai Then
                        If isFinishedJyoutai AndAlso frm01Kakunin.ConfirmOkCancel(msgStr1, msgStr2) <> MsgBoxResult.Ok Then
                            '---------------------------
                            '---------------------------
                            '２次改修
                            '排他情報をクリア
                            ExclusiveControl(StrEventCode, StrDept, blockNo)
                            '---------------------------
                            '---------------------------
                            Return
                        End If
                    End If

                    If Not (StringUtil.IsEmpty(jyouTai)) Then
                        If StrMode.Equals(ShishakuKaiteiHensyuMode) Then
                            Dim kaiteiNo = m_spCom.GetValue(TAG_KAITEI, rowNoFind(blockNo.ToString))
                            If jyouTai.Equals(TShisakuSekkeiBlockVoHelper.KachouJyoutai.APPROVAL) Then
                                AddMode(rowNoFind(blockNo.ToString))
                            ElseIf (jyouTai.Equals(TShisakuSekkeiBlockVoHelper.Jyoutai.FINISHED) _
                                    OrElse jyouTai.Equals(TShisakuSekkeiBlockVoHelper.TantoJyoutai.APPROVAL) _
                                    ) AndAlso kaiteiNo.Equals(TShisakuSekkeiBlockVoHelper.ShisakuBlockNoKaiteiNo.DEFAULT_VALUE) Then
                                AddMode(rowNoFind(blockNo.ToString))
                            Else
                                UpdateMode(rowNoFind(blockNo.ToString))
                            End If
                            '編集モード時でもUpdateModeを使用するため'
                        ElseIf StrMode.Equals(ShishakuHensyuMode) Then
                            If isFinishedJyoutai = True Then
                                UpdateMode(rowNoFind(blockNo.ToString))
                            End If
                        End If
                    ElseIf (StringUtil.IsEmpty(jyouTai)) Then
                        '改訂編集モードの時'
                        If StrMode.Equals(ShishakuKaiteiHensyuMode) Then
                            '改訂Noが000のとき改訂Noを繰り上げる'
                            If Not rowNoFind(blockNo.ToString) = -1 Then
                                If m_spCom.GetValue(TAG_KAITEI, rowNoFind(blockNo.ToString)) = "000" Then
                                    AddMode(rowNoFind(blockNo.ToString))
                                    '改訂Noが001以上かつ課長承認が完了しているとき改訂Noを繰り上げる'
                                ElseIf Integer.Parse(m_spCom.GetValue(TAG_KAITEI, rowNoFind(blockNo.ToString))) >= "001" And IsKachouSyouninCheckedRow(rowNoFind(txtBlockNo.Text)) Then
                                    AddMode(rowNoFind(blockNo.ToString))
                                End If
                            Else
                                NewAddMode()
                            End If
                        End If
                    End If
                End If
            End If
            Dim KSMode As Integer = 0
            If StrMode.Equals(ShishakuKaiteiHensyuMode) Then
                '改訂編集から来た'
                KSMode = 1
            End If

            Execute()

            'Me.Hide()

            Using frm41 As New Frm41DispShisakuBuhinEdit00(Me, StrEventCode, StrDept, blockNo, dispMode, KSMode, StrMode)   '*****完了閲覧モード対応*****
                '画面を開くときにカーソルを元に戻す'
                'Me.Cursor = Cursors.WaitCursor
                frm41.ShowDialog()
                '前画面で選択されていたブロックを選択
                Me.txtBlockNo.Text = frm41.cmbBlockNo.SelectedValue
            End Using

            'Me.Show()
            Execute()
        End Sub

        ''' <summary>
        ''' 次画面オーブン前の処理（改訂Noに関して）
        ''' </summary>
        ''' <param name="blockNo">当り前Spread行のブロックNo</param>
        ''' <remarks></remarks>
        Public Function ShowFrm41ShisakuBuhinEditFromFrm41(ByVal blockNo As String) As Boolean
            Dim msgStr1 As String = ""
            Dim msgStr2 As String = ""
            Dim jyouTai As String = ""
            Dim isFinishedJyoutai As Boolean = False

            'TODO 強引すぎるのでちゃんとした処理にする'
            Dim blockDao As IShisakuBuhinEditBlockDao = New ShisakuBuhinEditBlockDaoImpl()
            Dim blockList = blockDao.GetBlockSpreadList(StrEventCode, StrDept)

            'ブロック情報がないとこの処理は走らないようにする'
            If Not blockList.Count = 0 Then

                If IsBlockFuyouRow(rowNoFind(blockNo)) Then
                    MsgBox("指定されたブロックは不要ブロックです。")
                    Return False
                Else
                    If IsJyoutaiCheckedRow(rowNoFind(blockNo)) Then
                        msgStr1 = "該当ブロック№は完了しています。"
                        msgStr2 = "再編集しますか？"
                        jyouTai = TShisakuSekkeiBlockVoHelper.Jyoutai.FINISHED
                        isFinishedJyoutai = True
                    End If
                    If IsTantoSyouninCheckedRow(rowNoFind(blockNo)) Then
                        msgStr1 = "該当ブロック№は承認されています。"
                        msgStr2 = "承認を解除して再編集しますか？"
                        jyouTai = TShisakuSekkeiBlockVoHelper.TantoJyoutai.APPROVAL
                        isFinishedJyoutai = True
                    End If
                    If IsKachouSyouninCheckedRow(rowNoFind(blockNo)) Then
                        msgStr1 = "該当ブロック№は承認されています。"
                        msgStr2 = "承認を解除して再編集しますか？"
                        jyouTai = TShisakuSekkeiBlockVoHelper.KachouJyoutai.APPROVAL
                        isFinishedJyoutai = True
                    End If
                End If


                '処理が長いとき用にカーソルを砂時計に変更'
                'Me.Cursor = Cursors.WaitCursor
                If isFinishedJyoutai Then
                    If isFinishedJyoutai AndAlso frm01Kakunin.ConfirmOkCancel(msgStr1, msgStr2) <> MsgBoxResult.Ok Then
                        Return False
                    End If
                End If

                If Not (StringUtil.IsEmpty(jyouTai)) Then
                    If StrMode.Equals(ShishakuKaiteiHensyuMode) Then
                        Dim kaiteiNo = m_spCom.GetValue(TAG_KAITEI, rowNoFind(blockNo))
                        If jyouTai.Equals(TShisakuSekkeiBlockVoHelper.KachouJyoutai.APPROVAL) Then
                            AddMode(rowNoFind(blockNo))
                        ElseIf (jyouTai.Equals(TShisakuSekkeiBlockVoHelper.Jyoutai.FINISHED) _
                                OrElse jyouTai.Equals(TShisakuSekkeiBlockVoHelper.TantoJyoutai.APPROVAL) _
                                ) AndAlso kaiteiNo.Equals(TShisakuSekkeiBlockVoHelper.ShisakuBlockNoKaiteiNo.DEFAULT_VALUE) Then
                            AddMode(rowNoFind(blockNo))
                        Else
                            UpdateMode(rowNoFind(blockNo))
                        End If
                        '編集モード時でもUpdateModeを使用するため'
                    ElseIf StrMode.Equals(ShishakuHensyuMode) Then
                        If isFinishedJyoutai = True Then
                            UpdateMode(rowNoFind(blockNo))
                        End If
                    End If
                ElseIf (StringUtil.IsEmpty(jyouTai)) Then
                    '改訂編集モードの時'
                    If StrMode.Equals(ShishakuKaiteiHensyuMode) Then
                        '改訂Noが000のとき改訂Noを繰り上げる'
                        If Not rowNoFind(blockNo) = -1 Then
                            If m_spCom.GetValue(TAG_KAITEI, rowNoFind(blockNo)) = "000" Then
                                '-------------------------------------
                                '２次改修
                                'パラメーターに変更
                                '   じゃないとブロック編集画面でブロックを変更して編集ボタン押したときに改訂が上がらない。
                                'AddMode(rowNoFind(txtBlockNo.Text))
                                AddMode(rowNoFind(blockNo))
                                '-------------------------------------

                                '改訂Noが001以上かつ課長承認が完了しているとき改訂Noを繰り上げる'
                            ElseIf Integer.Parse(m_spCom.GetValue(TAG_KAITEI, rowNoFind(blockNo))) >= "001" And IsKachouSyouninCheckedRow(rowNoFind(blockNo)) Then
                                AddMode(rowNoFind(blockNo))
                            End If
                        Else
                            NewAddMode()
                        End If
                    End If
                End If
            Else
                MsgBox("指定されたブロックは不要ブロックです。")
                Return False
            End If

            Execute()
            Return True

        End Function


        ''' <summary>
        ''' DBレコードが更新の場合
        ''' </summary>
        ''' <param name="rowNo"></param>
        ''' <remarks></remarks>
        Private Sub UpdateMode(ByVal rowNo As Integer)
            Dim cntBlock = New TShisakuSekkeiBlockVo
            cntBlock.ShisakuEventCode = StrEventCode
            cntBlock.ShisakuBukaCode = StrDept
            cntBlock.ShisakuBlockNo = m_spCom.GetValue(TAG_NO, rowNo)
            cntBlock.ShisakuBlockNoKaiteiNo = m_spCom.GetValue(TAG_KAITEI, rowNo)

            Dim shisakuBlockDao As IShisakuBuhinEditBlockDao = New ShisakuBuhinEditBlockDaoImpl
            shisakuBlockDao.UpdateByPkMove(cntBlock)

        End Sub
        '仕様変更により承認欄の中身を空にする処理を追加'
        Private Sub UpdateMode2(ByVal rowNo As Integer)
            Dim cntBlock = New TShisakuSekkeiBlockVo
            cntBlock.ShisakuEventCode = StrEventCode
            cntBlock.ShisakuBukaCode = StrDept
            cntBlock.ShisakuBlockNo = m_spCom.GetValue(TAG_NO, rowNo)
            cntBlock.ShisakuBlockNoKaiteiNo = m_spCom.GetValue(TAG_KAITEI, rowNo)

            Dim shisakuBlockDao As IShisakuBuhinEditBlockDao = New ShisakuBuhinEditBlockDaoImpl
            shisakuBlockDao.UpdateByPkMove2(cntBlock)
        End Sub

        ''' <summary>
        ''' DBレコードに改訂No.を増える場合
        ''' </summary>
        ''' <param name="rowNo"></param>
        ''' <remarks></remarks>
        Private Sub AddMode(ByVal rowNo As Integer)
            Dim cntBlock = New TShisakuSekkeiBlockVo
            cntBlock.ShisakuEventCode = StrEventCode
            cntBlock.ShisakuBukaCode = StrDept
            cntBlock.ShisakuBlockNo = m_spCom.GetValue(TAG_NO, rowNo)
            cntBlock.ShisakuBlockNoKaiteiNo = m_spCom.GetValue(TAG_KAITEI, rowNo)

            Dim shisakuBlockDao As IShisakuBuhinEditBlockDao = New ShisakuBuhinEditBlockDaoImpl
            shisakuBlockDao.InsertByUpdateKaiteiNo(cntBlock)
        End Sub

        Private Sub AddMode2(ByVal rowNo As Integer)
            Dim cntBlock = New TShisakuSekkeiBlockVo
            cntBlock.ShisakuEventCode = StrEventCode
            cntBlock.ShisakuBukaCode = StrDept
            cntBlock.ShisakuBlockNo = m_spCom.GetValue(TAG_NO, rowNo)
            cntBlock.ShisakuBlockNoKaiteiNo = m_spCom.GetValue(TAG_KAITEI, rowNo)

            Dim shisakuBlockDao As IShisakuBuhinEditBlockDao = New ShisakuBuhinEditBlockDaoImpl
            shisakuBlockDao.InsertByUpdateKaiteiNoFuyou(cntBlock)
        End Sub



        ''' <summary>
        ''' 新規ブロック追加
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub NewAddMode()
            Dim cntBlock = New TShisakuSekkeiBlockVo
            cntBlock.ShisakuEventCode = StrEventCode
            cntBlock.ShisakuBukaCode = StrDept
            cntBlock.ShisakuBlockNo = txtBlockNo.Text
            cntBlock.ShisakuBlockNoKaiteiNo = "001"
            Dim shisakuBlockDao As IShisakuBuhinEditBlockDao = New ShisakuBuhinEditBlockDaoImpl
            shisakuBlockDao.InsertByUpdateKaiteiNo(cntBlock)

        End Sub

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

#Region "local memberSetとGet"
        ''' <summary>モード</summary>
        ''' <value>モード</value>
        ''' <returns>モード</returns>
        Public Property StrMode() As String
            Get
                Return _StrMode
            End Get
            Set(ByVal value As String)
                _StrMode = value
            End Set
        End Property

        ''' <summary>イベントコード</summary>
        ''' <value>イベントコード</value>
        ''' <returns>イベントコード</returns>
        Public Property StrEventCode() As String
            Get
                Return _StrEventCode
            End Get
            Set(ByVal value As String)
                _StrEventCode = value
            End Set
        End Property

        ''' <summary>開発符号</summary>
        ''' <value>開発符号</value>
        ''' <returns>開発符号</returns>
        Public Property StrFugo() As String
            Get
                Return _StrFugo
            End Get
            Set(ByVal value As String)
                _StrFugo = value
            End Set
        End Property

        ''' <summary>イベント名</summary>
        ''' <value>イベント名</value>
        ''' <returns>イベント名</returns>
        Public Property StrEventName() As String
            Get
                Return _StrEventName
            End Get
            Set(ByVal value As String)
                _StrEventName = value
            End Set
        End Property

        ''' <summary>処置期限</summary>
        ''' <value>処置期限</value>
        ''' <returns>処置期限</returns>
        Public Property StrDate() As String
            Get
                Return _StrDate
            End Get
            Set(ByVal value As String)
                _StrDate = value
            End Set
        End Property

        ''' <summary>あとXX日</summary>
        ''' <value>あとXX日</value>
        ''' <returns>あとXX日</returns>
        Public Property StrPeriod() As String
            Get
                Return _StrPeriod
            End Get
            Set(ByVal value As String)
                _StrPeriod = value
            End Set
        End Property

        ''' <summary>部課コード</summary>
        ''' <value>部課コード</value>
        ''' <returns>部課コード</returns>
        Public Property StrDept() As String
            Get
                Return _StrDept
            End Get
            Set(ByVal value As String)
                _StrDept = value
            End Set
        End Property

        ''' <summary>課略称</summary>
        ''' <value>課略称</value>
        ''' <returns>課略称</returns>
        Public Property StrDeptNo() As String
            Get
                Return _StrDeptNo
            End Get
            Set(ByVal value As String)
                _StrDeptNo = value
            End Set
        End Property

        ''' <summary>SKE1最終抽出日</summary>
        ''' <value>SKE1最終抽出日</value>
        ''' <returns>SKE1最終抽出日</returns>
        Public Property StrOutDate() As String
            Get
                Return _StrOutDate
            End Get
            Set(ByVal value As String)
                _StrOutDate = value
            End Set
        End Property

        ''' <summary>権限</summary>
        ''' <value>権限</value>
        ''' <returns>権限</returns>
        Public Property StrAuthority() As String
            Get
                Return _StrAuthority
            End Get
            Set(ByVal value As String)
                _StrAuthority = value
            End Set
        End Property
#End Region

#Region "event handeler"
        ''' <summary>
        ''' spreadにクリック処理
        ''' </summary>
        ''' <param name="sender">spread</param>
        ''' <param name="e">spreadのクリックイベント</param>
        ''' <remarks></remarks>
        Private Sub SpdParts_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellClick

            '２次改修
            '   試作イベント情報のステータスが正しいか確認する。
            '   ＮＧの場合、処理終了。
            '   編集モード：STATUS＝２１（設計メンテ中）
            '   改訂編集モード：STATUS＝２３（改訂受付中）
            '   完了閲覧モード：STATUS＝２４（完了）
            Dim flgStatusCheck As Boolean = StatusCheck(StrMode)
            If flgStatusCheck = False Then
                frm37ParaModori = "close"
                Me.Close()
                Exit Sub
            End If


            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor
            Dim flgRowSelect As Boolean
            '2012/02/07
            'RowHeaderクリック時にスプレッドのモードを拡張選択モードに変更することによりCtrl＋クリックで複数行選択を可能にする。
            'RowHeader以外をクリックした場合は通常モードに戻す。
            If e.RowHeader Then
                spdParts_Sheet1.OperationMode = Spread.OperationMode.ExtendedSelect
                flgRowSelect = True
            Else
                spdParts_Sheet1.OperationMode = Spread.OperationMode.Normal
                flgRowSelect = False
            End If
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
                If (e.Row = 2) Then
                    '*****完了閲覧モード対応*****
                    If StrMode = ShishakuKanryoViewMode Then
                        '何もしない
                        Return
                    End If
                    Dim rowCount = spdParts_Sheet1.RowCount
                    Dim columnKbn = ""
                    If (e.Column = m_spCom.GetColFromTag(TAG_TANTO_SYOUNIN).Index) Then
                        '2012-01-07 add s.ota #2-3
                        If frm01Kakunin.ConfirmOkCancel("対象全てを承認します。よろしいですか？") = MsgBoxResult.Cancel Then
                            Return
                        End If
                        columnKbn = TAG_TANTO_SYOUNIN
                    ElseIf (e.Column = m_spCom.GetColFromTag(TAG_KACHOU_SYOUNIN).Index) Then
                        '2012-01-07 add s.ota #2-3
                        If frm01Kakunin.ConfirmOkCancel("対象全てを承認します。よろしいですか？") = MsgBoxResult.Cancel Then
                            Return
                        End If
                        columnKbn = TAG_KACHOU_SYOUNIN
                    End If
                    'Emptyの場合処理しない。　※「全て選択」ボタンの隣のヘッダーセルクリック時の例外防止
                    If Not StringUtil.IsEmpty(columnKbn) Then
                        Dim i As Integer
                        For i = 0 To rowCount - 1
                            Dim cntCell = m_spCom.GetCell(columnKbn, i)
                            If (Not cntCell.Locked) Then
                                cntCell.Value = True
                                Dim spreadView = New Spread.SpreadView(sender)
                                Dim clickEvent = New Spread.EditorNotifyEventArgs(spreadView, Me.Controls.Owner, i, m_spCom.GetCell(columnKbn, i).Column.Index)
                                Spread_OnButtonClicked(sender, clickEvent)
                            End If
                        Next
                    End If
                End If
            Else

                '不要ブロックのチェック有無を確認し、チェック無しなら画面上部のブロックへ値を設定する。
                If IsBlockFuyouRow(e.Row) = False Then
                    ' 選択範囲の情報を表示します。
                    txtBlockNo.Text = m_spCom.GetValue(TAG_NO, e.Row)
                    'ブロック不要にチェックが無い場合、呼び出しボタンが利用できる'
                    btnCallEnable()
                    'ブロック不要にチェックが無い場合、閲覧しボタンが利用できる  'add s.ota 2012-01-09 #2-1 閲覧機能
                    btnViewEnable()
                    '「指定ブロックEXCEL出力」ボタンも利用できる
                    btnExcelExportEnable()
                    '下記のボタンも利用できる   '2013/06/11 追加
                    '   但し完了閲覧モードの場合は利用できない。
                    If StrMode = ShishakuKanryoViewMode Then
                        btnAlSaiTenkaiDisable()
                    Else
                        If flgRowSelect = True Then
                            btnAlSaiTenkaiEnable()
                        Else
                            btnAlSaiTenkaiDisable()
                        End If
                    End If
                Else

                    '2013/05/14　ブロック不要でも表示する。
                    ' ブロック不要行をクリックした場合、ブランクを設定する。
                    txtBlockNo.Text = m_spCom.GetValue(TAG_NO, e.Row)

                    'ブロック不要時、呼び出しボタンは使えない'
                    btnCallDisable()

                    '2013/05/14　ブロック不要でも閲覧ボタンは使用できる。
                    ''ブロック不要時、閲覧ボタンは使えない 'add s.ota 2012-01-09 #2-1 閲覧機能
                    'btnViewDisable()
                    btnViewEnable()

                    '「指定ブロックEXCEL出力」ボタンも使えない
                    btnExcelExportDisable()
                    '下記のボタンは使えない   '2013/06/11 追加
                    btnAlSaiTenkaiDisable()
                End If

                '-------------------------------------------------------------------------------------------------------
                '２次改修
                '　改訂№がクリックされたら改訂履歴の情報をメッセージボックスへ表示する。
                If (e.Column = m_spCom.GetColFromTag(TAG_KAITEI).Index) Then

                    '改訂内容を取得
                    Dim blockKaiteiDao As IShisakuBuhinEditBlockDao = New ShisakuBuhinEditBlockDaoImpl()
                    Dim blockKaiteiList = blockKaiteiDao.GetBlockKaiteiList(StrEventCode, m_spCom.GetValue(TAG_NO, e.Row))
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
                '-------------------------------------------------------------------------------------------------------



            End If

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

        End Sub

        ''' <summary>
        ''' spreadにダブルクリック処理
        ''' </summary>
        ''' <param name="sender">spread</param>
        ''' <param name="e">spreadのダブルクリックイベント</param>
        ''' <remarks></remarks>
        Private Sub SpdParts_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellDoubleClick
            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            If Not e.ColumnHeader Then
                Dim cntBlockNo As String = m_spCom.GetValue(TAG_NO, e.Row)
                Dim sheet As Spread.SheetView = spdParts.ActiveSheet

                'ブロック名称以外
                ' 選択セルの場所を特定します。
                ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
                ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ列インデックス
                If Convert.ToString(sheet.Columns(ParaActColIdx).Tag) <> TAG_MEISHOU Then

                    '不要ブロックのチェック有無を確認し、チェック無しなら次画面へ遷移可能とする。
                    If (Not IsBlockFuyouRow(e.Row)) Then
                        If Not StringUtil.IsEmpty(cntBlockNo) Then
                            Me.Hide()
                            '*****完了閲覧モード対応*****
                            If StrMode = ShishakuKanryoViewMode Then
                                ShowFrm41ShisakuBuhinEdit(cntBlockNo, VIEW_MODE)
                            Else

                                '12/6/26　SKE1森様と打合せし、イベントコード編集中でも部品表の編集は可能とする様に決定。
                                '           仕様変更があった場合、以下のコメントを解除する。
                                ''------------------------------------------------------------------------------------------------------
                                ''最初に試作イベントコードが編集中かチェックする。
                                ''------------------------------------------------------------------------------------------------------
                                ''２次改修

                                '------------------------------------------------------------------------------------------------------
                                '初期設定
                                '他のユーザーが編集中か確認する。
                                Dim tExclusiveControlEventDaoImpl As ExclusiveControlEventDao = New ExclusiveControlEventImpl
                                Dim tExclusiveControlEventVo As New TExclusiveControlEventVo()
                                Dim isExclusive As Boolean  'true=編集OK、false=編集NG
                                Dim isReg As Boolean  'true=追加、false=更新
                                Dim tanTousya As String = Nothing
                                '担当者名取得用に。
                                Dim getDate As New EditBlock2ExcelDaoImpl()
                                '------------------------------------------------------------------------------------------------------


                                ''排他制御イベント情報から試作イベントコードが登録されているかチェック。
                                'tExclusiveControlEventVo = tExclusiveControlEventDaoImpl.FindByPk(StrEventCode)

                                ''イベント情報の排他チェック
                                'If IsNothing(tExclusiveControlEventVo) Then
                                '    MsgBox("選択したイベントは誰も使用していません。")

                                '------------------------------------------------------------------------------------------------------
                                '------------------------------------------------------------------------------------------------------
                                '次に試作イベントコード、設計課、ブロックの部品表が編集中かチェックする。
                                '------------------------------------------------------------------------------------------------------
                                '２次改修

                                '他のユーザーが編集中か確認する。
                                Dim tExclusiveControlBlockDaoImpl As ExclusiveControlBlockDao = New ExclusiveControlBlockImpl
                                Dim tExclusiveControlBlockVo As New TExclusiveControlBlockVo()

                                '排他制御ブロック情報から試作イベントコード、部課コード、ブロック№が登録されているかチェック。
                                tExclusiveControlBlockVo = tExclusiveControlBlockDaoImpl.FindByPk(StrEventCode, StrDept, cntBlockNo)

                                'AL再展開中かもチェック。
                                If IsNothing(tExclusiveControlBlockVo) Then
                                    tExclusiveControlBlockVo = tExclusiveControlBlockDaoImpl.FindByPk(StrEventCode, StrDept, "ZZZZ")
                                    '他のユーザーがＡＬ再展開中なら警告を表示して終了。
                                    If Not IsNothing(tExclusiveControlBlockVo) Then
                                        MsgBox("ＡＬ再展開中です。" & vbCrLf & vbCrLf & _
                                               "暫くお待ちいただき再度実行をお願い致します。" & vbCrLf & vbCrLf & _
                                                "担当者「" & tExclusiveControlBlockVo.EditUserId & _
                                                         "：" & tanTousya & "」", MsgBoxStyle.OkOnly, "警告")
                                        'ＡＬ再展開中なら処理中断
                                        Exit Sub
                                    End If
                                End If

                                If IsNothing(tExclusiveControlBlockVo) Then
                                    'MsgBox("選択したブロックは誰も使用していません。")
                                    isExclusive = True '編集OK
                                    isReg = True '追加モード
                                Else
                                    isReg = False '更新モード
                                    'レコードが有る場合、ログインユーザーと編集者コードを比較する。
                                    '担当者名を取得する'
                                    tanTousya = getDate.FindByShainName(tExclusiveControlBlockVo.EditUserId)
                                    '同じなら編集OK。
                                    If tExclusiveControlBlockVo.EditUserId = LoginInfo.Now.UserId Then
                                        isExclusive = True '編集OK
                                        MsgBox("選択したブロック下記の方が編集中に異常終了したもようです。編集できます" & vbCrLf & vbCrLf & _
                                                "担当者「" & tExclusiveControlBlockVo.EditUserId & _
                                                         "：" & tanTousya & "」", MsgBoxStyle.OkOnly, "警告")
                                    Else
                                        '違うなら編集NG。
                                        isExclusive = False '編集NG
                                    End If
                                End If

                                '排他チェック
                                If isExclusive = True Then
                                    '他のユーザーが編集していなければ排他制御ブロックテーブルにレコード更新。
                                    RegisterMain(StrEventCode, StrDept, cntBlockNo, isReg)

                                    '続けて処理を続行。
                                    '------------------------------------------------
                                    ShowFrm41ShisakuBuhinEdit(cntBlockNo, EDIT_MODE)
                                    '------------------------------------------------


                                Else
                                    '他のユーザーが編集していれば警告を表示して終了。
                                    MsgBox("選択したブロックは編集中です。" & vbCrLf & vbCrLf & _
                                            "担当者「" & tExclusiveControlBlockVo.EditUserId & _
                                                     "：" & tanTousya & "」", MsgBoxStyle.OkOnly, "警告")
                                End If
                                '------------------------------------------------------------------------------------------------------
                                '------------------------------------------------------------------------------------------------------

                                '    Else
                                '    '他のユーザーが編集していれば警告を表示して終了。
                                '    MsgBox("選択したイベントは編集中です。" & vbCrLf & vbCrLf & _
                                '            "担当者「" & TExclusiveControlEventVo.EditUserId & _
                                '                     "：" & tanTousya & "」", MsgBoxStyle.OkOnly, "警告")
                                'End If


                                End If
                                Me.Show()
                                '---------------------------------------------------------------
                                '２次改修で追加。
                                '   排他処理でステータスが更新されていた場合、自画面も閉じる。
                                '以下のパラメータの場合自分も閉じる。
                                If frm37ParaModori = "close" Then
                                    Me.Close()
                                    Exit Sub
                                End If
                                '---------------------------------------------------------------
                        End If
                    End If

                End If

            End If

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

        End Sub

        Private Sub spdParts_LeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles spdParts.LeaveCell

            Dim shisakuSekkiBolckForUpdate As New TShisakuSekkeiBlockVo
            shisakuSekkiBolckForUpdate.ShisakuEventCode = StrEventCode
            shisakuSekkiBolckForUpdate.ShisakuBukaCode = StrDept
            shisakuSekkiBolckForUpdate.ShisakuBlockNo = m_spCom.GetValue(TAG_NO, e.Row)
            shisakuSekkiBolckForUpdate.ShisakuBlockNoKaiteiNo = m_spCom.GetValue(TAG_KAITEI, e.Row)

            'ブロック名称をDBに登録させるため'
            shisakuSekkiBolckForUpdate.ShisakuBlockName = m_spCom.GetValue(TAG_MEISHOU, e.Row)

            Dim shisakuBlockDao As IShisakuBuhinEditBlockDao = New ShisakuBuhinEditBlockDaoImpl
            Dim updatedShisakuBlock = shisakuBlockDao.UpdateByPkHasReturn(shisakuSekkiBolckForUpdate)
            Me.UpdateSpreadRow(updatedShisakuBlock, e.Row)

        End Sub

        ''' <summary>
        ''' ブロックNoのテキストボックスに値が変更の場合
        ''' </summary>
        ''' <param name="sender">TextBox</param>
        ''' <param name="e">TextChanged Event</param>
        ''' <remarks></remarks>
        Private Sub TxtBlockNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBlockNo.TextChanged
            'ブロックNoが反映されていない・・・'
            If StringUtil.IsEmpty(txtBlockNo.Text) Then
                btnCallDisable()
                btnExcelExportDisable() '使えない
                btnAlSaiTenkaiDisable() '使えない   2013/06/11 追加
            Else
                btnCallEnable()
                btnExcelExportEnable()  '使える
                'btnAlSaiTenkaiEnable()  '使える 2013/06/11 追加
            End If
        End Sub


        ''' <summary>
        ''' 排他制御ブロック情報の更新処理
        ''' </summary>
        ''' <param name="StrEventCode">イベントコード</param>
        ''' <param name="StrDept">部課コード</param>
        ''' <param name="StrBlockNo">ブロック№</param>
        ''' <param name="isMode">「登録」の場合、true</param>
        ''' <remarks></remarks>
        Private Sub RegisterMain(ByVal StrEventCode As String, _
                                 ByVal StrDept As String, _
                                 ByVal StrBlockNo As String, _
                                 ByVal isMode As Boolean)

            Dim aShisakuDate As New ShisakuDate
            Dim blockVo As New TExclusiveControlBlockVo
            Dim blockDao As ExclusiveControlBlockDao = New ExclusiveControlBlockImpl

            'KEY情報
            blockVo.ShisakuEventCode = StrEventCode
            blockVo.ShisakuBukaCode = StrDept
            blockVo.ShisakuBlockNo = StrBlockNo
            '編集情報
            blockVo.EditUserId = LoginInfo.Now.UserId
            blockVo.EditDate = aShisakuDate.CurrentDateDbFormat
            blockVo.EditTime = aShisakuDate.CurrentTimeDbFormat
            '作成情報
            blockVo.CreatedUserId = LoginInfo.Now.UserId
            blockVo.CreatedDate = aShisakuDate.CurrentDateDbFormat
            blockVo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
            '更新情報
            blockVo.UpdatedUserId = LoginInfo.Now.UserId
            blockVo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
            blockVo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

            '追加モードの場合、インサートする。
            If isMode = True Then
                blockDao.InsetByPk(blockVo)
            Else
                blockDao.UpdateByPk(blockVo)
            End If

        End Sub

        ''' <summary>
        ''' 「呼び出し」ボタンクリックの処理
        ''' </summary>
        ''' <param name="sender">ボタン</param>
        ''' <param name="e">ボタンクリックイベント</param>
        ''' <remarks></remarks>
        Private Sub BtnCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCall.Click

            '２次改修
            '   試作イベント情報のステータスが正しいか確認する。
            '   ＮＧの場合、処理終了。
            '   編集モード：STATUS＝２１（設計メンテ中）
            '   改訂編集モード：STATUS＝２３（改訂受付中）
            '   完了閲覧モード：STATUS＝２４（完了）
            Dim flgStatusCheck As Boolean = StatusCheck(StrMode)
            If flgStatusCheck = False Then
                '以下のパラメータの場合、親画面も画面を閉じる。
                frm37ParaModori = "close"
                Me.Close()
                Exit Sub
            End If



            '12/6/26　SKE1森様と打合せし、イベントコード編集中でも部品表の編集は可能とする様に決定。
            '           仕様変更があった場合、以下のコメントを解除する。
            ''------------------------------------------------------------------------------------------------------
            ''最初に試作イベントコードが編集中かチェックする。
            ''------------------------------------------------------------------------------------------------------
            ''２次改修

            '------------------------------------------------------------------------------------------------------
            '初期設定
            '他のユーザーが編集中か確認する。
            Dim tExclusiveControlEventDaoImpl As ExclusiveControlEventDao = New ExclusiveControlEventImpl
            Dim tExclusiveControlEventVo As New TExclusiveControlEventVo()
            Dim isExclusive As Boolean  'true=編集OK、false=編集NG
            Dim isReg As Boolean  'true=追加、false=更新
            Dim tanTousya As String = Nothing
            '担当者名取得用に。
            Dim getDate As New EditBlock2ExcelDaoImpl()
            '------------------------------------------------------------------------------------------------------

            ''排他制御イベント情報から試作イベントコードが登録されているかチェック。
            'tExclusiveControlEventVo = tExclusiveControlEventDaoImpl.FindByPk(StrEventCode)

            'If IsNothing(tExclusiveControlEventVo) Then
            '    MsgBox("選択したイベントは誰も使用していません。")
            '    isReg = True '追加モード
            'Else
            '    isReg = False '更新モード
            'End If

            ''排他チェック
            'If isReg = False Then
            '    '他のユーザーが編集していれば警告を表示して終了。
            '    MsgBox("選択したイベントは編集中です。" & vbCrLf & vbCrLf & _
            '            "担当者「" & tExclusiveControlEventVo.EditUserId & _
            '                     "：" & tanTousya & "」", MsgBoxStyle.OkOnly, "警告")
            '    Return
            'End If



            '------------------------------------------------------------------------------------------------------
            '次に試作イベントコード、設計課、ブロックの部品表が編集中かチェックする。
            '------------------------------------------------------------------------------------------------------
            '２次改修

            '他のユーザーが編集中か確認する。
            Dim tExclusiveControlBlockDaoImpl As ExclusiveControlBlockDao = New ExclusiveControlBlockImpl
            Dim tExclusiveControlBlockVo As New TExclusiveControlBlockVo()

            '排他制御ブロック情報から試作イベントコード、部課コード、ブロック№が登録されているかチェック。
            tExclusiveControlBlockVo = tExclusiveControlBlockDaoImpl.FindByPk(StrEventCode, StrDept, Me.txtBlockNo.Text)

            'AL再展開中かもチェック。
            If IsNothing(tExclusiveControlBlockVo) Then
                tExclusiveControlBlockVo = tExclusiveControlBlockDaoImpl.FindByPk(StrEventCode, StrDept, "ZZZZ")
                '他のユーザーがＡＬ再展開中なら警告を表示して終了。
                If Not IsNothing(tExclusiveControlBlockVo) Then
                    MsgBox("ＡＬ再展開中です。" & vbCrLf & vbCrLf & _
                           "暫くお待ちいただき再度実行をお願い致します。" & vbCrLf & vbCrLf & _
                            "担当者「" & tExclusiveControlBlockVo.EditUserId & _
                                     "：" & tanTousya & "」", MsgBoxStyle.OkOnly, "警告")
                    'ＡＬ再展開中なら処理中断
                    Exit Sub
                End If
            End If

            If IsNothing(tExclusiveControlBlockVo) Then
                'MsgBox("選択したブロックは誰も使用していません。")
                isExclusive = True '編集OK
                isReg = True '追加モード
            Else
                isReg = False '更新モード
                'レコードが有る場合、ログインユーザーと編集者コードを比較する。
                '担当者名を取得する'
                tanTousya = getDate.FindByShainName(tExclusiveControlBlockVo.EditUserId)
                '同じなら編集OK。
                If tExclusiveControlBlockVo.EditUserId = LoginInfo.Now.UserId Then
                    isExclusive = True '編集OK
                    MsgBox("選択したブロック下記の方が編集中に異常終了したもようです。編集できます" & vbCrLf & vbCrLf & _
                            "担当者「" & tExclusiveControlBlockVo.EditUserId & _
                                     "：" & tanTousya & "」", MsgBoxStyle.OkOnly, "警告")
                Else
                    '違うなら編集NG。
                    isExclusive = False '編集NG
                End If
            End If

            '------------------------------------------------------------------------------
            '   他部課に同一のブロック№が存在しないか？
            Dim tOtherSekkeiBlockVo As New TShisakuSekkeiBlockVo()
            tOtherSekkeiBlockVo = tExclusiveControlBlockDaoImpl.GetOtherBukaBlock(StrEventCode, StrDept, Me.txtBlockNo.Text)
            If StringUtil.IsNotEmpty(tOtherSekkeiBlockVo) Then
                ''エラーメッセージ
                ComFunc.ShowErrMsgBox("他の設計課で既にブロックが存在しています。" & vbLf & vbLf & _
                                      "この設計課 [" & tOtherSekkeiBlockVo.ShisakuBukaCode & _
                                      "] のブロック [" & tOtherSekkeiBlockVo.ShisakuBlockNo & "] で編集してください。")
                Exit Sub
            End If
            '------------------------------------------------------------------------------

            '排他チェック
            If isExclusive = True Then
                '他のユーザーが編集していなければ排他制御ブロックテーブルにレコード更新。
                RegisterMain(StrEventCode, StrDept, Me.txtBlockNo.Text, isReg)

                '続けて処理を続行。
                '画面を綺麗に、実行中のカーソルへ変更。
                Application.DoEvents()
                Cursor.Current = Cursors.WaitCursor
                Me.Hide()
                txtBlockNo.Text = txtBlockNo.Text.ToUpper
                ShowFrm41ShisakuBuhinEdit(txtBlockNo.Text, EDIT_MODE)
                Me.Show()

                'スプレッドの選択を前画面で選択されていたブロックに設定する
                For i As Integer = 0 To spdParts_Sheet1.RowCount() - 1
                    If m_spCom.GetCell(TAG_NO, i).Text = Me.txtBlockNo.Text Then
                        spdParts_Sheet1.SetActiveCell(i, 3)
                        Exit For
                    End If
                Next

                '画面を綺麗に、実行中のカーソルを元に戻す。
                Cursor.Current = Cursors.Default
            Else
                '他のユーザーが編集していれば警告を表示して終了。
                MsgBox("選択したブロックは編集中です。" & vbCrLf & vbCrLf & _
                        "担当者「" & tExclusiveControlBlockVo.EditUserId & _
                                 "：" & tanTousya & "」", MsgBoxStyle.OkOnly, "警告")
            End If

        End Sub

        ''' <summary>
        ''' 「閲覧」ボタンクリックの処理
        ''' </summary>
        ''' <param name="sender">ボタン</param>
        ''' <param name="e">ボタンクリックイベント</param>
        ''' <remarks></remarks>
        Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor
            Me.Hide()
            txtBlockNo.Text = txtBlockNo.Text.ToUpper
            ShowFrm41ShisakuBuhinEdit(txtBlockNo.Text, VIEW_MODE)
            Me.Show()

            '---------------------------------------------------------------
            '２次改修で追加。
            '   排他処理でステータスが更新されていた場合、自画面も閉じる。
            '以下のパラメータの場合自分も閉じる。
            If frm37ParaModori = "close" Then
                Me.Close()
                Exit Sub
            End If

            'スプレッドの選択を前画面で選択されていたブロックに設定する
            For i As Integer = 0 To spdParts_Sheet1.RowCount() - 1
                If m_spCom.GetCell(TAG_NO, i).Text = Me.txtBlockNo.Text Then
                    spdParts_Sheet1.SetActiveCell(i, 3)
                    Exit For
                End If
            Next

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default
        End Sub

        ''' <summary>
        ''' spreadにボタン、チェックボックス或いはradio button等クリックの処理
        ''' </summary>
        ''' <param name="sender">spread</param>
        ''' <param name="e">spreadのボタンクリックイベント</param>
        ''' <remarks></remarks>
        Private Sub Spread_OnButtonClicked(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles spdParts.ButtonClicked
            Dim cntCell = spdParts_Sheet1.Cells(e.Row, e.Column)
            Dim shisakuSekkiBolckForUpdate As New TShisakuSekkeiBlockVo
            shisakuSekkiBolckForUpdate.ShisakuEventCode = StrEventCode
            shisakuSekkiBolckForUpdate.ShisakuBukaCode = StrDept
            shisakuSekkiBolckForUpdate.ShisakuBlockNo = m_spCom.GetValue(TAG_NO, e.Row)
            shisakuSekkiBolckForUpdate.ShisakuBlockNoKaiteiNo = m_spCom.GetValue(TAG_KAITEI, e.Row)

            Select Case cntCell.Column.Tag
                Case TAG_JYOUTAI1
                    If IsJyoutaiCheckedRow(e.Row) Then
                        shisakuSekkiBolckForUpdate.Jyoutai = TShisakuSekkeiBlockVoHelper.Jyoutai.FINISHED
                    End If
                Case TAG_TANTO_SYOUNIN
                    If IsTantoSyouninCheckedRow(e.Row) Then
                        shisakuSekkiBolckForUpdate.TantoSyouninJyoutai = TShisakuSekkeiBlockVoHelper.TantoJyoutai.APPROVAL
                    End If
                Case TAG_KACHOU_SYOUNIN
                    If IsKachouSyouninCheckedRow(e.Row) Then
                        shisakuSekkiBolckForUpdate.KachouSyouninJyoutai = TShisakuSekkeiBlockVoHelper.KachouJyoutai.APPROVAL
                    End If
                Case TAG_FUYOU
                    If IsBlockFuyouRow(e.Row) Then
                        shisakuSekkiBolckForUpdate.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.UNNECESSARY
                        ''チェックボックスにチェックが入ったときに課長承認か担当承認が済んでいれば担当承認と課長承認をリセットする'
                        If IsKachouSyouninCheckedRow(spdParts_Sheet1.ActiveRow.Index) = True Or IsTantoSyouninCheckedRow(spdParts_Sheet1.ActiveRow.Index) = True Then

                            If StringUtil.Equals(m_spCom.GetValue(TAG_KAITEI, spdParts_Sheet1.ActiveRow.Index), "000") AndAlso StrMode.Equals(ShishakuKaiteiHensyuMode) Then

                            Else
                                UpdateMode2(spdParts_Sheet1.ActiveRow.Index)
                            End If

                        End If
                        'ブロックNoが000なら改訂をあげる'
                        If StringUtil.Equals(m_spCom.GetValue(TAG_KAITEI, spdParts_Sheet1.ActiveRow.Index), "000") AndAlso StrMode.Equals(ShishakuKaiteiHensyuMode) Then
                            AddMode2(spdParts_Sheet1.ActiveRow.Index)
                            shisakuSekkiBolckForUpdate.ShisakuBlockName = m_spCom.GetValue(TAG_MEISHOU, e.Row)
                            Dim shisakuBlockDaoBl As IShisakuBuhinEditBlockDao = New ShisakuBuhinEditBlockDaoImpl
                            '2012/01/23 ブロック不要チェック時のバグ修正
                            'コードブロック上部でチェックを外した時点で追加用のデータ（shisakuSekkiBolckForUpdate.BlockFuyou）を０（必要）にしているが
                            'アップデートするデータは不要として残さないとならない為、shisakuBlockDaoBl.UpdateByPkHasReturnBlockFuyou呼び出し前に
                            '（shisakuSekkiBolckForUpdate.BlockFuyou）を１（不要）戻し更新処理を呼び出すように変更した。
                            shisakuSekkiBolckForUpdate.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.UNNECESSARY  '<--一度戻さなくては？
                            Dim updatedShisakuBlockBl = shisakuBlockDaoBl.UpdateByPkHasReturnBlockFuyou(shisakuSekkiBolckForUpdate)
                            shisakuSekkiBolckForUpdate.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY  '<--一度戻さなくては？
                            Me.UpdateSpreadRow(updatedShisakuBlockBl, e.Row)
                            initShindo()
                            Return
                        End If
                    Else
                        shisakuSekkiBolckForUpdate.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY
                        'チェックボックスが外れた場合に担当承認、課長承認まで済んでいれば改訂してチェックを外す(改訂編集時のみ)'
                        If IsTantoSyouninCheckedRow(spdParts_Sheet1.ActiveRow.Index) Then
                            If IsKachouSyouninCheckedRow(spdParts_Sheet1.ActiveRow.Index) Then
                                '改訂編集モード'
                                If StrMode.Equals(ShishakuKaiteiHensyuMode) Then
                                    '改訂No000'
                                    If StringUtil.Equals(m_spCom.GetValue(TAG_KAITEI, spdParts_Sheet1.ActiveRow.Index), "000") Then
                                        'ブロック要で登録'
                                        AddMode(spdParts_Sheet1.ActiveRow.Index)
                                        '改訂Noがあがってないから'
                                        shisakuSekkiBolckForUpdate.ShisakuBlockName = m_spCom.GetValue(TAG_MEISHOU, e.Row)
                                        Dim shisakuBlockDaoBl As IShisakuBuhinEditBlockDao = New ShisakuBuhinEditBlockDaoImpl
                                        '2012/01/23 ブロック不要チェック時のバグ修正
                                        'コードブロック上部でチェックを外した時点で追加用のデータ（shisakuSekkiBolckForUpdate.BlockFuyou）を０（必要）にしているが
                                        'アップデートするデータは不要として残さないとならない為、shisakuBlockDaoBl.UpdateByPkHasReturnBlockFuyou呼び出し前に
                                        '（shisakuSekkiBolckForUpdate.BlockFuyou）を１（不要）戻し更新処理を呼び出すように変更した。
                                        shisakuSekkiBolckForUpdate.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.UNNECESSARY  '<--一度戻さなくては？
                                        Dim updatedShisakuBlockBl = shisakuBlockDaoBl.UpdateByPkHasReturnBlockFuyou(shisakuSekkiBolckForUpdate)
                                        shisakuSekkiBolckForUpdate.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY  '<--一度戻さなくては？
                                        Me.UpdateSpreadRow(updatedShisakuBlockBl, e.Row)
                                        initShindo()
                                        Return
                                    Else
                                        '改訂No001以降'
                                        AddMode(spdParts_Sheet1.ActiveRow.Index)
                                        '改訂Noがあがってないから'
                                        shisakuSekkiBolckForUpdate.ShisakuBlockName = m_spCom.GetValue(TAG_MEISHOU, e.Row)
                                        Dim shisakuBlockDaoBl As IShisakuBuhinEditBlockDao = New ShisakuBuhinEditBlockDaoImpl
                                        '2012/01/23 ブロック不要チェック時のバグ修正
                                        'コードブロック上部でチェックを外した時点で追加用のデータ（shisakuSekkiBolckForUpdate.BlockFuyou）を０（必要）にしているが
                                        'アップデートするデータは不要として残さないとならない為、shisakuBlockDaoBl.UpdateByPkHasReturnBlockFuyou呼び出し前に
                                        '（shisakuSekkiBolckForUpdate.BlockFuyou）を１（不要）戻し更新処理を呼び出すように変更した。
                                        shisakuSekkiBolckForUpdate.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.UNNECESSARY  '<--一度戻さなくては？
                                        Dim updatedShisakuBlockBl = shisakuBlockDaoBl.UpdateByPkHasReturnBlockFuyou(shisakuSekkiBolckForUpdate)
                                        shisakuSekkiBolckForUpdate.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY  '<--一度戻さなくては？
                                        Me.UpdateSpreadRow(updatedShisakuBlockBl, e.Row)
                                        initShindo()
                                        Return
                                    End If
                                Else
                                    '編集モード'
                                    '2012/02/08 イニシャルの時でも不要を解除した場合、承認状態を解除する必要があるので
                                    'アップロードロジックを追加
                                    UpdateMode2(spdParts_Sheet1.ActiveRow.Index)
                                End If
                            Else
                                UpdateMode2(spdParts_Sheet1.ActiveRow.Index)
                            End If
                        Else
                            '改訂000なら改訂をあげる'
                            If StrMode.Equals(ShishakuKaiteiHensyuMode) Then
                                If StringUtil.Equals(m_spCom.GetValue(TAG_KAITEI, spdParts_Sheet1.ActiveRow.Index), "000") Then
                                    'ブロック要で登録'
                                    AddMode(spdParts_Sheet1.ActiveRow.Index)
                                    '改訂Noがあがってないから'
                                    shisakuSekkiBolckForUpdate.ShisakuBlockName = m_spCom.GetValue(TAG_MEISHOU, e.Row)
                                    Dim shisakuBlockDaoBl As IShisakuBuhinEditBlockDao = New ShisakuBuhinEditBlockDaoImpl
                                    '2012/01/23 ブロック不要チェック時のバグ修正
                                    'コードブロック上部でチェックを外した時点で追加用のデータ（shisakuSekkiBolckForUpdate.BlockFuyou）を０（必要）にしているが
                                    'アップデートするデータは不要として残さないとならない為、shisakuBlockDaoBl.UpdateByPkHasReturnBlockFuyou呼び出し前に
                                    '（shisakuSekkiBolckForUpdate.BlockFuyou）を１（不要）戻し更新処理を呼び出すように変更した。
                                    shisakuSekkiBolckForUpdate.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.UNNECESSARY  '<--一度戻さなくては？
                                    Dim updatedShisakuBlockBl = shisakuBlockDaoBl.UpdateByPkHasReturnBlockFuyou(shisakuSekkiBolckForUpdate)
                                    shisakuSekkiBolckForUpdate.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY  '<--一度戻さなくては？
                                    Me.UpdateSpreadRow(updatedShisakuBlockBl, e.Row)
                                    initShindo()
                                    Return
                                End If
                            End If

                        End If
                    End If
            End Select

            'ブロック名称をDBに登録させるため'
            shisakuSekkiBolckForUpdate.ShisakuBlockName = m_spCom.GetValue(TAG_MEISHOU, e.Row)

            Dim shisakuBlockDao As IShisakuBuhinEditBlockDao = New ShisakuBuhinEditBlockDaoImpl
            Dim updatedShisakuBlock = shisakuBlockDao.UpdateByPkHasReturn(shisakuSekkiBolckForUpdate)
            Me.UpdateSpreadRow(updatedShisakuBlock, e.Row)
            initShindo()
        End Sub

        '''' <summary>
        '''' spread中身tooltip
        '''' </summary>
        '''' <param name="sender">spread</param>
        '''' <param name="e">tooltip event</param>
        '''' <remarks></remarks>
        'Private Sub spdParts_TextTipFetch(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.TextTipFetchEventArgs) Handles spdParts.TextTipFetch
        '    If Not e.ColumnHeader Then
        '        Dim sheet As Spread.SheetView = spdParts.ActiveSheet
        '        Dim col As Spread.Column = Nothing
        '        col = sheet.Columns(e.Column)
        '        Select Case col.Tag
        '            Case TAG_FUYOU
        '                e.TipText = "ブロックが不要の場合、チェックして下さい。"
        '                e.ShowTip = True
        '            Case TAG_JYOUTAI1
        '                e.TipText = "処置完了後、チェック（完了）して下さい。"
        '                e.ShowTip = True
        '            Case TAG_TANTO_SYOUNIN
        '                e.TipText = "状態「完了」後、承認して下さい。"
        '                e.ShowTip = True
        '            Case TAG_KACHOU_SYOUNIN
        '                e.TipText = "担当承認後、承認して下さい。"
        '                e.ShowTip = True
        '            Case Else
        '                e.ShowTip = False
        '        End Select
        '    End If
        'End Sub

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
                    Case TAG_FUYOU
                        tipText = "ブロックが不要の場合、チェックして下さい。"
                    Case TAG_KAITEI
                        tipText = "ブロックの改訂内容を確認する場合、クリックして下さい。"
                    Case TAG_JYOUTAI1
                        tipText = "処置完了後、チェック（完了）して下さい。"
                    Case TAG_TANTO_SYOUNIN
                        tipText = "状態「完了」後、承認して下さい。"
                    Case TAG_KACHOU_SYOUNIN
                        tipText = "担当承認後、承認して下さい。"
                    Case Else
                        tipText = ""
                End Select
                Me.ToolTip1.SetToolTip(spdParts, tipText)
            End If
            ToolTipRange = range

        End Sub

        ''' <summary>
        ''' 「指定ブロックEXCEL出力」ボタンクリック処理
        ''' </summary>
        ''' <param name="sender">ボタン</param>
        ''' <param name="e">ボタンクリックイベント</param>
        ''' <remarks></remarks>
        Private Sub btnExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelExport.Click
            ''引数を引き渡します。
            Dim strButtonFlag As String = "ShiTei"
            Dim eventCode As String = StrEventCode
            Dim bukaCode As String = StrDept
            Dim blockNo As String = txtBlockNo.Text
            Dim blockNoKaiteiNo As String = "000"   '000固定です。

            '2012/02/06 ブロックNoのリストをCtrl+クリック選択して出力'
            Dim blockNoList As New List(Of String)
            Dim selection As FarPoint.Win.Spread.Model.CellRange() = spdParts_Sheet1.GetSelections()
            'Dim selection As FarPoint.Win.Spread.Model.CellRange = spdParts_Sheet1.GetSelection(0)

            'For rowindex As Integer = 0 To selection.Length - 1
            '    blockNoList.Add(spdParts_Sheet1.Cells(selection(rowindex).Row, 3).Value)
            'Next
            For Each sel As FarPoint.Win.Spread.Model.CellRange In selection
                Dim row As Integer = sel.Row
                For i As Integer = 0 To sel.RowCount - 1
                    blockNoList.Add(spdParts_Sheet1.Cells(row, 3).Value)
                    row += 1
                Next
            Next



            '2012/02/14 ブロックNoでソート機能追加
            blockNoList.Sort()

            If blockNoList.Count = 0 Then
                blockNoList.Add(blockNo)
            End If
            If blockNoList.Count = 1 Then
                If Not StringUtil.Equals(blockNoList(0), blockNo) Then
                    blockNoList(0) = blockNo
                    'blockNoList = New List(Of String)
                End If
            End If

            Dim m_Disp38BlockExcel As New Frm38DispShisakuBuhinEditBlockExcel(strButtonFlag, StrEventCode, bukaCode, blockNo, Me.lblSekkeika.Text.ToString, blockNoList)
            m_Disp38BlockExcel.strButtonFlag = strButtonFlag
            m_Disp38BlockExcel.strEventCode = StrEventCode
            m_Disp38BlockExcel.strBukaCode = bukaCode
            m_Disp38BlockExcel.strBlockNo = blockNo
            m_Disp38BlockExcel.ShowDialog()

            'If StrMode.Equals(ShishakuKaiteiHensyuMode) Then

            '    ''試作部品表 編集・改訂編集（設計）画面
            '    Dim m_Disp38BlockExcel As New Frm38DispShisakuBuhinEditBlockExcel(strButtonFlag, StrEventCode, bukaCode, blockNo, Me.lblSekkeika.Text.ToString, blockNoList)
            '    m_Disp38BlockExcel.strButtonFlag = strButtonFlag
            '    m_Disp38BlockExcel.strEventCode = StrEventCode
            '    m_Disp38BlockExcel.strBukaCode = bukaCode
            '    m_Disp38BlockExcel.strBlockNo = blockNo
            '    m_Disp38BlockExcel.ShowDialog()
            'Else

            '    If frm01Kakunin.ConfirmOkCancel("指定ブロックをEXCEL出力しますか？") <> MsgBoxResult.Ok Then
            '        Return
            '    End If

            '    '画面を綺麗に、実行中のカーソルへ変更。
            '    'Application.DoEvents()
            '    Cursor.Current = Cursors.WaitCursor

            '    Dim Shitei As New ShisakuBuhinEditBlock2Excel(strButtonFlag, StrEventCode, bukaCode, blockNo, blockNoKaiteiNo, Me.lblSekkeika.Text.ToString, blockNoList)
            '    'Me.Close()　　閉じない

            '    '画面を綺麗に、実行中のカーソルを元に戻す。
            '    Cursor.Current = Cursors.Default

            'End If

        End Sub

        ''' <summary>
        ''' 「全ブロックEXCEL出力」ボタンクリック処理
        ''' </summary>
        ''' <param name="sender">ボタン</param>
        ''' <param name="e">ボタンクリックイベント</param>
        ''' <remarks></remarks>
        Private Sub btnExcelExportALL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelExportALL.Click

            '----------------------------------------------------------------------------------------------------
            '２次改修
            Dim m_Disp38BlockExcelAll As New Frm38DispShisakuBuhinEditBlockExcelAll("", StrEventCode, StrDept, Me.lblSekkeika.Text.ToString)
            m_Disp38BlockExcelAll.ShowDialog()

            'By　柳沼
            '以下の機能はコメントにしています。
            '   ２次改修で追加した画面に機能を移設してください。

            'If frm01Kakunin.ConfirmOkCancel("全ブロックをEXCEL出力しますか？") <> MsgBoxResult.Ok Then
            '    Return
            'End If

            ''画面を綺麗に、実行中のカーソルへ変更。
            'Application.DoEvents()
            'Cursor.Current = Cursors.WaitCursor

            'Dim showAll As New ShisakuBuhinEditBlock2Excel("", StrEventCode, StrDept, Nothing, Nothing, Me.lblSekkeika.Text.ToString)
            ''Me.Close()　　閉じない

            ''画面を綺麗に、実行中のカーソルを元に戻す。
            'Cursor.Current = Cursors.Default

        End Sub

#End Region

#Region "Excel改訂No入力画面で設定された改訂Noを取得する"

        Private _KaiteiNo As String

        Public Property KaiteiNo() As String
            Get
                Return _KaiteiNo
            End Get
            Set(ByVal value As String)
                _KaiteiNo = value
            End Set
        End Property


#End Region

#Region "２次改修・試作イベント情報のステータスをチェック。不正の場合処理中断"

        Public Function StatusCheck(ByVal strStatusMode As String) As Boolean

            '------------------------------------------------------------------------------------------------------
            '初期設定
            Dim tShisakuEventDaoImpl As ShisakuEventDao = New ShisakuEventDaoImpl
            Dim tShisakuEventVo As New TShisakuEventVo()
            '------------------------------------------------------------------------------------------------------

            '試作イベント情報からステータスを取得。
            tShisakuEventVo = tShisakuEventDaoImpl.FindByPk(StrEventCode)

            If IsNothing(tShisakuEventVo) Then
                MsgBox("試作イベント情報が存在しません。")
                Return False
            Else
                Select Case strStatusMode
                    Case ShishakuHensyuMode
                        If tShisakuEventVo.Status = "21" Then
                            'MsgBox("編集モードです。試作イベント情報のステータスは・・・ＯＫ。")
                            Return True
                        Else
                            MsgBox("イベントのステータスが変更されました。メニュー画面に戻ります。")
                            Return False
                        End If
                    Case ShishakuKaiteiHensyuMode
                        If tShisakuEventVo.Status = "23" Then
                            'MsgBox("改訂編集モードです。試作イベント情報のステータスは・・・ＯＫ。")
                            Return True
                        Else
                            MsgBox("イベントのステータスが変更されました。メニュー画面に戻ります。")
                            Return False
                        End If
                    Case Else
                        MsgBox("完了閲覧モードです。")
                        Return True
                End Select
            End If

        End Function

#End Region

        Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        End Sub

        '------------------------------------------------------------------------------------------------------------------
        '２次改修
        '   承認済みのブロックを選択してキャンセルされた場合は排他情報を削除する。
        Private Sub ExclusiveControl(ByVal strEventCode As String, ByVal strBukaCode As String, ByVal strBlockNo As String)
            Dim blockVo As New TExclusiveControlBlockVo
            Dim blockDao As ExclusiveControlBlockDao = New ExclusiveControlBlockImpl

            'キー情報が無いとエラーになるので事前にチェック（イレギュラー以外あるはず）
            blockVo = blockDao.FindByPk(strEventCode, strBukaCode, strBlockNo)
            If IsNothing(blockVo) Then
                'キー情報が無ければスルー。
            Else
                'KEY情報をセット
                blockVo.ShisakuEventCode = strEventCode
                blockVo.ShisakuBukaCode = strBukaCode
                blockVo.ShisakuBlockNo = strBlockNo
                blockVo.EditUserId = LoginInfo.Now.UserId 'ログインユーザーIDをセット
                'KEY情報を削除
                blockDao.DeleteByPk(blockVo)
            End If

        End Sub

        '------------------------------------------------------------------------------------------------------------------
        Private Sub btnAlSaiTenkai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAlSaiTenkai.Click
            ''引数を引き渡します。
            Dim strButtonFlag As String = "ShiTei"
            Dim eventCode As String = StrEventCode
            Dim bukaCode As String = StrDept
            Dim isExclusive As Boolean  'true=編集OK、false=編集NG
            Dim isReg As Boolean  'true=追加、false=更新
            Dim tanTousya As String = Nothing
            Dim getDate As New EditBlock2ExcelDaoImpl() '担当者名取得用に。

            '------------------------------------------------------------------------------------------------------
            '次に試作イベントコード、設計課、ブロックの部品表が編集中かチェックする。
            '------------------------------------------------------------------------------------------------------
            '他のユーザーが編集中か確認する。
            Dim tExclusiveControlBlockDaoImpl As ExclusiveControlBlockDao = New ExclusiveControlBlockImpl
            Dim tExclusiveControlBlockVo As New TExclusiveControlBlockVo()

            '
            'AL再展開中かチェック。
            If IsNothing(tExclusiveControlBlockVo) Then
                tExclusiveControlBlockVo = tExclusiveControlBlockDaoImpl.FindByPk(StrEventCode, StrDept, "ZZZZ")
                '他のユーザーがＡＬ再展開中なら警告を表示して終了。
                If Not IsNothing(tExclusiveControlBlockVo) Then
                    MsgBox("ＡＬ再展開中です。" & vbCrLf & vbCrLf & _
                           "暫くお待ちいただき再度実行をお願い致します。" & vbCrLf & vbCrLf & _
                            "担当者「" & tExclusiveControlBlockVo.EditUserId & _
                                     "：" & tanTousya & "」", MsgBoxStyle.OkOnly, "警告")
                    'ＡＬ再展開中なら処理中断
                    Exit Sub
                End If
            End If

            'ブロックの編集中かもチェック。
            '排他制御ブロック情報から試作イベントコード、部課コードで登録されているかチェック。
            tExclusiveControlBlockVo = tExclusiveControlBlockDaoImpl.FindByPkBuka(StrEventCode, StrDept)

            If IsNothing(tExclusiveControlBlockVo) Then
                'MsgBox("選択したブロックは誰も使用していません。")
                isExclusive = True '編集OK
                isReg = True '追加モード
            Else
                isReg = False '更新モード
                isExclusive = False '編集NG
            End If

            '排他チェック
            If isExclusive = True Then
                '他のユーザーが編集していなければ排他制御ブロックテーブルにレコード更新。
                '   試作イベントコード、部課コード、ブロック№（ZZZZ固定）で。
                RegisterMain(StrEventCode, StrDept, "ZZZZ", isReg)

                '続けて処理を続行。

            Else
                '他のユーザーが編集していれば警告を表示して終了。
                MsgBox("編集中のブロックが存在します。" & vbCrLf & vbCrLf & _
                       "暫くお待ちいただき再度実行をお願い致します。" & vbCrLf & vbCrLf & _
                        "担当者「" & tExclusiveControlBlockVo.EditUserId & _
                                 "：" & tanTousya & "」", MsgBoxStyle.OkOnly, "警告")
                '処理中断
                Exit Sub
            End If

            '----------------------------------------------------------------------------
            '処理続行
            '----------------------------------------------------------------------------

            'ブロックNoのリストをCtrl+クリック選択して出力'
            Dim blockNoList As New List(Of String)
            Dim selection As FarPoint.Win.Spread.Model.CellRange() = spdParts_Sheet1.GetSelections()

            Dim strBlockList As String = ""
            Dim strBlockErrorList As String = ""
            Dim strBlock000List As String = "" '改訂編集モードで改訂№：000を含んでいる場合
            Dim lng000cnt As Long = 0 '改訂№：000を含むブロックをカウントする
            Dim strBlockEditUmu As String = ""
            Dim frm04 As New frm04Kakunin
            Dim i As Long = 0
            '配列定義
            Dim blockNo As String() = New String(500) {}
            Dim kaiteiNo As String() = New String(500) {}
            Dim jyotai As String() = New String(500) {}
            Dim tanto As String() = New String(500) {}

            For rowindex As Integer = 0 To selection.Length - 1
                'リストに追加（ブロック№＋”：”＋ブロック№改訂№）
                blockNoList.Add(spdParts_Sheet1.Cells(selection(rowindex).Row, 3).Value + ":" + _
                                spdParts_Sheet1.Cells(selection(rowindex).Row, 4).Value)
                '選択ブロック表示用
                If strBlockList = "" Then
                    strBlockList = "以下のブロックをＡＬ再展開します。宜しいですか？"
                End If
                '２行空ける。
                strBlockList = strBlockList + vbLf + vbLf
                'ブロックをセットする。
                strBlockList = strBlockList + spdParts_Sheet1.Cells(selection(rowindex).Row, 3).Value
                'ブロック№改訂№をセットする。
                strBlockList = strBlockList + " : " + spdParts_Sheet1.Cells(selection(rowindex).Row, 4).Value
                'ブロック名をセットする。
                strBlockList = strBlockList + " : " + spdParts_Sheet1.Cells(selection(rowindex).Row, 6).Value

                '編集中チェック
                If spdParts_Sheet1.Cells(selection(rowindex).Row, 2).Value <> "" Then
                    If spdParts_Sheet1.Cells(selection(rowindex).Row, 2).Value = "編集中" Or _
                        spdParts_Sheet1.Cells(selection(rowindex).Row, 2).Value = "一時保存" Or _
                        spdParts_Sheet1.Cells(selection(rowindex).Row, 2).Value = "登録済み" Then
                        strBlockEditUmu = "ARI"
                        blockNo(i) = spdParts_Sheet1.Cells(selection(rowindex).Row, 3).Value
                        kaiteiNo(i) = spdParts_Sheet1.Cells(selection(rowindex).Row, 4).Value
                        jyotai(i) = spdParts_Sheet1.Cells(selection(rowindex).Row, 2).Value
                        tanto(i) = spdParts_Sheet1.Cells(selection(rowindex).Row, 7).Value
                        i = i + 1
                    End If
                End If

                'エラーチェック
                If spdParts_Sheet1.Cells(selection(rowindex).Row, 2).Value = "承認１" Or _
                    spdParts_Sheet1.Cells(selection(rowindex).Row, 2).Value = "承認２" Or _
                    spdParts_Sheet1.Cells(selection(rowindex).Row, 0).Value = True Or _
                    spdParts_Sheet1.Cells(selection(rowindex).Row, 1).Value = True Then
                    'エラーメッセージ
                    If strBlockErrorList = "" Then
                        strBlockErrorList = "ＡＬ再展開が出来ないブロックが指定されています。ご確認ください。"
                    End If
                    '２行空ける。
                    strBlockErrorList = strBlockErrorList + vbLf + vbLf
                    'エラーのブロックをセットする。
                    strBlockErrorList = strBlockErrorList + spdParts_Sheet1.Cells(selection(rowindex).Row, 3).Value
                    '   ブロック改訂№もセットする。
                    strBlockErrorList = strBlockErrorList + ":" + spdParts_Sheet1.Cells(selection(rowindex).Row, 4).Value
                    If spdParts_Sheet1.Cells(selection(rowindex).Row, 2).Value = "承認１" Or _
                        spdParts_Sheet1.Cells(selection(rowindex).Row, 2).Value = "承認２" Then
                        strBlockErrorList = strBlockErrorList + " : " + spdParts_Sheet1.Cells(selection(rowindex).Row, 2).Value
                    ElseIf spdParts_Sheet1.Cells(selection(rowindex).Row, 0).Value = True Then
                        strBlockErrorList = strBlockErrorList + " : " + "ブロック不要"
                    ElseIf spdParts_Sheet1.Cells(selection(rowindex).Row, 1).Value = True Then
                        strBlockErrorList = strBlockErrorList + " : " + "完了"
                    End If
                Else
                    '改訂編集モードで000を含む場合エラーを表示する。
                    If StrMode.Equals(ShishakuKaiteiHensyuMode) Then
                        If spdParts_Sheet1.Cells(selection(rowindex).Row, 4).Value = "000" Then
                            'エラーメッセージ
                            If strBlock000List = "" Then
                                strBlock000List = "改訂№［000］のブロックが指定されています。個別に実行して下さい。"
                            End If
                            '２行空ける。
                            strBlock000List = strBlock000List + vbLf + vbLf
                            '改訂№000のブロックをセットする。
                            strBlock000List = strBlock000List + spdParts_Sheet1.Cells(selection(rowindex).Row, 3).Value
                            '   ブロック改訂№もセットする。
                            strBlock000List = strBlock000List + ":" + spdParts_Sheet1.Cells(selection(rowindex).Row, 4).Value
                        End If
                        'カウント
                        lng000cnt = lng000cnt + 1
                    End If

                End If
            Next
            'エラーがあればメッセージを表示する。
            If strBlockErrorList <> "" Then
                ComFunc.ShowErrMsgBox(strBlockErrorList)
                '排他情報をクリア
                ExclusiveControl(StrEventCode, StrDept, "ZZZZ")
                Exit Sub
            End If
            '改訂編集モードで改訂№000を含むブロックが複数あればメッセージを表示する。
            If strBlock000List <> "" And lng000cnt > 1 Then
                ComFunc.ShowErrMsgBox(strBlock000List)
                '排他情報をクリア
                ExclusiveControl(StrEventCode, StrDept, "ZZZZ")
                Exit Sub
            End If

            If blockNoList.Count = 0 Then
                ComFunc.ShowErrMsgBox("１つ以上ブロックを選択してください。")
                '排他情報をクリア
                ExclusiveControl(StrEventCode, StrDept, "ZZZZ")
                Exit Sub
            End If

            'ブロックNoでソート機能追加
            blockNoList.Sort()

            '状態がブランク以外データがあれば画面を表示する。
            Dim msgStr1 As String = "編集中のブロックが存在します。"
            Dim msgStr2 As String = "ＡＬ再展開を実施しますか？"

            If strBlockEditUmu <> "" AndAlso frm04Kakunin.ConfirmOkCancel(msgStr1, _
                                                                          blockNo, kaiteiNo, jyotai, tanto, i, _
                                                                          msgStr2) <> MsgBoxResult.Ok Then
                ComFunc.ShowInfoMsgBox("ＡＬ再展開を中断しました。")
                '排他情報をクリア
                ExclusiveControl(StrEventCode, StrDept, "ZZZZ")
                Return
            End If

            'ブロック情報をMSGBOXへ表示する。
            If strBlockList <> "" Then
                Dim lngYesNo As Long = 0
                lngYesNo = MsgBox(strBlockList, MsgBoxStyle.YesNo, "確認")
                'Ｎｏなら処理中断。
                If lngYesNo = MsgBoxResult.No Then
                    ComFunc.ShowInfoMsgBox("ＡＬ再展開を中断しました。")
                    '排他情報をクリア
                    ExclusiveControl(StrEventCode, StrDept, "ZZZZ")
                    Exit Sub
                End If
            End If

            '--------------------------------------------------------------------------------------
            '自画面非表示
            Me.Hide()

            '新規作成画面表示。
            '   試作イベントコード、製作一覧発行№、製作一覧発行№改訂№
            '   パラメータは試作イベントコード、ブロック№（LIST）
            Dim strNext As String = ""
            Using frm As New Frm38DispEventEdit(StrEventCode, bukaCode, blockNoList)
                frm.ShowDialog()
                If frm.ResultOk Then
                    strNext = "NEXT"
                End If
            End Using
            'ベース車情報画面で展開ボタンが押されたならALを再展開する。
            If strNext = "NEXT" Then
                '処理中画面表示
                Dim SyorichuForm As New frm03Syorichu
                SyorichuForm.lblKakunin.Text = "指定ブロックのＡＬ情報再展開を"
                SyorichuForm.lblKakunin2.Text = "実施しています。"
                SyorichuForm.Execute()
                SyorichuForm.Show()
                Application.DoEvents()

                '改訂編集モードで000をなら最初に改訂を＋１する。
                If StrMode.Equals(ShishakuKaiteiHensyuMode) And Mid(blockNoList(0).ToString, 6, 3) = "000" Then
                    AddMode(rowNoFind(txtBlockNo.Text))
                    blockNoList(0) = Mid(blockNoList(0), 1, 5) & "001" '件数は一件、改訂№は000しかありえない。
                End If
                '選択した全てのブロックのINSTL情報がTMPベース情報でE-BOMから抽出できるか？
                '   T_SHISAKU_SEKKEI_BLOCK_INSTL 抽出チェック
                Dim sekkeiBlockInstl As New SekkeiBlockInstlSupplier(StrEventCode, bukaCode, blockNoList)
                Dim aShisakuDate As New ShisakuDate
                If StringUtil.Equals(sekkeiBlockInstl.Check, True) Then

                    'TMPベース情報よりALを再展開
                    '   T_SHISAKU_SEKKEI_BLOCK_INSTL 再作成
                    sekkeiBlockInstl.Register()
                    '   T_SHISAKU_BUHIN_EDIT 再作成
                    Dim buhinEdit As New BuhinEditTenkaiSubject(StrEventCode, bukaCode, blockNoList)
                    '   ＡＬ再展開で使用したベース車情報を作成しておく。

                    'ステータス更新
                    For rowindex As Integer = 0 To selection.Length - 1

                        Dim shisakuSekkiBolckForUpdate As New TShisakuSekkeiBlockVo
                        shisakuSekkiBolckForUpdate.ShisakuEventCode = StrEventCode
                        shisakuSekkiBolckForUpdate.ShisakuBukaCode = StrDept
                        shisakuSekkiBolckForUpdate.ShisakuBlockNo = m_spCom.GetValue(TAG_NO, selection(rowindex).Row)
                        If StrMode.Equals(ShishakuKaiteiHensyuMode) And Mid(blockNoList(0).ToString, 6, 3) = "001" Then
                            shisakuSekkiBolckForUpdate.ShisakuBlockNoKaiteiNo = "001"
                        Else
                            shisakuSekkiBolckForUpdate.ShisakuBlockNoKaiteiNo = m_spCom.GetValue(TAG_KAITEI, selection(rowindex).Row)
                        End If
                        'ブロック名称
                        shisakuSekkiBolckForUpdate.ShisakuBlockName = m_spCom.GetValue(TAG_MEISHOU, selection(rowindex).Row)
                        'ステータス
                        shisakuSekkiBolckForUpdate.Jyoutai = TShisakuSekkeiBlockVoHelper.Jyoutai.REALBUHIN
                        '担当者、更新日時はを再セットする。
                        shisakuSekkiBolckForUpdate.UserId = LoginInfo.Now.UserId
                        shisakuSekkiBolckForUpdate.SaisyuKoushinbi = DateUtil.ConvDateToIneteger(aShisakuDate.CurrentDateTime)
                        shisakuSekkiBolckForUpdate.SaisyuKoushinjikan = DateUtil.ConvTimeToIneteger(aShisakuDate.CurrentDateTime)

                        Dim shisakuBlockDao As IShisakuBuhinEditBlockDao = New ShisakuBuhinEditBlockDaoImpl
                        Dim updatedShisakuBlock = shisakuBlockDao.UpdateByPkHasReturn(shisakuSekkiBolckForUpdate)
                        Me.UpdateSpreadRow(updatedShisakuBlock, selection(rowindex).Row)

                    Next

                    '処理中画面非表示
                    SyorichuForm.Close()

                    ComFunc.ShowInfoMsgBox("ＡＬ再展開が完了しました。")
                Else
                    '処理中画面非表示
                    SyorichuForm.Close()

                    ComFunc.ShowInfoMsgBox("ＡＬ再展開を中断しました。")
                End If
            Else
                ComFunc.ShowInfoMsgBox("ＡＬ再展開を中断しました。")
            End If

            '排他情報をクリア
            ExclusiveControl(StrEventCode, StrDept, "ZZZZ")

            '自画面再表示
            Me.Show()
            System.GC.Collect()
            '--------------------------------------------------------------------------------------

        End Sub

        Private Sub LinkExcelマクロ_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkExcelマクロ.LinkClicked
            ' Excelのマクロを実行する。
            System.Diagnostics.Process.Start("\\fgnt30\pt\試作イベント管理\試作イベント情報\管理画面\EXCELメンテマクロ.xls")
        End Sub
    End Class
End Namespace
