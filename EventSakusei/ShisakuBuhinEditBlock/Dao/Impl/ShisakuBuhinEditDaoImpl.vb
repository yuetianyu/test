Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports System.Text
Imports EventSakusei.ShisakuBuhinEdit.Al.Logic
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl

Public Class ShisakuBuhinEditDaoImpl : Inherits DaoEachFeatureImpl
    Implements IShisakuBuhinEditDao
    Private ReadOnly subject As BuhinEditAlSubject
    Private Function SELECT_NEWEST_KOSEI_BY_BUHIN_NO_OYA() As String
        Dim sql As String
        sql = "WITH RT (BUHIN_NO_OYA, BUHIN_NO_KO, KAITEI_NO, LV) AS ( " _
            & "	SELECT P.BUHIN_NO_OYA, P.BUHIN_NO_KO, P.KAITEI_NO, 0 LV " _
            & "	FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_KOUSEI P WITH (NOLOCK, NOWAIT) " _
            & "	WHERE P.BUHIN_NO_OYA = @Value " _
            & "	    AND P.HAISI_DATE = 99999999 AND (P.SHUKEI_CODE <> ' ' OR P.SIA_SHUKEI_CODE <> ' ')  " _
            & "	UNION ALL " _
            & "	SELECT C.BUHIN_NO_OYA, C.BUHIN_NO_KO, C.KAITEI_NO, P.LV + 1 " _
            & "	FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_KOUSEI C WITH (NOLOCK, NOWAIT) INNER JOIN RT P " _
            & "	    ON C.BUHIN_NO_OYA = P.BUHIN_NO_KO " _
            & "	WHERE C.HAISI_DATE = 99999999 AND (C.SHUKEI_CODE <> ' ' OR C.SIA_SHUKEI_CODE <> ' ')  " _
            & ") "

        Return sql

    End Function
    Private Function JOIN_TABLE() As String
        Dim sql As String
        sql = "   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BUHIN_EDIT	WITH (NOLOCK, NOWAIT)  " _
        & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN BUHIN " _
        & " 	ON BUHIN_EDIT.SHISAKU_EVENT_CODE = BUHIN.SHISAKU_EVENT_CODE " _
        & " 	AND BUHIN_EDIT.SHISAKU_BUKA_CODE = BUHIN.SHISAKU_BUKA_CODE " _
        & " 	AND BUHIN_EDIT.SHISAKU_BLOCK_NO = BUHIN.SHISAKU_BLOCK_NO " _
        & " 	AND BUHIN_EDIT.SHISAKU_BLOCK_NO_KAITEI_NO = BUHIN.SHISAKU_BLOCK_NO_KAITEI_NO " _
        & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_KOUSEI BUHIN_KOUSEI " _
        & " 	ON BUHIN.BUHIN_NO = BUHIN_KOUSEI.BUHIN_NO_OYA " _
        & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL BLOCK_INSTL " _
        & " 	ON BLOCK_INSTL.SHISAKU_EVENT_CODE = BUHIN.SHISAKU_EVENT_CODE " _
        & " 	AND BLOCK_INSTL.SHISAKU_BUKA_CODE = BUHIN.SHISAKU_BUKA_CODE " _
        & " 	AND BLOCK_INSTL.SHISAKU_BLOCK_NO = BUHIN.SHISAKU_BLOCK_NO " _
        & " 	AND BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO = BUHIN.SHISAKU_BLOCK_NO_KAITEI_NO " _
        & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK BLOCK " _
        & " 	ON BLOCK.SHISAKU_EVENT_CODE = BLOCK_INSTL.SHISAKU_EVENT_CODE " _
        & " 	AND BLOCK.SHISAKU_BUKA_CODE = BLOCK_INSTL.SHISAKU_BUKA_CODE " _
        & " 	AND BLOCK.SHISAKU_BLOCK_NO = BLOCK_INSTL.SHISAKU_BLOCK_NO " _
        & " 	AND BLOCK.SHISAKU_BLOCK_NO_KAITEI_NO = BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO "

        Return sql

    End Function

    ''' <summary>
    ''' 部品編集情報と部品編集INSTL情報を取得する
    ''' </summary>
    ''' <param name="shisakuEventCode">イベントコード</param>
    ''' <param name="shisakuBukaCode">部課コード</param>
    ''' <param name="shisakuBlockNo">ブロックNo</param>
    ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FindByBuhinEditAndInstl(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, _
                                            ByVal shisakuBlockNoKaiteiNo As String) As System.Collections.Generic.List(Of ShisakuBuhinEditBlock.Dao.TShisakuBuhinEditVoHelper) Implements ShisakuBuhinEditBlock.Dao.IShisakuBuhinEditDao.FindByBuhinEditAndInstl
        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(5)) INSTL_DATA_KBN (TES)張 ADD BEGIN
        Dim sql As String = _
        " SELECT DISTINCT BE.*, BEI.INSU_SURYO, BEI.INSTL_HINBAN_HYOUJI_JUN, SBI.INSTL_HINBAN, SBI.INSTL_HINBAN_KBN,SBI.INSTL_DATA_KBN,SBI.BASE_INSTL_FLG " _
        & " FROM " _
        & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) " _
        & " LEFT JOIN " _
        & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
        & " ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
        & " AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
        & " AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO " _
        & " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO " _
        & " AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN " _
        & " LEFT JOIN " _
        & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
        & " ON SB.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
        & " AND SB.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
        & " AND SB.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO " _
        & " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO " _
        & " AND SB.BLOCK_FUYOU = @BlockFuyou " _
        & " LEFT JOIN " _
        & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
        & " ON SBI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE " _
        & " AND SBI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE " _
        & " AND SBI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO " _
        & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO " _
        & " AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN " _
        & " WHERE " _
        & " BE.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
        & " AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
        & " AND BE.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
        & " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
        & " ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN "
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(5)) (TES)張 ADD END
        Dim param = New EventSakusei.ShisakuBuhinEditBlock.Dao.TShisakuBuhinEditVoHelper
        param.ShisakuEventCode = shisakuEventCode
        param.ShisakuBukaCode = shisakuBukaCode
        param.ShisakuBlockNo = shisakuBlockNo
        param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
        param.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY
        Dim db As New EBomDbClient
        Return db.QueryForList(Of EventSakusei.ShisakuBuhinEditBlock.Dao.TShisakuBuhinEditVoHelper)(sql, param)
    End Function

    ''' <summary>
    ''' 部品編集ベース情報と部品編集INSTLベース情報を取得する
    ''' </summary>
    ''' <param name="shisakuEventCode">イベントコード</param>
    ''' <param name="shisakuBukaCode">部課コード</param>
    ''' <param name="shisakuBlockNo">ブロックNo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FindByBuhinEditAndInstlBase(ByVal shisakuEventCode As String, _
                                                ByVal shisakuBukaCode As String, _
                                                ByVal shisakuBlockNo As String) As List(Of ShisakuBuhinEditBlock.Dao.TShisakuBuhinEditVoHelper) Implements IShisakuBuhinEditDao.FindByBuhinEditAndInstlBase
        Dim sb As New StringBuilder
        With sb
            .Remove(0, .Length)
            ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(6)) INSTL_DATA_KBN (TES)張 ADD BEGIN
            .AppendLine("SELECT DISTINCT BE.*, ")
            .AppendLine(" BEI.INSU_SURYO,")
            .AppendLine(" BEI.INSTL_HINBAN_HYOUJI_JUN,")
            .AppendLine(" SBI.INSTL_HINBAN,")
            .AppendLine(" SBI.INSTL_HINBAN_KBN,")
            .AppendLine(" SBI.INSTL_DATA_KBN,")
            .AppendLine(" SBI.BASE_INSTL_FLG")
            .AppendLine(" ")
            ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(6)) (TES)張 ADD END
            .AppendLine(" FROM ")
            .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE BE WITH (NOLOCK, NOWAIT) ")
            .AppendLine(" LEFT JOIN ")
            .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE BEI ")
            .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
            .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
            .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
            .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
            .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
            .AppendLine(" LEFT JOIN ")
            .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
            .AppendLine(" ON SB.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
            .AppendLine(" AND SB.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
            .AppendLine(" AND SB.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
            .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
            .AppendLine(" AND SB.BLOCK_FUYOU = @BlockFuyou ")
            .AppendLine(" LEFT JOIN ")
            .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ")
            .AppendLine(" ON SBI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
            .AppendLine(" AND SBI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
            .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
            .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
            .AppendLine(" AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN ")
            .AppendLine(" WHERE ")
            .AppendLine(" BE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
            .AppendLine(" AND BE.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
            .AppendLine(" AND BE.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
            .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = '000' ")
            .AppendLine(" ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")
        End With

        Dim param = New EventSakusei.ShisakuBuhinEditBlock.Dao.TShisakuBuhinEditVoHelper
        param.ShisakuEventCode = shisakuEventCode
        param.ShisakuBukaCode = shisakuBukaCode
        param.ShisakuBlockNo = shisakuBlockNo
        param.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY
        Dim db As New EBomDbClient
        Return db.QueryForList(Of EventSakusei.ShisakuBuhinEditBlock.Dao.TShisakuBuhinEditVoHelper)(sb.ToString, param)
    End Function

    Private Function FindByBuhinEditInstl(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                                          ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) _
                                          As List(Of TShisakuSekkeiBlockInstlVo) Implements ShisakuBuhinEditBlock.Dao.IShisakuBuhinEditDao.FindByBuhinEditInstl
        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_w) (TES)張 CHG BEGIN
        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(3)) INSTL_DATA_KBN (TES)張 ADD BEGIN
        Dim sql As String = _
        " SELECT " _
        & " SBI.BASE_INSTL_FLG, " _
        & " SBI.INSTL_HINBAN, " _
        & " SBI.INSTL_HINBAN_KBN, " _
        & " SBI.INSTL_DATA_KBN, " _
        & " SBI.INSTL_HINBAN_HYOUJI_JUN " _
        & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) " _
        & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
        & " ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
        & " AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
        & " AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO " _
        & " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO " _
        & " WHERE " _
        & " SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
        & " AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
        & " AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
        & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
        & " GROUP BY SBI.BASE_INSTL_FLG, SBI.INSTL_DATA_KBN, SBI.INSTL_HINBAN, SBI.INSTL_HINBAN_KBN, SBI.INSTL_HINBAN_HYOUJI_JUN "
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(3)) (TES)張 ADD END
        '該当イベント取得
        Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
        Dim eventVo As TShisakuEventVo
        eventVo = eventDao.FindByPk(shisakuEventCode)
        '該当イベントが「移管車改修」の場合
        'If eventVo.BlockAlertKind = 2 Then
        sql = sql + " ORDER BY SBI.INSTL_HINBAN_HYOUJI_JUN "
        'End If
        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_w) (TES)張 CHG END
        Dim param = New EventSakusei.ShisakuBuhinEditBlock.Dao.TShisakuBuhinEditVoHelper
        param.ShisakuEventCode = shisakuEventCode
        param.ShisakuBukaCode = shisakuBukaCode
        param.ShisakuBlockNo = shisakuBlockNo
        param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
        'param.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY

        Dim db As New EBomDbClient
        Return db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sql, param)
    End Function

    ''' <summary>
    ''' 部品編集INSTLベース情報を取得する
    ''' </summary>
    ''' <param name="shisakuEventCode">イベントコード</param>
    ''' <param name="shisakuBukaCode">部課コード</param>
    ''' <param name="shisakuBlockNo">ブロックNo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FindByBuhinEditInstlBase(ByVal shisakuEventCode As String, _
                                             ByVal shisakuBukaCode As String, _
                                             ByVal shisakuBlockNo As String) As List(Of TShisakuSekkeiBlockInstlVo) Implements IShisakuBuhinEditDao.FindByBuhinEditInstlBase
        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_x) (TES)張 CHG BEGIN
        Dim sb As New StringBuilder
        With sb

            .Remove(0, .Length)
            .AppendLine(" SELECT ")
            .AppendLine(" SBI.BASE_INSTL_FLG, ")
            .AppendLine(" SBI.INSTL_HINBAN, ")
            .AppendLine(" SBI.INSTL_HINBAN_KBN, ")
            ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(4)) INSTL_DATA_KBN (TES)張 ADD BEGIN
            .AppendLine(" SBI.INSTL_DATA_KBN, ")
            ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(4)) (TES)張 ADD END
            .AppendLine(" SBI.INSTL_HINBAN_HYOUJI_JUN ")
            .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) ")
            .AppendLine(" WHERE ")
            .AppendLine(" SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
            .AppendLine(" AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
            .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
            .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
            .AppendLine(" GROUP BY SBI.BASE_INSTL_FLG, SBI.INSTL_DATA_KBN, SBI.INSTL_HINBAN, SBI.INSTL_HINBAN_KBN, SBI.INSTL_HINBAN_HYOUJI_JUN ")
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(shisakuEventCode)
            If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                .AppendLine(" ORDER BY SBI.INSTL_HINBAN_HYOUJI_JUN ")
            End If
        End With
        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_x) (TES)張 CHG END
        Dim param = New EventSakusei.ShisakuBuhinEditBlock.Dao.TShisakuBuhinEditVoHelper
        param.ShisakuEventCode = shisakuEventCode
        param.ShisakuBukaCode = shisakuBukaCode
        param.ShisakuBlockNo = shisakuBlockNo

        Dim db As New EBomDbClient
        Return db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sb.ToString, param)
    End Function

    ''' <summary>
    ''' 部品編集情報を取得する
    ''' </summary>
    ''' <param name="shisakuEventCode">試作イベントコード</param>
    ''' <param name="shisakuBukaCode">試作部課コード</param>
    ''' <param name="shisakuBLockNo">試作ブロックNo</param>
    ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
    ''' <param name="isBase">ベースか？</param>
    ''' <returns>部品編集情報</returns>
    ''' <remarks></remarks>
    Public Function FindByBuhinEdit(ByVal shisakuEventCode As String, _
                             ByVal shisakuBukaCode As String, _
                             ByVal shisakuBLockNo As String, _
                             ByVal shisakuBlockNoKaiteiNo As String, _
                             ByVal isBase As Boolean) As List(Of TShisakuBuhinEditVo) Implements IShisakuBuhinEditDao.FindByBuhinEdit

        Dim sql As New StringBuilder

        With sql
            .Remove(0, sql.Length)
            .AppendLine(" SELECT * ")
            If isBase Then
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE")
            Else
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT")
            End If

            .AppendLine(" WHERE ")
            .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode ")
            .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
            .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
            .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
        End With

        Dim param = New TShisakuBuhinEditVo
        param.ShisakuEventCode = shisakuEventCode
        param.ShisakuBukaCode = shisakuBukaCode
        param.ShisakuBlockNo = shisakuBLockNo
        param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
        If isBase Then
            param.ShisakuBlockNoKaiteiNo = "000"
        End If


        Dim db As New EBomDbClient
        Return db.QueryForList(Of TShisakuBuhinEditVo)(sql.ToString, param)

    End Function

    ''↓↓2014/08/11 2集計コード R/Yのブロック間紐付け b) (TES)施 ADD BEGIN
    ''' <summary>
    ''' EBOM設計展開情報から、国内集計コードRまたは海外集計コードRの部品を取得
    ''' </summary>
    ''' <param name="shisakuEventCode"></param>
    ''' <returns></returns>
    ''' <remarks>  ''2014/08/11 2集計コード R/Yのブロック間紐付け b) (TES)施 ADD </remarks>
    Function FindCodeRBuhinByEventCode(ByVal shisakuEventCode As String) As List(Of TShisakuBuhinEditVo) Implements IShisakuBuhinEditDao.FindCodeRBuhinByEventCode
        ''↓↓2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD BEGIN
        'Dim sql As String = " SELECT * FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT " & _
        '" WHERE SHUKEI_CODE ='R' AND SIA_SHUKEI_CODE ='R' AND SHISAKU_EVENT_CODE = @ShisakuEventCode" & _
        '" AND SHISAKU_BLOCK_NO_KAITEI_NO=(SELECT MAX (SHISAKU_BLOCK_NO_KAITEI_NO) FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT  " & _
        ' " WHERE SHUKEI_CODE ='R' AND SIA_SHUKEI_CODE ='R' AND SHISAKU_EVENT_CODE = @ShisakuEventCode)"
        ''↓↓2014/09/17 2集計コード R/Yのブロック間紐付け b) 酒井 ADD BEGIN
        'Dim sql As String = " SELECT * FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT " & _
        '" WHERE (SHUKEI_CODE ='R' OR SIA_SHUKEI_CODE ='R') AND SHISAKU_EVENT_CODE = @ShisakuEventCode" & _
        '" AND SHISAKU_BLOCK_NO_KAITEI_NO=(SELECT MAX (SHISAKU_BLOCK_NO_KAITEI_NO) FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT  " & _
        ' " WHERE (SHUKEI_CODE ='R' OR SIA_SHUKEI_CODE ='R') AND SHISAKU_EVENT_CODE = @ShisakuEventCode)"
        Dim sql As String = " SELECT BE.* FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE" & _
        " INNER JOIN  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI" & _
        " ON BEI.SHISAKU_EVENT_CODE=BE.SHISAKU_EVENT_CODE " & _
        " AND BEI.SHISAKU_BUKA_CODE=BE.SHISAKU_BUKA_CODE " & _
        " AND BEI.SHISAKU_BLOCK_NO=BE.SHISAKU_BLOCK_NO " & _
        " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO=BE.SHISAKU_BLOCK_NO_KAITEI_NO " & _
        " AND BEI.BUHIN_NO_HYOUJI_JUN=BE.BUHIN_NO_HYOUJI_JUN " & _
        " INNER JOIN  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI" & _
        " ON SBI.SHISAKU_EVENT_CODE=BEI.SHISAKU_EVENT_CODE " & _
        " AND SBI.SHISAKU_BUKA_CODE=BEI.SHISAKU_BUKA_CODE " & _
        " AND SBI.SHISAKU_BLOCK_NO=BEI.SHISAKU_BLOCK_NO " & _
        " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO=BEI.SHISAKU_BLOCK_NO_KAITEI_NO " & _
        " AND SBI.INSTL_HINBAN_HYOUJI_JUN=BEI.INSTL_HINBAN_HYOUJI_JUN " & _
        " WHERE (BE.SHUKEI_CODE ='R' OR BE.SIA_SHUKEI_CODE ='R') AND BE.SHISAKU_EVENT_CODE = @ShisakuEventCode" & _
        " AND SBI.INSTL_DATA_KBN='0' " & _
        " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO=(SELECT MAX (SHISAKU_BLOCK_NO_KAITEI_NO) FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT  " & _
         " WHERE (SHUKEI_CODE ='R' OR SIA_SHUKEI_CODE ='R') AND SHISAKU_EVENT_CODE = @ShisakuEventCode)"
        ''↑↑2014/09/17 2集計コード R/Yのブロック間紐付け b) 酒井 ADD END
        ''↑↑2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD END
        'WHERE句SHISAKU_BLOCK_NO_KAITEI_NOの不要の可能性。但し、設計展開直後は改訂No=000なので影響なし。
        ''↑↑2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD END

        Dim param = New TShisakuBuhinEditVo
        param.ShisakuEventCode = shisakuEventCode
        Dim db As New EBomDbClient
        Return db.QueryForList(Of TShisakuBuhinEditVo)(sql.ToString, param)
    End Function


    ''' <summary>
    '''1)のLEVELが2以上の場合、T_SHISAKU_BUHIN_EDITからLEVELが直上となる、直近の部品を親部品番号とする	
    ''' </summary>
    ''' <param name="shisakuEventCode">試作イベントコード </param>
    ''' <param name="ShisakuBukaCode">試作部課コード </param>
    ''' <param name="ShisakuBlockNo">試作ブロック№</param>
    ''' <param name="ShisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
    ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
    ''' <returns></returns>
    ''' <remarks>  ''2014/08/11 2集計コード R/Yのブロック間紐付け b) (TES)施 ADD </remarks>
    ''' 
    Function FindOyaBuhinByHighLevel(ByVal shisakuEventCode As String, _
                                       ByVal ShisakuBukaCode As String, _
                                       ByVal ShisakuBlockNo As String, _
                                       ByVal ShisakuBlockNoKaiteiNo As String, _
                                       ByVal BuhinNoHyoujiJun As Int32?, _
                                       ByVal Level As Integer) As List(Of TShisakuBuhinEditVo) Implements IShisakuBuhinEditDao.FindOyaBuhinByHighLevel
        ''↓↓2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD BEGIN
        '    Function FindOyaBuhinByHighLevel(ByVal shisakuEventCode As String, _
        '                                       ByVal ShisakuBukaCode As String, _
        '                                      ByVal ShisakuBlockNo As String, _
        '                                     ByVal ShisakuBlockNoKaiteiNo As String, _
        '                                    ByVal BuhinNoHyoujiJun As Int32?) As List(Of TShisakuBuhinEditVo) Implements IShisakuBuhinEditDao.FindOyaBuhinByHighLevel

        ''↑↑2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD END
        Dim sql As New StringBuilder
        With sql
            .Remove(0, sql.Length)
            ''↓↓2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD BEGIN
            '.AppendLine("SELECT * FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT B WHERE B.LEVEL=")
            .AppendLine("SELECT * FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT B WHERE B.BUHIN_NO_HYOUJI_JUN=")
            '.AppendLine(" (SELECT A.LEVEL ")
            .AppendLine(" (SELECT MAX(A.BUHIN_NO_HYOUJI_JUN) ")
            ''↑↑2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD END

            .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT A")
            '.AppendLine(" WHERE  A.SHISAKU_EVENT_CODE =B.SHISAKU_EVENT_CODE ")
            '.AppendLine(" AND A.SHISAKU_BLOCK_NO_KAITEI_NO =B.SHISAKU_BLOCK_NO_KAITEI_NO  ")
            '.AppendLine(" AND A.SHISAKU_BUKA_CODE=B.SHISAKU_BUKA_CODE ")
            '.AppendLine(" AND A.BUHIN_NO_HYOUJI_JUN=B.BUHIN_NO_HYOUJI_JUN ")
            .AppendLine(" WHERE ")
            .AppendLine(" A.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
            .AppendLine(" AND A.SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
            .AppendLine(" AND A.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
            .AppendLine(" AND A.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
            ''↓↓2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD BEGIN
            '.AppendLine(" AND A.BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun )-1")
            .AppendLine(" AND A.BUHIN_NO_HYOUJI_JUN < @BuhinNoHyoujiJun ")
            .AppendLine(" AND A.LEVEL < @Level )")

            ''↑↑2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD END
            .AppendLine(" AND B.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
            .AppendLine(" AND B.SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
            .AppendLine(" AND B.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
            .AppendLine(" AND B.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
        End With

        ''↓↓2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD BEGIN
        'Dim param = New TShisakuBuhinEditInstlVo
        Dim param = New TShisakuBuhinEditVo
        ''↑↑2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD END
        param.ShisakuEventCode = shisakuEventCode
        param.ShisakuBukaCode = ShisakuBukaCode
        param.ShisakuBlockNo = ShisakuBlockNo
        param.ShisakuBlockNoKaiteiNo = ShisakuBlockNoKaiteiNo
        param.BuhinNoHyoujiJun = BuhinNoHyoujiJun
        ''↓↓2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD BEGIN
        param.Level = Level
        ''↑↑2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD END
        Dim db As New EBomDbClient
        Return db.QueryForList(Of TShisakuBuhinEditVo)(sql.ToString, param)
    End Function
    ''' <summary>
    '''1)のLEVELが1の場合、T_SHISAKU_BUHIN_EDIT_INSTLから員数ゼロでないINSTL品番を親部品番号とする
    ''' </summary>
    ''' <param name="shisakuEventCode">試作イベントコード </param>
    ''' <param name="ShisakuBukaCode">試作部課コード </param>
    ''' <param name="ShisakuBlockNo">試作ブロック№</param>
    ''' <param name="ShisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
    ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
    ''' <returns></returns>
    ''' <remarks>  ''2014/08/11 2集計コード R/Yのブロック間紐付け b) (TES)施 ADD </remarks>
    Function FindOyaBuhinByLevelOne(ByVal shisakuEventCode As String, _
                                       ByVal ShisakuBukaCode As String, _
                                       ByVal ShisakuBlockNo As String, _
                                       ByVal ShisakuBlockNoKaiteiNo As String, _
                                       ByVal BuhinNoHyoujiJun As Int32?) As List(Of TShisakuBuhinEditVo) Implements IShisakuBuhinEditDao.FindOyaBuhinByLevelOne
        Dim sql As New StringBuilder
        With sql
            .Remove(0, sql.Length)
            ''↓↓2014/09/17 2集計コード R/Yのブロック間紐付け b) 酒井 ADD BEGIN
            '.AppendLine(" SELECT B.* ")
            '.AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL A")
            '.AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT B on A.SHISAKU_BLOCK_NO =B.SHISAKU_BLOCK_NO")
            '.AppendLine(" AND A.SHISAKU_EVENT_CODE =B.SHISAKU_EVENT_CODE ")
            '.AppendLine(" AND A.SHISAKU_BLOCK_NO_KAITEI_NO =B.SHISAKU_BLOCK_NO_KAITEI_NO  ")
            '.AppendLine(" AND A.SHISAKU_BUKA_CODE=B.SHISAKU_BUKA_CODE ")
            '.AppendLine(" AND A.BUHIN_NO_HYOUJI_JUN=B.BUHIN_NO_HYOUJI_JUN ")
            '.AppendLine(" WHERE ")
            '.AppendLine(" A.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
            '.AppendLine(" AND A.SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
            '.AppendLine(" AND A.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
            '.AppendLine(" AND A.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
            '.AppendLine(" AND A.BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
            '.AppendLine(" AND A.INSU_SURYO <>0 ")
            .AppendLine(" SELECT  T_SHISAKU_BUHIN_EDIT.* ")
            .AppendLine(" FROM   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL  ")
            .AppendLine(" INNER JOIN  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ON   ")
            .AppendLine(" T_SHISAKU_BUHIN_EDIT_INSTL.SHISAKU_EVENT_CODE = T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_EVENT_CODE And ")
            .AppendLine(" T_SHISAKU_BUHIN_EDIT_INSTL.SHISAKU_BUKA_CODE = T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_BUKA_CODE And ")
            .AppendLine(" T_SHISAKU_BUHIN_EDIT_INSTL.SHISAKU_BLOCK_NO = T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_BLOCK_NO And ")
            .AppendLine(" T_SHISAKU_BUHIN_EDIT_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO = T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO And ")
            .AppendLine(" T_SHISAKU_BUHIN_EDIT_INSTL.INSTL_HINBAN_HYOUJI_JUN = T_SHISAKU_SEKKEI_BLOCK_INSTL.INSTL_HINBAN_HYOUJI_JUN  ")
            .AppendLine(" INNER JOIN  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT ON  ")
            .AppendLine(" T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_EVENT_CODE = T_SHISAKU_BUHIN_EDIT.SHISAKU_EVENT_CODE And ")
            .AppendLine(" T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_BUKA_CODE = T_SHISAKU_BUHIN_EDIT.SHISAKU_BUKA_CODE And ")
            .AppendLine(" T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_BLOCK_NO = T_SHISAKU_BUHIN_EDIT.SHISAKU_BLOCK_NO And ")
            .AppendLine(" T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO = T_SHISAKU_BUHIN_EDIT.SHISAKU_BLOCK_NO_KAITEI_NO And ")
            .AppendLine(" T_SHISAKU_SEKKEI_BLOCK_INSTL.INSTL_HINBAN = T_SHISAKU_BUHIN_EDIT.BUHIN_NO And ")
            .AppendLine(" T_SHISAKU_SEKKEI_BLOCK_INSTL.INSTL_HINBAN_KBN = T_SHISAKU_BUHIN_EDIT.BUHIN_NO_KBN ")
            .AppendLine(" WHERE (T_SHISAKU_BUHIN_EDIT_INSTL.SHISAKU_EVENT_CODE = @ShisakuEventCode) AND  ")
            .AppendLine(" (T_SHISAKU_BUHIN_EDIT_INSTL.SHISAKU_BUKA_CODE = @ShisakuBukaCode) AND  ")
            .AppendLine(" (T_SHISAKU_BUHIN_EDIT_INSTL.SHISAKU_BLOCK_NO = @ShisakuBlockNo) AND  ")
            .AppendLine(" (T_SHISAKU_BUHIN_EDIT_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo) AND  ")
            .AppendLine(" (T_SHISAKU_BUHIN_EDIT_INSTL.BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun) AND (T_SHISAKU_BUHIN_EDIT_INSTL.INSU_SURYO <> 0) ")
            ''↑↑2014/09/17 2集計コード R/Yのブロック間紐付け b) 酒井 ADD END
        End With

        Dim param = New TShisakuBuhinEditInstlVo
        param.ShisakuEventCode = shisakuEventCode
        param.ShisakuBukaCode = ShisakuBukaCode
        param.ShisakuBlockNo = ShisakuBlockNo
        param.ShisakuBlockNoKaiteiNo = ShisakuBlockNoKaiteiNo
        param.BuhinNoHyoujiJun = BuhinNoHyoujiJun
        Dim db As New EBomDbClient
        Return db.QueryForList(Of TShisakuBuhinEditVo)(sql.ToString, param)
    End Function

    ''' <summary>
    ''' 部品番号、かつ集計コードYの部品を取得
    ''' </summary>
    ''' <param name="buhinNo">部品番号</param>
    ''' <param name="eventCode">イベントコード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function FindBuhinWithCodeYByInstl(ByVal buhinNo As String, ByVal eventCode As String) As List(Of TShisakuBuhinEditVo) Implements IShisakuBuhinEditDao.FindBuhinWithCodeYByInstl
        Dim sql As New StringBuilder
        With sql
            .Remove(0, sql.Length)
            ''↓↓2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD BEGIN
            '.AppendLine("  SELECT * FROM( SELECT A.*, CASE    ")
            '.AppendLine("  WHEN A.SIA_SHUKEI_CODE<>'' AND A.SHUKEI_CODE<>'' THEN A.SHUKEI_CODE ")
            '.AppendLine("  WHEN A.SIA_SHUKEI_CODE='' AND  A.SHUKEI_CODE<>'' THEN A.SHUKEI_CODE ")
            '.AppendLine("  WHEN A.SIA_SHUKEI_CODE<>'' AND A.SHUKEI_CODE='' THEN A.SIA_SHUKEI_CODE ")
            '.AppendLine("  END  AS CODE")
            '.AppendLine("  FROM " & MBOM_DB_NAME & ".DBO.T_SHISAKU_BUHIN_EDIT A  INNER JOIN " & MBOM_DB_NAME & ".DBO.T_SHISAKU_BUHIN_EDIT_INSTL B  ")
            '.AppendLine("  ON A.SHISAKU_EVENT_CODE=B.SHISAKU_EVENT_CODE ")
            '.AppendLine("  AND A.SHISAKU_BUKA_CODE=B.SHISAKU_BUKA_CODE ")
            '.AppendLine("  AND A.SHISAKU_BLOCK_NO=B.SHISAKU_BLOCK_NO ")
            '.AppendLine("  AND A.SHISAKU_BLOCK_NO_KAITEI_NO=B.SHISAKU_BLOCK_NO_KAITEI_NO ")
            '.AppendLine("  AND A.BUHIN_NO_HYOUJI_JUN=B.BUHIN_NO_HYOUJI_JUN ")
            '.AppendLine(" WHERE A.BUHIN_NO=@BuhinNo  AND B.INSU_SURYO <>0")

            '.AppendLine(" AND A.SHISAKU_EVENT_CODE=@ShisakuEventCode")

            '.AppendLine("  ) A WHERE A.CODE='Y' ")
            .AppendLine("  SELECT BE.* ")
            .AppendLine("  FROM  " & MBOM_DB_NAME & ".DBO.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ")
            .AppendLine("  INNER JOIN  " & MBOM_DB_NAME & ".DBO.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
            .AppendLine("  ON SBI.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
            .AppendLine("  AND SBI.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
            .AppendLine("  AND SBI.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
            .AppendLine("  AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
            .AppendLine("  AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN ")
            .AppendLine("  INNER JOIN  " & MBOM_DB_NAME & ".DBO.T_SHISAKU_BUHIN_EDIT BE ")
            .AppendLine("  ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
            .AppendLine("  AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
            .AppendLine("  AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
            .AppendLine("  AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
            .AppendLine("  AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
            .AppendLine("  WHERE SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
            .AppendLine("  AND SBI.INSTL_HINBAN = @BuhinNo ")
            .AppendLine("  AND BEI.INSU_SURYO > 0 ")
            .AppendLine("  AND (BE.SHUKEI_CODE = 'Y' OR BE.SIA_SHUKEI_CODE = 'Y') ")
            ''↑↑2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD END
        End With
        Dim param As New TShisakuBuhinEditVo()
        param.BuhinNo = buhinNo
        param.ShisakuEventCode = eventCode
        Dim db As New EBomDbClient
        Return db.QueryForList(Of TShisakuBuhinEditVo)(sql.ToString, param)


    End Function

    ''' <summary>
    ''' 部品番号、かつ集計コードYの部品を取得
    ''' </summary>
    ''' <param name="buhinNo">部品番号</param>
    ''' <param name="eventCode">イベントコード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function FindBuhinWithCodeYAndLevel(ByVal buhinNo As String, ByVal eventCode As String) As List(Of TShisakuBuhinEditVo) Implements IShisakuBuhinEditDao.FindBuhinWithCodeYAndLevel
        Dim sql As New StringBuilder
        With sql
            .Remove(0, sql.Length)
            .AppendLine("  SELECT * FROM( SELECT A.*, CASE    ")
            .AppendLine("  WHEN A.SIA_SHUKEI_CODE<>'' AND A.SHUKEI_CODE<>'' THEN A.SHUKEI_CODE ")
            .AppendLine("  WHEN A.SIA_SHUKEI_CODE='' AND  A.SHUKEI_CODE<>'' THEN A.SHUKEI_CODE ")
            .AppendLine("  WHEN A.SIA_SHUKEI_CODE<>'' AND A.SHUKEI_CODE='' THEN A.SIA_SHUKEI_CODE ")
            .AppendLine("  END  AS CODE")
            .AppendLine("  FROM " & MBOM_DB_NAME & ".DBO.T_SHISAKU_BUHIN_EDIT A  ")
            .AppendLine(" WHERE A.BUHIN_NO=@BuhinNo  AND A.LEVEL > 0 ")
            .AppendLine(" AND A.SHISAKU_EVENT_CODE=@ShisakuEventCode")
            .AppendLine("  ) A WHERE A.CODE='Y' ")
        End With
        Dim param As New TShisakuBuhinEditVo()
        param.BuhinNo = buhinNo
        param.ShisakuEventCode = eventCode
        Dim db As New EBomDbClient
        Return db.QueryForList(Of TShisakuBuhinEditVo)(sql.ToString, param)


    End Function

    ''' <summary>
    ''' 子部品の備考と引き取りコード更新
    ''' </summary>
    ''' <param name="buhinNo">部品番号</param>
    ''' <param name="bikou">更新備考</param>
    ''' <param name="makerCode">引き取りコード</param>
    ''' <param name="eventCode">イベントコード</param> 
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateKoBuhinBikou(ByVal buhinNo As String, ByVal bikou As String, ByVal makerCode As String, ByVal eventCode As String) Implements IShisakuBuhinEditDao.UpdateKoBuhinBikou
        Dim sql As New StringBuilder
        With sql
            .Remove(0, sql.Length)

            ''↓↓2014/09/17 2集計コード R/Yのブロック間紐付け b) 酒井 ADD BEGIN
            ''↓↓2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD BEGIN
            '.AppendLine(" UPDATE " & MBOM_DB_NAME & ".DBO.T_SHISAKU_BUHIN_EDIT A  SET Bikou=@Bikou  ")
            '.AppendLine(" UPDATE " & MBOM_DB_NAME & ".DBO.T_SHISAKU_BUHIN_EDIT  SET Bikou=@Bikou  ")
            ''↑↑2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD END
            'If makerCode <> "" Then
            ''↓↓2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD BEGIN
            '.AppendLine(" , Maker_Code =@MakerCode ")
            '.AppendLine(" , KYOUKU_SECTION =@MakerCode ")
            ''↑↑2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD END
            'End If
            '.AppendLine(" WHERE SHISAKU_EVENT_CODE=@ShisakuEventCode AND Buhin_No=@BuhinNo")
            .AppendLine(" UPDATE " & MBOM_DB_NAME & ".DBO.T_SHISAKU_BUHIN_EDIT ")
            .AppendLine(" SET BIKOU = @Bikou ")
            If makerCode <> "" Then
                .AppendLine(" , KYOUKU_SECTION = @MakerCode ")
            End If
            .AppendLine(" FROM " & MBOM_DB_NAME & ".DBO.T_SHISAKU_BUHIN_EDIT BE ")
            .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".DBO.T_SHISAKU_BUHIN_EDIT_INSTL BEI ON  ")
            .AppendLine(" BE.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE And ")
            .AppendLine(" BE.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE And ")
            .AppendLine(" BE.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO And ")
            .AppendLine(" BE.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO And ")
            .AppendLine(" BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN ")
            .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".DBO.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ON  ")
            .AppendLine(" BEI.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE And ")
            .AppendLine(" BEI.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE And ")
            .AppendLine(" BEI.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO And ")
            .AppendLine(" BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO And ")
            .AppendLine(" BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN ")
            .AppendLine(" WHERE (BE.SHISAKU_EVENT_CODE = @ShisakuEventCode) AND  ")
            .AppendLine(" (BE.BUHIN_NO = @BuhinNo) AND  ")
            .AppendLine(" (BE.LEVEL > 0) AND  ")
            .AppendLine(" ((BE.SHUKEI_CODE = 'Y') OR (BE.SIA_SHUKEI_CODE = 'Y')) AND ")
            .AppendLine(" (SBI.INSTL_DATA_KBN = '0')  ")
            ''↑↑2014/09/17 2集計コード R/Yのブロック間紐付け b) 酒井 ADD END
        End With
        Dim param As TShisakuBuhinEditVo = New TShisakuBuhinEditVo
        param.Bikou = bikou
        param.BuhinNo = buhinNo
        param.ShisakuEventCode = eventCode
        param.MakerCode = makerCode
        Dim db As New EBomDbClient
        Return db.Update(sql.ToString, param)

    End Function
    ''↑↑2014/08/11 2集計コード R/Yのブロック間紐付け b) (TES)施 ADD END





End Class
