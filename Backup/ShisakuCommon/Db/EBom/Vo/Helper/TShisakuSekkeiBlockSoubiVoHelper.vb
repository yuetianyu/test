
Namespace Db.EBom.Vo.Helper

    Public Class TShisakuSekkeiBlockSoubiVoHelper

        ''' <summary>試作イベントベース車情報項目No.</summary>
        Public Class ShisakuSoubiHyoujiJun

            ''-----------------------------------------------------------------------------------
            ''変更後
            '''' <summary>ベース車開発符号</summary>
            'Public Const BASE_KAIHATUFUGOU As String = "1001"
            '''' <summary>ベース車仕様情報№</summary>
            'Public Const BASE_SHIYOUJYOUHOU_NO As String = "1002"
            '''' <summary>参考情報・車型</summary>
            'Public Const SEISAKU_SYASYU As String = "1003"
            '''' <summary>参考情報・グレード</summary>
            'Public Const SEISAKU_GRADE As String = "1004"
            '''' <summary>参考情報・仕向地・仕向け</summary>
            'Public Const SEISAKU_SHIMUKE As String = "1005"
            '''' <summary>参考情報・仕向地・ハンドル</summary>
            'Public Const SEISAKU_HANDORU As String = "1006"
            '''' <summary>参考情報・E/G排気量</summary>
            'Public Const SEISAKU_EG_HAIKIRYOU As String = "1007"
            '''' <summary>参考情報・E/G型式</summary>
            'Public Const SEISAKU_EG_KATASHIKI As String = "1008"
            '''' <summary>参考情報・E/G過給器</summary>
            'Public Const SEISAKU_EG_KAKYUUKI As String = "1009"
            '''' <summary>参考情報・T/M駆動方式</summary>
            'Public Const SEISAKU_TM_KUDOU As String = "1010"
            '''' <summary>参考情報・T/M変速機</summary>
            'Public Const SEISAKU_TM_HENSOKUKI As String = "1011"
            '''' <summary>ベース車アプライド№</summary>
            'Public Const BASE_APPLIED_NO As String = "1012"
            '''' <summary>ベース車型式</summary>
            'Public Const BASE_KATASHIKI As String = "1013"
            '''' <summary>ベース車仕向</summary>
            'Public Const BASE_SHIMUKE As String = "1014"
            '''' <summary>ベース車OP</summary>
            'Public Const BASE_OP As String = "1015"
            '''' <summary>ベース車外装色</summary>
            'Public Const BASE_GAISOUSYOKU As String = "1016"
            '''' <summary>ベース車内装色</summary>
            'Public Const BASE_NAISOUSYOKU As String = "1017"
            '''' <summary>試作ベースイベントコード</summary>
            'Public Const BASE_EVENT_CODE As String = "1018"
            '''' <summary>試作ベース号車</summary>
            'Public Const BASE_GOUSYA As String = "1019"
            '''' <summary>参考情報・車体№</summary>
            'Public Const SEISAKU_SYATAI_NO As String = "1020"


            '''' <summary>設計展開・ベース車開発符号</summary>
            'Public Const TENKAI_BASE_KAIHATUFUGOU As String = "1100"
            '''' <summary>設計展開・ベース車仕様情報№</summary>
            'Public Const TENKAI_BASE_SHIYOUJYOUHOU_NO As String = "1101"
            '''' <summary>設計展開・ベース車アプライド№</summary>
            'Public Const TENKAI_BASE_APPLIED_NO As String = "1102"
            '''' <summary>設計展開・ベース車型式</summary>
            'Public Const TENKAI_BASE_KATASHIKI As String = "1103"
            '''' <summary>設計展開・ベース車仕向</summary>
            'Public Const TENKAI_BASE_SHIMUKE As String = "1104"
            '''' <summary>設計展開・ベース車OP</summary>
            'Public Const TENKAI_BASE_OP As String = "1105"
            '''' <summary>設計展開・ベース車外装色</summary>
            'Public Const TENKAI_BASE_GAISOUSYOKU As String = "1106"
            '''' <summary>設計展開・ベース車内装色</summary>
            'Public Const TENKAI_BASE_NAISOUSYOKU As String = "1107"
            '''' <summary>設計展開・試作ベースイベントコード</summary>
            'Public Const TENKAI_BASE_EVENT_CODE As String = "1108"
            '''' <summary>設計展開・試作ベース号車</summary>
            'Public Const TENKAI_BASE_GOUSYA As String = "1109"


            '''' <summary>完成試作車型</summary>
            'Public Const KANSEI_SYAGATA As String = "2001"
            '''' <summary>完成試作グレード</summary>
            'Public Const KANSEI_GRADE As String = "2002"
            '''' <summary>仕向地・仕向け</summary>
            'Public Const KANSEI_SHISAKU_SHIMUKECHI_SHIMUKE As String = "2003"
            '''' <summary>完成試作ハンドル</summary>
            'Public Const KANSEI_HANDORU As String = "2004"
            '''' <summary>完成試作E/G型式</summary>
            'Public Const KANSEI_EG_KATASHIKI As String = "2005"
            '''' <summary>完成試作E/G排気量</summary>
            'Public Const KANSEI_EG_HAIKIRYOU As String = "2006"
            '''' <summary>完成試作E/Gシステム</summary>
            'Public Const KANSEI_EG_SYSTEM As String = "2007"
            '''' <summary>完成試作E/G過給機</summary>
            'Public Const KANSEI_EG_KAKYUUKI As String = "2008"
            '''' <summary>完成試作E/Gメモ１</summary>
            'Public Const KANSEI_EG_MEMO1 As String = "2009"
            '''' <summary>完成試作E/Gメモ２</summary>
            'Public Const KANSEI_EG_MEMO2 As String = "2010"
            '''' <summary>完成試作T/M駆動</summary>
            'Public Const KANSEI_TM_KUDOU As String = "2011"
            '''' <summary>完成試作T/M変速機</summary>
            'Public Const KANSEI_TM_HENSOKUKI As String = "2012"
            '''' <summary>完成試作T/M副変速機</summary>
            'Public Const KANSEI_TM_FUKU_HENSOKUKI As String = "2013"
            '''' <summary>完成試作T/Mメモ１</summary>
            'Public Const KANSEI_TM_MEMO1 As String = "2014"
            '''' <summary>完成試作T/Mメモ２</summary>
            'Public Const KANSEI_TM_MEMO2 As String = "2015"
            '''' <summary>完成試作型式</summary>
            'Public Const KANSEI_KATASHIKI As String = "2016"
            '''' <summary>完成試作仕向け</summary>
            'Public Const KANSEI_SHIMUKE As String = "2017"
            '''' <summary>完成試作OP</summary>
            'Public Const KANSEI_OP As String = "2018"
            '''' <summary>完成試作外装色</summary>
            'Public Const KANSEI_GAISOUSYOKU As String = "2019"
            '''' <summary>完成試作外装色名</summary>
            'Public Const KANSEI_GAISOUSYOKU_NAME As String = "2020"
            '''' <summary>完成試作内装色</summary>
            'Public Const KANSEI_NAISOUSYOKU As String = "2021"
            '''' <summary>完成試作内装色名</summary>
            'Public Const KANSEI_NAISOUSYOKU_NAME As String = "2022"
            '''' <summary>完成試作車台№</summary>
            'Public Const KANSEI_SYADAI_NO As String = "2023"
            '''' <summary>完成試作使用目的</summary>
            'Public Const KANSEI_SHIYOU_MOKUTEKI As String = "2024"
            '''' <summary>完成試作試験目的</summary>
            'Public Const KANSEI_SHIKEN_MOKUTEKI As String = "2025"
            '''' <summary>完成試作使用部署</summary>
            'Public Const KANSEI_SIYOU_BUSYO As String = "2026"
            '''' <summary>完成試作グループ</summary>
            'Public Const KANSEI_GROUP As String = "2027"
            '''' <summary>完成試作製作順序</summary>
            'Public Const KANSEI_SEISAKU_JUNJYO As String = "2028"
            '''' <summary>完成試作完成日</summary>
            'Public Const KANSEI_KANSEIBI As String = "2029"
            '''' <summary>完成試作工指№</summary>
            'Public Const KANSEI_KOUSHI_NO As String = "2030"
            '''' <summary>完成試作製作方法区分</summary>
            'Public Const KANSEI_SEISAKU_HOUHOU_KBN As String = "2031"
            '''' <summary>完成試作製作方法</summary>
            'Public Const KANSEI_SEISAKU_HOUHOU As String = "2032"
            '''' <summary>完成試作メモ欄</summary>
            'Public Const KANSEI_SHISAKU_MEMO As String = "2033"
            ''-----------------------------------------------------------------------------------


            '-----------------------------------------------------------------------------------
            '変更前
            ''' <summary>ベース車開発符号</summary>
            Public Const BASE_KAIHATUFUGOU As String = "1001"
            ''' <summary>ベース車仕様情報№</summary>
            Public Const BASE_SHIYOUJYOUHOU_NO As String = "1002"
            ''' <summary>参考情報・車型</summary>
            Public Const SEISAKU_SYASYU As String = "1011"
            ''' <summary>参考情報・グレード</summary>
            Public Const SEISAKU_GRADE As String = "1012"
            ''' <summary>参考情報・仕向地・仕向け</summary>
            Public Const SEISAKU_SHIMUKE As String = "1013"
            ''' <summary>参考情報・仕向地・ハンドル</summary>
            Public Const SEISAKU_HANDORU As String = "1014"
            ''' <summary>参考情報・E/G排気量</summary>
            Public Const SEISAKU_EG_HAIKIRYOU As String = "1015"
            ''' <summary>参考情報・E/G型式</summary>
            Public Const SEISAKU_EG_KATASHIKI As String = "1016"
            ''' <summary>参考情報・E/G過給器</summary>
            Public Const SEISAKU_EG_KAKYUUKI As String = "1017"
            ''' <summary>参考情報・T/M駆動方式</summary>
            Public Const SEISAKU_TM_KUDOU As String = "1018"
            ''' <summary>参考情報・T/M変速機</summary>
            Public Const SEISAKU_TM_HENSOKUKI As String = "1019"
            ''' <summary>ベース車アプライド№</summary>
            Public Const BASE_APPLIED_NO As String = "1003"
            ''' <summary>ベース車型式</summary>
            Public Const BASE_KATASHIKI As String = "1004"
            ''' <summary>ベース車仕向</summary>
            Public Const BASE_SHIMUKE As String = "1005"
            ''' <summary>ベース車OP</summary>
            Public Const BASE_OP As String = "1006"
            ''' <summary>ベース車外装色</summary>
            Public Const BASE_GAISOUSYOKU As String = "1007"
            ''' <summary>ベース車内装色</summary>
            Public Const BASE_NAISOUSYOKU As String = "1008"
            ''' <summary>試作ベースイベントコード</summary>
            Public Const BASE_EVENT_CODE As String = "1009"
            ''' <summary>試作ベース号車</summary>
            Public Const BASE_GOUSYA As String = "1010"
            ''' <summary>参考情報・車体№</summary>
            Public Const SEISAKU_SYATAI_NO As String = "1020"

            ''' <summary>設計展開・ベース車開発符号</summary>
            Public Const TENKAI_BASE_KAIHATUFUGOU As String = "1100"
            ''' <summary>設計展開・ベース車仕様情報№</summary>
            Public Const TENKAI_BASE_SHIYOUJYOUHOU_NO As String = "1101"
            ''' <summary>設計展開・ベース車アプライド№</summary>
            Public Const TENKAI_BASE_APPLIED_NO As String = "1102"
            ''' <summary>設計展開・ベース車型式</summary>
            Public Const TENKAI_BASE_KATASHIKI As String = "1103"
            ''' <summary>設計展開・ベース車仕向</summary>
            Public Const TENKAI_BASE_SHIMUKE As String = "1104"
            ''' <summary>設計展開・ベース車OP</summary>
            Public Const TENKAI_BASE_OP As String = "1105"
            ''' <summary>設計展開・ベース車外装色</summary>
            Public Const TENKAI_BASE_GAISOUSYOKU As String = "1106"
            ''' <summary>設計展開・ベース車内装色</summary>
            Public Const TENKAI_BASE_NAISOUSYOKU As String = "1107"
            ''' <summary>設計展開・試作ベースイベントコード</summary>
            Public Const TENKAI_BASE_EVENT_CODE As String = "1108"
            ''' <summary>設計展開・試作ベース号車</summary>
            Public Const TENKAI_BASE_GOUSYA As String = "1109"

            ''' <summary>完成試作型式</summary>
            Public Const KANSEI_KATASHIKI As String = "2001"
            ''' <summary>完成試作仕向け</summary>
            Public Const KANSEI_SHIMUKE As String = "2002"
            ''' <summary>完成試作OP</summary>
            Public Const KANSEI_OP As String = "2003"
            ''' <summary>完成試作ハンドル</summary>
            Public Const KANSEI_HANDORU As String = "2004"
            ''' <summary>完成試作車型</summary>
            Public Const KANSEI_SYAGATA As String = "2005"
            ''' <summary>完成試作グレード</summary>
            Public Const KANSEI_GRADE As String = "2006"
            ''' <summary>完成試作車台№</summary>
            Public Const KANSEI_SYADAI_NO As String = "2007"
            ''' <summary>完成試作外装色</summary>
            Public Const KANSEI_GAISOUSYOKU As String = "2008"
            ''' <summary>完成試作内装色</summary>
            Public Const KANSEI_NAISOUSYOKU As String = "2009"
            ''' <summary>完成試作グループ</summary>
            Public Const KANSEI_GROUP As String = "2010"
            ''' <summary>完成試作工指№</summary>
            Public Const KANSEI_KOUSHI_NO As String = "2011"
            ''' <summary>完成試作完成日</summary>
            Public Const KANSEI_KANSEIBI As String = "2012"
            ''' <summary>完成試作E/G型式</summary>
            Public Const KANSEI_EG_KATASHIKI As String = "2013"
            ''' <summary>完成試作E/G排気量</summary>
            Public Const KANSEI_EG_HAIKIRYOU As String = "2014"
            ''' <summary>完成試作E/Gシステム</summary>
            Public Const KANSEI_EG_SYSTEM As String = "2015"
            ''' <summary>完成試作E/G過給機</summary>
            Public Const KANSEI_EG_KAKYUUKI As String = "2016"
            ''' <summary>完成試作T/M駆動</summary>
            Public Const KANSEI_TM_KUDOU As String = "2017"
            ''' <summary>完成試作T/M変速機</summary>
            Public Const KANSEI_TM_HENSOKUKI As String = "2018"
            ''' <summary>完成試作T/M副変速機</summary>
            Public Const KANSEI_TM_FUKU_HENSOKUKI As String = "2019"
            ''' <summary>完成試作使用部署</summary>
            Public Const KANSEI_SIYOU_BUSYO As String = "2020"
            ''' <summary>完成試作試験目的</summary>
            Public Const KANSEI_SHIKEN_MOKUTEKI As String = "2021"

            ''' <summary>仕向地・仕向け</summary>
            Public Const KANSEI_SHISAKU_SHIMUKECHI_SHIMUKE As String = "2022"
            ''' <summary>完成試作E/Gメモ１</summary>
            Public Const KANSEI_EG_MEMO1 As String = "2023"
            ''' <summary>完成試作E/Gメモ２</summary>
            Public Const KANSEI_EG_MEMO2 As String = "2024"
            ''' <summary>完成試作T/Mメモ１</summary>
            Public Const KANSEI_TM_MEMO1 As String = "2025"
            ''' <summary>完成試作T/Mメモ２</summary>
            Public Const KANSEI_TM_MEMO2 As String = "2026"
            ''' <summary>完成試作外装色名</summary>
            Public Const KANSEI_GAISOUSYOKU_NAME As String = "2027"
            ''' <summary>完成試作内装色名</summary>
            Public Const KANSEI_NAISOUSYOKU_NAME As String = "2028"
            ''' <summary>完成試作使用目的</summary>
            Public Const KANSEI_SHIYOU_MOKUTEKI As String = "2029"
            ''' <summary>完成試作製作順序</summary>
            Public Const KANSEI_SEISAKU_JUNJYO As String = "2030"
            ''' <summary>完成試作製作方法区分</summary>
            Public Const KANSEI_SEISAKU_HOUHOU_KBN As String = "2031"
            ''' <summary>完成試作製作方法</summary>
            Public Const KANSEI_SEISAKU_HOUHOU As String = "2032"
            ''' <summary>完成試作メモ欄</summary>
            Public Const KANSEI_SHISAKU_MEMO As String = "2033"
            '-----------------------------------------------------------------------------------

        End Class
        ''' <summary>試作イベントベース車情報項目名</summary>
        Public Class ShisakuSoubiHyoujiJunName

            ''' <summary>ベース車開発符号</summary>
            Public Const BASE_KAIHATUFUGOU As String = "開発符号"
            ''' <summary>ベース車仕様情報№</summary>
            Public Const BASE_SHIYOUJYOUHOU_NO As String = "仕様情報No."
            ''' <summary>ベース車アプライド№</summary>
            Public Const BASE_APPLIED_NO As String = "アプライドNo."
            ''' <summary>ベース車型式</summary>
            Public Const BASE_KATASHIKI As String = "型式"
            ''' <summary>ベース車仕向</summary>
            Public Const BASE_SHIMUKE As String = "仕向"
            ''' <summary>ベース車OP</summary>
            Public Const BASE_OP As String = "OP"
            ''' <summary>ベース車外装色</summary>
            Public Const BASE_GAISOUSYOKU As String = "外装色"
            ''' <summary>ベース車内装色</summary>
            Public Const BASE_NAISOUSYOKU As String = "内装色"
            ''' <summary>試作ベースイベントコード</summary>
            Public Const BASE_EVENT_CODE As String = "イベントコード"
            ''' <summary>試作ベース号車</summary>
            Public Const BASE_GOUSYA As String = "号車"

            ''' <summary>参考情報・車型</summary>
            Public Const SEISAKU_SYASYU As String = "(参)車型"
            ''' <summary>参考情報・グレード</summary>
            Public Const SEISAKU_GRADE As String = "(参)グレード"
            ''' <summary>参考情報・仕向地・仕向け</summary>
            Public Const SEISAKU_SHIMUKE As String = "(参)仕向け"
            ''' <summary>参考情報・仕向地・ハンドル</summary>
            Public Const SEISAKU_HANDORU As String = "(参)ハンドル"
            ''' <summary>参考情報・E/G排気量</summary>
            Public Const SEISAKU_EG_HAIKIRYOU As String = "(参)E/G排気量"
            ''' <summary>参考情報・E/G型式</summary>
            Public Const SEISAKU_EG_KATASHIKI As String = "(参)E/G型式"
            ''' <summary>参考情報・E/G過給器</summary>
            Public Const SEISAKU_EG_KAKYUUKI As String = "(参)E/G過給器"
            ''' <summary>参考情報・T/M駆動方式</summary>
            Public Const SEISAKU_TM_KUDOU As String = "(参)T/M駆動方式"
            ''' <summary>参考情報・T/M変速機</summary>
            Public Const SEISAKU_TM_HENSOKUKI As String = "(参)T/M変速機"
            ''' <summary>車体№</summary>
            Public Const SEISAKU_SYATAI_NO As String = "車体№"


            ''' <summary>完成試作型式</summary>
            Public Const KANSEI_KATASHIKI As String = "型式"
            ''' <summary>完成試作仕向け</summary>
            Public Const KANSEI_SHIMUKE As String = "仕向"
            ''' <summary>完成試作OP</summary>
            Public Const KANSEI_OP As String = "OP"
            ''' <summary>完成試作ハンドル</summary>
            Public Const KANSEI_HANDORU As String = "ハンドル"
            ''' <summary>完成試作車型</summary>
            Public Const KANSEI_SYAGATA As String = "車型"
            ''' <summary>完成試作グレード</summary>
            Public Const KANSEI_GRADE As String = "グレード"
            ''' <summary>完成試作車台№</summary>
            Public Const KANSEI_SYADAI_NO As String = "車台No."
            ''' <summary>完成試作外装色</summary>
            Public Const KANSEI_GAISOUSYOKU As String = "外装色"
            ''' <summary>完成試作内装色</summary>
            Public Const KANSEI_NAISOUSYOKU As String = "内装色"
            ''' <summary>完成試作グループ</summary>
            Public Const KANSEI_GROUP As String = "グループ"
            ''' <summary>完成試作工指№</summary>
            Public Const KANSEI_KOUSHI_NO As String = "工指No."
            ''' <summary>完成試作完成日</summary>
            Public Const KANSEI_KANSEIBI As String = "完成日"
            ''' <summary>完成試作E/G型式</summary>
            Public Const KANSEI_EG_KATASHIKI As String = "E/G 型式"
            ''' <summary>完成試作E/G排気量</summary>
            Public Const KANSEI_EG_HAIKIRYOU As String = "E/G 排気量"
            ''' <summary>完成試作E/Gシステム</summary>
            Public Const KANSEI_EG_SYSTEM As String = "E/G システム"
            ''' <summary>完成試作E/G過給機</summary>
            Public Const KANSEI_EG_KAKYUUKI As String = "E/G 過給機"
            ''' <summary>完成試作T/M駆動</summary>
            Public Const KANSEI_TM_KUDOU As String = "T/M 駆動"
            ''' <summary>完成試作T/M変速機</summary>
            Public Const KANSEI_TM_HENSOKUKI As String = "T/M 変速機"
            ''' <summary>完成試作T/M副変速機</summary>
            Public Const KANSEI_TM_FUKU_HENSOKUKI As String = "T/M 副変速機"
            ''' <summary>完成試作使用部署</summary>
            Public Const KANSEI_SIYOU_BUSYO As String = "使用部署"
            ''' <summary>完成試作試験目的⇒主要確認項目</summary>
            Public Const KANSEI_SHIKEN_MOKUTEKI As String = "主要確認項目"

            ''' <summary>仕向地・仕向け</summary>
            Public Const KANSEI_SHISAKU_SHIMUKECHI_SHIMUKE As String = "仕向地・仕向け"
            ''' <summary>完成試作E/Gメモ１</summary>
            Public Const KANSEI_EG_MEMO1 As String = "E/G メモ１"
            ''' <summary>完成試作E/Gメモ２</summary>
            Public Const KANSEI_EG_MEMO2 As String = "E/G メモ２"
            ''' <summary>完成試作T/Mメモ１</summary>
            Public Const KANSEI_TM_MEMO1 As String = "T/M メモ１"
            ''' <summary>完成試作E/Gメモ２</summary>
            Public Const KANSEI_TM_MEMO2 As String = "T/M メモ２"
            ''' <summary>完成試作外装色名</summary>
            Public Const KANSEI_GAISOUSYOKU_NAME As String = "外装色名"
            ''' <summary>完成試作内装色名</summary>
            Public Const KANSEI_NAISOUSYOKU_NAME As String = "内装色名"
            ''' <summary>完成試作使用目的</summary>
            Public Const KANSEI_SHIYOU_MOKUTEKI As String = "使用目的"
            ''' <summary>完成試作製作順序</summary>
            Public Const KANSEI_SEISAKU_JUNJYO As String = "製作順序"
            ''' <summary>完成試作製作方法区分</summary>
            Public Const KANSEI_SEISAKU_HOUHOU_KBN As String = "製作方法区分"
            ''' <summary>完成試作製作方法</summary>
            Public Const KANSEI_SEISAKU_HOUHOU As String = "製作方法"
            ''' <summary>完成試作メモ欄</summary>
            Public Const KANSEI_SHISAKU_MEMO As String = "メモ欄"


            ''' <summary>設計展開・ベース車開発符号</summary>
            Public Const TENKAI_BASE_KAIHATUFUGOU As String = "開発符号"
            ''' <summary>設計展開・ベース車仕様情報№</summary>
            Public Const TENKAI_BASE_SHIYOUJYOUHOU_NO As String = "仕様情報No."
            ''' <summary>設計展開・ベース車アプライド№</summary>
            Public Const TENKAI_BASE_APPLIED_NO As String = "アプライドNo."
            ''' <summary>設計展開・ベース車型式</summary>
            Public Const TENKAI_BASE_KATASHIKI As String = "型式"
            ''' <summary>設計展開・ベース車仕向</summary>
            Public Const TENKAI_BASE_SHIMUKE As String = "仕向"
            ''' <summary>設計展開・ベース車OP</summary>
            Public Const TENKAI_BASE_OP As String = "OP"
            ''' <summary>設計展開・ベース車外装色</summary>
            Public Const TENKAI_BASE_GAISOUSYOKU As String = "外装色"
            ''' <summary>設計展開・ベース車内装色</summary>
            Public Const TENKAI_BASE_NAISOUSYOKU As String = "内装色"
            ''' <summary>設計展開・試作ベースイベントコード</summary>
            Public Const TENKAI_BASE_EVENT_CODE As String = "イベントコード"
            ''' <summary>設計展開・試作ベース号車</summary>
            Public Const TENKAI_BASE_GOUSYA As String = "号車"

        End Class

        ''' <summary>
        ''' 項目No.の名称を返す
        ''' </summary>
        ''' <param name="strKomukuNo">項目No.</param>
        ''' <returns>項目No.の名称</returns>
        ''' <remarks></remarks>
        Public Shared Function GetNameByCode(ByVal strKomukuNo As String) As String

            Dim strKomukuName As String = ""

            Select Case strKomukuNo
                ''ベース車開発符号
                Case ShisakuSoubiHyoujiJun.BASE_KAIHATUFUGOU
                    strKomukuName = ShisakuSoubiHyoujiJunName.BASE_KAIHATUFUGOU
                    ''ベース車仕様情報№
                Case ShisakuSoubiHyoujiJun.BASE_SHIYOUJYOUHOU_NO
                    strKomukuName = ShisakuSoubiHyoujiJunName.BASE_SHIYOUJYOUHOU_NO
                    ''ベース車アプライド№
                Case ShisakuSoubiHyoujiJun.BASE_APPLIED_NO
                    strKomukuName = ShisakuSoubiHyoujiJunName.BASE_APPLIED_NO
                    ''ベース車型式
                Case ShisakuSoubiHyoujiJun.BASE_KATASHIKI
                    strKomukuName = ShisakuSoubiHyoujiJunName.BASE_KATASHIKI
                    ''ベース車仕向
                Case ShisakuSoubiHyoujiJun.BASE_SHIMUKE
                    strKomukuName = ShisakuSoubiHyoujiJunName.BASE_SHIMUKE
                    ''ベース車OP
                Case ShisakuSoubiHyoujiJun.BASE_OP
                    strKomukuName = ShisakuSoubiHyoujiJunName.BASE_OP
                    ''ベース車外装色
                Case ShisakuSoubiHyoujiJun.BASE_GAISOUSYOKU
                    strKomukuName = ShisakuSoubiHyoujiJunName.BASE_GAISOUSYOKU
                    ''ベース車内装色
                Case ShisakuSoubiHyoujiJun.BASE_NAISOUSYOKU
                    strKomukuName = ShisakuSoubiHyoujiJunName.BASE_NAISOUSYOKU
                    ''試作ベースイベントコード
                Case ShisakuSoubiHyoujiJun.BASE_EVENT_CODE
                    strKomukuName = ShisakuSoubiHyoujiJunName.BASE_EVENT_CODE
                    ''試作ベース号車
                Case ShisakuSoubiHyoujiJun.BASE_GOUSYA
                    strKomukuName = ShisakuSoubiHyoujiJunName.BASE_GOUSYA

                    ''参考情報・車型
                Case ShisakuSoubiHyoujiJun.SEISAKU_SYASYU
                    strKomukuName = ShisakuSoubiHyoujiJunName.SEISAKU_SYASYU
                    ''参考情報・グレード
                Case ShisakuSoubiHyoujiJun.SEISAKU_GRADE
                    strKomukuName = ShisakuSoubiHyoujiJunName.SEISAKU_GRADE
                    ''参考情報・仕向地・仕向け
                Case ShisakuSoubiHyoujiJun.SEISAKU_SHIMUKE
                    strKomukuName = ShisakuSoubiHyoujiJunName.SEISAKU_SHIMUKE
                    ''参考情報・仕向地・ハンドル
                Case ShisakuSoubiHyoujiJun.SEISAKU_HANDORU
                    strKomukuName = ShisakuSoubiHyoujiJunName.SEISAKU_HANDORU
                    ''参考情報・E/G排気量
                Case ShisakuSoubiHyoujiJun.SEISAKU_EG_HAIKIRYOU
                    strKomukuName = ShisakuSoubiHyoujiJunName.SEISAKU_EG_HAIKIRYOU
                    ''参考情報・E/G型式
                Case ShisakuSoubiHyoujiJun.SEISAKU_EG_KATASHIKI
                    strKomukuName = ShisakuSoubiHyoujiJunName.SEISAKU_EG_KATASHIKI
                    ''参考情報・E/G過給器
                Case ShisakuSoubiHyoujiJun.SEISAKU_EG_KAKYUUKI
                    strKomukuName = ShisakuSoubiHyoujiJunName.SEISAKU_EG_KAKYUUKI
                    ''参考情報・T/M駆動方式
                Case ShisakuSoubiHyoujiJun.SEISAKU_TM_KUDOU
                    strKomukuName = ShisakuSoubiHyoujiJunName.SEISAKU_TM_KUDOU
                    ''参考情報・T/M変速機
                Case ShisakuSoubiHyoujiJun.SEISAKU_TM_HENSOKUKI
                    strKomukuName = ShisakuSoubiHyoujiJunName.SEISAKU_TM_HENSOKUKI
                    ''車体№
                Case ShisakuSoubiHyoujiJun.SEISAKU_SYATAI_NO
                    strKomukuName = ShisakuSoubiHyoujiJunName.SEISAKU_SYATAI_NO


                    ''完成試作型式
                Case ShisakuSoubiHyoujiJun.KANSEI_KATASHIKI
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_KATASHIKI
                    ''完成試作仕向け
                Case ShisakuSoubiHyoujiJun.KANSEI_SHIMUKE
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_SHIMUKE
                    ''完成試作OP
                Case ShisakuSoubiHyoujiJun.KANSEI_OP
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_OP
                    ''完成試作ハンドル
                Case ShisakuSoubiHyoujiJun.KANSEI_HANDORU
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_HANDORU
                    ''完成試作車型
                Case ShisakuSoubiHyoujiJun.KANSEI_SYAGATA
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_SYAGATA
                    ''完成試作グレード
                Case ShisakuSoubiHyoujiJun.KANSEI_GRADE
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_GRADE
                    ''完成試作車台№
                Case ShisakuSoubiHyoujiJun.KANSEI_SYADAI_NO
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_SYADAI_NO
                    ''完成試作外装色
                Case ShisakuSoubiHyoujiJun.KANSEI_GAISOUSYOKU
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_GAISOUSYOKU
                    ''完成試作内装色
                Case ShisakuSoubiHyoujiJun.KANSEI_NAISOUSYOKU
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_NAISOUSYOKU
                    ''完成試作グループ
                Case ShisakuSoubiHyoujiJun.KANSEI_GROUP
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_GROUP
                    ''完成試作工指№
                Case ShisakuSoubiHyoujiJun.KANSEI_KOUSHI_NO
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_KOUSHI_NO
                    ''完成試作完成日
                Case ShisakuSoubiHyoujiJun.KANSEI_KANSEIBI
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_KANSEIBI
                    ''完成試作E/G型式
                Case ShisakuSoubiHyoujiJun.KANSEI_EG_KATASHIKI
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_EG_KATASHIKI
                    ''完成試作E/G排気量
                Case ShisakuSoubiHyoujiJun.KANSEI_EG_HAIKIRYOU
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_EG_HAIKIRYOU
                    ''完成試作E/Gシステム
                Case ShisakuSoubiHyoujiJun.KANSEI_EG_SYSTEM
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_EG_SYSTEM
                    ''完成試作E/G過給機
                Case ShisakuSoubiHyoujiJun.KANSEI_EG_KAKYUUKI
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_EG_KAKYUUKI
                    ''完成試作T/M駆動
                Case ShisakuSoubiHyoujiJun.KANSEI_TM_KUDOU
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_TM_KUDOU
                    ''完成試作T/M変速機
                Case ShisakuSoubiHyoujiJun.KANSEI_TM_HENSOKUKI
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_TM_HENSOKUKI
                    ''完成試作T/M副変速機
                Case ShisakuSoubiHyoujiJun.KANSEI_TM_FUKU_HENSOKUKI
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_TM_FUKU_HENSOKUKI
                    ''完成試作使用部署
                Case ShisakuSoubiHyoujiJun.KANSEI_SIYOU_BUSYO
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_SIYOU_BUSYO
                    ''完成試作試験目的
                Case ShisakuSoubiHyoujiJun.KANSEI_SHIKEN_MOKUTEKI
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_SHIKEN_MOKUTEKI


                    ''完成仕向地・仕向け
                Case ShisakuSoubiHyoujiJun.KANSEI_SHISAKU_SHIMUKECHI_SHIMUKE
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_SHISAKU_SHIMUKECHI_SHIMUKE
                    ''完成試作E/Gメモ１
                Case ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO1
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_EG_MEMO1
                    ''完成試作E/Gメモ２
                Case ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO2
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_EG_MEMO2
                    ''完成試作T/Mメモ１
                Case ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO1
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_TM_MEMO1
                    ''完成試作T/Mメモ２
                Case ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO2
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_TM_MEMO2
                    ''完成試作外装色名
                Case ShisakuSoubiHyoujiJun.KANSEI_GAISOUSYOKU_NAME
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_GAISOUSYOKU_NAME
                    ''完成試作内装色名
                Case ShisakuSoubiHyoujiJun.KANSEI_NAISOUSYOKU_NAME
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_NAISOUSYOKU_NAME
                    ''完成試作使用目的
                Case ShisakuSoubiHyoujiJun.KANSEI_SHIYOU_MOKUTEKI
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_SHIYOU_MOKUTEKI
                    ''完成試作製作順序
                Case ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_JUNJYO
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_SEISAKU_JUNJYO
                    ''完成試作製作方法区分
                Case ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_HOUHOU_KBN
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_SEISAKU_HOUHOU_KBN
                    ''完成試作製作方法
                Case ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_HOUHOU
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_SEISAKU_HOUHOU
                    ''完成試作メモ欄
                Case ShisakuSoubiHyoujiJun.KANSEI_SHISAKU_MEMO
                    strKomukuName = ShisakuSoubiHyoujiJunName.KANSEI_SHISAKU_MEMO


                    ''設計展開・ベース車開発符号
                Case ShisakuSoubiHyoujiJun.TENKAI_BASE_KAIHATUFUGOU
                    strKomukuName = ShisakuSoubiHyoujiJunName.TENKAI_BASE_KAIHATUFUGOU
                    ''設計展開・ベース車仕様情報№
                Case ShisakuSoubiHyoujiJun.TENKAI_BASE_SHIYOUJYOUHOU_NO
                    strKomukuName = ShisakuSoubiHyoujiJunName.TENKAI_BASE_SHIYOUJYOUHOU_NO
                    ''設計展開・ベース車アプライド№
                Case ShisakuSoubiHyoujiJun.TENKAI_BASE_APPLIED_NO
                    strKomukuName = ShisakuSoubiHyoujiJunName.TENKAI_BASE_APPLIED_NO
                    ''設計展開・ベース車型式
                Case ShisakuSoubiHyoujiJun.TENKAI_BASE_KATASHIKI
                    strKomukuName = ShisakuSoubiHyoujiJunName.TENKAI_BASE_KATASHIKI
                    ''設計展開・ベース車仕向
                Case ShisakuSoubiHyoujiJun.TENKAI_BASE_SHIMUKE
                    strKomukuName = ShisakuSoubiHyoujiJunName.TENKAI_BASE_SHIMUKE
                    ''設計展開・ベース車OP
                Case ShisakuSoubiHyoujiJun.TENKAI_BASE_OP
                    strKomukuName = ShisakuSoubiHyoujiJunName.TENKAI_BASE_OP
                    ''設計展開・ベース車外装色
                Case ShisakuSoubiHyoujiJun.TENKAI_BASE_GAISOUSYOKU
                    strKomukuName = ShisakuSoubiHyoujiJunName.TENKAI_BASE_GAISOUSYOKU
                    ''設計展開・ベース車内装色
                Case ShisakuSoubiHyoujiJun.TENKAI_BASE_NAISOUSYOKU
                    strKomukuName = ShisakuSoubiHyoujiJunName.TENKAI_BASE_NAISOUSYOKU
                    ''設計展開・試作ベースイベントコード
                Case ShisakuSoubiHyoujiJun.TENKAI_BASE_EVENT_CODE
                    strKomukuName = ShisakuSoubiHyoujiJunName.TENKAI_BASE_EVENT_CODE
                    ''設計展開・試作ベース号車
                Case ShisakuSoubiHyoujiJun.TENKAI_BASE_GOUSYA
                    strKomukuName = ShisakuSoubiHyoujiJunName.TENKAI_BASE_GOUSYA

            End Select

            Return strKomukuName

        End Function

        ''' <summary>
        ''' ベース車情報の表示順かを返す
        ''' </summary>
        ''' <param name="shisakuSoubiHyoujiJun">試作装備表示順</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsBaseCarHyoujiJun(ByVal shisakuSoubiHyoujiJun As String) As Boolean
            Return shisakuSoubiHyoujiJun IsNot Nothing AndAlso IsNumeric(shisakuSoubiHyoujiJun) AndAlso _
            1000 <= CInt(shisakuSoubiHyoujiJun) AndAlso CInt(shisakuSoubiHyoujiJun) <= 1099
        End Function


        ''' <summary>
        ''' 設計展開ベース車情報の表示順かを返す
        ''' </summary>
        ''' <param name="shisakuSoubiHyoujiJun">試作装備表示順</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsBaseTenkaiCarHyoujiJun(ByVal shisakuSoubiHyoujiJun As String) As Boolean
            Return shisakuSoubiHyoujiJun IsNot Nothing AndAlso IsNumeric(shisakuSoubiHyoujiJun) AndAlso _
            1100 <= CInt(shisakuSoubiHyoujiJun) AndAlso CInt(shisakuSoubiHyoujiJun) <= 1200
        End Function

        ''' <summary>
        ''' 完成車車情報の表示順かを返す
        ''' </summary>
        ''' <param name="shisakuSoubiHyoujiJun">試作装備表示順</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsCompleteCarHyoujiJun(ByVal shisakuSoubiHyoujiJun As String) As Boolean
            Return shisakuSoubiHyoujiJun IsNot Nothing AndAlso IsNumeric(shisakuSoubiHyoujiJun) AndAlso _
            2000 <= CInt(shisakuSoubiHyoujiJun) AndAlso CInt(shisakuSoubiHyoujiJun) <= 2100
        End Function


        Private vo As TShisakuSekkeiBlockSoubiVo

        ''' <summary>項目名</summary>
        Private _ShisakuSoubiHyoujiJunMei As String

        ''' <summary>
        ''' Get項目名 By 試作装備表示順
        ''' </summary>
        ''' <param name="vo">試作設計ブロック装備のVo</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal vo As TShisakuSekkeiBlockSoubiVo)
            Me.vo = vo
            'Get項目名
            _ShisakuSoubiHyoujiJunMei = GetNameByCode(vo.ShisakuSoubiHyoujiJun)
        End Sub


#Region "get set"
        ''' <summary>項目No.の名称</summary>
        ''' <value> 項目No.の名称</value>
        ''' <returns> 項目No.の名称</returns>
        Public Property ShisakuSoubiHyoujiJunMei() As String
            Get
                Return _ShisakuSoubiHyoujiJunMei
            End Get
            Set(ByVal value As String)
                _ShisakuSoubiHyoujiJunMei = value
            End Set
        End Property

#End Region

    End Class

End Namespace