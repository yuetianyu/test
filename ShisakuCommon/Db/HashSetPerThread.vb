
Namespace Db
    Friend Class HashSetPerThread(Of T)
        Private valuePerThread As Hashtable = Hashtable.Synchronized(New Hashtable)
        Private Sub Add(ByVal value As T)
            valuePerThread.Add(System.Threading.Thread.CurrentThread, value)
        End Sub
        Private Sub Remove()
            valuePerThread.Remove(System.Threading.Thread.CurrentThread)
        End Sub
        Private Function Exist() As Boolean
            Return valuePerThread.ContainsKey(System.Threading.Thread.CurrentThread)
        End Function
        Private Function GetValue() As T
            Return valuePerThread(System.Threading.Thread.CurrentThread)
        End Function

    End Class
End Namespace
