Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon
Imports System.Text
Imports EventSakusei.ShisakuBuhinEdit.KouseiBuhin.Dao.Vo

Namespace KouseiBuhin.Dao

    ''' <summary>
    ''' RHAC1320のイベント情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac1320EventDaoImpl : Implements Rhac1320EventDao

        ''' <summary>
        ''' イベント情報を取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByEventInfo(ByVal KaihatsuFugo As String) As List(Of Rhac1320EventVo) Implements Rhac1320EventDao.GetByEventInfo
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT R.EVENT_ID ")
                .AppendLine("  FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC1320 AS R ")
                .AppendLine(" WHERE R.KAIHATSU_FUGO = '" & KaihatsuFugo & "' ")
                .AppendLine("   AND R.END_DATE = '99999999'")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac1320EventVo)(sb.ToString, "")
        End Function

    End Class

End Namespace
