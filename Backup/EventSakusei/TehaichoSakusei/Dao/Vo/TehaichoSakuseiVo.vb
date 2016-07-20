
Namespace TehaichoSakusei.Dao
    Public Class TehaichoSakuseiVo

        ''' <summary>グループNo</summary>
        Private _GroupNo As String
        ''' <summary>工事指令No</summary>
        Private _KoujiShireiNo As String
        ''' <summary>イベント名称</summary>
        Private _EventName As String

        ''' <summary>グループNo</summary>
        ''' <value>グループNo</value>
        ''' <returns>グループNo</returns>
        Public Property GroupNo() As String
            Get
                Return _GroupNo
            End Get
            Set(ByVal value As String)
                _GroupNo = value
            End Set
        End Property


        ''' <summary>工事指令No</summary>
        ''' <value>工事指令No</value>
        ''' <returns>工事指令No</returns>
        Public Property KoujiShireiNo() As String
            Get
                Return _KoujiShireiNo
            End Get
            Set(ByVal value As String)
                _KoujiShireiNo = value
            End Set
        End Property

        ''' <summary>イベント名称</summary>
        ''' <value>イベント名称</value>
        ''' <returns>イベント名称</returns>
        Public Property EventName() As String
            Get
                Return _EventName
            End Get
            Set(ByVal value As String)
                _EventName = value
            End Set
        End Property



    End Class
End Namespace