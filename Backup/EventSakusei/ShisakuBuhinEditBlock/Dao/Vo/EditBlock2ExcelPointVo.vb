Namespace ShisakuBuhinEditBlock.Dao

    Public Class EditBlock2ExcelPointVo

        '' 行Start
        Private _RowStart As Nullable(Of Int32)
        '' 行End
        Private _RowEnd As Nullable(Of Int32)
        '' 列Start
        Private _ColStart As Nullable(Of Int32)
        '' 列 End
        Private _ColEnd As Nullable(Of Int32)

        ''' <summary>行Start</summary>
        ''' <value>行Start</value>
        ''' <returns>行Start</returns>
        Public Property RowStart() As Nullable(Of Int32)
            Get
                Return _RowStart
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _RowStart = value
            End Set
        End Property
        ''' <summary>行End</summary>
        ''' <value>行End</value>
        ''' <returns>行End</returns>
        Public Property RowEnd() As Nullable(Of Int32)
            Get
                Return _RowEnd
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _RowEnd = value
            End Set
        End Property
        ''' <summary>列Start</summary>
        ''' <value>列Start</value>
        ''' <returns>列Start</returns>
        Public Property ColStart() As Nullable(Of Int32)
            Get
                Return _ColStart
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ColStart = value
            End Set
        End Property
        ''' <summary>列 End</summary>
        ''' <value>列 End</value>
        ''' <returns>列 End</returns>
        Public Property ColEnd() As Nullable(Of Int32)
            Get
                Return _ColEnd
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ColEnd = value
            End Set
        End Property

    End Class

End Namespace
