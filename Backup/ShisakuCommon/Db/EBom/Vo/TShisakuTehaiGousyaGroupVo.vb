Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作手配帳情報（号車グループ情報）
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TShisakuTehaiGousyaGroupVo

        ''' <summary>
        ''' 試作イベントコード
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuEventCode As String
        Public Property ShisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
            End Set
        End Property

        ''' <summary>
        ''' 試作リストコード
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuListCode As String
        Public Property ShisakuListCode() As String
            Get
                Return _ShisakuListCode
            End Get
            Set(ByVal value As String)
                _ShisakuListCode = value
            End Set
        End Property

        ''' <summary>
        ''' 試作リストコード改訂№
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuListCodeKaiteiNo As String
        Public Property ShisakuListCodeKaiteiNo() As String
            Get
                Return _ShisakuListCodeKaiteiNo
            End Get
            Set(ByVal value As String)
                _ShisakuListCodeKaiteiNo = value
            End Set
        End Property

        ''' <summary>
        ''' 試作号車
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuGousya As String
        Public Property ShisakuGousya() As String
            Get
                Return _ShisakuGousya
            End Get
            Set(ByVal value As String)
                _ShisakuGousya = value
            End Set
        End Property

        ''' <summary>
        ''' 試作号車グループ
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuGousyaGroup As String
        Public Property ShisakuGousyaGroup() As String
            Get
                Return _ShisakuGousyaGroup
            End Get
            Set(ByVal value As String)
                _ShisakuGousyaGroup = value
            End Set
        End Property

        ''' <summary>
        ''' 作成ユーザーID
        ''' </summary>
        ''' <remarks></remarks>
        Private _CreatedUserId As String
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
        ''' <remarks></remarks>
        Private _CreatedDate As String
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
        ''' <remarks></remarks>
        Private _CreatedTime As String
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
        ''' <remarks></remarks>
        Private _UpdatedUserId As String
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
        ''' <remarks></remarks>
        Private _UpdatedDate As String
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
        ''' <remarks></remarks>
        Private _UpdatedTime As String
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