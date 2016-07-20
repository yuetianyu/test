Namespace ShisakuBuhinEditBlock.Logic
    ''' <summary>
    ''' ベース車・完成車・基本装備・特別装備の「種別」・「号車」の同期をとる
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface EzSyncShubetsuGosha
        ''' <summary>
        ''' 種別が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Sub NotifyShubetsu(ByVal sender As Object, ByVal rowNo As Integer)
        ''' <summary>
        ''' 号車が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Sub NotifyGosha(ByVal sender As Object, ByVal rowNo As Integer)

    End Interface
End Namespace