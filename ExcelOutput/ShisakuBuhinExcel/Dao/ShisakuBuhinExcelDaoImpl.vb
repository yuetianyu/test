Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.Impl
Imports ExcelOutput.ShisakuBuhinExcel.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl

Namespace ShisakuBuhinExcel.Dao
    Public Class ShisakuBuhinExcelDaoImpl : Inherits DaoEachFeatureImpl
        Implements ShisakuBuhinExcelDao

        ''' <summary>
        ''' 試作部品表編集情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作部品表編集情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEdit(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String) As List(Of BuhinEditVoHelper) Implements ShisakuBuhinExcelDao.FindByBuhinEdit
            Dim sql As String = _
            " SELECT DISTINCT BE.*, BEI.INSU_SURYO, B.HYOJIJUN_NO, BEI.INSTL_HINBAN_HYOUJI_JUN " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE " _
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
            & " AND NOT SBI.INSU_SURYO IS NULL " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B " _
            & " ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & " AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA " _
            & " WHERE " _
            & " BE.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND BE.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT   " _
            & " WHERE SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
            & " AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ) "
            ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_aj) (TES)張 CHG BEGIN
            ' & " ORDER BY BE.BUHIN_NO_HYOUJI_JUN "
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(shisakuEventCode)

            If eventVo.BlockAlertKind = 2 Then
                sql = sql + " ORDER BY BE.BASE_BUHIN_FLG DESC,BE.BUHIN_NO_HYOUJI_JUN "
            Else
                sql = sql + " ORDER BY BE.BUHIN_NO_HYOUJI_JUN "
            End If
            ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_aj) (TES)張 CHG END
            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = shisakuBlockNo

            Return db.QueryForList(Of BuhinEditVoHelper)(sql, param)
        End Function


        ''↓↓2014/09/19 酒井 ADD BEGIN
        Public Function FindByBuhinEditBase(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String) As List(Of BuhinEditVoHelper) Implements ShisakuBuhinExcelDao.FindByBuhinEditBase
            Dim sql As String = _
            " SELECT DISTINCT BE.*, BEI.INSU_SURYO, B.HYOJIJUN_NO, BEI.INSTL_HINBAN_HYOUJI_JUN, SBI.BASE_INSTL_FLG " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE " _
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
            & " AND NOT SBI.INSU_SURYO IS NULL " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B " _
            & " ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & " AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA " _
            & " WHERE " _
            & " BE.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND BE.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT   " _
            & " WHERE SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
            & " AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ) " _
            & " AND SBI.BASE_INSTL_FLG = '1'" _
            & " ORDER BY BE.BUHIN_NO_HYOUJI_JUN "

            Dim db As New EBomDbClient
            Dim param As New BuhinEditVoHelper

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = shisakuBlockNo

            Return db.QueryForList(Of BuhinEditVoHelper)(sql, param)
        End Function
        ''↑↑2014/09/18 酒井 ADD END

        ''↓↓2014/09/18 酒井 ADD BEGIN
        Public Function FindByAllBuhinEdit(ByVal shisakuEventCode As String, ByVal blockNo As String) As List(Of BuhinEditVoHelper) Implements ShisakuBuhinExcelDao.FindByAllBuhinEdit
            Dim sql As String = _
                " SELECT DISTINCT BE.*, BEI.INSU_SURYO, B.HYOJIJUN_NO, BEI.INSTL_HINBAN_HYOUJI_JUN, SBI.BASE_INSTL_FLG  " _
                & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE " _
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
                & " AND NOT SBI.INSU_SURYO IS NULL " _
                & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B " _
                & " ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
                & " AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA " _
                & " WHERE " _
                & " BE.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & " AND BE.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
                & " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
                & " SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
                & " FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT   " _
                & " WHERE SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
                & " AND SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
                & " AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ) " _
                & " AND NOT SBI.BASE_INSTL_FLG = '1' "
            sql = sql + " ORDER BY BE.SHISAKU_BLOCK_NO,BE.BUHIN_NO_HYOUJI_JUN "


            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = blockNo

            Return db.QueryForList(Of BuhinEditVoHelper)(sql, param)
        End Function
        ''↑↑2014/09/18 酒井 ADD END

        ''' <summary>
        ''' 試作部品表編集INSTL情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作部品表編集INSTL情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditInstl(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String) As List(Of BuhinEditInstlVoHelper) Implements ShisakuBuhinExcelDao.FindByBuhinEditInstl
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
            & " LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
            & " ON SB.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
            & " AND SB.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
            & " AND SB.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO " _
            & " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND SB.BLOCK_FUYOU = '0' " _
            & " LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            & " ON SBI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE " _
            & " AND SBI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE " _
            & " AND SBI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO " _
            & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN " _
            & " AND NOT SBI.INSU_SURYO IS NULL " _
            & " WHERE " _
            & " BEI.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND BEI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL  " _
            & " WHERE SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
            & " AND SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ) " _
            & " ORDER BY BEI.INSTL_HINBAN_HYOUJI_JUN, BEI.BUHIN_NO_HYOUJI_JUN "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditInstlVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = shisakuBlockNo

            Return db.QueryForList(Of BuhinEditInstlVoHelper)(sql, param)

        End Function

        ''' <summary>
        ''' 試作設計ブロックINSTL情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作設計ブロックINSTL情報</returns>
        ''' <remarks></remarks>
        Public Function FindBySekkeiBlockInstl(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockInstlVo) Implements ShisakuBuhinExcelDao.FindBySekkeiBlockInstl
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            & " LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
            & " ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & " AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            & " AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO " _
            & " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND SB.BLOCK_FUYOU = '0' " _
            & " WHERE " _
            & " SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL  " _
            & " WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            & " AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) " _
            & " AND NOT INSU_SURYO IS NULL " _
            & " ORDER BY SBI.SHISAKU_BLOCK_NO, SBI.INSTL_HINBAN_HYOUJI_JUN "

            Dim db As New EBomDbClient
            Dim param As New TShisakuSekkeiBlockInstlVo

            param.ShisakuEventCode = shisakuEventCode

            Return db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sql, param)
        End Function

        ''' <summary>
        ''' 試作イベント情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作イベント情報</returns>
        ''' <remarks></remarks>
        Public Function FindByEvent(ByVal shisakuEventCode As String) As TShisakuEventVo Implements ShisakuBuhinExcelDao.FindByEvent
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventVo

            param.ShisakuEventCode = shisakuEventCode

            Return db.QueryForObject(Of TShisakuEventVo)(sql, param)
        End Function

        ''' <summary>
        ''' 試作完成車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作イベント完成車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByKansei(ByVal shisakuEventCode As String) As List(Of TShisakuEventBaseVo) Implements ShisakuBuhinExcelDao.FindByBase
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventBaseVo

            param.ShisakuEventCode = shisakuEventCode

            Return db.QueryForList(Of TShisakuEventBaseVo)(sql, param)
        End Function

        ''' <summary>
        ''' 合計員数数量を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>合計員数数量</returns>
        ''' <remarks></remarks>
        Public Function GetTotalInsuSuryo(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                            ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, _
                            ByVal buhinNoHyoujiJun As Integer) As Integer Implements ShisakuBuhinExcelDao.GetTotalInsuSuryo
            Dim sql As String = _
            " SELECT SUM(BEI.INSU_SURYO) AS INSU_SURYO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
            & " LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE " _
            & " ON BE.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
            & " AND BE.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
            & " AND BE.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO " _
            & " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN " _
            & " WHERE " _
            & " BEI.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND BEI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND BEI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
            & " AND BEI.BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditInstlVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = buhinNoHyoujiJun

            Return db.QueryForObject(Of TShisakuBuhinEditInstlVo)(sql, param).InsuSuryo
        End Function

        ''' <summary>
        ''' 号車と関わる部品の員数を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>員数数量</returns>
        ''' <remarks></remarks>
        Public Function GetGousyaBuhinInsuSuryo(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                            ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, _
                            ByVal buhinNoHyoujiJun As Integer, ByVal shisakuGousya As String) As List(Of TShisakuBuhinEditInstlVo) Implements ShisakuBuhinExcelDao.GetGousyaBuhinInsuSuryo
            Dim sql As String = _
            " SELECT BEI.INSU_SURYO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            & " ON SBI.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
            & " AND SBI.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
            & " AND SBI.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO " _
            & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN " _
            & " AND SBI.SHISAKU_GOUSYA =  " _
            & "'" & shisakuGousya & "'" _
            & " WHERE " _
            & " BEI.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND BEI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND BEI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
            & " AND BEI.BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditInstlVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = buhinNoHyoujiJun


            Return db.QueryForList(Of TShisakuBuhinEditInstlVo)(sql, param)
        End Function


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
                                      ByVal shisakuBlockNoKaiteiNo As String) As String Implements ShisakuBuhinExcelDao.FindByShisakuBlockNo
            Dim sql As String = _
            " SELECT UNIT_KBN " _
            & "    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK " _
            & " WHERE " _
            & "    SHISAKU_EVENT_CODE =  '" & shisakuEventCode & "' " _
            & " AND SHISAKU_BUKA_CODE =  '" & shisakuBukaCode & "' " _
            & " AND SHISAKU_BLOCK_NO  =  '" & shisakuBlockNo & "' " _
            & " AND SHISAKU_BLOCK_NO_KAITEI_NO =  '" & shisakuBlockNoKaiteiNo & "' " _
            & " GROUP BY UNIT_KBN "

            Dim db As New EBomDbClient
            Dim vo As New TShisakuSekkeiBlockVo
            vo = db.QueryForObject(Of TShisakuSekkeiBlockVo)(sql)
            Dim result As String = ""
            If vo Is Nothing Then
                Return result
            Else
                Return vo.UnitKbn
            End If
        End Function
        Public Function FindByShisakuBlockNoForUnitKbn() As Hashtable Implements ShisakuBuhinExcelDao.FindByShisakuBlockNoForUnitKbn
            Dim ht As New Hashtable

            Dim sql As String = _
            " SELECT SHISAKU_EVENT_CODE,SHISAKU_BUKA_CODE,SHISAKU_BLOCK_NO,SHISAKU_BLOCK_NO_KAITEI_NO,UNIT_KBN " _
            & "    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK "

            Dim db As New EBomDbClient

            Dim vos As List(Of TShisakuSekkeiBlockVo) = db.QueryForList(Of TShisakuSekkeiBlockVo)(sql)

            For Each vo As TShisakuSekkeiBlockVo In vos
                ht.Add(vo.ShisakuEventCode & vo.ShisakuBukaCode & vo.ShisakuBlockNo & vo.ShisakuBlockNoKaiteiNo, vo.UnitKbn)
            Next
            Return ht
        End Function

        Function FindByBlockGroup(ByVal shisakuEventCode As String) As List(Of TShisakuBuhinEditVo) Implements ShisakuBuhinExcelDao.FindByBlockGroup
            Dim sql As String = _
                " SELECT " _
                & " BE.SHISAKU_BLOCK_NO " _
                & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) " _
                & " WHERE " _
                & " BE.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
                & " SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS " _
                & " SHISAKU_BLOCK_NO_KAITEI_NO " _
                & " FROM " _
                & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) " _
                & " WHERE " _
                & " SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
                & " AND SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ) " _
                & " GROUP BY BE.SHISAKU_BLOCK_NO " _
                & " ORDER BY BE.SHISAKU_BLOCK_NO "


            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditVo

            param.ShisakuEventCode = shisakuEventCode

            Return db.QueryForList(Of TShisakuBuhinEditVo)(sql, param)
        End Function


    End Class
End Namespace