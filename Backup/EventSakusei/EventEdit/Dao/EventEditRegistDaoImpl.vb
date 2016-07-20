Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo

Namespace EventEdit.Dao
    Public Class EventEditRegistDaoImpl : Inherits DaoEachFeatureImpl
        Implements EventEditRegistDao

        ''' <summary>
        ''' 最後のイベントコードを取得する
        ''' </summary>
        ''' <param name="shisakuKaihatsuFugo">試作開発符号</param>
        ''' <param name="shisakuEventPhase">試作イベントフェーズ</param>
        ''' <param name="unitKbn">ユニット区分</param>
        ''' <returns>該当するイベント情報</returns>
        ''' <remarks></remarks>
        Public Function FindMaxShisakuEventCode(ByVal shisakuKaihatsuFugo As String, ByVal shisakuEventPhase As String, ByVal unitKbn As String) As TShisakuEventVo Implements EventEditRegistDao.FindMaxShisakuEventCode
            Dim sql As String = _
            " SELECT E.SHISAKU_EVENT_CODE " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT E " _
            & " WHERE " _
            & " E.SHISAKU_KAIHATSU_FUGO = @ShisakuKaihatsuFugo " _
            & " AND E.SHISAKU_EVENT_PHASE = @ShisakuEventPhase " _
            & " AND E.UNIT_KBN = @UnitKbn " _
            & " AND E.SHISAKU_EVENT_CODE = (" _
            & " SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( SHISAKU_EVENT_CODE,'' ) ) ) AS SHISAKU_EVENT_CODE " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT " _
            & " WHERE " _
            & " SHISAKU_EVENT_PHASE = E.SHISAKU_EVENT_PHASE " _
            & " AND SHISAKU_KAIHATSU_FUGO = E.SHISAKU_KAIHATSU_FUGO " _
            & " AND UNIT_KBN = E.UNIT_KBN) "

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventVo
            param.ShisakuKaihatsuFugo = shisakuKaihatsuFugo
            param.ShisakuEventPhase = shisakuEventPhase
            param.UnitKbn = unitKbn

            Return db.QueryForObject(Of TShisakuEventVo)(sql, param)
        End Function

    End Class
End Namespace
