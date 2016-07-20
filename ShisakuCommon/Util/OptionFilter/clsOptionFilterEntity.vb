
Namespace Util.OptionFilter

    ''' <summary>
    ''' オプションフィルタエンティティー
    ''' </summary>
    ''' <remarks></remarks>
    Public Class OptionFilterEntity

#Region " メンバー変数 "
        ''' <summary>検索値</summary>
        Private m_value(MAX_FILTER_CONDITION) As String

        ''' <summary>フィルタ条件</summary>
        Private m_condition(MAX_FILTER_CONDITION) As FILTER_CONDITION

        ''' <summary>論理演算</summary>
        Private m_andOr(MAX_FILTER_CONDITION) As String
#End Region

#Region " コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            For idx As Integer = 0 To MAX_FILTER_CONDITION - 1
                m_value(idx) = String.Empty
                m_condition(idx) = FILTER_CONDITION.NONE
                m_andOr(idx) = String.Empty
            Next
        End Sub
#End Region

#Region " フィルタ条件存在チェック "
        ''' <summary>
        ''' フィルタ条件存在チェック
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsData() As Boolean
            For idx As Integer = 0 To MAX_FILTER_CONDITION - 1
                If m_value(idx) <> "" Then
                    Return True
                End If
            Next

            Return False
        End Function
#End Region

#Region " 検索値 "
        ''' <summary>
        ''' 検索値
        ''' </summary>
        ''' <param name="index"></param>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Value(ByVal index As Integer) As String
            Get
                Return m_value(index)
            End Get
            Set(ByVal value As String)
                m_value(index) = value
            End Set
        End Property
#End Region

#Region " フィルタ条件 "
        ''' <summary>
        ''' フィルタ条件
        ''' </summary>
        ''' <param name="index"></param>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Condition(ByVal index As Integer) As FILTER_CONDITION
            Get
                Return m_condition(index)
            End Get
            Set(ByVal value As FILTER_CONDITION)
                m_condition(index) = value
            End Set
        End Property
#End Region

#Region " 論理演算 "
        ''' <summary>
        ''' 論理演算
        ''' </summary>
        ''' <param name="index"></param>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AndOr(ByVal index As Integer) As String
            Get
                Return m_andOr(index)
            End Get
            Set(ByVal value As String)
                m_andOr(index) = value
            End Set
        End Property
#End Region

    End Class

End Namespace

