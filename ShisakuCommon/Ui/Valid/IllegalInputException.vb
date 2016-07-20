Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid
    Public Class IllegalInputException : Inherits ShisakuException
        Private ReadOnly _Controls As ErrorControl()
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New()
        End Sub
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="message">メッセージ</param>
        ''' <param name="controls">エラーのコントロール</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal message As String, ByVal ParamArray controls As ErrorControl())
            MyBase.New(message)
            _Controls = controls
        End Sub
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="message">メッセージ</param>
        ''' <param name="innerException">元例外</param>
        ''' <param name="controls">エラーのコントロール</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal message As String, ByVal innerException As Exception, ByVal ParamArray controls As ErrorControl())
            MyBase.New(message, innerException)
            _Controls = controls
        End Sub
        Public ReadOnly Property ErrorControls() As ErrorControl()
            Get
                Return _Controls
            End Get
        End Property
    End Class
End Namespace