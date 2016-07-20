Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports System.Text

Namespace ShisakuBuhinKaiteiBlock.Dao

    Public Class ShisakuBuhinKaiteiBlockTsuchiDaoImpl : Inherits DaoEachFeatureImpl
        Implements IShisakuBuhinKaiteiBlockTsuchiDao

#Region "試作部品表編集・改定編集（ブロック）改訂通知一覧情報を取得する"

        ''' <summary>
        ''' 試作部品表編集・改定編集（ブロック）改訂通知一覧情報を取得する
        ''' </summary>
        ''' <param name="MailTsuchiHi1">メール通知日１</param>
        ''' <param name="MailTsuchiHi2">メール通知日２</param>
        ''' <returns>ブロック一覧</returns>
        ''' <remarks></remarks>

        Public Function GetBlockSpreadList(ByVal MailTsuchiHi1 As Integer, ByVal MailTsuchiHi2 As Integer _
                                        ) As List(Of TShisakuSekkeiBlockTsuchiVo) Implements Dao.IShisakuBuhinKaiteiBlockTsuchiDao.GetBlockTsuchiList
            Dim sql As String = _
                "SELECT " _
            & "   COALESCE(SHISAKU_EVENT_CODE,'') AS SHISAKU_EVENT_CODE , " _
            & "   COALESCE(SHISAKU_BUKA_CODE,'') AS SHISAKU_BUKA_CODE , " _
            & "   COALESCE(MAX(R.KA_RYAKU_NAME),SHISAKU_BUKA_CODE) AS KA_RYAKU_NAME , " _
            & "   COALESCE(SHISAKU_BLOCK_NO,'') AS SHISAKU_BLOCK_NO , " _
            & "   COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'') AS SHISAKU_BLOCK_NO_KAITEI_NO , " _
            & "   COALESCE(BLOCK_FUYOU,'') AS BLOCK_FUYOU , " _
            & "   COALESCE(JYOUTAI,'') AS JYOUTAI, " _
            & "   COALESCE(UNIT_KBN,'') AS UNIT_KBN , " _
            & "   COALESCE(SHISAKU_BLOCK_NAME,'') AS SHISAKU_BLOCK_NAME , " _
            & "   COALESCE(SYAIN.SHAIN_NAME ,'') AS SYAIN_NAME , " _
            & "   COALESCE(TEL_NO,'') AS TEL_NO , " _
            & "   COALESCE(SAISYU_KOUSHINBI,'') AS SAISYU_KOUSHINBI, " _
            & "   COALESCE(SAISYU_KOUSHINJIKAN,'') AS SAISYU_KOUSHINJIKAN, " _
            & "   COALESCE(TANTO_SYOUNIN_JYOUTAI,'') AS TANTO_SYOUNIN_JYOUTAI, " _
            & "   COALESCE(TANTO_SYOUNIN_KA,'') AS TANTO_SYOUNIN_KA, " _
            & "   COALESCE(TANTO.SHAIN_NAME,'') AS TANTO_NAME, " _
            & "   COALESCE(TANTO_SYOUNIN_HI,'') AS TANTO_SYOUNIN_HI, " _
            & "   COALESCE(TANTO_SYOUNIN_JIKAN,'') AS TANTO_SYOUNIN_JIKAN, " _
            & "   COALESCE(KACHOU_SYOUNIN_JYOUTAI,'') AS KACHOU_SYOUNIN_JYOUTAI, " _
            & "   COALESCE(KACHOU_SYOUNIN_KA,'') AS KACHOU_SYOUNIN_KA, " _
            & "   COALESCE(KACHOU.SHAIN_NAME,'') AS KACHOU_NAME, " _
            & "   COALESCE(KACHOU_SYOUNIN_HI,'') AS KACHOU_SYOUNIN_HI, " _
            & "   COALESCE(KACHOU_SYOUNIN_JIKAN,'') AS KACHOU_SYOUNIN_JIKAN, " _
            & "   COALESCE(MAIL_TSUCHI_HI,'') AS MAIL_TSUCHI_HI " _
            & "FROM  " _
            & "   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_TSUCHI BLOCK WITH (NOLOCK, NOWAIT) " _
            & "LEFT JOIN  " _
            & "   (" + DataSqlCommon.Get_All_Syain_Sql() + " ) SYAIN " _
            & "ON  " _
            & "   BLOCK.USER_ID=SYAIN.SHAIN_NO " _
            & "LEFT JOIN  " _
            & "   (" + DataSqlCommon.Get_All_Syain_Sql() + " ) TANTO " _
            & "ON  " _
            & "   BLOCK.TANTO_SYOUNIN_SYA=TANTO.SHAIN_NO " _
            & "LEFT JOIN  " _
            & "   (" + DataSqlCommon.Get_All_Syain_Sql() + " ) KACHOU " _
            & "ON  " _
            & "   BLOCK.KACHOU_SYOUNIN_SYA=KACHOU.SHAIN_NO " _
            & "LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 R " _
            & "ON BLOCK.SHISAKU_BUKA_CODE=R.BU_CODE+R.KA_CODE " _
            & "WHERE " _
            & "   MAIL_TSUCHI_HI >= " & MailTsuchiHi1 & " AND MAIL_TSUCHI_HI <= " & MailTsuchiHi2 _
            & " GROUP BY SHISAKU_EVENT_CODE, " _
            & "          SHISAKU_BUKA_CODE, " _
            & "          SHISAKU_BLOCK_NO, " _
            & "          SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & "          BLOCK_FUYOU, " _
            & "          JYOUTAI, " _
            & "          UNIT_KBN, " _
            & "          SHISAKU_BLOCK_NAME, " _
            & "          TEL_NO, " _
            & "          SYAIN.SHAIN_NAME, " _
            & "          TANTO.SHAIN_NAME, " _
            & "          KACHOU.SHAIN_NAME, " _
            & "          KACHOU_SYOUNIN_HI, " _
            & "          SAISYU_KOUSHINBI, " _
            & "          SAISYU_KOUSHINJIKAN, " _
            & "          TANTO_SYOUNIN_JYOUTAI, " _
            & "          TANTO_SYOUNIN_KA, " _
            & "          TANTO_SYOUNIN_HI, " _
            & "          TANTO_SYOUNIN_JIKAN, " _
            & "          KACHOU_SYOUNIN_JYOUTAI, " _
            & "          KACHOU_SYOUNIN_KA, " _
            & "          KACHOU_SYOUNIN_JIKAN, " _
            & "          MAIL_TSUCHI_HI " _
            & "ORDER BY  " _
            & "   KA_RYAKU_NAME ASC, " _
            & "   SHISAKU_BLOCK_NO ASC "

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockTsuchiVo)(sql)
        End Function

#End Region

#Region "試作部品表編集・改定編集（ブロック）改訂通知へ送信日を更新する"

        '仕様変更により強制的に承認欄の中身を空にする処理を追加'
        Public Sub UpdateByTShisakuSekkeiBlockTsuchi(ByVal MailTsuchiHi1 As Integer, ByVal MailTsuchiHi2 As Integer _
                                        ) Implements Dao.IShisakuBuhinKaiteiBlockTsuchiDao.UpdateByTShisakuSekkeiBlockTsuchi
            Dim sql As String = _
            "UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_TSUCHI " _
            & " SET " _
            & " MAIL_TSUCHI_HI=@MailTsuchiHi, " _
            & " UPDATED_USER_ID=@UpdatedUserId, " _
            & " UPDATED_DATE=@UpdatedDate, " _
            & " UPDATED_TIME=@UpdatedTime " _
            & "     WHERE " _
            & "         MAIL_TSUCHI_HI >= " & MailTsuchiHi1 & " AND MAIL_TSUCHI_HI <= " & MailTsuchiHi2

            Dim aDate As New ShisakuDate
            Dim param As New TShisakuSekkeiBlockTsuchiVo

            'メール通知日へ更新日（システム日付）を設定
            param.MailTsuchiHi = Integer.Parse(aDate.CurrentDateDbFormat.Replace("-", ""))

            '更新日
            param.UpdatedUserId = "088267" 'SKE1森淳さんのIDを固定でセット
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            Dim db As New EBomDbClient
            db.Update(sql, param)
        End Sub

#End Region


    End Class

End Namespace

