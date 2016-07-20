Imports EventSakusei

Namespace YosanEventList

    ''' <summary>
    ''' 予算書イベント一覧画面・EXCELシート選択用
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FrmExcelSheetSelect

#Region "コンストラクタ"
        Public Sub New()

            ' この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent()

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
            Me.Close()
        End Sub

#End Region

#Region "ボタン(実行)"
        ''' <summary>
        ''' ボタン(実行)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnTehaiJyouhouFuka_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTehaiJyouhouFuka.Click

            '初期設定
            Dim i As Integer = 0
            ShisakuCommon.ExcelImportSheetName = ""

            '選択した値を取得する。
            For i = 0 To LBexcelSheetSelect.SelectedItems.Count - 1
                ShisakuCommon.ExcelImportSheetName = LBexcelSheetSelect.SelectedItems(i)
            Next

            '選択されていなければエラーメッセージを表示する。
            If ShisakuCommon.ExcelImportSheetName = "" Then
                MsgBox("シートを選択して下さい。", MsgBoxStyle.OkOnly, "警告")
            Else
                Dim result As Integer = frm00Kakunin.Confirm("確認", "選択したシートの情報を取り込みますか？", "", "OK", "CANCEL")
                If result = MsgBoxResult.Ok Then
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                End If
            End If

        End Sub

#End Region

#End Region

    End Class

End Namespace