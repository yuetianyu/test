Imports ShisakuCommon.Db.EBom.Vo

Namespace TehaichoSakusei.Vo
    Public Class SekkeiBlockInstlVoHelper : Inherits TShisakuSekkeiBlockInstlVo

        Private _KachouSyouninJyoutai As String

        Private _BlockFuyou As String

        ''' <summary>課長承認状態</summary>
        ''' <value>課長承認状態</value>
        ''' <returns>課長承認状態</returns>
        Public Property KachouSyouninJyoutai() As String
            Get
                Return _KachouSyouninJyoutai
            End Get
            Set(ByVal value As String)
                _KachouSyouninJyoutai = value
            End Set
        End Property

        ''' <summary>ブロック要、不要</summary>
        ''' <value>ブロック要、不要</value>
        ''' <returns>ブロック要、不要</returns>
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