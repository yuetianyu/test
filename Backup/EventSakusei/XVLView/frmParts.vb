Imports EventSakusei.XVLView
Imports EventSakusei.XVLView.Logic

Public Class frmParts

    ''' <summary>画面制御ロジック</summary>
    Private _Subject As PreViewParentSubject

    ''' <summary>
    ''' スプレッド内のボタンクリックイベント（チェックボックスクリックイベントを処理）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub spdParts_ButtonClicked(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles spdParts.ButtonClicked

        Select Case e.Column
            Case 0
                Select Case e.Row
                    Case 3  '表示チェックボックス

                        For iRow As Integer = 0 To spdParts_Sheet1.RowCount - 1
                            Dim iChk As String = spdParts_Sheet1.Cells(iRow, 1).Value
                            If iChk Is Nothing Then Continue For
                            Debug.Print("_" & spdParts_Sheet1.Cells(iRow, 0).Value & "_")
                            If iChk.Equals("○") Then spdParts_Sheet1.SetValue(iRow, 0, spdParts_Sheet1.Cells(3, 0).Value)
                        Next
                        Application.DoEvents()


                    Case Else

                        '_Subject.WindowReDraw(e.Row)

                End Select


        End Select

    End Sub

    ''' <summary>
    ''' スプレッド員数列クリック時の動作
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub spdParts_CellClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellClick
        '員数列以外EXIT
        If e.Row <> 2 Then Exit Sub
        'F品番列チェック & 'F品番アクティベート..
        If _Subject.isFhinban(e.Column) Then _Subject.XLVActive(e.Column)

    End Sub

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal Subject As PreViewParentSubject)

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        _Subject = Subject

    End Sub
#End Region

    ''' <summary>
    ''' 再描画ボタンイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnReDraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReDraw.Click
        _Subject.ShowForm()
    End Sub

End Class