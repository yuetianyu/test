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

Namespace ShisakuBuhinEditEvent.Dao

    Public Interface ExclusiveControlEventDao : Inherits DaoEachFeature

        ''' <summary>
        ''' テーブルへデータを追加する
        ''' </summary>
        ''' <param name="newTExclusiveControlEventVo">排他制御イベント情報</param>
        ''' <remarks></remarks>
        Sub InsetByPk(ByVal newTExclusiveControlEventVo As TExclusiveControlEventVo)

        ''' <summary>
        ''' テーブルのデータを更新する
        ''' </summary>
        ''' <param name="newTExclusiveControlEventVo">排他制御イベント情報</param>
        ''' <remarks></remarks>
        Sub UpdateByPk(ByVal newTExclusiveControlEventVo As TExclusiveControlEventVo)

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="newTExclusiveControlEventVo">排他制御イベント情報</param>
        ''' <remarks></remarks>
        Sub DeleteByPk(ByVal newTExclusiveControlEventVo As TExclusiveControlEventVo)

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="editMode">編集モード（1:手配担当,2:予算担当）</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, ByVal editMode As String) As TExclusiveControlEventVo

    End Interface

End Namespace
