Namespace Util.LabelValue
    Public Interface ILabelValueExtraction
        ''' <summary>
        ''' 抽出メソッド
        ''' </summary>
        ''' <param name="aLocator">VOの Label と Value を管理するインターフェース</param>
        ''' <remarks></remarks>
        Sub Extraction(ByVal aLocator As LabelValueLocator)
    End Interface
End Namespace