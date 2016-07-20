Namespace EventEdit.Vo
    ''' <summary>
    ''' 基本装備と特別装備のタイトル
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EventEditOptionTitleVo
        ''' <summary>
        ''' 試作種別
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuSyubetu As String
        ''' <summary>
        ''' 試作号車
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuGousya As String
        ''' <summary>
        ''' タイトルのColumn順番
        ''' </summary>
        ''' <remarks></remarks>
        Private _TitleColumnNo As Integer
        ''' <summary>
        ''' タイトル名
        ''' </summary>
        ''' <remarks></remarks>
        Private _TitleName As String
        ''' <summary>
        ''' タイトル名（大区分）
        ''' </summary>
        ''' <remarks></remarks>
        Private _TitleNameDai As String
        ''' <summary>
        ''' タイトル名（中区分）
        ''' </summary>
        ''' <remarks></remarks>
        Private _TitleNameChu As String

        ''' <summary>試作種別</summary>
        ''' <value>試作種別</value>
        ''' <returns>試作種別</returns>        
        Public Property ShisakuSyubetu() As String
            Get
                Return _ShisakuSyubetu
            End Get
            Set(ByVal value As String)
                _ShisakuSyubetu = value
            End Set
        End Property

        ''' <summary>試作号車</summary>
        ''' <value>試作号車</value>
        ''' <returns>試作号車</returns>
        Public Property ShisakuGousya() As String
            Get
                Return _ShisakuGousya
            End Get
            Set(ByVal value As String)
                _ShisakuGousya = value
            End Set
        End Property

        ''' <summary>タイトルのColumn順番</summary>
        ''' <value>タイトルのColumn順番</value>
        ''' <returns>タイトルのColumn順番</returns>
        Public Property TitleColumnNo() As Integer
            Get
                Return _TitleColumnNo
            End Get
            Set(ByVal value As Integer)
                _TitleColumnNo = value
            End Set
        End Property

        ''' <summary>タイトル名</summary>
        ''' <value>タイトル</value>
        ''' <returns>タイトル</returns>
        Public Property TitleName() As String
            Get
                Return _TitleName
            End Get
            Set(ByVal value As String)
                _TitleName = value
            End Set
        End Property

        ''' <summary>タイトル名（大区分）</summary>
        ''' <value>タイトル（大区分）</value>
        ''' <returns>タイトル（大区分）</returns>
        Public Property TitleNameDai() As String
            Get
                Return _TitleNameDai
            End Get
            Set(ByVal value As String)
                _TitleNameDai = value
            End Set
        End Property

        ''' <summary>タイトル名（中区分）</summary>
        ''' <value>タイトル（中区分）</value>
        ''' <returns>タイトル（中区分）</returns>
        Public Property TitleNameChu() As String
            Get
                Return _TitleNameChu
            End Get
            Set(ByVal value As String)
                _TitleNameChu = value
            End Set
        End Property

    End Class
End Namespace
