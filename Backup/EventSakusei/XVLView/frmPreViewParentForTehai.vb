Imports System.Windows.Forms
Imports EventSakusei.ShisakuBuhinEdit.Kosei
Imports ShisakuCommon.Db.EBom.Vo



Public Class frmPreViewParentForTehai 

#Region "メンバ変数."
    ''' <summary>画面制御ロジック</summary>
    Private _Subject As PreViewParentSubjectForTehai



#End Region

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal aGUID As Guid, ByVal aEventCode As String, ByVal aGroup As String, ByVal aGousya As Dictionary(Of Integer, String))

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        _Subject = New PreViewParentSubjectForTehai(Me, aGUID, aEventCode, aGroup, aGousya)

        'Initialize()

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

#Region "初期化メイン"

    ''' <summary>
    ''' 初期化メイン
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Initialize()
        Dim time As New Stopwatch
        time.Start()

        Try
            Cursor.Current = Cursors.WaitCursor

            'イニシャルからデータ作成
            _Subject.CreateTempTable()
            Console.WriteLine("データ作成:" & time.Elapsed.ToString)
            time.Reset()
            time.Start()

            'スプレッド初期化
            _Subject.InitSpd()
            Console.WriteLine("スプレッド作成:" & time.Elapsed.ToString)
            time.Reset()
            time.Start()

            '3D有無チェック
            _Subject.chkFileUmu()

            '_Subject.chkFileUmu("")
            Console.WriteLine("3D判定完了:" & time.Elapsed.ToString)
            time.Reset()
            time.Start()

            '2014/04/08 kabasawa'
            '初回は開かない'
            'フォームを開く
            '_Subject.ShowForm()

            '部品表フォームを開く
            _Subject.ShowFormParts()
            Console.WriteLine("フォーム開く:" & time.Elapsed.ToString)

        Catch ex As Exception

            Dim msg As String
            msg = String.Format("画面の初期化中に問題が発生しました(ERR={0})", ex.Message)
            MessageBox.Show(msg)
            Me.Close()

        Finally
            '使用済みテーブル情報を削除.
            _Subject.DeleteTempData()

            Cursor.Current = Cursors.Default
        End Try

    End Sub

#End Region


#Region "フォームイベント処理"

    ''' <summary>
    ''' 戻るボタンクリック.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' ボディーボタンクリック.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBody_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBody.Click
        Dim iBodyObj As frmBodySelect.EditDialogResult
        Dim ActiveForm As Form = Me.ActiveMdiChild

        'If ActiveForm Is Nothing OrElse ActiveForm.Text = "部品表" Then
        '    MessageBox.Show("BODY画像を表示する3D画面を選択してください。", "3D情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Sub
        'End If

        Using frm As New frmBodySelect(_Subject.EventCode, _Subject.BodyName)

            If frm.Result Then
                iBodyObj = frm.Show(Me)
                '選択したボディー名を退避.
                If iBodyObj._TextValue IsNot Nothing Then
                    _Subject.BodyName = iBodyObj._TextValue

                    _Subject.ShowForm()

                    '3Dフォーム再描画
                    '_Subject.RedrawActiveForm(ActiveForm)

                End If
            End If
        End Using
    End Sub

    Private Sub PreViewParent_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        _Subject.DeleteXVL()
    End Sub

#End Region

#Region " ウィンドウ "
    ''' <summary>
    ''' 重ねて表示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub miWinCascade_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miWinCascade.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    ''' <summary>
    ''' 左右に並べて表示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub miWinTileHorizontal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miWinTileHorizontal.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    ''' <summary>
    ''' 上下に並べて表示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub miWinTileVertical_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miWinTileVertical.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

#End Region
End Class