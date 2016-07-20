Imports ShisakuCommon
Imports EventSakusei.ShisakuBuhinEdit.Ui
Imports EventSakusei.TehaichoEdit.Ui

Namespace TehaichoEdit
    ''' <summary>
    ''' 定数・固定値類
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TehaichoEditNames

#Region "定数"
        ''' <summary>Excel出力時既定ファイル名称(号車)</summary>
        Public Const EXCEL_GOUSYA_FILENAME As String = "OutputExcelGousyaWork.xls"
        ''' <summary>Excel出力時既定ファイル名称(基本)</summary>
        Public Const EXCEL_KIHON_FILENAME As String = "OutputExcelKihonWork.xls"
        '''<summury>号車列開始位置</summury>
        Public Const START_COLUMMN_GOUSYA_NAME As Integer = 10
        ''' <summary>動的号車列のTAG名接頭辞 </summary>
        Public Const PREFIX_GOUSHA_TAG As String = "SHISAKU_GOUSYA_"                'SHISAKU_GOUSYA_0～となる
#End Region

    End Class


#Region "スプレッドTAG(基本情報)"

    ''' <summary>
    ''' スプレッドTAG(基本情報)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmSpdTagBase
        'Private だったが、Publicに変更、なぜPrivate?
        '履歴
        Public Const TAG_RIREKI As String = "RIREKI"
        '試作部課コード
        Public Const TAG_SHISAKU_BUKA_CODE As String = "SHISAKU_BUKA_CODE"
        '試作ブロック№
        Public Const TAG_SHISAKU_BLOCK_NO As String = "SHISAKU_BLOCK_NO"
        '行ID
        Public Const TAG_GYOU_ID As String = "GYOU_ID"
        '専用マーク
        Public Const TAG_SENYOU_MARK As String = "SENYOU_MARK"
        'レベル
        Public Const TAG_LEVEL As String = "LEVEL"
        '部品番号表示順
        Public Const TAG_BUHIN_NO_HYOUJI_JUN As String = "BUHIN_NO_HYOUJI_JUN"
        '部品番号
        Public Const TAG_BUHIN_NO As String = "BUHIN_NO"
        '部品番号試作区分
        Public Const TAG_BUHIN_NO_KBN As String = "BUHIN_NO_KBN"
        '部品番号改訂№
        Public Const TAG_BUHIN_NO_KAITEI_NO As String = "BUHIN_NO_KAITEI_NO"
        '枝番
        Public Const TAG_EDA_BAN As String = "EDA_BAN"
        '部品名称
        Public Const TAG_BUHIN_NAME As String = "BUHIN_NAME"
        '集計コード
        Public Const TAG_SHUKEI_CODE As String = "SHUKEI_CODE"
        '手配記号
        Public Const TAG_TEHAI_KIGOU As String = "TEHAI_KIGOU"
        '購坦
        Public Const TAG_KOUTAN As String = "KOUTAN"
        '取引先コード
        Public Const TAG_TORIHIKISAKI_CODE As String = "TORIHIKISAKI_CODE"
        '納場
        Public Const TAG_NOUBA As String = "NOUBA"
        '供給セクション
        Public Const TAG_KYOUKU_SECTION As String = "KYOUKU_SECTION"
        '納入指示日
        Public Const TAG_NOUNYU_SHIJIBI As String = "NOUNYU_SHIJIBI"
        '合計員数
        Public Const TAG_TOTAL_INSU_SURYO As String = "TOTAL_INSU_SURYO"
        '再使用不可
        Public Const TAG_SAISHIYOUFUKA As String = "SAISHIYOUFUKA"
        '出図予定日
        Public Const TAG_SHUTUZU_YOTEI_DATE As String = "SHUTUZU_YOTEI_DATE"
        '出図実績_日付
        Public Const TAG_SHUTUZU_JISEKI_DATE As String = "SHUTUZU_JISEKI_DATE"
        '出図実績_改訂№
        Public Const TAG_SHUTUZU_JISEKI_KAITEI_NO As String = "SHUTUZU_JISEKI_KAITEI_NO"
        '出図実績_設通№
        Public Const TAG_SHUTUZU_JISEKI_STSR_DHSTBA As String = "SHUTUZU_JISEKI_STSR_DHSTBA"
        '最終織込設変情報_日付
        Public Const TAG_SAISYU_SETSUHEN_DATE As String = "SAISYU_SETSUHEN_DATE"
        '最終織込設変情報_改訂№
        Public Const TAG_SAISYU_SETSUHEN_KAITEI_NO As String = "SAISYU_SETSUHEN_KAITEI_NO"
        '最終織込設変情報_設通№
        Public Const TAG_SAISYU_SETSUHEN_STSR_DHSTBA As String = "SAISYU_SETSUHEN_STSR_DHSTBA"
        ''↓↓2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
        Public Const TAG_TSUKURIKATA_SEISAKU As String = "TSUKURIKATA_SEISAKU"
        Public Const TAG_TSUKURIKATA_KATASHIYOU_1 As String = "TSUKURIKATA_KATASHIYOU_1"
        Public Const TAG_TSUKURIKATA_KATASHIYOU_2 As String = "TSUKURIKATA_KATASHIYOU_2"
        Public Const TAG_TSUKURIKATA_KATASHIYOU_3 As String = "TSUKURIKATA_KATASHIYOU_3"
        Public Const TAG_TSUKURIKATA_TIGU As String = "TSUKURIKATA_TIGU"
        Public Const TAG_TSUKURIKATA_NOUNYU As String = "TSUKURIKATA_NOUNYU"
        Public Const TAG_TSUKURIKATA_KIBO As String = "TSUKURIKATA_KIBO"
        ''↑↑2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD END
        '材質・規格１
        Public Const TAG_ZAISHITU_KIKAKU_1 As String = "ZAISHITU_KIKAKU_1"
        '材質・規格２
        Public Const TAG_ZAISHITU_KIKAKU_2 As String = "ZAISHITU_KIKAKU_2"
        '材質・規格３
        Public Const TAG_ZAISHITU_KIKAKU_3 As String = "ZAISHITU_KIKAKU_3"
        '材質・メッキ
        Public Const TAG_ZAISHITU_MEKKI As String = "ZAISHITU_MEKKI"
        '板厚
        Public Const TAG_SHISAKU_BANKO_SURYO As String = "SHISAKU_BANKO_SURYO"
        '板厚・ｕ
        Public Const TAG_SHISAKU_BANKO_SURYO_U As String = "SHISAKU_BANKO_SURYO_U"
        '
        Public Const TAG_MATERIAL_INFO_LENGTH As String = "MATERIAL_INFO_LENGTH"
        Public Const TAG_MATERIAL_INFO_WIDTH As String = "MATERIAL_INFO_WIDTH"
        '材料寸法_X(mm)
        Public Const TAG_ZAIRYO_SUNPO_X As String = "ZAIRYO_SUNPO_X"
        '材料寸法_Y(mm)
        Public Const TAG_ZAIRYO_SUNPO_Y As String = "ZAIRYO_SUNPO_Y"
        '材料寸法_Z(mm)
        Public Const TAG_ZAIRYO_SUNPO_Z As String = "ZAIRYO_SUNPO_Z"
        '材料寸法_X+Y(mm)
        Public Const TAG_ZAIRYO_SUNPO_XY As String = "ZAIRYO_SUNPO_XY"
        '材料寸法_X+Z(mm)
        Public Const TAG_ZAIRYO_SUNPO_XZ As String = "ZAIRYO_SUNPO_XZ"
        '材料寸法_Y+Z(mm)
        Public Const TAG_ZAIRYO_SUNPO_YZ As String = "ZAIRYO_SUNPO_YZ"
        '
        Public Const TAG_MATERIAL_INFO_ORDER_TARGET As String = "MATERIAL_INFO_ORDER_TARGET"
        Public Const TAG_MATERIAL_INFO_ORDER_CHK As String = "MATERIAL_INFO_ORDER_CHK"
        '↓↓↓2014/12/26 メタル項目を追加 TES)張 ADD BEGIN
        Public Const TAG_DATA_ITEM_KAITEI_NO As String = "DATA_ITEM_KAITEI_NO"
        Public Const TAG_DATA_ITEM_AREA_NAME As String = "DATA_ITEM_AREA_NAME"
        Public Const TAG_DATA_ITEM_SET_NAME As String = "DATA_ITEM_SET_NAME"
        Public Const TAG_DATA_ITEM_KAITEI_INFO As String = "DATA_ITEM_KAITEI_INFO"
        Public Const TAG_DATA_ITEM_DATA_PROVISION As String = "DATA_ITEM_DATA_PROVISION"
        '↑↑↑2014/12/26 メタル項目を追加 TES)張 ADD END
        '試作部品費（円）
        Public Const TAG_SHISAKU_BUHINN_HI As String = "SHISAKU_BUHINN_HI"
        '試作型費（千円）
        Public Const TAG_SHISAKU_KATA_HI As String = "SHISAKU_KATA_HI"
        '取引先名称
        Public Const TAG_MAKER_CODE As String = "MAKER_CODE"
        '備考
        Public Const TAG_BIKOU As String = "BIKOU"
        '親部品
        Public Const TAG_BUHIN_NO_OYA As String = "BUHIN_NO_OYA"
        '親部品試作区分
        Public Const TAG_BUHIN_NO_KBN_OYA As String = "BUHIN_NO_KBN_OYA"
        '変化点
        Public Const TAG_HENKATEN As String = "HENKATEN"
        '自動織込み改訂No'
        Public Const TAG_AUTO_ORIKOMI_KAITEI_NO As String = "AUTO_ORIKOMI_KAITEI_NO"


        ''' <summary>
        ''' TAG名称設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub initTagName(ByVal sheet As FarPoint.Win.Spread.SheetView)
            Dim index As Integer = 0

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_RIREKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_BUKA_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_BLOCK_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GYOU_ID
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SENYOU_MARK
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_LEVEL
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO_HYOUJI_JUN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO_KBN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO_KAITEI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EDA_BAN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHUKEI_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TEHAI_KIGOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KOUTAN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TORIHIKISAKI_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_NOUBA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KYOUKU_SECTION
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_NOUNYU_SHIJIBI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TOTAL_INSU_SURYO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SAISHIYOUFUKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHUTUZU_YOTEI_DATE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHUTUZU_JISEKI_DATE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHUTUZU_JISEKI_KAITEI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHUTUZU_JISEKI_STSR_DHSTBA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SAISYU_SETSUHEN_DATE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SAISYU_SETSUHEN_KAITEI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SAISYU_SETSUHEN_STSR_DHSTBA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TSUKURIKATA_SEISAKU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TSUKURIKATA_KATASHIYOU_1
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TSUKURIKATA_KATASHIYOU_2
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TSUKURIKATA_KATASHIYOU_3
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TSUKURIKATA_TIGU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TSUKURIKATA_NOUNYU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TSUKURIKATA_KIBO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_ZAISHITU_KIKAKU_1
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_ZAISHITU_KIKAKU_2
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_ZAISHITU_KIKAKU_3
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_ZAISHITU_MEKKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_BANKO_SURYO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_BANKO_SURYO_U
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MATERIAL_INFO_LENGTH
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MATERIAL_INFO_WIDTH
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_ZAIRYO_SUNPO_X
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_ZAIRYO_SUNPO_Y
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_ZAIRYO_SUNPO_Z
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_ZAIRYO_SUNPO_XY
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_ZAIRYO_SUNPO_XZ
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_ZAIRYO_SUNPO_YZ
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MATERIAL_INFO_ORDER_TARGET
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MATERIAL_INFO_ORDER_CHK
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_DATA_ITEM_KAITEI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_DATA_ITEM_AREA_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_DATA_ITEM_SET_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_DATA_ITEM_KAITEI_INFO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_DATA_ITEM_DATA_PROVISION
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_BUHINN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MAKER_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BIKOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO_OYA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO_KBN_OYA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_HENKATEN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_AUTO_ORIKOMI_KAITEI_NO

            Dim TcellTypeFactory As New TehaichoCellTypeFactory

            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SENYOU_MARK, TcellTypeFactory.SenyouMarkCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_LEVEL, TcellTypeFactory.LevelCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NO, TcellTypeFactory.BuhinNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NO_KBN, TcellTypeFactory.BuhinNoKbnCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NO_KAITEI_NO, TcellTypeFactory.BuhinNoKaiteiNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_EDA_BAN, TcellTypeFactory.EdaBanCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NAME, TcellTypeFactory.BuhinNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KOUTAN, TcellTypeFactory.KoutanCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_TORIHIKISAKI_CODE, TcellTypeFactory.MakerCodeCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_NOUBA, TcellTypeFactory.NoubaCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KYOUKU_SECTION, TcellTypeFactory.KyoukuSectionCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SAISHIYOUFUKA, TcellTypeFactory.SaishiyoufukaCellType)
            '
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SHUTUZU_JISEKI_KAITEI_NO, TcellTypeFactory.ShutuzuJisekiKaiteiNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SHUTUZU_JISEKI_STSR_DHSTBA, TcellTypeFactory.ShutuzuJisekiStsrDhstbaCellType)
            '
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SAISYU_SETSUHEN_KAITEI_NO, TcellTypeFactory.SaisyuSetsuhenKaiteiNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SAISYU_SETSUHEN_STSR_DHSTBA, TcellTypeFactory.SaisyuSetsuhenStsrDhstbaCellType)
            '
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_ZAIRYO_SUNPO_X, TcellTypeFactory.ZairyoSunpoXCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_ZAIRYO_SUNPO_Y, TcellTypeFactory.ZairyoSunpoYCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_ZAIRYO_SUNPO_Z, TcellTypeFactory.ZairyoSunpoZCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_ZAIRYO_SUNPO_XY, TcellTypeFactory.ZairyoSunpoXyCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_ZAIRYO_SUNPO_XZ, TcellTypeFactory.ZairyoSunpoXzCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_ZAIRYO_SUNPO_YZ, TcellTypeFactory.ZairyoSunpoYzCellType)

            ''↓↓2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            'BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_TSUKURIKATA_SEISAKU, TcellTypeFactory.TsukurikataSeisakuCellType)
            'BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_TSUKURIKATA_KATASHIYOU_1, TcellTypeFactory.TsukurikataKatashiyou1CellType)
            'BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_TSUKURIKATA_KATASHIYOU_2, TcellTypeFactory.TsukurikataKatashiyou2CellType)
            'BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_TSUKURIKATA_KATASHIYOU_3, TcellTypeFactory.TsukurikataKatashiyou3CellType)
            'BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_TSUKURIKATA_TIGU, TcellTypeFactory.TsukurikataTiguCellType)
            'BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_TSUKURIKATA_NOUNYU, TcellTypeFactory.TsukurikataNounyuCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_TSUKURIKATA_KIBO, TcellTypeFactory.TsukurikataKiboCellType)
            ''↑↑2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD END

            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_ZAISHITU_KIKAKU_1, TcellTypeFactory.ZaishituKikaku1CellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_ZAISHITU_KIKAKU_2, TcellTypeFactory.ZaishituKikaku2CellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_ZAISHITU_KIKAKU_3, TcellTypeFactory.ZaishituKikaku3CellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_ZAISHITU_MEKKI, TcellTypeFactory.ZaishituMekkiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SHISAKU_BANKO_SURYO, TcellTypeFactory.ShisakuBankoSuryoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SHISAKU_BANKO_SURYO_U, TcellTypeFactory.ShisakuBankoSuryoUCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SHISAKU_BUHINN_HI, TcellTypeFactory.ShisakuBuhinHiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SHISAKU_KATA_HI, TcellTypeFactory.ShisakuKataHiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MAKER_CODE, TcellTypeFactory.MakerNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BIKOU, TcellTypeFactory.BikouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NO_OYA, TcellTypeFactory.BuhinNoOyaCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NO_KBN_OYA, TcellTypeFactory.BuhinNoKbnOyaCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_HENKATEN, TcellTypeFactory.HenkatenCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_AUTO_ORIKOMI_KAITEI_NO, TcellTypeFactory.HenkatenCellType)

            '↓↓↓2014/12/26 メタル項目を追加 TES)張 ADD BEGIN
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SAISYU_SETSUHEN_KAITEI_NO, TcellTypeFactory.SaisyuSetsuhenKaiteiNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MATERIAL_INFO_LENGTH, TcellTypeFactory.MaterialInfoLengthCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MATERIAL_INFO_WIDTH, TcellTypeFactory.MaterialInfoWidthCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MATERIAL_INFO_ORDER_TARGET, TcellTypeFactory.MaterialInfoOrderTargetCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MATERIAL_INFO_ORDER_CHK, TcellTypeFactory.MaterialInfoOrderChkCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_DATA_ITEM_KAITEI_NO, TcellTypeFactory.DataItemKaiteiNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_DATA_ITEM_AREA_NAME, TcellTypeFactory.DataItemAreaNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_DATA_ITEM_SET_NAME, TcellTypeFactory.DataItemSetNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_DATA_ITEM_KAITEI_INFO, TcellTypeFactory.DataItemKaiteiInfoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_DATA_ITEM_DATA_PROVISION, TcellTypeFactory.DataItemDataProvisionCellType)
            '↑↑↑2014/12/26 メタル項目を追加 TES)張 ADD END
        End Sub

    End Class
#End Region

#Region "スプレッドTAG(号車情報)"
    ''' <summary>
    ''' データテーブル列名(号車情報)
    ''' 
    ''' ※号車列名称はPREFIX_GOUSHA_TAGに000からの連番となる
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmSpdTagGousya

        '履歴
        Public Const TAG_RIREKI As String = "RIREKI"
        '試作部課コード
        Public Const TAG_SHISAKU_BUKA_CODE As String = "SHISAKU_BUKA_CODE"
        '試作ブロック№
        Public Const TAG_SHISAKU_BLOCK_NO As String = "SHISAKU_BLOCK_NO"
        '行ID
        Public Const TAG_GYOU_ID As String = "GYOU_ID"
        '専用マーク
        Public Const TAG_SENYOU_MARK As String = "SENYOU_MARK"
        'レベル
        Public Const TAG_LEVEL As String = "LEVEL"
        '部品番号表示順
        Public Const TAG_BUHIN_NO_HYOUJI_JUN As String = "BUHIN_NO_HYOUJI_JUN"
        '部品番号
        Public Const TAG_BUHIN_NO As String = "BUHIN_NO"
        '部品番号試作区分
        Public Const TAG_BUHIN_NO_KBN As String = "BUHIN_NO_KBN"
        '員数差
        Public Const TAG_INSU_SA As String = "INSU_SA"

        ''' <summary>
        ''' TAG名称設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub initTagName(ByVal sheet As FarPoint.Win.Spread.SheetView)
            Dim index As Integer = 0
            index += 1
            '基本情報との共通項目
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_RIREKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_BUKA_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_BLOCK_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GYOU_ID
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SENYOU_MARK
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_LEVEL
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO_KBN

            '号車専用項目
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_INSU_SA

        End Sub
    End Class


#End Region

#Region "スプレッドTAG(号車別納期設定情報)"

    ''' <summary>
    ''' スプレッドTAG(号車別納期設定情報)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmSpdTagGousyaNoukiSettei
        '号車
        Public Const TAG_SHISAKU_GOUSYA As String = "SHISAKU_GOUSYA"
        'トリム納入指示日
        Public Const TAG_T_NOUNYU_SHIJIBI As String = "T_NOUNYU_SHIJIBI"
        'メタル納入指示日
        Public Const TAG_M_NOUNYU_SHIJIBI As String = "M_NOUNYU_SHIJIBI"

        ''' <summary>
        ''' TAG名称設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub initTagName(ByVal sheet As FarPoint.Win.Spread.SheetView)
            Dim index As Integer = 0

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_GOUSYA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_T_NOUNYU_SHIJIBI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_M_NOUNYU_SHIJIBI

        End Sub

    End Class
#End Region


#Region "スプレッドTAG(部品表EXCEL出力)"

    ''' <summary>
    ''' スプレッドTAG(部品表EXCEL出力)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmSpdTagGousyaBuhinhyoExcel
        '号車
        Public Const TAG_SHISAKU_GOUSYA As String = "SHISAKU_GOUSYA"
        'グループ
        Public Const TAG_SHISAKU_GOUSYA_GROUP As String = "SHISAKU_GOUSYA_GROUP"

        ''' <summary>
        ''' TAG名称設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub initTagName(ByVal sheet As FarPoint.Win.Spread.SheetView)
            Dim index As Integer = 0

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_GOUSYA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_GOUSYA_GROUP

        End Sub

    End Class
#End Region



#Region "スプレッドTAG(出図最新と織込みの差情報)"

    ''' <summary>
    ''' スプレッドTAG(出図最新と織込みの差情報)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmSpdTagShutuzuOrikomiSa


        '表示
        Public Const TAG_HYOJI As String = "HYOJI"
        '確定
        Public Const TAG_KAKUTEI As String = "KAKUTEI"
        'ブロック
        Public Const TAG_SHISAKU_BLOCK_NO As String = "SHISAKU_BLOCK_NO"
        '行ID
        Public Const TAG_GYOU_ID As String = "GYOU_ID"
        '部品番号
        Public Const TAG_BUHIN_NO As String = "BUHIN_NO"
        '代表品番
        Public Const TAG_KATA_DAIHYOU_BUHIN_NO As String = "KATA_DAIHYOU_BUHIN_NO"
        '最新出図_受領日
        Public Const TAG_NEW_SHUTUZU_JISEKI_JYURYO_DATE As String = "NEW_SHUTUZU_JISEKI_JYURYO_DATE"
        '最新出図_改訂№
        Public Const TAG_NEW_SHUTUZU_JISEKI_KAITEI_NO As String = "NEW_SHUTUZU_JISEKI_KAITEI_NO"
        '最新出図_設通№
        Public Const TAG_NEW_SHUTUZU_JISEKI_STSR_DHSTBA As String = "NEW_SHUTUZU_JISEKI_STSR_DHSTBA"
        '最新出図_件名
        Public Const TAG_NEW_SHUTUZU_KENMEI As String = "NEW_SHUTUZU_KENMEI"
        '最終織込設変_受領日
        Public Const TAG_LAST_SHUTUZU_JISEKI_JYURYO_DATE As String = "LAST_SHUTUZU_JISEKI_JYURYO_DATE"
        '最終織込設変_改訂№
        Public Const TAG_LAST_SHUTUZU_JISEKI_KAITEI_NO As String = "LAST_SHUTUZU_JISEKI_KAITEI_NO"
        '最終織込設変_設通№
        Public Const TAG_LAST_SHUTUZU_JISEKI_STSR_DHSTBA As String = "LAST_SHUTUZU_JISEKI_STSR_DHSTBA"
        '最終織込設変_件名
        Public Const TAG_LAST_SHUTUZU_KENMEI As String = "LAST_SHUTUZU_KENMEI"

        ''' <summary>
        ''' TAG名称設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub initTagName(ByVal sheet As FarPoint.Win.Spread.SheetView)
            Dim index As Integer = 0

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_HYOJI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKUTEI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_BLOCK_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GYOU_ID
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KATA_DAIHYOU_BUHIN_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_NEW_SHUTUZU_JISEKI_KAITEI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_NEW_SHUTUZU_JISEKI_STSR_DHSTBA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_NEW_SHUTUZU_JISEKI_JYURYO_DATE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_NEW_SHUTUZU_KENMEI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_LAST_SHUTUZU_JISEKI_KAITEI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_LAST_SHUTUZU_JISEKI_STSR_DHSTBA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_LAST_SHUTUZU_JISEKI_JYURYO_DATE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_LAST_SHUTUZU_KENMEI

        End Sub

    End Class
#End Region


#Region "スプレッドTAG(出図最新情報)"

    ''' <summary>
    ''' スプレッドTAG(出図最新情報)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmSpdTagShutuzuJiseki
        '表示
        Public Const TAG_HYOJI As String = "HYOJI"
        'ブロック
        Public Const TAG_SHISAKU_BLOCK_NO As String = "SHISAKU_BLOCK_NO"
        '部品番号
        Public Const TAG_BUHIN_NO As String = "BUHIN_NO"
        '代表品番
        Public Const TAG_KATA_DAIHYOU_BUHIN_NO As String = "KATA_DAIHYOU_BUHIN_NO"
        '最新出図_改訂№
        Public Const TAG_NEW_SHUTUZU_JISEKI_KAITEI_NO As String = "NEW_SHUTUZU_JISEKI_KAITEI_NO"
        '最新出図_設通№
        Public Const TAG_NEW_SHUTUZU_JISEKI_STSR_DHSTBA As String = "NEW_SHUTUZU_JISEKI_STSR_DHSTBA"
        '最新出図_受領日
        Public Const TAG_NEW_SHUTUZU_JISEKI_JYURYO_DATE As String = "NEW_SHUTUZU_JISEKI_JYURYO_DATE"
        '最新出図_件名
        Public Const TAG_NEW_SHUTUZU_KENMEI As String = "NEW_SHUTUZU_KENMEI"

        ''' <summary>
        ''' TAG名称設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub initTagName(ByVal sheet As FarPoint.Win.Spread.SheetView)
            Dim index As Integer = 0

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_HYOJI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_BLOCK_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KATA_DAIHYOU_BUHIN_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_NEW_SHUTUZU_JISEKI_KAITEI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_NEW_SHUTUZU_JISEKI_STSR_DHSTBA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_NEW_SHUTUZU_JISEKI_JYURYO_DATE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_NEW_SHUTUZU_KENMEI

        End Sub

    End Class
#End Region




#Region "スプレッドTAG(出図改訂履歴情報)"

    ''' <summary>
    ''' スプレッドTAG(出図改訂履歴情報)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmSpdTagShutuzuKaiteiRireki
        '織込
        Public Const TAG_ORIKOMI As String = "ORIKOMI"
        '出図_改訂№
        Public Const TAG_SHUTUZU_JISEKI_KAITEI_NO As String = "SHUTUZU_JISEKI_KAITEI_NO"
        '出図_設通№
        Public Const TAG_SHUTUZU_JISEKI_STSR_DHSTBA As String = "SHUTUZU_JISEKI_STSR_DHSTBA"
        '出図_受領日
        Public Const TAG_SHUTUZU_JISEKI_JYURYO_DATE As String = "SHUTUZU_JISEKI_JYURYO_DATE"
        '出図_件名
        Public Const TAG_SHUTUZU_KENMEI As String = "SHUTUZU_KENMEI"
        '出図_コメント
        Public Const TAG_COMMENT As String = "COMMENT"

        ''' <summary>
        ''' TAG名称設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub initTagName(ByVal sheet As FarPoint.Win.Spread.SheetView)
            Dim index As Integer = 0

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_ORIKOMI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHUTUZU_JISEKI_KAITEI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHUTUZU_JISEKI_STSR_DHSTBA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHUTUZU_JISEKI_JYURYO_DATE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHUTUZU_KENMEI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_COMMENT

        End Sub

    End Class
#End Region




#Region "データテーブル列名(RHAC0080)"
    ''' <summary>
    ''' データテーブル列名(RHAC0080)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmTDColRhac0080
        Public Const KAIHATSU_FUGO As String = "KAIHATSU_FUGO"
        Public Const BLOCK_NO_KINO As String = "BLOCK_NO_KINO"
        Public Const KAITEI_NO_KINO As String = "KAITEI_NO_KINO"
        Public Const TSUIKARIYU_KIJUTSU As String = "TSUIKARIYU_KIJUTSU"
        Public Const TANTO_BUSHO As String = "TANTO_BUSHO"
        Public Const KINO_SHIKI_KBN As String = "KINO_SHIKI_KBN"
        Public Const BUI_CODE As String = "BUI_CODE"
        Public Const UL_KBN As String = "UL_KBN"
        Public Const MT_KBN As String = "MT_KBN"
        Public Const KAIHATSUFG_DSG As String = "KAIHATSUFG_DSG"
        Public Const SHIYOSHO_SEQNO_DSG As String = "SHIYOSHO_SEQNO_DSG"
        Public Const SYSTEM_KBN_ID As String = "SYSTEM_KBN_ID"
        Public Const STATUS As String = "STATUS"
        Public Const SITE_KBN As String = "SITE_KBN"
        Public Const SAIYO_DATE As String = "SAIYO_DATE"
        Public Const HAISI_DATE As String = "HAISI_DATE"
    End Class
#End Region

#Region "'データテーブル列名集計コードマスタ参照用"
    ''' <summary>
    ''' 集計コードマスタ参照用
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmSyukei
        Public Const SYUKEI_CODE As String = "SYUKEI_CODE"
        Public Const SYUKEI_NAME As String = "SYUKEI_NAME"
    End Class

#End Region

#Region "データテーブル列名(AS_ARPF04テーブル)"
    ''' <summary>
    ''' 手配記号マスタ(AS_ARPF04テーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmTDColTehaiKigou

        Public Const TEAHAI_KIGOU As String = "TIKG"

    End Class

#End Region

#Region "データテーブル列名(試作イベントテーブル)"
    ''' <summary>
    ''' 試作イベントテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmTdColShisakuEvent

        Public Const SHISAKU_EVENT_CODE As String = "SHISAKU_EVENT_CODE"
        Public Const UNIT_KBN As String = "UNIT_KBN"

    End Class
#End Region

    ''↓↓2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD BEGIN

#Region "データテーブル列名(作り方項目マスタ)"
    ''' <summary>
    ''' 作り方項目マスタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmTsukurikata

        Public Const TSUKURIKATA_NO As String = "TSUKURIKATA_NO"
        Public Const TSUKURIKATA_NAME As String = "TSUKURIKATA_NAME"

    End Class
#End Region
    ''↑↑2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD END
#Region "データテーブル列名(基本情報)"
    ''' <summary>
    ''' データテーブル列名(基本情報)
    ''' ※
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmDTColBase
        '試作リストイベントコード
        Public Const TD_SHISAKU_EVENT_CODE As String = "SHISAKU_EVENT_CODE"
        '試作リストコード
        Public Const TD_SHISAKU_LIST_CODE As String = "SHISAKU_LIST_CODE"
        '試作リストコード改訂No
        Public Const TD_SHISAKU_LIST_CODE_KAITEI_NO As String = "SHISAKU_LIST_CODE_KAITEI_NO"
        '試作部課コード
        Public Const TD_SHISAKU_BUKA_CODE As String = "SHISAKU_BUKA_CODE"
        '履歴
        Public Const TD_RIREKI As String = "RIREKI"
        '試作ブロック№
        Public Const TD_SHISAKU_BLOCK_NO As String = "SHISAKU_BLOCK_NO"
        '行ID
        Public Const TD_GYOU_ID As String = "GYOU_ID"
        '専用マーク
        Public Const TD_SENYOU_MARK As String = "SENYOU_MARK"
        'レベル
        Public Const TD_LEVEL As String = "LEVEL"
        '部品番号表示順 
        Public Const TD_BUHIN_NO_HYOUJI_JUN As String = "BUHIN_NO_HYOUJI_JUN"
        '部品番号
        Public Const TD_BUHIN_NO As String = "BUHIN_NO"
        '部品番号試作区分
        Public Const TD_BUHIN_NO_KBN As String = "BUHIN_NO_KBN"
        '部品番号改訂№
        Public Const TD_BUHIN_NO_KAITEI_NO As String = "BUHIN_NO_KAITEI_NO"
        '枝番
        Public Const TD_EDA_BAN As String = "EDA_BAN"
        '部品名称
        Public Const TD_BUHIN_NAME As String = "BUHIN_NAME"
        '集計コード
        Public Const TD_SHUKEI_CODE As String = "SHUKEI_CODE"
        '手配記号
        Public Const TD_TEHAI_KIGOU As String = "TEHAI_KIGOU"
        '購坦
        Public Const TD_KOUTAN As String = "KOUTAN"
        '取引先コード
        Public Const TD_TORIHIKISAKI_CODE As String = "TORIHIKISAKI_CODE"
        '納場
        Public Const TD_NOUBA As String = "NOUBA"
        '供給セクション
        Public Const TD_KYOUKU_SECTION As String = "KYOUKU_SECTION"
        '納入指示日
        Public Const TD_NOUNYU_SHIJIBI As String = "NOUNYU_SHIJIBI"
        '合計員数
        Public Const TD_TOTAL_INSU_SURYO As String = "TOTAL_INSU_SURYO"
        '再使用不可
        Public Const TD_SAISHIYOUFUKA As String = "SAISHIYOUFUKA"
        '出図予定日
        Public Const TD_SHUTUZU_YOTEI_DATE As String = "SHUTUZU_YOTEI_DATE"

        '出図実績_日付
        Public Const TD_SHUTUZU_JISEKI_DATE As String = "SHUTUZU_JISEKI_DATE"
        '出図実績_改訂№
        Public Const TD_SHUTUZU_JISEKI_KAITEI_NO As String = "SHUTUZU_JISEKI_KAITEI_NO"
        '出図実績_設通№
        Public Const TD_SHUTUZU_JISEKI_STSR_DHSTBA As String = "SHUTUZU_JISEKI_STSR_DHSTBA"
        '最終織込設変情報_日付
        Public Const TD_SAISYU_SETSUHEN_DATE As String = "SAISYU_SETSUHEN_DATE"
        '最終織込設変情報_改訂№
        Public Const TD_SAISYU_SETSUHEN_KAITEI_NO As String = "SAISYU_SETSUHEN_KAITEI_NO"
        '最終織込設変情報_設通№
        Public Const TD_SAISYU_SETSUHEN_STSR_DHSTBA As String = "STSR_DHSTBA"
        '材料寸法_X(mm)
        Public Const TD_ZAIRYO_SUNPO_X As String = "ZAIRYO_SUNPO_X"
        '材料寸法_Y(mm)
        Public Const TD_ZAIRYO_SUNPO_Y As String = "ZAIRYO_SUNPO_Y"
        '材料寸法_Z(mm)
        Public Const TD_ZAIRYO_SUNPO_Z As String = "ZAIRYO_SUNPO_Z"
        '材料寸法_X+Y(mm)
        Public Const TD_ZAIRYO_SUNPO_XY As String = "ZAIRYO_SUNPO_XY"
        '材料寸法_X+Z(mm)
        Public Const TD_ZAIRYO_SUNPO_XZ As String = "ZAIRYO_SUNPO_XZ"
        '材料寸法_YZ(mm)
        Public Const TD_ZAIRYO_SUNPO_YZ As String = "ZAIRYO_SUNPO_YZ"


        '材質・規格１
        Public Const TD_ZAISHITU_KIKAKU_1 As String = "ZAISHITU_KIKAKU_1"
        '材質・規格２
        Public Const TD_ZAISHITU_KIKAKU_2 As String = "ZAISHITU_KIKAKU_2"
        '材質・規格３
        Public Const TD_ZAISHITU_KIKAKU_3 As String = "ZAISHITU_KIKAKU_3"
        '材質・メッキ
        Public Const TD_ZAISHITU_MEKKI As String = "ZAISHITU_MEKKI"

        ''↓↓2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
        Public Const TD_TSUKURIKATA_SEISAKU As String = "TSUKURIKATA_SEISAKU"
        Public Const TD_TSUKURIKATA_KATASHIYOU_1 As String = "TSUKURIKATA_KATASHIYOU_1"
        Public Const TD_TSUKURIKATA_KATASHIYOU_2 As String = "TSUKURIKATA_KATASHIYOU_2"
        Public Const TD_TSUKURIKATA_KATASHIYOU_3 As String = "TSUKURIKATA_KATASHIYOU_3"
        Public Const TD_TSUKURIKATA_TIGU As String = "TSUKURIKATA_TIGU"
        Public Const TD_TSUKURIKATA_NOUNYU As String = "TSUKURIKATA_NOUNYU"
        Public Const TD_TSUKURIKATA_KIBO As String = "TSUKURIKATA_KIBO"
        ''↑↑2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD END

        '板厚
        Public Const TD_SHISAKU_BANKO_SURYO As String = "SHISAKU_BANKO_SURYO"
        '板厚・ｕ
        Public Const TD_SHISAKU_BANKO_SURYO_U As String = "SHISAKU_BANKO_SURYO_U"
        '試作部品費（円）
        Public Const TD_SHISAKU_BUHINN_HI As String = "SHISAKU_BUHINN_HI"
        '試作型費（千円）
        Public Const TD_SHISAKU_KATA_HI As String = "SHISAKU_KATA_HI"
        '取引先名称
        Public Const TD_MAKER_CODE As String = "MAKER_CODE"
        '備考
        Public Const TD_BIKOU As String = "BIKOU"
        '親部品
        Public Const TD_BUHIN_NO_OYA As String = "BUHIN_NO_OYA"
        '親部品試作区分
        Public Const TD_BUHIN_NO_KBN_OYA As String = "BUHIN_NO_KBN_OYA"
        '変化点
        Public Const TD_HENKATEN As String = "HENKATEN"
        '自動織込み改訂No'
        Public Const TD_AUTO_ORIKOMI_KAITEI_NO As String = "AUTO_ORIKOMI_KAITEI_NO"

        '↓↓↓2014/12/26 メタル項目を追加 TES)張 ADD BEGIN
        Public Const TD_MATERIAL_INFO_LENGTH As String = "MATERIAL_INFO_LENGTH"
        Public Const TD_MATERIAL_INFO_WIDTH As String = "MATERIAL_INFO_WIDTH"
        Public Const TD_MATERIAL_INFO_ORDER_TARGET As String = "MATERIAL_INFO_ORDER_TARGET"
        Public Const TD_MATERIAL_INFO_ORDER_TARGET_DATE As String = "MATERIAL_INFO_ORDER_TARGET_DATE"
        Public Const TD_MATERIAL_INFO_ORDER_CHK As String = "MATERIAL_INFO_ORDER_CHK"
        Public Const TD_MATERIAL_INFO_ORDER_CHK_DATE As String = "MATERIAL_INFO_ORDER_CHK_DATE"
        Public Const TD_DATA_ITEM_KAITEI_NO As String = "DATA_ITEM_KAITEI_NO"
        Public Const TD_DATA_ITEM_AREA_NAME As String = "DATA_ITEM_AREA_NAME"
        Public Const TD_DATA_ITEM_SET_NAME As String = "DATA_ITEM_SET_NAME"
        Public Const TD_DATA_ITEM_KAITEI_INFO As String = "DATA_ITEM_KAITEI_INFO"
        Public Const TD_DATA_ITEM_DATA_PROVISION As String = "DATA_ITEM_DATA_PROVISION"
        Public Const TD_DATA_ITEM_DATA_PROVISION_DATE As String = "DATA_ITEM_DATA_PROVISION_DATE"
        '↑↑↑2014/12/26 メタル項目を追加 TES)張 ADD END
    End Class



#End Region

#Region "データテーブル列名(号車情報)"
    ''' <summary>
    ''' データテーブル列名(号車情報)
    ''' 
    ''' ※号車列名称はPREFIX_GOUSHA_TAGに000からの連番となる
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmDTColGousya
        '試作リストイベントコード
        Public Const TD_SHISAKU_EVENT_CODE As String = "SHISAKU_EVENT_CODE"
        '試作リストコード
        Public Const TD_SHISAKU_LIST_CODE As String = "SHISAKU_LIST_CODE"
        '試作リストコード改訂No
        Public Const TD_SHISAKU_LIST_CODE_KAITEI_NO As String = "SHISAKU_LIST_CODE_KAITEI_NO"
        '試作部課コード
        Public Const TD_SHISAKU_BUKA_CODE As String = "SHISAKU_BUKA_CODE"
        '履歴
        Public Const TD_RIREKI As String = "RIREKI"
        '試作ブロック№
        Public Const TD_SHISAKU_BLOCK_NO As String = "SHISAKU_BLOCK_NO"
        '行ID
        Public Const TD_GYOU_ID As String = "GYOU_ID"
        '専用マーク
        Public Const TD_SENYOU_MARK As String = "SENYOU_MARK"
        'レベル
        Public Const TD_LEVEL As String = "LEVEL"
        '部品番号表示順 
        Public Const TD_BUHIN_NO_HYOUJI_JUN As String = "BUHIN_NO_HYOUJI_JUN"
        '部品番号
        Public Const TD_BUHIN_NO As String = "BUHIN_NO"
        '部品番号試作区分
        Public Const TD_BUHIN_NO_KBN As String = "BUHIN_NO_KBN"

    End Class

#End Region

#Region "データテーブル列名(号車名称一覧)"
    ''' <summary>
    ''' データテーブル列名（号車名称一覧)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmDTColGousyaNameList

        '試作イベントコード
        Public Const TD_SHISAKU_EVENT_CODE = "SHISAKU_EVENT_CODE"
        '表示順
        Public Const TD_HYOJIJUN_NO = "HYOJIJUN_NO"
        '試作号車名称
        Public Const TD_SHISAKU_GOUSYA_NAME = "SHISAKU_GOUSYA"
        'メタル納入指示日
        Public Const TD_M_NOUNYU_SHIJIBI = "M_NOUNYU_SHIJIBI"
        'トリム納入指示日
        Public Const TD_T_NOUNYU_SHIJIBI = "T_NOUNYU_SHIJIBI"

    End Class

#End Region

#Region "データテーブル列名(試作部品編集情報 ベース)"
    ''' <summary>
    ''' データテーブル列名(試作部品編集情報 ベース)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmDtColShisakuBuhinEditBase

        Public Const TD_SHISAKU_EVENT_CODE As String = "SHISAKU_EVENT_CODE"
        Public Const TD_SHISAKU_BUKA_CODE As String = "SHISAKU_BUKA_CODE"
        Public Const TD_SHISAKU_BLOCK_NO As String = "SHISAKU_BLOCK_NO"
        Public Const TD_SHISAKU_BLOCK_NO_KAITEI_NO As String = "SHISAKU_BLOCK_NO_KAITEI_NO"
        Public Const TD_BUHIN_NO_HYOUJI_JUN As String = "BUHIN_NO_HYOUJI_JUN"
        Public Const TD_ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO As String = "ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO"
        Public Const TD_LEVEL As String = "LEVEL"
        Public Const TD_SHUKEI_CODE As String = "SHUKEI_CODE"
        Public Const TD_SIA_SHUKEI_CODE As String = "SIA_SHUKEI_CODE"
        Public Const TD_GENCYO_CKD_KBN As String = "GENCYO_CKD_KBN"
        Public Const TD_MAKER_CODE As String = "MAKER_CODE"
        Public Const TD_MAKER_NAME As String = "MAKER_NAME"
        Public Const TD_BUHIN_NO As String = "BUHIN_NO"
        Public Const TD_BUHIN_NO_KBN As String = "BUHIN_NO_KBN"
        Public Const TD_BUHIN_NO_KAITEI_NO As String = "BUHIN_NO_KAITEI_NO"
        Public Const TD_EDA_BAN As String = "EDA_BAN"
        Public Const TD_BUHIN_NAME As String = "BUHIN_NAME"
        Public Const TD_SAISHIYOUFUKA As String = "SAISHIYOUFUKA"
        Public Const TD_SHUTUZU_YOTEI_DATE As String = "SHUTUZU_YOTEI_DATE"
        Public Const TD_ZAISHITU_KIKAKU_1 As String = "ZAISHITU_KIKAKU_1"
        Public Const TD_ZAISHITU_KIKAKU_2 As String = "ZAISHITU_KIKAKU_2"
        Public Const TD_ZAISHITU_KIKAKU_3 As String = "ZAISHITU_KIKAKU_3"
        Public Const TD_ZAISHITU_MEKKI As String = "ZAISHITU_MEKKI"
        Public Const TD_SHISAKU_BANKO_SURYO As String = "SHISAKU_BANKO_SURYO"
        Public Const TD_SHISAKU_BANKO_SURYO_U As String = "SHISAKU_BANKO_SURYO_U"
        Public Const TD_SHISAKU_BUHIN_HI As String = "SHISAKU_BUHIN_HI"
        Public Const TD_SHISAKU_KATA_HI As String = "SHISAKU_KATA_HI"
        Public Const TD_BIKOU As String = "BIKOU"
        Public Const TD_EDIT_TOUROKUBI As String = "EDIT_TOUROKUBI"
        Public Const TD_EDIT_TOUROKUJIKAN As String = "EDIT_TOUROKUJIKAN"
        Public Const TD_KAITEI_HANDAN_FLG As String = "KAITEI_HANDAN_FLG"
        Public Const TD_SHISAKU_LIST_CODE As String = "SHISAKU_LIST_CODE"

    End Class

#End Region

#Region "試作部品編集・INSTL情報ベース"
    ''' <summary>
    ''' 試作部品編集・INSTL情報ベース
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmDtColShisakuBuhinEditInstlBase

        Public Const TD_SHISAKU_EVENT_CODE As String = "SHISAKU_EVENT_CODE"
        Public Const TD_SHISAKU_BUKA_CODE As String = "SHISAKU_BUKA_CODE"
        Public Const TD_SHISAKU_BLOCK_NO As String = "SHISAKU_BLOCK_NO"
        Public Const TD_SHISAKU_BLOCK_NO_KAITEI_NO As String = "SHISAKU_BLOCK_NO_KAITEI_NO"
        Public Const TD_BUHIN_NO_HYOUJI_JUN As String = "BUHIN_NO_HYOUJI_JUN"
        Public Const TD_INSTL_HINBAN_HYOUJI_JUN As String = "INSTL_HINBAN_HYOUJI_JUN"
        Public Const TD_ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO As String = "ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO"
        Public Const TD_INSU_SURYO As String = "INSU_SURYO"
        Public Const TD_SAISYU_KOUSHINBI As String = "SAISYU_KOUSHINBI"

    End Class
#End Region

#Region "試作設計ブロックINSTL情報"
    Public Class NmDtColShisakuBlockInstl

        Public Const TD_SHISAKU_EVENT_CODE As String = "SHISAKU_EVENT_CODE"
        Public Const TD_SHISAKU_BUKA_CODE As String = "SHISAKU_BUKA_CODE"
        Public Const TD_SHISAKU_BLOCK_NO As String = "SHISAKU_BLOCK_NO"
        Public Const TD_SHISAKU_BLOCK_NO_KAITEI_NO As String = "SHISAKU_BLOCK_NO_KAITEI_NO"
        Public Const TD_SHISAKU_GOUSYA As String = "SHISAKU_GOUSYA"
        Public Const TD_INSTL_HINBAN_HYOUJI_JUN As String = "INSTL_HINBAN_HYOUJI_JUN"
        Public Const TD_INSTL_HINBAN As String = "INSTL_HINBAN"
        Public Const TD_INSTL_HINBAN_KBN As String = "INSTL_HINBAN_KBN"
        Public Const TD_BF_BUHIN_NO As String = "BF_BUHIN_NO"
        Public Const TD_INSU_SURYO As String = "INSU_SURYO"
        Public Const TD_SAISYU_KOUSHINBI As String = "SAISYU_KOUSHINBI"

    End Class
#End Region

End Namespace