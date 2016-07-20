Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports ShisakuCommon
Imports EBom.Common
Imports EBom.Excel
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util.LabelValue
Imports EventSakusei.ShisakuBuhinEdit.Al.Logic
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl


Namespace ShisakuBuhinEditBlock.Export2Excel
    ''' <summary>
    ''' 全ブロックExcel出力する
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShisakuBuhinEditBlock2AllExcel

        Private shisakuEventCode As String
        Private shisakuBukaCode As String
        Private tantouUserName As String
        Private tel As String
        Private shisakuKaihatsuFugo As String
        Private shisakuEventName As String
        Private GousyaCount As Integer = 0
        Private EditGousyaCount As Integer = 0
        Private GousyaInsuCount As Integer = 0

        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ah) (TES)張 ADD BEGIN
        Private ReadOnly subject As BuhinEditAlSubject
        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ah) (TES)張 ADD END

        Public Sub New(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String)
            Me.shisakuEventCode = shisakuEventCode
            Me.shisakuBukaCode = shisakuBukaCode

        End Sub


        Public Sub Excute(ByVal xls As ShisakuExcel, ByVal fileName As String, Optional ByVal blockNosList As List(Of String) = Nothing, Optional ByVal shisakuBlockNoKaiteiNo As String = "", Optional ByVal isBase As Boolean = False)
            Dim ExcelImpl As EditBlock2ExcelDao = New EditBlock2ExcelDaoImpl

            'イベント情報を取得'
            Dim EventVo As New TShisakuEventVo
            EventVo = ExcelImpl.FindByEvent(shisakuEventCode)

            shisakuKaihatsuFugo = EventVo.ShisakuKaihatsuFugo
            shisakuEventName = EventVo.ShisakuEventName

            Dim BuhinEditListVo As New List(Of TShisakuBuhinEditVoHelperExcel)
            Dim GousyaEditInstlListVo As New List(Of EditBlockExcelBlockInstlVoHelper)
            'ベース車情報'
            Dim EventBaseListVo As New List(Of TShisakuEventBaseVo)

            Dim BaseGousyaList As List(Of TShisakuEventBaseVo)
            BaseGousyaList = New List(Of TShisakuEventBaseVo)
            BaseGousyaList = ExcelImpl.FindByBase(shisakuEventCode)

            For index As Integer = 0 To BaseGousyaList.Count - 1
                If index > 0 Then
                    If (BaseGousyaList(index).HyojijunNo = (BaseGousyaList(index - 1).HyojijunNo + 1)) Then
                        EventBaseListVo.Add(BaseGousyaList(index))
                    Else
                        '抜けてる表示順Noの数だけ空行を挿入'
                        Dim row As Integer = (BaseGousyaList(index).HyojijunNo - BaseGousyaList(index - 1).HyojijunNo) - 1
                        '   2012/12/18 ダミー列の開始位置用
                        Dim intDummyIndex As Integer = BaseGousyaList(index).HyojijunNo - row
                        For index2 As Integer = 0 To row - 1
                            Dim dummy As TShisakuEventBaseVo
                            dummy = New TShisakuEventBaseVo
                            '   2012/12/18 前にダミー列があった場合を考慮して開始位置を調整   By柳沼
                            dummy.HyojijunNo = intDummyIndex + index2
                            'dummy.HyojijunNo = index + index2
                            EventBaseListVo.Add(dummy)
                        Next
                        EventBaseListVo.Add(BaseGousyaList(index))
                    End If
                ElseIf index = 0 Then
                    EventBaseListVo.Add(BaseGousyaList(index))
                End If
            Next

            '部課コードに関わるブロックNoを抽出'
            Dim BlockNoList As New List(Of TShisakuBuhinEditVoHelperExcel)

            '処理速度向上の為、ブロックごとにデータを取得するように修正
            'ここでは何故かContextEditDedLockが発生（デバッグ時にのみ確認）
            '大量・・・といっても１０００件程度のデータをDataAdaptorでFillすると
            '非常に時間がかかる為、ブロック単位に小分けして取得し後にリストに
            '格納するように改修。その結果大幅に時間短縮に成功した
            If Not blockNosList Is Nothing Then
                If blockNosList.Count > 1 Then
                    For Each blVo As String In blockNosList
                        Dim blockVo As New TShisakuBuhinEditVoHelperExcel
                        blockVo.ShisakuEventCode = shisakuEventCode
                        blockVo.ShisakuBukaCode = shisakuBukaCode
                        blockVo.ShisakuBlockNo = blVo
                        BlockNoList.Add(blockVo)
                    Next
                Else
                    BlockNoList = ExcelImpl.FindByBlockGroup(shisakuEventCode, shisakuBukaCode)
                End If
            Else
                '全ブロックExcel出力の場合
                BlockNoList = ExcelImpl.FindByBlockGroup(shisakuEventCode, shisakuBukaCode)
            End If

            If isBase Then
                '全ブロックNo改訂(ベース)'
                For Each vo As TShisakuBuhinEditVoHelperExcel In BlockNoList
                    Dim Bvo As List(Of TShisakuBuhinEditVoHelperExcel)
                    Dim gVo As List(Of EditBlockExcelBlockInstlVoHelper)
                    Bvo = ExcelImpl.FindByAllBuhinEditBase(shisakuEventCode, shisakuBukaCode, vo.ShisakuBlockNo)
                    BuhinEditListVo.AddRange(Bvo)
                    gVo = ExcelImpl.FindByAllGousyaExcelBase(shisakuEventCode, shisakuBukaCode, vo.ShisakuBlockNo)
                    GousyaEditInstlListVo.AddRange(gVo)
                Next
            Else

                If Not StringUtil.IsEmpty(shisakuBlockNoKaiteiNo) Then
                    '全ブロックNo改訂No(指定)'
                    For Each vo As TShisakuBuhinEditVoHelperExcel In BlockNoList
                        Dim Bvo As List(Of TShisakuBuhinEditVoHelperExcel)
                        Dim gVo As List(Of EditBlockExcelBlockInstlVoHelper)
                        Bvo = ExcelImpl.FindByAllBuhinEdit(shisakuEventCode, shisakuBukaCode, vo.ShisakuBlockNo)
                        BuhinEditListVo.AddRange(Bvo)
                        gVo = ExcelImpl.FindByAllGousyaExcel(shisakuEventCode, shisakuBukaCode, vo.ShisakuBlockNo)
                        GousyaEditInstlListVo.AddRange(gVo)
                    Next

                Else
                    '全ブロックNo改訂No(最新)'
                    ''↓↓2014/09/18 酒井 ADD BEGIN
                    '該当イベント取得
                    Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
                    Dim shisakueventVo As TShisakuEventVo
                    shisakueventVo = eventDao.FindByPk(shisakuEventCode)
                    If shisakueventVo.BlockAlertKind = 2 Then
                        Dim Bvo As List(Of TShisakuBuhinEditVoHelperExcel)
                        Bvo = ExcelImpl.FindByAllBlockBuhinEdit(shisakuEventCode, shisakuBukaCode)
                        BuhinEditListVo.AddRange(Bvo)
                        For Each vo As TShisakuBuhinEditVoHelperExcel In BlockNoList
                            Dim gVo As List(Of EditBlockExcelBlockInstlVoHelper)
                            gVo = ExcelImpl.FindByAllGousyaExcel(shisakuEventCode, shisakuBukaCode, vo.ShisakuBlockNo)
                            GousyaEditInstlListVo.AddRange(gVo)
                        Next
                    Else
                        ''↑↑2014/09/18 酒井 ADD END
                        For Each vo As TShisakuBuhinEditVoHelperExcel In BlockNoList
                            Dim Bvo As List(Of TShisakuBuhinEditVoHelperExcel)
                            Dim gVo As List(Of EditBlockExcelBlockInstlVoHelper)
                            Bvo = ExcelImpl.FindByAllBuhinEdit(shisakuEventCode, shisakuBukaCode, vo.ShisakuBlockNo)
                            BuhinEditListVo.AddRange(Bvo)
                            gVo = ExcelImpl.FindByAllGousyaExcel(shisakuEventCode, shisakuBukaCode, vo.ShisakuBlockNo)
                            GousyaEditInstlListVo.AddRange(gVo)
                        Next
                        ''↓↓2014/09/18 酒井 ADD BEGIN
                    End If
                    ''↑↑2014/09/18 酒井 ADD END

                End If

            End If

            ''部課コードに関わる部品編集情報を取得'

            SetSheetAllBlock(xls, BuhinEditListVo, GousyaEditInstlListVo, shisakuEventName, shisakuKaihatsuFugo, EventBaseListVo)
        End Sub



        ''' <summary>
        ''' シートの設定
        ''' </summary>
        ''' <param name="BuhinEditListVo">部品編集情報+INSTL品番+号車</param>
        ''' <param name="GousyaEditInstlListVo">設計ブロックINSTL情報+部品番号表示順+部品編集INSTL情報の員数</param>
        ''' <param name="shisakuEventName">イベント名称</param>
        ''' <param name="shisakuKaihatsuFugo">開発符号</param>
        ''' <remarks></remarks>
        Private Sub SetSheetAllBlock(ByVal xls As ShisakuExcel, _
                                     ByVal BuhinEditListVo As List(Of TShisakuBuhinEditVoHelperExcel), _
                                     ByVal GousyaEditInstlListVo As List(Of EditBlockExcelBlockInstlVoHelper), _
                                     ByVal shisakuEventName As String, ByVal shisakuKaihatsuFugo As String, _
                                     ByVal EventBaseListVo As List(Of TShisakuEventBaseVo))
            ''↓↓2014/08/08 Ⅰ.10.全ブロックEXCEL出力での出力内容変更_e) (TES)張 CHG BEGIN
            ''xls.SetActiveSheet(1)
            xls.SetActiveSheet(2)
            ''↑↑2014/08/08 Ⅰ.10.全ブロックEXCEL出力での出力内容変更_e) (TES)張 CHG END
            SetSheetHead(xls, shisakuKaihatsuFugo, shisakuEventName)

            'タイトル部の設定'
            SetSheetTitle(xls, GousyaEditInstlListVo, EventBaseListVo)
            setAllBody(xls, BuhinEditListVo, GousyaEditInstlListVo, EventBaseListVo)
            SetRowCol(xls)



        End Sub

        ''' <summary>
        ''' ヘッダー部を設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <param name="shisakuEventName">イベント名称</param>
        ''' <param name="shisakuKaihatsuFugo">開発符号</param>
        ''' <remarks></remarks>
        Private Sub SetSheetHead(ByVal xls As ShisakuExcel, ByVal shisakuKaihatsuFugo As String, ByVal shisakuEventName As String)
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

            '最初に実行
            Dim gousyaLastHyoujiJyun As Integer = EventBaseListVo(EventBaseListVo.Count - 1).HyojijunNo
            For i As Integer = 0 To gousyaLastHyoujiJyun
                xls.SetValue(COL_START_GOUSYA + i, INSU_TITLE_ROW, alphabetList(i).Label)
                xls.MergeCells(COL_START_GOUSYA + i, GOUSYA_ROW, _
                               COL_START_GOUSYA + i, MERGE_ROW, True)
                xls.SetOrientation(COL_START_GOUSYA + i, GOUSYA_ROW, _
                                   COL_START_GOUSYA + i, GOUSYA_ROW, ShisakuExcel.XlOrientation.xlVertical)
                gousyaIndex = gousyaIndex + 1
            Next

            xls.SetValue(COL_START_GOUSYA, TITLE_ROW, "員数")
            Dim gousya As New List(Of String)
            For index As Integer = 0 To EventBaseListVo.Count - 1
                xls.SetValue(COL_START_GOUSYA + EventBaseListVo(index).HyojijunNo, GOUSYA_ROW, EventBaseListVo(index).ShisakuGousya)
            Next

            '号車名'
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
            ''↓↓2014/07/25 Ⅰ.2.管理項目追加_aw) (TES)張 ADD BEGIN
            gousyaIndex = gousyaIndex + 1
            ''↓↓2014/09/04 Ⅰ.2.管理項目追加_aw) 酒井 ADD BEGIN
            'xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + 5 + gousyaIndex, GOUSYA_ROW, True)
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + 6 + gousyaIndex, GOUSYA_ROW, True)
            ''↑↑2014/09/04 Ⅰ.2.管理項目追加_aw) 酒井 ADD END
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, "作り方")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "製作方法")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "型仕様１")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "型仕様２")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "型仕様３")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "治具")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "納入見通し")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "部品製作規模・概要")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)
            ''↑↑2014/07/25 Ⅰ.2.管理項目追加_aw) (TES)張 ADD END

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

            ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
            '材料情報'
            gousyaIndex = gousyaIndex + 1
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + 1 + gousyaIndex, GOUSYA_ROW, True)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, "材料情報")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "製品長")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "製品幅")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)
           
            'データ項目'
            gousyaIndex = gousyaIndex + 1
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + 3 + gousyaIndex, GOUSYA_ROW, True)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, "データ項目")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "改訂№")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "エリア名")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "セット名")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, "改訂情報")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, MERGE_ROW, XlHAlign.xlHAlignCenter)

            ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END

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

            ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ah) (TES)張 ADD BEGIN
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(shisakuEventCode)
            ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ah) (TES)張 ADD END

            '２次元配列の列数を算出
            For i As Integer = 0 To buhinEditList.Count - 1
                If Not i = 0 Then
                    '--------------------------------------------------------------------------------------------
                    'レベル’
                    '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                    '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                    Dim buhinEditListLevel As String
                    Dim buhinEditListLevelMae As String
                    If StringUtil.IsEmpty(buhinEditList(i).Level) Then
                        buhinEditListLevel = ""
                    Else
                        buhinEditListLevel = buhinEditList(i).Level
                    End If
                    If StringUtil.IsEmpty(buhinEditList(i - 1).Level) Then
                        buhinEditListLevelMae = ""
                    Else
                        buhinEditListLevelMae = buhinEditList(i - 1).Level
                    End If
                    '--------------------------------------------------------------------------------------------

                    If StringUtil.Equals(buhinEditListLevel, buhinEditListLevelMae) Then
                        If StringUtil.Equals(buhinEditList(i).BuhinNo, buhinEditList(i - 1).BuhinNo) Then
                            If StringUtil.Equals(buhinEditList(i).BuhinNoKbn, buhinEditList(i - 1).BuhinNoKbn) Then
                                If StringUtil.Equals(buhinEditList(i).InstlDataKbn, buhinEditList(i - 1).InstlDataKbn) Then

                                    If StringUtil.Equals(buhinEditList(i).KyoukuSection, buhinEditList(i - 1).KyoukuSection) Then
                                        If StringUtil.Equals(buhinEditList(i).Saishiyoufuka, buhinEditList(i - 1).Saishiyoufuka) Then
                                            If StringUtil.Equals(buhinEditList(i).ShukeiCode, buhinEditList(i - 1).ShukeiCode) Then
                                                ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ah) (TES)張 ADD BEGIN
                                                If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                                                    If StringUtil.Equals(buhinEditList(i).BaseInstlFlg, buhinEditList(i - 1).BaseInstlFlg) Then
                                                        'If StringUtil.Equals(buhinEditList(i).BaseBuhinFlg, buhinEditList(i - 1).BaseBuhinFlg) Then
                                                    Else
                                                        maxRowNumber = maxRowNumber + 1
                                                    End If
                                                End If
                                                ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ah) (TES)張 ADD END
                                            Else
                                                If StringUtil.Equals(buhinEditList(i).SiaShukeiCode, buhinEditList(i - 1).SiaShukeiCode) Then
                                                    ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ah) (TES)張 ADD BEGIN
                                                    If eventVo.BlockAlertKind = 2 Then
                                                        If StringUtil.Equals(buhinEditList(i).BaseInstlFlg, buhinEditList(i - 1).BaseInstlFlg) Then
                                                            'If StringUtil.Equals(buhinEditList(i).BaseBuhinFlg, buhinEditList(i - 1).BaseBuhinFlg) Then
                                                        Else
                                                            maxRowNumber = maxRowNumber + 1
                                                        End If
                                                    End If
                                                    ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ah) (TES)張 ADD END
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
                        Else
                            maxRowNumber = maxRowNumber + 1
                        End If
                    End If
            Next
            ''↓↓2014/08/08 Ⅰ.2.管理項目追加_ax) (TES)張 CHG BEGIN
            ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
            Dim dataMatrix(maxRowNumber, COL_BUHIN_NAME + GousyaCount + 27) As String
            ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END
            ''↑↑2014/08/08 Ⅰ.2.管理項目追加_ax) (TES)張 CHG END
            For index As Integer = 0 To buhinEditList.Count - 1

                If Not index = 0 Then
                    '--------------------------------------------------------------------------------------------
                    'レベル’
                    '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                    '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                    Dim buhinEditListLevel As String
                    Dim buhinEditListLevelMae As String
                    If StringUtil.IsEmpty(buhinEditList(index).Level) Then
                        buhinEditListLevel = ""
                    Else
                        buhinEditListLevel = buhinEditList(index).Level
                    End If
                    If StringUtil.IsEmpty(buhinEditList(index - 1).Level) Then
                        buhinEditListLevelMae = ""
                    Else
                        buhinEditListLevelMae = buhinEditList(index - 1).Level
                    End If
                    '--------------------------------------------------------------------------------------------

                    If StringUtil.Equals(buhinEditListLevel, buhinEditListLevelMae) Then
                        If StringUtil.Equals(buhinEditList(index).BuhinNo, buhinEditList(index - 1).BuhinNo) Then
                            If StringUtil.Equals(buhinEditList(index).BuhinNoKbn, buhinEditList(index - 1).BuhinNoKbn) Then
                                If StringUtil.Equals(buhinEditList(index).InstlDataKbn, buhinEditList(index - 1).InstlDataKbn) Then

                                    If StringUtil.Equals(buhinEditList(index).KyoukuSection, buhinEditList(index - 1).KyoukuSection) Then
                                        If StringUtil.Equals(buhinEditList(index).Saishiyoufuka, buhinEditList(index - 1).Saishiyoufuka) Then
                                            If StringUtil.Equals(buhinEditList(index).ShukeiCode, buhinEditList(index - 1).ShukeiCode) Then
                                                ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ah) (TES)張 CHG BEGIN
                                                'rowIndex = rowIndex - 1
                                                'MergeFlag = True
                                                If eventVo.BlockAlertKind = 2 Then
                                                    If StringUtil.Equals(buhinEditList(index).BaseInstlFlg, buhinEditList(index - 1).BaseInstlFlg) Then
                                                        'If StringUtil.Equals(buhinEditList(index).BaseBuhinFlg, buhinEditList(index - 1).BaseBuhinFlg) Then
                                                        rowIndex = rowIndex - 1
                                                        MergeFlag = True
                                                    End If
                                                Else
                                                    rowIndex = rowIndex - 1
                                                    MergeFlag = True
                                                End If
                                                ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ah) (TES)張 CHG END
                                            Else
                                                If StringUtil.Equals(buhinEditList(index).SiaShukeiCode, buhinEditList(index - 1).SiaShukeiCode) Then
                                                    ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ah) (TES)張 CHG BEGIN
                                                    'rowIndex = rowIndex - 1
                                                    'MergeFlag = True
                                                    If eventVo.BlockAlertKind = 2 Then
                                                        If StringUtil.Equals(buhinEditList(index).BaseInstlFlg, buhinEditList(index - 1).BaseInstlFlg) Then
                                                            'If StringUtil.Equals(buhinEditList(index).BaseBuhinFlg, buhinEditList(index - 1).BaseBuhinFlg) Then
                                                            rowIndex = rowIndex - 1
                                                            MergeFlag = True
                                                        End If
                                                    Else
                                                        rowIndex = rowIndex - 1
                                                        MergeFlag = True
                                                    End If
                                                    ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ah) (TES)張 CHG END
                                                End If
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
                    '2014/07/15 変更
                    Dim insu As String = dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1)

                    '2014/07/15 変更
                    If StringUtil.IsEmpty(insu) Then
                        If buhinEditList(index).InsuSuryo < 0 Then
                            dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = "**"
                        Else
                            dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = buhinEditList(index).InsuSuryo
                        End If
                    Else
                        If StringUtil.Equals(insu, "**") Then
                            dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = "**"
                        Else
                            If buhinEditList(index).InsuSuryo < 0 Then
                                dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = "**"
                            Else
                                Dim totalInsuSuryo As Integer = Integer.Parse(insu)
                                totalInsuSuryo = totalInsuSuryo + buhinEditList(index).InsuSuryo

                                dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = totalInsuSuryo
                            End If
                        End If
                    End If

                Else

                    'ブロックNo'
                    dataMatrix(rowIndex, COL_BLOCK_NO - 1) = buhinEditList(index).ShisakuBlockNo
                    '--------------------------------------------------------------------------------------------
                    'レベル’
                    '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                    '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                    If StringUtil.IsEmpty(buhinEditList(index).Level) Then
                        dataMatrix(rowIndex, COL_LEVEL - 1) = ""
                    Else
                        dataMatrix(rowIndex, COL_LEVEL - 1) = buhinEditList(index).Level
                    End If
                    '--------------------------------------------------------------------------------------------
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

                    '2014/07/15 変更
                    Dim insu As String = dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1)

                    '2014/07/15 変更
                    If StringUtil.IsEmpty(insu) Then
                        If buhinEditList(index).InsuSuryo < 0 Then
                            dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = "**"
                        Else
                            dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = buhinEditList(index).InsuSuryo
                        End If
                    Else
                        If StringUtil.Equals(insu, "**") Then
                            dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = "**"
                        Else
                            If buhinEditList(index).InsuSuryo < 0 Then
                                dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = "**"
                            Else
                                Dim totalInsuSuryo As Integer = Integer.Parse(insu)
                                totalInsuSuryo = totalInsuSuryo + buhinEditList(index).InsuSuryo

                                dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1) = totalInsuSuryo
                            End If
                        End If
                    End If

                    '↑↑2014/10/29 酒井 ADD END

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

                    '製作方法'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 3 - 1) = buhinEditList(index).TsukurikataSeisaku
                    '型仕様１'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 4 - 1) = buhinEditList(index).TsukurikataKatashiyou1
                    '型仕様２'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 5 - 1) = buhinEditList(index).TsukurikataKatashiyou2
                    '型仕様３'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 6 - 1) = buhinEditList(index).TsukurikataKatashiyou3
                    '治具'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 7 - 1) = buhinEditList(index).TsukurikataTigu
                    '納入見通し'
                    If buhinEditList(index).TsukurikataNounyu = "0" Or String.IsNullOrEmpty(buhinEditList(index).TsukurikataNounyu.ToString) Then
                        'EditGousyaCount = EditGousyaCount + 1
                        dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 8 - 1) = ""
                    Else
                        'EditGousyaCount = EditGousyaCount + 1
                        dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 8 - 1) = buhinEditList(index).TsukurikataNounyu
                    End If
                    '部品製作規模・概要'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 9 - 1) = buhinEditList(index).TsukurikataKibo
                    '材質'
                    '規格１'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 10 - 1) = buhinEditList(index).ZaishituKikaku1
                    '規格２'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 11 - 1) = buhinEditList(index).ZaishituKikaku2
                    '規格３'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 12 - 1) = buhinEditList(index).ZaishituKikaku3
                    'メッキ'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 13 - 1) = buhinEditList(index).ZaishituMekki

                    '板厚'
                    '板厚数量'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 14 - 1) = buhinEditList(index).ShisakuBankoSuryo
                    '板厚数量U'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 15 - 1) = buhinEditList(index).ShisakuBankoSuryoU

                    ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
                    '製品長
                    If StringUtil.IsNotEmpty(buhinEditList(index).MaterialInfoLength) Then
                        dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 16 - 1) = buhinEditList(index).MaterialInfoLength
                    End If
                    '製品幅
                    If StringUtil.IsNotEmpty(buhinEditList(index).MaterialInfoWidth) Then
                        dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 17 - 1) = buhinEditList(index).MaterialInfoWidth
                    End If
                    '改訂№
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 18 - 1) = buhinEditList(index).DataItemKaiteiNo
                    'エリア名
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 19 - 1) = buhinEditList(index).DataItemAreaName
                    'セット名
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 20 - 1) = buhinEditList(index).DataItemSetName
                    '改訂情報
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 21 - 1) = buhinEditList(index).DataItemKaiteiInfo
                    ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END
                    ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                    '試作部品費'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 22 - 1) = buhinEditList(index).ShisakuBuhinHi
                    '試作型費'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 23 - 1) = buhinEditList(index).ShisakuKataHi
                    '部品NOTE' 2012/03/09追加
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 24 - 1) = buhinEditList(index).BuhinNote
                    '備考'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 25 - 1) = buhinEditList(index).Bikou
                    '担当者名'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 26 - 1) = buhinEditList(index).UserId
                    '電話番号'
                    'EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount + 27 - 1) = buhinEditList(index).TelNo
                    ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END
                    ''↑↑2014/07/25 Ⅰ.2.管理項目追加_ax) (TES)張 CHG END


                    If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                        If buhinEditList(index).BaseInstlFlg = 1 Then
                            xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount + 21, START_ROW + rowIndex, RGB(176, 215, 237))
                        End If
                    End If
                    End If


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
            ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ai) (TES)張 ADD BEGIN
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(shisakuEventCode)
            ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ai) (TES)張 ADD END
            For index As Integer = 0 To buhinEditList.Count - 1



                'マージ可能ならマージする'
                If Not index = 0 Then
                    '--------------------------------------------------------------------------------------------
                    'レベル’
                    '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                    '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                    Dim buhinEditListLevel As String
                    Dim buhinEditListLevelMae As String
                    If StringUtil.IsEmpty(buhinEditList(index).Level) Then
                        buhinEditListLevel = ""
                    Else
                        buhinEditListLevel = buhinEditList(index).Level
                    End If
                    If StringUtil.IsEmpty(buhinEditList(index - 1).Level) Then
                        buhinEditListLevelMae = ""
                    Else
                        buhinEditListLevelMae = buhinEditList(index - 1).Level
                    End If
                    '--------------------------------------------------------------------------------------------

                    If StringUtil.Equals(buhinEditListLevel, buhinEditListLevelMae) Then
                        If StringUtil.Equals(buhinEditList(index).BuhinNo, buhinEditList(index - 1).BuhinNo) Then
                            If StringUtil.Equals(buhinEditList(index).KyoukuSection, buhinEditList(index - 1).KyoukuSection) Then
                                If StringUtil.Equals(buhinEditList(index).Saishiyoufuka, buhinEditList(index - 1).Saishiyoufuka) Then
                                    If StringUtil.Equals(buhinEditList(index).ShukeiCode, buhinEditList(index - 1).ShukeiCode) Then
                                        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ai) (TES)張 CHG BEGIN
                                        'rowIndex = rowIndex - 1
                                        'MergeFlag = True
                                        If eventVo.BlockAlertKind = 2 Then
                                            If StringUtil.Equals(buhinEditList(index).BaseBuhinFlg, buhinEditList(index - 1).BaseBuhinFlg) Then
                                                rowIndex = rowIndex - 1
                                                MergeFlag = True
                                            End If
                                        Else
                                            rowIndex = rowIndex - 1
                                            MergeFlag = True
                                        End If
                                        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ai) (TES)張 CHG END
                                    Else
                                        If StringUtil.Equals(buhinEditList(index).SiaShukeiCode, buhinEditList(index - 1).SiaShukeiCode) Then
                                            ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ai) (TES)張 CHG BEGIN
                                            'rowIndex = rowIndex - 1
                                            'MergeFlag = True
                                            If eventVo.BlockAlertKind = 2 Then
                                                If StringUtil.Equals(buhinEditList(index).BaseBuhinFlg, buhinEditList(index - 1).BaseBuhinFlg) Then
                                                    rowIndex = rowIndex - 1
                                                    MergeFlag = True
                                                End If
                                            Else
                                                rowIndex = rowIndex - 1
                                                MergeFlag = True
                                            End If
                                            ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ai) (TES)張 CHG END
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ai) (TES)張 ADD BEGIN
                If eventVo.BlockAlertKind Then
                    If buhinEditList(index).BaseBuhinFlg = 1 Then
                        ''↓↓2014/09/03 Ⅰ.3.設計編集 ベース改修専用化_ai) 酒井 ADD BEGIN
                        'xls.SetBackColor(COL_LEVEL - 1, START_ROW + rowIndex, COL_BUHIN_NAME - 1, START_ROW + rowIndex, Color.DimGray)
                        xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_BUHIN_NAME, START_ROW + rowIndex, RGB(169, 169, 169))
                        ''↑↑2014/09/03 Ⅰ.3.設計編集 ベース改修専用化_ai) 酒井 ADD END
                    End If
                End If
                ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ai) (TES)張 ADD END


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
                    '--------------------------------------------------------------------------------------------
                    'レベル’
                    '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                    '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                    If StringUtil.IsEmpty(buhinEditList(index).Level) Then
                        xls.SetValue(COL_LEVEL, START_ROW + rowIndex, "")
                    Else
                        xls.SetValue(COL_LEVEL, START_ROW + rowIndex, buhinEditList(index).Level)
                    End If
                    '--------------------------------------------------------------------------------------------
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

                    ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
                    '製品長'
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 9, START_ROW + rowIndex, buhinEditList(index).MaterialInfoLength)
                    '製品幅'
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 10, START_ROW + rowIndex, buhinEditList(index).MaterialInfoWidth)
                    'データ項目'
                    '改訂№'
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 11, START_ROW + rowIndex, buhinEditList(index).DataItemKaiteiNo)
                    'エリア名'
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 12, START_ROW + rowIndex, buhinEditList(index).DataItemAreaName)
                    'セット名'
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 13, START_ROW + rowIndex, buhinEditList(index).DataItemSetName)
                    '改訂情報'
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 14, START_ROW + rowIndex, buhinEditList(index).DataItemKaiteiInfo)

                    ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END
                    ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                    '試作部品費'
                    'EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 15, START_ROW + rowIndex, buhinEditList(index).ShisakuBuhinHi)
                    '試作型費'
                    'EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 16, START_ROW + rowIndex, buhinEditList(index).ShisakuKataHi)
                    '備考'
                    'EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 17, START_ROW + rowIndex, buhinEditList(index).Bikou)
                    '担当者名'
                    'EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 18, START_ROW + rowIndex, buhinEditList(index).UserId)
                    '電話番号'
                    'EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 19, START_ROW + rowIndex, buhinEditList(index).TelNo)
                    ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END

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
            ''↓↓2014/09/03 Ⅰ.3.設計編集 ベース改修専用化_ad) 酒井 ADD BEGIN
            xls.SetRowHeight(INSU_TITLE_ROW, INSU_TITLE_ROW, 40)
            ''↑↑2014/09/03 Ⅰ.3.設計編集 ベース改修専用化_ad) 酒井 ADD END

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
        ''ブロックNo
        Private COL_BLOCK_NO As Integer
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