Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>
    '''製作区分テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface AsSKMSDao : Inherits DaoEachTable(Of AsSKMSVo)



        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="BUKbn">B/U区分</param>
        ''' <param name="Snkm">製品区分</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal BUKbn As String, _
                          ByVal Snkm As String) As AsSKMSVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="BUKbn">B/U区分</param>
        ''' <param name="Snkm">製品区分</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal BUKbn As String, _
                            ByVal Snkm As String) As Integer

    End Interface
End Namespace