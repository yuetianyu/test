
Namespace Util.OptionFilter
    ''' <summary>
    ''' ソートの条件を記録しておくクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class clsOptionFilterVo
        '列番号'
        Private _columnIndex As Integer
        '条件'
        Private _condition As String

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

        ''' <summary>列番号</summary>
        ''' <value>列番号</value>
        ''' <returns>列番号</returns>
        Public Property ColumnIndex() As Integer
            Get
                Return _columnIndex
            End Get
            Set(ByVal value As Integer)
                _columnIndex = value
            End Set
        End Property


        ''' <summary>条件</summary>
        ''' <value>条件</value>
        ''' <returns>条件</returns>
        Public Property Condition() As String
            Get
                Return _condition
            End Get
            Set(ByVal value As String)
                _condition = value
            End Set
        End Property

    End Class

End Namespace