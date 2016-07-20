Imports ShisakuCommon.Db.EBom.Vo
Namespace ShisakuBuhinEditBlock.Dao

    Public Class TShisakuBuhinEditVoHelper : Inherits TShisakuBuhinEditVo
        ''' <summary>INSTL品番</summary>
        Private _InstlHinban As String
        ''' <summary>INSTL品番区分</summary>
        Private _InstlHinbanKbn As String
        ''' <summary>INSTL品番表示順</summary>
        Private _InstlHinbanHyoujiJun As Integer
        ''' <summary>員数数量</summary>
        Private _InsuSuryo As Integer
        ''' <summary>フラグ</summary>
        Private _Flag As Boolean
        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_z) (TES)張 ADD BEGIN
        Public _BaseInstlFlg As String
        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_z) (TES)張 ADD END
        ''' <summary>ブロック不要</summary>
        Private _BlockFuyou As String

        Private _InstlDataKbn As String

        Public Property BaseInstlFlg() As String
            Get
                Return _BaseInstlFlg
            End Get
            Set(ByVal value As String)
                _BaseInstlFlg = value
            End Set
        End Property


        Public Property InstlDataKbn() As String
            Get
                Return _InstlDataKbn
            End Get
            Set(ByVal value As String)
                _InstlDataKbn = value
            End Set
        End Property

        ''' <summary>INSTL品番</summary>
        ''' <value>INSTL品番</value>
        ''' <returns>INSTL品番</returns>
        Public Property InstlHinban() As String
            Get
                Return _InstlHinban
            End Get
            Set(ByVal value As String)
                _InstlHinban = value
            End Set
        End Property

        ''' <summary>INSTL品番区分</summary>
        ''' <value>INSTL品番区分</value>
        ''' <returns>INSTL品番区分</returns>
        Public Property InstlHinbanKbn() As String
            Get
                Return _InstlHinbanKbn
            End Get
            Set(ByVal value As String)
                _InstlHinbanKbn = value
            End Set
        End Property

        ''' <summary>フロック不要</summary>
        ''' <value>フロック不要</value>
        ''' <returns>フロック不要</returns>
        Public Property BlockFuyou() As String
            Get
                Return _BlockFuyou
            End Get
            Set(ByVal value As String)
                _BlockFuyou = value
            End Set
        End Property

        ''' <summary>INSTL品番表示順</summary>
        ''' <value>INSTL品番表示順</value>
        ''' <returns>INSTL品番表示順</returns>
        Public Property InstlHinbanHyoujiJun() As Integer
            Get
                Return _InstlHinbanHyoujiJun
            End Get
            Set(ByVal value As Integer)
                _InstlHinbanHyoujiJun = value
            End Set
        End Property

        ''' <summary>員数数量</summary>
        ''' <value>員数数量</value>
        ''' <returns>員数数量</returns>
        Public Property InsuSuryo() As Integer
            Get
                Return _InsuSuryo
            End Get
            Set(ByVal value As Integer)
                _InsuSuryo = value
            End Set
        End Property


        Public Property Flag() As Boolean
            Get
                Return _Flag
            End Get
            Set(ByVal value As Boolean)
                _Flag = value
            End Set
        End Property

    End Class

End Namespace

