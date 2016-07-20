Imports EBom.Common
Imports FarPoint.Win
Imports ShisakuCommon
Imports System.Reflection
Imports EventSakusei.ShisakuBuhinEditSekkei
Imports ShisakuCommon.Ui

Namespace ShisakuBuhinKaiteiEditList

    'Private m_shisakuBuhinKaiteiEditList As DispShisakuBuhinKaiteiEditList = Nothing
    ''' <summary>
    ''' 部品構成表示画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm36DispShisakuBuhinKaiteiEditList

        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ShisakuFormUtil.Initialize(Me)

        End Sub

        Private Sub spdParts_LeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles spdParts.LeaveCell
            If e.NewRow = -1 OrElse e.NewColumn = -1 Then
                Return
            End If

            ' 選択範囲の情報を表示します。
            txtIbentoNo.Text = spdParts.ActiveSheet.GetText(e.NewRow, 0)

            'リストコード入力時は削除と手配帳作成ボタンを使用可能へ
            If txtIbentoNo.Text <> "" Then
                '
                btnCall.ForeColor = Color.Black
                btnCall.BackColor = Color.LightCyan
                btnCall.Enabled = True
            Else
                '
                btnCall.ForeColor = Color.Black
                btnCall.BackColor = Color.White
                btnCall.Enabled = False
            End If
        End Sub

        Private Sub Frm36DispShisakuBuhinKaiteiEditList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Try
                'MasterLogin画面初期化
                'm_shisakuBuhinKaiteiEditList = New DispShisakuBuhinKaiteiEditList(Me)
                'm_shisakuBuhinKaiteiEditList.InitView()
                '初期起動時には以下のボタンを使用不可とする。
                '
                btnCall.ForeColor = Color.Black
                btnCall.BackColor = Color.White
                btnCall.Enabled = False
            Catch ex As Exception
                Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
                Throw
            End Try

        End Sub

        Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCall.Click

            '呼び出しのときの確認画面は不要です。　6/17決定
            'frm01Kakunin.lblKakunin.Text = "イベントを呼出します。"
            'frm35Para = "btnCall"
            'frm01Kakunin.ShowDialog()
            'Select Case frm35ParaModori
            '    Case "1"
            Frm37DispShisakuBuhinEditSekkei.ShowDialog()
            '    Case Else
            'End Select
        End Sub

        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Me.Close()
        End Sub

        Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
            Application.Exit()
        End Sub

        Private Sub ToolTip_Popup(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PopupEventArgs) Handles ToolTip.Popup
            'ToolTip.IsBalloon = True
        End Sub

        Private Sub spdParts_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellDoubleClick

            'ダブルクリックすると次画面へ遷移する。
            'ダブルクリック行のイベントコードを次画面へ引き渡す。


            Frm37DispShisakuBuhinEditSekkei.ShowDialog()
        End Sub

        Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        End Sub
    End Class
End Namespace
