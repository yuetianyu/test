Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports ShisakuCommon
Imports EBom.Common
Imports EBom.Excel
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util.LabelValue


Namespace ShisakuBuhinEditBlock.Export2Excel
    ''' <summary>
    ''' 複数ブロック比較する
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ExportShisakuBuhinEditConditonMultiBlockExcel

#Region "メンバ変数"
        Private shisakuEventCode As String
        Private shisakuBukaCode As String
        Private BlockAlertKind As String
        Private KounyuShijiFlg As String
        Private tantouUserName As String
        Private tel As String
        Private shisakuKaihatsuFugo As String
        Private shisakuEventName As String
        Private GousyaCount As Integer = 0
        Private EditGousyaCount As Integer = 0
        Private GousyaInsuCount As Integer = 0
        Private ExcelImpl As EditBlock2ExcelDao
        Private EventBaseListVo As List(Of TShisakuEventBaseVo)
        Private BlockNoList As List(Of TShisakuBuhinEditVoHelperExcel)

        Private TshisakuEventVo As TShisakuEventVo
#End Region

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String)
            Me.shisakuEventCode = shisakuEventCode
            Me.shisakuBukaCode = shisakuBukaCode
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo = eventDao.FindByPk(Me.shisakuEventCode)
            Me.BlockAlertKind = eventVo.BlockAlertKind
            Me.KounyuShijiFlg = eventVo.KounyuShijiFlg

            Dim dao As TShisakuEventDao = New TShisakuEventDaoImpl
            Me.TshisakuEventVo = dao.FindByPk(shisakuEventCode)


        End Sub

        ''' <summary>
        ''' 初期設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Initialize(Optional ByVal blockNosList As List(Of String) = Nothing)
            ExcelImpl = New EditBlock2ExcelDaoImpl
            'イベント情報を取得'
            Dim EventVo As New TShisakuEventVo
            EventVo = ExcelImpl.FindByEvent(shisakuEventCode)
            '開発符号セット'
            shisakuKaihatsuFugo = EventVo.ShisakuKaihatsuFugo
            'イベント名称セット'
            shisakuEventName = EventVo.ShisakuEventName

            setGousya()

            setBlockNos(blockNosList)

        End Sub

        ''' <summary>
        ''' ベース車情報の設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub setGousya()
            'ベース車情報'
            Dim BaseGousyaList As New List(Of TShisakuEventBaseVo)
            BaseGousyaList = ExcelImpl.FindByBase(shisakuEventCode)

            EventBaseListVo = New List(Of TShisakuEventBaseVo)

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
        End Sub

        ''' <summary>
        ''' ブロックNoのセット
        ''' </summary>
        ''' <param name="blockNosList">ブロックNoリスト</param>
        ''' <remarks></remarks>
        Private Sub setBlockNos(ByVal blockNosList As List(Of String))
            BlockNoList = New List(Of TShisakuBuhinEditVoHelperExcel)

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

        End Sub

        ''' <summary>
        ''' EXCEL出力
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <param name="fileName"></param>
        ''' <param name="shisakuBlockNoKaiteiNo"></param>
        ''' <remarks></remarks>
        Public Sub Excute(ByVal xls As ShisakuExcel, _
                          ByVal fileName As String, _
                          Optional ByVal shisakuBlockNoKaiteiNo As String = "")

            xls.SetActiveSheet(1)

            '最新の部品編集情報'
            Dim BuhinEditListVo As New List(Of TShisakuBuhinEditVoHelperExcel)
            '最新の号車情報'
            Dim GousyaEditInstlListVo As New List(Of EditBlockExcelBlockInstlVoHelper)

            'ベースの部品編集情報'
            Dim baseBuhinEditListVo As New List(Of TShisakuBuhinEditVoHelperExcel)
            'ベースの号車情報'
            Dim baseGousyaEditInstlListVo As New List(Of EditBlockExcelBlockInstlVoHelper)

            '全ブロックNo改訂No(最新)'
            For Each vo As TShisakuBuhinEditVoHelperExcel In BlockNoList
                Dim gVo As List(Of EditBlockExcelBlockInstlVoHelper)
                gVo = ExcelImpl.FindByAllGousyaExcel(shisakuEventCode, shisakuBukaCode, vo.ShisakuBlockNo)
                For Each y As EditBlockExcelBlockInstlVoHelper In gVo
                    GousyaEditInstlListVo.Add(y)
                Next
            Next

            SetSheetAllBlock(xls, _
                             BuhinEditListVo, _
                             GousyaEditInstlListVo, _
                             baseBuhinEditListVo, _
                             baseGousyaEditInstlListVo, _
                             shisakuEventName, _
                             shisakuKaihatsuFugo, _
                             EventBaseListVo)
        End Sub

        ''' <summary>
        ''' シートの設定
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <param name="BuhinEditListVo">部品編集情報+INSTL品番+号車</param>
        ''' <param name="GousyaEditInstlListVo">設計ブロックINSTL情報+部品番号表示順+部品編集INSTL情報の員数</param>
        ''' <param name="baseBuhinEditListVo">部品編集情報+INSTL品番+号車(ベース)</param>
        ''' <param name="baseGousyaEditInstlListVo">設計ブロックINSTL情報+部品番号表示順+部品編集INSTL情報の員数(ベース)</param>
        ''' <param name="shisakuEventName">イベント名称</param>
        ''' <param name="shisakuKaihatsuFugo">開発符号</param>
        ''' <param name="EventBaseListVo"></param>
        ''' <remarks></remarks>
        Private Sub SetSheetAllBlock(ByVal xls As ShisakuExcel, _
                                     ByVal BuhinEditListVo As List(Of TShisakuBuhinEditVoHelperExcel), _
                                     ByVal GousyaEditInstlListVo As List(Of EditBlockExcelBlockInstlVoHelper), _
                                     ByVal baseBuhinEditListVo As List(Of TShisakuBuhinEditVoHelperExcel), _
                                     ByVal baseGousyaEditInstlListVo As List(Of EditBlockExcelBlockInstlVoHelper), _
                                     ByVal shisakuEventName As String, ByVal shisakuKaihatsuFugo As String, _
                                     ByVal EventBaseListVo As List(Of TShisakuEventBaseVo))

            SetSheetHead(xls, shisakuKaihatsuFugo, shisakuEventName)

            'タイトル部の設定'
            SetSheetTitle(xls, GousyaEditInstlListVo, EventBaseListVo)
            setAllBody(xls, _
                       BuhinEditListVo, _
                       GousyaEditInstlListVo, _
                       baseBuhinEditListVo, _
                       baseGousyaEditInstlListVo, _
                       EventBaseListVo)

            '2012/09/11 kabasawa凡例を追加'
            xls.SetFont(11, 1, 12, 4, "MS Pゴシック", 11, &HFF0000)
            xls.SetValue(11, 1, "差分表示凡例:")
            xls.SetValue(12, 2, "変更箇所")
            xls.SetValue(12, 3, "変更前のデータ")
            xls.SetValue(12, 4, "削除されたデータ")
            'xls.SetValue(12, 5, "ベース車情報")

            xls.SetBackColor(11, 2, 11, 2, &H9FFFFF)
            xls.SetBackColor(11, 3, 11, 3, &HCCCCCC)
            xls.SetBackColor(11, 4, 11, 4, &HA0A0A0)
            'xls.SetBackColor(11, 5, 11, 5, &HB0D7ED)

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
                xls.SetValue(COL_START_GOUSYA + index, INSU_TITLE_ROW, alphabetList(index).Label)
                xls.SetValue(COL_START_GOUSYA + EventBaseListVo(index).HyojijunNo, GOUSYA_ROW, EventBaseListVo(index).ShisakuGousya)
            Next


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

            ''↓↓2014/09/04 Ⅰ.2.管理項目追加_aw) 酒井 ADD BEGIN
            gousyaIndex = gousyaIndex + 1
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + 6 + gousyaIndex, GOUSYA_ROW, True)
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
            ''↑↑2014/09/04 Ⅰ.2.管理項目追加_aw) 酒井 ADD END

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

            ''↓↓2014/12/26 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
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
            ''↑↑2014/12/26 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END

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
        Public Sub setAllBody(ByVal xls As ShisakuExcel, _
                              ByVal buhinEditList As List(Of TShisakuBuhinEditVoHelperExcel), _
                              ByVal gousyaList As List(Of EditBlockExcelBlockInstlVoHelper), _
                              ByVal baseBuhinEditList As List(Of TShisakuBuhinEditVoHelperExcel), _
                              ByVal basegousyaList As List(Of EditBlockExcelBlockInstlVoHelper), _
                              ByVal EventBaseListVo As List(Of TShisakuEventBaseVo))


            'マージさせるため'
            Dim rowIndex As Integer = 0
            Dim MergeFlag As Boolean = False

            '全ブロックNo改訂No(最新)'
            For Each vo As TShisakuBuhinEditVoHelperExcel In BlockNoList
                buhinEditList = ExcelImpl.FindByAllBuhinEdit(shisakuEventCode, shisakuBukaCode, vo.ShisakuBlockNo)
                MergeList(buhinEditList)

                baseBuhinEditList = ExcelImpl.FindByAllBuhinEditBase(shisakuEventCode, shisakuBukaCode, vo.ShisakuBlockNo)
                MergeList(baseBuhinEditList)


                For index As Integer = 0 To buhinEditList.Count - 1

                    'マージ可能ならマージする'
                    MergeFlag = False
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
                            If StringUtil.Equals(buhinEditList(index).BuhinNo, buhinEditList(index - 1).BuhinNo) _
                               And Me.BlockAlertKind = "2" And Me.KounyuShijiFlg = "0" _
                               And StringUtil.Equals(buhinEditList(index).BaseInstlFlg, buhinEditList(index - 1).BaseInstlFlg) Then
                                '-------------------------------------------------------------------------------
                                '2013/04/05 供給セクションのチェックを追加
                                Dim kyoku1 As String = buhinEditList(index).KyoukuSection
                                Dim kyoku2 As String = buhinEditList(index - 1).KyoukuSection
                                If StringUtil.IsEmpty(kyoku1) Then
                                    kyoku1 = ""
                                End If
                                If StringUtil.IsEmpty(kyoku2) Then
                                    kyoku2 = ""
                                End If
                                '-------------------------------------------------------------------------------
                                If StringUtil.Equals(kyoku1, kyoku2) Then
                                    '-------------------------------------------------------------------------------
                                    '2013/04/03 再使用不可のチェックを追加
                                    Dim saishiyou1 As String = buhinEditList(index).Saishiyoufuka
                                    Dim saishiyou2 As String = buhinEditList(index - 1).Saishiyoufuka
                                    If StringUtil.IsEmpty(saishiyou1) Then
                                        saishiyou1 = ""
                                    End If
                                    If StringUtil.IsEmpty(saishiyou2) Then
                                        saishiyou2 = ""
                                    End If
                                    '-------------------------------------------------------------------------------
                                    If StringUtil.Equals(saishiyou1, saishiyou2) Then
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
                            If StringUtil.Equals(buhinEditList(index).BuhinNo, buhinEditList(index - 1).BuhinNo) _
                               And Me.BlockAlertKind <> "2" Then
                                '-------------------------------------------------------------------------------
                                '2013/04/05 供給セクションのチェックを追加
                                Dim kyoku1 As String = buhinEditList(index).KyoukuSection
                                Dim kyoku2 As String = buhinEditList(index - 1).KyoukuSection
                                If StringUtil.IsEmpty(kyoku1) Then
                                    kyoku1 = ""
                                End If
                                If StringUtil.IsEmpty(kyoku2) Then
                                    kyoku2 = ""
                                End If
                                '-------------------------------------------------------------------------------
                                If StringUtil.Equals(kyoku1, kyoku2) Then
                                    '-------------------------------------------------------------------------------
                                    '2013/04/03 再使用不可のチェックを追加
                                    Dim saishiyou1 As String = buhinEditList(index).Saishiyoufuka
                                    Dim saishiyou2 As String = buhinEditList(index - 1).Saishiyoufuka
                                    If StringUtil.IsEmpty(saishiyou1) Then
                                        saishiyou1 = ""
                                    End If
                                    If StringUtil.IsEmpty(saishiyou2) Then
                                        saishiyou2 = ""
                                    End If
                                    '-------------------------------------------------------------------------------
                                    If StringUtil.Equals(saishiyou1, saishiyou2) Then
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

                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 3, START_ROW + rowIndex, buhinEditList(index).TsukurikataSeisaku)
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 4, START_ROW + rowIndex, buhinEditList(index).TsukurikataKatashiyou1)
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 5, START_ROW + rowIndex, buhinEditList(index).TsukurikataKatashiyou2)
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 6, START_ROW + rowIndex, buhinEditList(index).TsukurikataKatashiyou3)
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 7, START_ROW + rowIndex, buhinEditList(index).TsukurikataTigu)
                        If StringUtil.IsEmpty(buhinEditList(index).TsukurikataNounyu) Or buhinEditList(index).TsukurikataNounyu = "0" Then
                            xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 8, START_ROW + rowIndex, "")
                        Else
                            xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 8, START_ROW + rowIndex, buhinEditList(index).TsukurikataNounyu)
                        End If
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 9, START_ROW + rowIndex, buhinEditList(index).TsukurikataKibo)

                        '材質'
                        '規格１'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 10, START_ROW + rowIndex, buhinEditList(index).ZaishituKikaku1)
                        '規格２'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 11, START_ROW + rowIndex, buhinEditList(index).ZaishituKikaku2)
                        '規格３'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 12, START_ROW + rowIndex, buhinEditList(index).ZaishituKikaku3)
                        'メッキ'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 13, START_ROW + rowIndex, buhinEditList(index).ZaishituMekki)

                        '板厚'
                        '板厚数量'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 14, START_ROW + rowIndex, buhinEditList(index).ShisakuBankoSuryo)
                        '板厚数量U'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 15, START_ROW + rowIndex, buhinEditList(index).ShisakuBankoSuryoU)
                        ''↓↓2014/12/26 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
                        '材料情報'
                        '製品長'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 16, START_ROW + rowIndex, buhinEditList(index).MaterialInfoLength)
                        '製品幅'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 17, START_ROW + rowIndex, buhinEditList(index).MaterialInfoWidth)
                        'データ項目
                        '改訂№'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 18, START_ROW + rowIndex, buhinEditList(index).DataItemKaiteiNo)
                        'エリア名'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 19, START_ROW + rowIndex, buhinEditList(index).DataItemAreaName)
                        'セット名'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 20, START_ROW + rowIndex, buhinEditList(index).DataItemSetName)
                        '改訂情報'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 21, START_ROW + rowIndex, buhinEditList(index).DataItemKaiteiInfo)
                        ''↑↑2014/12/26 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END
                        ''↓↓2014/12/26 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END
                        '試作部品費'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 22, START_ROW + rowIndex, buhinEditList(index).ShisakuBuhinHi)
                        '試作型費'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 23, START_ROW + rowIndex, buhinEditList(index).ShisakuKataHi)
                        '部品NOTE'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 24, START_ROW + rowIndex, buhinEditList(index).BuhinNote)
                        '備考'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 25, START_ROW + rowIndex, buhinEditList(index).Bikou)
                        '担当者名'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 26, START_ROW + rowIndex, buhinEditList(index).UserId)
                        '電話番号'
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 27, START_ROW + rowIndex, buhinEditList(index).TelNo)
                        ''↑↑2014/12/26 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END

                        If Me.TshisakuEventVo.BlockAlertKind = 2 And Me.TshisakuEventVo.KounyuShijiFlg = "0" Then
                            If buhinEditList(index).BaseBuhinFlg = 1 Then
                                ''↓↓2014/12/29 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                                ' xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount + 21, START_ROW + rowIndex, RGB(176, 215, 237))
                                xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount + 27, START_ROW + rowIndex, RGB(176, 215, 237))
                                ''↑↑2014/12/29 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END
                            End If
                        End If

                    End If

                    If Not AllChk(buhinEditList(index), baseBuhinEditList) Then
                        AllChkColor(xls, buhinEditList(index), baseBuhinEditList, rowIndex, True)
                    End If

                    rowIndex = rowIndex + 1
                Next

                WriteChk(baseBuhinEditList)

                If BlockAlertKind <> "2" Then

                    For index As Integer = 0 To baseBuhinEditList.Count - 1

                        If Not baseBuhinEditList(index).Flag Then

                            'マージ可能ならマージする'
                            MergeFlag = False
                            If Not index = 0 Then

                                '--------------------------------------------------------------------------------------------
                                'レベル’
                                '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                                '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                                Dim baseBuhinEditListLevel As String
                                Dim baseBuhinEditListLevelMae As String
                                If StringUtil.IsEmpty(baseBuhinEditList(index).Level) Then
                                    baseBuhinEditListLevel = ""
                                Else
                                    baseBuhinEditListLevel = baseBuhinEditList(index).Level
                                End If
                                If StringUtil.IsEmpty(baseBuhinEditList(index - 1).Level) Then
                                    baseBuhinEditListLevelMae = ""
                                Else
                                    baseBuhinEditListLevelMae = baseBuhinEditList(index - 1).Level
                                End If
                                '--------------------------------------------------------------------------------------------

                                If StringUtil.Equals(baseBuhinEditListLevel, baseBuhinEditListLevelMae) Then
                                    If StringUtil.Equals(baseBuhinEditList(index).BuhinNo, baseBuhinEditList(index - 1).BuhinNo) Then
                                        '-------------------------------------------------------------------------------
                                        '2013/04/05 供給セクションのチェックを追加
                                        Dim kyoku1 As String = baseBuhinEditList(index).KyoukuSection
                                        Dim kyoku2 As String = baseBuhinEditList(index - 1).KyoukuSection
                                        If StringUtil.IsEmpty(kyoku1) Then
                                            kyoku1 = ""
                                        End If
                                        If StringUtil.IsEmpty(kyoku2) Then
                                            kyoku2 = ""
                                        End If
                                        '-------------------------------------------------------------------------------
                                        If StringUtil.Equals(kyoku1, kyoku2) Then
                                            '-------------------------------------------------------------------------------
                                            '2013/04/03 再使用不可のチェックを追加
                                            Dim saishiyou1 As String = baseBuhinEditList(index).Saishiyoufuka
                                            Dim saishiyou2 As String = baseBuhinEditList(index - 1).Saishiyoufuka
                                            If StringUtil.IsEmpty(saishiyou1) Then
                                                saishiyou1 = ""
                                            End If
                                            If StringUtil.IsEmpty(saishiyou2) Then
                                                saishiyou2 = ""
                                            End If
                                            '-------------------------------------------------------------------------------
                                            If StringUtil.Equals(saishiyou1, saishiyou2) Then
                                                If StringUtil.Equals(baseBuhinEditList(index).ShukeiCode, baseBuhinEditList(index - 1).ShukeiCode) Then
                                                    rowIndex = rowIndex - 1
                                                    MergeFlag = True
                                                Else
                                                    If StringUtil.Equals(baseBuhinEditList(index).SiaShukeiCode, baseBuhinEditList(index - 1).SiaShukeiCode) Then
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
                                Dim insu As String = xls.GetValue(COL_START_GOUSYA + baseBuhinEditList(index).HyojijunNo, START_ROW + rowIndex)
                                If Not StringUtil.IsEmpty(insu) Then
                                    '**は数値では無いから'
                                    If StringUtil.Equals(insu, "**") Then
                                        xls.SetValue(COL_START_GOUSYA + baseBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                                    Else
                                        '員数がマイナスなら'
                                        If baseBuhinEditList(index).InsuSuryo < 0 Then
                                            xls.SetValue(COL_START_GOUSYA + baseBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                                        Else
                                            xls.SetValue(COL_START_GOUSYA + baseBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, baseBuhinEditList(index).InsuSuryo + Integer.Parse(insu))
                                        End If
                                    End If

                                Else
                                    If baseBuhinEditList(index).InsuSuryo < 0 Then
                                        xls.SetValue(COL_START_GOUSYA + baseBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                                    Else
                                        xls.SetValue(COL_START_GOUSYA + baseBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, baseBuhinEditList(index).InsuSuryo)
                                    End If

                                End If
                            Else

                                'ブロックNo'
                                xls.SetValue(COL_BLOCK_NO, START_ROW + rowIndex, baseBuhinEditList(index).ShisakuBlockNo)
                                '--------------------------------------------------------------------------------------------
                                'レベル’
                                '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                                '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                                If StringUtil.IsEmpty(baseBuhinEditList(index).Level) Then
                                    xls.SetValue(COL_LEVEL, START_ROW + rowIndex, "")
                                Else
                                    xls.SetValue(COL_LEVEL, START_ROW + rowIndex, baseBuhinEditList(index).Level)
                                End If
                                '--------------------------------------------------------------------------------------------
                                '国内集計コード'
                                xls.SetValue(COL_SHUKEI_CODE, START_ROW + rowIndex, baseBuhinEditList(index).ShukeiCode)
                                '海外集計コード'
                                xls.SetValue(COL_SIA_SHUKEI_CODE, START_ROW + rowIndex, baseBuhinEditList(index).SiaShukeiCode)
                                '現調区分'
                                xls.SetValue(COL_GENCYO_KBN, START_ROW + rowIndex, baseBuhinEditList(index).GencyoCkdKbn)
                                '取引先コード'
                                xls.SetValue(COL_TORIHIKISAKI_CODE, START_ROW + rowIndex, baseBuhinEditList(index).MakerCode)
                                '取引先名称'
                                xls.SetValue(COL_TORIHIKISAKI_NAME, START_ROW + rowIndex, baseBuhinEditList(index).MakerName)
                                '部品番号'
                                xls.SetValue(COL_BUHIN_NO, START_ROW + rowIndex, baseBuhinEditList(index).BuhinNo)
                                '部品番号区分'
                                xls.SetValue(COL_BUHIN_NO_KBN, START_ROW + rowIndex, baseBuhinEditList(index).BuhinNoKbn)
                                '部品番号改訂No'
                                xls.SetValue(COL_KAITEI, START_ROW + rowIndex, baseBuhinEditList(index).BuhinNoKaiteiNo)
                                '枝番'
                                xls.SetValue(COL_EDA_BAN, START_ROW + rowIndex, baseBuhinEditList(index).EdaBan)
                                '部品名称'
                                xls.SetValue(COL_BUHIN_NAME, START_ROW + rowIndex, baseBuhinEditList(index).BuhinName)

                                EditGousyaCount = GousyaCount

                                '員数'
                                Dim insu As String = xls.GetValue(COL_START_GOUSYA + baseBuhinEditList(index).HyojijunNo, START_ROW + rowIndex)
                                If Not StringUtil.IsEmpty(insu) Then
                                    '**は数値では無いから'
                                    If StringUtil.Equals(insu, "**") Then
                                        xls.SetValue(COL_START_GOUSYA + baseBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                                    Else
                                        '員数がマイナスなら'
                                        If baseBuhinEditList(index).InsuSuryo < 0 Then
                                            xls.SetValue(COL_START_GOUSYA + baseBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                                        Else
                                            xls.SetValue(COL_START_GOUSYA + baseBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, baseBuhinEditList(index).InsuSuryo + Integer.Parse(insu))
                                        End If
                                    End If

                                Else
                                    If baseBuhinEditList(index).InsuSuryo < 0 Then
                                        xls.SetValue(COL_START_GOUSYA + baseBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                                    Else
                                        xls.SetValue(COL_START_GOUSYA + baseBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, baseBuhinEditList(index).InsuSuryo)
                                    End If

                                End If

                                '再使用不可'
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, baseBuhinEditList(index).Saishiyoufuka)

                                '2012/01/23 供給セクション追加
                                '供給セクション'
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 1, START_ROW + rowIndex, baseBuhinEditList(index).KyoukuSection)

                                '出図予定日'
                                If baseBuhinEditList(index).ShutuzuYoteiDate = "99999999" Or baseBuhinEditList(index).ShutuzuYoteiDate = "0" Then
                                    'EditGousyaCount = EditGousyaCount + 1
                                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 2, START_ROW + rowIndex, "")
                                Else
                                    'EditGousyaCount = EditGousyaCount + 1
                                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 2, START_ROW + rowIndex, baseBuhinEditList(index).ShutuzuYoteiDate)
                                End If
                                ''↓↓2014/09/04 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 3, START_ROW + rowIndex, baseBuhinEditList(index).TsukurikataSeisaku)
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 4, START_ROW + rowIndex, baseBuhinEditList(index).TsukurikataKatashiyou1)
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 5, START_ROW + rowIndex, baseBuhinEditList(index).TsukurikataKatashiyou2)
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 6, START_ROW + rowIndex, baseBuhinEditList(index).TsukurikataKatashiyou3)
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 7, START_ROW + rowIndex, baseBuhinEditList(index).TsukurikataTigu)
                                If StringUtil.IsEmpty(baseBuhinEditList(index).TsukurikataNounyu) Or baseBuhinEditList(index).TsukurikataNounyu = "0" Then
                                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 8, START_ROW + rowIndex, "")
                                Else
                                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 8, START_ROW + rowIndex, baseBuhinEditList(index).TsukurikataNounyu)
                                End If
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 9, START_ROW + rowIndex, baseBuhinEditList(index).TsukurikataKibo)


                                '材質'
                                '規格１'
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 3, START_ROW + rowIndex, baseBuhinEditList(index).ZaishituKikaku1)
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 10, START_ROW + rowIndex, baseBuhinEditList(index).ZaishituKikaku1)
                                '規格２'
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 4, START_ROW + rowIndex, baseBuhinEditList(index).ZaishituKikaku2)
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 11, START_ROW + rowIndex, baseBuhinEditList(index).ZaishituKikaku2)
                                '規格３'
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 5, START_ROW + rowIndex, baseBuhinEditList(index).ZaishituKikaku3)
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 12, START_ROW + rowIndex, baseBuhinEditList(index).ZaishituKikaku3)
                                'メッキ'
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 6, START_ROW + rowIndex, baseBuhinEditList(index).ZaishituMekki)
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 13, START_ROW + rowIndex, baseBuhinEditList(index).ZaishituMekki)

                                '板厚'
                                '板厚数量'
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 7, START_ROW + rowIndex, baseBuhinEditList(index).ShisakuBankoSuryo)
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 14, START_ROW + rowIndex, baseBuhinEditList(index).ShisakuBankoSuryo)
                                '板厚数量U'
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 8, START_ROW + rowIndex, baseBuhinEditList(index).ShisakuBankoSuryoU)
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 15, START_ROW + rowIndex, baseBuhinEditList(index).ShisakuBankoSuryoU)

                                ''↓↓2014/12/26 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
                                '材料情報'
                                '製品長'
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 16, START_ROW + rowIndex, baseBuhinEditList(index).MaterialInfoLength)
                                '製品幅'
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 17, START_ROW + rowIndex, baseBuhinEditList(index).MaterialInfoWidth)
                                'データ項目
                                '改訂№'
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 18, START_ROW + rowIndex, baseBuhinEditList(index).DataItemKaiteiNo)
                                'エリア名'
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 19, START_ROW + rowIndex, baseBuhinEditList(index).DataItemAreaName)
                                'セット名'
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 20, START_ROW + rowIndex, baseBuhinEditList(index).DataItemSetName)
                                '改訂情報'
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 21, START_ROW + rowIndex, baseBuhinEditList(index).DataItemKaiteiInfo)
                                ''↑↑2014/12/26 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END
                                ''↓↓2014/12/26 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END
                                '試作部品費'
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 9, START_ROW + rowIndex, baseBuhinEditList(index).ShisakuBuhinHi)
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 16, START_ROW + rowIndex, baseBuhinEditList(index).ShisakuBuhinHi)
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 22, START_ROW + rowIndex, baseBuhinEditList(index).ShisakuBuhinHi)
                                '試作型費'
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 10, START_ROW + rowIndex, baseBuhinEditList(index).ShisakuKataHi)
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 17, START_ROW + rowIndex, baseBuhinEditList(index).ShisakuKataHi)
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 23, START_ROW + rowIndex, baseBuhinEditList(index).ShisakuKataHi)
                                '部品NOTE'
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 11, START_ROW + rowIndex, baseBuhinEditList(index).BuhinNote)
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 18, START_ROW + rowIndex, baseBuhinEditList(index).BuhinNote)
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 24, START_ROW + rowIndex, baseBuhinEditList(index).BuhinNote)
                                '備考'
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 12, START_ROW + rowIndex, baseBuhinEditList(index).Bikou)
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 19, START_ROW + rowIndex, baseBuhinEditList(index).Bikou)
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 25, START_ROW + rowIndex, baseBuhinEditList(index).Bikou)
                                '担当者名'
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 13, START_ROW + rowIndex, baseBuhinEditList(index).UserId)
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 20, START_ROW + rowIndex, baseBuhinEditList(index).UserId)
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 26, START_ROW + rowIndex, baseBuhinEditList(index).UserId)
                                '電話番号'
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 14, START_ROW + rowIndex, baseBuhinEditList(index).TelNo)
                                'xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 21, START_ROW + rowIndex, baseBuhinEditList(index).TelNo)
                                xls.SetValue(COL_START_GOUSYA + EditGousyaCount + 27, START_ROW + rowIndex, baseBuhinEditList(index).TelNo)


                                'xls.SetBackColor(COL_BLOCK_NO, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount + 14, START_ROW + rowIndex, &HA0A0A0)
                                ''↓↓2014/12/29 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                                ' xls.SetBackColor(COL_BLOCK_NO, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount + 21, START_ROW + rowIndex, &HA0A0A0)
                                xls.SetBackColor(COL_BLOCK_NO, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount + 27, START_ROW + rowIndex, &HA0A0A0)
                                ''↑↑2014/12/29 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END
                                ''↑↑2014/12/26 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END
                                ''↑↑2014/09/04 Ⅰ.2.管理項目追加 酒井 ADD END
                                AllChkColor(xls, baseBuhinEditList(index), buhinEditList, rowIndex, False)
                            End If

                            rowIndex = rowIndex + 1
                        End If
                    Next
                End If
            Next

        End Sub

#Region "比較"

        ''' <summary>
        ''' 部品編集情報チェック
        ''' </summary>
        ''' <param name="buhinEdit"></param>
        ''' <param name="combuhinEditList"></param>
        ''' <remarks></remarks>
        Private Function AllChk(ByVal buhinEdit As TShisakuBuhinEditVoHelperExcel, ByVal combuhinEditList As List(Of TShisakuBuhinEditVoHelperExcel)) As Boolean

            For Each vo As TShisakuBuhinEditVoHelperExcel In combuhinEditList
                '全部チェック'
                'ブロックNo'
                If StringUtil.Equals(buhinEdit.ShisakuBlockNo.Trim, vo.ShisakuBlockNo.Trim) Then
                    '部品番号'
                    If StringUtil.Equals(vo.BuhinNo.Trim, buhinEdit.BuhinNo.Trim) Then
                        '--------------------------------------------------------------------------------------------
                        'レベル’
                        '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                        '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                        Dim voLevel As String
                        Dim buhinEditLevel As String
                        If StringUtil.IsEmpty(vo.Level) Then
                            voLevel = ""
                        Else
                            voLevel = vo.Level
                        End If
                        If StringUtil.IsEmpty(buhinEdit.Level) Then
                            buhinEditLevel = ""
                        Else
                            buhinEditLevel = buhinEdit.Level
                        End If
                        '--------------------------------------------------------------------------------------------
                        If voLevel = buhinEditLevel Then

                            '再使用不可'
                            If vo.Saishiyoufuka Is Nothing Then
                                vo.Saishiyoufuka = ""
                            End If
                            If buhinEdit.Saishiyoufuka Is Nothing Then
                                buhinEdit.Saishiyoufuka = ""
                            End If
                            If StringUtil.Equals(vo.Saishiyoufuka.Trim, buhinEdit.Saishiyoufuka.Trim) Then

                                '供給セクション'
                                If vo.KyoukuSection Is Nothing Then
                                    vo.KyoukuSection = ""
                                End If
                                If buhinEdit.KyoukuSection Is Nothing Then
                                    buhinEdit.KyoukuSection = ""
                                End If
                                If StringUtil.Equals(vo.KyoukuSection.Trim, buhinEdit.KyoukuSection.Trim) Then


                                    '国内集計'
                                    If vo.ShukeiCode Is Nothing Then
                                        vo.ShukeiCode = ""
                                    End If
                                    If buhinEdit.ShukeiCode Is Nothing Then
                                        buhinEdit.ShukeiCode = ""
                                    End If
                                    If Not StringUtil.Equals(vo.ShukeiCode.Trim, buhinEdit.ShukeiCode.Trim) Then
                                        Continue For
                                    End If
                                    '海外集計'
                                    If vo.SiaShukeiCode Is Nothing Then
                                        vo.SiaShukeiCode = ""
                                    End If
                                    If buhinEdit.SiaShukeiCode Is Nothing Then
                                        buhinEdit.SiaShukeiCode = ""
                                    End If
                                    If Not StringUtil.Equals(vo.SiaShukeiCode.Trim, buhinEdit.SiaShukeiCode.Trim) Then
                                        Continue For
                                    End If

                                    '現調区分'
                                    If vo.GencyoCkdKbn Is Nothing Then
                                        vo.GencyoCkdKbn = ""
                                    End If
                                    If buhinEdit.GencyoCkdKbn Is Nothing Then
                                        buhinEdit.GencyoCkdKbn = ""
                                    End If
                                    If Not StringUtil.Equals(vo.GencyoCkdKbn.Trim, buhinEdit.GencyoCkdKbn.Trim) Then
                                        Continue For
                                    End If
                                    '取引先コード'
                                    If vo.MakerCode Is Nothing Then
                                        vo.MakerCode = ""
                                    End If
                                    If buhinEdit.MakerCode Is Nothing Then
                                        buhinEdit.MakerCode = ""
                                    End If
                                    If Not StringUtil.Equals(vo.MakerCode.Trim, buhinEdit.MakerCode.Trim) Then
                                        Continue For
                                    End If
                                    '取引先名称'
                                    If vo.MakerName Is Nothing Then
                                        vo.MakerName = ""
                                    End If
                                    If buhinEdit.MakerName Is Nothing Then
                                        buhinEdit.MakerName = ""
                                    End If
                                    If Not StringUtil.Equals(vo.MakerName.Trim, buhinEdit.MakerName.Trim) Then
                                        Continue For
                                    End If
                                    '部品番号区分'
                                    If vo.BuhinNoKbn Is Nothing Then
                                        vo.BuhinNoKbn = ""
                                    End If
                                    If buhinEdit.BuhinNoKbn Is Nothing Then
                                        buhinEdit.BuhinNoKbn = ""
                                    End If
                                    If Not StringUtil.Equals(vo.BuhinNoKbn.Trim, buhinEdit.BuhinNoKbn.Trim) Then
                                        Continue For
                                    End If
                                    '部品番号改訂No'
                                    If vo.BuhinNoKaiteiNo Is Nothing Then
                                        vo.BuhinNoKaiteiNo = ""
                                    End If
                                    If buhinEdit.BuhinNoKaiteiNo Is Nothing Then
                                        buhinEdit.BuhinNoKaiteiNo = ""
                                    End If
                                    If Not StringUtil.Equals(vo.BuhinNoKaiteiNo.Trim, buhinEdit.BuhinNoKaiteiNo.Trim) Then
                                        Continue For
                                    End If
                                    '枝番'
                                    If vo.EdaBan Is Nothing Then
                                        vo.EdaBan = ""
                                    End If
                                    If buhinEdit.EdaBan Is Nothing Then
                                        buhinEdit.EdaBan = ""
                                    End If
                                    If Not StringUtil.Equals(vo.EdaBan.Trim, buhinEdit.EdaBan.Trim) Then
                                        Continue For
                                    End If
                                    '部品名称'
                                    If vo.BuhinName Is Nothing Then
                                        vo.BuhinName = ""
                                    End If
                                    If buhinEdit.BuhinName Is Nothing Then
                                        buhinEdit.BuhinName = ""
                                    End If
                                    If Not StringUtil.Equals(vo.BuhinName.Trim, buhinEdit.BuhinName.Trim) Then
                                        Continue For
                                    End If
                                    '再使用不可'
                                    If vo.Saishiyoufuka Is Nothing Then
                                        vo.Saishiyoufuka = ""
                                    End If
                                    If buhinEdit.Saishiyoufuka Is Nothing Then
                                        buhinEdit.Saishiyoufuka = ""
                                    End If
                                    If Not StringUtil.Equals(vo.Saishiyoufuka.Trim, buhinEdit.Saishiyoufuka.Trim) Then
                                        Continue For
                                    End If
                                    '出図予定日'
                                    If vo.ShutuzuYoteiDate = 99999999 Then
                                        vo.ShutuzuYoteiDate = 0
                                    End If
                                    If buhinEdit.ShutuzuYoteiDate = 99999999 Then
                                        buhinEdit.ShutuzuYoteiDate = 0
                                    End If
                                    If vo.ShutuzuYoteiDate <> buhinEdit.ShutuzuYoteiDate Then
                                        Continue For
                                    End If

                                    If vo.TsukurikataSeisaku Is Nothing Then
                                        vo.TsukurikataSeisaku = ""
                                    End If
                                    If buhinEdit.TsukurikataSeisaku Is Nothing Then
                                        buhinEdit.TsukurikataSeisaku = ""
                                    End If
                                    If Not StringUtil.Equals(vo.TsukurikataSeisaku.Trim, buhinEdit.TsukurikataSeisaku.Trim) Then
                                        Continue For
                                    End If


                                    If vo.TsukurikataKatashiyou1 Is Nothing Then
                                        vo.TsukurikataKatashiyou1 = ""
                                    End If
                                    If buhinEdit.TsukurikataKatashiyou1 Is Nothing Then
                                        buhinEdit.TsukurikataKatashiyou1 = ""
                                    End If
                                    If Not StringUtil.Equals(vo.TsukurikataKatashiyou1.Trim, buhinEdit.TsukurikataKatashiyou1.Trim) Then
                                        Continue For
                                    End If

                                    If vo.TsukurikataKatashiyou2 Is Nothing Then
                                        vo.TsukurikataKatashiyou2 = ""
                                    End If
                                    If buhinEdit.TsukurikataKatashiyou2 Is Nothing Then
                                        buhinEdit.TsukurikataKatashiyou2 = ""
                                    End If
                                    If Not StringUtil.Equals(vo.TsukurikataKatashiyou2.Trim, buhinEdit.TsukurikataKatashiyou2.Trim) Then
                                        Continue For
                                    End If

                                    If vo.TsukurikataKatashiyou3 Is Nothing Then
                                        vo.TsukurikataKatashiyou3 = ""
                                    End If
                                    If buhinEdit.TsukurikataKatashiyou3 Is Nothing Then
                                        buhinEdit.TsukurikataKatashiyou3 = ""
                                    End If
                                    If Not StringUtil.Equals(vo.TsukurikataKatashiyou3.Trim, buhinEdit.TsukurikataKatashiyou3.Trim) Then
                                        Continue For
                                    End If

                                    If vo.TsukurikataTigu Is Nothing Then
                                        vo.TsukurikataTigu = ""
                                    End If
                                    If buhinEdit.TsukurikataTigu Is Nothing Then
                                        buhinEdit.TsukurikataTigu = ""
                                    End If
                                    If Not StringUtil.Equals(vo.TsukurikataTigu.Trim, buhinEdit.TsukurikataTigu.Trim) Then
                                        Continue For
                                    End If

                                    If vo.TsukurikataNounyu Is Nothing Then
                                        vo.TsukurikataNounyu = 0
                                    End If
                                    If buhinEdit.TsukurikataNounyu Is Nothing Then
                                        buhinEdit.TsukurikataNounyu = 0
                                    End If
                                    If Not StringUtil.Equals(vo.TsukurikataNounyu, buhinEdit.TsukurikataNounyu) Then
                                        Continue For
                                    End If

                                    If vo.TsukurikataKibo Is Nothing Then
                                        vo.TsukurikataKibo = ""
                                    End If
                                    If buhinEdit.TsukurikataKibo Is Nothing Then
                                        buhinEdit.TsukurikataKibo = ""
                                    End If
                                    If Not StringUtil.Equals(vo.TsukurikataKibo.Trim, buhinEdit.TsukurikataKibo.Trim) Then
                                        Continue For
                                    End If
                                    ''↑↑2014/09/04 Ⅰ.2.管理項目追加 酒井 ADD BEGIN

                                    '材質規格1'
                                    If vo.ZaishituKikaku1 Is Nothing Then
                                        vo.ZaishituKikaku1 = ""
                                    End If
                                    If buhinEdit.ZaishituKikaku1 Is Nothing Then
                                        buhinEdit.ZaishituKikaku1 = ""
                                    End If
                                    If Not StringUtil.Equals(vo.ZaishituKikaku1.Trim, buhinEdit.ZaishituKikaku1.Trim) Then
                                        Continue For
                                    End If
                                    '材質規格2'
                                    If vo.ZaishituKikaku2 Is Nothing Then
                                        vo.ZaishituKikaku2 = ""
                                    End If
                                    If buhinEdit.ZaishituKikaku2 Is Nothing Then
                                        buhinEdit.ZaishituKikaku2 = ""
                                    End If
                                    If Not StringUtil.Equals(vo.ZaishituKikaku2.Trim, buhinEdit.ZaishituKikaku2.Trim) Then
                                        Continue For
                                    End If
                                    '材質規格3'
                                    If vo.ZaishituKikaku3 Is Nothing Then
                                        vo.ZaishituKikaku3 = ""
                                    End If
                                    If buhinEdit.ZaishituKikaku3 Is Nothing Then
                                        buhinEdit.ZaishituKikaku3 = ""
                                    End If
                                    If Not StringUtil.Equals(vo.ZaishituKikaku3.Trim, buhinEdit.ZaishituKikaku3.Trim) Then
                                        Continue For
                                    End If
                                    '材質メッキ'
                                    If vo.ZaishituMekki Is Nothing Then
                                        vo.ZaishituMekki = ""
                                    End If
                                    If buhinEdit.ZaishituMekki Is Nothing Then
                                        buhinEdit.ZaishituMekki = ""
                                    End If
                                    If Not StringUtil.Equals(vo.ZaishituMekki.Trim, buhinEdit.ZaishituMekki.Trim) Then
                                        Continue For
                                    End If
                                    '試作板厚数量'
                                    If vo.ShisakuBankoSuryo Is Nothing Then
                                        vo.ShisakuBankoSuryo = ""
                                    End If
                                    If buhinEdit.ShisakuBankoSuryo Is Nothing Then
                                        buhinEdit.ShisakuBankoSuryo = ""
                                    End If
                                    If Not StringUtil.Equals(vo.ShisakuBankoSuryo.Trim, buhinEdit.ShisakuBankoSuryo.Trim) Then
                                        Continue For
                                    End If
                                    '試作板厚数量U'
                                    If vo.ShisakuBankoSuryoU Is Nothing Then
                                        vo.ShisakuBankoSuryoU = ""
                                    End If
                                    If buhinEdit.ShisakuBankoSuryoU Is Nothing Then
                                        buhinEdit.ShisakuBankoSuryoU = ""
                                    End If
                                    If Not StringUtil.Equals(vo.ShisakuBankoSuryoU.Trim, buhinEdit.ShisakuBankoSuryoU.Trim) Then
                                        Continue For
                                    End If

                                    ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
                                    '製品長
                                    If Not StringUtil.Equals(vo.MaterialInfoLength, buhinEdit.MaterialInfoLength) Then
                                        Continue For
                                    End If
                                    '製品幅
                                    If Not StringUtil.Equals(vo.MaterialInfoWidth, buhinEdit.MaterialInfoWidth) Then
                                        Continue For
                                    End If
                                    '改訂№'
                                    If vo.DataItemKaiteiNo Is Nothing Then
                                        vo.DataItemKaiteiNo = ""
                                    End If
                                    If buhinEdit.DataItemKaiteiNo Is Nothing Then
                                        buhinEdit.DataItemKaiteiNo = ""
                                    End If
                                    If Not StringUtil.Equals(vo.DataItemKaiteiNo.Trim, buhinEdit.DataItemKaiteiNo.Trim) Then
                                        Continue For
                                    End If
                                    'エリア名'
                                    If vo.DataItemAreaName Is Nothing Then
                                        vo.DataItemAreaName = ""
                                    End If
                                    If buhinEdit.DataItemAreaName Is Nothing Then
                                        buhinEdit.DataItemAreaName = ""
                                    End If
                                    If Not StringUtil.Equals(vo.DataItemAreaName.Trim, buhinEdit.DataItemAreaName.Trim) Then
                                        Continue For
                                    End If
                                    'セット名'
                                    If vo.DataItemSetName Is Nothing Then
                                        vo.DataItemSetName = ""
                                    End If
                                    If buhinEdit.DataItemSetName Is Nothing Then
                                        buhinEdit.DataItemSetName = ""
                                    End If
                                    If Not StringUtil.Equals(vo.DataItemSetName.Trim, buhinEdit.DataItemSetName.Trim) Then
                                        Continue For
                                    End If
                                    '改訂情報'
                                    If vo.DataItemKaiteiInfo Is Nothing Then
                                        vo.DataItemKaiteiInfo = ""
                                    End If
                                    If buhinEdit.DataItemKaiteiInfo Is Nothing Then
                                        buhinEdit.DataItemKaiteiInfo = ""
                                    End If
                                    If Not StringUtil.Equals(vo.DataItemKaiteiInfo.Trim, buhinEdit.DataItemKaiteiInfo.Trim) Then
                                        Continue For
                                    End If
                                    ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END


                                    '試作部品費'
                                    If vo.ShisakuBuhinHi <> buhinEdit.ShisakuBuhinHi Then
                                        Continue For
                                    End If
                                    '試作型費'
                                    If vo.ShisakuKataHi <> buhinEdit.ShisakuKataHi Then
                                        Continue For
                                    End If
                                    '部品NOTE'
                                    If vo.BuhinNote Is Nothing Then
                                        vo.BuhinNote = ""
                                    End If
                                    If buhinEdit.BuhinNote Is Nothing Then
                                        buhinEdit.BuhinNote = ""
                                    End If
                                    If Not StringUtil.Equals(vo.BuhinNote.Trim, buhinEdit.BuhinNote.Trim) Then
                                        Continue For
                                    End If
                                    '備考'
                                    If vo.Bikou Is Nothing Then
                                        vo.Bikou = ""
                                    End If
                                    If buhinEdit.Bikou Is Nothing Then
                                        buhinEdit.Bikou = ""
                                    End If
                                    If Not StringUtil.Equals(vo.Bikou.Trim, buhinEdit.Bikou.Trim) Then
                                        Continue For
                                    End If
                                    '供給セクション'
                                    If vo.KyoukuSection Is Nothing Then
                                        vo.KyoukuSection = ""
                                    End If
                                    If buhinEdit.KyoukuSection Is Nothing Then
                                        buhinEdit.KyoukuSection = ""
                                    End If
                                    If Not StringUtil.Equals(vo.KyoukuSection.Trim, buhinEdit.KyoukuSection.Trim) Then
                                        Continue For
                                    End If
                                    If StringUtil.Equals("", buhinEdit.BuhinNo.Trim) Then
                                        Dim a As String = ""
                                    End If

                                    '号車表示順'
                                    If vo.HyojijunNo <> buhinEdit.HyojijunNo Then
                                        Continue For
                                    End If

                                    If Me.TshisakuEventVo.BlockAlertKind = 2 And Me.TshisakuEventVo.KounyuShijiFlg = "0" And vo.InstlHinbanHyoujiJun <> buhinEdit.InstlHinbanHyoujiJun Then
                                        Continue For
                                    End If

                                    '員数'
                                    If vo.InsuSuryo <> buhinEdit.InsuSuryo Then
                                        vo.Flag = False
                                        Continue For
                                    End If

                                    '員数'
                                    If Me.BlockAlertKind = 2 And Me.KounyuShijiFlg = "0" And vo.BaseInstlFlg <> buhinEdit.BaseInstlFlg Then
                                        vo.Flag = False
                                        Continue For
                                    End If

                                    '全件チェックの結果同一の部品が存在する'
                                    '旧構成から削除'
                                    vo.Flag = True
                                    'buhinEditList.Remove(vo)
                                    Return True
                                End If
                            End If

                        End If
                    End If

                End If
            Next
            Return False
        End Function

        ''' <summary>
        ''' 部品編集情報チェック
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <param name="buhinEdit"></param>
        ''' <param name="buhinEditList"></param>
        ''' <remarks></remarks>
        Private Sub AllChkColor(ByVal xls As ShisakuExcel, ByVal buhinEdit As TShisakuBuhinEditVoHelperExcel, _
                                ByVal buhinEditList As List(Of TShisakuBuhinEditVoHelperExcel), _
                                ByVal rowIndex As Integer, _
                                Optional ByVal isAdd As Boolean = True)
            Dim buhinNoFlag As Boolean = False
            Dim insuFlag As Boolean = False
            For Each vo As TShisakuBuhinEditVoHelperExcel In buhinEditList
                '全部チェック'
                'ブロックNo'
                If StringUtil.Equals(buhinEdit.ShisakuBlockNo.Trim, vo.ShisakuBlockNo.Trim) Then
                    '部品番号'
                    If StringUtil.Equals(vo.BuhinNo.Trim, buhinEdit.BuhinNo.Trim) Then
                        buhinNoFlag = True
                        '--------------------------------------------------------------------------------------------
                        'レベル’
                        '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                        '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                        Dim voLevel As String
                        Dim buhinEditLevel As String
                        If StringUtil.IsEmpty(vo.Level) Then
                            voLevel = ""
                        Else
                            voLevel = vo.Level
                        End If
                        If StringUtil.IsEmpty(buhinEdit.Level) Then
                            buhinEditLevel = ""
                        Else
                            buhinEditLevel = buhinEdit.Level
                        End If
                        '--------------------------------------------------------------------------------------------

                        If voLevel = buhinEditLevel Then
                            '変更確定なのでここで変更色を用意'
                            If Not isAdd Then
                                ''↓↓2014/09/04 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
                                'xls.SetBackColor(COL_BLOCK_NO, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount + 14, START_ROW + rowIndex, &HCCCCCC)
                                ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
                                'xls.SetBackColor(COL_BLOCK_NO, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount + 21, START_ROW + rowIndex, &HCCCCCC)
                                xls.SetBackColor(COL_BLOCK_NO, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount + 27, START_ROW + rowIndex, &HCCCCCC)
                                ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END
                                ''↑↑2014/09/04 Ⅰ.2.管理項目追加 酒井 ADD END
                                Continue For
                            End If

                            '国内集計'
                            If Not StringUtil.Equals(vo.ShukeiCode.Trim, buhinEdit.ShukeiCode.Trim) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_SHUKEI_CODE, START_ROW + rowIndex, COL_SHUKEI_CODE, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_BLOCK_NO, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount + 14, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '海外集計'
                            If Not StringUtil.Equals(vo.SiaShukeiCode.Trim, buhinEdit.SiaShukeiCode.Trim) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_SIA_SHUKEI_CODE, START_ROW + rowIndex, COL_SIA_SHUKEI_CODE, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_SIA_SHUKEI_CODE, START_ROW + rowIndex, COL_SIA_SHUKEI_CODE, START_ROW + rowIndex, &HF0F0F0)
                                End If

                            End If
                            '現調区分'
                            If Not StringUtil.Equals(vo.GencyoCkdKbn.Trim, buhinEdit.GencyoCkdKbn.Trim) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_GENCYO_KBN, START_ROW + rowIndex, COL_GENCYO_KBN, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_GENCYO_KBN, START_ROW + rowIndex, COL_GENCYO_KBN, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '取引先コード'
                            If vo.MakerCode Is Nothing Then
                                vo.MakerCode = ""
                            End If
                            If buhinEdit.MakerCode Is Nothing Then
                                buhinEdit.MakerCode = ""
                            End If
                            If Not StringUtil.Equals(vo.MakerCode.Trim, buhinEdit.MakerCode.Trim) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_TORIHIKISAKI_CODE, START_ROW + rowIndex, COL_TORIHIKISAKI_CODE, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_TORIHIKISAKI_CODE, START_ROW + rowIndex, COL_TORIHIKISAKI_CODE, START_ROW + rowIndex, &HF0F0F0)
                                End If

                            End If
                            '取引先名称'
                            If vo.MakerName Is Nothing Then
                                vo.MakerName = ""
                            End If
                            If buhinEdit.MakerName Is Nothing Then
                                buhinEdit.MakerName = ""
                            End If
                            If Not StringUtil.Equals(vo.MakerName.Trim, buhinEdit.MakerName.Trim) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_TORIHIKISAKI_NAME, START_ROW + rowIndex, COL_TORIHIKISAKI_NAME, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_TORIHIKISAKI_NAME, START_ROW + rowIndex, COL_TORIHIKISAKI_NAME, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '部品番号区分'
                            If Not StringUtil.Equals(vo.BuhinNoKbn.Trim, buhinEdit.BuhinNoKbn.Trim) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_BUHIN_NO_KBN, START_ROW + rowIndex, COL_BUHIN_NO_KBN, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_BUHIN_NO_KBN, START_ROW + rowIndex, COL_BUHIN_NO_KBN, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '部品番号改訂No'
                            If Not StringUtil.Equals(vo.BuhinNoKaiteiNo.Trim, buhinEdit.BuhinNoKaiteiNo.Trim) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_KAITEI, START_ROW + rowIndex, COL_KAITEI, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_KAITEI, START_ROW + rowIndex, COL_KAITEI, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '枝番'
                            If Not StringUtil.Equals(vo.EdaBan.Trim, buhinEdit.EdaBan.Trim) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_EDA_BAN, START_ROW + rowIndex, COL_EDA_BAN, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_EDA_BAN, START_ROW + rowIndex, COL_EDA_BAN, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '部品名称'
                            If Not StringUtil.Equals(vo.BuhinName.Trim, buhinEdit.BuhinName.Trim) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_BUHIN_NAME, START_ROW + rowIndex, COL_BUHIN_NAME, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_BUHIN_NAME, START_ROW + rowIndex, COL_BUHIN_NAME, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '再使用不可'
                            If Not StringUtil.Equals(vo.Saishiyoufuka.Trim, buhinEdit.Saishiyoufuka.Trim) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount, START_ROW + rowIndex, &HF0F0F0)
                                End If

                            End If
                            '供給セクション'
                            If vo.KyoukuSection Is Nothing Then
                                vo.KyoukuSection = ""
                            End If
                            If buhinEdit.KyoukuSection Is Nothing Then
                                buhinEdit.KyoukuSection = ""
                            End If
                            If Not StringUtil.Equals(vo.KyoukuSection.Trim, buhinEdit.KyoukuSection.Trim) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 1, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 1, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 1, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 1, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If

                            '出図予定日'
                            If vo.ShutuzuYoteiDate = 99999999 Then
                                vo.ShutuzuYoteiDate = 0
                            End If
                            If buhinEdit.ShutuzuYoteiDate = 99999999 Then
                                buhinEdit.ShutuzuYoteiDate = 0
                            End If
                            If vo.ShutuzuYoteiDate <> buhinEdit.ShutuzuYoteiDate Then
                                If isAdd Then
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 2, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 2, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 2, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 2, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If

                            ''↓↓2014/09/04 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
                            If vo.TsukurikataSeisaku Is Nothing Then
                                vo.TsukurikataSeisaku = ""
                            End If
                            If buhinEdit.TsukurikataSeisaku Is Nothing Then
                                buhinEdit.TsukurikataSeisaku = ""
                            End If
                            If Not StringUtil.Equals(vo.TsukurikataSeisaku.Trim, buhinEdit.TsukurikataSeisaku.Trim) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 3, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 3, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 3, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 3, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If

                            If vo.TsukurikataKatashiyou1 Is Nothing Then
                                vo.TsukurikataKatashiyou1 = ""
                            End If
                            If buhinEdit.TsukurikataKatashiyou1 Is Nothing Then
                                buhinEdit.TsukurikataKatashiyou1 = ""
                            End If
                            If Not StringUtil.Equals(vo.TsukurikataKatashiyou1.Trim, buhinEdit.TsukurikataKatashiyou1.Trim) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 4, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 4, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 4, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 4, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If


                            If vo.TsukurikataKatashiyou2 Is Nothing Then
                                vo.TsukurikataKatashiyou2 = ""
                            End If
                            If buhinEdit.TsukurikataKatashiyou2 Is Nothing Then
                                buhinEdit.TsukurikataKatashiyou2 = ""
                            End If
                            If Not StringUtil.Equals(vo.TsukurikataKatashiyou2.Trim, buhinEdit.TsukurikataKatashiyou2.Trim) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 5, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 5, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 5, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 5, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If


                            If vo.TsukurikataKatashiyou3 Is Nothing Then
                                vo.TsukurikataKatashiyou3 = ""
                            End If
                            If buhinEdit.TsukurikataKatashiyou3 Is Nothing Then
                                buhinEdit.TsukurikataKatashiyou3 = ""
                            End If
                            If Not StringUtil.Equals(vo.TsukurikataKatashiyou3.Trim, buhinEdit.TsukurikataKatashiyou3.Trim) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 6, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 6, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 6, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 6, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If


                            If vo.TsukurikataTigu Is Nothing Then
                                vo.TsukurikataTigu = ""
                            End If
                            If buhinEdit.TsukurikataTigu Is Nothing Then
                                buhinEdit.TsukurikataTigu = ""
                            End If
                            If Not StringUtil.Equals(vo.TsukurikataTigu.Trim, buhinEdit.TsukurikataTigu.Trim) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 7, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 7, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 7, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 7, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If


                            Dim wkTsukurikataNounyu1 As String = ""
                            Dim wkTsukurikataNounyu2 As String = ""
                            If vo.TsukurikataNounyu IsNot Nothing Then
                                wkTsukurikataNounyu1 = vo.TsukurikataNounyu.ToString
                            End If
                            If buhinEdit.TsukurikataNounyu IsNot Nothing Then
                                wkTsukurikataNounyu2 = buhinEdit.TsukurikataNounyu.ToString
                            End If
                            If Not StringUtil.Equals(wkTsukurikataNounyu1, wkTsukurikataNounyu2) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 8, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 8, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 8, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 8, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If

                            If vo.TsukurikataKibo Is Nothing Then
                                vo.TsukurikataKibo = ""
                            End If
                            If buhinEdit.TsukurikataKibo Is Nothing Then
                                buhinEdit.TsukurikataKibo = ""
                            End If
                            If Not StringUtil.Equals(vo.TsukurikataKibo.Trim, buhinEdit.TsukurikataKibo.Trim) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 9, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 9, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 9, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 9, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If


                            '材質規格1'
                            If vo.ZaishituKikaku1 Is Nothing Then
                                vo.ZaishituKikaku1 = ""
                            End If
                            If buhinEdit.ZaishituKikaku1 Is Nothing Then
                                buhinEdit.ZaishituKikaku1 = ""
                            End If
                            If Not StringUtil.Equals(vo.ZaishituKikaku1.Trim, buhinEdit.ZaishituKikaku1.Trim) Then
                                If isAdd Then
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 3, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 3, START_ROW + rowIndex, &H9FFFFF)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 10, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 10, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 3, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 3, START_ROW + rowIndex, &HF0F0F0)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 10, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 10, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '材質規格2'
                            If vo.ZaishituKikaku2 Is Nothing Then
                                vo.ZaishituKikaku2 = ""
                            End If
                            If buhinEdit.ZaishituKikaku2 Is Nothing Then
                                buhinEdit.ZaishituKikaku2 = ""
                            End If
                            If Not StringUtil.Equals(vo.ZaishituKikaku2.Trim, buhinEdit.ZaishituKikaku2.Trim) Then
                                If isAdd Then
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 4, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 4, START_ROW + rowIndex, &H9FFFFF)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 11, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 11, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 4, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 4, START_ROW + rowIndex, &HF0F0F0)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 11, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 11, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '材質規格3'
                            If vo.ZaishituKikaku3 Is Nothing Then
                                vo.ZaishituKikaku3 = ""
                            End If
                            If buhinEdit.ZaishituKikaku3 Is Nothing Then
                                buhinEdit.ZaishituKikaku3 = ""
                            End If
                            If Not StringUtil.Equals(vo.ZaishituKikaku3.Trim, buhinEdit.ZaishituKikaku3.Trim) Then
                                If isAdd Then
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 5, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 5, START_ROW + rowIndex, &H9FFFFF)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 12, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 12, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 5, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 5, START_ROW + rowIndex, &HF0F0F0)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 12, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 12, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '材質メッキ'
                            If vo.ZaishituMekki Is Nothing Then
                                vo.ZaishituMekki = ""
                            End If
                            If buhinEdit.ZaishituMekki Is Nothing Then
                                buhinEdit.ZaishituMekki = ""
                            End If
                            If Not StringUtil.Equals(vo.ZaishituMekki.Trim, buhinEdit.ZaishituMekki.Trim) Then
                                If isAdd Then
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 6, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 6, START_ROW + rowIndex, &H9FFFFF)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 13, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 13, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 6, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 6, START_ROW + rowIndex, &HF0F0F0)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 13, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 13, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '試作板厚数量'
                            If vo.ShisakuBankoSuryo Is Nothing Then
                                vo.ShisakuBankoSuryo = ""
                            End If
                            If buhinEdit.ShisakuBankoSuryo Is Nothing Then
                                buhinEdit.ShisakuBankoSuryo = ""
                            End If
                            If Not StringUtil.Equals(vo.ShisakuBankoSuryo.Trim, buhinEdit.ShisakuBankoSuryo.Trim) Then
                                If isAdd Then
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 7, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 7, START_ROW + rowIndex, &H9FFFFF)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 14, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 14, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 7, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 7, START_ROW + rowIndex, &HF0F0F0)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 14, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 14, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '試作板厚数量U'
                            If vo.ShisakuBankoSuryoU Is Nothing Then
                                vo.ShisakuBankoSuryoU = ""
                            End If
                            If buhinEdit.ShisakuBankoSuryoU Is Nothing Then
                                buhinEdit.ShisakuBankoSuryoU = ""
                            End If
                            If Not StringUtil.Equals(vo.ShisakuBankoSuryoU.Trim, buhinEdit.ShisakuBankoSuryoU.Trim) Then
                                If isAdd Then
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 8, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 8, START_ROW + rowIndex, &H9FFFFF)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 15, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 15, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 8, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 8, START_ROW + rowIndex, &HF0F0F0)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 15, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 15, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If

                            ''↓↓2014/12/26 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                            '材料情報'
                            '製品長'
                            If Not StringUtil.Equals(vo.MaterialInfoLength, buhinEdit.MaterialInfoLength) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 16, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 16, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                   xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 16, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 16, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '製品幅'
                            If Not StringUtil.Equals(vo.MaterialInfoWidth, buhinEdit.MaterialInfoWidth) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 17, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 17, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 17, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 17, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            'データ項目
                            '改訂№'
                            If vo.DataItemKaiteiNo Is Nothing Then
                                vo.DataItemKaiteiNo = ""
                            End If
                            If buhinEdit.DataItemKaiteiNo Is Nothing Then
                                buhinEdit.DataItemKaiteiNo = ""
                            End If
                            If Not StringUtil.Equals(vo.DataItemKaiteiNo, buhinEdit.DataItemKaiteiNo) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 18, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 18, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 18, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 18, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            'エリア名'
                            If vo.DataItemAreaName Is Nothing Then
                                vo.DataItemAreaName = ""
                            End If
                            If buhinEdit.DataItemAreaName Is Nothing Then
                                buhinEdit.DataItemAreaName = ""
                            End If
                            If Not StringUtil.Equals(vo.DataItemAreaName, buhinEdit.DataItemAreaName) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 19, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 19, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 19, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 19, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            'セット名'
                            If vo.DataItemSetName Is Nothing Then
                                vo.DataItemSetName = ""
                            End If
                            If buhinEdit.DataItemSetName Is Nothing Then
                                buhinEdit.DataItemSetName = ""
                            End If
                            If Not StringUtil.Equals(vo.DataItemSetName, buhinEdit.DataItemSetName) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 20, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 20, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 20, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 20, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '改訂情報'
                            If vo.DataItemKaiteiInfo Is Nothing Then
                                vo.DataItemKaiteiInfo = ""
                            End If
                            If buhinEdit.DataItemKaiteiInfo Is Nothing Then
                                buhinEdit.DataItemKaiteiInfo = ""
                            End If
                            If Not StringUtil.Equals(vo.DataItemKaiteiInfo, buhinEdit.DataItemKaiteiInfo) Then
                                If isAdd Then
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 21, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 21, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 21, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 21, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '試作部品費'
                            If vo.ShisakuBuhinHi <> buhinEdit.ShisakuBuhinHi Then
                                If isAdd Then
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 9, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 9, START_ROW + rowIndex, &H9FFFFF)
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 16, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 16, START_ROW + rowIndex, &H9FFFFF)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 22, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 22, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 9, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 9, START_ROW + rowIndex, &HF0F0F0)
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 16, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 16, START_ROW + rowIndex, &HF0F0F0)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 22, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 22, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '試作型費'
                            If vo.ShisakuKataHi <> buhinEdit.ShisakuKataHi Then
                                If isAdd Then
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 10, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 10, START_ROW + rowIndex, &H9FFFFF)
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 17, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 17, START_ROW + rowIndex, &H9FFFFF)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 23, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 17, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 10, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 10, START_ROW + rowIndex, &HF0F0F0)
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 17, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 17, START_ROW + rowIndex, &HF0F0F0)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 23, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 23, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '部品NOTE'
                            If vo.BuhinNote Is Nothing Then
                                vo.BuhinNote = ""
                            End If
                            If buhinEdit.BuhinNote Is Nothing Then
                                buhinEdit.BuhinNote = ""
                            End If
                            If Not StringUtil.Equals(vo.BuhinNote.Trim, buhinEdit.BuhinNote.Trim) Then
                                If isAdd Then
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 11, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 11, START_ROW + rowIndex, &H9FFFFF)
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 18, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 18, START_ROW + rowIndex, &H9FFFFF)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 24, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 24, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 11, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 11, START_ROW + rowIndex, &HF0F0F0)
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 18, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 18, START_ROW + rowIndex, &HF0F0F0)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 24, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 24, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If
                            '備考'
                            If vo.Bikou Is Nothing Then
                                vo.Bikou = ""
                            End If
                            If buhinEdit.Bikou Is Nothing Then
                                buhinEdit.Bikou = ""
                            End If
                            If Not StringUtil.Equals(vo.Bikou.Trim, buhinEdit.Bikou.Trim) Then
                                If isAdd Then
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 12, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 12, START_ROW + rowIndex, &H9FFFFF)
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 19, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 19, START_ROW + rowIndex, &H9FFFFF)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 25, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 25, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 12, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 12, START_ROW + rowIndex, &HF0F0F0)
                                    'xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 19, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 19, START_ROW + rowIndex, &HF0F0F0)
                                    xls.SetBackColor(COL_START_GOUSYA + EditGousyaCount + 25, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 25, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If

                            If isAdd Then
                                If vo.HyojijunNo = buhinEdit.HyojijunNo Then
                                    insuFlag = True
                                End If
                            End If
                            ''↑↑2014/12/26 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END
                            ''↑↑2014/09/04 Ⅰ.2.管理項目追加 酒井 ADD END

                            '員数'
                            If vo.HyojijunNo = buhinEdit.HyojijunNo Then
                                If vo.InsuSuryo <> buhinEdit.InsuSuryo Then
                                    If isAdd Then
                                        xls.SetBackColor(COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, &H9FFFFF)
                                    Else
                                        xls.SetBackColor(COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, &HF0F0F0)
                                    End If
                                End If
                            End If

                            '員数'
                            If vo.BaseInstlFlg <> buhinEdit.BaseInstlFlg Then
                                If isAdd Then
                                    xls.SetBackColor(COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, &H9FFFFF)
                                Else
                                    xls.SetBackColor(COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, &HF0F0F0)
                                End If
                            End If

                            '全件チェックの結果同一の部品が存在する'
                        Else
                            If isAdd Then
                                xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_LEVEL, START_ROW + rowIndex, &H9FFFFF)
                                xls.SetBackColor(COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_LEVEL, START_ROW + rowIndex, &HA0A0A0)
                                xls.SetBackColor(COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, &HA0A0A0)
                            End If

                        End If
                    End If
                End If
            Next

            If Not insuFlag Then
                If isAdd Then
                    xls.SetBackColor(COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, &H9FFFFF)
                End If
            End If


            '存在しない部品なので'
            If Not buhinNoFlag Then
                If isAdd Then
                    xls.SetBackColor(COL_BUHIN_NO, START_ROW + rowIndex, COL_BUHIN_NO, START_ROW + rowIndex, &H9FFFFF)
                    xls.SetBackColor(COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, &H9FFFFF)
                Else
                    xls.SetBackColor(COL_BUHIN_NO, START_ROW + rowIndex, COL_BUHIN_NO, START_ROW + rowIndex, &HA0A0A0)
                    xls.SetBackColor(COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + buhinEdit.HyojijunNo, START_ROW + rowIndex, &HA0A0A0)
                End If
            End If

        End Sub

        ''' <summary>
        ''' 出力するかを正式に決定する
        ''' </summary>
        ''' <param name="buhinEditList"></param>
        ''' <remarks></remarks>
        Private Sub WriteChk(ByVal buhinEditList As List(Of TShisakuBuhinEditVoHelperExcel))
            '一か所でもFlagがFalseなら同じ部品番号表示順の員数を出力するようにする'

            For Each buhinEdit As TShisakuBuhinEditVoHelperExcel In buhinEditList
                For Each vo As TShisakuBuhinEditVoHelperExcel In buhinEditList
                    '部品番号表示順'
                    If buhinEdit.BuhinNoHyoujiJun = vo.BuhinNoHyoujiJun And buhinEdit.InstlHinbanHyoujiJun = vo.InstlHinbanHyoujiJun Then
                        If Not buhinEdit.Flag Then
                            vo.Flag = False
                        End If
                    End If
                Next
            Next
        End Sub

        ''' <summary>
        ''' 員数のマージ処理を行ってみる
        ''' </summary>
        ''' <param name="BuhinList"></param>
        ''' <remarks></remarks>
        Private Sub MergeList(ByVal BuhinList As List(Of TShisakuBuhinEditVoHelperExcel))

            '前回と今回で比較する用にマージ'
            Dim count As Integer = BuhinList.Count - 1

            For Each vo1 As TShisakuBuhinEditVoHelperExcel In BuhinList
                For Each vo2 As TShisakuBuhinEditVoHelperExcel In BuhinList
                    If vo1.BuhinNoHyoujiJun = vo2.BuhinNoHyoujiJun Then
                        If Me.BlockAlertKind = "2" And Me.KounyuShijiFlg = "0" Then
                            If vo1.BaseInstlFlg = vo2.BaseInstlFlg Then
                                If StringUtil.Equals(vo1.BuhinNo, vo2.BuhinNo) Then
                                    '--------------------------------------------------------------------------------------------
                                    'レベル’
                                    '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                                    '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                                    Dim vo1Level As String
                                    Dim vo2Level As String
                                    If StringUtil.IsEmpty(vo1.Level) Then
                                        vo1Level = ""
                                    Else
                                        vo1Level = vo1.Level
                                    End If
                                    If StringUtil.IsEmpty(vo2.Level) Then
                                        vo2Level = ""
                                    Else
                                        vo2Level = vo2.Level
                                    End If
                                    '--------------------------------------------------------------------------------------------
                                    If vo1Level = vo2Level Then
                                        If vo1.HyojijunNo = vo2.HyojijunNo Then
                                            If vo1.InstlHinbanHyoujiJun <> vo2.InstlHinbanHyoujiJun Then
                                                If Not vo1.Flag AndAlso Not vo2.Flag Then
                                                    vo1.InsuSuryo = vo1.InsuSuryo + vo2.InsuSuryo
                                                    vo2.Flag = True
                                                End If

                                            End If
                                        End If
                                    End If
                                End If

                            End If
                        Else
                            If StringUtil.Equals(vo1.BuhinNo, vo2.BuhinNo) Then
                                '--------------------------------------------------------------------------------------------
                                'レベル’
                                '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                                '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                                Dim vo1Level As String
                                Dim vo2Level As String
                                If StringUtil.IsEmpty(vo1.Level) Then
                                    vo1Level = ""
                                Else
                                    vo1Level = vo1.Level
                                End If
                                If StringUtil.IsEmpty(vo2.Level) Then
                                    vo2Level = ""
                                Else
                                    vo2Level = vo2.Level
                                End If
                                '--------------------------------------------------------------------------------------------
                                If vo1Level = vo2Level Then
                                    If vo1.HyojijunNo = vo2.HyojijunNo Then
                                        If vo1.InstlHinbanHyoujiJun <> vo2.InstlHinbanHyoujiJun Then
                                            If Not vo1.Flag AndAlso Not vo2.Flag Then
                                                vo1.InsuSuryo = vo1.InsuSuryo + vo2.InsuSuryo
                                                vo2.Flag = True
                                            End If

                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
            Next

            For i As Integer = 0 To count
                If BuhinList.Count > i Then
                    If Not BuhinList(i) Is Nothing Then
                        If BuhinList(i).Flag Then
                            BuhinList.Remove(BuhinList(i))
                            i = i - 1
                        End If
                    End If
                End If
            Next
        End Sub

#End Region


        ''' <summary>
        ''' シートの行列サイズ変更
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub SetRowCol(ByVal xls As ShisakuExcel)
            xls.AutoFitCol(COL_LEVEL, xls.EndCol)
            xls.AutoFitRow(1, xls.EndRow)

            xls.SetColWidth(COL_LEVEL, COL_LEVEL, 3)
            xls.SetColWidth(COL_TORIHIKISAKI_NAME, COL_TORIHIKISAKI_NAME, 16)
            xls.SetRowHeight(GOUSYA_ROW, GOUSYA_ROW, 114)

        End Sub

        ''' <summary>
        ''' 各列のタグに番号付与
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' 各行のタグに新たに番号を付与
        ''' </summary>
        ''' <param name="BuhinCount"></param>
        ''' <remarks></remarks>
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
        Private TITLE_ROW As Integer = 6
        '員数タイトル行'
        Private INSU_TITLE_ROW As Integer = 7
        '号車行'
        Private GOUSYA_ROW As Integer = 8
        'マージする行'
        Private MERGE_ROW As Integer = 9
        'データ書き込み開始行'
        Private START_ROW As Integer = 10

#End Region

    End Class
End Namespace