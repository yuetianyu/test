Namespace Db.EBom.Vo
    ''' <summary>
    ''' 開発車
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac0020Vo
        '' 開発符号 
        Private _KaihatsuFugo As String
        '' 車系コード 
        Private _ShakeiCode As String
        '' FMC開発符号 
        Private _FmcKaihatsuFugo As String
        '' 開発符号表示設定 
        Private _KaihatsufgPln As String
        '' 仕様書一連No.(商企ベース車) 
        Private _ShiyoshoSeqnoPln As String
        '' 登録年月日 
        Private _TorokuDate As Nullable(of Int32)
        '' 廃止年月日 
        Private _HaishiDate As Nullable(of Int32)
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

        ''' <summary>車系コード</summary>
        ''' <value>車系コード</value>
        ''' <returns>車系コード</returns>
        Public Property ShakeiCode() As String
            Get
                Return _ShakeiCode
            End Get
            Set(ByVal value As String)
                _ShakeiCode = value
            End Set
        End Property

        ''' <summary>FMC開発符号</summary>
        ''' <value>FMC開発符号</value>
        ''' <returns>FMC開発符号</returns>
        Public Property FmcKaihatsuFugo() As String
            Get
                Return _FmcKaihatsuFugo
            End Get
            Set(ByVal value As String)
                _FmcKaihatsuFugo = value
            End Set
        End Property

        ''' <summary>開発符号表示設定</summary>
        ''' <value>開発符号表示設定</value>
        ''' <returns>開発符号表示設定</returns>
        Public Property KaihatsufgPln() As String
            Get
                Return _KaihatsufgPln
            End Get
            Set(ByVal value As String)
                _KaihatsufgPln = value
            End Set
        End Property

        ''' <summary>仕様書一連No.(商企ベース車)</summary>
        ''' <value>仕様書一連No.(商企ベース車)</value>
        ''' <returns>仕様書一連No.(商企ベース車)</returns>
        Public Property ShiyoshoSeqnoPln() As String
            Get
                Return _ShiyoshoSeqnoPln
            End Get
            Set(ByVal value As String)
                _ShiyoshoSeqnoPln = value
            End Set
        End Property

        ''' <summary>登録年月日</summary>
        ''' <value>登録年月日</value>
        ''' <returns>登録年月日</returns>
        Public Property TorokuDate() As Nullable(of Int32)
            Get
                Return _TorokuDate
            End Get
            Set(ByVal value As Nullable(of Int32))
                _TorokuDate = value
            End Set
        End Property

        ''' <summary>廃止年月日</summary>
        ''' <value>廃止年月日</value>
        ''' <returns>廃止年月日</returns>
        Public Property HaishiDate() As Nullable(of Int32)
            Get
                Return _HaishiDate
            End Get
            Set(ByVal value As Nullable(of Int32))
                _HaishiDate = value
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
