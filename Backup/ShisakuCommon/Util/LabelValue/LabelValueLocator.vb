Namespace Util.LabelValue
    ''' <summary>
    ''' VOの Label と Value を管理するインターフェース
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface LabelValueLocator
        ''' <summary>
        ''' Label と Value を設定する VO にマーキングをする
        ''' </summary>
        ''' <param name="vo">VOのインスタンス</param>
        ''' <returns>Label と Value を設定するMessengerインターフェース</returns>
        ''' <remarks></remarks>
        Function IsA(ByVal vo As Object) As LabelValueMessenger
    End Interface
End Namespace
