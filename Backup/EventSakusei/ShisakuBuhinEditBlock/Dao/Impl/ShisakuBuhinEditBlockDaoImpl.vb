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

Namespace ShisakuBuhinEditBlock.Dao

    Public Class ShisakuBuhinEditBlockDaoImpl : Inherits DaoEachFeatureImpl
        Implements IShisakuBuhinEditBlockDao

        Public Const site_kbn = 1

#Region "試作部品表編集・改定編集（ブロック）課別状況取得する"
        ''' <summary>
        ''' 試作部品表編集・改定編集（ブロック）課別状況取得する
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <returns>進度状況合計</returns>
        ''' <remarks></remarks>
        Public Function GetKabetuJyoutai(ByVal eventCode As String, _
                                         ByVal bukaCode As String _
                                        ) As KabetuJyoutaiVo Implements IShisakuBuhinEditBlockDao.GetKabetuJyoutai
            Dim sql As String = _
            "SELECT " _
             & "    COALESCE(MAX(R.KA_RYAKU_NAME),'') AS KA_RYAKU_NAME ,  " _
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
             & "WHERE SHISAKU_BUKA_CODE=@ShisakuBukaCode " _
             & "GROUP BY SHISAKU_BUKA_CODE "
            '& "GROUP BY SHISAKU_BUKA_CODE,KA_RYAKU_NAME "

            Dim param As New TShisakuSekkeiBlockVo
            If Not eventCode = String.Empty Then
                param.ShisakuEventCode = eventCode
            End If
            If Not bukaCode = String.Empty Then
                param.ShisakuBukaCode = bukaCode
            End If
            param.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY
            param.Jyoutai = TShisakuSekkeiBlockVoHelper.Jyoutai.FINISHED
            param.TantoSyouninJyoutai = TShisakuSekkeiBlockVoHelper.TantoJyoutai.APPROVAL
            param.KachouSyouninJyoutai = TShisakuSekkeiBlockVoHelper.KachouJyoutai.APPROVAL

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of KabetuJyoutaiVo)(sql, param)
        End Function
#End Region

#Region "試作部品表編集・改定編集（ブロック）一覧情報を取得する"
        ''' <summary>
        ''' 試作部品表編集・改定編集（ブロック）Spread一覧情報を取得する
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <returns>ブロック一覧</returns>
        ''' <remarks></remarks>
        Public Function GetBlockSpreadList(ByVal eventCode As String, _
                                         ByVal bukaCode As String _
                                        ) As List(Of ShisakuBuhinBlockVo) Implements Dao.IShisakuBuhinEditBlockDao.GetBlockSpreadList
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
            & "   SHISAKU_EVENT_CODE=@ShisakuEventCode " _
            & "   AND SHISAKU_BUKA_CODE=@ShisakuBukaCode " _
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
            & "          TANTO_SYOUNIN_JIKAN, " _
            & "          KACHOU_SYOUNIN_JYOUTAI, " _
            & "          KACHOU_SYOUNIN_KA, " _
            & "          KACHOU_SYOUNIN_JIKAN, " _
            & "          TANTO_SYOUNIN_SYA, " _
            & "          KACHOU_SYOUNIN_SYA " _
            & "ORDER BY  " _
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

        ''' <summary>
        ''' ブロックNo.同じの場合、改訂No.大きい１つにする
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
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
            & "	        " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK  " _
            & "     WHERE  " _
            & "	        SHISAKU_EVENT_CODE=BLOCK.SHISAKU_EVENT_CODE " _
            & "     AND SHISAKU_BUKA_CODE=BLOCK.SHISAKU_BUKA_CODE " _
            & "     AND SHISAKU_BLOCK_NO=BLOCK.SHISAKU_BLOCK_NO " _
            & " ) "
            Return sql
        End Function

        Public Function FindByPk(ByVal shisakuEventCode As String, _
                          ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String, _
                          ByVal shisakuBlockNoKaiteiNo As String) As ShisakuBuhinBlockVo Implements IShisakuBuhinEditBlockDao.FindByPk
            Dim sql As String = _
            " SELECT " _
            & "   COALESCE(SHISAKU_BUKA_CODE,'') AS SHISAKU_BUKA_CODE , " _
            & "   COALESCE(R.KA_RYAKU_NAME,'') AS KA_RYAKU_NAME , " _
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
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK BLOCK WITH (NOLOCK, NOWAIT) " _
            & " LEFT JOIN  " _
            & "   (" + DataSqlCommon.Get_All_Syain_Sql() + " ) SYAIN " _
            & " ON  " _
            & "   BLOCK.USER_ID=SYAIN.SHAIN_NO " _
            & " LEFT JOIN  " _
            & "   (" + DataSqlCommon.Get_All_Syain_Sql() + " ) TANTO " _
            & " ON  " _
            & "   BLOCK.TANTO_SYOUNIN_SYA=TANTO.SHAIN_NO " _
            & " LEFT JOIN  " _
            & "   (" + DataSqlCommon.Get_All_Syain_Sql() + " ) KACHOU " _
            & " ON  " _
            & "   BLOCK.KACHOU_SYOUNIN_SYA=KACHOU.SHAIN_NO " _
            & " LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 R " _
            & " ON BLOCK.SHISAKU_BUKA_CODE=R.BU_CODE+R.KA_CODE " _
            & "     WHERE " _
            & "         SHISAKU_EVENT_CODE=@ShisakuEventCode" _
            & "     AND SHISAKU_BUKA_CODE=@ShisakuBukaCode" _
            & "     AND SHISAKU_BLOCK_NO=@ShisakuBlockNo" _
            & "     AND SHISAKU_BLOCK_NO_KAITEI_NO=@ShisakuBlockNoKaiteiNo"

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of ShisakuBuhinBlockVo)(sql, param)
        End Function

        Public Sub UpdateByPk(ByVal shisakuBlock As TShisakuSekkeiBlockVo) Implements IShisakuBuhinEditBlockDao.UpdateByPk
            Dim sql As String = _
            "UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK " _
            & " SET UPDATED_USER_ID=@UpdatedUserId, " _
            & "SHISAKU_BLOCK_NAME=@ShisakuBlockName "
            If Not StringUtil.IsEmpty(shisakuBlock.BlockFuyou) Then
                sql = sql & ", BLOCK_FUYOU=@BlockFuyou "
            ElseIf Not StringUtil.IsEmpty(shisakuBlock.Jyoutai) Then
                sql = sql & ", JYOUTAI=@Jyoutai "
            ElseIf Not StringUtil.IsEmpty(shisakuBlock.TantoSyouninJyoutai) Then
                sql = sql & ", TANTO_SYOUNIN_JYOUTAI=@TantoSyouninJyoutai, " _
                & " TANTO_SYOUNIN_KA=@TantoSyouninKa, " _
                & " TANTO_SYOUNIN_SYA=@TantoSyouninSya, " _
                & " TANTO_SYOUNIN_HI=@TantoSyouninHi, " _
                & " TANTO_SYOUNIN_JIKAN=@TantoSyouninJikan "
            ElseIf Not StringUtil.IsEmpty(shisakuBlock.KachouSyouninJyoutai) Then
                sql = sql & ", KACHOU_SYOUNIN_JYOUTAI=@KachouSyouninJyoutai, " _
                & " KACHOU_SYOUNIN_KA=@KachouSyouninKa, " _
                & " KACHOU_SYOUNIN_SYA=@KachouSyouninSya, " _
                & " KACHOU_SYOUNIN_HI=@KachouSyouninHi, " _
                & " KACHOU_SYOUNIN_JIKAN=@KachouSyouninJikan "
            End If

            If Not StringUtil.IsEmpty(shisakuBlock.UserId) And _
                Not StringUtil.IsEmpty(shisakuBlock.SaisyuKoushinbi) And _
                Not StringUtil.IsEmpty(shisakuBlock.SaisyuKoushinjikan) Then
                sql = sql & "," _
                & " USER_ID=@UserId, " _
                & " SAISYU_KOUSHINBI=@SaisyuKoushinbi, " _
                & " SAISYU_KOUSHINJIKAN=@SaisyuKoushinjikan, " _
                & " UPDATED_DATE=@UpdatedDate, " _
                & " UPDATED_TIME=@UpdatedTime " _
                & "     WHERE " _
                & "         SHISAKU_EVENT_CODE=@ShisakuEventCode" _
                & "     AND SHISAKU_BUKA_CODE=@ShisakuBukaCode" _
                & "     AND SHISAKU_BLOCK_NO=@ShisakuBlockNo" _
                & "     AND SHISAKU_BLOCK_NO_KAITEI_NO=@ShisakuBlockNoKaiteiNo"
            Else
                sql = sql & "" _
                & "     WHERE " _
                & "         SHISAKU_EVENT_CODE=@ShisakuEventCode" _
                & "     AND SHISAKU_BUKA_CODE=@ShisakuBukaCode" _
                & "     AND SHISAKU_BLOCK_NO=@ShisakuBlockNo" _
                & "     AND SHISAKU_BLOCK_NO_KAITEI_NO=@ShisakuBlockNoKaiteiNo"
                '& " UPDATED_DATE=@UpdatedDate, " _
                '& " UPDATED_TIME=@UpdatedTime " _
            End If

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuBlock.ShisakuEventCode
            param.ShisakuBukaCode = shisakuBlock.ShisakuBukaCode
            param.ShisakuBlockNo = shisakuBlock.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlock.ShisakuBlockNoKaiteiNo
            param.ShisakuBlockName = shisakuBlock.ShisakuBlockName

            If StringUtil.IsEmpty(param.ShisakuEventCode) OrElse _
               StringUtil.IsEmpty(param.ShisakuBukaCode) OrElse _
               StringUtil.IsEmpty(param.ShisakuBlockNo) OrElse _
               StringUtil.IsEmpty(param.ShisakuBlockNoKaiteiNo) Then
                Exit Sub
            End If

            Dim aDate As New ShisakuDate
            If Not StringUtil.IsEmpty(shisakuBlock.BlockFuyou) Then
                param.BlockFuyou = shisakuBlock.BlockFuyou
            ElseIf Not StringUtil.IsEmpty(shisakuBlock.Jyoutai) Then
                param.Jyoutai = shisakuBlock.Jyoutai
            ElseIf Not StringUtil.IsEmpty(shisakuBlock.ShisakuBlockName) Then
                param.ShisakuBlockName = shisakuBlock.ShisakuBlockName
            End If

            'ElseIfの時処理が走らなかったので一旦分けます'
            If Not StringUtil.IsEmpty(shisakuBlock.TantoSyouninJyoutai) Then
                param.TantoSyouninJyoutai = shisakuBlock.TantoSyouninJyoutai
                param.TantoSyouninKa = LoginInfo.Now.BukaCode
                param.TantoSyouninSya = LoginInfo.Now.UserId
                param.TantoSyouninHi = Integer.Parse(aDate.CurrentDateDbFormat.Replace("-", ""))
                param.TantoSyouninJikan = Integer.Parse(aDate.CurrentTimeDbFormat.Replace(":", ""))
            End If

            If Not StringUtil.IsEmpty(shisakuBlock.KachouSyouninJyoutai) Then
                param.KachouSyouninJyoutai = shisakuBlock.KachouSyouninJyoutai
                param.KachouSyouninKa = LoginInfo.Now.BukaCode
                param.KachouSyouninSya = LoginInfo.Now.UserId
                param.KachouSyouninHi = Integer.Parse(aDate.CurrentDateDbFormat.Replace("-", ""))
                param.KachouSyouninJikan = Integer.Parse(aDate.CurrentTimeDbFormat.Replace(":", ""))

            End If

            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            If StringUtil.IsNotEmpty(shisakuBlock.UserId) Then
                param.UserId = shisakuBlock.UserId
            End If
            If StringUtil.IsNotEmpty(shisakuBlock.SaisyuKoushinbi) Then
                param.SaisyuKoushinbi = shisakuBlock.SaisyuKoushinbi
            End If
            If StringUtil.IsNotEmpty(shisakuBlock.SaisyuKoushinjikan) Then
                param.SaisyuKoushinjikan = shisakuBlock.SaisyuKoushinjikan
            End If

            Dim db As New EBomDbClient
            db.Update(sql, param)
        End Sub

        Public Sub UpdateByPkMove(ByVal shisakuBlock As TShisakuSekkeiBlockVo) Implements IShisakuBuhinEditBlockDao.UpdateByPkMove
            Dim sql As String = _
            "UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK " _
            & " SET " _
            & " JYOUTAI=@Jyoutai, " _
            & " TANTO_SYOUNIN_JYOUTAI=@TantoSyouninJyoutai, " _
            & " TANTO_SYOUNIN_KA=@TantoSyouninKa, " _
            & " TANTO_SYOUNIN_SYA=@TantoSyouninSya, " _
            & " TANTO_SYOUNIN_HI=@TantoSyouninHi, " _
            & " TANTO_SYOUNIN_JIKAN=@TantoSyouninJikan, " _
            & " KACHOU_SYOUNIN_JYOUTAI=@KachouSyouninJyoutai, " _
            & " KACHOU_SYOUNIN_KA=@KachouSyouninKa, " _
            & " KACHOU_SYOUNIN_SYA=@KachouSyouninSya, " _
            & " KACHOU_SYOUNIN_HI=@KachouSyouninHi, " _
            & " KACHOU_SYOUNIN_JIKAN=@KachouSyouninJikan, " _
            & " UPDATED_USER_ID=@UpdatedUserId, " _
            & " UPDATED_DATE=@UpdatedDate, " _
            & " UPDATED_TIME=@UpdatedTime " _
            & "     WHERE " _
            & "         SHISAKU_EVENT_CODE=@ShisakuEventCode" _
            & "     AND SHISAKU_BUKA_CODE=@ShisakuBukaCode" _
            & "     AND SHISAKU_BLOCK_NO=@ShisakuBlockNo" _
            & "     AND SHISAKU_BLOCK_NO_KAITEI_NO=@ShisakuBlockNoKaiteiNo"

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuBlock.ShisakuEventCode
            param.ShisakuBukaCode = shisakuBlock.ShisakuBukaCode
            param.ShisakuBlockNo = shisakuBlock.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlock.ShisakuBlockNoKaiteiNo

            If StringUtil.IsEmpty(param.ShisakuEventCode) OrElse _
               StringUtil.IsEmpty(param.ShisakuBukaCode) OrElse _
               StringUtil.IsEmpty(param.ShisakuBlockNo) OrElse _
               StringUtil.IsEmpty(param.ShisakuBlockNoKaiteiNo) Then
                Exit Sub
            End If

            param.Jyoutai = TShisakuSekkeiBlockVoHelper.Jyoutai.EDIT
            param.TantoSyouninJyoutai = ""
            param.TantoSyouninKa = ""
            param.TantoSyouninSya = ""
            param.TantoSyouninHi = 0
            param.TantoSyouninJikan = 0

            param.KachouSyouninJyoutai = ""
            param.KachouSyouninKa = ""
            param.KachouSyouninSya = ""
            param.KachouSyouninHi = 0
            param.KachouSyouninJikan = 0

            param.UpdatedUserId = LoginInfo.Now.UserId
            Dim aDate As New ShisakuDate
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            Dim db As New EBomDbClient
            db.Update(sql, param)
        End Sub

        '仕様変更により強制的に承認欄の中身を空にする処理を追加'
        Public Sub UpdateByPkMove2(ByVal shisakuBlock As TShisakuSekkeiBlockVo) Implements IShisakuBuhinEditBlockDao.UpdateByPkMove2
            Dim sql As String = _
            "UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK " _
            & " SET " _
            & " TANTO_SYOUNIN_JYOUTAI=@TantoSyouninJyoutai, " _
            & " TANTO_SYOUNIN_KA=@TantoSyouninKa, " _
            & " TANTO_SYOUNIN_SYA=@TantoSyouninSya, " _
            & " TANTO_SYOUNIN_HI=@TantoSyouninHi, " _
            & " TANTO_SYOUNIN_JIKAN=@TantoSyouninJikan, " _
            & " KACHOU_SYOUNIN_JYOUTAI=@KachouSyouninJyoutai, " _
            & " KACHOU_SYOUNIN_KA=@KachouSyouninKa, " _
            & " KACHOU_SYOUNIN_SYA=@KachouSyouninSya, " _
            & " KACHOU_SYOUNIN_HI=@KachouSyouninHi, " _
            & " KACHOU_SYOUNIN_JIKAN=@KachouSyouninJikan, " _
            & " UPDATED_USER_ID=@UpdatedUserId, " _
            & " UPDATED_DATE=@UpdatedDate, " _
            & " UPDATED_TIME=@UpdatedTime " _
            & "     WHERE " _
            & "         SHISAKU_EVENT_CODE=@ShisakuEventCode" _
            & "     AND SHISAKU_BUKA_CODE=@ShisakuBukaCode" _
            & "     AND SHISAKU_BLOCK_NO=@ShisakuBlockNo" _
            & "     AND SHISAKU_BLOCK_NO_KAITEI_NO=@ShisakuBlockNoKaiteiNo"

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuBlock.ShisakuEventCode
            param.ShisakuBukaCode = shisakuBlock.ShisakuBukaCode
            param.ShisakuBlockNo = shisakuBlock.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlock.ShisakuBlockNoKaiteiNo

            If StringUtil.IsEmpty(param.ShisakuEventCode) OrElse _
               StringUtil.IsEmpty(param.ShisakuBukaCode) OrElse _
               StringUtil.IsEmpty(param.ShisakuBlockNo) OrElse _
               StringUtil.IsEmpty(param.ShisakuBlockNoKaiteiNo) Then
                Exit Sub
            End If

            param.TantoSyouninJyoutai = ""
            param.TantoSyouninKa = ""
            param.TantoSyouninSya = ""
            param.TantoSyouninHi = 0
            param.TantoSyouninJikan = 0

            param.KachouSyouninJyoutai = ""
            param.KachouSyouninKa = ""
            param.KachouSyouninSya = ""
            param.KachouSyouninHi = 0
            param.KachouSyouninJikan = 0

            param.UpdatedUserId = LoginInfo.Now.UserId
            Dim aDate As New ShisakuDate
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            Dim db As New EBomDbClient
            db.Update(sql, param)
        End Sub

        Public Function UpdateByPkHasReturn(ByVal shisakuBlock As TShisakuSekkeiBlockVo) As ShisakuBuhinBlockVo Implements IShisakuBuhinEditBlockDao.UpdateByPkHasReturn
            If StringUtil.IsEmpty(shisakuBlock.ShisakuEventCode) OrElse _
               StringUtil.IsEmpty(shisakuBlock.ShisakuBukaCode) OrElse _
               StringUtil.IsEmpty(shisakuBlock.ShisakuBlockNo) OrElse _
               StringUtil.IsEmpty(shisakuBlock.ShisakuBlockNoKaiteiNo) Then
                Return Nothing
            End If
            Me.UpdateByPk(shisakuBlock)
            Dim updatedShisakuSekkeiBlock = Me.FindByPk(shisakuBlock.ShisakuEventCode, _
                                           shisakuBlock.ShisakuBukaCode, _
                                           shisakuBlock.ShisakuBlockNo, _
                                           shisakuBlock.ShisakuBlockNoKaiteiNo)


            '-------------------------------------------------------------
            '２次改修
            'ブロック通知情報を作成する
            '   条件として改訂№が００１以降で課長承認にチェックを付けたときとする。



            If shisakuBlock.ShisakuBlockNoKaiteiNo >= "001" And _
                shisakuBlock.KachouSyouninJyoutai = TShisakuSekkeiBlockVoHelper.KachouJyoutai.APPROVAL Then
                InsertShisakuSekkeiBlockTsuchi(shisakuBlock.ShisakuEventCode, _
                                         shisakuBlock.ShisakuBukaCode, _
                                         shisakuBlock.ShisakuBlockNo)
            End If
            '-------------------------------------------------------------


            Return updatedShisakuSekkeiBlock
        End Function

        'ブロック不要のチェックのせいで改訂Noがあがった時用'
        Public Function UpdateByPkHasReturnBlockFuyou(ByVal shisakuBlock As TShisakuSekkeiBlockVo) As ShisakuBuhinBlockVo Implements IShisakuBuhinEditBlockDao.UpdateByPkHasReturnBlockFuyou
            If StringUtil.IsEmpty(shisakuBlock.ShisakuEventCode) OrElse _
               StringUtil.IsEmpty(shisakuBlock.ShisakuBukaCode) OrElse _
               StringUtil.IsEmpty(shisakuBlock.ShisakuBlockNo) OrElse _
               StringUtil.IsEmpty(shisakuBlock.ShisakuBlockNoKaiteiNo) Then
                Return Nothing
            End If


            If Not StringUtil.Equals(shisakuBlock.ShisakuBlockNoKaiteiNo, "000") Then
                'shisakuBlock.BlockFuyou = "0"
            End If
            'アップデートはしない'
            'Me.UpdateByPk(shisakuBlock)
            '最新の情報を取ってくるだけ'
            Dim updatedShisakuSekkeiBlock = Me.FindByPk(shisakuBlock.ShisakuEventCode, _
                                           shisakuBlock.ShisakuBukaCode, _
                                           shisakuBlock.ShisakuBlockNo, _
                                           Right("000" + (Integer.Parse(shisakuBlock.ShisakuBlockNoKaiteiNo) + 1).ToString, 3))
            Return updatedShisakuSekkeiBlock
        End Function

        Public Sub InsertByUpdateKaiteiNo(ByVal shisakuBlock As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockVo) Implements IShisakuBuhinEditBlockDao.InsertByUpdateKaiteiNo
            If StringUtil.IsEmpty(shisakuBlock.ShisakuEventCode) OrElse _
               StringUtil.IsEmpty(shisakuBlock.ShisakuBukaCode) OrElse _
               StringUtil.IsEmpty(shisakuBlock.ShisakuBlockNo) OrElse _
               StringUtil.IsEmpty(shisakuBlock.ShisakuBlockNoKaiteiNo) Then
                Exit Sub
            End If
            Using db As New EBomDbClient
                db.BeginTransaction()

                'ブロック情報'
                InsertShisakuSekkeiBlock(shisakuBlock.ShisakuEventCode, _
                                         shisakuBlock.ShisakuBukaCode, _
                                         shisakuBlock.ShisakuBlockNo)
                'ブロック装備情報'
                InsertShisakuSekkeiBlockSuobi(shisakuBlock.ShisakuEventCode, _
                                              shisakuBlock.ShisakuBukaCode, _
                                              shisakuBlock.ShisakuBlockNo)
                'ブロック装備仕様'
                InsertShisakuSekkeiBlockSuobiShiyou(shisakuBlock.ShisakuEventCode, _
                                                    shisakuBlock.ShisakuBukaCode, _
                                                    shisakuBlock.ShisakuBlockNo)
                'メモ情報'
                InsertShisakuSekkeiBlockMemo(shisakuBlock.ShisakuEventCode, _
                                             shisakuBlock.ShisakuBukaCode, _
                                             shisakuBlock.ShisakuBlockNo)
                '設計ブロックINSTL情報'
                InsertShisakuSekkeiBlockInstl(shisakuBlock.ShisakuEventCode, _
                                              shisakuBlock.ShisakuBukaCode, _
                                              shisakuBlock.ShisakuBlockNo)
                'InsertShisakuBuhinKousei(shisakuBlock.ShisakuEventCode, _
                '                         shisakuBlock.ShisakuBukaCode, _
                '                         shisakuBlock.ShisakuBlockNo)
                'InsertShisakuBuhin(shisakuBlock.ShisakuEventCode, _
                '                   shisakuBlock.ShisakuBukaCode, _
                '                   shisakuBlock.ShisakuBlockNo)
                InsertShisakuBuhinEdit(shisakuBlock.ShisakuEventCode, _
                                   shisakuBlock.ShisakuBukaCode, _
                                   shisakuBlock.ShisakuBlockNo)
                InsertShisakuBuhinEditInstl(shisakuBlock.ShisakuEventCode, _
                   shisakuBlock.ShisakuBukaCode, _
                   shisakuBlock.ShisakuBlockNo)
                db.Commit()
            End Using
            
        End Sub

        Public Sub InsertByUpdateKaiteiNoFuyou(ByVal shisakuBlock As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockVo) Implements IShisakuBuhinEditBlockDao.InsertByUpdateKaiteiNoFuyou
            If StringUtil.IsEmpty(shisakuBlock.ShisakuEventCode) OrElse _
               StringUtil.IsEmpty(shisakuBlock.ShisakuBukaCode) OrElse _
               StringUtil.IsEmpty(shisakuBlock.ShisakuBlockNo) OrElse _
               StringUtil.IsEmpty(shisakuBlock.ShisakuBlockNoKaiteiNo) Then
                Exit Sub
            End If
            Using db As New EBomDbClient
                db.BeginTransaction()
                'ブロック情報'
                InsertShisakuSekkeiBlockFuyou(shisakuBlock.ShisakuEventCode, _
                                              shisakuBlock.ShisakuBukaCode, _
                                              shisakuBlock.ShisakuBlockNo)
                'ブロック装備情報'
                InsertShisakuSekkeiBlockSuobi(shisakuBlock.ShisakuEventCode, _
                                              shisakuBlock.ShisakuBukaCode, _
                                              shisakuBlock.ShisakuBlockNo)
                'ブロック装備仕様'
                InsertShisakuSekkeiBlockSuobiShiyou(shisakuBlock.ShisakuEventCode, _
                                                    shisakuBlock.ShisakuBukaCode, _
                                                    shisakuBlock.ShisakuBlockNo)
                'メモ情報'
                InsertShisakuSekkeiBlockMemo(shisakuBlock.ShisakuEventCode, _
                                             shisakuBlock.ShisakuBukaCode, _
                                             shisakuBlock.ShisakuBlockNo)
                '設計ブロックINSTL情報'
                InsertShisakuSekkeiBlockInstl(shisakuBlock.ShisakuEventCode, _
                                              shisakuBlock.ShisakuBukaCode, _
                                              shisakuBlock.ShisakuBlockNo)
                'InsertShisakuBuhinKousei(shisakuBlock.ShisakuEventCode, _
                '                         shisakuBlock.ShisakuBukaCode, _
                '                         shisakuBlock.ShisakuBlockNo)
                'InsertShisakuBuhin(shisakuBlock.ShisakuEventCode, _
                '                   shisakuBlock.ShisakuBukaCode, _
                '                   shisakuBlock.ShisakuBlockNo)
                InsertShisakuBuhinEdit(shisakuBlock.ShisakuEventCode, _
                                   shisakuBlock.ShisakuBukaCode, _
                                   shisakuBlock.ShisakuBlockNo)
                InsertShisakuBuhinEditInstl(shisakuBlock.ShisakuEventCode, _
                   shisakuBlock.ShisakuBukaCode, _
                   shisakuBlock.ShisakuBlockNo)
                db.Commit()
            End Using

        End Sub


        '-------------------------------------------------------------
        '２次改修
#Region "試作部品表編集・改定編集（ブロック）課長承認後に改訂を上げた一覧情報を更新する。"

        '改訂アップ前のデータを登録する場合には以下を使用。

        Public Sub InsertShisakuSekkeiBlockTsuchi(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String)

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_TSUCHI  ")
                .AppendLine("  ( ")
                .AppendLine("     SHISAKU_EVENT_CODE, ")
                .AppendLine("     SHISAKU_BUKA_CODE, ")
                .AppendLine("     SHISAKU_BLOCK_NO_HYOUJI_JUN, ")
                .AppendLine("     SHISAKU_BLOCK_NO, ")
                .AppendLine("     SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine("     BLOCK_FUYOU, ")
                .AppendLine("     JYOUTAI, ")
                .AppendLine("     UNIT_KBN, ")
                .AppendLine("     SHISAKU_BLOCK_NAME, ")
                .AppendLine("     USER_ID, ")
                .AppendLine("     TEL_NO, ")
                .AppendLine("     SAISYU_KOUSHINBI, ")
                .AppendLine("     SAISYU_KOUSHINJIKAN, ")
                .AppendLine("     MEMO, ")
                .AppendLine("     TANTO_SYOUNIN_JYOUTAI, ")
                .AppendLine("     TANTO_SYOUNIN_KA, ")
                .AppendLine("     TANTO_SYOUNIN_SYA, ")
                .AppendLine("     TANTO_SYOUNIN_HI, ")
                .AppendLine("     TANTO_SYOUNIN_JIKAN, ")
                .AppendLine("     KACHOU_SYOUNIN_JYOUTAI, ")
                .AppendLine("     KACHOU_SYOUNIN_KA, ")
                .AppendLine("     KACHOU_SYOUNIN_SYA, ")
                .AppendLine("     KACHOU_SYOUNIN_HI, ")
                .AppendLine("     KACHOU_SYOUNIN_JIKAN, ")
                .AppendLine("     KAITEI_HANDAN_FLG, ")
                .AppendLine("     MAIL_TSUCHI_HI, ")
                .AppendLine("     CREATED_USER_ID, ")
                .AppendLine("     CREATED_DATE, ")
                .AppendLine("     CREATED_TIME, ")
                .AppendLine("     UPDATED_USER_ID, ")
                .AppendLine("     UPDATED_DATE, ")
                .AppendLine("     UPDATED_TIME ")
                .AppendLine("  ) ")
                .AppendLine("  SELECT  ")
                .AppendLine("     @ShisakuEventCode, ")
                .AppendLine("     @ShisakuBukaCode, ")
                .AppendLine("     SHISAKU_BLOCK_NO_HYOUJI_JUN, ")
                .AppendLine("     @ShisakuBlockNo, ")
                .AppendLine("     RIGHT('000'+ ")
                .AppendLine("         CONVERT(VARCHAR(3),CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))+0) ")
                .AppendLine("     ,3) AS SHISAKU_BLOCK_NO_KAITEI_NO,  ")
                .AppendLine("     BLOCK_FUYOU, ")
                .AppendLine("     JYOUTAI, ")
                .AppendLine("     UNIT_KBN, ")
                .AppendLine("     SHISAKU_BLOCK_NAME, ")
                .AppendLine("     USER_ID, ")
                .AppendLine("     TEL_NO, ")
                .AppendLine("     SAISYU_KOUSHINBI, ")
                .AppendLine("     SAISYU_KOUSHINJIKAN, ")
                .AppendLine("     MEMO, ")
                .AppendLine("     TANTO_SYOUNIN_JYOUTAI, ")
                .AppendLine("     TANTO_SYOUNIN_KA, ")
                .AppendLine("     TANTO_SYOUNIN_SYA, ")
                .AppendLine("     TANTO_SYOUNIN_HI, ")
                .AppendLine("     TANTO_SYOUNIN_JIKAN, ")
                .AppendLine("     KACHOU_SYOUNIN_JYOUTAI, ")
                .AppendLine("     KACHOU_SYOUNIN_KA, ")
                .AppendLine("     KACHOU_SYOUNIN_SYA, ")
                .AppendLine("     KACHOU_SYOUNIN_HI, ")
                .AppendLine("     KACHOU_SYOUNIN_JIKAN, ")
                .AppendLine("     KAITEI_HANDAN_FLG, ")
                .AppendLine("     0, ")
                .AppendLine("     @CreatedUserId, ")
                .AppendLine("     @CreatedDate, ")
                .AppendLine("     @CreatedTime, ")
                .AppendLine("     @UpdatedUserId, ")
                .AppendLine("     @UpdatedDate, ")
                .AppendLine("     @UpdatedTime ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK BLOCK WITH (NOLOCK, NOWAIT) ")
                .AppendLine("  WHERE SHISAKU_EVENT_CODE=@ShisakuEventCode ")
                .AppendLine("    AND SHISAKU_BUKA_CODE=@ShisakuBukaCode ")
                .AppendLine("    AND SHISAKU_BLOCK_NO=@ShisakuBlockNo ")
                .AppendLine("    AND SHISAKU_BLOCK_NO_KAITEI_NO= ")
                .AppendLine("    ( ")
                .AppendLine("    SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    WHERE SHISAKU_EVENT_CODE = BLOCK.SHISAKU_EVENT_CODE ")
                .AppendLine("    AND SHISAKU_BUKA_CODE=BLOCK.SHISAKU_BUKA_CODE ")
                .AppendLine("    AND SHISAKU_BLOCK_NO=BLOCK.SHISAKU_BLOCK_NO ")
                .AppendLine("  ) ")
            End With

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            param.CreatedUserId = LoginInfo.Now.UserId
            Dim aDate As New ShisakuDate
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            Dim db As New EBomDbClient
            db.Insert(sb.ToString, param)
        End Sub
        'Public Sub InsertShisakuSekkeiBlockTsuchi(ByVal shisakuEventCode As String, _
        '                              ByVal shisakuBukaCode As String, _
        '                              ByVal shisakuBlockNo As String)

        'Dim sb As New StringBuilder
        '    With sb
        '        .Remove(0, .Length)
        '        .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_TSUCHI  ")
        '        .AppendLine("  ( ")
        '        .AppendLine("     SHISAKU_EVENT_CODE, ")
        '        .AppendLine("     SHISAKU_BUKA_CODE, ")
        '        .AppendLine("     SHISAKU_BLOCK_NO_HYOUJI_JUN, ")
        '        .AppendLine("     SHISAKU_BLOCK_NO, ")
        '        .AppendLine("     SHISAKU_BLOCK_NO_KAITEI_NO, ")
        '        .AppendLine("     BLOCK_FUYOU, ")
        '        .AppendLine("     JYOUTAI, ")
        '        .AppendLine("     UNIT_KBN, ")
        '        .AppendLine("     SHISAKU_BLOCK_NAME, ")
        '        .AppendLine("     USER_ID, ")
        '        .AppendLine("     TEL_NO, ")
        '        .AppendLine("     SAISYU_KOUSHINBI, ")
        '        .AppendLine("     SAISYU_KOUSHINJIKAN, ")
        '        .AppendLine("     MEMO, ")
        '        .AppendLine("     TANTO_SYOUNIN_JYOUTAI, ")
        '        .AppendLine("     TANTO_SYOUNIN_KA, ")
        '        .AppendLine("     TANTO_SYOUNIN_SYA, ")
        '        .AppendLine("     TANTO_SYOUNIN_HI, ")
        '        .AppendLine("     TANTO_SYOUNIN_JIKAN, ")
        '        .AppendLine("     KACHOU_SYOUNIN_JYOUTAI, ")
        '        .AppendLine("     KACHOU_SYOUNIN_KA, ")
        '        .AppendLine("     KACHOU_SYOUNIN_SYA, ")
        '        .AppendLine("     KACHOU_SYOUNIN_HI, ")
        '        .AppendLine("     KACHOU_SYOUNIN_JIKAN, ")
        '        .AppendLine("     KAITEI_HANDAN_FLG, ")
        '        .AppendLine("     CREATED_USER_ID, ")
        '        .AppendLine("     CREATED_DATE, ")
        '        .AppendLine("     CREATED_TIME, ")
        '        .AppendLine("     UPDATED_USER_ID, ")
        '        .AppendLine("     UPDATED_DATE, ")
        '        .AppendLine("     UPDATED_TIME ")
        '        .AppendLine("  ) ")
        '        .AppendLine("  SELECT  ")
        '        .AppendLine("     @ShisakuEventCode, ")
        '        .AppendLine("     @ShisakuBukaCode, ")
        '        .AppendLine("     SHISAKU_BLOCK_NO_HYOUJI_JUN, ")
        '        .AppendLine("     @ShisakuBlockNo, ")
        '        .AppendLine("     RIGHT('000'+ ")
        '        .AppendLine("         CONVERT(VARCHAR(3),CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))+1) ")
        '        .AppendLine("     ,3) AS SHISAKU_BLOCK_NO_KAITEI_NO,  ")
        '        .AppendLine("     (CASE WHEN BLOCK_FUYOU=@BlockFuyou THEN BLOCK_FUYOU ELSE '0' END) AS BLOCK_FUYOU, ")
        '        .AppendLine("     @Jyoutai, ")
        '        .AppendLine("     UNIT_KBN, ")
        '        .AppendLine("     SHISAKU_BLOCK_NAME, ")
        '        .AppendLine("     USER_ID, ")
        '        .AppendLine("     TEL_NO, ")
        '        .AppendLine("     SAISYU_KOUSHINBI, ")
        '        .AppendLine("     SAISYU_KOUSHINJIKAN, ")
        '        .AppendLine("     MEMO, ")
        '        .AppendLine("     @TantoSyouninJyoutai, ")
        '        .AppendLine("     @TantoSyouninKa, ")
        '        .AppendLine("     @TantoSyouninSya, ")
        '        .AppendLine("     @TantoSyouninHi, ")
        '        .AppendLine("     @TantoSyouninJikan, ")
        '        .AppendLine("     @KachouSyouninJyoutai, ")
        '        .AppendLine("     @KachouSyouninKa, ")
        '        .AppendLine("     @KachouSyouninSya, ")
        '        .AppendLine("     @KachouSyouninHi, ")
        '        .AppendLine("     @KachouSyouninJikan, ")
        '        .AppendLine("     KAITEI_HANDAN_FLG, ")
        '        .AppendLine("     @CreatedUserId, ")
        '        .AppendLine("     @CreatedDate, ")
        '        .AppendLine("     @CreatedTime, ")
        '        .AppendLine("     @UpdatedUserId, ")
        '        .AppendLine("     @UpdatedDate, ")
        '        .AppendLine("     @UpdatedTime ")
        '        .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK BLOCK WITH (NOLOCK, NOWAIT) ")
        '        .AppendLine("  WHERE SHISAKU_EVENT_CODE=@ShisakuEventCode ")
        '        .AppendLine("    AND SHISAKU_BUKA_CODE=@ShisakuBukaCode ")
        '        .AppendLine("    AND SHISAKU_BLOCK_NO=@ShisakuBlockNo ")
        '        .AppendLine("    AND SHISAKU_BLOCK_NO_KAITEI_NO= ")
        '        .AppendLine("    ( ")
        '        .AppendLine("    SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
        '        .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) ")
        '        .AppendLine("    WHERE SHISAKU_EVENT_CODE = BLOCK.SHISAKU_EVENT_CODE ")
        '        .AppendLine("    AND SHISAKU_BUKA_CODE=BLOCK.SHISAKU_BUKA_CODE ")
        '        .AppendLine("    AND SHISAKU_BLOCK_NO=BLOCK.SHISAKU_BLOCK_NO ")
        '        .AppendLine("  ) ")
        '    End With

        'Dim param As New TShisakuSekkeiBlockVo
        '    param.ShisakuEventCode = shisakuEventCode
        '    param.ShisakuBukaCode = shisakuBukaCode
        '    param.ShisakuBlockNo = shisakuBlockNo

        '    param.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY

        '    param.Jyoutai = TShisakuSekkeiBlockVoHelper.Jyoutai.EDIT
        '    param.TantoSyouninJyoutai = ""
        '    param.TantoSyouninKa = ""
        '    param.TantoSyouninSya = ""
        '    param.TantoSyouninHi = 0
        '    param.TantoSyouninJikan = 0

        '    param.KachouSyouninJyoutai = ""
        '    param.KachouSyouninKa = ""
        '    param.KachouSyouninSya = ""
        '    param.KachouSyouninHi = 0
        '    param.KachouSyouninJikan = 0

        '    param.CreatedUserId = LoginInfo.Now.UserId
        'Dim aDate As New ShisakuDate
        '    param.CreatedDate = aDate.CurrentDateDbFormat
        '    param.CreatedTime = aDate.CurrentTimeDbFormat
        '    param.UpdatedUserId = LoginInfo.Now.UserId
        '    param.UpdatedDate = aDate.CurrentDateDbFormat
        '    param.UpdatedTime = aDate.CurrentTimeDbFormat

        'Dim db As New EBomDbClient
        '    db.Insert(sb.ToString, param)
        'End Sub

#End Region
        '-------------------------------------------------------------




        Public Sub InsertShisakuSekkeiBlock(ByVal shisakuEventCode As String, _
                                            ByVal shisakuBukaCode As String, _
                                            ByVal shisakuBlockNo As String)

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK  ")
                .AppendLine("  ( ")
                .AppendLine("     SHISAKU_EVENT_CODE, ")
                .AppendLine("     SHISAKU_BUKA_CODE, ")
                .AppendLine("     SHISAKU_BLOCK_NO_HYOUJI_JUN, ")
                .AppendLine("     SHISAKU_BLOCK_NO, ")
                .AppendLine("     SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine("     BLOCK_FUYOU, ")
                .AppendLine("     JYOUTAI, ")
                .AppendLine("     UNIT_KBN, ")
                .AppendLine("     SHISAKU_BLOCK_NAME, ")
                .AppendLine("     USER_ID, ")
                .AppendLine("     TEL_NO, ")
                .AppendLine("     SAISYU_KOUSHINBI, ")
                .AppendLine("     SAISYU_KOUSHINJIKAN, ")
                .AppendLine("     MEMO, ")
                .AppendLine("     TANTO_SYOUNIN_JYOUTAI, ")
                .AppendLine("     TANTO_SYOUNIN_KA, ")
                .AppendLine("     TANTO_SYOUNIN_SYA, ")
                .AppendLine("     TANTO_SYOUNIN_HI, ")
                .AppendLine("     TANTO_SYOUNIN_JIKAN, ")
                .AppendLine("     KACHOU_SYOUNIN_JYOUTAI, ")
                .AppendLine("     KACHOU_SYOUNIN_KA, ")
                .AppendLine("     KACHOU_SYOUNIN_SYA, ")
                .AppendLine("     KACHOU_SYOUNIN_HI, ")
                .AppendLine("     KACHOU_SYOUNIN_JIKAN, ")
                .AppendLine("     KAITEI_HANDAN_FLG, ")
                .AppendLine("     CREATED_USER_ID, ")
                .AppendLine("     CREATED_DATE, ")
                .AppendLine("     CREATED_TIME, ")
                .AppendLine("     UPDATED_USER_ID, ")
                .AppendLine("     UPDATED_DATE, ")
                .AppendLine("     UPDATED_TIME ")
                .AppendLine("  ) ")
                .AppendLine("  SELECT  ")
                .AppendLine("     @ShisakuEventCode, ")
                .AppendLine("     @ShisakuBukaCode, ")
                .AppendLine("     SHISAKU_BLOCK_NO_HYOUJI_JUN, ")
                .AppendLine("     @ShisakuBlockNo, ")
                .AppendLine("     RIGHT('000'+ ")
                .AppendLine("         CONVERT(VARCHAR(3),CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))+1) ")
                .AppendLine("     ,3) AS SHISAKU_BLOCK_NO_KAITEI_NO,  ")
                'ブロックの初期状態を'0'で登録するように修正した（不具合管理表No17の件） 2012/02/06 
                '.AppendLine("     (CASE WHEN BLOCK_FUYOU=@BlockFuyou THEN BLOCK_FUYOU ELSE '' END) AS BLOCK_FUYOU, ")
                .AppendLine("     (CASE WHEN BLOCK_FUYOU=@BlockFuyou THEN BLOCK_FUYOU ELSE '0' END) AS BLOCK_FUYOU, ")
                .AppendLine("     @Jyoutai, ")
                .AppendLine("     UNIT_KBN, ")
                .AppendLine("     SHISAKU_BLOCK_NAME, ")
                .AppendLine("     USER_ID, ")
                .AppendLine("     TEL_NO, ")
                .AppendLine("     SAISYU_KOUSHINBI, ")
                .AppendLine("     SAISYU_KOUSHINJIKAN, ")
                .AppendLine("     MEMO, ")
                .AppendLine("     @TantoSyouninJyoutai, ")
                .AppendLine("     @TantoSyouninKa, ")
                .AppendLine("     @TantoSyouninSya, ")
                .AppendLine("     @TantoSyouninHi, ")
                .AppendLine("     @TantoSyouninJikan, ")
                .AppendLine("     @KachouSyouninJyoutai, ")
                .AppendLine("     @KachouSyouninKa, ")
                .AppendLine("     @KachouSyouninSya, ")
                .AppendLine("     @KachouSyouninHi, ")
                .AppendLine("     @KachouSyouninJikan, ")
                .AppendLine("     KAITEI_HANDAN_FLG, ")
                .AppendLine("     @CreatedUserId, ")
                .AppendLine("     @CreatedDate, ")
                .AppendLine("     @CreatedTime, ")
                .AppendLine("     @UpdatedUserId, ")
                .AppendLine("     @UpdatedDate, ")
                .AppendLine("     @UpdatedTime ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK BLOCK WITH (NOLOCK, NOWAIT) ")

                ''2015/07/16 修正 E.Ubukata Relese Ver 2.10.6
                ''バインド変数を使用すると速度が低下する（原因不明）
                '.AppendLine("  WHERE SHISAKU_EVENT_CODE=@ShisakuEventCode ")
                '.AppendLine("    AND SHISAKU_BUKA_CODE=@ShisakuBukaCode ")
                '.AppendLine("    AND SHISAKU_BLOCK_NO=@ShisakuBlockNo ")
                .AppendFormat("  WHERE SHISAKU_EVENT_CODE='{0}' ", shisakuEventCode)
                .AppendFormat("    AND SHISAKU_BUKA_CODE='{0}' ", shisakuBukaCode)
                .AppendFormat("    AND SHISAKU_BLOCK_NO='{0}' ", shisakuBlockNo)

                .AppendLine("    AND SHISAKU_BLOCK_NO_KAITEI_NO= ")
                .AppendLine("    ( ")
                .AppendLine("    SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    WHERE SHISAKU_EVENT_CODE = BLOCK.SHISAKU_EVENT_CODE ")
                .AppendLine("    AND SHISAKU_BUKA_CODE=BLOCK.SHISAKU_BUKA_CODE ")
                .AppendLine("    AND SHISAKU_BLOCK_NO=BLOCK.SHISAKU_BLOCK_NO ")
                .AppendLine("  ) ")
            End With

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            param.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY

            param.Jyoutai = TShisakuSekkeiBlockVoHelper.Jyoutai.EDIT
            param.TantoSyouninJyoutai = ""
            param.TantoSyouninKa = ""
            param.TantoSyouninSya = ""
            param.TantoSyouninHi = 0
            param.TantoSyouninJikan = 0

            param.KachouSyouninJyoutai = ""
            param.KachouSyouninKa = ""
            param.KachouSyouninSya = ""
            param.KachouSyouninHi = 0
            param.KachouSyouninJikan = 0

            param.CreatedUserId = LoginInfo.Now.UserId
            Dim aDate As New ShisakuDate
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            Dim db As New EBomDbClient
            db.Insert(sb.ToString, param)
        End Sub

        Public Sub InsertShisakuSekkeiBlockFuyou(ByVal shisakuEventCode As String, _
                                    ByVal shisakuBukaCode As String, _
                                    ByVal shisakuBlockNo As String)

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK  ")
                .AppendLine("  ( ")
                .AppendLine("     SHISAKU_EVENT_CODE, ")
                .AppendLine("     SHISAKU_BUKA_CODE, ")
                .AppendLine("     SHISAKU_BLOCK_NO_HYOUJI_JUN, ")
                .AppendLine("     SHISAKU_BLOCK_NO, ")
                .AppendLine("     SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine("     BLOCK_FUYOU, ")
                .AppendLine("     JYOUTAI, ")
                .AppendLine("     UNIT_KBN, ")
                .AppendLine("     SHISAKU_BLOCK_NAME, ")
                .AppendLine("     USER_ID, ")
                .AppendLine("     TEL_NO, ")
                .AppendLine("     SAISYU_KOUSHINBI, ")
                .AppendLine("     SAISYU_KOUSHINJIKAN, ")
                .AppendLine("     MEMO, ")
                .AppendLine("     TANTO_SYOUNIN_JYOUTAI, ")
                .AppendLine("     TANTO_SYOUNIN_KA, ")
                .AppendLine("     TANTO_SYOUNIN_SYA, ")
                .AppendLine("     TANTO_SYOUNIN_HI, ")
                .AppendLine("     TANTO_SYOUNIN_JIKAN, ")
                .AppendLine("     KACHOU_SYOUNIN_JYOUTAI, ")
                .AppendLine("     KACHOU_SYOUNIN_KA, ")
                .AppendLine("     KACHOU_SYOUNIN_SYA, ")
                .AppendLine("     KACHOU_SYOUNIN_HI, ")
                .AppendLine("     KACHOU_SYOUNIN_JIKAN, ")
                .AppendLine("     KAITEI_HANDAN_FLG, ")
                .AppendLine("     CREATED_USER_ID, ")
                .AppendLine("     CREATED_DATE, ")
                .AppendLine("     CREATED_TIME, ")
                .AppendLine("     UPDATED_USER_ID, ")
                .AppendLine("     UPDATED_DATE, ")
                .AppendLine("     UPDATED_TIME ")
                .AppendLine("  ) ")
                .AppendLine("  SELECT  ")
                .AppendLine("     @ShisakuEventCode, ")
                .AppendLine("     @ShisakuBukaCode, ")
                .AppendLine("     SHISAKU_BLOCK_NO_HYOUJI_JUN, ")
                .AppendLine("     @ShisakuBlockNo, ")
                .AppendLine("     RIGHT('000'+ ")
                .AppendLine("         CONVERT(VARCHAR(3),CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))+1) ")
                .AppendLine("     ,3) AS SHISAKU_BLOCK_NO_KAITEI_NO,  ")
                .AppendLine("     '1', ")
                .AppendLine("     @Jyoutai, ")
                .AppendLine("     UNIT_KBN, ")
                .AppendLine("     SHISAKU_BLOCK_NAME, ")
                .AppendLine("     USER_ID, ")
                .AppendLine("     TEL_NO, ")
                .AppendLine("     SAISYU_KOUSHINBI, ")
                .AppendLine("     SAISYU_KOUSHINJIKAN, ")
                .AppendLine("     MEMO, ")
                .AppendLine("     @TantoSyouninJyoutai, ")
                .AppendLine("     @TantoSyouninKa, ")
                .AppendLine("     @TantoSyouninSya, ")
                .AppendLine("     @TantoSyouninHi, ")
                .AppendLine("     @TantoSyouninJikan, ")
                .AppendLine("     @KachouSyouninJyoutai, ")
                .AppendLine("     @KachouSyouninKa, ")
                .AppendLine("     @KachouSyouninSya, ")
                .AppendLine("     @KachouSyouninHi, ")
                .AppendLine("     @KachouSyouninJikan, ")
                .AppendLine("     KAITEI_HANDAN_FLG, ")
                .AppendLine("     @CreatedUserId, ")
                .AppendLine("     @CreatedDate, ")
                .AppendLine("     @CreatedTime, ")
                .AppendLine("     @UpdatedUserId, ")
                .AppendLine("     @UpdatedDate, ")
                .AppendLine("     @UpdatedTime ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK BLOCK WITH (NOLOCK, NOWAIT) ")
                .AppendLine("  WHERE SHISAKU_EVENT_CODE=@ShisakuEventCode ")
                .AppendLine("    AND SHISAKU_BUKA_CODE=@ShisakuBukaCode ")
                .AppendLine("    AND SHISAKU_BLOCK_NO=@ShisakuBlockNo ")
                .AppendLine("    AND SHISAKU_BLOCK_NO_KAITEI_NO= ")
                .AppendLine("    ( ")
                .AppendLine("    SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    WHERE SHISAKU_EVENT_CODE = BLOCK.SHISAKU_EVENT_CODE ")
                .AppendLine("    AND SHISAKU_BUKA_CODE=BLOCK.SHISAKU_BUKA_CODE ")
                .AppendLine("    AND SHISAKU_BLOCK_NO=BLOCK.SHISAKU_BLOCK_NO ")
                .AppendLine("  ) ")
            End With

            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo


            param.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY

            param.Jyoutai = TShisakuSekkeiBlockVoHelper.Jyoutai.EDIT
            param.TantoSyouninJyoutai = ""
            param.TantoSyouninKa = ""
            param.TantoSyouninSya = ""
            param.TantoSyouninHi = 0
            param.TantoSyouninJikan = 0

            param.KachouSyouninJyoutai = ""
            param.KachouSyouninKa = ""
            param.KachouSyouninSya = ""
            param.KachouSyouninHi = 0
            param.KachouSyouninJikan = 0

            param.CreatedUserId = LoginInfo.Now.UserId
            Dim aDate As New ShisakuDate
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            Dim db As New EBomDbClient
            db.Insert(sb.ToString, param)
        End Sub

        Public Sub InsertShisakuSekkeiBlockSuobi(ByVal shisakuEventCode As String, _
                                            ByVal shisakuBukaCode As String, _
                                            ByVal shisakuBlockNo As String)
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_SOUBI  ")
                .AppendLine("  ( ")
                .AppendLine("     SHISAKU_EVENT_CODE, ")
                .AppendLine("     SHISAKU_BUKA_CODE, ")
                .AppendLine("     SHISAKU_BLOCK_NO, ")
                .AppendLine("     SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine("     SHISAKU_SOUBI_HYOUJI_JUN, ")
                .AppendLine("     CREATED_USER_ID, ")
                .AppendLine("     CREATED_DATE, ")
                .AppendLine("     CREATED_TIME, ")
                .AppendLine("     UPDATED_USER_ID, ")
                .AppendLine("     UPDATED_DATE, ")
                .AppendLine("     UPDATED_TIME ")
                .AppendLine("  ) ")
                .AppendLine("  SELECT  ")
                .AppendLine("     @ShisakuEventCode, ")
                .AppendLine("     @ShisakuBukaCode, ")
                .AppendLine("     @ShisakuBlockNo, ")
                .AppendLine("     RIGHT('000'+ ")
                .AppendLine("         CONVERT(VARCHAR(3),CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))+1) ")
                .AppendLine("     ,3) AS SHISAKU_BLOCK_NO_KAITEI_NO,  ")
                .AppendLine("     SHISAKU_SOUBI_HYOUJI_JUN, ")
                .AppendLine("     @CreatedUserId, ")
                .AppendLine("     @CreatedDate, ")
                .AppendLine("     @CreatedTime, ")
                .AppendLine("     @UpdatedUserId, ")
                .AppendLine("     @UpdatedDate, ")
                .AppendLine("     @UpdatedTime ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_SOUBI BLOCK_SOUBI WITH (NOLOCK, NOWAIT) ")

                ''2015/07/16 修正 E.Ubukata Relese Ver 2.10.6
                ''バインド変数を使用すると速度が低下する（原因不明）
                '.AppendLine("  WHERE SHISAKU_EVENT_CODE=@ShisakuEventCode ")
                '.AppendLine("    AND SHISAKU_BUKA_CODE=@ShisakuBukaCode ")
                '.AppendLine("    AND SHISAKU_BLOCK_NO=@ShisakuBlockNo ")
                .AppendFormat("  WHERE SHISAKU_EVENT_CODE='{0}' ", shisakuEventCode)
                .AppendFormat("    AND SHISAKU_BUKA_CODE='{0}' ", shisakuBukaCode)
                .AppendFormat("    AND SHISAKU_BLOCK_NO='{0}' ", shisakuBlockNo)

                .AppendLine("    AND SHISAKU_BLOCK_NO_KAITEI_NO= ")
                .AppendLine("    ( ")
                .AppendLine("    SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_SOUBI WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    WHERE SHISAKU_EVENT_CODE = BLOCK_SOUBI.SHISAKU_EVENT_CODE ")
                .AppendLine("    AND SHISAKU_BUKA_CODE=BLOCK_SOUBI.SHISAKU_BUKA_CODE ")
                .AppendLine("    AND SHISAKU_BLOCK_NO=BLOCK_SOUBI.SHISAKU_BLOCK_NO ")
                .AppendLine("  ) ")
            End With

            Dim param As New TShisakuSekkeiBlockSoubiVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            param.CreatedUserId = LoginInfo.Now.UserId
            Dim aDate As New ShisakuDate
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            Dim db As New EBomDbClient
            db.Insert(sb.ToString, param)
        End Sub

        Public Sub InsertShisakuSekkeiBlockSuobiShiyou(ByVal shisakuEventCode As String, _
                                                       ByVal shisakuBukaCode As String, _
                                                       ByVal shisakuBlockNo As String)
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_SOUBI_SHIYOU  ")
                .AppendLine("  ( ")
                .AppendLine("     SHISAKU_EVENT_CODE, ")
                .AppendLine("     SHISAKU_BUKA_CODE, ")
                .AppendLine("     SHISAKU_BLOCK_NO, ")
                .AppendLine("     SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine(" SHISAKU_SOUBI_HYOUJI_JUN, ")
                .AppendLine("     SHISAKU_SOUBI_KBN, ")
                .AppendLine("     SHISAKU_RETU_KOUMOKU_CODE, ")
                .AppendLine("     CREATED_USER_ID, ")
                .AppendLine("     CREATED_DATE, ")
                .AppendLine("     CREATED_TIME, ")
                .AppendLine("     UPDATED_USER_ID, ")
                .AppendLine("     UPDATED_DATE, ")
                .AppendLine("     UPDATED_TIME ")
                .AppendLine("  ) ")
                .AppendLine("  SELECT  ")
                .AppendLine("     @ShisakuEventCode, ")
                .AppendLine("     @ShisakuBukaCode, ")
                .AppendLine("     @ShisakuBlockNo, ")
                .AppendLine("     RIGHT('000'+ ")
                .AppendLine("         CONVERT(VARCHAR(3),CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))+1) ")
                .AppendLine("     ,3) AS SHISAKU_BLOCK_NO_KAITEI_NO,  ")
                '2012/03/07 何故０固定？'
                '.AppendLine(" @ShisakuSoubiHyoujiJun, ")
                .AppendLine(" SHISAKU_SOUBI_HYOUJI_JUN, ")
                .AppendLine("     SHISAKU_SOUBI_KBN, ")
                .AppendLine("     SHISAKU_RETU_KOUMOKU_CODE, ")
                .AppendLine("     @CreatedUserId, ")
                .AppendLine("     @CreatedDate, ")
                .AppendLine("     @CreatedTime, ")
                .AppendLine("     @UpdatedUserId, ")
                .AppendLine("     @UpdatedDate, ")
                .AppendLine("     @UpdatedTime ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_SOUBI_SHIYOU BLOCK_SOUBI_SHIYOU WITH (NOLOCK, NOWAIT) ")

                ''2015/07/16 修正 E.Ubukata Relese Ver 2.10.6
                ''バインド変数を使用すると速度が低下する（原因不明）
                '.AppendLine("  WHERE SHISAKU_EVENT_CODE=@ShisakuEventCode ")
                '.AppendLine("    AND SHISAKU_BUKA_CODE=@ShisakuBukaCode ")
                '.AppendLine("    AND SHISAKU_BLOCK_NO=@ShisakuBlockNo ")
                .AppendFormat("  WHERE SHISAKU_EVENT_CODE='{0}' ", shisakuEventCode)
                .AppendFormat("    AND SHISAKU_BUKA_CODE='{0}' ", shisakuBukaCode)
                .AppendFormat("    AND SHISAKU_BLOCK_NO='{0}' ", shisakuBlockNo)

                .AppendLine("    AND SHISAKU_BLOCK_NO_KAITEI_NO= ")
                .AppendLine("    ( ")
                .AppendLine("    SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_SOUBI_SHIYOU WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    WHERE SHISAKU_EVENT_CODE = BLOCK_SOUBI_SHIYOU.SHISAKU_EVENT_CODE ")
                .AppendLine("    AND SHISAKU_BUKA_CODE=BLOCK_SOUBI_SHIYOU.SHISAKU_BUKA_CODE ")
                .AppendLine("    AND SHISAKU_BLOCK_NO=BLOCK_SOUBI_SHIYOU.SHISAKU_BLOCK_NO ")
                .AppendLine("  ) ")
            End With

            Dim param As New TShisakuSekkeiBlockSoubiShiyouVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            'param.ShisakuSoubiHyoujiJun = 0

            param.CreatedUserId = LoginInfo.Now.UserId
            Dim aDate As New ShisakuDate
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            Dim db As New EBomDbClient
            db.Insert(sb.ToString, param)
        End Sub

        Public Sub InsertShisakuSekkeiBlockMemo(ByVal shisakuEventCode As String, _
                                            ByVal shisakuBukaCode As String, _
                                            ByVal shisakuBlockNo As String)
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_MEMO  ")
                .AppendLine("  ( ")
                .AppendLine("     SHISAKU_EVENT_CODE, ")
                .AppendLine("     SHISAKU_BUKA_CODE, ")
                .AppendLine("     SHISAKU_BLOCK_NO, ")
                .AppendLine("     SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine("     SHISAKU_GOUSYA, ")
                .AppendLine("     SHISAKU_MEMO_HYOUJI_JUN, ")
                .AppendLine("     SHISAKU_MEMO, ")
                .AppendLine("     SHISAKU_TEKIYOU, ")
                .AppendLine("     CREATED_USER_ID, ")
                .AppendLine("     CREATED_DATE, ")
                .AppendLine("     CREATED_TIME, ")
                .AppendLine("     UPDATED_USER_ID, ")
                .AppendLine("     UPDATED_DATE, ")
                .AppendLine("     UPDATED_TIME ")
                .AppendLine("  ) ")
                .AppendLine("  SELECT  ")
                .AppendLine("     @ShisakuEventCode, ")
                .AppendLine("     @ShisakuBukaCode, ")
                .AppendLine("     @ShisakuBlockNo, ")
                .AppendLine("     RIGHT('000'+ ")
                .AppendLine("         CONVERT(VARCHAR(3),CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))+1) ")
                .AppendLine("     ,3) AS SHISAKU_BLOCK_NO_KAITEI_NO,  ")
                .AppendLine("     SHISAKU_GOUSYA, ")
                .AppendLine("     SHISAKU_MEMO_HYOUJI_JUN, ")
                .AppendLine("     SHISAKU_MEMO, ")
                .AppendLine("     SHISAKU_TEKIYOU, ")
                .AppendLine("     @CreatedUserId, ")
                .AppendLine("     @CreatedDate, ")
                .AppendLine("     @CreatedTime, ")
                .AppendLine("     @UpdatedUserId, ")
                .AppendLine("     @UpdatedDate, ")
                .AppendLine("     @UpdatedTime ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_MEMO BLOCK_MEMO WITH (NOLOCK, NOWAIT) ")

                ''2015/07/16 修正 E.Ubukata Relese Ver 2.10.6
                ''バインド変数を使用すると速度が低下する（原因不明）
                '.AppendLine("  WHERE SHISAKU_EVENT_CODE=@ShisakuEventCode ")
                '.AppendLine("    AND SHISAKU_BUKA_CODE=@ShisakuBukaCode ")
                '.AppendLine("    AND SHISAKU_BLOCK_NO=@ShisakuBlockNo ")
                .AppendFormat("  WHERE SHISAKU_EVENT_CODE='{0}' ", shisakuEventCode)
                .AppendFormat("    AND SHISAKU_BUKA_CODE='{0}' ", shisakuBukaCode)
                .AppendFormat("    AND SHISAKU_BLOCK_NO='{0}' ", shisakuBlockNo)

                .AppendLine("    AND SHISAKU_BLOCK_NO_KAITEI_NO= ")
                .AppendLine("    ( ")
                .AppendLine("    SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_MEMO WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    WHERE SHISAKU_EVENT_CODE = BLOCK_MEMO.SHISAKU_EVENT_CODE ")
                .AppendLine("    AND SHISAKU_BUKA_CODE=BLOCK_MEMO.SHISAKU_BUKA_CODE ")
                .AppendLine("    AND SHISAKU_BLOCK_NO=BLOCK_MEMO.SHISAKU_BLOCK_NO ")
                .AppendLine("    ) ")
            End With

            Dim param As New TShisakuSekkeiBlockMemoVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            param.CreatedUserId = LoginInfo.Now.UserId
            Dim aDate As New ShisakuDate
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            Dim db As New EBomDbClient
            db.Insert(sb.ToString, param)
        End Sub

        Public Sub InsertShisakuSekkeiBlockInstl(ByVal shisakuEventCode As String, _
                                            ByVal shisakuBukaCode As String, _
                                            ByVal shisakuBlockNo As String)
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL  ")
                .AppendLine("  ( ")
                .AppendLine("     SHISAKU_EVENT_CODE, ")
                .AppendLine("     SHISAKU_BUKA_CODE, ")
                .AppendLine("     SHISAKU_BLOCK_NO, ")
                .AppendLine("     SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine("     SHISAKU_GOUSYA, ")
                .AppendLine("     INSTL_HINBAN_HYOUJI_JUN, ")
                .AppendLine("     INSTL_HINBAN, ")
                .AppendLine("     INSTL_HINBAN_KBN, ")
                '2014/09/23 酒井 ADD BEGIN
                .AppendLine("     INSTL_DATA_KBN, ")
                .AppendLine("     BASE_INSTL_FLG, ")
                '2014/09/23 酒井 ADD END
                .AppendLine("     BF_BUHIN_NO, ")
                .AppendLine("     INSU_SURYO, ")
                .AppendLine("     SAISYU_KOUSHINBI, ")
                .AppendLine("     CREATED_USER_ID, ")
                .AppendLine("     CREATED_DATE, ")
                .AppendLine("     CREATED_TIME, ")
                .AppendLine("     UPDATED_USER_ID, ")
                .AppendLine("     UPDATED_DATE, ")
                .AppendLine("     UPDATED_TIME ")
                .AppendLine("  ) ")
                .AppendLine("  SELECT  ")
                .AppendLine("     @ShisakuEventCode, ")
                .AppendLine("     @ShisakuBukaCode, ")
                .AppendLine("     @ShisakuBlockNo, ")
                .AppendLine("     RIGHT('000'+ ")
                .AppendLine("         CONVERT(VARCHAR(3),CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))+1) ")
                .AppendLine("     ,3) AS SHISAKU_BLOCK_NO_KAITEI_NO,  ")
                .AppendLine("     SHISAKU_GOUSYA, ")
                .AppendLine("     INSTL_HINBAN_HYOUJI_JUN, ")
                .AppendLine("     INSTL_HINBAN, ")
                .AppendLine("     INSTL_HINBAN_KBN, ")
                '2014/09/23 酒井 ADD BEGIN
                .AppendLine("     INSTL_DATA_KBN, ")
                .AppendLine("     BASE_INSTL_FLG, ")
                '2014/09/23 酒井 ADD END
                .AppendLine("     BF_BUHIN_NO, ")
                .AppendLine("     INSU_SURYO, ")
                .AppendLine("     SAISYU_KOUSHINBI, ")
                .AppendLine("     @CreatedUserId, ")
                .AppendLine("     @CreatedDate, ")
                .AppendLine("     @CreatedTime, ")
                .AppendLine("     @UpdatedUserId, ")
                .AppendLine("     @UpdatedDate, ")
                .AppendLine("     @UpdatedTime ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL BLOCK_INSTL WITH (NOLOCK, NOWAIT) ")

                ''2015/07/16 修正 E.Ubukata Relese Ver 2.10.6
                ''バインド変数を使用すると速度が低下する（原因不明）
                '.AppendLine("  WHERE SHISAKU_EVENT_CODE=@ShisakuEventCode ")
                '.AppendLine("    AND SHISAKU_BUKA_CODE=@ShisakuBukaCode ")
                '.AppendLine("    AND SHISAKU_BLOCK_NO=@ShisakuBlockNo ")
                .AppendFormat("  WHERE SHISAKU_EVENT_CODE='{0}' ", shisakuEventCode)
                .AppendFormat("    AND SHISAKU_BUKA_CODE='{0}' ", shisakuBukaCode)
                .AppendFormat("    AND SHISAKU_BLOCK_NO='{0}' ", shisakuBlockNo)

                .AppendLine(" AND NOT SHISAKU_BLOCK_NO_KAITEI_NO = '  0' ")
                .AppendLine("    AND SHISAKU_BLOCK_NO_KAITEI_NO= ")
                .AppendLine("    ( ")
                .AppendLine("    SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    WHERE SHISAKU_EVENT_CODE = BLOCK_INSTL.SHISAKU_EVENT_CODE ")
                .AppendLine("    AND SHISAKU_BUKA_CODE=BLOCK_INSTL.SHISAKU_BUKA_CODE ")
                .AppendLine("    AND SHISAKU_BLOCK_NO=BLOCK_INSTL.SHISAKU_BLOCK_NO ")
                .AppendLine("    ) ")

            End With

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            param.CreatedUserId = LoginInfo.Now.UserId
            Dim aDate As New ShisakuDate
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            Dim db As New EBomDbClient
            db.Insert(sb.ToString, param)
        End Sub

        Public Sub InsertShisakuBuhinKousei(ByVal shisakuEventCode As String, _
                                            ByVal shisakuBukaCode As String, _
                                            ByVal shisakuBlockNo As String)
            Dim sql As String = _
             " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_KOUSEI  " _
            & "  ( " _
            & "     SHISAKU_EVENT_CODE, " _
            & "     SAKUSEI_COUNT, " _
            & "     SHISAKU_BUKA_CODE, " _
            & "     SHISAKU_BLOCK_NO, " _
            & "     SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & "     SHISAKU_GOUSYA, " _
            & "     INSTL_HINBAN, " _
            & "     INSTL_HINBAN_KBN, " _
            & "     BUHIN_NO_OYA, " _
            & "     BUHIN_NO_KBN_OYA, " _
            & "     BUHIN_NO_KO, " _
            & "     BUHIN_NO_KBN_KO, " _
            & "     KAITEI_NO, " _
            & "     ZUMEN_NO, " _
            & "     MIDASHI_NO, " _
            & "     MIDASHI_NO_SHURUI, " _
            & "     MIDASHI_NO_HOJO, " _
            & "     INSU_SURYO, " _
            & "     SHUKEI_CODE, " _
            & "     SIA_SHUKEI_CODE, " _
            & "     GENCYO_CKD_KBN, " _
            & "     SHONIN_KBN, " _
            & "     SAIYO_DATE, " _
            & "     HAISI_DATE, " _
            & "     SASHIMODOSHI_DATE, " _
            & "     CREATED_USER_ID, " _
            & "     CREATED_DATE, " _
            & "     CREATED_TIME, " _
            & "     UPDATED_USER_ID, " _
            & "     UPDATED_DATE, " _
            & "     UPDATED_TIME " _
            & "  ) " _
            & "  SELECT  " _
            & "     @ShisakuEventCode, " _
            & "     SAKUSEI_COUNT, " _
            & "     @ShisakuBukaCode, " _
            & "     @ShisakuBlockNo, " _
            & "     RIGHT('000'+ " _
            & "         CONVERT(VARCHAR(3),CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))+1) " _
            & "     ,3) AS SHISAKU_BLOCK_NO_KAITEI_NO,  " _
            & "     SHISAKU_GOUSYA, " _
            & "     INSTL_HINBAN, " _
            & "     INSTL_HINBAN_KBN, " _
            & "     BUHIN_NO_OYA, " _
            & "     BUHIN_NO_KBN_OYA, " _
            & "     BUHIN_NO_KO, " _
            & "     BUHIN_NO_KBN_KO, " _
            & "     KAITEI_NO, " _
            & "     ZUMEN_NO, " _
            & "     MIDASHI_NO, " _
            & "     MIDASHI_NO_SHURUI, " _
            & "     MIDASHI_NO_HOJO, " _
            & "     INSU_SURYO, " _
            & "     SHUKEI_CODE, " _
            & "     SIA_SHUKEI_CODE, " _
            & "     GENCYO_CKD_KBN, " _
            & "     SHONIN_KBN, " _
            & "     SAIYO_DATE, " _
            & "     HAISI_DATE, " _
            & "     SASHIMODOSHI_DATE, " _
            & "     @CreatedUserId, " _
            & "     @CreatedDate, " _
            & "     @CreatedTime, " _
            & "     @UpdatedUserId, " _
            & "     @UpdatedDate, " _
            & "     @UpdatedTime " _
            & "  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_KOUSEI BUHIN_KOUSEI WITH (NOLOCK, NOWAIT) " _
            & "  WHERE SHISAKU_EVENT_CODE=@ShisakuEventCode " _
            & "    AND SHISAKU_BUKA_CODE=@ShisakuBukaCode " _
            & "    AND SHISAKU_BLOCK_NO=@ShisakuBlockNo " _
            & "    AND SHISAKU_BLOCK_NO_KAITEI_NO= " _
            & "    ( " _
            & "    SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
            & "    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_KOUSEI WITH (NOLOCK, NOWAIT) " _
            & "    WHERE SHISAKU_EVENT_CODE = BUHIN_KOUSEI.SHISAKU_EVENT_CODE " _
            & "    AND SHISAKU_BUKA_CODE=BUHIN_KOUSEI.SHISAKU_BUKA_CODE " _
            & "    AND SHISAKU_BLOCK_NO=BUHIN_KOUSEI.SHISAKU_BLOCK_NO " _
            & "    ) "

            Dim param As New TShisakuBuhinKouseiVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            param.CreatedUserId = LoginInfo.Now.UserId
            Dim aDate As New ShisakuDate
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            Dim db As New EBomDbClient
            db.Insert(sql, param)
        End Sub

        Public Sub InsertShisakuBuhin(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String)
            Dim sql As String = _
             " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN  " _
            & "  ( " _
            & "     SHISAKU_EVENT_CODE, " _
            & "     SHISAKU_BUKA_CODE, " _
            & "     SHISAKU_BLOCK_NO, " _
            & "     SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & "     SHISAKU_GOUSYA, " _
            & "     BUHIN_NO, " _
            & "     BUHIN_NO_KBN, " _
            & "     BUHIN_NO_KAITEI_NO, " _
            & "     EDA_BAN, " _
            & "     SEKKEI_SHAIN_NO, " _
            & "     MAKER_CODE, " _
            & "     SIA_MAKER_CODE, " _
            & "     TANTO_CODE, " _
            & "     SEKININ_BUKA_CODE, " _
            & "     SEKININ_SITE_KBN, " _
            & "     ZUMEN_NO, " _
            & "     ZUMEN_KAITEI_NO, " _
            & "     SHONIN_SIGN, " _
            & "     SHONIN_DATE, " _
            & "     HINMOKU_NO, " _
            & "     BUHIN_NAME, " _
            & "     BUHIN_KANA_NAME, " _
            & "     HOJO_NAME, " _
            & "     KEISU_CODE, " _
            & "     KYOYO_MODEL, " _
            & "     NONYU_TANI, " _
            & "     BUHIN_KIND, " _
            & "     NAISEI_KBN, " _
            & "     LOW_LEVEL, " _
            & "     BANKO_SURYO_INPUT, " _
            & "     BUHIN_SHITSURYO, " _
            & "     KINZOKU_SHITSURYO_INPUT, " _
            & "     KINZOKU_SHITSURYO, " _
            & "     BUHINHI_KINGAKU, " _
            & "     BUHIN_DATE_ID, " _
            & "     SIA_BUHINHI, " _
            & "     SIA_BUHIN_DATE_ID, " _
            & "     TENSHINZAI, " _
            & "     SIA_TENSHINZAI, " _
            & "     KATAHI_KINGAKU, " _
            & "     SIA_KATAHI_KINGAKU, " _
            & "     PALLET, " _
            & "     SIA_PALLET, " _
            & "     KAIHATSUHI, " _
            & "     SIA_KAIHATSUHI, " _
            & "     BANKO_SURYO, " _
            & "     SHUKEI_CODE, " _
            & "     SIA_SHUKEI_CODE, " _
            & "     GENCYO_CKD_KBN, " _
            & "     SHUKEI_BUKA_CODE, " _
            & "     ZUMEN_OVER, " _
            & "     ZUMEN_COLUMN, " _
            & "     TEKIYO_KBN, " _
            & "     CHUKI_KIJUTSU, " _
            & "     NAIGAISO_KBN, " _
            & "     ZAIRYO_KIJUTSU, " _
            & "     SEIHO_KIJUTU, " _
            & "     DSGN_MEMO, " _
            & "     FINAL_KBN, " _
            & "     JUYO_HOAN_KBN, " _
            & "     JUYO_HOAN_CODE, " _
            & "     HOYOHIN_CODE, " _
            & "     RECYCLE_MARK, " _
            & "     HOKISEI_CODE, " _
            & "     SAIYO_DATE, " _
            & "     HAISI_DATE, " _
            & "     SIRIES_CODE, " _
            & "     HYOMEN_SHORI, " _
            & "     SEISAN_KBN, " _
            & "     BUHIN_STATUS_KBN, " _
            & "     SANKO_HYOJI_CODE, " _
            & "     SHUTUZU_YOTEI_DATE, " _
            & "     STATUS, " _
            & "     UPDATE_KBN, " _
            & "     BUHIN_ITEM_NAME, " _
            & "     HENDO_SHUBETSU, " _
            & "     KONPO_YUSOHI, " _
            & "     BENCHI_BUHINHI, " _
            & "     SIA_BENCHI_BUHINHI, " _
            & "     BENCHI_KATAHI, " _
            & "     SIA_BENCHI_KATAHI, " _
            & "     KOKUNAI_COST_KBN, " _
            & "     KAIGAI_COST_KBN, " _
            & "     SAISHIYOUFUKA, " _
            & "     ZAISHITU_KIKAKU_1, " _
            & "     ZAISHITU_KIKAKU_2, " _
            & "     ZAISHITU_KIKAKU_3, " _
            & "     ZAISHITU_MEKKI, " _
            & "     SHISAKU_BANKO_SURYO, " _
            & "     SHISAKU_BANKO_SURYO_U, " _
            & "     SHISAKU_BUHINN_HI, " _
            & "     SHISAKU_KATA_HI, " _
            & "     BIKOU, " _
            & "     HENSYU_TOUROKUBI, " _
            & "     HENSYU_TOUROKUJIKAN, " _
            & "     KAITEI_HANDAN_FLG, " _
            & "     SHISAKU_LIST_CODE," _
            & "     CREATED_USER_ID, " _
            & "     CREATED_DATE, " _
            & "     CREATED_TIME, " _
            & "     UPDATED_USER_ID, " _
            & "     UPDATED_DATE, " _
            & "     UPDATED_TIME " _
            & "  ) " _
            & "  SELECT  " _
            & "     @ShisakuEventCode, " _
            & "     @ShisakuBukaCode, " _
            & "     @ShisakuBlockNo, " _
            & "     RIGHT('000'+ " _
            & "         CONVERT(VARCHAR(3),CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))+1) " _
            & "     ,3) AS SHISAKU_BLOCK_NO_KAITEI_NO,  " _
            & "     SHISAKU_GOUSYA, " _
            & "     BUHIN_NO, " _
            & "     BUHIN_NO_KBN, " _
            & "     BUHIN_NO_KAITEI_NO, " _
            & "     EDA_BAN, " _
            & "     SEKKEI_SHAIN_NO, " _
            & "     MAKER_CODE, " _
            & "     SIA_MAKER_CODE, " _
            & "     TANTO_CODE, " _
            & "     SEKININ_BUKA_CODE, " _
            & "     SEKININ_SITE_KBN, " _
            & "     ZUMEN_NO, " _
            & "     ZUMEN_KAITEI_NO, " _
            & "     SHONIN_SIGN, " _
            & "     SHONIN_DATE, " _
            & "     HINMOKU_NO, " _
            & "     BUHIN_NAME, " _
            & "     BUHIN_KANA_NAME, " _
            & "     HOJO_NAME, " _
            & "     KEISU_CODE, " _
            & "     KYOYO_MODEL, " _
            & "     NONYU_TANI, " _
            & "     BUHIN_KIND, " _
            & "     NAISEI_KBN, " _
            & "     LOW_LEVEL, " _
            & "     BANKO_SURYO_INPUT, " _
            & "     BUHIN_SHITSURYO, " _
            & "     KINZOKU_SHITSURYO_INPUT, " _
            & "     KINZOKU_SHITSURYO, " _
            & "     BUHINHI_KINGAKU, " _
            & "     BUHIN_DATE_ID, " _
            & "     SIA_BUHINHI, " _
            & "     SIA_BUHIN_DATE_ID, " _
            & "     TENSHINZAI, " _
            & "     SIA_TENSHINZAI, " _
            & "     KATAHI_KINGAKU, " _
            & "     SIA_KATAHI_KINGAKU, " _
            & "     PALLET, " _
            & "     SIA_PALLET, " _
            & "     KAIHATSUHI, " _
            & "     SIA_KAIHATSUHI, " _
            & "     BANKO_SURYO, " _
            & "     SHUKEI_CODE, " _
            & "     SIA_SHUKEI_CODE, " _
            & "     GENCYO_CKD_KBN, " _
            & "     SHUKEI_BUKA_CODE, " _
            & "     ZUMEN_OVER, " _
            & "     ZUMEN_COLUMN, " _
            & "     TEKIYO_KBN, " _
            & "     CHUKI_KIJUTSU, " _
            & "     NAIGAISO_KBN, " _
            & "     ZAIRYO_KIJUTSU, " _
            & "     SEIHO_KIJUTU, " _
            & "     DSGN_MEMO, " _
            & "     FINAL_KBN, " _
            & "     JUYO_HOAN_KBN, " _
            & "     JUYO_HOAN_CODE, " _
            & "     HOYOHIN_CODE, " _
            & "     RECYCLE_MARK, " _
            & "     HOKISEI_CODE, " _
            & "     SAIYO_DATE, " _
            & "     HAISI_DATE, " _
            & "     SIRIES_CODE, " _
            & "     HYOMEN_SHORI, " _
            & "     SEISAN_KBN, " _
            & "     BUHIN_STATUS_KBN, " _
            & "     SANKO_HYOJI_CODE, " _
            & "     SHUTUZU_YOTEI_DATE, " _
            & "     STATUS, " _
            & "     UPDATE_KBN, " _
            & "     BUHIN_ITEM_NAME, " _
            & "     HENDO_SHUBETSU, " _
            & "     KONPO_YUSOHI, " _
            & "     BENCHI_BUHINHI, " _
            & "     SIA_BENCHI_BUHINHI, " _
            & "     BENCHI_KATAHI, " _
            & "     SIA_BENCHI_KATAHI, " _
            & "     KOKUNAI_COST_KBN, " _
            & "     KAIGAI_COST_KBN, " _
            & "     SAISHIYOUFUKA, " _
            & "     ZAISHITU_KIKAKU_1, " _
            & "     ZAISHITU_KIKAKU_2, " _
            & "     ZAISHITU_KIKAKU_3, " _
            & "     ZAISHITU_MEKKI, " _
            & "     SHISAKU_BANKO_SURYO, " _
            & "     SHISAKU_BANKO_SURYO_U, " _
            & "     SHISAKU_BUHINN_HI, " _
            & "     SHISAKU_KATA_HI, " _
            & "     BIKOU, " _
            & "     HENSYU_TOUROKUBI, " _
            & "     HENSYU_TOUROKUJIKAN, " _
            & "     KAITEI_HANDAN_FLG, " _
            & "     SHISAKU_LIST_CODE, " _
            & "     @CreatedUserId, " _
            & "     @CreatedDate, " _
            & "     @CreatedTime, " _
            & "     @UpdatedUserId, " _
            & "     @UpdatedDate, " _
            & "     @UpdatedTime " _
            & "  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN BUHIN WITH (NOLOCK, NOWAIT) " _
            & "  WHERE SHISAKU_EVENT_CODE=@ShisakuEventCode " _
            & "    AND SHISAKU_BUKA_CODE=@ShisakuBukaCode " _
            & "    AND SHISAKU_BLOCK_NO=@ShisakuBlockNo " _
            & "    AND SHISAKU_BLOCK_NO_KAITEI_NO= " _
            & "    ( " _
            & "    SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
            & "    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN WITH (NOLOCK, NOWAIT) " _
            & "    WHERE SHISAKU_EVENT_CODE = BUHIN.SHISAKU_EVENT_CODE " _
            & "    AND SHISAKU_BUKA_CODE=BUHIN.SHISAKU_BUKA_CODE " _
            & "    AND SHISAKU_BLOCK_NO=BUHIN.SHISAKU_BLOCK_NO " _
            & "    ) "

            Dim param As New TShisakuBuhinVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            param.CreatedUserId = LoginInfo.Now.UserId
            Dim aDate As New ShisakuDate
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            Dim db As New EBomDbClient
            db.Insert(sql, param)
        End Sub
        '仕様変更により試作部品編集情報の改訂を追加'
        '2012/01/23 供給セクション追加
        '2012/01/25 部品ノート追加
        Public Sub InsertShisakuBuhinEdit(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String)
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT ")
                .AppendLine("  ( ")
                .AppendLine("     SHISAKU_EVENT_CODE, ")
                .AppendLine("     SHISAKU_BUKA_CODE, ")
                .AppendLine("     SHISAKU_BLOCK_NO, ")
                .AppendLine("     SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine("     BUHIN_NO_HYOUJI_JUN, ")
                .AppendLine("     LEVEL, ")
                .AppendLine("     SHUKEI_CODE, ")
                .AppendLine("     SIA_SHUKEI_CODE, ")
                .AppendLine("     GENCYO_CKD_KBN, ")
                .AppendLine("     KYOUKU_SECTION, ")
                .AppendLine("     MAKER_CODE, ")
                .AppendLine("     MAKER_NAME, ")
                .AppendLine("     BUHIN_NO, ")
                .AppendLine("     BUHIN_NO_KBN, ")
                .AppendLine("     BUHIN_NO_KAITEI_NO, ")
                .AppendLine("     EDA_BAN, ")
                .AppendLine("     BUHIN_NAME, ")
                .AppendLine("     SAISHIYOUFUKA, ")
                .AppendLine("     SHUTUZU_YOTEI_DATE, ")
                '2014/09/23 酒井 ADD BEGIN
                .AppendLine("     TSUKURIKATA_SEISAKU, ")
                .AppendLine("     TSUKURIKATA_KATASHIYOU_1, ")
                .AppendLine("     TSUKURIKATA_KATASHIYOU_2, ")
                .AppendLine("     TSUKURIKATA_KATASHIYOU_3, ")
                .AppendLine("     TSUKURIKATA_TIGU, ")
                .AppendLine("     TSUKURIKATA_NOUNYU, ")
                .AppendLine("     TSUKURIKATA_KIBO, ")
                .AppendLine("     BASE_BUHIN_FLG, ")
                '2014/09/23 酒井 ADD END
                .AppendLine("     ZAISHITU_KIKAKU_1, ")
                .AppendLine("     ZAISHITU_KIKAKU_2, ")
                .AppendLine("     ZAISHITU_KIKAKU_3, ")
                .AppendLine("     ZAISHITU_MEKKI, ")
                .AppendLine("     SHISAKU_BANKO_SURYO, ")
                .AppendLine("     SHISAKU_BANKO_SURYO_U, ")
                '↓↓↓2014/12/30 メタル項目を追加 TES)張 ADD BEGIN
                .AppendLine("     MATERIAL_INFO_LENGTH, ")
                .AppendLine("     MATERIAL_INFO_WIDTH, ")
                .AppendLine("     DATA_ITEM_KAITEI_NO, ")
                .AppendLine("     DATA_ITEM_AREA_NAME, ")
                .AppendLine("     DATA_ITEM_SET_NAME, ")
                .AppendLine("     DATA_ITEM_KAITEI_INFO, ")
                '↑↑↑2014/12/30 メタル項目を追加 TES)張 ADD END
                .AppendLine("     SHISAKU_BUHIN_HI, ")
                .AppendLine("     SHISAKU_KATA_HI, ")
                .AppendLine("     BIKOU, ")
                .AppendLine("     BUHIN_NOTE, ")
                .AppendLine("     EDIT_TOUROKUBI, ")
                .AppendLine("     EDIT_TOUROKUJIKAN, ")
                .AppendLine("     KAITEI_HANDAN_FLG, ")
                .AppendLine("     SHISAKU_LIST_CODE, ")
                .AppendLine("     CREATED_USER_ID, ")
                .AppendLine("     CREATED_DATE, ")
                .AppendLine("     CREATED_TIME, ")
                .AppendLine("     UPDATED_USER_ID, ")
                .AppendLine("     UPDATED_DATE, ")
                .AppendLine("     UPDATED_TIME ")

                ''2015/09/10 追加 E.Ubukata Ver2.11.0
                '' 手配帳作成時に使用するカラムを追加
                .AppendLine("     ,BASE_BUHIN_SEQ")

                .AppendLine("  ) ")
                .AppendLine("  SELECT  ")
                .AppendLine("     @ShisakuEventCode, ")
                .AppendLine("     @ShisakuBukaCode, ")
                .AppendLine("     @ShisakuBlockNo, ")
                .AppendLine("     RIGHT('000'+ ")
                .AppendLine("         CONVERT(VARCHAR(3),CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))+1) ")
                .AppendLine("     ,3) AS SHISAKU_BLOCK_NO_KAITEI_NO,  ")
                .AppendLine("     BUHIN_NO_HYOUJI_JUN, ")
                .AppendLine("     LEVEL, ")
                .AppendLine("     SHUKEI_CODE, ")
                .AppendLine("     SIA_SHUKEI_CODE, ")
                .AppendLine("     GENCYO_CKD_KBN, ")
                .AppendLine("     KYOUKU_SECTION, ")
                .AppendLine("     MAKER_CODE, ")
                .AppendLine("     MAKER_NAME, ")
                .AppendLine("     BUHIN_NO, ")
                .AppendLine("     BUHIN_NO_KBN, ")
                .AppendLine("     BUHIN_NO_KAITEI_NO, ")
                .AppendLine("     EDA_BAN, ")
                .AppendLine("     BUHIN_NAME, ")
                .AppendLine("     SAISHIYOUFUKA, ")
                .AppendLine("     SHUTUZU_YOTEI_DATE, ")
                '2014/09/23 酒井 ADD BEGIN
                .AppendLine("     TSUKURIKATA_SEISAKU, ")
                .AppendLine("     TSUKURIKATA_KATASHIYOU_1, ")
                .AppendLine("     TSUKURIKATA_KATASHIYOU_2, ")
                .AppendLine("     TSUKURIKATA_KATASHIYOU_3, ")
                .AppendLine("     TSUKURIKATA_TIGU, ")
                .AppendLine("     TSUKURIKATA_NOUNYU, ")
                .AppendLine("     TSUKURIKATA_KIBO, ")
                .AppendLine("     BASE_BUHIN_FLG, ")
                '2014/09/23 酒井 ADD END
                .AppendLine("     ZAISHITU_KIKAKU_1, ")
                .AppendLine("     ZAISHITU_KIKAKU_2, ")
                .AppendLine("     ZAISHITU_KIKAKU_3, ")
                .AppendLine("     ZAISHITU_MEKKI, ")
                .AppendLine("     SHISAKU_BANKO_SURYO, ")
                .AppendLine("     SHISAKU_BANKO_SURYO_U, ")
                '↓↓↓2014/12/30 メタル項目を追加 TES)張 ADD BEGIN
                .AppendLine("     MATERIAL_INFO_LENGTH, ")
                .AppendLine("     MATERIAL_INFO_WIDTH, ")
                .AppendLine("     DATA_ITEM_KAITEI_NO, ")
                .AppendLine("     DATA_ITEM_AREA_NAME, ")
                .AppendLine("     DATA_ITEM_SET_NAME, ")
                .AppendLine("     DATA_ITEM_KAITEI_INFO, ")
                '↑↑↑2014/12/30 メタル項目を追加 TES)張 ADD END
                .AppendLine("     SHISAKU_BUHIN_HI, ")
                .AppendLine("     SHISAKU_KATA_HI, ")
                .AppendLine("     BIKOU, ")
                .AppendLine("     BUHIN_NOTE, ")
                .AppendLine("     EDIT_TOUROKUBI, ")
                .AppendLine("     EDIT_TOUROKUJIKAN, ")
                .AppendLine("     KAITEI_HANDAN_FLG, ")
                .AppendLine("     SHISAKU_LIST_CODE, ")
                .AppendLine("     @CreatedUserId, ")
                .AppendLine("     @CreatedDate, ")
                .AppendLine("     @CreatedTime, ")
                .AppendLine("     @UpdatedUserId, ")
                .AppendLine("     @UpdatedDate, ")
                .AppendLine("     @UpdatedTime ")
                .AppendLine("     ,BASE_BUHIN_SEQ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT EDIT WITH (NOLOCK, NOWAIT) ")

                ''2015/07/16 修正 E.Ubukata Relese Ver 2.10.6
                ''バインド変数を使用すると速度が低下する（原因不明）
                '.AppendLine("  WHERE SHISAKU_EVENT_CODE=@ShisakuEventCode ")
                '.AppendLine("    AND SHISAKU_BUKA_CODE=@ShisakuBukaCode ")
                '.AppendLine("    AND SHISAKU_BLOCK_NO=@ShisakuBlockNo ")
                .AppendFormat("  WHERE SHISAKU_EVENT_CODE='{0}' ", shisakuEventCode)
                .AppendFormat("    AND SHISAKU_BUKA_CODE='{0}' ", shisakuBukaCode)
                .AppendFormat("    AND SHISAKU_BLOCK_NO='{0}' ", shisakuBlockNo)

                .AppendLine("    AND SHISAKU_BLOCK_NO_KAITEI_NO= ")
                .AppendLine("    ( ")
                .AppendLine("    SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    WHERE SHISAKU_EVENT_CODE = EDIT.SHISAKU_EVENT_CODE ")
                .AppendLine("    AND SHISAKU_BUKA_CODE = EDIT.SHISAKU_BUKA_CODE ")
                .AppendLine("    AND SHISAKU_BLOCK_NO = EDIT.SHISAKU_BLOCK_NO ")
                .AppendLine("  ) ")
            End With

            Dim param As New TShisakuBuhinEditVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            param.CreatedUserId = LoginInfo.Now.UserId
            Dim aDate As New ShisakuDate
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            Dim db As New EBomDbClient
            db.Insert(sb.ToString, param)
        End Sub
        '仕様変更により試作部品編集・INSTL情報の改訂を追加'
        Public Sub InsertShisakuBuhinEditInstl(ByVal shisakuEventCode As String, _
                                               ByVal shisakuBukaCode As String, _
                                               ByVal shisakuBlockNo As String)

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL ")
                .AppendLine("  ( ")
                .AppendLine("     SHISAKU_EVENT_CODE, ")
                .AppendLine("     SHISAKU_BUKA_CODE, ")
                .AppendLine("     SHISAKU_BLOCK_NO, ")
                .AppendLine("     SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine("     BUHIN_NO_HYOUJI_JUN, ")
                .AppendLine("     INSTL_HINBAN_HYOUJI_JUN, ")
                .AppendLine("     INSU_SURYO, ")
                .AppendLine("     SAISYU_KOUSHINBI, ")
                .AppendLine("     CREATED_USER_ID, ")
                .AppendLine("     CREATED_DATE, ")
                .AppendLine("     CREATED_TIME, ")
                .AppendLine("     UPDATED_USER_ID, ")
                .AppendLine("     UPDATED_DATE, ")
                .AppendLine("     UPDATED_TIME ")
                .AppendLine("  ) ")
                .AppendLine("  SELECT  ")
                .AppendLine("     @ShisakuEventCode, ")
                .AppendLine("     @ShisakuBukaCode, ")
                .AppendLine("     @ShisakuBlockNo, ")
                .AppendLine("     RIGHT('000'+ ")
                .AppendLine("         CONVERT(VARCHAR(3),CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))+1) ")
                .AppendLine("     ,3) AS SHISAKU_BLOCK_NO_KAITEI_NO,  ")
                .AppendLine("     BUHIN_NO_HYOUJI_JUN, ")
                .AppendLine("     INSTL_HINBAN_HYOUJI_JUN, ")
                .AppendLine("     INSU_SURYO, ")
                .AppendLine("     SAISYU_KOUSHINBI, ")
                .AppendLine("     @CreatedUserId, ")
                .AppendLine("     @CreatedDate, ")
                .AppendLine("     @CreatedTime, ")
                .AppendLine("     @UpdatedUserId, ")
                .AppendLine("     @UpdatedDate, ")
                .AppendLine("     @UpdatedTime ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL INSTL WITH (NOLOCK, NOWAIT) ")

                ''2015/07/16 修正 E.Ubukata Relese Ver 2.10.6
                ''バインド変数を使用すると速度が低下する（原因不明）
                '.AppendLine("  WHERE SHISAKU_EVENT_CODE=@ShisakuEventCode ")
                '.AppendLine("    AND SHISAKU_BUKA_CODE=@ShisakuBukaCode ")
                '.AppendLine("    AND SHISAKU_BLOCK_NO=@ShisakuBlockNo ")
                .AppendFormat("  WHERE SHISAKU_EVENT_CODE='{0}' ", shisakuEventCode)
                .AppendFormat("    AND SHISAKU_BUKA_CODE='{0}' ", shisakuBukaCode)
                .AppendFormat("    AND SHISAKU_BLOCK_NO='{0}' ", shisakuBlockNo)

                .AppendLine("    AND SHISAKU_BLOCK_NO_KAITEI_NO= ")
                .AppendLine("    ( ")
                .AppendLine("    SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    WHERE SHISAKU_EVENT_CODE = INSTL.SHISAKU_EVENT_CODE ")
                .AppendLine("    AND SHISAKU_BUKA_CODE=INSTL.SHISAKU_BUKA_CODE ")
                .AppendLine("    AND SHISAKU_BLOCK_NO=INSTL.SHISAKU_BLOCK_NO ")
                .AppendLine("  ) ")

            End With

            Dim param As New TShisakuBuhinEditInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            param.CreatedUserId = LoginInfo.Now.UserId
            Dim aDate As New ShisakuDate
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            Dim db As New EBomDbClient
            db.Insert(sb.ToString, param)
        End Sub


#Region "２次改修　試作部品表編集・改定編集（ブロック）の改訂情報を取得する"

        ''' <summary>
        ''' 試作部品表編集・改定編集（ブロック）の改訂情報を取得する
        ''' </summary>
        ''' <param name="blockNo">ブロック№</param>
        ''' <returns>ブロックの改訂一覧</returns>
        ''' <remarks></remarks>

        Public Function GetBlockKaiteiList(ByVal eventCode As String, ByVal blockNo As String) As List(Of TShisakuSekkeiBlockVo) Implements IShisakuBuhinEditBlockDao.GetBlockKaiteiList
            Dim sql As String = _
                "SELECT " _
            & "   COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'') AS SHISAKU_BLOCK_NO_KAITEI_NO , " _
            & "   COALESCE(SAISYU_KOUSHINBI,'') AS SAISYU_KOUSHINBI, " _
            & "   COALESCE(KAITEI_NAIYOU,'') AS KAITEI_NAIYOU, " _
            & "   COALESCE(KACHOU_SYOUNIN_JYOUTAI,'') AS KACHOU_SYOUNIN_JYOUTAI, " _
            & "   COALESCE(CREATED_DATE,'') AS CREATED_DATE, " _
            & "   COALESCE(UPDATED_DATE,'') AS UPDATED_DATE " _
            & "FROM  " _
            & "   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK BLOCK WITH (NOLOCK, NOWAIT) " _
            & "WHERE " _
            & "   SHISAKU_EVENT_CODE = '" & eventCode & "' " _
            & "   AND SHISAKU_BLOCK_NO = '" & blockNo & "' " _
            & "ORDER BY  " _
            & "   SHISAKU_BLOCK_NO_KAITEI_NO ASC "

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockVo)(sql)
        End Function

#End Region


    End Class

End Namespace

