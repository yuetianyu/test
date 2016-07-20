Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Dao
    Public Class BuhinKouseiResultVo : Inherits Rhac0552Vo
        '' 試作部課コード
        Private _ShisakuBukaCode As String
        '' 試作ブロック№
        Private _ShisakuBlockNo As String
        '' 試作号車
        Private _ShisakuGousya As String
        ''INSTL品番
        Private _InstlHinban As String
        ''INSTL品番試作区分
        Private _InstlHinbanKbn As String

        ''' <summary>試作部課コード</summary>
        ''' <value>試作部課コード</value>
        ''' <returns>試作部課コード</returns>
        Public Property ShisakuBukaCode() As String
            Get
                Return _ShisakuBukaCode
            End Get
            Set(ByVal value As String)
                _ShisakuBukaCode = value
            End Set
        End Property

        ''' <summary>試作ブロック№</summary>
        ''' <value>試作ブロック№</value>
        ''' <returns>試作ブロック№</returns>
        Public Property ShisakuBlockNo() As String
            Get
                Return _ShisakuBlockNo
            End Get
            Set(ByVal value As String)
                _ShisakuBlockNo = value
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

        ''' <summary>INSTL品番試作区分</summary>
        ''' <value>INSTL品番試作区分</value>
        ''' <returns>INSTL品番試作区分</returns>
        Public Property InstlHinbanKbn() As String
            Get
                Return _InstlHinbanKbn
            End Get
            Set(ByVal value As String)
                _InstlHinbanKbn = value
            End Set
        End Property
    End Class
End Namespace