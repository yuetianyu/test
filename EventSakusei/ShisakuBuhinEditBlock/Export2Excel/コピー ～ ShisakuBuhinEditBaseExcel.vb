Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports ShisakuCommon
Imports EBom.Common
Imports EBom.Excel
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util.LabelValue


Namespace ShisakuBuhinEditBlock.Export2Excel
    ''' <summary>
    ''' 全ブロックExcel出力する
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShisakuBuhinEditBaseExcel

        Private shisakuEventCode As String
        Private blockList As List(Of String)

        Public Sub New(ByVal shisakuEventCode As String, ByVal blockList As List(Of String))
            Me.shisakuEventCode = shisakuEventCode
            Me.blockList = blockList

        End Sub

        Public Sub Excute(ByVal xls As ShisakuExcel, ByVal fileName As String)
            Dim ExcelImpl As EditBlock2ExcelDao = New EditBlock2ExcelDaoImpl

            'ベース車情報'
            Dim EventBaseListVo As New List(Of TShisakuEventBaseTmpVo)

            'ベース車情報
            Dim BaseGousyaList As List(Of TShisakuEventBaseTmpVo)
            BaseGousyaList = New List(Of TShisakuEventBaseTmpVo)
            BaseGousyaList = ExcelImpl.FindByBaseTmp(shisakuEventCode, LoginInfo.Now.UserId)

            For index As Integer = 0 To BaseGousyaList.Count - 1
                If index > 0 Then

                    EventBaseListVo.Add(BaseGousyaList(index))

                End If
            Next
            'excel出力
            SetSheetAllBlock(xls, EventBaseListVo)
        End Sub



        ''' <summary>
        ''' シートの設定
        ''' </summary>
        ''' <param name="xls">xls</param>
        ''' <param name="EventBaseListVo">イベントベース情報</param>
        ''' <remarks></remarks>
        Private Sub SetSheetAllBlock(ByVal xls As ShisakuExcel, ByVal EventBaseListVo As List(Of TShisakuEventBaseTmpVo))
            xls.SetActiveSheet(1)

            SetSheetHead(xls)

            'タイトル部の設定'
            SetSheetTitle(xls, EventBaseListVo)
            setAllBody(xls, EventBaseListVo)
            SetRowCol(xls)

        End Sub

        ''' <summary>
        ''' ヘッダー部を設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <remarks></remarks>
        Private Sub SetSheetHead(ByVal xls As ShisakuExcel, )
            SetColumnNo()
            xls.SetValue(COL_LEVEL, HEADER_ROW, shisakuKaihatsuFugo + " " + shisakuEventName)
        End Sub

        ''' <summary>
        ''' タイトル部を設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <remarks></remarks>
        Private Sub SetSheetTitle(ByVal xls As ShisakuExcel, ByVal GousyaList As List(Of EditBlockExcelBlockInstlVoHelper), _
                                  ByVal EventBaseListVo As List(Of TShisakuEventBaseVo))

            Dim alphabetList As New List(Of LabelValueVo)

            alphabetList = GetLabelValues_Column()

            'ブロックNo'
            xls.MergeCells(COL_BLOCK_NO, TITLE_ROW, COL_BLOCK_NO, MERGE_ROW, True)
            xls.SetOrientation(COL_BLOCK_NO, TITLE_ROW, COL_BLOCK_NO, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetAlliment(COL_BLOCK_NO, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_BLOCK_NO, TITLE_ROW, "ブロックNo")

            'レベル'
            xls.MergeCells(COL_LEVEL, TITLE_ROW, COL_LEVEL, MERGE_ROW, True)
            xls.SetOrientation(COL_LEVEL, TITLE_ROW, COL_LEVEL, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetAlliment(COL_LEVEL, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_LEVEL, TITLE_ROW, "レベル")
            '国内集計コード'
            xls.MergeCells(COL_SHUKEI_CODE, TITLE_ROW, COL_SHUKEI_CODE, MERGE_ROW, True)
            xls.SetOrientation(COL_SHUKEI_CODE, TITLE_ROW, COL_SHUKEI_CODE, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetAlliment(COL_SHUKEI_CODE, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_SHUKEI_CODE, TITLE_ROW, "国内集計コード")
            '海外集計コード'
            xls.SetOrientation(COL_SIA_SHUKEI_CODE, TITLE_ROW, COL_SIA_SHUKEI_CODE, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.MergeCells(COL_SIA_SHUKEI_CODE, TITLE_ROW, COL_SIA_SHUKEI_CODE, MERGE_ROW, True)
            xls.SetAlliment(COL_SIA_SHUKEI_CODE, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_SIA_SHUKEI_CODE, TITLE_ROW, "海外集計コード")
            '現調区分'
            xls.SetOrientation(COL_GENCYO_KBN, TITLE_ROW, COL_GENCYO_KBN, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.MergeCells(COL_GENCYO_KBN, TITLE_ROW, COL_GENCYO_KBN, MERGE_ROW, True)
            xls.SetAlliment(COL_GENCYO_KBN, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_GENCYO_KBN, TITLE_ROW, "現調区分")
            '取引先コード'
            xls.SetOrientation(COL_TORIHIKISAKI_CODE, TITLE_ROW, COL_TORIHIKISAKI_CODE, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.MergeCells(COL_TORIHIKISAKI_CODE, TITLE_ROW, COL_TORIHIKISAKI_CODE, MERGE_ROW, True)
            xls.SetAlliment(COL_TORIHIKISAKI_CODE, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_TORIHIKISAKI_CODE, TITLE_ROW, "取引先コード")
            '取引先名称'
            xls.MergeCells(COL_TORIHIKISAKI_NAME, TITLE_ROW, COL_TORIHIKISAKI_NAME, MERGE_ROW, True)
            xls.SetAlliment(COL_TORIHIKISAKI_NAME, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetOrientation(COL_TORIHIKISAKI_NAME, TITLE_ROW, COL_TORIHIKISAKI_NAME, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COL_TORIHIKISAKI_NAME, TITLE_ROW, "取引先名称")
            '部品番号'
            xls.MergeCells(COL_BUHIN_NO, TITLE_ROW, COL_BUHIN_NO, MERGE_ROW, True)
            xls.SetAlliment(COL_BUHIN_NO, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_BUHIN_NO, TITLE_ROW, "部品番号")
            '部品番号区分'
            xls.MergeCells(COL_BUHIN_NO_KBN, TITLE_ROW, COL_BUHIN_NO_KBN, MERGE_ROW, True)
            xls.SetAlliment(COL_BUHIN_NO_KBN, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_BUHIN_NO_KBN, TITLE_ROW, "区分")
            '改訂'
            xls.MergeCells(COL_KAITEI, TITLE_ROW, COL_KAITEI, MERGE_ROW, True)
            xls.SetAlliment(COL_KAITEI, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetOrientation(COL_KAITEI, TITLE_ROW, COL_KAITEI, MERGE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COL_KAITEI, TITLE_ROW, "改訂")
            '枝番'
            xls.MergeCells(COL_EDA_BAN, TITLE_ROW, COL_EDA_BAN, MERGE_ROW, True)
            xls.SetAlliment(COL_EDA_BAN, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetOrientation(COL_EDA_BAN, TITLE_ROW, COL_EDA_BAN, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COL_EDA_BAN, TITLE_ROW, "枝番")
            '部品名称'
            xls.MergeCells(COL_BUHIN_NAME, TITLE_ROW, COL_BUHIN_NAME, MERGE_ROW, True)
            xls.SetAlliment(COL_BUHIN_NAME, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_BUHIN_NAME, TITLE_ROW, "部品名称")

            Dim gousyaIndex As Integer = 0

            '員数(号車)'
            'タイトル'

            xls.SetValue(COL_START_GOUSYA, TITLE_ROW, "員数")
            Dim gousya As New List(Of String)
            For index As Integer = 0 To EventBaseListVo.Count - 1
                xls.SetValue(COL_START_GOUSYA + index, INSU_TITLE_ROW, alphabetList(index).Label)
                xls.MergeCells(COL_START_GOUSYA + index, GOUSYA_ROW, COL_START_GOUSYA + index, MERGE_ROW, True)
                xls.SetValue(COL_START_GOUSYA + index, GOUSYA_ROW, EventBaseListVo(index).ShisakuGousya)
                gousyaIndex = gousyaIndex + 1
            Next

            xls.SetOrientation(COL_START_GOUSYA, GOUSYA_ROW, COL_START_GOUSYA + gousyaIndex, GOUSYA_ROW, ShisakuExcel.XlOrientation.xlVertical)


            '号車名'
            'For index As Integer = 0 To GousyaList.Count - 1
            '    '号車名が重複しないようにする'

            '    If StringUtil.IsEmpty(GousyaList(index).ShisakuGousya) Then
            '        Continue For
            '    End If

            '    If index = 0 Then
            '        gousya.Add(GousyaList(index).ShisakuGousya)
            '        xls.SetValue(COL_START_GOUSYA + gousyaIndex, INSU_TITLE_ROW, alphabetList(gousyaIndex).Label)
            '        xls.MergeCells(COL_START_GOUSYA + gousyaIndex, GOUSYA_ROW, COL_START_GOUSYA + gousyaIndex, MERGE_ROW, True)
            '        xls.SetOrientation(COL_START_GOUSYA + gousyaIndex, GOUSYA_ROW, _
            '                           COL_START_GOUSYA + gousyaIndex, GOUSYA_ROW, ShisakuExcel.XlOrientation.xlVertical)

            '        xls.SetValue(COL_START_GOUSYA + gousyaIndex, GOUSYA_ROW, GousyaList(index).ShisakuGousya)

            '        gousyaIndex = gousyaIndex + 1
            '    ElseIf Not gousya.Contains(GousyaList(index).ShisakuGousya) Then
            '        gousya.Add(GousyaList(index).ShisakuGousya)
            '        xls.SetValue(COL_START_GOUSYA + gousyaIndex, INSU_TITLE_ROW, alphabetList(gousyaIndex).Label)
            '        xls.MergeCells(COL_START_GOUSYA + gousyaIndex, GOUSYA_ROW, COL_START_GOUSYA + gousyaIndex, MERGE_ROW, True)
            '        xls.SetOrientation(COL_START_GOUSYA + gousyaIndex, GOUSYA_ROW, _
            '                           COL_START_GOUSYA + gousyaIndex, GOUSYA_ROW, ShisakuExcel.XlOrientation.xlVertical)
            '        xls.SetValue(COL_START_GOUSYA + gousyaIndex, GOUSYA_ROW, GousyaList(index).ShisakuGousya)

            '        gousyaIndex = gousyaIndex + 1

            '    End If
            'Next

            GousyaCount = gousyaIndex
            GousyaInsuCount = gousyaIndex

            '最後にマージしてタイトル完成'
            xls.MergeCells(COL_START_GOUSYA, TITLE_ROW, COL_START_GOUSYA + gousyaIndex - 1, TITLE_ROW, True)
            '罫線を引いておく'
            '上'
            xls.SetLine(COL_START_GOUSYA, TITLE_ROW, COL_START_GOUSYA + gousyaIndex, TITLE_ROW, _
                        XlBordersIndex.xlEdgeTop, XlLineStyle.xlDouble, XlBorderWeight.xlThin)
            '下'
            xls.SetLine(COL_START_GOUSYA, INSU_TITLE_ROW, COL_START_GOUSYA + gousyaIndex - 1, INSU_TITLE_ROW, _
                                    XlBordersIndex.xlEdgeBottom, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            '左'
            xls.SetLine(COL_START_GOUSYA, TITLE_ROW, COL_START_GOUSYA, xls.EndRow, _
                        XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble, XlBorderWeight.xlThin)
            '右'
            xls.SetLine(COL_START_GOUSYA + gousyaIndex - 1, TITLE_ROW, COL_START_GOUSYA + gousyaIndex - 1, xls.EndRow, _
                        XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)

            '再使用不可'
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + gousyaIndex, MERGE_ROW, True)
            xls.SetOrientation(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, _
                   COL_START_GOUSYA + gousyaIndex, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + gousyaIndex, TITLE_ROW, "再使用不可")

            '再使用不可の左'
            xls.SetLine(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + gousyaIndex, xls.EndRow, _
            XlBordersIndex.xlEdgeRight, XlLineStyle.xlDouble, XlBorderWeight.xlThin)

            '2012/01/25
            '供給セクション'
            gousyaIndex = gousyaIndex + 1
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + gousyaIndex, MERGE_ROW, True)
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, "供給セクション")


            '出図予定日'
            gousyaIndex = gousyaIndex + 1
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + gousyaIndex, MERGE_ROW, True)
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, "出図予定日")

            '材質'
            gousyaIndex = gousyaIndex + 1
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + 3 + gousyaIndex, GOUSYA_ROW, True)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, "材質")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "規格１")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "規格２")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "規格３")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "メッキ")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)

            '板厚'
            gousyaIndex = gousyaIndex + 1
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + 1 + gousyaIndex, GOUSYA_ROW, True)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, "板厚")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "板厚")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "u")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)

            '試作部品費(円)'
            gousyaIndex = gousyaIndex + 1
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + gousyaIndex, MERGE_ROW, True)
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, "試作部品費" + vbCrLf + "(円)")
            '試作型費'
            gousyaIndex = gousyaIndex + 1
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + gousyaIndex, MERGE_ROW, True)
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, "試作型費" + vbCrLf + "(千円)")
            '部品NOTE' 2012/03/09追加
            gousyaIndex = gousyaIndex + 1
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + gousyaIndex, MERGE_ROW, True)
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, "部品NOTE")
            '備考'
            gousyaIndex = gousyaIndex + 1
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + gousyaIndex, MERGE_ROW, True)
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, "備考")

            '担当者名'
            gousyaIndex = gousyaIndex + 1
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + gousyaIndex, MERGE_ROW, True)
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, "担当者名")



            'Tel'
            gousyaIndex = gousyaIndex + 1
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + gousyaIndex, MERGE_ROW, True)
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, "TEL")

        End Sub
        ''' <summary>
        ''' Excel出力　シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="buhinEditList">>部品編集情報+INSTL品番+号車</param>
        ''' <param name="gousyaList">設計ブロックINSTL情報+部品番号表示順+部品編集INSTL情報の員数</param>
        ''' <remarks></remarks>
        Public Sub setAllBody(ByVal xls As ShisakuExcel, ByVal buhinEditList As List(Of TShisakuBuhinEditVoHelperExcel), _
                            ByVal gousyaList As List(Of EditBlockExcelBlockInstlVoHelper), _
                            ByVal EventBaseListVo As List(Of TShisakuEventBaseVo))

            '速度向上の為、改行した時以外は共通データを書き込まないように変更
            'また１データづつ出力せず配列に格納し、範囲指定で出力するように変更
            'マージさせるため'
            Dim rowIndex As Integer = 0
            Dim MergeFlag As Boolean = False
            Dim maxRowNumber As Integer = 0


            Debug.Print("StartTime = " & Now())
            '２次元配列の列数を算出
            'For i As Integer = 0 To buhinEditList.Count - 1
            '    If Not i = 0 Then
            '        If StringUtil.Equals(buhinEditList(i).Level, buhinEditList(i - 1).Level) Then
            '            If StringUtil.Equals(buhinEditList(i).BuhinNo, buhinEditList(i - 1).BuhinNo) Then
            '                If StringUtil.Equals(buhinEditList(i).ShukeiCode, buhinEditList(i - 1).ShukeiCode) Then
            '                Else
            '                    If StringUtil.Equals(buhinEditList(i).SiaShukeiCode, buhinEditList(i - 1).SiaShukeiCode) Then
            '                    Else
            '                        maxRowNumber = maxRowNumber + 1
            '                    End If
            '                End If
            '            Else
            '                maxRowNumber = maxRowNumber + 1
            '            End If
            '        Else
            '            maxRowNumber = maxRowNumber + 1
            '        End If
            '    End If
            'Next
            '２次元配列の列数を算出
            For i As Integer = 0 To buhinEditList.Count - 1
                If Not i = 0 Then
                    If StringUtil.Equals(buhinEditList(i).Level, buhinEditList(i - 1).Level) Then
                        If StringUtil.Equals(buhinEditList(i).BuhinNo, buhinEditList(i - 1).BuhinNo) Then
                            If StringUtil.Equals(buhinEditList(i).BuhinNoKbn, buhinEditList(i - 1).BuhinNoKbn) Then
                                If StringUtil.Equals(buhinEditList(i).KyoukuSection, buhinEditList(i - 1).KyoukuSection) Then
                                    If StringUtil.Equals(buhinEditList(i).Saishiyoufuka, buhinEditList(i - 1).Saishiyoufuka) Then
                                        If StringUtil.Equals(buhinEditList(i).ShukeiCode, buhinEditList(i - 1).ShukeiCode) Then
                                        Else
                                            If StringUtil.Equals(buhinEditList(i).SiaShukeiCode, buhinEditList(i - 1).SiaShukeiCode) Then
                                            Else
                                                maxRowNumber = maxRowNumber + 1
                                            End If
                                        End If
                                    Else
                                        maxRowNumber = maxRowNumber + 1
                                    End If
                                Else
                                    maxRowNumber = maxRowNumber + 1
                                End If
                            Else
                                maxRowNumber = maxRowNumber + 1
                            End If
                        Else
                            maxRowNumber = maxRowNumber + 1
                        End If
                    Else
                        maxRowNumber = maxRowNumber + 1
                    End If
                End If
            Next

            Dim dataMatrix(maxRowNumber, COL_BUHIN_NAME + GousyaCount + 14) As String

            For index As Integer = 0 To buhinEditList.Count - 1



                'マージ可能ならマージする'
                'If Not index = 0 Then
                '    If StringUtil.Equals(buhinEditList(index).Level, buhinEditList(index - 1).Level) Then
                '        If StringUtil.Equals(buhinEditList(index).BuhinNo, buhinEditList(index - 1).BuhinNo) Then
                '            If StringUtil.Equals(buhinEditList(index).ShukeiCode, buhinEditList(index - 1).ShukeiCode) Then
                '                rowIndex = rowIndex - 1
                '                MergeFlag = True
                '            Else
                '                If StringUtil.Equals(buhinEditList(index).SiaShukeiCode, buhinEditList(index - 1).SiaShukeiCode) Then
                '                    rowIndex = rowIndex - 1
                '                    MergeFlag = True
                '                End If
                '            End If
                '        End If
                '    End If
                'End If
                If Not index = 0 Then
                    If StringUtil.Equals(buhinEditList(index).Level, buhinEditList(index - 1).Level) Then
                        If StringUtil.Equals(buhinEditList(index).BuhinNo, buhinEditList(index - 1).BuhinNo) Then
                            If StringUtil.Equals(buhinEditList(index).BuhinNoKbn, buhinEditList(index - 1).BuhinNoKbn) Then
                                If StringUtil.Equals(buhinEditList(index).KyoukuSection, buhinEditList(index - 1).KyoukuSection) Then
                                    If StringUtil.Equals(buhinEditList(index).Saishiyoufuka, buhinEditList(index - 1).Saishiyoufuka) Then
                                        If StringUtil.Equals(buhinEditList(index).ShukeiCode, buhinEditList(index - 1).ShukeiCode) Then
                                            rowIndex = rowIndex - 1
                                            MergeFlag = True
                                        Else
                                            If StringUtil.Equals(buhinEditList(index).SiaShukeiCode, buhinEditList(index - 1).SiaShukeiCode) Then
                                                rowIndex = rowIndex - 1
                                                MergeFlag = True
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If


                If MergeFlag Then
                    '員数'
                    Dim insu As Integer = 0
                    If buhinEditList(index).HyojijunNo = buhinEditList(index - 1).HyojijunNo Then
                        insu = buhinEditList(index - 1).InsuSuryo
                    End If

                    If Not StringUtil.IsEmpty(insu) Then
                        '**は数値では無いから'
                        If StringUtil.Equals(insu, "**") Then
                            dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = "**"
                        Else
                            '員数がマイナスなら'
                            If buhinEditList(index).InsuSuryo < 0 Then
                                dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = "**"
                            Else
                                dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = buhinEditList(index).InsuSuryo + insu
                            End If
                        End If

                    Else
                        If buhinEditList(index).InsuSuryo < 0 Then
                            dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = "**"
                        Else
                            dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = buhinEditList(index).InsuSuryo
                        End If

                    End If
                Else

                    'ブロックNo'
                    dataMatrix(rowIndex, COL_BLOCK_NO - 1) = buhinEditList(index).ShisakuBlockNo
                    'レベル'
                    dataMatrix(rowIndex, COL_LEVEL - 1) = buhinEditList(index).Level
                    '国内集計コード'
                    dataMatrix(rowIndex, COL_SHUKEI_CODE - 1) = buhinEditList(index).ShukeiCode
                    '海外集計コード'
                    dataMatrix(rowIndex, COL_SIA_SHUKEI_CODE - 1) = buhinEditList(index).SiaShukeiCode
                    '現調区分'
                    dataMatrix(rowIndex, COL_GENCYO_KBN - 1) = buhinEditList(index).GencyoCkdKbn
                    '取引先コード'
                    dataMatrix(rowIndex, COL_TORIHIKISAKI_CODE - 1) = buhinEditList(index).MakerCode
                    '取引先名称'
                    dataMatrix(rowIndex, COL_TORIHIKISAKI_NAME - 1) = buhinEditList(index).MakerName
                    '部品番号'
                    dataMatrix(rowIndex, COL_BUHIN_NO - 1) = buhinEditList(index).BuhinNo
                    '部品番号区分'
                    dataMatrix(rowIndex, COL_BUHIN_NO_KBN - 1) = buhinEditList(index).BuhinNoKbn
                    '部品番号改訂No'
                    dataMatrix(rowIndex, COL_KAITEI - 1) = buhinEditList(index).BuhinNoKaiteiNo
                    '枝番'
                    dataMatrix(rowIndex, COL_EDA_BAN - 1) = buhinEditList(index).EdaBan
                    '部品名称'
                    dataMatrix(rowIndex, COL_BUHIN_NAME - 1) = buhinEditList(index).BuhinName

                    EditGousyaCount = GousyaCount

                    '員数'
                    Dim insu As String = xls.GetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex)
                    If Not StringUtil.IsEmpty(insu) Then
                        '**は数値では無いから'
                        If StringUtil.Equals(insu, "**") Then
                            dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = "**"
                        Else
                            '員数がマイナスなら'
                            If buhinEditList(index).InsuSuryo < 0 Then
                                dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = "**"
                            Else
                                dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = buhinEditList(index).InsuSuryo + Integer.Parse(insu)
                            End If
                        End If

                    Else
                        If buhinEditList(index).InsuSuryo < 0 Then
                            dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = "**"
                        Else
                            dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = buhinEditList(index).InsuSuryo
                        End If

                    End If

                    '再使用不可'
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).Saishiyoufuka

                    '2012/01/23 供給セクション追加
                    '供給セクション'
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 1 - 1) = buhinEditList(index).KyoukuSection

                    '出図予定日'
                    If buhinEditList(index).ShutuzuYoteiDate = "99999999" Or buhinEditList(index).ShutuzuYoteiDate = "0" Then
                        'EditGousyaCount = EditGousyaCount + 1
                        dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 2 - 1) = ""
                    Else
                        'EditGousyaCount = EditGousyaCount + 1
                        dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 2 - 1) = buhinEditList(index).ShutuzuYoteiDate
                    End If

                    '材質'
                    '規格１'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 3 - 1) = buhinEditList(index).ZaishituKikaku1
                    '規格２'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 4 - 1) = buhinEditList(index).ZaishituKikaku2
                    '規格３'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 5 - 1) = buhinEditList(index).ZaishituKikaku3
                    'メッキ'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 6 - 1) = buhinEditList(index).ZaishituMekki

                    '板厚'
                    '板厚数量'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 7 - 1) = buhinEditList(index).ShisakuBankoSuryo
                    '板厚数量U'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 8 - 1) = buhinEditList(index).ShisakuBankoSuryoU
                    '試作部品費'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 9 - 1) = buhinEditList(index).ShisakuBuhinHi
                    '試作型費'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 10 - 1) = buhinEditList(index).ShisakuKataHi
                    '部品NOTE' 2012/03/09追加
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 11 - 1) = buhinEditList(index).BuhinNote
                    '備考'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 12 - 1) = buhinEditList(index).Bikou
                    '担当者名'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 13 - 1) = buhinEditList(index).UserId
                    '電話番号'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 14 - 1) = buhinEditList(index).TelNo



                End If






                'For gousyaIndex As Integer = 0 To gousyaList.Count - 1
                '    For insuIndex As Integer = 0 To GousyaInsuCount - 1
                '        If StringUtil.Equals(gousyaList(gousyaIndex).ShisakuGousya, xls.GetValue(COL_START_GOUSYA + insuIndex, GOUSYA_ROW)) Then
                '            'ブロックNoと部品番号表示順があっているかチェック'
                '            If InsuCheck(buhinEditList(index), gousyaList(gousyaIndex)) Then
                '                'あっていれば員数の書き込みが発生'
                '                'insuIndex番目の号車のINDEX番目が空かチェック'
                '                If Not StringUtil.IsEmpty(xls.GetValue(COL_START_GOUSYA + insuIndex, START_ROW + index)) Then
                '                    '空でないならそのセルの数値との合計を挿入'
                '                    Dim insu As Integer = Integer.Parse(xls.GetValue(COL_START_GOUSYA + insuIndex, START_ROW + index))
                '                    insu = insu + gousyaList(gousyaIndex).BuhinInsuSuryo
                '                    xls.SetValue(COL_START_GOUSYA + insuIndex, _
                '                                 START_ROW + index, insu)
                '                Else
                '                    '空なら新規で挿入'
                '                    xls.SetValue(COL_START_GOUSYA + insuIndex, _
                '                                 START_ROW + index, gousyaList(gousyaIndex).BuhinInsuSuryo)
                '                End If
                '            Else
                '                'あってないなら挿入しない'
                '                Continue For
                '            End If

                '        End If
                '    Next
                'Next

                MergeFlag = False
                rowIndex = rowIndex + 1

            Next
            xls.CopyRange(0, START_ROW, UBound(dataMatrix, 2) - 1, START_ROW + UBound(dataMatrix), dataMatrix)
            Debug.Print("EndTime = " & Now())


        End Sub
        ''' <summary>
        ''' Excel出力　シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="buhinEditList">>部品編集情報+INSTL品番+号車</param>
        ''' <param name="gousyaList">設計ブロックINSTL情報+部品番号表示順+部品編集INSTL情報の員数</param>
        ''' <remarks></remarks>
        Public Sub setAllBodyOLD(ByVal xls As ShisakuExcel, ByVal buhinEditList As List(Of TShisakuBuhinEditVoHelperExcel), _
                            ByVal gousyaList As List(Of EditBlockExcelBlockInstlVoHelper), _
                            ByVal EventBaseListVo As List(Of TShisakuEventBaseVo))

            'マージさせるため'
            Dim rowIndex As Integer = 0
            Dim MergeFlag As Boolean = False
            Debug.Print("StartTime = " & Now())
            For index As Integer = 0 To buhinEditList.Count - 1



                'マージ可能ならマージする'
                If Not index = 0 Then
                    If StringUtil.Equals(buhinEditList(index).Level, buhinEditList(index - 1).Level) Then
                        If StringUtil.Equals(buhinEditList(index).BuhinNo, buhinEditList(index - 1).BuhinNo) Then
                            If StringUtil.Equals(buhinEditList(index).KyoukuSection, buhinEditList(index - 1).KyoukuSection) Then
                                If StringUtil.Equals(buhinEditList(index).Saishiyoufuka, buhinEditList(index - 1).Saishiyoufuka) Then
                                    If StringUtil.Equals(buhinEditList(index).ShukeiCode, buhinEditList(index - 1).ShukeiCode) Then
                                        rowIndex = rowIndex - 1
                                        MergeFlag = True
                                    Else
                                        If StringUtil.Equals(buhinEditList(index).SiaShukeiCode, buhinEditList(index - 1).SiaShukeiCode) Then
                                            rowIndex = rowIndex - 1
                                            MergeFlag = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If


                If MergeFlag Then
                    '員数'
                    Dim insu As String = xls.GetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex)
                    If Not StringUtil.IsEmpty(insu) Then
                        '**は数値では無いから'
                        If StringUtil.Equals(insu, "**") Then
                            xls.SetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                        Else
                            '員数がマイナスなら'
                            If buhinEditList(index).InsuSuryo < 0 Then
                                xls.SetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                            Else
                                xls.SetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, buhinEditList(index).InsuSuryo + Integer.Parse(insu))
                            End If
                        End If

                    Else
                        If buhinEditList(index).InsuSuryo < 0 Then
                            xls.SetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                        Else
                            xls.SetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, buhinEditList(index).InsuSuryo)
                        End If

                    End If
                Else

                    'ブロックNo'
                    xls.SetValue(COL_BLOCK_NO, START_ROW + rowIndex, buhinEditList(index).ShisakuBlockNo)
                    'レベル'
                    xls.SetValue(COL_LEVEL, START_ROW + rowIndex, buhinEditList(index).Level)
                    '国内集計コード'
                    xls.SetValue(COL_SHUKEI_CODE, START_ROW + rowIndex, buhinEditList(index).ShukeiCode)
                    '海外集計コード'
                    xls.SetValue(COL_SIA_SHUKEI_CODE, START_ROW + rowIndex, buhinEditList(index).SiaShukeiCode)
                    '現調区分'
                    xls.SetValue(COL_GENCYO_KBN, START_ROW + rowIndex, buhinEditList(index).GencyoCkdKbn)
                    '取引先コード'
                    xls.SetValue(COL_TORIHIKISAKI_CODE, START_ROW + rowIndex, buhinEditList(index).MakerCode)
                    '取引先名称'
                    xls.SetValue(COL_TORIHIKISAKI_NAME, START_ROW + rowIndex, buhinEditList(index).MakerName)
                    '部品番号'
                    xls.SetValue(COL_BUHIN_NO, START_ROW + rowIndex, buhinEditList(index).BuhinNo)
                    '部品番号区分'
                    xls.SetValue(COL_BUHIN_NO_KBN, START_ROW + rowIndex, buhinEditList(index).BuhinNoKbn)
                    '部品番号改訂No'
                    xls.SetValue(COL_KAITEI, START_ROW + rowIndex, buhinEditList(index).BuhinNoKaiteiNo)
                    '枝番'
                    xls.SetValue(COL_EDA_BAN, START_ROW + rowIndex, buhinEditList(index).EdaBan)
                    '部品名称'
                    xls.SetValue(COL_BUHIN_NAME, START_ROW + rowIndex, buhinEditList(index).BuhinName)

                    EditGousyaCount = GousyaCount

                    '員数'
                    Dim insu As String = xls.GetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex)
                    If Not StringUtil.IsEmpty(insu) Then
                        '**は数値では無いから'
                        If StringUtil.Equals(insu, "**") Then
                            xls.SetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                        Else
                            '員数がマイナスなら'
                            If buhinEditList(index).InsuSuryo < 0 Then
                                xls.SetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                            Else
                                xls.SetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, buhinEditList(index).InsuSuryo + Integer.Parse(insu))
                            End If
                        End If

                    Else
                        If buhinEditList(index).InsuSuryo < 0 Then
                            xls.SetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                        Else
                            xls.SetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, buhinEditList(index).InsuSuryo)
                        End If

                    End If

                    '再使用不可'
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).Saishiyoufuka)

                    '2012/01/23 供給セクション追加
                    '供給セクション'
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 1, START_ROW + rowIndex, buhinEditList(index).KyoukuSection)

                    '出図予定日'
                    If buhinEditList(index).ShutuzuYoteiDate = "99999999" Or buhinEditList(index).ShutuzuYoteiDate = "0" Then
                        'EditGousyaCount = EditGousyaCount + 1
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 2, START_ROW + rowIndex, "")
                    Else
                        'EditGousyaCount = EditGousyaCount + 1
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 2, START_ROW + rowIndex, buhinEditList(index).ShutuzuYoteiDate)
                    End If

                    '材質'
                    '規格１'
                    'EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 3, START_ROW + rowIndex, buhinEditList(index).ZaishituKikaku1)
                    '規格２'
                    'EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 4, START_ROW + rowIndex, buhinEditList(index).ZaishituKikaku2)
                    '規格３'
                    'EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 5, START_ROW + rowIndex, buhinEditList(index).ZaishituKikaku3)
                    'メッキ'
                    'EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 6, START_ROW + rowIndex, buhinEditList(index).ZaishituMekki)

                    '板厚'
                    '板厚数量'
                    'EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 7, START_ROW + rowIndex, buhinEditList(index).ShisakuBankoSuryo)
                    '板厚数量U'
                    'EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 8, START_ROW + rowIndex, buhinEditList(index).ShisakuBankoSuryoU)
                    '試作部品費'
                    'EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 9, START_ROW + rowIndex, buhinEditList(index).ShisakuBuhinHi)
                    '試作型費'
                    'EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 10, START_ROW + rowIndex, buhinEditList(index).ShisakuKataHi)
                    '備考'
                    'EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 11, START_ROW + rowIndex, buhinEditList(index).Bikou)
                    '担当者名'
                    'EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 12, START_ROW + rowIndex, buhinEditList(index).UserId)
                    '電話番号'
                    'EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 13, START_ROW + rowIndex, buhinEditList(index).TelNo)



                End If






                'For gousyaIndex As Integer = 0 To gousyaList.Count - 1
                '    For insuIndex As Integer = 0 To GousyaInsuCount - 1
                '        If StringUtil.Equals(gousyaList(gousyaIndex).ShisakuGousya, xls.GetValue(COL_START_GOUSYA + insuIndex, GOUSYA_ROW)) Then
                '            'ブロックNoと部品番号表示順があっているかチェック'
                '            If InsuCheck(buhinEditList(index), gousyaList(gousyaIndex)) Then
                '                'あっていれば員数の書き込みが発生'
                '                'insuIndex番目の号車のINDEX番目が空かチェック'
                '                If Not StringUtil.IsEmpty(xls.GetValue(COL_START_GOUSYA + insuIndex, START_ROW + index)) Then
                '                    '空でないならそのセルの数値との合計を挿入'
                '                    Dim insu As Integer = Integer.Parse(xls.GetValue(COL_START_GOUSYA + insuIndex, START_ROW + index))
                '                    insu = insu + gousyaList(gousyaIndex).BuhinInsuSuryo
                '                    xls.SetValue(COL_START_GOUSYA + insuIndex, _
                '                                 START_ROW + index, insu)
                '                Else
                '                    '空なら新規で挿入'
                '                    xls.SetValue(COL_START_GOUSYA + insuIndex, _
                '                                 START_ROW + index, gousyaList(gousyaIndex).BuhinInsuSuryo)
                '                End If
                '            Else
                '                'あってないなら挿入しない'
                '                Continue For
                '            End If

                '        End If
                '    Next
                'Next

                MergeFlag = False
                rowIndex = rowIndex + 1

            Next
            Debug.Print("EndTime = " & Now())


        End Sub

        'シートの行の高さと列の幅を設定'
        Private Sub SetRowCol(ByVal xls As ShisakuExcel)
            xls.AutoFitCol(COL_LEVEL, xls.EndCol)
            xls.AutoFitRow(1, xls.EndRow)

            xls.SetColWidth(COL_LEVEL, COL_LEVEL, 3)
            xls.SetColWidth(COL_TORIHIKISAKI_NAME, COL_TORIHIKISAKI_NAME, 16)
            xls.SetRowHeight(GOUSYA_ROW, GOUSYA_ROW, 114)

        End Sub

        '各列のタグに番号付与'
        Private Sub SetColumnNo()
            Dim col As Integer = 1
            COL_BLOCK_NO = EzUtil.Increment(col)
            COL_LEVEL = EzUtil.Increment(col)
            COL_SHUKEI_CODE = EzUtil.Increment(col)
            COL_SIA_SHUKEI_CODE = EzUtil.Increment(col)
            COL_GENCYO_KBN = EzUtil.Increment(col)
            COL_TORIHIKISAKI_CODE = EzUtil.Increment(col)
            COL_TORIHIKISAKI_NAME = EzUtil.Increment(col)
            COL_BUHIN_NO = EzUtil.Increment(col)
            COL_BUHIN_NO_KBN = EzUtil.Increment(col)
            COL_KAITEI = EzUtil.Increment(col)
            COL_EDA_BAN = EzUtil.Increment(col)
            COL_BUHIN_NAME = EzUtil.Increment(col)
            COL_START_GOUSYA = EzUtil.Increment(col)
        End Sub

        '各行のタグに新たに番号を付与'
        Private Sub SetRowNo(ByVal BuhinCount As Integer)
            TITLE_ROW = TITLE_ROW + BuhinCount + 9
            GOUSYA_ROW = GOUSYA_ROW + BuhinCount + 9
            START_ROW = START_ROW + BuhinCount + 9
            HEADER_ROW = HEADER_ROW + BuhinCount + 9
            INSU_TITLE_ROW = INSU_TITLE_ROW + BuhinCount + 9
            MERGE_ROW = MERGE_ROW + BuhinCount + 9
        End Sub

        ''' <summary>
        ''' カラムコンボボックスへラベル、値を設定。
        ''' 後でINSTL品番と紐付け、必要分のみ設定する。
        ''' </summary>
        Public Shared Function GetLabelValues_Column() As List(Of LabelValueVo)

            Dim results As New List(Of LabelValueVo)
            Dim index As Integer = 0
            Dim alphabet As String = ""

            For index = 0 To 1000
                alphabet = EzUtil.ConvIndexToAlphabet(index)
                results.Add(New LabelValueVo(alphabet, index))
            Next

            Return results
        End Function

        ''' <summary>
        ''' 現在のセルの位置に員数をいれるかチェック
        ''' </summary>
        Private Function InsuCheck(ByVal BuhinVo As TShisakuBuhinEditVoHelperExcel, ByVal GousyaVo As EditBlockExcelBlockInstlVoHelper) As Boolean
            If Not StringUtil.Equals(BuhinVo.ShisakuBlockNo, GousyaVo.ShisakuBlockNo) Then
                Return False
            End If
            If Not StringUtil.Equals(BuhinVo.BuhinNoHyoujiJun, Integer.Parse(GousyaVo.BuhinNoHyoujiJun)) Then
                Return False
            End If

            Return True
        End Function


#Region "各列のタグ"
        ''種別
        Private SYUBETU As Integer
        ''レベル
        Private COL_LEVEL As Integer
        ''国内集計コード
        Private COL_SHUKEI_CODE As Integer
        ''海外集計コード
        Private COL_SIA_SHUKEI_CODE As Integer
        ''現調区分
        Private COL_GENCYO_KBN As Integer
        ''取引先コード
        Private COL_TORIHIKISAKI_CODE As Integer
        ''取引先名称
        Private COL_TORIHIKISAKI_NAME As Integer
        ''部品番号
        Private COL_BUHIN_NO As Integer
        ''部品番号区分
        Private COL_BUHIN_NO_KBN As Integer
        ''改訂
        Private COL_KAITEI As Integer
        ''枝番
        Private COL_EDA_BAN As Integer
        ''部品名称
        Private COL_BUHIN_NAME As Integer
        ''員数(号車)の開始列
        Private COL_START_GOUSYA As Integer

#End Region

#Region "各行のタグ名称"

        '全体用ヘッダー基点'
        Private HEADER_ROW As Integer = 1
        'タイトル行'
        Private TITLE_ROW As Integer = 4
        '員数タイトル行'
        Private INSU_TITLE_ROW As Integer = 5
        '号車行'
        Private GOUSYA_ROW As Integer = 6
        'マージする行'
        Private MERGE_ROW As Integer = 7
        'データ書き込み開始行'
        Private START_ROW As Integer = 8

#End Region

    End Class
End Namespace