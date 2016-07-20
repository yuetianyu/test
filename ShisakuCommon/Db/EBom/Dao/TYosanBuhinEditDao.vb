Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 予算書部品編集情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TYosanBuhinEditDao : Inherits DaoEachTable(Of TYosanBuhinEditVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="buhinName">部品表名</param>
        ''' <param name="yosanBlockNo">予算ブロック№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal yosanEventCode As String, ByVal buhinName As String, _
                          ByVal yosanBlockNo As String, ByVal buhinNoHyoujiJun As Int32) As TYosanBuhinEditVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="buhinName">部品表名</param>
        ''' <param name="yosanBlockNo">予算ブロック№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal yosanEventCode As String, ByVal buhinName As String, _
                            ByVal yosanBlockNo As String, ByVal buhinNoHyoujiJun As Int32) As Integer
    End Interface
End Namespace