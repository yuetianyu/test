Imports EventSakusei.ShisakuBuhinEdit.Al.Logic
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
Imports ShisakuCommon
'↓↓2014/09/19 酒井 ADD BEGIN
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
'↑↑2014/09/19 酒井 ADD END
'↓↓2014/10/23 酒井 ADD BEGIN
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
'↑↑2014/10/23 酒井 ADD END

Namespace ShisakuBuhinEdit.Logic
    ''' <summary>
    ''' INSTL品番関連で同期を取らせる為の簡単な実装クラス
    ''' </summary>
    ''' <remarks>A/L作成機能で入力したINSTL品番を、部品構成編集へ反映する</remarks>
    Public Class EzSyncInstlHinbanImpl
        Implements EzSyncInstlHinban
        Private alSubject As BuhinEditAlSubject
        Private koseiSubject As BuhinEditKoseiSubject

        Private Initializing As Boolean
        ''' <summary>
        ''' 初期設定する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Initialize(ByVal alSubject As BuhinEditAlSubject, ByVal koseiSubject As BuhinEditKoseiSubject)
            Me.alSubject = alSubject
            Me.koseiSubject = koseiSubject
            Initializing = True
            koseiSubject.IsSuspend_OnChangedInstlHinbanOrKbn = True
            Try
                For Each columnIndex As Integer In alSubject.GetInputInstlHinbanColumnIndexes
                    NotifyInstlHinban(columnIndex)
                    NotifyInstlHinbanKbn(columnIndex)
                    ''↓↓2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD BEGIN
                    NotifyInstlDataKbn(columnIndex)
                    ''↑↑2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD END
                    ''↓↓2014/08/15 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD BEGIN
                    NotifyBaseInstlFlg(columnIndex)
                ''↑↑2014/08/15 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD END
                Next
            Finally
                koseiSubject.IsSuspend_OnChangedInstlHinbanOrKbn = False
                Initializing = False
            End Try
        End Sub

        ''' <summary>
        ''' INSTL品番が更新された事を通知する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <remarks></remarks>
        Public Sub NotifyInstlHinban(ByVal columnIndex As Integer) Implements EzSyncInstlHinban.NotifyInstlHinban
            koseiSubject.InstlHinban(columnIndex) = alSubject.InstlHinban(columnIndex)
            If Not Initializing Then
                koseiSubject.NotifyTitleObservers(columnIndex)
            End If
        End Sub

        ''' <summary>
        ''' INSTL品番区分が更新された事を通知する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <remarks></remarks>
        Public Sub NotifyInstlHinbanKbn(ByVal columnIndex As Integer) Implements EzSyncInstlHinban.NotifyInstlHinbanKbn
            koseiSubject.InstlHinbanKbn(columnIndex) = alSubject.InstlHinbanKbn(columnIndex)
            If Not Initializing Then
                koseiSubject.NotifyTitleObservers(columnIndex)
            End If
        End Sub

        ''↓↓2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD BEGIN
        ''' <summary>
        ''' INSTLデータ区分が更新された事を通知する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <remarks></remarks>
        Public Sub NotifyInstlDataKbn(ByVal columnIndex As Integer) Implements EzSyncInstlHinban.NotifyInstlDataKbn
            koseiSubject.InstlDataKbn(columnIndex) = alSubject.InstlDataKbn(columnIndex)
            If Not Initializing Then
                koseiSubject.NotifyTitleObservers(columnIndex)
            End If
        End Sub
        ''↑↑2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD END

        '↓↓2014/08/15 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD BEGIN
        ''' <summary>
        ''' ベース情報フラグが更新された事を通知する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <remarks></remarks>
        Public Sub NotifyBaseInstlFlg(ByVal columnIndex As Integer) Implements EzSyncInstlHinban.NotifyBaseInstlFlg
            koseiSubject.BaseInstlFlg(columnIndex) = alSubject.BaseInstlFlg(columnIndex)
            If Not Initializing Then
                koseiSubject.NotifyTitleObservers(columnIndex)
            End If
        End Sub
        ''↑↑2014/08/15 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD END

        ''' <summary>
        ''' INSTL品番列に列挿入する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <param name="insertCount">列挿入数</param>
        ''' <remarks></remarks>
        Public Sub InsertColumnInInstl(ByVal columnIndex As Integer, ByVal insertCount As Integer) Implements EzSyncInstlHinban.InsertColumnInInstl

            koseiSubject.InsertColumnInInstl(columnIndex, insertCount)

            '----------------------------------------------------------------------------
            '2014/01/28 yaginuma AL画面で列挿入した場合は部品表画面の行挿入は行わない。
            '   '行挿入も同時に行う'
            'koseiSubject.InsertRowInstl(columnIndex, insertCount)
            '----------------------------------------------------------------------------


            koseiSubject.NotifyObservers()

        End Sub

        ''' <summary>
        ''' INSTL品番列を列削除する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <param name="removeCount">削除列数</param>
        ''' <remarks></remarks>
        Public Sub RemoveColumnInInstl(ByVal columnIndex As Integer, ByVal removeCount As Integer) Implements EzSyncInstlHinban.RemoveColumnInInstl
            'どの行のどの列を削除するか探す'
            'For Each index As Integer In koseiSubject.GetInputRowIndexes
            '    'レベルが０'
            '    If koseiSubject.Level(index) = 0 Then
            '        '員数が空でない'
            '        If Not StringUtil.IsEmpty(koseiSubject.InsuSuryo(index, columnIndex)) Then
            '            'サブジェクト側に員数がいないから無意味'
            '            koseiSubject.RemoveRowInstl(index, removeCount)
            '            Exit For
            '        End If
            '    End If
            'Next
            koseiSubject.RemoveColumnInInstl(columnIndex, removeCount)

            koseiSubject.NotifyObservers()
        End Sub

        ''' <summary>
        ''' INSTL品番が更新された事を通知する(イベント品番コピー)
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <remarks></remarks>
        Public Sub NotifyInstlHinbanEventCopy(ByVal columnIndex As Integer) Implements EzSyncInstlHinban.NotifyInstlHinbanEventCopy
            koseiSubject.InstlHinban2(columnIndex) = alSubject.InstlHinban(columnIndex)
            'If Not Initializing Then
            '    koseiSubject.NotifyTitleObservers(columnIndex)
            'End If
        End Sub
        ''' <summary>
        ''' 新しいINSTL品番で構成を作成する(イベント品番コピー)
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="kaiteiNo">改定No　  2014/08/04 Ⅰ.11.改訂戻し機能 ｊ) (TES)施 追加 </param>
        ''' <remarks></remarks>
        Public Sub NotifyInstlHinbanGetKosei(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String, Optional ByVal KaiteiNo As String = "", Optional ByVal eventCopyFlg As Boolean = False, Optional ByVal baseFlg As Boolean = False, Optional ByVal addStartIndex As Integer = 0) Implements EzSyncInstlHinban.NotifyInstlHinbanGetKosei
            '↓↓2014/10/23 酒井 ADD BEGIN
            'Public Sub NotifyInstlHinbanGetKosei(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String, Optional ByVal KaiteiNo As String = "", Optional ByVal eventCopyFlg As Boolean = False) Implements EzSyncInstlHinban.NotifyInstlHinbanGetKosei
            '↑↑2014/10/23 酒井 ADD END
            '↓↓2014/09/25 酒井 ADD BEGIN
            'Public Sub NotifyInstlHinbanGetKosei(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String, Optional ByVal KaiteiNo As String = "") Implements EzSyncInstlHinban.NotifyInstlHinbanGetKosei
            '↑↑2014/09/25 酒井 ADD END
            'イベントコードとブロックNoで構成を取得する'
            '画面側で構成を元に戻す処理'
            'Dim closer As New MatrixBackCloserIfNecessary(koseiSubject, bakMatrix, aFormCloseAdd)


            'koseiSubject.SupersedeMatrix(koseiSubject.NewMatrixKoseiTenkai(False, "1"))
            ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能 ｊ) (TES)施 ADD BEGIN
            If KaiteiNo = "" Then
                '↓↓2014/09/25 酒井 ADD BEGIN
                '                koseiSubject.SupersedeMatrix(koseiSubject.NewMatrixKoseiTenkai(False, "1"))
                If Not eventCopyFlg Then
                    koseiSubject.SupersedeMatrix(koseiSubject.NewMatrixKoseiTenkai(False, "1"))
                Else
                    '↓↓2014/10/23 酒井 ADD BEGIN
                    'koseiSubject.SupersedeMatrix(koseiSubject.NewMatrixKoseiTenkai(False, "1", "", eventCopyFlg))
                    If baseFlg Then
                        'ベース車改修かつイベント品番コピーの場合、ベース部品を残す。
                        For index As Integer = koseiSubject._koseiMatrix.GetMaxInputRowIndex To 0 Step -1
                            If Not koseiSubject._koseiMatrix.Record(index).BaseBuhinFlg = "1" Then
                                If Not koseiSubject._koseiMatrix.Record(index).Level.Equals(0) Then
                                    koseiSubject._koseiMatrix.RemoveRow(index)

                                End If
                            End If
                        Next
                        Dim tmpMatrix As BuhinKoseiMatrix = koseiSubject.NewMatrixKoseiTenkai(False, "1", "", eventCopyFlg, baseFlg, addStartIndex)

                        '参照先のINSTL、部品構成のベースフラグを「0」固定する
                        For Each index As Integer In koseiSubject.GetInputInstlHinbanColumnIndexes
                            If index >= addStartIndex Then
                                koseiSubject.instlTitle.BaseInstlFlg(index) = "0"
                            End If
                        Next
                        For Each index As Integer In tmpMatrix.GetInputRowIndexes
                            tmpMatrix.Record(index).BaseBuhinFlg = "0"
                        Next

                        koseiSubject._koseiMatrix.Insert(koseiSubject._koseiMatrix.GetNewRowIndex, tmpMatrix)

                        '旧koseiMatirxは、alInstl追加のezSyncでデフォルトのlevel=0行が追加されている。
                        '新tmpMatrixは、参照先のlevel=0が追加されている。
                        '重複しているため、（表示順が正しい方）前者に寄せる。
                        Dim delRows As New List(Of Integer)
                        For Each columnIndex As Integer In koseiSubject.GetInputInstlHinbanColumnIndexes
                            If columnIndex < addStartIndex Then
                                Continue For
                            End If

                            Dim sameRows As New List(Of Integer)

                            For rowIndex As Integer = 0 To koseiSubject.GetMaxInputRowIndex
                                If Not koseiSubject._koseiMatrix.Record(rowIndex).Level = 0 Then
                                    Continue For
                                End If

                                If koseiSubject.InsuSuryo(rowIndex, columnIndex) = 1 Then
                                    sameRows.Add(rowIndex)
                                End If
                            Next
                            For index = 1 To sameRows.Count - 1
                                VoUtil.CopyProperties(koseiSubject._koseiMatrix.Record(sameRows(index)), koseiSubject._koseiMatrix.Record(sameRows(0)))
                                delRows.Add(sameRows(index))
                            Next
                        Next
                        delRows.Sort()
                        Dim maeRow As Integer = -1
                        For index As Integer = delRows.Count - 1 To 0 Step -1
                            If delRows(index) = maeRow Then
                                Continue For
                            End If
                            koseiSubject.RemoveRow(delRows(index), 1)
                            maeRow = delRows(index)
                        Next

                        koseiSubject.CallSetChanged()
                    Else
                        koseiSubject.SupersedeMatrix(koseiSubject.NewMatrixKoseiTenkai(False, "1", "", eventCopyFlg))
                    End If
                '↑↑2014/10/23 酒井 ADD END
                End If
                '↑↑2014/09/25 酒井 ADD END
            Else
                koseiSubject.SupersedeMatrix(koseiSubject.NewMatrixKoseiTenkai(False, "1", KaiteiNo))
            End If
            ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 ｊ) (TES)施 ADD END
            koseiSubject.NotifyObservers()
        End Sub

        ''↓↓2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bm) (TES)張 ADD BEGIN
        ''' <summary>
        ''' 補用部品検索画面で構成を作成する(イベント品番コピー)
        ''' </summary>
        ''' <param name="HoyouKoseiMatrix">補用部品検索画面のspread全行</param>
        ''' <remarks></remarks>
        Public Sub NotifyHoyouGetKosei(ByVal HoyouKoseiMatrix As ShisakuBuhinEdit.Kosei.Logic.Matrix.BuhinKoseiMatrix) Implements EzSyncInstlHinban.NotifyHoyouGetKosei
            'イベントコードとブロックNoで構成を取得する'
            '画面側で構成を元に戻す処理'
            'Dim closer As New MatrixBackCloserIfNecessary(koseiSubject, bakMatrix, aFormCloseAdd)

            ''↓↓2014/09/19 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
            '該当イベント取得
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(koseiSubject.EventCode)
            ''↑↑2014/09/19 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END

            Dim removeIndex As New List(Of Integer)
            ''↓↓2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD BEGIN
            'For rowIndex As Integer = 1 To HoyouKoseiMatrix.Records.Count - 1
            For rowIndex As Integer = 0 To HoyouKoseiMatrix.Records.Count - 1
                ''↑↑2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD END
                If HoyouKoseiMatrix.Record(rowIndex).Level = 0 Then
                    Continue For
                End If
                Dim insus As Integer = 0
                ''↓↓2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD BEGIN
                'For j As Integer = 0 To rowIndex
                For j As Integer = 0 To rowIndex - 1
                    ''↑↑2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD END
                    If HoyouKoseiMatrix.Record(j).Level = 0 Then
                        Continue For
                    End If

                    ''↓↓2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD BEGIN
                    ''↓↓2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD BEGIN
                    ''↓↓2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD BEGIN
                    'If HoyouKoseiMatrix.Record(rowIndex).BuhinNo.Equals(HoyouKoseiMatrix.Record(j).BuhinNo) Then
                    'If StringUtil.Equals(HoyouKoseiMatrix.Record(rowIndex).BuhinNo, HoyouKoseiMatrix.Record(j).BuhinNo) Then
                    'If StringUtil.Equals(HoyouKoseiMatrix.Record(rowIndex).BuhinNo, HoyouKoseiMatrix.Record(j).BuhinNo) And _
                    'StringUtil.Equals(HoyouKoseiMatrix.Record(rowIndex).BuhinNoKbn, HoyouKoseiMatrix.Record(j).BuhinNoKbn) And _
                    'StringUtil.Equals(HoyouKoseiMatrix.Record(rowIndex).BaseBuhinFlg, HoyouKoseiMatrix.Record(j).BaseBuhinFlg) Then
                    'If StringUtil.Equals(HoyouKoseiMatrix.Record(rowIndex).BuhinNo, HoyouKoseiMatrix.Record(j).BuhinNo) And _
                    '    StringUtil.Equals(StringUtil.Nvl(HoyouKoseiMatrix.Record(rowIndex).BuhinNoKbn), StringUtil.Nvl(HoyouKoseiMatrix.Record(j).BuhinNoKbn)) Then
                    If StringUtil.Equals(HoyouKoseiMatrix.Record(rowIndex).BuhinNo, HoyouKoseiMatrix.Record(j).BuhinNo) AndAlso _
                       StringUtil.Equals(StringUtil.Nvl(HoyouKoseiMatrix.Record(rowIndex).Level), StringUtil.Nvl(HoyouKoseiMatrix.Record(j).Level)) AndAlso _
                       StringUtil.Equals(StringUtil.Nvl(HoyouKoseiMatrix.Record(rowIndex).ShukeiCode), StringUtil.Nvl(HoyouKoseiMatrix.Record(j).ShukeiCode)) AndAlso _
                       StringUtil.Equals(StringUtil.Nvl(HoyouKoseiMatrix.Record(rowIndex).SiaShukeiCode), StringUtil.Nvl(HoyouKoseiMatrix.Record(j).SiaShukeiCode)) AndAlso _
                       StringUtil.Equals(StringUtil.Nvl(HoyouKoseiMatrix.Record(rowIndex).GencyoCkdKbn), StringUtil.Nvl(HoyouKoseiMatrix.Record(j).GencyoCkdKbn)) AndAlso _
                       StringUtil.Equals(StringUtil.Nvl(HoyouKoseiMatrix.Record(rowIndex).MakerCode), StringUtil.Nvl(HoyouKoseiMatrix.Record(j).MakerCode)) AndAlso _
                       StringUtil.Equals(StringUtil.Nvl(HoyouKoseiMatrix.Record(rowIndex).BuhinNoKbn), StringUtil.Nvl(HoyouKoseiMatrix.Record(j).BuhinNoKbn)) AndAlso _
                       StringUtil.Equals(StringUtil.Nvl(HoyouKoseiMatrix.Record(rowIndex).KyoukuSection), StringUtil.Nvl(HoyouKoseiMatrix.Record(j).KyoukuSection)) Then
                        If (eventVo.BlockAlertKind = 1) Or _
                        (Not HoyouKoseiMatrix.Record(rowIndex).BaseBuhinFlg = 1 And Not HoyouKoseiMatrix.Record(j).BaseBuhinFlg = 1) Then
                            ''↑↑2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD END
                            ''↑↑2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD END
                            ''↑↑2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD END
                            'HoyouKoseiMatrix.record(j)の員数をHoyouKoseiMatrix.record(j)に加算する
                            Dim columnIndex As Integer = 0
                            For Each index As Integer In HoyouKoseiMatrix.GetInputInsuColumnIndexes
                                ''↓↓2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD BEGIN
                                If index >= columnIndex Then
                                    columnIndex = index
                                End If
                                ''↑↑2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD END
                            Next
                            ''↓↓2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD BEGIN
                            For instlIndex As Integer = 0 To columnIndex
                                insus = 0
                                'If Not HoyouKoseiMatrix.InsuSuryo(j, columnIndex) Is Nothing Then
                                If Not HoyouKoseiMatrix.InsuSuryo(j, instlIndex) Is Nothing Then
                                    'insus = insus + HoyouKoseiMatrix.InsuSuryo(j, columnIndex)
                                    insus = insus + HoyouKoseiMatrix.InsuSuryo(j, instlIndex)
                                End If
                                If Not HoyouKoseiMatrix.InsuSuryo(rowIndex, instlIndex) Is Nothing Then
                                    insus = insus + HoyouKoseiMatrix.InsuSuryo(rowIndex, instlIndex)
                                End If
                                'HoyouKoseiMatrix.InsuSuryo(j, columnIndex) = insus
                                ''↓↓2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD BEGIN
                                If Not insus = 0 Then
                                    ''↑↑2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD END
                                    HoyouKoseiMatrix.InsuSuryo(j, instlIndex) = insus
                                    ''↓↓2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD BEGIN
                                Else
                                    HoyouKoseiMatrix.InsuSuryo(j, instlIndex) = Nothing
                                End If
                                ''↑↑2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD END
                            Next
                            'removeIndex.Add(j)
                            removeIndex.Add(rowIndex)
                            ''↑↑2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD END
                        End If
                        ''↓↓2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD BEGIN
                    End If
                    ''↑↑2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD END
                Next
            Next
            ''↑↑2014/09/18 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD END
            'For Each index As Integer In removeIndex
            For index As Integer = removeIndex.Count - 1 To 0 Step -1
                ''↑↑2014/09/18 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD END
                ''↓↓2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD BEGIN
                'HoyouKoseiMatrix.RemoveRow(index)
                HoyouKoseiMatrix.RemoveRow(removeIndex(index))
                ''↑↑2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化_bm) 酒井 ADD END
            Next

            koseiSubject.SupersedeMatrix(HoyouKoseiMatrix)
            koseiSubject.NotifyObservers()
        End Sub
        ''↑↑2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bm) (TES)張 ADD END

        Private bakMatrix As ShisakuBuhinEdit.Kosei.Logic.Matrix.BuhinKoseiMatrix


        Public Sub BakEvent() Implements EzSyncInstlHinban.BakEvent
            '構成側を元に戻す'
            koseiSubject.SupersedeMatrix(bakMatrix)
            koseiSubject.NotifyObservers()
        End Sub

        ''' <summary>
        '''バックアップマトリクスを取得
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetBackUpKosei() Implements EzSyncInstlHinban.SetBackUpKosei
            bakMatrix = koseiSubject.copy
        End Sub

    End Class
End Namespace