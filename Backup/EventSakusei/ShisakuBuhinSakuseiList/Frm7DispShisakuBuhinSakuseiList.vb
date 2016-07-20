Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports EventSakusei.ShisakuBuhinMenu
Imports ShisakuCommon
Imports ShisakuCommon.Ui

Namespace ShisakuBuhinSakuseiList
    ''' <summary>
    ''' 部品構成表示画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm7DispShisakuBuhinSakuseiList

#Region " メンバー変数 "
        ''' <summary>ロジック</summary>
        Private m_buhinSakuseiList As DispShisakuBuhinSakuseiList = Nothing
#End Region
        ''↓↓2014/12/23 4試作１課メニュー (TES)張 ADD BEGIN
#Region "試作１課メニューフラグ"
        ' 試作１課メニューフラグ
        Private _IsSisaku1Ka As Integer
        ''' <summary>試作１課メニューフラグ</summary>
        ''' <returns>試作１課メニューフラグ</returns>
        Public ReadOnly Property IsSisaku1Ka() As Integer
            Get
                Return _IsSisaku1Ka
            End Get
        End Property
#End Region
        ''↑↑2014/12/23 4試作１課メニュー (TES)張 ADD END
        Public Sub New(Optional ByVal IsSisaku1Ka As Integer = 0)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
            ''↓↓2014/12/23 4試作１課メニュー (TES)張 ADD BEGIN
            Me._IsSisaku1Ka = IsSisaku1Ka
            ''↑↑2014/12/23 4試作１課メニュー (TES)張 ADD END

        End Sub
        ''' <summary>
        ''' 画面初期化
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Frm7DispShisakuBuhinSakuseiList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                m_buhinSakuseiList = New DispShisakuBuhinSakuseiList(Me)
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
            m_buhinSakuseiList.ResetColor()
            txtIbentoNo.Text = spdParts_Sheet1.GetText(e.Row, 0)
            m_buhinSakuseiList.SetGamenByEventCode()
        End Sub

        ''' <summary>
        ''' 呼出しボタンを押したら　処理する
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCall.Click
            m_buhinSakuseiList.ResetColor()

            '編集モード選択画面を表示する。
            Dim isMode As String
            Using frmSelect As New FrmDispEditModeSelect()
                frmSelect.ShowDialog()

                If frmSelect.ResultOk = False Then
                    Return
                End If
                'ラジオボタンによりフラグを変更
                If frmSelect.IsTehai = True Then
                    isMode = TehaiTantoMode   '手配担当モード
                Else
                    isMode = YosanTantoMode   '予算担当モード
                End If

            End Using

            'イベントコードに値がある場合は、編集モードで「試作部品表作成メニュー」画面へ遷移する。
            m_buhinSakuseiList.ToSakuseiMenuUpdateMode(isMode)
        End Sub

        ''' <summary>
        ''' ダブルクリックしたら　処理する
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdParts_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellDoubleClick
            If e.ColumnHeader Then
                Exit Sub
            End If
            m_buhinSakuseiList.ResetColor()
            txtIbentoNo.Text = spdParts_Sheet1.GetText(e.Row, 0)

            '編集モード選択画面を表示する。
            Dim isMode As String
            Using frmSelect As New FrmDispEditModeSelect()
                frmSelect.ShowDialog()

                If frmSelect.ResultOk = False Then
                    Return
                End If
                'ラジオボタンによりフラグを変更
                If frmSelect.IsTehai = True Then
                    isMode = TehaiTantoMode   '手配担当モード
                Else
                    isMode = YosanTantoMode   '予算担当モード
                End If

            End Using

            m_buhinSakuseiList.ToSakuseiMenuUpdateMode(isMode)
        End Sub

        ''' <summary>
        ''' 新規作成ボタンを押したら　登録モードで「試作部品表作成メニュー」画面へ遷移する。
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnNEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNEW.Click
            m_buhinSakuseiList.ResetColor()
            '新規モードで「試作部品表作成メニュー」画面へ遷移する。
            m_buhinSakuseiList.ToSakuseiMenuAddMode()
        End Sub

        ''' <summary>
        ''' 削除ボタンを押したら　処理する
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
            m_buhinSakuseiList.DeleteBtnClick()
        End Sub

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
            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            m_buhinSakuseiList.ExcelBtnClick()

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

        End Sub

        Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)

            'If spdParts.ActiveSheet.ActiveRowIndex > -1 AndAlso _
            '   spdParts.ActiveSheet.ActiveColumnIndex = 0 Then

            'Else
            '    e.Cancel = True
            'End If

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
        ''' <summary>
        ''' 列が自動ソートされたら　処理する
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdParts_AutoSortedColumn(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.AutoSortedColumnEventArgs) Handles spdParts.AutoSortedColumn
            m_buhinSakuseiList.AutoSortedColumn(sender, e)
        End Sub
        ''' <summary>
        ''' 列の自動フィルタリングが実行されたら　処理する
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdParts_AutoFilteredColumn(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.AutoFilteredColumnEventArgs) Handles spdParts.AutoFilteredColumn
            m_buhinSakuseiList.AutoFilteredColumn(sender, e)
        End Sub

        Private Sub LblTimeNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblTimeNow.Click

        End Sub
        Private Sub LblLoginBukaName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblLoginBukaName.Click

        End Sub
        Private Sub LblLoginUserId_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblLoginUserId.Click

        End Sub

        Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        End Sub
    End Class
End Namespace
