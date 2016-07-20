Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Dao
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShisakuSekkeiBlockDaoImpl : Inherits DaoEachFeatureImpl
        Implements IShisakuSekkeiBlockDao

        Public Const Site_Kbn = 1

        ''' <summary>
        ''' 試作部品作成メニューExcel出力（集計）
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns>集計一覧</returns>
        ''' <remarks></remarks>
        Public Function GetShisakuSekkeiBlockCount(ByVal eventCode As String) As System.Collections.Generic.List(Of ShisakuSekkeiBlockCountVo) Implements IShisakuSekkeiBlockDao.GetShisakuSekkeiBlockCount
            Dim sql As String = _
            "SELECT " _
             & "    COALESCE(B.SHISAKU_BUKA_CODE,'') AS BUKA_CODE ,  " _
             & "    COALESCE(MAX(R.KA_RYAKU_NAME),B.SHISAKU_BUKA_CODE) AS KA_RYAKU_NAME ,  " _
             & "    COALESCE(( " _
             & "        SELECT COUNT(SHISAKU_BLOCK_NO)   FROM " _
             & "            (" + Me.GetShisakuSekkiBlockSubSet() _
             & "            AND SHISAKU_BUKA_CODE=B.SHISAKU_BUKA_CODE" _
             & "        )T_BLOCK" _
             & "    ),0) AS TOTAL_BLOCK, " _
             & "    COALESCE(( " _
             & "        SELECT COUNT(SHISAKU_BLOCK_NO)   FROM " _
             & "            (" + Me.GetShisakuSekkiBlockSubSet() _
             & "            AND SHISAKU_BUKA_CODE=B.SHISAKU_BUKA_CODE" _
             & "            AND JYOUTAI=@Jyoutai " _
             & "        ) T_JYOUTAI" _
             & "    ),0) AS TOTAL_JYOUTAI, " _
             & "    COALESCE(( " _
             & "        SELECT COUNT(SHISAKU_BLOCK_NO)   FROM " _
             & "            (" + Me.GetShisakuSekkiBlockSubSet() _
             & "            AND SHISAKU_BUKA_CODE=B.SHISAKU_BUKA_CODE" _
             & "            AND TANTO_SYOUNIN_JYOUTAI=@TantoSyouninJyoutai " _
             & "        ) T_SYOUNIN_JYOUTAI" _
             & "    ),0) AS TOTAL_SYOUNIN_JYOUTAI, " _
             & "    COALESCE(( " _
             & "        SELECT COUNT(SHISAKU_BLOCK_NO)   FROM " _
             & "            (" + Me.GetShisakuSekkiBlockSubSet() _
             & "            AND SHISAKU_BUKA_CODE=B.SHISAKU_BUKA_CODE" _
             & "            AND  KACHOU_SYOUNIN_JYOUTAI=@KachouSyouninJyoutai " _
             & "        ) T_KACHOU_SYOUNIN_JYOUTAI" _
             & "    ),0) AS TOTAL_KACHOU_SYOUNIN_JYOUTAI " _
             & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK B WITH (NOLOCK, NOWAIT) " _
             & "LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 R " _
             & "ON B.SHISAKU_BUKA_CODE=R.BU_CODE+R.KA_CODE " _
             & "WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
             & "GROUP BY SHISAKU_BUKA_CODE " _
             & "ORDER BY SHISAKU_BUKA_CODE "
            '& " AND SITE_KBN = " & "'" & Site_Kbn & "'" _
            '& "GROUP BY SHISAKU_BUKA_CODE,KA_RYAKU_NAME "

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = eventCode
            param.BlockFuyou = ShishakuSekkeiBlockHitsuyou
            param.Jyoutai = ShishakuSekkeiBlockStatusShouchiKanryou
            param.TantoSyouninJyoutai = ShishakuSekkeiBlockStatusShounin1
            param.KachouSyouninJyoutai = ShishakuSekkeiBlockStatusShounin2


            Dim db As New EBomDbClient
            Return db.QueryForList(Of ShisakuSekkeiBlockCountVo)(sql, param)
        End Function

        ''' <summary>
        ''' 試作部品作成メニューExcel出力（明細）
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns>明細一覧</returns>
        ''' <remarks></remarks>
        Public Function GetShisakuSekkeiBlockMeisai(ByVal eventCode As String) As System.Collections.Generic.List(Of ShisakuSekkeiBlockMeisaiVo) Implements IShisakuSekkeiBlockDao.GetShisakuSekkeiBlockMeisai
            Dim sql As String = _
            "SELECT " _
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
            & "   COALESCE(MAX(R1.KA_RYAKU_NAME),'') AS TANTO_SYOUNIN_KA, " _
            & "   COALESCE(TANTO.SHAIN_NAME,'') AS TANTO_NAME, " _
            & "   COALESCE(TANTO_SYOUNIN_HI,'') AS TANTO_SYOUNIN_HI, " _
            & "   COALESCE(KACHOU_SYOUNIN_JYOUTAI,'') AS KACHOU_SYOUNIN_JYOUTAI, " _
            & "   COALESCE(MAX(R2.KA_RYAKU_NAME),'') AS KACHOU_SYOUNIN_KA, " _
            & "   COALESCE(KACHOU.SHAIN_NAME,'') AS KACHOU_NAME, " _
            & "   COALESCE(KACHOU_SYOUNIN_HI,'') AS KACHOU_SYOUNIN_HI " _
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
            & "LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 R1 " _
            & "ON BLOCK.TANTO_SYOUNIN_KA =R1.BU_CODE+R1.KA_CODE " _
            & "LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 R2 " _
            & "ON BLOCK.KACHOU_SYOUNIN_KA=R2.BU_CODE+R2.KA_CODE " _
            & "WHERE " _
            & "   SHISAKU_EVENT_CODE=@ShisakuEventCode " _
            & "   AND SHISAKU_BLOCK_NO_KAITEI_NO= " _
            & "   ( " _
            & "     SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
            & "     FROM  " _
            & "         " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) " _
            & "     WHERE " _
            & "         SHISAKU_EVENT_CODE=BLOCK.SHISAKU_EVENT_CODE" _
            & "     AND SHISAKU_BUKA_CODE=BLOCK.SHISAKU_BUKA_CODE" _
            & "     AND SHISAKU_BLOCK_NO=BLOCK.SHISAKU_BLOCK_NO" _
            & "   ) " _
            & " GROUP BY SHISAKU_BUKA_CODE, " _
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
            & "          KACHOU_SYOUNIN_JYOUTAI, " _
            & "          KACHOU_SYOUNIN_KA " _
            & "ORDER BY  " _
            & "   SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO"
            '& "AND R.SITE_KBN = " & "'" & Site_Kbn & "'" _
            '& "AND R1.SITE_KBN = " & "'" & Site_Kbn & "'" _
            '& "AND R2.SITE_KBN = " & "'" & Site_Kbn & "'" _
            '& " R.SITE_KBN = " & "'" & Site_Kbn & "'" _
            '& "   SHISAKU_BUKA_CODE, KA_RYAKU_NAME, SHISAKU_BLOCK_NO"

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = eventCode
            Dim db As New EBomDbClient
            Return db.QueryForList(Of ShisakuSekkeiBlockMeisaiVo)(Sql, param)
        End Function


        Public Function GetShisakuSekkiBlockSubSet() As String
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK BLOCK WITH (NOLOCK, NOWAIT) " _
            & " WHERE  " _
            & "     SHISAKU_EVENT_CODE=@ShisakuEventCode " _
            & " AND BLOCK_FUYOU=@BlockFuyou  " _
            & " AND SHISAKU_BLOCK_NO_KAITEI_NO=  " _
            & " (  " _
            & "     SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO  " _
            & "     FROM   " _
            & "	        " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT)  " _
            & "     WHERE  " _
            & "	        SHISAKU_EVENT_CODE=BLOCK.SHISAKU_EVENT_CODE " _
            & "     AND SHISAKU_BUKA_CODE=BLOCK.SHISAKU_BUKA_CODE " _
            & "     AND SHISAKU_BLOCK_NO=BLOCK.SHISAKU_BLOCK_NO " _
            & " ) "
            Return sql
        End Function

        ''' <summary>
        ''' 改訂№一番大きいの部課コードを得る
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GeShisakuSekkeiBlockBukaCode(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String) As TShisakuSekkeiBlockVo Implements IShisakuSekkeiBlockDao.GetShisakuSekkeiBlockBukaCode
            Dim sql As String = " SELECT TOP 1 * FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode" _
            & " AND SHISAKU_BLOCK_NO=@ShisakuBlockNo " _
            & " ORDER BY SHISAKU_BLOCK_NO_KAITEI_NO  DESC"
            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = shisakuBlockNo
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuSekkeiBlockVo)(sql, param)
        End Function


        ''' <summary>
        '''　改訂Noを得る
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetShisakuSekkeiKaiteiNo(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As List(Of TShisakuSekkeiBlockVo) Implements IShisakuSekkeiBlockDao.GetShisakuSekkeiKaiteiNo
            Dim sql As String = " SELECT  Distinct SHISAKU_BLOCK_NO_KAITEI_NO FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK " _
           & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
           & " AND SHISAKU_BLOCK_NO=@ShisakuBlockNo " _
           & " AND SHISAKU_BUKA_CODE=@ShisakuBukaCode "

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBukaCode = shisakuBukaCode
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockVo)(sql, param)
        End Function

    End Class
End Namespace

