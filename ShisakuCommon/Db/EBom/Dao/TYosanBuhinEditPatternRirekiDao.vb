Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 予算書部品編集ﾊﾟﾀｰﾝ履歴情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TYosanBuhinEditPatternRirekiDao : Inherits DaoEachTable(Of TYosanBuhinEditPatternRirekiVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="UnitKbn">ユニット区分</param>
        ''' <param name="registerDate">登録日</param>
        ''' <param name="PatternHyoujiJun">パターン表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal yosanEventCode As String, ByVal UnitKbn As String, _
                          ByVal registerDate As String, ByVal PatternHyoujiJun As Int32) As TYosanBuhinEditPatternRirekiVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="UnitKbn">ユニット区分</param>
        ''' <param name="registerDate">登録日</param>
        ''' <param name="PatternHyoujiJun">パターン表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal yosanEventCode As String, ByVal UnitKbn As String, _
                            ByVal registerDate As String, ByVal PatternHyoujiJun As Int32) As Integer
    End Interface
End Namespace