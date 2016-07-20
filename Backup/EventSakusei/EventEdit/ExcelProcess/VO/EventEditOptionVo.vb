Namespace EventEdit.Vo
    ''' <summary>
    ''' 基本装備と特別装備
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EventEditOptionVo
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
        ''' Column順番
        ''' </summary>
        ''' <remarks></remarks>
        Private _ColumnNo As Integer
        ''' <summary>
        ''' Row順番
        ''' </summary>
        ''' <remarks></remarks>
        Private _RowNo As Integer
        ''' <summary>
        ''' 試作適用
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuTekiyou As String

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

        ''' <summary>Column順番</summary>
        ''' <value>Column順番</value>
        ''' <returns>Column順番</returns>
        Public Property ColumnNo() As Integer
            Get
                Return _ColumnNo
            End Get
            Set(ByVal value As Integer)
                _ColumnNo = value
            End Set
        End Property

        ''' <summary>Row順番</summary>
        ''' <value>Row順番</value>
        ''' <returns>Row順番</returns>
        Public Property RowNo() As Integer
            Get
                Return _RowNo
            End Get
            Set(ByVal value As Integer)
                _RowNo = value
            End Set
        End Property

        ''' <summary>試作適用</summary>
        ''' <value>試作適用</value>
        ''' <returns>試作適用</returns>
        Public Property ShisakuTekiyou() As String
            Get
                Return _ShisakuTekiyou
            End Get
            Set(ByVal value As String)
                _ShisakuTekiyou = value
            End Set
        End Property
    End Class
End Namespace

