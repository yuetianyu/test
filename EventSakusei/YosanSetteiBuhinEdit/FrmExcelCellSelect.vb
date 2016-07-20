Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports EBom.Excel
Imports System.IO
Imports FarPoint.Win.Spread
Imports ShisakuCommon.Ui.Spd

''' <summary>
''' 手配帳編集画面・過去発注情報引当
'''  
''' ※取り込み先頭セル選択画面を作成する。
''' 施雪晨
''' </summary>
''' <remarks></remarks>
Public Class FrmExcelCellSelect
    Public activeSheet As SheetView
    Private activeCellValue As String
#Region "コンストラクタ"
    Public Sub New()

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="exl">選択されたExcel</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal exl As Stream)
        InitializeComponent()
        ' スプレッドにSelectFileを表示する
        Try
            EBomSpread1.OpenExcel(exl)

        Catch ex As Exception
            '    ' エラーが発生した場合にメッセージを表示します。
            Label1.ForeColor = Color.Red
            Label1.Text = ex.Message.ToString
        End Try

    End Sub

#End Region


#Region "イベント"

#Region "ボタン(戻る)"

    ''' <summary>
    ''' 戻るボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        EBomSpread1.Dispose()
        Me.Close()
    End Sub

#End Region

#Region "ボタン(決定）"
    ''' <summary>
    ''' 決定ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCellSelect.Click


        activeSheet = EBomSpread1.ActiveSheet
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

#End Region

#End Region

    Private Sub FrmExcelCellSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        EBomSpread1.EditMode = False
        EBomSpread1.FocusThicness = 2
        EBomSpread1.FocusRendererColor = Color.Blue

        For Each sht As SheetView In EBomSpread1.Sheets
            With sht
                .Columns(0, .ColumnCount - 1).Visible = True

                ''Lockedが何故か効かないので後述のキー操作イベントにて対処する
                '.Columns(0, .ColumnCount - 1).Locked = True
            End With
        Next
    End Sub


    Private Sub EBomSpread1_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles EBomSpread1.CellDoubleClick
        e.Cancel = True
        btnCellSelect.Focus()
    End Sub

    Private Sub EBomSpread1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles EBomSpread1.KeyDown
        activeCellValue = EBomSpread1.ActiveSheet.ActiveCell.Value
    End Sub

    Private Sub EBomSpread1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles EBomSpread1.KeyUp
        If e.KeyCode >= 1 And e.KeyCode <= 47 Then
            Exit Sub
        End If
        EBomSpread1.ActiveSheet.ActiveCell.Value = activeCellValue
    End Sub

    Private Sub EBomSpread1_Change(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles EBomSpread1.Change
        EBomSpread1.ActiveSheet.ActiveCell.Value = activeCellValue
    End Sub

    Private Sub EBomSpread1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles EBomSpread1.KeyPress
        EBomSpread1.ActiveSheet.ActiveCell.Value = activeCellValue
    End Sub
End Class
