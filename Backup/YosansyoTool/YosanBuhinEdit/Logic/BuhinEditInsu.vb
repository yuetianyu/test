Imports ShisakuCommon

Namespace YosanBuhinEdit.Logic
    ''' <summary>
    ''' 員数クラス
    ''' </summary>
    ''' <remarks>とはいえ、状態は持っていない、ユーティリティ的なクラス</remarks>
    Public Class BuhinEditInsu
        ''' <summary>グリスなどに使う"**"の表示値</summary>
        Public Const GREASE_FORM_VALUE As String = "**"
        ''' <summary>グリスなどに使う"**"のDB値</summary>
        Public Const GREASE_DB_VALUE As Integer = -1

        ''' <summary>
        ''' 員数のDB値を表示値にして返す
        ''' </summary>
        ''' <param name="value">員数のDB値</param>
        ''' <returns>表示値</returns>
        ''' <remarks></remarks>
        Public Shared Function ConvDbToForm(ByVal value As Integer?) As String
            If value IsNot Nothing AndAlso value <= GREASE_DB_VALUE Then
                Return GREASE_FORM_VALUE
            End If
            Return StringUtil.ToString(value)
        End Function

        ''' <summary>
        ''' 員数の表示値をDB値にして返す
        ''' </summary>
        ''' <param name="value">員数の表示値</param>
        ''' <returns>DB値</returns>
        ''' <remarks></remarks>
        Public Shared Function ConvFormToDb(ByVal value As String) As Integer?
            If GREASE_FORM_VALUE.Equals(value) Then
                Return GREASE_DB_VALUE
            ElseIf IsNumeric(value) Then
                Return CInt(value)
            End If
            Return Nothing
        End Function
    End Class
End Namespace