Namespace XVLView.Dao.Vo

    ''' <summary>
    ''' 試作号車取得VO
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ConditionSelectVo
        ''' <summary>
        ''' イベントコード
        ''' </summary>
        ''' <remarks></remarks>
        Private mShisakuEventCode As String
        ''' <summary>
        ''' 表示順
        ''' </summary>
        ''' <remarks></remarks>
        Private mHyojijunNo As String
        ''' <summary>
        ''' 試作グループ
        ''' </summary>
        ''' <remarks></remarks>
        Private mShisakuGroup As String
        ''' <summary>
        ''' 号車名.
        ''' </summary>
        ''' <remarks></remarks>
        Private mShisakuGousya As String


        Public Property ShisakuEventCode() As String
            Get
                Return mShisakuEventCode
            End Get
            Set(ByVal value As String)
                mShisakuEventCode = value
            End Set
        End Property

        Public Property HyojijunNo() As String
            Get
                Return mHyojijunNo
            End Get
            Set(ByVal value As String)
                mHyojijunNo = value
            End Set
        End Property

        Public Property ShisakuGroup() As String
            Get
                Return mShisakuGroup
            End Get
            Set(ByVal value As String)
                mShisakuGroup = value
            End Set
        End Property

        Public Property ShisakuGousya() As String
            Get
                Return mShisakuGousya
            End Get
            Set(ByVal value As String)
                mShisakuGousya = value
            End Set
        End Property

    End Class

End Namespace
