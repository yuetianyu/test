'遅延バインディングを使用しているため OFF
Option Strict Off

Imports EBom.Excel
Imports Microsoft.Office.Interop.Excel

Namespace Ui
#Region " クラス定義 "

    ''' <summary>
    ''' EBom.Exceの Excelクラス拡張
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShisakuExcel : Inherits Excel

#Region "コンストラクタ"
        ''' <summary>
        ''' Construct
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New()
        End Sub
        ''' <summary>
        ''' Construct
        ''' </summary>
        ''' <param name="fileName">目的ExcelファイルPath+Name</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal fileName As String)
            MyBase.New(fileName)
        End Sub
#End Region

#Region "orientation設定"

        ''' <summary>
        ''' orientation設定
        ''' </summary>
        ''' <param name="startCol">開始列インデックス</param>
        ''' <param name="startRow">開始行インデックス</param>
        ''' <param name="endCol">終了列インデックス</param>
        ''' <param name="endRow">終了行インデックス</param>
        ''' <param name="orientation">orientation設定</param>
        ''' <param name="wrap">折り返し[省略可]</param>
        ''' <param name="fit">縮小[省略可]</param>
        ''' <remarks></remarks>
        Public Sub SetOrientation(ByVal startCol As Integer, ByVal startRow As Integer, _
                                          ByVal endCol As Integer, ByVal endRow As Integer, _
                                          ByVal orientation As XlOrientation, _
                                          Optional ByVal wrap As Boolean = False, Optional ByVal fit As Boolean = False)
            Dim xlRange As Object = Nothing
            Try
                xlRange = Me.GetCell(startCol, startRow, endCol, endRow)
                xlRange.Orientation = orientation
                xlRange.WrapText = wrap
                xlRange.ShrinkToFit = fit
            Finally
                Me.Free(xlRange)
            End Try
        End Sub
#End Region

#Region "合併セールの列数を貰います"
        ''' <summary>
        ''' 合併セールの列数を貰います
        ''' </summary>
        ''' <param name="startCol">開始列インデックス</param>
        ''' <param name="startRow">開始行インデックス</param>
        ''' <param name="endCol">終了列インデックス</param>
        ''' <param name="endRow">終了行インデックス</param>
        ''' <returns>合併セールの列数</returns>
        ''' <remarks></remarks>
        Public Function GetMergedCellsColumnCount(ByVal startCol As Integer, ByVal startRow As Integer, _
                                      Optional ByVal endCol As Integer = -1, Optional ByVal endRow As Integer = -1) As Integer
            Dim xlRange As Object = Nothing
            Dim xlCell As Object = Nothing
            Dim xlMarea As Object = Nothing
            Dim xlColumns As Object = Nothing

            Try
                xlCell = GetCell(startCol, startRow, endCol, endRow)
                xlRange = m_activeSheet.Range(xlCell, xlCell)
                xlMarea = xlRange.MergeArea()
                xlColumns = xlMarea.Columns
                Return xlColumns.Count
            Finally
                Me.Free(xlColumns)
                Me.Free(xlMarea)
                Me.Free(xlRange)
                Me.Free(xlCell)
            End Try
        End Function
#End Region
        ''↓↓2014/09/10 Ⅰ.8.号車別仕様書 作成機能 酒井 ADD BEGIN
        Public Function GetMergedCellsRowCount(ByVal startCol As Integer, ByVal startRow As Integer, _
                                      Optional ByVal endCol As Integer = -1, Optional ByVal endRow As Integer = -1) As Integer
            Dim xlRange As Object = Nothing
            Dim xlCell As Object = Nothing
            Dim xlMarea As Object = Nothing
            Dim xlRows As Object = Nothing

            Try
                xlCell = GetCell(startCol, startRow, endCol, endRow)
                xlRange = m_activeSheet.Range(xlCell, xlCell)
                xlMarea = xlRange.MergeArea()
                xlRows = xlMarea.Rows
                Return xlRows.Count
            Finally
                Me.Free(xlRows)
                Me.Free(xlMarea)
                Me.Free(xlRange)
                Me.Free(xlCell)
            End Try
        End Function
        Public Sub CopyFont(ByVal motoCol As Integer, ByVal motoRow As Integer, _
                           ByVal sakiCol As Integer, ByVal sakiRow As Integer)

            Dim motoxlRange As Object = Nothing
            Dim motoxlFont As Object = Nothing
            Dim sakixlRange As Object = Nothing
            Dim sakixlFont As Object = Nothing

            Try
                motoxlRange = Me.GetCell(motoCol, motoRow)
                motoxlFont = motoxlRange.Font

                sakixlRange = Me.GetCell(sakiCol, sakiRow)
                sakixlFont = sakixlRange.Font

                sakixlFont.Name = motoxlFont.Name
                sakixlFont.Size = motoxlFont.Size
                sakixlFont.Color = motoxlFont.Color
                sakixlFont.Bold = motoxlFont.Bold
                sakixlFont.Italic = motoxlFont.Italic
                sakixlFont.Strikethrough = motoxlFont.Strikethrough
                sakixlFont.Subscript = motoxlFont.Subscript
                sakixlFont.Superscript = motoxlFont.Superscript
                sakixlFont.Shadow = motoxlFont.Shadow

            Finally
                Me.Free(motoxlFont)
                Me.Free(motoxlRange)
                Me.Free(sakixlFont)
                Me.Free(sakixlRange)
            End Try
        End Sub
        ''↓↓2014/09/11 Ⅰ.8.号車別仕様書 作成機能 酒井 ADD BEGIN
        Public Function GetPageBreaks() As List(Of Integer)
            Dim tmp As New List(Of Integer)

                For i As Integer = 1 To m_activeSheet.HPageBreaks.Count
                    tmp.Add(m_activeSheet.HPageBreaks(i).Location.Row)
                Next
                Return tmp
        End Function
        Public Sub CopySheetRowInsert2(ByVal sourceSheet As Integer, ByVal targetSheet As Integer, _
                                ByVal sourceStartRowIdx As Integer, ByVal targetStartRowIdx As Integer, _
                                ByVal rowCount As Integer)

            Dim xlSheets As Object = Nothing
            Dim xlSheet1 As Object = Nothing
            Dim xlsheet2 As Object = Nothing

            Dim rangeStRow1 As Object = Nothing
            Dim rangeEdRow1 As Object = Nothing

            Dim rangeStRow2 As Object = Nothing
            Dim rangeEdRow2 As Object = Nothing

            Dim range1 As Object = Nothing
            Dim range2 As Object = Nothing

            xlSheets = m_xlBook.Sheets
            xlSheet1 = xlSheets(targetSheet)
            xlsheet2 = xlSheets(sourceSheet)

            Try
                'アクティブシートをコピー先へ
                Me.SetActiveSheet(targetSheet)
                Me.UnProtectSheet()

                'アクティブシートをコピー元へ
                Me.SetActiveSheet(sourceSheet)

                'コピー元
                rangeStRow2 = xlsheet2.Rows(sourceStartRowIdx)
                rangeEdRow2 = xlsheet2.Rows(sourceStartRowIdx + rowCount)
                range2 = xlsheet2.Range(rangeStRow2, rangeEdRow2)
                range2.Copy()

                'コピー先
                rangeStRow1 = xlSheet1.Rows(targetStartRowIdx)
                rangeEdRow1 = xlSheet1.Rows(targetStartRowIdx + rowCount)
                range1 = xlSheet1.Range(rangeStRow1, rangeEdRow1)
                range1.PasteSpecial()

                'アクティブシートをコピー先へ
                Me.SetActiveSheet(targetSheet)

                '選択を戻す
                m_xlApp.CutCopyMode = False

            Finally
                Me.Free(rangeEdRow1)
                Me.Free(rangeStRow1)
                Me.Free(rangeEdRow2)
                Me.Free(rangeStRow2)

                Me.Free(range2)
                Me.Free(range1)
                xlsheet2 = Nothing
                xlSheet1 = Nothing
                Me.Free(xlsheet2)
                Me.Free(xlSheet1)
                Me.Free(xlSheets)
            End Try
        End Sub
        ''↑↑2014/09/11 Ⅰ.8.号車別仕様書 作成機能 酒井 ADD END
#Region "２次元排列をExcel出力します"
        ''' <summary>
        ''' 範囲を指定してデータを貼り付け
        ''' </summary>
        ''' <param name="startCol">開始列</param>
        ''' <param name="startRow">開始行</param>
        ''' <param name="endCol">終了列</param>
        ''' <param name="endRow">終了行</param>
        ''' <param name="dt">データ配列</param>
        ''' <remarks></remarks>
        Public Sub CopyRange(ByVal startCol As Integer, ByVal startRow As Integer, ByVal endCol As Integer, _
                                                                        ByVal endRow As Integer, ByVal dt As Object)

            Dim xlRange As Object = Nothing

            Try
                Dim cellPosition As String = ConvertToLetter(startCol + 1) & startRow.ToString & ":" & ConvertToLetter(endCol + 1) & endRow.ToString
                xlRange = m_activeSheet.Range(cellPosition)
                xlRange.value = dt
            Catch
                Throw
            Finally
                Me.Free(xlRange)
            End Try
        End Sub

#End Region
#Region "シートを削除"
        ''' <summary>
        ''' シートを削除
        ''' </summary>
        ''' <param name="sheetIndex">目的シートのIndex</param>
        ''' <remarks></remarks>
        Public Sub DeleteSheetTrialManufacture(ByVal sheetIndex As Integer, Optional ByVal flg As Boolean = False)
            '↓↓2014/10/17 酒井 ADD BEGIN
            '        Public Sub DeleteSheet(ByVal sheetIndex As Integer)
            '↑↑2014/10/17 酒井 ADD END
            Dim xlSheets As Object = Nothing
            Dim XSheet1 As Object = Nothing
            Try
                xlSheets = m_xlBook.Sheets
                XSheet1 = xlSheets.Item(sheetIndex)
                m_xlBook.Activate()
                '↓↓2014/10/17 酒井 ADD BEGIN
                If flg Then
                    m_xlApp.DisplayAlerts = False
                End If
                '↑↑2014/10/17 酒井 ADD END
                XSheet1.Delete()
                '↓↓2014/10/17 酒井 ADD BEGIN
                If flg Then
                    m_xlApp.DisplayAlerts = True
                End If
                '↑↑2014/10/17 酒井 ADD END
            Finally
                Me.Free(XSheet1)
                Me.Free(xlSheets)
            End Try
        End Sub
#End Region

#Region "別のワークブックのシートをコピーする"

        ''' <summary>
        ''' 別のワークブックのシートをコピーする
        ''' </summary>
        ''' <param name="copyFileName">コピー対象ファイル名称</param>
        ''' <param name="copySheetIndex">コピー対象シート位置</param>
        ''' <returns>成否</returns>
        ''' <remarks></remarks>
        Public Function SheetCopy(ByVal copyFileName As String, ByVal copySheetIndex As Integer) As Boolean

            Dim ret As Boolean = False
            Dim xlCopyBooks As Object = Nothing
            Dim xlCopyBook As Object = Nothing

            Try
                'ファイル存在確認
                If System.IO.File.Exists(copyFileName) Then
                    xlCopyBooks = m_xlApp.Workbooks
                    xlCopyBook = xlCopyBooks.Open(copyFileName, True)

                    'シートコピー実行
                    xlCopyBook.Sheets(1).Copy(after:=m_xlBook.Sheets(copySheetIndex))
                    ret = True
                Else
                    MsgBox("コピー対象として指定されたブックが存在しません", MsgBoxStyle.Critical, "エラー")
                    ret = False
                End If

                Return ret

            Finally
                Me.Free(xlCopyBook)
                Me.Free(xlCopyBooks)

            End Try
            Return ret
        End Function
#End Region

#Region "ExcelのWorkBookをクリア"
        ''' <summary>
        ''' ExcelのWorkBookをクリア
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ClearWorkBook()

            Dim xlSheets As Object = Nothing
            Dim sheetCount As Integer

            Try
                xlSheets = m_xlBook.Sheets
                sheetCount = xlSheets.Count()
                AddSheet()

                While xlSheets.Count() > 1
                    DeleteSheetTrialManufacture(1)
                End While

                AddSheet()
                AddSheet()
                SetActiveSheet(1)
                SetSheetName("Sheet1")
                SetActiveSheet(2)
                SetSheetName("Sheet2")
                SetActiveSheet(3)
                SetSheetName("Sheet3")
                SetActiveSheet(1)

            Finally
                Me.Free(xlSheets)
            End Try
        End Sub

        ''' <summary>
        ''' シートを削除
        ''' </summary>
        ''' <param name="sheetIndex">目的シートのIndex</param>
        ''' <remarks>制作一覧のdllから参照されるメソッド</remarks>
        Public Sub DeleteSheet(ByVal sheetIndex As Integer)
            Dim xlSheets As Object = Nothing
            Dim XSheet1 As Object = Nothing
            Try
                xlSheets = m_xlBook.Sheets
                XSheet1 = xlSheets.Item(sheetIndex)
                m_xlBook.Activate()
                XSheet1.Delete()
            Finally
                Me.Free(XSheet1)
                Me.Free(xlSheets)
            End Try
        End Sub

#End Region

#Region "シート保護解除"
        ''' <summary>
        ''' シート保護を解除
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub UnProtectSheet()
            m_activeSheet.Unprotect()
        End Sub
#End Region

#Region "指定された行を非表示にする"
        ''' <summary>
        ''' 指定された行を非表示にする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub HiddenRow(ByVal hidden As Boolean, ByVal stRow As Integer, Optional ByVal endRow As Integer = -1)
            Dim xCells As Object = Nothing
            Dim XEntire As Object = Nothing

            Try
                If endRow = -1 Then
                    xCells = Me.GetCell(1, stRow, 1, stRow)
                Else
                    xCells = Me.GetCell(1, stRow, 1, endRow)
                End If

                XEntire = xCells.EntireRow

                If hidden = True Then
                    XEntire.Hidden = True
                Else
                    XEntire.Hidden = False
                End If

            Finally
                Me.Free(XEntire)
                Me.Free(xCells)
            End Try
        End Sub
#End Region

#Region "XlOrientation"
        ''' <summary>
        ''' 文字列の向きを指定します
        ''' </summary>
        Public Enum XlOrientation
            ''' <summary>下向き</summary>
            xlDownward = -4170
            ''' <summary>水平</summary>
            xlHorizontal = -4128
            ''' <summary>上向き</summary>
            xlUpward = -4171
            ''' <summary>垂直 (縦書き)</summary>
            xlVertical = -4166
        End Enum
#End Region

        '20111001 配置設定追加
#Region " 配置設定 "
        ''' <summary>
        ''' 指定されたセル範囲の配置設定を行います.
        ''' </summary>
        ''' <param name="startCol">開始列インデックス</param>
        ''' <param name="startRow">開始行インデックス</param>
        ''' <param name="endCol">終了列インデックス</param>
        ''' <param name="endRow">終了行インデックス</param>
        ''' <param name="hAlign">横配置[省略可]</param>
        ''' <param name="vAlign">縦配置[省略可]</param>
        ''' <param name="wrap">折り返し[省略可]</param>
        ''' <param name="fit">縮小[省略可]</param>
        ''' <remarks></remarks>
        ''' <history>
        '''     T.Hirasawa    2009/02/23    新規作成
        ''' </history>
        Public Shadows Sub SetAlignment(ByVal startCol As Integer, ByVal startRow As Integer, _
                                ByVal endCol As Integer, ByVal endRow As Integer, _
                                Optional ByVal hAlign As XlHAlign = XlHAlign.xlHAlignGeneral, _
                                Optional ByVal vAlign As XlVAlign = XlVAlign.xlVAlignCenter, _
                                Optional ByVal wrap As Boolean = False, Optional ByVal fit As Boolean = False)

            Dim xlRange As Object = Nothing

            Try
                xlRange = Me.GetCell(startCol, startRow, endCol, endRow)
                xlRange.HorizontalAlignment = hAlign
                xlRange.VerticalAlignment = vAlign
                xlRange.WrapText = wrap
                xlRange.ShrinkToFit = fit

            Finally
                Me.Free(xlRange)
            End Try
        End Sub

        ''' <summary>
        ''' 指定されたセルの配置設定を行います.
        ''' </summary>
        ''' <param name="col">開始列インデックス</param>
        ''' <param name="row">開始行インデックス</param>
        ''' <param name="hAlign">横配置[省略可]</param>
        ''' <param name="vAlign">縦配置[省略可]</param>
        ''' <param name="wrap">折り返し[省略可]</param>
        ''' <param name="fit">縮小[省略可]</param>
        ''' <remarks></remarks>
        ''' <history>
        '''     T.Hirasawa    2009/02/23    新規作成
        ''' </history>
        Public Shadows Sub SetAlliment(ByVal col As Integer, ByVal row As Integer, _
                               Optional ByVal hAlign As XlHAlign = XlHAlign.xlHAlignGeneral, _
                               Optional ByVal vAlign As XlVAlign = XlVAlign.xlVAlignCenter, _
                               Optional ByVal wrap As Boolean = False, Optional ByVal fit As Boolean = False)

            Me.SetAlignment(col, row, -1, -1, hAlign, vAlign, wrap, fit)
        End Sub

        Public Shadows Sub SetAlignmentCol(ByVal startCol As Integer, ByVal endCol As Integer, _
                                   Optional ByVal hAlign As XlHAlign = XlHAlign.xlHAlignGeneral, _
                                   Optional ByVal vAlign As XlVAlign = XlVAlign.xlVAlignCenter, _
                                   Optional ByVal wrap As Boolean = False, Optional ByVal fit As Boolean = False)

            Dim xlRange As Object = Nothing

            Try
                xlRange = Me.GetColumn(startCol, endCol)
                xlRange.HorizontalAlignment = hAlign
                xlRange.VerticalAlignment = vAlign
                xlRange.WrapText = wrap
                xlRange.ShrinkToFit = fit

            Finally
                Me.Free(xlRange)
            End Try
        End Sub

        Public Shadows Sub SetAlignmentCol(ByVal col As Integer, _
                                   Optional ByVal hAlign As XlHAlign = XlHAlign.xlHAlignGeneral, _
                                   Optional ByVal vAlign As XlVAlign = XlVAlign.xlVAlignCenter, _
                                   Optional ByVal wrap As Boolean = False, Optional ByVal fit As Boolean = False)

            Dim xlRange As Object = Nothing

            Try
                xlRange = Me.GetColumn(col)
                xlRange.HorizontalAlignment = hAlign
                xlRange.VerticalAlignment = vAlign
                xlRange.WrapText = wrap
                xlRange.ShrinkToFit = fit

            Finally
                Me.Free(xlRange)
            End Try
        End Sub
#End Region

        '20111001 横配置追加
#Region " 横配置 "

        ''' <summary>横配置</summary>
        Public Enum XlHAlign
            ''' <summary>標準</summary>
            xlHAlignGeneral = 1
            ''' <summary>繰り返し</summary>
            xlHAlignFill = 5
            ''' <summary>選択範囲内</summary>
            xlHAlignCenterAcrossSelection = 7
            ''' <summary>中央揃え</summary>
            xlHAlignCenter = -4108
            ''' <summary>均等割り付け</summary>
            xlHAlignDistributed = -4117
            ''' <summary>両端揃え</summary>
            xlHAlignJustify = -4130
            ''' <summary>左詰め</summary>
            xlHAlignLeft = -4131
            ''' <summary>右詰め</summary>
            xlHAlignRight = -4152
        End Enum

#End Region

#Region " 縦配置 "

        ''' <summary>縦配置</summary>
        Public Enum XlVAlign
            ''' <summary>下詰め</summary>
            xlVAlignBottom = -4107
            ''' <summary>中央揃え</summary>
            xlVAlignCenter = -4108
            ''' <summary>均等割り付け</summary>
            xlVAlignDistributed = -4117
            ''' <summary>両端揃え</summary>
            xlVAlignJustify = -4130
            ''' <summary>上詰め</summary>
            xlVAlignTop = -4160
        End Enum

#End Region

#Region "シート間列コピー"
        ''' <summary>
        ''' シート間列コピー
        ''' </summary>
        ''' <param name="baseSheet">コピー元シートINDEX</param>
        ''' <param name="insertSheet">コピー先シートINDEX</param>
        ''' <param name="baseColIdx">コピー元開始列</param>
        ''' <param name="insertColIdx">列数</param>
        ''' <param name="count"></param>
        ''' <remarks></remarks>
        Public Sub CopySheetColInsert(ByVal baseSheet As Integer, ByVal insertSheet As Integer, _
                                                                        ByVal baseColIdx As Integer, ByVal insertColIdx As Integer, ByVal count As Integer)
            Dim xlSheets As Object = Nothing
            Dim xlSheet1 As Object = Nothing
            Dim xlsheet2 As Object = Nothing

            Dim rangeStCol2 As Object = Nothing
            Dim rangeEdCol2 As Object = Nothing

            Dim rangeStCol1 As Object = Nothing
            Dim rangeEdCol1 As Object = Nothing

            Dim range1 As Object = Nothing
            Dim range2 As Object = Nothing

            xlSheets = m_xlBook.Sheets
            xlSheet1 = xlSheets(insertSheet)
            xlsheet2 = xlSheets(baseSheet)

            Try
                'アクティブシートをコピー先へ
                Me.SetActiveSheet(insertSheet)
                Me.UnProtectSheet()

                'アクティブシートをコピー元へ
                Me.SetActiveSheet(baseSheet)

                'コピー元
                rangeStCol2 = xlsheet2.Columns(baseColIdx)
                rangeEdCol2 = xlsheet2.Columns(baseColIdx + count)
                range2 = xlsheet2.Range(rangeStCol2, rangeEdCol2)
                range2.Copy()

                'コピー先
                rangeStCol1 = xlSheet1.Columns(insertColIdx)
                rangeEdCol1 = xlSheet1.Columns(insertColIdx + count)
                range1 = xlSheet1.Range(rangeStCol1, rangeEdCol1)
                range1.PasteSpecial()

                'アクティブシートをコピー先へ
                Me.SetActiveSheet(insertSheet)

                '選択を戻す
                m_xlApp.CutCopyMode = False

            Finally
                Me.Free(rangeEdCol1)
                Me.Free(rangeStCol1)
                Me.Free(rangeEdCol2)
                Me.Free(rangeStCol2)

                Me.Free(range2)
                Me.Free(range1)
                Me.Free(xlsheet2)
                Me.Free(xlSheet1)
                Me.Free(xlSheets)
            End Try
        End Sub
#End Region

#Region "単一アクティブセルを選択"
        ''' <summary>
        ''' 単一アクティブセルを選択
        ''' </summary>
        ''' <param name="aSheetIndex">対象シートNo</param>
        ''' <param name="aCellAdr">単一セルアドレス(A1形式)</param>
        ''' <remarks></remarks>
        Public Sub SetActiveCell(ByVal aSheetIndex As Integer, ByVal aCellAdr As String)
            Dim xlSheets As Object = Nothing
            Dim xlSheet1 As Object = Nothing
            Dim xlsSelect1 As Object = Nothing

            Try

                xlSheets = m_xlBook.Sheets
                xlSheet1 = xlSheets(aSheetIndex)

                xlsSelect1 = xlSheet1.Range(aCellAdr)
                xlsSelect1.select()

            Finally
                Me.Free(xlsSelect1)
                Me.Free(xlSheet1)
                Me.Free(xlSheets)
            End Try

        End Sub
#End Region

#Region "印刷範囲を指定する"

        ''' <summary>
        ''' 印刷範囲を指定する
        ''' </summary>
        ''' <param name="FileName">対象ファイル名</param>
        ''' <param name="SheetIndex">対象シート位置</param>
        ''' <param name="endrow">終了行</param>
        ''' <remarks></remarks>
        Public Sub PrintRange(ByVal FileName As String, ByVal SheetIndex As Integer, ByVal endCol As Integer, ByVal endRow As Integer)

            'Dim ret As Boolean = False
            Dim workbook As Object = Nothing
            Dim endAlphabet As String
            Dim range As String
            Dim printSetup As Object = Nothing

            '2012/02/03
            '印刷範囲の指定を一時やめる
            Try
                'ファイル存在確認
                '2012/02/03 ファイルの有無を見ない
                'If System.IO.File.Exists(FileName) Then
                'xlCopyBooks = m_xlApp.Workbooks
                'xlCopyBook = xlCopyBooks.Open(FileName, True)

                workbook = m_xlBook.sheets()
                'シートの選択
                m_activeSheet = workbook.Item(SheetIndex)
                'm_xlBook.Activate()
                'workbook.Activate()

                'エクセルは1スタートなので'
                endAlphabet = EzUtil.ConvIndexToAlphabet(endCol + 1) + endRow.ToString
                range = "A1:" & endAlphabet

                printSetup = m_activeSheet.pagesetup
                printSetup.printarea = range

                'm_activeSheet.pagesetup.printarea = range
                'workbook.pagesetup().printarea = "A1:C10"

                'シートコピー実行
                'xlCopyBook.Sheets(1).Copy(after:=m_xlBook.Sheets(SheetIndex))
                'ret = True
                'Else
                'ここでエラーが発生している
                'MsgBox("範囲の指定に失敗しました", MsgBoxStyle.Critical, "エラー")
                'ret = False
                'End If

                'Return ret

            Finally
                Me.Free(printSetup)
                Me.Free(workbook)
                Me.Free(m_activeSheet)
                'Me.Free(m_xlBook)
            End Try
            'Return ret
        End Sub

#End Region

#Region "印刷用拡大率を変更する"

        ''' <summary>
        ''' 印刷用拡大率を変更する
        ''' </summary>
        ''' <param name="FileName">対象ファイル名</param>
        ''' <param name="SheetIndex">対象シート位置</param>
        ''' <param name="ZoomValue">拡大率(10%～400%まで)</param>
        ''' <remarks></remarks>
        Public Sub PrintZoom(ByVal FileName As String, ByVal SheetIndex As Integer, ByVal ZoomValue As Integer)
            Dim workbook As Object = Nothing
            Dim pageSetup As Object = Nothing
            Try
                '2012/02/03 ファイルの有無を見ない
                'ファイル存在確認
                'If System.IO.File.Exists(FileName) Then
                workbook = m_xlBook.sheets()
                'シートの選択
                m_activeSheet = workbook.Item(SheetIndex)
                'm_xlBook.Activate()

                pageSetup = m_activeSheet.pagesetup
                pageSetup.Zoom = ZoomValue

                'Else
                'MsgBox("失敗しました", MsgBoxStyle.Critical, "エラー")

                'End If
            Finally
                Me.Free(pageSetup)
                Me.Free(workbook)
                Me.Free(m_activeSheet)
            End Try
        End Sub

#End Region

#Region "印刷の向きを指定する"

        ''' <summary>
        ''' 印刷の向きを指定する
        ''' </summary>
        ''' <param name="FileName">対象ファイル名</param>
        ''' <param name="SheetIndex">対象シート位置</param>
        ''' <param name="Page">何枚で出力するか(0なら設定しない)</param>
        ''' <param name="Flag">縦ならTrue,横ならFalse</param>
        ''' <remarks></remarks>
        Public Sub PrintOrientation(ByVal FileName As String, ByVal SheetIndex As Integer, ByVal Page As Integer, ByVal Flag As Boolean)
            Dim workbook As Object = Nothing
            Dim pageSetup As Object = Nothing

            Try
                'ファイル存在確認
                'これが存在すると高確率で失敗する'
                'If System.IO.File.Exists(FileName) Then
                workbook = m_xlBook.sheets()
                'シートの選択
                m_activeSheet = workbook.Item(SheetIndex)
                'm_xlBook.Activate()

                pageSetup = m_activeSheet.pagesetup

                If Flag Then
                    '縦'
                    pageSetup.Zoom = False
                    pageSetup.Orientation = XlPageOrientation.xlPortrait
                    '枚数設定'
                    If Page > 0 Then
                        pageSetup.FitToPagesWide = Page
                    End If

                Else
                    '横'
                    pageSetup.Zoom = False
                    pageSetup.Orientation = XlPageOrientation.xlLandscape
                    '枚数設定'
                    If Page > 0 Then
                        pageSetup.FitToPagesWide = Page
                    End If
                End If
                'Else
                'MsgBox("失敗しました", MsgBoxStyle.Critical, "エラー")
                'End If
            Finally
                Me.Free(pageSetup)
                Me.Free(workbook)
                Me.Free(m_activeSheet)
            End Try
        End Sub

#End Region

#Region "印刷用紙の設定をする"

        ''' <summary>
        ''' 印刷用紙の設定をする(まだA3とA4のみ)
        ''' </summary>
        ''' <param name="FileName">対象ファイル名</param>
        ''' <param name="SheetIndex">対象シート位置</param>
        ''' <param name="PrintName">用紙名(まだA3とA4のみ)</param>
        ''' <remarks></remarks>
        Public Sub PrintPaper(ByVal FileName As String, ByVal SheetIndex As Integer, ByVal PrintName As String)
            Dim workbook As Object = Nothing
            Dim pageSetup As Object = Nothing

            Try
                'ファイル存在確認
                'これが存在すると高確率で失敗する'
                'If System.IO.File.Exists(FileName) Then
                workbook = m_xlBook.sheets()
                'シートの選択
                m_activeSheet = workbook.Item(SheetIndex)
                'm_xlBook.Activate()

                pageSetup = m_activeSheet.pagesetup

                If StringUtil.Equals(PrintName, "A3") Then
                    pageSetup.PaperSize = XlPaperSize.xlPaperA3
                End If
                If StringUtil.Equals(PrintName, "A4") Then
                    pageSetup.PaperSize = XlPaperSize.xlPaperA4
                End If
                pageSetup.FitToPagesWide = 1


                'Else
                'MsgBox("失敗しました", MsgBoxStyle.Critical, "エラー")
                'End If
            Finally
                Me.Free(pageSetup)
                Me.Free(workbook)
                Me.Free(m_activeSheet)
            End Try
        End Sub

#End Region

        '20140225 追加分

#Region "シート間行コピー"
        ''' <summary>
        ''' シート間行コピー
        ''' </summary>
        ''' <param name="sourceSheet">コピー元シートINDEX</param>
        ''' <param name="targetSheet">コピー先シートINDEX</param>
        ''' <param name="sourceStartRowIdx">コピー元開始行</param>
        ''' <param name="targetStartRowIdx">コピー先開始行</param>
        ''' <param name="rowCount">行数</param>
        ''' <remarks></remarks>
        Public Sub CopySheetRowInsert(ByVal sourceSheet As Integer, ByVal targetSheet As Integer, _
                                        ByVal sourceStartRowIdx As Integer, ByVal targetStartRowIdx As Integer, _
                                        ByVal rowCount As Integer)

            Dim xlSheets As Object = Nothing
            Dim xlSheet1 As Object = Nothing
            Dim xlsheet2 As Object = Nothing

            Dim rangeStRow1 As Object = Nothing
            Dim rangeEdRow1 As Object = Nothing

            Dim rangeStRow2 As Object = Nothing
            Dim rangeEdRow2 As Object = Nothing

            Dim range1 As Object = Nothing
            Dim range2 As Object = Nothing

            xlSheets = m_xlBook.Sheets
            xlSheet1 = xlSheets(targetSheet)
            xlsheet2 = xlSheets(sourceSheet)

            Try
                'アクティブシートをコピー先へ
                Me.SetActiveSheet(targetSheet)
                Me.UnProtectSheet()

                'アクティブシートをコピー元へ
                Me.SetActiveSheet(sourceSheet)

                'コピー元
                rangeStRow2 = xlsheet2.Rows(sourceStartRowIdx)
                rangeEdRow2 = xlsheet2.Rows(sourceStartRowIdx + rowCount)
                range2 = xlsheet2.Range(rangeStRow2, rangeEdRow2)
                range2.Copy()

                'コピー先
                rangeStRow1 = xlSheet1.Rows(targetStartRowIdx)
                rangeEdRow1 = xlSheet1.Rows(targetStartRowIdx + rowCount)
                range1 = xlSheet1.Range(rangeStRow1, rangeEdRow1)
                range1.PasteSpecial()

                'アクティブシートをコピー先へ
                Me.SetActiveSheet(targetSheet)

                '選択を戻す
                m_xlApp.CutCopyMode = False

            Finally
                Me.Free(rangeEdRow1)
                Me.Free(rangeStRow1)
                Me.Free(rangeEdRow2)
                Me.Free(rangeStRow2)

                Me.Free(range2)
                Me.Free(range1)
                Me.Free(xlsheet2)
                Me.Free(xlSheet1)
                Me.Free(xlSheets)
            End Try
        End Sub
#End Region

        'コメント'

        ''' <summary>
        ''' シート間行コピー
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AddComment(ByVal startCol As Integer, _
                              ByVal startRow As Integer, _
                              ByVal comment As String)

            Dim xlCell As Object = Nothing

            Try
                xlCell = Me.GetCell(startCol, startRow)

                xlCell.AddComment(comment)

            Finally
                Me.Free(xlCell)
            End Try


        End Sub



    End Class

#End Region

End Namespace

