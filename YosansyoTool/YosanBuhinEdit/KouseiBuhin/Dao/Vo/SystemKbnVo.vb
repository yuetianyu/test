Namespace YosanBuhinEdit.KouseiBuhin.Dao.Vo
    ''' <summary>
    ''' システム大区分Ｏｒシステム区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SystemKbnVo
        ''' <summary>区分</summary>
        Private _IdField As String
        ''' <summary>区分名</summary>
        Private _ValueField As String

        ''' <summary>
        ''' 区分
        ''' </summary>
        ''' <value>区分</value>
        ''' <returns>区分</returns>
        ''' <remarks></remarks>
        Public Property IdField() As String
            Get
                Return _IdField
            End Get
            Set(ByVal value As String)
                _IdField = value
            End Set
        End Property

        ''' <summary>
        ''' 区分名
        ''' </summary>
        ''' <value>区分名</value>
        ''' <returns>区分名</returns>
        ''' <remarks></remarks>
        Public Property ValueField() As String
            Get
                Return _ValueField
            End Get
            Set(ByVal value As String)
                _ValueField = value
            End Set
        End Property

    End Class
End Namespace


