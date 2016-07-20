Namespace Db.EBom.Vo
    ''' <summary>
    ''' 製作一覧ラインOP項目情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TSeisakuIchiranOpkoumokuVo
        '' 発行№
        Private _HakouNo As String
        '' 改訂№
        Private _KaiteiNo As String
        '' 種別
        Private _Syubetu As String
        '' 表示順
        Private _HyojijunNo As Nullable(of Int32)
        '' OP項目列表示順
        Private _OpKoumokuHyojijunNo As Nullable(Of Int32)
        '' 号車
        Private _Gousya As String
        '' 開発符号
        Private _KaihatsuFugo As String
        '' OPスペックコード
        Private _OpSpecCode As String
        '' OP項目名
        Private _OpName As String
        '' 適用
        Private _Tekiyou As String
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

        ''' <summary>種別</summary>
        ''' <value>種別</value>
        ''' <returns>種別</returns>
        Public Property Syubetu() As String
            Get
                Return _Syubetu
            End Get
            Set(ByVal value As String)
                _Syubetu = value
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

        ''' <summary>OP項目列表示順</summary>
        ''' <value>OP項目列表示順</value>
        ''' <returns>OP項目列表示順</returns>
        Public Property OpKoumokuHyojijunNo() As Nullable(Of Int32)
            Get
                Return _OpKoumokuHyojijunNo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _OpKoumokuHyojijunNo = value
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

        ''' <summary>OPスペックコード</summary>
        ''' <value>OPスペックコード</value>
        ''' <returns>OPスペックコード</returns>
        Public Property OpSpecCode() As String
            Get
                Return _OpSpecCode
            End Get
            Set(ByVal value As String)
                _OpSpecCode = value
            End Set
        End Property

        ''' <summary>OP項目名</summary>
        ''' <value>OP項目名</value>
        ''' <returns>OP項目名</returns>
        Public Property OpName() As String
            Get
                Return _OpName
            End Get
            Set(ByVal value As String)
                _OpName = value
            End Set
        End Property

        ''' <summary>適用</summary>
        ''' <value>適用</value>
        ''' <returns>適用</returns>
        Public Property Tekiyou() As String
            Get
                Return _Tekiyou
            End Get
            Set(ByVal value As String)
                _Tekiyou = value
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
