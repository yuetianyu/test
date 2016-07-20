Imports FarPoint.Win.Spread

Namespace Ui
    ''' <summary>
    ''' 試作システム全体で共通的な、使用可否を制御するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShisakuEnabler
        ''' <summary>使用可否制御の時に取り扱う情報クラス</summary>
        Public Class Info
            ''' <summary>前景色</summary>
            Public ForeColor As Color
            ''' <summary>背景色</summary>
            Public BackColor As Color
            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="ForeColor">前景色</param>
            ''' <param name="BackColor">背景色</param>
            ''' <remarks></remarks>
            Public Sub New(ByVal ForeColor As Color, ByVal BackColor As Color)
                Me.ForeColor = ForeColor
                Me.BackColor = BackColor
            End Sub
        End Class
        ''' <summary>ボタンをDisabledにした時の情報</summary>
        Public ButtonDisabledInfo As New Info(Color.Black, Color.White)

        Private controlEnabledInfos As New Dictionary(Of Control, Info)
        ''' <summary>
        ''' 使用可時の情報を保存する
        ''' </summary>
        ''' <param name="control">対象のコントロール</param>
        ''' <remarks></remarks>
        Private Sub SaveEnabledInfo(ByVal control As Control)
            If Not control.Enabled Then
                Return
            End If
            If controlEnabledInfos.ContainsKey(control) Then
                Return
            End If
            controlEnabledInfos.Add(control, New Info(control.ForeColor, control.BackColor))
        End Sub
        ''' <summary>
        ''' 使用可時の情報を読み込みコントロールに設定する
        ''' </summary>
        ''' <param name="control">対象のコントロール</param>
        ''' <remarks></remarks>
        Private Sub LoadEnabledInfo(ByVal control As Control)
            If Not controlEnabledInfos.ContainsKey(control) Then
                Return
            End If
            Setting(control, controlEnabledInfos(control))
        End Sub
        ''' <summary>
        ''' 情報を設定する
        ''' </summary>
        ''' <param name="control">設定するコントロール</param>
        ''' <param name="aInfo">情報</param>
        ''' <remarks></remarks>
        Private Sub Setting(ByVal control As Control, ByVal aInfo As Info)
            control.ForeColor = aInfo.ForeColor
            control.BackColor = aInfo.BackColor
        End Sub
        ''' <summary>
        ''' 使用可否を設定する
        ''' </summary>
        ''' <param name="aButton">設定するボタン</param>
        ''' <param name="enabled">使用可否</param>
        ''' <remarks></remarks>
        Public Sub SettingEnabled(ByVal aButton As Button, ByVal enabled As Boolean)
            SaveEnabledInfo(aButton)
            If enabled Then
                LoadEnabledInfo(aButton)
            Else
                Setting(aButton, ButtonDisabledInfo)
            End If
            aButton.Enabled = enabled
        End Sub
        ''' <summary>
        ''' 使用可否を設定する
        ''' </summary>
        ''' <param name="aComboBox">設定するコンボボックス</param>
        ''' <param name="enabled">使用可否</param>
        ''' <remarks></remarks>
        Public Sub SettingEnabled(ByVal aComboBox As ComboBox, ByVal enabled As Boolean)
            aComboBox.Enabled = enabled
        End Sub
        ''' <summary>
        ''' 使用可否を設定する
        ''' </summary>
        ''' <param name="aTextBox">設定するテキストボックス</param>
        ''' <param name="enabled">使用可否</param>
        ''' <remarks></remarks>
        Public Sub SettingEnabled(ByVal aTextBox As TextBox, ByVal enabled As Boolean)
            aTextBox.Enabled = enabled
        End Sub
        ''' <summary>
        ''' 使用可否を設定する
        ''' </summary>
        ''' <param name="aSpread">設定するSpread</param>
        ''' <param name="enabled">使用可否</param>
        ''' <remarks></remarks>
        Public Sub SettingEnabled(ByVal aSpread As FpSpread, ByVal enabled As Boolean)
            aSpread.Enabled = enabled
        End Sub
        ''' <summary>
        ''' 使用可否を設定する
        ''' </summary>
        ''' <param name="aDateTimePicker">設定する日付コントロール</param>
        ''' <param name="enabled">使用可否</param>
        ''' <remarks></remarks>
        Public Sub SettingEnabled(ByVal aDateTimePicker As DateTimePicker, ByVal enabled As Boolean)
            aDateTimePicker.Enabled = enabled
        End Sub
    End Class
End Namespace