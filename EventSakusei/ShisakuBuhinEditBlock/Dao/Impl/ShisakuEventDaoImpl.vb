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

    Public Class ShisakuEventDaoImpl : Inherits DaoEachFeatureImpl
        Implements ShisakuEventDao

        Public Function GetExclusiveControlBlock(ByVal ShisakuEventCode As String) As TShisakuEventVo _
                                                   Implements ShisakuEventDao.FindByPk
            Dim sql As String = _
                "SELECT " _
                & "   STATUS " _
                & "   FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT WITH (NOLOCK, NOWAIT) " _
                & "   WHERE" _
                & "   SHISAKU_EVENT_CODE =@ShisakuEventCode"

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventVo
            param.ShisakuEventCode = ShisakuEventCode

            Return db.QueryForObject(Of TShisakuEventVo)(sql, param)

        End Function

    End Class
End Namespace