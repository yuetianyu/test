Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports ShisakuCommon
Imports EventSakusei.ShisakuBuhinEditList
Imports EventSakusei.ShisakuBuhinSakuseiList
Imports ShisakuCommon.Ui
Imports ExcelOutput.ShisakuBuhinExcel
Imports ExcelOutput.TehaichoExcel
Imports SearchCost01
Imports EventSakusei

'''↓↓2014/12/23 4試作１課メニュー (TES)張 ADD BEGIN
Public Class frm4NORMALmenu04
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)

    End Sub

    Private Sub txtListCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub frm4NORMALmenu04_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
            ShisakuFormUtil.SetIdAndBuka(LblCurrUserId, LblCurrBukaName)

        Catch ex As Exception
            Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
            Throw
        End Try
    End Sub

    Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
        Application.Exit()
    End Sub

    Private Sub lblBuHin01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblBuHin01.Click
        Dim f As New Frm35DispShisakuBuhinEditList
        Me.Hide()
        f.strMode = ShishakuHensyuMode
        f.ShowDialog()
        Me.Show()
    End Sub

    Private Sub lblBuHin02_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblBuHin02.Click
        Dim f As New Frm35DispShisakuBuhinEditList
        Me.Hide()
        f.strMode = ShishakuKaiteiHensyuMode
        f.ShowDialog()
        Me.Show()
    End Sub

    Private Sub lblBuHin03_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblBuHin03.Click
        Dim f As New Frm44ShisakuBuhinExcel
        Me.Hide()
        f.ShowDialog()
        Me.Show()
    End Sub

    Private Sub lblTeCyou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New Frm46DispShisakuBuhinList
        Me.Hide()
        f.ShowDialog()
        Me.Show()
    End Sub

    'Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    '    ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
    'End Sub

    Private Sub frm3NORMALmenu02_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub

    Private Sub btnBuHin05_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuHin05.Click
        Dim f As New Frm35DispShisakuBuhinEditList
        Me.Hide()
        f.strMode = ShishakuKanryoViewMode
        f.ShowDialog()
        Me.Show()

    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
    End Sub

    Private Sub btnShisakuEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShisakuEvent.Click
        ' Accessの試作イベント管理を実行する。
        System.Diagnostics.Process.Start("\\fgnt30\pt\試作イベント管理\試作イベント情報\管理画面\試作イベント管理機能.mdb")
    End Sub

    Private Sub btnSearch01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch01.Click
        Dim strat As New clsStartSearch
        Me.Hide()

        '過去購入品検索画面
        strat.DisplayPastBuySearchDialog()

        Me.Show()

    End Sub

    Private Sub lblBuHinSakusei_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim f As New Frm7DispShisakuBuhinSakuseiList(SHISAKU1KA_SISAKU)
        Me.Hide()
        f.ShowDialog()
        Me.Show()
    End Sub

    Private Sub btnGousyabetsuShiyousyo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGousyabetsuShiyousyo.Click
        Using frm As New frmGousyabetsuShiyousyo()
            frm.ShowDialog(Me)
        End Using
    End Sub

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