Imports ShisakuCommon.Db.EBom.Vo
Namespace ShisakuBuhinEditSekkei.Dao

    Public Class Rhac1860VoHelper : Inherits Rhac1860Vo

        ''' <summary>稼働日</summary>
        Private _Kadobi As Integer
        ''' <summary>当日</summary>
        Private _Tojitu As Integer
        ''' <summary>処置期限日</summary>
        Private _SyochiKigenbi As Integer

        ''' <summary>稼働日</summary>
        ''' <value>稼働日</value>
        ''' <returns>稼働日</returns>
        Public Property Kadobi() As Integer
            Get
                Return _Kadobi
            End Get
            Set(ByVal value As Integer)
                _Kadobi = value
            End Set
        End Property

        ''' <summary>当日</summary>
        ''' <value>当日</value>
        ''' <returns>当日</returns>
        Public Property Tojitu() As Integer
            Get
                Return _Tojitu
            End Get
            Set(ByVal value As Integer)
                _Tojitu = value
            End Set
        End Property

        ''' <summary>処置期限日</summary>
        ''' <value>処置期限日</value>
        ''' <returns>処置期限日</returns>
        Public Property SyochiKigenbi() As Integer
            Get
                Return _SyochiKigenbi
            End Get
            Set(ByVal value As Integer)
                _SyochiKigenbi = value
            End Set
        End Property

    End Class

End Namespace

