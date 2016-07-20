Imports ShisakuCommon
Imports EBom.Common
Imports EBom.Excel
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports ShisakuCommon.Util.LabelValue

''↓↓2014/09/18 酒井 ADD BEGIN
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
''↑↑2014/09/18 酒井 ADD END


Namespace ShisakuBuhinEditBlock.Export2Excel

    Public Class ExportShisakuBuhinEditGousyaConditionExcel

#Region "メンバ変数"

        Dim tehaiGousyaEnd As Integer = 0
        Private GousyaCount As Integer = 0
        Private EditGousyaCount As Integer = 0
        Private GousyaInsuCount As Integer = 0
        Private gousyaImpl As EditBlock2ExcelDao = New EditBlock2ExcelDaoImpl
        Private BlockNoList As List(Of TShisakuSekkeiBlockVo)
        Private BuhinEditList As List(Of TShisakuBuhinEditVoHelperExcel)
        Private oldBuhinEditList As List(Of TShisakuBuhinEditVoHelperExcel)
        Private BaseList As List(Of TShisakuEventBaseVo)

        ''' <summary>試作イベントコード</summary>
        Private shisakuEventCode As String

        ''' <summary>試作部課コード</summary>
        Private shisakuBukaCode As String

        ''' <summary>試作ブロックNo</summary>
        Private shisakuBlockNo As String
        ''' <summary>試作ブロックNo改訂No(新)</summary>
        Private newShisakuBlockNoKaiteiNo As String
        ''' <summary>試作ブロックNo改訂No(旧)</summary>
        Private oldShisakuBlockNoKaiteiNo As String

        Private eventVo As TShisakuEventVo

#End Region

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="newShisakuBlockNoKaiteiNo">試作ブロックNo改訂No(新)</param>
        ''' <param name="oldShisakuBlockNoKaiteiNo">試作ブロックNo改訂No(旧)</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal shisakuBukaCode As String, _
                       ByVal shisakuBlockNo As String, _
                       ByVal newShisakuBlockNoKaiteiNo As String, _
                       ByVal oldShisakuBlockNoKaiteiNo As String)

            Me.shisakuEventCode = shisakuEventCode
            Me.shisakuBukaCode = shisakuBukaCode
            Me.shisakuBlockNo = shisakuBlockNo
            Me.newShisakuBlockNoKaiteiNo = newShisakuBlockNoKaiteiNo
            Me.oldShisakuBlockNoKaiteiNo = oldShisakuBlockNoKaiteiNo
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Me.eventVo = eventDao.FindByPk(shisakuEventCode)

        End Sub

        ''' <summary>
        ''' EXCEL書き込み
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <param name="tanTouSya">担当者</param>
        ''' <param name="tel">電話番号</param>
        ''' <param name="shisakuKaihatsuFugo">開発符号</param>
        ''' <param name="shisakuEventName">イベント名称</param>
        ''' <param name="fileName">ファイル名</param>
        ''' <param name="sekkeiKa">設計課名</param>
        ''' <param name="isBase">ベースか？</param>
        ''' <remarks></remarks>
        Public Sub Excute(ByVal xls As ShisakuExcel, _
                          ByVal tanTouSya As String, _
                          ByVal tel As String, _
                          ByVal shisakuKaihatsuFugo As String, _
                          ByVal shisakuEventName As String, _
                          ByVal fileName As String, _
                          ByVal sekkeiKa As String, _
                          Optional ByVal isBase As Boolean = False)


            gousyaImpl = New EditBlock2ExcelDaoImpl
            BlockNoList = New List(Of TShisakuSekkeiBlockVo)
            BuhinEditList = New List(Of TShisakuBuhinEditVoHelperExcel)
            oldBuhinEditList = New List(Of TShisakuBuhinEditVoHelperExcel)
            BaseList = New List(Of TShisakuEventBaseVo)
            Dim BaseGousyaList As List(Of TShisakuEventBaseVo)
            BaseGousyaList = New List(Of TShisakuEventBaseVo)

            'ベース車情報リスト'
            BaseGousyaList = gousyaImpl.FindByBase(shisakuEventCode)

            'ベース車情報の穴抜け対策'
            For index As Integer = 0 To BaseGousyaList.Count - 1
                If index > 0 Then
                    If (BaseGousyaList(index).HyojijunNo = (BaseGousyaList(index - 1).HyojijunNo + 1)) Then
                        BaseList.Add(BaseGousyaList(index))
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
                            BaseList.Add(dummy)
                        Next
                        BaseList.Add(BaseGousyaList(index))
                    End If
                ElseIf index = 0 Then
                    BaseList.Add(BaseGousyaList(index))
                End If
            Next

            'ブロックNoが無ければ全体出力'
            If StringUtil.IsEmpty(shisakuBlockNo) Then
                xls.SetActiveSheet(1)
                'データが存在するブロックNoのリストを取得'
                BlockNoList = gousyaImpl.FindBySekkeiBlockNo(shisakuEventCode, shisakuBukaCode)

                For BlockNoIndex As Integer = 0 To BlockNoList.Count - 1
                    '号車と員数のリスト'
                    If Not BlockNoIndex = 0 Then
                        SetRowNo(BuhinEditList.Count)
                    End If

                    Dim headTitleVos As EditBlock2ExcelTitle3BodyVo = _
                    gousyaImpl.FindHeadInfoWithSekkeiBlockBy(shisakuEventCode, shisakuBukaCode, BlockNoList(BlockNoIndex).ShisakuBlockNo, _
                                                          BlockNoList(BlockNoIndex).ShisakuBlockNoKaiteiNo)

                    If Not headTitleVos Is Nothing Then

                        tanTouSya = headTitleVos.UserId
                        tel = headTitleVos.TelNo
                        shisakuKaihatsuFugo = headTitleVos.ShisakuKaihatsuFugo
                        shisakuEventName = headTitleVos.ShisakuEventName

                    End If


                    '部品編集表リスト'
                    BuhinEditList = gousyaImpl.FindByBuhinEdit(shisakuEventCode, shisakuBukaCode, BlockNoList(BlockNoIndex).ShisakuBlockNo, _
                                                               BlockNoList(BlockNoIndex).ShisakuBlockNoKaiteiNo)
                    oldBuhinEditList = gousyaImpl.FindByAllBuhinEditBase(shisakuEventCode, shisakuBukaCode, BlockNoList(BlockNoIndex).ShisakuBlockNo)

                    Me.SetHeader(xls, sekkeiKa, tanTouSya, shisakuKaihatsuFugo, shisakuEventName, tel, BlockNoList(BlockNoIndex).ShisakuBlockNo)
                    Me.SetTitle(xls, BaseList)
                    Me.SetBody(xls, BuhinEditList, oldBuhinEditList)
                    Me.SetRowCol(xls)
                Next

            Else
                'ブロック単体出力'
                xls.SetActiveSheet(3)

                '部品編集表リスト'
                BuhinEditList = gousyaImpl.FindByBuhinEdit(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, newShisakuBlockNoKaiteiNo)
                If isBase Then
                    'ベース部品編集表リスト'
                    oldBuhinEditList = gousyaImpl.FindByBuhinEditBase(shisakuEventCode, shisakuBukaCode, shisakuBlockNo)
                Else
                    oldBuhinEditList = gousyaImpl.FindByBuhinEdit(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, oldShisakuBlockNoKaiteiNo)
                End If

                If BuhinEditList.Count = 0 Then
                    Return
                End If
                MergeList(BuhinEditList)
                MergeList(oldBuhinEditList)
                Me.SetHeader(xls, sekkeiKa, tanTouSya, shisakuKaihatsuFugo, shisakuEventName, tel, shisakuBlockNo)
                Me.SetTitle(xls, BaseList)
                Me.SetBody(xls, BuhinEditList, oldBuhinEditList)
                Me.SetRowCol(xls)

                '2012/09/11 kabasawa凡例を追加'
                xls.SetFont(11, 1, 12, 5, "MS Pゴシック", 11, &HFF0000)
                xls.SetValue(11, 1, "差分表示凡例:")
                xls.SetValue(12, 2, "変更箇所")
                xls.SetValue(12, 3, "変更前のデータ")
                xls.SetValue(12, 4, "削除されたデータ")
                xls.SetValue(12, 5, "ベース車データ")

                xls.SetBackColor(11, 2, 11, 2, &H9FFFFF)
                xls.SetBackColor(11, 3, 11, 3, &HCCCCCC)
                xls.SetBackColor(11, 4, 11, 4, &HA0A0A0)
                xls.SetBackColor(11, 5, 11, 5, RGB(176, 215, 237))
            End If

        End Sub

        ''' <summary>
        ''' タイトルを設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <remarks></remarks>
        Private Sub SetTitle(ByVal xls As ShisakuExcel, ByVal baseList As List(Of TShisakuEventBaseVo))

            SetTitleStr(xls, baseList)
        End Sub

        ''' <summary>
        ''' ヘッダー部を設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <param name="sekkeika">設計課番号</param>
        ''' <param name="tantosya">担当ユーザーID</param>
        ''' <remarks></remarks>
        Private Sub SetHeader(ByVal xls As ShisakuExcel, ByVal sekkeika As String, ByVal tantosya As String, _
                              ByVal shisakuKaihatsuFugo As String, ByVal shisakuEventName As String, _
                              ByVal tel As String, ByVal blockNo As String)
            SetColumnNo()
            Dim kaImpl As KaRyakuNameDao = New KaRyakuNameDaoImpl

            Dim excelImpl As EditBlock2ExcelDao = New EditBlock2ExcelDaoImpl
            Dim tantoImpl As New ShisakuBuhinEditBlock.Dao.KaRyakuNameDaoImpl
            Dim TantoName As New Rhac1560Vo
            Dim UserName As String


            '担当設計が無い場合、コードを課略名称へ設定する。
            If Not StringUtil.IsEmpty(tantoImpl.GetKa_Ryaku_Name(sekkeika)) Then
                TantoName = tantoImpl.GetKa_Ryaku_Name(sekkeika)
            Else
                TantoName.KaRyakuName = sekkeika
            End If

            'TantoName = excelImpl.FindByKaRyakuName(tantosya)
            UserName = excelImpl.FindByShainName(tantosya)

            xls.SetValue(COL_LEVEL, HEADER_ROW, shisakuKaihatsuFugo + " " + shisakuEventName)

            xls.SetValue(COL_BUHIN_NO, HEADER_ROW, "担当設計：" + TantoName.KaRyakuName + " 担当者：" + tantosya + " Tel: " + tel)
            xls.SetValue(COL_LEVEL, HEADER_ROW + 1, "ブロックNo：" + blockNo)
        End Sub

        ''' <summary>
        ''' タイトルの文字を設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <remarks></remarks>
        Private Sub SetTitleStr(ByVal xls As ShisakuExcel, ByVal BaseList As List(Of TShisakuEventBaseVo))

            Dim alphabetList As New List(Of LabelValueVo)

            alphabetList = GetLabelValues_Column()

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

            '最初に実行
            Dim gousyaLastHyoujiJyun As Integer = BaseList(BaseList.Count - 1).HyojijunNo
            For i As Integer = 0 To gousyaLastHyoujiJyun
                xls.SetValue(COL_START_GOUSYA + i, INSU_TITLE_ROW, alphabetList(i).Label)
                xls.MergeCells(COL_START_GOUSYA + i, GOUSYA_ROW, _
                               COL_START_GOUSYA + i, MERGE_ROW, True)
                xls.SetOrientation(COL_START_GOUSYA + i, GOUSYA_ROW, _
                                   COL_START_GOUSYA + i, GOUSYA_ROW, ShisakuExcel.XlOrientation.xlVertical)
                gousyaIndex = gousyaIndex + 1
            Next

            '号車名'
            For index As Integer = 0 To BaseList.Count - 1
                '号車名が重複しないようにする'

                'If StringUtil.IsEmpty(BaseList(index).ShisakuGousya) Then
                '    Continue For
                'End If

                'xls.SetValue(COL_START_GOUSYA + gousyaIndex, INSU_TITLE_ROW, alphabetList(index).Label)
                xls.MergeCells(COL_START_GOUSYA + BaseList(index).HyojijunNo, GOUSYA_ROW, COL_START_GOUSYA + BaseList(index).HyojijunNo, MERGE_ROW, True)
                'xls.SetOrientation(COL_START_GOUSYA + BaseList(index).HyojijunNo, GOUSYA_ROW, _
                '                   COL_START_GOUSYA + BaseList(index).HyojijunNo, GOUSYA_ROW, ShisakuExcel.XlOrientation.xlVertical)

                xls.SetValue(COL_START_GOUSYA + BaseList(index).HyojijunNo, GOUSYA_ROW, BaseList(index).ShisakuGousya)

                'gousyaIndex = gousyaIndex + 1

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

            ''↓↓2014/09/24 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            gousyaIndex = gousyaIndex + 1
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + 6 + gousyaIndex, 8, True)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, "作り方")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, 9, "製作方法")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, 9, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, 9, "型仕様１")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, 9, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, 9, "型仕様２")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, 9, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, 9, "型仕様３")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, 9, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, 9, "治具")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, 9, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, 9, "納入見通し")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, 9, XlHAlign.xlHAlignCenter)
            gousyaIndex = gousyaIndex + 1
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, 9, "部品製作規模・概要")
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, 9, XlHAlign.xlHAlignCenter)
            ''↑↑2014/09/24 Ⅰ.2.管理項目追加 酒井 ADD END

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
            '部品ノート'
            gousyaIndex = gousyaIndex + 1
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + gousyaIndex, MERGE_ROW, True)
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, "NOTE")
            '備考'
            gousyaIndex = gousyaIndex + 1
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + gousyaIndex, MERGE_ROW, True)
            xls.SetAlliment(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, "備考")

        End Sub

        ''' <summary>
        ''' シートのBODY部を設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <remarks></remarks>
        Private Sub SetBody(ByVal xls As ShisakuExcel, ByVal buhinEditList As List(Of TShisakuBuhinEditVoHelperExcel), ByVal oldBuhinEditList As List(Of TShisakuBuhinEditVoHelperExcel))
            ''↓↓2014/09/18 酒井 ADD BEGIN
            '該当イベント取得
            ''↑↑2014/09/18 酒井 ADD END

            '速度向上の為、改行した時以外は共通データを書き込まないように変更
            'また１データづつ出力せず配列に格納し、範囲指定で出力するように変更
            Dim rowIndex As Integer = 0
            Dim rowBreak As Boolean = True
            Dim maxRowNumber As Integer = 0

            For index As Integer = 0 To buhinEditList.Count - 1

                rowBreak = True
                If Not index = 0 Then
                    If StringUtil.Equals(buhinEditList(index).BuhinNoHyoujiJun, buhinEditList(index - 1).BuhinNoHyoujiJun) Then
                        rowIndex = rowIndex - 1
                        rowBreak = False
                    End If
                End If

                If rowBreak Then
                    '--------------------------------------------------------------------------------------------
                    'レベル’
                    '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                    '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                    If StringUtil.IsEmpty(buhinEditList(index).Level) Then
                        xls.SetValue(COL_LEVEL, START_ROW + rowIndex, COL_LEVEL, START_ROW + rowIndex, "")
                    Else
                        xls.SetValue(COL_LEVEL, START_ROW + rowIndex, COL_LEVEL, START_ROW + rowIndex, buhinEditList(index).Level)
                    End If
                    '--------------------------------------------------------------------------------------------
                    '国内集計コード'
                    xls.SetValue(COL_SHUKEI_CODE, START_ROW + rowIndex, COL_SHUKEI_CODE, START_ROW + rowIndex, buhinEditList(index).ShukeiCode)
                    '海外集計コード'
                    xls.SetValue(COL_SIA_SHUKEI_CODE, START_ROW + rowIndex, COL_SIA_SHUKEI_CODE, START_ROW + rowIndex, buhinEditList(index).SiaShukeiCode)
                    '現調区分'
                    xls.SetValue(COL_GENCYO_KBN, START_ROW + rowIndex, COL_GENCYO_KBN, START_ROW + rowIndex, buhinEditList(index).GencyoCkdKbn)
                    '取引先コード'
                    xls.SetValue(COL_TORIHIKISAKI_CODE, START_ROW + rowIndex, COL_TORIHIKISAKI_CODE, START_ROW + rowIndex, buhinEditList(index).MakerCode)
                    '取引先名称'
                    xls.SetValue(COL_TORIHIKISAKI_NAME, START_ROW + rowIndex, COL_TORIHIKISAKI_NAME, START_ROW + rowIndex, buhinEditList(index).MakerName)
                    '部品番号'
                    xls.SetValue(COL_BUHIN_NO, START_ROW + rowIndex, COL_BUHIN_NO, START_ROW + rowIndex, buhinEditList(index).BuhinNo)
                    '部品番号区分'
                    xls.SetValue(COL_BUHIN_NO_KBN, START_ROW + rowIndex, COL_BUHIN_NO_KBN, START_ROW + rowIndex, buhinEditList(index).BuhinNoKbn)
                    '部品番号改訂No'
                    xls.SetValue(COL_KAITEI, START_ROW + rowIndex, COL_KAITEI, START_ROW + rowIndex, buhinEditList(index).BuhinNoKaiteiNo)
                    '枝番'
                    xls.SetValue(COL_EDA_BAN, START_ROW + rowIndex, COL_EDA_BAN, START_ROW + rowIndex, buhinEditList(index).EdaBan)
                    '部品名称'
                    xls.SetValue(COL_BUHIN_NAME, START_ROW + rowIndex, COL_BUHIN_NAME, START_ROW + rowIndex, buhinEditList(index).BuhinName)
                End If

                Dim insu As String = xls.GetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex)

                If StringUtil.IsEmpty(insu) Then
                    If buhinEditList(index).InsuSuryo < 0 Then
                        xls.SetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                    Else
                        xls.SetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, buhinEditList(index).InsuSuryo)
                    End If
                Else
                    If StringUtil.Equals(insu, "**") Then
                        xls.SetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                    Else
                        If buhinEditList(index).InsuSuryo < 0 Then
                            xls.SetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                        Else
                            Dim totalInsuSuryo As Integer = Integer.Parse(insu)
                            totalInsuSuryo = totalInsuSuryo + buhinEditList(index).InsuSuryo
                            xls.SetValue(COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo, START_ROW + rowIndex, totalInsuSuryo)
                        End If
                    End If
                End If

                EditGousyaCount = GousyaCount
                If rowBreak Then

                    '再使用不可'
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).Saishiyoufuka)

                    '2012/01/23 供給セクション追加
                    '供給セクション'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).KyoukuSection)

                    '出図予定日'
                    If buhinEditList(index).ShutuzuYoteiDate = "99999999" Or buhinEditList(index).ShutuzuYoteiDate = "0" Then
                        EditGousyaCount = EditGousyaCount + 1
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, "")
                    Else
                        EditGousyaCount = EditGousyaCount + 1
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).ShutuzuYoteiDate)
                    End If

                    '↓↓2014/09/24 酒井 ADD BEGIN
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).TsukurikataSeisaku)
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).TsukurikataKatashiyou1)
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).TsukurikataKatashiyou2)
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).TsukurikataKatashiyou3)
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).TsukurikataTigu)
                    If StringUtil.IsEmpty(buhinEditList(index).TsukurikataNounyu) Or buhinEditList(index).TsukurikataNounyu = "0" Then
                        EditGousyaCount = EditGousyaCount + 1
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, "")
                    Else
                        EditGousyaCount = EditGousyaCount + 1
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).TsukurikataNounyu)
                    End If
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).TsukurikataKibo)
                    '↑↑2014/09/24 酒井 ADD END

                    '材質'
                    '規格１'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).ZaishituKikaku1)
                    '規格２'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).ZaishituKikaku2)
                    '規格３'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).ZaishituKikaku3)
                    'メッキ'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).ZaishituMekki)

                    '板厚'
                    '板厚数量'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).ShisakuBankoSuryo)
                    '板厚数量U'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).ShisakuBankoSuryoU)
                    ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
                    '製品長
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).MaterialInfoLength)
                    '製品幅
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).MaterialInfoWidth)
                    '改訂№
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).DataItemKaiteiNo)
                    'エリア名
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).DataItemAreaName)
                    'セット名
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).DataItemSetName)
                    '改訂情報
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).DataItemKaiteiInfo)
                    ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END

                    '試作部品費'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).ShisakuBuhinHi)
                    '試作型費'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).ShisakuKataHi)
                    '部品ノート'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).BuhinNote)
                    '備考'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, buhinEditList(index).Bikou)
                End If

                If AllChk(buhinEditList(index), oldBuhinEditList) Then

                Else
                    AllChkColor(xls, buhinEditList(index), oldBuhinEditList, rowIndex)
                End If
                ''↓↓2014/09/18 酒井 ADD BEGIN
                '該当イベント取得
                If Me.eventVo.BlockAlertKind = 2 And Me.eventVo.KounyuShijiFlg = "0" Then
                    If buhinEditList(index).BaseInstlFlg = 1 Then
                        xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, RGB(176, 215, 237))
                    End If
                End If
                ''↑↑2014/09/18 酒井 ADD END
                rowIndex = rowIndex + 1
            Next

            '一部員数が同じ場合に出力されない個所があるので'
            WriteChk(oldBuhinEditList)

            'If Me.BlockAlertKind <> "2" Then

            'xls.SetValue(COL_LEVEL, START_ROW + rowIndex + 3, COL_LEVEL, START_ROW + rowIndex + 3, newShisakuBlockNoKaiteiNo)

            ''2015/07/02 追加 E.Ubukata Ver2.10.4
            '' 直前の表示順と比較しても意味がないため追加
            Dim bkBuhinNoHyoujiJun As Integer = -1

            For index As Integer = 0 To oldBuhinEditList.Count - 1
                rowBreak = True

                '出力するかどうかの判定'
                If Not oldBuhinEditList(index).Flag Then
                    If Not index = 0 Then
                        ''2015/07/02 変更 E.Ubukata Ver2.10.4
                        '' 直前の表示順と比較していては部品表示の最初の部品が出力対象外であった場合rowIndexがすすまない
                        'If StringUtil.Equals(oldBuhinEditList(index).BuhinNoHyoujiJun, oldBuhinEditList(index - 1).BuhinNoHyoujiJun) Then
                        If StringUtil.Equals(oldBuhinEditList(index).BuhinNoHyoujiJun, bkBuhinNoHyoujiJun) Then
                            rowIndex = rowIndex - 1
                            rowBreak = False
                        End If
                    End If
                    bkBuhinNoHyoujiJun = oldBuhinEditList(index).BuhinNoHyoujiJun
                    'If rowBreak Then
                    '--------------------------------------------------------------------------------------------
                    'レベル’
                    '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                    '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                    If StringUtil.IsEmpty(oldBuhinEditList(index).Level) Then
                        xls.SetValue(COL_LEVEL, START_ROW + rowIndex, COL_LEVEL, START_ROW + rowIndex, "")
                    Else
                        xls.SetValue(COL_LEVEL, START_ROW + rowIndex, COL_LEVEL, START_ROW + rowIndex, oldBuhinEditList(index).Level)
                    End If
                    '--------------------------------------------------------------------------------------------
                    '国内集計コード'
                    xls.SetValue(COL_SHUKEI_CODE, START_ROW + rowIndex, COL_SHUKEI_CODE, START_ROW + rowIndex, oldBuhinEditList(index).ShukeiCode)
                    '海外集計コード'
                    xls.SetValue(COL_SIA_SHUKEI_CODE, START_ROW + rowIndex, COL_SIA_SHUKEI_CODE, START_ROW + rowIndex, oldBuhinEditList(index).SiaShukeiCode)
                    '現調区分'
                    xls.SetValue(COL_GENCYO_KBN, START_ROW + rowIndex, COL_GENCYO_KBN, START_ROW + rowIndex, oldBuhinEditList(index).GencyoCkdKbn)
                    '取引先コード'
                    xls.SetValue(COL_TORIHIKISAKI_CODE, START_ROW + rowIndex, COL_TORIHIKISAKI_CODE, START_ROW + rowIndex, oldBuhinEditList(index).MakerCode)
                    '取引先名称'
                    xls.SetValue(COL_TORIHIKISAKI_NAME, START_ROW + rowIndex, COL_TORIHIKISAKI_NAME, START_ROW + rowIndex, oldBuhinEditList(index).MakerName)
                    '部品番号'
                    xls.SetValue(COL_BUHIN_NO, START_ROW + rowIndex, COL_BUHIN_NO, START_ROW + rowIndex, oldBuhinEditList(index).BuhinNo)
                    '部品番号区分'
                    xls.SetValue(COL_BUHIN_NO_KBN, START_ROW + rowIndex, COL_BUHIN_NO_KBN, START_ROW + rowIndex, oldBuhinEditList(index).BuhinNoKbn)
                    '部品番号改訂No'
                    xls.SetValue(COL_KAITEI, START_ROW + rowIndex, COL_KAITEI, START_ROW + rowIndex, oldBuhinEditList(index).BuhinNoKaiteiNo)
                    '枝番'
                    xls.SetValue(COL_EDA_BAN, START_ROW + rowIndex, COL_EDA_BAN, START_ROW + rowIndex, oldBuhinEditList(index).EdaBan)
                    '部品名称'
                    xls.SetValue(COL_BUHIN_NAME, START_ROW + rowIndex, COL_BUHIN_NAME, START_ROW + rowIndex, oldBuhinEditList(index).BuhinName)
                    'End If

                    'If oldBuhinEditList(index).Flag Then
                    Dim insu As String = xls.GetValue(COL_START_GOUSYA + oldBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + oldBuhinEditList(index).HyojijunNo, START_ROW + rowIndex)

                    If StringUtil.IsEmpty(insu) Then
                        If oldBuhinEditList(index).InsuSuryo < 0 Then
                            xls.SetValue(COL_START_GOUSYA + oldBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + oldBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                        Else
                            xls.SetValue(COL_START_GOUSYA + oldBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + oldBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, oldBuhinEditList(index).InsuSuryo)
                        End If
                    Else
                        If StringUtil.Equals(insu, "**") Then
                            xls.SetValue(COL_START_GOUSYA + oldBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + oldBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                        Else
                            If oldBuhinEditList(index).InsuSuryo < 0 Then
                                xls.SetValue(COL_START_GOUSYA + oldBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + oldBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, "**")
                            Else
                                Dim totalInsuSuryo As Integer = Integer.Parse(insu)
                                totalInsuSuryo = totalInsuSuryo + oldBuhinEditList(index).InsuSuryo
                                xls.SetValue(COL_START_GOUSYA + oldBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, COL_START_GOUSYA + oldBuhinEditList(index).HyojijunNo, START_ROW + rowIndex, totalInsuSuryo)
                            End If
                        End If
                    End If
                    'End If


                    EditGousyaCount = GousyaCount
                    'If rowBreak Then

                    '再使用不可'
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).Saishiyoufuka)

                    '2012/01/23 供給セクション追加
                    '供給セクション'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).KyoukuSection)

                    '出図予定日'
                    If oldBuhinEditList(index).ShutuzuYoteiDate = "99999999" Or oldBuhinEditList(index).ShutuzuYoteiDate = "0" Then
                        EditGousyaCount = EditGousyaCount + 1
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, "")
                    Else
                        EditGousyaCount = EditGousyaCount + 1
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).ShutuzuYoteiDate)
                    End If

                    '↓↓2014/09/24 酒井 ADD BEGIN
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).TsukurikataSeisaku)
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).TsukurikataKatashiyou1)
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).TsukurikataKatashiyou2)
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).TsukurikataKatashiyou3)
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).TsukurikataTigu)
                    '↓↓2014/10/03 酒井 ADD BEGIN
                    'If StringUtil.IsEmpty(buhinEditList(index).TsukurikataNounyu) Or buhinEditList(index).TsukurikataNounyu = "0" Then
                    If StringUtil.IsEmpty(oldBuhinEditList(index).TsukurikataNounyu) Or oldBuhinEditList(index).TsukurikataNounyu = "0" Then
                        '↑↑2014/10/03 酒井 ADD END
                        EditGousyaCount = EditGousyaCount + 1
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, "")
                    Else
                        EditGousyaCount = EditGousyaCount + 1
                        xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).TsukurikataNounyu)
                    End If
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).TsukurikataKibo)
                    '↑↑2014/09/24 酒井 ADD END

                    '材質'
                    '規格１'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).ZaishituKikaku1)
                    '規格２'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).ZaishituKikaku2)
                    '規格３'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).ZaishituKikaku3)
                    'メッキ'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).ZaishituMekki)

                    '板厚'
                    '板厚数量'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).ShisakuBankoSuryo)
                    '板厚数量U'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).ShisakuBankoSuryoU)

                    ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
                    '材料情報'
                    '製品長'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).MaterialInfoLength)
                    '製品幅'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).MaterialInfoWidth)
                    'データ項目
                    '改訂№'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).DataItemKaiteiNo)
                    'エリア名'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).DataItemAreaName)
                    'セット名'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).DataItemSetName)
                    '改訂情報'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).DataItemKaiteiInfo)
                    ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END

                    '試作部品費'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).ShisakuBuhinHi)
                    '試作型費'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).ShisakuKataHi)
                    '部品ノート'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).BuhinNote)
                    '備考'
                    EditGousyaCount = EditGousyaCount + 1
                    xls.SetValue(COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, oldBuhinEditList(index).Bikou)
                    'End If


                    '↓↓2014/09/24 酒井 ADD BEGIN
                    'xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 12, START_ROW + rowIndex, &HA0A0A0)
                    ''↓↓2014/12/29 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                    'xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 19, START_ROW + rowIndex, &HA0A0A0)
                    xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 25, START_ROW + rowIndex, &HA0A0A0)
                    ''↑↑2014/12/29 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END

                    '↑↑2014/09/24 酒井 ADD END
                    AllChkColor(xls, oldBuhinEditList(index), buhinEditList, rowIndex, False)

                    rowIndex = rowIndex + 1
                End If

            Next
            'End If

            '員数列左の罫線'
            xls.SetLine(COL_START_GOUSYA, START_ROW, COL_START_GOUSYA, xls.EndRow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            '員数列右の罫線'
            xls.SetLine(COL_START_GOUSYA + GousyaCount, START_ROW, COL_START_GOUSYA + GousyaCount, xls.EndRow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            '再使用不可右の罫線'
            xls.SetLine(COL_START_GOUSYA + GousyaCount, TITLE_ROW, COL_START_GOUSYA + GousyaCount, xls.EndRow, XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)

        End Sub

        ''' <summary>
        ''' 部品編集情報チェック
        ''' </summary>
        ''' <param name="buhinEdit"></param>
        ''' <param name="buhinEditList"></param>
        ''' <remarks></remarks>
        Private Function AllChk(ByVal buhinEdit As TShisakuBuhinEditVoHelperExcel, ByVal buhinEditList As List(Of TShisakuBuhinEditVoHelperExcel)) As Boolean

            For Each vo As TShisakuBuhinEditVoHelperExcel In buhinEditList
                '全部チェック'
                '部品番号'
                If StringUtil.Equals(vo.BuhinNo.Trim, buhinEdit.BuhinNo.Trim) Then
                    If vo.Flag Then
                        Continue For
                    End If
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
                        ''↓↓2014/12/29 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
                        '作り方
                        '製作方法
                        If vo.TsukurikataSeisaku Is Nothing Then
                            vo.TsukurikataSeisaku = ""
                        End If
                        If buhinEdit.TsukurikataSeisaku Is Nothing Then
                            buhinEdit.TsukurikataSeisaku = ""
                        End If
                        If Not StringUtil.Equals(vo.TsukurikataSeisaku.Trim, buhinEdit.TsukurikataSeisaku.Trim) Then
                            Continue For
                        End If
                        '型仕様１
                        If vo.TsukurikataKatashiyou1 Is Nothing Then
                            vo.TsukurikataKatashiyou1 = ""
                        End If
                        If buhinEdit.TsukurikataKatashiyou1 Is Nothing Then
                            buhinEdit.TsukurikataKatashiyou1 = ""
                        End If
                        If Not StringUtil.Equals(vo.TsukurikataKatashiyou1.Trim, buhinEdit.TsukurikataKatashiyou1.Trim) Then
                            Continue For
                        End If
                        '型仕様２
                        If vo.TsukurikataKatashiyou2 Is Nothing Then
                            vo.TsukurikataKatashiyou2 = ""
                        End If
                        If buhinEdit.TsukurikataKatashiyou2 Is Nothing Then
                            buhinEdit.TsukurikataKatashiyou2 = ""
                        End If
                        If Not StringUtil.Equals(vo.TsukurikataKatashiyou2.Trim, buhinEdit.TsukurikataKatashiyou2.Trim) Then
                            Continue For
                        End If
                        '型仕様３
                        If vo.TsukurikataKatashiyou3 Is Nothing Then
                            vo.TsukurikataKatashiyou3 = ""
                        End If
                        If buhinEdit.TsukurikataKatashiyou3 Is Nothing Then
                            buhinEdit.TsukurikataKatashiyou3 = ""
                        End If
                        If Not StringUtil.Equals(vo.TsukurikataKatashiyou3.Trim, buhinEdit.TsukurikataKatashiyou3.Trim) Then
                            Continue For
                        End If
                        '治具
                        If vo.TsukurikataTigu Is Nothing Then
                            vo.TsukurikataTigu = ""
                        End If
                        If buhinEdit.TsukurikataTigu Is Nothing Then
                            buhinEdit.TsukurikataTigu = ""
                        End If
                        If Not StringUtil.Equals(vo.TsukurikataTigu.Trim, buhinEdit.TsukurikataTigu.Trim) Then
                            Continue For
                        End If
                        '納入見通し
                        Dim wkTsukurikataNounyu1 As String = ""
                        Dim wkTsukurikataNounyu2 As String = ""
                        If vo.TsukurikataNounyu IsNot Nothing Then
                            wkTsukurikataNounyu1 = vo.TsukurikataNounyu.ToString
                        End If
                        If buhinEdit.TsukurikataNounyu IsNot Nothing Then
                            wkTsukurikataNounyu2 = buhinEdit.TsukurikataNounyu.ToString
                        End If
                        If Not StringUtil.Equals(wkTsukurikataNounyu1, wkTsukurikataNounyu2) Then
                            Continue For
                        End If
                        '部品製作規模・概要
                        If vo.TsukurikataKibo Is Nothing Then
                            vo.TsukurikataKibo = ""
                        End If
                        If buhinEdit.TsukurikataKibo Is Nothing Then
                            buhinEdit.TsukurikataKibo = ""
                        End If
                        If Not StringUtil.Equals(vo.TsukurikataKibo.Trim, buhinEdit.TsukurikataKibo.Trim) Then
                            Continue For
                        End If
                        ''↑↑2014/12/29 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END  

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
                        '製品長'
                        If Not StringUtil.Equals(vo.MaterialInfoLength, buhinEdit.MaterialInfoLength) Then
                            Continue For
                        End If
                        '製品幅'
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

                        '号車表示順'
                        If vo.HyojijunNo <> buhinEdit.HyojijunNo Then
                            Continue For
                        End If

                        If Me.eventVo.BlockAlertKind = 2 And Me.eventVo.KounyuShijiFlg = "0" And vo.InstlHinbanHyoujiJun <> buhinEdit.InstlHinbanHyoujiJun Then
                            Continue For
                        End If

                        '員数'
                        If vo.InsuSuryo <> buhinEdit.InsuSuryo Then
                            '員数が違うだけなので、全部出す'
                            vo.Flag = False
                            Continue For
                        End If
                        '全件チェックの結果同一の部品が存在する'
                        '完全同一の場合は出力しない'

                        vo.Flag = True
                        '旧構成から削除'
                        'buhinEditList.Remove(vo)

                        Return True
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
                '部品番号'  
                If StringUtil.Equals(vo.BuhinNo.Trim, buhinEdit.BuhinNo.Trim) Then
                    '↓↓2014/10/29 酒井 ADD BEGIN
                    'Ver6_2 1.95以降の修正内容の展開
                    'buhinNoFlag = True
                    '2014/07/15 追加
                    '　buhinEditListから削除せずにFlagを立てているのにFlagを見ていないため追加
                    If Not vo.Flag Then
                        buhinNoFlag = True
                    End If
                    'buhinNoFlag = True
                    '↑↑2014/10/29 酒井 ADD END
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

                        '変更確定のため変更色にする'
                        If Not isAdd Then
                            ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                            ' xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 12, START_ROW + rowIndex, &HCCCCCC)
                            xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 25, START_ROW + rowIndex, &HCCCCCC)
                            ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END
                            Continue For
                        End If

                        '国内集計'
                        If Not StringUtil.Equals(vo.ShukeiCode.Trim, buhinEdit.ShukeiCode.Trim) Then
                            If isAdd Then
                                xls.SetBackColor(COL_SHUKEI_CODE, START_ROW + rowIndex, COL_SHUKEI_CODE, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_SHUKEI_CODE, START_ROW + rowIndex, COL_SHUKEI_CODE, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 1, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 1, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 1, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 1, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 2, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 2, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 2, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 2, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If

                        ''↓↓2014/12/29 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
                        '作り方
                        '製作方法
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
                        '型仕様１
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

                        '型仕様２
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

                        '型仕様３
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

                        '治具
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

                        '納入見通し
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
                        '部品製作規模・概要
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

                        ''↑↑2014/12/29 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END

                        '材質規格1'
                        If vo.ZaishituKikaku1 Is Nothing Then
                            vo.ZaishituKikaku1 = ""
                        End If
                        If buhinEdit.ZaishituKikaku1 Is Nothing Then
                            buhinEdit.ZaishituKikaku1 = ""
                        End If
                        If Not StringUtil.Equals(vo.ZaishituKikaku1.Trim, buhinEdit.ZaishituKikaku1.Trim) Then
                            If isAdd Then
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 10, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 10, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 10, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 10, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 11, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 11, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 11, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 11, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 12, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 12, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 12, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 12, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 13, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 13, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 13, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 13, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 14, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 14, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 14, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 14, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 15, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 15, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 15, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 15, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
                        '製品長
                        If Not StringUtil.Equals(vo.MaterialInfoLength, buhinEdit.MaterialInfoLength) Then
                            If isAdd Then
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 16, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 16, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 16, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 16, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        '製品幅
                        If Not StringUtil.Equals(vo.MaterialInfoWidth, buhinEdit.MaterialInfoWidth) Then
                            If isAdd Then
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 17, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 17, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 17, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 17, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        '改訂№'
                        If vo.DataItemKaiteiNo Is Nothing Then
                            vo.DataItemKaiteiNo = ""
                        End If
                        If buhinEdit.DataItemKaiteiNo Is Nothing Then
                            buhinEdit.DataItemKaiteiNo = ""
                        End If
                        If Not StringUtil.Equals(vo.DataItemKaiteiNo.Trim, buhinEdit.DataItemKaiteiNo.Trim) Then
                            If isAdd Then
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 18, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 18, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 18, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 18, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        'エリア名'
                        If vo.DataItemAreaName Is Nothing Then
                            vo.DataItemAreaName = ""
                        End If
                        If buhinEdit.DataItemAreaName Is Nothing Then
                            buhinEdit.DataItemAreaName = ""
                        End If
                        If Not StringUtil.Equals(vo.DataItemAreaName.Trim, buhinEdit.DataItemAreaName.Trim) Then
                            If isAdd Then
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 19, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 19, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 19, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 19, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        'セット名'
                        If vo.DataItemSetName Is Nothing Then
                            vo.DataItemSetName = ""
                        End If
                        If buhinEdit.DataItemSetName Is Nothing Then
                            buhinEdit.DataItemSetName = ""
                        End If
                        If Not StringUtil.Equals(vo.DataItemSetName.Trim, buhinEdit.DataItemSetName.Trim) Then
                            If isAdd Then
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 20, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 20, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 20, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 20, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        '改訂情報'
                        If vo.DataItemKaiteiInfo Is Nothing Then
                            vo.DataItemKaiteiInfo = ""
                        End If
                        If buhinEdit.DataItemKaiteiInfo Is Nothing Then
                            buhinEdit.DataItemKaiteiInfo = ""
                        End If
                        If Not StringUtil.Equals(vo.DataItemKaiteiInfo.Trim, buhinEdit.DataItemKaiteiInfo.Trim) Then
                            If isAdd Then
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 21, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 21, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 21, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 21, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END
                        ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                        '試作部品費'
                        If vo.ShisakuBuhinHi <> buhinEdit.ShisakuBuhinHi Then
                            If isAdd Then
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 22, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 22, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 22, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 22, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        '試作型費'
                        If vo.ShisakuKataHi <> buhinEdit.ShisakuKataHi Then
                            If isAdd Then
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 23, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 23, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 23, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 23, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        '部品NOTE'
                        If buhinEdit.BuhinNote Is Nothing Then
                            buhinEdit.BuhinNote = ""
                        End If
                        If vo.BuhinNote Is Nothing Then
                            vo.BuhinNote = ""
                        End If

                        If Not StringUtil.Equals(vo.BuhinNote.Trim, buhinEdit.BuhinNote.Trim) Then
                            If isAdd Then
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 24, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 24, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 24, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 24, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If

                        '備考'
                        If buhinEdit.Bikou Is Nothing Then
                            buhinEdit.Bikou = ""
                        End If
                        If vo.Bikou Is Nothing Then
                            vo.Bikou = ""
                        End If

                        If Not StringUtil.Equals(vo.Bikou.Trim, buhinEdit.Bikou.Trim) Then
                            If isAdd Then
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 25, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 25, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_GOUSYA + GousyaCount + 25, START_ROW + rowIndex, COL_START_GOUSYA + GousyaCount + 25, START_ROW + rowIndex, &HF0F0F0)
                            End If


                        End If
                        ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END

                        If isAdd Then
                            If vo.HyojijunNo = buhinEdit.HyojijunNo Then
                                insuFlag = True
                            End If
                        End If

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

                        '全件チェックの結果同一の部品が存在する'
                    Else
                        If isAdd Then
                            xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_LEVEL, START_ROW + rowIndex, &H9FFFFF)
                        Else
                            xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_LEVEL, START_ROW + rowIndex, &HA0A0A0)
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
            '一か所でもFlagがfalseなら同じ部品番号表示順の員数を出力するようにする'

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
        ''' シートの行の高さと列の幅を設定
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
        ''' 各列のタグに番号付与
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetColumnNo()
            Dim col As Integer = 1
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
        ''' 各行のタグに番号を付与
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

#Region "各列のタグ"
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
                        If Me.eventVo.BlockAlertKind = "2" And Me.eventVo.KounyuShijiFlg = "0" Then

                            If vo1.BaseInstlFlg = vo2.BaseInstlFlg Then
                                If StringUtil.Equals(vo1.BuhinNo, vo2.BuhinNo) Then
                                    If StringUtil.Equals(vo1.KyoukuSection, vo2.KyoukuSection) Then
                                        If StringUtil.Equals(vo1.Saishiyoufuka, vo2.Saishiyoufuka) Then
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

                            End If
                        Else
                            If StringUtil.Equals(vo1.BuhinNo, vo2.BuhinNo) Then
                                If StringUtil.Equals(vo1.KyoukuSection, vo2.KyoukuSection) Then
                                    If StringUtil.Equals(vo1.Saishiyoufuka, vo2.Saishiyoufuka) Then
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

    End Class

End Namespace

