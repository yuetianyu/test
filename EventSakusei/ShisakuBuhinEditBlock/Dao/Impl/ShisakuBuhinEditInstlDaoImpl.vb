Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports System.Text
Imports EventSakusei.ShisakuBuhinEdit.Al.Logic
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl

Public Class ShisakuBuhinEditInstlDaoImpl : Inherits DaoEachFeatureImpl
    Implements ShisakuBuhinEditInstlDao
    Private ReadOnly subject As BuhinEditAlSubject
    Private Function SELECT_NEWEST_KOSEI_BY_BUHIN_NO_OYA() As String
        Dim sql As String
        sql = "WITH RT (BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO, LV) AS ( " _
            & "	SELECT P.BUHIN_NO_OYA, P.BUHIN_NO_KO, P.KAITEI_NO, 0 LV " _
            & "	FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_KOUSEI P WITH (NOLOCK, NOWAIT) " _
            & "	WHERE P.BUHIN_NO_OYA = @Value " _
            & "	    AND P.HAISI_DATE = 99999999 AND (P.SHUKEI_CODE <> ' ' OR P.SIA_SHUKEI_CODE <> ' ')  " _
            & "	UNION ALL " _
            & "	SELECT C.BUHIN_NO_OYA, C.BUHIN_NO_KO, C.KAITEI_NO, P.LV + 1 " _
            & "	FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_KOUSEI C WITH (NOLOCK, NOWAIT) INNER JOIN RT P " _
            & "	    ON C.BUHIN_NO_OYA = P.BUHIN_NO_KO " _
            & "	WHERE C.HAISI_DATE = 99999999 AND (C.SHUKEI_CODE <> ' ' OR C.SIA_SHUKEI_CODE <> ' ')  " _
            & ") "

        Return sql

    End Function
    Private Function JOIN_TABLE() As String
        Dim sql As String
        sql = "   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BUHIN_EDIT	WITH (NOLOCK, NOWAIT)  " _
        & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN BUHIN " _
        & " 	ON BUHIN_EDIT.SHISAKU_EVENT_CODE = BUHIN.SHISAKU_EVENT_CODE " _
        & " 	AND BUHIN_EDIT.SHISAKU_BUKA_CODE = BUHIN.SHISAKU_BUKA_CODE " _
        & " 	AND BUHIN_EDIT.SHISAKU_BLOCK_NO = BUHIN.SHISAKU_BLOCK_NO " _
        & " 	AND BUHIN_EDIT.SHISAKU_BLOCK_NO_KAITEI_NO = BUHIN.SHISAKU_BLOCK_NO_KAITEI_NO " _
        & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_KOUSEI BUHIN_KOUSEI " _
        & " 	ON BUHIN.BUHIN_NO = BUHIN_KOUSEI.BUHIN_NO_OYA " _
        & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL BLOCK_INSTL " _
        & " 	ON BLOCK_INSTL.SHISAKU_EVENT_CODE = BUHIN.SHISAKU_EVENT_CODE " _
        & " 	AND BLOCK_INSTL.SHISAKU_BUKA_CODE = BUHIN.SHISAKU_BUKA_CODE " _
        & " 	AND BLOCK_INSTL.SHISAKU_BLOCK_NO = BUHIN.SHISAKU_BLOCK_NO " _
        & " 	AND BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO = BUHIN.SHISAKU_BLOCK_NO_KAITEI_NO " _
        & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK BLOCK " _
        & " 	ON BLOCK.SHISAKU_EVENT_CODE = BLOCK_INSTL.SHISAKU_EVENT_CODE " _
        & " 	AND BLOCK.SHISAKU_BUKA_CODE = BLOCK_INSTL.SHISAKU_BUKA_CODE " _
        & " 	AND BLOCK.SHISAKU_BLOCK_NO = BLOCK_INSTL.SHISAKU_BLOCK_NO " _
        & " 	AND BLOCK.SHISAKU_BLOCK_NO_KAITEI_NO = BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO "

        Return sql

    End Function

  


End Class
