Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.TehaichoMenu.Dao
Imports EventSakusei.TehaichoMenu.Vo
Imports EBom.Data
Imports EBom.Common
Imports System.Text
Imports EventSakusei.ShisakuBuhinKaiteiBlock.Dao

Namespace TehaichoMenu.Impl
    Public Class KaiteiChusyutuDaoImpl : Inherits DaoEachFeatureImpl
        Implements KaiteiChusyutuDao
        '2012/02/22 UpdateByShisakuEvent追加

        Public Function FindByBuhinEditBase(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuBuhinEditVoSekkeiHelper) Implements KaiteiChusyutuDao.FindByBuhinEditBase
            Dim db As New EBomDbClient

            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT TG.SHISAKU_GOUSYA AS SHISAKU_GOUSYA, TG.SHISAKU_GOUSYA_HYOUJI_JUN AS HYOJIJUN_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TG.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE = '{0}'", shisakuListCode)
                .AppendLine(" GROUP BY TG.SHISAKU_GOUSYA,  TG.SHISAKU_GOUSYA_HYOUJI_JUN ")
                .AppendLine(" ORDER BY  TG.SHISAKU_GOUSYA_HYOUJI_JUN,TG.SHISAKU_GOUSYA ")
            End With
            'Dim sql As String = _
            '" SELECT TG.SHISAKU_GOUSYA AS SHISAKU_GOUSYA, TG.SHISAKU_GOUSYA_HYOUJI_JUN AS HYOJIJUN_NO " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG  WITH (NOLOCK, NOWAIT) " _
            '& " WHERE " _
            '& " TG.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "'" _
            '& " AND TG.SHISAKU_LIST_CODE = '" & shisakuListCode & "'" _
            '& " GROUP BY TG.SHISAKU_GOUSYA,  TG.SHISAKU_GOUSYA_HYOUJI_JUN " _
            '& " ORDER BY  TG.SHISAKU_GOUSYA_HYOUJI_JUN,TG.SHISAKU_GOUSYA "

            Dim hshGosha As New Hashtable
            Dim goshaList As New List(Of String)
            For Each baseVo As TShisakuEventBaseVo In db.QueryForList(Of TShisakuEventBaseVo)(sql.ToString)
                goshaList.Add(String.Format("'{0}'", baseVo.ShisakuGousya))
                If Not hshGosha.Contains(baseVo.ShisakuGousya) Then
                    hshGosha.Add(baseVo.ShisakuGousya, baseVo.HyojijunNo)
                End If
            Next
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT BE.*, BEI.INSU_SURYO, SBI.SHISAKU_GOUSYA, SB.BLOCK_FUYOU ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = '000' ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE BEI  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE BE  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BE.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BE.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SBI.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND SBI.SHISAKU_GOUSYA IN ({0})", String.Join(",", goshaList.ToArray))
                .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")
                .AppendLine(" AND SBI.INSU_SURYO > 0 ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                .AppendLine(" ORDER BY SBI.SHISAKU_GOUSYA, BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")
            End With

            Dim RList As List(Of TShisakuBuhinEditVoSekkeiHelper) = db.QueryForList(Of TShisakuBuhinEditVoSekkeiHelper)(sql.ToString)
            For Each resultVo As TShisakuBuhinEditVoSekkeiHelper In RList
                resultVo.ShisakuGousyaHyoujiJun = hshGosha.Item(resultVo.ShisakuGousya)
            Next

            Return RList

        End Function

        ''' <summary>
        ''' 最新の部品編集情報を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <returns>最新の部品編集情報</returns>
        ''' <remarks></remarks>
        Public Function FindByNewBuhinEditList(ByVal eventcode As String, ByVal shisakuListCode As String, _
                                               ByVal shouninNichiji As Long, Optional ByVal isBase As Boolean = False, _
                                               Optional ByVal isIkansha As Boolean = False) As System.Collections.Generic.List(Of TShisakuBuhinEditVoSekkeiHelper) Implements Dao.KaiteiChusyutuDao.FindByNewBuhinEditList

            Dim db As New EBomDbClient


            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT TG.SHISAKU_GOUSYA AS SHISAKU_GOUSYA, TG.SHISAKU_GOUSYA_HYOUJI_JUN AS HYOJIJUN_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TG.SHISAKU_EVENT_CODE = '{0}'", eventcode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE = '{0}'", shisakuListCode)
                .AppendLine(" GROUP BY TG.SHISAKU_GOUSYA,  TG.SHISAKU_GOUSYA_HYOUJI_JUN ")
                .AppendLine(" ORDER BY  TG.SHISAKU_GOUSYA_HYOUJI_JUN,TG.SHISAKU_GOUSYA ")

            End With

            Dim hshGosha As New Hashtable
            Dim goshaList As New List(Of String)
            For Each baseVo As TShisakuEventBaseVo In db.QueryForList(Of TShisakuEventBaseVo)(sql.ToString)
                goshaList.Add(String.Format("'{0}'", baseVo.ShisakuGousya))
                If Not hshGosha.Contains(baseVo.ShisakuGousya) Then
                    hshGosha.Add(baseVo.ShisakuGousya, baseVo.HyojijunNo)
                End If
            Next


            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT BE.*, BEI.INSU_SURYO, SBI.SHISAKU_GOUSYA, SB.BLOCK_FUYOU ")
                .AppendFormat(" FROM {0}.dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI  WITH (NOLOCK, NOWAIT) ", MBOM_DB_NAME)
                .AppendFormat(" INNER JOIN {0}.dbo.T_SHISAKU_SEKKEI_BLOCK SB ", MBOM_DB_NAME)
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")

                If isBase Then
                    .AppendLine("AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO")
                Else
                    .AppendLine("AND ( NOT SB.SHISAKU_BLOCK_NO_KAITEI_NO = '000' AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO)")
                End If

                .AppendFormat(" INNER JOIN {0}.dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI  WITH (NOLOCK, NOWAIT) ", MBOM_DB_NAME)
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendFormat(" INNER JOIN {0}.dbo.T_SHISAKU_BUHIN_EDIT BE  WITH (NOLOCK, NOWAIT) ", MBOM_DB_NAME)
                .AppendLine(" ON BE.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BE.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SBI.SHISAKU_EVENT_CODE = '{0}'", eventcode)
                .AppendFormat(" AND SBI.SHISAKU_GOUSYA IN ({0})", String.Join(",", goshaList.ToArray))
                .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")
                .AppendLine(" AND SBI.INSU_SURYO > 0 ")

                If isIkansha Then
                    .AppendLine(" AND SBI.BASE_INSTL_FLG <> '1' ")
                End If

                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendFormat(" FROM {0}.dbo.T_SHISAKU_SEKKEI_BLOCK  WITH (NOLOCK, NOWAIT) ", MBOM_DB_NAME)
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND KACHOU_SYOUNIN_JYOUTAI = '36' ")
                .AppendFormat(" AND CAST(KACHOU_SYOUNIN_HI AS BIGINT) * 1000000 + CAST(KACHOU_SYOUNIN_JIKAN AS BIGINT) <= {0}) ", shouninNichiji)
                .AppendLine(" ORDER BY SBI.SHISAKU_GOUSYA, BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")

            End With
            Dim RList As List(Of TShisakuBuhinEditVoSekkeiHelper) = db.QueryForList(Of TShisakuBuhinEditVoSekkeiHelper)(sql.ToString)
            For Each resultVo As TShisakuBuhinEditVoSekkeiHelper In RList
                resultVo.ShisakuGousyaHyoujiJun = hshGosha.Item(resultVo.ShisakuGousya)
            Next

            Return RList
        End Function


        ''' <summary>
        ''' 最新の部品編集情報を取得する（課長承認なし）
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <returns>最新の部品編集情報</returns>
        ''' <remarks></remarks>
        Public Function FindByNewBuhinEditListSaishin(ByVal eventcode As String, ByVal shisakuListCode As String, Optional ByVal isBase As Boolean = False) As System.Collections.Generic.List(Of TShisakuBuhinEditVoSekkeiHelper) Implements Dao.KaiteiChusyutuDao.FindByNewBuhinEditListSaishin

            Dim db As New EBomDbClient
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT TG.SHISAKU_GOUSYA AS SHISAKU_GOUSYA, TG.SHISAKU_GOUSYA_HYOUJI_JUN AS HYOJIJUN_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TG.SHISAKU_EVENT_CODE = '{0}'", eventcode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE = '{0}'", shisakuListCode)
                .AppendLine(" GROUP BY TG.SHISAKU_GOUSYA,  TG.SHISAKU_GOUSYA_HYOUJI_JUN ")
                .AppendLine(" ORDER BY  TG.SHISAKU_GOUSYA_HYOUJI_JUN,TG.SHISAKU_GOUSYA ")
            End With


            'Dim sql As String = _
            '" SELECT TG.SHISAKU_GOUSYA AS SHISAKU_GOUSYA, TG.SHISAKU_GOUSYA_HYOUJI_JUN AS HYOJIJUN_NO " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG  WITH (NOLOCK, NOWAIT) " _
            '& " WHERE " _
            '& " TG.SHISAKU_EVENT_CODE = '" & eventcode & "'" _
            '& " AND TG.SHISAKU_LIST_CODE = '" & shisakuListCode & "'" _
            '& " GROUP BY TG.SHISAKU_GOUSYA,  TG.SHISAKU_GOUSYA_HYOUJI_JUN " _
            '& " ORDER BY  TG.SHISAKU_GOUSYA_HYOUJI_JUN,TG.SHISAKU_GOUSYA "

            Dim hshGosha As New Hashtable
            Dim goshaList As New List(Of String)
            For Each baseVo As TShisakuEventBaseVo In db.QueryForList(Of TShisakuEventBaseVo)(sql.ToString)
                goshaList.Add(String.Format("'{0}'", baseVo.ShisakuGousya))
                If Not hshGosha.Contains(baseVo.ShisakuGousya) Then
                    hshGosha.Add(baseVo.ShisakuGousya, baseVo.HyojijunNo)
                End If
            Next

            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT BE.*, BEI.INSU_SURYO, SBI.SHISAKU_GOUSYA, SB.BLOCK_FUYOU ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")

                If isBase Then
                    .AppendLine("AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO")
                Else
                    .AppendLine("AND ( NOT SB.SHISAKU_BLOCK_NO_KAITEI_NO = '000' AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO)")
                End If

                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BE.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BE.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SBI.SHISAKU_EVENT_CODE = '{0}'", eventcode)
                .AppendFormat(" AND SBI.SHISAKU_GOUSYA IN ({0})", String.Join(",", goshaList.ToArray))
                .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")
                .AppendLine(" AND SBI.INSU_SURYO > 0 ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" ORDER BY SBI.SHISAKU_GOUSYA, BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")
            End With
            Dim RList As List(Of TShisakuBuhinEditVoSekkeiHelper) = db.QueryForList(Of TShisakuBuhinEditVoSekkeiHelper)(sql.ToString)
            For Each resultVo As TShisakuBuhinEditVoSekkeiHelper In RList
                resultVo.ShisakuGousyaHyoujiJun = hshGosha.Item(resultVo.ShisakuGousya)
            Next
            Return RList
        End Function


        ''' <summary>
        ''' リストコード改訂Noに該当する手配改訂情報の前回改訂Noと一致する部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns>該当する部品編集情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditKaiteiNo(ByVal shisakuEventCode As String, _
                                                ByVal shisakuListCode As String, _
                                                Optional ByVal isIkansha As Boolean = False) As List(Of TShisakuTehaiKaiteiBlockVoHelper) Implements KaiteiChusyutuDao.FindByBuhinEditKaiteiNo
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT DISTINCT TKB.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KAITEI_BLOCK TKB WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TKB.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND TKB.SHISAKU_LIST_CODE = '{0}'", shisakuListCode)
            End With

            Dim db As New EBomDbClient

            Dim tehaiKaiteiBlockVoList As List(Of TShisakuTehaiKaiteiBlockVoHelper) = db.QueryForList(Of TShisakuTehaiKaiteiBlockVoHelper)(sql.ToString)

            Dim ikansha As String = ""
            If isIkansha Then
                ikansha = " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) " _
                            & " ON SBI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE " _
                            & " AND SBI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE " _
                            & " AND SBI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO " _
                            & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO " _
                            & " AND SBI.BASE_INSTL_FLG <> '1' "
            End If


            For Each tVo As TShisakuTehaiKaiteiBlockVoHelper In tehaiKaiteiBlockVoList

                If Not StringUtil.IsEmpty(tVo.ZenkaiBlockNoKaiteiNo) Then
                    With sql
                        .Remove(0, .Length)
                        .AppendLine(" SELECT * ")
                        .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB WITH (NOLOCK, NOWAIT) ")
                        .AppendLine(ikansha)
                        .AppendLine(" WHERE ")
                        .AppendFormat(" SB.SHISAKU_EVENT_CODE = '{0}'", tVo.ShisakuEventCode)
                        .AppendFormat(" AND SB.SHISAKU_BUKA_CODE = '{0}'", tVo.ShisakuBukaCode)
                        .AppendFormat(" AND SB.SHISAKU_BLOCK_NO = '{0}'", tVo.ShisakuBlockNo)
                        .AppendFormat(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}'", tVo.ZenkaiBlockNoKaiteiNo)
                    End With
                    Dim sekkeBlockVo As TShisakuSekkeiBlockVo = db.QueryForObject(Of TShisakuSekkeiBlockVo)(sql.ToString)
                    If sekkeBlockVo Is Nothing Then
                        tVo.BlockFuyou = "0"
                    Else
                        tVo.BlockFuyou = sekkeBlockVo.BlockFuyou
                    End If
                Else
                    tVo.BlockFuyou = "0"
                End If

            Next

            Return tehaiKaiteiBlockVoList
        End Function

        ''' <summary>
        ''' 最新-1の改訂Noで部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="level">レベル</param>
        ''' <param name="shukeiCode">集計コード</param>
        ''' <param name="siaShukeiCode">海外集計コード</param>
        ''' <param name="shisakuKbn">試作区分</param>
        ''' <param name="kyoukuSection">供給セクション</param>
        ''' <param name="saishiyoufuka">再使用不可</param>
        ''' <param name="KBFlag">今回前回フラグ(今回ならTrue)</param>
        ''' <returns>該当する部品編集情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBeforeBuhinEdit(ByVal shisakuEventCode As String, _
                                              ByVal shisakuBukaCode As String, _
                                              ByVal shisakuBlockNo As String, _
                                              ByVal level As Integer, _
                                              ByVal shukeiCode As String, _
                                              ByVal siaShukeiCode As String, _
                                              ByVal buhinNo As String, _
                                              ByVal shisakuKbn As String, _
                                              ByVal kyoukuSection As String, _
                                              ByVal saishiyoufuka As String, _
                                              ByVal KBFlag As Boolean) As TShisakuBuhinEditVoSekkeiHelper Implements KaiteiChusyutuDao.FindByBeforeBuhinEdit
            Dim KB As String = ""
            If KBFlag Then
                KB = "KON"
            Else
                KB = "ZEN"
            End If

            If StringUtil.IsEmpty(shisakuKbn) Then
                shisakuKbn = ""
            End If
            If StringUtil.IsEmpty(shukeiCode) Then
                shukeiCode = ""
            End If
            If StringUtil.IsEmpty(siaShukeiCode) Then
                siaShukeiCode = ""
            End If
            If StringUtil.IsEmpty(kyoukuSection) Then
                kyoukuSection = ""
            End If

            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI BEK  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = BEK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = BEK.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = BEK.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BEK.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BEK.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BEK.SHISAKU_LIST_CODE = '{0}' ", KB)
                .AppendFormat(" AND BEK.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", KB)
                .AppendFormat(" AND BEK.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND BEK.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND BEK.BUHIN_NO = '{0}' ", buhinNo)
                .AppendFormat(" AND BEK.LEVEL = {0} ", level)
                .AppendFormat(" AND BEK.SHUKEI_CODE = '{0}' ", shukeiCode)
                .AppendFormat(" AND BEK.SIA_SHUKEI_CODE = '{0}' ", siaShukeiCode)
                .AppendFormat(" AND BEK.BUHIN_NO_KBN = '{0}' ", shisakuKbn)
                .AppendFormat(" AND BEK.KYOUKU_SECTION = '{0}' ", kyoukuSection)
                .AppendFormat(" AND BEK.SAISHIYOUFUKA = '{0}' ", saishiyoufuka)
                .AppendLine(" ORDER BY BEK.SHISAKU_BLOCK_NO, BEK.BUHIN_NO_HYOUJI_JUN ")
            End With

            Dim db As New EBomDbClient

            Return db.QueryForObject(Of TShisakuBuhinEditVoSekkeiHelper)(sql.ToString)
        End Function


        ''' <summary>
        ''' 最新-1の改訂Noで部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="level">レベル</param>
        ''' <param name="shukeiCode">集計コード</param>
        ''' <param name="siaShukeiCode">海外集計コード</param>
        ''' <param name="shisakuKbn">試作区分</param>
        ''' <param name="KBFlag">今回前回フラグ(今回ならTrue)</param>
        ''' <returns>該当する部品編集情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBeforeBuhinEditFuyou(ByVal shisakuEventCode As String, _
                                                   ByVal shisakuBukaCode As String, _
                                                   ByVal shisakuBlockNo As String, _
                                                   ByVal level As Integer, _
                                                   ByVal shukeiCode As String, _
                                                   ByVal siaShukeiCode As String, _
                                                   ByVal buhinNo As String, _
                                                   ByVal shisakuKbn As String, _
                                                   ByVal KBFlag As Boolean) As TShisakuBuhinEditVoSekkeiHelper Implements KaiteiChusyutuDao.FindByBeforeBuhinEditFuyou
            Dim KB As String = ""
            If KBFlag Then
                KB = "KON"
            Else
                KB = "ZEN"
            End If

            If StringUtil.IsEmpty(shisakuKbn) Then
                shisakuKbn = ""
            End If
            If StringUtil.IsEmpty(shukeiCode) Then
                shukeiCode = ""
            End If
            If StringUtil.IsEmpty(siaShukeiCode) Then
                siaShukeiCode = ""
            End If

            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI BE  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BE.SHISAKU_LIST_CODE = '{0}' ", KB)
                .AppendFormat(" AND BE.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", KB)
                .AppendFormat(" AND BE.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND BE.BUHIN_NO = '{0}' ", buhinNo)
                .AppendFormat(" AND BE.LEVEL = {0} ", level)
                .AppendFormat(" AND BE.SHUKEI_CODE = '{0}' ", shukeiCode)
                .AppendFormat(" AND BE.SIA_SHUKEI_CODE = '{0}' ", siaShukeiCode)
                .AppendFormat(" AND BE.BUHIN_NO_KBN = '{0}' ", shisakuKbn)
                .AppendLine(" ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")
            End With

            Dim db As New EBomDbClient

            Return db.QueryForObject(Of TShisakuBuhinEditVoSekkeiHelper)(sql.ToString)
        End Function


        ''' <summary>
        ''' 最新-1の改訂Noでブロック情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="KBFlag">今回前回フラグ(今回ならTrue)</param>
        ''' <returns>該当する部品編集情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBeforeBlock(ByVal shisakuEventCode As String, _
                                              ByVal shisakuBukaCode As String, _
                                              ByVal shisakuBlockNo As String, _
                                              ByVal KBFlag As Boolean) As TShisakuBuhinEditVoSekkeiHelper Implements KaiteiChusyutuDao.FindByBeforeBlock
            Dim KB As String = ""
            If KBFlag Then
                KB = "KON"
            Else
                KB = "ZEN"
            End If

            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI BE  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BE.SHISAKU_LIST_CODE = '{0}' ", KB)
                .AppendFormat(" AND BE.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", KB)
                .AppendFormat(" AND BE.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendLine(" ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")
            End With

            Dim db As New EBomDbClient

            Return db.QueryForObject(Of TShisakuBuhinEditVoSekkeiHelper)(sql.ToString)
        End Function


        ''' <summary>
        ''' 部品編集号車改訂情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="KBFlag">今回か前回か(今回ならTrue)</param>
        ''' <returns>部品編集INSTL情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditGousya(ByVal shisakuEventCode As String, _
                                              ByVal shisakuBukaCode As String, _
                                              ByVal shisakuBlockNo As String, _
                                              ByVal shisakuBlockNoKaiteiNo As String, _
                                              ByVal buhinNoHyoujiJun As Integer, _
                                              ByVal KBFlag As Boolean) As List(Of TShisakuBuhinEditVoSekkeiHelper) Implements KaiteiChusyutuDao.FindByBuhinEditGousya

            Dim KB As String = ""
            If KBFlag Then
                KB = "KON"
            Else
                KB = "ZEN"
            End If

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT BEK.*, BEGK.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI BEK  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI BEGK  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BEGK.SHISAKU_EVENT_CODE = BEK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEGK.SHISAKU_LIST_CODE = BEK.SHISAKU_LIST_CODE ")
                .AppendLine(" AND BEGK.SHISAKU_LIST_CODE_KAITEI_NO = BEK.SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" AND BEGK.SHISAKU_BUKA_CODE = BEK.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEGK.SHISAKU_BLOCK_NO = BEK.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEGK.SHISAKU_BLOCK_NO_KAITEI_NO = BEK.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEGK.BUHIN_NO_HYOUJI_JUN = BEK.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BEK.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND BEK.SHISAKU_LIST_CODE = '{0}'", KB)
                .AppendFormat(" AND BEK.SHISAKU_LIST_CODE_KAITEI_NO = '{0}'", KB)
                .AppendFormat(" AND BEK.SHISAKU_BUKA_CODE = '{0}'", shisakuBukaCode)
                .AppendFormat(" AND BEK.SHISAKU_BLOCK_NO = '{0}'", shisakuBlockNo)
                .AppendFormat(" AND BEK.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}'", shisakuBlockNoKaiteiNo)
                .AppendFormat(" AND BEK.BUHIN_NO_HYOUJI_JUN = {0}", buhinNoHyoujiJun)
                .AppendLine(" ORDER BY BEK.SHISAKU_BLOCK_NO, BEK.BUHIN_NO_HYOUJI_JUN, BEGK.SHISAKU_GOUSYA_HYOUJI_JUN ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditVoSekkeiHelper)(sb.ToString)
        End Function

        ''' <summary>
        ''' 前回の改訂Noで該当する部品編集情報のリストを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuBukaCode"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <param name="shisakuBlockNoKaiteiNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByZenkaiBuhinEditList(ByVal shisakuEventCode As String, _
                                                  ByVal shisakuBukaCode As String, _
                                                  ByVal shisakuBlockNo As String, _
                                                  ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinEditVo) Implements KaiteiChusyutuDao.FindByZenkaiBuhinEditList

            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditVo)(sql.ToString)
        End Function


        ''' <summary>
        ''' 最新の部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuBukaCode"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <param name="buhinNoHyoujiJun"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByNewBuhinEdit(ByVal shisakuEventCode As String, _
                                    ByVal shisakuBukaCode As String, _
                                    ByVal shisakuBlockNo As String, _
                                    ByVal buhinNoHyoujiJun As Integer) As TShisakuBuhinEditVo Implements KaiteiChusyutuDao.FindByNewBuhinEdit
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT BE.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" ON BE.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BE.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendFormat(" AND BE.BUHIN_NO_HYOUJI_JUN = {0} ", buhinNoHyoujiJun)
                .AppendLine(" WHERE ")
                .AppendFormat(" SB.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SB.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SB.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX(SHISAKU_BLOCK_NO_KAITEI_NO) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND KACHOU_SYOUNIN_JYOUTAI = '36') ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuBuhinEditVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 号車のリストを取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByGousyaList(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuEventBaseVo) Implements KaiteiChusyutuDao.FindByGousyaList
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE L  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI K  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON K.SHISAKU_EVENT_CODE = L.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND K.SHISAKU_GROUP = L.SHISAKU_GROUP_NO ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON B.SHISAKU_EVENT_CODE = K.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND B.HYOJIJUN_NO = K.HYOJIJUN_NO ")
                .AppendLine(" WHERE ")
                .AppendFormat(" L.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND L.SHISAKU_LIST_CODE = '{0}'", shisakuListCode)
                .AppendLine(" AND L.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = L.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = L.SHISAKU_LIST_CODE ) ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventBaseVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 部品編集ベース情報を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <returns>部品編集ベース情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditBase(ByVal eventcode As String) As List(Of TShisakuBuhinEditBaseVo) Implements KaiteiChusyutuDao.FindByBuhinEditBase
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE BE WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') ")
                .AppendLine(" AND ( ( SB.JYOUTAI = '' AND SB.TANTO_SYOUNIN_JYOUTAI = '' AND SB.KACHOU_SYOUNIN_JYOUTAI = '') OR SB.KACHOU_SYOUNIN_JYOUTAI = '36') ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}' ", eventcode)
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditBaseVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 部品編集INSTLベース情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>部品編集INSTLベース情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditInstlBase(ByVal shisakuEventCode As String, _
                                                 ByVal shisakuBukaCode As String, _
                                                 ByVal shisakuBlockNo As String, _
                                                 ByVal buhinNoHyoujiJun As Integer) As List(Of TShisakuBuhinEditInstlBaseVo) Implements KaiteiChusyutuDao.FindByBuhinEditInstlBase
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE BE WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BE.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND BE.BUHIN_NO_HYOUJI_JUN = {0} ", buhinNoHyoujiJun)
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ) ")
                .AppendLine(" ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")
            End With
            Dim db As New EBomDbClient

            Return db.QueryForList(Of TShisakuBuhinEditInstlBaseVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 部品編集改訂号車ベース情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当する部品編集号車ベース情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditGousyaBase(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, ByVal buhinNoHyoujiJun As Integer) As List(Of TShisakuBuhinEditGousyaKaiteiVo) Implements KaiteiChusyutuDao.FindByBuhinEditGousyaBase
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT BEI.SHISAKU_EVENT_CODE, ")
                .AppendLine(" BEI.SHISAKU_BUKA_CODE, ")
                .AppendLine(" BEI.SHISAKU_BLOCK_NO, ")
                .AppendLine(" BEI.SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine(" BEI.INSU_SURYO, ")
                .AppendLine(" SBI.SHISAKU_GOUSYA, ")
                .AppendLine(" B.HYOJIJUN_NO AS SHISAKU_GOUSYA_HYOUJI_JUN, ")
                .AppendLine(" BEI.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE BEI WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" ON SBI.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SBI.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') ")
                .AppendLine(" AND ( ( SB.JYOUTAI = '' AND SB.TANTO_SYOUNIN_JYOUTAI = '' AND SB.KACHOU_SYOUNIN_JYOUTAI = '') OR SB.KACHOU_SYOUNIN_JYOUTAI = '36') ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA ")
                .AppendFormat(" WHERE BEI.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BEI.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND BEI.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND BEI.BUHIN_NO_HYOUJI_JUN = {0} ", buhinNoHyoujiJun)
                .AppendLine(" ORDER BY BEI.SHISAKU_BLOCK_NO, BEI.BUHIN_NO_HYOUJI_JUN ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditGousyaKaiteiVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 手配帳改訂抽出ブロック情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns>該当する手配帳改訂抽出ブロック情報</returns>
        ''' <remarks></remarks>
        Public Function FindByTehaiKaiteiBlock(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                                         ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As TShisakuTehaiKaiteiBlockVo Implements KaiteiChusyutuDao.FindByTehaiKaiteiBlock
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KAITEI_BLOCK WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND KONKAI_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuTehaiKaiteiBlockVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 手配帳改訂抽出ブロック情報を取得する(前回)
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns>該当する手配帳改訂抽出ブロック情報</returns>
        ''' <remarks></remarks>
        Public Function FindByTehaiKaiteiBlockZenkai(ByVal shisakuEventCode As String) As List(Of TShisakuBuhinEditVo) Implements KaiteiChusyutuDao.FindByTehaikaiteiBlockZenkai
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KAITEI_BLOCK TKB WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" ON TKB.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND TKB.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND TKB.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND TKB.ZENKAI_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = TKB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = TKB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = TKB.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = TKB.ZENKAI_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') ")
                .AppendLine(" AND ( ( SB.JYOUTAI = '' AND SB.TANTO_SYOUNIN_JYOUTAI = '' AND SB.KACHOU_SYOUNIN_JYOUTAI = '') OR SB.KACHOU_SYOUNIN_JYOUTAI = '36') ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 前回部品編集INSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>部品編集INSTLベース情報</returns>
        ''' <remarks></remarks>
        Public Function FindByZenkaiBuhinEditInstl(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                                          ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal buhinNoHyoujiJun As Integer) As List(Of TShisakuBuhinEditInstlVo) Implements KaiteiChusyutuDao.FindByZenkaiBuhinEditInstl
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BEI.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BEI.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND BEI.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendFormat(" AND BEI.BUHIN_NO_HYOUJI_JUN = {0} ", buhinNoHyoujiJun)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditInstlVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 号車改訂情報の員数を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <returns>号車改訂情報の員数</returns>
        ''' <remarks></remarks>
        Public Function FindByGousyaKaiteiInsu(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                                          ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, _
                                          ByVal buhinNoHyoujiJun As Integer, ByVal shisakuGousya As String) As Integer Implements KaiteiChusyutuDao.FindByGousyaKaiteiInsu
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine("SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI BEGK WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BEGK.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" BEGK.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" BEGK.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" BEGK.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendFormat(" BEGK.BUHIN_NO_HYOUJI_JUN = {0} ", buhinNoHyoujiJun)
                .AppendFormat(" BEGK.SHIAKU_GOUSYA = '{0}' ", shisakuGousya)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuBuhinEditGousyaKaiteiVo)(sql.ToString).InsuSuryo
        End Function

        ''' <summary>
        ''' 手配改訂ブロック情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns>手配改訂ブロック情報</returns>
        ''' <remarks></remarks>
        Public Function TehaiKaiteiBlock(ByVal shisakuEventCode As String, _
                                  ByVal shisakuListCode As String, _
                                  ByVal shisakuBukaCode As String, _
                                  ByVal shisakuBlockNo As String) As TShisakuTehaiKaiteiBlockVo Implements KaiteiChusyutuDao.TehaiKaiteiBlock
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KAITEI_BLOCK WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuTehaiKaiteiBlockVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' ブロックNoごとの部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuBukaCode"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <param name="shisakuBlockNoKaiteiNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByTehaiKaiteiBuhinEditList(ByVal shisakuEventCode As String, _
                                                    ByVal shisakuBukaCode As String, _
                                                    ByVal shisakuBlockNo As String, _
                                                    ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinEditVo) Implements KaiteiChusyutuDao.FindByTehaiKaiteiBuhinEditList
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" BE.SHISAKU_BUKA_CODE = '{0}'", shisakuBukaCode)
                .AppendFormat(" BE.SHISAKU_BLOCK_NO = '{0}'", shisakuBlockNo)
                .AppendFormat(" BE.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}'", shisakuBlockNoKaiteiNo)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditVo)(sql.ToString)
        End Function


        Public Function FindByTShisakuTehaiGousya(ByVal ShisakuEventCode As String, ByVal shisakuListCode As String) As Hashtable Implements KaiteiChusyutuDao.FindByTShisakuTehaiGousya
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT DISTINCT TG.SHISAKU_GOUSYA AS SHISAKU_GOUSYA, TG.SHISAKU_GOUSYA_HYOUJI_JUN AS HYOJIJUN_NO ")
                .AppendFormat(" FROM {0}.dbo.T_SHISAKU_TEHAI_GOUSYA TG WITH (NOLOCK, NOWAIT)  ", MBOM_DB_NAME)
                .AppendLine(" WHERE ")
                .AppendFormat(" TG.SHISAKU_EVENT_CODE = '{0}'", ShisakuEventCode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE = '{0}'", shisakuListCode)
                .AppendLine(" ORDER BY  TG.SHISAKU_GOUSYA_HYOUJI_JUN,TG.SHISAKU_GOUSYA ")
            End With
            Dim db As New EBomDbClient
            Dim rtnVal As New Hashtable
            For Each vo As TShisakuEventBaseVo In db.QueryForList(Of TShisakuEventBaseVo)(sb.ToString)
                If Not rtnVal.Contains(vo.ShisakuGousya) Then
                    rtnVal.Add(vo.ShisakuGousya, vo.HyojijunNo)
                End If
            Next
            Return rtnVal
        End Function


        ''' <summary>
        ''' 手配改訂ブロック情報をつかって部品編集情報を取得する
        ''' </summary>
        ''' <param name="kaiteiBlockList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByZenkaiTehaikaiteiBlock(ByVal kaiteiBlockList As TShisakuTehaiKaiteiBlockVoHelper, _
                                                     ByVal goushaList As Hashtable, _
                                                     Optional ByVal isIkansha As Boolean = False) As List(Of TShisakuBuhinEditVoSekkeiHelper) Implements KaiteiChusyutuDao.FindByZenkaiTehaikaiteiBlock

            Dim db As New EBomDbClient
            Dim sb As New StringBuilder


            '号車単位で取得する'
            Dim wkGosha As New List(Of String)
            For Each gosha As String In goushaList.Keys
                wkGosha.Add(String.Format("'{0}'", gosha))
            Next

            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT BE.*, SB.BLOCK_FUYOU, BEI.INSU_SURYO, SBI.SHISAKU_GOUSYA ")
                .AppendFormat(" FROM {0}.dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) ", MBOM_DB_NAME)
                .AppendFormat(" INNER JOIN {0}.dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI WITH (NOLOCK, NOWAIT)  ", MBOM_DB_NAME)
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                .AppendFormat(" INNER JOIN {0}.dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT)  ", MBOM_DB_NAME)
                .AppendLine(" ON SBI.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SBI.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")
                .AppendLine(" AND SBI.INSU_SURYO > 0 ")
                .AppendFormat(" AND SBI.SHISAKU_GOUSYA IN ({0})", String.Join(",", wkGosha.ToArray))
                If isIkansha Then
                    .AppendLine(" AND SBI.BASE_INSTL_FLG <> '1' ")
                End If
                .AppendFormat(" INNER JOIN {0}.dbo.T_SHISAKU_SEKKEI_BLOCK SB WITH (NOLOCK, NOWAIT)  ", MBOM_DB_NAME)
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}'", kaiteiBlockList.ShisakuEventCode)
                .AppendFormat(" AND BE.SHISAKU_BUKA_CODE = '{0}'", kaiteiBlockList.ShisakuBukaCode)
                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO = '{0}'", kaiteiBlockList.ShisakuBlockNo)
                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}'", kaiteiBlockList.ZenkaiBlockNoKaiteiNo)
                .AppendLine(" ORDER BY SBI.SHISAKU_GOUSYA,BE.BUHIN_NO_HYOUJI_JUN ")
            End With

            Dim resultList As List(Of TShisakuBuhinEditVoSekkeiHelper) = db.QueryForList(Of TShisakuBuhinEditVoSekkeiHelper)(sb.ToString)
            For Each Lvo As TShisakuBuhinEditVoSekkeiHelper In resultList
                Lvo.ShisakuGousyaHyoujiJun = goushaList.Item(Lvo.ShisakuGousya)
            Next

            Return resultList
        End Function


        ''' <summary>
        ''' 部品編集改訂情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="KBFlag">今回か前回か(今回ならTrue)</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditVoSekkeiHelper(ByVal shisakuEventCode As String, ByVal KBFlag As String, Optional ByVal isIkansha As Boolean = False) As List(Of TShisakuBuhinEditVoSekkeiHelper) Implements KaiteiChusyutuDao.FindByBuhinEditVoSekkeiHelper

            Dim KB As String = ""
            If KBFlag Then
                KB = "KON"
            Else
                KB = "ZEN"
            End If

            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT DISTINCT BEK.*, SB.BLOCK_FUYOU ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI BEK WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = BEK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = BEK.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = BEK.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BEK.SHISAKU_BLOCK_NO_KAITEI_NO ")
                If isIkansha Then
                    .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT)  ")
                    .AppendLine(" ON SBI.SHISAKU_EVENT_CODE = BEK.SHISAKU_EVENT_CODE ")
                    .AppendLine(" AND SBI.SHISAKU_BUKA_CODE = BEK.SHISAKU_BUKA_CODE ")
                    .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = BEK.SHISAKU_BLOCK_NO ")
                    .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BEK.SHISAKU_BLOCK_NO_KAITEI_NO ")
                    .AppendLine(" AND SBI.BASE_INSTL_FLG <> '1' ")
                End If
                .AppendLine(" WHERE ")
                .AppendFormat(" BEK.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}'", KB)
                .AppendFormat(" AND SHISAKU_LIST_CODE_KAITEI_NO = '{0}'", KB)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditVoSekkeiHelper)(sql.ToString)
        End Function


#Region "チェックする処理"

        ''' <summary>
        ''' 部品編集ベース情報をチェック
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当する部品編集ベース情報</returns>
        ''' <remarks></remarks>
        Public Function CheckByBuhinEditBase(ByVal shisakuEventCode As String, _
                                             ByVal shisakuBukaCode As String, _
                                             ByVal shisakuBlockNo As String, _
                                             ByVal buhinNoHyoujiJun As Integer) As TShisakuBuhinEditBaseVo Implements KaiteiChusyutuDao.CheckByBuhinEditBase
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE BE ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BE.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND BE.BUHIN_NO_HYOUJI_JUN = {0} ", buhinNoHyoujiJun)
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ) ")
                .AppendLine(" ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuBuhinEditBaseVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 部品編集情報をチェック
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当する部品編集情報</returns>
        ''' <remarks></remarks>
        Public Function CheckByBuhinEdit(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, ByVal buhinNoHyoujiJun As Integer) As TShisakuBuhinEditVo Implements KaiteiChusyutuDao.CheckByBuhinEdit
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BE.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND BE.BUHIN_NO_HYOUJI_JUN = {0} ", buhinNoHyoujiJun)
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX(SHISAKU_BLOCK_NO_KAITEI_NO) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ) ")
                .AppendLine(" ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuBuhinEditVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 部品編集情報をチェック
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当する部品編集情報</returns>
        ''' <remarks></remarks>
        Public Function CheckByZenkaiBuhinEdit(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal buhinNoHyoujiJun As Integer) As TShisakuBuhinEditVo Implements KaiteiChusyutuDao.CheckByZeinkaiBuhinEdit
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BE.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND BE.BUHIN_NO_HYOUJI_JUN = {0} ", buhinNoHyoujiJun)
                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendLine(" ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuBuhinEditVo)(sql.ToString)
        End Function


#End Region

#Region "追加する処理"

        ''' <summary>
        ''' 変更前、変更後の追加処理(ベース側)
        ''' </summary>
        ''' <param name="buhinEditBaseVo">ベースとなる部品編集情報</param>
        ''' <param name="buhinEditVo">部品編集情報</param>
        ''' <remarks></remarks>
        Public Sub InsertByChange(ByVal buhinEditBaseVo As TShisakuBuhinEditVo, ByVal buhinEditVo As TShisakuBuhinEditVo, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) Implements KaiteiChusyutuDao.InsertByChange

            Dim aDate As New ShisakuDate
            Dim UserId As String
            Dim Hi As String
            Dim Time As String
            UserId = LoginInfo.Now.UserId
            Hi = aDate.CurrentDateDbFormat
            Time = aDate.CurrentTimeDbFormat


            Dim Sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI (" _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_LIST_CODE, " _
            & " SHISAKU_LIST_CODE_KAITEI_NO, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
            & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " BUHIN_NO_HYOUJI_JUN, " _
            & " FLAG, " _
            & " LEVEL, " _
            & " SHUKEI_CODE, " _
            & " SIA_SHUKEI_CODE, " _
            & " GENCYO_CKD_KBN, " _
            & " KYOUKU_SECTION, " _
            & " MAKER_CODE, " _
            & " MAKER_NAME, " _
            & " BUHIN_NO, " _
            & " BUHIN_NO_KBN, " _
            & " BUHIN_NO_KAITEI_NO, " _
            & " EDA_BAN, " _
            & " BUHIN_NAME, " _
            & " SAISHIYOUFUKA, " _
            & " GOUSYA_HACHU_TENKAI_FLG, " _
            & " GOUSYA_HACHU_TENKAI_UNIT_KBN, " _
            & " SHUTUZU_YOTEI_DATE, " _
            & " SHUTUZU_JISEKI_DATE, " _
            & " SHUTUZU_JISEKI_KAITEI_NO, " _
            & " SHUTUZU_JISEKI_STSR_DHSTBA, " _
            & " SAISYU_SETSUHEN_DATE, " _
            & " SAISYU_SETSUHEN_KAITEI_NO, " _
            & " STSR_DHSTBA, " _
            & " ZAISHITU_KIKAKU_1, " _
            & " ZAISHITU_KIKAKU_2, " _
            & " ZAISHITU_KIKAKU_3, " _
            & " ZAISHITU_MEKKI, " _
                    & " TSUKURIKATA_SEISAKU, " _
                    & " TSUKURIKATA_KATASHIYOU_1, " _
                    & " TSUKURIKATA_KATASHIYOU_2, " _
                    & " TSUKURIKATA_KATASHIYOU_3, " _
                    & " TSUKURIKATA_TIGU, " _
                    & " TSUKURIKATA_NOUNYU, " _
                    & " TSUKURIKATA_KIBO, " _
            & " SHISAKU_BANKO_SURYO, " _
            & " SHISAKU_BANKO_SURYO_U, " _
            & " MATERIAL_INFO_LENGTH, " _
            & " MATERIAL_INFO_WIDTH, " _
            & " ZAIRYO_SUNPO_X, " _
            & " ZAIRYO_SUNPO_Y, " _
            & " ZAIRYO_SUNPO_Z, " _
            & " ZAIRYO_SUNPO_XY, " _
            & " ZAIRYO_SUNPO_XZ, " _
            & " ZAIRYO_SUNPO_YZ, " _
            & " MATERIAL_INFO_ORDER_TARGET, " _
            & " MATERIAL_INFO_ORDER_TARGET_DATE, " _
            & " MATERIAL_INFO_ORDER_CHK, " _
            & " MATERIAL_INFO_ORDER_CHK_DATE, " _
            & " DATA_ITEM_KAITEI_NO, " _
            & " DATA_ITEM_AREA_NAME, " _
            & " DATA_ITEM_SET_NAME, " _
            & " DATA_ITEM_KAITEI_INFO, " _
            & " DATA_ITEM_DATA_PROVISION, " _
            & " DATA_ITEM_DATA_PROVISION_DATE, " _
            & " SHISAKU_BUHIN_HI, " _
            & " SHISAKU_KATA_HI, " _
            & " BIKOU, " _
            & " BIKOU_NOTE, " _
            & " EDIT_TOUROKUBI, " _
            & " EDIT_TOUROKUJIKAN, " _
            & " KAITEI_HANDAN_FLG, " _
            & " AUTO_ORIKOMI_KAITEI_NO, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & "'" & buhinEditVo.ShisakuEventCode & "' , " _
            & "'" & shisakuListCode & "' , " _
            & "'" & shisakuListCodeKaiteiNo & "' , " _
            & "'" & buhinEditVo.ShisakuBukaCode & "' , " _
            & "'" & buhinEditVo.ShisakuBlockNo & "' , " _
            & "'" & buhinEditVo.ShisakuBlockNoKaiteiNo & "' , " _
            & "'" & buhinEditBaseVo.ShisakuBlockNoKaiteiNo & "' , " _
            & "'" & buhinEditBaseVo.BuhinNoHyoujiJun & "' , " _
            & " '3' , " _
            & "'" & buhinEditBaseVo.Level & "' , " _
            & "'" & buhinEditBaseVo.ShukeiCode & "' , " _
            & "'" & buhinEditBaseVo.SiaShukeiCode & "' , " _
            & "'" & buhinEditBaseVo.GencyoCkdKbn & "' , " _
            & "'" & buhinEditBaseVo.KyoukuSection & "' " _
            & "'" & buhinEditBaseVo.MakerCode & "' , " _
            & "'" & buhinEditBaseVo.MakerName & "' , " _
            & "'" & buhinEditBaseVo.BuhinNo & "' , " _
            & "'" & buhinEditBaseVo.BuhinNoKbn & "' , " _
            & "'" & buhinEditBaseVo.BuhinNoKaiteiNo & "' , " _
            & "'" & buhinEditBaseVo.EdaBan & "' , " _
            & "'" & buhinEditBaseVo.BuhinName & "' , " _
            & "'" & buhinEditBaseVo.Saishiyoufuka & "' , " _
            & "'' , " _
            & "'' , " _
            & "'" & buhinEditBaseVo.ShutuzuYoteiDate & "' , " _
            & "0 , " _
            & "'' , " _
            & "'' , " _
            & "0 , " _
            & "'' , " _
            & "'' , " _
            & "'" & buhinEditBaseVo.ZaishituKikaku1 & "' , " _
            & "'" & buhinEditBaseVo.ZaishituKikaku2 & "' , " _
            & "'" & buhinEditBaseVo.ZaishituKikaku3 & "' , " _
            & "'" & buhinEditBaseVo.ZaishituMekki & "' , " _
            & "'" & buhinEditBaseVo.TsukurikataSeisaku & "' , " _
            & "'" & buhinEditBaseVo.TsukurikataKatashiyou1 & "' , " _
            & "'" & buhinEditBaseVo.TsukurikataKatashiyou2 & "' , " _
            & "'" & buhinEditBaseVo.TsukurikataKatashiyou3 & "' , " _
            & "'" & buhinEditBaseVo.TsukurikataTigu & "' , " _
                        & "'" & buhinEditBaseVo.TsukurikataNounyu & "' , " _
                        & "'" & buhinEditBaseVo.TsukurikataKibo & "' , " _
            & "'" & buhinEditBaseVo.ShisakuBankoSuryo & "' , " _
            & "'" & buhinEditBaseVo.ShisakuBankoSuryoU & "' , " _
            & "'" & buhinEditBaseVo.MaterialInfoLength & "' , " _
            & "'" & buhinEditBaseVo.MaterialInfoWidth & "' , " _
            & "0 , " _
            & "0 , " _
            & "0 , " _
            & "0 , " _
            & "0 , " _
            & "0 , " _
            & "'' , " _
            & "'' , " _
            & "'' , " _
            & "'' , " _
            & "'" & buhinEditBaseVo.DataItemKaiteiNo & "' , " _
            & "'" & buhinEditBaseVo.DataItemAreaName & "' , " _
            & "'" & buhinEditBaseVo.DataItemSetName & "' , " _
            & "'" & buhinEditBaseVo.DataItemKaiteiInfo & "' , " _
            & "'' , " _
            & "'' , " _
            & "'" & buhinEditBaseVo.ShisakuBuhinHi & "' , " _
            & "'" & buhinEditBaseVo.ShisakuKataHi & "' , " _
            & "'" & buhinEditBaseVo.Bikou & "' , " _
            & "'' , " _
            & "'" & buhinEditBaseVo.EditTourokubi & "' , " _
            & "'" & buhinEditBaseVo.EditTourokujikan & "' , " _
            & "'" & buhinEditBaseVo.KaiteiHandanFlg & "' , " _
            & "'' , " _
            & "'" & UserId & "' , " _
            & "'" & Hi & "' , " _
            & "'" & Time & "' , " _
            & "'" & UserId & "' , " _
            & "'" & Hi & "' , " _
            & "'" & Time & "' " _
            & " ) " _
            & " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI (" _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_LIST_CODE, " _
            & " SHISAKU_LIST_CODE_KAITEI_NO, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
            & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " BUHIN_NO_HYOUJI_JUN, " _
            & " FLAG, " _
            & " LEVEL, " _
            & " SHUKEI_CODE, " _
            & " SIA_SHUKEI_CODE, " _
            & " GENCYO_CKD_KBN, " _
            & " KYOUKU_SECTION, " _
            & " MAKER_CODE, " _
            & " MAKER_NAME, " _
            & " BUHIN_NO, " _
            & " BUHIN_NO_KBN, " _
            & " BUHIN_NO_KAITEI_NO, " _
            & " EDA_BAN, " _
            & " BUHIN_NAME, " _
            & " SAISHIYOUFUKA, " _
            & " GOUSYA_HACHU_TENKAI_FLG, " _
            & " GOUSYA_HACHU_TENKAI_UNIT_KBN, " _
            & " SHUTUZU_YOTEI_DATE, " _
            & " SHUTUZU_JISEKI_DATE, " _
            & " SHUTUZU_JISEKI_KAITEI_NO, " _
            & " SHUTUZU_JISEKI_STSR_DHSTBA, " _
            & " SAISYU_SETSUHEN_DATE, " _
            & " SAISYU_SETSUHEN_KAITEI_NO, " _
            & " STSR_DHSTBA, " _
            & " ZAISHITU_KIKAKU_1, " _
            & " ZAISHITU_KIKAKU_2, " _
            & " ZAISHITU_KIKAKU_3, " _
            & " ZAISHITU_MEKKI, " _
                    & " TSUKURIKATA_SEISAKU, " _
                    & " TSUKURIKATA_KATASHIYOU_1, " _
                    & " TSUKURIKATA_KATASHIYOU_2, " _
                    & " TSUKURIKATA_KATASHIYOU_3, " _
                    & " TSUKURIKATA_TIGU, " _
                    & " TSUKURIKATA_NOUNYU, " _
                    & " TSUKURIKATA_KIBO, " _
            & " SHISAKU_BANKO_SURYO, " _
            & " SHISAKU_BANKO_SURYO_U, " _
            & " MATERIAL_INFO_LENGTH, " _
            & " MATERIAL_INFO_WIDTH, " _
            & " ZAIRYO_SUNPO_X, " _
            & " ZAIRYO_SUNPO_Y, " _
            & " ZAIRYO_SUNPO_Z, " _
            & " ZAIRYO_SUNPO_XY, " _
            & " ZAIRYO_SUNPO_XZ, " _
            & " ZAIRYO_SUNPO_YZ, " _
            & " MATERIAL_INFO_ORDER_TARGET, " _
            & " MATERIAL_INFO_ORDER_TARGET_DATE, " _
            & " MATERIAL_INFO_ORDER_CHK, " _
            & " MATERIAL_INFO_ORDER_CHK_DATE, " _
            & " DATA_ITEM_KAITEI_NO, " _
            & " DATA_ITEM_AREA_NAME, " _
            & " DATA_ITEM_SET_NAME, " _
            & " DATA_ITEM_KAITEI_INFO, " _
            & " DATA_ITEM_DATA_PROVISION, " _
            & " DATA_ITEM_DATA_PROVISION_DATE, " _
            & " SHISAKU_BUHIN_HI, " _
            & " SHISAKU_KATA_HI, " _
            & " BIKOU, " _
            & " BIKOU_NOTE, " _
            & " EDIT_TOUROKUBI, " _
            & " EDIT_TOUROKUJIKAN, " _
            & " KAITEI_HANDAN_FLG, " _
            & " AUTO_ORIKOMI_KAITEI_NO, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & "'" & buhinEditVo.ShisakuEventCode & "' , " _
            & "'" & shisakuListCode & "' , " _
            & "'" & shisakuListCodeKaiteiNo & "' , " _
            & "'" & buhinEditVo.ShisakuBukaCode & "' , " _
            & "'" & buhinEditVo.ShisakuBlockNo & "' , " _
            & "'" & buhinEditVo.ShisakuBlockNoKaiteiNo & "' , " _
            & " '', " _
            & "'" & buhinEditVo.BuhinNoHyoujiJun & "' , " _
            & " '4', " _
            & "'" & buhinEditVo.Level & "' , " _
            & "'" & buhinEditVo.ShukeiCode & "' , " _
            & "'" & buhinEditVo.SiaShukeiCode & "' , " _
            & "'" & buhinEditVo.GencyoCkdKbn & "' , " _
            & "'" & buhinEditVo.KyoukuSection & "' " _
            & "'" & buhinEditVo.MakerCode & "' , " _
            & "'" & buhinEditVo.MakerName & "' , " _
            & "'" & buhinEditVo.BuhinNo & "' , " _
            & "'" & buhinEditVo.BuhinNoKbn & "' , " _
            & "'" & buhinEditVo.BuhinNoKaiteiNo & "' , " _
            & "'" & buhinEditVo.EdaBan & "' , " _
            & "'" & buhinEditVo.BuhinName & "' , " _
            & "'" & buhinEditVo.Saishiyoufuka & "' , " _
            & "'' , " _
            & "'' , " _
            & "'" & buhinEditVo.ShutuzuYoteiDate & "' , " _
            & "0 , " _
            & "'' , " _
            & "'' , " _
            & "0 , " _
            & "'' , " _
            & "'' , " _
            & "'" & buhinEditVo.ZaishituKikaku1 & "' , " _
            & "'" & buhinEditVo.ZaishituKikaku2 & "' , " _
            & "'" & buhinEditVo.ZaishituKikaku3 & "' , " _
            & "'" & buhinEditVo.ZaishituMekki & "' , " _
            & "'" & buhinEditVo.TsukurikataSeisaku & "' , " _
            & "'" & buhinEditVo.TsukurikataKatashiyou1 & "' , " _
            & "'" & buhinEditVo.TsukurikataKatashiyou2 & "' , " _
            & "'" & buhinEditVo.TsukurikataKatashiyou3 & "' , " _
            & "'" & buhinEditVo.TsukurikataTigu & "' , " _
                        & "'" & buhinEditVo.TsukurikataNounyu & "' , " _
                        & "'" & buhinEditVo.TsukurikataKibo & "' , " _
            & "'" & buhinEditVo.ShisakuBankoSuryo & "' , " _
            & "'" & buhinEditVo.ShisakuBankoSuryoU & "' , " _
            & "'" & buhinEditVo.MaterialInfoLength & "' , " _
            & "'" & buhinEditVo.MaterialInfoWidth & "' , " _
            & "0 , " _
            & "0 , " _
            & "0 , " _
            & "0 , " _
            & "0 , " _
            & "0 , " _
            & "'' , " _
            & "'' , " _
            & "'' , " _
            & "'' , " _
            & "'" & buhinEditVo.DataItemKaiteiNo & "' , " _
            & "'" & buhinEditVo.DataItemAreaName & "' , " _
            & "'" & buhinEditVo.DataItemSetName & "' , " _
            & "'" & buhinEditVo.DataItemKaiteiInfo & "' , " _
            & "'' , " _
            & "'' , " _
            & "'" & buhinEditVo.ShisakuBuhinHi & "' , " _
            & "'" & buhinEditVo.ShisakuKataHi & "' , " _
            & "'" & buhinEditVo.Bikou & "' , " _
            & "'' , " _
            & "'" & buhinEditVo.EditTourokubi & "' , " _
            & "'" & buhinEditVo.EditTourokujikan & "' , " _
            & "'" & buhinEditVo.KaiteiHandanFlg & "' , " _
            & "'' , " _
            & "'" & UserId & "' , " _
            & "'" & Hi & "' , " _
            & "'" & Time & "' , " _
            & "'" & UserId & "' , " _
            & "'" & Hi & "' , " _
            & "'" & Time & "' " _
            & " ) "
            '↑↑2014/09/26 酒井 ADD 作り方項目
            Dim db As New EBomDbClient
            db.Insert(Sql)
        End Sub

        ''' <summary>
        ''' 部品編集改訂情報を追加する
        ''' </summary>
        ''' <param name="buhinEditVo">部品編集情報</param>
        ''' <remarks></remarks>
        Public Sub InsertByBuhinEditKaitei(ByVal buhinEditVo As TShisakuBuhinEditVo, _
                                           ByVal shisakuListCode As String, _
                                           ByVal shisakuListCodeKaiteiNo As String, _
                                           ByVal Flag As String, _
                                           ByVal zenkaiBlockKaiteiNo As String) Implements KaiteiChusyutuDao.InsertByBuhinEditKaitei
            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI (" _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_LIST_CODE, " _
            & " SHISAKU_LIST_CODE_KAITEI_NO, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
            & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " BUHIN_NO_HYOUJI_JUN, " _
            & " FLAG, " _
            & " LEVEL, " _
            & " SHUKEI_CODE, " _
            & " SIA_SHUKEI_CODE, " _
            & " GENCYO_CKD_KBN, " _
            & " KYOUKU_SECTION, " _
            & " MAKER_CODE, " _
            & " MAKER_NAME, " _
            & " BUHIN_NO, " _
            & " BUHIN_NO_KBN, " _
            & " BUHIN_NO_KAITEI_NO, " _
            & " EDA_BAN, " _
            & " BUHIN_NAME, " _
            & " SAISHIYOUFUKA, " _
            & " GOUSYA_HACHU_TENKAI_FLG, " _
            & " GOUSYA_HACHU_TENKAI_UNIT_KBN, " _
            & " SHUTUZU_YOTEI_DATE, " _
            & " SHUTUZU_JISEKI_DATE, " _
            & " SHUTUZU_JISEKI_KAITEI_NO, " _
            & " SHUTUZU_JISEKI_STSR_DHSTBA, " _
            & " SAISYU_SETSUHEN_DATE, " _
            & " SAISYU_SETSUHEN_KAITEI_NO, " _
            & " STSR_DHSTBA, " _
            & " ZAISHITU_KIKAKU_1, " _
            & " ZAISHITU_KIKAKU_2, " _
            & " ZAISHITU_KIKAKU_3, " _
            & " ZAISHITU_MEKKI, " _
                    & " TSUKURIKATA_SEISAKU, " _
                    & " TSUKURIKATA_KATASHIYOU_1, " _
                    & " TSUKURIKATA_KATASHIYOU_2, " _
                    & " TSUKURIKATA_KATASHIYOU_3, " _
                    & " TSUKURIKATA_TIGU, " _
                    & " TSUKURIKATA_NOUNYU, " _
                    & " TSUKURIKATA_KIBO, " _
            & " SHISAKU_BANKO_SURYO, " _
            & " SHISAKU_BANKO_SURYO_U, " _
            & " MATERIAL_INFO_LENGTH, " _
            & " MATERIAL_INFO_WIDTH, " _
            & " ZAIRYO_SUNPO_X, " _
            & " ZAIRYO_SUNPO_Y, " _
            & " ZAIRYO_SUNPO_Z, " _
            & " ZAIRYO_SUNPO_XY, " _
            & " ZAIRYO_SUNPO_XZ, " _
            & " ZAIRYO_SUNPO_YZ, " _
            & " MATERIAL_INFO_ORDER_TARGET, " _
            & " MATERIAL_INFO_ORDER_TARGET_DATE, " _
            & " MATERIAL_INFO_ORDER_CHK, " _
            & " MATERIAL_INFO_ORDER_CHK_DATE, " _
            & " DATA_ITEM_KAITEI_NO, " _
            & " DATA_ITEM_AREA_NAME, " _
            & " DATA_ITEM_SET_NAME, " _
            & " DATA_ITEM_KAITEI_INFO, " _
            & " DATA_ITEM_DATA_PROVISION, " _
            & " DATA_ITEM_DATA_PROVISION_DATE, " _
            & " SHISAKU_BUHIN_HI, " _
            & " SHISAKU_KATA_HI, " _
            & " BIKOU, " _
            & " BUHIN_NOTE, " _
            & " EDIT_TOUROKUBI, " _
            & " EDIT_TOUROKUJIKAN, " _
            & " KAITEI_HANDAN_FLG, " _
            & " AUTO_ORIKOMI_KAITEI_NO, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & " @ShisakuEventCode, " _
            & " @ShisakuListCode, " _
            & " @ShisakuListCodeKaiteiNo, " _
            & " @ShisakuBukaCode, " _
            & " @ShisakuBlockNo, " _
            & " @ShisakuBlockNoKaiteiNo, " _
            & " @ZenkaiShisakuBlockNoKaiteiNo, " _
            & " @BuhinNoHyoujiJun, " _
            & " @Flag, " _
            & " @Level, " _
            & " @ShukeiCode, " _
            & " @SiaShukeiCode, " _
            & " @GencyoCkdKbn, " _
            & " @KyoukuSection, " _
            & " @MakerCode, " _
            & " @MakerName, " _
            & " @BuhinNo, " _
            & " @BuhinNoKbn, " _
            & " @BuhinNoKaiteiNo, " _
            & " @EdaBan, " _
            & " @BuhinName, " _
            & " @Saishiyoufuka, " _
            & " @GousyaHachuTenkaiFlg, " _
            & " @GousyaHachuTenkaiUnitKbn, " _
            & " @ShutuzuYoteiDate, " _
            & " @ShutuzuJisekiDate," _
            & " @ShutuzuJisekiKaiteiNo," _
            & " @ShutuzuJisekiStsrDhstba," _
            & " @SaisyuSetsuhenDate," _
            & " @SaisyuSetsuhenKaiteiNo," _
            & " @StsrDhstba," _
            & " @ZaishituKikaku1, " _
            & " @ZaishituKikaku2, " _
            & " @ZaishituKikaku3, " _
            & " @ZaishituMekki, " _
            & " @TsukurikataSeisaku, " _
            & " @TsukurikataKatashiyou1, " _
            & " @TsukurikataKatashiyou2, " _
            & " @TsukurikataKatashiyou3, " _
            & " @TsukurikataTigu, " _
            & " @TsukurikataNounyu, " _
            & " @TsukurikataKibo, " _
            & " @ShisakuBankoSuryo, " _
            & " @ShisakuBankoSuryoU, " _
            & " @MaterialInfoLength, " _
            & " @MaterialInfoWidth, " _
            & " @ZairyoSunpoX, " _
            & " @ZairyoSunpoY, " _
            & " @ZairyoSunpoZ, " _
            & " @ZairyoSunpoXy, " _
            & " @ZairyoSunpoXz, " _
            & " @ZairyoSunpoYz, " _
            & " @MaterialInfoOrderTarget, " _
            & " @MaterialInfoOrderTargetDate, " _
            & " @MaterialInfoOrderChk, " _
            & " @MaterialInfoOrderChkDate, " _
            & " @DataItemKaiteiNo, " _
            & " @DataItemAreaName, " _
            & " @DataItemSetName, " _
            & " @DataItemKaiteiInfo, " _
            & " @DataItemDataProvision, " _
            & " @DataItemDataProvisionDate, " _
            & " @ShisakuBuhinHi, " _
            & " @ShisakuKataHi, " _
            & " @Bikou, " _
            & " @BuhinNote, " _
            & " @EditTourokubi, " _
            & " @EditTourokujikan, " _
            & " @KaiteiHandanFlg, " _
            & " @AutoOrikomiKaiteiNo, " _
            & " @CreatedUserId, " _
            & " @CreatedDate, " _
            & " @CreatedTime, " _
            & " @UpdatedUserId, " _
            & " @UpdatedDate, " _
            & " @UpdatedTime" _
            & " ) "
            '↑↑2014/09/26 酒井 ADD 作り方項目

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuBuhinEditKaiteiVo

            param.ShisakuEventCode = buhinEditVo.ShisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
            param.ShisakuBukaCode = buhinEditVo.ShisakuBukaCode
            param.ShisakuBlockNo = buhinEditVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = buhinEditVo.ShisakuBlockNoKaiteiNo
            param.ZenkaiShisakuBlockNoKaiteiNo = zenkaiBlockKaiteiNo
            param.BuhinNoHyoujiJun = buhinEditVo.BuhinNoHyoujiJun
            param.Flag = Flag
            param.Level = buhinEditVo.Level
            param.ShukeiCode = buhinEditVo.ShukeiCode
            param.SiaShukeiCode = buhinEditVo.SiaShukeiCode
            param.GencyoCkdKbn = buhinEditVo.GencyoCkdKbn
            param.KyoukuSection = buhinEditVo.KyoukuSection
            param.MakerCode = buhinEditVo.MakerCode
            param.MakerName = buhinEditVo.MakerName
            param.BuhinNo = buhinEditVo.BuhinNo
            param.BuhinNoKbn = buhinEditVo.BuhinNoKbn
            param.BuhinNoKaiteiNo = buhinEditVo.BuhinNoKaiteiNo
            param.EdaBan = buhinEditVo.EdaBan
            param.BuhinName = buhinEditVo.BuhinName
            param.Saishiyoufuka = buhinEditVo.Saishiyoufuka
            param.ShutuzuYoteiDate = buhinEditVo.ShutuzuYoteiDate

            param.GousyaHachuTenkaiFlg = ""
            param.GousyaHachuTenkaiUnitKbn = ""
            param.ShutuzuJisekiDate = 0
            param.ShutuzuJisekiKaiteiNo = ""
            param.ShutuzuJisekiStsrDhstba = ""
            param.SaisyuSetsuhenDate = 0
            param.SaisyuSetsuhenKaiteiNo = ""
            param.StsrDhstba = ""
            param.ZairyoSunpoX = 0
            param.ZairyoSunpoY = 0
            param.ZairyoSunpoZ = 0
            param.ZairyoSunpoXy = 0
            param.ZairyoSunpoXz = 0
            param.ZairyoSunpoYz = 0

            '↓↓2015/01/09 劉 ADD BEGIN
            param.BuhinNote = ""
            param.AutoOrikomiKaiteiNo = ""
            param.DataItemDataProvision = "0"
            param.DataItemDataProvisionDate = ""
            param.MaterialInfoOrderTarget = "0"
            param.MaterialInfoOrderTargetDate = ""
            param.MaterialInfoOrderChk = "0"
            param.MaterialInfoOrderChkDate = ""
            '↑↑2015/01/09 劉 ADD END

            '↓↓2014/09/26 酒井 ADD BEGIN
            param.TsukurikataSeisaku = buhinEditVo.TsukurikataSeisaku
            param.TsukurikataKatashiyou1 = buhinEditVo.TsukurikataKatashiyou1
            param.TsukurikataKatashiyou2 = buhinEditVo.TsukurikataKatashiyou2
            param.TsukurikataKatashiyou3 = buhinEditVo.TsukurikataKatashiyou3
            param.TsukurikataTigu = buhinEditVo.TsukurikataTigu
            param.TsukurikataNounyu = buhinEditVo.TsukurikataNounyu
            param.TsukurikataKibo = buhinEditVo.TsukurikataKibo
            '↑↑2014/09/26 酒井 ADD END
            param.ZaishituKikaku1 = buhinEditVo.ZaishituKikaku1
            param.ZaishituKikaku2 = buhinEditVo.ZaishituKikaku2
            param.ZaishituKikaku3 = buhinEditVo.ZaishituKikaku3
            param.ZaishituMekki = buhinEditVo.ZaishituMekki
            param.ShisakuBankoSuryo = buhinEditVo.ShisakuBankoSuryo
            param.ShisakuBankoSuryoU = buhinEditVo.ShisakuBankoSuryoU


            ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
            param.MaterialInfoLength = buhinEditVo.MaterialInfoLength
            param.MaterialInfoWidth = buhinEditVo.MaterialInfoWidth
            param.DataItemKaiteiNo = buhinEditVo.DataItemKaiteiNo
            param.DataItemAreaName = buhinEditVo.DataItemAreaName
            param.DataItemSetName = buhinEditVo.DataItemSetName
            param.DataItemKaiteiInfo = buhinEditVo.DataItemKaiteiInfo
            ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


            param.ShisakuBuhinHi = buhinEditVo.ShisakuBuhinHi
            param.ShisakuKataHi = buhinEditVo.ShisakuKataHi
            param.Bikou = buhinEditVo.Bikou
            param.EditTourokubi = buhinEditVo.EditTourokubi
            param.EditTourokujikan = buhinEditVo.EditTourokujikan
            param.KaiteiHandanFlg = buhinEditVo.KaiteiHandanFlg
            param.CreatedUserId = LoginInfo.Now.UserId
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Insert(sql, param)

        End Sub

        ''' <summary>
        ''' 部品編集改訂号車情報を追加する
        ''' </summary>
        ''' <param name="shisakuEventCode">部品編集号車改訂情報</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">今回ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="zenkaiShisakuBlockNoKaiteiNo">前回ブロック改訂No</param>
        ''' <param name="shisakuGousyaHyoujiJun">試作号車表示順</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="insuSuryo">員数数量</param>
        ''' <param name="Flag">フラグ</param>
        ''' <remarks></remarks>
        Public Sub InsertByBuhinEditGousya(ByVal shisakuEventCode As String, _
                                           ByVal shisakuListCode As String, _
                                           ByVal shisakuListCodeKaiteiNo As String, _
                                           ByVal shisakuBukaCode As String, _
                                           ByVal shisakuBlockNo As String, _
                                           ByVal shisakuBlockNoKaiteiNo As String, _
                                           ByVal zenkaiShisakuBlockNoKaiteiNo As String, _
                                           ByVal buhinNoHyoujiJun As Integer, _
                                           ByVal shisakuGousyaHyoujiJun As Integer, _
                                           ByVal shisakuGousya As String, _
                                           ByVal insuSuryo As Integer, _
                                           ByVal Flag As String) Implements KaiteiChusyutuDao.InsertByBuhinEditGousya

            'If insuSuryo = 0 Then
            '    Return
            'End If

            Dim aDate As New ShisakuDate

            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI (" _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_LIST_CODE, " _
            & " SHISAKU_LIST_CODE_KAITEI_NO, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
            & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " BUHIN_NO_HYOUJI_JUN, " _
            & " FLAG, " _
            & " SHISAKU_GOUSYA_HYOUJI_JUN, " _
            & " SHISAKU_GOUSYA, " _
            & " INSU_SURYO, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & "'" & shisakuEventCode & "', " _
            & "'" & shisakuListCode & "', " _
            & "'" & shisakuListCodeKaiteiNo & "', " _
            & "'" & shisakuBukaCode & "', " _
            & "'" & shisakuBlockNo & "', " _
            & "'" & shisakuBlockNoKaiteiNo & "', " _
            & "'" & zenkaiShisakuBlockNoKaiteiNo & "', " _
            & "'" & buhinNoHyoujiJun & "', " _
            & "'" & Flag & "', " _
            & "'" & shisakuGousyaHyoujiJun & "', " _
            & "'" & shisakuGousya & "', " _
            & "'" & insuSuryo & "', " _
            & "'" & LoginInfo.Now.UserId & "', " _
            & "'" & aDate.CurrentDateDbFormat & "', " _
            & "'" & aDate.CurrentTimeDbFormat & "', " _
            & "'" & LoginInfo.Now.UserId & "', " _
            & "'" & aDate.CurrentDateDbFormat & "', " _
            & "'" & aDate.CurrentTimeDbFormat & "' " _
            & " ) "

            Dim db As New EBomDbClient

            Dim uSql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI " _
            & " SET " _
            & " INSU_SURYO = INSU_SURYO + " & insuSuryo _
            & " WHERE  " _
            & " SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' " _
            & " AND SHISAKU_LIST_CODE = '" & shisakuListCode & "' " _
            & " AND SHISAKU_LIST_CODE_KAITEI_NO = '" & shisakuListCodeKaiteiNo & "' " _
            & " AND SHISAKU_BUKA_CODE = '" & shisakuBukaCode & "' " _
            & " AND SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "' " _
            & " AND SHISAKU_BLOCK_NO_KAITEI_NO = '" & shisakuBlockNoKaiteiNo & "' " _
            & " AND ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO = '" & zenkaiShisakuBlockNoKaiteiNo & "' " _
            & " AND BUHIN_NO_HYOUJI_JUN = '" & buhinNoHyoujiJun & "' " _
            & " AND SHISAKU_GOUSYA_HYOUJI_JUN = '" & shisakuGousyaHyoujiJun & "' " _
            & " AND FLAG = '" & Flag & "'"


            Try
                db.Insert(sql)
            Catch ex As SqlClient.SqlException
                Dim a As String = "a"
                'db.Update(uSql)
                'プライマリキー違反なら員数を追加する処理を行う'
                'UpdateByBuhinEditGousya(param)
            End Try
        End Sub

        ''' <summary>
        ''' 員数追加して更新する
        ''' </summary>
        ''' <param name="buhinEditGousyaKaiteiVo">部品編集号車改訂情報</param>
        ''' <remarks></remarks>
        Private Sub UpdateByBuhinEditGousya(ByVal buhinEditGousyaKaiteiVo As TShisakuBuhinEditGousyaKaiteiVo)
            Dim Fsql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI K " _
            & " WHERE  " _
            & " K.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND K.SHISAKU_LIST_CODE = @ShisakuListCode " _
            & " AND K.SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo " _
            & " AND K.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND K.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND K.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
            & " AND K.ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO = @ZenkaiShisakuBlockNoKaiteiNo " _
            & " AND K.BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
            & " AND K.SHISAKU_GOUSYA_HYOUJI_JUN = @ShisakuGousyaHyoujiJun "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuBuhinEditGousyaKaiteiVo
            param.ShisakuEventCode = buhinEditGousyaKaiteiVo.ShisakuEventCode
            param.ShisakuListCode = buhinEditGousyaKaiteiVo.ShisakuListCode
            param.ShisakuListCodeKaiteiNo = buhinEditGousyaKaiteiVo.ShisakuListCodeKaiteiNo
            param.ShisakuBukaCode = buhinEditGousyaKaiteiVo.ShisakuBukaCode
            param.ShisakuBlockNo = buhinEditGousyaKaiteiVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = buhinEditGousyaKaiteiVo.ShisakuBlockNoKaiteiNo
            param.ZenkaiShisakuBlockNoKaiteiNo = buhinEditGousyaKaiteiVo.ZenkaiShisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = buhinEditGousyaKaiteiVo.BuhinNoHyoujiJun
            param.ShisakuGousyaHyoujiJun = buhinEditGousyaKaiteiVo.ShisakuGousyaHyoujiJun

            Dim result As New TShisakuBuhinEditGousyaKaiteiVo
            result = db.QueryForObject(Of TShisakuBuhinEditGousyaKaiteiVo)(Fsql, param)

            If result Is Nothing Then
                Return
            Else
                Dim Usql As String = _
                " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI " _
                & " SET " _
                & " INSU_SURYO = @InsuSuryo " _
                & " WHERE  " _
                & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & " AND SHISAKU_LIST_CODE = @ShisakuListCode " _
                & " AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo " _
                & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
                & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
                & " AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
                & " AND ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO = @ZenkaiShisakuBlockNoKaiteiNo " _
                & " AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
                & " AND SHISAKU_GOUSYA_HYOUJI_JUN = @ShisakuGousyaHyoujiJun "

                If result.InsuSuryo < 0 Then
                    param.InsuSuryo = -1
                Else
                    If result.InsuSuryo < 0 Then
                        param.InsuSuryo = -1
                    Else
                        param.InsuSuryo = buhinEditGousyaKaiteiVo.InsuSuryo + result.InsuSuryo
                    End If
                End If

                db.Update(Usql, param)
            End If
        End Sub

        ''' <summary>
        ''' 部品編集改訂情報を追加する(削除)
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="zenkaiBlockNoKaiteiNo">前回のブロックNo改訂No</param>
        ''' <param name="zenkaibuhinNoHyoujiJun">前回の部品番号表示順</param>
        ''' <param name="zenkaiBuhinEditVo">部品編集ベース情報</param>
        ''' <param name="Flag">フラグ</param>
        ''' <remarks></remarks>
        Public Sub InsertByBuhinEditKaiteiDel(ByVal shisakuEventCode As String, _
                                              ByVal shisakuListCode As String, _
                                              ByVal shisakuListCodeKaiteiNo As String, _
                                              ByVal shisakuBukaCode As String, _
                                              ByVal shisakuBlockNo As String, _
                                              ByVal shisakuBlockNoKaiteiNo As String, _
                                              ByVal zenkaiBlockNoKaiteiNo As String, _
                                              ByVal zenkaibuhinNoHyoujiJun As Integer, _
                                              ByVal zenkaiBuhinEditVo As TShisakuBuhinEditVo, _
                                              ByVal Flag As String) Implements KaiteiChusyutuDao.InsertByBuhinEditKaiteiDel

            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI (" _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_LIST_CODE, " _
            & " SHISAKU_LIST_CODE_KAITEI_NO, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
            & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " BUHIN_NO_HYOUJI_JUN, " _
            & " FLAG, " _
            & " LEVEL, " _
            & " SHUKEI_CODE, " _
            & " SIA_SHUKEI_CODE, " _
            & " GENCYO_CKD_KBN, " _
            & " KYOUKU_SECTION, " _
            & " MAKER_CODE, " _
            & " MAKER_NAME, " _
            & " BUHIN_NO, " _
            & " BUHIN_NO_KBN, " _
            & " BUHIN_NO_KAITEI_NO, " _
            & " EDA_BAN, " _
            & " BUHIN_NAME, " _
            & " SAISHIYOUFUKA, " _
            & " GOUSYA_HACHU_TENKAI_FLG, " _
            & " GOUSYA_HACHU_TENKAI_UNIT_KBN, " _
            & " SHUTUZU_YOTEI_DATE, " _
            & " SHUTUZU_JISEKI_DATE, " _
            & " SHUTUZU_JISEKI_KAITEI_NO, " _
            & " SHUTUZU_JISEKI_STSR_DHSTBA, " _
            & " SAISYU_SETSUHEN_DATE, " _
            & " SAISYU_SETSUHEN_KAITEI_NO, " _
            & " STSR_DHSTBA, " _
            & " ZAISHITU_KIKAKU_1, " _
            & " ZAISHITU_KIKAKU_2, " _
            & " ZAISHITU_KIKAKU_3, " _
            & " ZAISHITU_MEKKI, " _
                    & " TSUKURIKATA_SEISAKU, " _
                    & " TSUKURIKATA_KATASHIYOU_1, " _
                    & " TSUKURIKATA_KATASHIYOU_2, " _
                    & " TSUKURIKATA_KATASHIYOU_3, " _
                    & " TSUKURIKATA_TIGU, " _
                    & " TSUKURIKATA_NOUNYU, " _
                    & " TSUKURIKATA_KIBO, " _
            & " SHISAKU_BANKO_SURYO, " _
            & " SHISAKU_BANKO_SURYO_U, " _
            & " MATERIAL_INFO_LENGTH, " _
            & " MATERIAL_INFO_WIDTH, " _
            & " ZAIRYO_SUNPO_X, " _
            & " ZAIRYO_SUNPO_Y, " _
            & " ZAIRYO_SUNPO_Z, " _
            & " ZAIRYO_SUNPO_XY, " _
            & " ZAIRYO_SUNPO_XZ, " _
            & " ZAIRYO_SUNPO_YZ, " _
            & " MATERIAL_INFO_ORDER_TARGET, " _
            & " MATERIAL_INFO_ORDER_TARGET_DATE, " _
            & " MATERIAL_INFO_ORDER_CHK, " _
            & " MATERIAL_INFO_ORDER_CHK_DATE, " _
            & " DATA_ITEM_KAITEI_NO, " _
            & " DATA_ITEM_AREA_NAME, " _
            & " DATA_ITEM_SET_NAME, " _
            & " DATA_ITEM_KAITEI_INFO, " _
            & " DATA_ITEM_DATA_PROVISION, " _
            & " DATA_ITEM_DATA_PROVISION_DATE, " _
            & " SHISAKU_BUHIN_HI, " _
            & " SHISAKU_KATA_HI, " _
            & " BIKOU, " _
            & " BIKOU_NOTE, " _
            & " EDIT_TOUROKUBI, " _
            & " EDIT_TOUROKUJIKAN, " _
            & " KAITEI_HANDAN_FLG, " _
            & " AUTO_ORIKOMI_KAITEI_NO, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & " @ShisakuEventCode, " _
            & " @ShisakuListCode, " _
            & " @ShisakuListCodeKaiteiNo, " _
            & " @ShisakuBukaCode, " _
            & " @ShisakuBlockNo, " _
            & " @ShisakuBlockNoKaiteiNo, " _
            & " @ZenkaiShisakuBlockNoKaiteiNo, " _
            & " @BuhinNoHyoujiJun, " _
            & " @Flag, " _
            & " @Level, " _
            & " @ShukeiCode, " _
            & " @SiaShukeiCode, " _
            & " @GencyoCkdKbn, " _
            & " @KyoukuSection, " _
            & " @MakerCode, " _
            & " @MakerName, " _
            & " @BuhinNo, " _
            & " @BuhinNoKbn, " _
            & " @BuhinNoKaiteiNo, " _
            & " @EdaBan, " _
            & " @BuhinName, " _
            & " @Saishiyoufuka, " _
            & " @GousyaHachuTenkaiFlg, " _
            & " @GousyaHachuTenkaiUnitKbn, " _
            & " @ShutuzuYoteiDate, " _
            & " @ShutuzuJisekiDate," _
            & " @ShutuzuJisekiKaiteiNo," _
            & " @ShutuzuJisekiStsrDhstba," _
            & " @SaisyuSetsuhenDate," _
            & " @SaisyuSetsuhenKaiteiNo," _
            & " @StsrDhstba," _
            & " @ZaishituKikaku1, " _
            & " @ZaishituKikaku2, " _
            & " @ZaishituKikaku3, " _
            & " @ZaishituMekki, " _
            & " @TsukurikataSeisaku, " _
            & " @TsukurikataKatashiyou1, " _
            & " @TsukurikataKatashiyou2, " _
            & " @TsukurikataKatashiyou3, " _
            & " @TsukurikataTigu, " _
            & " @TsukurikataNounyu, " _
            & " @TsukurikataKibo, " _
            & " @ShisakuBankoSuryo, " _
            & " @ShisakuBankoSuryoU, " _
            & " @MaterialInfoLength, " _
            & " @MaterialInfoWidth, " _
            & " @ZairyoSunpoX, " _
            & " @ZairyoSunpoY, " _
            & " @ZairyoSunpoZ, " _
            & " @ZairyoSunpoXy, " _
            & " @ZairyoSunpoXz, " _
            & " @ZairyoSunpoYz, " _
            & " @MaterialInfoOrderTarget, " _
            & " @MaterialInfoOrderTargetDate, " _
            & " @MaterialInfoOrderChk, " _
            & " @MaterialInfoOrderChkDate, " _
            & " @DataItemKaiteiNo, " _
            & " @DataItemAreaName, " _
            & " @DataItemSetName, " _
            & " @DataItemKaiteiInfo, " _
            & " @DataItemDataProvision, " _
            & " @DataItemDataProvisionDate " _
            & " @ShisakuBuhinHi, " _
            & " @ShisakuKataHi, " _
            & " @Bikou, " _
            & " @BuhinNote, " _
            & " @EditTourokubi, " _
            & " @EditTourokujikan, " _
            & " @KaiteiHandanFlg, " _
            & " @AutoOrikomiKaiteiNo, " _
            & " @CreatedUserId, " _
            & " @CreatedDate, " _
            & " @CreatedTime, " _
            & " @UpdatedUserId, " _
            & " @UpdatedDate, " _
            & " @UpdatedTime ," _
            & " ) "
            '↑↑2014/09/26 酒井 ADD 作り方項目
            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuBuhinEditKaiteiVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
            param.ZenkaiShisakuBlockNoKaiteiNo = zenkaiBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = zenkaibuhinNoHyoujiJun
            param.Flag = Flag
            param.Level = zenkaiBuhinEditVo.Level
            param.ShukeiCode = zenkaiBuhinEditVo.ShukeiCode
            param.SiaShukeiCode = zenkaiBuhinEditVo.SiaShukeiCode
            param.GencyoCkdKbn = zenkaiBuhinEditVo.GencyoCkdKbn
            param.KyoukuSection = zenkaiBuhinEditVo.KyoukuSection
            param.MakerCode = zenkaiBuhinEditVo.MakerCode
            param.MakerName = zenkaiBuhinEditVo.MakerName
            param.BuhinNo = zenkaiBuhinEditVo.BuhinNo
            param.BuhinNoKbn = zenkaiBuhinEditVo.BuhinNoKbn
            param.BuhinNoKaiteiNo = zenkaiBuhinEditVo.BuhinNoKaiteiNo
            param.EdaBan = zenkaiBuhinEditVo.EdaBan
            param.BuhinName = zenkaiBuhinEditVo.BuhinName
            param.Saishiyoufuka = zenkaiBuhinEditVo.Saishiyoufuka
            param.ShutuzuYoteiDate = zenkaiBuhinEditVo.ShutuzuYoteiDate
            '↓↓2014/09/26 酒井 ADD BEGIN
            param.TsukurikataSeisaku = zenkaiBuhinEditVo.TsukurikataSeisaku
            param.TsukurikataKatashiyou1 = zenkaiBuhinEditVo.TsukurikataKatashiyou1
            param.TsukurikataKatashiyou2 = zenkaiBuhinEditVo.TsukurikataKatashiyou2
            param.TsukurikataKatashiyou3 = zenkaiBuhinEditVo.TsukurikataKatashiyou3
            param.TsukurikataTigu = zenkaiBuhinEditVo.TsukurikataTigu
            param.TsukurikataNounyu = zenkaiBuhinEditVo.TsukurikataNounyu
            param.TsukurikataKibo = zenkaiBuhinEditVo.TsukurikataKibo
            '↑↑2014/09/26 酒井 ADD END
            param.ZaishituKikaku1 = zenkaiBuhinEditVo.ZaishituKikaku1
            param.ZaishituKikaku2 = zenkaiBuhinEditVo.ZaishituKikaku2
            param.ZaishituKikaku3 = zenkaiBuhinEditVo.ZaishituKikaku3
            param.ZaishituMekki = zenkaiBuhinEditVo.ZaishituMekki
            param.ShisakuBankoSuryo = zenkaiBuhinEditVo.ShisakuBankoSuryo
            param.ShisakuBankoSuryoU = zenkaiBuhinEditVo.ShisakuBankoSuryoU


            ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
            param.MaterialInfoLength = zenkaiBuhinEditVo.MaterialInfoLength
            param.MaterialInfoWidth = zenkaiBuhinEditVo.MaterialInfoWidth
            param.DataItemKaiteiNo = zenkaiBuhinEditVo.DataItemKaiteiNo
            param.DataItemAreaName = zenkaiBuhinEditVo.DataItemAreaName
            param.DataItemSetName = zenkaiBuhinEditVo.DataItemSetName
            param.DataItemKaiteiInfo = zenkaiBuhinEditVo.DataItemKaiteiInfo
            ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


            param.ShisakuBuhinHi = zenkaiBuhinEditVo.ShisakuBuhinHi
            param.ShisakuKataHi = zenkaiBuhinEditVo.ShisakuKataHi
            param.Bikou = zenkaiBuhinEditVo.Bikou
            param.EditTourokubi = zenkaiBuhinEditVo.EditTourokubi
            param.EditTourokujikan = zenkaiBuhinEditVo.EditTourokujikan
            param.KaiteiHandanFlg = zenkaiBuhinEditVo.KaiteiHandanFlg

            param.CreatedUserId = LoginInfo.Now.UserId
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            param.GousyaHachuTenkaiFlg = ""
            param.GousyaHachuTenkaiUnitKbn = ""
            param.ShutuzuJisekiDate = 0
            param.ShutuzuJisekiKaiteiNo = ""
            param.ShutuzuJisekiStsrDhstba = ""
            param.SaisyuSetsuhenDate = 0
            param.SaisyuSetsuhenKaiteiNo = ""
            param.StsrDhstba = ""
            param.ZairyoSunpoX = 0
            param.ZairyoSunpoY = 0
            param.ZairyoSunpoZ = 0
            param.ZairyoSunpoXy = 0
            param.ZairyoSunpoXz = 0
            param.ZairyoSunpoYz = 0

            Try
                db.Insert(sql, param)
            Catch ex As Exception
                Return
            End Try
        End Sub

        ''' <summary>
        ''' 部品編集号車改訂情報を追加する(削除)
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <param name="zenkaiShisakuBlockNoKaiteiNo">前回試作ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="zenkaishisakuGousyaHyoujiJun">前回試作号車表示順</param>
        ''' <param name="zenkaishisakuGousya">前回試作号車</param>
        ''' <param name="zenkaiInsuSuryo">前回員数数量</param>
        ''' <param name="Flag">フラグ</param>
        ''' <remarks></remarks>
        Public Sub InsertByBuhinEditGousyaKaiteiDel(ByVal shisakuEventCode As String, _
                                                    ByVal shisakuListCode As String, _
                                                    ByVal shisakuListCodeKaiteiNo As String, _
                                                    ByVal shisakuBukaCode As String, _
                                                    ByVal shisakuBlockNo As String, _
                                                    ByVal shisakuBlockNoKaiteiNo As String, _
                                                    ByVal zenkaiShisakuBlockNoKaiteiNo As String, _
                                                    ByVal buhinNoHyoujiJun As Integer, _
                                                    ByVal zenkaishisakuGousyaHyoujiJun As Integer, _
                                                    ByVal zenkaishisakuGousya As String, _
                                                    ByVal zenkaiInsuSuryo As Integer, _
                                                    ByVal Flag As String) Implements KaiteiChusyutuDao.InsertByBuhinEditGousyaKaiteiDel

            'If zenkaiInsuSuryo = 0 Then
            '    Return
            'End If

            Dim aDate As New ShisakuDate

            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI (" _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_LIST_CODE, " _
            & " SHISAKU_LIST_CODE_KAITEI_NO, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
            & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " BUHIN_NO_HYOUJI_JUN, " _
            & " FLAG, " _
            & " SHISAKU_GOUSYA_HYOUJI_JUN, " _
            & " SHISAKU_GOUSYA, " _
            & " INSU_SURYO, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & "'" & shisakuEventCode & "' , " _
            & "'" & shisakuListCode & "' , " _
            & "'" & shisakuListCodeKaiteiNo & "' , " _
            & "'" & shisakuBukaCode & "' , " _
            & "'" & shisakuBlockNo & "' , " _
            & "'" & shisakuBlockNoKaiteiNo & "' , " _
            & "'" & zenkaiShisakuBlockNoKaiteiNo & "' , " _
            & "'" & buhinNoHyoujiJun & "' , " _
            & "'" & Flag & "' , " _
            & "'" & zenkaishisakuGousyaHyoujiJun & "' , " _
            & "'" & zenkaishisakuGousya & "' , " _
            & "'" & zenkaiInsuSuryo & "' , " _
            & "'" & LoginInfo.Now.UserId & "' , " _
            & "'" & aDate.CurrentDateDbFormat & "' , " _
            & "'" & aDate.CurrentTimeDbFormat & "' , " _
            & "'" & LoginInfo.Now.UserId & "' , " _
            & "'" & aDate.CurrentDateDbFormat & "' , " _
            & "'" & aDate.CurrentTimeDbFormat & "'  " _
            & " ) "

            Dim db As New EBomDbClient
            db.Insert(sql)
        End Sub

        ''' <summary>
        ''' 手配帳改訂抽出ブロック情報を更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="konkaiBlockNoKaiteiNo">今回ブロックNo改訂No</param>
        ''' <remarks></remarks>
        Public Sub InsertByTehaiKaitei(ByVal shisakuEventCode As String, _
                                       ByVal shisakuListCode As String, _
                                       ByVal shisakuBukaCode As String, _
                                       ByVal shisakuBlockNo As String, _
                                       ByVal konkaiBlockNoKaiteiNo As String) Implements KaiteiChusyutuDao.InsertByTehaiKaitei
            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KAITEI_BLOCK (" _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_LIST_CODE, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
            & " ZENKAI_BLOCK_NO_KAITEI_NO, " _
            & " KONKAI_BLOCK_NO_KAITEI_NO, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & " @ShisakuEventCode, " _
            & " @ShisakuListCode, " _
            & " @ShisakuBukaCode, " _
            & " @ShisakuBlockNo, " _
            & " @ZenkaiBlockNoKaiteiNo, " _
            & " @KonkaiBlockNoKaiteiNo, " _
            & " @CreatedUserId, " _
            & " @CreatedDate, " _
            & " @CreatedTime, " _
            & " @UpdatedUserId, " _
            & " @UpdatedDate, " _
            & " @UpdatedTime " _
            & " ) "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuTehaiKaiteiBlockVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.KonkaiBlockNoKaiteiNo = konkaiBlockNoKaiteiNo
            param.ZenkaiBlockNoKaiteiNo = ""

            param.CreatedUserId = LoginInfo.Now.UserId
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Insert(sql, param)
        End Sub

        ''' <summary>
        ''' 部品編集改訂情報を追加する(削除前回)
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="zenkaibuhinEditVo">前回部品編集情報</param>
        ''' <param name="Flag">フラグ</param>
        ''' <remarks></remarks>
        Public Sub InsertByZenkaiBuhinEditKaiteiDel(ByVal shisakuEventCode As String, _
                                    ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String, _
                                    ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, _
                                    ByVal shisakuBlockNoKaiteiNo As String, ByVal buhinNoHyoujiJun As String, _
                                    ByVal zenkaibuhinEditVo As TShisakuBuhinEditVo, ByVal Flag As String) Implements KaiteiChusyutuDao.InsertByZenkaiBuhinEditKaiteiDel
            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI (" _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_LIST_CODE, " _
            & " SHISAKU_LIST_CODE_KAITEI_NO, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
            & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & " BUHIN_NO_HYOUJI_JUN, " _
            & " FLAG, " _
            & " LEVEL, " _
            & " SHUKEI_CODE, " _
            & " SIA_SHUKEI_CODE, " _
            & " GENCYO_CKD_KBN, " _
            & " KYOUKU_SECTION, " _
            & " MAKER_CODE, " _
            & " MAKER_NAME, " _
            & " BUHIN_NO, " _
            & " BUHIN_NO_KBN, " _
            & " BUHIN_NO_KAITEI_NO, " _
            & " EDA_BAN, " _
            & " BUHIN_NAME, " _
            & " SAISHIYOUFUKA, " _
            & " GOUSYA_HACHU_TENKAI_FLG, " _
            & " GOUSYA_HACHU_TENKAI_UNIT_KBN, " _
            & " SHUTUZU_YOTEI_DATE, " _
            & " SHUTUZU_JISEKI_DATE, " _
            & " SHUTUZU_JISEKI_KAITEI_NO, " _
            & " SHUTUZU_JISEKI_STSR_DHSTBA, " _
            & " SAISYU_SETSUHEN_DATE, " _
            & " SAISYU_SETSUHEN_KAITEI_NO, " _
            & " STSR_DHSTBA, " _
            & " ZAISHITU_KIKAKU_1, " _
            & " ZAISHITU_KIKAKU_2, " _
            & " ZAISHITU_KIKAKU_3, " _
            & " ZAISHITU_MEKKI, " _
                    & " TSUKURIKATA_SEISAKU, " _
                    & " TSUKURIKATA_KATASHIYOU_1, " _
                    & " TSUKURIKATA_KATASHIYOU_2, " _
                    & " TSUKURIKATA_KATASHIYOU_3, " _
                    & " TSUKURIKATA_TIGU, " _
                    & " TSUKURIKATA_NOUNYU, " _
                    & " TSUKURIKATA_KIBO, " _
            & " SHISAKU_BANKO_SURYO, " _
            & " SHISAKU_BANKO_SURYO_U, " _
            & " MATERIAL_INFO_LENGTH, " _
            & " MATERIAL_INFO_WIDTH, " _
            & " ZAIRYO_SUNPO_X, " _
            & " ZAIRYO_SUNPO_Y, " _
            & " ZAIRYO_SUNPO_Z, " _
            & " ZAIRYO_SUNPO_XY, " _
            & " ZAIRYO_SUNPO_XZ, " _
            & " ZAIRYO_SUNPO_YZ, " _
            & " MATERIAL_INFO_ORDER_TARGET, " _
            & " MATERIAL_INFO_ORDER_TARGET_DATE, " _
            & " MATERIAL_INFO_ORDER_CHK, " _
            & " MATERIAL_INFO_ORDER_CHK_DATE, " _
            & " DATA_ITEM_KAITEI_NO, " _
            & " DATA_ITEM_AREA_NAME, " _
            & " DATA_ITEM_SET_NAME, " _
            & " DATA_ITEM_KAITEI_INFO, " _
            & " DATA_ITEM_DATA_PROVISION, " _
            & " DATA_ITEM_DATA_PROVISION_DATE, " _
            & " SHISAKU_BUHIN_HI, " _
            & " SHISAKU_KATA_HI, " _
            & " BIKOU, " _
            & " BIKOU_NOTE, " _
            & " EDIT_TOUROKUBI, " _
            & " EDIT_TOUROKUJIKAN, " _
            & " KAITEI_HANDAN_FLG, " _
            & " AUTO_ORIKOMI_KAITEI_NO, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & " @ShisakuEventCode, " _
            & " @ShisakuListCode, " _
            & " @ShisakuListCodeKaiteiNo, " _
            & " @ShisakuBukaCode, " _
            & " @ShisakuBlockNo, " _
            & " @ShisakuBlockNoKaiteiNo, " _
            & " @ZenkaiShisakuBlockNoKaiteiNo, " _
            & " @BuhinNoHyoujiJun, " _
            & " @Flag, " _
            & " @Level, " _
            & " @ShukeiCode, " _
            & " @SiaShukeiCode, " _
            & " @GencyoCkdKbn, " _
            & " @KyoukuSection, " _
            & " @MakerCode, " _
            & " @MakerName, " _
            & " @BuhinNo, " _
            & " @BuhinNoKbn, " _
            & " @BuhinNoKaiteiNo, " _
            & " @EdaBan, " _
            & " @BuhinName, " _
            & " @Saishiyoufuka, " _
            & " @GousyaHachuTenkaiFlg, " _
            & " @GousyaHachuTenkaiUnitKbn, " _
            & " @ShutuzuYoteiDate, " _
            & " @ShutuzuJisekiDate," _
            & " @ShutuzuJisekiKaiteiNo," _
            & " @ShutuzuJisekiStsrDhstba," _
            & " @SaisyuSetsuhenDate," _
            & " @SaisyuSetsuhenKaiteiNo," _
            & " @StsrDhstba," _
            & " @ZaishituKikaku1, " _
            & " @ZaishituKikaku2, " _
            & " @ZaishituKikaku3, " _
            & " @ZaishituMekki, " _
            & " @TsukurikataSeisaku, " _
            & " @TsukurikataKatashiyou1, " _
            & " @TsukurikataKatashiyou2, " _
            & " @TsukurikataKatashiyou3, " _
            & " @TsukurikataTigu, " _
            & " @TsukurikataNounyu, " _
            & " @TsukurikataKibo, " _
            & " @ShisakuBankoSuryo, " _
            & " @ShisakuBankoSuryoU, " _
            & " @MaterialInfoLength, " _
            & " @MaterialInfoWidth, " _
            & " @ZairyoSunpoX, " _
            & " @ZairyoSunpoY, " _
            & " @ZairyoSunpoZ, " _
            & " @ZairyoSunpoXy, " _
            & " @ZairyoSunpoXz, " _
            & " @ZairyoSunpoYz, " _
            & " @MaterialInfoOrderTarget, " _
            & " @MaterialInfoOrderTargetDate, " _
            & " @MaterialInfoOrderChk, " _
            & " @MaterialInfoOrderChkDate, " _
            & " @DataItemKaiteiNo, " _
            & " @DataItemAreaName, " _
            & " @DataItemSetName, " _
            & " @DataItemKaiteiInfo, " _
            & " @DataItemDataProvision, " _
            & " @DataItemDataProvisionDate " _
            & " @ShisakuBuhinHi, " _
            & " @ShisakuKataHi, " _
            & " @Bikou, " _
            & " @BuhinNote, " _
            & " @EditTourokubi, " _
            & " @EditTourokujikan, " _
            & " @KaiteiHandanFlg, " _
            & " @AutoOrikomiKaiteiNo, " _
            & " @CreatedUserId, " _
            & " @CreatedDate, " _
            & " @CreatedTime, " _
            & " @UpdatedUserId, " _
            & " @UpdatedDate, " _
            & " @UpdatedTime ," _
            & " ) "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuBuhinEditKaiteiVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
            param.ZenkaiShisakuBlockNoKaiteiNo = zenkaibuhinEditVo.ShisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = buhinNoHyoujiJun
            param.Flag = Flag
            param.Level = zenkaibuhinEditVo.Level
            param.ShukeiCode = zenkaibuhinEditVo.ShukeiCode
            param.SiaShukeiCode = zenkaibuhinEditVo.SiaShukeiCode
            param.GencyoCkdKbn = zenkaibuhinEditVo.GencyoCkdKbn
            param.MakerCode = zenkaibuhinEditVo.MakerCode
            param.MakerName = zenkaibuhinEditVo.MakerName
            param.BuhinNo = zenkaibuhinEditVo.BuhinNo
            param.BuhinNoKbn = zenkaibuhinEditVo.BuhinNoKbn
            param.BuhinNoKaiteiNo = zenkaibuhinEditVo.BuhinNoKaiteiNo
            param.EdaBan = zenkaibuhinEditVo.EdaBan
            param.BuhinName = zenkaibuhinEditVo.BuhinName
            param.Saishiyoufuka = zenkaibuhinEditVo.Saishiyoufuka
            param.ShutuzuYoteiDate = zenkaibuhinEditVo.ShutuzuYoteiDate
            '↓↓2014/09/26 酒井 ADD BEGIN
            param.TsukurikataSeisaku = zenkaibuhinEditVo.TsukurikataSeisaku
            param.TsukurikataKatashiyou1 = zenkaibuhinEditVo.TsukurikataKatashiyou1
            param.TsukurikataKatashiyou2 = zenkaibuhinEditVo.TsukurikataKatashiyou2
            param.TsukurikataKatashiyou3 = zenkaibuhinEditVo.TsukurikataKatashiyou3
            param.TsukurikataTigu = zenkaibuhinEditVo.TsukurikataTigu
            param.TsukurikataNounyu = zenkaibuhinEditVo.TsukurikataNounyu
            param.TsukurikataKibo = zenkaibuhinEditVo.TsukurikataKibo
            '↑↑2014/09/26 酒井 ADD END
            param.ZaishituKikaku1 = zenkaibuhinEditVo.ZaishituKikaku1
            param.ZaishituKikaku2 = zenkaibuhinEditVo.ZaishituKikaku2
            param.ZaishituKikaku3 = zenkaibuhinEditVo.ZaishituKikaku3
            param.ZaishituMekki = zenkaibuhinEditVo.ZaishituMekki
            param.ShisakuBankoSuryo = zenkaibuhinEditVo.ShisakuBankoSuryo
            param.ShisakuBankoSuryoU = zenkaibuhinEditVo.ShisakuBankoSuryoU


            ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
            param.MaterialInfoLength = zenkaibuhinEditVo.MaterialInfoLength
            param.MaterialInfoWidth = zenkaibuhinEditVo.MaterialInfoWidth
            param.DataItemKaiteiNo = zenkaibuhinEditVo.DataItemKaiteiNo
            param.DataItemAreaName = zenkaibuhinEditVo.DataItemAreaName
            param.DataItemSetName = zenkaibuhinEditVo.DataItemSetName
            param.DataItemKaiteiInfo = zenkaibuhinEditVo.DataItemKaiteiInfo
            ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


            param.ShisakuBuhinHi = zenkaibuhinEditVo.ShisakuBuhinHi
            param.ShisakuKataHi = zenkaibuhinEditVo.ShisakuKataHi
            param.Bikou = zenkaibuhinEditVo.Bikou
            param.EditTourokubi = zenkaibuhinEditVo.EditTourokubi
            param.EditTourokujikan = zenkaibuhinEditVo.EditTourokujikan
            param.KaiteiHandanFlg = zenkaibuhinEditVo.KaiteiHandanFlg
            param.AutoOrikomiKaiteiNo = ""
            param.CreatedUserId = LoginInfo.Now.UserId
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            param.GousyaHachuTenkaiFlg = ""
            param.GousyaHachuTenkaiUnitKbn = ""
            param.ShutuzuJisekiDate = 0
            param.ShutuzuJisekiKaiteiNo = ""
            param.ShutuzuJisekiStsrDhstba = ""
            param.SaisyuSetsuhenDate = 0
            param.SaisyuSetsuhenKaiteiNo = ""
            param.StsrDhstba = ""
            param.ZairyoSunpoX = 0
            param.ZairyoSunpoY = 0
            param.ZairyoSunpoZ = 0
            param.ZairyoSunpoXy = 0
            param.ZairyoSunpoXz = 0
            param.ZairyoSunpoYz = 0

            db.Insert(sql, param)
        End Sub




        ''' <summary>
        ''' 手配帳形式にするために追加
        ''' </summary>
        ''' <param name="BuhinEditVoList"></param>
        ''' <remarks></remarks>
        Public Sub InsertByBuhinEditTehaiKaitei(ByVal BuhinEditVoList As List(Of TShisakuBuhinEditVoSekkeiHelper), ByVal zenkaiFlag As Boolean) Implements KaiteiChusyutuDao.InsertByBuhinEditTehaiKaitei

            Dim sqlList(BuhinEditVoList.Count - 1) As String
            Dim aDate As New ShisakuDate
            Dim zenkai As String = ""
            If zenkaiFlag Then
                zenkai = "KON"
            Else
                zenkai = "ZEN"
            End If

            Dim pkCheck As New ArrayList
            Dim wkPk As New System.Text.StringBuilder
            Dim insData As New List(Of TShisakuBuhinEditKaiteiVo)

            For Each BuhinEditVo As TShisakuBuhinEditVoSekkeiHelper In BuhinEditVoList
                With wkPk
                    .Remove(0, .Length)
                    .AppendLine(BuhinEditVo.ShisakuEventCode)
                    .AppendLine(zenkai)
                    .AppendLine(BuhinEditVo.ShisakuBukaCode)
                    .AppendLine(BuhinEditVo.ShisakuBlockNo)
                    .AppendLine(CStr(BuhinEditVo.BuhinNoHyoujiJun))
                End With
                If pkCheck.Contains(wkPk.ToString) Then
                    Continue For
                Else
                    pkCheck.Add(wkPk.ToString)
                End If

                '不正文字(')が入ってくることがあるので
                '   半角スペースに置き換える。
                If StringUtil.IsNotEmpty(BuhinEditVo.Bikou) Then
                    BuhinEditVo.Bikou = BuhinEditVo.Bikou.Replace("'", " ")
                End If

                Dim vo As New TShisakuBuhinEditKaiteiVo
                With BuhinEditVo
                    vo.ShisakuEventCode = .ShisakuEventCode
                    vo.ShisakuListCode = zenkai
                    vo.ShisakuListCodeKaiteiNo = zenkai
                    vo.ShisakuBukaCode = .ShisakuBukaCode
                    vo.ShisakuBlockNo = .ShisakuBlockNo
                    vo.ShisakuBlockNoKaiteiNo = .ShisakuBlockNoKaiteiNo
                    vo.ZenkaiShisakuBlockNoKaiteiNo = ""
                    vo.BuhinNoHyoujiJun = .BuhinNoHyoujiJun
                    vo.Flag = "9"
                    vo.Level = .Level
                    vo.ShukeiCode = .ShukeiCode
                    vo.SiaShukeiCode = .SiaShukeiCode
                    vo.GencyoCkdKbn = .GencyoCkdKbn
                    vo.KyoukuSection = .KyoukuSection
                    vo.MakerCode = .MakerCode
                    vo.MakerName = .MakerName
                    vo.BuhinNo = .BuhinNo
                    vo.BuhinNoKbn = .BuhinNoKbn
                    vo.BuhinNoKaiteiNo = .BuhinNoKaiteiNo
                    vo.EdaBan = .EdaBan
                    vo.BuhinName = .BuhinName
                    vo.Saishiyoufuka = .Saishiyoufuka
                    vo.GousyaHachuTenkaiFlg = ""
                    vo.GousyaHachuTenkaiUnitKbn = ""
                    vo.ShutuzuYoteiDate = .ShutuzuYoteiDate
                    vo.ShutuzuJisekiDate = 0
                    vo.ShutuzuJisekiKaiteiNo = ""
                    vo.ShutuzuJisekiStsrDhstba = ""
                    vo.SaisyuSetsuhenDate = 0
                    vo.SaisyuSetsuhenKaiteiNo = ""
                    vo.StsrDhstba = ""
                    vo.ZaishituKikaku1 = .ZaishituKikaku1
                    vo.ZaishituKikaku2 = .ZaishituKikaku2
                    vo.ZaishituKikaku3 = .ZaishituKikaku3
                    vo.ZaishituMekki = .ZaishituMekki
                    vo.TsukurikataSeisaku = .TsukurikataSeisaku
                    vo.TsukurikataKatashiyou1 = .TsukurikataKatashiyou1
                    vo.TsukurikataKatashiyou2 = .TsukurikataKatashiyou2
                    vo.TsukurikataKatashiyou3 = .TsukurikataKatashiyou3
                    vo.TsukurikataTigu = .TsukurikataTigu
                    vo.TsukurikataNounyu = .TsukurikataNounyu
                    vo.TsukurikataKibo = .TsukurikataKibo
                    vo.ShisakuBankoSuryo = .ShisakuBankoSuryo
                    vo.ShisakuBankoSuryoU = .ShisakuBankoSuryoU
                    vo.MaterialInfoLength = .MaterialInfoLength
                    vo.MaterialInfoWidth = .MaterialInfoWidth
                    vo.ZairyoSunpoX = 0
                    vo.ZairyoSunpoY = 0
                    vo.ZairyoSunpoZ = 0
                    vo.ZairyoSunpoXy = 0
                    vo.ZairyoSunpoXz = 0
                    vo.ZairyoSunpoYz = 0
                    vo.MaterialInfoOrderTarget = ""
                    vo.MaterialInfoOrderTargetDate = ""
                    vo.MaterialInfoOrderChk = ""
                    vo.MaterialInfoOrderChkDate = ""
                    vo.DataItemKaiteiNo = .DataItemKaiteiNo
                    vo.DataItemAreaName = .DataItemAreaName
                    vo.DataItemSetName = .DataItemSetName
                    vo.DataItemKaiteiInfo = .DataItemKaiteiInfo
                    vo.DataItemDataProvision = ""
                    vo.DataItemDataProvisionDate = ""
                    vo.ShisakuBuhinHi = .ShisakuBuhinHi
                    vo.ShisakuKataHi = .ShisakuKataHi
                    vo.Bikou = .Bikou
                    vo.BuhinNote = ""
                    vo.EditTourokubi = .EditTourokubi
                    vo.EditTourokujikan = .EditTourokujikan
                    vo.KaiteiHandanFlg = .KaiteiHandanFlg
                    vo.AutoOrikomiKaiteiNo = ""
                    vo.CreatedUserId = LoginInfo.Now.UserId
                    vo.CreatedDate = aDate.CurrentDateDbFormat
                    vo.CreatedTime = aDate.CurrentTimeDbFormat
                    vo.UpdatedUserId = LoginInfo.Now.UserId
                    vo.UpdatedDate = aDate.CurrentDateDbFormat
                    vo.UpdatedTime = aDate.CurrentTimeDbFormat
                End With
                insData.Add(vo)

                'Dim sql As String = _
                '" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI (" _
                '& " SHISAKU_EVENT_CODE, " _
                '& " SHISAKU_LIST_CODE, " _
                '& " SHISAKU_LIST_CODE_KAITEI_NO, " _
                '& " SHISAKU_BUKA_CODE, " _
                '& " SHISAKU_BLOCK_NO, " _
                '& " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                '& " ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO, " _
                '& " BUHIN_NO_HYOUJI_JUN, " _
                '& " FLAG, " _
                '& " LEVEL, " _
                '& " SHUKEI_CODE, " _
                '& " SIA_SHUKEI_CODE, " _
                '& " GENCYO_CKD_KBN, " _
                '& " KYOUKU_SECTION, " _
                '& " MAKER_CODE, " _
                '& " MAKER_NAME, " _
                '& " BUHIN_NO, " _
                '& " BUHIN_NO_KBN, " _
                '& " BUHIN_NO_KAITEI_NO, " _
                '& " EDA_BAN, " _
                '& " BUHIN_NAME, " _
                '& " SAISHIYOUFUKA, " _
                '& " SHUTUZU_YOTEI_DATE, " _
                '& " TSUKURIKATA_SEISAKU, " _
                '& " TSUKURIKATA_KATASHIYOU_1, " _
                '& " TSUKURIKATA_KATASHIYOU_2, " _
                '& " TSUKURIKATA_KATASHIYOU_3, " _
                '& " TSUKURIKATA_TIGU, " _
                '& " TSUKURIKATA_NOUNYU, " _
                '& " TSUKURIKATA_KIBO, " _
                '& " ZAISHITU_KIKAKU_1, " _
                '& " ZAISHITU_KIKAKU_2, " _
                '& " ZAISHITU_KIKAKU_3, " _
                '& " ZAISHITU_MEKKI, " _
                '& " SHISAKU_BANKO_SURYO, " _
                '& " SHISAKU_BANKO_SURYO_U, " _
                '& " MATERIAL_INFO_LENGTH, " _
                '& " MATERIAL_INFO_WIDTH, " _
                '& " DATA_ITEM_KAITEI_NO, " _
                '& " DATA_ITEM_AREA_NAME, " _
                '& " DATA_ITEM_SET_NAME, " _
                '& " DATA_ITEM_KAITEI_INFO, " _
                '& " SHISAKU_BUHIN_HI, " _
                '& " SHISAKU_KATA_HI, " _
                '& " BIKOU, " _
                '& " EDIT_TOUROKUBI, " _
                '& " EDIT_TOUROKUJIKAN, " _
                '& " KAITEI_HANDAN_FLG, " _
                '& " AUTO_ORIKOMI_KAITEI_NO, " _
                '& " CREATED_USER_ID, " _
                '& " CREATED_DATE, " _
                '& " CREATED_TIME, " _
                '& " UPDATED_USER_ID, " _
                '& " UPDATED_DATE, " _
                '& " UPDATED_TIME " _
                '& " ) " _
                '& " VALUES ( " _
                '& "'" & BuhinEditVoList(index).ShisakuEventCode & "', " _
                '& "'" & zenkai & "', " _
                '& "'" & zenkai & "', " _
                '& "'" & BuhinEditVoList(index).ShisakuBukaCode & "', " _
                '& "'" & BuhinEditVoList(index).ShisakuBlockNo & "', " _
                '& "'" & BuhinEditVoList(index).ShisakuBlockNoKaiteiNo & "', " _
                '& " '', " _
                '& "'" & BuhinEditVoList(index).BuhinNoHyoujiJun & "', " _
                '& " '9', " _
                '& "'" & BuhinEditVoList(index).Level & "', " _
                '& "'" & BuhinEditVoList(index).ShukeiCode & "', " _
                '& "'" & BuhinEditVoList(index).SiaShukeiCode & "', " _
                '& "'" & BuhinEditVoList(index).GencyoCkdKbn & "', " _
                '& "'" & BuhinEditVoList(index).KyoukuSection & "', " _
                '& "'" & BuhinEditVoList(index).MakerCode & "', " _
                '& "'" & BuhinEditVoList(index).MakerName & "', " _
                '& "'" & BuhinEditVoList(index).BuhinNo & "', " _
                '& "'" & BuhinEditVoList(index).BuhinNoKbn & "', " _
                '& "'" & BuhinEditVoList(index).BuhinNoKaiteiNo & "', " _
                '& "'" & BuhinEditVoList(index).EdaBan & "', " _
                '& "'" & BuhinEditVoList(index).BuhinName & "', " _
                '& "'" & BuhinEditVoList(index).Saishiyoufuka & "', " _
                '& "'" & BuhinEditVoList(index).ShutuzuYoteiDate & "', " _
                '& "'" & BuhinEditVoList(index).TsukurikataSeisaku & "', " _
                '& "'" & BuhinEditVoList(index).TsukurikataKatashiyou1 & "', " _
                '& "'" & BuhinEditVoList(index).TsukurikataKatashiyou2 & "', " _
                '& "'" & BuhinEditVoList(index).TsukurikataKatashiyou3 & "', " _
                '& "'" & BuhinEditVoList(index).TsukurikataTigu & "', " _
                '& "'" & BuhinEditVoList(index).TsukurikataNounyu & "', " _
                '& "'" & BuhinEditVoList(index).TsukurikataKibo & "', " _
                '& "'" & BuhinEditVoList(index).ZaishituKikaku1 & "', " _
                '& "'" & BuhinEditVoList(index).ZaishituKikaku2 & "', " _
                '& "'" & BuhinEditVoList(index).ZaishituKikaku3 & "', " _
                '& "'" & BuhinEditVoList(index).ZaishituMekki & "', " _
                '& "'" & BuhinEditVoList(index).ShisakuBankoSuryo & "', " _
                '& "'" & BuhinEditVoList(index).ShisakuBankoSuryoU & "', " _
                '& "'" & BuhinEditVoList(index).MaterialInfoLength & "', " _
                '& "'" & BuhinEditVoList(index).MaterialInfoWidth & "', " _
                '& "'" & BuhinEditVoList(index).DataItemKaiteiNo & "', " _
                '& "'" & BuhinEditVoList(index).DataItemAreaName & "', " _
                '& "'" & BuhinEditVoList(index).DataItemSetName & "', " _
                '& "'" & BuhinEditVoList(index).DataItemKaiteiInfo & "', " _
                '& "'" & BuhinEditVoList(index).ShisakuBuhinHi & "', " _
                '& "'" & BuhinEditVoList(index).ShisakuKataHi & "', " _
                '& "'" & BuhinEditVoList(index).Bikou & "', " _
                '& "'" & BuhinEditVoList(index).EditTourokubi & "', " _
                '& "'" & BuhinEditVoList(index).EditTourokujikan & "', " _
                '& "'" & BuhinEditVoList(index).KaiteiHandanFlg & "', " _
                '& "'', " _
                '& "'" & LoginInfo.Now.UserId & "', " _
                '& "'" & aDate.CurrentDateDbFormat & "', " _
                '& "'" & aDate.CurrentTimeDbFormat & "', " _
                '& "'" & LoginInfo.Now.UserId & "', " _
                '& "'" & aDate.CurrentDateDbFormat & "', " _
                '& "'" & aDate.CurrentTimeDbFormat & "' " _
                '& " ) "
                ''↑↑2014/09/26 酒井 ADD 作り方項目
                'sqlList(index) = sql
            Next

            'Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            '    insert.Open()
            '    insert.BeginTransaction()
            '    For index As Integer = 0 To BuhinEditVoList.Count - 1
            '        Try
            '            If Not StringUtil.IsEmpty(sqlList(index)) Then
            '                insert.ExecuteNonQuery(sqlList(index))
            '            End If
            '        Catch ex As SqlClient.SqlException
            '            Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
            '            If prm < 0 Then
            '                Dim msg As String = sqlList(index) + ex.Message
            '                MsgBox(ex.Message)
            '            Else
            '                Continue For
            '            End If
            '        End Try
            '    Next
            '    insert.Commit()
            'End Using

            Using sqlConn As New System.Data.SqlClient.SqlConnection(NitteiDbComFunc.GetConnectString)
                sqlConn.Open()
                Using trans As SqlClient.SqlTransaction = sqlConn.BeginTransaction
                    Try
                        Using addData As DataTable = NitteiDbComFunc.ConvListToDataTable(insData)
                            Using bulkCopy As System.Data.SqlClient.SqlBulkCopy = New System.Data.SqlClient.SqlBulkCopy(sqlConn, System.Data.SqlClient.SqlBulkCopyOptions.KeepIdentity, trans)
                                NitteiDbComFunc.SetColumnMappings(bulkCopy, addData)
                                bulkCopy.BulkCopyTimeout = 0  ' in seconds
                                bulkCopy.DestinationTableName = "dbo.T_SHISAKU_BUHIN_EDIT_KAITEI"
                                bulkCopy.WriteToServer(addData)
                                bulkCopy.Close()
                            End Using
                        End Using
                        trans.Commit()
                    Catch ex As Exception
                        MsgBox(ex.Message)
                        trans.Rollback()
                    End Try
                End Using
            End Using

        End Sub

        ''' <summary>
        ''' 手配帳形式にするために追加
        ''' </summary>
        ''' <param name="BuhinEditVoList"></param>
        ''' <remarks></remarks>
        Public Sub InsertByBuhinEditGousyaKaitei(ByVal BuhinEditVoList As List(Of TShisakuBuhinEditVoSekkeiHelper), ByVal zenkaiFlag As Boolean) Implements KaiteiChusyutuDao.InsertByBuhinEditGousyaKaitei
            Dim aDate As New ShisakuDate
            'Dim db As New EBomDbClient

            Dim zenkai As String = ""

            If zenkaiFlag Then
                zenkai = "KON"
            Else
                zenkai = "ZEN"
            End If

            ''2015/08/17 変更 E.Ubukata
            '' 処理速度向上のため処理をプロシージャ化

            'Dim sql As New StringBuilder
            'Dim uSql As New StringBuilder
            'For index As Integer = 0 To BuhinEditVoList.Count - 1
            '    With sql
            '        .Remove(0, .Length)
            '        .Append("INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI (")
            '        .Append(" SHISAKU_EVENT_CODE, ")
            '        .Append(" SHISAKU_LIST_CODE, ")
            '        .Append(" SHISAKU_LIST_CODE_KAITEI_NO, ")
            '        .Append(" SHISAKU_BUKA_CODE, ")
            '        .Append(" SHISAKU_BLOCK_NO, ")
            '        .Append(" SHISAKU_BLOCK_NO_KAITEI_NO, ")
            '        .Append(" ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO, ")
            '        .Append(" BUHIN_NO_HYOUJI_JUN, ")
            '        .Append(" FLAG, ")
            '        .Append(" SHISAKU_GOUSYA_HYOUJI_JUN, ")
            '        .Append(" SHISAKU_GOUSYA, ")
            '        .Append(" INSU_SURYO, ")
            '        .Append(" CREATED_USER_ID, ")
            '        .Append(" CREATED_DATE, ")
            '        .Append(" CREATED_TIME, ")
            '        .Append(" UPDATED_USER_ID, ")
            '        .Append(" UPDATED_DATE, ")
            '        .Append(" UPDATED_TIME ")
            '        .Append(" ) ")
            '        .Append(" VALUES ( ")
            '        .AppendFormat("'{0}' ,", BuhinEditVoList(index).ShisakuEventCode)
            '        .AppendFormat("'{0}' ,", zenkai)
            '        .AppendFormat("'{0}' ,", zenkai)
            '        .AppendFormat("'{0}' ,", BuhinEditVoList(index).ShisakuBukaCode)
            '        .AppendFormat("'{0}' ,", BuhinEditVoList(index).ShisakuBlockNo)
            '        .AppendFormat("'{0}' ,", BuhinEditVoList(index).ShisakuBlockNoKaiteiNo)
            '        .AppendFormat("'' ,")
            '        .AppendFormat("'{0}' ,", BuhinEditVoList(index).BuhinNoHyoujiJun)
            '        .AppendFormat("'9' ,")
            '        .AppendFormat("'{0}' ,", BuhinEditVoList(index).ShisakuGousyaHyoujiJun)
            '        .AppendFormat("'{0}' ,", BuhinEditVoList(index).ShisakuGousya)
            '        .AppendFormat("'{0}' ,", BuhinEditVoList(index).InsuSuryo)
            '        .AppendFormat("'{0}' ,", LoginInfo.Now.UserId)
            '        .AppendFormat("'{0}' ,", aDate.CurrentDateDbFormat)
            '        .AppendFormat("'{0}' ,", aDate.CurrentTimeDbFormat)
            '        .AppendFormat("'{0}' ,", LoginInfo.Now.UserId)
            '        .AppendFormat("'{0}' ,", aDate.CurrentDateDbFormat)
            '        .AppendFormat("'{0}'", aDate.CurrentTimeDbFormat)
            '        .Append(" ) ")
            '    End With

            '    With uSql
            '        .Remove(0, .Length)
            '        .Append(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI ")
            '        .Append(" SET ")
            '        .AppendFormat(" INSU_SURYO = INSU_SURYO + {0}", BuhinEditVoList(index).InsuSuryo)
            '        .Append(" WHERE  ")
            '        .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", BuhinEditVoList(index).ShisakuEventCode)
            '        .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}'", zenkai)
            '        .AppendFormat(" AND SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", zenkai)
            '        .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", BuhinEditVoList(index).ShisakuBukaCode)
            '        .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", BuhinEditVoList(index).ShisakuBlockNo)
            '        .AppendFormat(" AND SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", BuhinEditVoList(index).ShisakuBlockNoKaiteiNo)
            '        .AppendFormat(" AND ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO = '' ")
            '        .AppendFormat(" AND BUHIN_NO_HYOUJI_JUN = '{0}' ", BuhinEditVoList(index).BuhinNoHyoujiJun)
            '        .AppendFormat(" AND SHISAKU_GOUSYA_HYOUJI_JUN = '{0}' ", BuhinEditVoList(index).ShisakuGousyaHyoujiJun)
            '        .AppendFormat(" AND FLAG = '9'")
            '    End With


            '    Try
            '        db.Insert(sql.ToString)
            '    Catch ex As SqlClient.SqlException
            '        db.Update(uSql.ToString)
            '        'プライマリキー違反なら員数を追加する処理を行う'
            '        'UpdateByBuhinEditGousya(param)
            '    End Try


            'Next


            Using con As New SqlClient.SqlConnection
                con.ConnectionString = NitteiDbComFunc.GetConnectString
                Dim cmd As New SqlClient.SqlCommand
                con.Open()
                cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "TehaiInsertByBuhinEditGousyaKaitei"
                For Each param As TShisakuBuhinEditVoSekkeiHelper In BuhinEditVoList
                    With cmd.Parameters
                        .Clear()
                        .Add("@ShisakuEventCode", SqlDbType.VarChar).Value = param.ShisakuEventCode
                        .Add("@ShisakuListCode", SqlDbType.VarChar).Value = zenkai
                        .Add("@ShisakuListCodeKaiteiNo", SqlDbType.Char).Value = zenkai
                        .Add("@ShisakuBukaCode", SqlDbType.VarChar).Value = param.ShisakuBukaCode
                        .Add("@ShisakuBlockNo", SqlDbType.VarChar).Value = param.ShisakuBlockNo
                        .Add("@ShisakuBlockNoKaiteiNo", SqlDbType.VarChar).Value = param.ShisakuBlockNoKaiteiNo
                        .Add("@ZenkaiShisakuBlockNoKaiteiNo", SqlDbType.VarChar).Value = ""
                        .Add("@Flag", SqlDbType.Int).Value = 9
                        .Add("@BuhinNoHyoujiJun", SqlDbType.Int).Value = param.BuhinNoHyoujiJun
                        .Add("@ShisakuGousyaHyoujiJun", SqlDbType.Int).Value = param.ShisakuGousyaHyoujiJun
                        .Add("@ShisakuGousya", SqlDbType.VarChar).Value = param.ShisakuGousya
                        .Add("@InsuSuryo", SqlDbType.Int).Value = param.InsuSuryo
                        .Add("@InUserId", SqlDbType.VarChar).Value = LoginInfo.Now.UserId
                        .Add("@InDate", SqlDbType.VarChar).Value = aDate.CurrentDateDbFormat
                        .Add("@InTime", SqlDbType.VarChar).Value = aDate.CurrentTimeDbFormat
                    End With

                    cmd.ExecuteNonQuery()
                Next
            End Using
        End Sub


#End Region

#Region "更新する処理"

        ''' <summary>
        ''' 最新抽出日を更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="kaiteiNo">改訂No</param>
        ''' <remarks></remarks>
        Public Sub UpdateBySaishinChusyutu(ByVal shisakuEventCode As String, _
                                           ByVal shisakuListCode As String, _
                                           ByVal kaiteiNo As String, _
                                           ByVal chushutsuBi As Integer, _
                                           ByVal chushutsuJikan As Integer) Implements KaiteiChusyutuDao.UpdateBySaishinChusyutu
            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE " _
            & " SET " _
            & " SAISHIN_CHUSYUTUBI = @SaishinChusyutubi, " _
            & " SAISHIN_CHUSYUTUJIKAN = @SaishinChusyutujikan, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_LIST_CODE = @ShisakuListCode " _
            & " AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo "

            Dim db As New EBomDbClient
            Dim param As New TShisakuListcodeVo
            Dim aDate As New ShisakuDate

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuListCodeKaiteiNo = kaiteiNo

            '2014/08/28 抽出ボタンを押下した日時を使用するよう変更
            param.SaishinChusyutubi = chushutsuBi
            param.SaishinChusyutujikan = chushutsuJikan
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)
        End Sub

        ''' <summary>
        ''' 手配帳改訂抽出ブロック情報を更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="konkaiBlockNoKaiteiNo">今回ブロックNo改訂No</param>
        ''' <remarks></remarks>
        Public Sub UpdateByTehaiKaitei(ByVal shisakuEventCode As String, _
                                       ByVal shisakuListCode As String, _
                                       ByVal shisakuBukaCode As String, _
                                       ByVal shisakuBlockNo As String, _
                                       ByVal konkaiBlockNoKaiteiNo As String) Implements KaiteiChusyutuDao.UpdateByTehaiKaitei
            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KAITEI_BLOCK " _
            & " SET " _
            & " KONKAI_BLOCK_NO_KAITEI_NO = @KonkaiBlockNoKaiteiNo, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_LIST_CODE = @ShisakuListCode " _
            & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo "

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiKaiteiBlockVo
            Dim aDate As New ShisakuDate

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.KonkaiBlockNoKaiteiNo = konkaiBlockNoKaiteiNo

            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)
        End Sub


        ''' <summary>
        ''' 試作イベントの最終改訂抽出日を更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <remarks></remarks>
        Public Sub UpdateByShisakuEvent(ByVal shisakuEventCode As String, ByVal chushutsuNichiji As Integer) Implements KaiteiChusyutuDao.UpdateByShisakuEvent
            '2014/08/28 抽出ボタンを押した日時を利用するよう変更
            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT " _
            & " SET " _
            & " LAST_KAITEI_CHUSYUTUBI = @LastKaiteiChusyutubi, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiKaiteiBlockVo
            Dim aDate As New ShisakuDate

            param.ShisakuEventCode = shisakuEventCode
            param.LastKaiteiChusyutubi = chushutsuNichiji
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)
        End Sub


#End Region

#Region "削除する処理"

        ''' <summary>
        ''' 部品編集改訂を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByBuhinEditKaitei(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) Implements KaiteiChusyutuDao.DeleteByBuhinEditKaitei
            Dim sql As String = _
            " DELETE " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_LIST_CODE = @ShisakuListCode " _
            & " AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo "
            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditKaiteiVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo

            db.Delete(sql, param)
        End Sub

        ''' <summary>
        ''' 部品編集号車改訂を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByBuhinEditKaiteiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) Implements KaiteiChusyutuDao.DeleteByBuhinEditKaiteiGousya
            Dim sql As String = _
            " DELETE " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_LIST_CODE = @ShisakuListCode " _
            & " AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditGousyaKaiteiVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
            db.Delete(sql, param)
        End Sub

        ''' <summary>
        ''' 今回と前回の手配帳改定情報を削除
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <remarks></remarks>
        Public Sub DelteByTmpEdit(ByVal shisakuEventCode As String) Implements KaiteiChusyutuDao.DelteByTmpEdit

            Dim sql As String = _
            " DELETE " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_LIST_CODE = 'KON'" _
            & " AND SHISAKU_LIST_CODE_KAITEI_NO = 'KON' "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditKaiteiVo
            param.ShisakuEventCode = shisakuEventCode

            db.Delete(sql, param)

            Dim sql2 As String = _
            " DELETE " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_LIST_CODE = 'ZEN'" _
            & " AND SHISAKU_LIST_CODE_KAITEI_NO = 'ZEN' "

            db.Delete(sql2, param)

            Dim sql3 As String = _
            " DELETE " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_LIST_CODE = 'ZEN' " _
            & " AND SHISAKU_LIST_CODE_KAITEI_NO = 'ZEN' "

            db.Delete(sql3, param)

            Dim sql4 As String = _
            " DELETE " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_LIST_CODE = 'KON' " _
            & " AND SHISAKU_LIST_CODE_KAITEI_NO = 'KON' "

            db.Delete(sql4, param)
        End Sub

#End Region

#Region "自動織込み用"

        ''' <summary>
        ''' 最新の改訂ブロック情報を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>最新の部品編集情報</returns>
        ''' <remarks></remarks>
        Public Function FindByKaiteiBlockList(ByVal eventcode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiKaiteiBlockVo) Implements KaiteiChusyutuDao.FindByKaiteiBlockList
            Dim sb As New StringBuilder

            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT DISTINCT KB.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KAITEI_BLOCK KB ")
                .AppendLine(" WHERE ")
                .AppendFormat(" KB.SHISAKU_EVENT_CODE = '{0}' ", eventcode)
                .AppendFormat(" AND KB.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" ORDER BY SHISAKU_BLOCK_NO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiKaiteiBlockVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 今回の改訂部品情報を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>    
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns>今回の改訂ブロック情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditKaiteiList(ByVal eventcode As String, _
                                           ByVal shisakuListCode As String, _
                                           ByVal shisakuListCodeKaiteiNo As String, _
                                           ByVal shisakuBukaCode As String, _
                                           ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditKaiteiVo) Implements KaiteiChusyutuDao.FindByBuhinEditKaiteiList
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", eventcode)
                .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditKaiteiVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' ブロック単位で手配帳基本情報を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns>手配帳基本情報</returns>
        ''' <remarks></remarks>
        Public Function FindByTehaiKihonBlockList(ByVal eventcode As String, _
                                                  ByVal shisakuListCode As String, _
                                                  ByVal shisakuListCodeKaiteiNo As String, _
                                                  ByVal shisakuBukaCode As String, _
                                                  ByVal shisakuBlockNo As String) As List(Of TShisakuTehaiKihonVo) Implements KaiteiChusyutuDao.FindByTehaiKihonBlockList
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", eventcode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
                .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiKihonVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 最新のリストコード情報を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <returns>最新のリストコード情報</returns>
        ''' <remarks></remarks>
        Public Function FindByListCode(ByVal eventcode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As TShisakuListcodeVo Implements KaiteiChusyutuDao.FindByListCode
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", eventcode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuListcodeVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' ブロック情報を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Public Function FindBySekkeiBlock(ByVal eventcode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As TShisakuSekkeiBlockVo Implements KaiteiChusyutuDao.FindBySekkeiBlock
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", eventcode)
                .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
            End With

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuSekkeiBlockVo)(sb.ToString)

        End Function

        ''' <summary>
        ''' 合計員数数量を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <param name="shisakuGroup">グループ</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Public Function GetTotalInsuSuryo(ByVal eventcode As String, _
                                          ByVal shisakuBukaCode As String, _
                                          ByVal shisakuBlockNo As String, _
                                          ByVal shisakuBlockNoKaiteiNo As String, _
                                          ByVal shisakuGroup As String, _
                                          ByVal buhinNoHyoujiJun As Integer) As Integer Implements KaiteiChusyutuDao.GetTotalInsuSuryo
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT BEI.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI K ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B ")
                .AppendLine(" ON K.SHISAKU_EVENT_CODE = B.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND K.HYOJIJUN_NO = B.HYOJIJUN_NO ")
                .AppendLine(" AND (NOT B.SHISAKU_SYUBETU = 'D' OR B.SHISAKU_SYUBETU = 'W' OR B.SHISAKU_SYUBETU IS NULL) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ")
                .AppendLine(" ON SBI.SHISAKU_EVENT_CODE = B.SHISAKU_EVENT_CODE ")
                .AppendFormat(" AND SBI.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SBI.SHISAKU_BLOCK_NO =  '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendLine(" AND SBI.SHISAKU_GOUSYA = B.SHISAKU_GOUSYA ")
                .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")
                .AppendLine(" AND SBI.INSU_SURYO > 0 ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendFormat(" AND BEI.BUHIN_NO_HYOUJI_JUN = {0} ", buhinNoHyoujiJun)
                .AppendLine(" WHERE ")
                .AppendFormat(" K.SHISAKU_EVENT_CODE = '{0}' ", eventcode)
                .AppendFormat(" AND K.SHISAKU_GROUP = '{0}' ", shisakuGroup)
            End With

            Dim db As New EBomDbClient
            Dim totalInsuSuryo As Integer = 0
            Dim MTotalInsuSuryo As Integer = 0

            For Each Vo As TShisakuBuhinEditInstlVo In db.QueryForList(Of TShisakuBuhinEditInstlVo)(sb.ToString)
                If Vo.InsuSuryo < 0 Then
                    MTotalInsuSuryo += Vo.InsuSuryo
                Else
                    totalInsuSuryo += Vo.InsuSuryo
                End If
            Next

            If totalInsuSuryo = 0 Then
                If MTotalInsuSuryo < 0 Then
                    MTotalInsuSuryo = -1
                    Return MTotalInsuSuryo
                End If
            End If

            Return totalInsuSuryo
        End Function

        ''' <summary>
        ''' 最後の行IDと部品番号表示順を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Public Function FindByLatestGyouIdBuhinNoHyoujiJun(ByVal eventcode As String, _
                                                           ByVal shisakuListCode As String, _
                                                           ByVal shisakuListKaiteiNo As String, _
                                                           ByVal shisakuBukaCode As String, _
                                                           ByVal shisakuBlockNo As String) As TShisakuTehaiKihonVo Implements KaiteiChusyutuDao.FindByLatestGyouIdBuhinNoHyoujiJun

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", eventcode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListKaiteiNo)
                .AppendFormat(" AND TK.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND TK.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendLine(" AND TK.GYOU_ID = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( GYOU_ID,'' ) ) ) AS GYOU_ID ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = TK.SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = TK.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = TK.SHISAKU_BLOCK_NO)  ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuTehaiKihonVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 号車の員数を取得する
        ''' </summary>
        ''' <param name="shisakuEvetnCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="flag">フラグ</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByGousyaInsuList(ByVal shisakuEvetnCode As String, _
                                             ByVal shisakuListCode As String, _
                                             ByVal shisakuListCodeKaiteiNo As String, _
                                             ByVal shisakuBukaCode As String, _
                                             ByVal shisakuBlockNo As String, _
                                             ByVal shisakuBlockNoKaiteiNo As String, _
                                             ByVal buhinNoHyoujiJun As Integer, _
                                             ByVal flag As String) As List(Of TShisakuTehaiGousyaVo) Implements KaiteiChusyutuDao.FindByGousyaInsuList

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT BEI.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI BEI ")
                .AppendFormat(" WHERE BEI.SHISAKU_EVENT_CODE = '{0}' ", shisakuEvetnCode)
                .AppendFormat(" AND BEI.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND BEI.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
                .AppendFormat(" AND BEI.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND BEI.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendFormat(" AND BEI.BUHIN_NO_HYOUJI_JUN = {0} ", buhinNoHyoujiJun)
                .AppendFormat(" AND BEI.FLAG = '{0}'", flag)
            End With

            Dim db As New EBomDbClient
            Dim InstlVo As New List(Of TShisakuTehaiGousyaVo)

            Return db.QueryForList(Of TShisakuTehaiGousyaVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 手配基本情報と手配号車情報を削除
        ''' </summary>
        ''' <param name="kihonVo">手配基本情報</param>
        ''' <remarks></remarks>
        Public Sub DelteByKihonGousya(ByVal kihonVo As TShisakuTehaiKihonVo) Implements KaiteiChusyutuDao.DelteByKihonGousya
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" DELETE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
            End With

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiGousyaVo
            param.ShisakuEventCode = kihonVo.ShisakuEventCode
            param.ShisakuListCode = kihonVo.ShisakuListCode
            param.ShisakuListCodeKaiteiNo = kihonVo.ShisakuListCodeKaiteiNo
            param.ShisakuBukaCode = kihonVo.ShisakuBukaCode
            param.ShisakuBlockNo = kihonVo.ShisakuBlockNo
            param.BuhinNoHyoujiJun = kihonVo.BuhinNoHyoujiJun

            db.Delete(sb.ToString, param)
            With sb
                .Remove(0, .Length)
                .AppendLine(" DELETE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
            End With
            db.Delete(sb.ToString, param)
        End Sub

        ''' <summary>
        ''' 手配基本情報と手配号車情報を削除
        ''' </summary>
        ''' <param name="buhinVo">部品編集改訂情報</param>
        ''' <remarks></remarks>
        Public Sub DelteByKihonGousyaBlock(ByVal buhinVo As TShisakuBuhinEditKaiteiVo) Implements KaiteiChusyutuDao.DelteByKihonGousyaBlock
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" DELETE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
            End With

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiGousyaVo
            param.ShisakuEventCode = buhinVo.ShisakuEventCode
            param.ShisakuListCode = buhinVo.ShisakuListCode
            param.ShisakuListCodeKaiteiNo = buhinVo.ShisakuListCodeKaiteiNo
            param.ShisakuBukaCode = buhinVo.ShisakuBukaCode
            param.ShisakuBlockNo = buhinVo.ShisakuBlockNo

            db.Delete(sb.ToString, param)
            With sb
                .Remove(0, .Length)
                .AppendLine(" DELETE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
            End With
            db.Delete(sb.ToString, param)
        End Sub

        ''' <summary>
        ''' 手配基本情報を追加
        ''' </summary>
        ''' <param name="kihonVo">手配基本情報</param>
        ''' <remarks></remarks>
        Public Sub InsertByTehaiKihon(ByVal kihonVo As TShisakuTehaiKihonVo) Implements KaiteiChusyutuDao.InsertByTehaiKihon
            Dim sb As New StringBuilder
            Dim aDate As New ShisakuDate
            Dim UserId As String
            Dim Hi As String
            Dim Time As String
            UserId = LoginInfo.Now.UserId
            Hi = aDate.CurrentDateDbFormat
            Time = aDate.CurrentTimeDbFormat


            With sb
                .Remove(0, .Length)
                .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ( ")
                .AppendLine(" SHISAKU_EVENT_CODE, ")
                .AppendLine(" SHISAKU_LIST_CODE, ")
                .AppendLine(" SHISAKU_LIST_CODE_KAITEI_NO, ")
                .AppendLine(" SHISAKU_BUKA_CODE, ")
                .AppendLine(" SHISAKU_BLOCK_NO, ")
                .AppendLine(" BUHIN_NO_HYOUJI_JUN, ")
                .AppendLine(" SORT_JUN, ")
                .AppendLine(" RIREKI, ")
                .AppendLine(" GYOU_ID, ")
                .AppendLine(" SENYOU_MARK, ")
                .AppendLine(" LEVEL, ")
                .AppendLine(" UNIT_KBN, ")
                .AppendLine(" BUHIN_NO, ")
                .AppendLine(" BUHIN_NO_KBN, ")
                .AppendLine(" BUHIN_NO_KAITEI_NO, ")
                .AppendLine(" EDA_BAN, ")
                .AppendLine(" BUHIN_NAME, ")
                .AppendLine(" SHUKEI_CODE, ")
                .AppendLine(" GENCYO_CKD_KBN, ")
                .AppendLine(" TEHAI_KIGOU, ")
                .AppendLine(" KOUTAN, ")
                .AppendLine(" TORIHIKISAKI_CODE, ")
                .AppendLine(" NOUBA, ")
                .AppendLine(" KYOUKU_SECTION, ")
                .AppendLine(" NOUNYU_SHIJIBI, ")
                .AppendLine(" TOTAL_INSU_SURYO, ")
                .AppendLine(" SAISHIYOUFUKA, ")

                .AppendLine(" GOUSYA_HACHU_TENKAI_FLG, ")
                .AppendLine(" GOUSYA_HACHU_TENKAI_UNIT_KBN, ")

                .AppendLine(" SHUTUZU_YOTEI_DATE, ")
                .AppendLine(" SHUTUZU_JISEKI_DATE, ")
                .AppendLine(" SHUTUZU_JISEKI_KAITEI_NO, ")
                .AppendLine(" SHUTUZU_JISEKI_STSR_DHSTBA, ")
                .AppendLine(" SAISYU_SETSUHEN_DATE, ")
                .AppendLine(" SAISYU_SETSUHEN_KAITEI_NO, ")
                .AppendLine(" STSR_DHSTBA, ")

                .AppendLine(" ZAISHITU_KIKAKU_1, ")
                .AppendLine(" ZAISHITU_KIKAKU_2, ")
                .AppendLine(" ZAISHITU_KIKAKU_3, ")
                .AppendLine(" ZAISHITU_MEKKI, ")
                .AppendLine(" SHISAKU_BANKO_SURYO, ")
                .AppendLine(" SHISAKU_BANKO_SURYO_U, ")
                .AppendLine(" SHISAKU_BUHINN_HI, ")
                .AppendLine(" SHISAKU_KATA_HI, ")
                .AppendLine(" MAKER_CODE, ")
                .AppendLine(" BIKOU, ")
                .AppendLine(" BUHIN_NO_OYA, ")
                .AppendLine(" BUHIN_NO_KBN_OYA, ")
                .AppendLine(" ERROR_KBN, ")
                .AppendLine(" AUD_FLAG, ")
                .AppendLine(" AUD_BI, ")
                .AppendLine(" KETUGOU_NO, ")
                .AppendLine(" HENKATEN, ")
                .AppendLine(" SHISAKU_SEIHIN_KBN, ")
                .AppendLine(" AUTO_ORIKOMI_KAITEI_NO, ")
                .AppendLine(" TSUKURIKATA_SEISAKU, ")
                .AppendLine(" TSUKURIKATA_KATASHIYOU_1, ")
                .AppendLine(" TSUKURIKATA_KATASHIYOU_2, ")
                .AppendLine(" TSUKURIKATA_KATASHIYOU_3, ")
                .AppendLine(" TSUKURIKATA_TIGU, ")
                .AppendLine(" TSUKURIKATA_NOUNYU, ")
                .AppendLine(" TSUKURIKATA_KIBO, ")
                .AppendLine(" BASE_BUHIN_FLG, ")
                .AppendLine(" MATERIAL_INFO_LENGTH, ")
                .AppendLine(" MATERIAL_INFO_WIDTH, ")
                .AppendLine(" MATERIAL_INFO_ORDER_TARGET, ")
                .AppendLine(" MATERIAL_INFO_ORDER_TARGET_DATE, ")
                .AppendLine(" MATERIAL_INFO_ORDER_CHK, ")
                .AppendLine(" MATERIAL_INFO_ORDER_CHK_DATE, ")
                .AppendLine(" DATA_ITEM_KAITEI_NO, ")
                .AppendLine(" DATA_ITEM_AREA_NAME, ")
                .AppendLine(" DATA_ITEM_SET_NAME, ")
                .AppendLine(" DATA_ITEM_KAITEI_INFO, ")
                .AppendLine(" DATA_ITEM_DATA_PROVISION, ")
                .AppendLine(" DATA_ITEM_DATA_PROVISION_DATE, ")
                .AppendLine(" CREATED_USER_ID, ")
                .AppendLine(" CREATED_DATE, ")
                .AppendLine(" CREATED_TIME, ")
                .AppendLine(" UPDATED_USER_ID, ")
                .AppendLine(" UPDATED_DATE, ")
                .AppendLine(" UPDATED_TIME ")
                .AppendLine(" ) ")
                .AppendLine(" VALUES ( ")
                .AppendLine("'" & kihonVo.ShisakuEventCode & "' , ")
                .AppendLine("'" & kihonVo.ShisakuListCode & "' , ")
                .AppendLine("'" & kihonVo.ShisakuListCodeKaiteiNo & "' , ")
                .AppendLine("'" & kihonVo.ShisakuBukaCode & "' , ")
                .AppendLine("'" & kihonVo.ShisakuBlockNo & "' , ")
                .AppendLine("'" & kihonVo.BuhinNoHyoujiJun & "' , ")
                .AppendLine("'" & kihonVo.SortJun & "' , ")
                .AppendLine("'" & kihonVo.Rireki & "' , ")
                .AppendLine("'" & kihonVo.GyouId & "' , ")
                .AppendLine("'" & kihonVo.SenyouMark & "' , ")
                .AppendLine("'" & kihonVo.Level & "' , ")
                .AppendLine("'" & kihonVo.UnitKbn & "' , ")
                .AppendLine("'" & kihonVo.BuhinNo & "' , ")
                .AppendLine("'" & kihonVo.BuhinNoKbn & "' , ")
                .AppendLine("'" & kihonVo.BuhinNoKaiteiNo & "' , ")
                .AppendLine("'" & kihonVo.EdaBan & "' , ")
                .AppendLine("'" & kihonVo.BuhinName & "' , ")
                .AppendLine("'" & kihonVo.ShukeiCode & "' , ")
                .AppendLine("'" & kihonVo.GencyoCkdKbn & "' , ")
                .AppendLine("'" & kihonVo.TehaiKigou & "' , ")
                .AppendLine("'" & kihonVo.Koutan & "' , ")
                .AppendLine("'" & kihonVo.TorihikisakiCode & "' , ")
                .AppendLine("'" & kihonVo.Nouba & "' , ")
                .AppendLine("'" & kihonVo.KyoukuSection & "' , ")
                .AppendLine("'" & kihonVo.NounyuShijibi & "' , ")
                .AppendLine("'" & kihonVo.TotalInsuSuryo & "' , ")
                .AppendLine("'" & kihonVo.Saishiyoufuka & "' , ")

                .AppendLine("'" & kihonVo.GousyaHachuTenkaiFlg & "' , ")
                .AppendLine("'" & kihonVo.GousyaHachuTenkaiUnitKbn & "' , ")
                .AppendLine("'" & kihonVo.ShutuzuYoteiDate & "' , ")
                .AppendLine("'" & kihonVo.ShutuzuJisekiDate & "' , ")
                .AppendLine("'" & kihonVo.ShutuzuJisekiKaiteiNo & "' , ")
                .AppendLine("'" & kihonVo.ShutuzuJisekiStsrDhstba & "' , ")
                .AppendLine("'" & kihonVo.SaisyuSetsuhenDate & "' , ")
                .AppendLine("'" & kihonVo.SaisyuSetsuhenKaiteiNo & "' , ")
                .AppendLine("'" & kihonVo.StsrDhstba & "' , ")
                .AppendLine("'" & kihonVo.ZaishituKikaku1 & "' , ")
                .AppendLine("'" & kihonVo.ZaishituKikaku2 & "' , ")
                .AppendLine("'" & kihonVo.ZaishituKikaku3 & "' , ")
                .AppendLine("'" & kihonVo.ZaishituMekki & "' , ")
                .AppendLine("'" & kihonVo.ShisakuBankoSuryo & "' , ")
                .AppendLine("'" & kihonVo.ShisakuBankoSuryoU & "' , ")
                .AppendLine("'" & kihonVo.ShisakuBuhinnHi & "' , ")
                .AppendLine("'" & kihonVo.ShisakuKataHi & "' , ")
                .AppendLine("'" & kihonVo.MakerCode & "' , ")
                .AppendLine("'" & kihonVo.Bikou & "' , ")
                .AppendLine("'" & kihonVo.BuhinNoOya & "' , ")
                .AppendLine("'" & kihonVo.BuhinNoKbn & "' , ")
                .AppendLine("'" & kihonVo.ErrorKbn & "' , ")
                .AppendLine("'" & kihonVo.AudFlag & "' , ")
                .AppendLine("'" & kihonVo.AudBi & "' , ")
                .AppendLine("'" & kihonVo.KetugouNo & "' , ")
                .AppendLine("'" & kihonVo.Henkaten & "' , ")
                .AppendLine("'" & kihonVo.ShisakuSeihinKbn & "' , ")
                .AppendLine("'" & kihonVo.AutoOrikomiKaiteiNo & "' ,")
                .AppendLine(" '" & kihonVo.TsukurikataSeisaku & "' , ")
                .AppendLine(" '" & kihonVo.TsukurikataKatashiyou1 & "' , ")
                .AppendLine(" '" & kihonVo.TsukurikataKatashiyou2 & "' , ")
                .AppendLine(" '" & kihonVo.TsukurikataKatashiyou3 & "' , ")
                .AppendLine(" '" & kihonVo.TsukurikataTigu & "' , ")
                .AppendLine("'" & kihonVo.TsukurikataNounyu & "' , ")
                .AppendLine(" '" & kihonVo.TsukurikataKibo & "' , ")
                .AppendLine(" '" & kihonVo.BaseBuhinFlg & "' , ")
                .AppendLine("'" & kihonVo.MaterialInfoLength & "' , ")
                .AppendLine("'" & kihonVo.MaterialInfoWidth & "' , ")
                .AppendLine("'" & kihonVo.MaterialInfoOrderTarget & "' , ")
                .AppendLine("'" & kihonVo.MaterialInfoOrderTargetDate & "' , ")
                .AppendLine("'" & kihonVo.MaterialInfoOrderChk & "' , ")
                .AppendLine("'" & kihonVo.MaterialInfoOrderChkDate & "' , ")
                .AppendLine("'" & kihonVo.DataItemKaiteiNo & "' , ")
                .AppendLine("'" & kihonVo.DataItemAreaName & "' , ")
                .AppendLine("'" & kihonVo.DataItemSetName & "' , ")
                .AppendLine("'" & kihonVo.DataItemKaiteiInfo & "' , ")
                .AppendLine("'" & kihonVo.DataItemDataProvision & "' , ")
                .AppendLine("'" & kihonVo.DataItemDataProvisionDate & "' , ")

                .AppendLine("'" & UserId & "' , ")
                .AppendLine("'" & Hi & "' , ")
                .AppendLine("'" & Time & "' , ")
                .AppendLine("'" & UserId & "' , ")
                .AppendLine("'" & Hi & "' , ")
                .AppendLine("'" & Time & "' ) ")

            End With

            '2014/09/02
            ' XMLの禁則文字でエラーとなるため変更
            'Dim db As New EBomDbClient
            'db.Insert(sb.ToString)
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.ExecuteNonQuery(sb.ToString)
                db.Commit()
                db.Close()
            End Using

        End Sub

        ''' <summary>
        ''' 手配号車情報を追加
        ''' </summary>
        ''' <param name="gousyaVo">手配号車情報</param>
        ''' <remarks></remarks>
        Public Sub InsertByTehaiGousya(ByVal gousyaVo As TShisakuTehaiGousyaVo) Implements KaiteiChusyutuDao.InsertByTehaiGousya
            Dim sb As New StringBuilder
            Dim aDate As New ShisakuDate
            Dim UserId As String
            Dim Hi As String
            Dim Time As String
            UserId = LoginInfo.Now.UserId
            Hi = aDate.CurrentDateDbFormat
            Time = aDate.CurrentTimeDbFormat
            With sb
                .Remove(0, .Length)
                .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ( ")
                .AppendLine(" SHISAKU_EVENT_CODE, ")
                .AppendLine(" SHISAKU_LIST_CODE, ")
                .AppendLine(" SHISAKU_LIST_CODE_KAITEI_NO, ")
                .AppendLine(" SHISAKU_BUKA_CODE, ")
                .AppendLine(" SHISAKU_BLOCK_NO, ")
                .AppendLine(" BUHIN_NO_HYOUJI_JUN, ")
                .AppendLine(" SORT_JUN, ")
                .AppendLine(" GYOU_ID, ")
                .AppendLine(" SHISAKU_GOUSYA_HYOUJI_JUN, ")
                .AppendLine(" SHISAKU_GOUSYA, ")
                .AppendLine(" INSU_SURYO, ")
                .AppendLine(" M_NOUNYU_SHIJIBI, ")
                .AppendLine(" T_NOUNYU_SHIJIBI, ")
                .AppendLine(" CREATED_USER_ID, ")
                .AppendLine(" CREATED_DATE, ")
                .AppendLine(" CREATED_TIME, ")
                .AppendLine(" UPDATED_USER_ID, ")
                .AppendLine(" UPDATED_DATE, ")
                .AppendLine(" UPDATED_TIME ")
                .AppendLine(" ) ")
                .AppendLine(" VALUES ( ")
                .AppendLine("'" & gousyaVo.ShisakuEventCode & "' , ")
                .AppendLine("'" & gousyaVo.ShisakuListCode & "' , ")
                .AppendLine("'" & gousyaVo.ShisakuListCodeKaiteiNo & "' , ")
                .AppendLine("'" & gousyaVo.ShisakuBukaCode & "' , ")
                .AppendLine("'" & gousyaVo.ShisakuBlockNo & "' , ")
                .AppendLine("'" & gousyaVo.BuhinNoHyoujiJun & "' , ")
                .AppendLine("'" & gousyaVo.BuhinNoHyoujiJun & "' , ")
                .AppendLine("'" & gousyaVo.GyouId & "' , ")
                .AppendLine("'" & gousyaVo.ShisakuGousyaHyoujiJun & "' , ")
                .AppendLine("'" & gousyaVo.ShisakuGousya & "' , ")
                .AppendLine("'" & gousyaVo.InsuSuryo & "' , ")
                .AppendLine("'" & gousyaVo.MNounyuShijibi & "' , ")
                .AppendLine("'" & gousyaVo.TNounyuShijibi & "' , ")
                .AppendLine("'" & UserId & "' , ")
                .AppendLine("'" & Hi & "' , ")
                .AppendLine("'" & Time & "' , ")
                .AppendLine("'" & UserId & "' , ")
                .AppendLine("'" & Hi & "' , ")
                .AppendLine("'" & Time & "' ) ")
            End With
            Dim db As New EBomDbClient
            db.Insert(sb.ToString)

        End Sub

        ''' <summary>
        ''' DUMMY列を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <remarks></remarks>
        Public Function FindByDummy(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal buhinNoHyoujiJun As Integer) As TShisakuTehaiGousyaVo Implements KaiteiChusyutuDao.FindByDummy
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
                .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND BUHIN_NO_HYOUJI_JUN = {0} ", buhinNoHyoujiJun)
                .AppendLine(" AND SHISAKU_GOUSYA = 'DUMMY' ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuTehaiGousyaVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' DUMMY列を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <remarks></remarks>
        Public Function FindByDummy2(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As TShisakuTehaiGousyaVo Implements KaiteiChusyutuDao.FindByDummy2
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
                .AppendLine(" AND SHISAKU_GOUSYA = 'DUMMY' ")
                .AppendLine(" ORDER By SHISAKU_GOUSYA_HYOUJI_JUN DESC ")    '号車表示順を降順へ。2013/11/30
            End With

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuTehaiGousyaVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' DUMMY列を取得
        ''' 2013/12/02　DUMMY列の最大値を求めないと号車列と被って落ちることがあるので対策として追加。
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Function FindByDummy2222(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As TShisakuTehaiGousyaVo Implements KaiteiChusyutuDao.FindByDummy2222
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", shisakuEventCode)
                .AppendLine(" AND SHISAKU_GOUSYA = 'DUMMY' ")               '検索条件から改訂№を外す。2013/12/02
                .AppendLine(" ORDER By SHISAKU_GOUSYA_HYOUJI_JUN DESC ")    '号車表示順を降順へ。2013/11/30
            End With

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuTehaiGousyaVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 号車表示順最大値を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <remarks></remarks>
        Public Function FindByMaXGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As TShisakuTehaiGousyaVo Implements KaiteiChusyutuDao.FindByMaXGousya
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT MAX(SHISAKU_GOUSYA_HYOUJI_JUN) AS SHISAKU_GOUSYA_HYOUJI_JUN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
            End With

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuTehaiGousyaVo)(sb.ToString)

        End Function


        ''' <summary>
        ''' 手配号車リストを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <remarks></remarks>
        Public Function FindByTehaiGousyaList(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal buhinNoHyoujiJun As Integer) As List(Of TShisakuTehaiGousyaVo) Implements KaiteiChusyutuDao.FindByTehaiGousyaList
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
                .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND BUHIN_NO_HYOUJI_JUN = {0} ", buhinNoHyoujiJun)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiGousyaVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 手配基本情報を更新
        ''' </summary>
        ''' <param name="kihonVo">手配基本情報</param>
        ''' <remarks></remarks>
        Public Sub UpdateByTehaiKihon(ByVal kihonVo As TShisakuTehaiKihonVo) Implements KaiteiChusyutuDao.UpdateByTehaiKihon
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ")
                .AppendLine(" SET LEVEL = @Level, ")
                .AppendLine(" BUHIN_NO = @BuhinNo, ")
                .AppendLine(" BUHIN_NO_KBN = @BuhinNoKbn, ")
                .AppendLine(" BUHIN_NO_KAITEI_NO = @BuhinNoKaiteiNo, ")
                .AppendLine(" EDA_BAN = @EdaBan, ")
                .AppendLine(" BUHIN_NAME = @BuhinName, ")
                .AppendLine(" SHUKEI_CODE = @ShukeiCode, ")
                .AppendLine(" GENCYO_CKD_KBN = @GencyoCkdKbn, ")
                .AppendLine(" TEHAI_KIGOU = @TehaiKigou, ")
                .AppendLine(" KOUTAN = @Koutan, ")
                .AppendLine(" TORIHIKISAKI_CODE = @TorihikisakiCode, ")
                .AppendLine(" NOUBA = @Nouba, ")
                .AppendLine(" KYOUKU_SECTION = @KyoukuSection, ")
                .AppendLine(" NOUNYU_SHIJIBI = @NounyuShijibi, ")
                .AppendLine(" TOTAL_INSU_SURYO = @TotalInsuSuryo, ")
                .AppendLine(" SAISHIYOUFUKA = @Saishiyoufuka, ")

                .AppendLine(" GOUSYA_HACHU_TENKAI_FLG = @GousyaHachuTenkaiFlg, ")
                .AppendLine(" GOUSYA_HACHU_TENKAI_UNIT_KBN = @GousyaHachuTenkaiUnitKbn, ")

                .AppendLine(" SHUTUZU_YOTEI_DATE = @ShutuzuYoteiDate, ")

                .AppendLine(" SHUTUZU_JISEKI_DATE = @ShutuzuJisekiDate, ")
                .AppendLine(" SHUTUZU_JISEKI_KAITEI_NO = @ShutuzuJisekiKaiteiNo, ")
                .AppendLine(" SHUTUZU_JISEKI_STSR_DHSTBA = @ShutuzuJisekiStsrDhstba, ")

                .AppendLine(" SAISYU_SETSUHEN_DATE = @SaisyuSetsuhenDate, ")
                .AppendLine(" SAISYU_SETSUHEN_KAITEI_NO = @SaisyuSetsuhenKaiteiNo, ")
                .AppendLine(" STSR_DHSTBA = @StsrDhstba, ")

                .AppendLine(" ZAISHITU_KIKAKU_1 = @ZaishituKikaku1, ")
                .AppendLine(" ZAISHITU_KIKAKU_2 = @ZaishituKikaku2, ")
                .AppendLine(" ZAISHITU_KIKAKU_3 = @ZaishituKikaku3, ")
                .AppendLine(" ZAISHITU_MEKKI = @ZaishituMekki, ")
                .AppendLine(" SHISAKU_BANKO_SURYO = @ShisakuBankoSuryo, ")
                .AppendLine(" SHISAKU_BANKO_SURYO_U = @ShisakuBankoSuryoU, ")
                .AppendLine(" SHISAKU_BUHINN_HI = @ShisakuBuhinnHi, ")
                .AppendLine(" SHISAKU_KATA_HI = @ShisakuKataHi, ")
                .AppendLine(" MAKER_CODE = @MakerCode, ")
                .AppendLine(" BIKOU = @Bikou, ")
                .AppendLine(" BUHIN_NO_OYA = @BuhinNoOya, ")
                .AppendLine(" BUHIN_NO_KBN_OYA = @BuhinNoKbnOya, ")
                .AppendLine(" ERROR_KBN = @ErrorKbn, ")
                .AppendLine(" AUD_FLAG = @AudFlag, ")
                .AppendLine(" AUD_BI = @AudBi, ")
                .AppendLine(" KETUGOU_NO = @KetugouNo, ")
                .AppendLine(" HENKATEN = @Henkaten, ")
                .AppendLine(" SHISAKU_SEIHIN_KBN = @ShisakuSeihinKbn, ")
                .AppendLine(" AUTO_ORIKOMI_KAITEI_NO = @AutoOrikomiKaiteiNo, ")


                .AppendLine(" MATERIAL_INFO_LENGTH = @MaterialInfoLength, ")
                .AppendLine(" MATERIAL_INFO_WIDTH = @MaterialInfoWidth, ")

                .AppendLine(" ZAIRYO_SUNPO_X = @ZairyoSunpoX, ")
                .AppendLine(" ZAIRYO_SUNPO_Y = @ZairyoSunpoY, ")
                .AppendLine(" ZAIRYO_SUNPO_Z = @ZairyoSunpoZ, ")
                .AppendLine(" ZAIRYO_SUNPO_XY = @ZairyoSunpoXy, ")
                .AppendLine(" ZAIRYO_SUNPO_XZ = @ZairyoSunpoXz, ")
                .AppendLine(" ZAIRYO_SUNPO_YZ = @ZairyoSunpoYz, ")

                .AppendLine(" MATERIAL_INFO_ORDER_TARGET = @MaterialInfoOrderTarget, ")
                .AppendLine(" MATERIAL_INFO_ORDER_TARGET_DATE = @MaterialInfoOrderTargetDate, ")
                .AppendLine(" MATERIAL_INFO_ORDER_CHK = @MaterialInfoOrderChk, ")
                .AppendLine(" MATERIAL_INFO_ORDER_CHK_DATE = @MaterialInfoOrderChkDate, ")
                .AppendLine(" DATA_ITEM_KAITEI_NO = @DataItemKaiteiNo, ")
                .AppendLine(" DATA_ITEM_AREA_NAME = @DataItemAreaName, ")
                .AppendLine(" DATA_ITEM_SET_NAME = @DataItemSetName, ")
                .AppendLine(" DATA_ITEM_KAITEI_INFO = @DataItemKaiteiInfo, ")
                .AppendLine(" DATA_ITEM_DATA_PROVISION = @DataItemDataProvision, ")
                .AppendLine(" DATA_ITEM_DATA_PROVISION_DATE = @DataItemDataProvisionDate, ")

                .AppendLine(" UPDATED_USER_ID = @UpdatedUserId, ")
                .AppendLine(" UPDATED_DATE = @UpdatedDate, ")
                .AppendLine(" UPDATED_TIME = @UpdatedTime ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
            End With

            '2014/09/02
            ' XMLの禁則文字でエラーとなるため変更
            Dim aDate As New ShisakuDate
            'Dim param As New TShisakuTehaiKihonVo
            'param.ShisakuEventCode = kihonVo.ShisakuEventCode
            'param.ShisakuListCode = kihonVo.ShisakuListCode
            'param.ShisakuListCodeKaiteiNo = kihonVo.ShisakuListCodeKaiteiNo
            'param.ShisakuBukaCode = kihonVo.ShisakuBukaCode
            'param.ShisakuBlockNo = kihonVo.ShisakuBlockNo
            'param.BuhinNoHyoujiJun = kihonVo.BuhinNoHyoujiJun
            'param.Level = kihonVo.Level
            'param.BuhinNo = kihonVo.BuhinNo
            'param.BuhinNoKbn = kihonVo.BuhinNoKbn
            'param.BuhinNoKaiteiNo = kihonVo.BuhinNoKaiteiNo
            'param.EdaBan = kihonVo.EdaBan
            'param.BuhinName = kihonVo.BuhinName
            'param.ShukeiCode = kihonVo.ShukeiCode
            'param.GencyoCkdKbn = kihonVo.GencyoCkdKbn
            'param.TehaiKigou = kihonVo.TehaiKigou
            'param.Koutan = kihonVo.Koutan
            'param.TorihikisakiCode = kihonVo.TorihikisakiCode
            'param.Nouba = kihonVo.Nouba
            'param.KyoukuSection = kihonVo.KyoukuSection
            'param.NounyuShijibi = kihonVo.NounyuShijibi
            'param.TotalInsuSuryo = kihonVo.TotalInsuSuryo
            'param.Saishiyoufuka = kihonVo.Saishiyoufuka
            'param.ShutuzuYoteiDate = kihonVo.ShutuzuYoteiDate
            'param.StsrDhstba = kihonVo.StsrDhstba
            'param.ZaishituKikaku1 = kihonVo.ZaishituKikaku1
            'param.ZaishituKikaku2 = kihonVo.ZaishituKikaku2
            'param.ZaishituKikaku3 = kihonVo.ZaishituKikaku3
            'param.ZaishituMekki = kihonVo.ZaishituMekki
            'param.ShisakuBankoSuryo = kihonVo.ShisakuBankoSuryo
            'param.ShisakuBankoSuryoU = kihonVo.ShisakuBankoSuryoU
            'param.ShisakuBuhinnHi = kihonVo.ShisakuBuhinnHi
            'param.ShisakuKataHi = kihonVo.ShisakuKataHi
            'param.MakerCode = kihonVo.MakerCode
            'param.Bikou = kihonVo.Bikou
            'param.BuhinNoOya = kihonVo.BuhinNoOya
            'param.BuhinNoKbnOya = kihonVo.BuhinNoKbnOya
            'param.ErrorKbn = kihonVo.ErrorKbn
            'param.AudBi = kihonVo.AudBi
            'param.AudFlag = kihonVo.AudFlag
            'param.KetugouNo = kihonVo.KetugouNo
            'param.Henkaten = kihonVo.Henkaten
            'param.ShisakuSeihinKbn = kihonVo.ShisakuSeihinKbn
            'param.AutoOrikomiKaiteiNo = kihonVo.AutoOrikomiKaiteiNo
            'param.UpdatedUserId = LoginInfo.Now.UserId
            'param.UpdatedDate = aDate.CurrentDateDbFormat
            'param.UpdatedTime = aDate.CurrentTimeDbFormat

            'db.Update(sb.ToString, param)
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.AddParameter("@ShisakuEventCode", kihonVo.ShisakuEventCode, DbType.String)
                db.AddParameter("@ShisakuListCode", kihonVo.ShisakuListCode, DbType.String)
                db.AddParameter("@ShisakuListCodeKaiteiNo", kihonVo.ShisakuListCodeKaiteiNo, DbType.String)
                db.AddParameter("@ShisakuBukaCode", kihonVo.ShisakuBukaCode, DbType.String)
                db.AddParameter("@ShisakuBlockNo", kihonVo.ShisakuBlockNo, DbType.String)
                db.AddParameter("@BuhinNoHyoujiJun", kihonVo.BuhinNoHyoujiJun, DbType.Int32)
                db.AddParameter("@Level", kihonVo.Level, DbType.Int32)
                db.AddParameter("@BuhinNo", kihonVo.BuhinNo, DbType.String)
                db.AddParameter("@BuhinNoKbn", kihonVo.BuhinNoKbn, DbType.String)
                db.AddParameter("@BuhinNoKaiteiNo", kihonVo.BuhinNoKaiteiNo, DbType.String)
                db.AddParameter("@EdaBan", kihonVo.EdaBan, DbType.String)
                db.AddParameter("@BuhinName", kihonVo.BuhinName, DbType.String)
                db.AddParameter("@ShukeiCode", kihonVo.ShukeiCode, DbType.String)
                db.AddParameter("@GencyoCkdKbn", kihonVo.GencyoCkdKbn, DbType.String)
                db.AddParameter("@TehaiKigou", kihonVo.TehaiKigou, DbType.String)
                db.AddParameter("@Koutan", kihonVo.Koutan, DbType.String)
                db.AddParameter("@TorihikisakiCode", kihonVo.TorihikisakiCode, DbType.String)
                db.AddParameter("@Nouba", kihonVo.Nouba, DbType.String)
                db.AddParameter("@KyoukuSection", kihonVo.KyoukuSection, DbType.String)
                db.AddParameter("@NounyuShijibi", kihonVo.NounyuShijibi, DbType.Int32)
                db.AddParameter("@TotalInsuSuryo", kihonVo.TotalInsuSuryo, DbType.Int32)
                db.AddParameter("@Saishiyoufuka", kihonVo.Saishiyoufuka, DbType.String)

                db.AddParameter("@GousyaHachuTenkaiFlg", kihonVo.GousyaHachuTenkaiFlg, DbType.String)
                db.AddParameter("@GousyaHachuTenkaiUnitKbn", kihonVo.GousyaHachuTenkaiUnitKbn, DbType.String)

                db.AddParameter("@ShutuzuYoteiDate", kihonVo.ShutuzuYoteiDate, DbType.Int32)

                db.AddParameter("@ShutuzuJisekiDate", kihonVo.ShutuzuJisekiDate, DbType.Int32)
                db.AddParameter("@ShutuzuJisekiKaiteiNo", kihonVo.ShutuzuJisekiDate, DbType.String)
                db.AddParameter("@ShutuzuJisekiStsrDhstba", kihonVo.ShutuzuJisekiStsrDhstba, DbType.String)
                db.AddParameter("@SaisyuSetsuhenDate", kihonVo.SaisyuSetsuhenDate, DbType.Int32)
                db.AddParameter("@SaisyuSetsuhenKaiteiNo", kihonVo.SaisyuSetsuhenKaiteiNo, DbType.String)
                db.AddParameter("@StsrDhstba", kihonVo.StsrDhstba, DbType.String)

                db.AddParameter("@ZaishituKikaku1", kihonVo.ZaishituKikaku1, DbType.String)
                db.AddParameter("@ZaishituKikaku2", kihonVo.ZaishituKikaku2, DbType.String)
                db.AddParameter("@ZaishituKikaku3", kihonVo.ZaishituKikaku3, DbType.String)
                db.AddParameter("@ZaishituMekki", kihonVo.ZaishituMekki, DbType.String)
                db.AddParameter("@ShisakuBankoSuryo", kihonVo.ShisakuBankoSuryo, DbType.String)
                db.AddParameter("@ShisakuBankoSuryoU", kihonVo.ShisakuBankoSuryoU, DbType.String)
                db.AddParameter("@ShisakuBuhinnHi", kihonVo.ShisakuBuhinnHi, DbType.Int32)
                db.AddParameter("@ShisakuKataHi", kihonVo.ShisakuKataHi, DbType.Int32)
                db.AddParameter("@MakerCode", kihonVo.MakerCode, DbType.String)
                db.AddParameter("@Bikou", kihonVo.Bikou, DbType.String)
                db.AddParameter("@BuhinNoOya", kihonVo.BuhinNoOya, DbType.String)
                db.AddParameter("@BuhinNoKbnOya", kihonVo.BuhinNoKbnOya, DbType.String)
                db.AddParameter("@ErrorKbn", kihonVo.ErrorKbn, DbType.String)
                db.AddParameter("@AudBi", kihonVo.AudBi, DbType.Int32)
                db.AddParameter("@AudFlag", kihonVo.AudFlag, DbType.String)
                db.AddParameter("@KetugouNo", kihonVo.KetugouNo, DbType.String)
                db.AddParameter("@Henkaten", kihonVo.Henkaten, DbType.String)
                db.AddParameter("@ShisakuSeihinKbn", kihonVo.ShisakuSeihinKbn, DbType.String)
                db.AddParameter("@AutoOrikomiKaiteiNo", kihonVo.AutoOrikomiKaiteiNo, DbType.String)

                db.AddParameter("@MaterialInfoLength", kihonVo.MaterialInfoLength, DbType.Int32)
                db.AddParameter("@MaterialInfoWidth", kihonVo.MaterialInfoWidth, DbType.Int32)

                db.AddParameter("@ZairyoSunpoX", kihonVo.ZairyoSunpoX, DbType.Int32)
                db.AddParameter("@ZairyoSunpoY", kihonVo.ZairyoSunpoY, DbType.Int32)
                db.AddParameter("@ZairyoSunpoZ", kihonVo.ZairyoSunpoZ, DbType.Int32)
                db.AddParameter("@ZairyoSunpoXY", kihonVo.ZairyoSunpoXy, DbType.Int32)
                db.AddParameter("@ZairyoSunpoXZ", kihonVo.ZairyoSunpoXz, DbType.Int32)
                db.AddParameter("@ZairyoSunpoYZ", kihonVo.ZairyoSunpoYz, DbType.Int32)

                db.AddParameter("@MaterialInfoOrderTarget", kihonVo.MaterialInfoOrderTarget, DbType.String)
                db.AddParameter("@MaterialInfoOrderTargetDate", kihonVo.MaterialInfoOrderTargetDate, DbType.String)
                db.AddParameter("@MaterialInfoOrderChk", kihonVo.MaterialInfoOrderChk, DbType.String)
                db.AddParameter("@MaterialInfoOrderChkDate", kihonVo.MaterialInfoOrderChkDate, DbType.String)
                db.AddParameter("@DataItemKaiteiNo", kihonVo.DataItemKaiteiNo, DbType.String)
                db.AddParameter("@DataItemAreaName", kihonVo.DataItemAreaName, DbType.String)
                db.AddParameter("@DataItemSetName", kihonVo.DataItemSetName, DbType.String)
                db.AddParameter("@DataItemKaiteiInfo", kihonVo.DataItemKaiteiInfo, DbType.String)
                db.AddParameter("@DataItemDataProvision", kihonVo.DataItemDataProvision, DbType.String)
                db.AddParameter("@DataItemDataProvisionDate", kihonVo.DataItemDataProvisionDate, DbType.String)

                db.AddParameter("@UpdatedUserId", LoginInfo.Now.UserId, DbType.String)
                db.AddParameter("@UpdatedDate", aDate.CurrentDateDbFormat, DbType.String)
                db.AddParameter("@UpdatedTime", aDate.CurrentTimeDbFormat, DbType.String)
                db.ExecuteNonQuery(sb.ToString)
                db.Commit()
                db.Close()
            End Using
        End Sub

        ''' <summary>
        ''' 手配号車情報を追加
        ''' </summary>
        ''' <param name="gousyaVo">手配号車情報</param>
        ''' <remarks></remarks>
        Public Sub UpdateByTehaiGousya(ByVal gousyaVo As TShisakuTehaiGousyaVo) Implements KaiteiChusyutuDao.UpdateByTehaiGousya
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" SET SHISAKU_GOUSYA = @ShisakuGousya, ")
                .AppendLine(" INSU_SURYO = @InsuSuryo, ")
                .AppendLine(" UPDATED_USER_ID = @UpdatedUserId, ")
                .AppendLine(" UPDATED_DATE = @UpdatedDate, ")
                .AppendLine(" UPDATED_TIME = @UpdatedTime ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
                .AppendLine(" AND SHISAKU_GOUSYA_HYOUJI_JUN = @ShisakuGousyaHyoujiJun ")
            End With

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuTehaiGousyaVo
            param.ShisakuEventCode = gousyaVo.ShisakuEventCode
            param.ShisakuListCode = gousyaVo.ShisakuListCode
            param.ShisakuListCodeKaiteiNo = gousyaVo.ShisakuListCodeKaiteiNo
            param.ShisakuBukaCode = gousyaVo.ShisakuBukaCode
            param.ShisakuBlockNo = gousyaVo.ShisakuBlockNo
            param.BuhinNoHyoujiJun = gousyaVo.BuhinNoHyoujiJun
            param.ShisakuGousyaHyoujiJun = gousyaVo.ShisakuGousyaHyoujiJun
            param.ShisakuGousya = gousyaVo.ShisakuGousya
            param.InsuSuryo = gousyaVo.InsuSuryo
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sb.ToString, param)
        End Sub

        ''' <summary>
        ''' 部品編集改訂を更新
        ''' </summary>
        ''' <param name="buhinVo">手配基本情報</param>
        ''' <remarks></remarks>
        Public Sub UpdateByOrikomi(ByVal buhinVo As TShisakuBuhinEditKaiteiVo) Implements KaiteiChusyutuDao.UpdateByOrikomi
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI ")
                .AppendLine(" SET AUTO_ORIKOMI_KAITEI_NO = @AutoOrikomiKaiteiNo, ")
                .AppendLine(" UPDATED_USER_ID = @UpdatedUserId, ")
                .AppendLine(" UPDATED_DATE = @UpdatedDate, ")
                .AppendLine(" UPDATED_TIME = @UpdatedTime ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
            End With

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuBuhinEditKaiteiVo
            param.ShisakuEventCode = buhinVo.ShisakuEventCode
            param.ShisakuListCode = buhinVo.ShisakuListCode
            param.ShisakuListCodeKaiteiNo = buhinVo.ShisakuListCodeKaiteiNo
            param.ShisakuBukaCode = buhinVo.ShisakuBukaCode
            param.ShisakuBlockNo = buhinVo.ShisakuBlockNo
            param.BuhinNoHyoujiJun = buhinVo.BuhinNoHyoujiJun
            param.AutoOrikomiKaiteiNo = buhinVo.AutoOrikomiKaiteiNo
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sb.ToString, param)
        End Sub

        ''' <summary>
        ''' 自動織込みできるか手配基本情報を取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByOrikomiKihon(ByVal kihonVo As TShisakuBuhinEditKaiteiVo, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As List(Of TShisakuTehaiKihonVo) Implements KaiteiChusyutuDao.FindByOrikomiKihon
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", kihonVo.ShisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", kihonVo.ShisakuListCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", kihonVo.ShisakuListCodeKaiteiNo)
                .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", kihonVo.ShisakuBukaCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", kihonVo.ShisakuBlockNo)
                .AppendFormat(" AND LEVEL = {0} ", kihonVo.Level)
                .AppendFormat(" AND SHUKEI_CODE = '{0}'", kihonVo.ShukeiCode)
                .AppendFormat(" AND TORIHIKISAKI_CODE = '{0}'", kihonVo.MakerCode)
                .AppendFormat(" AND BUHIN_NO = '{0}'", kihonVo.BuhinNo)
                .AppendFormat(" AND KYOUKU_SECTION = '{0}' ", kihonVo.KyoukuSection)
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiKihonVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' リストコード情報の更新
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <remarks></remarks>
        Public Sub UpdateByListCode(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) Implements KaiteiChusyutuDao.UpdateByListCode
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE ")
                .AppendLine(" SET AUTO_ORIKOMI_FLAG = '1', ")
                .AppendLine(" UPDATED_USER_ID = @UpdatedUserId, ")
                .AppendLine(" UPDATED_DATE = @UpdatedDate, ")
                .AppendLine(" UPDATED_TIME = @UpdatedTime ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo ")
            End With

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuListcodeVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sb.ToString, param)
        End Sub

        ''' <summary>
        ''' 手配号車情報を削除
        ''' </summary>
        ''' <param name="kihonVo">手配号車情報</param>
        ''' <remarks></remarks>
        Public Sub DelteByGousya(ByVal kihonVo As TShisakuTehaiGousyaVo) Implements KaiteiChusyutuDao.DelteByGousya
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" DELETE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
                .AppendLine(" AND SHISAKU_GOUSYA_HYOUJI_JUN = @ShisakuGousyaHyoujiJun ")
            End With

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiGousyaVo
            param.ShisakuEventCode = kihonVo.ShisakuEventCode
            param.ShisakuListCode = kihonVo.ShisakuListCode
            param.ShisakuListCodeKaiteiNo = kihonVo.ShisakuListCodeKaiteiNo
            param.ShisakuBukaCode = kihonVo.ShisakuBukaCode
            param.ShisakuBlockNo = kihonVo.ShisakuBlockNo
            param.BuhinNoHyoujiJun = kihonVo.BuhinNoHyoujiJun
            param.ShisakuGousyaHyoujiJun = kihonVo.ShisakuGousyaHyoujiJun
            db.Delete(sb.ToString, param)
        End Sub

        ''' <summary>
        ''' バックアップ用に手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuListCode"></param>
        ''' <param name="shisakuListCodeKaiteiNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByAllTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As List(Of TShisakuTehaiKihonVo) Implements KaiteiChusyutuDao.FindByAllTehaiKihon
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TK.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiKihonVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' バックアップ用に手配号車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuListCode"></param>
        ''' <param name="shisakuListCodeKaiteiNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByAllTehaiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As List(Of TShisakuTehaiGousyaVo) Implements KaiteiChusyutuDao.FindByAllTehaiGousya
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TG.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiGousyaVo)(sb.ToString)

        End Function


        ''' <summary>
        ''' バックアップの手配基本情報を追加する
        ''' </summary>
        ''' <param name="tehaiKihonList">手配基本情報</param>
        ''' <remarks></remarks>
        Public Sub InsertByAllTehaiKihon(ByVal tehaiKihonList As List(Of TShisakuTehaiKihonVo)) Implements KaiteiChusyutuDao.InsertByAllTehaiKihon
            Dim sb As New StringBuilder

            Dim sqlList(tehaiKihonList.Count - 1) As String
            For index As Integer = 0 To tehaiKihonList.Count - 1
                With sb
                    .Remove(0, .Length)
                    .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ( ")
                    .AppendLine(" SHISAKU_EVENT_CODE, ")
                    .AppendLine(" SHISAKU_LIST_CODE, ")
                    .AppendLine(" SHISAKU_LIST_CODE_KAITEI_NO, ")
                    .AppendLine(" SHISAKU_BUKA_CODE, ")
                    .AppendLine(" SHISAKU_BLOCK_NO, ")
                    .AppendLine(" BUHIN_NO_HYOUJI_JUN, ")
                    .AppendLine(" SORT_JUN, ")
                    .AppendLine(" RIREKI, ")
                    .AppendLine(" GYOU_ID, ")
                    .AppendLine(" SENYOU_MARK, ")
                    .AppendLine(" LEVEL, ")
                    .AppendLine(" UNIT_KBN, ")
                    .AppendLine(" BUHIN_NO, ")
                    .AppendLine(" BUHIN_NO_KBN, ")
                    .AppendLine(" BUHIN_NO_KAITEI_NO, ")
                    .AppendLine(" EDA_BAN, ")
                    .AppendLine(" BUHIN_NAME, ")
                    .AppendLine(" SHUKEI_CODE, ")
                    .AppendLine(" GENCYO_CKD_KBN, ")
                    .AppendLine(" TEHAI_KIGOU, ")
                    .AppendLine(" KOUTAN, ")
                    .AppendLine(" TORIHIKISAKI_CODE, ")
                    .AppendLine(" NOUBA, ")
                    .AppendLine(" KYOUKU_SECTION, ")
                    .AppendLine(" NOUNYU_SHIJIBI, ")
                    .AppendLine(" TOTAL_INSU_SURYO, ")
                    .AppendLine(" SAISHIYOUFUKA, ")

                    .AppendLine(" GOUSYA_HACHU_TENKAI_FLG, ")
                    .AppendLine(" GOUSYA_HACHU_TENKAI_UNIT_KBN, ")

                    .AppendLine(" SHUTUZU_YOTEI_DATE, ")
                    .AppendLine(" SHUTUZU_JISEKI_DATE, ")
                    .AppendLine(" SHUTUZU_JISEKI_KAITEI_NO, ")
                    .AppendLine(" SHUTUZU_JISEKI_STSR_DHSTBA, ")
                    .AppendLine(" SAISYU_SETSUHEN_DATE, ")
                    .AppendLine(" SAISYU_SETSUHEN_KAITEI_NO, ")
                    .AppendLine(" STSR_DHSTBA, ")

                    .AppendLine(" ZAISHITU_KIKAKU_1, ")
                    .AppendLine(" ZAISHITU_KIKAKU_2, ")
                    .AppendLine(" ZAISHITU_KIKAKU_3, ")
                    .AppendLine(" ZAISHITU_MEKKI, ")
                    .AppendLine(" SHISAKU_BANKO_SURYO, ")
                    .AppendLine(" SHISAKU_BANKO_SURYO_U, ")
                    .AppendLine(" SHISAKU_BUHINN_HI, ")
                    .AppendLine(" SHISAKU_KATA_HI, ")
                    .AppendLine(" MAKER_CODE, ")
                    .AppendLine(" BIKOU, ")
                    .AppendLine(" BUHIN_NO_OYA, ")
                    .AppendLine(" BUHIN_NO_KBN_OYA, ")
                    .AppendLine(" ERROR_KBN, ")
                    .AppendLine(" AUD_FLAG, ")
                    .AppendLine(" AUD_BI, ")
                    .AppendLine(" KETUGOU_NO, ")
                    .AppendLine(" HENKATEN, ")
                    .AppendLine(" SHISAKU_SEIHIN_KBN, ")
                    .AppendLine(" AUTO_ORIKOMI_KAITEI_NO,")
                    .AppendLine(" TSUKURIKATA_SEISAKU, ")
                    .AppendLine(" TSUKURIKATA_KATASHIYOU_1, ")
                    .AppendLine(" TSUKURIKATA_KATASHIYOU_2, ")
                    .AppendLine(" TSUKURIKATA_KATASHIYOU_3, ")
                    .AppendLine(" TSUKURIKATA_TIGU, ")
                    .AppendLine(" TSUKURIKATA_NOUNYU, ")
                    .AppendLine(" TSUKURIKATA_KIBO, ")
                    .AppendLine(" BASE_BUHIN_FLG, ")
                    '
                    .AppendLine(" MATERIAL_INFO_LENGTH, ")
                    .AppendLine(" MATERIAL_INFO_WIDTH, ")

                    .AppendLine(" ZAIRYO_SUNPO_X, ")
                    .AppendLine(" ZAIRYO_SUNPO_Y, ")
                    .AppendLine(" ZAIRYO_SUNPO_Z, ")
                    .AppendLine(" ZAIRYO_SUNPO_XY, ")
                    .AppendLine(" ZAIRYO_SUNPO_XZ, ")
                    .AppendLine(" ZAIRYO_SUNPO_YZ, ")

                    .AppendLine(" MATERIAL_INFO_ORDER_TARGET, ")
                    .AppendLine(" MATERIAL_INFO_ORDER_TARGET_DATE, ")
                    .AppendLine(" MATERIAL_INFO_ORDER_CHK, ")
                    .AppendLine(" MATERIAL_INFO_ORDER_CHK_DATE, ")
                    .AppendLine(" DATA_ITEM_KAITEI_NO, ")
                    .AppendLine(" DATA_ITEM_AREA_NAME, ")
                    .AppendLine(" DATA_ITEM_SET_NAME, ")
                    .AppendLine(" DATA_ITEM_KAITEI_INFO, ")
                    .AppendLine(" DATA_ITEM_DATA_PROVISION, ")
                    .AppendLine(" DATA_ITEM_DATA_PROVISION_DATE, ")

                    .AppendLine(" CREATED_USER_ID, ")
                    .AppendLine(" CREATED_DATE, ")
                    .AppendLine(" CREATED_TIME, ")
                    .AppendLine(" UPDATED_USER_ID, ")
                    .AppendLine(" UPDATED_DATE, ")
                    .AppendLine(" UPDATED_TIME ")
                    .AppendLine(" ) ")
                    .AppendLine(" VALUES ( ")
                    .AppendLine(" '" & tehaiKihonList(index).ShisakuEventCode & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).ShisakuListCode & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).ShisakuListCodeKaiteiNo & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).ShisakuBukaCode & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).ShisakuBlockNo & "' , ")
                    .AppendLine(" " & tehaiKihonList(index).BuhinNoHyoujiJun & ", ")
                    .AppendLine(" " & tehaiKihonList(index).SortJun & ", ")
                    .AppendLine(" '" & tehaiKihonList(index).Rireki & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).GyouId & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).SenyouMark & "' , ")
                    .AppendLine(" " & tehaiKihonList(index).Level & ", ")
                    .AppendLine(" '" & tehaiKihonList(index).UnitKbn & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).BuhinNo & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).BuhinNoKbn & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).BuhinNoKaiteiNo & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).EdaBan & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).BuhinName & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).ShukeiCode & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).GencyoCkdKbn & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).TehaiKigou & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).Koutan & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).TorihikisakiCode & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).Nouba & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).KyoukuSection & "' , ")
                    .AppendLine(" " & tehaiKihonList(index).NounyuShijibi & ", ")
                    .AppendLine(" " & tehaiKihonList(index).TotalInsuSuryo & ", ")
                    .AppendLine(" '" & tehaiKihonList(index).Saishiyoufuka & "' , ")

                    .AppendLine(" '" & tehaiKihonList(index).GousyaHachuTenkaiFlg & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).GousyaHachuTenkaiUnitKbn & "' , ")

                    .AppendLine(" " & tehaiKihonList(index).ShutuzuYoteiDate & ", ")
                    .AppendLine(" " & tehaiKihonList(index).ShutuzuJisekiDate & " , ")
                    .AppendLine(" '" & tehaiKihonList(index).ShutuzuJisekiKaiteiNo & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).ShutuzuJisekiStsrDhstba & "' , ")
                    .AppendLine(" " & tehaiKihonList(index).SaisyuSetsuhenDate & " , ")
                    .AppendLine(" '" & tehaiKihonList(index).SaisyuSetsuhenKaiteiNo & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).StsrDhstba & "' , ")

                    .AppendLine(" '" & tehaiKihonList(index).ZaishituKikaku1 & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).ZaishituKikaku2 & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).ZaishituKikaku3 & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).ZaishituMekki & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).ShisakuBankoSuryo & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).ShisakuBankoSuryoU & "' , ")
                    .AppendLine(" " & tehaiKihonList(index).ShisakuBuhinnHi & ", ")
                    .AppendLine(" " & tehaiKihonList(index).ShisakuKataHi & ", ")
                    .AppendLine(" '" & tehaiKihonList(index).MakerCode & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).Bikou & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).BuhinNoOya & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).BuhinNoKbnOya & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).ErrorKbn & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).AudFlag & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).AudBi & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).KetugouNo & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).Henkaten & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).ShisakuSeihinKbn & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).AutoOrikomiKaiteiNo & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).TsukurikataSeisaku & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).TsukurikataKatashiyou1 & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).TsukurikataKatashiyou2 & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).TsukurikataKatashiyou3 & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).TsukurikataTigu & "' , ")
                    .AppendLine(" " & tehaiKihonList(index).TsukurikataNounyu & ", ")
                    .AppendLine(" '" & tehaiKihonList(index).TsukurikataKibo & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).BaseBuhinFlg & "' , ")
                    '
                    .AppendLine(" " & tehaiKihonList(index).MaterialInfoLength & " , ")
                    .AppendLine(" " & tehaiKihonList(index).MaterialInfoWidth & " , ")

                    .AppendLine(" " & tehaiKihonList(index).ZairyoSunpoX & " , ")
                    .AppendLine(" " & tehaiKihonList(index).ZairyoSunpoY & " , ")
                    .AppendLine(" " & tehaiKihonList(index).ZairyoSunpoZ & " , ")
                    .AppendLine(" " & tehaiKihonList(index).ZairyoSunpoXy & " , ")
                    .AppendLine(" " & tehaiKihonList(index).ZairyoSunpoXz & " , ")
                    .AppendLine(" " & tehaiKihonList(index).ZairyoSunpoYz & " , ")

                    .AppendLine(" '" & tehaiKihonList(index).MaterialInfoOrderTarget & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).MaterialInfoOrderTargetDate & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).MaterialInfoOrderChk & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).MaterialInfoOrderChkDate & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).DataItemKaiteiNo & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).DataItemAreaName & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).DataItemSetName & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).DataItemKaiteiInfo & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).DataItemDataProvision & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).DataItemDataProvisionDate & "' , ")

                    .AppendLine(" '" & tehaiKihonList(index).CreatedUserId & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).CreatedDate & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).CreatedTime & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).UpdatedUserId & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).UpdatedDate & "' , ")
                    .AppendLine(" '" & tehaiKihonList(index).UpdatedTime & "'  ")
                    .AppendLine(" ) ")
                End With
                sqlList(index) = sb.ToString
            Next

            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                insert.Open()
                'insert.BeginTransaction()
                Dim errorcount As Integer = 0
                For index As Integer = 0 To tehaiKihonList.Count - 1
                    'insert.ExecuteNonQuery(sqlList(index))
                    Try
                        '空なら何もしない'
                        If Not StringUtil.IsEmpty(sqlList(index)) Then
                            insert.ExecuteNonQuery(sqlList(index))
                        End If
                    Catch ex As SqlClient.SqlException
                        'プライマリキー違反のみ無視させたい'
                        Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
                        If prm < 0 Then
                            Continue For
                            'Dim msg As String = sqlList(index) + ex.Message
                            'MsgBox(ex.Message)
                        Else
                            Continue For
                        End If
                    End Try
                Next
                insert.Commit()
            End Using

        End Sub

        ''' <summary>
        ''' バックアップの手配号車情報を追加する
        ''' </summary>
        ''' <param name="tehaiGousyaList">手配号車情報</param>
        ''' <remarks></remarks>
        Public Sub InsertByAllTehaiGousya(ByVal tehaiGousyaList As List(Of TShisakuTehaiGousyaVo)) Implements KaiteiChusyutuDao.InsertByAllTehaiGousya
            Dim sb As New StringBuilder

            Dim sqlList(tehaiGousyaList.Count - 1) As String
            For index As Integer = 0 To tehaiGousyaList.Count - 1
                With sb
                    .Remove(0, .Length)
                    .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ( ")
                    .AppendLine(" SHISAKU_EVENT_CODE, ")
                    .AppendLine(" SHISAKU_LIST_CODE, ")
                    .AppendLine(" SHISAKU_LIST_CODE_KAITEI_NO, ")
                    .AppendLine(" SHISAKU_BUKA_CODE, ")
                    .AppendLine(" SHISAKU_BLOCK_NO, ")
                    .AppendLine(" BUHIN_NO_HYOUJI_JUN, ")
                    .AppendLine(" SORT_JUN, ")
                    .AppendLine(" GYOU_ID, ")
                    .AppendLine(" SHISAKU_GOUSYA_HYOUJI_JUN, ")
                    .AppendLine(" SHISAKU_GOUSYA, ")
                    .AppendLine(" INSU_SURYO, ")
                    .AppendLine(" M_NOUNYU_SHIJIBI, ")
                    .AppendLine(" T_NOUNYU_SHIJIBI, ")
                    .AppendLine(" CREATED_USER_ID, ")
                    .AppendLine(" CREATED_DATE, ")
                    .AppendLine(" CREATED_TIME, ")
                    .AppendLine(" UPDATED_USER_ID, ")
                    .AppendLine(" UPDATED_DATE, ")
                    .AppendLine(" UPDATED_TIME ")
                    .AppendLine(" ) ")
                    .AppendLine(" VALUES ( ")
                    .AppendLine(" '" & tehaiGousyaList(index).ShisakuEventCode & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).ShisakuListCode & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).ShisakuListCodeKaiteiNo & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).ShisakuBukaCode & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).ShisakuBlockNo & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).BuhinNoHyoujiJun & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).SortJun & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).GyouId & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).ShisakuGousyaHyoujiJun & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).ShisakuGousya & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).InsuSuryo & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).MNounyuShijibi & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).TNounyuShijibi & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).CreatedUserId & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).CreatedDate & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).CreatedTime & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).UpdatedUserId & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).UpdatedDate & "' , ")
                    .AppendLine(" '" & tehaiGousyaList(index).UpdatedTime & "'  ")
                    .AppendLine(" ) ")
                End With
                sqlList(index) = sb.ToString
            Next

            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                insert.Open()
                'insert.BeginTransaction()
                Dim errorcount As Integer = 0
                For index As Integer = 0 To tehaiGousyaList.Count - 1
                    'insert.ExecuteNonQuery(sqlList(index))
                    Try
                        '空なら何もしない'
                        If Not StringUtil.IsEmpty(sqlList(index)) Then
                            insert.ExecuteNonQuery(sqlList(index))
                        End If
                    Catch ex As SqlClient.SqlException
                        'プライマリキー違反のみ無視させたい'
                        Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
                        If prm < 0 Then
                            Continue For
                            'Dim msg As String = sqlList(index) + ex.Message
                            'MsgBox(ex.Message)
                        Else
                            Continue For
                        End If
                    End Try
                Next
                insert.Commit()
            End Using
        End Sub

        ''' <summary>
        ''' 手配基本情報を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuListCode"></param>
        ''' <param name="shisakuListCodeKaiteiNo"></param>
        ''' <remarks></remarks>
        Public Sub DeleteByAllTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) Implements KaiteiChusyutuDao.DeleteByAllTehaiKihon
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" DELETE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo ")
            End With

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiKihonVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo

            db.Delete(sb.ToString, param)
        End Sub

        ''' <summary>
        ''' 手配号車情報を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuListCode"></param>
        ''' <param name="shisakuListCodeKaiteiNo"></param>
        ''' <remarks></remarks>
        Public Sub DeleteByAllTehaiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) Implements KaiteiChusyutuDao.DeleteByAllTehaiGousya
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" DELETE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo ")
            End With

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiGousyaVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo

            db.Delete(sb.ToString, param)
        End Sub



#End Region



#Region "試作手配ブロック情報から最新改訂情報を取得する"
        ''' <summary>
        ''' 試作手配ブロック情報から最新改訂情報を取得する。
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuBlockNo">ブロック№</param>
        ''' <returns>ブロック情報</returns>
        ''' <remarks></remarks>

        Public Function FindByNewBlockList(ByVal eventcode As String, _
                                           ByVal shisakuListCode As String, _
                                           ByVal shisakuBlockNo As String, _
                                           ByVal shouninNichiji As Long) _
                                           As System.Collections.Generic.List(Of ShisakuBuhinBlockVo) Implements Dao.KaiteiChusyutuDao.FindByNewBlockList
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine("SELECT ")
                .AppendLine(" COALESCE(SHISAKU_EVENT_CODE,'') AS SHISAKU_EVENT_CODE , ")
                .AppendLine(" COALESCE(SHISAKU_BUKA_CODE,'') AS SHISAKU_BUKA_CODE , ")
                .AppendLine(" COALESCE(MAX(R.KA_RYAKU_NAME),SHISAKU_BUKA_CODE) AS KA_RYAKU_NAME , ")
                .AppendLine(" COALESCE(SHISAKU_BLOCK_NO,'') AS SHISAKU_BLOCK_NO , ")
                .AppendLine(" COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'') AS SHISAKU_BLOCK_NO_KAITEI_NO , ")
                .AppendLine(" COALESCE(BLOCK_FUYOU,'') AS BLOCK_FUYOU , ")
                .AppendLine(" COALESCE(JYOUTAI,'') AS JYOUTAI, ")
                .AppendLine(" COALESCE(UNIT_KBN,'') AS UNIT_KBN , ")
                .AppendLine(" COALESCE(SHISAKU_BLOCK_NAME,'') AS SHISAKU_BLOCK_NAME , ")
                .AppendLine(" COALESCE(SYAIN.SHAIN_NAME ,'') AS SYAIN_NAME , ")
                .AppendLine(" COALESCE(TEL_NO,'') AS TEL_NO , ")
                .AppendLine(" COALESCE(SAISYU_KOUSHINBI,'') AS SAISYU_KOUSHINBI, ")
                .AppendLine(" COALESCE(SAISYU_KOUSHINJIKAN,'') AS SAISYU_KOUSHINJIKAN, ")
                .AppendLine(" COALESCE(TANTO_SYOUNIN_JYOUTAI,'') AS TANTO_SYOUNIN_JYOUTAI, ")
                .AppendLine(" COALESCE(TANTO_SYOUNIN_KA,'') AS TANTO_SYOUNIN_KA, ")
                .AppendLine(" COALESCE(TANTO.SHAIN_NAME,TANTO_SYOUNIN_SYA) AS TANTO_NAME, ")
                .AppendLine(" COALESCE(TANTO_SYOUNIN_HI,'') AS TANTO_SYOUNIN_HI, ")
                .AppendLine(" COALESCE(TANTO_SYOUNIN_JIKAN,'') AS TANTO_SYOUNIN_JIKAN, ")
                .AppendLine(" COALESCE(KACHOU_SYOUNIN_JYOUTAI,'') AS KACHOU_SYOUNIN_JYOUTAI, ")
                .AppendLine(" COALESCE(KACHOU_SYOUNIN_KA,'') AS KACHOU_SYOUNIN_KA, ")
                .AppendLine(" COALESCE(KACHOU.SHAIN_NAME,KACHOU_SYOUNIN_SYA) AS KACHOU_NAME, ")
                .AppendLine(" COALESCE(KACHOU_SYOUNIN_HI,'') AS KACHOU_SYOUNIN_HI, ")
                .AppendLine(" COALESCE(KACHOU_SYOUNIN_JIKAN,'') AS KACHOU_SYOUNIN_JIKAN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK BLOCK WITH (NOLOCK, NOWAIT )")
                .AppendLine("   LEFT JOIN (SELECT A.SHAIN_NO, ")
                .AppendLine("           COALESCE( A.BUKA_CODE, B.BUKA_CODE) BUKA_CODE, ")
                .AppendLine("           COALESCE( A.SITE_KBN, B.SITE_KBN) SITE_KBN,  ")
                .AppendLine("           COALESCE( A.SHAIN_NAME, B.SHAIN_NAME) SHAIN_NAME,  ")
                .AppendLine("           COALESCE( A.SHAIN_NAME_EIJI, B.SHAIN_NAME_EIJI) SHAIN_NAME_EIJI, ")
                .AppendLine("           COALESCE( A.NAISEN_NO, B.NAISEN_NO) NAISEN_NO ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".DBO.RHAC0650 A  WITH (NOLOCK, NOWAIT) ")
                .AppendLine("   LEFT JOIN " & RHACLIBF_DB_NAME & ".DBO.RHAC2130 B WITH (NOLOCK, NOWAIT) ON A.SHAIN_NO = B.SHAIN_NO ")
                .AppendLine(" UNION ")
                .AppendLine("   SELECT ")
                .AppendLine("   SHAIN_NO, ")
                .AppendLine("   BUKA_CODE, ")
                .AppendLine("   SITE_KBN, ")
                .AppendLine("   SHAIN_NAME, ")
                .AppendLine("   SHAIN_NAME_EIJI, ")
                .AppendLine("   NAISEN_NO ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".DBO.RHAC2130 C ")
                .AppendLine("   WHERE  NOT EXISTS (SELECT * FROM " & RHACLIBF_DB_NAME & ".DBO.RHAC0650 WITH (NOLOCK, NOWAIT)  WHERE SHAIN_NO = C.SHAIN_NO)  ) SYAIN ON ")
                .AppendLine("           BLOCK.USER_ID=SYAIN.SHAIN_NO ")
                .AppendLine("   LEFT JOIN (SELECT A.SHAIN_NO, ")
                .AppendLine("       COALESCE( A.BUKA_CODE, B.BUKA_CODE) BUKA_CODE, ")
                .AppendLine("       COALESCE( A.SITE_KBN, B.SITE_KBN) SITE_KBN, ")
                .AppendLine("       COALESCE( A.SHAIN_NAME, B.SHAIN_NAME) SHAIN_NAME, ")
                .AppendLine("       COALESCE( A.SHAIN_NAME_EIJI, B.SHAIN_NAME_EIJI) SHAIN_NAME_EIJI, ")
                .AppendLine("       COALESCE( A.NAISEN_NO, B.NAISEN_NO) NAISEN_NO ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".DBO.RHAC0650 A WITH (NOLOCK, NOWAIT)  ")
                .AppendLine("   LEFT JOIN " & RHACLIBF_DB_NAME & ".DBO.RHAC2130 B WITH (NOLOCK, NOWAIT)  ")
                .AppendLine("   ON A.SHAIN_NO = B.SHAIN_NO ")
                .AppendLine(" UNION ")
                .AppendLine(" SELECT ")
                .AppendLine("   SHAIN_NO, ")
                .AppendLine("   BUKA_CODE, ")
                .AppendLine("   SITE_KBN, ")
                .AppendLine("   SHAIN_NAME, ")
                .AppendLine("   SHAIN_NAME_EIJI, ")
                .AppendLine("   NAISEN_NO ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".DBO.RHAC2130 C WITH (NOLOCK, NOWAIT)  ")
                .AppendLine("   WHERE NOT EXISTS (SELECT * FROM " & RHACLIBF_DB_NAME & ".DBO.RHAC0650 WHERE SHAIN_NO = C.SHAIN_NO)  ) TANTO ON ")
                .AppendLine("       BLOCK.TANTO_SYOUNIN_SYA = TANTO.SHAIN_NO ")
                .AppendLine(" LEFT JOIN (SELECT A.SHAIN_NO, ")
                .AppendLine("   COALESCE( A.BUKA_CODE, B.BUKA_CODE) BUKA_CODE, ")
                .AppendLine("   COALESCE( A.SITE_KBN, B.SITE_KBN) SITE_KBN, ")
                .AppendLine("   COALESCE( A.SHAIN_NAME, B.SHAIN_NAME) SHAIN_NAME, ")
                .AppendLine("   COALESCE( A.SHAIN_NAME_EIJI, B.SHAIN_NAME_EIJI) SHAIN_NAME_EIJI, ")
                .AppendLine("   COALESCE( A.NAISEN_NO, B.NAISEN_NO) NAISEN_NO ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".DBO.RHAC0650 A  WITH (NOLOCK, NOWAIT) ")
                .AppendLine("   LEFT JOIN " & RHACLIBF_DB_NAME & ".DBO.RHAC2130 B WITH (NOLOCK, NOWAIT)  ON A.SHAIN_NO = B.SHAIN_NO ")
                .AppendLine(" UNION ")
                .AppendLine(" SELECT ")
                .AppendLine("   SHAIN_NO, ")
                .AppendLine("   BUKA_CODE, ")
                .AppendLine("   SITE_KBN, ")
                .AppendLine("   SHAIN_NAME, ")
                .AppendLine("   SHAIN_NAME_EIJI, ")
                .AppendLine("   NAISEN_NO ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".DBO.RHAC2130 C WITH (NOLOCK, NOWAIT)  ")
                .AppendLine("   WHERE NOT EXISTS (SELECT * FROM " & RHACLIBF_DB_NAME & ".DBO.RHAC0650 WITH (NOLOCK, NOWAIT)  WHERE SHAIN_NO = C.SHAIN_NO)) ")
                .AppendLine("       KACHOU ON BLOCK.KACHOU_SYOUNIN_SYA=KACHOU.SHAIN_NO ")
                .AppendLine(" LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 R WITH (NOLOCK, NOWAIT)  ON BLOCK.SHISAKU_BUKA_CODE=R.BU_CODE+R.KA_CODE ")
                .AppendLine("   WHERE SHISAKU_EVENT_CODE ='" & eventcode & "'")
                .AppendLine("           AND SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "'")
                .AppendLine("           AND KACHOU_SYOUNIN_JYOUTAI = '36' ")
                .AppendLine("           AND CAST(KACHOU_SYOUNIN_HI AS BIGINT) * 1000000 + CAST(KACHOU_SYOUNIN_JIKAN AS BIGINT) <= " & CStr(shouninNichiji))
                .AppendLine("           AND SHISAKU_BLOCK_NO_KAITEI_NO >= '000' ")
                .AppendLine("           AND SHISAKU_BLOCK_NO_KAITEI_NO= ")
                .AppendLine("   (SELECT MAX(SHISAKU_BLOCK_NO_KAITEI_NO) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) ")
                .AppendLine("   WHERE(SHISAKU_EVENT_CODE = BLOCK.SHISAKU_EVENT_CODE) ")
                .AppendLine("       AND SHISAKU_BUKA_CODE=BLOCK.SHISAKU_BUKA_CODE ")
                .AppendLine("       AND SHISAKU_BLOCK_NO=BLOCK.SHISAKU_BLOCK_NO ")
                .AppendLine("       AND KACHOU_SYOUNIN_JYOUTAI = '36' ")
                .AppendLine("       AND CAST(KACHOU_SYOUNIN_HI AS BIGINT) * 1000000 + CAST(KACHOU_SYOUNIN_JIKAN AS BIGINT) <= " & CStr(shouninNichiji) & ") ")
                .AppendLine(" GROUP BY SHISAKU_EVENT_CODE, ")
                .AppendLine("       SHISAKU_BUKA_CODE,")
                .AppendLine("       SHISAKU_BLOCK_NO, ")
                .AppendLine("       SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine("       BLOCK_FUYOU, ")
                .AppendLine("       JYOUTAI,")
                .AppendLine("       UNIT_KBN, ")
                .AppendLine("       SHISAKU_BLOCK_NAME, ")
                .AppendLine("       TEL_NO, ")
                .AppendLine("       SYAIN.SHAIN_NAME, ")
                .AppendLine("       TANTO.SHAIN_NAME, ")
                .AppendLine("       KACHOU.SHAIN_NAME, ")
                .AppendLine("       KACHOU_SYOUNIN_HI, ")
                .AppendLine("       SAISYU_KOUSHINBI, ")
                .AppendLine("       SAISYU_KOUSHINJIKAN, ")
                .AppendLine("       TANTO_SYOUNIN_JYOUTAI, ")
                .AppendLine("       TANTO_SYOUNIN_KA, ")
                .AppendLine("       TANTO_SYOUNIN_HI, ")
                .AppendLine("       TANTO_SYOUNIN_JIKAN, ")
                .AppendLine("       KACHOU_SYOUNIN_JYOUTAI, ")
                .AppendLine("       KACHOU_SYOUNIN_KA, ")
                .AppendLine("       KACHOU_SYOUNIN_JIKAN, ")
                .AppendLine("       TANTO_SYOUNIN_SYA, ")
                .AppendLine("       KACHOU_SYOUNIN_SYA ")
                .AppendLine(" ORDER BY ")
                .AppendLine("   SHISAKU_EVENT_CODE, ")
                .AppendLine("   KA_RYAKU_NAME ASC,")
                .AppendLine("   SHISAKU_BLOCK_NO ASC")

            End With
            Dim db As New EBomDbClient
            Dim resultVoList As New List(Of ShisakuBuhinBlockVo)
            resultVoList = db.QueryForList(Of ShisakuBuhinBlockVo)(sql.ToString)

            Return resultVoList

        End Function
#End Region



#Region "改訂抽出EXCELと手配帳との差分用に号車員数情報を取得する。"
        ''' <summary>
        ''' 改訂抽出EXCELと手配帳との差分用に号車員数情報を取得する。
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuListCode"></param>
        ''' <param name="shisakuListCodeKaiteiNo"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <param name="buhinNoHyoujiJun"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByGousyaInsu(ByVal shisakuEventCode As String, _
                                         ByVal shisakuListCode As String, _
                                         ByVal shisakuListCodeKaiteiNo As String, _
                                         ByVal shisakuBlockNo As String, _
                                         ByVal buhinNoHyoujiJun As Integer) As List(Of TShisakuTehaiGousyaVo) Implements KaiteiChusyutuDao.FindByGousyaInsu
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA GOUSYA WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendFormat(" GOUSYA.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND GOUSYA.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND GOUSYA.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
                .AppendFormat(" AND GOUSYA.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND GOUSYA.BUHIN_NO_HYOUJI_JUN = {0} ", buhinNoHyoujiJun)
                .AppendFormat(" ORDER BY GOUSYA.SHISAKU_GOUSYA_HYOUJI_JUN ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiGousyaVo)(sb.ToString)
        End Function

#End Region

#Region "改訂抽出EXCELと手配帳との差分用に基本情報を取得する。"
        ''' <summary>
        ''' 改訂抽出EXCELと手配帳との差分用に基本情報を取得する。
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuListCode"></param>
        ''' <param name="shisakuListCodeKaiteiNo"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <param name="buhinNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByKihonSabun(ByVal shisakuEventCode As String, _
                                         ByVal shisakuListCode As String, _
                                         ByVal shisakuListCodeKaiteiNo As String, _
                                         ByVal shisakuBlockNo As String, _
                                         ByVal buhinNo As String) As List(Of TShisakuTehaiKihonVo) Implements KaiteiChusyutuDao.FindByKihonSabun
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON KIHON WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendLine(" KIHON.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND KIHON.SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine(" AND KIHON.SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo ")
                .AppendLine(" AND KIHON.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND KIHON.BUHIN_NO = @BuhinNo ")
            End With

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiKihonVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
            param.ShisakuBlockNo = shisakuBlockNo
            param.BuhinNo = buhinNo

            Return db.QueryForList(Of TShisakuTehaiKihonVo)(sb.ToString, param)
        End Function

#End Region

        ''' <summary>
        ''' ダミーを除く号車表示順の最大値を取得
        ''' </summary>
        ''' <param name="shisakueventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByMaxGousyaHyoujijunNotDummy(ByVal shisakuEventCode As String, _
                                                         ByVal shisakuListCode As String, _
                                                         ByVal shisakuListCodeKaiteiNo As String) As Integer Implements KaiteiChusyutuDao.FindByMaxGousyaHyoujijunNotDummy
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT MAX(SHISAKU_GOUSYA_HYOUJI_JUN) AS SHISAKU_GOUSYA_HYOUJI_JUN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
                .AppendLine(" AND NOT SHISAKU_GOUSYA = 'DUMMY' ")
            End With

            Dim db As New EBomDbClient
            Dim result As New TShisakuTehaiGousyaVo
            result = db.QueryForObject(Of TShisakuTehaiGousyaVo)(sb.ToString)
            Return result.ShisakuGousyaHyoujiJun
        End Function

        ''' <summary>
        ''' ダミー列情報の号車表示順を更新
        ''' </summary>
        ''' <param name="shisakueventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リスト改訂№</param>
        ''' <param name="shisakuGousyaHyoujijun"></param>
        ''' <remarks></remarks>
        Public Sub UpdateByMaxGousyaHyoujijunDummy(ByVal shisakuEventCode As String, _
                                                   ByVal shisakuListCode As String, _
                                                   ByVal shisakuListCodeKaiteiNo As String, _
                                                   ByVal shisakuGousyaHyoujijun As Integer) Implements KaiteiChusyutuDao.UpdateByMaxGousyaHyoujijunDummy
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" SET SHISAKU_GOUSYA_HYOUJI_JUN = @ShisakuGousyaHyoujiJun ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo ")
                .AppendLine(" AND SHISAKU_GOUSYA = 'DUMMY' ")
                .AppendLine(" AND SHISAKU_GOUSYA_HYOUJI_JUN <> @ShisakuGousyaHyoujiJun ")
            End With

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiGousyaVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
            param.ShisakuGousyaHyoujiJun = shisakuGousyaHyoujijun

            db.Update(sb.ToString, param)

        End Sub


        Public Sub SetTehaiKaiteiBlock(ByVal BlockNoList As List(Of TShisakuTehaiKaiteiBlockVo)) Implements KaiteiChusyutuDao.SetTehaiKaiteiBlock
            Dim aDate As New ShisakuDate
            Using con As New SqlClient.SqlConnection
                con.ConnectionString = NitteiDbComFunc.GetConnectString
                Dim cmd As New SqlClient.SqlCommand
                con.Open()
                cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "TehaiSetByTehaiKaiteiBlock"
                For Each param As TShisakuTehaiKaiteiBlockVo In BlockNoList
                    With cmd.Parameters
                        .Clear()
                        .Add("@ShisakuEventCode", SqlDbType.VarChar).Value = param.ShisakuEventCode
                        .Add("@ShisakuListCode", SqlDbType.VarChar).Value = param.ShisakuListCode
                        .Add("@ShisakuBukaCode", SqlDbType.VarChar).Value = param.ShisakuBukaCode
                        .Add("@ShisakuBlockNo", SqlDbType.VarChar).Value = param.ShisakuBlockNo
                        .Add("@ZenkaiBlockNoKaiteiNo", SqlDbType.VarChar).Value = ""
                        .Add("@KonkaiBlockNoKaiteiNo", SqlDbType.VarChar).Value = param.KonkaiBlockNoKaiteiNo
                        .Add("@InUserId", SqlDbType.VarChar).Value = LoginInfo.Now.UserId
                        .Add("@InDate", SqlDbType.VarChar).Value = aDate.CurrentDateDbFormat
                        .Add("@InTime", SqlDbType.VarChar).Value = aDate.CurrentTimeDbFormat
                    End With

                    cmd.ExecuteNonQuery()
                Next
            End Using

        End Sub

        ''' <summary>
        ''' 手配号車情報を取得する（納期取得用）
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <remarks></remarks>
        Public Function FindByTehaiGousyaNouki(ByVal shisakuEventCode As String, _
                                               ByVal shisakuListCode As String, _
                                               ByVal shisakuListCodeKaiteiNo As String, _
                                               ByVal shisakuBukaCode As String, _
                                               ByVal shisakuBlockNo As String, _
                                               ByVal shisakuGousya As String) As TShisakuTehaiGousyaVo Implements KaiteiChusyutuDao.FindByTehaiGousyaNouki

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT SHISAKU_EVENT_CODE,SHISAKU_LIST_CODE,SHISAKU_LIST_CODE_KAITEI_NO,SHISAKU_GOUSYA, ")
                .AppendLine(" SHISAKU_BUKA_CODE,SHISAKU_BLOCK_NO, ")
                .AppendLine(" M_NOUNYU_SHIJIBI,T_NOUNYU_SHIJIBI ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
                .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND SHISAKU_GOUSYA = '{0}' ", shisakuGousya)
                .AppendLine(" GROUP BY ")
                .AppendLine(" SHISAKU_EVENT_CODE,SHISAKU_LIST_CODE,SHISAKU_LIST_CODE_KAITEI_NO,SHISAKU_GOUSYA, ")
                .AppendLine(" SHISAKU_BUKA_CODE,SHISAKU_BLOCK_NO, ")
                .AppendLine(" M_NOUNYU_SHIJIBI,T_NOUNYU_SHIJIBI ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuTehaiGousyaVo)(sb.ToString)
        End Function

    End Class
End Namespace