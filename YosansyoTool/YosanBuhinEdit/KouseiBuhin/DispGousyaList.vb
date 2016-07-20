Imports EBom.Common
Imports EBom.Common.mdlConstraint
Imports EBom.Data
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Ui.Spd
Imports YosansyoTool.YosanBuhinEdit.KouseiBuhin.Dao

Namespace YosanBuhinEdit.KouseiBuhin
    Public Class DispGousyaList
#Region " メンバー変数 "
        ''' <summary>ビュー</summary>
        Private m_view As Frm41KouseiBuhinSelector
        ''' <summary>FpSpread 共通</summary>
        Private m_spCom As SpreadCommon
        ''' <summary>列が自動ソートされた、列の自動フィルタリングが実行された条件</summary>
        Private filterAndSort As GousyaListFilterAndSortVo

        Private Const TAG_KUBUN As String = "KUBUN"
        Private Const TAG_KUBUN_NAME As String = "KUBUN_NAME"

        Private Const TAG_SHAGATA As String = "SHAGATA"
        Private Const TAG_GRADE As String = "GRADE"
        Private Const TAG_SHIMUKECHI_SHIMUKE As String = "SHIMUKECHI_SHIMUKE"
        Private Const TAG_HANDLE As String = "HANDLE"
        Private Const TAG_EG_KATASHIKI As String = "EG_KATASHIKI"
        Private Const TAG_EG_HAIKIRYO As String = "EG_HAIKIRYO"
        Private Const TAG_EG_SYSTEM As String = "EG_SYSTEM"
        Private Const TAG_EG_KAKYUKI As String = "EG_KAKYUKI"
        Private Const TAG_EG_MEMO_1 As String = "EG_MEMO_1"
        Private Const TAG_EG_MEMO_2 As String = "EG_MEMO_2"
        Private Const TAG_TM_KUDO As String = "TM_KUDO"
        Private Const TAG_TM_HENSOKUKI As String = "TM_HENSOKUKI"
        Private Const TAG_TM_FUKU_HENSOKUKI As String = "TM_FUKU_HENSOKUKI"
        Private Const TAG_TM_MEMO_1 As String = "TM_MEMO_1"
        Private Const TAG_TM_MEMO_2 As String = "TM_MEMO_2"
        Private Const TAG_KATASHIKI As String = "KATASHIKI"
        Private Const TAG_SHIMUKE As String = "SHIMUKE"
        Private Const TAG_OP As String = "OP"
        Private Const TAG_GAISO_SHOKU As String = "GAISO_SHOKU"
        Private Const TAG_GAISO_SHOKU_NAME As String = "GAISO_SHOKU_NAME"
        Private Const TAG_NAISO_SHOKU As String = "NAISO_SHOKU"
        Private Const TAG_NAISO_SHOKU_NAME As String = "NAISO_SHOKU_NAME"
        Private Const TAG_SHADAI_NO As String = "SHADAI_NO"
        Private Const TAG_SHIYOU_MOKUTEKI As String = "SHIYOU_MOKUTEKI"
        Private Const TAG_SHIKEN_MOKUTEKI As String = "SHIKEN_MOKUTEKI"
        Private Const TAG_SHIYO_BUSHO As String = "SHIYO_BUSHO"
        Private Const TAG_GROUP As String = "GROUP"
        Private Const TAG_SEISAKU_JUNJYO As String = "SEISAKU_JUNJYO"
        Private Const TAG_KANSEIBI As String = "KANSEIBI"
        Private Const TAG_KOSHI_NO As String = "KOSHI_NO"
        Private Const TAG_SEISAKU_HOUHOU_KBN As String = "SEISAKU_HOUHOU_KBN"
        Private Const TAG_SEISAKU_HOUHOU As String = "SEISAKU_HOUHOU"
        Private Const TAG_SHISAKU_MEMO As String = "SHISAKU_MEMO"

        Private hoyouEventCode As String
        Private hoyouBukaCode As String
        Private hoyouTantoKey As String
        Private hoyouTanto As String

#End Region

#Region " コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="f"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal f As Frm41KouseiBuhinSelector)
            m_view = f
            m_spCom = New SpreadCommon(m_view.spdKubun)
            filterAndSort = New GousyaListFilterAndSortVo()
        End Sub
#End Region

#Region " ビュー初期化 "
        ''' <summary>
        ''' ビューの初期化        
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function InitView(ByVal kaihatsuFugo As String, _
                                 ByVal shisakuEventCode As String, ByVal shisakuEventName As String, _
                                 ByVal hoyouEventCode As String, ByVal hoyouBukaCode As String, ByVal hoyouTantoKey As String, ByVal hoyouTanto As String) As RESULT

            '
            Me.hoyouEventCode = hoyouEventCode
            Me.hoyouBukaCode = hoyouBukaCode
            Me.hoyouTantoKey = hoyouTantoKey
            Me.hoyouTanto = hoyouTanto

            'Spreadの初期化
            SpreadUtil.Initialize(m_view.spdKubun)
            '
            'SPREADのデータソースを設定する
            'カラム復旧のために。。。
            If StringUtil.IsNotEmpty(shisakuEventCode) Then
                m_view.spdKubun_Sheet1.DataSource = GetSpdGousyaListFromC(kaihatsuFugo, shisakuEventCode)
            Else
                m_view.spdKubun_Sheet1.DataSource = GetSpdGousyaListFromN(kaihatsuFugo, shisakuEventName)
            End If
            '
            SetSpdColTag()
            SetSpdDataField()
            'spreadにデータを設定する
            SetSpreadSource(kaihatsuFugo, shisakuEventCode, shisakuEventName)
            '
            setHoyouSekkeiTantoSoubi()

        End Function
#End Region

#Region "列が自動ソートされたら　処理する "
        Public Sub AutoSortedColumn(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.AutoSortedColumnEventArgs)
            filterAndSort.SortItem = m_view.spdKubun_Sheet1.Columns(e.Column).Tag
            filterAndSort.SortAscending = e.Ascending
        End Sub
#End Region

#Region "列の自動フィルタリングが実行されたら　処理する "
        Public Sub AutoFilteredColumn(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.AutoFilteredColumnEventArgs)
            Select Case m_view.spdKubun_Sheet1.Columns(e.Column).Tag
                Case TAG_KUBUN
                    filterAndSort.Kubun = FilterStringFormat(e.FilterString)
                Case TAG_KUBUN_NAME
                    filterAndSort.KubunName = FilterStringFormat(e.FilterString)
                Case TAG_SHAGATA
                    filterAndSort.Shagata = FilterStringFormat(e.FilterString)
                Case TAG_GRADE
                    filterAndSort.Grade = FilterStringFormat(e.FilterString)
                Case TAG_SHIMUKECHI_SHIMUKE
                    filterAndSort.ShimukechiShimuke = FilterStringFormat(e.FilterString)
                Case TAG_HANDLE
                    filterAndSort.Handle = FilterStringFormat(e.FilterString)
                Case TAG_EG_KATASHIKI
                    filterAndSort.EgKatashiki = FilterStringFormat(e.FilterString)
                Case TAG_EG_HAIKIRYO
                    filterAndSort.EgHaikiryo = FilterStringFormat(e.FilterString)
                Case TAG_EG_SYSTEM
                    filterAndSort.EgSystem = FilterStringFormat(e.FilterString)
                Case TAG_EG_KAKYUKI
                    filterAndSort.Egkakyuki = FilterStringFormat(e.FilterString)
                Case TAG_EG_MEMO_1
                    filterAndSort.EgMemo1 = FilterStringFormat(e.FilterString)
                Case TAG_EG_MEMO_2
                    filterAndSort.EgMemo2 = FilterStringFormat(e.FilterString)
                Case TAG_TM_KUDO
                    filterAndSort.TmKudo = FilterStringFormat(e.FilterString)
                Case TAG_TM_HENSOKUKI
                    filterAndSort.TmHensokuki = FilterStringFormat(e.FilterString)
                Case TAG_TM_FUKU_HENSOKUKI
                    filterAndSort.TmFukuHensokuki = FilterStringFormat(e.FilterString)
                Case TAG_TM_MEMO_1
                    filterAndSort.TmMemo1 = FilterStringFormat(e.FilterString)
                Case TAG_TM_MEMO_2
                    filterAndSort.TmMemo2 = FilterStringFormat(e.FilterString)
                Case TAG_KATASHIKI
                    filterAndSort.Katashiki = FilterStringFormat(e.FilterString)
                Case TAG_SHIMUKE
                    filterAndSort.Shimuke = FilterStringFormat(e.FilterString)
                Case TAG_OP
                    filterAndSort.Op = FilterStringFormat(e.FilterString)
                Case TAG_GAISO_SHOKU
                    filterAndSort.GaisoShoku = FilterStringFormat(e.FilterString)
                Case TAG_GAISO_SHOKU_NAME
                    filterAndSort.GaisoShokuName = FilterStringFormat(e.FilterString)
                Case TAG_NAISO_SHOKU
                    filterAndSort.NaisoShoku = FilterStringFormat(e.FilterString)
                Case TAG_NAISO_SHOKU_NAME
                    filterAndSort.NaisoShokuName = FilterStringFormat(e.FilterString)
                Case TAG_SHADAI_NO
                    filterAndSort.ShadaiNo = FilterStringFormat(e.FilterString)
                Case TAG_SHIYOU_MOKUTEKI
                    filterAndSort.ShiyouMokuteki = FilterStringFormat(e.FilterString)
                Case TAG_SHIKEN_MOKUTEKI
                    filterAndSort.ShikenMokuteki = FilterStringFormat(e.FilterString)
                Case TAG_SHIYO_BUSHO
                    filterAndSort.ShiyoBusho = FilterStringFormat(e.FilterString)
                Case TAG_GROUP
                    filterAndSort.Group = FilterStringFormat(e.FilterString)
                Case TAG_SEISAKU_JUNJYO
                    filterAndSort.SeisakuJunjyo = FilterStringFormat(e.FilterString)
                Case TAG_KANSEIBI
                    filterAndSort.Kanseibi = FilterStringFormat(e.FilterString)
                Case TAG_KOSHI_NO
                    filterAndSort.KoshiNo = FilterStringFormat(e.FilterString)
                Case TAG_SEISAKU_HOUHOU_KBN
                    filterAndSort.SeisakuHouhouKbn = FilterStringFormat(e.FilterString)
                Case TAG_SEISAKU_HOUHOU
                    filterAndSort.SeisakuHouhou = FilterStringFormat(e.FilterString)
                Case TAG_SHISAKU_MEMO
                    filterAndSort.ShisakuMemo = FilterStringFormat(e.FilterString)
            End Select
        End Sub
        Public Function FilterStringFormat(ByVal filterString As String) As String
            If filterString.Equals(m_view.spdKubun_Sheet1.RowFilter.NonBlanksString) Then
                Return m_view.spdKubun_Sheet1.RowFilter.NonBlanksString
            End If
            If filterString.Equals(m_view.spdKubun_Sheet1.RowFilter.AllString) Then
                Return m_view.spdKubun_Sheet1.RowFilter.AllString
            End If
            Return filterString
        End Function
#End Region

#Region " SPREADの列のデータフィールドを設定する "
        ''' <summary>
        ''' SPREADの列のデータフィールドを設定する        
        ''' ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpdDataField()

            '列の項目を設定する           
            For i As Integer = 0 To m_view.spdKubun_Sheet1.ColumnCount - 1
                m_view.spdKubun_Sheet1.Columns(i).DataField = m_view.spdKubun_Sheet1.Columns(i).Tag
            Next
        End Sub
#End Region

#Region " SPREADの列のタグ値を設定する "
        ''' <summary>
        ''' SPREADの列のタグ値を設定する        
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpdColTag()
            '列の項目を設定する           
            m_view.spdKubun_Sheet1.Columns(0).Tag = TAG_KUBUN
            m_view.spdKubun_Sheet1.Columns(1).Tag = TAG_KUBUN_NAME

            m_view.spdKubun_Sheet1.Columns(2).Tag = TAG_SHAGATA
            m_view.spdKubun_Sheet1.Columns(3).Tag = TAG_GRADE
            m_view.spdKubun_Sheet1.Columns(4).Tag = TAG_SHIMUKECHI_SHIMUKE
            m_view.spdKubun_Sheet1.Columns(5).Tag = TAG_HANDLE
            m_view.spdKubun_Sheet1.Columns(6).Tag = TAG_EG_KATASHIKI
            m_view.spdKubun_Sheet1.Columns(7).Tag = TAG_EG_HAIKIRYO
            m_view.spdKubun_Sheet1.Columns(8).Tag = TAG_EG_SYSTEM
            m_view.spdKubun_Sheet1.Columns(9).Tag = TAG_EG_KAKYUKI
            m_view.spdKubun_Sheet1.Columns(10).Tag = TAG_EG_MEMO_1
            m_view.spdKubun_Sheet1.Columns(11).Tag = TAG_EG_MEMO_2
            m_view.spdKubun_Sheet1.Columns(12).Tag = TAG_TM_KUDO
            m_view.spdKubun_Sheet1.Columns(13).Tag = TAG_TM_HENSOKUKI
            m_view.spdKubun_Sheet1.Columns(14).Tag = TAG_TM_FUKU_HENSOKUKI
            m_view.spdKubun_Sheet1.Columns(15).Tag = TAG_TM_MEMO_1
            m_view.spdKubun_Sheet1.Columns(16).Tag = TAG_TM_MEMO_2
            m_view.spdKubun_Sheet1.Columns(17).Tag = TAG_KATASHIKI
            m_view.spdKubun_Sheet1.Columns(18).Tag = TAG_SHIMUKE
            m_view.spdKubun_Sheet1.Columns(19).Tag = TAG_OP
            m_view.spdKubun_Sheet1.Columns(20).Tag = TAG_GAISO_SHOKU
            m_view.spdKubun_Sheet1.Columns(21).Tag = TAG_GAISO_SHOKU_NAME
            m_view.spdKubun_Sheet1.Columns(22).Tag = TAG_NAISO_SHOKU
            m_view.spdKubun_Sheet1.Columns(23).Tag = TAG_NAISO_SHOKU_NAME
            m_view.spdKubun_Sheet1.Columns(24).Tag = TAG_SHADAI_NO
            m_view.spdKubun_Sheet1.Columns(25).Tag = TAG_SHIYOU_MOKUTEKI
            m_view.spdKubun_Sheet1.Columns(26).Tag = TAG_SHIKEN_MOKUTEKI
            m_view.spdKubun_Sheet1.Columns(27).Tag = TAG_SHIYO_BUSHO
            m_view.spdKubun_Sheet1.Columns(28).Tag = TAG_GROUP
            m_view.spdKubun_Sheet1.Columns(29).Tag = TAG_SEISAKU_JUNJYO
            m_view.spdKubun_Sheet1.Columns(30).Tag = TAG_KANSEIBI
            m_view.spdKubun_Sheet1.Columns(31).Tag = TAG_KOSHI_NO
            m_view.spdKubun_Sheet1.Columns(32).Tag = TAG_SEISAKU_HOUHOU_KBN
            m_view.spdKubun_Sheet1.Columns(33).Tag = TAG_SEISAKU_HOUHOU
            m_view.spdKubun_Sheet1.Columns(34).Tag = TAG_SHISAKU_MEMO
        End Sub
#End Region

#Region "SPREADで 列のセルの水平方向の配置を設定する。"
        Public Sub SetSpdColPro()

            'ラベル名と横幅変更
            m_spCom.GetColFromTag(TAG_KUBUN_NAME).Label = "号車"
            m_spCom.GetColFromTag(TAG_KUBUN_NAME).Width = 80

            '
            m_spCom.GetColFromTag(TAG_SHAGATA).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_SHAGATA).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_SHAGATA).Label = "車型"
            m_spCom.GetColFromTag(TAG_SHAGATA).Width = 70
            '
            m_spCom.GetColFromTag(TAG_GRADE).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_GRADE).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_GRADE).Label = "ｸﾞﾚｰﾄﾞ"
            m_spCom.GetColFromTag(TAG_GRADE).Width = 75
            '
            m_spCom.GetColFromTag(TAG_SHIMUKECHI_SHIMUKE).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_SHIMUKECHI_SHIMUKE).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_SHIMUKECHI_SHIMUKE).Label = "仕向"
            m_spCom.GetColFromTag(TAG_SHIMUKECHI_SHIMUKE).Width = 70
            '
            m_spCom.GetColFromTag(TAG_HANDLE).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_HANDLE).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_HANDLE).Label = "ﾊﾝﾄﾞﾙ"
            m_spCom.GetColFromTag(TAG_HANDLE).Width = 70
            '
            m_spCom.GetColFromTag(TAG_EG_KATASHIKI).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_EG_KATASHIKI).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_EG_KATASHIKI).Label = "E/G型式"
            m_spCom.GetColFromTag(TAG_EG_KATASHIKI).Width = 85
            '
            m_spCom.GetColFromTag(TAG_EG_HAIKIRYO).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_EG_HAIKIRYO).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_EG_HAIKIRYO).Label = "E/G排気量"
            m_spCom.GetColFromTag(TAG_EG_HAIKIRYO).Width = 95
            '
            m_spCom.GetColFromTag(TAG_EG_SYSTEM).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_EG_SYSTEM).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_EG_SYSTEM).Label = "E/Gｼｽﾃﾑ"
            m_spCom.GetColFromTag(TAG_EG_SYSTEM).Width = 85
            '
            m_spCom.GetColFromTag(TAG_EG_KAKYUKI).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_EG_KAKYUKI).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_EG_KAKYUKI).Label = "E/G過給器"
            m_spCom.GetColFromTag(TAG_EG_KAKYUKI).Width = 95
            '
            m_spCom.GetColFromTag(TAG_EG_MEMO_1).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_EG_MEMO_1).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_EG_MEMO_1).Label = "E/Gﾒﾓ1"
            m_spCom.GetColFromTag(TAG_EG_MEMO_1).Width = 75
            '
            m_spCom.GetColFromTag(TAG_EG_MEMO_2).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_EG_MEMO_2).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_EG_MEMO_2).Label = "E/Gﾒﾓ2"
            m_spCom.GetColFromTag(TAG_EG_MEMO_2).Width = 75
            '
            m_spCom.GetColFromTag(TAG_TM_KUDO).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_TM_KUDO).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_TM_KUDO).Label = "T/M駆動"
            m_spCom.GetColFromTag(TAG_TM_KUDO).Width = 85
            '
            m_spCom.GetColFromTag(TAG_TM_HENSOKUKI).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_TM_HENSOKUKI).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_TM_HENSOKUKI).Label = "T/M変速機"
            m_spCom.GetColFromTag(TAG_TM_HENSOKUKI).Width = 95
            '
            m_spCom.GetColFromTag(TAG_TM_FUKU_HENSOKUKI).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_TM_FUKU_HENSOKUKI).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_TM_FUKU_HENSOKUKI).Label = "T/M変速機"
            m_spCom.GetColFromTag(TAG_TM_FUKU_HENSOKUKI).Width = 105
            '
            m_spCom.GetColFromTag(TAG_TM_MEMO_1).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_TM_MEMO_1).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_TM_MEMO_1).Label = "T/Mﾒﾓ1"
            m_spCom.GetColFromTag(TAG_TM_MEMO_1).Width = 75
            '
            m_spCom.GetColFromTag(TAG_TM_MEMO_2).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_TM_MEMO_2).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_TM_MEMO_2).Label = "T/Mﾒﾓ2"
            m_spCom.GetColFromTag(TAG_TM_MEMO_2).Width = 75
            '
            m_spCom.GetColFromTag(TAG_KATASHIKI).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_KATASHIKI).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_KATASHIKI).Label = "型式"
            m_spCom.GetColFromTag(TAG_KATASHIKI).Width = 65
            '
            m_spCom.GetColFromTag(TAG_SHIMUKE).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_SHIMUKE).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_SHIMUKE).Label = "仕向"
            m_spCom.GetColFromTag(TAG_SHIMUKE).Width = 65
            '
            m_spCom.GetColFromTag(TAG_OP).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_OP).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_OP).Label = "OP"
            m_spCom.GetColFromTag(TAG_OP).Width = 65
            '
            m_spCom.GetColFromTag(TAG_GAISO_SHOKU).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_GAISO_SHOKU).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_GAISO_SHOKU).Label = "外装色"
            m_spCom.GetColFromTag(TAG_GAISO_SHOKU).Width = 75
            '
            m_spCom.GetColFromTag(TAG_GAISO_SHOKU_NAME).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_GAISO_SHOKU_NAME).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_GAISO_SHOKU_NAME).Label = "外装色名"
            m_spCom.GetColFromTag(TAG_GAISO_SHOKU_NAME).Width = 90
            '
            m_spCom.GetColFromTag(TAG_NAISO_SHOKU).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_NAISO_SHOKU).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_NAISO_SHOKU).Label = "内装色"
            m_spCom.GetColFromTag(TAG_NAISO_SHOKU).Width = 75
            '
            m_spCom.GetColFromTag(TAG_NAISO_SHOKU_NAME).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_NAISO_SHOKU_NAME).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_NAISO_SHOKU_NAME).Label = "内装色名"
            m_spCom.GetColFromTag(TAG_NAISO_SHOKU_NAME).Width = 90
            '
            m_spCom.GetColFromTag(TAG_SHADAI_NO).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_SHADAI_NO).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_SHADAI_NO).Label = "車台№"
            m_spCom.GetColFromTag(TAG_SHADAI_NO).Width = 85
            '
            m_spCom.GetColFromTag(TAG_SHIYOU_MOKUTEKI).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_SHIYOU_MOKUTEKI).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_SHIYOU_MOKUTEKI).Label = "使用目的"
            m_spCom.GetColFromTag(TAG_SHIYOU_MOKUTEKI).Width = 100
            '
            m_spCom.GetColFromTag(TAG_SHIKEN_MOKUTEKI).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_SHIKEN_MOKUTEKI).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_SHIKEN_MOKUTEKI).Label = "試験目的"
            m_spCom.GetColFromTag(TAG_SHIKEN_MOKUTEKI).Width = 100
            '
            m_spCom.GetColFromTag(TAG_SHIYO_BUSHO).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_SHIYO_BUSHO).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_SHIYO_BUSHO).Label = "使用部署"
            m_spCom.GetColFromTag(TAG_SHIYO_BUSHO).Width = 90
            '
            m_spCom.GetColFromTag(TAG_GROUP).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_GROUP).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_GROUP).Label = "ｸﾞﾙｰﾌﾟ"
            m_spCom.GetColFromTag(TAG_GROUP).Width = 75
            '
            m_spCom.GetColFromTag(TAG_SEISAKU_JUNJYO).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_SEISAKU_JUNJYO).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_SEISAKU_JUNJYO).Label = "製作順序"
            m_spCom.GetColFromTag(TAG_SEISAKU_JUNJYO).Width = 90
            '
            m_spCom.GetColFromTag(TAG_KANSEIBI).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_KANSEIBI).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_KANSEIBI).Label = "完成日"
            m_spCom.GetColFromTag(TAG_KANSEIBI).Width = 75
            '
            m_spCom.GetColFromTag(TAG_KOSHI_NO).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_KOSHI_NO).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_KOSHI_NO).Label = "工指№"
            m_spCom.GetColFromTag(TAG_KOSHI_NO).Width = 75
            '
            m_spCom.GetColFromTag(TAG_SEISAKU_HOUHOU_KBN).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_SEISAKU_HOUHOU_KBN).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_SEISAKU_HOUHOU_KBN).Label = "区分"
            m_spCom.GetColFromTag(TAG_SEISAKU_HOUHOU_KBN).Width = 65
            '
            m_spCom.GetColFromTag(TAG_SEISAKU_HOUHOU).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_SEISAKU_HOUHOU).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_SEISAKU_HOUHOU).Label = "製作方法"
            m_spCom.GetColFromTag(TAG_SEISAKU_HOUHOU).Width = 100
            '
            m_spCom.GetColFromTag(TAG_SHISAKU_MEMO).AllowAutoFilter = True
            m_spCom.GetColFromTag(TAG_SHISAKU_MEMO).AllowAutoSort = True
            m_spCom.GetColFromTag(TAG_SHISAKU_MEMO).Label = "試作メモ"
            m_spCom.GetColFromTag(TAG_SHISAKU_MEMO).Width = 100

            '非表示列を変更
            m_spCom.GetColFromTag(TAG_KUBUN).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_KUBUN_NAME).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left

            m_spCom.GetColFromTag(TAG_SHAGATA).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_GRADE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_SHIMUKECHI_SHIMUKE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_HANDLE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_EG_KATASHIKI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_EG_HAIKIRYO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_EG_SYSTEM).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_EG_KAKYUKI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_EG_MEMO_1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_EG_MEMO_2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_TM_KUDO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_TM_HENSOKUKI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_TM_FUKU_HENSOKUKI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_TM_MEMO_1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_TM_MEMO_2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_KATASHIKI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_SHIMUKE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_OP).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_GAISO_SHOKU).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_GAISO_SHOKU_NAME).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_NAISO_SHOKU).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_NAISO_SHOKU_NAME).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_SHADAI_NO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_SHIYOU_MOKUTEKI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_SHIKEN_MOKUTEKI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_SHIYO_BUSHO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_GROUP).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_SEISAKU_JUNJYO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_KANSEIBI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_KOSHI_NO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_SEISAKU_HOUHOU_KBN).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_SEISAKU_HOUHOU).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_SHISAKU_MEMO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left

            For i As Integer = 0 To m_view.spdKubun_Sheet1.RowCount - 1
                m_view.spdKubun_Sheet1.Rows(i).Resizable = False
            Next
        End Sub
#End Region

#Region " spreadの完成情報を制御する "

        Private Sub setHoyouSekkeiTantoSoubi()

            '車型以降非表示
            m_spCom.GetColFromTag(TAG_SHAGATA).Visible = False
            m_spCom.GetColFromTag(TAG_GRADE).Visible = False
            m_spCom.GetColFromTag(TAG_SHIMUKECHI_SHIMUKE).Visible = False
            m_spCom.GetColFromTag(TAG_HANDLE).Visible = False
            m_spCom.GetColFromTag(TAG_EG_KATASHIKI).Visible = False
            m_spCom.GetColFromTag(TAG_EG_HAIKIRYO).Visible = False
            m_spCom.GetColFromTag(TAG_EG_SYSTEM).Visible = False
            m_spCom.GetColFromTag(TAG_EG_KAKYUKI).Visible = False
            m_spCom.GetColFromTag(TAG_EG_MEMO_1).Visible = False
            m_spCom.GetColFromTag(TAG_EG_MEMO_2).Visible = False
            m_spCom.GetColFromTag(TAG_TM_KUDO).Visible = False
            m_spCom.GetColFromTag(TAG_TM_HENSOKUKI).Visible = False
            m_spCom.GetColFromTag(TAG_TM_FUKU_HENSOKUKI).Visible = False
            m_spCom.GetColFromTag(TAG_TM_MEMO_1).Visible = False
            m_spCom.GetColFromTag(TAG_TM_MEMO_2).Visible = False
            m_spCom.GetColFromTag(TAG_KATASHIKI).Visible = False
            m_spCom.GetColFromTag(TAG_SHIMUKE).Visible = False
            m_spCom.GetColFromTag(TAG_OP).Visible = False
            m_spCom.GetColFromTag(TAG_GAISO_SHOKU).Visible = False
            m_spCom.GetColFromTag(TAG_GAISO_SHOKU_NAME).Visible = False
            m_spCom.GetColFromTag(TAG_NAISO_SHOKU).Visible = False
            m_spCom.GetColFromTag(TAG_NAISO_SHOKU_NAME).Visible = False
            m_spCom.GetColFromTag(TAG_SHADAI_NO).Visible = False
            m_spCom.GetColFromTag(TAG_SHIYOU_MOKUTEKI).Visible = False
            m_spCom.GetColFromTag(TAG_SHIKEN_MOKUTEKI).Visible = False
            m_spCom.GetColFromTag(TAG_SHIYO_BUSHO).Visible = False
            m_spCom.GetColFromTag(TAG_GROUP).Visible = False
            m_spCom.GetColFromTag(TAG_SEISAKU_JUNJYO).Visible = False
            m_spCom.GetColFromTag(TAG_KANSEIBI).Visible = False
            m_spCom.GetColFromTag(TAG_KOSHI_NO).Visible = False
            m_spCom.GetColFromTag(TAG_SEISAKU_HOUHOU_KBN).Visible = False
            m_spCom.GetColFromTag(TAG_SEISAKU_HOUHOU).Visible = False
            m_spCom.GetColFromTag(TAG_SHISAKU_MEMO).Visible = False

            '補用設計担当装備情報取得
            Dim getByTHoyouSekkeiTantoSoubi As New KouseiBuhinSelectorDaoImpl
            Dim soubiVos As New List(Of THoyouSekkeiTantoSoubiVo)
            soubiVos = _
                    getByTHoyouSekkeiTantoSoubi.GetByTHoyouSekkeiTantoSoubi(hoyouEventCode, hoyouBukaCode, hoyouTantoKey, hoyouTanto)
            '存在しない場合、全件を追加
            If soubiVos.Count = 0 Then

                Dim aShisakuDate As New ShisakuDate
                Dim hoyouSekkeiTantoSoubiDao As THoyouSekkeiTantoSoubiDao = New THoyouSekkeiTantoSoubiDaoImpl

                For i As Integer = 1 To 33

                    Dim value As New THoyouSekkeiTantoSoubiVo
                    Dim checkFlg As Boolean = False

                    Select Case i
                        Case 1
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SYAGATA
                            checkFlg = True
                        Case 2
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GRADE
                            checkFlg = True
                        Case 3
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHISAKU_SHIMUKECHI_SHIMUKE
                            checkFlg = True
                        Case 4
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_HANDORU
                            checkFlg = True
                        Case 5
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_KATASHIKI
                            checkFlg = True
                        Case 6
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_HAIKIRYOU
                            checkFlg = True
                        Case 7
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_SYSTEM
                            checkFlg = True
                        Case 8
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_KAKYUUKI
                            checkFlg = True
                        Case 9
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO1
                            checkFlg = True
                        Case 10
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO2
                            checkFlg = True
                        Case 11
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_KUDOU
                            checkFlg = True
                        Case 12
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_HENSOKUKI
                            checkFlg = True
                        Case 13
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_FUKU_HENSOKUKI
                            checkFlg = True
                        Case 14
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO1
                            checkFlg = True
                        Case 15
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO2
                            checkFlg = True
                        Case 16
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KATASHIKI
                            checkFlg = True
                        Case 17
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIMUKE
                            checkFlg = True
                        Case 18
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_OP
                            checkFlg = True
                        Case 19
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GAISOUSYOKU
                            checkFlg = True
                        Case 20
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GAISOUSYOKU_NAME
                            checkFlg = True
                        Case 21
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_NAISOUSYOKU
                            checkFlg = True
                        Case 22
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_NAISOUSYOKU_NAME
                            checkFlg = True
                        Case 23
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SYADAI_NO
                            checkFlg = True
                        Case 24
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIKEN_MOKUTEKI
                            checkFlg = True
                        Case 25
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIYOU_MOKUTEKI
                            checkFlg = True
                        Case 26
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SIYOU_BUSYO
                            checkFlg = True
                        Case 27
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GROUP
                            checkFlg = True
                        Case 28
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_JUNJYO
                            checkFlg = True
                        Case 29
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KANSEIBI
                            checkFlg = True
                        Case 30
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KOUSHI_NO
                            checkFlg = True
                        Case 31
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_HOUHOU_KBN
                            checkFlg = True
                        Case 32
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_HOUHOU
                            checkFlg = True
                        Case 33
                            value.HoyouSoubiHyoujiJun = _
                                TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHISAKU_MEMO
                            checkFlg = True

                    End Select

                    If StringUtil.Equals(checkFlg, True) Then
                        value.HoyouEventCode = hoyouEventCode
                        value.HoyouBukaCode = hoyouBukaCode
                        value.HoyouTantoKey = hoyouTantoKey
                        value.HoyouTanto = hoyouTanto

                        value.CreatedUserId = LoginInfo.Now.UserId
                        value.CreatedDate = aShisakuDate.CurrentDateDbFormat
                        value.CreatedTime = aShisakuDate.CurrentTimeDbFormat
                        value.UpdatedUserId = LoginInfo.Now.UserId
                        value.UpdatedDate = aShisakuDate.CurrentDateDbFormat
                        value.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

                        hoyouSekkeiTantoSoubiDao.InsertBy(value)
                    End If
                Next
                '再取得
                soubiVos = _
                        getByTHoyouSekkeiTantoSoubi.GetByTHoyouSekkeiTantoSoubi(hoyouEventCode, hoyouBukaCode, hoyouTantoKey, hoyouTanto)

            End If

            '該当列の表示を制御
            If Not StringUtil.Equals(soubiVos.Count, 0) Then

                For i As Integer = 0 To soubiVos.Count - 1

                    Select Case soubiVos.Item(i).HoyouSoubiHyoujiJun
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SYAGATA
                            m_spCom.GetColFromTag(TAG_SHAGATA).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GRADE
                            m_spCom.GetColFromTag(TAG_GRADE).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHISAKU_SHIMUKECHI_SHIMUKE
                            m_spCom.GetColFromTag(TAG_SHIMUKECHI_SHIMUKE).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_HANDORU
                            m_spCom.GetColFromTag(TAG_HANDLE).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_KATASHIKI
                            m_spCom.GetColFromTag(TAG_EG_KATASHIKI).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_HAIKIRYOU
                            m_spCom.GetColFromTag(TAG_EG_HAIKIRYO).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_SYSTEM
                            m_spCom.GetColFromTag(TAG_EG_SYSTEM).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_KAKYUUKI
                            m_spCom.GetColFromTag(TAG_EG_KAKYUKI).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO1
                            m_spCom.GetColFromTag(TAG_EG_MEMO_1).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO2
                            m_spCom.GetColFromTag(TAG_EG_MEMO_2).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_KUDOU
                            m_spCom.GetColFromTag(TAG_TM_KUDO).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_HENSOKUKI
                            m_spCom.GetColFromTag(TAG_TM_HENSOKUKI).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_FUKU_HENSOKUKI
                            m_spCom.GetColFromTag(TAG_TM_FUKU_HENSOKUKI).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO1
                            m_spCom.GetColFromTag(TAG_TM_MEMO_1).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO2
                            m_spCom.GetColFromTag(TAG_TM_MEMO_2).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KATASHIKI
                            m_spCom.GetColFromTag(TAG_KATASHIKI).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIMUKE
                            m_spCom.GetColFromTag(TAG_SHIMUKE).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_OP
                            m_spCom.GetColFromTag(TAG_OP).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GAISOUSYOKU
                            m_spCom.GetColFromTag(TAG_GAISO_SHOKU).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GAISOUSYOKU_NAME
                            m_spCom.GetColFromTag(TAG_GAISO_SHOKU_NAME).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_NAISOUSYOKU
                            m_spCom.GetColFromTag(TAG_NAISO_SHOKU).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_NAISOUSYOKU_NAME
                            m_spCom.GetColFromTag(TAG_NAISO_SHOKU_NAME).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SYADAI_NO
                            m_spCom.GetColFromTag(TAG_SHADAI_NO).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIKEN_MOKUTEKI
                            m_spCom.GetColFromTag(TAG_SHIYOU_MOKUTEKI).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIYOU_MOKUTEKI
                            m_spCom.GetColFromTag(TAG_SHIKEN_MOKUTEKI).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SIYOU_BUSYO
                            m_spCom.GetColFromTag(TAG_SHIYO_BUSHO).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GROUP
                            m_spCom.GetColFromTag(TAG_GROUP).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_JUNJYO
                            m_spCom.GetColFromTag(TAG_SEISAKU_JUNJYO).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KANSEIBI
                            m_spCom.GetColFromTag(TAG_KANSEIBI).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KOUSHI_NO
                            m_spCom.GetColFromTag(TAG_KOSHI_NO).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_HOUHOU_KBN
                            m_spCom.GetColFromTag(TAG_SEISAKU_HOUHOU_KBN).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_HOUHOU
                            m_spCom.GetColFromTag(TAG_SEISAKU_HOUHOU).Visible = True
                        Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHISAKU_MEMO
                            m_spCom.GetColFromTag(TAG_SHISAKU_MEMO).Visible = True
                    End Select

                Next

            End If

        End Sub

#End Region

#Region " spreadでデータを取得する "
        Public Function GetSpdGousyaListFromC(ByVal kaihatsuFugo As String, ByVal shisakuEvent As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetGousyaListFromC(kaihatsuFugo, shisakuEvent), dtData)
            End Using
            Return dtData
        End Function
        Public Function GetSpdGousyaListFromN(ByVal kaihatsuFugo As String, ByVal shisakuEventName As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetGousyaListFromN(kaihatsuFugo, shisakuEventName), dtData)
            End Using
            Return dtData
        End Function
#End Region

#Region "spreadのbind"
        Private Sub SetSpreadSource(ByVal kaihatsuFugo As String, _
                                    ByVal shisakuEventCode As String, ByVal shisakuEventName As String)

            If StringUtil.IsNotEmpty(shisakuEventCode) Then
                'SPREADのデータソースを設定する
                m_view.spdKubun_Sheet1.DataSource = GetSpdGousyaListFromC(kaihatsuFugo, shisakuEventCode)
            ElseIf StringUtil.IsNotEmpty(shisakuEventName) Then
                'SPREADのデータソースを設定する
                m_view.spdKubun_Sheet1.DataSource = GetSpdGousyaListFromN(kaihatsuFugo, shisakuEventName)
            End If

            'SPREADの列のセルの水平方向の配置を再設定する。
            SetSpdColPro()
        End Sub
#End Region

#Region "号車一覧"
        ''' <summary>
        ''' 画面でデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetGousyaListFromC(ByVal kaitatsuFugo As String, _
                                             ByVal shisakuEvent As String) As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT A.SHISAKU_SYUBETU, A.SHISAKU_GOUSYA, KANSEI.SHISAKU_SYAGATA, ")
                .AppendLine("        KANSEI.SHISAKU_GRADE, KANSEI.SHISAKU_SHIMUKECHI_SHIMUKE, ")
                .AppendLine("        KANSEI.SHISAKU_HANDORU, KANSEI.SHISAKU_EG_KATASHIKI, ")
                .AppendLine("        KANSEI.SHISAKU_EG_HAIKIRYOU, KANSEI.SHISAKU_EG_SYSTEM, ")
                .AppendLine("        KANSEI.SHISAKU_EG_KAKYUUKI, KANSEI.SHISAKU_EG_MEMO_1, ")
                .AppendLine("        KANSEI.SHISAKU_EG_MEMO_2, KANSEI.SHISAKU_TM_KUDOU, ")
                .AppendLine("        KANSEI.SHISAKU_TM_HENSOKUKI, KANSEI.SHISAKU_TM_FUKU_HENSOKUKI, ")
                .AppendLine("        KANSEI.SHISAKU_TM_MEMO_1, KANSEI.SHISAKU_TM_MEMO_2, ")
                .AppendLine("        KANSEI.SHISAKU_KATASHIKI, KANSEI.SHISAKU_SHIMUKE, ")
                .AppendLine("        KANSEI.SHISAKU_OP, KANSEI.SHISAKU_GAISOUSYOKU, ")
                .AppendLine("        KANSEI.SHISAKU_GAISOUSYOKU_NAME, KANSEI.SHISAKU_NAISOUSYOKU, ")
                .AppendLine("        KANSEI.SHISAKU_NAISOUSYOKU_NAME, KANSEI.SHISAKU_SYADAI_NO, ")
                .AppendLine("        KANSEI.SHISAKU_SHIYOU_MOKUTEKI, KANSEI.SHISAKU_SHIKEN_MOKUTEKI, ")
                .AppendLine("        KANSEI.SHISAKU_SIYOU_BUSYO, KANSEI.SHISAKU_GROUP, ")
                .AppendLine("        KANSEI.SHISAKU_SEISAKU_JUNJYO, KANSEI.SHISAKU_KANSEIBI, ")
                .AppendLine("        KANSEI.SHISAKU_KOUSHI_NO, KANSEI.SHISAKU_SEISAKU_HOUHOU_KBN, ")
                .AppendLine("        KANSEI.SHISAKU_SEISAKU_HOUHOU, KANSEI.SHISAKU_MEMO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE AS A INNER JOIN ")
                .AppendLine("      " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT AS B ON A.SHISAKU_EVENT_CODE = B.SHISAKU_EVENT_CODE INNER JOIN ")
                .AppendLine("      " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI AS KANSEI ON A.SHISAKU_EVENT_CODE = KANSEI.SHISAKU_EVENT_CODE AND ")
                .AppendLine("       A.HYOJIJUN_NO = KANSEI.HYOJIJUN_NO ")
                .AppendLine(" WHERE B.SHISAKU_KAIHATSU_FUGO = '" & kaitatsuFugo & "' ")
                .AppendLine("   AND B.SHISAKU_EVENT_CODE = '" & shisakuEvent & "' ")
                .AppendLine(" GROUP BY A.SHISAKU_SYUBETU, A.SHISAKU_GOUSYA, KANSEI.SHISAKU_SYAGATA, ")
                .AppendLine("          KANSEI.SHISAKU_GRADE, KANSEI.SHISAKU_SHIMUKECHI_SHIMUKE, ")
                .AppendLine("          KANSEI.SHISAKU_HANDORU, KANSEI.SHISAKU_EG_KATASHIKI, ")
                .AppendLine("          KANSEI.SHISAKU_EG_HAIKIRYOU, KANSEI.SHISAKU_EG_SYSTEM, ")
                .AppendLine("          KANSEI.SHISAKU_EG_KAKYUUKI, KANSEI.SHISAKU_EG_MEMO_1, ")
                .AppendLine("          KANSEI.SHISAKU_EG_MEMO_2, KANSEI.SHISAKU_TM_KUDOU, ")
                .AppendLine("          KANSEI.SHISAKU_TM_HENSOKUKI, KANSEI.SHISAKU_TM_FUKU_HENSOKUKI, ")
                .AppendLine("          KANSEI.SHISAKU_TM_MEMO_1, KANSEI.SHISAKU_TM_MEMO_2, ")
                .AppendLine("          KANSEI.SHISAKU_KATASHIKI, KANSEI.SHISAKU_SHIMUKE, ")
                .AppendLine("          KANSEI.SHISAKU_OP, KANSEI.SHISAKU_GAISOUSYOKU, ")
                .AppendLine("          KANSEI.SHISAKU_GAISOUSYOKU_NAME, KANSEI.SHISAKU_NAISOUSYOKU, ")
                .AppendLine("          KANSEI.SHISAKU_NAISOUSYOKU_NAME, KANSEI.SHISAKU_SYADAI_NO, ")
                .AppendLine("          KANSEI.SHISAKU_SHIYOU_MOKUTEKI, KANSEI.SHISAKU_SHIKEN_MOKUTEKI, ")
                .AppendLine("          KANSEI.SHISAKU_SIYOU_BUSYO, KANSEI.SHISAKU_GROUP, ")
                .AppendLine("          KANSEI.SHISAKU_SEISAKU_JUNJYO, KANSEI.SHISAKU_KANSEIBI, ")
                .AppendLine("          KANSEI.SHISAKU_KOUSHI_NO, KANSEI.SHISAKU_SEISAKU_HOUHOU_KBN, ")
                .AppendLine("          KANSEI.SHISAKU_SEISAKU_HOUHOU, KANSEI.SHISAKU_MEMO ")
                .AppendLine(" ORDER BY A.SHISAKU_SYUBETU, A.SHISAKU_GOUSYA ")
            End With
            Return sql.ToString()
        End Function

        ''' <summary>
        ''' 画面でデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetGousyaListFromN(ByVal kaitatsuFugo As String, _
                                             ByVal shisakuEventName As String) As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT A.SHISAKU_SYUBETU, A.SHISAKU_GOUSYA, KANSEI.SHISAKU_SYAGATA, ")
                .AppendLine("        KANSEI.SHISAKU_GRADE, KANSEI.SHISAKU_SHIMUKECHI_SHIMUKE, ")
                .AppendLine("        KANSEI.SHISAKU_HANDORU, KANSEI.SHISAKU_EG_KATASHIKI, ")
                .AppendLine("        KANSEI.SHISAKU_EG_HAIKIRYOU, KANSEI.SHISAKU_EG_SYSTEM, ")
                .AppendLine("        KANSEI.SHISAKU_EG_KAKYUUKI, KANSEI.SHISAKU_EG_MEMO_1, ")
                .AppendLine("        KANSEI.SHISAKU_EG_MEMO_2, KANSEI.SHISAKU_TM_KUDOU, ")
                .AppendLine("        KANSEI.SHISAKU_TM_HENSOKUKI, KANSEI.SHISAKU_TM_FUKU_HENSOKUKI, ")
                .AppendLine("        KANSEI.SHISAKU_TM_MEMO_1, KANSEI.SHISAKU_TM_MEMO_2, ")
                .AppendLine("        KANSEI.SHISAKU_KATASHIKI, KANSEI.SHISAKU_SHIMUKE, ")
                .AppendLine("        KANSEI.SHISAKU_OP, KANSEI.SHISAKU_GAISOUSYOKU, ")
                .AppendLine("        KANSEI.SHISAKU_GAISOUSYOKU_NAME, KANSEI.SHISAKU_NAISOUSYOKU, ")
                .AppendLine("        KANSEI.SHISAKU_NAISOUSYOKU_NAME, KANSEI.SHISAKU_SYADAI_NO, ")
                .AppendLine("        KANSEI.SHISAKU_SHIYOU_MOKUTEKI, KANSEI.SHISAKU_SHIKEN_MOKUTEKI, ")
                .AppendLine("        KANSEI.SHISAKU_SIYOU_BUSYO, KANSEI.SHISAKU_GROUP, ")
                .AppendLine("        KANSEI.SHISAKU_SEISAKU_JUNJYO, KANSEI.SHISAKU_KANSEIBI, ")
                .AppendLine("        KANSEI.SHISAKU_KOUSHI_NO, KANSEI.SHISAKU_SEISAKU_HOUHOU_KBN, ")
                .AppendLine("        KANSEI.SHISAKU_SEISAKU_HOUHOU, KANSEI.SHISAKU_MEMO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE AS A INNER JOIN ")
                .AppendLine("      " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT AS B ON A.SHISAKU_EVENT_CODE = B.SHISAKU_EVENT_CODE INNER JOIN ")
                .AppendLine("      " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI AS KANSEI ON A.SHISAKU_EVENT_CODE = KANSEI.SHISAKU_EVENT_CODE AND ")
                .AppendLine("       A.HYOJIJUN_NO = KANSEI.HYOJIJUN_NO ")
                .AppendLine(" WHERE B.SHISAKU_KAIHATSU_FUGO = '" & kaitatsuFugo & "' ")
                .AppendLine("   AND B.SHISAKU_EVENT_NAME = '" & shisakuEventName & "' ")
                .AppendLine(" GROUP BY A.SHISAKU_SYUBETU, A.SHISAKU_GOUSYA, KANSEI.SHISAKU_SYAGATA, ")
                .AppendLine("          KANSEI.SHISAKU_GRADE, KANSEI.SHISAKU_SHIMUKECHI_SHIMUKE, ")
                .AppendLine("          KANSEI.SHISAKU_HANDORU, KANSEI.SHISAKU_EG_KATASHIKI, ")
                .AppendLine("          KANSEI.SHISAKU_EG_HAIKIRYOU, KANSEI.SHISAKU_EG_SYSTEM, ")
                .AppendLine("          KANSEI.SHISAKU_EG_KAKYUUKI, KANSEI.SHISAKU_EG_MEMO_1, ")
                .AppendLine("          KANSEI.SHISAKU_EG_MEMO_2, KANSEI.SHISAKU_TM_KUDOU, ")
                .AppendLine("          KANSEI.SHISAKU_TM_HENSOKUKI, KANSEI.SHISAKU_TM_FUKU_HENSOKUKI, ")
                .AppendLine("          KANSEI.SHISAKU_TM_MEMO_1, KANSEI.SHISAKU_TM_MEMO_2, ")
                .AppendLine("          KANSEI.SHISAKU_KATASHIKI, KANSEI.SHISAKU_SHIMUKE, ")
                .AppendLine("          KANSEI.SHISAKU_OP, KANSEI.SHISAKU_GAISOUSYOKU, ")
                .AppendLine("          KANSEI.SHISAKU_GAISOUSYOKU_NAME, KANSEI.SHISAKU_NAISOUSYOKU, ")
                .AppendLine("          KANSEI.SHISAKU_NAISOUSYOKU_NAME, KANSEI.SHISAKU_SYADAI_NO, ")
                .AppendLine("          KANSEI.SHISAKU_SHIYOU_MOKUTEKI, KANSEI.SHISAKU_SHIKEN_MOKUTEKI, ")
                .AppendLine("          KANSEI.SHISAKU_SIYOU_BUSYO, KANSEI.SHISAKU_GROUP, ")
                .AppendLine("          KANSEI.SHISAKU_SEISAKU_JUNJYO, KANSEI.SHISAKU_KANSEIBI, ")
                .AppendLine("          KANSEI.SHISAKU_KOUSHI_NO, KANSEI.SHISAKU_SEISAKU_HOUHOU_KBN, ")
                .AppendLine("          KANSEI.SHISAKU_SEISAKU_HOUHOU, KANSEI.SHISAKU_MEMO ")
                .AppendLine(" ORDER BY A.SHISAKU_SYUBETU, A.SHISAKU_GOUSYA ")
            End With
            Return sql.ToString()
        End Function

#End Region

    End Class
End Namespace
