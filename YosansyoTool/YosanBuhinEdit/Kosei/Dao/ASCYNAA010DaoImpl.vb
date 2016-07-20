Imports System
Imports System.IO
Imports System.Text
Imports ShisakuCommon
Imports EBom.Common

Namespace YosanBuhinEdit.Kosei.Dao
    Public Class ASCYNAA010DaoImpl
        Implements ASCYNAA010Dao

        Public Function FindByKoujishirei(ByVal koujiShireiNo As String) As List(Of ASCYNAA010Vo) Implements ASCYNAA010Dao.FindByKoujishirei
            Dim inputFileName As String = Path.GetTempFileName()
            Dim outputFileName As String = Path.GetTempFileName()
            Dim progPath As String = My.Application.Info.DirectoryPath
            Dim buff As String = ""
            Dim buffSplit() As String
            Dim rtnVal As New List(Of ASCYNAA010Vo)

            '引数ファイル作成
            Using writer As New StreamWriter(inputFileName, True, Encoding.GetEncoding("Shift_JIS"))
                writer.WriteLine(koujiShireiNo)
                writer.Close()
            End Using

            'Java実行
            Dim p As New System.Diagnostics.Process
            JavaProgName = "getHachuJisekiListForSKE1.jar"
            p.StartInfo.UseShellExecute = False
            p.StartInfo.RedirectStandardOutput = True
            p.StartInfo.RedirectStandardError = True
            AddHandler p.OutputDataReceived, AddressOf p_OutputDataReceived
            AddHandler p.ErrorDataReceived, AddressOf p_ErrorDataReceived
            p.StartInfo.RedirectStandardInput = True
            p.StartInfo.CreateNoWindow = True
            JavaErrorMsg.Remove(0, JavaErrorMsg.Length)


            p.StartInfo.FileName = "java.exe"
            p.StartInfo.Arguments = String.Format(" -jar ""{0}\getHachuJisekiListForSKE1.jar"" ""{1}"" ""{2}"" ""{0}\connect.properties""", progPath, inputFileName, outputFileName)
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
            Using sr As New System.IO.StreamReader(outputFileName, Encoding.GetEncoding(932))
                While sr.Peek() > -1
                    buff = sr.ReadLine()
                    buffSplit = Split(buff, vbTab)
                    Dim vo As New ASCYNAA010Vo
                    vo.BUBA15 = buffSplit(0).Trim
                    vo.MIMXBH = Decimal.Parse(buffSplit(1))
                    vo.MIMXKH = Decimal.Parse(buffSplit(2))
                    vo.NOYM = Integer.Parse(buffSplit(3))
                    rtnVal.Add(vo)
                End While
                sr.Close()
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

    End Class
End Namespace
