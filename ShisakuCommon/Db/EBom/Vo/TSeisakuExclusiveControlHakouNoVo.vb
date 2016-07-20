Namespace Db.EBom.Vo
    ''' <summary>
    ''' 製作一覧排他管理発行№情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TSeisakuExclusiveControlHakouNoVo
        '' 発行No
        Private _HakouNo As String
        '' 編集ユーザーID
        Private _EditUserId As String
        '' 編集日
        Private _EditDate As String
        '' 編集時
        Private _EditTime As String
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
        '' 課略称
        Private _KaRyakuName As String

        ''' <summary>発行No</summary>
        ''' <value>発行No</value>
        ''' <returns>発行No</returns>
        Public Property HakouNo() As String
            Get
                Return _HakouNo
            End Get
            Set(ByVal value As String)
                _HakouNo = value
            End Set
        End Property

        ''' <summary>編集ユーザーID</summary>
        ''' <value>編集ユーザーID</value>
        ''' <returns>編集ユーザーID</returns>
        Public Property EditUserId() As String
            Get
                Return _EditUserId
            End Get
            Set(ByVal value As String)
                _EditUserId = value
            End Set
        End Property

        ''' <summary>編集日</summary>
        ''' <value>編集日</value>
        ''' <returns>編集日</returns>
        Public Property EditDate() As String
            Get
                Return _EditDate
            End Get
            Set(ByVal value As String)
                _EditDate = value
            End Set
        End Property

        ''' <summary>編集時</summary>
        ''' <value>編集時</value>
        ''' <returns>編集時</returns>
        Public Property EditTime() As String
            Get
                Return _EditTime
            End Get
            Set(ByVal value As String)
                _EditTime = value
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

        ''' <summary>課略称</summary>
        ''' <value>課略称</value>
        ''' <returns>課略称</returns>
        Public Property KaRyakuName() As String
            Get
                Return _KaRyakuName
            End Get
            Set(ByVal value As String)
                _KaRyakuName = value
            End Set
        End Property

    End Class
End Namespace
