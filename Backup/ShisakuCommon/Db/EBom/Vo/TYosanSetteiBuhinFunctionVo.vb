Namespace Db.EBom.Vo

    ''' <summary>
    ''' 予算設定ファンクションマスタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TYosanSetteiBuhinFunctionVo

        ''' <summary>予算ファンクション品番</summary>
        Private _YosanFunctionHinban As String
        ''' <summary>予算取引先コード</summary>
        Private _YosanMakerCode As String
        ''' <summary>予算単価</summary>
        Private _YosanTanka As Nullable(Of Decimal)
        ''' <summary>予算備考</summary>
        Private _YosanBiko As String
        ''' <summary>作成ユーザーID</summary>
        Private _CreatedUserId As String
        ''' <summary>作成年月日</summary>
        Private _CreatedDate As String
        ''' <summary>作成時分秒</summary>
        Private _CreatedTime As String
        ''' <summary>更新ユーザーID</summary>
        Private _UpdatedUserId As String
        ''' <summary>更新年月日</summary>
        Private _UpdatedDate As String
        ''' <summary>更新時分秒</summary>
        Private _UpdatedTime As String


        ''' <summary>
        ''' 予算ファンクション品番
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property YosanFunctionHinban() As String
            Get
                Return _YosanFunctionHinban
            End Get
            Set(ByVal value As String)
                _YosanFunctionHinban = value
            End Set
        End Property

        ''' <summary>
        ''' 予算取引先コード
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property YosanMakerCode() As String
            Get
                Return _YosanMakerCode
            End Get
            Set(ByVal value As String)
                _YosanMakerCode = value
            End Set
        End Property

        ''' <summary>
        ''' 予算単価
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property YosanTanka() As Nullable(Of Decimal)
            Get
                Return _YosanTanka
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanTanka = value
            End Set
        End Property

        ''' <summary>
        ''' 予算備考
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property YosanBiko() As String
            Get
                Return _YosanBiko
            End Get
            Set(ByVal value As String)
                _YosanBiko = value
            End Set
        End Property



        ''' <summary>
        ''' 作成ユーザーID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CreatedUserId() As String
            Get
                Return _CreatedUserId
            End Get
            Set(ByVal value As String)
                _CreatedUserId = value
            End Set
        End Property

        ''' <summary>
        ''' 作成年月日
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CreatedDate() As String
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As String)
                _CreatedDate = value
            End Set
        End Property

        ''' <summary>
        ''' 作成時分秒
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CreatedTime() As String
            Get
                Return _CreatedTime
            End Get
            Set(ByVal value As String)
                _CreatedTime = value
            End Set
        End Property

        ''' <summary>
        ''' 更新ユーザーID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property UpdatedUserId() As String
            Get
                Return _UpdatedUserId
            End Get
            Set(ByVal value As String)
                _UpdatedUserId = value
            End Set
        End Property

        ''' <summary>
        ''' 更新年月日
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property UpdatedDate() As String
            Get
                Return _UpdatedDate
            End Get
            Set(ByVal value As String)
                _UpdatedDate = value
            End Set
        End Property


        ''' <summary>
        ''' 更新時分秒
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
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
