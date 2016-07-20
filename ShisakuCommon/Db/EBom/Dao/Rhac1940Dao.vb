Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 属性テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac1940Dao

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kataDaihyouBuhinNo">型代表部品番号</param>
        ''' <param name="KataKaiteiNo">型改訂No</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal kataDaihyouBuhinNo As String, _
                          ByVal KataKaiteiNo As String) As Rhac1940Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kataDaihyouBuhinNo">型代表部品番号</param>
        ''' <param name="KataKaiteiNo">型改訂No</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal kataDaihyouBuhinNo As String, _
                            ByVal KataKaiteiNo As String) As Integer

    End Interface
End Namespace