Imports ShisakuCommon
Imports EBom.Excel
Imports EBom.Data
Imports EBom.Common
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo
Imports ExcelOutput.TehaichoExcel.Vo


Imports ExcelOutput.TehaichoExcel.Dao

Namespace TehaichoExcel.Excel
    '手配帳エクセル出力'
    Public Class ExportTehaichoExcel

        Private Impl As TehaichoExcelDao
        Private EndCol As Integer
        Private KoujiShireiNo As String
        Private Bikou As Integer

        Public Sub New(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)
            Dim systemDrive As String = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            Dim fileName As String
            Dim aEventVo As New TShisakuEventVo
            Dim aListCodeVo As New TShisakuListcodeVo

            Impl = New TehaichoExcelDaoImpl
            aEventVo = Impl.FindByEvent(shisakuEventCode)
            aListCodeVo = Impl.FindByListCode(shisakuEventCode, shisakuListCode)
            KoujiShireiNo = aListCodeVo.ShisakuKoujiShireiNo
            Using sfd As New SaveFileDialog()
                sfd.FileName = ShisakuCommon.ShisakuGlobal.ExcelOutPut
                sfd.InitialDirectory = systemDrive
                '2012/01/21
                '--------------------------------------------------
                'EXCEL出力先フォルダチェック
                ShisakuCommon.ShisakuComFunc.ExcelOutPutDirCheck()
                '--------------------------------------------------
                '[Excel出力系 E]
                'fileName = sfd.InitialDirectory + "\" + sfd.FileName
                fileName = aEventVo.ShisakuKaihatsuFugo + aListCodeVo.ShisakuEventName + " 手配帳 " + Now.ToString("MMdd") + Now.ToString("HHmm") + ".xls"
                fileName = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + StringUtil.ReplaceInvalidCharForFileName(fileName)    '2012/02/08 Excel出力ディレクトリ指定対応
            End Using

            If Not ShisakuComFunc.IsFileOpen(ShisakuCommon.ShisakuGlobal.ExcelOutPut) Then

                Dim aKihonListVo As New List(Of TShisakuTehaiKihonVoHelper)
                Dim aGousyaListVo As New List(Of TShisakuTehaiGousyaVo)

                Dim aBaseListVo As New List(Of TShisakuEventBaseVo)

                aKihonListVo = Impl.FindByTehaiKihon(shisakuEventCode, shisakuListCode)
                aGousyaListVo = Impl.FindByTehaiGousya(shisakuEventCode)

                aBaseListVo = Impl.FindByEventBase(shisakuEventCode, shisakuListCode)


                Dim i As Integer = 0
                For Each Vo As TShisakuEventBaseVo In aBaseListVo
                    If Not StringUtil.Equals(Vo.ShisakuGousya, "DUMMY") Then
                        For Each KVo As TShisakuTehaiKihonVoHelper In aKihonListVo
                            If KVo.ShisakuGousyaHyoujiJun = Vo.HyojijunNo Then
                                If StringUtil.IsEmpty(KVo.Flag) Then
                                    KVo.ShisakuGousyaHyoujiJun = i
                                    KVo.Flag = "１"
                                End If
                            End If
                        Next
                        Vo.HyojijunNo = i
                        i = i + 1
                    End If
                Next
                i = i + 1
                For Each Vo As TShisakuEventBaseVo In aBaseListVo
                    If StringUtil.Equals(Vo.ShisakuGousya, "DUMMY") Then
                        For Each KVo As TShisakuTehaiKihonVoHelper In aKihonListVo
                            If KVo.ShisakuGousyaHyoujiJun = Vo.HyojijunNo Then
                                If StringUtil.IsEmpty(KVo.Flag) Then
                                    KVo.ShisakuGousyaHyoujiJun = i
                                    KVo.Flag = "１"
                                End If
                            End If
                        Next
                        Vo.HyojijunNo = i
                    End If
                Next

                Using xls As New ShisakuExcel(fileName)
                    xls.OpenBook(fileName)
                    xls.ClearWorkBook()
                    xls.SetFont("ＭＳ Ｐゴシック", 11)
                    SetTehaichoExcel(xls, aKihonListVo, aGousyaListVo, aEventVo, aBaseListVo, aListCodeVo)
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
        Private Sub SetTehaichoExcel(ByVal xls As ShisakuExcel, ByVal aKihonListVo As List(Of TShisakuTehaiKihonVoHelper), _
                                     ByVal aGousyaListVo As List(Of TShisakuTehaiGousyaVo), _
                                     ByVal aEventVo As TShisakuEventVo, _
                                     ByVal aBaseListVo As List(Of TShisakuEventBaseVo), _
                                     ByVal aListCodeVo As TShisakuListcodeVo)

            '列の設定'
            SetColumnNo()
            'ヘッダー部'
            SetHeader(xls, aEventVo, aListCodeVo)
            'タイトル部'
            SetTitle(xls, aBaseListVo)
            'ボディ部'
            SetBody(xls, aKihonListVo, aBaseListVo)    '<----時間がかかる  要チューニング
            '列と行の調整'
            SetColumnRow(xls)

        End Sub

        ''' <summary>
        ''' Excel出力　試作部品表ヘッダー部
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub SetHeader(ByVal xls As ShisakuExcel, ByVal aEventVo As TShisakuEventVo, ByVal aListCode As TShisakuListcodeVo)
            'リストコード'
            xls.SetValue(1, 1, "リストコード：" + aListCode.ShisakuListCode)
            'リストコード改訂No'
            xls.SetValue(1, 2, "リストコード改訂No" + aListCode.ShisakuListCodeKaiteiNo)
            'イベント名称'
            Dim EventName As String = "イベント名称： " + aEventVo.ShisakuKaihatsuFugo + "  " + aEventVo.ShisakuEventName
            xls.SetValue(1, 3, EventName)
            '工事指令'
            xls.SetValue(1, 4, "工事指令：" + aListCode.ShisakuKoujiShireiNo)
            '抽出日時'
            Dim aDate As New DateTime
            aDate = DateTime.Now
            xls.SetValue(1, 5, "抽出日時：" + aDate.ToString)
            '旧リストコード'
            xls.SetValue(6, 1, "旧リストコード：" + aListCode.OldListCode)
            'グループNo'
            xls.SetValue(6, 2, "グループNo：" + aListCode.ShisakuGroupNo)
            '工事区分'
            xls.SetValue(6, 3, "工事区分：" + aListCode.ShisakuKoujiKbn)
            '予算区分(工事No)'
            xls.SetValue(6, 4, "予算区分(工事No)：" + aListCode.ShisakuKoujiNo)

        End Sub

        ''' <summary>
        ''' Excel出力　試作部品表タイトル部
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub SetTitle(ByVal xls As ShisakuExcel, ByVal aBaseListVo As List(Of TShisakuEventBaseVo))

            '履歴'
            xls.SetValue(COLUMN_RIREKI, TITLE_ROW, "履歴")
            'キャンセル実績'
            xls.SetValue(COLUMN_CANCEL_JISSEKI, TITLE_ROW, "キャンセル実績")
            'ブロックNo'
            xls.SetValue(COLUMN_BLOCK_NO, TITLE_ROW, "ブロックNo")
            '行ID'
            xls.SetValue(COLUMN_GYOU_ID, TITLE_ROW, "行ID")
            '専用マーク'
            xls.SetValue(COLUMN_SENYOU_MARK, TITLE_ROW, "専用マーク")
            'レベル'
            xls.SetValue(COLUMN_LEVEL, TITLE_ROW, "レベル")
            'ユニット区分'
            xls.SetValue(COLUMN_UNIT_KBN, TITLE_ROW, "ユニット区分")
            '集計コード'
            xls.SetValue(COLUMN_SHUKEI_CODE, TITLE_ROW, "集計コード")
            '手配記号'
            xls.SetValue(COLUMN_TEHAI_KIGOU, TITLE_ROW, "手配記号")
            '購担'
            xls.SetValue(COLUMN_KOUTAN, TITLE_ROW, "購担")
            '取引先コード'
            xls.SetValue(COLUMN_TORIHIKISAKI_CODE, TITLE_ROW, "取引先コード")
            '取引先名称'
            xls.SetValue(COLUMN_TORIHIKISAKI_NAME, TITLE_ROW, "取引先名称")
            '部品番号'
            xls.SetValue(COLUMN_BUHIN_NO, TITLE_ROW, "部品番号")
            '試作区分'
            xls.SetValue(COLUMN_SHISAKU_KBN, TITLE_ROW, "試作区分")
            '改訂No'
            xls.SetValue(COLUMN_KAITEI_NO, TITLE_ROW, "改訂No")
            '枝番'
            xls.SetValue(COLUMN_EDA_BAN, TITLE_ROW, "枝番")
            '部品名称'
            xls.SetValue(COLUMN_BUHIN_NAME, TITLE_ROW, "部品名称")
            '納入指示日'
            xls.SetValue(COLUMN_NOUNYUSHIJIBI, TITLE_ROW, "納入指示日")
            '納入指示数'
            xls.SetValue(COLUMN_NOUNYUSHIJISU, TITLE_ROW, "納入指示数")
            '納入場所'
            xls.SetValue(COLUMN_NOUNYUBASYO, TITLE_ROW, "納入場所")
            '供給セクション'
            xls.SetValue(COLUMN_KYOUKYU_SECTION, TITLE_ROW, "供給セクション")
            '再使用不可'
            xls.SetValue(COLUMN_SAISHIYOUFUKA, TITLE_ROW, "再使用不可")
            ''↓↓2014/07/25 Ⅰ.2.管理項目追加_ba) (TES)張 ADD BEGIN
            '作り方'
            xls.MergeCells(COLUMN_TSUKURIKATA_SEISAKU, INSU_ROW, COLUMN_TSUKURIKATA_KIBO, INSU_ROW, True)
            '　　　位置設定
            xls.SetAlignment(COLUMN_TSUKURIKATA_SEISAKU, INSU_ROW, COLUMN_TSUKURIKATA_KIBO, INSU_ROW, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignBottom)
            xls.SetValue(COLUMN_TSUKURIKATA_SEISAKU, INSU_ROW, "作り方")
            '制作方法'
            xls.SetValue(COLUMN_TSUKURIKATA_SEISAKU, TITLE_ROW, "製作方法")
            '型仕様１'
            xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU1, TITLE_ROW, "型仕様１")
            '型仕様２'
            xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU2, TITLE_ROW, "型仕様２")
            '型仕様３'
            xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU3, TITLE_ROW, "型仕様３")
            '治具'
            xls.SetValue(COLUMN_TSUKURIKATA_TIGU, TITLE_ROW, "治具")
            '納入見通し'
            xls.SetValue(COLUMN_TSUKURIKATA_NOUNYU, TITLE_ROW, "納入見通し")
            '部品製作規模・概要'
            xls.SetValue(COLUMN_TSUKURIKATA_KIBO, TITLE_ROW, "部品製作規模・概要")
            ''↑↑2014/07/25 Ⅰ.2.管理項目追加_ba) (TES)張 ADD END

            '材質'
            xls.MergeCells(COLUMN_ZAISHITSU_KIKAKU_1, INSU_ROW, COLUMN_ZAISHITSU_MEKKI, INSU_ROW, True)
            '　　　位置設定
            xls.SetAlignment(COLUMN_ZAISHITSU_KIKAKU_1, INSU_ROW, COLUMN_ZAISHITSU_MEKKI, INSU_ROW, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignBottom)
            xls.SetValue(COLUMN_ZAISHITSU_KIKAKU_1, INSU_ROW, "材質")
            '材質規格1'
            xls.SetValue(COLUMN_ZAISHITSU_KIKAKU_1, TITLE_ROW, "規格１")
            '材質規格2'
            xls.SetValue(COLUMN_ZAISHITSU_KIKAKU_2, TITLE_ROW, "規格２")
            '材質規格3'
            xls.SetValue(COLUMN_ZAISHITSU_KIKAKU_3, TITLE_ROW, "規格３")
            '材質メッキ'
            xls.SetValue(COLUMN_ZAISHITSU_MEKKI, TITLE_ROW, "メッキ")
            '板厚'
            xls.MergeCells(COLUMN_BANKO_SURYO, INSU_ROW, COLUMN_BANKO_SURYO_U, INSU_ROW, True)
            '　　　位置設定
            xls.SetAlignment(COLUMN_BANKO_SURYO, INSU_ROW, COLUMN_BANKO_SURYO_U, INSU_ROW, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignBottom)
            xls.SetValue(COLUMN_BANKO_SURYO, INSU_ROW, "板厚")
            '板厚数量'
            xls.SetValue(COLUMN_BANKO_SURYO, TITLE_ROW, "板厚")
            '板厚数量u'
            xls.SetValue(COLUMN_BANKO_SURYO_U, TITLE_ROW, "u")

            '材料情報・製品長'
            xls.SetValue(COLUMN_MATERIAL_INFO_LENGTH, TITLE_ROW, "製品長")
            '材料情報・製品幅'
            xls.SetValue(COLUMN_MATERIAL_INFO_WIDTH, TITLE_ROW, "製品幅")
            'データ項目・改訂№'
            xls.SetValue(COLUMN_DATA_ITEM_KAITEI_NO, TITLE_ROW, "データ項目・改訂№")
            'データ項目・エリア名'
            xls.SetValue(COLUMN_DATA_ITEM_AREA_NAME, TITLE_ROW, "データ項目・エリア名")
            'データ項目・セット名'
            xls.SetValue(COLUMN_DATA_ITEM_SET_NAME, TITLE_ROW, "データ項目・セット名")
            '材データ項目・改訂情報'
            xls.SetValue(COLUMN_DATA_ITEM_KAITEI_INFO, TITLE_ROW, "データ項目・改訂情報")


            '材料寸法_X(mm)'
            xls.SetValue(COLUMN_ZAIRYO_SUNPO_X, TITLE_ROW, "材料寸法_X(mm)")
            '材料寸法_Y(mm)'
            xls.SetValue(COLUMN_ZAIRYO_SUNPO_Y, TITLE_ROW, "材料寸法_Y(mm)")
            '材料寸法_Z(mm)'
            xls.SetValue(COLUMN_ZAIRYO_SUNPO_Z, TITLE_ROW, "材料寸法_Z(mm)")
            '材料寸法_XY(mm)'
            xls.SetValue(COLUMN_ZAIRYO_SUNPO_XY, TITLE_ROW, "材料寸法_XY(mm)")
            '材料寸法_XZ(mm)'
            xls.SetValue(COLUMN_ZAIRYO_SUNPO_XZ, TITLE_ROW, "材料寸法_XZ(mm)")
            '材料寸法_YZ(mm)'
            xls.SetValue(COLUMN_ZAIRYO_SUNPO_YZ, TITLE_ROW, "材料寸法_YZ(mm)")


            '試作部品費'
            xls.SetValue(COLUMN_SHISAKU_BUHIN_HI, TITLE_ROW, "試作部品費(円)")
            '試作型費'
            xls.SetValue(COLUMN_SHISAKU_KATA_HI, TITLE_ROW, "試作型費(千円)")
            '新試作手配システム'
            xls.SetValue(COLUMN_BIKOU, INSU_ROW, "新試作手配システム")
            '　　　位置設定
            xls.SetAlignment(COLUMN_BIKOU, INSU_ROW, COLUMN_BIKOU, INSU_ROW, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
            '備考'
            xls.SetValue(COLUMN_BIKOU, TITLE_ROW, "備考")

            '員数'
            Dim maxIndex As Integer = 0
            For index As Integer = 0 To aBaseListVo.Count - 1
                Dim insu As Integer = index + 1
                xls.SetOrientation(COLUMN_START_GOUSYA + index, INSU_ROW, _
                                   COLUMN_START_GOUSYA + index, INSU_ROW, ShisakuExcel.XlOrientation.xlVertical)
                If Not StringUtil.Equals(aBaseListVo(index).ShisakuGousya, "DUMMY") Then
                    xls.SetValue(COLUMN_START_GOUSYA + index, INSU_ROW, aBaseListVo(index).ShisakuGousya)
                    xls.SetValue(COLUMN_START_GOUSYA + index, TITLE_ROW, "員数" + insu.ToString)
                    maxIndex = index
                End If
            Next

            'DUMMY用に一つ空ける'
            xls.SetOrientation(COLUMN_START_GOUSYA + maxIndex + 1, INSU_ROW, COLUMN_START_GOUSYA + maxIndex + 1, INSU_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_START_GOUSYA + maxIndex + 1, INSU_ROW, "")
            xls.SetValue(COLUMN_START_GOUSYA + maxIndex + 1, TITLE_ROW, "員数" + StringUtil.ToString(maxIndex + 2))


            xls.SetOrientation(COLUMN_START_GOUSYA + maxIndex + 2, INSU_ROW, COLUMN_START_GOUSYA + maxIndex + 2, INSU_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_START_GOUSYA + maxIndex + 2, INSU_ROW, "DUMMY")
            xls.SetValue(COLUMN_START_GOUSYA + maxIndex + 2, TITLE_ROW, "員数" + StringUtil.ToString(maxIndex + 3))


            COLUMN_END_GOUSYA = COLUMN_START_GOUSYA + maxIndex + 3

            COLUMN_END_GOUSYA = EzUtil.Increment(COLUMN_END_GOUSYA)
            EndCol = COLUMN_END_GOUSYA




            '親品番'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "親品番")
            '親品番試作区分'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "親品番試作区分")
            '発行No.'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "発行No.")
            '発注年月日'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "発注年月日")
            '同期'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "同期")
            '分納'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "分納")
            'ネック'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "ネック")
            '暫定・欠品'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "暫定・欠品")
            'その他'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "その他")
            '現行試作手配システム'
            Dim index2 As Integer = COLUMN_END_GOUSYA
            xls.SetValue(EzUtil.Increment(index2), INSU_ROW, "現行試作手配システム")
            '　　　位置設定
            xls.SetAlignment(EzUtil.Increment(index2), INSU_ROW, EzUtil.Increment(COLUMN_END_GOUSYA), INSU_ROW, XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignBottom)
            '備考'
            COLUMN_END_GOUSYA = COLUMN_END_GOUSYA - 1
            Bikou = COLUMN_END_GOUSYA
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "備考")
            '納期回答１'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納期回答１")
            '納入予定数１'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納期予定数１")
            '納入区分'  
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納入区分")
            '検収年月日'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "検収年月日")
            '納入累計数'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納入累計数")
            '引取り検収年月日'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "引取り検収年月日")
            '引取り累計数'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "引取り累計数")
            '納入実績マーク'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納入実績マーク")
            '取消年月日'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "取消年月日")
            '取消数'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "取消数")
            '納期回答２'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納期回答２")
            '納入予定数２'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納入予定数２")
            '納期回答３'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納期回答３")
            '納入予定数３'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納入予定数３")
            '納期回答４'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納期回答４")
            '納入予定数４'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納入予定数４")
            '納期回答５'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納期回答５")
            '納入予定数５'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納入予定数５")
            '納期回答６'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納期回答６")
            '納入予定数６'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納入予定数６")
            '納期回答７'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納期回答７")
            '納入予定数７'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納入予定数７")
            '納期回答８'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納期回答８")
            '納入予定数８'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "納入予定数８")
            '処置'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "処置")
            '理由'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "理由")
            '対応'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "対応")
            '部署'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "部署")
            '設計担当者'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "設計担当者")
            'ＴＥＬ'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "ＴＥＬ")
            '暫定品納入日' 
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "暫定品納入日")
            '正規扱いOr後交換有り'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "正規扱いOr後交換有り")
            '設通No(最新)'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "設通No(最新)")
            '設通No(実績)'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "設通No(実績)")
            '出図予定日(最新)'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "出図予定日(最新)")
            '出図実績日'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "出図実績日")
            '型'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "型")
            '工法'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "工法")
            'ﾒｰｶｰ見積り型費(円)'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "メーカー見積り型費(円)")
            'ﾒｰｶｰ見積り部品費(円)'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "メーカー見積り部品費(円)")
            '工事区分'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "工事区分")
            '予算区分'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "予算区分")
            '手番'
            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), TITLE_ROW, "手番")

        End Sub

        ''' <summary>
        ''' Excel出力　試作部品表ボディ部
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aKihonListVo">手配基本情報リスト</param>
        ''' <param name="aBaseListVo">ベース車情報リスト</param>
        ''' <remarks></remarks>
        Private Sub SetBody(ByVal xls As ShisakuExcel, ByVal aKihonListVo As List(Of TShisakuTehaiKihonVoHelper), _
                            ByVal aBaseListVo As List(Of TShisakuEventBaseVo))

            Dim rowIndex As Integer = 0
            Dim MergeFlag As Boolean = False
            Dim wUnitKbn As String = "9"

            Dim maxRowNumber As Integer = 0
            '２次元配列の列数を算出
            For i As Integer = 0 To aKihonListVo.Count - 1
                If Not i = 0 Then

                    If Not StringUtil.Equals(aKihonListVo(i).BuhinNo, aKihonListVo(i - 1).BuhinNo) Then
                        maxRowNumber = maxRowNumber + 1
                    Else
                        '2012/02/23 ブロックが違う'
                        If Not StringUtil.Equals(aKihonListVo(i).ShisakuBlockNo, aKihonListVo(i - 1).ShisakuBlockNo) Then
                            maxRowNumber = maxRowNumber + 1
                        Else
                            '2012/02/23 レベルが違う'
                            If aKihonListVo(i).Level <> aKihonListVo(i - 1).Level Then
                                maxRowNumber = maxRowNumber + 1
                            Else
                                '2012/02/23 集計コードが違う'
                                If Not StringUtil.Equals(aKihonListVo(i).ShukeiCode, aKihonListVo(i - 1).ShukeiCode) Then
                                    maxRowNumber = maxRowNumber + 1
                                Else
                                    '2012/02/23 手配記号が違う'
                                    If Not StringUtil.Equals(aKihonListVo(i).TehaiKigou, aKihonListVo(i - 1).TehaiKigou) Then
                                        maxRowNumber = maxRowNumber + 1
                                    Else
                                        '2012/02/23 供給セクションが違う'
                                        If Not StringUtil.Equals(aKihonListVo(i).KyoukuSection, aKihonListVo(i - 1).KyoukuSection) Then
                                            maxRowNumber = maxRowNumber + 1
                                        Else
                                            '2013/04/05 再使用不可区分が違う'
                                            If Not StringUtil.Equals(aKihonListVo(i).Saishiyoufuka, aKihonListVo(i - 1).Saishiyoufuka) Then
                                                maxRowNumber = maxRowNumber + 1
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If

                    End If
                Else
                    maxRowNumber = maxRowNumber + 1
                End If
            Next
            Dim dataMatrix(maxRowNumber, COLUMN_END_GOUSYA - 1) As String

            'ユニット区分だけとる為にはDBアクセスはもったいない
            Dim unitKbnHash As Hashtable = Impl.FindByShisakuBlockNoForUnitKbn()

            For index As Integer = 0 To aKihonListVo.Count - 1

                If Not index = 0 Then
                    If StringUtil.Equals(aKihonListVo(index).BuhinNo, aKihonListVo(index - 1).BuhinNo) Then
                        '2012/02/23 他も比較する'
                        If StringUtil.Equals(aKihonListVo(index).ShisakuBlockNo, aKihonListVo(index - 1).ShisakuBlockNo) Then
                            If aKihonListVo(index).Level = aKihonListVo(index - 1).Level Then
                                If StringUtil.Equals(aKihonListVo(index).ShukeiCode, aKihonListVo(index - 1).ShukeiCode) Then
                                    If StringUtil.Equals(aKihonListVo(index).TehaiKigou, aKihonListVo(index - 1).TehaiKigou) Then
                                        If StringUtil.Equals(aKihonListVo(index).KyoukuSection, aKihonListVo(index - 1).KyoukuSection) Then
                                            If StringUtil.Equals(aKihonListVo(index).Saishiyoufuka, aKihonListVo(index - 1).Saishiyoufuka) Then
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
                    If aKihonListVo(index).InsuSuryo < 0 Then
                        xls.SetOrientation(COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun, _
                                           START_ROW + rowIndex, _
                                           COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun, _
                                           START_ROW + rowIndex, ShisakuExcel.XlOrientation.xlHorizontal)

                        'xls.SetValue(COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, "**")
                        dataMatrix(rowIndex, COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun - 1) = "**"
                    ElseIf aKihonListVo(index).InsuSuryo > 0 Then
                        xls.SetOrientation(COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun, _
                                           START_ROW + rowIndex, _
                                           COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun, _
                                           START_ROW + rowIndex, ShisakuExcel.XlOrientation.xlHorizontal)

                        'xls.SetValue(COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, aKihonListVo(index).InsuSuryo)
                        dataMatrix(rowIndex, COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun - 1) = aKihonListVo(index).InsuSuryo
                    End If
                    MergeFlag = False
                    rowIndex = rowIndex + 1
                Else



                    '履歴'
                    dataMatrix(rowIndex, COLUMN_RIREKI - 1) = aKihonListVo(index).Rireki
                    'キャンセル実績'
                    dataMatrix(rowIndex, COLUMN_CANCEL_JISSEKI - 1) = ""
                    'ブロックNo'
                    dataMatrix(rowIndex, COLUMN_BLOCK_NO - 1) = aKihonListVo(index).ShisakuBlockNo
                    '行ID'
                    dataMatrix(rowIndex, COLUMN_GYOU_ID - 1) = aKihonListVo(index).GyouId
                    '専用マーク'
                    dataMatrix(rowIndex, COLUMN_SENYOU_MARK - 1) = aKihonListVo(index).SenyouMark
                    'レベル'
                    dataMatrix(rowIndex, COLUMN_LEVEL - 1) = aKihonListVo(index).Level

                    'ユニット区分'
                    'ユニット区分は試作ブロック情報から取得する。


                    If Not index = 0 Then
                        If Not StringUtil.Equals(aKihonListVo(index).ShisakuBlockNo, aKihonListVo(index - 1).ShisakuBlockNo) Then
                            'wUnitKbn = Impl.FindByShisakuBlockNo(aKihonListVo(index).ShisakuEventCode, _
                            '         aKihonListVo(index).ShisakuBukaCode, _
                            '         aKihonListVo(index).ShisakuBlockNo)
                            wUnitKbn = unitKbnHash(aKihonListVo(index).ShisakuEventCode & _
                                        aKihonListVo(index).ShisakuBukaCode & _
                                        aKihonListVo(index).ShisakuBlockNo)
                        End If
                    Else
                        'wUnitKbn = Impl.FindByShisakuBlockNo(aKihonListVo(index).ShisakuEventCode, _
                        '             aKihonListVo(index).ShisakuBukaCode, _
                        '             aKihonListVo(index).ShisakuBlockNo)
                        wUnitKbn = unitKbnHash(aKihonListVo(index).ShisakuEventCode & _
                                    aKihonListVo(index).ShisakuBukaCode & _
                                    aKihonListVo(index).ShisakuBlockNo)
                    End If

                    'dataMatrix(rowIndex, COLUMN_UNIT_KBN-1) = aKihonListVo(index).UnitKbn
                    dataMatrix(rowIndex, COLUMN_UNIT_KBN - 1) = wUnitKbn


                    '集計コード'
                    dataMatrix(rowIndex, COLUMN_SHUKEI_CODE - 1) = aKihonListVo(index).ShukeiCode
                    '手配記号'
                    dataMatrix(rowIndex, COLUMN_TEHAI_KIGOU - 1) = aKihonListVo(index).TehaiKigou
                    '購担'
                    dataMatrix(rowIndex, COLUMN_KOUTAN - 1) = aKihonListVo(index).Koutan
                    '取引先コード'
                    dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_CODE - 1) = aKihonListVo(index).TorihikisakiCode
                    '取引先名称'
                    dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_NAME - 1) = aKihonListVo(index).MakerCode
                    '部品番号'
                    dataMatrix(rowIndex, COLUMN_BUHIN_NO - 1) = aKihonListVo(index).BuhinNo
                    '試作区分'
                    dataMatrix(rowIndex, COLUMN_SHISAKU_KBN - 1) = aKihonListVo(index).BuhinNoKbn
                    '改訂No'
                    dataMatrix(rowIndex, COLUMN_KAITEI_NO - 1) = aKihonListVo(index).BuhinNoKaiteiNo
                    '枝番'
                    dataMatrix(rowIndex, COLUMN_EDA_BAN - 1) = aKihonListVo(index).EdaBan
                    '部品名称'
                    dataMatrix(rowIndex, COLUMN_BUHIN_NAME - 1) = aKihonListVo(index).BuhinName
                    '納入指示日'
                    If aKihonListVo(index).NounyuShijibi = 0 Then
                        dataMatrix(rowIndex, COLUMN_NOUNYUSHIJIBI - 1) = ""
                    Else
                        dataMatrix(rowIndex, COLUMN_NOUNYUSHIJIBI - 1) = aKihonListVo(index).NounyuShijibi
                    End If

                    '納入指示数'
                    If aKihonListVo(index).TotalInsuSuryo < 0 Then
                        dataMatrix(rowIndex, COLUMN_NOUNYUSHIJISU - 1) = "**"
                    Else
                        dataMatrix(rowIndex, COLUMN_NOUNYUSHIJISU - 1) = aKihonListVo(index).TotalInsuSuryo
                    End If
                    '納入場所'
                    dataMatrix(rowIndex, COLUMN_NOUNYUBASYO - 1) = aKihonListVo(index).Nouba
                    '供給セクション'
                    dataMatrix(rowIndex, COLUMN_KYOUKYU_SECTION - 1) = aKihonListVo(index).KyoukuSection
                    '再使用不可'
                    dataMatrix(rowIndex, COLUMN_SAISHIYOUFUKA - 1) = aKihonListVo(index).Saishiyoufuka
                    ''↓↓2014/07/25 Ⅰ.2.管理項目追加_bb) (TES)張 ADD BEGIN
                    '作り方製作方法'
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_SEISAKU - 1) = aKihonListVo(index).TsukurikataSeisaku
                    '作り方型仕様１'
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU1 - 1) = aKihonListVo(index).TsukurikataKatashiyou1
                    '作り方型仕様２'
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU2 - 1) = aKihonListVo(index).TsukurikataKatashiyou2
                    '作り方型仕様３'
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU3 - 1) = aKihonListVo(index).TsukurikataKatashiyou3
                    '作り方治具'
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_TIGU - 1) = aKihonListVo(index).TsukurikataTigu
                    '作り方納入見通し'
                    ''↓↓2014/09/03 Ⅰ.2.管理項目追加_bb) 酒井 ADD BEGIN
                    'dataMatrix(rowIndex, COLUMN_TSUKURIKATA_NOUNYU - 1) = aKihonListVo(index).TsukurikataNounyu
                    If StringUtil.IsEmpty(aKihonListVo(index).TsukurikataNounyu) Or aKihonListVo(index).TsukurikataNounyu = 0 Then
                        dataMatrix(rowIndex, COLUMN_TSUKURIKATA_NOUNYU - 1) = ""
                    Else
                        dataMatrix(rowIndex, COLUMN_TSUKURIKATA_NOUNYU - 1) = aKihonListVo(index).TsukurikataNounyu
                    End If
                    ''↑↑2014/09/03 Ⅰ.2.管理項目追加_bb) 酒井 ADD END
                    '作り方部品製作規模・概要'
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KIBO - 1) = aKihonListVo(index).TsukurikataKibo
                    ''↑↑2014/07/25 Ⅰ.2.管理項目追加_bb) (TES)張 ADD END

                    '材質規格1'
                    dataMatrix(rowIndex, COLUMN_ZAISHITSU_KIKAKU_1 - 1) = aKihonListVo(index).ZaishituKikaku1
                    '材質規格2'
                    dataMatrix(rowIndex, COLUMN_ZAISHITSU_KIKAKU_2 - 1) = aKihonListVo(index).ZaishituKikaku2
                    '材質規格3'
                    dataMatrix(rowIndex, COLUMN_ZAISHITSU_KIKAKU_3 - 1) = aKihonListVo(index).ZaishituKikaku3
                    '材質メッキ'
                    dataMatrix(rowIndex, COLUMN_ZAISHITSU_MEKKI - 1) = aKihonListVo(index).ZaishituMekki
                    '板厚数量'
                    dataMatrix(rowIndex, COLUMN_BANKO_SURYO - 1) = aKihonListVo(index).ShisakuBankoSuryo
                    '板厚数量u'
                    dataMatrix(rowIndex, COLUMN_BANKO_SURYO_U - 1) = aKihonListVo(index).ShisakuBankoSuryoU
                    '↓↓↓2014/12/30 メタル項目を追加 TES)張 ADD BEGIN
                    dataMatrix(rowIndex, COLUMN_MATERIAL_INFO_LENGTH - 1) = strNVL(aKihonListVo(index).MaterialInfoLength)
                    dataMatrix(rowIndex, COLUMN_MATERIAL_INFO_WIDTH - 1) = strNVL(aKihonListVo(index).MaterialInfoWidth)
                    dataMatrix(rowIndex, COLUMN_DATA_ITEM_KAITEI_NO - 1) = strNVL(aKihonListVo(index).DataItemKaiteiNo)
                    dataMatrix(rowIndex, COLUMN_DATA_ITEM_AREA_NAME - 1) = strNVL(aKihonListVo(index).DataItemAreaName)
                    dataMatrix(rowIndex, COLUMN_DATA_ITEM_SET_NAME - 1) = strNVL(aKihonListVo(index).DataItemSetName)
                    dataMatrix(rowIndex, COLUMN_DATA_ITEM_KAITEI_INFO - 1) = strNVL(aKihonListVo(index).DataItemKaiteiInfo)
                    '↑↑↑2014/12/30 メタル項目を追加 TES)張 ADD END


                    '材料寸法_X(mm)'
                    dataMatrix(rowIndex, COLUMN_ZAIRYO_SUNPO_X - 1) = strNVL(aKihonListVo(index).ZairyoSunpoX)
                    '材料寸法_Y(mm)'
                    dataMatrix(rowIndex, COLUMN_ZAIRYO_SUNPO_Y - 1) = strNVL(aKihonListVo(index).ZairyoSunpoY)
                    '材料寸法_Z(mm)'
                    dataMatrix(rowIndex, COLUMN_ZAIRYO_SUNPO_Z - 1) = strNVL(aKihonListVo(index).ZairyoSunpoZ)
                    '材料寸法_XY(mm)'
                    dataMatrix(rowIndex, COLUMN_ZAIRYO_SUNPO_XY - 1) = strNVL(aKihonListVo(index).ZairyoSunpoXy)
                    '材料寸法_XZ(mm)'
                    dataMatrix(rowIndex, COLUMN_ZAIRYO_SUNPO_XZ - 1) = strNVL(aKihonListVo(index).ZairyoSunpoXz)
                    '材料寸法_YZ(mm)'
                    dataMatrix(rowIndex, COLUMN_ZAIRYO_SUNPO_YZ - 1) = strNVL(aKihonListVo(index).ZairyoSunpoYz)



                    '試作部品費'
                    dataMatrix(rowIndex, COLUMN_SHISAKU_BUHIN_HI - 1) = aKihonListVo(index).ShisakuBuhinnHi
                    '試作型費'
                    dataMatrix(rowIndex, COLUMN_SHISAKU_KATA_HI - 1) = aKihonListVo(index).ShisakuKataHi
                    '備考'
                    dataMatrix(rowIndex, COLUMN_BIKOU - 1) = aKihonListVo(index).Bikou

                    '員数'
                    If aKihonListVo(index).InsuSuryo < 0 Then
                        dataMatrix(rowIndex, COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun - 1) = "**"
                    ElseIf aKihonListVo(index).InsuSuryo > 0 Then
                        dataMatrix(rowIndex, COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun - 1) = aKihonListVo(index).InsuSuryo
                    End If

                    COLUMN_END_GOUSYA = EndCol


                    '親品番'
                    dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = aKihonListVo(index).BuhinNoOya
                    '親品番試作区分'
                    dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = aKihonListVo(index).BuhinNoKbnOya



                    'ここからAS400'
                    Dim AsPrpf02Vo As New AsPRPF02Vo
                    '結合Noと旧リストコードを元にAS部品を取得'
                    '中身が無い'
                    If Not StringUtil.IsEmpty(aKihonListVo(index).KetugouNo) Then
                        AsPrpf02Vo = Impl.FindByBuhinFile(aKihonListVo(index).ShisakuEventCode, _
                                  aKihonListVo(index).ShisakuListCode, _
                                  aKihonListVo(index).ShisakuListCodeKaiteiNo, _
                                  aKihonListVo(index).KetugouNo)
                        If Not AsPrpf02Vo Is Nothing Then

                            Dim orpf32Vos As New List(Of AsORPF32Vo)
                            Dim orpf57Vos As New List(Of AsORPF57Vo)

                            Dim orpf60Vo As AsORPF60Vo = Nothing
                            Dim orpf61vo As AsORPF61Vo = Nothing

                            Dim orpf57Vo As AsORPF57Vo = Nothing
                            Dim orpf57Nokm7VO As AsORPF57Vo = Nothing
                            Dim orpf32Vo As AsORPF32Vo = Nothing
                            Dim orpf32Nokm7Vo As AsORPF32Vo = Nothing
                            orpf32Vos = Impl.FindByORPF32(AsPrpf02Vo.Koba, AsPrpf02Vo.Gyoid, AsPrpf02Vo.Bnba, KoujiShireiNo)

                            For Each Temporpf32Vo As AsORPF32Vo In orpf32Vos
                                If Temporpf32Vo.Nokm = "5" Or Temporpf32Vo.Nokm = "6" Then
                                    orpf32Vo = Temporpf32Vo
                                ElseIf Temporpf32Vo.Nokm = "7" Then
                                    orpf32Nokm7Vo = Temporpf32Vo
                                End If
                            Next
                            If Not orpf32Vo Is Nothing Then
                                orpf60Vo = Impl.FindByORPF60(orpf32Vo.Sgisba, orpf32Vo.Kbba, orpf32Vo.Cmba, orpf32Vo.Nokm, orpf32Vo.Haym)
                            Else
                                orpf57Vos = Impl.FindByORPF57(AsPrpf02Vo.OldListCode, AsPrpf02Vo.Kbba, AsPrpf02Vo.Gyoid)
                                For Each Temporpf57Vo As AsORPF57Vo In orpf57Vos
                                    If Temporpf57Vo.Nokm = "5" Or Temporpf57Vo.Nokm = "6" Then
                                        orpf57Vo = Temporpf57Vo
                                    ElseIf Temporpf57Vo.Nokm = "7" Then
                                        orpf57Nokm7VO = Temporpf57Vo
                                    End If
                                Next

                                If Not orpf57Vo Is Nothing Then
                                    orpf61vo = Impl.FindByORPF61(orpf57Vo.Grno, orpf57Vo.Srno)
                                End If
                            End If

                            SetAs400(orpf32Vo, orpf57Vo, orpf60Vo, orpf61vo, orpf32Nokm7Vo, orpf57Nokm7VO)

                            '発行No.'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _HakkoNo
                            '発注年月日'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _HacyuDate
                            '同期'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _Doki
                            '分納'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _Bunno
                            'ネック'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _Neck
                            '暫定・欠品'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _Zank
                            'その他'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _Sonota
                            '備考'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _Biko
                            '納期回答１'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NokiKaito1
                            '納入予定数１'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NonyuYotei1
                            '納入区分'  
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NonyuKbn
                            '検収年月日'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _KensyuDate
                            '納入累計数'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NonyuTotal
                            '引取り検収年月日'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _HikitoriDate
                            '引取り累計数'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _HikitoriTotal
                            '納入実績マーク'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NonyuJissekiMark
                            '取消年月日'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _TorikeshiDate
                            '取消数'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _TorikeshiTotal
                            '納期回答２'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NokiKaito2
                            '納入予定数２'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NonyuYotei2
                            '納期回答３'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NokiKaito3
                            '納入予定数３'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NonyuYotei3
                            '納期回答４'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NokiKaito4
                            '納入予定数４'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NonyuYotei4
                            '納期回答５'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NokiKaito5
                            '納入予定数５'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NonyuYotei5
                            '納期回答６'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NokiKaito6
                            '納入予定数６'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NonyuYotei6
                            '納期回答７'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NokiKaito7
                            '納入予定数７'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NonyuYotei7
                            '納期回答８'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NokiKaito8
                            '納入予定数８'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _NonyuYotei8
                            '処置'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _Syochi
                            '理由'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _Riyu
                            '対応'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _Taio
                            '部署'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _Busho
                            '設計担当者'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _SekkeiTantosya
                            'ＴＥＬ'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _Tel
                            '暫定品納入日' 
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _ZanteiHinNonyubi
                            '正規扱いOr後交換有り'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _SeikiOrKoukan
                            '設通No(最新)'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _StsrNew
                            '設通No(実績)'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _StsrJisseki
                            '出図予定日(最新)'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _ShutuzuYoteiDate
                            '出図実績日'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _ShutuzuJissekiDate
                            '型'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _Kata
                            '工法'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _Koho
                            'ﾒｰｶｰ見積り型費(円)'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _MakerKatahi
                            'ﾒｰｶｰ見積り部品費(円)'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _MakerBuhinHi
                            '工事区分'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _KoujiKbn
                            '予算区分'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _YosanKbn
                            '手番'
                            dataMatrix(rowIndex, EzUtil.Increment(COLUMN_END_GOUSYA) - 1) = _Teban

                        End If

                    End If


                    rowIndex = rowIndex + 1

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
        ''' <param name="aKihonListVo">手配基本情報リスト</param>
        ''' <param name="aBaseListVo">ベース車情報リスト</param>
        ''' <remarks></remarks>
        Private Sub SetBodyOLD(ByVal xls As ShisakuExcel, ByVal aKihonListVo As List(Of TShisakuTehaiKihonVoHelper), _
                            ByVal aBaseListVo As List(Of TShisakuEventBaseVo))

            Dim rowIndex As Integer = 0
            Dim MergeFlag As Boolean = False
            Dim wUnitKbn As String = "9"


            For index As Integer = 0 To aKihonListVo.Count - 1

                If Not index = 0 Then
                    If StringUtil.Equals(aKihonListVo(index).BuhinNo, aKihonListVo(index - 1).BuhinNo) Then
                        rowIndex = rowIndex - 1
                        MergeFlag = True
                    End If
                End If

                If MergeFlag Then
                    '員数'
                    If aKihonListVo(index).InsuSuryo < 0 Then
                        xls.SetOrientation(COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun, _
                                           START_ROW + rowIndex, _
                                           COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun, _
                                           START_ROW + rowIndex, ShisakuExcel.XlOrientation.xlHorizontal)

                        xls.SetValue(COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, "**")
                    Else
                        xls.SetOrientation(COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun, _
                                           START_ROW + rowIndex, _
                                           COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun, _
                                           START_ROW + rowIndex, ShisakuExcel.XlOrientation.xlHorizontal)

                        xls.SetValue(COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, aKihonListVo(index).InsuSuryo)

                    End If
                    MergeFlag = False
                    rowIndex = rowIndex + 1
                Else

                    '履歴'
                    xls.SetValue(COLUMN_RIREKI, START_ROW + rowIndex, aKihonListVo(index).Rireki)
                    'キャンセル実績'
                    xls.SetValue(COLUMN_CANCEL_JISSEKI, START_ROW + rowIndex, "")
                    'ブロックNo'
                    xls.SetValue(COLUMN_BLOCK_NO, START_ROW + rowIndex, aKihonListVo(index).ShisakuBlockNo)
                    '行ID'
                    xls.SetValue(COLUMN_GYOU_ID, START_ROW + rowIndex, aKihonListVo(index).GyouId)
                    '専用マーク'
                    xls.SetValue(COLUMN_SENYOU_MARK, START_ROW + rowIndex, aKihonListVo(index).SenyouMark)
                    'レベル'
                    xls.SetValue(COLUMN_LEVEL, START_ROW + rowIndex, aKihonListVo(index).Level)

                    'ユニット区分'
                    'ユニット区分は試作ブロック情報から取得する。


                    If Not index = 0 Then
                        If Not StringUtil.Equals(aKihonListVo(index).ShisakuBlockNo, aKihonListVo(index - 1).ShisakuBlockNo) Then
                            wUnitKbn = Impl.FindByShisakuBlockNo(aKihonListVo(index).ShisakuEventCode, _
                                     aKihonListVo(index).ShisakuBukaCode, _
                                     aKihonListVo(index).ShisakuBlockNo)
                        End If
                    Else
                        wUnitKbn = Impl.FindByShisakuBlockNo(aKihonListVo(index).ShisakuEventCode, _
                                     aKihonListVo(index).ShisakuBukaCode, _
                                     aKihonListVo(index).ShisakuBlockNo)
                    End If

                    'xls.SetValue(COLUMN_UNIT_KBN, START_ROW + rowIndex, aKihonListVo(index).UnitKbn)
                    xls.SetValue(COLUMN_UNIT_KBN, START_ROW + rowIndex, wUnitKbn)


                    '集計コード'
                    xls.SetValue(COLUMN_SHUKEI_CODE, START_ROW + rowIndex, aKihonListVo(index).ShukeiCode)
                    '手配記号'
                    xls.SetValue(COLUMN_TEHAI_KIGOU, START_ROW + rowIndex, aKihonListVo(index).TehaiKigou)
                    '購担'
                    xls.SetValue(COLUMN_KOUTAN, START_ROW + rowIndex, aKihonListVo(index).Koutan)
                    '取引先コード'
                    xls.SetValue(COLUMN_TORIHIKISAKI_CODE, START_ROW + rowIndex, aKihonListVo(index).TorihikisakiCode)
                    '取引先名称'
                    xls.SetValue(COLUMN_TORIHIKISAKI_NAME, START_ROW + rowIndex, aKihonListVo(index).MakerCode)
                    '部品番号'
                    xls.SetValue(COLUMN_BUHIN_NO, START_ROW + rowIndex, aKihonListVo(index).BuhinNo)
                    '試作区分'
                    xls.SetValue(COLUMN_SHISAKU_KBN, START_ROW + rowIndex, aKihonListVo(index).BuhinNoKbn)
                    '改訂No'
                    xls.SetValue(COLUMN_KAITEI_NO, START_ROW + rowIndex, aKihonListVo(index).BuhinNoKaiteiNo)
                    '枝番'
                    xls.SetValue(COLUMN_EDA_BAN, START_ROW + rowIndex, aKihonListVo(index).EdaBan)
                    '部品名称'
                    xls.SetValue(COLUMN_BUHIN_NAME, START_ROW + rowIndex, aKihonListVo(index).BuhinName)
                    '納入指示日'
                    If aKihonListVo(index).NounyuShijibi = 0 Then
                        xls.SetValue(COLUMN_NOUNYUSHIJIBI, START_ROW + rowIndex, "")
                    Else
                        xls.SetValue(COLUMN_NOUNYUSHIJIBI, START_ROW + rowIndex, aKihonListVo(index).NounyuShijibi)
                    End If

                    '納入指示数'
                    If aKihonListVo(index).TotalInsuSuryo < 0 Then
                        xls.SetValue(COLUMN_NOUNYUSHIJISU, START_ROW + rowIndex, "**")
                    Else
                        xls.SetValue(COLUMN_NOUNYUSHIJISU, START_ROW + rowIndex, aKihonListVo(index).TotalInsuSuryo)
                    End If
                    '納入場所'
                    xls.SetValue(COLUMN_NOUNYUBASYO, START_ROW + rowIndex, aKihonListVo(index).Nouba)
                    '供給セクション'
                    xls.SetValue(COLUMN_KYOUKYU_SECTION, START_ROW + rowIndex, aKihonListVo(index).KyoukuSection)
                    '再使用不可'
                    xls.SetValue(COLUMN_SAISHIYOUFUKA, START_ROW + rowIndex, aKihonListVo(index).Saishiyoufuka)

                    '材質規格1'
                    xls.SetValue(COLUMN_ZAISHITSU_KIKAKU_1, START_ROW + rowIndex, aKihonListVo(index).ZaishituKikaku1)
                    '材質規格2'
                    xls.SetValue(COLUMN_ZAISHITSU_KIKAKU_2, START_ROW + rowIndex, aKihonListVo(index).ZaishituKikaku2)
                    '材質規格3'
                    xls.SetValue(COLUMN_ZAISHITSU_KIKAKU_3, START_ROW + rowIndex, aKihonListVo(index).ZaishituKikaku3)
                    '材質メッキ'
                    xls.SetValue(COLUMN_ZAISHITSU_MEKKI, START_ROW + rowIndex, aKihonListVo(index).ZaishituMekki)
                    '板厚数量'
                    xls.SetValue(COLUMN_BANKO_SURYO, START_ROW + rowIndex, aKihonListVo(index).ShisakuBankoSuryo)
                    '板厚数量u'
                    xls.SetValue(COLUMN_BANKO_SURYO_U, START_ROW + rowIndex, aKihonListVo(index).ShisakuBankoSuryoU)
                    '↓↓↓2014/12/30 メタル項目を追加 TES)張 ADD BEGIN
                    xls.SetValue(COLUMN_MATERIAL_INFO_LENGTH, START_ROW + rowIndex, aKihonListVo(index).MaterialInfoLength)
                    xls.SetValue(COLUMN_MATERIAL_INFO_WIDTH, START_ROW + rowIndex, aKihonListVo(index).MaterialInfoWidth)
                    xls.SetValue(COLUMN_DATA_ITEM_KAITEI_NO, START_ROW + rowIndex, aKihonListVo(index).DataItemKaiteiNo)
                    xls.SetValue(COLUMN_DATA_ITEM_AREA_NAME, START_ROW + rowIndex, aKihonListVo(index).DataItemAreaName)
                    xls.SetValue(COLUMN_DATA_ITEM_SET_NAME, START_ROW + rowIndex, aKihonListVo(index).DataItemSetName)
                    xls.SetValue(COLUMN_DATA_ITEM_KAITEI_INFO, START_ROW + rowIndex, aKihonListVo(index).DataItemKaiteiInfo)
                    '↑↑↑2014/12/30 メタル項目を追加 TES)張 ADD END
                    '試作部品費'
                    xls.SetValue(COLUMN_SHISAKU_BUHIN_HI, START_ROW + rowIndex, aKihonListVo(index).ShisakuBuhinnHi)
                    '試作型費'
                    xls.SetValue(COLUMN_SHISAKU_KATA_HI, START_ROW + rowIndex, aKihonListVo(index).ShisakuKataHi)
                    '備考'
                    xls.SetValue(COLUMN_BIKOU, START_ROW + rowIndex, aKihonListVo(index).Bikou)

                    '員数'
                    If aKihonListVo(index).InsuSuryo < 0 Then
                        xls.SetValue(COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, "**")
                    Else
                        xls.SetValue(COLUMN_START_GOUSYA + aKihonListVo(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, aKihonListVo(index).InsuSuryo)
                    End If

                    COLUMN_END_GOUSYA = EndCol


                    '親品番'
                    xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, aKihonListVo(index).BuhinNoOya)
                    '親品番試作区分'
                    xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, aKihonListVo(index).BuhinNoKbnOya)



                    'ここからAS400'
                    Dim AsPrpf02Vo As New AsPRPF02Vo
                    '結合Noと旧リストコードを元にAS部品を取得'
                    '中身が無い'
                    If Not StringUtil.IsEmpty(aKihonListVo(index).KetugouNo) Then
                        AsPrpf02Vo = Impl.FindByBuhinFile(aKihonListVo(index).ShisakuEventCode, _
                                  aKihonListVo(index).ShisakuListCode, _
                                  aKihonListVo(index).ShisakuListCodeKaiteiNo, _
                                  aKihonListVo(index).KetugouNo)
                        If Not AsPrpf02Vo Is Nothing Then

                            Dim orpf32Vo As New AsORPF32Vo
                            Dim orpf57Vo As New AsORPF57Vo
                            Dim orpf60Vo As New AsORPF60Vo
                            Dim orpf61vo As New AsORPF61Vo

                            'orpf32Vo = Impl.FindByORPF32(AsPrpf02Vo.Gyoid, AsPrpf02Vo.Kbba, AsPrpf02Vo.Bnba)
                            If Not orpf32Vo Is Nothing Then
                                orpf60Vo = Impl.FindByORPF60(orpf32Vo.Sgisba, orpf32Vo.Kbba, orpf32Vo.Cmba, orpf32Vo.Nokm, orpf32Vo.Haym)
                            Else
                                'orpf57Vo = Impl.FindByORPF57(AsPrpf02Vo.OldListCode, AsPrpf02Vo.Kbba, AsPrpf02Vo.Gyoid)
                                If Not orpf57Vo Is Nothing Then
                                    orpf61vo = Impl.FindByORPF61(orpf57Vo.Grno, orpf57Vo.Srno)
                                End If
                            End If

                            'SetAs400(orpf32Vo, orpf57Vo, orpf60Vo, orpf61vo, orpf32Vo)

                            '発行No.'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _HakkoNo)
                            '発注年月日'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _HacyuDate)
                            '同期'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _Doki)
                            '分納'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _Bunno)
                            'ネック'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _Neck)
                            '暫定・欠品'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _Zank)
                            'その他'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _Sonota)
                            '備考'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _Biko)
                            '納期回答１'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NokiKaito1)
                            '納入予定数１'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NonyuYotei1)
                            '納入区分'  
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NonyuKbn)
                            '検収年月日'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _KensyuDate)
                            '納入累計数'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NonyuTotal)
                            '引取り検収年月日'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _HikitoriDate)
                            '引取り累計数'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _HikitoriTotal)
                            '納入実績マーク'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NonyuJissekiMark)
                            '取消年月日'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _TorikeshiDate)
                            '取消数'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _TorikeshiTotal)
                            '納期回答２'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NokiKaito2)
                            '納入予定数２'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NonyuYotei2)
                            '納期回答３'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NokiKaito3)
                            '納入予定数３'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NonyuYotei3)
                            '納期回答４'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NokiKaito4)
                            '納入予定数４'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NonyuYotei4)
                            '納期回答５'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NokiKaito5)
                            '納入予定数５'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NonyuYotei5)
                            '納期回答６'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NokiKaito6)
                            '納入予定数６'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NonyuYotei6)
                            '納期回答７'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NokiKaito7)
                            '納入予定数７'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NonyuYotei7)
                            '納期回答８'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NokiKaito8)
                            '納入予定数８'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _NonyuYotei8)
                            '処置'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _Syochi)
                            '理由'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _Riyu)
                            '対応'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _Taio)
                            '部署'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _Busho)
                            '設計担当者'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _SekkeiTantosya)
                            'ＴＥＬ'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _Tel)
                            '暫定品納入日' 
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _ZanteiHinNonyubi)
                            '正規扱いOr後交換有り'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _SeikiOrKoukan)
                            '設通No(最新)'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _StsrNew)
                            '設通No(実績)'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _StsrJisseki)
                            '出図予定日(最新)'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _ShutuzuYoteiDate)
                            '出図実績日'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _ShutuzuJissekiDate)
                            '型'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _Kata)
                            '工法'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _Koho)
                            'ﾒｰｶｰ見積り型費(円)'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _MakerKatahi)
                            'ﾒｰｶｰ見積り部品費(円)'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _MakerBuhinHi)
                            '工事区分'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _KoujiKbn)
                            '予算区分'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _YosanKbn)
                            '手番'
                            xls.SetValue(EzUtil.Increment(COLUMN_END_GOUSYA), START_ROW + rowIndex, _Teban)

                        End If

                    End If


                    rowIndex = rowIndex + 1

                End If

            Next

        End Sub
        ''' <summary>
        ''' 各カラム名に数値を設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetColumnNo()
            Dim column As Integer = 1
            COLUMN_RIREKI = EzUtil.Increment(column)
            COLUMN_CANCEL_JISSEKI = EzUtil.Increment(column)
            COLUMN_BLOCK_NO = EzUtil.Increment(column)
            COLUMN_GYOU_ID = EzUtil.Increment(column)
            COLUMN_SENYOU_MARK = EzUtil.Increment(column)
            COLUMN_LEVEL = EzUtil.Increment(column)
            COLUMN_UNIT_KBN = EzUtil.Increment(column)
            COLUMN_SHUKEI_CODE = EzUtil.Increment(column)
            COLUMN_TEHAI_KIGOU = EzUtil.Increment(column)
            COLUMN_KOUTAN = EzUtil.Increment(column)
            COLUMN_TORIHIKISAKI_CODE = EzUtil.Increment(column)
            COLUMN_TORIHIKISAKI_NAME = EzUtil.Increment(column)
            COLUMN_BUHIN_NO = EzUtil.Increment(column)
            COLUMN_SHISAKU_KBN = EzUtil.Increment(column)
            COLUMN_KAITEI_NO = EzUtil.Increment(column)
            COLUMN_EDA_BAN = EzUtil.Increment(column)
            COLUMN_BUHIN_NAME = EzUtil.Increment(column)
            COLUMN_NOUNYUSHIJIBI = EzUtil.Increment(column)
            COLUMN_NOUNYUSHIJISU = EzUtil.Increment(column)
            COLUMN_NOUNYUBASYO = EzUtil.Increment(column)
            COLUMN_KYOUKYU_SECTION = EzUtil.Increment(column)
            COLUMN_SAISHIYOUFUKA = EzUtil.Increment(column)
            ''↓↓2014/07/25 Ⅰ.2.管理項目追加_az) (TES)張 ADD BEGIN
            COLUMN_TSUKURIKATA_SEISAKU = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KATASHIYOU1 = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KATASHIYOU2 = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KATASHIYOU3 = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_TIGU = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_NOUNYU = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KIBO = EzUtil.Increment(column)
            ''↑↑2014/07/25 Ⅰ.2.管理項目追加_az) (TES)張 ADD END
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

            COLUMN_ZAIRYO_SUNPO_X = EzUtil.Increment(column)
            COLUMN_ZAIRYO_SUNPO_Y = EzUtil.Increment(column)
            COLUMN_ZAIRYO_SUNPO_Z = EzUtil.Increment(column)
            COLUMN_ZAIRYO_SUNPO_XY = EzUtil.Increment(column)
            COLUMN_ZAIRYO_SUNPO_XZ = EzUtil.Increment(column)
            COLUMN_ZAIRYO_SUNPO_YZ = EzUtil.Increment(column)

            COLUMN_SHISAKU_BUHIN_HI = EzUtil.Increment(column)
            COLUMN_SHISAKU_KATA_HI = EzUtil.Increment(column)
            COLUMN_BIKOU = EzUtil.Increment(column)
            COLUMN_START_GOUSYA = EzUtil.Increment(column)

        End Sub

        ''' <summary>
        ''' AS400データのセット
        ''' </summary>
        ''' <param name="orpf32Vo"></param>
        ''' <param name="orpf57Vo"></param>
        ''' <param name="orpf60Vo"></param>
        ''' <param name="orpf61Vo"></param>
        ''' <param name="orpf32Nokm7Vo"></param>
        ''' <param name="orpf57Nokm7Vo"></param>
        ''' <remarks></remarks>
        Private Sub SetAs400(ByVal orpf32Vo As AsORPF32Vo, _
                             ByVal orpf57Vo As AsORPF57Vo, _
                             ByVal orpf60Vo As AsORPF60Vo, _
                             ByVal orpf61Vo As AsORPF61Vo, _
                             ByVal orpf32Nokm7Vo As AsORPF32Vo, _
                             ByVal orpf57Nokm7Vo As AsORPF57Vo)

            NewAS400()

            If Not orpf32Vo Is Nothing Then
                '32と60使用'
                _HakkoNo = Trim(StringUtil.Nvl(orpf32Vo.Cmba))
                _HacyuDate = Trim(StringUtil.Nvl(orpf32Vo.Haym))
                If Not orpf60Vo Is Nothing Then
                    _Doki = Trim(StringUtil.Nvl(orpf60Vo.Doki))
                    _Bunno = Trim(StringUtil.Nvl(orpf60Vo.Bunno))
                    _Neck = Trim(StringUtil.Nvl(orpf60Vo.Neck))
                    _Zank = Trim(StringUtil.Nvl(orpf60Vo.Zantei))
                    _Sonota = Trim(StringUtil.Nvl(orpf60Vo.Other))
                    _Biko = Trim(StringUtil.Nvl(orpf60Vo.Newbiko))
                    _NokiKaito3 = Trim(StringUtil.Nvl(orpf60Vo.Nqans3))
                    _NonyuYotei3 = Trim(StringUtil.Nvl(orpf60Vo.Ytca3))
                    _NokiKaito4 = Trim(StringUtil.Nvl(orpf60Vo.Nqans4))
                    _NonyuYotei4 = Trim(StringUtil.Nvl(orpf60Vo.Ytca4))
                    _NokiKaito5 = Trim(StringUtil.Nvl(orpf60Vo.Nqans5))
                    _NonyuYotei5 = Trim(StringUtil.Nvl(orpf60Vo.Ytca5))
                    _NokiKaito6 = Trim(StringUtil.Nvl(orpf60Vo.Nqans6))
                    _NonyuYotei6 = Trim(StringUtil.Nvl(orpf60Vo.Ytca6))
                    _NokiKaito7 = Trim(StringUtil.Nvl(orpf60Vo.Nqans7))
                    _NonyuYotei7 = Trim(StringUtil.Nvl(orpf60Vo.Ytca7))
                    _NokiKaito8 = Trim(StringUtil.Nvl(orpf60Vo.Nqans8))
                    _NonyuYotei8 = Trim(StringUtil.Nvl(orpf60Vo.Ytca8))
                    _Syochi = Trim(StringUtil.Nvl(orpf60Vo.Syoti))
                    _Riyu = Trim(StringUtil.Nvl(orpf60Vo.Riyu))
                    _Taio = Trim(StringUtil.Nvl(orpf60Vo.Taio))
                    _Busho = Trim(StringUtil.Nvl(orpf60Vo.Busho))
                    _SekkeiTantosya = Trim(StringUtil.Nvl(orpf60Vo.Tanto))
                    _Tel = Trim(StringUtil.Nvl(orpf60Vo.Tel))
                    _ZanteiHinNonyubi = Trim(StringUtil.Nvl(orpf60Vo.Zanonyu))
                    _SeikiOrKoukan = Trim(StringUtil.Nvl(orpf60Vo.Seiki))
                    _StsrNew = Trim(StringUtil.Nvl(orpf60Vo.Nwstba))
                    _StsrJisseki = Trim(StringUtil.Nvl(orpf60Vo.Jistba))
                    _ShutuzuYoteiDate = Trim(StringUtil.Nvl(orpf60Vo.Nwyozp))
                    _ShutuzuJissekiDate = Trim(StringUtil.Nvl(orpf60Vo.Jizpbi))
                    _Kata = Trim(StringUtil.Nvl(orpf60Vo.Kata))
                    _Koho = Trim(StringUtil.Nvl(orpf60Vo.Koho))
                    _MakerBuhinHi = Trim(StringUtil.Nvl(orpf60Vo.Buhinhi))
                    _MakerKatahi = Trim(StringUtil.Nvl(orpf60Vo.Katahi))
                    _Teban = Trim(StringUtil.Nvl(orpf60Vo.Teban))
                End If
                _NokiKaito1 = Trim(StringUtil.Nvl(orpf32Vo.Nqans1))
                _NonyuYotei1 = Trim(StringUtil.Nvl(orpf32Vo.Ytca1))
                _NonyuKbn = Trim(StringUtil.Nvl(orpf32Vo.Nokm))
                '2012/03/13 
                _KensyuDate = Trim(StringUtil.Nvl(orpf32Vo.Noym))
                _NonyuTotal = Trim(StringUtil.Nvl(orpf32Vo.Noru))
                If Not orpf32Nokm7Vo Is Nothing Then
                    _HikitoriDate = Trim(StringUtil.Nvl(orpf32Nokm7Vo.Noym))
                    _HikitoriTotal = Trim(StringUtil.Nvl(orpf32Nokm7Vo.Noru))
                Else
                    _HikitoriDate = ""
                    _HikitoriTotal = ""
                End If
                'If StringUtil.Equals(_NonyuKbn, "7") Then
                '    _KensyuDate = ""
                '    _NonyuTotal = ""
                '    _HikitoriDate = Trim(orpf32Vo.Noym)
                '    _HikitoriTotal = Trim(orpf32Vo.Noru)
                'Else
                '    _KensyuDate = Trim(orpf32Vo.Noym)
                '    _NonyuTotal = Trim(orpf32Vo.Noru)
                '    _HikitoriDate = ""
                '    _HikitoriTotal = ""
                'End If
                _TorikeshiDate = Trim(StringUtil.Nvl(orpf32Vo.Tlym))
                _TorikeshiTotal = Trim(StringUtil.Nvl(orpf32Vo.Tlca))
                _NokiKaito2 = Trim(StringUtil.Nvl(orpf32Vo.Nqans2))
                _NonyuYotei2 = Trim(StringUtil.Nvl(orpf32Vo.Ytca2))
                _KoujiKbn = Trim(StringUtil.Nvl(orpf32Vo.Kokm))
                _YosanKbn = Trim(StringUtil.Nvl(orpf32Vo.Koba))

            ElseIf Not orpf57Vo Is Nothing Then
                '57と61'
                _HakkoNo = Trim(StringUtil.Nvl(orpf57Vo.Edono))
                _HacyuDate = Trim(StringUtil.Nvl(orpf57Vo.Haym))
                If Not orpf61Vo Is Nothing Then
                    _Doki = ""
                    _Bunno = ""
                    _Neck = Trim(StringUtil.Nvl(orpf61Vo.Neck))
                    _Zank = Trim(StringUtil.Nvl(orpf61Vo.Zantei))
                    _Sonota = Trim(StringUtil.Nvl(orpf61Vo.Other))
                    _Biko = Trim(StringUtil.Nvl(orpf61Vo.Newbiko))
                    _NokiKaito3 = ""
                    _NonyuYotei3 = ""
                    _NokiKaito4 = ""
                    _NonyuYotei4 = ""
                    _NokiKaito5 = ""
                    _NonyuYotei5 = ""
                    _NokiKaito6 = ""
                    _NonyuYotei6 = ""
                    _NokiKaito7 = ""
                    _NonyuYotei7 = ""
                    _NokiKaito8 = ""
                    _NonyuYotei8 = ""
                    _Syochi = Trim(StringUtil.Nvl(orpf61Vo.Syoti))
                    _Riyu = Trim(StringUtil.Nvl(orpf61Vo.Riyu))
                    _Taio = Trim(StringUtil.Nvl(orpf61Vo.Taio))
                    _Busho = Trim(StringUtil.Nvl(orpf61Vo.Busho))
                    _SekkeiTantosya = Trim(StringUtil.Nvl(orpf61Vo.Tanto))
                    _Tel = Trim(StringUtil.Nvl(orpf61Vo.Tel))
                    _ZanteiHinNonyubi = Trim(StringUtil.Nvl(orpf61Vo.Zanonyu))
                    _SeikiOrKoukan = Trim(StringUtil.Nvl(orpf61Vo.Seiki))
                    _StsrNew = ""
                    _StsrJisseki = ""
                    _ShutuzuYoteiDate = Trim(StringUtil.Nvl(orpf61Vo.Nwyozp))
                    _ShutuzuJissekiDate = Trim(StringUtil.Nvl(orpf61Vo.Jizpbi))
                    _Kata = ""
                    _Koho = ""
                    _MakerBuhinHi = ""
                    _MakerKatahi = ""
                    _Teban = ""
                End If
                _NokiKaito1 = Trim(StringUtil.Nvl(orpf57Vo.Enqans))
                _NonyuYotei1 = Trim(StringUtil.Nvl(orpf57Vo.Enoytca))
                _NonyuKbn = Trim(StringUtil.Nvl(orpf57Vo.Nokm))
                '2012/03/13 
                _KensyuDate = Trim(StringUtil.Nvl(orpf57Vo.Noym))
                _NonyuTotal = Trim(StringUtil.Nvl(orpf57Vo.Noru))
                If Not orpf57Nokm7Vo Is Nothing Then
                    _HikitoriDate = Trim(StringUtil.Nvl(orpf32Nokm7Vo.Noym))
                    _HikitoriTotal = Trim(StringUtil.Nvl(orpf32Nokm7Vo.Noru))
                Else
                    _HikitoriDate = ""
                    _HikitoriTotal = ""
                End If
                'If StringUtil.Equals(_NonyuKbn, "7") Then
                '    _KensyuDate = ""
                '    _NonyuTotal = ""
                '    _HikitoriDate = Trim(orpf57Vo.Noym)
                '    _HikitoriTotal = Trim(orpf57Vo.Noru)
                'Else
                '    _KensyuDate = Trim(orpf57Vo.Noym)
                '    _NonyuTotal = Trim(orpf57Vo.Noru)
                '    _HikitoriDate = ""
                '    _HikitoriTotal = ""
                'End If
                _TorikeshiDate = Trim(StringUtil.Nvl(orpf57Vo.Tlym))
                _TorikeshiTotal = Trim(StringUtil.Nvl(orpf57Vo.Tlca))
                _NokiKaito2 = ""
                _NonyuYotei2 = ""
                _KoujiKbn = Trim(StringUtil.Nvl(orpf57Vo.Kokm))
                _YosanKbn = Trim(StringUtil.Nvl(orpf57Vo.Koba))

            End If

        End Sub

        ''' <summary>
        ''' 行と列の調整
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub SetColumnRow(ByVal xls As ShisakuExcel)

            xls.AutoFitCol(COLUMN_BLOCK_NO, xls.EndCol)
            xls.SetRowHeight(INSU_ROW, INSU_ROW, 104)
            xls.SetColWidth(1, 1, 8.5) '履歴
            xls.SetColWidth(6, 6, 8.5) 'レベル
            xls.SetColWidth(Bikou, Bikou, 40) '現行システム備考

        End Sub

        ''' <summary>
        ''' AS400の部品を初期化
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub NewAS400()

            _HakkoNo = ""
            _HacyuDate = ""
            _Doki = ""
            _Bunno = ""
            _Neck = ""
            _Zank = ""
            _Sonota = ""
            _Biko = ""
            _NokiKaito1 = ""
            _NonyuYotei1 = ""
            _NonyuKbn = ""
            _KensyuDate = ""
            _NonyuTotal = ""
            _HikitoriDate = ""
            _HikitoriTotal = ""
            _NonyuJissekiMark = ""
            _TorikeshiDate = ""
            _TorikeshiTotal = ""
            _NokiKaito2 = ""
            _NonyuYotei2 = ""
            _NokiKaito3 = ""
            _NonyuYotei3 = ""
            _NokiKaito4 = ""
            _NonyuYotei4 = ""
            _NokiKaito5 = ""
            _NonyuYotei5 = ""
            _NokiKaito6 = ""
            _NonyuYotei6 = ""
            _NokiKaito7 = ""
            _NonyuYotei7 = ""
            _NokiKaito8 = ""
            _NonyuYotei8 = ""
            _Syochi = ""
            _Riyu = ""
            _Taio = ""
            _Busho = ""
            _SekkeiTantosya = ""
            _Tel = ""
            _ZanteiHinNonyubi = ""
            _SeikiOrKoukan = ""
            _StsrNew = ""
            _StsrJisseki = ""
            _ShutuzuYoteiDate = ""
            _ShutuzuJissekiDate = ""
            _Kata = ""
            _Koho = ""
            _MakerBuhinHi = ""
            _MakerKatahi = ""
            _KoujiKbn = ""
            _YosanKbn = ""
            _Teban = ""
        End Sub


#Region "各列タグの設定"
        '' 履歴
        Private COLUMN_RIREKI As Integer
        '' キャンセル実績
        Private COLUMN_CANCEL_JISSEKI As Integer
        '' ブロックNo
        Private COLUMN_BLOCK_NO As Integer
        '' 行ID
        Private COLUMN_GYOU_ID As Integer
        '' 専用マーク
        Private COLUMN_SENYOU_MARK As Integer
        '' レベル
        Private COLUMN_LEVEL As Integer
        '' ユニット区分
        Private COLUMN_UNIT_KBN As Integer
        '' 集計コード
        Private COLUMN_SHUKEI_CODE As Integer
        '' 手配記号
        Private COLUMN_TEHAI_KIGOU As Integer
        '' 購担
        Private COLUMN_KOUTAN As Integer
        '' 取引先コード
        Private COLUMN_TORIHIKISAKI_CODE As Integer
        '' 取引先名称
        Private COLUMN_TORIHIKISAKI_NAME As Integer
        '' 部品番号
        Private COLUMN_BUHIN_NO As Integer
        '' 試作区分
        Private COLUMN_SHISAKU_KBN As Integer
        '' 改訂No
        Private COLUMN_KAITEI_NO As Integer
        '' 枝番
        Private COLUMN_EDA_BAN As Integer
        '' 部品名称
        Private COLUMN_BUHIN_NAME As Integer
        '' 納入指示日
        Private COLUMN_NOUNYUSHIJIBI As Integer
        '' 納入指示数
        Private COLUMN_NOUNYUSHIJISU As Integer
        '' 納入場所
        Private COLUMN_NOUNYUBASYO As Integer
        '' 供給セクション
        Private COLUMN_KYOUKYU_SECTION As Integer
        '' 再使用不可
        Private COLUMN_SAISHIYOUFUKA As Integer
        ''↓↓2014/07/25 Ⅰ.2.管理項目追加_ay) (TES)張 ADD BEGIN
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
        '納入見通し'
        Private COLUMN_TSUKURIKATA_NOUNYU As Integer
        '部品製作規模・概要'
        Private COLUMN_TSUKURIKATA_KIBO As Integer
        ''↑↑2014/07/25 Ⅰ.2.管理項目追加_ay) (TES)張 ADD END
        '材質'
        '' 材質規格１
        Private COLUMN_ZAISHITSU_KIKAKU_1 As Integer
        '' 材質規格２
        Private COLUMN_ZAISHITSU_KIKAKU_2 As Integer
        '' 材質規格３
        Private COLUMN_ZAISHITSU_KIKAKU_3 As Integer
        '' 材質メッキ
        Private COLUMN_ZAISHITSU_MEKKI As Integer
        '板厚'
        '' 板厚数量
        Private COLUMN_BANKO_SURYO As Integer
        '' 板厚数量u
        Private COLUMN_BANKO_SURYO_U As Integer
        '' 試作部品費（円）
        Private COLUMN_SHISAKU_BUHIN_HI As Integer
        '' 試作型費(千円)
        Private COLUMN_SHISAKU_KATA_HI As Integer
        '' 備考
        Private COLUMN_BIKOU As Integer
        '' 号車開始列
        Private COLUMN_START_GOUSYA As Integer
        '' 号車終了列
        Private COLUMN_END_GOUSYA As Integer

        '↓↓↓2014/12/30 メタル項目を追加 TES)張 ADD BEGIN
        Private COLUMN_MATERIAL_INFO_LENGTH As Integer
        Private COLUMN_MATERIAL_INFO_WIDTH As Integer
        Private COLUMN_DATA_ITEM_KAITEI_NO As Integer
        Private COLUMN_DATA_ITEM_AREA_NAME As Integer
        Private COLUMN_DATA_ITEM_SET_NAME As Integer
        Private COLUMN_DATA_ITEM_KAITEI_INFO As Integer
        '↑↑↑2014/12/30 メタル項目を追加 TES)張 ADD END

        Private COLUMN_ZAIRYO_SUNPO_X As Integer
        Private COLUMN_ZAIRYO_SUNPO_Y As Integer
        Private COLUMN_ZAIRYO_SUNPO_Z As Integer
        Private COLUMN_ZAIRYO_SUNPO_XY As Integer
        Private COLUMN_ZAIRYO_SUNPO_XZ As Integer
        Private COLUMN_ZAIRYO_SUNPO_YZ As Integer

#End Region


#Region "各行のタグ設定"
        '' 員数
        Private INSU_ROW As Integer = 6
        '' タイトル行
        Private TITLE_ROW As Integer = 7
        '' 書き込み開始行
        Private START_ROW As Integer = 8
#End Region

#Region "AS400用"

        ''' <summary>発行No</summary>
        Private _HakkoNo As String

        ''' <summary>発注年月日</summary>
        Private _HacyuDate As String

        ''' <summary>同期</summary>
        Private _Doki As String

        ''' <summary>分納</summary>
        Private _Bunno As String

        ''' <summary>ネック</summary>
        Private _Neck As String

        ''' <summary>暫定・欠品</summary>
        Private _Zank As String

        ''' <summary>その他</summary>
        Private _Sonota As String

        ''' <summary>備考</summary>
        Private _Biko As String

        ''' <summary>納期回答１</summary>
        Private _NokiKaito1 As String

        ''' <summary>納入予定数２</summary>
        Private _NonyuYotei1 As String

        ''' <summary>納入区分</summary>
        Private _NonyuKbn As String

        ''' <summary>検収年月日</summary>
        Private _KensyuDate As String

        ''' <summary>納入累計数</summary>
        Private _NonyuTotal As String

        ''' <summary>引取り検収年月日</summary>
        Private _HikitoriDate As String

        ''' <summary>引取り累計数</summary>
        Private _HikitoriTotal As String

        ''' <summary>納入実績マーク</summary>
        Private _NonyuJissekiMark As String

        ''' <summary>取消年月日</summary>
        Private _TorikeshiDate As String

        ''' <summary>取消数</summary>
        Private _TorikeshiTotal As String

        ''' <summary>納期回答２</summary>
        Private _NokiKaito2 As String

        ''' <summary>納入予定数２</summary>
        Private _NonyuYotei2 As String

        ''' <summary>納期回答３</summary>
        Private _NokiKaito3 As String

        ''' <summary>納入予定数３</summary>
        Private _NonyuYotei3 As String

        ''' <summary>納期回答４</summary>
        Private _NokiKaito4 As String

        ''' <summary>納入予定数４</summary>
        Private _NonyuYotei4 As String

        ''' <summary>納期回答５</summary>
        Private _NokiKaito5 As String

        ''' <summary>納入予定数５</summary>
        Private _NonyuYotei5 As String

        ''' <summary>納期回答６</summary>
        Private _NokiKaito6 As String

        ''' <summary>納入予定数６</summary>
        Private _NonyuYotei6 As String

        ''' <summary>納期回答７</summary>
        Private _NokiKaito7 As String

        ''' <summary>納入予定数７</summary>
        Private _NonyuYotei7 As String

        ''' <summary>納期回答８</summary>
        Private _NokiKaito8 As String

        ''' <summary>納入予定数８</summary>
        Private _NonyuYotei8 As String

        ''' <summary>処置</summary>
        Private _Syochi As String

        ''' <summary>理由</summary>
        Private _Riyu As String

        ''' <summary>対応</summary>
        Private _Taio As String

        ''' <summary>部署</summary>
        Private _Busho As String

        ''' <summary>設計担当者</summary>
        Private _SekkeiTantosya As String

        ''' <summary>TEL</summary>
        Private _Tel As String

        ''' <summary>暫定品納入日</summary>
        Private _ZanteiHinNonyubi As String

        ''' <summary>正規扱いOr後交換有り</summary>
        Private _SeikiOrKoukan As String

        ''' <summary>設通No(最新)</summary>
        Private _StsrNew As String

        ''' <summary>設通No(実績)</summary>
        Private _StsrJisseki As String

        ''' <summary>出図予定日(最新)</summary>
        Private _ShutuzuYoteiDate As String

        ''' <summary>出図実績日(最新)</summary>
        Private _ShutuzuJissekiDate As String

        ''' <summary>型</summary>
        Private _Kata As String

        ''' <summary>工法</summary>
        Private _Koho As String

        ''' <summary>メーカー見積り部品費(円)</summary>
        Private _MakerBuhinHi As String

        ''' <summary>メーカー見積り型費(円)</summary>
        Private _MakerKatahi As String

        ''' <summary>工事区分</summary>
        Private _KoujiKbn As String

        ''' <summary>予算区分</summary>
        Private _YosanKbn As String

        ''' <summary>手番</summary>
        Private _Teban As String


#End Region

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class
End Namespace