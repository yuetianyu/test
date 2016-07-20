Namespace Ui
    ''' <summary>
    ''' マウスのカーソル
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CursorUtil
        ''' <summary>
        ''' マウスカーソルが砂時計になります
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub SetWaitCursor()
            Cursor.Current = Cursors.WaitCursor
        End Sub

        ''' <summary>
        ''' マウスカーソルが砂時計になります、何ミリ秒後Defaultに戻ります
        ''' </summary>
        ''' <param name="milliseconds">ミリ秒</param>
        ''' <remarks></remarks>
        Public Shared Sub SetWaitCursor(ByVal milliseconds As Integer)
            Cursor.Current = Cursors.WaitCursor
            System.Threading.Thread.Sleep(milliseconds)
            Cursor.Current = Cursors.Default
        End Sub

        ''' <summary>
        ''' マウスカーソルがDefaultになります
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub SetDefaultCursor()
            Cursor.Current = Cursors.Default
        End Sub
    End Class
End Namespace


