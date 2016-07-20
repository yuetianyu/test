Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon

Namespace ShisakuBuhinEdit.Al.Dao
    ''' <summary>
    ''' A/L画面用のDao実装クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditAlDaoImpl : Inherits DaoEachFeatureImpl
        Implements BuhinEditAlDao

        ''' <summary>
        ''' ベース車情報・完成車情報を参照する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>ベース車情報・完成車情報</returns>
        ''' <remarks></remarks>
        Public Function FindEventInfoById(ByVal shisakuEventCode As String) As List(Of BuhinEditAlEventVo) Implements BuhinEditAlDao.FindEventInfoById
            '2012-01-11 #1-2 SHISAKU_SYUBETU 追加
            '/*** 20140911 CHANGE START（パラメータ化） ***/
            'Dim sql As String = "SELECT " _
            '                    & "    K.*," _
            '                    & "    BI.SHISAKU_GOUSYA," _
            '                    & "    BI.SHISAKU_SYUBETU," _
            '                    & "    BI.BASE_KAIHATSU_FUGO," _
            '                    & "    BI.BASE_SHIYOUJYOUHOU_NO," _
            '                    & "    BI.BASE_APPLIED_NO," _
            '                    & "    BI.BASE_KATASHIKI," _
            '                    & "    BI.BASE_SHIMUKE," _
            '                    & "    BI.BASE_OP," _
            '                    & "    BI.BASE_GAISOUSYOKU," _
            '                    & "    BI.BASE_NAISOUSYOKU," _
            '                    & "    BI.SHISAKU_BASE_EVENT_CODE," _
            '                    & "    BI.SHISAKU_BASE_GOUSYA, " _
            '                    & "    BI.SEISAKU_SYASYU AS BASE_SEISAKU_SYASYU, " _
            '                    & "    BI.SEISAKU_GRADE AS BASE_SEISAKU_GRADE, " _
            '                    & "    BI.SEISAKU_SHIMUKE AS BASE_SEISAKU_SHIMUKE, " _
            '                    & "    BI.SEISAKU_HANDORU AS BASE_SEISAKU_HANDORU, " _
            '                    & "    BI.SEISAKU_EG_HAIKIRYOU AS BASE_SEISAKU_EG_HAIKIRYOU, " _
            '                    & "    BI.SEISAKU_EG_KATASHIKI AS BASE_SEISAKU_EG_KATASHIKI, " _
            '                    & "    BI.SEISAKU_EG_KATASHIKI AS BASE_SEISAKU_EG_KATASHIKI, " _
            '                    & "    BI.SEISAKU_TM_KUDOU AS BASE_SEISAKU_TM_KUDOU, " _
            '                    & "    BI.SEISAKU_TM_HENSOKUKI AS BASE_SEISAKU_TM_HENSOKUKI, " _
            '                    & "    BI.SEISAKU_SYATAI_NO AS SHISAKU_SEISAKU_SYATAI_NO, " _
            '                    & "    B.SHISAKU_GOUSYA AS TENKAI_SHISAKU_GOUSYA," _
            '                    & "    B.SHISAKU_SYUBETU AS TENKAI_SHISAKU_SYUBETU," _
            '                    & "    B.BASE_KAIHATSU_FUGO AS TENKAI_BASE_KAIHATSU_FUGO," _
            '                    & "    B.BASE_SHIYOUJYOUHOU_NO AS TENKAI_BASE_SHIYOUJYOUHOU_NO," _
            '                    & "    B.BASE_APPLIED_NO AS TENKAI_BASE_APPLIED_NO," _
            '                    & "    B.BASE_KATASHIKI AS TENKAI_BASE_KATASHIKI," _
            '                    & "    B.BASE_SHIMUKE AS TENKAI_BASE_SHIMUKE," _
            '                    & "    B.BASE_OP AS TENKAI_BASE_OP," _
            '                    & "    B.BASE_GAISOUSYOKU AS TENKAI_BASE_GAISOUSYOKU," _
            '                    & "    B.BASE_NAISOUSYOKU AS TENKAI_BASE_NAISOUSYOKU," _
            '                    & "    B.SHISAKU_BASE_EVENT_CODE AS TENKAI_SHISAKU_BASE_EVENT_CODE," _
            '                    & "    B.SHISAKU_BASE_GOUSYA AS TENKAI_SHISAKU_BASE_GOUSYA " _
            '                    & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_SEISAKU_ICHIRAN BI WITH (NOLOCK, NOWAIT) " _
            '                    & "    INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI K " _
            '                    & "        ON BI.SHISAKU_EVENT_CODE = K.SHISAKU_EVENT_CODE " _
            '                    & "        AND BI.HYOJIJUN_NO = K.HYOJIJUN_NO " _
            '                    & "    INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B " _
            '                    & "        ON BI.SHISAKU_EVENT_CODE = B.SHISAKU_EVENT_CODE " _
            '                    & "        AND BI.HYOJIJUN_NO = B.HYOJIJUN_NO " _
            '                    & "WHERE BI.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "'" _
            '                    & "ORDER BY B.HYOJIJUN_NO"
            'Dim db As New EBomDbClient
            'Return db.QueryForList(Of BuhinEditAlEventVo)(sql)
            
            Dim sql As String = "SELECT " _
                                & "    K.*," _
                                & "    BI.SHISAKU_GOUSYA," _
                                & "    BI.SHISAKU_SYUBETU," _
                                & "    BI.BASE_KAIHATSU_FUGO," _
                                & "    BI.BASE_SHIYOUJYOUHOU_NO," _
                                & "    BI.BASE_APPLIED_NO," _
                                & "    BI.BASE_KATASHIKI," _
                                & "    BI.BASE_SHIMUKE," _
                                & "    BI.BASE_OP," _
                                & "    BI.BASE_GAISOUSYOKU," _
                                & "    BI.BASE_NAISOUSYOKU," _
                                & "    BI.SHISAKU_BASE_EVENT_CODE," _
                                & "    BI.SHISAKU_BASE_GOUSYA, " _
                                & "    BI.SEISAKU_SYASYU AS BASE_SEISAKU_SYASYU, " _
                                & "    BI.SEISAKU_GRADE AS BASE_SEISAKU_GRADE, " _
                                & "    BI.SEISAKU_SHIMUKE AS BASE_SEISAKU_SHIMUKE, " _
                                & "    BI.SEISAKU_HANDORU AS BASE_SEISAKU_HANDORU, " _
                                & "    BI.SEISAKU_EG_HAIKIRYOU AS BASE_SEISAKU_EG_HAIKIRYOU, " _
                                & "    BI.SEISAKU_EG_KATASHIKI AS BASE_SEISAKU_EG_KATASHIKI, " _
                                & "    BI.SEISAKU_EG_KATASHIKI AS BASE_SEISAKU_EG_KATASHIKI, " _
                                & "    BI.SEISAKU_TM_KUDOU AS BASE_SEISAKU_TM_KUDOU, " _
                                & "    BI.SEISAKU_TM_HENSOKUKI AS BASE_SEISAKU_TM_HENSOKUKI, " _
                                & "    BI.SEISAKU_SYATAI_NO AS SHISAKU_SEISAKU_SYATAI_NO, " _
                                & "    B.SHISAKU_GOUSYA AS TENKAI_SHISAKU_GOUSYA," _
                                & "    B.SHISAKU_SYUBETU AS TENKAI_SHISAKU_SYUBETU," _
                                & "    B.BASE_KAIHATSU_FUGO AS TENKAI_BASE_KAIHATSU_FUGO," _
                                & "    B.BASE_SHIYOUJYOUHOU_NO AS TENKAI_BASE_SHIYOUJYOUHOU_NO," _
                                & "    B.BASE_APPLIED_NO AS TENKAI_BASE_APPLIED_NO," _
                                & "    B.BASE_KATASHIKI AS TENKAI_BASE_KATASHIKI," _
                                & "    B.BASE_SHIMUKE AS TENKAI_BASE_SHIMUKE," _
                                & "    B.BASE_OP AS TENKAI_BASE_OP," _
                                & "    B.BASE_GAISOUSYOKU AS TENKAI_BASE_GAISOUSYOKU," _
                                & "    B.BASE_NAISOUSYOKU AS TENKAI_BASE_NAISOUSYOKU," _
                                & "    B.SHISAKU_BASE_EVENT_CODE AS TENKAI_SHISAKU_BASE_EVENT_CODE," _
                                & "    B.SHISAKU_BASE_GOUSYA AS TENKAI_SHISAKU_BASE_GOUSYA " _
                                & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_SEISAKU_ICHIRAN BI WITH (NOLOCK, NOWAIT) " _
                                & "    INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI K " _
                                & "        ON BI.SHISAKU_EVENT_CODE = K.SHISAKU_EVENT_CODE " _
                                & "        AND BI.HYOJIJUN_NO = K.HYOJIJUN_NO " _
                                & "    INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B " _
                                & "        ON BI.SHISAKU_EVENT_CODE = B.SHISAKU_EVENT_CODE " _
                                & "        AND BI.HYOJIJUN_NO = B.HYOJIJUN_NO " _
                                & "WHERE BI.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                                & "ORDER BY B.HYOJIJUN_NO"
            Dim db As New EBomDbClient
            Dim param As New BuhinEditAlEventVo
            param.ShisakuEventCode = shisakuEventCode

            Return db.QueryForList(Of BuhinEditAlEventVo)(sql, param)
        '/*** 20140911 CHANGE END ***/
        End Function

        'ブロック呼び出し用'
        ''' <summary>
        ''' ブロックNoの情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByNewBlockNo(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As TShisakuSekkeiBlockVo Implements Al.Dao.BuhinEditAlDao.FindByNewBlockNo
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
            & " WHERE " _
            & " SB.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SB.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SB.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
            & "  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  " _
            & "  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK " _
            & "  WHERE SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE " _
            & "  AND SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE " _
            & "  AND SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ) "

            Dim db As New EBomDbClient
            Dim param As New TShisakuSekkeiBlockVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            Return db.QueryForObject(Of TShisakuSekkeiBlockVo)(sql, param)
        End Function
        ''' <summary>
        ''' DBのタイムスタンプを取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByDBTimeStamp(ByVal shisakuEventCode As String) As TShisakuSekkeiBlockVo Implements BuhinEditAlDao.FindByDBTimeStamp
            Dim sql As String = _
            " SELECT  " _
            & " LTRIM(RTRIM(SUBSTRING(CONVERT(VARCHAR,getdate(),120),1,10))) AS UPDATED_DATE, " _
            & " LTRIM(RTRIM(SUBSTRING(CONVERT(VARCHAR,getdate(),120),12,8))) AS UPDATED_TIME  " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuSekkeiBlockVo

            param.ShisakuEventCode = shisakuEventCode

            Return db.QueryForObject(Of TShisakuSekkeiBlockVo)(sql, param)

        End Function

        ''' <summary>
        ''' 試作設計ブロックの最終更新日を確認する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuBukaCode"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByLastModifyDateTimeOfSekkeiBlock(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal openDateTime As String) As TShisakuSekkeiBlockVo Implements BuhinEditAlDao.FindByLastModifyDateTimeOfSekkeiBlock
            Dim sql As String = _
            " SELECT  " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME  " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & "   AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & "   AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & "   AND UPDATED_DATE + UPDATED_TIME > '" & openDateTime & "'"

            Dim db As New EBomDbClient
            Dim param As New TShisakuSekkeiBlockVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            Return db.QueryForObject(Of TShisakuSekkeiBlockVo)(sql, param)

        End Function


    End Class
End Namespace