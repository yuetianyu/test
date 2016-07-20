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

#Region "出図取込実績差照会クラス"
    ''' <summary>
    ''' 出図取込実績差照会クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm20ShutuzuTorikomi

#Region "プライベート変数"
        '''<summary>初期化完了フラグ</summary>>
        Private _InitComplete As Boolean = False
        ''' <summary>フォーム表示時に呼び出されるID</summary>
        Private m_initSwitchId As String = String.Empty

        ''' <summary>画面制御ロジック</summary>
        Private _TehaiEditLogic As TehaichoEditLogic
        ''' <summary>マウスクリック行列位置保存</summary>
        Private _clickCellPosition As Spread.Model.CellRange

        ''' <summary>出図取込情報シート</summary>
        Private ShutuzuTorikomiSheet As SheetView

        Private _ShutuzuJisekiVos As List(Of TShisakuTehaiShutuzuJisekiVo)
        '
        Private _ResultList As List(Of TShisakuTehaiKihonVo)

        Private _ResultOk As Boolean

#End Region

#Region "コンストラクタ"

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="ResultList">出図実績情報</param>
        ''' <param name="tehaiEditLogic">画面制御ロジック</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal ResultList As List(Of TShisakuTehaiShutuzuJisekiVo), ByVal tehaiEditLogic As TehaichoEditLogic)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            Me.Refresh()
            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)

            Me._ShutuzuJisekiVos = ResultList

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

                Dim sheet As FarPoint.Win.Spread.SheetView = spdShutuzuTorikomi_Sheet1
                NmSpdTagShutuzuJiseki.initTagName(sheet)

                ''画面のPG-IDが表示されます。
                ShisakuFormUtil.setTitleVersion(Me)
                LblCurrPGId.Text = "PG-ID :" + PROGRAM_ID_20

                ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
                ShisakuFormUtil.SetIdAndBuka(LblCurrUserId, LblCurrBukaName)

                'スプレッダーを初期化する
                InitSpreadColDefault()

                '初期データ表示スプレッド(出図取込)
                If SetSpreadShutuzuTorikomi() = False Then
                    '_ResultOk = False
                    ''初期化失敗とする。
                    'Exit Sub
                End If

                'スプレッドの読取専用列設定
                _TehaiEditLogic.LockColShutuzuTorikomiSpread(Me)

                'Enterキーの動作を、「編集」から「次行へ移動」にする
                ShisakuCommon.Ui.Spd.SpreadUtil.BindKeyEnterIsNextRow(spdShutuzuTorikomi)

                'Shift + Enterキーの動作を、「前行へ移動」にする
                ShisakuCommon.Ui.Spd.SpreadUtil.BindKeyShiftEnterIsPreviousRow(spdShutuzuTorikomi)

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

            Dim sheet As Spread.SheetView = spdShutuzuTorikomi_Sheet1
            Dim btnType As New ButtonCellType
            btnType.Text = "表示"
            btnType.TextDown = "表示"
            btnType.ButtonColor = Color.LightGray
            btnType.ButtonColor2 = Color.LightGray

            '表示
            sheet.Columns(NmSpdTagShutuzuJiseki.TAG_HYOJI).CellType = btnType

        End Sub
#End Region


        ''' <summary>
        ''' 出図取込情報スプレッドデータ格納
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SetSpreadShutuzuTorikomi() As Boolean

            '情報が無ければ終了
            If _ShutuzuJisekiVos.Count = 0 Then
                ComFunc.ShowInfoMsgBox("条件に合うデータがありません。")
                'Return True
            End If

            Dim sheet As FarPoint.Win.Spread.SheetView = Me.spdShutuzuTorikomi_Sheet1
            Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(sheet)
            Dim oRow As Integer = startRow

            'スプレッドを他の画面と同様に設定
            Dim spread As FarPoint.Win.Spread.FpSpread = Me.spdShutuzuTorikomi
            SpreadUtil.Initialize(spread)

            '取得したデータ件数によりスプレッドの総行数を拡張(）
            sheet.RowCount = startRow + _ShutuzuJisekiVos.Count

            For Each vo As TShisakuTehaiShutuzuJisekiVo In _ShutuzuJisekiVos
                '表示
                'ブロック№
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuJiseki.TAG_SHISAKU_BLOCK_NO)).Value = vo.ShisakuBlockNo
                '部品番号
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuJiseki.TAG_BUHIN_NO)).Value = vo.BuhinNo
                '代表品番
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuJiseki.TAG_KATA_DAIHYOU_BUHIN_NO)).Value = vo.KataDaihyouBuhinNo
                '最新出図_改訂№
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuJiseki.TAG_NEW_SHUTUZU_JISEKI_KAITEI_NO)).Value = vo.ShutuzuJisekiKaiteiNo
                '最新出図_設通№
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuJiseki.TAG_NEW_SHUTUZU_JISEKI_STSR_DHSTBA)).Value = vo.ShutuzuJisekiStsrDhstba
                '最新出図_受領日
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuJiseki.TAG_NEW_SHUTUZU_JISEKI_JYURYO_DATE)).Value = TehaichoEditLogic.ConvDateInt8(vo.ShutuzuJisekiJyuryoDate)
                '最新出図_件名
                sheet.Cells(oRow, TehaichoEditLogic.GetTagIdx(sheet, _
                                                              NmSpdTagShutuzuJiseki.TAG_NEW_SHUTUZU_KENMEI)).Value = vo.ShutuzuKenmei
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
        Private Sub Frm20ShutuzuTorikomi_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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


#Region "ボタン(反映)"

        ''' <summary>
        ''' ボタン（反映）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnHozon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHozon.Click

            Try

                '反映確認フォームを表示
                Dim result As Integer = frm00Kakunin.Confirm("確認", "反映を実行しますか。", _
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
                ComFunc.ShowErrMsgBox(String.Format("反映中にエラーが発生しました{0}{1}", vbCrLf, ex.Message))
            Finally
                Cursor.Current = Cursors.Default
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
        Private Sub spdShutuzuTorikomi_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles spdShutuzuTorikomi.MouseDown
            Dim sv As New FarPoint.Win.Spread.SpreadView(Me.spdShutuzuTorikomi)
            sv = Me.spdShutuzuTorikomi.GetRootWorkbook()

            Dim sheet As SheetView = Me.spdShutuzuTorikomi_Sheet1

            'セル行列位置取得
            _clickCellPosition = sv.GetCellFromPixel(e.X, e.Y)

            If _clickCellPosition.Row = -1 Then
                _clickCellPosition = sv.GetRowHeaderCellFromPixel(e.X, e.Y)
            Else
                'ボタンは０列目、部品番号は４列目'
                If _clickCellPosition.Column = 0 AndAlso _clickCellPosition.Row > 1 Then
                    _TehaiEditLogic.SettsuLogon(sheet.Cells(_clickCellPosition.Row, _
                                    TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagShutuzuJiseki.TAG_BUHIN_NO)).Value, LoginInfo.Now.UserId)
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

