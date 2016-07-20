Imports ShisakuCommon

Namespace EventEdit.Logic
    ''' <summary>
    ''' ベース車・完成車・基本装備・特別装備の「種別」・「号車」の同期をとる<br/>
    ''' 「種別」を集計してヘッダーの「制作台数・W/B・中止」の同期をとる
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EzSyncShubetsuGoshaImpl : Implements EzSyncShubetsuGosha
        ' TODO EzSyncInstlHinbanImplのInitializeのような実装にする!!
        Friend header As EventEditHeader
        Friend baseCar As EventEditBaseCar
        Friend completeCar As EventEditCompleteCar
        Friend basicOption As EventEditOption
        Friend specialOption As EventEditOption
        Friend referenceCar As EventEditReferenceCar
        Friend baseTenkaiCar As EventEditBaseTenkaiCar
        Private notifySync As Boolean

        Private summarySync As Boolean

        Private Sub SummaryDaisu()
            'If summarySync Then
            '    Return
            'End If
            Dim wb As Integer
            Dim daisu As Integer
            Dim abort As Integer

            For Each rowNo As Integer In baseCar.GetInputRowNos()
                'If baseCar.IsEditModes(rowNo) Then
                ' TODO Helperを参照するように！！

                'このまま数えると削除された号車もカウントするので'

                If Not StringUtil.IsEmpty(baseCar.ShisakuGousya(rowNo)) Then
                    If "W".Equals(baseCar.ShisakuSyubetu(rowNo)) Then
                        wb += 1
                    ElseIf "D".Equals(baseCar.ShisakuSyubetu(rowNo)) Then
                        abort += 1
                    ElseIf StringUtil.IsEmpty(baseCar.ShisakuSyubetu(rowNo)) Then
                        daisu += 1
                    End If
                End If

                'End If
            Next
            header.SeisakudaisuKanseisya = daisu
            header.SeisakudaisuWb = wb
            header.SeisakudaisuChushi = abort
            header.notifyObservers()
        End Sub

        ''' <summary>
        ''' ベース車の持つ種別・号車を元に、他へ通知する
        ''' </summary>
        ''' <param name="sender">ベース者情報</param>
        ''' <remarks></remarks>
        Public Sub NotifyAllShubetsuGosha(ByVal sender As EventEditBaseCar)
            If sender IsNot baseCar Then
                Throw New ArgumentException()
            End If
            summarySync = True
            Try
                For Each rowNo As Integer In baseCar.GetInputRowNos
                    NotifyShubetsu(baseCar, rowNo)
                    NotifyGosha(baseCar, rowNo)
                Next
            Finally
                summarySync = False
            End Try
            SummaryDaisu()
        End Sub
        ''' <summary>
        ''' 種別が更新された事を通知する
        ''' </summary>
        ''' <param name="sender">更新元</param>
        ''' <param name="rowNo">行No</param>
        ''' <remarks></remarks>
        Public Sub NotifyShubetsu(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncShubetsuGosha.NotifyShubetsu
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                Dim value As String = Nothing

                If sender Is baseCar Then
                    value = baseCar.ShisakuSyubetu(rowNo)
                ElseIf sender Is completeCar Then
                    value = completeCar.ShisakuSyubetu(rowNo)
                ElseIf sender Is basicOption Then
                    value = basicOption.ShisakuSyubetu(rowNo)
                ElseIf sender Is specialOption Then
                    value = specialOption.ShisakuSyubetu(rowNo)
                End If

                If sender IsNot baseCar Then
                    baseCar.ShisakuSyubetu(rowNo) = value
                End If
                If sender IsNot completeCar Then
                    completeCar.ShisakuSyubetu(rowNo) = value
                End If
                If sender IsNot basicOption Then
                    basicOption.ShisakuSyubetu(rowNo) = value
                End If
                If sender IsNot specialOption Then
                    specialOption.ShisakuSyubetu(rowNo) = value
                End If
                'baseTenkaiCar.ShisakuSyubetu(rowNo) = value
                'referenceCar.ShisakuSyubetu(rowNo) = value
                SummaryDaisu()
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
        Public Sub NotifyGosha(ByVal sender As Object, ByVal rowNo As Integer) Implements EzSyncShubetsuGosha.NotifyGosha
            If notifySync Then
                Return
            End If
            notifySync = True
            Try
                Dim value As String = Nothing
                If sender Is baseCar Then
                    value = baseCar.ShisakuGousya(rowNo)
                ElseIf sender Is completeCar Then
                    value = completeCar.ShisakuGousya(rowNo)
                ElseIf sender Is basicOption Then
                    value = basicOption.ShisakuGousya(rowNo)
                ElseIf sender Is specialOption Then
                    value = specialOption.ShisakuGousya(rowNo)
                End If

                If sender IsNot baseCar Then
                    baseCar.ShisakuGousya(rowNo) = value
                End If
                If sender IsNot completeCar Then
                    completeCar.ShisakuGousya(rowNo) = value
                End If
                If sender IsNot basicOption Then
                    basicOption.ShisakuGousya(rowNo) = value
                End If
                If sender IsNot specialOption Then
                    specialOption.ShisakuGousya(rowNo) = value
                End If
                'baseTenkaiCar.ShisakuGousya(rowNo) = value
                'referenceCar.ShisakuGousya(rowNo) = value
                SummaryDaisu()
            Finally
                notifySync = False
            End Try
        End Sub
    End Class
End Namespace
