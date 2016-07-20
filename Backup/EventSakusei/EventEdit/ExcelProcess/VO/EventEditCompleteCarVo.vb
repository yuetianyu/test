Namespace EventEdit.Vo
    ''' <summary>
    ''' 完成車
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EventEditCompleteCarVo
        ''' <summary>
        ''' 試作種別
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuSyubetu As String
        ''' <summary>
        ''' 試作号車
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuGousya As String


        ''' <summary>
        ''' 試作車型
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuSyagata As String
        ''' <summary>
        ''' 試作グレード
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuGrade As String
        ''' <summary>
        ''' 仕向地・仕向け
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuShimukechiShimuke As String
        ''' <summary>
        ''' 試作ハンドル
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuHandoru As String
        ''' <summary>
        ''' 試作E/G型式
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuEgKatashiki As String
        ''' <summary>
        ''' 試作E/G排気量
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuEgHaikiryou As String
        ''' <summary>
        ''' 試作E/Gシステム
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuEgSystem As String
        ''' <summary>
        ''' 試作E/G過給機
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuEgKakyuuki As String
        ''' <summary>
        ''' 試作E/Gメモ１
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuEgMemo1 As String
        ''' <summary>
        ''' 試作E/Gメモ２
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuEgMemo2 As String
        ''' <summary>
        ''' 試作T/M駆動
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuTmKudou As String
        ''' <summary>
        ''' 試作T/M変速機
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuTmHensokuki As String
        ''' <summary>
        ''' 試作T/M副変速機
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuTmFukuHensokuki As String
        ''' <summary>
        ''' 試作T/Mメモ１
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuTmMemo1 As String
        ''' <summary>
        ''' 試作T/Mメモ２
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuTmMemo2 As String
        ''' <summary>
        ''' 試作型式
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuKatashiki As String
        ''' <summary>
        ''' 試作仕向け
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuShimuke As String
        ''' <summary>
        ''' 試作OP
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuOp As String
        ''' <summary>
        ''' 試作外装色
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuGaisousyoku As String
        ''' <summary>
        ''' 試作外装色名
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuGaisousyokuName As String
        ''' <summary>
        ''' 試作内装色
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuNaisousyoku As String
        ''' <summary>
        ''' 試作内装色名
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuNaisousyokuName As String
        ''' <summary>
        ''' 試作車台№
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuSyadaiNo As String
        ''' <summary>
        ''' 試作使用目的
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuShiyouMokuteki As String
        ''' <summary>
        ''' 試作試験目的（主要確認項目）
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuShikenMokuteki As String
        ''' <summary>
        ''' 試作使用部署
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuSiyouBusyo As String
        ''' <summary>
        ''' 試作グループ
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuGroup As String
        ''' <summary>
        ''' 製作・製作順序
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuSeisakuJunjyo As String
        ''' <summary>
        ''' 試作完成日
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuKanseibi As Nullable(Of Int32)
        ''' <summary>
        ''' 試作工指№
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuKoushiNo As String
        ''' <summary>
        ''' 製作方法区分
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuSeisakuHouhouKbn As String
        ''' <summary>
        ''' 製作方法
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuSeisakuHouhou As String
        ''' <summary>
        ''' メモ欄
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuMemo As String


        ''' <summary>試作種別</summary>
        ''' <value>試作種別</value>
        ''' <returns>試作種別</returns>
        Public Property ShisakuSyubetu() As String
            Get
                Return _ShisakuSyubetu
            End Get
            Set(ByVal value As String)
                _ShisakuSyubetu = value
            End Set
        End Property

        ''' <summary>試作号車</summary>
        ''' <value>試作号車</value>
        ''' <returns>試作号車</returns>
        Public Property ShisakuGousya() As String
            Get
                Return _ShisakuGousya
            End Get
            Set(ByVal value As String)
                _ShisakuGousya = value
            End Set
        End Property

        ''' <summary>試作車型</summary>
        ''' <value>試作車型</value>
        ''' <returns>試作車型</returns>
        Public Property ShisakuSyagata() As String
            Get
                Return _ShisakuSyagata
            End Get
            Set(ByVal value As String)
                _ShisakuSyagata = value
            End Set
        End Property

        ''' <summary>試作グレード</summary>
        ''' <value>試作グレード</value>
        ''' <returns>試作グレード</returns>
        Public Property ShisakuGrade() As String
            Get
                Return _ShisakuGrade
            End Get
            Set(ByVal value As String)
                _ShisakuGrade = value
            End Set
        End Property

        ''' <summary>仕向地・仕向け</summary>
        ''' <value>仕向地・仕向け</value>
        ''' <returns>仕向地・仕向け</returns>
        Public Property ShisakuShimukechiShimuke() As String
            Get
                Return _ShisakuShimukechiShimuke
            End Get
            Set(ByVal value As String)
                _ShisakuShimukechiShimuke = value
            End Set
        End Property

        ''' <summary>試作ハンドル</summary>
        ''' <value>試作ハンドル</value>
        ''' <returns>試作ハンドル</returns>
        Public Property ShisakuHandoru() As String
            Get
                Return _ShisakuHandoru
            End Get
            Set(ByVal value As String)
                _ShisakuHandoru = value
            End Set
        End Property


        ''' <summary>試作E/G型式</summary>
        ''' <value>試作E/G型式</value>
        ''' <returns>試作E/G型式</returns>
        Public Property ShisakuEgKatashiki() As String
            Get
                Return _ShisakuEgKatashiki
            End Get
            Set(ByVal value As String)
                _ShisakuEgKatashiki = value
            End Set
        End Property

        ''' <summary>試作E/G排気量</summary>
        ''' <value>試作E/G排気量</value>
        ''' <returns>試作E/G排気量</returns>
        Public Property ShisakuEgHaikiryou() As String
            Get
                Return _ShisakuEgHaikiryou
            End Get
            Set(ByVal value As String)
                _ShisakuEgHaikiryou = value
            End Set
        End Property

        ''' <summary>試作E/Gシステム</summary>
        ''' <value>試作E/Gシステム</value>
        ''' <returns>試作E/Gシステム</returns>
        Public Property ShisakuEgSystem() As String
            Get
                Return _ShisakuEgSystem
            End Get
            Set(ByVal value As String)
                _ShisakuEgSystem = value
            End Set
        End Property

        ''' <summary>試作E/G過給機</summary>
        ''' <value>試作E/G過給機</value>
        ''' <returns>試作E/G過給機</returns>
        Public Property ShisakuEgKakyuuki() As String
            Get
                Return _ShisakuEgKakyuuki
            End Get
            Set(ByVal value As String)
                _ShisakuEgKakyuuki = value
            End Set
        End Property

        ''' <summary>試作E/Gメモ１</summary>
        ''' <value>試作E/Gメモ１</value>
        ''' <returns>試作E/Gメモ１</returns>
        Public Property ShisakuEgMemo1() As String
            Get
                Return _ShisakuEgMemo1
            End Get
            Set(ByVal value As String)
                _ShisakuEgMemo1 = value
            End Set
        End Property

        ''' <summary>試作E/Gメモ２</summary>
        ''' <value>試作E/Gメモ２</value>
        ''' <returns>試作E/Gメモ２</returns>
        Public Property ShisakuEgMemo2() As String
            Get
                Return _ShisakuEgMemo2
            End Get
            Set(ByVal value As String)
                _ShisakuEgMemo2 = value
            End Set
        End Property

        ''' <summary>試作T/M駆動</summary>
        ''' <value>試作T/M駆動</value>
        ''' <returns>試作T/M駆動</returns>
        Public Property ShisakuTmKudou() As String
            Get
                Return _ShisakuTmKudou
            End Get
            Set(ByVal value As String)
                _ShisakuTmKudou = value
            End Set
        End Property

        ''' <summary>試作T/M変速機</summary>
        ''' <value>試作T/M変速機</value>
        ''' <returns>試作T/M変速機</returns>
        Public Property ShisakuTmHensokuki() As String
            Get
                Return _ShisakuTmHensokuki
            End Get
            Set(ByVal value As String)
                _ShisakuTmHensokuki = value
            End Set
        End Property

        ''' <summary>試作T/M副変速機</summary>
        ''' <value>試作T/M副変速機</value>
        ''' <returns>試作T/M副変速機</returns>
        Public Property ShisakuTmFukuHensokuki() As String
            Get
                Return _ShisakuTmFukuHensokuki
            End Get
            Set(ByVal value As String)
                _ShisakuTmFukuHensokuki = value
            End Set
        End Property

        ''' <summary>試作T/Mメモ１</summary>
        ''' <value>試作T/Mメモ１</value>
        ''' <returns>試作T/Mメモ１</returns>
        Public Property ShisakuTmMemo1() As String
            Get
                Return _ShisakuTmMemo1
            End Get
            Set(ByVal value As String)
                _ShisakuTmMemo1 = value
            End Set
        End Property

        ''' <summary>試作T/Mメモ２</summary>
        ''' <value>試作T/Mメモ２</value>
        ''' <returns>試作T/Mメモ２</returns>
        Public Property ShisakuTmMemo2() As String
            Get
                Return _ShisakuTmMemo2
            End Get
            Set(ByVal value As String)
                _ShisakuTmMemo2 = value
            End Set
        End Property

        ''' <summary>試作型式</summary>
        ''' <value>試作型式</value>
        ''' <returns>試作型式</returns>
        Public Property ShisakuKatashiki() As String
            Get
                Return _ShisakuKatashiki
            End Get
            Set(ByVal value As String)
                _ShisakuKatashiki = value
            End Set
        End Property

        ''' <summary>試作仕向け</summary>
        ''' <value>試作仕向け</value>
        ''' <returns>試作仕向け</returns>
        Public Property ShisakuShimuke() As String
            Get
                Return _ShisakuShimuke
            End Get
            Set(ByVal value As String)
                _ShisakuShimuke = value
            End Set
        End Property

        ''' <summary>試作OP</summary>
        ''' <value>試作OP</value>
        ''' <returns>試作OP</returns>
        Public Property ShisakuOp() As String
            Get
                Return _ShisakuOp
            End Get
            Set(ByVal value As String)
                _ShisakuOp = value
            End Set
        End Property

        ''' <summary>試作外装色</summary>
        ''' <value>試作外装色</value>
        ''' <returns>試作外装色</returns>
        Public Property ShisakuGaisousyoku() As String
            Get
                Return _ShisakuGaisousyoku
            End Get
            Set(ByVal value As String)
                _ShisakuGaisousyoku = value
            End Set
        End Property

        ''' <summary>試作外装色名</summary>
        ''' <value>試作外装色名</value>
        ''' <returns>試作外装色名</returns>
        Public Property ShisakuGaisousyokuName() As String
            Get
                Return _ShisakuGaisousyokuName
            End Get
            Set(ByVal value As String)
                _ShisakuGaisousyokuName = value
            End Set
        End Property

        ''' <summary>試作内装色</summary>
        ''' <value>試作内装色</value>
        ''' <returns>試作内装色</returns>
        Public Property ShisakuNaisousyoku() As String
            Get
                Return _ShisakuNaisousyoku
            End Get
            Set(ByVal value As String)
                _ShisakuNaisousyoku = value
            End Set
        End Property

        ''' <summary>試作内装色名</summary>
        ''' <value>試作内装色名</value>
        ''' <returns>試作内装色名</returns>
        Public Property ShisakuNaisousyokuName() As String
            Get
                Return _ShisakuNaisousyokuName
            End Get
            Set(ByVal value As String)
                _ShisakuNaisousyokuName = value
            End Set
        End Property

        ''' <summary>試作車台№</summary>
        ''' <value>試作車台№</value>
        ''' <returns>試作車台№</returns>
        Public Property ShisakuSyadaiNo() As String
            Get
                Return _ShisakuSyadaiNo
            End Get
            Set(ByVal value As String)
                _ShisakuSyadaiNo = value
            End Set
        End Property

        ''' <summary>試作使用目的</summary>
        ''' <value>試作使用目的</value>
        ''' <returns>試作使用目的</returns>
        Public Property ShisakuShiyouMokuteki() As String
            Get
                Return _ShisakuShiyouMokuteki
            End Get
            Set(ByVal value As String)
                _ShisakuShiyouMokuteki = value
            End Set
        End Property

        ''' <summary>試作試験目的（主要確認項目）</summary>
        ''' <value>試作試験目的（主要確認項目）</value>
        ''' <returns>試作試験目的（主要確認項目）</returns>
        Public Property ShisakuShikenMokuteki() As String
            Get
                Return _ShisakuShikenMokuteki
            End Get
            Set(ByVal value As String)
                _ShisakuShikenMokuteki = value
            End Set
        End Property

        ''' <summary>試作使用部署</summary>
        ''' <value>試作使用部署</value>
        ''' <returns>試作使用部署</returns>
        Public Property ShisakuSiyouBusyo() As String
            Get
                Return _ShisakuSiyouBusyo
            End Get
            Set(ByVal value As String)
                _ShisakuSiyouBusyo = value
            End Set
        End Property

        ''' <summary>試作グループ</summary>
        ''' <value>試作グループ</value>
        ''' <returns>試作グループ</returns>
        Public Property ShisakuGroup() As String
            Get
                Return _ShisakuGroup
            End Get
            Set(ByVal value As String)
                _ShisakuGroup = value
            End Set
        End Property

        ''' <summary>製作・製作順序</summary>
        ''' <value>製作・製作順序</value>
        ''' <returns>製作・製作順序</returns>
        Public Property ShisakuSeisakuJunjyo() As String
            Get
                Return _ShisakuSeisakuJunjyo
            End Get
            Set(ByVal value As String)
                _ShisakuSeisakuJunjyo = value
            End Set
        End Property

        ''' <summary>試作完成日</summary>
        ''' <value>試作完成日</value>
        ''' <returns>試作完成日</returns>
        Public Property ShisakuKanseibi() As Nullable(Of Int32)
            Get
                Return _ShisakuKanseibi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShisakuKanseibi = value
            End Set
        End Property

        ''' <summary>試作工指№</summary>
        ''' <value>試作工指№</value>
        ''' <returns>試作工指№</returns>
        Public Property ShisakuKoushiNo() As String
            Get
                Return _ShisakuKoushiNo
            End Get
            Set(ByVal value As String)
                _ShisakuKoushiNo = value
            End Set
        End Property

        ''' <summary>製作方法区分</summary>
        ''' <value>製作方法区分</value>
        ''' <returns>製作方法区分</returns>
        Public Property ShisakuSeisakuHouhouKbn() As String
            Get
                Return _ShisakuSeisakuHouhouKbn
            End Get
            Set(ByVal value As String)
                _ShisakuSeisakuHouhouKbn = value
            End Set
        End Property

        ''' <summary>製作方法</summary>
        ''' <value>製作方法</value>
        ''' <returns>製作方法</returns>
        Public Property ShisakuSeisakuHouhou() As String
            Get
                Return _ShisakuSeisakuHouhou
            End Get
            Set(ByVal value As String)
                _ShisakuSeisakuHouhou = value
            End Set
        End Property

        ''' <summary>メモ欄</summary>
        ''' <value>メモ欄</value>
        ''' <returns>メモ欄</returns>
        Public Property ShisakuMemo() As String
            Get
                Return _ShisakuMemo
            End Get
            Set(ByVal value As String)
                _ShisakuMemo = value
            End Set
        End Property

    End Class
End Namespace

