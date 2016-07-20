Imports ShisakuCommon
Imports FarPoint.Win.Spread
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Ui.Spd

Namespace EventEdit.Ui
    ''' <summary>
    ''' イベント情報画面で使用するSpread関係のユーティリティ集
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EventSpreadUtil
        ''' <summary>スプレッドの初期表示行</summary>
        Public Const SPREAD_DEFAULT_ROW_COUNT As Integer = 1000



        ''' <summary>
        ''' イベント情報画面のSpreadのタイトル行数を返す
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <returns>タイトル行数</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTitleRows(ByVal spreadSheet As SheetView) As Integer
            Return spreadSheet.Cells(0, 0).RowSpan
        End Function

        ''' <summary>
        ''' 列全体に新しいコンボボックスを設定する
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <param name="tag">設定先の列タグ名</param>
        ''' <param name="labelValues">ラベルと値の一覧</param>
        ''' <param name="containsBlank">空白を含める場合、true</param>
        ''' <remarks></remarks>
        Public Shared Sub BindNewComboBoxToColumn(ByVal spreadSheet As SheetView, ByVal tag As String, ByVal labelValues As List(Of LabelValueVo), Optional ByVal containsBlank As Boolean = True)

            SpreadUtil.BindCellTypeToColumn(spreadSheet, tag, SpreadUtil.CreateComboBoxCellType(labelValues, containsBlank))
        End Sub

        ''' <summary>
        ''' 列全体に新しいチェックボックスを設定する
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <param name="tag">設定先の列タグ名</param>
        ''' <param name="textTrue">選択時の文言</param>
        ''' <param name="textFalse">未選択時の文言</param>
        ''' <remarks></remarks>
        Public Shared Sub BindNewCheckBoxToColumn(ByVal spreadSheet As SheetView, ByVal tag As String, ByVal textTrue As String, ByVal textFalse As String)
            SpreadUtil.BindCellTypeToColumn(spreadSheet, tag, SpreadUtil.CreateCheckBoxCellType(textTrue, textFalse))
        End Sub

        ''' <summary>
        ''' イベント情報登録画面のSpread初期化
        ''' </summary>
        ''' <param name="aSpread">対象Spread</param>
        ''' <remarks></remarks>
        Public Shared Sub InitializeFrm9(ByVal aSpread As FpSpread)

            SpreadUtil.Initialize(aSpread)
            'IMEを使用可能にする。
            aSpread.ImeMode = Windows.Forms.ImeMode.NoControl

            If aSpread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            Dim sheet As SheetView = aSpread.Sheets(0)

            sheet.RowCount = EventSpreadUtil.GetTitleRows(sheet)
            '2012/03/19 シートの最大行数を２００に設定
            'sheet.RowCount = EventSpreadUtil.GetTitleRows(sheet) + SPREAD_DEFAULT_ROW_COUNT
            sheet.RowCount = EventSpreadUtil.GetTitleRows(sheet) + 200
        End Sub
    End Class
End Namespace