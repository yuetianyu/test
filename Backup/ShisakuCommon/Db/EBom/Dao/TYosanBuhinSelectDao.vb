
Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 予算書部品表選択情報の簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TYosanBuhinSelectDao : Inherits DaoEachTable(Of TYosanBuhinSelectVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="YosanEventCode">予算イベントコード</param>
        ''' <param name="BuhinName">部品表名</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal YosanEventCode, _
                          ByVal BuhinName) As TYosanBuhinSelectVo
        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="YosanEventCode">予算イベントコード</param>
        ''' <param name="BuhinName">部品表名</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal YosanEventCode, _
                            ByVal BuhinName) As Integer

    End Interface
End Namespace


