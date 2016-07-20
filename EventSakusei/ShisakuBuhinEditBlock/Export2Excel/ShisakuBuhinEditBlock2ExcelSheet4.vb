

Imports ShisakuCommon
Imports EBom.Common
Imports EBom.Excel
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports ShisakuCommon.Util.LabelValue

Namespace ShisakuBuhinEditBlock.Export2Excel
    Public Class ShisakuBuhinEditBlock2ExcelSheet4
        Dim insuSuryoEnd As Integer
        Private insucount As Integer
        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(7)) (TES)張 ADD BEGIN
        Private strGetValue As String() = {"10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25"}
        Private strSetValue As String() = {"MA", "MB", "MC", "MD", "ME", "MF", "MG", "MH", "MI", "MJ", "MK", "ML", "MM", "MN", "MO", "MP"}
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(7)) (TES)張 ADD END


        Public Sub New()
            insuSuryoEnd = 12
        End Sub

        Public Sub Excute(ByVal xls As ShisakuExcel, _
                          ByVal eventCode As String, _
                          ByVal seKeka As String, _
                          ByVal blockNo As String, _
                          ByVal blockKaiteNo As String, _
                          ByVal tanTouSya As String, _
                          ByVal tel As String, _
                          ByVal shisakuKaihatsuFugo As String, _
                          ByVal shisakuEventName As String)

            xls.AddSheet()

            xls.SetActiveSheet(4)

            SetColumnNo()

            Me.setSheet1HeadPart1(xls, eventCode, seKeka, blockNo, blockKaiteNo, tanTouSya, tel, shisakuKaihatsuFugo, shisakuEventName)
            Dim instlList As New List(Of TShisakuSekkeiBlockInstlVo)
            instlList = GetInsuSuryoList(eventCode, seKeka, blockNo, blockKaiteNo)

            Dim kouseiList = GetKouseiList(eventCode, seKeka, blockNo, blockKaiteNo)
            Me.SetTitle(xls, instlList)
            Me.SetBody(xls, kouseiList)

            setRowCol(xls)

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
                                       ByVal eventCode As String, _
                                       ByVal seKeka As String, _
                                       ByVal blockNo As String, _
                                       ByVal blockKaiteNo As String, _
                                       ByVal tanTouSya As String, _
                                       ByVal tel As String, _
                                       ByVal shisakuKaihatsuFugo As String, _
                                       ByVal shisakuEventName As String)

            xls.SetValue(1, 1, shisakuKaihatsuFugo + " " + shisakuEventName)
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

            xls.SetValue(6, 1, "担当設計：" + TantoName.KaRyakuName + " 担当者：" + tanTouSya + " Tel: " + tel)
            xls.SetValue(1, 2, "ブロックNo：" + blockNo)
        End Sub

        ''' <summary>
        ''' タイトルを設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <param name="instlList">INSTLリスト</param>
        ''' <remarks></remarks>
        Private Sub SetTitle(ByVal xls As ShisakuExcel, ByVal instlList As List(Of TShisakuSekkeiBlockInstlVo))
            SetTitleStr(xls, instlList)
        End Sub

        ''' <summary>
        ''' タイトルの文字を設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <remarks></remarks>
        Private Sub SetTitleStr(ByVal xls As ShisakuExcel, ByVal instlList As List(Of TShisakuSekkeiBlockInstlVo))

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

            For i = 0 To instlList.Count - 1

                Dim bTable As String = ""

                If i = 0 Then
                    If StringUtil.IsEmpty(instlList(i).InstlHinbanKbn) Then
                        bTable = EImpl.FindByBuhinTable(instlList(i).InstlHinban)
                        '元のテーブルはどこ？'
                        xls.SetValue(COL_START_INSU + insuSuryoEnd, 3, bTable)
                    End If

                    '員数列'
                    xls.SetValue(COL_START_INSU + insuSuryoEnd, 5, alphabetList(insuSuryoEnd).Label)
                    ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(7)) (TES)張 ADD BEGIN
                    xls.SetOrientation(COL_START_INSU + insuSuryoEnd, 6, COL_START_INSU + insuSuryoEnd, 6, ShisakuExcel.XlOrientation.xlVertical)
                    'xls.SetValue(COL_START_INSU + insuSuryoEnd, 6, instlList.Item(i).InstlDataKbn)
                    If instlList.Item(i).InstlDataKbn = "0" Then
                        xls.SetValue(COL_START_INSU + insuSuryoEnd, 6, "")
                    ElseIf instlList.Item(i).InstlDataKbn >= 10 AndAlso instlList.Item(i).InstlDataKbn <= 25 Then
                        For index = 0 To strGetValue.Length - 1
                            If strGetValue(index).Equals(instlList.Item(i).InstlDataKbn) Then
                                xls.SetValue(COL_START_INSU + insuSuryoEnd, 6, strSetValue(index))
                            End If
                        Next
                    Else
                        xls.SetValue(COL_START_INSU + insuSuryoEnd, 6, "M" & instlList.Item(i).InstlDataKbn)
                    End If

                    

                    xls.SetOrientation(COL_START_INSU + insuSuryoEnd, 7, COL_START_INSU + insuSuryoEnd, 7, ShisakuExcel.XlOrientation.xlVertical)
                    xls.SetValue(COL_START_INSU + insuSuryoEnd, 7, instlList.Item(i).InstlHinban)
                    xls.SetOrientation(COL_START_INSU + insuSuryoEnd, 8, COL_START_INSU + insuSuryoEnd, 8, ShisakuExcel.XlOrientation.xlVertical)
                    xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, instlList.Item(i).InstlHinbanKbn)
                    ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(7)) (TES)張 ADD END
                    hinbanList.Add(instlList(i).InstlHinban)
                    insuSuryoEnd = insuSuryoEnd + 1
                End If
                If Not hinbanList.Contains(instlList(i).InstlHinban) Then
                    '元のテーブルはどこ？'
                    If StringUtil.IsEmpty(instlList(i).InstlHinbanKbn) Then
                        bTable = EImpl.FindByBuhinTable(instlList(i).InstlHinban)
                        xls.SetValue(COL_START_INSU + insuSuryoEnd, 3, bTable)
                    End If

                    '員数列(A,B,C...)'
                    xls.SetValue(COL_START_INSU + insuSuryoEnd, 5, alphabetList(insuSuryoEnd).Label)
                    '員数'
                    ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(7)) (TES)張 ADD BEGIN
                    xls.SetOrientation(COL_START_INSU + insuSuryoEnd, 6, COL_START_INSU + insuSuryoEnd, 6, ShisakuExcel.XlOrientation.xlVertical)
                    'xls.SetValue(COL_START_INSU + i, 6, instlList.Item(i).InstlDataKbn)
                    If instlList.Item(i).InstlDataKbn = "0" Then
                        xls.SetValue(COL_START_INSU + i, 6, "")
                    ElseIf instlList.Item(i).InstlDataKbn >= 10 AndAlso instlList.Item(i).InstlDataKbn <= 25 Then
                        For index = 0 To strGetValue.Length - 1
                            If strGetValue(index).Equals(instlList.Item(i).InstlDataKbn) Then
                                xls.SetValue(COL_START_INSU + i, 6, strSetValue(index))
                            End If
                        Next
                    Else
                        xls.SetValue(COL_START_INSU + i, 6, "M" & instlList.Item(i).InstlDataKbn)
                    End If


                    xls.SetOrientation(COL_START_INSU + insuSuryoEnd, 7, COL_START_INSU + insuSuryoEnd, 7, ShisakuExcel.XlOrientation.xlVertical)
                    xls.SetValue(COL_START_INSU + insuSuryoEnd, 7, instlList.Item(i).InstlHinban)
                    xls.SetOrientation(COL_START_INSU + insuSuryoEnd, 8, COL_START_INSU + insuSuryoEnd, 8, ShisakuExcel.XlOrientation.xlVertical)
                    xls.SetValue(COL_START_INSU + i, 8, instlList.Item(i).InstlHinbanKbn)
                    ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(7)) (TES)張 ADD END
                    hinbanList.Add(instlList(i).InstlHinban)
                    insuSuryoEnd = insuSuryoEnd + 1

                End If
            Next

            insucount = insuSuryoEnd

            '員数'
            xls.MergeCells(COL_START_INSU, TITLE_ROW, COL_START_INSU + insuSuryoEnd - 1, TITLE_ROW, True)
            xls.SetAlliment(COL_START_INSU, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU, TITLE_ROW, "員数")
            '再使用不可'
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, 7, True)
            xls.SetOrientation(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "再使用不可")
            '出荷予定日'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, 7, True)
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "出図予定日")
            '材質'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd + 3, 6, True)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "材質")
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 7, "規格１")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 7, "規格２")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 7, "規格３")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 7, "メッキ")
            '板厚'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd + 1, 6, True)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "板厚")
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 7, "板厚")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 7, "u")
            '試作部品費'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, 7, True)
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "試作部品費" + vbCrLf + "（円）")
            '試作型費'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, 7, True)
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "試作型費" + vbCrLf + "（千円）")
            '備考'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, 7, True)
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "備考")


        End Sub

        ''' <summary>
        ''' シートのBODY部を設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <param name="kouseiList">部品編集情報</param>
        ''' <remarks></remarks>
        Private Sub SetBody(ByVal xls As ShisakuExcel, ByVal kouseiList As List(Of TShisakuBuhinEditVoHelper))
            If Not (kouseiList Is Nothing) And (kouseiList.Count > 0) Then
                Dim i As Integer
                Dim rowIndex As Integer = 0
                Dim rowBreak As Boolean = True
                For i = 0 To kouseiList.Count - 1
                    rowBreak = True
                    '部品番号表示順と部品番号が同じなら行インデックスを戻す'
                    If Not i = 0 Then
                        If StringUtil.Equals(kouseiList(i).BuhinNoHyoujiJun, kouseiList(i - 1).BuhinNoHyoujiJun) Then
                            rowIndex = rowIndex - 1
                            rowBreak = False
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
                    End If

                    If kouseiList(i).InsuSuryo < 0 Then
                        xls.SetValue(COL_START_INSU + shisakuBuhinEditVo.InstlHinbanHyoujiJun, START_ROW + rowIndex, "**")
                    Else
                        xls.SetValue(COL_START_INSU + shisakuBuhinEditVo.InstlHinbanHyoujiJun, START_ROW + rowIndex, shisakuBuhinEditVo.InsuSuryo)
                    End If

                    'For insuIndex As Integer = 0 To insuSuryoList.Count - 1
                    '    For insu As Integer = 0 To insuSuryoEnd
                    '        '部品番号表示順が合っているか検索'
                    '        If insuSuryoList(insuIndex).InstlHinbanHyoujiJun = insu Then
                    '            If StringUtil.Equals(shisakuBuhinEditVo.BuhinNoHyoujiJun, insuSuryoList(insuIndex).BuhinNoHyoujiJun) Then
                    '                If CheckInsuSuryo(shisakuBuhinEditVo, insuSuryoList(insuIndex)) Then

                    '                    If insuSuryoList(insuIndex).InsuSuryo < 0 Then
                    '                        xls.SetValue(COL_START_INSU + insu, START_ROW + rowIndex, "**")
                    '                    Else
                    '                        xls.SetValue(COL_START_INSU + insu, START_ROW + rowIndex, insuSuryoList(insuIndex).InsuSuryo)
                    '                    End If

                    '                End If
                    '            End If
                    '        End If
                    '    Next
                    'Next

                    xls.SetValue(COL_START_INSU + insucount, START_ROW + rowIndex, shisakuBuhinEditVo.Saishiyoufuka)

                    '2012/01/23 供給セクション追加
                    '供給セクション'
                    xls.SetValue(COL_START_INSU + insucount + 1, START_ROW + rowIndex, shisakuBuhinEditVo.KyoukuSection)

                    '出図予定日
                    'xls.SetValue(COL_START_INSU + insucount + 2, START_ROW + rowIndex, shisakuBuhinEditVo.ShutuzuYoteiDate)
                    If shisakuBuhinEditVo.ShutuzuYoteiDate = "99999999" Or shisakuBuhinEditVo.ShutuzuYoteiDate = "0" Then
                        xls.SetValue(COL_START_INSU + insucount + 2, START_ROW + rowIndex, "")
                    Else
                        xls.SetValue(COL_START_INSU + insucount + 2, START_ROW + rowIndex, shisakuBuhinEditVo.ShutuzuYoteiDate)
                    End If
                    If rowBreak Then
                        xls.SetValue(COL_START_INSU + insucount + 3, START_ROW + rowIndex, shisakuBuhinEditVo.ZaishituKikaku1)
                        xls.SetValue(COL_START_INSU + insucount + 4, START_ROW + rowIndex, shisakuBuhinEditVo.ZaishituKikaku2)
                        xls.SetValue(COL_START_INSU + insucount + 5, START_ROW + rowIndex, shisakuBuhinEditVo.ZaishituKikaku3)
                        xls.SetValue(COL_START_INSU + insucount + 6, START_ROW + rowIndex, shisakuBuhinEditVo.ZaishituMekki)
                        xls.SetValue(COL_START_INSU + insucount + 7, START_ROW + rowIndex, shisakuBuhinEditVo.ShisakuBankoSuryo)
                        xls.SetValue(COL_START_INSU + insucount + 8, START_ROW + rowIndex, shisakuBuhinEditVo.ShisakuBankoSuryoU)
                        xls.SetValue(COL_START_INSU + insucount + 9, START_ROW + rowIndex, shisakuBuhinEditVo.ShisakuBuhinHi)
                        xls.SetValue(COL_START_INSU + insucount + 10, START_ROW + rowIndex, shisakuBuhinEditVo.ShisakuKataHi)
                        xls.SetValue(COL_START_INSU + insucount + 11, START_ROW + rowIndex, shisakuBuhinEditVo.Bikou)
                    End If

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

        Private Function GetKouseiList(ByVal eventCode As String, _
                                        ByVal seKeka As String, _
                                        ByVal blockNo As String, _
                                        ByVal blockKaiteNo As String) As List(Of TShisakuBuhinEditVoHelper)
            Dim kouseiList = New List(Of TShisakuBuhinEditVoHelper)

            Dim shisakuBuhinEditDao As IShisakuBuhinEditDao = New ShisakuBuhinEditDaoImpl()
            kouseiList = shisakuBuhinEditDao.FindByBuhinEditAndInstl(eventCode, seKeka, blockNo, blockKaiteNo)

            Return kouseiList
        End Function

        Private Function GetInsuSuryoList(ByVal eventCode As String, _
                                        ByVal seKeka As String, _
                                        ByVal blockNo As String, _
                                        ByVal blockKaiteNo As String) As List(Of TShisakuSekkeiBlockInstlVo)
            Dim insuSuryoList = New List(Of TShisakuSekkeiBlockInstlVo)

            Dim shisakuBuhinEditDao As IShisakuBuhinEditDao = New ShisakuBuhinEditDaoImpl()
            insuSuryoList = shisakuBuhinEditDao.FindByBuhinEditInstl(eventCode, seKeka, blockNo, blockKaiteNo)

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
            xls.SetRowHeight(7, 7, 70)
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
        Private TITLE_ROW As Integer = 4
        '員数行'
        Private INSU_ROW As Integer = 6
        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(8)) (TES)張 ADD BEGIN
        'データ書き込み開始行'
        Private START_ROW As Integer = 9
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(8)) (TES)張 ADD END
      

#End Region


    End Class
End Namespace




