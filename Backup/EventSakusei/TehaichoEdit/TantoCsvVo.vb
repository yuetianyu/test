
Namespace TehaichoEdit
    '担当者CSV読込用のVOクラス'
    Public Class TantoCsvVo
        ''職番
        Private _No As String
        ''TEL
        Private _Tel As String
        ''氏名
        Private _Name As String

        ''' <summary>職番</summary>
        ''' <value>職番</value>
        ''' <returns>職番</returns>
        Public Property No() As String
            Get
                Return _No
            End Get
            Set(ByVal value As String)
                _No = value
            End Set
        End Property

        ''' <summary>TEL</summary>
        ''' <value>TEL</value>
        ''' <returns>TEL</returns>
        Public Property Tel() As String
            Get
                Return _Tel
            End Get
            Set(ByVal value As String)
                _Tel = value
            End Set
        End Property

        ''' <summary>氏名</summary>
        ''' <value>氏名</value>
        ''' <returns>氏名</returns>
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property

    End Class

End Namespace