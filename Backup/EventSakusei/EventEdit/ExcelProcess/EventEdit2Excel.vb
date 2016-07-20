Imports FarPoint.Win.Spread
Imports FarPoint.Win
Imports ShisakuCommon
Imports EBom.Excel
Imports EventSakusei.EventEdit
Imports EventSakusei.EventEdit.Logic
Imports ShisakuCommon.Ui

Namespace EventEdit.Export2Excel
    ''' <summary>
    ''' Excelに出力
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EventEdit2Excel

        Private m_OutFileName As String = Nothing
        Private m_ExcelMaxColumnNum As Integer = 256        '2012/02/21 列が２５６を超えた場合エラーになるのを回避
        Private m_isOverColumns As Boolean = False

        Private strEgMemo1 As String
        Private strEgMemo2 As String
        Private strTmMemo1 As String
        Private strTmMemo2 As String

#Region "Construct"

        ''' <summary>
        ''' Construct
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

        ''' <summary>
        ''' Construct
        ''' </summary>
        ''' <param name="subject">EventEdit Subject</param>
        ''' <param name="aLoginInfo">使用者情報</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal subject As Logic.EventEdit, ByVal aLoginInfo As LoginInfo, _
                       ByVal strEgMemo1 As String, ByVal strEgMemo2 As String, _
                       ByVal strTmMemo1 As String, ByVal strTmMemo2 As String)
            Me.Subject = subject
            Me.aLoginInfo = aLoginInfo
            Me.BaseCarSubject = subject.BaseCarSubject
            Me.BaseTenkaiCarSubject = subject.BaseTenkaiCarSubject
            Me.CompleteCarSubject = subject.CompleteCarSubject
            Me.BasicOptionSubject = subject.BasicOptionSubject
            Me.SpecialOptionSubject = subject.SpecialOptionSubject
            Me.EbomKanshiSubject = subject.EbomKanshiSubject

            Me.BasicOptionStartColumn = 56
            Me.RowsCount = 0
            Me.ColumnsCount = 57

            Me.strEgMemo1 = strEgMemo1
            Me.strEgMemo2 = strEgMemo2
            Me.strTmMemo1 = strTmMemo1
            Me.strTmMemo2 = strTmMemo2

        End Sub
#End Region

#Region "起動"
        ''' <summary>
        ''' 起動
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Execute()
            Dim fileName As String
            Using sfd As New SaveFileDialog()
                sfd.FileName = ShisakuCommon.ShisakuGlobal.ExcelOutPut
                sfd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                '2012/01/25
                '2012/01/21
                'fileName = sfd.InitialDirectory + "\" + sfd.FileName
                '[Excel出力系 O]
                '--------------------------------------------------
                'EXCEL出力先フォルダチェック
                ShisakuCommon.ShisakuComFunc.ExcelOutPutDirCheck()
                '--------------------------------------------------
                fileName = Subject.HeaderSubject.ShisakuKaihatsuFugo + Subject.HeaderSubject.ShisakuEventName + " イベント登録・編集 " + Now.ToString("MMdd") + Now.ToString("HHmm") + ".xls"
                fileName = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + StringUtil.ReplaceInvalidCharForFileName(fileName)	'2012/02/08 Excel出力ディレクトリ指定対応
                '一旦モジュール変数へ待避する
                m_OutFileName = fileName
            End Using

            If Not ShisakuComFunc.IsFileOpen(ShisakuCommon.ShisakuGlobal.ExcelOutPut) Then
                Using xls As New ShisakuExcel(fileName)
                    xls.OpenBook(fileName)
                    xls.ClearWorkBook()
                    ColumnTagRenban()
                    setHeard(xls)
                    setIncrement(xls)
                    setBaseCar(xls)
                    setReferenceCar(xls)
                    setCompleteCar(xls)
                    setBasicOption(xls)
                    setSpecialOption(xls)
                    '2014/11/20 追加
                    setEbomKanshi(xls)

                    setKeisen(xls)
                    setWidthHeight(xls)
                    freezeCell(xls)
                    'とりあえずアクティブにしてみる。
                    xls.SetActiveSheet(1)
                    'A4横で印刷できるように変更'
                    xls.PrintPaper(fileName, 1, "A3")
                    xls.PrintOrientation(fileName, 1, 0, False)
                    xls.Save()
                End Using

                If m_isOverColumns Then
                    EBom.Common.ComFunc.ShowInfoMsgBox("出力列が２５６列を超えた為、全てのデータを表示できませんでした。", MessageBoxButtons.OK)
                End If

                'ここは外してみた。
                'Process.Start(fileName)
            End If
            
        End Sub
#End Region

        '一旦ここにルーチンを分離してみた。
        Public Sub OpenExcelOnScreen()
            System.Diagnostics.Process.Start(m_OutFileName)
            EBom.Common.ComFunc.ShowInfoMsgBox("Excel出力が完了しました", MessageBoxButtons.OK)
        End Sub

#Region "Heard部分"
        ''' <summary>
        ''' Excelヘーダ部分の出力
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub setHeard(ByVal xls As ShisakuExcel)
            xls.SetFont("ＭＳ ゴシック", 11)

            xls.MergeCells(2, 1, 3, 1, True)
            xls.SetAlignment(2, 1, 2, 1, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, True, True)
            xls.SetValue(2, 1, "出力日")
            Dim syutuRyokuDate = Now().ToString("yyyy/MM/dd HH:mm:ss ")
            xls.MergeCells(4, 1, 5, 1, True)
            xls.SetValue(4, 1, syutuRyokuDate)

            xls.MergeCells(2, 2, 3, 2, True)
            xls.SetAlignment(2, 2, 2, 2, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, True, True)
            xls.SetValue(2, 2, "開発符号")
            xls.SetValue(4, 2, Subject.HeaderSubject.ShisakuKaihatsuFugo)

            xls.SetValue(6, 1, "イベント")
            xls.SetValue(7, 1, Subject.HeaderSubject.ShisakuEventPhaseName)

            xls.SetAlignment(6, 2, 6, 2, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, True, True)
            xls.SetValue(6, 2, "イベント名称")
            xls.MergeCells(7, 2, 10, 2, True)
            xls.SetValue(7, 2, Subject.HeaderSubject.ShisakuEventName)

            xls.SetAlignment(14, 2, 14, 2, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter, True, True)
            xls.SetValue(14, 2, "現在")
            xls.MergeCells(15, 2, 16, 2, True)
            xls.SetAlignment(15, 2, 15, 2, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            xls.SetValue(15, 2, Subject.HeaderSubject.StatusName)
            xls.MergeCells(17, 2, 18, 2, True)
            xls.SetAlignment(17, 2, 17, 2, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            xls.SetValue(17, 2, Subject.HeaderSubject.DataKbnName)

            xls.MergeCells(2, 3, 3, 3, True)
            xls.SetAlignment(2, 3, 2, 3, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, True, True)
            xls.SetValue(2, 3, "M/T/S区分")
            xls.SetValue(4, 3, Subject.HeaderSubject.UnitKbn)

            Dim seisakuFrom As Date = Subject.HeaderSubject.SeisakujikiFrom
            Dim seisakuTo As Date = Subject.HeaderSubject.SeisakujikiTo
            Dim seisakuDate As String = seisakuFrom.ToString("yyyy/MM/dd") + " - " + seisakuTo.ToString("yyyy/MM/dd")

            xls.SetValue(6, 3, "製作時期")
            xls.MergeCells(7, 3, 10, 3, True)
            xls.SetOrientation(7, 3, 7, 3, ShisakuExcel.XlOrientation.xlHorizontal, False, True)
            xls.SetValue(7, 3, seisakuDate)

            'xls.SetValue(7, 3, "-")
            'xls.SetValue(8, 3, seisakuTo.ToString("yyyy/MM/dd"))

            xls.SetAlignment(12, 3, 12, 3, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter, True, True)
            xls.SetValue(12, 3, "製作台数")
            xls.SetValue(13, 3, "完成車")
            xls.SetValue(14, 3, Subject.HeaderSubject.SeisakudaisuKanseisya)

            xls.SetValue(15, 3, "W/B")
            xls.SetValue(16, 3, Subject.HeaderSubject.SeisakudaisuWb)

            'xls.MergeCells(17, 3, 17, 3, True)
            xls.SetAlignment(17, 3, 17, 3, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            xls.SetValue(17, 3, "製作中止")
            xls.SetValue(18, 3, Subject.HeaderSubject.SeisakudaisuChushi)

            xls.MergeCells(2, 4, 3, 4, True)
            xls.SetAlignment(2, 4, 2, 4, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, True, True)
            xls.SetValue(2, 4, "製作一覧発行№")
            xls.SetValue(4, 4, Subject.HeaderSubject.SeisakuichiranHakouNo)
            xls.SetValue(5, 4, Subject.HeaderSubject.SeisakuichiranHakouNoKai)

            'xls.MergeCells(6, 4, 6, 4, True)
            xls.SetAlignment(6, 4, 6, 4, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, True, True)
            xls.SetValue(6, 4, "発注有無")
            xls.SetValue(7, 4, Subject.HeaderSubject.HachuUmu)

            xls.SetAlignment(10, 4, 10, 4, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter, True, True)
            xls.SetValue(10, 4, "所属")
            xls.SetValue(11, 4, aLoginInfo.BukaRyakuName)

            xls.SetValue(12, 4, "担当者")
            xls.SetValue(13, 4, aLoginInfo.ShainName)

        End Sub

#End Region

#Region "連番"
        ''' <summary>
        ''' 連番の部分出力
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Public Sub setIncrement(ByVal xls As ShisakuExcel)
            xls.MergeCells(TagRenban, 10, TagRenban, 11, True)
            xls.SetAlignment(TagRenban, 10, TagRenban, 10, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignBottom, True, True)
            xls.SetValue(TagRenban, 10, "連番")
            Dim rowsIterator = BaseCarSubject.GetInputRowNos().GetEnumerator()
            While rowsIterator.MoveNext
                RowsCount = Math.Max(RowsCount, rowsIterator.Current + 1)
            End While
            If Not RowsCount = 0 Then
                Dim i As Integer
                For i = 0 To RowsCount - 1
                    xls.SetValue(1, dataStartRow + i, i + 1)
                    xls.SetAlignment(TagRenban, dataStartRow + i, TagRenban, dataStartRow + i, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
                Next
            End If
        End Sub

#End Region

#Region "ベース車"
        ''' <summary>
        ''' ベース車の部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setBaseCar(ByVal xls As ShisakuExcel)
            setBaseCarHead(xls)
            setBaseCarBody(xls)
        End Sub
        ''' <summary>
        ''' ベース車ヘーダの部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setBaseCarHead(ByVal xls As ShisakuExcel)

            With xls
                .MergeCells(TagBaseKaihatsuFugo, 9, TagSeisakuSyataiNo, 9, True)
                .SetAlignment(TagBaseKaihatsuFugo, 9, TagSeisakuSyataiNo, 9, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseKaihatsuFugo, 9, "ベース車情報")

                .MergeCells(TagShisakuSyubetu, 10, TagShisakuSyubetu, 11, True)
                .SetAlignment(TagShisakuSyubetu, 10, TagShisakuSyubetu, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagShisakuSyubetu, 10, "種別")

                .MergeCells(TagShisakuGousya, 10, TagShisakuGousya, 11, True)
                .SetAlignment(TagShisakuGousya, 10, TagShisakuGousya, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagShisakuGousya, 10, "号車")

                .MergeCells(TagBaseKaihatsuFugo, 10, TagBaseKaihatsuFugo, 11, True)
                .SetAlignment(TagBaseKaihatsuFugo, 10, TagBaseKaihatsuFugo, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseKaihatsuFugo, 10, "開発符号")

                .MergeCells(TagBaseShiyoujyouhouNo, 10, TagBaseShiyoujyouhouNo, 11, True)
                .SetAlignment(TagBaseShiyoujyouhouNo, 10, TagBaseShiyoujyouhouNo, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseShiyoujyouhouNo, 10, "仕様情報Ｎｏ")

                '参考情報を追加
                .SetValue(TagBaseSeisakuSyasyu, 10, "［参考情報］")
                .SetAlignment(TagBaseSeisakuSyasyu, 10, TagBaseSeisakuSyasyu, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .MergeCells(TagBaseSeisakuSyasyu, 10, TagBaseSeisakuTmHensokuki, 10, True)
                .SetAlignment(TagBaseSeisakuSyasyu, 11, TagBaseSeisakuSyasyu, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseSeisakuSyasyu, 11, "車型")
                .SetAlignment(TagBaseSeisakuGrade, 11, TagBaseSeisakuGrade, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseSeisakuGrade, 11, "グレード")
                .SetAlignment(TagBaseSeisakuShimuke, 11, TagBaseSeisakuShimuke, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseSeisakuShimuke, 11, "仕向地・仕向け")
                .SetAlignment(TagBaseSeisakuHandoru, 11, TagBaseSeisakuHandoru, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseSeisakuHandoru, 11, "仕向地・ハンドル")
                .SetAlignment(TagBaseSeisakuEgHaikiryou, 11, TagBaseSeisakuEgHaikiryou, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseSeisakuEgHaikiryou, 11, "E/G仕様・排気量")
                .SetAlignment(TagBaseSeisakuEgKatashiki, 11, TagBaseSeisakuEgKatashiki, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseSeisakuEgKatashiki, 11, "E/G仕様・型式")
                .SetAlignment(TagBaseSeisakuEgKakyuuki, 11, TagBaseSeisakuEgKakyuuki, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseSeisakuEgKakyuuki, 11, "E/G仕様・過給器")
                .SetAlignment(TagBaseSeisakuTmKudou, 11, TagBaseSeisakuTmKudou, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseSeisakuTmKudou, 11, "E/G仕様・駆動方式")
                .SetAlignment(TagBaseSeisakuTmHensokuki, 11, TagBaseSeisakuTmHensokuki, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseSeisakuTmHensokuki, 11, "E/G仕様・変速機")

                .MergeCells(TagBaseAppliedNo, 10, TagBaseAppliedNo, 11, True)
                .SetAlignment(TagBaseAppliedNo, 10, TagBaseAppliedNo, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseAppliedNo, 10, "アプライドNo")

                .MergeCells(TagBaseKatashiki, 10, TagBaseKatashiki, 11, True)
                .SetAlignment(TagBaseKatashiki, 10, TagBaseKatashiki, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseKatashiki, 10, "型式")

                .MergeCells(TagBaseShimuke, 10, TagBaseShimuke, 11, True)
                .SetAlignment(TagBaseShimuke, 10, TagBaseShimuke, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseShimuke, 10, "仕向")

                .MergeCells(TagBaseOp, 10, TagBaseOp, 11, True)
                .SetAlignment(TagBaseOp, 10, TagBaseOp, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseOp, 10, "ＯＰ")

                .MergeCells(TagBaseGaisousyoku, 10, TagBaseGaisousyoku, 11, True)
                .SetAlignment(TagBaseGaisousyoku, 10, TagBaseGaisousyoku, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseGaisousyoku, 10, "外装色")

                .MergeCells(TagBaseGaisousyokuName, 10, TagBaseGaisousyokuName, 11, True)
                .SetAlignment(TagBaseGaisousyoku, 10, TagBaseGaisousyokuName, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseGaisousyokuName, 10, "外装色名")

                .MergeCells(TagBaseNaisousyoku, 10, TagBaseNaisousyoku, 11, True)
                .SetAlignment(TagBaseNaisousyoku, 10, TagBaseNaisousyoku, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseNaisousyoku, 10, "内装色")

                .MergeCells(TagBaseNaisousyokuName, 10, TagBaseNaisousyokuName, 11, True)
                .SetAlignment(TagBaseNaisousyokuName, 10, TagBaseNaisousyokuName, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseNaisousyokuName, 10, "内装色名")

                .MergeCells(TagShisakuBaseEventCode, 10, TagShisakuBaseEventCode, 11, True)
                .SetAlignment(TagShisakuBaseEventCode, 10, TagShisakuBaseEventCode, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagShisakuBaseEventCode, 10, "イベントコード")

                .MergeCells(TagShisakuBaseGousya, 10, TagShisakuBaseGousya, 11, True)
                .SetAlignment(TagShisakuBaseGousya, 10, TagShisakuBaseGousya, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagShisakuBaseGousya, 10, "号車")

                .MergeCells(TagSeisakuSyataiNo, 10, TagSeisakuSyataiNo, 11, True)
                .SetAlignment(TagSeisakuSyataiNo, 10, TagSeisakuSyataiNo, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagSeisakuSyataiNo, 10, "車体№")

            End With

        End Sub
        ''' <summary>
        ''' ベース車データの部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setBaseCarBody(ByVal xls As ShisakuExcel)
            Dim getColorName As New EventEdit.Dao.EventEditBaseCarDaoImpl

            For Each rowNo As Integer In BaseCarSubject.GetInputRowNos()
                With xls
                    .SetValue(TagShisakuSyubetu, dataStartRow + rowNo, BaseCarSubject.ShisakuSyubetu(rowNo))
                    .SetValue(TagShisakuGousya, dataStartRow + rowNo, BaseCarSubject.ShisakuGousya(rowNo))
                    .SetValue(TagBaseKaihatsuFugo, dataStartRow + rowNo, BaseCarSubject.BaseKaihatsuFugo(rowNo))
                    .SetValue(TagBaseShiyoujyouhouNo, dataStartRow + rowNo, BaseCarSubject.BaseShiyoujyouhouNo(rowNo))

                    '参考情報
                    .SetValue(TagBaseSeisakuSyasyu, dataStartRow + rowNo, BaseCarSubject.SeisakuSyasyu(rowNo))
                    .SetValue(TagBaseSeisakuGrade, dataStartRow + rowNo, BaseCarSubject.SeisakuGrade(rowNo))
                    .SetValue(TagBaseSeisakuShimuke, dataStartRow + rowNo, BaseCarSubject.SeisakuShimuke(rowNo))
                    .SetValue(TagBaseSeisakuHandoru, dataStartRow + rowNo, BaseCarSubject.SeisakuHandoru(rowNo))
                    .SetValue(TagBaseSeisakuEgHaikiryou, dataStartRow + rowNo, BaseCarSubject.SeisakuEgHaikiryou(rowNo))
                    .SetValue(TagBaseSeisakuEgKatashiki, dataStartRow + rowNo, BaseCarSubject.SeisakuEgKatashiki(rowNo))
                    .SetValue(TagBaseSeisakuEgKakyuuki, dataStartRow + rowNo, BaseCarSubject.SeisakuEgKakyuuki(rowNo))
                    .SetValue(TagBaseSeisakuTmKudou, dataStartRow + rowNo, BaseCarSubject.SeisakuTmKudou(rowNo))
                    .SetValue(TagBaseSeisakuTmHensokuki, dataStartRow + rowNo, BaseCarSubject.SeisakuTmHensokuki(rowNo))

                    .SetValue(TagBaseAppliedNo, dataStartRow + rowNo, BaseCarSubject.BaseAppliedNo(rowNo))
                    .SetValue(TagBaseKatashiki, dataStartRow + rowNo, BaseCarSubject.BaseKatashiki(rowNo))
                    '国内を正しくExcel表示させる為の処理
                    If StringUtil.IsEmpty(BaseCarSubject.BaseShimuke(rowNo)) Then
                        Dim lvl As String
                        lvl = BaseCarSubject.BaseShimukeKokunai(rowNo)
                        .SetValue(TagBaseShimuke, dataStartRow + rowNo, lvl)
                    Else
                        .SetValue(TagBaseShimuke, dataStartRow + rowNo, BaseCarSubject.BaseShimuke(rowNo))
                    End If
                    .SetValue(TagBaseOp, dataStartRow + rowNo, BaseCarSubject.BaseOp(rowNo))
                    .SetValue(TagBaseGaisousyoku, dataStartRow + rowNo, BaseCarSubject.BaseGaisousyoku(rowNo))
                    '外装色名をエクセルに反映'
                    If Not StringUtil.IsEmpty(BaseCarSubject.BaseGaisousyoku(rowNo)) Then
                        Dim GaisoName = getColorName.FindGaisouColorName(BaseCarSubject.BaseGaisousyoku(rowNo))
                        If StringUtil.IsNotEmpty(GaisoName) Then
                            '後ろのスペースがやたら長いのでTrimしておく'
                            .SetValue(TagBaseGaisousyokuName, dataStartRow + rowNo, Trim(GaisoName.ColorName))
                        End If
                    End If
                    .SetValue(TagBaseNaisousyoku, dataStartRow + rowNo, BaseCarSubject.BaseNaisousyoku(rowNo))
                    '内装色名をエクセルに反映'
                    If Not StringUtil.IsEmpty(BaseCarSubject.BaseNaisousyoku(rowNo)) Then
                        Dim NaisoName = getColorName.FindNaisouColorName(BaseCarSubject.BaseNaisousyoku(rowNo))
                        If StringUtil.IsNotEmpty(NaisoName) Then
                            '後ろのスペースがやたら長いのでTrimしておく'
                            .SetValue(TagBaseNaisousyokuName, dataStartRow + rowNo, Trim(NaisoName.ColorName))
                        End If
                    End If
                    .SetValue(TagShisakuBaseEventCode, dataStartRow + rowNo, BaseCarSubject.ShisakuBaseEventCode(rowNo))
                    .SetValue(TagShisakuBaseGousya, dataStartRow + rowNo, BaseCarSubject.ShisakuBaseGousya(rowNo))
                    '製作一覧・車体№
                    .SetValue(TagSeisakuSyataiNo, dataStartRow + rowNo, BaseCarSubject.SeisakuSyataiNo(rowNo))
                End With
            Next

        End Sub
#End Region

#Region "設計展開ベース車"
        ''' <summary>
        ''' 設計展開ベース車の部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setReferenceCar(ByVal xls As ShisakuExcel)
            setReferenceCarHead(xls)
            setReferenceCarBody(xls)
        End Sub
        ''' <summary>
        ''' 設計展開ベース車ヘッダー部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setReferenceCarHead(ByVal xls As ShisakuExcel)
            With xls
                .MergeCells(TagBaseTenkaiKaihatsuFugo, 9, TagTenkaiShisakuBaseGousya, 9, True)
                .SetAlignment(TagBaseTenkaiKaihatsuFugo, 9, TagTenkaiShisakuBaseGousya, 9, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseTenkaiKaihatsuFugo, 9, "【　設計展開用　ベース車情報　】")

                .MergeCells(TagBaseTenkaiKaihatsuFugo, 10, TagBaseTenkaiKaihatsuFugo, 11, True)
                .SetAlignment(TagBaseTenkaiKaihatsuFugo, 10, TagBaseTenkaiKaihatsuFugo, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseTenkaiKaihatsuFugo, 10, "開発符号")

                .MergeCells(TagBaseTenkaiShiyoujyouhouNo, 10, TagBaseTenkaiShiyoujyouhouNo, 11, True)
                .SetAlignment(TagBaseTenkaiShiyoujyouhouNo, 10, TagBaseTenkaiShiyoujyouhouNo, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseTenkaiShiyoujyouhouNo, 10, "仕様情報№")

                .MergeCells(TagBaseTenkaiAppliedNo, 10, TagBaseTenkaiAppliedNo, 11, True)
                .SetAlignment(TagBaseTenkaiAppliedNo, 10, TagBaseTenkaiAppliedNo, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseTenkaiAppliedNo, 10, "アプライド№")

                .MergeCells(TagBaseTenkaiKatashiki, 10, TagBaseTenkaiKatashiki, 11, True)
                .SetAlignment(TagBaseTenkaiKatashiki, 10, TagBaseTenkaiKatashiki, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseTenkaiKatashiki, 10, "型式")

                .MergeCells(TagBaseTenkaiShimuke, 10, TagBaseTenkaiShimuke, 11, True)
                .SetAlignment(TagBaseTenkaiShimuke, 10, TagBaseTenkaiShimuke, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseTenkaiShimuke, 10, "仕向け")

                .MergeCells(TagBaseTenkaiOp, 10, TagBaseTenkaiOp, 11, True)
                .SetAlignment(TagBaseTenkaiOp, 10, TagBaseTenkaiOp, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseTenkaiOp, 10, "ＯＰ")

                .MergeCells(TagBaseTenkaiGaisousyoku, 10, TagBaseTenkaiGaisousyoku, 11, True)
                .SetAlignment(TagBaseTenkaiGaisousyoku, 10, TagBaseTenkaiGaisousyoku, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseTenkaiGaisousyoku, 10, "外装色")

                .MergeCells(TagBaseTenkaiGaisousyokuName, 10, TagBaseTenkaiGaisousyokuName, 11, True)
                .SetAlignment(TagBaseTenkaiGaisousyokuName, 10, TagBaseTenkaiGaisousyokuName, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseTenkaiGaisousyokuName, 10, "外装色名")

                .MergeCells(TagBaseTenkaiNaisousyoku, 10, TagBaseTenkaiNaisousyoku, 11, True)
                .SetAlignment(TagBaseTenkaiNaisousyoku, 10, TagBaseTenkaiNaisousyoku, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseTenkaiNaisousyoku, 10, "内装色")

                .MergeCells(TagBaseTenkaiNaisousyokuName, 10, TagBaseTenkaiNaisousyokuName, 11, True)
                .SetAlignment(TagBaseTenkaiNaisousyokuName, 10, TagBaseTenkaiNaisousyokuName, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagBaseTenkaiNaisousyokuName, 10, "内装色名")

                .MergeCells(TagTenkaiShisakuBaseEventCode, 10, TagTenkaiShisakuBaseEventCode, 11, True)
                .SetAlignment(TagTenkaiShisakuBaseEventCode, 10, TagTenkaiShisakuBaseEventCode, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagTenkaiShisakuBaseEventCode, 10, "イベントコード")

                .MergeCells(TagTenkaiShisakuBaseGousya, 10, TagTenkaiShisakuBaseGousya, 11, True)
                .SetAlignment(TagTenkaiShisakuBaseGousya, 10, TagTenkaiShisakuBaseGousya, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagTenkaiShisakuBaseGousya, 10, "号車")

            End With
        End Sub
        ''' <summary>
        ''' 設計展開ベース車データの部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setReferenceCarBody(ByVal xls As ShisakuExcel)
            Dim getColorName As New EventEdit.Dao.EventEditBaseCarDaoImpl

            For Each rowNo As Integer In BaseTenkaiCarSubject.GetInputRowNos()
                With xls
                    .SetValue(TagBaseTenkaiKaihatsuFugo, dataStartRow + rowNo, BaseTenkaiCarSubject.BaseKaihatsuFugo(rowNo))
                    .SetValue(TagBaseTenkaiShiyoujyouhouNo, dataStartRow + rowNo, BaseTenkaiCarSubject.BaseShiyoujyouhouNo(rowNo))
                    .SetValue(TagBaseTenkaiAppliedNo, dataStartRow + rowNo, BaseTenkaiCarSubject.BaseAppliedNo(rowNo))
                    .SetValue(TagBaseTenkaiKatashiki, dataStartRow + rowNo, BaseTenkaiCarSubject.BaseKatashiki(rowNo))
                    '国内を正しくExcel表示させる為の処理
                    If StringUtil.IsEmpty(BaseTenkaiCarSubject.BaseShimuke(rowNo)) Then
                        Dim lvl As String
                        lvl = BaseCarSubject.BaseShimukeKokunai(rowNo)
                        .SetValue(TagBaseTenkaiShimuke, dataStartRow + rowNo, lvl)
                    Else
                        .SetValue(TagBaseTenkaiShimuke, dataStartRow + rowNo, BaseTenkaiCarSubject.BaseShimuke(rowNo))
                    End If
                    .SetValue(TagBaseTenkaiOp, dataStartRow + rowNo, BaseTenkaiCarSubject.BaseOp(rowNo))
                    .SetValue(TagBaseTenkaiGaisousyoku, dataStartRow + rowNo, BaseTenkaiCarSubject.BaseGaisousyoku(rowNo))
                    '外装色名をエクセルに反映'
                    If Not StringUtil.IsEmpty(BaseTenkaiCarSubject.BaseGaisousyoku(rowNo)) Then
                        Dim GaisoName = getColorName.FindGaisouColorName(BaseTenkaiCarSubject.BaseGaisousyoku(rowNo))
                        If StringUtil.IsNotEmpty(GaisoName) Then
                            '後ろのスペースがやたら長いのでTrimしておく'
                            .SetValue(TagBaseGaisousyokuName, dataStartRow + rowNo, Trim(GaisoName.ColorName))
                        End If
                    End If
                    .SetValue(TagBaseTenkaiNaisousyoku, dataStartRow + rowNo, BaseTenkaiCarSubject.BaseNaisousyoku(rowNo))
                    '内装色名をエクセルに反映'
                    If Not StringUtil.IsEmpty(BaseTenkaiCarSubject.BaseNaisousyoku(rowNo)) Then
                        Dim NaisoName = getColorName.FindNaisouColorName(BaseTenkaiCarSubject.BaseNaisousyoku(rowNo))
                        If StringUtil.IsNotEmpty(NaisoName) Then
                            '後ろのスペースがやたら長いのでTrimしておく'
                            .SetValue(TagBaseNaisousyokuName, dataStartRow + rowNo, Trim(NaisoName.ColorName))
                        End If
                    End If
                    .SetValue(TagTenkaiShisakuBaseEventCode, dataStartRow + rowNo, BaseTenkaiCarSubject.ShisakuBaseEventCode(rowNo))
                    .SetValue(TagTenkaiShisakuBaseGousya, dataStartRow + rowNo, BaseTenkaiCarSubject.ShisakuBaseGousya(rowNo))
                End With
            Next
        End Sub

#End Region

#Region "完成車"
        ''' <summary>
        ''' 完成車の部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setCompleteCar(ByVal xls As ShisakuExcel)
            setCompleteCarHead(xls)
            setCompleteCarBody(xls)
        End Sub
        ''' <summary>
        ''' 完成車ヘーダの部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setCompleteCarHead(ByVal xls As ShisakuExcel)
            With xls
                .MergeCells(TagCompleteCarShisakuSyagata, 9, TagCompleteCarShisakuMemo, 9, True)
                .SetAlignment(TagCompleteCarShisakuSyagata, 9, TagCompleteCarShisakuMemo, 9, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuSyagata, 9, "完成車情報")

                .MergeCells(TagCompleteCarShisakuSyagata, 10, TagCompleteCarShisakuSyagata, 11, True)
                .SetAlignment(TagCompleteCarShisakuSyagata, 10, TagCompleteCarShisakuSyagata, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuSyagata, 10, "車型")

                .MergeCells(TagCompleteCarShisakuGrade, 10, TagCompleteCarShisakuGrade, 11, True)
                .SetAlignment(TagCompleteCarShisakuGrade, 10, TagCompleteCarShisakuGrade, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuGrade, 10, "グレード")

                .SetValue(TagCompleteCarShisakuShimukechiShimuke, 10, "仕向地")
                .SetAlignment(TagCompleteCarShisakuShimukechiShimuke, 10, TagCompleteCarShisakuShimukechiShimuke, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetAlignment(TagCompleteCarShisakuShimukechiShimuke, 11, TagCompleteCarShisakuShimukechiShimuke, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuShimukechiShimuke, 11, "仕向け")
                .SetAlignment(TagCompleteCarShisakuHandoru, 11, TagCompleteCarShisakuHandoru, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuHandoru, 11, "ハンドル")

                .SetValue(TagCompleteCarShisakuEgKatashiki, 10, "E/G")
                .SetAlignment(TagCompleteCarShisakuEgKatashiki, 10, TagCompleteCarShisakuEgKatashiki, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuEgKatashiki, 11, "型式")
                .SetAlignment(TagCompleteCarShisakuEgKatashiki, 11, TagCompleteCarShisakuEgKatashiki, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)

                .SetValue(TagCompleteCarShisakuEgHaikiryou, 11, "排気量")
                .SetAlignment(TagCompleteCarShisakuEgHaikiryou, 11, TagCompleteCarShisakuEgHaikiryou, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)

                .SetValue(TagCompleteCarShisakuEgSystem, 11, "システム")
                .SetAlignment(TagCompleteCarShisakuEgSystem, 11, TagCompleteCarShisakuEgSystem, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)

                .SetValue(TagCompleteCarShisakuEgKakyuuki, 11, "過給機")
                .SetAlignment(TagCompleteCarShisakuEgKakyuuki, 11, TagCompleteCarShisakuEgKakyuuki, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)

                .SetValue(TagCompleteCarShisakuEgMemo1, 11, strEgMemo1)
                .SetAlignment(TagCompleteCarShisakuEgMemo1, 11, TagCompleteCarShisakuEgMemo1, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuEgMemo2, 11, strEgMemo2)
                .SetAlignment(TagCompleteCarShisakuEgMemo2, 11, TagCompleteCarShisakuEgMemo2, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)

                .SetValue(TagCompleteCarShisakuTmKudou, 10, "T/M")
                .SetAlignment(TagCompleteCarShisakuTmKudou, 10, TagCompleteCarShisakuTmKudou, 10, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuTmKudou, 11, "駆動")
                .SetAlignment(TagCompleteCarShisakuTmKudou, 11, TagCompleteCarShisakuTmKudou, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)

                .SetValue(TagCompleteCarShisakuTmHensokuki, 11, "変速機")
                .SetAlignment(TagCompleteCarShisakuTmHensokuki, 11, TagCompleteCarShisakuTmHensokuki, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)

                .SetValue(TagCompleteCarShisakuTmFukuHensokuki, 11, "副変速")
                .SetAlignment(TagCompleteCarShisakuTmFukuHensokuki, 11, TagCompleteCarShisakuTmFukuHensokuki, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)

                .SetValue(TagCompleteCarShisakuTmMemo1, 11, strTmMemo1)
                .SetAlignment(TagCompleteCarShisakuTmMemo1, 11, TagCompleteCarShisakuTmMemo1, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuTmMemo2, 11, strTmMemo2)
                .SetAlignment(TagCompleteCarShisakuTmMemo2, 11, TagCompleteCarShisakuTmMemo2, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)

                .MergeCells(TagCompleteCarShisakuKatashiki, 10, TagCompleteCarShisakuKatashiki, 11, True)
                .SetAlignment(TagCompleteCarShisakuKatashiki, 10, TagCompleteCarShisakuKatashiki, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuKatashiki, 10, "型式")

                .MergeCells(TagCompleteCarShisakuShimuke, 10, TagCompleteCarShisakuShimuke, 11, True)
                .SetAlignment(TagCompleteCarShisakuShimuke, 10, TagCompleteCarShisakuShimuke, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuShimuke, 10, "仕向")

                .MergeCells(TagCompleteCarShisakuOp, 10, TagCompleteCarShisakuOp, 11, True)
                .SetAlignment(TagCompleteCarShisakuOp, 10, TagCompleteCarShisakuOp, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuOp, 10, "ＯＰ")

                .MergeCells(TagCompleteCarShisakuGaisousyoku, 10, TagCompleteCarShisakuGaisousyoku, 11, True)
                .SetAlignment(TagCompleteCarShisakuGaisousyoku, 10, TagCompleteCarShisakuGaisousyoku, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuGaisousyoku, 10, "外装色")
                .MergeCells(TagCompleteCarShisakuGaisousyokuName, 10, TagCompleteCarShisakuGaisousyokuName, 11, True)
                .SetAlignment(TagCompleteCarShisakuGaisousyokuName, 10, TagCompleteCarShisakuGaisousyokuName, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuGaisousyokuName, 10, "外装色名")

                .MergeCells(TagCompleteCarShisakuNaisousyoku, 10, TagCompleteCarShisakuNaisousyoku, 11, True)
                .SetAlignment(TagCompleteCarShisakuNaisousyoku, 10, TagCompleteCarShisakuNaisousyoku, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuNaisousyoku, 10, "内装色")
                .MergeCells(TagCompleteCarShisakuNaisousyokuName, 10, TagCompleteCarShisakuNaisousyokuName, 11, True)
                .SetAlignment(TagCompleteCarShisakuNaisousyokuName, 10, TagCompleteCarShisakuNaisousyokuName, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuNaisousyokuName, 10, "内装色名")

                .MergeCells(TagCompleteCarShisakuSyadaiNo, 10, TagCompleteCarShisakuSyadaiNo, 11, True)
                .SetAlignment(TagCompleteCarShisakuSyadaiNo, 10, TagCompleteCarShisakuSyadaiNo, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuSyadaiNo, 10, "車台No.")

                .MergeCells(TagCompleteCarShisakuShiyouMokuteki, 10, TagCompleteCarShisakuShiyouMokuteki, 11, True)
                .SetAlignment(TagCompleteCarShisakuShiyouMokuteki, 10, TagCompleteCarShisakuShiyouMokuteki, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuShiyouMokuteki, 10, "使用目的")

                .MergeCells(TagCompleteCarShisakuShikenMokuteki, 10, TagCompleteCarShisakuShikenMokuteki, 11, True)
                .SetValue(TagCompleteCarShisakuShikenMokuteki, 10, "主要確認項目")
                .SetAlignment(TagCompleteCarShisakuShikenMokuteki, 10, TagCompleteCarShisakuShikenMokuteki, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)

                .MergeCells(TagCompleteCarShisakuSiyouBusyo, 10, TagCompleteCarShisakuSiyouBusyo, 11, True)
                .SetValue(TagCompleteCarShisakuSiyouBusyo, 10, "使用部署")
                .SetAlignment(TagCompleteCarShisakuSiyouBusyo, 10, TagCompleteCarShisakuSiyouBusyo, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)

                .MergeCells(TagCompleteCarShisakuGroup, 10, TagCompleteCarShisakuGroup, 11, True)
                .SetAlignment(TagCompleteCarShisakuGroup, 10, TagCompleteCarShisakuGroup, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuGroup, 10, "グループ")

                .MergeCells(TagCompleteCarShisakuSeisakuJunjyo, 10, TagCompleteCarShisakuSeisakuJunjyo, 11, True)
                .SetAlignment(TagCompleteCarShisakuSeisakuJunjyo, 10, TagCompleteCarShisakuSeisakuJunjyo, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuSeisakuJunjyo, 10, "製作・製作順序")

                .MergeCells(TagCompleteCarShisakuKanseibi, 10, TagCompleteCarShisakuKanseibi, 11, True)
                .SetAlignment(TagCompleteCarShisakuKanseibi, 10, TagCompleteCarShisakuKanseibi, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuKanseibi, 10, "完成日")

                .MergeCells(TagCompleteCarShisakuKoushiNo, 10, TagCompleteCarShisakuKoushiNo, 11, True)
                .SetAlignment(TagCompleteCarShisakuKoushiNo, 10, TagCompleteCarShisakuKoushiNo, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuKoushiNo, 10, "工指No.")

                .MergeCells(TagCompleteCarShisakuSeisakuHouhouKbn, 10, TagCompleteCarShisakuSeisakuHouhouKbn, 11, True)
                .SetAlignment(TagCompleteCarShisakuSeisakuHouhouKbn, 10, TagCompleteCarShisakuSeisakuHouhouKbn, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuSeisakuHouhouKbn, 10, "製作方法区分")

                .MergeCells(TagCompleteCarShisakuSeisakuHouhou, 10, TagCompleteCarShisakuSeisakuHouhou, 11, True)
                .SetAlignment(TagCompleteCarShisakuSeisakuHouhou, 10, TagCompleteCarShisakuSeisakuHouhou, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuSeisakuHouhou, 10, "製作方法")

                .MergeCells(TagCompleteCarShisakuMemo, 10, TagCompleteCarShisakuMemo, 11, True)
                .SetAlignment(TagCompleteCarShisakuMemo, 10, TagCompleteCarShisakuMemo, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(TagCompleteCarShisakuMemo, 10, "メモ欄")

            End With
        End Sub
        ''' <summary>
        ''' 完成車データの部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setCompleteCarBody(ByVal xls As ShisakuExcel)
            For Each rowNo As Integer In CompleteCarSubject.GetInputRowNos()
                With xls
                    '車型'
                    .SetValue(TagCompleteCarShisakuSyagata, dataStartRow + rowNo, CompleteCarSubject.ShisakuSyagata(rowNo))
                    'グレード'
                    .SetValue(TagCompleteCarShisakuGrade, dataStartRow + rowNo, CompleteCarSubject.ShisakuGrade(rowNo))
                    '仕向地・仕向け'
                    .SetValue(TagCompleteCarShisakuShimukechiShimuke, dataStartRow + rowNo, CompleteCarSubject.ShisakuShimukechiShimuke(rowNo))
                    '仕向地・ハンドル'
                    .SetValue(TagCompleteCarShisakuHandoru, dataStartRow + rowNo, CompleteCarSubject.ShisakuHandoru(rowNo))
                    'EG型式'
                    .SetValue(TagCompleteCarShisakuEgKatashiki, dataStartRow + rowNo, CompleteCarSubject.ShisakuEgKatashiki(rowNo))
                    'EG排気量'
                    .SetValue(TagCompleteCarShisakuEgHaikiryou, dataStartRow + rowNo, CompleteCarSubject.ShisakuEgHaikiryou(rowNo))
                    'EGシステム'
                    .SetValue(TagCompleteCarShisakuEgSystem, dataStartRow + rowNo, CompleteCarSubject.ShisakuEgSystem(rowNo))
                    'EG過給機'
                    .SetValue(TagCompleteCarShisakuEgKakyuuki, dataStartRow + rowNo, CompleteCarSubject.ShisakuEgKakyuuki(rowNo))
                    'EGメモ１'
                    .SetValue(TagCompleteCarShisakuEgMemo1, dataStartRow + rowNo, CompleteCarSubject.ShisakuEgMemo1(rowNo))
                    'EGメモ２'
                    .SetValue(TagCompleteCarShisakuEgMemo2, dataStartRow + rowNo, CompleteCarSubject.ShisakuEgMemo2(rowNo))
                    'TM駆動'
                    .SetValue(TagCompleteCarShisakuTmKudou, dataStartRow + rowNo, CompleteCarSubject.ShisakuTmKudou(rowNo))
                    'TM変速機'
                    .SetValue(TagCompleteCarShisakuTmHensokuki, dataStartRow + rowNo, CompleteCarSubject.ShisakuTmHensokuki(rowNo))
                    'TM副変速機'
                    .SetValue(TagCompleteCarShisakuTmFukuHensokuki, dataStartRow + rowNo, CompleteCarSubject.ShisakuTmFukuHensokuki(rowNo))
                    'TMメモ１'
                    .SetValue(TagCompleteCarShisakuTmMemo1, dataStartRow + rowNo, CompleteCarSubject.ShisakuTmMemo1(rowNo))
                    'TMメモ２'
                    .SetValue(TagCompleteCarShisakuTmMemo2, dataStartRow + rowNo, CompleteCarSubject.ShisakuTmMemo2(rowNo))
                    '型式'
                    .SetValue(TagCompleteCarShisakuKatashiki, dataStartRow + rowNo, CompleteCarSubject.ShisakuKatashiki(rowNo))
                    '仕向'
                    .SetValue(TagCompleteCarShisakuShimuke, dataStartRow + rowNo, CompleteCarSubject.ShisakuShimuke(rowNo))
                    'OP'
                    .SetValue(TagCompleteCarShisakuOp, dataStartRow + rowNo, CompleteCarSubject.ShisakuOp(rowNo))
                    '外装色'
                    .SetValue(TagCompleteCarShisakuGaisousyoku, dataStartRow + rowNo, CompleteCarSubject.ShisakuGaisousyoku(rowNo))
                    '外装色名'
                    .SetValue(TagCompleteCarShisakuGaisousyokuName, dataStartRow + rowNo, CompleteCarSubject.ShisakuGaisousyokuName(rowNo))
                    '内装色'
                    .SetValue(TagCompleteCarShisakuNaisousyoku, dataStartRow + rowNo, CompleteCarSubject.ShisakuNaisousyoku(rowNo))
                    '内装色名'
                    .SetValue(TagCompleteCarShisakuNaisousyokuName, dataStartRow + rowNo, CompleteCarSubject.ShisakuNaisousyokuName(rowNo))
                    '車台No'
                    .SetValue(TagCompleteCarShisakuSyadaiNo, dataStartRow + rowNo, CompleteCarSubject.ShisakuSyadaiNo(rowNo))
                    '使用目的'
                    .SetValue(TagCompleteCarShisakuShiyouMokuteki, dataStartRow + rowNo, CompleteCarSubject.ShisakuShiyouMokuteki(rowNo))
                    '試作試験目的（主要確認項目）'
                    .SetValue(TagCompleteCarShisakuShikenMokuteki, dataStartRow + rowNo, CompleteCarSubject.ShisakuShikenMokuteki(rowNo))
                    '試作使用部署'
                    .SetValue(TagCompleteCarShisakuSiyouBusyo, dataStartRow + rowNo, CompleteCarSubject.ShisakuSiyouBusyo(rowNo))
                    'グループ'
                    .SetValue(TagCompleteCarShisakuGroup, dataStartRow + rowNo, CompleteCarSubject.ShisakuGroup(rowNo))
                    '製作順序'
                    .SetValue(TagCompleteCarShisakuSeisakuJunjyo, dataStartRow + rowNo, CompleteCarSubject.ShisakuSeisakuJunjyo(rowNo))
                    '完成日があれば'
                    If Not CompleteCarSubject.ShisakuKanseibi(rowNo) Is Nothing Then
                        '完成日'
                        .SetValue(TagCompleteCarShisakuKanseibi, dataStartRow + rowNo, ShisakuComFunc.moji8Convert2Date(CompleteCarSubject.ShisakuKanseibi(rowNo)))
                    End If
                    '工指No'
                    .SetValue(TagCompleteCarShisakuKoushiNo, dataStartRow + rowNo, CompleteCarSubject.ShisakuKoushiNo(rowNo))
                    '製作方法区分'
                    .SetValue(TagCompleteCarShisakuSeisakuHouhouKbn, dataStartRow + rowNo, CompleteCarSubject.ShisakuSeisakuHouhouKbn(rowNo))
                    '製作方法'
                    .SetValue(TagCompleteCarShisakuSeisakuHouhou, dataStartRow + rowNo, CompleteCarSubject.ShisakuSeisakuHouhou(rowNo))
                    'メモ欄'
                    .SetValue(TagCompleteCarShisakuMemo, dataStartRow + rowNo, CompleteCarSubject.ShisakuMemo(rowNo))

                End With
            Next
        End Sub

#End Region

#Region "基本装備"
        ''' <summary>
        ''' 基本装備の部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setBasicOption(ByVal xls As ShisakuExcel)
            setBaseOptionTitle(xls)
            setBasicOptionBody(xls)
            setBasicOptionHead(xls)
            setBasicOptionTitleAligmeng(xls)
        End Sub
        ''' <summary>
        ''' 基本装備タイトルの部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setBaseOptionTitle(ByVal xls As ShisakuExcel)
            Dim maxColumnNo = 0
            For Each columnNo As Integer In BasicOptionSubject.GetInputTitleNameColumnNos()
                maxColumnNo = Math.Max(maxColumnNo, columnNo)
                With xls
                    .SetValue(BasicOptionStartColumn + columnNo, 6, BasicOptionSubject.TitleNameDai(columnNo))
                    .SetValue(BasicOptionStartColumn + columnNo, 7, BasicOptionSubject.TitleNameChu(columnNo))
                    .SetValue(BasicOptionStartColumn + columnNo, 8, BasicOptionSubject.TitleName(columnNo))
                End With
            Next
            Me.SpecialOptionStartColumn = BasicOptionStartColumn + maxColumnNo + blankColumnsCount
            If Not (maxColumnNo = 0) Then
                Me.SpecialOptionStartColumn = Me.SpecialOptionStartColumn
            End If
        End Sub
        ''' <summary>
        ''' 基本装備データの部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setBasicOptionBody(ByVal xls As ShisakuExcel)
            Dim maxColumnNo = 0
            For Each rowNo As Integer In BasicOptionSubject.GetInputRowNos()
                For Each columnNo As Integer In BasicOptionSubject.GetInputColumnNos(rowNo)
                    maxColumnNo = Math.Max(maxColumnNo, columnNo)
                    With xls
                        .SetValue(BasicOptionStartColumn + columnNo, dataStartRow + rowNo, BasicOptionSubject.ShisakuTekiyou(rowNo, columnNo))
                    End With
                Next
            Next
            If Not (maxColumnNo = 0) Then
                Me.SpecialOptionStartColumn = Math.Max(SpecialOptionStartColumn, BasicOptionStartColumn + maxColumnNo + blankColumnsCount + 1)
            End If
        End Sub
        ''' <summary>
        ''' 基本装備ヘーダの部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setBasicOptionHead(ByVal xls As ShisakuExcel)
            With xls
                .MergeCells(BasicOptionStartColumn, 5, SpecialOptionStartColumn - 1, 5, True)
                .SetValue(BasicOptionStartColumn, 5, "基本装備仕様")
                .SetAlignment(BasicOptionStartColumn, 5, BasicOptionStartColumn, 5, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            End With
        End Sub

        ''' <summary>
        ''' 基本装備タイトルの部分セルの結合、Alignment、Orientation
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setBasicOptionTitleAligmeng(ByVal xls As ShisakuExcel)
            Dim basicOptionTitlesCount = xls.GetMergedCellsColumnCount(BasicOptionStartColumn, 5) + blankColumnsCount
            With xls
                .SetAlignment(BasicOptionStartColumn, 6, BasicOptionStartColumn + basicOptionTitlesCount, 6, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop)
                .SetOrientation(BasicOptionStartColumn, 6, BasicOptionStartColumn + basicOptionTitlesCount, 6, ShisakuExcel.XlOrientation.xlVertical)
                .SetAlignment(BasicOptionStartColumn, 7, BasicOptionStartColumn + basicOptionTitlesCount, 7, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop)
                .SetOrientation(BasicOptionStartColumn, 7, BasicOptionStartColumn + basicOptionTitlesCount, 7, ShisakuExcel.XlOrientation.xlVertical)
                .SetAlignment(BasicOptionStartColumn, 8, BasicOptionStartColumn + basicOptionTitlesCount, 8, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop)
                .SetOrientation(BasicOptionStartColumn, 8, BasicOptionStartColumn + basicOptionTitlesCount, 8, ShisakuExcel.XlOrientation.xlVertical)
            End With
        End Sub

#End Region

#Region "特別装備"
        ''' <summary>
        ''' 特別装備の部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks> 
        Public Sub setSpecialOption(ByVal xls As ShisakuExcel)
            setSpecialOptionTitle(xls)
            setSpecialOptionBody(xls)
            setSpecialOptionHead(xls)
            setSpecialOptionTitleAligmeng(xls)
            If m_isOverColumns Then
                xls.SetValue(SpecialOptionStartColumn, 1, "出力列が２５６列を超えた為、全てのデータを表示できませんでした。")
            End If
        End Sub
        ''' <summary>
        ''' 特別装備タイトルの部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>        
        Public Sub setSpecialOptionTitle(ByVal xls As ShisakuExcel)
            Me.ColumnsCount = SpecialOptionStartColumn
            Dim i As Integer = 0
            For Each columnNo As Integer In SpecialOptionSubject.GetInputTitleNameColumnNos()
                '2012/02/21 列が２５６を超えた場合エラーになるのを回避
                If SpecialOptionStartColumn + columnNo > m_ExcelMaxColumnNum Then
                    m_isOverColumns = True
                    Exit For
                End If
                With xls
                    .SetValue(SpecialOptionStartColumn + columnNo, 6, SpecialOptionSubject.TitleNameDai(columnNo))
                    .SetValue(SpecialOptionStartColumn + columnNo, 7, SpecialOptionSubject.TitleNameChu(columnNo))
                    .SetValue(SpecialOptionStartColumn + columnNo, 8, SpecialOptionSubject.TitleName(columnNo))
                    Me.ColumnsCount = Math.Max(Me.ColumnsCount, SpecialOptionStartColumn + columnNo + blankColumnsCount)
                End With
                i = i + 1
            Next
            If Me.ColumnsCount = SpecialOptionStartColumn Then
                Me.ColumnsCount = Me.ColumnsCount + blankColumnsCount - 1
            End If
            '2012/02/21 列が２５６を超えた場合エラーになるのを回避
            If Me.ColumnsCount > m_ExcelMaxColumnNum Then
                m_isOverColumns = True
                Me.ColumnsCount = m_ExcelMaxColumnNum
            End If
        End Sub
        ''' <summary>
        ''' 特別装備データの部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setSpecialOptionBody(ByVal xls As ShisakuExcel)
            For Each rowNo As Integer In SpecialOptionSubject.GetInputRowNos()
                For Each columnNo As Integer In SpecialOptionSubject.GetInputColumnNos(rowNo)
                    '2012/02/21 列が２５６を超えた場合エラーになるのを回避
                    If SpecialOptionStartColumn + columnNo > m_ExcelMaxColumnNum Then
                        m_isOverColumns = True
                        Exit For
                    End If
                    With xls
                        .SetValue(SpecialOptionStartColumn + columnNo, dataStartRow + rowNo, SpecialOptionSubject.ShisakuTekiyou(rowNo, columnNo))
                        Me.ColumnsCount = Math.Max(Me.ColumnsCount, SpecialOptionStartColumn + columnNo + blankColumnsCount)
                    End With
                Next
            Next
            If Me.ColumnsCount = SpecialOptionStartColumn Then
                'Me.ColumnsCount = Me.ColumnsCount + blankColumnsCount
                Me.ColumnsCount = Me.ColumnsCount + blankColumnsCount - 1
            End If
            '2012/02/21 列が２５６を超えた場合エラーになるのを回避
            If Me.ColumnsCount > m_ExcelMaxColumnNum Then
                Me.ColumnsCount = m_ExcelMaxColumnNum
                m_isOverColumns = True
            End If
        End Sub
        ''' <summary>
        ''' 特別装備ヘーダの部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setSpecialOptionHead(ByVal xls As ShisakuExcel)
            With xls
                .MergeCells(SpecialOptionStartColumn, 5, ColumnsCount, 5, True)
                .SetValue(SpecialOptionStartColumn, 5, "特別装備仕様")
                .SetAlignment(SpecialOptionStartColumn, 5, SpecialOptionStartColumn, 5, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, True, True)
            End With
        End Sub

        ''' <summary>
        ''' 特別装備タイトルの部分セルの結合、Alignment、Orientation
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setSpecialOptionTitleAligmeng(ByVal xls As ShisakuExcel)
            Dim specialOptionTitlesCount = xls.GetMergedCellsColumnCount(SpecialOptionStartColumn, 5) + blankColumnsCount
            With xls
                '2012/02/21 列が２５６を超えた場合エラーになるのを回避
                If SpecialOptionStartColumn + specialOptionTitlesCount > m_ExcelMaxColumnNum Then
                    m_isOverColumns = True
                    .SetAlignment(SpecialOptionStartColumn, 6, m_ExcelMaxColumnNum, 6, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop)
                    .SetOrientation(SpecialOptionStartColumn, 6, m_ExcelMaxColumnNum, 6, ShisakuExcel.XlOrientation.xlVertical)
                    .SetAlignment(SpecialOptionStartColumn, 7, m_ExcelMaxColumnNum, 7, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop)
                    .SetOrientation(SpecialOptionStartColumn, 7, m_ExcelMaxColumnNum, 7, ShisakuExcel.XlOrientation.xlVertical)
                    .SetAlignment(SpecialOptionStartColumn, 8, m_ExcelMaxColumnNum, 8, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop)
                    .SetOrientation(SpecialOptionStartColumn, 8, m_ExcelMaxColumnNum, 8, ShisakuExcel.XlOrientation.xlVertical)
                Else
                    .SetAlignment(SpecialOptionStartColumn, 6, SpecialOptionStartColumn + specialOptionTitlesCount, 6, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop)
                    .SetOrientation(SpecialOptionStartColumn, 6, SpecialOptionStartColumn + specialOptionTitlesCount, 6, ShisakuExcel.XlOrientation.xlVertical)
                    .SetAlignment(SpecialOptionStartColumn, 7, SpecialOptionStartColumn + specialOptionTitlesCount, 7, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop)
                    .SetOrientation(SpecialOptionStartColumn, 7, SpecialOptionStartColumn + specialOptionTitlesCount, 7, ShisakuExcel.XlOrientation.xlVertical)
                    .SetAlignment(SpecialOptionStartColumn, 8, SpecialOptionStartColumn + specialOptionTitlesCount, 8, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop)
                    .SetOrientation(SpecialOptionStartColumn, 8, SpecialOptionStartColumn + specialOptionTitlesCount, 8, ShisakuExcel.XlOrientation.xlVertical)
                End If
            End With
        End Sub
#End Region

#Region "設変監視情報"
        ''' <summary>
        ''' 設変監視情報
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setEbomKanshi(ByVal xls As ShisakuExcel)
            If m_isOverColumns Then
                Exit Sub
            ElseIf Me.ColumnsCount > (256 - 11) Then
                Exit Sub
            End If
            _EBomKanshiStartColumn = Me.ColumnsCount + 1
            setEbomKanshiHead(xls)
            setEbomKanshiBody(xls)

            Me.ColumnsCount = _EBomKanshiStartColumn + 11
        End Sub
        ''' <summary>
        ''' 設変監視情報ヘッダの部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setEbomKanshiHead(ByVal xls As ShisakuExcel)
            Dim col As Integer = _EBomKanshiStartColumn
            With xls
                .MergeCells(col, 9, col + 11, 9, True)
                .SetAlignment(col, 9, col + 12, 9, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignBottom)
                .SetValue(col, 9, "設変監視情報")

                .MergeCells(col, 10, col, 11, True)
                .SetAlignment(col, 10, col, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(col, 10, "種別")

                col += 1
                .MergeCells(col, 10, col, 11, True)
                .SetAlignment(col, 10, col, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(col, 10, "号車")

                col += 1
                .MergeCells(col, 10, col, 11, True)
                .SetAlignment(col, 10, col, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(col, 10, "開発符号")

                col += 1
                .MergeCells(col, 10, col, 11, True)
                .SetAlignment(col, 10, col, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(col, 10, "仕様情報Ｎｏ")


                col += 1
                .MergeCells(col, 10, col, 11, True)
                .SetAlignment(col, 10, col, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(col, 10, "アプライドNo")

                col += 1
                .MergeCells(col, 10, col, 11, True)
                .SetAlignment(col, 10, col, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(col, 10, "型式")

                col += 1
                .MergeCells(col, 10, col, 11, True)
                .SetAlignment(col, 10, col, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(col, 10, "仕向")

                col += 1
                .MergeCells(col, 10, col, 11, True)
                .SetAlignment(col, 10, col, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(col, 10, "ＯＰ")

                col += 1
                .MergeCells(col, 10, col, 11, True)
                .SetAlignment(col, 10, col, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(col, 10, "外装色")

                col += 1
                .MergeCells(col, 10, col, 11, True)
                .SetAlignment(col, 10, col, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(col, 10, "外装色名")

                col += 1
                .MergeCells(col, 10, col, 11, True)
                .SetAlignment(col, 10, col, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(col, 10, "内装色")

                col += 1
                .MergeCells(col, 10, col, 11, True)
                .SetAlignment(col, 10, col, 11, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
                .SetValue(col, 10, "内装色名")


            End With

        End Sub
        ''' <summary>
        ''' 設変監視情報データの部分出力
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setEbomKanshiBody(ByVal xls As ShisakuExcel)
            Dim getColorName As New EventEdit.Dao.EventEditBaseCarDaoImpl

            For Each rowNo As Integer In BaseCarSubject.GetInputRowNos()
                With xls
                    Dim col As Integer = _EBomKanshiStartColumn
                    .SetValue(col, dataStartRow + rowNo, EbomKanshiSubject.ShisakuSyubetu(rowNo))
                    col += 1
                    .SetValue(col, dataStartRow + rowNo, EbomKanshiSubject.ShisakuGousya(rowNo))
                    col += 1
                    .SetValue(col, dataStartRow + rowNo, EbomKanshiSubject.BaseKaihatsuFugo(rowNo))
                    col += 1
                    .SetValue(col, dataStartRow + rowNo, EbomKanshiSubject.BaseShiyoujyouhouNo(rowNo))

                    col += 1
                    .SetValue(col, dataStartRow + rowNo, EbomKanshiSubject.BaseAppliedNo(rowNo))
                    col += 1
                    .SetValue(col, dataStartRow + rowNo, EbomKanshiSubject.BaseKatashiki(rowNo))
                    '国内を正しくExcel表示させる為の処理
                    col += 1
                    If StringUtil.IsEmpty(EbomKanshiSubject.BaseShimuke(rowNo)) Then
                        Dim lvl As String
                        lvl = EbomKanshiSubject.BaseShimukeKokunai(rowNo)
                        .SetValue(col, dataStartRow + rowNo, lvl)
                    Else
                        .SetValue(col, dataStartRow + rowNo, EbomKanshiSubject.BaseShimuke(rowNo))
                    End If
                    col += 1
                    .SetValue(col, dataStartRow + rowNo, EbomKanshiSubject.BaseOp(rowNo))
                    col += 1
                    .SetValue(col, dataStartRow + rowNo, EbomKanshiSubject.BaseGaisousyoku(rowNo))
                    '外装色名をエクセルに反映'
                    col += 1
                    If Not StringUtil.IsEmpty(EbomKanshiSubject.BaseGaisousyoku(rowNo)) Then
                        Dim GaisoName = getColorName.FindGaisouColorName(EbomKanshiSubject.BaseGaisousyoku(rowNo))
                        If StringUtil.IsNotEmpty(GaisoName) Then
                            '後ろのスペースがやたら長いのでTrimしておく'
                            .SetValue(col, dataStartRow + rowNo, Trim(GaisoName.ColorName))
                        End If
                    End If
                    col += 1
                    .SetValue(col, dataStartRow + rowNo, EbomKanshiSubject.BaseNaisousyoku(rowNo))
                    '内装色名をエクセルに反映'
                    col += 1
                    If Not StringUtil.IsEmpty(EbomKanshiSubject.BaseNaisousyoku(rowNo)) Then
                        Dim NaisoName = getColorName.FindNaisouColorName(EbomKanshiSubject.BaseNaisousyoku(rowNo))
                        If StringUtil.IsNotEmpty(NaisoName) Then
                            '後ろのスペースがやたら長いのでTrimしておく'
                            .SetValue(col, dataStartRow + rowNo, Trim(NaisoName.ColorName))
                        End If
                    End If
                End With
            Next

        End Sub
#End Region

        ''' <summary>
        ''' 列のタグ設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ColumnTagRenban()
            Dim column As Integer = 1
            TagRenban = EzUtil.Increment(column)
            TagShisakuSyubetu = EzUtil.Increment(column)
            TagShisakuGousya = EzUtil.Increment(column)
            TagBaseKaihatsuFugo = EzUtil.Increment(column)
            TagBaseShiyoujyouhouNo = EzUtil.Increment(column)
            TagBaseSeisakuSyasyu = EzUtil.Increment(column)
            TagBaseSeisakuGrade = EzUtil.Increment(column)
            TagBaseSeisakuShimuke = EzUtil.Increment(column)
            TagBaseSeisakuHandoru = EzUtil.Increment(column)
            TagBaseSeisakuEgHaikiryou = EzUtil.Increment(column)
            TagBaseSeisakuEgKatashiki = EzUtil.Increment(column)
            TagBaseSeisakuEgKakyuuki = EzUtil.Increment(column)
            TagBaseSeisakuTmKudou = EzUtil.Increment(column)
            TagBaseSeisakuTmHensokuki = EzUtil.Increment(column)
            TagBaseAppliedNo = EzUtil.Increment(column)
            TagBaseKatashiki = EzUtil.Increment(column)
            TagBaseShimuke = EzUtil.Increment(column)
            TagBaseOp = EzUtil.Increment(column)
            TagBaseGaisousyoku = EzUtil.Increment(column)
            TagBaseGaisousyokuName = EzUtil.Increment(column)
            TagBaseNaisousyoku = EzUtil.Increment(column)
            TagBaseNaisousyokuName = EzUtil.Increment(column)
            TagShisakuBaseEventCode = EzUtil.Increment(column)
            TagShisakuBaseGousya = EzUtil.Increment(column)
            TagSeisakuSyataiNo = EzUtil.Increment(column)

            TagBaseTenkaiKaihatsuFugo = EzUtil.Increment(column)
            TagBaseTenkaiShiyoujyouhouNo = EzUtil.Increment(column)
            TagBaseTenkaiAppliedNo = EzUtil.Increment(column)
            TagBaseTenkaiKatashiki = EzUtil.Increment(column)
            TagBaseTenkaiShimuke = EzUtil.Increment(column)
            TagBaseTenkaiOp = EzUtil.Increment(column)
            TagBaseTenkaiGaisousyoku = EzUtil.Increment(column)
            TagBaseTenkaiGaisousyokuName = EzUtil.Increment(column)
            TagBaseTenkaiNaisousyoku = EzUtil.Increment(column)
            TagBaseTenkaiNaisousyokuName = EzUtil.Increment(column)
            TagTenkaiShisakuBaseEventCode = EzUtil.Increment(column)
            TagTenkaiShisakuBaseGousya = EzUtil.Increment(column)

            TagCompleteCarShisakuSyagata = EzUtil.Increment(column)
            TagCompleteCarShisakuGrade = EzUtil.Increment(column)
            TagCompleteCarShisakuShimukechiShimuke = EzUtil.Increment(column)
            TagCompleteCarShisakuHandoru = EzUtil.Increment(column)
            TagCompleteCarShisakuEgKatashiki = EzUtil.Increment(column)
            TagCompleteCarShisakuEgHaikiryou = EzUtil.Increment(column)
            TagCompleteCarShisakuEgSystem = EzUtil.Increment(column)
            TagCompleteCarShisakuEgKakyuuki = EzUtil.Increment(column)
            TagCompleteCarShisakuEgMemo1 = EzUtil.Increment(column)
            TagCompleteCarShisakuEgMemo2 = EzUtil.Increment(column)
            TagCompleteCarShisakuTmKudou = EzUtil.Increment(column)
            TagCompleteCarShisakuTmHensokuki = EzUtil.Increment(column)
            TagCompleteCarShisakuTmFukuHensokuki = EzUtil.Increment(column)
            TagCompleteCarShisakuTmMemo1 = EzUtil.Increment(column)
            TagCompleteCarShisakuTmMemo2 = EzUtil.Increment(column)
            TagCompleteCarShisakuKatashiki = EzUtil.Increment(column)
            TagCompleteCarShisakuShimuke = EzUtil.Increment(column)
            TagCompleteCarShisakuOp = EzUtil.Increment(column)
            TagCompleteCarShisakuGaisousyoku = EzUtil.Increment(column)
            TagCompleteCarShisakuGaisousyokuName = EzUtil.Increment(column)
            TagCompleteCarShisakuNaisousyoku = EzUtil.Increment(column)
            TagCompleteCarShisakuNaisousyokuName = EzUtil.Increment(column)
            TagCompleteCarShisakuSyadaiNo = EzUtil.Increment(column)
            TagCompleteCarShisakuShiyouMokuteki = EzUtil.Increment(column)
            TagCompleteCarShisakuShikenMokuteki = EzUtil.Increment(column)
            TagCompleteCarShisakuSiyouBusyo = EzUtil.Increment(column)
            TagCompleteCarShisakuGroup = EzUtil.Increment(column)
            TagCompleteCarShisakuSeisakuJunjyo = EzUtil.Increment(column)
            TagCompleteCarShisakuKanseibi = EzUtil.Increment(column)
            TagCompleteCarShisakuKoushiNo = EzUtil.Increment(column)
            TagCompleteCarShisakuSeisakuHouhouKbn = EzUtil.Increment(column)
            TagCompleteCarShisakuSeisakuHouhou = EzUtil.Increment(column)
            TagCompleteCarShisakuMemo = EzUtil.Increment(column)

            Me.BasicOptionStartColumn = EzUtil.Increment(column)
            Me.ColumnsCount = EzUtil.Increment(column)

        End Sub


#Region "Tag"

        ''' <summary>連番</summary>
        Private TagRenban As Integer

        ''' <summary>種別</summary>
        Private TagShisakuSyubetu As Integer
        ''' <summary>号車</summary>
        Private TagShisakuGousya As Integer

#Region "ベース車Column"
        ''' <summary>開発符号</summary>
        Private TagBaseKaihatsuFugo As Integer
        ''' <summary>仕様情報NO</summary>
        Private TagBaseShiyoujyouhouNo As Integer

        ''' <summary>製作一覧_車型</summary>
        Private TagBaseSeisakuSyasyu As Integer
        ''' <summary>製作一覧_グレード</summary>
        Private TagBaseSeisakuGrade As Integer
        ''' <summary>製作一覧_仕向地・仕向け</summary>
        Private TagBaseSeisakuShimuke As Integer
        ''' <summary>製作一覧_仕向地・ハンドル</summary>
        Private TagBaseSeisakuHandoru As Integer
        ''' <summary>製作一覧_E/G仕様・排気量</summary>
        Private TagBaseSeisakuEgHaikiryou As Integer
        ''' <summary>製作一覧_E/G仕様・型式</summary>
        Private TagBaseSeisakuEgKatashiki As Integer
        ''' <summary>製作一覧_E/G仕様・過給器</summary>
        Private TagBaseSeisakuEgKakyuuki As Integer
        ''' <summary>製作一覧_T/M仕様・駆動方式</summary>
        Private TagBaseSeisakuTmKudou As Integer
        ''' <summary>製作一覧_T/M仕様・変速機</summary>
        Private TagBaseSeisakuTmHensokuki As Integer

        ''' <summary>アブライドNO</summary>
        Private TagBaseAppliedNo As Integer
        ''' <summary>型式</summary>
        Private TagBaseKatashiki As Integer
        ''' <summary>仕向</summary>
        Private TagBaseShimuke As Integer
        ''' <summary>OP</summary>
        Private TagBaseOp As Integer
        ''' <summary>外装色</summary>
        Private TagBaseGaisousyoku As Integer
        ''' <summary>外装色名</summary>
        Private TagBaseGaisousyokuName As Integer
        ''' <summary>内装色</summary>
        Private TagBaseNaisousyoku As Integer
        ''' <summary>外装色名</summary>
        Private TagBaseNaisousyokuName As Integer
        ''' <summary>イベントコード</summary>
        Private TagShisakuBaseEventCode As Integer
        ''' <summary>号車</summary>
        Private TagShisakuBaseGousya As Integer

        ''' <summary>製作一覧_車体№</summary>
        Private TagSeisakuSyataiNo As Integer

#End Region

#Region "設計展開ベース車Column"
        ''' <summary>開発符号</summary>
        Private TagBaseTenkaiKaihatsuFugo As Integer
        ''' <summary>仕様情報NO</summary>
        Private TagBaseTenkaiShiyoujyouhouNo As Integer
        ''' <summary>アブライドNO</summary>
        Private TagBaseTenkaiAppliedNo As Integer
        ''' <summary>型式</summary>
        Private TagBaseTenkaiKatashiki As Integer
        ''' <summary>仕向</summary>
        Private TagBaseTenkaiShimuke As Integer
        ''' <summary>OP</summary>
        Private TagBaseTenkaiOp As Integer
        ''' <summary>外装色</summary>
        Private TagBaseTenkaiGaisousyoku As Integer
        ''' <summary>外装色名</summary>
        Private TagBaseTenkaiGaisousyokuName As Integer
        ''' <summary>内装色</summary>
        Private TagBaseTenkaiNaisousyoku As Integer
        ''' <summary>内装色名</summary>
        Private TagBaseTenkaiNaisousyokuName As Integer
        ''' <summary>イベントコード</summary>
        Private TagTenkaiShisakuBaseEventCode As Integer
        ''' <summary>号車</summary>
        Private TagTenkaiShisakuBaseGousya As Integer

#End Region

#Region "完成車"
        ''' <summary>車型</summary>
        Private TagCompleteCarShisakuSyagata As Integer
        ''' <summary>グレード</summary>
        Private TagCompleteCarShisakuGrade As Integer
        ''' <summary>仕向地・仕向け</summary>
        Private TagCompleteCarShisakuShimukechiShimuke As Integer
        ''' <summary>ハンドル</summary>
        Private TagCompleteCarShisakuHandoru As Integer
        ''' <summary>E/G型式</summary>
        Private TagCompleteCarShisakuEgKatashiki As Integer
        ''' <summary>排気量</summary>
        Private TagCompleteCarShisakuEgHaikiryou As Integer
        ''' <summary>システム</summary>
        Private TagCompleteCarShisakuEgSystem As Integer
        ''' <summary>過給機</summary>
        Private TagCompleteCarShisakuEgKakyuuki As Integer
        ''' <summary>試作E/Gメモ１</summary>
        Private TagCompleteCarShisakuEgMemo1 As Integer
        ''' <summary>試作E/Gメモ２</summary>
        Private TagCompleteCarShisakuEgMemo2 As Integer
        ''' <summary>駆動</summary>
        Private TagCompleteCarShisakuTmKudou As Integer
        ''' <summary>変速機</summary>
        Private TagCompleteCarShisakuTmHensokuki As Integer
        ''' <summary>副変速</summary>
        Private TagCompleteCarShisakuTmFukuHensokuki As Integer
        ''' <summary>試作T/Mメモ１</summary>
        Private TagCompleteCarShisakuTmMemo1 As Integer
        ''' <summary>試作T/Mメモ２</summary>
        Private TagCompleteCarShisakuTmMemo2 As Integer
        ''' <summary>型式</summary>
        Private TagCompleteCarShisakuKatashiki As Integer
        ''' <summary>仕向</summary>
        Private TagCompleteCarShisakuShimuke As Integer
        ''' <summary>ＯＰ</summary>
        Private TagCompleteCarShisakuOp As Integer
        ''' <summary>外装色</summary>
        Private TagCompleteCarShisakuGaisousyoku As Integer
        ''' <summary>外装色名</summary>
        Private TagCompleteCarShisakuGaisousyokuName As Integer
        ''' <summary>内装色</summary>
        Private TagCompleteCarShisakuNaisousyoku As Integer
        ''' <summary>内装色名</summary>
        Private TagCompleteCarShisakuNaisousyokuName As Integer
        ''' <summary>車台No.</summary>
        Private TagCompleteCarShisakuSyadaiNo As Integer
        ''' <summary>使用目的</summary>
        Private TagCompleteCarShisakuShiyouMokuteki As Integer
        ''' <summary>試作試験目的（主要確認項目）</summary>
        Private TagCompleteCarShisakuShikenMokuteki As Integer
        ''' <summary>使用部署</summary>
        Private TagCompleteCarShisakuSiyouBusyo As Integer
        ''' <summary>グループ</summary>
        Private TagCompleteCarShisakuGroup As Integer
        ''' <summary>製作・製作順序</summary>
        Private TagCompleteCarShisakuSeisakuJunjyo As Integer
        ''' <summary>完成日</summary>
        Private TagCompleteCarShisakuKanseibi As Integer
        ''' <summary>工指No.</summary>
        Private TagCompleteCarShisakuKoushiNo As Integer
        ''' <summary>製作方法区分</summary>
        Private TagCompleteCarShisakuSeisakuHouhouKbn As Integer
        ''' <summary>製作方法</summary>
        Private TagCompleteCarShisakuSeisakuHouhou As Integer
        ''' <summary>メモ欄</summary>
        Private TagCompleteCarShisakuMemo As Integer

#End Region

#End Region

#Region "罫線"
        ''' <summary>
        ''' 罫線の設定
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setKeisen(ByVal xls As ShisakuExcel)
            With xls
                .SetLine(1, 4, ColumnsCount, 4, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                .SetLine(BasicOptionStartColumn, 5, ColumnsCount, 5, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                .SetLine(1, 9, ColumnsCount, 9, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                .SetLine(1, 11, ColumnsCount, 11, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                .SetLine(2, 1, 2, dataStartRow - 1 + RowsCount, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                .SetLine(4, 5, 4, dataStartRow - 1 + RowsCount, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
                .SetLine(TagBaseTenkaiKaihatsuFugo, 5, TagBaseTenkaiKaihatsuFugo, dataStartRow - 1 + RowsCount, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
                .SetLine(TagCompleteCarShisakuSyagata, 5, TagCompleteCarShisakuSyagata, dataStartRow - 1 + RowsCount, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
                .SetLine(BasicOptionStartColumn, 5, BasicOptionStartColumn, dataStartRow - 1 + RowsCount, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
                .SetLine(SpecialOptionStartColumn, 5, SpecialOptionStartColumn, dataStartRow - 1 + RowsCount, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
                If ColumnsCount < 256 Then
                    .SetLine(_EBomKanshiStartColumn, 5, _EBomKanshiStartColumn, dataStartRow - 1 + RowsCount, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
                    .SetLine(ColumnsCount, 5, ColumnsCount, dataStartRow - 1 + RowsCount, XlBordersIndex.xlEdgeRight, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
                End If

                '.SetLine(1, 4, ColumnsCount, 4, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                '.SetLine(BasicOptionStartColumn, 5, ColumnsCount, 5, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                '.SetLine(1, 7, BasicOptionStartColumn - 1, 7, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                '.SetLine(1, 9, ColumnsCount, 9, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                '.SetLine(2, 1, 2, dataStartRow - 1 + RowsCount, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                '.SetLine(4, 5, 4, dataStartRow - 1 + RowsCount, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
                '.SetLine(14, 5, 14, dataStartRow - 1 + RowsCount, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
                '.SetLine(35, 5, 35, dataStartRow - 1 + RowsCount, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
                '.SetLine(BasicOptionStartColumn, 5, BasicOptionStartColumn, dataStartRow - 1 + RowsCount, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
                '.SetLine(SpecialOptionStartColumn, 5, SpecialOptionStartColumn, dataStartRow - 1 + RowsCount, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)
                '.SetLine(ColumnsCount + 1, 5, ColumnsCount + 1, dataStartRow - 1 + RowsCount, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThick)

            End With
        End Sub
#End Region

        ''' <summary>
        ''' FreezePanes設定
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub freezeCell(ByVal xls As ShisakuExcel)
            xls.FreezePanes(TagShisakuSyubetu, 10, True)
        End Sub


#Region "セルの広さと高さ"
        ''' <summary>
        ''' 広さと高さの設定
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setWidthHeight(ByVal xls As ShisakuExcel)

            xls.AutoFitCol(TagRenban, xls.EndCol())
            xls.AutoFitRow(1, xls.EndRow())
            xls.SetColWidth(TagShisakuSyubetu, TagShisakuSyubetu, 6.75)
            xls.SetColWidth(TagShisakuGousya, TagShisakuGousya, 6.75)
            xls.SetColWidth(TagBaseGaisousyokuName, TagBaseGaisousyokuName, 26.25)
            xls.SetColWidth(TagCompleteCarShisakuShikenMokuteki, TagCompleteCarShisakuShikenMokuteki, 22.13)
            If _EBomKanshiStartColumn > 0 Then
                xls.SetColWidth(BasicOptionStartColumn, _EBomKanshiStartColumn - 1, 2.88)
                xls.SetColWidth(_EBomKanshiStartColumn, _EBomKanshiStartColumn + 12, 6.75)
            Else
                xls.SetColWidth(BasicOptionStartColumn, xls.EndCol, 2.88)
            End If

            '2012/02/21 追加
            '基本装備仕様及び特別装備仕様のヘッダータイトルが長いとスクロール固定をかけたときに上部の行が隠れて
            '表示できなくなってしまう為、現実的な幅に調整した。
            xls.SetRowHeight(6, 6, 60)
            xls.SetRowHeight(7, 7, 60)
            xls.SetRowHeight(8, 8, 60)

            'With xls
            '    .SetColWidth(TagRenban, TagRenban, 5)
            '    .SetColWidth(TagShisakuSyubetu, TagShisakuSyubetu, 9)
            '    .SetColWidth(TagShisakuGousya, TagShisakuGousya, 10)

            '    .SetColWidth(TagBaseKaihatsuFugo, TagBaseKaihatsuFugo, 8)
            '    .SetColWidth(TagBaseShiyoujyouhouNo, TagBaseShiyoujyouhouNo, 12)
            '    .SetColWidth(TagBaseAppliedNo, TagBaseAppliedNo, 12)
            '    .SetColWidth(TagBaseKatashiki, TagBaseKatashiki, 9)
            '    .SetColWidth(TagBaseShimuke, TagBaseShimuke, 12)
            '    .SetColWidth(TagBaseOp, TagBaseOp, 5)
            '    .SetColWidth(TagBaseGaisousyoku, TagBaseGaisousyoku, 11)
            '    .SetColWidth(TagBaseGaisousyokuName, TagBaseGaisousyokuName, 11)
            '    .SetColWidth(TagBaseNaisousyoku, TagBaseNaisousyoku, 7)
            '    .SetColWidth(TagBaseNaisousyokuName, TagBaseNaisousyokuName, 11)
            '    .SetColWidth(TagShisakuBaseEventCode, TagShisakuBaseEventCode, 14)
            '    .SetColWidth(TagShisakuBaseGousya, TagShisakuBaseGousya, 9)

            '    .SetColWidth(TagReferenceCarShisakuKatashiki, TagReferenceCarShisakuKatashiki, 9)
            '    .SetColWidth(TagReferenceCarShisakuShimuke, TagReferenceCarShisakuShimuke, 12)
            '    .SetColWidth(TagReferenceCarShisakuOp, TagReferenceCarShisakuOp, 9)
            '    .SetColWidth(TagReferenceCarShisakuHandoru, TagReferenceCarShisakuHandoru, 8)
            '    .SetColWidth(TagReferenceCarShisakuSyagata, TagReferenceCarShisakuSyagata, 5)
            '    .SetColWidth(TagReferenceCarShisakuGrade, TagReferenceCarShisakuGrade, 8)
            '    .SetColWidth(TagReferenceCarShisakuSyadaiNo, TagReferenceCarShisakuSyadaiNo, 9)
            '    .SetColWidth(TagReferenceCarShisakuGaisousyoku, TagReferenceCarShisakuGaisousyoku, 7)
            '    .SetColWidth(TagReferenceCarShisakuNaisousyoku, TagReferenceCarShisakuNaisousyoku, 7)
            '    .SetColWidth(TagReferenceCarShisakuGroup, TagReferenceCarShisakuGroup, 8)
            '    .SetColWidth(TagReferenceCarShisakuKoushiNo, TagReferenceCarShisakuKoushiNo, 8)
            '    .SetColWidth(TagReferenceCarShisakuKanseibi, TagReferenceCarShisakuKanseibi, 12)
            '    .SetColWidth(TagReferenceCarShisakuEgKatashiki, TagReferenceCarShisakuEgKatashiki, 5)
            '    .SetColWidth(TagReferenceCarShisakuEgHaikiryou, TagReferenceCarShisakuEgHaikiryou, 7)
            '    .SetColWidth(TagReferenceCarShisakuEgSystem, TagReferenceCarShisakuEgSystem, 8)
            '    .SetColWidth(TagReferenceCarShisakuEgKakyuuki, TagReferenceCarShisakuEgKakyuuki, 7)
            '    .SetColWidth(TagReferenceCarShisakuTmKudou, TagReferenceCarShisakuTmKudou, 5)
            '    .SetColWidth(TagReferenceCarShisakuTmHensokuki, TagReferenceCarShisakuTmHensokuki, 7)
            '    .SetColWidth(TagReferenceCarShisakuTmFukuHensokuki, TagReferenceCarShisakuTmFukuHensokuki, 7)
            '    .SetColWidth(TagReferenceCarShisakuSiyouBusyo, TagReferenceCarShisakuSiyouBusyo, 9)
            '    .SetColWidth(TagReferenceCarShisakuShikenMokuteki, TagReferenceCarShisakuShikenMokuteki, 20)

            '    .SetColWidth(TagCompleteCarShisakuKatashiki, TagCompleteCarShisakuKatashiki, 10)
            '    .SetColWidth(TagCompleteCarShisakuShimuke, TagCompleteCarShisakuShimuke, 7)
            '    .SetColWidth(TagCompleteCarShisakuOp, TagCompleteCarShisakuOp, 5)
            '    .SetColWidth(TagCompleteCarShisakuHandoru, TagCompleteCarShisakuHandoru, 8)
            '    .SetColWidth(TagCompleteCarShisakuSyagata, TagCompleteCarShisakuSyagata, 5)
            '    .SetColWidth(TagCompleteCarShisakuGrade, TagCompleteCarShisakuGrade, 8)
            '    .SetColWidth(TagCompleteCarShisakuSyadaiNo, TagCompleteCarShisakuSyadaiNo, 9)
            '    .SetColWidth(TagCompleteCarShisakuSyadaiNo, TagCompleteCarShisakuSyadaiNo, 7)
            '    .SetColWidth(TagCompleteCarShisakuNaisousyoku, TagCompleteCarShisakuNaisousyoku, 7)
            '    .SetColWidth(TagCompleteCarShisakuGroup, TagCompleteCarShisakuGroup, 8)
            '    .SetColWidth(TagCompleteCarShisakuKoushiNo, TagCompleteCarShisakuKoushiNo, 8)
            '    .SetColWidth(TagCompleteCarShisakuKanseibi, TagCompleteCarShisakuKanseibi, 12)
            '    .SetColWidth(TagCompleteCarShisakuEgKatashiki, TagCompleteCarShisakuEgKatashiki, 5)
            '    .SetColWidth(TagCompleteCarShisakuEgHaikiryou, TagCompleteCarShisakuEgHaikiryou, 7)
            '    .SetColWidth(TagCompleteCarShisakuEgSystem, TagCompleteCarShisakuEgSystem, 8)
            '    .SetColWidth(TagCompleteCarShisakuEgKakyuuki, TagCompleteCarShisakuEgKakyuuki, 7)
            '    .SetColWidth(TagCompleteCarShisakuTmKudou, TagCompleteCarShisakuTmKudou, 5)
            '    .SetColWidth(TagCompleteCarShisakuTmHensokuki, TagCompleteCarShisakuTmHensokuki, 7)
            '    .SetColWidth(TagCompleteCarShisakuTmFukuHensokuki, TagCompleteCarShisakuTmFukuHensokuki, 7)
            '    .SetColWidth(TagCompleteCarShisakuSiyouBusyo, TagCompleteCarShisakuSiyouBusyo, 9)
            '    .SetColWidth(TagCompleteCarShisakuShikenMokuteki, TagCompleteCarShisakuShikenMokuteki, 20)

            '    Dim i As Integer
            '    For i = 1 To ColumnsCount - 57 + 2 'カラー名称分追加
            '        .SetColWidth(57 + i, 57 + i, 3)
            '    Next

            '    .SetRowHeight(6, 6, 200)

            'End With
        End Sub
#End Region

#Region "Local Property"
        ''' <summary>
        ''' EventEditSubject
        ''' </summary>
        ''' <remarks></remarks>
        Private _Subject As Logic.EventEdit
        ''' <summary>
        ''' ベース車subject
        ''' </summary>
        ''' <remarks></remarks>
        Private _BaseCarSubject As EventEditBaseCar
        ''' <summary>
        ''' 設計展開ベース車subject
        ''' </summary>
        ''' <remarks></remarks>
        Private _BaseTenkaiCarSubject As EventEditBaseTenkaiCar
        ''' <summary>
        ''' 完了車subject
        ''' </summary>
        ''' <remarks></remarks>
        Private _CompleteCarSubject As EventEditCompleteCar
        ''' <summary>
        ''' 基本装備仕様subject
        ''' </summary>
        ''' <remarks></remarks>
        Private _BasicOptionSubject As EventEditOption
        ''' <summary>
        ''' 特別装備仕様subject
        ''' </summary>
        ''' <remarks></remarks>
        Private _SpecialOptionSubject As EventEditOption
        ''' <summary>
        ''' 特別装備仕様subject
        ''' </summary>
        ''' <remarks></remarks>
        Private _EbomKanshiSubject As EventEditEbomKanshi
        ''' <summary>
        ''' 出力時「基本装備仕様」部分1番目列の列数
        ''' </summary>
        ''' <remarks></remarks>
        Private _BasicOptionStartColumn As Integer
        ''' <summary>
        ''' 出力時「特別装備仕様」部分1番目列の列数
        ''' </summary>
        ''' <remarks></remarks>
        Private _SpecialOptionStartColumn As Integer
        ''' <summary>
        ''' 出力時「特別装備仕様」部分1番目列の列数
        ''' </summary>
        ''' <remarks></remarks>
        Private _EBomKanshiStartColumn As Integer
        ''' <summary>
        ''' データ部分の行数（データレコド数）
        ''' </summary>
        ''' <remarks></remarks>
        Private _RowsCount As Integer
        ''' <summary>
        ''' データ部分の列数
        ''' </summary>
        ''' <remarks></remarks>
        Private _ColumnsCount As Integer
        ''' <summary>
        ''' データ部分の一番目の行位置
        ''' １０→１２へ（基本及び特別装備仕様に大、中を追加により2行追加）
        ''' </summary>
        ''' <remarks></remarks>
        Private dataStartRow = 12
        ''' <summary>
        ''' 使用者情報
        ''' </summary>
        ''' <remarks></remarks>
        Private ReadOnly aLoginInfo As LoginInfo
        ''' <summary>
        ''' 基本装備仕様、特別装備仕様にブランク列を付け
        ''' </summary>
        ''' <remarks></remarks>
        Private ReadOnly blankColumnsCount As Integer = 5
#End Region

#Region "Local Property getとset"
        ''' <summary>
        ''' EventEditSubject
        ''' </summary>
        ''' <value>EventEditSubject</value>
        ''' <returns>EventEditSubject</returns>
        ''' <remarks></remarks>
        Public Property Subject() As Logic.EventEdit
            Get
                Return _Subject
            End Get
            Set(ByVal value As Logic.EventEdit)
                _Subject = value
            End Set
        End Property

        ''' <summary>
        ''' ベース車subject
        ''' </summary>
        ''' <value>ベース車subject</value>
        ''' <returns>ベース車subject</returns>
        ''' <remarks></remarks>
        Public Property BaseCarSubject() As EventEditBaseCar
            Get
                Return _BaseCarSubject
            End Get
            Set(ByVal value As EventEditBaseCar)
                _BaseCarSubject = value
            End Set
        End Property

        ''' <summary>
        ''' 設計展開ベース車subject
        ''' </summary>
        ''' <value>設計展開ベース車subject</value>
        ''' <returns>設計展開ベース車subject</returns>
        ''' <remarks></remarks>
        Public Property BaseTenkaiCarSubject() As EventEditBaseTenkaiCar
            Get
                Return _BaseTenkaiCarSubject
            End Get
            Set(ByVal value As EventEditBaseTenkaiCar)
                _BaseTenkaiCarSubject = value
            End Set
        End Property

        ''' <summary>
        ''' 完了車subject
        ''' </summary>
        ''' <value>完了車subject</value>
        ''' <returns>完了車subject</returns>
        ''' <remarks></remarks>
        Public Property CompleteCarSubject() As EventEditCompleteCar
            Get
                Return _CompleteCarSubject
            End Get
            Set(ByVal value As EventEditCompleteCar)
                _CompleteCarSubject = value
            End Set
        End Property

        ''' <summary>
        ''' 基本装備仕様subject
        ''' </summary>
        ''' <value>基本装備仕様subject</value>
        ''' <returns>基本装備仕様subject</returns>
        ''' <remarks></remarks>
        Public Property BasicOptionSubject() As EventEditOption
            Get
                Return _BasicOptionSubject
            End Get
            Set(ByVal value As EventEditOption)
                _BasicOptionSubject = value
            End Set
        End Property

        ''' <summary>
        ''' 特別装備仕様subject
        ''' </summary>
        ''' <value>特別装備仕様subject</value>
        ''' <returns>特別装備仕様subject</returns>
        ''' <remarks></remarks>
        Public Property SpecialOptionSubject() As EventEditOption
            Get
                Return _SpecialOptionSubject
            End Get
            Set(ByVal value As EventEditOption)
                _SpecialOptionSubject = value
            End Set
        End Property

        ''' <summary>
        ''' 特別装備仕様subject
        ''' </summary>
        ''' <value>特別装備仕様subject</value>
        ''' <returns>特別装備仕様subject</returns>
        ''' <remarks></remarks>
        Public Property EbomKanshiSubject() As EventEditEbomKanshi
            Get
                Return _EbomKanshiSubject
            End Get
            Set(ByVal value As EventEditEbomKanshi)
                _EbomKanshiSubject = value
            End Set
        End Property

        ''' <summary>
        ''' 出力時「基本装備仕様」部分1番目列の列数
        ''' </summary>
        ''' <value>出力時「基本装備仕様」部分1番目列の列数</value>
        ''' <returns>出力時「基本装備仕様」部分1番目列の列数</returns>
        ''' <remarks></remarks>
        Public Property BasicOptionStartColumn() As Integer
            Get
                Return _BasicOptionStartColumn
            End Get
            Set(ByVal value As Integer)
                _BasicOptionStartColumn = value
            End Set
        End Property

        ''' <summary>
        ''' 出力時「特別装備仕様」部分1番目列の列数
        ''' </summary>
        ''' <value>出力時「特別装備仕様」部分1番目列の列数</value>
        ''' <returns>出力時「特別装備仕様」部分1番目列の列数</returns>
        ''' <remarks></remarks>
        Public Property SpecialOptionStartColumn() As Integer
            Get
                Return _SpecialOptionStartColumn
            End Get
            Set(ByVal value As Integer)
                _SpecialOptionStartColumn = value
            End Set
        End Property

        ''' <summary>
        ''' データ部分の行数（データレコド数）
        ''' </summary>
        ''' <value>データ部分の行数（データレコド数）</value>
        ''' <returns>データ部分の行数（データレコド数）</returns>
        ''' <remarks></remarks>
        Public Property RowsCount() As Integer
            Get
                Return _RowsCount
            End Get
            Set(ByVal value As Integer)
                _RowsCount = value
            End Set
        End Property


        ''' <summary>
        ''' データ部分の列数
        ''' </summary>
        ''' <value>データ部分の列数</value>
        ''' <returns>データ部分の列数</returns>
        ''' <remarks></remarks>
        Public Property ColumnsCount() As Integer
            Get
                Return _ColumnsCount
            End Get
            Set(ByVal value As Integer)
                _ColumnsCount = value
            End Set
        End Property
#End Region

    End Class
End Namespace