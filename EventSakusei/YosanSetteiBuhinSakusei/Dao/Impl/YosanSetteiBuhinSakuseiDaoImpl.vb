Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.YosanSetteiBuhinSakusei.Dao
Imports EventSakusei.YosanSetteiBuhinSakusei.Vo
Imports EBom.Data
Imports EBom.Common
Imports System.Text

Imports System.Data.SqlClient


Namespace YosanSetteiBuhinSakusei.Dao
    Public Class YosanSetteiBuhinSakuseiDaoImpl : Inherits DaoEachFeatureImpl
        Implements YosanSetteiBuhinSakuseiDao

#Region "取得する処理(Find)"

        ''' <summary>
        ''' ダミーを除く号車表示順の最大値を取得
        ''' </summary>
        ''' <param name="shisakueventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByMaxGousyaHyoujijunNotDummy(ByVal shisakuEventCode As String, _
                                                         ByVal shisakuListCode As String) As Integer Implements YosanSetteiBuhinSakuseiDao.FindByMaxGousyaHyoujijunNotDummy
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT MAX(YOSAN_GOUSYA_HYOUJI_JUN) AS YOSAN_GOUSYA_HYOUJI_JUN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND YOSAN_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND NOT YOSAN_GOUSYA = 'DUMMY' ")
            End With

            Dim db As New EBomDbClient
            Dim result As New TYosanSetteiGousyaVo
            result = db.QueryForObject(Of TYosanSetteiGousyaVo)(sb.ToString)
            Return result.YosanGousyaHyoujiJun
        End Function

#Region "手配帳作成初期表示"

        ''' <summary>
        ''' グループNoのリストを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>完成車情報リスト</returns>
        ''' <remarks></remarks>
        Public Function FindByGroupNoList(ByVal shisakuEventCode As String) _
                                As List(Of TShisakuEventKanseiVo) _
                                Implements YosanSetteiBuhinSakuseiDao.FindByGroupNoList
            'イベントコードを完成車情報からグループと工指Noを取得する'
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine("SELECT K.SHISAKU_GROUP")
                .AppendLine("   FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI K ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B ")
                .AppendLine(" ON B.SHISAKU_EVENT_CODE = K.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND B.HYOJIJUN_NO = K.HYOJIJUN_NO ")
                .AppendLine(" AND (NOT B.SHISAKU_SYUBETU = 'D' OR B.SHISAKU_SYUBETU = 'W' OR B.SHISAKU_SYUBETU IS NULL) ")
                .AppendLine("   WHERE ")
                .AppendFormat("     K.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendLine(" AND K.SHISAKU_GROUP <> '' ")
                .AppendLine("   GROUP BY K.SHISAKU_GROUP ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventKanseiVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' グループNoを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>完成車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByGroupNo(ByVal shisakuEventCode As String) _
                        As TShisakuEventKanseiVo _
                        Implements YosanSetteiBuhinSakuseiDao.FindByGroupNo
            'イベントコードを完成車情報からグループと工指Noを取得する'
            'ブランクデータは除外する。　2011/2/17　By柳沼
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine("SELECT K.SHISAKU_GROUP")
                .AppendLine("   FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI K ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B ")
                .AppendLine(" ON B.SHISAKU_EVENT_CODE = K.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND B.HYOJIJUN_NO = K.HYOJIJUN_NO ")
                .AppendLine(" AND (NOT B.SHISAKU_SYUBETU = 'D' OR B.SHISAKU_SYUBETU = 'W' OR B.SHISAKU_SYUBETU IS NULL) ")
                .AppendLine("   WHERE ")
                .AppendFormat("     K.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendLine(" AND K.SHISAKU_GROUP <> '' ")
                .AppendLine("   GROUP BY K.SHISAKU_GROUP ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventKanseiVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' イベント名称を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>イベント情報</returns>
        ''' <remarks></remarks>
        Public Function FindByEventName(ByVal shisakuEventCode As String) _
                        As TShisakuEventVo _
                        Implements YosanSetteiBuhinSakuseiDao.FindByEventName
            'イベントコードを元にイベント情報からイベント名称を、完成車情報から'
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine("SELECT ")
                .AppendLine("  SHISAKU_EVENT_NAME")
                .AppendLine(" ,SHISAKU_KAIHATSU_FUGO")
                .AppendLine("   FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT WITH (READUNCOMMITTED)")
                .AppendFormat("   WHERE SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' AsSKMSから製品区分リストを取得する
        ''' </summary>
        ''' <returns>製品区分リスト</returns>
        ''' <remarks></remarks>
        Public Function FindByAsSNKM() As List(Of AsSKMSVo) _
                                            Implements YosanSetteiBuhinSakuseiDao.FindByAsSNKM
            Dim sql As String = _
            "SELECT " _
            & " SNKM " _
            & " FROM " _
            & " " & MBOM_DB_NAME & ".dbo.AS_SKMS " _
            & " WITH (READUNCOMMITTED) " _
            & "WHERE " _
            & "BUKBN = 'B' "

            Dim db As New EBomDbClient
            Return db.QueryForList(Of AsSKMSVo)(sql)

        End Function

        ''' <summary>
        ''' 製品区分を取得する
        ''' </summary>
        ''' <returns>製品区分</returns>
        ''' <remarks></remarks>
        Public Function FindByAsSNKMOne() As AsSKMSVo _
                                            Implements YosanSetteiBuhinSakuseiDao.FindByAsSNKMOne
            Dim sql As String = _
            "SELECT " _
            & " SNKM " _
            & " FROM " _
            & " " & MBOM_DB_NAME & ".dbo.AS_SKMS " _
            & " WITH (READUNCOMMITTED) " _
            & "WHERE " _
            & "BUKBN = 'B' "

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of AsSKMSVo)(sql)

        End Function

#End Region

        ''' <summary>
        ''' リストコード表示順Noを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>リストコード情報</returns>
        ''' <remarks></remarks>
        Public Function FindByHyoujijunNo(ByVal shisakuEventCode As String) As TYosanSetteiListcodeVo Implements YosanSetteiBuhinSakuseiDao.FindByHyoujijunNo
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT MAX(YOSAN_LIST_HYOJIJUN_NO) AS YOSAN_LIST_HYOJIJUN_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_LISTCODE WITH (READUNCOMMITTED) ")
                .AppendFormat("WHERE SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
            End With

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TYosanSetteiListcodeVo)(sql.ToString)

        End Function

        ''' <summary>
        ''' 最新のリストコードを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuKoujiShireiNo">工事指令No</param>
        ''' <returns>最新のリストコード</returns>
        ''' <remarks></remarks>
        Public Function FindByListCode(ByVal shisakuEventCode As String, ByVal shisakuKoujiShireiNo As String) As TYosanSetteiListcodeVo Implements YosanSetteiBuhinSakuseiDao.FindByListCode
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT MAX(RIGHT(YOSAN_LIST_CODE, 2)) AS YOSAN_LIST_CODE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_LISTCODE ")
                .AppendLine(" WITH (READUNCOMMITTED) ")
                .AppendFormat("WHERE SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat("AND YOSAN_KOUJI_SHIREI_NO = '{0}' ", shisakuKoujiShireiNo)
            End With

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TYosanSetteiListcodeVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 試作部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuGroup">グループNo</param>
        ''' <returns>試作部品編集情報リスト</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhin(ByVal shisakuEventCode As String, _
                                         ByVal shisakuGroup As String, _
                                         ByVal JikyuFlg As Boolean) As List(Of YosanSetteiBuhinEditTmpVo) Implements YosanSetteiBuhinSakuseiDao.FindByBuhin
            Dim Jikyuhin As String
            '自給品の消しこみがありかなしかでSQL文を追加'
            If JikyuFlg = True Then
                Jikyuhin = " AND NOT ( BE.SHUKEI_CODE = 'J' AND BE.SIA_SHUKEI_CODE = 'J' OR BE.SHUKEI_CODE = 'J' AND  BE.SIA_SHUKEI_CODE  <> 'J' OR BE.SHUKEI_CODE = ' ' AND BE.SIA_SHUKEI_CODE = 'J') "
            Else
                Jikyuhin = ""
            End If

            Dim db As New EBomDbClient

            Dim BuhinList As New List(Of YosanSetteiBuhinEditTmpVo)
            Dim sb1 As New StringBuilder

            With sb1
                .Remove(0, .Length)
                .AppendLine(" SELECT B.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI K ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B ")
                .AppendLine(" ON B.SHISAKU_EVENT_CODE = K.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND B.HYOJIJUN_NO = K.HYOJIJUN_NO ")
                .AppendLine(" AND (NOT B.SHISAKU_SYUBETU = 'D' OR B.SHISAKU_SYUBETU = 'W' OR B.SHISAKU_SYUBETU IS NULL) ")
                .AppendLine(" WHERE ")
                .AppendFormat(" K.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                If StringUtil.IsNotEmpty(shisakuGroup) Then
                    .AppendFormat(" AND K.SHISAKU_GROUP = '{0}' ", shisakuGroup)
                End If

                .AppendLine(" ORDER BY B.HYOJIJUN_NO ")
            End With

            Dim BaseList As New List(Of TShisakuEventBaseVo)
            BaseList = db.QueryForList(Of TShisakuEventBaseVo)(sb1.ToString)

            '号車リスト'
            For Each BaseVo As TShisakuEventBaseVo In BaseList
                Dim TEST As New StringBuilder

                With TEST
                    .Remove(0, .Length)
                    .AppendLine(" SELECT SB.SHISAKU_BLOCK_NO, SB.SHISAKU_BLOCK_NO_KAITEI_NO, SB.KACHOU_SYOUNIN_JYOUTAI, SB.BLOCK_FUYOU ")
                    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                    .AppendLine(" WHERE  ")
                    .AppendFormat(" SB.SHISAKU_EVENT_CODE = '{0}' ", BaseVo.ShisakuEventCode)
                    .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO ")
                    .AppendLine(" = (  SELECT MAX(SHISAKU_BLOCK_NO_KAITEI_NO) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
                    .AppendLine(" WHERE SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                    .AppendLine(" AND SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ) ")
                    .AppendLine(" ORDER BY SB.SHISAKU_BLOCK_NO ")
                End With

                Dim sekkeiBlockList As New List(Of SekkeiBlockInstlVoHelper)
                sekkeiBlockList = db.QueryForList(Of SekkeiBlockInstlVoHelper)(TEST.ToString)

                Dim bukaCode As String = ""
                Dim blockNo As String = ""
                Dim flag As Boolean = True
                '設計ブロックINSTLリスト'

                For Each sekkeiblockVo As SekkeiBlockInstlVoHelper In sekkeiBlockList
                    'If Not StringUtil.Equals("642A", sekkeiblockVo.ShisakuBlockNo) Then
                    '    Continue For
                    'End If

                    If sekkeiblockVo.ShisakuBlockNoKaiteiNo <> "000" Then
                        '承認が完了していない'
                        If sekkeiblockVo.KachouSyouninJyoutai <> "36" Then
                            '前回の改定を取得する'
                            Dim zenkaiBlockNoKaiteiNo As String = Right("000" + (Integer.Parse(sekkeiblockVo.ShisakuBlockNoKaiteiNo) - 1).ToString, 3)
                            sekkeiblockVo.ShisakuBlockNoKaiteiNo = zenkaiBlockNoKaiteiNo
                            flag = False
                        End If
                    Else
                        '改訂000がブロック不要なら出さない'
                        If sekkeiblockVo.BlockFuyou = "1" Then
                            '同じブロックなら出さないようにする'
                            blockNo = sekkeiblockVo.ShisakuBlockNo
                            bukaCode = sekkeiblockVo.ShisakuBukaCode
                            flag = False
                            Continue For
                        Else
                            flag = True
                        End If
                    End If

                    '2012/02/23 ブロック要、不要は見ないで最新を取る'
                    Dim sb2 As New StringBuilder
                    With sb2
                        .Remove(0, .Length)
                        .AppendLine(" SELECT SBI.*, SB.KACHOU_SYOUNIN_JYOUTAI, SB.BLOCK_FUYOU ")
                        .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ")
                        .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                        .AppendLine(" ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE")
                        .AppendLine(" AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                        .AppendLine(" AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                        .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                        .AppendLine(" AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') ")
                        .AppendLine(" WHERE  ")
                        .AppendFormat(" SBI.SHISAKU_EVENT_CODE = '{0}' ", BaseVo.ShisakuEventCode)
                        .AppendFormat(" AND SBI.SHISAKU_GOUSYA = '{0}' ", BaseVo.ShisakuGousya)
                        .AppendFormat(" AND SBI.SHISAKU_BLOCK_NO = '{0}' ", sekkeiblockVo.ShisakuBlockNo)
                        .AppendFormat(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", sekkeiblockVo.ShisakuBlockNoKaiteiNo)
                        .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")
                        .AppendLine(" AND SBI.INSU_SURYO > 0 ")
                        .AppendLine(" ORDER BY SBI.SHISAKU_BLOCK_NO ")
                    End With

                    Dim sekkeiBlockInstlList As New List(Of SekkeiBlockInstlVoHelper)
                    sekkeiBlockInstlList = db.QueryForList(Of SekkeiBlockInstlVoHelper)(sb2.ToString)

                    For Each sekkeiBlockInstlVo As SekkeiBlockInstlVoHelper In sekkeiBlockInstlList
                        'ブロックの出力パターン'
                        '改訂000以外'
                        '課長承認が完了しているかつ、ブロック要なら最新を使用'
                        '課長承認が完了していないまたはブロック不要なら改訂-1を取得'
                        '改訂-1がブロック不要なら出力しない'
                        '改訂-1がブロック要なら出力する'

                        If Not flag Then
                            '同じ部課コードかチェック'
                            If bukaCode = sekkeiBlockInstlVo.ShisakuBukaCode Then
                                '同じブロックかチェック'
                                If blockNo = sekkeiBlockInstlVo.ShisakuBlockNo Then
                                    Continue For
                                End If
                            End If
                        End If

                        Dim sb3 As New StringBuilder
                        With sb3
                            .Remove(0, .Length)
                            .AppendLine(" SELECT BE.*, BEI.INSU_SURYO ")
                            .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                            .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                            .AppendLine(" ON BE.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                            .AppendLine(" AND BE.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                            .AppendLine(" AND BE.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                            .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                            .AppendLine(" AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN ")
                            .AppendLine(" " & Jikyuhin & " ")
                            .AppendLine(" WHERE ")
                            .AppendFormat(" BEI.SHISAKU_EVENT_CODE = '{0}' ", sekkeiBlockInstlVo.ShisakuEventCode)
                            .AppendFormat(" AND BEI.SHISAKU_BUKA_CODE = '{0}' ", sekkeiBlockInstlVo.ShisakuBukaCode)
                            .AppendFormat(" AND BEI.SHISAKU_BLOCK_NO = '{0}' ", sekkeiBlockInstlVo.ShisakuBlockNo)
                            .AppendFormat(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}'", sekkeiBlockInstlVo.ShisakuBlockNoKaiteiNo)
                            .AppendFormat(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = {0} ", sekkeiBlockInstlVo.InstlHinbanHyoujiJun)
                        End With

                        Dim buhinEditList As New List(Of YosanSetteiBuhinEditTmpVo)
                        buhinEditList = db.QueryForList(Of YosanSetteiBuhinEditTmpVo)(sb3.ToString)

                        For Each buhinEditVo As YosanSetteiBuhinEditTmpVo In buhinEditList

                            Dim buhinNo As String = Trim(buhinEditVo.BuhinNo)
                            buhinEditVo.BuhinNo = buhinNo

                            buhinEditVo.ShisakuGousya = BaseVo.ShisakuGousya
                            buhinEditVo.ShisakuGousyaHyoujiJun = BaseVo.HyojijunNo

                            'BuhinList.Add(buhinEditVo)
                        Next
                        BuhinList.AddRange(buhinEditList)
                    Next
                Next

            Next

            Return BuhinList
        End Function

        ''' <summary>
        ''' ベース車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuGroup">試作グループNo</param>
        ''' <returns>該当する情報のリスト</returns>
        ''' <remarks></remarks>
        Public Function FindByBaseList(ByVal shisakuEventCode As String, _
                                       ByVal shisakuGroup As String) As List(Of TShisakuEventBaseVo) Implements YosanSetteiBuhinSakuseiDao.FindByBaseList
            Dim sb1 As New StringBuilder
            With sb1
                .Remove(0, .Length)
                .AppendLine(" SELECT B.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI K ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B ")
                .AppendLine(" ON B.SHISAKU_EVENT_CODE = K.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND B.HYOJIJUN_NO = K.HYOJIJUN_NO ")
                .AppendLine(" AND (NOT B.SHISAKU_SYUBETU = 'D' OR B.SHISAKU_SYUBETU = 'W' OR B.SHISAKU_SYUBETU IS NULL) ")
                .AppendLine(" WHERE ")
                .AppendFormat(" K.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND K.SHISAKU_GROUP = '{0}' ", shisakuGroup)
                .AppendLine(" ORDER BY B.HYOJIJUN_NO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventBaseVo)(sb1.ToString)
        End Function

        ''' <summary>
        ''' 設計ブロックINSTL+課長情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <returns>該当する情報のリスト</returns>
        ''' <remarks></remarks>
        Public Function FindBySekkeiList(ByVal shisakuEventCode As String, _
                                         ByVal shisakuGousya As String) As List(Of SekkeiBlockInstlVoHelper) Implements YosanSetteiBuhinSakuseiDao.FindBySekkeiList
            Dim sb2 As New StringBuilder
            With sb2
                .Remove(0, .Length)
                .AppendLine(" SELECT SBI.*, SB.KACHOU_SYOUNIN_JYOUTAI ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') ")
                .AppendLine(" WHERE  ")
                .AppendFormat(" SBI.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND SBI.SHISAKU_GOUSYA = '{0}'", shisakuGousya)
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" = (  SELECT MAX ( CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")
                .AppendLine(" AND SBI.INSU_SURYO > 0 ")
                .AppendLine(" ORDER BY SBI.SHISAKU_BLOCK_NO ")
            End With
            Dim db As New EBomDbClient

            Return db.QueryForList(Of SekkeiBlockInstlVoHelper)(sb2.ToString)
        End Function

        ''' <summary>
        ''' 設計ブロックINSTL+課長情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns>該当する情報のリスト</returns>
        ''' <remarks></remarks>
        Public Function FindBySekkeiList(ByVal shisakuEventCode As String, _
                                         ByVal shisakuGousya As String, _
                                         ByVal shisakuBukaCode As String, _
                                         ByVal shisakuBlockNo As String, _
                                         ByVal shisakuBlockNoKaiteiNo As String) As List(Of SekkeiBlockInstlVoHelper) Implements YosanSetteiBuhinSakuseiDao.FindBySekkeiList
            Dim sql As New StringBuilder
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT SBI.*, SB.KACHOU_SYOUNIN_JYOUTAI ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') ")
                .AppendLine(" WHERE  ")
                .AppendFormat(" SBI.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SBI.SHISAKU_GOUSYA = '{0}' ", shisakuGousya)
                .AppendFormat(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendFormat(" AND SBI.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND SBI.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")
                .AppendLine(" AND SBI.INSU_SURYO > 0 ")
                .AppendLine(" ORDER BY SBI.SHISAKU_BLOCK_NO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of SekkeiBlockInstlVoHelper)(sql.ToString)
        End Function

        ''' <summary>
        ''' 設計ブロック情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>該当する情報のリスト</returns>
        ''' <remarks></remarks>
        Public Function FindBySekkeiBlockList(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockVo) Implements YosanSetteiBuhinSakuseiDao.FindBySekkeiBlockList
            Dim sql As New StringBuilder
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT DISTINCT SB.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine(" WHERE  ")
                .AppendFormat(" SB.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendLine(" AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') ")
                .AppendLine(" ORDER BY SB.SHISAKU_BLOCK_NO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockVo)(sql.ToString)
            '↑↑2014/10/29 酒井 ADD END
        End Function

        ''' <summary>
        ''' 部品編集+員数を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="instlHinbanHyoujiJun">INSTL品番表示順</param>
        ''' <returns>該当する情報のリスト</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditInsu(ByVal shisakuEventCode As String, _
                                            ByVal shisakuGousya As String, _
                                            ByVal shisakuBukaCode As String, _
                                            ByVal shisakuBlockNo As String, _
                                            ByVal shisakuBlockNoKaiteiNo As String, _
                                            ByVal instlHinbanHyoujiJun As Integer, _
                                            ByVal JikyuFlg As Boolean) As List(Of YosanSetteiBuhinEditTmpVo) Implements YosanSetteiBuhinSakuseiDao.FindByBuhinEditInsu
            Dim sql As New StringBuilder
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT BE.*, BEI.INSU_SURYO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                .AppendLine(" ON BE.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BE.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN ")
                '自給品の消しこみがありかなしかでSQL文を追加'
                If JikyuFlg = True Then
                    .AppendLine(" AND NOT ( BE.SHUKEI_CODE = 'J' AND BE.SIA_SHUKEI_CODE = 'J' OR BE.SHUKEI_CODE = 'J' AND  BE.SIA_SHUKEI_CODE  <> 'J' OR BE.SHUKEI_CODE = ' ' AND BE.SIA_SHUKEI_CODE = 'J') ")
                End If
                .AppendLine(" WHERE ")
                .AppendFormat(" SBI.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SBI.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SBI.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendFormat(" AND SBI.INSTL_HINBAN_HYOUJI_JUN = {0} ", instlHinbanHyoujiJun)
                .AppendFormat(" AND SBI.SHISAKU_GOUSYA = '{0}' ", shisakuGousya)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of YosanSetteiBuhinEditTmpVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 部品編集+員数を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="instlHinbanHyoujiJun">INSTL品番表示順</param>
        ''' <returns>該当する情報のリスト</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditInsu(ByVal shisakuEventCode As String, _
                                                   ByVal shisakuBukaCode As String, _
                                                   ByVal shisakuBlockNo As String, _
                                                   ByVal shisakuBlockNoKaiteiNo As String, _
                                                   ByVal instlHinbanHyoujiJun As Integer, _
                                                   ByVal JikyuFlg As Boolean) As List(Of YosanSetteiBuhinEditTmpVo) Implements YosanSetteiBuhinSakuseiDao.FindByBuhinEditInsu
            Dim sql As New StringBuilder
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT BE.*, BEI.INSU_SURYO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                .AppendLine(" ON BE.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BE.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN ")
                '自給品の消しこみがありかなしかでSQL文を追加'
                If JikyuFlg = True Then
                    .AppendLine(" AND NOT ( BE.SHUKEI_CODE = 'J' AND BE.SIA_SHUKEI_CODE = 'J' OR BE.SHUKEI_CODE = 'J' AND  BE.SIA_SHUKEI_CODE  <> 'J' OR BE.SHUKEI_CODE = ' ' AND BE.SIA_SHUKEI_CODE = 'J') ")
                End If
                .AppendLine(" WHERE ")
                .AppendFormat(" BEI.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BEI.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND BEI.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendFormat(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = '{0}' ", instlHinbanHyoujiJun)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of YosanSetteiBuhinEditTmpVo)(sql.ToString)
        End Function



        ''' <summary>
        ''' 試作部品号車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuGroup">グループNo</param>
        ''' <returns>試作部品号車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinGousya(ByVal shisakuEventCode As String, _
                                         ByVal shisakuGroup As String, ByVal JikyuFlg As Boolean) As List(Of TYosanSetteiGousyaTmpVo) Implements YosanSetteiBuhinSakuseiDao.FindByBuhinTmpGosya
            Dim Jikyuhin As String
            '自給品の消しこみがありかなしかでSQL文を追加'
            If JikyuFlg = True Then
                Jikyuhin = " AND NOT ( BE.SHUKEI_CODE = 'J' AND BE.SIA_SHUKEI_CODE = 'J' OR BE.SHUKEI_CODE = 'J' AND  BE.SIA_SHUKEI_CODE  <> 'J' OR BE.SHUKEI_CODE = ' ' AND BE.SIA_SHUKEI_CODE = 'J') "
            Else
                Jikyuhin = ""
            End If

            '小分けにする'
            Dim GousyaTmpList As New List(Of TYosanSetteiGousyaTmpVo)

            Dim sql1 As New System.Text.StringBuilder
            With sql1
                .AppendLine(" SELECT B.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI K ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B ")
                .AppendLine(" ON B.SHISAKU_EVENT_CODE = K.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND B.HYOJIJUN_NO = K.HYOJIJUN_NO ")
                .AppendLine(" WHERE ")
                .AppendFormat(" K.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND K.SHISAKU_GROUP = '{0}' ", shisakuGroup)
                .AppendLine(" ORDER BY B.HYOJIJUN_NO ")
            End With

            Dim db As New EBomDbClient

            '小分けにする'

            Dim sql2 As New System.Text.StringBuilder
            Dim sql3 As New System.Text.StringBuilder
            '号車リスト'
            For Each BaseVo As TShisakuEventBaseVo In db.QueryForList(Of TShisakuEventBaseVo)(sql1.ToString)
                With sql2
                    .Remove(0, .Length)
                    .AppendLine(" SELECT SBI.* ")
                    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ")
                    .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                    .AppendLine(" ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE")
                    .AppendLine(" AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                    .AppendLine(" AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                    .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                    .AppendLine(" AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') ")
                    .AppendLine(" WHERE  ")
                    .AppendFormat(" SBI.SHISAKU_EVENT_CODE = '{0}' ", BaseVo.ShisakuEventCode)
                    .AppendFormat(" AND SBI.SHISAKU_GOUSYA = '{0}' ", BaseVo.ShisakuGousya)
                    .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                    .AppendLine(" = (SELECT MAX(SHISAKU_BLOCK_NO_KAITEI_NO) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                    .AppendLine(" WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                    .AppendLine(" AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) ")
                    .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")
                    .AppendLine(" AND SBI.INSU_SURYO > 0 ")
                    .AppendLine(" ORDER BY SBI.SHISAKU_BLOCK_NO ")
                End With
                For Each sekkeiBlockInstlVo As SekkeiBlockInstlVoHelper In db.QueryForList(Of SekkeiBlockInstlVoHelper)(sql2.ToString)
                    '改定Noが000では無い'
                    If sekkeiBlockInstlVo.ShisakuBlockNoKaiteiNo <> "000" Then
                        '課長承認が完了していない'
                        If sekkeiBlockInstlVo.KachouSyouninJyoutai <> "36" Then
                            '前回の改定Noを使用する'
                            Dim zenkaiBlockNoKaiteiNo As String = Right("000" + (Integer.Parse(sekkeiBlockInstlVo.ShisakuBlockNoKaiteiNo) - 1).ToString, 3)
                            sekkeiBlockInstlVo.ShisakuBlockNoKaiteiNo = zenkaiBlockNoKaiteiNo
                        End If
                    End If
                    With sql3
                        .Remove(0, .Length)
                        .AppendLine(" SELECT BEI.* ")
                        .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                        .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                        .AppendLine(" ON BE.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                        .AppendLine(" AND BE.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                        .AppendLine(" AND BE.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                        .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                        .AppendLine(" AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN ")
                        .AppendLine(Jikyuhin)
                        .AppendLine(" WHERE ")
                        .AppendFormat(" BEI.SHISAKU_EVENT_CODE = '{0}' ", sekkeiBlockInstlVo.ShisakuEventCode)
                        .AppendFormat(" AND BEI.SHISAKU_BUKA_CODE = '{0}' ", sekkeiBlockInstlVo.ShisakuBukaCode)
                        .AppendFormat(" AND BEI.SHISAKU_BLOCK_NO = '{0}' ", sekkeiBlockInstlVo.ShisakuBlockNo)
                        .AppendFormat(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", sekkeiBlockInstlVo.ShisakuBlockNoKaiteiNo)
                        .AppendFormat(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = {0} ", sekkeiBlockInstlVo.InstlHinbanHyoujiJun)
                    End With

                    For Each buhinEditInstlVo As TShisakuBuhinEditInstlVo In db.QueryForList(Of TShisakuBuhinEditInstlVo)(sql3.ToString)

                        Dim gousyaVo As New TYosanSetteiGousyaTmpVo
                        gousyaVo.ShisakuEventCode = buhinEditInstlVo.ShisakuEventCode
                        gousyaVo.ShisakuBukaCode = buhinEditInstlVo.ShisakuBukaCode
                        gousyaVo.ShisakuBlockNo = buhinEditInstlVo.ShisakuBlockNo
                        gousyaVo.ShisakuBlockNoKaiteiNo = buhinEditInstlVo.ShisakuBlockNoKaiteiNo
                        gousyaVo.BuhinNoHyoujiJun = buhinEditInstlVo.BuhinNoHyoujiJun
                        gousyaVo.ShisakuGousyaHyoujiJun = BaseVo.HyojijunNo
                        gousyaVo.ShisakuGousya = BaseVo.ShisakuGousya
                        gousyaVo.InsuSuryo = buhinEditInstlVo.InsuSuryo

                        GousyaTmpList.Add(gousyaVo)
                    Next
                Next
            Next

            Return GousyaTmpList
        End Function

        ''' <summary>
        ''' 種別を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuGroup">グループNo</param>
        ''' <returns>親品番</returns>
        ''' <remarks></remarks>
        Public Function FindBySyubetu(ByVal shisakuEventCode As String, _
                                         ByVal shisakuGroup As String) As List(Of TShisakuEventBaseVo) Implements YosanSetteiBuhinSakuseiDao.FindBySyubetu
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT SHISAKU_SYUBETU ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI K ")
                .AppendLine(" WITH (READUNCOMMITTED) ")
                .AppendLine(" LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B ")
                .AppendLine(" ON K.SHISAKU_EVENT_CODE = B.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND K.HYOJIJUN_NO = B.HYOJIJUN_NO ")
                .AppendFormat(" WHERE K.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                If StringUtil.IsNotEmpty(shisakuGroup) Then
                    .AppendFormat(" AND K.SHISAKU_GROUP = '{0}' ", shisakuGroup)
                End If
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventBaseVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 親品番を取得する
        ''' </summary>
        ''' <returns>親品番</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinNoOya(ByVal kaihatsuFugo As String, ByVal BuhinNo As String) As String Implements YosanSetteiBuhinSakuseiDao.FindByBuhinNoOya
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT R.BUHIN_NO_OYA ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 R ")
                .AppendLine(" WHERE ")
                .AppendFormat(" R.BUHIN_NO_KO = '{0}' ", BuhinNo)
                .AppendLine(" AND KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 ")
                .AppendLine(" WHERE BUHIN_NO_OYA = R.BUHIN_NO_OYA ")
                .AppendLine(" AND BUHIN_NO_KO = R.BUHIN_NO_KO ) ")
            End With
            Dim db As New EBomDbClient

            Dim R552Vo As Rhac0552Vo = db.QueryForObject(Of Rhac0552Vo)(sql.ToString)
            If R552Vo Is Nothing Then
                With sql
                    .Remove(0, .Length)
                    .AppendLine(" SELECT R.BUHIN_NO_OYA ")
                    .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 R ")
                    .AppendLine(" WHERE ")
                    .AppendFormat(" R.BUHIN_NO_KO = '{0}' ", BuhinNo)
                    .AppendFormat(" AND R.KAIHATSU_FUGO = '{0}' ", kaihatsuFugo)
                    .AppendLine(" AND R.KAITEI_NO = ( ")
                    .AppendLine(" SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO ")
                    .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 ")
                    .AppendLine(" WHERE BUHIN_NO_OYA = R.BUHIN_NO_OYA ")
                    .AppendLine(" AND BUHIN_NO_KO = R.BUHIN_NO_KO  ")
                    .AppendLine(" AND KAIHATSU_FUGO = R.KAIHATSU_FUGO ) ")
                End With
                Dim R553Vo As Rhac0553Vo = db.QueryForObject(Of Rhac0553Vo)(sql.ToString)

                If R553Vo Is Nothing Then
                    With sql
                        .Remove(0, .Length)
                        .AppendLine(" SELECT R.BUHIN_NO_OYA ")
                        .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 R ")
                        .AppendLine(" WHERE ")
                        .AppendFormat(" R.BUHIN_NO_KO = '{0}' ", BuhinNo)
                        .AppendLine(" AND R.KAITEI_NO = ( ")
                        .AppendLine(" SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO ")
                        .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 ")
                        .AppendLine(" WHERE BUHIN_NO_OYA = R.BUHIN_NO_OYA ")
                        .AppendLine(" AND BUHIN_NO_KO = R.BUHIN_NO_KO ) ")
                    End With
                    Dim R551Vo As Rhac0551Vo = db.QueryForObject(Of Rhac0551Vo)(sql.ToString)
                    If R551Vo Is Nothing Then
                        Return Nothing
                    Else
                        Return R551Vo.BuhinNoOya
                    End If
                Else
                    Return R553Vo.BuhinNoOya
                End If
            Else
                Return R552Vo.BuhinNoOya
            End If
        End Function

        ''' <summary>
        ''' メーカーコードを取得する
        ''' </summary>
        ''' <returns>メーカーコード</returns>
        ''' <remarks></remarks>
        Public Function FindByMakerCode(ByVal kaihatsuFugo As String, ByVal BuhinNo As String) As String Implements YosanSetteiBuhinSakuseiDao.FindByMakerCode
            Dim BuhinNoOya As String

            BuhinNoOya = FindByBuhinNoOya(kaihatsuFugo, BuhinNo)

            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT MAKER_CODE ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 ")
                .AppendFormat(" WHERE BUHIN_NO = @BuhinNo ", BuhinNoOya)
            End With
            Dim db As New EBomDbClient

            Dim R0532vo As Rhac0532Vo = db.QueryForObject(Of Rhac0532Vo)(sql.ToString)

            If R0532vo Is Nothing Then
                Return Nothing
            Else
                Return R0532vo.MakerCode
            End If


        End Function

        ''' <summary>
        ''' 専用品かどうか存在チェックする（手配帳作成用）
        ''' </summary>
        Private Shared FindBuhinNo As String
        Private Shared Function FindAsKPSM10PVo(ByVal Vo As AsKPSM10PVo) As Boolean
            If Vo.Buba15.TrimEnd = FindBuhinNo Then
                Return True
            Else
                Return False
            End If
        End Function
        Private Shared Function FindAsPARTSPVo(ByVal Vo As AsPARTSPVo) As Boolean
            If Vo.Buba13.TrimEnd = FindBuhinNo Then
                Return True
            Else
                Return False
            End If
        End Function
        Private Shared Function FindAsGKPSM10PVo(ByVal Vo As AsGKPSM10PVo) As Boolean
            If Vo.Buba15.TrimEnd = FindBuhinNo Then
                Return True
            Else
                Return False
            End If
        End Function
        ''' <summary>
        ''' 専用品かどうか存在チェックする
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <returns>あればTrue</returns>
        ''' <remarks></remarks>
        Public Function FindBySenyouCheckSakusei(ByVal BuhinNo As String, ByVal seihinKbn As String, _
                                          ByVal aKPSM As List(Of AsKPSM10PVo), _
                                          ByVal aPARTS As List(Of AsPARTSPVo), _
                                          ByVal aGKPSM As List(Of AsGKPSM10PVo)) As Boolean Implements YosanSetteiBuhinSakuseiDao.FindBySenyouCheckSakusei

            Dim result As Boolean = False
            Dim NewBuhinNo As String = BuhinNo
            If Left(BuhinNo, 1) = "-" Then
                '-をスペースに置き換える'
                NewBuhinNo = " " + Right(BuhinNo, BuhinNo.Length - 1)
            End If

            'LISTにデータが有るか？
            FindBuhinNo = NewBuhinNo
            If aKPSM.FindIndex(AddressOf FindAsKPSM10PVo) > 0 Then
                result = True
                Return result
            End If

            '存在チェックその１'
            '無ければパーツプライリスト'
            Dim paramP As New AsPARTSPVo
            If BuhinNo.Length >= 13 Then
                If Left(BuhinNo, 1) = "-" Then
                    paramP.Buba13 = Left(" " + Right(BuhinNo, BuhinNo.Length - 1), 13)
                Else
                    paramP.Buba13 = Left(BuhinNo, 13)
                End If
            Else
                If Left(BuhinNo, 1) = "-" Then
                    paramP.Buba13 = " " + Right(BuhinNo, BuhinNo.Length - 1)
                Else
                    paramP.Buba13 = BuhinNo
                End If
            End If

            'LISTにデータが有るか？
            FindBuhinNo = paramP.Buba13
            If aPARTS.FindIndex(AddressOf FindAsPARTSPVo) > 0 Then
                result = True
                Return result
            End If

            '存在チェックその２'
            '無ければ海外生産マスタ'
            Dim paramG As New AsGKPSM10PVo
            If Left(BuhinNo, 1) = "-" Then
                paramG.Buba15 = " " + Right(BuhinNo, BuhinNo.Length - 1)
            Else
                paramG.Buba15 = Left(BuhinNo, 15)
            End If

            'LISTにデータが有るか？
            FindBuhinNo = paramG.Buba15
            If aGKPSM.FindIndex(AddressOf FindAsGKPSM10PVo) > 0 Then
                result = True
                Return result
            End If

        End Function

        ''' <summary>
        ''' 専用品かどうか存在チェックする
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <returns>あればTrue</returns>
        ''' <remarks></remarks>
        Public Function FindBySenyouCheck(ByVal BuhinNo As String, ByVal seihinKbn As String) As Boolean Implements YosanSetteiBuhinSakuseiDao.FindBySenyouCheck

            '製品区分は不要かも
            ' 2011/03/14 柳沼
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.AS_KPSM10P WITH (NOLOCK, NOWAIT)" _
            & " WHERE " _
            & "     BUBA_15 = @Buba15 "
            'Dim sql As String = _
            '" SELECT * " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.AS_KPSM10P " _
            '& " WHERE " _
            '& " SNKM = @Snkm " _
            '& " AND BUBA_15 = @Buba15 "

            Dim result As Boolean = False
            Dim NewBuhinNo As String = BuhinNo
            If Left(BuhinNo, 1) = "-" Then
                '-をスペースに置き換える'
                NewBuhinNo = " " + Right(BuhinNo, BuhinNo.Length - 1)
            End If

            Dim db As New EBomDbClient
            Dim paramK As New AsKPSM10PVo

            'パラメータは部品番号のみ設定する。
            paramK.Buba15 = NewBuhinNo  '部品番号
            'paramK.Snkm = seihinKbn    '製品区分

            Dim aKPSM As New List(Of AsKPSM10PVo)

            aKPSM = db.QueryForList(Of AsKPSM10PVo)(sql, paramK)
            '存在チェックその１'
            If aKPSM.Count = 0 Then
                '無ければパーツプライリスト'
                Dim sql2 As String = _
                " SELECT * " _
                & " FROM " & MBOM_DB_NAME & ".dbo.AS_PARTSP WITH (NOLOCK, NOWAIT) " _
                & " WHERE BUBA_13 = @Buba13 "

                Dim paramP As New AsPARTSPVo
                If BuhinNo.Length >= 13 Then
                    If Left(BuhinNo, 1) = "-" Then
                        paramP.Buba13 = Left(" " + Right(BuhinNo, BuhinNo.Length - 1), 13)
                    Else
                        paramP.Buba13 = Left(" " + Right(BuhinNo, BuhinNo.Length - 1), 13)
                    End If
                Else
                    If Left(BuhinNo, 1) = "-" Then
                        paramP.Buba13 = " " + Right(BuhinNo, BuhinNo.Length - 1)
                    Else
                        paramP.Buba13 = BuhinNo
                    End If
                End If

                Dim aPARTS As New List(Of AsPARTSPVo)
                aPARTS = db.QueryForList(Of AsPARTSPVo)(sql2, paramP)

                '存在チェックその２'
                If aPARTS.Count = 0 Then
                    '無ければ海外生産マスタ'


                    Dim paramG As New AsGKPSM10PVo
                    If Left(BuhinNo, 1) = "-" Then
                        paramG.Buba15 = " " + Right(BuhinNo, BuhinNo.Length - 1)
                    Else
                        paramG.Buba15 = Left(BuhinNo, 15)
                    End If

                    Dim sql3 As String = _
                    " SELECT * " _
                    & " FROM " & MBOM_DB_NAME & ".dbo.AS_GKPSM10P WITH (NOLOCK, NOWAIT) " _
                    & " WHERE BUBA_15 = @Buba15 "
                    '↓↓2014/10/29 酒井 ADD BEGIN
                    'Ver6_2 1.95以降の修正内容の展開
                    '& " WHERE BUBA_15 = '" & paramG.Buba15 & "'"
                    '↑↑2014/10/29 酒井 ADD END

                    Dim aGKPSM As New List(Of AsGKPSM10PVo)
                    '↓↓2014/10/29 酒井 ADD BEGIN
                    'Ver6_2 1.95以降の修正内容の展開
                    'aGKPSM = db.QueryForList(Of AsGKPSM10PVo)(sql3)
                    aGKPSM = db.QueryForList(Of AsGKPSM10PVo)(sql3, paramG)
                    '↑↑2014/10/29 酒井 ADD END
                    '以下のチェックは不要かも
                    ' 2011/03/14 柳沼
                    ''存在チェックその３ '
                    'If aGKPSM.Count = 0 Then
                    '    '無ければ'
                    '    NewBuhinNo = ""
                    '    '無ければ部品マスタ'
                    '    If BuhinNo.Length < 10 Then
                    '        If StringUtil.Equals(Left(BuhinNo, 1), "-") Then
                    '            NewBuhinNo = Replace(BuhinNo, "-", " ")
                    '        Else
                    '            NewBuhinNo = BuhinNo
                    '        End If
                    '    Else
                    '        If StringUtil.Equals(Left(BuhinNo, 1), "-") Then
                    '            NewBuhinNo = Left(Replace(BuhinNo, "-", " "), 10)
                    '        Else
                    '            NewBuhinNo = Left(BuhinNo, 10)
                    '        End If
                    '    End If
                    '    Dim sql4 As String = _
                    '    " SELECT KOTAN, " _
                    '    & " MAKER " _
                    '    & " FROM " & MBOM_DB_NAME & ".dbo.AS_BUHIN01 " _
                    '    & " WHERE " _
                    '    & " GZZCP = @Gzzcp " _
                    '    & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                    '    Dim param4 As New AsBUHIN01Vo
                    '    param4.Gzzcp = NewBuhinNo

                    '    Dim aBuhin01 As New List(Of AsBUHIN01Vo)
                    '    aBuhin01 = db.QueryForList(Of AsBUHIN01Vo)(sql4, param4)
                    '    '存在チェックその４(部品マスタ)'
                    '    If aBuhin01.Count = 0 Then
                    '        '無ければ属性管理'
                    '        Dim sql5 As String = _
                    '        "SELECT " _
                    '        & " FHI_NOMINATE_KOTAN, " _
                    '        & " TORIHIKISAKI_CODE " _
                    '        & " FROM " _
                    '        & " " & RHACLIBF_DB_NAME & ".dbo.T_VALUE_DEV " _
                    '        & " WHERE " _
                    '        & "  AN_LEVEL = '1' " _
                    '        & " AND BUHIN_NO = @BuhinNo " _
                    '        & " ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC "

                    '        Dim aDev As New List(Of TValueDevVo)
                    '        Dim paramT As New TValueDevVo
                    '        'paramT.KaihatsuFugo = KaihatsuFugo
                    '        If BuhinNo.Length < 10 Then
                    '            paramT.BuhinNo = BuhinNo
                    '        Else
                    '            paramT.BuhinNo = Left(BuhinNo, 10)
                    '        End If

                    '        aDev = db.QueryForList(Of TValueDevVo)(sql5, paramT)

                    '        '存在チェックその５(属性管理(開発符号毎)) '
                    '        If aDev.Count = 0 Then
                    '            Return result
                    '        Else
                    '            result = True
                    '            Return result
                    '        End If
                    '    Else
                    '        result = True
                    '        Return result
                    '    End If
                    'Else
                    '    result = True
                    '    Return result
                    'End If
                Else
                    result = True
                    Return result
                End If
            Else
                result = True
                Return result
            End If
        End Function

        ''' <summary>
        ''' 親の部品番号を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="level">レベル</param>
        ''' <returns>親の部品番号候補</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditBuhinNoOya(ByVal shisakuEventCode As String, _
                                                  ByVal shisakuBukaCode As String, _
                                                  ByVal shisakuBlockNo As String, _
                                                  ByVal shisakuBlockNoKaiteiNo As String, _
                                                  ByVal level As Integer, _
                                                  ByVal buhinNoKo As String, _
                                                  ByVal kaihatsuFugo As String) As TShisakuBuhinEditVo Implements YosanSetteiBuhinSakuseiDao.FindByBuhinEditBuhinNoOya

            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT BE.BUHIN_NO, BE.BUHIN_NO_KBN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BE.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendFormat(" AND BE.LEVEL = {0} ", (level - 1))
            End With
            Dim db As New EBomDbClient

            Dim BuhinEditListVos As List(Of TShisakuBuhinEditVo) = db.QueryForList(Of TShisakuBuhinEditVo)(sql.ToString)
            Dim ResultVo As New TShisakuBuhinEditVo

            'INSTL品番表示順を取得する'
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT MAX (BEI.INSTL_HINBAN_HYOUJI_JUN) AS INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" WHERE  ")
                .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BE.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendFormat(" AND BE.BUHIN_NO = '{0}' ", buhinNoKo)
            End With
            Dim BuhinEditInstlVo As TShisakuBuhinEditInstlVo = db.QueryForObject(Of TShisakuBuhinEditInstlVo)(sql.ToString)
            For Each BuhinEditListVo As TShisakuBuhinEditVo In BuhinEditListVos

                If Not StringUtil.IsEmpty(ResultVo.BuhinNo) Then
                    Continue For
                End If

                With sql
                    .Remove(0, .Length)
                    .AppendLine(" SELECT * ")
                    .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 R ")
                    .AppendLine(" WHERE ")
                    .AppendFormat(" R.BUHIN_NO_OYA = '{0}' ", BuhinEditListVo.BuhinNo)
                    .AppendFormat(" AND R.BUHIN_NO_KO = '{0}' ", buhinNoKo)
                    .AppendLine(" AND R.KAITEI_NO = ( ")
                    .AppendLine(" SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO ")
                    .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 ")
                    .AppendLine(" WHERE BUHIN_NO_OYA = R.BUHIN_NO_OYA ")
                    .AppendLine(" AND BUHIN_NO_KO = R.BUHIN_NO_KO ) ")
                End With
                Dim result0552 As Rhac0552Vo = db.QueryForObject(Of Rhac0552Vo)(sql.ToString)

                If result0552 Is Nothing Then
                    With sql
                        .Remove(0, .Length)
                        .AppendLine(" SELECT * ")
                        .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 R ")
                        .AppendLine(" WHERE ")
                        .AppendFormat(" BUHIN_NO_OYA = '{0}' ", BuhinEditListVo.BuhinNo)
                        .AppendFormat(" AND R.BUHIN_NO_KO = '{0}' ", buhinNoKo)
                        .AppendFormat(" AND R.KAIHATSU_FUGO = '{0}' ", kaihatsuFugo)
                        .AppendLine(" AND R.KAITEI_NO = ( ")
                        .AppendLine(" SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO ")
                        .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 ")
                        .AppendLine(" WHERE BUHIN_NO_OYA = R.BUHIN_NO_OYA ")
                        .AppendLine(" AND BUHIN_NO_KO = R.BUHIN_NO_KO  ")
                        .AppendLine(" AND KAIHATSU_FUGO = R.KAIHATSU_FUGO ) ")
                    End With

                    Dim result0553Vo As Rhac0553Vo = db.QueryForObject(Of Rhac0553Vo)(sql.ToString)

                    If result0553Vo Is Nothing Then
                        With sql
                            .Remove(0, .Length)
                            .AppendLine(" SELECT * ")
                            .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 R ")
                            .AppendLine(" WHERE ")
                            .AppendFormat(" BUHIN_NO_OYA = '{0}' ", BuhinEditListVo.BuhinNo)
                            .AppendFormat(" AND R.BUHIN_NO_KO = '{0}' ", buhinNoKo)
                            .AppendLine(" AND R.KAITEI_NO = ( ")
                            .AppendLine(" SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO ")
                            .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 ")
                            .AppendLine(" WHERE BUHIN_NO_OYA = R.BUHIN_NO_OYA ")
                            .AppendLine(" AND BUHIN_NO_KO = R.BUHIN_NO_KO ) ")
                        End With
                        Dim result0551Vo As New Rhac0551Vo
                        result0551Vo = db.QueryForObject(Of Rhac0551Vo)(sql.ToString)

                        If result0551Vo Is Nothing Then

                        Else
                            '該当する部品番号のINSTL品番表示順が子の部品番号のINSTL品番に該当するかチェックする'
                            With sql
                                .Remove(0, .Length)
                                .AppendLine(" SELECT MAX (BEI.INSTL_HINBAN_HYOUJI_JUN) AS INSTL_HINBAN_HYOUJI_JUN ")
                                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                                .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                                .AppendLine(" WHERE  ")
                                .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                                .AppendFormat(" AND BE.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                                .AppendFormat(" AND BE.BUHIN_NO = '{0}' ", result0551Vo.BuhinNoOya)
                            End With
                            Dim Ivo As TShisakuBuhinEditInstlVo = db.QueryForObject(Of TShisakuBuhinEditInstlVo)(sql.ToString)
                            If Ivo.InstlHinbanHyoujiJun = BuhinEditInstlVo.InstlHinbanHyoujiJun Then
                                ResultVo.BuhinNo = result0551Vo.BuhinNoOya
                                ResultVo.MakerCode = BuhinEditListVo.MakerCode
                                Return ResultVo
                            End If
                        End If

                    Else
                        '該当する部品番号のINSTL品番表示順が子の部品番号のINSTL品番に該当するかチェックする'
                        With sql
                            .Remove(0, .Length)
                            .AppendLine(" SELECT MAX (BEI.INSTL_HINBAN_HYOUJI_JUN) AS INSTL_HINBAN_HYOUJI_JUN ")
                            .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                            .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                            .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                            .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                            .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                            .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                            .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                            .AppendLine(" WHERE  ")
                            .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                            .AppendFormat(" AND BE.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                            .AppendFormat(" AND BE.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                            .AppendFormat(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                            .AppendFormat(" AND BE.BUHIN_NO = '{0}' ", result0553Vo.BuhinNoOya)
                        End With
                        Dim Ivo As TShisakuBuhinEditInstlVo = db.QueryForObject(Of TShisakuBuhinEditInstlVo)(sql.ToString)
                        If Ivo.InstlHinbanHyoujiJun = BuhinEditInstlVo.InstlHinbanHyoujiJun Then
                            ResultVo.BuhinNo = result0553Vo.BuhinNoOya
                            ResultVo.MakerCode = BuhinEditListVo.MakerCode
                        End If
                    End If

                Else
                    '該当する部品番号のINSTL品番表示順が子の部品番号のINSTL品番に該当するかチェックする'
                    With sql
                        .Remove(0, .Length)
                        .AppendLine(" SELECT MAX (BEI.INSTL_HINBAN_HYOUJI_JUN) AS INSTL_HINBAN_HYOUJI_JUN ")
                        .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                        .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                        .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                        .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                        .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                        .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                        .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                        .AppendLine(" WHERE  ")
                        .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                        .AppendFormat(" AND BE.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                        .AppendFormat(" AND BE.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                        .AppendFormat(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                        .AppendFormat(" AND BE.BUHIN_NO = '{0}' ", result0552.BuhinNoOya)
                    End With
                    Dim Ivo As TShisakuBuhinEditInstlVo = db.QueryForObject(Of TShisakuBuhinEditInstlVo)(sql.ToString)
                    If Ivo.InstlHinbanHyoujiJun = BuhinEditInstlVo.InstlHinbanHyoujiJun Then
                        ResultVo.BuhinNo = result0552.BuhinNoOya
                        ResultVo.MakerCode = BuhinEditListVo.MakerCode
                    End If
                End If
            Next

            If StringUtil.IsEmpty(ResultVo.BuhinNo) Then
                Return Nothing
            End If

            Return ResultVo

        End Function

        ''' <summary>
        ''' 購担/取引先を取得する（手配帳作成用）
        ''' </summary>
        Private Shared FindMakerCode As String
        Private Shared Function FindTValueDevVo(ByVal Vo As TValueDevVo) As Boolean
            If Vo.BuhinNo.TrimEnd = FindBuhinNo Then
                Return True
            Else
                Return False
            End If
        End Function
        Private Shared Function FindRhac0610Vo(ByVal Vo As Rhac0610Vo) As Boolean
            If Vo.MakerCode.TrimEnd = FindMakerCode Then
                Return True
            Else
                Return False
            End If
        End Function
        ''' <summary>
        ''' 購担/取引先を取得する（手配帳作成用）
        ''' </summary>
        ''' <param name="buhinNo">部品No</param>
        ''' <returns>購担と取引先</returns>
        ''' <remarks></remarks>
        Public Function FindByKoutanTorihikisakiSakusei(ByVal buhinNo As String, _
                                                        ByVal aAsKpsm10p As List(Of AsKPSM10PVo), _
                                                        ByVal aAsPartsp As List(Of AsPARTSPVo), _
                                                        ByVal aAsGkpsm10p As List(Of AsGKPSM10PVo), _
                                                        ByVal aAsBuhin01 As List(Of AsBUHIN01Vo), _
                                                        ByVal aTValueDev As List(Of TValueDevVo), _
                                                        ByVal aRhac0610 As List(Of Rhac0610Vo)) As TYosanSetteiBuhinTmpVo _
                                                        Implements YosanSetteiBuhinSakuseiDao.FindByKoutanTorihikisakiSakusei

            Dim db As New EBomDbClient
            Dim ETVO As New TYosanSetteiBuhinTmpVo

            '存在チェックその１(３ヶ月インフォメーション)'
            Dim NewBuhinNo As String = ""
            If Left(buhinNo, 1) = "-" Then
                'NewBuhinNo = Replace(buhinNo, "-", " ")
                NewBuhinNo = " " + Right(buhinNo, buhinNo.Length - 1)
            Else
                NewBuhinNo = buhinNo
            End If

            'LISTにデータが有るか？
            Dim intIndex As Integer = 0
            FindBuhinNo = NewBuhinNo
            intIndex = aAsKpsm10p.FindIndex(AddressOf FindAsKPSM10PVo)
            If intIndex > 0 Then
                ETVO.Koutan = aAsKpsm10p.Item(intIndex).Ka
                ETVO.MakerCode = aAsKpsm10p.Item(intIndex).Trcd
                GoTo getMakerName
            End If

            '存在チェックその２(パーツプライリスト)'
            intIndex = 0
            NewBuhinNo = ""
            '無ければパーツプライリスト'
            If buhinNo.Length < 13 Then
                If Left(buhinNo, 1) = "-" Then
                    NewBuhinNo = " " + Right(buhinNo, buhinNo.Length - 1)
                Else
                    NewBuhinNo = buhinNo
                End If
            Else
                If Left(buhinNo, 1) = "-" Then
                    NewBuhinNo = Left(" " + Right(buhinNo, buhinNo.Length - 1), 13)
                Else
                    NewBuhinNo = Left(buhinNo, 13)
                End If
            End If

            'LISTにデータが有るか？
            FindBuhinNo = NewBuhinNo
            intIndex = aAsPartsp.FindIndex(AddressOf FindAsPARTSPVo)
            If intIndex > 0 Then
                ETVO.Koutan = aAsPartsp.Item(intIndex).Ka
                ETVO.MakerCode = aAsPartsp.Item(intIndex).Trcd
                GoTo getMakerName
            End If

            '存在チェックその３(海外生産マスタ) '
            intIndex = 0
            NewBuhinNo = ""
            '無ければ海外生産マスタ'
            If Left(buhinNo, 1) = "-" Then
                NewBuhinNo = " " + Right(buhinNo, buhinNo.Length - 1)
            Else
                NewBuhinNo = buhinNo
            End If

            'LISTにデータが有るか？
            FindBuhinNo = NewBuhinNo
            intIndex = aAsGkpsm10p.FindIndex(AddressOf FindAsGKPSM10PVo)
            If intIndex > 0 Then
                ETVO.Koutan = aAsGkpsm10p.Item(intIndex).Ka
                ETVO.MakerCode = aAsGkpsm10p.Item(intIndex).Trcd
                GoTo getMakerName
            End If

            '存在チェックその４(部品マスタ)'
            intIndex = 0
            NewBuhinNo = ""
            '無ければ部品マスタ'
            If buhinNo.Length < 10 Then
                If Left(buhinNo, 1) = "-" Then
                    NewBuhinNo = " " + Right(buhinNo, buhinNo.Length - 1)
                Else
                    NewBuhinNo = buhinNo
                End If
            Else
                If Left(buhinNo, 1) = "-" Then
                    NewBuhinNo = " " + Right(buhinNo, buhinNo.Length - 1)
                Else
                    NewBuhinNo = Left(buhinNo, 10)
                End If
            End If

            'LISTにデータが有るか？
            FindBuhinNo = NewBuhinNo
            intIndex = aAsBuhin01.FindIndex(AddressOf FindAsBUHIN01Vo)
            If intIndex > 0 Then
                ETVO.Koutan = aAsBuhin01.Item(intIndex).Kotan
                ETVO.MakerCode = aAsBuhin01.Item(intIndex).Maker
                GoTo getMakerName
            End If

            '存在チェックその５(属性管理(開発符号毎)) '
            intIndex = 0
            '無ければ属性管理'
            Dim aDev As New TValueDevVo
            Dim paramT As New TValueDevVo
            If buhinNo.Length < 10 Then
                paramT.BuhinNo = buhinNo
            Else
                paramT.BuhinNo = Left(buhinNo, 10)
            End If

            'LISTにデータが有るか？
            FindBuhinNo = paramT.BuhinNo
            intIndex = aTValueDev.FindIndex(AddressOf FindTValueDevVo)
            If intIndex > 0 Then
                ETVO.Koutan = aTValueDev.Item(intIndex).FhiNominateKotan
                ETVO.MakerCode = aTValueDev.Item(intIndex).TorihikisakiCode
                GoTo getMakerName
            End If

            ETVO.Koutan = ""
            ETVO.MakerCode = ""

getMakerName:
            If Not StringUtil.IsEmpty(ETVO.MakerCode) Then

                'LISTにデータが有るか？
                ETVO.MakerName = ""
                FindMakerCode = ETVO.MakerCode
                intIndex = aRhac0610.FindIndex(AddressOf FindRhac0610Vo)
                If intIndex > 0 Then
                    ETVO.MakerName = aRhac0610.Item(intIndex).MakerName
                End If

            End If

            Return ETVO

            '抜けるまでの間に何も無ければNOTHING'
            'Return Nothing
        End Function



        Private _hshRHAC0610 As New Hashtable

        ''' <summary>
        ''' 購担/取引先を取得する
        ''' </summary>
        ''' <param name="buhinNo">部品No</param>
        ''' <returns>購担と取引先</returns>
        ''' <remarks></remarks>
        Public Function FindByKoutanTorihikisaki(ByVal buhinNo As String) As TYosanSetteiBuhinTmpVo Implements YosanSetteiBuhinSakuseiDao.FindByKoutanTorihikisaki
            Dim sql As New System.Text.StringBuilder
            Dim db As New EBomDbClient
            Dim ETVO As New TYosanSetteiBuhinTmpVo

            Dim NewBuhinNo As String = ""
            If Left(buhinNo, 1) = "-" Then
                NewBuhinNo = " " + Right(buhinNo, buhinNo.Length - 1)
            Else
                NewBuhinNo = buhinNo
            End If

            With sql
                .AppendLine(" SELECT KA,TRCD ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_KPSM10P WITH (NOLOCK, NOWAIT) ")
                .AppendFormat(" WHERE BUBA_15 = '{0}' ", NewBuhinNo)
                .AppendLine(" ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC ")
            End With
            Dim aKPSM As AsKPSM10PVo = db.QueryForObject(Of AsKPSM10PVo)(sql.ToString)
            '存在チェックその１(３ヶ月インフォメーション)'
            If aKPSM Is Nothing Then
                NewBuhinNo = ""
                '無ければパーツプライリスト'

                If buhinNo.Length < 13 Then
                    If Left(buhinNo, 1) = "-" Then
                        NewBuhinNo = " " + Right(buhinNo, buhinNo.Length - 1)
                    Else
                        NewBuhinNo = buhinNo
                    End If
                Else
                    If Left(buhinNo, 1) = "-" Then
                        NewBuhinNo = Left(" " + Right(buhinNo, buhinNo.Length - 1), 13)
                    Else
                        NewBuhinNo = Left(buhinNo, 13)
                    End If
                End If

                With sql
                    .Remove(0, .Length)
                    .AppendLine(" SELECT KA,TRCD ")
                    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_PARTSP WITH (NOLOCK, NOWAIT) ")
                    .AppendFormat(" WHERE BUBA_13 = '{0}' ", NewBuhinNo)
                    .AppendLine(" ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC ")
                End With
                Dim aPARTS As AsPARTSPVo = db.QueryForObject(Of AsPARTSPVo)(sql.ToString)

                '存在チェックその２(パーツプライリスト)'
                If aPARTS Is Nothing Then
                    NewBuhinNo = ""
                    '無ければ海外生産マスタ'
                    'Dim paramG As New AsGKPSM10PVo
                    If Left(buhinNo, 1) = "-" Then
                        NewBuhinNo = " " + Right(buhinNo, buhinNo.Length - 1)
                    Else
                        NewBuhinNo = buhinNo
                    End If

                    With sql
                        .Remove(0, .Length)
                        .AppendLine(" SELECT KA,TRCD ")
                        .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_GKPSM10P WITH (NOLOCK, NOWAIT) ")
                        .AppendFormat(" WHERE BUBA_15 = '{0}' ", NewBuhinNo)
                        .AppendLine(" ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC ")
                    End With
                    Dim aGKPSM As AsGKPSM10PVo = db.QueryForObject(Of AsGKPSM10PVo)(sql.ToString)

                    '存在チェックその３(海外生産マスタ) '
                    If aGKPSM Is Nothing Then
                        NewBuhinNo = ""
                        '無ければ部品マスタ'
                        If buhinNo.Length < 10 Then
                            If Left(buhinNo, 1) = "-" Then
                                NewBuhinNo = " " + Right(buhinNo, buhinNo.Length - 1)
                            Else
                                NewBuhinNo = buhinNo
                            End If
                        Else
                            If Left(buhinNo, 1) = "-" Then
                                NewBuhinNo = " " + Right(buhinNo, buhinNo.Length - 1)
                            Else
                                NewBuhinNo = Left(buhinNo, 10)
                            End If
                        End If

                        With sql
                            .Remove(0, .Length)
                            .AppendLine("SELECT KOTAN,MAKER ")
                            .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_BUHIN01 WITH (NOLOCK, NOWAIT) ")
                            .AppendFormat(" WHERE GZZCP = '{0}' ", NewBuhinNo)
                            .AppendLine(" ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC ")
                        End With
                        Dim aBuhin01 As AsBUHIN01Vo = db.QueryForObject(Of AsBUHIN01Vo)(sql.ToString)

                        '存在チェックその４(部品マスタ)'
                        If aBuhin01 Is Nothing Then
                            '無ければ属性管理'
                            Dim paramT As New TValueDevVo
                            'paramT.KaihatsuFugo = KaihatsuFugo
                            If buhinNo.Length < 10 Then
                                paramT.BuhinNo = buhinNo
                            Else
                                paramT.BuhinNo = Left(buhinNo, 10)
                            End If

                            With sql
                                .Remove(0, .Length)
                                .AppendLine("SELECT FHI_NOMINATE_KOTAN,TORIHIKISAKI_CODE ")
                                .AppendLine(" FROM " & EBOM_DB_NAME & ".dbo.T_VALUE_DEV WITH (NOLOCK, NOWAIT) ")
                                .AppendLine(" WHERE AN_LEVEL = '1' ")
                                .AppendFormat(" AND BUHIN_NO = '{0}' ", paramT.BuhinNo)
                                .AppendLine(" ORDER BY UPDATED_DATE DESC, UPDATED_TIME DESC ")
                            End With
                            Dim aDev As TValueDevVo = db.QueryForObject(Of TValueDevVo)(sql.ToString)

                            '存在チェックその５(属性管理(開発符号毎)) '
                            If aDev Is Nothing Then
                                ETVO.Koutan = ""
                                ETVO.MakerCode = ""
                            Else
                                ETVO.Koutan = aDev.FhiNominateKotan
                                ETVO.MakerCode = aDev.TorihikisakiCode
                            End If

                        Else
                            ETVO.Koutan = aBuhin01.Kotan
                            ETVO.MakerCode = aBuhin01.Maker
                        End If
                    Else
                        ETVO.Koutan = aGKPSM.Ka
                        ETVO.MakerCode = aGKPSM.Trcd
                    End If
                Else
                    ETVO.Koutan = aPARTS.Ka
                    ETVO.MakerCode = aPARTS.Trcd
                End If
            Else
                ETVO.Koutan = aKPSM.Ka
                ETVO.MakerCode = aKPSM.Trcd
            End If

            If StringUtil.IsNotEmpty(ETVO.MakerCode) Then
                If _hshRHAC0610.Contains(ETVO.MakerCode) Then
                    ETVO.MakerName = _hshRHAC0610.Item(ETVO.MakerCode)
                Else
                    With sql
                        .Remove(0, .Length)
                        .AppendLine(" SELECT MAKER_NAME ")
                        .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0610 WITH (NOLOCK, NOWAIT) ")
                        .AppendFormat(" WHERE MAKER_CODE = '{0}' ", ETVO.MakerCode)
                    End With
                    Dim MVo As Rhac0610Vo = db.QueryForObject(Of Rhac0610Vo)(sql.ToString)
                    If MVo Is Nothing Then
                        ETVO.MakerName = ""
                    Else
                        ETVO.MakerName = MVo.MakerName
                    End If
                    _hshRHAC0610.Add(ETVO.MakerCode, ETVO.MakerName)
                End If

            End If

            Return ETVO
        End Function

        ''' <summary>
        ''' 図面Noを取得する（手配帳作成用）
        ''' </summary>
        Private Shared Function FindAsBUHIN01Vo(ByVal Vo As AsBUHIN01Vo) As Boolean
            If Vo.Gzzcp.TrimEnd = FindBuhinNo Then
                Return True
            Else
                Return False
            End If
        End Function
        ''' <summary>
        ''' 図面Noを取得する（手配帳作成用）
        ''' </summary>
        ''' <param name="buhinNo">部品No</param>
        ''' <returns>購担と取引先</returns>
        ''' <remarks></remarks>
        Public Function FindByZumenNoSakusei(ByVal buhinNo As String, _
                                   ByVal aAsBuhin01 As List(Of AsBUHIN01Vo)) As String Implements YosanSetteiBuhinSakuseiDao.FindByZumenNoSakusei
            Dim db As New EBomDbClient
            Dim param As New AsBUHIN01Vo
            Dim BuhinTmpVo As New AsBUHIN01Vo
            Dim str As String = ""
            If buhinNo.Length < 10 Then
                If Left(buhinNo, 1) = "-" Then
                    param.Gzzcp = Replace(buhinNo, "-", " ")
                Else
                    param.Gzzcp = buhinNo
                End If
            Else
                If Left(buhinNo, 1) = "-" Then
                    param.Gzzcp = Replace(Left(buhinNo, 10), "-", " ")
                Else
                    param.Gzzcp = Left(buhinNo, 10)
                End If
            End If

            'LISTにデータが有るか？
            Dim intIndex As Integer = 0
            FindBuhinNo = param.Gzzcp
            intIndex = aAsBuhin01.FindIndex(AddressOf FindAsBUHIN01Vo)

            If intIndex > 0 Then
                str = aAsBuhin01.Item(intIndex).Stsr + " " + aAsBuhin01.Item(intIndex).Dhstba
            End If

            Return str
        End Function

        ''' <summary>
        ''' 図面Noを取得する
        ''' </summary>
        ''' <param name="buhinNo">部品No</param>
        ''' <returns>購担と取引先</returns>
        ''' <remarks></remarks>
        Public Function FindByZumenNo(ByVal buhinNo As String) As String Implements YosanSetteiBuhinSakuseiDao.FindByZumenNo
            Dim db As New EBomDbClient
            Dim param As New AsBUHIN01Vo
            Dim str As String
            If buhinNo.Length < 10 Then
                If Left(buhinNo, 1) = "-" Then
                    param.Gzzcp = Replace(buhinNo, "-", " ")
                Else
                    param.Gzzcp = buhinNo
                End If
            Else
                If Left(buhinNo, 1) = "-" Then
                    param.Gzzcp = Replace(Left(buhinNo, 10), "-", " ")
                Else
                    param.Gzzcp = Left(buhinNo, 10)
                End If
            End If

            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_BUHIN01 ")
                .AppendFormat(" WHERE GZZCP = '{0}' ", param.Gzzcp)
            End With
            Dim BuhinTmpVo As AsBUHIN01Vo = db.QueryForObject(Of AsBUHIN01Vo)(sql.ToString)
            If Not BuhinTmpVo Is Nothing Then
                str = BuhinTmpVo.Stsr + " " + BuhinTmpVo.Dhstba
            Else
                str = ""
            End If
            Return str
        End Function

        ''' <summary>
        ''' 部品編集TMP情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinTmp(ByVal shisakuEventCode As String, _
                                         ByVal shisakuListCode As String) As List(Of TYosanSetteiBuhinTmpVo) Implements YosanSetteiBuhinSakuseiDao.FindByBuhinTmp
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT DISTINCT BE.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP BE ")
                .AppendFormat(" WHERE BE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BE.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN")
            End With

            Dim db As New EBomDbClient
            Try
                Return db.QueryForList(Of TYosanSetteiBuhinTmpVo)(sql.ToString)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function


        ''' <summary>
        ''' 比較織り込み後の部品編集情報取得
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuListCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinHikakuTmp(ByVal shisakuEventCode As String, _
                                             ByVal shisakuListCode As String) As List(Of TYosanSetteiBuhinTmpVo) Implements YosanSetteiBuhinSakuseiDao.FindByBuhinHikakuTmp
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT DISTINCT BE.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP BE ")
                .AppendLine(" WHERE  ")
                .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BE.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND NOT BE.GYOU_ID = '888' ")
                .AppendLine(" AND NOT BE.GYOU_ID = '999' ")
                .AppendLine(" ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN")
            End With
            Dim db As New EBomDbClient
            Try
                Return db.QueryForList(Of TYosanSetteiBuhinTmpVo)(sql.ToString)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

        ''' <summary>
        ''' ユニット区分を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>ユニット区分</returns>
        ''' <remarks></remarks>
        Public Function FindByUnitKbn(ByVal shisakuEventCode As String) As TShisakuEventVo Implements YosanSetteiBuhinSakuseiDao.FindByUnitKbn
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT ")
                .AppendFormat(" WHERE SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 部品編集情報TMP情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>部品編集情報TMP情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinTmpList(ByVal shisakuEventCode As String, _
                                           ByVal shisakuListCode As String, _
                                           ByVal shisakuBlockNo As String) As List(Of TYosanSetteiBuhinTmpVo) Implements YosanSetteiBuhinSakuseiDao.FindByBuhinTmpList
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendLine(" ORDER BY SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, BUHIN_NO_HYOUJI_JUN")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TYosanSetteiBuhinTmpVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 部品編集号車情報TMP情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>部品編集号車情報TMP情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinGousyaTmpList(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String) As List(Of TYosanSetteiGousyaTmpVo) Implements YosanSetteiBuhinSakuseiDao.FindByBuhinGousyaTmpList
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA_TMP")
                .AppendFormat(" WHERE SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendLine(" ORDER BY SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, BUHIN_NO_HYOUJI_JUN")
            End With
            Dim db As New EBomDbClient
            Try
                Return db.QueryForList(Of TYosanSetteiGousyaTmpVo)(sql.ToString)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        ''' <summary>
        ''' 員数を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>合計員数数量</returns>
        ''' <remarks></remarks>
        Public Function FindByTotalInsuSuryo(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String, ByVal buhinNoHyoujiJun As Integer) As Integer Implements YosanSetteiBuhinSakuseiDao.FindByTotalInsuSuryo
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND YOSAN_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND YOSAN_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND YOSAN_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND BUHIN_NO_HYOUJI_JUN = {0} ", buhinNoHyoujiJun)
                .AppendLine(" ORDER BY YOSAN_BLOCK_NO, BUHIN_NO_HYOUJI_JUN")
            End With
            Dim db As New EBomDbClient

            Dim Volist As List(Of TYosanSetteiGousyaVo) = db.QueryForList(Of TYosanSetteiGousyaVo)(sql.ToString)

            Dim totalInsuSuryo As Integer = 0
            Dim MTotalInsuSuryo As Integer = 0

            For index As Integer = 0 To Volist.Count - 1
                If Volist(index).InsuSuryo < 0 Then
                    MTotalInsuSuryo = MTotalInsuSuryo + Volist(index).InsuSuryo
                Else
                    totalInsuSuryo = totalInsuSuryo + Volist(index).InsuSuryo
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
        ''' 親品番と試作区分と取引先コードを取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="level">レベル</param>
        ''' <param name="shukeiCode">集計コード</param>
        ''' <param name="siaShukeiCode">海外集計コード</param>
        ''' <returns>該当する親品番と試作区分と取引先コード</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinNoOyaKbn(ByVal kaihatsuFugo As String, _
                                            ByVal BuhinNo As String, _
                                            ByVal shisakuEventCode As String, _
                                            ByVal shisakuBukaCode As String, _
                                            ByVal shisakuBlockNo As String, _
                                            ByVal shisakuBlockNoKaiteiNo As String, _
                                            ByVal level As Integer, _
                                            ByVal shukeiCode As String, _
                                            ByVal siaShukeiCode As String) As TYosanSetteiBuhinTmpVo Implements YosanSetteiBuhinSakuseiDao.FindByBuhinNoOyaKbn
            Dim sql As New System.Text.StringBuilder
            Dim db As New EBomDbClient
            Dim ResultVo As New TYosanSetteiBuhinTmpVo

            '自身のレベル-1の部品番号を全て取得する'
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BE.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendFormat(" AND BE.LEVEL = {0}", level - 1)
            End With

            Dim BVoList As List(Of YosanSetteiBuhinEditTmpVo) = db.QueryForList(Of YosanSetteiBuhinEditTmpVo)(sql.ToString)



            '自身のINSTL品番表示順を取得'
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT BEI.INSTL_HINBAN_HYOUJI_JUN,BEI.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BE.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendFormat(" AND BE.BUHIN_NO = '{0}' ", BuhinNo)
                .AppendFormat(" AND BE.LEVEL = {0} ", level)
                .AppendFormat(" AND BE.SHUKEI_CODE = '{0}' ", shukeiCode)
                .AppendFormat(" AND BE.SIA_SHUKEI_CODE = '{0}'", siaShukeiCode)
            End With
            Dim InstlVo As List(Of TShisakuBuhinEditInstlVo) = db.QueryForList(Of TShisakuBuhinEditInstlVo)(sql.ToString)

            For index As Integer = 0 To BVoList.Count - 1

                For Each IVo As TShisakuBuhinEditInstlVo In InstlVo
                    '子のINSTL品番表示順と親のINSTL品番表示順が異なる場合は親になりえない'
                    If Not IVo.InstlHinbanHyoujiJun = BVoList(index).InstlHinbanHyoujiJun Then
                        Continue For
                    End If

                    '子の部品番号表示順より表示順Noが下はありえない'
                    If IVo.BuhinNoHyoujiJun < BVoList(index).BuhinNoHyoujiJun Then
                        Continue For
                    End If

                    With sql
                        .Remove(0, .Length)
                        .AppendLine(" SELECT R.BUHIN_NO_OYA ")
                        .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 R ")
                        .AppendLine(" WHERE ")
                        .AppendFormat(" R.BUHIN_NO_OYA = '{0}' ", BVoList(index).BuhinNo)
                        .AppendFormat(" AND R.BUHIN_NO_KO = '{0}' ", BuhinNo)
                        .AppendLine(" AND KAITEI_NO = ( ")
                        .AppendLine(" SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO ")
                        .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0552 ")
                        .AppendLine(" WHERE BUHIN_NO_OYA = R.BUHIN_NO_OYA ")
                        .AppendLine(" AND BUHIN_NO_KO = R.BUHIN_NO_KO ) ")

                    End With
                    Dim R552Vo As Rhac0552Vo = db.QueryForObject(Of Rhac0552Vo)(sql.ToString)

                    'R0552に存在しない場合'
                    If R552Vo Is Nothing Then
                        With sql
                            .Remove(0, .Length)
                            .AppendLine(" SELECT R.BUHIN_NO_OYA ")
                            .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 R ")
                            .AppendLine(" WHERE ")
                            .AppendFormat(" R.BUHIN_NO_OYA = '{0}' ", BVoList(index).BuhinNo)
                            .AppendFormat(" AND R.BUHIN_NO_KO = '{0}' ", BuhinNo)
                            .AppendFormat(" AND R.KAIHATSU_FUGO = '{0}' ", kaihatsuFugo)
                            .AppendLine(" AND R.KAITEI_NO = ( ")
                            .AppendLine(" SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO ")
                            .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0553 ")
                            .AppendLine(" WHERE BUHIN_NO_OYA = R.BUHIN_NO_OYA ")
                            .AppendLine(" AND BUHIN_NO_KO = R.BUHIN_NO_KO  ")
                            .AppendLine(" AND KAIHATSU_FUGO = R.KAIHATSU_FUGO ) ")

                        End With
                        Dim R553Vo As Rhac0553Vo = db.QueryForObject(Of Rhac0553Vo)(sql.ToString)

                        If R553Vo Is Nothing Then
                            With sql
                                .Remove(0, .Length)
                                .AppendLine(" SELECT R.BUHIN_NO_OYA ")
                                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 R ")
                                .AppendLine(" WHERE ")
                                .AppendFormat(" R.BUHIN_NO_OYA = '{0}' ", BVoList(index).BuhinNo)
                                .AppendFormat(" AND R.BUHIN_NO_KO = '{0}' ", BuhinNo)
                                .AppendLine(" AND R.KAITEI_NO = ( ")
                                .AppendLine(" SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( KAITEI_NO,'' ) ) ) AS KAITEI_NO ")
                                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0551 ")
                                .AppendLine(" WHERE BUHIN_NO_OYA = R.BUHIN_NO_OYA ")
                                .AppendLine(" AND BUHIN_NO_KO = R.BUHIN_NO_KO ) ")
                            End With
                            Dim R551Vo As Rhac0551Vo = db.QueryForObject(Of Rhac0551Vo)(sql.ToString)

                            If R551Vo Is Nothing Then

                                '構成に存在しないが編集には存在'
                                If BVoList.Count = 1 Then
                                    ResultVo.BuhinNo = BVoList(index).BuhinNo
                                    ResultVo.BuhinNoKbn = BVoList(index).BuhinNoKbn
                                    ResultVo.MakerCode = BVoList(index).MakerCode
                                    Return ResultVo
                                Else
                                    Continue For
                                End If
                            Else
                                ResultVo.BuhinNo = R551Vo.BuhinNoOya
                                ResultVo.BuhinNoKbn = BVoList(index).BuhinNoKbn
                                ResultVo.MakerCode = BVoList(index).MakerCode
                                Return ResultVo
                            End If
                        Else
                            ResultVo.BuhinNo = R553Vo.BuhinNoOya
                            ResultVo.BuhinNoKbn = BVoList(index).BuhinNoKbn
                            ResultVo.MakerCode = BVoList(index).MakerCode
                            Return ResultVo
                        End If
                    Else
                        ResultVo.BuhinNo = R552Vo.BuhinNoOya
                        ResultVo.BuhinNoKbn = BVoList(index).BuhinNoKbn
                        ResultVo.MakerCode = BVoList(index).MakerCode
                        Return ResultVo
                    End If
                Next
            Next

            Return ResultVo

        End Function

        ''' <summary>
        ''' 行ID更新用に手配号車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>該当する手配号車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByDummyTehaiGousya(ByVal shisakuEventCode As String, _
                                               ByVal shisakuListCode As String, _
                                               ByVal shisakuBukaCode As String, _
                                               ByVal shisakuBlockNo As String) As TYosanSetteiGousyaVo Implements YosanSetteiBuhinSakuseiDao.FindByDummyTehaiGousya

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT DISTINCT TG.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN TK ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA TG ")
                .AppendLine(" ON TG.SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND TG.YOSAN_LIST_CODE = TK.YOSAN_LIST_CODE ")
                .AppendLine(" AND TG.YOSAN_BUKA_CODE = TK.YOSAN_BUKA_CODE ")
                .AppendLine(" AND TG.YOSAN_BLOCK_NO = TK.YOSAN_BLOCK_NO ")
                .AppendLine(" AND TG.BUHIN_NO_HYOUJI_JUN = TK.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TK.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TK.YOSAN_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" ORDER BY TG.BUHIN_NO_HYOUJI_JUN DESC, TG.YOSAN_GOUSYA_HYOUJI_JUN DESC ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TYosanSetteiGousyaVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' ブロックNoのリストを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード </param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByBlockNoList(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuSekkeiBlockVo) Implements YosanSetteiBuhinSakuseiDao.FindByBlockNoList
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT DISTINCT BE.SHISAKU_BUKA_CODE, BE.SHISAKU_BLOCK_NO, SB.UNIT_KBN, BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP BE ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BE.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND BE.GYOU_ID = '000' ")
                .AppendLine(" ORDER BY BE.SHISAKU_BLOCK_NO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' ブロックNo毎に部品編集TMPと部品編集号車TMPの合体リストを取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード </param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByTehaiMergeList(ByVal shisakuEventCode As String, _
                                             ByVal shisakuListCode As String, _
                                             ByVal shisakuBukaCode As String, _
                                             ByVal shisakuBlockNo As String, _
                                             ByVal shisakuBlockNoKaiteiNo As String) As List(Of YosanSetteiBuhinEditTmpVo) Implements YosanSetteiBuhinSakuseiDao.FindByTehaiMergeList

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT BET.*, BEG.SHISAKU_GOUSYA_HYOUJI_JUN, BEG.SHISAKU_GOUSYA, BEG.INSU_SURYO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP BET ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA_TMP BEG ")
                .AppendLine(" ON BEG.SHISAKU_EVENT_CODE = BET.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEG.SHISAKU_BUKA_CODE = BET.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEG.SHISAKU_BLOCK_NO = BET.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEG.SHISAKU_BLOCK_NO_KAITEI_NO = BET.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEG.BUHIN_NO_HYOUJI_JUN = BET.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" AND BEG.GYOU_ID = BET.GYOU_ID ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BET.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BET.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND BET.SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND BET.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendLine(" AND NOT BET.GYOU_ID = '888' ")
                .AppendLine(" AND NOT BET.GYOU_ID = '999' ")
                .AppendLine(" ORDER BY BET.LEVEL, BEG.SHISAKU_GOUSYA_HYOUJI_JUN ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of YosanSetteiBuhinEditTmpVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 号車毎の開発符号を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuGousyaHyoujiJun">号車表示順</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByGousyaKaihatsuFugo(ByVal shisakuEventCode As String, ByVal shisakuGousyaHyoujiJun As Integer) As TShisakuEventBaseVo Implements YosanSetteiBuhinSakuseiDao.FindByGousyaKaihatsuFugo
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE ")
                .AppendFormat(" WHERE SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND HYOJIJUN_NO = {0} ", shisakuGousyaHyoujiJun)
            End With

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventBaseVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByTehaiKihonList(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TYosanSetteiBuhinVo) Implements YosanSetteiBuhinSakuseiDao.FindByTehaiKihonList
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM  " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN ")
                .AppendFormat(" WHERE SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND YOSAN_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" ORDER BY YOSAN_BLOCK_NO, BUHIN_NO_HYOUJI_JUN ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TYosanSetteiBuhinVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 部品編集TMP情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditTmp(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TYosanSetteiBuhinTmpVo) Implements YosanSetteiBuhinSakuseiDao.FindByBuhinEditTmp
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP ")
                .AppendFormat(" WHERE SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" ORDER BY SHISAKU_BLOCK_NO, BUHIN_NO_HYOUJI_JUN ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TYosanSetteiBuhinTmpVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 試作部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuGroup">グループNo</param>
        ''' <returns>試作部品編集情報リスト</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhin3D(ByVal shisakuEventCode As String, _
                                         ByVal shisakuGroup As String, _
                                         ByVal shisakuGousya As Dictionary(Of Integer, String)) As List(Of YosanSetteiBuhinEditTmpVo) Implements YosanSetteiBuhinSakuseiDao.FindByBuhin3D
            Dim db As New EBomDbClient
            Dim BuhinList As New List(Of YosanSetteiBuhinEditTmpVo)
            Dim sql As New StringBuilder

            Dim gousya As New ArrayList
            For Each g As String In shisakuGousya.Values
                gousya.Add("'" & g & "'")
            Next

            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT B.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI K ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B ")
                .AppendLine(" ON B.SHISAKU_EVENT_CODE = K.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND B.HYOJIJUN_NO = K.HYOJIJUN_NO ")
                .AppendLine(" AND (NOT B.SHISAKU_SYUBETU = 'D' OR B.SHISAKU_SYUBETU = 'W' OR B.SHISAKU_SYUBETU IS NULL) ")
                .AppendFormat(" AND B.SHISAKU_GOUSYA IN ({0})", String.Join(",", gousya.ToArray))
                .AppendLine(" WHERE ")
                .AppendFormat(" K.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND K.SHISAKU_GROUP = '{0}' ", shisakuGroup)
                .AppendLine(" ORDER BY B.HYOJIJUN_NO ")
            End With

            '小分けにする'
            '号車リスト'
            For Each BaseVo As TShisakuEventBaseVo In db.QueryForList(Of TShisakuEventBaseVo)(sql.ToString)
                With sql
                    .Remove(0, .Length)
                    .AppendLine(" SELECT SB.SHISAKU_BLOCK_NO, SB.SHISAKU_BLOCK_NO_KAITEI_NO, SB.KACHOU_SYOUNIN_JYOUTAI, SB.BLOCK_FUYOU ")
                    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                    .AppendLine(" WHERE  ")
                    .AppendFormat(" SB.SHISAKU_EVENT_CODE = '{0}'", BaseVo.ShisakuEventCode)
                    .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO ")
                    .AppendLine(" = (SELECT MAX(SHISAKU_BLOCK_NO_KAITEI_NO) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
                    .AppendLine(" WHERE SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                    .AppendLine(" AND SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                    .AppendLine(" AND SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ) ")
                    .AppendLine(" ORDER BY SB.SHISAKU_BLOCK_NO ")
                End With

                Dim bukaCode As String = ""
                Dim blockNo As String = ""
                Dim flag As Boolean = True
                '設計ブロックINSTLリスト'

                For Each sekkeiblockVo As SekkeiBlockInstlVoHelper In db.QueryForList(Of SekkeiBlockInstlVoHelper)(sql.ToString)
                    If sekkeiblockVo.ShisakuBlockNoKaiteiNo <> "000" Then
                        '承認が完了していない'
                        If sekkeiblockVo.KachouSyouninJyoutai <> "36" Then
                            '前回の改定を取得する'
                            Dim zenkaiBlockNoKaiteiNo As String = Right("000" + (Integer.Parse(sekkeiblockVo.ShisakuBlockNoKaiteiNo) - 1).ToString, 3)
                            sekkeiblockVo.ShisakuBlockNoKaiteiNo = zenkaiBlockNoKaiteiNo
                            flag = False
                        End If
                    Else
                        '改訂000がブロック不要なら出さない'
                        If sekkeiblockVo.BlockFuyou = "1" Then
                            '同じブロックなら出さないようにする'
                            blockNo = sekkeiblockVo.ShisakuBlockNo
                            bukaCode = sekkeiblockVo.ShisakuBukaCode
                            flag = False
                            Continue For
                        Else
                            flag = True
                        End If
                    End If

                    '2012/02/23 ブロック要、不要は見ないで最新を取る'
                    With sql
                        .Remove(0, .Length)
                        .AppendLine(" SELECT SBI.*, SB.KACHOU_SYOUNIN_JYOUTAI, SB.BLOCK_FUYOU ")
                        .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI ")
                        .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB ")
                        .AppendLine(" ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE")
                        .AppendLine(" AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                        .AppendLine(" AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")
                        .AppendLine(" AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                        .AppendLine(" AND ( SB.BLOCK_FUYOU = '0' OR SB.BLOCK_FUYOU = '') ")
                        .AppendLine(" WHERE  ")
                        .AppendFormat(" SBI.SHISAKU_EVENT_CODE = '{0}'", BaseVo.ShisakuEventCode)
                        .AppendFormat(" AND SBI.SHISAKU_GOUSYA = '{0}'", BaseVo.ShisakuGousya)
                        .AppendFormat(" AND SBI.SHISAKU_BLOCK_NO = '{0}'", sekkeiblockVo.ShisakuBlockNo)
                        .AppendFormat(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}'", sekkeiblockVo.ShisakuBlockNoKaiteiNo)
                        .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")
                        .AppendLine(" AND SBI.INSU_SURYO > 0 ")
                        .AppendLine(" ORDER BY SBI.SHISAKU_BLOCK_NO ")
                    End With

                    For Each sekkeiBlockInstlVo As SekkeiBlockInstlVoHelper In db.QueryForList(Of SekkeiBlockInstlVoHelper)(sql.ToString)
                        If Not flag Then
                            '同じ部課コードかチェック'
                            If bukaCode = sekkeiBlockInstlVo.ShisakuBukaCode Then
                                '同じブロックかチェック'
                                If blockNo = sekkeiBlockInstlVo.ShisakuBlockNo Then
                                    Continue For
                                End If
                            End If
                        End If

                        With sql
                            .Remove(0, .Length)
                            .AppendLine(" SELECT BE.*, BEI.INSU_SURYO ")
                            .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
                            .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE ")
                            .AppendLine(" ON BE.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                            .AppendLine(" AND BE.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                            .AppendLine(" AND BE.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                            .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                            .AppendLine(" AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN ")
                            .AppendLine(" WHERE ")
                            .AppendFormat(" BEI.SHISAKU_EVENT_CODE = '{0}'", sekkeiBlockInstlVo.ShisakuEventCode)
                            .AppendFormat(" AND BEI.SHISAKU_BUKA_CODE = '{0}'", sekkeiBlockInstlVo.ShisakuBukaCode)
                            .AppendFormat(" AND BEI.SHISAKU_BLOCK_NO = '{0}'", sekkeiBlockInstlVo.ShisakuBlockNo)
                            .AppendFormat(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = '{0}'", sekkeiBlockInstlVo.ShisakuBlockNoKaiteiNo)
                            .AppendFormat(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = '{0}'", sekkeiBlockInstlVo.InstlHinbanHyoujiJun)
                        End With

                        Dim buhinEditList As List(Of YosanSetteiBuhinEditTmpVo) = db.QueryForList(Of YosanSetteiBuhinEditTmpVo)(sql.ToString)

                        For Each buhinEditVo As YosanSetteiBuhinEditTmpVo In buhinEditList
                            buhinEditVo.BuhinNo = Trim(buhinEditVo.BuhinNo)
                            buhinEditVo.ShisakuGousya = BaseVo.ShisakuGousya
                            buhinEditVo.ShisakuGousyaHyoujiJun = BaseVo.HyojijunNo
                        Next
                        BuhinList.AddRange(buhinEditList)
                    Next
                Next
            Next

            Return BuhinList
        End Function

#End Region

#Region "更新する処理(Update)"

        ''' <summary>
        ''' 部品情報号車情報(TMP)を更新する
        ''' </summary>
        ''' <param name="gousyaTmpVo">号車TMP用情報</param>
        ''' <remarks></remarks>
        Public Sub UpdateByBuhinEditGousyaTmp(ByVal gousyaTmpVo As TYosanSetteiGousyaTmpVo) Implements YosanSetteiBuhinSakuseiDao.UpdateByBuhinEditGousyaTmp
            Dim sql As String = _
            " UPDATE  " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA_TMP " _
            & " SET INSU_SURYO = @InsuSuryo, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            & " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            & " AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo " _
            & " AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
            & " AND SHISAKU_GOUSYA_HYOUJI_JUN = @ShisakuGousyaHyoujiJun "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TYosanSetteiGousyaTmpVo
            param.ShisakuEventCode = gousyaTmpVo.ShisakuEventCode
            param.ShisakuBukaCode = gousyaTmpVo.ShisakuBukaCode
            param.ShisakuBlockNo = gousyaTmpVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = gousyaTmpVo.ShisakuBlockNoKaiteiNo
            param.BuhinNoHyoujiJun = gousyaTmpVo.BuhinNoHyoujiJun
            param.ShisakuGousyaHyoujiJun = gousyaTmpVo.ShisakuGousyaHyoujiJun
            param.InsuSuryo = gousyaTmpVo.InsuSuryo
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)

        End Sub

        ''' <summary>
        ''' 手配号車情報の行IDを更新する
        ''' </summary>
        ''' <param name="tehaiGousyaVoList">手配号車情報リスト</param>
        ''' <remarks></remarks>
        Public Sub UpdateByTehaiGousyaGyouID(ByVal tehaiGousyaVoList As List(Of TYosanSetteiGousyaVo)) Implements YosanSetteiBuhinSakuseiDao.UpdateByTehaiGousyaGyouID
            Dim sql As String = _
            " UPDATE  " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA " _
            & " SET GYOU_ID = @GyouId, " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND YOSAN_LIST_CODE = @YosanListCode " _
            & " AND YOSAN_BUKA_CODE = @YosanBukaCode " _
            & " AND YOSAN_BLOCK_NO = @YosanBlockNo " _
            & " AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
            & " AND YOSAN_GOUSYA_HYOUJI_JUN = @YosanGousyaHyoujiJun "

            Dim db As New EBomDbClient

            Dim blockNo As String
            Dim gyouId As Integer = 0

            For index As Integer = 0 To tehaiGousyaVoList.Count

                'ブロックNoごとに行IDを振る'
                Dim param As New TYosanSetteiGousyaVo
                blockNo = tehaiGousyaVoList(index).YosanBlockNo
                If index > 0 Then
                    If Not blockNo = tehaiGousyaVoList(index - 1).YosanBlockNo Then
                        gyouId = 0
                    End If
                    gyouId = gyouId + 1
                    param.GyouId = Right("000" + gyouId.ToString, 3)
                Else
                    gyouId = gyouId + 1
                    param.GyouId = Right("000" + gyouId.ToString, 3)
                End If

                param.ShisakuEventCode = tehaiGousyaVoList(index).ShisakuEventCode
                param.YosanListCode = tehaiGousyaVoList(index).YosanListCode
                param.YosanBukaCode = tehaiGousyaVoList(index).YosanBukaCode
                param.YosanBlockNo = tehaiGousyaVoList(index).YosanBlockNo
                param.BuhinNoHyoujiJun = tehaiGousyaVoList(index).BuhinNoHyoujiJun
                param.YosanGousyaHyoujiJun = tehaiGousyaVoList(index).YosanGousyaHyoujiJun

                db.Update(sql, param)
            Next
        End Sub

        ''' <summary>
        ''' 手配号車情報の員数を合計する
        ''' </summary>
        ''' <param name="tehaiGousyaVo">手配号車情報リスト</param>
        ''' <remarks></remarks>
        Public Sub UpdateByTehaiGousyaInsuTotal(ByVal tehaiGousyaVo As TYosanSetteiGousyaVo) Implements YosanSetteiBuhinSakuseiDao.UpdateByTehaiGousyaInsuTotal
            Dim sql As String = _
            " UPDATE  " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA " _
            & " SET INSU_SURYO = @InsuSuryo " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND YOSAN_LIST_CODE = @YosanListCode " _
            & " AND YOSAN_BUKA_CODE = @YosanBukaCode " _
            & " AND YOSAN_BLOCK_NO = @YosanBlockNo " _
            & " AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun " _
            & " AND YOSAN_GOUSYA_HYOUJI_JUN = @YosanGousyaHyoujiJun "

            Dim db As New EBomDbClient
            Dim param As New TYosanSetteiGousyaVo
            param.ShisakuEventCode = tehaiGousyaVo.ShisakuEventCode
            param.YosanListCode = tehaiGousyaVo.YosanListCode
            param.YosanBukaCode = tehaiGousyaVo.YosanBukaCode
            param.YosanBlockNo = tehaiGousyaVo.YosanBlockNo
            param.BuhinNoHyoujiJun = tehaiGousyaVo.BuhinNoHyoujiJun
            param.YosanGousyaHyoujiJun = tehaiGousyaVo.YosanGousyaHyoujiJun
            param.InsuSuryo = tehaiGousyaVo.InsuSuryo

            db.Update(sql, param)
        End Sub

        ''' <summary>
        ''' 行IDを振る
        ''' </summary>
        ''' <param name="tehaiKihonVoList">手配基本情報リスト</param>
        ''' <remarks></remarks>
        Public Sub UpdateByGyouId(ByVal tehaiKihonVoList As List(Of TYosanSetteiBuhinVo)) Implements YosanSetteiBuhinSakuseiDao.UpdateByGyouId
            Dim blockNo As String
            Dim gyouId As Integer = 0
            Dim param As New TYosanSetteiBuhinVo
            Dim sqlList(tehaiKihonVoList.Count - 1) As String

            For index As Integer = 0 To tehaiKihonVoList.Count - 1

                param.ShisakuEventCode = tehaiKihonVoList(index).ShisakuEventCode
                param.YosanListCode = tehaiKihonVoList(index).YosanListCode
                param.YosanBukaCode = tehaiKihonVoList(index).YosanBukaCode
                param.YosanBlockNo = tehaiKihonVoList(index).YosanBlockNo
                param.BuhinNoHyoujiJun = tehaiKihonVoList(index).BuhinNoHyoujiJun

                'ブロックNoごとに行IDを振る'
                blockNo = tehaiKihonVoList(index).YosanBlockNo
                If index > 0 Then
                    If Not blockNo = tehaiKihonVoList(index - 1).YosanBlockNo Then
                        gyouId = 0
                    End If
                    gyouId = gyouId + 1
                    param.YosanGyouId = Right("000" + gyouId.ToString, 3)
                Else
                    gyouId = gyouId + 1
                    param.YosanGyouId = Right("000" + gyouId.ToString, 3)
                End If

                Dim sql As String = _
                " UPDATE  " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN " _
                & " SET YOSAN_GYOU_ID = '" & param.YosanGyouId & "'" _
                & " WHERE SHISAKU_EVENT_CODE = '" & param.ShisakuEventCode & "'" _
                & " AND YOSAN_LIST_CODE = '" & param.YosanListCode & "'" _
                & " AND YOSAN_BUKA_CODE = '" & param.YosanBukaCode & "'" _
                & " AND YOSAN_BLOCK_NO = '" & param.YosanBlockNo & "'" _
                & " AND BUHIN_NO_HYOUJI_JUN = " & param.BuhinNoHyoujiJun & ""

                sqlList(index) = sql
            Next
            Using update As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                update.Open()
                update.BeginTransaction()
                For index As Integer = 0 To tehaiKihonVoList.Count - 1
                    Try
                        '空なら何もしない'
                        If Not StringUtil.IsEmpty(sqlList(index)) Then
                            update.ExecuteNonQuery(sqlList(index))
                        End If
                    Catch ex As SqlClient.SqlException
                        Continue For
                    End Try
                Next
                update.Commit()
            End Using

        End Sub

        ''' <summary>
        ''' 専用マークを更新する
        ''' </summary>
        ''' <param name="buhinEditTmpVoList">部品編集TMP情報</param>
        ''' <remarks></remarks>
        Public Sub UpdateBySenyouMark(ByVal buhinEditTmpVoList As List(Of TYosanSetteiBuhinTmpVo)) Implements YosanSetteiBuhinSakuseiDao.UpdateBySenyouMark

            Dim sqlList(buhinEditTmpVoList.Count - 1) As String

            For index As Integer = 0 To buhinEditTmpVoList.Count - 1
                Dim senyoumark As String = ""
                If Not FindBySenyouCheck(buhinEditTmpVoList(index).BuhinNo, buhinEditTmpVoList(index).ShisakuSeihinKbn) Then
                    senyoumark = "*"
                Else
                    senyoumark = ""
                End If



                Dim sql As String = _
                " UPDATE " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP " _
                & " SET SENYOU_MARK = '" & senyoumark & "'" _
                & " WHERE " _
                & " SHISAKU_EVENT_CODE = '" & buhinEditTmpVoList(index).ShisakuEventCode & "'" _
                & " AND SHISAKU_BUKA_CODE = '" & buhinEditTmpVoList(index).ShisakuBukaCode & "'" _
                & " AND SHISAKU_BLOCK_NO = '" & buhinEditTmpVoList(index).ShisakuBlockNo & "'" _
                & " AND SHISAKU_BLOCK_NO_KAITEI_NO = '" & buhinEditTmpVoList(index).ShisakuBlockNoKaiteiNo & "'" _
                & " AND BUHIN_NO_HYOUJI_JUN = '" & buhinEditTmpVoList(index).BuhinNoHyoujiJun & "'" _
                & " AND SHISAKU_LIST_CODE = '" & buhinEditTmpVoList(index).ShisakuListCode & "'"

                sqlList(index) = sql
            Next

            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                insert.Open()
                insert.BeginTransaction()
                For index As Integer = 0 To buhinEditTmpVoList.Count - 1
                    Try
                        If Not StringUtil.IsEmpty(sqlList(index)) Then
                            insert.ExecuteNonQuery(sqlList(index))
                        End If
                    Catch ex As SqlClient.SqlException
                        Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
                        If prm < 0 Then
                            Dim msg As String = sqlList(index) + ex.Message
                            MsgBox(ex.Message)
                        Else
                            Continue For
                        End If
                    End Try
                Next
                insert.Commit()
            End Using



        End Sub

        ''' <summary>
        ''' TMPの行IDを振りなおす
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub UpdateByTmpGyouId(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) Implements YosanSetteiBuhinSakuseiDao.UpdateByTmpGyouId

            Dim sql1 As String = _
            " UPDATE  " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP " _
            & " SET GYOU_ID = '000'" _
            & " WHERE SHISAKU_EVENT_CODE = '" & shisakuEventCode & "'" _
            & " AND SHISAKU_LIST_CODE = '" & shisakuListCode & "'"

            Dim sql2 As String = _
            " UPDATE  " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA_TMP " _
            & " SET GYOU_ID = '000'" _
            & " WHERE SHISAKU_EVENT_CODE = '" & shisakuEventCode & "'"

            Dim db As New EBomDbClient
            db.Update(sql1)
            db.Update(sql2)
        End Sub

        ''' <summary>
        ''' 試作イベントの手配帳作成日を更新
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <remarks></remarks>
        Public Sub UpdateByShisakuEvent(ByVal shisakuEventCode As String) Implements YosanSetteiBuhinSakuseiDao.UpdateByShisakuEvent

            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT " _
            & " SET " _
            & " TEHAICHOU_SAKUSEIBI = CONVERT(INT,CONVERT(VARCHAR,getdate(),112)), " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiKaiteiBlockVo
            Dim aDate As New ShisakuDate

            param.ShisakuEventCode = shisakuEventCode

            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)
        End Sub

#End Region

#Region "挿入する処理(Insert)"

        Private _BuhinList As New ArrayList
        ''' <summary>
        ''' 部品表編集情報テンポラリ情報を追加する
        ''' </summary>
        ''' <param name="BuhinEditTMPvo">部品表編集情報VO</param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="KaihatsuFugo">開発符号</param>
        ''' <param name="SyukeiSuru">集計コードからの展開をするフラグ</param>
        ''' <remarks></remarks>
        Public Sub InsertByBuhinEditTmp(ByVal BuhinEditTMPvo As List(Of YosanSetteiBuhinEditTmpVo), _
                                        ByVal seihinKbn As String, _
                                        ByVal shisakuListCode As String, _
                                        ByVal KaihatsuFugo As String, _
                                        ByVal SyukeiSuru As Boolean, _
                                        ByVal aAsKpsm10p As List(Of AsKPSM10PVo), _
                                        ByVal aAsPartsp As List(Of AsPARTSPVo), _
                                        ByVal aAsGkpsm10p As List(Of AsGKPSM10PVo)) _
                                        Implements YosanSetteiBuhinSakuseiDao.InsertByBuhinEditTMP

            Dim aDate As New ShisakuDate

            Dim strSaishiyoufuka As String = ""
            Dim intShutuzuYoteiDate As Integer = 0
            Dim intTsukurikataNounyu As Integer = 0
            Dim intShisakuBuhinHi As Integer = 0
            Dim intShisakuKataHi As Integer = 0
            Dim intEditTourokubi As Integer = 0
            Dim intEditTourokujikan As Integer = 0
            Dim strTehaiKigou As String = ""
            Dim strSenyouMark As String = ""

            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                insert.Open()
                insert.BeginTransaction()


                Dim sql As New System.Text.StringBuilder
                With sql
                    .AppendLine("INSERT ")
                    .AppendLine("INTO  " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP( ")
                    .AppendLine("  SHISAKU_EVENT_CODE ")
                    .AppendLine("  , SHISAKU_BUKA_CODE ")
                    .AppendLine("  , SHISAKU_BLOCK_NO ")
                    .AppendLine("  , SHISAKU_BLOCK_NO_KAITEI_NO ")
                    .AppendLine("  , BUHIN_NO_HYOUJI_JUN ")
                    .AppendLine("  , GYOU_ID ")
                    .AppendLine("  , LEVEL ")
                    .AppendLine("  , SHUKEI_CODE ")
                    .AppendLine("  , SIA_SHUKEI_CODE ")
                    .AppendLine("  , GENCYO_CKD_KBN ")
                    .AppendLine("  , MAKER_CODE ")
                    .AppendLine("  , MAKER_NAME ")
                    .AppendLine("  , BUHIN_NO ")
                    .AppendLine("  , BUHIN_NO_KBN ")
                    .AppendLine("  , BUHIN_NO_KAITEI_NO ")
                    .AppendLine("  , EDA_BAN ")
                    .AppendLine("  , BUHIN_NAME ")
                    .AppendLine("  , SAISHIYOUFUKA ")
                    .AppendLine("  , SHUTUZU_YOTEI_DATE ")
                    .AppendLine("  , ZAISHITU_KIKAKU_1 ")
                    .AppendLine("  , ZAISHITU_KIKAKU_2 ")
                    .AppendLine("  , ZAISHITU_KIKAKU_3 ")
                    .AppendLine("  , ZAISHITU_MEKKI ")
                    .AppendLine("  , TSUKURIKATA_SEISAKU ")
                    .AppendLine("  , TSUKURIKATA_KATASHIYOU_1 ")
                    .AppendLine("  , TSUKURIKATA_KATASHIYOU_2 ")
                    .AppendLine("  , TSUKURIKATA_KATASHIYOU_3 ")
                    .AppendLine("  , TSUKURIKATA_TIGU ")
                    .AppendLine("  , TSUKURIKATA_NOUNYU ")
                    .AppendLine("  , TSUKURIKATA_KIBO ")
                    .AppendLine("  , BASE_BUHIN_FLG ")
                    .AppendLine("  , SHISAKU_BANKO_SURYO ")
                    .AppendLine("  , SHISAKU_BANKO_SURYO_U ")
                    '.AppendLine("  , MATERIAL_INFO_LENGTH ")
                    '.AppendLine("  , MATERIAL_INFO_WIDTH ")
                    '.AppendLine("  , DATA_ITEM_KAITEI_NO ")
                    '.AppendLine("  , DATA_ITEM_AREA_NAME ")
                    '.AppendLine("  , DATA_ITEM_SET_NAME ")
                    '.AppendLine("  , DATA_ITEM_KAITEI_INFO ")
                    .AppendLine("  , SHISAKU_BUHIN_HI ")
                    .AppendLine("  , SHISAKU_KATA_HI ")
                    .AppendLine("  , BIKOU ")
                    .AppendLine("  , BUHIN_NOTE ")
                    .AppendLine("  , EDIT_TOUROKUBI ")
                    .AppendLine("  , EDIT_TOUROKUJIKAN ")
                    .AppendLine("  , KAITEI_HANDAN_FLG ")
                    .AppendLine("  , TEHAI_KIGOU ")
                    .AppendLine("  , NOUBA ")
                    .AppendLine("  , KYOUKU_SECTION ")
                    .AppendLine("  , SENYOU_MARK ")
                    .AppendLine("  , KOUTAN ")
                    .AppendLine("  , STSR_DHSTBA ")
                    .AppendLine("  , HENKATEN ")
                    .AppendLine("  , SHISAKU_SEIHIN_KBN ")
                    .AppendLine("  , SHISAKU_LIST_CODE ")
                    .AppendLine("  , CREATED_USER_ID ")
                    .AppendLine("  , CREATED_DATE ")
                    .AppendLine("  , CREATED_TIME ")
                    .AppendLine("  , UPDATED_USER_ID ")
                    .AppendLine("  , UPDATED_DATE ")
                    .AppendLine("  , UPDATED_TIME ")
                    .AppendLine(") ")
                    .AppendLine("VALUES ( ")
                    .AppendLine(" @ShisakuEventCode")
                    .AppendLine(",@ShisakuBukaCode")
                    .AppendLine(",@ShisakuBlockNo")
                    .AppendLine(",@ShisakuBlockNoKaiteiNo")
                    .AppendLine(",@BuhinNoHyoujiJun")
                    .AppendLine(",'999'")
                    .AppendLine(",@Level")
                    .AppendLine(",@ShukeiCode")
                    .AppendLine(",@SiaShukeiCode")
                    .AppendLine(",@GencyoCkdKbn")
                    .AppendLine(",@MakerCode")
                    .AppendLine(",@MakerName")
                    .AppendLine(",@BuhinNo")
                    .AppendLine(",@BuhinNoKbn")
                    .AppendLine(",@BuhinNoKaiteiNo")
                    .AppendLine(",@EdaBan")
                    .AppendLine(",@BuhinName")
                    .AppendLine(",@Saishiyoufuka")
                    .AppendLine(",@ShutuzuYoteiDate")
                    .AppendLine(",@ZaishituKikaku1")
                    .AppendLine(",@ZaishituKikaku2")
                    .AppendLine(",@ZaishituKikaku3")
                    .AppendLine(",@ZaishituMekki")
                    .AppendLine(",@TsukurikataSeisaku")
                    .AppendLine(",@TsukurikataKatashiyou1")
                    .AppendLine(",@TsukurikataKatashiyou2")
                    .AppendLine(",@TsukurikataKatashiyou3")
                    .AppendLine(",@TsukurikataTigu")
                    .AppendLine(",@TsukurikataNounyu")
                    .AppendLine(",@TsukurikataKibo")
                    .AppendLine(",@BaseBuhinFlg")
                    .AppendLine(",@ShisakuBankoSuryo")
                    .AppendLine(",@ShisakuBankoSuryoU")
                    '.AppendLine(",@MaterialInfoLength")
                    '.AppendLine(",@MaterialInfoWidth")
                    '.AppendLine(",@DataItemKaiteiNo")
                    '.AppendLine(",@DataItemAreaName")
                    '.AppendLine(",@DataItemSetName")
                    '.AppendLine(",@DataItemKaiteiInfo")
                    .AppendLine(",@ShisakuBuhinHi")
                    .AppendLine(",@ShisakuKataHi")
                    .AppendLine(",@Bikou")
                    .AppendLine(",@BuhinNote")
                    .AppendLine(",@EditTourokubi")
                    .AppendLine(",@EditTourokujikan")
                    .AppendLine(",@KaiteiHandanFlg")
                    .AppendLine(",@TehaiKigou")
                    .AppendLine(",''")
                    .AppendLine(",@KyoukuSection")
                    .AppendLine(",@SenyouMark")
                    .AppendLine(",''")
                    .AppendLine(",''")
                    .AppendLine(",''")
                    .AppendLine(",@SeihinKbn")
                    .AppendLine(",@ShisakuListCode")
                    .AppendLine(",@CreatedUserId")
                    .AppendLine(",@CreatedDate")
                    .AppendLine(",@CreatedTime")
                    .AppendLine(",@UpdatedUserId")
                    .AppendLine(",@UpdatedDate")
                    .AppendLine(",@UpdatedTime")
                    .AppendLine(" ) ")

                End With

                For index As Integer = 0 To BuhinEditTMPvo.Count - 1

                    If BuhinEditTMPvo(index).CreatedUserId = "Merge" Then
                        Continue For
                    End If

                    If BuhinEditTMPvo(index).BuhinNo.TrimEnd = "" Then
                        Continue For
                    End If

                    If BuhinEditTMPvo(index).Saishiyoufuka Is Nothing Then
                        strSaishiyoufuka = ""
                    Else
                        strSaishiyoufuka = BuhinEditTMPvo(index).Saishiyoufuka
                    End If
                    If BuhinEditTMPvo(index).ShutuzuYoteiDate Is Nothing Then
                        intShutuzuYoteiDate = 0
                    Else
                        intShutuzuYoteiDate = BuhinEditTMPvo(index).ShutuzuYoteiDate
                    End If
                    If BuhinEditTMPvo(index).TsukurikataNounyu Is Nothing Then
                        intTsukurikataNounyu = 0
                    Else
                        intTsukurikataNounyu = BuhinEditTMPvo(index).TsukurikataNounyu
                    End If
                    intShisakuBuhinHi = BuhinEditTMPvo(index).ShisakuBuhinHi
                    intShisakuKataHi = BuhinEditTMPvo(index).ShisakuKataHi
                    intEditTourokubi = BuhinEditTMPvo(index).EditTourokubi
                    intEditTourokujikan = BuhinEditTMPvo(index).EditTourokujikan
                    If SyukeiSuru Then
                        strTehaiKigou = ""
                    Else
                        strTehaiKigou = "F"
                    End If
                    '専用マークをここでフル。
                    strSenyouMark = ""
                    If Not FindBySenyouCheckSakusei(BuhinEditTMPvo(index).BuhinNo, BuhinEditTMPvo(index).ShisakuSeihinKbn, _
                                             aAsKpsm10p, aAsPartsp, aAsGkpsm10p) Then
                        strSenyouMark = "*"
                    End If

                    'メーカーコード
                    Dim strMakerCode As String = ""
                    If StringUtil.IsNotEmpty(BuhinEditTMPvo(index).MakerCode) Then
                        strMakerCode = BuhinEditTMPvo(index).MakerCode.TrimEnd
                    Else
                        strMakerCode = ""
                    End If

                    '不正文字(')が入ってくることがあるので
                    '   半角スペースに置き換える。
                    If StringUtil.IsNotEmpty(BuhinEditTMPvo(index).Bikou) Then
                        BuhinEditTMPvo(index).Bikou = BuhinEditTMPvo(index).Bikou.Replace("'", " ")
                    End If
                    If StringUtil.IsNotEmpty(BuhinEditTMPvo(index).TsukurikataKibo) Then
                        BuhinEditTMPvo(index).TsukurikataKibo = BuhinEditTMPvo(index).TsukurikataKibo.Replace("'", " ")
                    End If

                    Dim key As New System.Text.StringBuilder
                    With key
                        .AppendLine(BuhinEditTMPvo(index).ShisakuEventCode)
                        .AppendLine(BuhinEditTMPvo(index).ShisakuBukaCode)
                        .AppendLine(BuhinEditTMPvo(index).ShisakuBlockNo)
                        .AppendLine(BuhinEditTMPvo(index).ShisakuBlockNoKaiteiNo)
                        .AppendLine(BuhinEditTMPvo(index).BuhinNoHyoujiJun)
                    End With

                    If Not _BuhinList.Contains(key.ToString) Then
                        _BuhinList.Add(key.ToString)
                        Try
                            With insert
                                .ClearParameters()
                                .AddParameter("@ShisakuEventCode", BuhinEditTMPvo(index).ShisakuEventCode)
                                .AddParameter("@ShisakuBukaCode", BuhinEditTMPvo(index).ShisakuBukaCode)
                                .AddParameter("@ShisakuBlockNo", BuhinEditTMPvo(index).ShisakuBlockNo)
                                .AddParameter("@ShisakuBlockNoKaiteiNo", BuhinEditTMPvo(index).ShisakuBlockNoKaiteiNo)
                                .AddParameter("@BuhinNoHyoujiJun", BuhinEditTMPvo(index).BuhinNoHyoujiJun)
                                .AddParameter("@Level", BuhinEditTMPvo(index).Level)
                                .AddParameter("@ShukeiCode", BuhinEditTMPvo(index).ShukeiCode)
                                .AddParameter("@SiaShukeiCode", BuhinEditTMPvo(index).SiaShukeiCode)
                                .AddParameter("@GencyoCkdKbn", BuhinEditTMPvo(index).GencyoCkdKbn)
                                .AddParameter("@MakerCode", strMakerCode)
                                .AddParameter("@MakerName", BuhinEditTMPvo(index).MakerName)
                                .AddParameter("@BuhinNo", BuhinEditTMPvo(index).BuhinNo.Trim)
                                .AddParameter("@BuhinNoKbn", BuhinEditTMPvo(index).BuhinNoKbn)
                                .AddParameter("@BuhinNoKaiteiNo", BuhinEditTMPvo(index).BuhinNoKaiteiNo)
                                .AddParameter("@EdaBan", BuhinEditTMPvo(index).EdaBan)
                                .AddParameter("@BuhinName", BuhinEditTMPvo(index).BuhinName)
                                .AddParameter("@Saishiyoufuka", strSaishiyoufuka)
                                .AddParameter("@ShutuzuYoteiDate", intShutuzuYoteiDate)
                                .AddParameter("@ZaishituKikaku1", BuhinEditTMPvo(index).ZaishituKikaku1)
                                .AddParameter("@ZaishituKikaku2", BuhinEditTMPvo(index).ZaishituKikaku2)
                                .AddParameter("@ZaishituKikaku3", BuhinEditTMPvo(index).ZaishituKikaku3)
                                .AddParameter("@ZaishituMekki", BuhinEditTMPvo(index).ZaishituMekki)
                                .AddParameter("@TsukurikataSeisaku", BuhinEditTMPvo(index).TsukurikataSeisaku)
                                .AddParameter("@TsukurikataKatashiyou1", BuhinEditTMPvo(index).TsukurikataKatashiyou1)
                                .AddParameter("@TsukurikataKatashiyou2", BuhinEditTMPvo(index).TsukurikataKatashiyou2)
                                .AddParameter("@TsukurikataKatashiyou3", BuhinEditTMPvo(index).TsukurikataKatashiyou3)
                                .AddParameter("@TsukurikataTigu", BuhinEditTMPvo(index).TsukurikataTigu)
                                .AddParameter("@TsukurikataNounyu", intTsukurikataNounyu)
                                .AddParameter("@TsukurikataKibo", BuhinEditTMPvo(index).TsukurikataKibo)
                                .AddParameter("@BaseBuhinFlg", BuhinEditTMPvo(index).BaseBuhinFlg)
                                .AddParameter("@ShisakuBankoSuryo", BuhinEditTMPvo(index).ShisakuBankoSuryo)
                                .AddParameter("@ShisakuBankoSuryoU", BuhinEditTMPvo(index).ShisakuBankoSuryoU)
                                '.AddParameter("@MaterialInfoLength", BuhinEditTMPvo(index).MaterialInfoLength)
                                '.AddParameter("@MaterialInfoWidth", BuhinEditTMPvo(index).MaterialInfoWidth)
                                '.AddParameter("@DataItemKaiteiNo", BuhinEditTMPvo(index).DataItemKaiteiNo)
                                '.AddParameter("@DataItemAreaName", BuhinEditTMPvo(index).DataItemAreaName)
                                '.AddParameter("@DataItemSetName", BuhinEditTMPvo(index).DataItemSetName)
                                '.AddParameter("@DataItemKaiteiInfo", BuhinEditTMPvo(index).DataItemKaiteiInfo)
                                .AddParameter("@ShisakuBuhinHi", intShisakuBuhinHi)
                                .AddParameter("@ShisakuKataHi", intShisakuKataHi)
                                .AddParameter("@Bikou", BuhinEditTMPvo(index).Bikou)
                                .AddParameter("@BuhinNote", BuhinEditTMPvo(index).BuhinNote)
                                .AddParameter("@EditTourokubi", intEditTourokubi)
                                .AddParameter("@EditTourokujikan", intEditTourokujikan)
                                .AddParameter("@KaiteiHandanFlg", BuhinEditTMPvo(index).KaiteiHandanFlg)
                                .AddParameter("@TehaiKigou", strTehaiKigou)
                                .AddParameter("@KyoukuSection", BuhinEditTMPvo(index).KyoukuSection)
                                .AddParameter("@SenyouMark", strSenyouMark)
                                .AddParameter("@SeihinKbn", seihinKbn)
                                .AddParameter("@ShisakuListCode", shisakuListCode)
                                .AddParameter("@CreatedUserId", LoginInfo.Now.UserId)
                                .AddParameter("@CreatedDate", aDate.CurrentDateDbFormat)
                                .AddParameter("@CreatedTime", aDate.CurrentTimeDbFormat)
                                .AddParameter("@UpdatedUserId", LoginInfo.Now.UserId)
                                .AddParameter("@UpdatedDate", aDate.CurrentDateDbFormat)
                                .AddParameter("@UpdatedTime", aDate.CurrentTimeDbFormat)
                                .ExecuteNonQuery(sql.ToString)
                            End With
                        Catch ex As SqlClient.SqlException
                            Continue For
                        End Try
                    End If
                Next

                insert.Commit()
                Dim test As String = ""
            End Using

        End Sub

        Private _GoushaList As New ArrayList

        ''' <summary>
        ''' 部品表編集号車情報テンポラリ情報を追加する
        ''' </summary>
        ''' <param name="gousyaTmpvo">部品表編集号車情報VO</param>
        ''' <remarks></remarks>
        Public Sub InsertByGousyaTMP(ByVal gousyaTmpvo As List(Of YosanSetteiBuhinEditTmpVo)) Implements YosanSetteiBuhinSakuseiDao.InsertByGousyaTMP

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate

            For index As Integer = 0 To gousyaTmpvo.Count - 1

                Dim sql As String = _
                " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA_TMP ( " _
                & " SHISAKU_EVENT_CODE, " _
                & " SHISAKU_BUKA_CODE, " _
                & " SHISAKU_BLOCK_NO, " _
                & " SHISAKU_BLOCK_NO_KAITEI_NO, " _
                & " BUHIN_NO_HYOUJI_JUN, " _
                & " SHISAKU_GOUSYA_HYOUJI_JUN, " _
                & " GYOU_ID, " _
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
                & " '" & gousyaTmpvo(index).ShisakuEventCode & "' ," _
                & " '" & gousyaTmpvo(index).ShisakuBukaCode & "' ," _
                & " '" & gousyaTmpvo(index).ShisakuBlockNo & "' ," _
                & " '" & gousyaTmpvo(index).ShisakuBlockNoKaiteiNo & "' ," _
                & gousyaTmpvo(index).BuhinNoHyoujiJun & "," _
                & gousyaTmpvo(index).ShisakuGousyaHyoujiJun & "," _
                & " '999', " _
                & " '" & gousyaTmpvo(index).ShisakuGousya & "' ," _
                & gousyaTmpvo(index).InsuSuryo & "," _
                & " '" & LoginInfo.Now.UserId & "' ," _
                & " '" & aDate.CurrentDateDbFormat & "' ," _
                & " '" & aDate.CurrentTimeDbFormat & "' ," _
                & " '" & LoginInfo.Now.UserId & "' ," _
                & " '" & aDate.CurrentDateDbFormat & "' ," _
                & " '" & aDate.CurrentTimeDbFormat & "' " _
                & " ) "

                Dim key As New System.Text.StringBuilder
                With key
                    key.AppendLine(gousyaTmpvo(index).ShisakuEventCode)
                    key.AppendLine(gousyaTmpvo(index).ShisakuBlockNo)
                    key.AppendLine(gousyaTmpvo(index).ShisakuBlockNoKaiteiNo)
                    key.AppendLine(gousyaTmpvo(index).BuhinNoHyoujiJun.ToString)
                    key.AppendLine(gousyaTmpvo(index).ShisakuGousyaHyoujiJun.ToString)
                End With

                If Not _GoushaList.Contains(key.ToString) Then
                    Try
                        db.Insert(sql)
                    Catch ex As Exception
                        '重複キーがあるのなら更新。
                        Dim Usql As String = _
                        " UPDATE  " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA_TMP " _
                        & " SET INSU_SURYO = INSU_SURYO + " & gousyaTmpvo(index).InsuSuryo & "" _
                        & " WHERE SHISAKU_EVENT_CODE = '" & gousyaTmpvo(index).ShisakuEventCode & "'" _
                        & " AND SHISAKU_BUKA_CODE =  '" & gousyaTmpvo(index).ShisakuBukaCode & "'" _
                        & " AND SHISAKU_BLOCK_NO = '" & gousyaTmpvo(index).ShisakuBlockNo & "'" _
                        & " AND SHISAKU_BLOCK_NO_KAITEI_NO = '" & gousyaTmpvo(index).ShisakuBlockNoKaiteiNo & "'" _
                        & " AND BUHIN_NO_HYOUJI_JUN = '" & gousyaTmpvo(index).BuhinNoHyoujiJun & "'" _
                        & " AND SHISAKU_GOUSYA_HYOUJI_JUN = '" & gousyaTmpvo(index).ShisakuGousyaHyoujiJun & "'" _
                        & " AND GYOU_ID = '999'"
                        db.Update(Usql)
                    End Try
                    _GoushaList.Add(key.ToString)
                Else
                    '重複キーがあるのなら更新。
                    Dim Usql As String = _
                    " UPDATE  " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA_TMP " _
                    & " SET INSU_SURYO = INSU_SURYO + " & gousyaTmpvo(index).InsuSuryo & "" _
                    & " WHERE SHISAKU_EVENT_CODE = '" & gousyaTmpvo(index).ShisakuEventCode & "'" _
                    & " AND SHISAKU_BUKA_CODE =  '" & gousyaTmpvo(index).ShisakuBukaCode & "'" _
                    & " AND SHISAKU_BLOCK_NO = '" & gousyaTmpvo(index).ShisakuBlockNo & "'" _
                    & " AND SHISAKU_BLOCK_NO_KAITEI_NO = '" & gousyaTmpvo(index).ShisakuBlockNoKaiteiNo & "'" _
                    & " AND BUHIN_NO_HYOUJI_JUN = '" & gousyaTmpvo(index).BuhinNoHyoujiJun & "'" _
                    & " AND SHISAKU_GOUSYA_HYOUJI_JUN = '" & gousyaTmpvo(index).ShisakuGousyaHyoujiJun & "'" _
                    & " AND GYOU_ID = '999'"
                    db.Update(Usql)
                End If

            Next

        End Sub

        ''' <summary>
        ''' 手配基本情報を追加する
        ''' </summary>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="MergeList">マージした情報</param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="unitKbn">ユニット区分</param>
        ''' <remarks></remarks>
        Public Sub InsertByTehaiKihon2(ByVal MergeList As List(Of YosanSetteiBuhinEditTmpVo), _
                                       ByVal seihinKbn As String, _
                                       ByVal shisakuListCode As String, _
                                       ByVal unitKbn As String, _
                                       ByVal aAsBuhin01 As List(Of AsBUHIN01Vo), _
                                       ByVal aAsKpsm10p As List(Of AsKPSM10PVo), _
                                       ByVal aAsPartsp As List(Of AsPARTSPVo), _
                                       ByVal aAsGkpsm10p As List(Of AsGKPSM10PVo), _
                                       ByVal aTValueDev As List(Of TValueDevVo), _
                                       ByVal aRhac0610 As List(Of Rhac0610Vo)) Implements YosanSetteiBuhinSakuseiDao.InsertByTehaiKihon2

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate

            Dim hikakuImpl As YosanSetteiBuhinHikakuDao = New YosanSetteiBuhinHikakuDaoImpl
            Dim gyouId As Integer = 0
            Dim eventVo As New TShisakuEventVo
            Dim KoutanTorihikisaki As New TYosanSetteiBuhinTmpVo
            Dim beforeBuhinNoHyoujiJun As Integer

            Dim strShukeiCode As String = ""
            Dim intTotalInsuSuryo As Integer = 0
            Dim strTehaiKigou As String = ""
            Dim strNouba As String = ""
            Dim strStsrDhstba As String = ""
            Dim strKoutan As String = ""
            Dim strTorihikisakiCode As String = ""
            Dim strMakerCode As String = ""

            eventVo = FindByUnitKbn(MergeList(0).ShisakuEventCode)

            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                insert.Open()
                insert.BeginTransaction()

                For index As Integer = 0 To MergeList.Count - 1
                    'マージ対象は除く
                    If MergeList(index).CreatedUserId = "Merge" Then
                        Continue For
                    End If

                    If index = 0 Then
                        beforeBuhinNoHyoujiJun = MergeList(index).BuhinNoHyoujiJun
                    Else
                        If beforeBuhinNoHyoujiJun = MergeList(index).BuhinNoHyoujiJun Then
                            Continue For
                        End If
                    End If

                    If MergeList(index).ShukeiCode.TrimEnd = "" Then
                        strShukeiCode = MergeList(index).SiaShukeiCode
                    Else
                        strShukeiCode = MergeList(index).ShukeiCode
                    End If
                    'とり方がおかしいのか？'
                    '何故か部品編集情報に存在しないパターンがある'
                    intTotalInsuSuryo = FindByTotalInsuSuryo(MergeList(index).ShisakuEventCode, shisakuListCode, MergeList(index).ShisakuBukaCode, _
                                                                MergeList(index).ShisakuBlockNo, MergeList(index).BuhinNoHyoujiJun)
                    '合計が0未満ならFにする'
                    If intTotalInsuSuryo < 0 Then
                        strTehaiKigou = "F"
                        strNouba = ""
                    Else
                        strTehaiKigou = MergeList(index).TehaiKigou
                        strNouba = MergeList(index).Nouba
                    End If
                    '図面設通No取得'
                    strStsrDhstba = FindByZumenNoSakusei(MergeList(index).BuhinNo, aAsBuhin01)
                    KoutanTorihikisaki = FindByKoutanTorihikisakiSakusei(MergeList(index).BuhinNo, _
                                                                         aAsKpsm10p, aAsPartsp, aAsGkpsm10p, aAsBuhin01, _
                                                                         aTValueDev, aRhac0610)
                    If Not KoutanTorihikisaki Is Nothing Then
                        strKoutan = KoutanTorihikisaki.Koutan
                        If MergeList(index).MakerCode = "" AndAlso MergeList(index).MakerName = "" Then
                            strTorihikisakiCode = KoutanTorihikisaki.MakerCode
                            strMakerCode = KoutanTorihikisaki.MakerName
                        Else
                            strTorihikisakiCode = MergeList(index).MakerCode
                            strMakerCode = MergeList(index).MakerName
                        End If
                    Else
                        strKoutan = ""
                        If MergeList(index).MakerCode = "" AndAlso MergeList(index).MakerName = "" Then
                            strMakerCode = ""
                            strTorihikisakiCode = ""
                        Else
                            strTorihikisakiCode = MergeList(index).MakerCode.TrimEnd
                            strMakerCode = MergeList(index).MakerName.TrimEnd
                        End If
                    End If

                    '不正文字(')が入ってくることがあるので
                    '   半角スペースに置き換える。
                    If StringUtil.IsNotEmpty(MergeList(index).Bikou) Then
                        MergeList(index).Bikou = MergeList(index).Bikou.Replace("'", " ")
                    End If
                    If StringUtil.IsNotEmpty(MergeList(index).TsukurikataKibo) Then
                        MergeList(index).TsukurikataKibo = MergeList(index).TsukurikataKibo.Replace("'", " ")
                    End If

                    Dim sql As New System.Text.StringBuilder
                    With sql
                        .AppendLine("INSERT ")
                        .AppendLine("INTO  " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN( ")
                        .AppendLine("  SHISAKU_EVENT_CODE ")
                        .AppendLine("  , YOSAN_LIST_CODE ")
                        .AppendLine("  , YOSAN_BUKA_CODE ")
                        .AppendLine("  , YOSAN_BLOCK_NO ")
                        .AppendLine("  , BUHIN_NO_HYOUJI_JUN ")
                        .AppendLine("  , YOSAN_SORT_JUN ")
                        .AppendLine("  , YOSAN_GYOU_ID ")
                        .AppendLine("  , YOSAN_LEVEL ")
                        .AppendLine("  , YOSAN_SHUKEI_CODE ")
                        .AppendLine("  , YOSAN_SIA_SHUKEI_CODE ")
                        .AppendLine("  , YOSAN_BUHIN_NO ")
                        .AppendLine("  , YOSAN_BUHIN_NAME ")
                        .AppendLine("  , YOSAN_INSU ")
                        .AppendLine("  , YOSAN_MAKER_CODE ")
                        .AppendLine("  , YOSAN_KYOUKU_SECTION ")
                        .AppendLine("  , YOSAN_KOUTAN ")
                        .AppendLine("  , YOSAN_TEHAI_KIGOU ")
                        .AppendLine("  , YOSAN_TSUKURIKATA_SEISAKU ")
                        .AppendLine("  , YOSAN_TSUKURIKATA_KATASHIYOU_1 ")
                        .AppendLine("  , YOSAN_TSUKURIKATA_KATASHIYOU_2 ")
                        .AppendLine("  , YOSAN_TSUKURIKATA_KATASHIYOU_3 ")
                        .AppendLine("  , YOSAN_TSUKURIKATA_TIGU ")
                        '.AppendLine("  , YOSAN_TSUKURIKATA_NOUNYU ")
                        .AppendLine("  , YOSAN_TSUKURIKATA_KIBO ")
                        .AppendLine("  , YOSAN_SHISAKU_BUHIN_HI ")
                        .AppendLine("  , YOSAN_SHISAKU_KATA_HI ")
                        .AppendLine("  , YOSAN_BUHIN_NOTE ")
                        .AppendLine("  , YOSAN_BIKOU ")
                        .AppendLine("  , YOSAN_KONKYO_KOKUGAI_KBN ")
                        .AppendLine("  , YOSAN_KONKYO_MIX_BUHIN_HI ")
                        .AppendLine("  , YOSAN_KONKYO_INYOU_MIX_BUHIN_HI ")
                        .AppendLine("  , YOSAN_KONKYO_KEISU_1 ")
                        .AppendLine("  , YOSAN_KONKYO_KOUHOU ")
                        .AppendLine("  , YOSAN_WARITUKE_BUHIN_HI ")
                        .AppendLine("  , YOSAN_WARITUKE_KEISU_2 ")
                        .AppendLine("  , YOSAN_WARITUKE_BUHIN_HI_TOTAL ")
                        .AppendLine("  , YOSAN_WARITUKE_KATA_HI ")
                        .AppendLine("  , YOSAN_KOUNYU_KIBOU_TANKA ")
                        .AppendLine("  , YOSAN_KOUNYU_KIBOU_BUHIN_HI ")
                        .AppendLine("  , YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL ")
                        .AppendLine("  , YOSAN_KOUNYU_KIBOU_KATA_HI ")
                        .AppendLine("  , AUD_FLAG ")
                        .AppendLine("  , AUD_BI ")
                        .AppendLine("  , HENKATEN ")
                        .AppendLine("  , CREATED_USER_ID ")
                        .AppendLine("  , CREATED_DATE ")
                        .AppendLine("  , CREATED_TIME ")
                        .AppendLine("  , UPDATED_USER_ID ")
                        .AppendLine("  , UPDATED_DATE ")
                        .AppendLine("  , UPDATED_TIME ")
                        .AppendLine(") ")
                        .AppendLine("VALUES ( ")
                        .AppendLine(" @ShisakuEventCode")
                        .AppendLine(",@YosanListCode")
                        .AppendLine(",@YosanBukaCode")
                        .AppendLine(",@YosanBlockNo")
                        .AppendLine(",@BuhinNoHyoujiJun")
                        .AppendLine(",@BuhinNoHyoujiJun")
                        .AppendLine(",@YosanGyouId")
                        .AppendLine(",@YosanLevel")
                        .AppendLine(",@YosanShukeiCode")
                        .AppendLine(",@YosanSiaShukeiCode")
                        .AppendLine(",@YosanBuhinNo")
                        .AppendLine(",@YosanBuhinName")
                        .AppendLine(",@YosanInsu")
                        .AppendLine(",@YosanMakerCode")
                        .AppendLine(",@YosanKyoukuSection")
                        .AppendLine(",@YosanKoutan")
                        .AppendLine(",@YosanTehaiKigou")
                        .AppendLine(",@YosanTsukurikataSeisaku")
                        .AppendLine(",@YosanTsukurikataKatashiyou1")
                        .AppendLine(",@YosanTsukurikataKatashiyou2")
                        .AppendLine(",@YosanTsukurikataKatashiyou3")
                        .AppendLine(",@YosanTsukurikataTigu")
                        .AppendLine(",@YosanTsukurikataKibo")
                        .AppendLine(",@YosanShisakuBuhinHi")
                        .AppendLine(",@YosanShisakuKataHi")
                        .AppendLine(",@YosanBuhinNote")
                        .AppendLine(",@YosanBikou")
                        .AppendLine(",''")
                        .AppendLine(",0")
                        .AppendLine(",''")
                        .AppendLine(",1")
                        .AppendLine(",''")
                        .AppendLine(",0")
                        .AppendLine(",1")
                        .AppendLine(",0")
                        .AppendLine(",0")
                        .AppendLine(",0")
                        .AppendLine(",0")
                        .AppendLine(",0")
                        .AppendLine(",0")
                        .AppendLine(",''")
                        .AppendLine(",0")
                        .AppendLine(",@Henkaten")
                        .AppendLine(",@CreatedUserId")
                        .AppendLine(",@CreatedUserDate")
                        .AppendLine(",@CreatedUserTime")
                        .AppendLine(",@UpdatedUserId")
                        .AppendLine(",@UpdatedDate")
                        .AppendLine(",@UpdatedTime")
                        .AppendLine(" ) ")
                    End With

                    Try
                        With insert
                            .ClearParameters()
                            .AddParameter("@ShisakuEventCode", MergeList(index).ShisakuEventCode)
                            .AddParameter("@YosanListCode", shisakuListCode)
                            .AddParameter("@YosanBukaCode", MergeList(index).ShisakuBukaCode)
                            .AddParameter("@YosanBlockNo", MergeList(index).ShisakuBlockNo)
                            .AddParameter("@BuhinNoHyoujiJun", MergeList(index).BuhinNoHyoujiJun)
                            .AddParameter("@YosanGyouId", Right(("000" + gyouId), 3))
                            .AddParameter("@YosanLevel", MergeList(index).Level)
                            .AddParameter("@YosanShukeiCode", MergeList(index).ShukeiCode)
                            .AddParameter("@YosanSiaShukeiCode", MergeList(index).SiaShukeiCode)
                            .AddParameter("@YosanBuhinNo", MergeList(index).BuhinNo)
                            .AddParameter("@YosanBuhinNoKbn", MergeList(index).BuhinNoKbn)
                            .AddParameter("@YosanBuhinName", MergeList(index).BuhinName)
                            .AddParameter("@YosanInsu", intTotalInsuSuryo)
                            .AddParameter("@YosanMakerCode", strTorihikisakiCode)
                            .AddParameter("@YosanKyoukuSection", MergeList(index).KyoukuSection)
                            .AddParameter("@YosanKoutan", strKoutan)
                            .AddParameter("@YosanTehaiKigou", strTehaiKigou)
                            .AddParameter("@YosanTsukurikataSeisaku", MergeList(index).TsukurikataSeisaku)
                            .AddParameter("@YosanTsukurikataKatashiyou1", MergeList(index).TsukurikataKatashiyou1)
                            .AddParameter("@YosanTsukurikataKatashiyou2", MergeList(index).TsukurikataKatashiyou2)
                            .AddParameter("@YosanTsukurikataKatashiyou3", MergeList(index).TsukurikataKatashiyou3)
                            .AddParameter("@YosanTsukurikataTigu", MergeList(index).TsukurikataTigu)
                            .AddParameter("@YosanTsukurikataKibo", MergeList(index).TsukurikataKibo)
                            .AddParameter("@YosanShisakuBuhinHi", MergeList(index).ShisakuBuhinHi)
                            .AddParameter("@YosanShisakuKataHi", MergeList(index).ShisakuKataHi)
                            .AddParameter("@YosanBuhinNote", MergeList(index).BuhinNote)
                            .AddParameter("@YosanBikou", MergeList(index).Bikou)

                            .AddParameter("@Henkaten", MergeList(index).Henkaten)

                            .AddParameter("@CreatedUserId", LoginInfo.Now.UserId)
                            .AddParameter("@CreatedUserDate", aDate.CurrentDateDbFormat)
                            .AddParameter("@CreatedUserTime", aDate.CurrentTimeDbFormat)
                            .AddParameter("@UpdatedUserId", LoginInfo.Now.UserId)
                            .AddParameter("@UpdatedDate", aDate.CurrentDateDbFormat)
                            .AddParameter("@UpdatedTime", aDate.CurrentTimeDbFormat)

                            .ExecuteNonQuery(sql.ToString)
                        End With

                    Catch ex As SqlClient.SqlException
                        Continue For
                    End Try

                    beforeBuhinNoHyoujiJun = MergeList(index).BuhinNoHyoujiJun
                Next

                insert.Commit()
            End Using

        End Sub

        ''' <summary>
        ''' 手配号車情報を追加する
        ''' </summary>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="MergeList">マージした情報</param>
        ''' <remarks></remarks>
        Public Sub InsertByTehaiGousya2(ByVal MergeList As List(Of YosanSetteiBuhinEditTmpVo), _
                               ByVal shisakuListCode As String) Implements YosanSetteiBuhinSakuseiDao.InsertByTehaiGousya2

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim gyouId As Integer = 0

            Dim intInsuSuryo As Integer = 0

            '更新用
            Dim Usql As String = ""

            For index As Integer = 0 To MergeList.Count - 1

                intInsuSuryo = MergeList(index).InsuSuryo
                If intInsuSuryo < 0 Then
                    intInsuSuryo = -1
                End If

                Dim sql As String = _
                " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA ( " _
                & " SHISAKU_EVENT_CODE, " _
                & " YOSAN_LIST_CODE, " _
                & " YOSAN_BUKA_CODE, " _
                & " YOSAN_BLOCK_NO, " _
                & " BUHIN_NO_HYOUJI_JUN, " _
                & " SORT_JUN, " _
                & " GYOU_ID, " _
                & " YOSAN_GOUSYA_HYOUJI_JUN, " _
                & " YOSAN_GOUSYA, " _
                & " INSU_SURYO, " _
                & " CREATED_USER_ID, " _
                & " CREATED_DATE, " _
                & " CREATED_TIME, " _
                & " UPDATED_USER_ID, " _
                & " UPDATED_DATE, " _
                & " UPDATED_TIME " _
                & " ) " _
                & " VALUES ( " _
                & "'" & MergeList(index).ShisakuEventCode & "', " _
                & "'" & shisakuListCode & "', " _
                & "'" & MergeList(index).ShisakuBukaCode & "', " _
                & "'" & MergeList(index).ShisakuBlockNo & "', " _
                & MergeList(index).BuhinNoHyoujiJun & ", " _
                & MergeList(index).BuhinNoHyoujiJun & ", " _
                & "'" & Right(("000" + gyouId), 3) & "', " _
                & MergeList(index).ShisakuGousyaHyoujiJun & ", " _
                & "'" & MergeList(index).ShisakuGousya & "', " _
                & intInsuSuryo & ", " _
                & " '" & LoginInfo.Now.UserId & "', " _
                & " '" & aDate.CurrentDateDbFormat & "', " _
                & " '" & aDate.CurrentTimeDbFormat & "', " _
                & " '" & LoginInfo.Now.UserId & "', " _
                & " '" & aDate.CurrentDateDbFormat & "', " _
                & " '" & aDate.CurrentTimeDbFormat & "' " _
                & " ) "

                Try
                    db.Insert(sql)
                Catch ex As Exception

                    '------------------------------------------------------------------------------------------
                    If MergeList(index).InsuSuryo < 0 Then
                        Usql = _
                        " UPDATE  " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA " _
                        & " SET INSU_SURYO = " & MergeList(index).InsuSuryo _
                        & " WHERE SHISAKU_EVENT_CODE = '" & MergeList(index).ShisakuEventCode & "' " _
                        & " AND YOSAN_LIST_CODE = '" & shisakuListCode & "' " _
                        & " AND YOSAN_BUKA_CODE = '" & MergeList(index).ShisakuBukaCode & "' " _
                        & " AND YOSAN_BLOCK_NO = '" & MergeList(index).ShisakuBlockNo & "' " _
                        & " AND BUHIN_NO_HYOUJI_JUN = " & MergeList(index).BuhinNoHyoujiJun _
                        & " AND YOSAN_GOUSYA_HYOUJI_JUN = " & MergeList(index).ShisakuGousyaHyoujiJun
                    Else
                        Usql = _
                        " UPDATE  " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA " _
                        & " SET INSU_SURYO = CASE WHEN INSU_SURYO < 0 THEN -1 " _
                        & "                            ELSE INSU_SURYO + " & MergeList(index).InsuSuryo & " END" _
                        & " WHERE SHISAKU_EVENT_CODE = '" & MergeList(index).ShisakuEventCode & "' " _
                        & " AND YOSAN_LIST_CODE = '" & shisakuListCode & "' " _
                        & " AND YOSAN_BUKA_CODE = '" & MergeList(index).ShisakuBukaCode & "' " _
                        & " AND YOSAN_BLOCK_NO = '" & MergeList(index).ShisakuBlockNo & "' " _
                        & " AND BUHIN_NO_HYOUJI_JUN = " & MergeList(index).BuhinNoHyoujiJun _
                        & " AND YOSAN_GOUSYA_HYOUJI_JUN = " & MergeList(index).ShisakuGousyaHyoujiJun
                    End If
                    db.Update(Usql)
                    '------------------------------------------------------------------------------------------

                End Try
            Next

        End Sub

        ''' <summary>
        ''' ダミー列の追加
        ''' </summary>
        ''' <param name="DummyVo">手配号車情報</param>
        ''' <remarks></remarks>
        Public Sub InserByDummyTehaiGousya(ByVal DummyVo As TYosanSetteiGousyaVo) Implements YosanSetteiBuhinSakuseiDao.InserByDummyTehaiGousya
            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA ( " _
            & " SHISAKU_EVENT_CODE, " _
            & " YOSAN_LIST_CODE, " _
            & " YOSAN_BUKA_CODE, " _
            & " YOSAN_BLOCK_NO, " _
            & " BUHIN_NO_HYOUJI_JUN, " _
            & " SORT_JUN, " _
            & " GYOU_ID, " _
            & " YOSAN_GOUSYA_HYOUJI_JUN, " _
            & " YOSAN_GOUSYA, " _
            & " INSU_SURYO, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & " @ShisakuEventCode, " _
            & " @YosanListCode, " _
            & " @YosanBukaCode, " _
            & " @YosanBlockNo, " _
            & " @BuhinNoHyoujiJun, " _
            & " @SortJun, " _
            & " @GyouId, " _
            & " @YosanGousyaHyoujiJun, " _
            & " @YosanGousya, " _
            & " @InsuSuryo, " _
            & " @CreatedUserId, " _
            & " @CreatedDate, " _
            & " @CreatedTime, " _
            & " @UpdatedUserId, " _
            & " @UpdatedDate, " _
            & " @UpdatedTime " _
            & " ) "

            Dim db As New EBomDbClient
            Dim param As New TYosanSetteiGousyaVo
            Dim aDate As New ShisakuDate
            param.ShisakuEventCode = DummyVo.ShisakuEventCode
            param.YosanListCode = DummyVo.YosanListCode
            param.YosanBukaCode = DummyVo.YosanBukaCode
            param.YosanBlockNo = DummyVo.YosanBlockNo
            param.BuhinNoHyoujiJun = DummyVo.BuhinNoHyoujiJun
            param.YosanGousyaHyoujiJun = DummyVo.YosanGousyaHyoujiJun + 2
            param.yosanGousya = "DUMMY"
            param.GyouId = DummyVo.GyouId
            param.InsuSuryo = 0
            param.SortJun = DummyVo.BuhinNoHyoujiJun
            param.CreatedUserId = LoginInfo.Now.UserId
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Insert(sql, param)
        End Sub

        ''' <summary>
        ''' 手配号車情報を追加する
        ''' </summary>
        ''' <param name="gousyaVo">手配号車情報</param>
        ''' <remarks></remarks>
        Public Sub InsertByTehaiGousya(ByVal gousyaVo As TYosanSetteiGousyaVo) Implements YosanSetteiBuhinSakuseiDao.InsertByTehaiGousya
            Dim sb As New StringBuilder

            With sb
                .Remove(0, .Length)
                .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA ( ")
                .AppendLine(" SHISAKU_EVENT_CODE, ")
                .AppendLine(" YOSAN_LIST_CODE, ")
                .AppendLine(" YOSAN_BUKA_CODE, ")
                .AppendLine(" YOSAN_BLOCK_NO, ")
                .AppendLine(" BUHIN_NO_HYOUJI_JUN, ")
                .AppendLine(" SORT_JUN, ")
                .AppendLine(" GYOU_ID, ")
                .AppendLine(" YOSAN_GOUSYA_HYOUJI_JUN, ")
                .AppendLine(" YOSAN_GOUSYA, ")
                .AppendLine(" INSU_SURYO, ")
                .AppendLine(" CREATED_USER_ID, ")
                .AppendLine(" CREATED_DATE, ")
                .AppendLine(" CREATED_TIME, ")
                .AppendLine(" UPDATED_USER_ID, ")
                .AppendLine(" UPDATED_DATE, ")
                .AppendLine(" UPDATED_TIME ")
                .AppendLine(" ) ")
                .AppendLine(" VALUES ( ")
                .AppendLine(" @ShisakuEventCode, ")
                .AppendLine(" @YosanListCode, ")
                .AppendLine(" @YosanBukaCode, ")
                .AppendLine(" @YosanBlockNo, ")
                .AppendLine(" @BuhinNoHyoujiJun, ")
                .AppendLine(" @SortJun, ")
                .AppendLine(" @GyouId, ")
                .AppendLine(" @YosanGousyaHyoujiJun, ")
                .AppendLine(" @YosanGousya, ")
                .AppendLine(" @InsuSuryo, ")
                .AppendLine(" @CreatedUserId, ")
                .AppendLine(" @CreatedDate, ")
                .AppendLine(" @CreatedTime, ")
                .AppendLine(" @UpdatedUserId, ")
                .AppendLine(" @UpdatedDate, ")
                .AppendLine(" @UpdatedTime ")
                .AppendLine(" ) ")
            End With

            Dim db As New EBomDbClient
            Dim param As New TYosanSetteiGousyaVo
            Dim aDate As New ShisakuDate
            param.ShisakuEventCode = gousyaVo.ShisakuEventCode
            param.YosanListCode = gousyaVo.YosanListCode
            param.YosanBukaCode = gousyaVo.YosanBukaCode
            param.YosanBlockNo = gousyaVo.YosanBlockNo
            param.BuhinNoHyoujiJun = 0
            param.YosanGousyaHyoujiJun = gousyaVo.YosanGousyaHyoujiJun
            param.YosanGousya = gousyaVo.YosanGousya
            param.GyouId = ""
            param.InsuSuryo = 0
            param.SortJun = 0
            param.CreatedUserId = LoginInfo.Now.UserId
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Insert(sb.ToString, param)
        End Sub

#End Region

#Region "マージ専用"

        ''' <summary>
        ''' 手配基本情報を更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="totalInsuSuryo">合計員数数量</param>
        ''' <remarks></remarks>
        Public Sub UpdateByTehaiKihon(ByVal shisakuEventCode As String, _
                                ByVal shisakuListCode As String, _
                                ByVal shisakuListCodeKaiteNo As String, _
                                ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String, _
                                ByVal buhinNoHyoujiJun As String, _
                                ByVal totalInsuSuryo As Integer) Implements YosanSetteiBuhinSakuseiDao.UpdateByTehaiKihon

            Dim param As New TYosanSetteiBuhinVo
            param.ShisakuEventCode = shisakuEventCode
            param.YosanListCode = shisakuListCode
            param.YosanBukaCode = shisakuBukaCode
            param.YosanBlockNo = shisakuBlockNo
            param.BuhinNoHyoujiJun = buhinNoHyoujiJun
            param.YosanInsu = totalInsuSuryo

            Dim sql As String = _
            " UPDATE  " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN " _
            & " SET YOSAN_INSU = " & param.YosanInsu & ", " _
            & " WHERE SHISAKU_EVENT_CODE = '" & param.ShisakuEventCode & "'" _
            & " AND YOSAN_LIST_CODE = '" & param.YosanListCode & "'" _
            & " AND YOSAN_BUKA_CODE = '" & param.YosanBukaCode & "'" _
            & " AND YOSAN_BLOCK_NO = '" & param.YosanBlockNo & "'" _
            & " AND BUHIN_NO_HYOUJI_JUN = " & param.BuhinNoHyoujiJun

            Dim db As New EBomDbClient
            db.Update(sql)
        End Sub

        ''' <summary>
        ''' 手配号車情報の部品番号表示順を更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">検索用部品番号表示順</param>
        ''' <param name="mergebuhinNoHyoujiJun">更新後の部品番号表示順</param>
        ''' <remarks></remarks>
        Public Sub UpdateByTehaiGousyaBuhinNoHyoujiJun(ByVal shisakuEventCode As String, _
                                                  ByVal shisakuListCode As String, _
                                                  ByVal shisakuListCodeKaiteNo As String, _
                                                  ByVal shisakuBukaCode As String, _
                                                  ByVal shisakuBlockNo As String, _
                                                  ByVal buhinNoHyoujiJun As String, _
                                                  ByVal mergebuhinNoHyoujiJun As String) Implements YosanSetteiBuhinSakuseiDao.UpdateByTehaiGousyaBuhinNoHyoujiJun
            Dim param As New TYosanSetteiBuhinVo
            param.ShisakuEventCode = shisakuEventCode
            param.YosanListCode = shisakuListCode
            param.YosanBukaCode = shisakuBukaCode
            param.YosanBlockNo = shisakuBlockNo
            param.BuhinNoHyoujiJun = mergebuhinNoHyoujiJun

            Dim sql As String = _
            " UPDATE  " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA " _
            & " SET TOTAL_INSU_SURYO = " & param.BuhinNoHyoujiJun & ", " _
            & " WHERE SHISAKU_EVENT_CODE = '" & param.ShisakuEventCode & "'" _
            & " AND YOSAN_LIST_CODE = '" & param.YosanListCode & "'" _
            & " AND YOSAN_BUKA_CODE = '" & param.YosanBukaCode & "'" _
            & " AND YOSAN_BLOCK_NO = '" & param.YosanBlockNo & "'" _
            & " AND BUHIN_NO_HYOUJI_JUN = '" & buhinNoHyoujiJun & "' "

            Dim db As New EBomDbClient
            db.Update(sql)
        End Sub

#End Region

#Region "削除する処理"

        ''' <summary>
        ''' 部品表号車情報(TMP)を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByGousyaTmp(ByVal shisakuEventCode As String) Implements YosanSetteiBuhinSakuseiDao.DeleteByGousyaTmp
            Dim sql As String = _
            " DELETE GO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA_TMP GO " _
            & " WHERE " _
            & " GO.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "'"

            Dim db As New EBomDbClient
            db.Delete(sql)

        End Sub

        ''' <summary>
        ''' 部品表号車情報(TMP)を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByBuhinTmp(ByVal shisakuEventCode As String) Implements YosanSetteiBuhinSakuseiDao.DeleteByBuhinTmp
            Dim sql As String = _
            " DELETE ET " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP ET " _
            & " WHERE " _
            & " ET.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "'"

            Dim db As New EBomDbClient
            db.Delete(sql)
        End Sub

#End Region

    End Class
End Namespace