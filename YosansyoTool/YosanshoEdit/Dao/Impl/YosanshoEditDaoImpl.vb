Imports System.Text
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports EBom.Common

Namespace YosanshoEdit.Dao.Impl

    Public Class YosanshoEditDaoImpl : Implements YosanshoEditDao

        ''' <summary>
        ''' 予算書イベント別製作台数情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <returns>予算書イベント別製作台数情報</returns>
        ''' <remarks></remarks>
        Public Function FindYosanSeisakuDaisuBy(ByVal yosanEventCode As String) As List(Of TYosanSeisakuDaisuVo) Implements YosanshoEditDao.FindYosanSeisakuDaisuBy

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SEISAKU_DAISU ")
                .AppendLine("WHERE YOSAN_EVENT_CODE = @YosanEventCode ")
                .AppendLine("ORDER BY SHISAKU_SYUBETU, ")
                .AppendLine("KOUJI_SHIREI_NO_HYOJIJUN_NO ")
            End With
            Dim db As New EBomDbClient
            Dim param As New TYosanSeisakuDaisuVo
            param.YosanEventCode = yosanEventCode

            Return db.QueryForList(Of TYosanSeisakuDaisuVo)(sql.ToString, param)

        End Function

        ''' <summary>
        ''' 予算書イベント別金材情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <returns>予算書イベント別金材情報</returns>
        ''' <remarks></remarks>
        Public Function FindYosanKanazaiBy(ByVal yosanEventCode As String) As List(Of TYosanKanazaiVo) Implements YosanshoEditDao.FindYosanKanazaiBy

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_KANAZAI ")
                .AppendLine("WHERE YOSAN_EVENT_CODE = @YosanEventCode ")
                .AppendLine("ORDER BY KANAZAI_HYOUJI_JUN, ")
                .AppendLine("YOSAN_TUKURIKATA_YYYY_MM ")
            End With
            Dim db As New EBomDbClient
            Dim param As New TYosanKanazaiVo
            param.YosanEventCode = yosanEventCode

            Return db.QueryForList(Of TYosanKanazaiVo)(sql.ToString, param)

        End Function

        ''' <summary>
        ''' 予算書部品編集ﾊﾟﾀｰﾝ情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <returns>予算書部品編集ﾊﾟﾀｰﾝ情報</returns>
        ''' <remarks></remarks>
        Public Function FindYosanBuhinEditPatternBy(ByVal yosanEventCode As String) As List(Of TYosanBuhinEditPatternVo) Implements YosanshoEditDao.FindYosanBuhinEditPatternBy

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT DISTINCT PATTERN_NAME, BUHINHYO_NAME ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_BUHIN_EDIT_PATTERN ")
                .AppendLine("WHERE YOSAN_EVENT_CODE = @YosanEventCode ")
                .AppendLine("ORDER BY BUHINHYO_NAME, ")
                .AppendLine("PATTERN_NAME ")
            End With
            Dim db As New EBomDbClient
            Dim param As New TYosanBuhinEditPatternVo
            param.YosanEventCode = yosanEventCode

            Return db.QueryForList(Of TYosanBuhinEditPatternVo)(sql.ToString, param)

        End Function

        ''' <summary>
        ''' ﾊﾟﾀｰﾝ情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <returns>ﾊﾟﾀｰﾝ情報</returns>
        ''' <remarks></remarks>
        Public Function FindPatternNameBy(ByVal yosanEventCode As String) As List(Of TYosanBuhinEditPatternVo) Implements YosanshoEditDao.FindPatternNameBy

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT DISTINCT PATTERN_NAME, BUHINHYO_NAME ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_BUHIN_EDIT_PATTERN ")
                .AppendLine("WHERE YOSAN_EVENT_CODE = @YosanEventCode ")
                .AppendLine("UNION ")
                .AppendLine("SELECT DISTINCT PATTERN_NAME, BUHINHYO_NAME ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_TUKURIKATA ")
                .AppendLine("WHERE YOSAN_EVENT_CODE = @YosanEventCode ")
                .AppendLine("ORDER BY BUHINHYO_NAME, PATTERN_NAME ")
            End With
            Dim db As New EBomDbClient
            Dim param As New TYosanBuhinEditPatternVo
            param.YosanEventCode = yosanEventCode

            Return db.QueryForList(Of TYosanBuhinEditPatternVo)(sql.ToString, param)

        End Function

        ''' <summary>
        ''' 予算書イベント別造り方情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <param name="shisakuSyubetu">試作種別</param>
        ''' <returns>予算書イベント別造り方情報</returns>
        ''' <remarks></remarks>
        Public Function FindYosanTukurikataBy(ByVal yosanEventCode As String, ByVal shisakuSyubetu As String) As List(Of TYosanTukurikataVo) Implements YosanshoEditDao.FindYosanTukurikataBy

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_TUKURIKATA ")
                .AppendLine("WHERE YOSAN_EVENT_CODE = @YosanEventCode ")
                .AppendLine("AND SHISAKU_SYUBETU = @ShisakuSyubetu ")
                .AppendLine("ORDER BY BUHINHYO_NAME, ")
                .AppendLine("PATTERN_HYOUJI_JUN, ")
                .AppendLine("YOSAN_TUKURIKATA_YYYY_MM ")
            End With
            Dim db As New EBomDbClient
            Dim param As New TYosanTukurikataVo
            param.YosanEventCode = yosanEventCode
            param.ShisakuSyubetu = shisakuSyubetu

            Return db.QueryForList(Of TYosanTukurikataVo)(sql.ToString, param)

        End Function

        ''' <summary>
        ''' 予算書イベント別年月別財務実績情報取得
        ''' </summary>
        ''' <param name="yosanCode">予算書コード</param>
        ''' <returns>予算書イベント別年月別財務実績情報</returns>
        ''' <remarks></remarks>
        Public Function FindYosanZaimuJisekiBy(ByVal yosanCode As String) As List(Of TYosanZaimuJisekiVo) Implements YosanshoEditDao.FindYosanZaimuJisekiBy

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_ZAIMU_JISEKI ")
                .AppendLine("WHERE YOSAN_CODE = @YosanCode ")
                .AppendLine("ORDER BY YOSAN_ZAIMU_JISEKI_YYYY_MM, ")
                .AppendLine("YOSAN_ZAIMU_HIREI_KOTEI_KBN, ")
                .AppendLine("YOSAN_ZAIMU_JISEKI_KBN ")
            End With
            Dim db As New EBomDbClient
            Dim param As New TYosanZaimuJisekiVo
            param.YosanCode = yosanCode

            Return db.QueryForList(Of TYosanZaimuJisekiVo)(sql.ToString, param)

        End Function

        ''' <summary>
        ''' 予予算書イベント別見通情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <returns>予算書イベント別見通情報</returns>
        ''' <remarks></remarks>
        Private Function FindYosanEventMitoshiBy(ByVal yosanEventCode As String) As List(Of TYosanEventMitoshiVo) Implements YosanshoEditDao.FindYosanEventMitoshiBy

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_EVENT_MITOSHI ")
                .AppendLine("WHERE YOSAN_EVENT_CODE = @YosanEventCode ")
                .AppendLine("ORDER BY YOSAN_EVENT_MITOSHI_YYYY_MM, ")
                .AppendLine("YOSAN_EVENT_MITOSHI_KBN ")
            End With
            Dim db As New EBomDbClient
            Dim param As New TYosanEventMitoshiVo
            param.YosanEventCode = yosanEventCode

            Return db.QueryForList(Of TYosanEventMitoshiVo)(sql.ToString, param)

        End Function

        ''' <summary>
        ''' 購入品予算進度管理表取得
        ''' </summary>
        ''' <param name="kojishireiNo">工事指令№</param>
        ''' <param name="unitKbn">ユニット</param>
        ''' <returns>予算書イベント別部品費</returns>
        ''' <remarks></remarks>
        Public Function FindSeisakuAsKounyuYosanBy(ByVal kojishireiNo As String, ByVal unitKbn As String) As List(Of SeisakuAsKounyuYosanVo) Implements YosanshoEditDao.FindSeisakuAsKounyuYosanBy

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)

                '.AppendLine("SELECT CASE WHEN YOSAN.EQANS_1 > 0 THEN YOSAN.EQANS_1 ELSE YOSAN.SDYOYM END AS KOUNYU_YOSAN_YYYY_MM, ")
                '.AppendLine("   SUM(YOSAN.MIMXBH) AS BUHIN_HIREI_HI_COUNT, ")
                '.AppendLine("   SUM(YOSAN.MIMXKH) AS BUHIN_KOTEI_HI_COUNT ")
                '.AppendLine("FROM ( ")
                '.AppendLine("   SELECT MAIN_DATA.* ")
                '.AppendLine("       FROM ( ")
                '.AppendLine("           Select HEAD_INFO.SHISAKU_EVENT_NAME ")
                '.AppendLine("                 ,HEAD_INFO.KOMZBA ")
                '.AppendLine("                 ,BUHIN.* ")
                '.AppendLine("       FROM ( ")
                '.AppendLine("           Select LIST_CODE ")
                '.AppendLine("                 ,SHISAKU_EVENT_NAME ")
                '.AppendLine("                 ,KOMZBA ")
                '.AppendLine("       FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_AS_BUHIN_GOUSYA WITH (NOLOCK, NOWAIT)  ")
                '.AppendLine("           GROUP BY LIST_CODE, SHISAKU_EVENT_NAME,KOMZBA ")
                '.AppendLine("                   ) HEAD_INFO ")
                '.AppendLine("           INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SEISAKU_AS_BUHIN BUHIN WITH (NOLOCK, NOWAIT) ")
                '.AppendLine("               ON HEAD_INFO.LIST_CODE = BUHIN.LIST_CODE ")
                '.AppendLine("                 ) MAIN_DATA ")
                '.AppendLine("           WHERE SUBSTRING(MAIN_DATA.KOBA, 1, 2) IN ( ")
                '.AppendLine("               Select YOSAN_CODE ")
                '.AppendLine("       FROM " & MBOM_DB_NAME & ".dbo.M_SEISAKU_LINK_YOSAN WITH (NOLOCK, NOWAIT) ")
                '.AppendLine("           WHERE(KEISAN_KIJITSU >= CURRENT_TIMESTAMP Or KEISAN_KIJITSU Is NULL) ")
                '.AppendLine("           ) ")
                '.AppendLine("              AND MAIN_DATA.KOMZBA IN ( ")
                '.AppendLine("           Select KOUJI_SHIREI_NO ")
                '.AppendLine("                FROM " & MBOM_DB_NAME & ".dbo.M_SEISAKU_LINK_KOUJI WITH (NOLOCK, NOWAIT) ")
                '.AppendLine("                   WHERE(KEISAN_KIJITSU >= CURRENT_TIMESTAMP Or KEISAN_KIJITSU Is NULL) ")
                '.AppendLine("           ) ")
                '.AppendLine("           ) TARGET_DATA ")
                '.AppendLine("  INNER JOIN (SELECT DISTINCT UNIT_KBN,KOUJI_SHIREI_NO FROM " & MBOM_DB_NAME & ".dbo.M_SEISAKU_LINK_KOUJI WITH (NOLOCK, NOWAIT)) KOUJI ")
                '.AppendLine("       ON KOUJI.KOUJI_SHIREI_NO = TARGET_DATA.KOMZBA ")
                '.AppendLine("        INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SEISAKU_AS_KOUNYU_YOSAN YOSAN WITH (NOLOCK, NOWAIT) ")
                '.AppendLine("         ON SUBSTRING(TARGET_DATA.KOBA, 1, 2) = SUBSTRING(YOSAN.KOBA, 1, 2) ")
                '.AppendLine("        AND TARGET_DATA.BNBA = SUBSTRING(YOSAN.SGISBA, 4, 4) ")
                '.AppendLine("        AND TARGET_DATA.GYOID = YOSAN.GYOID ")
                '.AppendLine("        AND TARGET_DATA.BUBA = YOSAN.BUBA_15 ")
                '.AppendLine("        AND TARGET_DATA.KOMZBA = SUBSTRING(YOSAN.SGISBA, 1, 3) ")
                '.AppendLine(" WHERE ( ")
                '.AppendLine("    NOT EXISTS ( ")
                '.AppendLine("      SELECT * ")
                '.AppendLine("      FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_AS_HACHU_SHINCHOTATSU SHINCHOTATSU WITH (NOLOCK, NOWAIT) ")
                '.AppendLine("WHERE(SUBSTRING(TARGET_DATA.KOBA, 1, 2) = SUBSTRING(SHINCHOTATSU.YXKM, 1, 2)) ")
                '.AppendLine("          AND TARGET_DATA.BNBA = SUBSTRING(SHINCHOTATSU.KOMZBA, 4, 4) ")
                '.AppendLine("          AND TARGET_DATA.GYOID = SHINCHOTATSU.GYOID ")
                '.AppendLine("          AND TARGET_DATA.BUBA = SHINCHOTATSU.BUBA_15 ")
                '.AppendLine("          AND TARGET_DATA.KOMZBA = SUBSTRING(SHINCHOTATSU.KOMZBA, 1, 3) ")
                '.AppendLine("    ) ")
                '.AppendLine("    OR NOT EXISTS ( ")
                '.AppendLine("      SELECT * ")
                '.AppendLine("      FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_AS_HACHU_GENCHO GENCHO WITH (NOLOCK, NOWAIT) ")
                '.AppendLine("WHERE(SUBSTRING(TARGET_DATA.KOBA, 1, 2) = SUBSTRING(GENCHO.KOBA, 1, 2)) ")
                '.AppendLine("          AND TARGET_DATA.BNBA = SUBSTRING(GENCHO.SGISBA, 4, 4) ")
                '.AppendLine("          AND TARGET_DATA.GYOID = GENCHO.GYOID ")
                '.AppendLine("          AND TARGET_DATA.BUBA = GENCHO.BUBA_15 ")
                '.AppendLine("          AND TARGET_DATA.KOMZBA = SUBSTRING(GENCHO.SGISBA, 1, 3) ")
                '.AppendLine("    ) ")
                '.AppendLine("  ) ")
                '.AppendLine("   AND YOSAN.SGISBA LIKE '" & kojishireiNo & "%' ")
                '.AppendLine("   AND YOSAN.UNKM = '" & unitKbn & "' ")
                '.AppendLine("   GROUP BY CASE WHEN YOSAN.EQANS_1 > 0 THEN YOSAN.EQANS_1 ELSE YOSAN.SDYOYM END ")
                '.AppendLine("   ORDER BY KOUNYU_YOSAN_YYYY_MM ")









                .AppendLine("SELECT CASE WHEN EQANS_1 > 0 THEN EQANS_1 ELSE SDYOYM END AS KOUNYU_YOSAN_YYYY_MM, ")
                .AppendLine("SUM(MIMXBH) AS BUHIN_HIREI_HI_COUNT, ")
                .AppendLine("SUM(MIMXKH) AS BUHIN_KOTEI_HI_COUNT ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_AS_KOUNYU_YOSAN ")
                .AppendLine("WHERE SGISBA LIKE '" & kojishireiNo & "%' ")
                '.AppendLine("AND UNKM = '" & unitKbn & "' ")
                .AppendLine("GROUP BY CASE WHEN EQANS_1 > 0 THEN EQANS_1 ELSE SDYOYM END ")
                ''.AppendLine("UNION ")
                ''.AppendLine("SELECT CASE WHEN EQANS_1 > 0 THEN EQANS_1 ELSE SDYOYM END AS KOUNYU_YOSAN_YYYY_MM, ")
                ''.AppendLine("SUM(MIMXBH) AS BUHIN_HIREI_HI_COUNT, ")
                ''.AppendLine("SUM(MIMXKH) AS BUHIN_KOTEI_HI_COUNT ")
                ''.AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_AS_KOUNYU_YOSAN_W ")
                ''.AppendLine("WHERE SGISBA LIKE '" & kojishireiNo & "%' ")
                ''.AppendLine("AND UNKM = '" & unitKbn & "' ")
                ''.AppendLine("GROUP BY CASE WHEN EQANS_1 > 0 THEN EQANS_1 ELSE SDYOYM END ")
                '.AppendLine("ORDER BY KOUNYU_YOSAN_YYYY_MM")
            End With
            Dim db As New EBomDbClient

            Return db.QueryForList(Of SeisakuAsKounyuYosanVo)(sql.ToString)


            'Dim inputFileName As String = System.IO.Path.GetTempFileName()
            'Dim outputFileName As String = System.IO.Path.GetTempFileName()
            'Dim progPath As String = My.Application.Info.DirectoryPath
            'Dim buff As String = ""
            'Dim buffSplit() As String
            'Dim rtnVal As New List(Of SeisakuAsKounyuYosanVo)

            ''引数ファイル作成
            'Using writer As New System.IO.StreamWriter(inputFileName, True, Encoding.GetEncoding("Shift_JIS"))
            '    writer.WriteLine(kojishireiNo)
            '    writer.Close()
            'End Using

            ''Java実行
            'Dim p As New System.Diagnostics.Process

            'JavaProgName = "FindSeisakuAsKounyuYosanBy.jar"
            'p.StartInfo.UseShellExecute = False
            'p.StartInfo.RedirectStandardOutput = True
            'p.StartInfo.RedirectStandardError = True
            'AddHandler p.OutputDataReceived, AddressOf p_OutputDataReceived
            'AddHandler p.ErrorDataReceived, AddressOf p_ErrorDataReceived
            'p.StartInfo.RedirectStandardInput = True
            'p.StartInfo.CreateNoWindow = True
            'JavaErrorMsg.Remove(0, JavaErrorMsg.Length)

            'p.StartInfo.FileName = "java.exe"
            'p.StartInfo.Arguments = String.Format(" -jar ""{0}\FindSeisakuAsKounyuYosanBy.jar"" 1 ""{1}"" ""{2}"" ""{0}\connect.properties""", progPath, inputFileName, outputFileName)
            'p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            'p.Start()

            'p.BeginOutputReadLine()
            'p.BeginErrorReadLine()

            'p.WaitForExit()

            'If JavaErrorMsg.Length > 0 Then
            '    If System.IO.File.Exists(outputFileName) Then
            '        System.IO.File.Delete(inputFileName)
            '        System.IO.File.Delete(outputFileName)
            '    End If
            '    Throw New Exception(JavaErrorMsg.ToString)
            '    Return Nothing
            'End If

            ''出力ファイル読み込み
            'Using sr As New System.IO.StreamReader(outputFileName, Encoding.GetEncoding(932))
            '    While sr.Peek() > -1
            '        buff = sr.ReadLine()
            '        buffSplit = Split(buff, vbTab)
            '        Dim vo As New SeisakuAsKounyuYosanVo
            '        vo.KounyuYosanYyyyMm = buffSplit(0).Trim
            '        vo.BuhinHireiHiCount = Decimal.Parse(buffSplit(1))
            '        vo.BuhinKoteiHiCount = Decimal.Parse(buffSplit(2))
            '        rtnVal.Add(vo)
            '    End While
            '    sr.Close()
            'End Using

            ''ファイル削除
            'System.IO.File.Delete(inputFileName)
            'System.IO.File.Delete(outputFileName)


            'Return rtnVal

        End Function

        ''' <summary>
        ''' 購入品予算進度管理表取得
        ''' </summary>
        ''' <param name="kojishireiNo">工事指令№</param>
        ''' <param name="isMax">最大の年月かどうか</param>
        ''' <returns>予算書イベント別部品費</returns>
        ''' <remarks></remarks>
        Public Function FindSeisakuAsKounyuYosanBy(ByVal kojishireiNo As String, ByVal isMax As Boolean) As String Implements YosanshoEditDao.FindSeisakuAsKounyuYosanBy

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                If isMax Then
                    .AppendLine("MAX(CASE WHEN EQANS_1 > 0 THEN EQANS_1 ELSE SDYOYM END) AS KOUNYU_YOSAN_YYYY_MM ")
                Else
                    .AppendLine("MIN(CASE WHEN EQANS_1 > 0 THEN EQANS_1 ELSE SDYOYM END) AS KOUNYU_YOSAN_YYYY_MM ")
                End If
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SEISAKU_AS_KOUNYU_YOSAN ")
                .AppendLine("WHERE SGISBA LIKE '" & kojishireiNo & "%' ")
            End With
            Dim db As New EBomDbClient

            Dim resultVo As SeisakuAsKounyuYosanVo = db.QueryForObject(Of SeisakuAsKounyuYosanVo)(sql.ToString)

            Return resultVo.KounyuYosanYyyyMm

            'Dim inputFileName As String = System.IO.Path.GetTempFileName()
            'Dim outputFileName As String = System.IO.Path.GetTempFileName()
            'Dim progPath As String = My.Application.Info.DirectoryPath
            'Dim buff As String = ""
            'Dim buffSplit() As String
            'Dim rtnVal As Integer

            ''引数ファイル作成
            'Using writer As New System.IO.StreamWriter(inputFileName, True, Encoding.GetEncoding("Shift_JIS"))
            '    writer.WriteLine(kojishireiNo)
            '    writer.Close()
            'End Using

            ''Java実行
            'Dim p As New System.Diagnostics.Process

            'JavaProgName = "FindSeisakuAsKounyuYosanBy.jar"
            'p.StartInfo.UseShellExecute = False
            'p.StartInfo.RedirectStandardOutput = True
            'p.StartInfo.RedirectStandardError = True
            'AddHandler p.OutputDataReceived, AddressOf p_OutputDataReceived
            'AddHandler p.ErrorDataReceived, AddressOf p_ErrorDataReceived
            'p.StartInfo.RedirectStandardInput = True
            'p.StartInfo.CreateNoWindow = True
            'JavaErrorMsg.Remove(0, JavaErrorMsg.Length)

            'p.StartInfo.FileName = "java.exe"
            'p.StartInfo.Arguments = String.Format(" -jar ""{0}\FindSeisakuAsKounyuYosanBy.jar"" 2 ""{1}"" ""{2}"" ""{0}\connect.properties""", progPath, inputFileName, outputFileName)
            'p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            'p.Start()

            'p.BeginOutputReadLine()
            'p.BeginErrorReadLine()

            'p.WaitForExit()

            'If JavaErrorMsg.Length > 0 Then
            '    If System.IO.File.Exists(outputFileName) Then
            '        System.IO.File.Delete(inputFileName)
            '        System.IO.File.Delete(outputFileName)
            '    End If
            '    Throw New Exception(JavaErrorMsg.ToString)
            '    Return Nothing
            'End If

            ''出力ファイル読み込み
            'Using sr As New System.IO.StreamReader(outputFileName, Encoding.GetEncoding(932))
            '    While sr.Peek() > -1
            '        buff = sr.ReadLine()
            '        buffSplit = Split(buff, vbTab)
            '        If isMax Then
            '            rtnVal = Integer.Parse(buffSplit(0))
            '        Else
            '            rtnVal = Integer.Parse(buffSplit(1))
            '        End If
            '    End While
            '    sr.Close()
            'End Using

            ''ファイル削除
            'System.IO.File.Delete(inputFileName)
            'System.IO.File.Delete(outputFileName)


            'Return rtnVal

        End Function

        ''' <summary>
        ''' 予算書イベント別年月別財務実績情報の最大または最小の財務実績計上年月取得
        ''' </summary>
        ''' <param name="yosanCode">予算書コード</param>
        ''' <returns>財務実績計上年月</returns>
        ''' <remarks></remarks>
        Public Function FindYosanZaimuJisekiYyyyMmBy(ByVal yosanCode As String, ByVal isMax As Boolean) As String Implements YosanshoEditDao.FindYosanZaimuJisekiYyyyMmBy

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                If isMax Then
                    .AppendLine("MAX(YOSAN_ZAIMU_JISEKI_YYYY_MM) AS YOSAN_ZAIMU_JISEKI_YYYY_MM ")
                Else
                    .AppendLine("MIN(YOSAN_ZAIMU_JISEKI_YYYY_MM) AS YOSAN_ZAIMU_JISEKI_YYYY_MM ")
                End If
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_ZAIMU_JISEKI ")
                .AppendLine("WHERE YOSAN_CODE = @YosanCode ")
            End With
            Dim db As New EBomDbClient
            Dim param As New TYosanZaimuJisekiVo
            param.YosanCode = yosanCode

            Dim resultVo As TYosanZaimuJisekiVo = db.QueryForObject(Of TYosanZaimuJisekiVo)(sql.ToString, param)

            Return resultVo.YosanZaimuJisekiYyyyMm

        End Function



        'OutputDataReceivedイベントハンドラ
        '行が出力されるたびに呼び出される
        Private Shared Sub p_OutputDataReceived(ByVal sender As Object, _
                ByVal e As System.Diagnostics.DataReceivedEventArgs)
            '出力された文字列を表示する
            'g_log.WriteInfo("getCostList:" & e.Data)
        End Sub

        Private Shared JavaProgName As String = ""
        Private Shared JavaErrorMsg As New System.Text.StringBuilder

        'ErrorDataReceivedイベントハンドラ
        Private Shared Sub p_ErrorDataReceived(ByVal sender As Object, _
                ByVal e As System.Diagnostics.DataReceivedEventArgs)
            'エラー出力された文字列を表示する
            If StringUtil.IsNotEmpty(e.Data) Then

                g_log.WriteErr(JavaProgName & " ERR>" & e.Data)
                JavaErrorMsg.AppendLine(e.Data)
            End If
        End Sub


        ''' <summary>
        ''' 予算書部品表選択情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <returns>予算書部品表選択情報</returns>
        ''' <remarks></remarks>
        Public Function FindBuhinhyoNameBy(ByVal yosanEventCode As String) As List(Of TYosanBuhinSelectVo) Implements YosanshoEditDao.FindBuhinhyoNameBy

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT BUHINHYO_NAME")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_BUHIN_SELECT ")
                .AppendLine("WHERE YOSAN_EVENT_CODE = @YosanEventCode ")
                .AppendLine("ORDER BY BUHINHYO_NAME ")
            End With
            Dim db As New EBomDbClient
            Dim param As New TYosanBuhinSelectVo
            param.YosanEventCode = yosanEventCode

            Return db.QueryForList(Of TYosanBuhinSelectVo)(sql.ToString, param)

        End Function


#Region "追加"

        ''' <summary>
        ''' 予算書イベント別造り方情報取得
        ''' </summary>
        ''' <param name="yosanEventCode">予算書イベントコード</param>
        ''' <returns>予算書イベント別造り方情報</returns>
        ''' <remarks></remarks>
        Public Function FindYosanTukurikataListBy(ByVal yosanEventCode As String) As List(Of TYosanTukurikataVo) Implements YosanshoEditDao.FindYosanTukurikataListBy

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_TUKURIKATA ")
                .AppendLine("WHERE YOSAN_EVENT_CODE = @YosanEventCode ")
            End With
            Dim db As New EBomDbClient
            Dim param As New TYosanTukurikataVo
            param.YosanEventCode = yosanEventCode

            Return db.QueryForList(Of TYosanTukurikataVo)(sql.ToString, param)

        End Function


#End Region



    End Class

End Namespace
