Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports ShisakuCommon
Imports EBom.Data
Imports EBom.Common
Imports System.Text
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Vo
Imports System.Data.SqlClient

''↓↓2014/09/15 酒井 ADD START
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
''↑↑2014/09/15 酒井 ADD END

Namespace ShisakuBuhinMenu.Dao
    Public Class BuhinEditBaseDaoImpl : Implements BuhinEditBaseDao

        ''' <summary>
        ''' 設計ブロック情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Public Function FindBySekkeiBlock(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As List(Of TShisakuSekkeiBlockVo) Implements BuhinEditBaseDao.FindBySekkeiBlock
            Dim sql As New StringBuilder

            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT SB.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE")
                .AppendLine(" SB.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
            End With

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockVo)(sql.ToString, param)
        End Function

        ''' <summary>
        ''' 全設計ブロック情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Public Function FindBySekkeiBlockAll(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockVo) Implements BuhinEditBaseDao.FindBySekkeiBlockAll
            Dim sql As New StringBuilder

            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine("   SB.SHISAKU_EVENT_CODE")
                .AppendLine(" , TMP.SHISAKU_BUKA_CODE")
                .AppendLine(" , SB.SHISAKU_BLOCK_NO_HYOUJI_JUN")
                .AppendLine(" , SB.SHISAKU_BLOCK_NO")
                .AppendLine(" , SB.SHISAKU_BLOCK_NO_KAITEI_NO")
                .AppendLine(" , SB.BLOCK_FUYOU")
                .AppendLine(" , SB.JYOUTAI")
                .AppendLine(" , SB.UNIT_KBN")
                .AppendLine(" , SB.SHISAKU_BLOCK_NAME")
                .AppendLine(" , SB.USER_ID")
                .AppendLine(" , SB.TEL_NO")
                .AppendLine(" , SB.KAITEI_NAIYOU")
                .AppendLine(" , SB.SAISYU_KOUSHINBI")
                .AppendLine(" , SB.SAISYU_KOUSHINJIKAN")
                .AppendLine(" , SB.MEMO")
                .AppendLine(" , SB.TANTO_SYOUNIN_JYOUTAI")
                .AppendLine(" , SB.TANTO_SYOUNIN_KA")
                .AppendLine(" , SB.TANTO_SYOUNIN_SYA")
                .AppendLine(" , SB.TANTO_SYOUNIN_HI")
                .AppendLine(" , SB.TANTO_SYOUNIN_JIKAN")
                .AppendLine(" , SB.KACHOU_SYOUNIN_JYOUTAI")
                .AppendLine(" , SB.KACHOU_SYOUNIN_KA")
                .AppendLine(" , SB.KACHOU_SYOUNIN_SYA")
                .AppendLine(" , SB.KACHOU_SYOUNIN_HI")
                .AppendLine(" , SB.KACHOU_SYOUNIN_JIKAN")
                .AppendLine(" , SB.KAITEI_HANDAN_FLG")
                .AppendLine(" , SB.CREATED_USER_ID")
                .AppendLine(" , SB.CREATED_DATE")
                .AppendLine(" , SB.CREATED_TIME")
                .AppendLine(" , SB.UPDATED_USER_ID")
                .AppendLine(" , SB.UPDATED_DATE")
                .AppendLine(" , SB.UPDATED_TIME")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BLOCK_SEKKEIKA_TMP TMP")
                .AppendLine(" ON TMP.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE")
                .AppendLine(" AND TMP.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO")
                .AppendLine(" WHERE")
                .AppendLine(" SB.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
            End With

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuEventCode

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockVo)(sql.ToString, param)

        End Function

        ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD START
        Public Function FindBySekkeiBlockEbomKanshiAll(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockEbomKanshiVo) Implements BuhinEditBaseDao.FindBySekkeiBlockEbomKanshiAll
            Dim sql As New StringBuilder

            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT SB.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_EBOM_KANSHI SB WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE")
                .AppendLine(" SB.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND NOT(SB.SHISAKU_BLOCK_NO_KAITEI_NO = '999') ")
            End With

            Dim param As New TShisakuSekkeiBlockEbomKanshiVo
            param.ShisakuEventCode = shisakuEventCode

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockEbomKanshiVo)(sql.ToString, param)

        End Function

        ''' <summary>
        ''' 開発符号を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>イベント情報</returns>
        ''' <remarks></remarks>
        Public Function FindByKaihatsufugo(ByVal shisakuEventCode As String) As TShisakuEventVo Implements BuhinEditBaseDao.FindByKaihatsufugo
            Dim sql As String = _
             " SELECT * " _
             & " FROM " _
             & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT WITH (NOLOCK, NOWAIT) " _
             & " WHERE " _
             & " SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventVo
            param.ShisakuEventCode = shisakuEventCode

            Return db.QueryForObject(Of TShisakuEventVo)(sql, param)
        End Function

        ''↓↓2014/09/18 酒井 ADD BEGIN
        Public Function FindByEventCode(ByVal shisakuEventCode As String) As List(Of TShisakuBuhinEditVo) Implements BuhinEditBaseDao.FindByEventCode
            Dim sql As String = _
             " SELECT DISTINCT SHISAKU_EVENT_CODE,SHISAKU_BUKA_CODE,SHISAKU_BLOCK_NO,SHISAKU_BLOCK_NO_KAITEI_NO" _
             & " FROM " _
             & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) " _
             & " WHERE " _
             & " SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditVo
            param.ShisakuEventCode = shisakuEventCode

            Return db.QueryForList(Of TShisakuBuhinEditVo)(sql, param)
        End Function

        'Public Function FindByLevelZero(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinEditVo) Implements BuhinEditBaseDao.FindByLevelZero
        '    Dim sql As String = _
        '         " SELECT * " _
        '         & " FROM " _
        '         & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) " _
        '         & " WHERE " _
        '         & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
        '         & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
        '         & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
        '         & " AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
        '         & " AND LEVEL = 0 " _
        '         & " AND BUHIN_NO_HYOUJI_JUN > " _
        '         & " (SELECT MIN(BUHIN_NO_HYOUJI_JUN) " _
        '         & " FROM " _
        '         & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) " _
        '         & " WHERE " _
        '         & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
        '         & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
        '         & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
        '         & " AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
        '         & " AND LEVEL > 0 )" _
        '         & " ORDER BY BUHIN_NO_HYOUJI_JUN "

        '    Dim db As New EBomDbClient
        '    Dim param As New TShisakuBuhinEditVo
        '    param.ShisakuEventCode = shisakuEventCode
        '    param.ShisakuBukaCode = shisakuBukaCode
        '    param.ShisakuBlockNo = shisakuBlockNo
        '    param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo

        '    Return db.QueryForList(Of TShisakuBuhinEditVo)(sql, param)


        'End Function

        Public Function FindByLevelZero2(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinEditInstlVo) Implements BuhinEditBaseDao.FindByLevelZero2
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine("SELECT BEI.* FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI WITH (NOLOCK, NOWAIT)")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT)")
                .AppendLine(" ON BE.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BE.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" AND BE.LEVEL = 0 ")
                .AppendLine(" WHERE BEI.SHISAKU_EVENT_CODE = @ShisakuEventCode")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = @ShisakuBukaCode")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = @ShisakuBlockNo")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo")
                .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN <> BEI.INSTL_HINBAN_HYOUJI_JUN")
                .AppendLine(" ORDER BY BEI.INSTL_HINBAN_HYOUJI_JUN")
            End With

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo

            Return db.QueryForList(Of TShisakuBuhinEditInstlVo)(sql.ToString, param)

        End Function


        '↓↓2014/10/30 酒井 ADD BEGIN

        Public Function FindByLevelZeroInstlHyoujiJun(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal buhinNoHyoujiJun As Integer) As List(Of TShisakuBuhinEditInstlVo) Implements BuhinEditBaseDao.FindByLevelZeroInstlHyoujiJun
            Dim sql As String = _
                     " SELECT * " _
                     & " FROM " _
                     & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL WITH (NOLOCK, NOWAIT) " _
                     & " WHERE " _
                     & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                     & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
                     & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
                     & " AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
                     & " AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
                     & " AND INSU_SURYO = 1 "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = buhinNoHyoujiJun

            Return db.QueryForList(Of TShisakuBuhinEditInstlVo)(sql, param)
        End Function

        '↑↑2014/10/30 酒井 ADD END
        Public Function FindByNotLevelZero(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinEditVo) Implements BuhinEditBaseDao.FindByNotLevelZero
            Dim sql As String = _
             " SELECT * " _
             & " FROM " _
             & " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) " _
             & " WHERE " _
             & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
             & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
             & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
             & " AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
             & " AND NOT LEVEL = 0 " _
             & " ORDER BY BUHIN_NO_HYOUJI_JUN "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo

            Return db.QueryForList(Of TShisakuBuhinEditVo)(sql, param)
        End Function

        Public Sub UpdateLevelZeroBuhinNoHyoujiJunByBuhinEdit(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal buhinNoHyoujiJun As Integer, ByVal updateBuhinNoHyoujiJun As Integer) Implements BuhinEditBaseDao.UpdateLevelZeroBuhinNoHyoujiJunByBuhinEdit

            If Not buhinNoHyoujiJun > updateBuhinNoHyoujiJun Then
                Exit Sub
            End If

            Dim sb As New StringBuilder
            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT ")
                .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = - 1 ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = " & buhinNoHyoujiJun)
            End With
            db.Update(sb.ToString, param)

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT ")
                .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = BUHIN_NO_HYOUJI_JUN + 1 ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN >= " & updateBuhinNoHyoujiJun)
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN < " & buhinNoHyoujiJun)
            End With
            db.Update(sb.ToString, param)

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT ")
                .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = " & updateBuhinNoHyoujiJun)
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = -1 ")
            End With
            db.Update(sb.ToString, param)
        End Sub
        Public Sub UpdateLevelZeroBuhinNoHyoujiJunByBuhinEditInstl(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal buhinNoHyoujiJun As Integer, ByVal updateBuhinNoHyoujiJun As Integer) Implements BuhinEditBaseDao.UpdateLevelZeroBuhinNoHyoujiJunByBuhinEditInstl
            If Not buhinNoHyoujiJun > updateBuhinNoHyoujiJun Then
                Exit Sub
            End If

            Dim sb As New StringBuilder
            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL ")
                .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = - 1 ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = " & buhinNoHyoujiJun)
            End With
            db.Update(sb.ToString, param)

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL ")
                .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = BUHIN_NO_HYOUJI_JUN + 1 ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN >= " & updateBuhinNoHyoujiJun)
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN < " & buhinNoHyoujiJun)
            End With
            db.Update(sb.ToString, param)

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL ")
                .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = " & updateBuhinNoHyoujiJun)
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = -1 ")
            End With
            db.Update(sb.ToString, param)
        End Sub
        Public Sub UpdateLevelZeroBuhinNoHyoujiJunByBuhinEditBase(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal buhinNoHyoujiJun As Integer, ByVal updateBuhinNoHyoujiJun As Integer) Implements BuhinEditBaseDao.UpdateLevelZeroBuhinNoHyoujiJunByBuhinEditBase

            If Not buhinNoHyoujiJun > updateBuhinNoHyoujiJun Then
                Exit Sub
            End If

            Dim sb As New StringBuilder
            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditBaseVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE ")
                .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = - 1 ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = " & buhinNoHyoujiJun)
            End With
            db.Update(sb.ToString, param)

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE ")
                .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = BUHIN_NO_HYOUJI_JUN + 1 ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN >= " & updateBuhinNoHyoujiJun)
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN < " & buhinNoHyoujiJun)
            End With
            db.Update(sb.ToString, param)

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE ")
                .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = " & updateBuhinNoHyoujiJun)
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = -1 ")
            End With
            db.Update(sb.ToString, param)
        End Sub
        Public Sub UpdateLevelZeroBuhinNoHyoujiJunByBuhinEditInstlBase(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal buhinNoHyoujiJun As Integer, ByVal updateBuhinNoHyoujiJun As Integer) Implements BuhinEditBaseDao.UpdateLevelZeroBuhinNoHyoujiJunByBuhinEditInstlBase
            If Not buhinNoHyoujiJun > updateBuhinNoHyoujiJun Then
                Exit Sub
            End If

            Dim sb As New StringBuilder
            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditInstlBaseVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE ")
                .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = - 1 ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = " & buhinNoHyoujiJun)
            End With
            db.Update(sb.ToString, param)

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE ")
                .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = BUHIN_NO_HYOUJI_JUN + 1 ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN >= " & updateBuhinNoHyoujiJun)
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN < " & buhinNoHyoujiJun)
            End With
            db.Update(sb.ToString, param)

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE ")
                .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = " & updateBuhinNoHyoujiJun)
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = -1 ")
            End With
            db.Update(sb.ToString, param)
        End Sub

        '/*** 20140911 CHANGE START ***/
        ''' <summary>
        ''' 部品表編集と部品編集ベースにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <param name="JikyuUmu">自給品の有無</param>
        ''' <param name="TsukurikataTenkaiFlg">作り方フラグ</param>
        ''' <param name="aDate">試作日</param>
        ''' <remarks></remarks>
        Public Sub InsertBySekkeiBuhinEdit(ByVal shisakuEventCode As String, _
                                     ByVal shisakuBukaCode As String, _
                                     ByVal shisakuBlockNo As String, _
                                     ByVal shisakuBlockNoKaiteiNo As String, _
                                     ByVal koseiMatrix As BuhinKoseiMatrix, _
                                     ByVal JikyuUmu As String, _
                                     ByVal TsukurikataTenkaiFlg As String, _
                                     ByVal login As LoginInfo, _
                                     ByVal aDate As ShisakuDate, Optional ByVal MaxUpdateBuhinNoHyoujijun As Integer = 0) Implements BuhinEditBaseDao.InsertBySekkeiBuhinEdit
            Dim param As New TShisakuBuhinEditVo
            Dim buhinNoHyoujiJun As Integer = 0
            'Dim aDate As New ShisakuDate
            Const FMT_STR As String = ",'{0}'"
            Const FMT_INT As String = ",{0}"

            '配列定義
            'Dim sqlHairetu(5000) As String

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.BeginTransaction()

                For index As Integer = 0 To koseiMatrix.GetInputRowIndexes.Count - 1

                    'Dim MakerVo As New TShisakuBuhinEditVo

                    'MakerVo = FindByKoutanTorihikisaki(koseiMatrix(index).BuhinNo)

                    '部品番号がNULLの項目が存在している'
                    If StringUtil.IsEmpty(koseiMatrix(index).BuhinNo) Then
                        Continue For
                    End If

                    '自給品の削除'
                    If StringUtil.Equals(JikyuUmu, "0") Then
                        If StringUtil.IsEmpty(koseiMatrix(index).ShukeiCode) Then
                            If StringUtil.Equals(koseiMatrix(index).SiaShukeiCode, "J") Then
                                Continue For
                            End If
                        ElseIf StringUtil.Equals(koseiMatrix(index).ShukeiCode, "J") Then
                            Continue For
                        End If
                    End If
                    ''↓↓2014/07/24 Ⅰ.2.管理項目追加_ak) (TES)張 ADD BEGIN
                    If StringUtil.Equals(TsukurikataTenkaiFlg, "0") Then
                        koseiMatrix(index).TsukurikataSeisaku = ""
                        koseiMatrix(index).TsukurikataKatashiyou1 = ""
                        koseiMatrix(index).TsukurikataKatashiyou2 = ""
                        koseiMatrix(index).TsukurikataKatashiyou3 = ""
                        koseiMatrix(index).TsukurikataTigu = ""
                        ''↓↓2014/08/19 Ⅰ.2.管理項目追加_ad) 酒井 ADD BEGIN
                        koseiMatrix(index).TsukurikataNounyu = 0
                        ''↑↑2014/08/19 Ⅰ.2.管理項目追加_ad) 酒井 ADD END
                        koseiMatrix(index).TsukurikataKibo = ""
                    End If
                    ''↑↑2014/07/24 Ⅰ.2.管理項目追加_ak) (TES)張 ADD END
                    '出図予定日が99999999の場合、0を設定する。
                    If koseiMatrix(index).ShutuzuYoteiDate = 99999999 Then
                        koseiMatrix(index).ShutuzuYoteiDate = 0
                    End If
                    If StringUtil.IsEmpty(koseiMatrix(index).ShutuzuYoteiDate) Then
                        koseiMatrix(index).ShutuzuYoteiDate = 0
                    End If

                    ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
                    'Nothingなら０を設定
                    If StringUtil.IsEmpty(koseiMatrix(index).MaterialInfoLength) Then
                        koseiMatrix(index).MaterialInfoLength = 0
                    End If
                    If StringUtil.IsEmpty(koseiMatrix(index).MaterialInfoWidth) Then
                        koseiMatrix(index).MaterialInfoWidth = 0
                    End If
                    ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END

                    'Nothingなら０を設定
                    If StringUtil.IsEmpty(koseiMatrix(index).ShisakuBuhinHi) Then
                        koseiMatrix(index).ShisakuBuhinHi = 0
                    End If
                    If StringUtil.IsEmpty(koseiMatrix(index).ShisakuKataHi) Then
                        koseiMatrix(index).ShisakuKataHi = 0
                    End If
                    If StringUtil.IsEmpty(koseiMatrix(index).EditTourokubi) Then
                        koseiMatrix(index).EditTourokubi = 0
                    End If
                    If StringUtil.IsEmpty(koseiMatrix(index).EditTourokujikan) Then
                        koseiMatrix(index).EditTourokujikan = 0
                    End If

                    '試作部品編集情報
                    '2012/01/23 供給セクション追加
                    ''↓↓2014/07/24 Ⅰ.2.管理項目追加_ak) (TES)張 ADD BEGIN
                    ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_j) (TES)張 CHG BEGIN
                    ''↓↓2014/08/19 Ⅰ.5.EBOM差分出力 ba) (TES)張 CHG START
                    ''夜間展開するかどうかの判断を追加
                    'Dim sql As String = _
                    '    " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT " _
                    Dim sql As New System.Text.StringBuilder
                    With sql
                        If login IsNot Nothing Then
                            .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT ")
                        Else
                            .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI ")
                        End If
                        .AppendLine(" WITH (UPDLOCK) ")
                        .AppendLine(" ( ")
                        .AppendLine(" SHISAKU_EVENT_CODE, ")
                        .AppendLine(" SHISAKU_BUKA_CODE, ")
                        .AppendLine(" SHISAKU_BLOCK_NO, ")
                        .AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO, ")
                        .AppendLine(" BUHIN_NO_HYOUJI_JUN, ")
                        .AppendLine(" LEVEL, ")
                        .AppendLine(" SHUKEI_CODE, ")
                        .AppendLine(" SIA_SHUKEI_CODE, ")
                        .AppendLine(" GENCYO_CKD_KBN, ")
                        .AppendLine(" KYOUKU_SECTION, ")
                        .AppendLine(" MAKER_CODE, ")
                        .AppendLine(" MAKER_NAME, ")
                        .AppendLine(" BUHIN_NO, ")
                        .AppendLine(" BUHIN_NO_KBN, ")
                        .AppendLine(" BUHIN_NO_KAITEI_NO, ")
                        .AppendLine(" EDA_BAN, ")
                        .AppendLine(" BUHIN_NAME, ")
                        .AppendLine(" SAISHIYOUFUKA, ")
                        .AppendLine(" SHUTUZU_YOTEI_DATE, ")
                        .AppendLine(" BASE_BUHIN_FLG, ")
                        .AppendLine(" ZAISHITU_KIKAKU_1, ")
                        .AppendLine(" ZAISHITU_KIKAKU_2, ")
                        .AppendLine(" ZAISHITU_KIKAKU_3, ")
                        .AppendLine(" ZAISHITU_MEKKI, ")
                        .AppendLine(" TSUKURIKATA_SEISAKU, ")
                        .AppendLine(" TSUKURIKATA_KATASHIYOU_1, ")
                        .AppendLine(" TSUKURIKATA_KATASHIYOU_2, ")
                        .AppendLine(" TSUKURIKATA_KATASHIYOU_3, ")
                        .AppendLine(" TSUKURIKATA_TIGU, ")
                        .AppendLine(" TSUKURIKATA_NOUNYU, ")
                        .AppendLine(" TSUKURIKATA_KIBO, ")
                        .AppendLine(" SHISAKU_BANKO_SURYO, ")
                        .AppendLine(" SHISAKU_BANKO_SURYO_U, ")
                        .AppendLine(" MATERIAL_INFO_LENGTH, ")
                        .AppendLine(" MATERIAL_INFO_WIDTH, ")
                        .AppendLine(" DATA_ITEM_KAITEI_NO, ")
                        .AppendLine(" DATA_ITEM_AREA_NAME, ")
                        .AppendLine(" DATA_ITEM_SET_NAME, ")
                        .AppendLine(" DATA_ITEM_KAITEI_INFO, ")
                        .AppendLine(" SHISAKU_BUHIN_HI, ")
                        .AppendLine(" SHISAKU_KATA_HI, ")
                        .AppendLine(" BIKOU, ")
                        .AppendLine(" BUHIN_NOTE, ")
                        .AppendLine(" EDIT_TOUROKUBI, ")
                        .AppendLine(" EDIT_TOUROKUJIKAN, ")
                        .AppendLine(" KAITEI_HANDAN_FLG, ")
                        .AppendLine(" SHISAKU_LIST_CODE, ")
                        .AppendLine(" CREATED_USER_ID, ")
                        .AppendLine(" CREATED_DATE, ")
                        .AppendLine(" CREATED_TIME, ")
                        .AppendLine(" UPDATED_USER_ID, ")
                        .AppendLine(" UPDATED_DATE, ")
                        .AppendLine(" UPDATED_TIME ")
                        .AppendLine(" ,BASE_BUHIN_SEQ")
                        .AppendLine(" ) ")
                        .AppendLine("VALUES ( ")
                        .AppendFormat("'{0}'", shisakuEventCode)
                        .AppendFormat(FMT_STR, shisakuBukaCode)
                        .AppendFormat(FMT_STR, shisakuBlockNo)
                        .AppendFormat(FMT_STR, shisakuBlockNoKaiteiNo)
                        If login IsNot Nothing Then
                            .AppendFormat(FMT_INT, buhinNoHyoujiJun + MaxUpdateBuhinNoHyoujijun)
                        Else
                            .AppendFormat(FMT_INT, buhinNoHyoujiJun)
                        End If
                        .AppendFormat(FMT_INT, koseiMatrix(index).Level)
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).ShukeiCode))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).SiaShukeiCode))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).GencyoCkdKbn))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).KyoukuSection))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).MakerCode))
                        .AppendFormat(FMT_STR, Trim(koseiMatrix(index).MakerName))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).BuhinNo))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).BuhinNoKbn))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).BuhinNoKaiteiNo))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).EdaBan))
                        .AppendFormat(FMT_STR, Trim(StringUtil.Nvl(koseiMatrix(index).BuhinName)))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).Saishiyoufuka))
                        .AppendFormat(FMT_INT, koseiMatrix(index).ShutuzuYoteiDate)
                        .AppendFormat(FMT_STR, "1")
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).ZaishituKikaku1))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).ZaishituKikaku2))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).ZaishituKikaku3))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).ZaishituMekki))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).TsukurikataSeisaku))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).TsukurikataKatashiyou1))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).TsukurikataKatashiyou2))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).TsukurikataKatashiyou3))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).TsukurikataTigu))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).TsukurikataNounyu))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).TsukurikataKibo))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).ShisakuBankoSuryo))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).ShisakuBankoSuryoU))
                        .AppendFormat(FMT_INT, koseiMatrix(index).MaterialInfoLength)
                        .AppendFormat(FMT_INT, koseiMatrix(index).MaterialInfoWidth)
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).DataItemKaiteiNo))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).DataItemAreaName))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).DataItemSetName))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).DataItemKaiteiInfo))
                        .AppendFormat(FMT_INT, koseiMatrix(index).ShisakuBuhinHi)
                        .AppendFormat(FMT_INT, koseiMatrix(index).ShisakuKataHi)
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).Bikou))
                        .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).BuhinNote))
                        .AppendFormat(FMT_INT, koseiMatrix(index).EditTourokubi)
                        .AppendFormat(FMT_INT, koseiMatrix(index).EditTourokujikan)
                        .AppendFormat(FMT_STR, koseiMatrix(index).KaiteiHandanFlg)
                        .AppendFormat(FMT_STR, koseiMatrix(index).ShisakuListCode)
                        .AppendFormat(FMT_STR, LoginInfo.Now.UserId)
                        .AppendFormat(FMT_STR, aDate.CurrentDateDbFormat)
                        .AppendFormat(FMT_STR, aDate.CurrentTimeDbFormat)
                        .AppendFormat(FMT_STR, LoginInfo.Now.UserId)
                        .AppendFormat(FMT_STR, aDate.CurrentDateDbFormat)
                        .AppendFormat(FMT_STR, aDate.CurrentTimeDbFormat)
                        If login IsNot Nothing Then
                            .AppendFormat(FMT_INT, buhinNoHyoujiJun + MaxUpdateBuhinNoHyoujijun)
                        Else
                            .AppendFormat(FMT_INT, buhinNoHyoujiJun)
                        End If
                        .AppendLine(" ) ")
                    End With
                    ''↑↑2014/08/19 Ⅰ.5.EBOM差分出力 ba) (TES)張 CHG END
                    'sql = sql & " WITH (UPDLOCK) " _
                    '    & " ( " _
                    '    & " SHISAKU_EVENT_CODE, " _
                    '    & " SHISAKU_BUKA_CODE, " _
                    '    & " SHISAKU_BLOCK_NO, " _
                    '    & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                    '    & " BUHIN_NO_HYOUJI_JUN, " _
                    '    & " LEVEL, " _
                    '    & " SHUKEI_CODE, " _
                    '    & " SIA_SHUKEI_CODE, " _
                    '    & " GENCYO_CKD_KBN, " _
                    '    & " KYOUKU_SECTION, " _
                    '    & " MAKER_CODE, " _
                    '    & " MAKER_NAME, " _
                    '    & " BUHIN_NO, " _
                    '    & " BUHIN_NO_KBN, " _
                    '    & " BUHIN_NO_KAITEI_NO, " _
                    '    & " EDA_BAN, " _
                    '    & " BUHIN_NAME, " _
                    '    & " SAISHIYOUFUKA, " _
                    '    & " SHUTUZU_YOTEI_DATE, " _
                    '    & " BASE_BUHIN_FLG, " _
                    '    & " ZAISHITU_KIKAKU_1, " _
                    '    & " ZAISHITU_KIKAKU_2, " _
                    '    & " ZAISHITU_KIKAKU_3, " _
                    '    & " ZAISHITU_MEKKI, " _
                    '    & " TSUKURIKATA_SEISAKU, " _
                    '    & " TSUKURIKATA_KATASHIYOU_1, " _
                    '    & " TSUKURIKATA_KATASHIYOU_2, " _
                    '    & " TSUKURIKATA_KATASHIYOU_3, " _
                    '    & " TSUKURIKATA_TIGU, " _
                    '    & " TSUKURIKATA_NOUNYU, " _
                    '    & " TSUKURIKATA_KIBO, " _
                    '    & " SHISAKU_BANKO_SURYO, " _
                    '    & " SHISAKU_BANKO_SURYO_U, " _
                    '    & " MATERIAL_INFO_LENGTH, " _
                    '    & " MATERIAL_INFO_WIDTH, " _
                    '    & " DATA_ITEM_KAITEI_NO, " _
                    '    & " DATA_ITEM_AREA_NAME, " _
                    '    & " DATA_ITEM_SET_NAME, " _
                    '    & " DATA_ITEM_KAITEI_INFO, " _
                    '    & " SHISAKU_BUHIN_HI, " _
                    '    & " SHISAKU_KATA_HI, " _
                    '    & " BIKOU, " _
                    '    & " BUHIN_NOTE, " _
                    '    & " EDIT_TOUROKUBI, " _
                    '    & " EDIT_TOUROKUJIKAN, " _
                    '    & " KAITEI_HANDAN_FLG, " _
                    '    & " SHISAKU_LIST_CODE, " _
                    '    & " CREATED_USER_ID, " _
                    '    & " CREATED_DATE, " _
                    '    & " CREATED_TIME, " _
                    '    & " UPDATED_USER_ID, " _
                    '    & " UPDATED_DATE, " _
                    '    & " UPDATED_TIME " _
                    '    & " ,BASE_BUHIN_SEQ" _
                    '    & " ) " _
                    '    & "VALUES ( " _
                    '    & "'" & shisakuEventCode & "', " _
                    '    & "'" & shisakuBukaCode & "', " _
                    '& "'" & shisakuBlockNo & "', " _
                    '& "'" & shisakuBlockNoKaiteiNo & "', "
                    'If login IsNot Nothing Then
                    '    sql = sql & buhinNoHyoujiJun + MaxUpdateBuhinNoHyoujijun & ", "
                    'Else
                    '    sql = sql & buhinNoHyoujiJun & ", "
                    'End If
                    'sql = sql _
                    '    & koseiMatrix(index).Level & ", " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).ShukeiCode) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).SiaShukeiCode) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).GencyoCkdKbn) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).KyoukuSection) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).MakerCode) & "', " _
                    '        & "'" & Trim(koseiMatrix(index).MakerName) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).BuhinNo) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).BuhinNoKbn) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).BuhinNoKaiteiNo) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).EdaBan) & "', " _
                    '        & "'" & Trim(StringUtil.Nvl(koseiMatrix(index).BuhinName)) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).Saishiyoufuka) & "', " _
                    '        & koseiMatrix(index).ShutuzuYoteiDate & ", " _
                    '        & "'1', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).ZaishituKikaku1) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).ZaishituKikaku2) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).ZaishituKikaku3) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).ZaishituMekki) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataSeisaku) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataKatashiyou1) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataKatashiyou2) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataKatashiyou3) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataTigu) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataNounyu) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataKibo) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).ShisakuBankoSuryo) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).ShisakuBankoSuryoU) & "', " _
                    '        & koseiMatrix(index).MaterialInfoLength & ", " _
                    '        & koseiMatrix(index).MaterialInfoWidth & ", " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).DataItemKaiteiNo) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).DataItemAreaName) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).DataItemSetName) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).DataItemKaiteiInfo) & "', " _
                    '        & koseiMatrix(index).ShisakuBuhinHi & ", " _
                    '        & koseiMatrix(index).ShisakuKataHi & ", " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).Bikou) & "', " _
                    '        & "'" & StringUtil.Nvl(koseiMatrix(index).BuhinNote) & "'," _
                    '        & koseiMatrix(index).EditTourokubi & ", " _
                    '        & koseiMatrix(index).EditTourokujikan & ", " _
                    '        & "'" & koseiMatrix(index).KaiteiHandanFlg & "', " _
                    '        & "'" & koseiMatrix(index).ShisakuListCode & "', " _
                    '        & "'" & LoginInfo.Now.UserId & "', " _
                    '        & "'" & aDate.CurrentDateDbFormat & "', " _
                    '        & "'" & aDate.CurrentTimeDbFormat & "', " _
                    '        & "'" & LoginInfo.Now.UserId & "', " _
                    '        & "'" & aDate.CurrentDateDbFormat & "', " _
                    '        & "'" & aDate.CurrentTimeDbFormat & "' "
                    'If login IsNot Nothing Then
                    '    sql = sql & "," & buhinNoHyoujiJun + MaxUpdateBuhinNoHyoujijun
                    'Else
                    '    sql = sql & "," & buhinNoHyoujiJun
                    'End If
                    'sql = sql & " ) "
                    '20140818 Sakai Add
                    '& " TSUKURIKATA_KOUHOU, " _を削除
                    '& " TSUKURIKATA_KATASHIYOU, " _を削除
                    '    & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataKatashiyou3) & "', " _を追加
                    ''↑↑2014/07/24 Ⅰ.2.管理項目追加_ak) (TES)張 ADD END

                    '試作部品編集情報（ベース）
                    ''↓↓2014/07/24 Ⅰ.2.管理項目追加_ak) (TES)張 ADD BEGIN
                    ''↓↓2014/08/19 Ⅰ.5.EBOM差分出力 ba) (TES)張 CHG START
                    ''夜間展開するかどうかの判断を追加
                    Dim sqlBase As New System.Text.StringBuilder
                    If login IsNot Nothing Then
                        With sqlBase
                            .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE ")
                            .AppendLine(" WITH (UPDLOCK) ")
                            .AppendLine(" ( ")
                            .AppendLine(" SHISAKU_EVENT_CODE, ")
                            .AppendLine(" SHISAKU_BUKA_CODE, ")
                            .AppendLine(" SHISAKU_BLOCK_NO, ")
                            .AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO, ")
                            .AppendLine(" BUHIN_NO_HYOUJI_JUN, ")
                            .AppendLine(" LEVEL, ")
                            .AppendLine(" SHUKEI_CODE, ")
                            .AppendLine(" SIA_SHUKEI_CODE, ")
                            .AppendLine(" GENCYO_CKD_KBN, ")
                            .AppendLine(" KYOUKU_SECTION, ")
                            .AppendLine(" MAKER_CODE, ")
                            .AppendLine(" MAKER_NAME, ")
                            .AppendLine(" BUHIN_NO, ")
                            .AppendLine(" BUHIN_NO_KBN, ")
                            .AppendLine(" BUHIN_NO_KAITEI_NO, ")
                            .AppendLine(" EDA_BAN, ")
                            .AppendLine(" BUHIN_NAME, ")
                            .AppendLine(" SAISHIYOUFUKA, ")
                            .AppendLine(" SHUTUZU_YOTEI_DATE, ")
                            .AppendLine(" BASE_BUHIN_FLG, ")
                            .AppendLine(" ZAISHITU_KIKAKU_1, ")
                            .AppendLine(" ZAISHITU_KIKAKU_2, ")
                            .AppendLine(" ZAISHITU_KIKAKU_3, ")
                            .AppendLine(" ZAISHITU_MEKKI, ")
                            .AppendLine(" TSUKURIKATA_SEISAKU, ")
                            .AppendLine(" TSUKURIKATA_KATASHIYOU_1, ")
                            .AppendLine(" TSUKURIKATA_KATASHIYOU_2, ")
                            .AppendLine(" TSUKURIKATA_KATASHIYOU_3, ")
                            .AppendLine(" TSUKURIKATA_TIGU, ")
                            .AppendLine(" TSUKURIKATA_NOUNYU, ")
                            .AppendLine(" TSUKURIKATA_KIBO, ")
                            .AppendLine(" SHISAKU_BANKO_SURYO, ")
                            .AppendLine(" SHISAKU_BANKO_SURYO_U, ")
                            .AppendLine(" MATERIAL_INFO_LENGTH, ")
                            .AppendLine(" MATERIAL_INFO_WIDTH, ")
                            .AppendLine(" DATA_ITEM_KAITEI_NO, ")
                            .AppendLine(" DATA_ITEM_AREA_NAME, ")
                            .AppendLine(" DATA_ITEM_SET_NAME, ")
                            .AppendLine(" DATA_ITEM_KAITEI_INFO, ")
                            .AppendLine(" SHISAKU_BUHIN_HI, ")
                            .AppendLine(" SHISAKU_KATA_HI, ")
                            .AppendLine(" BIKOU, ")
                            .AppendLine(" BUHIN_NOTE, ")
                            .AppendLine(" EDIT_TOUROKUBI, ")
                            .AppendLine(" EDIT_TOUROKUJIKAN, ")
                            .AppendLine(" KAITEI_HANDAN_FLG, ")
                            .AppendLine(" SHISAKU_LIST_CODE, ")
                            .AppendLine(" CREATED_USER_ID, ")
                            .AppendLine(" CREATED_DATE, ")
                            .AppendLine(" CREATED_TIME, ")
                            .AppendLine(" UPDATED_USER_ID, ")
                            .AppendLine(" UPDATED_DATE, ")
                            .AppendLine(" UPDATED_TIME ")
                            .AppendLine(" ,BASE_BUHIN_SEQ")
                            .AppendLine(" ) ")
                            .AppendLine("VALUES ( ")
                            .AppendFormat("'{0}'", shisakuEventCode)
                            .AppendFormat(FMT_STR, shisakuBukaCode)
                            .AppendFormat(FMT_STR, shisakuBlockNo)
                            .AppendFormat(FMT_STR, shisakuBlockNoKaiteiNo)
                            .AppendFormat(FMT_INT, buhinNoHyoujiJun + MaxUpdateBuhinNoHyoujijun)
                            .AppendFormat(FMT_INT, koseiMatrix(index).Level)
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).ShukeiCode))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).SiaShukeiCode))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).GencyoCkdKbn))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).KyoukuSection))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).MakerCode))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).MakerName))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).BuhinNo))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).BuhinNoKbn))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).BuhinNoKaiteiNo))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).EdaBan))
                            .AppendFormat(FMT_STR, Trim(StringUtil.Nvl(koseiMatrix(index).BuhinName)))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).Saishiyoufuka))
                            .AppendFormat(FMT_INT, koseiMatrix(index).ShutuzuYoteiDate)
                            .AppendFormat(FMT_STR, "1")
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).ZaishituKikaku1))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).ZaishituKikaku2))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).ZaishituKikaku3))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).ZaishituMekki))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).TsukurikataSeisaku))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).TsukurikataKatashiyou1))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).TsukurikataKatashiyou2))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).TsukurikataKatashiyou3))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).TsukurikataTigu))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).TsukurikataNounyu))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).TsukurikataKibo))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).ShisakuBankoSuryo))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).ShisakuBankoSuryoU))
                            .AppendFormat(FMT_INT, koseiMatrix(index).MaterialInfoLength)
                            .AppendFormat(FMT_INT, koseiMatrix(index).MaterialInfoWidth)
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).DataItemKaiteiNo))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).DataItemAreaName))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).DataItemSetName))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).DataItemKaiteiInfo))
                            .AppendFormat(FMT_INT, koseiMatrix(index).ShisakuBuhinHi)
                            .AppendFormat(FMT_INT, koseiMatrix(index).ShisakuKataHi)
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).Bikou))
                            .AppendFormat(FMT_STR, StringUtil.Nvl(koseiMatrix(index).BuhinNote))
                            .AppendFormat(FMT_INT, koseiMatrix(index).EditTourokubi)
                            .AppendFormat(FMT_INT, koseiMatrix(index).EditTourokujikan)
                            .AppendFormat(FMT_STR, koseiMatrix(index).KaiteiHandanFlg)
                            .AppendFormat(FMT_STR, koseiMatrix(index).ShisakuListCode)
                            .AppendFormat(FMT_STR, LoginInfo.Now.UserId)
                            .AppendFormat(FMT_STR, aDate.CurrentDateDbFormat)
                            .AppendFormat(FMT_STR, aDate.CurrentTimeDbFormat)
                            .AppendFormat(FMT_STR, LoginInfo.Now.UserId)
                            .AppendFormat(FMT_STR, aDate.CurrentDateDbFormat)
                            .AppendFormat(FMT_STR, aDate.CurrentTimeDbFormat)
                            .AppendFormat(FMT_INT, buhinNoHyoujiJun + MaxUpdateBuhinNoHyoujijun)
                            .AppendLine(" ) ")

                        End With
                        'sqlBase = _
                        '    " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE " _
                        '     & " WITH (UPDLOCK) " _
                        '     & " ( " _
                        '     & " SHISAKU_EVENT_CODE, " _
                        '     & " SHISAKU_BUKA_CODE, " _
                        '     & " SHISAKU_BLOCK_NO, " _
                        '     & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                        '     & " BUHIN_NO_HYOUJI_JUN, " _
                        '     & " LEVEL, " _
                        '     & " SHUKEI_CODE, " _
                        '     & " SIA_SHUKEI_CODE, " _
                        '     & " GENCYO_CKD_KBN, " _
                        '     & " KYOUKU_SECTION, " _
                        '     & " MAKER_CODE, " _
                        '     & " MAKER_NAME, " _
                        '     & " BUHIN_NO, " _
                        '     & " BUHIN_NO_KBN, " _
                        '     & " BUHIN_NO_KAITEI_NO, " _
                        '     & " EDA_BAN, " _
                        '     & " BUHIN_NAME, " _
                        '     & " SAISHIYOUFUKA, " _
                        '     & " SHUTUZU_YOTEI_DATE, " _
                        '     & " BASE_BUHIN_FLG, " _
                        '     & " ZAISHITU_KIKAKU_1, " _
                        '     & " ZAISHITU_KIKAKU_2, " _
                        '     & " ZAISHITU_KIKAKU_3, " _
                        '     & " ZAISHITU_MEKKI, " _
                        '     & " TSUKURIKATA_SEISAKU, " _
                        '     & " TSUKURIKATA_KATASHIYOU_1, " _
                        '     & " TSUKURIKATA_KATASHIYOU_2, " _
                        '     & " TSUKURIKATA_KATASHIYOU_3, " _
                        '     & " TSUKURIKATA_TIGU, " _
                        '     & " TSUKURIKATA_NOUNYU, " _
                        '     & " TSUKURIKATA_KIBO, " _
                        '     & " SHISAKU_BANKO_SURYO, " _
                        '     & " SHISAKU_BANKO_SURYO_U, " _
                        '     & " MATERIAL_INFO_LENGTH, " _
                        '     & " MATERIAL_INFO_WIDTH, " _
                        '     & " DATA_ITEM_KAITEI_NO, " _
                        '     & " DATA_ITEM_AREA_NAME, " _
                        '     & " DATA_ITEM_SET_NAME, " _
                        '     & " DATA_ITEM_KAITEI_INFO, " _
                        '     & " SHISAKU_BUHIN_HI, " _
                        '     & " SHISAKU_KATA_HI, " _
                        '     & " BIKOU, " _
                        '     & " BUHIN_NOTE, " _
                        '     & " EDIT_TOUROKUBI, " _
                        '     & " EDIT_TOUROKUJIKAN, " _
                        '     & " KAITEI_HANDAN_FLG, " _
                        '     & " SHISAKU_LIST_CODE, " _
                        '     & " CREATED_USER_ID, " _
                        '     & " CREATED_DATE, " _
                        '     & " CREATED_TIME, " _
                        '     & " UPDATED_USER_ID, " _
                        '     & " UPDATED_DATE, " _
                        '     & " UPDATED_TIME " _
                        '     & " ,BASE_BUHIN_SEQ" _
                        '     & " ) " _
                        '     & "VALUES ( " _
                        '         & "'" & shisakuEventCode & "', " _
                        '         & "'" & shisakuBukaCode & "', " _
                        '     & "'" & shisakuBlockNo & "', " _
                        '     & "'" & shisakuBlockNoKaiteiNo & "', "
                        '    sqlBase = sqlBase & buhinNoHyoujiJun + MaxUpdateBuhinNoHyoujijun & ", "
                        '    sqlBase = sqlBase _
                        '         & koseiMatrix(index).Level & ", " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).ShukeiCode) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).SiaShukeiCode) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).GencyoCkdKbn) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).KyoukuSection) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).MakerCode) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).MakerName) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).BuhinNo) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).BuhinNoKbn) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).BuhinNoKaiteiNo) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).EdaBan) & "', " _
                        '             & "'" & Trim(StringUtil.Nvl(koseiMatrix(index).BuhinName)) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).Saishiyoufuka) & "', " _
                        '             & koseiMatrix(index).ShutuzuYoteiDate & ", " _
                        '             & "'1', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).ZaishituKikaku1) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).ZaishituKikaku2) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).ZaishituKikaku3) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).ZaishituMekki) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataSeisaku) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataKatashiyou1) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataKatashiyou2) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataKatashiyou3) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataTigu) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataNounyu) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).TsukurikataKibo) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).ShisakuBankoSuryo) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).ShisakuBankoSuryoU) & "', " _
                        '             & koseiMatrix(index).MaterialInfoLength & ", " _
                        '             & koseiMatrix(index).MaterialInfoWidth & ", " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).DataItemKaiteiNo) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).DataItemAreaName) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).DataItemSetName) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).DataItemKaiteiInfo) & "', " _
                        '             & koseiMatrix(index).ShisakuBuhinHi & ", " _
                        '             & koseiMatrix(index).ShisakuKataHi & ", " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).Bikou) & "', " _
                        '             & "'" & StringUtil.Nvl(koseiMatrix(index).BuhinNote) & "'," _
                        '             & koseiMatrix(index).EditTourokubi & ", " _
                        '             & koseiMatrix(index).EditTourokujikan & ", " _
                        '             & "'" & koseiMatrix(index).KaiteiHandanFlg & "', " _
                        '             & "'" & koseiMatrix(index).ShisakuListCode & "', " _
                        '             & "'" & LoginInfo.Now.UserId & "', " _
                        '             & "'" & aDate.CurrentDateDbFormat & "', " _
                        '             & "'" & aDate.CurrentTimeDbFormat & "', " _
                        '             & "'" & LoginInfo.Now.UserId & "', " _
                        '             & "'" & aDate.CurrentDateDbFormat & "', " _
                        '             & "'" & aDate.CurrentTimeDbFormat & "' "
                        '    sqlBase = sqlBase & "," & buhinNoHyoujiJun + MaxUpdateBuhinNoHyoujijun
                        '    sqlBase = sqlBase & " ) "
                    Else
                        'sqlBase = ""
                    End If

                    db.ExecuteNonQuery(sql.ToString & sqlBase.ToString)

                    '表示順を採番
                    buhinNoHyoujiJun = buhinNoHyoujiJun + 1
                Next

                db.Commit()
            End Using

            'Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            '    db.Open()
            '    db.BeginTransaction()
            '    For index As Integer = 0 To buhinNoHyoujiJun - 1
            '        If Not sqlHairetu(index) Is Nothing Then
            '            db.ExecuteNonQuery(sqlHairetu(index))
            '        End If
            '    Next
            '    db.Commit()
            'End Using

            '配列クリア
            'Array.Clear(sqlHairetu, 0, sqlHairetu.Length)
        End Sub

        ''' <summary>
        ''' 部品表編集と部品編集ベースにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <param name="JikyuUmu">自給品の有無</param>
        ''' <param name="TsukurikataTenkaiFlg">作り方フラグ</param>
        ''' <remarks></remarks>
        Public Sub InsertBySekkeiBuhinEdit(ByVal shisakuEventCode As String, _
                                     ByVal shisakuBukaCode As String, _
                                     ByVal shisakuBlockNo As String, _
                                     ByVal shisakuBlockNoKaiteiNo As String, _
                                     ByVal koseiMatrix As BuhinKoseiMatrix, _
                                     ByVal JikyuUmu As String, _
                                     ByVal TsukurikataTenkaiFlg As String, _
                                     ByVal login As LoginInfo) Implements BuhinEditBaseDao.InsertBySekkeiBuhinEdit
            ' ShisakuDateをnewする
            Me.InsertBySekkeiBuhinEdit(shisakuEventCode, _
                                     shisakuBukaCode, _
                                     shisakuBlockNo, _
                                     shisakuBlockNoKaiteiNo, _
                                     koseiMatrix, _
                                     JikyuUmu, _
                                     TsukurikataTenkaiFlg, _
                                     login, _
                                     New ShisakuDate)

        End Sub


        ''' <summary>
        ''' 部品表編集INSTLと部品編集ベースINSTLにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <remarks></remarks>
        Public Sub InsertBySekkeiBuhinEditInstl(ByVal shisakuEventCode As String, _
                                         ByVal shisakuBukaCode As String, _
                                         ByVal shisakuBlockNo As String, _
                                         ByVal shisakuBlockNoKaiteiNo As String, _
                                         ByVal koseiMatrix As BuhinKoseiMatrix, _
                                         ByVal login As LoginInfo) Implements BuhinEditBaseDao.InsertBySekkeiBuhinEditInstl
            Me.InsertBySekkeiBuhinEditInstl(shisakuEventCode, _
                                         shisakuBukaCode, _
                                         shisakuBlockNo, _
                                         shisakuBlockNoKaiteiNo, _
                                         koseiMatrix, _
                                         login, _
                                         New ShisakuDate)

        End Sub
        ''' <summary>
        ''' 部品表編集INSTLと部品編集ベースINSTLにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <param name="aDate">試作日</param>
        ''' <remarks></remarks>
        Public Sub InsertBySekkeiBuhinEditInstl(ByVal shisakuEventCode As String, _
                                         ByVal shisakuBukaCode As String, _
                                         ByVal shisakuBlockNo As String, _
                                         ByVal shisakuBlockNoKaiteiNo As String, _
                                         ByVal koseiMatrix As BuhinKoseiMatrix, _
                                         ByVal login As LoginInfo, _
                                         ByVal aDate As ShisakuDate, Optional ByVal MaxUpdateBuhinNoHyoujijun As Integer = 0, Optional ByVal MaxUpdateInstlHyoujijun As Integer = 0, Optional ByVal instlTitle As BuhinEditKoseiInstlTitle = Nothing) Implements BuhinEditBaseDao.InsertBySekkeiBuhinEditInstl
            Dim instlHinbanHyoujiJun As Integer = 0

            '縦'
            Dim row As Integer = 0
            Dim col As Integer = 0

            ''↓↓2014/09/15 1 ベース部品表作成表機能増強 酒井ADD BEGIN
            Dim buhinNoHyuojijun As Integer = MaxUpdateBuhinNoHyoujijun
            Dim InstlHyoujijun As Integer
            ''↑↑2014/09/15 1 ベース部品表作成表機能増強 酒井ADD END

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.BeginTransaction()

                For rowindex As Integer = 0 To koseiMatrix.GetInputRowIndexes.Count - 1

                    'If koseiMatrix(rowindex).Level = 0 Then
                    '    Dim sql As String
                    'End If
                    'koseiMatrix.GetInputInsuColumnIndexes.Count

                    '部品番号がNULLの項目が存在している'
                    If StringUtil.IsEmpty(koseiMatrix(rowindex).BuhinNo) Then
                        Continue For
                    End If

                    '自給品の削除'
                    'If StringUtil.IsEmpty(koseiMatrix(rowindex).ShukeiCode) Then
                    '    If StringUtil.Equals(koseiMatrix(rowindex).SiaShukeiCode, "J") Then
                    '        Continue For
                    '    End If
                    'ElseIf StringUtil.Equals(koseiMatrix(rowindex).ShukeiCode, "J") Then
                    '    Continue For
                    'End If

                    '横'
                    For columnIndex As Integer = 0 To koseiMatrix.GetInputInsuColumnIndexes.Count - 1

                        Dim param As New TShisakuBuhinEditInstlVo

                        'Nothingの項目を飛ばす
                        If koseiMatrix.InsuSuryo(rowindex, columnIndex) Is Nothing Then
                            Continue For
                        End If

                        ''↓↓2014/09/15 1 ベース部品表作成表機能増強 酒井ADD BEGIN
                        If MaxUpdateInstlHyoujijun > 0 Then
                            '↓↓2014/10/10 酒井 ADD BEGIN
                            'Dim sbiVo As TShisakuSekkeiBlockInstlVo = FindByInstlHinbanBaseEbom(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, instlTitle.InstlHinban(columnIndex), instlTitle.InstlHinbanKbn(columnIndex))
                            Dim sbiVo As TShisakuSekkeiBlockInstlVo = FindByInstlHinbanBaseEbom(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, instlTitle.InstlHinban(columnIndex), instlTitle.InstlHinbanKbn(columnIndex), instlTitle.InstlDataKbn(columnIndex), instlTitle.BaseInstlFlg(columnIndex))
                            '↑↑2014/10/10 酒井 ADD END
                            InstlHyoujijun = sbiVo.InstlHinbanHyoujiJun
                        Else
                            InstlHyoujijun = columnIndex
                        End If
                        ''↑↑2014/09/15 1 ベース部品表作成表機能増強 酒井ADD END

                        '試作部品編集INSTL情報
                        ''↓↓2014/08/19 Ⅰ.5.EBOM差分出力 bb) (TES)張 CHG START
                        ''夜間展開するかどうかの判断を追加
                        Dim sql As String
                        'Dim sql As String = _
                        '" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL " _
                        If login IsNot Nothing Then
                            sql = " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL "
                        Else
                            sql = " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_EBOM_KANSHI "
                        End If
                        ''↑↑2014/08/19 Ⅰ.5.EBOM差分出力 bb) (TES)張 CHG END
                        sql = sql & " WITH (UPDLOCK) " _
                        & " ( " _
                        & " SHISAKU_EVENT_CODE, " _
                        & " SHISAKU_BUKA_CODE, " _
                        & " SHISAKU_BLOCK_NO, " _
                        & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                        & " BUHIN_NO_HYOUJI_JUN, " _
                        & " INSTL_HINBAN_HYOUJI_JUN, " _
                        & " INSU_SURYO, " _
                        & " SAISYU_KOUSHINBI, " _
                        & " CREATED_USER_ID, " _
                        & " CREATED_DATE, " _
                        & " CREATED_TIME, " _
                        & " UPDATED_USER_ID, " _
                        & " UPDATED_DATE, " _
                        & " UPDATED_TIME " _
                        & " ) " _
                        & " VALUES ( " _
                        & "'" & shisakuEventCode & "', " _
                        & "'" & shisakuBukaCode & "', " _
                    & "'" & shisakuBlockNo & "', " _
                    & "'" & shisakuBlockNoKaiteiNo & "', "
                        ''↓↓2014/09/12 1 ベース部品表作成表機能増強 酒井ADD BEGIN
                        '& "'" & shisakuBlockNoKaiteiNo & "', " _
                        '& rowindex & ", " _
                        '& columnIndex & ", " _
                        If login IsNot Nothing Then
                            sql = sql _
                                & buhinNoHyuojijun & ", " _
                                & InstlHyoujijun & ", "
                        Else
                            sql = sql _
                                & rowindex & ", " _
                                & columnIndex & ", "
                        End If
                        ''↑↑2014/09/12 1 ベース部品表作成表機能増強 酒井ADD END
                        sql = sql _
                            & koseiMatrix.InsuSuryo(rowindex, columnIndex) & ", " _
                            & "'" & Integer.Parse(Replace(aDate.CurrentDateDbFormat, "-", "")) & "', " _
                            & "'" & LoginInfo.Now.UserId & "', " _
                            & "'" & aDate.CurrentDateDbFormat & "', " _
                            & "'" & aDate.CurrentTimeDbFormat & "', " _
                            & "'" & LoginInfo.Now.UserId & "', " _
                            & "'" & aDate.CurrentDateDbFormat & "', " _
                            & "'" & aDate.CurrentTimeDbFormat & "' " _
                            & " ) "

                        '試作部品編集INSTL情報（ベース）
                        ''↓↓2014/08/19 Ⅰ.5.EBOM差分出力 bb) (TES)張 CHG START
                        ''夜間展開するかどうかの判断を追加
                        Dim sqlBase As String
                        If login IsNot Nothing Then
                            sqlBase = " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE " _
                                & " WITH (UPDLOCK) " _
                                & " ( " _
                                & " SHISAKU_EVENT_CODE, " _
                                & " SHISAKU_BUKA_CODE, " _
                                & " SHISAKU_BLOCK_NO, " _
                                & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                                & " BUHIN_NO_HYOUJI_JUN, " _
                                & " INSTL_HINBAN_HYOUJI_JUN, " _
                                & " INSU_SURYO, " _
                                & " SAISYU_KOUSHINBI, " _
                                & " CREATED_USER_ID, " _
                                & " CREATED_DATE, " _
                                & " CREATED_TIME, " _
                                & " UPDATED_USER_ID, " _
                                & " UPDATED_DATE, " _
                                & " UPDATED_TIME " _
                                & " ) " _
                                & " VALUES ( " _
                                & "'" & shisakuEventCode & "', " _
                                & "'" & shisakuBukaCode & "', " _
                            & "'" & shisakuBlockNo & "', " _
                            & "'" & shisakuBlockNoKaiteiNo & "', "
                            ''↓↓2014/09/12 1 ベース部品表作成表機能増強 酒井ADD BEGIN
                            '& "'" & shisakuBlockNoKaiteiNo & "', " _
                            '& rowindex & ", " _
                            '& columnIndex & ", " _
                            '& koseiMatrix.InsuSuryo(rowindex, columnIndex) & ", " _
                            sqlBase = sqlBase _
                                & buhinNoHyuojijun & ", " _
                                & InstlHyoujijun & ", "
                            ''↑↑2014/09/12 1 ベース部品表作成表機能増強 酒井ADD END
                            sqlBase = sqlBase _
                                    & koseiMatrix.InsuSuryo(rowindex, columnIndex) & ", " _
                                    & "'" & Integer.Parse(Replace(aDate.CurrentDateDbFormat, "-", "")) & "', " _
                                    & "'" & LoginInfo.Now.UserId & "', " _
                                    & "'" & aDate.CurrentDateDbFormat & "', " _
                                    & "'" & aDate.CurrentTimeDbFormat & "', " _
                                    & "'" & LoginInfo.Now.UserId & "', " _
                                    & "'" & aDate.CurrentDateDbFormat & "', " _
                                    & "'" & aDate.CurrentTimeDbFormat & "' " _
                                    & " ) "
                        Else
                            sqlBase = ""
                        End If
                        ''↑↑2014/08/19 Ⅰ.5.EBOM差分出力 bb) (TES)張 CHG END

                        '本体とベースを合体
                        'sqlHairetu(columnIndex) = sql + sqlBase
                        db.ExecuteNonQuery(sql + sqlBase)

                        instlHinbanHyoujiJun = columnIndex

                    Next

                    'Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                    '    db.Open()
                    '    db.BeginTransaction()
                    '    For index As Integer = 0 To instlHinbanHyoujiJun
                    '        If Not StringUtil.IsEmpty(sqlHairetu(index)) Then
                    '            db.ExecuteNonQuery(sqlHairetu(index))
                    '        End If
                    '    Next
                    '    db.Commit()
                    'End Using

                    '配列クリア
                    'Array.Clear(sqlHairetu, 0, sqlHairetu.Length)

                    row = row + 1
                    ''↓↓2014/09/15 1 ベース部品表作成表機能増強 酒井ADD BEGIN
                    buhinNoHyuojijun = buhinNoHyuojijun + 1
                    ''↑↑2014/09/15 1 ベース部品表作成表機能増強 酒井ADD END
                Next

                db.Commit()
            End Using

        End Sub
        ''' <summary>
        ''' 部品表編集と部品編集ベースにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <param name="JikyuUmu">自給品の有無</param>
        ''' <param name="TsukurikataTenkaiFlg">作り方フラグ</param>
        ''' <remarks></remarks>
        Public Sub InsertBySekkeiBuhinEditEvent(ByVal shisakuEventCode As String, _
                                                ByVal shisakuBukaCode As String, _
                                                ByVal shisakuBlockNo As String, _
                                                ByVal shisakuBlockNoKaiteiNo As String, _
                                                ByVal koseiMatrix As BuhinKoseiMatrix, _
                                                ByVal JikyuUmu As String, _
                                                ByVal TsukurikataTenkaiFlg As String) Implements BuhinEditBaseDao.InsertBySekkeiBuhinEditEvent
            Me.InsertBySekkeiBuhinEditEvent(shisakuEventCode, _
                                                 shisakuBukaCode, _
                                                 shisakuBlockNo, _
                                                 shisakuBlockNoKaiteiNo, _
                                                 koseiMatrix, _
                                                 JikyuUmu, _
                                                 TsukurikataTenkaiFlg, _
                                                 New ShisakuDate)
        End Sub
        ''' <summary>
        ''' 部品表編集と部品編集ベースにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <param name="JikyuUmu">自給品の有無</param>
        ''' <param name="TsukurikataTenkaiFlg">作り方フラグ</param>
        ''' <param name="aDate">試作日</param>
        ''' <remarks></remarks>
        Public Sub InsertBySekkeiBuhinEditEvent(ByVal shisakuEventCode As String, _
                                                ByVal shisakuBukaCode As String, _
                                                ByVal shisakuBlockNo As String, _
                                                ByVal shisakuBlockNoKaiteiNo As String, _
                                                ByVal koseiMatrix As BuhinKoseiMatrix, _
                                                ByVal JikyuUmu As String, _
                                                ByVal TsukurikataTenkaiFlg As String, _
                                                ByVal aDate As ShisakuDate, Optional ByRef MaxUpdateBuhinNoHyoujijun As Integer = 0) Implements BuhinEditBaseDao.InsertBySekkeiBuhinEditEvent
            Dim param As New TShisakuBuhinEditVo
            Dim buhinNoHyoujiJun As Integer = 0
            'Dim aDate As New ShisakuDate
            'Dim BEVos As New List(Of TShisakuBuhinEditVo)
            'Dim BEBVos As New List(Of TShisakuBuhinEditBaseVo)


            '配列定義
            'Dim sqlHairetu(5000) As String

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.BeginTransaction()

                '先にレベル0'
                For index As Integer = 0 To koseiMatrix.GetInputRowIndexes.Count - 1

                    '部品番号がNULLの項目が存在している'
                    If StringUtil.IsEmpty(koseiMatrix(index).BuhinNo) Then
                        Continue For
                    End If

                    '自給品の削除'
                    If StringUtil.Equals(JikyuUmu, "0") Then
                        If StringUtil.IsEmpty(koseiMatrix(index).ShukeiCode) Then
                            If StringUtil.Equals(koseiMatrix(index).SiaShukeiCode, "J") Then
                                Continue For
                            End If
                        ElseIf StringUtil.Equals(koseiMatrix(index).ShukeiCode, "J") Then
                            Continue For
                        End If
                    End If

                    ''↓↓2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD BEGIN
                    '作り方
                    If StringUtil.Equals(TsukurikataTenkaiFlg, "0") Then
                        koseiMatrix(index).TsukurikataSeisaku = ""
                        koseiMatrix(index).TsukurikataKatashiyou1 = ""
                        koseiMatrix(index).TsukurikataKatashiyou2 = ""
                        koseiMatrix(index).TsukurikataKatashiyou3 = ""
                        koseiMatrix(index).TsukurikataTigu = ""
                        ''↓↓2014/08/19 Ⅰ.2.管理項目追加_ad) 酒井 ADD BEGIN
                        koseiMatrix(index).TsukurikataNounyu = 0
                        ''↑↑2014/08/19 Ⅰ.2.管理項目追加_ad) 酒井 ADD END
                        koseiMatrix(index).TsukurikataKibo = ""
                    End If
                    ''↑↑2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD END


                    '出図予定日が99999999の場合、0を設定する。
                    If koseiMatrix(index).ShutuzuYoteiDate = 99999999 Then
                        koseiMatrix(index).ShutuzuYoteiDate = 0
                    End If
                    If StringUtil.IsEmpty(koseiMatrix(index).ShutuzuYoteiDate) Then
                        koseiMatrix(index).ShutuzuYoteiDate = 0
                    End If


                    ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
                    'Nothingなら０を設定
                    If StringUtil.IsEmpty(koseiMatrix(index).MaterialInfoLength) Then
                        koseiMatrix(index).MaterialInfoLength = 0
                    End If
                    If StringUtil.IsEmpty(koseiMatrix(index).MaterialInfoWidth) Then
                        koseiMatrix(index).MaterialInfoWidth = 0
                    End If
                    ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


                    'Nothingなら０を設定
                    If StringUtil.IsEmpty(koseiMatrix(index).ShisakuBuhinHi) Then
                        koseiMatrix(index).ShisakuBuhinHi = 0
                    End If
                    If StringUtil.IsEmpty(koseiMatrix(index).ShisakuKataHi) Then
                        koseiMatrix(index).ShisakuKataHi = 0
                    End If
                    If StringUtil.IsEmpty(koseiMatrix(index).EditTourokubi) Then
                        koseiMatrix(index).EditTourokubi = 0
                    End If
                    If StringUtil.IsEmpty(koseiMatrix(index).EditTourokujikan) Then
                        koseiMatrix(index).EditTourokujikan = 0
                    End If



                    '試作部品編集情報
                    '2012/01/23 供給セクション追加
                    Dim sb As New StringBuilder
                    With sb
                        .Remove(0, .Length)
                        .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT ")
                        .AppendLine(" WITH (UPDLOCK) ")
                        .AppendLine(" ( ")
                        .AppendLine(" SHISAKU_EVENT_CODE, ")
                        .AppendLine(" SHISAKU_BUKA_CODE, ")
                        .AppendLine(" SHISAKU_BLOCK_NO, ")
                        .AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO, ")
                        .AppendLine(" BUHIN_NO_HYOUJI_JUN, ")
                        .AppendLine(" LEVEL, ")
                        .AppendLine(" SHUKEI_CODE, ")
                        .AppendLine(" SIA_SHUKEI_CODE, ")
                        .AppendLine(" GENCYO_CKD_KBN, ")
                        .AppendLine(" KYOUKU_SECTION, ")
                        .AppendLine(" MAKER_CODE, ")
                        .AppendLine(" MAKER_NAME, ")
                        .AppendLine(" BUHIN_NO, ")
                        .AppendLine(" BUHIN_NO_KBN, ")
                        .AppendLine(" BUHIN_NO_KAITEI_NO, ")
                        .AppendLine(" EDA_BAN, ")
                        .AppendLine(" BUHIN_NAME, ")
                        .AppendLine(" SAISHIYOUFUKA, ")
                        .AppendLine(" SHUTUZU_YOTEI_DATE, ")
                        .AppendLine(" BASE_BUHIN_FLG, ")
                        .AppendLine(" ZAISHITU_KIKAKU_1, ")
                        .AppendLine(" ZAISHITU_KIKAKU_2, ")
                        .AppendLine(" ZAISHITU_KIKAKU_3, ")
                        .AppendLine(" ZAISHITU_MEKKI, ")
                        .AppendLine(" TSUKURIKATA_SEISAKU, ")
                        .AppendLine(" TSUKURIKATA_KATASHIYOU_1, ")
                        .AppendLine(" TSUKURIKATA_KATASHIYOU_2, ")
                        .AppendLine(" TSUKURIKATA_KATASHIYOU_3, ")
                        .AppendLine(" TSUKURIKATA_TIGU, ")
                        .AppendLine(" TSUKURIKATA_NOUNYU, ")
                        .AppendLine(" TSUKURIKATA_KIBO, ")
                        .AppendLine(" SHISAKU_BANKO_SURYO, ")
                        .AppendLine(" SHISAKU_BANKO_SURYO_U, ")


                        ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
                        .AppendLine(" MATERIAL_INFO_LENGTH, ")
                        .AppendLine(" MATERIAL_INFO_WIDTH, ")
                        .AppendLine(" DATA_ITEM_KAITEI_NO, ")
                        .AppendLine(" DATA_ITEM_AREA_NAME, ")
                        .AppendLine(" DATA_ITEM_SET_NAME, ")
                        .AppendLine(" DATA_ITEM_KAITEI_INFO, ")
                        ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


                        .AppendLine(" SHISAKU_BUHIN_HI, ")
                        .AppendLine(" SHISAKU_KATA_HI, ")
                        .AppendLine(" BIKOU, ")
                        .AppendLine(" BUHIN_NOTE, ")
                        .AppendLine(" EDIT_TOUROKUBI, ")
                        .AppendLine(" EDIT_TOUROKUJIKAN, ")
                        .AppendLine(" KAITEI_HANDAN_FLG, ")
                        .AppendLine(" SHISAKU_LIST_CODE, ")
                        .AppendLine(" CREATED_USER_ID, ")
                        .AppendLine(" CREATED_DATE, ")
                        .AppendLine(" CREATED_TIME, ")
                        .AppendLine(" UPDATED_USER_ID, ")
                        .AppendLine(" UPDATED_DATE, ")
                        .AppendLine(" UPDATED_TIME ")

                        ''2015/09/03 追加 E.Ubukata
                        .AppendLine(" ,BASE_BUHIN_SEQ")

                        .AppendLine(" ) ")
                        .AppendLine("VALUES ( ")
                        .AppendLine("'" & shisakuEventCode & "', ")
                        .AppendLine("'" & shisakuBukaCode & "', ")
                        .AppendLine("'" & shisakuBlockNo & "', ")
                        .AppendLine("'" & shisakuBlockNoKaiteiNo & "', ")
                        .AppendLine(buhinNoHyoujiJun & ", ")
                        .AppendLine(koseiMatrix(index).Level & ", ")
                        .AppendLine("'" & koseiMatrix(index).ShukeiCode & "', ")
                        .AppendLine("'" & koseiMatrix(index).SiaShukeiCode & "', ")
                        .AppendLine("'" & koseiMatrix(index).GencyoCkdKbn & "', ")
                        .AppendLine("'" & koseiMatrix(index).KyoukuSection & "', ")
                        .AppendLine("'" & koseiMatrix(index).MakerCode & "', ")
                        .AppendLine("'" & Trim(koseiMatrix(index).MakerName) & "', ")
                        .AppendLine("'" & koseiMatrix(index).BuhinNo & "', ")
                        .AppendLine("'" & koseiMatrix(index).BuhinNoKbn & "', ")
                        .AppendLine("'" & koseiMatrix(index).BuhinNoKaiteiNo & "', ")
                        .AppendLine("'" & koseiMatrix(index).EdaBan & "', ")
                        .AppendLine("'" & Trim(koseiMatrix(index).BuhinName) & "', ")

                        '.AppendLine("'" & koseiMatrix(index).Saishiyoufuka & "', ")
                        .AppendLine("'', ")

                        .AppendLine(koseiMatrix(index).ShutuzuYoteiDate & ", ")
                        ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_i) (TES)張 ADD BEGIN
                        .AppendLine("'1', ")
                        ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_i) (TES)張 ADD END
                        .AppendLine("'" & koseiMatrix(index).ZaishituKikaku1 & "', ")
                        .AppendLine("'" & koseiMatrix(index).ZaishituKikaku2 & "', ")
                        .AppendLine("'" & koseiMatrix(index).ZaishituKikaku3 & "', ")
                        .AppendLine("'" & koseiMatrix(index).ZaishituMekki & "', ")

                        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD BEGIN
                        .AppendLine("'" & koseiMatrix(index).TsukurikataSeisaku & "', ")
                        .AppendLine("'" & koseiMatrix(index).TsukurikataKatashiyou1 & "', ")
                        .AppendLine("'" & koseiMatrix(index).TsukurikataKatashiyou2 & "', ")
                        '20140818 Sakai Add
                        .AppendLine("'" & koseiMatrix(index).TsukurikataKatashiyou3 & "', ")
                        .AppendLine("'" & koseiMatrix(index).TsukurikataTigu & "', ")
                        .AppendLine("'" & koseiMatrix(index).TsukurikataNounyu & "', ")
                        .AppendLine("'" & koseiMatrix(index).TsukurikataKibo & "', ")
                        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD END

                        .AppendLine("'" & koseiMatrix(index).ShisakuBankoSuryo & "', ")
                        .AppendLine("'" & koseiMatrix(index).ShisakuBankoSuryoU & "', ")


                        ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
                        .AppendLine(koseiMatrix(index).MaterialInfoLength & ", ")
                        .AppendLine(koseiMatrix(index).MaterialInfoWidth & ", ")
                        .AppendLine("'" & koseiMatrix(index).DataItemKaiteiNo & "', ")
                        .AppendLine("'" & koseiMatrix(index).DataItemAreaName & "', ")
                        .AppendLine("'" & koseiMatrix(index).DataItemSetName & "', ")
                        .AppendLine("'" & koseiMatrix(index).DataItemKaiteiInfo & "', ")
                        ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


                        .AppendLine(koseiMatrix(index).ShisakuBuhinHi & ", ")
                        .AppendLine(koseiMatrix(index).ShisakuKataHi & ", ")
                        .AppendLine("'" & koseiMatrix(index).Bikou & "', ")
                        .AppendLine("'" & koseiMatrix(index).BuhinNote & "',")
                        .AppendLine(koseiMatrix(index).EditTourokubi & ", ")
                        .AppendLine(koseiMatrix(index).EditTourokujikan & ", ")
                        .AppendLine("'" & koseiMatrix(index).KaiteiHandanFlg & "', ")
                        .AppendLine("'" & koseiMatrix(index).ShisakuListCode & "', ")
                        .AppendLine("'" & LoginInfo.Now.UserId & "', ")
                        .AppendLine("'" & aDate.CurrentDateDbFormat & "', ")
                        .AppendLine("'" & aDate.CurrentTimeDbFormat & "', ")
                        .AppendLine("'" & LoginInfo.Now.UserId & "', ")
                        .AppendLine("'" & aDate.CurrentDateDbFormat & "', ")
                        .AppendLine("'" & aDate.CurrentTimeDbFormat & "' ")

                        ''2015/09/03 追加 E.Ubukata
                        .AppendFormat(",{0}", buhinNoHyoujiJun)

                        .AppendLine(" ) ")
                    End With
                    'Dim BEVo As TShisakuBuhinEditVo = NitteiDbComFunc.setDefault(Of TShisakuBuhinEditVo)(aDate)
                    'With BEVo
                    '    .ShisakuEventCode = shisakuEventCode
                    '    .ShisakuBukaCode = shisakuBukaCode
                    '    .ShisakuBlockNo = shisakuBlockNo
                    '    .ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
                    '    .BuhinNoHyoujiJun = buhinNoHyoujiJun
                    '    .Level = koseiMatrix(index).Level
                    '    .ShukeiCode = koseiMatrix(index).ShukeiCode
                    '    .SiaShukeiCode = koseiMatrix(index).SiaShukeiCode
                    '    .GencyoCkdKbn = koseiMatrix(index).GencyoCkdKbn
                    '    .KyoukuSection = koseiMatrix(index).KyoukuSection
                    '    .MakerCode = koseiMatrix(index).MakerCode
                    '    .MakerName = Trim(koseiMatrix(index).MakerName)
                    '    .BuhinNo = koseiMatrix(index).BuhinNo
                    '    .BuhinNoKbn = koseiMatrix(index).BuhinNoKbn
                    '    .BuhinNoKaiteiNo = koseiMatrix(index).BuhinNoKaiteiNo
                    '    .EdaBan = koseiMatrix(index).EdaBan
                    '    .BuhinName = Trim(koseiMatrix(index).BuhinName)
                    '    .Saishiyoufuka = ""
                    '    .ShutuzuYoteiDate = koseiMatrix(index).ShutuzuYoteiDate
                    '    .BaseBuhinFlg = "1"
                    '    .ZaishituKikaku1 = koseiMatrix(index).ZaishituKikaku1
                    '    .ZaishituKikaku2 = koseiMatrix(index).ZaishituKikaku2
                    '    .ZaishituKikaku3 = koseiMatrix(index).ZaishituKikaku3
                    '    .TsukurikataSeisaku = koseiMatrix(index).TsukurikataSeisaku
                    '    .TsukurikataKatashiyou1 = koseiMatrix(index).TsukurikataKatashiyou1
                    '    .TsukurikataKatashiyou2 = koseiMatrix(index).TsukurikataKatashiyou2
                    '    .TsukurikataKatashiyou3 = koseiMatrix(index).TsukurikataKatashiyou3
                    '    .TsukurikataTigu = koseiMatrix(index).TsukurikataTigu
                    '    .TsukurikataNounyu = koseiMatrix(index).TsukurikataNounyu
                    '    .TsukurikataKibo = koseiMatrix(index).TsukurikataKibo
                    '    .ShisakuBankoSuryo = koseiMatrix(index).ShisakuBankoSuryo
                    '    .ShisakuBankoSuryoU = koseiMatrix(index).ShisakuBankoSuryoU
                    '    .ShisakuBuhinHi = koseiMatrix(index).ShisakuBuhinHi
                    '    .ShisakuKataHi = koseiMatrix(index).ShisakuKataHi
                    '    .Bikou = koseiMatrix(index).Bikou
                    '    .BuhinNote = koseiMatrix(index).BuhinNote
                    '    .EditTourokubi = koseiMatrix(index).EditTourokubi
                    '    .EditTourokujikan = koseiMatrix(index).EditTourokujikan
                    '    .KaiteiHandanFlg = koseiMatrix(index).KaiteiHandanFlg
                    '    .ShisakuListCode = koseiMatrix(index).ShisakuListCode
                    'End With
                    'BEVos.Add(BEVo)



                    '試作部品編集情報（ベース）
                    Dim sb2 As New StringBuilder
                    With sb2
                        .Remove(0, .Length)
                        .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE ")
                        .AppendLine(" WITH (UPDLOCK) ")
                        .AppendLine(" ( ")
                        .AppendLine(" SHISAKU_EVENT_CODE, ")
                        .AppendLine(" SHISAKU_BUKA_CODE, ")
                        .AppendLine(" SHISAKU_BLOCK_NO, ")
                        .AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO, ")
                        .AppendLine(" BUHIN_NO_HYOUJI_JUN, ")
                        .AppendLine(" LEVEL, ")
                        .AppendLine(" SHUKEI_CODE, ")
                        .AppendLine(" SIA_SHUKEI_CODE, ")
                        .AppendLine(" GENCYO_CKD_KBN, ")
                        .AppendLine(" KYOUKU_SECTION, ")
                        .AppendLine(" MAKER_CODE, ")
                        .AppendLine(" MAKER_NAME, ")
                        .AppendLine(" BUHIN_NO, ")
                        .AppendLine(" BUHIN_NO_KBN, ")
                        .AppendLine(" BUHIN_NO_KAITEI_NO, ")
                        .AppendLine(" EDA_BAN, ")
                        .AppendLine(" BUHIN_NAME, ")
                        .AppendLine(" SAISHIYOUFUKA, ")
                        .AppendLine(" SHUTUZU_YOTEI_DATE, ")
                        ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_i) (TES)張 ADD BEGIN
                        .AppendLine(" BASE_BUHIN_FLG, ")
                        ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_i) (TES)張 ADD END
                        .AppendLine(" ZAISHITU_KIKAKU_1, ")
                        .AppendLine(" ZAISHITU_KIKAKU_2, ")
                        .AppendLine(" ZAISHITU_KIKAKU_3, ")
                        .AppendLine(" ZAISHITU_MEKKI, ")

                        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD BEGIN
                        .AppendLine(" TSUKURIKATA_SEISAKU, ")
                        '20140818 Sakai Add
                        '.AppendLine(" TSUKURIKATA_KOUHOU, ")
                        '.AppendLine(" TSUKURIKATA_KATASHIYOU, ")
                        .AppendLine(" TSUKURIKATA_KATASHIYOU_1, ")
                        .AppendLine(" TSUKURIKATA_KATASHIYOU_2, ")
                        .AppendLine(" TSUKURIKATA_KATASHIYOU_3, ")
                        .AppendLine(" TSUKURIKATA_TIGU, ")
                        .AppendLine(" TSUKURIKATA_NOUNYU, ")
                        .AppendLine(" TSUKURIKATA_KIBO, ")
                        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD END

                        .AppendLine(" SHISAKU_BANKO_SURYO, ")
                        .AppendLine(" SHISAKU_BANKO_SURYO_U, ")


                        ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
                        .AppendLine(" MATERIAL_INFO_LENGTH, ")
                        .AppendLine(" MATERIAL_INFO_WIDTH, ")
                        .AppendLine(" DATA_ITEM_KAITEI_NO, ")
                        .AppendLine(" DATA_ITEM_AREA_NAME, ")
                        .AppendLine(" DATA_ITEM_SET_NAME, ")
                        .AppendLine(" DATA_ITEM_KAITEI_INFO, ")
                        ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


                        .AppendLine(" SHISAKU_BUHIN_HI, ")
                        .AppendLine(" SHISAKU_KATA_HI, ")
                        .AppendLine(" BIKOU, ")
                        .AppendLine(" BUHIN_NOTE, ")
                        .AppendLine(" EDIT_TOUROKUBI, ")
                        .AppendLine(" EDIT_TOUROKUJIKAN, ")
                        .AppendLine(" KAITEI_HANDAN_FLG, ")
                        .AppendLine(" SHISAKU_LIST_CODE, ")
                        .AppendLine(" CREATED_USER_ID, ")
                        .AppendLine(" CREATED_DATE, ")
                        .AppendLine(" CREATED_TIME, ")
                        .AppendLine(" UPDATED_USER_ID, ")
                        .AppendLine(" UPDATED_DATE, ")
                        .AppendLine(" UPDATED_TIME ")

                        ''2015/09/03 追加 E.Ubukata
                        .AppendLine(" ,BASE_BUHIN_SEQ")

                        .AppendLine(" ) ")
                        .AppendLine("VALUES ( ")
                        .AppendLine("'" & shisakuEventCode & "', ")
                        .AppendLine("'" & shisakuBukaCode & "', ")
                        .AppendLine("'" & shisakuBlockNo & "', ")
                        .AppendLine("'" & shisakuBlockNoKaiteiNo & "', ")
                        .AppendLine(buhinNoHyoujiJun & ", ")
                        .AppendLine(koseiMatrix(index).Level & ", ")
                        .AppendLine("'" & koseiMatrix(index).ShukeiCode & "', ")
                        .AppendLine("'" & koseiMatrix(index).SiaShukeiCode & "', ")
                        .AppendLine("'" & koseiMatrix(index).GencyoCkdKbn & "', ")
                        .AppendLine("'" & koseiMatrix(index).KyoukuSection & "', ")
                        .AppendLine("'" & koseiMatrix(index).MakerCode & "', ")
                        .AppendLine("'" & Trim(koseiMatrix(index).MakerName) & "', ")
                        .AppendLine("'" & koseiMatrix(index).BuhinNo & "', ")
                        .AppendLine("'" & koseiMatrix(index).BuhinNoKbn & "', ")
                        .AppendLine("'" & koseiMatrix(index).BuhinNoKaiteiNo & "', ")
                        .AppendLine("'" & koseiMatrix(index).EdaBan & "', ")
                        .AppendLine("'" & Trim(koseiMatrix(index).BuhinName) & "', ")
                        '.AppendLine("'" & koseiMatrix(index).Saishiyoufuka & "', ")
                        .AppendLine("'', ")
                        ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_i) (TES)張 ADD BEGIN
                        ''↓↓2014/09/04 Ⅰ.3.設計編集 ベース改修専用化_i) 酒井 DEL BEGIN
                        '.AppendLine("'1', ")
                        ''↑↑2014/09/04 Ⅰ.3.設計編集 ベース改修専用化_i) 酒井 DEL END
                        ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_i) (TES)張 ADD END
                        .AppendLine(koseiMatrix(index).ShutuzuYoteiDate & ", ")
                        ''↓↓2014/09/04 Ⅰ.3.設計編集 ベース改修専用化_i) 酒井 ADD BEGIN
                        .AppendLine("'1', ")
                        ''↑↑2014/09/04 Ⅰ.3.設計編集 ベース改修専用化_i) 酒井 ADD END
                        .AppendLine("'" & koseiMatrix(index).ZaishituKikaku1 & "', ")
                        .AppendLine("'" & koseiMatrix(index).ZaishituKikaku2 & "', ")
                        .AppendLine("'" & koseiMatrix(index).ZaishituKikaku3 & "', ")
                        .AppendLine("'" & koseiMatrix(index).ZaishituMekki & "', ")

                        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD BEGIN
                        .AppendLine("'" & koseiMatrix(index).TsukurikataSeisaku & "', ")
                        .AppendLine("'" & koseiMatrix(index).TsukurikataKatashiyou1 & "', ")
                        .AppendLine("'" & koseiMatrix(index).TsukurikataKatashiyou2 & "', ")
                        '20140818 Sakai Add
                        .AppendLine("'" & koseiMatrix(index).TsukurikataKatashiyou3 & "', ")
                        .AppendLine("'" & koseiMatrix(index).TsukurikataTigu & "', ")
                        .AppendLine("'" & koseiMatrix(index).TsukurikataNounyu & "', ")
                        .AppendLine("'" & koseiMatrix(index).TsukurikataKibo & "', ")
                        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_ad) (TES)張 ADD END

                        .AppendLine("'" & koseiMatrix(index).ShisakuBankoSuryo & "', ")
                        .AppendLine("'" & koseiMatrix(index).ShisakuBankoSuryoU & "', ")


                        ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
                        .AppendLine(koseiMatrix(index).MaterialInfoLength & ", ")
                        .AppendLine(koseiMatrix(index).MaterialInfoWidth & ", ")
                        .AppendLine("'" & koseiMatrix(index).DataItemKaiteiNo & "', ")
                        .AppendLine("'" & koseiMatrix(index).DataItemAreaName & "', ")
                        .AppendLine("'" & koseiMatrix(index).DataItemSetName & "', ")
                        .AppendLine("'" & koseiMatrix(index).DataItemKaiteiInfo & "', ")
                        ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


                        .AppendLine(koseiMatrix(index).ShisakuBuhinHi & ", ")
                        .AppendLine(koseiMatrix(index).ShisakuKataHi & ", ")
                        .AppendLine("'" & koseiMatrix(index).Bikou & "', ")
                        .AppendLine("'" & koseiMatrix(index).BuhinNote & "',")
                        .AppendLine(koseiMatrix(index).EditTourokubi & ", ")
                        .AppendLine(koseiMatrix(index).EditTourokujikan & ", ")
                        .AppendLine("'" & koseiMatrix(index).KaiteiHandanFlg & "', ")
                        .AppendLine("'" & koseiMatrix(index).ShisakuListCode & "', ")
                        .AppendLine("'" & LoginInfo.Now.UserId & "', ")
                        .AppendLine("'" & aDate.CurrentDateDbFormat & "', ")
                        .AppendLine("'" & aDate.CurrentTimeDbFormat & "', ")
                        .AppendLine("'" & LoginInfo.Now.UserId & "', ")
                        .AppendLine("'" & aDate.CurrentDateDbFormat & "', ")
                        .AppendLine("'" & aDate.CurrentTimeDbFormat & "' ")

                        ''2015/09/03 追加 E.Ubukata
                        .AppendFormat(",{0}", buhinNoHyoujiJun)

                        .AppendLine(" ) ")
                    End With

                    'Dim BEBVo As TShisakuBuhinEditBaseVo = NitteiDbComFunc.setDefault(Of TShisakuBuhinEditBaseVo)(aDate)
                    'With BEBVo
                    '    .ShisakuEventCode = shisakuEventCode
                    '    .ShisakuBukaCode = shisakuBukaCode
                    '    .ShisakuBlockNo = shisakuBlockNo
                    '    .ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
                    '    .BuhinNoHyoujiJun = buhinNoHyoujiJun
                    '    .Level = koseiMatrix(index).Level
                    '    .ShukeiCode = koseiMatrix(index).ShukeiCode
                    '    .SiaShukeiCode = koseiMatrix(index).SiaShukeiCode
                    '    .GencyoCkdKbn = koseiMatrix(index).GencyoCkdKbn
                    '    .KyoukuSection = koseiMatrix(index).KyoukuSection
                    '    .MakerCode = koseiMatrix(index).MakerCode
                    '    .MakerName = Trim(koseiMatrix(index).MakerName)
                    '    .BuhinNo = koseiMatrix(index).BuhinNo
                    '    .BuhinNoKbn = koseiMatrix(index).BuhinNoKbn
                    '    .BuhinNoKaiteiNo = koseiMatrix(index).BuhinNoKaiteiNo
                    '    .EdaBan = koseiMatrix(index).EdaBan
                    '    .BuhinName = Trim(koseiMatrix(index).BuhinName)
                    '    .Saishiyoufuka = ""
                    '    .ShutuzuYoteiDate = koseiMatrix(index).ShutuzuYoteiDate
                    '    .ZaishituKikaku1 = koseiMatrix(index).ZaishituKikaku1
                    '    .ZaishituKikaku2 = koseiMatrix(index).ZaishituKikaku2
                    '    .ZaishituKikaku3 = koseiMatrix(index).ZaishituKikaku3
                    '    .ZaishituMekki = koseiMatrix(index).ZaishituMekki
                    '    .TsukurikataSeisaku = koseiMatrix(index).TsukurikataSeisaku
                    '    .TsukurikataKatashiyou1 = koseiMatrix(index).TsukurikataKatashiyou1
                    '    .TsukurikataKatashiyou2 = koseiMatrix(index).TsukurikataKatashiyou2
                    '    .TsukurikataKatashiyou3 = koseiMatrix(index).TsukurikataKatashiyou3
                    '    .TsukurikataTigu = koseiMatrix(index).TsukurikataTigu
                    '    .TsukurikataNounyu = koseiMatrix(index).TsukurikataNounyu
                    '    .TsukurikataKibo = koseiMatrix(index).TsukurikataKibo
                    '    .ShisakuBankoSuryo = koseiMatrix(index).ShisakuBankoSuryo
                    '    .ShisakuBankoSuryoU = koseiMatrix(index).ShisakuBankoSuryoU
                    '    .ShisakuBuhinHi = koseiMatrix(index).ShisakuBuhinHi
                    '    .ShisakuKataHi = koseiMatrix(index).ShisakuKataHi
                    '    .Bikou = koseiMatrix(index).Bikou
                    '    .BuhinNote = koseiMatrix(index).BuhinNote
                    '    .EditTourokubi = koseiMatrix(index).EditTourokubi
                    '    .EditTourokujikan = koseiMatrix(index).EditTourokujikan
                    '    .KaiteiHandanFlg = koseiMatrix(index).KaiteiHandanFlg
                    '    .ShisakuListCode = koseiMatrix(index).ShisakuListCode
                    'End With
                    'BEBVos.Add(BEBVo)

                    '本体とベースを合体
                    'sqlHairetu(buhinNoHyoujiJun) = sb.ToString + sb2.ToString
                    db.ExecuteNonQuery(sb.ToString + sb2.ToString)

                    '表示順を採番
                    buhinNoHyoujiJun = buhinNoHyoujiJun + 1
                Next

                db.Commit()
            End Using

            'Using sqlConn As New SqlConnection(NitteiDbComFunc.GetConnectString)
            '    sqlConn.Open()
            '    Using tr As SqlClient.SqlTransaction = sqlConn.BeginTransaction
            '        Using addData As DataTable = NitteiDbComFunc.ConvListToDataTable(BEVos)
            '            Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(sqlConn, SqlBulkCopyOptions.KeepIdentity, tr)
            '                bulkCopy.BulkCopyTimeout = 0  ' in seconds
            '                bulkCopy.DestinationTableName = MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT"
            '                bulkCopy.WriteToServer(addData)
            '                bulkCopy.Close()
            '            End Using
            '        End Using

            '        Using addData As DataTable = NitteiDbComFunc.ConvListToDataTable(BEBVos)
            '            Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(sqlConn, SqlBulkCopyOptions.KeepIdentity, tr)
            '                bulkCopy.BulkCopyTimeout = 0  ' in seconds
            '                bulkCopy.DestinationTableName = MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE"
            '                bulkCopy.WriteToServer(addData)
            '                bulkCopy.Close()
            '            End Using
            '        End Using
            '        tr.Commit()
            '    End Using
            'End Using


            'Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            '    db.Open()
            '    db.BeginTransaction()
            '    For index As Integer = 0 To buhinNoHyoujiJun - 1
            '        If Not sqlHairetu(index) Is Nothing Then
            '            db.ExecuteNonQuery(sqlHairetu(index))
            '        End If
            '    Next
            '    db.Commit()
            'End Using

            ''配列クリア
            'Array.Clear(sqlHairetu, 0, sqlHairetu.Length)

            ''↓↓2014/09/12 1 ベース部品表作成表機能増強 酒井ADD BEGIN                
            MaxUpdateBuhinNoHyoujijun = buhinNoHyoujiJun
            ''↑↑2014/09/12 1 ベース部品表作成表機能増強 酒井ADD END

        End Sub



        '/*** 20140911 CHANGE START ***/
        ''' <summary>
        ''' 部品表編集INSTLと部品編集ベースINSTLにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <param name="JikyuUmu">自給品の有無</param>
        ''' <remarks></remarks>
        Public Sub InsertBySekkeiBuhinEditInstlEvent(ByVal shisakuEventCode As String, _
                                                     ByVal shisakuBukaCode As String, _
                                                     ByVal shisakuBlockNo As String, _
                                                     ByVal shisakuBlockNoKaiteiNo As String, _
                                                     ByVal koseiMatrix As BuhinKoseiMatrix, _
                                                     ByVal JikyuUmu As String) Implements BuhinEditBaseDao.InsertBySekkeiBuhinEditInstlEvent
            Me.InsertBySekkeiBuhinEditInstlEvent(shisakuEventCode, _
                                                      shisakuBukaCode, _
                                                      shisakuBlockNo, _
                                                      shisakuBlockNoKaiteiNo, _
                                                      koseiMatrix, _
                                                      JikyuUmu, _
                                                      New ShisakuDate)

        End Sub
        ''' <summary>
        ''' 部品表編集INSTLと部品編集ベースINSTLにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <param name="JikyuUmu">自給品の有無</param>
        ''' <param name="aDate">試作日</param>
        ''' <remarks></remarks>
        Public Sub InsertBySekkeiBuhinEditInstlEvent(ByVal shisakuEventCode As String, _
                                                     ByVal shisakuBukaCode As String, _
                                                     ByVal shisakuBlockNo As String, _
                                                     ByVal shisakuBlockNoKaiteiNo As String, _
                                                     ByVal koseiMatrix As BuhinKoseiMatrix, _
                                                     ByVal JikyuUmu As String, _
                                                     ByVal aDate As ShisakuDate, Optional ByVal instlTitle As BuhinEditKoseiInstlTitle = Nothing) Implements BuhinEditBaseDao.InsertBySekkeiBuhinEditInstlEvent
            Dim instlHinbanHyoujiJun As Integer = 0
            Dim InstlHyoujijun As Integer = 0

            '縦'
            Dim row As Integer = 0
            Dim col As Integer = 0
            Dim flag As Boolean
            Dim BuhinNoHyoujiJun As Integer = 0

            '↓↓2014/10/08 酒井 ADD BEGIN
            'EBOM、試作イベントが展開されたSBIから、試作イベントから展開されたINSTLの表示順を取得
            'Dim instlHinbanHyoujiJunList As List(Of TShisakuSekkeiBlockInstlVo) = FindEventInstlHyoujiJun(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, shisakuBlockNoKaiteiNo)
            '↑↑2014/10/08 酒井 ADD END
            '空列を含む場合、koseiMatrix.GetInputInsuColumnIndexesとinstlHinbanHyoujiJunListの数が合わないので対応付ける
            '↓↓2014/10/09 酒井 ADD BEGIN
            'Dim i As Integer = 0
            'Dim instlHinbanHyoujiJunDic As New Dictionary(Of Integer, Integer)
            'For Each columnIndex As Integer In koseiMatrix.GetInputInsuColumnIndexes
            '    For rowindex As Integer = 0 To koseiMatrix.GetInputRowIndexes.Count - 1
            '        If koseiMatrix.InsuSuryo(rowindex, columnIndex) IsNot Nothing Then
            '            instlHinbanHyoujiJunDic.Add(columnIndex, instlHinbanHyoujiJunList(i).InstlHinbanHyoujiJun)
            '            i = i + 1
            '            Exit For
            '        End If
            '    Next
            'Next
            ''↑↑2014/10/09 酒井 ADD END
            'Dim BEIVos As New List(Of TShisakuBuhinEditInstlVo)
            'Dim BEIBVos As New List(Of TShisakuBuhinEditInstlBaseVo)

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.BeginTransaction()

                For rowindex As Integer = 0 To koseiMatrix.GetInputRowIndexes.Count - 1
                    flag = False
                    '自給品の削除'
                    If StringUtil.Equals(JikyuUmu, "0") Then
                        If StringUtil.IsEmpty(koseiMatrix(rowindex).ShukeiCode) Then
                            If StringUtil.Equals(koseiMatrix(rowindex).SiaShukeiCode, "J") Then
                                Continue For
                            End If
                        ElseIf StringUtil.Equals(koseiMatrix(rowindex).ShukeiCode, "J") Then
                            Continue For
                        End If
                    End If
                    For Each columnIndex As Integer In koseiMatrix.GetInputInsuColumnIndexes
                        Dim param As New TShisakuBuhinEditInstlVo
                        If koseiMatrix.InsuSuryo(rowindex, columnIndex) Is Nothing Then
                            Continue For
                        End If

                        ''↓↓2014/10/08 酒井 ADD BEGIN
                        'InstlHyoujijun = instlHinbanHyoujiJunDic(columnIndex)
                        ''↑↑2014/10/08 酒井 ADD END

                        '構成の部品番号表示順と同じ員数のみ登録'

                        Dim sql As String = _
                            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL " _
                            & " WITH (UPDLOCK) " _
                            & " ( " _
                            & " SHISAKU_EVENT_CODE, " _
                            & " SHISAKU_BUKA_CODE, " _
                            & " SHISAKU_BLOCK_NO, " _
                            & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                            & " BUHIN_NO_HYOUJI_JUN, " _
                            & " INSTL_HINBAN_HYOUJI_JUN, " _
                            & " INSU_SURYO, " _
                            & " SAISYU_KOUSHINBI, " _
                            & " CREATED_USER_ID, " _
                            & " CREATED_DATE, " _
                            & " CREATED_TIME, " _
                            & " UPDATED_USER_ID, " _
                            & " UPDATED_DATE, " _
                            & " UPDATED_TIME " _
                            & " ) " _
                            & " VALUES ( " _
                            & "'" & shisakuEventCode & "', " _
                            & "'" & shisakuBukaCode & "', " _
                            & "'" & shisakuBlockNo & "', " _
                            & "'" & shisakuBlockNoKaiteiNo & "', " _
                            & BuhinNoHyoujiJun & ", " _
                            & columnIndex & ", " _
                            & koseiMatrix.InsuSuryo(rowindex, columnIndex) & ", " _
                            & "'" & Integer.Parse(Replace(aDate.CurrentDateDbFormat, "-", "")) & "', " _
                            & "'" & LoginInfo.Now.UserId & "', " _
                            & "'" & aDate.CurrentDateDbFormat & "', " _
                            & "'" & aDate.CurrentTimeDbFormat & "', " _
                            & "'" & LoginInfo.Now.UserId & "', " _
                            & "'" & aDate.CurrentDateDbFormat & "', " _
                            & "'" & aDate.CurrentTimeDbFormat & "' " _
                            & " ) "
                        ''↓↓2014/10/08 酒井 ADD BEGIN                        '
                        '& columnIndex & ", " _
                        '↑↑2014/10/08 酒井 ADD END
                        'Dim BEIVo As TShisakuBuhinEditInstlVo = NitteiDbComFunc.setDefault(Of TShisakuBuhinEditInstlVo)(aDate)
                        'With BEIVo
                        '    .ShisakuEventCode = shisakuEventCode
                        '    .ShisakuBukaCode = shisakuBukaCode
                        '    .ShisakuBlockNo = shisakuBlockNo
                        '    .ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
                        '    .BuhinNoHyoujiJun = BuhinNoHyoujiJun
                        '    .InstlHinbanHyoujiJun = columnIndex
                        '    .InsuSuryo = koseiMatrix.InsuSuryo(rowindex, columnIndex)
                        '    .SaisyuKoushinbi = Integer.Parse(Replace(aDate.CurrentDateDbFormat, "-", ""))
                        'End With
                        'BEIVos.Add(BEIVo)


                        '試作部品編集INSTL情報（ベース）
                        Dim sqlBase As String = _
                        " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE " _
                        & " WITH (UPDLOCK) " _
                        & " ( " _
                        & " SHISAKU_EVENT_CODE, " _
                        & " SHISAKU_BUKA_CODE, " _
                        & " SHISAKU_BLOCK_NO, " _
                        & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                        & " BUHIN_NO_HYOUJI_JUN, " _
                        & " INSTL_HINBAN_HYOUJI_JUN, " _
                        & " INSU_SURYO, " _
                        & " SAISYU_KOUSHINBI, " _
                        & " CREATED_USER_ID, " _
                        & " CREATED_DATE, " _
                        & " CREATED_TIME, " _
                        & " UPDATED_USER_ID, " _
                        & " UPDATED_DATE, " _
                        & " UPDATED_TIME " _
                        & " ) " _
                        & " VALUES ( " _
                        & "'" & shisakuEventCode & "', " _
                        & "'" & shisakuBukaCode & "', " _
                        & "'" & shisakuBlockNo & "', " _
                        & "'" & shisakuBlockNoKaiteiNo & "', " _
                        & BuhinNoHyoujiJun & ", " _
                        & columnIndex & ", " _
                        & koseiMatrix.InsuSuryo(rowindex, columnIndex) & ", " _
                        & "'" & Integer.Parse(Replace(aDate.CurrentDateDbFormat, "-", "")) & "', " _
                        & "'" & LoginInfo.Now.UserId & "', " _
                        & "'" & aDate.CurrentDateDbFormat & "', " _
                        & "'" & aDate.CurrentTimeDbFormat & "', " _
                        & "'" & LoginInfo.Now.UserId & "', " _
                        & "'" & aDate.CurrentDateDbFormat & "', " _
                        & "'" & aDate.CurrentTimeDbFormat & "' " _
                        & " ) "

                        'Dim BEIBVo As TShisakuBuhinEditInstlBaseVo = NitteiDbComFunc.setDefault(Of TShisakuBuhinEditInstlBaseVo)(aDate)
                        'With BEIBVo
                        '    .ShisakuEventCode = shisakuEventCode
                        '    .ShisakuBukaCode = shisakuBukaCode
                        '    .ShisakuBlockNo = shisakuBlockNo
                        '    .ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
                        '    .BuhinNoHyoujiJun = BuhinNoHyoujiJun
                        '    .InstlHinbanHyoujiJun = columnIndex
                        '    .InsuSuryo = koseiMatrix.InsuSuryo(rowindex, columnIndex)
                        '    .SaisyuKoushinbi = Integer.Parse(Replace(aDate.CurrentDateDbFormat, "-", ""))
                        'End With
                        'BEIBVos.Add(BEIBVo)

                        '本体とベースを合体
                        db.ExecuteNonQuery(sql + sqlBase)

                        instlHinbanHyoujiJun = columnIndex
                        flag = True
                    Next

                    If flag Then
                        BuhinNoHyoujiJun = BuhinNoHyoujiJun + 1
                    End If
                Next

                db.Commit()
            End Using

            'Using sqlConn As New SqlConnection(NitteiDbComFunc.GetConnectString)
            '    sqlConn.Open()
            '    Using tr As SqlClient.SqlTransaction = sqlConn.BeginTransaction
            '        Using addData As DataTable = NitteiDbComFunc.ConvListToDataTable(BEIVos)
            '            Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(sqlConn, SqlBulkCopyOptions.KeepIdentity, tr)
            '                bulkCopy.BulkCopyTimeout = 0  ' in seconds
            '                bulkCopy.DestinationTableName = MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL"
            '                bulkCopy.WriteToServer(addData)
            '                bulkCopy.Close()
            '            End Using
            '        End Using

            '        Using addData As DataTable = NitteiDbComFunc.ConvListToDataTable(BEIBVos)
            '            Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(sqlConn, SqlBulkCopyOptions.KeepIdentity, tr)
            '                bulkCopy.BulkCopyTimeout = 0  ' in seconds
            '                bulkCopy.DestinationTableName = MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE"
            '                bulkCopy.WriteToServer(addData)
            '                bulkCopy.Close()
            '            End Using
            '        End Using
            '        tr.Commit()
            '    End Using
            'End Using


        End Sub


        ''' <summary>
        ''' 設計ブロックINSTLの表示順を更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <remarks></remarks>
        Public Sub UpdateBySekkeiBlockInstl(ByVal shisakuEventCode As String, _
                                            ByVal shisakuBukaCode As String, _
                                            ByVal shisakuBlockNo As String, _
                                            ByVal koseiMatrix As BuhinKoseiMatrix, Optional ByRef MaxUpdateInstlHyoujijun As Integer = 0, Optional ByVal paraInstlTitle As BuhinEditKoseiInstlTitle = Nothing) Implements BuhinEditBaseDao.UpdateBySekkeiBlockInstl
            Dim sb As New StringBuilder

            Dim col As Integer = 0

            '事前にインスタンスを生成する
            Dim db As New EBomDbClient

            For columnindex As Integer = 0 To koseiMatrix.GetInputInsuColumnIndexes.Count - 1
                If koseiMatrix.InstlColumn(columnindex).Count = 0 Then
                    Continue For
                End If
                For rowindex As Integer = 0 To koseiMatrix.InstlColumn(columnindex).Count - 1

                    If koseiMatrix.InstlColumn(columnindex).Record(rowindex).Level = 0 Then
                        With sb
                            .Remove(0, .Length)
                            .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                            .AppendLine(" SET INSTL_HINBAN_HYOUJI_JUN = '" & col & "' ")
                            .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT ")
                            .AppendLine(" WHERE ")
                            .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_EVENT_CODE = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_EVENT_CODE ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_BUKA_CODE = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_BUKA_CODE ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_BLOCK_NO = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_BLOCK_NO ")
                            '.AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_BLOCK_NO_KAITEI_NO = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.BUHIN_NO = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.INSTL_HINBAN ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.LEVEL = 0 ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.BUHIN_NO = @BuhinNo ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.BUHIN_NO_KBN = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.INSTL_HINBAN_KBN ")
                            .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.BUHIN_NO_KBN = @BuhinNoKbn ")

                            '↓↓2014/10/07 酒井 ADD BEGIN
                            Dim flg As Boolean = False
                            For Each index As Integer In paraInstlTitle.GetInputInstlHinbanColumnIndexes()
                                If index = columnindex Then
                                    flg = True
                                    Exit For
                                End If
                            Next
                            If flg Then
                                .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.INSTL_DATA_KBN = '" & paraInstlTitle.InstlDataKbn(columnindex) & "' ")
                                .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.BASE_INSTL_FLG = '" & paraInstlTitle.BaseInstlFlg(columnindex) & "' ")
                            End If

                        End With

                        Dim param As New TShisakuBuhinEditVo
                        param.ShisakuEventCode = shisakuEventCode
                        param.ShisakuBukaCode = shisakuBukaCode
                        param.ShisakuBlockNo = shisakuBlockNo
                        param.BuhinNo = koseiMatrix.InstlColumn(columnindex).Record(rowindex).BuhinNo
                        param.BuhinNoKbn = koseiMatrix.InstlColumn(columnindex).Record(rowindex).BuhinNoKbn

                        col = col + 1
                        Exit For
                    End If
                Next
            Next
            MaxUpdateInstlHyoujijun = col
        End Sub

        Public Sub UpdateBySekkeiBlockInstlEbom(ByVal shisakuEventCode As String, _
                                    ByVal shisakuBukaCode As String, _
                                    ByVal shisakuBlockNo As String, _
                                    ByVal koseiMatrix As BuhinKoseiMatrix, Optional ByRef MaxUpdateInstlHyoujijun As Integer = 0, Optional ByVal paraInstlTitle As BuhinEditKoseiInstlTitle = Nothing) Implements BuhinEditBaseDao.UpdateBySekkeiBlockInstlEbom
            Dim sb As New StringBuilder

            Dim col As Integer = 0
            Dim InstlHyoujijun As Integer = MaxUpdateInstlHyoujijun
            For columnindex As Integer = 0 To koseiMatrix.GetInputInsuColumnIndexes.Count - 1
                If koseiMatrix.InstlColumn(columnindex).Count = 0 Then
                    Continue For
                End If
                Dim flg As Boolean = False
                For rowindex As Integer = 0 To koseiMatrix.InstlColumn(columnindex).Count - 1
                    If koseiMatrix.InstlColumn(columnindex).Record(rowindex).Level = 0 Then
                        If StringUtil.IsNotEmpty(koseiMatrix.InstlColumn(columnindex).Record(rowindex).BuhinNo) Then
                            flg = True
                            With sb
                                .Remove(0, .Length)
                                .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                                .AppendLine(" SET INSTL_HINBAN_HYOUJI_JUN = '" & InstlHyoujijun & "' ")
                                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT ")
                                .AppendLine(" WHERE ")
                                .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_EVENT_CODE = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_EVENT_CODE ")
                                .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_BUKA_CODE = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_BUKA_CODE ")
                                .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_BLOCK_NO = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.SHISAKU_BLOCK_NO ")
                                .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.BUHIN_NO = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.INSTL_HINBAN ")
                                .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.LEVEL = 0 ")
                                .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                                .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                                .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                                .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.BUHIN_NO = @BuhinNo ")
                                .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.BUHIN_NO_KBN = " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.INSTL_HINBAN_KBN ")
                                .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT.BUHIN_NO_KBN = @BuhinNoKbn ")
                                .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.INSTL_DATA_KBN = '0' ")

                                Dim flg2 As Boolean = False
                                For Each index As Integer In paraInstlTitle.GetInputInstlHinbanColumnIndexes()
                                    If index = columnindex Then
                                        flg2 = True
                                        Exit For
                                    End If
                                Next
                                If flg2 Then
                                    .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.INSTL_DATA_KBN = '" & paraInstlTitle.InstlDataKbn(columnindex) & "' ")
                                    .AppendLine(" AND " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL.BASE_INSTL_FLG = '" & paraInstlTitle.BaseInstlFlg(columnindex) & "' ")
                                End If
                            End With

                            Dim param As New TShisakuBuhinEditVo
                            param.ShisakuEventCode = shisakuEventCode
                            param.ShisakuBukaCode = shisakuBukaCode
                            param.ShisakuBlockNo = shisakuBlockNo
                            param.BuhinNo = koseiMatrix.InstlColumn(columnindex).Record(rowindex).BuhinNo
                            param.BuhinNoKbn = koseiMatrix.InstlColumn(columnindex).Record(rowindex).BuhinNoKbn

                            Dim db As New EBomDbClient
                            col = col + 1
                            Exit For
                        End If
                    End If
                Next
                If flg Then
                    InstlHyoujijun = InstlHyoujijun + 1
                End If
            Next
        End Sub

        Public Sub UpdateInstlHinbanHyoujiJunBySekkeiBlockInstl(ByVal deleteInstl As TShisakuSekkeiBlockInstlVo) Implements BuhinEditBaseDao.UpdateInstlHinbanHyoujiJunBySekkeiBlockInstl

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                .AppendLine(" SET INSTL_HINBAN_HYOUJI_JUN = INSTL_HINBAN_HYOUJI_JUN - 1 ")
                .AppendLine(" WHERE INSTL_HINBAN_HYOUJI_JUN > @InstlHinbanHyoujiJun ")
                .AppendLine(" AND SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
            End With

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = deleteInstl.ShisakuEventCode
            param.ShisakuBukaCode = deleteInstl.ShisakuBukaCode
            param.ShisakuBlockNo = deleteInstl.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = deleteInstl.ShisakuBlockNoKaiteiNo
            param.InstlHinbanHyoujiJun = deleteInstl.InstlHinbanHyoujiJun

            Dim db As New EBomDbClient
            db.Update(sb.ToString, param)

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                .AppendLine(" SET INSTL_HINBAN_HYOUJI_JUN = INSTL_HINBAN_HYOUJI_JUN - 1 ")
                .AppendLine(" WHERE INSTL_HINBAN_HYOUJI_JUN > @InstlHinbanHyoujiJun ")
                .AppendLine(" AND SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
            End With

            Dim paramBase As New TShisakuSekkeiBlockInstlVo
            paramBase.ShisakuEventCode = deleteInstl.ShisakuEventCode
            paramBase.ShisakuBukaCode = deleteInstl.ShisakuBukaCode
            paramBase.ShisakuBlockNo = deleteInstl.ShisakuBlockNo
            paramBase.ShisakuBlockNoKaiteiNo = "  0"
            paramBase.InstlHinbanHyoujiJun = deleteInstl.InstlHinbanHyoujiJun

            db.Update(sb.ToString, paramBase)
        End Sub
        Public Sub UpdateInstlHinbanHyoujiJunByBuhinEditInstl(ByVal deleteInstl As TShisakuSekkeiBlockInstlVo) Implements BuhinEditBaseDao.UpdateInstlHinbanHyoujiJunByBuhinEditInstl

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL ")
                .AppendLine(" SET INSTL_HINBAN_HYOUJI_JUN = INSTL_HINBAN_HYOUJI_JUN - 1 ")
                .AppendLine(" WHERE INSTL_HINBAN_HYOUJI_JUN > @InstlHinbanHyoujiJun ")
                .AppendLine(" AND SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
            End With

            Dim param As New TShisakuBuhinEditInstlVo
            param.ShisakuEventCode = deleteInstl.ShisakuEventCode
            param.ShisakuBukaCode = deleteInstl.ShisakuBukaCode
            param.ShisakuBlockNo = deleteInstl.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = deleteInstl.ShisakuBlockNoKaiteiNo
            param.InstlHinbanHyoujiJun = deleteInstl.InstlHinbanHyoujiJun

            Dim db As New EBomDbClient
            db.Update(sb.ToString, param)

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE ")
                .AppendLine(" SET INSTL_HINBAN_HYOUJI_JUN = INSTL_HINBAN_HYOUJI_JUN - 1 ")
                .AppendLine(" WHERE INSTL_HINBAN_HYOUJI_JUN > @InstlHinbanHyoujiJun ")
                .AppendLine(" AND SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
            End With

            Dim paramBase As New TShisakuBuhinEditInstlBaseVo
            paramBase.ShisakuEventCode = deleteInstl.ShisakuEventCode
            paramBase.ShisakuBukaCode = deleteInstl.ShisakuBukaCode
            paramBase.ShisakuBlockNo = deleteInstl.ShisakuBlockNo
            paramBase.ShisakuBlockNoKaiteiNo = deleteInstl.ShisakuBlockNoKaiteiNo
            paramBase.InstlHinbanHyoujiJun = deleteInstl.InstlHinbanHyoujiJun

            db.Update(sb.ToString, paramBase)
        End Sub
        Public Sub UpdateBuhinNoHyoujiJunByBuhinEditInstl(ByVal deleteBuhin As List(Of TShisakuBuhinEditVo)) Implements BuhinEditBaseDao.UpdateBuhinNoHyoujiJunByBuhinEditInstl

            Dim sb As New StringBuilder
            Dim maeBuhinNoHyoujiJun As Integer

            For index As Integer = deleteBuhin.Count - 1 To 0 Step -1
                If deleteBuhin(index).BuhinNoHyoujiJun = maeBuhinNoHyoujiJun Then
                    Continue For
                End If
                With sb
                    .Remove(0, .Length)
                    .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL ")
                    .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = BUHIN_NO_HYOUJI_JUN - 1 ")
                    .AppendLine(" WHERE BUHIN_NO_HYOUJI_JUN > @BuhinNoHyoujiJun ")
                    .AppendLine(" AND SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                    .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                End With

                Dim param As New TShisakuBuhinEditInstlVo
                param.ShisakuEventCode = deleteBuhin(index).ShisakuEventCode
                param.ShisakuBukaCode = deleteBuhin(index).ShisakuBukaCode
                param.ShisakuBlockNo = deleteBuhin(index).ShisakuBlockNo
                param.ShisakuBlockNoKaiteiNo = deleteBuhin(index).ShisakuBlockNoKaiteiNo
                param.BuhinNoHyoujiJun = deleteBuhin(index).BuhinNoHyoujiJun
                Dim db As New EBomDbClient
                db.Update(sb.ToString, param)

                With sb
                    .Remove(0, .Length)
                    .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE ")
                    .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = BUHIN_NO_HYOUJI_JUN - 1 ")
                    .AppendLine(" WHERE BUHIN_NO_HYOUJI_JUN > @BuhinNoHyoujiJun ")
                    .AppendLine(" AND SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                    .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                End With

                Dim paramBase As New TShisakuBuhinEditInstlBaseVo
                paramBase.ShisakuEventCode = deleteBuhin(index).ShisakuEventCode
                paramBase.ShisakuBukaCode = deleteBuhin(index).ShisakuBukaCode
                paramBase.ShisakuBlockNo = deleteBuhin(index).ShisakuBlockNo
                paramBase.ShisakuBlockNoKaiteiNo = deleteBuhin(index).ShisakuBlockNoKaiteiNo
                paramBase.BuhinNoHyoujiJun = deleteBuhin(index).BuhinNoHyoujiJun
                db.Update(sb.ToString, paramBase)

                maeBuhinNoHyoujiJun = deleteBuhin(index).BuhinNoHyoujiJun
            Next

        End Sub
        Public Sub UpdateBuhinNoHyoujiJunByBuhinEdit(ByVal deleteBuhin As List(Of TShisakuBuhinEditVo)) Implements BuhinEditBaseDao.UpdateBuhinNoHyoujiJunByBuhinEdit

            Dim sb As New StringBuilder
            Dim maeBuhinNoHyoujiJun As Integer

            For index As Integer = deleteBuhin.Count - 1 To 0 Step -1
                If deleteBuhin(index).BuhinNoHyoujiJun = maeBuhinNoHyoujiJun Then
                    Continue For
                End If
                With sb
                    .Remove(0, .Length)
                    .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT ")
                    .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = BUHIN_NO_HYOUJI_JUN - 1 ")
                    .AppendLine(" WHERE BUHIN_NO_HYOUJI_JUN > @BuhinNoHyoujiJun ")
                    .AppendLine(" AND SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                    .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                End With

                Dim param As New TShisakuBuhinEditVo
                param.ShisakuEventCode = deleteBuhin(index).ShisakuEventCode
                param.ShisakuBukaCode = deleteBuhin(index).ShisakuBukaCode
                param.ShisakuBlockNo = deleteBuhin(index).ShisakuBlockNo
                param.ShisakuBlockNoKaiteiNo = deleteBuhin(index).ShisakuBlockNoKaiteiNo
                param.BuhinNoHyoujiJun = deleteBuhin(index).BuhinNoHyoujiJun
                Dim db As New EBomDbClient
                db.Update(sb.ToString, param)

                With sb
                    .Remove(0, .Length)
                    .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE ")
                    .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = BUHIN_NO_HYOUJI_JUN - 1 ")
                    .AppendLine(" WHERE BUHIN_NO_HYOUJI_JUN > @BuhinNoHyoujiJun ")
                    .AppendLine(" AND SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                    .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                End With

                Dim paramBase As New TShisakuBuhinEditBaseVo
                paramBase.ShisakuEventCode = deleteBuhin(index).ShisakuEventCode
                paramBase.ShisakuBukaCode = deleteBuhin(index).ShisakuBukaCode
                paramBase.ShisakuBlockNo = deleteBuhin(index).ShisakuBlockNo
                paramBase.ShisakuBlockNoKaiteiNo = deleteBuhin(index).ShisakuBlockNoKaiteiNo
                paramBase.BuhinNoHyoujiJun = deleteBuhin(index).BuhinNoHyoujiJun
                db.Update(sb.ToString, paramBase)

                maeBuhinNoHyoujiJun = deleteBuhin(index).BuhinNoHyoujiJun
            Next
        End Sub
        ''↑↑2014/09/12 1 ベース部品表作成表機能増強 酒井ADD END


        ''' <summary>
        ''' 購担/取引先を取得する
        ''' </summary>
        ''' <param name="buhinNo">部品No</param>
        ''' <returns>購担と取引先</returns>
        ''' <remarks></remarks>
        Public Function FindByKoutanTorihikisaki(ByVal buhinNo As String) As TShisakuBuhinEditVo Implements BuhinEditBaseDao.FindByKoutanTorihikisaki

            Dim db As New EBomDbClient

            Dim sql As String = _
            " SELECT KA, " _
            & " TRCD " _
            & " FROM " & MBOM_DB_NAME & ".dbo.AS_KPSM10P WITH (NOLOCK, NOWAIT)" _
            & " WHERE  " _
            & " BUBA_15 = @Buba15 " _
            & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

            Dim NewBuhinNo As String = ""
            If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                NewBuhinNo = Replace(buhinNo, "-", " ")
            Else
                NewBuhinNo = buhinNo
            End If

            Dim paramK As New AsKPSM10PVo
            Dim ETVO As New TShisakuBuhinEditVo

            paramK.Buba15 = NewBuhinNo

            Dim aKPSM As New AsKPSM10PVo

            aKPSM = db.QueryForObject(Of AsKPSM10PVo)(sql, paramK)
            '存在チェックその１(３ヶ月インフォメーション)'
            If aKPSM Is Nothing Then
                NewBuhinNo = ""
                '無ければパーツプライリスト'
                Dim sql2 As String = _
                " SELECT KA, " _
                & " TRCD " _
                & " FROM " & MBOM_DB_NAME & ".dbo.AS_PARTSP WITH (NOLOCK, NOWAIT)" _
                & " WHERE BUBA_13 = @Buba13 " _
                & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                Dim paramP As New AsPARTSPVo

                If buhinNo.Length < 13 Then
                    If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                        NewBuhinNo = Replace(buhinNo, "-", " ")
                    Else
                        NewBuhinNo = buhinNo
                    End If
                    paramP.Buba13 = NewBuhinNo
                Else
                    If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                        NewBuhinNo = Left(Replace(buhinNo, "-", " "), 13)
                    Else
                        NewBuhinNo = Left(buhinNo, 13)
                    End If
                    paramP.Buba13 = NewBuhinNo
                End If

                Dim aPARTS As New AsPARTSPVo
                aPARTS = db.QueryForObject(Of AsPARTSPVo)(sql2, paramP)

                '存在チェックその２(パーツプライリスト)'
                If aPARTS Is Nothing Then
                    NewBuhinNo = ""
                    '無ければ海外生産マスタ'
                    Dim sql3 As String = _
                    " SELECT KA, " _
                    & " TRCD " _
                    & " FROM " & MBOM_DB_NAME & ".dbo.AS_GKPSM10P WITH (NOLOCK, NOWAIT)" _
                    & " WHERE BUBA_15 = @Buba15 " _
                    & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                    Dim paramG As New AsGKPSM10PVo
                    If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                        NewBuhinNo = Replace(buhinNo, "-", " ")
                    Else
                        NewBuhinNo = buhinNo
                    End If
                    paramG.Buba15 = NewBuhinNo
                    Dim aGKPSM As New AsGKPSM10PVo
                    aGKPSM = db.QueryForObject(Of AsGKPSM10PVo)(sql3, paramG)

                    '存在チェックその３(海外生産マスタ) '
                    If aGKPSM Is Nothing Then
                        NewBuhinNo = ""
                        '無ければ部品マスタ'
                        If buhinNo.Length < 10 Then
                            If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                                NewBuhinNo = Replace(buhinNo, "-", " ")
                            Else
                                NewBuhinNo = buhinNo
                            End If
                        Else
                            If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                                NewBuhinNo = Left(Replace(buhinNo, "-", " "), 10)
                            Else
                                NewBuhinNo = Left(buhinNo, 10)
                            End If
                        End If
                        Dim sql4 As String = _
                        " SELECT KOTAN, " _
                        & " MAKER " _
                        & " FROM " & MBOM_DB_NAME & ".dbo.AS_BUHIN01 WITH (NOLOCK, NOWAIT)" _
                        & " WHERE " _
                        & " GZZCP = @Gzzcp " _
                        & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                        Dim param4 As New AsBUHIN01Vo
                        param4.Gzzcp = NewBuhinNo

                        Dim aBuhin01 As New AsBUHIN01Vo
                        aBuhin01 = db.QueryForObject(Of AsBUHIN01Vo)(sql4, param4)

                        '存在チェックその４(部品マスタ)'
                        If aBuhin01 Is Nothing Then
                            '無ければ属性管理'
                            Dim sql5 As String = _
                            "SELECT " _
                            & " FHI_NOMINATE_KOTAN, " _
                            & " TORIHIKISAKI_CODE " _
                            & " FROM " _
                            & " " + EBOM_DB_NAME + ".dbo.T_VALUE_DEV WITH (NOLOCK, NOWAIT)" _
                            & " WHERE " _
                            & "  AN_LEVEL = '1' " _
                            & " AND BUHIN_NO = @BuhinNo " _
                            & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                            Dim aDev As New TValueDevVo
                            Dim paramT As New TValueDevVo
                            'paramT.KaihatsuFugo = KaihatsuFugo
                            If buhinNo.Length < 10 Then
                                paramT.BuhinNo = buhinNo
                            Else
                                paramT.BuhinNo = Left(buhinNo, 10)
                            End If

                            aDev = db.QueryForObject(Of TValueDevVo)(sql5, paramT)

                            '存在チェックその５(属性管理(開発符号毎)) '
                            If aDev Is Nothing Then
                                ETVO.MakerCode = ""
                            Else
                                ETVO.MakerCode = aDev.TorihikisakiCode
                            End If

                        Else
                            ETVO.MakerCode = aBuhin01.Maker
                        End If
                    Else
                        ETVO.MakerCode = aGKPSM.Trcd
                    End If
                Else
                    ETVO.MakerCode = aPARTS.Trcd
                End If
            Else
                ETVO.MakerCode = aKPSM.Trcd
            End If

            If Not StringUtil.IsEmpty(ETVO.MakerCode) Then
                Dim Msql As String = _
                " SELECT MAKER_NAME " _
                & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0610 WITH (NOLOCK, NOWAIT)" _
                & " WHERE " _
                & " MAKER_CODE = @MakerCode "

                Dim Mparam As New Rhac0610Vo
                Mparam.MakerCode = ETVO.MakerCode
                Dim MVo As New Rhac0610Vo
                MVo = db.QueryForObject(Of Rhac0610Vo)(Msql, Mparam)
                If MVo Is Nothing Then
                    ETVO.MakerName = ""
                Else
                    ETVO.MakerName = MVo.MakerName

                End If
            End If

            Return ETVO


            '抜けるまでの間に何も無ければNOTHING'
            'Return Nothing
        End Function

        '/*** 20140911 CHANGE START ***/
        Private Function getThreeMonthInfo(ByVal db As EBomDbClient, _
                                           ByVal buhinNo As String) As AsKPSM10PVo

            Dim sql As String = _
            " SELECT KA, " _
            & " TRCD " _
            & " FROM " & MBOM_DB_NAME & ".dbo.AS_KPSM10P WITH (NOLOCK, NOWAIT)" _
            & " WHERE  " _
            & " BUBA_15 = @Buba15 " _
            & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "


            Dim paramK As New AsKPSM10PVo
            If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                paramK.Buba15 = Replace(buhinNo, "-", " ")
            Else
                paramK.Buba15 = buhinNo
            End If

            Return db.QueryForObject(Of AsKPSM10PVo)(sql, paramK)

        End Function


        Private Function getParts(ByVal db As EBomDbClient, _
                                   ByVal buhinNo As String) As AsPARTSPVo

            Dim sql2 As String = _
                " SELECT KA, " _
                & " TRCD " _
                & " FROM " & MBOM_DB_NAME & ".dbo.AS_PARTSP WITH (NOLOCK, NOWAIT)" _
                & " WHERE BUBA_13 = @Buba13 " _
                & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

            Dim paramP As New AsPARTSPVo
            If buhinNo.Length < 13 Then
                If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                    paramP.Buba13 = Replace(buhinNo, "-", " ")
                Else
                    paramP.Buba13 = buhinNo
                End If
            Else
                If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                    paramP.Buba13 = Left(Replace(buhinNo, "-", " "), 13)
                Else
                    paramP.Buba13 = Left(buhinNo, 13)
                End If
            End If

            Return db.QueryForObject(Of AsPARTSPVo)(sql2, paramP)

        End Function


        Private Function getKaigaiSeisan(ByVal db As EBomDbClient, _
                           ByVal buhinNo As String) As AsGKPSM10PVo

            Dim sql3 As String = _
                    " SELECT KA, " _
                    & " TRCD " _
                    & " FROM " & MBOM_DB_NAME & ".dbo.AS_GKPSM10P WITH (NOLOCK, NOWAIT)" _
                    & " WHERE BUBA_15 = @Buba15 " _
                    & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

            Dim paramG As New AsGKPSM10PVo

            If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                paramG.Buba15 = Replace(buhinNo, "-", " ")
            Else
                paramG.Buba15 = buhinNo
            End If

            Return db.QueryForObject(Of AsGKPSM10PVo)(sql3, paramG)

        End Function


        Private Function getBuhin(ByVal db As EBomDbClient, _
                   ByVal buhinNo As String) As AsBUHIN01Vo

            Dim sql4 As String = _
            " SELECT KOTAN, " _
            & " MAKER " _
            & " FROM " & MBOM_DB_NAME & ".dbo.AS_BUHIN01 WITH (NOLOCK, NOWAIT)" _
            & " WHERE " _
            & " GZZCP = @Gzzcp " _
            & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

            Dim param4 As New AsBUHIN01Vo
            If buhinNo.Length < 10 Then
                If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                    param4.Gzzcp = Replace(buhinNo, "-", " ")
                Else
                    param4.Gzzcp = buhinNo
                End If
            Else
                If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                    param4.Gzzcp = Left(Replace(buhinNo, "-", " "), 10)
                Else
                    param4.Gzzcp = Left(buhinNo, 10)
                End If
            End If

            Return db.QueryForObject(Of AsBUHIN01Vo)(sql4, param4)

        End Function


        Private Function getZokuseiKanri(ByVal db As EBomDbClient, _
           ByVal buhinNo As String) As TValueDevVo

            '無ければ属性管理'
            Dim sql5 As String = _
            "SELECT " _
            & " FHI_NOMINATE_KOTAN, " _
            & " TORIHIKISAKI_CODE " _
            & " FROM " _
            & " " + EBOM_DB_NAME + ".dbo.T_VALUE_DEV WITH (NOLOCK, NOWAIT)" _
            & " WHERE " _
            & "   BUHIN_NO = @BuhinNo " _
            & "   AND AN_LEVEL = '1' " _
            & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

            Dim paramT As New TValueDevVo
            If buhinNo.Length < 10 Then
                paramT.BuhinNo = buhinNo
            Else
                paramT.BuhinNo = Left(buhinNo, 10)
            End If

            Return db.QueryForObject(Of TValueDevVo)(sql5, paramT)

        End Function

        Private Function getTorihikisaki(ByVal db As EBomDbClient, _
                           ByVal makerCode As String) As Rhac0610Vo

            Dim Msql As String = _
                " SELECT MAKER_NAME " _
                & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0610 WITH (NOLOCK, NOWAIT)" _
                & " WHERE " _
                & " MAKER_CODE = @MakerCode "

            Dim Mparam As New Rhac0610Vo
            Mparam.MakerCode = makerCode

            Return db.QueryForObject(Of Rhac0610Vo)(Msql, Mparam)

        End Function
        ''' <summary>
        ''' 購担/取引先を取得する()
        ''' </summary>
        ''' <param name="buhinNo">部品No</param>
        ''' <param name="kotanTorihikisakiSelected">取得済みの購担と取引先</param>
        ''' <returns>購担と取引先</returns>
        ''' <remarks></remarks>
        Public Function FindByKoutanTorihikisakiUseDictionary(ByVal buhinNo As String, _
                                                            ByRef kotanTorihikisakiSelected As Dictionary(Of String, TShisakuBuhinEditVo)) As TShisakuBuhinEditVo Implements BuhinEditBaseDao.FindByKoutanTorihikisakiUseDictionary

            If kotanTorihikisakiSelected.ContainsKey(buhinNo) Then
                Return kotanTorihikisakiSelected(buhinNo)
            Else

                Dim ETVO As New TShisakuBuhinEditVo
                Dim db As New EBomDbClient


                '３ヶ月インフォメーション取得
                Dim aKPSM As AsKPSM10PVo = getThreeMonthInfo(db, buhinNo)


                '存在チェックその１(３ヶ月インフォメーション)'
                If aKPSM Is Nothing Then

                    'パーツプライリスト取得
                    Dim aPARTS As AsPARTSPVo = getParts(db, buhinNo)

                    '存在チェックその２(パーツプライリスト)'
                    If aPARTS Is Nothing Then

                        '海外生産マスタ取得'
                        Dim aGKPSM As AsGKPSM10PVo = getKaigaiSeisan(db, buhinNo)

                        '存在チェックその３(海外生産マスタ) '
                        If aGKPSM Is Nothing Then

                            '部品マスタ取得
                            Dim aBuhin01 As AsBUHIN01Vo = getBuhin(db, buhinNo)

                            '存在チェックその４(部品マスタ)'
                            If aBuhin01 Is Nothing Then
                                '属性管理取得'
                                Dim aDev As TValueDevVo = getZokuseiKanri(db, buhinNo)


                                '存在チェックその５(属性管理(開発符号毎)) '
                                If aDev Is Nothing Then
                                    ETVO.MakerCode = ""
                                Else
                                    ETVO.MakerCode = aDev.TorihikisakiCode
                                End If

                            Else
                                ETVO.MakerCode = aBuhin01.Maker
                            End If
                        Else
                            ETVO.MakerCode = aGKPSM.Trcd
                        End If
                    Else
                        ETVO.MakerCode = aPARTS.Trcd
                    End If
                Else
                    ETVO.MakerCode = aKPSM.Trcd
                End If

                If Not StringUtil.IsEmpty(ETVO.MakerCode) Then
                    Dim MVo As Rhac0610Vo = getTorihikisaki(db, ETVO.MakerCode)
                    If MVo Is Nothing Then
                        ETVO.MakerName = ""
                    Else
                        ETVO.MakerName = MVo.MakerName

                    End If
                End If

                kotanTorihikisakiSelected.Add(buhinNo, ETVO)

                Return ETVO
            End If
        End Function



        '/*** 20140911 CHANGE END ***/
        ''' <summary>
        ''' 部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEdit(ByVal shisakuEventCode As String, _
                                        ByVal shisakuBukaCode As String, _
                                        ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditVo) Implements BuhinEditBaseDao.FindByBuhinEdit
            Dim sql As String = _
            " SELECT DISTINCT BE.* " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B WITH (NOLOCK, NOWAIT) " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            & " ON SBI.SHISAKU_EVENT_CODE = B.SHISAKU_BASE_EVENT_CODE " _
            & " AND SBI.SHISAKU_GOUSYA = B.SHISAKU_BASE_GOUSYA " _
            & " AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO  = ( " _
            & "  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  " _
            & "  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL " _
            & "  WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & "  AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            & "  AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) " _
            & " AND NOT SHISAKU_BLOCK_NO_KAITEI_NO = '  0' " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
            & " ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & " AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            & " AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO " _
            & " AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') " _
            & " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
            & " ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE " _
            & " AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE " _
            & " AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO " _
            & " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE " _
            & " ON BE.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
            & " AND BE.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
            & " AND BE.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO " _
            & " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN " _
            & " WHERE  " _
            & " B.SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim param As New TShisakuBuhinEditVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditVo)(sql, param)
        End Function
        '↓↓2014/10/23 酒井 ADD BEGIN
        Public Function FindBuhin4UpdateBuhinHyoujiJun(ByVal shisakuEventCode As String) As List(Of Buhin4UpdateBuhinHyoujiJunVo) Implements BuhinEditBaseDao.FindBuhin4UpdateBuhinHyoujiJun
            Dim sql As String = _
                    " SELECT " _
                    & " BE1.SHISAKU_EVENT_CODE,  " _
                    & " BE1.SHISAKU_BUKA_CODE,  " _
                    & " BE1.SHISAKU_BLOCK_NO,  " _
                    & " BE1.SHISAKU_BLOCK_NO_KAITEI_NO,  " _
                    & " BE1.BUHIN_NO_HYOUJI_JUN,  " _
                    & " BE1.KYOUKU_SECTION," _
                    & " MIN(BE2.BUHIN_NO_HYOUJI_JUN) AS BUHIN_NO_HYOUJI_JUN_NEW " _
                    & " FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE1  WITH (NOLOCK, NOWAIT) " _
                    & " INNER JOIN  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE2  " _
                    & " ON BE1.SHISAKU_EVENT_CODE = BE2.SHISAKU_EVENT_CODE  " _
                    & " AND BE1.SHISAKU_BUKA_CODE = BE2.SHISAKU_BUKA_CODE  " _
                    & " AND BE1.SHISAKU_BLOCK_NO = BE2.SHISAKU_BLOCK_NO  " _
                    & " AND BE1.SHISAKU_BLOCK_NO_KAITEI_NO = BE2.SHISAKU_BLOCK_NO_KAITEI_NO  " _
                    & " AND BE1.BUHIN_NO_HYOUJI_JUN > BE2.BUHIN_NO_HYOUJI_JUN  " _
                    & " AND BE1.SHUKEI_CODE = BE2.SHUKEI_CODE  " _
                    & " AND BE1.SIA_SHUKEI_CODE = BE2.SIA_SHUKEI_CODE  " _
                    & " AND BE1.GENCYO_CKD_KBN = BE2.GENCYO_CKD_KBN  " _
                    & " AND BE1.MAKER_CODE = BE2.MAKER_CODE  " _
                    & " AND BE1.BUHIN_NO = BE2.BUHIN_NO  " _
                    & " AND BE1.LEVEL = BE2.LEVEL  " _
                    & " AND BE1.BUHIN_NO_KBN = BE2.BUHIN_NO_KBN  " _
                    & " AND BE1.BUHIN_NO_KAITEI_NO = BE2.BUHIN_NO_KAITEI_NO  " _
                    & " AND BE1.EDA_BAN = BE2.EDA_BAN " _
                    & " WHERE " _
                    & " BE1.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                    & " AND BE1.LEVEL <> 0 " _
                    & " GROUP BY " _
                    & " BE1.SHISAKU_EVENT_CODE,  " _
                    & " BE1.SHISAKU_BUKA_CODE,  " _
                    & " BE1.SHISAKU_BLOCK_NO,  " _
                    & " BE1.SHISAKU_BLOCK_NO_KAITEI_NO,  " _
                    & " BE1.KYOUKU_SECTION," _
                    & " BE1.BUHIN_NO_HYOUJI_JUN " _
                    & " ORDER BY " _
                    & " BE1.SHISAKU_EVENT_CODE,  " _
                    & " BE1.SHISAKU_BUKA_CODE,  " _
                    & " BE1.SHISAKU_BLOCK_NO,  " _
                    & " BE1.SHISAKU_BLOCK_NO_KAITEI_NO,  " _
                    & " BE1.BUHIN_NO_HYOUJI_JUN DESC"

            Dim param As New Buhin4UpdateBuhinHyoujiJunVo
            param.ShisakuEventCode = shisakuEventCode

            Dim db As New EBomDbClient
            Return db.QueryForList(Of Buhin4UpdateBuhinHyoujiJunVo)(sql, param)
        End Function
        Public Function FindBuhin4UpdateBuhinHyoujiJunBase(ByVal shisakuEventCode As String) As List(Of Buhin4UpdateBuhinHyoujiJunVo) Implements BuhinEditBaseDao.FindBuhin4UpdateBuhinHyoujiJunBase
            Dim sql As String = _
" SELECT " _
& " BE1.SHISAKU_EVENT_CODE,  " _
& " BE1.SHISAKU_BUKA_CODE,  " _
& " BE1.SHISAKU_BLOCK_NO,  " _
& " BE1.SHISAKU_BLOCK_NO_KAITEI_NO,  " _
& " BE1.BUHIN_NO_HYOUJI_JUN,  " _
& " MIN(BE2.BUHIN_NO_HYOUJI_JUN) AS BUHIN_NO_HYOUJI_JUN_NEW " _
& " FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE BE1  WITH (NOLOCK, NOWAIT) " _
& " INNER JOIN  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE BE2  " _
& " ON BE1.SHISAKU_EVENT_CODE = BE2.SHISAKU_EVENT_CODE  " _
& " AND BE1.SHISAKU_BUKA_CODE = BE2.SHISAKU_BUKA_CODE  " _
& " AND BE1.SHISAKU_BLOCK_NO = BE2.SHISAKU_BLOCK_NO  " _
& " AND BE1.SHISAKU_BLOCK_NO_KAITEI_NO = BE2.SHISAKU_BLOCK_NO_KAITEI_NO  " _
& " AND BE1.BUHIN_NO_HYOUJI_JUN > BE2.BUHIN_NO_HYOUJI_JUN  " _
& " AND BE1.SHUKEI_CODE = BE2.SHUKEI_CODE  " _
& " AND BE1.SIA_SHUKEI_CODE = BE2.SIA_SHUKEI_CODE  " _
& " AND BE1.GENCYO_CKD_KBN = BE2.GENCYO_CKD_KBN  " _
& " AND BE1.MAKER_CODE = BE2.MAKER_CODE  " _
& " AND BE1.BUHIN_NO = BE2.BUHIN_NO  " _
& " AND BE1.LEVEL = BE2.LEVEL  " _
& " AND BE1.BUHIN_NO_KBN = BE2.BUHIN_NO_KBN  " _
& " AND BE1.BUHIN_NO_KAITEI_NO = BE2.BUHIN_NO_KAITEI_NO  " _
& " AND BE1.EDA_BAN = BE2.EDA_BAN " _
& " WHERE " _
& " BE1.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
& " AND BE1.LEVEL <> 0 " _
& " GROUP BY " _
& " BE1.SHISAKU_EVENT_CODE,  " _
& " BE1.SHISAKU_BUKA_CODE,  " _
& " BE1.SHISAKU_BLOCK_NO,  " _
& " BE1.SHISAKU_BLOCK_NO_KAITEI_NO,  " _
& " BE1.BUHIN_NO_HYOUJI_JUN " _
& " ORDER BY " _
& " BE1.SHISAKU_EVENT_CODE,  " _
& " BE1.SHISAKU_BUKA_CODE,  " _
& " BE1.SHISAKU_BLOCK_NO,  " _
& " BE1.SHISAKU_BLOCK_NO_KAITEI_NO,  " _
& " BE1.BUHIN_NO_HYOUJI_JUN DESC"

            Dim param As New Buhin4UpdateBuhinHyoujiJunVo
            param.ShisakuEventCode = shisakuEventCode

            Dim db As New EBomDbClient
            Return db.QueryForList(Of Buhin4UpdateBuhinHyoujiJunVo)(sql, param)
        End Function

        '↑↑2014/10/23 酒井 ADD END
        '↓↓2014/10/07 酒井 ADD BEGIN
        Public Function FindBuhin4UpdateInstlHyoujiJun(ByVal blockVo As TShisakuSekkeiBlockVo) As List(Of Buhin4UpdateInstlHyoujiJunVo) Implements BuhinEditBaseDao.FindBuhin4UpdateInstlHyoujiJun
            Dim sql As String = _
" SELECT BEI.*,BE.BUHIN_NO,BUHIN_NO_KBN " _
& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI WITH (NOLOCK, NOWAIT) " _
& " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE " _
& " ON BE.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
& " AND BE.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
& " AND BE.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO " _
& " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO " _
& " AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN " _
& " WHERE  " _
& " BE.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
& " AND BE.LEVEL = 0 "

            Dim param As New Buhin4UpdateInstlHyoujiJunVo
            param.ShisakuEventCode = blockVo.ShisakuEventCode
            param.ShisakuBukaCode = blockVo.ShisakuBukaCode
            param.ShisakuBlockNo = blockVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = blockVo.ShisakuBlockNoKaiteiNo

            Dim db As New EBomDbClient
            Return db.QueryForList(Of Buhin4UpdateInstlHyoujiJunVo)(sql, param)
        End Function
        Public Function FindNewInstlHyoujiJun(ByVal Buhin4UpdateInstlHyoujiJunVos As List(Of Buhin4UpdateInstlHyoujiJunVo)) As List(Of Buhin4UpdateInstlHyoujiJunVo) Implements BuhinEditBaseDao.FindNewInstlHyoujiJun
            Dim vos As New List(Of Buhin4UpdateInstlHyoujiJunVo)

            For Each tmpVo In Buhin4UpdateInstlHyoujiJunVos

                Dim sql As String = _
    " SELECT DISTINCT INSTL_HINBAN_HYOUJI_JUN " _
    & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WITH (NOLOCK, NOWAIT) " _
    & " WHERE  " _
    & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
    & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
    & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
    & " AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
    & " AND INSTL_HINBAN = @InstlHinban " _
    & " AND INSTL_HINBAN_KBN = @InstlHinbanKbn "

                Dim param As New TShisakuSekkeiBlockInstlVo
                param.ShisakuEventCode = tmpVo.ShisakuEventCode
                param.ShisakuBukaCode = tmpVo.ShisakuBukaCode
                param.ShisakuBlockNo = tmpVo.ShisakuBlockNo
                param.ShisakuBlockNoKaiteiNo = tmpVo.ShisakuBlockNoKaiteiNo
                param.InstlHinban = tmpVo.BuhinNo
                param.InstlHinbanKbn = tmpVo.BuhinNoKbn

                Dim db As New EBomDbClient

                Dim sbiVo As TShisakuSekkeiBlockInstlVo = db.QueryForObject(Of TShisakuSekkeiBlockInstlVo)(sql, param)
                If Not sbiVo Is Nothing Then
                    tmpVo.NewInstlHinbanHyoujiJun = sbiVo.InstlHinbanHyoujiJun
                    vos.Add(tmpVo)
                End If
            Next

            Return vos
        End Function
        Public Function FindEventInstlHyoujiJun(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuSekkeiBlockInstlVo) Implements BuhinEditBaseDao.FindEventInstlHyoujiJun
            Dim sql As String = _
                        " SELECT DISTINCT INSTL_HINBAN_HYOUJI_JUN " _
                        & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WITH (NOLOCK, NOWAIT) " _
                        & " WHERE  " _
                        & " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                        & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
                        & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
                        & " AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
                        & " AND INSTL_DATA_KBN > '0' " _
                        & " AND BASE_INSTL_FLG = '1' " _
                        & " ORDER BY  INSTL_HINBAN_HYOUJI_JUN"


            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo

            Dim db As New EBomDbClient

            Return db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sql, param)
        End Function

        Public Sub UpdateNewInstlHyoujiJun(ByVal Buhin4UpdateInstlHyoujiJunVos As List(Of Buhin4UpdateInstlHyoujiJunVo)) Implements BuhinEditBaseDao.UpdateNewInstlHyoujiJun
            For index As Integer = 0 To Buhin4UpdateInstlHyoujiJunVos.Count - 1

                Dim sb As New StringBuilder
                With sb
                    .Remove(0, .Length)
                    .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL ")
                    .AppendLine(" SET INSTL_HINBAN_HYOUJI_JUN = " & Buhin4UpdateInstlHyoujiJunVos(index).NewInstlHinbanHyoujiJun)
                    .AppendLine(" WHERE INSTL_HINBAN_HYOUJI_JUN = @InstlHinbanHyoujiJun ")
                    .AppendLine(" AND SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                    .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                End With

                Dim param As New TShisakuBuhinEditInstlVo
                param.ShisakuEventCode = Buhin4UpdateInstlHyoujiJunVos(index).ShisakuEventCode
                param.ShisakuBukaCode = Buhin4UpdateInstlHyoujiJunVos(index).ShisakuBukaCode
                param.ShisakuBlockNo = Buhin4UpdateInstlHyoujiJunVos(index).ShisakuBlockNo
                param.ShisakuBlockNoKaiteiNo = Buhin4UpdateInstlHyoujiJunVos(index).ShisakuBlockNoKaiteiNo
                param.InstlHinbanHyoujiJun = Buhin4UpdateInstlHyoujiJunVos(index).InstlHinbanHyoujiJun

                Dim db As New EBomDbClient
                db.Update(sb.ToString, param)

                With sb
                    .Remove(0, .Length)
                    .AppendLine(" UPDATE  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE ")
                    .AppendLine(" SET INSTL_HINBAN_HYOUJI_JUN = " & Buhin4UpdateInstlHyoujiJunVos(index).NewInstlHinbanHyoujiJun)
                    .AppendLine(" WHERE INSTL_HINBAN_HYOUJI_JUN = @InstlHinbanHyoujiJun ")
                    .AppendLine(" AND SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                    .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                End With

                Dim paramBase As New TShisakuBuhinEditInstlBaseVo
                paramBase.ShisakuEventCode = Buhin4UpdateInstlHyoujiJunVos(index).ShisakuEventCode
                paramBase.ShisakuBukaCode = Buhin4UpdateInstlHyoujiJunVos(index).ShisakuBukaCode
                paramBase.ShisakuBlockNo = Buhin4UpdateInstlHyoujiJunVos(index).ShisakuBlockNo
                paramBase.ShisakuBlockNoKaiteiNo = Buhin4UpdateInstlHyoujiJunVos(index).ShisakuBlockNoKaiteiNo
                paramBase.InstlHinbanHyoujiJun = Buhin4UpdateInstlHyoujiJunVos(index).InstlHinbanHyoujiJun

                db.Update(sb.ToString, paramBase)

            Next
        End Sub
        Public Sub UpdateNewInstlHyoujiJun2(ByVal blockVo As TShisakuSekkeiBlockVo) Implements BuhinEditBaseDao.UpdateNewInstlHyoujiJun2

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL")
                .AppendLine(" SET INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN")
                .AppendLine(" FROM   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI")
                .AppendLine(" INNER JOIN   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                .AppendLine(" ON SBI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SBI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND SBI.INSTL_HINBAN = BE.BUHIN_NO ")
                .AppendLine(" AND SBI.INSTL_HINBAN_KBN = BE.BUHIN_NO_KBN ")
                .AppendLine(" INNER JOIN   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL ")
                .AppendLine(" ON BE.SHISAKU_EVENT_CODE = T_SHISAKU_BUHIN_EDIT_INSTL.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BE.SHISAKU_BUKA_CODE = T_SHISAKU_BUHIN_EDIT_INSTL.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO = T_SHISAKU_BUHIN_EDIT_INSTL.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = T_SHISAKU_BUHIN_EDIT_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BE.BUHIN_NO_HYOUJI_JUN = T_SHISAKU_BUHIN_EDIT_INSTL.BUHIN_NO_HYOUJI_JUN")
                .AppendLine(" WHERE (SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode) ")
                .AppendLine(" AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND (SBI.INSTL_DATA_KBN > '0') ")
                .AppendLine(" AND (SBI.BASE_INSTL_FLG = '1') ")
                .AppendLine(" AND (BE.LEVEL = 0)")
            End With

            Dim param As New TShisakuBuhinEditInstlVo
            param.ShisakuEventCode = blockVo.ShisakuEventCode
            param.ShisakuBukaCode = blockVo.ShisakuBukaCode
            param.ShisakuBlockNo = blockVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = blockVo.ShisakuBlockNoKaiteiNo

            Dim db As New EBomDbClient
            db.Update(sb.ToString, param)

        End Sub

        '↑↑2014/10/07 酒井 ADD END
        '↓↓2014/10/23 酒井 ADD BEGIN
        Public Sub UpdateNewBuhinHyoujiJun(ByVal UpdateVo As Buhin4UpdateBuhinHyoujiJunVo) Implements BuhinEditBaseDao.UpdateNewBuhinHyoujiJun

            Dim db As New EBomDbClient
            Dim sb As New StringBuilder

            '更新元レコードを取得
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL  WITH (NOLOCK, NOWAIT)")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
            End With

            Dim param As New TShisakuBuhinEditInstlVo
            param.ShisakuEventCode = UpdateVo.ShisakuEventCode
            param.ShisakuBukaCode = UpdateVo.ShisakuBukaCode
            param.ShisakuBlockNo = UpdateVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = UpdateVo.ShisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = UpdateVo.BuhinNoHyoujiJun

            Dim beiVos As List(Of TShisakuBuhinEditInstlVo) = db.QueryForList(Of TShisakuBuhinEditInstlVo)(sb.ToString, param)

            '更新先レコードの存在有無チェック（PK重複チェック）
            For Each beiVo As TShisakuBuhinEditInstlVo In beiVos
                With sb
                    .Remove(0, .Length)
                    .AppendLine(" SELECT * FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL  WITH (NOLOCK, NOWAIT)")
                    .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                    .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                    .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
                    .AppendLine(" AND INSTL_HINBAN_HYOUJI_JUN = @InstlHinbanHyoujiJun ")

                    param = New TShisakuBuhinEditInstlVo
                    param.ShisakuEventCode = beiVo.ShisakuEventCode
                    param.ShisakuBukaCode = beiVo.ShisakuBukaCode
                    param.ShisakuBlockNo = beiVo.ShisakuBlockNo
                    param.ShisakuBlockNoKaiteiNo = beiVo.ShisakuBlockNoKaiteiNo
                    param.BuhinNoHyoujiJun = UpdateVo.BuhinNoHyoujiJunNew
                    param.InstlHinbanHyoujiJun = beiVo.InstlHinbanHyoujiJun
                End With

                Dim beiVos2 As List(Of TShisakuBuhinEditInstlVo) = db.QueryForList(Of TShisakuBuhinEditInstlVo)(sb.ToString, param)
                If Not beiVos2 Is Nothing AndAlso Not beiVos2.Count = 0 Then
                    'キー重複ありの場合、INSUを加算＋元レコードを削除
                    With sb
                        .Remove(0, .Length)
                        .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL ")
                        .AppendLine(" SET INSU_SURYO = INSU_SURYO +  " & beiVo.InsuSuryo)
                        .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                        .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                        .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                        .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                        .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
                        .AppendLine(" AND INSTL_HINBAN_HYOUJI_JUN = @InstlHinbanHyoujiJun ")
                    End With

                    Dim param4 As New TShisakuBuhinEditInstlVo
                    param4.ShisakuEventCode = beiVo.ShisakuEventCode
                    param4.ShisakuBukaCode = beiVo.ShisakuBukaCode
                    param4.ShisakuBlockNo = beiVo.ShisakuBlockNo
                    param4.ShisakuBlockNoKaiteiNo = beiVo.ShisakuBlockNoKaiteiNo
                    param4.BuhinNoHyoujiJun = UpdateVo.BuhinNoHyoujiJunNew
                    param4.InstlHinbanHyoujiJun = beiVo.InstlHinbanHyoujiJun

                    db.Update(sb.ToString, param4)

                    With sb
                        .Remove(0, .Length)
                        .AppendLine(" DELETE FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL ")
                        .AppendLine(" WHERE SHISAKU_EVENT_CODE = '" & beiVo.ShisakuEventCode & "'")
                        .AppendLine(" AND SHISAKU_BUKA_CODE = '" & beiVo.ShisakuBukaCode & "'")
                        .AppendLine(" AND SHISAKU_BLOCK_NO = '" & beiVo.ShisakuBlockNo & "'")
                        .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = '" & beiVo.ShisakuBlockNoKaiteiNo & "'")
                        .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = '" & beiVo.BuhinNoHyoujiJun & "'")
                        .AppendLine(" AND INSTL_HINBAN_HYOUJI_JUN = '" & beiVo.InstlHinbanHyoujiJun & "'")
                    End With

                    db.Delete(sb.ToString)

                Else
                    'キー重複なしの場合、UPDATE
                    With sb
                        .Remove(0, .Length)
                        .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL ")
                        .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = " & UpdateVo.BuhinNoHyoujiJunNew)
                        .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                        .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                        .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                        .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                        .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
                        .AppendLine(" AND INSTL_HINBAN_HYOUJI_JUN = @InstlHinbanHyoujiJun ")
                    End With

                    Dim param4 As New TShisakuBuhinEditInstlVo
                    param4.ShisakuEventCode = beiVo.ShisakuEventCode
                    param4.ShisakuBukaCode = beiVo.ShisakuBukaCode
                    param4.ShisakuBlockNo = beiVo.ShisakuBlockNo
                    param4.ShisakuBlockNoKaiteiNo = beiVo.ShisakuBlockNoKaiteiNo
                    param4.BuhinNoHyoujiJun = beiVo.BuhinNoHyoujiJun
                    param4.InstlHinbanHyoujiJun = beiVo.InstlHinbanHyoujiJun

                    db.Update(sb.ToString, param4)
                End If
            Next

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT ")
                .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = BUHIN_NO_HYOUJI_JUN - 1 ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN >= @BuhinNoHyoujiJun ")
            End With

            Dim param2 As New TShisakuBuhinEditVo
            param2.ShisakuEventCode = UpdateVo.ShisakuEventCode
            param2.ShisakuBukaCode = UpdateVo.ShisakuBukaCode
            param2.ShisakuBlockNo = UpdateVo.ShisakuBlockNo
            param2.ShisakuBlockNoKaiteiNo = UpdateVo.ShisakuBlockNoKaiteiNo
            param2.BuhinNoHyoujiJun = UpdateVo.BuhinNoHyoujiJun

            db.Update(sb.ToString, param2)

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL ")
                .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = BUHIN_NO_HYOUJI_JUN - 1 ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN >= @BuhinNoHyoujiJun ")
            End With

            Dim param3 As New TShisakuBuhinEditInstlVo
            param3.ShisakuEventCode = UpdateVo.ShisakuEventCode
            param3.ShisakuBukaCode = UpdateVo.ShisakuBukaCode
            param3.ShisakuBlockNo = UpdateVo.ShisakuBlockNo
            param3.ShisakuBlockNoKaiteiNo = UpdateVo.ShisakuBlockNoKaiteiNo
            param3.BuhinNoHyoujiJun = UpdateVo.BuhinNoHyoujiJun

            db.Update(sb.ToString, param3)

        End Sub
        Public Sub UpdateNewBuhinHyoujiJunBase(ByVal UpdateVo As Buhin4UpdateBuhinHyoujiJunVo) Implements BuhinEditBaseDao.UpdateNewBuhinHyoujiJunBase
            Dim db As New EBomDbClient
            Dim sb As New StringBuilder

            '更新元レコードを取得
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE  WITH (NOLOCK, NOWAIT)")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
            End With

            Dim param As New TShisakuBuhinEditInstlVo
            param.ShisakuEventCode = UpdateVo.ShisakuEventCode
            param.ShisakuBukaCode = UpdateVo.ShisakuBukaCode
            param.ShisakuBlockNo = UpdateVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = UpdateVo.ShisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = UpdateVo.BuhinNoHyoujiJun

            Dim beiVos As List(Of TShisakuBuhinEditInstlVo) = db.QueryForList(Of TShisakuBuhinEditInstlVo)(sb.ToString, param)

            '更新先レコードの存在有無チェック（PK重複チェック）
            For Each beiVo As TShisakuBuhinEditInstlVo In beiVos
                With sb
                    .Remove(0, .Length)
                    .AppendLine(" SELECT * FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE  WITH (NOLOCK, NOWAIT)")
                    .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                    .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                    .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
                    .AppendLine(" AND INSTL_HINBAN_HYOUJI_JUN = @InstlHinbanHyoujiJun ")

                    param = New TShisakuBuhinEditInstlVo
                    param.ShisakuEventCode = beiVo.ShisakuEventCode
                    param.ShisakuBukaCode = beiVo.ShisakuBukaCode
                    param.ShisakuBlockNo = beiVo.ShisakuBlockNo
                    param.ShisakuBlockNoKaiteiNo = beiVo.ShisakuBlockNoKaiteiNo
                    param.BuhinNoHyoujiJun = UpdateVo.BuhinNoHyoujiJunNew
                    param.InstlHinbanHyoujiJun = beiVo.InstlHinbanHyoujiJun
                End With

                Dim beiVos2 As List(Of TShisakuBuhinEditInstlVo) = db.QueryForList(Of TShisakuBuhinEditInstlVo)(sb.ToString, param)
                If Not beiVos2 Is Nothing AndAlso Not beiVos2.Count = 0 Then
                    'キー重複ありの場合、INSUを加算＋元レコードを削除

                    With sb
                        .Remove(0, .Length)
                        .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE ")
                        .AppendLine(" SET INSU_SURYO = INSU_SURYO +  " & beiVo.InsuSuryo)
                        .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                        .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                        .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                        .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                        .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
                        .AppendLine(" AND INSTL_HINBAN_HYOUJI_JUN = @InstlHinbanHyoujiJun ")
                    End With

                    Dim param4 As New TShisakuBuhinEditInstlVo
                    param4.ShisakuEventCode = beiVo.ShisakuEventCode
                    param4.ShisakuBukaCode = beiVo.ShisakuBukaCode
                    param4.ShisakuBlockNo = beiVo.ShisakuBlockNo
                    param4.ShisakuBlockNoKaiteiNo = beiVo.ShisakuBlockNoKaiteiNo
                    param4.BuhinNoHyoujiJun = UpdateVo.BuhinNoHyoujiJunNew
                    param4.InstlHinbanHyoujiJun = beiVo.InstlHinbanHyoujiJun

                    db.Update(sb.ToString, param4)

                    With sb
                        .Remove(0, .Length)
                        .AppendLine(" DELETE FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE ")
                        .AppendLine(" WHERE SHISAKU_EVENT_CODE = '" & beiVo.ShisakuEventCode & "'")
                        .AppendLine(" AND SHISAKU_BUKA_CODE = '" & beiVo.ShisakuBukaCode & "'")
                        .AppendLine(" AND SHISAKU_BLOCK_NO = '" & beiVo.ShisakuBlockNo & "'")
                        .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = '" & beiVo.ShisakuBlockNoKaiteiNo & "'")
                        .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = '" & beiVo.BuhinNoHyoujiJun & "'")
                        .AppendLine(" AND INSTL_HINBAN_HYOUJI_JUN = '" & beiVo.InstlHinbanHyoujiJun & "'")
                    End With

                    db.Delete(sb.ToString)

                Else
                    'キー重複なしの場合、UPDATE
                    With sb
                        .Remove(0, .Length)
                        .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE ")
                        .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = " & UpdateVo.BuhinNoHyoujiJunNew)
                        .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                        .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                        .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                        .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                        .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
                        .AppendLine(" AND INSTL_HINBAN_HYOUJI_JUN = @InstlHinbanHyoujiJun ")
                    End With

                    Dim param4 As New TShisakuBuhinEditInstlVo
                    param4.ShisakuEventCode = beiVo.ShisakuEventCode
                    param4.ShisakuBukaCode = beiVo.ShisakuBukaCode
                    param4.ShisakuBlockNo = beiVo.ShisakuBlockNo
                    param4.ShisakuBlockNoKaiteiNo = beiVo.ShisakuBlockNoKaiteiNo
                    param4.BuhinNoHyoujiJun = beiVo.BuhinNoHyoujiJun
                    param4.InstlHinbanHyoujiJun = beiVo.InstlHinbanHyoujiJun

                    db.Update(sb.ToString, param4)
                End If
            Next

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE ")
                .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = BUHIN_NO_HYOUJI_JUN - 1 ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN >= @BuhinNoHyoujiJun ")
            End With

            Dim param2 As New TShisakuBuhinEditBaseVo
            param2.ShisakuEventCode = UpdateVo.ShisakuEventCode
            param2.ShisakuBukaCode = UpdateVo.ShisakuBukaCode
            param2.ShisakuBlockNo = UpdateVo.ShisakuBlockNo
            param2.ShisakuBlockNoKaiteiNo = UpdateVo.ShisakuBlockNoKaiteiNo
            param2.BuhinNoHyoujiJun = UpdateVo.BuhinNoHyoujiJun

            db.Update(sb.ToString, param2)

            With sb
                .Remove(0, .Length)
                .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE ")
                .AppendLine(" SET BUHIN_NO_HYOUJI_JUN = BUHIN_NO_HYOUJI_JUN - 1 ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN >= @BuhinNoHyoujiJun ")
            End With

            Dim param3 As New TShisakuBuhinEditInstlBaseVo
            param3.ShisakuEventCode = UpdateVo.ShisakuEventCode
            param3.ShisakuBukaCode = UpdateVo.ShisakuBukaCode
            param3.ShisakuBlockNo = UpdateVo.ShisakuBlockNo
            param3.ShisakuBlockNoKaiteiNo = UpdateVo.ShisakuBlockNoKaiteiNo
            param3.BuhinNoHyoujiJun = UpdateVo.BuhinNoHyoujiJun

            db.Update(sb.ToString, param3)

        End Sub

        '↑↑2014/10/23 酒井 ADD END
        ''' <summary>
        ''' 部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEdit2(ByVal orgShisakuEventCode As String, _
                                         ByVal shisakuEventCode As String, _
                                         ByVal shisakuBlockNo As String, _
                                         ByVal instlHinban As String, _
                                         ByVal instlHinbanKbn As String, _
                                         ByVal InstlDataKbn As String, _
                                         ByVal instlHinbanHyojijyun As Integer, _
                                        Optional ByVal BaseInstlFlg As String = "") As List(Of TShisakuBuhinEditVo) Implements BuhinEditBaseDao.FindByBuhinEdit2
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT DISTINCT BE.SHISAKU_EVENT_CODE, ")
                .AppendLine(" T.SHISAKU_BUKA_CODE, ")
                .AppendLine(" BE.SHISAKU_BLOCK_NO, ")
                .AppendLine(" BE.SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine(" BE.BUHIN_NO_HYOUJI_JUN, ")
                .AppendLine(" BE.LEVEL, ")
                .AppendLine(" BE.SHUKEI_CODE, ")
                .AppendLine(" BE.SIA_SHUKEI_CODE, ")
                .AppendLine(" BE.GENCYO_CKD_KBN, ")
                .AppendLine(" BE.KYOUKU_SECTION, ")
                .AppendLine(" BE.MAKER_CODE, ")
                .AppendLine(" BE.MAKER_NAME, ")
                .AppendLine(" BE.BUHIN_NO, ")
                .AppendLine(" BE.BUHIN_NO_KBN, ")
                .AppendLine(" BE.BUHIN_NO_KAITEI_NO, ")
                .AppendLine(" BE.EDA_BAN, ")
                .AppendLine(" BE.BUHIN_NAME, ")
                .AppendLine(" BE.SAISHIYOUFUKA, ")
                .AppendLine(" BE.SHUTUZU_YOTEI_DATE, ")
                .AppendLine(" BE.TSUKURIKATA_SEISAKU, ")
                .AppendLine(" BE.TSUKURIKATA_KATASHIYOU_1, ")
                .AppendLine(" BE.TSUKURIKATA_KATASHIYOU_2, ")
                .AppendLine(" BE.TSUKURIKATA_KATASHIYOU_3, ")
                .AppendLine(" BE.TSUKURIKATA_TIGU, ")
                .AppendLine(" BE.TSUKURIKATA_NOUNYU, ")
                .AppendLine(" BE.TSUKURIKATA_KIBO, ")
                .AppendLine(" BE.BASE_BUHIN_FLG, ")
                .AppendLine(" BE.ZAISHITU_KIKAKU_1, ")
                .AppendLine(" BE.ZAISHITU_KIKAKU_2, ")
                .AppendLine(" BE.ZAISHITU_KIKAKU_3, ")
                .AppendLine(" BE.ZAISHITU_MEKKI, ")
                .AppendLine(" BE.SHISAKU_BANKO_SURYO, ")
                .AppendLine(" BE.SHISAKU_BANKO_SURYO_U, ")


                ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
                .AppendLine(" BE.MATERIAL_INFO_LENGTH, ")
                .AppendLine(" BE.MATERIAL_INFO_WIDTH, ")
                .AppendLine(" BE.DATA_ITEM_KAITEI_NO, ")
                .AppendLine(" BE.DATA_ITEM_AREA_NAME, ")
                .AppendLine(" BE.DATA_ITEM_SET_NAME, ")
                .AppendLine(" BE.DATA_ITEM_KAITEI_INFO, ")
                ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


                .AppendLine(" BE.SHISAKU_BUHIN_HI, ")
                .AppendLine(" BE.SHISAKU_KATA_HI, ")
                .AppendLine(" BE.BIKOU, ")
                .AppendLine(" BE.BUHIN_NOTE, ")
                .AppendLine(" BE.EDIT_TOUROKUBI, ")
                .AppendLine(" BE.EDIT_TOUROKUJIKAN, ")
                .AppendLine(" BE.KAITEI_HANDAN_FLG, ")
                .AppendLine(" BE.SHISAKU_LIST_CODE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                ''↓↓2014/08/21 1 ベース部品表作成表機能増強_Ⅲ②＋④(2) (TES)張 ADD BEGIN
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BLOCK_SEKKEIKA_TMP T ")
                .AppendFormat(" ON  T.SHISAKU_EVENT_CODE = '{0}' ", orgShisakuEventCode)
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO = T.SHISAKU_BLOCK_NO ")

                ''↓↓2014/08/27 1 ベース部品表作成表機能増強_Ⅲ②＋④(2) 酒井 ADD BEGIN
                '.AppendLine(" AND BE.SHISAKU_BUKA_CODE = T.SHISAKU_BUKA_CODE ")
                ''↑↑2014/08/27 1 ベース部品表作成表機能増強_Ⅲ②＋④(2) 酒井 ADD END
                ''↑↑2014/08/21 1 ベース部品表作成表機能増強_Ⅲ②＋④(2) (TES)張 ADD END
                .AppendLine(" WHERE  ")
                .AppendFormat("  SBI.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SBI.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO  = ( ")
                .AppendLine("  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                .AppendLine("  WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine("  AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine("  AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" AND NOT SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")
                .AppendFormat(" AND SBI.INSTL_HINBAN = '{0}' ", instlHinban)
                .AppendFormat(" AND SBI.INSTL_HINBAN_KBN = '{0}' ", instlHinbanKbn)
                ''↓↓2014/08/21 1 ベース部品表作成表機能増強 ADD BEGIN
                .AppendFormat(" AND SBI.INSTL_DATA_KBN = '{0}' ", InstlDataKbn)
                ''↑↑2014/08/21 1 ベース部品表作成表機能増強 ADD END
                '↓↓2014/09/30 酒井 ADD BEGIN
                'If Not BaseInstlFlg = "" Then
                '    .AppendFormat(" AND SBI.BASE_INSTL_FLG = '{0}' ", BaseInstlFlg)
                'End If
                '↑↑2014/09/30 酒井 ADD END

                ''2015/09/01 追加 E.Ubukata Ver2.11.0
                '' 上記条件だけでは移管車改修のイベントで別の列に定義されているものが同一に扱われてしまうため追加
                .AppendFormat(" AND SBI.INSTL_HINBAN_HYOUJI_JUN = {0} ", instlHinbanHyojijyun)
            End With

            'Dim param As New TShisakuSekkeiBlockInstlVo
            'param.ShisakuEventCode = shisakuEventCode
            'param.ShisakuBlockNo = shisakuBlockNo
            'param.InstlHinban = instlHinban
            'param.InstlHinbanKbn = instlHinbanKbn
            'param.InstlDataKbn = InstlDataKbn
            ''↓↓2014/09/30 酒井 ADD BEGIN
            'If Not BaseInstlFlg = "" Then
            '    param.BaseInstlFlg = BaseInstlFlg
            'End If
            ''↑↑2014/09/30 酒井 ADD END

            ' ''2015/09/01
            'param.InstlHinbanHyoujiJun = instlHinbanHyojijyun

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 部品編集INSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditInstl2(ByVal orgShisakuEventCode As String, _
                                      ByVal shisakuEventCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal instlHinban As String, _
                                      ByVal instlHinbanKbn As String, _
                                      ByVal InstlDataKbn As String, _
                                      ByVal instlHinbanHyojijyun As Integer, _
                                      Optional ByVal BaseInstlFlg As String = "") As List(Of TShisakuBuhinEditInstlVo) Implements BuhinEditBaseDao.FindByBuhinEditInstl2
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                ''↓↓2014/08/21 1 ベース部品表作成表機能増強_Ⅲ②＋④ (TES)張 CHG BEGIN
                '.AppendLine(" SELECT DISTINCT BEI.* ")
                .AppendLine(" SELECT DISTINCT BEI.SHISAKU_EVENT_CODE, ")
                .AppendLine(" T.SHISAKU_BUKA_CODE, ")
                .AppendLine(" BEI.SHISAKU_BLOCK_NO, ")
                .AppendLine(" BEI.SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine(" BEI.BUHIN_NO_HYOUJI_JUN, ")
                .AppendLine(" BEI.INSTL_HINBAN_HYOUJI_JUN, ")
                .AppendLine(" BEI.INSU_SURYO ")
                ''↑↑2014/08/21 1 ベース部品表作成表機能増強_Ⅲ②＋④ (TES)張 CHG END
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                ''↓↓2014/08/21 1 ベース部品表作成表機能増強_Ⅲ②＋④ (TES)張 ADD BEGIN
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BLOCK_SEKKEIKA_TMP T ")
                .AppendFormat(" ON  T.SHISAKU_EVENT_CODE = '{0}' ", orgShisakuEventCode)
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = T.SHISAKU_BLOCK_NO ")
                ''↓↓2014/08/27 1 ベース部品表作成表機能増強_Ⅲ②＋④ 酒井 ADD BEGIN
                '.AppendLine(" AND BEI.SHISAKU_BUKA_CODE = T.SHISAKU_BUKA_CODE ")
                ''↑↑2014/08/27 1 ベース部品表作成表機能増強_Ⅲ②＋④ 酒井 ADD END
                ''↑↑2014/08/21 1 ベース部品表作成表機能増強_Ⅲ②＋④ (TES)張 ADD END
                .AppendLine(" WHERE  ")
                .AppendFormat("  SBI.SHISAKU_EVENT_CODE ='{0}' ", shisakuEventCode)
                .AppendFormat(" AND SBI.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO  = ( ")

                ''2015/08/04 E.Ubukata 変更
                '.AppendLine("  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine("  SELECT MAX ( SHISAKU_BLOCK_NO_KAITEI_NO ) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")


                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                .AppendLine("  WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine("  AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine("  AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" AND NOT SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")
                .AppendFormat(" AND SBI.INSTL_HINBAN = '{0}' ", instlHinban)
                .AppendFormat(" AND SBI.INSTL_HINBAN_KBN = '{0}' ", instlHinbanKbn)
                ''↓↓2014/08/21 1 ベース部品表作成表機能増強 ADD BEGIN
                .AppendFormat(" AND SBI.INSTL_DATA_KBN = '{0}' ", InstlDataKbn)
                ''↑↑2014/08/21 1 ベース部品表作成表機能増強 ADD END
                '↓↓2014/09/30 酒井 ADD BEGIN
                'If Not BaseInstlFlg = "" Then
                '    .AppendFormat(" AND SBI.BASE_INSTL_FLG = '{0}' ", BaseInstlFlg)
                'End If
                '↑↑2014/09/30 酒井 ADD END

                ''2015/09/01 追加 E.Ubukata Ver2.11.0
                '' 上記条件だけでは移管車改修のイベントで別の列に定義されているものが同一に扱われてしまうため追加
                .AppendFormat(" AND SBI.INSTL_HINBAN_HYOUJI_JUN = {0} ", instlHinbanHyojijyun)

            End With

            'Dim param As New TShisakuSekkeiBlockInstlVo
            'param.ShisakuEventCode = shisakuEventCode
            'param.ShisakuBlockNo = shisakuBlockNo
            'param.InstlHinban = instlHinban
            'param.InstlHinbanKbn = instlHinbanKbn
            'param.InstlDataKbn = InstlDataKbn
            ''↓↓2014/09/30 酒井 ADD BEGIN
            'If Not BaseInstlFlg = "" Then
            '    param.BaseInstlFlg = BaseInstlFlg
            'End If
            ''↑↑2014/09/30 酒井 ADD END

            ' ''2015/09/01
            'param.InstlHinbanHyoujiJun = instlHinbanHyojijyun


            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditInstlVo)(sb.ToString)
        End Function


        ''' <summary>
        ''' 部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditKai(ByVal shisakuEventCode As String, _
                                        ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditVo) Implements BuhinEditBaseDao.FindByBuhinEditKai
            Dim sql As String = _
            " SELECT DISTINCT SBI.INSTL_HINBAN AS BUHIN_NO ,SBI.INSTL_HINBAN_KBN AS BUHIN_NO_KBN, " _
            & " BE.SHISAKU_EVENT_CODE,BE.SHISAKU_BUKA_CODE,BE.SHISAKU_BLOCK_NO,BE.SHISAKU_BLOCK_NO_KAITEI_NO,BE.BUHIN_NO_HYOUJI_JUN,BE.LEVEL,BE.SHUKEI_CODE,BE.SIA_SHUKEI_CODE,BE.GENCYO_CKD_KBN,BE.KYOUKU_SECTION,BE.MAKER_CODE,BE.MAKER_NAME,BE.BUHIN_NO_KAITEI_NO,BE.EDA_BAN,BE.BUHIN_NAME,BE.SAISHIYOUFUKA,BE.SHUTUZU_YOTEI_DATE,BE.ZAISHITU_KIKAKU_1,BE.ZAISHITU_KIKAKU_2,BE.ZAISHITU_KIKAKU_3,BE.ZAISHITU_MEKKI,BE.SHISAKU_BANKO_SURYO,BE.SHISAKU_BANKO_SURYO_U,BE.SHISAKU_BUHIN_HI,BE.SHISAKU_KATA_HI,BE.BUHIN_NOTE,BE.EDIT_TOUROKUBI,BE.EDIT_TOUROKUJIKAN,BE.KAITEI_HANDAN_FLG,BE.SHISAKU_LIST_CODE,BE.CREATED_USER_ID,BE.CREATED_DATE,BE.CREATED_TIME,BE.UPDATED_USER_ID,BE.UPDATED_DATE,BE.UPDATED_TIME " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B WITH (NOLOCK, NOWAIT) " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            & " ON SBI.SHISAKU_EVENT_CODE = B.SHISAKU_BASE_EVENT_CODE " _
            & " AND SBI.SHISAKU_GOUSYA = B.SHISAKU_BASE_GOUSYA " _
            & " AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO  = ( " _
            & "  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  " _
            & "  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL " _
            & "  WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & "  AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            & "  AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) " _
            & " AND NOT SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
            & " ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & " AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            & " AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO " _
            & " AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') " _
            & " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
            & " ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE " _
            & " AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE " _
            & " AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO " _
            & " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE " _
            & " ON BE.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE " _
            & " AND BE.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE " _
            & " AND BE.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO " _
            & " AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN " _
            & " WHERE  " _
            & " B.SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim param As New TShisakuBuhinEditVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = shisakuBlockNo

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditVo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品編集INSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditInstl(ByVal shisakuEventCode As String, _
                                             ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditInstlVo) Implements BuhinEditBaseDao.FindByBuhinEditInstl
            Dim sql As String = _
            " SELECT DISTINCT BEI.* " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B WITH (NOLOCK, NOWAIT) " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            & " ON SBI.SHISAKU_EVENT_CODE = B.SHISAKU_BASE_EVENT_CODE " _
            & " AND SBI.SHISAKU_GOUSYA = B.SHISAKU_BASE_GOUSYA " _
            & " AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO  = ( " _
            & "  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  " _
            & "  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL " _
            & "  WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & "  AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            & "  AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) " _
            & " AND NOT SHISAKU_BLOCK_NO_KAITEI_NO = '  0' " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
            & " ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE " _
            & " AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE " _
            & " AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO " _
            & " AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') " _
            & " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI " _
            & " ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE " _
            & " AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE " _
            & " AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO " _
            & " AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN " _
            & " WHERE  " _
            & " B.SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim param As New TShisakuBuhinEditVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = shisakuBlockNo

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditInstlVo)(sql, param)
        End Function

        ''' <summary>
        ''' 設計ブロックINSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByInstlHinban(ByVal shisakuEventCode As String, _
                                          ByVal shisakuBukaCode As String, _
                                          ByVal shisakuBlockNo As String, _
                                          ByVal instlHinban As String, _
                                          ByVal instlHinbanKbn As String) As TShisakuSekkeiBlockInstlVo Implements BuhinEditBaseDao.FindByInstlHinban

            Dim sql As String = _
            " SELECT * " _
            & " FROM T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            & " WHERE " _
            & " SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlock_No " _
            & " AND SBI.INSTL_HINBAN = @InstlHinban " _
            & " AND SBI.INSTL_HINBAN_KBN = @InstlHinbanKbn "

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.InstlHinban = instlHinban
            param.InstlHinbanKbn = instlHinbanKbn

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuSekkeiBlockInstlVo)(sql)

        End Function

        Public Function FindByInstlHinbanBaseEvent(ByVal shisakuEventCode As String, _
                          ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String, _
                          ByVal instlHinban As String, _
                          ByVal instlHinbanKbn As String) As TShisakuSekkeiBlockInstlVo Implements BuhinEditBaseDao.FindByInstlHinbanBaseEvent

            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            & " WHERE " _
            & " SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SBI.INSTL_HINBAN = @InstlHinban " _
            & " AND SBI.INSTL_HINBAN_KBN = @InstlHinbanKbn " _
            & " AND SBI.INSTL_DATA_KBN <> '0' " _
            & " AND SBI.BASE_INSTL_FLG = '1' "

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.InstlHinban = instlHinban
            param.InstlHinbanKbn = instlHinbanKbn

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuSekkeiBlockInstlVo)(sql, param)

        End Function

        Public Function FindByInstlHinbanBaseEbom(ByVal shisakuEventCode As String, _
                                  ByVal shisakuBukaCode As String, _
                                  ByVal shisakuBlockNo As String, _
                                  ByVal instlHinban As String, _
                                  ByVal instlHinbanKbn As String, _
                                  ByVal instlDataKbn As String, _
                                  ByVal baseInstlFlg As String) As TShisakuSekkeiBlockInstlVo Implements BuhinEditBaseDao.FindByInstlHinbanBaseEbom
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI " _
            & " WHERE " _
            & " SBI.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SBI.SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SBI.INSTL_HINBAN = @InstlHinban " _
            & " AND SBI.INSTL_HINBAN_KBN = @InstlHinbanKbn " _
            & " AND SBI.INSTL_DATA_KBN = @InstlDataKbn " _
            & " AND SBI.BASE_INSTL_FLG = @BaseInstlFlg "

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.InstlHinban = instlHinban
            param.InstlHinbanKbn = instlHinbanKbn
            param.InstlDataKbn = instlDataKbn
            param.BaseInstlFlg = baseInstlFlg

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuSekkeiBlockInstlVo)(sql, param)

        End Function
        ''↑↑2014/09/15 1 ベース部品表作成表機能増強 酒井ADD END
        ''' <summary>
        ''' 設計ブロックINSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindBySekkeiBlockInstl(ByVal shisakuEventCode As String, _
                                               ByVal shisakuBlockNo As String) As List(Of TShisakuSekkeiBlockInstlVoEx) Implements BuhinEditBaseDao.FindBySekkeiBlockInstl
            Dim sb As New StringBuilder
            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = shisakuBlockNo

            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT DISTINCT SBI.SHISAKU_EVENT_CODE, SBI.SHISAKU_BUKA_CODE, SBI.SHISAKU_BLOCK_NO, SBI2.INSTL_HINBAN, ")
                .AppendLine(" SBI.INSTL_DATA_KBN, ")
                .AppendLine(" SBI.BASE_INSTL_FLG, ")
                .AppendLine(" SBI2.INSTL_DATA_KBN AS INSTL_DATA_KBN_2, ")
                .AppendLine(" SBI2.BASE_INSTL_FLG AS BASE_INSTL_FLG_2, ")
                .AppendLine(" SBI.INSTL_HINBAN_KBN, ")
                .AppendLine(" SBI2.INSTL_HINBAN_HYOUJI_JUN,")
                .AppendLine(" SBI2.BASE_INSTL_HINBAN_HYOUJI_JUN AS OLD_INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B WITH (NOLOCK, NOWAIT) ")

                '↓ここで取り込んでいるレコードはコピー元の情報でINSTL_DATA_KBNやBASE_INSTL_FLGを取得しても元の情報では意味がない
                .AppendLine(" LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON SBI.SHISAKU_EVENT_CODE = B.SHISAKU_BASE_EVENT_CODE ")
                .AppendLine(" AND SBI.SHISAKU_GOUSYA = B.SHISAKU_BASE_GOUSYA ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO  = ( ")
                .AppendLine("  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("  WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine("  AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine("  AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO  ")
                .AppendLine("  AND SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA ) ")
                .AppendLine(" AND NOT SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")

                '↓
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI2 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON SBI2.SHISAKU_EVENT_CODE = B.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SBI2.SHISAKU_GOUSYA = B.SHISAKU_GOUSYA ")
                .AppendLine(" AND SBI2.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")

                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = SBI2.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = SBI2.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI2.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" WHERE  ")
                .AppendLine(" B.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" ORDER BY SBI2.INSTL_HINBAN_HYOUJI_JUN")
            End With


            Dim db As New EBomDbClient
            Dim rtn As New List(Of TShisakuSekkeiBlockInstlVoEx)
            For Each vo As TShisakuSekkeiBlockInstlVoEx In db.QueryForList(Of TShisakuSekkeiBlockInstlVoEx)(sb.ToString, param)
                If StringUtil.IsEmpty(vo.ShisakuEventCode) Then
                    rtn.Add(vo)
                End If
            Next

            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT DISTINCT SBI.SHISAKU_EVENT_CODE, SBI.SHISAKU_BUKA_CODE, SBI.SHISAKU_BLOCK_NO, SBI2.INSTL_HINBAN, ")
                .AppendLine(" SBI.INSTL_DATA_KBN, ")
                .AppendLine(" SBI.BASE_INSTL_FLG, ")
                .AppendLine(" SBI2.INSTL_DATA_KBN AS INSTL_DATA_KBN_2, ")
                .AppendLine(" SBI2.BASE_INSTL_FLG AS BASE_INSTL_FLG_2, ")
                .AppendLine(" SBI.INSTL_HINBAN_KBN, ")
                .AppendLine(" SBI2.INSTL_HINBAN_HYOUJI_JUN, ")
                .AppendLine(" SBI2.BASE_INSTL_HINBAN_HYOUJI_JUN AS OLD_INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B WITH (NOLOCK, NOWAIT) ")

                '↓ここで取り込んでいるレコードはコピー元の情報でINSTL_DATA_KBNやBASE_INSTL_FLGを取得しても元の情報では意味がない
                .AppendLine(" LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON SBI.SHISAKU_EVENT_CODE = B.SHISAKU_BASE_EVENT_CODE ")
                .AppendLine(" AND SBI.SHISAKU_GOUSYA = B.SHISAKU_BASE_GOUSYA ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO  = ( ")
                .AppendLine("  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("  WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine("  AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine("  AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO  ")
                .AppendLine("  AND SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA ) ")
                .AppendLine(" AND NOT SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")

                '↓
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI2 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON SBI2.SHISAKU_EVENT_CODE = B.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SBI2.SHISAKU_GOUSYA = B.SHISAKU_GOUSYA ")

                .AppendLine(" AND SBI2.INSTL_HINBAN = SBI.INSTL_HINBAN ")
                .AppendLine(" AND SBI2.INSTL_HINBAN_KBN = SBI.INSTL_HINBAN_KBN ")


                .AppendLine(" AND SBI2.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")

                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = SBI2.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = SBI2.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI2.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" WHERE  ")
                .AppendLine(" B.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" ORDER BY SBI2.INSTL_HINBAN_HYOUJI_JUN")
            End With

            rtn.AddRange(db.QueryForList(Of TShisakuSekkeiBlockInstlVoEx)(sb.ToString, param))

            Return rtn

        End Function

        ''' <summary>
        ''' 設計ブロック情報と設計ブロックINSTL情報を削除
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <remarks></remarks>
        Public Sub DeleteBySekkeiBlockAndInstl(ByVal shisakuEventCode As String, _
                                               ByVal shisakuBukaCode As String, _
                                               ByVal shisakuBlockNo As String) Implements BuhinEditBaseDao.DeleteBySekkeiBlockAndInstl
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" DELETE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = '" & shisakuBukaCode & "' ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "' ")
            End With

            Dim db As New EBomDbClient
            db.Delete(sb.ToString)

            With sb
                .Remove(0, .Length)
                .AppendLine(" DELETE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = '" & shisakuBukaCode & "' ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "' ")
            End With
            db.Delete(sb.ToString)



        End Sub

        ''↓↓2014/09/16 1 ベース部品表作成表機能増強 酒井 ADD BEGIN
        Public Sub DeleteByBuhinKousei(ByVal shisakuEventCode As String, _
                                   ByVal shisakuBukaCode As String, _
                                   ByVal shisakuBlockNo As String, _
                                   ByVal shisakuBlockNoKaiteiNo As String, _
                                   ByVal instlHinbanHyoujiJun As String, ByRef deleteBuhin As List(Of TShisakuBuhinEditVo)) Implements BuhinEditBaseDao.DeleteByBuhinKousei
            Dim sb As New StringBuilder

            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT BE.*")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT  BE")
                .AppendLine(" ON  BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE  ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE  ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" WHERE BEI.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = '" & shisakuBukaCode & "' ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "' ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = '" & shisakuBlockNoKaiteiNo & "' ")
                .AppendLine(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = " & instlHinbanHyoujiJun & " ")
                .AppendLine(" ORDER BY BE.BUHIN_NO_HYOUJI_JUN")
            End With
            Dim db As New EBomDbClient
            deleteBuhin = db.QueryForList(Of TShisakuBuhinEditVo)(sb.ToString)

            With sb
                .Remove(0, .Length)
                .AppendLine(" DELETE FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT  BE")
                .AppendLine(" ON  BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE  ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE  ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" WHERE BEI.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = '" & shisakuBukaCode & "' ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "' ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = '" & shisakuBlockNoKaiteiNo & "' ")
                .AppendLine(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = " & instlHinbanHyoujiJun & " ")
            End With
            db.Delete(sb.ToString)

            With sb
                .Remove(0, .Length)
                .AppendLine(" DELETE FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE  BE")
                .AppendLine(" ON  BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE  ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE  ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO  ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" WHERE BEI.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = '" & shisakuBukaCode & "' ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "' ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = '" & shisakuBlockNoKaiteiNo & "' ")
                .AppendLine(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = " & instlHinbanHyoujiJun & " ")
            End With
            db.Delete(sb.ToString)
        End Sub
        ''↑↑2014/09/16 1 ベース部品表作成表機能増強 酒井 ADD END

        ''↓↓2014/08/21 1 ベース部品表作成表機能増強_d ADD BEGIN
        ''' <summary>
        ''' 試作部品編集情報と員数を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinKousei(ByVal shisakuEventCode As String, _
                                   ByVal shisakuBukaCode As String, _
                                   ByVal shisakuBlockNo As String, _
                                   ByVal shisakuBlockNoKaiteiNo As String, _
                                   ByVal instlHinbanHyoujiJun As String) As List(Of BuhinEditInstlKoseiVo) Implements BuhinEditBaseDao.FindByBuhinKousei
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT BE.LEVEL, ")
                .AppendLine(" BE.SHUKEI_CODE, ")
                .AppendLine(" BE.SIA_SHUKEI_CODE, ")
                .AppendLine(" BE.BUHIN_NO, ")
                .AppendLine(" BE.MAKER_CODE, ")
                .AppendLine(" BE.KYOUKU_SECTION, ")
                .AppendLine(" TB.INSU_SURYO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL TB ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                .AppendLine(" ON TB.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE AND  ")
                .AppendLine(" TB.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE AND  ")
                .AppendLine(" TB.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO AND  ")
                .AppendLine(" TB.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN AND  ")
                .AppendLine(" TB.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" WHERE (TB.SHISAKU_EVENT_CODE = @ShisakuEventCode) AND  ")
                .AppendLine(" (TB.SHISAKU_BUKA_CODE = @ShisakuBukaCode) AND  ")
                .AppendLine(" (TB.SHISAKU_BLOCK_NO = @ShisakuBlockNo) AND  ")
                .AppendLine(" (TB.SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo) AND  ")
                .AppendLine(" (TB.INSTL_HINBAN_HYOUJI_JUN = @InstlHinbanHyoujiJun) ")
                .AppendLine(" ORDER BY BE.LEVEL, ")
                .AppendLine(" BE.SHUKEI_CODE, ")
                .AppendLine(" BE.SIA_SHUKEI_CODE, ")
                .AppendLine(" BE.BUHIN_NO, ")
                .AppendLine(" BE.MAKER_CODE, ")
                .AppendLine(" BE.KYOUKU_SECTION, ")
                .AppendLine(" TB.INSU_SURYO ")
            End With

            Dim param As New TShisakuBuhinEditInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
            param.ShisakuBukaCode = shisakuBukaCode
            param.InstlHinbanHyoujiJun = instlHinbanHyoujiJun
            Dim db As New EBomDbClient
            Return db.QueryForList(Of BuhinEditInstlKoseiVo)(sb.ToString, param)

        End Function
        ''↑↑2014/08/21 1 ベース部品表作成表機能増強_d ADD END

#Region "パンダ前"

        ''' <summary>
        ''' 部品構成情報を取得
        ''' </summary>
        ''' <param name="BuhinNoOya">部品番号(親)</param>
        ''' <returns>部品構成情報</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0551(ByVal BuhinNoOya As String) As List(Of Rhac0551Vo) Implements BuhinEditBaseDao.FindByRhac0551
            Dim sql As String = _
             " SELECT * " _
             & " FROM " _
             & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 WITH (NOLOCK, NOWAIT) " _
             & " WHERE " _
             & " BUHIN_NO_OYA = @BuhinNoOya "

            Dim db As New EBomDbClient
            Dim param As New Rhac0551Vo
            param.BuhinNoOya = BuhinNoOya

            Return db.QueryForList(Of Rhac0551Vo)(sql, param)

        End Function

        ''' <summary>
        ''' 部品情報を取得
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0530(ByVal BuhinNo As String) As Rhac0530Vo Implements BuhinEditBaseDao.FindByRhac0530
            Dim sql As String = _
             " SELECT R.* " _
             & " FROM " _
             & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 R WITH (NOLOCK, NOWAIT) " _
             & " WHERE " _
             & " R.BUHIN_NO = @BuhinNo " _
             & " AND R.HAISI_DATE = '99999999' " _
             & " AND R.KAITEI_NO = ( " _
             & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
             & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0530 WITH (NOLOCK, NOWAIT) " _
             & " WHERE BUHIN_NO = R.BUHIN_NO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0530Vo
            param.BuhinNo = BuhinNo

            Return db.QueryForObject(Of Rhac0530Vo)(sql, param)
        End Function

#End Region

#Region "図面"

        ''' <summary>
        ''' 部品構成情報を取得
        ''' </summary>
        ''' <param name="BuhinNoOya">部品番号(親)</param>
        ''' <returns>部品構成情報</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0552(ByVal BuhinNoOya As String) As List(Of Rhac0552Vo) Implements BuhinEditBaseDao.FindByRhac0552
            Dim sql As String = _
            " SELECT * " _
            & " FROM " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " BUHIN_NO_OYA = @BuhinNoOya "

            Dim db As New EBomDbClient
            Dim param As New Rhac0552Vo
            param.BuhinNoOya = BuhinNoOya

            Return db.QueryForList(Of Rhac0552Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品情報を取得
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0532(ByVal BuhinNo As String) As Rhac0532Vo Implements BuhinEditBaseDao.FindByRhac0532
            Dim sql As String = _
            " SELECT R.* " _
            & " FROM " _
            & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 R WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " R.BUHIN_NO = @BuhinNo " _
            & " AND R.KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 WITH (NOLOCK, NOWAIT) " _
            & " WHERE BUHIN_NO = R.BUHIN_NO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0532Vo
            param.BuhinNo = BuhinNo

            Return db.QueryForObject(Of Rhac0532Vo)(sql, param)
        End Function



#End Region

#Region "FM5以降"

        ''' <summary>
        ''' 部品構成情報を取得
        ''' </summary>
        ''' <param name="BuhinNoOya">部品番号(親)</param>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>部品構成情報</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0553(ByVal BuhinNoOya As String, ByVal kaihatsuFugo As String) As List(Of Rhac0553Vo) Implements BuhinEditBaseDao.FindByRhac0553
            Dim sql As String = _
             " SELECT R.* " _
             & " FROM " _
             & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 R WITH (NOLOCK, NOWAIT) " _
             & " WHERE " _
             & " R.BUHIN_NO_OYA = @BuhinNoOya " _
             & " AND R.KAIHATSU_FUGO = @KaihatsuFugo " _
             & " AND R.KAITEI_NO =  " _
             & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
             & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 WITH (NOLOCK, NOWAIT) " _
             & " WHERE BUHIN_NO = R.BUHIN_NO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0553Vo
            param.BuhinNoOya = BuhinNoOya
            param.KaihatsuFugo = kaihatsuFugo

            Return db.QueryForList(Of Rhac0553Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品情報を取得
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac0533(ByVal BuhinNo As String) As Rhac0533Vo Implements BuhinEditBaseDao.FindByRhac0533
            Dim sql As String = _
             " SELECT R.* " _
             & " FROM " _
             & " " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 R WITH (NOLOCK, NOWAIT) " _
             & " WHERE " _
             & " R.BUHIN_NO = @BuhinNo " _
             & " AND R.HAISI_DATE = '99999999' " _
             & " AND R.KAITEI_NO = ( " _
             & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
             & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0533 WITH (NOLOCK, NOWAIT) " _
             & " WHERE BUHIN_NO = R.BUHIN_NO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac0533Vo
            param.BuhinNo = BuhinNo

            Return db.QueryForObject(Of Rhac0533Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 部品情報を取得
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Public Function FindByRhac1910(ByVal kaihatsuFugo As String, ByVal BuhinNo As String) As Rhac1910Vo Implements BuhinEditBaseDao.FindByRhac1910
            Dim sql As String = _
             " SELECT R.* " _
             & " FROM " _
             & " " & RHACLIBF_DB_NAME & ".dbo.RHAC1910 R WITH (NOLOCK, NOWAIT) " _
             & " WHERE " _
             & " R.BUHIN_NO = @BuhinNo " _
             & " R.KAIHATSU_FUGO = @KiahatsuFugo " _
             & " AND R.HAISI_DATE = '99999999' " _
             & " AND R.KAITEI_NO = ( " _
             & " SELECT MAX ( CONVERT ( INT,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO " _
             & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC1910 WITH (NOLOCK, NOWAIT) " _
             & " WHERE BUHIN_NO = R.BUHIN_NO  " _
             & " AND KAIHATSU_FUGO = R.KAIHATSU_FUGO ) "

            Dim db As New EBomDbClient
            Dim param As New Rhac1910Vo
            param.BuhinNo = BuhinNo
            param.KaihatsuFugo = kaihatsuFugo

            Return db.QueryForObject(Of Rhac1910Vo)(sql, param)
        End Function

#End Region

    End Class
End Namespace
