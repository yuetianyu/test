Imports EBom.Common
Imports FarPoint.Win.Spread.CellType
Imports EventSakusei.NokiIkkatuSettei
Imports ShisakuCommon.Ui
Imports EventSakusei.TehaichoEdit.Ui
Imports EventSakusei.TehaichoEdit
Imports FarPoint.Win.Spread
Imports FarPoint.Win
Imports ShisakuCommon
Imports ShisakuCommon.Ui.Spd
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util.LabelValue

''' <summary>
''' 出力対象を選択画面 TES)劉 2015/01/14 追加
''' </summary>
''' <remarks></remarks>
Public Class Frm20OutputDataSelect

#Region "プライベート変数"
    '''<summary>初期化完了フラグ</summary>>
    Private _InitComplete As Boolean = False
    ''' <summary>画面制御ロジック</summary>
    Private _TehaiEditLogic As TehaichoEditLogic
    '手配基本情報
    Private _TehaiKihonVos As List(Of TShisakuTehaiKihonVo)
    '試作イベントコード
    Private _shisakuEventCode As String
    '表示フラグ
    Private _DisplayFlag As String

    Private _ResultList As List(Of TShisakuTehaiKihonVo)

    Private _ResultOk As Boolean
    ' '' ブロックNo
    'Private _blockNo As String
    ''部品番号
    'Private _buhinNo As String
    ''名称
    'Private _buhinName As String

#Region "各COLUMNのINDEX定義"
    Private Const COLUMN_SELECTED_INDEX As Integer = 0
    Private Const COLUMN_SHISAKU_BLOCK_NO_INDEX As Integer = 1
    Private Const COLUMN_GYOU_ID_INDEX As Integer = 2
    Private Const COLUMN_LEVEL_INDEX As Integer = 3
    Private Const COLUMN_BUHIN_NO_INDEX As Integer = 4
    Private Const COLUMN_BUHIN_NAME_INDEX As Integer = 5
    Private Const COLUMN_SHUKEI_CODE_INDEX As Integer = 6
    Private Const COLUMN_TEHAI_KIGOU_INDEX As Integer = 7
    Private Const COLUMN_KOUTAN_INDEX As Integer = 8
    Private Const COLUMN_TORIHIKISAKI_CODE_INDEX As Integer = 9
    Private Const COLUMN_NOUBA_INDEX As Integer = 10
    Private Const COLUMN_KYOUKU_SECTION_INDEX As Integer = 11
    Private Const COLUMN_NOUNYU_SHIJIBI_INDEX As Integer = 12
    Private Const COLUMN_TOTAL_INSU_SURYO_INDEX As Integer = 13
    Private Const COLUMN_SHUTUZU_YOTEI_DATE_INDEX As Integer = 14
    Private Const COLUMN_SHUTUZU_JISEKI_DATE_INDEX As Integer = 15
    Private Const COLUMN_SHUTUZU_JISEKI_KAITEI_NO_INDEX As Integer = 16
    Private Const COLUMN_SHUTUZU_JISEKI_STSR_DHSTBA_INDEX As Integer = 17
    Private Const COLUMN_SAISYU_SETSUHEN_DATE_INDEX As Integer = 18
    Private Const COLUMN_SAISYU_SETSUHEN_KAITEI_NO_INDEX As Integer = 19
    Private Const COLUMN_SAISYU_SETSUHEN_STSR_DHSTBA_INDEX As Integer = 20
    Private Const COLUMN_ZAISHITU_KIKAKU_1_INDEX As Integer = 21
    Private Const COLUMN_ZAISHITU_KIKAKU_2_INDEX As Integer = 22
    Private Const COLUMN_ZAISHITU_KIKAKU_3_INDEX As Integer = 23
    Private Const COLUMN_ZAISHITU_MEKKI_INDEX As Integer = 24
    Private Const COLUMN_SHISAKU_BANKO_SURYO_INDEX As Integer = 25
    Private Const COLUMN_SHISAKU_BANKO_SURYO_U_INDEX As Integer = 26
    Private Const COLUMN_MATERIAL_INFO_LENGTH_INDEX As Integer = 27
    Private Const COLUMN_MATERIAL_INFO_WIDTH_INDEX As Integer = 28
    Private Const COLUMN_ZAIRYO_SUNPO_X_INDEX As Integer = 29
    Private Const COLUMN_ZAIRYO_SUNPO_Y_INDEX As Integer = 30
    Private Const COLUMN_ZAIRYO_SUNPO_Z_INDEX As Integer = 31
    Private Const COLUMN_ZAIRYO_SUNPO_XY_INDEX As Integer = 32
    Private Const COLUMN_ZAIRYO_SUNPO_XZ_INDEX As Integer = 33
    Private Const COLUMN_ZAIRYO_SUNPO_YZ_INDEX As Integer = 34
    Private Const COLUMN_MATERIAL_INFO_ORDER_TARGET_INDEX As Integer = 35
    Private Const COLUMN_MATERIAL_INFO_ORDER_CHK_INDEX As Integer = 36
    Private Const COLUMN_DATA_ITEM_KAITEI_NO_INDEX As Integer = 37
    Private Const COLUMN_DATA_ITEM_AREA_NAME_INDEX As Integer = 38
    Private Const COLUMN_DATA_ITEM_SET_NAME_INDEX As Integer = 39
    Private Const COLUMN_DATA_ITEM_KAITEI_INFO_INDEX As Integer = 40
    Private Const COLUMN_DATA_ITEM_DATA_PROVISION_INDEX As Integer = 41
    Private Const COLUMN_MAKER_CODE_INDEX As Integer = 42
    Private Const COLUMN_BIKOU_INDEX As Integer = 43

#End Region

#End Region

#Region "プロパティ"

#Region "初期化完了確認"
    ''' <summary>
    ''' 初期化完了確認
    ''' 
    ''' 初期化正常実行でTRUEを返す
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property InitComplete()
        Get
            Return _InitComplete
        End Get
    End Property
#End Region

    ''' <summary>
    ''' OKボタンでTRUEを返す
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ResultOk()
        Get
            Return _ResultOk
        End Get
    End Property

    ''' <summary>
    ''' 選択された情報を返す
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ResultList()
        Get
            Return _ResultList
        End Get
    End Property

#End Region

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="_ResultList">手配基本情報</param>
    ''' <param name="tehaiEditLogic">画面制御ロジック</param>
    ''' <param name="shisakuEventCode">試作イベントコード</param>
    ''' <param name="displayFlag">表示フラグ</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal _ResultList As List(Of TShisakuTehaiKihonVo), ByVal tehaiEditLogic As TehaichoEditLogic, ByVal shisakuEventCode As String, ByVal displayFlag As String)

        InitializeComponent()

        Me._TehaiKihonVos = _ResultList

        Me._TehaiEditLogic = tehaiEditLogic

        Me._shisakuEventCode = shisakuEventCode

        Me._DisplayFlag = displayFlag

        ShisakuFormUtil.Initialize(Me)

        '初期化メイン
        Initialize()
    End Sub

#End Region

#Region "フォームロード"
    ''' <summary>
    ''' フォームロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm20OutputDataSelect_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Focus()

    End Sub
#End Region

#Region "初期化"

#Region "初期化メイン"
    ''' <summary>
    ''' 初期化メイン
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Initialize()

        Cursor.Current = Cursors.WaitCursor

        ''画面のPG-IDが表示されます。
        ShisakuFormUtil.setTitleVersion(Me)
        LblCurrPGId.Text = "PG-ID :" + PROGRAM_ID_20

        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        ShisakuFormUtil.SetIdAndBuka(LblCurrUserId, LblCurrBukaName)

        'コンボボックス類初期化
        InitComboControl()

        '初期データ表示スプレッド
        DispSpreadBase()

        '表示選択をチェック
        RadioButton1.Checked = True

        If String.Equals(_DisplayFlag, "DataProvision") Then
            btnSelect.Text = "支給先選択"
            lblMakerCode.Visible = False
            cmbMakerCode.Visible = False
            lblMakerName.Visible = False
            cmbMakerName.Visible = False
        ElseIf String.Equals(_DisplayFlag, "KojiShirei") Then
            btnSelect.Text = "担当者選択"
        ElseIf String.Equals(_DisplayFlag, "Order") Then
            btnSelect.Text = "取引先選択"
            lblMakerCode.Visible = False
            cmbMakerCode.Visible = False
            lblMakerName.Visible = False
            cmbMakerName.Visible = False
        End If

        '初期化完了
        _InitComplete = True

    End Sub

#End Region

#Region "コンボボックス類初期化"
    ''' <summary>
    ''' コンボボックス類初期化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitComboControl()

        'ブロックNoコンボボックス
        FormUtil.BindLabelValuesToComboBox(Me.cmbBlockNo, GetLabelValues_BlockNo(), True)
        'ShisakuFormUtil.SettingDefaultProperty(cmbBlockNo)
        '' 大文字化
        FormUtil.SettingComboBoxCharacterCasingUpper(cmbBlockNo)

        '部品番号コンボボックス
        FormUtil.BindLabelValuesToComboBox(Me.cmbBuhinNo, GetLabelValues_BuhinNo(), True)
        'ShisakuFormUtil.SettingDefaultProperty(cmbBuhinNo)
        '' 大文字化
        FormUtil.SettingComboBoxCharacterCasingUpper(cmbBuhinNo)

        '部品名称コンボボックス
        FormUtil.BindLabelValuesToComboBox(Me.cmbBuhinName, GetLabelValues_BuhinName(), True)
        'ShisakuFormUtil.SettingDefaultProperty(cmbBuhinName)
        '' 大文字化
        FormUtil.SettingComboBoxCharacterCasingUpper(cmbBuhinName)

        '工事指令書の場合、取引先コンボを追加
        If String.Equals(_DisplayFlag, "KojiShirei") Then
            '取引先コードコンボボックス
            FormUtil.BindLabelValuesToComboBox(Me.cmbMakerCode, GetLabelValues_MakerCode(), True)
            'ShisakuFormUtil.SettingDefaultProperty(cmbMakerCode)
            '' 大文字化
            FormUtil.SettingComboBoxCharacterCasingUpper(cmbMakerCode)

            '取引先名称コンボボックス
            FormUtil.BindLabelValuesToComboBox(Me.cmbMakerName, GetLabelValues_MakerName(), True)
            'ShisakuFormUtil.SettingDefaultProperty(cmbMakerName)
            '' 大文字化
            FormUtil.SettingComboBoxCharacterCasingUpper(cmbMakerName)
        End If

    End Sub
#End Region

#Region "スプレッド基本情報のデータ表示"
    ''' <summary>
    ''' スプレッド基本情報の表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DispSpreadBase()
        Try
            SpreadUtil.Initialize(Me.spdBase)

            '基本情報表示ロジック
            If SetSpreadBase() = False Then
                Throw New Exception()
            End If

            'Enterキーの動作を、「編集」から「次行へ移動」にする
            ShisakuCommon.Ui.Spd.SpreadUtil.BindKeyEnterIsNextRow(Me.spdBase)

            'Shift + Enterキーの動作を、「前行へ移動」にする
            ShisakuCommon.Ui.Spd.SpreadUtil.BindKeyShiftEnterIsPreviousRow(Me.spdBase)

            SpreadUtil.LockAllColumns(Me.spdBase_Sheet1)
            Me.spdBase_Sheet1.Columns(COLUMN_SELECTED_INDEX).Locked = False
        Catch ex As Exception
            Dim msg As String
            msg = String.Format("基本情報のデータ表示で問題が発生しました(ERR={0})", ex.Message)
            ComFunc.ShowErrMsgBox(msg)
            Me.Close()
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 基本情報スプレッドデータ格納
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSpreadBase() As Boolean

        Me.spdBase_Sheet1.RowCount = _TehaiKihonVos.Count + TehaichoEditLogic.GetTitleRowsIn(spdBase_Sheet1)
        Dim rowIndex As Integer = TehaichoEditLogic.GetTitleRowsIn(spdBase_Sheet1)

        For Each result_Vo As TShisakuTehaiKihonVo In _TehaiKihonVos
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SELECTED_INDEX).Value = False
            'ブロック
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SHISAKU_BLOCK_NO_INDEX).Value = result_Vo.ShisakuBlockNo
            '行ＩＤ
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_GYOU_ID_INDEX).Value = result_Vo.GyouId
            'レベル
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_LEVEL_INDEX).Value = result_Vo.Level
            '部品番号
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_BUHIN_NO_INDEX).Value = result_Vo.BuhinNo
            '部品名称
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_BUHIN_NAME_INDEX).Value = result_Vo.BuhinName
            '集計コード
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SHUKEI_CODE_INDEX).Value = result_Vo.ShukeiCode
            '手配記号
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_TEHAI_KIGOU_INDEX).Value = result_Vo.TehaiKigou
            '購坦
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_KOUTAN_INDEX).Value = result_Vo.Koutan
            '取引先コード
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_TORIHIKISAKI_CODE_INDEX).Value = result_Vo.TorihikisakiCode
            '納場
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_NOUBA_INDEX).Value = result_Vo.Nouba
            '供給セクション
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_KYOUKU_SECTION_INDEX).Value = result_Vo.KyoukuSection
            '納入指示日
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_NOUNYU_SHIJIBI_INDEX).Value = TehaichoEditLogic.ConvDateInt8(result_Vo.NounyuShijibi)
            '合計員数
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_TOTAL_INSU_SURYO_INDEX).Value = result_Vo.TotalInsuSuryo
            '出図予定日
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SHUTUZU_YOTEI_DATE_INDEX).Value = TehaichoEditLogic.ConvDateInt8(result_Vo.ShutuzuYoteiDate)
            '出図実績_日付
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SHUTUZU_JISEKI_DATE_INDEX).Value = TehaichoEditLogic.ConvDateInt8(result_Vo.ShutuzuJisekiDate)
            '出図実績_改訂№
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SHUTUZU_JISEKI_KAITEI_NO_INDEX).Value = result_Vo.ShutuzuJisekiKaiteiNo
            '出図実績_設通№
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SHUTUZU_JISEKI_STSR_DHSTBA_INDEX).Value = result_Vo.ShutuzuJisekiStsrDhstba
            '最終織込設変情報_日付
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SAISYU_SETSUHEN_DATE_INDEX).Value = TehaichoEditLogic.ConvDateInt8(result_Vo.SaisyuSetsuhenDate)
            '最終織込設変情報_改訂№
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SAISYU_SETSUHEN_KAITEI_NO_INDEX).Value = result_Vo.SaisyuSetsuhenKaiteiNo
            '最終織込設変情報_設通№
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SAISYU_SETSUHEN_STSR_DHSTBA_INDEX).Value = result_Vo.StsrDhstba
            '材質・規格１
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAISHITU_KIKAKU_1_INDEX).Value = result_Vo.ZaishituKikaku1
            '材質・規格２
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAISHITU_KIKAKU_2_INDEX).Value = result_Vo.ZaishituKikaku2
            '材質・規格３
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAISHITU_KIKAKU_3_INDEX).Value = result_Vo.ZaishituKikaku3
            '材質・メッキ
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAISHITU_MEKKI_INDEX).Value = result_Vo.ZaishituMekki
            '板厚
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SHISAKU_BANKO_SURYO_INDEX).Value = result_Vo.ShisakuBankoSuryo
            '板厚・ｕ
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SHISAKU_BANKO_SURYO_U_INDEX).Value = result_Vo.ShisakuBankoSuryoU
            '材料情報・製品長
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_MATERIAL_INFO_LENGTH_INDEX).Value = result_Vo.MaterialInfoLength
            '材料情報・製品幅
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_MATERIAL_INFO_WIDTH_INDEX).Value = result_Vo.MaterialInfoWidth

            '材料寸法_X(mm)
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAIRYO_SUNPO_X_INDEX).Value = result_Vo.ZairyoSunpoX
            '材料寸法_Y(mm)
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAIRYO_SUNPO_Y_INDEX).Value = result_Vo.ZairyoSunpoY
            '材料寸法_Z(mm)
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAIRYO_SUNPO_Z_INDEX).Value = result_Vo.ZairyoSunpoZ
            '材料寸法_X+Y(mm)
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAIRYO_SUNPO_XY_INDEX).Value = result_Vo.ZairyoSunpoXy
            '材料寸法_X+Z(mm)
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAIRYO_SUNPO_XZ_INDEX).Value = result_Vo.ZairyoSunpoXz
            '材料寸法_Y+Z(mm)
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAIRYO_SUNPO_YZ_INDEX).Value = result_Vo.ZairyoSunpoYz

            '材料情報・発注対象
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_MATERIAL_INFO_ORDER_TARGET_INDEX).Value = result_Vo.MaterialInfoOrderTarget
            '材料情報・発注済
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_MATERIAL_INFO_ORDER_CHK_INDEX).Value = result_Vo.MaterialInfoOrderChk
            'データ項目・改訂№
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_DATA_ITEM_KAITEI_NO_INDEX).Value = result_Vo.DataItemKaiteiNo
            'データ項目・エリア名
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_DATA_ITEM_AREA_NAME_INDEX).Value = result_Vo.DataItemAreaName
            'データ項目・セット名
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_DATA_ITEM_SET_NAME_INDEX).Value = result_Vo.DataItemSetName
            'データ項目・改訂情報
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_DATA_ITEM_KAITEI_INFO_INDEX).Value = result_Vo.DataItemKaiteiInfo
            'データ項目・データ支給チェック欄
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_DATA_ITEM_DATA_PROVISION_INDEX).Value = result_Vo.DataItemDataProvision
            '取引先名称
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_MAKER_CODE_INDEX).Value = result_Vo.MakerCode
            '備考
            Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_BIKOU_INDEX).Value = result_Vo.Bikou

            rowIndex = rowIndex + 1
        Next
        Return True
    End Function

#End Region

#End Region

#Region "コンボボックス設定"

    Private Class BlockNoExtraction : Implements ILabelValueExtraction
        Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
            Dim vo As New TShisakuTehaiKihonVo
            aLocator.IsA(vo).Label(vo.ShisakuBlockNo).Value(vo.ShisakuBlockNo)
        End Sub
    End Class

    Public Function GetLabelValues_BlockNo() As List(Of LabelValueVo)
        Dim results As List(Of LabelValueVo) = _
            LabelValueExtracter(Of TShisakuTehaiKihonVo).Extract(_TehaiKihonVos, New BlockNoExtraction)
        results.Sort(New LabelValueComparer)
        Return results
    End Function

    Public Function GetLabelValues_BuhinNo() As List(Of LabelValueVo)
        Dim results As List(Of LabelValueVo) = _
            LabelValueExtracter(Of TShisakuTehaiKihonVo).Extract(_TehaiKihonVos, New BuhinNoExtraction)
        results.Sort(New LabelValueComparer)
        Return results
    End Function

    Private Class BuhinNoExtraction : Implements ILabelValueExtraction
        Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
            Dim vo As New TShisakuTehaiKihonVo
            aLocator.IsA(vo).Label(vo.BuhinNo).Value(vo.BuhinNo)
        End Sub
    End Class

    Public Function GetLabelValues_BuhinName() As List(Of LabelValueVo)
        Dim results As List(Of LabelValueVo) = _
            LabelValueExtracter(Of TShisakuTehaiKihonVo).Extract(_TehaiKihonVos, New BuhinNameExtraction)
        results.Sort(New LabelValueComparer)
        Return results
    End Function

    Private Class BuhinNameExtraction : Implements ILabelValueExtraction
        Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
            Dim vo As New TShisakuTehaiKihonVo
            aLocator.IsA(vo).Label(vo.BuhinName).Value(vo.BuhinName)
        End Sub
    End Class

    Public Function GetLabelValues_MakerCode() As List(Of LabelValueVo)
        Dim results As List(Of LabelValueVo) = _
            LabelValueExtracter(Of TShisakuTehaiKihonVo).Extract(_TehaiKihonVos, New MakerCodeExtraction)
        results.Sort(New LabelValueComparer)
        Return results
    End Function

    Private Class MakerCodeExtraction : Implements ILabelValueExtraction
        Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
            Dim vo As New TShisakuTehaiKihonVo
            aLocator.IsA(vo).Label(vo.TorihikisakiCode).Value(vo.TorihikisakiCode)
        End Sub
    End Class

    Public Function GetLabelValues_MakerName() As List(Of LabelValueVo)
        Dim results As List(Of LabelValueVo) = _
            LabelValueExtracter(Of TShisakuTehaiKihonVo).Extract(_TehaiKihonVos, New MakerNameExtraction)
        results.Sort(New LabelValueComparer)
        Return results
    End Function

    Private Class MakerNameExtraction : Implements ILabelValueExtraction
        Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
            Dim vo As New TShisakuTehaiKihonVo
            aLocator.IsA(vo).Label(vo.MakerCode).Value(vo.MakerCode)
        End Sub
    End Class

#End Region

#Region "戻るボタン"

    ''' <summary>
    ''' 戻るボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        _ResultOk = False
        Me.Close()
    End Sub

#End Region

#Region "選択ボタン"

    ''' <summary>
    ''' 選択ボタン処理．
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click

        Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(spdBase_Sheet1)
        Dim rowCount As Integer = 0
        For index As Integer = startRow To spdBase_Sheet1.RowCount - 1
            '選択チェックボックスに☑が付いて
            If spdBase_Sheet1.GetValue(index, COLUMN_SELECTED_INDEX) = True Then
                rowCount = rowCount + 1
            End If
        Next

        '選択列に☑が一つもついていない場合はエラー
        If rowCount = 0 Then
            ComFunc.ShowErrMsgBox("出力対象にチェックを付けてください。")
            Return
        End If

        '選択情報SPREADより選択列に☑を付けたデータを抽出
        GetSpreadSelectVos()

        _ResultOk = True
        Me.Close()
    End Sub

#End Region

#Region "コンボボックス変更"

    ''' <summary>
    ''' コンボボックスチェンジ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBlockNo.TextChanged, cmbBuhinNo.TextChanged, cmbBuhinName.TextChanged, cmbMakerCode.TextChanged, cmbMakerName.TextChanged
        DisabledNotEqualsRows(cmbBlockNo.Text, cmbBuhinNo.Text, cmbBuhinName.Text, cmbMakerCode.Text, cmbMakerName.Text)
    End Sub

    ''' <summary>
    ''' コンボボックスに　delete　key　press
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Controls_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbBlockNo.KeyDown, cmbBuhinNo.KeyDown, cmbBuhinName.KeyDown, cmbMakerCode.KeyDown, cmbMakerName.KeyDown
        ShisakuFormUtil.DelKeyDown(sender, e)
    End Sub

#End Region

#Region "表示選択ラジオボタン"

    ''' <summary>
    ''' 全てラジオボタンをチェック後ン処理．
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadioButton1_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            ClearComboBox()
            VisibleSpreadRows(True)
        End If
    End Sub

    ''' <summary>
    ''' 出力対象のみラジオボタンをチェック後ン処理．
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadioButton2_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            ClearComboBox()
            VisibleSpreadRows(False)
        End If
    End Sub

#End Region

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
    End Sub

    ''' <summary>
    ''' コンボボックスをクリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearComboBox()
        cmbBlockNo.Text = ""
        cmbBuhinNo.Text = ""
        cmbBuhinName.Text = ""
        '工事指令書の場合、取引先コンボを追加
        If String.Equals(_DisplayFlag, "KojiShirei") Then
            cmbMakerCode.Text = ""
            cmbMakerName.Text = ""
        End If
    End Sub

    ''' <summary>
    ''' 条件が満足されない行を非表示
    ''' </summary>
    ''' <param name="blockNo"></param>
    ''' <param name="buhinNo"></param>
    ''' <param name="buhinName"></param>
    ''' <param name="makerCode"></param>
    ''' <param name="makerName"></param>
    ''' <remarks></remarks>
    Private Sub DisabledNotEqualsRows(ByVal blockNo As String, ByVal buhinNo As String, ByVal buhinName As String, _
                                      ByVal makerCode As String, ByVal makerName As String)
        Dim sheet As FarPoint.Win.Spread.SheetView = spdBase_Sheet1
        Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(sheet)

        For index As Integer = startRow To sheet.RowCount - 1
            sheet.SetRowVisible(index, True)
            Dim strBlockNo = sheet.GetText(index, COLUMN_SHISAKU_BLOCK_NO_INDEX).Trim
            Dim strBuhinNo = sheet.GetText(index, COLUMN_BUHIN_NO_INDEX).Trim
            Dim strBuhinName = sheet.GetText(index, COLUMN_BUHIN_NAME_INDEX).Trim
            '工事指令書の場合、取引先コンボを追加
            If String.Equals(_DisplayFlag, "KojiShirei") Then
                Dim strMakerCode = sheet.GetText(index, COLUMN_TORIHIKISAKI_CODE_INDEX).Trim
                Dim strMakerName = sheet.GetText(index, COLUMN_MAKER_CODE_INDEX).Trim
                If (StringUtil.IsNotEmpty(blockNo) AndAlso Not strBlockNo.StartsWith(blockNo)) OrElse _
                   (StringUtil.IsNotEmpty(buhinNo) AndAlso Not strBuhinNo.StartsWith(buhinNo)) OrElse _
                   (StringUtil.IsNotEmpty(buhinName) AndAlso Not strBuhinName.StartsWith(buhinName)) OrElse _
                   (StringUtil.IsNotEmpty(makerCode) AndAlso Not strMakerCode.StartsWith(makerCode)) OrElse _
                   (StringUtil.IsNotEmpty(makerName) AndAlso Not strMakerName.StartsWith(makerName)) Then
                    '条件に該当しない行は非表示にする。
                    sheet.SetRowVisible(index, False)
                End If
            Else
                If (StringUtil.IsNotEmpty(blockNo) AndAlso Not strBlockNo.StartsWith(blockNo)) OrElse _
                   (StringUtil.IsNotEmpty(buhinNo) AndAlso Not strBuhinNo.StartsWith(buhinNo)) OrElse _
                   (StringUtil.IsNotEmpty(buhinName) AndAlso Not strBuhinName.StartsWith(buhinName)) Then
                    '条件に該当しない行は非表示にする。
                    sheet.SetRowVisible(index, False)
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' スプレッドの各行の表示かどうか？
    ''' </summary>
    ''' <param name="allFlag"></param>
    ''' <remarks></remarks>
    Private Sub VisibleSpreadRows(ByVal allFlag As Boolean)
        Dim sheet As FarPoint.Win.Spread.SheetView = spdBase_Sheet1
        Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(sheet)

        For index As Integer = startRow To sheet.RowCount - 1
            '非表示にしていた行を全て表示する。
            sheet.SetRowVisible(index, True)
            If allFlag = False Then
                '選択チェックボックスに☑が付いていない行を非表示にする。
                If sheet.GetValue(index, COLUMN_SELECTED_INDEX) = False Then
                    sheet.SetRowVisible(index, False)
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' スプレッドから選択された行の情報を取得する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetSpreadSelectVos()
        _ResultList = New List(Of TShisakuTehaiKihonVo)

        Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(spdBase_Sheet1)

        For rowIndex As Integer = startRow To spdBase_Sheet1.RowCount - 1
            If spdBase_Sheet1.Cells(rowIndex, COLUMN_SELECTED_INDEX).Value = True Then
                Dim addVo As New TShisakuTehaiKihonVo
                'ブロック
                addVo.ShisakuBlockNo = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SHISAKU_BLOCK_NO_INDEX).Value
                '行ＩＤ
                addVo.GyouId = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_GYOU_ID_INDEX).Value
                'レベル
                addVo.Level = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_LEVEL_INDEX).Value
                '部品番号
                addVo.BuhinNo = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_BUHIN_NO_INDEX).Value
                '部品名称
                addVo.BuhinName = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_BUHIN_NAME_INDEX).Value
                '集計コード
                addVo.ShukeiCode = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SHUKEI_CODE_INDEX).Value
                '手配記号
                addVo.TehaiKigou = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_TEHAI_KIGOU_INDEX).Value
                '購坦
                addVo.Koutan = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_KOUTAN_INDEX).Value
                '取引先コード
                addVo.TorihikisakiCode = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_TORIHIKISAKI_CODE_INDEX).Value
                '納場
                addVo.Nouba = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_NOUBA_INDEX).Value
                '供給セクション
                addVo.KyoukuSection = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_KYOUKU_SECTION_INDEX).Value
                '納入指示日
                addVo.NounyuShijibi = TehaichoEditLogic.ConvInt8Date(Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_NOUNYU_SHIJIBI_INDEX).Value)
                '合計員数
                addVo.TotalInsuSuryo = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_TOTAL_INSU_SURYO_INDEX).Value
                '出図予定日
                addVo.ShutuzuYoteiDate = TehaichoEditLogic.ConvInt8Date(Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SHUTUZU_YOTEI_DATE_INDEX).Value)

                '出図実績_日付
                addVo.ShutuzuJisekiDate = TehaichoEditLogic.ConvInt8Date(Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SHUTUZU_JISEKI_DATE_INDEX).Value)
                '出図実績_改訂№
                addVo.ShutuzuJisekiKaiteiNo = TehaichoEditLogic.ConvInt8Date(Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SHUTUZU_JISEKI_KAITEI_NO_INDEX).Value)
                '出図実績_設通№
                addVo.ShutuzuJisekiStsrDhstba = TehaichoEditLogic.ConvInt8Date(Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SHUTUZU_JISEKI_STSR_DHSTBA_INDEX).Value)
                '最終織込設変情報_日付
                addVo.SaisyuSetsuhenDate = TehaichoEditLogic.ConvInt8Date(Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SAISYU_SETSUHEN_DATE_INDEX).Value)
                '最終織込設変情報_改訂№
                addVo.SaisyuSetsuhenKaiteiNo = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SAISYU_SETSUHEN_KAITEI_NO_INDEX).Value
                '最終織込設変情報_図面設通№
                addVo.StsrDhstba = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SAISYU_SETSUHEN_STSR_DHSTBA_INDEX).Value

                '材質・規格１
                addVo.ZaishituKikaku1 = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAISHITU_KIKAKU_1_INDEX).Value
                '材質・規格２
                addVo.ZaishituKikaku2 = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAISHITU_KIKAKU_2_INDEX).Value
                '材質・規格３
                addVo.ZaishituKikaku3 = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAISHITU_KIKAKU_3_INDEX).Value
                '材質・メッキ
                addVo.ZaishituMekki = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAISHITU_MEKKI_INDEX).Value
                '板厚
                addVo.ShisakuBankoSuryo = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SHISAKU_BANKO_SURYO_INDEX).Value
                '板厚・ｕ
                addVo.ShisakuBankoSuryoU = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_SHISAKU_BANKO_SURYO_U_INDEX).Value
                '材料情報・製品長
                addVo.MaterialInfoLength = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_MATERIAL_INFO_LENGTH_INDEX).Value
                '材料情報・製品幅
                addVo.MaterialInfoWidth = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_MATERIAL_INFO_WIDTH_INDEX).Value

                '材料寸法_X(mm)
                addVo.ZairyoSunpoX = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAIRYO_SUNPO_X_INDEX).Value
                '材料寸法_Y(mm)
                addVo.ZairyoSunpoY = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAIRYO_SUNPO_Y_INDEX).Value
                '材料寸法_Z(mm)
                addVo.ZairyoSunpoZ = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAIRYO_SUNPO_Z_INDEX).Value
                '材料寸法_X+Y(mm)
                addVo.ZairyoSunpoXy = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAIRYO_SUNPO_XY_INDEX).Value
                '材料寸法_X+Z(mm)
                addVo.ZairyoSunpoXz = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAIRYO_SUNPO_XZ_INDEX).Value
                '材料寸法_Y+Z(mm)
                addVo.ZairyoSunpoYz = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_ZAIRYO_SUNPO_YZ_INDEX).Value

                '材料情報・発注対象
                addVo.MaterialInfoOrderTargetDate = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_MATERIAL_INFO_ORDER_TARGET_INDEX).Value
                '材料情報・発注済
                addVo.MaterialInfoOrderChk = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_MATERIAL_INFO_ORDER_CHK_INDEX).Text
                'データ項目・改訂№
                addVo.DataItemKaiteiNo = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_DATA_ITEM_KAITEI_NO_INDEX).Value
                'データ項目・エリア名
                addVo.DataItemAreaName = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_DATA_ITEM_AREA_NAME_INDEX).Value
                'データ項目・セット名
                addVo.DataItemSetName = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_DATA_ITEM_SET_NAME_INDEX).Value
                'データ項目・改訂情報
                addVo.DataItemKaiteiInfo = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_DATA_ITEM_KAITEI_INFO_INDEX).Value
                'データ項目・データ支給チェック欄
                addVo.DataItemDataProvision = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_DATA_ITEM_DATA_PROVISION_INDEX).Text
                '取引先名称
                addVo.MakerCode = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_MAKER_CODE_INDEX).Value
                '備考
                addVo.Bikou = Me.spdBase_Sheet1.Cells(rowIndex, COLUMN_BIKOU_INDEX).Value

                _ResultList.Add(addVo)
            End If
        Next
    End Sub

End Class