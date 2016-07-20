Imports ShisakuCommon

Namespace EventEdit.Logic
    ''' <summary>
    ''' ベース車・完成車・基本装備・特別装備の「種別」・「号車」の同期をとる<br/>
    ''' 「種別」を集計してヘッダーの「制作台数・W/B・中止」の同期をとる
    ''' </summary>
    ''' <remarks>  ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 j) (TES)施 ADD </remarks>
    Public Class EzSyncEbomKanshiImpl : Implements EzSyncEbomKanshi
        ' TODO EzSyncInstlHinbanImplのInitializeのような実装にする!!
        Friend EbomKanshi As EventEditEbomKanshi

        Private notifySync As Boolean

        Private summarySync As Boolean

        ''' <summary>
        ''' ベース車の持つ種別・号車を元に、他へ通知する
        ''' </summary>
        ''' <param name="sender">ベース者情報</param>
        ''' <remarks></remarks>
        Public Sub NotifyAllEbomKanshi(ByVal sender As EventEditEbomKanshi)
            If sender IsNot EbomKanshi Then
                Throw New ArgumentException()
            End If
            summarySync = True
            Try
                For Each rowNo As Integer In EbomKanshi.GetInputRowNos
                    NotifyShubetsu(EbomKanshi, rowNo)
                    NotifyGosha(EbomKanshi, rowNo)
                    NotifyBaseKaihatsuFugo(EbomKanshi, rowNo)
                    NotifyBaseShiyoujyouhouNo(EbomKanshi, rowNo)
                    NotifyBaseAppliedNo(EbomKanshi, rowNo)
                    NotifyBaseKatashiki(EbomKanshi, rowNo)
                    NotifyBaseShimuke(EbomKanshi, rowNo)
                    NotifyBaseOp(EbomKanshi, rowNo)
                    NotifyBaseGaisousyoku(EbomKanshi, rowNo)
                    NotifyBaseNaisousyoku(EbomKanshi, rowNo)
                    NotifyShisakuBaseEventCode(EbomKanshi, rowNo)
                    NotifyShisakuBaseGousya(EbomKanshi, rowNo)
                Next
            Finally
                summarySync = False
            End Try
        End Sub
        ''' <summary>
        ''' 種別が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Public Sub NotifyShubetsu(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncEbomKanshi.NotifyShubetsu
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                EbomKanshi.ShisakuSyubetu(rowNo) = sender.ShisakuSyubetu(rowNo)
            Finally
                notifySync = False
            End Try
        End Sub
        ''' <summary>
        ''' 号車が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Public Sub NotifyGosha(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncEbomKanshi.NotifyGosha
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                EbomKanshi.ShisakuGousya(rowNo) = sender.ShisakuGousya(rowNo)
            Finally
                notifySync = False
            End Try
        End Sub
        ''' <summary>
        ''' ベース車開発符号が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Public Sub NotifyBaseKaihatsuFugo(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncEbomKanshi.NotifyBaseKaihatsuFugo
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                EbomKanshi.BaseKaihatsuFugo(rowNo) = sender.BaseKaihatsuFugo(rowNo)
            Finally
                notifySync = False
            End Try
        End Sub
        ''' <summary>
        ''' ベース車仕様情報№が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Public Sub NotifyBaseShiyoujyouhouNo(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncEbomKanshi.NotifyBaseShiyoujyouhouNo
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                EbomKanshi.BaseShiyoujyouhouNo(rowNo) = sender.BaseShiyoujyouhouNo(rowNo)
            Finally
                notifySync = False
            End Try
        End Sub
        ''' <summary>
        ''' ベース車アプライド№が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Public Sub NotifyBaseAppliedNo(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncEbomKanshi.NotifyBaseAppliedNo
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                EbomKanshi.BaseAppliedNo(rowNo) = sender.BaseAppliedNo(rowNo)
            Finally
                notifySync = False
            End Try
        End Sub
        ''' <summary>
        ''' ベース車型式が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Public Sub NotifyBaseKatashiki(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncEbomKanshi.NotifyBaseKatashiki
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                EbomKanshi.BaseKatashiki(rowNo) = sender.BaseKatashiki(rowNo)
            Finally
                notifySync = False
            End Try
        End Sub
        ''' <summary>
        ''' ベース車仕向が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Public Sub NotifyBaseShimuke(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncEbomKanshi.NotifyBaseShimuke
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                EbomKanshi.BaseShimuke(rowNo) = sender.BaseShimuke(rowNo)
            Finally
                notifySync = False
            End Try
        End Sub
        ''' <summary>
        ''' ベース車OPが更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Public Sub NotifyBaseOp(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncEbomKanshi.NotifyBaseOp
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                EbomKanshi.BaseOp(rowNo) = sender.BaseOp(rowNo)
            Finally
                notifySync = False
            End Try
        End Sub
        ''' <summary>
        ''' ベース車外装色が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Public Sub NotifyBaseGaisousyoku(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncEbomKanshi.NotifyBaseGaisousyoku
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                EbomKanshi.BaseGaisousyoku(rowNo) = sender.BaseGaisousyoku(rowNo)
            Finally
                notifySync = False
            End Try
        End Sub
        ''' <summary>
        ''' ベース車内装色が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Public Sub NotifyBaseNaisousyoku(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncEbomKanshi.NotifyBaseNaisousyoku
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                EbomKanshi.BaseNaisousyoku(rowNo) = sender.BaseNaisousyoku(rowNo)
            Finally
                notifySync = False
            End Try
        End Sub
        ''' <summary>
        ''' 試作ベースイベントコードが更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Public Sub NotifyShisakuBaseEventCode(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncEbomKanshi.NotifyShisakuBaseEventCode
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                EbomKanshi.ShisakuBaseEventCode(rowNo) = sender.ShisakuBaseEventCode(rowNo)
            Finally
                notifySync = False
            End Try
        End Sub
        ''' <summary>
        ''' 試作ベース号車が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Public Sub NotifyShisakuBaseGousya(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncEbomKanshi.NotifyShisakuBaseGousya
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                EbomKanshi.ShisakuBaseGousya(rowNo) = sender.ShisakuBaseGousya(rowNo)
            Finally
                notifySync = False
            End Try
        End Sub
        '20140820 Sakai Add
        ''' <summary>
        ''' 行が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Public Sub NotifyRow(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncEbomKanshi.NotifyRow
            NotifyBaseKaihatsuFugo(sender, rowNo)
            NotifyBaseShiyoujyouhouNo(sender, rowNo)
            NotifyBaseAppliedNo(sender, rowNo)
            NotifyBaseKatashiki(sender, rowNo)
            NotifyBaseShimuke(sender, rowNo)
            NotifyBaseOp(sender, rowNo)
            NotifyBaseGaisousyoku(sender, rowNo)
            NotifyBaseNaisousyoku(sender, rowNo)
        End Sub
    End Class
End Namespace
