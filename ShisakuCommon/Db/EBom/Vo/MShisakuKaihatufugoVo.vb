Namespace Db.EBom.Vo
    ''' <summary>
    ''' 車系／開発符号マスター
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MShisakuKaihatufugoVo
        '' 試作車系 
        Private _ShisakuShakeiCode As String
        '' 試作開発符号 
        Private _ShisakuKaihatsuFugo As String
        '' 表示順 
        Private _HyojijunNo As Nullable(of Int32)
        '' 試作イベントフェーズ 
        Private _ShisakuEventPhase As String
        '' 試作イベントフェーズ名 
        Private _ShisakuEventPhaseName As String
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

        ''' <summary>試作車系</summary>
        ''' <value>試作車系</value>
        ''' <returns>試作車系</returns>
        Public Property ShisakuShakeiCode() As String
            Get
                Return _ShisakuShakeiCode
            End Get
            Set(ByVal value As String)
                _ShisakuShakeiCode = value
            End Set
        End Property

        ''' <summary>試作開発符号</summary>
        ''' <value>試作開発符号</value>
        ''' <returns>試作開発符号</returns>
        Public Property ShisakuKaihatsuFugo() As String
            Get
                Return _ShisakuKaihatsuFugo
            End Get
            Set(ByVal value As String)
                _ShisakuKaihatsuFugo = value
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

        ''' <summary>試作イベントフェーズ</summary>
        ''' <value>試作イベントフェーズ</value>
        ''' <returns>試作イベントフェーズ</returns>
        Public Property ShisakuEventPhase() As String
            Get
                Return _ShisakuEventPhase
            End Get
            Set(ByVal value As String)
                _ShisakuEventPhase = value
            End Set
        End Property

        ''' <summary>試作イベントフェーズ名</summary>
        ''' <value>試作イベントフェーズ名</value>
        ''' <returns>試作イベントフェーズ名</returns>
        Public Property ShisakuEventPhaseName() As String
            Get
                Return _ShisakuEventPhaseName
            End Get
            Set(ByVal value As String)
                _ShisakuEventPhaseName = value
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
