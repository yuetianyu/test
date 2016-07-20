Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports ShisakuCommon
Imports ShisakuCommon.Ui

Namespace ShisakuBuhinExcel
    ''' <summary>
    ''' 試作部品表リスト画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm44ShisakuBuhinExcel

#Region " メンバー変数 "
        ''' <summary>ロジック</summary>
        Private m_buhinSakuseiList As DispShisakuBuhinExcel = Nothing
#End Region
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ShisakuFormUtil.Initialize(Me)
            '日付、時間を設定
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)

        End Sub
        ''' <summary>
        ''' 画面初期化
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Frm44ShisakuBuhinExcel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                m_buhinSakuseiList = New DispShisakuBuhinExcel(Me)
                m_buhinSakuseiList.InitView()
            Catch ex As Exception
                Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' ユーザーがセル内でマウスの左ボタンを押したら　処理する。
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdParts_CellClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellClick
            If e.ColumnHeader Then
                Exit Sub
            End If
            txtIbentoNo.Text = spdParts_Sheet1.GetText(e.Row, 0)
            m_buhinSakuseiList.SetGamenByEventCode()
        End Sub

        '''' <summary>
        '''' ダブルクリックしたら　処理する
        '''' </summary>
        '''' <param name="sender"></param>
        '''' <param name="e"></param>
        '''' <remarks></remarks>
        'Private Sub spdParts_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellDoubleClick
        '    If e.ColumnHeader Then
        '        Exit Sub
        '    End If
        '    txtIbentoNo.Text = spdParts_Sheet1.GetText(e.Row, 0)
        '    m_buhinSakuseiList.ToSakuseiMenuUpdateMode()
        'End Sub

        ''' <summary>
        ''' 戻るボタンを押したら　処理する
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Me.Close()
        End Sub

        ''' <summary>
        ''' アプリケーション終了ボタンを押したら　アプリケーションは終了する。
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
            Application.Exit()
            System.Environment.Exit(0)
        End Sub

        ''' <summary>
        ''' EXCEl出力ボタンを押したら　処理する
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelExport.Click
            Cursor.Current = Cursors.WaitCursor
            'm_buhinSakuseiList.ExcelBtnClick(txtIbentoNo.Text)
            Try
                m_buhinSakuseiList.ExcelBtnClick(txtIbentoNo.Text)
            Catch ex As Exception
                MsgBox("Excelの書込みに失敗しました。", MsgBoxStyle.Information, "エラー")
            End Try

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default
        End Sub

        Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)

        End Sub
        '''' <summary>
        '''' Time Tick
        '''' </summary>
        '''' <param name="sender"></param>
        '''' <param name="e"></param>
        '''' <remarks></remarks>
        'Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        '    ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        'End Sub

        Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        End Sub
    End Class
End Namespace
