Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作イベント完成車情報改訂
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TShisakuEventKanseiKaiteiVo
        '' 試作イベントコード
        Private _ShisakuEventCode As String
        '' 表示順
        Private _HyojijunNo As Nullable(of Int32)
       
        '' 試作車型
        Private _ShisakuSyagata As String
        '' 試作グレード
        Private _ShisakuGrade As String
        '' 仕向地・仕向け
        Private _ShisakuShimukechiShimuke As String
        '' 試作ハンドル
        Private _ShisakuHandoru As String
        '' 試作E/G型式
        Private _ShisakuEgKatashiki As String
        '' 試作E/G排気量
        Private _ShisakuEgHaikiryou As String
        '' 試作E/Gシステム
        Private _ShisakuEgSystem As String
        '' 試作E/G過給機
        Private _ShisakuEgKakyuuki As String
        '' 試作E/Gメモ１
        Private _ShisakuEgMemo1 As String
        '' 試作E/Gメモ２
        Private _ShisakuEgMemo2 As String
        '' 試作T/M駆動
        Private _ShisakuTmKudou As String
        '' 試作T/M変速機
        Private _ShisakuTmHensokuki As String
        '' 試作T/M副変速機
        Private _ShisakuTmFukuHensokuki As String
        '' 試作T/Mメモ１
        Private _ShisakuTmMemo1 As String
        '' 試作T/Mメモ２
        Private _ShisakuTmMemo2 As String
        '' 試作型式
        Private _ShisakuKatashiki As String
        '' 試作仕向け
        Private _ShisakuShimuke As String
        '' 試作OP
        Private _ShisakuOp As String
        '' 試作外装色
        Private _ShisakuGaisousyoku As String
        '' 試作外装色名
        Private _ShisakuGaisousyokuName As String
        '' 試作内装色
        Private _ShisakuNaisousyoku As String
        '' 試作内装色名
        Private _ShisakuNaisousyokuName As String
        '' 試作車台№
        Private _ShisakuSyadaiNo As String
        '' 試作使用目的
        Private _ShisakuShiyouMokuteki As String
        '' 試作試験目的（主要確認項目）
        Private _ShisakuShikenMokuteki As String
        '' 試作使用部署
        Private _ShisakuSiyouBusyo As String
        '' 試作グループ
        Private _ShisakuGroup As String
        '' 製作・製作順序
        Private _ShisakuSeisakuJunjyo As String
        '' 試作完成日
        Private _ShisakuKanseibi As Nullable(Of Int32)
        '' 試作工指№
        Private _ShisakuKoushiNo As String
        '' 製作方法区分
        Private _ShisakuSeisakuHouhouKbn As String
        '' 製作方法
        Private _ShisakuSeisakuHouhou As String
        '' メモ欄
        Private _ShisakuMemo As String
        '' 作成ユーザーID
        Private _CreatedUserId As String
        '' 作成日
        Private _CreatedDate As String
        '' 作成時
        Private _CreatedTime As String
        '' 更新ユーザーID
        Private _UpdatedUserId As String
        '' 更新日
        Private _UpdatedDate As String
        '' 更新時間
        Private _UpdatedTime As String

        ''' <summary>試作イベントコード</summary>
        ''' <value>試作イベントコード</value>
        ''' <returns>試作イベントコード</returns>
        Public Property ShisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
            End Set
        End Property

        ''' <summary>表示順</summary>
        ''' <value>表示順</value>
        ''' <returns>表示順</returns>
        Public Property HyojijunNo() As Nullable(Of Int32)
            Get
                Return _HyojijunNo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _HyojijunNo = value
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

        ''' <summary>作成ユーザーID</summary>
        ''' <value>作成ユーザーID</value>
        ''' <returns>作成ユーザーID</returns>
        Public Property CreatedUserId() As String
            Get
                Return _CreatedUserId
            End Get
            Set(ByVal value As String)
                _CreatedUserId = value
            End Set
        End Property

        ''' <summary>作成日</summary>
        ''' <value>作成日</value>
        ''' <returns>作成日</returns>
        Public Property CreatedDate() As String
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As String)
                _CreatedDate = value
            End Set
        End Property

        ''' <summary>作成時</summary>
        ''' <value>作成時</value>
        ''' <returns>作成時</returns>
        Public Property CreatedTime() As String
            Get
                Return _CreatedTime
            End Get
            Set(ByVal value As String)
                _CreatedTime = value
            End Set
        End Property

        ''' <summary>更新ユーザーID</summary>
        ''' <value>更新ユーザーID</value>
        ''' <returns>更新ユーザーID</returns>
        Public Property UpdatedUserId() As String
            Get
                Return _UpdatedUserId
            End Get
            Set(ByVal value As String)
                _UpdatedUserId = value
            End Set
        End Property

        ''' <summary>更新日</summary>
        ''' <value>更新日</value>
        ''' <returns>更新日</returns>
        Public Property UpdatedDate() As String
            Get
                Return _UpdatedDate
            End Get
            Set(ByVal value As String)
                _UpdatedDate = value
            End Set
        End Property

        ''' <summary>更新時間</summary>
        ''' <value>更新時間</value>
        ''' <returns>更新時間</returns>
        Public Property UpdatedTime() As String
            Get
                Return _UpdatedTime
            End Get
            Set(ByVal value As String)
                _UpdatedTime = value
            End Set
        End Property
    End Class
End Namespace
