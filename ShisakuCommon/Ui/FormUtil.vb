Imports ShisakuCommon.Util.LabelValue

Namespace Ui
    ''' <summary>
    ''' 汎用的に使用できるForm関連のユーティリティ集
    ''' </summary>
    ''' <remarks>試作システムに依存しているなら、ShisakuFormUtil</remarks>
    Public Class FormUtil
        ''' <summary>コンストラクタ（非公開）</summary>
        Private Sub New()
        End Sub
        ''' <summary>
        ''' ComboBoxにラベルと値を設定する
        ''' </summary>
        ''' <param name="aCombobox">設定先のComboBox</param>
        ''' <param name="aLabelValues">ラベルと値の一覧</param>
        ''' <param name="hasBlank">ブランクを表示する場合、true</param>
        ''' <remarks></remarks>
        Public Shared Sub BindLabelValuesToComboBox(ByVal aCombobox As ComboBox, ByVal aLabelValues As List(Of LabelValueVo), Optional ByVal hasBlank As Boolean = False)
            Dim labelValues As New List(Of LabelValueVo)
            If aLabelValues IsNot Nothing AndAlso 0 < aLabelValues.Count Then
                If hasBlank Then
                    labelValues.Add(New LabelValueVo("", Nothing))
                End If
                labelValues.AddRange(aLabelValues)
            End If
            aCombobox.ValueMember = "Value"
            aCombobox.DisplayMember = "Label"
            aCombobox.DataSource = labelValues
        End Sub

        ''' <summary>
        ''' コンボボックスのキー入力時に小文字を大文字変換する設定をする
        ''' </summary>
        ''' <param name="aComboBox">対象のコンボボックス</param>
        ''' <remarks></remarks>
        Public Shared Sub SettingComboBoxCharacterCasingUpper(ByVal aComboBox As ComboBox)
            AddHandler aComboBox.KeyPress, AddressOf ComboBoxCharacterCasingUpper
        End Sub

        Private Shared Sub ComboBoxCharacterCasingUpper(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
            If Not TypeOf sender Is ComboBox Then
                Return
            End If
            Dim combo As ComboBox = CType(sender, ComboBox)
            If "a" <= e.KeyChar And e.KeyChar <= "z" Then
                'イベントキャンセル
                e.Handled = True
                '文字の挿入位置
                Dim Idx As Integer = combo.SelectionStart
                '選択範囲削除
                Dim Str As String = combo.Text.Remove(Idx, combo.SelectionLength)
                combo.Text = Str.Insert(Idx, e.KeyChar.ToString.ToUpper)
                '選択位置を再設定
                combo.SelectionStart = Idx + 1
            End If
        End Sub

#Region " コンボボックスで　データを設定する。"
        ''' <summary>
        ''' コンボボックスで　データを設定する        ''' </summary>
        ''' <param name="cmbItem">コンボボックス名</param>
        ''' <param name="dtData">data</param>
        ''' <param name="strValueKey">テーブルで項目</param>
        ''' <param name="strTextKey">テーブルで項目</param>
        ''' <remarks></remarks>
        Public Shared Sub ComboBoxBind(ByVal cmbItem As ComboBox, ByVal dtData As DataTable, ByVal strValueKey As String, ByVal strTextKey As String)

            dtData.Rows.InsertAt(dtData.NewRow(), 0)
            cmbItem.DataSource = dtData
            cmbItem.DisplayMember = strTextKey
            cmbItem.ValueMember = strValueKey
        End Sub
#End Region

        ''' <summary>
        ''' コンボボックスの選択値を設定する
        ''' </summary>
        ''' <param name="aComboBox">コンボボックス</param>
        ''' <param name="value">値(Null値可)</param>
        ''' <remarks>Null値を SelectedValue に設定する事を考慮</remarks>
        Public Shared Sub SetComboBoxSelectedValue(ByVal aComboBox As ComboBox, ByVal value As String)
            If value Is Nothing Then
                For i As Integer = 0 To aComboBox.Items.Count - 1
                    aComboBox.SelectedIndex = i
                    If aComboBox.SelectedValue Is Nothing Then
                        Return
                    End If
                Next
                aComboBox.SelectedIndex = -1
            Else
                aComboBox.SelectedValue = value
            End If
        End Sub
    End Class
End Namespace