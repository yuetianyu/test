Imports ShisakuCommon.Db.EBom.Vo

Namespace Db
    ''' <summary>
    ''' 試作システムにとって共通的なDao
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface ShisakuDao : Inherits DaoEachFeature
        ''' <summary>
        ''' DBの日時を返す
        ''' </summary>
        ''' <returns>日時</returns>
        ''' <remarks></remarks>
        Function GetCurrentTimestamp() As DateTime

        ''' <summary>
        ''' EBomユーザー情報を返す
        ''' </summary>
        ''' <param name="userId">ユーザーID</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindUserById(ByVal userId As String) As Rhac0650Vo
    End Interface
End Namespace