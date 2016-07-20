Imports ShisakuCommon

Namespace EventEdit.Logic
    ''' <summary>
    ''' ベース車・完成車・基本装備・特別装備の「種別」・「号車」の同期をとる<br/>
    ''' 「種別」を集計してヘッダーの「制作台数・W/B・中止」の同期をとる
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EzSyncBaseTenkaiImpl : Implements EzSyncBaseTenkai
        ' TODO EzSyncInstlHinbanImplのInitializeのような実装にする!!
        Friend baseTenkaiCar As EventEditBaseTenkaiCar

        Private notifySync As Boolean

        Private summarySync As Boolean

        ''' <summary>
        ''' ベース車の持つ種別・号車を元に、他へ通知する
        ''' </summary>
        ''' <param name="sender">ベース者情報</param>
        ''' <remarks></remarks>
        Public Sub NotifyAllBaseTenkai(ByVal sender As EventEditBasetenkaiCar)
            If sender IsNot basetenkaiCar Then
                Throw New ArgumentException()
            End If
            summarySync = True
            Try
                For Each rowNo As Integer In basetenkaiCar.GetInputRowNos
                    NotifyShubetsu(basetenkaiCar, rowNo)
                    NotifyGosha(basetenkaiCar, rowNo)
                    NotifyBaseKaihatsuFugo(basetenkaiCar, rowNo)
                    NotifyBaseShiyoujyouhouNo(basetenkaiCar, rowNo)
                    NotifyBaseAppliedNo(basetenkaiCar, rowNo)
                    NotifyBaseKatashiki(basetenkaiCar, rowNo)
                    NotifyBaseShimuke(basetenkaiCar, rowNo)
                    NotifyBaseOp(basetenkaiCar, rowNo)
                    NotifyBaseGaisousyoku(basetenkaiCar, rowNo)
                    NotifyBaseNaisousyoku(basetenkaiCar, rowNo)
                    NotifyShisakuBaseEventCode(basetenkaiCar, rowNo)
                    NotifyShisakuBaseGousya(basetenkaiCar, rowNo)
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
        Public Sub NotifyShubetsu(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncbasetenkai.NotifyShubetsu
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                baseTenkaiCar.ShisakuSyubetu(rowNo) = sender.ShisakuSyubetu(rowNo)
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
        Public Sub NotifyGosha(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncbasetenkai.NotifyGosha
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                baseTenkaiCar.ShisakuGousya(rowNo) = sender.ShisakuGousya(rowNo)
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
        Public Sub NotifyBaseKaihatsuFugo(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncbasetenkai.NotifyBaseKaihatsuFugo
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                baseTenkaiCar.BaseKaihatsuFugo(rowNo) = sender.BaseKaihatsuFugo(rowNo)
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
        Public Sub NotifyBaseShiyoujyouhouNo(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncbasetenkai.NotifyBaseShiyoujyouhouNo
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                baseTenkaiCar.BaseShiyoujyouhouNo(rowNo) = sender.BaseShiyoujyouhouNo(rowNo)
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
        Public Sub NotifyBaseAppliedNo(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncbasetenkai.NotifyBaseAppliedNo
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                baseTenkaiCar.BaseAppliedNo(rowNo) = sender.BaseAppliedNo(rowNo)
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
        Public Sub NotifyBaseKatashiki(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncbasetenkai.NotifyBaseKatashiki
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                baseTenkaiCar.BaseKatashiki(rowNo) = sender.BaseKatashiki(rowNo)
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
        Public Sub NotifyBaseShimuke(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncbasetenkai.NotifyBaseShimuke
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                baseTenkaiCar.BaseShimuke(rowNo) = sender.BaseShimuke(rowNo)
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
        Public Sub NotifyBaseOp(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncbasetenkai.NotifyBaseOp
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                baseTenkaiCar.BaseOp(rowNo) = sender.BaseOp(rowNo)
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
        Public Sub NotifyBaseGaisousyoku(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncbasetenkai.NotifyBaseGaisousyoku
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                baseTenkaiCar.BaseGaisousyoku(rowNo) = sender.BaseGaisousyoku(rowNo)
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
        Public Sub NotifyBaseNaisousyoku(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncbasetenkai.NotifyBaseNaisousyoku
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                baseTenkaiCar.BaseNaisousyoku(rowNo) = sender.BaseNaisousyoku(rowNo)
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
        Public Sub NotifyShisakuBaseEventCode(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncbasetenkai.NotifyShisakuBaseEventCode
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                baseTenkaiCar.ShisakuBaseEventCode(rowNo) = sender.ShisakuBaseEventCode(rowNo)
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
        Public Sub NotifyShisakuBaseGousya(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncbasetenkai.NotifyShisakuBaseGousya
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                baseTenkaiCar.ShisakuBaseGousya(rowNo) = sender.ShisakuBaseGousya(rowNo)
            Finally
                notifySync = False
            End Try
        End Sub
    End Class
End Namespace
