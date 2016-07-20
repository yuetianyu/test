Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
Imports FarPoint.Win.Spread.Model
Imports FarPoint.Win
Imports EventSakusei.ShisakuBuhinEdit.Ikkatsu
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Ui
Imports ShisakuCommon.Ui.Valid
Imports ShisakuCommon.Ui.Spd
Imports ShisakuCommon.Util.OptionFilter
Imports FarPoint.Win.Spread
Imports EventSakusei.ShisakuBuhinEdit.Ui
Imports ShisakuCommon
'↓↓2014/10/29 酒井 ADD BEGIN
'Ver6_2 1.95以降の修正内容の展開
Imports EBom.Common
'↑↑2014/10/29 酒井 ADD END
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Dao
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.ShisakuBuhinEdit.SourceSelector.Logic
Imports EventSakusei.ShisakuBuhinEdit.Selector
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports FarPoint.Win.Spread.CellType


Namespace ShisakuBuhinEdit.Kosei
    ''' <summary>
    ''' 部品構成表示画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm41DispShisakuBuhinEdit20

        'SPREADのサイズ変更用に使用する。
        '１は標準サイズ
        Private Kakudai As Decimal = 1

        Private INZU As String = "D"
        Private ZAI As String = "D"
        Private JIKYU As String = "N"
        Private TSU As String = "D"

        'コピー＆ペースト用
        Private w_RowCount As Integer = 0
        Private w_ColumnCount As Integer = 0
        '補用品不具合水平展開
        'ペーストでセルを変更した際のチェンジイベント
        Dim WithEvents datamodel As FarPoint.Win.Spread.Model.DefaultSheetDataModel
        'ヘッダーの行高の標準サイズ。
        Private w_HEAD As Integer = 164

        Private _dispMode As Integer

        Private Const LARGER_WIDTH = 200

        Private _isIkansha As Boolean

        Private _eventVo As TShisakuEventVo

        '------------------------------------------------------------------
        '閉じるボタンを無効化するロジックです。
        Protected Overrides ReadOnly Property CreateParams() As  _
            System.Windows.Forms.CreateParams
            Get
                Const CS_NOCLOSE As Integer = &H200
                Dim cp As CreateParams = MyBase.CreateParams
                cp.ClassStyle = cp.ClassStyle Or CS_NOCLOSE

                Return cp
            End Get
        End Property

        'フォームのFormClosingイベントハンドラ
        Private Sub frm41DispShisakuBuhinHensyu20_FormClosing(ByVal sender As System.Object, _
                ByVal e As System.Windows.Forms.FormClosingEventArgs) _
                Handles MyBase.FormClosing
            '    '最大化されているときは閉じない
            e.Cancel = True
        End Sub

        Private Delegate Sub ConfirmIkkatsuDelegate(ByVal frm As Frm48DispShisakuBuhinEditIkkatsu)

        ''' <summary>
        ''' キャンセルだったら「一括設定」を再表示してやり直し
        ''' </summary>
        Private Class ReshowIfCancel : Implements IConfirmFormCloseAdd
            Private frm As Frm48DispShisakuBuhinEditIkkatsu
            Private confirmIkkatsu As ConfirmIkkatsuDelegate
            Public Sub New(ByVal frm As Frm48DispShisakuBuhinEditIkkatsu, ByVal confirmIkkatsu As ConfirmIkkatsuDelegate)
                Me.confirmIkkatsu = confirmIkkatsu
                Me.frm = frm
            End Sub
            Public Sub Process(ByVal IsOk As Boolean) Implements IConfirmFormCloseAdd.Process
                If IsOk Then
                    frm.Dispose()
                    Return
                End If

                ' キャンセルだったら、もう一度画面を表示する
                confirmIkkatsu(frm)
            End Sub
        End Class

        Private Sub msBuhinKouseiYobidashi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msBuhinKouseiYobidashi.Click

            If koujichuFlag Then
                ComFunc.ShowInfoMsgBox("システムの制限により現在「構成再展開」および「最新化」１度のみ実行できます。" & vbCrLf & _
                                       "お手数ですが「保存」し再度画面を開き直してから実行してください。", MessageBoxButtons.OK)
                Exit Sub
            End If

            Dim frm As New Frm48DispShisakuBuhinEditIkkatsu(koseiSubject.NewIkkatsuSubject, koseiObserver.InstlColumnCount, Me.shisakuEventCode)
            ConfirmIkkatsu(frm)

            koujichuFlag = True

        End Sub

        Private Sub ConfirmIkkatsu(ByVal frm As Frm48DispShisakuBuhinEditIkkatsu)
            frm.ShowDialog()
            If Not frm.ResultOk Then
                frm.Dispose()
                Return
            End If

            Dim jikyu As String = ""
            If StringUtil.Equals(cmbJikyuhinUmu.Text, "無") Then
                jikyu = "0"
            Else
                jikyu = "1"
            End If

            ConfirmNewMatrix(koseiSubject.NewMatrixBySpecified(True, frm.Results, jikyu, 1))
        End Sub

        Private Sub msBuhinKouseiIchiran_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msBuhinKouseiIchiran.Click
            Dim frm As New Frm41DispShisakuBuhinEdit25(koseiSubject)
            frm.Show()
        End Sub

        Private Sub ToolStripPurasu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripPurasu.Click
            Kakudai += 0.2
            If Kakudai > 2 Then
                MsgBox("これ以上拡大できません。", MsgBoxStyle.Information, "アラーム")
                Kakudai = 2
            End If
            spdParts.ActiveSheet.ZoomFactor = Kakudai
        End Sub

        Private Sub ToolStripMainasu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMainasu.Click
            Kakudai -= 0.2
            If Kakudai <= 0 Then
                MsgBox("これ以上縮小できません。", MsgBoxStyle.Information, "アラーム")
                Kakudai = 0.2
            End If
            spdParts.ActiveSheet.ZoomFactor = Kakudai
        End Sub

        Private Sub ToolStripHyoujyun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripHyoujyun.Click
            Kakudai = 1 'Nomalへ戻す
            spdParts.ActiveSheet.ZoomFactor = Kakudai
        End Sub

        Private Sub ToolStripGroupInzuu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripGroupInzuu.Click

            If INZU = "N" Then
                ToolStripGroupInzuu.Text = "員数非表示"
                ToolStripGroupInzuu.ToolTipText = "員数を非表示にします"
                ToolStripGroupInzuu.Checked = True
                koseiObserver.InzuuCoulumnVisible()
                INZU = "D"
            Else
                ToolStripGroupInzuu.Text = "員数表示"
                ToolStripGroupInzuu.ToolTipText = "員数を表示します"
                ToolStripGroupInzuu.Checked = False
                koseiObserver.InzuuCoulumnDisable()
                INZU = "N"
            End If
        End Sub

        Private Sub ToolStripGroupZaishitu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripGroupZaishitu.Click
            If ZAI = "N" Then
                ToolStripGroupZaishitu.Text = "材質非表示"
                ToolStripGroupZaishitu.ToolTipText = "材質を非表示にします"
                ToolStripGroupZaishitu.Checked = True
                koseiObserver.ZaishituColumnVisible()
                ZAI = "D"
            Else
                ToolStripGroupZaishitu.Text = "材質表示"
                ToolStripGroupZaishitu.ToolTipText = "材質を表示します"
                ToolStripGroupZaishitu.Checked = False
                koseiObserver.ZaishituColumnDisable()
                ZAI = "N"
            End If

        End Sub

        Private Sub ToolStripGroupTsukurikata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripGroupTsukurikata.Click

            If TSU = "N" Then
                ToolStripGroupTsukurikata.Text = "作り方非表示"
                ToolStripGroupTsukurikata.ToolTipText = "作り方を非表示にします"
                ToolStripGroupTsukurikata.Checked = True
                koseiObserver.TsukurikataColumnVisible()
                TSU = "D"
            Else
                ToolStripGroupTsukurikata.Text = "作り方表示"
                ToolStripGroupTsukurikata.ToolTipText = "作り方を表示します"
                ToolStripGroupTsukurikata.Checked = False
                koseiObserver.TsukurikataColumnDisable()
                TSU = "N"
                LabelTsukurikataMsg.Visible = False
            End If

        End Sub

        Private errorController As New ErrorController()

        Private koujichuFlag As Boolean = False



        ''' <summary>
        ''' 構成再展開ボタンの処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub BtnKoseiTenkai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnKoseiTenkai.Click

            'If koujichuFlag Then
            '    ComFunc.ShowInfoMsgBox("システムの制限により現在「構成再展開」および「最新化」１度のみ実行できます。" & vbCrLf & _
            '                           "お手数ですが「保存」し再度画面を開き直してから実行してください。", MessageBoxButtons.OK)
            '    Exit Sub
            'End If

            '-----------------------------------------------------------------------
            '2011/06/27 柳沼　INSTL品番が重複していたらエラーを返して処理を終了する。
            Dim existPair As New Dictionary(Of String, Integer)
            For Each columnIndex As Integer In koseiSubject.GetInputInstlHinbanColumnIndexes
                Dim key As String
                If Not _eventVo.BlockAlertKind = 2 And Not _eventVo.KounyuShijiFlg = "0" Then
                    key = EzUtil.MakeKey(StringUtil.Evl(koseiSubject.InstlHinban(columnIndex), ""), _
                                         StringUtil.Evl(koseiSubject.InstlHinbanKbn(columnIndex), ""), _
                                         StringUtil.Evl(koseiSubject.InstlDataKbn(columnIndex), "0"))
                Else
                    key = EzUtil.MakeKey(StringUtil.Evl(koseiSubject.InstlHinban(columnIndex), ""), _
                                         StringUtil.Evl(koseiSubject.InstlHinbanKbn(columnIndex), ""), _
                                         StringUtil.Evl(koseiSubject.InstlDataKbn(columnIndex), "0"), _
                                         StringUtil.Evl(koseiSubject.BaseInstlFlg(columnIndex), "0"))

                End If
                If existPair.ContainsKey(key) Then
                    MsgBox(String.Format("INSTL品番が他の列にも入力されています。"), MsgBoxStyle.Critical)
                    Exit Sub
                End If
                existPair.Add(key, columnIndex)
            Next
            '-----------------------------------------------------------------------

            Dim fm As New Frm41KouseiSourceSelector(koseiSubject.NewSourceSelectorSubject, koseiObserver.InstlColumnCount, 0, shisakuEventCode)
            fm.ShowDialog()

            If fm.ResultOk = False Then
                Exit Sub
            End If
            'フィルタリングを解除後、構成再展開を実施する。
            Try
                '全列のフィルタリングを解除
                koseiObserver.ResetFilterAll()
            Catch ex As Exception
                MsgBox(String.Format("フィルタリング解除でシステムエラーが発生しました。({0})", ex.Message), MsgBoxStyle.Critical)
            End Try

            errorController.ClearBackColor()
            Dim msgBoxResult As MsgBoxResult = Nothing
            Try

                koseiObserver.AssertValidateKoseiTenkai()

            Catch ex As IllegalInputException
                errorController.SetBackColorOnError(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)

                msgBoxResult = frm01Kakunin.ConfirmYesNo("赤色部分の部品構成が存在しません。", "現在表示されている部品構成を残しますか？")
                errorController.ClearBackColor()
            End Try

            Dim jikyu As String = ""
            If StringUtil.Equals(cmbJikyuhinUmu.Text, "無") Then
                jikyu = "0"
            Else
                jikyu = "1"
            End If

            If Not fm.Sabun Then
                '再展開の場合
                If _eventVo.BlockAlertKind = 2 And _eventVo.KounyuShijiFlg = "0" Then
                    '移管車改修の場合、ベース部品は元のまま

                    '2015/01/20 下記処理ではだめなので複数カラム選択の最新化と同様の処理へ変更

                    'Dim aBuhinKoseiMatrix As BuhinKoseiMatrix
                    'aBuhinKoseiMatrix = koseiSubject.NewMatrixBySpecified(msgBoxResult = msgBoxResult.Yes, fm.Results, jikyu, 1, True)
                    'Dim orgBakMatrix As BuhinKoseiMatrix = koseiSubject.Matrix.Copy
                    'Dim sakiBuhinKosei As BuhinKoseiMatrix = aBuhinKoseiMatrix
                    'Dim motoExistIndex As New List(Of Integer)
                    'For i As Integer = 0 To orgBakMatrix.Records.Count - 1
                    '    If orgBakMatrix.Record(i).BaseBuhinFlg = "1" Then
                    '        motoExistIndex.Add(i)
                    '    ElseIf orgBakMatrix.Record(i).Level = 0 Then
                    '        motoExistIndex.Add(i)
                    '    End If
                    'Next
                    'motoExistIndex.Sort()
                    'For i As Integer = sakiBuhinKosei.Records.Count - 1 To 0 Step -1
                    '    If sakiBuhinKosei.Record(i).Level = 0 Then
                    '        sakiBuhinKosei.RemoveRow(i)
                    '    End If
                    'Next
                    'Dim tmpMatrix As BuhinKoseiMatrix = koseiSubject.Matrix.CopyByIndex(motoExistIndex)
                    'tmpMatrix.Insert(tmpMatrix.GetNewRowIndex, sakiBuhinKosei)
                    'ConfirmNewMatrix(tmpMatrix, Nothing, orgBakMatrix)


                    '構成から有効列を取得
                    Dim lst As New ArrayList
                    For Each columnIndex As Integer In koseiSubject.GetInputInstlHinbanColumnIndexes
                        lst.Add(columnIndex)
                    Next
                    lst.Sort()
                    Dim col As Integer = -1 'カラム開始位置
                    Dim cnt As Integer = 0  'カラム終了位置
                    For Each columnIndex As Integer In lst

                        '移管車改修時のベースインストール品番以外の先頭列を取得
                        If Not koseiSubject.BaseInstlFlg(columnIndex) = "1" Then
                            If col < 0 Then
                                col = columnIndex
                            End If
                            cnt = columnIndex
                        End If
                    Next


                    Dim aBuhinKoseiMatrix As BuhinKoseiMatrix
                    aBuhinKoseiMatrix = koseiSubject.NewMatrixBySpecified(msgBoxResult = msgBoxResult.Yes, fm.Results, jikyu, 0, col, cnt - col + 1)

                    ConfirmNewMatrix(aBuhinKoseiMatrix)
                Else
                    'フル組の場合、既存処理
                    ConfirmNewMatrix(koseiSubject.NewMatrixBySpecified(msgBoxResult = msgBoxResult.Yes, fm.Results, jikyu, 0))
                    koseiObserver.ClearSheetBackColor()
                End If
            Else
                '2012/02/24 INSTL品番を差し替えるかどうかのフラグを追加
                Dim aBuhinKoseiMatrix As BuhinKoseiMatrix
                Dim index As Integer = 0
                aBuhinKoseiMatrix = koseiSubject.NewMatrixBySpecified(msgBoxResult = msgBoxResult.Yes, fm.Results, jikyu, 1, True)
                Dim orgBakMatrix As BuhinKoseiMatrix = koseiSubject.Matrix.Copy

                '比較元構成
                Dim motoBuhinKosei As BuhinKoseiMatrix = koseiSubject.Matrix.Copy4KoseiTenkaiTmpUse

                'motoBuhinKoseiの最後まで削除されなかったRecordの元々のindexを追跡する()
                'List.keyは最新のmotoBuhinKoseiのindexと同期、List.itemは元々のindexを表す
                Dim motoExistIndex As New List(Of Integer)
                For i As Integer = 0 To motoBuhinKosei.Records.Count - 1
                    motoExistIndex.Add(i)
                Next
                If _eventVo.BlockAlertKind = 2 And _eventVo.KounyuShijiFlg = "0" Then
                    For i As Integer = motoBuhinKosei.Records.Count - 1 To 0 Step -1
                        If motoBuhinKosei.Record(i).BaseBuhinFlg = "1" Then
                            motoBuhinKosei.RemoveRow(i)
                            motoExistIndex.RemoveAt(i)
                        End If
                    Next
                End If
                Dim sakiBuhinKosei As BuhinKoseiMatrix = aBuhinKoseiMatrix

                '比較結果セット
                Dim ResultSet As New List(Of HoyouBuhinfrm41HikakuResultSelectorVo)
                Dim selectorVo As HoyouBuhinfrm41HikakuResultSelectorVo

                '比較元構成と比較先構成を比較し、比較結果にセット
                For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                    Dim sakiVo As BuhinKoseiRecordVo = sakiBuhinKosei.Record(sakiIndex)
                    Dim sakiInsuCount As Integer = 0
                    'FINALの場合
                    If sakiVo.Level = 0 Then
                        Continue For
                    Else
                        For Each columnIndex As Integer In sakiBuhinKosei.GetInputInsuColumnIndexes
                            If sakiBuhinKosei.InsuSuryo(sakiIndex, columnIndex) > 0 Then
                                sakiInsuCount = sakiInsuCount + sakiBuhinKosei.InsuSuryo(sakiIndex, columnIndex)
                            End If
                        Next
                    End If

                    Dim contain As Boolean = False
                    Dim motoInsuCount As Integer = 0
                    For Each motoIndex As Integer In motoBuhinKosei.GetInputRowIndexes()
                        Dim motoVo As BuhinKoseiRecordVo = motoBuhinKosei.Record(motoIndex)
                        'FINALの場合
                        If motoVo.Level = 0 Then
                            Continue For
                        Else
                            For Each columnIndex As Integer In motoBuhinKosei.GetInputInsuColumnIndexes
                                If motoBuhinKosei.InsuSuryo(motoIndex, columnIndex) > 0 Then
                                    motoInsuCount = motoInsuCount + motoBuhinKosei.InsuSuryo(motoIndex, columnIndex)
                                End If
                            Next
                        End If

                        If StringUtil.Equals(sakiVo.BuhinNo, motoVo.BuhinNo) Then
                            contain = True
                        End If
                    Next
                    '比較元構成に存在しない
                    If contain = False Then
                        selectorVo = New HoyouBuhinfrm41HikakuResultSelectorVo
                        selectorVo.BuhinKoseiRecordVo = sakiVo
                        selectorVo.Insu = sakiInsuCount
                        selectorVo.Flag = "A"
                        selectorVo.Kubun = "比較先"
                        selectorVo.MotoGamen = "Frm41DispShisakuBuhinEdit20"
                        ResultSet.Add(selectorVo)
                    End If
                Next
                '比較元構成と比較先構成を比較し、比較結果にセット
                For Each motoIndex As Integer In motoBuhinKosei.GetInputRowIndexes()
                    Dim motoInsuCount As Integer = 0
                    Dim motoVo As BuhinKoseiRecordVo = motoBuhinKosei.Record(motoIndex)
                    'FINALの場合
                    If motoVo.Level = 0 Then
                        Continue For
                    Else
                        For Each columnIndex As Integer In motoBuhinKosei.GetInputInsuColumnIndexes
                            If motoBuhinKosei.InsuSuryo(motoIndex, columnIndex) > 0 Then
                                motoInsuCount = motoInsuCount + motoBuhinKosei.InsuSuryo(motoIndex, columnIndex)
                            End If
                        Next
                    End If

                    Dim contain As Boolean = False
                    For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                        Dim sakiVo As BuhinKoseiRecordVo = sakiBuhinKosei.Record(sakiIndex)
                        Dim sakiInsuCount As Integer = 0
                        'FINALの場合
                        If sakiVo.Level = 0 Then
                            Continue For
                        Else
                            For Each columnIndex As Integer In sakiBuhinKosei.GetInputInsuColumnIndexes
                                If sakiBuhinKosei.InsuSuryo(sakiIndex, columnIndex) > 0 Then
                                    sakiInsuCount = sakiInsuCount + sakiBuhinKosei.InsuSuryo(sakiIndex, columnIndex)
                                End If
                            Next
                        End If

                        '比較元構成．部品番号＝比較先構成．部品番号の場合
                        If StringUtil.Equals(motoVo.BuhinNo, sakiVo.BuhinNo) Then
                            contain = True
                            If motoInsuCount = sakiInsuCount And _
                               motoVo.Level = sakiVo.Level And _
                               StringUtil.Equals(motoVo.ShukeiCode, sakiVo.ShukeiCode) And _
                               StringUtil.Equals(motoVo.SiaShukeiCode, sakiVo.SiaShukeiCode) And _
                               StringUtil.Equals(motoVo.GencyoCkdKbn, sakiVo.GencyoCkdKbn) And _
                               StringUtil.Equals(motoVo.MakerCode, sakiVo.MakerCode) And _
                               StringUtil.Equals(motoVo.BuhinNoKbn, sakiVo.BuhinNoKbn) And _
                               StringUtil.Equals(motoVo.BuhinNoKaiteiNo, sakiVo.BuhinNoKaiteiNo) And _
                               StringUtil.Equals(motoVo.EdaBan, sakiVo.EdaBan) Then
                                '完全一致の場合
                                selectorVo = New HoyouBuhinfrm41HikakuResultSelectorVo
                                selectorVo.BuhinKoseiRecordVo = sakiVo
                                selectorVo.Insu = sakiInsuCount
                                selectorVo.Flag = ""
                                selectorVo.Kubun = ""
                                selectorVo.MotoGamen = "Frm41DispShisakuBuhinEdit20"
                                ResultSet.Add(selectorVo)
                            Else
                                '部品番号以外で不一致の場合
                                selectorVo = New HoyouBuhinfrm41HikakuResultSelectorVo
                                selectorVo.BuhinKoseiRecordVo = sakiVo
                                selectorVo.Insu = sakiInsuCount
                                selectorVo.Flag = "C"
                                selectorVo.Kubun = "比較先"
                                selectorVo.MotoGamen = "Frm41DispShisakuBuhinEdit20"
                                ResultSet.Add(selectorVo)
                                selectorVo = New HoyouBuhinfrm41HikakuResultSelectorVo
                                selectorVo.BuhinKoseiRecordVo = motoVo
                                selectorVo.Insu = motoInsuCount
                                selectorVo.Flag = "C"
                                selectorVo.Kubun = "比較元"
                                selectorVo.MotoGamen = "Frm41DispShisakuBuhinEdit20"
                                ResultSet.Add(selectorVo)
                            End If
                            Exit For
                        End If
                    Next
                    '比較先構成に存在しない
                    If contain = False Then
                        selectorVo = New HoyouBuhinfrm41HikakuResultSelectorVo
                        selectorVo.BuhinKoseiRecordVo = motoVo
                        selectorVo.Insu = motoInsuCount
                        selectorVo.Flag = "D"
                        selectorVo.Kubun = "比較元"
                        selectorVo.MotoGamen = "Frm41DispShisakuBuhinEdit20"
                        ResultSet.Add(selectorVo)
                    End If
                Next
                'A→D→Cの順で画面表示する
                Dim ResultSetSort As New List(Of HoyouBuhinfrm41HikakuResultSelectorVo)
                For Each result As HoyouBuhinfrm41HikakuResultSelectorVo In ResultSet
                    If result.Flag = "A" Then
                        ResultSetSort.Add(result)
                    End If
                Next
                For Each result As HoyouBuhinfrm41HikakuResultSelectorVo In ResultSet
                    If result.Flag = "D" Then
                        ResultSetSort.Add(result)
                    End If
                Next
                For Each result As HoyouBuhinfrm41HikakuResultSelectorVo In ResultSet
                    If result.Flag = "C" Then
                        ResultSetSort.Add(result)
                    End If
                Next
                For Each result As HoyouBuhinfrm41HikakuResultSelectorVo In ResultSet
                    If result.Flag <> "A" And result.Flag <> "D" And result.Flag <> "C" Then
                        ResultSetSort.Add(result)
                    End If
                Next
                Using frm41HikakuResult As New HoyouBuhinFrm41HikakuResultSelector(ResultSetSort)
                    frm41HikakuResult.ShowDialog()

                    If frm41HikakuResult.ResultOk = False Then
                        Exit Sub
                    End If

                    For Each resultVo As HoyouBuhinfrm41HikakuResultSelectorVo In frm41HikakuResult.ResultVos
                        'チェックあり
                        If resultVo.CheckedKbn = True Then
                            If resultVo.Kubun = "比較元" Then
                                '比較元構成の該当部品のLEVEL=1
                                For Each motoVo As BuhinKoseiRecordVo In motoBuhinKosei.Records
                                    If motoVo.Level = resultVo.BuhinKoseiRecordVo.Level And _
                                      StringUtil.Equals(motoVo.BuhinNo, resultVo.BuhinKoseiRecordVo.BuhinNo) And _
                                      StringUtil.Equals(motoVo.ShukeiCode, resultVo.BuhinKoseiRecordVo.ShukeiCode) And _
                                      StringUtil.Equals(motoVo.SiaShukeiCode, resultVo.BuhinKoseiRecordVo.SiaShukeiCode) And _
                                      StringUtil.Equals(motoVo.GencyoCkdKbn, resultVo.BuhinKoseiRecordVo.GencyoCkdKbn) And _
                                      StringUtil.Equals(motoVo.MakerCode, resultVo.BuhinKoseiRecordVo.MakerCode) And _
                                      StringUtil.Equals(motoVo.BuhinNoKbn, resultVo.BuhinKoseiRecordVo.BuhinNoKbn) And _
                                      StringUtil.Equals(motoVo.BuhinNoKaiteiNo, resultVo.BuhinKoseiRecordVo.BuhinNoKaiteiNo) And _
                                      StringUtil.Equals(motoVo.EdaBan, resultVo.BuhinKoseiRecordVo.EdaBan) Then
                                        '↑↑2014/10/14 酒井 ADD END
                                        motoVo.Level = 1
                                    End If
                                Next
                            ElseIf resultVo.Kubun = "比較先" Then
                                '比較先構成の該当部品のLEVEL=1
                                For Each sakiVo As BuhinKoseiRecordVo In sakiBuhinKosei.Records
                                    '↓↓2014/10/14 酒井 ADD BEGIN
                                    'If sakiVo.Equals(resultVo.BuhinKoseiRecordVo) Then
                                    If sakiVo.Level = resultVo.BuhinKoseiRecordVo.Level And _
                                      StringUtil.Equals(sakiVo.BuhinNo, resultVo.BuhinKoseiRecordVo.BuhinNo) And _
                                      StringUtil.Equals(sakiVo.ShukeiCode, resultVo.BuhinKoseiRecordVo.ShukeiCode) And _
                                      StringUtil.Equals(sakiVo.SiaShukeiCode, resultVo.BuhinKoseiRecordVo.SiaShukeiCode) And _
                                      StringUtil.Equals(sakiVo.GencyoCkdKbn, resultVo.BuhinKoseiRecordVo.GencyoCkdKbn) And _
                                      StringUtil.Equals(sakiVo.MakerCode, resultVo.BuhinKoseiRecordVo.MakerCode) And _
                                      StringUtil.Equals(sakiVo.BuhinNoKbn, resultVo.BuhinKoseiRecordVo.BuhinNoKbn) And _
                                      StringUtil.Equals(sakiVo.BuhinNoKaiteiNo, resultVo.BuhinKoseiRecordVo.BuhinNoKaiteiNo) And _
                                      StringUtil.Equals(sakiVo.EdaBan, resultVo.BuhinKoseiRecordVo.EdaBan) Then
                                        '↑↑2014/10/14 酒井 ADD END
                                        sakiVo.Level = 1
                                    End If
                                Next
                            Else
                                '比較元構成の該当部品のLEVEL=1
                                For Each motoVo As BuhinKoseiRecordVo In motoBuhinKosei.Records
                                    '↓↓2014/10/14 酒井 ADD BEGIN
                                    'If motoVo.Equals(resultVo.BuhinKoseiRecordVo) Then
                                    If motoVo.Level = resultVo.BuhinKoseiRecordVo.Level And _
                                      StringUtil.Equals(motoVo.BuhinNo, resultVo.BuhinKoseiRecordVo.BuhinNo) And _
                                      StringUtil.Equals(motoVo.ShukeiCode, resultVo.BuhinKoseiRecordVo.ShukeiCode) And _
                                      StringUtil.Equals(motoVo.SiaShukeiCode, resultVo.BuhinKoseiRecordVo.SiaShukeiCode) And _
                                      StringUtil.Equals(motoVo.GencyoCkdKbn, resultVo.BuhinKoseiRecordVo.GencyoCkdKbn) And _
                                      StringUtil.Equals(motoVo.MakerCode, resultVo.BuhinKoseiRecordVo.MakerCode) And _
                                      StringUtil.Equals(motoVo.BuhinNoKbn, resultVo.BuhinKoseiRecordVo.BuhinNoKbn) And _
                                      StringUtil.Equals(motoVo.BuhinNoKaiteiNo, resultVo.BuhinKoseiRecordVo.BuhinNoKaiteiNo) And _
                                      StringUtil.Equals(motoVo.EdaBan, resultVo.BuhinKoseiRecordVo.EdaBan) Then
                                        '↑↑2014/10/14 酒井 ADD END
                                        motoVo.Level = 1
                                    End If
                                Next
                                '比較先構成の該当部品を削除(.removecolumn)
                                For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                                    '↓↓2014/10/14 酒井 ADD BEGIN
                                    'If sakiBuhinKosei.Record(sakiIndex).Equals(resultVo.BuhinKoseiRecordVo) Then

                                    If sakiBuhinKosei.Record(sakiIndex).Level = resultVo.BuhinKoseiRecordVo.Level And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNo, resultVo.BuhinKoseiRecordVo.BuhinNo) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).ShukeiCode, resultVo.BuhinKoseiRecordVo.ShukeiCode) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).SiaShukeiCode, resultVo.BuhinKoseiRecordVo.SiaShukeiCode) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).GencyoCkdKbn, resultVo.BuhinKoseiRecordVo.GencyoCkdKbn) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).MakerCode, resultVo.BuhinKoseiRecordVo.MakerCode) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNoKbn, resultVo.BuhinKoseiRecordVo.BuhinNoKbn) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNoKaiteiNo, resultVo.BuhinKoseiRecordVo.BuhinNoKaiteiNo) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).EdaBan, resultVo.BuhinKoseiRecordVo.EdaBan) Then
                                        '↑↑2014/10/14 酒井 ADD END
                                        sakiBuhinKosei.RemoveRow(sakiIndex)
                                    End If
                                Next
                            End If
                        Else
                            If resultVo.Kubun = "比較元" Then
                                '比較元構成.removecolumn
                                For Each motoIndex As Integer In motoBuhinKosei.GetInputRowIndexes()
                                    '↓↓2014/10/14 酒井 ADD BEGIN
                                    'If motoBuhinKosei.Record(motoIndex).Equals(resultVo.BuhinKoseiRecordVo) Then
                                    If motoBuhinKosei.Record(motoIndex).Level = resultVo.BuhinKoseiRecordVo.Level And _
                                      StringUtil.Equals(motoBuhinKosei.Record(motoIndex).BuhinNo, resultVo.BuhinKoseiRecordVo.BuhinNo) And _
                                      StringUtil.Equals(motoBuhinKosei.Record(motoIndex).ShukeiCode, resultVo.BuhinKoseiRecordVo.ShukeiCode) And _
                                      StringUtil.Equals(motoBuhinKosei.Record(motoIndex).SiaShukeiCode, resultVo.BuhinKoseiRecordVo.SiaShukeiCode) And _
                                      StringUtil.Equals(motoBuhinKosei.Record(motoIndex).GencyoCkdKbn, resultVo.BuhinKoseiRecordVo.GencyoCkdKbn) And _
                                      StringUtil.Equals(motoBuhinKosei.Record(motoIndex).MakerCode, resultVo.BuhinKoseiRecordVo.MakerCode) And _
                                      StringUtil.Equals(motoBuhinKosei.Record(motoIndex).BuhinNoKbn, resultVo.BuhinKoseiRecordVo.BuhinNoKbn) And _
                                      StringUtil.Equals(motoBuhinKosei.Record(motoIndex).BuhinNoKaiteiNo, resultVo.BuhinKoseiRecordVo.BuhinNoKaiteiNo) And _
                                      StringUtil.Equals(motoBuhinKosei.Record(motoIndex).EdaBan, resultVo.BuhinKoseiRecordVo.EdaBan) Then
                                        '↑↑2014/10/14 酒井 ADD END
                                        motoBuhinKosei.RemoveRow(motoIndex)
                                        '↓↓2014/10/10 酒井 ADD BEGIN
                                        motoExistIndex.RemoveAt(motoIndex)
                                        '↑↑2014/10/10 酒井 ADD END
                                    End If
                                Next
                            ElseIf resultVo.Kubun = "比較先" Then
                                '比較先構成.removecolumn
                                For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                                    '↓↓2014/10/14 酒井 ADD BEGIN
                                    'If sakiBuhinKosei.Record(sakiIndex).Equals(resultVo.BuhinKoseiRecordVo) Then
                                    If sakiBuhinKosei.Record(sakiIndex).Level = resultVo.BuhinKoseiRecordVo.Level And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNo, resultVo.BuhinKoseiRecordVo.BuhinNo) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).ShukeiCode, resultVo.BuhinKoseiRecordVo.ShukeiCode) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).SiaShukeiCode, resultVo.BuhinKoseiRecordVo.SiaShukeiCode) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).GencyoCkdKbn, resultVo.BuhinKoseiRecordVo.GencyoCkdKbn) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).MakerCode, resultVo.BuhinKoseiRecordVo.MakerCode) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNoKbn, resultVo.BuhinKoseiRecordVo.BuhinNoKbn) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNoKaiteiNo, resultVo.BuhinKoseiRecordVo.BuhinNoKaiteiNo) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).EdaBan, resultVo.BuhinKoseiRecordVo.EdaBan) Then

                                        '↑↑2014/10/14 酒井 ADD END
                                        sakiBuhinKosei.RemoveRow(sakiIndex)
                                    End If
                                Next
                            Else
                                '比較元構成.removecolumn
                                For Each motoIndex As Integer In motoBuhinKosei.GetInputRowIndexes()
                                    '↓↓2014/10/14 酒井 ADD BEGIN
                                    'If motoBuhinKosei.Record(motoIndex).Equals(resultVo.BuhinKoseiRecordVo) Then
                                    If motoBuhinKosei.Record(motoIndex).Level = resultVo.BuhinKoseiRecordVo.Level And _
                                      StringUtil.Equals(motoBuhinKosei.Record(motoIndex).BuhinNo, resultVo.BuhinKoseiRecordVo.BuhinNo) And _
                                      StringUtil.Equals(motoBuhinKosei.Record(motoIndex).ShukeiCode, resultVo.BuhinKoseiRecordVo.ShukeiCode) And _
                                      StringUtil.Equals(motoBuhinKosei.Record(motoIndex).SiaShukeiCode, resultVo.BuhinKoseiRecordVo.SiaShukeiCode) And _
                                      StringUtil.Equals(motoBuhinKosei.Record(motoIndex).GencyoCkdKbn, resultVo.BuhinKoseiRecordVo.GencyoCkdKbn) And _
                                      StringUtil.Equals(motoBuhinKosei.Record(motoIndex).MakerCode, resultVo.BuhinKoseiRecordVo.MakerCode) And _
                                      StringUtil.Equals(motoBuhinKosei.Record(motoIndex).BuhinNoKbn, resultVo.BuhinKoseiRecordVo.BuhinNoKbn) And _
                                      StringUtil.Equals(motoBuhinKosei.Record(motoIndex).BuhinNoKaiteiNo, resultVo.BuhinKoseiRecordVo.BuhinNoKaiteiNo) And _
                                      StringUtil.Equals(motoBuhinKosei.Record(motoIndex).EdaBan, resultVo.BuhinKoseiRecordVo.EdaBan) Then
                                        '↑↑2014/10/14 酒井 ADD END
                                        motoBuhinKosei.RemoveRow(motoIndex)
                                        '↓↓2014/10/10 酒井 ADD BEGIN
                                        motoExistIndex.RemoveAt(motoIndex)
                                        '↑↑2014/10/10 酒井 ADD END
                                    End If
                                Next
                                '比較先構成.removecolumn
                                For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                                    '↓↓2014/10/14 酒井 ADD BEGIN
                                    'If sakiBuhinKosei.Record(sakiIndex).Equals(resultVo.BuhinKoseiRecordVo) Then
                                    If sakiBuhinKosei.Record(sakiIndex).Level = resultVo.BuhinKoseiRecordVo.Level And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNo, resultVo.BuhinKoseiRecordVo.BuhinNo) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).ShukeiCode, resultVo.BuhinKoseiRecordVo.ShukeiCode) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).SiaShukeiCode, resultVo.BuhinKoseiRecordVo.SiaShukeiCode) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).GencyoCkdKbn, resultVo.BuhinKoseiRecordVo.GencyoCkdKbn) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).MakerCode, resultVo.BuhinKoseiRecordVo.MakerCode) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNoKbn, resultVo.BuhinKoseiRecordVo.BuhinNoKbn) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNoKaiteiNo, resultVo.BuhinKoseiRecordVo.BuhinNoKaiteiNo) And _
                                      StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).EdaBan, resultVo.BuhinKoseiRecordVo.EdaBan) Then
                                        '↑↑2014/10/14 酒井 ADD END
                                        sakiBuhinKosei.RemoveRow(sakiIndex)
                                    End If
                                Next
                            End If
                        End If
                    Next
                    '比較先構成のLEVEL=0の部品を削除（.removecolumn）
                    ''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                    Dim delIndex As New List(Of Integer)
                    ''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                    For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                        If sakiBuhinKosei.Record(sakiIndex).Level = 0 Then
                            ''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                            'sakiBuhinKosei.RemoveRow(sakiIndex)
                            delIndex.Add(sakiIndex)
                            ''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                        End If
                    Next

                    ''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                    For index = delIndex.Count - 1 To 0 Step -1
                        sakiBuhinKosei.RemoveRow(index)
                    Next
                    ''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END

                    '空行を削除
                    index = 0
                    For Each record As BuhinKoseiRecordVo In sakiBuhinKosei.Records
                        If record.BuhinNo Is Nothing Then
                            sakiBuhinKosei.RemoveRow(index)
                        Else
                            index = index + 1
                        End If
                    Next
                    index = 0
                    For Each record As BuhinKoseiRecordVo In motoBuhinKosei.Records
                        If record.BuhinNo Is Nothing Then
                            motoBuhinKosei.RemoveRow(index)
                            '↓↓2014/10/14 酒井 ADD BEGIN
                            ''↓↓2014/10/10 酒井 ADD BEGIN
                            'motoExistIndex.RemoveAt(index)
                            ''↑↑2014/10/10 酒井 ADD END
                            '↑↑2014/10/14 酒井 ADD END
                        Else
                            index = index + 1
                        End If
                    Next

                    If _eventVo.BlockAlertKind = 2 And _eventVo.KounyuShijiFlg IsNot Nothing Then
                        For i As Integer = 0 To orgBakMatrix.Records.Count - 1
                            If orgBakMatrix.Record(i).BaseBuhinFlg = "1" Then
                                motoExistIndex.Add(i)
                            End If
                        Next
                        motoExistIndex.Sort()
                    End If
                    Dim tmpMatrix As BuhinKoseiMatrix = koseiSubject.Matrix.CopyByIndex(motoExistIndex)
                    tmpMatrix.Insert(tmpMatrix.GetNewRowIndex, sakiBuhinKosei)
                    ConfirmNewMatrix(tmpMatrix, Nothing, orgBakMatrix)
                    '↑↑2014/10/10 酒井 ADD END
                End Using
            End If
            koujichuFlag = True
        End Sub

        Public Property EControl() As ErrorController
            Get
                Return errorController
            End Get
            Set(ByVal value As ErrorController)
                errorController = value
            End Set
        End Property

        Private Sub ToolStripGroupJikyuhin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripGroupJikyuhin.Click
            '自給品ボタン非表示の為、コメントアウト
            'If JIKYU = "N" Then
            '    ToolStripGroupJikyuhin.ToolTipText = "自給品を非表示にします"
            '    ToolStripGroupJikyuhin.Text = "自給品非表示"
            '    ToolStripGroupJikyuhin.Checked = True
            '    koseiObserver.JikyuRowVisible()
            '    JIKYU = "D"
            'Else
            '    ToolStripGroupJikyuhin.ToolTipText = "自給品を表示します"
            '    'ToolStripGroupJikyuhin.Text = " 自給品表示 "
            '    ToolStripGroupJikyuhin.Text = "自給品表示"
            '    ToolStripGroupJikyuhin.Checked = False
            '    koseiObserver.JikyuRowDisable()
            '    JIKYU = "N"
            'End If
            ''変数を更新
            'SpdKoseiObserver.SPREAD_JIKYU = JIKYU

        End Sub

        Private ReadOnly shisakuEventCode As String
        Private koseiObserver As SpdKoseiObserver
        ''↓↓2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_QA95 (TES)張 ADD BEGIN
        Public ReadOnly Property frmKoseiObserver() As SpdKoseiObserver
            Get
                Return koseiObserver
            End Get
        End Property
        ''↑↑2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_QA95 (TES)張 ADD END
        Private ReadOnly koseiSubject As BuhinEditKoseiSubject
        Private ReadOnly inputSupport As ShisakuInputSupport

        ''↓↓2014/08/22 Ⅰ.3.設計編集 ベース車改修専用化_cf) (TES)張 ADD BEGIN
        'FINAL品番
        'Private finalBuhinNos As List(Of Rhac2210Vo)
        ''↑↑2014/08/22 Ⅰ.3.設計編集 ベース車改修専用化_cf) (TES)張 ADD END

        Public Sub New(ByVal shisakuEventCode As String, ByVal koseiSubject As BuhinEditKoseiSubject, ByVal dispMode As Integer)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)
            'ShisakuFormUtil.setTitleVersion(Me)
            'タイトル名を表示する。
            Me.Text = "[ 試作部品構成作成機能 ]"

            Me.StartPosition = FormStartPosition.Manual

            Me.shisakuEventCode = shisakuEventCode
            Me.koseiSubject = koseiSubject

            _dispMode = dispMode

            '該当イベント取得
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            _eventVo = eventDao.FindByPk(shisakuEventCode)


            _isIkansha = (_eventVo.KounyuShijiFlg = "0") And (_eventVo.BlockAlertKind = 2)


            koseiObserver = New SpdKoseiObserver(spdParts, koseiSubject)

            inputSupport = New ShisakuInputSupport(TxtInputSupport, spdParts)

            Initialize()
            InitializeSpread()

            '自給品ボタン非表示の為、コメントアウト
            ''自給品を非表示にする。
            'koseiObserver.JikyuRowDisableSyoki()
            'フラグをセットする。
            'SpdKoseiObserver.SPREAD_JIKYU = JIKYU

            ''↓↓2014/07/23 Ⅰ.2.管理項目追加_x) (TES)張 ADD BEGIN
            If koseiSubject.Tsukurikata = 1 Then
                ToolStripGroupTsukurikata.Text = "作り方非表示"
                ToolStripGroupTsukurikata.ToolTipText = "作り方を非表示にします"
                ToolStripGroupTsukurikata.Checked = True
                koseiObserver.TsukurikataColumnVisible()
                TSU = "D"
                '↓↓2014/10/01 酒井 ADD BEGIN
                'LabelTsukurikataMsg.Visible = True
                '↑↑2014/10/01 酒井 ADD END
            Else
                ToolStripGroupTsukurikata.Text = "作り方表示"
                ToolStripGroupTsukurikata.ToolTipText = "作り方を表示します"
                ToolStripGroupTsukurikata.Checked = False
                koseiObserver.TsukurikataColumnDisable()
                TSU = "N"
                LabelTsukurikataMsg.Visible = False
            End If
            ''↑↑2014/07/23 Ⅰ.2.管理項目追加_x) (TES)張 ADD END


        End Sub

        Public Sub Initialize()
            ' TODO 11月末実装予定
            cmbJikyuhinUmu.Items.Add("無")
            cmbJikyuhinUmu.Items.Add("有")

        End Sub

        Private Sub InitializeSpread()
            koseiObserver.Initialize()
            ShisakuSpreadUtil.AddEventCellRightClick(spdParts, inputSupport)

            'スプレッドに対してのコピーショートカットキー（Ctrl +C)を無効に（コード上でコピーを処理する為必要な処置)
            Dim imBase As New FarPoint.Win.Spread.InputMap
            imBase = Me.spdParts.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imBase.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)

            Dim imGousya As New FarPoint.Win.Spread.InputMap
            imGousya = Me.spdParts.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imGousya.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)


            '↓↓2014/10/06 酒井 ADD BEGIN
            '補用品不具合展開
            'スプレッドに対しペーストを行い変更を行った際のイベント処理です
            datamodel = CType(Me.spdParts.ActiveSheet.Models.Data, FarPoint.Win.Spread.Model.DefaultSheetDataModel)
            '↑↑2014/10/06 酒井 ADD END

        End Sub

        Public Sub AssertValidateRegister()
            koseiObserver.AssertValidateRegister()
        End Sub
        Public Sub AssertValidateRegisterWarning()
            koseiObserver.AssertValidateRegisterWarning()
        End Sub
        Public Sub AssertValidateSave()
            koseiObserver.AssertValidateSave()
        End Sub
        '↓↓2014/10/29 酒井 ADD BEGIN
        'Ver6_2 1.95以降の修正内容の展開
        Public Sub DeleteRowCheck()
            koseiObserver.DeleteRowCheck()
        End Sub
        '↑↑2014/10/29 酒井 ADD END

        ''' <summary>
        ''' 構成の員数チェック
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <remarks></remarks>
        Public Sub AssertValidateKoseiRegister(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String)
            koseiObserver.AssertValidateKoseiInsuCheck(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, JIKYU)
        End Sub

        ''' <summary>
        ''' 構成の員数チェック(ワーニング)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AssertValidateKoseiWarningRegster(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String)
            koseiObserver.AssertValidateKoseiInsuCheckWarning(shisakuEventCode, shisakuBukaCode, shisakuBlockNo)
        End Sub

        ''' <summary>
        ''' 取引先コードのチェック
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AssertValidateMakerCode()
            koseiObserver.AssertValidateKoseiMakerCodeAndNameCheck()
        End Sub

        ''' <summary>
        ''' 供給セクションのチェック
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AssertValidateKyoukuSection()
            koseiObserver.AssertValidateKoseiKyoukuSection()
        End Sub

        Public Sub AssertValidateHinban()
            koseiObserver.AssertValidateKoseiHinban()
        End Sub


        ''' <summary>
        ''' INSTL品番列の表示値をクリアする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ClearInstlColumns()
            koseiObserver.ClearInstlColumns(True)
        End Sub


        Public Sub ClearSheetBackColor()
            koseiObserver.ClearSheetBackColor()
        End Sub
        Public Sub ClearSheetBackColorAll()
            koseiObserver.ClearSheetBackColorAll()
        End Sub


        Private Sub ContextMenuStrip1_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening

            '2013/05/14　
            '閲覧モードの場合は常に「全て」無効
            If _dispMode = VIEW_MODE Then
                コピーToolStripMenuItem.Enabled = False
                貼り付けToolStripMenuItem.Enabled = False
                挿入ToolStripMenuItem.Enabled = False
                挿入下位ToolStripMenuItem.Enabled = False
                削除ToolStripMenuItem.Enabled = False
                最新化ToolStripMenuItem.Enabled = False
                子部品展開ToolStripMenuItem.Enabled = False
                挿入貼り付けToolStripMenuItem.Enabled = False
            Else
                'フィルタリング中かチェックする。
                'フィルタリング中なら行挿入、行削除を非表示にする。
                Dim wFilter As String = koseiObserver.FilterCheck()

                '切り取りToolStripMenuItem1.Enabled = True
                コピーToolStripMenuItem.Enabled = True
                貼り付けToolStripMenuItem.Enabled = True
                挿入ToolStripMenuItem.Enabled = False
                挿入下位ToolStripMenuItem.Enabled = False
                削除ToolStripMenuItem.Enabled = False
                最新化ToolStripMenuItem.Enabled = False
                子部品展開ToolStripMenuItem.Enabled = False
                挿入貼り付けToolStripMenuItem.Enabled = False

                If spdParts_Sheet1.SelectionCount <> 1 Then
                    Return
                End If

                Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)

                If SpreadUtil.IsSelectedRow(selection) AndAlso spdParts_Sheet1.Cells(selection.Row, 0).Value Is Nothing Then
                    If StringUtil.IsEmpty(wFilter) Then
                        挿入ToolStripMenuItem.Enabled = True
                        挿入下位ToolStripMenuItem.Enabled = True
                        削除ToolStripMenuItem.Enabled = True

                        If copyColumn = -1 Then
                            挿入貼り付けToolStripMenuItem.Enabled = True
                        Else
                            挿入貼り付けToolStripMenuItem.Enabled = False
                        End If
                    End If
                    '切り取りToolStripMenuItem1.Enabled = True
                    'コピーToolStripMenuItem.Enabled = True
                    '貼り付けToolStripMenuItem.Enabled = True
                    '切り取りToolStripMenuItem1.Enabled = False
                    コピーToolStripMenuItem.Enabled = False
                    貼り付けToolStripMenuItem.Enabled = False
                    Return
                End If


                If SpreadUtil.IsSelectedRow(selection) AndAlso koseiObserver.IsDataRow(selection.Row) Then
                    '但しINSTL品番行の場合は表示しない。
                    If (_eventVo.BlockAlertKind = 2 And _eventVo.KounyuShijiFlg = "0" And koseiSubject.Matrix.Record(koseiObserver.ConvSpreadRowToSubjectIndex(selection.Row)).BaseBuhinFlg = "1") Then
                        挿入ToolStripMenuItem.Enabled = True
                        挿入下位ToolStripMenuItem.Enabled = True
                        コピーToolStripMenuItem.Enabled = True
                    ElseIf spdParts_Sheet1.Cells(selection.Row, 0).Value <> 0 Then
                        If StringUtil.IsEmpty(wFilter) Then
                            挿入ToolStripMenuItem.Enabled = True
                            挿入下位ToolStripMenuItem.Enabled = True
                            削除ToolStripMenuItem.Enabled = True
                            '切り取りToolStripMenuItem1.Enabled = True
                            コピーToolStripMenuItem.Enabled = True
                            貼り付けToolStripMenuItem.Enabled = True
                            If copyColumn = -1 Then
                                挿入貼り付けToolStripMenuItem.Enabled = True
                            Else
                                挿入貼り付けToolStripMenuItem.Enabled = False
                            End If
                        End If
                    Else
                        '切り取りToolStripMenuItem1.Enabled = False
                        コピーToolStripMenuItem.Enabled = False
                        貼り付けToolStripMenuItem.Enabled = False
                    End If
                End If


                Dim sheet As Spread.SheetView = spdParts.ActiveSheet

                ' 選択セルの場所を特定します。
                ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
                ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス
                If ParaActColIdx = 6 AndAlso koseiObserver.IsDataRow(selection.Row) Then
                    '但しINSTL品番行の場合は表示しない。
                    If spdParts_Sheet1.Cells(selection.Row, 0).Value <> 0 Then
                        If StringUtil.IsEmpty(wFilter) Then
                            If Not (_eventVo.BlockAlertKind = 2 And _eventVo.KounyuShijiFlg = "0" And koseiSubject.Matrix.Record(koseiObserver.ConvSpreadRowToSubjectIndex(selection.Row)).BaseBuhinFlg = "1") Then
                                子部品展開ToolStripMenuItem.Enabled = True
                            Else
                                最新化ToolStripMenuItem.Enabled = False
                            End If
                        End If
                    End If
                End If

                'タイトル行だったら
                Dim titleRows As Integer = BuhinEditSpreadUtil.GetTitleRows(sheet)
                If selection.Row <= titleRows Then
                    '切り取りToolStripMenuItem1.Enabled = False
                    コピーToolStripMenuItem.Enabled = False
                    貼り付けToolStripMenuItem.Enabled = False
                    挿入貼り付けToolStripMenuItem.Enabled = False
                End If

                'フィルタリング中だったら・・・
                If Not StringUtil.IsEmpty(wFilter) Then
                    挿入ToolStripMenuItem.Enabled = False
                    挿入下位ToolStripMenuItem.Enabled = False
                    削除ToolStripMenuItem.Enabled = False
                    '切り取りToolStripMenuItem1.Enabled = False
                    コピーToolStripMenuItem.Enabled = False
                    貼り付けToolStripMenuItem.Enabled = False
                    挿入貼り付けToolStripMenuItem.Enabled = False
                End If

                If koseiObserver.CanEnabledOptimizeMenu(selection) Then
                    '切り取りToolStripMenuItem1.Enabled = False
                    コピーToolStripMenuItem.Enabled = False
                    貼り付けToolStripMenuItem.Enabled = False

                    If StringUtil.IsEmpty(wFilter) Then
                        If StringUtil.IsNotEmpty(spdParts_Sheet1.Cells(selection.Row, selection.Column).Value) Then
                            最新化ToolStripMenuItem.Enabled = True
                        End If
                    End If
                End If
                If koseiObserver.IsInputInstlColumn(ParaActColIdx) Then
                    Dim columnIndex As Integer = koseiObserver.ConvSpreadColumnToKoseiInstl(ParaActColIdx)

                    If StringUtil.IsNotEmpty(koseiSubject.BaseInstlFlg(columnIndex)) Then
                        If koseiSubject.BaseInstlFlg(columnIndex).Equals("1") Then
                            貼り付けToolStripMenuItem.Enabled = False
                            削除ToolStripMenuItem.Enabled = False
                            If _isIkansha Then
                                最新化ToolStripMenuItem.Enabled = False
                            End If
                        End If
                    End If
                End If

                If selection.Row >= 0 Then
                    If spdParts_Sheet1.Cells(selection.Row, 0).Value = "0" Then
                        挿入ToolStripMenuItem.Enabled = False
                    Else
                        If selection.Row > 0 Then
                            If spdParts_Sheet1.Cells(selection.Row - 1, 0).Value = "0" Then
                                挿入ToolStripMenuItem.Enabled = False
                            End If
                        End If
                    End If
                Else
                    挿入ToolStripMenuItem.Enabled = False
                End If


                Dim listClip As New List(Of String())

                listClip = GetClipbordList()

                If Not listClip Is Nothing Then

                    If listClip.Item(0)(0).Equals("0") Then
                        挿入貼り付けToolStripMenuItem.Enabled = False
                    End If
                End If

            End If
        End Sub

        Private Sub 挿入ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 挿入ToolStripMenuItem.Click
            If spdParts_Sheet1.SelectionCount <> 1 Then
                Return
            End If

            Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)
            Dim i As Integer = 0
            Dim wRowCount As Integer = selection.RowCount

            '自給品非表示の場合、自給品行数を除いた行数を計算
            If SpdKoseiObserver.SPREAD_JIKYU = "N" Then
                wRowCount = IsSelectRows(selection.Row, selection.RowCount)
            End If

            If SpreadUtil.IsSelectedRow(selection) Then
                koseiObserver.InsertRows(selection.Row, wRowCount)
            End If
        End Sub

        Private Sub 挿入下位ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 挿入下位ToolStripMenuItem.Click
            If spdParts_Sheet1.SelectionCount <> 1 Then
                Return
            End If

            Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)
            Dim i As Integer = 0
            Dim wRowCount As Integer = selection.RowCount

            '自給品ボタン非表示の為、コメントアウト
            ''自給品非表示の場合、自給品行数を除いた行数を計算
            'If SpdKoseiObserver.SPREAD_JIKYU = "N" Then
            '    wRowCount = IsSelectRows(selection.Row, selection.RowCount)
            'End If

            If SpreadUtil.IsSelectedRow(selection) Then
                koseiObserver.InsertRowsNext(selection.Row, wRowCount)
            End If
        End Sub

        ''' <summary>
        ''' 自給品を除いた行数を返す
        ''' </summary>
        ''' <param name="selectionRow">行index</param>
        ''' <param name="selectionRowCount">行カウント数</param>
        ''' <returns>行数</returns>
        ''' <remarks></remarks>
        Private Function IsSelectRows(ByVal selectionRow As Integer, ByVal selectionRowCount As Integer)
            '自給品ボタン非表示の為、コメントアウト
            'Dim i As Integer = 0
            'Dim wRowCount As Integer = selectionRowCount
            'If SpdKoseiObserver.SPREAD_JIKYU = "N" Then
            '    For i = selectionRow To selectionRowCount + selectionRow - 1
            '        If spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns("SHUKEI_CODE").Index).Value = "J" And _
            '                spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns("SIA_SHUKEI_CODE").Index).Value = "J" _
            '           Or spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns("SHUKEI_CODE").Index).Value = "J" And _
            '                spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns("SIA_SHUKEI_CODE").Index).Value <> "J" _
            '           Or spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns("SHUKEI_CODE").Index).Value = " " And _
            '                spdParts_Sheet1.Cells(i, spdParts_Sheet1.Columns("SIA_SHUKEI_CODE").Index).Value = "J" Then
            '            '自給品を非表示にしている場合には、非表示分の行数をマイナスする。
            '            wRowCount = wRowCount - 1
            '        End If
            '    Next
            'End If
            'Return wRowCount
            Return Nothing
        End Function

        Private Sub 削除ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 削除ToolStripMenuItem.Click
            If spdParts_Sheet1.SelectionCount <> 1 Then
                Return
            End If

            Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)
            If SpreadUtil.IsSelectedRow(selection) Then
                ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 CHG BEGIN
                'koseiObserver.RemoveRows(selection.Row, selection.RowCount)
                koseiObserver.RemoveRows(selection.Row, selection.RowCount, Me.shisakuEventCode)
                ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 CHG END
            End If
            '自給品ボタン非表示の為、コメントアウト
            ''自給品をいったん表示しフラグによって続けて非表示にする。'
            'koseiObserver.JikyuRowVisible()
            'If SpdKoseiObserver.SPREAD_JIKYU = "N" Then
            '    koseiObserver.JikyuRowDisable()
            'End If

            ''自給品の表示か非表示か判断させる'
            'If JIKYU = "N" Then
            '    koseiObserver.JikyuRowDisable()
            'Else
            '    koseiObserver.JikyuRowVisible()
            'End If
        End Sub

        ''' <summary>
        ''' 確認画面Cloes時の追加処理
        ''' </summary>
        ''' <remarks></remarks>
        Private Interface IConfirmFormCloseAdd
            ''' <summary>
            ''' 追加処理
            ''' </summary>
            ''' <param name="IsOk">OKが押された場合、true</param>
            ''' <remarks></remarks>
            Sub Process(ByVal IsOk As Boolean)
        End Interface

        ''' <summary>
        ''' キャンセルされたら古い部品構成編集に戻すCloser実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class MatrixBackCloserIfNecessary : Implements frm01Kakunin.IFormCloser
            Private koseiSubject As BuhinEditKoseiSubject
            Private bakMatrix As BuhinKoseiMatrix
            Private aFormCloseAdd As IConfirmFormCloseAdd
            Public Sub New(ByVal koseiSubject As BuhinEditKoseiSubject, ByVal bakMatrix As BuhinKoseiMatrix, ByVal aFormCloseAdd As IConfirmFormCloseAdd)
                Me.koseiSubject = koseiSubject
                Me.bakMatrix = bakMatrix
                Me.aFormCloseAdd = aFormCloseAdd
            End Sub
            Public Sub FormClose(ByVal IsOk As Boolean) Implements frm01Kakunin.IFormCloser.FormClose
                If Not IsOk Then
                    koseiSubject.SupersedeMatrix(bakMatrix)
                End If
                koseiSubject.IsViewerMode = False
                koseiSubject.NotifyObservers()
                If aFormCloseAdd IsNot Nothing Then
                    aFormCloseAdd.Process(IsOk)
                End If
            End Sub
        End Class

        ''' <summary>
        ''' 新しい部品構成編集で良いかを確認し、確認後に適切な処置を行う
        ''' </summary>
        ''' <param name="newMatrix">新しい部品構成編集</param>
        ''' <param name="aFormCloseAdd">確認画面Close時の追加処理</param>
        ''' <remarks></remarks>
        Private Sub ConfirmNewMatrix(ByVal newMatrix As BuhinKoseiMatrix, Optional ByVal aFormCloseAdd As IConfirmFormCloseAdd = Nothing, Optional ByVal orgBakMatrix As BuhinKoseiMatrix = Nothing)
            '↓↓2014/10/06 酒井 ADD BEGIN
            '        Private Sub ConfirmNewMatrix(ByVal newMatrix As BuhinKoseiMatrix, Optional ByVal aFormCloseAdd As IConfirmFormCloseAdd = Nothing)
            'Dim bakMatrix As BuhinKoseiMatrix = koseiSubject.Matrix

            Dim bakMatrix As BuhinKoseiMatrix
            If orgBakMatrix Is Nothing Then
                bakMatrix = koseiSubject.Matrix
            Else
                bakMatrix = orgBakMatrix
            End If
            '↑↑2014/10/06 酒井 ADD END

            If orgBakMatrix Is Nothing Then
                Dim lst As New ArrayList
                For Each colIdx As Integer In newMatrix.GetInputInsuColumnIndexes
                    lst.Add(colIdx)
                Next
                lst.Sort()
                Dim row As Integer = 0
                For Each colIdx As Integer In lst
                    If StringUtil.IsNotEmpty(koseiSubject.InstlHinban(colIdx)) Then
                        For Each rowIdx As Integer In newMatrix.GetInputRowIndexesOnColumn(colIdx)
                            If newMatrix.Record(rowIdx).Level = 0 Then

                                ''2015/08/26 追加 E.Ubukata 
                                ''ColumnBagをバックアップ
                                Dim colBug = newMatrix.InstlColumn(colIdx).Clone

                                ''Matrixのレコードをソート
                                '' 注)レコード削除の延長でColumnBagの対象レコードも削除されてしまう
                                Dim wkIdx As New List(Of Integer)
                                wkIdx.Add(rowIdx)
                                Dim wkRec1 = newMatrix.CopyByIndex(wkIdx)
                                wkIdx.Clear()
                                wkIdx.Add(row)
                                Dim wkRec2 = newMatrix.CopyByIndex(wkIdx)
                                newMatrix.RemoveRow(rowIdx)
                                newMatrix.Insert(rowIdx, wkRec2)
                                newMatrix.RemoveRow(row)
                                newMatrix.Insert(row, wkRec1)

                                ''ColumnBag内のレコードをソート
                                Dim addColBug As New InstlColumnBag
                                Dim k As Integer = 0
                                For Each j As Integer In newMatrix.GetInputRowIndexesOnColumn(colIdx)
                                    For i As Integer = 0 To colBug.Count - 1
                                        If colBug.Record(i).BuhinNoHyoujiJun IsNot Nothing Then
                                            If colBug.Record(i).BuhinNoHyoujiJun = newMatrix.Record(j).BuhinNoHyoujiJun Then
                                                addColBug.Insert(k, colBug.Record(i), colBug.InsuVo(i))
                                                k += 1
                                            End If
                                        Else
                                            If EzUtil.IsEqualIfNull(colBug.Record(i).BuhinNo, newMatrix.Record(j).BuhinNo) AndAlso _
                                               EzUtil.IsEqualIfNull(colBug.Record(i).BuhinNoKaiteiNo, newMatrix.Record(j).BuhinNoKaiteiNo) AndAlso _
                                               EzUtil.IsEqualIfNull(colBug.Record(i).BuhinNoKbn, newMatrix.Record(j).BuhinNoKbn) AndAlso _
                                               EzUtil.IsEqualIfNull(colBug.Record(i).InstlDataKbn, newMatrix.Record(j).InstlDataKbn) AndAlso _
                                               EzUtil.IsEqualIfNull(colBug.Record(i).KyoukuSection, newMatrix.Record(j).KyoukuSection) AndAlso _
                                               EzUtil.IsEqualIfNull(colBug.Record(i).Level, newMatrix.Record(j).Level) AndAlso _
                                               EzUtil.IsEqualIfNull(colBug.Record(i).ShukeiCode, newMatrix.Record(j).ShukeiCode) AndAlso _
                                               EzUtil.IsEqualIfNull(colBug.Record(i).SiaShukeiCode, newMatrix.Record(j).SiaShukeiCode) AndAlso _
                                               EzUtil.IsEqualIfNull(colBug.Record(i).BaseBuhinFlg, newMatrix.Record(j).BaseBuhinFlg) Then
                                                addColBug.Insert(k, colBug.Record(i), colBug.InsuVo(i))
                                                k += 1

                                            End If
                                        End If
                                    Next
                                Next
                                newMatrix.InstlColumnRemove(colIdx)
                                newMatrix.InstlColumnAdd(colIdx, addColBug)
                            End If
                        Next
                        row += 1
                    End If
                Next
            End If


            koseiSubject.IsViewerMode = True
            koseiSubject.SupersedeMatrix(newMatrix)
            koseiSubject.NotifyObservers()

            Dim closer As New MatrixBackCloserIfNecessary(koseiSubject, bakMatrix, aFormCloseAdd)
            ''最新化時にも表示非表示の処理をさせる'
            ''自給品の表示か非表示か判断させる'
            'If JIKYU = "N" Then
            '    koseiObserver.JikyuRowDisable()
            'Else
            '    koseiObserver.JikyuRowVisible()
            'End If
            frm01Kakunin.ConfirmShow("確定します。宜しいですか？", closer)




        End Sub

        Private Sub 最新化ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 最新化ToolStripMenuItem.Click
            If spdParts_Sheet1.SelectionCount <> 1 Then
                Return
            End If

            'If koujichuFlag Then
            '    ComFunc.ShowInfoMsgBox("システムの制限により現在「構成再展開」および「最新化」１度のみ実行できます。" & vbCrLf & _
            '                           "お手数ですが「保存」し再度画面を開き直してから実行してください。", MessageBoxButtons.OK)
            '    Exit Sub
            'End If

            '-----------------------------------------------------------------------
            '2011/06/27 柳沼　INSTL品番が重複していたらエラーを返して処理を終了する。
            Dim existPair As New Dictionary(Of String, Integer)
            For Each columnIndex As Integer In koseiSubject.GetInputInstlHinbanColumnIndexes
                Dim key As String
                If Not _eventVo.BlockAlertKind = 2 And Not _eventVo.KounyuShijiFlg = "0" Then
                    key = EzUtil.MakeKey(StringUtil.Evl(koseiSubject.InstlHinban(columnIndex), ""), _
                                         StringUtil.Evl(koseiSubject.InstlHinbanKbn(columnIndex), ""), _
                                         StringUtil.Evl(koseiSubject.InstlDataKbn(columnIndex), "0"))
                Else
                    key = EzUtil.MakeKey(StringUtil.Evl(koseiSubject.InstlHinban(columnIndex), ""), _
                                         StringUtil.Evl(koseiSubject.InstlHinbanKbn(columnIndex), ""), _
                                         StringUtil.Evl(koseiSubject.InstlDataKbn(columnIndex), "0"), _
                                         StringUtil.Evl(koseiSubject.BaseInstlFlg(columnIndex), "0"))

                End If
                If existPair.ContainsKey(key) Then
                    MsgBox(String.Format("INSTL品番が他の列にも入力されています。"), MsgBoxStyle.Critical)
                    Exit Sub
                End If
                existPair.Add(key, columnIndex)
            Next
            '-----------------------------------------------------------------------

            Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)
            Dim msg As String = "カラム：" & spdParts_Sheet1.Cells(selection.Row, selection.Column).Value
            Dim msgBoxResult As MsgBoxResult = Nothing

            If 1 < selection.ColumnCount Then
                msg &= "～" & spdParts_Sheet1.Cells(selection.Row, selection.Column + selection.ColumnCount - 1).Value
            End If
            If frm01Kakunin.ConfirmOkCancel("以下の構成を最新化しますか？", msg) <> msgBoxResult.Ok Then
                Return
            End If
            Dim selectionInstlHinbanAndKbn As New Dictionary(Of String, Integer)
            For Each columnIndex As Integer In koseiSubject.GetInputInstlHinbanColumnIndexes
                For i As Integer = 0 To selection.ColumnCount - 1
                    If koseiSubject.InstlHinban(columnIndex) = spdParts_Sheet1.Cells(selection.Row + 2, selection.Column + i).Value And _
                          koseiSubject.InstlHinbanKbn(columnIndex) = spdParts_Sheet1.Cells(selection.Row + 3, selection.Column + i).Value And _
                          SpdKoseiObserver.getInstlDataKbn(koseiSubject.InstlDataKbn(columnIndex)) = spdParts_Sheet1.Cells(selection.Row + 1, selection.Column + i).Value And _
                          Not (koseiSubject.BaseInstlFlg(columnIndex) = "1" And _isIkansha) Then
                        Dim key As String = koseiSubject.InstlHinban(columnIndex) & ":" & koseiSubject.InstlHinbanKbn(columnIndex) & ":" & koseiSubject.InstlDataKbn(columnIndex)
                        selectionInstlHinbanAndKbn.Add(key, columnIndex)
                    End If
                Next
            Next
            Dim fm As New Frm41KouseiSourceSelector(koseiSubject.NewSourceSelectorSubject(selectionInstlHinbanAndKbn), koseiObserver.InstlColumnCount, 1, shisakuEventCode)
            fm.ShowDialog()
            If fm.ResultOk = False Then
                Exit Sub
            End If


            'ここから'
            Dim jikyu As String = ""
            If StringUtil.Equals(cmbJikyuhinUmu.Text, "無") Then
                jikyu = "0"
            Else
                jikyu = "1"
            End If
            Try
                '単独の場合と複数の場合とすべての場合に分けるべき？'
                If selection.ColumnCount = 1 Then
                    koseiObserver.AssertValidateNewChange(selection.Column)
                Else
                    koseiObserver.AssertValidateNewChangeRange(selection.Column, selection.Column + selection.ColumnCount - 1)
                End If

            Catch ex As IllegalInputException
                errorController.SetBackColorOnError(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)

                msgBoxResult = frm01Kakunin.ConfirmYesNo("赤色部分の部品構成が存在しません。", "現在表示されている部品構成を残しますか？")

                errorController.ClearBackColor()
            End Try

            If Not fm.Sabun Then
                '最新化の場合
                koseiObserver.ClearSheetBackColor()
                Dim aBuhinKoseiMatrix As BuhinKoseiMatrix
                aBuhinKoseiMatrix = koseiSubject.NewMatrixBySpecified(msgBoxResult = msgBoxResult.Yes, fm.Results, jikyu, 0, koseiObserver.ConvSpreadColumnToKoseiInstl(selection.Column), selection.ColumnCount)

                ConfirmNewMatrix(aBuhinKoseiMatrix)


            Else
                '差分反映の場合
                Dim aBuhinKoseiMatrix As BuhinKoseiMatrix
                Dim index As Integer = 0
                aBuhinKoseiMatrix = koseiSubject.NewMatrixBySpecified(msgBoxResult = msgBoxResult.Yes, fm.Results, jikyu, 1, koseiObserver.ConvSpreadColumnToKoseiInstl(selection.Column), selection.ColumnCount, True)
                Dim orgBakMatrix As BuhinKoseiMatrix = koseiSubject.Matrix.Copy
                '比較元構成
                Dim motoBuhinKosei As BuhinKoseiMatrix = koseiSubject.Matrix.Copy4KoseiTenkaiTmpUse
                'motoBuhinKoseiの最後まで削除されなかったRecordの元々のindexを追跡する()
                'List.keyは最新のmotoBuhinKoseiのindexと同期、List.itemは元々のindexを表す
                Dim motoExistIndex As New List(Of Integer)
                For i As Integer = 0 To motoBuhinKosei.Records.Count - 1
                    motoExistIndex.Add(i)
                Next

                '比較元取得のため、選択列INSTL以外を消す
                Dim selectionFlg As Boolean
                For i As Integer = motoBuhinKosei.Records.Count - 1 To 0 Step -1
                    selectionFlg = False
                    For j As Integer = 0 To selection.ColumnCount - 1
                        If StringUtil.IsNotEmpty(motoBuhinKosei.InsuSuryo(i, koseiObserver.ConvSpreadColumnToKoseiInstl(selection.Column) + j)) Then
                            If CInt(motoBuhinKosei.InsuSuryo(i, koseiObserver.ConvSpreadColumnToKoseiInstl(selection.Column) + j)) > 0 Then
                                selectionFlg = True
                                Exit For
                            End If
                        End If
                    Next
                    If Not selectionFlg Then
                        motoBuhinKosei.RemoveRow(i)
                        motoExistIndex.RemoveAt(i)
                    End If
                Next
                '比較先構成
                Dim sakiBuhinKosei As BuhinKoseiMatrix = aBuhinKoseiMatrix
                '比較結果セット
                Dim ResultSet As New List(Of HoyouBuhinfrm41HikakuResultSelectorVo)
                Dim selectorVo As HoyouBuhinfrm41HikakuResultSelectorVo

                '比較元構成と比較先構成を比較し、比較結果にセット
                For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                    Dim sakiVo As BuhinKoseiRecordVo = sakiBuhinKosei.Record(sakiIndex)
                    Dim sakiInsuCount As Integer = 0
                    'FINALの場合
                    If sakiVo.Level = 0 Then
                        Continue For
                    Else
                        For Each columnIndex As Integer In sakiBuhinKosei.GetInputInsuColumnIndexes
                            If sakiBuhinKosei.InsuSuryo(sakiIndex, columnIndex) > 0 Then
                                sakiInsuCount = sakiInsuCount + sakiBuhinKosei.InsuSuryo(sakiIndex, columnIndex)
                            End If
                        Next
                    End If

                    Dim contain As Boolean = False
                    Dim motoInsuCount As Integer = 0
                    For Each motoIndex As Integer In motoBuhinKosei.GetInputRowIndexes()
                        Dim motoVo As BuhinKoseiRecordVo = motoBuhinKosei.Record(motoIndex)
                        If motoVo.Level = 0 Then
                            Continue For
                        Else
                            For Each columnIndex As Integer In motoBuhinKosei.GetInputInsuColumnIndexes
                                If motoBuhinKosei.InsuSuryo(motoIndex, columnIndex) > 0 Then
                                    motoInsuCount = motoInsuCount + motoBuhinKosei.InsuSuryo(motoIndex, columnIndex)
                                End If
                            Next
                        End If

                        If StringUtil.Equals(sakiVo.BuhinNo, motoVo.BuhinNo) Then
                            contain = True
                        End If
                    Next
                    '比較元構成に存在しない
                    If contain = False Then
                        selectorVo = New HoyouBuhinfrm41HikakuResultSelectorVo
                        selectorVo.BuhinKoseiRecordVo = sakiVo
                        selectorVo.Insu = sakiInsuCount
                        selectorVo.Flag = "A"
                        selectorVo.Kubun = "比較先"
                        selectorVo.MotoGamen = "Frm41DispShisakuBuhinEdit20"
                        ResultSet.Add(selectorVo)
                    End If
                Next
                '比較元構成と比較先構成を比較し、比較結果にセット
                For Each motoIndex As Integer In motoBuhinKosei.GetInputRowIndexes()
                    Dim motoInsuCount As Integer = 0
                    Dim motoVo As BuhinKoseiRecordVo = motoBuhinKosei.Record(motoIndex)
                    'FINALの場合
                    '↓↓2014/10/09 酒井 ADD BEGIN
                    '                If CheckFinalBuhin(motoVo.BuhinNo) Then
                    If motoVo.Level = 0 Then
                        '↑↑2014/10/09 酒井 ADD END
                        Continue For
                    Else
                        For Each columnIndex As Integer In motoBuhinKosei.GetInputInsuColumnIndexes
                            If motoBuhinKosei.InsuSuryo(motoIndex, columnIndex) > 0 Then
                                motoInsuCount = motoInsuCount + motoBuhinKosei.InsuSuryo(motoIndex, columnIndex)
                            End If
                        Next
                    End If

                    Dim contain As Boolean = False
                    For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                        Dim sakiVo As BuhinKoseiRecordVo = sakiBuhinKosei.Record(sakiIndex)
                        Dim sakiInsuCount As Integer = 0
                        'FINALの場合
                        '↓↓2014/10/09 酒井 ADD BEGIN
                        '                    If CheckFinalBuhin(sakiVo.BuhinNo) Then
                        If sakiVo.Level = 0 Then
                            '↑↑2014/10/09 酒井 ADD END
                            Continue For
                        Else
                            For Each columnIndex As Integer In sakiBuhinKosei.GetInputInsuColumnIndexes
                                If sakiBuhinKosei.InsuSuryo(sakiIndex, columnIndex) > 0 Then
                                    sakiInsuCount = sakiInsuCount + sakiBuhinKosei.InsuSuryo(sakiIndex, columnIndex)
                                End If
                            Next
                        End If

                        '比較元構成．部品番号＝比較先構成．部品番号の場合
                        If StringUtil.Equals(motoVo.BuhinNo, sakiVo.BuhinNo) Then
                            contain = True
                            If motoInsuCount = sakiInsuCount And _
                               motoVo.Level = sakiVo.Level And _
                               StringUtil.Equals(motoVo.ShukeiCode, sakiVo.ShukeiCode) And _
                               StringUtil.Equals(motoVo.SiaShukeiCode, sakiVo.SiaShukeiCode) And _
                               StringUtil.Equals(motoVo.GencyoCkdKbn, sakiVo.GencyoCkdKbn) And _
                               StringUtil.Equals(motoVo.MakerCode, sakiVo.MakerCode) And _
                               StringUtil.Equals(motoVo.BuhinNoKbn, sakiVo.BuhinNoKbn) And _
                               StringUtil.Equals(motoVo.BuhinNoKaiteiNo, sakiVo.BuhinNoKaiteiNo) And _
                               StringUtil.Equals(motoVo.EdaBan, sakiVo.EdaBan) Then
                                '完全一致の場合
                                selectorVo = New HoyouBuhinfrm41HikakuResultSelectorVo
                                selectorVo.BuhinKoseiRecordVo = sakiVo
                                selectorVo.Insu = sakiInsuCount
                                selectorVo.Flag = ""
                                selectorVo.Kubun = ""
                                selectorVo.MotoGamen = "Frm41DispShisakuBuhinEdit20"
                                ResultSet.Add(selectorVo)
                            Else
                                '部品番号以外で不一致の場合
                                selectorVo = New HoyouBuhinfrm41HikakuResultSelectorVo
                                selectorVo.BuhinKoseiRecordVo = sakiVo
                                selectorVo.Insu = sakiInsuCount
                                selectorVo.Flag = "C"
                                selectorVo.Kubun = "比較先"
                                selectorVo.MotoGamen = "Frm41DispShisakuBuhinEdit20"
                                ResultSet.Add(selectorVo)
                                selectorVo = New HoyouBuhinfrm41HikakuResultSelectorVo
                                selectorVo.BuhinKoseiRecordVo = motoVo
                                selectorVo.Insu = motoInsuCount
                                selectorVo.Flag = "C"
                                selectorVo.Kubun = "比較元"
                                selectorVo.MotoGamen = "Frm41DispShisakuBuhinEdit20"
                                ResultSet.Add(selectorVo)
                            End If
                            Exit For
                        End If
                    Next
                    '比較先構成に存在しない
                    If contain = False Then
                        selectorVo = New HoyouBuhinfrm41HikakuResultSelectorVo
                        selectorVo.BuhinKoseiRecordVo = motoVo
                        selectorVo.Insu = motoInsuCount
                        selectorVo.Flag = "D"
                        selectorVo.Kubun = "比較元"
                        selectorVo.MotoGamen = "Frm41DispShisakuBuhinEdit20"
                        ResultSet.Add(selectorVo)
                    End If
                Next
                '↓↓2014/10/22 酒井 ADD BEGIN
                'A→D→Cの順で画面表示する
                Dim ResultSetSort As New List(Of HoyouBuhinfrm41HikakuResultSelectorVo)
                For Each result As HoyouBuhinfrm41HikakuResultSelectorVo In ResultSet
                    If result.Flag = "A" Then
                        ResultSetSort.Add(result)
                    End If
                Next
                For Each result As HoyouBuhinfrm41HikakuResultSelectorVo In ResultSet
                    If result.Flag = "D" Then
                        ResultSetSort.Add(result)
                    End If
                Next
                For Each result As HoyouBuhinfrm41HikakuResultSelectorVo In ResultSet
                    If result.Flag = "C" Then
                        ResultSetSort.Add(result)
                    End If
                Next
                For Each result As HoyouBuhinfrm41HikakuResultSelectorVo In ResultSet
                    If result.Flag <> "A" And result.Flag <> "D" And result.Flag <> "C" Then
                        ResultSetSort.Add(result)
                    End If
                Next

                Using frm41HikakuResult As New HoyouBuhinFrm41HikakuResultSelector(ResultSetSort)
                    '↑↑2014/10/22 酒井 ADD END
                    frm41HikakuResult.ShowDialog()

                    If frm41HikakuResult.ResultOk = False Then
                        Exit Sub
                    End If

                    For Each resultVo As HoyouBuhinfrm41HikakuResultSelectorVo In frm41HikakuResult.ResultVos
                        'チェックあり
                        If resultVo.CheckedKbn = True Then
                            If resultVo.Kubun = "比較元" Then
                                '比較元構成の該当部品のLEVEL=1
                                For Each motoVo As BuhinKoseiRecordVo In motoBuhinKosei.Records
                                    '↓↓2014/10/14 酒井 ADD BEGIN
                                    'If motoVo.Equals(resultVo.BuhinKoseiRecordVo) Then
                                    If motoVo.Level = resultVo.BuhinKoseiRecordVo.Level And _
                                       StringUtil.Equals(motoVo.BuhinNo, resultVo.BuhinKoseiRecordVo.BuhinNo) And _
                                       StringUtil.Equals(motoVo.ShukeiCode, resultVo.BuhinKoseiRecordVo.ShukeiCode) And _
                                       StringUtil.Equals(motoVo.SiaShukeiCode, resultVo.BuhinKoseiRecordVo.SiaShukeiCode) And _
                                       StringUtil.Equals(motoVo.GencyoCkdKbn, resultVo.BuhinKoseiRecordVo.GencyoCkdKbn) And _
                                       StringUtil.Equals(motoVo.MakerCode, resultVo.BuhinKoseiRecordVo.MakerCode) And _
                                       StringUtil.Equals(motoVo.BuhinNoKbn, resultVo.BuhinKoseiRecordVo.BuhinNoKbn) And _
                                       StringUtil.Equals(motoVo.BuhinNoKaiteiNo, resultVo.BuhinKoseiRecordVo.BuhinNoKaiteiNo) And _
                                       StringUtil.Equals(motoVo.EdaBan, resultVo.BuhinKoseiRecordVo.EdaBan) Then
                                        '↑↑2014/10/14 酒井 ADD END
                                        motoVo.Level = 1
                                    End If
                                Next
                            ElseIf resultVo.Kubun = "比較先" Then
                                '比較先構成の該当部品のLEVEL=1
                                For Each sakiVo As BuhinKoseiRecordVo In sakiBuhinKosei.Records
                                    '↓↓2014/10/14 酒井 ADD BEGIN
                                    'If sakiVo.Equals(resultVo.BuhinKoseiRecordVo) Then
                                    If sakiVo.Level = resultVo.BuhinKoseiRecordVo.Level And _
                                       StringUtil.Equals(sakiVo.BuhinNo, resultVo.BuhinKoseiRecordVo.BuhinNo) And _
                                       StringUtil.Equals(sakiVo.ShukeiCode, resultVo.BuhinKoseiRecordVo.ShukeiCode) And _
                                       StringUtil.Equals(sakiVo.SiaShukeiCode, resultVo.BuhinKoseiRecordVo.SiaShukeiCode) And _
                                       StringUtil.Equals(sakiVo.GencyoCkdKbn, resultVo.BuhinKoseiRecordVo.GencyoCkdKbn) And _
                                       StringUtil.Equals(sakiVo.MakerCode, resultVo.BuhinKoseiRecordVo.MakerCode) And _
                                       StringUtil.Equals(sakiVo.BuhinNoKbn, resultVo.BuhinKoseiRecordVo.BuhinNoKbn) And _
                                       StringUtil.Equals(sakiVo.BuhinNoKaiteiNo, resultVo.BuhinKoseiRecordVo.BuhinNoKaiteiNo) And _
                                       StringUtil.Equals(sakiVo.EdaBan, resultVo.BuhinKoseiRecordVo.EdaBan) Then
                                        '↑↑2014/10/14 酒井 ADD END
                                        sakiVo.Level = 1
                                    End If
                                Next
                            Else
                                '比較元構成の該当部品のLEVEL=1
                                For Each motoVo As BuhinKoseiRecordVo In motoBuhinKosei.Records
                                    '↓↓2014/10/14 酒井 ADD BEGIN
                                    'If motoVo.Equals(resultVo.BuhinKoseiRecordVo) Then
                                    If motoVo.Level = resultVo.BuhinKoseiRecordVo.Level And _
                                       StringUtil.Equals(motoVo.BuhinNo, resultVo.BuhinKoseiRecordVo.BuhinNo) And _
                                       StringUtil.Equals(motoVo.ShukeiCode, resultVo.BuhinKoseiRecordVo.ShukeiCode) And _
                                       StringUtil.Equals(motoVo.SiaShukeiCode, resultVo.BuhinKoseiRecordVo.SiaShukeiCode) And _
                                       StringUtil.Equals(motoVo.GencyoCkdKbn, resultVo.BuhinKoseiRecordVo.GencyoCkdKbn) And _
                                       StringUtil.Equals(motoVo.MakerCode, resultVo.BuhinKoseiRecordVo.MakerCode) And _
                                       StringUtil.Equals(motoVo.BuhinNoKbn, resultVo.BuhinKoseiRecordVo.BuhinNoKbn) And _
                                       StringUtil.Equals(motoVo.BuhinNoKaiteiNo, resultVo.BuhinKoseiRecordVo.BuhinNoKaiteiNo) And _
                                       StringUtil.Equals(motoVo.EdaBan, resultVo.BuhinKoseiRecordVo.EdaBan) Then
                                        '↑↑2014/10/14 酒井 ADD END
                                        motoVo.Level = 1
                                    End If
                                Next
                                '比較先構成の該当部品を削除(.removecolumn)
                                For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                                    '↓↓2014/10/14 酒井 ADD BEGIN
                                    'If sakiBuhinKosei.Record(sakiIndex).Equals(resultVo.BuhinKoseiRecordVo) Then

                                    If sakiBuhinKosei.Record(sakiIndex).Level = resultVo.BuhinKoseiRecordVo.Level And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNo, resultVo.BuhinKoseiRecordVo.BuhinNo) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).ShukeiCode, resultVo.BuhinKoseiRecordVo.ShukeiCode) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).SiaShukeiCode, resultVo.BuhinKoseiRecordVo.SiaShukeiCode) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).GencyoCkdKbn, resultVo.BuhinKoseiRecordVo.GencyoCkdKbn) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).MakerCode, resultVo.BuhinKoseiRecordVo.MakerCode) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNoKbn, resultVo.BuhinKoseiRecordVo.BuhinNoKbn) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNoKaiteiNo, resultVo.BuhinKoseiRecordVo.BuhinNoKaiteiNo) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).EdaBan, resultVo.BuhinKoseiRecordVo.EdaBan) Then
                                        '↑↑2014/10/14 酒井 ADD END
                                        sakiBuhinKosei.RemoveRow(sakiIndex)
                                    End If
                                Next
                            End If
                        Else
                            If resultVo.Kubun = "比較元" Then
                                '比較元構成.removecolumn
                                For Each motoIndex As Integer In motoBuhinKosei.GetInputRowIndexes()
                                    '↓↓2014/10/14 酒井 ADD BEGIN
                                    'If motoBuhinKosei.Record(motoIndex).Equals(resultVo.BuhinKoseiRecordVo) Then
                                    If motoBuhinKosei.Record(motoIndex).Level = resultVo.BuhinKoseiRecordVo.Level And _
                          StringUtil.Equals(motoBuhinKosei.Record(motoIndex).BuhinNo, resultVo.BuhinKoseiRecordVo.BuhinNo) And _
                          StringUtil.Equals(motoBuhinKosei.Record(motoIndex).ShukeiCode, resultVo.BuhinKoseiRecordVo.ShukeiCode) And _
                          StringUtil.Equals(motoBuhinKosei.Record(motoIndex).SiaShukeiCode, resultVo.BuhinKoseiRecordVo.SiaShukeiCode) And _
                          StringUtil.Equals(motoBuhinKosei.Record(motoIndex).GencyoCkdKbn, resultVo.BuhinKoseiRecordVo.GencyoCkdKbn) And _
                          StringUtil.Equals(motoBuhinKosei.Record(motoIndex).MakerCode, resultVo.BuhinKoseiRecordVo.MakerCode) And _
                          StringUtil.Equals(motoBuhinKosei.Record(motoIndex).BuhinNoKbn, resultVo.BuhinKoseiRecordVo.BuhinNoKbn) And _
                          StringUtil.Equals(motoBuhinKosei.Record(motoIndex).BuhinNoKaiteiNo, resultVo.BuhinKoseiRecordVo.BuhinNoKaiteiNo) And _
                          StringUtil.Equals(motoBuhinKosei.Record(motoIndex).EdaBan, resultVo.BuhinKoseiRecordVo.EdaBan) Then
                                        '↑↑2014/10/14 酒井 ADD END
                                        motoBuhinKosei.RemoveRow(motoIndex)
                                        '↓↓2014/10/10 酒井 ADD BEGIN
                                        motoExistIndex.RemoveAt(motoIndex)
                                        '↑↑2014/10/10 酒井 ADD END
                                    End If
                                Next
                            ElseIf resultVo.Kubun = "比較先" Then
                                '比較先構成.removecolumn
                                For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                                    '↓↓2014/10/14 酒井 ADD BEGIN
                                    'If sakiBuhinKosei.Record(sakiIndex).Equals(resultVo.BuhinKoseiRecordVo) Then
                                    If sakiBuhinKosei.Record(sakiIndex).Level = resultVo.BuhinKoseiRecordVo.Level And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNo, resultVo.BuhinKoseiRecordVo.BuhinNo) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).ShukeiCode, resultVo.BuhinKoseiRecordVo.ShukeiCode) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).SiaShukeiCode, resultVo.BuhinKoseiRecordVo.SiaShukeiCode) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).GencyoCkdKbn, resultVo.BuhinKoseiRecordVo.GencyoCkdKbn) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).MakerCode, resultVo.BuhinKoseiRecordVo.MakerCode) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNoKbn, resultVo.BuhinKoseiRecordVo.BuhinNoKbn) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNoKaiteiNo, resultVo.BuhinKoseiRecordVo.BuhinNoKaiteiNo) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).EdaBan, resultVo.BuhinKoseiRecordVo.EdaBan) Then

                                        '↑↑2014/10/14 酒井 ADD END
                                        sakiBuhinKosei.RemoveRow(sakiIndex)
                                    End If
                                Next
                            Else
                                '比較元構成.removecolumn
                                For Each motoIndex As Integer In motoBuhinKosei.GetInputRowIndexes()
                                    '↓↓2014/10/14 酒井 ADD BEGIN
                                    'If motoBuhinKosei.Record(motoIndex).Equals(resultVo.BuhinKoseiRecordVo) Then
                                    If motoBuhinKosei.Record(motoIndex).Level = resultVo.BuhinKoseiRecordVo.Level And _
                          StringUtil.Equals(motoBuhinKosei.Record(motoIndex).BuhinNo, resultVo.BuhinKoseiRecordVo.BuhinNo) And _
                          StringUtil.Equals(motoBuhinKosei.Record(motoIndex).ShukeiCode, resultVo.BuhinKoseiRecordVo.ShukeiCode) And _
                          StringUtil.Equals(motoBuhinKosei.Record(motoIndex).SiaShukeiCode, resultVo.BuhinKoseiRecordVo.SiaShukeiCode) And _
                          StringUtil.Equals(motoBuhinKosei.Record(motoIndex).GencyoCkdKbn, resultVo.BuhinKoseiRecordVo.GencyoCkdKbn) And _
                          StringUtil.Equals(motoBuhinKosei.Record(motoIndex).MakerCode, resultVo.BuhinKoseiRecordVo.MakerCode) And _
                          StringUtil.Equals(motoBuhinKosei.Record(motoIndex).BuhinNoKbn, resultVo.BuhinKoseiRecordVo.BuhinNoKbn) And _
                          StringUtil.Equals(motoBuhinKosei.Record(motoIndex).BuhinNoKaiteiNo, resultVo.BuhinKoseiRecordVo.BuhinNoKaiteiNo) And _
                          StringUtil.Equals(motoBuhinKosei.Record(motoIndex).EdaBan, resultVo.BuhinKoseiRecordVo.EdaBan) Then
                                        '↑↑2014/10/14 酒井 ADD END
                                        motoBuhinKosei.RemoveRow(motoIndex)
                                        '↓↓2014/10/10 酒井 ADD BEGIN
                                        motoExistIndex.RemoveAt(motoIndex)
                                        '↑↑2014/10/10 酒井 ADD END
                                    End If
                                Next
                                '比較先構成.removecolumn
                                For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                                    '↓↓2014/10/14 酒井 ADD BEGIN
                                    'If sakiBuhinKosei.Record(sakiIndex).Equals(resultVo.BuhinKoseiRecordVo) Then
                                    If sakiBuhinKosei.Record(sakiIndex).Level = resultVo.BuhinKoseiRecordVo.Level And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNo, resultVo.BuhinKoseiRecordVo.BuhinNo) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).ShukeiCode, resultVo.BuhinKoseiRecordVo.ShukeiCode) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).SiaShukeiCode, resultVo.BuhinKoseiRecordVo.SiaShukeiCode) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).GencyoCkdKbn, resultVo.BuhinKoseiRecordVo.GencyoCkdKbn) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).MakerCode, resultVo.BuhinKoseiRecordVo.MakerCode) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNoKbn, resultVo.BuhinKoseiRecordVo.BuhinNoKbn) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNoKaiteiNo, resultVo.BuhinKoseiRecordVo.BuhinNoKaiteiNo) And _
                          StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).EdaBan, resultVo.BuhinKoseiRecordVo.EdaBan) Then
                                        '↑↑2014/10/14 酒井 ADD END
                                        sakiBuhinKosei.RemoveRow(sakiIndex)
                                    End If
                                Next
                            End If
                        End If
                    Next
                    '比較先構成のLEVEL=0の部品を削除（.removecolumn）
                    Dim delIndex As New List(Of Integer)
                    For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                        If sakiBuhinKosei.Record(sakiIndex).Level = 0 Then
                            delIndex.Add(sakiIndex)
                        End If
                    Next

                    For index = delIndex.Count - 1 To 0 Step -1
                        sakiBuhinKosei.RemoveRow(index)
                    Next

                    '空行を削除
                    index = 0
                    For Each record As BuhinKoseiRecordVo In sakiBuhinKosei.Records
                        If record.BuhinNo Is Nothing Then
                            sakiBuhinKosei.RemoveRow(index)
                        Else
                            index = index + 1
                        End If
                    Next
                    index = 0
                    For Each record As BuhinKoseiRecordVo In motoBuhinKosei.Records
                        If record.BuhinNo Is Nothing Then
                            motoBuhinKosei.RemoveRow(index)
                        Else
                            index = index + 1
                        End If
                    Next

                    '「比較元取得のため、選択列INSTL以外を消す」を戻す
                    For i As Integer = orgBakMatrix.Records.Count - 1 To 0 Step -1
                        selectionFlg = False
                        For j As Integer = 0 To selection.ColumnCount - 1
                            If StringUtil.IsNotEmpty(orgBakMatrix.InsuSuryo(i, koseiObserver.ConvSpreadColumnToKoseiInstl(selection.Column) + j)) Then
                                If CInt(orgBakMatrix.InsuSuryo(i, koseiObserver.ConvSpreadColumnToKoseiInstl(selection.Column) + j)) > 0 Then
                                    selectionFlg = True
                                    Exit For
                                End If
                            End If
                        Next
                        If Not selectionFlg Then
                            motoExistIndex.Add(i)
                        End If
                    Next
                    motoExistIndex.Sort()
                    Dim tmpMatrix As BuhinKoseiMatrix = koseiSubject.Matrix.CopyByIndex(motoExistIndex)
                    tmpMatrix.Insert(tmpMatrix.GetNewRowIndex, sakiBuhinKosei)
                    ConfirmNewMatrix(tmpMatrix, Nothing, orgBakMatrix)
                End Using
            End If
            koujichuFlag = True
        End Sub

        Private Sub Frm41DispShisakuBuhinEdit20_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'フィルタリングを全列に設定
            '       OptionFilterCommon.SetOptionFilter(spdParts.ActiveSheet, spdParts.ActiveSheet.ColumnCount - 1, 4)

            ' ［Ctrl］+［C］キーを無効とします
            Dim im As New FarPoint.Win.Spread.InputMap
            im = spdParts.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)
            ' ［Ctrl］+［X］キーを無効とします'
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.X, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)
            ' ［Ctrl］+［V］キーを無効とします'
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.V, Keys.Control), SpreadActions.None)

            '2014/10/07 追加 E.Ubukata
            'ペースト処理によるデータ変更イベント
            datamodel = CType(spdParts.ActiveSheet.Models.Data, FarPoint.Win.Spread.Model.DefaultSheetDataModel)

            With spdParts.ActiveSheet
                .Rows(.RowCount - 1).Locked = True
            End With
        End Sub


        ''↓↓2014/08/19 Ⅰ.2.管理項目追加_x) (TES)張 ADD BEGIN
        Private Sub spdParts_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellClick
            '作り方列にマウスカーソルを重ねるとメッセージを表示する
            If e.Column >= spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_SEISAKU).Index And _
                    e.Column <= spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KIBO).Index Then
                '↓↓2014/10/01 酒井 ADD BEGIN
                'LabelTsukurikataMsg.Visible = True
                '↑↑2014/10/01 酒井 ADD END
            Else
                LabelTsukurikataMsg.Visible = False
            End If
        End Sub
        ''↑↑2014/08/19 Ⅰ.2.管理項目追加_x) (TES)張 ADD END

        Private Sub spdParts_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdParts.Change

            Dim sheet As Spread.SheetView = spdParts.ActiveSheet
            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            '2014/10/07 追加 E.Ubukata
            If ParaActColIdx = 0 AndAlso sheet.Cells(ParaActRowIdx, ParaActColIdx).Value IsNot Nothing AndAlso sheet.Cells(ParaActRowIdx, ParaActColIdx).Value.Equals(0) Then
                EBom.Common.ComFunc.ShowErrMsgBox("レベルに0を設定することは出来ません。")
                sheet.Cells(ParaActRowIdx, ParaActColIdx).Value = Nothing
                Exit Sub
            End If

            ' 該当セルの文字色、文字太を変更する。
            sheet.SetStyleInfo(ParaActRowIdx, ParaActColIdx, CreateNewStyle())

        End Sub

        ''' <summary>
        ''' フォント色を青色に、文字を太くする。
        ''' </summary>
        ''' <remarks></remarks>
        Private Function CreateNewStyle() As Spread.StyleInfo
            Dim styleinfo As New Spread.StyleInfo
            styleinfo.ForeColor = Color.Blue '青色に
            styleinfo.Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
            Return styleinfo
        End Function

        Private Sub コピーToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles コピーToolStripMenuItem.Click
            'オプション・コピーサブルーチンへ
            OptionCopy()
        End Sub

        Private Sub コピーCToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles コピーCToolStripButton.Click
            'オプション・コピーサブルーチンへ
            OptionCopy()
        End Sub

        Private Sub 貼り付けToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 貼り付けToolStripMenuItem.Click
            'オプション・ペーストサブルーチンへ
            OptionPaste()
        End Sub

        Private Sub 貼り付けPToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 貼り付けPToolStripButton.Click
            'オプション・ペーストサブルーチンへ
            OptionPaste()
        End Sub

        Private Sub 切り取りToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            'オプション・カットサブルーチンへ
            OptionCut()
        End Sub

        Private Sub 切り取りUToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            'オプション・カットサブルーチンへ
            OptionCut()
        End Sub

        Private Sub Sequence1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Sequence1.Click
            Try
                ' 現在選択しているセル範囲を取得します。
                cr = spdParts.ActiveSheet.GetSelection(0)
                'アクティブセル列を非表示にします。
                'spdParts.ActiveSheet.Columns(cr.Column).Visible = False
                'spdParts.ActiveSheet.SetActiveCell(cr.Row, cr.Column + 1)
                '最終列を計算
                Dim w_Count As Integer = cr.ColumnCount + cr.Column - 1
                '選択しているアクティブセル列の非表示列を表示します。
                For i As Integer = cr.Column To w_Count
                    spdParts.ActiveSheet.Columns(i).Visible = False
                Next
            Catch Exception As Exception
                MessageBox.Show("選択されたセルがありません。")
                Exit Sub
            End Try
        End Sub

        Private Sub sequence2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sequence2.Click
            Try
                ' 現在選択しているセル範囲を取得します。
                cr = spdParts.ActiveSheet.GetSelection(0)
                '最終列を計算
                Dim w_Count As Integer = cr.ColumnCount + cr.Column - 1
                '選択しているアクティブセル列の非表示列を表示します。
                For i As Integer = cr.Column To w_Count
                    spdParts.ActiveSheet.Columns(i).Visible = True
                Next
            Catch Exception As Exception
                MessageBox.Show("選択されたセルがありません。")
                Exit Sub
            End Try
        End Sub

        Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
            Try
                ' 現在選択しているセル範囲を取得します。
                cr = spdParts.ActiveSheet.GetSelection(0)
                '最終列(備考列の列)を計算
                Dim w_Count As Integer = spdParts_Sheet1.Columns("BIKOU").Index
                '選択しているアクティブセル列の非表示列を表示します。
                For i As Integer = 0 To w_Count
                    spdParts.ActiveSheet.Columns(i).Visible = True
                Next
            Catch Exception As Exception
                MessageBox.Show("選択されたセルがありません。")
                Exit Sub
            End Try
        End Sub

        Private Sub HEADDW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HEADDW.Click
            w_HEAD -= 4
            If w_HEAD < 100 Then
                MsgBox("これ以上縮小できません。", MsgBoxStyle.Information, "アラーム")
                w_HEAD = 100
            End If
            'ヘッダーの行高を縮小します。
            spdParts.ActiveSheet.Rows(2).Height = w_HEAD
        End Sub

        Private Sub HEADUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HEADUP.Click
            w_HEAD += 4
            If w_HEAD > 250 Then
                MsgBox("これ以上拡大できません。", MsgBoxStyle.Information, "アラーム")
                w_HEAD = 250
            End If
            'ヘッダーの行高を縮小します。
            spdParts.ActiveSheet.Rows(2).Height = w_HEAD
        End Sub

        Private Sub UNdo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UNdo.Click
            Try
                ' 元に戻すことが出来るか確認します。
                If spdParts.UndoManager.CanUndo = True Then
                    spdParts.UndoManager.Undo()
                End If
            Catch Exception As Exception
                MessageBox.Show("元に戻す事が出来ません。")
                Exit Sub
            End Try
        End Sub

        Private Sub REdo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles REdo.Click
            Try
                ' やり直しが出来るか確認します。
                If spdParts.UndoManager.CanRedo = True Then
                    spdParts.UndoManager.Redo()
                End If
            Catch Exception As Exception
                MessageBox.Show("やり直しが出来ません。")
                Exit Sub
            End Try
        End Sub

#Region "ツールボタン(フィルタ解除)"
        ''' <summary>
        ''' ツールボタン(フィルタ解除)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub FilterCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

            'Try
            '    Cursor = Cursors.WaitCursor
            '    '指定列のフィルタリングを解除
            koseiObserver.ResetFilter(spdParts_Sheet1.ActiveColumn.Index, spdParts_Sheet1.ActiveColumn.Index2)

            'Catch ex As Exception
            '    MsgBox(String.Format("フィルタリング解除でシステムエラーが発生しました。({0})", ex.Message), MsgBoxStyle.Critical)
            'Finally
            '    Cursor = Cursors.Default
            'End Try
        End Sub

        ''' <summary>
        ''' ツールボタン(フィルタ解除(全部))
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub FilterCancelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterCancelAll.Click

            ''全列のフィルタリングを解除
            'For colIdx As Integer = 0 To spdParts.ActiveSheet.ColumnCount - 1
            ' '   OptionFilterCommon.SetFilterCancel(spdParts.ActiveSheet, colIdx, 5)
            'Next

            Try

                Cursor = Cursors.WaitCursor

                '全列のフィルタリングを解除
                koseiObserver.ResetFilterAll()

            Catch ex As Exception
                MsgBox(String.Format("フィルタリング解除でシステムエラーが発生しました。({0})", ex.Message), MsgBoxStyle.Critical)
            Finally
                Cursor = Cursors.Default
            End Try
            '自給品ボタン非表示の為、コメントアウト
            ''自給品をいったん表示しフラグによって続けて非表示にする。'
            'koseiObserver.JikyuRowVisible()
            'If JIKYU = "N" Then
            '    koseiObserver.JikyuRowDisable()
            'End If

        End Sub

#End Region

#Region "ツールボタン(フィルタ設定)"
        ''' <summary>
        ''' ツールボタン(フィルタ設定)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub SetFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetFilter.Click

            Try
                Cursor = Cursors.WaitCursor

                'フィルタ設定
                koseiObserver.SetFiltering()

            Catch ex As Exception
                MsgBox(String.Format("フィルタリング設定処理でシステムエラーが発生しました。({0})", ex.Message), MsgBoxStyle.Critical)
            Finally
                Cursor = Cursors.Default
            End Try

        End Sub

#End Region

#Region "ツールボタン（コピーペーストボタン対応)"

#Region "コピーイベント"
        ''' <summary>
        ''' コピーイベント
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub OptionCopy()
            Try

                Dim spd As FarPoint.Win.Spread.FpSpread = Me.spdParts

                spd.Focus()

                System.Threading.Thread.Sleep(10)
                System.Windows.Forms.SendKeys.Flush()

                'スプレッドにCTRL+Cキーを送信
                System.Windows.Forms.SendKeys.Send("^c")
                ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！

            Catch Exception As Exception
                MessageBox.Show("選択されたセルがありません。")
                Exit Sub
            End Try
        End Sub
#End Region

#Region "切取りイベント"
        ''' <summary>
        ''' 切取りイベント
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub OptionCut()

            Try
                Dim spd As FarPoint.Win.Spread.FpSpread = Me.spdParts

                spd.Focus()

                System.Threading.Thread.Sleep(10)

                System.Windows.Forms.SendKeys.Flush()

                'スプレッドにCTRL+xキーを送信
                System.Windows.Forms.SendKeys.Send("^x")
                ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！

            Catch Exception As Exception
                MessageBox.Show("選択されたセルがありません。")
                Exit Sub
            End Try

        End Sub

#End Region

#Region "貼りつけイベント"
        ''' <summary>
        ''' 貼りつけイベント
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub OptionPaste()
            Try

                Dim spd As FarPoint.Win.Spread.FpSpread = Me.spdParts

                spd.Focus()

                System.Windows.Forms.SendKeys.Flush()
                System.Threading.Thread.Sleep(10)
                'スプレッドにCTRL+vキーを送信
                System.Windows.Forms.SendKeys.Send("^v")
                ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！
            Catch Exception As Exception
                MessageBox.Show("選択されたセルがありません。")
                Exit Sub
            End Try
        End Sub

#End Region

#Region "キー押下イベント"
        ''' <summary>
        ''' キー押下イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdParts_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdParts.KeyDown

            Dim downKey As Object
            Dim sheet As Spread.SheetView = Me.spdParts_Sheet1
            Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)
            downKey = e.KeyCode()

            Select Case downKey

                Case Keys.C
                    '情報列タイトルの色をチェック
                    For i As Integer = 0 To sheet.ColumnCount - 1
                        '青色か？
                        If sheet.Cells(0, i, 2, i).ForeColor = Color.Blue Then
                            Exit Sub
                        End If
                    Next

                    'コントロールキーとCキーが押された
                    If (e.Modifiers And Keys.Control) = Keys.Control Then

                        '書式バックアップ
                        Dim listBln As List(Of Boolean()) = GetEditCellInfo(sheet)
                        '書式を一時的に全て保存編集対象にする
                        SetUndoCellFormat(sheet)
                        ''コピー
                        'sheet.ClipboardCopy()

                        ' 選択範囲を取得
                        Dim cr As FarPoint.Win.Spread.Model.CellRange() = spdParts.ActiveSheet.GetSelections()
                        If cr.Length > 0 Then
                            If cr(0).Row > -1 Then
                                If cr(0).Column = -1 OrElse cr(0).Column = 0 Then
                                    If spdParts.ActiveSheet.Cells(cr(0).Row, 0).Text = "0" Then
                                        ComFunc.ShowInfoMsgBox("0レベルはコピーできません")
                                        Clipboard.Clear()
                                        Exit Sub
                                    End If
                                End If
                            End If

                            '2012/02/16挿入貼り付けの判定'
                            copyRowCount = selection.RowCount
                            copyColumn = selection.Column
                            Dim data As [String] = Nothing
                            If cr(0).Row = -1 Then
                                ' 列単位で選択されている場合
                                For i As Integer = 0 To spdParts.ActiveSheet.RowCount - 1
                                    If spdParts.ActiveSheet.GetRowVisible(i) = True Then
                                        data += spdParts.ActiveSheet.GetClipValue(i, cr(0).Column, 1, cr(0).ColumnCount)
                                    End If
                                Next
                            Else
                                Dim count As Integer = 0
                                ' セル単位か行単位で選択されている場合
                                For i As Integer = 0 To cr(0).RowCount - 1

                                    If spdParts.ActiveSheet.GetRowVisible(cr(0).Row + i) = True Then
                                        '2012/01/28'
                                        'count = count + 1
                                        'data += spdParts.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                        If cr(0).Column = -1 AndAlso cr(0).ColumnCount = -1 Then
                                            ''
                                            count = count + 1
                                            data += spdParts.ActiveSheet.GetClipValue(cr(0).Row + i, 0, 1, spdParts.ActiveSheet.ColumnCount)
                                        Else
                                            count = count + 1
                                            '2013/05/14 １列で空白行を含む複数行選択した時、貼り付けると詰まってしまう現象回避する。
                                            If spdParts.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount) = "" Then
                                                data += "  " + vbCrLf  'ブランクの場合には改行をセットする。
                                            Else
                                                data += spdParts.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                            End If
                                        End If

                                        'If count < 1 Then
                                        '    count = count + 1
                                        '    data += spdParts.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)

                                        'Else
                                        '    '2012/01/27 複数コピーを可能にする'
                                        '    count = count + 1
                                        '    data += spdParts.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                        '    'MsgBox("複数の行に渡ってのコピーをすることはできません")
                                        '    'SetUndoCellFormat(sheet, listBln)
                                        '    'Return
                                        'End If
                                    End If
                                Next
                            End If

                            ' クリップボードに設定します
                            Clipboard.SetData(DataFormats.Text, data)
                        Else
                            Dim data As [String] = Nothing
                            data += spdParts.ActiveSheet.GetClipValue(sheet.ActiveRowIndex, sheet.ActiveColumnIndex, 1, 1)
                            Clipboard.SetData(DataFormats.Text, data)
                        End If

                        '書式を戻す
                        SetUndoCellFormat(sheet, listBln)

                    End If

                Case Keys.X
                    '20110604 樺澤 コピーする範囲と削除する範囲がまだ不完全'

                    '2012/02/21 協議の結果一時封印'

                    ''情報列タイトルの色をチェック
                    'For i As Integer = 0 To sheet.ColumnCount - 1
                    '    '青色か？
                    '    If sheet.Cells(0, i, 2, i).ForeColor = Color.Blue Then
                    '        'Exit Sub
                    '    End If
                    'Next

                    ''コントロールキーとXキーが押された
                    'If (e.Modifiers And Keys.Control) = Keys.Control Then

                    '    '書式バックアップ
                    '    Dim listBln As List(Of Boolean()) = GetEditCellInfo(sheet)
                    '    '書式を一時的に全て保存編集対象にする
                    '    SetUndoCellFormat(sheet)
                    '    ''コピー
                    '    'sheet.ClipboardCopy()

                    '    ' 選択範囲を取得
                    '    Dim cr As FarPoint.Win.Spread.Model.CellRange() = spdParts.ActiveSheet.GetSelections()
                    '    If cr.Length > 0 Then
                    '        '2012/02/16挿入貼り付けの判定'
                    '        copyRowCount = selection.RowCount
                    '        copyColumn = selection.Column
                    '        Dim data As [String] = Nothing
                    '        If cr(0).Row = -1 Then
                    '            ' 列単位で選択されている場合
                    '            For i As Integer = 0 To spdParts.ActiveSheet.ColumnCount - 1
                    '                If spdParts.ActiveSheet.GetRowVisible(i) = True Then
                    '                    data += spdParts.ActiveSheet.GetClipValue(cr(0).Row, i, cr(0).Row, i)
                    '                    spdParts_Sheet1.ClearRange(cr(0).Row, cr(0).Column + i, 1, 1, True)
                    '                End If
                    '            Next
                    '        Else
                    '            Dim count As Integer = 0
                    '            ' セル単位で選択されている場合
                    '            'If cr(0).RowCount > 1 Then
                    '            '    MsgBox("複数の行に渡っての切り取りをすることはできません")
                    '            '    SetUndoCellFormat(sheet, listBln)
                    '            '    Return
                    '            'End If


                    '            For i As Integer = 0 To cr(0).RowCount - 1

                    '                If spdParts.ActiveSheet.GetRowVisible(cr(0).Row + i) = True Then
                    '                    data += spdParts.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                    '                    '5/18 樺澤
                    '                    '対象セルの中身を削除
                    '                    spdParts_Sheet1.ClearRange(cr(0).Row + i, cr(0).Column, 1, 1, True)
                    '                    koseiSubject.ClearRow(cr(0).Row + i, 1)
                    '                End If
                    '            Next
                    '        End If

                    '        ' クリップボードに設定します
                    '        Clipboard.SetData(DataFormats.Text, data)
                    '    End If

                    '    '書式を戻す
                    '    SetUndoCellFormat(sheet, listBln)

                    'End If

                Case Keys.V

                    '情報列タイトルの色をチェック
                    For i As Integer = 0 To sheet.ColumnCount - 1
                        '青色か？
                        If sheet.Cells(0, i, 2, i).ForeColor = Color.Blue Then
                            Exit Sub
                        End If
                    Next

                    '行選択ではコントロールキーとVキーは無効に
                    'If selection.Column = -1 AndAlso selection.ColumnCount - 1 Then
                    '    e.Handled = True
                    'Else
                    'コントロールキーとVキーが押された
                    If (e.Modifiers And Keys.Control) = Keys.Control Then

                        Dim listClip As New List(Of String())

                        listClip = GetClipbordList()

                        If Not listClip Is Nothing Then

                            Dim selColumnCount As Integer = 0
                            Dim selCol As Integer = 0

                            If Not selection Is Nothing Then
                                If selection.Column = -1 AndAlso selection.ColumnCount - 1 Then
                                    selColumnCount = spdParts.ActiveSheet.ColumnCount
                                Else
                                    selColumnCount = selection.ColumnCount
                                    selCol = selection.Column
                                End If
                            Else
                                selCol = sheet.ActiveColumnIndex
                                selColumnCount = 1
                            End If

                            If selCol = 0 Then
                                If listClip.Item(0)(0).Equals("0") Then
                                    EBom.Common.ComFunc.ShowErrMsgBox("レベルに0を設定することは出来ません。")
                                    Exit Sub
                                End If
                            End If



                            Dim rowCount As Integer = listClip.Count - 1
                            Dim colCount As Integer = listClip(0).Length

                            '2012/02/01'
                            'スプレッド自身に貼り付けさせる'
                            spdParts.ClipboardOptions = ClipboardOptions.NoHeaders
                            'Dim im As New InputMap
                            'im.Put(New Keystroke(Keys.V, Keys.Control), SpreadActions.ClipboardPasteValues)
                            spdParts_Sheet1.ClipboardPaste()


                            'セル編集モード時にコピーした場合、以下を行う。
                            If rowCount = 0 Then
                                rowCount = 1
                            End If

                            '行選択時
                            If Not selection Is Nothing Then
                                If selection.Column = -1 Then

                                    '貼りつけ対象のセルを編集済みとし書式を設定する
                                    Me.spdParts_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                                    Me.spdParts_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).ForeColor = Color.Blue

                                Else

                                    'If (selection.Column + colCount) >= sheet.ColumnCount - 1 Then
                                    '    EBom.Common.ComFunc.ShowInfoMsgBox("貼り付けようとしている範囲がスプレッド表の最大列を超えています")
                                    '    Return
                                    'End If

                                    If (selection.Row + rowCount) >= sheet.RowCount - 1 Then
                                        EBom.Common.ComFunc.ShowInfoMsgBox("貼り付けしようとしている範囲がスプレッド表の最大行を超えています")
                                        Return
                                    End If

                                    '貼りつけ対象のセルを編集済みとし書式を設定する
                                    Me.spdParts_Sheet1.Cells(selection.Row, selCol, selection.Row + rowCount - 1, _
                                                           selection.Column + colCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

                                    Me.spdParts_Sheet1.Cells(selection.Row, selCol, selection.Row + rowCount - 1, _
                                                    selCol + colCount - 1).ForeColor = Color.Blue
                                End If
                            Else
                                '貼りつけ対象のセルを編集済みとし書式を設定する
                                Me.spdParts_Sheet1.Cells(sheet.ActiveRowIndex, selCol, sheet.ActiveRowIndex, _
                                                       sheet.ActiveColumnIndex).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

                                Me.spdParts_Sheet1.Cells(sheet.ActiveRowIndex, selCol, sheet.ActiveRowIndex, _
                                                       sheet.ActiveColumnIndex).ForeColor = Color.Blue
                            End If
                        End If
                    End If

                    'End If

                Case Keys.Delete
                    '行選択ではDeleteは無効に
                    If Not selection Is Nothing Then
                        If selection.Column = -1 AndAlso selection.ColumnCount - 1 Then
                            e.Handled = True
                        End If
                        '2012/03/08'
                        'フィルタが掛かってるところは削除対象外'
                        Dim row As Integer = selection.Row
                        Dim col As Integer = selection.Column
                        Dim colcount As Integer = selection.ColumnCount
                        Dim rowcount As Integer = selection.RowCount


                        For rowindex As Integer = row To row + rowcount - 1
                            'フィルタ中の行は対象外'
                            If sheet.Rows(rowindex).Visible Then
                                'レベル０行も対象外'

                                If koseiSubject.Level(rowindex - 4) <> 0 Then
                                    'タイトル行も対象外'
                                    If row > 3 Then
                                        For colindex As Integer = col To col + colcount - 1
                                            sheet.ClearRange(rowindex, colindex, 1, 1, True)
                                            'koseiSubject.ClearCell(row, col)
                                            '------------------------------------------------------------------------
                                            '2012/07/25　柳沼
                                            '   フィルタで非表示行の値がクリアされてしまうので
                                            '   無効にする。
                                            e.Handled = True
                                            '------------------------------------------------------------------------
                                        Next
                                    End If
                                End If
                            End If
                        Next
                    Else
                        Dim row As Integer = sheet.ActiveRowIndex
                        Dim col As Integer = sheet.ActiveColumnIndex
                        Dim colcount As Integer = 1
                        Dim rowcount As Integer = 1


                        For rowindex As Integer = row To row + rowcount - 1
                            'フィルタ中の行は対象外'
                            If sheet.Rows(rowindex).Visible Then
                                'レベル０行も対象外'
                                If koseiSubject.Level(rowindex - 4) <> 0 Then
                                    'タイトル行も対象外'
                                    If row > 3 Then
                                        For colindex As Integer = col To col + colcount - 1
                                            sheet.ClearRange(rowindex, colindex, 1, 1, True)
                                            'koseiSubject.ClearCell(row, col)
                                        Next
                                    End If
                                End If
                            End If
                        Next
                        e.Handled = True
                    End If

            End Select
        End Sub
        Private Sub spdParts_ClipboardPasting(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ClipboardPastingEventArgs) Handles spdParts.ClipboardPasting
        End Sub




#End Region

#Region "クリップボードの内容をstring()型のリストに格納し返す"
        Public Shared Function GetClipbordList() As List(Of String())
            Dim listStr As New List(Of String())

            'システムクリップボードにあるデータを取得します
            Dim iData As IDataObject = Clipboard.GetDataObject()

            Dim strRow() As String

            'テキスト形式データの判断
            If iData.GetDataPresent(DataFormats.Text) = False Then
                Return Nothing
            Else

                Console.WriteLine(CType(iData.GetData(DataFormats.Text), String))
                strRow = CType(iData.GetData(DataFormats.Text), String).Split(vbCrLf)

            End If

            For i As Integer = 0 To strRow.Length - 1
                Dim strChar() As String = strRow(i).Split(vbTab)
                listStr.Add(strChar)
            Next

            Return listStr

        End Function

#End Region

#Region "編集書済式有無のセル配列を返す"
        ''' <summary>
        ''' 編集済書式有無のセル配列を返す
        ''' </summary>
        ''' <param name="aSheet">対象シートをセットする</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetEditCellInfo(ByVal aSheet As SheetView) As List(Of Boolean())

            Dim selection As FarPoint.Win.Spread.Model.CellRange = aSheet.GetSelection(0)

            Dim listBln As New List(Of Boolean())

            'selectionが空の場合があるので'
            If Not selection Is Nothing Then
                For i As Integer = 0 To selection.RowCount - 1

                    Dim blnTbl() As Boolean = Nothing
                    Dim colCnt As Integer = 0
                    Dim col As Integer = 0
                    If selection.ColumnCount = -1 Then
                        colCnt = aSheet.ColumnCount
                        col = 0
                    Else
                        colCnt = selection.ColumnCount
                        col = selection.Column
                    End If

                    ReDim Preserve blnTbl(colCnt - 1)

                    For j As Integer = 0 To colCnt - 1
                        Dim objFont As System.Drawing.Font = aSheet.Cells(selection.Row + i, col + j).Font

                        '太字Cellを編集済セルと判定
                        If Not objFont Is Nothing AndAlso objFont.Bold = True Then
                            blnTbl(j) = True
                        Else
                            blnTbl(j) = False
                        End If

                    Next
                    listBln.Add(blnTbl)
                Next
            Else

                Dim blnTbl() As Boolean = Nothing
                Dim colCnt As Integer = 0
                Dim col As Integer = 0
                colCnt = 1
                col = aSheet.ActiveColumnIndex
                ReDim Preserve blnTbl(colCnt - 1)

                For j As Integer = 0 To colCnt - 1
                    Dim objFont As System.Drawing.Font = aSheet.Cells(aSheet.ActiveRowIndex, aSheet.ActiveColumnIndex).Font

                    '太字Cellを編集済セルと判定
                    If Not objFont Is Nothing AndAlso objFont.Bold = True Then
                        blnTbl(j) = True
                    Else
                        blnTbl(j) = False
                    End If

                Next
                listBln.Add(blnTbl)
            End If



            Return listBln

        End Function

#End Region

#Region "コピーの時に一時的に編集済書式を設定する、また設定した書式を元に戻す"
        ''' <summary>
        ''' コピーの時に一時的に書式を設定する、また設定した書式を元に戻す
        ''' 
        ''' この処理はCTRL+cでの貼りつけの場合書式と値がコピーされてしまうため、単純操作では
        ''' 貼付け先に編集済みの書式が設定出来ません。
        ''' この問題に対応する為に、編集済み書式をCTRL+Cを送信する前に設定し、
        ''' 送信後に元の書式にするという対応が必要になります。
        ''' 
        ''' そもそも、こんな面倒な事が必要な原因は
        ''' 
        ''' コードで"spdParts_Sheet1.ClipboardPaste"と単純に記述されて実行された操作は
        ''' Undo操作が一切対象外になるという事が原因です。
        ''' 
        ''' Undoを行うにはキーボードからCTRL+Xなどの操作をコードから行う必要があり
        ''' SendKeyの様なコードが記述されています。
        ''' 
        ''' 
        ''' </summary>
        ''' <param name="aSheet">対象シート</param>
        ''' <param name="alistBln">書式を全て編集済書式にするときは指定しない</param>
        ''' <remarks></remarks>
        Public Shared Sub SetUndoCellFormat(ByVal aSheet As SheetView, Optional ByVal alistBln As List(Of Boolean()) = Nothing)

            Dim selection As FarPoint.Win.Spread.Model.CellRange = aSheet.GetSelection(0)
            Dim colCnt As Integer = 0
            Dim col As Integer = 0

            '2012/03/12 selectionが無い場合があるので'
            If Not selection Is Nothing Then
                If selection.ColumnCount = -1 Then
                    colCnt = aSheet.ColumnCount
                    col = 0
                Else
                    colCnt = selection.ColumnCount
                    col = selection.Column
                End If
            Else
                colCnt = 1
                col = aSheet.ActiveColumnIndex
            End If




            '無い場合は全て保存対象編集書式とするため全てTrueをセット
            If alistBln Is Nothing Then
                alistBln = New List(Of Boolean())
                If Not selection Is Nothing Then
                    For i As Integer = 0 To selection.RowCount - 1
                        Dim blnTbl() As Boolean = Nothing
                        ReDim Preserve blnTbl(colCnt - 1)

                        For j As Integer = 0 To colCnt - 1
                            blnTbl(j) = True
                        Next
                        alistBln.Add(blnTbl)
                    Next
                Else
                    Dim blnTbl() As Boolean = Nothing
                    ReDim Preserve blnTbl(colCnt - 1)

                    For j As Integer = 0 To colCnt - 1
                        blnTbl(j) = True
                    Next
                    alistBln.Add(blnTbl)
                End If
            End If

            '受け取ったListの内容で書式を設定
            If Not selection Is Nothing Then
                For i As Integer = 0 To selection.RowCount - 1
                    For j As Integer = 0 To selection.ColumnCount - 1
                        If alistBln(i)(j) = False Then
                            aSheet.Cells(selection.Row + i, selection.Column + j).ForeColor = Nothing
                            aSheet.Cells(selection.Row + i, selection.Column + j).Font = Nothing
                        Else
                            aSheet.Cells(selection.Row + i, selection.Column + j).ForeColor = Color.Blue
                            aSheet.Cells(selection.Row + i, selection.Column + j).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                        End If
                    Next
                Next
            Else
                If alistBln(0)(0) = False Then
                    aSheet.Cells(aSheet.ActiveRowIndex, aSheet.ActiveColumnIndex).ForeColor = Nothing
                    aSheet.Cells(aSheet.ActiveRowIndex, aSheet.ActiveColumnIndex).Font = Nothing
                Else
                    aSheet.Cells(aSheet.ActiveRowIndex, aSheet.ActiveColumnIndex).ForeColor = Color.Blue
                    aSheet.Cells(aSheet.ActiveRowIndex, aSheet.ActiveColumnIndex).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                End If
            End If

        End Sub
#End Region

#End Region

#Region "ツールボタン(ソート)"
        ''' <summary>
        ''' ソートを行う
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Sort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Sort.Click
            'ソート用の画面を開く'
            Dim fms As New frm41Sort()
            fms.ShowDialog()
            'If fm.ResultOk = False Then
            '    Exit Sub
            'End If
            '状態を受け取る'
            Dim Conditions1 As String = fms.ComboBox1.Text
            'Dim Conditions2 As String = fms.ComboBox2.Text
            'Dim Conditions3 As String = fms.ComboBox3.Text
            Dim order1 As Boolean = fms.RadioButton1.Checked
            'Dim order2 As Boolean = fms.RadioButton3.Checked
            'Dim order3 As Boolean = fms.RadioButton5.Checked

            '状態を渡してソート処理'
            koseiSubject.IsViewerMode = True
            koseiSubject.SortMatrix(Conditions1, order1)
            'koseiSubject.SortMatrix(Conditions1, order1, Conditions2, order2, Conditions3, order3)
            koseiSubject.IsViewerMode = False
            koseiSubject.NotifyObservers()

        End Sub
#End Region


        Private Sub 子部品展開ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 子部品展開ToolStripMenuItem.Click
            '子部品を展開する。
            Dim sheet As Spread.SheetView = spdParts.ActiveSheet

            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            Dim wBuhinNo As String = sheet.Cells(ParaActRowIdx, ParaActColIdx).Value

            'タイトル行を取得
            Dim titleRows As Integer = BuhinEditSpreadUtil.GetTitleRows(sheet)
            '自給品の有無を取得'
            Dim jikyu As String = ""
            If StringUtil.Equals(cmbJikyuhinUmu.Text, "有") Then
                jikyu = "D"
            Else
                jikyu = "N"
            End If


            koseiSubject.JikyuSet(jikyu)
            '部品構成情報を表示
            koseiSubject.BuhinNoBom(sheet.ActiveRowIndex - titleRows) = wBuhinNo

            'シートの変更後セルのフォントをデフォルトに戻してから、
            '部品構成情報を挿入した行の背景色を変更
            koseiObserver.ClearSheetBackColor()
            koseiObserver.UpdateBackColor(SpdKoseiObserver.SPREAD_ROW + titleRows - 1, SpdKoseiObserver.SPREAD_ROWCOUNT)

            '↓↓2014/10/06 酒井 ADD BEGIN
            koseiSubject.CallSetChanged()
            koseiSubject.NotifyObservers(Nothing)
            '↑↑2014/10/06 酒井 ADD END
        End Sub

        Private Sub spdParts_EditModeOn(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdParts.EditModeOn
            Dim sheet As Spread.SheetView = spdParts_Sheet1
            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            '↓↓↓2014/12/25 メタル項目を追加 TES)張 CHG BEGIN
            ''↓↓2014/09/05 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            'If ParaActColIdx = sheet.Columns(SpdKoseiObserver.TAG_MAKER_NAME).Index _
            '  Or ParaActColIdx = sheet.Columns(SpdKoseiObserver.TAG_BIKOU).Index _
            '  Or ParaActColIdx = sheet.Columns(SpdKoseiObserver.TAG_BUHIN_NOTE).Index Then
            'If ParaActColIdx = sheet.Columns(SpdKoseiObserver.TAG_MAKER_NAME).Index _
            '  Or ParaActColIdx = sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KIBO).Index _
            '  Or ParaActColIdx = sheet.Columns(SpdKoseiObserver.TAG_BIKOU).Index _
            '  Or ParaActColIdx = sheet.Columns(SpdKoseiObserver.TAG_BUHIN_NOTE).Index Then
            ''↑↑2014/09/05 Ⅰ.2.管理項目追加 酒井 ADD END
            If ParaActColIdx = sheet.Columns(SpdKoseiObserver.TAG_MAKER_NAME).Index _
              Or ParaActColIdx = sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KIBO).Index _
              Or ParaActColIdx = sheet.Columns(SpdKoseiObserver.TAG_BIKOU).Index _
              Or ParaActColIdx = sheet.Columns(SpdKoseiObserver.TAG_BUHIN_NOTE).Index _
              Or ParaActColIdx = sheet.Columns(SpdKoseiObserver.TAG_DATA_ITEM_KAITEI_INFO).Index Then
                '↑↑↑2014/12/25 メタル項目を追加 TES)張 CHG END
                'IMEを使用可能にする。
                spdParts.EditingControl.ImeMode = Windows.Forms.ImeMode.NoControl
                spdParts.ImeMode = Windows.Forms.ImeMode.NoControl
            Else
                'IMEを使用不可能にする。
                spdParts.EditingControl.ImeMode = Windows.Forms.ImeMode.Disable
                spdParts.ImeMode = Windows.Forms.ImeMode.Disable
            End If
        End Sub
        Private Sub spdParts_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdParts.EditModeOff
            spdParts.EditingControl.ImeMode = Windows.Forms.ImeMode.NoControl
            spdParts.ImeMode = Windows.Forms.ImeMode.NoControl
        End Sub
        ''' <summary>
        ''' スプレッドの表示を制御する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub viewLockForViewMode()

            ''コントロールをロックする
            '2013/05/14 便利ツールは使用できる。（ＡＬ画面では使用できるので合わせる。）
            '           但し、コピー＆ペーストは使用できない。
            Me.ToolStrip1.Enabled = True
            Me.コピーCToolStripButton.Enabled = False
            Me.貼り付けPToolStripButton.Enabled = False

            msBuhinKouseiIchiran.Enabled = False
            Me.ToolStripMenuItem.Enabled = True
            Me.msBuhinKouseiIchiran.Enabled = True
            Me.msBuhinKouseiYobidashi.Enabled = False
            Me.msKyoukuA.Enabled = False
            Me.TxtInputSupport.Enabled = False
            Me.BtnKoseiTenkai.Enabled = False
            cmbJikyuhinUmu.Enabled = False

            For i As Integer = 0 To spdParts_Sheet1.RowCount - 1
                For j As Integer = 0 To spdParts_Sheet1.ColumnCount - 1
                    spdParts_Sheet1.Cells(i, j).Locked = True
                Next
            Next
        End Sub

        ''' <summary>
        ''' 供給セクションを振る
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub msKyoukuA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msKyoukuA.Click

            Dim result As DialogResult = MsgBox("集計コードAに対して供給セクション「9SH10」を振ります。よろしいですか？", MsgBoxStyle.OkCancel)

            If result = 1 Then
                koseiSubject.setKyouku()
                koseiSubject.NotifyObservers()
            End If


        End Sub


        Private copyRowCount As Integer
        Private copyColumn As Integer
        ''' <summary>
        ''' 挿入貼り付け
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub 挿入貼り付けToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 挿入貼り付けToolStripMenuItem.Click
            '行挿入と貼り付け'
            Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)
            Dim wRowCount As Integer = selection.RowCount

            If SpreadUtil.IsSelectedRow(selection) Then
                koseiObserver.InsertRows(selection.Row, copyRowCount)
            End If
            '挿入後、貼り付けを行う'
            OptionPaste()


        End Sub

        Private Sub cmbJikyuhinUmu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbJikyuhinUmu.SelectedIndexChanged
            ''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
            If StringUtil.Equals(cmbJikyuhinUmu.Text, "無") Then
                koseiSubject.JikyuFlg = False
            Else
                koseiSubject.JikyuFlg = True
            End If
            ''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
        End Sub

        ''' <summary>
        ''' 3Dビュー
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub View3DMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles View3DMenuItem.Click

            Dim ranges As FarPoint.Win.Spread.Model.CellRange = spdParts_Sheet1.GetSelection(0)

            If ranges.Row = -1 And ranges.RowCount = -1 Then

                koseiSubject.callXVL(Me, ranges)

            Else
                MessageBox.Show("表示する員数列を選択してください。", "3D表示", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If


        End Sub

        ''↓↓2014/08/05 Ⅰ.2.管理項目追加_bx) (TES)張 ADD BEGIN
        Private Sub spdParts_ComboDropDown(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles spdParts.ComboDropDown
            Dim sheet As Spread.SheetView = spdParts_Sheet1

            Dim isKatashiyou As Boolean = e.Column = sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU1).Index Or _
                    e.Column = sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU2).Index Or _
                    e.Column = sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU3).Index

            ' （型仕様1～3コンボ）のコンボドロップダウン表示イベント
            If isKatashiyou Then
                Dim katashiyou1 As String = sheet.Cells(e.Row, sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU1).Index).Value
                Dim katashiyou2 As String = sheet.Cells(e.Row, sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU2).Index).Value
                Dim katashiyou3 As String = sheet.Cells(e.Row, sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU3).Index).Value
                Dim dropCell = spdParts_Sheet1.Cells(e.Row, e.Column)
                Dim fc As FpCombo = e.EditingControl

                ' セル幅を全角１５文字程度に拡張する。
                'dropCell.Column.Width = LARGER_WIDTH
                fc.Width = LARGER_WIDTH

                ' 型仕様1～3の内、他で選択済みの項目を選択不可にする。
                Select Case e.Column
                    Case sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU1).Index
                        If Not String.IsNullOrEmpty(katashiyou2) Then
                            fc.ItemData.Remove(katashiyou2)
                            fc.List.Remove(katashiyou2)
                        End If
                        If Not String.IsNullOrEmpty(katashiyou3) Then
                            fc.ItemData.Remove(katashiyou3)
                            fc.List.Remove(katashiyou3)
                        End If

                    Case sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU2).Index
                        If Not String.IsNullOrEmpty(katashiyou1) Then
                            fc.ItemData.Remove(katashiyou1)
                            fc.List.Remove(katashiyou1)
                        End If
                        If Not String.IsNullOrEmpty(katashiyou3) Then
                            fc.ItemData.Remove(katashiyou3)
                            fc.List.Remove(katashiyou3)
                        End If
                    Case sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU3).Index
                        If Not String.IsNullOrEmpty(katashiyou1) Then
                            fc.ItemData.Remove(katashiyou1)
                            fc.List.Remove(katashiyou1)
                        End If
                        If Not String.IsNullOrEmpty(katashiyou2) Then
                            fc.ItemData.Remove(katashiyou2)
                            fc.List.Remove(katashiyou2)
                        End If
                    Case Else
                        ' なりえない
                End Select


            End If
        End Sub
        Private Sub spdParts_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles spdParts.Validating
            Dim sheet As Spread.SheetView = spdParts_Sheet1
            Dim shiyou1Value As String = String.Empty
            Dim shiyou2Value As String = String.Empty
            Dim shiyou3Value As String = String.Empty

            ParaActRowIdx = sheet.ActiveRowIndex
            ParaActColIdx = sheet.ActiveColumnIndex

            Select Case ParaActColIdx
                Case sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU1).Index
                    shiyou1Value = sheet.Cells(ParaActRowIdx, ParaActColIdx).Text
                    shiyou2Value = sheet.Cells(ParaActRowIdx, ParaActColIdx + 1).Text
                    shiyou3Value = sheet.Cells(ParaActRowIdx, ParaActColIdx + 2).Text
                    If String.IsNullOrEmpty(shiyou2Value) = False Then
                        If shiyou1Value = shiyou2Value Then
                            e.Cancel = True
                        End If
                    End If
                    If String.IsNullOrEmpty(shiyou3Value) = False Then
                        If shiyou1Value = shiyou3Value Then
                            e.Cancel = True
                        End If
                    End If
                Case sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU2).Index
                    shiyou1Value = sheet.Cells(ParaActRowIdx, ParaActColIdx - 1).Text
                    shiyou2Value = sheet.Cells(ParaActRowIdx, ParaActColIdx).Text
                    shiyou3Value = sheet.Cells(ParaActRowIdx, ParaActColIdx + 1).Text
                    If String.IsNullOrEmpty(shiyou1Value) = False Then
                        If shiyou2Value = shiyou1Value Then
                            e.Cancel = True
                        End If
                    End If
                    If String.IsNullOrEmpty(shiyou3Value) = False Then
                        If shiyou2Value = shiyou3Value Then
                            e.Cancel = True
                        End If
                    End If
                Case sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU3).Index
                    shiyou1Value = sheet.Cells(ParaActRowIdx, ParaActColIdx - 2).Text
                    shiyou2Value = sheet.Cells(ParaActRowIdx, ParaActColIdx - 1).Text
                    shiyou3Value = sheet.Cells(ParaActRowIdx, ParaActColIdx).Text
                    If String.IsNullOrEmpty(shiyou1Value) = False Then
                        If shiyou3Value = shiyou1Value Then
                            e.Cancel = True
                        End If
                    End If
                    If String.IsNullOrEmpty(shiyou2Value) = False Then
                        If shiyou3Value = shiyou2Value Then
                            e.Cancel = True
                        End If
                    End If
            End Select

        End Sub
        ''↑↑2014/08/05 Ⅰ.2.管理項目追加_bx) (TES)張 ADD END

        ''↓↓2014/08/05 Ⅰ.2.管理項目追加_by) (TES)張 ADD BEGIN
        Private Sub spdParts_ComboCloseUp(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles spdParts.ComboCloseUp
            Dim sheet As Spread.SheetView = spdParts_Sheet1

            Dim isKatashiyou As Boolean = e.Column = sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU1).Index Or _
            e.Column = sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU2).Index Or _
            e.Column = sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU3).Index

            ' 幅を元に戻る
            If isKatashiyou Then
                e.EditingControl.Width = sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU1).Width
            End If
        End Sub
        ''↑↑2014/08/05 Ⅰ.2.管理項目追加_by) (TES)張 ADD END

        ''↓↓2014/08/05 2集計コード R/Yのブロック間紐付け d) (TES)施 ADD BEGIN
        Private Sub EBOM最新構成閲覧ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EBOM最新構成閲覧ToolStripMenuItem.Click

            ' If アクティブセルが部品番号列の場合 Then sxc

            If spdParts.ActiveSheet.ActiveCell.Column.Tag = "BUHIN_NO" Then
                Dim activeCell As String = spdParts.ActiveSheet.ActiveCell.Value
                '試作開発符号
                Dim shisakuEventDao As TShisakuEventDao = New TShisakuEventDaoImpl
                Dim shisakuFugo As String = shisakuEventDao.FindByPk(shisakuEventCode).ShisakuKaihatsuFugo

                Dim frm As Frm41DispKosei = New Frm41DispKosei()
                Dim makeDao As MakeStructureResultDao = New MakeStructureResultDaoImpl
                Dim rhac0552Vos As List(Of Rhac0552Vo) = makeDao.FindStructure0552ByBuhinNoOya(activeCell)
                If rhac0552Vos.Count > 0 Then
                    frm.setrhac0552(activeCell, rhac0552Vos)
                Else
                    'select 試作開発符号 from T_SHISAKU_EVENT where SHISAKU_EVENT_CODE=画面選択状態のイベントコード																																																																							

                    Dim rhac0553Vos As List(Of Rhac0553Vo) = makeDao.FindStructure0553ByBuhinNoOya(activeCell, shisakuFugo)
                    If rhac0553Vos.Count > 0 Then
                        frm.setrhac0553(activeCell, rhac0553Vos)
                    Else

                        Dim eventBaseDao As ShisakuEventBaseDao = New ShisakuEventBaseDaoImpl
                        '	T_SHISAKU_BUHIN_EDITからBUHIN_NO=アクティブセル.valueのPK項目を取得。
                        '	T_SHISAKU_BUHIN_EDIT_INSTLから、上記PK項目、かつ、INSU_SURYO>0の、PKとINSTL_HINBAN_HYOUJI_JUNを取得。																																																																						
                        '	T_SHISAKU_SEKKEI_BLOCK_INSTLから、上記PKとINSTL_HINBAN_HYOUJI_JUN、かつ、INSU_SURYO>0の、PKとGOUSYAを取得。?																																																																						
                        '	T_SHISAKU_EVENT_BASEから、上記PKとGOUSYAの、ベース車開発符号を取得。																																																																						

                        For Each Vo As TShisakuEventBaseVo In eventBaseDao.FindBaseKaihatsuFugo(activeCell)
                            rhac0553Vos = makeDao.FindStructure0553ByBuhinNoOya(activeCell, Vo.BaseKaihatsuFugo)
                            If rhac0553Vos.Count > 0 Then
                                frm.setrhac0553(activeCell, rhac0553Vos)
                                Exit For
                            End If
                        Next
                    End If
                End If
                frm.Show()
            End If
        End Sub
        ''↑↑2014/08/05 2集計コード R/Yのブロック間紐付け d) (TES)施 ADD END

        ''↓↓2014/08/22 Ⅰ.3.設計編集 ベース車改修専用化_cf) (TES)張 AND BEGIN
        ''' <summary>
        ''' Final品番とするかどうかを判断
        ''' </summary>
        ''' <param name="buhinNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CheckFinalBuhin(ByVal buhinNo As String) As Boolean

            If buhinNo Is Nothing Or String.IsNullOrEmpty(buhinNo) = True Then
                Return False
            End If

            Dim result As Rhac2210Vo = koseiSubject.GetFinalBuhinNo(buhinNo)
            Return result IsNot Nothing
        End Function

        Private Sub datamodel_Changed(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs) Handles datamodel.Changed

            '2014/10/07 追加 E.Ubukata
            With spdParts.ActiveSheet
                If Not koseiObserver.ObserverUpdating AndAlso e.Column = 0 AndAlso StringUtil.IsNotEmpty(.GetValue(e.Row, e.Column)) AndAlso .GetValue(e.Row, e.Column).Equals(0) Then
                    EBom.Common.ComFunc.ShowErrMsgBox("レベルに0を設定することは出来ません。")
                    .SetValue(e.Row, e.Column, Nothing)
                    Exit Sub
                End If
            End With

            Dim ByteLength As Integer
            Select Case spdParts.ActiveSheet.Columns(e.Column).Tag

                Case SpdKoseiObserver.TAG_SHUKEI_CODE, _
                     SpdKoseiObserver.TAG_SIA_SHUKEI_CODE, _
                     SpdKoseiObserver.TAG_GENCYO_CKD_KBN, _
                     SpdKoseiObserver.TAG_MAKER_CODE, _
                     SpdKoseiObserver.TAG_BUHIN_NO, _
                     SpdKoseiObserver.TAG_BUHIN_NAME, _
                     SpdKoseiObserver.TAG_KYOUKU_SECTION

                    Dim Value As String = Me.spdParts.ActiveSheet.GetText(e.Row, e.Column)

                    If Trim(Value) <> "" Then
                        ByteLength = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Value)
                        If Len(Value) <> ByteLength Then
                            MsgBox("全角文字が含まれています。")
                            Me.spdParts.ActiveSheet.Cells(e.Row, e.Column).Text = ""
                        End If
                    End If

            End Select

        End Sub

        '↓↓↓2014/12/26 材料の表示/非表示を切り替える機能を追加 TES)張 ADD BEGIN
        Public Sub SetZaishituColumnDisable()
            ToolStripGroupZaishitu.Text = "材質表示"
            ToolStripGroupZaishitu.ToolTipText = "材質を表示します"
            ToolStripGroupZaishitu.Checked = False
            koseiObserver.ZaishituColumnDisable()
            ZAI = "N"
        End Sub
        '↑↑↑2014/12/26 材料の表示/非表示を切り替える機能を追加 TES)張 ADD END

    End Class
End Namespace


