Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports ShisakuCommon
Imports EBom.Common
Imports EBom.Excel
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports YakanSekkeiTenakai.Vo
Imports YakanSekkeiTenakai.Dao
Imports EventSakusei

Namespace ExcelProcess
    Public Class ExportYakanSekkeiBlockExcel
        '↓↓2014/10/10 酒井 ADD BEGIN
        Private EventEbomKanshiVos As List(Of TShisakuEventEbomKanshiVo)
        '↑↑2014/10/10 酒井 ADD END

        ''' <summary>
        ''' Excel出力
        ''' </summary>
        ''' <param name="strEventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal strEventCode As String, ByVal diffVos As List(Of YakanSekkeiBlockVoHelperExcel), ByVal sameVos As List(Of YakanSekkeiBlockVoHelperExcel))

            Dim fileName1 As String = Nothing
            Dim fileName2 As String
            Dim yakanSekkeiTenkaiDao As New YakanSekkeiTenkaiDaoImpl()
            Dim shisakiEventVo As TShisakuEventVo = yakanSekkeiTenkaiDao.FindByEvent(strEventCode)

            '↓↓2014/10/10 酒井 ADD BEGIN
            EventEbomKanshiVos = yakanSekkeiTenkaiDao.FindByGousya(strEventCode)
            '↑↑2014/10/10 酒井 ADD END

            '--------------------------------------------------
            'EXCEL出力先フォルダチェック
            ShisakuCommon.ShisakuComFunc.ExcelOutPutDirCheck()
            '--------------------------------------------------

            ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 ay) 酒井 ADD BEGIN
            If diffVos.Count > 0 Then
                ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 ay) 酒井 ADD END
                '差異ありファイル
                fileName1 = shisakiEventVo.ShisakuKaihatsuFugo + shisakiEventVo.ShisakuEventName + " " + "ＥＢＯＭ差分 " + Now.ToString("MMdd") + " " + Now.ToString("HHmm") + ".xls"
                fileName1 = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + StringUtil.ReplaceInvalidCharForFileName(fileName1)
                If Not ShisakuComFunc.IsFileOpen(ShisakuCommon.ShisakuGlobal.ExcelOutPut) Then
                    Using xls As New ShisakuExcel(fileName1)
                        xls.OpenBook(fileName1)
                        xls.ClearWorkBook()
                        xls.SetFont("ＭＳ Ｐゴシック", 11)
                        SetYakanSekkeiBlockExcel(xls, diffVos, shisakiEventVo)
                        'A4横で印刷できるように変更'
                        xls.PrintPaper(fileName1, 1, "A4")
                        xls.PrintOrientation(fileName1, 1, 1, False)
                        xls.SetActiveSheet(1)
                        xls.Save()
                        xls.Dispose()
                    End Using
                End If

                ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 ay) 酒井 ADD BEGIN
            Else
                ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 ay) 酒井 ADD END
                '差異なしファイル
                fileName2 = shisakiEventVo.ShisakuKaihatsuFugo + shisakiEventVo.ShisakuEventName + " " + "ＥＢＯＭ差分 " + Now.ToString("MMdd") + " " + Now.ToString("HHmm") + " 差異なし.xls"
                ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 ay) 酒井 ADD BEGIN
                'fileName2 = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + StringUtil.ReplaceInvalidCharForFileName(fileName1)
                fileName2 = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + StringUtil.ReplaceInvalidCharForFileName(fileName2)
                ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 ay) 酒井 ADD END
                If Not ShisakuComFunc.IsFileOpen(ShisakuCommon.ShisakuGlobal.ExcelOutPut) Then
                    Using xls As New ShisakuExcel(fileName2)
                        xls.OpenBook(fileName2)
                        xls.ClearWorkBook()
                        xls.SetFont("ＭＳ Ｐゴシック", 11)
                        SetYakanSekkeiBlockExcel(xls, sameVos, shisakiEventVo)
                        'A4横で印刷できるように変更'
                        xls.PrintPaper(fileName1, 1, "A4")
                        xls.PrintOrientation(fileName1, 1, 1, False)
                        xls.SetActiveSheet(1)
                        xls.Save()
                        xls.Dispose()
                    End Using
                End If

                ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 ay) 酒井 ADD BEGIN
            End If
            ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 ay) 酒井 ADD END

        End Sub

        ''' <summary>
        ''' Excel出力　試作部品表
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub SetYakanSekkeiBlockExcel(ByVal xls As ShisakuExcel, ByVal BuhinEditListVo As List(Of YakanSekkeiBlockVoHelperExcel), ByVal EventVo As TShisakuEventVo)

            '列の設定'
            SetColumnNo()
            'ヘッダー部'
            SetHeader(xls, EventVo)
            'タイトル部'
            SetTitle(xls)
            'ボディ部'
            SetBody(xls, BuhinEditListVo)

            '列と行の調整'
            SetColumnRow(xls)


        End Sub

        ''' <summary>
        ''' 各カラム名に数値を設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetColumnNo()
            Dim column As Integer = 1

            COLUMN_SYMBOL = EzUtil.Increment(column)
            '↓↓2014/10/10 酒井 ADD BEGIN
            '            COLUMN_GOUSYA = EzUtil.Increment(column)
            '↑↑2014/10/10 酒井 ADD END
            COLUMN_BLOCK_NO = EzUtil.Increment(column)
            COLUMN_BUHIN_NO = EzUtil.Increment(column)
            COLUMN_SEKKEIKA = EzUtil.Increment(column)
            COLUMN_STSR_DHSTBA_NO = EzUtil.Increment(column)
            COLUMN_LEVEL = EzUtil.Increment(column)
            COLUMN_SHUKEI_CODE = EzUtil.Increment(column)
            COLUMN_SIA_SHUKEI_CODE = EzUtil.Increment(column)
            COLUMN_GENCYO_KBN = EzUtil.Increment(column)
            COLUMN_TORIHIKISAKI_CODE = EzUtil.Increment(column)
            COLUMN_TORIHIKISAKI_NAME = EzUtil.Increment(column)
            COLUMN_SHISAKU_KBN = EzUtil.Increment(column)
            COLUMN_KAITEI = EzUtil.Increment(column)
            COLUMN_EDA_BAN = EzUtil.Increment(column)
            COLUMN_BUHIN_NAME = EzUtil.Increment(column)
            '↓↓2014/10/10 酒井 ADD BEGIN
            'COLUMN_TOTAL_INSU_SURYO = EzUtil.Increment(column)
            '↑↑2014/10/10 酒井 ADD END
            COLUMN_SAISHIYOUFUKA = EzUtil.Increment(column)
            COLUMN_KYOUKU_SECTION = EzUtil.Increment(column)
            COLUMN_SHUTUZUYOTEIBI = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_SEISAKU = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KATASHIYOU1 = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KATASHIYOU2 = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KATASHIYOU3 = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_TIGU = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_NOUNYU = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KIBO = EzUtil.Increment(column)
            COLUMN_ZAIRYO_KIJUTSU = EzUtil.Increment(column)
            COLUMN_BANKO_SURYO = EzUtil.Increment(column)
            COLUMN_SHISAKU_BUHIN_HI = EzUtil.Increment(column)
            COLUMN_SHISAKU_KATA_HI = EzUtil.Increment(column)
            COLUMN_BUHIN_NOTE = EzUtil.Increment(column)
            COLUMN_BIKOU = EzUtil.Increment(column)
            COLUMN_TANTOSYA_NAME = EzUtil.Increment(column)
            COLUMN_TEL = EzUtil.Increment(column)
            '↓↓2014/10/10 酒井 ADD BEGIN
            COLUMN_TOTAL_INSU_SURYO = EzUtil.Increment(column)
            '↑↑2014/10/10 酒井 ADD END
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
        Private Sub SetTitle(ByVal xls As ShisakuExcel)

            'シンボル'
            xls.MergeCells(COLUMN_SYMBOL, TITLE_ROW, COLUMN_SYMBOL, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_SYMBOL, TITLE_ROW, COLUMN_SYMBOL, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_SYMBOL, TITLE_ROW, "シンボル")
            '↓↓2014/10/10 酒井 ADD BEGIN
            ''号車'
            'xls.MergeCells(COLUMN_GOUSYA, TITLE_ROW, COLUMN_GOUSYA, TITLE_ROW + 1, True)
            'xls.SetOrientation(COLUMN_GOUSYA, TITLE_ROW, COLUMN_GOUSYA, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            'xls.SetValue(COLUMN_GOUSYA, TITLE_ROW, "号車")
            '↑↑2014/10/10 酒井 ADD END
            'ブロックNo'
            xls.MergeCells(COLUMN_BLOCK_NO, TITLE_ROW, COLUMN_BLOCK_NO, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_BLOCK_NO, TITLE_ROW, COLUMN_BLOCK_NO, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_BLOCK_NO, TITLE_ROW, "ブロックNo")
            '部品番号'
            xls.MergeCells(COLUMN_BUHIN_NO, TITLE_ROW, COLUMN_BUHIN_NO, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_BUHIN_NO, TITLE_ROW, COLUMN_BUHIN_NO, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_BUHIN_NO, TITLE_ROW, "部品番号")
            '設計課'
            xls.MergeCells(COLUMN_SEKKEIKA, TITLE_ROW, COLUMN_SEKKEIKA, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_SEKKEIKA, TITLE_ROW, COLUMN_SEKKEIKA, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_SEKKEIKA, TITLE_ROW, "設計課")
            '設通No'
            xls.MergeCells(COLUMN_STSR_DHSTBA_NO, TITLE_ROW, COLUMN_STSR_DHSTBA_NO, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_STSR_DHSTBA_NO, TITLE_ROW, COLUMN_STSR_DHSTBA_NO, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_STSR_DHSTBA_NO, TITLE_ROW, "設通No")
            'レベル'
            xls.MergeCells(COLUMN_LEVEL, TITLE_ROW, COLUMN_LEVEL, TITLE_ROW + 1, True)
            xls.SetOrientation(COLUMN_LEVEL, TITLE_ROW, COLUMN_LEVEL, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_LEVEL, TITLE_ROW, "レベル")
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
            '↓↓2014/10/10 酒井 ADD BEGIN
            ''合計員数'
            'xls.MergeCells(COLUMN_TOTAL_INSU_SURYO, TITLE_ROW, COLUMN_TOTAL_INSU_SURYO, TITLE_ROW + 1, True)
            'xls.SetOrientation(COLUMN_TOTAL_INSU_SURYO, TITLE_ROW, COLUMN_TOTAL_INSU_SURYO, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            'xls.SetValue(COLUMN_TOTAL_INSU_SURYO, TITLE_ROW, "合計員数")
            '↑↑2014/10/10 酒井 ADD END
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
            '作り方制作方法'
            xls.MergeCells(COLUMN_TSUKURIKATA_SEISAKU, TITLE_ROW, COLUMN_TSUKURIKATA_KIBO, TITLE_ROW, True)
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
            '材質'
            xls.MergeCells(COLUMN_ZAIRYO_KIJUTSU, TITLE_ROW, COLUMN_ZAIRYO_KIJUTSU, TITLE_ROW + 1, True)
            xls.SetValue(COLUMN_ZAIRYO_KIJUTSU, TITLE_ROW, "規格１")
            '板厚'
            xls.MergeCells(COLUMN_BANKO_SURYO, TITLE_ROW, COLUMN_BANKO_SURYO, TITLE_ROW + 1, True)
            xls.SetValue(COLUMN_BANKO_SURYO, TITLE_ROW, "板厚")
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
            '担当者名'
            xls.MergeCells(COLUMN_TANTOSYA_NAME, TITLE_ROW, COLUMN_TANTOSYA_NAME, TITLE_ROW + 1, True)
            xls.SetValue(COLUMN_TANTOSYA_NAME, TITLE_ROW, "担当者名")
            'TEL'
            xls.MergeCells(COLUMN_TEL, TITLE_ROW, COLUMN_TEL, TITLE_ROW + 1, True)
            xls.SetValue(COLUMN_TEL, TITLE_ROW, "TEL")
            '↓↓2014/10/10 酒井 ADD BEGIN
            '合計員数'
            For i As Integer = 0 To EventEbomKanshiVos.Count - 1
                '↓↓2014/10/13 酒井 ADD BEGIN
                'xls.MergeCells(COLUMN_TOTAL_INSU_SURYO, TITLE_ROW, COLUMN_TOTAL_INSU_SURYO, TITLE_ROW + 1, True)
                'xls.SetOrientation(COLUMN_TOTAL_INSU_SURYO, TITLE_ROW, COLUMN_TOTAL_INSU_SURYO, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                xls.MergeCells(COLUMN_TOTAL_INSU_SURYO + i, TITLE_ROW, COLUMN_TOTAL_INSU_SURYO + i, TITLE_ROW + 1, True)
                xls.SetOrientation(COLUMN_TOTAL_INSU_SURYO + i, TITLE_ROW, COLUMN_TOTAL_INSU_SURYO + i, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                '↑↑2014/10/13 酒井 ADD END
                xls.SetValue(COLUMN_TOTAL_INSU_SURYO + i, TITLE_ROW, EventEbomKanshiVos(i).ShisakuGousya)
            Next
            '↑↑2014/10/10 酒井 ADD END
        End Sub

        ''' <summary>
        ''' Excel出力　試作部品表ボディ部
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub SetBody(ByVal xls As ShisakuExcel, ByVal buhinEditVos As List(Of YakanSekkeiBlockVoHelperExcel))
            '’↓↓2014/10/10 酒井 ADD BEGIN
            '            Dim dataMatrix(buhinEditVos.Count - 1, COLUMN_TEL) As String
            Dim dataMatrix(buhinEditVos.Count - 1, COLUMN_TOTAL_INSU_SURYO + EventEbomKanshiVos.Count - 1) As String
            Dim maeEventCode As String = ""
            Dim maeBukaCode As String = ""
            Dim maeBlockNo As String = ""
            Dim maeBuhinNo As String = ""
            '↑↑2014/10/10 酒井 ADD END
            '↓↓2014/10/13 酒井 ADD BEGIN
            Dim maeKaiteiNo As String = ""
            Dim mae2EventCode As String = ""
            Dim mae2BukaCode As String = ""
            Dim mae2BlockNo As String = ""
            Dim mae2BuhinNo As String = ""
            Dim mae2KaiteiNo As String = ""
            '↑↑2014/10/13 酒井 ADD END

            Dim dao As New YakanSekkeiTenkaiDaoImpl
            '↓↓2014/10/10 酒井 ADD BEGIN
            '            Dim rowIndex As Integer = 0
            Dim rowIndex As Integer = -1
            '↑↑2014/10/10 酒井 ADD END
            For Each buhinEditVo As YakanSekkeiBlockVoHelperExcel In buhinEditVos
                '↓↓2014/10/10 酒井 ADD BEGIN
                '↓↓2014/10/13 酒井 ADD BEGIN
                'If maeEventCode = buhinEditVo.ShisakuEventCode AndAlso _
                '    maeBukaCode = buhinEditVo.ShisakuBukaCode AndAlso _
                '    maeBlockNo = buhinEditVo.ShisakuBlockNo AndAlso _
                '    maeBuhinNo = buhinEditVo.BuhinNo Then
                If maeEventCode = buhinEditVo.ShisakuEventCode AndAlso _
                    maeBukaCode = buhinEditVo.ShisakuBukaCode AndAlso _
                    maeBlockNo = buhinEditVo.ShisakuBlockNo AndAlso _
                    maeBuhinNo = buhinEditVo.BuhinNo AndAlso _
                    maeKaiteiNo = buhinEditVo.ShisakuBlockNoKaiteiNo Then
                    '↑↑2014/10/13 酒井 ADD END

                    '1行前とキー項目が同じで、部品改訂Noも同じ場合
                    '（→キー項目が同じで、部品改訂Noが異なる部品情報がない）
                    '（→Add or Delete）
                    'シンボルと号車別員数のみ更新する

                    If StringUtil.IsEmpty(dataMatrix(rowIndex, COLUMN_SYMBOL - 1)) Then
                        dataMatrix(rowIndex, COLUMN_SYMBOL - 1) = buhinEditVo.Symbol
                    End If

                    '↓↓2014/10/13 酒井 ADD BEGIN
                    For i As Integer = 0 To EventEbomKanshiVos.Count - 1
                        If EventEbomKanshiVos(i).ShisakuGousya = buhinEditVo.ShisakuGousya Then
                            '合計員数'
                            If StringUtil.IsEmpty(buhinEditVo.InsuSuryo) Then
                                dataMatrix(rowIndex, COLUMN_TOTAL_INSU_SURYO - 1 + i) = ""
                            Else
                                dataMatrix(rowIndex, COLUMN_TOTAL_INSU_SURYO - 1 + i) = buhinEditVo.InsuSuryo
                            End If
                        End If
                    Next
                ElseIf mae2EventCode = buhinEditVo.ShisakuEventCode AndAlso _
                    mae2BukaCode = buhinEditVo.ShisakuBukaCode AndAlso _
                    mae2BlockNo = buhinEditVo.ShisakuBlockNo AndAlso _
                    mae2BuhinNo = buhinEditVo.BuhinNo AndAlso _
                    mae2KaiteiNo = buhinEditVo.ShisakuBlockNoKaiteiNo Then

                    '2行前とキー項目が同じで、部品改訂Noも同じ場合
                    '（→1行前はキー項目が同じで、部品改訂Noが違う）
                    'シンボルと号車別員数のみ更新する

                    If StringUtil.IsEmpty(dataMatrix(rowIndex - 1, COLUMN_SYMBOL - 1)) Then
                        dataMatrix(rowIndex - 1, COLUMN_SYMBOL - 1) = buhinEditVo.Symbol
                    End If

                    '↓↓2014/10/13 酒井 ADD BEGIN
                    For i As Integer = 0 To EventEbomKanshiVos.Count - 1
                        If EventEbomKanshiVos(i).ShisakuGousya = buhinEditVo.ShisakuGousya Then
                            '合計員数'
                            If StringUtil.IsEmpty(buhinEditVo.InsuSuryo) Then
                                dataMatrix(rowIndex - 1, COLUMN_TOTAL_INSU_SURYO - 1 + i) = ""
                            Else
                                dataMatrix(rowIndex - 1, COLUMN_TOTAL_INSU_SURYO - 1 + i) = buhinEditVo.InsuSuryo
                            End If
                        End If
                    Next
                    '↑↑2014/10/13 酒井 ADD END
                Else
                    '直前2行とキー項目が異なる場合

                    '↓↓2014/10/13 酒井 ADD BEGIN

                    '直前2行のシンボル後処理
                    If rowIndex > 0 Then
                        '2行目以降

                        If maeEventCode = mae2EventCode AndAlso _
                        maeBukaCode = mae2BukaCode AndAlso _
                        maeBlockNo = mae2BlockNo AndAlso _
                        maeBuhinNo = mae2BuhinNo Then
                            '直前2行が同じ部品

                            If StringUtil.IsNotEmpty(dataMatrix(rowIndex, COLUMN_SYMBOL - 1)) Or _
                                                    StringUtil.IsNotEmpty(dataMatrix(rowIndex - 1, COLUMN_SYMBOL - 1)) Then
                                '少なくともどちらか一方にシンボルがある場合、シンボルCで揃える。
                                '号車No.1は同一だが、号車No.2の員数が増えた場合など。
                                dataMatrix(rowIndex, COLUMN_SYMBOL - 1) = "C"
                                dataMatrix(rowIndex - 1, COLUMN_SYMBOL - 1) = "C"
                            End If

                        End If

                    End If

                    '出力rowindexの採番
                    'rowIndex = rowIndex + 1
                    If rowIndex < 1 Then
                        '最初の2行
                        rowIndex = rowIndex + 1
                    ElseIf maeEventCode = mae2EventCode AndAlso _
                        maeBukaCode = mae2BukaCode AndAlso _
                        maeBlockNo = mae2BlockNo AndAlso _
                        maeBuhinNo = mae2BuhinNo AndAlso _
                        StringUtil.IsEmpty(dataMatrix(rowIndex, COLUMN_SYMBOL - 1)) AndAlso _
                        StringUtil.IsEmpty(dataMatrix(rowIndex - 1, COLUMN_SYMBOL - 1)) Then
                        '前2行のキー項目が同一で、シンボルなしの場合（完全一致部品の場合）、
                        '2行目を削除（上書き）するため、rowIndexをインクリメントしない
                    Else
                        rowIndex = rowIndex + 1
                    End If

                    mae2EventCode = maeEventCode
                    mae2BukaCode = maeBukaCode
                    mae2BlockNo = maeBlockNo
                    mae2BuhinNo = maeBuhinNo
                    mae2KaiteiNo = maeKaiteiNo

                    maeEventCode = buhinEditVo.ShisakuEventCode
                    maeBukaCode = buhinEditVo.ShisakuBukaCode
                    maeBlockNo = buhinEditVo.ShisakuBlockNo
                    maeBuhinNo = buhinEditVo.BuhinNo
                    maeKaiteiNo = buhinEditVo.ShisakuBlockNoKaiteiNo
                    '↑↑2014/10/13 酒井 ADD END

                    '↑↑2014/10/10 酒井 ADD END
                    'シンボル'
                    dataMatrix(rowIndex, COLUMN_SYMBOL - 1) = buhinEditVo.Symbol
                    '↓↓2014/10/10 酒井 ADD BEGIN
                    '号車'
                    '                dataMatrix(rowIndex, COLUMN_GOUSYA - 1) = buhinEditVo.ShisakuGousya
                    '↑↑2014/10/10 酒井 ADD END
                    'ブロックNo'
                    dataMatrix(rowIndex, COLUMN_BLOCK_NO - 1) = buhinEditVo.ShisakuBlockNo
                    '部品番号'
                    dataMatrix(rowIndex, COLUMN_BUHIN_NO - 1) = buhinEditVo.BuhinNo
                    '設計課'
                    dataMatrix(rowIndex, COLUMN_SEKKEIKA - 1) = dao.GetKa_Ryaku_Name(buhinEditVo.ShisakuBukaCode).KaRyakuName
                    '設通No'
                    ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
                    'dataMatrix(rowIndex, COLUMN_STSR_DHSTBA_NO - 1) = buhinEditVo.ShukeiCode
                    dataMatrix(rowIndex, COLUMN_STSR_DHSTBA_NO - 1) = buhinEditVo.TsuchishoNo
                    ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END
                    'レベル'
                    If StringUtil.IsEmpty(buhinEditVo.Level) Then
                        dataMatrix(rowIndex, COLUMN_LEVEL - 1) = ""
                    Else
                        dataMatrix(rowIndex, COLUMN_LEVEL - 1) = buhinEditVo.Level
                    End If
                    '国内集計'
                    dataMatrix(rowIndex, COLUMN_SHUKEI_CODE - 1) = buhinEditVo.ShukeiCode
                    '海外集計'
                    dataMatrix(rowIndex, COLUMN_SIA_SHUKEI_CODE - 1) = buhinEditVo.SiaShukeiCode
                    '現調区分'
                    dataMatrix(rowIndex, COLUMN_GENCYO_KBN - 1) = buhinEditVo.GencyoCkdKbn
                    '取引先コード'
                    dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_CODE - 1) = buhinEditVo.MakerCode
                    '取引先名称'
                    dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_NAME - 1) = buhinEditVo.MakerName
                    '試作区分'
                    dataMatrix(rowIndex, COLUMN_SHISAKU_KBN - 1) = buhinEditVo.BuhinNoKbn
                    '改訂'
                    dataMatrix(rowIndex, COLUMN_KAITEI - 1) = buhinEditVo.ZumenKaiteiNo
                    '枝番'
                    dataMatrix(rowIndex, COLUMN_EDA_BAN - 1) = buhinEditVo.EdaBan
                    '部品名称'
                    dataMatrix(rowIndex, COLUMN_BUHIN_NAME - 1) = buhinEditVo.BuhinName
                    '↓↓2014/10/10 酒井 ADD BEGIN
                    ''合計員数'
                    'If StringUtil.IsEmpty(buhinEditVo.InsuSuryo) Then
                    '    dataMatrix(rowIndex, COLUMN_TOTAL_INSU_SURYO - 1) = ""
                    'Else
                    '    dataMatrix(rowIndex, COLUMN_TOTAL_INSU_SURYO - 1) = buhinEditVo.InsuSuryo
                    'End If
                    '↑↑2014/10/10 酒井 ADD END
                    '再使用不可'
                    dataMatrix(rowIndex, COLUMN_SAISHIYOUFUKA - 1) = buhinEditVo.Saishiyoufuka
                    '供給セクション'
                    dataMatrix(rowIndex, COLUMN_KYOUKU_SECTION - 1) = buhinEditVo.KyoukuSection
                    '出図予定日'
                    If buhinEditVo.ShutuzuYoteiDate = 99999999 OrElse buhinEditVo.ShutuzuYoteiDate = 0 Then
                        dataMatrix(rowIndex, COLUMN_SHUTUZUYOTEIBI - 1) = ""
                    Else
                        dataMatrix(rowIndex, COLUMN_SHUTUZUYOTEIBI - 1) = buhinEditVo.ShutuzuYoteiDate
                    End If
                    '作り方制作方法'
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_SEISAKU - 1) = buhinEditVo.TsukurikataSeisaku
                    '作り方型仕様１'
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU1 - 1) = buhinEditVo.TsukurikataKatashiyou1
                    '作り方型仕様２'
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU2 - 1) = buhinEditVo.TsukurikataKatashiyou2
                    '作り方型仕様３'
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU3 - 1) = buhinEditVo.TsukurikataKatashiyou3
                    '作り方治具'
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_TIGU - 1) = buhinEditVo.TsukurikataTigu
                    '作り方納入見通り'
                    If StringUtil.IsEmpty(buhinEditVo.TsukurikataNounyu) Or buhinEditVo.TsukurikataNounyu = 0 Then
                        dataMatrix(rowIndex, COLUMN_TSUKURIKATA_NOUNYU - 1) = ""
                    Else
                        dataMatrix(rowIndex, COLUMN_TSUKURIKATA_NOUNYU - 1) = buhinEditVo.TsukurikataNounyu
                    End If
                    '作り方部品製作規模・概要'
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KIBO - 1) = buhinEditVo.TsukurikataKibo
                    '材質'
                    dataMatrix(rowIndex, COLUMN_ZAIRYO_KIJUTSU - 1) = buhinEditVo.ZairyoKijutsu
                    '板厚'
                    dataMatrix(rowIndex, COLUMN_BANKO_SURYO - 1) = buhinEditVo.BankoSuryo
                    '試作部品費'
                    dataMatrix(rowIndex, COLUMN_SHISAKU_BUHIN_HI - 1) = buhinEditVo.ShisakuBuhinHi
                    '試作型費'
                    dataMatrix(rowIndex, COLUMN_SHISAKU_KATA_HI - 1) = buhinEditVo.ShisakuKataHi
                    'NOTE'
                    dataMatrix(rowIndex, COLUMN_BUHIN_NOTE - 1) = buhinEditVo.BuhinNote
                    '備考'
                    dataMatrix(rowIndex, COLUMN_BIKOU - 1) = buhinEditVo.Bikou
                    '担当者名'
                    dataMatrix(rowIndex, COLUMN_TANTOSYA_NAME - 1) = dao.FindByShainName(buhinEditVo.UserId)
                    'TEL'
                    dataMatrix(rowIndex, COLUMN_TEL - 1) = buhinEditVo.TelNo
                    '↓↓2014/10/10 酒井 ADD BEGIN
                    '↓↓2014/10/13 酒井 ADD BEGIN
                    'End If
                    '↑↑2014/10/13 酒井 ADD END

                    For i As Integer = 0 To EventEbomKanshiVos.Count - 1
                        If EventEbomKanshiVos(i).ShisakuGousya = buhinEditVo.ShisakuGousya Then
                            '合計員数'
                            If StringUtil.IsEmpty(buhinEditVo.InsuSuryo) Then
                                dataMatrix(rowIndex, COLUMN_TOTAL_INSU_SURYO - 1 + i) = ""
                            Else
                                dataMatrix(rowIndex, COLUMN_TOTAL_INSU_SURYO - 1 + i) = buhinEditVo.InsuSuryo
                            End If
                        End If
                    Next

                    '↓↓2014/10/13 酒井 ADD BEGIN
                End If
                '↑↑2014/10/13 酒井 ADD END

                'rowIndex = rowIndex + 1
                '↓↓2014/10/13 酒井 ADD BEGIN
                'maeEventCode = buhinEditVo.ShisakuEventCode
                'maeBukaCode = buhinEditVo.ShisakuBukaCode
                'maeBlockNo = buhinEditVo.ShisakuBlockNo
                'maeBuhinNo = buhinEditVo.BuhinNo
                '↑↑2014/10/13 酒井 ADD END
                '↑↑2014/10/10 酒井 ADD END
            Next

            '↓↓2014/10/13 酒井 ADD BEGIN
            '直前2行のシンボル後処理
            If rowIndex > 0 Then
                '2行目以降

                If maeEventCode = mae2EventCode AndAlso _
                maeBukaCode = mae2BukaCode AndAlso _
                maeBlockNo = mae2BlockNo AndAlso _
                maeBuhinNo = mae2BuhinNo Then
                    '直前2行が同じ部品

                    If StringUtil.IsNotEmpty(dataMatrix(rowIndex, COLUMN_SYMBOL - 1)) Or _
                                            StringUtil.IsNotEmpty(dataMatrix(rowIndex - 1, COLUMN_SYMBOL - 1)) Then
                        '少なくともどちらか一方にシンボルがある場合、シンボルCで揃える。
                        '号車No.1は同一だが、号車No.2の員数が増えた場合など。
                        dataMatrix(rowIndex, COLUMN_SYMBOL - 1) = "C"
                        dataMatrix(rowIndex - 1, COLUMN_SYMBOL - 1) = "C"
                    End If

                End If

            End If

            '↑↑2014/10/13 酒井 ADD END

                xls.CopyRange(0, START_ROW, UBound(dataMatrix, 2) - 1, START_ROW + UBound(dataMatrix), dataMatrix)

        End Sub

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
        ''シンボル
        Private COLUMN_SYMBOL As Integer
        '' 号車
        Private COLUMN_GOUSYA As Integer
        '' ブロックNo
        Private COLUMN_BLOCK_NO As Integer
        '' 部品番号
        Private COLUMN_BUHIN_NO As Integer
        '' 設計課
        Private COLUMN_SEKKEIKA As Integer
        '' 設通No
        Private COLUMN_STSR_DHSTBA_NO As Integer
        '' レベル
        Private COLUMN_LEVEL As Integer
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
        '' 材質
        Private COLUMN_ZAIRYO_KIJUTSU As Integer
        '' 板厚
        Private COLUMN_BANKO_SURYO As Integer
        '' 試作部品費
        Private COLUMN_SHISAKU_BUHIN_HI As Integer
        '' 試作型費
        Private COLUMN_SHISAKU_KATA_HI As Integer
        '' NOTE
        Private COLUMN_BUHIN_NOTE As Integer
        '' 備考
        Private COLUMN_BIKOU As Integer
        '' 担当者名
        Private COLUMN_TANTOSYA_NAME As Integer
        '' TEL
        Private COLUMN_TEL As Integer
#End Region

#Region "各行のタグ設定"
        '' タイトル行
        Private TITLE_ROW As Integer = 4
        '' 書き込み開始行
        Private START_ROW As Integer = 6
#End Region

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class
End Namespace