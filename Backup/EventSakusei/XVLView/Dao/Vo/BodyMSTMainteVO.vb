
Namespace XVLView.Dao.Vo
    ''' <summary>
    ''' ボディマスタ表示用VO
    ''' </summary>
    ''' <remarks>
    ''' ShisakuBuhinMSTMaintenance.BodyMSTMainte.Dao.Voに定義を置いているが
    ''' 共通プロジェクトに移してもOK
    ''' </remarks>
    Public Class BodyMSTMainteVO

        ''' <summary>ボディ名</summary>
        Private mBodyName As String
        ''' <summary>開発符号</summary>
        Private mKaihatuFugo As String
        ''' <summary>部品番号</summary>
        Private mPartsNo As String


        ''' <summary>ボディ名''' </summary>
        Public Property BodyName() As String
            Get
                Return mBodyName
            End Get
            Set(ByVal value As String)
                mBodyName = value
            End Set
        End Property

        ''' <summary>開発符号</summary>
        Public Property KaihatsuFugo() As String
            Get
                Return mKaihatuFugo
            End Get
            Set(ByVal value As String)
                mKaihatuFugo = value
            End Set
        End Property

        ''' <summary>部品番号</summary>
        Public Property BuhinNo() As String
            Get
                Return mPartsNo
            End Get
            Set(ByVal value As String)
                mPartsNo = value
            End Set
        End Property

#Region "共通項目"
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
#End Region



    End Class
End Namespace


