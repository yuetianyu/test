Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports System.Text
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl

Namespace ShisakuBuhinEditBlock.Dao
    ''' <summary>
    ''' Excel出力を集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class EditBlock2ExcelDaoImpl : Inherits DaoEachFeatureImpl
        Implements EditBlock2ExcelDao

        Dim db As New EBomDbClient
 
        ''' <summary>
        ''' 改訂ブロック№データを読む
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNo">試作ブロック№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindKaiteiNoBy(ByVal eventCode As String, _
                            ByVal bukaCode As String, _
                            ByVal blockNo As String) _
               As List(Of LabelValueVo) _
               Implements EditBlock2ExcelDao.FindKaiteiNoBy
            'change s.ota 2012-01-09 改修 #2-2
            Dim sql As String = "SELECT " _
            & " DISTINCT " _
            & " CASE SB.BLOCK_FUYOU " _
            & " WHEN '1' THEN SHISAKU_BLOCK.SHISAKU_BLOCK_NO_KAITEI_NO + '(不要)' " _
            & " ELSE SHISAKU_BLOCK.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " END AS LABEL, " _
            & " SHISAKU_BLOCK.SHISAKU_BLOCK_NO_KAITEI_NO AS VALUE " _
            & "FROM " _
            & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SHISAKU_BLOCK WITH (NOLOCK, NOWAIT) " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
            & " ON " _
            & " SHISAKU_BLOCK.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE AND " _
            & " SHISAKU_BLOCK.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE AND " _
            & " SHISAKU_BLOCK.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO AND " _
            & " SHISAKU_BLOCK.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & "WHERE " _
            & "SHISAKU_BLOCK.SHISAKU_EVENT_CODE = @ShisakuEventCode AND " _
            & "SHISAKU_BLOCK.SHISAKU_BUKA_CODE = @ShisakuBukaCode AND " _
            & "SHISAKU_BLOCK.SHISAKU_BLOCK_NO = @ShisakuBlockNo  " _
            & " AND NOT SHISAKU_BLOCK.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' " _
            & "ORDER BY " _
            & "SHISAKU_BLOCK.SHISAKU_BLOCK_NO_KAITEI_NO DESC "

            Dim param As New EditBlock2ExcelParamVo
            param.ShisakuEventCode = eventCode
            param.ShisakuBukaCode = bukaCode
            param.ShisakuBlockNo = blockNo

            Return db.QueryForList(Of LabelValueVo)(sql, param)
        End Function

        ''' <summary>
        ''' 試作A/L情報ヘッダ開発符号、試作部品イベント名称、担当者、Telを読む
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNo">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindHeadInfoWithSekkeiBlockBy(ByVal eventCode As String, _
                             ByVal bukaCode As String, _
                             ByVal blockNo As String, _
                             ByVal blockKaiteNo As String) _
                As EditBlock2ExcelTitle3BodyVo _
                Implements EditBlock2ExcelDao.FindHeadInfoWithSekkeiBlockBy
            Dim sql As String = "SELECT " _
                   & " SHISAKU_BLOCK.USER_ID, " _
                   & " SHISAKU_BLOCK.TEL_NO, " _
                   & " SHISAKU_EVENT.SHISAKU_KAIHATSU_FUGO, " _
                   & " SHISAKU_EVENT.SHISAKU_EVENT_NAME " _
                   & "FROM " _
                   & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SHISAKU_BLOCK WITH (NOLOCK, NOWAIT) " _
                   & "LEFT JOIN  " _
                   & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT SHISAKU_EVENT " _
                   & "ON " _
                   & " SHISAKU_BLOCK.SHISAKU_EVENT_CODE = SHISAKU_EVENT.SHISAKU_EVENT_CODE " _
                   & "WHERE " _
                   & "SHISAKU_BLOCK.SHISAKU_EVENT_CODE = @ShisakuEventCode AND " _
                   & "SHISAKU_BLOCK.SHISAKU_BUKA_CODE = @ShisakuBukaCode AND " _
                   & "SHISAKU_BLOCK.SHISAKU_BLOCK_NO = @ShisakuBlockNo AND " _
                   & "SHISAKU_BLOCK.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo "


            Dim param As New EditBlock2ExcelParamVo
            param.ShisakuEventCode = eventCode
            param.ShisakuBukaCode = bukaCode
            param.ShisakuBlockNo = blockNo
            param.ShisakuBlockNoKaiteiNo = blockKaiteNo

            Return db.QueryForObject(Of EditBlock2ExcelTitle3BodyVo)(sql, param)
        End Function

        ''' <summary>
        ''' 試作イベントベース車項目No.対応表の項目名を返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNoList">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindWithTitle1NameBy(ByVal eventCode As String, _
                                             ByVal bukaCode As String, _
                                             ByVal blockNoList As List(Of String), _
                                             ByVal blockKaiteNo As String, _
                                             ByVal isBase As Boolean) _
                                             As List(Of EditBlock2ExcelTitle1Vo) _
                                             Implements EditBlock2ExcelDao.FindWithTitle1NameBy
            '適用外でも出力'
            '& "AND NOT ( BLOCK_INSTL.INSU_SURYO IS NULL OR BLOCK_INSTL.INSU_SURYO ='' ) " _
            Dim str As String = ""

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("BLOCK_SOUBI.SHISAKU_SOUBI_HYOUJI_JUN ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL AS BLOCK_INSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("LEFT JOIN ")
                .AppendLine("" & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_SOUBI AS BLOCK_SOUBI ")
                .AppendLine("ON ")
                .AppendLine("BLOCK_INSTL.SHISAKU_EVENT_CODE = BLOCK_SOUBI.SHISAKU_EVENT_CODE AND ")
                .AppendLine("BLOCK_INSTL.SHISAKU_BUKA_CODE = BLOCK_SOUBI.SHISAKU_BUKA_CODE AND ")
                .AppendLine("BLOCK_INSTL.SHISAKU_BLOCK_NO = BLOCK_SOUBI.SHISAKU_BLOCK_NO AND ")
                If isBase Then
                    str = "BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO = '000' "
                Else
                    str = "BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO = BLOCK_SOUBI.SHISAKU_BLOCK_NO_KAITEI_NO "
                End If
                .AppendLine(Str)
                .AppendLine("WHERE ")
                ''↓↓2014/08/07 Ⅰ.10.全ブロックEXCEL出力での出力内容変更_g) (TES)張 CHG BEGIN
                ''.AppendLine("BLOCK_INSTL.SHISAKU_EVENT_CODE = @ShisakuEventCode AND ")
                ''.AppendLine("BLOCK_INSTL.SHISAKU_BUKA_CODE = @ShisakuBukaCode AND ")
                ''.AppendLine("BLOCK_INSTL.SHISAKU_BLOCK_NO = @ShisakuBlockNo AND ")
                ''.AppendLine("BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine("BLOCK_INSTL.SHISAKU_EVENT_CODE = @ShisakuEventCode AND ")
                .AppendLine("BLOCK_INSTL.SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                If blockNoList IsNot Nothing Then
                    If blockNoList.Count = 1 Then
                        .AppendFormat(" AND BLOCK_INSTL.SHISAKU_BLOCK_NO = '{0}'", blockNoList(0))
                    Else
                        Dim buf As New List(Of String)
                        For Each s As String In blockNoList
                            buf.Add("'" & s & "'")
                        Next
                        .AppendFormat(" AND BLOCK_INSTL.SHISAKU_BLOCK_NO IN ({0})", String.Join(",", buf.ToArray))
                    End If

                    If blockKaiteNo IsNot Nothing Then
                        .AppendLine(" AND BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                    Else
                        .AppendLine(" AND BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO = (")
                        .AppendLine("SELECT MAX(SHISAKU_BLOCK_NO_KAITEI_NO)")
                        .AppendLine(" from  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB")
                        .AppendLine(" where(BLOCK_INSTL.SHISAKU_EVENT_CODE = sb.SHISAKU_EVENT_CODE)")
                        .AppendLine(" and BLOCK_INSTL.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE")
                        .AppendLine(" and BLOCK_INSTL.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO)")
                    End If
                Else
                    .AppendLine(" AND BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO = (")
                    .AppendLine(" SELECT MAX(SHISAKU_BLOCK_NO_KAITEI_NO)")
                    .AppendLine(" from  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB")
                    .AppendLine(" where(BLOCK_INSTL.SHISAKU_EVENT_CODE = sb.SHISAKU_EVENT_CODE)")
                    .AppendLine(" and BLOCK_INSTL.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE")
                    .AppendLine(" and BLOCK_INSTL.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO)")
                End If


                'If Not String.IsNullOrEmpty(blockNoList) Then
                '    .AppendLine(" AND ")
                '    .AppendLine("BLOCK_INSTL.SHISAKU_BLOCK_NO = @ShisakuBlockNo AND ")
                '    .AppendLine("BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                'Else
                '    .AppendLine("AND BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO = (")
                '    .AppendLine("SELECT MAX(SHISAKU_BLOCK_NO_KAITEI_NO)")
                '    .AppendLine(" from  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB")
                '    .AppendLine(" where(BLOCK_INSTL.SHISAKU_EVENT_CODE = sb.SHISAKU_EVENT_CODE)")
                '    .AppendLine(" and BLOCK_INSTL.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE")
                '    .AppendLine(" and BLOCK_INSTL.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO)")

                '    '.AppendLine(" AND ")
                '    '.AppendLine(" EXISTS ( ")
                '    '.AppendLine(" SELECT ")
                '    '.AppendLine(" BE.SHISAKU_BLOCK_NO ")
                '    '.AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) ")
                '    '.AppendLine(" WHERE ")
                '    '.AppendLine(" BE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                '    '.AppendLine(" AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                '    '.AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                '    '.AppendLine(" SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS ")
                '    '.AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO ")
                '    '.AppendLine(" FROM ")
                '    '.AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) ")
                '    '.AppendLine(" WHERE ")
                '    '.AppendLine(" SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                '    '.AppendLine(" AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                '    '.AppendLine(" AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ) ")
                '    '.AppendLine(" GROUP BY BE.SHISAKU_BLOCK_NO ")
                '    '.AppendLine(" )")
                'End If
                ''↑↑2014/08/07 Ⅰ.10.全ブロックEXCEL出力での出力内容変更_g) (TES)張 CHG END
                .AppendLine("GROUP BY ")
                .AppendLine("BLOCK_SOUBI.SHISAKU_SOUBI_HYOUJI_JUN ")
                .AppendLine("ORDER BY ")
                .AppendLine("BLOCK_SOUBI.SHISAKU_SOUBI_HYOUJI_JUN ")
            End With


            Dim param As New EditBlock2ExcelParamVo
            param.ShisakuEventCode = eventCode
            param.ShisakuBukaCode = bukaCode
            'param.ShisakuBlockNo = blockNoList
            If isBase Then
                param.ShisakuBlockNoKaiteiNo = "  0"
            Else
                param.ShisakuBlockNoKaiteiNo = blockKaiteNo
            End If


            Return db.QueryForList(Of EditBlock2ExcelTitle1Vo)(sb.ToString, param)

        End Function
        ''' <summary>
        ''' 試作イベント装備仕様項目名を返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNoList">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindWithTitle2NameBy(ByVal eventCode As String, _
                            ByVal bukaCode As String, _
                            ByVal blockNoList As List(Of String), _
                            ByVal blockKaiteNo As String, _
                            ByVal isBase As Boolean) _
                            As List(Of EditBlock2ExcelTitle2Vo) _
                            Implements EditBlock2ExcelDao.FindWithTitle2NameBy

            Dim sb As New StringBuilder
            Dim str As String = ""
            With sb
                .AppendLine("SELECT  ")
                .AppendLine(" DISTINCT ")
                .AppendLine("  SOUBISHIYOU.SHISAKU_SOUBI_HYOUJI_JUN, ")
                .AppendLine("  SOUBISHIYOU.SHISAKU_SOUBI_KBN, ")
                .AppendLine("  SOUBISHIYOU.SHISAKU_RETU_KOUMOKU_CODE, ")
                .AppendLine("  SOUBI.SHISAKU_RETU_KOUMOKU_NAME, ")
                .AppendLine("  SOUBI.SHISAKU_RETU_KOUMOKU_NAME_DAI, ")
                .AppendLine("  SOUBI.SHISAKU_RETU_KOUMOKU_NAME_CHU ")
                .AppendLine("FROM ")
                .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL BLOCKINSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("LEFT JOIN ")
                .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_SOUBI_SHIYOU SOUBISHIYOU ")
                .AppendLine("ON ")
                .AppendLine("  BLOCKINSTL.SHISAKU_EVENT_CODE = SOUBISHIYOU.SHISAKU_EVENT_CODE AND ")
                .AppendLine("  BLOCKINSTL.SHISAKU_BUKA_CODE = SOUBISHIYOU.SHISAKU_BUKA_CODE AND ")
                .AppendLine("  BLOCKINSTL.SHISAKU_BLOCK_NO = SOUBISHIYOU.SHISAKU_BLOCK_NO AND ")

                If isBase Then
                    str = "  BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = '000' "
                Else
                    str = " BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = SOUBISHIYOU.SHISAKU_BLOCK_NO_KAITEI_NO "
                End If
                .AppendLine(str)
                '2014/12/22 変更 
                .AppendLine("INNER JOIN ")
                .AppendLine(" " & MBOM_DB_NAME & ".dbo.M_SHISAKU_SOUBI SOUBI ")
                '.AppendLine("LEFT JOIN ")
                '.AppendLine(" " & MBOM_DB_NAME & ".dbo.M_SHISAKU_SOUBI SOUBI ")
                .AppendLine("ON ")
                .AppendLine("  SOUBISHIYOU.SHISAKU_SOUBI_KBN = SOUBI.SHISAKU_SOUBI_KBN AND ")
                .AppendLine("  SOUBISHIYOU.SHISAKU_RETU_KOUMOKU_CODE = SOUBI.SHISAKU_RETU_KOUMOKU_CODE ")
                .AppendLine("WHERE ")

                ''↓↓2014/08/07 Ⅰ.10.全ブロックEXCEL出力での出力内容変更_k) (TES)張 CHG BEGIN
                .AppendLine("  BLOCKINSTL.SHISAKU_EVENT_CODE = @ShisakuEventCode AND ")
                .AppendLine("  BLOCKINSTL.SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")

                If blockNoList IsNot Nothing Then
                    If blockNoList.Count = 1 Then
                        .AppendFormat("  AND BLOCKINSTL.SHISAKU_BLOCK_NO = '{0}' ", blockNoList(0))
                    Else
                        Dim buf As New List(Of String)
                        For Each s As String In blockNoList
                            buf.Add("'" & s & "'")
                        Next
                        .AppendFormat("  AND BLOCKINSTL.SHISAKU_BLOCK_NO IN ({0}) ", String.Join(",", buf.ToArray))
                    End If

                    If blockKaiteNo IsNot Nothing Then
                        .AppendLine("  AND BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                    Else
                        .AppendLine(" AND BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = (")
                        .AppendLine(" SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))")
                        .AppendLine(" from  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB")
                        .AppendLine(" where(BLOCKINSTL.SHISAKU_EVENT_CODE = sb.SHISAKU_EVENT_CODE)")
                        .AppendLine(" and BLOCKINSTL.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE")
                        .AppendLine(" and BLOCKINSTL.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO)")
                    End If
                Else
                    .AppendLine("AND BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = (")
                    .AppendLine("SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))")
                    .AppendLine(" from  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB")
                    .AppendLine(" where(BLOCKINSTL.SHISAKU_EVENT_CODE = sb.SHISAKU_EVENT_CODE)")
                    .AppendLine(" and BLOCKINSTL.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE")
                    .AppendLine(" and BLOCKINSTL.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO)")
                End If



                'If Not String.IsNullOrEmpty(blockNo) Then
                '    .AppendLine("  AND ")
                '    .AppendLine("  BLOCKINSTL.SHISAKU_BLOCK_NO = @ShisakuBlockNo AND ")
                '    .AppendLine("  BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                'Else
                '    .AppendLine("AND BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = (")
                '    .AppendLine("SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))")
                '    .AppendLine(" from  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB")
                '    .AppendLine(" where(BLOCKINSTL.SHISAKU_EVENT_CODE = sb.SHISAKU_EVENT_CODE)")
                '    .AppendLine(" and BLOCKINSTL.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE")
                '    .AppendLine(" and BLOCKINSTL.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO)")

                '    '.AppendLine(" AND ")
                '    '.AppendLine(" EXISTS ( ")
                '    '.AppendLine(" SELECT ")
                '    '.AppendLine(" BE.SHISAKU_BLOCK_NO ")
                '    '.AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) ")
                '    '.AppendLine(" WHERE ")
                '    '.AppendLine(" BE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                '    '.AppendLine(" AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                '    '.AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                '    '.AppendLine(" SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS ")
                '    '.AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO ")
                '    '.AppendLine(" FROM ")
                '    '.AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) ")
                '    '.AppendLine(" WHERE ")
                '    '.AppendLine(" SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                '    '.AppendLine(" AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                '    '.AppendLine(" AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ) ")
                '    '.AppendLine(" GROUP BY BE.SHISAKU_BLOCK_NO ")
                '    '.AppendLine(" )")
                'End If
                ''↑↑2014/08/07 Ⅰ.10.全ブロックEXCEL出力での出力内容変更_k) (TES)張 CHG END
                .AppendLine("  AND NOT ( BLOCKINSTL.INSU_SURYO IS NULL OR BLOCKINSTL.INSU_SURYO ='' ) ")
                .AppendLine("GROUP BY ")
                .AppendLine("  SOUBISHIYOU.SHISAKU_SOUBI_HYOUJI_JUN, ")
                .AppendLine("  SOUBISHIYOU.SHISAKU_SOUBI_KBN, ")
                .AppendLine("  SOUBISHIYOU.SHISAKU_RETU_KOUMOKU_CODE, ")
                .AppendLine("  soubi.SHISAKU_RETU_KOUMOKU_NAME, ")
                .AppendLine("  soubi.SHISAKU_RETU_KOUMOKU_NAME_DAI, ")
                .AppendLine("  soubi.SHISAKU_RETU_KOUMOKU_NAME_CHU ")
                .AppendLine("ORDER BY ")
                .AppendLine("SOUBISHIYOU.SHISAKU_SOUBI_KBN, ")
                .AppendLine("SOUBISHIYOU.SHISAKU_SOUBI_HYOUJI_JUN ")
            End With

            Dim param As New EditBlock2ExcelParamVo
            param.ShisakuEventCode = eventCode
            param.ShisakuBukaCode = bukaCode
            'param.ShisakuBlockNo = blockNo
            If isBase Then
                param.ShisakuBlockNoKaiteiNo = "  0"
            Else
                param.ShisakuBlockNoKaiteiNo = blockKaiteNo
            End If
            Return db.QueryForList(Of EditBlock2ExcelTitle2Vo)(sb.ToString, param)

        End Function
        ''' <summary>
        ''' 試作イベントメーモを返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNoList">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindWithTitle3NameBy(ByVal eventCode As String, _
                            ByVal bukaCode As String, _
                            ByVal blockNoList As List(Of String), _
                            ByVal blockKaiteNo As String) _
               As List(Of TShisakuSekkeiBlockMemoVo) _
               Implements EditBlock2ExcelDao.FindWithTitle3NameBy

            Dim sql As New StringBuilder
            With sql
                .AppendLine("SELECT  ")
                .AppendLine("  DISTINCT ")
                .AppendLine("  BLOCKMEMO.SHISAKU_MEMO_HYOUJI_JUN, ")
                .AppendLine("  BLOCKMEMO.SHISAKU_MEMO, ")
                .AppendLine("  BLOCKMEMO.SHISAKU_TEKIYOU , ")
                .AppendLine("  BLOCKMEMO.SHISAKU_GOUSYA ")
                .AppendLine("FROM ")
                .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL BLOCKINSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("INNER JOIN ")
                .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_MEMO BLOCKMEMO ")
                .AppendLine("ON ")
                .AppendLine("  BLOCKINSTL.SHISAKU_EVENT_CODE =BLOCKMEMO.SHISAKU_EVENT_CODE AND ")
                .AppendLine("  BLOCKINSTL.SHISAKU_BUKA_CODE =BLOCKMEMO.SHISAKU_BUKA_CODE AND ")
                .AppendLine("  BLOCKINSTL.SHISAKU_BLOCK_NO =BLOCKMEMO.SHISAKU_BLOCK_NO AND ")
                .AppendLine("  BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO =BLOCKMEMO.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("WHERE ")
                .AppendLine("  BLOCKINSTL.SHISAKU_EVENT_CODE = @ShisakuEventCode AND ")
                .AppendLine("  BLOCKINSTL.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                If blockNoList IsNot Nothing Then
                    If blockNoList.Count = 1 Then
                        .AppendFormat("  AND  BLOCKINSTL.SHISAKU_BLOCK_NO = '{0}'  ", blockNoList(0))
                    Else
                        Dim buf As New List(Of String)
                        For Each s As String In blockNoList
                            buf.Add("'" & s & "'")
                        Next
                        .AppendFormat("  AND  BLOCKINSTL.SHISAKU_BLOCK_NO IN ({0})  ", String.Join(",", buf.ToArray))
                    End If

                    If blockKaiteNo IsNot Nothing Then
                        .AppendLine("  AND  BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                    Else
                        .AppendLine(" AND BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = (")
                        .AppendLine(" SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))")
                        .AppendLine(" from  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB")
                        .AppendLine(" where(BLOCKINSTL.SHISAKU_EVENT_CODE = sb.SHISAKU_EVENT_CODE)")
                        .AppendLine(" and BLOCKINSTL.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE")
                        .AppendLine(" and BLOCKINSTL.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO)")
                    End If

                Else
                    .AppendLine("AND BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = (")
                    .AppendLine("SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))")
                    .AppendLine(" from  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB")
                    .AppendLine(" where(BLOCKINSTL.SHISAKU_EVENT_CODE = sb.SHISAKU_EVENT_CODE)")
                    .AppendLine(" and BLOCKINSTL.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE")
                    .AppendLine(" and BLOCKINSTL.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO)")
                End If




                'If Not String.IsNullOrEmpty(blockNoList) Then
                '    .AppendLine("  AND ")
                '    .AppendLine("  BLOCKINSTL.SHISAKU_BLOCK_NO = @ShisakuBlockNo AND ")
                '    .AppendLine("  BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                'Else
                '    .AppendLine("AND BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = (")
                '    .AppendLine("SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))")
                '    .AppendLine(" from  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB")
                '    .AppendLine(" where(BLOCKINSTL.SHISAKU_EVENT_CODE = sb.SHISAKU_EVENT_CODE)")
                '    .AppendLine(" and BLOCKINSTL.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE")
                '    .AppendLine(" and BLOCKINSTL.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO)")

                '    '.AppendLine(" AND ")
                '    '.AppendLine(" EXISTS ( ")
                '    '.AppendLine(" SELECT ")
                '    '.AppendLine(" BE.SHISAKU_BLOCK_NO ")
                '    '.AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) ")
                '    '.AppendLine(" WHERE ")
                '    '.AppendLine(" BE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                '    '.AppendLine(" AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                '    '.AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                '    '.AppendLine(" SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS ")
                '    '.AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO ")
                '    '.AppendLine(" FROM ")
                '    '.AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) ")
                '    '.AppendLine(" WHERE ")
                '    '.AppendLine(" SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                '    '.AppendLine(" AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                '    '.AppendLine(" AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ) ")
                '    '.AppendLine(" GROUP BY BE.SHISAKU_BLOCK_NO ")
                '    '.AppendLine(" )")
                'End If
                .AppendLine("  AND NOT ( BLOCKINSTL.INSU_SURYO IS NULL OR BLOCKINSTL.INSU_SURYO ='' ) ")
                .AppendLine("ORDER BY ")
                .AppendLine("  BLOCKMEMO.SHISAKU_MEMO_HYOUJI_JUN, ")
                .AppendLine("  BLOCKMEMO.SHISAKU_MEMO ")

            End With
            ''↑↑2014/08/07 Ⅰ.10.全ブロックEXCEL出力での出力内容変更_l) (TES)張 CHG AND
            Dim param As New EditBlock2ExcelParamVo
            param.ShisakuEventCode = eventCode
            param.ShisakuBukaCode = bukaCode
            'param.ShisakuBlockNo = blockNoList
            param.ShisakuBlockNoKaiteiNo = blockKaiteNo
            ''↓↓2014/08/07 Ⅰ.10.全ブロックEXCEL出力での出力内容変更_l) (TES)張 CHG BEGIN
            ''Return db.QueryForList(Of TShisakuSekkeiBlockMemoVo)(Sql, param)
            Return db.QueryForList(Of TShisakuSekkeiBlockMemoVo)(sql.ToString, param)
            ''↑↑2014/08/07 Ⅰ.10.全ブロックEXCEL出力での出力内容変更_l) (TES)張 CHG AND
        End Function

        ''' <summary>
        ''' 試作イベントINSTL品番を返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNoList">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <param name="isBase">ベースか</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindWithTitle4NameBy(ByVal eventCode As String, _
                                        ByVal bukaCode As String, _
                                        ByVal blockNoList As List(Of String), _
                                        ByVal blockKaiteNo As String, _
                                        ByVal isBase As Boolean) _
                                        As List(Of EditBlock2ExcelTitle4Vo) _
                                        Implements EditBlock2ExcelDao.FindWithTitle4NameBy
            Dim str As String = ""
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("  DISTINCT ")
                .AppendLine("  BLOCKINSTL.BASE_INSTL_FLG, ")
                .AppendLine("  BLOCKINSTL.INSTL_HINBAN_HYOUJI_JUN, ")
                .AppendLine("  BLOCKINSTL.INSTL_HINBAN, ")
                .AppendLine("  BLOCKINSTL.INSTL_HINBAN_KBN, ")
                .AppendLine("  BLOCKINSTL.SHISAKU_BLOCK_NO, ")
                .AppendLine("  BUHINEDIT.BUHIN_NOTE ")
                .AppendLine("FROM ")
                .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL BLOCKINSTL WITH (NOLOCK, NOWAIT) ")
                '※ベース情報判定を追加
                '全ブロックExcel出力フォーマットに併せて、BE.BUHIN_NOTE取得を追加。
                '但し、部品のベース情報は（INSTL）とDB構造が異なり、
                'BE/BEI.BLOCK_NO_KAITEI_NO=0ではなく、BE_BASE/BEI_BASE.BLOCK_NO_KAITEI_NO='000'となる。
                If Not isBase Then
                    .AppendLine("inner JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BUHINEDIT")
                Else
                    .AppendLine("inner JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE BUHINEDIT")
                End If

                .AppendLine("ON ")
                .AppendLine(" BUHINEDIT.SHISAKU_EVENT_CODE = BLOCKINSTL.SHISAKU_EVENT_CODE AND ")
                .AppendLine(" BUHINEDIT.SHISAKU_BUKA_CODE = BLOCKINSTL.SHISAKU_BUKA_CODE AND ")
                .AppendLine(" BUHINEDIT.SHISAKU_BLOCK_NO = BLOCKINSTL.SHISAKU_BLOCK_NO AND ")
                If Not isBase Then
                    .AppendLine(" BUHINEDIT.SHISAKU_BLOCK_NO_KAITEI_NO = BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO AND ")
                End If

                .AppendLine(" BUHINEDIT.BUHIN_NO = BLOCKINSTL.INSTL_HINBAN ")
                .AppendLine(" AND ")
                .AppendLine(" BUHINEDIT.BUHIN_NO_KBN = BLOCKINSTL.INSTL_HINBAN_KBN ")
                If Not isBase Then
                    .AppendLine("inner JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI")
                Else
                    .AppendLine("inner JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE BEI")
                End If
                .AppendLine("ON ")
                .AppendLine(" BEI.SHISAKU_EVENT_CODE = BLOCKINSTL.SHISAKU_EVENT_CODE AND ")
                .AppendLine(" BEI.SHISAKU_BUKA_CODE = BLOCKINSTL.SHISAKU_BUKA_CODE AND ")
                .AppendLine(" BEI.SHISAKU_BLOCK_NO = BLOCKINSTL.SHISAKU_BLOCK_NO AND ")
                If Not isBase Then
                    .AppendLine(" BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO AND ")
                Else
                    .AppendLine(" BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BUHINEDIT.SHISAKU_BLOCK_NO_KAITEI_NO AND ")
                End If
                .AppendLine(" BEI.INSTL_HINBAN_HYOUJI_JUN = BLOCKINSTL.INSTL_HINBAN_HYOUJI_JUN AND ")
                .AppendLine(" BEI.BUHIN_NO_HYOUJI_JUN  = BUHINEDIT.BUHIN_NO_HYOUJI_JUN  ")
                .AppendLine("WHERE ")
                .AppendLine("  BLOCKINSTL.SHISAKU_EVENT_CODE = @ShisakuEventCode AND ")
                .AppendLine("  BLOCKINSTL.SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")

                If blockNoList IsNot Nothing Then
                    If blockNoList.Count = 1 Then
                        .AppendLine("  AND ")
                        If isBase Then
                            'KAITEI_NOの条件を変更
                            .AppendLine("  BUHINEDIT.SHISAKU_BLOCK_NO_KAITEI_NO = '000' AND ")
                        End If
                        .AppendFormat("  BLOCKINSTL.SHISAKU_BLOCK_NO = '{0}' ", blockNoList(0))

                    Else
                        Dim buf As New List(Of String)
                        For Each s As String In blockNoList
                            buf.Add("'" & s & "'")
                        Next
                        .AppendFormat(" AND  BLOCKINSTL.SHISAKU_BLOCK_NO IN ({0}) ", String.Join(",", buf.ToArray))
                    End If

                    If blockKaiteNo IsNot Nothing Then
                        .AppendLine(" AND  BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                    Else
                        .AppendLine(" AND BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = (")
                        .AppendLine(" SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))")
                        .AppendLine(" from  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB")
                        .AppendLine(" where(BLOCKINSTL.SHISAKU_EVENT_CODE = sb.SHISAKU_EVENT_CODE)")
                        .AppendLine(" and BLOCKINSTL.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE")
                        .AppendLine(" and BLOCKINSTL.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO)")
                    End If
                Else
                    .AppendLine("AND BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = (")
                    .AppendLine("SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))")
                    .AppendLine(" from  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB")
                    .AppendLine(" where(BLOCKINSTL.SHISAKU_EVENT_CODE = sb.SHISAKU_EVENT_CODE)")
                    .AppendLine(" and BLOCKINSTL.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE")
                    .AppendLine(" and BLOCKINSTL.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO)")
                End If




                'If Not String.IsNullOrEmpty(blockNo) Then
                '    .AppendLine("  AND ")
                '    If isBase Then
                '        'KAITEI_NOの条件を変更
                '        .AppendLine("  BUHINEDIT.SHISAKU_BLOCK_NO_KAITEI_NO = '000' AND ")
                '    End If
                '    .AppendLine("  BLOCKINSTL.SHISAKU_BLOCK_NO = @ShisakuBlockNo AND ")
                '    .AppendLine("  BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                'Else

                '    .AppendLine("AND BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = (")
                '    .AppendLine("SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))")
                '    .AppendLine(" from  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB")
                '    .AppendLine(" where(BLOCKINSTL.SHISAKU_EVENT_CODE = sb.SHISAKU_EVENT_CODE)")
                '    .AppendLine(" and BLOCKINSTL.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE")
                '    .AppendLine(" and BLOCKINSTL.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO)")

                '    '    .AppendLine(" AND ")
                '    '    .AppendLine(" EXISTS ( ")
                '    '    .AppendLine(" SELECT ")
                '    '    .AppendLine(" BE.SHISAKU_BLOCK_NO ")
                '    '    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) ")
                '    '    .AppendLine(" WHERE ")
                '    '    .AppendLine(" BE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                '    '    .AppendLine(" AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                '    '    .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                '    '    .AppendLine(" SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS ")
                '    '    .AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO ")
                '    '    .AppendLine(" FROM ")
                '    '    .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) ")
                '    '    .AppendLine(" WHERE ")
                '    '    .AppendLine(" SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                '    '    .AppendLine(" AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                '    '    .AppendLine(" AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ) ")
                '    '    .AppendLine(" GROUP BY BE.SHISAKU_BLOCK_NO ")
                '    '    .AppendLine(" )")
                'End If
                .AppendLine("ORDER BY ")
                '該当イベント取得
                Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
                Dim eventVo As TShisakuEventVo
                eventVo = eventDao.FindByPk(eventCode)
                '該当イベントが「移管車改修」の場合
                .AppendLine("  BLOCKINSTL.SHISAKU_BLOCK_NO, ")
                If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                    .AppendLine("  BLOCKINSTL.BASE_INSTL_FLG DESC, ")
                End If
                .AppendLine("  BLOCKINSTL.INSTL_HINBAN_HYOUJI_JUN, ")
                .AppendLine("  BLOCKINSTL.INSTL_HINBAN, ")
                .AppendLine("  BLOCKINSTL.INSTL_HINBAN_KBN ")
            End With

            Dim param As New EditBlock2ExcelParamVo
            param.ShisakuEventCode = eventCode
            param.ShisakuBukaCode = bukaCode
            'param.ShisakuBlockNo = blockNo
            If isBase Then
                param.ShisakuBlockNoKaiteiNo = "  0"
            Else
                param.ShisakuBlockNoKaiteiNo = blockKaiteNo
            End If


            Return db.QueryForList(Of EditBlock2ExcelTitle4Vo)(sb.ToString, param)
        End Function


        ''' <summary>
        ''' 試作イベント全て号車を返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindAllGouSya(ByVal eventCode As String) As List(Of TShisakuEventBaseVo) Implements EditBlock2ExcelDao.FindAllGouSya

            Dim sql As String = "SELECT  " _
                     & "  DISTINCT * " _
                     & "  FROM " _
                     & "  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE WITH (NOLOCK, NOWAIT) " _
                     & "WHERE " _
                     & "  SHISAKU_EVENT_CODE = @ShisakuEventCode  " _
                     & "ORDER BY " _
                     & "HYOJIJUN_NO "

            Dim param As New TShisakuEventBaseVo
            param.ShisakuEventCode = eventCode
            Return db.QueryForList(Of TShisakuEventBaseVo)(sql, param)
        End Function

        ''' <summary>
        ''' 号車の試作イベントベース車項目No.対応のデータVoを返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNo">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <param name="strGousya">号車</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindWithTitle1BodyDataBy(ByVal eventCode As String, _
                             ByVal bukaCode As String, _
                             ByVal blockNo As String, _
                             ByVal blockKaiteNo As String, _
                             ByVal strGousya As String) _
                             As EditBlock2ExcelTitle1BodyVo _
               Implements EditBlock2ExcelDao.FindWithTitle1BodyDataBy


            Dim sql As String = "SELECT " _
                  & "   BI.BASE_KAIHATSU_FUGO, " _
                  & "   BI.BASE_SHIYOUJYOUHOU_NO, " _
                  & "   BI.BASE_APPLIED_NO, " _
                  & "   BI.BASE_KATASHIKI, " _
                  & "   BI.BASE_SHIMUKE, " _
                  & "   BI.BASE_OP, " _
                  & "   BI.BASE_GAISOUSYOKU, " _
                  & "   BI.BASE_NAISOUSYOKU, " _
                  & "   BI.SEISAKU_SYASYU, " _
                  & "   BI.SEISAKU_GRADE, " _
                  & "   BI.SEISAKU_SHIMUKE, " _
                  & "   BI.SEISAKU_HANDORU, " _
                  & "   BI.SEISAKU_EG_HAIKIRYOU, " _
                  & "   BI.SEISAKU_EG_KATASHIKI, " _
                  & "   BI.SEISAKU_EG_KAKYUUKI, " _
                  & "   BI.SEISAKU_TM_KUDOU, " _
                  & "   BI.SEISAKU_TM_HENSOKUKI, " _
                  & "   BI.SHISAKU_BASE_EVENT_CODE, " _
                  & "   BI.SHISAKU_BASE_GOUSYA, " _
                  & "   BI.SEISAKU_SYATAI_NO, " _
                  & "   B.BASE_KAIHATSU_FUGO AS TENKAI_BASE_KAIHATSU_FUGO , " _
                  & "   B.BASE_SHIYOUJYOUHOU_NO AS TENKAI_BASE_SHIYOUJYOUHOU_NO , " _
                  & "   B.BASE_APPLIED_NO AS TENKAI_BASE_APPLIED_NO , " _
                  & "   B.BASE_KATASHIKI AS TENKAI_BASE_KATASHIKI , " _
                  & "   B.BASE_SHIMUKE AS TENKAI_BASE_SHIMUKE, " _
                  & "   B.BASE_OP AS TENKAI_BASE_OP , " _
                  & "   B.BASE_GAISOUSYOKU AS TENKAI_BASE_GAISOUSYOKU , " _
                  & "   B.BASE_NAISOUSYOKU AS TENKAI_BASE_NAISOUSYOKU , " _
                  & "   B.SHISAKU_BASE_EVENT_CODE AS TENKAI_SHISAKU_BASE_EVENT_CODE , " _
                  & "   B.SHISAKU_BASE_GOUSYA AS TENKAI_SHISAKU_BASE_GOUSYA , " _
                  & "   K.SHISAKU_KATASHIKI, " _
                  & "   K.SHISAKU_SHIMUKE, " _
                  & "   K.SHISAKU_OP, " _
                  & "   K.SHISAKU_HANDORU, " _
                  & "   K.SHISAKU_SYAGATA, " _
                  & "   K.SHISAKU_GRADE, " _
                  & "   K.SHISAKU_SYADAI_NO, " _
                  & "   K.SHISAKU_GAISOUSYOKU, " _
                  & "   K.SHISAKU_NAISOUSYOKU, " _
                  & "   K.SHISAKU_GROUP, " _
                  & "   K.SHISAKU_KOUSHI_NO, " _
                  & "   K.SHISAKU_KANSEIBI, " _
                  & "   K.SHISAKU_EG_KATASHIKI, " _
                  & "   K.SHISAKU_EG_HAIKIRYOU, " _
                  & "   K.SHISAKU_EG_SYSTEM, " _
                  & "   K.SHISAKU_EG_KAKYUUKI, " _
                  & "   K.SHISAKU_TM_KUDOU, " _
                  & "   K.SHISAKU_TM_HENSOKUKI, " _
                  & "   K.SHISAKU_TM_FUKU_HENSOKUKI, " _
                  & "   K.SHISAKU_SIYOU_BUSYO, " _
                  & "   K.SHISAKU_SHIKEN_MOKUTEKI, " _
                  & "   K.SHISAKU_SHIMUKECHI_SHIMUKE, " _
                  & "   K.SHISAKU_EG_MEMO_1, " _
                  & "   K.SHISAKU_EG_MEMO_2, " _
                  & "   K.SHISAKU_TM_MEMO_1, " _
                  & "   K.SHISAKU_TM_MEMO_2, " _
                  & "   K.SHISAKU_GAISOUSYOKU_NAME, " _
                  & "   K.SHISAKU_NAISOUSYOKU_NAME, " _
                  & "   K.SHISAKU_SHIYOU_MOKUTEKI, " _
                  & "   K.SHISAKU_SEISAKU_JUNJYO, " _
                  & "   K.SHISAKU_SEISAKU_HOUHOU_KBN, " _
                  & "   K.SHISAKU_SEISAKU_HOUHOU, " _
                  & "   K.SHISAKU_MEMO " _
                  & "FROM " _
                  & "   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_SEISAKU_ICHIRAN BI WITH (NOLOCK, NOWAIT) " _
                  & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI K " _
                  & " ON K.SHISAKU_EVENT_CODE = BI.SHISAKU_EVENT_CODE " _
                  & " AND K.HYOJIJUN_NO = BI.HYOJIJUN_NO " _
                  & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B " _
                  & " ON B.SHISAKU_EVENT_CODE = BI.SHISAKU_EVENT_CODE " _
                  & " AND B.HYOJIJUN_NO = BI.HYOJIJUN_NO " _
                  & "WHERE " _
                  & "B.SHISAKU_EVENT_CODE = @ShisakuEventCode AND " _
                  & "B.SHISAKU_GOUSYA = @ShisakuGousya "

            

            Dim param As New EditBlock2ExcelParamVo
            param.ShisakuEventCode = eventCode
            param.ShisakuBukaCode = bukaCode
            param.ShisakuBlockNo = blockNo
            param.ShisakuBlockNoKaiteiNo = blockKaiteNo
            param.ShisakuGousya = strGousya

            Return db.QueryForObject(Of EditBlock2ExcelTitle1BodyVo)(sql, param)
        End Function

        ''' <summary>
        ''' 号車の試作イベント装備仕様のデータVoを返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNo">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <param name="strGousya">号車</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindWithTitle2BodyDataBy(ByVal eventCode As String, _
                             ByVal bukaCode As String, _
                             ByVal blockNo As String, _
                             ByVal blockKaiteNo As String, _
                             ByVal strGousya As String) _
                As List(Of EditBlock2ExcelTitle2BodyVo) _
                Implements EditBlock2ExcelDao.FindWithTitle2BodyDataBy

            '& "  AND NOT ( BLOCKINSTL.INSU_SURYO IS NULL OR BLOCKINSTL.INSU_SURYO ='' ) " _



            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT ")
                .AppendLine(" M_EVENTSOUBI.SHISAKU_SOUBI_KBN, ")
                .AppendLine(" M_EVENTSOUBI.SHISAKU_RETU_KOUMOKU_NAME, ")
                .AppendLine(" M_EVENTSOUBI.SHISAKU_RETU_KOUMOKU_NAME_DAI, ")
                .AppendLine(" M_EVENTSOUBI.SHISAKU_RETU_KOUMOKU_NAME_CHU, ")
                .AppendLine(" BLOCKSOUBISHIYOU.SHISAKU_RETU_KOUMOKU_CODE, ")
                .AppendLine(" EVENTSOUBI.SHISAKU_TEKIYOU ")
                .AppendLine(" FROM    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE BASE WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_SOUBI_SHIYOU BLOCKSOUBISHIYOU ")
                .AppendLine(" ON    BASE.SHISAKU_EVENT_CODE = BLOCKSOUBISHIYOU.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND    BLOCKSOUBISHIYOU.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" LEFT JOIN " & MBOM_DB_NAME & ".dbo.M_SHISAKU_SOUBI M_EVENTSOUBI ")
                .AppendLine(" ON    BLOCKSOUBISHIYOU.SHISAKU_SOUBI_KBN = M_EVENTSOUBI.SHISAKU_SOUBI_KBN ")
                .AppendLine(" AND    BLOCKSOUBISHIYOU.SHISAKU_RETU_KOUMOKU_CODE = M_EVENTSOUBI.SHISAKU_RETU_KOUMOKU_CODE ")
                .AppendLine(" LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI EVENTSOUBI ")
                .AppendLine(" ON    BASE.SHISAKU_EVENT_CODE = EVENTSOUBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND    BASE.HYOJIJUN_NO = EVENTSOUBI.HYOJIJUN_NO ")
                .AppendLine(" AND    BLOCKSOUBISHIYOU.SHISAKU_SOUBI_KBN = EVENTSOUBI.SHISAKU_SOUBI_KBN ")
                .AppendLine(" AND    BLOCKSOUBISHIYOU.SHISAKU_RETU_KOUMOKU_CODE = EVENTSOUBI.SHISAKU_RETU_KOUMOKU_CODE ")
                .AppendLine(" WHERE    BASE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND    BASE.SHISAKU_GOUSYA = @ShisakuGousya ")
            End With



            Dim param As New EditBlock2ExcelParamVo

            param.ShisakuEventCode = eventCode
            param.ShisakuBukaCode = bukaCode
            param.ShisakuBlockNo = blockNo
            param.ShisakuBlockNoKaiteiNo = blockKaiteNo
            param.ShisakuGousya = strGousya

            Return db.QueryForList(Of EditBlock2ExcelTitle2BodyVo)(sql.ToString, param)
        End Function

        ''' <summary>
        ''' 号車の試作イベントメモのデータVoを返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNoList">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <param name="strGousya">号車</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindWithTitle3BodyDataBy(ByVal eventCode As String, _
                            ByVal bukaCode As String, _
                            ByVal blockNoList As List(Of String), _
                             ByVal blockKaiteNo As String, _
                             ByVal strGousya As String) _
               As List(Of TShisakuSekkeiBlockMemoVo) _
               Implements EditBlock2ExcelDao.FindWithTitle3BodyDataBy

            Dim sql As New StringBuilder
            With sql
                .AppendLine("SELECT distinct")
                .AppendLine("   BLOCKMEMO.SHISAKU_GOUSYA, ")
                .AppendLine("   BLOCKMEMO.SHISAKU_MEMO_HYOUJI_JUN, ")
                .AppendLine("   BLOCKMEMO.SHISAKU_MEMO, ")
                .AppendLine("   BLOCKMEMO.SHISAKU_TEKIYOU ")
                .AppendLine("FROM ")
                .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_MEMO BLOCKMEMO WITH (NOLOCK, NOWAIT) ")
                .AppendLine("WHERE ")
                .AppendLine("   BLOCKMEMO.SHISAKU_EVENT_CODE = @ShisakuEventCode AND ")
                .AppendLine("   BLOCKMEMO.SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")

                If blockNoList IsNot Nothing Then
                    If blockNoList.Count = 1 Then
                        .AppendFormat("  AND  BLOCKMEMO.SHISAKU_BLOCK_NO = '{0}' ", blockNoList(0))
                    Else
                        Dim buf As New List(Of String)
                        For Each s As String In blockNoList
                            buf.Add("'" & s & "'")
                        Next
                        .AppendFormat("  AND  BLOCKMEMO.SHISAKU_BLOCK_NO IN ({0}) ", String.Join(",", buf.ToArray))
                    End If

                    If blockKaiteNo IsNot Nothing Then
                        .AppendLine("  AND  BLOCKMEMO.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo  AND")
                    Else
                        .AppendLine(" AND ")
                        .AppendLine(" EXISTS ( ")
                        .AppendLine(" SELECT ")
                        .AppendLine(" BE.SHISAKU_BLOCK_NO ")
                        .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) ")
                        .AppendLine(" WHERE ")
                        .AppendLine(" BE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                        .AppendLine(" AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                        .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                        .AppendLine(" SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'')) AS ")
                        .AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO ")
                        .AppendLine(" FROM ")
                        .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) ")
                        .AppendLine(" WHERE ")
                        .AppendLine(" SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                        .AppendLine(" AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                        .AppendLine(" AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ) ")
                        .AppendLine(" GROUP BY BE.SHISAKU_BLOCK_NO ")
                        .AppendLine(" ) AND")
                    End If
                Else
                    .AppendLine(" AND ")
                    .AppendLine(" EXISTS ( ")
                    .AppendLine(" SELECT ")
                    .AppendLine(" BE.SHISAKU_BLOCK_NO ")
                    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) ")
                    .AppendLine(" WHERE ")
                    .AppendLine(" BE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                    .AppendLine(" AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                    .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                    .AppendLine(" SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'')) AS ")
                    .AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO ")
                    .AppendLine(" FROM ")
                    .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) ")
                    .AppendLine(" WHERE ")
                    .AppendLine(" SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                    .AppendLine(" AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ) ")
                    .AppendLine(" GROUP BY BE.SHISAKU_BLOCK_NO ")
                    .AppendLine(" ) AND")
                End If

                'If Not String.IsNullOrEmpty(blockNoList) Then
                '    .AppendLine("  AND ")
                '    .AppendLine("  BLOCKMEMO.SHISAKU_BLOCK_NO = @ShisakuBlockNo AND ")
                '    .AppendLine("  BLOCKMEMO.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo  AND")
                'Else
                '    .AppendLine(" AND ")
                '    .AppendLine(" EXISTS ( ")
                '    .AppendLine(" SELECT ")
                '    .AppendLine(" BE.SHISAKU_BLOCK_NO ")
                '    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) ")
                '    .AppendLine(" WHERE ")
                '    .AppendLine(" BE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                '    .AppendLine(" AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                '    .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                '    .AppendLine(" SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'')) AS ")
                '    .AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO ")
                '    .AppendLine(" FROM ")
                '    .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) ")
                '    .AppendLine(" WHERE ")
                '    .AppendLine(" SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                '    .AppendLine(" AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                '    .AppendLine(" AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ) ")
                '    .AppendLine(" GROUP BY BE.SHISAKU_BLOCK_NO ")
                '    .AppendLine(" ) AND")
                'End If
                .AppendLine("   BLOCKMEMO.SHISAKU_GOUSYA = @ShisakuGousya ")
                .AppendLine("ORDER BY ")
                .AppendLine("   BLOCKMEMO.SHISAKU_GOUSYA, ")
                .AppendLine("   BLOCKMEMO.SHISAKU_MEMO_HYOUJI_JUN, ")
                .AppendLine("   BLOCKMEMO.SHISAKU_MEMO, ")
                .AppendLine("   BLOCKMEMO.SHISAKU_TEKIYOU ")
            End With
            ''↑↑2014/08/08 Ⅰ.10.全ブロックEXCEL出力での出力内容変更_p) (TES)張 CHG END

            Dim param As New EditBlock2ExcelParamVo
            param.ShisakuEventCode = eventCode
            param.ShisakuBukaCode = bukaCode
            'param.ShisakuBlockNo = blockNoList
            param.ShisakuBlockNoKaiteiNo = blockKaiteNo
            param.ShisakuGousya = strGousya

            Return db.QueryForList(Of TShisakuSekkeiBlockMemoVo)(sql.ToString, param)

        End Function

        ''' <summary>
        ''' 車の試作イベントINSTL品番のデータVoを返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNoList">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <param name="strGousya">号車</param>
        ''' <param name="isBase">ベースか？</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindWithTitle4BodyDataBy(ByVal eventCode As String, _
                             ByVal bukaCode As String, _
                             ByVal blockNoList As List(Of String), _
                             ByVal blockKaiteNo As String, _
                             ByVal strGousya As String, _
                             Optional ByVal isBase As Boolean = False) _
                             As List(Of TShisakuSekkeiBlockInstlVo) _
                             Implements EditBlock2ExcelDao.FindWithTitle4BodyDataBy


            Dim sql As New StringBuilder
            With sql
                .AppendLine("SELECT ")
                .AppendLine("   BLOCKINSTL.SHISAKU_GOUSYA, ")
                .AppendLine("   BLOCKINSTL.INSTL_HINBAN_HYOUJI_JUN, ")
                .AppendLine("   BLOCKINSTL.INSTL_HINBAN, ")
                .AppendLine("   BLOCKINSTL.INSTL_HINBAN_KBN, ")
                .AppendLine("   BLOCKINSTL.INSU_SURYO ")
                .AppendLine("   ,BLOCKINSTL.INSTL_DATA_KBN ")
                .AppendLine("   ,BLOCKINSTL.BASE_INSTL_FLG ")
                .AppendLine("FROM ")
                .AppendLine("   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL BLOCKINSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("WHERE ")
                .AppendLine("   BLOCKINSTL.SHISAKU_EVENT_CODE = @ShisakuEventCode AND ")
                .AppendLine("   BLOCKINSTL.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")

                If blockNoList IsNot Nothing Then
                    If blockNoList.Count = 1 Then
                        .AppendLine(" ")
                        .AppendFormat("  AND  BLOCKINSTL.SHISAKU_BLOCK_NO = '{0}' ", blockNoList(0))
                    Else
                        Dim buf As New List(Of String)
                        For Each s As String In blockNoList
                            buf.Add("'" & s & "'")
                        Next
                        .AppendFormat("  AND  BLOCKINSTL.SHISAKU_BLOCK_NO IN ({0}) ", String.Join(",", buf.ToArray))
                    End If

                    If blockKaiteNo IsNot Nothing Then
                        .AppendLine("  AND  BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo AND")
                    Else
                        .AppendLine(" AND ")
                        .AppendLine(" EXISTS ( ")
                        .AppendLine(" SELECT ")
                        .AppendLine(" BE.SHISAKU_BLOCK_NO ")
                        .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) ")
                        .AppendLine(" WHERE ")
                        .AppendLine(" BE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                        .AppendLine(" AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                        .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                        .AppendLine(" SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'')) AS ")
                        .AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO ")
                        .AppendLine(" FROM ")
                        .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) ")
                        .AppendLine(" WHERE ")
                        .AppendLine(" SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                        .AppendLine(" AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                        .AppendLine(" AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ) ")
                        .AppendLine(" GROUP BY BE.SHISAKU_BLOCK_NO ")
                        .AppendLine(" ) AND")

                        ''2015/06/22 追加 E.Ubukata
                        .AppendLine("   BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = (")
                        .AppendLine(" SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'')) AS ")
                        .AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO ")
                        .AppendLine(" FROM ")
                        .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) ")
                        .AppendLine(" WHERE ")
                        .AppendLine(" SHISAKU_EVENT_CODE = BLOCKINSTL.SHISAKU_EVENT_CODE ")
                        .AppendLine(" AND SHISAKU_BUKA_CODE = BLOCKINSTL.SHISAKU_BUKA_CODE ")
                        .AppendLine(" AND SHISAKU_BLOCK_NO = BLOCKINSTL.SHISAKU_BLOCK_NO  ) AND ")
                    End If
                Else
                    .AppendLine(" AND ")
                    .AppendLine(" EXISTS ( ")
                    .AppendLine(" SELECT ")
                    .AppendLine(" BE.SHISAKU_BLOCK_NO ")
                    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) ")
                    .AppendLine(" WHERE ")
                    .AppendLine(" BE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                    .AppendLine(" AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                    .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                    .AppendLine(" SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'')) AS ")
                    .AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO ")
                    .AppendLine(" FROM ")
                    .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) ")
                    .AppendLine(" WHERE ")
                    .AppendLine(" SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                    .AppendLine(" AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ) ")
                    .AppendLine(" GROUP BY BE.SHISAKU_BLOCK_NO ")
                    .AppendLine(" ) AND")

                    ''2015/06/22 追加 E.Ubukata
                    .AppendLine("   BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = (")
                    .AppendLine(" SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'')) AS ")
                    .AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO ")
                    .AppendLine(" FROM ")
                    .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) ")
                    .AppendLine(" WHERE ")
                    .AppendLine(" SHISAKU_EVENT_CODE = BLOCKINSTL.SHISAKU_EVENT_CODE ")
                    .AppendLine(" AND SHISAKU_BUKA_CODE = BLOCKINSTL.SHISAKU_BUKA_CODE ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO = BLOCKINSTL.SHISAKU_BLOCK_NO  ) AND ")
                End If



                'If Not String.IsNullOrEmpty(blockNo) Then
                '    .AppendLine("  AND ")
                '    .AppendLine("  BLOCKINSTL.SHISAKU_BLOCK_NO = @ShisakuBlockNo AND ")
                '    .AppendLine("  BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo AND")
                'Else
                '    .AppendLine(" AND ")
                '    .AppendLine(" EXISTS ( ")
                '    .AppendLine(" SELECT ")
                '    .AppendLine(" BE.SHISAKU_BLOCK_NO ")
                '    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) ")
                '    .AppendLine(" WHERE ")
                '    .AppendLine(" BE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                '    .AppendLine(" AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                '    .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                '    .AppendLine(" SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'')) AS ")
                '    .AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO ")
                '    .AppendLine(" FROM ")
                '    .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) ")
                '    .AppendLine(" WHERE ")
                '    .AppendLine(" SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                '    .AppendLine(" AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                '    .AppendLine(" AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ) ")
                '    .AppendLine(" GROUP BY BE.SHISAKU_BLOCK_NO ")
                '    .AppendLine(" ) AND")

                '    ''2015/06/22 追加 E.Ubukata
                '    .AppendLine("   BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = (")
                '    .AppendLine(" SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'')) AS ")
                '    .AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO ")
                '    .AppendLine(" FROM ")
                '    .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) ")
                '    .AppendLine(" WHERE ")
                '    .AppendLine(" SHISAKU_EVENT_CODE = BLOCKINSTL.SHISAKU_EVENT_CODE ")
                '    .AppendLine(" AND SHISAKU_BUKA_CODE = BLOCKINSTL.SHISAKU_BUKA_CODE ")
                '    .AppendLine(" AND SHISAKU_BLOCK_NO = BLOCKINSTL.SHISAKU_BLOCK_NO  ) AND ")
                'End If
                .AppendLine("   BLOCKINSTL.SHISAKU_GOUSYA = @ShisakuGousya ")


                .AppendLine("ORDER BY ")
                .AppendLine("BLOCKINSTL.SHISAKU_GOUSYA, ")
                .AppendLine("BLOCKINSTL.INSTL_HINBAN_HYOUJI_JUN ")
            End With
            ''↑↑2014/08/08 Ⅰ.10.全ブロックEXCEL出力での出力内容変更_q) (TES)張 CHG END


            Dim param As New EditBlock2ExcelParamVo
            param.ShisakuEventCode = eventCode
            param.ShisakuBukaCode = bukaCode
            'param.ShisakuBlockNo = blockNo
            If isBase Then
                param.ShisakuBlockNoKaiteiNo = "  0"
            Else
                param.ShisakuBlockNoKaiteiNo = blockKaiteNo
            End If


            param.ShisakuGousya = strGousya
            ''↓↓2014/08/08 Ⅰ.10.全ブロックEXCEL出力での出力内容変更_q) (TES)張 CHG BEGIN
            Return db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sql.ToString, param)
            ''↑↑2014/08/08 Ⅰ.10.全ブロックEXCEL出力での出力内容変更_q) (TES)張 CHG END
        End Function

        ''' <summary>
        ''' 社員名を返す
        ''' </summary>
        ''' <param name="shainId">社員ID</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByShainName(ByVal shainId As String) As String Implements EditBlock2ExcelDao.FindByShainName

            Dim sql As String = _
            " SELECT * " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0650 WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " SHAIN_NO = @ShainNo " _
            & " AND SITE_KBN = @SiteKbn "

            Dim db As New EBomDbClient
            Dim param As New Rhac0650Vo
            param.ShainNo = shainId
            'とりあえず１にしておく'
            param.SiteKbn = "1"
            Dim result As String = ""

            Dim shain As New Rhac0650Vo
            Dim shain2 As New Rhac2130Vo
            shain = db.QueryForObject(Of Rhac0650Vo)(sql, param)

            If shain Is Nothing Then
                Dim sql2 As String = _
                " SELECT * " _
                & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0650 WITH (NOLOCK, NOWAIT) " _
                & " WHERE " _
                & " SHAIN_NO = @ShainNo " _
                & " AND SITE_KBN = @SiteKbn "

                Dim db2 As New EBomDbClient
                Dim param2 As New Rhac2130Vo
                param.ShainNo = shainId
                'とりあえず１にしておく'
                param2.SiteKbn = "1"

                shain2 = db.QueryForObject(Of Rhac2130Vo)(sql2, param2)

                If Not shain2 Is Nothing Then
                    result = shain2.ShainName
                End If
            Else
                result = shain.ShainName
            End If

            Return result
        End Function

#Region "比較用"
        ''' <summary>
        ''' 試作イベントINSTL品番を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="ShisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="isBase">ベースか？</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByInstlHinbanCondition(ByVal shisakuEventCode As String, _
                                                   ByVal shisakuBukaCode As String, _
                                                   ByVal shisakuBlockNo As String, _
                                                   ByVal ShisakuBlockNoKaiteiNo As String, _
                                                   Optional ByVal isBase As Boolean = False) _
                                                   As List(Of TShisakuSekkeiBlockInstlVo) _
                                                   Implements EditBlock2ExcelDao.FindByInstlHinbanCondition


            Dim str As String = ""
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("  DISTINCT ")
                .AppendLine("  BLOCKINSTL.INSTL_HINBAN_HYOUJI_JUN, ")
                .AppendLine("  BLOCKINSTL.INSTL_HINBAN, ")
                .AppendLine("  BLOCKINSTL.INSTL_HINBAN_KBN, ")
                .AppendLine("  BLOCKINSTL.BASE_INSTL_FLG ")
                .AppendLine(" ,BLOCKINSTL.INSTL_DATA_KBN")
                .AppendLine("FROM ")
                .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL BLOCKINSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("WHERE ")
                .AppendLine("  BLOCKINSTL.SHISAKU_EVENT_CODE = @ShisakuEventCode AND ")
                .AppendLine("  BLOCKINSTL.SHISAKU_BUKA_CODE = @ShisakuBukaCode AND ")
                .AppendLine("  BLOCKINSTL.SHISAKU_BLOCK_NO = @ShisakuBlockNo AND ")
                .AppendLine("  BLOCKINSTL.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine("ORDER BY ")
                .AppendLine("  BLOCKINSTL.INSTL_HINBAN_HYOUJI_JUN, ")
                .AppendLine("  BLOCKINSTL.INSTL_HINBAN, ")
                .AppendLine("  BLOCKINSTL.INSTL_HINBAN_KBN ")
            End With

            Dim param As New EditBlock2ExcelParamVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            If isBase Then
                param.ShisakuBlockNoKaiteiNo = "  0"
            Else
                param.ShisakuBlockNoKaiteiNo = ShisakuBlockNoKaiteiNo
            End If



            Return db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sb.ToString, param)
        End Function

        ''' <summary>
        ''' 部品編集情報ベースを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditBaseCondition(ByVal shisakuEventCode As String, _
                                                     ByVal shisakuBukaCode As String, _
                                                     ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditBaseVo) Implements EditBlock2ExcelDao.FindByBuhinEditBaseCondtion

            Dim sql As New StringBuilder

            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT  ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE BEB ")
                .AppendLine(" WHERE ")
                .AppendLine(" BEB.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND BEB.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND BEB.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND BEB.SHISAKU_BLOCK_NO_KAITEI_NO = '000' ")
                .AppendLine(" ORDER BY BEB.BUHIN_NO_HYOUJI_JUN ")
            End With

            Dim param As New TShisakuBuhinEditBaseVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            Return db.QueryForList(Of TShisakuBuhinEditBaseVo)(sql.ToString, param)
        End Function

#End Region



#Region "Excel用"

        ''' <summary>
        ''' 部品編集情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns>部品編集情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEdit(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                              ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinEditVoHelperExcel) Implements EditBlock2ExcelDao.FindByBuhinEdit


            Dim sql As String = _
            " SELECT " _
            & " BE.*, " _
            & " BEI.INSTL_HINBAN_HYOUJI_JUN, " _
            & " BEI.INSU_SURYO, " _
            & " B.HYOJIJUN_NO, " _
            & " SB.USER_ID, " _
            & " SB.TEL_NO " _
            & " ,SBI.BASE_INSTL_FLG " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
            & " ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
            & " AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
            & " AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO " _
            & " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
            & " ON SB.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
            & " AND SB.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
            & " AND SB.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO " _
            & " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            & " ON SBI.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
            & " AND SBI.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
            & " AND SBI.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO " _
            & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN " _
            & " AND NOT ( SBI.INSU_SURYO IS NULL OR SBI.INSU_SURYO ='' ) " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B " _
            & " ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & " AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA " _
            & " WHERE " _
            & " BE.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND BE.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo "
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(shisakuEventCode)
            If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                sql = sql + " ORDER BY SBI.BASE_INSTL_FLG DESC,BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN "
            Else
                sql = sql + " ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN,B.HYOJIJUN_NO "
            End If


            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditVoHelperExcel

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo

            Return db.QueryForList(Of TShisakuBuhinEditVoHelperExcel)(sql, param)
        End Function

        ''' <summary>
        ''' 部品編集ベース情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns>部品編集情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditBase(ByVal shisakuEventCode As String, _
                                            ByVal shisakuBukaCode As String, _
                                            ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditVoHelperExcel) Implements EditBlock2ExcelDao.FindByBuhinEditBase

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine(" BE.*, ")
                .AppendLine(" BEI.INSTL_HINBAN_HYOUJI_JUN, ")
                .AppendLine(" BEI.INSU_SURYO, ")
                .AppendLine(" B.HYOJIJUN_NO, ")
                .AppendLine(" SB.USER_ID, ")
                .AppendLine(" SB.TEL_NO ")
                .AppendLine(" ,SBI.BASE_INSTL_FLG ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE BE WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE BEI ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ")
                .AppendLine(" ON SBI.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SBI.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                .AppendLine(" AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" AND NOT ( SBI.INSU_SURYO IS NULL OR SBI.INSU_SURYO ='' ) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B ")
                .AppendLine(" ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA ")
                .AppendLine(" WHERE ")
                .AppendLine(" BE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = '000' ")
                Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
                Dim eventVo As TShisakuEventVo
                eventVo = eventDao.FindByPk(shisakuEventCode)
                If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                    .AppendLine(" ORDER BY BE.BASE_BUHIN_FLG DESC,BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN")
                Else
                    .AppendLine(" ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ,B.HYOJIJUN_NO")
                End If

            End With

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditVoHelperExcel

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            Return db.QueryForList(Of TShisakuBuhinEditVoHelperExcel)(sb.ToString, param)
        End Function


        ''' <summary>
        ''' 部課コードに該当するブロックNoを取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <returns>ブロック情報</returns>
        ''' <remarks></remarks>
        Public Function FindBySekkeiBlockNo(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String) As List(Of TShisakuSekkeiBlockVo) Implements EditBlock2ExcelDao.FindBySekkeiBlockNo


            Dim sql As String = _
            "  SELECT  SB.SHISAKU_BLOCK_NO,  SB.SHISAKU_BLOCK_NO_KAITEI_NO   " _
            & " FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB WITH (NOLOCK, NOWAIT)   " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE  ON BE.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE   " _
            & " AND BE.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE  AND BE.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO   " _
            & " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO   " _
            & " INNER JOIN  " _
            & " ( " _
            & " SELECT SHISAKU_EVENT_CODE,SHISAKU_BUKA_CODE,SHISAKU_BLOCK_NO,MAX ( CONVERT ( VARCHAR,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO   " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) " _
            & " GROUP BY SHISAKU_EVENT_CODE,SHISAKU_BUKA_CODE,SHISAKU_BLOCK_NO " _
            & " ) SB2 ON  " _
            & " SB2.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE   " _
            & " AND SB2.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE   " _
            & " AND SB2.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO  " _
            & " AND SB2.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO  " _
            & " WHERE  SB.SHISAKU_EVENT_CODE = @ShisakuEventCode   " _
            & " AND SB.SHISAKU_BUKA_CODE = @ShisakuBukaCode   " _
            & " GROUP BY SB.SHISAKU_BLOCK_NO, SB.SHISAKU_BLOCK_NO_KAITEI_NO  " _
            & " ORDER BY SB.SHISAKU_BLOCK_NO, SB.SHISAKU_BLOCK_NO_KAITEI_NO "
            'Dim sql As String = _
            '" SELECT " _
            '& " SB.SHISAKU_BLOCK_NO, " _
            '& " SB.SHISAKU_BLOCK_NO_KAITEI_NO " _
            '& " FROM " _
            '& " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB WITH (NOLOCK, NOWAIT) " _
            '& " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE " _
            '& " ON BE.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE " _
            '& " AND BE.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE " _
            '& " AND BE.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO " _
            '& " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO " _
            '& " WHERE " _
            '& " SB.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            '& " AND SB.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            '& " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
            '& " SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) " _
            '& " WHERE SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE " _
            '& " AND SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE " _
            '& " AND SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ) " _
            '& " GROUP BY SB.SHISAKU_BLOCK_NO, SB.SHISAKU_BLOCK_NO_KAITEI_NO "


            Dim db As New EBomDbClient
            Dim param As New TShisakuSekkeiBlockVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode

            Return db.QueryForList(Of TShisakuSekkeiBlockVo)(sql, param)
        End Function

        ''' <summary>
        ''' 部課略名を取得
        ''' </summary>
        ''' <param name="syainId">イベントコード</param>
        ''' <returns>部課略名</returns>
        ''' <remarks></remarks>
        Public Function FindByKaRyakuName(ByVal syainId As String) As String Implements EditBlock2ExcelDao.FindByKaRyakuName

            Dim sql As String = _
            " SELECT * " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0650 WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " SHAIN_NO = @ShainNo " _
            & " AND SITE_KBN = @SiteKbn "

            Dim db As New EBomDbClient
            Dim param As New Rhac0650Vo
            param.ShainNo = syainId
            'とりあえず１にしておく'
            param.SiteKbn = "1"
            Dim KaryakuName As String = ""
            Dim result As String = ""

            Dim shain As New Rhac0650Vo
            Dim shain2 As New Rhac2130Vo
            shain = db.QueryForObject(Of Rhac0650Vo)(sql, param)

            If shain Is Nothing Then

                Dim sql2 As String = _
                " SELECT * " _
                & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0650 WITH (NOLOCK, NOWAIT) " _
                & " WHERE " _
                & " SHAIN_NO = @ShainNo " _
                & " AND SITE_KBN = @SiteKbn "

                Dim db2 As New EBomDbClient
                Dim param2 As New Rhac2130Vo
                param.ShainNo = syainId
                'とりあえず１にしておく'
                param2.SiteKbn = "1"

                shain2 = db.QueryForObject(Of Rhac2130Vo)(sql2, param2)

                If Not shain2 Is Nothing Then
                    result = shain2.BukaCode
                End If
            Else
                result = shain.BukaCode
            End If

            '部課略名を取得'
            Dim Bu_Code As String
            Dim Ka_Code As String
            Bu_Code = Left(result, 2)
            Ka_Code = Right(result, 2)

            Dim sql3 As String = _
            "SELECT " _
            & "   KA_RYAKU_NAME " _
            & "   FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 WITH (NOLOCK, NOWAIT) " _
            & "   WHERE" _
            & "   SITE_KBN = @SiteKbn " _
            & "   AND BU_CODE =@BuCode" _
            & "   AND KA_CODE =@KaCode"


            Dim db3 As New EBomDbClient
            Dim param3 As New Rhac1560Vo
            param3.BuCode = Bu_Code
            param3.KaCode = Ka_Code
            param3.SiteKbn = "1"
            Dim r1560 As New Rhac1560Vo
            r1560 = db3.QueryForObject(Of Rhac1560Vo)(sql3, param3)
            If Not r1560 Is Nothing Then
                Return r1560.KaRyakuName
            End If
            Return ""

        End Function

        ''' <summary>
        ''' イベント情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns>イベント情報</returns>
        ''' <remarks></remarks>
        Public Function FindByEvent(ByVal shisakuEventCode As String) As TShisakuEventVo Implements EditBlock2ExcelDao.FindByEvent

            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventVo
            param.ShisakuEventCode = shisakuEventCode

            Return db.QueryForObject(Of TShisakuEventVo)(sql, param)
        End Function
        ''↓↓2014/09/18 酒井 ADD BEGIN
        Public Function FindByAllBlockBuhinEdit(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String) As List(Of TShisakuBuhinEditVoHelperExcel) Implements EditBlock2ExcelDao.FindByAllBlockBuhinEdit
            Dim sql As String = _
                " SELECT " _
                & " BE.*, " _
                & " SB.USER_ID, " _
                & " SB.TEL_NO,  " _
                & " B.HYOJIJUN_NO, " _
                & " BEI.INSU_SURYO, " _
                & " BEI.INSTL_HINBAN_HYOUJI_JUN," _
                & " SBI.BASE_INSTL_FLG " _
                & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) " _
                & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
                & " ON SB.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
                & " AND SB.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
                & " AND SB.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO " _
                & " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO " _
                & " AND SB.BLOCK_FUYOU = '0' " _
                & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
                & " ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
                & " AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
                & " AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO " _
                & " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO " _
                & " AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN " _
                & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
                & " ON SBI.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
                & " AND SBI.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
                & " AND SBI.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO " _
                & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO " _
                & " AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN " _
                & " AND NOT ( SBI.INSU_SURYO IS NULL OR SBI.INSU_SURYO ='' ) " _
                & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B " _
                & " ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
                & " AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA " _
                & " WHERE " _
                & " BE.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & " AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
                & " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
                & " SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'')) AS " _
                & " SHISAKU_BLOCK_NO_KAITEI_NO " _
                & " FROM " _
                & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) " _
                & " WHERE " _
                & " SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
                & " AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
                & " AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ) "
            '            sql = sql + " ORDER BY BE.BASE_BUHIN_FLG DESC,BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN "
            sql = sql + " ORDER BY BE.SHISAKU_BLOCK_NO, SBI.BASE_INSTL_FLG DESC, BE.BUHIN_NO_HYOUJI_JUN "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditVoHelperExcel

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode

            Return db.QueryForList(Of TShisakuBuhinEditVoHelperExcel)(sql, param)

        End Function
        ''↑↑2014/09/18 酒井 ADD END

        ''' <summary>
        ''' 部課コードに該当する部品編集情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <returns>部品編集情報</returns>
        ''' <remarks></remarks>
        Public Function FindByAllBuhinEdit(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditVoHelperExcel) Implements EditBlock2ExcelDao.FindByAllBuhinEdit
            Dim sql As String = _
            " SELECT " _
            & " BE.*, " _
            & " SB.USER_ID, " _
            & " SB.TEL_NO,  " _
            & " B.HYOJIJUN_NO, " _
            & " BEI.INSU_SURYO, " _
            & " BEI.INSTL_HINBAN_HYOUJI_JUN," _
            & " SBI.BASE_INSTL_FLG, " _
            & " SBI.INSTL_DATA_KBN " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
            & " ON SB.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
            & " AND SB.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
            & " AND SB.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO " _
            & " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND SB.BLOCK_FUYOU = '0' " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
            & " ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
            & " AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
            & " AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO " _
            & " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            & " ON SBI.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
            & " AND SBI.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
            & " AND SBI.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO " _
            & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN " _
            & " AND NOT ( SBI.INSU_SURYO IS NULL OR SBI.INSU_SURYO ='' ) " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B " _
            & " ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & " AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA " _
            & " WHERE " _
            & " BE.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND BE.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
            & " SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'')) AS " _
            & " SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " FROM " _
            & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
            & " AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ) "

            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(shisakuEventCode)
            If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                sql = sql + " ORDER BY SBI.BASE_INSTL_FLG DESC,BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN "
                'sql = sql + " ORDER BY BE.BASE_BUHIN_FLG DESC,BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN "
            Else
                sql = sql + " ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN "
            End If

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditVoHelperExcel

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            Return db.QueryForList(Of TShisakuBuhinEditVoHelperExcel)(sql, param)
        End Function

        ''' <summary>
        ''' 号車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <returns>号車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByAllGousyaExcel(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As List(Of EditBlockExcelBlockInstlVoHelper) Implements EditBlock2ExcelDao.FindByAllGousyaExcel

            Dim sql As String = _
            " SELECT " _
            & " SBI.SHISAKU_BUKA_CODE, " _
            & " SBI.SHISAKU_BLOCK_NO, " _
            & " SBI.SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " SBI.SHISAKU_GOUSYA, " _
            & " SBI.INSTL_HINBAN, " _
            & " SBI.INSTL_HINBAN_KBN, " _
            & " SBI.BF_BUHIN_NO, " _
            & " SBI.SAISYU_KOUSHINBI, " _
            & " BEI.INSTL_HINBAN_HYOUJI_JUN, " _
            & " BEI.BUHIN_NO_HYOUJI_JUN, " _
            & " BEI.INSU_SURYO AS BUHIN_INSU_SURYO " _
            & " FROM " _
            & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) " _
            & " LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
            & " ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & " AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            & " AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO " _
            & " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND SB.BLOCK_FUYOU = '0' " _
            & " LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
            & " ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE " _
            & " AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE " _
            & " AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO " _
            & " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN " _
            & " AND NOT ( BEI.INSU_SURYO IS NULL OR BEI.INSU_SURYO ='' ) " _
            & " WHERE " _
            & " SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
            & " SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'')) AS " _
            & " SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " FROM " _
            & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            & " AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO  ) " _
            & " ORDER BY SBI.SHISAKU_BLOCK_NO, SBI.SHISAKU_GOUSYA, " _
            & " BEI.BUHIN_NO_HYOUJI_JUN "


            Dim db As New EBomDbClient
            Dim param As New TShisakuSekkeiBlockInstlVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            Return db.QueryForList(Of EditBlockExcelBlockInstlVoHelper)(sql, param)



        End Function

        ''' <summary>
        ''' 試作ベース車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBase(ByVal shisakuEventCode As String) As List(Of TShisakuEventBaseVo) Implements EditBlock2ExcelDao.FindByBase
            Dim sql As String = _
            "SELECT  " _
            & "  DISTINCT * " _
             & "  FROM " _
             & "  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE WITH (NOLOCK, NOWAIT) " _
             & "WHERE " _
             & "  SHISAKU_EVENT_CODE = @ShisakuEventCode  " _
             & "ORDER BY " _
             & "HYOJIJUN_NO "

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventBaseVo
            param.ShisakuEventCode = shisakuEventCode

            Return db.QueryForList(Of TShisakuEventBaseVo)(sql, param)
        End Function

        ''' <summary>
        ''' INSTL品番がどのテーブルに属するか返す
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>INSTL品番が属するテーブル名</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinTable(ByVal buhinNo As String) As String Implements EditBlock2ExcelDao.FindByBuhinTable
            Dim result As String = ""

            Dim db As New EBomDbClient

            Dim sql1 As String = _
            " SELECT * " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 " _
            & " WHERE " _
            & " BUHIN_NO_OYA = '" & buhinNo & "'"

            Dim r0552 As New Rhac0552Vo

            r0552 = db.QueryForObject(Of Rhac0552Vo)(sql1)

            If r0552 Is Nothing Then

                Dim sql2 As String = _
                " SELECT * " _
                & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 " _
                & " WHERE " _
                & " BUHIN_NO_OYA = '" & buhinNo & "'"

                Dim r0553 As New Rhac0553Vo

                r0553 = db.QueryForObject(Of Rhac0553Vo)(sql2)

                If r0553 Is Nothing Then

                    Dim sql3 As String = _
                " SELECT * " _
                & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 " _
                & " WHERE " _
                & " BUHIN_NO_OYA = '" & buhinNo & "'"

                    Dim r0551 As New Rhac0551Vo
                    r0551 = db.QueryForObject(Of Rhac0551Vo)(sql3)

                    If r0551 Is Nothing Then
                    Else
                        result = "R551"
                    End If

                Else
                    result = "R553"
                End If
            Else
                result = "R552"
            End If

            Return result
        End Function
        ''' <summary>
        ''' 部課コードに該当する部品編集情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <returns>部品編集情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBlockGroup(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String) As List(Of TShisakuBuhinEditVoHelperExcel) Implements EditBlock2ExcelDao.FindByBlockGroup
            Dim sql As String = _
            " SELECT " _
            & " BE.SHISAKU_BLOCK_NO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " BE.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
            & " SELECT MAX(COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'')) AS " _
            & " SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " FROM " _
            & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
            & " AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ) " _
            & " GROUP BY BE.SHISAKU_BLOCK_NO " _
            & " ORDER BY BE.SHISAKU_BLOCK_NO "


            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditVoHelperExcel

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode

            Return db.QueryForList(Of TShisakuBuhinEditVoHelperExcel)(sql, param)
        End Function

        ''' <summary>
        ''' 部課コードに該当する部品編集情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns>部品編集情報</returns>
        ''' <remarks></remarks>
        Public Function FindByAllBuhinEdit(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinEditVoHelperExcel) Implements EditBlock2ExcelDao.FindByAllBuhinEdit
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine("BE.*, ")
                .AppendLine("SB.USER_ID, ")
                .AppendLine("SB.TEL_NO,  ")
                .AppendLine("B.HYOJIJUN_NO, ")
                .AppendLine("BEI.INSU_SURYO ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) ")
                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine("ON SB.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine("AND SB.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine("AND SB.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine("AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("AND SB.BLOCK_FUYOU = '0' ")
                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                .AppendLine("ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine("AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine("AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine("AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ")
                .AppendLine("ON SBI.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                .AppendLine("AND SBI.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                .AppendLine("AND SBI.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                .AppendLine("AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine("AND NOT ( SBI.INSU_SURYO IS NULL OR SBI.INSU_SURYO ='' ) ")
                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B ")
                .AppendLine("ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine("AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA ")
                .AppendLine("WHERE ")
                .AppendLine("BE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine("AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine("AND BE.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine("AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNokaiteiNo ")
                ''↓↓2014/07/28 Ⅰ.3.設計編集 ベース改修専用化_af) (TES)張 CHG BEGIN
                '.AppendLine("ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN, B.HYOJIJUN_NO ")
                Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
                Dim eventVo As TShisakuEventVo
                eventVo = eventDao.FindByPk(shisakuEventCode)
                If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                    .AppendLine(" ORDER BY BE.BASE_BUHIN_FLG DESC,BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")
                Else
                    .AppendLine(" ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")
                End If
                ''↑↑2014/07/28 Ⅰ.3.設計編集 ベース改修専用化_af) (TES)張 CHG END
            End With


            'Dim sql As String = _
            '" SELECT " _
            '& " TOP 7500 BE.SHISAKU_EVENT_CODE,BE.SHISAKU_BUKA_CODE,BE.SHISAKU_BLOCK_NO,BE.SHISAKU_BLOCK_NO_KAITEI_NO,BE.BUHIN_NO_HYOUJI_JUN,BE.LEVEL,BE.SHUKEI_CODE,BE.SIA_SHUKEI_CODE,BE.GENCYO_CKD_KBN,BE.KYOUKU_SECTION,BE.MAKER_CODE,BE.MAKER_NAME,BE.BUHIN_NO,BE.BUHIN_NO_KBN,BE.BUHIN_NO_KAITEI_NO,BE.EDA_BAN,BE.BUHIN_NAME,BE.SAISHIYOUFUKA,BE.SHUTUZU_YOTEI_DATE,BE.ZAISHITU_KIKAKU_1,BE.ZAISHITU_KIKAKU_2,BE.ZAISHITU_KIKAKU_3,BE.ZAISHITU_MEKKI,BE.SHISAKU_BANKO_SURYO,BE.SHISAKU_BANKO_SURYO_U,BE.SHISAKU_BUHIN_HI,BE.SHISAKU_KATA_HI,BE.BIKOU,BE.EDIT_TOUROKUBI,BE.EDIT_TOUROKUJIKAN,BE.KAITEI_HANDAN_FLG,BE.SHISAKU_LIST_CODE,BE.CREATED_USER_ID,BE.CREATED_DATE,BE.CREATED_TIME,BE.UPDATED_USER_ID,BE.UPDATED_DATE,BE.UPDATED_TIME,   " _
            '& " SB.USER_ID,  SB.TEL_NO,   B.HYOJIJUN_NO,  BEI.INSU_SURYO   " _
            '& "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT)  " _
            '& "INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB   " _
            '& "ON SB.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE  AND  " _
            '& "SB.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE  AND  " _
            '& "SB.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  AND  " _
            '& "SB.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO  AND  " _
            '& "SB.BLOCK_FUYOU = '0'   " _
            '& "INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI   " _
            '& "ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE   " _
            '& "AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE   " _
            '& "AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO   " _
            '& "AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO   " _
            '& "AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN   " _
            '& "INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI   " _
            '& "ON SBI.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE   " _
            '& "AND SBI.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE   " _
            '& "AND SBI.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO   " _
            '& "AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO   " _
            '& "AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN  AND NOT ( SBI.INSU_SURYO IS NULL OR SBI.INSU_SURYO ='' )   " _
            '& "INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B  ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE   " _
            '& "AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA  INNER JOIN   " _
            '& "( SELECT SHISAKU_EVENT_CODE,SHISAKU_BUKA_CODE,SHISAKU_BLOCK_NO  " _
            '& ", MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT  " _
            '& "GROUP BY SHISAKU_EVENT_CODE,SHISAKU_BUKA_CODE,SHISAKU_BLOCK_NO " _
            '& ")  BE2 ON  BE2.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE  AND BE2.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE  AND BE2.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO    " _
            '& " AND BE2.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO     " _
            '& "WHERE  BE.SHISAKU_EVENT_CODE = @ShisakuEventCode  AND BE.SHISAKU_BUKA_CODE =@ShisakuBukaCode   " _
            '& " ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN  "


            'Dim sql As String = _
            '" SELECT " _
            '& " BE.*, " _
            '& " SB.USER_ID, " _
            '& " SB.TEL_NO,  " _
            '& " B.HYOJIJUN_NO, " _
            '& " BEI.INSU_SURYO " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) " _
            '& " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
            '& " ON SB.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
            '& " AND SB.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
            '& " AND SB.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO " _
            '& " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO " _
            '& " AND SB.BLOCK_FUYOU = '0' " _
            '& " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
            '& " ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
            '& " AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
            '& " AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO " _
            '& " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO " _
            '& " AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN " _
            '& " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            '& " ON SBI.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
            '& " AND SBI.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
            '& " AND SBI.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO " _
            '& " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            '& " AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN " _
            '& " AND NOT ( SBI.INSU_SURYO IS NULL OR SBI.INSU_SURYO ='' ) " _
            '& " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B " _
            '& " ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            '& " AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA " _
            '& " WHERE " _
            '& " BE.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            '& " AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            '& " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
            '& " SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS " _
            '& " SHISAKU_BLOCK_NO_KAITEI_NO " _
            '& " FROM " _
            '& " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) " _
            '& " WHERE " _
            '& " SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
            '& " AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
            '& " AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ) " _
            '& " ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN "
            '            " SELECT " _
            '& " BE.*, " _
            '& " BEI.INSTL_HINBAN_HYOUJI_JUN, " _
            '& " SB.USER_ID, " _
            '& " SB.TEL_NO " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE " _
            '& " LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
            '& " ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
            '& " AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
            '& " AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO " _
            '& " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO " _
            '& " AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN " _
            '& " LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
            '& " ON SB.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
            '& " AND SB.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
            '& " AND SB.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO " _
            '& " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            '& " AND SB.BLOCK_FUYOU = '0' " _
            '& " WHERE " _
            '& " BE.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            '& " AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            '& " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _            '& " SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS " _
            '& " SHISAKU_BLOCK_NO_KAITEI_NO " _
            '& " FROM " _
            '& " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT " _
            '& " WHERE " _
            '& " SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
            '& " AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
            '& " AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ) " _
            '& " ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN "


            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditVoHelperExcel

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
            Return db.QueryForList(Of TShisakuBuhinEditVoHelperExcel)(sb.ToString, param)
        End Function

        ''' <summary>
        ''' 号車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <returns>号車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByAllGousyaExcel(ByVal shisakuEventCode As String, _
                                             ByVal shisakuBukaCode As String, _
                                             ByVal shisakuBlockNo As String, _
                                             ByVal shisakuBlockNoKaiteiNo As String) As List(Of EditBlockExcelBlockInstlVoHelper) Implements EditBlock2ExcelDao.FindByAllGousyaExcel
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine("SBI.SHISAKU_BUKA_CODE, ")
                .AppendLine("SBI.SHISAKU_BLOCK_NO, ")
                .AppendLine("SBI.SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine("SBI.SHISAKU_GOUSYA, ")
                .AppendLine("SBI.INSTL_HINBAN, ")
                .AppendLine("SBI.INSTL_HINBAN_KBN, ")
                .AppendLine("SBI.BF_BUHIN_NO, ")
                .AppendLine("SBI.SAISYU_KOUSHINBI, ")
                .AppendLine("BEI.INSTL_HINBAN_HYOUJI_JUN, ")
                .AppendLine("BEI.BUHIN_NO_HYOUJI_JUN, ")
                .AppendLine("BEI.INSU_SURYO AS BUHIN_INSU_SURYO ")
                .AppendLine("FROM ")
                .AppendLine("" & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) ")
                .AppendLine("LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine("ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine("AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine("AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                .AppendLine("AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("AND SB.BLOCK_FUYOU = '0' ")
                .AppendLine("LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                .AppendLine("ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine("AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine("AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ")
                .AppendLine("AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine("AND NOT ( BEI.INSU_SURYO IS NULL OR BEI.INSU_SURYO ='' ) ")
                .AppendLine("WHERE ")
                .AppendLine("SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine("AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine("AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine("AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine("ORDER BY SBI.SHISAKU_BLOCK_NO, SBI.SHISAKU_GOUSYA, ")
                .AppendLine("BEI.BUHIN_NO_HYOUJI_JUN ")
            End With


            'Dim sql1 As String = _
            '" " _
            '& " SELECT TOP 7500 BE.*,  SB.USER_ID,  SB.TEL_NO,   B.HYOJIJUN_NO,  BEI.INSU_SURYO   " _
            '& "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT)   " _
            '& "INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB  ON SB.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE  " _
            '& " AND SB.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE  AND SB.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO  AND SB.BLOCK_FUYOU = '0'  INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI  ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE  AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE  AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO  AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN  INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI  ON SBI.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE  AND SBI.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE  AND SBI.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO  AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO  AND  " _
            '& "SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN  AND NOT ( SBI.INSU_SURYO IS NULL OR SBI.INSU_SURYO ='' )  " _
            '& " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B  ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE   " _
            '& "AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA   " _
            '& "INNER JOIN  " _
            '& "( " _
            '& "SELECT SHISAKU_EVENT_CODE,SHISAKU_BUKA_CODE,SHISAKU_BLOCK_NO,MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS  SHISAKU_BLOCK_NO_KAITEI_NO FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT " _
            '& "GROUP BY SHISAKU_EVENT_CODE,SHISAKU_BUKA_CODE,SHISAKU_BLOCK_NO " _
            '& ") BE2 ON  " _
            '& "BE2.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE  AND  " _
            '& "BE2.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE  AND  " _
            '& "BE2.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  AND   " _
            '& "BE2.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO  " _
            '& "WHERE   " _
            '& "BE.SHISAKU_EVENT_CODE = @ShisakuEventCode  AND  " _
            '& "BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            '& "ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN  "

            'Dim sql As String = _
            '" SELECT " _
            '& " SBI.SHISAKU_BUKA_CODE, " _
            '& " SBI.SHISAKU_BLOCK_NO, " _
            '& " SBI.SHISAKU_BLOCK_NO_KAITEI_NO, " _
            '& " SBI.SHISAKU_GOUSYA, " _
            '& " SBI.INSTL_HINBAN, " _
            '& " SBI.INSTL_HINBAN_KBN, " _
            '& " SBI.BF_BUHIN_NO, " _
            '& " SBI.SAISYU_KOUSHINBI, " _
            '& " BEI.INSTL_HINBAN_HYOUJI_JUN, " _
            '& " BEI.BUHIN_NO_HYOUJI_JUN, " _
            '& " BEI.INSU_SURYO AS BUHIN_INSU_SURYO " _
            '& " FROM " _
            '& " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) " _
            '& " LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
            '& " ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            '& " AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            '& " AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO " _
            '& " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            '& " AND SB.BLOCK_FUYOU = '0' " _
            '& " LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
            '& " ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE " _
            '& " AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE " _
            '& " AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO " _
            '& " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO " _
            '& " AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN " _
            '& " AND NOT ( BEI.INSU_SURYO IS NULL OR BEI.INSU_SURYO ='' ) " _
            '& " WHERE " _
            '& " SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            '& " AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            '& " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
            '& " SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS " _
            '& " SHISAKU_BLOCK_NO_KAITEI_NO " _
            '& " FROM " _
            '& " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) " _
            '& " WHERE " _
            '& " SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            '& " AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            '& " AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO  ) " _
            '& " ORDER BY SBI.SHISAKU_BLOCK_NO, SBI.SHISAKU_GOUSYA, " _
            '& " BEI.BUHIN_NO_HYOUJI_JUN "

            Dim db As New EBomDbClient
            Dim param As New TShisakuSekkeiBlockInstlVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo

            Return db.QueryForList(Of EditBlockExcelBlockInstlVoHelper)(sb.ToString, param)
        End Function

        ''' <summary>
        ''' 部品編集情報を取得する(ベース情報)
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByAllBuhinEditBase(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditVoHelperExcel) Implements EditBlock2ExcelDao.FindByAllBuhinEditBase
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine("BE.*, ")
                .AppendLine("SB.USER_ID, ")
                .AppendLine("SB.TEL_NO,  ")
                .AppendLine("B.HYOJIJUN_NO, ")
                .AppendLine("BEI.INSU_SURYO, ")
                .AppendLine("BEI.INSTL_HINBAN_HYOUJI_JUN,")
                .AppendLine("SBI.BASE_INSTL_FLG, ")
                .AppendLine("SBI.INSTL_DATA_KBN ")

                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE BE WITH (NOLOCK, NOWAIT) ")
                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine("ON SB.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine("AND SB.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine("AND SB.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine("AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("AND SB.BLOCK_FUYOU = '0' ")
                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE BEI ")
                .AppendLine("ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine("AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine("AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine("AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ")
                .AppendLine("ON SBI.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                .AppendLine("AND SBI.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                .AppendLine("AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                .AppendLine("AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine("AND NOT ( SBI.INSU_SURYO IS NULL OR SBI.INSU_SURYO ='' ) ")
                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B ")
                .AppendLine("ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine("AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA ")
                .AppendLine("WHERE ")
                .AppendLine("BE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine("AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine("AND BE.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine("AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = '000' ")
                ''↓↓2014/07/28 Ⅰ.3.設計編集 ベース改修専用化_ag) (TES)張 CHG BEGIN
                '.AppendLine("ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")
                Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
                Dim eventVo As TShisakuEventVo
                eventVo = eventDao.FindByPk(shisakuEventCode)
                If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                    .AppendLine(" ORDER BY SBI.BASE_INSTL_FLG DESC,BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")
                Else
                    .AppendLine(" ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")
                End If
                ''↑↑2014/07/28 Ⅰ.3.設計編集 ベース改修専用化_ag) (TES)張 CHG END
            End With
            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditVoHelperExcel

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            Return db.QueryForList(Of TShisakuBuhinEditVoHelperExcel)(sb.ToString, param)
        End Function

        ''' <summary>
        ''' 号車情報を取得(ベース情報)
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByAllGousyaExcelBase(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As List(Of EditBlockExcelBlockInstlVoHelper) Implements EditBlock2ExcelDao.FindByAllGousyaExcelBase
            Dim sb As New StringBuilder

            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine("SBI.SHISAKU_BUKA_CODE, ")
                .AppendLine("SBI.SHISAKU_BLOCK_NO, ")
                .AppendLine("SBI.SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine("SBI.SHISAKU_GOUSYA, ")
                .AppendLine("SBI.INSTL_HINBAN, ")
                .AppendLine("SBI.INSTL_HINBAN_KBN, ")
                .AppendLine("SBI.BF_BUHIN_NO, ")
                .AppendLine("SBI.SAISYU_KOUSHINBI, ")
                .AppendLine("BEI.INSTL_HINBAN_HYOUJI_JUN, ")
                .AppendLine("BEI.BUHIN_NO_HYOUJI_JUN, ")
                .AppendLine("BEI.INSU_SURYO AS BUHIN_INSU_SURYO ")
                .AppendLine("FROM ")
                .AppendLine("" & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) ")
                .AppendLine("LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine("ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine("AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine("AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                .AppendLine("AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                .AppendLine("AND SB.BLOCK_FUYOU = '0' ")
                .AppendLine("LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE BEI ")
                .AppendLine("ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine("AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine("AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ")
                .AppendLine("AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = '000' ")
                .AppendLine("AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN ")
                '.AppendLine("AND NOT ( BEI.INSU_SURYO IS NULL OR BEI.INSU_SURYO ='' ) ")
                .AppendLine("WHERE ")
                .AppendLine("SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine("AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine("AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine("AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                .AppendLine("ORDER BY SBI.SHISAKU_BLOCK_NO, SBI.SHISAKU_GOUSYA, ")
                .AppendLine("BEI.BUHIN_NO_HYOUJI_JUN ")
            End With

            Dim db As New EBomDbClient
            Dim param As New TShisakuSekkeiBlockInstlVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            Return db.QueryForList(Of EditBlockExcelBlockInstlVoHelper)(sb.ToString, param)
        End Function
        ''' <summary>
        ''' ブロックが新規作成されたかどうかを返す
        ''' 設計ブロックINSTLにあるかどうか
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <returns>True:新規 False:既存</returns>
        ''' <remarks>ブロック一覧の「指定ブロックExcel出力」の履歴プルダウンに「ベース」を出力するかどうかの判定に利用</remarks>
        Public Function IsNewCreatedBlock(ByVal eventCode As String, _
                            ByVal bukaCode As String, _
                            ByVal blockNo As String) As Boolean Implements EditBlock2ExcelDao.IsNewCreatedBlock
            Dim sb As New StringBuilder

            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine(" SHISAKU_BLOCK_NO ")
                .AppendLine("FROM ")
                .AppendLine("" & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("WHERE ")
                .AppendLine("SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine("AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine("AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine("AND SHISAKU_BLOCK_NO_KAITEI_NO = '  0'")
            End With

            Dim db As New EBomDbClient
            Dim param As New TShisakuSekkeiBlockInstlVo

            param.ShisakuEventCode = eventCode
            param.ShisakuBukaCode = bukaCode
            param.ShisakuBlockNo = blockNo

            Dim sbiVos As List(Of TShisakuSekkeiBlockInstlVo) = db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sb.ToString, param)
            If sbiVos.Count > 0 Then
                Return False
            Else
                Return True
            End If

        End Function
#End Region

    End Class

End Namespace

