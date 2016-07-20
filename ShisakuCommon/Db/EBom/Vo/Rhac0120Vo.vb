Namespace Db.EBom.Vo
    ''' <summary>
    ''' 仕様・装備項目(承認済み)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac0120Vo
        '' 仕様・装備項目コード 
        Private _ShiyosobiCode As String
        '' 仕様・装備項目コード(親) 
        Private _ShiyosobiCodeOya As String
        '' 仕様・装備項目名称 
        Private _ShiyosobiName As String
        '' 仕様・装備項目名称(英語) 
        Private _ShiyosobiEgName As String
        '' メモ 
        Private _Memo As String
        '' 変更不可フラグ 
        Private _LockFlg As String
        '' 廃止フラグ 
        Private _AbolishFlg As String
        '' 表示順 
        Private _DisplayOrder As Nullable(of Int32)
        '' 必要フラグ 
        Private _NecessaryFlg As String
        '' 制御区分 
        Private _ControlType As String
        '' アライアンスフラグ 
        Private _AlianceFlg As String
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

        ''' <summary>仕様・装備項目コード</summary>
        ''' <value>仕様・装備項目コード</value>
        ''' <returns>仕様・装備項目コード</returns>
        Public Property ShiyosobiCode() As String
            Get
                Return _ShiyosobiCode
            End Get
            Set(ByVal value As String)
                _ShiyosobiCode = value
            End Set
        End Property

        ''' <summary>仕様・装備項目コード(親)</summary>
        ''' <value>仕様・装備項目コード(親)</value>
        ''' <returns>仕様・装備項目コード(親)</returns>
        Public Property ShiyosobiCodeOya() As String
            Get
                Return _ShiyosobiCodeOya
            End Get
            Set(ByVal value As String)
                _ShiyosobiCodeOya = value
            End Set
        End Property

        ''' <summary>仕様・装備項目名称</summary>
        ''' <value>仕様・装備項目名称</value>
        ''' <returns>仕様・装備項目名称</returns>
        Public Property ShiyosobiName() As String
            Get
                Return _ShiyosobiName
            End Get
            Set(ByVal value As String)
                _ShiyosobiName = value
            End Set
        End Property

        ''' <summary>仕様・装備項目名称(英語)</summary>
        ''' <value>仕様・装備項目名称(英語)</value>
        ''' <returns>仕様・装備項目名称(英語)</returns>
        Public Property ShiyosobiEgName() As String
            Get
                Return _ShiyosobiEgName
            End Get
            Set(ByVal value As String)
                _ShiyosobiEgName = value
            End Set
        End Property

        ''' <summary>メモ</summary>
        ''' <value>メモ</value>
        ''' <returns>メモ</returns>
        Public Property Memo() As String
            Get
                Return _Memo
            End Get
            Set(ByVal value As String)
                _Memo = value
            End Set
        End Property

        ''' <summary>変更不可フラグ</summary>
        ''' <value>変更不可フラグ</value>
        ''' <returns>変更不可フラグ</returns>
        Public Property LockFlg() As String
            Get
                Return _LockFlg
            End Get
            Set(ByVal value As String)
                _LockFlg = value
            End Set
        End Property

        ''' <summary>廃止フラグ</summary>
        ''' <value>廃止フラグ</value>
        ''' <returns>廃止フラグ</returns>
        Public Property AbolishFlg() As String
            Get
                Return _AbolishFlg
            End Get
            Set(ByVal value As String)
                _AbolishFlg = value
            End Set
        End Property

        ''' <summary>表示順</summary>
        ''' <value>表示順</value>
        ''' <returns>表示順</returns>
        Public Property DisplayOrder() As Nullable(of Int32)
            Get
                Return _DisplayOrder
            End Get
            Set(ByVal value As Nullable(of Int32))
                _DisplayOrder = value
            End Set
        End Property

        ''' <summary>必要フラグ</summary>
        ''' <value>必要フラグ</value>
        ''' <returns>必要フラグ</returns>
        Public Property NecessaryFlg() As String
            Get
                Return _NecessaryFlg
            End Get
            Set(ByVal value As String)
                _NecessaryFlg = value
            End Set
        End Property

        ''' <summary>制御区分</summary>
        ''' <value>制御区分</value>
        ''' <returns>制御区分</returns>
        Public Property ControlType() As String
            Get
                Return _ControlType
            End Get
            Set(ByVal value As String)
                _ControlType = value
            End Set
        End Property

        ''' <summary>アライアンスフラグ</summary>
        ''' <value>アライアンスフラグ</value>
        ''' <returns>アライアンスフラグ</returns>
        Public Property AlianceFlg() As String
            Get
                Return _AlianceFlg
            End Get
            Set(ByVal value As String)
                _AlianceFlg = value
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
