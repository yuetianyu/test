Namespace Db.EBom.Vo
    ''' <summary>特別織込アラートマスタ</summary>
    Public Class MShisakuTokubetsuOrikomiAlartVo

        ''' <summary>ブロックNo</summary>
        Private _BlockNo As String

        ''' <summary>ブロック名称</summary>
        Private _BlockName As String

        ''' <summary>大区分名</summary>
        Private _DaiKbnName As String

        ''' <summary>中区分名</summary>
        Private _ChuKbnName As String

        ''' <summary>詳細</summary>
        Private _ShoKbnName As String

        ''' <summary>作成ユーザーID</summary>
        Private _CreatedUserId As String

        ''' <summary>作成日</summary>
        Private _CreatedDate As String

        ''' <summary>作成時</summary>
        Private _CreatedTime As String

        ''' <summary>更新ユーザーID</summary>
        Private _UpdatedUserId As String

        ''' <summary>更新日</summary>
        Private _UpdatedDate As String

        ''' <summary>更新時間</summary>
        Private _UpdatedTime As String


        ''' <summary>ブロックNo</summary>
        ''' <value>ブロックNo</value>
        ''' <returns>ブロックNo</returns>
        Public Property BlockNo() As String
            Get
                Return _BlockNo
            End Get
            Set(ByVal value As String)
                _BlockNo = value
            End Set
        End Property

        ''' <summary>ブロック名称</summary>
        ''' <value>ブロック名称</value>
        ''' <returns>ブロック名称</returns>
        Public Property BlockName() As String
            Get
                Return _BlockName
            End Get
            Set(ByVal value As String)
                _BlockName = value
            End Set
        End Property

        ''' <summary>大区分名</summary>
        ''' <value>大区分名</value>
        ''' <returns>大区分名</returns>
        Public Property DaiKbnName() As String
            Get
                Return _DaiKbnName
            End Get
            Set(ByVal value As String)
                _DaiKbnName = value
            End Set
        End Property

        ''' <summary>中区分名</summary>
        ''' <value>中区分名</value>
        ''' <returns>中区分名</returns>
        Public Property ChuKbnName() As String
            Get
                Return _ChuKbnName
            End Get
            Set(ByVal value As String)
                _ChuKbnName = value
            End Set
        End Property

        ''' <summary>詳細</summary>
        ''' <value>詳細</value>
        ''' <returns>詳細</returns>
        Public Property ShoKbnName() As String
            Get
                Return _ShoKbnName
            End Get
            Set(ByVal value As String)
                _ShoKbnName = value
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