Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports System.Text

Namespace EventEdit.Dao

    Public Class SeisakuIchiranDaoImpl : Inherits DaoEachFeatureImpl
        Implements SeisakuIchiranDao

#Region "製作一覧のヘッダーの一覧情報を取得する"
        ''' <summary>
        ''' 製作一覧発行処理Spread一覧情報を取得する
        ''' </summary>
        ''' <param name="status">ステータス</param>
        ''' <remarks></remarks>
        Public Function GetSeisakuIchiranHdSpreadList(ByVal status As String, _
                                                      ByVal strHakouNo As String, _
                                                      ByVal strKaihatsuFugo As String, _
                                                      ByVal strEvent As String, _
                                                      ByVal strEventName As String, _
                                                      ByVal strKaiteiNo As String) As List(Of TSeisakuHakouHdVo) Implements SeisakuIchiranDao.GetSeisakuIchiranHdSpreadList
            Dim strSql As String = Nothing
            If status = STATUS_B Then
                strSql = GetStatusSubSet()
            Else
                strSql = GetStatusAllSubSet()
            End If

            Dim sqlSub As String = _
            " WHERE  " _
            & "     HD.KAIHATSU_FUGO LIKE '" & strKaihatsuFugo & "%' AND " _
            & "     HD.SEISAKU_EVENT LIKE '" & strEvent & "%' AND " _
            & "     HD.SEISAKU_EVENT_NAME LIKE '" & strEventName & "%' AND " _
            & "     HD.HAKOU_NO LIKE '" & strHakouNo & "%' AND " _
            & "     HD.KAITEI_NO > '" & strKaiteiNo & "%' "

            Dim sql As String = _
                "SELECT " _
            & "     COALESCE(HAKOU_NO,'') AS HAKOU_NO , " _
            & "     COALESCE(KAITEI_NO,'') AS KAITEI_NO , " _
            & "     COALESCE(KAIHATSU_FUGO,'') AS KAIHATSU_FUGO , " _
            & "     COALESCE(SEISAKU_EVENT,'') AS SEISAKU_EVENT , " _
            & "     COALESCE(SEISAKU_EVENT_NAME,'') AS SEISAKU_EVENT_NAME , " _
            & "     COALESCE(STATUS,'') AS STATUS, " _
            & "     COALESCE(SEISAKUDAISU_KANSEISYA,0) AS SEISAKUDAISU_KANSEISYA , " _
            & "     COALESCE(SEISAKUDAISU_WB,0) AS SEISAKUDAISU_WB , " _
            & "     COALESCE(TOUROKU_DATE ,'') AS TOUROKU_DATE , " _
            & "     COALESCE(CHUSHI_FLG ,'') AS CHUSHI_FLG " _
            & "FROM  " _
            & "   " & MBOM_DB_NAME & ".dbo.T_SEISAKU_HAKOU_HD HD WITH (NOLOCK, NOWAIT) " _
            & "   " + sqlSub + " " _
            & "   " + strSql + " " _
            & "ORDER BY  " _
            & "   KAIHATSU_FUGO, " _
            & "   HAKOU_NO, " _
            & "   KAITEI_NO DESC "

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TSeisakuHakouHdVo)(sql)
        End Function

        Public Function GetStatusSubSet() As String
            Dim sql As String = _
             " AND HD.STATUS = '" & STATUS_B & "'"
            Return Sql
        End Function

        Public Function GetStatusAllSubSet() As String
            Dim sql As String = ""
            Return sql
        End Function

#End Region

#Region "製作一覧のヘッダーの情報を取得する"
        ''' <summary>
        ''' 製作一覧の情報を取得する
        ''' </summary>
        ''' <param name="strHakouNo">発行№</param>
        ''' <param name="strKaiteiNo">改訂№</param>
        ''' <remarks></remarks>
        Public Function GetSeisakuIchiranHd(ByVal strHakouNo As String, ByVal strKaiteiNo As String) _
                As TSeisakuHakouHdVo Implements SeisakuIchiranDao.GetSeisakuIchiranHd
            Dim sql As String = _
                "SELECT * " _
            & "     FROM  " _
            & "   " & MBOM_DB_NAME & ".dbo.T_SEISAKU_HAKOU_HD HD WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & "     HAKOU_NO = @HakouNo " _
            & "     AND KAITEI_NO = @KaiteiNo "

            Dim param As New TSeisakuHakouHdVo
            param.HakouNo = strHakouNo
            param.KaiteiNo = strKaiteiNo

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TSeisakuHakouHdVo)(sql, param)
        End Function
#End Region

#Region "ベース車情報を取得"

        Public Function GetTSeisakuIchiranBase(ByVal hakouNo As String, ByVal kaiteiNo As String) _
                            As List(Of TSeisakuIchiranBaseVo) Implements SeisakuIchiranDao.GetTSeisakuIchiranBase
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_ICHIRAN_BASE BASE WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     HAKOU_NO = @HakouNo AND " _
            & "     KAITEI_NO = @KaiteiNo " _
            & " ORDER BY  " _
            & "     HAKOU_NO," _
            & "     KAITEI_NO," _
            & "     HYOJIJUN_NO"

            Dim db As New EBomDbClient
            Dim param As New TSeisakuIchiranBaseVo
            param.HakouNo = hakouNo
            param.KaiteiNo = kaiteiNo

            Return db.QueryForList(Of TSeisakuIchiranBaseVo)(sql, param)
        End Function

        Public Function GetTSeisakuIchiranBaseGousya(ByVal hakouNo As String, ByVal kaiteiNo As String, ByVal gousya As String) _
                            As TSeisakuIchiranBaseVo Implements SeisakuIchiranDao.GetTSeisakuIchiranBaseGousya
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_ICHIRAN_BASE BASE WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     HAKOU_NO = @HakouNo AND " _
            & "     KAITEI_NO = @KaiteiNo AND " _
            & "     GOUSYA = @Gousya " _
            & " ORDER BY  " _
            & "     HAKOU_NO," _
            & "     KAITEI_NO," _
            & "     HYOJIJUN_NO"

            Dim db As New EBomDbClient
            Dim param As New TSeisakuIchiranBaseVo
            param.HakouNo = hakouNo
            param.KaiteiNo = kaiteiNo
            param.Gousya = gousya

            Return db.QueryForObject(Of TSeisakuIchiranBaseVo)(sql, param)
        End Function

#End Region

#Region "WB車情報を取得"

        Public Function GetTSeisakuIchiranWb(ByVal hakouNo As String, ByVal kaiteiNo As String) _
                            As List(Of TSeisakuIchiranWbVo) Implements SeisakuIchiranDao.GetTSeisakuIchiranWb
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_ICHIRAN_WB WB WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     HAKOU_NO = @HakouNo AND " _
            & "     KAITEI_NO = @KaiteiNo " _
            & " ORDER BY  " _
            & "     HAKOU_NO," _
            & "     KAITEI_NO," _
            & "     HYOJIJUN_NO"

            Dim db As New EBomDbClient
            Dim param As New TSeisakuIchiranWbVo
            param.HakouNo = hakouNo
            param.KaiteiNo = kaiteiNo

            Return db.QueryForList(Of TSeisakuIchiranWbVo)(sql, param)
        End Function

        Public Function GetTSeisakuIchiranWbGousya(ByVal hakouNo As String, ByVal kaiteiNo As String, ByVal gousya As String) _
                            As TSeisakuIchiranWbVo Implements SeisakuIchiranDao.GetTSeisakuIchiranWbGousya
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_ICHIRAN_WB WB WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     HAKOU_NO = @HakouNo AND " _
            & "     KAITEI_NO = @KaiteiNo AND " _
            & "     GOUSYA = @Gousya " _
            & " ORDER BY  " _
            & "     HAKOU_NO," _
            & "     KAITEI_NO," _
            & "     HYOJIJUN_NO"

            Dim db As New EBomDbClient
            Dim param As New TSeisakuIchiranWbVo
            param.HakouNo = hakouNo
            param.KaiteiNo = kaiteiNo
            param.Gousya = gousya

            Return db.QueryForObject(Of TSeisakuIchiranWbVo)(sql, param)
        End Function

#End Region

#Region "WB車情報を取得"

        Public Function GetTSeisakuIchiranKansei(ByVal hakouNo As String, ByVal kaiteiNo As String) _
                            As List(Of TSeisakuIchiranKanseiVo) Implements SeisakuIchiranDao.GetTSeisakuIchiranKansei
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_ICHIRAN_KANSEI KANSEI WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     HAKOU_NO = @HakouNo AND " _
            & "     KAITEI_NO = @KaiteiNo " _
            & " ORDER BY  " _
            & "     HAKOU_NO," _
            & "     KAITEI_NO," _
            & "     HYOJIJUN_NO"

            Dim db As New EBomDbClient
            Dim param As New TSeisakuIchiranKanseiVo
            param.HakouNo = hakouNo
            param.KaiteiNo = kaiteiNo

            Return db.QueryForList(Of TSeisakuIchiranKanseiVo)(sql, param)
        End Function

        Public Function GetTSeisakuIchiranKanseiGousya(ByVal hakouNo As String, ByVal kaiteiNo As String, ByVal gousya As String) _
                            As TSeisakuIchiranKanseiVo Implements SeisakuIchiranDao.GetTSeisakuIchiranKanseiGousya
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_ICHIRAN_KANSEI WB WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     HAKOU_NO = @HakouNo AND " _
            & "     KAITEI_NO = @KaiteiNo AND " _
            & "     GOUSYA = @Gousya " _
            & " ORDER BY  " _
            & "     HAKOU_NO," _
            & "     KAITEI_NO," _
            & "     HYOJIJUN_NO"

            Dim db As New EBomDbClient
            Dim param As New TSeisakuIchiranKanseiVo
            param.HakouNo = hakouNo
            param.KaiteiNo = kaiteiNo
            param.Gousya = gousya

            Return db.QueryForObject(Of TSeisakuIchiranKanseiVo)(sql, param)
        End Function

#End Region

#Region "OP項目列（完成車／ベース車／ＷＢ車）を取得"

        Public Function GetTSeisakuIchiranOpkoumoku(ByVal hakouNo As String, ByVal kaiteiNo As String) _
                            As List(Of TSeisakuIchiranOpkoumokuVo) Implements SeisakuIchiranDao.GetTSeisakuIchiranOpkoumoku
            Dim sql As String = _
            " SELECT HAKOU_NO,KAITEI_NO,KAIHATSU_FUGO,OP_SPEC_CODE,OP_NAME,MIN(OP_KOUMOKU_HYOJIJUN_NO) AS OP_KOUMOKU_HYOJIJUN_NO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_ICHIRAN_OPKOUMOKU OPKOUMOKU WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     HAKOU_NO = @HakouNo AND " _
            & "     KAITEI_NO = @KaiteiNo AND " _
            & "     SYUBETU <> 'B' " _
            & " GROUP BY  " _
            & "     HAKOU_NO, " _
            & "     KAITEI_NO, " _
            & "     KAIHATSU_FUGO, " _
            & "     OP_SPEC_CODE, " _
            & "     OP_NAME " _
            & " ORDER BY " _
            & "     HAKOU_NO, " _
            & "     KAITEI_NO, " _
            & "     KAIHATSU_FUGO, " _
            & "     OP_KOUMOKU_HYOJIJUN_NO "

            Dim db As New EBomDbClient
            Dim param As New TSeisakuIchiranOpkoumokuVo
            param.HakouNo = hakouNo
            param.KaiteiNo = kaiteiNo

            Return db.QueryForList(Of TSeisakuIchiranOpkoumokuVo)(sql, param)
        End Function

        Public Function GetTSeisakuIchiranOpkoumokuGousya(ByVal hakouNo As String, ByVal kaiteiNo As String, _
                                                          ByVal syubetu As String, ByVal gousya As String) _
                            As List(Of TSeisakuIchiranOpkoumokuVo) Implements SeisakuIchiranDao.GetTSeisakuIchiranOpkoumokuGousya
            Dim sql As String = _
            " SELECT HAKOU_NO,KAITEI_NO,SYUBETU,GOUSYA,KAIHATSU_FUGO,OP_SPEC_CODE,OP_NAME ,TEKIYOU " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_ICHIRAN_OPKOUMOKU OPKOUMOKU WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     HAKOU_NO = @HakouNo AND " _
            & "     KAITEI_NO = @KaiteiNo AND " _
            & "     SYUBETU = @Syubetu AND " _
            & "     (GOUSYA = @Gousya OR GOUSYA = '#' + @Gousya OR GOUSYA = 'W' + @Gousya) AND " _
            & "     (TEKIYOU is not null or TEKIYOU <> '') " _
            & " GROUP BY " _
            & "     HAKOU_NO, " _
            & "     KAITEI_NO, " _
            & "     SYUBETU, " _
            & "     GOUSYA, " _
            & "     KAIHATSU_FUGO, " _
            & "     OP_SPEC_CODE, " _
            & "     OP_NAME, " _
            & "     TEKIYOU " _
            & " ORDER BY  " _
            & "     HAKOU_NO, " _
            & "     KAITEI_NO, " _
            & "     SYUBETU, " _
            & "     GOUSYA, " _
            & "     KAIHATSU_FUGO, " _
            & "     OP_SPEC_CODE, " _
            & "     OP_NAME, " _
            & "     TEKIYOU "

            Dim db As New EBomDbClient
            Dim param As New TSeisakuIchiranOpkoumokuVo
            param.HakouNo = hakouNo
            param.KaiteiNo = kaiteiNo
            param.Syubetu = syubetu
            param.Gousya = gousya

            Return db.QueryForList(Of TSeisakuIchiranOpkoumokuVo)(sql, param)
        End Function

#End Region

#Region "試作特別織込み項目列及びＷＢ特別装備仕様を取得"

        Public Function GetTSeisakuIchiranTokubetu(ByVal hakouNo As String, ByVal kaiteiNo As String) _
                            As List(Of TSeisakuTokubetuOrikomiVo) Implements SeisakuIchiranDao.GetTSeisakuIchiranTokubetu
            Dim sql As String = _
            " SELECT HAKOU_NO,KAITEI_NO,DAI_KBN_NAME,CHU_KBN_NAME,SHO_KBN_NAME,min(TOKUBETU_ORIKOMI_HYOJIJUN_NO) as TOKUBETU_ORIKOMI_HYOJIJUN_NO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_TOKUBETU_ORIKOMI TOKUBETU WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     HAKOU_NO = @HakouNo AND " _
            & "     KAITEI_NO = @KaiteiNo AND " _
            & "     SYUBETU = 'S' " _
            & " GROUP BY  " _
            & "     HAKOU_NO, " _
            & "     KAITEI_NO, " _
            & "     DAI_KBN_NAME, " _
            & "     CHU_KBN_NAME, " _
            & "     SHO_KBN_NAME " _
            & " ORDER BY " _
            & "     HAKOU_NO, " _
            & "     KAITEI_NO, " _
            & "     DAI_KBN_NAME, " _
            & "     CHU_KBN_NAME, " _
            & "     TOKUBETU_ORIKOMI_HYOJIJUN_NO "

            Dim db As New EBomDbClient
            Dim param As New TSeisakuTokubetuOrikomiVo
            param.HakouNo = hakouNo
            param.KaiteiNo = kaiteiNo

            Return db.QueryForList(Of TSeisakuTokubetuOrikomiVo)(sql, param)
        End Function

        Public Function GetTSeisakuIchiranTokubetuWB(ByVal hakouNo As String, ByVal kaiteiNo As String) _
                    As List(Of TSeisakuWbSoubiShiyouVo) Implements SeisakuIchiranDao.GetTSeisakuIchiranTokubetuWB
            Dim sql As String = _
            " SELECT HAKOU_NO,KAITEI_NO,DAI_KBN_NAME,CHU_KBN_NAME,SHO_KBN_NAME,SOUBISHIYOU_HYOUJIJUN_NO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_WB_SOUBI_SHIYOU TOKUBETU WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     HAKOU_NO = @HakouNo AND " _
            & "     KAITEI_NO = @KaiteiNo " _
            & " GROUP BY  " _
            & "     HAKOU_NO, " _
            & "     KAITEI_NO, " _
            & "     DAI_KBN_NAME, " _
            & "     CHU_KBN_NAME, " _
            & "     SHO_KBN_NAME, " _
            & "     SOUBISHIYOU_HYOUJIJUN_NO " _
            & " ORDER BY " _
            & "     HAKOU_NO, " _
            & "     KAITEI_NO, " _
            & "     SOUBISHIYOU_HYOUJIJUN_NO "

            Dim db As New EBomDbClient
            Dim param As New TSeisakuWbSoubiShiyouVo
            param.HakouNo = hakouNo
            param.KaiteiNo = kaiteiNo

            Return db.QueryForList(Of TSeisakuWbSoubiShiyouVo)(sql, param)
        End Function

        Public Function GetTSeisakuIchiranTokubetuGousya(ByVal hakouNo As String, ByVal kaiteiNo As String, _
                                                           ByVal gousya As String) _
                            As List(Of TSeisakuTokubetuOrikomiVo) Implements SeisakuIchiranDao.GetTSeisakuIchiranTokubetuGousya
            Dim sql As String = _
            " SELECT HAKOU_NO,KAITEI_NO,SYUBETU,GOUSYA,DAI_KBN_NAME,CHU_KBN_NAME,SHO_KBN_NAME,TEKIYOU " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_TOKUBETU_ORIKOMI TOKUBETU WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     HAKOU_NO = @HakouNo AND " _
            & "     KAITEI_NO = @KaiteiNo AND " _
            & "     SYUBETU = 'S' AND " _
            & "     (GOUSYA = @Gousya OR GOUSYA = '#' + @Gousya) AND " _
            & "     (TEKIYOU is not null or TEKIYOU <> '') " _
            & " GROUP BY " _
            & "     HAKOU_NO, " _
            & "     KAITEI_NO, " _
            & "     SYUBETU, " _
            & "     GOUSYA, " _
            & "     DAI_KBN_NAME, " _
            & "     CHU_KBN_NAME, " _
            & "     SHO_KBN_NAME, " _
            & "     TEKIYOU " _
            & " ORDER BY  " _
            & "     HAKOU_NO, " _
            & "     KAITEI_NO, " _
            & "     SYUBETU, " _
            & "     GOUSYA, " _
            & "     DAI_KBN_NAME, " _
            & "     CHU_KBN_NAME, " _
            & "     SHO_KBN_NAME, " _
            & "     TEKIYOU "

            Dim db As New EBomDbClient
            Dim param As New TSeisakuTokubetuOrikomiVo
            param.HakouNo = hakouNo
            param.KaiteiNo = kaiteiNo
            param.Gousya = gousya

            Return db.QueryForList(Of TSeisakuTokubetuOrikomiVo)(sql, param)
        End Function

        Public Function GetTSeisakuIchiranTokubetuGousyaWB(ByVal hakouNo As String, ByVal kaiteiNo As String, _
                                                           ByVal gousya As String) _
                            As List(Of TSeisakuWbSoubiShiyouVo) Implements SeisakuIchiranDao.GetTSeisakuIchiranTokubetuGousyaWB
            Dim sql As String = _
            " SELECT HAKOU_NO,KAITEI_NO,GOUSYA,SOUBISHIYOU_HYOUJIJUN_NO,DAI_KBN_NAME,CHU_KBN_NAME,SHO_KBN_NAME,TEKIYOU " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_WB_SOUBI_SHIYOU TOKUBETU WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     HAKOU_NO = @HakouNo AND " _
            & "     KAITEI_NO = @KaiteiNo AND " _
            & "     (GOUSYA = @Gousya OR GOUSYA = 'W' + @Gousya)" _
            & " GROUP BY " _
            & "     HAKOU_NO, " _
            & "     KAITEI_NO, " _
            & "     GOUSYA, " _
            & "     SOUBISHIYOU_HYOUJIJUN_NO, " _
            & "     DAI_KBN_NAME, " _
            & "     CHU_KBN_NAME, " _
            & "     SHO_KBN_NAME, " _
            & "     TEKIYOU " _
            & " ORDER BY  " _
            & "     HAKOU_NO, " _
            & "     KAITEI_NO, " _
            & "     GOUSYA, " _
            & "     SOUBISHIYOU_HYOUJIJUN_NO, " _
            & "     DAI_KBN_NAME, " _
            & "     CHU_KBN_NAME, " _
            & "     SHO_KBN_NAME, " _
            & "     TEKIYOU "

            '& "     GOUSYA = @Gousya AND " _
            '& "     (TEKIYOU is not null or TEKIYOU <> '') " _

            Dim db As New EBomDbClient
            Dim param As New TSeisakuWbSoubiShiyouVo
            param.HakouNo = hakouNo
            param.KaiteiNo = kaiteiNo
            param.Gousya = gousya

            Return db.QueryForList(Of TSeisakuWbSoubiShiyouVo)(sql, param)
        End Function

#End Region


#Region "イベントコードで試作設計ブロック情報から設計担当者情報を取得"
        Public Function GetShisakuSekkeiBlockTanto(ByVal eventCode As String) _
                        As List(Of SendMailUserAddressVo) Implements SeisakuIchiranDao.GetShisakuSekkeiBlockTanto
            Dim sql As String = _
            " SELECT SHISAKU_EVENT_CODE, USER_ID " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK AS BLOCK WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     SHISAKU_EVENT_CODE = @ShisakuEventCode AND " _
            & "     SHISAKU_BLOCK_NO_KAITEI_NO = " _
            & "     (SELECT MAX(CONVERT(INT, COALESCE (SHISAKU_BLOCK_NO_KAITEI_NO, ''))) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
            & "         FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK " _
            & "              WHERE SHISAKU_EVENT_CODE = BLOCK.SHISAKU_EVENT_CODE AND " _
            & "                    SHISAKU_BUKA_CODE = BLOCK.SHISAKU_BUKA_CODE AND " _
            & "                    SHISAKU_BLOCK_NO = BLOCK.SHISAKU_BLOCK_NO) " _
            & " GROUP BY" _
            & "  SHISAKU_EVENT_CODE, USER_ID " _
            & " ORDER BY  " _
            & "  SHISAKU_EVENT_CODE, USER_ID "

            Dim db As New EBomDbClient
            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = eventCode

            Return db.QueryForList(Of SendMailUserAddressVo)(sql, param)
        End Function

#End Region

    End Class

End Namespace

