Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon

Namespace ShisakuBuhinMenu.Dao
    Public Class BuhinDaoImpl
        Implements BuhinDao

        'Public Function Find0532ByShisakuInstl(ByVal shisakuEventCode As String) As List(Of BuhinResultVo) Implements BuhinDao.Find0532ByShisakuInstl
        '    Dim sql As String = "WITH RT (SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_GOUSYA, BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO, LV) AS ( " _
        '            & "SELECT I.SHISAKU_BUKA_CODE, I.SHISAKU_BLOCK_NO, I.SHISAKU_GOUSYA, P.BUHIN_NO_OYA, P.BUHIN_NO_KO, P.KAITEI_NO, 0 LV " _
        '            & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL I INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 P " _
        '                & "ON I.INSTL_HINBAN = P.BUHIN_NO_OYA " _
        '            & "WHERE I.SHISAKU_EVENT_CODE = @Value " _
        '                & "AND P.HAISI_DATE = 99999999 " _
        '            & "UNION ALL " _
        '            & "SELECT RT.SHISAKU_BUKA_CODE, RT.SHISAKU_BLOCK_NO, RT.SHISAKU_GOUSYA, C.BUHIN_NO_OYA, C.BUHIN_NO_KO, C.KAITEI_NO, RT.LV & 1 " _
        '            & "FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 C INNER JOIN RT " _
        '                & "ON C.BUHIN_NO_OYA = RT.BUHIN_NO_KO " _
        '            & "WHERE C.HAISI_DATE = 99999999 " _
        '        & ") " _
        '        & "SELECT RT.SHISAKU_BUKA_CODE, RT.SHISAKU_BLOCK_NO, RT.SHISAKU_GOUSYA, COALESCE(Z.SHONIN_DATE, 0) SHONIN_DATE, B.* " _
        '        & "FROM RT INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 B " _
        '                & "ON RT.BUHIN_NO_OYA = B.BUHIN_NO " _
        '            & "INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0702 Z " _
        '                & "ON B.ZUMEN_NO = Z.ZUMEN_NO " _
        '                & "AND B.ZUMEN_KAITEI_NO = Z.ZUMEN_KAITEI_NO " _
        '        & "WHERE Z.ZUMEN_KAITEI_NO IN ( " _
        '                & "SELECT MAX(ZUMEN_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 WHERE ZUMEN_NO = Z.ZUMEN_NO AND 0 = COALESCE(SHONIN_DATE, 0) " _
        '                & "UNION ALL " _
        '                & "SELECT MAX(ZUMEN_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 WHERE ZUMEN_NO = Z.ZUMEN_NO AND 0 < COALESCE(SHONIN_DATE, 0) " _
        '            & ") "
        '    Dim db As New EBomDbClient
        '    Return db.QueryForList(Of BuhinResultVo)(sql, shisakuEventCode)
        'End Function

        Public Function Find0532ByShisakuKousei(ByVal shisakuEventCode As String, ByVal sakuseiCount As Integer) As List(Of BuhinResultVo) Implements BuhinDao.Find0532ByShisakuKousei
            ' TODO rhac032 の改訂No最新で縛っていない。大丈夫か？
            Dim sql As String = _
                "SELECT K.SHISAKU_BUKA_CODE, K.SHISAKU_BLOCK_NO, K.SHISAKU_BLOCK_NO_KAITEI_NO, K.SHISAKU_GOUSYA, COALESCE(Z.SHONIN_DATE, 0) SHONIN_DATE, Z.SHONIN_SIGN, B.* " _
                & "FROM (SELECT SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_BLOCK_NO_KAITEI_NO, SHISAKU_GOUSYA, BUHIN_NO " _
                & "			FROM (SELECT SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_BLOCK_NO_KAITEI_NO, SHISAKU_GOUSYA, BUHIN_NO_KO BUHIN_NO " _
                & "					FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_KOUSEI WITH (NOLOCK, NOWAIT) " _
                & "					WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & "						AND SAKUSEI_COUNT = @SakuseiCount " _
                & "					UNION ALL " _
                & "					SELECT SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_BLOCK_NO_KAITEI_NO, SHISAKU_GOUSYA, BF_BUHIN_NO BUHIN_NO " _
                & "					FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WITH (NOLOCK, NOWAIT) " _
                & "					WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & "					) A " _
                & "			GROUP BY SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_BLOCK_NO_KAITEI_NO, SHISAKU_GOUSYA, BUHIN_NO) K " _
                & "	INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 B " _
                & "		ON K.BUHIN_NO = B.BUHIN_NO " _
                & "	LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0702 Z " _
                & "		ON B.ZUMEN_NO = Z.ZUMEN_NO " _
                & "		AND B.ZUMEN_KAITEI_NO = Z.ZUMEN_KAITEI_NO " _
                & "WHERE (Z.ZUMEN_NO IS NULL) OR Z.ZUMEN_KAITEI_NO IN ( " _
                & "		SELECT MAX(CZ.ZUMEN_KAITEI_NO) " _
                & "		FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 CB WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0702 CZ " _
                & "			ON CB.ZUMEN_NO = CZ.ZUMEN_NO " _
                & "			AND CB.ZUMEN_KAITEI_NO = CZ.ZUMEN_KAITEI_NO " _
                & "		WHERE CB.BUHIN_NO = B.BUHIN_NO " _
                & "			AND 0 = COALESCE(CZ.SHONIN_DATE, 0) " _
                & "		UNION ALL " _
                & "		SELECT MAX(CZ.ZUMEN_KAITEI_NO) " _
                & "		FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 CB WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0702 CZ " _
                & "			ON CB.ZUMEN_NO = CZ.ZUMEN_NO " _
                & "			AND CB.ZUMEN_KAITEI_NO = CZ.ZUMEN_KAITEI_NO " _
                & "		WHERE CB.BUHIN_NO = B.BUHIN_NO " _
                & "			AND 0 < COALESCE(CZ.SHONIN_DATE, 0) " _
                & "	) " _
                & "ORDER BY K.SHISAKU_BUKA_CODE, K.SHISAKU_BLOCK_NO, K.SHISAKU_BLOCK_NO_KAITEI_NO, K.SHISAKU_GOUSYA, B.BUHIN_NO, B.KAITEI_NO"
            Dim param As New TShisakuBuhinKouseiVo
            param.ShisakuEventCode = shisakuEventCode
            param.SakuseiCount = sakuseiCount
            Dim db As New EBomDbClient
            Return db.QueryForList(Of BuhinResultVo)(sql, param)
        End Function

        Public Function Find0533ByShisakuKousei(ByVal shisakuEventCode As String, ByVal sakuseiCount As Integer) As List(Of BuhinResultVo) Implements BuhinDao.Find0533ByShisakuKousei
            ' TODO rhac032 の改訂No最新で縛っていない。大丈夫か？
            Dim sql As String = _
                "SELECT K.SHISAKU_BUKA_CODE, K.SHISAKU_BLOCK_NO, K.SHISAKU_BLOCK_NO_KAITEI_NO, K.SHISAKU_GOUSYA, B.* " _
                & "FROM (SELECT SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_BLOCK_NO_KAITEI_NO, SHISAKU_GOUSYA, BUHIN_NO " _
                & "			FROM (SELECT SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_BLOCK_NO_KAITEI_NO, SHISAKU_GOUSYA, BUHIN_NO_KO BUHIN_NO " _
                & "					FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_KOUSEI WITH (NOLOCK, NOWAIT) " _
                & "					WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & "						AND SAKUSEI_COUNT = @SakuseiCount " _
                & "					UNION ALL " _
                & "					SELECT SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_BLOCK_NO_KAITEI_NO, SHISAKU_GOUSYA, BF_BUHIN_NO BUHIN_NO " _
                & "					FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WITH (NOLOCK, NOWAIT) " _
                & "					WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & "					) A " _
                & "			GROUP BY SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_BLOCK_NO_KAITEI_NO, SHISAKU_GOUSYA, BUHIN_NO) K " _
                & "	INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 B " _
                & "		ON K.BUHIN_NO = B.BUHIN_NO " _
                & "	LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0703 Z " _
                & "		ON B.ZUMEN_NO = Z.ZUMEN_NO " _
                & "		AND B.ZUMEN_KAITEI_NO = Z.ZUMEN_KAITEI_NO " _
                & "WHERE (Z.ZUMEN_NO IS NULL) OR Z.ZUMEN_KAITEI_NO IN ( " _
                & "		SELECT MAX(CZ.ZUMEN_KAITEI_NO) " _
                & "		FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 CB WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0703 CZ " _
                & "			ON CB.ZUMEN_NO = CZ.ZUMEN_NO " _
                & "			AND CB.ZUMEN_KAITEI_NO = CZ.ZUMEN_KAITEI_NO " _
                & "		WHERE CB.BUHIN_NO = B.BUHIN_NO " _
                & "			 " _
                & "		UNION ALL " _
                & "		SELECT MAX(CZ.ZUMEN_KAITEI_NO) " _
                & "		FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 CB WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0703 CZ " _
                & "			ON CB.ZUMEN_NO = CZ.ZUMEN_NO " _
                & "			AND CB.ZUMEN_KAITEI_NO = CZ.ZUMEN_KAITEI_NO " _
                & "		WHERE CB.BUHIN_NO = B.BUHIN_NO " _
                & "			 " _
                & "	) " _
                & "ORDER BY K.SHISAKU_BUKA_CODE, K.SHISAKU_BLOCK_NO, K.SHISAKU_BLOCK_NO_KAITEI_NO, K.SHISAKU_GOUSYA, B.BUHIN_NO, B.KAITEI_NO"
            Dim param As New TShisakuBuhinKouseiVo
            param.ShisakuEventCode = shisakuEventCode
            param.SakuseiCount = sakuseiCount
            Dim db As New EBomDbClient
            Return db.QueryForList(Of BuhinResultVo)(sql, param)
        End Function

        Public Function Find0530ByShisakuKousei(ByVal shisakuEventCode As String, ByVal sakuseiCount As Integer) As List(Of BuhinResultVo) Implements BuhinDao.Find0530ByShisakuKousei
            ' TODO rhac032 の改訂No最新で縛っていない。大丈夫か？
            Dim sql As String = _
                "SELECT K.SHISAKU_BUKA_CODE, K.SHISAKU_BLOCK_NO, K.SHISAKU_BLOCK_NO_KAITEI_NO, K.SHISAKU_GOUSYA, COALESCE(Z.SHONIN_DATE, 0) DATE, Z.SHONIN_SIGN, B.* " _
                & "FROM (SELECT SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_BLOCK_NO_KAITEI_NO, SHISAKU_GOUSYA, BUHIN_NO " _
                & "			FROM (SELECT SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_BLOCK_NO_KAITEI_NO, SHISAKU_GOUSYA, BUHIN_NO_KO BUHIN_NO " _
                & "					FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_KOUSEI WITH (NOLOCK, NOWAIT) " _
                & "					WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & "						AND SAKUSEI_COUNT = @SakuseiCount " _
                & "					UNION ALL " _
                & "					SELECT SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_BLOCK_NO_KAITEI_NO, SHISAKU_GOUSYA, BF_BUHIN_NO BUHIN_NO " _
                & "					FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WITH (NOLOCK, NOWAIT) " _
                & "					WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & "					) A " _
                & "			GROUP BY SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_BLOCK_NO_KAITEI_NO, SHISAKU_GOUSYA, BUHIN_NO) K " _
                & "	INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 B " _
                & "		ON K.BUHIN_NO = B.BUHIN_NO " _
                & "	LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0700 Z " _
                & "		ON B.ZUMEN_NO = Z.ZUMEN_NO " _
                & "		AND B.ZUMEN_KAITEI_NO = Z.ZUMEN_KAITEI_NO " _
                & "WHERE (Z.ZUMEN_NO IS NULL) OR Z.ZUMEN_KAITEI_NO IN ( " _
                & "		SELECT MAX(CZ.ZUMEN_KAITEI_NO) " _
                & "		FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 CB WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0700 CZ " _
                & "			ON CB.ZUMEN_NO = CZ.ZUMEN_NO " _
                & "			AND CB.ZUMEN_KAITEI_NO = CZ.ZUMEN_KAITEI_NO " _
                & "		WHERE CB.BUHIN_NO = B.BUHIN_NO " _
                & "			AND 0 = COALESCE(CZ.SHONIN_DATE, 0) " _
                & "		UNION ALL " _
                & "		SELECT MAX(CZ.ZUMEN_KAITEI_NO) " _
                & "		FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 CB WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0700 CZ " _
                & "			ON CB.ZUMEN_NO = CZ.ZUMEN_NO " _
                & "			AND CB.ZUMEN_KAITEI_NO = CZ.ZUMEN_KAITEI_NO " _
                & "		WHERE CB.BUHIN_NO = B.BUHIN_NO " _
                & "			AND 0 < COALESCE(CZ.SHONIN_DATE, 0) " _
                & "	) " _
                & "ORDER BY K.SHISAKU_BUKA_CODE, K.SHISAKU_BLOCK_NO, K.SHISAKU_BLOCK_NO_KAITEI_NO, K.SHISAKU_GOUSYA, B.BUHIN_NO, B.KAITEI_NO"
            Dim param As New TShisakuBuhinKouseiVo
            param.ShisakuEventCode = shisakuEventCode
            param.SakuseiCount = sakuseiCount
            Dim db As New EBomDbClient
            Return db.QueryForList(Of BuhinResultVo)(sql, param)
        End Function
    End Class
End Namespace