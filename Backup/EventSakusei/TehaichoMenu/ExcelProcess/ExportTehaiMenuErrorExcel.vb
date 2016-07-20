Imports ShisakuCommon
Imports EBom.Excel
Imports EBom.Data
Imports EBom.Common
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.TehaichoMenu.Dao
Imports EventSakusei.TehaichoMenu.Impl
Imports EventSakusei.TehaichoMenu.Vo

Namespace TehaichoMenu.Excel
    Public Class ExportTehaiMenuErrorExcel

        Private impl As TehaichoMenuDaoImpl
        'リストコード'
        Private aList As TShisakuListcodeVo
        '新調達リスト'
        Private aSEListVo As List(Of TehaiMenuErrorExcelVo)
        '現調品リスト'
        Private aGEListVo As List(Of TehaiMenuErrorExcelVo)
        'ユーザー名'
        Private aNameVo As String
        'イベント情報'
        Private aEventVo As TShisakuEventVo


        Public Sub New(ByVal eventCode As String, ByVal listCode As String, ByVal kaiteiNo As String)
            Dim systemDrive As String = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            Dim fileName As String

            impl = New TehaichoMenuDaoImpl
            aList = New TShisakuListcodeVo
            aSEListVo = New List(Of TehaiMenuErrorExcelVo)
            aGEListVo = New List(Of TehaiMenuErrorExcelVo)
            aEventVo = New TShisakuEventVo

            aEventVo = impl.FindByUnitKbn(eventCode)


            '取得処理を追加する'
            aList = impl.FindByListCode(eventCode, listCode)

            Using sfd As New SaveFileDialog()
                sfd.FileName = ShisakuCommon.ShisakuGlobal.ExcelOutPut
                '2012/01/25
                '--------------------------------------------------
                'EXCEL出力先フォルダチェック
                ShisakuCommon.ShisakuComFunc.ExcelOutPutDirCheck()
                '--------------------------------------------------
                '[Excel出力系 I]
                sfd.InitialDirectory = systemDrive
                fileName = aEventVo.ShisakuKaihatsuFugo + aList.ShisakuEventName + " " + kaiteiNo + " エラーチェック.xls"
                fileName = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + StringUtil.ReplaceInvalidCharForFileName(fileName)    '2012/02/08 Excel出力ディレクトリ指定対応
            End Using

            aSEListVo = impl.FindByShinchotsuError(eventCode, listCode)
            aGEListVo = impl.FindByGenchoError(eventCode, listCode)

            'エラー情報が一件も無ければメッセージを出力して終了する。
            If aSEListVo.Count = 0 And aGEListVo.Count = 0 Then
                MsgBox("エラーが一件もありませんでした。", MsgBoxStyle.OkOnly, "OK")
                Return
            End If

            If Not ShisakuComFunc.IsFileOpen(ShisakuCommon.ShisakuGlobal.ExcelOutPut) Then

                Using xls As New ShisakuExcel(fileName)
                    xls.OpenBook(fileName)
                    xls.ClearWorkBook()
                    xls.SetFont("ＭＳ Ｐゴシック", 11)
                    setShinchotatsuSheet(xls, aList, aSEListVo, aEventVo)
                    '2012/02/02'
                    'A4横で印刷できるように変更'
                    xls.PrintPaper(fileName, 1, "A4")
                    xls.PrintOrientation(fileName, 1, 1, False)
                    setGenchohinSheet(xls, aList, aGEListVo, aEventVo)
                    '2012/02/02'
                    'A4横で印刷できるように変更'
                    xls.PrintPaper(fileName, 2, "A4")
                    xls.PrintOrientation(fileName, 2, 1, False)
                    xls.SetActiveSheet(1)
                    xls.Save()
                End Using
                Process.Start(fileName)
            End If

        End Sub


        ''' <summary>
        ''' Excel出力　新調達シート
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="SErrorList">新調達エラー情報リスト</param>
        ''' <remarks></remarks>
        Private Sub setShinchotatsuSheet(ByVal xls As ShisakuExcel, _
                                         ByVal aList As TShisakuListcodeVo, _
                                         ByVal SErrorList As List(Of TehaiMenuErrorExcelVo), _
                                         ByVal aEventVo As TShisakuEventVo)
            xls.SetActiveSheet(1)

            'シートのデフォルトの罫線を消す'
            'xls.SetLine(1, 1, 600, 65535, XlBordersIndex.xlEdgeTop, XlLineStyle.xlLineStyleNone, XlBorderWeight.xlHairline)
            'xls.SetLine(1, 1, 600, 65535, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlLineStyleNone, XlBorderWeight.xlHairline)
            'xls.SetLine(1, 1, 600, 65535, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlLineStyleNone, XlBorderWeight.xlHairline)
            'xls.SetLine(1, 1, 600, 65535, XlBordersIndex.xlEdgeRight, XlLineStyle.xlLineStyleNone, XlBorderWeight.xlHairline)
            'xls.SetLine(1, 1, 600, 65535, XlBordersIndex.xlInsideHorizontal, XlLineStyle.xlLineStyleNone, XlBorderWeight.xlHairline)
            'xls.SetLine(1, 1, 600, 65535, XlBordersIndex.xlInsideVertical, XlLineStyle.xlLineStyleNone, XlBorderWeight.xlHairline)


            setShinchotatsuSheetHeard(xls, aList, aEventVo)

            setShinchotatsuSheetBody(xls, aSEListVo)

            setShinchotastuSheetColumnWidth(xls)
        End Sub

        ''' <summary>
        ''' Excel出力　現調品シート
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="GErrorList">現調品エラー情報リスト</param>
        ''' <remarks></remarks>
        Private Sub setGenchohinSheet(ByVal xls As ShisakuExcel, _
                                      ByVal aList As TShisakuListcodeVo, _
                                      ByVal GErrorList As List(Of TehaiMenuErrorExcelVo), _
                                      ByVal aEventVo As TShisakuEventVo)
            xls.SetActiveSheet(2)

            setGenchoSheetHeard(xls, aList, aEventVo)

            setGenchoSheetBody(xls, aGEListVo)

            setGenchoSheetColumnWidth(xls)


        End Sub

        ''' <summary>
        ''' Excel出力　新調達シートのHeaderの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aList">リストコード情報</param>
        ''' <remarks></remarks>
        Public Sub setShinchotatsuSheetHeard(ByVal xls As ShisakuExcel, _
                                  ByVal aList As TShisakuListcodeVo, _
                                  ByVal aEventVo As TShisakuEventVo)

            'TODO 仕様変更に耐えうるようにすべき'
            'ユニット区分用にイベント情報も追加()

            'タイトル'
            xls.MergeCells(1, 1, 8, 1, True)

            xls.SetValue(1, 1, "手配帳エラーリスト(新調達)")
            xls.SetFont(1, 1, 1, 1, "ＭＳ Ｐゴシック", 14, Nothing, True)

            'リストコード'
            xls.MergeCells(1, 3, 3, 3, True)
            xls.SetValue(1, 3, "リストコード")
            xls.SetValue(4, 3, aList.ShisakuListCode)
            xls.SetFont(1, 3, 1, 3, "ＭＳ Ｐゴシック", 11, Nothing, True)
            'イベント名称'
            xls.MergeCells(1, 4, 3, 4, True)
            xls.SetValue(1, 4, "イベント名称")
            xls.SetValue(4, 4, aList.ShisakuEventName)
            xls.SetFont(1, 4, 1, 4, "ＭＳ Ｐゴシック", 11, Nothing, True)
            '工事指令No'
            xls.MergeCells(1, 6, 3, 6, True)
            xls.SetValue(1, 6, "工事指令No")
            xls.SetValue(4, 6, aList.ShisakuKoujiShireiNo)
            xls.SetFont(1, 6, 1, 6, "ＭＳ Ｐゴシック", 11, Nothing, True)
            '工事区分'
            xls.MergeCells(1, 7, 3, 7, True)
            xls.SetValue(1, 7, "工事区分")
            xls.SetValue(4, 7, aList.ShisakuKoujiKbn)
            xls.SetFont(1, 7, 1, 7, "ＭＳ Ｐゴシック", 11, Nothing, True)
            'ユニット区分'
            xls.MergeCells(1, 8, 3, 8, True)
            xls.SetValue(1, 8, "ユニット区分")
            xls.SetValue(4, 8, aEventVo.UnitKbn)
            xls.SetFont(1, 8, 1, 8, "ＭＳ Ｐゴシック", 11, Nothing, True)
            '製品区分'
            xls.MergeCells(1, 9, 3, 9, True)
            xls.SetValue(1, 9, "製品区分")
            xls.SetValue(4, 9, aList.ShisakuSeihinKbn)
            xls.SetFont(1, 9, 1, 9, "ＭＳ Ｐゴシック", 11, Nothing, True)

            '工事指令から製品区分まで罫線'
            xls.SetLine(1, 6, 3, 9, XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)
            xls.SetLine(1, 6, 3, 9, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)
            xls.SetLine(1, 6, 3, 9, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)
            xls.SetLine(1, 6, 3, 9, XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)
            xls.SetLine(1, 6, 3, 9, XlBordersIndex.xlInsideHorizontal, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)
            'xls.SetLine(1, 3, 1, 10, XlBordersIndex.xlInsideVertical, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)

            xls.SetLine(4, 6, 4, 9, XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            xls.SetLine(4, 6, 4, 9, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            xls.SetLine(4, 6, 4, 9, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            xls.SetLine(4, 6, 4, 9, XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            xls.SetLine(4, 6, 4, 9, XlBordersIndex.xlInsideHorizontal, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            'xls.SetLine(4, 3, 4, 10, XlBordersIndex.xlInsideVertical, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)

            '判定コード表'
            xls.MergeCells(13, 1, 14, 1, True)
            xls.SetValue(13, 1, "◆判定コード表")
            xls.SetValue(13, 2, "E")

            xls.SetValue(14, 2, "ERROR(発注不可)")
            xls.SetValue(15, 2, "W")
            xls.SetValue(16, 2, "WARNING(警告:発注可能)")

            xls.SetLine(13, 2, 16, 2, XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            xls.SetLine(13, 2, 16, 2, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            xls.SetLine(13, 2, 16, 2, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            xls.SetLine(13, 2, 16, 2, XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            'xls.SetLine(13, 2, 16, 2, XlBordersIndex.xlInsideVertical, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)

            'エラーコード表'

            xls.SetFont(15, 4, 25, 9, "ＭＳ Ｐゴシック", 10)

            xls.SetValue(13, 3, "◆エラーコード表")
            xls.SetValue(13, 4, "E01")
            xls.SetValue(13, 5, "E03")
            xls.SetValue(13, 6, "E04")
            xls.SetValue(13, 7, "E05")
            xls.SetValue(13, 8, "E06")

            xls.SetValue(14, 4, "ブロックNoが未入力")
            xls.SetValue(14, 5, "部品番号が未入力")
            xls.SetValue(14, 6, "部品番号の文字エラー")
            xls.SetValue(14, 7, "部品名称が未入力")
            xls.SetValue(14, 8, "納入指示数が０個")

            xls.SetValue(15, 4, "E07")
            xls.SetValue(15, 5, "E08")
            xls.SetValue(15, 6, "E09")
            xls.SetValue(15, 7, "E10")
            xls.SetValue(15, 8, "E11")

            xls.MergeCells(16, 4, 18, 4, True)
            xls.MergeCells(16, 5, 18, 5, True)
            xls.MergeCells(16, 6, 18, 6, True)
            xls.MergeCells(16, 7, 18, 7, True)
            xls.MergeCells(16, 8, 18, 8, True)

            xls.SetValue(16, 4, "納入指示日が未入力")
            xls.SetValue(16, 5, "納入指示日が過去日付")
            xls.SetValue(16, 6, "納入指示日が仮伝納期(３日以内)")
            xls.SetValue(16, 7, "納入指示日要注意(５日以内)")
            xls.SetValue(16, 8, "納入場所が未入力")

            xls.SetValue(19, 4, "E12")
            xls.SetValue(19, 5, "E13")
            xls.SetValue(19, 6, "E14")
            xls.SetValue(19, 7, "E16")
            xls.SetValue(19, 8, "E17")

            xls.MergeCells(20, 4, 22, 4, True)
            xls.MergeCells(20, 5, 22, 5, True)
            xls.MergeCells(20, 6, 22, 6, True)
            xls.MergeCells(20, 7, 22, 7, True)
            xls.MergeCells(20, 8, 22, 8, True)

            xls.SetValue(20, 4, "供給セクションが未入力")
            xls.SetValue(20, 5, "取引先と供給先が社内コード(納区４)")
            xls.SetValue(20, 6, "供給セクションエラー(新調達取扱い無し)")
            xls.SetValue(20, 7, "購担が未入力")
            xls.SetValue(20, 8, "購担がマスタと不一致")

            xls.SetValue(23, 4, "E18")
            xls.SetValue(23, 5, "E19")
            xls.SetValue(23, 6, "E20")
            xls.SetValue(23, 7, "E21")

            xls.MergeCells(24, 4, 26, 4, True)
            xls.MergeCells(24, 5, 26, 5, True)
            xls.MergeCells(24, 6, 26, 6, True)
            xls.MergeCells(24, 7, 26, 7, True)
            xls.MergeCells(24, 8, 26, 8, True)

            xls.SetValue(24, 4, "取扱先が未入力")
            xls.SetValue(24, 5, "取扱い先がマスタと不一致")
            xls.SetValue(24, 6, "購担エラー(新調達取扱い無し)")
            xls.SetValue(24, 7, "取引先エラー(新調達取扱い無し)")


            xls.SetLine(13, 4, 26, 8, XlBordersIndex.xlEdgeTop, XlLineStyle.xlDot, XlBorderWeight.xlThin)
            xls.SetLine(13, 4, 26, 8, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlDot, XlBorderWeight.xlThin)
            xls.SetLine(13, 4, 26, 8, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDot, XlBorderWeight.xlThin)
            xls.SetLine(13, 4, 26, 8, XlBordersIndex.xlEdgeRight, XlLineStyle.xlDot, XlBorderWeight.xlThin)
            xls.SetLine(13, 4, 26, 8, XlBordersIndex.xlInsideVertical, XlLineStyle.xlDot, XlBorderWeight.xlThin)

            xls.MergeCells(28, 1, 29, 1, True)
            xls.MergeCells(28, 2, 29, 2, True)
            xls.SetValue(28, 1, 28, 1, "処理年月日")
            xls.SetValue(28, 2, 28, 2, "担当者")
            xls.MergeCells(30, 1, 31, 1, True)
            xls.MergeCells(30, 2, 31, 2, True)
            xls.SetValue(30, 1, 30, 1, Replace(aList.UpdatedDate, "-", "/"))

            Dim shainName As String
            Dim login As New LoginInfo

            shainName = impl.FindByShainName(login.UserId)

            xls.SetValue(30, 2, 30, 2, shainName)


        End Sub

        ''' <summary>
        ''' Excel出力　新調達シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aSEList">新調達エラー情報リスト</param>
        ''' <remarks></remarks>
        Public Sub setShinchotatsuSheetBody(ByVal xls As ShisakuExcel, ByVal aSEList As List(Of TehaiMenuErrorExcelVo))

            'タイトル部分の作成'
            setShinchotastuTitleRow(xls)
            Dim dataMatrix(aSEList.Count - 1, COLUMN_GENCHO_MASTER_TORIHIKISAKI) As String

            For index As Integer = 0 To aSEList.Count - 1

                '判定'
                dataMatrix(index, COLUMN_ERROR_HANTEI - 1) = aSEList(index).ErrorKbn
                'ブロックNo.EC'
                dataMatrix(index, COLUMN_EC_BLOCK_NO - 1) = aSEList(index).EcShisakuBlockNo
                'ブロックNo'
                dataMatrix(index, COLUMN_BLOCK_NO - 1) = aSEList(index).ShisakuBlockNo
                '工事No.EC'
                dataMatrix(index, COLUMN_EC_KOUJI_NO - 1) = ""
                '工事No.'
                dataMatrix(index, COLUMN_KOUJI_NO - 1) = aSEList(index).ShisakuKoujiNo
                '行ID'
                dataMatrix(index, COLUMN_ROW_ID - 1) = aSEList(index).GyouId
                '専用マーク'
                dataMatrix(index, COLUMN_SENYO_MARK - 1) = aSEList(index).SenyouMark
                '記'
                dataMatrix(index, COLUMN_TEHAI_KIGOU - 1) = aSEList(index).TehaiKigou
                '部品番号EC'
                dataMatrix(index, COLUMN_EC_BUHIN_NO - 1) = aSEList(index).EcBuhinNo
                '部品番号'
                dataMatrix(index, COLUMN_BUHIN_NO - 1) = aSEList(index).BuhinNo
                '試作区分'
                dataMatrix(index, COLUMN_SHISAKU_KBN - 1) = aSEList(index).BuhinNoKbn
                '部品名称EC'
                dataMatrix(index, COLUMN_EC_BUHIN_NAME - 1) = aSEList(index).EcBuhinName
                '部品名称'
                dataMatrix(index, COLUMN_BUHIN_NAME - 1) = aSEList(index).BuhinName
                '納入指示数EC'
                dataMatrix(index, COLUMN_EC_NOUNYU_SHIJISU - 1) = aSEList(index).EcTotalInsuSuryo
                '納入指示数'
                dataMatrix(index, COLUMN_NOUNYU_SHIJI_SU - 1) = aSEList(index).TotalInsuSuryo
                '納入指示日EC'
                dataMatrix(index, COLUMN_EC_NOUNYU_SHIJI_BI - 1) = aSEList(index).EcNounyuShijibi
                '納入指示日'
                dataMatrix(index, COLUMN_NOUNYU_SHIJI_BI - 1) = aSEList(index).NounyuShijibi
                '納入場所EC'
                dataMatrix(index, COLUMN_EC_NOUBA - 1) = aSEList(index).EcNouba
                '納入場所'
                dataMatrix(index, COLUMN_NOUBA - 1) = aSEList(index).Nouba
                '供給セクションEC'
                dataMatrix(index, COLUMN_EC_KYOKU_SECTION - 1) = aSEList(index).EcKyoukuSection
                '供給セクション'
                dataMatrix(index, COLUMN_KYOUKU_SECTION - 1) = aSEList(index).KyoukuSection
                '購入希望単価EC'
                dataMatrix(index, COLUMN_EC_KOUNYU_KIBOU_TANKA - 1) = ""
                '購入希望単価'
                dataMatrix(index, COLUMN_KOUNYU_KIBOU_TANKA - 1) = ""
                '購担EC'
                dataMatrix(index, COLUMN_EC_KOUTAN - 1) = aSEList(index).EcKoutanSection
                '購担'
                dataMatrix(index, COLUMN_KOUTAN - 1) = aSEList(index).Koutan
                '取引先EC'
                dataMatrix(index, COLUMN_EC_TORIHIKISAKI - 1) = aSEList(index).EcTorihikisaki
                '取引先'
                dataMatrix(index, COLUMN_TORIHIKISAKI - 1) = aSEList(index).Torihikisaki
                'マスタ参照'
                '購担'
                dataMatrix(index, COLUMN_MASTER_KOUTAN - 1) = aSEList(index).MasterKoutan
                '取引先'
                dataMatrix(index, COLUMN_MASTER_TORIHIKISAKI - 1) = aSEList(index).MasterTorihikisaki


                'SetLineBody(xls, COLUMN_ERROR_HANTEI, START_ROW + index, COLUMN_ERROR_HANTEI, START_ROW + index)
                'SetLineBody(xls, COLUMN_EC_BLOCK_NO, START_ROW + index, COLUMN_BLOCK_NO, START_ROW + index)
                'SetLineBody(xls, COLUMN_EC_KOUJI_NO, START_ROW + index, COLUMN_KOUJI_NO, START_ROW + index)
                'SetLineBody(xls, COLUMN_ROW_ID, START_ROW + index, COLUMN_ROW_ID, START_ROW + index)
                'SetLineBody(xls, COLUMN_SENYO_MARK, START_ROW + index, COLUMN_SENYO_MARK, START_ROW + index)
                'SetLineBody(xls, COLUMN_TEHAI_KIGOU, START_ROW + index, COLUMN_TEHAI_KIGOU, START_ROW + index)
                'SetLineBody(xls, COLUMN_EC_BUHIN_NO, START_ROW + index, COLUMN_BUHIN_NO, START_ROW + index)
                'SetLineBody(xls, COLUMN_SHISAKU_KBN, START_ROW + index, COLUMN_SHISAKU_KBN, START_ROW + index)
                'SetLineBody(xls, COLUMN_EC_BUHIN_NAME, START_ROW + index, COLUMN_BUHIN_NAME, START_ROW + index)
                'SetLineBody(xls, COLUMN_EC_NOUNYU_SHIJISU, START_ROW + index, COLUMN_NOUNYU_SHIJI_SU, START_ROW + index)
                'SetLineBody(xls, COLUMN_EC_NOUNYU_SHIJI_BI, START_ROW + index, COLUMN_NOUNYU_SHIJI_BI, START_ROW + index)
                'SetLineBody(xls, COLUMN_EC_NOUBA, START_ROW + index, COLUMN_NOUBA, START_ROW + index)
                'SetLineBody(xls, COLUMN_EC_KYOKU_SECTION, START_ROW + index, COLUMN_KYOUKU_SECTION, START_ROW + index)
                'SetLineBody(xls, COLUMN_EC_KOUNYU_KIBOU_TANKA, START_ROW + index, COLUMN_KOUNYU_KIBOU_TANKA, START_ROW + index)
                'SetLineBody(xls, COLUMN_EC_KOUTAN, START_ROW + index, COLUMN_KOUTAN, START_ROW + index)
                'SetLineBody(xls, COLUMN_EC_TORIHIKISAKI, START_ROW + index, COLUMN_TORIHIKISAKI, START_ROW + index)
                'SetLineBody(xls, COLUMN_MASTER_KOUTAN, START_ROW + index, COLUMN_MASTER_KOUTAN, START_ROW + index)
                'SetLineBody(xls, COLUMN_MASTER_TORIHIKISAKI, START_ROW + index, COLUMN_MASTER_TORIHIKISAKI, START_ROW + index)


            Next
            xls.SetFont(COLUMN_EC_BLOCK_NO, START_ROW, COLUMN_EC_BLOCK_NO, START_ROW + aSEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_KOUJI_NO, START_ROW, COLUMN_EC_KOUJI_NO, START_ROW + aSEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_BUHIN_NO, START_ROW, COLUMN_EC_BUHIN_NO, START_ROW + aSEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_BUHIN_NAME, START_ROW, COLUMN_EC_BUHIN_NAME, START_ROW + aSEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_NOUNYU_SHIJISU, START_ROW, COLUMN_EC_NOUNYU_SHIJISU, START_ROW + aSEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_NOUNYU_SHIJI_BI, START_ROW, COLUMN_EC_NOUNYU_SHIJI_BI, START_ROW + aSEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_NOUBA, START_ROW, COLUMN_EC_NOUBA, START_ROW + aSEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_KYOKU_SECTION, START_ROW, COLUMN_EC_KYOKU_SECTION, START_ROW + aSEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_KOUNYU_KIBOU_TANKA, START_ROW, COLUMN_EC_KOUNYU_KIBOU_TANKA, START_ROW + aSEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_KOUTAN, START_ROW, COLUMN_EC_KOUTAN, START_ROW + aSEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_TORIHIKISAKI, START_ROW, COLUMN_EC_TORIHIKISAKI, START_ROW + aSEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)

            SetLineBody(xls, COLUMN_ERROR_HANTEI, START_ROW, COLUMN_ERROR_HANTEI, START_ROW + aSEList.Count)
            SetLineBody(xls, COLUMN_EC_BLOCK_NO, START_ROW, COLUMN_BLOCK_NO, START_ROW + aSEList.Count)
            SetLineBody(xls, COLUMN_EC_KOUJI_NO, START_ROW, COLUMN_KOUJI_NO, START_ROW + aSEList.Count)
            SetLineBody(xls, COLUMN_ROW_ID, START_ROW, COLUMN_ROW_ID, START_ROW + aSEList.Count)
            SetLineBody(xls, COLUMN_SENYO_MARK, START_ROW, COLUMN_SENYO_MARK, START_ROW + aSEList.Count)
            SetLineBody(xls, COLUMN_TEHAI_KIGOU, START_ROW, COLUMN_TEHAI_KIGOU, START_ROW + aSEList.Count)
            SetLineBody(xls, COLUMN_EC_BUHIN_NO, START_ROW, COLUMN_BUHIN_NO, START_ROW + aSEList.Count)
            SetLineBody(xls, COLUMN_SHISAKU_KBN, START_ROW, COLUMN_SHISAKU_KBN, START_ROW + aSEList.Count)
            SetLineBody(xls, COLUMN_EC_BUHIN_NAME, START_ROW, COLUMN_BUHIN_NAME, START_ROW + aSEList.Count)
            SetLineBody(xls, COLUMN_EC_NOUNYU_SHIJISU, START_ROW, COLUMN_NOUNYU_SHIJI_SU, START_ROW + aSEList.Count)
            SetLineBody(xls, COLUMN_EC_NOUNYU_SHIJI_BI, START_ROW, COLUMN_NOUNYU_SHIJI_BI, START_ROW + aSEList.Count)
            SetLineBody(xls, COLUMN_EC_NOUBA, START_ROW, COLUMN_NOUBA, START_ROW + aSEList.Count)
            SetLineBody(xls, COLUMN_EC_KYOKU_SECTION, START_ROW, COLUMN_KYOUKU_SECTION, START_ROW + aSEList.Count)
            SetLineBody(xls, COLUMN_EC_KOUNYU_KIBOU_TANKA, START_ROW, COLUMN_KOUNYU_KIBOU_TANKA, START_ROW + aSEList.Count)
            SetLineBody(xls, COLUMN_EC_KOUTAN, START_ROW, COLUMN_KOUTAN, START_ROW + aSEList.Count)
            SetLineBody(xls, COLUMN_EC_TORIHIKISAKI, START_ROW, COLUMN_TORIHIKISAKI, START_ROW + aSEList.Count)
            SetLineBody(xls, COLUMN_MASTER_KOUTAN, START_ROW, COLUMN_MASTER_KOUTAN, START_ROW + aSEList.Count)
            SetLineBody(xls, COLUMN_MASTER_TORIHIKISAKI, START_ROW, COLUMN_MASTER_TORIHIKISAKI, START_ROW + aSEList.Count)

            xls.CopyRange(0, START_ROW, UBound(dataMatrix, 2) - 1, START_ROW + UBound(dataMatrix), dataMatrix)

        End Sub
        ''' <summary>
        ''' Excel出力　新調達シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aSEList">新調達エラー情報リスト</param>
        ''' <remarks></remarks>
        Public Sub setShinchotatsuSheetBodyOLD(ByVal xls As ShisakuExcel, ByVal aSEList As List(Of TehaiMenuErrorExcelVo))

            'タイトル部分の作成'
            setShinchotastuTitleRow(xls)

            For index As Integer = 0 To aSEList.Count - 1
                '判定'
                xls.SetValue(COLUMN_ERROR_HANTEI, START_ROW + index, COLUMN_ERROR_HANTEI, START_ROW + index, aSEList(index).ErrorKbn)
                SetLineBody(xls, COLUMN_ERROR_HANTEI, START_ROW + index, COLUMN_ERROR_HANTEI, START_ROW + index)
                'ブロックNo.EC'
                xls.SetFont(COLUMN_EC_BLOCK_NO, START_ROW + index, COLUMN_EC_BLOCK_NO, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_BLOCK_NO, START_ROW + index, COLUMN_EC_BLOCK_NO, START_ROW + index, aSEList(index).EcShisakuBlockNo)
                'ブロックNo'
                xls.SetValue(COLUMN_BLOCK_NO, START_ROW + index, COLUMN_BLOCK_NO, START_ROW + index, aSEList(index).ShisakuBlockNo)
                SetLineBody(xls, COLUMN_EC_BLOCK_NO, START_ROW + index, COLUMN_BLOCK_NO, START_ROW + index)
                '工事No.EC'
                xls.SetFont(COLUMN_EC_KOUJI_NO, START_ROW + index, COLUMN_EC_KOUJI_NO, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_KOUJI_NO, START_ROW + index, COLUMN_EC_KOUJI_NO, START_ROW + index, "")
                '工事No.'
                xls.SetValue(COLUMN_KOUJI_NO, START_ROW + index, COLUMN_KOUJI_NO, START_ROW + index, aSEList(index).ShisakuKoujiNo)
                SetLineBody(xls, COLUMN_EC_KOUJI_NO, START_ROW + index, COLUMN_KOUJI_NO, START_ROW + index)
                '行ID'
                xls.SetValue(COLUMN_ROW_ID, START_ROW + index, COLUMN_ROW_ID, START_ROW + index, aSEList(index).GyouId)
                SetLineBody(xls, COLUMN_ROW_ID, START_ROW + index, COLUMN_ROW_ID, START_ROW + index)
                '専用マーク'
                xls.SetValue(COLUMN_SENYO_MARK, START_ROW + index, COLUMN_SENYO_MARK, START_ROW + index, aSEList(index).SenyouMark)
                SetLineBody(xls, COLUMN_SENYO_MARK, START_ROW + index, COLUMN_SENYO_MARK, START_ROW + index)
                '記'
                xls.SetValue(COLUMN_TEHAI_KIGOU, START_ROW + index, COLUMN_TEHAI_KIGOU, START_ROW + index, aSEList(index).TehaiKigou)
                SetLineBody(xls, COLUMN_TEHAI_KIGOU, START_ROW + index, COLUMN_TEHAI_KIGOU, START_ROW + index)
                '部品番号EC'
                xls.SetFont(COLUMN_EC_BUHIN_NO, START_ROW + index, COLUMN_EC_BUHIN_NO, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_BUHIN_NO, START_ROW + index, COLUMN_EC_BUHIN_NO, START_ROW + index, aSEList(index).EcBuhinNo)
                '部品番号'
                xls.SetValue(COLUMN_BUHIN_NO, START_ROW + index, COLUMN_BUHIN_NO, START_ROW + index, aSEList(index).BuhinNo)
                SetLineBody(xls, COLUMN_EC_BUHIN_NO, START_ROW + index, COLUMN_BUHIN_NO, START_ROW + index)
                '試作区分'
                xls.SetValue(COLUMN_SHISAKU_KBN, START_ROW + index, COLUMN_SHISAKU_KBN, START_ROW + index, aSEList(index).BuhinNoKbn)
                SetLineBody(xls, COLUMN_SHISAKU_KBN, START_ROW + index, COLUMN_SHISAKU_KBN, START_ROW + index)
                '部品名称EC'
                xls.SetFont(COLUMN_EC_BUHIN_NAME, START_ROW + index, COLUMN_EC_BUHIN_NAME, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_BUHIN_NAME, START_ROW + index, COLUMN_EC_BUHIN_NAME, START_ROW + index, aSEList(index).EcBuhinName)
                '部品名称'
                xls.SetValue(COLUMN_BUHIN_NAME, START_ROW + index, COLUMN_BUHIN_NAME, START_ROW + index, aSEList(index).BuhinName)
                SetLineBody(xls, COLUMN_EC_BUHIN_NAME, START_ROW + index, COLUMN_BUHIN_NAME, START_ROW + index)
                '納入指示数EC'
                xls.SetFont(COLUMN_EC_NOUNYU_SHIJISU, START_ROW + index, COLUMN_EC_NOUNYU_SHIJISU, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_NOUNYU_SHIJISU, START_ROW + index, COLUMN_EC_NOUNYU_SHIJISU, START_ROW + index, aSEList(index).EcTotalInsuSuryo)
                '納入指示数'
                xls.SetValue(COLUMN_NOUNYU_SHIJI_SU, START_ROW + index, COLUMN_NOUNYU_SHIJI_SU, START_ROW + index, aSEList(index).TotalInsuSuryo)
                SetLineBody(xls, COLUMN_EC_NOUNYU_SHIJISU, START_ROW + index, COLUMN_NOUNYU_SHIJI_SU, START_ROW + index)
                '納入指示日EC'
                xls.SetFont(COLUMN_EC_NOUNYU_SHIJI_BI, START_ROW + index, COLUMN_EC_NOUNYU_SHIJI_BI, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_NOUNYU_SHIJI_BI, START_ROW + index, COLUMN_EC_NOUNYU_SHIJI_BI, START_ROW + index, aSEList(index).EcNounyuShijibi)
                '納入指示日'
                xls.SetValue(COLUMN_NOUNYU_SHIJI_BI, START_ROW + index, COLUMN_NOUNYU_SHIJI_BI, START_ROW + index, aSEList(index).NounyuShijibi)
                SetLineBody(xls, COLUMN_EC_NOUNYU_SHIJI_BI, START_ROW + index, COLUMN_NOUNYU_SHIJI_BI, START_ROW + index)
                '納入場所EC'
                xls.SetFont(COLUMN_EC_NOUBA, START_ROW + index, COLUMN_EC_NOUBA, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_NOUBA, START_ROW + index, COLUMN_EC_NOUBA, START_ROW + index, aSEList(index).EcNouba)
                '納入場所'
                xls.SetValue(COLUMN_NOUBA, START_ROW + index, COLUMN_NOUBA, START_ROW + index, aSEList(index).Nouba)
                SetLineBody(xls, COLUMN_EC_NOUBA, START_ROW + index, COLUMN_NOUBA, START_ROW + index)
                '供給セクションEC'
                xls.SetFont(COLUMN_EC_KYOKU_SECTION, START_ROW + index, COLUMN_EC_KYOKU_SECTION, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_KYOKU_SECTION, START_ROW + index, COLUMN_EC_KYOKU_SECTION, START_ROW + index, aSEList(index).EcKyoukuSection)
                '供給セクション'
                xls.SetValue(COLUMN_KYOUKU_SECTION, START_ROW + index, COLUMN_KYOUKU_SECTION, START_ROW + index, aSEList(index).KyoukuSection)
                SetLineBody(xls, COLUMN_EC_KYOKU_SECTION, START_ROW + index, COLUMN_KYOUKU_SECTION, START_ROW + index)
                '購入希望単価EC'
                xls.SetFont(COLUMN_EC_KOUNYU_KIBOU_TANKA, START_ROW + index, COLUMN_EC_KOUNYU_KIBOU_TANKA, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_KOUNYU_KIBOU_TANKA, START_ROW + index, COLUMN_EC_KOUNYU_KIBOU_TANKA, START_ROW + index, "")
                '購入希望単価'
                xls.SetValue(COLUMN_KOUNYU_KIBOU_TANKA, START_ROW + index, COLUMN_KOUNYU_KIBOU_TANKA, START_ROW + index, "")
                SetLineBody(xls, COLUMN_EC_KOUNYU_KIBOU_TANKA, START_ROW + index, COLUMN_KOUNYU_KIBOU_TANKA, START_ROW + index)
                '購担EC'
                xls.SetFont(COLUMN_EC_KOUTAN, START_ROW + index, COLUMN_EC_KOUTAN, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_KOUTAN, START_ROW + index, COLUMN_EC_KOUTAN, START_ROW + index, aSEList(index).EcKoutanSection)
                '購担'
                xls.SetValue(COLUMN_KOUTAN, START_ROW + index, COLUMN_KOUTAN, START_ROW + index, aSEList(index).Koutan)
                SetLineBody(xls, COLUMN_EC_KOUTAN, START_ROW + index, COLUMN_KOUTAN, START_ROW + index)
                '取引先EC'
                xls.SetFont(COLUMN_EC_TORIHIKISAKI, START_ROW + index, COLUMN_EC_TORIHIKISAKI, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_TORIHIKISAKI, START_ROW + index, COLUMN_EC_TORIHIKISAKI, START_ROW + index, aSEList(index).EcTorihikisaki)
                '取引先'
                xls.SetValue(COLUMN_TORIHIKISAKI, START_ROW + index, COLUMN_TORIHIKISAKI, START_ROW + index, aSEList(index).Torihikisaki)
                SetLineBody(xls, COLUMN_EC_TORIHIKISAKI, START_ROW + index, COLUMN_TORIHIKISAKI, START_ROW + index)
                'マスタ参照'
                '購担'
                xls.SetValue(COLUMN_MASTER_KOUTAN, START_ROW + index, COLUMN_MASTER_KOUTAN, START_ROW + index, aSEList(index).MasterKoutan)
                SetLineBody(xls, COLUMN_MASTER_KOUTAN, START_ROW + index, COLUMN_MASTER_KOUTAN, START_ROW + index)
                '取引先'
                xls.SetValue(COLUMN_MASTER_TORIHIKISAKI, START_ROW + index, COLUMN_MASTER_TORIHIKISAKI, START_ROW + index, aSEList(index).MasterTorihikisaki)
                SetLineBody(xls, COLUMN_MASTER_TORIHIKISAKI, START_ROW + index, COLUMN_MASTER_TORIHIKISAKI, START_ROW + index)
            Next
        End Sub

        ''' <summary>
        ''' Excel出力　現調品シートのHeaderの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aList">リストコード情報</param>
        ''' <remarks></remarks>
        Public Sub setGenchoSheetHeard(ByVal xls As ShisakuExcel, _
                                  ByVal aList As TShisakuListcodeVo, _
                                  ByVal aEventVo As TShisakuEventVo)

            'TODO 仕様変更に耐えうるようにすべき'
            'ユニット区分用にイベント情報も追加()

            'タイトル'
            xls.MergeCells(1, 1, 8, 1, True)
            xls.SetValue(1, 1, "手配帳エラーリスト(現調品)")
            xls.SetFont(1, 1, 1, 1, "ＭＳ Ｐゴシック", 14, Nothing, True)

            'リストコード'
            xls.SetValue(1, 3, "リストコード")
            xls.SetValue(4, 3, aList.ShisakuListCode)
            xls.SetFont(1, 3, 1, 3, "ＭＳ Ｐゴシック", 11, Nothing, True)
            'イベント名称'
            xls.MergeCells(1, 4, 3, 4, True)
            xls.SetValue(1, 4, "イベント名称")
            xls.SetValue(4, 4, aList.ShisakuEventName)
            xls.SetFont(1, 4, 1, 4, "ＭＳ Ｐゴシック", 11, Nothing, True)
            '工事指令No'
            xls.MergeCells(1, 6, 3, 6, True)
            xls.SetValue(1, 6, "工事指令No")
            xls.SetValue(4, 6, aList.ShisakuKoujiShireiNo)
            xls.SetFont(1, 6, 1, 6, "ＭＳ Ｐゴシック", 11, Nothing, True)
            '工事区分'
            xls.MergeCells(1, 7, 3, 7, True)
            xls.SetValue(1, 7, "工事区分")
            xls.SetValue(4, 7, aList.ShisakuKoujiKbn)
            xls.SetFont(1, 7, 1, 7, "ＭＳ Ｐゴシック", 11, Nothing, True)
            'ユニット区分'
            xls.MergeCells(1, 8, 3, 8, True)
            xls.SetValue(1, 8, "ユニット区分")
            xls.SetValue(4, 8, aEventVo.UnitKbn)
            xls.SetFont(1, 8, 1, 8, "ＭＳ Ｐゴシック", 11, Nothing, True)
            '製品区分'
            xls.MergeCells(1, 9, 3, 9, True)
            xls.SetValue(1, 9, "製品区分")
            xls.SetValue(4, 9, aList.ShisakuSeihinKbn)
            xls.SetFont(1, 9, 1, 9, "ＭＳ Ｐゴシック", 11, Nothing, True)

            '工事指令Noから製品区分まで罫線'
            xls.SetLine(1, 6, 3, 9, XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)
            xls.SetLine(1, 6, 3, 9, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)
            xls.SetLine(1, 6, 3, 9, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)
            xls.SetLine(1, 6, 3, 9, XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)
            xls.SetLine(1, 6, 3, 9, XlBordersIndex.xlInsideHorizontal, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)
            'xls.SetLine(1, 3, 1, 10, XlBordersIndex.xlInsideVertical, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)

            xls.SetLine(4, 6, 4, 9, XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            xls.SetLine(4, 6, 4, 9, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            'xls.SetLine(4, 3, 4, 9, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            xls.SetLine(4, 6, 4, 9, XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            xls.SetLine(4, 6, 4, 9, XlBordersIndex.xlInsideHorizontal, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            'xls.SetLine(4, 3, 4, 10, XlBordersIndex.xlInsideVertical, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)

            '判定コード表'

            xls.SetFont(15, 4, 28, 9, "ＭＳ Ｐゴシック", 10)
            xls.MergeCells(15, 1, 18, 1, True)
            xls.SetValue(15, 1, "◆判定コード表")
            xls.SetFont(15, 2, 15, 2, "ＭＳ Ｐゴシック", 10, RGB(255, 0, 0), True)
            xls.SetValue(15, 2, "E")
            xls.MergeCells(16, 2, 18, 2, True)
            xls.SetValue(16, 2, ": ERROR(発注不可)")
            xls.SetFont(19, 2, 19, 2, "ＭＳ Ｐゴシック", 10, RGB(255, 0, 0), True)
            xls.SetValue(19, 2, "W")
            xls.MergeCells(20, 2, 22, 2, True)
            xls.SetValue(20, 2, ": WARNING(警告:発注可能)")

            xls.SetLine(15, 2, 22, 2, XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            xls.SetLine(15, 2, 22, 2, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            xls.SetLine(15, 2, 22, 2, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            xls.SetLine(15, 2, 22, 2, XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            xls.SetLine(15, 2, 22, 2, XlBordersIndex.xlInsideVertical, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)

            'エラーコード表'

            xls.SetFont(15, 4, 15, 9, "ＭＳ Ｐゴシック", 10, RGB(255, 0, 0), True)

            xls.MergeCells(15, 3, 18, 3, True)
            xls.SetValue(15, 3, "◆エラーコード表")
            xls.SetValue(15, 4, "E01")
            xls.SetValue(15, 5, "E03")
            xls.SetValue(15, 6, "E04")
            xls.SetValue(15, 7, "E05")
            xls.SetValue(15, 8, "E06")
            xls.SetValue(15, 9, "E07")


            xls.MergeCells(16, 4, 18, 4, True)
            xls.MergeCells(16, 5, 18, 5, True)
            xls.MergeCells(16, 6, 18, 6, True)
            xls.MergeCells(16, 7, 18, 7, True)
            xls.MergeCells(16, 8, 18, 8, True)
            xls.MergeCells(16, 9, 18, 9, True)

            xls.SetValue(16, 4, "ブロックNoが未入力")
            xls.SetValue(16, 5, "部品番号が未入力")
            xls.SetValue(16, 6, "部品番号の文字エラー")
            xls.SetValue(16, 7, "部品名称が未入力")
            xls.SetValue(16, 8, "納入指示数が０個")
            xls.SetValue(16, 9, "納入指示日が未入力")

            xls.SetFont(19, 4, 19, 9, "ＭＳ Ｐゴシック", 10, RGB(255, 0, 0), True)

            xls.SetValue(19, 4, "E08")
            xls.SetValue(19, 5, "E09")
            xls.SetValue(19, 6, "E10")
            xls.SetValue(19, 7, "E12")
            xls.SetValue(19, 8, "E16")
            xls.SetValue(19, 9, "E17")


            xls.MergeCells(20, 4, 22, 4, True)
            xls.MergeCells(20, 5, 22, 5, True)
            xls.MergeCells(20, 6, 22, 6, True)
            xls.MergeCells(20, 7, 22, 7, True)
            xls.MergeCells(20, 8, 22, 8, True)
            xls.MergeCells(20, 9, 22, 9, True)

            xls.SetValue(20, 4, "納入指示日が過去日付")
            xls.SetValue(20, 5, "納入指示日が仮伝納期(３日以内)")
            xls.SetValue(20, 6, "納入指示日要注意(５日以内)")
            xls.SetValue(20, 7, "供給セクションが未入力")
            xls.SetValue(20, 8, "購担が未入力")
            xls.SetValue(20, 9, "購担がマスタと不一致")

            xls.SetFont(23, 3, 23, 7, "ＭＳ Ｐゴシック", 10, RGB(255, 0, 0), True)

            xls.SetValue(23, 4, "E18")
            xls.SetValue(23, 5, "E19")
            xls.SetValue(23, 6, "E22")
            xls.SetValue(23, 7, "E31")

            xls.MergeCells(24, 4, 28, 4, True)
            xls.MergeCells(24, 5, 28, 5, True)
            xls.MergeCells(24, 6, 28, 6, True)
            xls.MergeCells(24, 7, 28, 7, True)


            xls.SetValue(24, 4, "取扱先が未入力")
            xls.SetValue(24, 5, "取扱い先がマスタと不一致")
            xls.SetValue(24, 6, "取引先コード要確認(国内コード)")
            xls.SetValue(24, 7, "手配記号と供給先がアンマッチ")


            xls.SetLine(15, 4, 28, 9, XlBordersIndex.xlEdgeTop, XlLineStyle.xlDot, XlBorderWeight.xlThin)
            xls.SetLine(15, 4, 28, 9, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlDot, XlBorderWeight.xlThin)
            xls.SetLine(15, 4, 28, 9, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDot, XlBorderWeight.xlThin)
            xls.SetLine(15, 4, 28, 9, XlBordersIndex.xlEdgeRight, XlLineStyle.xlDot, XlBorderWeight.xlThin)
            xls.SetLine(15, 4, 28, 9, XlBordersIndex.xlInsideVertical, XlLineStyle.xlDot, XlBorderWeight.xlThin)

            xls.MergeCells(31, 1, 32, 1, True)
            xls.MergeCells(31, 2, 32, 2, True)
            xls.SetValue(31, 1, 31, 1, "処理年月日")
            xls.SetValue(31, 2, 31, 2, "担当者")
            xls.MergeCells(33, 1, 34, 1, True)
            xls.MergeCells(33, 2, 34, 2, True)
            xls.SetValue(33, 1, 33, 1, Replace(aList.UpdatedDate, "-", "/"))

            Dim shainName As String
            Dim login As New LoginInfo

            shainName = impl.FindByShainName(login.UserId)

            xls.SetValue(33, 2, 33, 2, shainName)

        End Sub

        ''' <summary>
        ''' Excel出力　現調品シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aGEList">現調品エラー情報リスト</param>
        ''' <remarks></remarks>
        Public Sub setGenchoSheetBody(ByVal xls As ShisakuExcel, ByVal aGEList As List(Of TehaiMenuErrorExcelVo))

            'タイトル部分の作成'
            setGenchoTitleRow(xls)
            Dim dataMatrix(aGEList.Count - 1, COLUMN_GENCHO_MASTER_TORIHIKISAKI) As String

            '中身の作成'
            For index As Integer = 0 To aGEList.Count - 1

                '判定'
                dataMatrix(index, COLUMN_ERROR_HANTEI - 1) = aGEList(index).ErrorKbn

                'ブロックNo.EC'
                dataMatrix(index, COLUMN_EC_BLOCK_NO - 1) = aGEList(index).EcShisakuBlockNo
                'ブロックNo'
                dataMatrix(index, COLUMN_BLOCK_NO - 1) = aGEList(index).ShisakuBlockNo
                '工事No.EC'
                dataMatrix(index, COLUMN_EC_KOUJI_NO - 1) = ""
                '工事No.'
                dataMatrix(index, COLUMN_KOUJI_NO - 1) = aGEList(index).ShisakuKoujiNo
                '行ID'
                dataMatrix(index, COLUMN_ROW_ID - 1) = aGEList(index).GyouId
                '専用マーク'
                dataMatrix(index, COLUMN_SENYO_MARK - 1) = aGEList(index).SenyouMark
                '記'
                dataMatrix(index, COLUMN_TEHAI_KIGOU - 1) = aGEList(index).TehaiKigou
                '部品番号EC'
                dataMatrix(index, COLUMN_EC_BUHIN_NO - 1) = aGEList(index).EcBuhinNo
                '部品番号'
                dataMatrix(index, COLUMN_BUHIN_NO - 1) = aGEList(index).BuhinNo
                '試作区分'
                dataMatrix(index, COLUMN_SHISAKU_KBN - 1) = aGEList(index).BuhinNoKbn
                '部品名称EC'
                dataMatrix(index, COLUMN_EC_BUHIN_NAME - 1) = aGEList(index).EcBuhinName
                '部品名称'
                dataMatrix(index, COLUMN_BUHIN_NAME - 1) = aGEList(index).BuhinName
                '納入指示数EC'
                dataMatrix(index, COLUMN_EC_NOUNYU_SHIJISU - 1) = aGEList(index).EcTotalInsuSuryo
                '納入指示数'
                dataMatrix(index, COLUMN_NOUNYU_SHIJI_SU - 1) = aGEList(index).TotalInsuSuryo
                '納入指示日EC'
                dataMatrix(index, COLUMN_EC_NOUNYU_SHIJI_BI - 1) = aGEList(index).EcNounyuShijibi
                '納入指示日'
                dataMatrix(index, COLUMN_NOUNYU_SHIJI_BI - 1) = aGEList(index).NounyuShijibi
                '購入希望単価EC'
                dataMatrix(index, COLUMN_EC_GENCHO_KOUNYU_KIBOU_TANKA - 1) = aGEList(index).EcNouba
                '購入希望単価'
                dataMatrix(index, COLUMN_GENCHO_KOUNYU_KIBOU_TANKA - 1) = aGEList(index).Nouba
                '購担EC'
                dataMatrix(index, COLUMN_EC_GENCHO_KOUTAN - 1) = aGEList(index).EcKoutanSection
                '購担'
                dataMatrix(index, COLUMN_GENCHO_KOUTAN - 1) = aGEList(index).Koutan
                '取引先EC'
                dataMatrix(index, COLUMN_EC_GENCHO_TORIHIKISAKI - 1) = aGEList(index).EcTorihikisaki
                '取引先'
                dataMatrix(index, COLUMN_GENCHO_TORIHIKISAKI - 1) = aGEList(index).Torihikisaki
                '供給セクションEC'
                dataMatrix(index, COLUMN_EC_GENCHO_KYOKU_SECTION - 1) = aGEList(index).EcKyoukuSection
                '供給セクション'
                dataMatrix(index, COLUMN_GENCHO_KYOKU_SECTION - 1) = aGEList(index).KyoukuSection
                '輸送会社EC'
                dataMatrix(index, COLUMN_EC_GENCHO_YUSO_KAISHA - 1) = ""
                '輸送会社'
                dataMatrix(index, COLUMN_GENCHO_YUSO_KAISHA - 1) = ""
                '現地セクションEC'
                dataMatrix(index, COLUMN_EC_GENCHO_GENCHI_KYOKU_SECTION - 1) = ""

                '現地セクション'
                dataMatrix(index, COLUMN_GENCHO_GENCHI_KYOKU_SECTION - 1) = ""
                '配送日EC'
                dataMatrix(index, COLUMN_EC_GENCHO_HAISO_BI - 1) = ""
                '配送日'
                dataMatrix(index, COLUMN_GENCHO_HAISO_BI - 1) = ""
                '配送先EC'
                dataMatrix(index, COLUMN_EC_GENCHO_HAISO_SAKI - 1) = ""
                '配送先'
                dataMatrix(index, COLUMN_GENCHO_HAISO_SAKI - 1) = ""

                'マスタ参照'
                '購担'
                dataMatrix(index, COLUMN_GENCHO_MASTER_KOUTAN - 1) = aGEList(index).MasterKoutan
                '取引先'
                dataMatrix(index, COLUMN_GENCHO_MASTER_TORIHIKISAKI - 1) = aGEList(index).MasterTorihikisaki






            Next
            SetLineBody(xls, COLUMN_ERROR_HANTEI, START_ROW, COLUMN_ERROR_HANTEI, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_EC_BLOCK_NO, START_ROW, COLUMN_BLOCK_NO, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_EC_KOUJI_NO, START_ROW, COLUMN_KOUJI_NO, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_ROW_ID, START_ROW, COLUMN_ROW_ID, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_SENYO_MARK, START_ROW, COLUMN_SENYO_MARK, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_TEHAI_KIGOU, START_ROW, COLUMN_TEHAI_KIGOU, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_EC_BUHIN_NO, START_ROW, COLUMN_BUHIN_NO, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_SHISAKU_KBN, START_ROW, COLUMN_SHISAKU_KBN, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_EC_BUHIN_NAME, START_ROW, COLUMN_BUHIN_NAME, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_EC_NOUNYU_SHIJISU, START_ROW, COLUMN_NOUNYU_SHIJI_SU, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_EC_NOUNYU_SHIJI_BI, START_ROW, COLUMN_NOUNYU_SHIJI_BI, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_EC_GENCHO_KOUNYU_KIBOU_TANKA, START_ROW, COLUMN_GENCHO_KOUNYU_KIBOU_TANKA, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_EC_GENCHO_KOUTAN, START_ROW, COLUMN_GENCHO_KOUTAN, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_EC_GENCHO_TORIHIKISAKI, START_ROW, COLUMN_EC_GENCHO_TORIHIKISAKI, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_EC_GENCHO_TORIHIKISAKI, START_ROW, COLUMN_GENCHO_TORIHIKISAKI, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_EC_GENCHO_KYOKU_SECTION, START_ROW, COLUMN_GENCHO_KYOKU_SECTION, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_EC_GENCHO_YUSO_KAISHA, START_ROW, COLUMN_GENCHO_YUSO_KAISHA, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_EC_GENCHO_GENCHI_KYOKU_SECTION, START_ROW, COLUMN_GENCHO_GENCHI_KYOKU_SECTION, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_EC_GENCHO_HAISO_BI, START_ROW, COLUMN_GENCHO_HAISO_BI, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_EC_GENCHO_HAISO_SAKI, START_ROW, COLUMN_GENCHO_HAISO_SAKI, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_GENCHO_MASTER_KOUTAN, START_ROW, COLUMN_GENCHO_MASTER_KOUTAN, START_ROW + aGEList.Count)
            SetLineBody(xls, COLUMN_GENCHO_MASTER_TORIHIKISAKI, START_ROW, COLUMN_GENCHO_MASTER_TORIHIKISAKI, START_ROW + aGEList.Count)


            xls.SetFont(COLUMN_EC_BLOCK_NO, START_ROW, COLUMN_EC_BLOCK_NO, START_ROW + aGEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_KOUJI_NO, START_ROW, COLUMN_EC_KOUJI_NO, START_ROW + aGEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_BUHIN_NO, START_ROW, COLUMN_EC_BUHIN_NO, START_ROW + aGEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_BUHIN_NAME, START_ROW, COLUMN_EC_BUHIN_NAME, START_ROW + aGEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_NOUNYU_SHIJISU, START_ROW, COLUMN_EC_NOUNYU_SHIJISU, START_ROW + aGEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_NOUNYU_SHIJI_BI, START_ROW, COLUMN_EC_NOUNYU_SHIJI_BI, START_ROW + aGEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_GENCHO_KOUNYU_KIBOU_TANKA, START_ROW, COLUMN_EC_GENCHO_KOUNYU_KIBOU_TANKA, START_ROW + aGEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_GENCHO_KOUTAN, START_ROW, COLUMN_EC_GENCHO_KOUTAN, START_ROW + aGEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_GENCHO_TORIHIKISAKI, START_ROW, COLUMN_EC_GENCHO_TORIHIKISAKI, START_ROW + aGEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_GENCHO_KYOKU_SECTION, START_ROW, COLUMN_EC_GENCHO_KYOKU_SECTION, START_ROW + aGEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_GENCHO_YUSO_KAISHA, START_ROW, COLUMN_EC_GENCHO_YUSO_KAISHA, START_ROW + aGEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_GENCHO_GENCHI_KYOKU_SECTION, START_ROW, COLUMN_EC_GENCHO_GENCHI_KYOKU_SECTION, START_ROW + aGEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_GENCHO_HAISO_BI, START_ROW, COLUMN_EC_GENCHO_HAISO_BI, START_ROW + aGEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.SetFont(COLUMN_EC_GENCHO_HAISO_SAKI, START_ROW, COLUMN_EC_GENCHO_HAISO_SAKI, START_ROW + aGEList.Count, "ＭＳ Ｐゴシック", 11, RGB(255, 0, 0), True)
            xls.CopyRange(0, START_ROW, UBound(dataMatrix, 2) - 1, START_ROW + UBound(dataMatrix), dataMatrix)

        End Sub
        ''' <summary>
        ''' Excel出力　現調品シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aGEList">現調品エラー情報リスト</param>
        ''' <remarks></remarks>
        Public Sub setGenchoSheetBodyOLD(ByVal xls As ShisakuExcel, ByVal aGEList As List(Of TehaiMenuErrorExcelVo))

            'タイトル部分の作成'
            setGenchoTitleRow(xls)

            '中身の作成'
            For index As Integer = 0 To aGEList.Count - 1
                '判定'
                xls.SetValue(COLUMN_ERROR_HANTEI, START_ROW + index, COLUMN_ERROR_HANTEI, START_ROW + index, aGEList(index).ErrorKbn)
                SetLineBody(xls, COLUMN_ERROR_HANTEI, START_ROW + index, COLUMN_ERROR_HANTEI, START_ROW + index)
                'ブロックNo.EC'
                xls.SetFont(COLUMN_EC_BLOCK_NO, START_ROW + index, COLUMN_EC_BLOCK_NO, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_BLOCK_NO, START_ROW + index, COLUMN_EC_BLOCK_NO, START_ROW + index, aGEList(index).EcShisakuBlockNo)
                'ブロックNo'
                xls.SetValue(COLUMN_BLOCK_NO, START_ROW + index, COLUMN_BLOCK_NO, START_ROW + index, aGEList(index).ShisakuBlockNo)
                SetLineBody(xls, COLUMN_EC_BLOCK_NO, START_ROW + index, COLUMN_BLOCK_NO, START_ROW + index)
                '工事No.EC'
                xls.SetFont(COLUMN_EC_KOUJI_NO, START_ROW + index, COLUMN_EC_KOUJI_NO, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_KOUJI_NO, START_ROW + index, COLUMN_EC_KOUJI_NO, START_ROW + index, "")
                '工事No.'
                xls.SetValue(COLUMN_KOUJI_NO, START_ROW + index, COLUMN_KOUJI_NO, START_ROW + index, aGEList(index).ShisakuKoujiNo)
                SetLineBody(xls, COLUMN_EC_KOUJI_NO, START_ROW + index, COLUMN_KOUJI_NO, START_ROW + index)
                '行ID'
                xls.SetValue(COLUMN_ROW_ID, START_ROW + index, COLUMN_ROW_ID, START_ROW + index, aGEList(index).GyouId)
                SetLineBody(xls, COLUMN_ROW_ID, START_ROW + index, COLUMN_ROW_ID, START_ROW + index)
                '専用マーク'
                xls.SetValue(COLUMN_SENYO_MARK, START_ROW + index, COLUMN_SENYO_MARK, START_ROW + index, aGEList(index).SenyouMark)
                SetLineBody(xls, COLUMN_SENYO_MARK, START_ROW + index, COLUMN_SENYO_MARK, START_ROW + index)
                '記'
                xls.SetValue(COLUMN_TEHAI_KIGOU, START_ROW + index, COLUMN_TEHAI_KIGOU, START_ROW + index, aGEList(index).TehaiKigou)
                SetLineBody(xls, COLUMN_TEHAI_KIGOU, START_ROW + index, COLUMN_TEHAI_KIGOU, START_ROW + index)
                '部品番号EC'
                xls.SetFont(COLUMN_EC_BUHIN_NO, START_ROW + index, COLUMN_EC_BUHIN_NO, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_BUHIN_NO, START_ROW + index, COLUMN_EC_BUHIN_NO, START_ROW + index, aGEList(index).EcBuhinNo)
                '部品番号'
                xls.SetValue(COLUMN_BUHIN_NO, START_ROW + index, COLUMN_BUHIN_NO, START_ROW + index, aGEList(index).BuhinNo)
                SetLineBody(xls, COLUMN_EC_BUHIN_NO, START_ROW + index, COLUMN_BUHIN_NO, START_ROW + index)
                '試作区分'
                xls.SetValue(COLUMN_SHISAKU_KBN, START_ROW + index, COLUMN_SHISAKU_KBN, START_ROW + index, aGEList(index).BuhinNoKbn)
                SetLineBody(xls, COLUMN_SHISAKU_KBN, START_ROW + index, COLUMN_SHISAKU_KBN, START_ROW + index)
                '部品名称EC'
                xls.SetFont(COLUMN_EC_BUHIN_NAME, START_ROW + index, COLUMN_EC_BUHIN_NAME, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_BUHIN_NAME, START_ROW + index, COLUMN_EC_BUHIN_NAME, START_ROW + index, aGEList(index).EcBuhinName)
                '部品名称'
                xls.SetValue(COLUMN_BUHIN_NAME, START_ROW + index, COLUMN_BUHIN_NAME, START_ROW + index, aGEList(index).BuhinName)
                SetLineBody(xls, COLUMN_EC_BUHIN_NAME, START_ROW + index, COLUMN_BUHIN_NAME, START_ROW + index)
                '納入指示数EC'
                xls.SetFont(COLUMN_EC_NOUNYU_SHIJISU, START_ROW + index, COLUMN_EC_NOUNYU_SHIJISU, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                       RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_NOUNYU_SHIJISU, START_ROW + index, COLUMN_EC_NOUNYU_SHIJISU, START_ROW + index, aGEList(index).EcTotalInsuSuryo)
                '納入指示数'
                xls.SetValue(COLUMN_NOUNYU_SHIJI_SU, START_ROW + index, COLUMN_NOUNYU_SHIJI_SU, START_ROW + index, aGEList(index).TotalInsuSuryo)
                SetLineBody(xls, COLUMN_EC_NOUNYU_SHIJISU, START_ROW + index, COLUMN_NOUNYU_SHIJI_SU, START_ROW + index)
                '納入指示日EC'
                xls.SetFont(COLUMN_EC_NOUNYU_SHIJI_BI, START_ROW + index, COLUMN_EC_NOUNYU_SHIJI_BI, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_NOUNYU_SHIJI_BI, START_ROW + index, COLUMN_EC_NOUNYU_SHIJI_BI, START_ROW + index, aGEList(index).EcNounyuShijibi)
                '納入指示日'
                xls.SetValue(COLUMN_NOUNYU_SHIJI_BI, START_ROW + index, COLUMN_NOUNYU_SHIJI_BI, START_ROW + index, aGEList(index).NounyuShijibi)
                SetLineBody(xls, COLUMN_EC_NOUNYU_SHIJI_BI, START_ROW + index, COLUMN_NOUNYU_SHIJI_BI, START_ROW + index)
                '購入希望単価EC'
                xls.SetFont(COLUMN_EC_GENCHO_KOUNYU_KIBOU_TANKA, START_ROW + index, COLUMN_EC_GENCHO_KOUNYU_KIBOU_TANKA, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_GENCHO_KOUNYU_KIBOU_TANKA, START_ROW + index, COLUMN_EC_GENCHO_KOUNYU_KIBOU_TANKA, START_ROW + index, aGEList(index).EcNouba)
                '購入希望単価'
                xls.SetValue(COLUMN_GENCHO_KOUNYU_KIBOU_TANKA, START_ROW + index, COLUMN_GENCHO_KOUNYU_KIBOU_TANKA, START_ROW + index, aGEList(index).Nouba)
                SetLineBody(xls, COLUMN_EC_GENCHO_KOUNYU_KIBOU_TANKA, START_ROW + index, COLUMN_GENCHO_KOUNYU_KIBOU_TANKA, START_ROW + index)
                '購担EC'
                xls.SetFont(COLUMN_EC_GENCHO_KOUTAN, START_ROW + index, COLUMN_EC_GENCHO_KOUTAN, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_GENCHO_KOUTAN, START_ROW + index, COLUMN_EC_GENCHO_KOUTAN, START_ROW + index, aGEList(index).EcKoutanSection)
                '購担'
                xls.SetValue(COLUMN_GENCHO_KOUTAN, START_ROW + index, COLUMN_GENCHO_KOUTAN, START_ROW + index, aGEList(index).Koutan)
                SetLineBody(xls, COLUMN_EC_GENCHO_KOUTAN, START_ROW + index, COLUMN_GENCHO_KOUTAN, START_ROW + index)
                '取引先EC'
                xls.SetFont(COLUMN_EC_GENCHO_TORIHIKISAKI, START_ROW + index, COLUMN_EC_GENCHO_TORIHIKISAKI, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_GENCHO_TORIHIKISAKI, START_ROW + index, COLUMN_EC_GENCHO_TORIHIKISAKI, START_ROW + index, aGEList(index).EcTorihikisaki)
                SetLineBody(xls, COLUMN_EC_GENCHO_TORIHIKISAKI, START_ROW + index, COLUMN_EC_GENCHO_TORIHIKISAKI, START_ROW + index)
                '取引先'
                xls.SetValue(COLUMN_GENCHO_TORIHIKISAKI, START_ROW + index, COLUMN_GENCHO_TORIHIKISAKI, START_ROW + index, aGEList(index).Torihikisaki)
                SetLineBody(xls, COLUMN_EC_GENCHO_TORIHIKISAKI, START_ROW + index, COLUMN_GENCHO_TORIHIKISAKI, START_ROW + index)
                '供給セクションEC'
                xls.SetFont(COLUMN_EC_GENCHO_KYOKU_SECTION, START_ROW + index, COLUMN_EC_GENCHO_KYOKU_SECTION, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_GENCHO_KYOKU_SECTION, START_ROW + index, COLUMN_EC_GENCHO_KYOKU_SECTION, START_ROW + index, aGEList(index).EcKyoukuSection)
                '供給セクション'
                xls.SetValue(COLUMN_GENCHO_KYOKU_SECTION, START_ROW + index, COLUMN_GENCHO_KYOKU_SECTION, START_ROW + index, aGEList(index).KyoukuSection)
                SetLineBody(xls, COLUMN_EC_GENCHO_KYOKU_SECTION, START_ROW + index, COLUMN_GENCHO_KYOKU_SECTION, START_ROW + index)
                '輸送会社EC'
                xls.SetFont(COLUMN_EC_GENCHO_YUSO_KAISHA, START_ROW + index, COLUMN_EC_GENCHO_YUSO_KAISHA, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_GENCHO_YUSO_KAISHA, START_ROW + index, COLUMN_EC_GENCHO_YUSO_KAISHA, START_ROW + index, "")
                '輸送会社'
                xls.SetValue(COLUMN_GENCHO_YUSO_KAISHA, START_ROW + index, COLUMN_GENCHO_YUSO_KAISHA, START_ROW + index, "")
                SetLineBody(xls, COLUMN_EC_GENCHO_YUSO_KAISHA, START_ROW + index, COLUMN_GENCHO_YUSO_KAISHA, START_ROW + index)
                '現地セクションEC'
                xls.SetFont(COLUMN_EC_GENCHO_GENCHI_KYOKU_SECTION, START_ROW + index, COLUMN_EC_GENCHO_GENCHI_KYOKU_SECTION, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_GENCHO_GENCHI_KYOKU_SECTION, START_ROW + index, COLUMN_EC_GENCHO_GENCHI_KYOKU_SECTION, START_ROW + index, "")

                '現地セクション'
                xls.SetValue(COLUMN_GENCHO_GENCHI_KYOKU_SECTION, START_ROW + index, COLUMN_GENCHO_GENCHI_KYOKU_SECTION, START_ROW + index, "")
                SetLineBody(xls, COLUMN_EC_GENCHO_GENCHI_KYOKU_SECTION, START_ROW + index, COLUMN_GENCHO_GENCHI_KYOKU_SECTION, START_ROW + index)
                '配送日EC'
                xls.SetFont(COLUMN_EC_GENCHO_HAISO_BI, START_ROW + index, COLUMN_EC_GENCHO_HAISO_BI, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_GENCHO_HAISO_BI, START_ROW + index, COLUMN_EC_GENCHO_HAISO_BI, START_ROW + index, "")
                '配送日'
                xls.SetValue(COLUMN_GENCHO_HAISO_BI, START_ROW + index, COLUMN_GENCHO_HAISO_BI, START_ROW + index, "")
                SetLineBody(xls, COLUMN_EC_GENCHO_HAISO_BI, START_ROW + index, COLUMN_GENCHO_HAISO_BI, START_ROW + index)
                '配送先EC'
                xls.SetFont(COLUMN_EC_GENCHO_HAISO_SAKI, START_ROW + index, COLUMN_EC_GENCHO_HAISO_SAKI, START_ROW + index, "ＭＳ Ｐゴシック", 11, _
                            RGB(255, 0, 0), True)
                xls.SetValue(COLUMN_EC_GENCHO_HAISO_SAKI, START_ROW + index, COLUMN_EC_GENCHO_HAISO_SAKI, START_ROW + index, "")
                '配送先'
                xls.SetValue(COLUMN_GENCHO_HAISO_SAKI, START_ROW + index, COLUMN_GENCHO_HAISO_SAKI, START_ROW + index, "")
                SetLineBody(xls, COLUMN_EC_GENCHO_HAISO_SAKI, START_ROW + index, COLUMN_GENCHO_HAISO_SAKI, START_ROW + index)

                'マスタ参照'
                '購担'
                xls.SetValue(COLUMN_GENCHO_MASTER_KOUTAN, START_ROW + index, COLUMN_GENCHO_MASTER_KOUTAN, START_ROW + index, aGEList(index).MasterKoutan)
                SetLineBody(xls, COLUMN_GENCHO_MASTER_KOUTAN, START_ROW + index, COLUMN_GENCHO_MASTER_KOUTAN, START_ROW + index)
                '取引先'
                xls.SetValue(COLUMN_GENCHO_MASTER_TORIHIKISAKI, START_ROW + index, COLUMN_GENCHO_MASTER_TORIHIKISAKI, START_ROW + index, aGEList(index).MasterTorihikisaki)
                SetLineBody(xls, COLUMN_GENCHO_MASTER_TORIHIKISAKI, START_ROW + index, COLUMN_GENCHO_MASTER_TORIHIKISAKI, START_ROW + index)
            Next
        End Sub

        '新調達タイトル行の作成'
        Private Sub setShinchotastuTitleRow(ByVal xls As ShisakuExcel)
            '判定'
            SetLineTitle(xls, COLUMN_ERROR_HANTEI, TITLE_ROW, COLUMN_ERROR_HANTEI, TITLE_ROW)
            xls.SetFont(COLUMN_ERROR_HANTEI, TITLE_ROW, COLUMN_ERROR_HANTEI, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_ERROR_HANTEI, TITLE_ROW, COLUMN_ERROR_HANTEI, TITLE_ROW, "判定")

            'ブロックNo'
            SetLineTitle(xls, COLUMN_EC_BLOCK_NO, TITLE_ROW, COLUMN_BLOCK_NO, TITLE_ROW)
            xls.SetFont(COLUMN_EC_BLOCK_NO, TITLE_ROW, COLUMN_EC_BLOCK_NO, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_BLOCK_NO, TITLE_ROW, COLUMN_EC_BLOCK_NO, TITLE_ROW, "ＥＣ")

            xls.SetFont(COLUMN_BLOCK_NO, TITLE_ROW, COLUMN_BLOCK_NO, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_BLOCK_NO, TITLE_ROW, COLUMN_BLOCK_NO, TITLE_ROW, "ブロック")


            '工事No'
            SetLineTitle(xls, COLUMN_EC_KOUJI_NO, TITLE_ROW, COLUMN_KOUJI_NO, TITLE_ROW)
            xls.SetFont(COLUMN_EC_KOUJI_NO, TITLE_ROW, COLUMN_EC_KOUJI_NO, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_KOUJI_NO, TITLE_ROW, COLUMN_EC_KOUJI_NO, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_KOUJI_NO, TITLE_ROW, COLUMN_KOUJI_NO, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_KOUJI_NO, TITLE_ROW, COLUMN_KOUJI_NO, TITLE_ROW, "工事No.")

            '行ID'
            SetLineTitle(xls, COLUMN_ROW_ID, TITLE_ROW, COLUMN_ROW_ID, TITLE_ROW)
            xls.SetFont(COLUMN_ROW_ID, TITLE_ROW, COLUMN_ROW_ID, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_ROW_ID, TITLE_ROW, COLUMN_ROW_ID, TITLE_ROW, "行ID")

            '専用マーク'
            SetLineTitle(xls, COLUMN_SENYO_MARK, TITLE_ROW, COLUMN_SENYO_MARK, TITLE_ROW)
            xls.SetFont(COLUMN_SENYO_MARK, TITLE_ROW, COLUMN_SENYO_MARK, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_SENYO_MARK, TITLE_ROW, COLUMN_SENYO_MARK, TITLE_ROW, "専")

            '手配記号'
            SetLineTitle(xls, COLUMN_TEHAI_KIGOU, TITLE_ROW, COLUMN_TEHAI_KIGOU, TITLE_ROW)
            xls.SetFont(COLUMN_TEHAI_KIGOU, TITLE_ROW, COLUMN_TEHAI_KIGOU, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_TEHAI_KIGOU, TITLE_ROW, COLUMN_TEHAI_KIGOU, TITLE_ROW, "記")

            '部品番号'
            SetLineTitle(xls, COLUMN_EC_BUHIN_NO, TITLE_ROW, COLUMN_BUHIN_NO, TITLE_ROW)
            xls.SetFont(COLUMN_EC_BUHIN_NO, TITLE_ROW, COLUMN_EC_BUHIN_NO, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_BUHIN_NO, TITLE_ROW, COLUMN_EC_BUHIN_NO, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_BUHIN_NO, TITLE_ROW, COLUMN_BUHIN_NO, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_BUHIN_NO, TITLE_ROW, COLUMN_BUHIN_NO, TITLE_ROW, "部品番号")

            '試作区分'
            SetLineTitle(xls, COLUMN_SHISAKU_KBN, TITLE_ROW, COLUMN_SHISAKU_KBN, TITLE_ROW)
            xls.SetFont(COLUMN_SHISAKU_KBN, TITLE_ROW, COLUMN_SHISAKU_KBN, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_SHISAKU_KBN, TITLE_ROW, COLUMN_SHISAKU_KBN, TITLE_ROW, "試作区分")

            '部品名称'
            SetLineTitle(xls, COLUMN_EC_BUHIN_NAME, TITLE_ROW, COLUMN_BUHIN_NAME, TITLE_ROW)
            xls.SetFont(COLUMN_EC_BUHIN_NAME, TITLE_ROW, COLUMN_EC_BUHIN_NAME, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_BUHIN_NAME, TITLE_ROW, COLUMN_EC_BUHIN_NAME, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_BUHIN_NAME, TITLE_ROW, COLUMN_BUHIN_NAME, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_BUHIN_NAME, TITLE_ROW, COLUMN_BUHIN_NAME, TITLE_ROW, "部品名称")

            '納入指示数'
            SetLineTitle(xls, COLUMN_EC_NOUNYU_SHIJISU, TITLE_ROW, COLUMN_NOUNYU_SHIJI_SU, TITLE_ROW)
            xls.SetFont(COLUMN_EC_NOUNYU_SHIJISU, TITLE_ROW, COLUMN_EC_NOUNYU_SHIJISU, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_NOUNYU_SHIJISU, TITLE_ROW, COLUMN_EC_NOUNYU_SHIJISU, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_NOUNYU_SHIJI_SU, TITLE_ROW, COLUMN_NOUNYU_SHIJI_SU, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_NOUNYU_SHIJI_SU, TITLE_ROW, COLUMN_NOUNYU_SHIJI_SU, TITLE_ROW, "納入指示数")

            '納入指示日'
            SetLineTitle(xls, COLUMN_EC_NOUNYU_SHIJI_BI, TITLE_ROW, COLUMN_NOUNYU_SHIJI_BI, TITLE_ROW)
            xls.SetFont(COLUMN_EC_NOUNYU_SHIJI_BI, TITLE_ROW, COLUMN_EC_NOUNYU_SHIJI_BI, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_NOUNYU_SHIJI_BI, TITLE_ROW, COLUMN_EC_NOUNYU_SHIJI_BI, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_NOUNYU_SHIJI_BI, TITLE_ROW, COLUMN_NOUNYU_SHIJI_BI, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_NOUNYU_SHIJI_BI, TITLE_ROW, COLUMN_NOUNYU_SHIJI_BI, TITLE_ROW, "納入指示日")

            '納入場所'
            SetLineTitle(xls, COLUMN_EC_NOUBA, TITLE_ROW, COLUMN_NOUBA, TITLE_ROW)
            xls.SetFont(COLUMN_EC_NOUBA, TITLE_ROW, COLUMN_EC_NOUBA, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_NOUBA, TITLE_ROW, COLUMN_EC_NOUBA, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_NOUBA, TITLE_ROW, COLUMN_NOUBA, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_NOUBA, TITLE_ROW, COLUMN_NOUBA, TITLE_ROW, "納入場所")

            '供給セクション'
            SetLineTitle(xls, COLUMN_EC_KYOKU_SECTION, TITLE_ROW, COLUMN_KYOUKU_SECTION, TITLE_ROW)
            xls.SetFont(COLUMN_EC_KYOKU_SECTION, TITLE_ROW, COLUMN_EC_KYOKU_SECTION, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_KYOKU_SECTION, TITLE_ROW, COLUMN_EC_KYOKU_SECTION, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_KYOUKU_SECTION, TITLE_ROW, COLUMN_KYOUKU_SECTION, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_KYOUKU_SECTION, TITLE_ROW, COLUMN_KYOUKU_SECTION, TITLE_ROW, "供給セクション")

            '購入希望単価'
            SetLineTitle(xls, COLUMN_EC_KOUNYU_KIBOU_TANKA, TITLE_ROW, COLUMN_KOUNYU_KIBOU_TANKA, TITLE_ROW)
            xls.SetFont(COLUMN_EC_KOUNYU_KIBOU_TANKA, TITLE_ROW, COLUMN_EC_KOUNYU_KIBOU_TANKA, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_KOUNYU_KIBOU_TANKA, TITLE_ROW, COLUMN_EC_KOUNYU_KIBOU_TANKA, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_KOUNYU_KIBOU_TANKA, TITLE_ROW, COLUMN_KOUNYU_KIBOU_TANKA, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_KOUNYU_KIBOU_TANKA, TITLE_ROW, COLUMN_KOUNYU_KIBOU_TANKA, TITLE_ROW, "購入希望単価")

            '購担'
            SetLineTitle(xls, COLUMN_EC_KOUTAN, TITLE_ROW, COLUMN_KOUTAN, TITLE_ROW)
            xls.SetFont(COLUMN_EC_KOUTAN, TITLE_ROW, COLUMN_EC_KOUTAN, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_KOUTAN, TITLE_ROW, COLUMN_EC_KOUTAN, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_KOUTAN, TITLE_ROW, COLUMN_KOUTAN, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_KOUTAN, TITLE_ROW, COLUMN_KOUTAN, TITLE_ROW, "購担")

            '取引先'
            SetLineTitle(xls, COLUMN_EC_TORIHIKISAKI, TITLE_ROW, COLUMN_TORIHIKISAKI, TITLE_ROW)
            xls.SetFont(COLUMN_EC_TORIHIKISAKI, TITLE_ROW, COLUMN_EC_TORIHIKISAKI, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_TORIHIKISAKI, TITLE_ROW, COLUMN_EC_TORIHIKISAKI, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_TORIHIKISAKI, TITLE_ROW, COLUMN_TORIHIKISAKI, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_TORIHIKISAKI, TITLE_ROW, COLUMN_TORIHIKISAKI, TITLE_ROW, "取引先")

            'マスタ参照'
            xls.MergeCells(COLUMN_MASTER_KOUTAN, 10, COLUMN_MASTER_TORIHIKISAKI, 10, True)
            SetLineTitle(xls, COLUMN_MASTER_KOUTAN, 10, COLUMN_MASTER_TORIHIKISAKI, 10)
            xls.SetFont(COLUMN_MASTER_KOUTAN, 10, COLUMN_MASTER_KOUTAN, 10, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_MASTER_KOUTAN, 10, COLUMN_MASTER_KOUTAN, 10, "マスタ参照")

            'マスタ参照・購担'
            SetLineTitle(xls, COLUMN_MASTER_KOUTAN, TITLE_ROW, COLUMN_MASTER_KOUTAN, TITLE_ROW)
            xls.SetFont(COLUMN_MASTER_KOUTAN, TITLE_ROW, COLUMN_MASTER_KOUTAN, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_MASTER_KOUTAN, TITLE_ROW, COLUMN_MASTER_KOUTAN, TITLE_ROW, "購担")

            'マスタ参照・取引先'
            SetLineTitle(xls, COLUMN_MASTER_TORIHIKISAKI, TITLE_ROW, COLUMN_MASTER_TORIHIKISAKI, TITLE_ROW)
            xls.SetFont(COLUMN_MASTER_TORIHIKISAKI, TITLE_ROW, COLUMN_MASTER_TORIHIKISAKI, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_MASTER_TORIHIKISAKI, TITLE_ROW, COLUMN_MASTER_TORIHIKISAKI, TITLE_ROW, "取引先")

        End Sub

        '現調品タイトル行の作成'
        Private Sub setGenchoTitleRow(ByVal xls As ShisakuExcel)
            '判定'
            SetLineTitle(xls, COLUMN_ERROR_HANTEI, TITLE_ROW, COLUMN_ERROR_HANTEI, TITLE_ROW)
            xls.SetFont(COLUMN_ERROR_HANTEI, TITLE_ROW, COLUMN_ERROR_HANTEI, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_ERROR_HANTEI, TITLE_ROW, COLUMN_ERROR_HANTEI, TITLE_ROW, "判定")

            'ブロックNo'
            SetLineTitle(xls, COLUMN_EC_BLOCK_NO, TITLE_ROW, COLUMN_BLOCK_NO, TITLE_ROW)
            xls.SetFont(COLUMN_EC_BLOCK_NO, TITLE_ROW, COLUMN_EC_BLOCK_NO, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_BLOCK_NO, TITLE_ROW, COLUMN_EC_BLOCK_NO, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_BLOCK_NO, TITLE_ROW, COLUMN_BLOCK_NO, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_BLOCK_NO, TITLE_ROW, COLUMN_BLOCK_NO, TITLE_ROW, "ブロック")

            '工事No'
            SetLineTitle(xls, COLUMN_EC_KOUJI_NO, TITLE_ROW, COLUMN_KOUJI_NO, TITLE_ROW)
            xls.SetFont(COLUMN_EC_KOUJI_NO, TITLE_ROW, COLUMN_EC_KOUJI_NO, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_KOUJI_NO, TITLE_ROW, COLUMN_EC_KOUJI_NO, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_KOUJI_NO, TITLE_ROW, COLUMN_KOUJI_NO, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_KOUJI_NO, TITLE_ROW, COLUMN_KOUJI_NO, TITLE_ROW, "工事No.")

            '行ID'
            SetLineTitle(xls, COLUMN_ROW_ID, TITLE_ROW, COLUMN_ROW_ID, TITLE_ROW)
            xls.SetFont(COLUMN_ROW_ID, TITLE_ROW, COLUMN_ROW_ID, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_ROW_ID, TITLE_ROW, COLUMN_ROW_ID, TITLE_ROW, "行ID")

            '専用マーク'
            SetLineTitle(xls, COLUMN_SENYO_MARK, TITLE_ROW, COLUMN_SENYO_MARK, TITLE_ROW)
            xls.SetFont(COLUMN_SENYO_MARK, TITLE_ROW, COLUMN_SENYO_MARK, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_SENYO_MARK, TITLE_ROW, COLUMN_SENYO_MARK, TITLE_ROW, "専")

            '手配記号'
            SetLineTitle(xls, COLUMN_TEHAI_KIGOU, TITLE_ROW, COLUMN_TEHAI_KIGOU, TITLE_ROW)
            xls.SetFont(COLUMN_TEHAI_KIGOU, TITLE_ROW, COLUMN_TEHAI_KIGOU, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_TEHAI_KIGOU, TITLE_ROW, COLUMN_TEHAI_KIGOU, TITLE_ROW, "記")

            '部品番号'
            SetLineTitle(xls, COLUMN_EC_BUHIN_NO, TITLE_ROW, COLUMN_BUHIN_NO, TITLE_ROW)
            xls.SetFont(COLUMN_EC_BUHIN_NO, TITLE_ROW, COLUMN_EC_BUHIN_NO, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_BUHIN_NO, TITLE_ROW, COLUMN_EC_BUHIN_NO, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_BUHIN_NO, TITLE_ROW, COLUMN_BUHIN_NO, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_BUHIN_NO, TITLE_ROW, COLUMN_BUHIN_NO, TITLE_ROW, "部品番号")

            '試作区分'
            SetLineTitle(xls, COLUMN_SHISAKU_KBN, TITLE_ROW, COLUMN_SHISAKU_KBN, TITLE_ROW)
            xls.SetFont(COLUMN_SHISAKU_KBN, TITLE_ROW, COLUMN_SHISAKU_KBN, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_SHISAKU_KBN, TITLE_ROW, COLUMN_SHISAKU_KBN, TITLE_ROW, "試作区分")

            '部品名称'
            SetLineTitle(xls, COLUMN_EC_BUHIN_NAME, TITLE_ROW, COLUMN_BUHIN_NAME, TITLE_ROW)
            xls.SetFont(COLUMN_EC_BUHIN_NAME, TITLE_ROW, COLUMN_EC_BUHIN_NAME, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_BUHIN_NAME, TITLE_ROW, COLUMN_EC_BUHIN_NAME, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_BUHIN_NAME, TITLE_ROW, COLUMN_BUHIN_NAME, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_BUHIN_NAME, TITLE_ROW, COLUMN_BUHIN_NAME, TITLE_ROW, "部品名称")

            '納入指示数'
            SetLineTitle(xls, COLUMN_EC_NOUNYU_SHIJISU, TITLE_ROW, COLUMN_NOUNYU_SHIJI_SU, TITLE_ROW)
            xls.SetFont(COLUMN_EC_NOUNYU_SHIJISU, TITLE_ROW, COLUMN_EC_NOUNYU_SHIJISU, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_NOUNYU_SHIJISU, TITLE_ROW, COLUMN_EC_NOUNYU_SHIJISU, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_NOUNYU_SHIJI_SU, TITLE_ROW, COLUMN_NOUNYU_SHIJI_SU, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_NOUNYU_SHIJI_SU, TITLE_ROW, COLUMN_NOUNYU_SHIJI_SU, TITLE_ROW, "納入指示数")

            '納入指示日'
            SetLineTitle(xls, COLUMN_EC_NOUNYU_SHIJI_BI, TITLE_ROW, COLUMN_NOUNYU_SHIJI_BI, TITLE_ROW)
            xls.SetFont(COLUMN_EC_NOUNYU_SHIJI_BI, TITLE_ROW, COLUMN_EC_NOUNYU_SHIJI_BI, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_NOUNYU_SHIJI_BI, TITLE_ROW, COLUMN_EC_NOUNYU_SHIJI_BI, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_NOUNYU_SHIJI_BI, TITLE_ROW, COLUMN_NOUNYU_SHIJI_BI, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_NOUNYU_SHIJI_BI, TITLE_ROW, COLUMN_NOUNYU_SHIJI_BI, TITLE_ROW, "納入指示日")

            '購入希望単価'
            SetLineTitle(xls, COLUMN_EC_GENCHO_KOUNYU_KIBOU_TANKA, TITLE_ROW, COLUMN_GENCHO_KOUNYU_KIBOU_TANKA, TITLE_ROW)
            xls.SetFont(COLUMN_EC_GENCHO_KOUNYU_KIBOU_TANKA, TITLE_ROW, COLUMN_EC_GENCHO_KOUNYU_KIBOU_TANKA, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_GENCHO_KOUNYU_KIBOU_TANKA, TITLE_ROW, COLUMN_EC_GENCHO_KOUNYU_KIBOU_TANKA, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_GENCHO_KOUNYU_KIBOU_TANKA, TITLE_ROW, COLUMN_GENCHO_KOUNYU_KIBOU_TANKA, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_GENCHO_KOUNYU_KIBOU_TANKA, TITLE_ROW, COLUMN_GENCHO_KOUNYU_KIBOU_TANKA, TITLE_ROW, "購入希望単価")

            '購担'
            SetLineTitle(xls, COLUMN_EC_GENCHO_KOUTAN, TITLE_ROW, COLUMN_GENCHO_KOUTAN, TITLE_ROW)
            xls.SetFont(COLUMN_EC_GENCHO_KOUTAN, TITLE_ROW, COLUMN_EC_GENCHO_KOUTAN, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_GENCHO_KOUTAN, TITLE_ROW, COLUMN_EC_GENCHO_KOUTAN, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_GENCHO_KOUTAN, TITLE_ROW, COLUMN_GENCHO_KOUTAN, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_GENCHO_KOUTAN, TITLE_ROW, COLUMN_GENCHO_KOUTAN, TITLE_ROW, "購担")

            '取引先'
            SetLineTitle(xls, COLUMN_EC_GENCHO_TORIHIKISAKI, TITLE_ROW, COLUMN_GENCHO_TORIHIKISAKI, TITLE_ROW)
            xls.SetFont(COLUMN_EC_GENCHO_TORIHIKISAKI, TITLE_ROW, COLUMN_EC_GENCHO_TORIHIKISAKI, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_GENCHO_TORIHIKISAKI, TITLE_ROW, COLUMN_EC_GENCHO_TORIHIKISAKI, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_GENCHO_TORIHIKISAKI, TITLE_ROW, COLUMN_GENCHO_TORIHIKISAKI, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_GENCHO_TORIHIKISAKI, TITLE_ROW, COLUMN_GENCHO_TORIHIKISAKI, TITLE_ROW, "取引先")

            '供給セクション'
            SetLineTitle(xls, COLUMN_EC_GENCHO_KYOKU_SECTION, TITLE_ROW, COLUMN_GENCHO_KYOKU_SECTION, TITLE_ROW)
            xls.SetFont(COLUMN_EC_GENCHO_KYOKU_SECTION, TITLE_ROW, COLUMN_EC_GENCHO_KYOKU_SECTION, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_GENCHO_KYOKU_SECTION, TITLE_ROW, COLUMN_EC_GENCHO_KYOKU_SECTION, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_GENCHO_KYOKU_SECTION, TITLE_ROW, COLUMN_GENCHO_KYOKU_SECTION, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_GENCHO_KYOKU_SECTION, TITLE_ROW, COLUMN_GENCHO_KYOKU_SECTION, TITLE_ROW, "供給セクション")

            '輸送会社'
            SetLineTitle(xls, COLUMN_EC_GENCHO_YUSO_KAISHA, TITLE_ROW, COLUMN_GENCHO_YUSO_KAISHA, TITLE_ROW)
            xls.SetFont(COLUMN_EC_GENCHO_YUSO_KAISHA, TITLE_ROW, COLUMN_EC_GENCHO_YUSO_KAISHA, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_GENCHO_YUSO_KAISHA, TITLE_ROW, COLUMN_EC_GENCHO_YUSO_KAISHA, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_GENCHO_YUSO_KAISHA, TITLE_ROW, COLUMN_GENCHO_GENCHI_KYOKU_SECTION, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_GENCHO_YUSO_KAISHA, TITLE_ROW, COLUMN_GENCHO_YUSO_KAISHA, TITLE_ROW, "輸送会社")

            '現地供給セクション'
            SetLineTitle(xls, COLUMN_EC_GENCHO_GENCHI_KYOKU_SECTION, TITLE_ROW, COLUMN_GENCHO_GENCHI_KYOKU_SECTION, TITLE_ROW)
            xls.SetFont(COLUMN_EC_GENCHO_GENCHI_KYOKU_SECTION, TITLE_ROW, COLUMN_EC_GENCHO_GENCHI_KYOKU_SECTION, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_GENCHO_GENCHI_KYOKU_SECTION, TITLE_ROW, COLUMN_EC_GENCHO_GENCHI_KYOKU_SECTION, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_GENCHO_GENCHI_KYOKU_SECTION, TITLE_ROW, COLUMN_GENCHO_GENCHI_KYOKU_SECTION, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_GENCHO_GENCHI_KYOKU_SECTION, TITLE_ROW, COLUMN_GENCHO_GENCHI_KYOKU_SECTION, TITLE_ROW, "現地供給セクション")

            '配送日'
            SetLineTitle(xls, COLUMN_EC_GENCHO_HAISO_BI, TITLE_ROW, COLUMN_GENCHO_HAISO_BI, TITLE_ROW)
            xls.SetFont(COLUMN_EC_GENCHO_HAISO_BI, TITLE_ROW, COLUMN_EC_GENCHO_HAISO_BI, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_GENCHO_HAISO_BI, TITLE_ROW, COLUMN_EC_GENCHO_HAISO_BI, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_GENCHO_HAISO_BI, TITLE_ROW, COLUMN_GENCHO_HAISO_BI, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_GENCHO_HAISO_BI, TITLE_ROW, COLUMN_GENCHO_HAISO_BI, TITLE_ROW, "配送日")

            '配送先'
            SetLineTitle(xls, COLUMN_EC_GENCHO_HAISO_SAKI, TITLE_ROW, COLUMN_GENCHO_HAISO_SAKI, TITLE_ROW)
            xls.SetFont(COLUMN_EC_GENCHO_HAISO_SAKI, TITLE_ROW, COLUMN_EC_GENCHO_HAISO_SAKI, TITLE_ROW, "ＭＳ Ｐゴシック", 11, _
                        RGB(255, 0, 0), True)
            xls.SetValue(COLUMN_EC_GENCHO_HAISO_SAKI, TITLE_ROW, COLUMN_EC_GENCHO_HAISO_SAKI, TITLE_ROW, "ＥＣ")
            xls.SetFont(COLUMN_GENCHO_HAISO_SAKI, TITLE_ROW, COLUMN_GENCHO_HAISO_SAKI, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_GENCHO_HAISO_SAKI, TITLE_ROW, COLUMN_GENCHO_HAISO_SAKI, TITLE_ROW, "配送先")

            'マスタ参照'
            xls.MergeCells(COLUMN_GENCHO_MASTER_KOUTAN, 10, COLUMN_GENCHO_MASTER_KOUTAN, 10, True)
            SetLineTitle(xls, COLUMN_GENCHO_MASTER_KOUTAN, 10, COLUMN_GENCHO_MASTER_TORIHIKISAKI, 10)
            xls.SetFont(COLUMN_GENCHO_MASTER_KOUTAN, 10, COLUMN_GENCHO_MASTER_KOUTAN, 10, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_GENCHO_MASTER_KOUTAN, 10, COLUMN_GENCHO_MASTER_KOUTAN, 10, "マスタ参照")

            'マスタ参照・購担'
            SetLineTitle(xls, COLUMN_GENCHO_MASTER_KOUTAN, TITLE_ROW, COLUMN_GENCHO_MASTER_KOUTAN, TITLE_ROW)
            xls.SetFont(COLUMN_GENCHO_MASTER_KOUTAN, TITLE_ROW, COLUMN_GENCHO_MASTER_KOUTAN, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_GENCHO_MASTER_KOUTAN, TITLE_ROW, COLUMN_GENCHO_MASTER_KOUTAN, TITLE_ROW, "購担")

            'マスタ参照・取引先'
            SetLineTitle(xls, COLUMN_GENCHO_MASTER_TORIHIKISAKI, TITLE_ROW, COLUMN_GENCHO_MASTER_TORIHIKISAKI, TITLE_ROW)
            xls.SetFont(COLUMN_GENCHO_MASTER_TORIHIKISAKI, TITLE_ROW, COLUMN_GENCHO_MASTER_TORIHIKISAKI, TITLE_ROW, "ＭＳ Ｐゴシック", 11, Nothing, True)
            xls.SetValue(COLUMN_GENCHO_MASTER_TORIHIKISAKI, TITLE_ROW, COLUMN_GENCHO_MASTER_TORIHIKISAKI, TITLE_ROW, "取引先")

        End Sub

        'タイトル行の罫線を引く'
        Private Sub SetLineTitle(ByVal xls As ShisakuExcel, ByVal StartColumn As Integer, _
                                 ByVal StartRow As Integer, ByVal EndColumn As Integer, _
                                 ByVal EndRow As Integer)

            '単独セルか複数セルかで変化'
            If StartColumn = EndColumn Then
                xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)
                xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)
                xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)
                xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)

            Else
                xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)
                xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)
                xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)
                xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThick)
                xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlInsideVertical, XlLineStyle.xlDot, XlBorderWeight.xlHairline)
            End If

        End Sub

        'BODY行の罫線を引く'
        Private Sub SetLineBody(ByVal xls As ShisakuExcel, ByVal StartColumn As Integer, _
                                 ByVal StartRow As Integer, ByVal EndColumn As Integer, _
                                 ByVal EndRow As Integer)


            '罫線を一旦封印'
            '単独セルか複数セルかで変化'
            If StartColumn = EndColumn Then

                If StartRow = EndRow Then
                    'xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                    xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                    xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                    xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                Else
                    xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                    xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                    xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                    xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlInsideHorizontal, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                End If

            Else
                If StartRow = EndRow Then
                    'xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                    xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                    xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                    xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                    xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlInsideVertical, XlLineStyle.xlDot, XlBorderWeight.xlThin)
                Else
                    xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                    xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                    xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                    xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlInsideVertical, XlLineStyle.xlDot, XlBorderWeight.xlThin)
                    xls.SetLine(StartColumn, StartRow, EndColumn, EndRow, XlBordersIndex.xlInsideHorizontal, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
                End If

            End If

        End Sub

        '新調達シートの列サイズ設定'
        Private Sub setShinchotastuSheetColumnWidth(ByVal xls As ShisakuExcel)

            xls.SetColWidth(COLUMN_ERROR_HANTEI, COLUMN_ERROR_HANTEI, 6)
            xls.SetColWidth(2, 2, 2)
            xls.SetColWidth(COLUMN_EC_BLOCK_NO, COLUMN_EC_BLOCK_NO, 5)
            xls.SetColWidth(COLUMN_BLOCK_NO, COLUMN_BLOCK_NO, 9)
            xls.SetColWidth(COLUMN_EC_KOUJI_NO, COLUMN_EC_KOUJI_NO, 5)
            xls.SetColWidth(COLUMN_KOUJI_NO, COLUMN_KOUJI_NO, 11)
            xls.SetColWidth(COLUMN_ROW_ID, COLUMN_ROW_ID, 6)
            xls.SetColWidth(COLUMN_SENYO_MARK, COLUMN_SENYO_MARK, 5)
            xls.SetColWidth(COLUMN_TEHAI_KIGOU, COLUMN_TEHAI_KIGOU, 3)
            xls.SetColWidth(COLUMN_EC_BUHIN_NO, COLUMN_EC_BUHIN_NO, 5)
            xls.SetColWidth(COLUMN_BUHIN_NO, COLUMN_BUHIN_NO, 20)
            xls.SetColWidth(COLUMN_EC_BUHIN_NO, COLUMN_EC_BUHIN_NO, 5)
            xls.SetColWidth(COLUMN_SHISAKU_KBN, COLUMN_SHISAKU_KBN, 13)
            xls.SetColWidth(COLUMN_EC_BUHIN_NAME, COLUMN_EC_BUHIN_NAME, 5)
            xls.SetColWidth(COLUMN_BUHIN_NAME, COLUMN_BUHIN_NAME, 33)
            xls.SetColWidth(COLUMN_EC_NOUNYU_SHIJISU, COLUMN_EC_NOUNYU_SHIJISU, 5)
            xls.SetColWidth(COLUMN_NOUNYU_SHIJI_SU, COLUMN_NOUNYU_SHIJI_SU, 24)
            xls.SetColWidth(COLUMN_EC_NOUNYU_SHIJI_BI, COLUMN_EC_NOUNYU_SHIJI_BI, 5)
            xls.SetColWidth(COLUMN_NOUNYU_SHIJI_BI, COLUMN_NOUNYU_SHIJI_BI, 12)
            xls.SetColWidth(COLUMN_EC_NOUBA, COLUMN_EC_NOUBA, 5)
            xls.SetColWidth(COLUMN_NOUBA, COLUMN_NOUBA, 12)
            xls.SetColWidth(COLUMN_EC_KYOKU_SECTION, COLUMN_EC_KYOKU_SECTION, 5)
            xls.SetColWidth(COLUMN_KYOUKU_SECTION, COLUMN_KYOUKU_SECTION, 19)
            xls.SetColWidth(COLUMN_EC_KOUNYU_KIBOU_TANKA, COLUMN_EC_KOUNYU_KIBOU_TANKA, 5)
            xls.SetColWidth(COLUMN_KOUNYU_KIBOU_TANKA, COLUMN_KOUNYU_KIBOU_TANKA, 19)
            xls.SetColWidth(COLUMN_EC_KOUTAN, COLUMN_EC_KOUTAN, 5)
            xls.SetColWidth(COLUMN_KOUTAN, COLUMN_KOUTAN, 7)
            xls.SetColWidth(COLUMN_EC_TORIHIKISAKI, COLUMN_EC_TORIHIKISAKI, 5)
            xls.SetColWidth(COLUMN_TORIHIKISAKI, COLUMN_TORIHIKISAKI, 10)
            xls.SetColWidth(COLUMN_MASTER_KOUTAN, COLUMN_MASTER_KOUTAN, 7)
            xls.SetColWidth(COLUMN_MASTER_TORIHIKISAKI, COLUMN_MASTER_TORIHIKISAKI, 10)

        End Sub

        '現調品シートの列サイズ設定'
        Private Sub setGenchoSheetColumnWidth(ByVal xls As ShisakuExcel)

            xls.SetColWidth(COLUMN_ERROR_HANTEI, COLUMN_ERROR_HANTEI, 6)
            xls.SetColWidth(2, 2, 2)
            xls.SetColWidth(COLUMN_EC_BLOCK_NO, COLUMN_EC_BLOCK_NO, 5)
            xls.SetColWidth(COLUMN_BLOCK_NO, COLUMN_BLOCK_NO, 9)
            xls.SetColWidth(COLUMN_EC_KOUJI_NO, COLUMN_EC_KOUJI_NO, 5)
            xls.SetColWidth(COLUMN_KOUJI_NO, COLUMN_KOUJI_NO, 11)
            xls.SetColWidth(COLUMN_ROW_ID, COLUMN_ROW_ID, 6)
            xls.SetColWidth(COLUMN_SENYO_MARK, COLUMN_SENYO_MARK, 5)
            xls.SetColWidth(COLUMN_TEHAI_KIGOU, COLUMN_TEHAI_KIGOU, 3)
            xls.SetColWidth(COLUMN_EC_BUHIN_NO, COLUMN_EC_BUHIN_NO, 5)
            xls.SetColWidth(COLUMN_BUHIN_NO, COLUMN_BUHIN_NO, 20)
            xls.SetColWidth(COLUMN_EC_BUHIN_NO, COLUMN_EC_BUHIN_NO, 5)
            xls.SetColWidth(COLUMN_SHISAKU_KBN, COLUMN_SHISAKU_KBN, 13)
            xls.SetColWidth(COLUMN_EC_BUHIN_NAME, COLUMN_EC_BUHIN_NAME, 5)
            xls.SetColWidth(COLUMN_BUHIN_NAME, COLUMN_BUHIN_NAME, 33)
            xls.SetColWidth(COLUMN_EC_NOUNYU_SHIJISU, COLUMN_EC_NOUNYU_SHIJISU, 5)
            xls.SetColWidth(COLUMN_NOUNYU_SHIJI_SU, COLUMN_NOUNYU_SHIJI_SU, 24)
            xls.SetColWidth(COLUMN_EC_NOUNYU_SHIJI_BI, COLUMN_EC_NOUNYU_SHIJI_BI, 5)
            xls.SetColWidth(COLUMN_NOUNYU_SHIJI_BI, COLUMN_NOUNYU_SHIJI_BI, 16)
            xls.SetColWidth(COLUMN_EC_GENCHO_KOUNYU_KIBOU_TANKA, COLUMN_EC_GENCHO_KOUNYU_KIBOU_TANKA, 5)
            xls.SetColWidth(COLUMN_GENCHO_KOUNYU_KIBOU_TANKA, COLUMN_GENCHO_KOUNYU_KIBOU_TANKA, 19)
            xls.SetColWidth(COLUMN_EC_GENCHO_KOUTAN, COLUMN_EC_GENCHO_KOUTAN, 5)
            xls.SetColWidth(COLUMN_GENCHO_KOUTAN, COLUMN_GENCHO_KOUTAN, 7)
            xls.SetColWidth(COLUMN_EC_GENCHO_TORIHIKISAKI, COLUMN_EC_GENCHO_TORIHIKISAKI, 5)
            xls.SetColWidth(COLUMN_GENCHO_TORIHIKISAKI, COLUMN_GENCHO_TORIHIKISAKI, 10)
            xls.SetColWidth(COLUMN_EC_GENCHO_KYOKU_SECTION, COLUMN_EC_GENCHO_KYOKU_SECTION, 5)
            xls.SetColWidth(COLUMN_GENCHO_KYOKU_SECTION, COLUMN_GENCHO_KYOKU_SECTION, 19)
            xls.SetColWidth(COLUMN_EC_GENCHO_YUSO_KAISHA, COLUMN_EC_GENCHO_YUSO_KAISHA, 5)
            xls.SetColWidth(COLUMN_GENCHO_YUSO_KAISHA, COLUMN_GENCHO_YUSO_KAISHA, 13)
            xls.SetColWidth(COLUMN_EC_GENCHO_GENCHI_KYOKU_SECTION, COLUMN_EC_GENCHO_GENCHI_KYOKU_SECTION, 5)
            xls.SetColWidth(COLUMN_GENCHO_GENCHI_KYOKU_SECTION, COLUMN_GENCHO_GENCHI_KYOKU_SECTION, 25)
            xls.SetColWidth(COLUMN_EC_GENCHO_HAISO_BI, COLUMN_EC_GENCHO_HAISO_BI, 5)
            xls.SetColWidth(COLUMN_GENCHO_HAISO_BI, COLUMN_GENCHO_HAISO_BI, 11)
            xls.SetColWidth(COLUMN_EC_GENCHO_HAISO_SAKI, COLUMN_EC_GENCHO_HAISO_SAKI, 5)
            xls.SetColWidth(COLUMN_GENCHO_HAISO_SAKI, COLUMN_GENCHO_HAISO_SAKI, 22)
            xls.SetColWidth(COLUMN_GENCHO_MASTER_KOUTAN, COLUMN_GENCHO_MASTER_KOUTAN, 7)
            xls.SetColWidth(COLUMN_GENCHO_MASTER_TORIHIKISAKI, COLUMN_GENCHO_MASTER_TORIHIKISAKI, 10)

        End Sub

#Region "各列の番号指定"
        '' エラー判定
        Private COLUMN_ERROR_HANTEI As Integer = 1

        '' ブロックNo.EC
        Private COLUMN_EC_BLOCK_NO As Integer = 3

        '' ブロックNo.
        Private COLUMN_BLOCK_NO As Integer = 4

        '' 工事No.EC
        Private COLUMN_EC_KOUJI_NO As Integer = 5

        '' 工事No
        Private COLUMN_KOUJI_NO As Integer = 6

        '' 行ID
        Private COLUMN_ROW_ID As Integer = 7

        '' 専用マーク
        Private COLUMN_SENYO_MARK As Integer = 8

        '' 手配記号
        Private COLUMN_TEHAI_KIGOU As Integer = 9

        '' 部品番号EC
        Private COLUMN_EC_BUHIN_NO As Integer = 10

        '' 部品番号
        Private COLUMN_BUHIN_NO As Integer = 11

        '' 試作区分
        Private COLUMN_SHISAKU_KBN As Integer = 12

        '' 部品名称EC
        Private COLUMN_EC_BUHIN_NAME As Integer = 13

        '' 部品名称
        Private COLUMN_BUHIN_NAME As Integer = 14

        '' 納入指示数EC
        Private COLUMN_EC_NOUNYU_SHIJISU As Integer = 15

        '' 納入指示数
        Private COLUMN_NOUNYU_SHIJI_SU As Integer = 16

        '' 納入指示日EC
        Private COLUMN_EC_NOUNYU_SHIJI_BI As Integer = 17

        '' 納入指示日
        Private COLUMN_NOUNYU_SHIJI_BI As Integer = 18

        '' 納入場所EC
        Private COLUMN_EC_NOUBA As Integer = 19

        '' 納入場所
        Private COLUMN_NOUBA As Integer = 20

        '' 供給セクションEC
        Private COLUMN_EC_KYOKU_SECTION As Integer = 21

        '' 供給セクション
        Private COLUMN_KYOUKU_SECTION As Integer = 22

        '' 購入希望単価EC
        Private COLUMN_EC_KOUNYU_KIBOU_TANKA As Integer = 23

        '' 購入希望単価
        Private COLUMN_KOUNYU_KIBOU_TANKA As Integer = 24

        '' 購担EC
        Private COLUMN_EC_KOUTAN As Integer = 25

        '' 購担
        Private COLUMN_KOUTAN As Integer = 26

        '' 取引先EC
        Private COLUMN_EC_TORIHIKISAKI As Integer = 27

        '' 取引先
        Private COLUMN_TORIHIKISAKI As Integer = 28

        '' マスタ参照・購担
        Private COLUMN_MASTER_KOUTAN As Integer = 30

        '' マスタ参照・取引先
        Private COLUMN_MASTER_TORIHIKISAKI As Integer = 31

        ''現調品

        '' 現調品購入希望単価EC
        Private COLUMN_EC_GENCHO_KOUNYU_KIBOU_TANKA As Integer = 19

        '' 現調品購入希望単価
        Private COLUMN_GENCHO_KOUNYU_KIBOU_TANKA As Integer = 20

        '' 現調品購担EC
        Private COLUMN_EC_GENCHO_KOUTAN As Integer = 21

        '' 現調品購担
        Private COLUMN_GENCHO_KOUTAN As Integer = 22

        '' 現調品取引先EC
        Private COLUMN_EC_GENCHO_TORIHIKISAKI As Integer = 23

        '' 現調品取引先
        Private COLUMN_GENCHO_TORIHIKISAKI As Integer = 24

        '' 現調品供給セクションEC
        Private COLUMN_EC_GENCHO_KYOKU_SECTION As Integer = 25

        '' 現調品供給セクション
        Private COLUMN_GENCHO_KYOKU_SECTION As Integer = 26

        '' 現調品輸送会社EC
        Private COLUMN_EC_GENCHO_YUSO_KAISHA As Integer = 27

        '' 現調品輸送会社
        Private COLUMN_GENCHO_YUSO_KAISHA As Integer = 28

        '' 現調品現地供給セクションEC
        Private COLUMN_EC_GENCHO_GENCHI_KYOKU_SECTION As Integer = 29

        '' 現調品現地供給セクション
        Private COLUMN_GENCHO_GENCHI_KYOKU_SECTION As Integer = 30

        '' 現調品配送日EC
        Private COLUMN_EC_GENCHO_HAISO_BI As Integer = 31

        '' 現調品配送日
        Private COLUMN_GENCHO_HAISO_BI As Integer = 32

        '' 現調品配送先EC
        Private COLUMN_EC_GENCHO_HAISO_SAKI As Integer = 33

        '' 現調品配送先
        Private COLUMN_GENCHO_HAISO_SAKI As Integer = 34

        '' 現調品マスタ参照・購担
        Private COLUMN_GENCHO_MASTER_KOUTAN As Integer = 36

        '' 現調品マスタ参照・取引先
        Private COLUMN_GENCHO_MASTER_TORIHIKISAKI As Integer = 37


#End Region

#Region "各行の番号指定"

        Private TITLE_ROW As Integer = 11

        Private START_ROW As Integer = 12

#End Region


    End Class
End Namespace