Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports ShisakuCommon
Imports EBom.Common
Imports EBom.Excel
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl




Namespace ShisakuBuhinEditBlock.Export2Excel


    Public Class ShisakuBuhinEditBlock2Excel

        Private isBase As Boolean

        ''' <summary>
        ''' Excel出力
        ''' </summary>
        ''' <param name="buttonFlag">ボタンフラグ</param>
        ''' <param name="strEventCode">イベントコード</param>
        ''' <param name="strBukaCode">部課コード</param>
        ''' <param name="strBlockNo">ブロックNo</param>
        ''' <param name="strBlockKaiteNo">ブロック改訂No</param>
        ''' <param name="strSekkeika">設計課</param>
        ''' <param name="blockNoList">ブロックNoリスト</param>
        ''' <param name="isRireki">履歴か</param>
        ''' <param name="isBase">ベースか</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal buttonFlag As String, ByVal strEventCode As String, ByVal strBukaCode As String, _
                       ByVal strBlockNo As String, ByVal strBlockKaiteNo As String, ByVal strSekkeika As String, _
                       Optional ByVal blockNoList As List(Of String) = Nothing, Optional ByVal isRireki As Boolean = False, Optional ByVal isBase As Boolean = False)

            Dim shisakuKaihatsuFugo As String = ""
            Dim shisakuEventName As String = ""
            Dim tanTouSya As String = ""
            Dim tel As String = ""
            Me.isBase = isBase
            Dim fileName As String

            Dim getDate As New EditBlock2ExcelDaoImpl()
            Dim shisakiEventVo As TShisakuEventVo = getDate.FindByEvent(strEventCode)
            shisakuKaihatsuFugo = shisakiEventVo.ShisakuKaihatsuFugo
            shisakuEventName = shisakiEventVo.ShisakuEventName

            Using sfd As New SaveFileDialog()
                sfd.FileName = ShisakuCommon.ShisakuGlobal.ExcelOutPut
                sfd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                '[Excel出力系 A,B,C]
                '--------------------------------------------------
                'EXCEL出力先フォルダチェック
                ShisakuCommon.ShisakuComFunc.ExcelOutPutDirCheck()
                '--------------------------------------------------
                Const fmtFileName As String = "{0}{1} {2}{3}{4} {5} {6}.xls"    ''[開発符号][イベント名] [設計課][ブロックNo.][改訂No.] [日付] [時刻].xls
                If buttonFlag = "ShiTei" Then
                    If isRireki Then
                        ''2015/02/25 ベースを選択した際には改訂Noではなく"base"をファイル名へ追記
                        If isBase Then
                            fileName = String.Format(fmtFileName, _
                                                     shisakiEventVo.ShisakuKaihatsuFugo, _
                                                     shisakiEventVo.ShisakuEventName, _
                                                     strSekkeika, _
                                                     strBlockNo, _
                                                     "base", _
                                                     Now.ToString("MMdd"), Now.ToString("HHmm"))
                        Else
                            fileName = String.Format(fmtFileName, _
                                                     shisakiEventVo.ShisakuKaihatsuFugo, _
                                                     shisakiEventVo.ShisakuEventName, _
                                                     strSekkeika, _
                                                     strBlockNo, _
                                                     strBlockKaiteNo, _
                                                     Now.ToString("MMdd"), Now.ToString("HHmm"))
                        End If
                    Else
                        fileName = String.Format(fmtFileName, _
                                                 shisakiEventVo.ShisakuKaihatsuFugo, _
                                                 shisakiEventVo.ShisakuEventName, _
                                                 strSekkeika, _
                                                 strBlockNo, _
                                                 strBlockKaiteNo, _
                                                 Now.ToString("MMdd"), Now.ToString("HHmm"))
                    End If
                Else
                    fileName = String.Format(fmtFileName, _
                                             shisakiEventVo.ShisakuKaihatsuFugo, _
                                             shisakiEventVo.ShisakuEventName, _
                                             strSekkeika, _
                                             "全ブロック", _
                                             "", _
                                             Now.ToString("MMdd"), Now.ToString("HHmm"))
                End If
                fileName = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir & "\" & StringUtil.ReplaceInvalidCharForFileName(fileName)    '2012/02/08 Excel出力ディレクトリ指定対応
            End Using

            Dim sheet1BlockKaiteNo As String
            If isRireki Then
                sheet1BlockKaiteNo = strBlockKaiteNo
            Else
                sheet1BlockKaiteNo = Nothing
            End If
            If Not ShisakuComFunc.IsFileOpen(ShisakuCommon.ShisakuGlobal.ExcelOutPut) Then

                Using xls As New ShisakuExcel(fileName)

                    '指定ブロックNoの場合'
                    If buttonFlag = "ShiTei" Then
                        xls.OpenBook(fileName)
                        xls.ClearWorkBook()

                        If Not blockNoList.Count > 1 Then
                            ''Get装備仕様内容タイトルデータ---------------------------------------------------
                            Dim headTitleVos As EditBlock2ExcelTitle3BodyVo = _
                                    getDate.FindHeadInfoWithSekkeiBlockBy(strEventCode, strBukaCode, strBlockNo, strBlockKaiteNo)

                            If Not headTitleVos Is Nothing Then

                                tanTouSya = headTitleVos.UserId
                                tel = headTitleVos.TelNo
                                shisakuKaihatsuFugo = headTitleVos.ShisakuKaihatsuFugo
                                shisakuEventName = headTitleVos.ShisakuEventName

                                '担当者名を取得する'
                                tanTouSya = getDate.FindByShainName(tanTouSya)
                                ''試作A/L情報Excel出力
                                setSheet1(xls, strEventCode, strBukaCode, blockNoList, sheet1BlockKaiteNo, tanTouSya, tel, shisakuKaihatsuFugo, shisakuEventName, isBase)
                                '2012/02/02'
                                'A4横で印刷できるように変更'
                                xls.PrintPaper(fileName, 1, "A4")
                                xls.PrintOrientation(fileName, 1, 1, False)
                                ''試作部品表構成情報Excel出力
                                Dim sheet2Process = New ShisakuBuhinEditBlock2ExcelSheet2()
                                sheet2Process.Excute(xls, strEventCode, strBukaCode, strBlockNo, strBlockKaiteNo, tanTouSya, tel, shisakuKaihatsuFugo, shisakuEventName, fileName, isBase)
                                '2012/02/02'
                                'A4横で印刷できるように変更'
                                xls.PrintPaper(fileName, 2, "A4")
                                xls.PrintOrientation(fileName, 2, 1, False)
                                '試作部品表Excel出力
                                Dim sheet3Process = New ShisakuBuhinEditBlock2ExcelSheet3()
                                sheet3Process.Excute(xls, strEventCode, strBukaCode, strBlockNo, strBlockKaiteNo, tanTouSya, tel, shisakuKaihatsuFugo, shisakuEventName, strBukaCode, fileName, isBase)
                                '2012/02/02'
                                'A4横で印刷できるように変更'
                                xls.PrintPaper(fileName, 3, "A4")
                                xls.PrintOrientation(fileName, 3, 1, False)
                            End If
                        Else
                            '指定ブロック複数出力'
                            setSheet1(xls, strEventCode, strBukaCode, blockNoList, sheet1BlockKaiteNo, tanTouSya, tel, shisakuKaihatsuFugo, shisakuEventName, isBase)
                            Dim sheetAllProcess = New ShisakuBuhinEditBlock2AllExcel(strEventCode, strBukaCode)
                            sheetAllProcess.Excute(xls, fileName, blockNoList, strBlockKaiteNo, isBase)

                        End If

                    Else
                        '全ブロックExcel出力の場合'
                        xls.OpenBook(fileName)
                        xls.ClearWorkBook()
                        setSheet1(xls, strEventCode, strBukaCode, Nothing, sheet1BlockKaiteNo, tanTouSya, tel, shisakuKaihatsuFugo, shisakuEventName, isBase)

                        ''試作部品表Excel出力
                        Dim sheetAllProcess = New ShisakuBuhinEditBlock2AllExcel(strEventCode, strBukaCode)
                        sheetAllProcess.Excute(xls, fileName)
                    End If
                    xls.SetActiveSheet(1)
                    '2012/02/02'
                    'A4横で印刷できるように変更'
                    xls.PrintPaper(fileName, 1, "A4")
                    xls.PrintOrientation(fileName, 1, 1, False)
                    xls.Save()
                End Using
                Process.Start(fileName)
            End If

        End Sub

#Region "Excel出力　シート１の部分"

        ''' <summary>
        ''' A/L情報Excel
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <param name="eventCode"></param>
        ''' <param name="bukaCode"></param>
        ''' <param name="blockNoList"></param>
        ''' <param name="blockKaiteNo"></param>
        ''' <param name="isBase"></param>
        ''' <remarks></remarks>
        Public Sub setSheet1(ByVal xls As ShisakuExcel, _
                             ByVal eventCode As String, _
                             ByVal bukaCode As String, _
                             ByVal blockNoList As List(Of String), _
                             ByVal blockKaiteNo As String, _
                             ByVal tanTousya As String, _
                             ByVal tel As String, _
                             ByVal shisakuKaihatsuFugo As String, _
                             ByVal shisakuEventName As String, _
                             ByVal isBase As Boolean)


            xls.SetActiveSheet(1)

            'HeadPart開始設定
            Dim headPartPoint As New EditBlock2ExcelPointVo()

            headPartPoint.RowStart = 1
            headPartPoint.RowEnd = 1
            headPartPoint.ColStart = 1
            headPartPoint.ColEnd = 1

            ''Draw HeadPart1
            headPartPoint = setSheet1HeadPart1(xls, eventCode, bukaCode, blockNoList, blockKaiteNo, tanTousya, tel, shisakuKaihatsuFugo, shisakuEventName, headPartPoint)
            headPartPoint.RowStart = headPartPoint.RowStart + 2
            headPartPoint.RowEnd = headPartPoint.RowEnd + 2

            Dim headPart2Point As New EditBlock2ExcelPointVo()

            ''Draw HeadPart2
            headPart2Point = headPartPoint

            ''Draw Body Title1とTitle2
            setSheet1BodyPart(xls, eventCode, bukaCode, blockNoList, blockKaiteNo, headPart2Point, isBase)

        End Sub


#Region "setSheet1"
        ''Draw Head

        ''' <summary>
        ''' ヘッダー情報の作成
        ''' </summary>
        ''' <param name="xls">EXCEL</param>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="seKeka">設計課</param>
        ''' <param name="blockNoList">ブロックNo</param>
        ''' <param name="blockKaiteNo">ブロックNo改訂No</param>
        ''' <param name="tanTouSya">担当者</param>
        ''' <param name="tel">電話番号</param>
        ''' <param name="shisakuKaihatsuFugo">開発符号</param>
        ''' <param name="shisakuEventName">イベント名称</param>
        ''' <param name="point"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function setSheet1HeadPart1(ByVal xls As ShisakuExcel, _
                                       ByVal eventCode As String, _
                                       ByVal seKeka As String, _
                                       ByVal blockNoList As List(Of String), _
                                       ByVal blockKaiteNo As String, _
                                       ByVal tanTouSya As String, _
                                       ByVal tel As String, _
                                       ByVal shisakuKaihatsuFugo As String, _
                                       ByVal shisakuEventName As String, _
                                       ByVal point As EditBlock2ExcelPointVo) As EditBlock2ExcelPointVo

            xls.SetValue(point.ColStart, point.RowStart, shisakuKaihatsuFugo + " " + shisakuEventName)
            If blockNoList IsNot Nothing AndAlso blockNoList.Count = 1 Then
                '担当設計課を取得する'
                Dim tantoImpl As New ShisakuBuhinEditBlock.Dao.KaRyakuNameDaoImpl
                Dim TantoName As New Rhac1560Vo

                'TantoName = tantoImpl.GetKa_Ryaku_Name(seKeka)

                '担当設計が無い場合、コードを課略名称へ設定する。
                If Not StringUtil.IsEmpty(tantoImpl.GetKa_Ryaku_Name(seKeka)) Then
                    TantoName = tantoImpl.GetKa_Ryaku_Name(seKeka)
                Else
                    TantoName.KaRyakuName = seKeka
                End If

                '担当者名を取得する'
                Dim ExcelImpl As EditBlock2ExcelDao = New EditBlock2ExcelDaoImpl
                Dim UserName As String

                UserName = ExcelImpl.FindByShainName(tanTouSya)

                xls.SetValue(point.ColStart + 6, point.RowStart, String.Format("担当設計：{0} 担当者：{1} Tel: {2}", TantoName.KaRyakuName, tanTouSya, tel))
                xls.SetValue(point.ColStart, point.RowStart + 1, String.Format("ブロックNo：{0}", blockNoList(0)))

            ElseIf blockNoList IsNot Nothing AndAlso blockNoList.Count > 1 Then
                Dim buf As New List(Of String)
                For Each s As String In blockNoList
                    buf.Add(s)
                Next
                xls.SetValue(point.ColStart, point.RowStart + 1, String.Format("ブロックNo：{0}", String.Join("、", buf.ToArray)))
            End If

            Return point
        End Function

        'A/L部品表の作成'
        ''Draw Body Title1とTitle2

        ''' <summary>
        ''' EXCEL出力
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <param name="eventCode"></param>
        ''' <param name="seKeka"></param>
        ''' <param name="blockNoList"></param>
        ''' <param name="blockKaiteNo"></param>
        ''' <param name="point"></param>
        ''' <param name="isBase"></param>
        ''' <remarks></remarks>
        Public Sub setSheet1BodyPart(ByVal xls As ShisakuExcel, _
                                       ByVal eventCode As String, _
                                       ByVal seKeka As String, _
                                       ByVal blockNoList As List(Of String), _
                                       ByVal blockKaiteNo As String, _
                                       ByVal point As EditBlock2ExcelPointVo, _
                                       ByVal isBase As Boolean)
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(eventCode)

            ''第二分部TitlePart2(1.イベントベース車、2.装備仕様内容、3.メモ欄、4.INSTL品番)
            Dim getDate As New EditBlock2ExcelDaoImpl()

            ''処理開始
            ''Get装備仕様内容タイトルデータ---------------------------------------------------
            Dim titleVos As New List(Of EditBlock2ExcelTitle1Vo)

            titleVos = getDate.FindWithTitle1NameBy(eventCode, seKeka, blockNoList, blockKaiteNo, isBase)

            ''Get Title1の列数
            Dim colTitleGousyaCount As Integer
            colTitleGousyaCount = titleVos.Count ''Title1列数
            ''開始Point
            Dim pointBodyTitleGousya As New EditBlock2ExcelPointVo()
            pointBodyTitleGousya.RowStart = point.RowStart

            pointBodyTitleGousya.RowEnd = point.RowEnd + 7
            pointBodyTitleGousya.ColStart = point.ColStart
            pointBodyTitleGousya.ColEnd = point.ColEnd

            ''第一列Title号車設定
            xls.SetValue(pointBodyTitleGousya.ColStart, pointBodyTitleGousya.RowEnd, pointBodyTitleGousya.ColEnd, pointBodyTitleGousya.RowEnd, "号車")
            ''位置はCenter
            xls.SetAlignment(pointBodyTitleGousya.ColStart, pointBodyTitleGousya.RowEnd, pointBodyTitleGousya.ColEnd, pointBodyTitleGousya.RowEnd, _
                             XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            xls.SetLine(pointBodyTitleGousya.ColStart, pointBodyTitleGousya.RowEnd, pointBodyTitleGousya.ColEnd, pointBodyTitleGousya.RowEnd, _
                            XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)

            If titleVos.Count > 0 Then

                ''試作設計ブロックInstal情報Title
                '号車列の列番号'
                pointBodyTitleGousya.ColStart = pointBodyTitleGousya.ColStart + 1
                pointBodyTitleGousya.ColEnd = pointBodyTitleGousya.ColEnd + 1
                Dim i As Integer

                'アプライドNoとか取得'
                ''Get 項目No.対応表の値(例:項目の1001 = 開発符号)
                For i = 0 To titleVos.Count - 1
                    ''Get項目名Byコード
                    Dim title1VoHelp As New EditBlock2ExcelTitle1VoHelper(titleVos.Item(i))

                    'Set項目名のCellValue
                    xls.SetValue(pointBodyTitleGousya.ColStart + i, pointBodyTitleGousya.RowEnd, pointBodyTitleGousya.ColEnd + i, pointBodyTitleGousya.RowEnd, title1VoHelp.ShisakuSoubiHyoujiJunMei)
                    ''位置がCenterになります｡
                    xls.SetAlignment(pointBodyTitleGousya.ColStart + i, pointBodyTitleGousya.RowEnd, pointBodyTitleGousya.ColEnd + i, pointBodyTitleGousya.RowEnd, _
                                     XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
                    ''線を書きます
                    xls.SetLine(pointBodyTitleGousya.ColStart + i, pointBodyTitleGousya.RowEnd, pointBodyTitleGousya.ColEnd + i, pointBodyTitleGousya.RowEnd, _
                             XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                Next

                pointBodyTitleGousya.ColEnd = pointBodyTitleGousya.ColEnd + titleVos.Count

            Else
                ''タイトルない場合は
                pointBodyTitleGousya.RowStart = point.RowStart
                pointBodyTitleGousya.RowEnd = point.RowEnd + 7
                pointBodyTitleGousya.ColStart = point.ColStart
                pointBodyTitleGousya.ColEnd = point.ColEnd
            End If

            ''Get装備仕様内容タイトルデータ-------------------------------------------------------
            Dim title2Vos As List(Of EditBlock2ExcelTitle2Vo) = _
                    getDate.FindWithTitle2NameBy(eventCode, seKeka, blockNoList, blockKaiteNo, isBase)

            ''Get 装備仕様の列数
            Dim colTitleSoubiCount As Integer

            'カウントNなのに中身が無い場合があるので'
            If title2Vos.Count > 0 Then
                If StringUtil.IsEmpty(title2Vos(0).ShisakuSoubiHyoujiJun) Then
                    colTitleSoubiCount = 0
                Else
                    colTitleSoubiCount = title2Vos.Count ''Title2列数
                End If
            Else
                colTitleSoubiCount = 0
            End If

            Dim pointBodyTitleSoubi As New EditBlock2ExcelPointVo()
            pointBodyTitleSoubi.RowStart = pointBodyTitleGousya.RowStart
            pointBodyTitleSoubi.RowEnd = pointBodyTitleGousya.RowEnd
            pointBodyTitleSoubi.ColStart = pointBodyTitleGousya.ColEnd
            pointBodyTitleSoubi.ColEnd = pointBodyTitleGousya.ColEnd

            If colTitleSoubiCount > 0 Then
                If Not title2Vos Is Nothing Then
                    If Not StringUtil.IsEmpty(title2Vos(0).ShisakuSoubiHyoujiJun) Then

                        pointBodyTitleSoubi.ColStart = pointBodyTitleSoubi.ColStart
                        pointBodyTitleSoubi.ColEnd = pointBodyTitleSoubi.ColEnd + colTitleSoubiCount

                        ''Cells合併
                        xls.MergeCells(pointBodyTitleSoubi.ColStart, pointBodyTitleSoubi.RowStart, pointBodyTitleSoubi.ColEnd - 1, pointBodyTitleSoubi.RowStart, True)
                        ''位置はCenter
                        xls.SetAlignment(pointBodyTitleSoubi.ColStart, pointBodyTitleSoubi.RowStart, pointBodyTitleSoubi.ColStart, pointBodyTitleSoubi.RowStart, _
                                         XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
                        ''Set項目名のCellValue
                        xls.SetValue(pointBodyTitleSoubi.ColStart, pointBodyTitleSoubi.RowStart, pointBodyTitleSoubi.ColStart, pointBodyTitleSoubi.RowStart, "装備仕様内容")

                        ''装備仕様内容Title設定()----------------------
                        Dim i As Integer
                        For i = 0 To title2Vos.Count - 1

                            ''Cells合併
                            xls.MergeCells(pointBodyTitleSoubi.ColStart + i, pointBodyTitleSoubi.RowStart + 1, pointBodyTitleSoubi.ColStart + i, pointBodyTitleSoubi.RowEnd, True)
                            xls.SetOrientation(pointBodyTitleSoubi.ColStart + i, pointBodyTitleSoubi.RowStart + 1, pointBodyTitleSoubi.ColStart + i, pointBodyTitleSoubi.RowStart + 1, _
                                               ShisakuExcel.XlOrientation.xlVertical, False, False)
                            ''位置はCenter
                            xls.SetAlignment(pointBodyTitleSoubi.ColStart + i, pointBodyTitleSoubi.RowStart + 1, pointBodyTitleSoubi.ColStart + i, pointBodyTitleSoubi.RowEnd, _
                                         XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop, False, False)
                            ''幅設定
                            xls.SetColWidth(pointBodyTitleSoubi.ColStart + i, 2.8)
                            xls.SetRowHeight(pointBodyTitleSoubi.RowStart + 2, 200)
                            ''Value設定
                            xls.SetValue(pointBodyTitleSoubi.ColStart + i, pointBodyTitleSoubi.RowStart + 1, pointBodyTitleSoubi.ColStart + i, pointBodyTitleSoubi.RowStart + 1, _
                                         title2Vos.Item(i).ShisakuRetuKoumokuNameDai & "・" & _
                                         title2Vos.Item(i).ShisakuRetuKoumokuNameChu & "・" & _
                                         title2Vos.Item(i).ShisakuRetuKoumokuName)

                        Next

                        '上の線'
                        xls.SetLine(pointBodyTitleSoubi.ColStart, pointBodyTitleSoubi.RowStart + 1, pointBodyTitleSoubi.ColEnd, pointBodyTitleSoubi.RowStart + 1, _
                                 XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                        '下の線'
                        xls.SetLine(pointBodyTitleSoubi.ColStart, pointBodyTitleSoubi.RowStart + 5, pointBodyTitleSoubi.ColEnd, pointBodyTitleSoubi.RowEnd, _
                                    XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)

                    End If
                End If
            End If

            ''Getメモ欄内容タイトルデータ-------------------------------------------------------
            Dim title3Vos As List(Of TShisakuSekkeiBlockMemoVo) = _
                     getDate.FindWithTitle3NameBy(eventCode, seKeka, blockNoList, blockKaiteNo)

            ''Get Titleメモの列数
            Dim colTitleMemoCount As Integer = 0

            For voindex As Integer = 0 To title3Vos.Count - 1
                If colTitleMemoCount < title3Vos(voindex).ShisakuMemoHyoujiJun Then
                    colTitleMemoCount = title3Vos(voindex).ShisakuMemoHyoujiJun
                End If
            Next

            Dim pointBodyTitleMemo As New EditBlock2ExcelPointVo()
            pointBodyTitleMemo.RowStart = pointBodyTitleSoubi.RowStart
            pointBodyTitleMemo.RowEnd = pointBodyTitleSoubi.RowEnd

            pointBodyTitleMemo.ColStart = pointBodyTitleSoubi.ColEnd
            pointBodyTitleMemo.ColEnd = pointBodyTitleSoubi.ColEnd

            If colTitleMemoCount > 0 Then
                'エクセルの数値にあわせる'

                pointBodyTitleMemo.ColStart = pointBodyTitleMemo.ColStart
                pointBodyTitleMemo.ColEnd = pointBodyTitleMemo.ColEnd + colTitleMemoCount

                ''Cells合併
                xls.MergeCells(pointBodyTitleMemo.ColStart, pointBodyTitleMemo.RowStart, pointBodyTitleMemo.ColEnd, pointBodyTitleMemo.RowStart, True)
                ''位置はCenter
                xls.SetAlignment(pointBodyTitleMemo.ColStart, _
                                 pointBodyTitleMemo.RowStart, _
                                 pointBodyTitleMemo.ColStart, pointBodyTitleMemo.RowStart, _
                                 XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
                ''Set項目名のCellValue
                xls.SetValue(pointBodyTitleMemo.ColStart, pointBodyTitleMemo.RowStart, pointBodyTitleMemo.ColStart, pointBodyTitleMemo.RowStart, "メモ欄")

                ''メモ内容Title設定()----------------------
                Dim i As Integer
                For i = 0 To title3Vos.Count - 1

                    If StringUtil.Equals(title3Vos(i).ShisakuGousya, "<TITLE;>") Then
                        ''Cells合併
                        xls.MergeCells(pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, pointBodyTitleMemo.RowStart + 1, pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, pointBodyTitleMemo.RowEnd, True)
                        xls.SetOrientation(pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, pointBodyTitleMemo.RowStart + 1, pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, pointBodyTitleMemo.RowStart + 1, _
                                           ShisakuExcel.XlOrientation.xlVertical, False, False)
                        ''位置はCenter
                        xls.SetAlignment(pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, pointBodyTitleMemo.RowStart + 1, pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, pointBodyTitleMemo.RowEnd, _
                                     XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop, False, False)
                        ''幅設定
                        xls.SetColWidth(pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, 2.8)
                        xls.SetRowHeight(pointBodyTitleMemo.RowStart + 2, 200)
                        ''Value設定
                        xls.SetValue(pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, pointBodyTitleMemo.RowStart + 1, _
                                     pointBodyTitleMemo.ColStart + title3Vos(i).ShisakuMemoHyoujiJun, pointBodyTitleMemo.RowStart + 1, _
                                     title3Vos(i).ShisakuMemo)
                    End If
                Next

                '上の線'
                xls.SetLine(pointBodyTitleMemo.ColStart, pointBodyTitleMemo.RowStart + 1, pointBodyTitleMemo.ColEnd, pointBodyTitleMemo.RowStart + 1, _
                         XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                '下の線'
                xls.SetLine(pointBodyTitleMemo.ColStart, pointBodyTitleMemo.RowStart + 5, pointBodyTitleMemo.ColEnd, pointBodyTitleMemo.RowEnd, _
                            XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            End If


            Dim kaiNo As String = blockKaiteNo
            If isBase Then
                'ベースのINSTL改訂Noを設定する'
                kaiNo = "  0"
            End If

            ''GetINSTL品番タイトルデータ-------------------------------------------------------
            ''いま-----------------作業修正中
            Dim title4Vos As List(Of EditBlock2ExcelTitle4Vo) = _
                      getDate.FindWithTitle4NameBy(eventCode, seKeka, blockNoList, kaiNo, isBase)

            ''Get Title4の列数
            Dim colTitleInsuCount As Integer
            colTitleInsuCount = title4Vos.Count ''Title2列数

            Dim pointBodyTitleInsu As New EditBlock2ExcelPointVo()
            pointBodyTitleInsu.RowStart = pointBodyTitleMemo.RowStart
            pointBodyTitleInsu.RowEnd = pointBodyTitleMemo.RowEnd

            pointBodyTitleInsu.ColStart = pointBodyTitleMemo.ColEnd
            pointBodyTitleInsu.ColEnd = pointBodyTitleMemo.ColEnd

            Dim allGousyaList As List(Of TShisakuEventBaseVo) = getDate.FindAllGouSya(eventCode)

            '員数作成'
            If colTitleInsuCount > 0 Then
                'pointBodyTitleInsu.ColStart = pointBodyTitleInsu.ColStart
                'メモがずれる'
                If pointBodyTitleMemo.ColEnd = pointBodyTitleSoubi.ColEnd And colTitleSoubiCount = 0 Then

                    pointBodyTitleInsu.ColStart = pointBodyTitleInsu.ColStart
                    pointBodyTitleInsu.ColEnd = pointBodyTitleInsu.ColEnd + colTitleInsuCount - 1
                Else
                    pointBodyTitleInsu.ColStart = pointBodyTitleInsu.ColStart + 1
                    pointBodyTitleInsu.ColEnd = pointBodyTitleInsu.ColEnd + colTitleInsuCount
                End If




                ''Cells合併
                xls.MergeCells(pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart, pointBodyTitleInsu.ColEnd, pointBodyTitleInsu.RowStart, True)
                ''位置はCenter
                xls.SetAlignment(pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart, pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart, _
                                 XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
                ''Set項目名のCellValue
                xls.SetValue(pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart, pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart, "INSTL品番")
                'INSTL品番は横書き'
                xls.SetOrientation(pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart, pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart, ShisakuExcel.XlOrientation.xlHorizontal)
                ''INSTL品番内容Title設定----------------------
                Dim i As Integer

                Dim insuRowEnd As Integer = 0
                For Each vo As TShisakuEventBaseVo In allGousyaList
                    If vo.HyojijunNo > insuRowEnd Then
                        insuRowEnd = vo.HyojijunNo
                    End If
                Next
                insuRowEnd += 1


                For i = 0 To title4Vos.Count - 1
                    xls.MergeCells(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 5, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowEnd, True)
                    xls.SetOrientation(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 2, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 2, _
                                       ShisakuExcel.XlOrientation.xlVertical, False, False)
                    xls.SetOrientation(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, _
                                       ShisakuExcel.XlOrientation.xlVertical, False, False)
                    ''↓↓2014/09/03 Ⅰ.10.全ブロックEXCEL出力での出力内容変更_d) 酒井 ADD BEGIN
                    xls.SetOrientation(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 4, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 4, _
                                      ShisakuExcel.XlOrientation.xlVertical, False, False)
                    ''↑↑2014/09/03 Ⅰ.10.全ブロックEXCEL出力での出力内容変更_d) 酒井 ADD END
                    xls.SetOrientation(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 5, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 5, _
                                      ShisakuExcel.XlOrientation.xlVertical, False, False)
                    ''位置はCenter
                    xls.SetAlignment(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 1, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowEnd + 3, _
                                 XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop, False, False)
                    ''幅設定
                    xls.SetColWidth(pointBodyTitleInsu.ColStart + i, 2.8)
                    xls.SetRowHeight(pointBodyTitleInsu.RowStart + 4, 150)
                    xls.SetRowHeight(pointBodyTitleInsu.RowStart + 5, 50)

                    ''Value設定
                    xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 1, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 1, _
                                 IntToString(i))

                    ''ブロックNo Value設定
                    'If Not String.IsNullOrEmpty(blockNoList) AndAlso Not String.IsNullOrEmpty(blockKaiteNo) Then
                    If blockNoList IsNot Nothing AndAlso blockNoList.Count = 1 Then

                        ''仕様書情報 Value設定
                        xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, _
                                     Trim(title4Vos.Item(i).BuhinNote))
                    Else
                        xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 2, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 2, _
                                    title4Vos.Item(i).ShisakuBlockNo)
                        xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 3, _
                                    Trim(title4Vos.Item(i).BuhinNote))
                    End If

                    ''INSTL品番Value設定
                    xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 4, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 4, _
                                 title4Vos.Item(i).InstlHinban)
                    ''INSTL品番区分Value設定
                    xls.SetValue(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 5, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 5, _
                                 title4Vos.Item(i).InstlHinbanKbn)

                    '該当イベント取得
                    If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                        If title4Vos.Item(i).BaseInstlFlg = 1 Then
                            ''↓↓2014/09/03 Ⅰ.3.設計編集 ベース改修専用化_v) 酒井 ADD BEGIN
                            xls.SetBackColor(pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowStart + 1, pointBodyTitleInsu.ColStart + i, pointBodyTitleInsu.RowEnd + insuRowEnd, RGB(176, 215, 237))
                            ''↑↑2014/09/03 Ⅰ.3.設計編集 ベース改修専用化_v) 酒井 ADD END
                        End If
                    End If
                Next
                '上の線'
                xls.SetLine(pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart + 1, pointBodyTitleInsu.ColEnd, pointBodyTitleInsu.RowStart + 1, _
                         XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                '下の線'
                xls.SetLine(pointBodyTitleInsu.ColStart, pointBodyTitleInsu.RowStart + 5, pointBodyTitleInsu.ColEnd, pointBodyTitleInsu.RowEnd, _
                        XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            Else
                ''Cells合併
                xls.MergeCells(pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart, pointBodyTitleInsu.ColEnd + 1, pointBodyTitleInsu.RowStart, True)
                ''位置はCenter
                xls.SetAlignment(pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart, pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart, _
                                 XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
                ''Set項目名のCellValue
                xls.SetValue(pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart, pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart, "INSTL品番")
                'INSTL品番は横書き'
                xls.SetOrientation(pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart, pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart, ShisakuExcel.XlOrientation.xlHorizontal)
                '上の線'
                xls.SetLine(pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart + 1, pointBodyTitleInsu.ColEnd + 1, pointBodyTitleInsu.RowStart + 1, _
                         XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                '下の線'
                xls.SetLine(pointBodyTitleInsu.ColStart + 1, pointBodyTitleInsu.RowStart + 5, pointBodyTitleInsu.ColEnd + 1, pointBodyTitleInsu.RowEnd, _
                            XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            End If


            ''Body作成
            '号車名取得'
            Dim strGousya As String

            '号車の開始位置'
            Dim pointGousya As New EditBlock2ExcelPointVo()
            pointGousya.RowStart = pointBodyTitleGousya.RowEnd + 1
            pointGousya.RowEnd = pointBodyTitleGousya.RowEnd + 1
            pointGousya.ColStart = 1
            pointGousya.ColEnd = 1

            '号車の中身の開始'
            Dim pointBodyGousya As New EditBlock2ExcelPointVo()
            pointBodyGousya.RowStart = pointBodyTitleGousya.RowEnd + 1
            pointBodyGousya.RowEnd = pointBodyTitleGousya.RowEnd + 1
            pointBodyGousya.ColStart = 2
            pointBodyGousya.ColEnd = 2

            '装備仕様の開始位置'
            Dim pointBodySoubi As New EditBlock2ExcelPointVo()
            pointBodySoubi.RowStart = pointBodyTitleGousya.RowEnd + 1
            pointBodySoubi.RowEnd = pointBodyTitleGousya.RowEnd + 1
            pointBodySoubi.ColStart = 2 + colTitleGousyaCount
            pointBodySoubi.ColEnd = 1 + colTitleGousyaCount + colTitleSoubiCount

            'メモ欄の開始位置'
            Dim pointBodyMemo As New EditBlock2ExcelPointVo()
            pointBodyMemo.RowStart = pointBodyTitleGousya.RowEnd + 1
            pointBodyMemo.RowEnd = pointBodyTitleGousya.RowEnd + 1
            pointBodyMemo.ColStart = 2 + colTitleGousyaCount + colTitleSoubiCount
            pointBodyMemo.ColEnd = 1 + colTitleGousyaCount + colTitleSoubiCount + colTitleMemoCount

            Dim pointBodyInsu As New EditBlock2ExcelPointVo()

            pointBodyInsu.RowStart = pointBodyTitleGousya.RowEnd + 1
            pointBodyInsu.RowEnd = pointBodyTitleGousya.RowEnd + 1

            'ずれまくるので一旦無理やり'
            If colTitleSoubiCount = 0 Then

                If colTitleMemoCount = 0 Then
                    pointBodyInsu.ColStart = 2 + colTitleGousyaCount + colTitleSoubiCount + colTitleMemoCount
                    pointBodyInsu.ColEnd = 2 + colTitleGousyaCount + colTitleSoubiCount + colTitleMemoCount

                Else
                    pointBodyInsu.ColStart = 3 + colTitleGousyaCount + colTitleSoubiCount + colTitleMemoCount
                    pointBodyInsu.ColEnd = 2 + colTitleGousyaCount + colTitleSoubiCount + colTitleMemoCount + colTitleInsuCount
                End If

            Else
                pointBodyInsu.ColStart = 3 + colTitleGousyaCount + colTitleSoubiCount + colTitleMemoCount
                pointBodyInsu.ColEnd = 2 + colTitleGousyaCount + colTitleSoubiCount + colTitleMemoCount + colTitleInsuCount
            End If


            'Dim j As Integer
            If Not allGousyaList.Count = 0 Then

                Dim MaxIndex As Integer = 0
                For Each g As TShisakuEventBaseVo In allGousyaList
                    If MaxIndex < g.HyojijunNo Then
                        MaxIndex = g.HyojijunNo
                    End If
                Next



                'メモ出力用のマトリクス
                Dim dataMatrixForMemo(MaxIndex, colTitleMemoCount) As String
                For x As Integer = 0 To UBound(dataMatrixForMemo)
                    For y As Integer = 0 To UBound(dataMatrixForMemo, 2)
                        dataMatrixForMemo(x, y) = ""
                    Next
                Next

                '員数出力用のマトリクス
                Dim dataMatrixForInsu(MaxIndex, colTitleInsuCount) As String
                For s As Integer = 0 To UBound(dataMatrixForInsu)
                    For t As Integer = 0 To UBound(dataMatrixForInsu, 2)
                        dataMatrixForInsu(s, t) = ""
                    Next
                Next

                ''BodyDataLoop
                For Each gVo As TShisakuEventBaseVo In allGousyaList
                    'Get号車値(step1)
                    strGousya = gVo.ShisakuGousya
                    ''号車値の設定
                    xls.SetValue(pointGousya.ColStart, pointGousya.RowStart + gVo.HyojijunNo, pointGousya.ColEnd, pointGousya.RowEnd + gVo.HyojijunNo, strGousya)
                    '位置はCenter
                    xls.SetAlignment(pointGousya.ColStart, pointGousya.RowStart + gVo.HyojijunNo, pointGousya.ColEnd, pointGousya.RowEnd + gVo.HyojijunNo, _
                                     XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)


                    'Get試作イベントベース車情報と試作イベント完成車情報の値(step2)
                    Dim titleGousyaBody As EditBlock2ExcelTitle1BodyVo = _
                        getDate.FindWithTitle1BodyDataBy(eventCode, seKeka, "", blockKaiteNo, strGousya)

                    Dim k As Integer

                    For k = 0 To titleVos.Count - 1

                        Dim strTitleGousya As String = ""
                        Dim strTitleGousyaBodyValue As String = ""
                        strTitleGousya = titleVos.Item(k).ShisakuSoubiHyoujiJun
                        strTitleGousyaBodyValue = GetTitle1Value(strTitleGousya, titleGousyaBody)

                        ''試作イベントベース車情報と試作イベント完成車情報の値設定
                        xls.SetValue(pointBodyGousya.ColStart + k, pointBodyGousya.RowStart + gVo.HyojijunNo, pointBodyGousya.ColStart + k, pointBodyGousya.RowEnd + gVo.HyojijunNo, strTitleGousyaBodyValue)
                        ''位置はCenter
                        xls.SetAlignment(pointBodyGousya.ColStart + k, pointBodyGousya.RowStart + gVo.HyojijunNo, pointBodyGousya.ColStart + k, pointBodyGousya.RowEnd + gVo.HyojijunNo, _
                                         XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
                    Next

                    ''Get試作装備仕様の値(step3)
                    Dim titleSoubiBody As List(Of EditBlock2ExcelTitle2BodyVo) = _
                        getDate.FindWithTitle2BodyDataBy(eventCode, seKeka, "", blockKaiteNo, strGousya)


                    Dim l As Integer
                    For l = 0 To title2Vos.Count - 1

                        Dim strtitle2BodyValue As String = ""
                        Dim strSoubiTitle As String = ""
                        For i As Integer = 0 To titleSoubiBody.Count - 1
                            'エラー回避'
                            If Not titleSoubiBody.Count = 0 Then
                                strtitle2BodyValue = titleSoubiBody.Item(i).ShisakuTekiyou
                                strSoubiTitle = titleSoubiBody(i).ShisakuRetuKoumokuNameDai & "・" & _
                                titleSoubiBody.Item(i).ShisakuRetuKoumokuNameChu & "・" & _
                                titleSoubiBody.Item(i).ShisakuRetuKoumokuName
                            End If

                            ''20110831樺澤 タイトル名が無い場合は飛ばす処理を追加
                            If Not strtitle2BodyValue Is Nothing Then
                                If xls.GetValue(Integer.Parse(pointBodySoubi.ColStart) + l, 4).ToString = strSoubiTitle Then
                                    ''試作イベントベース車情報と試作イベント完成車情報の値設定
                                    xls.SetValue(pointBodySoubi.ColStart + l, pointBodySoubi.RowStart + gVo.HyojijunNo, pointBodySoubi.ColStart + l, pointBodySoubi.RowEnd + gVo.HyojijunNo, strtitle2BodyValue)
                                    ''位置はCenter
                                    xls.SetAlignment(pointBodySoubi.ColStart + l, pointBodySoubi.RowStart + gVo.HyojijunNo, pointBodySoubi.ColStart + l, pointBodySoubi.RowEnd + gVo.HyojijunNo, _
                                                     XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
                                End If
                            End If
                        Next

                    Next

                    'Get試作メモの値(step4)
                    Dim titleMemoBody As List(Of TShisakuSekkeiBlockMemoVo) = _
                        getDate.FindWithTitle3BodyDataBy(eventCode, seKeka, blockNoList, blockKaiteNo, strGousya)

                    Dim m As Integer
                    Dim n As Integer

                    Dim index As Integer = 0

                    '-------------------------------------------------------------------
                    'ここをチューニング
                    '-------------------------------------------------------------------
                    For m = 0 To title3Vos.Count - 1
                        If titleMemoBody.Count > 0 Then
                            For n = 0 To titleMemoBody.Count - 1
                                If title3Vos(m).ShisakuMemoHyoujiJun = titleMemoBody(n).ShisakuMemoHyoujiJun Then

                                    dataMatrixForMemo(gVo.HyojijunNo, titleMemoBody(n).ShisakuMemoHyoujiJun) = titleMemoBody(n).ShisakuTekiyou
                                End If
                            Next
                        End If
                    Next

                    'Get試作INSTL品番の人数値(step5)
                    Dim title4Body As List(Of TShisakuSekkeiBlockInstlVo) = _
                    getDate.FindWithTitle4BodyDataBy(eventCode, seKeka, blockNoList, kaiNo, strGousya)

                    Dim p As Integer
                    Dim q As Integer
                    Dim pindex As Integer = 0
                    For p = 0 To title4Vos.Count - 1

                        Dim strHinbanHyoujiJun As String = ""
                        strHinbanHyoujiJun = title4Vos.Item(p).InstlHinbanHyoujiJun

                        If title4Body.Count > 0 Then

                            For q = 0 To title4Body.Count - 1

                                '2014/11/19 インストール品番を条件に追加
                                If title4Vos(p).InstlHinban = title4Body(q).InstlHinban Then
                                    If title4Vos(p).InstlHinbanHyoujiJun = title4Body(q).InstlHinbanHyoujiJun Then
                                        ''試作INSTL品番の人数値設定
                                        If title4Body.Item(q).InsuSuryo < 0 Then
                                            dataMatrixForInsu(gVo.HyojijunNo, pindex) = "**"
                                        Else
                                            dataMatrixForInsu(gVo.HyojijunNo, pindex) = title4Body.Item(q).InsuSuryo.ToString()
                                        End If
                                    End If
                                End If
                            Next
                        End If

                        pindex = pindex + 1
                    Next

                Next



                xls.CopyRange(pointBodyMemo.ColStart - 1, pointBodyGousya.RowStart, pointBodyMemo.ColStart + UBound(dataMatrixForMemo, 2) - 1, pointBodyGousya.RowStart + UBound(dataMatrixForMemo), dataMatrixForMemo)
                xls.CopyRange(pointBodyInsu.ColStart - 1, pointBodyGousya.RowStart, pointBodyInsu.ColStart + UBound(dataMatrixForInsu, 2) - 1, pointBodyGousya.RowStart + UBound(dataMatrixForInsu), dataMatrixForInsu)
                'ここでまとめてAlignmentの設定
                xls.SetAlignment(pointBodyMemo.ColStart, pointBodyGousya.RowStart, pointBodyMemo.ColStart + UBound(dataMatrixForMemo, 2) - 1, pointBodyGousya.RowStart + UBound(dataMatrixForMemo), _
                                 XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
                xls.SetAlignment(pointBodyInsu.ColStart, pointBodyInsu.RowStart, pointBodyInsu.ColStart + UBound(dataMatrixForInsu, 2) - 1, pointBodyInsu.RowStart + UBound(dataMatrixForInsu), _
                                 XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)

            End If

            '縦線を描く
            xls.SetLine(pointBodyTitleGousya.ColEnd, point.RowStart, pointBodyTitleGousya.ColEnd, xls.EndRow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
            xls.SetLine(pointBodyTitleSoubi.ColEnd, point.RowStart, pointBodyTitleSoubi.ColEnd, xls.EndRow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
            xls.SetLine(pointBodyTitleMemo.ColStart, point.RowStart, pointBodyTitleMemo.ColStart, xls.EndRow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
            xls.SetLine(pointBodyTitleInsu.ColStart, point.RowStart, pointBodyTitleInsu.ColStart, xls.EndRow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)

        End Sub




#End Region

#End Region

#Region "Excel出力　シート２の部分"
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <param name="eventCode"></param>
        ''' <param name="seKeka"></param>
        ''' <param name="blockNo"></param>
        ''' <param name="blockKaiteNo"></param>
        ''' <remarks></remarks>
        Public Sub setSheet2(ByVal xls As ShisakuExcel, _
                             ByVal eventCode As String, _
                             ByVal seKeka As String, _
                             ByVal blockNo As String, _
                             ByVal blockKaiteNo As String, _
                             ByVal tanTouSya As String, _
                             ByVal tel As String, _
                             ByVal shisakuKaihatsuFugo As String, _
                             ByVal shisakuEventName As String)

            xls.SetActiveSheet(2)

            'HeadPart開始設定
            Dim headPartPoint As New EditBlock2ExcelPointVo()
            headPartPoint.RowStart = 1
            headPartPoint.RowEnd = 1
            headPartPoint.ColStart = 1
            headPartPoint.ColEnd = 1

            ''Draw HeadPart1
            headPartPoint = setSheet2HeadPart1(xls, eventCode, seKeka, blockNo, blockKaiteNo, tanTouSya, tel, headPartPoint, shisakuKaihatsuFugo, shisakuEventName)
            headPartPoint.RowStart = headPartPoint.RowStart + 2
            headPartPoint.RowEnd = headPartPoint.RowEnd + 2

            Dim headPart2Point As New EditBlock2ExcelPointVo()


            ''Draw Body Title1とTitle2
            setSheet2BodyPart(xls, eventCode, seKeka, blockNo, blockKaiteNo, headPart2Point)

        End Sub

        ''Draw Head
        Public Function setSheet2HeadPart1(ByVal xls As ShisakuExcel, _
                                       ByVal eventCode As String, _
                                       ByVal seKeka As String, _
                                       ByVal blockNo As String, _
                                       ByVal blockKaiteNo As String, _
                                       ByVal tanTouSya As String, _
                                       ByVal tel As String, _
                                       ByVal point As EditBlock2ExcelPointVo, _
                                       ByVal shisakuKaihatsuFugo As String, _
                                       ByVal shisakuEventName As String) As EditBlock2ExcelPointVo


            xls.SetValue(point.ColStart, point.RowStart, "MF1台車(トリム)")
            xls.SetValue(point.ColStart + 6, point.RowStart, "担当設計：" + seKeka + " 担当者：" + tanTouSya + " Tel: " + tel)
            xls.SetValue(point.ColStart, point.RowStart + 1, "ブロックNo：" + blockNo)

            Return point
        End Function

        ''Draw Body
        Public Sub setSheet2BodyPart(ByVal xls As ShisakuExcel, _
                                      ByVal eventCode As String, _
                                      ByVal seKeka As String, _
                                      ByVal blockNo As String, _
                                      ByVal blockKaiteNo As String, _
                                      ByVal point As EditBlock2ExcelPointVo)

        End Sub

#End Region

#Region "Excel出力　シート３の部分"
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <param name="eventCode"></param>
        ''' <param name="eventName"></param>
        ''' <param name="dtHeard"></param>
        ''' <param name="strFlag"></param>
        ''' <remarks></remarks>
        Public Sub setSheet3(ByVal xls As ShisakuExcel, _
                             ByVal eventCode As String, _
                             ByVal eventName As String, _
                             ByVal dtHeard As DataSet, _
                             ByVal strFlag As String)
            xls.SetActiveSheet(2)

        End Sub


#End Region

        ''' <summary>
        ''' ベース車データを読む
        ''' </summary>
        ''' <param name="strKomokuNo">項目No.</param>
        ''' <param name="vo">ベース車データ</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetTitle1Value(ByVal strKomokuNo As String, ByVal vo As EditBlock2ExcelTitle1BodyVo) As String
            Dim strKomokuName As String = ""

            '無いものは呼ばない'
            If vo Is Nothing Then
                Return ""
            End If


            Select Case strKomokuNo
                ''ベース車開発符号
                Case "1001"
                    strKomokuName = vo.BaseKaihatsuFugo
                    ''ベース車仕様情報№
                Case "1002"
                    strKomokuName = vo.BaseShiyoujyouhouNo
                    ''ベース車アプライド№
                Case "1003"
                    strKomokuName = vo.BaseAppliedNo
                    ''ベース車型式
                Case "1004"
                    strKomokuName = vo.BaseKatashiki
                    ''ベース車仕向
                Case "1005"
                    strKomokuName = vo.BaseShimuke
                    ''ベース車OP
                Case "1006"
                    strKomokuName = vo.BaseOp
                    ''ベース車外装色
                Case "1007"
                    strKomokuName = vo.BaseGaisousyoku
                    ''ベース車内装色
                Case "1008"
                    strKomokuName = vo.BaseNaisousyoku
                    ''試作ベースイベントコード
                Case "1009"
                    strKomokuName = vo.ShisakuBaseEventCode
                    ''試作ベース号車
                Case "1010"
                    strKomokuName = vo.ShisakuBaseGousya
                    '' 参考情報・車型
                Case "1011"
                    strKomokuName = vo.SeisakuSyasyu()
                    '' 参考情報・グレード
                Case "1012"
                    strKomokuName = vo.SeisakuGrade
                    '' 参考情報・仕向地・仕向け
                Case "1013"
                    strKomokuName = vo.SeisakuShimuke
                    '' 参考情報・仕向地・ハンドル
                Case "1014"
                    strKomokuName = vo.SeisakuHandoru
                    '' 参考情報・E/G排気量
                Case "1015"
                    strKomokuName = vo.SeisakuEgHaikiryou
                    '' 参考情報・E/G型式
                Case "1016"
                    strKomokuName = vo.SeisakuEgKatashiki
                    '' 参考情報・E/G過給器
                Case "1017"
                    strKomokuName = vo.SeisakuEgKakyuuki
                    '' 参考情報・T/M駆動方式
                Case "1018"
                    strKomokuName = vo.SeisakuTmKudou
                    '' 参考情報・T/M変速機
                Case "1019"
                    strKomokuName = vo.SeisakuTmHensokuki
                    '' 参考情報・車体№
                Case "1020"
                    strKomokuName = vo.SeisakuSyataiNo


                    ''設計展開ベース車開発符号
                Case "1100"
                    strKomokuName = vo.TenkaiBaseKaihatsuFugo
                    ''設計展開ベース車仕様情報№
                Case "1101"
                    strKomokuName = vo.TenkaiBaseShiyoujyouhouNo
                    ''設計展開ベース車アプライド№
                Case "1102"
                    strKomokuName = vo.TenkaiBaseAppliedNo
                    ''設計展開ベース車型式
                Case "1103"
                    strKomokuName = vo.TenkaiBaseKatashiki
                    ''設計展開ベース車仕向
                Case "1104"
                    strKomokuName = vo.TenkaiBaseShimuke
                    ''設計展開ベース車OP
                Case "1105"
                    strKomokuName = vo.TenkaiBaseOp
                    ''設計展開ベース車外装色
                Case "1106"
                    strKomokuName = vo.TenkaiBaseGaisousyoku
                    ''設計展開ベース車内装色
                Case "1107"
                    strKomokuName = vo.TenkaiBaseNaisousyoku
                    ''設計展開試作ベースイベントコード
                Case "1108"
                    strKomokuName = vo.TenkaiShisakuBaseEventCode
                    ''設計展開試作ベース号車
                Case "1109"
                    strKomokuName = vo.TenkaiShisakuBaseGousya



                    ''完成試作型式
                Case "2001"
                    strKomokuName = vo.ShisakuKatashiki
                    ''完成試作仕向け
                Case "2002"
                    strKomokuName = vo.ShisakuShimuke
                    ''完成試作OP
                Case "2003"
                    strKomokuName = vo.ShisakuOp
                    ''完成試作ハンドル
                Case "2004"
                    strKomokuName = vo.ShisakuHandoru
                    ''完成試作車型
                Case "2005"
                    strKomokuName = vo.ShisakuSyagata
                    ''完成試作グレード
                Case "2006"
                    strKomokuName = vo.ShisakuGrade
                    ''完成試作車台№
                Case "2007"
                    strKomokuName = vo.ShisakuSyadaiNo
                    ''完成試作外装色
                Case "2008"
                    strKomokuName = vo.ShisakuGaisousyoku
                    ''完成試作内装色
                Case "2009"
                    strKomokuName = vo.ShisakuNaisousyoku
                    ''完成試作グループ
                Case "2010"
                    strKomokuName = vo.ShisakuGroup
                    ''完成試作工指№
                Case "2011"
                    strKomokuName = vo.ShisakuKoushiNo
                    ''完成試作完成日
                Case "2012"
                    strKomokuName = vo.ShisakuKanseibi
                    ''完成試作E/G型式
                Case "2013"
                    strKomokuName = vo.ShisakuEgKatashiki
                    ''完成試作E/G排気量
                Case "2014"
                    strKomokuName = vo.ShisakuEgHaikiryou
                    ''完成試作E/Gシステム
                Case "2015"
                    strKomokuName = vo.ShisakuEgSystem
                    ''完成試作E/G過給機
                Case "2016"
                    strKomokuName = vo.ShisakuEgKakyuuki
                    ''完成試作T/M駆動
                Case "2017"
                    strKomokuName = vo.ShisakuTmKudou
                    ''完成試作T/M変速機
                Case "2018"
                    strKomokuName = vo.ShisakuTmHensokuki
                    ''完成試作T/M副変速機
                Case "2019"
                    strKomokuName = vo.ShisakuTmFukuHensokuki
                    ''完成試作使用部署
                Case "2020"
                    strKomokuName = vo.ShisakuSiyouBusyo
                    ''完成試作試験目的
                Case "2021"
                    strKomokuName = vo.ShisakuShikenMokuteki
                    '' 仕向地・仕向け
                Case "2022"
                    strKomokuName = vo.ShisakuShimukechiShimuke
                    '' 完成試作E/Gメモ１
                Case "2023"
                    strKomokuName = vo.ShisakuEgMemo1
                    '' 完成試作E/Gメモ２
                Case "2024"
                    strKomokuName = vo.ShisakuEgMemo2
                    '' 完成試作T/Mメモ１
                Case "2025"
                    strKomokuName = vo.ShisakuTmMemo1
                    '' 完成試作T/Mメモ２
                Case "2026"
                    strKomokuName = vo.ShisakuTmMemo2
                    '' 完成試作外装色名
                Case "2027"
                    strKomokuName = vo.ShisakuGaisousyokuName
                    '' 完成試作内装色名
                Case "2028"
                    strKomokuName = vo.ShisakuNaisousyokuName
                    '' 完成試作使用目的
                Case "2029"
                    strKomokuName = vo.ShisakuShiyouMokuteki
                    '' 完成試作製作順序
                Case "2030"
                    strKomokuName = vo.ShisakuSeisakuJunjyo
                    '' 完成試作製作方法区分
                Case "2031"
                    strKomokuName = vo.ShisakuSeisakuHouhouKbn
                    '' 完成試作製作方法
                Case "2032"
                    strKomokuName = vo.ShisakuSeisakuHouhou
                    '' 完成試作メモ欄
                Case "2033"
                    strKomokuName = vo.ShisakuMemo

            End Select
            Return strKomokuName
        End Function

        ''' <summary>
        ''' GetタイトルA,B,C...
        ''' </summary>
        ''' <param name="loopNum">Loop number</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function IntToString(ByVal loopNum As Integer) As String
            If loopNum >= 702 Then
                Return ""
            Else
                If loopNum < 26 Then
                    Return IntToChar(loopNum)
                Else
                    Dim num1 As Integer = loopNum \ 26 - 1
                    Dim num2 = loopNum Mod 26
                    Return IntToChar(num1) & IntToChar(num2)
                End If
            End If
        End Function
        ''' <summary>
        ''' Chrを返す
        ''' </summary>
        ''' <param name="num">number</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function IntToChar(ByVal num As Integer) As String
            Return Chr(Asc("A") + num)
        End Function

    End Class
End Namespace