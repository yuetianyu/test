Namespace ShisakuBuhinMenu.Logic.Impl.Tenkai
    Public Class BukaCodeBlockNoVo
        '' 部課コード
        Private _BukaCode As String
        '' ブロック№
        Private _BlockNo As String
        '' 号車
        Private _gosha As String
        '' 付加f品番
        Private _ffBuhinNo As String
        '' INSTL品番区分
        Private _instlHinbanKbn As String


        ''↓↓2014/08/13 1 ベース部品表作成表機能増強_Ⅱ④ (4)) (ダニエル上海)柳沼 ADD BEGIN
        '' INSTL元データ区分
        Private _instlDataKbn As String
        ''↑↑2014/08/13 1 ベース部品表作成表機能増強_Ⅱ④ (4)) (ダニエル上海)柳沼 ADD END


        '' 基本F品番
        Private _bfBuhinNo As String
        '' 表示順
        Private _hyoujiJun As Nullable(Of Int32)
        '' ユニット区分
        Private _UnitKbn As String

        Private _baseHyoujiJun As Nullable(Of Int32)

        ''' <summary>部課コード</summary>
        ''' <value>部課コード</value>
        ''' <returns>部課コード</returns>
        Public Property BukaCode() As String
            Get
                Return _BukaCode
            End Get
            Set(ByVal value As String)
                _BukaCode = value
            End Set
        End Property

        ''' <summary>ブロック№</summary>
        ''' <value>ブロック№</value>
        ''' <returns>ブロック№</returns>
        Public Property BlockNo() As String
            Get
                Return _BlockNo
            End Get
            Set(ByVal value As String)
                _BlockNo = value
            End Set
        End Property

        ''' <summary>号車</summary>
        ''' <value>号車</value>
        ''' <returns>号車</returns>
        Public Property Gosha() As String
            Get
                Return _gosha
            End Get
            Set(ByVal value As String)
                _gosha = value
            End Set
        End Property

        ''' <summary>付加f品番</summary>
        ''' <value>付加f品番</value>
        ''' <returns>付加f品番</returns>
        Public Property FfBuhinNo() As String
            Get
                Return _ffBuhinNo
            End Get
            Set(ByVal value As String)
                _ffBuhinNo = value
            End Set
        End Property

        ''' <summary>INSTL品番区分</summary>
        ''' <value>INSTL品番区分</value>
        ''' <returns>INSTL品番区分</returns>
        Public Property InstlHinbanKbn() As String
            Get
                Return _instlHinbanKbn
            End Get
            Set(ByVal value As String)
                _instlHinbanKbn = value
            End Set
        End Property

        ''' <summary>INSTL元データ区分</summary>
        ''' <value>INSTL元データ区分</value>
        ''' <returns>INSTL元データ区分</returns>
        Public Property InstlDataKbn() As String
            Get
                Return _instlDataKbn
            End Get
            Set(ByVal value As String)
                _instlDataKbn = value
            End Set
        End Property

        ''' <summary>基本F品番</summary>
        ''' <value>基本F品番</value>
        ''' <returns>基本F品番</returns>
        Public Property BfBuhinNo() As String
            Get
                Return _BfBuhinNo
            End Get
            Set(ByVal value As String)
                _BfBuhinNo = value
            End Set
        End Property

        ''' <summary>表示順</summary>
        ''' <value>表示順</value>
        ''' <returns>表示順</returns>
        Public Property HyoujiJun() As Nullable(Of Int32)
            Get
                Return _hyoujiJun
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _hyoujiJun = value
            End Set
        End Property

        ''' <summary>表示順</summary>
        ''' <value>表示順</value>
        ''' <returns>表示順</returns>
        Public Property BaseHyoujiJun() As Nullable(Of Int32)
            Get
                Return _baseHyoujiJun
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _baseHyoujiJun = value
            End Set
        End Property

        ''' <summary>ユニット区分</summary>
        ''' <value>ユニット区分</value>
        ''' <returns>ユニット区分</returns>
        Public Property UnitKbn() As String
            Get
                Return _UnitKbn
            End Get
            Set(ByVal value As String)
                _UnitKbn = value
            End Set
        End Property

    End Class
End Namespace