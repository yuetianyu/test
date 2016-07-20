Namespace Db.EBom.Vo
    ''' <summary>
    ''' 12桁型式
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac0540Vo
        '' 開発符号 
        Private _KaihatsuFugo As String
        '' 装備改訂No. 
        Private _SobiKaiteiNo As String
        '' 7桁型式識別コード 
        Private _KatashikiScd7 As String
        '' 仕向地コード 
        Private _ShimukechiCode As String
        '' OP改訂No. 
        Private _OpKaiteiNo As String
        '' OPコード 
        Private _OpCode As String
        '' 品目番号 
        Private _HinmokuNo As String
        '' 列No 
        Private _ColNo As Nullable(of Int32)
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

        ''' <summary>装備改訂No.</summary>
        ''' <value>装備改訂No.</value>
        ''' <returns>装備改訂No.</returns>
        Public Property SobiKaiteiNo() As String
            Get
                Return _SobiKaiteiNo
            End Get
            Set(ByVal value As String)
                _SobiKaiteiNo = value
            End Set
        End Property

        ''' <summary>7桁型式識別コード</summary>
        ''' <value>7桁型式識別コード</value>
        ''' <returns>7桁型式識別コード</returns>
        Public Property KatashikiScd7() As String
            Get
                Return _KatashikiScd7
            End Get
            Set(ByVal value As String)
                _KatashikiScd7 = value
            End Set
        End Property

        ''' <summary>仕向地コード</summary>
        ''' <value>仕向地コード</value>
        ''' <returns>仕向地コード</returns>
        Public Property ShimukechiCode() As String
            Get
                Return _ShimukechiCode
            End Get
            Set(ByVal value As String)
                _ShimukechiCode = value
            End Set
        End Property

        ''' <summary>OP改訂No.</summary>
        ''' <value>OP改訂No.</value>
        ''' <returns>OP改訂No.</returns>
        Public Property OpKaiteiNo() As String
            Get
                Return _OpKaiteiNo
            End Get
            Set(ByVal value As String)
                _OpKaiteiNo = value
            End Set
        End Property

        ''' <summary>OPコード</summary>
        ''' <value>OPコード</value>
        ''' <returns>OPコード</returns>
        Public Property OpCode() As String
            Get
                Return _OpCode
            End Get
            Set(ByVal value As String)
                _OpCode = value
            End Set
        End Property

        ''' <summary>品目番号</summary>
        ''' <value>品目番号</value>
        ''' <returns>品目番号</returns>
        Public Property HinmokuNo() As String
            Get
                Return _HinmokuNo
            End Get
            Set(ByVal value As String)
                _HinmokuNo = value
            End Set
        End Property

        ''' <summary>列No</summary>
        ''' <value>列No</value>
        ''' <returns>列No</returns>
        Public Property ColNo() As Nullable(of Int32)
            Get
                Return _ColNo
            End Get
            Set(ByVal value As Nullable(of Int32))
                _ColNo = value
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
