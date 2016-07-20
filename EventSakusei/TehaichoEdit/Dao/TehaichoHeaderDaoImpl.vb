Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.Kosei.Vo
Imports System.IO
Imports System.Text

Namespace TehaichoEdit.Dao

    Public Class TehaichoHeaderDaoImpl : Inherits DaoEachFeatureImpl
        Implements TehaichoHeaderDao

        ''' <summary>
        ''' 集計コード一覧を返す
        ''' </summary>
        ''' <returns>該当する一覧</returns>
        ''' <remarks></remarks>
        Public Function FindAllSyukeiCodeInfo() As List(Of TSyukeiCodeVo) Implements TehaichoHeaderDao.FindByShukeiCodeInfo
            Dim db As New EBomDbClient

            Return db.QueryForList(Of TSyukeiCodeVo)("SELECT SYUKEI_CODE,SYUKEI_NAME FROM " & MBOM_DB_NAME & ".dbo.M_SYUKEI_CODE ")

        End Function

        ''' <summary>
        ''' イベント情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>イベント情報</returns>
        ''' <remarks></remarks>
        Public Function FindByEvent(ByVal shisakuEventCode As String) As ShisakuCommon.Db.EBom.Vo.TShisakuEventVo Implements TehaichoHeaderDao.FindByEvent
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
        Public Function FindByListCode(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As ShisakuCommon.Db.EBom.Vo.TShisakuListcodeVo Implements TehaichoHeaderDao.FindByListCode
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE LC WITH(NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendFormat(" LC.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND LC.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND LC.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX(SHISAKU_LIST_CODE_KAITEI_NO) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE  WITH(NOLOCK, NOWAIT)")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = LC.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = LC.SHISAKU_LIST_CODE )")
            End With


            'Dim sql As String = _
            '" SELECT * " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE LC " _
            '& " WHERE " _
            '& " LC.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            '& " AND LC.SHISAKU_LIST_CODE = @ShisakuListCode " _
            '& " AND LC.SHISAKU_LIST_CODE_KAITEI_NO = ( " _
            '& " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE " _
            '& " WHERE SHISAKU_EVENT_CODE = LC.SHISAKU_EVENT_CODE " _
            '& " AND SHISAKU_LIST_CODE = LC.SHISAKU_LIST_CODE )"

            Dim db As New EBomDbClient
            'Dim param As New TShisakuListcodeVo

            'param.ShisakuEventCode = shisakuEventCode
            'param.ShisakuListCode = shisakuListCode

            Return db.QueryForObject(Of TShisakuListcodeVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns>該当する手配基本情報</returns>
        ''' <remarks></remarks>
        Public Function FindByTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiKihonVo) Implements TehaichoHeaderDao.FindByTehaiKihon
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT DISTINCT SHISAKU_BLOCK_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON A WITH(NOLOCK, NOWAIT)")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)

                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX(SHISAKU_LIST_CODE_KAITEI_NO) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE B WITH(NOLOCK, NOWAIT)")
                .AppendLine(" WHERE A.SHISAKU_EVENT_CODE = B.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND A.SHISAKU_LIST_CODE = B.SHISAKU_LIST_CODE )")
            End With
            'Dim sql As String = _
            '" SELECT * " _
            '& " FROM " _
            '& " " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON " _
            '& " WHERE " _
            '& " SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            '& " AND SHISAKU_LIST_CODE = @ShisakuListCode "

            Dim db As New EBomDbClient
            'Dim param As New TShisakuTehaiKihonVo
            'param.ShisakuEventCode = shisakuEventCode
            'param.ShisakuListCode = shisakuListCode

            Return db.QueryForList(Of TShisakuTehaiKihonVo)(sql.ToString)
        End Function


#Region "部品表EXCEL出力(最新)"

#Region "SELECT 試作手配帳情報（号車グループ情報）を返す"
        ''' <summary>
        ''' 試作手配帳情報（号車グループ情報）を返す。
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindGousyaGroupBy(ByVal shisakuGousya As String, _
                                                 ByVal shisakuEventCode As String, _
                                                 ByVal shisakuListCode As String, _
                                                 ByVal shisakuListCodeKaiteiNo As String) As TShisakuTehaiGousyaGroupVo Implements TehaichoHeaderDao.FindGousyaGroupBy

            Dim dtResult As New TShisakuTehaiGousyaGroupVo
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA_GROUP TG WITH (NOLOCK, NOWAIT) ")
                .AppendFormat(" WHERE TG.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
                .AppendFormat(" AND TG.SHISAKU_GOUSYA = '{0}' ", shisakuGousya)
            End With

            Dim db As New EBomDbClient

            Return db.QueryForObject(Of TShisakuTehaiGousyaGroupVo)(sql.ToString)

        End Function
#End Region

#End Region



#Region "EXCEL出力(出図履歴含む)"

#Region "SELECT 試作手配出図実績情報を返す"
        ''' <summary>
        ''' 試作手配出図実績情報を返す。
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindShutuzuJisekiBy(ByVal shisakuEventCode As String, _
                                            ByVal shisakuListCode As String, _
                                            ByVal shisakuListCodeKaiteiNo As String, _
                                            ByVal blockNo As String, _
                                            ByVal buhinNo As String, _
                                            ByVal descJun As Boolean) As List(Of TShisakuTehaiShutuzuJisekiVo) Implements TehaichoHeaderDao.FindShutuzuJisekiBy

            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_JISEKI TJ WITH (NOLOCK, NOWAIT) ")
                .AppendFormat(" WHERE TJ.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TJ.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND TJ.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
                .AppendFormat(" AND TJ.SHISAKU_BLOCK_NO = '{0}' ", blockNo)
                .AppendFormat(" AND TJ.BUHIN_NO = '{0}' ", buhinNo)
                If descJun Then
                    .AppendFormat(" ORDER BY SHUTUZU_JISEKI_KAITEI_NO DESC ")
                Else
                    .AppendFormat(" ORDER BY SHUTUZU_JISEKI_KAITEI_NO ")
                End If
            End With

            Dim db As New EBomDbClient

            Return db.QueryForList(Of TShisakuTehaiShutuzuJisekiVo)(sql.ToString)

        End Function
#End Region

#End Region

#Region "SELECT 試作手配出図実績手入力情報を返す"
        ''' <summary>
        ''' 試作手配出図実績手入力情報を返す。
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindShutuzuJisekiInputBy(ByVal shisakuEventCode As String, _
                                            ByVal shisakuListCode As String, _
                                            ByVal shisakuListCodeKaiteiNo As String, _
                                            ByVal blockNo As String, _
                                            ByVal buhinNo As String, _
                                            ByVal kaiteiNo As String, _
                                            ByVal maxKaitei As Boolean) As TShisakuTehaiShutuzuJisekiInputVo Implements TehaichoHeaderDao.FindShutuzuJisekiInputBy

            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_JISEKI_INPUT TJ WITH (NOLOCK, NOWAIT) ")
                .AppendFormat(" WHERE TJ.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TJ.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND TJ.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
                .AppendFormat(" AND TJ.SHISAKU_BLOCK_NO = '{0}' ", blockNo)
                .AppendFormat(" AND TJ.BUHIN_NO = '{0}' ", buhinNo)
                If Not maxKaitei Then
                    .AppendFormat(" AND TJ.SHUTUZU_JISEKI_KAITEI_NO = '{0}' ", kaiteiNo)
                Else
                    .AppendLine(" AND TJ.SHUTUZU_JISEKI_KAITEI_NO = ( ")
                    .AppendLine(" SELECT MAX(SHUTUZU_JISEKI_KAITEI_NO) AS SHUTUZU_JISEKI_KAITEI_NO ")
                    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_JISEKI_INPUT  WITH(NOLOCK, NOWAIT)")
                    .AppendFormat(" WHERE TJ.SHISAKU_EVENT_CODE = SHISAKU_EVENT_CODE ")
                    .AppendFormat(" AND TJ.SHISAKU_LIST_CODE = SHISAKU_LIST_CODE ")
                    .AppendFormat(" AND TJ.SHISAKU_LIST_CODE_KAITEI_NO = SHISAKU_LIST_CODE_KAITEI_NO ")
                    .AppendFormat(" AND TJ.SHISAKU_BLOCK_NO = SHISAKU_BLOCK_NO ")
                    .AppendFormat(" AND TJ.BUHIN_NO = BUHIN_NO) ")
                End If
            End With

            Dim db As New EBomDbClient

            Return db.QueryForObject(Of TShisakuTehaiShutuzuJisekiInputVo)(sql.ToString)

        End Function
#End Region


#Region "最新と織込みの差"

#Region "SELECT 試作手配出図実績情報を返す"
        ''' <summary>
        ''' 出図実績の最新と織込みの差を返す。
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindShutuzuJisekiOrikomiSaBy(ByVal shisakuEventCode As String, _
                                            ByVal shisakuListCode As String, _
                                            ByVal shisakuListCodeKaiteiNo As String, _
                                            ByVal shisakuBlockNo As String, _
                                            ByVal gyouId As String, _
                                            ByVal buhinNo As String) As TShisakuTehaiShutuzuOrikomiVo Implements TehaichoHeaderDao.FindShutuzuJisekiOrikomiSaBy

            Dim dtResult As New TShisakuTehaiShutuzuOrikomiVo
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_ORIKOMI TJ WITH (NOLOCK, NOWAIT) ")
                .AppendFormat(" WHERE TJ.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TJ.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND TJ.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
                .AppendFormat(" AND TJ.SHISAKU_BLOCK_NO = '{0}' ", shisakuBlockNo)
                .AppendFormat(" AND TJ.GYOU_ID = '{0}' ", gyouId)
                .AppendFormat(" AND TJ.BUHIN_NO = '{0}' ", buhinNo)
            End With

            Dim db As New EBomDbClient

            Return db.QueryForObject(Of TShisakuTehaiShutuzuOrikomiVo)(sql.ToString)

        End Function
#End Region


        ''' <summary>
        ''' 試作手配出図実績情報を返す。（＋改訂№）
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindShutuzuJisekiKaiteiBy(ByVal shisakuEventCode As String, _
                                            ByVal shisakuListCode As String, _
                                            ByVal shisakuListCodeKaiteiNo As String, _
                                            ByVal blockNo As String, _
                                            ByVal buhinNo As String, _
                                            ByVal kaiteiNo As String) As TShisakuTehaiShutuzuJisekiVo Implements TehaichoHeaderDao.FindShutuzuJisekiKaiteiBy

            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_JISEKI TJ WITH (NOLOCK, NOWAIT) ")
                .AppendFormat(" WHERE TJ.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TJ.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendFormat(" AND TJ.SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", shisakuListCodeKaiteiNo)
                .AppendFormat(" AND TJ.SHISAKU_BLOCK_NO = '{0}' ", blockNo)
                .AppendFormat(" AND TJ.BUHIN_NO = '{0}' ", buhinNo)
                .AppendFormat(" AND TJ.SHUTUZU_JISEKI_KAITEI_NO = '{0}' ", kaiteiNo)
            End With

            Dim db As New EBomDbClient

            Return db.QueryForObject(Of TShisakuTehaiShutuzuJisekiVo)(sql.ToString)

        End Function


#End Region


#Region "出図実績取得"
        ''' <summary>
        ''' タイトル図面が存在するか確認する
        ''' </summary>
        Friend Function FindByT_ZUMEN(ByVal buhinNo As String) As List(Of Rhac0533Vo) Implements TehaichoHeaderDao.FindByT_ZUMEN
            Dim sql As New System.Text.StringBuilder
            With sql
                .Remove(0, .Length)
                .AppendLine("use  BRAKU_DEVELOP " & vbLf & "SELECT P.ZUMEN_NO ")
                .AppendLine("FROM BRAKU_DEVELOP.dbo.T_PARTS P ")
                .AppendLine("INNER JOIN T_ZUMEN Z ")
                .AppendLine("ON Z.ZUMEN_NO = P.ZUMEN_NO ")
                .AppendLine("WHERE  P.BUHIN_NO = @BuhinNo ")
            End With
            '2015/12/23 kabasawa'
            '廃止日みたり最新改訂見たりしていないのは問題ないのか？'

            Dim param As New Rhac0533Vo
            param.BuhinNo = buhinNo

            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac0533Vo)(sql.ToString, param)
        End Function

#End Region


#Region "サイト区分取得"
        ''' <summary>
        ''' サイト区分取得
        ''' </summary>
        ''' <param name="userId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetSiteInfo(ByVal userId As String) As MUserVo Implements TehaichoHeaderDao.GetSiteInfo

            Dim sb As New System.Text.StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT SITE_INFO ")
                .AppendLine(" FROM " & EBOM_DB_NAME & ".dbo.M_USER ")
                .AppendLine("WHERE  USER_ID = @UserId ")
            End With

            Dim paramVo As New MUserVo
            paramVo.UserId = userId

            Dim db As New EBomDbClient
            Dim resultVo = db.QueryForObject(Of MUserVo)(sb.ToString, paramVo)

            Return resultVo



        End Function

#End Region


#Region "java起動"
        '20150420 Edit Watanabe 承認日を見るようにAS400のプログラムを調整。
        '調整後のファイルは別のJarファイルとしてアクセスできるようにしておいた。
        '承認日も見る版⇒getOrpf57ForSKE1_VerShoninDateCheck.jar
        '承認日見ない版⇒getSettsuNoForSKE1.jar


        ''' <summary>JAR実行プロパティファイル</summary>
        Private Const conJarFile As String = "getZumenForSKE1.jar"
        'Private Const conJarFile As String = "getOrpf57ForSKE1_VerShoninDateCheck.jar"
        'Private Const conJarFile As String = "getOrpf57ForSKE1.jar"


        ''' <summary>JAR実行プロパティファイル</summary>
        Private Const conPropertyFile As String = "connect.properties"

        ''' <summary>
        ''' 部品Noリストから設通Noリスト作成
        ''' </summary>
        ''' <param name="BuhinNoList">部品Noのリスト</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByBuhin(ByVal BuhinNoList As List(Of String)) As List(Of Zspf10Vo) Implements TehaichoHeaderDao.FindByBuhin
            Dim inputFileName1 As String = Path.GetTempFileName()
            Dim inputFileName2 As String = Path.GetTempFileName()
            Dim inputFileName3 As String = Path.GetTempFileName()
            Dim outputFileName1 As String = Path.GetTempFileName()
            Dim outputFileName2 As String = Path.GetTempFileName()
            Dim outputFileName3 As String = Path.GetTempFileName()
            Dim progPath As String = My.Application.Info.DirectoryPath
            Dim buff As String = Nothing
            Dim buffSplit() As String
            Dim rtnVal As New List(Of Zspf10Vo)
            Dim vo As Zspf10Vo
            Dim hitBuhinxList As New ArrayList



            '生品番検索
            '引数ファイル作成
            Using writer As New StreamWriter(inputFileName1, True, Encoding.GetEncoding("Shift_JIS"))

                For Each buhinNo As String In BuhinNoList
                    writer.WriteLine(String.Format("{0}{1}{0}", buhinNo, vbTab))
                Next
                writer.Close()
            End Using

            'Java実行
            Dim Process As System.Diagnostics.Process = CreateProccesStartInfo(progPath, _
                                         conJarFile, _
                                         inputFileName1, _
                                         outputFileName1, _
                                         conPropertyFile)
            Process.Start()
            'Dim a As StreamReader = Process.StandardError
            'While a.Peek > -1
            '    Console.WriteLine(a.ReadLine)
            'End While

            Process.BeginOutputReadLine()
            Process.BeginErrorReadLine()
            Process.WaitForExit()


            '出力ファイル読み込み
            Using sr As New System.IO.StreamReader(outputFileName1, Encoding.GetEncoding(932))
                While sr.Peek() > -1
                    buff = sr.ReadLine()
                    buffSplit = Split(buff, vbTab)
                    vo = New Zspf10Vo
                    vo.Gzzcp = buffSplit(0)       '部品番号
                    vo.Tzzmbap = buffSplit(1)       'タイトル図面番号
                    vo.Tzkdbap = buffSplit(2) '図面改訂№
                    vo.Dhstba = buffSplit(3)      '設通№
                    vo.Kxjrdt = buffSplit(4)        '受領日
                    vo.Prfgp = buffSplit(5)  '開発符号
                    'vo.KaihatsuFugo = buffSplit(6)  '出図予定日
                    vo.Kdri = buffSplit(7)  '件名
                    vo.Stsr = buffSplit(8)  '設通シリーズ
                    rtnVal.Add(vo)
                    If hitBuhinxList.Contains(vo.Gzzcp) Then
                        hitBuhinxList.Add(vo.Gzzcp)
                    End If
                End While
                sr.Close()
            End Using

            '##品番検索
            '引数ファイル作成
            Using writer As New StreamWriter(inputFileName2, True, Encoding.GetEncoding("Shift_JIS"))
                For Each buhinNo As String In BuhinNoList
                    If buhinNo.Length < 12 Then
                        Continue For
                    End If
                    If Not hitBuhinxList.Contains(buhinNo) Then
                        writer.WriteLine(String.Format("{0}{1}{2}", _
                                                       buhinNo.Substring(0, buhinNo.Length - 2) & "##", _
                                                       vbTab, _
                                                       buhinNo))
                    End If
                Next
                writer.Close()
            End Using

            ''Java実行
            'si.FileName = "java.exe"
            'si.Arguments = String.Format(" -jar ""{0}\{1}"" ""{2}"" ""{3}"" ""{0}\{4}""", _
            '                             progPath, _
            '                             conJarFile, _
            '                             inputFileName2, _
            '                             outputFileName2, _
            '                             conPropertyFile)
            'si.WindowStyle = ProcessWindowStyle.Hidden
            'si.UseShellExecute = False
            'p = Process.Start(si)
            'p.WaitForExit()

            Process = CreateProccesStartInfo(progPath, _
                                         conJarFile, _
                                         inputFileName2, _
                                         outputFileName2, _
                                         conPropertyFile)
            Process.Start()
            Process.BeginOutputReadLine()
            Process.BeginErrorReadLine()
            Process.WaitForExit()

            '出力ファイル読み込み
            Using sr As New System.IO.StreamReader(outputFileName2, Encoding.GetEncoding(932))
                While sr.Peek() > -1
                    buff = sr.ReadLine()
                    buffSplit = Split(buff, vbTab)
                    vo = New Zspf10Vo
                    vo.Gzzcp = buffSplit(0)       '部品番号
                    vo.Tzzmbap = buffSplit(1)       'タイトル図面番号
                    vo.Tzkdbap = buffSplit(2) '図面改訂№
                    vo.Dhstba = buffSplit(3)      '設通№
                    vo.Kxjrdt = buffSplit(4)        '受領日
                    vo.Prfgp = buffSplit(5)  '開発符号
                    'vo.KaihatsuFugo = buffSplit(6)  '出図予定日
                    vo.Kdri = buffSplit(7)  '件名
                    vo.Stsr = buffSplit(8)  '設通シリーズ
                    rtnVal.Add(vo)
                End While
                sr.Close()
            End Using

            '9桁品番対応（頭にハイフンを付けて検索）
            '引数ファイル作成
            Using writer As New StreamWriter(inputFileName3, True, Encoding.GetEncoding("Shift_JIS"))
                For Each buhinNo As String In BuhinNoList
                    If Not hitBuhinxList.Contains(buhinNo) Then
                        writer.WriteLine(String.Format("{0}{1}{2}", _
                                                       "-" & buhinNo, _
                                                       vbTab, _
                                                       buhinNo))
                    End If
                Next
                writer.Close()
            End Using

            ''Java実行
            'si.FileName = "java.exe"
            'si.Arguments = String.Format(" -jar ""{0}\{1}"" ""{2}"" ""{3}"" ""{0}\{4}""", _
            '                             progPath, _
            '                             conJarFile, _
            '                             inputFileName3, _
            '                             outputFileName3, _
            '                             conPropertyFile)
            'si.WindowStyle = ProcessWindowStyle.Hidden
            'si.UseShellExecute = False
            'p = Process.Start(si)

            'p.WaitForExit()
            Process = CreateProccesStartInfo(progPath, _
                                  conJarFile, _
                                  inputFileName3, _
                                  outputFileName3, _
                                  conPropertyFile)
            Process.Start()
            Process.BeginOutputReadLine()
            Process.BeginErrorReadLine()
            Process.WaitForExit()

            '出力ファイル読み込み
            Using sr As New System.IO.StreamReader(outputFileName3, Encoding.GetEncoding(932))
                While sr.Peek() > -1
                    buff = sr.ReadLine()
                    buffSplit = Split(buff, vbTab)
                    vo = New Zspf10Vo
                    vo.Gzzcp = buffSplit(0)       '部品番号
                    vo.Tzzmbap = buffSplit(1)       'タイトル図面番号
                    vo.Tzkdbap = buffSplit(2) '図面改訂№
                    vo.Dhstba = buffSplit(3)      '設通№
                    vo.Kxjrdt = buffSplit(4)        '受領日
                    vo.Prfgp = buffSplit(5)  '開発符号
                    'vo.KaihatsuFugo = buffSplit(6)  '出図予定日
                    vo.Kdri = buffSplit(7)  '件名
                    vo.Stsr = buffSplit(8)  '設通シリーズ
                    rtnVal.Add(vo)
                End While
                sr.Close()
            End Using


            'ファイル削除
            Dim fi As System.IO.FileInfo
            fi = New System.IO.FileInfo(inputFileName1)
            fi.Delete()
            fi = New System.IO.FileInfo(inputFileName2)
            fi.Delete()
            fi = New System.IO.FileInfo(inputFileName3)
            fi.Delete()
            fi = New System.IO.FileInfo(outputFileName1)
            fi.Delete()
            fi = New System.IO.FileInfo(outputFileName2)
            fi.Delete()
            fi = New System.IO.FileInfo(outputFileName3)
            fi.Delete()

            Return rtnVal
        End Function

        'Private Shared JavaProgName As String = ""
        Private Shared JavaErrorMsg As New System.Text.StringBuilder

        ''' <summary>
        ''' 織込み指示
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CreateProccesStartInfo(ByVal progPath As String, _
                                         ByVal conJarFile As String, _
                                         ByVal inputFileName As String, _
                                         ByVal outputFileName As String, _
                                         ByVal conPropertyFile As String) As System.Diagnostics.Process
            Dim ProcessInfo As New System.Diagnostics.Process
            ProcessInfo.StartInfo.UseShellExecute = False
            ProcessInfo.StartInfo.RedirectStandardOutput = True
            ProcessInfo.StartInfo.RedirectStandardError = True
            AddHandler ProcessInfo.OutputDataReceived, AddressOf p_OutputDataReceived
            AddHandler ProcessInfo.ErrorDataReceived, AddressOf p_ErrorDataReceived
            ProcessInfo.StartInfo.RedirectStandardInput = True
            ProcessInfo.StartInfo.CreateNoWindow = False
            JavaErrorMsg.Remove(0, JavaErrorMsg.Length)

            ProcessInfo.StartInfo.FileName = "java.exe"

            ProcessInfo.StartInfo.Arguments = String.Format(" -jar ""{0}\{1}"" ""{2}"" ""{3}"" ""{0}\{4}""", _
                                         progPath, _
                                         conJarFile, _
                                         inputFileName, _
                                         outputFileName, _
                                         conPropertyFile)

            ProcessInfo.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

            Return ProcessInfo
        End Function

        'OutputDataReceivedイベントハンドラ
        '行が出力されるたびに呼び出される
        Private Shared Sub p_OutputDataReceived(ByVal sender As Object, _
                ByVal e As System.Diagnostics.DataReceivedEventArgs)
            '出力された文字列を表示する
            'g_log.WriteInfo(JavaProgName & ":" & e.Data)
        End Sub
        'ErrorDataReceivedイベントハンドラ
        Private Shared Sub p_ErrorDataReceived(ByVal sender As Object, _
                ByVal e As System.Diagnostics.DataReceivedEventArgs)
            'エラー出力された文字列を表示する
            If StringUtil.IsNotEmpty(e.Data) Then

                'g_log.WriteErr(conJarFile & " ERR>" & e.Data)
                'JavaErrorMsg.AppendLine(e.Data)
            End If
        End Sub


#End Region

#Region "試作手配出図実績へのIO"

        ''' <summary>
        ''' 出図実績情報を取得する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByTShisakuTehaiShutuzuJissekiPk(ByVal ShisakuEventCode As String, _
                                                            ByVal ShisakuListCode As String, _
                                                            ByVal ShisakuListCodeKaiteiNo As String _
                                                            ) As List(Of Vo.TShisakuTehaiShutuzuJisekiVo) Implements TehaichoHeaderDao.FindByTShisakuTehaiShutuzuJisseki

            Dim sql As New System.Text.StringBuilder
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_JISEKI SJ ")
                .AppendLine(" WHERE ")
                .AppendLine(" SJ.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SJ.SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine(" AND SJ.SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo ")
            End With
            '2015/12/23 kabasawa'
            '廃止日みたり最新改訂見たりしていないのは問題ないのか？'

            Dim param As New Vo.TShisakuTehaiShutuzuJisekiVo
            param.ShisakuEventCode = ShisakuEventCode
            param.ShisakuListCode = ShisakuListCode
            param.ShisakuListCodeKaiteiNo = ShisakuListCodeKaiteiNo

            Dim db As New EBomDbClient
            Return db.QueryForList(Of Vo.TShisakuTehaiShutuzuJisekiVo)(sql.ToString, param)
        End Function

#End Region

    End Class
End Namespace