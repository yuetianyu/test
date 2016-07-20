Imports System.Text
Imports ShisakuCommon
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo

Namespace EventEdit.Dao
    Public Class EventEditMShisakuSoubiDaoImpl : Inherits DaoEachFeatureImpl
        Implements EventEditMShisakuSoubiDao

        Public Function MaxShisakuRetuKoumokuCode(ByVal vo As MShisakuSoubiVo) As Integer Implements EventEditMShisakuSoubiDao.MaxShisakuRetuKoumokuCode
            Dim sql As New StringBuilder
            Dim db As New EBomDbClient
            With sql
                .AppendLine("SELECT top 1 [SHISAKU_SOUBI_KBN]")
                .AppendLine("      ,right('00000' + [SHISAKU_RETU_KOUMOKU_CODE],5) as shisaku_retu_koumoku_code")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_SOUBI")
                .AppendLine("  where shisaku_soubi_kbn=@ShisakuSoubiKbn")
                .AppendLine("  order by shisaku_soubi_kbn,shisaku_retu_koumoku_code desc")

            End With
            Dim rtnVo = db.QueryForObject(Of MShisakuSoubiVo)(sql.ToString, vo)
            Return Integer.Parse(rtnVo.ShisakuRetuKoumokuCode)
        End Function
    End Class
End Namespace
