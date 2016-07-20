Imports ShisakuCommon.Ui.Access
Imports ShisakuCommon.Db
Imports EBom.Common
Imports ShisakuCommon.Util
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Valid
Imports FarPoint.Win
Imports ShisakuCommon.Ui.Spd
Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.CellType
Imports System.IO
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.TehaichoEdit.Dao

Namespace TehaichoEdit

#Region "号車別納期設定クラス"
    ''' <summary>
    ''' 号車別納期設定クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class frm20GousyaNoukiSettei

#Region "プライベート変数"
        '''<summary>初期化完了フラグ</summary>>
        Private _InitComplete As Boolean = False
        '''<summary>入力有無判定に使用</summary>>
        Private _aInputWatcher As InputWatcher
        ''' <summary>フォーム表示時に呼び出されるID</summary>
        Private m_initSwitchId As String = String.Empty

        ''' <summary>画面制御ロジック</summary>
        Private _TehaiEditLogic As TehaichoEditLogic
        ''' <summary>マウスクリック行列位置保存</summary>
        Private _clickCellPosition As Spread.Model.CellRange
        ''' <summary>号車情報シート</summary>
        Private GousyaSheet As SheetView

        Private _ResultOk As Boolean

        ''' <summary>号車名称リストデータテーブル (NmTDColGousyaNameListで列名参照)</summary>
        Public _dtGousyaNameList As DataTable

        Private Const _DUMMY_NAME As String = "DUMMY"

#End Region

#Region "コンストラクタ"

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="tehaiEditLogic">画面制御ロジック</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal tehaiEditLogic As TehaichoEditLogic)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            Me.Refresh()
            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)

            Me._dtGousyaNameList = tehaiEditLogic._dtGousyaNameList
            Me._TehaiEditLogic = tehaiEditLogic

            '初期化メイン
            Initialize()
        End Sub

#End Region

#Region "プロパティ"
#Region "初期化完了確認"
        ''' <summary>
        ''' 初期化完了確認
        ''' 
        ''' 初期化正常実行でTRUEを返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property InitComplete()
            Get
                Return _InitComplete
            End Get
        End Property
#End Region

        ''' <summary>
        ''' OKボタンでTRUEを返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ResultOk()
            Get
                Return _ResultOk
            End Get
        End Property

#End Region

#Region "初期化"

#Region "初期化メイン"
        ''' <summary>
        ''' 初期化メイン
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub Initialize()
            Try

                Cursor.Current = Cursors.WaitCursor

                Dim sheet As FarPoint.Win.Spread.SheetView = spdGouSya_Sheet1
                NmSpdTagGousyaNoukiSettei.initTagName(sheet)

                'タイトルバージョンセット
                ShisakuFormUtil.setTitleVersion(Me)

                'スプレッダーを初期化する
                InitSpreadColDefaultDate()

                '入力監視初期化
                InitializeWatcher()

                '初期データ表示スプレッド(号車)
                SetSpreadGousya()

                'スプレッドの読取専用列設定
                _TehaiEditLogic.LockColGousyaNoukiSetteiSpread(Me)

                'Enterキーの動作を、「編集」から「次行へ移動」にする
                ShisakuCommon.Ui.Spd.SpreadUtil.BindKeyEnterIsNextRow(spdGouSya)

                'Shift + Enterキーの動作を、「前行へ移動」にする
                ShisakuCommon.Ui.Spd.SpreadUtil.BindKeyShiftEnterIsPreviousRow(spdGouSya)

                '初期化完了
                _InitComplete = True

            Catch ex As Exception

                Dim msg As String
                msg = String.Format("画面の初期化中に問題が発生しました(ERR={0})", ex.Message)
                ComFunc.ShowErrMsgBox(msg)
                Me.Close()

            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Sub

#End Region


#Region "スプレッド日付列の初期日付設定"
        ''' <summary>
        ''' スプレッド日付列の初期日付設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitSpreadColDefaultDate()
            Dim sheet As Spread.SheetView = spdGouSya_Sheet1

            '納入指示日付
            Dim nounyuDate As New Spread.CellType.DateTimeCellType

            nounyuDate.DropDownButton = True
            'nounyuDate.DateDefault = Now
            sheet.Columns(NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI).CellType = nounyuDate
            sheet.Columns(NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI).CellType = nounyuDate

        End Sub
#End Region


        ''' <summary>
        ''' 号車情報スプレッドデータ格納
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SetSpreadGousya() As Boolean

            Dim dtBase As DataTable = _dtGousyaNameList

            Dim sheet As FarPoint.Win.Spread.SheetView = Me.spdGouSya_Sheet1
            Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(sheet)

            'スプレッドを他の画面と同様に設定
            Dim spread As FarPoint.Win.Spread.FpSpread = Me.spdGouSya
            SpreadUtil.Initialize(spread)

            '取得したデータ件数によりスプレッドの総行数を拡張(）
            sheet.RowCount = dtBase.Rows.Count + 2

            Dim oRow As Integer = startRow
            For Each dtRow As DataRow In dtBase.Rows

                '号車がブランク、DUMMYの場合、行をロックする。
                If StringUtil.IsEmpty(dtRow(NmSpdTagGousyaNoukiSettei.TAG_SHISAKU_GOUSYA)) Or _
                    StringUtil.Equals(dtRow(NmSpdTagGousyaNoukiSettei.TAG_SHISAKU_GOUSYA), "DUMMY") Then
                    sheet.Rows(oRow).Locked = True
                End If

                '号車
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagGousyaNoukiSettei.TAG_SHISAKU_GOUSYA)).Value = _
                                                              dtRow(NmSpdTagGousyaNoukiSettei.TAG_SHISAKU_GOUSYA)

                'トリム納入指示日
                Dim strTNounyuShijibi As String = TehaichoEditLogic.ConvDateInt8(IIf(IsDBNull(dtRow(NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI)), 0, dtRow(NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI)))
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI)).Value = strTNounyuShijibi
                'メタル納入指示日
                Dim strMNounyuShijibi As String = TehaichoEditLogic.ConvDateInt8(IIf(IsDBNull(dtRow(NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI)), 0, dtRow(NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI)))
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI)).Value = strMNounyuShijibi
                oRow += 1
            Next

            Return True

        End Function

#Region "コントロール監視初期化"
        ''' <summary>
        ''' コントロール監視初期化
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitializeWatcher()

            _aInputWatcher = New InputWatcher

            _aInputWatcher.Add(spdGouSya)

            'スプレッドに対してのコピーショートカットキー（Ctrl +C)を無効に（コード上でコピーを処理する為必要な処置)\
            Dim imGousya As New FarPoint.Win.Spread.InputMap
            imGousya = spdGouSya.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imGousya.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)

        End Sub
#End Region

#End Region

#Region "イベント"

#Region "フォームロード"
        ''' <summary>
        ''' フォームロード
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub frm20GousyaNoukiSettei_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            Me.Focus()

        End Sub
#End Region

#Region "ボタン(戻る)"

        ''' <summary>
        ''' ボタン(戻る)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
            '変更が発生した場合だけ、警告を表示する。
            '※項目を一つでも修正したら、戻るボタン押下時に警告を出す。
            '※何も変更していない場合は、警告は無し。

            If Not _aInputWatcher.WasUpdate Then
                _ResultOk = False
                Me.Close()
                Return
            End If
            If frm01Kakunin.ConfirmOkCancel("変更を更新せずに終了しますか？") = MsgBoxResult.Ok Then
                _ResultOk = False
                Me.Close()
                Return
            End If

        End Sub

#End Region

#Region "保存前入力データチェック"
        ''' <summary>
        '''  保存前入力データチェック
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SaveDataCheck() As Boolean
            Dim sheet As FarPoint.Win.Spread.SheetView = Me.spdGouSya_Sheet1
            Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(sheet)

            '最小最大
            Dim minRow As Integer = 0
            Dim minCol As Integer = 0
            Dim minNounyuShijibi As String = ""
            Dim maxRow As Integer = 0
            Dim maxCol As Integer = 0
            Dim maxNounyuShijibi As String = ""

            'エラー対象セル
            Dim errCell As Spread.Cell = Nothing
            Dim errFlag As Boolean = False
            Dim firstErrCell As Spread.Cell = Nothing
            '空行後入力判定用
            Dim empRowNo As Integer = -1
            '
            Dim mNonyuCnt As Integer = 0
            Dim tNonyuCnt As Integer = 0

            'スプレッドループ
            For i As Integer = startRow To sheet.RowCount - startRow - 1
                'スプレッドのエラー色を戻す
                For j As Integer = 0 To sheet.ColumnCount - 1
                    sheet.Cells(i, j).BackColor = Nothing
                Next
                '号車がブランク、DUMMYの場合、チェック不要。
                Dim strGousya As String = sheet.GetText(i, _
                                                        TehaichoEditLogic.GetTagIdx(sheet, _
                                                                                    NmSpdTagGousyaNoukiSettei.TAG_SHISAKU_GOUSYA)).Trim
                If StringUtil.IsEmpty(strGousya) Or _
                    StringUtil.Equals(strGousya, "DUMMY") Then
                    Continue For
                End If
                'トリム納入指示日必須確認
                Dim tNounyuShijibi As String = sheet.GetText(i, _
                                                             TehaichoEditLogic.GetTagIdx(sheet, _
                                                                                         NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI)).Trim
                If Not tNounyuShijibi.Equals(String.Empty) Then
                    tNonyuCnt += 1
                End If
                'メタル納入指示日必須確認
                Dim mNounyuShijibi As String = sheet.GetText(i, _
                                                             TehaichoEditLogic.GetTagIdx(sheet, _
                                                                                         NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI)).Trim
                If Not mNounyuShijibi.Equals(String.Empty) Then
                    mNonyuCnt += 1
                End If
            Next

            'スプレッドループ
            For i As Integer = startRow To sheet.RowCount - startRow - 1
                '号車がブランク、DUMMYの場合、チェック不要。
                Dim strGousya As String = sheet.GetText(i, _
                                                        TehaichoEditLogic.GetTagIdx(sheet, _
                                                                                    NmSpdTagGousyaNoukiSettei.TAG_SHISAKU_GOUSYA)).Trim
                If StringUtil.IsEmpty(strGousya) Or _
                    StringUtil.Equals(strGousya, "DUMMY") Then
                    Continue For
                End If

                'トリム納入指示日必須確認
                Dim tNounyuShijibi As String = sheet.GetText(i, _
                                                             TehaichoEditLogic.GetTagIdx(sheet, _
                                                                                         NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI)).Trim
                If tNounyuShijibi.Equals(String.Empty) And tNonyuCnt > 0 Then
                    sheet.Cells(i, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI)).BackColor = Color.Red
                    errCell = sheet.Cells(i, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI))
                    errFlag = True
                End If

                'メタル納入指示日必須確認
                Dim mNounyuShijibi As String = sheet.GetText(i, _
                                                             TehaichoEditLogic.GetTagIdx(sheet, _
                                                                                         NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI)).Trim
                If mNounyuShijibi.Equals(String.Empty) And mNonyuCnt > 0 Then
                    sheet.Cells(i, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI)).BackColor = Color.Red
                    errCell = sheet.Cells(i, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI))
                    errFlag = True
                End If

                '最初のエラーをアクティブセルにする為に保持
                If firstErrCell Is Nothing AndAlso Not errCell Is Nothing Then
                    firstErrCell = errCell
                End If

                'minを求める。
                If StringUtil.IsNotEmpty(tNounyuShijibi) And StringUtil.IsNotEmpty(mNounyuShijibi) Then
                    If tNounyuShijibi < mNounyuShijibi Then
                        If StringUtil.IsEmpty(minNounyuShijibi) Or _
                           StringUtil.IsNotEmpty(minNounyuShijibi) And tNounyuShijibi < minNounyuShijibi Then
                            minNounyuShijibi = tNounyuShijibi
                            minRow = i
                            minCol = TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI)
                        End If
                    Else
                        If StringUtil.IsEmpty(minNounyuShijibi) Or _
                           StringUtil.IsNotEmpty(minNounyuShijibi) And mNounyuShijibi < minNounyuShijibi Then
                            minNounyuShijibi = mNounyuShijibi
                            minRow = i
                            minCol = TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI)
                        End If
                    End If
                Else
                    If StringUtil.IsNotEmpty(tNounyuShijibi) Then
                        If StringUtil.IsEmpty(minNounyuShijibi) Or _
                           StringUtil.IsNotEmpty(minNounyuShijibi) And tNounyuShijibi < minNounyuShijibi Then
                            minNounyuShijibi = tNounyuShijibi
                            minRow = i
                            minCol = TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI)
                        End If
                    ElseIf StringUtil.IsNotEmpty(mNounyuShijibi) Then
                        If StringUtil.IsEmpty(minNounyuShijibi) Or _
                           StringUtil.IsNotEmpty(minNounyuShijibi) And mNounyuShijibi < minNounyuShijibi Then
                            minNounyuShijibi = mNounyuShijibi
                            minRow = i
                            minCol = TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI)
                        End If
                    End If
                End If

                'maxを求める。
                If StringUtil.IsNotEmpty(maxNounyuShijibi) Then
                    If tNounyuShijibi > mNounyuShijibi Then
                        If StringUtil.IsNotEmpty(tNounyuShijibi) And tNounyuShijibi > maxNounyuShijibi Then
                            maxNounyuShijibi = tNounyuShijibi
                            maxRow = i
                            maxCol = TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI)
                        End If
                    Else
                        If StringUtil.IsNotEmpty(mNounyuShijibi) And mNounyuShijibi > maxNounyuShijibi Then
                            maxNounyuShijibi = mNounyuShijibi
                            maxRow = i
                            maxCol = TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI)
                        End If
                    End If
                Else
                    If tNounyuShijibi > mNounyuShijibi Then
                        maxNounyuShijibi = tNounyuShijibi
                        maxRow = i
                        maxCol = TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI)
                    Else
                        maxNounyuShijibi = mNounyuShijibi
                        maxRow = i
                        maxCol = TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI)
                    End If
                End If

            Next

            'エラー存在有無
            If errFlag = True Then

                'エラー位置をアクティブにする
                sheet.SetActiveCell(firstErrCell.Row.Index, errCell.Column.Index)

                Me.spdGouSya.ShowActiveCell(Spread.VerticalPosition.Top, Spread.HorizontalPosition.Left)

                ComFunc.ShowErrMsgBox("号車の納入指示日が未入力です。" & vbCrLf & vbCrLf & _
                                       "赤色個所を確認してください。")
                Return False
            End If

            If StringUtil.IsEmpty(minNounyuShijibi) And StringUtil.IsEmpty(maxNounyuShijibi) Then
                Return True
            End If

            '納入指示日の最小と最大の差が６カ月未満かチェック
            Dim dtPura6 As DateTime = DateTime.Parse(minNounyuShijibi)
            Dim dtMaxNounyuShijibi As DateTime = DateTime.Parse(maxNounyuShijibi)

            'minへ 6 ヶ月加算する
            dtPura6 = dtPura6.AddMonths(6)
            '最大納入指示日と比較する
            If dtPura6 < dtMaxNounyuShijibi Then

                'アラート位置の背景色を黄色にする
                sheet.Cells(minRow, minCol).BackColor = Color.Yellow
                sheet.Cells(maxRow, maxCol).BackColor = Color.Yellow
                '
                Me.spdGouSya.ShowActiveCell(Spread.VerticalPosition.Top, Spread.HorizontalPosition.Left)

                '確認フォームを表示
                Dim result As Integer = MsgBox("納入指示日に６ヵ月以上の差が発生しています。" & vbCrLf & vbCrLf & _
                                               "そのまま実行しますか？", MsgBoxStyle.OkCancel, "ワーニング")
                If result = MsgBoxResult.Ok Then  'OK
                    Return True
                Else
                    Return False
                End If
            End If

            Return True

        End Function

#End Region

#Region "ボタン(保存)"

#Region "号車列名データテーブル生成)"
        ''' <summary>
        ''' </summary>
        ''' <remarks></remarks>
        Private Function SetGousyaName() As DataTable

            Dim dtGousya As New DataTable
            Dim sheet As SheetView = Me.spdGouSya_Sheet1
            Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(sheet)

            'データテーブルの属性情報のみ取得
            dtGousya = TehaichoEditImpl.FindAllGousyaNameList("ZZZZZZZZ", "ZZZZZZZZ")

            Dim intHyoujiCnt As Integer = 0

            '基本情報の末列以降に格納された号車情報をデータテーブルに格納
            '   スプレッドループ
            For i As Integer = startRow To sheet.RowCount - 1

                Dim row As DataRow = dtGousya.NewRow

                Dim mDate As String = TehaichoEditLogic.ConvInt8Date(IIf(StringUtil.IsEmpty(sheet.GetText(i, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI)).Trim), 0, _
                                                       sheet.GetText(i, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI)).Trim))
                If mDate Is Nothing AndAlso Not IsDate(mDate) Then
                    mDate = String.Empty
                End If
                Dim tDate As String = TehaichoEditLogic.ConvInt8Date(IIf(StringUtil.IsEmpty(sheet.GetText(i, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI)).Trim), 0, _
                                                       sheet.GetText(i, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI)).Trim))
                If tDate Is Nothing AndAlso Not IsDate(tDate) Then
                    tDate = String.Empty
                End If

                Dim gousyaName As String = sheet.GetText(i, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagGousyaNoukiSettei.TAG_SHISAKU_GOUSYA)).Trim
                Dim gousyaHyoujiJun As String = intHyoujiCnt.ToString.Trim

                If gousyaName Is Nothing Then
                    row(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME) = String.Empty
                    gousyaName = String.Empty
                Else
                    row(NmDTColGousyaNameList.TD_HYOJIJUN_NO) = gousyaHyoujiJun
                    row(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME) = gousyaName.Trim
                    If StringUtil.IsNotEmpty(mDate) Then
                        row(NmDTColGousyaNameList.TD_M_NOUNYU_SHIJIBI) = mDate.Replace("/", "")
                    End If
                    If StringUtil.IsNotEmpty(tDate) Then
                        row(NmDTColGousyaNameList.TD_T_NOUNYU_SHIJIBI) = tDate.Replace("/", "")
                    End If

                    intHyoujiCnt += 1
                    dtGousya.Rows.Add(row)

                    'DUMMYまで来たら抜ける
                    If gousyaName.Equals(_DUMMY_NAME) Then
                        Exit For
                    End If
                End If
            Next

            Return dtGousya

        End Function
#End Region


        ''' <summary>
        ''' ボタン（保存）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnHozon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHozon.Click

            Try

                '入力データチェック
                If SaveDataCheck() = False Then
                    Exit Sub
                End If

                '保存確認フォームを表示
                Dim result As Integer = frm00Kakunin.Confirm("確認", "設定を実行しますか。", _
                                               "", "OK", "CANCEL")

                If result = MsgBoxResult.Ok Then  'OK

                    '入れ替える。
                    _TehaiEditLogic.InitSpreadColGousyaNameNoukiSettei(SetGousyaName())

                    Cursor.Current = Cursors.WaitCursor
                    _ResultOk = True

                    Me.Close()

                Else
                    Return
                End If
                Cursor.Current = Cursors.Default
            Catch ex As Exception
                ComFunc.ShowErrMsgBox(String.Format("保存中にエラーが発生しました{0}{1}", vbCrLf, ex.Message))
            Finally
                Cursor.Current = Cursors.Default
            End Try
        End Sub

#End Region

#Region "スプレッドChange号車"
        ''' <summary>
        '''  スプレッドChange号車
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdGouSya_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdGouSya.Change

            Try

                Dim actCol As Integer = spdGouSya_Sheet1.ActiveColumn.Index
                Dim actRow As Integer = spdGouSya_Sheet1.ActiveRow.Index
                Dim actColTag As String = spdGouSya_Sheet1.Columns(actCol).Tag.ToString

                'セル変更対応処理
                Select Case actColTag

                    Case NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI
                        'トリム納入指示日
                        Dim valNounyu As String = spdGouSya_Sheet1.GetValue(actRow, _
                                                                            spdGouSya_Sheet1.Columns(NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI).Index)
                        'Nullや空文字の場合
                        If valNounyu Is Nothing OrElse valNounyu.ToString.Trim.Equals(String.Empty) Then
                            spdGouSya_Sheet1.SetValue(actRow, spdGouSya_Sheet1.Columns(NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI).Index, String.Empty)
                        Else
                            Dim dateNounyu As Date = spdGouSya_Sheet1.GetValue(actRow, spdGouSya_Sheet1.Columns(NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI).Index)

                            'カレンダーから入力された値から時刻を取り除き格納する
                            If IsDate(dateNounyu) = True Then
                                spdGouSya_Sheet1.SetValue(actRow, spdGouSya_Sheet1.Columns(NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI).Index, Format(dateNounyu, "yyyy/MM/dd"))
                            End If
                            '編集されたセルは太文字・青文字にする
                            spdGouSya_Sheet1.Cells(actRow, actCol, actRow, actCol).ForeColor = Color.Blue
                            spdGouSya_Sheet1.Cells(actRow, actCol, actRow, actCol).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                        End If
                    Case NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI
                        'メタル納入指示日
                        Dim valNounyu As String = spdGouSya_Sheet1.GetValue(actRow, _
                                                                            spdGouSya_Sheet1.Columns(NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI).Index)
                        'Nullや空文字の場合
                        If valNounyu Is Nothing OrElse valNounyu.ToString.Trim.Equals(String.Empty) Then
                            spdGouSya_Sheet1.SetValue(actRow, spdGouSya_Sheet1.Columns(NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI).Index, String.Empty)
                        Else
                            Dim dateNounyu As Date = spdGouSya_Sheet1.GetValue(actRow, spdGouSya_Sheet1.Columns(NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI).Index)

                            'カレンダーから入力された値から時刻を取り除き格納する
                            If IsDate(dateNounyu) = True Then
                                spdGouSya_Sheet1.SetValue(actRow, spdGouSya_Sheet1.Columns(NmSpdTagGousyaNoukiSettei.TAG_M_NOUNYU_SHIJIBI).Index, Format(dateNounyu, "yyyy/MM/dd"))
                            End If
                            '編集されたセルは太文字・青文字にする
                            spdGouSya_Sheet1.Cells(actRow, actCol, actRow, actCol).ForeColor = Color.Blue
                            spdGouSya_Sheet1.Cells(actRow, actCol, actRow, actCol).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                        End If
                End Select

            Catch ex As Exception
                MsgBox("値変更処理で問題が発生しました")
                Me.Close()
            End Try

        End Sub
#End Region

#Region "スプレッド号車マウスクリックイベント情報取得"


        ''' <summary>
        '''  スプレッド号車マウスクリックイベント情報取得
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdGousya_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles spdGouSya.MouseDown
            Dim sv As New FarPoint.Win.Spread.SpreadView(Me.spdGouSya)
            sv = Me.spdGouSya.GetRootWorkbook()

            'セル行列位置取得
            _clickCellPosition = sv.GetCellFromPixel(e.X, e.Y)

            If _clickCellPosition.Row = -1 Then
                _clickCellPosition = sv.GetRowHeaderCellFromPixel(e.X, e.Y)

            Else
                _clickCellPosition = Nothing
            End If

        End Sub
#End Region

#Region "キー押下処理"
        ''' <summary>
        ''' キー押下処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub frm20GousyaNoukiSettei_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
            Dim downKey As Object
            Dim sheet As Spread.SheetView = Me.spdGouSya_Sheet1
            Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)

            downKey = e.KeyCode()

            Select Case downKey
                Case Keys.C
                    If selection Is Nothing Then
                        Exit Sub
                    End If

                    'コントロールキーとCキーが押された
                    If (e.Modifiers And Keys.Control) = Keys.Control Then
                        If selection.Row <= -1 Then
                            ComFunc.ShowInfoMsgBox("列選択でのコピーは出来ません")
                            Return
                        End If

                        '書式バックアップ
                        Dim listBln As List(Of Boolean()) = TehaichoEditLogic.GetEditCellInfo(sheet)
                        '書式を一時的に全て保存編集対象にする
                        TehaichoEditLogic.SetUndoCellFormat(sheet)

                        ' 選択範囲を取得
                        Dim cr As FarPoint.Win.Spread.Model.CellRange() = sheet.GetSelections()
                        If cr.Length > 0 Then
                            Dim data As [String] = Nothing
                            If cr(0).Row = -1 Then
                                ' 列単位で選択されている場合
                                For i As Integer = 0 To sheet.RowCount - 1
                                    If sheet.GetRowVisible(i) = True Then
                                        data += sheet.GetClipValue(i, cr(0).Column, 1, cr(0).ColumnCount)
                                    End If
                                Next
                            Else
                                ' セル単位で選択されている場合
                                Dim count As Integer = 0
                                For i As Integer = 0 To cr(0).RowCount - 1
                                    If sheet.GetRowVisible(cr(0).Row + i) = True Then
                                        data += sheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                    End If
                                Next
                            End If
                            ' クリップボードに設定します
                            Clipboard.SetData(DataFormats.Text, data)
                        End If

                        '書式を戻す
                        TehaichoEditLogic.SetUndoCellFormat(sheet, listBln)
                    End If

                Case Keys.X

                Case Keys.V

                    If selection Is Nothing Then
                        Exit Sub
                    End If

                    'コントロールキーとVキーが押された
                    If (e.Modifiers And Keys.Control) = Keys.Control Then
                        If selection.Row <= -1 Then
                            ComFunc.ShowInfoMsgBox("列選択での貼付けは出来ません")
                            Return
                        End If

                        Dim listClip As New List(Of String())

                        listClip = TehaichoEditLogic.GetClipbordList

                        'スプレッド自身に貼り付けさせる'
                        Dim im As New Spread.InputMap
                        spdGouSya.ClipboardOptions = Spread.ClipboardOptions.NoHeaders
                        im.Put(New Spread.Keystroke(Keys.V, Keys.Control), Spread.SpreadActions.ClipboardPasteValues)

                        If Not listClip Is Nothing Then

                            Dim rowCount As Integer = listClip.Count - 1
                            Dim colCount As Integer = listClip(0).Length

                            If (selection.Row + rowCount) >= sheet.RowCount - 1 Then
                                ComFunc.ShowInfoMsgBox("貼り付けしようとしている範囲がスプレッド表の最大行を超えています")
                                Return
                            End If

                            '編集されたセルは太文字・青文字にする
                            sheet.Cells(selection.Row, selection.Column, _
                                        selection.Row + rowCount - 1, selection.Column + colCount - 1).ForeColor = Color.Blue
                            sheet.Cells(selection.Row, selection.Column, _
                                        selection.Row + rowCount - 1, selection.Column + colCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

                        End If

                    End If

                Case Keys.Delete
                    'デリートキー押下時'

                    If sheet.SelectionCount <> 1 Then
                        Return
                    End If

                    ClearRange(sheet)

                    e.Handled = True

            End Select

        End Sub

#End Region

#Region "DELETEKeyの処理"
        ''' <summary>
        ''' セルの中身の削除
        ''' </summary>
        ''' <param name="asheetView"></param>
        ''' <remarks></remarks>
        Private Sub ClearRange(ByVal asheetView As FarPoint.Win.Spread.SheetView)
            Dim datamodel As FarPoint.Win.Spread.Model.DefaultSheetDataModel
            '削除範囲の設定'
            Dim fromRow As Integer
            Dim rowCount As Integer
            Dim fromCol As Integer
            Dim colCount As Integer

            datamodel = asheetView.Models.Data

            '開始行<=終了行になるように調整'
            If asheetView.Models.Selection.AnchorRow < asheetView.Models.Selection.LeadRow Then
                fromRow = asheetView.Models.Selection.AnchorRow
                rowCount = asheetView.Models.Selection.LeadRow - fromRow + 1
            Else
                fromRow = asheetView.Models.Selection.LeadRow
                rowCount = asheetView.Models.Selection.AnchorRow - fromRow + 1
            End If

            '開始列<=終了列になるように調整'
            If asheetView.Models.Selection.AnchorColumn < asheetView.Models.Selection.LeadColumn Then
                fromCol = asheetView.Models.Selection.AnchorColumn
                colCount = asheetView.Models.Selection.LeadColumn - fromCol + 1
            Else
                fromCol = asheetView.Models.Selection.LeadColumn
                colCount = asheetView.Models.Selection.AnchorColumn - fromCol + 1
            End If

            '
            If fromRow < 2 Then
                fromRow = 2
            End If

            For Row As Integer = fromRow To fromRow + rowCount - 1
                For col As Integer = fromCol To fromCol + colCount - 1
                    asheetView.ClearRange(Row, col, 1, 1, True)
                Next
            Next

        End Sub


#End Region

#End Region

#Region "右ショートカットメニュー(切取り)"
        ''' <summary>
        ''' 右ショートカットメニュー(切取り)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolMenuCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            ToolCut()
        End Sub

#End Region

#Region "右ショートカットメニュー(コピー)"
        ''' <summary>
        ''' 右ショートカットメニュー(コピー)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolMenuCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolMenuCopy.Click
            ToolCopy()
        End Sub

#End Region

#Region "右ショートカットメニュー(貼付け)"
        ''' <summary>
        ''' 右ショートカットメニュー(貼付け)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolMenuPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolMenuPaste.Click
            ToolPaste()
        End Sub

#End Region

#Region "切取りイベント処理"
        ''' <summary>
        ''' 切取りイベント処理
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ToolCut()
            Try

                If spdGouSya_Sheet1.GetSelection(0) Is Nothing Then
                    ComFunc.ShowInfoMsgBox("セル範囲が指定されていません。")
                    Exit Sub
                End If

                Dim spd As FpSpread = Me.spdGouSya
                spd.Focus()
                System.Threading.Thread.Sleep(10)
                System.Windows.Forms.SendKeys.Flush()
                ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！
                System.Windows.Forms.SendKeys.Send("^x")

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("切取りが正常に行えませんでした。")
            End Try
        End Sub
#End Region

#Region "コピーイベント処理"
        ''' <summary>
        ''' コピーイベント処理
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ToolCopy()
            Try

                If spdGouSya_Sheet1.GetSelection(0) Is Nothing Then
                    ComFunc.ShowInfoMsgBox("セル範囲が指定されていません。")
                    Exit Sub
                End If

                Dim spd As FpSpread = Me.spdGouSya
                spd.Focus()
                System.Threading.Thread.Sleep(10)
                System.Windows.Forms.SendKeys.Flush()
                ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！
                System.Windows.Forms.SendKeys.Send("^c")

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("コピーが正常に行えませんでした。")
            End Try

        End Sub

#End Region

#Region "貼りつけイベント処理"

        Private Sub ToolPaste()
            Try

                If spdGouSya_Sheet1.GetSelection(0) Is Nothing Then
                    ComFunc.ShowInfoMsgBox("セル範囲が指定されていません。")
                    Exit Sub
                End If

                Dim spd As FpSpread = Me.spdGouSya
                spd.Focus()
                System.Windows.Forms.SendKeys.Flush()
                System.Threading.Thread.Sleep(10)
                ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！
                System.Windows.Forms.SendKeys.Send("^v")

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("貼りつけが正常に行えませんでした。")
            End Try
        End Sub
#End Region

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class

#End Region

End Namespace

