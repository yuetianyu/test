Imports System.Text
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanEventNew.Dao.Impl

    Public Class YosanEventNewDaoImpl : Implements YosanEventNewDao

        ''' <summary>
        ''' MAX予算イベントコード
        ''' </summary>
        ''' <param name="eventCode">予算書</param>
        ''' <returns>MAX予算イベントコード</returns>
        ''' <remarks></remarks>
        Public Function FindMaxEventCode(ByVal eventCode As TYosanEventVo) As TYosanEventVo Implements YosanEventNewDao.FindMaxEventCode

            Dim sql As String = ""
            If StringUtil.Equals(eventCode.YosanKaihatsuFugo.Length, 3) Then
                sql = _
                    "SELECT " _
                    & "   MAX(YOSAN_EVENT_CODE) AS YOSAN_EVENT_CODE " _
                    & "   FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_EVENT " _
                    & "   WHERE" _
                    & "   YOSAN_KAIHATSU_FUGO=@YosanKaihatsuFugo" _
                    & "  AND substring(YOSAN_EVENT_CODE,5,1) =@YosanEvent"
            Else
                sql = _
                    "SELECT " _
                    & "   MAX(YOSAN_EVENT_CODE) AS YOSAN_EVENT_CODE " _
                    & "   FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_EVENT " _
                    & "   WHERE" _
                    & "   YOSAN_KAIHATSU_FUGO=@YosanKaihatsuFugo" _
                    & "  AND substring(YOSAN_EVENT_CODE,6,1) =@YosanEvent"
            End If

            Dim db As New EBomDbClient
            Dim param As New TYosanEventVo
            param.YosanEvent = eventCode.YosanEvent
            param.YosanKaihatsuFugo = eventCode.YosanKaihatsuFugo

            Return db.QueryForObject(Of TYosanEventVo)(sql, param)


        End Function


    End Class

End Namespace
