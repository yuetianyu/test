Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>予算設定リストコード情報の簡単なCRUDを集めたDAO</summary>
    Public Interface TYosanSetteiListcodeDao : Inherits DaoEachTable(Of TYosanSetteiListcodeVo)

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="_ShisakuEventCode">試作イベントコード</param>
        ''' <param name="_YosanListHyojijunNo">予算リスト表示順</param>
        ''' <param name="_YosanListCode">予算リストコード</param>
        ''' <param name="_YosanGroupNo">予算グループ№</param>      
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal _ShisakuEventCode As String, _
                          ByVal _YosanListHyojijunNo As Nullable(Of Int32), _
                          ByVal _YosanListCode As String, _
                          ByVal _YosanGroupNo As String) As TYosanSetteiListcodeVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="_ShisakuEventCode">試作イベントコード</param>
        ''' <param name="_YosanListHyojijunNo">予算リスト表示順</param>
        ''' <param name="_YosanListCode">予算リストコード</param>
        ''' <param name="_YosanGroupNo">予算グループ№</param>         
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal _ShisakuEventCode As String, _
                          ByVal _YosanListHyojijunNo As Nullable(Of Int32), _
                          ByVal _YosanListCode As String, _
                          ByVal _YosanGroupNo As String) As Integer
    End Interface
End Namespace



