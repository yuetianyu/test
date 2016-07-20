Namespace Db.EBom.Vo
    ''' <summary>
    ''' 開発車機能ブロック
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac0080Vo
        '' 開発符号 
        Private _KaihatsuFugo As String
        '' ブロックNo.(機能) 
        Private _BlockNoKino As String
        '' 改訂No.(機能) 
        Private _KaiteiNoKino As String
        '' 追加理由 
        Private _TsuikariyuKijutsu As String
        '' 担当部署 
        Private _TantoBusho As String
        '' 機能識別区分 
        Private _KinoShikiKbn As String
        '' 対応最上位部位コード 
        Private _BuiCode As String
        '' U/L区分 
        Private _UlKbn As String
        '' M/T区分 
        Private _MtKbn As String
        '' 開発符号(設計ベース車) 
        Private _KaihatsufgDsg As String
        '' 仕様書一連No.(設計ベース車) 
        Private _ShiyoshoSeqnoDsg As String
        '' システム区分ID 
        Private _SystemKbnId As String
        '' ステータス 
        Private _Status As String
        '' サイト区分 
        Private _SiteKbn As String
        '' 採用年月日 
        Private _SaiyoDate As Nullable(of Int32)
        '' 廃止年月日 
        Private _HaisiDate As Nullable(of Int32)
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

        ''' <summary>ブロックNo.(機能)</summary>
        ''' <value>ブロックNo.(機能)</value>
        ''' <returns>ブロックNo.(機能)</returns>
        Public Property BlockNoKino() As String
            Get
                Return _BlockNoKino
            End Get
            Set(ByVal value As String)
                _BlockNoKino = value
            End Set
        End Property

        ''' <summary>改訂No.(機能)</summary>
        ''' <value>改訂No.(機能)</value>
        ''' <returns>改訂No.(機能)</returns>
        Public Property KaiteiNoKino() As String
            Get
                Return _KaiteiNoKino
            End Get
            Set(ByVal value As String)
                _KaiteiNoKino = value
            End Set
        End Property

        ''' <summary>追加理由</summary>
        ''' <value>追加理由</value>
        ''' <returns>追加理由</returns>
        Public Property TsuikariyuKijutsu() As String
            Get
                Return _TsuikariyuKijutsu
            End Get
            Set(ByVal value As String)
                _TsuikariyuKijutsu = value
            End Set
        End Property

        ''' <summary>担当部署</summary>
        ''' <value>担当部署</value>
        ''' <returns>担当部署</returns>
        Public Property TantoBusho() As String
            Get
                Return _TantoBusho
            End Get
            Set(ByVal value As String)
                _TantoBusho = value
            End Set
        End Property

        ''' <summary>機能識別区分</summary>
        ''' <value>機能識別区分</value>
        ''' <returns>機能識別区分</returns>
        Public Property KinoShikiKbn() As String
            Get
                Return _KinoShikiKbn
            End Get
            Set(ByVal value As String)
                _KinoShikiKbn = value
            End Set
        End Property

        ''' <summary>対応最上位部位コード</summary>
        ''' <value>対応最上位部位コード</value>
        ''' <returns>対応最上位部位コード</returns>
        Public Property BuiCode() As String
            Get
                Return _BuiCode
            End Get
            Set(ByVal value As String)
                _BuiCode = value
            End Set
        End Property

        ''' <summary>U/L区分</summary>
        ''' <value>U/L区分</value>
        ''' <returns>U/L区分</returns>
        Public Property UlKbn() As String
            Get
                Return _UlKbn
            End Get
            Set(ByVal value As String)
                _UlKbn = value
            End Set
        End Property

        ''' <summary>M/T区分</summary>
        ''' <value>M/T区分</value>
        ''' <returns>M/T区分</returns>
        Public Property MtKbn() As String
            Get
                Return _MtKbn
            End Get
            Set(ByVal value As String)
                _MtKbn = value
            End Set
        End Property

        ''' <summary>開発符号(設計ベース車)</summary>
        ''' <value>開発符号(設計ベース車)</value>
        ''' <returns>開発符号(設計ベース車)</returns>
        Public Property KaihatsufgDsg() As String
            Get
                Return _KaihatsufgDsg
            End Get
            Set(ByVal value As String)
                _KaihatsufgDsg = value
            End Set
        End Property

        ''' <summary>仕様書一連No.(設計ベース車)</summary>
        ''' <value>仕様書一連No.(設計ベース車)</value>
        ''' <returns>仕様書一連No.(設計ベース車)</returns>
        Public Property ShiyoshoSeqnoDsg() As String
            Get
                Return _ShiyoshoSeqnoDsg
            End Get
            Set(ByVal value As String)
                _ShiyoshoSeqnoDsg = value
            End Set
        End Property

        ''' <summary>システム区分ID</summary>
        ''' <value>システム区分ID</value>
        ''' <returns>システム区分ID</returns>
        Public Property SystemKbnId() As String
            Get
                Return _SystemKbnId
            End Get
            Set(ByVal value As String)
                _SystemKbnId = value
            End Set
        End Property

        ''' <summary>ステータス</summary>
        ''' <value>ステータス</value>
        ''' <returns>ステータス</returns>
        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
            End Set
        End Property

        ''' <summary>サイト区分</summary>
        ''' <value>サイト区分</value>
        ''' <returns>サイト区分</returns>
        Public Property SiteKbn() As String
            Get
                Return _SiteKbn
            End Get
            Set(ByVal value As String)
                _SiteKbn = value
            End Set
        End Property

        ''' <summary>採用年月日</summary>
        ''' <value>採用年月日</value>
        ''' <returns>採用年月日</returns>
        Public Property SaiyoDate() As Nullable(of Int32)
            Get
                Return _SaiyoDate
            End Get
            Set(ByVal value As Nullable(of Int32))
                _SaiyoDate = value
            End Set
        End Property

        ''' <summary>廃止年月日</summary>
        ''' <value>廃止年月日</value>
        ''' <returns>廃止年月日</returns>
        Public Property HaisiDate() As Nullable(of Int32)
            Get
                Return _HaisiDate
            End Get
            Set(ByVal value As Nullable(of Int32))
                _HaisiDate = value
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
