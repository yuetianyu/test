Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon

Namespace ShisakuBuhinEdit.Selector.Dao
    Public Class DispEventBuhinCopySelectorDaoImpl
        Implements DispEventBuhinCopySelectorDao
        ''' <summary>
        ''' 自身を除いたイベント情報を返す
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindEvent(ByVal blockNo As String, ByVal shisakuEventCode As String) As List(Of TShisakuEventVo) Implements DispEventBuhinCopySelectorDao.FindEvent
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("    E.SHISAKU_EVENT_CODE, ")
                .AppendLine("    E.SHISAKU_KAIHATSU_FUGO, ")
                .AppendLine("    E.SHISAKU_EVENT_PHASE_NAME, ")
                .AppendLine("    E.UNIT_KBN, ")
                .AppendLine("    E.SHISAKU_EVENT_NAME ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT E ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine(" ON E.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND NOT SB.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO  = ( ")
                .AppendLine("  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ) ")
                .AppendLine("ORDER BY E.SHISAKU_EVENT_CODE, E.SHISAKU_KAIHATSU_FUGO ")
            End With

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = blockNo
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventVo)(sql.ToString, param)

        End Function

        ''' <summary>
        ''' 設計ブロックINSTL情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <param name="kaiteiNo">改定No　2014/08/04 Ⅰ.11.改訂戻し機能 ｈ) (TES)施 追加</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindBySekkeiBlockInstl(ByVal shisakuEventCode As String, ByVal blockNo As String, Optional ByVal KaiteiNo As String = "") As List(Of TShisakuSekkeiBlockInstlVo) Implements DispEventBuhinCopySelectorDao.FindBySekkeiBlockInstl

            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT SBI.INSTL_HINBAN_HYOUJI_JUN,")
                .AppendLine(" SBI.INSTL_HINBAN, ")
                .AppendLine(" SBI.INSTL_HINBAN_KBN, ")
                ''↓↓2014/09/23 Ⅰ.11.改訂戻し機能 ｈ) 酒井 ADD BEGIN
                .AppendLine(" SBI.INSTL_DATA_KBN, ")
                .AppendLine(" SBI.BASE_INSTL_FLG, ")
                .AppendLine(" SBI.SHISAKU_GOUSYA, ")
                .AppendLine(" SBI.INSU_SURYO, ")
                ''↑↑2014/09/23 Ⅰ.11.改訂戻し機能 ｈ) 酒井 ADD END
                .AppendLine(" SBI.BF_BUHIN_NO ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ")
                .AppendLine(" WHERE ")
                .AppendLine(" SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能 ｈ) (TES)施 ADD BEGIN
                If Not KaiteiNo = "" Then
                    .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                    ''↓↓2014/08/20 Ⅰ.11.改訂戻し機能 ｈ) 酒井 ADD BEGIN
                Else
                    'End If
                    ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 ｈ) (TES)施 ADD END

                    .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO  = ( ")
                    .AppendLine("  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                    .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                    .AppendLine(" WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                    .AppendLine(" AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) ")
                    '2012/03/13 No.234 235
                    .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO <> '  0'")
                End If
                ''↑↑2014/08/20 Ⅰ.11.改訂戻し機能 ｈ) 酒井 ADD END
                ''↓↓2014/09/23 Ⅰ.11.改訂戻し機能 ｈ) 酒井 ADD BEGIN
                '.AppendLine(" GROUP BY SBI.INSTL_HINBAN_HYOUJI_JUN, SBI.INSTL_HINBAN, SBI.INSTL_HINBAN_KBN, SBI.BF_BUHIN_NO ")
                .AppendLine(" GROUP BY SBI.INSTL_HINBAN_HYOUJI_JUN, SBI.INSTL_HINBAN, SBI.INSTL_HINBAN_KBN, SBI.BF_BUHIN_NO ,SBI.INSTL_DATA_KBN, SBI.BASE_INSTL_FLG, SBI.INSU_SURYO, SBI.SHISAKU_GOUSYA")
                ''↑↑2014/09/23 Ⅰ.11.改訂戻し機能 ｈ) 酒井 ADD END
                .AppendLine("ORDER BY SBI.INSTL_HINBAN_HYOUJI_JUN ")
            End With

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = blockNo
            ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能 ｈ) (TES)施 ADD BEGIN
            If Not KaiteiNo = "" Then
                param.ShisakuBlockNoKaiteiNo = KaiteiNo
            End If
            ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 ｈ) (TES)施 ADD END
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sql.ToString, param)
        End Function

        ''↓↓2014/09/23 酒井 ADD BEGIN
        Public Function FindByEventBase(ByVal shisakuEventCode As String) As List(Of TShisakuEventBaseVo) Implements DispEventBuhinCopySelectorDao.FindByEventBase

            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT *")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE BASE")
                .AppendLine(" WHERE ")
                .AppendLine(" BASE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
            End With

            Dim param As New TShisakuEventBaseVo
            param.ShisakuEventCode = shisakuEventCode
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventBaseVo)(sql.ToString, param)
        End Function
        ''↑↑2014/09/23 酒井 ADD END  

        ''' <summary>
        ''' 開発符号を取得
        ''' </summary>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindEventkaiHatsuFugoList(ByVal blockNo As String, ByVal shisakuEventCode As String) As List(Of TShisakuEventVo) Implements DispEventBuhinCopySelectorDao.FindEventkaiHatsuFugoList
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("    E.SHISAKU_KAIHATSU_FUGO ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT E ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine(" ON E.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND NOT SB.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO  = ( ")
                .AppendLine("  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" GROUP BY E.SHISAKU_KAIHATSU_FUGO ")
                .AppendLine("ORDER BY  E.SHISAKU_KAIHATSU_FUGO ")
            End With

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = blockNo
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventVo)(sql.ToString, param)
        End Function

        ''' <summary>
        ''' 開発符号を取得
        ''' </summary>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindBykaiHatsuFugo(ByVal blockNo As String, ByVal shisakuEventCode As String) As TShisakuEventVo Implements DispEventBuhinCopySelectorDao.FindBykaiHatsuFugo
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("    E.SHISAKU_KAIHATSU_FUGO ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT E ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine(" ON E.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND NOT SB.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO  = ( ")
                .AppendLine("  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" GROUP BY E.SHISAKU_KAIHATSU_FUGO ")
                .AppendLine("ORDER BY  E.SHISAKU_KAIHATSU_FUGO ")
            End With

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = blockNo
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventVo)(sql.ToString, param)
        End Function

        ''' <summary>
        ''' イベント情報を返す
        ''' </summary>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuKaihatsuFugo">開発符号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByEventKaiHatsuFugo(ByVal blockNo As String, ByVal shisakuEventCode As String, ByVal shisakuKaihatsuFugo As String) As List(Of TShisakuEventVo) Implements DispEventBuhinCopySelectorDao.FindByEventKaiHatsuFugo
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("    E.SHISAKU_EVENT_CODE, ")
                .AppendLine("    E.SHISAKU_KAIHATSU_FUGO, ")
                .AppendLine("    E.SHISAKU_EVENT_PHASE_NAME, ")
                .AppendLine("    E.UNIT_KBN, ")
                .AppendLine("    E.SHISAKU_EVENT_NAME ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT E ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine(" ON E.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND NOT SB.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO  = ( ")
                .AppendLine("  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" WHERE E.SHISAKU_KAIHATSU_FUGO = '" & shisakuKaihatsuFugo & "' ")
                .AppendLine("ORDER BY E.SHISAKU_EVENT_CODE, E.SHISAKU_KAIHATSU_FUGO ")
            End With

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = blockNo

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventVo)(sql.ToString, param)
        End Function


    End Class
End Namespace
