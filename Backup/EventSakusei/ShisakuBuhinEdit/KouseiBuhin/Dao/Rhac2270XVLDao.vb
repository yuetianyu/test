Imports EventSakusei.ShisakuBuhinEdit.KouseiBuhin.Dao.Vo

Namespace KouseiBuhin.Dao

    Public Interface Rhac2270XVLDao

        ''' <summary>
        ''' RHAC2270からXVLの基本情報を取得する為のファンクション定義
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="BlockNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetXVLFileNameS(ByVal KaihatsuFugo As String, ByVal BlockNo As String) As List(Of Rhac2270XVLVo)

    End Interface

End Namespace
