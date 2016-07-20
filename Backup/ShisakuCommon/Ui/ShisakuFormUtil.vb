Imports EBom.Common

Namespace Ui
    ''' <summary>
    ''' 試作システム固有のForm関連のユーティリティ集
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShisakuFormUtil
#Region "画面タイトルとバージョン"
        Private Const SYSTEM_TITLE As String = "新試作手配システム "
        Private Const SYSTEM_VERSION As String = "Ver 2.11.5"
#End Region

#Region " システムのタイトルとバージョンを設定する"
        Public Shared Sub setTitleVersion(ByVal winForm As Form)

            winForm.Text = SYSTEM_TITLE + SYSTEM_VERSION

#If DEBUG Then
                        If ComFunc.ChkEBomEnv() = "\\fgnt08\PT\全車共通\E-BOMアプリ\INI\" Then
                            winForm.Text = "【 DB:本番環境 】 " + SYSTEM_TITLE + SYSTEM_VERSION
                        Else
                            winForm.Text = "*** DB:テスト環境 *** " + SYSTEM_TITLE + SYSTEM_VERSION
                        End If
#End If

        End Sub
#End Region

#Region " エラー表示は、背景色が赤で、文字色を白色にします"
        Public Shared Sub onErro(ByVal obj As Object)
            Dim objName = obj.GetType().Name
            Select Case objName
                Case "TextBox"
                    DirectCast(obj, TextBox).BackColor = Color.Red
                    DirectCast(obj, TextBox).ForeColor = Color.White
                Case "ComboBox"
                    DirectCast(obj, ComboBox).BackColor = Color.Red
                    DirectCast(obj, ComboBox).ForeColor = Color.White
                Case "Cell"
                    DirectCast(obj, FarPoint.Win.Spread.Cell).BackColor = Color.Red
                    DirectCast(obj, FarPoint.Win.Spread.Cell).ForeColor = Color.White
                Case "Row"
                    DirectCast(obj, FarPoint.Win.Spread.Row).BackColor = Color.Red
                    DirectCast(obj, FarPoint.Win.Spread.Row).ForeColor = Color.White
            End Select


        End Sub
#End Region

#Region " ワーニング表示は、背景色が赤で、文字色を黒色にします"
        Public Shared Sub onWarning(ByVal obj As Object)
            Dim objName = obj.GetType().Name
            Select Case objName
                Case "TextBox"
                    DirectCast(obj, TextBox).BackColor = Color.Yellow
                    DirectCast(obj, TextBox).ForeColor = Color.Black
                Case "ComboBox"
                    DirectCast(obj, ComboBox).BackColor = Color.Yellow
                    DirectCast(obj, ComboBox).ForeColor = Color.Black
                Case "Cell"
                    DirectCast(obj, FarPoint.Win.Spread.Cell).BackColor = Color.Yellow
                    DirectCast(obj, FarPoint.Win.Spread.Cell).ForeColor = Color.Black
                Case "Row"
                    DirectCast(obj, FarPoint.Win.Spread.Row).BackColor = Color.Yellow
                    DirectCast(obj, FarPoint.Win.Spread.Row).ForeColor = Color.Black
            End Select


        End Sub
#End Region




#Region " 背景色と文字色を普通に変更します"
        Public Shared Sub initlColor(ByVal obj As Object)
            Dim objName = obj.GetType().Name
            Select Case objName
                Case "TextBox"
                    DirectCast(obj, TextBox).BackColor = Nothing
                    DirectCast(obj, TextBox).ForeColor = Nothing
                Case "ComboBox"
                    DirectCast(obj, ComboBox).BackColor = Nothing
                    DirectCast(obj, ComboBox).ForeColor = Nothing
                Case "Cell"
                    CType(obj, FarPoint.Win.Spread.Cell).ResetBackColor()
                    CType(obj, FarPoint.Win.Spread.Cell).ResetForeColor()
                    'DirectCast(obj, FarPoint.Win.Spread.Cell).BackColor = Nothing
                    'DirectCast(obj, FarPoint.Win.Spread.Cell).ForeColor = Nothing
                Case "Row"
                    DirectCast(obj, FarPoint.Win.Spread.Row).BackColor = Nothing
                    DirectCast(obj, FarPoint.Win.Spread.Row).ForeColor = Nothing
            End Select
        End Sub
#End Region

#Region " 画面で　ユーザーIDと部課を設定する。"
        ''' <summary>
        ''' 画面で　ユーザーIDと部課を設定する
        ''' </summary>
        ''' <param name="LblCurrUserId"></param>
        ''' <param name="LblCurrBukaName"></param>
        ''' <remarks></remarks>
        Public Shared Sub SetIdAndBuka(ByVal LblCurrUserId As Label, ByVal LblCurrBukaName As Label)
            LblCurrUserId.Text = String.Format("(ID    ：{0})", LoginInfo.Now.UserId)
            LblCurrBukaName.Text = String.Format("(部課：{0})", LoginInfo.Now.BukaRyakuName)
        End Sub
#End Region

#Region " 画面で　日付と時間を設定する。"
        ''' <summary>
        ''' 画面で　日付と時間を設定する
        ''' </summary>
        ''' <param name="LblDate"></param>
        ''' <param name="LblTime"></param>
        ''' <remarks></remarks>
        Public Shared Sub SetDateTimeNow(ByVal LblDate As Label, ByVal LblTime As Label)
            LblDate.Text = DateTime.Now.ToString("yyyy/MM/dd")
            LblTime.Text = DateTime.Now.ToString("HH:mm:ss")
        End Sub
#End Region

#Region "Delete key を押す。値が変更すると　trueを戻る"

        Public Shared Function DelKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) As Boolean
            Dim retValue As Boolean = False
            If e.KeyValue = Keys.Delete Then
                Dim objName = sender.GetType().Name
                Select Case objName
                    Case "TextBox"
                        If Not DirectCast(sender, TextBox).Text = String.Empty Then
                            DirectCast(sender, TextBox).Text = ""
                            retValue = True
                        End If
                    Case "ComboBox"
                        If Not DirectCast(sender, ComboBox).Text = String.Empty Then
                            DirectCast(sender, ComboBox).Text = ""
                            retValue = True
                        End If
                    Case "FpSpread"
                        Dim sheet As FarPoint.Win.Spread.SheetView = DirectCast(sender, FarPoint.Win.Spread.FpSpread).ActiveSheet
                        If sheet.SelectionCount = 0 Then
                            DirectCast(sender, FarPoint.Win.Spread.FpSpread).ActiveSheet.ActiveCell.Value = ""
                        Else
                            For selIndex As Integer = 0 To sheet.SelectionCount - 1
                                '複数範囲選択場合

                                Dim cr As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(selIndex)
                                Dim startRowIdx As Integer = cr.Row
                                Dim endRowIdx As Integer = cr.Row + (cr.RowCount - 1)
                                Dim startColIdx As Integer = cr.Column
                                Dim endColIdx As Integer = cr.Column + (cr.ColumnCount - 1)
                                '列選択の場合
                                If cr.Row = -1 Then
                                    startRowIdx = 0
                                    endRowIdx = sheet.Rows.Count - 1
                                End If
                                '行選択の場合
                                If cr.Column = -1 Then
                                    startColIdx = 0
                                    endColIdx = sheet.Columns.Count - 1
                                End If
                                For rowIdx As Integer = startRowIdx To endRowIdx
                                    For colIdx As Integer = startColIdx To endColIdx
                                        '非表示列はクリアしない
                                        If Not sheet.Columns(colIdx).Visible Then
                                            Continue For
                                        End If
                                        If Not sheet.Cells(rowIdx, colIdx).Value = String.Empty Then
                                            sheet.Cells(rowIdx, colIdx).Value = ""
                                            retValue = True
                                        End If
                                    Next colIdx
                                Next rowIdx
                            Next
                        End If
                End Select
            End If
            Return retValue
        End Function
#End Region

#Region " システムのFormの位置を設定する"

        Public Shared Sub Initialize(ByVal winForm As Form)
            winForm.StartPosition = FormStartPosition.CenterScreen
        End Sub


#End Region


        ''' <summary>
        ''' イベント情報画面で使用するComboBoxの初期設定を行う
        ''' </summary>
        ''' <param name="aComboBox">コンボボックス</param>
        ''' <remarks></remarks>
        Public Shared Sub SettingDefaultProperty(ByVal aComboBox As ComboBox)
            '' 大文字化
            FormUtil.SettingComboBoxCharacterCasingUpper(aComboBox)
            '' 前方一致
            aComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            aComboBox.AutoCompleteSource = AutoCompleteSource.ListItems
            'aComboBox.DropDownStyle = ComboBoxStyle.DropDownList
        End Sub

        ''' <summary>
        ''' イベント情報画面で使用するTextBoxの初期設定を行う
        ''' </summary>
        ''' <param name="aTextBox">テキストボックス</param>
        ''' <remarks></remarks>
        Public Shared Sub SettingDefaultProperty(ByVal aTextBox As TextBox)
            '' 大文字化
            aTextBox.CharacterCasing = CharacterCasing.Upper
        End Sub
    End Class


End Namespace