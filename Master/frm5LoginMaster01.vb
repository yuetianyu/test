Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports Master
Imports EBom.Data
Imports ShisakuCommon
Imports System.Text
Imports Microsoft.Office.Interop
Imports System.IO
Imports ShisakuCommon.Ui


''' <summary>
''' 部品構成表示画面
''' </summary>
''' <remarks></remarks>


Public Class frm5LoginMaster01


    Private m_loginmaster As LoginMaster01 = Nothing
    Private isLoad As Boolean = True

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)

    End Sub

    Private Sub frm5LoginMaster01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            'MasterLogin画面初期化
            m_loginmaster = New LoginMaster01(Me)
            m_loginmaster.InitView()
            isLoad = False
        Catch ex As Exception
            Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
            Throw
        End Try

    End Sub

   
    '終了ボタン
    Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
        Application.Exit()
    End Sub
    '戻りボタン
    Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
        Me.Close()
    End Sub


    'あいまい検索
    Private Sub cmbUserId_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUserId.TextChanged, cmbUserName.TextChanged, cmbSite.TextChanged, cmbDepart.TextChanged
        If isLoad = False Then
            m_loginmaster.SetDataList()
        End If
    End Sub
    'Click事件
    Private Sub spdParts_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellDoubleClick
        m_loginmaster.InitUpdateView()
    End Sub


    'Excelボタン
    Private Sub btnExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelExport.Click

        ExcelCommon.SaveExcelFile("Loginマスタ", spdParts, "Login")

    End Sub

    ' Spread XML ファイルへシートのデータを保存します。
    Sub SaveSpreadFile(ByVal filename As String)

        Dim ret As Boolean

        ret = spdParts.Save(filename, False)

    End Sub


    '時間動く機能
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
    End Sub

    'Del Key の削除
    Private Sub Controls_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbUserId.KeyDown, cmbUserName.KeyDown, cmbSite.KeyDown, cmbDepart.KeyDown
        ShisakuFormUtil.DelKeyDown(sender, e)
    End Sub

End Class