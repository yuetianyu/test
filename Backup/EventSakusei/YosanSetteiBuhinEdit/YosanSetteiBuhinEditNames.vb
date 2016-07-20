Imports ShisakuCommon
Imports EventSakusei.ShisakuBuhinEdit.Ui
Imports EventSakusei.YosanSetteiBuhinEdit.Ui

Namespace YosanSetteiBuhinEdit
    ''' <summary>
    ''' 定数・固定値類
    ''' </summary>
    ''' <remarks></remarks>
    Public Class YosanSetteiBuhinEditNames

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
        '追加変更削除フラグ
        Public Const TAG_AUD_FLAG As String = "AUD_FLAG"
        '予算部課コード
        Public Const TAG_YOSAN_BUKA_CODE As String = "YOSAN_BUKA_CODE"
        '予算ブロック№
        Public Const TAG_YOSAN_BLOCK_NO As String = "YOSAN_BLOCK_NO"
        '行ID
        Public Const TAG_YOSAN_GYOU_ID As String = "YOSAN_GYOU_ID"
        'レベル
        Public Const TAG_YOSAN_LEVEL As String = "YOSAN_LEVEL"
        '部品番号表示順
        Public Const TAG_BUHIN_NO_HYOUJI_JUN As String = "BUHIN_NO_HYOUJI_JUN"
        '国内集計コード
        Public Const TAG_YOSAN_SHUKEI_CODE As String = "YOSAN_SHUKEI_CODE"
        '海外SIA集計コード
        Public Const TAG_YOSAN_SIA_SHUKEI_CODE As String = "YOSAN_SIA_SHUKEI_CODE"
        '部品番号
        Public Const TAG_YOSAN_BUHIN_NO As String = "YOSAN_BUHIN_NO"
        '部品名称
        Public Const TAG_YOSAN_BUHIN_NAME As String = "YOSAN_BUHIN_NAME"
        '合計員数
        Public Const TAG_YOSAN_INSU As String = "YOSAN_INSU"
        '取引先コード
        Public Const TAG_YOSAN_MAKER_CODE As String = "YOSAN_MAKER_CODE"
        '供給セクション
        Public Const TAG_YOSAN_KYOUKU_SECTION As String = "YOSAN_KYOUKU_SECTION"
        '購担
        Public Const TAG_YOSAN_KOUTAN As String = "YOSAN_KOUTAN"
        '手配記号
        Public Const TAG_YOSAN_TEHAI_KIGOU As String = "YOSAN_TEHAI_KIGOU"
        '設計情報_作り方・製作方法
        Public Const TAG_YOSAN_TSUKURIKATA_SEISAKU As String = "YOSAN_TSUKURIKATA_SEISAKU"
        '設計情報_作り方・型仕様1
        Public Const TAG_YOSAN_TSUKURIKATA_KATASHIYOU_1 As String = "YOSAN_TSUKURIKATA_KATASHIYOU_1"
        '設計情報_作り方・型仕様2
        Public Const TAG_YOSAN_TSUKURIKATA_KATASHIYOU_2 As String = "YOSAN_TSUKURIKATA_KATASHIYOU_2"
        '設計情報_作り方・型仕様3
        Public Const TAG_YOSAN_TSUKURIKATA_KATASHIYOU_3 As String = "YOSAN_TSUKURIKATA_KATASHIYOU_3"
        '設計情報_作り方・治具
        Public Const TAG_YOSAN_TSUKURIKATA_TIGU As String = "YOSAN_TSUKURIKATA_TIGU"
        '設計情報_作り方・部品製作規模・概要
        Public Const TAG_YOSAN_TSUKURIKATA_KIBO As String = "YOSAN_TSUKURIKATA_KIBO"
        '設計情報_試作部品費（円）
        Public Const TAG_YOSAN_SHISAKU_BUHIN_HI As String = "YOSAN_SHISAKU_BUHIN_HI"
        '設計情報_試作型費（千円）
        Public Const TAG_YOSAN_SHISAKU_KATA_HI As String = "YOSAN_SHISAKU_KATA_HI"
        '設計情報_部品ノート
        Public Const TAG_YOSAN_BUHIN_NOTE As String = "YOSAN_BUHIN_NOTE"
        '設計情報_備考
        Public Const TAG_YOSAN_BIKOU As String = "YOSAN_BIKOU"
        '部品費根拠_国外区分
        Public Const TAG_YOSAN_KONKYO_KOKUGAI_KBN As String = "YOSAN_KONKYO_KOKUGAI_KBN"
        '部品費根拠_MIXコスト部品費(円/ｾﾝﾄ)
        Public Const TAG_YOSAN_KONKYO_MIX_BUHIN_HI As String = "YOSAN_KONKYO_MIX_BUHIN_HI"
        '部品費根拠_引用元MIX値部品費
        Public Const TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI As String = "YOSAN_KONKYO_INYOU_MIX_BUHIN_HI"
        '部品費根拠_係数１
        Public Const TAG_YOSAN_KONKYO_KEISU_1 As String = "YOSAN_KONKYO_KEISU_1"
        '部品費根拠_工法
        Public Const TAG_YOSAN_KONKYO_KOUHOU As String = "YOSAN_KONKYO_KOUHOU"
        '割付予算_部品費(円)
        Public Const TAG_YOSAN_WARITUKE_BUHIN_HI As String = "YOSAN_WARITUKE_BUHIN_HI"
        '割付予算_係数２
        Public Const TAG_YOSAN_WARITUKE_KEISU_2 As String = "YOSAN_WARITUKE_KEISU_2"

        Public Const TAG_KAKO_1_RYOSAN_TANKA As String = "KAKO_1_RYOSAN_TANKA"
        Public Const TAG_KAKO_1_WARITUKE_BUHIN_HI As String = "KAKO_1_WARITUKE_BUHIN_HI"
        Public Const TAG_KAKO_1_WARITUKE_KATA_HI As String = "KAKO_1_WARITUKE_KATA_HI"
        Public Const TAG_KAKO_1_WARITUKE_KOUHOU As String = "KAKO_1_WARITUKE_KOUHOU"
        Public Const TAG_KAKO_1_MAKER_BUHIN_HI As String = "KAKO_1_MAKER_BUHIN_HI"
        Public Const TAG_KAKO_1_MAKER_KATA_HI As String = "KAKO_1_MAKER_KATA_HI"
        Public Const TAG_KAKO_1_MAKER_KOUHOU As String = "KAKO_1_MAKER_KOUHOU"
        Public Const TAG_KAKO_1_SHINGI_BUHIN_HI As String = "KAKO_1_SHINGI_BUHIN_HI"
        Public Const TAG_KAKO_1_SHINGI_KATA_HI As String = "KAKO_1_SHINGI_KATA_HI"
        Public Const TAG_KAKO_1_SHINGI_KOUHOU As String = "KAKO_1_SHINGI_KOUHOU"
        Public Const TAG_KAKO_1_KOUNYU_KIBOU_TANKA As String = "KAKO_1_KOUNYU_KIBOU_TANKA"
        Public Const TAG_KAKO_1_KOUNYU_TANKA As String = "KAKO_1_KOUNYU_TANKA"
        Public Const TAG_KAKO_1_SHIKYU_HIN As String = "KAKO_1_SHIKYU_HIN"
        Public Const TAG_KAKO_1_KOUJI_SHIREI_NO As String = "KAKO_1_KOUJI_SHIREI_NO"
        Public Const TAG_KAKO_1_EVENT_NAME As String = "KAKO_1_EVENT_NAME"
        Public Const TAG_KAKO_1_HACHU_BI As String = "KAKO_1_HACHU_BI"
        Public Const TAG_KAKO_1_KENSHU_BI As String = "KAKO_1_KENSHU_BI"


        Public Const TAG_KAKO_2_RYOSAN_TANKA As String = "KAKO_2_RYOSAN_TANKA"
        Public Const TAG_KAKO_2_WARITUKE_BUHIN_HI As String = "KAKO_2_WARITUKE_BUHIN_HI"
        Public Const TAG_KAKO_2_WARITUKE_KATA_HI As String = "KAKO_2_WARITUKE_KATA_HI"
        Public Const TAG_KAKO_2_WARITUKE_KOUHOU As String = "KAKO_2_WARITUKE_KOUHOU"
        Public Const TAG_KAKO_2_MAKER_BUHIN_HI As String = "KAKO_2_MAKER_BUHIN_HI"
        Public Const TAG_KAKO_2_MAKER_KATA_HI As String = "KAKO_2_MAKER_KATA_HI"
        Public Const TAG_KAKO_2_MAKER_KOUHOU As String = "KAKO_2_MAKER_KOUHOU"
        Public Const TAG_KAKO_2_SHINGI_BUHIN_HI As String = "KAKO_2_SHINGI_BUHIN_HI"
        Public Const TAG_KAKO_2_SHINGI_KATA_HI As String = "KAKO_2_SHINGI_KATA_HI"
        Public Const TAG_KAKO_2_SHINGI_KOUHOU As String = "KAKO_2_SHINGI_KOUHOU"
        Public Const TAG_KAKO_2_KOUNYU_KIBOU_TANKA As String = "KAKO_2_KOUNYU_KIBOU_TANKA"
        Public Const TAG_KAKO_2_KOUNYU_TANKA As String = "KAKO_2_KOUNYU_TANKA"
        Public Const TAG_KAKO_2_SHIKYU_HIN As String = "KAKO_2_SHIKYU_HIN"
        Public Const TAG_KAKO_2_KOUJI_SHIREI_NO As String = "KAKO_2_KOUJI_SHIREI_NO"
        Public Const TAG_KAKO_2_EVENT_NAME As String = "KAKO_2_EVENT_NAME"
        Public Const TAG_KAKO_2_HACHU_BI As String = "KAKO_2_HACHU_BI"
        Public Const TAG_KAKO_2_KENSHU_BI As String = "KAKO_2_KENSHU_BI"

        Public Const TAG_KAKO_3_RYOSAN_TANKA As String = "KAKO_3_RYOSAN_TANKA"
        Public Const TAG_KAKO_3_WARITUKE_BUHIN_HI As String = "KAKO_3_WARITUKE_BUHIN_HI"
        Public Const TAG_KAKO_3_WARITUKE_KATA_HI As String = "KAKO_3_WARITUKE_KATA_HI"
        Public Const TAG_KAKO_3_WARITUKE_KOUHOU As String = "KAKO_3_WARITUKE_KOUHOU"
        Public Const TAG_KAKO_3_MAKER_BUHIN_HI As String = "KAKO_3_MAKER_BUHIN_HI"
        Public Const TAG_KAKO_3_MAKER_KATA_HI As String = "KAKO_3_MAKER_KATA_HI"
        Public Const TAG_KAKO_3_MAKER_KOUHOU As String = "KAKO_3_MAKER_KOUHOU"
        Public Const TAG_KAKO_3_SHINGI_BUHIN_HI As String = "KAKO_3_SHINGI_BUHIN_HI"
        Public Const TAG_KAKO_3_SHINGI_KATA_HI As String = "KAKO_3_SHINGI_KATA_HI"
        Public Const TAG_KAKO_3_SHINGI_KOUHOU As String = "KAKO_3_SHINGI_KOUHOU"
        Public Const TAG_KAKO_3_KOUNYU_KIBOU_TANKA As String = "KAKO_3_KOUNYU_KIBOU_TANKA"
        Public Const TAG_KAKO_3_KOUNYU_TANKA As String = "KAKO_3_KOUNYU_TANKA"
        Public Const TAG_KAKO_3_SHIKYU_HIN As String = "KAKO_3_SHIKYU_HIN"
        Public Const TAG_KAKO_3_KOUJI_SHIREI_NO As String = "KAKO_3_KOUJI_SHIREI_NO"
        Public Const TAG_KAKO_3_EVENT_NAME As String = "KAKO_3_EVENT_NAME"
        Public Const TAG_KAKO_3_HACHU_BI As String = "KAKO_3_HACHU_BI"
        Public Const TAG_KAKO_3_KENSHU_BI As String = "KAKO_3_KENSHU_BI"

        Public Const TAG_KAKO_4_RYOSAN_TANKA As String = "KAKO_4_RYOSAN_TANKA"
        Public Const TAG_KAKO_4_WARITUKE_BUHIN_HI As String = "KAKO_4_WARITUKE_BUHIN_HI"
        Public Const TAG_KAKO_4_WARITUKE_KATA_HI As String = "KAKO_4_WARITUKE_KATA_HI"
        Public Const TAG_KAKO_4_WARITUKE_KOUHOU As String = "KAKO_4_WARITUKE_KOUHOU"
        Public Const TAG_KAKO_4_MAKER_BUHIN_HI As String = "KAKO_4_MAKER_BUHIN_HI"
        Public Const TAG_KAKO_4_MAKER_KATA_HI As String = "KAKO_4_MAKER_KATA_HI"
        Public Const TAG_KAKO_4_MAKER_KOUHOU As String = "KAKO_4_MAKER_KOUHOU"
        Public Const TAG_KAKO_4_SHINGI_BUHIN_HI As String = "KAKO_4_SHINGI_BUHIN_HI"
        Public Const TAG_KAKO_4_SHINGI_KATA_HI As String = "KAKO_4_SHINGI_KATA_HI"
        Public Const TAG_KAKO_4_SHINGI_KOUHOU As String = "KAKO_4_SHINGI_KOUHOU"
        Public Const TAG_KAKO_4_KOUNYU_KIBOU_TANKA As String = "KAKO_4_KOUNYU_KIBOU_TANKA"
        Public Const TAG_KAKO_4_KOUNYU_TANKA As String = "KAKO_4_KOUNYU_TANKA"
        Public Const TAG_KAKO_4_SHIKYU_HIN As String = "KAKO_4_SHIKYU_HIN"
        Public Const TAG_KAKO_4_KOUJI_SHIREI_NO As String = "KAKO_4_KOUJI_SHIREI_NO"
        Public Const TAG_KAKO_4_EVENT_NAME As String = "KAKO_4_EVENT_NAME"
        Public Const TAG_KAKO_4_HACHU_BI As String = "KAKO_4_HACHU_BI"
        Public Const TAG_KAKO_4_KENSHU_BI As String = "KAKO_4_KENSHU_BI"

        Public Const TAG_KAKO_5_RYOSAN_TANKA As String = "KAKO_5_RYOSAN_TANKA"
        Public Const TAG_KAKO_5_WARITUKE_BUHIN_HI As String = "KAKO_5_WARITUKE_BUHIN_HI"
        Public Const TAG_KAKO_5_WARITUKE_KATA_HI As String = "KAKO_5_WARITUKE_KATA_HI"
        Public Const TAG_KAKO_5_WARITUKE_KOUHOU As String = "KAKO_5_WARITUKE_KOUHOU"
        Public Const TAG_KAKO_5_MAKER_BUHIN_HI As String = "KAKO_5_MAKER_BUHIN_HI"
        Public Const TAG_KAKO_5_MAKER_KATA_HI As String = "KAKO_5_MAKER_KATA_HI"
        Public Const TAG_KAKO_5_MAKER_KOUHOU As String = "KAKO_5_MAKER_KOUHOU"
        Public Const TAG_KAKO_5_SHINGI_BUHIN_HI As String = "KAKO_5_SHINGI_BUHIN_HI"
        Public Const TAG_KAKO_5_SHINGI_KATA_HI As String = "KAKO_5_SHINGI_KATA_HI"
        Public Const TAG_KAKO_5_SHINGI_KOUHOU As String = "KAKO_5_SHINGI_KOUHOU"
        Public Const TAG_KAKO_5_KOUNYU_KIBOU_TANKA As String = "KAKO_5_KOUNYU_KIBOU_TANKA"
        Public Const TAG_KAKO_5_KOUNYU_TANKA As String = "KAKO_5_KOUNYU_TANKA"
        Public Const TAG_KAKO_5_SHIKYU_HIN As String = "KAKO_5_SHIKYU_HIN"
        Public Const TAG_KAKO_5_KOUJI_SHIREI_NO As String = "KAKO_5_KOUJI_SHIREI_NO"
        Public Const TAG_KAKO_5_EVENT_NAME As String = "KAKO_5_EVENT_NAME"
        Public Const TAG_KAKO_5_HACHU_BI As String = "KAKO_5_HACHU_BI"
        Public Const TAG_KAKO_5_KENSHU_BI As String = "KAKO_5_KENSHU_BI"



        '割付予算_部品費合計(円)
        Public Const TAG_YOSAN_WARITUKE_BUHIN_HI_TOTAL As String = "YOSAN_WARITUKE_BUHIN_HI_TOTAL"
        '割付予算_型費(千円)
        Public Const TAG_YOSAN_WARITUKE_KATA_HI As String = "YOSAN_WARITUKE_KATA_HI"
        '購入希望_購入希望単価(円)
        Public Const TAG_YOSAN_KOUNYU_KIBOU_TANKA As String = "YOSAN_KOUNYU_KIBOU_TANKA"
        '購入希望_部品費(円)
        Public Const TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI As String = "YOSAN_KOUNYU_KIBOU_BUHIN_HI"
        '購入希望_部品費合計(円)
        Public Const TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL As String = "YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL"
        '購入希望_型費(円)
        Public Const TAG_YOSAN_KOUNYU_KIBOU_KATA_HI As String = "YOSAN_KOUNYU_KIBOU_KATA_HI"
        '変化点
        Public Const TAG_HENKATEN As String = "HENKATEN"

        ''' <summary>
        ''' TAG名称設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub initTagName(ByVal sheet As FarPoint.Win.Spread.SheetView)
            Dim index As Integer = 0

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_AUD_FLAG
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_BUKA_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_BLOCK_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_GYOU_ID
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_LEVEL
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO_HYOUJI_JUN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_SHUKEI_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_SIA_SHUKEI_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_BUHIN_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_BUHIN_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_INSU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_MAKER_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_KYOUKU_SECTION
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_KOUTAN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_TEHAI_KIGOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_TSUKURIKATA_SEISAKU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_TSUKURIKATA_KATASHIYOU_1
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_TSUKURIKATA_KATASHIYOU_2
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_TSUKURIKATA_KATASHIYOU_3
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_TSUKURIKATA_TIGU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_TSUKURIKATA_KIBO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_SHISAKU_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_SHISAKU_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_BUHIN_NOTE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_BIKOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_KONKYO_KOKUGAI_KBN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_KONKYO_MIX_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_KONKYO_KEISU_1
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_KONKYO_KOUHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_WARITUKE_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_WARITUKE_KEISU_2

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_1_RYOSAN_TANKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_1_WARITUKE_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_1_WARITUKE_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_1_WARITUKE_KOUHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_1_MAKER_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_1_MAKER_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_1_MAKER_KOUHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_1_SHINGI_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_1_SHINGI_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_1_SHINGI_KOUHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_1_KOUNYU_KIBOU_TANKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_1_KOUNYU_TANKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_1_SHIKYU_HIN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_1_KOUJI_SHIREI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_1_EVENT_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_1_HACHU_BI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_1_KENSHU_BI


            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_2_RYOSAN_TANKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_2_WARITUKE_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_2_WARITUKE_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_2_WARITUKE_KOUHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_2_MAKER_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_2_MAKER_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_2_MAKER_KOUHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_2_SHINGI_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_2_SHINGI_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_2_SHINGI_KOUHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_2_KOUNYU_KIBOU_TANKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_2_KOUNYU_TANKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_2_SHIKYU_HIN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_2_KOUJI_SHIREI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_2_EVENT_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_2_HACHU_BI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_2_KENSHU_BI

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_3_RYOSAN_TANKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_3_WARITUKE_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_3_WARITUKE_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_3_WARITUKE_KOUHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_3_MAKER_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_3_MAKER_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_3_MAKER_KOUHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_3_SHINGI_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_3_SHINGI_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_3_SHINGI_KOUHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_3_KOUNYU_KIBOU_TANKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_3_KOUNYU_TANKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_3_SHIKYU_HIN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_3_KOUJI_SHIREI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_3_EVENT_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_3_HACHU_BI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_3_KENSHU_BI


            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_4_RYOSAN_TANKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_4_WARITUKE_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_4_WARITUKE_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_4_WARITUKE_KOUHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_4_MAKER_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_4_MAKER_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_4_MAKER_KOUHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_4_SHINGI_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_4_SHINGI_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_4_SHINGI_KOUHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_4_KOUNYU_KIBOU_TANKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_4_KOUNYU_TANKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_4_SHIKYU_HIN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_4_KOUJI_SHIREI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_4_EVENT_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_4_HACHU_BI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_4_KENSHU_BI


            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_5_RYOSAN_TANKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_5_WARITUKE_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_5_WARITUKE_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_5_WARITUKE_KOUHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_5_MAKER_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_5_MAKER_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_5_MAKER_KOUHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_5_SHINGI_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_5_SHINGI_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_5_SHINGI_KOUHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_5_KOUNYU_KIBOU_TANKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_5_KOUNYU_TANKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_5_SHIKYU_HIN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_5_KOUJI_SHIREI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_5_EVENT_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_5_HACHU_BI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAKO_5_KENSHU_BI


            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_WARITUKE_BUHIN_HI_TOTAL
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_WARITUKE_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_KOUNYU_KIBOU_TANKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_KOUNYU_KIBOU_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_HENKATEN

            Dim TcellTypeFactory As New YosanSetteiBuhinEditCellTypeFactory

            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_AUD_FLAG, TcellTypeFactory.AudFlagCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_LEVEL, TcellTypeFactory.YosanLevelCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_SHUKEI_CODE, TcellTypeFactory.YosanShukeiCodeCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_SIA_SHUKEI_CODE, TcellTypeFactory.YosanSiaShukeiCodeCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_BUHIN_NO, TcellTypeFactory.YosanBuhinNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_BUHIN_NAME, TcellTypeFactory.YosanBuhinNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_INSU, TcellTypeFactory.YosanInsuCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_MAKER_CODE, TcellTypeFactory.YosanMakerCodeCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_KYOUKU_SECTION, TcellTypeFactory.YosanKyoukuSectionCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_KOUTAN, TcellTypeFactory.YosanKoutanCellType)

            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_TEHAI_KIGOU, TcellTypeFactory.YosanKoutanCellType)

            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_TSUKURIKATA_KIBO, TcellTypeFactory.YosanTsukurikataKiboCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_SHISAKU_BUHIN_HI, TcellTypeFactory.YosanShisakuBuhinHiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_SHISAKU_KATA_HI, TcellTypeFactory.YosanShisakuKataHiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_BUHIN_NOTE, TcellTypeFactory.YosanBuhinNoteCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_BIKOU, TcellTypeFactory.YosanBikouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_KONKYO_KOKUGAI_KBN, TcellTypeFactory.YosanKonkyoKokugaiKbnCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_KONKYO_MIX_BUHIN_HI, TcellTypeFactory.YosanKonkyoMixBuhinHiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI, TcellTypeFactory.YosanKonkyoInyouMixBuhinHiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_KONKYO_KEISU_1, TcellTypeFactory.YosanKonkyoKeisu1CellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_KONKYO_KOUHOU, TcellTypeFactory.YosanKonkyoKouhouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_WARITUKE_BUHIN_HI, TcellTypeFactory.YosanWaritukeBuhinHiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_WARITUKE_KEISU_2, TcellTypeFactory.YosanWaritukeKeisu2CellType)

            '過去購入用1'
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_1_RYOSAN_TANKA, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_1_WARITUKE_BUHIN_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_1_WARITUKE_KATA_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_1_WARITUKE_KOUHOU, TcellTypeFactory.YosanKonkyoKouhouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_1_MAKER_BUHIN_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_1_MAKER_KATA_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_1_MAKER_KOUHOU, TcellTypeFactory.YosanKonkyoKouhouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_1_SHINGI_BUHIN_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_1_SHINGI_KATA_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_1_SHINGI_KOUHOU, TcellTypeFactory.YosanKonkyoKouhouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_1_KOUNYU_KIBOU_TANKA, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_1_KOUNYU_TANKA, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_1_SHIKYU_HIN, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_1_KOUJI_SHIREI_NO, TcellTypeFactory.KakoKojiShireiNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_1_EVENT_NAME, TcellTypeFactory.KakoEventNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_1_HACHU_BI, TcellTypeFactory.KakoHacchuDateCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_1_KENSHU_BI, TcellTypeFactory.KakoKenshuDateCellType)

            '過去購入用2'
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_2_RYOSAN_TANKA, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_2_WARITUKE_BUHIN_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_2_WARITUKE_KATA_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_2_WARITUKE_KOUHOU, TcellTypeFactory.YosanKonkyoKouhouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_2_MAKER_BUHIN_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_2_MAKER_KATA_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_2_MAKER_KOUHOU, TcellTypeFactory.YosanKonkyoKouhouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_2_SHINGI_BUHIN_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_2_SHINGI_KATA_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_2_SHINGI_KOUHOU, TcellTypeFactory.YosanKonkyoKouhouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_2_KOUNYU_KIBOU_TANKA, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_2_KOUNYU_TANKA, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_2_SHIKYU_HIN, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_2_KOUJI_SHIREI_NO, TcellTypeFactory.KakoKojiShireiNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_2_EVENT_NAME, TcellTypeFactory.KakoEventNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_2_HACHU_BI, TcellTypeFactory.KakoHacchuDateCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_2_KENSHU_BI, TcellTypeFactory.KakoKenshuDateCellType)
            '過去購入用3'
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_3_RYOSAN_TANKA, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_3_WARITUKE_BUHIN_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_3_WARITUKE_KATA_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_3_WARITUKE_KOUHOU, TcellTypeFactory.YosanKonkyoKouhouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_3_MAKER_BUHIN_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_3_MAKER_KATA_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_3_MAKER_KOUHOU, TcellTypeFactory.YosanKonkyoKouhouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_3_SHINGI_BUHIN_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_3_SHINGI_KATA_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_3_SHINGI_KOUHOU, TcellTypeFactory.YosanKonkyoKouhouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_3_KOUNYU_KIBOU_TANKA, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_3_KOUNYU_TANKA, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_3_SHIKYU_HIN, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_3_KOUJI_SHIREI_NO, TcellTypeFactory.KakoKojiShireiNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_3_EVENT_NAME, TcellTypeFactory.KakoEventNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_3_HACHU_BI, TcellTypeFactory.KakoHacchuDateCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_3_KENSHU_BI, TcellTypeFactory.KakoKenshuDateCellType)
            '過去購入用4'
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_4_RYOSAN_TANKA, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_4_WARITUKE_BUHIN_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_4_WARITUKE_KATA_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_4_WARITUKE_KOUHOU, TcellTypeFactory.YosanKonkyoKouhouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_4_MAKER_BUHIN_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_4_MAKER_KATA_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_4_MAKER_KOUHOU, TcellTypeFactory.YosanKonkyoKouhouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_4_SHINGI_BUHIN_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_4_SHINGI_KATA_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_4_SHINGI_KOUHOU, TcellTypeFactory.YosanKonkyoKouhouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_4_KOUNYU_KIBOU_TANKA, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_4_KOUNYU_TANKA, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_4_SHIKYU_HIN, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_4_KOUJI_SHIREI_NO, TcellTypeFactory.KakoKojiShireiNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_4_EVENT_NAME, TcellTypeFactory.KakoEventNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_4_HACHU_BI, TcellTypeFactory.KakoHacchuDateCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_4_KENSHU_BI, TcellTypeFactory.KakoKenshuDateCellType)
            '過去購入用5'
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_5_RYOSAN_TANKA, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_5_WARITUKE_BUHIN_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_5_WARITUKE_KATA_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_5_WARITUKE_KOUHOU, TcellTypeFactory.YosanKonkyoKouhouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_5_MAKER_BUHIN_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_5_MAKER_KATA_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_5_MAKER_KOUHOU, TcellTypeFactory.YosanKonkyoKouhouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_5_SHINGI_BUHIN_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_5_SHINGI_KATA_HI, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_5_SHINGI_KOUHOU, TcellTypeFactory.YosanKonkyoKouhouCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_5_KOUNYU_KIBOU_TANKA, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_5_KOUNYU_TANKA, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_5_SHIKYU_HIN, TcellTypeFactory.YosanKakoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_5_KOUJI_SHIREI_NO, TcellTypeFactory.KakoKojiShireiNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_5_EVENT_NAME, TcellTypeFactory.KakoEventNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_5_HACHU_BI, TcellTypeFactory.KakoHacchuDateCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KAKO_5_KENSHU_BI, TcellTypeFactory.KakoKenshuDateCellType)

            '割付予算部品費合計'
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_WARITUKE_BUHIN_HI_TOTAL, TcellTypeFactory.YosanWaritukeBuhinHiTotalCellType)

            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_WARITUKE_KATA_HI, TcellTypeFactory.YosanWaritukeKataHiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_KOUNYU_KIBOU_TANKA, TcellTypeFactory.YosanKounyuKibouTankaCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI, TcellTypeFactory.YosanKounyuKibouBuhinHiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL, TcellTypeFactory.YosanKounyuKibouBuhinHiTotalCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_YOSAN_KOUNYU_KIBOU_KATA_HI, TcellTypeFactory.YosanKounyuKibouKataHiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_HENKATEN, TcellTypeFactory.HenkatenCellType)




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
        '予算リストコード
        Public Const TD_YOSAN_LIST_CODE As String = "YOSAN_LIST_CODE"
        '予算部課コード
        Public Const TD_YOSAN_BUKA_CODE As String = "YOSAN_BUKA_CODE"
        '予算ブロック№
        Public Const TD_YOSAN_BLOCK_NO As String = "YOSAN_BLOCK_NO"
        '部品番号表示順
        Public Const TD_BUHIN_NO_HYOUJI_JUN As String = "BUHIN_NO_HYOUJI_JUN"
        'ソート順
        Public Const TD_YOSAN_SORT_JUN As String = "YOSAN_SORT_JUN"
        '行ID
        Public Const TD_YOSAN_GYOU_ID As String = "YOSAN_GYOU_ID"
        'レベル
        Public Const TD_YOSAN_LEVEL As String = "YOSAN_LEVEL"
        '国内集計コード
        Public Const TD_YOSAN_SHUKEI_CODE As String = "YOSAN_SHUKEI_CODE"
        '海外SIA集計コード
        Public Const TD_YOSAN_SIA_SHUKEI_CODE As String = "YOSAN_SIA_SHUKEI_CODE"
        '部品番号
        Public Const TD_YOSAN_BUHIN_NO As String = "YOSAN_BUHIN_NO"
        '部品名称
        Public Const TD_YOSAN_BUHIN_NAME As String = "YOSAN_BUHIN_NAME"
        '合計員数
        Public Const TD_YOSAN_INSU As String = "YOSAN_INSU"
        '取引先コード
        Public Const TD_YOSAN_MAKER_CODE As String = "YOSAN_MAKER_CODE"
        '供給セクション
        Public Const TD_YOSAN_KYOUKU_SECTION As String = "YOSAN_KYOUKU_SECTION"
        '購担
        Public Const TD_YOSAN_KOUTAN As String = "YOSAN_KOUTAN"
        '手配記号
        Public Const TD_YOSAN_TEHAI_KIGOU As String = "YOSAN_TEHAI_KIGOU"
        '設計情報_作り方・製作方法
        Public Const TD_YOSAN_TSUKURIKATA_SEISAKU As String = "YOSAN_TSUKURIKATA_SEISAKU"
        '設計情報_作り方・型仕様_1
        Public Const TD_YOSAN_TSUKURIKATA_KATASHIYOU_1 As String = "YOSAN_TSUKURIKATA_KATASHIYOU_1"
        '設計情報_作り方・型仕様_2
        Public Const TD_YOSAN_TSUKURIKATA_KATASHIYOU_2 As String = "YOSAN_TSUKURIKATA_KATASHIYOU_2"
        '設計情報_作り方・型仕様_3
        Public Const TD_YOSAN_TSUKURIKATA_KATASHIYOU_3 As String = "YOSAN_TSUKURIKATA_KATASHIYOU_3"
        '設計情報_作り方・治具
        Public Const TD_YOSAN_TSUKURIKATA_TIGU As String = "YOSAN_TSUKURIKATA_TIGU"
        '設計情報_作り方・納入見通し
        Public Const TD_YOSAN_TSUKURIKATA_NOUNYU As String = "YOSAN_TSUKURIKATA_NOUNYU"
        '設計情報_作り方・部品製作規模・概要
        Public Const TD_YOSAN_TSUKURIKATA_KIBO As String = "YOSAN_TSUKURIKATA_KIBO"
        '設計情報_試作部品費（円）
        Public Const TD_YOSAN_SHISAKU_BUHIN_HI As String = "YOSAN_SHISAKU_BUHIN_HI"
        '設計情報_試作型費（千円）
        Public Const TD_YOSAN_SHISAKU_KATA_HI As String = "YOSAN_SHISAKU_KATA_HI"
        '設計情報_部品ノート
        Public Const TD_YOSAN_BUHIN_NOTE As String = "YOSAN_BUHIN_NOTE"
        '設計情報_備考
        Public Const TD_YOSAN_BIKOU As String = "YOSAN_BIKOU"
        '部品費根拠_国外区分
        Public Const TD_YOSAN_KONKYO_KOKUGAI_KBN As String = "YOSAN_KONKYO_KOKUGAI_KBN"
        '部品費根拠_MIXコスト部品費(円/ｾﾝﾄ)
        Public Const TD_YOSAN_KONKYO_MIX_BUHIN_HI As String = "YOSAN_KONKYO_MIX_BUHIN_HI"
        '部品費根拠_引用元MIX値部品費
        Public Const TD_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI As String = "YOSAN_KONKYO_INYOU_MIX_BUHIN_HI"
        '部品費根拠_係数１
        Public Const TD_YOSAN_KONKYO_KEISU_1 As String = "YOSAN_KONKYO_KEISU_1"
        '部品費根拠_工法
        Public Const TD_YOSAN_KONKYO_KOUHOU As String = "YOSAN_KONKYO_KOUHOU"
        '割付予算_部品費(円)
        Public Const TD_YOSAN_WARITUKE_BUHIN_HI As String = "YOSAN_WARITUKE_BUHIN_HI"
        '割付予算_係数２
        Public Const TD_YOSAN_WARITUKE_KEISU_2 As String = "YOSAN_WARITUKE_KEISU_2"


        Public Const TD_KAKO_1_RYOSAN_TANKA As String = "KAKO_1_RYOSAN_TANKA"
        Public Const TD_KAKO_1_WARITUKE_BUHIN_HI As String = "KAKO_1_WARITUKE_BUHIN_HI"
        Public Const TD_KAKO_1_WARITUKE_KATA_HI As String = "KAKO_1_WARITUKE_KATA_HI"
        Public Const TD_KAKO_1_WARITUKE_KOUHOU As String = "KAKO_1_WARITUKE_KOUHOU"
        Public Const TD_KAKO_1_MAKER_BUHIN_HI As String = "KAKO_1_MAKER_BUHIN_HI"
        Public Const TD_KAKO_1_MAKER_KATA_HI As String = "KAKO_1_MAKER_KATA_HI"
        Public Const TD_KAKO_1_MAKER_KOUHOU As String = "KAKO_1_MAKER_KOUHOU"
        Public Const TD_KAKO_1_SHINGI_BUHIN_HI As String = "KAKO_1_SHINGI_BUHIN_HI"
        Public Const TD_KAKO_1_SHINGI_KATA_HI As String = "KAKO_1_SHINGI_KATA_HI"
        Public Const TD_KAKO_1_SHINGI_KOUHOU As String = "KAKO_1_SHINGI_KOUHOU"
        Public Const TD_KAKO_1_KOUNYU_KIBOU_TANKA As String = "KAKO_1_KOUNYU_KIBOU_TANKA"
        Public Const TD_KAKO_1_KOUNYU_TANKA As String = "KAKO_1_KOUNYU_TANKA"
        Public Const TD_KAKO_1_SHIKYU_HIN As String = "KAKO_1_SHIKYU_HIN"
        Public Const TD_KAKO_1_KOUJI_SHIREI_NO As String = "KAKO_1_KOUJI_SHIREI_NO"
        Public Const TD_KAKO_1_EVENT_NAME As String = "KAKO_1_EVENT_NAME"
        Public Const TD_KAKO_1_HACHU_BI As String = "KAKO_1_HACHU_BI"
        Public Const TD_KAKO_1_KENSHU_BI As String = "KAKO_1_KENSHU_BI"

        Public Const TD_KAKO_2_RYOSAN_TANKA As String = "KAKO_2_RYOSAN_TANKA"
        Public Const TD_KAKO_2_WARITUKE_BUHIN_HI As String = "KAKO_2_WARITUKE_BUHIN_HI"
        Public Const TD_KAKO_2_WARITUKE_KATA_HI As String = "KAKO_2_WARITUKE_KATA_HI"
        Public Const TD_KAKO_2_WARITUKE_KOUHOU As String = "KAKO_2_WARITUKE_KOUHOU"
        Public Const TD_KAKO_2_MAKER_BUHIN_HI As String = "KAKO_2_MAKER_BUHIN_HI"
        Public Const TD_KAKO_2_MAKER_KATA_HI As String = "KAKO_2_MAKER_KATA_HI"
        Public Const TD_KAKO_2_MAKER_KOUHOU As String = "KAKO_2_MAKER_KOUHOU"
        Public Const TD_KAKO_2_SHINGI_BUHIN_HI As String = "KAKO_2_SHINGI_BUHIN_HI"
        Public Const TD_KAKO_2_SHINGI_KATA_HI As String = "KAKO_2_SHINGI_KATA_HI"
        Public Const TD_KAKO_2_SHINGI_KOUHOU As String = "KAKO_2_SHINGI_KOUHOU"
        Public Const TD_KAKO_2_KOUNYU_KIBOU_TANKA As String = "KAKO_2_KOUNYU_KIBOU_TANKA"
        Public Const TD_KAKO_2_KOUNYU_TANKA As String = "KAKO_2_KOUNYU_TANKA"
        Public Const TD_KAKO_2_SHIKYU_HIN As String = "KAKO_2_SHIKYU_HIN"
        Public Const TD_KAKO_2_KOUJI_SHIREI_NO As String = "KAKO_2_KOUJI_SHIREI_NO"
        Public Const TD_KAKO_2_EVENT_NAME As String = "KAKO_2_EVENT_NAME"
        Public Const TD_KAKO_2_HACHU_BI As String = "KAKO_2_HACHU_BI"
        Public Const TD_KAKO_2_KENSHU_BI As String = "KAKO_2_KENSHU_BI"

        Public Const TD_KAKO_3_RYOSAN_TANKA As String = "KAKO_3_RYOSAN_TANKA"
        Public Const TD_KAKO_3_WARITUKE_BUHIN_HI As String = "KAKO_3_WARITUKE_BUHIN_HI"
        Public Const TD_KAKO_3_WARITUKE_KATA_HI As String = "KAKO_3_WARITUKE_KATA_HI"
        Public Const TD_KAKO_3_WARITUKE_KOUHOU As String = "KAKO_3_WARITUKE_KOUHOU"
        Public Const TD_KAKO_3_MAKER_BUHIN_HI As String = "KAKO_3_MAKER_BUHIN_HI"
        Public Const TD_KAKO_3_MAKER_KATA_HI As String = "KAKO_3_MAKER_KATA_HI"
        Public Const TD_KAKO_3_MAKER_KOUHOU As String = "KAKO_3_MAKER_KOUHOU"
        Public Const TD_KAKO_3_SHINGI_BUHIN_HI As String = "KAKO_3_SHINGI_BUHIN_HI"
        Public Const TD_KAKO_3_SHINGI_KATA_HI As String = "KAKO_3_SHINGI_KATA_HI"
        Public Const TD_KAKO_3_SHINGI_KOUHOU As String = "KAKO_3_SHINGI_KOUHOU"
        Public Const TD_KAKO_3_KOUNYU_KIBOU_TANKA As String = "KAKO_3_KOUNYU_KIBOU_TANKA"
        Public Const TD_KAKO_3_KOUNYU_TANKA As String = "KAKO_3_KOUNYU_TANKA"
        Public Const TD_KAKO_3_SHIKYU_HIN As String = "KAKO_3_SHIKYU_HIN"
        Public Const TD_KAKO_3_KOUJI_SHIREI_NO As String = "KAKO_3_KOUJI_SHIREI_NO"
        Public Const TD_KAKO_3_EVENT_NAME As String = "KAKO_3_EVENT_NAME"
        Public Const TD_KAKO_3_HACHU_BI As String = "KAKO_3_HACHU_BI"
        Public Const TD_KAKO_3_KENSHU_BI As String = "KAKO_3_KENSHU_BI"

        Public Const TD_KAKO_4_RYOSAN_TANKA As String = "KAKO_4_RYOSAN_TANKA"
        Public Const TD_KAKO_4_WARITUKE_BUHIN_HI As String = "KAKO_4_WARITUKE_BUHIN_HI"
        Public Const TD_KAKO_4_WARITUKE_KATA_HI As String = "KAKO_4_WARITUKE_KATA_HI"
        Public Const TD_KAKO_4_WARITUKE_KOUHOU As String = "KAKO_4_WARITUKE_KOUHOU"
        Public Const TD_KAKO_4_MAKER_BUHIN_HI As String = "KAKO_4_MAKER_BUHIN_HI"
        Public Const TD_KAKO_4_MAKER_KATA_HI As String = "KAKO_4_MAKER_KATA_HI"
        Public Const TD_KAKO_4_MAKER_KOUHOU As String = "KAKO_4_MAKER_KOUHOU"
        Public Const TD_KAKO_4_SHINGI_BUHIN_HI As String = "KAKO_4_SHINGI_BUHIN_HI"
        Public Const TD_KAKO_4_SHINGI_KATA_HI As String = "KAKO_4_SHINGI_KATA_HI"
        Public Const TD_KAKO_4_SHINGI_KOUHOU As String = "KAKO_4_SHINGI_KOUHOU"
        Public Const TD_KAKO_4_KOUNYU_KIBOU_TANKA As String = "KAKO_4_KOUNYU_KIBOU_TANKA"
        Public Const TD_KAKO_4_KOUNYU_TANKA As String = "KAKO_4_KOUNYU_TANKA"
        Public Const TD_KAKO_4_SHIKYU_HIN As String = "KAKO_4_SHIKYU_HIN"
        Public Const TD_KAKO_4_KOUJI_SHIREI_NO As String = "KAKO_4_KOUJI_SHIREI_NO"
        Public Const TD_KAKO_4_EVENT_NAME As String = "KAKO_4_EVENT_NAME"
        Public Const TD_KAKO_4_HACHU_BI As String = "KAKO_4_HACHU_BI"
        Public Const TD_KAKO_4_KENSHU_BI As String = "KAKO_4_KENSHU_BI"

        Public Const TD_KAKO_5_RYOSAN_TANKA As String = "KAKO_5_RYOSAN_TANKA"
        Public Const TD_KAKO_5_WARITUKE_BUHIN_HI As String = "KAKO_5_WARITUKE_BUHIN_HI"
        Public Const TD_KAKO_5_WARITUKE_KATA_HI As String = "KAKO_5_WARITUKE_KATA_HI"
        Public Const TD_KAKO_5_WARITUKE_KOUHOU As String = "KAKO_5_WARITUKE_KOUHOU"
        Public Const TD_KAKO_5_MAKER_BUHIN_HI As String = "KAKO_5_MAKER_BUHIN_HI"
        Public Const TD_KAKO_5_MAKER_KATA_HI As String = "KAKO_5_MAKER_KATA_HI"
        Public Const TD_KAKO_5_MAKER_KOUHOU As String = "KAKO_5_MAKER_KOUHOU"
        Public Const TD_KAKO_5_SHINGI_BUHIN_HI As String = "KAKO_5_SHINGI_BUHIN_HI"
        Public Const TD_KAKO_5_SHINGI_KATA_HI As String = "KAKO_5_SHINGI_KATA_HI"
        Public Const TD_KAKO_5_SHINGI_KOUHOU As String = "KAKO_5_SHINGI_KOUHOU"
        Public Const TD_KAKO_5_KOUNYU_KIBOU_TANKA As String = "KAKO_5_KOUNYU_KIBOU_TANKA"
        Public Const TD_KAKO_5_KOUNYU_TANKA As String = "KAKO_5_KOUNYU_TANKA"
        Public Const TD_KAKO_5_SHIKYU_HIN As String = "KAKO_5_SHIKYU_HIN"
        Public Const TD_KAKO_5_KOUJI_SHIREI_NO As String = "KAKO_5_KOUJI_SHIREI_NO"
        Public Const TD_KAKO_5_EVENT_NAME As String = "KAKO_5_EVENT_NAME"
        Public Const TD_KAKO_5_HACHU_BI As String = "KAKO_5_HACHU_BI"
        Public Const TD_KAKO_5_KENSHU_BI As String = "KAKO_5_KENSHU_BI"


        '割付予算_部品費合計(円)
        Public Const TD_YOSAN_WARITUKE_BUHIN_HI_TOTAL As String = "YOSAN_WARITUKE_BUHIN_HI_TOTAL"
        '割付予算_型費(千円)
        Public Const TD_YOSAN_WARITUKE_KATA_HI As String = "YOSAN_WARITUKE_KATA_HI"
        '購入希望_購入希望単価(円)
        Public Const TD_YOSAN_KOUNYU_KIBOU_TANKA As String = "YOSAN_KOUNYU_KIBOU_TANKA"
        '購入希望_部品費(円)
        Public Const TD_YOSAN_KOUNYU_KIBOU_BUHIN_HI As String = "YOSAN_KOUNYU_KIBOU_BUHIN_HI"
        '購入希望_部品費合計(円)
        Public Const TD_YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL As String = "YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL"
        '購入希望_型費(円)
        Public Const TD_YOSAN_KOUNYU_KIBOU_KATA_HI As String = "YOSAN_KOUNYU_KIBOU_KATA_HI"
        '追加変更削除フラグ
        Public Const TD_AUD_FLAG As String = "AUD_FLAG"
        '追加変更削除日
        Public Const TD_AUD_BI As String = "AUD_BI"
        '変化点
        Public Const TD_HENKATEN As String = "HENKATEN"

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