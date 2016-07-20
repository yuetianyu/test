Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid
    ''' <summary>
    ''' エラー項目の制御を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ErrorController
        Private controls As ErrorControl()
        ''' <summary>
        ''' 背景色をクリアする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ClearBackColor()
            If controls IsNot Nothing Then
                For Each errorControl As ErrorControl In controls
                    errorControl.ClearBackColor()
                Next
                controls = Nothing
            End If
        End Sub
        ''' <summary>
        ''' 背景色をエラー色にする
        ''' </summary>
        ''' <param name="controls">エラーになった項目たち</param>
        ''' <remarks></remarks>
        Public Sub SetBackColorOnError(ByVal ParamArray controls As ErrorControl())
            For Each errorControl As ErrorControl In controls
                errorControl.SetBackColorOnError()
            Next
            Me.controls = controls
        End Sub
        ''' <summary>
        ''' 背景色をエラー色にする
        ''' </summary>
        ''' <param name="controls">エラーになった項目たち</param>
        ''' <remarks></remarks>
        Public Sub SetBackColorOnWarning(ByVal ParamArray controls As ErrorControl())
            For Each errorControl As ErrorControl In controls
                errorControl.SetBackColorOnWarning()
            Next
            Me.controls = controls
        End Sub




        ''' <summary>
        ''' 最初のコントロールにフォーカスを当てる
        ''' </summary>
        ''' <param name="controls">エラーになった項目たち</param>
        ''' <remarks></remarks>
        Public Sub FocusAtFirstControl(ByVal ParamArray controls As ErrorControl())
            If 0 < controls.Length Then
                controls(0).Focus()
            End If
        End Sub
    End Class
End Namespace