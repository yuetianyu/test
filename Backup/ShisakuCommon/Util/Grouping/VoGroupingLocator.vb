Namespace Util.Grouping
    ''' <summary>
    ''' 項目のLocatorインターフェース
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface VoGroupingLocator
        ''' <summary>
        ''' グループ化する項目を指定する
        ''' </summary>
        ''' <param name="groupingField">グループ化する項目</param>
        ''' <returns>項目のLocatorインターフェース</returns>
        ''' <remarks></remarks>
        Function Prop(ByVal groupingField As Object) As VoGroupingLocator
    End Interface
End Namespace