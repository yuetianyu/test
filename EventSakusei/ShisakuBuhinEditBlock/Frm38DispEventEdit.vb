Imports ShisakuCommon.Ui.Access
Imports ShisakuCommon.Db
Imports EBom.Common
Imports ShisakuCommon.Util
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Valid
Imports FarPoint.Win
Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.Model
Imports ShisakuCommon.Ui.Spd
Imports System.Text
Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.ShisakuComFunc
Imports EventSakusei.EventEdit.Vo

Namespace ShisakuBuhinEditBlock

    ''' <summary>
    ''' イベント情報登録・編集
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm38DispEventEdit : Implements Observer

        ' 戻るボタン押下か？
        Private _resultOk As Boolean

        ''' <summary>戻るボタン押下か？</summary>
        ''' <returns>戻るボタン押下か？</returns>
        Public ReadOnly Property ResultOk() As Boolean
            Get
                Return _resultOk
            End Get
        End Property

        Private Sub BtnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
            If Not aInputWatcher.WasUpdate Then
                Me.Close()
                Return
            End If
            If frm01Kakunin.ConfirmOkCancel("再展開を実施せずに終了しますか？") = MsgBoxResult.Ok Then
                If subject.RegistError Then
                    _WasSaveRegister = False
                End If
                Me.Close()
                Return
            End If

        End Sub

        Private Function IsActiveBaseCar() As Boolean
            Return spdBaseCar.Visible
        End Function

        Private _WasSaveRegister As Boolean
        ''' <summary>
        ''' 保存、または、登録をしたかを返す
        ''' </summary>
        ''' <returns>保存・登録をした場合、true</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WasSaveRegister() As Boolean
            Get
                Return _WasSaveRegister
            End Get
        End Property

        Private errorController As New ErrorController()
        Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
            Try

                errorController.ClearBackColor()

                'SPREADのエラーチェック前にも表示値を更新してみる。
                UpdateObserver(subject, Nothing)

                baseCarObserver.AssertValidateRegister()

                'データが一件も登録されていないときのエラーを追加
                Dim ht As New Hashtable
                Dim duplicateGoshaRowNos(spdBaseCar_Sheet1.RowCount) As Integer
                Dim j As Integer = 0
                Dim wGousyaCount As Integer = 0
                For rowNo As Integer = 3 To spdBaseCar_Sheet1.RowCount - 1
                    spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).ResetBackColor()
                    '号車チェックします。
                    If Not StringUtil.IsEmpty( _
                           spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value) Then
                        '2012/02/08　重複した号車のチェック
                        If ht.ContainsKey(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value) Then
                            'エラーメッセージ
                            duplicateGoshaRowNos(j) = rowNo
                            j = j + 1
                            duplicateGoshaRowNos(j) = Integer.Parse(ht(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value))
                            j = j + 1
                        Else
                            '2012/02/08　重複した号車のチェック
                            ht.Add(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value, rowNo)
                        End If
                        '号車の数をカウントする。
                        wGousyaCount += 1
                    End If
                Next
                If wGousyaCount = 0 Then
                    'エラーメッセージ
                    ComFunc.ShowErrMsgBox("号車が１件も登録されていません")
                    Return
                End If
                If j > 0 Then
                    For i As Integer = 0 To j - 1
                        spdBaseCar_Sheet1.Cells(duplicateGoshaRowNos(i), spdBaseCar_Sheet1.Columns("GOSHA").Index).BackColor = ERROR_COLOR
                    Next
                    ComFunc.ShowErrMsgBox("同じ名称を持つ号車が２件以上登録されています")
                    Return
                End If
            Catch ex As IllegalInputException
                errorController.SetBackColorOnError(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)
                ''エラーメッセージ
                ComFunc.ShowErrMsgBox(ex.Message)
                Return
            End Try

            If frm01Kakunin.ConfirmOkCancel("展開を実行しますか？", "★実行すると元に戻せません！！", "AL") <> MsgBoxResult.Ok Then
                Return
            End If
            Try
                '
                'ＡＬ再展開用のベース情報作成
                subject.RegisterMain()

                'ブロック№リストを変数にセット
                Dim strBlockNoList As String = ""
                For Each v As String In blockNoList
                    v = v.Replace(":"c, ","c)
                    If strBlockNoList <> "" Then
                        strBlockNoList = strBlockNoList + "_"
                    End If
                    strBlockNoList = strBlockNoList + v
                Next
                'EXCEL出力
                ExcelCommon.SaveExcelFile("ＡＬ再展開BASE[" + strBlockNoList + "]" + Now.ToString("MMdd") + Now.ToString("HHmm"), _
                                          spdBaseCar, "ＡＬ再展開", "C") '"C"を付けると自動で開かない。
                _resultOk = True

            Catch ex As TableExclusionException
                ComFunc.ShowInfoMsgBox(ex.Message)
            Finally
                _WasSaveRegister = True ' 排他例外だったとしても、誰かが更新したから
            End Try

            If Not subject.RegistError Then
                Me.Close()
            End If

        End Sub

        ''' <summary>
        ''' 行背景色を強制的にグレーに変更する(種別が "D" の場合)
        ''' </summary>
        ''' <param name="activeSheet">色を設定するSpreadSheet</param>
        ''' <param name="targetColumn">種別のカラム位置</param>
        ''' <remarks></remarks>
        Private Sub ForceSetActiveSpreadColor(ByVal activeSheet As SheetView, ByVal targetColumn As Long)
            Const DEF_TYPE_D As String = "D"
            Const DEF_TYPE_H As String = "種別"
            Dim nRow As Long
            Dim nColumn As Long = 0
            Dim nColumn2 As Long = 0
            Dim IsHeader As Boolean = False
            Dim dataStartRow As Long

            If activeSheet.Equals(spdBaseCar_Sheet1)  Then
                dataStartRow = 2
            Else
                dataStartRow = 0
            End If
            For nRow = dataStartRow To activeSheet.RowCount - 1
                If activeSheet.Cells(nRow, targetColumn, nRow, targetColumn).Text = DEF_TYPE_H Then
                    IsHeader = True
                Else
                    If IsHeader = True Then
                        If activeSheet.ColumnCount - 1 > 0 Then
                            nColumn2 = activeSheet.ColumnCount - 1
                        End If
                        If activeSheet.Cells(nRow, targetColumn, nRow, targetColumn).Text = DEF_TYPE_D Then
                            activeSheet.Cells(nRow, nColumn, nRow, nColumn2).BackColor = Color.DimGray
                            activeSheet.Cells(nRow, nColumn, nRow, nColumn2).ForeColor = Color.LightGray
                        Else
                            'TODO : 色の判断以外で行えないか？
                            If activeSheet.Cells(nRow, nColumn, nRow, nColumn2).BackColor = Color.DimGray Then
                                activeSheet.Cells(nRow, nColumn, nRow, nColumn2).ResetBackColor()
                                activeSheet.Cells(nRow, nColumn, nRow, nColumn2).ResetForeColor()
                            End If
                        End If
                    End If
                End If
            Next
        End Sub

        Private ReadOnly baseCarObserver As SpdBaseCarObserver
        Private subject As Logic.EventEdit
        Private headerSubject As Logic.EventEditHeader
        Private ReadOnly aInputWatcher As InputWatcher
        Private ReadOnly enabler As New ShisakuEnabler

        Private bukaCode As String
        Private blockNoList As List(Of String)

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Me.New(Nothing, Nothing, Nothing)
        End Sub

        ''' <summary>
        ''' コンストラクタ（編集モード）
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNoList">ブロック№</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, ByVal bukaCode As String, ByVal blockNoList As List(Of String))

            '処理中画面表示
            Dim SyorichuForm As New frm03Syorichu
            SyorichuForm.lblKakunin.Text = "試作イベント・ベース車情報画面を"
            SyorichuForm.Execute()
            SyorichuForm.Show()

            Application.DoEvents()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)

            Me.bukaCode = bukaCode
            Me.blockNoList = blockNoList

            subject = New Logic.EventEdit(shisakuEventCode, LoginInfo.Now, False)
            headerSubject = subject.HeaderSubject
            subject.AddObserver(Me)
            headerSubject.AddObserver(Me)

            baseCarObserver = New SpdBaseCarObserver(spdBaseCar, subject.BaseCarSubject)
            aInputWatcher = New InputWatcher

            InitializeWatcher()
            InitializeHeader()
            InitializeSpread()

            subject.NotifyObservers()

            aInputWatcher.Clear()

            '処理中画面非表示
            SyorichuForm.Close()
        End Sub

        Private Sub InitializeWatcher()
            aInputWatcher.Add(spdBaseCar)
        End Sub

        Public ReadOnly Property ShisakuEventCode() As String
            Get
                Return subject.ShisakuEventCode
            End Get
        End Property

        ''' <summary>
        ''' 表示値を更新する
        ''' </summary>
        ''' <param name="observable">呼び出し元のObservable</param>
        ''' <param name="arg">引数</param>
        ''' <remarks></remarks>
        Public Sub UpdateObserver(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update
            If observable Is headerSubject Then
            Else
                subject.BaseCarSubject.NotifyObservers(arg)
            End If
        End Sub

        ''' <summary>
        ''' スプレッドを初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitializeSpread()

            baseCarObserver.Initialize()

        End Sub

        ''' <summary>
        ''' ヘッダー部を初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitializeHeader()

            ShisakuFormUtil.setTitleVersion(Me)
            ''画面のPG-IDが表示されます。
            LblCurrPGId.Text = "PG-ID :" + PROGRAM_ID_09

            ShisakuFormUtil.SetDateTimeNow(Me.LblDateNow, Me.LblTimeNow)
            ShisakuFormUtil.SetIdAndBuka(Me.LblCurrUserId, Me.LblCurrBukaName)

        End Sub

#Region "画面右上時計動く機能"
        Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        End Sub
#End Region

        Private Sub spdBaseCar_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdBaseCar.CellClick

            If e.ColumnHeader Then
                '参考情報の表示／非表示
                If (e.Row = 0) Then
                    '
                    baseCarObserver.referenceCarInfo(e.Column)

                End If
            End If

        End Sub

        ''' <summary>
        ''' ベース車情報キー押下
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdBaseCar_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdBaseCar.KeyDown

            Dim downKey As Object
            Dim sheet As Spread.SheetView = Me.spdBaseCar_Sheet1
            Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)

            downKey = e.KeyCode()

            Select Case downKey

                Case Keys.C
                    '設計展開以降のイベントはKeyDownを無効に'
                    If Not headerSubject.IsSekkeiTenkaiIkou Then
                        'コントロールキーとCキーが押された
                        If (e.Modifiers And Keys.Control) = Keys.Control Then

                            '書式バックアップ
                            Dim listBln As List(Of Boolean()) = GetEditCellInfo(sheet)
                            '書式を一時的に全て保存編集対象にする
                            SetUndoCellFormat(sheet)

                            ' 選択範囲を取得
                            Dim cr As FarPoint.Win.Spread.Model.CellRange() = spdBaseCar.ActiveSheet.GetSelections()
                            If cr.Length > 0 Then
                                Dim data As [String] = Nothing
                                Dim count As Integer = 0
                                If cr(0).Row = -1 Then
                                    ' 列単位で選択されている場合
                                    For i As Integer = 0 To spdBaseCar.ActiveSheet.RowCount - 1
                                        If spdBaseCar.ActiveSheet.GetRowVisible(i) = True Then
                                            data += spdBaseCar.ActiveSheet.GetClipValue(i, cr(0).Column, 1, cr(0).ColumnCount)
                                        End If
                                    Next
                                Else
                                    ' セル単位で選択されている場合
                                    For i As Integer = 0 To cr(0).RowCount - 1
                                        If spdBaseCar.ActiveSheet.GetRowVisible(cr(0).Row + i) = True Then
                                            data += spdBaseCar.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                        End If
                                    Next
                                End If

                                ' クリップボードに設定します
                                Clipboard.SetData(DataFormats.Text, data)
                            End If

                            '書式を戻す
                            SetUndoCellFormat(sheet, listBln)

                        End If
                    End If

                Case Keys.V
                    '設計展開以降のイベントはKeyDownを無効に'
                    If Not headerSubject.IsSekkeiTenkaiIkou Then
                        '行選択時は無効にする。
                        If Not selection Is Nothing Then
                            If (selection.Column = -1 AndAlso selection.ColumnCount - 1) Then
                                e.Handled = True
                            Else

                                'コントロールキーとVキーが押された
                                If (e.Modifiers And Keys.Control) = Keys.Control Then

                                    Dim listClip As New List(Of String())

                                    listClip = GetClipbordList()

                                    If Not listClip Is Nothing Then

                                        Dim rowCount As Integer = listClip.Count - 1
                                        Dim colCount As Integer = listClip(0).Length

                                        '単一コピーの場合'
                                        If rowCount = 1 Then
                                            For col As Integer = 0 To selection.ColumnCount - 1
                                                For rowindex As Integer = 0 To selection.RowCount - 1
                                                    If Not Me.spdBaseCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Locked Then
                                                        '隠された行にはペーストしない'
                                                        If Me.spdBaseCar_Sheet1.Rows(selection.Row + rowindex).Visible Then
                                                            Me.spdBaseCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                        End If
                                                    End If
                                                Next
                                            Next
                                        ElseIf rowCount > 1 Then
                                            '複数コピーの場合'
                                            For col As Integer = 0 To selection.ColumnCount - 1
                                                For rowindex As Integer = 0 To selection.RowCount - 1
                                                    If Not Me.spdBaseCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Locked Then
                                                        '隠された行にはペーストしない'
                                                        If Me.spdBaseCar_Sheet1.Rows(selection.Row + rowindex).Visible Then
                                                            Me.spdBaseCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                        Else
                                                            '非表示なら'

                                                        End If
                                                    End If
                                                Next
                                            Next
                                        End If

                                        'セル編集モード時にコピーした場合、以下を行う。
                                        If rowCount = 0 Then
                                            rowCount = 1
                                        End If

                                        '行選択時
                                        If selection.Column = -1 Then
                                            If Not StringUtil.IsEmpty(headerSubject.StatusName) Then
                                                '貼りつけ対象のセルを編集済みとし書式を設定する
                                                Me.spdBaseCar_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                                                Me.spdBaseCar_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).ForeColor = Color.Blue
                                            End If
                                        Else
                                            If (selection.Row + rowCount) >= sheet.RowCount - 1 Then
                                                EBom.Common.ComFunc.ShowInfoMsgBox("貼り付けしようとしている範囲がスプレッド表の最大行を超えています")
                                                Return
                                            End If

                                            If Not StringUtil.IsEmpty(headerSubject.StatusName) Then
                                                '貼りつけ対象のセルを編集済みとし書式を設定する
                                                Me.spdBaseCar_Sheet1.Cells(selection.Row, selection.Column, selection.Row + rowCount - 1, _
                                                                       selection.Column + colCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

                                                Me.spdBaseCar_Sheet1.Cells(selection.Row, selection.Column, selection.Row + rowCount - 1, _
                                                                selection.Column + colCount - 1).ForeColor = Color.Blue
                                            End If
                                        End If
                                    End If

                                End If
                            End If
                        Else

                        End If

                    End If

                Case Keys.Delete
                    '設計展開以降のイベントはKeyDownを無効に'
                    If Not headerSubject.IsSekkeiTenkaiIkou Then
                        '行選択・列選択ではDeleteは無効に
                        If Not selection Is Nothing Then
                            If (selection.Column = -1 AndAlso selection.ColumnCount - 1) Then
                                e.Handled = True
                            End If

                            If (selection.Row = -1 AndAlso selection.RowCount - 1) Then
                                e.Handled = True
                            End If
                        End If
                    Else
                        e.Handled = True
                    End If
            End Select

        End Sub

        '''' <summary>
        '''' ベース車情報変更
        '''' </summary>
        '''' <param name="sender"></param>
        '''' <param name="e"></param>
        '''' <remarks></remarks>
        'Private Sub spdBaseCar_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdBaseCar.Change

        '    Dim sheet As Spread.SheetView = spdBaseCar.ActiveSheet

        '    ' 選択セルの場所を特定します。
        '    ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
        '    ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

        '    ' 該当セルの文字色、文字太を変更する。
        '    sheet.SetStyleInfo(ParaActRowIdx, ParaActColIdx, CreateNewStyle())
        '    sheet.Cells(ParaActRowIdx, ParaActColIdx).Locked = False        '2012/01/09

        '    If e.Column = 0 Then
        '        SetActiveSpreadColor(sheet, sheet.ActiveRowIndex, 0)
        '    End If

        'End Sub

        '2012/01/09
        ''' <summary>
        ''' 行背景色をグレーに変更する(種別が "D" の場合のみ)
        ''' </summary>
        ''' <param name="activeSheet">色を設定するSpreadSheet</param>
        ''' <param name="targetRow">対象の行位置</param>
        ''' <param name="targetColumn">種別のカラム位置</param>
        ''' <remarks></remarks>
        Private Sub SetActiveSpreadColor(ByVal activeSheet As SheetView, ByVal targetRow As Long, ByVal targetColumn As Long)
            Const DEF_TYPE_D As String = "D"
            Dim nColumn As Long = 0
            Dim nColumn2 As Long = 0

            If activeSheet.ColumnCount - 1 > 0 Then
                nColumn2 = activeSheet.ColumnCount - 1
            End If

            If activeSheet.Cells(targetRow, targetColumn, targetRow, targetColumn).Text = DEF_TYPE_D Then
                activeSheet.Cells(targetRow, nColumn, targetRow, nColumn2).BackColor = Color.DimGray       'ディムグレー
                activeSheet.Cells(targetRow, nColumn, targetRow, nColumn2).ForeColor = Color.LightGray     '文字色も薄くグレー
            Else
                activeSheet.Cells(targetRow, nColumn, targetRow, nColumn2).ResetBackColor()
                activeSheet.Cells(targetRow, nColumn, targetRow, nColumn2).ResetForeColor()
            End If
        End Sub

        ''' <summary>
        ''' 文字列チェック
        ''' </summary>
        ''' <param name="value">対象文字列</param>
        ''' <returns>ALL半角ならTrue</returns>
        ''' <remarks></remarks>
        Private Function SpellCheck(ByVal value As String) As Boolean
            '文字列の長さ
            Dim valueLength As Integer = value.Length
            Dim Enc As Encoding = Encoding.GetEncoding("Shift_JIS")

            For i As Integer = 0 To valueLength - 1
                Dim c As String = Mid(value, i + 1, 1)

                '半角か全角かチェック'
                If Not StringUtil.IsEmpty(c) Then
                    If Enc.GetByteCount(c) = 1 Then

                    Else
                        Return False
                    End If
                End If
            Next
            Return True
        End Function

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
        Private Sub ToolMenuCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
        Private Sub ToolMenuPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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

                Dim spd As FpSpread = GetVisibleSpread

                If spd.ActiveSheet.GetSelection(0) Is Nothing Then
                    ComFunc.ShowInfoMsgBox("セル範囲が指定されていません。")
                    Exit Sub
                End If

                ToolCutHontai()

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

                Dim spd As FpSpread = GetVisibleSpread

                If spd.ActiveSheet.GetSelection(0) Is Nothing Then
                    ComFunc.ShowInfoMsgBox("セル範囲が指定されていません。")
                    Exit Sub
                End If

                ToolCopyHontai()

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("コピーが正常に行えませんでした。")
            End Try

        End Sub

#End Region

#Region "貼りつけイベント処理"

        Private Sub ToolPaste()
            Try

                Dim spd As FpSpread = GetVisibleSpread

                If spd.ActiveSheet.GetSelection(0) Is Nothing Then
                    ComFunc.ShowInfoMsgBox("セル範囲が指定されていません。")
                    Exit Sub
                End If

                ToolPasteHontai(GetVisibleSheet)

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("貼りつけが正常に行えませんでした。")
            End Try
        End Sub
#End Region

#Region "ボタン（コピー）"
        ''' <summary>
        ''' ボタン（コピー）
        ''' 
        ''' キーボードでCTRL + c を押した事にし
        ''' この後KeyDownイベントに流す
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ToolCopyHontai()

            Dim spd As FpSpread = GetVisibleSpread

            spd.Focus()

            System.Threading.Thread.Sleep(10)
            System.Windows.Forms.SendKeys.Flush()

            ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！
            System.Windows.Forms.SendKeys.Send("^c")

        End Sub

#End Region

#Region "ボタン（切り取り）"
        ''' <summary>
        ''' ボタン（切り取り）
        ''' 
        ''' キーボードでCTRL + Xを押した事にし
        ''' この後KeyDownイベントに流す
        ''' 
        ''' 
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ToolCutHontai()

            Dim spd As FpSpread = GetVisibleSpread

            spd.Focus()

            System.Threading.Thread.Sleep(10)

            System.Windows.Forms.SendKeys.Flush()
            ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！
            System.Windows.Forms.SendKeys.Send("^x")
            ''カットした後に同じ箇所をDELETEしてみる。
            'System.Windows.Forms.SendKeys.Send("{DELETE}")

        End Sub

#End Region

#Region "ボタン（貼りつけ）"
        ''' <summary>
        ''' ボタン（貼りつけ）
        ''' 
        ''' キーボードでCTRL + vを押した事にし
        ''' この後KeyDownイベントに流す
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ToolPasteHontai(ByVal aSheet As Spread.SheetView)

            Dim spd As FpSpread = GetVisibleSpread

            spd.Focus()

            System.Windows.Forms.SendKeys.Flush()
            System.Threading.Thread.Sleep(10)
            ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！
            System.Windows.Forms.SendKeys.Send("^v")


        End Sub
#End Region

#Region "表示されているスプレッドを返す"
        ''' <summary>
        ''' 表示されているスプレッドを返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetVisibleSpread() As Spread.FpSpread
            Get
                Return Me.spdBaseCar
            End Get
        End Property
#End Region

#Region "表示されているシートオブジェクトを返す"
        ''' <summary>
        ''' 表示されているシートを返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetVisibleSheet() As Spread.SheetView
            Get
                Return Me.spdBaseCar_Sheet1
            End Get
        End Property
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
                    Dim objFont As System.Drawing.Font = aSheet.Cells(aSheet.ActiveRowIndex, col + j).Font

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

            If Not selection Is Nothing Then
                If selection.ColumnCount = -1 Then
                    colCnt = aSheet.ColumnCount
                    col = 0
                Else
                    colCnt = selection.ColumnCount
                    col = selection.Column
                End If
                '無い場合は全て保存対象編集書式とするため全てTrueをセット
                If alistBln Is Nothing Then
                    alistBln = New List(Of Boolean())

                    For i As Integer = 0 To selection.RowCount - 1

                        Dim blnTbl() As Boolean = Nothing
                        ReDim Preserve blnTbl(colCnt - 1)

                        For j As Integer = 0 To colCnt - 1
                            blnTbl(j) = True
                        Next
                        alistBln.Add(blnTbl)
                    Next

                End If

                '受け取ったListの内容で書式を設定
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
                colCnt = 1
                col = aSheet.ActiveColumnIndex
                '無い場合は全て保存対象編集書式とするため全てTrueをセット
                If alistBln Is Nothing Then
                    alistBln = New List(Of Boolean())


                    Dim blnTbl() As Boolean = Nothing
                    ReDim Preserve blnTbl(colCnt - 1)

                    For j As Integer = 0 To colCnt - 1
                        blnTbl(j) = True
                    Next
                    alistBln.Add(blnTbl)

                End If

                '受け取ったListの内容で書式を設定

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

        Private Sub spdBaseCar_EditModeOn(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdBaseCar.EditModeOn
            Dim sheet As Spread.SheetView = spdBaseCar_Sheet1
            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            'IMEを使用不可能にする。
            spdBaseCar.ImeMode = Windows.Forms.ImeMode.Disable
        End Sub
        Private Sub spdBaseCar_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdBaseCar.EditModeOff
            spdBaseCar.EditingControl.ImeMode = Windows.Forms.ImeMode.NoControl
            spdBaseCar.ImeMode = Windows.Forms.ImeMode.NoControl
        End Sub

        Private Function IsAddMode() As Boolean
            Return StringUtil.IsEmpty(ShisakuEventCode)
        End Function
    End Class
End Namespace
