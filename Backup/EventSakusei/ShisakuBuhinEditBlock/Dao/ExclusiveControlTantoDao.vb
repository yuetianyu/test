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

    Public Interface ExclusiveControlTantoDao : Inherits DaoEachFeature

        ''' <summary>
        ''' テーブルへデータを追加する
        ''' </summary>
        ''' <param name="newTExclusiveControlTantoVo">排他制御担当情報</param>
        ''' <remarks></remarks>
        Sub InsetByPk(ByVal newTExclusiveControlTantoVo As THoyouExclusiveControlTantoVo)

        ''' <summary>
        ''' テーブルのデータを更新する
        ''' </summary>
        ''' <param name="newTExclusiveControlTantoVo">排他制御担当情報</param>
        ''' <remarks></remarks>
        Sub UpdateByPk(ByVal newTExclusiveControlTantoVo As THoyouExclusiveControlTantoVo)

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="newTExclusiveControlTantoVo">排他制御担当情報</param>
        ''' <remarks></remarks>
        Sub DeleteByPk(ByVal newTExclusiveControlTantoVo As THoyouExclusiveControlTantoVo)

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <param name="hoyouBukaCode">補用部課コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPkBuka(ByVal hoyouEventCode As String, _
                          ByVal hoyouBukaCode As String) As THoyouExclusiveControlTantoVo

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <param name="hoyouBukaCode">補用部課コード</param>
        ''' <param name="hoyouTanto">補用担当№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal hoyouEventCode As String, _
                          ByVal hoyouBukaCode As String, _
                          ByVal hoyouTanto As String) As THoyouExclusiveControlTantoVo

        ''' <summary>
        ''' テーブル値の検索結果を返す（キーは試作イベントコードのみ）
        ''' 試作イベントメニューでチェック用に使用
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function GetExclusiveControlTanto(ByVal hoyouEventCode As String) As List(Of THoyouExclusiveControlTantoVo)

        ''' <summary>
        ''' テーブル値の検索結果を返す（キーは試作イベントコードのみ）
        ''' 試作イベントメニューでチェック用に使用（内線番号を取得する）
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <param name="hoyoubukacode">補用部課コード</param>
        ''' <param name="hoyouTanto">補用担当№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function GetHoyouSekkeiTantoNaisen(ByVal hoyouEventCode As String, _
                                             ByVal hoyouBukaCode As String, _
                                             ByVal hoyouTanto As String) As List(Of THoyouSekkeiTantoVo)

    End Interface

End Namespace
