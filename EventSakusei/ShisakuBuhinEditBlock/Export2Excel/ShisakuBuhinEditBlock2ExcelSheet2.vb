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
    Public Class ShisakuBuhinEditBlock2ExcelSheet2
        Dim insuSuryoEnd As Integer
        Private insucount As Integer
        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧) (TES)張 ADD BEGIN
        Private strGetValue As String() = {"10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25"}
        Private strSetValue As String() = {"MA", "MB", "MC", "MD", "ME", "MF", "MG", "MH", "MI", "MJ", "MK", "ML", "MM", "MN", "MO", "MP"}
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧) (TES)張 ADD END

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
                          ByVal shisakuEventName As String, _
                          ByVal fileName As String, _
                          Optional ByVal isBase As Boolean = False)
            xls.SetActiveSheet(2)

            SetColumnNo()

            Me.setSheet1HeadPart1(xls, eventCode, seKeka, blockNo, blockKaiteNo, tanTouSya, tel, shisakuKaihatsuFugo, shisakuEventName)
            Dim instlList As New List(Of TShisakuSekkeiBlockInstlVo)
            Dim kouseiList As New List(Of TShisakuBuhinEditVoHelper)
            If isBase Then
                instlList = GetInsuSuryoList(eventCode, seKeka, blockNo, blockKaiteNo, isBase)
                kouseiList = GetKouseiList(eventCode, seKeka, blockNo, blockKaiteNo, isBase)
            Else
                instlList = GetInsuSuryoList(eventCode, seKeka, blockNo, blockKaiteNo)
                kouseiList = GetKouseiList(eventCode, seKeka, blockNo, blockKaiteNo)
            End If

            ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_y) (TES)張 ADD BEGIN
            '該当イベント取得
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(eventCode)
            ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_y) (TES)張 ADD END

            ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_y) (TES)張 CHG BEGIN
            'Me.SetTitle(xls, instlList)
            Me.SetTitle(xls, instlList, eventVo)
            ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_y) (TES)張 CHG END
            ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_z) (TES)張 CHG BEGIN
            'Me.SetBody(xls, kouseiList)
            Me.SetBody(xls, kouseiList, eventVo)
            ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_z) (TES)張 CHG END

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

        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_y) (TES)張 CHG BEGIN
        'Private Sub SetTitle(ByVal xls As ShisakuExcel, ByVal instlList As List(Of TShisakuSekkeiBlockInstlVo))
        '    SetTitleStr(xls, instlList)
        'End Sub
        ''' <summary>
        ''' タイトルを設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <param name="instlList">INSTLリスト</param>
        ''' <param name="eventVo">イベント情報</param>
        ''' <remarks></remarks>
        Private Sub SetTitle(ByVal xls As ShisakuExcel, ByVal instlList As List(Of TShisakuSekkeiBlockInstlVo), ByVal eventVo As TShisakuEventVo)
            SetTitleStr(xls, instlList, eventVo)
        End Sub
        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_y) (TES)張 CHG END

        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_y) (TES)張 CHG BEGIN
        'Private Sub SetTitleStr(ByVal xls As ShisakuExcel, ByVal instlList As List(Of TShisakuSekkeiBlockInstlVo))
        ''' <summary>
        ''' タイトルの文字を設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <param name="instlList">INSTLリスト</param>
        ''' <param name="eventVo">イベント情報</param>
        ''' <remarks></remarks>
        Private Sub SetTitleStr(ByVal xls As ShisakuExcel, ByVal instlList As List(Of TShisakuSekkeiBlockInstlVo), ByVal eventVo As TShisakuEventVo)
            ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_y) (TES)張 CHG END

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

            '最初に実行
            Dim lastInstlHinbanHyoujiJun As Integer = instlList(instlList.Count - 1).InstlHinbanHyoujiJun
            For i = 0 To lastInstlHinbanHyoujiJun
                xls.SetValue(COL_START_INSU + i, 5, alphabetList(i).Label)
                '員数'
                xls.SetOrientation(COL_START_INSU + i, 8, COL_START_INSU + i, 8, ShisakuExcel.XlOrientation.xlVertical)
                xls.SetOrientation(COL_START_INSU + i, 7, COL_START_INSU + i, 7, ShisakuExcel.XlOrientation.xlVertical)
                insuSuryoEnd = lastInstlHinbanHyoujiJun + 1
            Next

            For i = 0 To instlList.Count - 1

                Dim bTable As String = ""
                Dim instlHinbanAndKbn As String = ""
                Dim instlHinbanHyoujiJun As Integer = instlList(i).InstlHinbanHyoujiJun
                If i = 0 Then

                    '員数列'
                    If StringUtil.IsEmpty(instlList.Item(i).InstlDataKbn) Or instlList.Item(i).InstlDataKbn = "0" Then
                        xls.SetValue(COL_START_INSU + instlHinbanHyoujiJun, 6, "")
                    ElseIf CInt(instlList.Item(i).InstlDataKbn) >= 10 AndAlso CInt(instlList.Item(i).InstlDataKbn) <= 25 Then
                        For index = 0 To strGetValue.Length - 1
                            If strGetValue(index).Equals(instlList.Item(i).InstlDataKbn) Then
                                xls.SetValue(COL_START_INSU + instlHinbanHyoujiJun, 6, strSetValue(index))
                            End If
                        Next
                    Else
                        xls.SetValue(COL_START_INSU + instlHinbanHyoujiJun, 6, "M" & instlList.Item(i).InstlDataKbn)
                    End If
                    xls.SetValue(COL_START_INSU + instlHinbanHyoujiJun, 7, instlList.Item(i).InstlHinban)
                    xls.SetValue(COL_START_INSU + instlHinbanHyoujiJun, 8, instlList.Item(i).InstlHinbanKbn)
                    '該当イベントが「移管車改修」の場合
                    If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                        If instlList.Item(i).BaseInstlFlg = 1 Then
                            xls.SetBackColor(COL_START_INSU + instlHinbanHyoujiJun, 5, COL_START_INSU + instlHinbanHyoujiJun, 10000, RGB(176, 215, 237))
                        End If

                    End If
                    If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                        instlHinbanAndKbn = instlList(i).InstlHinban + instlList(i).InstlHinbanKbn + instlList(i).InstlDataKbn + instlList(i).BaseInstlFlg
                    Else
                        instlHinbanAndKbn = instlList(i).InstlHinban + instlList(i).InstlHinbanKbn + instlList(i).InstlDataKbn
                    End If
                    hinbanList.Add(instlHinbanAndKbn)
                End If

                If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                    instlHinbanAndKbn = instlList(i).InstlHinban + instlList(i).InstlHinbanKbn + instlList(i).InstlDataKbn + instlList(i).BaseInstlFlg
                Else
                    instlHinbanAndKbn = instlList(i).InstlHinban + instlList(i).InstlHinbanKbn + instlList(i).InstlDataKbn
                End If

                If Not hinbanList.Contains(instlHinbanAndKbn) Then
                    If StringUtil.IsEmpty(instlList.Item(i).InstlDataKbn) Or instlList.Item(i).InstlDataKbn = "0" Then
                        xls.SetValue(COL_START_INSU + instlHinbanHyoujiJun, 6, "")
                    ElseIf CInt(instlList.Item(i).InstlDataKbn) >= 10 AndAlso CInt(instlList.Item(i).InstlDataKbn) <= 25 Then
                        For index = 0 To strGetValue.Length - 1
                            If strGetValue(index).Equals(instlList.Item(i).InstlDataKbn) Then
                                xls.SetValue(COL_START_INSU + instlHinbanHyoujiJun, 6, strSetValue(index))
                            End If
                        Next
                    Else
                        xls.SetValue(COL_START_INSU + instlHinbanHyoujiJun, 6, "M" & instlList.Item(i).InstlDataKbn)
                    End If


                    xls.SetValue(COL_START_INSU + instlHinbanHyoujiJun, 7, instlList.Item(i).InstlHinban)
                    xls.SetValue(COL_START_INSU + instlHinbanHyoujiJun, 8, instlList.Item(i).InstlHinbanKbn)
                    If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                        If instlList.Item(i).BaseInstlFlg = 1 Then
                            xls.SetBackColor(COL_START_INSU + instlHinbanHyoujiJun, 5, COL_START_INSU + instlHinbanHyoujiJun, 10000, RGB(176, 215, 237))
                        End If
                    End If
                    If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                        instlHinbanAndKbn = instlList(i).InstlHinban + instlList(i).InstlHinbanKbn + instlList(i).InstlDataKbn + instlList(i).BaseInstlFlg
                    Else
                        instlHinbanAndKbn = instlList(i).InstlHinban + instlList(i).InstlHinbanKbn + instlList(i).InstlDataKbn
                    End If
                    hinbanList.Add(instlHinbanAndKbn)
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
            '2012/01/25 供給セクション
            '供給セクション'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, 7, True)
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "供給セクション")
            '出荷予定日'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, 7, True)
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "出図予定日")

            ''↓↓2014/07/25 Ⅰ.2.管理項目追加_as) (TES)張 ADD BEGIN
            ''↓↓2014/09/03 Ⅰ.2.管理項目追加_as) 酒井 CHG BEGIN
            '作り方、材質、板厚サブタイトル行のMerge行数を１行追加
            '作り方'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd + 6, 7, True)
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "作り方")
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, 8, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "製作方法")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, 8, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "型仕様１")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, 8, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "型仕様２")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, 8, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "型仕様３")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, 8, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "治具")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, 8, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "納入見通し")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, 8, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "部品製作規模・概要")
            ''↑↑2014/07/25 Ⅰ.2.管理項目追加_as) (TES)張 ADD END

            '材質'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd + 3, 7, True)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "材質")
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "規格１")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "規格２")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "規格３")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "メッキ")
            '板厚'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd + 1, 7, True)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "板厚")
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "板厚")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "u")
            ''↑↑2014/09/03 Ⅰ.2.管理項目追加_as) 酒井 CHG END
            ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
            '材料情報'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd + 1, 7, True)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "材料情報")
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "製品長")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "製品幅")
            'データ項目'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd + 3, 7, True)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "データ項目")
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "改訂№")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "エリア名")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "セット名")
            insuSuryoEnd = insuSuryoEnd + 1
            xls.SetValue(COL_START_INSU + insuSuryoEnd, 8, "改訂情報")
            ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END          
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
            '部品ノート'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, 7, True)
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "NOTE")
            '備考'
            insuSuryoEnd = insuSuryoEnd + 1
            xls.MergeCells(COL_START_INSU + insuSuryoEnd, TITLE_ROW, COL_START_INSU + insuSuryoEnd, 7, True)
            xls.SetAlliment(COL_START_INSU + insuSuryoEnd, TITLE_ROW, XlHAlign.xlHAlignCenter)
            xls.SetValue(COL_START_INSU + insuSuryoEnd, TITLE_ROW, "備考")


        End Sub

        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_z) (TES)張 CHG BEGIN
        'Private Sub SetBody(ByVal xls As ShisakuExcel, ByVal kouseiList As List(Of TShisakuBuhinEditVoHelper))
        ''' <summary>
        ''' シートのBODY部を設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <param name="kouseiList">部品編集情報</param>
        ''' <param name="eventVo">イベント情報</param>
        ''' <remarks></remarks>
        Private Sub SetBody(ByVal xls As ShisakuExcel, ByVal kouseiList As List(Of TShisakuBuhinEditVoHelper), ByVal eventVo As TShisakuEventVo)
            ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_z) (TES)張 CHG END
            If Not (kouseiList Is Nothing) And (kouseiList.Count > 0) Then
                '速度向上の為、改行した時以外は共通データを書き込まないように変更
                'また１データづつ出力せず配列に格納し、範囲指定で出力するように変更
                Dim i As Integer
                Dim rowIndex As Integer = 0
                Dim rowBreak As Boolean = True
                Dim maxRowNumber As Integer = 0

                '２次元配列の列数を算出
                For j As Integer = 0 To kouseiList.Count - 1
                    If Not j = 0 Then
                        If Not StringUtil.Equals(kouseiList(j).BuhinNoHyoujiJun, kouseiList(j - 1).BuhinNoHyoujiJun) Then
                            maxRowNumber = maxRowNumber + 1
                            ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_z) (TES)張 ADD BEGIN
                        Else
                            If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                                If Not StringUtil.Equals(kouseiList(j).BaseBuhinFlg, kouseiList(j - 1).BaseBuhinFlg) Then
                                    maxRowNumber = maxRowNumber + 1
                                End If
                            End If
                            ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_z) (TES)張 ADD END
                        End If
                    End If
                Next
                ''↓↓2014/08/13 Ⅰ.2.管理項目追加_at) (TES)張 CHG BEGIN
                ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                Dim dataMatrix(maxRowNumber, COL_START_INSU + insucount + 25) As String
                ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END
                ''↑↑2014/08/13 Ⅰ.2.管理項目追加_at) (TES)張 CHG END

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

                    If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                        If shisakuBuhinEditVo.BaseBuhinFlg = 1 Then
                            xls.SetBackColor(COL_LEVEL, START_ROW + rowIndex, COL_START_INSU + insucount + 25, START_ROW + rowIndex, RGB(176, 215, 237))
                        End If
                    End If


                    If rowBreak Then
                        '--------------------------------------------------------------------------------------------
                        'レベル’
                        '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                        '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                        If StringUtil.IsEmpty(shisakuBuhinEditVo.Level) Then
                            dataMatrix(rowIndex, COL_LEVEL - 1) = ""
                        Else
                            dataMatrix(rowIndex, COL_LEVEL - 1) = shisakuBuhinEditVo.Level.ToString()
                        End If
                        '--------------------------------------------------------------------------------------------

                        dataMatrix(rowIndex, COL_SHUKEI_CODE - 1) = shisakuBuhinEditVo.ShukeiCode
                        dataMatrix(rowIndex, COL_SIA_SHUKEI_CODE - 1) = shisakuBuhinEditVo.SiaShukeiCode
                        dataMatrix(rowIndex, COL_GENCYO_KBN - 1) = shisakuBuhinEditVo.GencyoCkdKbn
                        dataMatrix(rowIndex, COL_TORIHIKISAKI_CODE - 1) = shisakuBuhinEditVo.MakerCode
                        dataMatrix(rowIndex, COL_TORIHIKISAKI_NAME - 1) = shisakuBuhinEditVo.MakerName
                        dataMatrix(rowIndex, COL_BUHIN_NO - 1) = shisakuBuhinEditVo.BuhinNo
                        dataMatrix(rowIndex, COL_BUHIN_NO_KBN - 1) = shisakuBuhinEditVo.BuhinNoKbn
                        dataMatrix(rowIndex, COL_KAITEI - 1) = shisakuBuhinEditVo.BuhinNoKaiteiNo
                        dataMatrix(rowIndex, COL_EDA_BAN - 1) = shisakuBuhinEditVo.EdaBan
                        dataMatrix(rowIndex, COL_BUHIN_NAME - 1) = shisakuBuhinEditVo.BuhinName
                    End If

                    If kouseiList(i).InsuSuryo < 0 Then
                        dataMatrix(rowIndex, COL_START_INSU + shisakuBuhinEditVo.InstlHinbanHyoujiJun - 1) = "**"
                    Else
                        dataMatrix(rowIndex, COL_START_INSU + shisakuBuhinEditVo.InstlHinbanHyoujiJun - 1) = shisakuBuhinEditVo.InsuSuryo
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

                    If rowBreak Then
                        '再使用不可
                        dataMatrix(rowIndex, COL_START_INSU + insucount - 1) = shisakuBuhinEditVo.Saishiyoufuka

                        '2012/01/23 供給セクション追加
                        '供給セクション'
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 1 - 1) = shisakuBuhinEditVo.KyoukuSection

                        '出図予定日
                        'data(rowIndex,COL_START_INSU + insucount + 2-1) = shisakuBuhinEditVo.ShutuzuYoteiDate
                        If shisakuBuhinEditVo.ShutuzuYoteiDate = "99999999" Or shisakuBuhinEditVo.ShutuzuYoteiDate = "0" Then
                            dataMatrix(rowIndex, COL_START_INSU + insucount + 2 - 1) = ""
                        Else
                            dataMatrix(rowIndex, COL_START_INSU + insucount + 2 - 1) = shisakuBuhinEditVo.ShutuzuYoteiDate
                        End If

                        ''↓↓2014/07/25 Ⅰ.2.管理項目追加_at) (TES)張 CHG BEGIN
                        'dataMatrix(rowIndex, COL_START_INSU + insucount + 3 - 1) = shisakuBuhinEditVo.ZaishituKikaku1
                        'dataMatrix(rowIndex, COL_START_INSU + insucount + 4 - 1) = shisakuBuhinEditVo.ZaishituKikaku2
                        'dataMatrix(rowIndex, COL_START_INSU + insucount + 5 - 1) = shisakuBuhinEditVo.ZaishituKikaku3
                        'dataMatrix(rowIndex, COL_START_INSU + insucount + 6 - 1) = shisakuBuhinEditVo.ZaishituMekki
                        'dataMatrix(rowIndex, COL_START_INSU + insucount + 7 - 1) = shisakuBuhinEditVo.ShisakuBankoSuryo
                        'dataMatrix(rowIndex, COL_START_INSU + insucount + 8 - 1) = shisakuBuhinEditVo.ShisakuBankoSuryoU
                        'dataMatrix(rowIndex, COL_START_INSU + insucount + 9 - 1) = shisakuBuhinEditVo.ShisakuBuhinHi
                        'dataMatrix(rowIndex, COL_START_INSU + insucount + 10 - 1) = shisakuBuhinEditVo.ShisakuKataHi
                        ''2012/03/08 部品ノート出力追加
                        'dataMatrix(rowIndex, COL_START_INSU + insucount + 11 - 1) = shisakuBuhinEditVo.BuhinNote
                        'dataMatrix(rowIndex, COL_START_INSU + insucount + 12 - 1) = shisakuBuhinEditVo.Bikou
                        '作り方'
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 3 - 1) = shisakuBuhinEditVo.TsukurikataSeisaku
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 4 - 1) = shisakuBuhinEditVo.TsukurikataKatashiyou1
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 5 - 1) = shisakuBuhinEditVo.TsukurikataKatashiyou2
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 6 - 1) = shisakuBuhinEditVo.TsukurikataKatashiyou3
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 7 - 1) = shisakuBuhinEditVo.TsukurikataTigu
                        If shisakuBuhinEditVo.TsukurikataNounyu = "0" OrElse String.IsNullOrEmpty(shisakuBuhinEditVo.TsukurikataNounyu.ToString) Then
                            dataMatrix(rowIndex, COL_START_INSU + insucount + 8 - 1) = ""
                        Else
                            dataMatrix(rowIndex, COL_START_INSU + insucount + 8 - 1) = shisakuBuhinEditVo.TsukurikataNounyu
                        End If
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 9 - 1) = shisakuBuhinEditVo.TsukurikataKibo
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 10 - 1) = shisakuBuhinEditVo.ZaishituKikaku1
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 11 - 1) = shisakuBuhinEditVo.ZaishituKikaku2
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 12 - 1) = shisakuBuhinEditVo.ZaishituKikaku3
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 13 - 1) = shisakuBuhinEditVo.ZaishituMekki
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 14 - 1) = shisakuBuhinEditVo.ShisakuBankoSuryo
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 15 - 1) = shisakuBuhinEditVo.ShisakuBankoSuryoU
                        ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
                        If StringUtil.IsNotEmpty(shisakuBuhinEditVo.MaterialInfoLength) Then
                            dataMatrix(rowIndex, COL_START_INSU + insucount + 16 - 1) = shisakuBuhinEditVo.MaterialInfoLength
                        End If
                        If StringUtil.IsNotEmpty(shisakuBuhinEditVo.MaterialInfoWidth) Then
                            dataMatrix(rowIndex, COL_START_INSU + insucount + 17 - 1) = shisakuBuhinEditVo.MaterialInfoWidth
                        End If
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 18 - 1) = shisakuBuhinEditVo.DataItemKaiteiNo
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 19 - 1) = shisakuBuhinEditVo.DataItemAreaName
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 20 - 1) = shisakuBuhinEditVo.DataItemSetName
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 21 - 1) = shisakuBuhinEditVo.DataItemKaiteiInfo
                        ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END
                        ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 22 - 1) = shisakuBuhinEditVo.ShisakuBuhinHi
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 23 - 1) = shisakuBuhinEditVo.ShisakuKataHi
                        '2012/03/08 部品ノート出力追加
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 24 - 1) = shisakuBuhinEditVo.BuhinNote
                        dataMatrix(rowIndex, COL_START_INSU + insucount + 25 - 1) = shisakuBuhinEditVo.Bikou
                        ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END
                        ''↑↑2014/07/25 Ⅰ.2.管理項目追加_at) (TES)張 CHG END
                    End If

                    rowIndex = rowIndex + 1

                Next
                xls.CopyRange(0, START_ROW, UBound(dataMatrix, 2) - 1, START_ROW + UBound(dataMatrix), dataMatrix)
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
        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_aa) (TES)張 CHG BEGIN
        'Private Sub SetBodyOLD(ByVal xls As ShisakuExcel, ByVal kouseiList As List(Of TShisakuBuhinEditVoHelper))
        ''' <summary>
        ''' シートのBODY部を設定します
        ''' </summary>
        ''' <param name="xls">目的ファイル</param>
        ''' <param name="kouseiList">部品編集情報</param>
        ''' <param name="eventVo">イベント情報</param>
        ''' <remarks></remarks>
        Private Sub SetBodyOLD(ByVal xls As ShisakuExcel, ByVal kouseiList As List(Of TShisakuBuhinEditVoHelper), ByVal eventVo As TShisakuEventVo)
            ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_aa) (TES)張 CHG END
            If Not (kouseiList Is Nothing) And (kouseiList.Count > 0) Then
                '速度向上の為、改行した時以外は共通データを書き込まないように変更
                Dim i As Integer
                Dim rowIndex As Integer = 0
                Dim rowBreak As Boolean = True
                Dim maxRowNumber As Integer = 0

                '２次元配列の列数を算出
                For j As Integer = 0 To kouseiList.Count - 1
                    If Not j = 0 Then
                        If Not StringUtil.Equals(kouseiList(j).BuhinNoHyoujiJun, kouseiList(j - 1).BuhinNoHyoujiJun) Then
                            maxRowNumber = maxRowNumber + 1
                            ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_aa) (TES)張 ADD BEGIN
                        Else
                            If eventVo.BlockAlertKind = 2 Then
                                If Not StringUtil.Equals(kouseiList(j).BaseBuhinFlg, kouseiList(j - 1).BaseBuhinFlg) Then
                                    maxRowNumber = maxRowNumber + 1
                                End If
                            End If
                            ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_aa) (TES)張 ADD END
                        End If
                    End If
                Next
                Dim data(maxRowNumber, COL_START_INSU + insucount + 18) As String


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
                    ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_aa) (TES)張 ADD BEGIN
                    If eventVo.BlockAlertKind = 2 Then
                        If shisakuBuhinEditVo.BaseInstlFlg = 1 Then
                            ''↓↓2014/09/03 Ⅰ.3.設計編集 ベース改修専用化_aa) 酒井 ADD BEGIN
                            'xls.SetBackColor(COL_LEVEL - 1, rowIndex, COL_BUHIN_NAME - 1, rowIndex, Color.DimGray)
                            xls.SetBackColor(COL_LEVEL - 1, rowIndex, COL_BUHIN_NAME - 1, rowIndex, RGB(169, 169, 169))
                            ''↑↑2014/09/03 Ⅰ.3.設計編集 ベース改修専用化_aa) 酒井 ADD END
                        End If
                    End If
                    ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_aa) (TES)張 ADD END
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

                    If rowBreak Then
                        '再使用不可
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

                        xls.SetValue(COL_START_INSU + insucount + 3, START_ROW + rowIndex, shisakuBuhinEditVo.ZaishituKikaku1)
                        xls.SetValue(COL_START_INSU + insucount + 4, START_ROW + rowIndex, shisakuBuhinEditVo.ZaishituKikaku2)
                        xls.SetValue(COL_START_INSU + insucount + 5, START_ROW + rowIndex, shisakuBuhinEditVo.ZaishituKikaku3)
                        xls.SetValue(COL_START_INSU + insucount + 6, START_ROW + rowIndex, shisakuBuhinEditVo.ZaishituMekki)
                        xls.SetValue(COL_START_INSU + insucount + 7, START_ROW + rowIndex, shisakuBuhinEditVo.ShisakuBankoSuryo)
                        xls.SetValue(COL_START_INSU + insucount + 8, START_ROW + rowIndex, shisakuBuhinEditVo.ShisakuBankoSuryoU)
                        ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD BEGIN
                        xls.SetValue(COL_START_INSU + insucount + 9, START_ROW + rowIndex, shisakuBuhinEditVo.MaterialInfoLength)
                        xls.SetValue(COL_START_INSU + insucount + 10, START_ROW + rowIndex, shisakuBuhinEditVo.MaterialInfoWidth)
                        '改訂№
                        xls.SetValue(COL_START_INSU + insucount + 11, START_ROW + rowIndex, shisakuBuhinEditVo.DataItemKaiteiNo)
                        'エリア名
                        xls.SetValue(COL_START_INSU + insucount + 12, START_ROW + rowIndex, shisakuBuhinEditVo.DataItemAreaName)
                        xls.SetValue(COL_START_INSU + insucount + 13, START_ROW + rowIndex, shisakuBuhinEditVo.DataItemSetName)
                        xls.SetValue(COL_START_INSU + insucount + 14, START_ROW + rowIndex, shisakuBuhinEditVo.DataItemKaiteiInfo)
                        ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 ADD END
                        ''↓↓2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG BEGIN
                        xls.SetValue(COL_START_INSU + insucount + 15, START_ROW + rowIndex, shisakuBuhinEditVo.ShisakuBuhinHi)
                        xls.SetValue(COL_START_INSU + insucount + 16, START_ROW + rowIndex, shisakuBuhinEditVo.ShisakuKataHi)
                        xls.SetValue(COL_START_INSU + insucount + 17, START_ROW + rowIndex, shisakuBuhinEditVo.Bikou)
                        ''↑↑2014/12/24 38試作部品表 編集・改訂編集（ブロック） (TES)張 CHG END
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
                                        ByVal blockKaiteNo As String, _
                                        Optional ByVal isBase As Boolean = False) As List(Of TShisakuBuhinEditVoHelper)
            Dim kouseiList = New List(Of TShisakuBuhinEditVoHelper)
            Dim shisakuBuhinEditDao As IShisakuBuhinEditDao = New ShisakuBuhinEditDaoImpl()
            If isBase Then
                'ベース情報'
                kouseiList = shisakuBuhinEditDao.FindByBuhinEditAndInstlBase(eventCode, seKeka, blockNo)
            Else
                '改訂No情報'
                kouseiList = shisakuBuhinEditDao.FindByBuhinEditAndInstl(eventCode, seKeka, blockNo, blockKaiteNo)
            End If
            Return kouseiList
        End Function

        Private Function GetInsuSuryoList(ByVal eventCode As String, _
                                          ByVal seKeka As String, _
                                        ByVal blockNo As String, _
                                        ByVal blockKaiteNo As String, _
                                              Optional ByVal isBase As Boolean = False) As List(Of TShisakuSekkeiBlockInstlVo)
            Dim insuSuryoList = New List(Of TShisakuSekkeiBlockInstlVo)

            Dim shisakuBuhinEditDao As IShisakuBuhinEditDao = New ShisakuBuhinEditDaoImpl()

            If isBase Then
                insuSuryoList = shisakuBuhinEditDao.FindByBuhinEditInstlBase(eventCode, seKeka, blockNo)
            Else
                insuSuryoList = shisakuBuhinEditDao.FindByBuhinEditInstl(eventCode, seKeka, blockNo, blockKaiteNo)
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
            ''↓↓2014/09/03 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
            'INSTL品番行'
            'xls.SetRowHeight(INSU_ROW, INSU_ROW, 167)
            xls.SetRowHeight(7, 7, 167)
            'INSTL品番区分行'
            'xls.SetRowHeight(7, 7, 70)
            xls.SetRowHeight(8, 8, 70)
            ''↑↑2014/09/03 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END
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
        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(2)) (TES)張 ADD BEGIN
        'データ書き込み開始行'
        Private START_ROW As Integer = 9
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑧(2)) (TES)張 ADD END
#End Region


    End Class
End Namespace


