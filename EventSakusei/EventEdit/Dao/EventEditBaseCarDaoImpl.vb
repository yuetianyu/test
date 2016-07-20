Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo

Namespace EventEdit.Dao

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EventEditBaseCarDaoImpl : Inherits DaoEachFeatureImpl
        Implements EventEditBaseCarDao

        ''' <summary>
        ''' 開発符号の一覧を取得する
        ''' </summary>
        ''' <returns>開発符号の一覧</returns>
        ''' <remarks></remarks>
        Public Function FindKaihatsuFugoLabelValues() As List(Of LabelValueVo) Implements EventEditBaseCarDao.FindKaihatsuFugoLabelValues
            Dim sql As String = _
                "SELECT " _
                    & "KAIHATSU_FUGO AS LABEL, " _
                    & "KAIHATSU_FUGO AS VALUE " _
                & "FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0020 WITH (NOLOCK, NOWAIT) " _
                & "WHERE " _
                    & "KAIHATSUFG_PLN <> '0' " _
                & "GROUP BY KAIHATSU_FUGO " _
                & "ORDER BY " _
                    & "SUBSTRING(KAIHATSU_FUGO, 1, 1) ASC, " _
                    & "SUBSTRING(KAIHATSU_FUGO, 2, 1) DESC "
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sql)
        End Function

        ''' <summary>
        ''' 仕様書一連Noの一覧を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>仕様書一連Noの一覧</returns>
        ''' <remarks></remarks>
        Public Function FindShiyoshoSeqnoLabelValues(ByVal kaihatsuFugo As String) As List(Of LabelValueVo) Implements EventEditBaseCarDao.FindShiyoshoSeqnoLabelValues
            Dim sql As String = _
                "SELECT " _
                & "    SHIYOSHO_SEQNO AS VALUE, " _
                & "    SHIYOSHO_SEQNO AS LABEL " _
                & "FROM " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 WITH (NOLOCK, NOWAIT) " _
                & "WHERE " _
                & "    KAIHATSU_FUGO    = @KaihatsuFugo " _
                & "GROUP BY " _
                & "    KAIHATSU_FUGO, " _
                & "    SHIYOSHO_SEQNO " _
                & "ORDER BY " _
                & "    SUBSTRING(SHIYOSHO_SEQNO, 1, 1) ASC, " _
                & "    SUBSTRING(SHIYOSHO_SEQNO, 2, 3) DESC "
            Dim param As New EventEditBaseCarParamVo
            param.KaihatsuFugo = kaihatsuFugo
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sql, param)
        End Function

        ''' <summary>
        ''' ７桁型式の一覧の取得
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No</param>
        ''' <returns>７桁型式の一覧</returns>
        ''' <remarks></remarks>
        Public Function FindRhac0230By(ByVal kaihatsuFugo As String, ByVal shiyoshoSeqno As String) As List(Of Rhac0230Vo) Implements EventEditBaseCarDao.FindRhac0230By
            Dim sql As String = _
                "SELECT " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.* " _
                & "FROM " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 " _
                & "    ON " & RHACLIBF_DB_NAME & ".dbo.RHAC0030.KAIHATSU_FUGO    = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KAIHATSU_FUGO " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0030.SOBI_KAITEI_NO  = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.SOBI_KAITEI_NO " _
                & "WHERE " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0030.KAIHATSU_FUGO    = @KaihatsuFugo AND " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0030.SHIYOSHO_SEQNO   = @ShiyoshoSeqno "
            Dim param As New EventEditBaseCarParamVo
            param.KaihatsuFugo = kaihatsuFugo
            param.ShiyoshoSeqno = shiyoshoSeqno
            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0230Vo)(sql, param)
        End Function

        ''' <summary>
        ''' アプライドNoの一覧を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No</param>
        ''' <returns>アプライドNoの一覧</returns>
        ''' <remarks></remarks>
        Public Function FindAppliedNoLabelValues(ByVal kaihatsuFugo As String, ByVal shiyoshoSeqno As String) As List(Of LabelValueVo) Implements EventEditBaseCarDao.FindAppliedNoLabelValues
            Dim sql As String = _
                "SELECT " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.APPLIED_NO AS VALUE, " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.APPLIED_NO AS LABEL " _
                & "FROM " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 " _
                & "    ON " & RHACLIBF_DB_NAME & ".dbo.RHAC0030.KAIHATSU_FUGO    = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KAIHATSU_FUGO " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0030.SOBI_KAITEI_NO  = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.SOBI_KAITEI_NO " _
                & "WHERE " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0030.KAIHATSU_FUGO    = @KaihatsuFugo AND " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0030.SHIYOSHO_SEQNO   = @ShiyoshoSeqno " _
                & "GROUP BY " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.APPLIED_NO " _
                & "ORDER BY " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.APPLIED_NO ASC "
            Dim param As New EventEditBaseCarParamVo
            param.KaihatsuFugo = kaihatsuFugo
            param.ShiyoshoSeqno = shiyoshoSeqno
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sql, param)
        End Function

        ''' <summary>
        ''' ７桁開発符号の一覧を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No</param>
        ''' <param name="appliedNo">アプライドNo</param>
        ''' <returns>７桁開発符号の一覧</returns>
        ''' <remarks></remarks>
        Public Function FindKatashikiFugo7LabelValues(ByVal kaihatsuFugo As String, ByVal shiyoshoSeqno As String, ByVal appliedNo As String) As List(Of LabelValueVo) Implements EventEditBaseCarDao.FindKatashikiFugo7LabelValues
            Dim sql As String = _
                "SELECT " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KATASHIKI_FUGO_7 AS VALUE, " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KATASHIKI_FUGO_7 AS LABEL " _
                & "FROM " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0030  " _
                & "    ON " & RHACLIBF_DB_NAME & ".dbo.RHAC0030.KAIHATSU_FUGO   = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KAIHATSU_FUGO " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0030.SOBI_KAITEI_NO = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.SOBI_KAITEI_NO " _
                & "WHERE " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0030.KAIHATSU_FUGO    = @KaihatsuFugo AND " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0030.SHIYOSHO_SEQNO   = @ShiyoshoSeqno AND" _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.APPLIED_NO       = @AppliedNo " _
                & "GROUP BY " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KATASHIKI_FUGO_7 " _
                & "ORDER BY " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KATASHIKI_FUGO_7 ASC "
            Dim param As New EventEditBaseCarParamVo
            param.KaihatsuFugo = kaihatsuFugo
            param.ShiyoshoSeqno = shiyoshoSeqno
            param.AppliedNo = StringUtil.ToInteger(appliedNo)
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sql, param)
        End Function

        ''' <summary>
        ''' 仕向地コードの一覧を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No</param>
        ''' <param name="appliedNo">アプライドNo</param>
        ''' <param name="katashikiFugo7">７桁開発符号</param>
        ''' <returns>仕向地コードの一覧</returns>
        ''' <remarks></remarks>
        Public Function FindShimukechiCodeLabelValues(ByVal kaihatsuFugo As String, ByVal shiyoshoSeqno As String, ByVal appliedNo As String, ByVal katashikiFugo7 As String) As List _
            (Of LabelValueVo) Implements EventEditBaseCarDao.FindShimukechiCodeLabelValues
            Dim sql As String = _
                "SELECT " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.SHIMUKECHI_CODE AS VALUE, " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.SHIMUKECHI_CODE AS LABEL " _
                & "FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0540  " _
                & "    ON " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.KAIHATSU_FUGO    = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KAIHATSU_FUGO " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.SOBI_KAITEI_NO  = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.SOBI_KAITEI_NO " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.KATASHIKI_SCD_7 = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KATASHIKI_SCD_7 " _
                & "WHERE " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KAIHATSU_FUGO      = @KaihatsuFugo " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KATASHIKI_FUGO_7 = @KatashikiFugo7 " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.APPLIED_NO       = @AppliedNo " _
                & "    AND EXISTS (SELECT * FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 WHERE KAIHATSU_FUGO = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KAIHATSU_FUGO AND SOBI_KAITEI_NO = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.SOBI_KAITEI_NO AND SHIYOSHO_SEQNO = @ShiyoshoSeqno) " _
                & "GROUP BY " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.SHIMUKECHI_CODE " _
                & "ORDER BY " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.SHIMUKECHI_CODE ASC "
            Dim param As New EventEditBaseCarParamVo
            param.KaihatsuFugo = kaihatsuFugo
            param.ShiyoshoSeqno = shiyoshoSeqno
            param.AppliedNo = StringUtil.ToInteger(appliedNo)
            param.KatashikiFugo7 = katashikiFugo7
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sql, param)
        End Function

        ''' <summary>
        ''' OPコードの一覧を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No</param>
        ''' <param name="appliedNo">アプライドNo</param>
        ''' <param name="katashikiFugo7">７桁型式</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <returns>OPコード</returns>
        ''' <remarks></remarks>
        Public Function FindOpCodeLabelValues(ByVal kaihatsuFugo As String, ByVal shiyoshoSeqno As String, ByVal appliedNo As String, ByVal katashikiFugo7 As String, ByVal shimukechiCode As String) As List _
            (Of LabelValueVo) Implements EventEditBaseCarDao.FindOpCodeLabelValues
            Dim sql As String = _
                "SELECT " _
                & "    RTRIM(" & RHACLIBF_DB_NAME & ".dbo.RHAC0540.OP_CODE) AS VALUE, " _
                & "    RTRIM(" & RHACLIBF_DB_NAME & ".dbo.RHAC0540.OP_CODE) AS LABEL " _
                & "FROM " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0540  " _
                & "    ON " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.KAIHATSU_FUGO    = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KAIHATSU_FUGO " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.SOBI_KAITEI_NO  = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.SOBI_KAITEI_NO " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.KATASHIKI_SCD_7 = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KATASHIKI_SCD_7 " _
                & "WHERE " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KAIHATSU_FUGO      = @KaihatsuFugo " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KATASHIKI_FUGO_7 = @KatashikiFugo7 " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.APPLIED_NO       = @AppliedNo " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.SHIMUKECHI_CODE  = @ShimukechiCode " _
                & "    AND EXISTS (SELECT * FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 WHERE KAIHATSU_FUGO = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KAIHATSU_FUGO AND SOBI_KAITEI_NO = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.SOBI_KAITEI_NO AND SHIYOSHO_SEQNO = @ShiyoshoSeqno) " _
                & "GROUP BY " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.OP_CODE " _
                & "ORDER BY " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.OP_CODE ASC "
            Dim param As New EventEditBaseCarParamVo
            param.KaihatsuFugo = kaihatsuFugo
            param.ShiyoshoSeqno = shiyoshoSeqno
            param.KatashikiFugo7 = katashikiFugo7
            param.AppliedNo = StringUtil.ToInteger(appliedNo)
            param.ShimukechiCode = shimukechiCode
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sql, param)
        End Function

        ''' <summary>
        ''' 外装色の一覧を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No</param>
        ''' <param name="appliedNo">アプライドNo</param>
        ''' <param name="katashikiFugo7">７桁型式</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <param name="opCode">OPコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindGaisoShokuLabelValues(ByVal kaihatsuFugo As String, ByVal shiyoshoSeqno As String, ByVal appliedNo As String, ByVal katashikiFugo7 As String, ByVal shimukechiCode As String, ByVal opCode As String) As List _
            (Of LabelValueVo) Implements EventEditBaseCarDao.FindGaisoShokuLabelValues
            Return FindBaseCarColorCode(kaihatsuFugo, shiyoshoSeqno, katashikiFugo7, appliedNo, shimukechiCode, opCode, Rhac0430VoHelper.NaigaisoKbn.Gaiso)
        End Function

        ''' <summary>
        ''' 内装色の一覧を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No</param>
        ''' <param name="appliedNo">アプライドNo</param>
        ''' <param name="katashikiFugo7">７桁型式</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <param name="opCode">OPコード</param>
        ''' <returns>内装色の一覧を取得する</returns>
        ''' <remarks></remarks>
        Public Function FindNaisoShokuLabelValues(ByVal kaihatsuFugo As String, ByVal shiyoshoSeqno As String, ByVal appliedNo As String, ByVal katashikiFugo7 As String, ByVal shimukechiCode As String, ByVal opCode As String) As List _
            (Of LabelValueVo) Implements EventEditBaseCarDao.FindNaisoShokuLabelValues
            Return FindBaseCarColorCode(kaihatsuFugo, shiyoshoSeqno, katashikiFugo7, appliedNo, shimukechiCode, opCode, Rhac0430VoHelper.NaigaisoKbn.Naiso)
        End Function

        ''' <summary>
        ''' ベース車カラーコードを取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No</param>
        ''' <param name="katashikiFugo7">７桁型式</param>
        ''' <param name="appliedNo">アプライドNo</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <param name="opCode">OPコード</param>
        ''' <param name="naigaisoKbn">内外装区分</param>
        ''' <returns>ベース車カラーコードを取得する</returns>
        ''' <remarks></remarks>
        Private Function FindBaseCarColorCode(ByVal kaihatsuFugo As String, ByVal shiyoshoSeqno As String, ByVal katashikiFugo7 As String, ByVal appliedNo As String, ByVal shimukechiCode As String, ByVal opCode As String, ByVal naigaisoKbn As String) As List(Of LabelValueVo)
            Dim sql As String = _
                "SELECT " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC1630.COLOR_CODE AS VALUE, " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC1630.COLOR_CODE AS LABEL " _
                & "FROM " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0540  " _
                & "    ON " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.KAIHATSU_FUGO    = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KAIHATSU_FUGO " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.SOBI_KAITEI_NO  = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.SOBI_KAITEI_NO " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.KATASHIKI_SCD_7 = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KATASHIKI_SCD_7 " _
                & "    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1630 " _
                & "    ON " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.KAIHATSU_FUGO    = " & RHACLIBF_DB_NAME & ".dbo.RHAC1630.KAIHATSU_FUGO " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.SOBI_KAITEI_NO  = " & RHACLIBF_DB_NAME & ".dbo.RHAC1630.SOBI_KAITEI_NO " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.SHIMUKECHI_CODE = " & RHACLIBF_DB_NAME & ".dbo.RHAC1630.SHIMUKECHI_CODE " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.KATASHIKI_SCD_7 = " & RHACLIBF_DB_NAME & ".dbo.RHAC1630.KATASHIKI_SCD_7 " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.COL_NO          = " & RHACLIBF_DB_NAME & ".dbo.RHAC1630.COL_NO " _
                & "WHERE " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KATASHIKI_FUGO_7  = @KatashikiFugo7 " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.APPLIED_NO      = @AppliedNo " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.KAIHATSU_FUGO   = @KaihatsuFugo " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.SHIMUKECHI_CODE = @ShimukechiCode " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.OP_CODE         = @OpCode " _
                & "    AND EXISTS (SELECT * FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0430 WHERE COLOR_CODE = " & RHACLIBF_DB_NAME & ".dbo.RHAC1630.COLOR_CODE AND NAIGAISO_KBN = @NaigaisoKbn) " _
                & "    AND EXISTS (SELECT * FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 WHERE KAIHATSU_FUGO = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KAIHATSU_FUGO AND SOBI_KAITEI_NO = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.SOBI_KAITEI_NO AND SHIYOSHO_SEQNO = @ShiyoshoSeqno) " _
                & "GROUP BY " & RHACLIBF_DB_NAME & ".dbo.RHAC1630.COLOR_CODE " _
                & "ORDER BY " & RHACLIBF_DB_NAME & ".dbo.RHAC1630.COLOR_CODE ASC "
            Dim param As New EventEditBaseCarParamVo
            param.KaihatsuFugo = kaihatsuFugo
            param.ShiyoshoSeqno = shiyoshoSeqno
            param.KatashikiFugo7 = katashikiFugo7
            param.AppliedNo = StringUtil.ToInteger(appliedNo)
            param.ShimukechiCode = shimukechiCode
            param.OpCode = opCode
            param.NaigaisoKbn = naigaisoKbn
            Dim db As New EBomDbClient
            Return db.QueryForList(Of LabelValueVo)(sql, param)
        End Function

        ''' <summary>
        ''' ７桁型式を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shiyoshoSeqno">仕様書一連No</param>
        ''' <param name="katashikiFugo7">７桁型式</param>
        ''' <param name="appliedNo">アプライドNo</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <param name="opCode">OPコード</param>
        ''' <param name="colorCode">カラーコード</param>
        ''' <param name="naigaisoKbn">内外装区分</param>
        ''' <returns>７桁型式</returns>
        ''' <remarks></remarks>
        Public Function FindRhac0230By(ByVal kaihatsuFugo As String, ByVal shiyoshoSeqno As String, ByVal katashikiFugo7 As String, ByVal appliedNo As String, ByVal shimukechiCode As String, ByVal opCode As String, ByVal colorCode As String, ByVal naigaisoKbn As String) As Rhac0230Vo Implements EventEditBaseCarDao.FindRhac0230By
            Dim sql As String = "SELECT " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.*" _
                & "FROM " _
                & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0540 " _
                & "    ON " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.KAIHATSU_FUGO    = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KAIHATSU_FUGO " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.SOBI_KAITEI_NO  = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.SOBI_KAITEI_NO " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.KATASHIKI_SCD_7 = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KATASHIKI_SCD_7 " _
                & "    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1630 " _
                & "    ON " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.KAIHATSU_FUGO    = " & RHACLIBF_DB_NAME & ".dbo.RHAC1630.KAIHATSU_FUGO " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.SOBI_KAITEI_NO  = " & RHACLIBF_DB_NAME & ".dbo.RHAC1630.SOBI_KAITEI_NO " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.SHIMUKECHI_CODE = " & RHACLIBF_DB_NAME & ".dbo.RHAC1630.SHIMUKECHI_CODE " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.KATASHIKI_SCD_7 = " & RHACLIBF_DB_NAME & ".dbo.RHAC1630.KATASHIKI_SCD_7 " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.COL_NO          = " & RHACLIBF_DB_NAME & ".dbo.RHAC1630.COL_NO " _
                & "WHERE " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KATASHIKI_FUGO_7  = @KatashikiFugo7 " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.APPLIED_NO      = @AppliedNo " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.KAIHATSU_FUGO   = @KaihatsuFugo " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.SHIMUKECHI_CODE = @ShimukechiCode " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0540.OP_CODE         = @OpCode " _
                & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC1630.COLOR_CODE      = @ColorCode " _
                & "    AND EXISTS (SELECT * FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0430 WHERE COLOR_CODE = " & RHACLIBF_DB_NAME & ".dbo.RHAC1630.COLOR_CODE AND NAIGAISO_KBN = @NaigaisoKbn) " _
                & "    AND EXISTS (SELECT * FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 WHERE KAIHATSU_FUGO = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KAIHATSU_FUGO AND SOBI_KAITEI_NO = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.SOBI_KAITEI_NO AND SHIYOSHO_SEQNO = @ShiyoshoSeqno) "
            Dim param As New EventEditBaseCarParamVo
            param.KaihatsuFugo = kaihatsuFugo
            param.ShiyoshoSeqno = shiyoshoSeqno
            param.KatashikiFugo7 = katashikiFugo7
            param.AppliedNo = StringUtil.ToInteger(appliedNo)
            param.ShimukechiCode = shimukechiCode
            param.OpCode = opCode
            param.ColorCode = colorCode
            param.NaigaisoKbn = naigaisoKbn
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of Rhac0230Vo)(sql, param)

        End Function

        Private Class ParamA
            ' 試作イベントコード
            Private _ShisakuEventCode As String
            ' 除外する試作イベントコード
            Private _WithoutShisakuEventCode As String

            ''' <summary>試作イベントコード</summary>
            ''' <value>試作イベントコード</value>
            ''' <returns>試作イベントコード</returns>
            Public Property ShisakuEventCode() As String
                Get
                    Return _ShisakuEventCode
                End Get
                Set(ByVal value As String)
                    _ShisakuEventCode = value
                End Set
            End Property

            ''' <summary>除外する試作イベントコード</summary>
            ''' <value>除外する試作イベントコード</value>
            ''' <returns>除外する試作イベントコード</returns>
            Public Property WithoutShisakuEventCode() As String
                Get
                    Return _WithoutShisakuEventCode
                End Get
                Set(ByVal value As String)
                    _WithoutShisakuEventCode = value
                End Set
            End Property
        End Class
        ''' <summary>
        ''' ベース車の「試作イベントコード」コンボボックス表示用の値を返す
        ''' </summary>
        ''' <param name="withoutShisakuEventCode">取り除く試作イベントコード</param>
        ''' <returns>試作設計ブロックINSTL情報</returns>
        ''' <remarks></remarks>
        Public Function FindBlockInstlForLabelValuesByWithout(ByVal withoutShisakuEventCode As String) As List(Of TShisakuSekkeiBlockInstlVo) Implements EventEditBaseCarDao.FindBlockInstlForLabelValuesByWithout
            Return FindBlockInstlForLabelValuesBy(Nothing, withoutShisakuEventCode)
        End Function
        ''' <summary>
        ''' ベース車の「号車」コンボボックス表示用の値を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">選択された「試作イベントコード」</param>
        ''' <returns>試作設計ブロックINSTL情報</returns>
        ''' <remarks></remarks>
        Public Function FindBlockInstlForLabelValuesBy(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockInstlVo) Implements EventEditBaseCarDao.FindBlockInstlForLabelValuesBy
            Return FindBlockInstlForLabelValuesBy(shisakuEventCode, Nothing)
        End Function
        ''' <summary>
        ''' 試作設計ブロックINSTL情報を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">絞り込みたい試作イベントコード</param>
        ''' <param name="withoutShisakuEventCode">取り除きたい試作イベントコード</param>
        ''' <returns>試作設計ブロックINSTL情報</returns>
        ''' <remarks></remarks>
        Private Function FindBlockInstlForLabelValuesBy(ByVal shisakuEventCode As String, ByVal withoutShisakuEventCode As String) As List(Of TShisakuSekkeiBlockInstlVo)

            ' > 設計展開時のメソッド
            ' > SekkeiBlockDaoImpl#FindShisakuBlockInstlByShisakuEventBase 
            ' 上記は似たようなSQLをもつメソッド
            ' 当SQL修正時は、上記メソッドも恐らく修正必要

            Dim sql As String = _
                "SELECT I.SHISAKU_EVENT_CODE,I.SHISAKU_GOUSYA " _
                & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL I " _
                & " WITH (NOLOCK, NOWAIT) " _
                & "WHERE I.SHISAKU_BLOCK_NO_KAITEI_NO = " _
                & "        (SELECT MAX(SHISAKU_BLOCK_NO_KAITEI_NO) FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK " _
                & "        WHERE SHISAKU_EVENT_CODE = I.SHISAKU_EVENT_CODE " _
                & "            AND SHISAKU_BUKA_CODE = I.SHISAKU_BUKA_CODE " _
                & "            AND SHISAKU_BLOCK_NO = I.SHISAKU_BLOCK_NO) " _
                & "    <if test='@ShisakuEventCode != null'>" _
                & "        AND I.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & "    </if>" _
                & "    <if test='@WithoutShisakuEventCode != null'>" _
                & "        AND I.SHISAKU_EVENT_CODE <> @WithoutShisakuEventCode " _
                & "    </if>" _
                & "    GROUP BY I.SHISAKU_EVENT_CODE,I.SHISAKU_GOUSYA " _
                & "    ORDER BY I.SHISAKU_EVENT_CODE,I.SHISAKU_GOUSYA "

            'Dim sql As String = _
            '    "SELECT I.* " _
            '    & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL I " _
            '    & " WITH (NOLOCK, NOWAIT) " _
            '    & "WHERE I.SHISAKU_BLOCK_NO_KAITEI_NO = " _
            '    & "        (SELECT MAX(SHISAKU_BLOCK_NO_KAITEI_NO) FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK " _
            '    & "        WHERE SHISAKU_EVENT_CODE = I.SHISAKU_EVENT_CODE " _
            '    & "            AND SHISAKU_BUKA_CODE = I.SHISAKU_BUKA_CODE " _
            '    & "            AND SHISAKU_BLOCK_NO = I.SHISAKU_BLOCK_NO) " _
            '    & "    AND EXISTS (SELECT * FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL " _
            '    & "        WHERE SHISAKU_EVENT_CODE = I.SHISAKU_EVENT_CODE " _
            '    & "            AND SHISAKU_BUKA_CODE = I.SHISAKU_BUKA_CODE " _
            '    & "            AND SHISAKU_BLOCK_NO = I.SHISAKU_BLOCK_NO " _
            '    & "            AND SHISAKU_BLOCK_NO_KAITEI_NO = I.SHISAKU_BLOCK_NO_KAITEI_NO " _
            '    & "            AND INSTL_HINBAN_HYOUJI_JUN = I.INSTL_HINBAN_HYOUJI_JUN " _
            '    & "        )" _
            '    & "    <if test='@ShisakuEventCode != null'>" _
            '    & "        AND I.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            '    & "    </if>" _
            '    & "    <if test='@WithoutShisakuEventCode != null'>" _
            '    & "        AND I.SHISAKU_EVENT_CODE <> @WithoutShisakuEventCode " _
            '    & "    </if>"

            Dim param As New ParamA
            param.ShisakuEventCode = shisakuEventCode
            param.WithoutShisakuEventCode = withoutShisakuEventCode
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sql, param)
        End Function

        ''' <summary>
        ''' 外装色コードから外装色名を返す
        ''' </summary>
        ''' <param name="colorCode">外装色コード</param>
        ''' <returns>外装色名</returns>
        ''' <remarks></remarks>
        Public Function FindGaisouColorName(ByVal colorCode As String) As Rhac0430Vo Implements EventEditBaseCarDao.FindGaisouColorName
            '  樺澤'
            Dim sql As String = _
                "SELECT COLOR_NAME " _
                & "FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0430 WITH (NOLOCK, NOWAIT) " _
                & "WHERE COLOR_CODE = @ColorCode" _
                & " AND NAIGAISO_KBN = @NaigaisoKbn "
            Dim param As New Rhac0430Vo
            param.ColorCode = colorCode
            param.NaigaisoKbn = "1"
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of Rhac0430Vo)(sql, param)
        End Function

        ''' <summary>
        ''' 内装色コードから内装色名を返す
        ''' </summary>
        ''' <param name="colorCode">内装色コード</param>
        ''' <returns>内装色名</returns>
        ''' <remarks></remarks>
        Public Function FindNaisouColorName(ByVal colorCode As String) As Rhac0430Vo Implements EventEditBaseCarDao.FindNaisouColorName
            Dim sql As String = _
                "SELECT COLOR_NAME " _
                & "FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0430 WITH (NOLOCK, NOWAIT) " _
                & "WHERE COLOR_CODE = @ColorCode" _
                & " AND NAIGAISO_KBN = @NaigaisoKbn "
            Dim param As New Rhac0430Vo
            param.ColorCode = colorCode
            param.NaigaisoKbn = "0"
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of Rhac0430Vo)(sql, param)
        End Function

        Dim _hshSobiKaitei As New Hashtable
        Public Function FindSobiKaitei(ByVal kaihatsuFugo As String, ByVal shiyoshoSeqno As String, ByVal katashiki As String) As Rhac0230Vo Implements EventEditBaseCarDao.FindSobiKaitei
            Dim key As New System.Text.StringBuilder
            With key
                .AppendLine(kaihatsuFugo)
                .AppendLine(shiyoshoSeqno)
                .AppendLine(katashiki)
            End With

            If _hshSobiKaitei.Contains(key.ToString) Then
                Return _hshSobiKaitei.Item(key.ToString)
            Else
                Dim sql As String = _
                    "SELECT " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.* " _
                    & "FROM " _
                    & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0230 WITH (NOLOCK, NOWAIT) INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 " _
                    & "    ON " & RHACLIBF_DB_NAME & ".dbo.RHAC0030.KAIHATSU_FUGO    = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KAIHATSU_FUGO " _
                    & "    AND " & RHACLIBF_DB_NAME & ".dbo.RHAC0030.SOBI_KAITEI_NO  = " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.SOBI_KAITEI_NO " _
                    & "WHERE " _
                    & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0230.KATASHIKI_FUGO_7  = @KatashikiFugo7 AND " _
                    & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0030.KAIHATSU_FUGO    = @KaihatsuFugo AND " _
                    & "    " & RHACLIBF_DB_NAME & ".dbo.RHAC0030.SHIYOSHO_SEQNO   = @ShiyoshoSeqno "
                Dim param As New EventEditBaseCarParamVo
                param.KaihatsuFugo = kaihatsuFugo
                param.ShiyoshoSeqno = shiyoshoSeqno
                param.KatashikiFugo7 = katashiki
                Dim db As New EBomDbClient
                Dim obj As Rhac0230Vo = db.QueryForObject(Of Rhac0230Vo)(sql, param)

                _hshSobiKaitei.Add(key.ToString, obj)

                Return obj

            End If

        End Function



    End Class
End Namespace