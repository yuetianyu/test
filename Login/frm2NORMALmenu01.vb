Imports EBom.Common
Imports Master
Imports ShisakuCommon
Imports EventSakusei.ShisakuBuhinEditList
Imports EventSakusei.ShisakuBuhinSakuseiList
Imports ExcelOutput.ShisakuBuhinExcel
Imports ExcelOutput.TehaichoExcel
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinKaiteiBlock
Imports EventSakusei

''' <summary>
''' 部品構成表示画面
''' </summary>
''' <remarks></remarks>

Public Class frm2NORMALmenu01

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)

    End Sub

    Private Sub frm2NORMALmenu01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
            ShisakuFormUtil.SetIdAndBuka(LblCurrUserId, LblCurrBukaName)

            Dim login = New LoginLogic()
            If (login.checkKurumaKengen(LoginInfo.Now.UserId) = RESULT.OK) Then
                btnKuruma.Visible = True
            Else
                btnKuruma.Visible = False
            End If

        Catch ex As Exception
            Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
            Throw
        End Try
        Me.btnBuHin01.Focus()
    End Sub

    Private Sub btnShisakuBuhinhyoSakusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuHin01.Click
        Dim f As New Frm7DispShisakuBuhinSakuseiList
        Me.Hide()
        f.ShowDialog()
        Me.Show()
    End Sub

    Private Sub btnKaihatufugou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKuruma.Click
        Dim f As New frm6KaihatufugouMaster01
        Me.Hide()
        f.ShowDialog()
        Me.Show()
    End Sub

    Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
        Application.Exit()
    End Sub

    Private Sub btnShisakubuhinhyouHensyu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuHin02.Click
        Dim f As New Frm35DispShisakuBuhinEditList
        Me.Hide()
        f.strMode = ShishakuHensyuMode
        f.ShowDialog()
        Me.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuHin03.Click
        Dim f As New Frm35DispShisakuBuhinEditList
        Me.Hide()
        f.strMode = ShishakuKaiteiHensyuMode
        f.ShowDialog()
        Me.Show()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuHin04.Click
        Dim f As New Frm44ShisakuBuhinExcel
        Me.Hide()
        f.ShowDialog()
        Me.Show()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTeCyou.Click
        Dim f As New Frm46DispShisakuBuhinList
        Me.Hide()
        f.ShowDialog()
        Me.Show()
    End Sub

    'Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    '    ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
    'End Sub

    Private Sub frm2NORMALmenu01_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub

    Private Sub btnBuHin05_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuHin05.Click
        Dim f As New Frm35DispShisakuBuhinEditList
        Me.Hide()
        f.strMode = ShishakuKanryoViewMode
        f.ShowDialog()
        Me.Show()
    End Sub

    Private Sub btnBuHin06_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuHin06.Click

        Me.Hide()

        Using frmShisakuBuhinKaiteiBlock As New Frm60DispShisakuBuhinKaiteiBlock
            frmShisakuBuhinKaiteiBlock.Execute()
            frmShisakuBuhinKaiteiBlock.ShowDialog()
        End Using

        Me.Show()

    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
    End Sub

    Private Sub btnView3D_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView3D.Click
        Using frm As New EventSakusei.frmConditionSelect()
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub btnFindCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindCost.Click
        Me.Hide()
        Using frm As New frm2NORMALmenu02
            frm.ShowDialog(Me)
        End Using
        If Not Me.IsDisposed Then
            Me.Show()
        End If
    End Sub

    ''↓↓2014/08/04 Ⅰ.8.号車別仕様書 作成機能_a) (TES)張 ADD BEGIN
    Private Sub btnGousyabetsuShiyousyo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGousyabetsuShiyousyo.Click
        Using frm As New frmGousyabetsuShiyousyo()
            frm.ShowDialog(Me)
        End Using
    End Sub
    ''↑↑2014/08/04 Ⅰ.8.号車別仕様書 作成機能_a) (TES)張 ADD END

    Private Sub btnOrderSheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrderSheet.Click

        'ここで実行ファイルの有無確認をして、インストールされていない場合はインフォメーションメッセージを展開する
        '「現調部品表システムが正しく設定されていません」「実行を終了します」
        'として終わりにする。


        Dim OrderSheetExePath As String = System.IO.Path.GetDirectoryName(Application.ExecutablePath) & "\OrderSheetLogin.exe"

        If Not System.IO.File.Exists(OrderSheetExePath) Then
            MsgBox("現調部品表システムが正しく設定されていません" & vbCrLf & _
                   "お手数ですが管理者までお問い合わせください。", MsgBoxStyle.Information, "確認")
            Exit Sub
        End If

        Me.Hide()

        Dim p As System.Diagnostics.Process = _
        System.Diagnostics.Process.Start(OrderSheetExePath, LoginInfo.Now.UserId)

        p.WaitForExit()

        Me.Show()

    End Sub
End Class