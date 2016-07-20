Namespace Db.EBom.Vo
    ''' <summary>
    ''' 12桁型式-F品番
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac1750Vo
        '' 開発符号 
        Private _KaihatsuFugo As String
        '' 装備改訂No 
        Private _SobiKaiteiNo As String
        '' 7桁型式識別コード 
        Private _KatashikiScd7 As String
        '' 仕向地コード 
        Private _ShimukechiCode As String
        '' 列No 
        Private _ColNo As Nullable(of Int32)
        '' Ｆ品番 
        Private _FBuhinNo As String
        '' 仕様差品番改訂No. 
        Private _ShiyosaKaiteiNo As String
        '' 部位コード 
        Private _BuiCode As String
        '' 装備仕様差組合せコード 
        Private _KumiawaseCodeSo As Nullable(of Int32)
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

        ''' <summary>装備改訂No</summary>
        ''' <value>装備改訂No</value>
        ''' <returns>装備改訂No</returns>
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

        ''' <summary>Ｆ品番</summary>
        ''' <value>Ｆ品番</value>
        ''' <returns>Ｆ品番</returns>
        Public Property FBuhinNo() As String
            Get
                Return _FBuhinNo
            End Get
            Set(ByVal value As String)
                _FBuhinNo = value
            End Set
        End Property

        ''' <summary>仕様差品番改訂No.</summary>
        ''' <value>仕様差品番改訂No.</value>
        ''' <returns>仕様差品番改訂No.</returns>
        Public Property ShiyosaKaiteiNo() As String
            Get
                Return _ShiyosaKaiteiNo
            End Get
            Set(ByVal value As String)
                _ShiyosaKaiteiNo = value
            End Set
        End Property

        ''' <summary>部位コード</summary>
        ''' <value>部位コード</value>
        ''' <returns>部位コード</returns>
        Public Property BuiCode() As String
            Get
                Return _BuiCode
            End Get
            Set(ByVal value As String)
                _BuiCode = value
            End Set
        End Property

        ''' <summary>装備仕様差組合せコード</summary>
        ''' <value>装備仕様差組合せコード</value>
        ''' <returns>装備仕様差組合せコード</returns>
        Public Property KumiawaseCodeSo() As Nullable(of Int32)
            Get
                Return _KumiawaseCodeSo
            End Get
            Set(ByVal value As Nullable(of Int32))
                _KumiawaseCodeSo = value
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
