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
    Public Class TehaichoMenuDaoImpl : Inherits DaoEachFeatureImpl
        Implements TehaichoMenuDao

        ''' <summary>
        ''' 手配訂正通知情報の最終更新日を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="listcode">リストコード</param>
        ''' <param name="listcodeKaiteiNo">リストコード改訂№</param>
        ''' <returns>手配訂正通知情報の最終更新日を取得する</returns>
        ''' <remarks></remarks>
        Public Function FindByTehaiSaishin(ByVal eventcode As String, ByVal listcode As String, ByVal listcodeKaiteiNo As String) As TShisakuTehaiTeiseiKihonVo Implements TehaichoMenuDao.FindByTehaiSaishin
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY UPDATED_DATE + UPDATED_TIME  DESC) AS rownum,*")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_TEISEI_KIHON T ")
                .AppendLine(" WHERE ")
                .AppendFormat(" T.SHISAKU_EVENT_CODE = '{0}' ", eventcode)
                .AppendFormat(" AND T.SHISAKU_LIST_CODE = '{0}' ", listcode)
                .AppendFormat(" AND T.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", listcodeKaiteiNo)
                .AppendLine(" )as rownumbered_result ")
                .AppendLine(" WHERE ")
                .AppendLine(" rownum between 1 and 1 ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuTehaiTeiseiKihonVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 手配基本情報の最終更新日を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="listcode">リストコード</param>
        ''' <param name="listcodeKaiteiNo">リストコード改訂№</param>
        ''' <returns>手配基本情報の最終更新日を取得する</returns>
        ''' <remarks></remarks>
        Public Function FindByKihonSaishin(ByVal eventcode As String, ByVal listcode As String, ByVal listcodeKaiteiNo As String) As TShisakuTehaiKihonVo Implements TehaichoMenuDao.FindByKihonSaishin
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY UPDATED_DATE + UPDATED_TIME  DESC) AS rownum,*")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON K ")
                .AppendLine(" WHERE ")
                .AppendFormat(" K.SHISAKU_EVENT_CODE = '{0}' ", eventcode)
                .AppendFormat(" AND K.SHISAKU_LIST_CODE = '{0}' ", listcode)
                .AppendFormat(" AND K.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", listcodeKaiteiNo)
                .AppendLine(" )as rownumbered_result ")
                .AppendLine(" WHERE ")
                .AppendLine(" rownum between 1 and 1 ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuTehaiKihonVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' リストコードを取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="listcode">リストコード</param>
        ''' <returns>リストコードを取得する</returns>
        ''' <remarks></remarks>
        Public Function FindByListCode(ByVal eventcode As String, ByVal listcode As String) As TShisakuListcodeVo Implements TehaichoMenuDao.FindByListCode
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE L ")
                .AppendLine(" WHERE ")
                .AppendFormat(" L.SHISAKU_EVENT_CODE = '{0}' ", eventcode)
                .AppendFormat(" AND L.SHISAKU_LIST_CODE = '{0}' ", listcode)
                .AppendLine(" AND L.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE   ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = L.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = L.SHISAKU_LIST_CODE ) ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuListcodeVo)(sql.ToString)

        End Function

        ''' <summary>
        ''' 手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>リスト基本情報のリスト</returns>
        ''' <remarks></remarks>
        Public Function FindByListKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiKihonVo) Implements TehaichoMenuDao.FindByListKihon
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK")
                .AppendLine(" WHERE ")
                .AppendFormat(" TK.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND RIREKI <> '*' ")
                .AppendLine(" AND  TK.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON  ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ) ")
                .AppendLine(" ORDER BY SHISAKU_BLOCK_NO, BUHIN_NO_HYOUJI_JUN  ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiKihonVo)(sql.ToString)
        End Function


        ''' <summary>
        ''' 手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>リスト基本情報のリスト</returns>
        ''' <remarks></remarks>
        Public Function FindByListKihonTeisei(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiKihonVo) Implements TehaichoMenuDao.FindByListKihonTeisei
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK")
                .AppendLine(" WHERE ")
                .AppendFormat(" TK.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND  TK.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON  ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ) ")
                .AppendLine(" ORDER BY SHISAKU_BLOCK_NO, BUHIN_NO_HYOUJI_JUN  ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiKihonVo)(sb.ToString)

        End Function

        ''' <summary>
        ''' ユニット区分を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns>ユニット区分</returns>
        ''' <remarks></remarks>
        Public Function FindByUnitKbn(ByVal shisakuEventCode As String) As TShisakuEventVo Implements TehaichoMenuDao.FindByUnitKbn
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
        ''' 旧リストコードを更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="oldListCode">旧リストコード</param>
        ''' <remarks></remarks>
        Public Sub UpdateByOldListCode(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal oldListCode As String, ByVal statusFlag As Boolean) Implements TehaichoMenuDao.UpdateByOldListCode
            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE " _
            & " SET " _
            & " STATUS = @Status, " _
            & " OLD_LIST_CODE = @OldListCode, " _
            & " SHISAKU_DATA_TOUROKUBI = @ShisakuDataTourokubi, " _
            & " SHISAKU_DATA_TOUROKUJIKAN = @ShisakuDataTourokujikan, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_LIST_CODE = @ShisakuListCode " _
            & " AND SHISAKU_LIST_CODE_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE " _
            & " WHERE(SHISAKU_EVENT_CODE = @ShisakuEventCode) " _
            & " AND SHISAKU_LIST_CODE = @ShisakuListCode ) "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuListcodeVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.OldListCode = oldListCode
            param.ShisakuDataTourokubi = Integer.Parse(Replace(aDate.CurrentDateDbFormat, "-", ""))
            param.ShisakuDataTourokujikan = Integer.Parse(Replace(aDate.CurrentTimeDbFormat, ":", ""))

            If statusFlag Then
                param.Status = "62"
            Else
                param.Status = FindByListCode(shisakuEventCode, shisakuListCode).Status
            End If
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)
        End Sub

        ''' <summary>
        ''' ステータスを更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub UpdateByStatus(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal Status As String) Implements TehaichoMenuDao.UpdateByStatus

            Dim UpdateSql As String = ""

            If StringUtil.Equals(Status, "60") Or StringUtil.Equals(Status, "61") Then
                UpdateSql = ""
            ElseIf StringUtil.Equals(Status, "62") Then
                UpdateSql = " SHISAKU_DATA_TOUROKUBI = @ShisakuDataTourokubi, " _
                            & " SHISAKU_DATA_TOUROKUJIKAN = @ShisakuDataTourokujikan, "
            ElseIf StringUtil.Equals(Status, "63") Then
                UpdateSql = " SHISAKU_TENSOUBI = @ShisakuTensoubi, " _
                            & " SHISAKU_TENSOUJIKAN = @ShisakuTensoujikan, "
            End If

            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE " _
            & " SET " _
            & " STATUS = @Status, " _
            & " " + UpdateSql _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_LIST_CODE = @ShisakuListCode " _
            & " AND SHISAKU_LIST_CODE_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_LIST_CODE = @ShisakuListCode ) "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuListcodeVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode

            param.Status = Status

            '新調達'
            If StringUtil.Equals(Status, "63") Then
                param.ShisakuTensoubi = Integer.Parse(Replace(aDate.CurrentDateDbFormat, "-", ""))
                param.ShisakuTensoujikan = Integer.Parse(Replace(aDate.CurrentTimeDbFormat, ":", ""))
            ElseIf StringUtil.IsEmpty(Status) Then
                '再編集(テスト用)'
                'param.Status = "6A"
                param.Status = ""
                param.OldListCode = Nothing
                param.ShisakuDataTourokubi = Nothing
                param.ShisakuDataTourokujikan = Nothing
            End If

            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)
        End Sub

        ''' <summary>
        ''' エラー区分を更新する
        ''' </summary>
        ''' <param name="errorVoList">エラーリスト</param>
        ''' <remarks></remarks>
        Public Sub UpdateByErrorKbn(ByVal errorVoList As List(Of TShisakuTehaiErrorVo)) Implements TehaichoMenuDao.UpdateByErrorKbn

            Dim aDate As New ShisakuDate
            Dim sql As New System.Text.StringBuilder
            Using update As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                update.Open()
                update.BeginTransaction()
                For Each vo As TShisakuTehaiErrorVo In errorVoList

                    If vo Is Nothing Then
                        Continue For
                    End If

                    With sql
                        .Remove(0, .Length)
                        .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ")
                        .AppendLine(" SET ")
                        .AppendFormat(" ERROR_KBN = '{0}' ,", vo.ErrorKbn)
                        .AppendFormat(" UPDATED_USER_ID = '{0}' ,", LoginInfo.Now.UserId)
                        .AppendFormat(" UPDATED_DATE = '{0}' ,", aDate.CurrentDateDbFormat)
                        .AppendFormat(" UPDATED_TIME = '{0}' ", aDate.CurrentTimeDbFormat)
                        .AppendFormat(" WHERE SHISAKU_EVENT_CODE = '{0}' ", vo.ShisakuEventCode)
                        .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", vo.ShisakuListCode)
                        .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", vo.ShisakuBukaCode)
                        .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", vo.ShisakuBlockNo)
                        .AppendFormat(" AND BUHIN_NO_HYOUJI_JUN = {0} ", vo.ShisakuBuhinHyoujiJun)
                        .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                        .AppendLine(" SELECT MAX(SHISAKU_LIST_CODE_KAITEI_NO) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                        .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ")
                        .AppendLine(" WHERE ")
                        .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", vo.ShisakuEventCode)
                        .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", vo.ShisakuListCode)
                        .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", vo.ShisakuBukaCode)
                        .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", vo.ShisakuBlockNo)
                        .AppendFormat(" AND BUHIN_NO_HYOUJI_JUN = {0} ) ", vo.ShisakuBuhinHyoujiJun)
                    End With

                    update.ExecuteNonQuery(sql.ToString)
                Next
                update.Commit()
            End Using

        End Sub

        ''' <summary>
        ''' ベース車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBase(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuEventBaseVo) Implements TehaichoMenuDao.FindByBase

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT TG.SHISAKU_GOUSYA_HYOUJI_JUN AS HYOJIJUN_NO, ")
                .AppendLine(" TG.SHISAKU_GOUSYA AS SHISAKU_GOUSYA, TG.M_NOUNYU_SHIJIBI AS M_NOUNYU_SHIJIBI, TG.T_NOUNYU_SHIJIBI AS T_NOUNYU_SHIJIBI  ")
                .AppendLine(" FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG ")
                .AppendFormat(" WHERE  TG.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" GROUP BY TG.SHISAKU_GOUSYA_HYOUJI_JUN,TG.SHISAKU_GOUSYA, TG.M_NOUNYU_SHIJIBI, TG.T_NOUNYU_SHIJIBI ")
                .AppendLine(" ORDER BY  TG.SHISAKU_GOUSYA_HYOUJI_JUN,TG.SHISAKU_GOUSYA ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventBaseVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' ベース車情報を取得する（試作手配帳情報（号車グループ情報））
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuGousyaGroup">グループ</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBaseToGroup(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuGousyaGroup As String) As List(Of TShisakuEventBaseVo) Implements TehaichoMenuDao.FindByBaseToGroup

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT TG.SHISAKU_GOUSYA_HYOUJI_JUN AS HYOJIJUN_NO, ")
                .AppendLine(" TG.SHISAKU_GOUSYA AS SHISAKU_GOUSYA, TG.M_NOUNYU_SHIJIBI AS M_NOUNYU_SHIJIBI, TG.T_NOUNYU_SHIJIBI AS T_NOUNYU_SHIJIBI  ")
                .AppendLine(" FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG ")
                .AppendLine(" INNER JOIN ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA_GROUP GP ON ")
                .AppendLine(" TG.SHISAKU_EVENT_CODE = GP.SHISAKU_EVENT_CODE AND ")
                .AppendLine(" TG.SHISAKU_LIST_CODE = GP.SHISAKU_LIST_CODE AND ")
                .AppendLine(" TG.SHISAKU_LIST_CODE_KAITEI_NO = GP.SHISAKU_LIST_CODE_KAITEI_NO AND ")
                .AppendLine(" TG.SHISAKU_GOUSYA = GP.SHISAKU_GOUSYA ")
                .AppendFormat(" WHERE  TG.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND GP.SHISAKU_GOUSYA_GROUP = '{0}' ", shisakuGousyaGroup)
                .AppendLine(" GROUP BY TG.SHISAKU_GOUSYA_HYOUJI_JUN,TG.SHISAKU_GOUSYA, TG.M_NOUNYU_SHIJIBI, TG.T_NOUNYU_SHIJIBI ")
                .AppendLine(" ORDER BY  TG.SHISAKU_GOUSYA_HYOUJI_JUN,TG.SHISAKU_GOUSYA ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventBaseVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 全てのベース車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByBaseAll(ByVal shisakuEventCode As String) As List(Of TShisakuEventBaseVo) Implements TehaichoMenuDao.FindByBaseAll
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendLine(" ORDER BY HYOJIJUN_NO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventBaseVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 社員名を返す
        ''' </summary>
        ''' <param name="shainId">社員ID</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByShainName(ByVal shainId As String) As String Implements TehaichoMenuDao.FindByShainName
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0650 " _
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
                & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0650 " _
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
                                      ByVal shisakuBlockNoKaiteiNo As String) As String Implements TehaichoMenuDao.FindByShisakuBlockNo
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT UNIT_KBN ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
                .AppendLine(" WHERE ")
                .AppendFormat("    SHISAKU_EVENT_CODE         = '{0}' AND ", shisakuEventCode)
                .AppendFormat("    SHISAKU_BLOCK_NO           = '{0}' AND ", shisakuBlockNo)
                .AppendFormat("    SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendLine(" GROUP BY UNIT_KBN ")
            End With
            Dim db As New EBomDbClient
            Dim result As TShisakuSekkeiBlockVo
            result = db.QueryForObject(Of TShisakuSekkeiBlockVo)(sql.ToString)

            If result Is Nothing Then
                Return ""
            Else
                Return result.UnitKbn
            End If


        End Function


#Region "エラーチェック"
        ''' <summary>
        ''' 新調達エラー情報のリストを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>該当する新調達エラー情報のリスト</returns>
        ''' <remarks></remarks>
        Public Function FindByShinchotsuError(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TehaiMenuErrorExcelVo) Implements TehaichoMenuDao.FindByShinChoTatsuError
            '後で精査する　樺澤'
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT TE.ERROR_KBN , ")
                .AppendLine(" TE.EC_SHISAKU_BLOCK_NO , ")
                .AppendLine(" TE.SHISAKU_BLOCK_NO , ")
                .AppendLine(" LC.SHISAKU_KOUJI_NO , ")
                .AppendLine(" TK.GYOU_ID , ")
                .AppendLine(" TK.SENYOU_MARK , ")
                .AppendLine(" TK.TEHAI_KIGOU , ")
                .AppendLine(" TE.EC_BUHIN_NO , ")
                .AppendLine(" TE.BUHIN_NO , ")
                .AppendLine(" TE.BUHIN_NO_KBN , ")
                .AppendLine(" TE.EC_BUHIN_NAME , ")
                .AppendLine(" TE.BUHIN_NAME , ")
                .AppendLine(" TE.EC_TOTAL_INSU_SURYO , ")
                .AppendLine(" TE.TOTAL_INSU_SURYO , ")
                .AppendLine(" TE.EC_NOUNYU_SHIJIBI , ")
                .AppendLine(" TE.NOUNYU_SHIJIBI , ")
                .AppendLine(" TE.EC_NOUBA , ")
                .AppendLine(" TE.NOUBA , ")
                .AppendLine(" TE.EC_KYOUKU_SECTION , ")
                .AppendLine(" TE.KYOUKU_SECTION , ")
                .AppendLine(" TE.EC_KOUTAN_SECTION , ")
                .AppendLine(" TE.KOUTAN , ")
                .AppendLine(" TE.EC_TORIHIKISAKI , ")
                .AppendLine(" TE.TORIHIKISAKI , ")
                .AppendLine(" TE.MASTER_KOUTAN , ")
                .AppendLine(" TE.MASTER_TORIHIKISAKI  ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_ERROR TE")
                .AppendLine(" LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK ")
                .AppendLine(" ON TE.SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND TE.SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ")
                .AppendLine(" AND TE.SHISAKU_BLOCK_NO = TK.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND TE.SHISAKU_BUHIN_HYOUJI_JUN = TK.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" AND TK.SHISAKU_LIST_CODE_KAITEI_NO = TE.SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE LC ")
                .AppendLine(" ON LC.SHISAKU_EVENT_CODE = TE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND LC.SHISAKU_LIST_CODE = TE.SHISAKU_LIST_CODE ")
                .AppendLine(" AND LC.SHISAKU_LIST_CODE_KAITEI_NO = TE.SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TE.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND TE.KOKUNAI_GENCHO_FLG = '1' ")
                .AppendLine(" AND NOT TE.ERROR_KBN = '' ")
                .AppendLine(" AND ( NOT TK.TEHAI_KIGOU = 'J' OR NOT TK.TEHAI_KIGOU = 'B' OR TK.TEHAI_KIGOU IS NULL ) ")
                .AppendLine(" AND TE.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_ERROR  ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = TE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TE.SHISAKU_LIST_CODE )")
                .AppendLine(" ORDER BY TE.SHISAKU_BLOCK_NO ")

            End With

            'Dim sql As String = _
            '" SELECT TE.ERROR_KBN , " _
            '& " TE.EC_SHISAKU_BLOCK_NO , " _
            '& " TE.SHISAKU_BLOCK_NO , " _
            '& " LC.SHISAKU_KOUJI_NO , " _
            '& " TK.GYOU_ID , " _
            '& " TK.SENYOU_MARK , " _
            '& " TK.TEHAI_KIGOU , " _
            '& " TE.EC_BUHIN_NO , " _
            '& " TE.BUHIN_NO , " _
            '& " TE.BUHIN_NO_KBN , " _
            '& " TE.EC_BUHIN_NAME , " _
            '& " TE.BUHIN_NAME , " _
            '& " TE.EC_TOTAL_INSU_SURYO , " _
            '& " TE.TOTAL_INSU_SURYO , " _
            '& " TE.EC_NOUNYU_SHIJIBI , " _
            '& " TE.NOUNYU_SHIJIBI , " _
            '& " TE.EC_NOUBA , " _
            '& " TE.NOUBA , " _
            '& " TE.EC_KYOUKU_SECTION , " _
            '& " TE.KYOUKU_SECTION , " _
            '& " TE.EC_KOUTAN_SECTION , " _
            '& " TE.KOUTAN , " _
            '& " TE.EC_TORIHIKISAKI , " _
            '& " TE.TORIHIKISAKI , " _
            '& " TE.MASTER_KOUTAN , " _
            '& " TE.MASTER_TORIHIKISAKI  " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_ERROR TE" _
            '& " LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK " _
            '& " ON TE.SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE " _
            '& " AND TE.SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE " _
            '& " AND TE.SHISAKU_BLOCK_NO = TK.SHISAKU_BLOCK_NO " _
            '& " AND TE.SHISAKU_BUHIN_HYOUJI_JUN = TK.BUHIN_NO_HYOUJI_JUN " _
            '& " AND TK.SHISAKU_LIST_CODE_KAITEI_NO = TE.SHISAKU_LIST_CODE_KAITEI_NO " _
            '& " LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE LC " _
            '& " ON LC.SHISAKU_EVENT_CODE = TE.SHISAKU_EVENT_CODE " _
            '& " AND LC.SHISAKU_LIST_CODE = TE.SHISAKU_LIST_CODE " _
            '& " AND LC.SHISAKU_LIST_CODE_KAITEI_NO = TE.SHISAKU_LIST_CODE_KAITEI_NO " _
            '& " WHERE " _
            '& " TE.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            '& " AND TE.SHISAKU_LIST_CODE = @ShisakuListCode " _
            '& " AND TE.KOKUNAI_GENCHO_FLG = '1' " _
            '& " AND NOT TE.ERROR_KBN = '' " _
            '& " AND ( NOT TK.TEHAI_KIGOU = 'J' OR NOT TK.TEHAI_KIGOU = 'B' OR TK.TEHAI_KIGOU IS NULL ) " _
            '& " AND TE.SHISAKU_LIST_CODE_KAITEI_NO = ( " _
            '& " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_ERROR  " _
            '& " WHERE SHISAKU_EVENT_CODE = TE.SHISAKU_EVENT_CODE " _
            '& " AND SHISAKU_LIST_CODE = TE.SHISAKU_LIST_CODE )" _
            '& " ORDER BY TE.SHISAKU_BLOCK_NO "


            Dim db As New EBomDbClient
            'Dim param As New TShisakuTehaiKihonVo
            'param.ShisakuEventCode = shisakuEventCode
            'param.ShisakuListCode = shisakuListCode

            Return db.QueryForList(Of TehaiMenuErrorExcelVo)(sql.ToString)

        End Function

        ''' <summary>
        ''' 現調品エラー情報のリストを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>該当する現調品エラー情報のリスト</returns>
        ''' <remarks></remarks>
        Public Function FindByGenchoError(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TehaiMenuErrorExcelVo) Implements TehaichoMenuDao.FindByGenchoError
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT TE.ERROR_KBN , ")
                .AppendLine(" TE.EC_SHISAKU_BLOCK_NO , ")
                .AppendLine(" TE.SHISAKU_BLOCK_NO , ")
                .AppendLine(" LC.SHISAKU_KOUJI_NO , ")
                .AppendLine(" TK.GYOU_ID , ")
                .AppendLine(" TK.SENYOU_MARK , ")
                .AppendLine(" TK.TEHAI_KIGOU , ")
                .AppendLine(" TE.EC_BUHIN_NO , ")
                .AppendLine(" TE.BUHIN_NO , ")
                .AppendLine(" TE.BUHIN_NO_KBN , ")
                .AppendLine(" TE.EC_BUHIN_NAME , ")
                .AppendLine(" TE.BUHIN_NAME , ")
                .AppendLine(" TE.EC_TOTAL_INSU_SURYO , ")
                .AppendLine(" TE.TOTAL_INSU_SURYO , ")
                .AppendLine(" TE.EC_NOUNYU_SHIJIBI , ")
                .AppendLine(" TE.NOUNYU_SHIJIBI , ")
                .AppendLine(" TE.EC_NOUBA , ")
                .AppendLine(" TE.NOUBA , ")
                .AppendLine(" TE.EC_KYOUKU_SECTION , ")
                .AppendLine(" TE.KYOUKU_SECTION , ")
                .AppendLine(" TE.EC_KOUTAN_SECTION , ")
                .AppendLine(" TE.KOUTAN , ")
                .AppendLine(" TE.EC_TORIHIKISAKI , ")
                .AppendLine(" TE.TORIHIKISAKI , ")
                .AppendLine(" TE.MASTER_KOUTAN , ")
                .AppendLine(" TE.MASTER_TORIHIKISAKI  ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_ERROR TE")
                .AppendLine(" LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK ")
                .AppendLine(" ON TE.SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND TE.SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ")
                .AppendLine(" AND TE.SHISAKU_BLOCK_NO = TK.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND TE.SHISAKU_BUHIN_HYOUJI_JUN = TK.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" AND ( TK.TEHAI_KIGOU = 'J' OR TK.TEHAI_KIGOU = 'B' ) ")
                .AppendLine(" AND TK.SHISAKU_LIST_CODE_KAITEI_NO = TE.SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE LC ")
                .AppendLine(" ON LC.SHISAKU_EVENT_CODE = TE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND LC.SHISAKU_LIST_CODE = TE.SHISAKU_LIST_CODE ")
                .AppendLine(" AND LC.SHISAKU_LIST_CODE_KAITEI_NO = TE.SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TE.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND TE.KOKUNAI_GENCHO_FLG = '2' ")
                .AppendLine(" AND NOT TE.ERROR_KBN = '' ")
                .AppendLine(" AND TE.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_ERROR  ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = TE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TE.SHISAKU_LIST_CODE )")
                .AppendLine(" ORDER BY TE.SHISAKU_BLOCK_NO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TehaiMenuErrorExcelVo)(sql.ToString)

        End Function

        ''' <summary>
        ''' エラー情報を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByError(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) Implements TehaichoMenuDao.DeleteByError
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" DELETE TE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_ERROR TE ")
                .AppendLine(" WHERE ")
                .AppendLine(" TE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND TE.SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine(" AND TE.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_ERROR  ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = TE.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TE.SHISAKU_LIST_CODE )")
            End With
            'Dim sql As String = _
            '" DELETE TE " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_ERROR TE " _
            '& " WHERE " _
            '& " TE.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            '& " AND TE.SHISAKU_LIST_CODE = @ShisakuListCode " _
            '& " AND TE.SHISAKU_LIST_CODE_KAITEI_NO = ( " _
            '& " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_ERROR  " _
            '& " WHERE SHISAKU_EVENT_CODE = TE.SHISAKU_EVENT_CODE " _
            '& " AND SHISAKU_LIST_CODE = TE.SHISAKU_LIST_CODE )"

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiKihonVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode

            db.Delete(sql.ToString, param)
        End Sub

        ''' <summary>
        ''' エラー情報を追加する
        ''' </summary>
        ''' <param name="errorVoList">エラー情報</param>
        ''' <remarks></remarks>
        Public Sub InsertByError(ByVal errorVoList As List(Of TShisakuTehaiErrorVo)) Implements TehaichoMenuDao.InsertByError

            'Dim sqlList(errorVoList.Count - 1) As String
            Dim aDate As New ShisakuDate
            'Dim sql As New System.Text.StringBuilder
            'Const FMT_TXT As String = ", '{0}'"
            'Const FMT_NUM As String = ", {0}"
            Dim aErrorCheckBi As Integer = Integer.Parse(Replace(aDate.CurrentDateDbFormat, "-", ""))
            Dim aErrorCheckJikan As Integer = Integer.Parse(Replace(aDate.CurrentTimeDbFormat, ":", ""))
            For Each vo As TShisakuTehaiErrorVo In errorVoList

                If vo Is Nothing Then
                    Continue For
                End If

                'With sql
                '    .Remove(0, .Length)
                '    .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_ERROR (")
                '    .AppendLine(" SHISAKU_EVENT_CODE, ")
                '    .AppendLine(" SHISAKU_LIST_CODE, ")
                '    .AppendLine(" SHISAKU_LIST_CODE_KAITEI_NO, ")
                '    .AppendLine(" SHISAKU_BUKA_CODE, ")
                '    .AppendLine(" SHISAKU_BUHIN_HYOUJI_JUN, ")
                '    .AppendLine(" KOKUNAI_GENCHO_FLG, ")
                '    .AppendLine(" ERROR_KBN, ")
                '    .AppendLine(" EC_SHISAKU_BLOCK_NO, ")
                '    .AppendLine(" SHISAKU_BLOCK_NO, ")
                '    .AppendLine(" EC_BUHIN_NO, ")
                '    .AppendLine(" BUHIN_NO, ")
                '    .AppendLine(" BUHIN_NO_KBN, ")
                '    .AppendLine(" EC_BUHIN_NAME, ")
                '    .AppendLine(" BUHIN_NAME, ")
                '    .AppendLine(" EC_TOTAL_INSU_SURYO, ")
                '    .AppendLine(" TOTAL_INSU_SURYO, ")
                '    .AppendLine(" EC_NOUNYU_SHIJIBI, ")
                '    .AppendLine(" NOUNYU_SHIJIBI, ")
                '    .AppendLine(" EC_NOUBA, ")
                '    .AppendLine(" NOUBA, ")
                '    .AppendLine(" EC_KYOUKU_SECTION, ")
                '    .AppendLine(" KYOUKU_SECTION, ")
                '    .AppendLine(" EC_KOUTAN_SECTION, ")
                '    .AppendLine(" KOUTAN, ")
                '    .AppendLine(" EC_TORIHIKISAKI, ")
                '    .AppendLine(" TORIHIKISAKI, ")
                '    .AppendLine(" MASTER_KOUTAN, ")
                '    .AppendLine(" MASTER_TORIHIKISAKI, ")
                '    .AppendLine(" ERROR_CHECK_BI, ")
                '    .AppendLine(" ERROR_CHECK_JIKAN, ")
                '    .AppendLine(" USER_ID, ")
                '    .AppendLine(" CREATED_USER_ID, ")
                '    .AppendLine(" CREATED_DATE, ")
                '    .AppendLine(" CREATED_TIME, ")
                '    .AppendLine(" UPDATED_USER_ID, ")
                '    .AppendLine(" UPDATED_DATE, ")
                '    .AppendLine(" UPDATED_TIME ")
                '    .AppendLine(" ) ")
                '    .AppendLine(" VALUES ( ")
                '    .AppendFormat("'{0}'", vo.ShisakuEventCode)
                '    .AppendFormat(FMT_TXT, vo.ShisakuListCode)
                '    .AppendFormat(FMT_TXT, vo.ShisakuListCodeKaiteiNo)
                '    .AppendFormat(FMT_TXT, vo.ShisakuBukaCode)
                '    .AppendFormat(FMT_NUM, vo.ShisakuBuhinHyoujiJun)
                '    .AppendFormat(FMT_TXT, vo.KokunaiGenchoFlg)
                '    .AppendFormat(FMT_TXT, vo.ErrorKbn)
                '    .AppendFormat(FMT_TXT, vo.EcShisakuBlockNo)
                '    .AppendFormat(FMT_TXT, vo.ShisakuBlockNo)
                '    .AppendFormat(FMT_TXT, vo.EcBuhinNo)
                '    .AppendFormat(FMT_TXT, vo.BuhinNo)
                '    .AppendFormat(FMT_TXT, vo.BuhinNoKbn)
                '    .AppendFormat(FMT_TXT, vo.EcBuhinName)
                '    .AppendFormat(FMT_TXT, vo.BuhinName)
                '    .AppendFormat(FMT_TXT, vo.EcTotalInsuSuryo)
                '    .AppendFormat(FMT_NUM, vo.TotalInsuSuryo)
                '    .AppendFormat(FMT_TXT, vo.EcNounyuShijibi)
                '    .AppendFormat(FMT_NUM, vo.NounyuShijibi)
                '    .AppendFormat(FMT_TXT, vo.EcNouba)
                '    .AppendFormat(FMT_TXT, vo.Nouba)
                '    .AppendFormat(FMT_TXT, vo.EcKyoukuSection)
                '    .AppendFormat(FMT_TXT, vo.KyoukuSection)
                '    .AppendFormat(FMT_TXT, vo.EcKoutanSection)
                '    .AppendFormat(FMT_TXT, vo.Koutan)
                '    .AppendFormat(FMT_TXT, vo.EcTorihikisaki)
                '    .AppendFormat(FMT_TXT, vo.Torihikisaki)
                '    .AppendFormat(FMT_TXT, vo.MasterKoutan)
                '    .AppendFormat(FMT_TXT, vo.MasterTorihikisaki)
                '    .AppendFormat(FMT_NUM, Integer.Parse(Replace(aDate.CurrentDateDbFormat, "-", "")))
                '    .AppendFormat(FMT_NUM, Integer.Parse(Replace(aDate.CurrentTimeDbFormat, ":", "")))
                '    .AppendFormat(FMT_TXT, vo.UserId)
                '    .AppendFormat(FMT_TXT, LoginInfo.Now.UserId)
                '    .AppendFormat(FMT_TXT, aDate.CurrentDateDbFormat)
                '    .AppendFormat(FMT_TXT, aDate.CurrentTimeDbFormat)
                '    .AppendFormat(FMT_TXT, LoginInfo.Now.UserId)
                '    .AppendFormat(FMT_TXT, aDate.CurrentDateDbFormat)
                '    .AppendFormat(FMT_TXT, aDate.CurrentTimeDbFormat)
                '    .AppendLine(" )")
                'End With
                'sqlList(index) = sql.ToString

                vo.ErrorCheckBi = aErrorCheckBi
                vo.ErrorCheckJikan = aErrorCheckJikan
                vo.CreatedUserId = LoginInfo.Now.UserId
                vo.CreatedDate = aDate.CurrentDateDbFormat
                vo.CreatedTime = aDate.CurrentTimeDbFormat
                vo.UpdatedUserId = LoginInfo.Now.UserId
                vo.UpdatedDate = aDate.CurrentDateDbFormat
                vo.UpdatedTime = aDate.CurrentTimeDbFormat
            Next

            Using sqlConn As New System.Data.SqlClient.SqlConnection(NitteiDbComFunc.GetConnectString)
                sqlConn.Open()
                Using trans As SqlClient.SqlTransaction = sqlConn.BeginTransaction
                    Try
                        Using addData As DataTable = NitteiDbComFunc.ConvListToDataTable(errorVoList)
                            Using bulkCopy As System.Data.SqlClient.SqlBulkCopy = New System.Data.SqlClient.SqlBulkCopy(sqlConn, System.Data.SqlClient.SqlBulkCopyOptions.KeepIdentity, trans)
                                NitteiDbComFunc.SetColumnMappings(bulkCopy, addData)
                                bulkCopy.BulkCopyTimeout = 0  ' in seconds
                                bulkCopy.DestinationTableName = "dbo.T_SHISAKU_TEHAI_ERROR"
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



            'Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            '    insert.Open()
            '    insert.BeginTransaction()
            '    For index As Integer = 0 To errorVoList.Count - 1
            '        'Try
            '        If Not errorVoList(index) Is Nothing Then
            '            insert.ExecuteNonQuery(sqlList(index))
            '        End If
            '        'Catch ex As SqlClient.SqlException
            '        '    MsgBox(ex.Message)
            '        '    Continue For
            '        'End Try
            '    Next
            '    insert.Commit()
            'End Using

            '& " VALUES ( " _
            '& " @ShisakuEventCode, " _
            '& " @ShisakuListCode, " _
            '& " @ShisakuListCodeKaiteiNo, " _
            '& " @ShisakuBukaCode, " _
            '& " @ShisakuBuhinHyoujiJun, " _
            '& " @KokunaiGenchoFlg, " _
            '& " @ErrorKbn, " _
            '& " @EcShisakuBlockNo, " _
            '& " @ShisakuBlockNo, " _
            '& " @EcBuhinNo, " _
            '& " @BuhinNo, " _
            '& " @BuhinNoKbn, " _
            '& " @EcBuhinName, " _
            '& " @BuhinName, " _
            '& " @EcTotalInsuSuryo, " _
            '& " @TotalInsuSuryo, " _
            '& " @EcNounyuShijibi, " _
            '& " @NounyuShijibi, " _
            '& " @EcNouba, " _
            '& " @Nouba, " _
            '& " @EcKyoukuSection, " _
            '& " @KyoukuSection, " _
            '& " @EcKoutanSection, " _
            '& " @Koutan, " _
            '& " @EcTorihikisaki, " _
            '& " @Torihikisaki, " _
            '& " @MasterKoutan, " _
            '& " @MasterTorihikisaki, " _
            '& " @ErrorCheckBi, " _
            '& " @ErrorCheckJikan, " _
            '& " @UserId, " _
            '& " @CreatedUserId, " _
            '& " @CreatedDate, " _
            '& " @CreatedTime, " _
            '& " @UpdatedUserId, " _
            '& " @UpdatedDate, " _
            '& " @UpdatedTime " _
            '& " ) "

        End Sub

        ''' <summary>
        ''' ３か月インフォメーションを取得する
        ''' </summary>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当する３ヶ月インフォメーション情報</returns>
        ''' <remarks></remarks>
        Public Function FindBy3Month(ByVal seihinKbn As String, ByVal buhinNo As String) As AsKPSM10PVo Implements TehaichoMenuDao.FindBy3Month
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_KPSM10P ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SNKM = '{0}' ", seihinKbn)
                .AppendFormat(" AND BUBA_15 = '{0}' ", buhinNo)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of AsKPSM10PVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 海外生産情報を取得する
        ''' </summary>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当する海外生産情報</returns>
        ''' <remarks></remarks>
        Public Function FindByForign(ByVal seihinKbn As String, ByVal buhinNo As String) As AsGKPSM10PVo Implements TehaichoMenuDao.FindByForign
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_GKPSM10P ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SNKM = '{0}' ", seihinKbn)
                .AppendFormat(" AND BUBA_15 = '{0}' ", buhinNo)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of AsGKPSM10PVo)(sql.ToString)
        End Function


        Private _hshFindByAsPAPF14 As New Hashtable
        ''' <summary>
        ''' 新調達取引先情報の取得
        ''' </summary>
        ''' <param name="torihikisakiCode">取引先コード</param>
        ''' <returns>該当する新調達取引先情報</returns>
        ''' <remarks></remarks>
        Public Function FindByAsPAPF14(ByVal torihikisakiCode As String) As AsPAPF14Vo Implements TehaichoMenuDao.FindByAsPAPF14

            If Not _hshFindByAsPAPF14.Contains(torihikisakiCode) Then
                Dim sql As New System.Text.StringBuilder
                With sql
                    .AppendLine(" SELECT * ")
                    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_PAPF14")
                    .AppendFormat(" WHERE TORICD = '{0}'", torihikisakiCode)
                End With
                Dim db As New EBomDbClient
                Dim vo As AsPAPF14Vo = db.QueryForObject(Of AsPAPF14Vo)(sql.ToString)
                _hshFindByAsPAPF14.Add(torihikisakiCode, vo)
            End If
            Return _hshFindByAsPAPF14.Item(torihikisakiCode)

        End Function

        ''' <summary>
        ''' パーツプライリスト情報の取得
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当する新調達取引先情報</returns>
        ''' <remarks></remarks>
        Public Function FindByAsPARTSP(ByVal buhinNo As String) As AsPARTSPVo Implements TehaichoMenuDao.FindByAsPARTSP
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_PARTSP")
                .AppendFormat(" WHERE BUBA_13 = '{0}'", buhinNo)

            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of AsPARTSPVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 最新のブロックNoのリストを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByShisakuBlockList(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuSekkeiBlockVo) Implements TehaichoMenuDao.FindByShisakuBlockList
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON K ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX (SHISAKU_LIST_CODE_KAITEI_NO) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON   ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = K.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = K.SHISAKU_LIST_CODE ) ")
                .AppendLine(" GROUP BY SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO ")
                .AppendLine(" ORDER BY SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>手配基本情報のリスト</returns>
        ''' <remarks></remarks>
        Public Function FindByListKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As List(Of TShisakuTehaiKihonVo) Implements TehaichoMenuDao.FindByListKihon

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK")
                .AppendLine(" WHERE ")
                .AppendFormat(" TK.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE = '{0}'", shisakuListCode)
                .AppendFormat(" AND TK.SHISAKU_BUKA_CODE = '{0}'", shisakuBukaCode)
                .AppendFormat(" AND TK.SHISAKU_BLOCK_NO = '{0}'", shisakuBlockNo)
                .AppendLine(" AND RIREKI <> '*' ")
                .AppendLine(" AND  TK.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX(SHISAKU_LIST_CODE_KAITEI_NO) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON  ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ) ")
                .AppendLine(" ORDER BY SHISAKU_BLOCK_NO, BUHIN_NO_HYOUJI_JUN  ")
            End With

            Dim db As New EBomDbClient

            Return db.QueryForList(Of TShisakuTehaiKihonVo)(sb.ToString)
        End Function

#End Region

#Region "発注データ登録"

        ''' <summary>
        ''' 発注用データを登録する
        ''' </summary>
        ''' <param name="tehaiKihonVo">手配基本情報</param>
        ''' <param name="ketugouNo">結合No</param>
        ''' <remarks></remarks>
        Public Sub UpdateKetsugoNo(ByVal tehaiKihonVo As TShisakuTehaiKihonVo, ByVal ketugouNo As String) Implements TehaichoMenuDao.UpdateKetsugoNo
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ")
                .AppendLine(" SET ")
                .AppendLine(" KETUGOU_NO = @KetugouNo ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode  ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
            End With
            'Dim sql As String = _
            '" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON " _
            '& " SET " _
            '& " KETUGOU_NO = @KetugouNo " _
            '& " WHERE " _
            '& " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            '& " AND SHISAKU_LIST_CODE = @ShisakuListCode  " _
            '& " AND SHISAKU_BUKA_CODE = @ShisakuBukaCode " _
            '& " AND SHISAKU_BLOCK_NO = @ShisakuBlockNo " _
            '& " AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo " _
            '& " AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun "

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiKihonVo
            param.ShisakuEventCode = tehaiKihonVo.ShisakuEventCode
            param.ShisakuListCode = tehaiKihonVo.ShisakuListCode
            param.ShisakuBukaCode = tehaiKihonVo.ShisakuBukaCode
            param.ShisakuBlockNo = tehaiKihonVo.ShisakuBlockNo
            param.ShisakuListCodeKaiteiNo = tehaiKihonVo.ShisakuListCodeKaiteiNo
            param.BuhinNoHyoujiJun = tehaiKihonVo.BuhinNoHyoujiJun
            param.KetugouNo = ketugouNo

            db.Update(sql.ToString, param)
        End Sub

        ''' <summary>
        ''' 発注用データ用手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Function FindByKetsugoNo(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiKihonVo) Implements TehaichoMenuDao.FindByKetsugoNo
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TK.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND NOT TK.RIREKI = '*' ")
                .AppendLine(" AND NOT TK.ERROR_KBN = 'E' ")
                .AppendLine(" AND NOT TK.TEHAI_KIGOU = 'F' ")
                .AppendLine(" AND TK.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON  ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ) ")
                .AppendLine(" ORDER BY SHISAKU_BLOCK_NO, BUHIN_NO_HYOUJI_JUN ,SORT_JUN ")
            End With
            'Dim sql As String = _
            '" SELECT * " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK " _
            '& " WHERE " _
            '& " TK.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            '& " AND TK.SHISAKU_LIST_CODE = @ShisakuListCode " _
            '& " AND NOT TK.RIREKI = '*' " _
            '& " AND NOT TK.ERROR_KBN = 'E' " _
            '& " AND NOT TK.TEHAI_KIGOU = 'F' " _
            '& " AND TK.SHISAKU_LIST_CODE_KAITEI_NO = ( " _
            '& " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON  " _
            '& " WHERE SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE " _
            '& " AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ) " _
            '& " ORDER BY SHISAKU_BLOCK_NO, BUHIN_NO_HYOUJI_JUN ,SORT_JUN "

            Dim db As New EBomDbClient
            'Dim param As New TShisakuTehaiKihonVo
            'param.ShisakuEventCode = shisakuEventCode
            'param.ShisakuListCode = shisakuListCode

            Return db.QueryForList(Of TShisakuTehaiKihonVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 発注用データ用結合Noを取得する
        ''' </summary>
        ''' <param name="ketugouHeader">結合Noの頭</param>
        ''' <remarks></remarks>
        Function FindByMaxKetsugoNo(ByVal ketugouHeader As String) As TShisakuTehaiKihonVo Implements TehaichoMenuDao.FindByMaxKetsugoNo
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine("SELECT MAX(KETUGOU_NO) AS KETUGOU_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK ")
                .AppendLine(" WHERE ")
                .AppendFormat("TK.KETUGOU_NO LIKE '{0}%'", ketugouHeader)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuTehaiKihonVo)(sql.ToString)
        End Function


#End Region

#Region "新調達への転送"

        ''' <summary>
        ''' 履歴を更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub UpdateByRireki(ByVal shisakuEventCode As String, _
                               ByVal shisakuListCode As String) Implements TehaichoMenuDao.UpdateByRireki
            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON " _
            & " SET " _
            & " RIREKI = @Rireki, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_LIST_CODE = @ShisakuListCode " _
            & " AND ( RIREKI IS NULL OR RIREKI = '') " _
            & " AND NOT ERROR_KBN = 'E' " _
            & " AND NOT TEHAI_KIGOU = 'F' " _
            & " AND NOT SHUKEI_CODE = 'X'  " _
            & " AND SHISAKU_LIST_CODE_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON " _
            & " WHERE(SHISAKU_EVENT_CODE = @ShisakuEventCode) " _
            & " AND SHISAKU_LIST_CODE = @ShisakuListCode ) "


            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuListcodeVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode
            param.Rireki = "*"

            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)

        End Sub

        ''' <summary>
        ''' 指定の改訂Noの手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="kaiteiNo">リストコード改訂№</param>
        ''' <param name="unitKbn">ユニット区分</param>
        ''' <returns>該当する手配基本情報</returns>
        ''' <remarks></remarks>
        Public Function FindByTehaiKihonKaiteiNo(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal kaiteiNo As String, ByVal unitKbn As String) As List(Of TShisakuTehaiKihonVoHelper) Implements TehaichoMenuDao.FindByTehaiKihonKaiteiNo

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT TK.*, TG.SHISAKU_GOUSYA, TG.INSU_SURYO, TG.SHISAKU_GOUSYA_HYOUJI_JUN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK ")
                ' 20150216 UnitKbnがMの場合はLeftJoinにする。（員数が無いので）
                If Not String.Equals(unitKbn, "M") Then
                    .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG ")
                Else
                    .AppendLine(" LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG ")
                End If
                .AppendLine(" ON TG.SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND TG.SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ")
                .AppendLine(" AND TG.SHISAKU_LIST_CODE_KAITEI_NO = TK.SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" AND TG.SHISAKU_BUKA_CODE = TK.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND TG.SHISAKU_BLOCK_NO = TK.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND TG.BUHIN_NO_HYOUJI_JUN = TK.BUHIN_NO_HYOUJI_JUN ")
                ' 20150216 UnitKbnがMの場合は員数チェックが不要
                If Not String.Equals(unitKbn, "M") Then
                    .AppendLine(" AND TG.INSU_SURYO <> 0 ")
                End If
                .AppendLine(" WHERE ")
                .AppendFormat(" TK.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE = '{0}'", shisakuListCode)
                .AppendFormat(" AND  TK.SHISAKU_LIST_CODE_KAITEI_NO = '{0}'", kaiteiNo)
                .AppendLine(" ORDER BY TK.SHISAKU_BLOCK_NO, TK.BUHIN_NO_HYOUJI_JUN, TG.SHISAKU_GOUSYA_HYOUJI_JUN ")
                .AppendLine()

            End With

            Dim db As New EBomDbClient

            Return db.QueryForList(Of TShisakuTehaiKihonVoHelper)(sb.ToString)

        End Function

        ''' <summary>
        ''' 指定の改訂Noの手配基本情報を取得する（試作手配帳情報（号車グループ情報））
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="kaiteiNo">リストコード改訂№</param>
        ''' <param name="unitKbn">ユニット区分</param>
        ''' <param name="shisakuGousyaGroup">グループ</param>
        ''' <returns>該当する手配基本情報</returns>
        ''' <remarks></remarks>
        Public Function FindByTehaiKihonKaiteiNoToGroup(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal kaiteiNo As String, ByVal unitKbn As String, ByVal shisakuGousyaGroup As String) As List(Of TShisakuTehaiKihonVoHelper) Implements TehaichoMenuDao.FindByTehaiKihonKaiteiNoToGroup

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT TK.*, TG.SHISAKU_GOUSYA, TG.INSU_SURYO, TG.SHISAKU_GOUSYA_HYOUJI_JUN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG ")
                .AppendLine(" ON TG.SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND TG.SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ")
                .AppendLine(" AND TG.SHISAKU_LIST_CODE_KAITEI_NO = TK.SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" AND TG.SHISAKU_BUKA_CODE = TK.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND TG.SHISAKU_BLOCK_NO = TK.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND TG.BUHIN_NO_HYOUJI_JUN = TK.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" INNER JOIN ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA_GROUP GP ON ")
                .AppendLine(" TG.SHISAKU_EVENT_CODE = GP.SHISAKU_EVENT_CODE AND ")
                .AppendLine(" TG.SHISAKU_LIST_CODE = GP.SHISAKU_LIST_CODE AND ")
                .AppendLine(" TG.SHISAKU_LIST_CODE_KAITEI_NO = GP.SHISAKU_LIST_CODE_KAITEI_NO AND ")
                .AppendLine(" TG.SHISAKU_GOUSYA = GP.SHISAKU_GOUSYA ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TK.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE = '{0}'", shisakuListCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE_KAITEI_NO = '{0}'", kaiteiNo)
                .AppendFormat(" AND GP.SHISAKU_GOUSYA_GROUP = '{0}' ", shisakuGousyaGroup)
                .AppendLine(" ORDER BY TK.SHISAKU_BLOCK_NO, TK.BUHIN_NO_HYOUJI_JUN, TG.SHISAKU_GOUSYA_HYOUJI_JUN ")
                .AppendLine()

            End With

            Dim db As New EBomDbClient

            Return db.QueryForList(Of TShisakuTehaiKihonVoHelper)(sb.ToString)

        End Function

        ''' <summary>
        ''' 指定の改訂Noの手配号車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>該当する手配号車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByTehaiGousyaKaiteiNo(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal kaiteiNo As String) As List(Of TShisakuTehaiGousyaVo) Implements TehaichoMenuDao.FindByTehaiGousyaKaiteiNo
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG")
                .AppendLine(" WHERE ")
                .AppendFormat(" TG.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND  TG.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", kaiteiNo)
                .AppendLine(" ORDER BY SHISAKU_BLOCK_NO, BUHIN_NO_HYOUJI_JUN  ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiGousyaVo)(sb.ToString)

        End Function

        ''' <summary>
        ''' 指定の改訂Noの手配号車情報を取得する（試作手配帳情報（号車グループ情報））
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="ShisakuGousyaGroup">グループ</param>
        ''' <returns>該当する手配号車情報</returns>
        ''' <remarks></remarks>
        Public Function FindByTehaiGousyaKaiteiNo(ByVal shisakuEventCode As String, _
                                                  ByVal shisakuListCode As String, _
                                                  ByVal kaiteiNo As String, _
                                                  ByVal shisakuGousyaGroup As String) As List(Of TShisakuTehaiGousyaVo) Implements TehaichoMenuDao.FindByTehaiGousyaKaiteiNoToGroup
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG")
                .AppendLine(" INNER JOIN ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA_GROUP GP ON ")
                .AppendLine(" TG.SHISAKU_EVENT_CODE = GP.SHISAKU_EVENT_CODE AND ")
                .AppendLine(" TG.SHISAKU_LIST_CODE = GP.SHISAKU_LIST_CODE AND ")
                .AppendLine(" TG.SHISAKU_LIST_CODE_KAITEI_NO = GP.SHISAKU_LIST_CODE_KAITEI_NO AND ")
                .AppendLine(" TG.SHISAKU_GOUSYA = GP.SHISAKU_GOUSYA ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TG.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", kaiteiNo)
                .AppendFormat(" AND GP.SHISAKU_GOUSYA_GROUP = '{0}' ", shisakuGousyaGroup)
                .AppendLine(" ORDER BY SHISAKU_BLOCK_NO, BUHIN_NO_HYOUJI_JUN  ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiGousyaVo)(sb.ToString)

        End Function

        ''' <summary>
        ''' 指定の改訂Noのリストコードを取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="listcode">リストコード</param>
        ''' <returns>リストコードを取得する</returns>
        ''' <remarks></remarks>
        Public Function FindByListCodeKaiteiNo(ByVal eventcode As String, ByVal listcode As String, ByVal kaiteiNo As String) As TShisakuListcodeVo Implements TehaichoMenuDao.FindByListCodeKaiteiNo
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE L ")
                .AppendLine(" WHERE ")
                .AppendFormat(" L.SHISAKU_EVENT_CODE = '{0}' ", eventcode)
                .AppendFormat(" AND L.SHISAKU_LIST_CODE = '{0}' ", listcode)
                .AppendFormat(" AND L.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", kaiteiNo)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuListcodeVo)(sql.ToString)
        End Function

#End Region

#Region "改訂抽出"

        ''' <summary>
        ''' 試作部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns>試作部品編集情報リスト</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEdit(ByVal shisakuEventCode As String) As List(Of TShisakuBuhinEditVo) Implements TehaichoMenuDao.FindByBuhinEdit
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 試作部品編集情報(ベース)を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品No表示順</param>
        ''' <returns>試作部品編集情報(ベース)リスト</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditBase(ByVal shisakuEventCode As String, _
                                            ByVal shisakuBukaCode As String, _
                                            ByVal shisakuBlockNo As String, _
                                            ByVal shisakuBlockNoKaiteiNo As String, _
                                            ByVal buhinNoHyoujiJun As String) As List(Of TShisakuBuhinEditBaseVo) Implements TehaichoMenuDao.FindByBuhinEditBase
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_BASE ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendFormat(" AND BUHIN_NO_HYOUJIJUN = {0} ", buhinNoHyoujiJun)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditBaseVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 試作部品編集・INSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">ブロックNo表示順</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns>試作部品編集・INSTL情報リスト</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditInstl(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal shisakuBlockNoKaiteiNo As String, _
                                      ByVal buhinNoHyoujiJun As String) As List(Of TShisakuBuhinEditInstlVo) Implements TehaichoMenuDao.FindByBuhinEditInstl
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendFormat(" AND BUHIN_NO_HYOUJI_JUN = {0} ", buhinNoHyoujiJun)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditInstlVo)(sql.ToString)

        End Function

        ''' <summary>
        ''' 試作部品編集・INSTL情報(ベース)を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="InstlHinbanHyoujiJun">INSTL品番表示順</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns>試作部品編集・INSTL情報(ベース)リスト</returns>
        ''' <remarks></remarks>
        Public Function FindByBuhinEditInstlBase(ByVal shisakuEventCode As String, _
                                                 ByVal shisakuBukaCode As String, _
                                                 ByVal shisakuBlockNo As String, _
                                                 ByVal shisakuBlockNoKaiteiNo As String, _
                                                 ByVal InstlHinbanHyoujiJun As String) As List(Of TShisakuBuhinEditInstlBaseVo) Implements TehaichoMenuDao.FindByBuhinEditInstlBase
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendFormat(" AND INSTL_HINBAN_HYOUJI_JUN = {0} ", InstlHinbanHyoujiJun)
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditInstlBaseVo)(sql.ToString)

        End Function

        Public Function FindByBuhinEditIkanshaKaishu(ByVal shisakuEventCode As String, ByVal blockNo As String, ByVal shisakuListCode As String) As List(Of TShisakuBuhinEditVoHelperExcel) Implements TehaichoMenuDao.FindByBuhinEditIkanshaKaishu
            Dim db As New EBomDbClient
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT TG.SHISAKU_GOUSYA AS SHISAKU_GOUSYA, TG.SHISAKU_GOUSYA_HYOUJI_JUN AS HYOJIJUN_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TG.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE = '{0}'", shisakuListCode)
                .AppendLine(" GROUP BY TG.SHISAKU_GOUSYA,  TG.SHISAKU_GOUSYA_HYOUJI_JUN ")
                .AppendLine(" ORDER BY  TG.SHISAKU_GOUSYA_HYOUJI_JUN,TG.SHISAKU_GOUSYA ")
            End With


            'Dim sql1 As String = _
            '    " SELECT TG.SHISAKU_GOUSYA AS SHISAKU_GOUSYA, TG.SHISAKU_GOUSYA_HYOUJI_JUN AS HYOJIJUN_NO " _
            '    & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG " _
            '    & " WHERE " _
            '    & " TG.SHISAKU_EVENT_CODE = @ShisakuEventCode" _
            '    & " AND TG.SHISAKU_LIST_CODE = @ShisakuListCode" _
            '    & " GROUP BY TG.SHISAKU_GOUSYA,  TG.SHISAKU_GOUSYA_HYOUJI_JUN " _
            '    & " ORDER BY  TG.SHISAKU_GOUSYA_HYOUJI_JUN,TG.SHISAKU_GOUSYA "

            Dim BaseList As List(Of TShisakuEventBaseVo) = db.QueryForList(Of TShisakuEventBaseVo)(sql.ToString)
            Dim rtnVal As New List(Of TShisakuBuhinEditVoHelperExcel)
            For Each baseVo As TShisakuEventBaseVo In BaseList
                With sql
                    .Remove(0, .Length)
                    .AppendLine(" SELECT ")
                    .AppendLine(" BE.*, ")
                    .AppendLine(" BEI.INSTL_HINBAN_HYOUJI_JUN, ")
                    .AppendLine(" BEI.INSU_SURYO, ")
                    .AppendLine(" B.HYOJIJUN_NO, ")
                    .AppendLine(" SB.USER_ID, ")
                    .AppendLine(" SB.TEL_NO, ")
                    .AppendLine(" SBI.BASE_INSTL_FLG ")
                    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) ")
                    .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI ")
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
                    .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                    .AppendLine(" AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN ")
                    .AppendLine(" AND NOT ( SBI.INSU_SURYO IS NULL OR SBI.INSU_SURYO ='' ) ")
                    .AppendFormat(" AND SBI.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                    .AppendFormat(" AND SBI.SHISAKU_GOUSYA = '{0}'", baseVo.ShisakuGousya)
                    .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")
                    .AppendLine(" AND SBI.INSU_SURYO > 0 ")
                    .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B ")
                    .AppendLine(" ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                    .AppendLine(" AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA ")
                    .AppendLine(" WHERE ")
                    .AppendFormat(" BE.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                    .AppendFormat(" AND BE.SHISAKU_BLOCK_NO = '{0}'", blockNo)
                    .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = '000' ")
                    .AppendLine(" AND SBI.BASE_INSTL_FLG = '1' ")
                    .AppendLine(" ORDER BY BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")
                End With
                Dim resultVoList = db.QueryForList(Of TShisakuBuhinEditVoHelperExcel)(sql.ToString)
                For Each resultVo As TShisakuBuhinEditVoHelperExcel In resultVoList
                    resultVo.ShisakuGousyaHyoujiJun = baseVo.HyojijunNo
                Next
                rtnVal.AddRange(resultVoList)
            Next

            rtnVal.Sort(AddressOf TShisakuBuhinEditVoHelperExcel.Comparison)
            Return rtnVal
        End Function

        ''' <summary>
        ''' 試作設計ブロック・INSTL情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns>試作設計ブロック・INSTL情報(ベース)リスト</returns>
        ''' <remarks></remarks>
        Public Function FindBySekkeiBlockInstl(ByVal shisakuEventCode As String, _
                                          ByVal shisakuBukaCode As String, _
                                          ByVal shisakuBlockNo As String, _
                                          ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuSekkeiBlockInstlVo) Implements TehaichoMenuDao.FindBySekkeiBlockInstl
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_BASE ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", shisakuBukaCode)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND SHISAKU_BLOCK_NO_KAITEI_NO = '{0}' ", shisakuBlockNoKaiteiNo)
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 部品編集改訂情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>部品編集改訂情報</returns>
        ''' <remarks></remarks>
        Public Function FindByKaiteiBuhinEdit(ByVal shisakuEventCode As String, _
                                              ByVal shisakuListCode As String, _
                                              ByVal shisakuListCodeKaiteiNo As String) As List(Of TShisakuBuhinEditKaiteiVoHelper) Implements TehaichoMenuDao.FindByKaiteiBuhinEdit
            Dim sql As New StringBuilder
            With sql
                .AppendLine(" SELECT BEK.*, BEGK.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI BEK ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI BEGK ")
                .AppendLine(" ON BEGK.SHISAKU_EVENT_CODE = BEK.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEGK.SHISAKU_LIST_CODE = BEK.SHISAKU_LIST_CODE ")
                .AppendLine(" AND BEGK.SHISAKU_LIST_CODE_KAITEI_NO = BEK.SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" AND BEGK.SHISAKU_BUKA_CODE = BEK.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEGK.SHISAKU_BLOCK_NO = BEK.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEGK.SHISAKU_BLOCK_NO_KAITEI_NO = BEK.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEGK.BUHIN_NO_HYOUJI_JUN = BEK.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" AND BEGK.FLAG = BEK.FLAG ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BEK.SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND BEK.SHISAKU_LIST_CODE = '{0}'", shisakuListCode)
                .AppendFormat(" AND BEK.SHISAKU_LIST_CODE_KAITEI_NO = '{0}'", shisakuListCodeKaiteiNo)
                .AppendLine(" ORDER BY BEK.SHISAKU_BLOCK_NO, BEK.LEVEL, BEK.BUHIN_NO_HYOUJI_JUN, BEK.FLAG, BEGK.SHISAKU_GOUSYA_HYOUJI_JUN ")
            End With

            Dim db As New EBomDbClient

            Return db.QueryForList(Of TShisakuBuhinEditKaiteiVoHelper)(sql.ToString)
        End Function

        'Public Function FindByKaiteiBuhinEdit(ByVal shisakuEventCode As String, _
        '                          ByVal shisakuListCode As String) As List(Of TShisakuBuhinEditKaiteiVo) Implements TehaichoMenuDao.FindByKaiteiBuhinEdit
        '    Dim sql As String = _
        '    " SELECT * " _
        '    & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI BEK " _
        '    & " WHERE " _
        '    & " BEK.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
        '    & " AND BEK.SHISAKU_LIST_CODE = @ShisakuListCode " _
        '    & " AND BEK.SHISAKU_LIST_CODE_KAITEI_NO = ( " _
        '    & " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO " _
        '    & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI " _
        '    & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
        '    & " AND SHISAKU_LIST_CODE = @ShisakuListCode ) "

        '    Dim db As New EBomDbClient
        '    Dim param As New TShisakuBuhinEditKaiteiVo
        '    param.ShisakuEventCode = shisakuEventCode
        '    param.ShisakuListCode = shisakuListCode

        '    Return db.QueryForList(Of TShisakuBuhinEditKaiteiVo)(sql, param)
        'End Function

        ''' <summary>
        ''' 部品編集号車改訂情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>部品編集号車改訂情報</returns>
        ''' <remarks></remarks>
        Public Function FindByKaiteiBuhinEditGousya(ByVal shisakuEventCode As String, _
                                           ByVal shisakuListCode As String) As List(Of TShisakuBuhinEditGousyaKaiteiVo) Implements TehaichoMenuDao.FindByKaiteiBuhinEditGousya
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI BEG ")
                .AppendLine(" WHERE ")
                .AppendFormat(" BEG.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND BEG.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND BEG.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI ")
                .AppendFormat(" WHERE SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ) ", shisakuListCode)
                .AppendLine(" ORDER BY SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_GOUSYA_HYOUJI_JUN ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditGousyaKaiteiVo)(sql.ToString)
        End Function


#End Region

#Region "再編集"

        ''' <summary>
        ''' 手配基本情報を編集前に戻す
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub RisetByTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) Implements TehaichoMenuDao.RisetByTehaiKihon
            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON " _
            & " SET " _
            & " KETUGOU_NO = '' , " _
            & " ERROR_KBN = '' ," _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_LIST_CODE = @ShisakuListCode " _
            & " AND SHISAKU_LIST_CODE_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND SHISAKU_LIST_CODE = @ShisakuListCode ) "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuListcodeVo

            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode

            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)

        End Sub

#End Region

    End Class
End Namespace