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
    Public Class ExportShisakuBuhinEditBuhinConditionExcel
        Dim insuSuryoEnd As Integer
#Region "メンバ変数"
        Private insucount As Integer
        Private shisakuEventCode As String
        Private shisakuBukaCode As String
        Private shisakuBlockNo As String
        Private newShisakuBlockNoKaiteiNo As String
        Private oldShisakuBlockNoKaiteiNo As String
        Private newInsuCount As Integer
        Private Dao As IShisakuBuhinEditDao
        Private rowCount As Integer
        Private instlHyoujiJunList As List(Of Integer)

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
            insuSuryoEnd = 12
        End Sub

        ''' <summary>
        ''' EXCEL出力
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <param name="tanTouSya"></param>
        ''' <param name="tel"></param>
        ''' <param name="shisakuKaihatsuFugo"></param>
        ''' <param name="shisakuEventName"></param>
        ''' <param name="fileName"></param>
        ''' <param name="isBase"></param>
        ''' <remarks></remarks>
        Public Sub Excute(ByVal xls As ShisakuExcel, _
                          ByVal tanTouSya As String, _
                          ByVal tel As String, _
                          ByVal shisakuKaihatsuFugo As String, _
                          ByVal shisakuEventName As String, _
                          ByVal fileName As String, _
                          Optional ByVal isBase As Boolean = False)

            Dao = New ShisakuBuhinEditDaoImpl()


            xls.SetActiveSheet(2)

            SetColumnNo()

            instlHyoujiJunList = New List(Of Integer)

            Me.setSheet1HeadPart1(xls, shisakuEventCode, shisakuBukaCode, shisakuBlockNo, newShisakuBlockNoKaiteiNo, tanTouSya, tel, shisakuKaihatsuFugo, shisakuEventName)
            Dim instlList As New List(Of TShisakuSekkeiBlockInstlVo)
            Dim oldInstlList As New List(Of TShisakuSekkeiBlockInstlVo)
            Dim kouseiList As New List(Of TShisakuBuhinEditVoHelper)
            Dim oldKoseiList As New List(Of TShisakuBuhinEditVoHelper)

            instlList = GetInsuSuryoList(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, newShisakuBlockNoKaiteiNo)
            kouseiList = GetKouseiList(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, newShisakuBlockNoKaiteiNo)

            If isBase Then
                oldInstlList = GetInsuSuryoList(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, oldShisakuBlockNoKaiteiNo, isBase)
                oldKoseiList = GetKouseiList(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, oldShisakuBlockNoKaiteiNo, isBase)
            Else
                oldInstlList = GetInsuSuryoList(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, oldShisakuBlockNoKaiteiNo)
                oldKoseiList = GetKouseiList(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, oldShisakuBlockNoKaiteiNo)
            End If

            'INSTLが追加OR削除の場合の色用'
            rowCount = kouseiList.Count + oldKoseiList.Count

            Me.SetTitle(xls, instlList, oldInstlList)
            'セル単位での比較になるので'
            Me.SetBodyCondition(xls, kouseiList, oldKoseiList, instlList, oldInstlList)

            setRowCol(xls)

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

        End Sub

        ''' <summary>
        ''' ヘッダー部分を設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <remarks></remarks>
        Private Sub SetHeader(ByVal xls As ShisakuExcel, ByVal insuSuryoList As List(Of InsuSuryoVoHelpler))

        End Sub

        ''' <summary>
        ''' ヘッダー部分を設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <remarks></remarks>
        Public Sub setSheet1HeadPart1(ByVal xls As ShisakuExcel, _
                                       ByVal shisakushisakuEventCode As String, _
                                       ByVal shisakuBukaCode As String, _
                                       ByVal shisakuBlockNo As String, _
                                       ByVal newShisakuBlockNoKaiteiNo As String, _
                                       ByVal tanTouSya As String, _
                                       ByVal tel As String, _
                                       ByVal shisakuKaihatsuFugo As String, _
                                       ByVal shisakuEventName As String)

            xls.SetValue(1, 1, shisakuKaihatsuFugo + " " + shisakuEventName)
            '担当設計課を取得する'
            Dim tantoImpl As New ShisakuBuhinEditBlock.Dao.KaRyakuNameDaoImpl
            Dim TantoName As New Rhac1560Vo

            'TantoName = tantoImpl.GetKa_Ryaku_Name(shisakuBukaCode)

            '担当設計が無い場合、コードを課略名称へ設定する。
            If Not StringUtil.IsEmpty(tantoImpl.GetKa_Ryaku_Name(shisakuBukaCode)) Then
                TantoName = tantoImpl.GetKa_Ryaku_Name(shisakuBukaCode)
            Else
                TantoName.KaRyakuName = shisakuBukaCode
            End If

            '担当者名を取得する'
            Dim ExcelImpl As EditBlock2ExcelDao = New EditBlock2ExcelDaoImpl
            Dim UserName As String

            UserName = ExcelImpl.FindByShainName(tanTouSya)

            xls.SetValue(6, 1, "担当設計：" + TantoName.KaRyakuName + " 担当者：" + tanTouSya + " Tel: " + tel)
            xls.SetValue(1, 2, "ブロックNo：" + shisakuBlockNo)

        End Sub

        ''' <summary>
        ''' タイトルを設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <param name="instlList">INSTLリスト</param>
        ''' <param name="oldInstlList">改訂前INSTLリスト</param>
        ''' <remarks></remarks>
        Private Sub SetTitle(ByVal xls As ShisakuExcel, ByVal instlList As List(Of TShisakuSekkeiBlockInstlVo), ByRef oldInstlList As List(Of TShisakuSekkeiBlockInstlVo))
            SetTitleStr(xls, instlList, oldInstlList)
        End Sub

        ''' <summary>
        ''' タイトルの文字を設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <param name="instlList">最新のINSTL</param>
        ''' <param name="oldInstlList">前改訂INSTL</param>
        ''' <remarks></remarks>
        Private Sub SetTitleStr(ByVal xls As ShisakuExcel, ByVal instlList As List(Of TShisakuSekkeiBlockInstlVo), ByRef oldInstlList As List(Of TShisakuSekkeiBlockInstlVo))

            ''↓↓2014/09/18 酒井 ADD BEGIN
            '該当イベント取得
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(shisakuEventCode)
            ''↑↑2014/09/18 酒井 ADD END

            Dim alphabetList As New List(Of LabelValueVo)

            alphabetList = GetLabelValues_Column()

            Dim EImpl As EditBlock2ExcelDao = New EditBlock2ExcelDaoImpl


            'レベル'
            xls.MergeCells(COL_LEVEL, TITLE_ROW, COL_LEVEL, TITLE_ROW + 3, True)
            xls.SetOrientation(COL_LEVEL, TITLE_ROW, COL_LEVEL, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetAlliment(COL_LEVEL, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_LEVEL, TITLE_ROW, "レベル")
            '国内集計コード'
            xls.MergeCells(COL_SHUKEI_CODE, TITLE_ROW, COL_SHUKEI_CODE, TITLE_ROW + 3, True)
            xls.SetOrientation(COL_SHUKEI_CODE, TITLE_ROW, COL_SHUKEI_CODE, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetAlliment(COL_SHUKEI_CODE, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_SHUKEI_CODE, TITLE_ROW, "国内集計")
            '海外集計コード'
            xls.MergeCells(COL_SIA_SHUKEI_CODE, TITLE_ROW, COL_SIA_SHUKEI_CODE, TITLE_ROW + 3, True)
            xls.SetOrientation(COL_SIA_SHUKEI_CODE, TITLE_ROW, COL_SIA_SHUKEI_CODE, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetAlliment(COL_SIA_SHUKEI_CODE, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_SIA_SHUKEI_CODE, TITLE_ROW, "海外集計")
            '現調区分'
            xls.MergeCells(COL_GENCYO_KBN, TITLE_ROW, COL_GENCYO_KBN, TITLE_ROW + 3, True)
            xls.SetOrientation(COL_GENCYO_KBN, TITLE_ROW, COL_GENCYO_KBN, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetAlliment(COL_GENCYO_KBN, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_GENCYO_KBN, TITLE_ROW, "現調区分")
            '取引先コード'
            xls.MergeCells(COL_TORIHIKISAKI_CODE, TITLE_ROW, COL_TORIHIKISAKI_CODE, TITLE_ROW + 3, True)
            xls.SetOrientation(COL_TORIHIKISAKI_CODE, TITLE_ROW, COL_TORIHIKISAKI_CODE, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetAlliment(COL_TORIHIKISAKI_CODE, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_TORIHIKISAKI_CODE, TITLE_ROW, "取引先コード")
            '取引先名称'
            xls.MergeCells(COL_TORIHIKISAKI_NAME, TITLE_ROW, COL_TORIHIKISAKI_NAME, TITLE_ROW + 3, True)
            xls.SetOrientation(COL_TORIHIKISAKI_NAME, TITLE_ROW, COL_TORIHIKISAKI_NAME, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetAlliment(COL_TORIHIKISAKI_NAME, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_TORIHIKISAKI_NAME, TITLE_ROW, "取引先名称")
            '部品番号'
            xls.MergeCells(COL_BUHIN_NO, TITLE_ROW, COL_BUHIN_NO, TITLE_ROW + 3, True)
            xls.SetAlliment(COL_BUHIN_NO, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_BUHIN_NO, TITLE_ROW, "部品番号")
            '部品番号区分'
            xls.MergeCells(COL_BUHIN_NO_KBN, TITLE_ROW, COL_BUHIN_NO_KBN, TITLE_ROW + 3, True)
            xls.SetAlliment(COL_BUHIN_NO_KBN, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_BUHIN_NO_KBN, TITLE_ROW, "区分")
            '部品番号改訂No'
            xls.MergeCells(COL_KAITEI, TITLE_ROW, COL_KAITEI, TITLE_ROW + 3, True)
            xls.SetOrientation(COL_KAITEI, TITLE_ROW, COL_KAITEI, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetAlliment(COL_KAITEI, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_KAITEI, TITLE_ROW, "改訂")
            '枝番'
            xls.MergeCells(COL_EDA_BAN, TITLE_ROW, COL_EDA_BAN, TITLE_ROW + 3, True)
            xls.SetOrientation(COL_EDA_BAN, TITLE_ROW, COL_EDA_BAN, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetAlliment(COL_EDA_BAN, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_EDA_BAN, TITLE_ROW, "枝番")
            '部品名称'
            xls.MergeCells(COL_BUHIN_NAME, TITLE_ROW, COL_BUHIN_NAME, TITLE_ROW + 3, True)
            xls.SetAlliment(COL_BUHIN_NAME, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_BUHIN_NAME, TITLE_ROW, "部品名称")

            Dim hinbanList As New List(Of String)

            '員数の表示'

            Dim i As Integer
            insuSuryoEnd = 0
            Dim oldinsuSuryoEnd = 0

            '最初に実行
            Dim lastInstlHinbanHyoujiJun As Integer = instlList(instlList.Count - 1).InstlHinbanHyoujiJun
            For i = 0 To lastInstlHinbanHyoujiJun
                xls.SetValue(COL_START_INSU + i, 7, alphabetList(i).Label)
                '員数'
                xls.SetOrientation(COL_START_INSU + i, INSTL_TITLE_ROW, COL_START_INSU + i, INSTL_TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                xls.SetOrientation(COL_START_INSU + i, INSTL_KBN_ROW, COL_START_INSU + i, INSTL_KBN_ROW, ShisakuExcel.XlOrientation.xlVertical)
                insuSuryoEnd = lastInstlHinbanHyoujiJun + 1
            Next

            Dim key As New System.Text.StringBuilder

            For i = 0 To instlList.Count - 1
                Dim bTable As String = ""
                Dim instlHinbanHyoujiJun As Integer = instlList(i).InstlHinbanHyoujiJun
                If i = 0 Then
                    ''員数列'
                    ''員数列(A,B,C...)'
                    'xls.SetValue(COL_START_INSU + insuSuryoEnd, 7, alphabetList(insuSuryoEnd).Label)
                    '員数'
                    'xls.SetOrientation(COL_START_INSU + insuSuryoEnd, INSTL_TITLE_ROW, COL_START_INSU + insuSuryoEnd, INSTL_TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                    xls.SetValue(COL_START_INSU + instlHinbanHyoujiJun, INSTL_TITLE_ROW, instlList.Item(i).InstlHinban)
                    'xls.SetOrientation(COL_START_INSU + insuSuryoEnd, INSTL_KBN_ROW, COL_START_INSU + insuSuryoEnd, INSTL_KBN_ROW, ShisakuExcel.XlOrientation.xlVertical)
                    xls.SetValue(COL_START_INSU + instlHinbanHyoujiJun, INSTL_KBN_ROW, instlList.Item(i).InstlHinbanKbn)

                    key.Remove(0, key.Length)
                    key.AppendLine(instlList(i).InstlHinban)
                    key.AppendLine(instlList(i).InstlHinbanKbn)
                    key.AppendLine(instlList(i).InstlDataKbn)
                    If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                        key.AppendLine(instlList(i).BaseInstlFlg)
                    End If

                    hinbanList.Add(key.ToString)

                    If chkInstl(instlList(i).InstlHinban, instlList(i).InstlHinbanKbn, oldInstlList) Then
                        xls.SetBackColor(COL_START_INSU + instlHinbanHyoujiJun, INSTL_TITLE_ROW, COL_START_INSU + instlHinbanHyoujiJun, INSTL_KBN_ROW, &H9FFFFF)
                        instlHyoujiJunList.Add(instlList(i).InstlHinbanHyoujiJun)
                    End If
                    'insuSuryoEnd = insuSuryoEnd + 1
                End If

                'If Not hinbanList.Contains(instlList(i).InstlHinban + instlList(i).InstlHinbanKbn) Then
                ''員数列(A,B,C...)'
                'xls.SetValue(COL_START_INSU + insuSuryoEnd, 7, alphabetList(insuSuryoEnd).Label)
                '員数'
                'xls.SetOrientation(COL_START_INSU + insuSuryoEnd, INSTL_TITLE_ROW, COL_START_INSU + insuSuryoEnd, INSTL_TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                xls.SetValue(COL_START_INSU + instlHinbanHyoujiJun, INSTL_TITLE_ROW, instlList.Item(i).InstlHinban)
                'xls.SetOrientation(COL_START_INSU + insuSuryoEnd, INSTL_KBN_ROW, COL_START_INSU + insuSuryoEnd, INSTL_KBN_ROW, ShisakuExcel.XlOrientation.xlVertical)
                xls.SetValue(COL_START_INSU + instlHinbanHyoujiJun, INSTL_KBN_ROW, instlList.Item(i).InstlHinbanKbn)
                key.Remove(0, key.Length)
                key.AppendLine(instlList(i).InstlHinban)
                key.AppendLine(instlList(i).InstlHinbanKbn)
                key.AppendLine(instlList(i).InstlDataKbn)
                If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                    key.AppendLine(instlList(i).BaseInstlFlg)
                End If
                hinbanList.Add(key.ToString)

                If chkInstl(instlList(i).InstlHinban, instlList(i).InstlHinbanKbn, oldInstlList) Then
                    xls.SetBackColor(COL_START_INSU + instlHinbanHyoujiJun, INSTL_TITLE_ROW, COL_START_INSU + instlHinbanHyoujiJun, INSTL_KBN_ROW, &H9FFFFF)
                    instlHyoujiJunList.Add(instlList(i).InstlHinbanHyoujiJun)
                End If
                'insuSuryoEnd = insuSuryoEnd + 1
                'End If

                ''↓↓2014/09/18 酒井 ADD BEGIN
                '該当イベント取得
                If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                    If instlList(i).BaseInstlFlg = 1 Then
                        xls.SetBackColor(COL_START_INSU + instlHinbanHyoujiJun, INSTL_TITLE_ROW, COL_START_INSU + instlHinbanHyoujiJun, 10000, RGB(176, 215, 237))
                    End If
                End If
                ''↑↑2014/09/18 酒井 ADD END
            Next
            '最新のみの員数列数'
            newInsuCount = insuSuryoEnd

            '旧リストから'
            For i = 0 To oldInstlList.Count - 1
                If oldInstlList.Count <= i Then
                    Exit For
                End If

                key.Remove(0, key.Length)
                key.AppendLine(oldInstlList(i).InstlHinban)
                key.AppendLine(oldInstlList(i).InstlHinbanKbn)
                key.AppendLine(oldInstlList(i).InstlDataKbn)
                If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                    key.AppendLine(oldInstlList(i).BaseInstlFlg)
                End If
                If Not hinbanList.Contains(key.ToString) Then
                    '旧リストにしか存在しないので追加する'
                    '員数列(A,B,C...)'
                    xls.SetValue(COL_START_INSU + insuSuryoEnd, 7, alphabetList(insuSuryoEnd).Label)
                    '員数'
                    xls.SetOrientation(COL_START_INSU + insuSuryoEnd, INSTL_TITLE_ROW, COL_START_INSU + insuSuryoEnd, INSTL_TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                    xls.SetValue(COL_START_INSU + insuSuryoEnd, INSTL_TITLE_ROW, oldInstlList.Item(i).InstlHinban)
                    xls.SetOrientation(COL_START_INSU + insuSuryoEnd, INSTL_KBN_ROW, COL_START_INSU + insuSuryoEnd, INSTL_KBN_ROW, ShisakuExcel.XlOrientation.xlVertical)
                    xls.SetValue(COL_START_INSU + insuSuryoEnd, INSTL_KBN_ROW, oldInstlList.Item(i).InstlHinbanKbn)
                    oldInstlList(i).InstlHinbanHyoujiJun = COL_START_INSU + insuSuryoEnd
                    key.Remove(0, key.Length)
                    key.AppendLine(oldInstlList(i).InstlHinban)
                    key.AppendLine(oldInstlList(i).InstlHinbanKbn)
                    key.AppendLine(oldInstlList(i).InstlDataKbn)
                    If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                        key.AppendLine(oldInstlList(i).BaseInstlFlg)
                    End If
                    hinbanList.Add(key.ToString)
                    xls.SetBackColor(COL_START_INSU + insuSuryoEnd, INSTL_TITLE_ROW, COL_START_INSU + insuSuryoEnd, INSTL_KBN_ROW, &HA0A0A0)
                    insuSuryoEnd = insuSuryoEnd + 1
                Else
                    oldInstlList.Remove(oldInstlList(i))
                    i -= 1
                End If
            Next

            insucount = insuSuryoEnd

            '員数'
            xls.MergeCells(COL_START_INSU, TITLE_ROW, COL_START_INSU + insuSuryoEnd - 1, TITLE_ROW, True)
            xls.SetAlliment(COL_START_INSU, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU, TITLE_ROW, "員数")
            '再使用不可'
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, 9, True)
            xls.SetOrientation(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "再使用不可")
            '2012/01/25 供給セクション
            '供給セクション'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, 9, True)
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "供給セクション")
            '出荷予定日'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, 9, True)
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "出図予定日")

            ''↓↓2014/09/04 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + 6 + insuSuryoEnd, 8, True)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "作り方")
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "製作方法")
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, 9, XlHAlign.xlHAlignCenter)
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "型仕様１")
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, 9, XlHAlign.xlHAlignCenter)
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "型仕様２")
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, 9, XlHAlign.xlHAlignCenter)
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "型仕様３")
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, 9, XlHAlign.xlHAlignCenter)
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "治具")
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, 9, XlHAlign.xlHAlignCenter)
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "納入見通し")
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, 9, XlHAlign.xlHAlignCenter)
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "部品製作規模・概要")
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, 9, XlHAlign.xlHAlignCenter)
            ''↑↑2014/09/04 Ⅰ.2.管理項目追加 酒井 ADD END

            '材質'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd + 3, 8, True)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "材質")
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "規格１")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "規格２")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "規格３")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "メッキ")
            '板厚'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd + 1, 8, True)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "板厚")
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "板厚")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "u")

            ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
            '材料情報'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd + 1, 8, True)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "材料情報")
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "製品長")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "製品幅")

            'データ項目'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd + 3, 8, True)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "データ項目")
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "改訂№")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "エリア名")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "セット名")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 9, "改訂情報")
            ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END

            '試作部品費'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, 9, True)
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "試作部品費" + vbCrLf + "（円）")
            '試作型費'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, 9, True)
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "試作型費" + vbCrLf + "（千円）")
            '部品ノート'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, 9, True)
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "NOTE")
            '備考'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, 9, True)
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "備考")


        End Sub

        ''' <summary>
        ''' シートのBODY部を設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <param name="kouseiList">部品編集情報</param>
        ''' <param name="oldKouseiList">部品編集情報</param>
        ''' <remarks></remarks>
        Private Sub SetBodyCondition(ByVal xls As ShisakuExcel, ByVal kouseiList As List(Of TShisakuBuhinEditVoHelper), ByVal oldKouseiList As List(Of TShisakuBuhinEditVoHelper), ByVal instlList As List(Of TShisakuSekkeiBlockInstlVo), ByVal oldInstlList As List(Of TShisakuSekkeiBlockInstlVo))
            ''↓↓2014/09/18 酒井 ADD BEGIN
            '該当イベント取得
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(shisakuEventCode)
            ''↑↑2014/09/18 酒井 ADD END

            '最新が空なら何もしない？'
            Dim i As Integer
            Dim rowIndex As Integer = 0
            Dim oldrowIndex As Integer = 0
            Dim rowBreak As Boolean = True


            Dim AddFlag As Boolean = False


            Dim newEdit As New TShisakuBuhinEditVoHelper
            Dim oldEdit As New TShisakuBuhinEditVoHelper

            '同一の部品番号の存在フラグ'
            Dim BuhinNoChkFlag As Boolean = False


            '第一段階'
            '部品編集情報のみを比較'

            '最新の部品編集情報'
            Dim newBuhinEditVoList As New List(Of TShisakuBuhinEditVo)
            newBuhinEditVoList = Dao.FindByBuhinEdit(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, newShisakuBlockNoKaiteiNo, False)
            '旧部品編集情報'


            '色を塗る為にデータを書きながらの作業になる'
            If Not (kouseiList Is Nothing) And (kouseiList.Count > 0) Then

                For i = 0 To kouseiList.Count - 1
                    rowBreak = True

                    '部品番号表示順と部品番号が同じなら行インデックスを戻す'
                    If Not i = 0 Then
                        If StringUtil.Equals(kouseiList(i).BuhinNoHyoujiJun, kouseiList(i - 1).BuhinNoHyoujiJun) Then
                            'rowindexは0未満にならない'
                            rowIndex = rowIndex - 1
                            rowBreak = False
                            If rowIndex < 0 Then
                                rowIndex = 0
                            End If
                        End If
                    End If

                    Dim shisakuBuhinEditVo = kouseiList.Item(i)

                    If rowBreak Then
                        '--------------------------------------------------------------------------------------------
                        'レベル’
                        '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                        '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                        If StringUtil.IsEmpty(shisakuBuhinEditVo.Level) Then
                            xls.SetValue(COL_LEVEL, START_ROW + rowIndex, "")
                        Else
                            xls.SetValue(COL_LEVEL, START_ROW + rowIndex, shisakuBuhinEditVo.Level.ToString())
                        End If
                        '--------------------------------------------------------------------------------------------

                        xls.SetValue(COL_SHUKEI_CODE, START_ROW + rowIndex, shisakuBuhinEditVo.ShukeiCode)
                        xls.SetValue(COL_SIA_SHUKEI_CODE, START_ROW + rowIndex, shisakuBuhinEditVo.SiaShukeiCode)
                        xls.SetValue(COL_GENCYO_KBN, START_ROW + rowIndex, shisakuBuhinEditVo.GencyoCkdKbn)
                        xls.SetValue(COL_TORIHIKISAKI_CODE, START_ROW + rowIndex, shisakuBuhinEditVo.MakerCode)
                        xls.SetValue(COL_TORIHIKISAKI_NAME, START_ROW + rowIndex, shisakuBuhinEditVo.MakerName)
                        xls.SetValue(COL_BUHIN_NO, START_ROW + rowIndex, shisakuBuhinEditVo.BuhinNo)
                        xls.SetValue(COL_BUHIN_NO_KBN, START_ROW + rowIndex, shisakuBuhinEditVo.BuhinNoKbn)
                        xls.SetValue(COL_KAITEI, START_ROW + rowIndex, shisakuBuhinEditVo.BuhinNoKaiteiNo)
                        xls.SetValue(COL_EDA_BAN, START_ROW + rowIndex, shisakuBuhinEditVo.EdaBan)
                        xls.SetValue(COL_BUHIN_NAME, START_ROW + rowIndex, shisakuBuhinEditVo.BuhinName)

                        '員数以降'
                        '再使用不可
                        xls.SetValue(COL_START_INSU + insucount, START_ROW + rowIndex, shisakuBuhinEditVo.Saishiyoufuka)

                        '2012/01/23 供給セクション追加
                        '供給セクション'
                        xls.SetValue(COL_START_INSU + insucount + 1, START_ROW + rowIndex, shisakuBuhinEditVo.KyoukuSection)

                        '出図予定日
                        If shisakuBuhinEditVo.ShutuzuYoteiDate = "99999999" Or shisakuBuhinEditVo.ShutuzuYoteiDate = "0" Then
                            xls.SetValue(COL_START_INSU + insucount + 2, START_ROW + rowIndex, "")
                        Else
                            xls.SetValue(COL_START_INSU + insucount + 2, START_ROW + rowIndex, shisakuBuhinEditVo.ShutuzuYoteiDate)
                        End If

                        xls.SetValue(COL_START_INSU + insucount + 3, START_ROW + rowIndex, shisakuBuhinEditVo.TsukurikataSeisaku)
                        xls.SetValue(COL_START_INSU + insucount + 4, START_ROW + rowIndex, shisakuBuhinEditVo.TsukurikataKatashiyou1)
                        xls.SetValue(COL_START_INSU + insucount + 5, START_ROW + rowIndex, shisakuBuhinEditVo.TsukurikataKatashiyou2)
                        xls.SetValue(COL_START_INSU + insucount + 6, START_ROW + rowIndex, shisakuBuhinEditVo.TsukurikataKatashiyou3)
                        xls.SetValue(COL_START_INSU + insucount + 7, START_ROW + rowIndex, shisakuBuhinEditVo.TsukurikataTigu)
                        If StringUtil.IsEmpty(shisakuBuhinEditVo.TsukurikataNounyu) Or shisakuBuhinEditVo.TsukurikataNounyu = "0" Then
                            xls.SetValue(COL_START_INSU + insucount + 8, START_ROW + rowIndex, "")
                        Else
                            xls.SetValue(COL_START_INSU + insucount + 8, START_ROW + rowIndex, shisakuBuhinEditVo.TsukurikataNounyu)
                        End If
                        xls.SetValue(COL_START_INSU + insucount + 9, START_ROW + rowIndex, shisakuBuhinEditVo.TsukurikataKibo)
                        xls.SetValue(COL_START_INSU + insucount + 10, START_ROW + rowIndex, shisakuBuhinEditVo.ZaishituKikaku1)
                        xls.SetValue(COL_START_INSU + insucount + 11, START_ROW + rowIndex, shisakuBuhinEditVo.ZaishituKikaku2)
                        xls.SetValue(COL_START_INSU + insucount + 12, START_ROW + rowIndex, shisakuBuhinEditVo.ZaishituKikaku3)
                        xls.SetValue(COL_START_INSU + insucount + 13, START_ROW + rowIndex, shisakuBuhinEditVo.ZaishituMekki)
                        xls.SetValue(COL_START_INSU + insucount + 14, START_ROW + rowIndex, shisakuBuhinEditVo.ShisakuBankoSuryo)
                        xls.SetValue(COL_START_INSU + insucount + 15, START_ROW + rowIndex, shisakuBuhinEditVo.ShisakuBankoSuryoU)
                        ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
                        xls.SetValue(COL_START_INSU + insucount + 16, START_ROW + rowIndex, shisakuBuhinEditVo.MaterialInfoLength)
                        xls.SetValue(COL_START_INSU + insucount + 17, START_ROW + rowIndex, shisakuBuhinEditVo.MaterialInfoWidth)
                        xls.SetValue(COL_START_INSU + insucount + 18, START_ROW + rowIndex, shisakuBuhinEditVo.DataItemKaiteiNo)
                        xls.SetValue(COL_START_INSU + insucount + 19, START_ROW + rowIndex, shisakuBuhinEditVo.DataItemAreaName)
                        xls.SetValue(COL_START_INSU + insucount + 20, START_ROW + rowIndex, shisakuBuhinEditVo.DataItemSetName)
                        xls.SetValue(COL_START_INSU + insucount + 21, START_ROW + rowIndex, shisakuBuhinEditVo.DataItemKaiteiInfo)
                        ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END
                        ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                        xls.SetValue(COL_START_INSU + insucount + 22, START_ROW + rowIndex, shisakuBuhinEditVo.ShisakuBuhinHi)
                        xls.SetValue(COL_START_INSU + insucount + 23, START_ROW + rowIndex, shisakuBuhinEditVo.ShisakuKataHi)
                        xls.SetValue(COL_START_INSU + insucount + 24, START_ROW + rowIndex, shisakuBuhinEditVo.BuhinNote)
                        xls.SetValue(COL_START_INSU + insucount + 25, START_ROW + rowIndex, shisakuBuhinEditVo.Bikou)
                        ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END
                    End If

                    If kouseiList(i).InsuSuryo < 0 Then
                        xls.SetValue(COL_START_INSU + shisakuBuhinEditVo.InstlHinbanHyoujiJun, START_ROW + rowIndex, "**")
                    Else
                        xls.SetValue(COL_START_INSU + shisakuBuhinEditVo.InstlHinbanHyoujiJun, START_ROW + rowIndex, shisakuBuhinEditVo.InsuSuryo)
                    End If

                    '全件チェックでTrueならそのまま'
                    If AllChk(shisakuBuhinEditVo, oldKouseiList) Then
                    Else
                        '全件チェックで引っかからない場合は比較先を用意する'
                        AllChkColor(xls, shisakuBuhinEditVo, oldKouseiList, rowIndex)
                    End If

                    ''↓↓2014/09/18 酒井 ADD BEGIN
                    '該当イベント取得
                    If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                        If shisakuBuhinEditVo.BaseBuhinFlg = 1 Then
                            ''↓↓2014/12/29 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                            '  xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_START_INSU + insucount + 19, START_ROW + rowIndex, RGB(176, 215, 237))
                            xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_START_INSU + insucount + 25, START_ROW + rowIndex, RGB(176, 215, 237))
                            ''↑↑2014/12/29 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END
                        End If
                    End If
                    ''↑↑2014/09/18 酒井 ADD END

                    rowIndex = rowIndex + 1
                Next
            End If

            For Each instl As Integer In instlHyoujiJunList
                xls.SetBackColor(COL_START_INSU + instl, START_ROW, COL_START_INSU + instl, START_ROW + rowIndex - 1, &H9FFFFF)
            Next

            '旧構成'
            If Not (oldKouseiList Is Nothing) And (oldKouseiList.Count > 0) Then

                For i = 0 To oldKouseiList.Count - 1
                    rowBreak = True

                    '部品番号表示順と部品番号が同じなら行インデックスを戻す'
                    If Not i = 0 Then
                        If StringUtil.Equals(oldKouseiList(i).BuhinNoHyoujiJun, oldKouseiList(i - 1).BuhinNoHyoujiJun) Then
                            rowIndex = rowIndex - 1
                            rowBreak = False
                        End If
                    End If

                    Dim shisakuBuhinEditVo = oldKouseiList.Item(i)

                    If rowBreak Then
                        '--------------------------------------------------------------------------------------------
                        'レベル’
                        '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                        '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                        If StringUtil.IsEmpty(shisakuBuhinEditVo.Level) Then
                            xls.SetValue(COL_LEVEL, START_ROW + rowIndex, "")
                        Else
                            xls.SetValue(COL_LEVEL, START_ROW + rowIndex, shisakuBuhinEditVo.Level.ToString())
                        End If
                        '--------------------------------------------------------------------------------------------
                        xls.SetValue(COL_SHUKEI_CODE, START_ROW + rowIndex, shisakuBuhinEditVo.ShukeiCode)
                        xls.SetValue(COL_SIA_SHUKEI_CODE, START_ROW + rowIndex, shisakuBuhinEditVo.SiaShukeiCode)
                        xls.SetValue(COL_GENCYO_KBN, START_ROW + rowIndex, shisakuBuhinEditVo.GencyoCkdKbn)
                        xls.SetValue(COL_TORIHIKISAKI_CODE, START_ROW + rowIndex, shisakuBuhinEditVo.MakerCode)
                        xls.SetValue(COL_TORIHIKISAKI_NAME, START_ROW + rowIndex, shisakuBuhinEditVo.MakerName)
                        xls.SetValue(COL_BUHIN_NO, START_ROW + rowIndex, shisakuBuhinEditVo.BuhinNo)
                        xls.SetValue(COL_BUHIN_NO_KBN, START_ROW + rowIndex, shisakuBuhinEditVo.BuhinNoKbn)
                        xls.SetValue(COL_KAITEI, START_ROW + rowIndex, shisakuBuhinEditVo.BuhinNoKaiteiNo)
                        xls.SetValue(COL_EDA_BAN, START_ROW + rowIndex, shisakuBuhinEditVo.EdaBan)
                        xls.SetValue(COL_BUHIN_NAME, START_ROW + rowIndex, shisakuBuhinEditVo.BuhinName)

                        '員数以降'
                        '再使用不可
                        xls.SetValue(COL_START_INSU + insucount, START_ROW + rowIndex, shisakuBuhinEditVo.Saishiyoufuka)

                        '2012/01/23 供給セクション追加
                        '供給セクション'
                        xls.SetValue(COL_START_INSU + insucount + 1, START_ROW + rowIndex, shisakuBuhinEditVo.KyoukuSection)

                        '出図予定日
                        If shisakuBuhinEditVo.ShutuzuYoteiDate = "99999999" Or shisakuBuhinEditVo.ShutuzuYoteiDate = "0" Then
                            xls.SetValue(COL_START_INSU + insucount + 2, START_ROW + rowIndex, "")
                        Else
                            xls.SetValue(COL_START_INSU + insucount + 2, START_ROW + rowIndex, shisakuBuhinEditVo.ShutuzuYoteiDate)
                        End If

                        xls.SetValue(COL_START_INSU + insucount + 3, START_ROW + rowIndex, shisakuBuhinEditVo.TsukurikataSeisaku)
                        xls.SetValue(COL_START_INSU + insucount + 4, START_ROW + rowIndex, shisakuBuhinEditVo.TsukurikataKatashiyou1)
                        xls.SetValue(COL_START_INSU + insucount + 5, START_ROW + rowIndex, shisakuBuhinEditVo.TsukurikataKatashiyou2)
                        xls.SetValue(COL_START_INSU + insucount + 6, START_ROW + rowIndex, shisakuBuhinEditVo.TsukurikataKatashiyou3)
                        xls.SetValue(COL_START_INSU + insucount + 7, START_ROW + rowIndex, shisakuBuhinEditVo.TsukurikataTigu)
                        If StringUtil.IsEmpty(shisakuBuhinEditVo.TsukurikataNounyu) Or shisakuBuhinEditVo.TsukurikataNounyu = "0" Then
                            xls.SetValue(COL_START_INSU + insucount + 8, START_ROW + rowIndex, "")
                        Else
                            xls.SetValue(COL_START_INSU + insucount + 8, START_ROW + rowIndex, shisakuBuhinEditVo.TsukurikataNounyu)
                        End If
                        xls.SetValue(COL_START_INSU + insucount + 9, START_ROW + rowIndex, shisakuBuhinEditVo.TsukurikataKibo)
                        xls.SetValue(COL_START_INSU + insucount + 10, START_ROW + rowIndex, shisakuBuhinEditVo.ZaishituKikaku1)
                        xls.SetValue(COL_START_INSU + insucount + 11, START_ROW + rowIndex, shisakuBuhinEditVo.ZaishituKikaku2)
                        xls.SetValue(COL_START_INSU + insucount + 12, START_ROW + rowIndex, shisakuBuhinEditVo.ZaishituKikaku3)
                        xls.SetValue(COL_START_INSU + insucount + 13, START_ROW + rowIndex, shisakuBuhinEditVo.ZaishituMekki)
                        xls.SetValue(COL_START_INSU + insucount + 14, START_ROW + rowIndex, shisakuBuhinEditVo.ShisakuBankoSuryo)
                        xls.SetValue(COL_START_INSU + insucount + 15, START_ROW + rowIndex, shisakuBuhinEditVo.ShisakuBankoSuryoU)

                        ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
                        xls.SetValue(COL_START_INSU + insucount + 16, START_ROW + rowIndex, shisakuBuhinEditVo.MaterialInfoLength)
                        xls.SetValue(COL_START_INSU + insucount + 17, START_ROW + rowIndex, shisakuBuhinEditVo.MaterialInfoWidth)
                        xls.SetValue(COL_START_INSU + insucount + 18, START_ROW + rowIndex, shisakuBuhinEditVo.DataItemKaiteiNo)
                        xls.SetValue(COL_START_INSU + insucount + 19, START_ROW + rowIndex, shisakuBuhinEditVo.DataItemAreaName)
                        xls.SetValue(COL_START_INSU + insucount + 20, START_ROW + rowIndex, shisakuBuhinEditVo.DataItemSetName)
                        xls.SetValue(COL_START_INSU + insucount + 21, START_ROW + rowIndex, shisakuBuhinEditVo.DataItemKaiteiInfo)
                        ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END
                        ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                        xls.SetValue(COL_START_INSU + insucount + 22, START_ROW + rowIndex, shisakuBuhinEditVo.ShisakuBuhinHi)
                        xls.SetValue(COL_START_INSU + insucount + 23, START_ROW + rowIndex, shisakuBuhinEditVo.ShisakuKataHi)
                        xls.SetValue(COL_START_INSU + insucount + 24, START_ROW + rowIndex, shisakuBuhinEditVo.BuhinNote)
                        xls.SetValue(COL_START_INSU + insucount + 25, START_ROW + rowIndex, shisakuBuhinEditVo.Bikou)
                        ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END
                    End If

                    For Each vo As TShisakuSekkeiBlockInstlVo In oldInstlList
                        If EzUtil.IsEqualIfNull(vo.InstlHinban, shisakuBuhinEditVo.InstlHinban) And _
                           EzUtil.IsEqualIfNull(vo.InstlHinbanKbn, shisakuBuhinEditVo.InstlHinbanKbn) And _
                           EzUtil.IsEqualIfNull(vo.InstlDataKbn, shisakuBuhinEditVo.InstlDataKbn) Then
                            If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                                If EzUtil.IsEqualIfNull(vo.BaseInstlFlg, shisakuBuhinEditVo.BaseInstlFlg) Then
                                    If shisakuBuhinEditVo.InsuSuryo < 0 Then
                                        xls.SetValue(vo.InstlHinbanHyoujiJun, START_ROW + rowIndex, "**")
                                    Else
                                        xls.SetValue(vo.InstlHinbanHyoujiJun, START_ROW + rowIndex, shisakuBuhinEditVo.InsuSuryo)
                                    End If
                                End If
                            Else
                                If shisakuBuhinEditVo.InsuSuryo < 0 Then
                                    xls.SetValue(vo.InstlHinbanHyoujiJun, START_ROW + rowIndex, "**")
                                Else
                                    xls.SetValue(vo.InstlHinbanHyoujiJun, START_ROW + rowIndex, shisakuBuhinEditVo.InsuSuryo)
                                End If
                            End If
                        End If
                    Next
                    ''↓↓2014/12/29 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                    'xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_START_INSU + insucount + 19, START_ROW + rowIndex, &HA0A0A0)
                    xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_START_INSU + insucount + 25, START_ROW + rowIndex, &HA0A0A0)
                    ''↑↑2014/12/29 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END
                    AllChkColor(xls, shisakuBuhinEditVo, kouseiList, rowIndex, False)
                    rowIndex = rowIndex + 1
                Next
            End If

            '線の設定'
            xls.SetLine(COL_LEVEL, START_ROW, xls.EndCol, START_ROW, XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous)
            '員数の線の設定'
            '左'
            xls.SetLine(COL_START_INSU, TITLE_ROW, COL_START_INSU, xls.EndRow, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlDouble)
            '再使用不可の右'
            xls.SetLine(COL_START_INSU, TITLE_ROW, COL_START_INSU + insucount, xls.EndRow, XlBordersIndex.xlEdgeRight, XlLineStyle.xlDouble)
            '下'
            xls.SetLine(COL_START_INSU, TITLE_ROW + 1, COL_START_INSU + insucount - 1, TITLE_ROW + 1, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlDouble)
            '上'
            xls.SetLine(COL_START_INSU, TITLE_ROW, COL_START_INSU + insucount, TITLE_ROW, XlBordersIndex.xlEdgeTop, XlLineStyle.xlContinuous)
            '員数の右'
            xls.SetLine(COL_START_INSU, TITLE_ROW, COL_START_INSU + insucount - 1, xls.EndRow, XlBordersIndex.xlEdgeRight, XlLineStyle.xlContinuous)
        End Sub


        ''' <summary>
        ''' 部品編集情報チェック
        ''' </summary>
        ''' <param name="buhinEdit"></param>
        ''' <param name="buhinEditList"></param>
        ''' <remarks></remarks>
        Private Function AllChk(ByVal buhinEdit As TShisakuBuhinEditVoHelper, ByVal buhinEditList As List(Of TShisakuBuhinEditVoHelper)) As Boolean
            For Each vo As TShisakuBuhinEditVoHelper In buhinEditList
                '全部チェック'
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

                        'INSTL品番'
                        If Not StringUtil.Equals(vo.InstlHinban.Trim, buhinEdit.InstlHinban.Trim) Then
                            Continue For
                        End If

                        'INSTL品番区分'
                        If Not StringUtil.Equals(vo.InstlHinbanKbn.Trim, buhinEdit.InstlHinbanKbn.Trim) Then
                            Continue For
                        End If

                        '員数'
                        If vo.InsuSuryo <> buhinEdit.InsuSuryo Then
                            Continue For
                        End If

                        '全件チェックの結果同一の部品が存在する'
                        '旧構成から削除'
                        buhinEditList.Remove(vo)
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
        Private Sub AllChkColor(ByVal xls As ShisakuExcel, ByVal buhinEdit As TShisakuBuhinEditVoHelper, _
                                ByVal buhinEditList As List(Of TShisakuBuhinEditVoHelper), _
                                ByVal rowIndex As Integer, _
                                Optional ByVal isAdd As Boolean = True)
            Dim buhinNoFlag As Boolean = False

            Dim flag As Boolean = False

            For Each vo As TShisakuBuhinEditVoHelper In buhinEditList
                flag = False
                '全部チェック'
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
                        For Each i As Integer In instlHyoujiJunList
                            If i = buhinEdit.InstlHinbanHyoujiJun Then
                                '追加扱いなので'
                                flag = True
                            End If
                        Next

                        If flag Then
                            Continue For
                        End If

                        '変更確定のため変更色にする'
                        If Not isAdd Then
                            ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                            'xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_START_INSU + insucount + 12, START_ROW + rowIndex, &HCCCCCC)
                            xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_START_INSU + insucount + 25, START_ROW + rowIndex, &HCCCCCC)
                            ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END
                            Continue For
                        End If

                        'INSTL品番'
                        If Not StringUtil.Equals(vo.InstlHinban.Trim, buhinEdit.InstlHinban.Trim) Then
                            If isAdd Then
                                xls.SetBackColor(COL_BUHIN_NO, START_ROW + rowIndex, COL_BUHIN_NO, START_ROW + rowIndex, &H9FFFFF)
                                If Not StringUtil.Equals(vo.InstlHinbanKbn.Trim, buhinEdit.InstlHinbanKbn.Trim) Then
                                    xls.SetBackColor(COL_BUHIN_NO_KBN, START_ROW + rowIndex, COL_BUHIN_NO_KBN, START_ROW + rowIndex, &H9FFFFF)
                                    Continue For
                                End If
                            Else
                                xls.SetBackColor(COL_BUHIN_NO, START_ROW + rowIndex, COL_BUHIN_NO, START_ROW + rowIndex, &HA0A0A0)
                                If Not StringUtil.Equals(vo.InstlHinbanKbn.Trim, buhinEdit.InstlHinbanKbn.Trim) Then
                                    xls.SetBackColor(COL_BUHIN_NO_KBN, START_ROW + rowIndex, COL_BUHIN_NO_KBN, START_ROW + rowIndex, &HA0A0A0)
                                    Continue For
                                End If
                            End If

                        End If

                        'INSTL品番区分'
                        If Not StringUtil.Equals(vo.InstlHinbanKbn.Trim, buhinEdit.InstlHinbanKbn.Trim) Then
                            If Not StringUtil.IsEmpty(buhinEdit.InstlHinbanKbn.Trim) Then
                                xls.SetBackColor(COL_BUHIN_NO, START_ROW + rowIndex, COL_BUHIN_NO, START_ROW + rowIndex, &H9FFFFF)
                                xls.SetBackColor(COL_BUHIN_NO_KBN, START_ROW + rowIndex, COL_BUHIN_NO_KBN, START_ROW + rowIndex, &H9FFFFF)
                                Continue For
                            Else
                                xls.SetBackColor(COL_BUHIN_NO, START_ROW + rowIndex, COL_BUHIN_NO, START_ROW + rowIndex, &HA0A0A0)
                                xls.SetBackColor(COL_BUHIN_NO_KBN, START_ROW + rowIndex, COL_BUHIN_NO_KBN, START_ROW + rowIndex, &HA0A0A0)
                                Continue For
                            End If
                        End If
                        '国内集計'
                        If vo.ShukeiCode Is Nothing Then
                            vo.ShukeiCode = ""
                        End If
                        If buhinEdit.ShukeiCode Is Nothing Then
                            buhinEdit.ShukeiCode = ""
                        End If

                        If Not StringUtil.Equals(vo.ShukeiCode.Trim, buhinEdit.ShukeiCode.Trim) Then
                            If isAdd Then
                                xls.SetBackColor(COL_SHUKEI_CODE, START_ROW + rowIndex, COL_SHUKEI_CODE, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_SHUKEI_CODE, START_ROW + rowIndex, COL_SHUKEI_CODE, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        '海外集計'
                        If vo.SiaShukeiCode Is Nothing Then
                            vo.SiaShukeiCode = ""
                        End If
                        If buhinEdit.SiaShukeiCode Is Nothing Then
                            buhinEdit.SiaShukeiCode = ""
                        End If
                        If Not StringUtil.Equals(vo.SiaShukeiCode.Trim, buhinEdit.SiaShukeiCode.Trim) Then
                            If isAdd Then
                                xls.SetBackColor(COL_SIA_SHUKEI_CODE, START_ROW + rowIndex, COL_SIA_SHUKEI_CODE, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_SIA_SHUKEI_CODE, START_ROW + rowIndex, COL_SIA_SHUKEI_CODE, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If

                        '現調区分'
                        If vo.GencyoCkdKbn Is Nothing Then
                            vo.GencyoCkdKbn = ""
                        End If
                        If buhinEdit.GencyoCkdKbn Is Nothing Then
                            buhinEdit.GencyoCkdKbn = ""
                        End If
                        If Not StringUtil.Equals(vo.GencyoCkdKbn.Trim, buhinEdit.GencyoCkdKbn.Trim) Then
                            If isAdd Then
                                xls.SetBackColor(COL_GENCYO_KBN, START_ROW + rowIndex, COL_GENCYO_KBN, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
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
                                '変更'
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
                                '変更'
                                xls.SetBackColor(COL_TORIHIKISAKI_NAME, START_ROW + rowIndex, COL_TORIHIKISAKI_NAME, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        '部品番号区分'
                        If vo.BuhinNoKbn Is Nothing Then
                            vo.BuhinNoKbn = ""
                        End If
                        If buhinEdit.BuhinNoKbn Is Nothing Then
                            buhinEdit.BuhinNoKbn = ""
                        End If
                        If Not StringUtil.Equals(vo.BuhinNoKbn.Trim, buhinEdit.BuhinNoKbn.Trim) Then
                            If isAdd Then
                                xls.SetBackColor(COL_BUHIN_NO_KBN, START_ROW + rowIndex, COL_BUHIN_NO_KBN, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_BUHIN_NO_KBN, START_ROW + rowIndex, COL_BUHIN_NO_KBN, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        '部品番号改訂No'
                        If vo.BuhinNoKaiteiNo Is Nothing Then
                            vo.BuhinNoKaiteiNo = ""
                        End If
                        If buhinEdit.BuhinNoKaiteiNo Is Nothing Then
                            buhinEdit.BuhinNoKaiteiNo = ""
                        End If
                        If Not StringUtil.Equals(vo.BuhinNoKaiteiNo.Trim, buhinEdit.BuhinNoKaiteiNo.Trim) Then
                            If isAdd Then
                                xls.SetBackColor(COL_KAITEI, START_ROW + rowIndex, COL_KAITEI, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_KAITEI, START_ROW + rowIndex, COL_KAITEI, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        '枝番'
                        If vo.EdaBan Is Nothing Then
                            vo.EdaBan = ""
                        End If
                        If buhinEdit.EdaBan Is Nothing Then
                            buhinEdit.EdaBan = ""
                        End If
                        If Not StringUtil.Equals(vo.EdaBan.Trim, buhinEdit.EdaBan.Trim) Then
                            If isAdd Then
                                xls.SetBackColor(COL_EDA_BAN, START_ROW + rowIndex, COL_EDA_BAN, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_EDA_BAN, START_ROW + rowIndex, COL_EDA_BAN, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        '部品名称'
                        If vo.BuhinName Is Nothing Then
                            vo.BuhinName = ""
                        End If
                        If buhinEdit.BuhinName Is Nothing Then
                            buhinEdit.BuhinName = ""
                        End If
                        If Not StringUtil.Equals(vo.BuhinName.Trim, buhinEdit.BuhinName.Trim) Then
                            If isAdd Then
                                xls.SetBackColor(COL_BUHIN_NAME, START_ROW + rowIndex, COL_BUHIN_NAME, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_BUHIN_NAME, START_ROW + rowIndex, COL_BUHIN_NAME, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        '再使用不可'
                        If vo.Saishiyoufuka Is Nothing Then
                            vo.Saishiyoufuka = ""
                        End If
                        If buhinEdit.Saishiyoufuka Is Nothing Then
                            buhinEdit.Saishiyoufuka = ""
                        End If
                        If Not StringUtil.Equals(vo.Saishiyoufuka.Trim, buhinEdit.Saishiyoufuka.Trim) Then
                            If isAdd Then
                                xls.SetBackColor(COL_START_INSU + insucount, START_ROW + rowIndex, COL_START_INSU + insucount, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_START_INSU + insucount, START_ROW + rowIndex, COL_START_INSU + insucount, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_INSU + insucount + 1, START_ROW + rowIndex, COL_START_INSU + insucount + 1, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_START_INSU + insucount + 1, START_ROW + rowIndex, COL_START_INSU + insucount + 1, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_INSU + insucount + 2, START_ROW + rowIndex, COL_START_INSU + insucount + 2, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_START_INSU + insucount + 2, START_ROW + rowIndex, COL_START_INSU + insucount + 2, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_INSU + insucount + 3, START_ROW + rowIndex, COL_START_INSU + insucount + 3, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_INSU + insucount + 3, START_ROW + rowIndex, COL_START_INSU + insucount + 3, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_INSU + insucount + 4, START_ROW + rowIndex, COL_START_INSU + insucount + 4, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_INSU + insucount + 4, START_ROW + rowIndex, COL_START_INSU + insucount + 4, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_INSU + insucount + 5, START_ROW + rowIndex, COL_START_INSU + insucount + 5, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_INSU + insucount + 5, START_ROW + rowIndex, COL_START_INSU + insucount + 5, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_INSU + insucount + 6, START_ROW + rowIndex, COL_START_INSU + insucount + 6, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_INSU + insucount + 6, START_ROW + rowIndex, COL_START_INSU + insucount + 6, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_INSU + insucount + 7, START_ROW + rowIndex, COL_START_INSU + insucount + 7, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_INSU + insucount + 7, START_ROW + rowIndex, COL_START_INSU + insucount + 7, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_INSU + insucount + 8, START_ROW + rowIndex, COL_START_INSU + insucount + 8, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_INSU + insucount + 8, START_ROW + rowIndex, COL_START_INSU + insucount + 8, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_INSU + insucount + 9, START_ROW + rowIndex, COL_START_INSU + insucount + 9, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                xls.SetBackColor(COL_START_INSU + insucount + 9, START_ROW + rowIndex, COL_START_INSU + insucount + 9, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If

                        ''↑↑2014/12/29 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END
                        ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                        '材質規格1'
                        If vo.ZaishituKikaku1 Is Nothing Then
                            vo.ZaishituKikaku1 = ""
                        End If
                        If buhinEdit.ZaishituKikaku1 Is Nothing Then
                            buhinEdit.ZaishituKikaku1 = ""
                        End If

                        If Not StringUtil.Equals(vo.ZaishituKikaku1.Trim, buhinEdit.ZaishituKikaku1.Trim) Then
                            If isAdd Then
                                'xls.SetBackColor(COL_START_INSU + insucount + 3, START_ROW + rowIndex, COL_START_INSU + insucount + 3, START_ROW + rowIndex, &H9FFFFF)
                                xls.SetBackColor(COL_START_INSU + insucount + 10, START_ROW + rowIndex, COL_START_INSU + insucount + 10, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                'xls.SetBackColor(COL_START_INSU + insucount + 3, START_ROW + rowIndex, COL_START_INSU + insucount + 3, START_ROW + rowIndex, &HF0F0F0)
                                xls.SetBackColor(COL_START_INSU + insucount + 10, START_ROW + rowIndex, COL_START_INSU + insucount + 10, START_ROW + rowIndex, &HF0F0F0)
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
                                'xls.SetBackColor(COL_START_INSU + insucount + 4, START_ROW + rowIndex, COL_START_INSU + insucount + 4, START_ROW + rowIndex, &H9FFFFF)
                                xls.SetBackColor(COL_START_INSU + insucount + 11, START_ROW + rowIndex, COL_START_INSU + insucount + 11, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                'xls.SetBackColor(COL_START_INSU + insucount + 4, START_ROW + rowIndex, COL_START_INSU + insucount + 4, START_ROW + rowIndex, &HF0F0F0)
                                xls.SetBackColor(COL_START_INSU + insucount + 11, START_ROW + rowIndex, COL_START_INSU + insucount + 11, START_ROW + rowIndex, &HF0F0F0)

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
                                'xls.SetBackColor(COL_START_INSU + insucount + 5, START_ROW + rowIndex, COL_START_INSU + insucount + 5, START_ROW + rowIndex, &H9FFFFF)
                                xls.SetBackColor(COL_START_INSU + insucount + 12, START_ROW + rowIndex, COL_START_INSU + insucount + 12, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                'xls.SetBackColor(COL_START_INSU + insucount + 5, START_ROW + rowIndex, COL_START_INSU + insucount + 5, START_ROW + rowIndex, &HF0F0F0)
                                xls.SetBackColor(COL_START_INSU + insucount + 12, START_ROW + rowIndex, COL_START_INSU + insucount + 12, START_ROW + rowIndex, &HF0F0F0)
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
                                'xls.SetBackColor(COL_START_INSU + insucount + 6, START_ROW + rowIndex, COL_START_INSU + insucount + 6, START_ROW + rowIndex, &H9FFFFF)
                                xls.SetBackColor(COL_START_INSU + insucount + 13, START_ROW + rowIndex, COL_START_INSU + insucount + 13, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                'xls.SetBackColor(COL_START_INSU + insucount + 6, START_ROW + rowIndex, COL_START_INSU + insucount + 6, START_ROW + rowIndex, &HF0F0F0)
                                xls.SetBackColor(COL_START_INSU + insucount + 13, START_ROW + rowIndex, COL_START_INSU + insucount + 13, START_ROW + rowIndex, &HF0F0F0)
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
                                'xls.SetBackColor(COL_START_INSU + insucount + 7, START_ROW + rowIndex, COL_START_INSU + insucount + 7, START_ROW + rowIndex, &H9FFFFF)
                                xls.SetBackColor(COL_START_INSU + insucount + 14, START_ROW + rowIndex, COL_START_INSU + insucount + 14, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                'xls.SetBackColor(COL_START_INSU + insucount + 7, START_ROW + rowIndex, COL_START_INSU + insucount + 7, START_ROW + rowIndex, &HF0F0F0)
                                xls.SetBackColor(COL_START_INSU + insucount + 14, START_ROW + rowIndex, COL_START_INSU + insucount + 14, START_ROW + rowIndex, &HF0F0F0)
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
                                'xls.SetBackColor(COL_START_INSU + insucount + 8, START_ROW + rowIndex, COL_START_INSU + insucount + 8, START_ROW + rowIndex, &H9FFFFF)
                                xls.SetBackColor(COL_START_INSU + insucount + 15, START_ROW + rowIndex, COL_START_INSU + insucount + 15, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                'xls.SetBackColor(COL_START_INSU + insucount + 8, START_ROW + rowIndex, COL_START_INSU + insucount + 8, START_ROW + rowIndex, &HF0F0F0)
                                xls.SetBackColor(COL_START_INSU + insucount + 15, START_ROW + rowIndex, COL_START_INSU + insucount + 15, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END
                        ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
                        '製品長
                        If Not StringUtil.Equals(vo.MaterialInfoLength, buhinEdit.MaterialInfoLength) Then
                            If isAdd Then
                                xls.SetBackColor(COL_START_INSU + insucount + 16, START_ROW + rowIndex, COL_START_INSU + insucount + 16, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_START_INSU + insucount + 16, START_ROW + rowIndex, COL_START_INSU + insucount + 16, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        '製品幅
                        If Not StringUtil.Equals(vo.MaterialInfoWidth, buhinEdit.MaterialInfoWidth) Then
                            If isAdd Then
                                xls.SetBackColor(COL_START_INSU + insucount + 17, START_ROW + rowIndex, COL_START_INSU + insucount + 17, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_START_INSU + insucount + 17, START_ROW + rowIndex, COL_START_INSU + insucount + 17, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_INSU + insucount + 18, START_ROW + rowIndex, COL_START_INSU + insucount + 18, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_START_INSU + insucount + 18, START_ROW + rowIndex, COL_START_INSU + insucount + 18, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_INSU + insucount + 19, START_ROW + rowIndex, COL_START_INSU + insucount + 19, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_START_INSU + insucount + 19, START_ROW + rowIndex, COL_START_INSU + insucount + 19, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_INSU + insucount + 20, START_ROW + rowIndex, COL_START_INSU + insucount + 20, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_START_INSU + insucount + 20, START_ROW + rowIndex, COL_START_INSU + insucount + 20, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_INSU + insucount + 21, START_ROW + rowIndex, COL_START_INSU + insucount + 21, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_START_INSU + insucount + 21, START_ROW + rowIndex, COL_START_INSU + insucount + 21, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END
                        ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                        '試作部品費'
                        If vo.ShisakuBuhinHi <> buhinEdit.ShisakuBuhinHi Then
                            If isAdd Then
                                xls.SetBackColor(COL_START_INSU + insucount + 22, START_ROW + rowIndex, COL_START_INSU + insucount + 22, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_START_INSU + insucount + 22, START_ROW + rowIndex, COL_START_INSU + insucount + 22, START_ROW + rowIndex, &HF0F0F0)
                            End If
                        End If
                        '試作型費'
                        If vo.ShisakuKataHi <> buhinEdit.ShisakuKataHi Then
                            If isAdd Then
                                xls.SetBackColor(COL_START_INSU + insucount + 23, START_ROW + rowIndex, COL_START_INSU + insucount + 23, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_START_INSU + insucount + 23, START_ROW + rowIndex, COL_START_INSU + insucount + 23, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_INSU + insucount + 24, START_ROW + rowIndex, COL_START_INSU + insucount + 24, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_START_INSU + insucount + 24, START_ROW + rowIndex, COL_START_INSU + insucount + 24, START_ROW + rowIndex, &HF0F0F0)
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
                                xls.SetBackColor(COL_START_INSU + insucount + 25, START_ROW + rowIndex, COL_START_INSU + insucount + 25, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_START_INSU + insucount + 25, START_ROW + rowIndex, COL_START_INSU + insucount + 25, START_ROW + rowIndex, &H707070)
                            End If
                        End If
                        ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END

                        '員数'
                        If vo.InsuSuryo <> buhinEdit.InsuSuryo Then
                            If isAdd Then
                                xls.SetBackColor(COL_START_INSU + buhinEdit.InstlHinbanHyoujiJun, START_ROW + rowIndex, COL_START_INSU + buhinEdit.InstlHinbanHyoujiJun, START_ROW + rowIndex, &H9FFFFF)
                            Else
                                '変更'
                                xls.SetBackColor(COL_START_INSU + buhinEdit.InstlHinbanHyoujiJun, START_ROW + rowIndex, COL_START_INSU + buhinEdit.InstlHinbanHyoujiJun, START_ROW + rowIndex, &H707070)
                            End If
                        End If
                        '全件チェックの結果同一の部品が存在する'
                    Else
                        If isAdd Then
                            xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_LEVEL, START_ROW + rowIndex, &H9FFFFF)
                        Else
                            '削除'
                            xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_LEVEL, START_ROW + rowIndex, &HA0A0A0)
                        End If

                    End If
                End If
            Next

            Dim addFlag As Boolean = False

            '存在しない部品なので'
            If Not buhinNoFlag Then

                If isAdd Then
                    '追加'
                    For Each i As Integer In instlHyoujiJunList
                        '存在しない部品かつINSTL品番表示順が異なる'
                        If i <> buhinEdit.InstlHinbanHyoujiJun Then
                            xls.SetBackColor(COL_BUHIN_NO, START_ROW + rowIndex, COL_BUHIN_NO, START_ROW + rowIndex, &H9FFFFF)
                            xls.SetBackColor(COL_START_INSU + buhinEdit.InstlHinbanHyoujiJun, START_ROW + rowIndex, COL_START_INSU + buhinEdit.InstlHinbanHyoujiJun, START_ROW + rowIndex, &H9FFFFF)
                        End If
                    Next
                    'レベル０なら追加のINSTLなので追加確定'
                    '--------------------------------------------------------------------------------------------
                    'レベル’
                    '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                    '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                    Dim buhinEditLevel As Integer
                    If StringUtil.IsEmpty(buhinEdit.Level) Then
                        buhinEditLevel = 0
                    Else
                        buhinEditLevel = buhinEdit.Level
                    End If
                    '--------------------------------------------------------------------------------------------

                    If buhinEditLevel = 0 Then
                        xls.SetBackColor(COL_BUHIN_NO, START_ROW + rowIndex, COL_BUHIN_NO, START_ROW + rowIndex, &H9FFFFF)
                    Else
                        '追加のINSTLが存在しないなら追加確定'
                        If instlHyoujiJunList.Count = 0 Then
                            xls.SetBackColor(COL_BUHIN_NO, START_ROW + rowIndex, COL_BUHIN_NO, START_ROW + rowIndex, &H9FFFFF)
                            xls.SetBackColor(COL_START_INSU + buhinEdit.InstlHinbanHyoujiJun, START_ROW + rowIndex, COL_START_INSU + buhinEdit.InstlHinbanHyoujiJun, START_ROW + rowIndex, &H9FFFFF)
                        End If
                    End If
                Else
                    '削除扱いなので'
                    xls.SetBackColor(COL_BUHIN_NO, START_ROW + rowIndex, COL_BUHIN_NO, START_ROW + rowIndex, &HA0A0A0)
                End If
            End If

        End Sub

        Private Function GetKouseiList(ByVal shisakuEventCode As String, _
                                        ByVal shisakuBukaCode As String, _
                                        ByVal shisakuBlockNo As String, _
                                        ByVal ShisakuBlockNoKaiteiNo As String, _
                                        Optional ByVal isBase As Boolean = False) As List(Of TShisakuBuhinEditVoHelper)
            Dim kouseiList = New List(Of TShisakuBuhinEditVoHelper)
            Dim shisakuBuhinEditDao As IShisakuBuhinEditDao = New ShisakuBuhinEditDaoImpl()
            If isBase Then
                'ベース情報'
                kouseiList = shisakuBuhinEditDao.FindByBuhinEditAndInstlBase(shisakuEventCode, shisakuBukaCode, shisakuBlockNo)
            Else
                '改訂No情報'
                kouseiList = shisakuBuhinEditDao.FindByBuhinEditAndInstl(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, ShisakuBlockNoKaiteiNo)
            End If
            Return kouseiList
        End Function

        ''' <summary>
        ''' INSTLリストを取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="ShisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <param name="isBase"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetInsuSuryoList(ByVal shisakuEventCode As String, _
                                          ByVal shisakuBukaCode As String, _
                                          ByVal shisakuBlockNo As String, _
                                          ByVal ShisakuBlockNoKaiteiNo As String, _
                                              Optional ByVal isBase As Boolean = False) As List(Of TShisakuSekkeiBlockInstlVo)
            Dim insuSuryoList = New List(Of TShisakuSekkeiBlockInstlVo)

            Dim shisakuBuhinEditDao As IShisakuBuhinEditDao = New ShisakuBuhinEditDaoImpl()

            If isBase Then
                insuSuryoList = shisakuBuhinEditDao.FindByBuhinEditInstlBase(shisakuEventCode, shisakuBukaCode, shisakuBlockNo)
            Else
                insuSuryoList = shisakuBuhinEditDao.FindByBuhinEditInstl(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, ShisakuBlockNoKaiteiNo)
            End If

            Return insuSuryoList
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
            COL_START_INSU = EzUtil.Increment(col)
        End Sub

        '行の高さと列の幅を設定'
        Private Sub setRowCol(ByVal xls As ShisakuExcel)
            'INSTL品番行'
            xls.SetRowHeight(INSU_ROW, INSU_ROW, 167)
            'INSTL品番区分行'
            xls.SetRowHeight(9, 9, 70)
            '全列自動設定'
            xls.AutoFitCol(COL_LEVEL, xls.EndCol())
            xls.SetColWidth(COL_LEVEL, COL_LEVEL, 3)
            xls.SetColWidth(COL_TORIHIKISAKI_NAME, COL_TORIHIKISAKI_NAME, 12)

        End Sub

        '部品と品番が紐付いているか確認'
        Private Function CheckInsuSuryo(ByVal BuhinEdit As TShisakuBuhinEditVoHelper, _
                                        ByVal BuhinInstl As InsuSuryoVoHelpler) As Boolean
            If Not StringUtil.Equals(BuhinEdit.ShisakuBukaCode, BuhinInstl.ShisakuBukaCode) Then
                Return False
            End If
            If Not StringUtil.Equals(BuhinEdit.ShisakuBlockNo, BuhinInstl.ShisakuBlockNo) Then
                Return False
            End If

            Return True
        End Function

        '重複するデータを削除する'

        Private Function CheckByKousei(ByVal KouseiList As List(Of TShisakuBuhinEditVoHelper)) As List(Of TShisakuBuhinEditVoHelper)
            Dim newKoseiList As New List(Of TShisakuBuhinEditVoHelper)
            If Not KouseiList Is Nothing Then
                newKoseiList.Add(KouseiList(0))
            Else
                Return Nothing
            End If

            For Each vo As TShisakuBuhinEditVoHelper In KouseiList

            Next

            For index As Integer = 0 To KouseiList.Count - 1

            Next

            Return newKoseiList
        End Function

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
        ''' 追加されたINSTLかチェックする
        ''' </summary>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <param name="instlList">INSTLリスト</param>
        ''' <returns>変更なしならtrue</returns>
        ''' <remarks></remarks>
        Private Function chkInstl(ByVal instlHinban As String, _
                                  ByVal instlHinbanKbn As String, _
                                  ByVal instlList As List(Of TShisakuSekkeiBlockInstlVo)) As Boolean

            For Each vo As TShisakuSekkeiBlockInstlVo In instlList
                If StringUtil.Equals(vo.InstlHinban, instlHinban) Then
                    If StringUtil.Equals(vo.InstlHinbanKbn, instlHinbanKbn) Then
                        Return False
                    End If
                End If
            Next
            Return True
        End Function




#Region "各列のタグ名称"
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
        ''員数の開始列
        Private COL_START_INSU As Integer



#End Region

#Region "各行のタグ名称"

        'タイトル行'
        Private TITLE_ROW As Integer = 6
        '員数行'
        Private INSU_ROW As Integer = 8
        'データ書き込み開始行'
        Private START_ROW As Integer = 10
        'INSTLタイトル行'
        Private INSTL_TITLE_ROW As Integer = 8
        'INSTL区分行'
        Private INSTL_KBN_ROW As Integer = 9

#End Region


    End Class
End Namespace


