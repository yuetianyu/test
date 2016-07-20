Imports EBom.Common
Imports System.IO
Imports Microsoft.Office.Interop

Namespace Ui

    ''' <summary>
    ''' Excel出力 共通メソッド
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ExcelCommon

        Public Shared Sub SaveExcelFile(ByVal filename As String, _
                                        ByVal spdInfo As FarPoint.Win.Spread.FpSpread, _
                                        ByVal strFlag As String, _
                                        Optional ByVal OpenOrClose As String = Nothing)
            Dim systemDrive As String = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            Dim sfd As New SaveFileDialog()

            ' ファイル名を指定します

            sfd.FileName = filename
            ' 起動ディレクトリを指定します

            'sfd.InitialDirectory = systemDrive
            sfd.InitialDirectory = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir   '2012/02/08 Excel出力ディレクトリ指定対応
            '  [ファイルの種類] ボックスに表示される選択肢を指定します

            sfd.Filter = "Excel files(*.xls)|*.xls|XML files(*.xml)|*.xml"
            ' ダイアログを表示します
            Try
                spdInfo.ActiveSheet.Protect = False

                '--------------------------------------------------
                'EXCEL出力先フォルダチェック
                ShisakuCommon.ShisakuComFunc.ExcelOutPutDirCheck()
                '--------------------------------------------------

                If strFlag = "Shakei" Then
                    spdInfo.SaveExcel(sfd.InitialDirectory + "\" + sfd.FileName + ".xls", FarPoint.Win.Spread.Model.IncludeHeaders.None)
                Else
                    spdInfo.SaveExcel(sfd.InitialDirectory + "\" + sfd.FileName + ".xls", FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly)
                End If
                'Nothingだったら自動で開く（2013/06/18時点：AL再展開からのみパラメータセット）
                If StringUtil.Equals(OpenOrClose, Nothing) Then
                    Process.Start(sfd.InitialDirectory + "\" + sfd.FileName + ".xls")
                End If

            Catch ex As Exception
                MsgBox("出力に失敗しました")
                'Message (Excel出力しました。)
                ComFunc.ShowInfoMsgBox(T0010)

            Finally
                spdInfo.ActiveSheet.Protect = True
            End Try

            sfd.Dispose()
        End Sub

        Public Sub ExportToExcel(ByVal dt As DataTable)

            If dt Is Nothing Then
                Exit Sub
            End If
            If dt.Rows.Count = 0 Then
                Exit Sub
            End If

            Dim xlApp As Excel.Application
            xlApp = New Excel.Application
            If xlApp Is Nothing Then
                Exit Sub
            End If

            'Creat EXCEL
            Dim workbooks As Excel.Workbooks
            workbooks = xlApp.Workbooks

            Dim workbook As Excel.Workbook
            workbook = workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet)

            Dim worksheet As Excel.Worksheet
            worksheet = workbook.Worksheets(1)

            Dim range As Excel.Range
            range = Nothing

            Dim totalCount As Integer
            totalCount = dt.Rows.Count

            Dim rowRead As Integer
            rowRead = 0

            'Title
            worksheet.Name = "Loginマスタ"

            Dim i As Integer

            For i = 0 To dt.Columns.Count - 1
                worksheet.Cells(1, i + 1) = dt.Columns(i).ColumnName
                range = worksheet.Cells(1, i + 1)
                'Black Color
                range.Interior.ColorIndex = 15
                'Font Type
                range.Font.Bold = True
                'HAlignCenter
                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                'Border
                range.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic)
                'Width
                range.ColumnWidth = 4.63
                'Auto Width
                range.EntireColumn.AutoFit()
                'Auto HEITH
                range.EntireRow.AutoFit()
            Next

            'Write Data
            Dim j As Integer
            For j = 0 To dt.Rows.Count - 1
                For i = 0 To dt.Columns.Count - 1
                    worksheet.Cells(j + 2, i + 1) = dt.Rows(j)(i)
                    range = worksheet.Cells(j + 2, i + 1)
                    'Font Size
                    range.Font.Size = 9
                    'Line
                    range.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic)
                    'Auto Width
                    range.EntireColumn.AutoFit()
                Next
            Next
            'SAVE EXCEL Directory
            Try
                Dim tPath As String
                tPath = System.AppDomain.CurrentDomain.BaseDirectory
                If Not Directory.Exists(tPath + "Excel") Then
                    Directory.CreateDirectory(tPath + "Excel")
                End If
                'workbook.SaveCopyAs(tPath + "Excel" + "\" + System.DateTime.Today.ToString("yyyyMMdd") + "Loginマスタ.xls")
                workbook.SaveAs(tPath + "Excel" + "\" + System.DateTime.Today.ToString("yyyyMMdd") + "Loginマスタ.xls")

            Finally
                'EXIT EXCEL
                Try
                    If Not (xlApp Is Nothing) Then
                        killprocess("Excel")
                    End If
                Catch ex As Exception
                    '' ここの例外は無視する by T.Homma
                    Console.WriteLine("delete excel process error:" + ex.Message)
                End Try
            End Try
        End Sub

        '  Declare Function GetWindowThreadProcessId Lib "user32" (ByVal hwnd As Long, ByVal lpdwProcessId As Long) As Long
        'COLSE EXCEL PROCESS
        Private Sub killprocess(ByVal processname As String)
            Dim myproc As System.Diagnostics.Process
            myproc = New System.Diagnostics.Process
            For Each thisproc As Process In Process.GetProcessesByName(processname)
                If Not thisproc.CloseMainWindow Then
                    thisproc.Kill()
                End If
            Next
        End Sub

    End Class
End Namespace