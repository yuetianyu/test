Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon
Imports System.Text

Namespace ShisakuBuhinEdit.Ikkatsu.Dao
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditIkkatsuDaoImpl
        Implements BuhinEditIkkatsuDao
        ' このクラスを元にSourceSelectorDaoImpleを作成したのでこのクラスへ修正を加えた場合
        ' SourceSelectorDaoImpleも修正をする必要がある可能性がある。

        ''' <summary>
        ''' INSTL品番に紐付く試作設計ブロックINSTL情報を取得する
        ''' </summary>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="shisakuEventCode">除外する試作イベントコード</param>
        ''' <param name="shisakuBukaCode">除外する試作部課コード</param>
        ''' <param name="shisakuBlockNo">除外する試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">除外する試作ブロックNo改訂No</param>
        ''' <returns>該当データ</returns>
        ''' <remarks>同一区分が複数該当する場合、最終更新日時のデータを取得</remarks>
        Public Function FindLatestInstlHinbanKbnBy(ByVal instlHinban As String, ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuSekkeiBlockInstlVo) Implements BuhinEditIkkatsuDao.FindLatestInstlHinbanKbnBy

            ' INSTL品番で抽出する
            ' 引数のブロック情報キーに一致する場合は除外する
            ' 同一区分は、最終更新日時の区分を有効にする
            Dim sql As String = _
                "SELECT I.* " _
                & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL I WITH (NOLOCK, NOWAIT) " _
                & "WHERE I.INSTL_HINBAN = @InstlHinban " _
                & "    AND (I.UPDATED_DATE + I.UPDATED_TIME) = (" _
                & "        SELECT MAX(UPDATED_DATE + UPDATED_TIME) " _
                & "        FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL " _
                & "        WHERE INSTL_HINBAN = I.INSTL_HINBAN " _
                & "            AND INSTL_HINBAN_KBN = I.INSTL_HINBAN_KBN " _
                & "            AND NOT (SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & "                AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
                & "                AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
                & "                AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo)" _
                & "    ) " _
                & "ORDER BY I.INSTL_HINBAN_KBN"

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
            param.InstlHinban = instlHinban

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品番号に紐付く試作部品編集情報を取得する
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="shisakuEventCode">除外する試作イベントコード</param>
        ''' <param name="shisakuBukaCode">除外する試作部課コード</param>
        ''' <param name="shisakuBlockNo">除外する試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">除外する試作ブロックNo改訂No</param>
        ''' <returns>該当データ</returns>
        ''' <remarks>同一の部品番号が複数該当する場合、最終更新日時のデータを取得</remarks>
        Public Function FindLatestShisakuBuhinNoBy(ByVal buhinNo As String, ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As TShisakuBuhinEditVo

            ' 部品番号で抽出する
            ' 引数のブロック情報キーに一致する場合は除外する
            ' 同一部品番号は、最終更新日時の部品番号を有効にする
            Dim sql As String = _
                "SELECT E.* " _
                & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT E WITH (NOLOCK, NOWAIT) " _
                & "WHERE E.BUHIN_NO = @BuhinNo " _
                & "    AND E.BUHIN_NO_KBN = '' " _
                & "    AND (E.UPDATED_DATE + E.UPDATED_TIME) = (" _
                & "        SELECT MAX(UPDATED_DATE + UPDATED_TIME) " _
                & "        FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT " _
                & "        WHERE BUHIN_NO = E.BUHIN_NO " _
                & "            AND BUHIN_NO_KBN = E.BUHIN_NO_KBN " _
                & "            AND NOT (SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & "                AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
                & "                AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
                & "                AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo)" _
                & "    )"

            Dim param As New TShisakuBuhinEditVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
            param.BuhinNo = buhinNo

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuBuhinEditVo)(sql, param)
        End Function
        ''' <summary>
        ''' 試作イベント名を返す
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindShisakuEventName(ByVal shisakuEventCode As String) As TShisakuEventVo Implements BuhinEditIkkatsuDao.FindShisakuEventName
            Dim sql As String = _
                "SELECT SHISAKU_EVENT_CODE,SHISAKU_KAIHATSU_FUGO,SHISAKU_EVENT_NAME FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT " _
                & "WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim param As New TShisakuEventVo
            param.ShisakuEventCode = shisakuEventCode

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventVo)(sql, param)

        End Function
        Public Function FindKaihatsuFugoOf553ByInstlHinban(ByVal instlHinban As String) As List(Of TShisakuEventVo) Implements BuhinEditIkkatsuDao.FindKaihatsuFugoOf553ByInstlHinban

            Dim sql As String = _
                " SELECT KAIHATSU_FUGO AS SHISAKU_KAIHATSU_FUGO " _
                & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 R WITH (NOLOCK, NOWAIT) " _
                & " WHERE " _
                & "     R.BUHIN_NO_OYA = 'T' " _
                & " AND R.BUHIN_NO_KO = '" & instlHinban & "' " _
                & "	    AND R.HAISI_DATE = 99999999  " _
                & " AND R.KAITEI_NO = ( " _
                & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
                & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553  WITH (NOLOCK, NOWAIT)  " _
                & " WHERE KAIHATSU_FUGO = R.KAIHATSU_FUGO " _
                & " AND BUHIN_NO_OYA = R.BUHIN_NO_OYA  " _
                & " AND BUHIN_NO_KO = R.BUHIN_NO_KO ) "
            '& "     AND NOT (R.SHUKEI_CODE = ' ' AND R.SIA_SHUKEI_CODE =  ' ' ) " _

            Dim db As New EBomDbClient

            Return db.QueryForList(Of TShisakuEventVo)(sql)

        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="instlHinban"></param>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuBukaCode"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <param name="shisakuBlockNoKaiteiNo"></param>
        ''' <param name="instlHinbanKbn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindShisakuEventByInstlHinbanAndKbn(ByVal instlHinban As String, ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal instlHinbanKbn As String) As List(Of TShisakuSekkeiBlockInstlVo) Implements BuhinEditIkkatsuDao.FindShisakuEventByInstlHinbanAndKbn
            Dim sql As New StringBuilder()

            'With sql
            '    .Remove(0, .Length)
            '    .AppendLine(" SELECT ")
            '    .AppendLine(" I.SHISAKU_EVENT_CODE, ")
            '    .AppendLine(" I.INSTL_HINBAN, ")
            '    .AppendLine(" I.INSTL_HINBAN_KBN   ")
            '    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL I WITH (NOLOCK, NOWAIT) ")
            '    .AppendLine(" WHERE I.SHISAKU_EVENT_CODE <> @ShisakuEventCode ")
            '    .AppendLine(" AND I.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
            '    .AppendLine(" AND I.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
            '    .AppendLine(" AND I.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
            '    .AppendLine(" AND I.INSTL_HINBAN = @InstlHinban ")
            '    .AppendLine(" AND I.INSTL_HINBAN_KBN = @InstlHinbanKbn ")
            '    .AppendLine(" GROUP BY I.SHISAKU_EVENT_CODE,I.INSTL_HINBAN,I.INSTL_HINBAN_KBN ")
            '    .AppendLine(" ORDER BY I.SHISAKU_EVENT_CODE ")
            'End With
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine(" I.SHISAKU_EVENT_CODE, ")
                .AppendLine(" I.SHISAKU_BUKA_CODE, ")
                .AppendLine(" I.SHISAKU_BLOCK_NO, ")
                .AppendLine(" I.INSTL_HINBAN, ")
                .AppendLine(" I.INSTL_HINBAN_KBN   ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL I WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE I.SHISAKU_EVENT_CODE <> @ShisakuEventCode ")
                .AppendLine(" AND I.INSTL_HINBAN = @InstlHinban ")
                .AppendLine(" AND I.INSTL_HINBAN_KBN = @InstlHinbanKbn ")
                .AppendLine(" AND (I.UPDATED_DATE + I.UPDATED_TIME) = (")
                .AppendLine("        SELECT MAX(UPDATED_DATE + UPDATED_TIME) ")
                .AppendLine("         FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                .AppendLine("         WHERE SHISAKU_EVENT_CODE = I.SHISAKU_EVENT_CODE ")
                .AppendLine("         AND INSTL_HINBAN = I.INSTL_HINBAN ")
                .AppendLine("         AND INSTL_HINBAN_KBN = I.INSTL_HINBAN_KBN ")
                .AppendLine("     ) ")
                .AppendLine(" GROUP BY I.SHISAKU_EVENT_CODE, I.SHISAKU_BUKA_CODE, I.SHISAKU_BLOCK_NO, I.INSTL_HINBAN,I.INSTL_HINBAN_KBN ")
                .AppendLine(" ORDER BY I.SHISAKU_EVENT_CODE ")
            End With

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
            param.InstlHinban = instlHinban
            param.InstlHinbanKbn = instlHinbanKbn

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sql.ToString, param)

        End Function

        ''' <summary>
        ''' イベントコード以外のINSTL品番を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindBySekkeiBlockInstlKbn(ByVal shisakuEventCode As String, ByVal instlHinban As String) As List(Of TShisakuSekkeiBlockInstlVo) Implements BuhinEditIkkatsuDao.FindBySekkeiBlockInstlKbn
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT DISTINCT SBI.SHISAKU_EVENT_CODE, ")
                .AppendLine(" SBI.SHISAKU_BUKA_CODE, SBI.SHISAKU_BLOCK_NO, ")
                .AppendLine(" SBI.SHISAKU_BLOCK_NO_KAITEI_NO, SBI.INSTL_HINBAN_KBN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ")
                .AppendLine(" WHERE ")
                .AppendLine(" SBI.SHISAKU_EVENT_CODE <> @ShisakuEventCode ")
                .AppendLine(" AND SBI.INSTL_HINBAN = @InstlHinban ")
                .AppendLine("AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" AND NOT SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                .AppendLine(" AND NOT SBI.INSTL_HINBAN_KBN = '' ")
            End With

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.InstlHinban = instlHinban

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sb.ToString, param)
        End Function

    End Class
End Namespace