Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.Impl
Imports ExcelOutput.TehaichoExcel.Vo
Imports System.Text

Namespace TehaichoExcel.Dao
    Public Class TehaichoExcelDaoImpl : Inherits DaoEachFeatureImpl
        Implements TehaichoExcelDao

        ''' <summary>
        ''' 試作部品表ファイル情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodekaiteiNo">リストコード改訂No</param>
        ''' <returns>試作部品表ファイル情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinFile(ByVal shisakuEventCode As String, _
                                        ByVal shisakuListCode As String, _
                                        ByVal shisakuListCodekaiteiNo As String, _
                                        ByVal ketugouNo As String) As AsPRPF02Vo Implements TehaichoExcelDao.FindByBuhinFile

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT P.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE L WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON L.SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND L.SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ")
                .AppendLine(" AND L.SHISAKU_LIST_CODE_KAITEI_NO = TK.SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.AS_PRPF02 P WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON P.OLD_LIST_CODE = L.OLD_LIST_CODE ")
                .AppendLine(" AND P.ZRKG = TK.KETUGOU_NO ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TK.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodekaiteiNo)
                .AppendFormat(" AND TK.KETUGOU_NO = '{0}' ", ketugouNo)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of AsPRPF02Vo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 発注納入状況ファイル情報を取得
        ''' </summary>
        ''' <param name="KOBA">工事No</param>
        ''' <param name="KBBA">管理No(行ID)</param>
        ''' <param name="BNBA">ブロックNo</param>
        ''' <param name="KoujiShireiNo">工事指令No</param>
        ''' <returns>発注納入状況ファイル情報</returns>
        ''' <remarks></remarks>
        Public Function FindByORPF32(ByVal KOBA As String, _
                                     ByVal KBBA As String, _
                                     ByVal BNBA As String, _
                                     ByVal KoujiShireiNo As String) As List(Of AsORPF32Vo) Implements TehaichoExcelDao.FindByORPF32
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_ORPF32 R ")
                .AppendLine(" WHERE ")
                .AppendFormat(" R.KOBA = '{0}' ", KOBA)
                .AppendFormat(" AND R.KBBA ='{0}' ", KBBA)
                .AppendFormat(" AND SUBSTRING(R.SGISBA, 4, 4) = '{0}' ", BNBA)
                .AppendFormat("  AND SUBSTRING(R.SGISBA, 1, 3) = '{0}' ", KoujiShireiNo)
                .AppendLine(" AND R.UPDATED_DATE = ( ")
                .AppendLine("  SELECT MAX ( UPDATED_DATE ) AS UPDATED_DATE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_ORPF32 ")
                .AppendLine(" WHERE KOBA = R.KOBA ")
                .AppendLine(" AND KBBA = R.KBBA  ")
                .AppendLine(" AND SGISBA = R.SGISBA ) ")
            End With

            Dim db As New EBomDbClient

            Return db.QueryForList(Of AsORPF32Vo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 現調品発注控情報を取得
        ''' </summary>
        ''' <param name="LSCD">旧リストコード</param>
        ''' <param name="KBBA">管理No(行ID)</param>
        ''' <param name="GyouId">行ID</param>
        ''' <returns>現調品発注控情報</returns>
        ''' <remarks></remarks>
        Public Function FindByORPF57(ByVal LSCD As String, ByVal KBBA As String, ByVal GyouId As String) As List(Of AsORPF57Vo) Implements TehaichoExcelDao.FindByORPF57
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_ORPF57 R ")
                .AppendLine(" WHERE ")
                .AppendFormat(" R.LSCD = '{0}' ", LSCD)
                .AppendFormat(" AND R.KBBA = '{0}' ", KBBA)
                .AppendFormat(" AND R.GYOID = '{0}' ", GyouId)
                .AppendLine(" AND R.UPDATED_DATE = ( ")
                .AppendLine("  SELECT MAX ( UPDATED_DATE ) AS UPDATED_DATE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_ORPF57 ")
                .AppendLine(" WHERE LSCD = R.LSCD ")
                .AppendLine(" AND KBBA = R.KBBA ")
                .AppendLine(" AND GYOID = R.GYOID ) ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of AsORPF57Vo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 新調達手配進捗ファイル情報を取得
        ''' </summary>
        ''' <param name="SGISBA">作業依頼書No</param>
        ''' <param name="KBBA">管理No</param>
        ''' <param name="CMBA">注文書No</param>
        ''' <param name="NOKM">納入区分</param>
        ''' <param name="HAYM">発注年月日</param>
        ''' <returns>新調達手配進捗ファイル情報</returns>
        ''' <remarks></remarks>
        Public Function FindByORPF60(ByVal SGISBA As String, ByVal KBBA As String, _
                                     ByVal CMBA As String, ByVal NOKM As String, ByVal HAYM As String) As AsORPF60Vo Implements TehaichoExcelDao.FindByORPF60

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_ORPF60 R ")
                .AppendLine(" WHERE ")
                .AppendFormat(" R.SGISBA = '{0}' ", SGISBA)
                .AppendFormat(" AND R.KBBA = '{0}' ", KBBA)
                .AppendFormat(" AND R.CMBA = '{0}' ", CMBA)
                .AppendFormat(" AND R.NOKM = '{0}' ", NOKM)
                .AppendFormat(" AND R.HAYM = '{0}' ", HAYM)
                .AppendLine(" AND R.UPDATED_DATE = ( ")
                .AppendLine("  SELECT MAX ( UPDATED_DATE ) AS UPDATED_DATE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_ORPF60 ")
                .AppendLine(" WHERE SGISBA = R.SGISBA ")
                .AppendLine(" AND KBBA = R.KBBA ")
                .AppendLine(" AND CMBA = R.CMBA ")
                .AppendLine(" AND NOKM = R.NOKM ")
                .AppendLine(" AND HAYM = R.HAYM ) ")
            End With


            Dim db As New EBomDbClient

            Return db.QueryForObject(Of AsORPF60Vo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 現調品手配進捗情報を取得
        ''' </summary>
        ''' <param name="GRNO">グループNo</param>
        ''' <param name="SRNO">シリアルNo</param>
        ''' <returns>現調品手配進捗情報</returns>
        ''' <remarks></remarks>
        Public Function FindByORPF61(ByVal GRNO As String, ByVal SRNO As String) As AsORPF61Vo Implements TehaichoExcelDao.FindByORPF61

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_ORPF61 R ")
                .AppendLine(" WHERE ")
                .AppendFormat("  R.GRNO = '{0}' ", GRNO)
                .AppendFormat(" AND R.SRNO = '{0}' ", SRNO)
                .AppendLine(" AND R.UPDATED_DATE = ( ")
                .AppendLine("  SELECT MAX ( UPDATED_DATE ) AS UPDATED_DATE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_ORPF61 ")
                .AppendLine(" WHERE GRNO = R.GRNO ")
                .AppendLine(" AND SRNO = R.SRNO ) ")

            End With

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of AsORPF61Vo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 試作手配号車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作手配号車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByTehaiGousya(ByVal shisakuEventCode As String) As List(Of TShisakuTehaiGousyaVo) Implements TehaichoExcelDao.FindByTehaiGousya
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine("SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TG.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendLine(" AND TG.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = TG.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TG.SHISAKU_LIST_CODE ) ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiGousyaVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 試作手配基本情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作手配基本情報</returns>
        ''' <remarks></remarks>
        Public Function FindByTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiKihonVoHelper) Implements TehaichoExcelDao.FindByTehaiKihon
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendFormat("SELECT TK.*, TG.SHISAKU_GOUSYA, TG.INSU_SURYO, TG.SHISAKU_GOUSYA_HYOUJI_JUN ")
                .AppendFormat(" FROM {0}.dbo.T_SHISAKU_TEHAI_KIHON TK WITH (NOLOCK, NOWAIT) ", MBOM_DB_NAME)
                .AppendFormat(" INNER JOIN {0}.dbo.T_SHISAKU_TEHAI_GOUSYA TG ", MBOM_DB_NAME)
                .AppendFormat(" ON TG.SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE ")
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ")
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE_KAITEI_NO = TK.SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendFormat(" AND TG.SHISAKU_BUKA_CODE = TK.SHISAKU_BUKA_CODE ")
                .AppendFormat(" AND TG.SHISAKU_BLOCK_NO = TK.SHISAKU_BLOCK_NO ")
                .AppendFormat(" AND TG.BUHIN_NO_HYOUJI_JUN = TK.BUHIN_NO_HYOUJI_JUN ")
                .AppendFormat(" WHERE ")
                .AppendFormat(" TK.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE = '{0}'", shisakuListCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendFormat(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendFormat(" FROM {0}.dbo.T_SHISAKU_TEHAI_KIHON ", MBOM_DB_NAME)
                .AppendFormat(" WHERE SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE ")
                .AppendFormat(" AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ) ")
                .AppendFormat("  ORDER BY TK.SHISAKU_BLOCK_NO, TK.GYOU_ID ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiKihonVoHelper)(sql.ToString)
        End Function

        ''' <summary>
        ''' 試作イベント情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作イベント情報</returns>
        ''' <remarks></remarks>
        Public Function FindByEvent(ByVal shisakuEventCode As String) As TShisakuEventVo Implements TehaichoExcelDao.FindByEvent
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine("SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT E ")
                .AppendLine(" WHERE ")
                .AppendFormat(" E.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 試作ベース車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns>試作ベース車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByEventBase(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuEventBaseVo) Implements TehaichoExcelDao.FindByEventBase

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT DISTINCT TG.SHISAKU_GOUSYA_HYOUJI_JUN AS HYOJIJUN_NO, ")
                .AppendLine(" TG.SHISAKU_GOUSYA ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE L ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG ")
                .AppendLine(" ON TG.SHISAKU_EVENT_CODE = L.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND TG.SHISAKU_LIST_CODE = L.SHISAKU_LIST_CODE ")
                .AppendLine(" AND TG.SHISAKU_LIST_CODE_KAITEI_NO = L.SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" WHERE ")
                .AppendFormat(" L.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND L.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND L.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE   ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = L.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = L.SHISAKU_LIST_CODE ) ")
                .AppendLine(" ORDER BY HYOJIJUN_NO ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventBaseVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 試作リストコード情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>リストコード情報</returns>
        ''' <remarks></remarks>
        Public Function FindByListCode(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As TShisakuListcodeVo Implements TehaichoExcelDao.FindByListCode
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine("SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE L ")
                .AppendLine(" WHERE ")
                .AppendFormat(" L.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND L.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND L.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = L.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = L.SHISAKU_LIST_CODE ) ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuListcodeVo)(sql.ToString)

        End Function


        ''' <summary>
        ''' 試作設計ブロック情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <returns>ユニット区分</returns>
        ''' <remarks></remarks>
        Public Function FindByShisakuBlockNo(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String) As String Implements TehaichoExcelDao.FindByShisakuBlockNo
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT UNIT_KBN ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
                .AppendLine(" WHERE ")
                .AppendFormat("    SHISAKU_EVENT_CODE =  '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_BUKA_CODE =  '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO  =  '{0}' ", shisakuBlockNo)
                .AppendLine(" GROUP BY UNIT_KBN ")
            End With
            Dim db As New EBomDbClient
            Dim vo As New TShisakuSekkeiBlockVo
            vo = db.QueryForObject(Of TShisakuSekkeiBlockVo)(sql.ToString)
            If vo Is Nothing Then
                Return ""
            Else
                Return vo.UnitKbn
            End If
        End Function

        Public Function FindByShisakuBlockNoForUnitKbn() As Hashtable Implements TehaichoExcelDao.FindByShisakuBlockNoForUnitKbn
            Dim ht As New Hashtable

            Dim sql As String = _
            " SELECT SHISAKU_EVENT_CODE,SHISAKU_BUKA_CODE,SHISAKU_BLOCK_NO,UNIT_KBN " _
            & "    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK "

            Dim db As New EBomDbClient

            Dim vos As List(Of TShisakuSekkeiBlockVo) = db.QueryForList(Of TShisakuSekkeiBlockVo)(sql)

            For Each vo As TShisakuSekkeiBlockVo In vos
                If Not ht.ContainsKey(vo.ShisakuEventCode & vo.ShisakuBukaCode & vo.ShisakuBlockNo) Then
                    ht.Add(vo.ShisakuEventCode & vo.ShisakuBukaCode & vo.ShisakuBlockNo, vo.UnitKbn)
                End If
            Next
            Return ht
        End Function

    End Class
End Namespace