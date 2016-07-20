Namespace Db.EBom.Vo.Helper
    Public Class Rhac0553VoHelper

        ''' <summary>廃止年月日</summary>
        Public Class HaisiDate
            ''' <summary>現在、無期限で有効な場合の廃止年月日</summary>
            Public Const NOW_EFFECTIVE_DATE As Integer = 99999999
        End Class

        ''' <summary>
        ''' 個別ルールを解決した部品番号(親)を返す
        ''' </summary>
        ''' <param name="vo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ResolveBuhinNoOyaFrom(ByVal vo As Rhac0553Vo) As String
            Return TrimBuhinNo(vo.BuhinNoOya)
        End Function
        ''' <summary>
        ''' 個別ルールを解決した部品番号(子)を返す
        ''' </summary>
        ''' <param name="vo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ResolveBuhinNoKoFrom(ByVal vo As Rhac0553Vo) As String
            Return TrimBuhinNo(vo.BuhinNoKo)
        End Function
        ''' <summary>
        ''' 部品番号から空白を除外する
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>空白を除外した部品番号</returns>
        ''' <remarks></remarks>
        Private Shared Function TrimBuhinNo(ByVal buhinNo As String) As String
            If buhinNo Is Nothing Then
                Return Nothing
            End If
            Return buhinNo.TrimEnd
        End Function
        ''' <summary>
        ''' 個別ルールを解決する
        ''' </summary>
        ''' <param name="vos">構成情報</param>
        ''' <remarks></remarks>
        Public Shared Sub ResolveRule(ByVal ParamArray vos As Rhac0553Vo())
            For Each vo As Rhac0553Vo In vos
                If vo Is Nothing Then
                    Continue For
                End If
                vo.BuhinNoKo = ResolveBuhinNoKoFrom(vo)
                vo.BuhinNoOya = ResolveBuhinNoOyaFrom(vo)
            Next
        End Sub
        ''' <summary>
        ''' 個別ルールを解決する
        ''' </summary>
        ''' <param name="vos">構成情報</param>
        ''' <remarks></remarks>
        Public Shared Sub ResolveRule(ByVal vos As List(Of Rhac0553Vo))
            ResolveRule(vos.ToArray)
        End Sub

        Private ReadOnly vo As Rhac0553Vo
        Public Sub New(ByVal vo As Rhac0553Vo)
            Me.vo = vo
        End Sub

    End Class
End Namespace