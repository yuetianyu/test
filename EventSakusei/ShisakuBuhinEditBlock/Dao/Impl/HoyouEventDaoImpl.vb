Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.EventEdit.Dao
Imports EventSakusei.ShisakuBuhinEditBlock
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EBom.Data
Imports EBom.Common

Namespace ShisakuBuhinEditBlock.Dao

    Public Class HoyouEventDaoImpl : Inherits DaoEachFeatureImpl
        Implements HoyouEventDao

        Public Function GetExclusiveControlTanto(ByVal HoyouEventCode As String) As THoyouEventVo _
                                                   Implements hoyouEventDao.FindByPk
            Dim sql As String = _
                "SELECT " _
                & "   STATUS " _
                & "   FROM " & MBOM_DB_NAME & ".dbo.T_HOYOU_EVENT WITH (NOLOCK, NOWAIT) " _
                & "   WHERE" _
                & "   HOYOU_EVENT_CODE =@HoyouEventCode"

            Dim db As New EBomDbClient
            Dim param As New THoyouEventVo
            param.HoyouEventCode = HoyouEventCode

            Return db.QueryForObject(Of THoyouEventVo)(sql, param)

        End Function

    End Class
End Namespace