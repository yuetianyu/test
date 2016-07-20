Namespace Db.EBom.Vo

    ''' <summary>
    ''' イベント別台ブロック当たり単価情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TSeisakuEventGousyaBlockPriceVo
        '発行№
        Private _HakouNo As String
        '表示順
        Private _HyojijunNo As Integer
        '試作種別
        Private _ShisakuSyubetu As String
        '号車
        Private _ShisakuGousya As String
        'ブロック№
        Private _BlockNo As String
        '比例費・トリム見通し
        Private _HireiTMitoshi As Decimal
        '比例費・メタル見通し
        Private _HireiMMitoshi As Decimal
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

        ''' <summary>
        ''' 発行№
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HakouNo() As String
            Get
                Return _HakouNo
            End Get
            Set(ByVal value As String)
                _HakouNo = value
            End Set
        End Property

        ''' <summary>
        ''' 表示順
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HyojijunNo() As Integer
            Get
                Return _HyojijunNo
            End Get
            Set(ByVal value As Integer)
                _HyojijunNo = value
            End Set
        End Property

        ''' <summary>
        ''' 試作種別
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ShisakuSyubetu() As String
            Get
                Return _ShisakuSyubetu
            End Get
            Set(ByVal value As String)
                _ShisakuSyubetu = value
            End Set
        End Property

        ''' <summary>
        ''' 号車
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ShisakuGousya() As String
            Get
                Return _ShisakuGousya
            End Get
            Set(ByVal value As String)
                _ShisakuGousya = value
            End Set
        End Property

        ''' <summary>
        ''' ブロックNo.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BlockNo() As String
            Get
                Return _BlockNo
            End Get
            Set(ByVal value As String)
                _BlockNo = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・トリム予算
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HireiTMitoshi() As Decimal
            Get
                Return _HireiTMitoshi
            End Get
            Set(ByVal value As Decimal)
                _HireiTMitoshi = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・トリム見通し
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HireiMMitoshi() As Decimal
            Get
                Return _HireiMMitoshi
            End Get
            Set(ByVal value As Decimal)
                _HireiMMitoshi = value
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
