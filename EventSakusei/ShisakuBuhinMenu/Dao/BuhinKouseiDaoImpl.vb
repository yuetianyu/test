Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon

Namespace ShisakuBuhinMenu.Dao
    Public Class BuhinKouseiDaoImpl
        Implements BuhinKouseiDao

        ''' <summary>
        ''' INSTL情報のINSTL品番で、最新改訂の RHAC0552 を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>最新改訂のRHAC0552情報</returns>
        ''' <remarks></remarks>
        Public Function FindNew0552ByShisakuInslt(ByVal shisakuEventCode As String) As List(Of BuhinKouseiResultVo) Implements BuhinKouseiDao.FindNew0552ByShisakuInslt
            Dim sql As String = "WITH RT (SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_GOUSYA, INSTL_HINBAN, INSTL_HINBAN_KBN, BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO, LV) AS ( " _
                    & "SELECT I.SHISAKU_BUKA_CODE, I.SHISAKU_BLOCK_NO, I.SHISAKU_GOUSYA, I.INSTL_HINBAN, I.INSTL_HINBAN_KBN, P.BUHIN_NO_OYA, P.BUHIN_NO_KO, P.KAITEI_NO, 0 LV " _
                    & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL I WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 P " _
                        & "ON I.BF_BUHIN_NO = P.BUHIN_NO_OYA " _
                    & "WHERE I.SHISAKU_EVENT_CODE = @Value " _
                        & "AND P.HAISI_DATE = 99999999  AND (P.SHUKEI_CODE <> ' ' OR P.SIA_SHUKEI_CODE <> ' ') " _
                    & "UNION ALL " _
                    & "SELECT P.SHISAKU_BUKA_CODE, P.SHISAKU_BLOCK_NO, P.SHISAKU_GOUSYA, P.INSTL_HINBAN, P.INSTL_HINBAN_KBN, C.BUHIN_NO_OYA, C.BUHIN_NO_KO, C.KAITEI_NO, P.LV + 1 " _
                    & "FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 C WITH (NOLOCK, NOWAIT) INNER JOIN RT P " _
                        & "ON C.BUHIN_NO_OYA = P.BUHIN_NO_KO " _
                    & "WHERE C.HAISI_DATE = 99999999 AND (C.SHUKEI_CODE <> ' ' OR C.SIA_SHUKEI_CODE <> ' ')  " _
                & ") " _
                & "SELECT B.SHISAKU_BUKA_CODE, B.SHISAKU_BLOCK_NO, B.SHISAKU_GOUSYA, B.INSTL_HINBAN, B.INSTL_HINBAN_KBN, K.* " _
                & "FROM (SELECT SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_GOUSYA, INSTL_HINBAN, INSTL_HINBAN_KBN, BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO " _
                        & "FROM RT WITH (NOLOCK, NOWAIT) " _
                        & "GROUP BY SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_GOUSYA, INSTL_HINBAN, INSTL_HINBAN_KBN, BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO) B " _
                    & "INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 K " _
                        & "ON B.BUHIN_NO_OYA = K.BUHIN_NO_OYA " _
                        & "AND B.BUHIN_NO_KO = K.BUHIN_NO_KO " _
                        & "AND B.KAITEI_NO = K.KAITEI_NO "
            Dim db As New EBomDbClient
            Return db.QueryForList(Of BuhinKouseiResultVo)(sql, shisakuEventCode)
        End Function

        ''' <summary>
        ''' INSTL情報のINSTL品番で、最新改訂の RHAC0553 を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>最新改訂のRHAC0553情報</returns>
        ''' <remarks></remarks>
        Public Function FindNew0553ByShisakuInslt(ByVal shisakuEventCode As String) As List(Of BuhinKouseiResultVo) Implements BuhinKouseiDao.FindNew0553ByShisakuInslt
            Dim sql As String = _
            "WITH RT (SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_GOUSYA, INSTL_HINBAN, INSTL_HINBAN_KBN, BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO, LV) AS ( " _
            & "SELECT I.SHISAKU_BUKA_CODE, I.SHISAKU_BLOCK_NO, I.SHISAKU_GOUSYA, I.INSTL_HINBAN, I.INSTL_HINBAN_KBN, P.BUHIN_NO_OYA, P.BUHIN_NO_KO, P.KAITEI_NO, 0 LV " _
            & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL I WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 P " _
            & "ON I.BF_BUHIN_NO = P.BUHIN_NO_OYA " _
            & "WHERE I.SHISAKU_EVENT_CODE = @Value " _
            & "AND P.HAISI_DATE = 99999999  AND (P.SHUKEI_CODE <> ' ' OR P.SIA_SHUKEI_CODE <> ' ') " _
            & "UNION ALL " _
            & "SELECT P.SHISAKU_BUKA_CODE, P.SHISAKU_BLOCK_NO, P.SHISAKU_GOUSYA, P.INSTL_HINBAN, P.INSTL_HINBAN_KBN, C.BUHIN_NO_OYA, C.BUHIN_NO_KO, C.KAITEI_NO, P.LV + 1 " _
            & "FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 C WITH (NOLOCK, NOWAIT) INNER JOIN RT P " _
            & "ON C.BUHIN_NO_OYA = P.BUHIN_NO_KO " _
            & "WHERE C.HAISI_DATE = 99999999 AND (C.SHUKEI_CODE <> ' ' OR C.SIA_SHUKEI_CODE <> ' ')  " _
            & ") " _
            & "SELECT B.SHISAKU_BUKA_CODE, B.SHISAKU_BLOCK_NO, B.SHISAKU_GOUSYA, B.INSTL_HINBAN, B.INSTL_HINBAN_KBN, K.* " _
            & "FROM (SELECT SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_GOUSYA, INSTL_HINBAN, INSTL_HINBAN_KBN, BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO " _
            & "FROM RT WITH (NOLOCK, NOWAIT) " _
            & "GROUP BY SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_GOUSYA, INSTL_HINBAN, INSTL_HINBAN_KBN, BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO) B " _
            & "INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 K " _
            & "ON B.BUHIN_NO_OYA = K.BUHIN_NO_OYA " _
            & "AND B.BUHIN_NO_KO = K.BUHIN_NO_KO " _
            & "AND B.KAITEI_NO = K.KAITEI_NO "
            Dim db As New EBomDbClient
            Return db.QueryForList(Of BuhinKouseiResultVo)(sql, shisakuEventCode)
        End Function

        ''' <summary>
        ''' INSTL情報のINSTL品番で、最新改訂の RHAC0551 を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>最新改訂のRHAC0551情報</returns>
        ''' <remarks></remarks>
        Public Function FindNew0551ByShisakuInslt(ByVal shisakuEventCode As String) As List(Of BuhinKouseiResultVo) Implements BuhinKouseiDao.FindNew0551ByShisakuInslt
            Dim sql As String = _
            "WITH RT (SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_GOUSYA, INSTL_HINBAN, INSTL_HINBAN_KBN, BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO, LV) AS ( " _
            & "SELECT I.SHISAKU_BUKA_CODE, I.SHISAKU_BLOCK_NO, I.SHISAKU_GOUSYA, I.INSTL_HINBAN, I.INSTL_HINBAN_KBN, P.BUHIN_NO_OYA, P.BUHIN_NO_KO, P.KAITEI_NO, 0 LV " _
            & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL I WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 P " _
            & "ON I.BF_BUHIN_NO = P.BUHIN_NO_OYA " _
            & "WHERE I.SHISAKU_EVENT_CODE = @Value " _
            & "AND  (P.SHUKEI_CODE <> ' ' OR P.SIA_SHUKEI_CODE <> ' ') " _
            & "AND P.KAITEI_NO = ( " _
            & "     SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & "     FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 WITH (NOLOCK, NOWAIT)   " _
            & "         WHERE BUHIN_NO_KO = P.BUHIN_NO_KO " _
            & "         AND BUHIN_NO_OYA = P.BUHIN_NO_OYA ) " _
            & "UNION ALL " _
            & "SELECT P.SHISAKU_BUKA_CODE, P.SHISAKU_BLOCK_NO, P.SHISAKU_GOUSYA, P.INSTL_HINBAN, P.INSTL_HINBAN_KBN, C.BUHIN_NO_OYA, C.BUHIN_NO_KO, C.KAITEI_NO, P.LV + 1 " _
            & "FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 C WITH (NOLOCK, NOWAIT) INNER JOIN RT P " _
            & "ON C.BUHIN_NO_OYA = P.BUHIN_NO_KO " _
            & "WHERE  (C.SHUKEI_CODE <> ' ' OR C.SIA_SHUKEI_CODE <> ' ')  " _
            & ") " _
            & "SELECT B.SHISAKU_BUKA_CODE, B.SHISAKU_BLOCK_NO, B.SHISAKU_GOUSYA, B.INSTL_HINBAN, B.INSTL_HINBAN_KBN, K.* " _
            & "FROM (SELECT SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_GOUSYA, INSTL_HINBAN, INSTL_HINBAN_KBN, BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO " _
            & "FROM RT WITH (NOLOCK, NOWAIT) " _
            & "GROUP BY SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_GOUSYA, INSTL_HINBAN, INSTL_HINBAN_KBN, BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO) B " _
            & "INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 K " _
            & "ON B.BUHIN_NO_OYA = K.BUHIN_NO_OYA " _
            & "AND B.BUHIN_NO_KO = K.BUHIN_NO_KO " _
            & "AND B.KAITEI_NO = K.KAITEI_NO " _
            & "AND B.KAITEI_NO = ( " _
            & "         SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & "         FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 WITH (NOLOCK, NOWAIT)   " _
            & "             WHERE BUHIN_NO_KO = B.BUHIN_NO_KO " _
            & "             AND BUHIN_NO_OYA = B.BUHIN_NO_OYA ) "
            Dim db As New EBomDbClient
            Return db.QueryForList(Of BuhinKouseiResultVo)(sql, shisakuEventCode)

        End Function



    End Class
End Namespace