Imports EventSakusei.ShisakuBuhinMenu
Imports ShisakuCommon
Imports EBom.Excel
Imports EBom.Data
Imports EBom.Common
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports System
Imports System.Text.RegularExpressions
Imports EventSakusei.ShisakuBuhinEditSekkei.Dao

Namespace ShisakuBuhinMenu.Export2Excel

    Public Class ExportShisakuBuhinMenu2Excel


#Region "Construct"
        ''' <summary>
        ''' Construct
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub
        Private filename As String
        ''' <summary>
        ''' Construct
        ''' </summary>
        ''' <param name="buhinMenuForm">試作部品メニュー画面Form</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal buhinMenuForm As Frm8DispShisakuBuhinMenu)
            Dim systemDrive As String = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            'Dim fileName As String
            Using sfd As New SaveFileDialog()
                sfd.FileName = ShisakuCommon.ShisakuGlobal.ExcelOutPut
                sfd.InitialDirectory = systemDrive
                '2012/01/25
                '2012/01/21
                '--------------------------------------------------
                'EXCEL出力先フォルダチェック
                ShisakuCommon.ShisakuComFunc.ExcelOutPutDirCheck()
                '--------------------------------------------------
                '[Excel出力系 N]
                'fileName = sfd.InitialDirectory + "\" + sfd.FileName
                filename = buhinMenuForm.lblEventName.Text.ToString() + " 設計処置状況 " + Now.ToString("MMdd") + Now.ToString("HHmm") + ".xls"
                filename = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + StringUtil.ReplaceInvalidCharForFileName(filename)	'2012/02/08 Excel出力ディレクトリ指定対応
            End Using


            If Not ShisakuComFunc.IsFileOpen(ShisakuCommon.ShisakuGlobal.ExcelOutPut) Then
                Dim eventCode As String = buhinMenuForm.lblEventCode.Text.ToString()
                Dim eventName As String = buhinMenuForm.lblEventName.Text.ToString()

                Dim headObject = getHeaderObj(eventCode)

                Using xls As New ShisakuExcel(fileName)
                    '     xls.OpenBook(fileName)
                    xls.ClearWorkBook()
                    xls.SetFont("ＭＳ Ｐゴシック", 11)
                    setSheet1(xls, eventCode, eventName, headObject)
                    '2012/02/02'
                    'A4横で印刷できるように変更'
                    xls.PrintPaper(filename, 1, "A4")
                    xls.PrintOrientation(filename, 1, 1, False)
                    setSheet2(xls, eventCode, eventName, headObject)
                    '2012/02/02'
                    'A4横で印刷できるように変更'
                    xls.PrintPaper(filename, 2, "A4")
                    xls.PrintOrientation(filename, 2, 1, False)
                    xls.SetActiveSheet(1)
                    xls.Save()

                End Using


                Process.Start(filename)

            End If
        End Sub
#End Region

#Region "ExcelのHeaderのDB抽出部分"
        ''' <summary>
        ''' ExcelのHeaderのDB抽出部分
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <returns>ExcelのHeader資料含んで</returns>
        ''' <remarks></remarks>
        Public Function getHeaderObj(ByVal eventCode As String) As ShisakuSekkeiBlockHeadVo

            Dim eventDao As New ShisakuEventDaoImpl
            Dim headObject = eventDao.GetShisakuBuhinMenuHead(eventCode)
            Return headObject
        End Function
#End Region

#Region "Excel出力　シート１の部分"
        ''' <summary>
        ''' Excel出力　シート１の部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <param name="eventName">試作イベント名</param>
        ''' <param name="headerObj">ExcelのHeader資料含んで</param>
        ''' <remarks></remarks>
        Public Sub setSheet1(ByVal xls As ShisakuExcel, _
                         ByVal eventCode As String, _
                         ByVal eventName As String, _
                         ByVal headerObj As ShisakuSekkeiBlockHeadVo)
            xls.SetActiveSheet(1)

            setSheet1Heard(xls, eventName, headerObj)

            setSheet1Body(xls, eventCode)

            setSheet1ColumnWidth(xls)
            setSheet1RowHeight(xls)

            xls.PrintRange(filename, 1, xls.EndCol - 2, xls.EndRow)
            xls.PrintPaper(filename, 2, "A4")
            xls.PrintOrientation(filename, 1, 1, False)

        End Sub
#End Region

#Region "Excel出力　シート１のColumnの広さを設定"
        ''' <summary>
        ''' Excel出力　シート１のColumnの広さを設定
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setSheet1ColumnWidth(ByVal xls As ShisakuExcel)
            xls.SetColWidth(1, 1, 4)
            xls.SetColWidth(2, 14, 12)
            xls.SetColWidth(3, 3, 14)
            xls.SetColWidth(4, 4, 9)
            xls.SetColWidth(8, 8, 7)
            xls.SetColWidth(11, 11, 7)
            xls.SetColWidth(14, 14, 7)

            'xls.SetColWidth(1, 1, 3)
            'xls.SetColWidth(2, 14, 8)
            'xls.SetColWidth(3, 3, 10)
            'xls.SetColWidth(4, 4, 6)
            'xls.SetColWidth(8, 8, 6)
            'xls.SetColWidth(11, 11, 6)
            'xls.SetColWidth(14, 14, 6)
        End Sub
#End Region

#Region "Excel出力　シート１のRowの高さを設定"
        ''' <summary>
        ''' Excel出力　シート１のColumnの広さを設定
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setSheet1RowHeight(ByVal xls As ShisakuExcel)
            xls.SetRowHeight(1, xls.EndRow, 15)
        End Sub
#End Region

#Region "Excel出力　シート１のHeaderの部分"
        ''' <summary>
        ''' Excel出力　シート１のHeaderの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="eventName">試作イベント名</param>
        ''' <param name="headerObj">ExcelのHeader資料含んで</param>
        ''' <remarks></remarks>
        Public Sub setSheet1Heard(ByVal xls As ShisakuExcel, _
                                  ByVal eventName As String, _
                                  ByVal headerObj As ShisakuSekkeiBlockHeadVo)
            Dim syochiKigen = getSyochiKigen(headerObj)

            'なぜ１を足したのか？
            'Dim ato As String = getAto(syochiKigen) + 1
            Dim ato As String = getAto(syochiKigen)

            Dim sindoJyoukyou = getSindoJyoukyou(headerObj)
            Dim kanryouSuu = getKanryouSuu(headerObj)
            Dim nokori = getNokori(headerObj)

            xls.SetValue(2, 1, "≪　設計処置状況EXCEL　≫")
            Dim syutuRyokuDate = Now().ToString("yyyy/MM/dd   HH:mm:ss ")
            xls.MergeCells(6, 1, 9, 1, True)
            xls.SetAlignment(6, 1, 9, 1, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            'ずれが無いか 樺澤'
            xls.SetAlignment(6, 1, 9, 1, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            xls.SetValue(6, 1, "出力日 " + syutuRyokuDate)
            xls.SetValue(2, 3, "イベント")
            xls.SetValue(3, 3, eventName)
            xls.SetValue(2, 4, "処置期限")
            xls.SetValue(3, 4, syochiKigen) '処置期限の値

            'xls.SetValue(4, 4, "あと")
            'xls.SetValue(5, 4, ato + " 日") '出力日－処置期限の値
            '残り日数を見てメッセージを変える。
            If ato > 0 Then
                '期限が切れていない時
                xls.SetValue(4, 4, "あと")
                xls.SetValue(5, 4, ato + " 日") '出力日－処置期限の値
            Else
                '期限が切れた時は以下のメッセージを出力する。
                xls.MergeCells(4, 4, 5, 4, True)
                xls.SetValue(4, 4, "もう過ぎてます")
            End If

            xls.SetValue(2, 5, "進度状況")
            xls.SetValue(3, 5, sindoJyoukyou) '完了数/総数
            xls.SetValue(4, 5, "完了数")
            xls.SetValue(5, 5, kanryouSuu) '完了数/総数
            xls.SetValue(4, 6, "残り")
            xls.SetValue(5, 6, nokori) '総数-完了数
            xls.MergeCells(6, 8, 8, 8, True)
            xls.SetValue(6, 8, "完了")
            xls.MergeCells(9, 8, 11, 8, True)
            xls.SetValue(9, 8, "担当承認")
            xls.MergeCells(12, 8, 14, 8, True)
            xls.SetValue(12, 8, "課長・主査承認")
            xls.SetLine(6, 8, 14, 8, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            xls.SetValue(1, 9, "")
            xls.SetValue(2, 9, "設計課")
            xls.SetValue(3, 9, "ブロック数")
            xls.SetValue(4, 9, "処置")
            xls.SetValue(5, 9, "処置完了")
            xls.SetValue(6, 9, "進度状況")
            xls.SetValue(7, 9, "処置数")
            xls.SetValue(8, 9, "残り")
            xls.SetValue(9, 9, "進度状況")
            xls.SetValue(10, 9, "処置数")
            xls.SetValue(11, 9, "残り")
            xls.SetValue(12, 9, "進度状況")
            xls.SetValue(13, 9, "処置数")
            xls.SetValue(14, 9, "残り")
            xls.SetAlignment(2, 3, 2, 5, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter, True, True)
            xls.SetAlignment(3, 4, 3, 5, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            xls.SetAlignment(4, 4, 4, 4, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            xls.SetAlignment(4, 5, 4, 6, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter, True, True)

            xls.SetAlignment(6, 8, 8, 8, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            xls.SetAlignment(9, 8, 11, 8, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            xls.SetAlignment(12, 8, 14, 8, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            xls.SetLine(1, 9, 14, 9, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
        End Sub
#End Region

#Region "Excel出力　シート１のBodyの部分"
        ''' <summary>
        ''' Excel出力　シート１のBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Public Sub setSheet1Body(ByVal xls As ShisakuExcel, ByVal eventCode As String)
            Dim listCount = getListCount(eventCode)  'DBから設計課情報の集計データ
            Dim i As Integer
            For i = 0 To listCount.Count - 1 '＋１日する。
                Dim shisakuBlockCount = listCount.Item(i)
                Dim karyakuName As String = shisakuBlockCount.KaRyakuName
                Dim totalBlock As Decimal = shisakuBlockCount.TotalBlock
                Dim totalJyouTai As Decimal = shisakuBlockCount.TotalJyoutai
                Dim totalSyouNinJyouTai As Decimal = shisakuBlockCount.TotalSyouninJyoutai
                Dim totalKaChouSyouNinJyouTai As Decimal = shisakuBlockCount.TotalKachouSyouninJyoutai
                If 0 < shisakuBlockCount.TotalBlock Then
                    xls.SetValue(1, 10 + i, i + 1)
                    xls.SetValue(2, 10 + i, karyakuName)
                    xls.SetValue(3, 10 + i, totalBlock)
                    xls.SetValue(4, 10 + i, totalKaChouSyouNinJyouTai)
                    xls.SetValue(5, 10 + i, Decimal.Round(totalKaChouSyouNinJyouTai / totalBlock * 100, 0).ToString() + " %")
                    xls.SetValue(6, 10 + i, Decimal.Round(totalJyouTai / totalBlock * 100, 0).ToString() + " %")
                    xls.SetValue(7, 10 + i, totalJyouTai.ToString() + "/" + totalBlock.ToString())
                    xls.SetValue(8, 10 + i, totalBlock - totalJyouTai)
                    xls.SetValue(9, 10 + i, Decimal.Round(totalSyouNinJyouTai / totalBlock * 100, 0).ToString() + " %")
                    xls.SetValue(10, 10 + i, totalSyouNinJyouTai.ToString() + "/" + totalBlock.ToString())
                    xls.SetValue(11, 10 + i, totalBlock - totalSyouNinJyouTai)
                    xls.SetValue(12, 10 + i, Decimal.Round(totalKaChouSyouNinJyouTai / totalBlock * 100, 0).ToString() + " %")
                    xls.SetValue(13, 10 + i, totalKaChouSyouNinJyouTai.ToString() + "/" + totalBlock.ToString())
                    xls.SetValue(14, 10 + i, totalBlock - totalKaChouSyouNinJyouTai)
                Else
                    xls.SetValue(1, 10 + i, i + 1)
                    xls.SetValue(2, 10 + i, karyakuName)
                    xls.SetValue(3, 10 + i, totalBlock)
                    xls.SetValue(4, 10 + i, "0")
                    xls.SetValue(5, 10 + i, "0 %")
                    xls.SetValue(6, 10 + i, "0 %")
                    xls.SetValue(7, 10 + i, "0/0")
                    xls.SetValue(8, 10 + i, "0")
                    xls.SetValue(9, 10 + i, "0 %")
                    xls.SetValue(10, 10 + i, "0/0")
                    xls.SetValue(11, 10 + i, "0")
                    xls.SetValue(12, 10 + i, "0 %")
                    xls.SetValue(13, 10 + i, "0/0")
                    xls.SetValue(14, 10 + i, "0")
                End If
            Next
            xls.SetLine(1, 10, 14, 10 + i - 1, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            xls.SetAlignment(1, 10, 1, 10 + i - 1, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            xls.SetAlignment(2, 10, 2, 10 + i - 1, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, True, True)
            xls.SetAlignment(3, 10, 14, 10 + i - 1, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter, True, True)
        End Sub
#End Region

#Region "Excelのシート１のBodyのDB抽出部分"
        ''' <summary>
        ''' Excelのシート１のBodyのDB抽出部分
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <returns>Excelのシート１のBody資料含んでList</returns>
        ''' <remarks></remarks>
        Public Function getListCount(ByVal eventCode As String) As List(Of ShisakuSekkeiBlockCountVo)

            Dim blockDao As New ShisakuSekkeiBlockDaoImpl
            Dim list = blockDao.GetShisakuSekkeiBlockCount(eventCode)
            Return list
        End Function
#End Region

#Region "Excel出力　シート２の部分"
        ''' <summary>
        ''' Excel出力　シート２の部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <param name="eventName">試作イベント名</param>
        ''' <param name="headerObj">ExcelのHeader資料含んでList</param>
        ''' <remarks></remarks>
        Public Sub setSheet2(ByVal xls As ShisakuExcel, _
                             ByVal eventCode As String, _
                             ByVal eventName As String, _
                             ByVal headerObj As ShisakuSekkeiBlockHeadVo)
            xls.SetActiveSheet(2)

            setSheet2Heard(xls, eventName, headerObj)
            setSheet2Body(xls, eventCode)
            setSheet2ColumnWidth(xls)

            xls.PrintRange(filename, 2, xls.EndCol - 2, xls.EndRow)
            xls.PrintZoom(filename, 2, 83)
            xls.PrintPaper(filename, 2, "A3")
            xls.PrintOrientation(filename, 2, 1, True)
        End Sub
#End Region

#Region "Excel出力　シート２のColumnの広さを設定"
        ''' <summary>
        ''' Excel出力　シート１のColumnの広さを設定
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setSheet2ColumnWidth(ByVal xls As ShisakuExcel)
            xls.SetColWidth(1, 1, 8)
            xls.SetColWidth(2, 3, 10)
            xls.SetColWidth(4, 4, 13.5)
            xls.SetColWidth(5, 5, 8)
            xls.SetColWidth(6, 6, 3)
            xls.SetColWidth(7, 7, 6)
            xls.SetColWidth(8, 8, 30)
            xls.SetColWidth(9, 9, 10)
            xls.SetColWidth(10, 10, 6)
            xls.SetColWidth(11, 11, 21) '最終更新日の横幅
            xls.SetColWidth(12, 12, 8)
            xls.SetColWidth(13, 14, 10)
            xls.SetColWidth(15, 15, 8)
            xls.SetColWidth(16, 17, 10)
        End Sub
#End Region

#Region "Excel出力　シート２のHeaderの部分"
        ''' <summary>
        ''' Excel出力　シート２のHeaderの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="eventName">試作イベント名</param>
        ''' <param name="headerObj">ExcelのHeader資料含んで</param>
        ''' <remarks></remarks>
        Public Sub setSheet2Heard(ByVal xls As ShisakuExcel, _
                                  ByVal eventName As String, _
                                  ByVal headerObj As ShisakuSekkeiBlockHeadVo)
            Dim syochiKigen = getSyochiKigen(headerObj)

            'なぜ１を足したのか？
            'Dim ato As String = getAto(syochiKigen) + 1 '＋１日する。
            Dim ato As String = getAto(syochiKigen)  '＋１日する。

            Dim sindoJyoukyou = getSindoJyoukyou(headerObj)
            Dim kanryouSuu = getKanryouSuu(headerObj)
            Dim nokori = getNokori(headerObj)

            xls.SetValue(1, 1, "≪　設計処置状況EXCEL　≫")
            Dim syutuRyokuDate = Now().ToString("yyyy/MM/dd   HH:mm:ss ")
            xls.MergeCells(14, 1, 17, 1, True)
            'xls.SetAlignment(14, 1, 17, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            xls.SetValue(14, 1, "出力日 " + syutuRyokuDate)
            xls.SetValue(1, 3, "イベント")
            xls.SetValue(2, 3, eventName)
            xls.SetValue(1, 4, "処置期限")
            xls.SetValue(2, 4, syochiKigen) '処置期限の値

            'xls.SetValue(3, 4, "あと")
            'xls.SetValue(4, 4, ato + " 日") '出力日－処置期限の値
            '残り日数を見てメッセージを変える。
            If ato > 0 Then
                '期限が切れていない時
                xls.SetValue(3, 4, "あと")
                xls.SetValue(4, 4, ato + " 日") '出力日－処置期限の値
            Else
                '期限が切れた時は以下のメッセージを出力する。
                xls.MergeCells(3, 4, 4, 4, True)
                xls.SetValue(3, 4, "もう過ぎてます")
            End If

            xls.SetValue(1, 5, "進度状況")
            xls.SetValue(2, 5, sindoJyoukyou) '完了数/総数
            xls.SetValue(3, 5, "完了数")
            xls.SetValue(4, 5, kanryouSuu) '完了数/総数
            xls.SetValue(3, 6, "残り")
            xls.SetValue(4, 6, nokori) '総数-完了数

            xls.MergeCells(12, 7, 14, 7, True)
            xls.SetValue(12, 7, "担当承認")
            xls.MergeCells(15, 7, 17, 7, True)
            xls.SetValue(15, 7, "課長・主査承認")
            xls.SetValue(1, 8, "設計課")
            xls.SetValue(2, 8, "ブロック" + vbCrLf + "不要")
            '2012/03/26 マージ解除
            'xls.MergeCells(3, 8, 4, 8, True)
            xls.SetValue(3, 8, "状態")
            xls.SetValue(5, 8, "ブロックNo.")
            xls.SetValue(6, 8, "改" + vbCrLf + "訂")
            xls.SetValue(7, 8, "ユニット" + vbCrLf + "区分")
            xls.SetValue(8, 8, "ブロック名称")
            xls.SetValue(9, 8, "担当者")
            xls.SetValue(10, 8, "TEL")
            xls.SetValue(11, 8, "最終更新日")
            xls.SetValue(12, 8, "所属")
            xls.SetValue(13, 8, "承認者")
            xls.SetValue(14, 8, "承認日")
            xls.SetValue(15, 8, "所属")
            xls.SetValue(16, 8, "承認者")
            xls.SetValue(17, 8, "承認日")

            xls.SetAlignment(1, 3, 1, 5, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter, True, True)
            xls.SetAlignment(2, 4, 2, 5, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            xls.SetAlignment(3, 4, 3, 4, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            xls.SetAlignment(3, 5, 3, 6, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter, True, True)

            xls.SetAlignment(12, 7, 14, 7, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            xls.SetAlignment(15, 7, 17, 7, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            xls.SetAlignment(1, 8, 17, 8, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignTop, True, True)

            xls.SetLine(12, 7, 17, 7, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            xls.SetLine(1, 8, 17, 8, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)

        End Sub
#End Region

#Region "Excel出力　シート２のBodyの部分"
        ''' <summary>
        ''' Excel出力　シート２のBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Public Sub setSheet2Body(ByVal xls As ShisakuExcel, ByVal eventCode As String)
            Dim listMeiSai = getListMeiSai(eventCode)  'DBから設計課情報の集計データ
            Dim i As Integer
            For i = 0 To listMeiSai.Count - 1
                Dim shisakuSekkeiBlock = listMeiSai.Item(i)

                Dim blockFuyou = ShisakuComFunc.getBlockFuyouMoji(shisakuSekkeiBlock.BlockFuyou)
                Dim jyoutai1 = ""
                Dim jyoutai2 = ""
                Dim jyoutai = shisakuSekkeiBlock.Jyoutai
                Dim tantoJyoutai = shisakuSekkeiBlock.TantoSyouninJyoutai
                Dim kachouJyoutai = shisakuSekkeiBlock.KachouSyouninJyoutai
                If (kachouJyoutai = ShishakuSekkeiBlockStatusShounin2) Then
                    jyoutai2 = ShisakuComFunc.getBlockJyoutaiMoji(kachouJyoutai)
                    jyoutai1 = ShisakuComFunc.getBlockJyoutaiMoji(ShishakuSekkeiBlockStatusShouchiKanryou)
                ElseIf (tantoJyoutai = ShishakuSekkeiBlockStatusShounin1) Then
                    jyoutai2 = ShisakuComFunc.getBlockJyoutaiMoji(tantoJyoutai)
                    jyoutai1 = ShisakuComFunc.getBlockJyoutaiMoji(ShishakuSekkeiBlockStatusShouchiKanryou)
                Else
                    jyoutai1 = ShisakuComFunc.getBlockJyoutaiMoji(jyoutai)
                End If

                Dim saisyuKoushinbi As String = shisakuSekkeiBlock.SaisyuKoushinbi
                Dim saisyuKoushinjikan As String = shisakuSekkeiBlock.SaisyuKoushinjikan
                If (saisyuKoushinjikan.Length = 5) Then
                    saisyuKoushinjikan = "0" + saisyuKoushinjikan
                End If
                Dim saisyuKousinHitsuke As String = ShisakuComFunc.moji8Convert2Date(saisyuKoushinbi) _
                                                    + "  " _
                                                    + ShisakuComFunc.moji6Convert2Time(saisyuKoushinjikan)
                Dim tantoSyouninHi = ShisakuComFunc.moji8Convert2Date(shisakuSekkeiBlock.TantoSyouninHi)
                Dim kachouSyouninHi = ShisakuComFunc.moji8Convert2Date(shisakuSekkeiBlock.KachouSyouninHi)

                xls.SetValue(1, 9 + i, shisakuSekkeiBlock.KaRyakuName)
                xls.SetValue(2, 9 + i, blockFuyou)
                xls.SetValue(3, 9 + i, jyoutai1)
                xls.SetValue(4, 9 + i, jyoutai2)
                xls.SetValue(5, 9 + i, shisakuSekkeiBlock.ShisakuBlockNo)
                xls.SetValue(6, 9 + i, shisakuSekkeiBlock.ShisakuBlockNoKaiteiNo)
                xls.SetValue(7, 9 + i, shisakuSekkeiBlock.UnitKbn)
                xls.SetValue(8, 9 + i, shisakuSekkeiBlock.ShisakuBlockName)
                xls.SetValue(9, 9 + i, shisakuSekkeiBlock.SyainName)
                xls.SetValue(10, 9 + i, shisakuSekkeiBlock.TelNo)
                xls.SetValue(11, 9 + i, saisyuKousinHitsuke)
                xls.SetValue(12, 9 + i, shisakuSekkeiBlock.TantoSyouninKa)
                xls.SetValue(13, 9 + i, shisakuSekkeiBlock.TantoName)
                xls.SetValue(14, 9 + i, tantoSyouninHi)
                xls.SetValue(15, 9 + i, shisakuSekkeiBlock.KachouSyouninKa)
                xls.SetValue(16, 9 + i, shisakuSekkeiBlock.KachouName)
                xls.SetValue(17, 9 + i, kachouSyouninHi)
            Next
            If Not i = 0 Then
                xls.SetLine(1, 9, 17, 9 + i - 1, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                xls.SetAlignment(1, 9, 17, 9 + i, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, True, True)
                xls.SetAlignment(6, 9, 6, 9 + i, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter, True, True)
                xls.SetAlignment(7, 9, 7, 9 + i, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
                xls.SetAlignment(10, 9, 11, 9 + i, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter, True, True)
                xls.SetAlignment(13, 9, 14, 9 + i, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter, True, True)
                xls.SetAlignment(16, 9, 17, 9 + i, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter, True, True)
            End If
        End Sub
#End Region

#Region "Excelのシート２のBodyのDB抽出部分"
        ''' <summary>
        ''' Excelのシート１のBodyのDB抽出部分
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <returns>Excelのシート１のBody資料含んでList</returns>
        ''' <remarks></remarks>
        Public Function getListMeiSai(ByVal eventCode As String) As List(Of ShisakuSekkeiBlockMeisaiVo)

            Dim blockDao As New ShisakuSekkeiBlockDaoImpl
            Dim list = blockDao.GetShisakuSekkeiBlockMeisai(eventCode)
            Return list

        End Function
#End Region

#Region "Excel Hearderの「処置期限」を貰います"
        ''' <summary>
        ''' 「処置期限」
        ''' </summary>
        ''' <param name="headerObj">ExcelのHeader資料含んで</param>
        ''' <returns>処置期限 date</returns>
        ''' <remarks></remarks>
        Public Function getSyochiKigen(ByVal headerObj As ShisakuSekkeiBlockHeadVo) As String

            'Dim status = ShisakuComFunc.moji8Convert2Date(headerObj.Status)
            Dim status = headerObj.Status

            Dim syochiKigen As String
            If status = "21" Then
                syochiKigen = ShisakuComFunc.moji8Convert2Date(headerObj.KaiteiSyochiShimekiribi)
            Else
                syochiKigen = ShisakuComFunc.moji8Convert2Date(headerObj.Shimekiribi)
            End If

            If syochiKigen.Length = 8 Then
                syochiKigen = "20" & syochiKigen
            End If

            Return syochiKigen
        End Function
#End Region

#Region "Excel Hearderの「後」を貰います"
        ''' <summary>
        ''' 「後」
        ''' </summary>
        ''' <param name="syochiKigen">処置期限</param>
        ''' <returns>「処置期限」減「出力日」の日数</returns>
        ''' <remarks></remarks>
        Public Function getAto(ByVal syochiKigen As String) As String
            If Not syochiKigen.Equals("") Then
                'Dim ato = DateTime.ParseExact(syochiKigen, "yyyy/MM/dd", Nothing)
                'Return DateDiff(DateInterval.Day, Now(), ato)

                Dim wToday As Integer = Integer.Parse(DateTime.Now.ToString("yyyyMMdd"))
                Dim wSyochikigen As Integer = Integer.Parse(syochiKigen.Substring(0, 4) & syochiKigen.Substring(5, 2) & syochiKigen.Substring(8, 2))

                Dim kabetuJyoutaiDao As New ShisakuBuhinEditBlockDaoImpl
                Dim wAto As Integer = kabetuJyoutaiDao.GetKadoubi(wToday, wSyochikigen).Kadobi

                Return wAto

            End If
            Return Nothing
        End Function
#End Region

#Region "Excel Hearderの「進度状況」を貰います"
        ''' <summary>
        ''' 「進度状況」
        ''' </summary>
        ''' <param name="headerObj">ExcelのHeader資料含んで</param>
        ''' <returns>「完了数」除く「総数」　％</returns>
        ''' <remarks></remarks>
        Public Function getSindoJyoukyou(ByVal headerObj As ShisakuSekkeiBlockHeadVo) As String
            Dim sousuu = headerObj.Sousuu
            Dim kanryou = headerObj.Kanryou
            Dim sindoJyoukyou = ""
            If (Not sousuu Is Nothing) And Not sousuu = 0 Then
                sindoJyoukyou = (Decimal.Round(kanryou / sousuu.ToString * 100)).ToString() + "  %"
            End If
            Return sindoJyoukyou
        End Function
#End Region

#Region "Excel Hearderの「完了数」を貰います"
        ''' <summary>
        ''' 「完了数」
        ''' </summary>
        ''' <param name="headerObj">ExcelのHeader資料含んで</param>
        ''' <returns>「完了数」／「総数」</returns>
        ''' <remarks></remarks>
        Public Function getKanryouSuu(ByVal headerObj As ShisakuSekkeiBlockHeadVo) As String
            Dim sousuu = headerObj.Sousuu
            Dim kanryou = headerObj.Kanryou
            Dim kanryouSuu = kanryou.ToString() + "/" + sousuu.ToString
            Return kanryouSuu
        End Function
#End Region

#Region "Excel Hearderの「残り」を貰います"
        ''' <summary>
        ''' 「残り」
        ''' </summary>
        ''' <param name="headerObj">ExcelのHeader資料含んで</param>
        ''' <returns>「総数」減「完了数」</returns>
        ''' <remarks></remarks>
        Public Function getNokori(ByVal headerObj As ShisakuSekkeiBlockHeadVo) As String
            Dim sousuu = headerObj.Sousuu
            Dim kanryou = headerObj.Kanryou
            Dim nokori = sousuu - kanryou
            Return nokori
        End Function
#End Region


    End Class

End Namespace
