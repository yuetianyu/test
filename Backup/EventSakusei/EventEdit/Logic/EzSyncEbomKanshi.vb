Namespace EventEdit.Logic
    ''' <summary>
    ''' 監視
    ''' </summary>
    ''' <remarks>  ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 i) (TES)施 ADD BEGIN</remarks>
    Public Interface EzSyncEbomKanshi
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
        ''' <summary>
        ''' ベース車開発符号が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Sub NotifyBaseKaihatsuFugo(ByVal sender As Object, ByVal rowNo As Integer)
        ''' <summary>
        ''' ベース車仕様情報№が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Sub NotifyBaseShiyoujyouhouNo(ByVal sender As Object, ByVal rowNo As Integer)
        ''' <summary>
        ''' ベース車アプライド№が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Sub NotifyBaseAppliedNo(ByVal sender As Object, ByVal rowNo As Integer)
        ''' <summary>
        ''' ベース車型式が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Sub NotifyBaseKatashiki(ByVal sender As Object, ByVal rowNo As Integer)
        ''' <summary>
        ''' ベース車仕向が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Sub NotifyBaseShimuke(ByVal sender As Object, ByVal rowNo As Integer)
        ''' <summary>
        ''' ベース車OPが更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Sub NotifyBaseOp(ByVal sender As Object, ByVal rowNo As Integer)
        ''' <summary>
        ''' ベース車外装色が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Sub NotifyBaseGaisousyoku(ByVal sender As Object, ByVal rowNo As Integer)
        ''' <summary>
        ''' ベース車内装色が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Sub NotifyBaseNaisousyoku(ByVal sender As Object, ByVal rowNo As Integer)
        ''' <summary>
        ''' 試作ベースイベントコードが更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Sub NotifyShisakuBaseEventCode(ByVal sender As Object, ByVal rowNo As Integer)
        ''' <summary>
        ''' 試作ベース号車が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Sub NotifyShisakuBaseGousya(ByVal sender As Object, ByVal rowNo As Integer)
        '20140820 Sakai Add
        ''' <summary>
        ''' 行が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Sub NotifyRow(ByVal sender As Object, ByVal rowNo As Integer)

    End Interface
End Namespace