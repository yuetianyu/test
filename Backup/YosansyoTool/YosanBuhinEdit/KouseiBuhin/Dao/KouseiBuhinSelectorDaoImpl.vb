Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon
Imports System.Text
Imports YosansyoTool.YosanBuhinEdit.KouseiBuhin.Dao.Vo
Imports EventSakusei

Namespace YosanBuhinEdit.KouseiBuhin.Dao
    Public Class KouseiBuhinSelectorDaoImpl : Implements KouseiBuhinSelectorDao

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' EBOMデータに構成が存在するかどうか取得する
        ''' </summary>
        ''' <param name="ShisakuEventCode"></param>
        ''' <returns>True:存在する False:存在しない</returns>
        ''' <remarks></remarks>
        Function FindByDataEbom(ByVal ShisakuEventCode As String) As Boolean Implements KouseiBuhinSelectorDao.FindByDataEbom
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

        ''' <summary>
        ''' 試作側にイベントが存在する場合そのイベントコードを取得する
        ''' </summary>
        ''' <param name="ShisakuEventCode"></param>
        ''' <returns>イベントコードのリスト</returns>
        ''' <remarks></remarks>
        Function FindByDataShisaku(ByVal ShisakuEventCode As String) As List(Of String) Implements KouseiBuhinSelectorDao.FindByDataShisaku
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

        ''' <summary>
        ''' システム大区分を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>システム大区分のリスト</returns>
        ''' <remarks></remarks>
        Function GetBySystemDaiKbnDataEbom(ByVal KaihatsuFugo As String) As List(Of SystemKbnVo) Implements KouseiBuhinSelectorDao.GetBySystemDaiKbnDataEbom
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    T117.SYSTEM_DAIKBN_ID AS ID_FIELD,  ")
                .AppendLine("    T117.SYSTEM_DAIKBN_NAME AS VALUE_FIELD ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC1170 AS T117 ")
                .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1180 AS T118 ON T117.SYSTEM_DAIKBN_ID=T118.SYSTEM_DAIKBN_ID ")
                .AppendLine("WHERE ")
                .AppendLine("    T118.KAIHATSU_FUGO = '" & KaihatsuFugo & "' ")
                .AppendLine("ORDER BY ")
                .AppendLine("   T117.SYSTEM_DAIKBN_ID ASC ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of SystemKbnVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' システム区分を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>システム区分のリスト</returns>
        ''' <remarks></remarks>
        Function GetBySystemKbnDataEbom(ByVal KaihatsuFugo As String) As List(Of SystemKbnVo) Implements KouseiBuhinSelectorDao.GetBySystemKbnDataEbom
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    T114.SYSTEM_KBN_ID AS ID_FIELD, ")
                .AppendLine("    T113.SYSTEM_KBN_NAME AS VALUE_FIELD ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC1140 AS T114 ")
                .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1130 AS T113 ON T114.SYSTEM_KBN_ID=T113.SYSTEM_KBN_ID ")
                .AppendLine("WHERE ")
                .AppendLine("    T114.KAIHATSU_FUGO = '" & KaihatsuFugo & "' ")
                .AppendLine("ORDER BY ")
                .AppendLine("   T114.SYSTEM_KBN_ID ASC ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of SystemKbnVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' ブロック№を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>ブロック何のリスト</returns>
        ''' <remarks></remarks>
        Public Function GetByBlockDataEbom(ByVal kaihatsuFugo As String) As List(Of BlockListVo) Implements KouseiBuhinSelectorDao.GetByBlockDataEbom
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    T004.BLOCK_NO AS BLOCK_NO, ")
                .AppendLine("    T004.BLOCK_NAME AS BLOCK_NAME ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0080 AS T008 ")
                .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 AS T004 ON T008.BLOCK_NO_KINO = T004.BLOCK_NO ")
                .AppendLine("WHERE ")
                .AppendLine("    T008.KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                .AppendLine("AND T008.HAISI_DATE = 99999999 ")
                .AppendLine("GROUP BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
                .AppendLine("ORDER BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of BlockListVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' システム大区分に紐づくブロック№を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="selectKbn">選択したシステム区分</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetBySystemDaiKbnToBlockEbom(ByVal kaihatsuFugo As String, ByVal selectKbn As List(Of SystemKbnVo)) As List(Of BlockListVo) Implements KouseiBuhinSelectorDao.GetBySystemDaiKbnToBlockEbom
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("    T119.BLOCK_NO_KINO AS BLOCK_NO ")
                .AppendLine("FROM ")
                .AppendLine("    RHAC1190 AS T119 ")
                .AppendLine("    LEFT JOIN RHAC0080 AS T008 ON T119.BLOCK_NO_KINO=T008.BLOCK_NO_KINO ")
                .AppendLine("WHERE ")
                .AppendLine("    T119.KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                .AppendLine("AND T119.SYSTEM_DAIKBN_ID IN (''")
                For Each SystemVo In selectKbn
                    .AppendLine(",").Append("'").Append(SystemVo.IdField).Append("'")
                Next
                .AppendLine("    ) ")
                .AppendLine("AND T008.HAISI_DATE = 99999999 ")
                .AppendLine("GROUP BY ")
                .AppendLine("    T119.BLOCK_NO_KINO ")
                .AppendLine("ORDER BY ")
                .AppendLine("    T119.BLOCK_NO_KINO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of BlockListVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' システム区分に紐づくブロック№を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="selectKbn">選択したシステム区分</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetBySystemKbnToBlockEbom(ByVal kaihatsuFugo As String, ByVal selectKbn As List(Of SystemKbnVo)) As List(Of BlockListVo) Implements KouseiBuhinSelectorDao.GetBySystemKbnToBlockEbom
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("    T119.BLOCK_NO_KINO AS BLOCK_NO ")
                .AppendLine("FROM ")
                .AppendLine("    RHAC1190 AS T119 ")
                .AppendLine("    LEFT JOIN RHAC0080 AS T008 ON T119.BLOCK_NO_KINO=T008.BLOCK_NO_KINO ")
                .AppendLine("WHERE ")
                .AppendLine("    T119.KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                .AppendLine("AND T119.SYSTEM_KBN_ID IN (''")
                For Each SystemVo In selectKbn
                    .AppendLine(",").Append("'").Append(SystemVo.IdField).Append("'")
                Next
                .AppendLine("    ) ")
                .AppendLine("AND T008.HAISI_DATE = 99999999 ")
                .AppendLine("GROUP BY ")
                .AppendLine("    T119.BLOCK_NO_KINO ")
                .AppendLine("ORDER BY ")
                .AppendLine("    T119.BLOCK_NO_KINO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of BlockListVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' ブロックを取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="blockNo">ブロック№</param>
        ''' <returns>ブロック何のリスト</returns>
        ''' <remarks></remarks>
        Public Function FindByBlockData(ByVal kaihatsuFugo As String, ByVal blockNo As String) As List(Of BlockListVo) Implements KouseiBuhinSelectorDao.FindByBlockData
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    T004.BLOCK_NO AS BLOCK_NO, ")
                .AppendLine("    T004.BLOCK_NAME AS BLOCK_NAME ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0080 AS T008 ")
                .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 AS T004 ON T008.BLOCK_NO_KINO = T004.BLOCK_NO ")
                .AppendLine("WHERE ")
                .AppendLine("    T008.KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                .AppendLine("AND T008.BLOCK_NO_KINO = '" & blockNo & "' ")
                .AppendLine("AND T008.HAISI_DATE = 99999999 ")
                .AppendLine("GROUP BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
                .AppendLine("ORDER BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of BlockListVo)(sb.ToString)
        End Function

#Region "部品番号に該当する部品名称と取引先コードを取得する"
        ''' <summary>
        ''' 部品番号に該当する部品名称と取引先コードを取得する
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinMakerGencho(ByVal BuhinNo As String) As BuhinListVo Implements KouseiBuhinSelectorDao.FindByBuhinMaker
            ''552, 553, 551, BuhinEditから検索する
            Dim Result As New BuhinListVo
            Dim db As New EBomDbClient

            '部品名称を取得する
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
                        '無ければ部品編集情報
                        Dim sqlEdit As String = _
                        " SELECT * " _
                        & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE " _
                        & " WHERE  " _
                        & " BE.BUHIN_NO = '" & BuhinNo & "' "

                        Dim EditVo As New TShisakuBuhinEditVo
                        EditVo = db.QueryForObject(Of TShisakuBuhinEditVo)(sqlEdit)

                        If EditVo Is Nothing Then
                            '存在しない'
                        Else
                            Result.BuhinName = EditVo.BuhinName
                            Result.TorihikisakiCode = EditVo.MakerCode
                        End If
                    Else
                        Result.BuhinName = a530Vo.BuhinName
                        Result.TorihikisakiCode = a530Vo.MakerCode
                    End If
                Else
                    Result.BuhinName = a533Vo.BuhinName
                End If
            Else
                Result.BuhinName = a532Vo.BuhinName
                Result.TorihikisakiCode = a532Vo.MakerCode
            End If

            '取引先コードがからなら取得にいく
            If StringUtil.IsEmpty(Result.TorihikisakiCode) Then
                '取引先コードと取引先名称を取得
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

                '存在チェックその１(３ヶ月インフォメーション)
                If aKPSM Is Nothing Then
                    NewBuhinNo = ""
                    '無ければパーツプライリスト
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

                    '存在チェックその２(パーツプライリスト)
                    If aPARTS Is Nothing Then
                        NewBuhinNo = ""
                        '無ければ海外生産マスタ
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

                        '存在チェックその３(海外生産マスタ) 
                        If aGKPSM Is Nothing Then
                            NewBuhinNo = ""
                            '無ければ部品マスタ
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

                            '存在チェックその４(部品マスタ)
                            If aBuhin01 Is Nothing Then
                                '無ければ属性管理
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
                                If BuhinNo.Length < 10 Then
                                    paramT.BuhinNo = BuhinNo
                                Else
                                    paramT.BuhinNo = Left(BuhinNo, 10)
                                End If

                                aDev = db.QueryForObject(Of TValueDevVo)(sql5, paramT)

                                '存在チェックその５(属性管理(開発符号毎)) 
                                If aDev Is Nothing Then
                                    Result.TorihikisakiCode = ""
                                Else

                                    Result.TorihikisakiCode = aDev.TorihikisakiCode
                                End If

                            Else
                                Result.TorihikisakiCode = aBuhin01.Maker
                            End If
                        Else
                            Result.TorihikisakiCode = aGKPSM.Trcd
                        End If
                    Else
                        Result.TorihikisakiCode = aPARTS.Trcd
                    End If
                Else
                    Result.TorihikisakiCode = aKPSM.Trcd
                End If
            End If

            If Not StringUtil.IsEmpty(Result.TorihikisakiCode) Then
                Dim Msql As String = _
                " SELECT MAKER_NAME " _
                & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0610 " _
                & " WHERE " _
                & " MAKER_CODE = @MakerCode "

                Dim Mparam As New Rhac0610Vo
                Mparam.MakerCode = Result.TorihikisakiCode
                Dim MVo As New Rhac0610Vo
                MVo = db.QueryForObject(Of Rhac0610Vo)(Msql, Mparam)
                If MVo Is Nothing Then
                    Result.TorihikisakiName = ""
                Else
                    Result.TorihikisakiName = MVo.MakerName

                End If
            End If
            Return Result
        End Function
#End Region

        ''' <summary>
        ''' 開発車機能ブロックのデータ取得(RHAC0080)
        ''' </summary>
        ''' <param name="aKaihatsuFugo">開発符号</param>
        ''' <param name="aBlockNo">ブロック番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByKaiteiNoRhac0080(ByVal aKaihatsuFugo As String, ByVal aBlockNo As String) As List(Of Rhac0080Vo) Implements KouseiBuhinSelectorDao.GetByKaiteiNoRhac0080
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT * FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0080 A ")
                .AppendLine(" WHERE A.KAIHATSU_FUGO = '" & aKaihatsuFugo & "' ")
                .AppendLine("   AND A.BLOCK_NO_KINO = '" & aBlockNo & "' ")
                .AppendLine("   AND A.HAISI_DATE = 99999999 ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0080Vo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 部品(図面テーブル群)のデータ取得(RHAC0532)
        ''' </summary>
        ''' <param name="aBuhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByKaiteiNoRhac0532(ByVal aBuhinNo As String) As List(Of Rhac0532Vo) Implements KouseiBuhinSelectorDao.GetByKaiteiNoRhac0532
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT * FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 A ")
                .AppendLine(" WHERE A.BUHIN_NO = '" & aBuhinNo & "' ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0532Vo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 開発部品情報のデータ取得(RHAC0533)
        ''' </summary>
        ''' <param name="aBuhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByKaiteiNoRhac0533(ByVal aBuhinNo As String) As List(Of Rhac0533Vo) Implements KouseiBuhinSelectorDao.GetByKaiteiNoRhac0533
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT * FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 A ")
                .AppendLine(" WHERE A.BUHIN_NO = '" & aBuhinNo & "' ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0533Vo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 号車情報のデータ取得(T_SHISAKU_EVENT_BASE/T_SHISAKU_EVENT)
        ''' </summary>
        ''' <param name="aKaitatsuFugo">開発符号</param>
        ''' <param name="aShisakuEvent">試作イベントコードｏｒ名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByGousyaDataMbomFromC(ByVal aKaitatsuFugo As String, ByVal aShisakuEvent As String) As  _
                            List(Of GousyaListVo) Implements KouseiBuhinSelectorDao.GetByGousyaDataMbomFromC
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT A.SHISAKU_SYUBETU, A.SHISAKU_GOUSYA ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE A ")
                .AppendLine(" INNER JOIN ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT B ")
                .AppendLine(" ON A.SHISAKU_EVENT_CODE = B.SHISAKU_EVENT_CODE ")
                .AppendLine(" WHERE B.SHISAKU_KAIHATSU_FUGO = '" & aKaitatsuFugo & "' ")
                .AppendLine("   AND B.SHISAKU_EVENT_CODE = '" & aShisakuEvent & "' ")
                .AppendLine(" GROUP BY A.SHISAKU_SYUBETU, A.SHISAKU_GOUSYA ")
                .AppendLine(" ORDER BY A.SHISAKU_SYUBETU, A.SHISAKU_GOUSYA ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of GousyaListVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 号車情報のデータ取得(T_SHISAKU_EVENT_BASE/T_SHISAKU_EVENT)
        ''' </summary>
        ''' <param name="aKaitatsuFugo">開発符号</param>
        ''' <param name="aShisakuEvent">試作イベントコードｏｒ名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByGousyaDataMbomFromN(ByVal aKaitatsuFugo As String, ByVal aShisakuEvent As String) As  _
                            List(Of GousyaListVo) Implements KouseiBuhinSelectorDao.GetByGousyaDataMbomFromN
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT A.SHISAKU_SYUBETU, A.SHISAKU_GOUSYA ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE A ")
                .AppendLine(" INNER JOIN ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT B ")
                .AppendLine(" ON A.SHISAKU_EVENT_CODE = B.SHISAKU_EVENT_CODE ")
                .AppendLine(" WHERE B.SHISAKU_KAIHATSU_FUGO = '" & aKaitatsuFugo & "' ")
                .AppendLine("   AND B.SHISAKU_EVENT_NAME = '" & aShisakuEvent & "' ")
                .AppendLine(" GROUP BY A.SHISAKU_SYUBETU, A.SHISAKU_GOUSYA ")
                .AppendLine(" ORDER BY A.SHISAKU_SYUBETU, A.SHISAKU_GOUSYA ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of GousyaListVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' システム大区分に紐づくブロック№を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shisakuEvent">試作イベントコードｏｒ名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByBlockDataMbomFromC(ByVal kaihatsuFugo As String, ByVal shisakuEvent As String) As List(Of BlockListVo) _
                                    Implements KouseiBuhinSelectorDao.GetByBlockDataMbomFromC
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    T004.BLOCK_NO AS BLOCK_NO, ")
                .AppendLine("    T004.BLOCK_NAME AS BLOCK_NAME ")
                .AppendLine("FROM ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL AS INSTL ")
                .AppendLine("    INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT AS EVENT ON INSTL.SHISAKU_EVENT_CODE = EVENT.SHISAKU_EVENT_CODE ")
                .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 AS T004 ON INSTL.SHISAKU_BLOCK_NO = T004.BLOCK_NO ")
                .AppendLine("WHERE ")
                .AppendLine("    EVENT.SHISAKU_KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                .AppendLine("AND EVENT.SHISAKU_EVENT_CODE = '" & shisakuEvent & "' ")
                .AppendLine("GROUP BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
                .AppendLine("ORDER BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of BlockListVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' システム大区分に紐づくブロック№を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shisakuEvent">試作イベントコードｏｒ名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByBlockDataMbomFromN(ByVal kaihatsuFugo As String, ByVal shisakuEvent As String) As List(Of BlockListVo) _
                                    Implements KouseiBuhinSelectorDao.GetByBlockDataMbomFromN
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    T004.BLOCK_NO AS BLOCK_NO, ")
                .AppendLine("    T004.BLOCK_NAME AS BLOCK_NAME ")
                .AppendLine("FROM ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL AS INSTL ")
                .AppendLine("    INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT AS EVENT ON INSTL.SHISAKU_EVENT_CODE = EVENT.SHISAKU_EVENT_CODE ")
                .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 AS T004 ON INSTL.SHISAKU_BLOCK_NO = T004.BLOCK_NO ")
                .AppendLine("WHERE ")
                .AppendLine("    EVENT.SHISAKU_KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                .AppendLine("AND EVENT.SHISAKU_EVENT_CODE = '" & shisakuEvent & "' ")
                .AppendLine("GROUP BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
                .AppendLine("ORDER BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of BlockListVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 試作号車に紐づくブロック№を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shisakueventCode">試作イベントコード</param>
        ''' <param name="selectGousya">選択した号車</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 20140620 部品の検索結果と一致するブロック選択とする為、大幅変更
        ''' </remarks>
        Function GetByGousyaToBlockMbomFromC(ByVal kaihatsuFugo As String, _
                                             ByVal shisakuEventCode As String, _
                                             ByVal selectGousya As List(Of SystemKbnVo)) As List(Of BlockListVo) _
                                             Implements KouseiBuhinSelectorDao.GetByGousyaToBlockMbomFromC

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)

                .AppendLine(" SELECT ")
                .AppendLine(" 	BEI.SHISAKU_BLOCK_NO AS BLOCK_NO ")
                .AppendLine(" 	,R4.BLOCK_NAME ")
                .AppendLine(" FROM ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" INNER JOIN ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI  ")
                .AppendLine(" ON SBI.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SBI.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" LEFT JOIN ")
                .AppendLine(RHACLIBF_DB_NAME & ".DBO.RHAC0040 R4 ON R4.BLOCK_NO=BEI.SHISAKU_BLOCK_NO ")
                .AppendLine(" WHERE BEI.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO= ")
                .AppendLine(" ( ")
                .AppendLine(" SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" FROM mBOM.dbo.T_SHISAKU_BUHIN_EDIT_INSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE(SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE) ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                .AppendLine(" ) ")
                .AppendLine(" AND BEI.INSU_SURYO IS NOT NULL ")
                .AppendLine(" AND BEI.INSU_SURYO > 0 ")
                .AppendLine(" AND SBI.SHISAKU_GOUSYA IN (''")
                For Each SystemVo In selectGousya
                    .AppendLine(",").Append("'").Append(SystemVo.IdField).Append("'")
                Next
                .AppendLine(" ) ")
                .AppendLine(" GROUP BY BEI.SHISAKU_BLOCK_NO,R4.BLOCK_NAME ")
                .AppendLine(" ORDER BY BEI.SHISAKU_BLOCK_NO,R4.BLOCK_NAME ")

            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of BlockListVo)(sb.ToString)

        End Function

        ''' <summary>
        ''' 試作号車に紐づくブロック№を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shisakueventname">試作イベント名称</param>
        ''' <param name="selectGousya">選択した号車</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 20140620 イベント名称からの取得処理に付いては事実上不要となっている
        ''' </remarks>
        Function GetByGousyaToBlockMbomFromN(ByVal kaihatsuFugo As String, _
                                             ByVal shisakuEventName As String, _
                                             ByVal selectGousya As List(Of SystemKbnVo)) As List(Of BlockListVo) _
                                             Implements KouseiBuhinSelectorDao.GetByGousyaToBlockMbomFromN
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    T004.BLOCK_NO AS BLOCK_NO, ")
                .AppendLine("    T004.BLOCK_NAME AS BLOCK_NAME ")
                .AppendLine("FROM ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL AS INSTL ")
                .AppendLine("    INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT AS EVENT ON INSTL.SHISAKU_EVENT_CODE = EVENT.SHISAKU_EVENT_CODE ")
                .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 AS T004 ON INSTL.SHISAKU_BLOCK_NO = T004.BLOCK_NO ")
                .AppendLine("WHERE ")
                .AppendLine("    EVENT.SHISAKU_KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                .AppendLine("AND EVENT.SHISAKU_EVENT_NAME = '" & shisakuEventName & "' ")
                .AppendLine("AND INSTL.SHISAKU_GOUSYA IN (''")
                For Each SystemVo In selectGousya
                    .AppendLine(",").Append("'").Append(SystemVo.IdField).Append("'")
                Next
                .AppendLine("    ) ")
                .AppendLine(" GROUP BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
                .AppendLine(" ORDER BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of BlockListVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 開発符号、ブロック№に紐づく部品情報を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shisakueventCode">試作イベントコード</param>
        ''' <param name="selectBlock">選択したブロック№</param>
        ''' <param name="selectSystem">選択したシステム</param>
        ''' <param name="selectPhaseNo">選択したフェーズ№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByShisakuBuhinToSpreadFromC(ByVal kaihatsuFugo As String, _
                                                ByVal shisakuEventCode As String, _
                                                ByVal selectBlock As List(Of BlockListVo), _
                                                ByVal selectSystem As String, _
                                                ByVal selectPhaseNo As Integer) _
                                                As List(Of TehaichoMenu.Vo.TShisakuBuhinEditVoSekkeiHelper) _
                                                Implements KouseiBuhinSelectorDao.GetByShisakuBuhinToSpreadFromC

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)

                '   新試作手配システムのブロック情報から
                If StringUtil.Equals(selectSystem, HOYOU_SELECT_VER1) = False And StringUtil.Equals(selectSystem, HOYOU_SELECT_VER2) = False Then
                    .AppendLine("SELECT ")
                    .AppendLine("EDIT.[SHISAKU_EVENT_CODE] ")
                    .AppendLine(",EDIT.[SHISAKU_BUKA_CODE] ")
                    .AppendLine(",EDIT.[SHISAKU_BLOCK_NO] ")
                    .AppendLine(",EDIT.[SHISAKU_BLOCK_NO_KAITEI_NO] ")
                    .AppendLine(",EDIT.[BUHIN_NO_HYOUJI_JUN] ")
                    .AppendLine(",EDIT.[LEVEL] ")
                    .AppendLine(",EDIT.[SHUKEI_CODE] ")
                    .AppendLine(",EDIT.[SIA_SHUKEI_CODE] ")
                    .AppendLine(",EDIT.[GENCYO_CKD_KBN] ")
                    .AppendLine(",EDIT.[MAKER_CODE] ")
                    .AppendLine(",EDIT.[MAKER_NAME] ")
                    .AppendLine(",EDIT.[BUHIN_NO] ")
                    .AppendLine(",EDIT.[BUHIN_NO_KBN] ")
                    .AppendLine(",EDIT.[BUHIN_NO_KAITEI_NO] ")
                    .AppendLine(",EDIT.[EDA_BAN] ")
                    .AppendLine(",EDIT.[BUHIN_NAME] ")
                    .AppendLine(",EDIT.[SAISHIYOUFUKA] ")
                    .AppendLine(",EDIT.[SHUTUZU_YOTEI_DATE] ")
                    .AppendLine(",EDIT.[ZAISHITU_KIKAKU_1] ")
                    .AppendLine(",EDIT.[ZAISHITU_KIKAKU_2] ")
                    .AppendLine(",EDIT.[ZAISHITU_KIKAKU_3] ")
                    .AppendLine(",EDIT.[ZAISHITU_MEKKI] ")
                    .AppendLine(",EDIT.[SHISAKU_BANKO_SURYO] ")
                    .AppendLine(",EDIT.[SHISAKU_BANKO_SURYO_U] ")
                    .AppendLine(",EDIT.[SHISAKU_BUHIN_HI] ")
                    .AppendLine(",EDIT.[SHISAKU_KATA_HI] ")
                    .AppendLine(",EDIT.[BIKOU] ")
                    .AppendLine(",EDIT.[KAITEI_HANDAN_FLG] ")
                    .AppendLine(",EDIT.[SHISAKU_LIST_CODE] ")
                    .AppendLine(",EDIT.[KYOUKU_SECTION] ")
                    .AppendLine(",EDIT.[BUHIN_NOTE] ")
                    .AppendLine(",BEI.INSU_SURYO  ")

                    .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT AS EDIT ")
                    .AppendLine("    INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT AS EVENT ON EDIT.SHISAKU_EVENT_CODE = EVENT.SHISAKU_EVENT_CODE ")
                    .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 AS T004 ON EDIT.SHISAKU_BLOCK_NO = T004.BLOCK_NO ")

                    .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL AS BEI")
                    .AppendLine("ON EDIT.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                    .AppendLine("AND EDIT.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                    .AppendLine("AND EDIT.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                    .AppendLine("AND EDIT.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                    .AppendLine("AND EDIT.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN ")

                    .AppendLine("WHERE ")
                    .AppendLine("    EVENT.SHISAKU_KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                    .AppendLine("AND EVENT.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")
                    .AppendLine("AND EDIT.SHISAKU_BLOCK_NO_KAITEI_NO=   ")
                    .AppendLine(" ( ")
                    .AppendLine("   SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                    .AppendLine("   FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) ")
                    .AppendLine("   WHERE ")
                    .AppendLine("       SHISAKU_EVENT_CODE = EDIT.SHISAKU_EVENT_CODE ")
                    .AppendLine("       AND SHISAKU_BUKA_CODE=EDIT.SHISAKU_BUKA_CODE ")
                    .AppendLine("       AND SHISAKU_BLOCK_NO=EDIT.SHISAKU_BLOCK_NO ")
                    .AppendLine("   ) ")
                    .AppendLine("AND EDIT.SHISAKU_BLOCK_NO IN (''")
                    For Each BlockVo In selectBlock
                        .AppendLine(", '" & BlockVo.BlockNo & "'")
                    Next
                    .AppendLine(" ) ")
                    .AppendLine(" AND BEI.INSU_SURYO IS NOT NULL")
                    .AppendLine(" AND BEI.INSU_SURYO > 0")

                    .AppendLine("GROUP BY ")
                    .AppendLine("EDIT.[SHISAKU_EVENT_CODE] ")
                    .AppendLine(",EDIT.[SHISAKU_BUKA_CODE] ")
                    .AppendLine(",EDIT.[SHISAKU_BLOCK_NO] ")
                    .AppendLine(",EDIT.[SHISAKU_BLOCK_NO_KAITEI_NO] ")
                    .AppendLine(",EDIT.[BUHIN_NO_HYOUJI_JUN] ")
                    .AppendLine(",EDIT.[LEVEL] ")
                    .AppendLine(",EDIT.[SHUKEI_CODE] ")
                    .AppendLine(",EDIT.[SIA_SHUKEI_CODE] ")
                    .AppendLine(",EDIT.[GENCYO_CKD_KBN] ")
                    .AppendLine(",EDIT.[MAKER_CODE] ")
                    .AppendLine(",EDIT.[MAKER_NAME] ")
                    .AppendLine(",EDIT.[BUHIN_NO] ")
                    .AppendLine(",EDIT.[BUHIN_NO_KBN] ")
                    .AppendLine(",EDIT.[BUHIN_NO_KAITEI_NO] ")
                    .AppendLine(",EDIT.[EDA_BAN] ")
                    .AppendLine(",EDIT.[BUHIN_NAME] ")
                    .AppendLine(",EDIT.[SAISHIYOUFUKA] ")
                    .AppendLine(",EDIT.[SHUTUZU_YOTEI_DATE] ")
                    .AppendLine(",EDIT.[ZAISHITU_KIKAKU_1] ")
                    .AppendLine(",EDIT.[ZAISHITU_KIKAKU_2] ")
                    .AppendLine(",EDIT.[ZAISHITU_KIKAKU_3] ")
                    .AppendLine(",EDIT.[ZAISHITU_MEKKI] ")
                    .AppendLine(",EDIT.[SHISAKU_BANKO_SURYO] ")
                    .AppendLine(",EDIT.[SHISAKU_BANKO_SURYO_U] ")
                    .AppendLine(",EDIT.[SHISAKU_BUHIN_HI] ")
                    .AppendLine(",EDIT.[SHISAKU_KATA_HI] ")
                    .AppendLine(",EDIT.[BIKOU] ")
                    .AppendLine(",EDIT.[KAITEI_HANDAN_FLG] ")
                    .AppendLine(",EDIT.[SHISAKU_LIST_CODE] ")
                    .AppendLine(",EDIT.[KYOUKU_SECTION] ")
                    .AppendLine(",EDIT.[BUHIN_NOTE] ")
                    .AppendLine(",BEI.INSU_SURYO  ")

                    .AppendLine(" ORDER BY ")
                    .AppendLine("   EDIT.SHISAKU_BLOCK_NO ,EDIT.BUHIN_NO_HYOUJI_JUN ")
                ElseIf StringUtil.Equals(selectSystem, HOYOU_SELECT_VER1) = True Then
                    .AppendLine("SELECT ")
                    .AppendLine("EDIT.[GENCHO_EVENT_CODE] AS SHISAKU_EVENT_CODE ")
                    .AppendLine(",EDIT.[GENCHO_BUKA_CODE] AS SHISAKU_BUKA_CODE ")
                    .AppendLine(",EDIT.[GENCHO_BLOCK_NO] AS SHISAKU_BLOCK_NO ")
                    .AppendLine(",'VER1' AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                    .AppendLine(",EDIT.[BUHIN_NO_HYOUJI_JUN] ")
                    .AppendLine(",CONVERT(INT,EDIT.[LEVEL]) AS LEVEL ")
                    .AppendLine(",EDIT.[SHUKEI_CODE] ")
                    .AppendLine(",EDIT.[SIA_SHUKEI_CODE] ")
                    .AppendLine(",EDIT.[GENCYO_CKD_KBN] ")
                    .AppendLine(",EDIT.[MAKER_CODE] ")
                    .AppendLine(",EDIT.[MAKER_NAME] ")
                    .AppendLine(",EDIT.[BUHIN_NO] ")
                    .AppendLine(",EDIT.[BUHIN_NAME] ")
                    .AppendLine(",BEI.INSU_SURYO  ")

                    .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_GENCHO_BUHIN_EBOM AS EDIT ")
                    .AppendLine("    INNER JOIN " & MBOM_DB_NAME & ".dbo.T_GENCHO_EVENT AS EVENT ON EDIT.GENCHO_EVENT_CODE = EVENT.GENCHO_EVENT_CODE ")
                    .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 AS T004 ON EDIT.GENCHO_BLOCK_NO = T004.BLOCK_NO ")

                    .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_GENCHO_BUHIN_GOUSYA AS BEI")
                    .AppendLine("ON EDIT.GENCHO_EVENT_CODE = BEI.GENCHO_EVENT_CODE ")
                    .AppendLine("AND EDIT.GENCHO_BUKA_CODE = BEI.GENCHO_BUKA_CODE ")
                    .AppendLine("AND EDIT.GENCHO_BLOCK_NO = BEI.GENCHO_BLOCK_NO ")
                    .AppendLine("AND EDIT.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN ")

                    .AppendLine("WHERE ")
                    .AppendLine("    EVENT.GENCHO_KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                    .AppendLine("AND EVENT.GENCHO_EVENT_CODE = '" & shisakuEventCode & "' ")
                    .AppendLine("AND EDIT.GENCHO_BLOCK_NO IN (''")
                    For Each BlockVo In selectBlock
                        .AppendLine(", '" & BlockVo.BlockNo & "'")
                    Next
                    .AppendLine(" ) ")
                    .AppendLine(" AND BEI.INSU_SURYO IS NOT NULL")
                    .AppendLine(" AND BEI.INSU_SURYO > 0")
                    .AppendLine(" AND EDIT.LEVEL IS NOT NULL")
                    .AppendLine(" AND EDIT.LEVEL >= '0'")
                    .AppendLine(" AND EDIT.BUHIN_NO IS NOT NULL")

                    .AppendLine("GROUP BY ")
                    .AppendLine("EDIT.[GENCHO_EVENT_CODE] ")
                    .AppendLine(",EDIT.[GENCHO_BUKA_CODE] ")
                    .AppendLine(",EDIT.[GENCHO_BLOCK_NO] ")
                    .AppendLine(",EDIT.[BUHIN_NO_HYOUJI_JUN] ")
                    .AppendLine(",EDIT.[LEVEL] ")
                    .AppendLine(",EDIT.[SHUKEI_CODE] ")
                    .AppendLine(",EDIT.[SIA_SHUKEI_CODE] ")
                    .AppendLine(",EDIT.[GENCYO_CKD_KBN] ")
                    .AppendLine(",EDIT.[MAKER_CODE] ")
                    .AppendLine(",EDIT.[MAKER_NAME] ")
                    .AppendLine(",EDIT.[BUHIN_NO] ")
                    .AppendLine(",EDIT.[BUHIN_NAME] ")
                    .AppendLine(",BEI.INSU_SURYO  ")

                    .AppendLine(" ORDER BY ")
                    .AppendLine("   EDIT.GENCHO_BLOCK_NO ,EDIT.BUHIN_NO_HYOUJI_JUN ")
                ElseIf StringUtil.Equals(selectSystem, HOYOU_SELECT_VER2) = True Then
                    .AppendLine("SELECT ")
                    .AppendLine("EDIT.[GENCHO_EVENT_CODE] AS SHISAKU_EVENT_CODE ")
                    .AppendLine(",EDIT.[GENCHO_BUKA_CODE] AS SHISAKU_BUKA_CODE ")
                    .AppendLine(",EDIT.[GENCHO_BLOCK_NO] AS SHISAKU_BLOCK_NO ")
                    .AppendLine(",'VER1' AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                    .AppendLine(",EDIT.[BUHIN_NO_HYOUJI_JUN] ")
                    .AppendLine(",CASE WHEN EDIT.LEVEL IS NULL THEN 9 ELSE EDIT.LEVEL END AS LEVEL ")
                    .AppendLine(",EDIT.[SHUKEI_CODE] ")
                    .AppendLine(",EDIT.[SIA_SHUKEI_CODE] ")
                    .AppendLine(",EDIT.[GENCYO_CKD_KBN] ")
                    .AppendLine(",EDIT.[MAKER_CODE] ")
                    .AppendLine(",EDIT.[MAKER_NAME] ")
                    .AppendLine(",EDIT.[BUHIN_NO] ")
                    .AppendLine(",EDIT.[BUHIN_NAME] ")
                    .AppendLine(",0 AS INSU_SURYO ")

                    .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_FUNC_BUHIN_EBOM AS EDIT ")
                    .AppendLine("    INNER JOIN " & MBOM_DB_NAME & ".dbo.T_GENCHO_EVENT AS EVENT ON EDIT.GENCHO_EVENT_CODE = EVENT.GENCHO_EVENT_CODE ")
                    .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 AS T004 ON EDIT.GENCHO_BLOCK_NO = T004.BLOCK_NO ")

                    .AppendLine("WHERE ")
                    .AppendLine("    EVENT.GENCHO_KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                    .AppendLine("AND EVENT.GENCHO_EVENT_CODE = '" & shisakuEventCode & "' ")
                    .AppendLine("AND EDIT.PHASE_NO = " & selectPhaseNo)
                    .AppendLine("AND EDIT.HAISHI_DATE = 99999999 ")
                    .AppendLine("AND EDIT.GENCHO_BLOCK_NO IN (''")
                    For Each BlockVo In selectBlock
                        .AppendLine(", '" & BlockVo.BlockNo & "'")
                    Next
                    .AppendLine(" ) ")
                    .AppendLine(" AND EDIT.BUHIN_NO IS NOT NULL")

                    .AppendLine("GROUP BY ")
                    .AppendLine("EDIT.[GENCHO_EVENT_CODE] ")
                    .AppendLine(",EDIT.[GENCHO_BUKA_CODE] ")
                    .AppendLine(",EDIT.[GENCHO_BLOCK_NO] ")
                    .AppendLine(",EDIT.[BUHIN_NO_HYOUJI_JUN] ")
                    .AppendLine(",EDIT.[LEVEL] ")
                    .AppendLine(",EDIT.[SHUKEI_CODE] ")
                    .AppendLine(",EDIT.[SIA_SHUKEI_CODE] ")
                    .AppendLine(",EDIT.[GENCYO_CKD_KBN] ")
                    .AppendLine(",EDIT.[MAKER_CODE] ")
                    .AppendLine(",EDIT.[MAKER_NAME] ")
                    .AppendLine(",EDIT.[BUHIN_NO] ")
                    .AppendLine(",EDIT.[BUHIN_NAME] ")

                    .AppendLine(" ORDER BY ")
                    .AppendLine("   EDIT.GENCHO_BLOCK_NO ,EDIT.BUHIN_NO_HYOUJI_JUN ")
                End If

            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TehaichoMenu.Vo.TShisakuBuhinEditVoSekkeiHelper)(sb.ToString)
        End Function

        ''' <summary>
        ''' 開発符号、ブロック№に紐づく部品情報を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shisakueventName">試作イベント名称</param>
        ''' <param name="selectBlock">選択したブロック№</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 20140620日時点、イベント名称からの取得は行われなくなった為、事実上不要となっている
        ''' </remarks>
        Function GetByShisakuBuhinToSpreadFromN(ByVal kaihatsuFugo As String, _
                                           ByVal shisakuEventName As String, _
                                           ByVal selectBlock As List(Of BlockListVo) _
                                           ) As List(Of TehaichoMenu.Vo.TShisakuBuhinEditVoSekkeiHelper) _
                                           Implements KouseiBuhinSelectorDao.GetByShisakuBuhinToSpreadFromN

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)

                .AppendLine("SELECT ")
                .AppendLine("EDIT.[SHISAKU_EVENT_CODE] ")
                .AppendLine(",EDIT.[SHISAKU_BUKA_CODE] ")
                .AppendLine(",EDIT.[SHISAKU_BLOCK_NO] ")
                .AppendLine(",EDIT.[SHISAKU_BLOCK_NO_KAITEI_NO] ")
                .AppendLine(",EDIT.[BUHIN_NO_HYOUJI_JUN] ")
                .AppendLine(",EDIT.[LEVEL] ")
                .AppendLine(",EDIT.[SHUKEI_CODE] ")
                .AppendLine(",EDIT.[SIA_SHUKEI_CODE] ")
                .AppendLine(",EDIT.[GENCYO_CKD_KBN] ")
                .AppendLine(",EDIT.[MAKER_CODE] ")
                .AppendLine(",EDIT.[MAKER_NAME] ")
                .AppendLine(",EDIT.[BUHIN_NO] ")
                .AppendLine(",EDIT.[BUHIN_NO_KBN] ")
                .AppendLine(",EDIT.[BUHIN_NO_KAITEI_NO] ")
                .AppendLine(",EDIT.[EDA_BAN] ")
                .AppendLine(",EDIT.[BUHIN_NAME] ")
                .AppendLine(",EDIT.[SAISHIYOUFUKA] ")
                .AppendLine(",EDIT.[SHUTUZU_YOTEI_DATE] ")
                .AppendLine(",EDIT.[ZAISHITU_KIKAKU_1] ")
                .AppendLine(",EDIT.[ZAISHITU_KIKAKU_2] ")
                .AppendLine(",EDIT.[ZAISHITU_KIKAKU_3] ")
                .AppendLine(",EDIT.[ZAISHITU_MEKKI] ")
                .AppendLine(",EDIT.[SHISAKU_BANKO_SURYO] ")
                .AppendLine(",EDIT.[SHISAKU_BANKO_SURYO_U] ")
                .AppendLine(",EDIT.[SHISAKU_BUHIN_HI] ")
                .AppendLine(",EDIT.[SHISAKU_KATA_HI] ")
                .AppendLine(",EDIT.[BIKOU] ")
                .AppendLine(",EDIT.[KAITEI_HANDAN_FLG] ")
                .AppendLine(",EDIT.[SHISAKU_LIST_CODE] ")
                .AppendLine(",EDIT.[KYOUKU_SECTION] ")
                .AppendLine(",EDIT.[BUHIN_NOTE] ")
                .AppendLine(",BEI.INSU_SURYO  ")

                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT AS EDIT ")
                .AppendLine("    INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT AS EVENT ON EDIT.SHISAKU_EVENT_CODE = EVENT.SHISAKU_EVENT_CODE ")
                .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 AS T004 ON EDIT.SHISAKU_BLOCK_NO = T004.BLOCK_NO ")

                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL AS BEI")
                .AppendLine("ON EDIT.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                .AppendLine("AND EDIT.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                .AppendLine("AND EDIT.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                .AppendLine("AND EDIT.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("AND EDIT.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN ")

                .AppendLine("WHERE ")
                .AppendLine("    EVENT.SHISAKU_KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                .AppendLine("AND EVENT.SHISAKU_EVENT_NAME = '" & shisakuEventName & "' ")
                .AppendLine("AND EDIT.SHISAKU_BLOCK_NO_KAITEI_NO=   ")
                .AppendLine(" ( ")
                .AppendLine("   SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine("   FROM ")
                .AppendLine("       " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) ")
                .AppendLine("   WHERE ")
                .AppendLine("       SHISAKU_EVENT_CODE = EDIT.SHISAKU_EVENT_CODE ")
                .AppendLine("       AND SHISAKU_BUKA_CODE=EDIT.SHISAKU_BUKA_CODE ")
                .AppendLine("       AND SHISAKU_BLOCK_NO=EDIT.SHISAKU_BLOCK_NO ")
                .AppendLine("   ) ")
                .AppendLine("AND EDIT.SHISAKU_BLOCK_NO IN (''")
                For Each BlockVo In selectBlock
                    .AppendLine(", '" & BlockVo.BlockNo & "'")
                Next
                .AppendLine(" ) ")
                .AppendLine(" AND BEI.INSU_SURYO IS NOT NULL")
                .AppendLine(" AND BEI.INSU_SURYO > 0")

                .AppendLine("GROUP BY ")
                .AppendLine("EDIT.[SHISAKU_EVENT_CODE] ")
                .AppendLine(",EDIT.[SHISAKU_BUKA_CODE] ")
                .AppendLine(",EDIT.[SHISAKU_BLOCK_NO] ")
                .AppendLine(",EDIT.[SHISAKU_BLOCK_NO_KAITEI_NO] ")
                .AppendLine(",EDIT.[BUHIN_NO_HYOUJI_JUN] ")
                .AppendLine(",EDIT.[LEVEL] ")
                .AppendLine(",EDIT.[SHUKEI_CODE] ")
                .AppendLine(",EDIT.[SIA_SHUKEI_CODE] ")
                .AppendLine(",EDIT.[GENCYO_CKD_KBN] ")
                .AppendLine(",EDIT.[MAKER_CODE] ")
                .AppendLine(",EDIT.[MAKER_NAME] ")
                .AppendLine(",EDIT.[BUHIN_NO] ")
                .AppendLine(",EDIT.[BUHIN_NO_KBN] ")
                .AppendLine(",EDIT.[BUHIN_NO_KAITEI_NO] ")
                .AppendLine(",EDIT.[EDA_BAN] ")
                .AppendLine(",EDIT.[BUHIN_NAME] ")
                .AppendLine(",EDIT.[SAISHIYOUFUKA] ")
                .AppendLine(",EDIT.[SHUTUZU_YOTEI_DATE] ")
                .AppendLine(",EDIT.[ZAISHITU_KIKAKU_1] ")
                .AppendLine(",EDIT.[ZAISHITU_KIKAKU_2] ")
                .AppendLine(",EDIT.[ZAISHITU_KIKAKU_3] ")
                .AppendLine(",EDIT.[ZAISHITU_MEKKI] ")
                .AppendLine(",EDIT.[SHISAKU_BANKO_SURYO] ")
                .AppendLine(",EDIT.[SHISAKU_BANKO_SURYO_U] ")
                .AppendLine(",EDIT.[SHISAKU_BUHIN_HI] ")
                .AppendLine(",EDIT.[SHISAKU_KATA_HI] ")
                .AppendLine(",EDIT.[BIKOU] ")
                .AppendLine(",EDIT.[KAITEI_HANDAN_FLG] ")
                .AppendLine(",EDIT.[SHISAKU_LIST_CODE] ")
                .AppendLine(",EDIT.[KYOUKU_SECTION] ")
                .AppendLine(",EDIT.[BUHIN_NOTE] ")
                .AppendLine(",BEI.INSU_SURYO  ")

                .AppendLine(" ORDER BY ")
                .AppendLine("   EDIT.SHISAKU_BLOCK_NO ,EDIT.BUHIN_NO_HYOUJI_JUN ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TehaichoMenu.Vo.TShisakuBuhinEditVoSekkeiHelper)(sb.ToString)
        End Function

        ''' <summary>
        ''' </summary>
        ''' <param name="shisakueventCode">試作イベントコード</param>
        ''' <param name="selectBlock">選択したブロック№</param>
        ''' <param name="selectBase">選択した号車№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByShisakuBuhinToSpreadBase(ByVal shisakuEventCode As String, _
                                                ByVal selectBlock As List(Of BlockListVo), _
                                                ByVal selectBase As List(Of SystemKbnVo) _
                                                ) As List(Of TShisakuBuhinEditInstlVo) _
                                                Implements KouseiBuhinSelectorDao.GetByShisakuBuhinToSpreadBase
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT DISTINCT BEI.* ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI WITH (NOLOCK, NOWAIT) ")
                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ")
                .AppendLine("ON SBI.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                .AppendLine("AND SBI.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                .AppendLine("AND SBI.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                .AppendLine("AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN ")

                .AppendLine("WHERE BEI.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")
                .AppendLine("AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO= ")
                .AppendLine(" ( ")
                .AppendLine("   SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine("   FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("   WHERE SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                .AppendLine("     AND SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                .AppendLine("     AND SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                .AppendLine("   ) ")
                .AppendLine("AND BEI.SHISAKU_BLOCK_NO IN (''")
                For Each BlockVo In selectBlock
                    .AppendLine(", '" & BlockVo.BlockNo & "'")
                Next
                .AppendLine(" ) ")

                .AppendLine(" AND BEI.INSU_SURYO IS NOT NULL")
                .AppendLine(" AND BEI.INSU_SURYO > 0")

                '試作号車が選択されていたら実行
                If selectBase.Count > 0 Then
                    .AppendLine("AND SBI.SHISAKU_GOUSYA IN (''")
                    For Each BaseVo In selectBase
                        .AppendLine(",'" & BaseVo.IdField & "'")
                    Next
                    .AppendLine(" ) ")
                End If
                .AppendLine("ORDER BY BEI.SHISAKU_BLOCK_NO,")
                .AppendLine(" BEI.SHISAKU_BLOCK_NO_KAITEI_NO,")
                .AppendLine(" BEI.INSTL_HINBAN_HYOUJI_JUN")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditInstlVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' イベントコード、担当に紐づく完成車装備情報を取得する
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="sekkeika">設計課</param>
        ''' <param name="tantoKey">担当KEY</param>
        ''' <param name="tanto">担当</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByTHoyouSekkeiTantoSoubi(ByVal eventCode As String, _
                                             ByVal sekkeika As String, _
                                             ByVal tantoKey As String, _
                                             ByVal tanto As String) As List(Of THoyouSekkeiTantoSoubiVo) Implements KouseiBuhinSelectorDao.GetByTHoyouSekkeiTantoSoubi
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_HOYOU_SEKKEI_TANTO_SOUBI AS S ")
                .AppendLine("WHERE ")
                .AppendLine("    S.HOYOU_EVENT_CODE = '" & eventCode & "' ")
                .AppendLine("AND S.HOYOU_BUKA_CODE = '" & sekkeika & "' ")
                .AppendLine("AND S.HOYOU_TANTO_KEY = '" & tantoKey & "' ")
                .AppendLine("AND S.HOYOU_TANTO = '" & tanto & "' ")
                .AppendLine(" ORDER BY ")
                .AppendLine("   S.HOYOU_EVENT_CODE , S.HOYOU_BUKA_CODE , S.HOYOU_TANTO_KEY , S.HOYOU_SOUBI_HYOUJI_JUN ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of THoyouSekkeiTantoSoubiVo)(sb.ToString)
        End Function

    End Class
End Namespace