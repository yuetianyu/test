Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Vo
Imports ShisakuCommon
Imports System.Text

Namespace ShisakuBuhinEdit.Kosei.Dao
    Public Class BuhinKouseiSourceSelectorDaoImpl : Implements BuhinKouseiSourceSelectorDao
        Private Function SELECT_NEWEST_KOSEI_BY_BUHIN_NO_OYA() As String

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("WITH RT (BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO, LV) AS ( ")
                .AppendLine("	SELECT P.BUHIN_NO_OYA, P.BUHIN_NO_KO, P.KAITEI_NO, 0 LV ")
                .AppendLine("	FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 P WITH (NOLOCK, NOWAIT) ")
                .AppendLine("	WHERE P.BUHIN_NO_OYA = @Value ")
                .AppendLine(" AND P.HAISI_DATE = 99999999 ")
                .AppendLine("	UNION ALL ")
                .AppendLine("	SELECT C.BUHIN_NO_OYA, C.BUHIN_NO_KO, C.KAITEI_NO, P.LV + 1 ")
                .AppendLine("	FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 C WITH (NOLOCK, NOWAIT) INNER JOIN RT P ")
                .AppendLine(" ON C.BUHIN_NO_OYA = P.BUHIN_NO_KO ")
                .AppendLine("	WHERE C.HAISI_DATE = 99999999  ")
                .AppendLine(" AND NOT (C.SHUKEI_CODE = ' ' AND C.SIA_SHUKEI_CODE =  ' ') ")
                .AppendLine(")")
            End With

            Return sb.ToString

        End Function

        'Private Const SELECT_NEWEST_KOSEI_BY_BUHIN_NO_OYA As String = _
        '    "WITH RT (BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO, LV) AS ( " _
        '    & "	SELECT P.BUHIN_NO_OYA, P.BUHIN_NO_KO, P.KAITEI_NO, 0 LV " _
        '    & "	FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 P WITH (NOLOCK, NOWAIT) " _
        '    & "	WHERE P.BUHIN_NO_OYA = @Value " _
        '    & "	    AND P.HAISI_DATE = 99999999 " _
        '    & "	UNION ALL " _
        '    & "	SELECT C.BUHIN_NO_OYA, C.BUHIN_NO_KO, C.KAITEI_NO, P.LV + 1 " _
        '    & "	FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 C WITH (NOLOCK, NOWAIT) INNER JOIN RT P " _
        '    & "	    ON C.BUHIN_NO_OYA = P.BUHIN_NO_KO " _
        '    & "	WHERE C.HAISI_DATE = 99999999  " _
        '    & "     AND NOT (C.SHUKEI_CODE = ' ' AND C.SIA_SHUKEI_CODE =  ' ') " _
        '    & ") "
        Function FindByDataEbom(ByVal ShisakuEventCode As String) As Boolean Implements BuhinKouseiSourceSelectorDao.FindByDataEbom
            Dim sb As New StringBuilder

            With sb
                .Remove(0, .Length)
                .AppendLine("WITH RT (BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO, LV) AS ( ")
                .AppendLine("	SELECT P.BUHIN_NO_OYA, P.BUHIN_NO_KO, P.KAITEI_NO, 0 LV ")
                .AppendLine("	FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 P WITH (NOLOCK, NOWAIT) ")
                .AppendLine("	WHERE P.BUHIN_NO_OYA = @Value ")
                .AppendLine("	    AND P.HAISI_DATE = 99999999 ")
                .AppendLine("  AND NOT (P.SHUKEI_CODE = 'J' AND P.SIA_SHUKEI_CODE = 'J' OR P.SHUKEI_CODE = 'J' AND  P.SIA_SHUKEI_CODE  <> 'J' OR P.SHUKEI_CODE = ' ' AND P.SIA_SHUKEI_CODE = 'J') ")
                .AppendLine("	UNION ALL ")
                .AppendLine("	SELECT C.BUHIN_NO_OYA, C.BUHIN_NO_KO, C.KAITEI_NO, P.LV + 1 ")
                .AppendLine("	FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 C WITH (NOLOCK, NOWAIT) INNER JOIN RT P ")
                .AppendLine("	    ON C.BUHIN_NO_OYA = P.BUHIN_NO_KO ")
                .AppendLine("	WHERE C.HAISI_DATE = 99999999  ")
                .AppendLine("     AND NOT (C.SHUKEI_CODE = ' ' AND C.SIA_SHUKEI_CODE =  ' ') ")
                .AppendLine(" AND NOT (C.SHUKEI_CODE = 'J' AND C.SIA_SHUKEI_CODE = 'J' OR C.SHUKEI_CODE = 'J' AND  C.SIA_SHUKEI_CODE  <> 'J' OR C.SHUKEI_CODE = ' ' AND C.SIA_SHUKEI_CODE = 'J')")
                .AppendLine(") ")
                .AppendLine("SELECT B.* ")
                .AppendLine("FROM RT K WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 B ")
                .AppendLine("	ON K.BUHIN_NO_KO = B.BUHIN_NO ")
                .AppendLine("	AND B.KAITEI_NO IN (SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 WHERE BUHIN_NO = B.BUHIN_NO AND NOT LEFT(B.BUHIN_NO,7) = '-----ZZ'  ) ")
                .AppendLine(" AND NOT (B.SHUKEI_CODE = 'J' AND B.SIA_SHUKEI_CODE = 'J' OR B.SHUKEI_CODE = 'J' AND  B.SIA_SHUKEI_CODE  <> 'J' OR B.SHUKEI_CODE = ' ' AND B.SIA_SHUKEI_CODE = 'J')")
            End With

            Dim db As New EBomDbClient
            'Return db.QueryForList(Of String)(sb.ToString, "")
            Return True

        End Function

        Function FindByDataShisaku(ByVal ShisakuEventCode As String) As List(Of String) Implements BuhinKouseiSourceSelectorDao.FindByDataShisaku
            Dim sb As New StringBuilder

            With sb
                .Remove(0, .Length)
                .AppendLine("WITH RT (BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO, LV) AS ( ")
                .AppendLine("	SELECT P.BUHIN_NO_OYA, P.BUHIN_NO_KO, P.KAITEI_NO, 0 LV ")
                .AppendLine("	FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 P WITH (NOLOCK, NOWAIT) ")
                .AppendLine("	WHERE P.BUHIN_NO_OYA = @Value ")
                .AppendLine("	    AND P.HAISI_DATE = 99999999 ")
                .AppendLine("  AND NOT (P.SHUKEI_CODE = 'J' AND P.SIA_SHUKEI_CODE = 'J' OR P.SHUKEI_CODE = 'J' AND  P.SIA_SHUKEI_CODE  <> 'J' OR P.SHUKEI_CODE = ' ' AND P.SIA_SHUKEI_CODE = 'J') ")
                .AppendLine("	UNION ALL ")
                .AppendLine("	SELECT C.BUHIN_NO_OYA, C.BUHIN_NO_KO, C.KAITEI_NO, P.LV + 1 ")
                .AppendLine("	FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 C WITH (NOLOCK, NOWAIT) INNER JOIN RT P ")
                .AppendLine("	    ON C.BUHIN_NO_OYA = P.BUHIN_NO_KO ")
                .AppendLine("	WHERE C.HAISI_DATE = 99999999  ")
                .AppendLine("     AND NOT (C.SHUKEI_CODE = ' ' AND C.SIA_SHUKEI_CODE =  ' ') ")
                .AppendLine(" AND NOT (C.SHUKEI_CODE = 'J' AND C.SIA_SHUKEI_CODE = 'J' OR C.SHUKEI_CODE = 'J' AND  C.SIA_SHUKEI_CODE  <> 'J' OR C.SHUKEI_CODE = ' ' AND C.SIA_SHUKEI_CODE = 'J')")
                .AppendLine(") ")
                .AppendLine("SELECT B.* ")
                .AppendLine("FROM RT K WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 B ")
                .AppendLine("	ON K.BUHIN_NO_KO = B.BUHIN_NO ")
                .AppendLine("	AND B.KAITEI_NO IN (SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 WHERE BUHIN_NO = B.BUHIN_NO AND NOT LEFT(B.BUHIN_NO,7) = '-----ZZ'  ) ")
                .AppendLine(" AND NOT (B.SHUKEI_CODE = 'J' AND B.SIA_SHUKEI_CODE = 'J' OR B.SHUKEI_CODE = 'J' AND  B.SIA_SHUKEI_CODE  <> 'J' OR B.SHUKEI_CODE = ' ' AND B.SIA_SHUKEI_CODE = 'J')")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of String)(sb.ToString, "")

        End Function

    End Class
End Namespace