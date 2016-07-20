Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作システム権限マスター
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MShisakuKengenVo
        '' 試作プログラムID  
        Private _ShisakuProgramId1 As String
        '' 試作機能ID01  
        Private _ShisakuKinoId1 As String
        '' 試作機能ID02  
        Private _ShisakuKinoId2 As String
        '' 試作プログラム名  
        Private _ShisakuProgramName1 As String
        '' 試作機能名01  
        Private _ShisakuKinoName1 As String
        '' 試作機能名02  
        Private _ShisakuKinoName2 As String
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

        ''' <summary>試作プログラムID</summary>
        ''' <value>試作プログラムID</value>
        ''' <returns>試作プログラムID</returns>
        Public Property ShisakuProgramId1() As String
            Get
                Return _ShisakuProgramId1
            End Get
            Set(ByVal value As String)
                _ShisakuProgramId1 = value
            End Set
        End Property

        ''' <summary>試作機能ID01</summary>
        ''' <value>試作機能ID01</value>
        ''' <returns>試作機能ID01</returns>
        Public Property ShisakuKinoId1() As String
            Get
                Return _ShisakuKinoId1
            End Get
            Set(ByVal value As String)
                _ShisakuKinoId1 = value
            End Set
        End Property

        ''' <summary>試作機能ID02</summary>
        ''' <value>試作機能ID02</value>
        ''' <returns>試作機能ID02</returns>
        Public Property ShisakuKinoId2() As String
            Get
                Return _ShisakuKinoId2
            End Get
            Set(ByVal value As String)
                _ShisakuKinoId2 = value
            End Set
        End Property

        ''' <summary>試作プログラム名</summary>
        ''' <value>試作プログラム名</value>
        ''' <returns>試作プログラム名</returns>
        Public Property ShisakuProgramName1() As String
            Get
                Return _ShisakuProgramName1
            End Get
            Set(ByVal value As String)
                _ShisakuProgramName1 = value
            End Set
        End Property

        ''' <summary>試作機能名01</summary>
        ''' <value>試作機能名01</value>
        ''' <returns>試作機能名01</returns>
        Public Property ShisakuKinoName1() As String
            Get
                Return _ShisakuKinoName1
            End Get
            Set(ByVal value As String)
                _ShisakuKinoName1 = value
            End Set
        End Property

        ''' <summary>試作機能名02</summary>
        ''' <value>試作機能名02</value>
        ''' <returns>試作機能名02</returns>
        Public Property ShisakuKinoName2() As String
            Get
                Return _ShisakuKinoName2
            End Get
            Set(ByVal value As String)
                _ShisakuKinoName2 = value
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
