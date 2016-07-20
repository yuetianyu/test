Imports System.Windows.Forms
Imports EventSakusei.ShisakuBuhinEdit.Kosei
Imports ShisakuCommon.Db.EBom.Vo

Public Class PreViewParent

    ''' <summary>画面制御ロジック</summary>
    Private _Subject As PreViewParentSubject


#Region "メンバ変数."
    Dim mEventCode As String
    Dim mBlockVo As TShisakuSekkeiBlockVo

#End Region

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal aEventCode As String, ByVal blockVo As TShisakuSekkeiBlockVo, ByVal frm As Frm41DispShisakuBuhinEdit20, ByVal aRange As FarPoint.Win.Spread.Model.CellRange)

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        mEventCode = aEventCode
        mBlockVo = blockVo

        Dim blockNo As String = blockVo.ShisakuBlockNo
        _Subject = New PreViewParentSubject(aEventCode, blockNo, frm, Me, aRange)

        Initialize()

    End Sub

#End Region

#Region "初期化メイン"

    ''' <summary>
    ''' 初期化メイン
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Initialize()
        Try
            Cursor.Current = Cursors.WaitCursor

            'pnlRedraw.Top = 0
            'pnlRedraw.Left = 0
            'pnlRedraw.Width = 1012
            'pnlRedraw.Height = 414

            'スプレッド初期化
            _Subject.InitSpd()

            '3D有無チェック
            _Subject.chkFileUmu()

            'XVLフォームを開く
            _Subject.ShowForm()

            '部品表フォームを開く
            _Subject.ShowFormParts()

        Catch ex As Exception

            Dim msg As String
            msg = String.Format("画面の初期化中に問題が発生しました(ERR={0})", ex.Message)
            Me.Close()

        Finally
            Cursor.Current = Cursors.Default
        End Try

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

    ''' <summary>
    ''' 戻る
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' ボディ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBody_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBody.Click
        Dim iBodyObj As frmBodySelect.EditDialogResult
        Dim ActiveForm As Form = Me.ActiveMdiChild

        If ActiveForm Is Nothing OrElse ActiveForm.Text = "部品表" Then
            MessageBox.Show("BODY画像を表示する3D画面を選択してください。", "3D情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        '3Dフォーム選択チェック
        Using frm As New frmBodySelect(mEventCode, _Subject.BodyName)

            If frm.Result Then
                iBodyObj = frm.Show(Me)
                '選択したボディー名を退避.
                If iBodyObj._TextValue IsNot Nothing Then
                    _Subject.BodyName = iBodyObj._TextValue

                    '_Subject.showWindowList()

                    '3Dフォーム再描画
                    _Subject.RedrawActiveForm(ActiveForm)
                End If
            End If

        End Using
    End Sub

    Private Sub PreViewParent_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub

    Private Sub PreViewParent_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        _Subject.DeleteXVL()
    End Sub
End Class
