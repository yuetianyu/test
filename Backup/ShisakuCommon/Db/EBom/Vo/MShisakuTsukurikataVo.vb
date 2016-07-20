Namespace Db.EBom.Vo
    ''' <summary>作り方項目マスタ</summary>
    Public Class MShisakuTsukurikataVo

        ''' <summary>作り方区分</summary>
        Private _TsukurikataKbn As String

        ''' <summary>作り方項目No</summary>
        Private _TsukurikataNo As Nullable(Of Int32)

        ''' <summary>作り方項目名称</summary>
        Private _TsukutikataName As String

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


        ''' <summary>作り方区分</summary>
        ''' <value>作り方区分</value>
        ''' <returns>作り方区分</returns>
        Public Property TsukurikataKbn() As String
            Get
                Return _TsukurikataKbn
            End Get
            Set(ByVal value As String)
                _TsukurikataKbn = value
            End Set
        End Property

        ''' <summary>作り方項目No</summary>
        ''' <value>作り方項目No</value>
        ''' <returns>作り方項目No</returns>
        Public Property TsukurikataNo() As Nullable(Of Int32)
            Get
                Return _TsukurikataNo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _TsukurikataNo = value
            End Set
        End Property

        ''' <summary>作り方項目名称</summary>
        ''' <value>作り方項目名称</value>
        ''' <returns>作り方項目名称</returns>
        Public Property TsukutikataName() As String
            Get
                Return _TsukutikataName
            End Get
            Set(ByVal value As String)
                _TsukutikataName = value
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