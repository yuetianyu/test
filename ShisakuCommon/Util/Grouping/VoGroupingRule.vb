Namespace Util.Grouping
    Public Interface VoGroupingRule(Of T)
        Sub GroupRule(ByVal groupBy As VoGroupingLocator, ByVal vo As T)
    End Interface
End Namespace