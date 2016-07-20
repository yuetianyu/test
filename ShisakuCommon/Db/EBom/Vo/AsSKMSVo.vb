Namespace Db.EBom.Vo
    ''' <summary>
    ''' 製品区分マスター情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AsSKMSVo
        '' B/U区分  
        Private _BUKbn As String
        '' 製品区分  
        Private _Snkm As String
        '' 製品区分名称  
        Private _SnkmNM As String

        ''' <summary>B/U区分</summary>
        ''' <value>B/U区分</value>
        ''' <returns>B/U区分</returns>
        Public Property BUKbn() As String
            Get
                Return _BUKbn
            End Get
            Set(ByVal value As String)
                _BUKbn = value
            End Set
        End Property

        ''' <summary>製品区分</summary>
        ''' <value>製品区分</value>
        ''' <returns>製品区分</returns>
        Public Property Snkm() As String
            Get
                Return _Snkm
            End Get
            Set(ByVal value As String)
                _Snkm = value
            End Set
        End Property

        ''' <summary>製品区分名称</summary>
        ''' <value>製品区分名称</value>
        ''' <returns>製品区分名称</returns>
        Public Property SnkmNM() As String
            Get
                Return _SnkmNM
            End Get
            Set(ByVal value As String)
                _SnkmNM = value
            End Set
        End Property


    End Class
End Namespace
