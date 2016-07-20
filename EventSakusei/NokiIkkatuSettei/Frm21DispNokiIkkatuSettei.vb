Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports ShisakuCommon
Imports ShisakuCommon.Ui

Namespace NokiIkkatuSettei
    ''' <summary>
    ''' 納期一括設定画面
    ''' 
    ''' ※プロパティについては全て登録ボタンOK後に取得可能となる
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm21DispNokiIkkatuSettei

#Region "プライベート変数"

        Private _dispLogic As NokiIkkatsuSetteiLogic
        Private _clickCellPosition As FarPoint.Win.Spread.Model.CellRange
        Private _genchoFlag As Boolean
        Private _senyouKyouyouFlag As Boolean
        Private _dtMakerNouki As DataTable
        Private _dtKonyuNouki As DataTable

#End Region

#Region "コンストラクタ"

        Public Sub New()
            InitializeComponent()

            _dispLogic = New NokiIkkatsuSetteiLogic(Me)
            ShisakuFormUtil.Initialize(Me)

            '初期化
            initialize()
        End Sub

        ''' <summary>
        ''' 初期化
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub initialize()
            ''画面初期化
            'ボタン初期状態
            ckbBetuSettei.Checked = False
            ckbBetuSetteiGenchou.Checked = False

            'スプレッド表示状態
            spdMaker.Visible = True
            spdKonyuGencho.Visible = True
            spdKonyu.Visible = True
            spdKonyuGencho.Visible = True

            'タイトルバー情報セット
            ShisakuFormUtil.setTitleVersion(Me)

            'スプレッド類初期化
            _dispLogic.Initialize()
        End Sub

#End Region

#Region "プロパティ"
        ''' <summary>
        ''' 取引先納期取得
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetDtMakerNouki() As DataTable
            Get
                Return _dtMakerNouki
            End Get
        End Property
        ''' <summary>
        ''' 購入品納期取得
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetDtKonyuNouki() As DataTable
            Get
                Return _dtKonyuNouki
            End Get
        End Property
        ''' <summary>
        ''' 支給部品日付取得
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetShikyuBuhinDate(ByVal aGencho As Boolean) As String
            Get
                Return _dispLogic.GetShikyuBuhinDate(aGencho)
            End Get
        End Property
        ''' <summary>
        ''' 国内対象判定
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetIsKokunai() As Boolean
            Get
                If _genchoFlag = False Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property
        ''' <summary>
        ''' 現調品判定
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetIsGencho() As Boolean
            Get
                If _genchoFlag = True Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property
        ''' <summary>
        ''' 専用品、共用品別設定チェック有無
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetIsSenyouKyouyou() As Boolean
            Get
                Return _senyouKyouyouFlag
            End Get
        End Property

#End Region

#Region "イベント"
        ''' <summary>
        ''' 戻るボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
            Me.Close()
        End Sub
        ''' <summary>
        ''' 現調品登録ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnADDGencho_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnADDGencho.Click
            '現調品として登録実行
            registIkkatsu(True)
        End Sub
        ''' <summary>
        ''' 国内登録ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnADDKokunaihin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnADDKokunaihin.Click
            '国内として登録実行
            registIkkatsu(False)
        End Sub
        ''' <summary>
        '''国内 チェック専用品、共用品別設定
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ckbBetuSettei_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckbBetuSettei.CheckedChanged
            'チェック状況により画面を制御する。
            If ckbBetuSettei.Checked = True Then
                _dispLogic.SetSpSenyouKyouyou_On(False)
            Else
                _dispLogic.SetSpSenyouKyouyou_Off(False)
            End If
        End Sub
        ''' <summary>
        ''' 現調品 チェック専用品、共用品別設定
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ckbBetuSetteiGenchou_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckbBetuSetteiGenchou.CheckedChanged
            'チェック状況により画面を制御する。
            If ckbBetuSetteiGenchou.Checked = True Then
                _dispLogic.SetSpSenyouKyouyou_On(True)
            Else
                _dispLogic.SetSpSenyouKyouyou_Off(True)
            End If
        End Sub

        ''' <summary>
        '''  取引スプレッドマウスクリックイベント情報取得
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdMaker_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles spdMaker.MouseDown, spdMakerGencho.MouseDown
            Dim sv As New FarPoint.Win.Spread.SpreadView(Me.spdMaker)
            sv = Me.spdMaker.GetRootWorkbook()
            _clickCellPosition = sv.GetCellFromPixel(e.X, e.Y)

            'セル行列位置取得
            _clickCellPosition = sv.GetCellFromPixel(e.X, e.Y)

            If _clickCellPosition.Row = -1 Then
                _clickCellPosition = sv.GetRowHeaderCellFromPixel(e.X, e.Y)

            Else
                _clickCellPosition = Nothing
            End If

        End Sub
        ''' <summary>
        ''' 行挿入クリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub 行挿入IToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 行挿入IToolStripMenuItem.Click

            'マウスクリック位置情報有
            If Not _clickCellPosition Is Nothing AndAlso _clickCellPosition.Row <> -1 Then

                'タブコントロールの位置により
                If _dispLogic.IsGencho = False Then
                    'スプレッド行挿入処理へ（国内)
                    _dispLogic.InsertRow(_clickCellPosition.Row, False)
                Else
                    'スプレッド行挿入処理へ(現調）
                    _dispLogic.InsertRow(_clickCellPosition.Row, True)
                End If

                _clickCellPosition = Nothing

            Else
                MsgBox("ContextMenuが行見出し位置のみで表示されるように調整中")
            End If


        End Sub
        ''' <summary>
        ''' 行削除クリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub 行削除DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 行削除DToolStripMenuItem.Click
            'マウスクリック位置情報有
            If Not _clickCellPosition Is Nothing AndAlso _clickCellPosition.Row <> -1 Then

                'cellPositionは1からの値が割り振られるので-1して行操作は行う
                Dim editRow As Integer = _clickCellPosition.Row

                'タブコントロールの位置により
                If _dispLogic.IsGencho = False Then
                    'スプレッド行削除処理へ（国内)
                    _dispLogic.DeleteRow(editRow, False)
                Else
                    'スプレッド行削除処理へ(現調）
                    _dispLogic.DeleteRow(editRow, True)
                End If

                _clickCellPosition = Nothing

            Else
                MsgBox("ContextMenuが行見出し位置のみで表示されるように調整中")
            End If

        End Sub
        ''' <summary>
        ''' チェックチェンジ支給部品納期(国内)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub chkShikyuBuhin_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShikyuBuhin.CheckedChanged

            If chkShikyuBuhin.Checked = True Then
                dtpShikyuBuhinDay.Enabled = True
            Else
                dtpShikyuBuhinDay.Enabled = False
            End If

        End Sub
        ''' <summary>
        ''' チェックチェンジ支給部品納期(現調品)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub chkShikyuBuhinGencho_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShikyuBuhinGencho.CheckedChanged

            If chkShikyuBuhinGencho.Checked = True Then
                dtpShikyuBuhinDayGencho.Enabled = True
            Else
                dtpShikyuBuhinDayGencho.Enabled = False
            End If

        End Sub
#End Region

#Region "メソッド"
        ''' <summary>
        '''  登録処理
        ''' </summary>
        ''' <param name="aGencho"></param>
        ''' <remarks></remarks>
        Private Sub registIkkatsu(ByVal aGencho As Boolean)
            Dim result As Integer = _
            ComFunc.ShowInfoMsgBox("納期一括登録を行ってもよろしいですか？", MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2)

            'ｷｬﾝｾﾙ
            If result = vbCancel Then
                Return
            End If

            'エラー有無
            If _dispLogic.GetIsError(aGencho) = False Then
                ComFunc.ShowInfoMsgBox("入力した内容にエラーが有ります。")
                Return
            End If

            '専用品、共用品別設定
            If aGencho = False Then
                _senyouKyouyouFlag = Me.ckbBetuSettei.Checked
            Else
                _senyouKyouyouFlag = Me.ckbBetuSetteiGenchou.Checked
            End If

            '納期情報取得
            _dtMakerNouki = _dispLogic.GetMakerNouki(aGencho)
            _dtKonyuNouki = _dispLogic.GetKounyuNouki(_senyouKyouyouFlag, aGencho)

            '現調フラグ(true=現調,false=国内)
            _genchoFlag = aGencho

            Me.DialogResult = Windows.Forms.DialogResult.OK

            Me.Close()
        End Sub
#End Region

#Region "キー押下処理"
        ''' <summary>
        ''' キー押下処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>

        Private Sub spdMaker_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdMaker.KeyDown
            Dim downKey As Object
            Dim sheet As Spread.SheetView = spdMaker.ActiveSheet

            downKey = e.KeyCode()

            Select Case downKey

                Case Keys.Delete

                    ClearRange(sheet)

            End Select
        End Sub

        Private Sub spdKonyu_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdKonyu.KeyDown
            Dim downKey As Object
            Dim sheet As Spread.SheetView = spdKonyu.ActiveSheet

            downKey = e.KeyCode()

            Select Case downKey
                Case Keys.Delete
                    ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ列インデックス
                    'ブロックは削除対象外
                    If ParaActColIdx = 0 Then
                        Exit Sub
                    End If
                    ClearRange(sheet)
            End Select
        End Sub

        Private Sub sspdMakerGencho_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdMakerGencho.KeyDown
            Dim downKey As Object
            Dim sheet As Spread.SheetView = spdMakerGencho.ActiveSheet

            downKey = e.KeyCode()

            Select Case downKey

                Case Keys.Delete

                    ClearRange(sheet)

            End Select
        End Sub


        Private Sub spdKonyuGencho_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdKonyuGencho.KeyDown
            Dim downKey As Object
            Dim sheet As Spread.SheetView = spdKonyuGencho.ActiveSheet

            downKey = e.KeyCode()

            Select Case downKey
                Case Keys.Delete
                    ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ列インデックス
                    'ブロックは削除対象外
                    If ParaActColIdx = 0 Then
                        Exit Sub
                    End If
                    ClearRange(sheet)
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

            datamodel.ClearData(fromRow, fromCol, rowCount, colCount)

        End Sub


#End Region

    End Class
End Namespace
