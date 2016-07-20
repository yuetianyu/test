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

#Region "出図織込み実績差照会クラス"
    ''' <summary>
    ''' 出図織込み実績差照会クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm20ShutuzuOrikomi

#Region "プライベート変数"
        '''<summary>初期化完了フラグ</summary>>
        Private _InitComplete As Boolean = False
        ''' <summary>フォーム表示時に呼び出されるID</summary>
        Private m_initSwitchId As String = String.Empty

        ''' <summary>画面制御ロジック</summary>
        Private _TehaiEditLogic As TehaichoEditLogic
        ''' <summary>マウスクリック行列位置保存</summary>
        Private _clickCellPosition As Spread.Model.CellRange
        ''' <summary>出図織込シート</summary>
        Private ShutuzuOrikomiSheet As SheetView

        Private _ResultOk As Boolean

        '手配基本情報
        Private _TehaiKihonVos As List(Of TShisakuTehaiKihonVo)
        '
        Private _ResultList As List(Of TShisakuTehaiKihonVo)

#End Region

#Region "コンストラクタ"

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="ResultList">手配基本情報</param>
        ''' <param name="tehaiEditLogic">画面制御ロジック</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal ResultList As List(Of TShisakuTehaiKihonVo), ByVal tehaiEditLogic As TehaichoEditLogic)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            Me.Refresh()
            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)

            Me._TehaiKihonVos = ResultList

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

        ''' <summary>
        ''' 選択された情報を返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ResultList()
            Get
                Return _ResultList
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

                Dim sheet As FarPoint.Win.Spread.SheetView = spdShutuzuOrikomi_Sheet1
                NmSpdTagShutuzuOrikomiSa.initTagName(sheet)

                ''画面のPG-IDが表示されます。
                ShisakuFormUtil.setTitleVersion(Me)
                LblCurrPGId.Text = "PG-ID :" + PROGRAM_ID_20

                ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
                ShisakuFormUtil.SetIdAndBuka(LblCurrUserId, LblCurrBukaName)

                'スプレッダーを初期化する
                InitSpreadColDefault()

                '初期データ表示スプレッド(出図織込み)
                If SetSpreadShutuzuOrikomi() = False Then
                    '_ResultOk = False
                    ''初期化失敗とする。
                    'Exit Sub
                End If

                'スプレッドの読取専用列設定
                _TehaiEditLogic.LockColShutuzuOrikomiSpread(Me)

                'Enterキーの動作を、「編集」から「次行へ移動」にする
                ShisakuCommon.Ui.Spd.SpreadUtil.BindKeyEnterIsNextRow(spdShutuzuOrikomi)

                'Shift + Enterキーの動作を、「前行へ移動」にする
                ShisakuCommon.Ui.Spd.SpreadUtil.BindKeyShiftEnterIsPreviousRow(spdShutuzuOrikomi)

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


#Region "スプレッド列の初期設定"
        ''' <summary>
        ''' スプレッド列の初期設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitSpreadColDefault()
            Dim sheet As Spread.SheetView = spdShutuzuOrikomi_Sheet1
            Dim chkType As New CheckBoxCellType()
            chkType.Caption = ""
            Dim btnType As New ButtonCellType
            btnType.Text = "表示"
            btnType.TextDown = "表示"
            btnType.ButtonColor = Color.LightGray
            btnType.ButtonColor2 = Color.LightGray

            '表示
            sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_HYOJI).CellType = btnType
            '確定
            sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_KAKUTEI).CellType = chkType

        End Sub
#End Region


        ''' <summary>
        ''' 出図織込み情報スプレッドデータ格納
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SetSpreadShutuzuOrikomi() As Boolean

            '情報が無ければ終了
            If _TehaiKihonVos.Count = 0 Then
                ComFunc.ShowInfoMsgBox("条件に合うデータがありません。")
                'Return True
            End If

            Dim sheet As FarPoint.Win.Spread.SheetView = Me.spdShutuzuOrikomi_Sheet1
            Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(sheet)
            Dim oRow As Integer = startRow

            'スプレッドを他の画面と同様に設定
            Dim spread As FarPoint.Win.Spread.FpSpread = Me.spdShutuzuOrikomi
            SpreadUtil.Initialize(spread)

            '取得したデータ件数によりスプレッドの総行数を拡張(）
            sheet.RowCount = startRow + _TehaiKihonVos.Count

            For Each vo As TShisakuTehaiKihonVo In _TehaiKihonVos
                '表示
                '確定
                If StringUtil.IsNotEmpty(_TehaiEditLogic.TehaiShutuzuJisekiOrikomiSa(vo.ShisakuBlockNo, vo.GyouId, vo.BuhinNo)) Then
                    Dim kakutei As String = _TehaiEditLogic.TehaiShutuzuJisekiOrikomiSa(vo.ShisakuBlockNo, vo.GyouId, vo.BuhinNo).Kakutei
                    '１：☑、２：□
                    If StringUtil.Equals(kakutei, "1") Then
                        sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                                      NmSpdTagShutuzuOrikomiSa.TAG_KAKUTEI)).Value = True
                    Else
                        sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                                      NmSpdTagShutuzuOrikomiSa.TAG_KAKUTEI)).Value = False
                    End If
                Else
                    sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                                  NmSpdTagShutuzuOrikomiSa.TAG_KAKUTEI)).Value = False
                End If

                'ブロック№
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuOrikomiSa.TAG_SHISAKU_BLOCK_NO)).Value = vo.ShisakuBlockNo
                '行ID
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuOrikomiSa.TAG_GYOU_ID)).Value = vo.GyouId
                '部品番号
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuOrikomiSa.TAG_BUHIN_NO)).Value = vo.BuhinNo

                '最新出図_改訂№
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuOrikomiSa.TAG_NEW_SHUTUZU_JISEKI_KAITEI_NO)).Value = vo.ShutuzuJisekiKaiteiNo
                '最新出図_設通№
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuOrikomiSa.TAG_NEW_SHUTUZU_JISEKI_STSR_DHSTBA)).Value = vo.ShutuzuJisekiStsrDhstba
                '最新出図_受領日
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuOrikomiSa.TAG_NEW_SHUTUZU_JISEKI_JYURYO_DATE)).Value = TehaichoEditLogic.ConvDateInt8(vo.ShutuzuJisekiDate)

                '代表品番と件名を取得
                If StringUtil.IsNotEmpty(_TehaiEditLogic.TehaiShutuzuJisekiKaitei(vo.ShisakuBlockNo, vo.BuhinNo, vo.ShutuzuJisekiKaiteiNo)) Then

                    Dim daihyoHinban As String = String.Empty
                    Dim kenmei As String = String.Empty

                    If StringUtil.IsNotEmpty(_TehaiEditLogic.TehaiShutuzuJisekiKaitei(vo.ShisakuBlockNo, vo.BuhinNo, vo.ShutuzuJisekiKaiteiNo)) Then
                        daihyoHinban = _TehaiEditLogic.TehaiShutuzuJisekiKaitei(vo.ShisakuBlockNo, vo.BuhinNo, vo.ShutuzuJisekiKaiteiNo).KataDaihyouBuhinNo
                        kenmei = _TehaiEditLogic.TehaiShutuzuJisekiKaitei(vo.ShisakuBlockNo, vo.BuhinNo, vo.ShutuzuJisekiKaiteiNo).ShutuzuKenmei
                    End If

                    '代表品番
                    sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                                  NmSpdTagShutuzuOrikomiSa.TAG_KATA_DAIHYOU_BUHIN_NO)).Value = daihyoHinban
                    '最新出図_件名
                    sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                                  NmSpdTagShutuzuOrikomiSa.TAG_NEW_SHUTUZU_KENMEI)).Value = kenmei
                End If


                '最終織込設変_改訂№
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_JISEKI_KAITEI_NO)).Value = vo.SaisyuSetsuhenKaiteiNo
                '最終織込設変_設通№
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_JISEKI_STSR_DHSTBA)).Value = vo.StsrDhstba
                '最終織込設変_受領日
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_JISEKI_JYURYO_DATE)).Value = TehaichoEditLogic.ConvDateInt8(vo.SaisyuSetsuhenDate)
                '代表品番と件名を取得
                If StringUtil.IsNotEmpty(_TehaiEditLogic.TehaiShutuzuJisekiKaitei(vo.ShisakuBlockNo, vo.BuhinNo, vo.SaisyuSetsuhenKaiteiNo)) Then
                    Dim kenmei As String = String.Empty
                    If StringUtil.IsNotEmpty(_TehaiEditLogic.TehaiShutuzuJisekiKaitei(vo.ShisakuBlockNo, vo.BuhinNo, vo.SaisyuSetsuhenKaiteiNo)) Then
                        kenmei = _TehaiEditLogic.TehaiShutuzuJisekiKaitei(vo.ShisakuBlockNo, vo.BuhinNo, vo.SaisyuSetsuhenKaiteiNo).ShutuzuKenmei
                    End If

                    '最終織込設変_件名
                    sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                                  NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_KENMEI)).Value = kenmei
                End If
                '
                oRow += 1

            Next

            Return True

        End Function

#End Region

#Region "イベント"

#Region "フォームロード"
        ''' <summary>
        ''' フォームロード
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Frm20ShutuzuOrikomi_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

            If frm01Kakunin.ConfirmOkCancel("終了しますか？") = MsgBoxResult.Ok Then
                _ResultOk = False
                Me.Close()
                Return
            End If

        End Sub

#End Region


#Region "ボタン(保存)"


        ''' <summary>
        ''' ボタン（保存）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnHozon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHozon.Click

            Try

                '保存確認フォームを表示
                Dim result As Integer = frm00Kakunin.Confirm("確認", "保存を実行しますか。", _
                                               "", "OK", "CANCEL")

                If result = MsgBoxResult.Ok Then  'OK

                    ''選択情報SPREADより最新出図と最終織込の改訂№アンマッチのデータを抽出
                    'GetSpreadSelectVos()

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

        ''' <summary>
        ''' スプレッドから改訂№アンマッチ行の情報を取得する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub GetSpreadSelectVos()

            _ResultList = New List(Of TShisakuTehaiKihonVo)

            Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(spdShutuzuOrikomi_Sheet1)

            For rowIndex As Integer = startRow To spdShutuzuOrikomi_Sheet1.RowCount - 1
                If spdShutuzuOrikomi_Sheet1.Cells(rowIndex, NmSpdTagShutuzuOrikomiSa.TAG_NEW_SHUTUZU_JISEKI_KAITEI_NO).Value _
                <> spdShutuzuOrikomi_Sheet1.Cells(rowIndex, NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_JISEKI_KAITEI_NO).Value Then

                    Dim addVo As New TShisakuTehaiKihonVo
                    'ブロック
                    addVo.ShisakuBlockNo = Me.spdShutuzuOrikomi_Sheet1.Cells(rowIndex, NmSpdTagShutuzuOrikomiSa.TAG_SHISAKU_BLOCK_NO).Value
                    '行ＩＤ
                    addVo.GyouId = Me.spdShutuzuOrikomi_Sheet1.Cells(rowIndex, NmSpdTagShutuzuOrikomiSa.TAG_GYOU_ID).Value
                    '部品番号
                    addVo.BuhinNo = Me.spdShutuzuOrikomi_Sheet1.Cells(rowIndex, NmSpdTagShutuzuOrikomiSa.TAG_BUHIN_NO).Value

                    '最新出図_改訂№
                    addVo.ShutuzuJisekiKaiteiNo = Me.spdShutuzuOrikomi_Sheet1.Cells(rowIndex, NmSpdTagShutuzuOrikomiSa.TAG_NEW_SHUTUZU_JISEKI_KAITEI_NO).Value
                    '最新出図_設通№
                    addVo.ShutuzuJisekiStsrDhstba = Me.spdShutuzuOrikomi_Sheet1.Cells(rowIndex, NmSpdTagShutuzuOrikomiSa.TAG_NEW_SHUTUZU_JISEKI_STSR_DHSTBA).Value
                    '最新出図_受領日
                    addVo.ShutuzuJisekiDate = TehaichoEditLogic.ConvInt8Date(Me.spdShutuzuOrikomi_Sheet1.Cells(rowIndex, NmSpdTagShutuzuOrikomiSa.TAG_NEW_SHUTUZU_JISEKI_JYURYO_DATE).Value)

                    '最終織込出図_改訂№
                    addVo.SaisyuSetsuhenKaiteiNo = Me.spdShutuzuOrikomi_Sheet1.Cells(rowIndex, NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_JISEKI_KAITEI_NO).Value
                    '最終織込_設通№
                    addVo.StsrDhstba = Me.spdShutuzuOrikomi_Sheet1.Cells(rowIndex, NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_JISEKI_STSR_DHSTBA).Value
                    '最終織込_受領日
                    addVo.SaisyuSetsuhenDate = TehaichoEditLogic.ConvInt8Date(Me.spdShutuzuOrikomi_Sheet1.Cells(rowIndex, NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_JISEKI_JYURYO_DATE).Value)

                    _ResultList.Add(addVo)
                End If
            Next
        End Sub

#End Region

#Region "スプレッドChange"

        Private Sub spdShutuzuOrikomi_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdShutuzuOrikomi.CellDoubleClick
            Try
                If e.ColumnHeader Then
                    Exit Sub
                End If

                '出力対象選択画面の選択された情報
                Dim selectedVos As List(Of TShisakuTehaiKihonVo)

                Dim sheet As SheetView = Me.spdShutuzuOrikomi_Sheet1
                Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(sheet)
                'ヘッダーセルダブルクリック時はスルー
                If e.Row < startRow Then
                    Exit Sub
                End If
                'ダブルクリック行のブロック№、行ID、部品番号、代表品番を取得
                Dim gyoId As String = _
                            sheet.GetText(e.Row, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagShutuzuOrikomiSa.TAG_GYOU_ID)).Trim
                Dim blockNo As String = _
                            sheet.GetText(e.Row, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagShutuzuOrikomiSa.TAG_SHISAKU_BLOCK_NO)).Trim
                Dim buhinNo As String = _
                            sheet.GetText(e.Row, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagShutuzuOrikomiSa.TAG_BUHIN_NO)).Trim
                Dim daihyoHinban As String = _
                            sheet.GetText(e.Row, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagShutuzuOrikomiSa.TAG_KATA_DAIHYOU_BUHIN_NO)).Trim
                Dim lastKaiteiNo As String = _
                            sheet.GetText(e.Row, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_JISEKI_KAITEI_NO)).Trim

                '画面遷移
                Using frmShutuzuKaiteiRireki As New Frm20ShutuzuKaiteiRireki(_TehaiEditLogic, gyoId, blockNo, buhinNo, lastKaiteiNo, daihyoHinban)
                    '
                    If frmShutuzuKaiteiRireki.InitComplete = True Then

                        frmShutuzuKaiteiRireki.ShowDialog()

                    End If
                    '戻るの場合、処理を終了
                    If frmShutuzuKaiteiRireki.ResultOk = False And frmShutuzuKaiteiRireki.ResultOkHanei = False Then
                        Return
                    End If

                    If frmShutuzuKaiteiRireki.ResultOk Then
                        'コメントを更新、手入力情報を更新
                        _TehaiEditLogic.UpdShutuzuKaiteiRireki(frmShutuzuKaiteiRireki, blockNo, buhinNo)
                    End If

                    If frmShutuzuKaiteiRireki.ResultOkHanei Then
                        'VOを返す
                        selectedVos = frmShutuzuKaiteiRireki.ResultList

                        If selectedVos.Count > 0 Then
                            '「T_SHISAKU_TEHAI_KIHON」情報を更新
                            If _TehaiEditLogic.UpdateDataSaishinOrikomi(selectedVos) = False Then
                                Return
                            End If

                            '自前の最終織込情報も変更する。
                            spdShutuzuOrikomi_Sheet1.SetValue(e.Row, spdShutuzuOrikomi_Sheet1.Columns(NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_JISEKI_JYURYO_DATE).Index, _
                                                              TehaichoEditLogic.ConvDateInt8(selectedVos(0).SaisyuSetsuhenDate))
                            spdShutuzuOrikomi_Sheet1.SetValue(e.Row, spdShutuzuOrikomi_Sheet1.Columns(NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_JISEKI_KAITEI_NO).Index, _
                                                              selectedVos(0).SaisyuSetsuhenKaiteiNo)
                            spdShutuzuOrikomi_Sheet1.SetValue(e.Row, spdShutuzuOrikomi_Sheet1.Columns(NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_JISEKI_STSR_DHSTBA).Index, _
                                                              selectedVos(0).StsrDhstba)
                        End If

                    End If

                    ''画面を更新
                    'Initialize()
                End Using

                Cursor.Current = Cursors.Default

            Catch ex As Exception
                ComFunc.ShowErrMsgBox(String.Format("出図改訂履歴処理でエラーが発生しました。", vbCrLf, ex.Message))
            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Sub

        ''' <summary>
        '''  スプレッドChange
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdShutuzuOrikomi_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdShutuzuOrikomi.Change

            Try

                Dim actCol As Integer = spdShutuzuOrikomi_Sheet1.ActiveColumn.Index
                Dim actRow As Integer = spdShutuzuOrikomi_Sheet1.ActiveRow.Index
                Dim actColTag As String = spdShutuzuOrikomi_Sheet1.Columns(actCol).Tag.ToString

                'セル変更対応処理
                Select Case actColTag

                    Case NmSpdTagShutuzuOrikomiSa.TAG_HYOJI
                    Case NmSpdTagShutuzuOrikomiSa.TAG_KAKUTEI
                        ''トリム納入指示日
                        'Dim valNounyu As String = spdShutuzuOrikomi_Sheet1.GetValue(actRow, _
                        '                                                    spdShutuzuOrikomi_Sheet1.Columns(NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI).Index)
                        ''Nullや空文字の場合
                        'If valNounyu Is Nothing OrElse valNounyu.ToString.Trim.Equals(String.Empty) Then
                        '    spdShutuzuOrikomi_Sheet1.SetValue(actRow, spdShutuzuOrikomi_Sheet1.Columns(NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI).Index, String.Empty)
                        'Else
                        '    Dim dateNounyu As Date = spdShutuzuOrikomi_Sheet1.GetValue(actRow, spdShutuzuOrikomi_Sheet1.Columns(NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI).Index)

                        '    'カレンダーから入力された値から時刻を取り除き格納する
                        '    If IsDate(dateNounyu) = True Then
                        '        spdShutuzuOrikomi_Sheet1.SetValue(actRow, spdShutuzuOrikomi_Sheet1.Columns(NmSpdTagGousyaNoukiSettei.TAG_T_NOUNYU_SHIJIBI).Index, Format(dateNounyu, "yyyy/MM/dd"))
                        '    End If
                        '    '編集されたセルは太文字・青文字にする
                        '    spdShutuzuOrikomi_Sheet1.Cells(actRow, actCol, actRow, actCol).ForeColor = Color.Blue
                        '    spdShutuzuOrikomi_Sheet1.Cells(actRow, actCol, actRow, actCol).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                        'End If

                End Select

            Catch ex As Exception
                MsgBox("値変更処理で問題が発生しました")
                Me.Close()
            End Try

        End Sub
#End Region


#Region "スプレッドマウスクリックイベント情報取得"


        ''' <summary>
        '''  スプレッドマウスクリックイベント情報取得
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdShutuzuOrikomi_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles spdShutuzuOrikomi.MouseDown
            Dim sv As New FarPoint.Win.Spread.SpreadView(Me.spdShutuzuOrikomi)
            sv = Me.spdShutuzuOrikomi.GetRootWorkbook()

            Dim sheet As SheetView = Me.spdShutuzuOrikomi_Sheet1

            'セル行列位置取得
            _clickCellPosition = sv.GetCellFromPixel(e.X, e.Y)

            If _clickCellPosition.Row = -1 Then
                _clickCellPosition = sv.GetRowHeaderCellFromPixel(e.X, e.Y)
            Else
                'ボタンは０列目、部品番号は４列目'
                If _clickCellPosition.Column = 0 AndAlso _clickCellPosition.Row > 1 Then
                    _TehaiEditLogic.SettsuLogon(sheet.Cells(_clickCellPosition.Row, _
                                    TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagShutuzuOrikomiSa.TAG_BUHIN_NO)).Value, LoginInfo.Now.UserId)
                End If
                '
                _clickCellPosition = Nothing
            End If

        End Sub
#End Region

#End Region

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

    End Class

#End Region

End Namespace

