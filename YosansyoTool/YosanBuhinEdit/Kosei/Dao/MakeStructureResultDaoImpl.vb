﻿Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon
Imports System.Text

Namespace YosanBuhinEdit.Kosei.Dao
    Public Class MakeStructureResultDaoImpl : Implements MakeStructureResultDao
        Private Function SELECT_NEWEST_KOSEI_BY_BUHIN_NO_OYA() As String
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("WITH RT (BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO, LV) AS ( ")
                .AppendLine("	SELECT P.BUHIN_NO_OYA, P.BUHIN_NO_KO, P.KAITEI_NO, 0 LV ")
                .AppendLine("	FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 P WITH (NOLOCK, NOWAIT) ")
                .AppendLine("	WHERE P.BUHIN_NO_OYA = @Value ")
                .AppendLine("	    AND P.HAISI_DATE = 99999999 ")
                .AppendLine("	UNION ALL ")
                .AppendLine("	SELECT C.BUHIN_NO_OYA, C.BUHIN_NO_KO, C.KAITEI_NO, P.LV + 1 ")
                .AppendLine("	FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 C WITH (NOLOCK, NOWAIT) INNER JOIN RT P ")
                .AppendLine("	    ON C.BUHIN_NO_OYA = P.BUHIN_NO_KO ")
                .AppendLine("	WHERE C.HAISI_DATE = 99999999  ")
                .AppendLine("     AND NOT (C.SHUKEI_CODE = ' ' AND C.SIA_SHUKEI_CODE =  ' ') ")
                .AppendLine(") ")
            End With
            Return sb.ToString

        End Function
 
        ''' <summary>
        ''' 部品番号（親）を基点に構成情報を取得して返す
        ''' </summary>
        ''' <param name="BuhinNoOya">部品番号（親）</param>
        ''' <returns>一連の構成情報</returns>
        ''' <remarks></remarks>
        Public Function FindStructure0552ByBuhinNoOya(ByVal BuhinNoOya As String) As List(Of Rhac0552Vo) Implements MakeStructureResultDao.FindStructure0552ByBuhinNoOya
            Dim sql As String = _
                SELECT_NEWEST_KOSEI_BY_BUHIN_NO_OYA _
                & "SELECT K.* " _
                & "FROM RT B WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 K " _
                & "	ON B.BUHIN_NO_OYA = K.BUHIN_NO_OYA " _
                & "	AND B.BUHIN_NO_KO = K.BUHIN_NO_KO " _
                & "	AND B.KAITEI_NO = K.KAITEI_NO "

            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0552Vo)(sql, BuhinNoOya)
        End Function

        ''' <summary>
        ''' 部品番号（親）を基点に構成を作り、子の部品情報を返す
        ''' </summary>
        ''' <param name="BuhinNoOya">部品番号（親）</param>
        ''' <returns>一連の構成情報に紐付く部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindStructure0552ByBuhinNoOyaAnd0532ForKo(ByVal BuhinNoOya As String) As List(Of Rhac0532Vo) Implements MakeStructureResultDao.FindStructure0552ByBuhinNoOyaAnd0532ForKo
            Dim sql As String = _
                SELECT_NEWEST_KOSEI_BY_BUHIN_NO_OYA _
                & "SELECT B.* " _
                & "FROM RT K WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 B " _
                & "	ON K.BUHIN_NO_KO = B.BUHIN_NO " _
                & "	AND B.KAITEI_NO IN (SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 WHERE BUHIN_NO = B.BUHIN_NO AND NOT LEFT(B.BUHIN_NO,7) = '-----ZZ'  ) "

            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0532Vo)(sql, BuhinNoOya)
        End Function

        ''' <summary>
        ''' 最終改訂の部品情報を返す
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>最終改訂の部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindLastestRhac0532MakerNameByBuhinNo(ByVal BuhinNo As String) As Rhac0532BuhinNoteVo Implements MakeStructureResultDao.FindLastestRhac0532MakerNameByBuhinNo
            Dim sql As String = "SELECT B.*, M.MAKER_NAME FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 B WITH (NOLOCK, NOWAIT) LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0610 M ON B.MAKER_CODE = M.MAKER_CODE WHERE B.BUHIN_NO = @Value AND B.KAITEI_NO IN " _
            & "(SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 WITH (NOLOCK, NOWAIT) WHERE BUHIN_NO = B.BUHIN_NO AND NOT LEFT(B.BUHIN_NO,7) = '-----ZZ' )"
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of Rhac0532BuhinNoteVo)(sql, BuhinNo)
        End Function

        ''' <summary>
        ''' 最終改訂の部品構成情報を返す
        ''' </summary>
        ''' <param name="BuhinNoKo">部品番号（子）</param>
        ''' <returns>最終改訂の部品構成情報</returns>
        ''' <remarks></remarks>
        Public Function FindLastestRhac0552ByBuhinNoKo(ByVal BuhinNoKo As String) As Rhac0552Vo Implements MakeStructureResultDao.FindLastestRhac0552ByBuhinNoKo
            Dim sql As String = _
                "SELECT * FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 P WITH (NOLOCK, NOWAIT) WHERE P.BUHIN_NO_KO = '" & BuhinNoKo & "' " _
                & " AND P.HAISI_DATE = 99999999 " _
                & "	    AND P.BUHIN_NO_OYA IN (SELECT MIN(BUHIN_NO_OYA) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 WITH (NOLOCK, NOWAIT) WHERE BUHIN_NO_KO = P.BUHIN_NO_KO AND HAISI_DATE = P.HAISI_DATE) "
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of Rhac0552Vo)(sql)
        End Function

        ''' <summary>
        ''' 部品番号（親）を基点に構成を作り、子の部品情報の取引先情報を返す
        ''' </summary>
        ''' <param name="BuhinNoOya">部品番号（親）</param>
        ''' <returns>一連の構成情報に紐付く子の取引先情報</returns>
        ''' <remarks></remarks>
        Public Function FindStructure0552ByBuhinNoOyaAnd0610ForKo(ByVal BuhinNoOya As String) As List(Of Rhac0610Vo) Implements MakeStructureResultDao.FindStructure0552ByBuhinNoOyaAnd0610ForKo

             Dim sb As New StringBuilder

            With sb
                .AppendLine(SELECT_NEWEST_KOSEI_BY_BUHIN_NO_OYA)
                .AppendLine(" SELECT CASE WHEN LEN(R0610.MAKER_CODE) != 0 THEN R0610.MAKER_CODE ")
                .AppendLine(" ELSE " & MBOM_DB_NAME & ".dbo.getMakerValues(R0532.BUHIN_NO, 'C') ")
                .AppendLine(" END AS MAKER_CODE, ")
                .AppendLine(" CASE WHEN LEN(R0610.MAKER_CODE) != 0 THEN R0610.MAKER_NAME ")
                .AppendLine(" ELSE " & MBOM_DB_NAME & ".dbo.getMakerValues(R0532.BUHIN_NO, 'N') ")
                .AppendLine(" END AS MAKER_NAME ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0610 R0610 ")
                .AppendLine(" INNER JOIN ( ")
                .AppendLine(" SELECT sub.MAKER_CODE, sub.BUHIN_NO ")
                .AppendLine(" FROM RT K INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 sub ")
                .AppendLine(" ON K.BUHIN_NO_KO = sub.BUHIN_NO ")
                .AppendLine(" AND sub.KAITEI_NO IN (SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 WHERE BUHIN_NO = sub.BUHIN_NO AND NOT LEFT(sub.BUHIN_NO,7) = '-----ZZ'  ) ")
                .AppendLine(" ) R0532 ")
                .AppendLine("  ON R0610.MAKER_CODE = R0532.MAKER_CODE ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0610Vo)(sb.ToString, BuhinNoOya)


        End Function

        ''' <summary>
        ''' 部品情報を取得する(RHAC0552)
        ''' </summary>
        ''' <param name="BFBuhinNo">BF部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinRhac0552(ByVal BFBuhinNo As String) As Rhac0552Vo Implements MakeStructureResultDao.FindByBuhinRhac0552
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 R WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " R.BUHIN_NO_OYA = @BuhinNoOya " _
            & "	    AND R.HAISI_DATE = 99999999  " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 WITH (NOLOCK, NOWAIT)   " _
            & " WHERE BUHIN_NO_KO = R.BUHIN_NO_KO " _
            & " AND BUHIN_NO_OYA = R.BUHIN_NO_OYA ) "
       
            Dim db As New EBomDbClient
            Dim param As New Rhac0552Vo
            param.BuhinNoOya = BFBuhinNo

            Return db.QueryForObject(Of Rhac0552Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品情報を取得する(RHAC0552)
        ''' 構成の１レベル目が取得できない場合、親情報のみ取得したいので
        ''' </summary>
        ''' <param name="BFBuhinNo">BF部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinRhac0552LIST(ByVal BFBuhinNo As String) As List(Of Rhac0552Vo) Implements MakeStructureResultDao.FindByBuhinRhac0552LIST
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 R WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " R.BUHIN_NO_OYA = @BuhinNoOya " _
            & "	    AND R.HAISI_DATE = 99999999  " _
            & "     AND NOT (R.SHUKEI_CODE = ' ' AND R.SIA_SHUKEI_CODE =  ' ' ) " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 WITH (NOLOCK, NOWAIT)   " _
            & " WHERE BUHIN_NO_KO = R.BUHIN_NO_KO " _
            & " AND BUHIN_NO_OYA = R.BUHIN_NO_OYA ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0552Vo
            param.BuhinNoOya = BFBuhinNo

            Return db.QueryForList(Of Rhac0552Vo)(sql, param)
        End Function


#Region "部品構成の取得(FM5以降)"
        Private Function SELECT_NEWEST_KOSEI_BY_BUHIN_NO_OYA_RHAC0553() As String
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("WITH RT (BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO, LV, KAIHATSU_FUGO) AS ( ")
                .AppendLine("	SELECT P.BUHIN_NO_OYA, P.BUHIN_NO_KO, P.KAITEI_NO, 0 LV, P.KAIHATSU_FUGO ")
                .AppendLine("	FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 P WITH (NOLOCK, NOWAIT) ")
                .AppendLine("	WHERE P.BUHIN_NO_OYA = 'T' ")
                .AppendLine(" AND P.BUHIN_NO_KO = @BuhinNoOya ")
                .AppendLine(" AND P.KAIHATSU_FUGO = @KaihatsuFugo ")
                .AppendLine(" AND P.HAISI_DATE = 99999999 ")
                .AppendLine("	UNION ALL ")
                .AppendLine("	SELECT C.BUHIN_NO_OYA, C.BUHIN_NO_KO, C.KAITEI_NO, P.LV + 1, C.KAIHATSU_FUGO ")
                .AppendLine("	FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 C WITH (NOLOCK, NOWAIT) INNER JOIN RT P ")
                .AppendLine(" ON C.BUHIN_NO_OYA = P.BUHIN_NO_KO ")
                .AppendLine(" AND C.KAIHATSU_FUGO = P.KAIHATSU_FUGO ")
                .AppendLine("	WHERE C.HAISI_DATE = 99999999   ")
                .AppendLine(" AND NOT (C.SHUKEI_CODE = ' ' AND C.SIA_SHUKEI_CODE =  ' ') ")
                .AppendLine(" )")
            End With

            Return sb.ToString
        End Function

        ''' <summary>
        ''' ベース車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="Gousya">試作号車</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBase(ByVal shisakuEventCode As String, ByVal Gousya As String) As TShisakuEventBaseVo Implements MakeStructureResultDao.FindByBase
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_GOUSYA = @ShisakuGousya "

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventBaseVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuGousya = Gousya
            Return db.QueryForObject(Of TShisakuEventBaseVo)(sql, param)
        End Function

        ''' <summary>
        ''' イベント情報を取得する
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <returns>イベント情報</returns>
        ''' <remarks></remarks>
        Public Function FindByEvent(ByVal hoyouEventCode As String) As THoyouEventVo Implements MakeStructureResultDao.FindByEvent
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_HOYOU_EVENT WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendLine(" HOYOU_EVENT_CODE = @HoyouEventCode ")
            End With

            Dim db As New EBomDbClient
            Dim param As New THoyouEventVo
            param.HoyouEventCode = hoyouEventCode
            Return db.QueryForObject(Of THoyouEventVo)(sb.ToString, param)
        End Function

        ''' <summary>
        ''' 開発符号を取得する
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <returns>イベント情報</returns>
        ''' <remarks></remarks>
        Public Function FindByKaihatsugo(ByVal hoyouEventCode As String) As THoyouEventVo Implements MakeStructureResultDao.FindByKaihatsuFugo
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_HOYOU_EVENT WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " HOYOU_EVENT_CODE = @HoyouEventCode "

            Dim db As New EBomDbClient
            Dim param As New THoyouEventVo

            param.HoyouEventCode = hoyouEventCode

            Return db.QueryForObject(Of THoyouEventVo)(sql, param)
        End Function

        ''' <summary>
        ''' ブロックINSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="BFBuhinNo">BF部品番号</param>
        ''' <returns>設計ブロックINSTL情報</returns>
        ''' <remarks></remarks>
        Public Function FindByInstlBase(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal BFBuhinNo As String) As TShisakuEventBaseVo Implements MakeStructureResultDao.FindByInstlBase

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B")
                .AppendLine(" ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE")
                .AppendLine(" AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA")
                .AppendLine(" WHERE ")
                .AppendLine(" SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SBI.BF_BUHIN_NO = @BfBuhinNo ")
                .AppendLine(" AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WITH (NOLOCK, NOWAIT)   ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                .AppendLine(" ORDER BY B.HYOJIJUN_NO ")
            End With

            Dim db As New EBomDbClient
            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.BfBuhinNo = BFBuhinNo
            Return db.QueryForObject(Of TShisakuEventBaseVo)(sb.ToString, param)
        End Function

        ''' <summary>
        ''' 部品情報を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="BFBuhinNo">BF部品番号</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinRhac0553(ByVal kaihatsuFugo As String, ByVal BFBuhinNo As String) As Rhac0553Vo Implements MakeStructureResultDao.FindByBuhinRhac0553
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 R WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " R.KAIHATSU_FUGO = @KaihatsuFugo " _
            & " AND R.BUHIN_NO_OYA = 'T' " _
            & " AND R.BUHIN_NO_KO = @BuhinNoOya " _
            & "	    AND R.HAISI_DATE = 99999999  " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553  WITH (NOLOCK, NOWAIT)  " _
            & " WHERE KAIHATSU_FUGO = R.KAIHATSU_FUGO " _
            & " AND BUHIN_NO_OYA = R.BUHIN_NO_OYA  " _
            & " AND BUHIN_NO_KO = R.BUHIN_NO_KO ) "
      
            Dim db As New EBomDbClient
            Dim param As New Rhac0553Vo
            param.BuhinNoOya = BFBuhinNo
            param.KaihatsuFugo = kaihatsuFugo

            Return db.QueryForObject(Of Rhac0553Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品情報を取得する(A/L用)
        ''' </summary>
        ''' <param name="BFBuhinNo">BF部品番号</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinRhac0553AL(ByVal BFBuhinNo As String) As Rhac0553Vo Implements MakeStructureResultDao.FindByBuhinRhac0553AL
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 R WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " R.BUHIN_NO_OYA = @BuhinNoOya " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 WITH (NOLOCK, NOWAIT)   " _
            & " WHERE KAIHATSU_FUGO = R.KAIHATSU_FUGO " _
            & " AND BUHIN_NO_OYA = R.BUHIN_NO_OYA ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0553Vo
            param.BuhinNoOya = BFBuhinNo

            Return db.QueryForObject(Of Rhac0553Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品番号（親）を基点に構成情報を取得して返す
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号</param>
        ''' <returns>一連の構成情報</returns>
        ''' <remarks></remarks>
        Public Function FindStructure0553ByBuhinNoOya(ByVal buhinNoOya As String, ByVal kaihatsuFugo As String) As List(Of Rhac0553Vo) Implements MakeStructureResultDao.FindStructure0553ByBuhinNoOya
            Dim sql As String = _
            SELECT_NEWEST_KOSEI_BY_BUHIN_NO_OYA_RHAC0553() _
            & "SELECT K.* " _
            & "FROM RT B WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 K " _
            & "	ON B.BUHIN_NO_OYA = K.BUHIN_NO_OYA " _
            & "	AND B.BUHIN_NO_KO = K.BUHIN_NO_KO " _
            & " AND NOT K.BUHIN_NO_OYA = 'T' " _
            & " AND B.KAIHATSU_FUGO = K.KAIHATSU_FUGO " _
            & "	AND B.KAITEI_NO = K.KAITEI_NO "
            Dim param As New Rhac0553Vo
            param.BuhinNoOya = buhinNoOya
            param.KaihatsuFugo = kaihatsuFugo
            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0553Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品番号（親）を基点に構成を作り、子の部品情報を返す
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号</param>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>一連の構成情報に紐付く部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindStructure0553ByBuhinNoOyaAnd0533ForKo(ByVal buhinNoOya As String, ByVal kaihatsuFugo As String) As List(Of Rhac0533Vo) Implements MakeStructureResultDao.FindStructure0553ByBuhinNoOyaAnd0533ForKo
            Dim sql As String = _
            SELECT_NEWEST_KOSEI_BY_BUHIN_NO_OYA_RHAC0553() _
            & "SELECT B.* " _
            & "FROM RT K WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 B " _
            & "	ON K.BUHIN_NO_KO = B.BUHIN_NO " _
            & "	AND B.KAITEI_NO IN (SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 WITH (NOLOCK, NOWAIT) WHERE BUHIN_NO = B.BUHIN_NO AND NOT LEFT(B.BUHIN_NO,7) = '-----ZZ'  ) "


            Dim param As New Rhac0553Vo
            Dim db As New EBomDbClient
            param.BuhinNoOya = buhinNoOya
            param.KaihatsuFugo = kaihatsuFugo
            Return db.QueryForList(Of Rhac0533Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 最終改訂の部品情報を返す
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>最終改訂の部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindLastestRhac0533MakerNameByBuhinNo(ByVal BuhinNo As String) As Rhac0533BuhinNoteVo Implements MakeStructureResultDao.FindLastestRhac0533MakerNameByBuhinNo
            'メーカーコードが無いので取得しない'
            Dim sql As String = _
            "SELECT B.* FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 B WITH (NOLOCK, NOWAIT) " _
            & " WHERE B.BUHIN_NO = @Value AND B.KAITEI_NO IN " _
            & "(SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 WITH (NOLOCK, NOWAIT) WHERE BUHIN_NO = B.BUHIN_NO AND NOT LEFT(B.BUHIN_NO,7) = '-----ZZ'  )"

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of Rhac0533BuhinNoteVo)(sql, BuhinNo)
        End Function

        ''' <summary>
        ''' 最終改訂の部品構成情報を返す
        ''' </summary>
        ''' <param name="BuhinNoKo">部品番号（子）</param>
        ''' <returns>最終改訂の部品構成情報</returns>
        ''' <remarks></remarks>
        Public Function FindLastestRhac0553ByBuhinNoKo(ByVal BuhinNoKo As String) As Rhac0553Vo Implements MakeStructureResultDao.FindLastestRhac0553ByBuhinNoKo
            Dim sql As String = _
            "SELECT * FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 P WITH (NOLOCK, NOWAIT) WHERE P.BUHIN_NO_KO = @Value AND P.HAISI_DATE = 99999999 " _
            & "	    AND P.BUHIN_NO_OYA IN (SELECT MIN(BUHIN_NO_OYA) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 WITH (NOLOCK, NOWAIT) WHERE BUHIN_NO_KO = P.BUHIN_NO_KO AND HAISI_DATE = P.HAISI_DATE) "
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of Rhac0553Vo)(sql, BuhinNoKo)
        End Function

        ''' <summary>
        ''' 部品番号（親）を基点に構成を作り、子の部品情報の取引先情報を返す
        ''' </summary>
        ''' <param name="BuhinNoOya">部品番号（親）</param>
        ''' <returns>一連の構成情報に紐付く子の取引先情報</returns>
        ''' <remarks></remarks>
        Public Function FindStructure0553ByBuhinNoOyaAnd0610ForKo(ByVal BuhinNoOya As String) As List(Of Rhac0610Vo) Implements MakeStructureResultDao.FindStructure0553ByBuhinNoOyaAnd0610ForKo
            Dim sql As String = _
            SELECT_NEWEST_KOSEI_BY_BUHIN_NO_OYA_RHAC0553() _
            & "SELECT * FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0610 WITH (NOLOCK, NOWAIT) WHERE MAKER_CODE IN ( " _
            & "	SELECT B.MAKER_CODE " _
            & "	FROM RT K WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 B " _
            & "		ON K.BUHIN_NO_KO = B.BUHIN_NO " _
            & "		AND B.KAITEI_NO IN (SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 WITH (NOLOCK, NOWAIT) WHERE BUHIN_NO = B.BUHIN_NO AND NOT LEFT(B.BUHIN_NO,7) = '-----ZZ'  ) " _
            & "	)"

            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0610Vo)(sql, BuhinNoOya)
        End Function

        ''' <summary>
        ''' 部品情報を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="BFBuhinNo">BF部品番号</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinRhac0553LIST(ByVal kaihatsuFugo As String, ByVal BFBuhinNo As String) As List(Of Rhac0553Vo) Implements MakeStructureResultDao.FindByBuhinRhac0553LIST
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 R WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " R.KAIHATSU_FUGO = @KaihatsuFugo " _
            & " AND R.BUHIN_NO_OYA = @BuhinNoOya " _
            & "	    AND R.HAISI_DATE = 99999999  " _
            & "     AND NOT (R.SHUKEI_CODE = ' ' AND R.SIA_SHUKEI_CODE =  ' ' ) " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553  WITH (NOLOCK, NOWAIT)  " _
            & " WHERE KAIHATSU_FUGO = R.KAIHATSU_FUGO " _
            & " AND BUHIN_NO_OYA = R.BUHIN_NO_OYA ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0553Vo
            param.BuhinNoOya = BFBuhinNo
            param.KaihatsuFugo = kaihatsuFugo

            Return db.QueryForList(Of Rhac0553Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品編集INSTL情報を取得する
        ''' </summary>
        ''' <param name="yosanEventCode"></param>
        ''' <param name="patternName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindbyEditInstl(ByVal yosanEventCode As String, _
                                        ByVal patternName As String) As List(Of TYosanBuhinEditInsuVo) Implements MakeStructureResultDao.FindbyEditInstl
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT DISTINCT BEI.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_BUHIN_EDIT_PATTERN SBI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_YOSAN_BUHIN_EDIT_INSU BEI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BEI.YOSAN_EVENT_CODE = SBI.YOSAN_EVENT_CODE ")
                .AppendLine(" AND BEI.PATTERN_HYOUJI_JUN = SBI.PATTERN_HYOUJI_JUN ")
                .AppendLine(" AND BEI.BUHINHYO_NAME = SBI.BUHINHYO_NAME ")
                .AppendLine(" WHERE ")
                .AppendLine(" SBI.YOSAN_EVENT_CODE = @YosanEventCode ")
                .AppendLine(" AND SBI.PATTERN_NAME = @PatternName ")
            End With

            Dim db As New EBomDbClient
            Dim param As New TYosanBuhinEditPatternVo
            param.YosanEventCode = yosanEventCode
            param.PatternName = patternName

            Return db.QueryForList(Of TYosanBuhinEditInsuVo)(sb.ToString, param)
        End Function

        ''' <summary>
        ''' 部品編集情報を取得する
        ''' </summary>
        ''' <param name="yosanEventCode"></param>
        ''' <param name="patternName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindbyEdit(ByVal yosanEventCode As String, _
                                   ByVal patternName As String) As List(Of TYosanBuhinEditVo) Implements MakeStructureResultDao.FindbyEdit
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT DISTINCT BE.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BE.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BE.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" WHERE ")
                .AppendLine(" SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SBI.INSTL_HINBAN = @InstlHinban ")
                .AppendLine(" AND SBI.INSTL_HINBAN_KBN = @InstlHinbanKbn ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                .AppendLine("    SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine("     FROM   ")
                .AppendLine("	        " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK  ")
                .AppendLine("     WHERE  ")
                .AppendLine("	        SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine("     AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine("     AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO )")
            End With

            Dim db As New EBomDbClient
            Dim param As New TYosanBuhinEditPatternVo
            param.yosanEventCode = yosanEventCode
            param.PatternName = patternName

            Return db.QueryForList(Of TYosanBuhinEditVo)(sb.ToString, param)
        End Function


#End Region

#Region "部品構成の取得(パンダ前)"
        Private Function SELECT_NEWEST_KOSEI_BY_BUHIN_NO_OYA_RHAC0551() As String
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("WITH RT (BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO, LV) AS ( ")
                .AppendLine("	SELECT P.BUHIN_NO_OYA, P.BUHIN_NO_KO, P.KAITEI_NO, 0 LV ")
                .AppendLine("	FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 P WITH (NOLOCK, NOWAIT) ")
                .AppendLine("	WHERE P.BUHIN_NO_OYA = @Value ")
                .AppendLine(" AND P.KAITEI_NO = ( ")
                .AppendLine("     SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO ")
                .AppendLine("         FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 WITH (NOLOCK, NOWAIT)   ")
                .AppendLine("         WHERE BUHIN_NO_KO = P.BUHIN_NO_KO ")
                .AppendLine("           AND BUHIN_NO_OYA = P.BUHIN_NO_OYA ) ")
                .AppendLine("	UNION ALL ")
                .AppendLine("	SELECT C.BUHIN_NO_OYA, C.BUHIN_NO_KO, C.KAITEI_NO, P.LV + 1 ")
                .AppendLine("	FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 C WITH (NOLOCK, NOWAIT) INNER JOIN RT P ")
                .AppendLine("	    ON C.BUHIN_NO_OYA = P.BUHIN_NO_KO ")
                .AppendLine("	WHERE   ")
                .AppendLine("     NOT (C.SHUKEI_CODE = ' ' AND C.SIA_SHUKEI_CODE =  ' ') ")
                .AppendLine(") ")
            End With
            Return sb.ToString
        End Function
  
        ''' <summary>
        ''' 部品情報を取得する(RHAC0551)
        ''' </summary>
        ''' <param name="BFBuhinNo">BF部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinRhac0551(ByVal BFBuhinNo As String) As Rhac0551Vo Implements MakeStructureResultDao.FindByBuhinRhac0551
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 R WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " R.BUHIN_NO_OYA = @BuhinNoOya " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 WITH (NOLOCK, NOWAIT)   " _
            & " WHERE BUHIN_NO_KO = R.BUHIN_NO_KO " _
            & "     AND BUHIN_NO_OYA = R.BUHIN_NO_OYA ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0551Vo
            param.BuhinNoOya = BFBuhinNo

            Return db.QueryForObject(Of Rhac0551Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品番号（親）を基点に構成情報を取得して返す
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号</param>
        ''' <returns>一連の構成情報</returns>
        ''' <remarks></remarks>
        Public Function FindStructure0551ByBuhinNoOya(ByVal buhinNoOya As String) As List(Of Rhac0551Vo) Implements MakeStructureResultDao.FindStructure0551ByBuhinNoOya
            Dim sql As String = _
            SELECT_NEWEST_KOSEI_BY_BUHIN_NO_OYA_RHAC0551() _
            & "SELECT K.* " _
            & "FROM RT B WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 K " _
            & "	ON B.BUHIN_NO_OYA = K.BUHIN_NO_OYA " _
            & "	AND B.BUHIN_NO_KO = K.BUHIN_NO_KO " _
            & "	AND B.KAITEI_NO = K.KAITEI_NO "

            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0551Vo)(sql, buhinNoOya)
        End Function

        ''' <summary>
        ''' 部品番号（親）を基点に構成を作り、子の部品情報を返す
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号</param>
        ''' <returns>一連の構成情報に紐付く部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindStructure0551ByBuhinNoOyaAnd0530ForKo(ByVal buhinNoOya As String) As List(Of Rhac0530Vo) Implements MakeStructureResultDao.FindStructure0551ByBuhinNoOyaAnd0530ForKo
            Dim sql As String = _
            SELECT_NEWEST_KOSEI_BY_BUHIN_NO_OYA_RHAC0551() _
            & "SELECT B.* " _
            & "FROM RT K WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 B " _
            & "	ON K.BUHIN_NO_KO = B.BUHIN_NO " _
            & "	AND B.KAITEI_NO IN (SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 WITH (NOLOCK, NOWAIT) WHERE BUHIN_NO = B.BUHIN_NO ) "

            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0530Vo)(sql, buhinNoOya)
        End Function

        ''' <summary>
        ''' 最終改訂の部品情報を返す
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>最終改訂の部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindLastestRhac0530MakerNameByBuhinNo(ByVal BuhinNo As String) As Rhac0530MakerNameVo Implements MakeStructureResultDao.FindLastestRhac0530MakerNameByBuhinNo
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT B.*, M.MAKER_NAME FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 B WITH (NOLOCK, NOWAIT) LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0610 M ON B.MAKER_CODE = M.MAKER_CODE WHERE B.BUHIN_NO = @Value AND B.KAITEI_NO IN ")
                .AppendLine("(SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 WITH (NOLOCK, NOWAIT) WHERE BUHIN_NO = B.BUHIN_NO AND NOT LEFT(B.BUHIN_NO,7) = '-----ZZ' )")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of Rhac0530MakerNameVo)(sb.ToString, BuhinNo)

        End Function

        ''' <summary>
        ''' 最終改訂の部品構成情報を返す
        ''' </summary>
        ''' <param name="BuhinNoKo">部品番号（子）</param>
        ''' <returns>最終改訂の部品構成情報</returns>
        ''' <remarks></remarks>
        Public Function FindLastestRhac0551ByBuhinNoKo(ByVal BuhinNoKo As String) As Rhac0551Vo Implements MakeStructureResultDao.FindLastestRhac0551ByBuhinNoKo
            Dim sql As String = _
            "SELECT * FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 P WITH (NOLOCK, NOWAIT) WHERE P.BUHIN_NO_KO = @Value " _
            & " AND P.KAITEI_NO = ( " _
            & "     SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & "         FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 WITH (NOLOCK, NOWAIT)   " _
            & "         WHERE BUHIN_NO_KO = P.BUHIN_NO_KO " _
            & "           AND BUHIN_NO_OYA = P.BUHIN_NO_OYA ) " _
            & "	    AND P.BUHIN_NO_OYA IN (SELECT MIN(BUHIN_NO_OYA) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 WITH (NOLOCK, NOWAIT) WHERE BUHIN_NO_KO = P.BUHIN_NO_KO ) "
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of Rhac0551Vo)(sql, BuhinNoKo)
        End Function

        ''' <summary>
        ''' 部品番号（親）を基点に構成を作り、子の部品情報の取引先情報を返す
        ''' </summary>
        ''' <param name="BuhinNoOya">部品番号（親）</param>
        ''' <returns>一連の構成情報に紐付く子の取引先情報</returns>
        ''' <remarks></remarks>
        Public Function FindStructure0551ByBuhinNoOyaAnd0610ForKo(ByVal BuhinNoOya As String) As List(Of Rhac0610Vo) Implements MakeStructureResultDao.FindStructure0551ByBuhinNoOyaAnd0610ForKo
            Dim sql As String = _
            SELECT_NEWEST_KOSEI_BY_BUHIN_NO_OYA_RHAC0551() _
            & "SELECT * FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0610 WITH (NOLOCK, NOWAIT) WHERE MAKER_CODE IN ( " _
            & "	SELECT B.MAKER_CODE " _
            & "	FROM RT K WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 B " _
            & "		ON K.BUHIN_NO_KO = B.BUHIN_NO " _
            & "		AND B.KAITEI_NO IN (SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 WITH (NOLOCK, NOWAIT) WHERE BUHIN_NO = B.BUHIN_NO AND NOT LEFT(B.BUHIN_NO,7) = '-----ZZ'  ) " _
            & "	)"

            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0610Vo)(sql, BuhinNoOya)
        End Function

        ''' <summary>
        ''' 部品情報をリストで取得する(RHAC0551)
        ''' </summary>
        ''' <param name="BFBuhinNo">BF部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinRhac0551LIST(ByVal BFBuhinNo As String) As List(Of Rhac0551Vo) Implements MakeStructureResultDao.FindByBuhinRhac0551LIST
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 R WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " R.BUHIN_NO_OYA = @BuhinNoOya " _
            & "     AND NOT (R.SHUKEI_CODE = ' ' AND R.SIA_SHUKEI_CODE =  ' ' ) " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 WITH (NOLOCK, NOWAIT)   " _
            & " WHERE BUHIN_NO_KO = R.BUHIN_NO_KO " _
            & " AND BUHIN_NO_OYA = R.BUHIN_NO_OYA ) "
           
            Dim db As New EBomDbClient
            Dim param As New Rhac0551Vo
            param.BuhinNoOya = BFBuhinNo

            Return db.QueryForList(Of Rhac0551Vo)(sql, param)
        End Function

#End Region

        ''' <summary>
        ''' 親の部品情報を返す(RHAC0532)
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>親の部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindLastestRhac0532ByBuhinNoOya(ByVal BuhinNo As String) As Rhac0532Vo Implements MakeStructureResultDao.FindLastestRhac0532ByBuhinNoOya
            Dim sql As String = "SELECT B.* FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 B WITH (NOLOCK, NOWAIT) WHERE B.BUHIN_NO = @Value AND B.KAITEI_NO IN " _
            & "(SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 WITH (NOLOCK, NOWAIT) WHERE BUHIN_NO = B.BUHIN_NO AND NOT LEFT(B.BUHIN_NO,7) = '-----ZZ'  )"
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of Rhac0532Vo)(sql, BuhinNo)
        End Function

        ''' <summary>
        ''' 親の部品情報を返す(RHAC0530)
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>親の部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindLastestRhac0530ByBuhinNoOya(ByVal BuhinNo As String) As Rhac0530Vo Implements MakeStructureResultDao.FindLastestRhac0530ByBuhinNoOya
            Dim sql As String = "SELECT B.* FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 B WITH (NOLOCK, NOWAIT) WHERE B.BUHIN_NO = @Value AND B.KAITEI_NO IN " _
            & "(SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 WITH (NOLOCK, NOWAIT) WHERE BUHIN_NO = B.BUHIN_NO AND NOT LEFT(B.BUHIN_NO,7) = '-----ZZ'  )"
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of Rhac0530Vo)(sql, BuhinNo)
        End Function

        ''' <summary>
        ''' 部品番号に該当する部品名称と取引先コードを取得する
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinMaker(ByVal BuhinNo As String) As THoyouBuhinEditVo Implements MakeStructureResultDao.FindByBuhinMaker
            '552, 553, 551, BuhinEditから検索する'
            Dim Result As New THoyouBuhinEditVo
            Dim db As New EBomDbClient

            '部品名称を取得する'
            Dim sql532 As String = _
            " SELECT * " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 R " _
            & " WHERE  " _
            & " R.BUHIN_NO = '" & BuhinNo & "' " _
            & " AND R.KAITEI_NO =  " _
            & "(SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 WITH (NOLOCK, NOWAIT) WHERE BUHIN_NO = R.BUHIN_NO AND NOT LEFT(R.BUHIN_NO,7) = '-----ZZ'  )"

            Dim a532Vo As New Rhac0532Vo
            a532Vo = db.QueryForObject(Of Rhac0532Vo)(sql532)

            If a532Vo Is Nothing Then
                Dim sql533 As String = _
                " SELECT * " _
                & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 R " _
                & " WHERE  " _
                & " R.BUHIN_NO = '" & BuhinNo & "' " _
                & " AND R.KAITEI_NO =  " _
                & "(SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 WITH (NOLOCK, NOWAIT) WHERE BUHIN_NO = R.BUHIN_NO AND NOT LEFT(R.BUHIN_NO,7) = '-----ZZ'  )"

                Dim a533Vo As New Rhac0533Vo
                a533Vo = db.QueryForObject(Of Rhac0533Vo)(sql533)

                If a533Vo Is Nothing Then
                    Dim sql530 As String = _
                    " SELECT * " _
                    & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 R " _
                    & " WHERE  " _
                    & " R.BUHIN_NO = '" & BuhinNo & "' " _
                    & " AND R.KAITEI_NO =  " _
                    & "(SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 WITH (NOLOCK, NOWAIT) WHERE BUHIN_NO = R.BUHIN_NO AND NOT LEFT(R.BUHIN_NO,7) = '-----ZZ'  )"


                    Dim a530Vo As New Rhac0530Vo
                    a530Vo = db.QueryForObject(Of Rhac0530Vo)(sql530)
                    If a530Vo Is Nothing Then
                        '無ければ部品編集情報'
                        Dim sqlEdit As String = _
                        " SELECT * " _
                        & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE " _
                        & " WHERE  " _
                        & " BE.BUHIN_NO = '" & BuhinNo & "' "

                        Dim EditVo As New THoyouBuhinEditVo
                        EditVo = db.QueryForObject(Of THoyouBuhinEditVo)(sqlEdit)

                        If EditVo Is Nothing Then
                            '存在しない'
                        Else
                            Result.BuhinName = EditVo.BuhinName
                            Result.MakerCode = EditVo.MakerCode
                        End If
                    Else
                        Result.BuhinName = a530Vo.BuhinName
                        Result.MakerCode = a530Vo.MakerCode
                    End If
                Else
                    Result.BuhinName = a533Vo.BuhinName
                End If
            Else
                Result.BuhinName = a532Vo.BuhinName
                Result.MakerCode = a532Vo.MakerCode
            End If

            '取引先コードがからなら取得にいく'
            If StringUtil.IsEmpty(Result.MakerCode) Then
                '取引先コードと取引先名称を取得'

                Dim sql As String = _
                " SELECT KA, " _
                & " TRCD " _
                & " FROM " & MBOM_DB_NAME & ".dbo.AS_KPSM10P " _
                & " WHERE " _
                & " BUBA_15 = @Buba15 " _
                & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                Dim NewBuhinNo As String = ""
                If StringUtil.Equals(Left(NewBuhinNo, 1), "-") Then
                    NewBuhinNo = Replace(BuhinNo, "-", " ")
                Else
                    NewBuhinNo = BuhinNo
                End If


                Dim paramK As New AsKPSM10PVo

                paramK.Buba15 = NewBuhinNo

                Dim aKPSM As New AsKPSM10PVo

                aKPSM = db.QueryForObject(Of AsKPSM10PVo)(sql, paramK)

                '存在チェックその１(３ヶ月インフォメーション)'
                If aKPSM Is Nothing Then
                    NewBuhinNo = ""
                    '無ければパーツプライリスト'
                    Dim sql2 As String = _
                    " SELECT KA, " _
                    & " TRCD " _
                    & " FROM " & MBOM_DB_NAME & ".dbo.AS_PARTSP " _
                    & " WHERE BUBA_13 = @Buba13 " _
                    & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                    Dim paramP As New AsPARTSPVo

                    If BuhinNo.Length < 13 Then
                        If StringUtil.Equals(Left(BuhinNo, 1), "-") Then
                            NewBuhinNo = Replace(BuhinNo, "-", " ")
                        Else
                            NewBuhinNo = BuhinNo
                        End If
                        paramP.Buba13 = NewBuhinNo
                    Else
                        If StringUtil.Equals(Left(BuhinNo, 1), "-") Then
                            NewBuhinNo = Left(Replace(BuhinNo, "-", " "), 13)
                        Else
                            NewBuhinNo = Left(BuhinNo, 13)
                        End If
                        paramP.Buba13 = NewBuhinNo
                    End If

                    Dim aPARTS As New AsPARTSPVo
                    aPARTS = db.QueryForObject(Of AsPARTSPVo)(sql2, paramP)

                    '存在チェックその２(パーツプライリスト)'
                    If aPARTS Is Nothing Then
                        NewBuhinNo = ""
                        '無ければ海外生産マスタ'
                        Dim sql3 As String = _
                        " SELECT KA, " _
                        & " TRCD " _
                        & " FROM " & MBOM_DB_NAME & ".dbo.AS_GKPSM10P " _
                        & " WHERE BUBA_15 = @Buba15 " _
                        & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                        Dim paramG As New AsGKPSM10PVo
                        If StringUtil.Equals(Left(BuhinNo, 1), "-") Then
                            NewBuhinNo = Replace(BuhinNo, "-", " ")
                        Else
                            NewBuhinNo = BuhinNo
                        End If
                        paramG.Buba15 = NewBuhinNo
                        Dim aGKPSM As New AsGKPSM10PVo
                        aGKPSM = db.QueryForObject(Of AsGKPSM10PVo)(sql3, paramG)

                        '存在チェックその３(海外生産マスタ) '
                        If aGKPSM Is Nothing Then
                            NewBuhinNo = ""
                            '無ければ部品マスタ'
                            If BuhinNo.Length < 10 Then
                                If StringUtil.Equals(Left(BuhinNo, 1), "-") Then
                                    NewBuhinNo = Replace(BuhinNo, "-", " ")
                                Else
                                    NewBuhinNo = BuhinNo
                                End If
                            Else
                                If StringUtil.Equals(Left(BuhinNo, 1), "-") Then
                                    NewBuhinNo = Left(Replace(BuhinNo, "-", " "), 10)
                                Else
                                    NewBuhinNo = Left(BuhinNo, 10)
                                End If
                            End If
                            Dim sql4 As String = _
                            " SELECT KOTAN, " _
                            & " MAKER " _
                            & " FROM " & MBOM_DB_NAME & ".dbo.AS_BUHIN01 " _
                            & " WHERE " _
                            & " GZZCP = @Gzzcp " _
                            & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                            Dim param4 As New AsBUHIN01Vo
                            param4.Gzzcp = NewBuhinNo

                            Dim aBuhin01 As New AsBUHIN01Vo
                            aBuhin01 = db.QueryForObject(Of AsBUHIN01Vo)(sql4, param4)

                            '存在チェックその４(部品マスタ)'
                            If aBuhin01 Is Nothing Then
                                '無ければ属性管理'
                                Dim sql5 As String = _
                                "SELECT " _
                                & " FHI_NOMINATE_KOTAN, " _
                                & " TORIHIKISAKI_CODE " _
                                & " FROM " _
                                & " " + EBOM_DB_NAME + ".dbo.T_VALUE_DEV " _
                                & " WHERE " _
                                & "  AN_LEVEL = '1' " _
                                & " AND BUHIN_NO = @BuhinNo " _
                                & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                                Dim aDev As New TValueDevVo
                                Dim paramT As New TValueDevVo
                                'paramT.KaihatsuFugo = KaihatsuFugo
                                If BuhinNo.Length < 10 Then
                                    paramT.BuhinNo = BuhinNo
                                Else
                                    paramT.BuhinNo = Left(BuhinNo, 10)
                                End If

                                aDev = db.QueryForObject(Of TValueDevVo)(sql5, paramT)

                                '存在チェックその５(属性管理(開発符号毎)) '
                                If aDev Is Nothing Then
                                    Result.MakerCode = ""
                                Else

                                    Result.MakerCode = aDev.TorihikisakiCode
                                End If

                            Else
                                Result.MakerCode = aBuhin01.Maker
                            End If
                        Else
                            Result.MakerCode = aGKPSM.Trcd
                        End If
                    Else
                        Result.MakerCode = aPARTS.Trcd
                    End If
                Else
                    Result.MakerCode = aKPSM.Trcd
                End If
            End If




            If Not StringUtil.IsEmpty(Result.MakerCode) Then
                Dim Msql As String = _
                " SELECT MAKER_NAME " _
                & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0610 " _
                & " WHERE " _
                & " MAKER_CODE = @MakerCode "

                Dim Mparam As New Rhac0610Vo
                Mparam.MakerCode = Result.MakerCode
                Dim MVo As New Rhac0610Vo
                MVo = db.QueryForObject(Of Rhac0610Vo)(Msql, Mparam)
                If MVo Is Nothing Then
                    Result.MakerName = ""
                Else
                    Result.MakerName = MVo.MakerName

                End If
            End If

            Return Result
        End Function

        ''' <summary>
        ''' 部品番号に該当する国内集計コード、海外SIA集計コードを取得する
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinShukei(ByVal BuhinNo As String) As THoyouBuhinEditVo Implements MakeStructureResultDao.FindByBuhinShukei
            '552, 553, 551, BuhinEditから検索する'
            Dim Result As New THoyouBuhinEditVo
            Dim db As New EBomDbClient

            '国内集計コード、海外SIA集計コードを取得する'
            Dim sql532 As String = _
            " SELECT * " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 R " _
            & " WHERE  " _
            & " R.BUHIN_NO = '" & BuhinNo & "' " _
            & " AND R.KAITEI_NO =  " _
            & "(SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 WITH (NOLOCK, NOWAIT) WHERE BUHIN_NO = R.BUHIN_NO AND NOT LEFT(R.BUHIN_NO,7) = '-----ZZ'  )"

            Dim a532Vo As New Rhac0532Vo
            a532Vo = db.QueryForObject(Of Rhac0532Vo)(sql532)

            If a532Vo Is Nothing Then
                Dim sql530 As String = _
                " SELECT * " _
                & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 R " _
                & " WHERE  " _
                & " R.BUHIN_NO = '" & BuhinNo & "' " _
                & " AND R.KAITEI_NO =  " _
                & "(SELECT MAX(KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 WITH (NOLOCK, NOWAIT) WHERE BUHIN_NO = R.BUHIN_NO AND NOT LEFT(R.BUHIN_NO,7) = '-----ZZ'  )"


                Dim a530Vo As New Rhac0530Vo
                a530Vo = db.QueryForObject(Of Rhac0530Vo)(sql530)
                If a530Vo Is Nothing Then
                    '無ければ部品編集情報'
                    Dim sqlEdit As String = _
                    " SELECT * " _
                    & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE " _
                    & " WHERE  " _
                    & " BE.BUHIN_NO = '" & BuhinNo & "' "

                    Dim EditVo As New THoyouBuhinEditVo
                    EditVo = db.QueryForObject(Of THoyouBuhinEditVo)(sqlEdit)

                    If EditVo Is Nothing Then
                        '存在しない'
                    Else
                        Result.ShukeiCode = EditVo.ShukeiCode
                        Result.SiaShukeiCode = EditVo.SiaShukeiCode
                    End If
                Else
                    Result.ShukeiCode = a530Vo.ShukeiCode
                    Result.SiaShukeiCode = a530Vo.SiaShukeiCode
                End If
            Else
                Result.ShukeiCode = a532Vo.ShukeiCode
                Result.SiaShukeiCode = a532Vo.SiaShukeiCode
            End If

            Return Result
        End Function

        ''' <summary>
        ''' 部品の色を取得する(0460)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinColor0532(ByVal shisakuEventCode As String, _
                                             ByVal shisakuBukaCode As String, _
                                             ByVal shisakuBlockNo As String, _
                                             ByVal instlHinban As String, _
                                             ByVal bfBuhinNo As String, _
                                             ByVal buhinNo As String) As String Implements MakeStructureResultDao.FindByBuhinColor0532
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT COLORM.FUKA_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA ")
                .AppendLine(" LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 BUHINM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BUHINM.BUHIN_NO = '" & buhinNo & "' ")
                .AppendLine(" And BUHINM.HAISI_DATE = 99999999  ")
                .AppendLine(" LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0790 BUIM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BUIM.ZUMEN_NO = BUHINM.ZUMEN_NO  ")
                .AppendLine(" And BUIM.KAIHATSU_FUGO = B.BASE_KAIHATSU_FUGO ")
                .AppendLine(" And BUIM.HAISI_DATE = 99999999  ")
                .AppendLine(" LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 COLORM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON COLORM.KAIHATSU_FUGO = B.BASE_KAIHATSU_FUGO  ")
                .AppendLine(" And COLORM.COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN  ")
                .AppendLine(" And COLORM.DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE  ")
                .AppendLine(" And COLORM.DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE  ")
                .AppendLine("		 AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE")
                .AppendLine(" AND (COLORM.COLOR_CODE =  B.BASE_GAISOUSYOKU  ")
                .AppendLine(" OR COLORM.COLOR_CODE =  B.BASE_NAISOUSYOKU)  ")
                .AppendLine(" And COLORM.DESIGN_GROUP_KAITEI_NO = (SELECT MAX(DESIGN_GROUP_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE KAIHATSU_FUGO = B.BASE_KAIHATSU_FUGO  ")
                .AppendLine(" And COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN  ")
                .AppendLine(" And DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE  ")
                .AppendLine("		 AND KYOTEN_CODE = BUIM.KYOTEN_CODE  ")
                .AppendLine(" And DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE  ")
                .AppendLine(" AND COLOR_CODE =  COLORM.COLOR_CODE ) ")
                .AppendLine(" WHERE ")
                .AppendLine(" SBI.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")
                .AppendLine(" AND SBI.SHISAKU_BUKA_CODE = '" & shisakuBukaCode & "' ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "' ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                .AppendLine("  SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WITH (NOLOCK, NOWAIT)   ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" AND SBI.INSTL_HINBAN = '" & instlHinban & "' ")
                .AppendLine(" AND SBI.BF_BUHIN_NO = '" & bfBuhinNo & "' ")
            End With
            Dim db As New EBomDbClient
            'ほんとは460だけど・・・'
            Dim result As New Rhac0430Vo
            result = db.QueryForObject(Of Rhac0430Vo)(sb.ToString)
            If result Is Nothing Then
                Return Nothing
            End If
            Return result.FukaNo
        End Function

        ''' <summary>
        ''' 部品の色を取得する(0460)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinColor0530(ByVal shisakuEventCode As String, _
                                             ByVal shisakuBukaCode As String, _
                                             ByVal shisakuBlockNo As String, _
                                             ByVal instlHinban As String, _
                                             ByVal bfBuhinNo As String, _
                                             ByVal buhinNo As String) As String Implements MakeStructureResultDao.FindByBuhinColor0530
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA ")
                .AppendLine(" LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 BUHINM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BUHINM.BUHIN_NO = '" & buhinNo & "' ")
                .AppendLine(" And BUHINM.HAISI_DATE = 99999999  ")
                .AppendLine(" LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0790 BUIM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BUIM.ZUMEN_NO = BUHINM.ZUMEN_NO  ")
                .AppendLine(" And BUIM.KAIHATSU_FUGO = B.BASE_KAIHATSU_FUGO ")
                .AppendLine(" And BUIM.HAISI_DATE = 99999999  ")
                .AppendLine(" LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 COLORM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON COLORM.KAIHATSU_FUGO = B.BASE_KAIHATSU_FUGO  ")
                .AppendLine(" And COLORM.COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN  ")
                .AppendLine(" 		 AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE ")
                .AppendLine(" And COLORM.DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE  ")
                .AppendLine(" And COLORM.DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE  ")
                .AppendLine(" AND (COLORM.COLOR_CODE =  B.BASE_GAISOUSYOKU  ")
                .AppendLine(" OR COLORM.COLOR_CODE =  B.BASE_NAISOUSYOKU)  ")
                .AppendLine(" And COLORM.DESIGN_GROUP_KAITEI_NO = (SELECT MAX(DESIGN_GROUP_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE KAIHATSU_FUGO = B.BASE_KAIHATSU_FUGO  ")
                .AppendLine(" And COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN  ")
                .AppendLine("		 AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE ")
                .AppendLine(" And DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE  ")
                .AppendLine(" And DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE  ")
                .AppendLine(" AND COLOR_CODE =  COLORM.COLOR_CODE ) ")
                .AppendLine(" WHERE ")
                .AppendLine(" SBI.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")
                .AppendLine(" AND SBI.SHISAKU_BUKA_CODE = '" & shisakuBukaCode & "' ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "' ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                .AppendLine("  SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WITH (NOLOCK, NOWAIT)   ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" AND SBI.INSTL_HINBAN = '" & instlHinban & "' ")
                .AppendLine(" AND SBI.BF_BUHIN_NO = '" & bfBuhinNo & "' ")
            End With
            Dim db As New EBomDbClient
            'ほんとは460だけど・・・'
            Dim result As New Rhac0430Vo
            result = db.QueryForObject(Of Rhac0430Vo)(sb.ToString)
            If result Is Nothing Then
                Return Nothing
            End If
            Return result.FukaNo
        End Function

        ''' <summary>
        ''' 部品の色を取得する(0460)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinColor0533(ByVal shisakuEventCode As String, _
                                             ByVal shisakuBukaCode As String, _
                                             ByVal shisakuBlockNo As String, _
                                             ByVal instlHinban As String, _
                                              ByVal bfBuhinNo As String, _
                                              ByVal kaihatsuFugo As String, _
                                             ByVal buhinNo As String) As String Implements MakeStructureResultDao.FindByBuhinColor0533
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA ")
                .AppendLine(" LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 BUHINM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BUHINM.BUHIN_NO = '" & buhinNo & "' ")
                .AppendLine(" And BUHINM.HAISI_DATE = 99999999  ")
                .AppendLine(" LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0790 BUIM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BUIM.ZUMEN_NO = BUHINM.ZUMEN_NO  ")
                .AppendLine(" And BUIM.KAIHATSU_FUGO = B.BASE_KAIHATSU_FUGO ")
                .AppendLine(" And BUIM.HAISI_DATE = 99999999  ")
                .AppendLine(" LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 COLORM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON COLORM.KAIHATSU_FUGO = B.BASE_KAIHATSU_FUGO  ")
                .AppendLine(" And COLORM.COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN  ")
                .AppendLine(" And COLORM.DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE  ")
                .AppendLine(" And COLORM.DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE  ")
                .AppendLine(" AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE ")
                .AppendLine(" AND (COLORM.COLOR_CODE =  B.BASE_GAISOUSYOKU  ")
                .AppendLine(" OR COLORM.COLOR_CODE =  B.BASE_NAISOUSYOKU)  ")
                .AppendLine(" And COLORM.DESIGN_GROUP_KAITEI_NO = (SELECT MAX(DESIGN_GROUP_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE KAIHATSU_FUGO = B.BASE_KAIHATSU_FUGO  ")
                .AppendLine(" And COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN  ")
                .AppendLine(" And DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE  ")
                .AppendLine(" And DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE  ")
                .AppendLine(" AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE ")
                .AppendLine(" AND COLOR_CODE =  COLORM.COLOR_CODE ) ")
                .AppendLine(" WHERE ")
                .AppendLine(" SBI.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")
                .AppendLine(" AND SBI.SHISAKU_BUKA_CODE = '" & shisakuBukaCode & "' ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "' ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                .AppendLine("  SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WITH (NOLOCK, NOWAIT)   ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" AND NOT SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                .AppendLine(" AND SBI.INSTL_HINBAN = '" & instlHinban & "' ")
                .AppendLine(" AND SBI.BF_BUHIN_NO = '" & bfBuhinNo & "' ")
            End With
            Dim db As New EBomDbClient
            'ほんとは460だけど・・・'
            Dim result As New Rhac0430Vo
            result = db.QueryForObject(Of Rhac0430Vo)(sb.ToString)
            If result Is Nothing Then
                Return Nothing
            End If
            Return result.FukaNo
        End Function

        ''' <summary>
        ''' 部品の色を取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinColor2220(ByVal shisakuEventCode As String, _
                                             ByVal shisakuBlockNo As String, _
                                             ByVal instlHinban As String, _
                                             ByVal bfBuhinNo As String, _
                                             ByVal buhinNo As String) As String Implements MakeStructureResultDao.FindByBuhinColor2220
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("   SELECT AL2.FUKA_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE BASE ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON SBI.SHISAKU_EVENT_CODE = BASE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SBI.INSTL_HINBAN = '" & instlHinban & "'")
                .AppendLine(" AND SBI.BF_BUHIN_NO = '" & bfBuhinNo & "'")
                .AppendLine(" AND SBI.SHISAKU_GOUSYA = BASE.SHISAKU_GOUSYA ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "'")
                .AppendLine(" AND NOT SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                .AppendLine(" INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 AL WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210)) ")
                .AppendLine(" ON AL.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine(" And AL.KATASHIKI_SCD_7 = BASE.BASE_KATASHIKI_SCD_7 ")
                .AppendLine(" And AL.SHIMUKECHI_CODE = BASE.BASE_SHIMUKE ")
                .AppendLine(" And AL.OP_CODE = BASE.BASE_OP ")
                .AppendLine(" AND AL.F_BUHIN_NO <>  '         0     ' ")
                .AppendLine(" AND LEFT(AL.BLOCK_NO,3) <>  '999' ")
                .AppendLine(" AND AL.BLOCK_NO = SBI.SHISAKU_BLOCK_NO")
                .AppendLine(" AND AL.F_BUHIN_NO = '" & buhinNo & "' ")
                .AppendLine(" AND AL.SHIYOSA_KAITEI_NO = (SELECT MAX(SHIYOSA_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210)) ")
                .AppendLine(" WHERE KAIHATSU_FUGO = Al.KAIHATSU_FUGO ")
                .AppendLine(" And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine(" And COL_NO = AL.COL_NO ")
                .AppendLine(" And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine(" AND SOBI_KAITEI_NO = AL.SOBI_KAITEI_NO ) ")
                .AppendLine(" AND AL.SOBI_KAITEI_NO = ")
                .AppendLine(" (SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210)) ")
                .AppendLine(" WHERE KAIHATSU_FUGO = Al.KAIHATSU_FUGO ")
                .AppendLine(" And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine(" And COL_NO = AL.COL_NO ")
                .AppendLine(" And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine(" AND SOBI_KAITEI_NO <= ( ")
                .AppendLine(" SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE KAIHATSU_FUGO = Al.KAIHATSU_FUGO ")
                .AppendLine(" And SHIYOSHO_SEQNO =  ")
                .AppendLine(" RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3) ")
                .AppendLine(" ) ")
                .AppendLine("  ) ")
                .AppendLine(" INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 SHIYO WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON SHIYO.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine(" And SHIYO.SHIYOSHO_SEQNO = ")
                .AppendLine(" RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3) ")
                .AppendLine(" AND AL.SOBI_KAITEI_NO = SHIYO.SOBI_KAITEI_NO ")
                .AppendLine(" INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 AL2 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON AL2.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine(" And AL2.KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine(" And AL2.COL_NO = AL.COL_NO ")
                .AppendLine(" And AL2.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine(" And AL2.F_BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine(" AND (AL2.COLOR_CODE = BASE.BASE_GAISOUSYOKU ")
                .AppendLine(" OR AL2.COLOR_CODE = BASE.BASE_NAISOUSYOKU) ")
                .AppendLine(" AND AL2.TOP_COLOR_KAITEI_NO = (SELECT MAX(TOP_COLOR_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE KAIHATSU_FUGO = AL2.KAIHATSU_FUGO ")
                .AppendLine(" AND KATASHIKI_SCD_7 = AL2.KATASHIKI_SCD_7 ")
                .AppendLine(" AND COL_NO = AL2.COL_NO ")
                .AppendLine(" AND BLOCK_NO = AL2.BLOCK_NO ")
                .AppendLine(" AND F_BUHIN_NO = AL2.F_BUHIN_NO ")
                .AppendLine(" AND COLOR_CODE = AL2.COLOR_CODE ) ")
                .AppendLine(" WHERE ")
                .AppendLine(" BASE.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "'")



            End With

            Dim db As New EBomDbClient
            'ほんとは2220だけど・・・'
            Dim result As New Rhac0430Vo
            result = db.QueryForObject(Of Rhac0430Vo)(sb.ToString)
            If result Is Nothing Then
                Return Nothing
            End If
            Return result.FukaNo

        End Function

        ''' <summary>
        ''' 部品の色を取得する(構成再展開)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinColor0532Kosei(ByVal shisakuEventCode As String, _
                                                  ByVal shisakuBukaCode As String, _
                                                  ByVal shisakuBlockNo As String, _
                                                  ByVal instlHinban As String, _
                                                  ByVal bfBuhinNo As String, _
                                                  ByVal buhinNo As String) As String Implements MakeStructureResultDao.FindByBuhinColor0532Kosei

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT E ")
                .AppendLine(" LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 BUHINM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BUHINM.BUHIN_NO = '" & buhinNo & "' ")
                .AppendLine(" And BUHINM.HAISI_DATE = 99999999  ")
                .AppendLine(" LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0790 BUIM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BUIM.ZUMEN_NO = BUHINM.ZUMEN_NO  ")
                .AppendLine(" And BUIM.KAIHATSU_FUGO = E.SHISAKU_KAIHATSU_FUGO ")
                .AppendLine(" And BUIM.HAISI_DATE = 99999999  ")
                .AppendLine(" LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 COLORM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON COLORM.KAIHATSU_FUGO = E.SHISAKU_KAIHATSU_FUGO  ")
                .AppendLine(" And COLORM.COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN  ")
                .AppendLine(" And COLORM.DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE  ")
                .AppendLine(" And COLORM.DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE  ")
                .AppendLine(" AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE ")
                .AppendLine(" And COLORM.DESIGN_GROUP_KAITEI_NO = (SELECT MAX(DESIGN_GROUP_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE KAIHATSU_FUGO = E.SHISAKU_KAIHATSU_FUGO  ")
                .AppendLine(" And COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN  ")
                .AppendLine(" And DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE  ")
                .AppendLine(" And DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE  ")
                .AppendLine(" AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE ")
                .AppendLine(" AND COLOR_CODE =  COLORM.COLOR_CODE ) ")
                .AppendLine(" WHERE ")
                .AppendLine(" E.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")

            End With
            Dim db As New EBomDbClient
            'ほんとは460だけど・・・'
            Dim result As New Rhac0430Vo
            result = db.QueryForObject(Of Rhac0430Vo)(sb.ToString)
            If result Is Nothing Then
                Return Nothing
            End If
            Return result.FukaNo
        End Function

        ''' <summary>
        ''' 部品の色を取得する(0553)構成再展開
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuBukaCode"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <param name="instlHinban"></param>
        ''' <param name="bfBuhinNo"></param>
        ''' <param name="buhinNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinColor0533Kosei(ByVal shisakuEventCode As String, _
                                           ByVal shisakuBukaCode As String, _
                                           ByVal shisakuBlockNo As String, _
                                           ByVal instlHinban As String, _
                                           ByVal bfBuhinNo As String, _
                                           ByVal kaihatsuFugo As String, _
                                           ByVal buhinNo As String) As String Implements MakeStructureResultDao.FindByBuhinColor0533Kosei
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA ")
                .AppendLine(" LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 BUHINM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BUHINM.BUHIN_NO = '" & buhinNo & "' ")
                .AppendLine(" ON BUHINM.KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                .AppendLine(" And BUHINM.HAISI_DATE = 99999999  ")
                .AppendLine(" LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0790 BUIM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BUIM.ZUMEN_NO = BUHINM.ZUMEN_NO  ")
                .AppendLine(" And BUIM.KAIHATSU_FUGO = B.BASE_KAIHATSU_FUGO ")
                .AppendLine(" And BUIM.HAISI_DATE = 99999999  ")
                .AppendLine(" LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 COLORM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON COLORM.KAIHATSU_FUGO = B.BASE_KAIHATSU_FUGO  ")
                .AppendLine(" And COLORM.COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN  ")
                .AppendLine(" And COLORM.DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE  ")
                .AppendLine(" And COLORM.DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE  ")
                .AppendLine(" AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE ")
                .AppendLine(" AND (COLORM.COLOR_CODE =  B.BASE_GAISOUSYOKU  ")
                .AppendLine(" OR COLORM.COLOR_CODE =  B.BASE_NAISOUSYOKU)  ")
                .AppendLine(" And COLORM.DESIGN_GROUP_KAITEI_NO = (SELECT MAX(DESIGN_GROUP_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE KAIHATSU_FUGO = B.BASE_KAIHATSU_FUGO  ")
                .AppendLine(" And COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN  ")
                .AppendLine(" And DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE  ")
                .AppendLine(" And DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE  ")
                .AppendLine(" AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE ")
                .AppendLine(" AND COLOR_CODE =  COLORM.COLOR_CODE ) ")
                .AppendLine(" WHERE ")
                .AppendLine(" SBI.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")
                .AppendLine(" AND SBI.SHISAKU_BUKA_CODE = '" & shisakuBukaCode & "' ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "' ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                .AppendLine("  SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WITH (NOLOCK, NOWAIT) WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" AND SBI.INSTL_HINBAN = '" & instlHinban & "' ")
                .AppendLine(" AND SBI.BF_BUHIN_NO = '" & bfBuhinNo & "' ")

            End With
            Dim db As New EBomDbClient
            'ほんとは460だけど・・・'
            Dim result As New Rhac0430Vo
            result = db.QueryForObject(Of Rhac0430Vo)(sb.ToString)
            If result Is Nothing Then
                Return Nothing
            End If
            Return result.FukaNo
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuBukaCode"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <param name="instlHinban"></param>
        ''' <param name="bfBuhinNo"></param>
        ''' <param name="buhinNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinColor0530Kosei(ByVal shisakuEventCode As String, _
                                           ByVal shisakuBukaCode As String, _
                                           ByVal shisakuBlockNo As String, _
                                           ByVal instlHinban As String, _
                                           ByVal bfBuhinNo As String, _
                                           ByVal buhinNo As String) As String Implements MakeStructureResultDao.FindByBuhinColor0530Kosei

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT E ")
                .AppendLine(" LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 BUHINM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BUHINM.BUHIN_NO = '" & buhinNo & "' ")
                .AppendLine(" And BUHINM.HAISI_DATE = 99999999  ")
                .AppendLine(" LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0790 BUIM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BUIM.ZUMEN_NO = BUHINM.ZUMEN_NO  ")
                .AppendLine(" And BUIM.KAIHATSU_FUGO = E.SHISAKU_KAIHATSU_FUGO ")
                .AppendLine(" And BUIM.HAISI_DATE = 99999999  ")
                .AppendLine(" LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 COLORM WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON COLORM.KAIHATSU_FUGO = E.SHISAKU_KAIHATSU_FUGO  ")
                .AppendLine(" And COLORM.COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN  ")
                .AppendLine(" And COLORM.DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE  ")
                .AppendLine(" And COLORM.DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE  ")
                .AppendLine(" AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE ")
                .AppendLine(" And COLORM.DESIGN_GROUP_KAITEI_NO = (SELECT MAX(DESIGN_GROUP_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE KAIHATSU_FUGO = B.BASE_KAIHATSU_FUGO  ")
                .AppendLine(" And COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN  ")
                .AppendLine(" And DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE  ")
                .AppendLine(" And DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE  ")
                .AppendLine(" AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE )")
                .AppendLine(" WHERE ")
                .AppendLine(" E.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")

            End With
            Dim db As New EBomDbClient
            'ほんとは460だけど・・・'
            Dim result As New Rhac0430Vo
            result = db.QueryForObject(Of Rhac0430Vo)(sb.ToString)
            If result Is Nothing Then
                Return Nothing
            End If
            Return result.FukaNo

        End Function

        ''' <summary>
        ''' イベントコード以外の設計ブロックINSTLを取得する
        ''' </summary>
        ''' <param name="yosanEventCode"></param>
        ''' <param name="unitKbn"></param>
        ''' <param name="patternName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindbySekkeiBlockInstlStructure(ByVal yosanEventCode As String, _
                                                        ByVal unitKbn As String, _
                                                        ByVal patternName As String) As List(Of TYosanBuhinEditPatternVo) Implements MakeStructureResultDao.FindbySekkeiBlockInstlStructure
            Dim sb As New StringBuilder
            With sb
                .AppendLine(" SELECT DISTINCT SBI.YOSAN_EVENT_CODE, ")
                .AppendLine(" SBI.BUHINHYO_NAME, ")
                .AppendLine(" SBI.PATTERN_NAME, ")
                .AppendLine(" SBI.UPDATED_DATE, ")
                .AppendLine(" SBI.UPDATED_TIME ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_BUHIN_EDIT_PATTERN SBI ")
                .AppendLine(" WHERE ")
                .AppendLine(" SBI.YOSAN_EVENT_CODE <> @YosanEventCode ")
                .AppendLine(" AND SBI.BUHINHYO_NAME = @BuhinhyoName ")
                .AppendLine(" AND SBI.PATTERN_NAME = @PatternName ")
            End With

            Dim db As New EBomDbClient
            Dim param As New TYosanBuhinEditPatternVo
            param.YosanEventCode = yosanEventCode
            param.BuhinhyoName = unitKbn
            param.PatternName = patternName

            Return db.QueryForList(Of TYosanBuhinEditPatternVo)(sb.ToString, param)
        End Function

        ''' <summary>
        ''' イベントコード以外の設計ブロックINSTLを取得する
        ''' </summary>
        ''' <param name="yosanEventCode"></param>
        ''' <param name="unitKbn"></param>
        ''' <param name="patternName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindbySekkeiBlockInstlStructureEventCopy(ByVal yosanEventCode As String, _
                                                                 ByVal unitKbn As String, _
                                                                 ByVal patternName As String) As List(Of TYosanBuhinEditPatternVo) Implements MakeStructureResultDao.FindbySekkeiBlockInstlStructureEventCopy
            Dim sb As New StringBuilder
            With sb
                .AppendLine(" SELECT DISTINCT SBI.YOSAN_EVENT_CODE, ")
                .AppendLine(" SBI.BUHINHYO_NAME, ")
                .AppendLine(" SBI.PATTERN_NAME, ")
                .AppendLine(" SBI.UPDATED_DATE, ")
                .AppendLine(" SBI.UPDATED_TIME ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_BUHIN_EDIT_PATTERN SBI ")
                .AppendLine(" WHERE ")
                .AppendLine(" SBI.YOSAN_EVENT_CODE = @YosanEventCode ")
                .AppendLine(" AND SBI.BUHINHYO_NAME = @BuhinhyoName ")
                .AppendLine(" AND SBI.PATTERN_NAME = @PatternName ")
              End With

            Dim db As New EBomDbClient
            Dim param As New TYosanBuhinEditPatternVo
            param.YosanEventCode = yosanEventCode
            param.BuhinhyoName = unitKbn
            param.PatternName = patternName

            Return db.QueryForList(Of TYosanBuhinEditPatternVo)(sb.ToString, param)
        End Function

    End Class
End Namespace