Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl

Namespace ShisakuBuhinMenu.Dao

    Public Class TSeisakuHakouDaoImpl : Inherits DaoEachFeatureImpl
        Implements TSeisakuHakouDao

        'ヘッダー情報を取得'
        '   最新の改訂№を取得
        Public Function GetTSeisakuHakouHdStatus(ByVal hakouNo As String, ByVal status As String) As TSeisakuHakouHdVo Implements TSeisakuHakouDao.GetTSeisakuHakouHdStatus
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_HAKOU_HD HD WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     HAKOU_NO = @HakouNo " _
            & " AND KAITEI_NO=  " _
            & " (  " _
            & "     SELECT MAX(CONVERT(INT,COALESCE(KAITEI_NO,''))) AS KAITEI_NO  " _
            & "     FROM   " _
            & "	        " & MBOM_DB_NAME & ".dbo.T_SEISAKU_HAKOU_HD " _
            & "     WHERE  " _
            & "	        HAKOU_NO = HD.HAKOU_NO AND " _
            & "         STATUS = @Status " _
            & " ) "

            Dim db As New EBomDbClient
            Dim param As New TSeisakuHakouHdVo
            param.HakouNo = hakouNo
            param.Status = status

            Dim vo As TSeisakuHakouHdVo
            vo = db.QueryForObject(Of TSeisakuHakouHdVo)(sql, param)
            Return vo
        End Function

        'ヘッダー情報を取得'
        Public Function GetTSeisakuHakouHdKaitei(ByVal hakouNo As String, ByVal kaiteiNo As String) As List(Of TSeisakuHakouHdVo) Implements TSeisakuHakouDao.GetTSeisakuHakouHdKaitei
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_HAKOU_HD HD WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     HAKOU_NO = @HakouNo " _
            & " AND KAITEI_NO> @KaiteiNo "

            Dim db As New EBomDbClient
            Dim param As New TSeisakuHakouHdVo
            param.HakouNo = hakouNo
            param.KaiteiNo = kaiteiNo

            Return db.QueryForList(Of TSeisakuHakouHdVo)(sql, param)
        End Function

    End Class
End Namespace