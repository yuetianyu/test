Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports YosansyoTool.YosanEventList
Imports YosansyoTool.YosanBuhinEdit

Public Class frm2NORMALmenu03
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)

    End Sub

    Private Sub frm2NORMALmenu02_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
            ShisakuFormUtil.SetIdAndBuka(LblCurrUserId, LblCurrBukaName)


        Catch ex As Exception
            Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
            Throw
        End Try

    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
    End Sub

    ''' <summary>
    ''' イベント一覧画面呼出
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEventList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEventList.Click
        Me.Hide()

        Using frm As New FrmYosanEventList()
            frm.ShowDialog()
        End Using

        Me.Show()
    End Sub

    ''' <summary>
    ''' ベント履歴参照画面呼出
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEventRireki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Hide()


        Me.Show()
    End Sub

    ''' <summary>
    ''' 部品表メンテ画面呼出
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBuhinEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuhinEdit.Click
        Me.Hide()

        'TODO
        'Dim eventCode As String = "A75--01"
        'Dim unitKbn As String = "T"
        Dim eventCode As String = "10B-N-01"
        Dim unitKbn As String = "M"
        Using frm As New Frm41DispYosanBuhinEdit00(Nothing, eventCode, unitKbn, LoginInfo.Now, 0, 0, "")
            frm.ShowDialog()
        End Using

        Me.Show()

    End Sub

    Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
        Application.Exit()
        System.Environment.Exit(0)
    End Sub


    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        Me.Close()
    End Sub

End Class