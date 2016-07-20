Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 予算書部品編集ﾊﾟﾀｰﾝ情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TYosanBuhinEditPatternDao : Inherits DaoEachTable(Of TYosanBuhinEditPatternVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="buhinName">部品表名</param>
        ''' <param name="PatternHyoujiJun">パターン表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal yosanEventCode As String, ByVal buhinName As String, ByVal PatternHyoujiJun As Int32) As TYosanBuhinEditPatternVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="buhinName">部品表名</param>
        ''' <param name="PatternHyoujiJun">パターン表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal yosanEventCode As String, ByVal buhinName As String, ByVal PatternHyoujiJun As Int32) As Integer
    End Interface
End Namespace