Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports ShisakuCommon
Imports ShisakuCommon.Ui

''' <summary>
''' 部品構成表示画面
''' </summary>
''' <remarks></remarks>

Public Class frm51KengenMaster01
#Region " メンバー変数 "
    ''' <summary>ロジック</summary>
    Private m_kengenMas As KengenMasert01 = Nothing
    Private isLoad As Boolean = True
#End Region

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)

    End Sub

    ''' <summary>
    ''' 画面初期化
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm51KengenMaster01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.KeyPreview = True
            m_kengenMas = New KengenMasert01(Me)
            m_kengenMas.InitView()
            isLoad = False
        Catch ex As Exception
            Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
            Throw
        End Try
    End Sub
    ''' <summary>
    ''' 検索条件が入力後　または　選択後
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUserId.TextChanged, cmbUserName.TextChanged, cmbSite.TextChanged, cmbCompetent.TextChanged, cmbBuka.TextChanged
        If isLoad = False Then
            m_kengenMas.SetDataList()
        End If
    End Sub
    ''' <summary>
    ''' 戻るボタンを押す
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' アプリケーション終了を押す
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
        Application.Exit()
    End Sub
    ''' <summary>
    ''' SPREADで行をダブルクリックする
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub spdInfo_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdInfo.CellDoubleClick
        m_kengenMas.GotoUpdate(e)
    End Sub
    ''' <summary>
    ''' excel出力ボタンを押す
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelExport.Click
        ExcelCommon.SaveExcelFile("権限マスター", spdInfo, "Kengen")

    End Sub
    ''' <summary>
    ''' time tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
    End Sub
    ''' <summary>
    ''' コンボボックスに　delete　key　press
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Controls_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbUserId.KeyDown, cmbUserName.KeyDown, cmbSite.KeyDown, cmbCompetent.KeyDown, cmbBuka.KeyDown

        ShisakuFormUtil.DelKeyDown(sender, e)
    End Sub
End Class