Namespace Db.EBom.Vo
    ''' <summary>
    ''' 製作イベントマスター
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MSeisakuEventVo
        '' 製作イベントコード
        Private _SeisakuEventCode As String
        '' 製作イベント
        Private _ShisakuEvent As String
        '' 製作イベント名
        Private _ShisakuEventName As String
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

        ''' <summary>製作イベントコード</summary>
        ''' <value>製作イベントコード</value>
        ''' <returns>製作イベントコード</returns>
        Public Property SeisakuEventCode() As String
            Get
                Return _SeisakuEventCode
            End Get
            Set(ByVal value As String)
                _SeisakuEventCode = value
            End Set
        End Property

        ''' <summary>製作イベント</summary>
        ''' <value>製作イベント</value>
        ''' <returns>製作イベント</returns>
        Public Property ShisakuEvent() As String
            Get
                Return _ShisakuEvent
            End Get
            Set(ByVal value As String)
                _ShisakuEvent = value
            End Set
        End Property

        ''' <summary>製作イベント名</summary>
        ''' <value>製作イベント名</value>
        ''' <returns>製作イベント名</returns>
        Public Property ShisakuEventName() As String
            Get
                Return _ShisakuEventName
            End Get
            Set(ByVal value As String)
                _ShisakuEventName = value
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
