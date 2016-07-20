Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports System.Text
Imports ShisakuCommon

''↓↓2014/08/21 1 ベース部品表作成表機能増強 ADD 
Namespace ShisakuBuhinMenu.Dao
    Public Class ShisakuBlockSekkeikaTmpDaoImpl
        Implements ShisakuBlockSekkeikaTmpDao

        ''' <summary>
        ''' Delete
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function DeleteByEventCode(ByVal eventCode As String) As Integer Implements ShisakuBlockSekkeikaTmpDao.DeleteByEventCode
            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("DELETE FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BLOCK_SEKKEIKA_TMP WITH (UPDLOCK) ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
            End With
            Dim param As New TShisakuBlockSekkeikaTmpVo
            param.ShisakuEventCode = eventCode
            Dim db As New EBomDbClient
            Return db.Delete(sql.ToString, param)

        End Function
        '↓↓2014/10/24 酒井 ADD BEGIN
        Public Function FindByAll4Ebom(ByVal eventCode As String) As List(Of ShisakuBlockSekkeikaTmp4EbomVo) Implements ShisakuBlockSekkeikaTmpDao.FindByAll4Ebom
            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT  * ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BLOCK_SEKKEIKA_TMP ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
            End With
            Dim param As New TShisakuBlockSekkeikaTmpVo
            param.ShisakuEventCode = eventCode
            Dim db As New EBomDbClient
            Return db.QueryForList(Of ShisakuBlockSekkeikaTmp4EbomVo)(sql.ToString(), param)
        End Function
        '↑↑2014/10/24 酒井 ADD END

    End Class
End Namespace