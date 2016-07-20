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

Namespace TehaichoMenu.Impl
    Public Class TeiseiTsuchiDaoImpl : Inherits DaoEachFeatureImpl
        Implements TeiseiTsuchiDao

#Region "取得する処理(FindBy)"

        ''' <summary>
        ''' 指定の改訂Noの手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <returns>指定の改訂Noの手配基本情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBeforeTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As List(Of TShisakuTehaiKihonVo) Implements TeiseiTsuchiDao.FindByBeforeTehaiKihon
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT TK.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK ")
                .AppendFormat(" WHERE TK.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
                .AppendLine(" ORDER BY SHISAKU_BLOCK_NO, BUHIN_NO_HYOUJI_JUN ")
            End With
            'Dim sql As String = _
            '" SELECT TK.* " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK " _
            '& " WHERE TK.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            '& " AND TK.SHISAKU_LIST_CODE = @ShisakuListCode " _
            '& " AND TK.SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo " _
            '& " ORDER BY SHISAKU_BLOCK_NO, BUHIN_NO_HYOUJI_JUN "

            Dim db As New EBomDbClient
            'Dim param As New TShisakuTehaiKihonVo
            'param.ShisakuEventCode = shisakuEventCode
            'param.ShisakuListCode = shisakuListCode
            'param.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo

            Return db.QueryForList(Of TShisakuTehaiKihonVo)(sql.ToString)

        End Function

        ''' <summary>
        ''' 最新の手配号車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>最新の手配号車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByTehaiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiGousyaVo) Implements TeiseiTsuchiDao.FindByTehaiGousya
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT TG.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG")
                .AppendLine(" LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK ")
                .AppendLine(" ON TG.SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND TG.SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ")
                .AppendLine(" AND TG.SHISAKU_BUKA_CODE = TK.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND TG.BUHIN_NO_HYOUJI_JUN = TK.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" AND TG.SHISAKU_LIST_CODE_KAITEI_NO = TK.SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendFormat(" WHERE TG.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND TG.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ) ")
                .AppendLine(" ORDER BY BUHIN_NO_HYOUJI_JUN ")
            End With
            'Dim sql As String = _
            '" SELECT TG.* " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG" _
            '& " LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK " _
            '& " ON TG.SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE " _
            '& " AND TG.SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE " _
            '& " AND TG.SHISAKU_BUKA_CODE = TK.SHISAKU_BUKA_CODE " _
            '& " AND TG.BUHIN_NO_HYOUJI_JUN = TK.BUHIN_NO_HYOUJI_JUN " _
            '& " AND TG.SHISAKU_LIST_CODE_KAITEI_NO = TK.SHISAKU_LIST_CODE_KAITEI_NO " _
            '& " WHERE TG.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            '& " AND TG.SHISAKU_LIST_CODE = @ShisakuListCode " _
            '& " AND TG.SHISAKU_LIST_CODE_KAITEI_NO = ( " _
            '& " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON " _
            '& " WHERE SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE " _
            '& " AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ) " _
            '& " ORDER BY BUHIN_NO_HYOUJI_JUN "

            Dim db As New EBomDbClient
            'Dim param As New TShisakuTehaiKihonVo
            'param.ShisakuEventCode = shisakuEventCode
            'param.ShisakuListCode = shisakuListCode

            Return db.QueryForList(Of TShisakuTehaiGousyaVo)(sql.ToString)

        End Function

        '''' <summary>
        '''' 指定の改訂Noの手配号車情報を取得する
        '''' </summary>
        '''' <param name="shisakuEventCode">イベントコード</param>
        '''' <param name="shisakuListCode">リストコード</param>
        '''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        '''' <returns>指定の改訂Noの手配号車情報</returns>
        '''' <remarks></remarks>
        'Public Function FindByBeforeTehaiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As List(Of TShisakuTehaiGousyaVo) Implements TeiseiTsuchiDao.FindByBeforeTehaiGousya
        '    Dim sql As New System.Text.StringBuilder
        '    With sql
        '        .AppendLine(" SELECT TG.* ")
        '        .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG")
        '        .AppendLine(" LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK ")
        '        .AppendLine(" ON TG.SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE ")
        '        .AppendLine(" AND TG.SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ")
        '        .AppendLine(" AND TG.SHISAKU_BUKA_CODE = TK.SHISAKU_BUKA_CODE ")
        '        .AppendLine(" AND TG.BUHIN_NO_HYOUJI_JUN = TK.BUHIN_NO_HYOUJI_JUN ")
        '        .AppendLine(" AND TG.SHISAKU_LIST_CODE_KAITEI_NO = TK.SHISAKU_LIST_CODE_KAITEI_NO ")
        '        .AppendFormat(" WHERE TG.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
        '        .AppendFormat(" AND TG.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
        '        .AppendFormat(" AND TG.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
        '        .AppendLine(" ORDER BY BUHIN_NO_HYOUJI_JUN ")
        '    End With
        '    'Dim sql As String = _
        '    '" SELECT TG.* " _
        '    '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG" _
        '    '& " LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK " _
        '    '& " ON TG.SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE " _
        '    '& " AND TG.SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE " _
        '    '& " AND TG.SHISAKU_BUKA_CODE = TK.SHISAKU_BUKA_CODE " _
        '    '& " AND TG.BUHIN_NO_HYOUJI_JUN = TK.BUHIN_NO_HYOUJI_JUN " _
        '    '& " AND TG.SHISAKU_LIST_CODE_KAITEI_NO = TK.SHISAKU_LIST_CODE_KAITEI_NO " _
        '    '& " WHERE TG.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
        '    '& " AND TG.SHISAKU_LIST_CODE = @ShisakuListCode " _
        '    '& " AND TG.SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo " _
        '    '& " ORDER BY BUHIN_NO_HYOUJI_JUN "

        '    Dim db As New EBomDbClient
        '    'Dim param As New TShisakuTehaiKihonVo
        '    'param.ShisakuEventCode = shisakuEventCode
        '    'param.ShisakuListCode = shisakuListCode
        '    'param.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo

        '    Return db.QueryForList(Of TShisakuTehaiGousyaVo)(sql.ToString)

        'End Function

        ''↓↓2015/01/12 メタル改訂抽出修正) (TES)劉 CHG BEGIN
        ''' <summary>
        ''' 最新の手配情報取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="level">レベル</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当する手配基本情報</returns>
        ''' <remarks></remarks>
        Public Function FindByNewTehaiKihon(ByVal shisakuEventCode As String, _
                                            ByVal shisakuListCode As String, _
                                            ByVal shisakuBukaCode As String, _
                                            ByVal shisakuBlockNo As String, _
                                            ByVal level As Integer, _
                                            ByVal buhinNo As String) As TShisakuTehaiKihonVo Implements TeiseiTsuchiDao.FindByNewTehaiKihon
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine("SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TK.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND TK.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND TK.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND TK.LEVEL = {0} ", level)
                .AppendFormat(" AND TK.BUHIN_NO = '{0}' ", buhinNo)
                .AppendLine(" AND TK.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE )")
                .AppendLine(" ORDER BY BUHIN_NO_HYOUJI_JUN ")
            End With
            'Dim sql As String = _
            '"SELECT * " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK " _
            '& " WHERE " _
            '& " TK.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            '& " AND TK.SHISAKU_LIST_CODE = @ShisakuListCode " _
            '& " AND TK.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            '& " AND TK.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            '& " AND TK.LEVEL = @Level " _
            '& " AND TK.BUHIN_NO = @BuhinNo " _
            '& " AND TK.SHISAKU_LIST_CODE_KAITEI_NO = ( " _
            '& " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON " _
            '& " WHERE SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE " _
            '& " AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE )" _
            '& " ORDER BY BUHIN_NO_HYOUJI_JUN "

            Dim db As New EBomDbClient
            'Dim param As New TShisakuTehaiKihonVo
            'param.ShisakuEventCode = shisakuEventCode
            'param.ShisakuListCode = shisakuListCode
            'param.ShisakuBukaCode = shisakuBukaCode
            'param.ShisakuBlockNo = shisakuBlockNo

            'param.Level = level
            'param.BuhinNo = buhinNo

            'param.GyouId = gyouId

            Return db.QueryForObject(Of TShisakuTehaiKihonVo)(sql.ToString)
        End Function
        ''↑↑2015/01/12 メタル改訂抽出修正) (TES)劉 CHG END

        ''' <summary>
        ''' 最新の手配号車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品No表示順</param>
        ''' <returns>該当する手配号車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByNewTehaiGousya(ByVal shisakuEventCode As String, _
                                             ByVal shisakuListCode As String, _
                                             ByVal shisakuListCodeKaiteiNo As String, _
                                             ByVal shisakuBukaCode As String, _
                                             ByVal shisakuBlockNo As String, _
                                             ByVal buhinNoHyoujiJun As Integer) As List(Of TShisakuTehaiGousyaVo) Implements TeiseiTsuchiDao.FindByNewTehaiGousya

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TG.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE = '{0}'", shisakuListCode)
                .AppendFormat(" AND TG.SHISAKU_BUKA_CODE = '{0}'", shisakuBukaCode)
                .AppendFormat(" AND TG.SHISAKU_BLOCK_NO = '{0}'", shisakuBlockNo)
                .AppendFormat(" AND TG.BUHIN_NO_HYOUJI_JUN = {0}", buhinNoHyoujiJun)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE_KAITEI_NO = '{0}'", shisakuListCodeKaiteiNo)
                .AppendLine(" ORDER BY BUHIN_NO_HYOUJI_JUN ")
            End With

            Dim db As New EBomDbClient

            Return db.QueryForList(Of TShisakuTehaiGousyaVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 直前の手配号車情報を取得する(追加用)
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品No表示順</param>
        ''' <returns>該当する最新の手配号車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBeforeGousya(ByVal shisakuEventCode As String, _
                                           ByVal shisakuListCode As String, _
                                           ByVal shisakuBukaCode As String, _
                                           ByVal shisakuBlockNo As String, _
                                           ByVal buhinNoHyoujiJun As String, _
                                           ByVal shisakuListCodeKaiteiNo As String) As List(Of TShisakuTehaiGousyaVo) Implements TeiseiTsuchiDao.FindByBeforeGousya
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT TG.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG")
                .AppendFormat(" WHERE TG.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE = '{0}'", shisakuListCode)
                .AppendFormat(" AND TG.SHISAKU_BUKA_CODE = '{0}'", shisakuBukaCode)
                .AppendFormat(" AND TG.SHISAKU_BLOCK_NO = '{0}'", shisakuBlockNo)
                .AppendFormat(" AND TG.BUHIN_NO_HYOUJI_JUN = {0}", buhinNoHyoujiJun)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE_KAITEI_NO = '{0}'", shisakuListCodeKaiteiNo)
                .AppendLine(" ORDER BY BUHIN_NO_HYOUJI_JUN ")
            End With

            Dim db As New EBomDbClient

            Return db.QueryForList(Of TShisakuTehaiGousyaVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 最新を除く全ての改訂Noを取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>該当する全てのリストコード</returns>
        ''' <remarks></remarks>
        Public Function FindByKaiteiNoList(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuListcodeVo) Implements TeiseiTsuchiDao.FindByKaiteiNoList
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE L ")
                .AppendLine(" WHERE ")
                .AppendFormat(" L.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND L.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND NOT L.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = L.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = L.SHISAKU_LIST_CODE ) ")
                .AppendLine(" ORDER BY SHISAKU_LIST_CODE_KAITEI_NO DESC")
            End With

            Dim db As New EBomDbClient

            Return db.QueryForList(Of TShisakuListcodeVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 最新の訂正手配基本情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>該当する全てのリストコード</returns>
        ''' <remarks></remarks>
        Public Function FindByTeiseiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As List(Of TShisakuTehaiTeiseiKihonVoHelper) Implements TeiseiTsuchiDao.FindByTeiseiKihon
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT TTK.*, TTG.SHISAKU_GOUSYA, TTG.INSU_SURYO, TTG.SHISAKU_GOUSYA_HYOUJI_JUN, TTG.M_NOUNYU_SHIJIBI, TTG.T_NOUNYU_SHIJIBI ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_TEISEI_KIHON TTK ")
                .AppendLine(" LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_TEISEI_GOUSYA TTG ")
                .AppendLine(" ON TTG.SHISAKU_EVENT_CODE = TTK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND TTG.SHISAKU_LIST_CODE = TTK.SHISAKU_LIST_CODE ")
                .AppendLine(" AND TTG.SHISAKU_LIST_CODE_KAITEI_NO = TTK.SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" AND TTG.SHISAKU_BUKA_CODE = TTK.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND TTG.SHISAKU_BLOCK_NO = TTK.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND TTG.BUHIN_NO_HYOUJI_JUN = TTK.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" AND TTG.FLAG = TTK.FLAG ")
                .AppendFormat(" WHERE TTK.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TTK.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND TTK.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
                .AppendLine(" ORDER BY SHISAKU_BLOCK_NO, BUHIN_NO_HYOUJI_JUN, GYOU_ID, FLAG ")
            End With

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiTeiseiKihonVoHelper

            Return db.QueryForList(Of TShisakuTehaiTeiseiKihonVoHelper)(sql.ToString)
        End Function

        ''' <summary>
        ''' 訂正手配号車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>該当する全ての訂正号車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByTeiseiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiTeiseiGousyaVo) Implements TeiseiTsuchiDao.FindByTeiseiGousya
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT TTG.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_TEISEI_GOUSYA TTG ")
                .AppendFormat(" WHERE TTG.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TTG.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND TTG.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = TTG.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TTG.SHISAKU_LIST_CODE ) ")
                .AppendLine(" ORDER BY SHISAKU_BLOCK_NO, GYOU_ID ,BUHIN_NO_HYOUJI_JUN ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiTeiseiGousyaVo)(sql.ToString)
        End Function


        ''' <summary>
        ''' 指定の改訂Noの訂正手配号車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="kaiteiNo">改訂No</param>
        ''' <returns>該当する全ての訂正号車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByOldTeiseiGousya(ByVal shisakuEventCode As String, _
                                              ByVal shisakuListCode As String, _
                                              ByVal kaiteiNo As String, _
                                              ByVal shisakuBukaCode As String, _
                                              ByVal shisakuBlockNo As String, _
                                              ByVal buhinNoHyoujiJun As String) As TShisakuTehaiTeiseiGousyaVo Implements TeiseiTsuchiDao.FindByOldTeiseiGousya
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT TTG.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_TEISEI_GOUSYA TTG ")
                .AppendFormat(" WHERE TTG.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TTG.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND TTG.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", kaiteiNo)
                .AppendLine(" ORDER BY SHISAKU_BLOCK_NO, GYOU_ID ,BUHIN_NO_HYOUJI_JUN ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuTehaiTeiseiGousyaVo)(sql.ToString)
        End Function


        '''' <summary>
        '''' AS/400の部品情報を取得
        '''' </summary>
        '''' <param name="shisakuEventCode">イベントコード</param>
        '''' <param name="shisakuListCode">リストコード</param>
        '''' <returns>該当する全ての部品情報</returns>
        '''' <remarks></remarks>
        'Public Function FindByAS400(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodekaiteiNo As String) As List(Of TeiseiTsuchiExcelVo) Implements TeiseiTsuchiDao.FindByAS400
        '    'テーブルが無いので保留'
        '    Dim sql As New System.Text.StringBuilder
        '    With sql
        '        .AppendLine(" SELECT TTG.* ")
        '        .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_TEISEI_GOUSYA TTG ")
        '        .AppendLine(" WHERE TTG.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
        '        .AppendLine(" AND TTG.SHISAKU_LIST_CODE = @ShisakuListCode ")
        '        .AppendLine(" AND TTG.SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo ")
        '        .AppendLine(" ORDER BY SHISAKU_BLOCK_NO, GYOU_ID ,BUHIN_NO_HYOUJI_JUN ")
        '    End With

        '    Dim db As New EBomDbClient
        '    Dim param As New TShisakuTehaiTeiseiGousyaVo
        '    param.ShisakuEventCode = shisakuEventCode
        '    param.ShisakuListCode = shisakuListCode
        '    param.ShisakuListCodeKaiteiNo = shisakuListCodekaiteiNo

        '    Return db.QueryForList(Of TeiseiTsuchiExcelVo)(sql.ToString, param)
        'End Function

        ''' <summary>
        ''' 試作設計ブロック情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <returns>ユニット区分</returns>
        ''' <remarks></remarks>
        Public Function FindByShisakuBlockNo(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal shisakuBlockNoKaiteiNo As String) As String Implements TeiseiTsuchiDao.FindByShisakuBlockNo
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT UNIT_KBN ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
                .AppendLine(" WHERE ")
                .AppendFormat("    SHISAKU_EVENT_CODE         = '{0}' AND ", shisakuEventCode)
                .AppendFormat("    SHISAKU_BLOCK_NO           = '{0}'   AND ", shisakuBlockNo)
                .AppendFormat("    SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendLine(" GROUP BY UNIT_KBN ")
            End With
            Dim db As New EBomDbClient

            '2012/06/12 樺澤 ユニット区分が存在しない場合がある'
            Dim vo As New TShisakuSekkeiBlockVo
            Dim unitkbn As String = String.Empty
            vo = db.QueryForObject(Of TShisakuSekkeiBlockVo)(sql.ToString)
            If Not vo Is Nothing Then
                If Not StringUtil.IsEmpty(vo.UnitKbn) Then
                    unitkbn = vo.UnitKbn
                End If
            End If

            Return unitkbn
            'Return db.QueryForObject(Of TShisakuSekkeiBlockVo)(sql, param).UnitKbn
        End Function


#End Region

#Region "AS400用"

        ''' <summary>
        ''' 試作部品表ファイル情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodekaiteiNo">リストコード改訂No</param>
        ''' <returns>試作部品表ファイル情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinFile(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodekaiteiNo As String, ByVal ketugouNo As String) As AsPRPF02Vo Implements TeiseiTsuchiDao.FindByBuhinFile
            Dim sql As String = _
            " SELECT P.* " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_TEISEI_KIHON TK " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE L " _
            & " ON L.SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE " _
            & " AND L.SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE " _
            & " AND L.SHISAKU_LIST_CODE_KAITEI_NO = TK.SHISAKU_LIST_CODE_KAITEI_NO " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.AS_PRPF02 P " _
            & " ON P.OLD_LIST_CODE = L.OLD_LIST_CODE " _
            & " AND P.ZRKG = TK.KETUGOU_NO " _
            & " WHERE " _
            & " TK.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND TK.SHISAKU_LIST_CODE = @ShisakuListCode " _
            & " AND TK.SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo " _
            & " AND TK.KETUGOU_NO = @KetugouNo "

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiTeiseiKihonVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuListCodeKaiteiNo = shisakuListCodekaiteiNo
            param.KetugouNo = ketugouNo

            Return db.QueryForObject(Of AsPRPF02Vo)(sql, param)
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
        Public Function FindByORPF32(ByVal KOBA As String, ByVal KBBA As String, ByVal BNBA As String, ByVal KoujiShireiNo As String) As List(Of AsORPF32Vo) Implements TeiseiTsuchiDao.FindByORPF32
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_ORPF32 R ")
                .AppendLine(" WHERE ")
                .AppendLine(" R.KOBA = '" & KOBA & "' ")
                .AppendLine(" AND R.KBBA = '" & KBBA & "' ")
                .AppendLine(" AND SUBSTRING(R.SGISBA, 4, 4) = '" & BNBA & "' ")
                .AppendLine("  AND SUBSTRING(R.SGISBA, 1, 3) = '" & KoujiShireiNo & "' ")
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
        ''' 新調達手配進捗ファイル情報を取得
        ''' </summary>
        ''' <param name="SGISBA">作業依頼書No</param>
        ''' <param name="KBBA">管理No</param>
        ''' <param name="CMBA">注文書No</param>
        ''' <param name="NOKM">納入区分</param>
        ''' <param name="HAYM">発注年月日</param>
        ''' <returns>新調達手配進捗ファイル情報</returns>
        ''' <remarks></remarks>
        Public Function FindByORPF60(ByVal SGISBA As String, ByVal KBBA As String, ByVal CMBA As String, _
                                     ByVal NOKM As String, ByVal HAYM As String) As AsORPF60Vo Implements TeiseiTsuchiDao.FindByORPF60

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_ORPF60 R ")
                .AppendLine(" WHERE ")
                .AppendLine(" R.SGISBA = @Sgisba ")
                .AppendLine(" AND R.KBBA = @Kbba ")
                .AppendLine(" AND R.CMBA = @Cmba ")
                .AppendLine(" AND R.NOKM = @Nokm ")
                .AppendLine(" AND R.HAYM = @Haym ")
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
            Dim param As New AsORPF60Vo
            param.Sgisba = SGISBA
            param.Kbba = KBBA
            param.Cmba = CMBA
            param.Nokm = NOKM
            param.Haym = HAYM

            Return db.QueryForObject(Of AsORPF60Vo)(sb.ToString, param)
        End Function

        ''' <summary>
        ''' 現調品発注控情報を取得
        ''' </summary>
        ''' <param name="LSCD">旧リストコード</param>
        ''' <param name="KBBA">管理No(行ID)</param>
        ''' <param name="GyouId">行ID</param>
        ''' <returns>現調品発注控情報</returns>
        ''' <remarks></remarks>
        Public Function FindByORPF57(ByVal LSCD As String, ByVal KBBA As String, ByVal GyouId As String) As List(Of AsORPF57Vo) Implements TeiseiTsuchiDao.FindByORPF57
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.AS_ORPF57 R " _
            & " WHERE " _
            & " R.LSCD = @Lscd " _
            & " AND R.KBBA = @Kbba " _
            & " AND R.GYOID = @Gyoid " _
            & " AND R.UPDATED_DATE = ( " _
            & "  SELECT MAX ( UPDATED_DATE ) AS UPDATED_DATE " _
            & " FROM " & MBOM_DB_NAME & ".dbo.AS_ORPF57 " _
            & " WHERE LSCD = R.LSCD " _
            & " AND KBBA = R.KBBA " _
            & " AND GYOID = R.GYOID ) "

            Dim db As New EBomDbClient
            Dim param As New AsORPF57Vo
            param.Lscd = LSCD
            param.Kbba = KBBA
            param.Gyoid = GyouId

            Return db.QueryForList(Of AsORPF57Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 現調品手配進捗情報を取得
        ''' </summary>
        ''' <param name="GRNO">グループNo</param>
        ''' <param name="SRNO">シリアルNo</param>
        ''' <returns>現調品手配進捗情報</returns>
        ''' <remarks></remarks>
        Public Function FindByORPF61(ByVal GRNO As String, ByVal SRNO As String) As AsORPF61Vo Implements TeiseiTsuchiDao.FindByORPF61
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_ORPF61 R ")
                .AppendLine(" WHERE ")
                .AppendLine("  R.GRNO = @Grno ")
                .AppendLine(" AND R.SRNO = @Srno ")
                .AppendLine(" AND R.UPDATED_DATE = ( ")
                .AppendLine("  SELECT MAX ( UPDATED_DATE ) AS UPDATED_DATE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_ORPF61 ")
                .AppendLine(" WHERE GRNO = R.GRNO ")
                .AppendLine(" AND SRNO = R.SRNO ) ")

            End With

            Dim db As New EBomDbClient
            Dim param As New AsORPF61Vo
            param.Grno = GRNO
            param.Srno = SRNO

            Return db.QueryForObject(Of AsORPF61Vo)(sb.ToString, param)
        End Function

        ''' <summary>
        ''' 試作ベース車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBase(ByVal shisakuEventCode As String) As List(Of TShisakuEventBaseVo) Implements TeiseiTsuchiDao.FindByBase
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventBaseVo
            param.ShisakuEventCode = shisakuEventCode

            Return db.QueryForList(Of TShisakuEventBaseVo)(sql, param)
        End Function

#End Region

#Region "更新する処理(UpdateBy)"

        ''' <summary>
        ''' リストコードの訂正通知抽出日と時間を更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub UpdateByTeiseiChusyutubi(ByVal shisakuEventCode As String, _
                               ByVal shisakuListCode As String) Implements TeiseiTsuchiDao.UpdateByTeiseiChusyutubi
            Dim sql As String = _
            " UPDATE L " _
            & " SET " _
            & " L.TEISEI_CHUSYUTUBI = @TeiseiChusyutubi, " _
            & " L.TEISEI_CHUSYUTUJIKAN = @TeiseiChusyutujikan, " _
            & " L.UPDATED_USER_ID = @UpdatedUserId, " _
            & " L.UPDATED_DATE = @UpdatedDate, " _
            & " L.UPDATED_TIME = @UpdatedTime " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE L " _
            & " WHERE L.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND L.SHISAKU_LIST_CODE = @ShisakuListCode " _
            & " AND L.SHISAKU_LIST_CODE_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE " _
            & " WHERE SHISAKU_EVENT_CODE = L.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_LIST_CODE = L.SHISAKU_LIST_CODE ) "


            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuListcodeVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.TeiseiChusyutubi = Integer.Parse(Replace(aDate.CurrentDateDbFormat, "-", ""))
            param.TeiseiChusyutujikan = Integer.Parse(Replace(aDate.CurrentTimeDbFormat, ":", ""))

            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)


        End Sub

#End Region

#Region "追加する処理(InsertBy)"

        ''' <summary>
        ''' 訂正基本情報を追加する(追加、手配記号変更、変更無し)
        ''' </summary>
        ''' <param name="newKihonVo">最新の手配基本情報</param>
        ''' <param name="flag">フラグ番号(追加:1, 削除:2, 変更前:3, 変更後:4, 変更(手配記号):5, 変更無し:ブランク)</param>
        ''' <remarks></remarks>
        Public Sub InsetByTeiseiKihon(ByVal newKihonVo As TShisakuTehaiKihonVo, ByVal flag As String) Implements TeiseiTsuchiDao.InsetByTeiseiKihon

            Dim aDate As New ShisakuDate

            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_TEISEI_KIHON ( " _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_LIST_CODE, " _
            & " SHISAKU_LIST_CODE_KAITEI_NO, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
            & " BUHIN_NO_HYOUJI_JUN, " _
            & " FLAG, " _
            & " SORT_JUN, " _
            & " RIREKI, " _
            & " GYOU_ID, " _
            & " SENYOU_MARK, " _
            & " LEVEL, " _
            & " UNIT_KBN, " _
            & " BUHIN_NO, " _
            & " BUHIN_NO_KBN, " _
            & " BUHIN_NO_KAITEI_NO, " _
            & " EDA_BAN, " _
            & " BUHIN_NAME, " _
            & " SHUKEI_CODE, " _
            & " GENCYO_CKD_KBN, " _
            & " TEHAI_KIGOU, " _
            & " KOUTAN, " _
            & " TORIHIKISAKI_CODE, " _
            & " NOUBA, " _
            & " KYOUKU_SECTION, " _
            & " NOUNYU_SHIJIBI, " _
            & " TOTAL_INSU_SURYO, " _
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
            & " SHISAKU_BUHINN_HI, " _
            & " SHISAKU_KATA_HI, " _
            & " MAKER_CODE, " _
            & " BIKOU, " _
            & " BUHIN_NO_OYA, " _
            & " BUHIN_NO_KBN_OYA, " _
            & " ERROR_KBN, " _
            & " AUD_FLAG, " _
            & " AUD_BI, " _
            & " KETUGOU_NO, " _
            & " HENKATEN, " _
            & " TEISEI_CHUSYUTUBI, " _
            & " TEISEI_CHUSYUTUJIKAN, " _
            & " SHISAKU_SEIHIN_KBN, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & "'" & newKihonVo.ShisakuEventCode & "', " _
            & "'" & newKihonVo.ShisakuListCode & "', " _
            & "'" & newKihonVo.ShisakuListCodeKaiteiNo & "', " _
            & "'" & newKihonVo.ShisakuBukaCode & "', " _
            & "'" & newKihonVo.ShisakuBlockNo & "', " _
            & "'" & newKihonVo.BuhinNoHyoujiJun & "', " _
            & "'" & flag & "', " _
            & "'" & newKihonVo.SortJun & "', " _
            & "'" & newKihonVo.Rireki & "', " _
            & "'" & newKihonVo.GyouId & "', " _
            & "'" & newKihonVo.SenyouMark & "', " _
            & "'" & newKihonVo.Level & "', " _
            & "'" & newKihonVo.UnitKbn & "', " _
            & "'" & newKihonVo.BuhinNo & "', " _
            & "'" & newKihonVo.BuhinNoKbn & "', " _
            & "'" & newKihonVo.BuhinNoKaiteiNo & "', " _
            & "'" & newKihonVo.EdaBan & "', " _
            & "'" & newKihonVo.BuhinName & "', " _
            & "'" & newKihonVo.ShukeiCode & "', " _
            & "'" & newKihonVo.GencyoCkdKbn & "', " _
            & "'" & newKihonVo.TehaiKigou & "', " _
            & "'" & newKihonVo.Koutan & "', " _
            & "'" & newKihonVo.TorihikisakiCode & "', " _
            & "'" & newKihonVo.Nouba & "', " _
            & "'" & newKihonVo.KyoukuSection & "', " _
            & "'" & newKihonVo.NounyuShijibi & "', " _
            & "'" & newKihonVo.TotalInsuSuryo & "', " _
            & "'" & newKihonVo.Saishiyoufuka & "', " _
            & "'" & newKihonVo.GousyaHachuTenkaiFlg & "', " _
            & "'" & newKihonVo.GousyaHachuTenkaiUnitKbn & "', " _
            & "'" & newKihonVo.ShutuzuYoteiDate & "', " _
            & "'" & newKihonVo.ShutuzuJisekiDate & "', " _
            & "'" & newKihonVo.ShutuzuJisekiKaiteiNo & "', " _
            & "'" & newKihonVo.ShutuzuJisekiStsrDhstba & "', " _
            & "'" & newKihonVo.SaisyuSetsuhenDate & "', " _
            & "'" & newKihonVo.SaisyuSetsuhenKaiteiNo & "', " _
            & "'" & newKihonVo.StsrDhstba & "', " _
            & "'" & newKihonVo.ZaishituKikaku1 & "', " _
            & "'" & newKihonVo.ZaishituKikaku2 & "', " _
            & "'" & newKihonVo.ZaishituKikaku3 & "', " _
            & "'" & newKihonVo.ZaishituMekki & "', " _
            & "'" & newKihonVo.TsukurikataSeisaku & "' , " _
            & "'" & newKihonVo.TsukurikataKatashiyou1 & "' , " _
            & "'" & newKihonVo.TsukurikataKatashiyou2 & "' , " _
            & "'" & newKihonVo.TsukurikataKatashiyou3 & "' , " _
            & "'" & newKihonVo.TsukurikataTigu & "' , " _
            & "'" & newKihonVo.TsukurikataNounyu & "' , " _
            & "'" & newKihonVo.TsukurikataKibo & "' , " _
            & "'" & newKihonVo.ShisakuBankoSuryo & "', " _
            & "'" & newKihonVo.ShisakuBankoSuryoU & "', " _
            & "'" & newKihonVo.MaterialInfoLength & "', " _
            & "'" & newKihonVo.MaterialInfoWidth & "', " _
            & "'" & newKihonVo.ZairyoSunpoX & "', " _
            & "'" & newKihonVo.ZairyoSunpoY & "', " _
            & "'" & newKihonVo.ZairyoSunpoZ & "', " _
            & "'" & newKihonVo.ZairyoSunpoXy & "', " _
            & "'" & newKihonVo.ZairyoSunpoXz & "', " _
            & "'" & newKihonVo.ZairyoSunpoYz & "', " _
            & "'" & newKihonVo.MaterialInfoOrderTarget & "', " _
            & "'" & newKihonVo.MaterialInfoOrderTargetDate & "', " _
            & "'" & newKihonVo.MaterialInfoOrderChk & "', " _
            & "'" & newKihonVo.MaterialInfoOrderChkDate & "', " _
            & "'" & newKihonVo.DataItemKaiteiNo & "', " _
            & "'" & newKihonVo.DataItemAreaName & "', " _
            & "'" & newKihonVo.DataItemSetName & "', " _
            & "'" & newKihonVo.DataItemKaiteiInfo & "', " _
            & "'" & newKihonVo.DataItemDataProvision & "', " _
            & "'" & newKihonVo.DataItemDataProvisionDate & "', " _
            & "'" & newKihonVo.ShisakuBuhinnHi & "', " _
            & "'" & newKihonVo.ShisakuKataHi & "', " _
            & "'" & newKihonVo.MakerCode & "', " _
            & "'" & newKihonVo.Bikou & "', " _
            & "'" & newKihonVo.BuhinNoOya & "', " _
            & "'" & newKihonVo.BuhinNoKbnOya & "', " _
            & "'" & newKihonVo.ErrorKbn & "', " _
            & "'" & newKihonVo.AudFlag & "', " _
            & "'" & newKihonVo.AudBi & "', " _
            & "'" & newKihonVo.KetugouNo & "', " _
            & "'" & newKihonVo.Henkaten & "', " _
            & "null, " _
            & "null, " _
            & "'" & newKihonVo.ShisakuSeihinKbn & "', " _
            & "'" & LoginInfo.Now.UserId & "', " _
            & "'" & aDate.CurrentDateDbFormat & "', " _
            & "'" & aDate.CurrentTimeDbFormat & "', " _
            & "'" & LoginInfo.Now.UserId & "', " _
            & "'" & aDate.CurrentDateDbFormat & "', " _
            & "'" & aDate.CurrentTimeDbFormat & "' " _
            & " ) "
            '↓↓2014/09/24 酒井 ADD BEGIN
            '作り方項目を追加
            '↑↑2014/09/24 酒井 ADD END

            ''↓↓2015/01/12 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
            '作り方項目を追加
            ''↑↑2015/01/12 メタル改訂抽出追加_z) (TES)劉 ADD END

            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                insert.Open()
                insert.BeginTransaction()
                Dim errorcount As Integer = 0
                'insert.ExecuteNonQuery(sqlList(index))
                Try
                    '空なら何もしない'
                    insert.ExecuteNonQuery(sql)
                Catch ex As SqlClient.SqlException
                    'プライマリキー違反のみ無視させたい'
                    Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
                    If prm < 0 Then
                        MsgBox(ex.Message)
                    End If
                End Try
                insert.Commit()
            End Using

        End Sub

        ''' <summary>
        ''' 訂正号車情報を追加する(追加、手配記号変更、変更無し)
        ''' </summary>
        ''' <param name="newGousyaListVo">最新の手配号車情報</param>
        ''' <param name="flag">フラグ番号</param>
        ''' <remarks></remarks>
        Sub InsetByTeiseiGousya(ByVal newGousyaListVo As List(Of TShisakuTehaiGousyaVo), ByVal flag As String) Implements TeiseiTsuchiDao.InsetByTeiseiGousya


            '配列定義
            Dim sqlHairetu(1000) As String
            Dim aDate As New ShisakuDate
            For index As Integer = 0 To newGousyaListVo.Count - 1

                Dim sql As String = _
                " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_TEISEI_GOUSYA ( " _
                & " SHISAKU_EVENT_CODE, " _
                & " SHISAKU_LIST_CODE, " _
                & " SHISAKU_LIST_CODE_KAITEI_NO, " _
                & " SHISAKU_BUKA_CODE, " _
                & " SHISAKU_BLOCK_NO, " _
                & " BUHIN_NO_HYOUJI_JUN, " _
                & " FLAG, " _
                & " SORT_JUN, " _
                & " GYOU_ID, " _
                & " SHISAKU_GOUSYA_HYOUJI_JUN, " _
                & " SHISAKU_GOUSYA, " _
                & " INSU_SURYO, " _
                & " M_NOUNYU_SHIJIBI, " _
                & " T_NOUNYU_SHIJIBI, " _
                & " CREATED_USER_ID, " _
                & " CREATED_DATE, " _
                & " CREATED_TIME, " _
                & " UPDATED_USER_ID, " _
                & " UPDATED_DATE, " _
                & " UPDATED_TIME " _
                & " ) " _
                & " VALUES ( " _
                & " '" & newGousyaListVo(index).ShisakuEventCode & "', " _
                & " '" & newGousyaListVo(index).ShisakuListCode & "', " _
                & " '" & newGousyaListVo(index).ShisakuListCodeKaiteiNo & "', " _
                & " '" & newGousyaListVo(index).ShisakuBukaCode & "', " _
                & " '" & newGousyaListVo(index).ShisakuBlockNo & "', " _
                & " '" & newGousyaListVo(index).BuhinNoHyoujiJun & "', " _
                & " '" & flag & "', " _
                & " '" & newGousyaListVo(index).SortJun & "', " _
                & " '" & newGousyaListVo(index).GyouId & "', " _
                & " '" & newGousyaListVo(index).ShisakuGousyaHyoujiJun & "', " _
                & " '" & newGousyaListVo(index).ShisakuGousya & "', " _
                & " '" & newGousyaListVo(index).InsuSuryo & "', " _
                & " '" & newGousyaListVo(index).MNounyuShijibi & "', " _
                & " '" & newGousyaListVo(index).TNounyuShijibi & "', " _
                & " '" & LoginInfo.Now.UserId & "' ," _
                & " '" & aDate.CurrentDateDbFormat & "' ," _
                & " '" & aDate.CurrentTimeDbFormat & "' ," _
                & " '" & LoginInfo.Now.UserId & "' ," _
                & " '" & aDate.CurrentDateDbFormat & "' ," _
                & " '" & aDate.CurrentTimeDbFormat & "' " _
                & " ) "

                '& " VALUES ( " _
                '& " @ShisakuEventCode, " _
                '& " @ShisakuListCode, " _
                '& " @ShisakuListCodeKaiteiNo, " _
                '& " @ShisakuBukaCode, " _
                '& " @ShisakuBlockNo, " _
                '& " @BuhinNoHyoujiJun, " _
                '& " @Flag, " _
                '& " @SortJun, " _
                '& " @GyouId, " _
                '& " @ShisakuGousyaHyoujiJun, " _
                '& " @ShisakuGousya, " _
                '& " @InsuSuryo, " _
                '& " @CreatedUserId, " _
                '& " @CreatedDate, " _
                '& " @CreatedTime, " _
                '& " @UpdatedUserId, " _
                '& " @UpdatedDate, " _
                '& " @UpdatedTime " _
                '& " ) "

                sqlHairetu(index) = sql

            Next

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.BeginTransaction()
                For index As Integer = 0 To newGousyaListVo.Count - 1
                    If Not StringUtil.IsEmpty(sqlHairetu(index)) Then
                        db.ExecuteNonQuery(sqlHairetu(index))
                    End If
                Next
                db.Commit()
            End Using

        End Sub

        ''' <summary>
        ''' 訂正基本情報を追加する(削除)
        ''' </summary>
        ''' <param name="shisakuEventCode">最新の試作イベントコード</param>
        ''' <param name="shisakuListCode">最新の試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">最新のリストコード改訂No</param>
        ''' <param name="beforeKihonVo">前回の手配基本情報</param>
        ''' <param name="flag">フラグ番号</param>
        ''' <remarks></remarks>
        Public Sub InsetByTeiseiKihonDel(ByVal shisakuEventCode As String, _
                                   ByVal shisakuListCode As String, _
                                   ByVal shisakuListCodeKaiteiNo As String, _
                                   ByVal beforeKihonVo As TShisakuTehaiKihonVo, ByVal flag As String) Implements TeiseiTsuchiDao.InsetByTeiseiKihonDel

            Dim aDate As New ShisakuDate

            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_TEISEI_KIHON ( " _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_LIST_CODE, " _
            & " SHISAKU_LIST_CODE_KAITEI_NO, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
            & " BUHIN_NO_HYOUJI_JUN, " _
            & " FLAG, " _
            & " SORT_JUN, " _
            & " RIREKI, " _
            & " GYOU_ID, " _
            & " SENYOU_MARK, " _
            & " LEVEL, " _
            & " UNIT_KBN, " _
            & " BUHIN_NO, " _
            & " BUHIN_NO_KBN, " _
            & " BUHIN_NO_KAITEI_NO, " _
            & " EDA_BAN, " _
            & " BUHIN_NAME, " _
            & " SHUKEI_CODE, " _
            & " GENCYO_CKD_KBN, " _
            & " TEHAI_KIGOU, " _
            & " KOUTAN, " _
            & " TORIHIKISAKI_CODE, " _
            & " NOUBA, " _
            & " KYOUKU_SECTION, " _
            & " NOUNYU_SHIJIBI, " _
            & " TOTAL_INSU_SURYO, " _
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
            & " SHISAKU_BUHINN_HI, " _
            & " SHISAKU_KATA_HI, " _
            & " MAKER_CODE, " _
            & " BIKOU, " _
            & " BUHIN_NO_OYA, " _
            & " BUHIN_NO_KBN_OYA, " _
            & " ERROR_KBN, " _
            & " AUD_FLAG, " _
            & " AUD_BI, " _
            & " KETUGOU_NO, " _
            & " HENKATEN, " _
            & " TEISEI_CHUSYUTUBI, " _
            & " TEISEI_CHUSYUTUJIKAN, " _
            & " SHISAKU_SEIHIN_KBN, " _
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
            & "'" & beforeKihonVo.ShisakuBukaCode & "', " _
            & "'" & beforeKihonVo.ShisakuBlockNo & "', " _
            & "'" & beforeKihonVo.BuhinNoHyoujiJun & "', " _
            & "'" & flag & "', " _
            & "'" & beforeKihonVo.SortJun & "', " _
            & "'" & beforeKihonVo.Rireki & "', " _
            & "'" & beforeKihonVo.GyouId & "', " _
            & "'" & beforeKihonVo.SenyouMark & "', " _
            & "'" & beforeKihonVo.Level & "', " _
            & "'" & beforeKihonVo.UnitKbn & "', " _
            & "'" & beforeKihonVo.BuhinNo & "', " _
            & "'" & beforeKihonVo.BuhinNoKbn & "', " _
            & "'" & beforeKihonVo.BuhinNoKaiteiNo & "', " _
            & "'" & beforeKihonVo.EdaBan & "', " _
            & "'" & beforeKihonVo.BuhinName & "', " _
            & "'" & beforeKihonVo.ShukeiCode & "', " _
            & "'" & beforeKihonVo.GencyoCkdKbn & "', " _
            & "'" & beforeKihonVo.TehaiKigou & "', " _
            & "'" & beforeKihonVo.Koutan & "', " _
            & "'" & beforeKihonVo.TorihikisakiCode & "', " _
            & "'" & beforeKihonVo.Nouba & "', " _
            & "'" & beforeKihonVo.KyoukuSection & "', " _
            & "'" & beforeKihonVo.NounyuShijibi & "', " _
            & "'" & beforeKihonVo.TotalInsuSuryo & "', " _
            & "'" & beforeKihonVo.Saishiyoufuka & "', " _
            & "'" & beforeKihonVo.GousyaHachuTenkaiFlg & "', " _
            & "'" & beforeKihonVo.GousyaHachuTenkaiUnitKbn & "', " _
            & "'" & beforeKihonVo.ShutuzuYoteiDate & "', " _
            & "'" & beforeKihonVo.ShutuzuJisekiDate & "', " _
            & "'" & beforeKihonVo.ShutuzuJisekiKaiteiNo & "', " _
            & "'" & beforeKihonVo.ShutuzuJisekiStsrDhstba & "', " _
            & "'" & beforeKihonVo.SaisyuSetsuhenDate & "', " _
            & "'" & beforeKihonVo.SaisyuSetsuhenKaiteiNo & "', " _
            & "'" & beforeKihonVo.StsrDhstba & "', " _
            & "'" & beforeKihonVo.ZaishituKikaku1 & "', " _
            & "'" & beforeKihonVo.ZaishituKikaku2 & "', " _
            & "'" & beforeKihonVo.ZaishituKikaku3 & "', " _
            & "'" & beforeKihonVo.ZaishituMekki & "', " _
            & "'" & beforeKihonVo.TsukurikataSeisaku & "' , " _
            & "'" & beforeKihonVo.TsukurikataKatashiyou1 & "' , " _
            & "'" & beforeKihonVo.TsukurikataKatashiyou2 & "' , " _
            & "'" & beforeKihonVo.TsukurikataKatashiyou3 & "' , " _
            & "'" & beforeKihonVo.TsukurikataTigu & "' , " _
            & "'" & beforeKihonVo.TsukurikataNounyu & "' , " _
            & "'" & beforeKihonVo.TsukurikataKibo & "' , " _
            & "'" & beforeKihonVo.ShisakuBankoSuryo & "', " _
            & "'" & beforeKihonVo.ShisakuBankoSuryoU & "', " _
            & "'" & beforeKihonVo.MaterialInfoLength & "', " _
            & "'" & beforeKihonVo.MaterialInfoWidth & "', " _
            & "'" & beforeKihonVo.ZairyoSunpoX & "', " _
            & "'" & beforeKihonVo.ZairyoSunpoY & "', " _
            & "'" & beforeKihonVo.ZairyoSunpoZ & "', " _
            & "'" & beforeKihonVo.ZairyoSunpoXy & "', " _
            & "'" & beforeKihonVo.ZairyoSunpoXz & "', " _
            & "'" & beforeKihonVo.ZairyoSunpoYz & "', " _
            & "'" & beforeKihonVo.MaterialInfoOrderTarget & "', " _
            & "'" & beforeKihonVo.MaterialInfoOrderTargetDate & "', " _
            & "'" & beforeKihonVo.MaterialInfoOrderChk & "', " _
            & "'" & beforeKihonVo.MaterialInfoOrderChkDate & "', " _
            & "'" & beforeKihonVo.DataItemKaiteiNo & "', " _
            & "'" & beforeKihonVo.DataItemAreaName & "', " _
            & "'" & beforeKihonVo.DataItemSetName & "', " _
            & "'" & beforeKihonVo.DataItemKaiteiInfo & "', " _
            & "'" & beforeKihonVo.DataItemDataProvision & "', " _
            & "'" & beforeKihonVo.DataItemDataProvisionDate & "', " _
            & "'" & beforeKihonVo.ShisakuBuhinnHi & "', " _
            & "'" & beforeKihonVo.ShisakuKataHi & "', " _
            & "'" & beforeKihonVo.MakerCode & "', " _
            & "'" & beforeKihonVo.Bikou & "', " _
            & "'" & beforeKihonVo.BuhinNoOya & "', " _
            & "'" & beforeKihonVo.BuhinNoKbnOya & "', " _
            & "'" & beforeKihonVo.ErrorKbn & "', " _
            & "'" & beforeKihonVo.AudFlag & "', " _
            & "'" & beforeKihonVo.AudBi & "', " _
            & "'" & beforeKihonVo.KetugouNo & "', " _
            & "'" & beforeKihonVo.Henkaten & "', " _
            & "null, " _
            & "null, " _
            & "'" & beforeKihonVo.ShisakuSeihinKbn & "', " _
            & "'" & LoginInfo.Now.UserId & "', " _
            & "'" & aDate.CurrentDateDbFormat & "', " _
            & "'" & aDate.CurrentTimeDbFormat & "', " _
            & "'" & LoginInfo.Now.UserId & "', " _
            & "'" & aDate.CurrentDateDbFormat & "', " _
            & "'" & aDate.CurrentTimeDbFormat & "' " _
            & " ) "
            '↓↓2014/09/24 酒井 ADD BEGIN
            '作り方項目を追加
            '↑↑2014/09/24 酒井 ADD END

            ''↓↓2015/01/12 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
            '作り方項目を追加
            ''↑↑2015/01/12 メタル改訂抽出追加_z) (TES)劉 ADD END

            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                insert.Open()
                insert.BeginTransaction()
                Dim errorcount As Integer = 0
                'insert.ExecuteNonQuery(sqlList(index))
                Try
                    '空なら何もしない'
                    insert.ExecuteNonQuery(sql)
                Catch ex As SqlClient.SqlException
                    'プライマリキー違反のみ無視させたい'
                    Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
                    If prm < 0 Then
                        MsgBox(ex.Message)
                    End If
                End Try
                insert.Commit()
            End Using


        End Sub

        ''' <summary>
        ''' 訂正号車情報を追加する(削除)
        ''' </summary>
        ''' <param name="shisakuEventCode">最新の試作イベントコード</param>
        ''' <param name="shisakuListCode">最新の試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">最新のリストコード改訂No</param>
        ''' <param name="beforeGousyaVo">最新の手配号車情報</param>
        ''' <param name="flag">フラグ番号</param>
        ''' <remarks></remarks>
        Public Sub InsetByTeiseiGousyaDel(ByVal shisakuEventCode As String, _
                                          ByVal shisakuListCode As String, _
                                          ByVal shisakuListCodeKaiteiNo As String, _
                                          ByVal beforeGousyaVo As TShisakuTehaiGousyaVo, _
                                          ByVal flag As String) Implements TeiseiTsuchiDao.InsetByTeiseiGousyaDel

            Dim aDate As New ShisakuDate

            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_TEISEI_GOUSYA ( " _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_LIST_CODE, " _
            & " SHISAKU_LIST_CODE_KAITEI_NO, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
            & " BUHIN_NO_HYOUJI_JUN, " _
            & " FLAG, " _
            & " SORT_JUN, " _
            & " GYOU_ID, " _
            & " SHISAKU_GOUSYA_HYOUJI_JUN, " _
            & " SHISAKU_GOUSYA, " _
            & " INSU_SURYO, " _
            & " M_NOUNYU_SHIJIBI, " _
            & " T_NOUNYU_SHIJIBI, " _
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
            & "'" & beforeGousyaVo.ShisakuBukaCode & "', " _
            & "'" & beforeGousyaVo.ShisakuBlockNo & "', " _
            & "'" & beforeGousyaVo.BuhinNoHyoujiJun & "', " _
            & "'" & flag & "', " _
            & "'" & beforeGousyaVo.SortJun & "', " _
            & "'" & beforeGousyaVo.GyouId & "', " _
            & "'" & beforeGousyaVo.ShisakuGousyaHyoujiJun & "', " _
            & "'" & beforeGousyaVo.ShisakuGousya & "', " _
            & "'" & beforeGousyaVo.InsuSuryo & "', " _
            & "'" & beforeGousyaVo.MNounyuShijibi & "', " _
            & "'" & beforeGousyaVo.TNounyuShijibi & "', " _
            & "'" & LoginInfo.Now.UserId & "', " _
            & "'" & aDate.CurrentDateDbFormat & "', " _
            & "'" & aDate.CurrentTimeDbFormat & "', " _
            & "'" & LoginInfo.Now.UserId & "', " _
            & "'" & aDate.CurrentDateDbFormat & "', " _
            & "'" & aDate.CurrentTimeDbFormat & "' " _
            & " ) "

            Dim db As New EBomDbClient

            Dim param As New TShisakuTehaiTeiseiGousyaVo

            db.Insert(sql)
        End Sub

        ''' <summary>
        ''' 員数増の際の号車追加
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="Flag">フラグ</param>
        ''' <param name="GyouId">行ID</param>
        ''' <param name="shisakuGousyaHyoujiJun">号車表示順</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="Insu">員数</param>
        ''' <param name="mNounyuShijibi">メタル納入指示日</param>
        ''' <param name="tNounyuShijibi">トリム納入指示日</param>
        ''' <remarks></remarks>
        Sub InsertByTeiseiGousyaAdd(ByVal shisakuEventCode As String, _
                                    ByVal shisakuListCode As String, _
                                    ByVal shisakuListCodeKaiteiNo As String, _
                                    ByVal shisakuBukaCode As String, _
                                    ByVal shisakuBlockNo As String, _
                                    ByVal buhinNoHyoujiJun As Integer, _
                                    ByVal Flag As String, _
                                    ByVal GyouId As String, _
                                    ByVal shisakuGousyaHyoujiJun As String, _
                                    ByVal shisakuGousya As String, _
                                    ByVal Insu As Integer, _
                                    ByVal mNounyuShijibi As Integer, _
                                    ByVal tNounyuShijibi As Integer) Implements TeiseiTsuchiDao.InsertByTeiseiGousyaAdd

            Dim aDate As New ShisakuDate

            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_TEISEI_GOUSYA ( " _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_LIST_CODE, " _
            & " SHISAKU_LIST_CODE_KAITEI_NO, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
            & " BUHIN_NO_HYOUJI_JUN, " _
            & " FLAG, " _
            & " SORT_JUN, " _
            & " GYOU_ID, " _
            & " SHISAKU_GOUSYA_HYOUJI_JUN, " _
            & " SHISAKU_GOUSYA, " _
            & " INSU_SURYO, " _
            & " M_NOUNYU_SHIJIBI, " _
            & " T_NOUNYU_SHIJIBI, " _
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
            & "'" & buhinNoHyoujiJun & "', " _
            & "'" & Flag & "', " _
            & "'" & buhinNoHyoujiJun & "', " _
            & "'" & GyouId & "', " _
            & "'" & shisakuGousyaHyoujiJun & "', " _
            & "'" & shisakuGousya & "', " _
            & "'" & Insu & "', " _
            & "'" & mNounyuShijibi & "', " _
            & "'" & tNounyuShijibi & "', " _
            & "'" & LoginInfo.Now.UserId & "', " _
            & "'" & aDate.CurrentDateDbFormat & "', " _
            & "'" & aDate.CurrentTimeDbFormat & "', " _
            & "'" & LoginInfo.Now.UserId & "', " _
            & "'" & aDate.CurrentDateDbFormat & "', " _
            & "'" & aDate.CurrentTimeDbFormat & "' " _
            & " ) "

            Dim db As New EBomDbClient

            db.Insert(sql)

        End Sub


#End Region

#Region "削除する処理(DeleteBy)"

        ''' <summary>
        ''' 前回の訂正基本情報を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByTeiseiKihon(ByVal shisakuEventCode As String, _
                                       ByVal shisakuListCode As String, _
                                       ByVal shisakuListCodeKaiteiNo As String) Implements TeiseiTsuchiDao.DeleteByTeiseiKihon

            Dim sql As String = _
            " DELETE TTK " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_TEISEI_KIHON TTK " _
            & " WHERE " _
            & " TTK.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND TTK.SHISAKU_LIST_CODE = @ShisakuListCode " _
            & " AND TTK.SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo"

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiKihonVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo

            db.Delete(sql, param)

        End Sub

        ''' <summary>
        ''' 前回の訂正号車情報を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByTeiseiGousya(ByVal shisakuEventCode As String, _
                                        ByVal shisakuListCode As String, _
                                        ByVal shisakuListCodeKaiteiNo As String) Implements TeiseiTsuchiDao.DeleteByTeiseiGousya
            Dim sql As String = _
            " DELETE TTG " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_TEISEI_GOUSYA TTG " _
            & " WHERE " _
            & " TTG.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND TTG.SHISAKU_LIST_CODE = @ShisakuListCode " _
            & " AND TTG.SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo"

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiGousyaVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo

            db.Delete(sql, param)
        End Sub

#End Region

#Region "チェックする処理(CheckBy)"

        ''' <summary>
        ''' 手配記号の比較
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="buhinNoHyoujiJun">部品No表示順</param>
        ''' <returns>改訂前がF、改訂後がF以外ならTrue</returns>
        ''' <remarks></remarks>
        Public Function CheckByTehaiKigou(ByVal shisakuEventCode As String, _
                               ByVal shisakuListCode As String, _
                               ByVal shisakuBukaCode As String, _
                               ByVal shisakuBlockNo As String, _
                               ByVal buhinNoHyoujiJun As String) As Boolean Implements TeiseiTsuchiDao.CheckByTehaiKigou
            Dim sql As String = _
            "SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK " _
            & " WHERE " _
            & " TK.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND TK.SHISAKU_LIST_CODE = @ShisakuListCode " _
            & " AND TK.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND TK.BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
            & " AND TK.TEHAI_KIGOU = 'F' " _
            & " AND TK.SHISAKU_LIST_CODE_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON " _
            & " WHERE SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE  " _
            & " AND SHISAKU_BUKA_CODE = TK.SHISAKU_BUKA_CODE " _
            & " AND SHISAKU_BLOCK_NO = TK.SHISAKU_BLOCK_NO ) " _
            & " ORDER BY BUHIN_NO_HYOUJI_JUN "

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiKihonVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.BuhinNoHyoujiJun = buhinNoHyoujiJun
            Dim test As New TShisakuTehaiKihonVo
            test = db.QueryForObject(Of TShisakuTehaiKihonVo)(sql, param)

            If test Is Nothing Then
                Return False
            Else
                Return True
            End If

        End Function

        ''↓↓2015/01/12 メタル改訂抽出修正) (TES)劉 CHG BEGIN
        ''' <summary>
        ''' 直前の改訂と最新の改訂を比較して存在を確認する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="level">レベル</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>あればTrue,なければFalse</returns>
        ''' <remarks></remarks>
        Public Function CheckByBefore(ByVal shisakuEventCode As String, _
                               ByVal shisakuListCode As String, _
                               ByVal shisakuBukaCode As String, _
                               ByVal shisakuBlockNo As String, _
                               ByVal level As Integer, _
                               ByVal buhinNo As String, _
                               ByVal shisakuListCodeKaiteiNo As String) As Boolean Implements TeiseiTsuchiDao.CheckByBefore
            Dim sql As String = _
            "SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK " _
            & " WHERE " _
            & " TK.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND TK.SHISAKU_LIST_CODE = @ShisakuListCode " _
            & " AND TK.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND TK.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND TK.LEVEL = @Level " _
            & " AND TK.BUHIN_NO = @BuhinNo " _
            & " AND TK.SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo " _
            & " ORDER BY BUHIN_NO_HYOUJI_JUN "

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiKihonVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBukaCode = shisakuBukaCode

            param.Level = level
            param.BuhinNo = buhinNo

            param.ShisakuListCodeKaiteiNo = shisakuListCodeKaiteiNo
            Dim test As New TShisakuTehaiKihonVo
            test = db.QueryForObject(Of TShisakuTehaiKihonVo)(sql, param)

            If test Is Nothing Then
                Return False
            Else
                Return True
            End If
        End Function
        ''↑↑2015/01/12 メタル改訂抽出修正) (TES)劉 CHG END

#End Region

    End Class
End Namespace