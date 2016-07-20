Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon

Namespace ShisakuBuhinEdit.Al.Dao
    ''' <summary>
    ''' A/L画面用のDao実装クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HoyouBuhinBuhinEditAlDaoImpl : Inherits DaoEachFeatureImpl
        Implements HoyouBuhinBuhinEditAlDao

        '''' <summary>
        '''' ベース車情報・完成車情報を参照する
        '''' </summary>
        '''' <param name="shisakuEventCode">試作イベントコード</param>
        '''' <returns>ベース車情報・完成車情報</returns>
        '''' <remarks></remarks>
        'Public Function FindEventInfoById(ByVal shisakuEventCode As String) As List(Of BuhinEditAlEventVo) Implements HoyouBuhinBuhinEditAlDao.FindEventInfoById
        '    '2012-01-11 #1-2 SHISAKU_SYUBETU 追加
        '    Dim sql As String = "SELECT " _
        '                        & "    K.*," _
        '                        & "    BI.SHISAKU_GOUSYA," _
        '                        & "    BI.SHISAKU_SYUBETU," _
        '                        & "    BI.BASE_KAIHATSU_FUGO," _
        '                        & "    BI.BASE_SHIYOUJYOUHOU_NO," _
        '                        & "    BI.BASE_APPLIED_NO," _
        '                        & "    BI.BASE_KATASHIKI," _
        '                        & "    BI.BASE_SHIMUKE," _
        '                        & "    BI.BASE_OP," _
        '                        & "    BI.BASE_GAISOUSYOKU," _
        '                        & "    BI.BASE_NAISOUSYOKU," _
        '                        & "    BI.SHISAKU_BASE_EVENT_CODE," _
        '                        & "    BI.SHISAKU_BASE_GOUSYA, " _
        '                        & "    BI.SEISAKU_SYASYU AS BASE_SEISAKU_SYASYU, " _
        '                        & "    BI.SEISAKU_GRADE AS BASE_SEISAKU_GRADE, " _
        '                        & "    BI.SEISAKU_SHIMUKE AS BASE_SEISAKU_SHIMUKE, " _
        '                        & "    BI.SEISAKU_HANDORU AS BASE_SEISAKU_HANDORU, " _
        '                        & "    BI.SEISAKU_EG_HAIKIRYOU AS BASE_SEISAKU_EG_HAIKIRYOU, " _
        '                        & "    BI.SEISAKU_EG_KATASHIKI AS BASE_SEISAKU_EG_KATASHIKI, " _
        '                        & "    BI.SEISAKU_EG_KATASHIKI AS BASE_SEISAKU_EG_KATASHIKI, " _
        '                        & "    BI.SEISAKU_TM_KUDOU AS BASE_SEISAKU_TM_KUDOU, " _
        '                        & "    BI.SEISAKU_TM_HENSOKUKI AS BASE_SEISAKU_TM_HENSOKUKI, " _
        '                        & "    BI.SEISAKU_SYATAI_NO AS SHISAKU_SEISAKU_SYATAI_NO, " _
        '                        & "    B.SHISAKU_GOUSYA AS TENKAI_SHISAKU_GOUSYA," _
        '                        & "    B.SHISAKU_SYUBETU AS TENKAI_SHISAKU_SYUBETU," _
        '                        & "    B.BASE_KAIHATSU_FUGO AS TENKAI_BASE_KAIHATSU_FUGO," _
        '                        & "    B.BASE_SHIYOUJYOUHOU_NO AS TENKAI_BASE_SHIYOUJYOUHOU_NO," _
        '                        & "    B.BASE_APPLIED_NO AS TENKAI_BASE_APPLIED_NO," _
        '                        & "    B.BASE_KATASHIKI AS TENKAI_BASE_KATASHIKI," _
        '                        & "    B.BASE_SHIMUKE AS TENKAI_BASE_SHIMUKE," _
        '                        & "    B.BASE_OP AS TENKAI_BASE_OP," _
        '                        & "    B.BASE_GAISOUSYOKU AS TENKAI_BASE_GAISOUSYOKU," _
        '                        & "    B.BASE_NAISOUSYOKU AS TENKAI_BASE_NAISOUSYOKU," _
        '                        & "    B.SHISAKU_BASE_EVENT_CODE AS TENKAI_SHISAKU_BASE_EVENT_CODE," _
        '                        & "    B.SHISAKU_BASE_GOUSYA AS TENKAI_SHISAKU_BASE_GOUSYA " _
        '                        & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_SEISAKU_ICHIRAN BI WITH (NOLOCK, NOWAIT) " _
        '                        & "    INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI K " _
        '                        & "        ON BI.SHISAKU_EVENT_CODE = K.SHISAKU_EVENT_CODE " _
        '                        & "        AND BI.HYOJIJUN_NO = K.HYOJIJUN_NO " _
        '                        & "    INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B " _
        '                        & "        ON BI.SHISAKU_EVENT_CODE = B.SHISAKU_EVENT_CODE " _
        '                        & "        AND BI.HYOJIJUN_NO = B.HYOJIJUN_NO " _
        '                        & "WHERE BI.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "'" _
        '                        & "ORDER BY B.HYOJIJUN_NO"
        '    Dim db As New EBomDbClient
        '    Return db.QueryForList(Of BuhinEditAlEventVo)(sql)
        'End Function

        '担当者呼び出し用'
        ''' <summary>
        ''' 担当者の情報を取得する
        ''' </summary>
        ''' <param name="hoyouEventCode">イベントコード</param>
        ''' <param name="hoyouBukaCode">部課コード</param>
        ''' <param name="hoyouTanto">担当者</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByNewTnato(ByVal hoyouEventCode As String, ByVal hoyouBukaCode As String, ByVal hoyouTanto As String) As THoyouSekkeiTantoVo Implements Al.Dao.HoyouBuhinBuhinEditAlDao.FindByNewTanto
            'Dim sql As String = _
            '" SELECT * " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_HOYOU_SEKKEI_TANTO SB " _
            '& " WHERE " _
            '& " SB.HOYOU_EVENT_CODE = @HoyouEventCode " _
            '& " AND SB.HOYOU_BUKA_CODE = @HoyouBukaCode " _
            '& " AND SB.HOYOU_TANTO = @HoyouTanto " _
            '& " AND SB.HOYOU_TANTO_KAITEI_NO = ( " _
            '& "  SELECT MAX ( CONVERT(INT,COALESCE ( HOYOU_TANTO_KAITEI_NO,'' ) ) ) AS HOYOU_TANTO_KAITEI_NO  " _
            '& "  FROM " & MBOM_DB_NAME & ".dbo.T_HOYOU_SEKKEI_TANTO " _
            '& "  WHERE HOYOU_EVENT_CODE = SB.HOYOU_EVENT_CODE " _
            '& "  AND HOYOU_BUKA_CODE = SB.HOYOU_BUKA_CODE " _
            '& "  AND HOYOU_TANTO = SB.HOYOU_TANTO ) "

            Dim sb As New System.Text.StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_HOYOU_SEKKEI_TANTO SB ")
                .AppendLine(" WHERE ")
                .AppendLine(" SB.HOYOU_EVENT_CODE = @HoyouEventCode ")
                If Not hoyouBukaCode.Equals(String.Empty) Then
                    .AppendLine(" AND SB.HOYOU_BUKA_CODE = @HoyouBukaCode ")
                End If
                .AppendLine(" AND SB.HOYOU_TANTO = @HoyouTanto ")
                .AppendLine(" AND SB.HOYOU_TANTO_KAITEI_NO = ( ")
                .AppendLine("  SELECT MAX ( CONVERT(INT,COALESCE ( HOYOU_TANTO_KAITEI_NO,'' ) ) ) AS HOYOU_TANTO_KAITEI_NO  ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_HOYOU_SEKKEI_TANTO ")
                .AppendLine("  WHERE HOYOU_EVENT_CODE = SB.HOYOU_EVENT_CODE ")
                .AppendLine("  AND HOYOU_BUKA_CODE = SB.HOYOU_BUKA_CODE ")
                .AppendLine("  AND HOYOU_TANTO = SB.HOYOU_TANTO ) ")
            End With
            Dim db As New EBomDbClient
            Dim param As New THoyouSekkeiTantoVo

            param.HoyouEventCode = hoyouEventCode
            param.HoyouBukaCode = hoyouBukaCode
            param.HoyouTanto = hoyouTanto

            Return db.QueryForObject(Of THoyouSekkeiTantoVo)(sb.ToString, param)
        End Function
        ''' <summary>
        ''' DBのタイムスタンプを取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByDBTimeStamp(ByVal hoyouEventCode As String) As THoyouSekkeiTantoVo Implements HoyouBuhinBuhinEditAlDao.FindByDBTimeStamp
            Dim sql As String = _
            " SELECT  " _
            & " LTRIM(RTRIM(SUBSTRING(CONVERT(VARCHAR,getdate(),120),1,10))) AS UPDATED_DATE, " _
            & " LTRIM(RTRIM(SUBSTRING(CONVERT(VARCHAR,getdate(),120),12,8))) AS UPDATED_TIME  " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_HOYOU_SEKKEI_TANTO " _
            & " WHERE " _
            & " HOYOU_EVENT_CODE = @HoyouEventCode "

            Dim db As New EBomDbClient
            Dim param As New THoyouSekkeiTantoVo

            param.HoyouEventCode = hoyouEventCode

            Return db.QueryForObject(Of THoyouSekkeiTantoVo)(sql, param)

        End Function

        ''' <summary>
        ''' 補用設計ブロックの最終更新日を確認する
        ''' </summary>
        ''' <param name="hoyouEventCode"></param>
        ''' <param name="hoyouBukaCode"></param>
        ''' <param name="hoyouTanto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByLastModifyDateTimeOfSekkeiTanto(ByVal hoyouEventCode As String, ByVal hoyouBukaCode As String, ByVal hoyouTanto As String, ByVal openDateTime As String) As THoyouSekkeiTantoVo Implements HoyouBuhinBuhinEditAlDao.FindByLastModifyDateTimeOfSekkeiTanto
            Dim sql As String = _
            " SELECT  " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME  " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_HOYOU_SEKKEI_TANTO " _
            & " WHERE HOYOU_EVENT_CODE = @HoyouEventCode " _
            & "   AND HOYOU_BUKA_CODE = @HoyouBukaCode " _
            & "   AND HOYOU_TANTO = @HoyouTanto " _
            & "   AND UPDATED_DATE + UPDATED_TIME > '" & openDateTime & "'"

            Dim db As New EBomDbClient
            Dim param As New THoyouSekkeiTantoVo

            param.HoyouEventCode = hoyouEventCode
            param.HoyouBukaCode = hoyouBukaCode
            param.HoyouTanto = hoyouTanto

            Return db.QueryForObject(Of THoyouSekkeiTantoVo)(sql, param)

        End Function


    End Class
End Namespace