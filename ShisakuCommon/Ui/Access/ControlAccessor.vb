Namespace Ui.Access
    ''' <summary>
    ''' Windowsコントロールへのアクセス方法を統一するインターフェース
    ''' </summary>
    ''' <typeparam name="T">Value値の型</typeparam>
    ''' <remarks></remarks>
    Public Interface ControlAccessor(Of T)

        ''' <summary>名前</summary>
        ''' <value>名前</value>
        ''' <returns>名前</returns>
        ReadOnly Property Name() As String

        ''' <summary>値</summary>
        ''' <param name="index">値のindex</param>
        ''' <value>値</value>
        ''' <returns>値</returns>
        ReadOnly Property Value(Optional ByVal index As Integer = -1) As T

        ''' <summary>Windowsコントロール</summary>
        ''' <value>Windowsコントロール</value>
        ''' <returns>Windowsコントロール</returns>
        ReadOnly Property Control() As Control

        ''' <summary>
        ''' WindowsコントロールをErrorControlとして生成する
        ''' </summary>
        ''' <returns>新しいErrorControl</returns>
        ''' <remarks></remarks>
        Function NewErrorControl() As ErrorControl

        ''' <summary>
        ''' もう一つの ControlAccessor と値を比較して返す
        ''' </summary>
        ''' <param name="anotherAccessor">もう一つの ControlAccessor</param>
        ''' <param name="index">値のindex</param>
        ''' <returns>比較結果</returns>
        Function CompareTo(ByVal anotherAccessor As ControlAccessor(Of T), Optional ByVal index As Integer = -1) As Integer
    End Interface
End Namespace