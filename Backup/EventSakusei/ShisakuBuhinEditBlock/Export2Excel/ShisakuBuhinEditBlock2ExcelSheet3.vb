Imports ShisakuCommon
Imports EBom.Common
Imports EBom.Excel
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports ShisakuCommon.Util.LabelValue
Imports EventSakusei.ShisakuBuhinEdit.Al.Logic
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl



Namespace ShisakuBuhinEditBlock.Export2Excel

    Public Class ShisakuBuhinEditBlock2ExcelSheet3
        Dim tehaiGousyaEnd As Integer = 0
        Private GousyaCount As Integer = 0
        Private EditGousyaCount As Integer = 0
        Private GousyaInsuCount As Integer = 0
        Private gousyaImpl As EditBlock2ExcelDao = New EditBlock2ExcelDaoImpl
        Private BlockNoList As List(Of TShisakuSekkeiBlockVo)
        Private BuhinEditList As List(Of TShisakuBuhinEditVoHelperExcel)
        Private BaseList As List(Of TShisakuEventBaseVo)
        Private ReadOnly subject As BuhinEditAlSubject

        Public Sub New()

        End Sub

        Public Sub Excute(ByVal xls As ShisakuExcel, _
                          ByVal eventCode As String, _
                          ByVal seKeka As String, _
                          ByVal blockNo As String, _
                          ByVal blockKaiteNo As String, _
                          ByVal tanTouSya As String, _
                          ByVal tel As String, _
                          ByVal shisakuKaihatsuFugo As String, _
                          ByVal shisakuEventName As String, _
                          ByVal shisakuBukaCode As String, _
                          ByVal fileName As String, _
                          Optional ByVal isBase As Boolean = False)

            gousyaImpl = New EditBlock2ExcelDaoImpl
            BlockNoList = New List(Of TShisakuSekkeiBlockVo)
            BuhinEditList = New List(Of TShisakuBuhinEditVoHelperExcel)
            BaseList = New List(Of TShisakuEventBaseVo)
            Dim BaseGousyaList As List(Of TShisakuEventBaseVo)
            BaseGousyaList = New List(Of TShisakuEventBaseVo)

            BaseGousyaList = gousyaImpl.FindByBase(eventCode)

            'ベース車情報の穴抜け対策'
            For index As Integer = 0 To BaseGousyaList.Count - 1
                If index > 0 Then
                    If (BaseGousyaList(index).HyojijunNo = (BaseGousyaList(index - 1).HyojijunNo + 1)) Then
                        BaseList.Add(BaseGousyaList(index))
                    Else
                        '抜けてる表示順Noの数だけ空行を挿入'
                        Dim row As Integer = (BaseGousyaList(index).HyojijunNo - BaseGousyaList(index - 1).HyojijunNo) - 1
                        '   2012/12/13 ダミー列の開始位置用
                        Dim intDummyIndex As Integer = BaseGousyaList(index).HyojijunNo - row
                        For index2 As Integer = 0 To row - 1
                            Dim dummy As TShisakuEventBaseVo
                            dummy = New TShisakuEventBaseVo
                            '   2012/12/13 前にダミー列があった場合を考慮して開始位置を調整   By柳沼
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





            ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ad) (TES)張 ADD BEGIN
            '該当イベント取得
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(eventCode)
            ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ad) (TES)張 ADD END


            'ブロックNoが無ければ全体出力'
            If StringUtil.IsEmpty(blockNo) Then
                xls.SetActiveSheet(1)
                'データが存在するブロックNoのリストを取得'
                BlockNoList = gousyaImpl.FindBySekkeiBlockNo(eventCode, shisakuBukaCode)

                For BlockNoIndex As Integer = 0 To BlockNoList.Count - 1
                    '号車と員数のリスト'
                    If Not BlockNoIndex = 0 Then
                        SetRowNo(BuhinEditList.Count)
                    End If

                    Dim headTitleVos As EditBlock2ExcelTitle3BodyVo = _
                    gousyaImpl.FindHeadInfoWithSekkeiBlockBy(eventCode, shisakuBukaCode, BlockNoList(BlockNoIndex).ShisakuBlockNo, _
                                                          BlockNoList(BlockNoIndex).ShisakuBlockNoKaiteiNo)

                    If Not headTitleVos Is Nothing Then

                        tanTouSya = headTitleVos.UserId
                        tel = headTitleVos.TelNo
                        shisakuKaihatsuFugo = headTitleVos.ShisakuKaihatsuFugo
                        shisakuEventName = headTitleVos.ShisakuEventName

                    End If

                    '部品編集表リスト'
                    BuhinEditList = gousyaImpl.FindByBuhinEdit(eventCode, shisakuBukaCode, BlockNoList(BlockNoIndex).ShisakuBlockNo, _
                                                         BlockNoList(BlockNoIndex).ShisakuBlockNoKaiteiNo)

                    Me.SetHeader(xls, seKeka, tanTouSya, shisakuKaihatsuFugo, shisakuEventName, tel, BlockNoList(BlockNoIndex).ShisakuBlockNo)
                    Me.SetTitle(xls, BaseList)
                    ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ad) (TES)張 CHG BEGIN
                    'Me.SetBody(xls, BuhinEditList)
                    Me.SetBody(xls, BuhinEditList, eventVo)
                    ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ad) (TES)張 CHG END
                    Me.SetRowCol(xls)
                Next

            Else
                'ブロック単体出力'
                xls.SetActiveSheet(3)

                If isBase Then
                    '部品編集表リスト'
                    BuhinEditList = gousyaImpl.FindByBuhinEditBase(eventCode, shisakuBukaCode, blockNo)
                Else
                    '部品編集表リスト'
                    BuhinEditList = gousyaImpl.FindByBuhinEdit(eventCode, shisakuBukaCode, blockNo, blockKaiteNo)
                End If
                If BuhinEditList.Count = 0 Then
                    Return
                End If


                Me.SetHeader(xls, seKeka, tanTouSya, shisakuKaihatsuFugo, shisakuEventName, tel, blockNo)
                Me.SetTitle(xls, BaseList)
                ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ad) (TES)張 CHG BEGIN
                'Me.SetBody(xls, BuhinEditList)
                Me.SetBody(xls, BuhinEditList, eventVo)
                ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ad) (TES)張 CHG END
                Me.SetRowCol(xls)
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
                'xls.MergeCells(COL_START_GOUSYA + BaseList(index).HyojijunNo, GOUSYA_ROW, COL_START_GOUSYA + BaseList(index).HyojijunNo, MERGE_ROW, True)
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

            ''↓↓2014/07/25 Ⅰ.2.管理項目追加_au) (TES)張 ADD BEGIN
            '作り方'
            gousyaIndex = gousyaIndex + 1
            xls.MergeCells(COL_START_GOUSYA + gousyaIndex, TITLE_ROW, COL_START_GOUSYA + 5 + gousyaIndex, GOUSYA_ROW, True)
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
            ''↑↑2014/07/25 Ⅰ.2.管理項目追加_au) (TES)張 ADD END

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

        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ad) (TES)張 CHG BEGIN
        'Private Sub SetBody(ByVal xls As ShisakuExcel, ByVal buhinEditList As List(Of TShisakuBuhinEditVoHelperExcel))
        ''' <summary>
        ''' シートのBODY部を設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <param name="buhinEditList">部品編集情報</param>
        ''' <param name="eventVo">イベント情報</param>
        ''' <remarks></remarks>
        Private Sub SetBody(ByVal xls As ShisakuExcel, ByVal buhinEditList As List(Of TShisakuBuhinEditVoHelperExcel), ByVal eventVo As TShisakuEventVo)
            ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ad) (TES)張 CHG END
            '速度向上の為、改行した時以外は共通データを書き込まないように変更
            'また１データづつ出力せず配列に格納し、範囲指定で出力するように変更
            Dim rowIndex As Integer = 0
            Dim rowBreak As Boolean = True
            Dim maxRowNumber As Integer = 0

            '２次元配列の列数を算出
            For i As Integer = 0 To buhinEditList.Count - 1
                If Not i = 0 Then
                    If Not StringUtil.Equals(buhinEditList(i).BuhinNoHyoujiJun, buhinEditList(i - 1).BuhinNoHyoujiJun) Then
                        maxRowNumber = maxRowNumber + 1
                        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ad) (TES)張 ADD BEGIN
                    Else
                        If eventVo.BlockAlertKind = 2 Then
                            If Not StringUtil.Equals(buhinEditList(i).BaseBuhinFlg, buhinEditList(i - 1).BaseBuhinFlg) Then
                                maxRowNumber = maxRowNumber + 1
                            End If
                            If Not StringUtil.Equals(buhinEditList(i).BaseInstlFlg, buhinEditList(i - 1).BaseInstlFlg) Then
                                maxRowNumber = maxRowNumber + 1
                            End If
                        End If
                        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ad) (TES)張 ADD END
                    End If
                End If
            Next

            ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
            '２次元配列作成
            Dim dataMatrix(maxRowNumber, COL_BUHIN_NAME + GousyaCount + 25) As String
            ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END

            For index As Integer = 0 To buhinEditList.Count - 1
                rowBreak = True
                If Not index = 0 Then
                    If StringUtil.Equals(buhinEditList(index).BuhinNoHyoujiJun, buhinEditList(index - 1).BuhinNoHyoujiJun) Then
                        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ad) (TES)張 CHG BEGIN
                        'rowIndex = rowIndex - 1
                        'rowBreak = False
                        If eventVo.BlockAlertKind = 2 Then
                            If StringUtil.Equals(buhinEditList(index).BaseInstlFlg, buhinEditList(index - 1).BaseInstlFlg) Then
                                'If StringUtil.Equals(buhinEditList(index).BaseBuhinFlg, buhinEditList(index - 1).BaseBuhinFlg) Then
                                rowIndex = rowIndex - 1
                                rowBreak = False
                            End If
                        Else
                            rowIndex = rowIndex - 1
                            rowBreak = False
                        End If
                        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_ad) (TES)張 CHG END
                    End If
                End If

                If rowBreak Then
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
                End If



                Dim insu As String = dataMatrix(rowIndex, COL_START_GOUSYA + buhinEditList(index).HyojijunNo - 1)


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

                EditGousyaCount = GousyaCount
                If rowBreak Then

                    '再使用不可'
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).Saishiyoufuka

                    '2012/01/23 供給セクション追加
                    '供給セクション'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).KyoukuSection

                    '出図予定日'
                    If buhinEditList(index).ShutuzuYoteiDate = "99999999" Or buhinEditList(index).ShutuzuYoteiDate = "0" Then
                        EditGousyaCount = EditGousyaCount + 1
                        dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = ""
                    Else
                        EditGousyaCount = EditGousyaCount + 1
                        dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).ShutuzuYoteiDate
                    End If
                    ''↓↓2014/07/25 Ⅰ.2.管理項目追加_av) (TES)張 ADD BEGIN
                    '作り方'
                    '製作方法'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).TsukurikataSeisaku
                    '型仕様１'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).TsukurikataKatashiyou1
                    '型仕様２'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).TsukurikataKatashiyou2
                    '型仕様３'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).TsukurikataKatashiyou3
                    '治具'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).TsukurikataTigu
                    '納入見通し'
                    If buhinEditList(index).TsukurikataNounyu = "0" Or String.IsNullOrEmpty(buhinEditList(index).TsukurikataNounyu.ToString) Then
                        EditGousyaCount = EditGousyaCount + 1
                        dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = ""
                    Else
                        EditGousyaCount = EditGousyaCount + 1
                        dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).TsukurikataNounyu
                    End If
                    '部品製作規模・概要'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).TsukurikataKibo
                    ''↑↑2014/07/25 Ⅰ.2.管理項目追加_av) (TES)張 ADD END

                    '材質'
                    '規格１'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).ZaishituKikaku1
                    '規格２'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).ZaishituKikaku2
                    '規格３'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).ZaishituKikaku3
                    'メッキ'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).ZaishituMekki

                    '板厚'
                    '板厚数量'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).ShisakuBankoSuryo
                    '板厚数量U'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).ShisakuBankoSuryoU

                    ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
                    '材料情報'
                    '製品長'
                    EditGousyaCount = EditGousyaCount + 1
                    If StringUtil.IsNotEmpty(buhinEditList(index).MaterialInfoLength) Then
                        dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).MaterialInfoLength
                    End If
                    '製品幅'
                    EditGousyaCount = EditGousyaCount + 1
                    If StringUtil.IsNotEmpty(buhinEditList(index).MaterialInfoWidth) Then
                        dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).MaterialInfoWidth
                    End If
                    'データ項目
                    '改訂№'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).DataItemKaiteiNo
                    'エリア名'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).DataItemAreaName
                    'セット名'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).DataItemSetName
                    '改訂情報'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).DataItemKaiteiInfo
                    ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END

                    '試作部品費'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).ShisakuBuhinHi
                    '試作型費'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).ShisakuKataHi
                    '部品ノート'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).BuhinNote
                    '備考'
                    EditGousyaCount = EditGousyaCount + 1
                    dataMatrix(rowIndex, COL_START_GOUSYA + EditGousyaCount - 1) = buhinEditList(index).Bikou
                End If

                ''↓↓2014/09/03 Ⅰ.3.設計編集 ベース改修専用化_ad) 酒井 ADD BEGIN
                If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                    If buhinEditList(index).BaseInstlFlg = 1 Then
                        xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_START_GOUSYA + EditGousyaCount, START_ROW + rowIndex, RGB(176, 215, 237))
                    End If
                End If
                ''↑↑2014/09/03 Ⅰ.3.設計編集 ベース改修専用化_ad) 酒井 ADD END

                rowIndex = rowIndex + 1

            Next
            xls.CopyRange(0, START_ROW, UBound(dataMatrix, 2) - 1, START_ROW + UBound(dataMatrix), dataMatrix)

            '員数列左の罫線'
            xls.SetLine(COL_START_GOUSYA, START_ROW, COL_START_GOUSYA, xls.EndRow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            '員数列右の罫線'
            xls.SetLine(COL_START_GOUSYA + GousyaCount, START_ROW, COL_START_GOUSYA + GousyaCount, xls.EndRow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)
            '再使用不可右の罫線'
            xls.SetLine(COL_START_GOUSYA + GousyaCount, TITLE_ROW, COL_START_GOUSYA + GousyaCount, xls.EndRow, XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)

        End Sub

        'シートの行の高さと列の幅を設定'
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

        '各列のタグに番号付与'
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

        '各行のタグに新たに番号を付与'
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


