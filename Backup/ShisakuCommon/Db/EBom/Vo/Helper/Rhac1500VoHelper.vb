Namespace Db.EBom.Vo.Helper
    Public Class Rhac1500VoHelper

        ''' <summary>
        ''' 個別ルールを解決した付加F品番を返す
        ''' </summary>
        ''' <param name="vo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ResolveFfBuhinNoFrom(ByVal vo As Rhac1500Vo) As String
            Return TrimBuhinNo(vo.FfBuhinNo)
        End Function
        ''' <summary>
        ''' 個別ルールを解決した基本F品番を返す
        ''' </summary>
        ''' <param name="vo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ResolveBfBuhinNoFrom(ByVal vo As Rhac1500Vo) As String
            Return TrimBuhinNo(vo.BfBuhinNo)
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
        ''' <param name="vos">A/Lの素</param>
        ''' <remarks></remarks>
        Public Shared Sub ResolveRule(ByVal ParamArray vos As Rhac1500Vo())
            For Each vo As Rhac1500Vo In vos
                If vo Is Nothing Then
                    Continue For
                End If
                vo.FfBuhinNo = ResolveFfBuhinNoFrom(vo)
                vo.BfBuhinNo = ResolveBfBuhinNoFrom(vo)
            Next
        End Sub
        ''' <summary>
        ''' 個別ルールを解決する
        ''' </summary>
        ''' <param name="vos">A/Lの素</param>
        ''' <remarks></remarks>
        Public Shared Sub ResolveRule(ByVal vos As List(Of Rhac1500Vo))
            ResolveRule(vos.ToArray)
        End Sub

        Private ReadOnly vo As Rhac1500Vo
        Public Sub New(ByVal vo As Rhac1500Vo)
            Me.vo = vo
        End Sub
    End Class
End Namespace