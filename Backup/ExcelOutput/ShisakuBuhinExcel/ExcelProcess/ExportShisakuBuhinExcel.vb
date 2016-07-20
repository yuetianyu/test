Imports ShisakuCommon
Imports EBom.Excel
Imports EBom.Data
Imports EBom.Common
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo
Imports ExcelOutput.ShisakuBuhinExcel.Dao
Imports ExcelOutput.ShisakuBuhinExcel.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl

Namespace ShisakuBuhinExcel.Excel
    '試作部品表エクセルの出力'
    Public Class ExportShisakuBuhinExcel

        Private impl As ShisakuBuhinExcelDao
        Private MaxBaseHyoujiJun As Integer


        Private EventVo As TShisakuEventVo

        Public Sub New(ByVal shisakuEventCode As String)
            Dim systemDrive As String = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            Dim fileName As String
            Dim BuhinEditListVo As New List(Of BuhinEditVoHelper)
            Dim BaseListVo As New List(Of TShisakuEventBaseVo)
            impl = New ShisakuBuhinExcelDaoImpl

            EventVo = impl.FindByEvent(shisakuEventCode)

            Dim BlockNoList As New List(Of TShisakuBuhinEditVo)
            BlockNoList = impl.FindByBlockGroup(shisakuEventCode)

            Dim Bvo As List(Of BuhinEditVoHelper)
            For Each vo As TShisakuBuhinEditVo In BlockNoList

                If EventVo.BlockAlertKind = "2" And EventVo.KounyuShijiFlg = "0" Then
                    '最初にベース情報を抽出（ブロック毎）
                    Bvo = impl.FindByBuhinEditBase(shisakuEventCode, vo.ShisakuBlockNo)
                Else
                    Bvo = impl.FindByBuhinEdit(shisakuEventCode, vo.ShisakuBlockNo)
                End If
                BuhinEditListVo.AddRange(Bvo)

                '2012/02/27 レンジで追加
                If EventVo.BlockAlertKind = "2" And EventVo.KounyuShijiFlg = "0" Then
                    '最後にベース情報以外を抽出（全ブロック）
                    Bvo = impl.FindByAllBuhinEdit(shisakuEventCode, vo.ShisakuBlockNo)
                    BuhinEditListVo.AddRange(Bvo)
                End If
            Next

            '2014/12/17 追加
            'ベース情報とベース以外の情報を部品表示順などでソート()
            BuhinEditListVo.Sort(AddressOf BuhinEditVoHelper.Comparison)

            BaseListVo = impl.FindByBase(shisakuEventCode)
            MaxBaseHyoujiJun = 0
            For Each Vo As TShisakuEventBaseVo In BaseListVo
                If MaxBaseHyoujiJun < Vo.HyojijunNo Then
                    MaxBaseHyoujiJun = Vo.HyojijunNo
                End If
            Next

            Using sfd As New SaveFileDialog()
                sfd.FileName = ShisakuCommon.ShisakuGlobal.ExcelOutPut
                sfd.InitialDirectory = systemDrive
                '--------------------------------------------------
                'EXCEL出力先フォルダチェック
                ShisakuCommon.ShisakuComFunc.ExcelOutPutDirCheck()
                '--------------------------------------------------
                '[Excel出力系 D]
                fileName = EventVo.ShisakuKaihatsuFugo + EventVo.ShisakuEventName + " 試作部品表 " + Now.ToString("MMdd") + Now.ToString("HHmm") + ".xls"
                fileName = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + StringUtil.ReplaceInvalidCharForFileName(fileName)    '2012/02/08 Excel出力ディレクトリ指定対応
            End Using

            If Not ShisakuComFunc.IsFileOpen(ShisakuCommon.ShisakuGlobal.ExcelOutPut) Then

                Using xls As New ShisakuExcel(fileName)
                    xls.OpenBook(fileName)
                    xls.ClearWorkBook()
                    xls.SetFont("ＭＳ Ｐゴシック", 11)
                    SetShisakuBuhinExcel(xls, BuhinEditListVo, EventVo, BaseListVo)
                    '2012/02/02'
                    'A4横で印刷できるように変更'
                    xls.PrintPaper(fileName, 1, "A4")
                    xls.PrintOrientation(fileName, 1, 1, False)
                    xls.SetActiveSheet(1)
                    xls.Save()
                End Using
                Process.Start(fileName)
            End If

        End Sub

        ''' <summary>
        ''' Excel出力　試作部品表
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub SetShisakuBuhinExcel(ByVal xls As ShisakuExcel, ByVal BuhinEditListVo As List(Of BuhinEditVoHelper), _
                                         ByVal EventVo As TShisakuEventVo, _
                                         ByVal BaseListVo As List(Of TShisakuEventBaseVo))

            '列の設定'
            SetColumnNo()
            SetIdxColumnNo()
            'ヘッダー部'
            SetHeader(xls, EventVo)
            'タイトル部'
            SetTitle(xls, BaseListVo)

            'ボディ部'
            SetBody(xls, BuhinEditListVo, EventVo, BaseListVo)   '<----時間がかかる  要チューニング


            '列と行の調整'
            SetColumnRow(xls)


        End Sub

        ''' <summary>
        ''' Excel出力　試作部品表ヘッダー部
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub SetHeader(ByVal xls As ShisakuExcel, ByVal EventVo As TShisakuEventVo)
            'イベントコード'
            xls.SetValue(1, 1, EventVo.ShisakuEventCode)
            'イベント名称'
            Dim EventName As String
            EventName = EventVo.ShisakuKaihatsuFugo + " " + EventVo.ShisakuEventName
            xls.SetValue(1, 2, "イベント名称：" + EventName)

            Dim aDate As New DateTime
            aDate = DateTime.Now

            '抽出日時'
            xls.SetValue(5, 2, "抽出日時：" + aDate.ToString())

        End Sub

        ''' <summary>
        ''' Excel出力　試作部品表タイトル部
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub SetTitle(ByVal xls As ShisakuExcel, ByVal BaseListVo As List(Of TShisakuEventBaseVo))

            'ブロックNo'
            xls.MergeCells(COLUMN_BLOCK_NO, TITLE_ROW, COLUMN_BLOCK_NO, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_BLOCK_NO, TITLE_ROW, COLUMN_BLOCK_NO, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_BLOCK_NO, TITLE_ROW, "ブロックNo")
            '改訂No'
            xls.MergeCells(COLUMN_KAITEI_NO, TITLE_ROW, COLUMN_KAITEI_NO, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_KAITEI_NO, TITLE_ROW, COLUMN_KAITEI_NO, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_KAITEI_NO, TITLE_ROW, "改訂No")

            'レベル'
            xls.MergeCells(COLUMN_LEVEL, TITLE_ROW, COLUMN_LEVEL, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_LEVEL, TITLE_ROW, COLUMN_LEVEL, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_LEVEL, TITLE_ROW, "レベル")
            'ユニット区分'
            xls.MergeCells(COLUMN_UNIT_KBN, TITLE_ROW, COLUMN_UNIT_KBN, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_UNIT_KBN, TITLE_ROW, COLUMN_UNIT_KBN, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_UNIT_KBN, TITLE_ROW, "ユニット区分")
            '国内集計'
            xls.MergeCells(COLUMN_SHUKEI_CODE, TITLE_ROW, COLUMN_SHUKEI_CODE, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_SHUKEI_CODE, TITLE_ROW, COLUMN_SHUKEI_CODE, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_SHUKEI_CODE, TITLE_ROW, "国内集計")
            '海外集計'
            xls.MergeCells(COLUMN_SIA_SHUKEI_CODE, TITLE_ROW, COLUMN_SIA_SHUKEI_CODE, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_SIA_SHUKEI_CODE, TITLE_ROW, COLUMN_SIA_SHUKEI_CODE, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_SIA_SHUKEI_CODE, TITLE_ROW, "海外集計")
            '現調区分'
            xls.MergeCells(COLUMN_GENCYO_KBN, TITLE_ROW, COLUMN_GENCYO_KBN, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_GENCYO_KBN, TITLE_ROW, COLUMN_GENCYO_KBN, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_GENCYO_KBN, TITLE_ROW, "現調区分")
            '取引先コード'
            xls.MergeCells(COLUMN_TORIHIKISAKI_CODE, TITLE_ROW, COLUMN_TORIHIKISAKI_CODE, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_TORIHIKISAKI_CODE, TITLE_ROW, COLUMN_TORIHIKISAKI_CODE, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_TORIHIKISAKI_CODE, TITLE_ROW, "取引先コード")
            '取引先名称'
            xls.MergeCells(COLUMN_TORIHIKISAKI_NAME, TITLE_ROW, COLUMN_TORIHIKISAKI_NAME, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_TORIHIKISAKI_NAME, TITLE_ROW, COLUMN_TORIHIKISAKI_NAME, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_TORIHIKISAKI_NAME, TITLE_ROW, "取引先名称")
            '部品番号'
            xls.MergeCells(COLUMN_BUHIN_NO, TITLE_ROW, COLUMN_BUHIN_NO, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_BUHIN_NO, TITLE_ROW, COLUMN_BUHIN_NO, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_BUHIN_NO, TITLE_ROW, "部品番号")
            '試作区分'
            xls.MergeCells(COLUMN_SHISAKU_KBN, TITLE_ROW, COLUMN_SHISAKU_KBN, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_SHISAKU_KBN, TITLE_ROW, COLUMN_SHISAKU_KBN, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_SHISAKU_KBN, TITLE_ROW, "試作区分")
            '改訂'
            xls.MergeCells(COLUMN_KAITEI, TITLE_ROW, COLUMN_KAITEI, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_KAITEI, TITLE_ROW, COLUMN_KAITEI, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_KAITEI, TITLE_ROW, "改訂")
            '枝番'
            xls.MergeCells(COLUMN_EDA_BAN, TITLE_ROW, COLUMN_EDA_BAN, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_EDA_BAN, TITLE_ROW, COLUMN_EDA_BAN, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_EDA_BAN, TITLE_ROW, "枝番")
            '部品名称'
            xls.MergeCells(COLUMN_BUHIN_NAME, TITLE_ROW, COLUMN_BUHIN_NAME, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_BUHIN_NAME, TITLE_ROW, COLUMN_BUHIN_NAME, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_BUHIN_NAME, TITLE_ROW, "部品名称")
            '合計員数'
            xls.MergeCells(COLUMN_TOTAL_INSU_SURYO, TITLE_ROW, COLUMN_TOTAL_INSU_SURYO, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_TOTAL_INSU_SURYO, TITLE_ROW, COLUMN_TOTAL_INSU_SURYO, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_TOTAL_INSU_SURYO, TITLE_ROW, "合計員数")
            '再使用不可'
            xls.MergeCells(COLUMN_SAISHIYOUFUKA, TITLE_ROW, COLUMN_SAISHIYOUFUKA, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_SAISHIYOUFUKA, TITLE_ROW, COLUMN_SAISHIYOUFUKA, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_SAISHIYOUFUKA, TITLE_ROW, "再使用不可")
            '供給セクション'
            xls.MergeCells(COLUMN_KYOUKU_SECTION, TITLE_ROW, COLUMN_KYOUKU_SECTION, TITLE_ROW + 1, True)
            xls.SetValue(COLUMN_KYOUKU_SECTION, TITLE_ROW, "供給セクション")
            '出図予定日'
            xls.MergeCells(COLUMN_SHUTUZUYOTEIBI, TITLE_ROW, COLUMN_SHUTUZUYOTEIBI, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_SHUTUZUYOTEIBI, TITLE_ROW, COLUMN_SHUTUZUYOTEIBI, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_SHUTUZUYOTEIBI, TITLE_ROW, "出図予定日")

            ''↓↓2014/07/25 Ⅰ.2.管理項目追加_be) (TES)張 ADD BEGIN
            xls.MergeCells(COLUMN_TSUKURIKATA_SEISAKU, TITLE_ROW, COLUMN_TSUKURIKATA_KIBO, TITLE_ROW, True)
            xls.SetValue(COLUMN_TSUKURIKATA_SEISAKU, TITLE_ROW, "作り方")

            '作り方制作方法'
            xls.SetValue(COLUMN_TSUKURIKATA_SEISAKU, TITLE_ROW + 1, "製作方法")
            '作り方型仕様１'
            xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU1, TITLE_ROW + 1, "型仕様１")
            '作り方型仕様２'
            xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU2, TITLE_ROW + 1, "型仕様２")
            '作り方型仕様３'
            xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU3, TITLE_ROW + 1, "型仕様３")
            '作り方治具'
            xls.SetValue(COLUMN_TSUKURIKATA_TIGU, TITLE_ROW + 1, "治具")
            '作り方納入見通し'
            xls.SetValue(COLUMN_TSUKURIKATA_NOUNYU, TITLE_ROW + 1, "納入見通し")
            '作り方部品製作規模・概要'
            xls.SetValue(COLUMN_TSUKURIKATA_KIBO, TITLE_ROW + 1, "部品製作規模・概要")
            ''↑↑2014/07/25 Ⅰ.2.管理項目追加_be) (TES)張 ADD END


            '材質規格'
            xls.MergeCells(COLUMN_ZAISHITSU_KIKAKU_1, TITLE_ROW, COLUMN_ZAISHITSU_MEKKI, TITLE_ROW, True)
            xls.SetValue(COLUMN_ZAISHITSU_KIKAKU_1, TITLE_ROW, "材質規格")
            '材質規格１'
            xls.SetValue(COLUMN_ZAISHITSU_KIKAKU_1, TITLE_ROW + 1, "規格１")
            '材質規格２'
            xls.SetValue(COLUMN_ZAISHITSU_KIKAKU_2, TITLE_ROW + 1, "規格２")
            '材質規格３'
            xls.SetValue(COLUMN_ZAISHITSU_KIKAKU_3, TITLE_ROW + 1, "規格３")
            '材質メッキ'
            xls.SetValue(COLUMN_ZAISHITSU_MEKKI, TITLE_ROW + 1, "メッキ")

            '板厚'
            xls.MergeCells(COLUMN_BANKO_SURYO, TITLE_ROW, COLUMN_BANKO_SURYO_U, TITLE_ROW, True)
            xls.SetValue(COLUMN_BANKO_SURYO, TITLE_ROW, "板厚")
            '板厚'
            xls.SetValue(COLUMN_BANKO_SURYO, TITLE_ROW + 1, "板厚")
            '板厚u'
            xls.SetValue(COLUMN_BANKO_SURYO_U, TITLE_ROW + 1, "u")

            ''材料情報
            xls.MergeCells(COLUMN_MATERIAL_INFO_LENGTH, TITLE_ROW, COLUMN_MATERIAL_INFO_WIDTH, TITLE_ROW, True)
            xls.SetValue(COLUMN_MATERIAL_INFO_LENGTH, TITLE_ROW, "材料情報")
            xls.SetValue(COLUMN_MATERIAL_INFO_LENGTH, TITLE_ROW + 1, "製品長")
            xls.SetValue(COLUMN_MATERIAL_INFO_WIDTH, TITLE_ROW + 1, "製品幅")


            xls.MergeCells(COLUMN_DATA_ITEM_KAITEI_NO, TITLE_ROW, COLUMN_DATA_ITEM_KAITEI_INFO, TITLE_ROW, True)
            xls.SetValue(COLUMN_DATA_ITEM_KAITEI_NO, TITLE_ROW, "データ項目")
            xls.SetValue(COLUMN_DATA_ITEM_KAITEI_NO, TITLE_ROW + 1, "改訂№")
            xls.SetValue(COLUMN_DATA_ITEM_AREA_NAME, TITLE_ROW + 1, "エリア名")
            xls.SetValue(COLUMN_DATA_ITEM_SET_NAME, TITLE_ROW + 1, "セット名")
            xls.SetValue(COLUMN_DATA_ITEM_KAITEI_INFO, TITLE_ROW + 1, "改訂情報")

            '試作部品費'
            xls.MergeCells(COLUMN_SHISAKU_BUHIN_HI, TITLE_ROW, COLUMN_SHISAKU_BUHIN_HI, TITLE_ROW + 1, True)
            xls.SetValue(COLUMN_SHISAKU_BUHIN_HI, TITLE_ROW, "試作部品費(円)")
            '試作型費'
            xls.MergeCells(COLUMN_SHISAKU_KATA_HI, TITLE_ROW, COLUMN_SHISAKU_KATA_HI, TITLE_ROW + 1, True)
            xls.SetValue(COLUMN_SHISAKU_KATA_HI, TITLE_ROW, "試作型費(千円)")
            'NOTE'
            xls.MergeCells(COLUMN_BUHIN_NOTE, TITLE_ROW, COLUMN_BUHIN_NOTE, TITLE_ROW + 1, True)
            xls.SetValue(COLUMN_BUHIN_NOTE, TITLE_ROW, "NOTE")
            '備考'
            xls.MergeCells(COLUMN_BIKOU, TITLE_ROW, COLUMN_BIKOU, TITLE_ROW + 1, True)
            xls.SetValue(COLUMN_BIKOU, TITLE_ROW, "備考")

            '号車'
            For Each bVo As TShisakuEventBaseVo In BaseListVo
                xls.MergeCells(COLUMN_START_GOUSYA + bVo.HyojijunNo, TITLE_ROW, COLUMN_START_GOUSYA + bVo.HyojijunNo, TITLE_ROW + 1, True)
                xls.SetValue(COLUMN_START_GOUSYA + bVo.HyojijunNo, TITLE_ROW, bVo.ShisakuGousya)
                xls.SetOrientation(COLUMN_START_GOUSYA + bVo.HyojijunNo, TITLE_ROW, COLUMN_START_GOUSYA + bVo.HyojijunNo, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            Next
            'For index As Integer = 0 To BaseListVo.Count - 1

            For index As Integer = 0 To MaxBaseHyoujiJun
                xls.MergeCells(COLUMN_START_GOUSYA + index, TITLE_ROW, COLUMN_START_GOUSYA + index, TITLE_ROW + 1, True)
                xls.SetColWidth(COLUMN_START_GOUSYA + index, COLUMN_START_GOUSYA + index, 2.25)
            Next
        End Sub

        ''' <summary>
        ''' Excel出力　試作部品表ボディ部
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub SetBody(ByVal xls As ShisakuExcel, ByVal BuhinEditListVo As List(Of BuhinEditVoHelper), _
                            ByVal EventVo As TShisakuEventVo, _
                            ByVal BaseListVo As List(Of TShisakuEventBaseVo))

            '速度向上の為、改行した時以外は共通データを書き込まないように変更
            'また１データづつ出力せず配列に格納し、範囲指定で出力するように変更
            Dim rowIndex As Integer = 0
            Dim MergeFlag As Boolean = False
            Dim wUnitKbn As String = "9"

            Dim maxRowNumber As Integer = 0

            '２次元配列の列数を算出
            For i As Integer = 0 To BuhinEditListVo.Count - 1
                If Not i = 0 Then
                    If Not CheckMerge(BuhinEditListVo(i), BuhinEditListVo(i - 1)) Then
                        maxRowNumber += 1
                    End If
                Else
                    maxRowNumber += 1
                End If
            Next
            Dim dataMatrix(maxRowNumber, COLUMN_START_GOUSYA + MaxBaseHyoujiJun) As String

            'ユニット区分だけとる為にはDBアクセスはもったいない
            Dim unitKbnHash As Hashtable = impl.FindByShisakuBlockNoForUnitKbn()

            For editindex As Integer = 0 To BuhinEditListVo.Count - 1

                If Not editindex = 0 Then
                    If CheckMerge(BuhinEditListVo(editindex), BuhinEditListVo(editindex - 1)) Then
                        rowIndex = rowIndex - 1
                        MergeFlag = True
                    End If
                End If

                If MergeFlag Then
                    '員数'
                    Dim colIndex As Integer = COLUMN_START_GOUSYA + BuhinEditListVo(editindex).HyojijunNo - 1
                    Dim insu As String = dataMatrix(rowIndex, colIndex)
                    If StringUtil.IsEmpty(insu) Then
                        If BuhinEditListVo(editindex).InsuSuryo < 0 Then
                            dataMatrix(rowIndex, colIndex) = "**"
                        Else
                            dataMatrix(rowIndex, colIndex) = BuhinEditListVo(editindex).InsuSuryo
                        End If
                    Else
                        If Not StringUtil.Equals(insu, "**") Then
                            If BuhinEditListVo(editindex).InsuSuryo < 0 Then
                                dataMatrix(rowIndex, colIndex) = "**"
                            Else
                                Dim total As Integer = Integer.Parse(insu) + BuhinEditListVo(editindex).InsuSuryo
                                dataMatrix(rowIndex, colIndex) = total
                            End If
                        End If
                    End If
                    rowIndex = rowIndex + 1
                    MergeFlag = False
                Else


                    With BuhinEditListVo(editindex)

                        'ブロックNo'
                        dataMatrix(rowIndex, IDX_COLUMN_BLOCK_NO) = .ShisakuBlockNo
                        '改訂No'
                        dataMatrix(rowIndex, IDX_COLUMN_KAITEI_NO) = .ShisakuBlockNoKaiteiNo
                        'レベル'
                        '   2014/02/13　レベルがブランクの場合、ブランクで出力する。
                        '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                        If StringUtil.IsEmpty(.Level) Then
                            dataMatrix(rowIndex, IDX_COLUMN_LEVEL) = ""
                        Else
                            dataMatrix(rowIndex, IDX_COLUMN_LEVEL) = .Level
                        End If

                        'ユニット区分は試作ブロック情報から取得する。


                        If Not editindex = 0 Then
                            If Not StringUtil.Equals(.ShisakuBlockNo, BuhinEditListVo(editindex - 1).ShisakuBlockNo) Then
                                wUnitKbn = unitKbnHash(.ShisakuEventCode & _
                                                        .ShisakuBukaCode & _
                                                        .ShisakuBlockNo & _
                                                        .ShisakuBlockNoKaiteiNo)
                            End If
                        Else
                            wUnitKbn = unitKbnHash(.ShisakuEventCode & _
                                                    .ShisakuBukaCode & _
                                                    .ShisakuBlockNo & _
                                                    .ShisakuBlockNoKaiteiNo)
                        End If


                        dataMatrix(rowIndex, IDX_COLUMN_UNIT_KBN) = wUnitKbn

                        '国内集計'
                        dataMatrix(rowIndex, IDX_COLUMN_SHUKEI_CODE) = .ShukeiCode
                        '海外集計'
                        dataMatrix(rowIndex, IDX_COLUMN_SIA_SHUKEI_CODE) = .SiaShukeiCode
                        '現調区分'
                        dataMatrix(rowIndex, IDX_COLUMN_GENCYO_KBN) = .GencyoCkdKbn
                        '取引先コード'
                        dataMatrix(rowIndex, IDX_COLUMN_TORIHIKISAKI_CODE) = .MakerCode
                        '取引先名称'
                        dataMatrix(rowIndex, IDX_COLUMN_TORIHIKISAKI_NAME) = .MakerName
                        '部品番号'
                        dataMatrix(rowIndex, IDX_COLUMN_BUHIN_NO) = .BuhinNo
                        '試作区分'
                        dataMatrix(rowIndex, IDX_COLUMN_SHISAKU_KBN) = .BuhinNoKbn
                        '改訂'
                        dataMatrix(rowIndex, IDX_COLUMN_KAITEI) = .BuhinNoKaiteiNo
                        '枝番'
                        dataMatrix(rowIndex, IDX_COLUMN_EDA_BAN) = .EdaBan
                        '部品名称'
                        dataMatrix(rowIndex, IDX_COLUMN_BUHIN_NAME) = .BuhinName

                        '再使用不可'
                        dataMatrix(rowIndex, IDX_COLUMN_SAISHIYOUFUKA) = .Saishiyoufuka
                        '供給セクション'
                        dataMatrix(rowIndex, IDX_COLUMN_KYOUKU_SECTION) = .KyoukuSection
                        '出図予定日'
                        If .ShutuzuYoteiDate = 99999999 OrElse .ShutuzuYoteiDate = 0 Then
                            dataMatrix(rowIndex, IDX_COLUMN_SHUTUZUYOTEIBI) = ""
                        Else
                            dataMatrix(rowIndex, IDX_COLUMN_SHUTUZUYOTEIBI) = .ShutuzuYoteiDate
                        End If
                        '作り方制作方法'
                        dataMatrix(rowIndex, IDX_COLUMN_TSUKURIKATA_SEISAKU) = .TsukurikataSeisaku
                        '作り方型仕様１'
                        dataMatrix(rowIndex, IDX_COLUMN_TSUKURIKATA_KATASHIYOU1) = .TsukurikataKatashiyou1
                        '作り方型仕様２'
                        dataMatrix(rowIndex, IDX_COLUMN_TSUKURIKATA_KATASHIYOU2) = .TsukurikataKatashiyou2
                        '作り方型仕様３'
                        dataMatrix(rowIndex, IDX_COLUMN_TSUKURIKATA_KATASHIYOU3) = .TsukurikataKatashiyou3
                        '作り方治具'
                        dataMatrix(rowIndex, IDX_COLUMN_TSUKURIKATA_TIGU) = .TsukurikataTigu
                        '作り方納入見通り'
                        If .TsukurikataNounyu Is Nothing Or .TsukurikataNounyu = 0 Then
                            dataMatrix(rowIndex, IDX_COLUMN_TSUKURIKATA_NOUNYU) = ""
                        Else
                            dataMatrix(rowIndex, IDX_COLUMN_TSUKURIKATA_NOUNYU) = .TsukurikataNounyu
                        End If
                        '作り方部品製作規模・概要'
                        dataMatrix(rowIndex, IDX_COLUMN_TSUKURIKATA_KIBO) = .TsukurikataKibo
                        '材質規格１'
                        dataMatrix(rowIndex, IDX_COLUMN_ZAISHITSU_KIKAKU_1) = .ZaishituKikaku1
                        '材質規格２'
                        dataMatrix(rowIndex, IDX_COLUMN_ZAISHITSU_KIKAKU_2) = .ZaishituKikaku2
                        '材質規格３'
                        dataMatrix(rowIndex, IDX_COLUMN_ZAISHITSU_KIKAKU_3) = .ZaishituKikaku3
                        '材質メッキ'
                        dataMatrix(rowIndex, IDX_COLUMN_ZAISHITSU_MEKKI) = .ZaishituMekki
                        '板厚'
                        dataMatrix(rowIndex, IDX_COLUMN_BANKO_SURYO) = .ShisakuBankoSuryo
                        '板厚'
                        dataMatrix(rowIndex, IDX_COLUMN_BANKO_SURYO_U) = .ShisakuBankoSuryoU
                        '↓↓↓2014/12/30 メタル項目を追加 TES)張 ADD BEGIN
                        dataMatrix(rowIndex, IDX_COLUMN_MATERIAL_INFO_LENGTH) = strNVL(.MaterialInfoLength)
                        dataMatrix(rowIndex, IDX_COLUMN_MATERIAL_INFO_WIDTH) = strNVL(.MaterialInfoWidth)
                        dataMatrix(rowIndex, IDX_COLUMN_DATA_ITEM_KAITEI_NO) = strNVL(.DataItemKaiteiNo)
                        dataMatrix(rowIndex, IDX_COLUMN_DATA_ITEM_AREA_NAME) = strNVL(.DataItemAreaName)
                        dataMatrix(rowIndex, IDX_COLUMN_DATA_ITEM_SET_NAME) = strNVL(.DataItemSetName)
                        dataMatrix(rowIndex, IDX_COLUMN_DATA_ITEM_KAITEI_INFO) = strNVL(.DataItemKaiteiInfo)
                        '↑↑↑2014/12/30 メタル項目を追加 TES)張 ADD END
                        '試作部品費'
                        dataMatrix(rowIndex, IDX_COLUMN_SHISAKU_BUHIN_HI) = .ShisakuBuhinHi
                        '試作型費'
                        dataMatrix(rowIndex, IDX_COLUMN_SHISAKU_KATA_HI) = .ShisakuKataHi
                        'NOTE'
                        dataMatrix(rowIndex, IDX_COLUMN_BUHIN_NOTE) = .BuhinNote
                        '備考'
                        dataMatrix(rowIndex, IDX_COLUMN_BIKOU) = .Bikou

                        '員数'
                        Dim HoyjijyunNo As Integer = COLUMN_START_GOUSYA + .HyojijunNo - 1
                        Dim insu As String = dataMatrix(rowIndex, HoyjijyunNo)
                        If StringUtil.IsEmpty(insu) Then
                            If .InsuSuryo < 0 Then
                                dataMatrix(rowIndex, HoyjijyunNo) = "**"
                            Else
                                dataMatrix(rowIndex, HoyjijyunNo) = .InsuSuryo
                            End If
                        Else
                            If Not StringUtil.Equals(insu, "**") Then
                                If .InsuSuryo < 0 Then
                                    dataMatrix(rowIndex, HoyjijyunNo) = "**"
                                Else
                                    insu += .InsuSuryo
                                    dataMatrix(rowIndex, HoyjijyunNo) = insu
                                End If
                            End If

                        End If

                        If EventVo.BlockAlertKind = 2 And EventVo.KounyuShijiFlg = "0" Then
                            If .BaseInstlFlg = 1 Then
                                xls.SetBackColor(COLUMN_BLOCK_NO, START_ROW + rowIndex, COLUMN_START_GOUSYA + MaxBaseHyoujiJun, START_ROW + rowIndex, RGB(176, 215, 237))
                            End If
                        End If

                    End With

                    rowIndex += 1

                End If

            Next

            '合計員数を取得する'
            For rowindex2 As Integer = 0 To rowIndex
                Dim flag As Boolean = False
                Dim TotalInsuSuryo As Integer = 0
                'For gousyaIndex As Integer = 0 To BaseListVo.Count - 1

                '2012/09/07　柳沼
                '   号車数で回しているがマトリクスでは間に空白列があり、正しく合計員数が求められない。
                '   最終号車の表示順を求めてその回数分回してあげるように修正。
                Dim intMaxHyoujiJun As Integer = BaseListVo(BaseListVo.Count - 1).HyojijunNo
                For gousyaIndex As Integer = 0 To intMaxHyoujiJun

                    Dim insu As String = dataMatrix(rowindex2, COLUMN_START_GOUSYA + gousyaIndex - 1)
                    If StringUtil.IsEmpty(insu) Then
                        '何もしない'
                    Else
                        If StringUtil.Equals(insu, "**") Then
                            flag = True
                        Else
                            If Not flag Then
                                TotalInsuSuryo += Integer.Parse(insu)
                            End If
                        End If
                    End If
                Next
                If flag Then
                    dataMatrix(rowindex2, COLUMN_TOTAL_INSU_SURYO - 1) = "**"
                Else
                    dataMatrix(rowindex2, COLUMN_TOTAL_INSU_SURYO - 1) = TotalInsuSuryo
                End If
            Next

            xls.CopyRange(0, START_ROW, UBound(dataMatrix, 2) - 1, START_ROW + UBound(dataMatrix), dataMatrix)
        End Sub

        Private Function strNVL(ByVal val As Object) As String
            If val Is Nothing Then
                Return ""
            Else
                Return val.ToString
            End If
        End Function


        ''' <summary>
        ''' Excel出力　試作部品表ボディ部
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub SetBodyOLD(ByVal xls As ShisakuExcel, ByVal BuhinEditListVo As List(Of BuhinEditVoHelper), _
                            ByVal BuhinEditInstlListVo As List(Of BuhinEditInstlVoHelper), _
                            ByVal SekkeiBlockInstlListVo As List(Of TShisakuSekkeiBlockInstlVo), _
                            ByVal EventVo As TShisakuEventVo, _
                            ByVal BaseListVo As List(Of TShisakuEventBaseVo))


            Dim rowIndex As Integer = 0
            Dim MergeFlag As Boolean = False
            Dim wUnitKbn As String = "9"


            For editindex As Integer = 0 To BuhinEditListVo.Count - 1

                If Not editindex = 0 Then
                    If CheckMerge(BuhinEditListVo(editindex), BuhinEditListVo(editindex - 1)) Then

                        rowIndex = rowIndex - 1
                        MergeFlag = True
                    End If
                End If

                ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_al) (TES)張 ADD BEGIN
                ''↓↓2014/09/03 Ⅰ.3.設計編集 ベース改修専用化_al) 酒井 DEL BEGIN
                'If EventVo.BlockAlertKind = 2 Then
                'If BuhinEditListVo(editindex).BaseBuhinFlg = 1 Then
                'xls.SetBackColor(COLUMN_BLOCK_NO, START_ROW + rowIndex, COLUMN_BUHIN_NAME, START_ROW + rowIndex + 1, Color.DimGray)
                'End If
                'End If
                ''↑↑2014/09/03 Ⅰ.3.設計編集 ベース改修専用化_al) 酒井 DEL END
                ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_al) (TES)張 ADD END

                If MergeFlag Then
                    '員数'
                    Dim insu As String = xls.GetValue(COLUMN_START_GOUSYA + BuhinEditListVo(editindex).HyojijunNo, START_ROW + rowIndex)
                    If StringUtil.IsEmpty(insu) Then
                        If BuhinEditListVo(editindex).InsuSuryo < 0 Then
                            xls.SetValue(COLUMN_START_GOUSYA + BuhinEditListVo(editindex).HyojijunNo, START_ROW + rowIndex, "**")
                        Else
                            xls.SetValue(COLUMN_START_GOUSYA + BuhinEditListVo(editindex).HyojijunNo, START_ROW + rowIndex, BuhinEditListVo(editindex).InsuSuryo)
                        End If
                    Else
                        If Not StringUtil.Equals(insu, "**") Then
                            If BuhinEditListVo(editindex).InsuSuryo < 0 Then
                                xls.SetValue(COLUMN_START_GOUSYA + BuhinEditListVo(editindex).HyojijunNo, START_ROW + rowIndex, "**")
                            Else
                                Dim total As Integer = Integer.Parse(insu) + BuhinEditListVo(editindex).InsuSuryo
                                xls.SetValue(COLUMN_START_GOUSYA + BuhinEditListVo(editindex).HyojijunNo, START_ROW + rowIndex, total)
                            End If
                        End If
                    End If
                    rowIndex = rowIndex + 1
                    MergeFlag = False
                Else
                    'ブロックNo'
                    xls.SetValue(COLUMN_BLOCK_NO, START_ROW + rowIndex, BuhinEditListVo(editindex).ShisakuBlockNo)
                    '改訂No'
                    xls.SetValue(COLUMN_KAITEI_NO, START_ROW + rowIndex, BuhinEditListVo(editindex).ShisakuBlockNoKaiteiNo)
                    'レベル'
                    '   2014/02/13　レベルがブランクの場合、ブランクで出力する。
                    '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                    If StringUtil.IsEmpty(BuhinEditListVo(editindex).Level) Then
                        xls.SetValue(COLUMN_LEVEL, START_ROW + rowIndex, "")
                    Else
                        xls.SetValue(COLUMN_LEVEL, START_ROW + rowIndex, BuhinEditListVo(editindex).Level)
                    End If

                    'ユニット区分は試作ブロック情報から取得する。


                    If Not editindex = 0 Then
                        If Not StringUtil.Equals(BuhinEditListVo(editindex).ShisakuBlockNo, BuhinEditListVo(editindex - 1).ShisakuBlockNo) Then
                            wUnitKbn = impl.FindByShisakuBlockNo(BuhinEditListVo(editindex).ShisakuEventCode, _
                                     BuhinEditListVo(editindex).ShisakuBukaCode, _
                                     BuhinEditListVo(editindex).ShisakuBlockNo, _
                                     BuhinEditListVo(editindex).ShisakuBlockNoKaiteiNo)
                        End If
                    Else
                        wUnitKbn = impl.FindByShisakuBlockNo(BuhinEditListVo(editindex).ShisakuEventCode, _
                                     BuhinEditListVo(editindex).ShisakuBukaCode, _
                                     BuhinEditListVo(editindex).ShisakuBlockNo, _
                                     BuhinEditListVo(editindex).ShisakuBlockNoKaiteiNo)
                    End If


                    xls.SetValue(COLUMN_UNIT_KBN, START_ROW + rowIndex, wUnitKbn)
                    'xls.SetValue(COLUMN_UNIT_KBN, START_ROW + rowIndex, EventVo.UnitKbn)

                    '国内集計'
                    xls.SetValue(COLUMN_SHUKEI_CODE, START_ROW + rowIndex, BuhinEditListVo(editindex).ShukeiCode)
                    '海外集計'
                    xls.SetValue(COLUMN_SIA_SHUKEI_CODE, START_ROW + rowIndex, BuhinEditListVo(editindex).SiaShukeiCode)
                    '現調区分'
                    xls.SetValue(COLUMN_GENCYO_KBN, START_ROW + rowIndex, BuhinEditListVo(editindex).GencyoCkdKbn)
                    '取引先コード'
                    xls.SetValue(COLUMN_TORIHIKISAKI_CODE, START_ROW + rowIndex, BuhinEditListVo(editindex).MakerCode)
                    '取引先名称'
                    xls.SetValue(COLUMN_TORIHIKISAKI_NAME, START_ROW + rowIndex, BuhinEditListVo(editindex).MakerName)
                    '部品番号'
                    xls.SetValue(COLUMN_BUHIN_NO, START_ROW + rowIndex, BuhinEditListVo(editindex).BuhinNo)
                    '試作区分'
                    xls.SetValue(COLUMN_SHISAKU_KBN, START_ROW + rowIndex, BuhinEditListVo(editindex).BuhinNoKbn)
                    '改訂'
                    xls.SetValue(COLUMN_KAITEI, START_ROW + rowIndex, BuhinEditListVo(editindex).BuhinNoKaiteiNo)
                    '枝番'
                    xls.SetValue(COLUMN_EDA_BAN, START_ROW + rowIndex, BuhinEditListVo(editindex).EdaBan)
                    '部品名称'
                    xls.SetValue(COLUMN_BUHIN_NAME, START_ROW + rowIndex, BuhinEditListVo(editindex).BuhinName)

                    '再使用不可'
                    xls.SetValue(COLUMN_SAISHIYOUFUKA, START_ROW + rowIndex, BuhinEditListVo(editindex).Saishiyoufuka)
                    '供給セクション'
                    xls.SetValue(COLUMN_KYOUKU_SECTION, START_ROW + rowIndex, BuhinEditListVo(editindex).KyoukuSection)
                    '出図予定日'
                    If StringUtil.Equals(BuhinEditListVo(editindex).ShutuzuYoteiDate, "99999999") Or BuhinEditListVo(editindex).ShutuzuYoteiDate = 0 Then
                        xls.SetValue(COLUMN_SHUTUZUYOTEIBI, START_ROW + rowIndex, "")
                    Else
                        xls.SetValue(COLUMN_SHUTUZUYOTEIBI, START_ROW + rowIndex, BuhinEditListVo(editindex).ShutuzuYoteiDate)
                    End If
                    '材質規格１'
                    xls.SetValue(COLUMN_ZAISHITSU_KIKAKU_1, START_ROW + rowIndex, BuhinEditListVo(editindex).ZaishituKikaku1)
                    '材質規格２'
                    xls.SetValue(COLUMN_ZAISHITSU_KIKAKU_2, START_ROW + rowIndex, BuhinEditListVo(editindex).ZaishituKikaku2)
                    '材質規格３'
                    xls.SetValue(COLUMN_ZAISHITSU_KIKAKU_3, START_ROW + rowIndex, BuhinEditListVo(editindex).ZaishituKikaku3)
                    '材質メッキ'
                    xls.SetValue(COLUMN_ZAISHITSU_MEKKI, START_ROW + rowIndex, BuhinEditListVo(editindex).ZaishituMekki)
                    '板厚'
                    xls.SetValue(COLUMN_BANKO_SURYO, START_ROW + rowIndex, BuhinEditListVo(editindex).ShisakuBankoSuryo)
                    '板厚'
                    xls.SetValue(COLUMN_BANKO_SURYO_U, START_ROW + rowIndex, BuhinEditListVo(editindex).ShisakuBankoSuryoU)
                    '↓↓↓2014/12/30 メタル項目を追加 TES)張 ADD BEGIN
                    xls.SetValue(COLUMN_MATERIAL_INFO_LENGTH, START_ROW + rowIndex, BuhinEditListVo(editindex).MaterialInfoLength)
                    xls.SetValue(COLUMN_MATERIAL_INFO_WIDTH, START_ROW + rowIndex, BuhinEditListVo(editindex).MaterialInfoWidth)
                    xls.SetValue(COLUMN_DATA_ITEM_KAITEI_NO, START_ROW + rowIndex, BuhinEditListVo(editindex).DataItemKaiteiNo)
                    xls.SetValue(COLUMN_DATA_ITEM_AREA_NAME, START_ROW + rowIndex, BuhinEditListVo(editindex).DataItemAreaName)
                    xls.SetValue(COLUMN_DATA_ITEM_SET_NAME, START_ROW + rowIndex, BuhinEditListVo(editindex).DataItemSetName)
                    xls.SetValue(COLUMN_DATA_ITEM_KAITEI_INFO, START_ROW + rowIndex, BuhinEditListVo(editindex).DataItemKaiteiInfo)
                    '↑↑↑2014/12/30 メタル項目を追加 TES)張 ADD END
                    '試作部品費'
                    xls.SetValue(COLUMN_SHISAKU_BUHIN_HI, START_ROW + rowIndex, BuhinEditListVo(editindex).ShisakuBuhinHi)
                    '試作型費'
                    xls.SetValue(COLUMN_SHISAKU_KATA_HI, START_ROW + rowIndex, BuhinEditListVo(editindex).ShisakuKataHi)
                    'NOTE'
                    xls.SetValue(COLUMN_BUHIN_NOTE, START_ROW + rowIndex, BuhinEditListVo(editindex).BuhinNote)
                    '備考'
                    xls.SetValue(COLUMN_BIKOU, START_ROW + rowIndex, BuhinEditListVo(editindex).Bikou)

                    '員数'
                    Dim insu As String = xls.GetValue(COLUMN_START_GOUSYA + BuhinEditListVo(editindex).HyojijunNo, START_ROW + rowIndex)
                    If StringUtil.IsEmpty(insu) Then
                        If BuhinEditListVo(editindex).InsuSuryo < 0 Then
                            xls.SetValue(COLUMN_START_GOUSYA + BuhinEditListVo(editindex).HyojijunNo, START_ROW + rowIndex, "**")
                        Else
                            xls.SetValue(COLUMN_START_GOUSYA + BuhinEditListVo(editindex).HyojijunNo, START_ROW + rowIndex, BuhinEditListVo(editindex).InsuSuryo)
                        End If
                    Else
                        If Not StringUtil.Equals(insu, "**") Then
                            If BuhinEditListVo(editindex).InsuSuryo < 0 Then
                                xls.SetValue(COLUMN_START_GOUSYA + BuhinEditListVo(editindex).HyojijunNo, START_ROW + rowIndex, "**")
                            Else
                                insu = insu + BuhinEditListVo(editindex).InsuSuryo
                                xls.SetValue(COLUMN_START_GOUSYA + BuhinEditListVo(editindex).HyojijunNo, START_ROW + rowIndex, insu)
                            End If
                        End If

                    End If

                    ''↓↓2014/09/03 Ⅰ.3.設計編集 ベース改修専用化_al) 酒井 ADD BEGIN
                    If EventVo.BlockAlertKind = 2 Then
                        If BuhinEditListVo(editindex).BaseBuhinFlg = 1 Then
                            xls.SetBackColor(COLUMN_BLOCK_NO, START_ROW + rowIndex, COLUMN_START_GOUSYA + MaxBaseHyoujiJun, START_ROW + rowIndex, RGB(169, 169, 169))
                        End If
                    End If
                    ''↑↑2014/09/03 Ⅰ.3.設計編集 ベース改修専用化_al) 酒井 ADD END

                    rowIndex = rowIndex + 1

                End If

            Next

            '合計員数を取得する'
            For rowindex2 As Integer = 0 To rowIndex
                Dim flag As Boolean = False
                Dim TotalInsuSuryo As Integer = 0
                'For gousyaIndex As Integer = 0 To BaseListVo.Count - 1
                For gousyaIndex As Integer = 0 To BaseListVo.Count - 1

                    Dim insu As String = xls.GetValue(COLUMN_START_GOUSYA + gousyaIndex, START_ROW + rowindex2)
                    If StringUtil.IsEmpty(insu) Then
                        '何もしない'
                    Else
                        If StringUtil.Equals(insu, "**") Then
                            flag = True
                        Else
                            If Not flag Then
                                TotalInsuSuryo = TotalInsuSuryo + Integer.Parse(insu)
                            End If
                        End If
                    End If
                Next
                If flag Then
                    xls.SetValue(COLUMN_TOTAL_INSU_SURYO, START_ROW + rowindex2, "**")
                Else
                    xls.SetValue(COLUMN_TOTAL_INSU_SURYO, START_ROW + rowindex2, TotalInsuSuryo)
                End If
            Next

        End Sub
        ''' <summary>
        ''' 基本情報に紐付く号車情報を検索
        ''' </summary>
        ''' <param name="aBuhinKaiteiVo">部品編集改訂情報</param>
        ''' <param name="aGousyaKaiteiVo">部品編集号車改訂情報</param>
        ''' <returns>基本情報に紐付いた号車情報であればTrue</returns>
        ''' <remarks></remarks>
        Private Function CheckGousyaKihon(ByVal aBuhinKaiteiVo As TShisakuBuhinEditVo, ByVal aGousyaKaiteiVo As TShisakuBuhinEditInstlVo) As Boolean
            If Not StringUtil.Equals(aBuhinKaiteiVo.ShisakuBukaCode, aGousyaKaiteiVo.ShisakuBukaCode) Then
                Return False
            End If
            If Not StringUtil.Equals(aBuhinKaiteiVo.ShisakuBlockNo, aGousyaKaiteiVo.ShisakuBlockNo) Then
                Return False
            End If
            If Not StringUtil.Equals(aBuhinKaiteiVo.BuhinNoHyoujiJun, aGousyaKaiteiVo.BuhinNoHyoujiJun) Then
                Return False
            End If
            Return True
        End Function

        ''' <summary>
        ''' 各カラム名に数値を設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetColumnNo()
            Dim column As Integer = 1

            COLUMN_BLOCK_NO = EzUtil.Increment(column)
            COLUMN_KAITEI_NO = EzUtil.Increment(column)
            COLUMN_LEVEL = EzUtil.Increment(column)
            COLUMN_UNIT_KBN = EzUtil.Increment(column)
            COLUMN_SHUKEI_CODE = EzUtil.Increment(column)
            COLUMN_SIA_SHUKEI_CODE = EzUtil.Increment(column)
            COLUMN_GENCYO_KBN = EzUtil.Increment(column)
            COLUMN_TORIHIKISAKI_CODE = EzUtil.Increment(column)
            COLUMN_TORIHIKISAKI_NAME = EzUtil.Increment(column)
            COLUMN_BUHIN_NO = EzUtil.Increment(column)
            COLUMN_SHISAKU_KBN = EzUtil.Increment(column)
            COLUMN_KAITEI = EzUtil.Increment(column)
            COLUMN_EDA_BAN = EzUtil.Increment(column)
            COLUMN_BUHIN_NAME = EzUtil.Increment(column)
            COLUMN_TOTAL_INSU_SURYO = EzUtil.Increment(column)
            COLUMN_SAISHIYOUFUKA = EzUtil.Increment(column)
            COLUMN_KYOUKU_SECTION = EzUtil.Increment(column)
            COLUMN_SHUTUZUYOTEIBI = EzUtil.Increment(column)
            ''↓↓2014/07/25 Ⅰ.2.管理項目追加_bd) (TES)張 ADD BEGIN
            COLUMN_TSUKURIKATA_SEISAKU = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KATASHIYOU1 = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KATASHIYOU2 = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KATASHIYOU3 = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_TIGU = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_NOUNYU = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KIBO = EzUtil.Increment(column)
            ''↑↑2014/07/25 Ⅰ.2.管理項目追加_bd) (TES)張 ADD END
            COLUMN_ZAISHITSU_KIKAKU_1 = EzUtil.Increment(column)
            COLUMN_ZAISHITSU_KIKAKU_2 = EzUtil.Increment(column)
            COLUMN_ZAISHITSU_KIKAKU_3 = EzUtil.Increment(column)
            COLUMN_ZAISHITSU_MEKKI = EzUtil.Increment(column)
            COLUMN_BANKO_SURYO = EzUtil.Increment(column)
            COLUMN_BANKO_SURYO_U = EzUtil.Increment(column)
            '↓↓↓2014/12/30 メタル項目を追加 TES)張 ADD BEGIN
            COLUMN_MATERIAL_INFO_LENGTH = EzUtil.Increment(column)
            COLUMN_MATERIAL_INFO_WIDTH = EzUtil.Increment(column)
            COLUMN_DATA_ITEM_KAITEI_NO = EzUtil.Increment(column)
            COLUMN_DATA_ITEM_AREA_NAME = EzUtil.Increment(column)
            COLUMN_DATA_ITEM_SET_NAME = EzUtil.Increment(column)
            COLUMN_DATA_ITEM_KAITEI_INFO = EzUtil.Increment(column)
            '↑↑↑2014/12/30 メタル項目を追加 TES)張 ADD END
            COLUMN_SHISAKU_BUHIN_HI = EzUtil.Increment(column)
            COLUMN_SHISAKU_KATA_HI = EzUtil.Increment(column)
            COLUMN_BUHIN_NOTE = EzUtil.Increment(column)
            COLUMN_BIKOU = EzUtil.Increment(column)
            COLUMN_START_GOUSYA = EzUtil.Increment(column)

        End Sub

        ''' <summary>
        ''' 各カラム名に数値を設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetIdxColumnNo()
            Dim column As Integer = 0

            IDX_COLUMN_BLOCK_NO = EzUtil.Increment(column)
            IDX_COLUMN_KAITEI_NO = EzUtil.Increment(column)
            IDX_COLUMN_LEVEL = EzUtil.Increment(column)
            IDX_COLUMN_UNIT_KBN = EzUtil.Increment(column)
            IDX_COLUMN_SHUKEI_CODE = EzUtil.Increment(column)
            IDX_COLUMN_SIA_SHUKEI_CODE = EzUtil.Increment(column)
            IDX_COLUMN_GENCYO_KBN = EzUtil.Increment(column)
            IDX_COLUMN_TORIHIKISAKI_CODE = EzUtil.Increment(column)
            IDX_COLUMN_TORIHIKISAKI_NAME = EzUtil.Increment(column)
            IDX_COLUMN_BUHIN_NO = EzUtil.Increment(column)
            IDX_COLUMN_SHISAKU_KBN = EzUtil.Increment(column)
            IDX_COLUMN_KAITEI = EzUtil.Increment(column)
            IDX_COLUMN_EDA_BAN = EzUtil.Increment(column)
            IDX_COLUMN_BUHIN_NAME = EzUtil.Increment(column)
            IDX_COLUMN_TOTAL_INSU_SURYO = EzUtil.Increment(column)
            IDX_COLUMN_SAISHIYOUFUKA = EzUtil.Increment(column)
            IDX_COLUMN_KYOUKU_SECTION = EzUtil.Increment(column)
            IDX_COLUMN_SHUTUZUYOTEIBI = EzUtil.Increment(column)
            ''↓↓2014/07/25 Ⅰ.2.管理項目追加_bd) (TES)張 ADD BEGIN
            IDX_COLUMN_TSUKURIKATA_SEISAKU = EzUtil.Increment(column)
            IDX_COLUMN_TSUKURIKATA_KATASHIYOU1 = EzUtil.Increment(column)
            IDX_COLUMN_TSUKURIKATA_KATASHIYOU2 = EzUtil.Increment(column)
            IDX_COLUMN_TSUKURIKATA_KATASHIYOU3 = EzUtil.Increment(column)
            IDX_COLUMN_TSUKURIKATA_TIGU = EzUtil.Increment(column)
            IDX_COLUMN_TSUKURIKATA_NOUNYU = EzUtil.Increment(column)
            IDX_COLUMN_TSUKURIKATA_KIBO = EzUtil.Increment(column)
            ''↑↑2014/07/25 Ⅰ.2.管理項目追加_bd) (TES)張 ADD END
            IDX_COLUMN_ZAISHITSU_KIKAKU_1 = EzUtil.Increment(column)
            IDX_COLUMN_ZAISHITSU_KIKAKU_2 = EzUtil.Increment(column)
            IDX_COLUMN_ZAISHITSU_KIKAKU_3 = EzUtil.Increment(column)
            IDX_COLUMN_ZAISHITSU_MEKKI = EzUtil.Increment(column)
            IDX_COLUMN_BANKO_SURYO = EzUtil.Increment(column)
            IDX_COLUMN_BANKO_SURYO_U = EzUtil.Increment(column)
            '↓↓↓2014/12/30 メタル項目を追加 TES)張 ADD BEGIN
            IDX_COLUMN_MATERIAL_INFO_LENGTH = EzUtil.Increment(column)
            IDX_COLUMN_MATERIAL_INFO_WIDTH = EzUtil.Increment(column)
            IDX_COLUMN_DATA_ITEM_KAITEI_NO = EzUtil.Increment(column)
            IDX_COLUMN_DATA_ITEM_AREA_NAME = EzUtil.Increment(column)
            IDX_COLUMN_DATA_ITEM_SET_NAME = EzUtil.Increment(column)
            IDX_COLUMN_DATA_ITEM_KAITEI_INFO = EzUtil.Increment(column)
            '↑↑↑2014/12/30 メタル項目を追加 TES)張 ADD END
            IDX_COLUMN_SHISAKU_BUHIN_HI = EzUtil.Increment(column)
            IDX_COLUMN_SHISAKU_KATA_HI = EzUtil.Increment(column)
            IDX_COLUMN_BUHIN_NOTE = EzUtil.Increment(column)
            IDX_COLUMN_BIKOU = EzUtil.Increment(column)
            IDX_COLUMN_START_GOUSYA = EzUtil.Increment(column)

        End Sub

        ''' <summary>
        ''' 合計員数を計算
        ''' </summary>
        ''' <param name="aBuhinKaiteiVo">部品編集改訂情報</param>
        ''' <param name="aGousyaKaiteiVo">部品編集号車改訂情報</param>
        ''' <returns>員数</returns>
        ''' <remarks></remarks>
        Private Function GetTotalInsuSuryo(ByVal aBuhinKaiteiVo As TShisakuBuhinEditVo, ByVal aGousyaKaiteiVo As TShisakuBuhinEditInstlVo) As Integer
            '部品編集の部品番号表示順'
            Dim result As Integer = 0
            If StringUtil.Equals(aBuhinKaiteiVo.ShisakuBukaCode, aGousyaKaiteiVo.ShisakuBukaCode) Then
                If StringUtil.Equals(aBuhinKaiteiVo.ShisakuBlockNo, aGousyaKaiteiVo.ShisakuBlockNo) Then
                    If StringUtil.Equals(aBuhinKaiteiVo.BuhinNoHyoujiJun, aGousyaKaiteiVo.BuhinNoHyoujiJun) Then

                        result = result + aGousyaKaiteiVo.InsuSuryo

                    End If
                End If
            End If

            Return result
        End Function

        ''' <summary>
        ''' マージするか計算
        ''' </summary>
        ''' <param name="NowBuhinEditVo">現在の部品編集情報</param>
        ''' <param name="BeforeBuhinEditVo">直前の部品編集情報</param>
        ''' <returns>マージ可能ならTrue</returns>
        ''' <remarks></remarks>
        Private Function CheckMerge(ByVal NowBuhinEditVo As BuhinEditVoHelper, ByVal BeforeBuhinEditVo As BuhinEditVoHelper) As Boolean

            'ブロックNo'
            If Not StringUtil.Equals(NowBuhinEditVo.ShisakuBlockNo, BeforeBuhinEditVo.ShisakuBlockNo) Then
                Return False
            End If
            '--------------------------------------------------------------------------------------------
            'レベル’
            '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
            '               →試作部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
            Dim NowBuhinEditVoLevel As String
            Dim BeforeBuhinEditVoLevel As String
            If StringUtil.IsEmpty(NowBuhinEditVo.Level) Then
                NowBuhinEditVoLevel = ""
            Else
                NowBuhinEditVoLevel = NowBuhinEditVo.Level
            End If
            If StringUtil.IsEmpty(BeforeBuhinEditVo.Level) Then
                BeforeBuhinEditVoLevel = ""
            Else
                BeforeBuhinEditVoLevel = BeforeBuhinEditVo.Level
            End If
            If Not StringUtil.Equals(NowBuhinEditVoLevel, BeforeBuhinEditVoLevel) Then
                Return False
            End If
            '--------------------------------------------------------------------------------------------

            '集計コード'
            If StringUtil.IsNotEmpty(NowBuhinEditVo.ShukeiCode) Then
                If StringUtil.IsNotEmpty(BeforeBuhinEditVo.ShukeiCode) Then
                    If Not StringUtil.Equals(NowBuhinEditVo.ShukeiCode, BeforeBuhinEditVo.ShukeiCode) Then
                        Return False
                    End If
                Else
                    Return False
                End If
            End If

            '海外集計コード'
            If StringUtil.IsNotEmpty(NowBuhinEditVo.SiaShukeiCode) Then
                If StringUtil.IsNotEmpty(BeforeBuhinEditVo.SiaShukeiCode) Then
                    If Not StringUtil.Equals(NowBuhinEditVo.SiaShukeiCode, BeforeBuhinEditVo.SiaShukeiCode) Then
                        Return False
                    End If
                Else
                    Return False
                End If
            End If

            '供給セクション'
            If StringUtil.IsNotEmpty(NowBuhinEditVo.KyoukuSection) Then
                If StringUtil.IsNotEmpty(BeforeBuhinEditVo.KyoukuSection) Then
                    If Not StringUtil.Equals(NowBuhinEditVo.KyoukuSection, BeforeBuhinEditVo.KyoukuSection) Then
                        Return False
                    End If
                Else
                    Return False
                End If
            End If

            '再使用不可区分'
            If Not StringUtil.Equals(NowBuhinEditVo.Saishiyoufuka, BeforeBuhinEditVo.Saishiyoufuka) Then
                Return False
            End If

            '部品番号'
            If Not StringUtil.Equals(NowBuhinEditVo.BuhinNo, BeforeBuhinEditVo.BuhinNo) Then
                Return False
            End If

            '2012/03/08 圧縮条件に品番区分追加（No.212対応）
            '部品番号区分'
            If Not StringUtil.Equals(NowBuhinEditVo.BuhinNoKbn, BeforeBuhinEditVo.BuhinNoKbn) Then
                Return False
            End If
            If EventVo.BlockAlertKind = 2 And EventVo.KounyuShijiFlg = "0" Then
                If Not StringUtil.Equals(NowBuhinEditVo.BaseInstlFlg, BeforeBuhinEditVo.BaseInstlFlg) Then
                    Return False
                End If
            End If
            Return True
        End Function



        ''' <summary>
        ''' 行と列の調整
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub SetColumnRow(ByVal xls As ShisakuExcel)

            xls.AutoFitCol(COLUMN_BLOCK_NO, xls.EndCol)
            xls.SetRowHeight(TITLE_ROW, TITLE_ROW, 104)
            xls.SetColWidth(1, 1, 7)
            xls.SetColWidth(5, 5, 3)
            xls.SetColWidth(9, 9, 20)
        End Sub

#Region "各列タグの設定"
        '' ブロックNo
        Private COLUMN_BLOCK_NO As Integer
        '' 改訂No
        Private COLUMN_KAITEI_NO As Integer
        '' レベル
        Private COLUMN_LEVEL As Integer
        '' ユニット区分
        Private COLUMN_UNIT_KBN As Integer
        '' 国内集計
        Private COLUMN_SHUKEI_CODE As Integer
        '' 海外集計
        Private COLUMN_SIA_SHUKEI_CODE As Integer
        '' 現調区分
        Private COLUMN_GENCYO_KBN As Integer
        '' 取引先コード
        Private COLUMN_TORIHIKISAKI_CODE As Integer
        '' 取引先名称
        Private COLUMN_TORIHIKISAKI_NAME As Integer
        '' 部品番号
        Private COLUMN_BUHIN_NO As Integer
        '' 試作区分
        Private COLUMN_SHISAKU_KBN As Integer
        '' 改訂
        Private COLUMN_KAITEI As Integer
        '' 枝番
        Private COLUMN_EDA_BAN As Integer
        '' 部品名称
        Private COLUMN_BUHIN_NAME As Integer
        '' 合計員数
        Private COLUMN_TOTAL_INSU_SURYO As Integer
        '' 再使用不可
        Private COLUMN_SAISHIYOUFUKA As Integer
        '' 供給セクション
        Private COLUMN_KYOUKU_SECTION As Integer
        '' 出図予定日
        Private COLUMN_SHUTUZUYOTEIBI As Integer
        ''↓↓2014/07/25 Ⅰ.2.管理項目追加_bc) (TES)張 ADD BEGIN
        '作り方'
        '制作方法'
        Private COLUMN_TSUKURIKATA_SEISAKU As Integer
        '型仕様１'
        Private COLUMN_TSUKURIKATA_KATASHIYOU1 As Integer
        '型仕様２'
        Private COLUMN_TSUKURIKATA_KATASHIYOU2 As Integer
        '型仕様３'
        Private COLUMN_TSUKURIKATA_KATASHIYOU3 As Integer
        '治具'
        Private COLUMN_TSUKURIKATA_TIGU As Integer
        '納入見通り'
        Private COLUMN_TSUKURIKATA_NOUNYU As Integer
        '部品制作規模・概要'
        Private COLUMN_TSUKURIKATA_KIBO As Integer
        ''↑↑2014/07/25 Ⅰ.2.管理項目追加_bc) (TES)張 ADD END
        '' 材質規格１
        Private COLUMN_ZAISHITSU_KIKAKU_1 As Integer
        '' 材質規格２
        Private COLUMN_ZAISHITSU_KIKAKU_2 As Integer
        '' 材質規格３
        Private COLUMN_ZAISHITSU_KIKAKU_3 As Integer
        '' 材質メッキ
        Private COLUMN_ZAISHITSU_MEKKI As Integer
        '' 板厚数量
        Private COLUMN_BANKO_SURYO As Integer
        '' 板厚数量u
        Private COLUMN_BANKO_SURYO_U As Integer
        '' 試作部品費
        Private COLUMN_SHISAKU_BUHIN_HI As Integer
        '' 試作型費
        Private COLUMN_SHISAKU_KATA_HI As Integer
        '' NOTE
        Private COLUMN_BUHIN_NOTE As Integer
        '' 備考
        Private COLUMN_BIKOU As Integer
        '' 号車開始列
        Private COLUMN_START_GOUSYA As Integer

        '↓↓↓2014/12/30 メタル項目を追加 TES)張 ADD BEGIN
        Private COLUMN_MATERIAL_INFO_LENGTH As Integer
        Private COLUMN_MATERIAL_INFO_WIDTH As Integer
        Private COLUMN_DATA_ITEM_KAITEI_NO As Integer
        Private COLUMN_DATA_ITEM_AREA_NAME As Integer
        Private COLUMN_DATA_ITEM_SET_NAME As Integer
        Private COLUMN_DATA_ITEM_KAITEI_INFO As Integer
        '↑↑↑2014/12/30 メタル項目を追加 TES)張 ADD END

#End Region

#Region "各列タグの設定"
        '' ブロックNo
        Private IDX_COLUMN_BLOCK_NO As Integer
        '' 改訂No
        Private IDX_COLUMN_KAITEI_NO As Integer
        '' レベル
        Private IDX_COLUMN_LEVEL As Integer
        '' ユニット区分
        Private IDX_COLUMN_UNIT_KBN As Integer
        '' 国内集計
        Private IDX_COLUMN_SHUKEI_CODE As Integer
        '' 海外集計
        Private IDX_COLUMN_SIA_SHUKEI_CODE As Integer
        '' 現調区分
        Private IDX_COLUMN_GENCYO_KBN As Integer
        '' 取引先コード
        Private IDX_COLUMN_TORIHIKISAKI_CODE As Integer
        '' 取引先名称
        Private IDX_COLUMN_TORIHIKISAKI_NAME As Integer
        '' 部品番号
        Private IDX_COLUMN_BUHIN_NO As Integer
        '' 試作区分
        Private IDX_COLUMN_SHISAKU_KBN As Integer
        '' 改訂
        Private IDX_COLUMN_KAITEI As Integer
        '' 枝番
        Private IDX_COLUMN_EDA_BAN As Integer
        '' 部品名称
        Private IDX_COLUMN_BUHIN_NAME As Integer
        '' 合計員数
        Private IDX_COLUMN_TOTAL_INSU_SURYO As Integer
        '' 再使用不可
        Private IDX_COLUMN_SAISHIYOUFUKA As Integer
        '' 供給セクション
        Private IDX_COLUMN_KYOUKU_SECTION As Integer
        '' 出図予定日
        Private IDX_COLUMN_SHUTUZUYOTEIBI As Integer
        ''↓↓2014/07/25 Ⅰ.2.管理項目追加_bc) (TES)張 ADD BEGIN
        '作り方'
        '制作方法'
        Private IDX_COLUMN_TSUKURIKATA_SEISAKU As Integer
        '型仕様１'
        Private IDX_COLUMN_TSUKURIKATA_KATASHIYOU1 As Integer
        '型仕様２'
        Private IDX_COLUMN_TSUKURIKATA_KATASHIYOU2 As Integer
        '型仕様３'
        Private IDX_COLUMN_TSUKURIKATA_KATASHIYOU3 As Integer
        '治具'
        Private IDX_COLUMN_TSUKURIKATA_TIGU As Integer
        '納入見通り'
        Private IDX_COLUMN_TSUKURIKATA_NOUNYU As Integer
        '部品制作規模・概要'
        Private IDX_COLUMN_TSUKURIKATA_KIBO As Integer
        ''↑↑2014/07/25 Ⅰ.2.管理項目追加_bc) (TES)張 ADD END
        '' 材質規格１
        Private IDX_COLUMN_ZAISHITSU_KIKAKU_1 As Integer
        '' 材質規格２
        Private IDX_COLUMN_ZAISHITSU_KIKAKU_2 As Integer
        '' 材質規格３
        Private IDX_COLUMN_ZAISHITSU_KIKAKU_3 As Integer
        '' 材質メッキ
        Private IDX_COLUMN_ZAISHITSU_MEKKI As Integer
        '' 板厚数量
        Private IDX_COLUMN_BANKO_SURYO As Integer
        '' 板厚数量u
        Private IDX_COLUMN_BANKO_SURYO_U As Integer
        '' 試作部品費
        Private IDX_COLUMN_SHISAKU_BUHIN_HI As Integer
        '' 試作型費
        Private IDX_COLUMN_SHISAKU_KATA_HI As Integer
        '' NOTE
        Private IDX_COLUMN_BUHIN_NOTE As Integer
        '' 備考
        Private IDX_COLUMN_BIKOU As Integer
        '' 号車開始列
        Private IDX_COLUMN_START_GOUSYA As Integer

        '↓↓↓2014/12/30 メタル項目を追加 TES)張 ADD BEGIN
        Private IDX_COLUMN_MATERIAL_INFO_LENGTH As Integer
        Private IDX_COLUMN_MATERIAL_INFO_WIDTH As Integer
        Private IDX_COLUMN_DATA_ITEM_KAITEI_NO As Integer
        Private IDX_COLUMN_DATA_ITEM_AREA_NAME As Integer
        Private IDX_COLUMN_DATA_ITEM_SET_NAME As Integer
        Private IDX_COLUMN_DATA_ITEM_KAITEI_INFO As Integer
        '↑↑↑2014/12/30 メタル項目を追加 TES)張 ADD END

#End Region



#Region "各行のタグ設定"
        '' タイトル行
        Private TITLE_ROW As Integer = 4
        '' 書き込み開始行
        Private START_ROW As Integer = 6
#End Region

    End Class
End Namespace