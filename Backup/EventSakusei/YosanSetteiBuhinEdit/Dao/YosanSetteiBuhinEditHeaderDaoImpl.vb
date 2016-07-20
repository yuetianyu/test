Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports System.IO
Imports System.Text

Namespace YosanSetteiBuhinEdit.Dao

    Public Class YosanSetteiBuhinEditHeaderDaoImpl : Inherits DaoEachFeatureImpl
        Implements YosanSetteiBuhinEditHeaderDao

        ''' <summary>
        ''' 集計コード一覧を返す
        ''' </summary>
        ''' <returns>該当する一覧</returns>
        ''' <remarks></remarks>
        Public Function FindAllSyukeiCodeInfo() As List(Of TSyukeiCodeVo) Implements YosanSetteiBuhinEditHeaderDao.FindByShukeiCodeInfo
            Dim db As New EBomDbClient

            Return db.QueryForList(Of TSyukeiCodeVo)("SELECT SYUKEI_CODE,SYUKEI_NAME FROM " & MBOM_DB_NAME & ".dbo.M_SYUKEI_CODE ")

        End Function

        ''' <summary>
        ''' イベント情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>イベント情報</returns>
        ''' <remarks></remarks>
        Public Function FindByEvent(ByVal shisakuEventCode As String) As ShisakuCommon.Db.EBom.Vo.TShisakuEventVo Implements YosanSetteiBuhinEditHeaderDao.FindByEvent
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT E.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT E WITH(NOLOCK, NOWAIT)")
                .AppendLine(" WHERE ")
                .AppendFormat(" E.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
            End With
            'Dim sql As String = _
            '" SELECT E.* " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT E " _
            '& " WHERE " _
            '& " E.SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            'Dim param As New TShisakuEventVo

            'param.ShisakuEventCode = shisakuEventCode

            Return db.QueryForObject(Of TShisakuEventVo)(sql.ToString)

        End Function

        ''' <summary>
        ''' リストコードを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns>該当するリストコード情報</returns>
        ''' <remarks></remarks>
        Public Function FindByListCode(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As ShisakuCommon.Db.EBom.Vo.TYosanSetteiListcodeVo Implements YosanSetteiBuhinEditHeaderDao.FindByListCode
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_LISTCODE LC WITH(NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendFormat(" LC.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND LC.YOSAN_LIST_CODE = '{0}' ", shisakuListCode)
            End With

            Dim db As New EBomDbClient

            Return db.QueryForObject(Of TYosanSetteiListcodeVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns>該当する手配基本情報</returns>
        ''' <remarks></remarks>
        Public Function FindByTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TYosanSetteiBuhinVo) Implements YosanSetteiBuhinEditHeaderDao.FindByTehaiKihon
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT DISTINCT YOSAN_BLOCK_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN A WITH(NOLOCK, NOWAIT)")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND YOSAN_LIST_CODE = '{0}' ", shisakuListCode)
            End With

            Dim db As New EBomDbClient

            Return db.QueryForList(Of TYosanSetteiBuhinVo)(sql.ToString)
        End Function


#Region "追加"

        ''' <summary>
        ''' 現調部品情報(試作)のデータ取得
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="phaseNo">ﾌｪｰｽﾞ№</param>
        ''' <returns></returns>
        ''' <remarks>複数レコードを１クエリで抽出する.</remarks>
        Public Function FindByTFuncBuhinShisaku(ByVal eventCode As String, _
                                                 ByVal phaseNo As String) As List(Of TFuncBuhinShisakuVo) Implements YosanSetteiBuhinEditHeaderDao.FindByTFuncBuhinShisaku
            Dim sb As New System.Text.StringBuilder
            Dim ORstr As String = "   OR ( "
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT A.* ")
                .AppendLine("FROM " & ShisakuCommon.ShisakuGlobal.MBOM_DB_NAME & ".dbo.T_FUNC_BUHIN_SHISAKU A WITH (NOLOCK, NOWAIT) ")

                'E-BOMとリンク。
                .AppendLine("  INNER JOIN  " & ShisakuCommon.ShisakuGlobal.MBOM_DB_NAME & ".dbo.T_FUNC_BUHIN_EBOM EBOM ON ")
                .AppendLine("   A.GENCHO_EVENT_CODE = EBOM.GENCHO_EVENT_CODE ")
                .AppendLine("   AND A.GENCHO_BUKA_CODE = EBOM.GENCHO_BUKA_CODE ")
                .AppendLine("   AND A.GENCHO_BLOCK_NO = EBOM.GENCHO_BLOCK_NO ")
                .AppendLine("   AND A.PHASE_NO = EBOM.PHASE_NO ")
                .AppendLine("   AND A.SHUKEI_CODE = EBOM.SHUKEI_CODE ")
                .AppendLine("   AND A.SIA_SHUKEI_CODE = EBOM.SIA_SHUKEI_CODE ")
                .AppendLine("   AND A.BUHIN_NO = EBOM.BUHIN_NO ")
                .AppendLine(" WHERE ")
                .AppendLine("        A.GENCHO_EVENT_CODE = '" & eventCode & "' ")
                .AppendLine("  AND   A.PHASE_NO = " & phaseNo & " ")

                'Dim j As Integer = 0
                'For Each paramVo In aParamVo
                '    If j = 0 Then .AppendLine(" WHERE (")
                '    j += 1
                '    .AppendLine("        A.GENCHO_EVENT_CODE = '" & paramVo.GenchoEventCode & "' ")
                '    If StringUtil.IsNotEmpty(paramVo.GenchoBukaCode) Then
                '        .AppendLine("        AND A.GENCHO_BUKA_CODE = '" & paramVo.GenchoBukaCode & "' ")
                '    End If

                '    If StringUtil.IsNotEmpty(paramVo.GenchoBlockNo) Then
                '        .AppendLine("        AND A.GENCHO_BLOCK_NO = '" & paramVo.GenchoBlockNo & "' ")
                '    End If

                '    If StringUtil.IsNotEmpty(phaseNo) Then
                '        .AppendLine("  AND   A.PHASE_NO = " & phaseNo & " ")
                '    End If

                '    If StringUtil.IsNotEmpty(paramVo.TehaiMakerCode) Then
                '        If StringUtil.Equals(paramVo.TehaiMakerCode, "未決") Then
                '            .AppendLine("        AND ( A.TEHAI_MAKER_CODE = '....' OR A.TEHAI_MAKER_CODE = '' ) ")
                '        Else
                '            .AppendLine("        AND A.TEHAI_MAKER_CODE = '" & paramVo.TehaiMakerCode & "' ")
                '        End If
                '    End If

                '    .AppendLine("    )")
                '    .AppendLine(ORstr)
                'Next
                '最後の条件文字列を削除.
                'If aParamVo.Count = 0 Then
                '    .AppendLine(" WHERE (")
                '    .AppendLine("        A.GENCHO_EVENT_CODE = '" & eventCode & "' ")
                '    If StringUtil.IsNotEmpty(phaseNo) Then
                '        .AppendLine("  AND   A.PHASE_NO = " & phaseNo & " ")
                '    End If
                '    .AppendLine("    )")
                'Else
                '    .Length -= ORstr.Length
                'End If

                .AppendLine(" ORDER BY A.GENCHO_BLOCK_NO ASC ,EBOM.BUHIN_NO_HYOUJI_JUN ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TFuncBuhinShisakuVo)(sb.ToString)
        End Function

        ''' <summary>
        ''' 現調イベントコードからフェーズを取得する
        ''' </summary>
        ''' <param name="genchoEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByPhase(ByVal genchoEventCode As String) As List(Of TFuncEventPhaseVo) Implements YosanSetteiBuhinEditHeaderDao.FindByPhase
            Dim sb As New System.Text.StringBuilder
            With sb
                .AppendLine("SELECT * FROM " & MBOM_DB_NAME & ".dbo.T_FUNC_EVENT_PHASE")
                .AppendLine(" WHERE GENCHO_EVENT_CODE = @GenchoEventCode")
                .AppendLine(" ORDER BY PHASE_HYOUJI_NO")
            End With
            Dim db As New EBomDbClient
            Dim param As New TFuncEventPhaseVo
            param.GenchoEventCode = genchoEventCode
            Return db.QueryForList(Of TFuncEventPhaseVo)(sb.ToString, param)
        End Function

        ''' <summary>
        ''' 現調イベントコードを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByGenchoEventCode() As List(Of TFuncEventPhaseVo) Implements YosanSetteiBuhinEditHeaderDao.FindByGenchoEventCode
            Dim sb As New System.Text.StringBuilder
            With sb
                .AppendLine("SELECT GENCHO_EVENT_CODE FROM " & MBOM_DB_NAME & ".dbo.T_FUNC_EVENT_PHASE")
                .AppendLine(" GROUP BY GENCHO_EVENT_CODE ")
                .AppendLine(" ORDER BY GENCHO_EVENT_CODE")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TFuncEventPhaseVo)(sb.ToString)
        End Function


#Region "パーツプライス取得(AS400)"

        Private Shared JavaProgName As String = ""
        Private Shared JavaErrorMsg As New System.Text.StringBuilder
        ''' <summary>JAR実行プロパティファイル</summary>
        Private Const conJarFile As String = "GetPartsPriceForSKE1Plus.jar"
        'Private Const conJarFile As String = "getPartsPriceListForSKE1.jar"

        ''' <summary>JAR実行プロパティファイル</summary>
        Private Const conPropertyFile As String = "connect.properties"

        ''' <summary>
        ''' 部品Noリストから設通Noリスト作成
        ''' </summary>
        ''' <param name="BuhinNoList">部品Noのリスト</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByPartsPrice(ByVal BuhinNoList As List(Of String)) As List(Of ASPartsPriceListVo) Implements YosanSetteiBuhinEditHeaderDao.FindByPartsPrice
            Dim rtnVal As New List(Of ASPartsPriceListVo)
            If BuhinNoList.Count = 0 Then
                Return rtnVal
            End If

            Dim inputFileName As String = Path.GetTempFileName()
            Dim outputFileName As String = Path.GetTempFileName()
            Dim progPath As String = My.Application.Info.DirectoryPath
            Dim buff() As String = Nothing
            Dim vo As ASPartsPriceListVo

            '引数ファイル作成
            Using writer As New StreamWriter(inputFileName, True, System.Text.Encoding.GetEncoding("Shift_JIS"))
                For Each buhinNo As String In BuhinNoList
                    writer.WriteLine(buhinNo)
                Next
                writer.Close()
            End Using

            'Java実行
            'Java実行
            Dim p As New System.Diagnostics.Process
            JavaProgName = "GetPartsPriceForSKE1Plus.jar"
            'JavaProgName = "getHachuJisekiListForSKE1.jar"
            p.StartInfo.UseShellExecute = False
            p.StartInfo.RedirectStandardOutput = True
            p.StartInfo.RedirectStandardError = True
            AddHandler p.OutputDataReceived, AddressOf p_OutputDataReceived
            AddHandler p.ErrorDataReceived, AddressOf p_ErrorDataReceived
            p.StartInfo.RedirectStandardInput = True
            p.StartInfo.CreateNoWindow = True
            JavaErrorMsg.Remove(0, JavaErrorMsg.Length)

            p.StartInfo.FileName = "java.exe"
            p.StartInfo.Arguments = String.Format(" -jar ""{0}\{1}"" ""{2}"" ""{3}"" ""{0}\{4}""", _
                                         progPath, _
                                         conJarFile, _
                                         inputFileName, _
                                         outputFileName, _
                                         conPropertyFile)
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

            p.Start()

            p.BeginOutputReadLine()
            p.BeginErrorReadLine()

            p.WaitForExit()

            If JavaErrorMsg.Length > 0 Then
                If System.IO.File.Exists(outputFileName) Then
                    System.IO.File.Delete(inputFileName)
                    System.IO.File.Delete(outputFileName)
                End If
                Throw New Exception(JavaErrorMsg.ToString)
                Return Nothing
            End If


            '出力ファイル読み込み
            Using txt As FileIO.TextFieldParser = New FileIO.TextFieldParser(outputFileName, System.Text.Encoding.GetEncoding(932))
                txt.TextFieldType = FileIO.FieldType.Delimited
                txt.SetDelimiters(vbTab)
                While Not txt.EndOfData
                    buff = txt.ReadFields()
                    vo = New ASPartsPriceListVo
                    vo.BuhinNo = buff(0)
                    vo.KPrice = convertDecimal(buff(1))
                    vo.SiaPrice = convertDecimal(buff(2))
                    vo.Mark = buff(3)
                    rtnVal.Add(vo)
                End While
                txt.Close()
            End Using

            'ファイル削除
            Dim fi As System.IO.FileInfo
            fi = New System.IO.FileInfo(inputFileName)
            fi.Delete()
            fi = New System.IO.FileInfo(outputFileName)
            fi.Delete()

            Return rtnVal
        End Function

        'OutputDataReceivedイベントハンドラ
        '行が出力されるたびに呼び出される
        Private Shared Sub p_OutputDataReceived(ByVal sender As Object, _
                ByVal e As System.Diagnostics.DataReceivedEventArgs)
            '出力された文字列を表示する
            'g_log.WriteInfo("getCostList:" & e.Data)
        End Sub

        'ErrorDataReceivedイベントハンドラ
        Private Shared Sub p_ErrorDataReceived(ByVal sender As Object, _
                ByVal e As System.Diagnostics.DataReceivedEventArgs)
            'エラー出力された文字列を表示する
            If StringUtil.IsNotEmpty(e.Data) Then

                'g_log.WriteErr(JavaProgName & " ERR>" & e.Data)
                JavaErrorMsg.AppendLine(e.Data)
            End If
        End Sub

        Private Function convertDecimal(ByVal val As String) As Nullable(Of Decimal)
            If StringUtil.IsEmpty(val) Then Return Nothing
            Dim rtnVal As Decimal
            If Decimal.TryParse(val, rtnVal) Then
                Return rtnVal
            Else
                Return 0
            End If
        End Function

#End Region

#Region "パーツプライス取得(RHAC2110)"

        Private Const SQL_PARTS_NUM_REPLACE As String = "<join property='@BuhinNos' separator=', ' />"
        ''' <summary>
        ''' 最新の（複数あれば低価格の）パーツプライスを取得する
        ''' </summary>
        ''' <param name="buhinNoList"></param>
        ''' <returns>取得結果[]</returns>
        ''' <remarks></remarks>
        Public Function FindLatestLowPartsPriceBy(ByVal buhinNoList As List(Of String)) As List(Of Rhac2110Vo) Implements YosanSetteiBuhinEditHeaderDao.FindLatestLowPartsPriceBy
            Dim sql As New System.Text.StringBuilder
            'RHAC1320の最新イベントのコスト取得日より前の情報で最新の金額を取得する'
            '国内・海外・CKDの値をそれぞれ取る'
            With sql
                .Remove(0, .Length)
                '国内'
                '2014/05/08 K.Haraguchi コスト取得日追加
                '.AppendLine(" SELECT T211.* ")
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2110 T211 ")
                .AppendLine(" WHERE ")
                .AppendLine(" T211.HACCYU_KBN = '1' ")
                .AppendLine(" AND T211.TANKA_JYOTAI_KBN = '3' ")
                .AppendLine(" AND T211.BUHIN_NO IN (" & SQL_PARTS_NUM_REPLACE & ") ")
                .AppendLine(" AND T211.TSUKA_KBN = 'Y' ")
                .AppendLine(" AND T211.TANKA_KEIYAKU_KBN = 'G' ")
                .AppendLine(" AND T211.HAISI_DATE = (SELECT MAX(HAISI_DATE) ")
                .AppendLine("                   FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2110 ")
                .AppendLine("                   WHERE ")
                .AppendLine("                   BUHIN_NO = T211.BUHIN_NO) ")


                '海外'
                .AppendLine(" UNION ALL ")
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2110 T211 ")
                .AppendLine(" WHERE ")
                .AppendLine(" T211.HACCYU_KBN = '1' ")
                .AppendLine(" AND T211.TANKA_JYOTAI_KBN = '3' ")
                .AppendLine(" AND T211.BUHIN_NO IN (" & SQL_PARTS_NUM_REPLACE & ") ")
                .AppendLine(" AND T211.TSUKA_KBN = 'D' ")
                .AppendLine(" AND T211.TANKA_KEIYAKU_KBN = 'A' ")
                .AppendLine(" AND T211.HAISI_DATE = (SELECT MAX(HAISI_DATE) ")
                .AppendLine("                   FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2110 ")
                .AppendLine("                   WHERE ")
                .AppendLine("                   BUHIN_NO = T211.BUHIN_NO) ")

                'CKD'
                .AppendLine(" UNION ALL ")
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2110 T211 ")
                .AppendLine(" WHERE ")
                .AppendLine(" T211.HACCYU_KBN = '1' ")
                .AppendLine(" AND T211.TANKA_JYOTAI_KBN = '3' ")
                .AppendLine(" AND T211.BUHIN_NO IN (" & SQL_PARTS_NUM_REPLACE & ") ")
                .AppendLine(" AND T211.TSUKA_KBN = 'Y' ")
                .AppendLine(" AND T211.TANKA_KEIYAKU_KBN = 'A' ")
                .AppendLine(" AND T211.HAISI_DATE = (SELECT MAX(HAISI_DATE) ")
                .AppendLine("                   FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2110 ")
                .AppendLine("                   WHERE ")
                .AppendLine("                   BUHIN_NO = T211.BUHIN_NO) ")


            End With

            Dim db As New EBomDbClient
            Dim result As New List(Of Rhac2110Vo)
            For Each splittedBuhinNos As ICollection(Of String) In SplitCount(buhinNoList, 500)
                ' 埋め込みパラメータを使うと500個の部品番号で約12s
                ' 使わないと約1.3s .............なのでココでは使わない。
                Dim aSql2 As String = sql.ToString.Replace(SQL_PARTS_NUM_REPLACE, _
                                                   "'" & Join(EscapeParameters(splittedBuhinNos), "', '") & "'")
                result.AddRange(db.QueryForList(Of Rhac2110Vo)(aSql2))
            Next
            Return result
        End Function

        ''' <summary>
        ''' 最大数を設定してコレクションを分割する
        ''' </summary>
        ''' <typeparam name="T">コレクションの内部の型</typeparam>
        ''' <param name="collection">コレクション</param>
        ''' <param name="maxCollectionSize">最大数</param>
        ''' <returns>最大数で分割されたコレクションの配列</returns>
        ''' <remarks></remarks>
        Public Shared Function SplitCount(Of T)(ByVal collection As ICollection(Of t), ByVal maxCollectionSize As Integer) As t()()
            Dim result As New List(Of T())
            If collection.Count = 0 Then
                Return result.ToArray
            End If

            Dim hoge As New List(Of T)
            Dim count As Integer = 0
            For Each value As T In collection
                If maxCollectionSize <= count Then
                    result.Add(hoge.ToArray)
                    hoge.Clear()
                    count = 0
                End If
                hoge.Add(value)
                count += 1
            Next
            result.Add(hoge.ToArray)
            Return result.ToArray
        End Function

        ''' <summary>
        ''' SQLパラメータを直接設定可能なエスケープした値にして返す
        ''' </summary>
        ''' <param name="params">パラメータ値[]</param>
        ''' <returns>エスケープした値[]</returns>
        ''' <remarks></remarks>
        Public Shared Function EscapeParameters(ByVal params As ICollection(Of String)) As String()
            Dim result As New List(Of String)
            For Each param As String In params
                result.Add(EscapeParameter(param))
            Next
            Return result.ToArray
        End Function
        ''' <summary>
        ''' SQLパラメータを直接設定可能なエスケープした値にして返す
        ''' </summary>
        ''' <param name="param">パラメータ値</param>
        ''' <returns>エスケープ後のパラメータ値</returns>
        ''' <remarks></remarks>
        Public Shared Function EscapeParameter(ByVal param As String) As String
            If param Is Nothing Then
                Return Nothing
            End If
            Return param.Replace("'", "''")
        End Function

#End Region

#Region "部品絶対値"

        ''' <summary>
        ''' 絶対値
        ''' </summary>
        ''' <param name="buhinNoList">部品番号[]</param>
        ''' <returns>取得結果[]</returns>
        ''' <remarks></remarks>
        Public Function FindLatestCostItBy(ByVal buhinNoList As List(Of String)) As List(Of Rhac0580Vo) Implements YosanSetteiBuhinEditHeaderDao.FindLatestCostItBy
            Dim sql As New System.Text.StringBuilder
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0580 T58 ")
                .AppendLine(" WHERE ")
                .AppendLine(" T58.BUHIN_NO IN (" & SQL_PARTS_NUM_REPLACE & ")")
                .AppendLine(" AND T58.GENKA_KBN IN ('011', '019', '020', '111', '119', '120') ")
                .AppendLine(" AND T58.GENKA_KAITEI_NO = (SELECT MAX(GENKA_KAITEI_NO) ")
                .AppendLine("                            FROM RHAC0580 ")
                .AppendLine("                            WHERE ")
                .AppendLine("                            BUHIN_NO = T58.BUHIN_NO ")
                .AppendLine("                            AND GENKA_KBN = T58.GENKA_KBN ) ")
                .AppendLine(" AND T58.HAISI_DATE = 99999999 ")
            End With


            Dim db As New EBomDbClient
            Dim result As New List(Of Rhac0580Vo)
            For Each splittedBuhinNos As ICollection(Of String) In SplitCount(buhinNoList, 500)
                ' 埋め込みパラメータを使うと500個の部品番号で約12s
                ' 使わないと約1.3s .............なのでココでは使わない。
                Dim aSql2 As String = sql.ToString.Replace(SQL_PARTS_NUM_REPLACE, _
                                                   "'" & Join(EscapeParameters(splittedBuhinNos), "', '") & "'")
                result.AddRange(db.QueryForList(Of Rhac0580Vo)(aSql2))
            Next
            Return result
        End Function

#End Region

#Region "設計試算部品費"

        ''' <summary>
        ''' 絶対値
        ''' </summary>
        ''' <param name="buhinNoList">部品番号[]</param>
        ''' <returns>取得結果[]</returns>
        ''' <remarks></remarks>
        Public Function FindLatestSekkeichiBy(ByVal buhinNoList As List(Of String)) As List(Of Rhac0580Vo) Implements YosanSetteiBuhinEditHeaderDao.FindLatestSekkeichiBy
            Dim sql As New System.Text.StringBuilder
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine(" V.BUHIN_NO ")
                .AppendLine(" ,V.COL_ID AS GENKA_KBN ")
                .AppendLine(" ,V.COL_VALUE AS GENKA_KINGAKU ") 'T_VALUE_DEVだとStringなので
                .AppendLine(" FROM " & BRAKU_DB_NAME & ".dbo.T_VALUE_DEV V ")
                .AppendLine(" WHERE ")
                .AppendLine(" V.BUHIN_NO IN (" & SQL_PARTS_NUM_REPLACE & ")")
                .AppendLine(" AND V.COL_ID IN ('S_G_SHISAN_BUHINHI', 'S_G_SIA_SHISAN_BUHINHI') ")
                .AppendLine(" AND V.HAISI_DATE = 99999999 ")
                '共用開発符号で設定'
                .AppendLine(" AND V.KAIHATSU_FUGO = '#CMN' ")

                '.AppendLine(" AND V.SAIYO_DATE = (SELECT MAX(SAIYO_DATE) ")
                '.AppendLine("                     FROM " & BRAKU_DB_NAME & ".dbo.T_VALUE_DEV ")
                '.AppendLine("                     WHERE ")
                '.AppendLine("                     BUHIN_NO = V.BUHIN_NO ")
                '.AppendLine("                     AND COL_ID = V.COL_ID )")

            End With


            Dim db As New EBomDbClient
            Dim result As New List(Of Rhac0580Vo)
            For Each splittedBuhinNos As ICollection(Of String) In SplitCount(buhinNoList, 500)
                ' 埋め込みパラメータを使うと500個の部品番号で約12s
                ' 使わないと約1.3s .............なのでココでは使わない。
                Dim aSql2 As String = sql.ToString.Replace(SQL_PARTS_NUM_REPLACE, _
                                                   "'" & Join(EscapeParameters(splittedBuhinNos), "', '") & "'")
                result.AddRange(db.QueryForList(Of Rhac0580Vo)(aSql2))
            Next
            Return result
        End Function

#End Region

#Region "ファンクションマスタから取得"

        ''' <summary>
        ''' 予算設定部品表_ﾌｧﾝｸｼｮﾝから取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByTYosanSetteiBuhinFunction() As List(Of TYosanSetteiBuhinFunctionVo) Implements YosanSetteiBuhinEditHeaderDao.FindByTYosanSetteiBuhinFunction
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_FUNCTION A WITH(NOLOCK, NOWAIT)")
                .AppendLine(" ORDER BY YOSAN_FUNCTION_HINBAN ")
                'これはいる？'
                '.AppendLine(" ,YOSAN_MAKER_CODE ")
            End With

            Dim db As New EBomDbClient

            Return db.QueryForList(Of TYosanSetteiBuhinFunctionVo)(sql.ToString)
        End Function

#End Region


#Region "最新化用"

#Region "予算設定部品情報を取得"

        ''' <summary>
        ''' 予算設定部品情報を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="yosanListCode">予算リストコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByTYosanSetteiBuhin(ByVal shisakuEventCode As String, ByVal yosanListCode As String) As List(Of TYosanSetteiBuhinVo) Implements YosanSetteiBuhinEditHeaderDao.FindByTYosanSetteiBuhin
            Dim sql As New StringBuilder

            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN WITH(NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND YOSAN_LIST_CODE = '{0}' ", yosanListCode)
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TYosanSetteiBuhinVo)(sql.ToString)
        End Function

#End Region

#Region "予算設定部品情報比較用情報を取得"

        ''' <summary>
        ''' 最新の部品編集情報を取得する（課長承認なし）
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="yosanListCode">予算リストコード</param>
        ''' <returns>最新の部品編集情報</returns>
        ''' <remarks></remarks>
        Public Function FindByNewBuhinEditListSaishin(ByVal eventcode As String, ByVal yosanListCode As String) As System.Collections.Generic.List(Of TehaichoMenu.Vo.TShisakuBuhinEditVoSekkeiHelper) Implements YosanSetteiBuhinEditHeaderDao.FindByNewBuhinEditListSaishin

            Dim db As New EBomDbClient
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT TG.YOSAN_GOUSYA AS SHISAKU_GOUSYA, TG.YOSAN_GOUSYA_HYOUJI_JUN AS HYOJIJUN_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA TG  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TG.SHISAKU_EVENT_CODE = '{0}'", eventcode)
                .AppendFormat(" AND TG.YOSAN_LIST_CODE = '{0}'", yosanListCode)
                .AppendLine(" GROUP BY TG.YOSAN_GOUSYA,  TG.YOSAN_GOUSYA_HYOUJI_JUN ")
                .AppendLine(" ORDER BY  TG.YOSAN_GOUSYA_HYOUJI_JUN,TG.YOSAN_GOUSYA ")
            End With

            Dim hshGosha As New Hashtable
            Dim goshaList As New List(Of String)
            For Each baseVo As TShisakuEventBaseVo In db.QueryForList(Of TShisakuEventBaseVo)(sql.ToString)
                goshaList.Add(String.Format("'{0}'", baseVo.ShisakuGousya))
                If Not hshGosha.Contains(baseVo.ShisakuGousya) Then
                    hshGosha.Add(baseVo.ShisakuGousya, baseVo.HyojijunNo)
                End If
            Next

            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT BE.*, BEI.INSU_SURYO, SBI.SHISAKU_GOUSYA, SB.BLOCK_FUYOU ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL SBI  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB  WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON SB.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SB.SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND SB.SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ")

                .AppendLine("AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO")
                'If isBase Then
                '    .AppendLine("AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO")
                'Else
                '    .AppendLine("AND ( NOT SB.SHISAKU_BLOCK_NO_KAITEI_NO = '000' AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = SBI.SHISAKU_BLOCK_NO_KAITEI_NO)")
                'End If

                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL BEI WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BEI.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BEI.INSTL_HINBAN_HYOUJI_JUN = SBI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT BE WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" ON BE.SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND BE.SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND BE.SHISAKU_BLOCK_NO_KAITEI_NO = SB.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND BE.BUHIN_NO_HYOUJI_JUN = BEI.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SBI.SHISAKU_EVENT_CODE = '{0}'", eventcode)
                .AppendFormat(" AND SBI.SHISAKU_GOUSYA IN ({0})", String.Join(",", goshaList.ToArray))
                .AppendLine(" AND NOT SBI.INSU_SURYO IS NULL ")
                .AppendLine(" AND SBI.INSU_SURYO > 0 ")
                .AppendLine(" AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( VARCHAR,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = SBI.SHISAKU_BUKA_CODE  ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = SBI.SHISAKU_BLOCK_NO ) ")
                .AppendLine(" ORDER BY SBI.SHISAKU_GOUSYA, BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO_HYOUJI_JUN ")
            End With
            Dim RList As List(Of TehaichoMenu.Vo.TShisakuBuhinEditVoSekkeiHelper) = db.QueryForList(Of TehaichoMenu.Vo.TShisakuBuhinEditVoSekkeiHelper)(sql.ToString)
            For Each resultVo As TehaichoMenu.Vo.TShisakuBuhinEditVoSekkeiHelper In RList
                resultVo.ShisakuGousyaHyoujiJun = hshGosha.Item(resultVo.ShisakuGousya)
            Next
            Return RList
        End Function

#End Region

#Region "予算設定部品履歴情報を取得"

        ''' <summary>
        ''' 予算設定部品履歴情報を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="yosanListCode">予算リストコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByTYosanSetteiBuhinRireki(ByVal shisakuEventCode As String, ByVal yosanListCode As String) As List(Of TYosanSetteiBuhinRirekiVo) Implements YosanSetteiBuhinEditHeaderDao.FindByTYosanSetteiBuhinRireki
            Dim sb As New System.Text.StringBuilder
            With sb
                .AppendLine("SELECT * FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_RIREKI")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = '" & shisakuEventCode & "'")
                .AppendLine(" AND YOSAN_LIST_CODE = '" & yosanListCode & "'")
                .AppendLine(" ORDER BY ")
                .AppendLine(" YOSAN_BLOCK_NO, ")
                .AppendLine(" YOSAN_BUHIN_NO, ")
                .AppendLine(" UPDATE_BI, ")
                .AppendLine(" UPDATE_JIKAN ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TYosanSetteiBuhinRirekiVo)(sb.ToString)
        End Function

#End Region

        ''' <summary>
        ''' 予算設定部品情報を削除
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="yosanListCode">予算リストコード</param>
        ''' <param name="yosanBlockNo">予算ブロック№</param>
        ''' <remarks></remarks>
        Public Sub DeleteByTYosanSetteiBuhin(ByVal shisakuEventCode As String, ByVal yosanListCode As String, ByVal yosanBlockNo As String) Implements YosanSetteiBuhinEditHeaderDao.DeleteByTYosanSetteiBuhin
            Dim sb As New System.Text.StringBuilder
            With sb
                .AppendLine("DELETE FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}'", shisakuEventCode)
                .AppendFormat(" AND YOSAN_LIST_CODE = '{0}'", yosanListCode)
                .AppendFormat(" AND YOSAN_BLOCK_NO = '{0}'", yosanBlockNo)
            End With
            Dim db As New EBomDbClient
            db.Delete(sb.ToString)
        End Sub


#End Region

#Region "過去購入部品情報取得"


        '20150420 Edit Watanabe 承認日を見るようにAS400のプログラムを調整。
        '調整後のファイルは別のJarファイルとしてアクセスできるようにしておいた。
        '承認日も見る版⇒getOrpf57ForSKE1_VerShoninDateCheck.jar
        '承認日見ない版⇒getSettsuNoForSKE1.jar


        ''' <summary>JAR実行プロパティファイル</summary>
        Private Const conJarFilePastPurchase As String = "GetPastPurchaseForSKE1.jar"

        ''' <summary>
        ''' 部品Noリストから設通Noリスト作成
        ''' </summary>
        ''' <param name="BuhinNoList">部品Noのリスト</param>
        ''' <param name="flag">0:発注最新順（発注日降順）1:検収最新順（検収日降順）2:コスト低順（検収金額昇順）</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByPastPurchase(ByVal BuhinNoList As List(Of String), ByVal flag As String) As List(Of TYosanSetteiBuhinVo) Implements YosanSetteiBuhinEditHeaderDao.FindByPastPurchase
            Dim inputFileName1 As String = Path.GetTempFileName()
            'Dim inputFileName2 As String = Path.GetTempFileName()
            'Dim inputFileName3 As String = Path.GetTempFileName()
            Dim outputFileName1 As String = Path.GetTempFileName()
            'Dim outputFileName2 As String = Path.GetTempFileName()
            'Dim outputFileName3 As String = Path.GetTempFileName()
            Dim progPath As String = My.Application.Info.DirectoryPath
            Dim buff As String = Nothing
            Dim buffSplit() As String
            Dim vo As TYosanSetteiBuhinVo
            Dim hitBuhinxList As New ArrayList


            Dim rtnVal As New List(Of TYosanSetteiBuhinVo)


            '生品番検索
            '引数ファイル作成
            Using writer As New StreamWriter(inputFileName1, True, Encoding.GetEncoding("Shift_JIS"))

                For Each buhinNo As String In BuhinNoList
                    writer.WriteLine(String.Format("{0}{1}", buhinNo, vbTab))

                Next
                writer.Close()
            End Using

            'Java実行
            Dim Process As System.Diagnostics.Process = CreateProccesStartInfo(progPath, _
                                                                               conJarFilePastPurchase, _
                                                                               inputFileName1, _
                                                                               outputFileName1, _
                                                                               conPropertyFile, _
                                                                               flag)
            Process.Start()

            Process.BeginOutputReadLine()
            Process.BeginErrorReadLine()
            Process.WaitForExit()



            '出力ファイル読み込み
            Using sr As New System.IO.StreamReader(outputFileName1, Encoding.GetEncoding(932))
                While sr.Peek() > -1
                    buff = sr.ReadLine()
                    buffSplit = Split(buff, vbTab)
                    vo = New TYosanSetteiBuhinVo
                    vo.YosanBuhinNo = buffSplit(0)  '部品番号
                    vo.Kako1RyosanTanka = buffSplit(1)  '量産単価
                    vo.Kako1WaritukeBuhinHi = buffSplit(2)  '割付部品費
                    vo.Kako1WaritukeKataHi = buffSplit(3)  '割付型費
                    vo.Kako1WaritukeKouhou = buffSplit(4)  '割付工法
                    vo.Kako1MakerBuhinHi = buffSplit(5)  'メーカー部品費
                    vo.Kako1MakerKataHi = buffSplit(6)  'メーカー型費
                    vo.Kako1MakerKouhou = buffSplit(7)  'メーカー工法
                    vo.Kako1ShingiBuhinHi = buffSplit(8)  '審議値部品費
                    vo.Kako1ShingiKataHi = buffSplit(9)  '審議値型費
                    vo.Kako1ShingiKouhou = buffSplit(10)  '審議値工法
                    vo.Kako1KounyuKibouTanka = buffSplit(11)  '購入希望単価
                    vo.Kako1KounyuTanka = buffSplit(12)  '購入単価
                    vo.Kako1ShikyuHin = buffSplit(13)  '支給品
                    vo.Kako1KoujiShireiNo = buffSplit(14)  '工事指令№
                    vo.Kako1EventName = buffSplit(15)   'イベント名称
                    'これらはString型でくる'
                    Dim hachu As String = buffSplit(16)
                    If StringUtil.IsNotEmpty(hachu) Then
                        vo.Kako1HachuBi = hachu  '発注日
                    End If

                    Dim kensyu As String = buffSplit(17)
                    If StringUtil.IsNotEmpty(kensyu) Then
                        vo.Kako1KenshuBi = kensyu  '検収日
                    End If

                    rtnVal.Add(vo)
                    'If hitBuhinxList.Contains(vo.Gzzcp) Then
                    '    hitBuhinxList.Add(vo.Gzzcp)
                    'End If
                End While
                sr.Close()
            End Using


            ''##品番検索
            ''引数ファイル作成
            'Using writer As New StreamWriter(inputFileName2, True, Encoding.GetEncoding("Shift_JIS"))
            '    For Each buhinNo As String In BuhinNoList
            '        If buhinNo.Length < 12 Then
            '            Continue For
            '        End If
            '        If Not hitBuhinxList.Contains(buhinNo) Then
            '            writer.WriteLine(String.Format("{0}{1}{2}", _
            '                                           buhinNo.Substring(0, buhinNo.Length - 2) & "##", _
            '                                           vbTab, _
            '                                           buhinNo))
            '        End If
            '    Next
            '    writer.Close()
            'End Using


            'Process = CreateProccesStartInfo(progPath, _
            '                             conJarFile, _
            '                             inputFileName2, _
            '                             outputFileName2, _
            '                             conPropertyFile)
            'Process.Start()
            'Process.BeginOutputReadLine()
            'Process.BeginErrorReadLine()
            'Process.WaitForExit()

            ''出力ファイル読み込み
            'Using sr As New System.IO.StreamReader(outputFileName2, Encoding.GetEncoding(932))
            '    While sr.Peek() > -1
            '        buff = sr.ReadLine()
            '        buffSplit = Split(buff, vbTab)
            '        vo = New Zspf10Vo
            '        vo.Gzzcp = buffSplit(0)       '部品番号
            '        vo.Tzzmbap = buffSplit(1)       'タイトル図面番号
            '        vo.Tzkdbap = buffSplit(2) '図面改訂№
            '        vo.Dhstba = buffSplit(3)      '設通№
            '        vo.Kxjrdt = buffSplit(4)        '受領日
            '        vo.Prfgp = buffSplit(5)  '開発符号
            '        'vo.KaihatsuFugo = buffSplit(6)  '出図予定日
            '        vo.Kdri = buffSplit(7)  '件名
            '        rtnVal.Add(vo)
            '    End While
            '    sr.Close()
            'End Using

            ''9桁品番対応（頭にハイフンを付けて検索）
            ''引数ファイル作成
            'Using writer As New StreamWriter(inputFileName3, True, Encoding.GetEncoding("Shift_JIS"))
            '    For Each buhinNo As String In BuhinNoList
            '        If Not hitBuhinxList.Contains(buhinNo) Then
            '            writer.WriteLine(String.Format("{0}{1}{2}", _
            '                                           "-" & buhinNo, _
            '                                           vbTab, _
            '                                           buhinNo))
            '        End If
            '    Next
            '    writer.Close()
            'End Using

            'Process = CreateProccesStartInfo(progPath, _
            '                      conJarFile, _
            '                      inputFileName3, _
            '                      outputFileName3, _
            '                      conPropertyFile)
            'Process.Start()
            'Process.BeginOutputReadLine()
            'Process.BeginErrorReadLine()
            'Process.WaitForExit()

            ''出力ファイル読み込み
            'Using sr As New System.IO.StreamReader(outputFileName3, Encoding.GetEncoding(932))
            '    While sr.Peek() > -1
            '        buff = sr.ReadLine()
            '        buffSplit = Split(buff, vbTab)
            '        vo = New Zspf10Vo
            '        vo.Gzzcp = buffSplit(0)       '部品番号
            '        vo.Tzzmbap = buffSplit(1)       'タイトル図面番号
            '        vo.Tzkdbap = buffSplit(2) '図面改訂№
            '        vo.Dhstba = buffSplit(3)      '設通№
            '        vo.Kxjrdt = buffSplit(4)        '受領日
            '        vo.Prfgp = buffSplit(5)  '開発符号
            '        'vo.KaihatsuFugo = buffSplit(6)  '出図予定日
            '        vo.Kdri = buffSplit(7)  '件名
            '        rtnVal.Add(vo)
            '    End While
            '    sr.Close()
            'End Using


            'ファイル削除
            Dim fi As System.IO.FileInfo
            fi = New System.IO.FileInfo(inputFileName1)
            fi.Delete()
            'fi = New System.IO.FileInfo(inputFileName2)
            'fi.Delete()
            'fi = New System.IO.FileInfo(inputFileName3)
            'fi.Delete()
            fi = New System.IO.FileInfo(outputFileName1)
            'fi.Delete()
            'fi = New System.IO.FileInfo(outputFileName2)
            'fi.Delete()
            'fi = New System.IO.FileInfo(outputFileName3)
            'fi.Delete()

            Return rtnVal
        End Function


        ''' <summary>
        ''' 織込み指示
        ''' </summary>
        ''' <param name="progPath"></param>
        ''' <param name="conJarFile"></param>
        ''' <param name="inputFileName"></param>
        ''' <param name="outputFileName"></param>
        ''' <param name="conPropertyFile"></param>
        ''' <param name="flag">0:発注最新順（発注日降順）1:検収最新順（検収日降順）2:コスト低順（検収金額昇順）</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CreateProccesStartInfo(ByVal progPath As String, _
                                                ByVal conJarFile As String, _
                                                ByVal inputFileName As String, _
                                                ByVal outputFileName As String, _
                                                ByVal conPropertyFile As String, _
                                                ByVal flag As String) As System.Diagnostics.Process
            Dim ProcessInfo As New System.Diagnostics.Process
            ProcessInfo.StartInfo.UseShellExecute = False
            ProcessInfo.StartInfo.RedirectStandardOutput = True
            ProcessInfo.StartInfo.RedirectStandardError = True
            AddHandler ProcessInfo.OutputDataReceived, AddressOf p_OutputDataReceived
            AddHandler ProcessInfo.ErrorDataReceived, AddressOf p_ErrorDataReceived
            ProcessInfo.StartInfo.RedirectStandardInput = True
            ProcessInfo.StartInfo.CreateNoWindow = True
            JavaErrorMsg.Remove(0, JavaErrorMsg.Length)

            ProcessInfo.StartInfo.FileName = "java.exe"

            ProcessInfo.StartInfo.Arguments = String.Format(" -jar ""{0}\{1}"" ""{2}"" ""{3}"" ""{0}\{4}"" ""{5}"" ", _
                                         progPath, _
                                         conJarFile, _
                                         inputFileName, _
                                         outputFileName, _
                                         conPropertyFile, _
                                         flag)

            ProcessInfo.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

            Return ProcessInfo
        End Function

#End Region

#End Region





    End Class
End Namespace