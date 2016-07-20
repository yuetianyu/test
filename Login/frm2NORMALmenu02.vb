Imports ShisakuCommon.Ui
Imports SearchCost01
Imports YosansyoTool.YosanEventList
Imports YosanKoujiLink.NO_30

Public Class frm2NORMALmenu02
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
    ''' 予算・工事指令№紐付け画面呼出
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnYosanLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYosanLink.Click
        Me.Hide()

        Using frmShisakuBuhinKaiteiBlock As New Frm30DispYosanKoujiLink
            frmShisakuBuhinKaiteiBlock.Execute()
            frmShisakuBuhinKaiteiBlock.ShowDialog()
        End Using

        Me.Show()
    End Sub

    ''' <summary>
    ''' 試作コスト検索画面呼出
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearchCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCost.Click
        Dim start As New SisakuCostSearch.clsStart

        Me.Hide()

        '試作Cost検索画面
        start.DisplayCostSearch()

        Me.Show()
    End Sub

    ''' <summary>
    ''' 過去購入品検索画面呼出
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch01.Click
        Dim strat As New clsStartSearch
        Me.Hide()

        '過去購入品検索画面
        strat.DisplayPastBuySearchDialog()

        Me.Show()

    End Sub

    '''↓↓↓2015/01/28 予算書ツールを追加 TES)劉 ADD BEGIN
    ''' <summary>
    ''' 予算書ツール画面呼出
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnYosannShoTool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYosannShoTool.Click
        Me.Hide()
        'Using frmYosannSho As New frm2NORMALmenu03
        '    frmYosannSho.ShowDialog()
        'End Using

        Using frm As New FrmYosanEventList()
            frm.ShowDialog()
        End Using
        Me.Show()
    End Sub
    '''↑↑↑2015/01/28 予算書ツールを追加 TES)劉 ADD END

    Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
        Application.Exit()
    End Sub


    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        Me.Close()
    End Sub

End Class