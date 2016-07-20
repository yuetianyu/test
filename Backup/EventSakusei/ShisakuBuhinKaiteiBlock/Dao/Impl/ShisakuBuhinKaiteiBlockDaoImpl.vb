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

    Public Class ShisakuBuhinKaiteiBlockDaoImpl : Inherits DaoEachFeatureImpl
        Implements IShisakuBuhinKaiteiBlockDao

        Public Const site_kbn = 1

#Region "試作部品表編集・改定編集（ブロック）改訂通知一覧情報を取得する"
        ''' <summary>
        ''' 試作部品表編集・改定編集（ブロック）改訂通知Spread一覧情報を取得する
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <returns>ブロック一覧</returns>
        ''' <remarks></remarks>
        Public Function GetBlockSpreadList(ByVal eventCode As String, _
                                         ByVal bukaCode As String, _
                                         ByVal blockCode1 As String, ByVal blockCode2 As String, _
                                         ByVal saisyuKoushinbi1 As Integer, ByVal saisyuKoushinbi2 As Integer _
                                        ) As List(Of ShisakuBuhinBlockVo) Implements Dao.IShisakuBuhinKaiteiBlockDao.GetBlockSpreadList
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
            & "   COALESCE(TANTO.SHAIN_NAME,TANTO_SYOUNIN_SYA) AS TANTO_NAME, " _
            & "   COALESCE(TANTO_SYOUNIN_HI,'') AS TANTO_SYOUNIN_HI, " _
            & "   COALESCE(TANTO_SYOUNIN_JIKAN,'') AS TANTO_SYOUNIN_JIKAN, " _
            & "   COALESCE(KACHOU_SYOUNIN_JYOUTAI,'') AS KACHOU_SYOUNIN_JYOUTAI, " _
            & "   COALESCE(KACHOU_SYOUNIN_KA,'') AS KACHOU_SYOUNIN_KA, " _
            & "   COALESCE(KACHOU.SHAIN_NAME,KACHOU_SYOUNIN_SYA) AS KACHOU_NAME, " _
            & "   COALESCE(KACHOU_SYOUNIN_HI,'') AS KACHOU_SYOUNIN_HI, " _
            & "   COALESCE(KACHOU_SYOUNIN_JIKAN,'') AS KACHOU_SYOUNIN_JIKAN " _
            & "FROM  " _
            & "   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK BLOCK WITH (NOLOCK, NOWAIT) " _
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
            & "   SHISAKU_EVENT_CODE LIKE @ShisakuEventCode " _
            & "   AND (KA_RYAKU_NAME LIKE @ShisakuBukaCode  or SHISAKU_BUKA_CODE LIKE @ShisakuBukaCode )  " _
            & "   AND SHISAKU_BLOCK_NO >= '" & blockCode1 & "' AND SHISAKU_BLOCK_NO <= '" & blockCode2 & "'" _
            & "   AND ((KACHOU_SYOUNIN_HI >= " & saisyuKoushinbi1 & " AND KACHOU_SYOUNIN_HI <= " & saisyuKoushinbi2 & ")" _
            & "         OR (KACHOU_SYOUNIN_HI Is Null AND " _
            & "             KACHOU_SYOUNIN_HI >= " & saisyuKoushinbi1 & " AND KACHOU_SYOUNIN_HI <= " & saisyuKoushinbi2 & "))" _
            & "   AND KACHOU_SYOUNIN_JYOUTAI = '" & ShishakuSekkeiBlockStatusShounin2 & "'" _
            & "   AND SHISAKU_BLOCK_NO_KAITEI_NO >= '001'" _
            & "   AND SHISAKU_BLOCK_NO_KAITEI_NO= " _
            & "   ( " _
            & "     SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
            & "     FROM  " _
            & "         " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) " _
            & "     WHERE " _
            & "         SHISAKU_EVENT_CODE=BLOCK.SHISAKU_EVENT_CODE" _
            & "     AND SHISAKU_BUKA_CODE=BLOCK.SHISAKU_BUKA_CODE" _
            & "     AND SHISAKU_BLOCK_NO=BLOCK.SHISAKU_BLOCK_NO" _
            & "     AND ((KACHOU_SYOUNIN_HI >= " & saisyuKoushinbi1 & " AND KACHOU_SYOUNIN_HI <= " & saisyuKoushinbi2 & ")" _
            & "         OR (KACHOU_SYOUNIN_HI Is Null AND " _
            & "             KACHOU_SYOUNIN_HI >= " & saisyuKoushinbi1 & " AND KACHOU_SYOUNIN_HI <= " & saisyuKoushinbi2 & "))" _
            & "     AND KACHOU_SYOUNIN_JYOUTAI = '" & ShishakuSekkeiBlockStatusShounin2 & "'" _
            & "   ) " _
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
            & "          TANTO_SYOUNIN_SYA, " _
            & "          KACHOU_SYOUNIN_SYA " _
            & "ORDER BY  " _
            & "   SHISAKU_EVENT_CODE, " _
            & "   KA_RYAKU_NAME ASC, " _
            & "   SHISAKU_BLOCK_NO ASC "
            '& "   AND R.SITE_KBN =" & site_kbn _

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = eventCode
            param.ShisakuBukaCode = bukaCode
            param.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY
            Dim db As New EBomDbClient
            Return db.QueryForList(Of ShisakuBuhinBlockVo)(sql, param)
        End Function
#End Region

    End Class

End Namespace

