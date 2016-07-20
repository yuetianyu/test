Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    '''  試作イベントベース車情報(EBOM設変)テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuEventEbomKanshiDao : Inherits DaoEachTable(Of TShisakuEventEbomKanshiVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyoJiJun_No">表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, ByVal hyoJiJun_No As Integer) As TShisakuEventEbomKanshiVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyoJiJun_No">表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, ByVal hyoJiJun_No As Integer) As Integer



        Function FindByEvent(ByVal eventCode As String) As List(Of TShisakuEventEbomKanshiVo)
    End Interface


End Namespace

