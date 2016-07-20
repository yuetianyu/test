Namespace YosanBuhinEdit.KouseiBuhin
    ''' <summary>
    ''' 「区分一覧」　列が自動ソートされた、列の自動フィルタリングが実行された条件
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GousyaListFilterAndSortVo
#Region " メンバー変数 "
        ''' <summary>区分</summary>
        Private m_kubun As String

        ''' <summary>区分名称</summary>
        Private m_kubunName As String

        ''' <summary>ソート順項目</summary>
        Private m_sortItem As String

        ''' <summary>ソート順</summary>
        Private m_sortAscending As Boolean

        ''' <summary>車型</summary>
        Private m_shagata As String
        ''' <summary>グレード</summary>
        Private m_grade As String
        ''' <summary>仕向地・仕向け</summary>
        Private m_shimukechiShimuke As String
        ''' <summary>ハンドル</summary>
        Private m_handle As String
        ''' <summary>EG型式</summary>
        Private m_egKatashiki As String
        ''' <summary>EG排気量</summary>
        Private m_egHaikiryo As String
        ''' <summary>EGシステム</summary>
        Private m_egSystem As String
        ''' <summary>EG過給器</summary>
        Private m_egkakyuki As String
        ''' <summary>EGメモ１</summary>
        Private m_egMemo1 As String
        ''' <summary>EGメモ２</summary>
        Private m_egMemo2 As String
        ''' <summary>TM駆動</summary>
        Private m_tmKudo As String
        ''' <summary>TM変速機</summary>
        Private m_tmHensokuki As String
        ''' <summary>TM副変速機</summary>
        Private m_tmFukuHensokuki As String
        ''' <summary>TMメモ１</summary>
        Private m_tmMemo1 As String
        ''' <summary>TMメモ２</summary>
        Private m_tmMemo2 As String
        ''' <summary>型式</summary>
        Private m_katashiki As String
        ''' <summary>仕向</summary>
        Private m_shimuke As String
        ''' <summary>OP</summary>
        Private m_op As String
        ''' <summary>外装色</summary>
        Private m_gaisoShoku As String
        ''' <summary>外装色名</summary>
        Private m_gaisoShokuName As String
        ''' <summary>内装色</summary>
        Private m_naisoShoku As String
        ''' <summary>内装色名</summary>
        Private m_naisoShokuName As String
        ''' <summary>車台№</summary>
        Private m_shadaiNo As String
        ''' <summary>使用目的</summary>
        Private m_shiyouMokuteki As String
        ''' <summary>試験目的</summary>
        Private m_shikenMokuteki As String
        ''' <summary>使用部署</summary>
        Private m_shiyoBusho As String
        ''' <summary>ｸﾞﾙｰﾌﾟ</summary>
        Private m_group As String
        ''' <summary>製作順序</summary>
        Private m_seisakuJunjyo As String
        ''' <summary>完成日</summary>
        Private m_kanseibi As Nullable(Of Int32)
        ''' <summary>工指№</summary>
        Private m_koshiNo As String
        ''' <summary>製作方法区分</summary>
        Private m_seisakuHouhouKbn As String
        ''' <summary>製作方法</summary>
        Private m_seisakuHouhou As String
        ''' <summary>試作メモ</summary>
        Private m_shisakuMemo As String

#End Region
#Region " 区分、区分名称、ソート順項目、ソート順 ・・・"
        ''' <summary>
        ''' 区分
        ''' </summary>
        ''' <value>区分</value>
        ''' <returns>区分</returns>
        ''' <remarks></remarks>
        Public Property Kubun() As String
            Get
                Return m_kubun
            End Get
            Set(ByVal value As String)
                m_kubun = value
            End Set
        End Property

        ''' <summary>
        ''' 区分名称
        ''' </summary>
        ''' <value>区分名称</value>
        ''' <returns>区分名称</returns>
        ''' <remarks></remarks>
        Public Property KubunName() As String
            Get
                Return m_kubunName
            End Get
            Set(ByVal value As String)
                m_kubunName = value
            End Set
        End Property

        ''' <summary>
        ''' ソート順項目
        ''' </summary>
        ''' <value>ソート順項目</value>
        ''' <returns>ソート順項目</returns>
        ''' <remarks></remarks>
        Public Property SortItem() As String
            Get
                Return m_sortItem
            End Get
            Set(ByVal value As String)
                m_sortItem = value
            End Set
        End Property

        ''' <summary>
        ''' ソート順が昇順で行われたかどうか
        ''' </summary>
        ''' <value>ソート順</value>
        ''' <returns>ソート順</returns>
        ''' <remarks></remarks>
        Public Property SortAscending() As Boolean
            Get
                Return m_sortAscending
            End Get
            Set(ByVal value As Boolean)
                m_sortAscending = value
            End Set
        End Property

        ''' <summary>
        ''' 車型
        ''' </summary>
        ''' <value>車型</value>
        ''' <returns>車型</returns>
        ''' <remarks></remarks>
        Public Property Shagata() As String
            Get
                Return m_shagata
            End Get
            Set(ByVal value As String)
                m_shagata = value
            End Set
        End Property

        ''' <summary>
        ''' グレード
        ''' </summary>
        ''' <value>グレード</value>
        ''' <returns>グレード</returns>
        ''' <remarks></remarks>
        Public Property Grade() As String
            Get
                Return m_grade
            End Get
            Set(ByVal value As String)
                m_grade = value
            End Set
        End Property

        ''' <summary>
        ''' 仕向地・仕向け
        ''' </summary>
        ''' <value>仕向地・仕向け</value>
        ''' <returns>仕向地・仕向け</returns>
        ''' <remarks></remarks>
        Public Property ShimukechiShimuke() As String
            Get
                Return m_shimukechiShimuke
            End Get
            Set(ByVal value As String)
                m_shimukechiShimuke = value
            End Set
        End Property

        ''' <summary>
        ''' ハンドル
        ''' </summary>
        ''' <value>ハンドル</value>
        ''' <returns>ハンドル</returns>
        ''' <remarks></remarks>
        Public Property Handle() As String
            Get
                Return m_handle
            End Get
            Set(ByVal value As String)
                m_handle = value
            End Set
        End Property

        ''' <summary>
        ''' EG型式
        ''' </summary>
        ''' <value>EG型式</value>
        ''' <returns>EG型式</returns>
        ''' <remarks></remarks>
        Public Property EgKatashiki() As String
            Get
                Return m_egKatashiki
            End Get
            Set(ByVal value As String)
                m_egKatashiki = value
            End Set
        End Property

        ''' <summary>
        ''' EG排気量
        ''' </summary>
        ''' <value>EG排気量</value>
        ''' <returns>EG排気量</returns>
        ''' <remarks></remarks>
        Public Property EgHaikiryo() As String
            Get
                Return m_egHaikiryo
            End Get
            Set(ByVal value As String)
                m_egHaikiryo = value
            End Set
        End Property

        ''' <summary>
        ''' EGシステム
        ''' </summary>
        ''' <value>EGシステム</value>
        ''' <returns>EGシステム</returns>
        ''' <remarks></remarks>
        Public Property EgSystem() As String
            Get
                Return m_egSystem
            End Get
            Set(ByVal value As String)
                m_egSystem = value
            End Set
        End Property

        ''' <summary>
        ''' EG過給器
        ''' </summary>
        ''' <value>EG過給器</value>
        ''' <returns>EG過給器</returns>
        ''' <remarks></remarks>
        Public Property Egkakyuki() As String
            Get
                Return m_egkakyuki
            End Get
            Set(ByVal value As String)
                m_egkakyuki = value
            End Set
        End Property

        ''' <summary>
        ''' EGメモ１
        ''' </summary>
        ''' <value>EGメモ１</value>
        ''' <returns>EGメモ１</returns>
        ''' <remarks></remarks>
        Public Property EgMemo1() As String
            Get
                Return m_egMemo1
            End Get
            Set(ByVal value As String)
                m_egMemo1 = value
            End Set
        End Property

        ''' <summary>
        ''' EGメモ２
        ''' </summary>
        ''' <value>EGメモ２</value>
        ''' <returns>EGメモ２</returns>
        ''' <remarks></remarks>
        Public Property EgMemo2() As String
            Get
                Return m_egMemo2
            End Get
            Set(ByVal value As String)
                m_egMemo2 = value
            End Set
        End Property

        ''' <summary>
        ''' TM駆動
        ''' </summary>
        ''' <value>TM駆動</value>
        ''' <returns>TM駆動</returns>
        ''' <remarks></remarks>
        Public Property TmKudo() As String
            Get
                Return m_tmKudo
            End Get
            Set(ByVal value As String)
                m_tmKudo = value
            End Set
        End Property

        ''' <summary>
        ''' TM変速機
        ''' </summary>
        ''' <value>TM変速機</value>
        ''' <returns>TM変速機</returns>
        ''' <remarks></remarks>
        Public Property TmHensokuki() As String
            Get
                Return m_tmHensokuki
            End Get
            Set(ByVal value As String)
                m_tmHensokuki = value
            End Set
        End Property

        ''' <summary>
        ''' TM副変速機
        ''' </summary>
        ''' <value>TM副変速機</value>
        ''' <returns>TM副変速機</returns>
        ''' <remarks></remarks>
        Public Property TmFukuHensokuki() As String
            Get
                Return m_tmFukuHensokuki
            End Get
            Set(ByVal value As String)
                m_tmFukuHensokuki = value
            End Set
        End Property

        ''' <summary>
        ''' TMメモ１
        ''' </summary>
        ''' <value>TMメモ１</value>
        ''' <returns>TMメモ１</returns>
        ''' <remarks></remarks>
        Public Property TmMemo1() As String
            Get
                Return m_tmMemo1
            End Get
            Set(ByVal value As String)
                m_tmMemo1 = value
            End Set
        End Property

        ''' <summary>
        ''' TMメモ２
        ''' </summary>
        ''' <value>TMメモ２</value>
        ''' <returns>TMメモ２</returns>
        ''' <remarks></remarks>
        Public Property TmMemo2() As String
            Get
                Return m_tmMemo2
            End Get
            Set(ByVal value As String)
                m_tmMemo2 = value
            End Set
        End Property

        ''' <summary>
        ''' 型式
        ''' </summary>
        ''' <value>型式</value>
        ''' <returns>型式</returns>
        ''' <remarks></remarks>
        Public Property Katashiki() As String
            Get
                Return m_katashiki
            End Get
            Set(ByVal value As String)
                m_katashiki = value
            End Set
        End Property

        ''' <summary>
        ''' 仕向
        ''' </summary>
        ''' <value>仕向</value>
        ''' <returns>仕向</returns>
        ''' <remarks></remarks>
        Public Property Shimuke() As String
            Get
                Return m_shimuke
            End Get
            Set(ByVal value As String)
                m_shimuke = value
            End Set
        End Property

        ''' <summary>
        ''' OP
        ''' </summary>
        ''' <value>OP</value>
        ''' <returns>OP</returns>
        ''' <remarks></remarks>
        Public Property Op() As String
            Get
                Return m_op
            End Get
            Set(ByVal value As String)
                m_op = value
            End Set
        End Property

        ''' <summary>
        ''' 外装色
        ''' </summary>
        ''' <value>外装色</value>
        ''' <returns>外装色</returns>
        ''' <remarks></remarks>
        Public Property GaisoShoku() As String
            Get
                Return m_gaisoShoku
            End Get
            Set(ByVal value As String)
                m_gaisoShoku = value
            End Set
        End Property

        ''' <summary>
        ''' 外装色名
        ''' </summary>
        ''' <value>外装色名</value>
        ''' <returns>外装色名</returns>
        ''' <remarks></remarks>
        Public Property GaisoShokuName() As String
            Get
                Return m_gaisoShokuName
            End Get
            Set(ByVal value As String)
                m_gaisoShokuName = value
            End Set
        End Property

        ''' <summary>
        ''' 内装色
        ''' </summary>
        ''' <value>内装色</value>
        ''' <returns>内装色</returns>
        ''' <remarks></remarks>
        Public Property NaisoShoku() As String
            Get
                Return m_naisoShoku
            End Get
            Set(ByVal value As String)
                m_naisoShoku = value
            End Set
        End Property

        ''' <summary>
        ''' 内装色名
        ''' </summary>
        ''' <value>内装色名</value>
        ''' <returns>内装色名</returns>
        ''' <remarks></remarks>
        Public Property NaisoShokuName() As String
            Get
                Return m_naisoShokuName
            End Get
            Set(ByVal value As String)
                m_naisoShokuName = value
            End Set
        End Property

        ''' <summary>
        ''' 車台№
        ''' </summary>
        ''' <value>車台№</value>
        ''' <returns>車台№</returns>
        ''' <remarks></remarks>
        Public Property ShadaiNo() As String
            Get
                Return m_shadaiNo
            End Get
            Set(ByVal value As String)
                m_shadaiNo = value
            End Set
        End Property

        ''' <summary>
        ''' 使用目的
        ''' </summary>
        ''' <value>使用目的</value>
        ''' <returns>使用目的</returns>
        ''' <remarks></remarks>
        Public Property ShiyouMokuteki() As String
            Get
                Return m_shiyouMokuteki
            End Get
            Set(ByVal value As String)
                m_shiyouMokuteki = value
            End Set
        End Property

        ''' <summary>
        ''' 試験目的
        ''' </summary>
        ''' <value>試験目的</value>
        ''' <returns>試験目的</returns>
        ''' <remarks></remarks>
        Public Property ShikenMokuteki() As String
            Get
                Return m_shikenMokuteki
            End Get
            Set(ByVal value As String)
                m_shikenMokuteki = value
            End Set
        End Property

        ''' <summary>
        ''' 使用部署
        ''' </summary>
        ''' <value>使用部署</value>
        ''' <returns>使用部署</returns>
        ''' <remarks></remarks>
        Public Property ShiyoBusho() As String
            Get
                Return m_shiyoBusho
            End Get
            Set(ByVal value As String)
                m_shiyoBusho = value
            End Set
        End Property

        ''' <summary>
        ''' ｸﾞﾙｰﾌﾟ
        ''' </summary>
        ''' <value>ｸﾞﾙｰﾌﾟ</value>
        ''' <returns>ｸﾞﾙｰﾌﾟ</returns>
        ''' <remarks></remarks>
        Public Property Group() As String
            Get
                Return m_group
            End Get
            Set(ByVal value As String)
                m_group = value
            End Set
        End Property

        ''' <summary>
        ''' 製作順序
        ''' </summary>
        ''' <value>製作順序</value>
        ''' <returns>製作順序</returns>
        ''' <remarks></remarks>
        Public Property SeisakuJunjyo() As String
            Get
                Return m_seisakuJunjyo
            End Get
            Set(ByVal value As String)
                m_seisakuJunjyo = value
            End Set
        End Property

        ''' <summary>
        ''' 完成日
        ''' </summary>
        ''' <value>完成日</value>
        ''' <returns>完成日</returns>
        ''' <remarks></remarks>
        Public Property Kanseibi() As Nullable(Of Int32)
            Get
                Return m_kanseibi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                m_kanseibi = value
            End Set
        End Property

        ''' <summary>
        ''' 工指№
        ''' </summary>
        ''' <value>工指№</value>
        ''' <returns>工指№</returns>
        ''' <remarks></remarks>
        Public Property KoshiNo() As String
            Get
                Return m_koshiNo
            End Get
            Set(ByVal value As String)
                m_koshiNo = value
            End Set
        End Property

        ''' <summary>
        ''' 製作方法区分
        ''' </summary>
        ''' <value>製作方法区分</value>
        ''' <returns>製作方法区分</returns>
        ''' <remarks></remarks>
        Public Property SeisakuHouhouKbn() As String
            Get
                Return m_seisakuHouhouKbn
            End Get
            Set(ByVal value As String)
                m_seisakuHouhouKbn = value
            End Set
        End Property

        ''' <summary>
        ''' 製作方法
        ''' </summary>
        ''' <value>製作方法</value>
        ''' <returns>製作方法</returns>
        ''' <remarks></remarks>
        Public Property SeisakuHouhou() As String
            Get
                Return m_seisakuHouhou
            End Get
            Set(ByVal value As String)
                m_seisakuHouhou = value
            End Set
        End Property

        ''' <summary>
        ''' 試作メモ
        ''' </summary>
        ''' <value>試作メモ</value>
        ''' <returns>試作メモ</returns>
        ''' <remarks></remarks>
        Public Property ShisakuMemo() As String
            Get
                Return m_shisakuMemo
            End Get
            Set(ByVal value As String)
                m_shisakuMemo = value
            End Set
        End Property

#End Region
    End Class
End Namespace