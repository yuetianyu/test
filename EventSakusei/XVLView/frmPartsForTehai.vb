Imports EventSakusei.XVLView
Imports EventSakusei.XVLView.Logic

Public Class frmPartsForTehai
    ''' <summary>画面制御ロジック</summary>
    Private _Subject As PreViewParentSubjectForTehai

#Region "スプレッドイベント処理"


    ''' <summary>
    ''' スプレッド員数列クリック時の動作
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub spdParts_CellClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellClick

        '員数列以外EXIT
        If e.Row <> 1 Then Exit Sub
        'F品番列チェック & 'F品番アクティベート..
        If _Subject.isGousya(e.Column) Then _Subject.XLVActive(e.Column)

    End Sub


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

                        Dim iFlg As Integer = IIf(spdParts_Sheet1.Cells(3, 0).Value = True, 1, -1)
                        For iRow As Integer = 0 To spdParts_Sheet1.RowCount - 1
                            Dim iChk As String = spdParts_Sheet1.Cells(iRow, 1).Value
                            If iChk Is Nothing Then Continue For
                            Debug.Print("_" & spdParts_Sheet1.Cells(iRow, 0).Value & "_")
                            If iChk.Equals("○") Then
                                spdParts_Sheet1.SetValue(iRow, 0, IIf(iFlg = 1, True, False))
                                'Else
                                '    spdParts_Sheet1.SetValue(iRow, 0, False)
                            End If

                        Next
                        Application.DoEvents()
                End Select
        End Select
    End Sub

#End Region

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal Subject As PreViewParentSubjectForTehai)

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        _Subject = Subject



    End Sub
#End Region

    ''' <summary>
    ''' 画面閉じるとき
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmPartsForTehai_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        '当画面で使用されたファイルの削除処理を行う'

    End Sub

    Private Sub btnReDraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReDraw.Click
        _Subject.ShowForm()
    End Sub
End Class