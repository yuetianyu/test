Namespace Util.LabelValue
    ''' <summary>
    ''' Label と Value を設定するインターフェース
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface LabelValueMessenger
        ''' <summary>
        ''' Labelプロパティである事を設定する
        ''' </summary>
        ''' <param name="labelProperty">Labelプロパティ</param>
        ''' <returns>Label と Value を設定するMessengerインターフェース</returns>
        ''' <remarks></remarks>
        Function Label(ByVal labelProperty As Object) As LabelValueMessenger

        ''' <summary>
        ''' Valueプロパティである事を設定する
        ''' </summary>
        ''' <param name="valueProperty">Valueプロパティ</param>
        ''' <returns>Label と Value を設定するMessengerインターフェース</returns>
        ''' <remarks></remarks>
        Function Value(ByVal valueProperty As Object) As LabelValueMessenger
    End Interface
End Namespace
