Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl

Namespace ShisakuBuhinEditBlock.Dao

    Public Interface ExclusiveControlBlockDao : Inherits DaoEachFeature

        ''' <summary>
        ''' テーブルへデータを追加する
        ''' </summary>
        ''' <param name="newTExclusiveControlBlockVo">排他制御ブロック情報</param>
        ''' <remarks></remarks>
        Sub InsetByPk(ByVal newTExclusiveControlBlockVo As TExclusiveControlBlockVo)

        ''' <summary>
        ''' テーブルのデータを更新する
        ''' </summary>
        ''' <param name="newTExclusiveControlBlockVo">排他制御ブロック情報</param>
        ''' <remarks></remarks>
        Sub UpdateByPk(ByVal newTExclusiveControlBlockVo As TExclusiveControlBlockVo)

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="newTExclusiveControlBlockVo">排他制御ブロック情報</param>
        ''' <remarks></remarks>
        Sub DeleteByPk(ByVal newTExclusiveControlBlockVo As TExclusiveControlBlockVo)

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPkBuka(ByVal shisakuEventCode As String, _
                          ByVal shisakuBukaCode As String) As TExclusiveControlBlockVo

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, _
                          ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String) As TExclusiveControlBlockVo


        ''' <summary>
        ''' テーブル値の検索結果を返す（キーは試作イベントコードのみ）
        ''' 試作イベントメニューでチェック用に使用
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function GetExclusiveControlBlock(ByVal shisakuEventCode As String) As List(Of TExclusiveControlBlockVo)

        ''' <summary>
        ''' テーブル値の検索結果を返す（キーは試作イベントコードのみ）
        ''' 試作イベントメニューでチェック用に使用（内線番号を取得する）
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakubukacode">試作部課コード</param>
        ''' <param name="shisakublockno">試作ブロック№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function GetShisakuSekkeiBlockNaisen(ByVal shisakuEventCode As String, _
                                             ByVal ShisakuBukaCode As String, _
                                             ByVal ShisakuBlockNo As String) As List(Of TShisakuSekkeiBlockVo)

        ''' <summary>
        ''' テーブル値の検索結果を返す（キーは試作イベントコードのみ）
        ''' 試作イベントコード、ブロック№が同一で他部課コードの場合
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakubukacode">試作部課コード</param>
        ''' <param name="shisakublockno">試作ブロック№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function GetOtherBukaBlock(ByVal shisakuEventCode As String, _
                                             ByVal ShisakuBukaCode As String, _
                                             ByVal ShisakuBlockNo As String) As TShisakuSekkeiBlockVo

    End Interface

End Namespace
