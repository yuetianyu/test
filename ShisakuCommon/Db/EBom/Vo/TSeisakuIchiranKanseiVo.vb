Namespace Db.EBom.Vo
    ''' <summary>
    ''' 製作一覧完成車情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TSeisakuIchiranKanseiVo
        '' 発行№
        Private _HakouNo As String
        '' 改訂№
        Private _KaiteiNo As String
        '' 表示順
        Private _HyojijunNo As Nullable(of Int32)
        '' 開発符号
        Private _KaihatsuFugo As String
        '' 仕様情報№
        Private _ShiyoujyouhouNo As String
        '' 製作方法区分
        Private _SeisakuHouhouKbn As String
        '' 製作方法
        Private _SeisakuHouhou As String
        '' 号車
        Private _Gousya As String
        '' 車種
        Private _Syasyu As String
        '' グレード
        Private _Grade As String
        '' 仕向地・仕向け
        Private _Shimuke As String
        '' 仕向地・ハンドル
        Private _Handoru As String
        '' E/G仕様・排気量
        Private _EgHaikiryou As String
        '' E/G仕様・型式
        Private _EgKatashiki As String
        '' E/G仕様・過給器
        Private _EgKakyuuki As String
        '' E/G仕様・E/G名
        Private _EgEgName As String
        '' E/G仕様・ISS
        Private _EgIss As String
        '' T/M仕様・駆動方式
        Private _TmKudou As String
        '' T/M仕様・変速機
        Private _TmHensokuki As String
        '' T/M仕様・T/M名
        Private _TmTmName As String
        '' T/M仕様・R/Dｷﾞｱ比
        Private _TmRdGiahi As String
        '' 車体№
        Private _SyataiNo As String
        '' 型式・７桁型式
        Private _KatashikiScd7 As String
        '' 型式・仕向け
        Private _KatashikiShimuke As String
        '' 型式・OP
        Private _KatashikiOp As String
        '' 外装色コード
        Private _Gaisousyoku As String
        '' 外装色名
        Private _GaisousyokuName As String
        '' 内装色コード
        Private _Naisousyoku As String
        '' 内装色名
        Private _NaisousyokuName As String
        '' 使用目的
        Private _ShiyouMokuteki As String
        '' 主要確認項目
        Private _SyuyoukakuninKoumoku As String
        '' 使用部署
        Private _ShiyouBusyo As String
        '' 製作・製作順序
        Private _SeisakuJunjyo As String
        '' 製作・製作グループ
        Private _SeisakuGroup As String
        '' 製作・完成希望日
        Private _KanseiKibouBi As String
        '' メモ欄
        Private _Memo As String
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

        ''' <summary>発行№</summary>
        ''' <value>発行№</value>
        ''' <returns>発行№</returns>
        Public Property HakouNo() As String
            Get
                Return _HakouNo
            End Get
            Set(ByVal value As String)
                _HakouNo = value
            End Set
        End Property

        ''' <summary>改訂№</summary>
        ''' <value>改訂№</value>
        ''' <returns>改訂№</returns>
        Public Property KaiteiNo() As String
            Get
                Return _KaiteiNo
            End Get
            Set(ByVal value As String)
                _KaiteiNo = value
            End Set
        End Property

        ''' <summary>表示順</summary>
        ''' <value>表示順</value>
        ''' <returns>表示順</returns>
        Public Property HyojijunNo() As Nullable(of Int32)
            Get
                Return _HyojijunNo
            End Get
            Set(ByVal value As Nullable(of Int32))
                _HyojijunNo = value
            End Set
        End Property

        ''' <summary>開発符号</summary>
        ''' <value>開発符号</value>
        ''' <returns>開発符号</returns>
        Public Property KaihatsuFugo() As String
            Get
                Return _KaihatsuFugo
            End Get
            Set(ByVal value As String)
                _KaihatsuFugo = value
            End Set
        End Property

        ''' <summary>仕様情報№</summary>
        ''' <value>仕様情報№</value>
        ''' <returns>仕様情報№</returns>
        Public Property ShiyoujyouhouNo() As String
            Get
                Return _ShiyoujyouhouNo
            End Get
            Set(ByVal value As String)
                _ShiyoujyouhouNo = value
            End Set
        End Property

        ''' <summary>製作方法区分</summary>
        ''' <value>製作方法区分</value>
        ''' <returns>製作方法区分</returns>
        Public Property SeisakuHouhouKbn() As String
            Get
                Return _SeisakuHouhouKbn
            End Get
            Set(ByVal value As String)
                _SeisakuHouhouKbn = value
            End Set
        End Property

        ''' <summary>製作方法</summary>
        ''' <value>製作方法</value>
        ''' <returns>製作方法</returns>
        Public Property SeisakuHouhou() As String
            Get
                Return _SeisakuHouhou
            End Get
            Set(ByVal value As String)
                _SeisakuHouhou = value
            End Set
        End Property

        ''' <summary>号車</summary>
        ''' <value>号車</value>
        ''' <returns>号車</returns>
        Public Property Gousya() As String
            Get
                Return _Gousya
            End Get
            Set(ByVal value As String)
                _Gousya = value
            End Set
        End Property

        ''' <summary>車種</summary>
        ''' <value>車種</value>
        ''' <returns>車種</returns>
        Public Property Syasyu() As String
            Get
                Return _Syasyu
            End Get
            Set(ByVal value As String)
                _Syasyu = value
            End Set
        End Property

        ''' <summary>グレード</summary>
        ''' <value>グレード</value>
        ''' <returns>グレード</returns>
        Public Property Grade() As String
            Get
                Return _Grade
            End Get
            Set(ByVal value As String)
                _Grade = value
            End Set
        End Property

        ''' <summary>仕向地・仕向け</summary>
        ''' <value>仕向地・仕向け</value>
        ''' <returns>仕向地・仕向け</returns>
        Public Property Shimuke() As String
            Get
                Return _Shimuke
            End Get
            Set(ByVal value As String)
                _Shimuke = value
            End Set
        End Property

        ''' <summary>仕向地・ﾊﾝﾄﾞﾙ</summary>
        ''' <value>仕向地・ﾊﾝﾄﾞﾙ</value>
        ''' <returns>仕向地・ﾊﾝﾄﾞﾙ</returns>
        Public Property Handoru() As String
            Get
                Return _Handoru
            End Get
            Set(ByVal value As String)
                _Handoru = value
            End Set
        End Property

        ''' <summary>E/G仕様・排気量</summary>
        ''' <value>E/G仕様・排気量</value>
        ''' <returns>E/G仕様・排気量</returns>
        Public Property EgHaikiryou() As String
            Get
                Return _EgHaikiryou
            End Get
            Set(ByVal value As String)
                _EgHaikiryou = value
            End Set
        End Property

        ''' <summary>E/G仕様・型式</summary>
        ''' <value>E/G仕様・型式</value>
        ''' <returns>E/G仕様・型式</returns>
        Public Property EgKatashiki() As String
            Get
                Return _EgKatashiki
            End Get
            Set(ByVal value As String)
                _EgKatashiki = value
            End Set
        End Property

        ''' <summary>E/G仕様・過給器</summary>
        ''' <value>E/G仕様・過給器</value>
        ''' <returns>E/G仕様・過給器</returns>
        Public Property EgKakyuuki() As String
            Get
                Return _EgKakyuuki
            End Get
            Set(ByVal value As String)
                _EgKakyuuki = value
            End Set
        End Property

        ''' <summary>E/G仕様・E/G名</summary>
        ''' <value>E/G仕様・E/G名</value>
        ''' <returns>E/G仕様・E/G名</returns>
        Public Property EgEgName() As String
            Get
                Return _EgEgName
            End Get
            Set(ByVal value As String)
                _EgEgName = value
            End Set
        End Property

        ''' <summary>E/G仕様・ISS</summary>
        ''' <value>E/G仕様・ISS</value>
        ''' <returns>E/G仕様・ISS</returns>
        Public Property EgIss() As String
            Get
                Return _EgIss
            End Get
            Set(ByVal value As String)
                _EgIss = value
            End Set
        End Property

        ''' <summary>T/M仕様・駆動方式</summary>
        ''' <value>T/M仕様・駆動方式</value>
        ''' <returns>T/M仕様・駆動方式</returns>
        Public Property TmKudou() As String
            Get
                Return _TmKudou
            End Get
            Set(ByVal value As String)
                _TmKudou = value
            End Set
        End Property

        ''' <summary>T/M仕様・変速機</summary>
        ''' <value>T/M仕様・変速機</value>
        ''' <returns>T/M仕様・変速機</returns>
        Public Property TmHensokuki() As String
            Get
                Return _TmHensokuki
            End Get
            Set(ByVal value As String)
                _TmHensokuki = value
            End Set
        End Property

        ''' <summary>T/M仕様・T/M名</summary>
        ''' <value>T/M仕様・T/M名</value>
        ''' <returns>T/M仕様・T/M名</returns>
        Public Property TmTmName() As String
            Get
                Return _TmTmName
            End Get
            Set(ByVal value As String)
                _TmTmName = value
            End Set
        End Property

        ''' <summary>T/M仕様・R/Dｷﾞｱ比</summary>
        ''' <value>T/M仕様・R/Dｷﾞｱ比</value>
        ''' <returns>T/M仕様・R/Dｷﾞｱ比</returns>
        Public Property TmRdGiahi() As String
            Get
                Return _TmRdGiahi
            End Get
            Set(ByVal value As String)
                _TmRdGiahi = value
            End Set
        End Property

        ''' <summary>車体№</summary>
        ''' <value>車体№</value>
        ''' <returns>車体№</returns>
        Public Property SyataiNo() As String
            Get
                Return _SyataiNo
            End Get
            Set(ByVal value As String)
                _SyataiNo = value
            End Set
        End Property

        ''' <summary>型式・７桁型式</summary>
        ''' <value>型式・７桁型式</value>
        ''' <returns>型式・７桁型式</returns>
        Public Property KatashikiScd7() As String
            Get
                Return _KatashikiScd7
            End Get
            Set(ByVal value As String)
                _KatashikiScd7 = value
            End Set
        End Property

        ''' <summary>型式・仕向け</summary>
        ''' <value>型式・仕向け</value>
        ''' <returns>型式・仕向け</returns>
        Public Property KatashikiShimuke() As String
            Get
                Return _KatashikiShimuke
            End Get
            Set(ByVal value As String)
                _KatashikiShimuke = value
            End Set
        End Property

        ''' <summary>型式・OP</summary>
        ''' <value>型式・OP</value>
        ''' <returns>型式・OP</returns>
        Public Property KatashikiOp() As String
            Get
                Return _KatashikiOp
            End Get
            Set(ByVal value As String)
                _KatashikiOp = value
            End Set
        End Property

        ''' <summary>外装色コード</summary>
        ''' <value>外装色コード</value>
        ''' <returns>外装色コード</returns>
        Public Property Gaisousyoku() As String
            Get
                Return _Gaisousyoku
            End Get
            Set(ByVal value As String)
                _Gaisousyoku = value
            End Set
        End Property

        ''' <summary>外装色名</summary>
        ''' <value>外装色名</value>
        ''' <returns>外装色名</returns>
        Public Property GaisousyokuName() As String
            Get
                Return _GaisousyokuName
            End Get
            Set(ByVal value As String)
                _GaisousyokuName = value
            End Set
        End Property

        ''' <summary>内装色コード</summary>
        ''' <value>内装色コード</value>
        ''' <returns>内装色コード</returns>
        Public Property Naisousyoku() As String
            Get
                Return _Naisousyoku
            End Get
            Set(ByVal value As String)
                _Naisousyoku = value
            End Set
        End Property

        ''' <summary>内装色名</summary>
        ''' <value>内装色名</value>
        ''' <returns>内装色名</returns>
        Public Property NaisousyokuName() As String
            Get
                Return _NaisousyokuName
            End Get
            Set(ByVal value As String)
                _NaisousyokuName = value
            End Set
        End Property

        ''' <summary>使用目的</summary>
        ''' <value>使用目的</value>
        ''' <returns>使用目的</returns>
        Public Property ShiyouMokuteki() As String
            Get
                Return _ShiyouMokuteki
            End Get
            Set(ByVal value As String)
                _ShiyouMokuteki = value
            End Set
        End Property

        ''' <summary>主要確認項目</summary>
        ''' <value>主要確認項目</value>
        ''' <returns>主要確認項目</returns>
        Public Property SyuyoukakuninKoumoku() As String
            Get
                Return _SyuyoukakuninKoumoku
            End Get
            Set(ByVal value As String)
                _SyuyoukakuninKoumoku = value
            End Set
        End Property

        ''' <summary>使用部署</summary>
        ''' <value>使用部署</value>
        ''' <returns>使用部署</returns>
        Public Property ShiyouBusyo() As String
            Get
                Return _ShiyouBusyo
            End Get
            Set(ByVal value As String)
                _ShiyouBusyo = value
            End Set
        End Property

        ''' <summary>製作・製作順序</summary>
        ''' <value>製作・製作順序</value>
        ''' <returns>製作・製作順序</returns>
        Public Property SeisakuJunjyo() As String
            Get
                Return _SeisakuJunjyo
            End Get
            Set(ByVal value As String)
                _SeisakuJunjyo = value
            End Set
        End Property

        ''' <summary>製作・製作グループ</summary>
        ''' <value>製作・製作グループ</value>
        ''' <returns>製作・製作グループ</returns>
        Public Property SeisakuGroup() As String
            Get
                Return _SeisakuGroup
            End Get
            Set(ByVal value As String)
                _SeisakuGroup = value
            End Set
        End Property

        ''' <summary>製作・完成希望日</summary>
        ''' <value>製作・完成希望日</value>
        ''' <returns>製作・完成希望日</returns>
        Public Property KanseiKibouBi() As String
            Get
                Return _KanseiKibouBi
            End Get
            Set(ByVal value As String)
                _KanseiKibouBi = value
            End Set
        End Property

        ''' <summary>メモ欄</summary>
        ''' <value>メモ欄</value>
        ''' <returns>メモ欄</returns>
        Public Property Memo() As String
            Get
                Return _Memo
            End Get
            Set(ByVal value As String)
                _Memo = value
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
