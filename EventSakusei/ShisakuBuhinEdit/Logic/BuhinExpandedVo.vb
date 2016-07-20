Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Logic
    Public Class BuhinExpandedVo : Inherits TShisakuBuhinKouseiVo
        ' F品番
        Private _fHinban As String
        ' 子の員数計
        Private _InsuSummary As Nullable(Of Int32)
        ' 子のレベル（0始まり）
        Private _Level As Nullable(Of Int32)

        ''' <summary>F品番</summary>
        ''' <value>F品番</value>
        ''' <returns>F品番</returns>
        Public Property FHinban() As String
            Get
                Return _fHinban
            End Get
            Set(ByVal value As String)
                _fHinban = value
            End Set
        End Property

        ''' <summary>子の員数計</summary>
        ''' <value>子の員数計</value>
        ''' <returns>子の員数計</returns>
        Public Property InsuSummary() As Nullable(Of Int32)
            Get
                Return _InsuSummary
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _InsuSummary = value
            End Set
        End Property

        ''' <summary>子のレベル（0始まり）</summary>
        ''' <value>子のレベル（0始まり）</value>
        ''' <returns>子のレベル（0始まり）</returns>
        Public Property Level() As Nullable(Of Int32)
            Get
                Return _Level
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Level = value
            End Set
        End Property
    End Class
End Namespace
