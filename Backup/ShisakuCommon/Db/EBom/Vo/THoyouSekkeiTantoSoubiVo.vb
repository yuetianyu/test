Namespace Db.EBom.Vo
    ''' <summary>
    ''' 補用設計担当装備情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class THoyouSekkeiTantoSoubiVo
        '' 補用イベントコード
        Private _HoyouEventCode As String
        '' 補用部課コード
        Private _HoyouBukaCode As String
        '' 補用担当ＫＥＹ
        Private _HoyouTantoKey As String
        '' 補用担当
        Private _HoyouTanto As String
        '' 項目№
        Private _HoyouSoubiHyoujiJun As String
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

        ''' <summary>補用イベントコード</summary>
        ''' <value>補用イベントコード</value>
        ''' <returns>補用イベントコード</returns>
        Public Property HoyouEventCode() As String
            Get
                Return _HoyouEventCode
            End Get
            Set(ByVal value As String)
                _HoyouEventCode = value
            End Set
        End Property

        ''' <summary>補用部課コード</summary>
        ''' <value>補用部課コード</value>
        ''' <returns>補用部課コード</returns>
        Public Property HoyouBukaCode() As String
            Get
                Return _HoyouBukaCode
            End Get
            Set(ByVal value As String)
                _HoyouBukaCode = value
            End Set
        End Property

        ''' <summary>補用担当ＫＥＹ</summary>
        ''' <value>補用担当ＫＥＹ</value>
        ''' <returns>補用担当ＫＥＹ</returns>
        Public Property HoyouTantoKey() As String
            Get
                Return _HoyouTantoKey
            End Get
            Set(ByVal value As String)
                _HoyouTantoKey = value
            End Set
        End Property

        ''' <summary>補用担当</summary>
        ''' <value>補用担当</value>
        ''' <returns>補用担当</returns>
        Public Property HoyouTanto() As String
            Get
                Return _HoyouTanto
            End Get
            Set(ByVal value As String)
                _HoyouTanto = value
            End Set
        End Property

        ''' <summary>項目№</summary>
        ''' <value>項目№</value>
        ''' <returns>項目№</returns>
        Public Property HoyouSoubiHyoujiJun() As String
            Get
                Return _HoyouSoubiHyoujiJun
            End Get
            Set(ByVal value As String)
                _HoyouSoubiHyoujiJun = value
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
