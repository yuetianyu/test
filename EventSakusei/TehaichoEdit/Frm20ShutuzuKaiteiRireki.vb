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

#Region "出図改訂履歴クラス"
    ''' <summary>
    ''' 出図改訂履歴クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm20ShutuzuKaiteiRireki

#Region "プライベート変数"
        '''<summary>初期化完了フラグ</summary>>
        Private _InitComplete As Boolean = False
        ''' <summary>フォーム表示時に呼び出されるID</summary>
        Private m_initSwitchId As String = String.Empty

        ''' <summary>画面制御ロジック</summary>
        Private _TehaiEditLogic As TehaichoEditLogic
        ''' <summary>マウスクリック行列位置保存</summary>
        Private _clickCellPosition As Spread.Model.CellRange

        Private _ResultOk As Boolean

        Private _ResultOkHanei As Boolean

        Private _gyoId As String
        Private _blockNo As String
        Private _buhinNo As String
        Private _daihyoHinban As String
        Private _saisyuShutuzuKaiteiNo As String
        '
        Private _ResultList As List(Of TShisakuTehaiKihonVo)

#End Region

#Region "コンストラクタ"

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="tehaiEditLogic">画面制御ロジック</param>
        ''' <param name="gyoId">行ID</param>
        ''' <param name="blockNo">ブロック№</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="saisyuShutuzuKaiteiNo">最終織込改訂№</param>
        ''' <param name="daihyoHinban">代表品番</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal tehaiEditLogic As TehaichoEditLogic, _
                       ByVal gyoId As String, _
                       ByVal blockNo As String, _
                       ByVal buhinNo As String, _
                       ByVal saisyuShutuzuKaiteiNo As String, _
                       ByVal daihyoHinban As String)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            Me.Refresh()
            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)

            Me._TehaiEditLogic = tehaiEditLogic

            Me._gyoId = gyoId
            Me._blockNo = blockNo
            Me._buhinNo = buhinNo
            Me._daihyoHinban = daihyoHinban
            Me._saisyuShutuzuKaiteiNo = saisyuShutuzuKaiteiNo

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
        ''' OKボタンでTRUEを返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ResultOkHanei()
            Get
                Return _ResultOkHanei
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

                Dim sheet As FarPoint.Win.Spread.SheetView = spdShutuzuKaiteiRireki_Sheet1
                NmSpdTagShutuzuKaiteiRireki.initTagName(sheet)

                ''画面のPG-IDが表示されます。
                ShisakuFormUtil.setTitleVersion(Me)
                LblCurrPGId.Text = "PG-ID :" + PROGRAM_ID_20

                ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
                ShisakuFormUtil.SetIdAndBuka(LblCurrUserId, LblCurrBukaName)

                'ヘッダー
                lblGyoId.Text = _gyoId
                lblBlockNo.Text = _blockNo
                lblBuhinNo.Text = _buhinNo
                lblDaihyoBuhin.Text = _daihyoHinban

                'スプレッダーを初期化する
                InitSpreadColDefault()

                '初期データ表示スプレッド(出図改訂履歴)
                If SetSpreadShutuzuKaiteiRireki() = False Then
                    '_ResultOk = False
                    ''初期化失敗とする。
                    'Exit Sub
                End If

                'スプレッドの読取専用列設定
                _TehaiEditLogic.LockColShutuzuKaiteiRirekiSpread(Me)

                'Enterキーの動作を、「編集」から「次行へ移動」にする
                ShisakuCommon.Ui.Spd.SpreadUtil.BindKeyEnterIsNextRow(spdShutuzuKaiteiRireki)

                'Shift + Enterキーの動作を、「前行へ移動」にする
                ShisakuCommon.Ui.Spd.SpreadUtil.BindKeyShiftEnterIsPreviousRow(spdShutuzuKaiteiRireki)

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
            Dim sheet As Spread.SheetView = spdShutuzuKaiteiRireki_Sheet1
            Dim chkType As New CheckBoxCellType()
            chkType.Caption = ""

            '織込済
            sheet.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_ORIKOMI).CellType = chkType

            '受領日
            Dim jyuryoDate As New Spread.CellType.DateTimeCellType
            jyuryoDate.DropDownButton = True
            sheet.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_JYURYO_DATE).CellType = jyuryoDate

            '設通№
            Dim dhstbaTextType As New TextCellType
            dhstbaTextType.CharacterSet = CharacterSet.AlphaNumeric
            dhstbaTextType.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.None
            dhstbaTextType.MaxLength = 10
            dhstbaTextType.CharacterCasing = CharacterCasing.Upper
            sheet.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_STSR_DHSTBA).CellType = dhstbaTextType

            '件名＆コメント
            Dim textType As New TextCellType
            textType.CharacterSet = CharacterSet.AllIME
            textType.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.None
            textType.MaxLength = 256
            textType.CharacterCasing = CharacterCasing.Upper
            sheet.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_KENMEI).CellType = textType
            sheet.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_COMMENT).CellType = textType

        End Sub
#End Region


        ''' <summary>
        ''' 出図改訂履歴情報スプレッドデータ格納
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SetSpreadShutuzuKaiteiRireki() As Boolean

            ''出図実績を取得
            Dim JisekiVos As New List(Of TShisakuTehaiShutuzuJisekiVo)
            JisekiVos = _TehaiEditLogic.TehaiShutuzuJiseki(_blockNo, _buhinNo, False)   'false:改訂№の昇順指定
            ''情報が無ければコメント出す？
            'If vos.Count = 0 Then
            '    ComFunc.ShowInfoMsgBox("条件に合うデータがありません。")
            '    'Return True
            'End If

            Dim sheet As FarPoint.Win.Spread.SheetView = Me.spdShutuzuKaiteiRireki_Sheet1
            Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(sheet)
            Dim oRow As Integer = startRow
            Dim lastKaiteiNo As String = String.Empty

            'スプレッドを他の画面と同様に設定
            Dim spread As FarPoint.Win.Spread.FpSpread = Me.spdShutuzuKaiteiRireki
            SpreadUtil.Initialize(spread)

            '取得したデータ件数によりスプレッドの総行数を拡張(）
            '   手入力行として１行プラスする。
            sheet.RowCount = startRow + JisekiVos.Count + 1

            For Each vo As TShisakuTehaiShutuzuJisekiVo In JisekiVos
                '織込
                If StringUtil.Equals(vo.ShutuzuJisekiKaiteiNo, _saisyuShutuzuKaiteiNo) Then
                    sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                                  NmSpdTagShutuzuKaiteiRireki.TAG_ORIKOMI)).Value = True
                Else
                    sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                                  NmSpdTagShutuzuKaiteiRireki.TAG_ORIKOMI)).Value = False
                End If
                '出図_改訂№
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_KAITEI_NO)).Value = vo.ShutuzuJisekiKaiteiNo
                '   改訂№を保持
                lastKaiteiNo = vo.ShutuzuJisekiKaiteiNo

                '出図_設通№
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)).Value = vo.ShutuzuJisekiStsrDhstba
                '   ロックする
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)).Locked = True
                '出図_受領日
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_JYURYO_DATE)).Value = TehaichoEditLogic.ConvDateInt8(vo.ShutuzuJisekiJyuryoDate)
                '   ロックする
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_JYURYO_DATE)).Locked = True
                '出図_件名
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_KENMEI)).Value = vo.ShutuzuKenmei
                '   ロックする
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_KENMEI)).Locked = True
                'コメント
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuKaiteiRireki.TAG_COMMENT)).Value = vo.Comment
                '   コメントはロックしない
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuKaiteiRireki.TAG_COMMENT)).Locked = False

                '手入力情報があるか？
                Dim JisekiInputVo As New TShisakuTehaiShutuzuJisekiInputVo
                JisekiInputVo = _TehaiEditLogic.TehaiShutuzuJisekiInput(_blockNo, _buhinNo, vo.ShutuzuJisekiKaiteiNo, False) 'false:改訂№で検索
                '赤ぽっちで手入力情報を出力する
                If StringUtil.IsNotEmpty(JisekiInputVo) Then
                    '出図_設通№
                    sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                            NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)).NoteStyle = NoteStyle.PopupNote
                    sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                            NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)).Note = _
                                                JisekiInputVo.CreatedDate & ":" & JisekiInputVo.ShutuzuJisekiStsrDhstba
                    '出図_受領日
                    sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                            NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_JYURYO_DATE)).NoteStyle = NoteStyle.PopupNote

                    sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                            NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_JYURYO_DATE)).Note = _
                                            JisekiInputVo.CreatedDate & ":" & TehaichoEditLogic.ConvDateInt8(JisekiInputVo.ShutuzuJisekiJyuryoDate)
                    '出図_件名
                    sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                            NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_KENMEI)).NoteStyle = NoteStyle.PopupNote
                    sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                            NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_KENMEI)).Note = JisekiInputVo.CreatedDate & ":" & JisekiInputVo.ShutuzuKenmei
                    'コメント
                    sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                            NmSpdTagShutuzuKaiteiRireki.TAG_COMMENT)).NoteStyle = NoteStyle.PopupNote
                    sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                            NmSpdTagShutuzuKaiteiRireki.TAG_COMMENT)).Note = JisekiInputVo.CreatedDate & ":" & JisekiInputVo.Comment
                End If
                '
                oRow += 1

            Next

            '手入力（空行用）変数
            If StringUtil.IsEmpty(lastKaiteiNo) Then
                lastKaiteiNo = "00"
            End If
            Dim nextKaiteiNo As String = (CInt(lastKaiteiNo) + 1).ToString("00")
            Dim inputShutuzuJisekiStsrDhstba As String = String.Empty
            Dim inputShutuzuJisekiJyuryoDate As Integer = 0
            Dim inputShutuzuKenmei As String = String.Empty
            Dim inputComment As String = String.Empty

            '＋１行作成する
            '   出図実績手入力情報があれば表示、無ければ空白行を設定する。
            '手入力情報があるか？
            Dim InputVo As New TShisakuTehaiShutuzuJisekiInputVo
            InputVo = _TehaiEditLogic.TehaiShutuzuJisekiInput(_blockNo, _buhinNo, nextKaiteiNo, False)   'false:改訂№で検索する
            '   あれば変数にセットする。
            If StringUtil.IsNotEmpty(InputVo) Then
                inputShutuzuJisekiStsrDhstba = InputVo.ShutuzuJisekiStsrDhstba
                inputShutuzuJisekiJyuryoDate = InputVo.ShutuzuJisekiJyuryoDate
                inputShutuzuKenmei = InputVo.ShutuzuKenmei
                inputComment = InputVo.Comment
            End If

            '織込
            If StringUtil.Equals(nextKaiteiNo, _saisyuShutuzuKaiteiNo) Then
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuKaiteiRireki.TAG_ORIKOMI)).Value = True
            Else
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuKaiteiRireki.TAG_ORIKOMI)).Value = False
            End If
            '出図_改訂№
            sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                          NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_KAITEI_NO)).Value = nextKaiteiNo
            '   ロックする
            sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                        NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_KAITEI_NO)).Locked = True
            '出図_設通№
            sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                          NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)).Value = inputShutuzuJisekiStsrDhstba
            '   ロックしない
            sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                        NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)).Locked = False
            '出図_受領日
            sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                          NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_JYURYO_DATE)).Value = TehaichoEditLogic.ConvDateInt8(inputShutuzuJisekiJyuryoDate)
            '   ロックしない
            sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                        NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_JYURYO_DATE)).Locked = False
            '出図_件名
            sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                          NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_KENMEI)).Value = inputShutuzuKenmei
            '   ロックしない
            sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                        NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_KENMEI)).Locked = False
            'コメント
            sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                          NmSpdTagShutuzuKaiteiRireki.TAG_COMMENT)).Value = inputComment
            '   ロックしない
            sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                        NmSpdTagShutuzuKaiteiRireki.TAG_COMMENT)).Locked = False

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
        Private Sub Frm20ShutuzuKaiteiRireki_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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
                _ResultOkHanei = False
                Me.Close()
                Return
            End If

        End Sub

#End Region


#Region "ボタン"


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
        ''' ボタン（反映）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnHanei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHanei.Click

            Try

                '保存確認フォームを表示
                Dim result As Integer = frm00Kakunin.Confirm("確認", "最終織込設変情報へ反映しますか。", _
                                               "", "OK", "CANCEL")

                If result = MsgBoxResult.Ok Then  'OK

                    'SPREADよりチェックを付けたデータを抽出
                    GetSpreadSelectVos()

                    Cursor.Current = Cursors.WaitCursor
                    _ResultOkHanei = True

                    Me.Close()

                Else
                    Return
                End If
                Cursor.Current = Cursors.Default
            Catch ex As Exception
                ComFunc.ShowErrMsgBox(String.Format("反映中にエラーが発生しました{0}{1}", vbCrLf, ex.Message))
            Finally
                Cursor.Current = Cursors.Default
            End Try
        End Sub

        ''' <summary>
        ''' スプレッドから☑行の情報を取得する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub GetSpreadSelectVos()

            _ResultList = New List(Of TShisakuTehaiKihonVo)

            Dim sheet As FarPoint.Win.Spread.SheetView = Me.spdShutuzuKaiteiRireki_Sheet1
            Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(sheet)

            For rowIndex As Integer = startRow To sheet.RowCount - 1
                If sheet.Cells(rowIndex, TehaichoEditLogic.GetTagIdx(sheet, _
                                            NmSpdTagShutuzuKaiteiRireki.TAG_ORIKOMI)).Value = True Then

                    Dim addVo As New TShisakuTehaiKihonVo
                    'ブロック
                    addVo.ShisakuBlockNo = Me.lblBlockNo.Text
                    '行ＩＤ
                    addVo.GyouId = Me.lblGyoId.Text
                    '部品番号
                    addVo.BuhinNo = Me.lblBuhinNo.Text

                    '最新出図_改訂№
                    addVo.ShutuzuJisekiKaiteiNo = ""
                    '最新出図_設通№
                    addVo.ShutuzuJisekiStsrDhstba = ""
                    '最新出図_受領日
                    addVo.ShutuzuJisekiDate = 0

                    '最終織込出図_改訂№
                    addVo.SaisyuSetsuhenKaiteiNo = sheet.Cells(rowIndex, _
                                                               TehaichoEditLogic.GetTagIdx(sheet, _
                                                                                           NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_KAITEI_NO)).Value
                    '最終織込_設通№
                    addVo.StsrDhstba = sheet.Cells(rowIndex, _
                                                                 TehaichoEditLogic.GetTagIdx(sheet, _
                                                                                             NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)).Value
                    '最終織込_受領日
                    addVo.SaisyuSetsuhenDate = TehaichoEditLogic.ConvInt8Date(sheet.Cells(rowIndex, _
                                                                                          TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_JYURYO_DATE)).Value)

                    _ResultList.Add(addVo)
                End If
            Next
        End Sub

#End Region

#Region "スプレッドChange改訂履歴"

        ''' <summary>
        '''  スプレッドCellClick
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdShutuzuKaiteiRireki_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdShutuzuKaiteiRireki.CellClick
            Try

                Dim sheet As FarPoint.Win.Spread.SheetView = Me.spdShutuzuKaiteiRireki_Sheet1

                Dim actColTag As String = sheet.Columns(e.Column).Tag.ToString
                Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(sheet)

                'セルクリック処理
                Select Case actColTag
                    Case NmSpdTagShutuzuKaiteiRireki.TAG_ORIKOMI
                        For rowIndex As Integer = startRow To sheet.RowCount - 1
                            '自分以外に□
                            sheet.SetValue(rowIndex, TehaichoEditLogic.GetTagIdx(sheet, _
                                              NmSpdTagShutuzuKaiteiRireki.TAG_ORIKOMI), False)
                        Next
                        ''自分に☑
                        'sheet.SetValue(e.Row, TehaichoEditLogic.GetTagIdx(sheet, _
                        '                                              NmSpdTagShutuzuKaiteiRireki.TAG_ORIKOMI), True)
                End Select

            Catch ex As Exception
                MsgBox("セルクリック処理で問題が発生しました")
                Me.Close()
            End Try
        End Sub

        ''' <summary>
        '''  スプレッドChange改訂履歴
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdShutuzuKaiteiRireki_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdShutuzuKaiteiRireki.Change

            Try

                Dim sheet As FarPoint.Win.Spread.SheetView = Me.spdShutuzuKaiteiRireki_Sheet1

                Dim actColTag As String = sheet.Columns(e.Column).Tag.ToString

                Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(sheet)

                'セル変更対応処理
                Select Case actColTag
                    Case NmSpdTagShutuzuKaiteiRireki.TAG_ORIKOMI
                        '自分に☑
                        sheet.SetValue(e.Row, TehaichoEditLogic.GetTagIdx(sheet, _
                                                                      NmSpdTagShutuzuKaiteiRireki.TAG_ORIKOMI), True)

                        '    For rowIndex As Integer = startRow To sheet.RowCount - 1
                        '        '自分に☑、以外に□
                        '        If actRow <> rowIndex Then
                        '            sheet.SetValue(actRow, sheet.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_ORIKOMI).Index, False)
                        '        Else
                        '            sheet.SetValue(actRow, sheet.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_ORIKOMI).Index, True)
                        '        End If
                        '    Next
                    Case NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_STSR_DHSTBA
                        '編集されたセルは太文字・青文字にする
                        spdShutuzuKaiteiRireki_Sheet1.Cells(e.Row, e.Column, e.Row, e.Column).ForeColor = Color.Blue
                        spdShutuzuKaiteiRireki_Sheet1.Cells(e.Row, e.Column, e.Row, e.Column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                    Case NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_KENMEI
                        '編集されたセルは太文字・青文字にする
                        spdShutuzuKaiteiRireki_Sheet1.Cells(e.Row, e.Column, e.Row, e.Column).ForeColor = Color.Blue
                        spdShutuzuKaiteiRireki_Sheet1.Cells(e.Row, e.Column, e.Row, e.Column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                    Case NmSpdTagShutuzuKaiteiRireki.TAG_COMMENT
                        '編集されたセルは太文字・青文字にする
                        spdShutuzuKaiteiRireki_Sheet1.Cells(e.Row, e.Column, e.Row, e.Column).ForeColor = Color.Blue
                        spdShutuzuKaiteiRireki_Sheet1.Cells(e.Row, e.Column, e.Row, e.Column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                    Case NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_JYURYO_DATE
                        '受領日
                        Dim valJyuryo As String = spdShutuzuKaiteiRireki_Sheet1.GetValue(e.Row, _
                                                            spdShutuzuKaiteiRireki_Sheet1.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_JYURYO_DATE).Index)
                        'Nullや空文字の場合
                        If valJyuryo Is Nothing OrElse valJyuryo.ToString.Trim.Equals(String.Empty) Then
                            spdShutuzuKaiteiRireki_Sheet1.SetValue(e.Row, spdShutuzuKaiteiRireki_Sheet1.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_JYURYO_DATE).Index, String.Empty)
                        Else
                            Dim dateJyuryo As Date = spdShutuzuKaiteiRireki_Sheet1.GetValue(e.Row, spdShutuzuKaiteiRireki_Sheet1.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_JYURYO_DATE).Index)

                            'カレンダーから入力された値から時刻を取り除き格納する
                            If IsDate(dateJyuryo) = True Then
                                spdShutuzuKaiteiRireki_Sheet1.SetValue(e.Row, spdShutuzuKaiteiRireki_Sheet1.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_JYURYO_DATE).Index, Format(dateJyuryo, "yyyy/MM/dd"))
                            End If
                            '編集されたセルは太文字・青文字にする
                            spdShutuzuKaiteiRireki_Sheet1.Cells(e.Row, e.Column, e.Row, e.Column).ForeColor = Color.Blue
                            spdShutuzuKaiteiRireki_Sheet1.Cells(e.Row, e.Column, e.Row, e.Column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                        End If
                End Select

            Catch ex As Exception
                MsgBox("値変更処理で問題が発生しました")
                Me.Close()
            End Try

        End Sub
#End Region

#Region "スプレッド改訂履歴マウスクリックイベント情報取得"


        ''' <summary>
        '''  スプレッドマウスクリックイベント情報取得
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdShutuzuKaiteiRireki_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles spdShutuzuKaiteiRireki.MouseDown
            Dim sv As New FarPoint.Win.Spread.SpreadView(Me.spdShutuzuKaiteiRireki)
            sv = Me.spdShutuzuKaiteiRireki.GetRootWorkbook()

            'セル行列位置取得
            _clickCellPosition = sv.GetCellFromPixel(e.X, e.Y)

            If _clickCellPosition.Row = -1 Then
                _clickCellPosition = sv.GetRowHeaderCellFromPixel(e.X, e.Y)

            Else
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

