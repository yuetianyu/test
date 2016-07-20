Imports ShisakuCommon.Db.EBom.Vo

Namespace TehaichoMenu.Vo
    Public Class TShisakuTehaiKaiteiBlockVoHelper : Inherits TShisakuTehaiKaiteiBlockVo
        Private _BlockFuyou As String

        ''' <summary>ブロック不要</summary>
        ''' <value>ブロック不要</value>
        ''' <returns>ブロック不要</returns>
        Public Property BlockFuyou() As String
            Get
                Return _BlockFuyou
            End Get
            Set(ByVal value As String)
                _BlockFuyou = value
            End Set
        End Property


    End Class
End Namespace