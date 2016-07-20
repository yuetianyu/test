Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon

Namespace ShisakuBuhinEditSekkei.Dao

    Public Class ShisakuBuhinEditBlockDaoImpl : Inherits DaoEachFeatureImpl
        Implements IShisakuBuhinEditBlockDao

        Public Const site_kbn = 1

#Region "試作部品表編集・改定編集（設計）進度状況全体取得する"
        ''' <summary>
        ''' 試作部品表編集・改定編集（設計）進度状況全体取得する
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns>進度状況合計</returns>
        ''' <remarks></remarks>
        Public Function GetTotalJyoutai(ByVal eventCode As String _
                                        ) As KabetuJyoutaiVo Implements IShisakuBuhinEditBlockDao.GetTotalJyoutai
            Dim sql As String = _
            "SELECT " _
             & "    SUM(TOTAL_BLOCK) AS TOTAL_BLOCK, " _
             & "    SUM(TOTAL_JYOUTAI) AS TOTAL_JYOUTAI, " _
             & "    SUM(TOTAL_SYOUNIN_JYOUTAI) AS TOTAL_SYOUNIN_JYOUTAI, " _
             & "    SUM(TOTAL_KACHOU_SYOUNIN_JYOUTAI) AS TOTAL_KACHOU_SYOUNIN_JYOUTAI " _
             & " FROM ( " _
             & "   SELECT " _
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
             & "GROUP BY SHISAKU_BUKA_CODE " _
             & " ) SUM_BLOCK "
            '& "WHERE SITE_KBN = " & site_kbn _
            '& "GROUP BY SHISAKU_BUKA_CODE,KA_RYAKU_NAME " _

            Dim param As New TShisakuSekkeiBlockVo
            If Not eventCode = String.Empty Then
                param.ShisakuEventCode = eventCode
            End If

            param.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY
            param.Jyoutai = TShisakuSekkeiBlockVoHelper.Jyoutai.FINISHED
            param.TantoSyouninJyoutai = TShisakuSekkeiBlockVoHelper.TantoJyoutai.APPROVAL
            param.KachouSyouninJyoutai = TShisakuSekkeiBlockVoHelper.KachouJyoutai.APPROVAL

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of KabetuJyoutaiVo)(sql, param)
        End Function

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

#End Region
        ''' <summary>
        ''' 試作部品表編集・改定編集（設計）進度状況　課別取得する
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns>進度状況合計</returns>
        ''' <remarks></remarks>
        Public Function GetKabetuJyoutai(ByVal eventCode As String) As String Implements IShisakuBuhinEditBlockDao.GetKabetuJyoutai
            Dim sql As String = _
            "SELECT " _
             & "    COALESCE(B.SHISAKU_BUKA_CODE,'') AS BUKA_CODE ,  " _
             & "    COALESCE(MAX(R.KA_RYAKU_NAME),B.SHISAKU_BUKA_CODE) AS KA_RYAKU_NAME , " _
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
             & "WHERE SHISAKU_EVENT_CODE = " & "'" & eventCode & "'"
            If eventCode = "FM5-K-T03" Or eventCode = "FM5-K-T04" Or eventCode = "FM5-K-T05" Or eventCode = "FM5-K-T06" Then
            Else
                sql = sql & "AND B.SHISAKU_BUKA_CODE <> '7221' "
            End If
            sql = sql & "GROUP BY SHISAKU_BUKA_CODE " _
             & "ORDER BY SHISAKU_BUKA_CODE "
            '2012/03/21 ↑ eventCode = "FM5-K-T03～06"以外の時 B.SHISAKU_BUKA_CODE <> '7221'をしている訳
            ' CSMCが重複して登録できてしまった件の対応
            ' 当課はRHAC1560に同じ略名を持つレコードが２件存在し、前回（FM5の時）設計展開時7221で展開されたので7221で
            ' でデータが作成されてしまった。どこかのタイミングで設計展開において部課コード取得方法を課コードのMAXを採用するように変更された為
            ' 以後は7222が正しいコードとなる。第一次改修にて設計課の追加仕様を盛り込んだが、入力された設計課が存在するかどうかをRHAC1560に
            ' 参照するロジックで課コードの若い7221が取得されてしまった為、設計課が追加されてしまった。しかし画面上は有効な設計課のみ表示したい
            ' との要望から間違えて作られてしまった7221を表示しないように対応した。ただし、FM5時のデータは7221で進行してしまった為、これらの
            ' イベントの場合、7221をあえて表示するようにした。

            '& " AND SITE_KBN = " & site_kbn _
            '& "GROUP BY SHISAKU_BUKA_CODE,KA_RYAKU_NAME "
            Return sql
        End Function

#Region "カレンダーマスタより処置期限日まであと何日か取得する"
        ''' <summary>
        ''' RHAC1860カレンダーマスタ
        ''' </summary>
        ''' <param name="Tojitu">当日</param>
        ''' <param name="SyochiKigenbi">処置期限日</param>
        ''' <returns>稼働日合計</returns>
        ''' <remarks></remarks>
        Public Function GetKadoubi(ByVal Tojitu As Integer, ByVal SyochiKigenbi As Integer) As Rhac1860VoHelper Implements IShisakuBuhinEditBlockDao.GetKadoubi

            Dim sql As String = _
            "SELECT " _
             & " COUNT(KADOBI_KBN) AS KADOBI " _
             & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC1860 " _
             & " WHERE KADOBI_KBN = '1' " _
             & "   AND CALENDAR_DATE >= @Tojitu" _
             & "   AND CALENDAR_DATE <= @SyochiKigenbi"

            Dim parm = New Rhac1860VoHelper
            parm.Tojitu = Tojitu
            parm.SyochiKigenbi = SyochiKigenbi

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of Rhac1860VoHelper)(sql, parm)

        End Function

#End Region

    End Class
End Namespace

