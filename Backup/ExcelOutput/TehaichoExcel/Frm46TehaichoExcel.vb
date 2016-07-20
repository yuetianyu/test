Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports ShisakuCommon
Imports ShisakuCommon.Ui

Namespace TehaichoExcel
    ''' <summary>
    ''' 手配帳リスト画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm46TehaichoExcel

#Region " メンバー変数 "
        ''' <summary>ロジック</summary>
        Private m_TehaichoList As DispTehaichoExcel = Nothing
        ''' <summary>前画面からもらったイベントコード</summary>
        Private eventCode As String = ""
#End Region
        Public Sub New(ByVal shisakuEventCode As String)

            '前画面よりイベントコード取得
            eventCode = shisakuEventCode

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
        Private Sub Frm46TehaichoExcel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                m_TehaichoList = New DispTehaichoExcel(Me)
                m_TehaichoList.InitView(eventCode)
                '該当リストコードが無い場合、メッセージを表示して閉じる。
                If spdParts_Sheet1.Rows.Count = 0 Then
                    MsgBox("選択したイベントは手配帳未作成です。", MsgBoxStyle.Information, "アラーム")
                    Me.Close()
                End If
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
            'リストコードを設定
            txtListCode.Text = spdParts_Sheet1.GetText(e.Row, 1)
            m_TehaichoList.SetGamenByListCode()
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
        '    txtListCode.Text = spdParts_Sheet1.GetText(e.Row, 0)
        '    m_TehaichoList.ToTehaichoExcel()
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
            Try
                m_TehaichoList.ExcelBtnClick(eventCode, txtListCode.Text)
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
