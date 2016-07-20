Imports ShisakuCommon
Imports EBom.Excel
Imports EBom.Data
Imports EBom.Common
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.TehaichoMenu.Dao
Imports EventSakusei.TehaichoMenu.Impl
Imports EventSakusei.TehaichoMenu.Vo

Namespace TehaichoMenu.Excel

    '改訂抽出のエクセル'
    Public Class ExportKaiteChushutuExcel

        Private InsuCount As Integer
        'Private maxColumnNum As Integer
        Private BlockAlertKind As String
        Private KounyuShijiFlg As String

        ''' <summary>
        ''' 初期設定
        ''' </summary>
        ''' <param name="eventCode"></param>
        ''' <param name="listCode"></param>
        ''' <param name="kaiteiNo"></param>
        ''' <param name="eventName"></param>
        ''' <param name="sw">抽出ボタンまたは履歴参照を切り分ける（0:抽出 1:履歴参照）</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal eventCode As String, ByVal listCode As String, ByVal kaiteiNo As String, ByVal eventName As String, ByVal sw As Integer)
            Dim systemDrive As String = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            Dim fileName As String

            Dim impl As TehaichoMenuDao = New TehaichoMenuDaoImpl
            'イベント情報'
            Dim aEventVo As New TShisakuEventVo
            aEventVo = impl.FindByUnitKbn(eventCode)
            Dim daoEvent As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim voEvent As TShisakuEventVo = daoEvent.FindByPk(eventCode)
            Me.BlockAlertKind = voEvent.BlockAlertKind
            Me.KounyuShijiFlg = voEvent.KounyuShijiFlg

            Using sfd As New SaveFileDialog()
                sfd.FileName = ShisakuCommon.ShisakuGlobal.ExcelOutPut
                sfd.InitialDirectory = systemDrive
                '2012/01/25
                '2012/01/21
                '--------------------------------------------------
                'EXCEL出力先フォルダチェック
                ShisakuCommon.ShisakuComFunc.ExcelOutPutDirCheck()
                '--------------------------------------------------
                '[Excel出力系 G H]
                fileName = sfd.InitialDirectory + "\" + sfd.FileName
                If sw = 0 Then
                    fileName = aEventVo.ShisakuKaihatsuFugo + eventName + " 改訂抽出 " + Now.ToString("MMdd") + Now.ToString("HHmm") + ".xls"
                Else
                    fileName = aEventVo.ShisakuKaihatsuFugo + eventName + " 改訂履歴参照 " + kaiteiNo + ".xls"
                End If
                fileName = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + StringUtil.ReplaceInvalidCharForFileName(fileName)	'2012/02/08 Excel出力ディレクトリ指定対応
            End Using

            'リストコード'
            Dim aList As New TShisakuListcodeVo
            '改訂抽出リスト'
            Dim aBuhinKaiteiListVo As New List(Of TShisakuBuhinEditKaiteiVoHelper)
            'Dim aBuhinKaiteiListVo As New List(Of TShisakuBuhinEditKaiteiVo)


            '改訂抽出号車リスト '
            Dim aGousyaKaiteiListVo As New List(Of TShisakuBuhinEditGousyaKaiteiVo)
            'ベース車情報'
            Dim aBaseListVo As New List(Of TShisakuEventBaseVo)


            '取得処理を追加する'
            aList = impl.FindByListCode(eventCode, listCode)
            aBuhinKaiteiListVo = impl.FindByKaiteiBuhinEdit(eventCode, listCode, kaiteiNo)

            aGousyaKaiteiListVo = impl.FindByKaiteiBuhinEditGousya(eventCode, listCode)

            'グループごとベース車情報を取得する'
            aBaseListVo = impl.FindByBase(eventCode, listCode)


            '表示順をExcel用に'
            Dim i As Integer = 0
            For Each Vo As TShisakuEventBaseVo In aBaseListVo
                If Not StringUtil.Equals(Vo.ShisakuGousya, "DUMMY") Then
                    For Each KVo As TShisakuBuhinEditKaiteiVoHelper In aBuhinKaiteiListVo
                        If KVo.ShisakuGousyaHyoujiJun = Vo.HyojijunNo Then
                            If StringUtil.IsEmpty(KVo.MFlag) Then
                                If Not StringUtil.Equals(KVo.ShisakuGousya, "DUMMY") Then
                                    KVo.ShisakuGousyaHyoujiJun = i
                                    KVo.MFlag = "１"
                                End If
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
                    Vo.HyojijunNo = i
                End If
            Next

            For Each KVo As TShisakuBuhinEditKaiteiVoHelper In aBuhinKaiteiListVo
                If StringUtil.Equals(KVo.ShisakuGousya, "DUMMY") Then
                    KVo.ShisakuGousyaHyoujiJun = i
                End If
            Next



            If Not ShisakuComFunc.IsFileOpen(ShisakuCommon.ShisakuGlobal.ExcelOutPut) Then

                Using xls As New ShisakuExcel(fileName)
                    xls.OpenBook(fileName)
                    xls.ClearWorkBook()
                    xls.SetFont("ＭＳ Ｐゴシック", 11)
                    setKaiteiChushutsuSheet(xls, aList, aBuhinKaiteiListVo, aGousyaKaiteiListVo, aEventVo, aBaseListVo)
                    '2012/02/02'
                    'A4横で印刷できるように変更'
                    xls.PrintPaper(fileName, 1, "A4")
                    xls.PrintOrientation(fileName, 1, 1, False)
                    SetAutoOrikomiSheet(xls, aList, aBuhinKaiteiListVo, aGousyaKaiteiListVo, aEventVo, aBaseListVo)
                    '2012/02/02'
                    'A4横で印刷できるように変更'
                    xls.PrintPaper(fileName, 2, "A4")
                    xls.PrintOrientation(fileName, 2, 1, False)

                    '2014/09/16 追加（新調達シート）
                    setShinchotasuSheet(xls, aList, aBuhinKaiteiListVo, aGousyaKaiteiListVo, aEventVo, aBaseListVo)
                    xls.PrintPaper(fileName, 3, "A4")
                    xls.PrintOrientation(fileName, 3, 1, False)

                    xls.SetActiveSheet(1)
                    xls.Save()
                End Using
                Process.Start(fileName)
            End If

        End Sub

        ''' <summary>
        ''' Excel出力　シート
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aList">リストコード情報</param>
        ''' <param name="aBuhinKaiteiList">改訂抽出リスト</param>
        ''' <param name="aGousyaKaiteiListVo">改訂抽出号車リスト</param>
        ''' <param name="aEventVo">イベント情報</param>
        ''' <remarks></remarks>
        Private Sub setKaiteiChushutsuSheet(ByVal xls As ShisakuExcel, _
                                         ByVal aList As TShisakuListcodeVo, _
                                         ByVal aBuhinKaiteiList As List(Of TShisakuBuhinEditKaiteiVoHelper), _
                                         ByVal aGousyaKaiteiListVo As List(Of TShisakuBuhinEditGousyaKaiteiVo), _
                                         ByVal aEventVo As TShisakuEventVo, _
                                         ByVal aBaseListVo As List(Of TShisakuEventBaseVo))

            'ByVal aBuhinKaiteiList As List(Of TShisakuBuhinEditKaiteiVo), _

            xls.SetActiveSheet(1)
            xls.SetSheetName("改訂抽出")

            SetColumnNo()

            setSheetHeard(xls, aList, aEventVo)

            setSheetColumnWidth(xls)

            setSheetBody(xls, aBuhinKaiteiList, aGousyaKaiteiListVo, aList, aEventVo, aBaseListVo)


        End Sub

        ''' <summary>
        ''' Excel出力　シートのHeaderの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aList">リストコード情報</param>
        ''' <remarks></remarks>
        Public Sub setSheetHeard(ByVal xls As ShisakuExcel, _
                                  ByVal aList As TShisakuListcodeVo, _
                                  ByVal aEventVo As TShisakuEventVo)

            'TODO 仕様変更に耐えうるようにすべき'
            'ユニット区分用にイベント情報も追加()

            'イベントコード'
            xls.MergeCells(1, 1, 4, 1, True)
            xls.SetValue(1, 1, "イベントコード : ")
            xls.MergeCells(5, 1, 9, 1, True)
            xls.SetValue(5, 1, aList.ShisakuListCode)
            'イベント名称'
            xls.MergeCells(1, 2, 4, 2, True)
            xls.SetValue(1, 2, "イベント名称 : ")
            xls.MergeCells(5, 2, 9, 2, True)
            xls.SetValue(5, 2, aEventVo.ShisakuKaihatsuFugo + " " + aList.ShisakuEventName)
            '工事指令No'
            xls.MergeCells(1, 3, 4, 3, True)
            xls.SetValue(1, 3, "工事指令No : ")
            xls.MergeCells(5, 3, 9, 3, True)
            xls.SetValue(5, 3, aList.ShisakuKoujiShireiNo)
            '抽出日時'
            xls.MergeCells(1, 4, 4, 4, True)
            xls.SetValue(1, 4, "抽出日時")
            xls.MergeCells(5, 4, 9, 4, True)

            Dim chusyutubi As String
            Dim chusyutujikan As String

            chusyutubi = Mid(aList.SaishinChusyutubi.ToString, 1, 4) + "/" + Mid(aList.SaishinChusyutubi.ToString, 5, 2) + "/" + Mid(aList.SaishinChusyutubi.ToString, 7, 2)

            '-----------------------------------------------------------
            '２次改修
            '   日付が取得できなければシステム日付を設定する。
            If aList.SaishinChusyutubi.ToString = "0" Then
                chusyutubi = DateTime.Now.ToString("yyyy/MM/dd")
            End If
            '-----------------------------------------------------------

            If aList.SaishinChusyutujikan.ToString.Length < 6 Then
                chusyutujikan = Mid(aList.SaishinChusyutujikan.ToString, 1, 1) + ":" + Mid(aList.SaishinChusyutujikan.ToString, 2, 2) + ":" + Mid(aList.SaishinChusyutujikan.ToString, 4, 2)
            Else
                chusyutujikan = Mid(aList.SaishinChusyutujikan.ToString, 1, 2) + ":" + Mid(aList.SaishinChusyutujikan.ToString, 3, 2) + ":" + Mid(aList.SaishinChusyutujikan.ToString, 5, 2)
            End If

            '-----------------------------------------------------------
            '２次改修
            '   時間が取得できなければシステム時間を設定する。
            If aList.SaishinChusyutujikan.ToString = "0" Then
                chusyutujikan = DateTime.Now.ToString("HH:mm:ss")
            End If
            '-----------------------------------------------------------

            xls.SetValue(5, 4, chusyutubi + " " + chusyutujikan)

        End Sub

        ''' <summary>
        ''' Excel出力　シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aBuhinKaiteiList">改訂抽出リスト</param>
        ''' <param name="aGousyaKaiteiList">改訂抽出号車リスト</param>
        ''' <remarks></remarks>
        Public Sub setSheetBody(ByVal xls As ShisakuExcel, ByVal aBuhinKaiteiList As List(Of TShisakuBuhinEditKaiteiVoHelper), _
                                            ByVal aGousyaKaiteiList As List(Of TShisakuBuhinEditGousyaKaiteiVo), _
                                            ByVal aList As TShisakuListcodeVo, _
                                            ByVal aEventVo As TShisakuEventVo, _
                                            ByVal aBaseListVo As List(Of TShisakuEventBaseVo))

            Dim KaiteiImpl As KaiteiChusyutuDao = New KaiteiChusyutuDaoImpl
            Dim impl As TehaichoMenuDao = New TehaichoMenuDaoImpl

            'タイトル部分の作成'
            Dim maxColumnNum As Integer = setKaiteiChushutsuTitleRow(xls, aGousyaKaiteiList, aBaseListVo)

            Dim rowIndex As Integer = 0
            Dim mergeFlag As Boolean = False
            Dim backBlockNo As String = ""

            Dim maxRowNumber As Integer = 0
            For index As Integer = 0 To aBuhinKaiteiList.Count - 1
                If Me.BlockAlertKind = "2" AndAlso Me.KounyuShijiFlg = "0" AndAlso backBlockNo <> aBuhinKaiteiList(index).ShisakuBlockNo Then
                    maxRowNumber = setSheetBody_Ikansha_cnt(impl, aBaseListVo, aBuhinKaiteiList(index).ShisakuEventCode, aBuhinKaiteiList(index).ShisakuBlockNo, maxRowNumber, aBuhinKaiteiList(index).ShisakuListCode)
                End If
                If Not index = 0 Then
                    '移管車改修時情報出力

                    If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNoHyoujiJun, aBuhinKaiteiList(index - 1).BuhinNoHyoujiJun) Then
                        If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNo, aBuhinKaiteiList(index - 1).BuhinNo) Then
                            If StringUtil.Equals(aBuhinKaiteiList(index).Flag, aBuhinKaiteiList(index - 1).Flag) Then
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
            Dim dataMatrix(maxRowNumber, maxColumnNum) As String

            '-------------------------------------------------------------------------------------------------------------------------
            '改訂抽出出力
            For index As Integer = 0 To aBuhinKaiteiList.Count - 1
                '-------------------------------------------------------------------------------------------------------------------------
                '移管車改修時情報出力
                If Me.BlockAlertKind = "2" AndAlso Me.KounyuShijiFlg = "0" AndAlso backBlockNo <> aBuhinKaiteiList(index).ShisakuBlockNo Then

                    'rowIndex = setSheetBody_Ikansha(xls, aBaseListVo, aBuhinKaiteiList(index).ShisakuEventCode, aBuhinKaiteiList(index).ShisakuBlockNo, rowIndex, aBuhinKaiteiList(index).ShisakuListCode)
                    rowIndex = setSheetBody_Ikansha(impl, dataMatrix, aBaseListVo, aBuhinKaiteiList(index).ShisakuEventCode, aBuhinKaiteiList(index).ShisakuBlockNo, rowIndex, aBuhinKaiteiList(index).ShisakuListCode)
                End If
                backBlockNo = aBuhinKaiteiList(index).ShisakuBlockNo


                If Not index = 0 Then
                    If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNoHyoujiJun, aBuhinKaiteiList(index - 1).BuhinNoHyoujiJun) Then
                        If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNo, aBuhinKaiteiList(index - 1).BuhinNo) Then
                            If StringUtil.Equals(aBuhinKaiteiList(index).Flag, aBuhinKaiteiList(index - 1).Flag) Then
                                rowIndex = rowIndex - 1
                                mergeFlag = True
                            End If
                        End If
                    End If
                End If

                If mergeFlag Then
                    '員数のみ'
                    Dim colIdx As Integer = COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1
                    If aBuhinKaiteiList(index).InsuSuryo < 0 Then
                        'xls.SetValue(COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, "**")
                        dataMatrix(rowIndex, colIdx) = "**"
                    ElseIf aBuhinKaiteiList(index).InsuSuryo = 0 Then
                    Else
                        'Dim insu As String = xls.GetValue(COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex)
                        Dim insu As String = dataMatrix(rowIndex, colIdx)

                        If StringUtil.Equals(insu, "**") Then
                            'xls.SetValue(COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, "**")
                            dataMatrix(rowIndex, colIdx) = "**"
                        Else
                            Dim totalInsu As Integer = 0

                            If StringUtil.IsEmpty(insu) Then
                                'xls.SetValue(COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, aBuhinKaiteiList(index).InsuSuryo)
                                dataMatrix(rowIndex, colIdx) = aBuhinKaiteiList(index).InsuSuryo
                            Else
                                totalInsu = Integer.Parse(insu)
                                totalInsu += aBuhinKaiteiList(index).InsuSuryo
                                'xls.SetValue(COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, totalInsu)
                                dataMatrix(rowIndex, colIdx) = totalInsu
                            End If
                        End If
                    End If
                    mergeFlag = False
                Else
                    'フラグ'
                    'xls.SetValue(COLUMN_FLAG, xlsRow, GetFlag(aBuhinKaiteiList(index)))
                    dataMatrix(rowIndex, COLUMN_FLAG - 1) = GetFlag(aBuhinKaiteiList(index))

                    'ブロックNo'
                    'xls.SetValue(COLUMN_BLOCK_NO, xlsRow, aBuhinKaiteiList(index).ShisakuBlockNo)
                    dataMatrix(rowIndex, COLUMN_BLOCK_NO - 1) = aBuhinKaiteiList(index).ShisakuBlockNo
                    '改訂No'
                    '前回ブロックNo改訂Noが無いときブロックNo改訂Noを設定する'
                    If StringUtil.IsEmpty(aBuhinKaiteiList(index).ZenkaiShisakuBlockNoKaiteiNo) Then
                        'xls.SetValue(COLUMN_KAITEI_NO, xlsRow, aBuhinKaiteiList(index).ShisakuBlockNoKaiteiNo)
                        dataMatrix(rowIndex, COLUMN_KAITEI_NO - 1) = aBuhinKaiteiList(index).ShisakuBlockNoKaiteiNo
                    Else
                        'xls.SetValue(COLUMN_KAITEI_NO, xlsRow, aBuhinKaiteiList(index).ZenkaiShisakuBlockNoKaiteiNo)
                        dataMatrix(rowIndex, COLUMN_KAITEI_NO - 1) = aBuhinKaiteiList(index).ZenkaiShisakuBlockNoKaiteiNo
                    End If
                    '専用マーク'
                    If SenyouCheck(aBuhinKaiteiList(index).BuhinNo, aList.ShisakuSeihinKbn) Then
                        'xls.SetValue(COLUMN_SENYOU_MARK, xlsRow, "")
                        dataMatrix(rowIndex, COLUMN_SENYOU_MARK - 1) = ""
                    Else
                        'xls.SetValue(COLUMN_SENYOU_MARK, xlsRow, "*")
                        dataMatrix(rowIndex, COLUMN_SENYOU_MARK - 1) = "*"
                    End If

                    'レベル'
                    'xls.SetValue(COLUMN_LEVEL, xlsRow, aBuhinKaiteiList(index).Level)
                    dataMatrix(rowIndex, COLUMN_LEVEL - 1) = aBuhinKaiteiList(index).Level
                    '部品番号'
                    'xls.SetValue(COLUMN_BUHIN_NO, xlsRow, aBuhinKaiteiList(index).BuhinNo)
                    dataMatrix(rowIndex, COLUMN_BUHIN_NO - 1) = aBuhinKaiteiList(index).BuhinNo
                    '試作区分'
                    'xls.SetValue(COLUMN_SHISAKU_KBN, xlsRow, aBuhinKaiteiList(index).BuhinNoKbn)
                    dataMatrix(rowIndex, COLUMN_SHISAKU_KBN - 1) = aBuhinKaiteiList(index).BuhinNoKbn
                    '改訂'
                    'xls.SetValue(COLUMN_KAITEI, xlsRow, aBuhinKaiteiList(index).BuhinNoKaiteiNo)
                    dataMatrix(rowIndex, COLUMN_KAITEI - 1) = aBuhinKaiteiList(index).BuhinNoKaiteiNo
                    '枝番'
                    'xls.SetValue(COLUMN_EDA_BAN, xlsRow, aBuhinKaiteiList(index).EdaBan)
                    dataMatrix(rowIndex, COLUMN_EDA_BAN - 1) = aBuhinKaiteiList(index).EdaBan
                    '部品名称'
                    'xls.SetValue(COLUMN_BUHIN_NAME, xlsRow, aBuhinKaiteiList(index).BuhinName)
                    dataMatrix(rowIndex, COLUMN_BUHIN_NAME - 1) = aBuhinKaiteiList(index).BuhinName
                    '集計コード'
                    If StringUtil.IsEmpty(aBuhinKaiteiList(index).ShukeiCode) Then
                        'xls.SetValue(COLUMN_SHUKEI_CODE, xlsRow, aBuhinKaiteiList(index).SiaShukeiCode)
                        dataMatrix(rowIndex, COLUMN_SHUKEI_CODE - 1) = aBuhinKaiteiList(index).SiaShukeiCode
                    Else
                        'xls.SetValue(COLUMN_SHUKEI_CODE, xlsRow, aBuhinKaiteiList(index).ShukeiCode)
                        dataMatrix(rowIndex, COLUMN_SHUKEI_CODE - 1) = aBuhinKaiteiList(index).ShukeiCode
                    End If

                    '購担'
                    Dim BuhinEdittmp As TShisakuBuhinEditTmpVo = getBuhinEdittmp(aBuhinKaiteiList(index).BuhinNo)
                    'xls.SetValue(COLUMN_KOUTAN, xlsRow, BuhinEdittmp.Koutan)
                    dataMatrix(rowIndex, COLUMN_KOUTAN - 1) = BuhinEdittmp.Koutan


                    '取引先コードがNULLなら取引先コードを取得する'
                    If StringUtil.IsEmpty(aBuhinKaiteiList(index).MakerCode) Then
                        '取引先コード'
                        'xls.SetValue(COLUMN_TORIHIKISAKI_CODE, xlsRow, BuhinEdittmp.MakerCode)
                        dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_CODE - 1) = BuhinEdittmp.MakerCode
                        '取引先名称'
                        'xls.SetValue(COLUMN_TORIHIKISAKI_NAME, xlsRow, BuhinEdittmp.MakerName)
                        dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_NAME - 1) = BuhinEdittmp.MakerName
                    Else
                        '取引先コード'
                        'xls.SetValue(COLUMN_TORIHIKISAKI_CODE, xlsRow, aBuhinKaiteiList(index).MakerCode)
                        dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_CODE - 1) = aBuhinKaiteiList(index).MakerCode
                        '取引先名称'
                        'xls.SetValue(COLUMN_TORIHIKISAKI_NAME, xlsRow, aBuhinKaiteiList(index).MakerName)
                        dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_NAME - 1) = aBuhinKaiteiList(index).MakerName
                    End If

                    '再使用不可'
                    'xls.SetValue(COLUMN_SAISHIYOUFUKA, xlsRow, aBuhinKaiteiList(index).Saishiyoufuka)
                    dataMatrix(rowIndex, COLUMN_SAISHIYOUFUKA - 1) = aBuhinKaiteiList(index).Saishiyoufuka
                    '供給セクション'
                    'xls.SetValue(COLUMN_KYOUKU_SECTION, xlsRow, aBuhinKaiteiList(index).KyoukuSection)
                    dataMatrix(rowIndex, COLUMN_KYOUKU_SECTION - 1) = aBuhinKaiteiList(index).KyoukuSection

                    '出図予定日'
                    Dim shukkayoteibi As String

                    If aBuhinKaiteiList(index).ShutuzuYoteiDate = 0 Then
                        shukkayoteibi = ""
                    Else
                        If aBuhinKaiteiList(index).ShutuzuYoteiDate = 99999999 Then
                            shukkayoteibi = ""
                        Else
                            shukkayoteibi = Mid(aBuhinKaiteiList(index).ShutuzuYoteiDate.ToString, 1, 4) + "/" + Mid(aBuhinKaiteiList(index).ShutuzuYoteiDate.ToString, 5, 2) + "/" + Mid(aBuhinKaiteiList(index).ShutuzuYoteiDate.ToString, 7, 2)
                        End If
                    End If
                    'xls.SetValue(COLUMN_SHUKKAYOTEIBI, xlsRow, shukkayoteibi)
                    dataMatrix(rowIndex, COLUMN_SHUKKAYOTEIBI - 1) = shukkayoteibi
                    'xls.SetValue(COLUMN_TSUKURIKATA_SEISAKU, xlsRow, aBuhinKaiteiList(index).TsukurikataSeisaku)
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_SEISAKU - 1) = aBuhinKaiteiList(index).TsukurikataSeisaku
                    'xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_1, xlsRow, aBuhinKaiteiList(index).TsukurikataKatashiyou1)
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU_1 - 1) = aBuhinKaiteiList(index).TsukurikataKatashiyou1
                    'xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_2, xlsRow, aBuhinKaiteiList(index).TsukurikataKatashiyou2)
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU_2 - 1) = aBuhinKaiteiList(index).TsukurikataKatashiyou2
                    'xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_3, xlsRow, aBuhinKaiteiList(index).TsukurikataKatashiyou3)
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU_3 - 1) = aBuhinKaiteiList(index).TsukurikataKatashiyou3
                    'xls.SetValue(COLUMN_TSUKURIKATA_TIGU, xlsRow, aBuhinKaiteiList(index).TsukurikataTigu)
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_TIGU - 1) = aBuhinKaiteiList(index).TsukurikataTigu

                    Dim nounyu As String
                    If aBuhinKaiteiList(index).TsukurikataNounyu = 0 Then
                        nounyu = ""
                    Else
                        If StringUtil.IsEmpty(aBuhinKaiteiList(index).TsukurikataNounyu) Then
                            nounyu = ""
                        Else
                            nounyu = Mid(aBuhinKaiteiList(index).TsukurikataNounyu.ToString, 1, 4) + "/" + Mid(aBuhinKaiteiList(index).TsukurikataNounyu.ToString, 5, 2) + "/" + Mid(aBuhinKaiteiList(index).TsukurikataNounyu.ToString, 7, 2)
                        End If
                    End If
                    'xls.SetValue(COLUMN_TSUKURIKATA_NOUNYU, xlsRow, nounyu)
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_NOUNYU - 1) = nounyu
                    'xls.SetValue(COLUMN_TSUKURIKATA_KIBO, xlsRow, aBuhinKaiteiList(index).TsukurikataKibo)
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KIBO - 1) = aBuhinKaiteiList(index).TsukurikataKibo

                    '材質規格１'
                    'xls.SetValue(COLUMN_ZAISHITU_KIKAKU_1, xlsRow, aBuhinKaiteiList(index).ZaishituKikaku1)
                    dataMatrix(rowIndex, COLUMN_ZAISHITU_KIKAKU_1 - 1) = aBuhinKaiteiList(index).ZaishituKikaku1
                    '材質規格２'
                    'xls.SetValue(COLUMN_ZAISHITU_KIKAKU_2, xlsRow, aBuhinKaiteiList(index).ZaishituKikaku2)
                    dataMatrix(rowIndex, COLUMN_ZAISHITU_KIKAKU_2 - 1) = aBuhinKaiteiList(index).ZaishituKikaku2
                    '材質規格３'
                    'xls.SetValue(COLUMN_ZAISHITU_KIKAKU_3, xlsRow, aBuhinKaiteiList(index).ZaishituKikaku3)
                    dataMatrix(rowIndex, COLUMN_ZAISHITU_KIKAKU_3 - 1) = aBuhinKaiteiList(index).ZaishituKikaku3
                    '材質メッキ'
                    'xls.SetValue(COLUMN_ZAISHITU_MEKKI, xlsRow, aBuhinKaiteiList(index).ZaishituMekki)
                    dataMatrix(rowIndex, COLUMN_ZAISHITU_MEKKI - 1) = aBuhinKaiteiList(index).ZaishituMekki
                    '板厚'
                    'xls.SetValue(COLUMN_SHISAKU_BANKO_SURYO, xlsRow, aBuhinKaiteiList(index).ShisakuBankoSuryo)
                    dataMatrix(rowIndex, COLUMN_SHISAKU_BANKO_SURYO - 1) = aBuhinKaiteiList(index).ShisakuBankoSuryo
                    '板厚U'
                    'xls.SetValue(COLUMN_SHISAKU_BANKO_SURYO_U, xlsRow, aBuhinKaiteiList(index).ShisakuBankoSuryoU)
                    dataMatrix(rowIndex, COLUMN_SHISAKU_BANKO_SURYO_U - 1) = aBuhinKaiteiList(index).ShisakuBankoSuryoU


                    ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
                    '材料情報・製品長'
                    'xls.SetValue(COLUMN_MATERIAL_INFO_LENGTH, xlsRow, aBuhinKaiteiList(index).MaterialInfoLength)
                    dataMatrix(rowIndex, COLUMN_MATERIAL_INFO_LENGTH - 1) = NVL(aBuhinKaiteiList(index).MaterialInfoLength)
                    '材料情報・製品幅'
                    'xls.SetValue(COLUMN_MATERIAL_INFO_WIDTH, xlsRow, aBuhinKaiteiList(index).MaterialInfoWidth)
                    dataMatrix(rowIndex, COLUMN_MATERIAL_INFO_WIDTH - 1) = NVL(aBuhinKaiteiList(index).MaterialInfoWidth)
                    'データ項目・改訂№'
                    'xls.SetValue(COLUMN_DATA_ITEM_KAITEI_NO, xlsRow, aBuhinKaiteiList(index).DataItemKaiteiNo)
                    dataMatrix(rowIndex, COLUMN_DATA_ITEM_KAITEI_NO - 1) = aBuhinKaiteiList(index).DataItemKaiteiNo
                    'データ項目・エリア名'
                    'xls.SetValue(COLUMN_DATA_ITEM_AREA_NAME, xlsRow, aBuhinKaiteiList(index).DataItemAreaName)
                    dataMatrix(rowIndex, COLUMN_DATA_ITEM_AREA_NAME - 1) = aBuhinKaiteiList(index).DataItemAreaName
                    'データ項目・セット名'
                    'xls.SetValue(COLUMN_DATA_ITEM_SET_NAME, xlsRow, aBuhinKaiteiList(index).DataItemSetName)
                    dataMatrix(rowIndex, COLUMN_DATA_ITEM_SET_NAME - 1) = aBuhinKaiteiList(index).DataItemSetName
                    'データ項目・改訂情報'
                    'xls.SetValue(COLUMN_DATA_ITEM_KAITEI_INFO, xlsRow, aBuhinKaiteiList(index).DataItemKaiteiInfo)
                    dataMatrix(rowIndex, COLUMN_DATA_ITEM_KAITEI_INFO - 1) = aBuhinKaiteiList(index).DataItemKaiteiInfo
                    ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


                    '試作部品費'
                    'xls.SetValue(COLUMN_SHISAKU_BUHIN_HI, xlsRow, aBuhinKaiteiList(index).ShisakuBuhinHi)
                    dataMatrix(rowIndex, COLUMN_SHISAKU_BUHIN_HI - 1) = NVL(aBuhinKaiteiList(index).ShisakuBuhinHi)
                    '試作型費'
                    'xls.SetValue(COLUMN_SHISAKU_KATA_HI, xlsRow, aBuhinKaiteiList(index).ShisakuKataHi)
                    dataMatrix(rowIndex, COLUMN_SHISAKU_KATA_HI - 1) = NVL(aBuhinKaiteiList(index).ShisakuKataHi)
                    'NOTE'
                    'xls.SetValue(COLUMN_NOTE, xlsRow, aBuhinKaiteiList(index).BuhinNote)
                    dataMatrix(rowIndex, COLUMN_NOTE - 1) = aBuhinKaiteiList(index).BuhinNote
                    '備考'
                    'xls.SetValue(COLUMN_BIKOU, xlsRow, aBuhinKaiteiList(index).Bikou)
                    dataMatrix(rowIndex, COLUMN_BIKOU - 1) = aBuhinKaiteiList(index).Bikou

                    If aBuhinKaiteiList(index).InsuSuryo < 0 Then
                        'xls.SetValue(COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun, xlsRow, "**")
                        dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                    ElseIf aBuhinKaiteiList(index).InsuSuryo = 0 Then
                    Else
                        'xls.SetValue(COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun, xlsRow, aBuhinKaiteiList(index).InsuSuryo)
                        dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = aBuhinKaiteiList(index).InsuSuryo
                    End If

                End If

                rowIndex = rowIndex + 1

            Next

            '合計員数数量を計算する'
            'For rowIndex2 As Integer = 0 To rowIndex - 1
            '    Dim totalInsuSuryo As Integer = 0
            '    Dim insu As String
            '    For columnIndex As Integer = 0 To aBaseListVo.Count
            '        Dim colIdx As Integer = COLUMN_INSU + columnIndex - 1

            '        'insu = xls.GetValue(COLUMN_INSU + columnIndex, xlsRow)
            '        insu = dataMatrix(rowIndex2, colIdx)

            '        If StringUtil.IsNotEmpty(insu) Then

            '            If StringUtil.Equals(insu, "**") Then
            '                'xls.SetValue(COLUMN_TOTAL_INSU, xlsRow, "**")
            '                If StringUtil.IsEmpty(dataMatrix(rowIndex2, COLUMN_TOTAL_INSU - 1)) Then
            '                    dataMatrix(rowIndex2, COLUMN_TOTAL_INSU - 1) = "**"
            '                End If
            '            Else
            '                If Integer.Parse(insu) < 0 Then
            '                    'xls.SetValue(COLUMN_TOTAL_INSU, xlsRow, "**")
            '                    If StringUtil.IsEmpty(dataMatrix(rowIndex2, COLUMN_TOTAL_INSU - 1)) Then
            '                        dataMatrix(rowIndex2, COLUMN_TOTAL_INSU - 1) = "**"
            '                    End If
            '                ElseIf Integer.Parse(insu) = 0 Then

            '                Else
            '                    totalInsuSuryo = totalInsuSuryo + Integer.Parse(insu)
            '                    'xls.SetValue(COLUMN_TOTAL_INSU, xlsRow, totalInsuSuryo)
            '                    dataMatrix(rowIndex2, COLUMN_TOTAL_INSU - 1) = totalInsuSuryo
            '                End If
            '            End If

            '        Else
            '            'xls.SetValue(COLUMN_TOTAL_INSU, START_ROW + rowIndex, "**")
            '            'dataMatrix(rowIndex2, COLUMN_TOTAL_INSU - 1) = "**"
            '        End If
            '    Next

            'Next


            For rowIndex2 As Integer = 0 To rowIndex - 1
                Dim totalInsuSuryo As Integer = 0
                Dim insuStr As String
                For columnIndex As Integer = 0 To aBaseListVo.Count
                    insuStr = dataMatrix(rowIndex2, COLUMN_INSU + columnIndex - 1)
                    If StringUtil.IsNotEmpty(insuStr) Then

                        If StringUtil.Equals(insuStr, "**") Then
                            totalInsuSuryo += 0
                        Else
                            totalInsuSuryo += 0
                            If Integer.Parse(insuStr) < 0 Then
                            ElseIf Integer.Parse(insuStr) = 0 Then

                            Else
                                totalInsuSuryo += Integer.Parse(insuStr)
                            End If
                        End If
                    Else
                        totalInsuSuryo += 0
                    End If
                Next
                If totalInsuSuryo > 0 Then
                    dataMatrix(rowIndex2, COLUMN_TOTAL_INSU - 1) = CStr(totalInsuSuryo)
                Else
                    dataMatrix(rowIndex2, COLUMN_TOTAL_INSU - 1) = "**"
                End If
            Next

            xls.CopyRange(0, START_ROW, UBound(dataMatrix, 2) - 1, START_ROW + UBound(dataMatrix), dataMatrix)


            '列の幅を自動調整する'
            xls.AutoFitCol(COLUMN_FLAG, xls.EndCol())
        End Sub

        Private Function NVL(ByVal val As Object, Optional ByVal rtn As Object = "") As Object
            If val Is Nothing Then Return rtn
            Return val
        End Function

        Public Function setSheetBody_Ikansha_cnt(ByVal impl As TehaichoMenuDao, _
                                  ByVal aBaseListVo As List(Of TShisakuEventBaseVo), _
                                  ByVal eventCode As String, ByVal blockNo As String, _
                                  ByVal rowIndex As Integer, _
                                  ByVal listCode As String) As Integer

            Dim aBuhinEdit As List(Of TShisakuBuhinEditVoHelperExcel) = Nothing
            aBuhinEdit = impl.FindByBuhinEditIkanshaKaishu(eventCode, blockNo, listCode)

            Dim hyoujiHosei As Integer = -1
            For Each vo As TShisakuBuhinEditVoHelperExcel In aBuhinEdit
                If hyoujiHosei = -1 Then
                    hyoujiHosei = vo.ShisakuGousyaHyoujiJun
                Else
                    If hyoujiHosei > vo.ShisakuGousyaHyoujiJun Then
                        hyoujiHosei = vo.ShisakuGousyaHyoujiJun
                    End If
                End If
            Next
            '-------------------------------------------------------------------------------------------------------------------------
            '移管車改修時情報出力
            If aBuhinEdit IsNot Nothing Then
                For index As Integer = 0 To aBuhinEdit.Count - 1
                    If Not index = 0 Then
                        If StringUtil.Equals(aBuhinEdit(index).BuhinNoHyoujiJun, aBuhinEdit(index - 1).BuhinNoHyoujiJun) Then
                            If StringUtil.Equals(aBuhinEdit(index).BuhinNo, aBuhinEdit(index - 1).BuhinNo) Then
                                If StringUtil.Equals(aBuhinEdit(index).Flag, aBuhinEdit(index - 1).Flag) Then
                                    rowIndex = rowIndex - 1
                                End If
                            End If
                        End If
                    End If
                    rowIndex = rowIndex + 1
                Next
            End If

            Return rowIndex
        End Function

        Public Function setSheetBody_Ikansha(ByVal impl As TehaichoMenuDao, _
                                          ByRef dataMatrix As String(,), _
                                          ByVal aBaseListVo As List(Of TShisakuEventBaseVo), _
                                          ByVal eventCode As String, ByVal blockNo As String, _
                                          ByVal inRowIndex As Integer, _
                                          ByVal listCode As String) As Integer


            Dim rowIndex As Integer = inRowIndex
            Dim aBuhinEdit As List(Of TShisakuBuhinEditVoHelperExcel) = Nothing
            aBuhinEdit = impl.FindByBuhinEditIkanshaKaishu(eventCode, blockNo, listCode)

            Dim hyoujiHosei As Integer = -1
            For Each vo As TShisakuBuhinEditVoHelperExcel In aBuhinEdit
                If hyoujiHosei = -1 Then
                    hyoujiHosei = vo.ShisakuGousyaHyoujiJun
                Else
                    If hyoujiHosei > vo.ShisakuGousyaHyoujiJun Then
                        hyoujiHosei = vo.ShisakuGousyaHyoujiJun
                    End If
                End If
            Next



            Dim mergeFlag As Boolean = False
            '-------------------------------------------------------------------------------------------------------------------------
            '移管車改修時情報出力
            If aBuhinEdit IsNot Nothing Then
                For index As Integer = 0 To aBuhinEdit.Count - 1
                    If Not index = 0 Then
                        If StringUtil.Equals(aBuhinEdit(index).BuhinNoHyoujiJun, aBuhinEdit(index - 1).BuhinNoHyoujiJun) Then
                            If StringUtil.Equals(aBuhinEdit(index).BuhinNo, aBuhinEdit(index - 1).BuhinNo) Then
                                If StringUtil.Equals(aBuhinEdit(index).Flag, aBuhinEdit(index - 1).Flag) Then
                                    rowIndex = rowIndex - 1
                                    mergeFlag = True
                                End If
                            End If
                        End If
                    End If

                    If mergeFlag Then
                        '員数のみ'
                        Dim colIdx As Integer = COLUMN_INSU + aBuhinEdit(index).ShisakuGousyaHyoujiJun - hyoujiHosei
                        If aBuhinEdit(index).InsuSuryo < 0 Then
                            'xls.SetValue(COLUMN_INSU + aBuhinEdit(index).ShisakuGousyaHyoujiJun - hyoujiHosei, START_ROW + rowIndex, "**")
                            dataMatrix(rowIndex, colIdx) = "**"
                        ElseIf aBuhinEdit(index).InsuSuryo = 0 Then
                        Else
                            'Dim insu As String = xls.GetValue(COLUMN_INSU + aBuhinEdit(index).ShisakuGousyaHyoujiJun - hyoujiHosei, START_ROW + rowIndex)
                            Dim insu As String = dataMatrix(rowIndex, COLUMN_INSU + aBuhinEdit(index).ShisakuGousyaHyoujiJun - hyoujiHosei)

                            If StringUtil.Equals(insu, "**") Then
                                'xls.SetValue(COLUMN_INSU + aBuhinEdit(index).ShisakuGousyaHyoujiJun - hyoujiHosei, START_ROW + rowIndex, "**")
                                dataMatrix(rowIndex, colIdx) = "**"
                            Else
                                Dim totalInsu As Integer = 0

                                If StringUtil.IsEmpty(insu) Then
                                    'xls.SetValue(COLUMN_INSU + aBuhinEdit(index).ShisakuGousyaHyoujiJun - hyoujiHosei, START_ROW + rowIndex, aBuhinEdit(index).InsuSuryo)
                                    dataMatrix(rowIndex, colIdx) = aBuhinEdit(index).InsuSuryo
                                Else
                                    If Integer.TryParse(insu, totalInsu) Then
                                        totalInsu = totalInsu + aBuhinEdit(index).InsuSuryo
                                        'xls.SetValue(COLUMN_INSU + aBuhinEdit(index).ShisakuGousyaHyoujiJun - hyoujiHosei, START_ROW + rowIndex, totalInsu)
                                        dataMatrix(rowIndex, colIdx) = totalInsu
                                    End If
                                End If
                            End If
                        End If
                        mergeFlag = False
                    Else
                        'フラグ'
                        'xls.SetValue(COLUMN_FLAG, START_ROW + rowIndex, "ベース")
                        dataMatrix(rowIndex, COLUMN_FLAG - 1) = "ベース"
                        'ブロックNo'
                        'xls.SetValue(COLUMN_BLOCK_NO, START_ROW + rowIndex, aBuhinEdit(index).ShisakuBlockNo)
                        dataMatrix(rowIndex, COLUMN_BLOCK_NO - 1) = aBuhinEdit(index).ShisakuBlockNo
                        'レベル'
                        'xls.SetValue(COLUMN_LEVEL, START_ROW + rowIndex, aBuhinEdit(index).Level)
                        dataMatrix(rowIndex, COLUMN_LEVEL - 1) = aBuhinEdit(index).Level
                        '部品番号'
                        'xls.SetValue(COLUMN_BUHIN_NO, START_ROW + rowIndex, aBuhinEdit(index).BuhinNo)
                        dataMatrix(rowIndex, COLUMN_BUHIN_NO - 1) = aBuhinEdit(index).BuhinNo
                        '試作区分'
                        'xls.SetValue(COLUMN_SHISAKU_KBN, START_ROW + rowIndex, aBuhinEdit(index).BuhinNoKbn)
                        dataMatrix(rowIndex, COLUMN_SHISAKU_KBN - 1) = aBuhinEdit(index).BuhinNoKbn
                        '改訂'
                        'xls.SetValue(COLUMN_KAITEI, START_ROW + rowIndex, aBuhinEdit(index).BuhinNoKaiteiNo)
                        dataMatrix(rowIndex, COLUMN_KAITEI - 1) = aBuhinEdit(index).BuhinNoKaiteiNo
                        '枝番'
                        'xls.SetValue(COLUMN_EDA_BAN, START_ROW + rowIndex, aBuhinEdit(index).EdaBan)
                        dataMatrix(rowIndex, COLUMN_EDA_BAN - 1) = aBuhinEdit(index).EdaBan
                        '部品名称'
                        'xls.SetValue(COLUMN_BUHIN_NAME, START_ROW + rowIndex, aBuhinEdit(index).BuhinName)
                        dataMatrix(rowIndex, COLUMN_BUHIN_NAME - 1) = aBuhinEdit(index).BuhinName
                        '集計コード'
                        If StringUtil.IsEmpty(aBuhinEdit(index).ShukeiCode) Then
                            'xls.SetValue(COLUMN_SHUKEI_CODE, START_ROW + rowIndex, aBuhinEdit(index).SiaShukeiCode)
                            dataMatrix(rowIndex, COLUMN_SHUKEI_CODE - 1) = aBuhinEdit(index).SiaShukeiCode
                        Else
                            'xls.SetValue(COLUMN_SHUKEI_CODE, START_ROW + rowIndex, aBuhinEdit(index).ShukeiCode)
                            dataMatrix(rowIndex, COLUMN_SHUKEI_CODE - 1) = aBuhinEdit(index).ShukeiCode
                        End If

                        '購担'
                        Dim BuhinEdittmp As TShisakuBuhinEditTmpVo = getBuhinEdittmp(aBuhinEdit(index).BuhinNo)
                        'xls.SetValue(COLUMN_KOUTAN, START_ROW + rowIndex, BuhinEdittmp.Koutan)
                        dataMatrix(rowIndex, COLUMN_KOUTAN - 1) = BuhinEdittmp.Koutan


                        '取引先コードがNULLなら取引先コードを取得する'
                        If StringUtil.IsEmpty(aBuhinEdit(index).MakerCode) Then
                            '取引先コード'
                            'xls.SetValue(COLUMN_TORIHIKISAKI_CODE, START_ROW + rowIndex, BuhinEdittmp.MakerCode)
                            dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_CODE - 1) = BuhinEdittmp.MakerCode
                            '取引先名称'
                            'xls.SetValue(COLUMN_TORIHIKISAKI_NAME, START_ROW + rowIndex, BuhinEdittmp.MakerName)
                            dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_NAME - 1) = BuhinEdittmp.MakerName
                        Else
                            '取引先コード'
                            'xls.SetValue(COLUMN_TORIHIKISAKI_CODE, START_ROW + rowIndex, aBuhinEdit(index).MakerCode)
                            dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_CODE - 1) = aBuhinEdit(index).MakerCode
                            '取引先名称'
                            'xls.SetValue(COLUMN_TORIHIKISAKI_NAME, START_ROW + rowIndex, aBuhinEdit(index).MakerName)
                            dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_NAME - 1) = aBuhinEdit(index).MakerName
                        End If

                        '再使用不可'
                        'xls.SetValue(COLUMN_SAISHIYOUFUKA, START_ROW + rowIndex, aBuhinEdit(index).Saishiyoufuka)
                        dataMatrix(rowIndex, COLUMN_SAISHIYOUFUKA - 1) = aBuhinEdit(index).Saishiyoufuka
                        '供給セクション'
                        'xls.SetValue(COLUMN_KYOUKU_SECTION, START_ROW + rowIndex, aBuhinEdit(index).KyoukuSection)
                        dataMatrix(rowIndex, COLUMN_KYOUKU_SECTION - 1) = aBuhinEdit(index).KyoukuSection

                        '出図予定日'
                        Dim shukkayoteibi As String

                        If aBuhinEdit(index).ShutuzuYoteiDate = 0 Then
                            shukkayoteibi = ""
                        Else
                            If aBuhinEdit(index).ShutuzuYoteiDate = 99999999 Then
                                shukkayoteibi = ""
                            Else
                                shukkayoteibi = Mid(aBuhinEdit(index).ShutuzuYoteiDate.ToString, 1, 4) + "/" + Mid(aBuhinEdit(index).ShutuzuYoteiDate.ToString, 5, 2) + "/" + Mid(aBuhinEdit(index).ShutuzuYoteiDate.ToString, 7, 2)
                            End If
                        End If
                        'xls.SetValue(COLUMN_SHUKKAYOTEIBI, START_ROW + rowIndex, shukkayoteibi)
                        dataMatrix(rowIndex, COLUMN_SHUKKAYOTEIBI - 1) = shukkayoteibi
                        'xls.SetValue(COLUMN_TSUKURIKATA_SEISAKU, START_ROW + rowIndex, aBuhinEdit(index).TsukurikataSeisaku)
                        dataMatrix(rowIndex, COLUMN_TSUKURIKATA_SEISAKU - 1) = aBuhinEdit(index).TsukurikataSeisaku
                        'xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_1, START_ROW + rowIndex, aBuhinEdit(index).TsukurikataKatashiyou1)
                        dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU_1 - 1) = aBuhinEdit(index).TsukurikataKatashiyou1
                        'xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_2, START_ROW + rowIndex, aBuhinEdit(index).TsukurikataKatashiyou2)
                        dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU_2 - 1) = aBuhinEdit(index).TsukurikataKatashiyou2
                        'xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_3, START_ROW + rowIndex, aBuhinEdit(index).TsukurikataKatashiyou3)
                        dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU_3 - 1) = aBuhinEdit(index).TsukurikataKatashiyou3
                        'xls.SetValue(COLUMN_TSUKURIKATA_TIGU, START_ROW + rowIndex, aBuhinEdit(index).TsukurikataTigu)
                        dataMatrix(rowIndex, COLUMN_TSUKURIKATA_TIGU - 1) = aBuhinEdit(index).TsukurikataTigu

                        Dim nounyu As String
                        If aBuhinEdit(index).TsukurikataNounyu = 0 Then
                            nounyu = ""
                        Else
                            If StringUtil.IsEmpty(aBuhinEdit(index).TsukurikataNounyu) Then
                                nounyu = ""
                            Else
                                nounyu = Mid(aBuhinEdit(index).TsukurikataNounyu.ToString, 1, 4) + "/" + Mid(aBuhinEdit(index).TsukurikataNounyu.ToString, 5, 2) + "/" + Mid(aBuhinEdit(index).TsukurikataNounyu.ToString, 7, 2)
                            End If
                        End If
                        'xls.SetValue(COLUMN_TSUKURIKATA_NOUNYU, START_ROW + rowIndex, nounyu)
                        dataMatrix(rowIndex, COLUMN_TSUKURIKATA_NOUNYU - 1) = nounyu
                        'xls.SetValue(COLUMN_TSUKURIKATA_KIBO, START_ROW + rowIndex, aBuhinEdit(index).TsukurikataKibo)
                        dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KIBO - 1) = aBuhinEdit(index).TsukurikataKibo

                        '材質規格１'
                        'xls.SetValue(COLUMN_ZAISHITU_KIKAKU_1, START_ROW + rowIndex, aBuhinEdit(index).ZaishituKikaku1)
                        dataMatrix(rowIndex, COLUMN_ZAISHITU_KIKAKU_1 - 1) = aBuhinEdit(index).ZaishituKikaku1
                        '材質規格２'
                        'xls.SetValue(COLUMN_ZAISHITU_KIKAKU_2, START_ROW + rowIndex, aBuhinEdit(index).ZaishituKikaku2)
                        dataMatrix(rowIndex, COLUMN_ZAISHITU_KIKAKU_2 - 1) = aBuhinEdit(index).ZaishituKikaku2
                        '材質規格３'
                        'xls.SetValue(COLUMN_ZAISHITU_KIKAKU_3, START_ROW + rowIndex, aBuhinEdit(index).ZaishituKikaku3)
                        dataMatrix(rowIndex, COLUMN_ZAISHITU_KIKAKU_3 - 1) = aBuhinEdit(index).ZaishituKikaku3
                        '材質メッキ'
                        'xls.SetValue(COLUMN_ZAISHITU_MEKKI, START_ROW + rowIndex, aBuhinEdit(index).ZaishituMekki)
                        dataMatrix(rowIndex, COLUMN_ZAISHITU_MEKKI - 1) = aBuhinEdit(index).ZaishituMekki
                        '板厚'
                        'xls.SetValue(COLUMN_SHISAKU_BANKO_SURYO, START_ROW + rowIndex, aBuhinEdit(index).ShisakuBankoSuryo)
                        dataMatrix(rowIndex, COLUMN_SHISAKU_BANKO_SURYO - 1) = aBuhinEdit(index).ShisakuBankoSuryo
                        '板厚U'
                        'xls.SetValue(COLUMN_SHISAKU_BANKO_SURYO_U, START_ROW + rowIndex, aBuhinEdit(index).ShisakuBankoSuryoU)
                        dataMatrix(rowIndex, COLUMN_SHISAKU_BANKO_SURYO_U - 1) = aBuhinEdit(index).ShisakuBankoSuryoU


                        ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
                        '材料情報・製品長'
                        'xls.SetValue(COLUMN_MATERIAL_INFO_LENGTH, START_ROW + rowIndex, aBuhinEdit(index).MaterialInfoLength)
                        dataMatrix(rowIndex, COLUMN_MATERIAL_INFO_LENGTH - 1) = NVL(aBuhinEdit(index).MaterialInfoLength)
                        '材料情報・製品幅'
                        'xls.SetValue(COLUMN_MATERIAL_INFO_WIDTH, START_ROW + rowIndex, aBuhinEdit(index).MaterialInfoWidth)
                        dataMatrix(rowIndex, COLUMN_MATERIAL_INFO_WIDTH - 1) = NVL(aBuhinEdit(index).MaterialInfoWidth)
                        'データ項目・改訂№'
                        'xls.SetValue(COLUMN_DATA_ITEM_KAITEI_NO, START_ROW + rowIndex, aBuhinEdit(index).DataItemKaiteiNo)
                        dataMatrix(rowIndex, COLUMN_DATA_ITEM_KAITEI_NO - 1) = NVL(aBuhinEdit(index).DataItemKaiteiNo)
                        'データ項目・エリア名'
                        'xls.SetValue(COLUMN_DATA_ITEM_AREA_NAME, START_ROW + rowIndex, aBuhinEdit(index).DataItemAreaName)
                        dataMatrix(rowIndex, COLUMN_DATA_ITEM_AREA_NAME - 1) = NVL(aBuhinEdit(index).DataItemAreaName)
                        'データ項目・セット名'
                        'xls.SetValue(COLUMN_DATA_ITEM_SET_NAME, START_ROW + rowIndex, aBuhinEdit(index).DataItemSetName)
                        dataMatrix(rowIndex, COLUMN_DATA_ITEM_SET_NAME - 1) = NVL(aBuhinEdit(index).DataItemSetName)
                        'データ項目・改訂情報'
                        'xls.SetValue(COLUMN_DATA_ITEM_KAITEI_INFO, START_ROW + rowIndex, aBuhinEdit(index).DataItemKaiteiInfo)
                        dataMatrix(rowIndex, COLUMN_DATA_ITEM_KAITEI_INFO - 1) = NVL(aBuhinEdit(index).DataItemKaiteiInfo)
                        ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


                        '試作部品費'
                        'xls.SetValue(COLUMN_SHISAKU_BUHIN_HI, START_ROW + rowIndex, aBuhinEdit(index).ShisakuBuhinHi)
                        dataMatrix(rowIndex, COLUMN_SHISAKU_BUHIN_HI - 1) = NVL(aBuhinEdit(index).ShisakuBuhinHi)
                        '試作型費'
                        'xls.SetValue(COLUMN_SHISAKU_KATA_HI, START_ROW + rowIndex, aBuhinEdit(index).ShisakuKataHi)
                        dataMatrix(rowIndex, COLUMN_SHISAKU_KATA_HI - 1) = NVL(aBuhinEdit(index).ShisakuKataHi)
                        'NOTE'
                        'xls.SetValue(COLUMN_NOTE, START_ROW + rowIndex, aBuhinEdit(index).BuhinNote)
                        dataMatrix(rowIndex, COLUMN_NOTE - 1) = NVL(aBuhinEdit(index).BuhinNote)
                        '備考'
                        'xls.SetValue(COLUMN_BIKOU, START_ROW + rowIndex, aBuhinEdit(index).Bikou)
                        dataMatrix(rowIndex, COLUMN_BIKOU - 1) = NVL(aBuhinEdit(index).Bikou)

                        If aBuhinEdit(index).InsuSuryo < 0 Then
                            'xls.SetValue(COLUMN_INSU + aBuhinEdit(index).ShisakuGousyaHyoujiJun - hyoujiHosei, START_ROW + rowIndex, "**")
                            dataMatrix(rowIndex, COLUMN_INSU + aBuhinEdit(index).ShisakuGousyaHyoujiJun - hyoujiHosei - 1) = "**"
                        ElseIf aBuhinEdit(index).InsuSuryo = 0 Then
                        Else
                            'xls.SetValue(COLUMN_INSU + aBuhinEdit(index).ShisakuGousyaHyoujiJun - hyoujiHosei, START_ROW + rowIndex, aBuhinEdit(index).InsuSuryo)
                            dataMatrix(rowIndex, COLUMN_INSU + aBuhinEdit(index).ShisakuGousyaHyoujiJun - hyoujiHosei - 1) = aBuhinEdit(index).InsuSuryo
                        End If

                    End If

                    rowIndex = rowIndex + 1

                Next

                '    '合計員数数量を計算する'
                '    For rowIndex2 As Integer = 0 To rowIndex - 1
                '        Dim totalInsuSuryo As Integer = 0
                '        Dim insu As String
                '        For columnIndex As Integer = 0 To aBaseListVo.Count
                '            Dim colIdx As Integer = COLUMN_INSU + columnIndex
                '            'insu = xls.GetValue(COLUMN_INSU + columnIndex, START_ROW + rowIndex2)
                '            insu = dataMatrix(rowIndex2, colIdx)
                '            If Not StringUtil.IsEmpty(insu) Then

                '                If StringUtil.Equals(insu, "**") Then
                '                    'xls.SetValue(COLUMN_TOTAL_INSU, START_ROW + rowIndex, "**")
                '                    dataMatrix(rowIndex2, colIdx) = "**"
                '                Else
                '                    If Integer.Parse(insu) < 0 Then
                '                        'xls.SetValue(COLUMN_TOTAL_INSU, START_ROW + rowIndex, "**")
                '                        dataMatrix(rowIndex2, colIdx) = "**"
                '                    ElseIf Integer.Parse(insu) = 0 Then
                '                    Else
                '                        totalInsuSuryo = totalInsuSuryo + Integer.Parse(insu)
                '                        'xls.SetValue(COLUMN_TOTAL_INSU, START_ROW + rowIndex2, totalInsuSuryo)
                '                        dataMatrix(rowIndex2, colIdx) = totalInsuSuryo
                '                    End If
                '                End If
                '            Else
                '                'xls.SetValue(COLUMN_TOTAL_INSU, START_ROW + rowIndex, "**")
                '                dataMatrix(rowIndex2, colIdx) = "**"
                '            End If
                '        Next
                '    Next
            End If

            Return rowIndex

        End Function





        ''' <summary>
        ''' Excel出力　シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aBuhinKaiteiList">改訂抽出リスト</param>
        ''' <param name="aGousyaKaiteiList">改訂抽出号車リスト</param>
        ''' <remarks></remarks>
        Public Sub setSheetBodyNEW(ByVal xls As ShisakuExcel, ByVal aBuhinKaiteiList As List(Of TShisakuBuhinEditKaiteiVoHelper), _
                                            ByVal aGousyaKaiteiList As List(Of TShisakuBuhinEditGousyaKaiteiVo), _
                                            ByVal aList As TShisakuListcodeVo, _
                                            ByVal aEventVo As TShisakuEventVo, _
                                            ByVal aBaseListVo As List(Of TShisakuEventBaseVo))

            'ByVal aBuhinKaiteiList As List(Of TShisakuBuhinEditKaiteiVo), _
            Dim KaiteiImpl As KaiteiChusyutuDao = New KaiteiChusyutuDaoImpl
            'Dim flag As Boolean = False

            Dim maxRowNumber As Integer = 0

            'タイトル部分の作成'
            setKaiteiChushutsuTitleRow(xls, aGousyaKaiteiList, aBaseListVo)

            For index As Integer = 0 To aBuhinKaiteiList.Count - 1
                If Not index = 0 Then
                    If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNoHyoujiJun, aBuhinKaiteiList(index - 1).BuhinNoHyoujiJun) Then
                        If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNo, aBuhinKaiteiList(index - 1).BuhinNo) Then
                            If StringUtil.Equals(aBuhinKaiteiList(index).Flag, aBuhinKaiteiList(index - 1).Flag) Then
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
            Dim dataMatrix(maxRowNumber, 100) As String

            Dim rowIndex As Integer = 0
            Dim mergeFlag As Boolean = False
            For index As Integer = 0 To aBuhinKaiteiList.Count - 1


                If Not index = 0 Then
                    If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNoHyoujiJun, aBuhinKaiteiList(index - 1).BuhinNoHyoujiJun) Then
                        If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNo, aBuhinKaiteiList(index - 1).BuhinNo) Then
                            If StringUtil.Equals(aBuhinKaiteiList(index).Flag, aBuhinKaiteiList(index - 1).Flag) Then
                                rowIndex = rowIndex - 1
                                mergeFlag = True
                            End If
                        End If
                    End If
                End If

                If mergeFlag Then
                    '員数のみ'
                    If aBuhinKaiteiList(index).InsuSuryo < 0 Then
                        dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                    ElseIf aBuhinKaiteiList(index).InsuSuryo = 0 Then
                    Else
                        'Dim insu As String = xls.GetValue(COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex)
                        Dim insu As String = dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1)

                        If StringUtil.Equals(insu, "**") Then
                            dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                        Else
                            Dim totalInsu As Integer = 0

                            If StringUtil.IsEmpty(insu) Then
                                dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = aBuhinKaiteiList(index).InsuSuryo
                            Else
                                totalInsu = Integer.Parse(insu)
                                totalInsu = totalInsu + aBuhinKaiteiList(index).InsuSuryo
                                dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = totalInsu
                            End If
                        End If
                    End If
                    mergeFlag = False
                Else
                    'フラグ'
                    dataMatrix(rowIndex, COLUMN_FLAG - 1) = GetFlag(aBuhinKaiteiList(index))
                    'ブロックNo'
                    dataMatrix(rowIndex, COLUMN_BLOCK_NO - 1) = aBuhinKaiteiList(index).ShisakuBlockNo
                    '改訂No'
                    '前回ブロックNo改訂Noが無いときブロックNo改訂Noを設定する'
                    If StringUtil.IsEmpty(aBuhinKaiteiList(index).ZenkaiShisakuBlockNoKaiteiNo) Then
                        dataMatrix(rowIndex, COLUMN_KAITEI_NO - 1) = aBuhinKaiteiList(index).ShisakuBlockNoKaiteiNo
                    Else
                        dataMatrix(rowIndex, COLUMN_KAITEI_NO - 1) = aBuhinKaiteiList(index).ZenkaiShisakuBlockNoKaiteiNo
                    End If
                    '専用マーク'
                    If SenyouCheck(aBuhinKaiteiList(index).BuhinNo, aList.ShisakuSeihinKbn) Then
                        dataMatrix(rowIndex, COLUMN_SENYOU_MARK - 1) = ""
                    Else
                        dataMatrix(rowIndex, COLUMN_SENYOU_MARK - 1) = "*"
                    End If

                    'レベル'
                    dataMatrix(rowIndex, COLUMN_LEVEL - 1) = aBuhinKaiteiList(index).Level
                    '部品番号'
                    dataMatrix(rowIndex, COLUMN_BUHIN_NO - 1) = aBuhinKaiteiList(index).BuhinNo
                    '試作区分'
                    dataMatrix(rowIndex, COLUMN_SHISAKU_KBN - 1) = aBuhinKaiteiList(index).BuhinNoKbn
                    '改訂'
                    dataMatrix(rowIndex, COLUMN_KAITEI - 1) = aBuhinKaiteiList(index).BuhinNoKaiteiNo
                    '枝番'
                    dataMatrix(rowIndex, COLUMN_EDA_BAN - 1) = aBuhinKaiteiList(index).EdaBan
                    '部品名称'
                    dataMatrix(rowIndex, COLUMN_BUHIN_NAME - 1) = aBuhinKaiteiList(index).BuhinName
                    '集計コード'
                    If StringUtil.IsEmpty(aBuhinKaiteiList(index).ShukeiCode) Then
                        dataMatrix(rowIndex, COLUMN_SHUKEI_CODE - 1) = aBuhinKaiteiList(index).SiaShukeiCode
                    Else
                        dataMatrix(rowIndex, COLUMN_SHUKEI_CODE - 1) = aBuhinKaiteiList(index).ShukeiCode
                    End If

                    '購担'

                    Dim BuhinEdittmp As TShisakuBuhinEditTmpVo = getBuhinEdittmp(aBuhinKaiteiList(index).BuhinNo)
                    dataMatrix(rowIndex, COLUMN_KOUTAN - 1) = BuhinEdittmp.Koutan


                    '取引先コードがNULLなら取引先コードを取得する'
                    If StringUtil.IsEmpty(aBuhinKaiteiList(index).MakerCode) Then
                        '取引先コード'
                        dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_CODE - 1) = BuhinEdittmp.MakerCode
                        '取引先名称'
                        dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_NAME - 1) = BuhinEdittmp.MakerName
                    Else
                        '取引先コード'
                        dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_CODE - 1) = aBuhinKaiteiList(index).MakerCode
                        '取引先名称'
                        dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_NAME - 1) = aBuhinKaiteiList(index).MakerName
                    End If



                    '合計員数'
                    'Dim TotalInsuSuryo As Integer = 0
                    'For insuindex As Integer = 0 To aGousyaKaiteiList.Count - 1
                    '    TotalInsuSuryo = TotalInsuSuryo + GetTotalInsuSuryo(aBuhinKaiteiList(index), aGousyaKaiteiList(insuindex))
                    'Next
                    'If TotalInsuSuryo < 0 Then
                    '    dataMatrix(rowIndex,COLUMN_TOTAL_INSU-1)= "**"
                    'Else
                    '    dataMatrix(rowIndex,COLUMN_TOTAL_INSU-1)= TotalInsuSuryo.ToString
                    'End If


                    '再使用不可'
                    dataMatrix(rowIndex, COLUMN_SAISHIYOUFUKA - 1) = aBuhinKaiteiList(index).Saishiyoufuka
                    '供給セクション'
                    dataMatrix(rowIndex, COLUMN_KYOUKU_SECTION - 1) = aBuhinKaiteiList(index).KyoukuSection

                    '出図予定日'
                    Dim shukkayoteibi As String

                    If aBuhinKaiteiList(index).ShutuzuYoteiDate = 0 Then
                        shukkayoteibi = ""
                    Else
                        If aBuhinKaiteiList(index).ShutuzuYoteiDate = 99999999 Then
                            shukkayoteibi = ""
                        Else
                            shukkayoteibi = Mid(aBuhinKaiteiList(index).ShutuzuYoteiDate.ToString, 1, 4) + "/" + Mid(aBuhinKaiteiList(index).ShutuzuYoteiDate.ToString, 5, 2) + "/" + Mid(aBuhinKaiteiList(index).ShutuzuYoteiDate.ToString, 7, 2)
                        End If
                    End If
                    dataMatrix(rowIndex, COLUMN_SHUKKAYOTEIBI - 1) = shukkayoteibi

                    '↓↓2014/09/24 酒井 ADD BEGIN
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_SEISAKU - 1) = aBuhinKaiteiList(index).TsukurikataSeisaku
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU_1 - 1) = aBuhinKaiteiList(index).TsukurikataKatashiyou1
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU_2 - 1) = aBuhinKaiteiList(index).TsukurikataKatashiyou2
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU_3 - 1) = aBuhinKaiteiList(index).TsukurikataKatashiyou3
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_TIGU - 1) = aBuhinKaiteiList(index).TsukurikataTigu

                    Dim nounyu As String

                    If aBuhinKaiteiList(index).TsukurikataNounyu = 0 Then
                        nounyu = ""
                    Else
                        If StringUtil.IsEmpty(aBuhinKaiteiList(index).TsukurikataNounyu) Then
                            nounyu = ""
                        Else
                            nounyu = Mid(aBuhinKaiteiList(index).TsukurikataNounyu.ToString, 1, 4) + "/" + Mid(aBuhinKaiteiList(index).TsukurikataNounyu.ToString, 5, 2) + "/" + Mid(aBuhinKaiteiList(index).TsukurikataNounyu.ToString, 7, 2)
                        End If
                    End If
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_NOUNYU - 1) = nounyu

                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KIBO - 1) = aBuhinKaiteiList(index).TsukurikataKibo
                    '↑↑2014/09/24 酒井 ADD END

                    '材質規格１'
                    dataMatrix(rowIndex, COLUMN_ZAISHITU_KIKAKU_1 - 1) = aBuhinKaiteiList(index).ZaishituKikaku1
                    '材質規格２'
                    dataMatrix(rowIndex, COLUMN_ZAISHITU_KIKAKU_2 - 1) = aBuhinKaiteiList(index).ZaishituKikaku2
                    '材質規格３'
                    dataMatrix(rowIndex, COLUMN_ZAISHITU_KIKAKU_3 - 1) = aBuhinKaiteiList(index).ZaishituKikaku3
                    '材質メッキ'
                    dataMatrix(rowIndex, COLUMN_ZAISHITU_MEKKI - 1) = aBuhinKaiteiList(index).ZaishituMekki
                    '板厚'
                    dataMatrix(rowIndex, COLUMN_SHISAKU_BANKO_SURYO - 1) = aBuhinKaiteiList(index).ShisakuBankoSuryo
                    '板厚U'
                    dataMatrix(rowIndex, COLUMN_SHISAKU_BANKO_SURYO_U - 1) = aBuhinKaiteiList(index).ShisakuBankoSuryoU


                    ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
                    '材料情報・製品長
                    dataMatrix(rowIndex, COLUMN_MATERIAL_INFO_LENGTH - 1) = aBuhinKaiteiList(index).MaterialInfoLength
                    '材料情報・製品幅
                    dataMatrix(rowIndex, COLUMN_MATERIAL_INFO_WIDTH - 1) = aBuhinKaiteiList(index).MaterialInfoWidth
                    'データ項目・改訂№
                    dataMatrix(rowIndex, COLUMN_DATA_ITEM_KAITEI_NO - 1) = aBuhinKaiteiList(index).DataItemKaiteiNo
                    'データ項目・エリア名
                    dataMatrix(rowIndex, COLUMN_DATA_ITEM_AREA_NAME - 1) = aBuhinKaiteiList(index).DataItemAreaName
                    'データ項目・セット名
                    dataMatrix(rowIndex, COLUMN_DATA_ITEM_SET_NAME - 1) = aBuhinKaiteiList(index).DataItemSetName
                    'データ項目・改訂情報
                    dataMatrix(rowIndex, COLUMN_DATA_ITEM_KAITEI_INFO - 1) = aBuhinKaiteiList(index).DataItemKaiteiInfo
                    ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


                    '試作部品費'
                    dataMatrix(rowIndex, COLUMN_SHISAKU_BUHIN_HI - 1) = aBuhinKaiteiList(index).ShisakuBuhinHi
                    '試作型費'
                    dataMatrix(rowIndex, COLUMN_SHISAKU_KATA_HI - 1) = aBuhinKaiteiList(index).ShisakuKataHi
                    'NOTE'
                    dataMatrix(rowIndex, COLUMN_NOTE - 1) = aBuhinKaiteiList(index).BuhinNote
                    '備考'
                    dataMatrix(rowIndex, COLUMN_BIKOU - 1) = aBuhinKaiteiList(index).Bikou

                    '変更前以外は最新の号車情報を使用(２重ループなので時間が掛かる、別の手段を探すべき)'
                    'For gousyaIndex As Integer = 0 To aGousyaKaiteiList.Count - 1
                    '    '基本情報のINDEX番目に該当する号車情報を探す'
                    '    For Insu As Integer = 0 To InsuCount
                    '        If StringUtil.Equals(aGousyaKaiteiList(gousyaIndex).ShisakuGousya, xls.GetValue(COLUMN_INSU + Insu, TITLE_ROW)) Then
                    '            If CheckGousyaKihon(aBuhinKaiteiList(index), aGousyaKaiteiList(gousyaIndex)) Then
                    '                dataMatrix(rowIndex,COLUMN_INSU + Insu-1)=ousyaKaiteiList(gousyaIndex).InsuSuryo
                    '            End If
                    '        End If
                    '    Next
                    'Next

                    '号車の数だけまわす'
                    'For gousyaIndex As Integer = 0 To aBaseListVo.Count - 1
                    '    '号車における部品番号の合計数値を員数部に表示する'
                    '    Dim GousyaInsu As New Integer

                    '    GousyaInsu = KaiteiImpl.FindByGousyaKaiteiInsu(aBuhinKaiteiList(index).ShisakuEventCode, _
                    '                                           aBuhinKaiteiList(index).ShisakuBukaCode, _
                    '                                           aBuhinKaiteiList(index).ShisakuBlockNo, _
                    '                                           aBuhinKaiteiList(index).ShisakuBlockNoKaiteiNo, _
                    '                                           aBuhinKaiteiList(index).BuhinNoHyoujiJun, _
                    '                                           xls.GetValue(COLUMN_INSU + gousyaIndex, TITLE_ROW))

                    '    xls.SetValue(COLUMN_INSU + gousyaIndex, START_ROW + index, GousyaInsu)
                    'Next

                    If aBuhinKaiteiList(index).InsuSuryo < 0 Then
                        dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                    ElseIf aBuhinKaiteiList(index).InsuSuryo = 0 Then
                    Else
                        dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = aBuhinKaiteiList(index).InsuSuryo
                    End If

                End If

                rowIndex = rowIndex + 1

            Next

            '合計員数数量を計算する'
            For rowIndex2 As Integer = 0 To rowIndex - 1
                Dim totalInsuSuryo As Integer = 0
                Dim insu As String
                For columnIndex As Integer = 0 To aBaseListVo.Count

                    insu = dataMatrix(rowIndex2, COLUMN_INSU + columnIndex - 1)
                    If Not StringUtil.IsEmpty(insu) Then

                        If StringUtil.Equals(insu, "**") Then
                            dataMatrix(rowIndex, COLUMN_TOTAL_INSU - 1) = "**"
                        Else
                            If Integer.Parse(insu) < 0 Then
                                dataMatrix(rowIndex, COLUMN_TOTAL_INSU - 1) = "**"
                            ElseIf Integer.Parse(insu) = 0 Then

                            Else
                                totalInsuSuryo = totalInsuSuryo + Integer.Parse(insu)
                                dataMatrix(rowIndex2, COLUMN_TOTAL_INSU - 1) = totalInsuSuryo
                            End If
                        End If

                    Else
                        dataMatrix(rowIndex, COLUMN_TOTAL_INSU - 1) = "**"
                    End If
                Next

            Next

            xls.CopyRange(0, START_ROW, UBound(dataMatrix, 2) - 1, START_ROW + UBound(dataMatrix), dataMatrix)

            '列の幅を自動調整する'
            xls.AutoFitCol(COLUMN_FLAG, xls.EndCol())
        End Sub

        ''' <summary>
        ''' タイトル行の作成
        ''' </summary>
        ''' <param name="xls">目的のEXCELファイル</param>
        ''' <param name="aGousyaKaiteiListVo">部品編集号車改訂情報</param>
        ''' <remarks></remarks>
        Private Function setKaiteiChushutsuTitleRow(ByVal xls As ShisakuExcel, ByVal aGousyaKaiteiListVo As List(Of TShisakuBuhinEditGousyaKaiteiVo), _
                                            ByVal aBaseListVo As List(Of TShisakuEventBaseVo)) As Integer

            'フラグ'
            xls.MergeCells(COLUMN_FLAG, TITLE_ROW, COLUMN_FLAG, TITLE_ROW_2, True)
            xls.SetOrientation(COLUMN_FLAG, TITLE_ROW, COLUMN_FLAG, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_FLAG, TITLE_ROW, "フラグ  ")
            'ブロックNo'
            xls.MergeCells(COLUMN_BLOCK_NO, TITLE_ROW, COLUMN_BLOCK_NO, TITLE_ROW_2, True)
            xls.SetOrientation(COLUMN_BLOCK_NO, TITLE_ROW, COLUMN_BLOCK_NO, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_BLOCK_NO, TITLE_ROW, "ブロックNo")
            '改訂No'
            xls.MergeCells(COLUMN_KAITEI_NO, TITLE_ROW, COLUMN_KAITEI_NO, TITLE_ROW_2, True)
            xls.SetOrientation(COLUMN_KAITEI_NO, TITLE_ROW, COLUMN_KAITEI_NO, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_KAITEI_NO, TITLE_ROW, "改訂No")
            '専用マーク'
            xls.MergeCells(COLUMN_SENYOU_MARK, TITLE_ROW, COLUMN_SENYOU_MARK, TITLE_ROW_2, True)
            xls.SetOrientation(COLUMN_SENYOU_MARK, TITLE_ROW, COLUMN_SENYOU_MARK, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_SENYOU_MARK, TITLE_ROW, "専用マーク")
            'レベル'
            xls.MergeCells(COLUMN_LEVEL, TITLE_ROW, COLUMN_LEVEL, TITLE_ROW_2, True)
            xls.SetOrientation(COLUMN_LEVEL, TITLE_ROW, COLUMN_LEVEL, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_LEVEL, TITLE_ROW, "レベル")
            '部品番号'
            xls.MergeCells(COLUMN_BUHIN_NO, TITLE_ROW, COLUMN_BUHIN_NO, TITLE_ROW_2, True)
            xls.SetValue(COLUMN_BUHIN_NO, TITLE_ROW, "部品番号")
            '試作区分'
            xls.MergeCells(COLUMN_SHISAKU_KBN, TITLE_ROW, COLUMN_SHISAKU_KBN, TITLE_ROW_2, True)
            xls.SetValue(COLUMN_SHISAKU_KBN, TITLE_ROW, "試作区分")
            '改訂'
            xls.MergeCells(COLUMN_KAITEI, TITLE_ROW, COLUMN_KAITEI, TITLE_ROW_2, True)
            xls.SetOrientation(COLUMN_KAITEI, TITLE_ROW, COLUMN_KAITEI, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_KAITEI, TITLE_ROW, "改訂")
            '枝番'
            xls.MergeCells(COLUMN_EDA_BAN, TITLE_ROW, COLUMN_EDA_BAN, TITLE_ROW_2, True)
            xls.SetOrientation(COLUMN_EDA_BAN, TITLE_ROW, COLUMN_EDA_BAN, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_EDA_BAN, TITLE_ROW, "枝番")
            '部品名称'
            xls.MergeCells(COLUMN_BUHIN_NAME, TITLE_ROW, COLUMN_BUHIN_NAME, TITLE_ROW_2, True)
            xls.SetValue(COLUMN_BUHIN_NAME, TITLE_ROW, "部品名称")
            '集計コード'
            xls.MergeCells(COLUMN_SHUKEI_CODE, TITLE_ROW, COLUMN_SHUKEI_CODE, TITLE_ROW_2, True)
            xls.SetOrientation(COLUMN_SHUKEI_CODE, TITLE_ROW, COLUMN_SHUKEI_CODE, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_SHUKEI_CODE, TITLE_ROW, "集計コード")
            '購担'
            xls.MergeCells(COLUMN_KOUTAN, TITLE_ROW, COLUMN_KOUTAN, TITLE_ROW_2, True)
            xls.SetOrientation(COLUMN_KOUTAN, TITLE_ROW, COLUMN_KOUTAN, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_KOUTAN, TITLE_ROW, "購担")
            '取引先コード'
            xls.MergeCells(COLUMN_TORIHIKISAKI_CODE, TITLE_ROW, COLUMN_TORIHIKISAKI_CODE, TITLE_ROW_2, True)
            xls.SetOrientation(COLUMN_TORIHIKISAKI_CODE, TITLE_ROW, COLUMN_TORIHIKISAKI_CODE, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_TORIHIKISAKI_CODE, TITLE_ROW, "取引先コード")
            '取引先名称'
            xls.MergeCells(COLUMN_TORIHIKISAKI_NAME, TITLE_ROW, COLUMN_TORIHIKISAKI_NAME, TITLE_ROW_2, True)
            xls.SetOrientation(COLUMN_TORIHIKISAKI_NAME, TITLE_ROW, COLUMN_TORIHIKISAKI_NAME, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_TORIHIKISAKI_NAME, TITLE_ROW, "取引先名称")
            '合計員数数量'
            xls.MergeCells(COLUMN_TOTAL_INSU, TITLE_ROW, COLUMN_TOTAL_INSU, TITLE_ROW_2, True)
            xls.SetOrientation(COLUMN_TOTAL_INSU, TITLE_ROW, COLUMN_TOTAL_INSU, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_TOTAL_INSU, TITLE_ROW, "員数")
            '再使用不可'
            xls.MergeCells(COLUMN_SAISHIYOUFUKA, TITLE_ROW, COLUMN_SAISHIYOUFUKA, TITLE_ROW_2, True)
            xls.SetOrientation(COLUMN_SAISHIYOUFUKA, TITLE_ROW, COLUMN_SAISHIYOUFUKA, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetValue(COLUMN_SAISHIYOUFUKA, TITLE_ROW, "再使用不可")
            '供給セクション'
            xls.MergeCells(COLUMN_KYOUKU_SECTION, TITLE_ROW, COLUMN_KYOUKU_SECTION, TITLE_ROW_2, True)
            xls.SetValue(COLUMN_KYOUKU_SECTION, TITLE_ROW, "供給セクション")
            '出荷予定日'
            xls.MergeCells(COLUMN_SHUKKAYOTEIBI, TITLE_ROW, COLUMN_SHUKKAYOTEIBI, TITLE_ROW_2, True)
            xls.SetValue(COLUMN_SHUKKAYOTEIBI, TITLE_ROW, "出図予定日")
            '↓↓2014/09/24 酒井 ADD BEGIN
            xls.MergeCells(COLUMN_TSUKURIKATA_SEISAKU, TITLE_ROW, COLUMN_TSUKURIKATA_KIBO, TITLE_ROW, True)
            xls.SetValue(COLUMN_TSUKURIKATA_SEISAKU, TITLE_ROW, "作り方")
            xls.SetValue(COLUMN_TSUKURIKATA_SEISAKU, TITLE_ROW_2, "製作方法")
            xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_1, TITLE_ROW_2, "型仕様1")
            xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_2, TITLE_ROW_2, "型仕様2")
            xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_3, TITLE_ROW_2, "型仕様3")
            xls.SetValue(COLUMN_TSUKURIKATA_TIGU, TITLE_ROW_2, "治具")
            xls.SetValue(COLUMN_TSUKURIKATA_NOUNYU, TITLE_ROW_2, "納入見通し")
            xls.SetValue(COLUMN_TSUKURIKATA_KIBO, TITLE_ROW_2, "部品製作規模・概要")
            '↑↑2014/09/24 酒井 ADD END
            '材質'
            xls.MergeCells(COLUMN_ZAISHITU_KIKAKU_1, TITLE_ROW, COLUMN_ZAISHITU_MEKKI, TITLE_ROW, True)
            xls.SetValue(COLUMN_ZAISHITU_KIKAKU_1, TITLE_ROW, "材質")
            '材質規格１'
            xls.SetValue(COLUMN_ZAISHITU_KIKAKU_1, TITLE_ROW_2, "規格１")
            '材質規格２'
            xls.SetValue(COLUMN_ZAISHITU_KIKAKU_2, TITLE_ROW_2, "規格2")
            '材質規格３'
            xls.SetValue(COLUMN_ZAISHITU_KIKAKU_3, TITLE_ROW_2, "規格3")
            '材質メッキ'
            xls.SetValue(COLUMN_ZAISHITU_MEKKI, TITLE_ROW_2, "メッキ")
            '板厚'
            xls.MergeCells(COLUMN_SHISAKU_BANKO_SURYO, TITLE_ROW, COLUMN_SHISAKU_BANKO_SURYO_U, TITLE_ROW, True)
            xls.SetValue(COLUMN_SHISAKU_BANKO_SURYO, TITLE_ROW, "板厚")
            '板厚数量'
            xls.SetValue(COLUMN_SHISAKU_BANKO_SURYO, TITLE_ROW_2, "板厚")
            '板厚数量U'
            xls.SetValue(COLUMN_SHISAKU_BANKO_SURYO_U, TITLE_ROW_2, "u")


            ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
            '材料情報'
            xls.MergeCells(COLUMN_MATERIAL_INFO_LENGTH, TITLE_ROW, COLUMN_MATERIAL_INFO_WIDTH, TITLE_ROW, True)
            xls.SetValue(COLUMN_MATERIAL_INFO_LENGTH, TITLE_ROW, "材料情報")
            '材料情報・製品長'
            xls.SetValue(COLUMN_MATERIAL_INFO_LENGTH, TITLE_ROW_2, "製品長")
            '材料情報・製品幅'
            xls.SetValue(COLUMN_MATERIAL_INFO_WIDTH, TITLE_ROW_2, "製品幅")
            'データ項目'
            xls.MergeCells(COLUMN_DATA_ITEM_KAITEI_NO, TITLE_ROW, COLUMN_DATA_ITEM_KAITEI_INFO, TITLE_ROW, True)
            xls.SetValue(COLUMN_DATA_ITEM_KAITEI_NO, TITLE_ROW, "データ項目")
            'データ項目・改訂№'
            xls.SetValue(COLUMN_DATA_ITEM_KAITEI_NO, TITLE_ROW_2, "改訂№")
            'データ項目・エリア名'
            xls.SetValue(COLUMN_DATA_ITEM_AREA_NAME, TITLE_ROW_2, "エリア名")
            'データ項目・セット名'
            xls.SetValue(COLUMN_DATA_ITEM_SET_NAME, TITLE_ROW_2, "セット名")
            'データ項目・改訂情報'
            xls.SetValue(COLUMN_DATA_ITEM_KAITEI_INFO, TITLE_ROW_2, "改訂情報")
            ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


            '試作部品費'
            xls.MergeCells(COLUMN_SHISAKU_BUHIN_HI, TITLE_ROW, COLUMN_SHISAKU_BUHIN_HI, TITLE_ROW_2, True)
            xls.SetValue(COLUMN_SHISAKU_BUHIN_HI, TITLE_ROW, "試作部品費")
            '試作型費'
            xls.MergeCells(COLUMN_SHISAKU_KATA_HI, TITLE_ROW, COLUMN_SHISAKU_KATA_HI, TITLE_ROW_2, True)
            xls.SetValue(COLUMN_SHISAKU_KATA_HI, TITLE_ROW, "試作型費")
            'NOTE'
            xls.MergeCells(COLUMN_NOTE, TITLE_ROW, COLUMN_NOTE, TITLE_ROW_2, True)
            xls.SetValue(COLUMN_NOTE, TITLE_ROW, "NOTE")
            '備考'
            xls.MergeCells(COLUMN_BIKOU, TITLE_ROW, COLUMN_BIKOU, TITLE_ROW_2, True)
            xls.SetValue(COLUMN_BIKOU, TITLE_ROW, "備考")

            InsuCount = 0

            Dim GousyaCount As Integer = 0
            'ベース車情報を元に号車列を作成'
            For gousyaIndex As Integer = 0 To aBaseListVo.Count - 1
                If Not StringUtil.Equals(aBaseListVo(gousyaIndex).ShisakuGousya, "DUMMY") Then
                    xls.MergeCells(COLUMN_INSU + GousyaCount, TITLE_ROW, COLUMN_INSU + GousyaCount, TITLE_ROW_2, True)
                    xls.SetValue(COLUMN_INSU + GousyaCount, TITLE_ROW, aBaseListVo(gousyaIndex).ShisakuGousya)
                    xls.SetOrientation(COLUMN_INSU + GousyaCount, TITLE_ROW, COLUMN_INSU + GousyaCount, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                    GousyaCount += 1
                End If
            Next
            xls.MergeCells(COLUMN_INSU + GousyaCount, TITLE_ROW, COLUMN_INSU + GousyaCount, TITLE_ROW_2, True)
            xls.SetColWidth(COLUMN_INSU + GousyaCount, COLUMN_INSU + GousyaCount + 1, 3)
            xls.MergeCells(COLUMN_INSU + GousyaCount + 1, TITLE_ROW, COLUMN_INSU + GousyaCount + 1, TITLE_ROW_2, True)
            xls.SetValue(COLUMN_INSU + GousyaCount + 1, TITLE_ROW, "DUMMY")
            xls.SetOrientation(COLUMN_INSU + GousyaCount + 1, TITLE_ROW, COLUMN_INSU + GousyaCount + 1, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)

            Return COLUMN_INSU + GousyaCount
        End Function

        ''' <summary>
        ''' シートの設定(行の高さや列の幅等)
        ''' </summary>
        ''' <param name="xls">目的EXCELファイル</param>
        ''' <remarks></remarks>
        Private Sub setSheetColumnWidth(ByVal xls As ShisakuExcel)
            '員数行の高さを自動調整'
            'xls.AutoFitRow(TITLE_ROW, TITLE_ROW)
            xls.SetRowHeight(TITLE_ROW, TITLE_ROW, 104)
            '列の幅を自動調整'
            xls.AutoFitCol(COLUMN_FLAG, xls.EndCol())
            'ウィンドウ枠の固定'
            'xls.FreezePanes(COLUMN_TOTAL_INSU, TITLE_ROW_2 + 1, True)

        End Sub

        ''' <summary>
        ''' 基本情報に紐付く号車情報を検索
        ''' </summary>
        ''' <param name="aBuhinKaiteiVo">部品編集改訂情報</param>
        ''' <param name="aGousyaKaiteiVo">部品編集号車改訂情報</param>
        ''' <returns>基本情報に紐付いた号車情報であればTrue</returns>
        ''' <remarks></remarks>
        Private Function CheckGousyaKihon(ByVal aBuhinKaiteiVo As TShisakuBuhinEditKaiteiVo, ByVal aGousyaKaiteiVo As TShisakuBuhinEditGousyaKaiteiVo) As Boolean
            If Not StringUtil.Equals(aBuhinKaiteiVo.ShisakuBukaCode, aGousyaKaiteiVo.ShisakuBukaCode) Then
                Return False
            End If
            If Not StringUtil.Equals(aBuhinKaiteiVo.ShisakuBlockNo, aGousyaKaiteiVo.ShisakuBlockNo) Then
                Return False
            End If
            If Not StringUtil.Equals(aBuhinKaiteiVo.BuhinNoHyoujiJun, aGousyaKaiteiVo.BuhinNoHyoujiJun) Then
                Return False
            End If
            If Not StringUtil.Equals(aBuhinKaiteiVo.ShisakuListCode, aGousyaKaiteiVo.ShisakuListCode) Then
                Return False
            End If
            Return True
        End Function

        ''' <summary>
        ''' 合計員数を計算
        ''' </summary>
        ''' <param name="aBuhinKaiteiVo">部品編集改訂情報</param>
        ''' <param name="aGousyaKaiteiVo">部品編集号車改訂情報</param>
        ''' <returns>員数</returns>
        ''' <remarks></remarks>
        Private Function GetTotalInsuSuryo(ByVal aBuhinKaiteiVo As TShisakuBuhinEditKaiteiVo, ByVal aGousyaKaiteiVo As TShisakuBuhinEditGousyaKaiteiVo) As Integer
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
        ''' フラグの表示文字列
        ''' </summary>
        ''' <param name="aBuhinKaiteiVo">部品編集改訂情報</param>
        ''' <returns>フラグの表示文字列</returns>
        ''' <remarks></remarks>
        Private Function GetFlag(ByVal aBuhinKaiteiVo As TShisakuBuhinEditKaiteiVo) As String
            Dim result As String = ""

            '変更無しはブランク'

            Select Case aBuhinKaiteiVo.Flag
                Case "1"
                    result = "追加"
                Case "2"
                    result = "削除"
                Case "3"
                    result = "変更後"
                Case "4"
                    result = "変更前"
                Case "0"
                    result = ""
                Case "7"
                    result = "削除"
            End Select

            Return result
        End Function

        ''' <summary>
        ''' 各カラム名に数値を設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetColumnNo()
            Dim column As Integer = 1
            COLUMN_FLAG = EzUtil.Increment(column)
            COLUMN_BLOCK_NO = EzUtil.Increment(column)
            COLUMN_KAITEI_NO = EzUtil.Increment(column)
            COLUMN_SENYOU_MARK = EzUtil.Increment(column)
            COLUMN_LEVEL = EzUtil.Increment(column)
            COLUMN_BUHIN_NO = EzUtil.Increment(column)
            COLUMN_SHISAKU_KBN = EzUtil.Increment(column)
            COLUMN_KAITEI = EzUtil.Increment(column)
            COLUMN_EDA_BAN = EzUtil.Increment(column)
            COLUMN_BUHIN_NAME = EzUtil.Increment(column)
            COLUMN_SHUKEI_CODE = EzUtil.Increment(column)
            COLUMN_KOUTAN = EzUtil.Increment(column)
            COLUMN_TORIHIKISAKI_CODE = EzUtil.Increment(column)
            COLUMN_TORIHIKISAKI_NAME = EzUtil.Increment(column)
            COLUMN_TOTAL_INSU = EzUtil.Increment(column)
            COLUMN_SAISHIYOUFUKA = EzUtil.Increment(column)
            COLUMN_KYOUKU_SECTION = EzUtil.Increment(column)
            COLUMN_SHUKKAYOTEIBI = EzUtil.Increment(column)
            '↓↓2014/09/24 酒井 ADD BEGIN
            COLUMN_TSUKURIKATA_SEISAKU = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KATASHIYOU_1 = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KATASHIYOU_2 = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KATASHIYOU_3 = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_TIGU = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_NOUNYU = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KIBO = EzUtil.Increment(column)
            'COLUMN_BASE_BUHIN_FLG  = EzUtil.Increment(column)
            '↑↑2014/09/24 酒井 ADD END
            COLUMN_ZAISHITU_KIKAKU_1 = EzUtil.Increment(column)
            COLUMN_ZAISHITU_KIKAKU_2 = EzUtil.Increment(column)
            COLUMN_ZAISHITU_KIKAKU_3 = EzUtil.Increment(column)
            COLUMN_ZAISHITU_MEKKI = EzUtil.Increment(column)
            COLUMN_SHISAKU_BANKO_SURYO = EzUtil.Increment(column)
            COLUMN_SHISAKU_BANKO_SURYO_U = EzUtil.Increment(column)


            ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
            COLUMN_MATERIAL_INFO_LENGTH = EzUtil.Increment(column)
            COLUMN_MATERIAL_INFO_WIDTH = EzUtil.Increment(column)
            COLUMN_DATA_ITEM_KAITEI_NO = EzUtil.Increment(column)
            COLUMN_DATA_ITEM_AREA_NAME = EzUtil.Increment(column)
            COLUMN_DATA_ITEM_SET_NAME = EzUtil.Increment(column)
            COLUMN_DATA_ITEM_KAITEI_INFO = EzUtil.Increment(column)
            ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


            COLUMN_SHISAKU_BUHIN_HI = EzUtil.Increment(column)
            COLUMN_SHISAKU_KATA_HI = EzUtil.Increment(column)
            COLUMN_NOTE = EzUtil.Increment(column)
            COLUMN_BIKOU = EzUtil.Increment(column)
            COLUMN_INSU = EzUtil.Increment(column)

        End Sub

        Private Sub SetAutoOrikomiSheet(ByVal xls As ShisakuExcel, _
                                         ByVal aList As TShisakuListcodeVo, _
                                         ByVal aBuhinKaiteiList As List(Of TShisakuBuhinEditKaiteiVoHelper), _
                                         ByVal aGousyaKaiteiListVo As List(Of TShisakuBuhinEditGousyaKaiteiVo), _
                                         ByVal aEventVo As TShisakuEventVo, _
                                         ByVal aBaseListVo As List(Of TShisakuEventBaseVo))
            xls.SetActiveSheet(2)
            xls.SetSheetName("自動織込み不可")

            SetColumnNo()

            setSheetOrikomiHeader(xls, aList, aEventVo)

            setSheetColumnWidth(xls)

            setSheetOrikomiBody(xls, aBuhinKaiteiList, aGousyaKaiteiListVo, aList, aEventVo, aBaseListVo)
        End Sub

        ''' <summary>
        ''' Excel出力　シートのHeaderの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aList">リストコード情報</param>
        ''' <remarks></remarks>
        Public Sub setSheetOrikomiHeader(ByVal xls As ShisakuExcel, _
                                   ByVal aList As TShisakuListcodeVo, _
                                   ByVal aEventVo As TShisakuEventVo)

            'TODO 仕様変更に耐えうるようにすべき'
            'ユニット区分用にイベント情報も追加()

            'イベントコード'
            xls.MergeCells(1, 1, 4, 1, True)
            xls.SetValue(1, 1, "イベントコード : ")
            xls.MergeCells(5, 1, 9, 1, True)
            xls.SetValue(5, 1, aList.ShisakuListCode)
            'イベント名称'
            xls.MergeCells(1, 2, 4, 2, True)
            xls.SetValue(1, 2, "イベント名称 : ")
            xls.MergeCells(5, 2, 9, 2, True)
            xls.SetValue(5, 2, aEventVo.ShisakuKaihatsuFugo + " " + aList.ShisakuEventName)
            '工事指令No'
            xls.MergeCells(1, 3, 4, 3, True)
            xls.SetValue(1, 3, "工事指令No : ")
            xls.MergeCells(5, 3, 9, 3, True)
            xls.SetValue(5, 3, aList.ShisakuKoujiShireiNo)
            '抽出日時'
            xls.MergeCells(1, 4, 4, 4, True)
            xls.SetValue(1, 4, "抽出日時")
            xls.MergeCells(5, 4, 9, 4, True)

            Dim chusyutubi As String
            Dim chusyutujikan As String

            chusyutubi = Mid(aList.SaishinChusyutubi.ToString, 1, 4) + "/" + Mid(aList.SaishinChusyutubi.ToString, 5, 2) + "/" + Mid(aList.SaishinChusyutubi.ToString, 7, 2)

            '-----------------------------------------------------------
            '２次改修
            '   日付が取得できなければシステム日付を設定する。
            If aList.SaishinChusyutubi.ToString = "0" Then
                chusyutubi = DateTime.Now.ToString("yyyy/MM/dd")
            End If
            '-----------------------------------------------------------

            If aList.SaishinChusyutujikan.ToString.Length < 6 Then
                chusyutujikan = Mid(aList.SaishinChusyutujikan.ToString, 1, 1) + ":" + Mid(aList.SaishinChusyutujikan.ToString, 2, 2) + ":" + Mid(aList.SaishinChusyutujikan.ToString, 4, 2)
            Else
                chusyutujikan = Mid(aList.SaishinChusyutujikan.ToString, 1, 2) + ":" + Mid(aList.SaishinChusyutujikan.ToString, 3, 2) + ":" + Mid(aList.SaishinChusyutujikan.ToString, 5, 2)
            End If

            '-----------------------------------------------------------
            '２次改修
            '   時間が取得できなければシステム時間を設定する。
            If aList.SaishinChusyutujikan.ToString = "0" Then
                chusyutujikan = DateTime.Now.ToString("HH:mm:ss")
            End If
            '-----------------------------------------------------------

            xls.SetValue(5, 4, chusyutubi + " " + chusyutujikan)
        End Sub

        ''' <summary>
        ''' 自動織込み結果
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <param name="aBuhinKaiteiList"></param>
        ''' <param name="aGousyaKaiteiList"></param>
        ''' <param name="aList"></param>
        ''' <param name="aEventVo"></param>
        ''' <param name="aBaseListVo"></param>
        ''' <remarks></remarks>
        Public Sub setSheetOrikomiBody(ByVal xls As ShisakuExcel, ByVal aBuhinKaiteiList As List(Of TShisakuBuhinEditKaiteiVoHelper), _
                                    ByVal aGousyaKaiteiList As List(Of TShisakuBuhinEditGousyaKaiteiVo), _
                                    ByVal aList As TShisakuListcodeVo, _
                                    ByVal aEventVo As TShisakuEventVo, _
                                    ByVal aBaseListVo As List(Of TShisakuEventBaseVo))

            'ByVal aBuhinKaiteiList As List(Of TShisakuBuhinEditKaiteiVo), _
            Dim sakuseiImpl As TehaichoSakusei.Dao.TehaichoSakuseiDao = New TehaichoSakusei.Dao.TehaichoSakuseiDaoImpl
            Dim KaiteiImpl As KaiteiChusyutuDao = New KaiteiChusyutuDaoImpl
            'Dim flag As Boolean = False

            'タイトル部分の作成'
            setKaiteiChushutsuTitleRow(xls, aGousyaKaiteiList, aBaseListVo)

            Dim rowIndex As Integer = 0
            Dim mergeFlag As Boolean = False
            For index As Integer = 0 To aBuhinKaiteiList.Count - 1

                '自動織込み改訂NoがNULLのものはだめ'
                If StringUtil.IsEmpty(aBuhinKaiteiList(index).AutoOrikomiKaiteiNo) Then
                    Continue For
                End If
                '追加は出力しない'
                If StringUtil.Equals(aBuhinKaiteiList(index).Flag, "1") Then
                    Continue For
                End If


                If Not index = 0 Then
                    If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNoHyoujiJun, aBuhinKaiteiList(index - 1).BuhinNoHyoujiJun) Then
                        If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNo, aBuhinKaiteiList(index - 1).BuhinNo) Then
                            If StringUtil.Equals(aBuhinKaiteiList(index).Flag, aBuhinKaiteiList(index - 1).Flag) Then
                                rowIndex = rowIndex - 1
                                mergeFlag = True
                            End If
                        End If
                    End If
                End If

                If mergeFlag Then
                    '員数のみ'
                    If aBuhinKaiteiList(index).InsuSuryo < 0 Then
                        xls.SetValue(COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, "**")
                    ElseIf aBuhinKaiteiList(index).InsuSuryo = 0 Then
                    Else
                        Dim insu As String = xls.GetValue(COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex)

                        If StringUtil.Equals(insu, "**") Then
                            xls.SetValue(COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, "**")
                        Else
                            Dim totalInsu As Integer = 0

                            If StringUtil.IsEmpty(insu) Then
                                xls.SetValue(COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, aBuhinKaiteiList(index).InsuSuryo)
                            Else
                                totalInsu = Integer.Parse(insu)
                                totalInsu = totalInsu + aBuhinKaiteiList(index).InsuSuryo
                                xls.SetValue(COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, totalInsu)
                            End If
                        End If
                    End If
                    mergeFlag = False
                Else
                    'フラグ'
                    xls.SetValue(COLUMN_FLAG, START_ROW + rowIndex, GetFlag(aBuhinKaiteiList(index)))
                    'ブロックNo'
                    xls.SetValue(COLUMN_BLOCK_NO, START_ROW + rowIndex, aBuhinKaiteiList(index).ShisakuBlockNo)
                    '改訂No'
                    '前回ブロックNo改訂Noが無いときブロックNo改訂Noを設定する'
                    If StringUtil.IsEmpty(aBuhinKaiteiList(index).ZenkaiShisakuBlockNoKaiteiNo) Then
                        xls.SetValue(COLUMN_KAITEI_NO, START_ROW + rowIndex, aBuhinKaiteiList(index).ShisakuBlockNoKaiteiNo)
                    Else
                        xls.SetValue(COLUMN_KAITEI_NO, START_ROW + rowIndex, aBuhinKaiteiList(index).ZenkaiShisakuBlockNoKaiteiNo)
                    End If
                    '専用マーク'
                    If sakuseiImpl.FindBySenyouCheck(aBuhinKaiteiList(index).BuhinNo, aList.ShisakuSeihinKbn) Then
                        xls.SetValue(COLUMN_SENYOU_MARK, START_ROW + rowIndex, "")
                    Else
                        xls.SetValue(COLUMN_SENYOU_MARK, START_ROW + rowIndex, "*")
                    End If

                    'レベル'
                    xls.SetValue(COLUMN_LEVEL, START_ROW + rowIndex, aBuhinKaiteiList(index).Level)
                    '部品番号'
                    xls.SetValue(COLUMN_BUHIN_NO, START_ROW + rowIndex, aBuhinKaiteiList(index).BuhinNo)
                    '試作区分'
                    xls.SetValue(COLUMN_SHISAKU_KBN, START_ROW + rowIndex, aBuhinKaiteiList(index).BuhinNoKbn)
                    '改訂'
                    xls.SetValue(COLUMN_KAITEI, START_ROW + rowIndex, aBuhinKaiteiList(index).BuhinNoKaiteiNo)
                    '枝番'
                    xls.SetValue(COLUMN_EDA_BAN, START_ROW + rowIndex, aBuhinKaiteiList(index).EdaBan)
                    '部品名称'
                    xls.SetValue(COLUMN_BUHIN_NAME, START_ROW + rowIndex, aBuhinKaiteiList(index).BuhinName)
                    '集計コード'
                    If StringUtil.IsEmpty(aBuhinKaiteiList(index).ShukeiCode) Then
                        xls.SetValue(COLUMN_SHUKEI_CODE, START_ROW + rowIndex, aBuhinKaiteiList(index).SiaShukeiCode)
                    Else
                        xls.SetValue(COLUMN_SHUKEI_CODE, START_ROW + rowIndex, aBuhinKaiteiList(index).ShukeiCode)
                    End If

                    '購担'

                    Dim BuhinEdittmp As New TShisakuBuhinEditTmpVo
                    BuhinEdittmp = sakuseiImpl.FindByKoutanTorihikisaki(aBuhinKaiteiList(index).BuhinNo)

                    xls.SetValue(COLUMN_KOUTAN, START_ROW + rowIndex, BuhinEdittmp.Koutan)


                    '取引先コードがNULLなら取引先コードを取得する'
                    If StringUtil.IsEmpty(aBuhinKaiteiList(index).MakerCode) Then
                        '取引先コード'
                        xls.SetValue(COLUMN_TORIHIKISAKI_CODE, START_ROW + rowIndex, BuhinEdittmp.MakerCode)
                        '取引先名称'
                        xls.SetValue(COLUMN_TORIHIKISAKI_NAME, START_ROW + rowIndex, BuhinEdittmp.MakerName)
                    Else
                        '取引先コード'
                        xls.SetValue(COLUMN_TORIHIKISAKI_CODE, START_ROW + rowIndex, aBuhinKaiteiList(index).MakerCode)
                        '取引先名称'
                        xls.SetValue(COLUMN_TORIHIKISAKI_NAME, START_ROW + rowIndex, aBuhinKaiteiList(index).MakerName)
                    End If



                    '合計員数'
                    'Dim TotalInsuSuryo As Integer = 0
                    'For insuindex As Integer = 0 To aGousyaKaiteiList.Count - 1
                    '    TotalInsuSuryo = TotalInsuSuryo + GetTotalInsuSuryo(aBuhinKaiteiList(index), aGousyaKaiteiList(insuindex))
                    'Next
                    'If TotalInsuSuryo < 0 Then
                    '    xls.SetValue(COLUMN_TOTAL_INSU, START_ROW + rowIndex, "**")
                    'Else
                    '    xls.SetValue(COLUMN_TOTAL_INSU, START_ROW + rowIndex, TotalInsuSuryo.ToString)
                    'End If


                    '再使用不可'
                    xls.SetValue(COLUMN_SAISHIYOUFUKA, START_ROW + rowIndex, aBuhinKaiteiList(index).Saishiyoufuka)
                    '供給セクション'
                    xls.SetValue(COLUMN_KYOUKU_SECTION, START_ROW + rowIndex, aBuhinKaiteiList(index).KyoukuSection)

                    '出図予定日'
                    Dim shukkayoteibi As String

                    If aBuhinKaiteiList(index).ShutuzuYoteiDate = 0 Then
                        shukkayoteibi = ""
                    Else
                        If aBuhinKaiteiList(index).ShutuzuYoteiDate = 99999999 Then
                            shukkayoteibi = ""
                        Else
                            shukkayoteibi = Mid(aBuhinKaiteiList(index).ShutuzuYoteiDate.ToString, 1, 4) + "/" + Mid(aBuhinKaiteiList(index).ShutuzuYoteiDate.ToString, 5, 2) + "/" + Mid(aBuhinKaiteiList(index).ShutuzuYoteiDate.ToString, 7, 2)
                        End If
                    End If
                    xls.SetValue(COLUMN_SHUKKAYOTEIBI, START_ROW + rowIndex, shukkayoteibi)

                    '↓↓2014/09/24 酒井 ADD BEGIN
                    xls.SetValue(COLUMN_TSUKURIKATA_SEISAKU, START_ROW + rowIndex, aBuhinKaiteiList(index).TsukurikataSeisaku)
                    xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_1, START_ROW + rowIndex, aBuhinKaiteiList(index).TsukurikataKatashiyou1)
                    xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_2, START_ROW + rowIndex, aBuhinKaiteiList(index).TsukurikataKatashiyou2)
                    xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_3, START_ROW + rowIndex, aBuhinKaiteiList(index).TsukurikataKatashiyou3)
                    xls.SetValue(COLUMN_TSUKURIKATA_TIGU, START_ROW + rowIndex, aBuhinKaiteiList(index).TsukurikataTigu)

                    Dim nounyu As String

                    If aBuhinKaiteiList(index).TsukurikataNounyu = 0 Then
                        nounyu = ""
                    Else
                        If StringUtil.IsEmpty(aBuhinKaiteiList(index).TsukurikataNounyu) Then
                            nounyu = ""
                        Else
                            nounyu = Mid(aBuhinKaiteiList(index).TsukurikataNounyu.ToString, 1, 4) + "/" + Mid(aBuhinKaiteiList(index).TsukurikataNounyu.ToString, 5, 2) + "/" + Mid(aBuhinKaiteiList(index).TsukurikataNounyu.ToString, 7, 2)
                        End If
                    End If
                    xls.SetValue(COLUMN_TSUKURIKATA_NOUNYU, START_ROW + rowIndex, nounyu)

                    xls.SetValue(COLUMN_TSUKURIKATA_KIBO, START_ROW + rowIndex, aBuhinKaiteiList(index).TsukurikataKibo)
                    '↑↑2014/09/24 酒井 ADD END

                    '材質規格１'
                    xls.SetValue(COLUMN_ZAISHITU_KIKAKU_1, START_ROW + rowIndex, aBuhinKaiteiList(index).ZaishituKikaku1)
                    '材質規格２'
                    xls.SetValue(COLUMN_ZAISHITU_KIKAKU_2, START_ROW + rowIndex, aBuhinKaiteiList(index).ZaishituKikaku2)
                    '材質規格３'
                    xls.SetValue(COLUMN_ZAISHITU_KIKAKU_3, START_ROW + rowIndex, aBuhinKaiteiList(index).ZaishituKikaku3)
                    '材質メッキ'
                    xls.SetValue(COLUMN_ZAISHITU_MEKKI, START_ROW + rowIndex, aBuhinKaiteiList(index).ZaishituMekki)
                    '板厚'
                    xls.SetValue(COLUMN_SHISAKU_BANKO_SURYO, START_ROW + rowIndex, aBuhinKaiteiList(index).ShisakuBankoSuryo)
                    '板厚U'
                    xls.SetValue(COLUMN_SHISAKU_BANKO_SURYO_U, START_ROW + rowIndex, aBuhinKaiteiList(index).ShisakuBankoSuryoU)


                    ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
                    '材料情報・製品長'
                    xls.SetValue(COLUMN_MATERIAL_INFO_LENGTH, START_ROW + rowIndex, aBuhinKaiteiList(index).MaterialInfoLength)
                    '材料情報・製品幅'
                    xls.SetValue(COLUMN_MATERIAL_INFO_WIDTH, START_ROW + rowIndex, aBuhinKaiteiList(index).MaterialInfoWidth)
                    'データ項目・改訂№'
                    xls.SetValue(COLUMN_DATA_ITEM_KAITEI_NO, START_ROW + rowIndex, aBuhinKaiteiList(index).DataItemKaiteiNo)
                    'データ項目・エリア名'
                    xls.SetValue(COLUMN_DATA_ITEM_AREA_NAME, START_ROW + rowIndex, aBuhinKaiteiList(index).DataItemAreaName)
                    'データ項目・セット名'
                    xls.SetValue(COLUMN_DATA_ITEM_SET_NAME, START_ROW + rowIndex, aBuhinKaiteiList(index).DataItemSetName)
                    'データ項目・改訂情報'
                    xls.SetValue(COLUMN_DATA_ITEM_KAITEI_INFO, START_ROW + rowIndex, aBuhinKaiteiList(index).DataItemKaiteiInfo)
                    ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


                    '試作部品費'
                    xls.SetValue(COLUMN_SHISAKU_BUHIN_HI, START_ROW + rowIndex, aBuhinKaiteiList(index).ShisakuBuhinHi)
                    '試作型費'
                    xls.SetValue(COLUMN_SHISAKU_KATA_HI, START_ROW + rowIndex, aBuhinKaiteiList(index).ShisakuKataHi)
                    '供給セクション'
                    xls.SetValue(COLUMN_NOTE, START_ROW + rowIndex, aBuhinKaiteiList(index).BuhinNote)
                    '備考'
                    xls.SetValue(COLUMN_BIKOU, START_ROW + rowIndex, aBuhinKaiteiList(index).Bikou)

                    '変更前以外は最新の号車情報を使用(２重ループなので時間が掛かる、別の手段を探すべき)'
                    'For gousyaIndex As Integer = 0 To aGousyaKaiteiList.Count - 1
                    '    '基本情報のINDEX番目に該当する号車情報を探す'
                    '    For Insu As Integer = 0 To InsuCount
                    '        If StringUtil.Equals(aGousyaKaiteiList(gousyaIndex).ShisakuGousya, xls.GetValue(COLUMN_INSU + Insu, TITLE_ROW)) Then
                    '            If CheckGousyaKihon(aBuhinKaiteiList(index), aGousyaKaiteiList(gousyaIndex)) Then
                    '                xls.SetValue(COLUMN_INSU + Insu, START_ROW + index, aGousyaKaiteiList(gousyaIndex).InsuSuryo)
                    '            End If
                    '        End If
                    '    Next
                    'Next

                    '号車の数だけまわす'
                    'For gousyaIndex As Integer = 0 To aBaseListVo.Count - 1
                    '    '号車における部品番号の合計数値を員数部に表示する'
                    '    Dim GousyaInsu As New Integer

                    '    GousyaInsu = KaiteiImpl.FindByGousyaKaiteiInsu(aBuhinKaiteiList(index).ShisakuEventCode, _
                    '                                           aBuhinKaiteiList(index).ShisakuBukaCode, _
                    '                                           aBuhinKaiteiList(index).ShisakuBlockNo, _
                    '                                           aBuhinKaiteiList(index).ShisakuBlockNoKaiteiNo, _
                    '                                           aBuhinKaiteiList(index).BuhinNoHyoujiJun, _
                    '                                           xls.GetValue(COLUMN_INSU + gousyaIndex, TITLE_ROW))

                    '    xls.SetValue(COLUMN_INSU + gousyaIndex, START_ROW + index, GousyaInsu)
                    'Next

                    If aBuhinKaiteiList(index).InsuSuryo < 0 Then
                        xls.SetValue(COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, "**")
                    ElseIf aBuhinKaiteiList(index).InsuSuryo = 0 Then
                    Else
                        xls.SetValue(COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, aBuhinKaiteiList(index).InsuSuryo)
                    End If

                End If

                rowIndex = rowIndex + 1

            Next

            '合計員数数量を計算する'
            For rowIndex2 As Integer = 0 To rowIndex - 1
                Dim totalInsuSuryo As Integer = 0
                Dim insu As String
                For columnIndex As Integer = 0 To aBaseListVo.Count

                    insu = xls.GetValue(COLUMN_INSU + columnIndex, START_ROW + rowIndex2)
                    If Not StringUtil.IsEmpty(insu) Then

                        If StringUtil.Equals(insu, "**") Then
                            xls.SetValue(COLUMN_TOTAL_INSU, START_ROW + rowIndex, "**")
                        Else
                            If Integer.Parse(insu) < 0 Then
                                xls.SetValue(COLUMN_TOTAL_INSU, START_ROW + rowIndex, "**")
                            ElseIf Integer.Parse(insu) = 0 Then

                            Else
                                totalInsuSuryo = totalInsuSuryo + Integer.Parse(insu)
                                xls.SetValue(COLUMN_TOTAL_INSU, START_ROW + rowIndex2, totalInsuSuryo)
                            End If
                        End If

                    Else
                        xls.SetValue(COLUMN_TOTAL_INSU, START_ROW + rowIndex, "**")
                    End If
                Next

            Next

            '列の幅を自動調整する'
            xls.AutoFitCol(COLUMN_FLAG, xls.EndCol())
        End Sub

        ''' <summary>
        ''' 自動織込み結果
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <param name="aBuhinKaiteiList"></param>
        ''' <param name="aGousyaKaiteiList"></param>
        ''' <param name="aList"></param>
        ''' <param name="aEventVo"></param>
        ''' <param name="aBaseListVo"></param>
        ''' <remarks></remarks>
        Public Sub setSheetOrikomiBodyNEW(ByVal xls As ShisakuExcel, ByVal aBuhinKaiteiList As List(Of TShisakuBuhinEditKaiteiVoHelper), _
                                    ByVal aGousyaKaiteiList As List(Of TShisakuBuhinEditGousyaKaiteiVo), _
                                    ByVal aList As TShisakuListcodeVo, _
                                    ByVal aEventVo As TShisakuEventVo, _
                                    ByVal aBaseListVo As List(Of TShisakuEventBaseVo))

            'ByVal aBuhinKaiteiList As List(Of TShisakuBuhinEditKaiteiVo), _
            Dim sakuseiImpl As TehaichoSakusei.Dao.TehaichoSakuseiDao = New TehaichoSakusei.Dao.TehaichoSakuseiDaoImpl
            Dim KaiteiImpl As KaiteiChusyutuDao = New KaiteiChusyutuDaoImpl
            'Dim flag As Boolean = False

            'タイトル部分の作成'
            setKaiteiChushutsuTitleRow(xls, aGousyaKaiteiList, aBaseListVo)

            Dim maxRowNumber As Integer = 0
            '２次元排配列の大きさを定義
            For index As Integer = 0 To aBuhinKaiteiList.Count - 1
                '自動織込み改訂NoがNULLのものはだめ'
                If StringUtil.IsEmpty(aBuhinKaiteiList(index).AutoOrikomiKaiteiNo) Then
                    Continue For
                End If
                '追加は織込み確定なので出力しない'
                If StringUtil.Equals(aBuhinKaiteiList(index).Flag, "1") Then
                    Continue For
                End If

                If Not index = 0 Then
                    If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNoHyoujiJun, aBuhinKaiteiList(index - 1).BuhinNoHyoujiJun) Then
                        If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNo, aBuhinKaiteiList(index - 1).BuhinNo) Then
                            If StringUtil.Equals(aBuhinKaiteiList(index).Flag, aBuhinKaiteiList(index - 1).Flag) Then
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

            Dim dataMatrix(maxRowNumber, 100) As String

            Dim rowIndex As Integer = 0
            Dim mergeFlag As Boolean = False


            For index As Integer = 0 To aBuhinKaiteiList.Count - 1

                '自動織込み改訂NoがNULLのものはだめ'
                If StringUtil.IsEmpty(aBuhinKaiteiList(index).AutoOrikomiKaiteiNo) Then
                    Continue For
                End If
                '追加は出力しない'
                If StringUtil.Equals(aBuhinKaiteiList(index).Flag, "1") Then
                    Continue For
                End If

                If Not index = 0 Then
                    If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNoHyoujiJun, aBuhinKaiteiList(index - 1).BuhinNoHyoujiJun) Then
                        If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNo, aBuhinKaiteiList(index - 1).BuhinNo) Then
                            If StringUtil.Equals(aBuhinKaiteiList(index).Flag, aBuhinKaiteiList(index - 1).Flag) Then
                                rowIndex = rowIndex - 1
                                mergeFlag = True
                            End If
                        End If
                    End If
                End If

                If mergeFlag Then
                    '員数のみ'
                    If aBuhinKaiteiList(index).InsuSuryo < 0 Then
                        dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                    ElseIf aBuhinKaiteiList(index).InsuSuryo = 0 Then
                    Else
                        Dim insu As String = dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1)

                        If StringUtil.Equals(insu, "**") Then
                            dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                        Else
                            Dim totalInsu As Integer = 0

                            If StringUtil.IsEmpty(insu) Then
                                dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = aBuhinKaiteiList(index).InsuSuryo
                            Else
                                totalInsu = Integer.Parse(insu)
                                totalInsu = totalInsu + aBuhinKaiteiList(index).InsuSuryo
                                dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = totalInsu
                            End If
                        End If
                    End If
                    mergeFlag = False
                Else
                    'フラグ'
                    dataMatrix(rowIndex, COLUMN_FLAG - 1) = GetFlag(aBuhinKaiteiList(index))
                    'ブロックNo'
                    dataMatrix(rowIndex, COLUMN_BLOCK_NO - 1) = aBuhinKaiteiList(index).ShisakuBlockNo
                    '改訂No'
                    '前回ブロックNo改訂Noが無いときブロックNo改訂Noを設定する'
                    If StringUtil.IsEmpty(aBuhinKaiteiList(index).ZenkaiShisakuBlockNoKaiteiNo) Then
                        dataMatrix(rowIndex, COLUMN_KAITEI_NO - 1) = aBuhinKaiteiList(index).ShisakuBlockNoKaiteiNo
                    Else
                        dataMatrix(rowIndex, COLUMN_KAITEI_NO - 1) = aBuhinKaiteiList(index).ZenkaiShisakuBlockNoKaiteiNo
                    End If
                    '専用マーク'
                    If sakuseiImpl.FindBySenyouCheck(aBuhinKaiteiList(index).BuhinNo, aList.ShisakuSeihinKbn) Then
                        dataMatrix(rowIndex, COLUMN_SENYOU_MARK - 1) = ""
                    Else
                        dataMatrix(rowIndex, COLUMN_SENYOU_MARK - 1) = "*"
                    End If

                    'レベル'
                    dataMatrix(rowIndex, COLUMN_LEVEL - 1) = aBuhinKaiteiList(index).Level
                    '部品番号'
                    dataMatrix(rowIndex, COLUMN_BUHIN_NO - 1) = aBuhinKaiteiList(index).BuhinNo
                    '試作区分'
                    dataMatrix(rowIndex, COLUMN_SHISAKU_KBN - 1) = aBuhinKaiteiList(index).BuhinNoKbn
                    '改訂'
                    dataMatrix(rowIndex, COLUMN_KAITEI - 1) = aBuhinKaiteiList(index).BuhinNoKaiteiNo
                    '枝番'
                    dataMatrix(rowIndex, COLUMN_EDA_BAN - 1) = aBuhinKaiteiList(index).EdaBan
                    '部品名称'
                    dataMatrix(rowIndex, COLUMN_BUHIN_NAME - 1) = aBuhinKaiteiList(index).BuhinName
                    '集計コード'
                    If StringUtil.IsEmpty(aBuhinKaiteiList(index).ShukeiCode) Then
                        dataMatrix(rowIndex, COLUMN_SHUKEI_CODE - 1) = aBuhinKaiteiList(index).SiaShukeiCode
                    Else
                        dataMatrix(rowIndex, COLUMN_SHUKEI_CODE - 1) = aBuhinKaiteiList(index).ShukeiCode
                    End If

                    '購担'
                    'ここでの購担の取得もボトルネック
                    Dim BuhinEdittmp As New TShisakuBuhinEditTmpVo
                    BuhinEdittmp = sakuseiImpl.FindByKoutanTorihikisaki(aBuhinKaiteiList(index).BuhinNo)

                    dataMatrix(rowIndex, COLUMN_KOUTAN - 1) = BuhinEdittmp.Koutan


                    '取引先コードがNULLなら取引先コードを取得する'
                    If StringUtil.IsEmpty(aBuhinKaiteiList(index).MakerCode) Then
                        '取引先コード'
                        dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_CODE - 1) = BuhinEdittmp.MakerCode
                        '取引先名称'
                        dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_NAME - 1) = BuhinEdittmp.MakerName
                    Else
                        '取引先コード'
                        dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_CODE - 1) = aBuhinKaiteiList(index).MakerCode
                        '取引先名称'
                        dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_NAME - 1) = aBuhinKaiteiList(index).MakerName
                    End If



                    '合計員数'
                    'Dim TotalInsuSuryo As Integer = 0
                    'For insuindex As Integer = 0 To aGousyaKaiteiList.Count - 1
                    '    TotalInsuSuryo = TotalInsuSuryo + GetTotalInsuSuryo(aBuhinKaiteiList(index), aGousyaKaiteiList(insuindex))
                    'Next
                    'If TotalInsuSuryo < 0 Then
                    '    dataMatrix(rowIndex,COLUMN_TOTAL_INSU-1) =  "**"
                    'Else
                    '    dataMatrix(rowIndex,COLUMN_TOTAL_INSU-1) =  TotalInsuSuryo.ToString
                    'End If


                    '再使用不可'
                    dataMatrix(rowIndex, COLUMN_SAISHIYOUFUKA - 1) = aBuhinKaiteiList(index).Saishiyoufuka
                    '供給セクション'
                    dataMatrix(rowIndex, COLUMN_KYOUKU_SECTION - 1) = aBuhinKaiteiList(index).KyoukuSection

                    '出図予定日'
                    Dim shukkayoteibi As String

                    If aBuhinKaiteiList(index).ShutuzuYoteiDate = 0 Then
                        shukkayoteibi = ""
                    Else
                        If aBuhinKaiteiList(index).ShutuzuYoteiDate = 99999999 Then
                            shukkayoteibi = ""
                        Else
                            shukkayoteibi = Mid(aBuhinKaiteiList(index).ShutuzuYoteiDate.ToString, 1, 4) + "/" + Mid(aBuhinKaiteiList(index).ShutuzuYoteiDate.ToString, 5, 2) + "/" + Mid(aBuhinKaiteiList(index).ShutuzuYoteiDate.ToString, 7, 2)
                        End If
                    End If
                    dataMatrix(rowIndex, COLUMN_SHUKKAYOTEIBI - 1) = shukkayoteibi

                    '↓↓2014/09/24 酒井 ADD BEGIN
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_SEISAKU - 1) = aBuhinKaiteiList(index).TsukurikataSeisaku
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU_1 - 1) = aBuhinKaiteiList(index).TsukurikataKatashiyou1
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU_2 - 1) = aBuhinKaiteiList(index).TsukurikataKatashiyou2
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU_3 - 1) = aBuhinKaiteiList(index).TsukurikataKatashiyou3
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_TIGU - 1) = aBuhinKaiteiList(index).TsukurikataTigu

                    Dim nounyu As String

                    If aBuhinKaiteiList(index).TsukurikataNounyu = 0 Then
                        nounyu = ""
                    Else
                        If StringUtil.IsEmpty(aBuhinKaiteiList(index).TsukurikataNounyu) Then
                            nounyu = ""
                        Else
                            nounyu = Mid(aBuhinKaiteiList(index).TsukurikataNounyu.ToString, 1, 4) + "/" + Mid(aBuhinKaiteiList(index).TsukurikataNounyu.ToString, 5, 2) + "/" + Mid(aBuhinKaiteiList(index).TsukurikataNounyu.ToString, 7, 2)
                        End If
                    End If
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_NOUNYU - 1) = nounyu

                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KIBO - 1) = aBuhinKaiteiList(index).TsukurikataKibo
                    '↑↑2014/09/24 酒井 ADD END

                    '材質規格１'
                    dataMatrix(rowIndex, COLUMN_ZAISHITU_KIKAKU_1 - 1) = aBuhinKaiteiList(index).ZaishituKikaku1
                    '材質規格２'
                    dataMatrix(rowIndex, COLUMN_ZAISHITU_KIKAKU_2 - 1) = aBuhinKaiteiList(index).ZaishituKikaku2
                    '材質規格３'
                    dataMatrix(rowIndex, COLUMN_ZAISHITU_KIKAKU_3 - 1) = aBuhinKaiteiList(index).ZaishituKikaku3
                    '材質メッキ'
                    dataMatrix(rowIndex, COLUMN_ZAISHITU_MEKKI - 1) = aBuhinKaiteiList(index).ZaishituMekki
                    '板厚'
                    dataMatrix(rowIndex, COLUMN_SHISAKU_BANKO_SURYO - 1) = aBuhinKaiteiList(index).ShisakuBankoSuryo
                    '板厚U'
                    dataMatrix(rowIndex, COLUMN_SHISAKU_BANKO_SURYO_U - 1) = aBuhinKaiteiList(index).ShisakuBankoSuryoU


                    ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
                    '材料情報・製品長'
                    dataMatrix(rowIndex, COLUMN_MATERIAL_INFO_LENGTH - 1) = aBuhinKaiteiList(index).MaterialInfoLength
                    '材料情報・製品幅'
                    dataMatrix(rowIndex, COLUMN_MATERIAL_INFO_WIDTH - 1) = aBuhinKaiteiList(index).MaterialInfoWidth
                    'データ項目・改訂№'
                    dataMatrix(rowIndex, COLUMN_DATA_ITEM_KAITEI_NO - 1) = aBuhinKaiteiList(index).DataItemKaiteiNo
                    'データ項目・エリア名'
                    dataMatrix(rowIndex, COLUMN_DATA_ITEM_AREA_NAME - 1) = aBuhinKaiteiList(index).DataItemAreaName
                    'データ項目・セット名'
                    dataMatrix(rowIndex, COLUMN_DATA_ITEM_SET_NAME - 1) = aBuhinKaiteiList(index).DataItemSetName
                    'データ項目・改訂情報'
                    dataMatrix(rowIndex, COLUMN_DATA_ITEM_KAITEI_INFO - 1) = aBuhinKaiteiList(index).DataItemKaiteiInfo
                    ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


                    '試作部品費'
                    dataMatrix(rowIndex, COLUMN_SHISAKU_BUHIN_HI - 1) = aBuhinKaiteiList(index).ShisakuBuhinHi
                    '試作型費'
                    dataMatrix(rowIndex, COLUMN_SHISAKU_KATA_HI - 1) = aBuhinKaiteiList(index).ShisakuKataHi
                    '供給セクション'
                    dataMatrix(rowIndex, COLUMN_NOTE - 1) = aBuhinKaiteiList(index).BuhinNote
                    '備考'
                    dataMatrix(rowIndex, COLUMN_BIKOU - 1) = aBuhinKaiteiList(index).Bikou


                    If aBuhinKaiteiList(index).InsuSuryo < 0 Then
                        dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                    ElseIf aBuhinKaiteiList(index).InsuSuryo = 0 Then
                    Else
                        dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = aBuhinKaiteiList(index).InsuSuryo
                    End If

                End If

                rowIndex = rowIndex + 1

            Next

            '合計員数数量を計算する'
            For rowIndex2 As Integer = 0 To rowIndex - 1
                Dim totalInsuSuryo As Integer = 0
                Dim insu As String
                For columnIndex As Integer = 0 To aBaseListVo.Count

                    insu = dataMatrix(rowIndex2, COLUMN_INSU + columnIndex - 1)
                    If Not StringUtil.IsEmpty(insu) Then

                        If StringUtil.Equals(insu, "**") Then
                            dataMatrix(rowIndex, COLUMN_TOTAL_INSU - 1) = "**"
                        Else
                            If Integer.Parse(insu) < 0 Then
                                dataMatrix(rowIndex, COLUMN_TOTAL_INSU - 1) = "**"
                            ElseIf Integer.Parse(insu) = 0 Then

                            Else
                                totalInsuSuryo = totalInsuSuryo + Integer.Parse(insu)
                                dataMatrix(rowIndex2, COLUMN_TOTAL_INSU - 1) = totalInsuSuryo
                            End If
                        End If

                    Else
                        dataMatrix(rowIndex, COLUMN_TOTAL_INSU - 1) = "**"
                    End If
                Next

            Next
            xls.CopyRange(0, START_ROW, UBound(dataMatrix, 2) - 1, START_ROW + UBound(dataMatrix), dataMatrix)

            '列の幅を自動調整する'
            xls.AutoFitCol(COLUMN_FLAG, xls.EndCol())
        End Sub

#Region "明細部各列名の設定"
        '' フラグ
        Private COLUMN_FLAG As Integer
        '' ブロックNo
        Private COLUMN_BLOCK_NO As Integer
        '' 改訂No
        Private COLUMN_KAITEI_NO As Integer
        '' 専用マーク
        Private COLUMN_SENYOU_MARK As Integer
        '' レベル
        Private COLUMN_LEVEL As Integer
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
        '' 集計コード
        Private COLUMN_SHUKEI_CODE As Integer
        '' 購担
        Private COLUMN_KOUTAN As Integer
        '' 取引先コード
        Private COLUMN_TORIHIKISAKI_CODE As Integer
        '' 取引先名称
        Private COLUMN_TORIHIKISAKI_NAME As Integer
        '' 合計員数
        Private COLUMN_TOTAL_INSU As Integer
        '' 再使用不可
        Private COLUMN_SAISHIYOUFUKA As Integer
        '' 供給セクション
        Private COLUMN_KYOUKU_SECTION As Integer
        '' 出荷予定日
        Private COLUMN_SHUKKAYOTEIBI As Integer
        '↓↓2014/09/24 酒井 ADD BEGIN
        Private COLUMN_TSUKURIKATA_SEISAKU As Integer
        Private COLUMN_TSUKURIKATA_KATASHIYOU_1 As Integer
        Private COLUMN_TSUKURIKATA_KATASHIYOU_2 As Integer
        Private COLUMN_TSUKURIKATA_KATASHIYOU_3 As Integer
        Private COLUMN_TSUKURIKATA_TIGU As Integer
        Private COLUMN_TSUKURIKATA_NOUNYU As Integer
        Private COLUMN_TSUKURIKATA_KIBO As Integer
        Private COLUMN_BASE_BUHIN_FLG As Integer
        '↑↑2014/09/24 酒井 ADD END

        '' 材質規格１
        Private COLUMN_ZAISHITU_KIKAKU_1 As Integer
        '' 材質規格２
        Private COLUMN_ZAISHITU_KIKAKU_2 As Integer
        '' 材質規格３
        Private COLUMN_ZAISHITU_KIKAKU_3 As Integer
        '' 材質メッキ
        Private COLUMN_ZAISHITU_MEKKI As Integer
        '' 板厚
        Private COLUMN_SHISAKU_BANKO_SURYO As Integer
        '' 板厚U
        Private COLUMN_SHISAKU_BANKO_SURYO_U As Integer


        ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
        '' 材料情報・製品長
        Private COLUMN_MATERIAL_INFO_LENGTH As Integer
        '' 材料情報・製品幅
        Private COLUMN_MATERIAL_INFO_WIDTH As Integer
        '' データ項目・改訂№
        Private COLUMN_DATA_ITEM_KAITEI_NO As Integer
        '' データ項目・エリア名
        Private COLUMN_DATA_ITEM_AREA_NAME As Integer
        '' データ項目・セット名
        Private COLUMN_DATA_ITEM_SET_NAME As Integer
        '' データ項目・改訂情報
        Private COLUMN_DATA_ITEM_KAITEI_INFO As Integer
        ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


        '' 試作部品費
        Private COLUMN_SHISAKU_BUHIN_HI As Integer
        '' 試作型費
        Private COLUMN_SHISAKU_KATA_HI As Integer
        '' NOTE
        Private COLUMN_NOTE As Integer
        '' 備考
        Private COLUMN_BIKOU As Integer
        '' 員数
        Private COLUMN_INSU As Integer

#End Region

#Region "各行の番号指定"

        Private TITLE_ROW As Integer = 5

        Private START_ROW As Integer = 7

        Private TITLE_ROW_2 As Integer = 6

#End Region

        ''' <summary>
        ''' Excel出力　新調達シート
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aList">リストコード情報</param>
        ''' <param name="aBuhinKaiteiList">訂正基本リスト</param>
        ''' <param name="aGousyaList">訂正号車リスト</param>
        ''' <param name="aEventVo">イベント情報</param>
        ''' <remarks></remarks>
        Private Sub setShinchotasuSheet(ByVal xls As ShisakuExcel, _
                                         ByVal aList As TShisakuListcodeVo, _
                                         ByVal aBuhinKaiteiList As List(Of TShisakuBuhinEditKaiteiVoHelper), _
                                         ByVal aGousyaList As List(Of TShisakuBuhinEditGousyaKaiteiVo), _
                                         ByVal aEventVo As TShisakuEventVo, _
                                         ByVal aBaseListVo As List(Of TShisakuEventBaseVo))
            TITLE_ROW = 4

            START_ROW = 5


            xls.SetActiveSheet(3)
            xls.SetSheetName("新調達")

            SetShinchotasuColumnNo()

            setShinchotasuSheetHeard(xls, aList, aEventVo)

            setShinchotasuSheetBodyNEW(xls, aBuhinKaiteiList, aGousyaList, aList, aEventVo, aBaseListVo)

            setShinchotasuSheetSheetColumnWidth(xls)

        End Sub

        ''' <summary>
        ''' 各カラム名に数値を設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetShinchotasuColumnNo()
            Dim column As Integer = 1
            COLUMN_FLAG = EzUtil.Increment(column)
            COLUMN_SYASYU = EzUtil.Increment(column)
            COLUMN_BLOCK_NO = EzUtil.Increment(column)
            COLUMN_KOUJI_NO = EzUtil.Increment(column)
            COLUMN_GYOU_ID = EzUtil.Increment(column)
            COLUMN_KANRI_NO = EzUtil.Increment(column)
            COLUMN_SENYOU_MARK = EzUtil.Increment(column)
            COLUMN_RIREKI = EzUtil.Increment(column)
            COLUMN_LEVEL = EzUtil.Increment(column)
            COLUMN_UNIT_KBN = EzUtil.Increment(column)
            COLUMN_BUHIN_NO = EzUtil.Increment(column)
            COLUMN_KAITEI_NO = EzUtil.Increment(column)
            COLUMN_BUHIN_NAME = EzUtil.Increment(column)
            COLUMN_KOUTAN = EzUtil.Increment(column)
            COLUMN_TORIHIKISAKI_CODE = EzUtil.Increment(column)
            COLUMN_DANDORI = EzUtil.Increment(column)
            COLUMN_KOSU = EzUtil.Increment(column)
            COLUMN_INSU = EzUtil.Increment(column)

        End Sub

#Region "明細部各列名の設定（新調達）"
        '' フラグ
        'Private COLUMN_FLAG As Integer
        '' 車種
        Private COLUMN_SYASYU As Integer
        '' ブロックNo
        'Private COLUMN_BLOCK_NO As Integer
        '' 工事No
        Private COLUMN_KOUJI_NO As Integer
        '' 行ID.
        Private COLUMN_GYOU_ID As Integer
        '' 管理No
        Private COLUMN_KANRI_NO As Integer
        '' 専用マーク
        'Private COLUMN_SENYOU_MARK As Integer
        '' 履歴
        Private COLUMN_RIREKI As Integer
        '' レベル
        'Private COLUMN_LEVEL As Integer
        '' ユニット区分
        Private COLUMN_UNIT_KBN As Integer
        '' 部品番号
        'Private COLUMN_BUHIN_NO As Integer
        '' 改訂No
        'Private COLUMN_KAITEI_NO As Integer
        '' 部品名称
        'Private COLUMN_BUHIN_NAME As Integer
        '' 購担
        'Private COLUMN_KOUTAN As Integer
        '' 取引先コード
        'Private COLUMN_TORIHIKISAKI_CODE As Integer
        '' 段取り
        Private COLUMN_DANDORI As Integer
        '' 工数
        Private COLUMN_KOSU As Integer
        '' 員数
        'Private COLUMN_INSU As Integer


#End Region

#Region "各行の番号指定"

        'Private TITLE_ROW As Integer = 4

        'Private START_ROW As Integer = 5

        Private INSU_ROW As Integer = 3

#End Region

        ''' <summary>
        ''' Excel出力　新調達シートのHeaderの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aList">リストコード情報</param>
        ''' <remarks></remarks>
        Public Sub setShinchotasuSheetHeard(ByVal xls As ShisakuExcel, _
                                  ByVal aList As TShisakuListcodeVo, _
                                  ByVal aEventVo As TShisakuEventVo)

            'TODO 仕様変更に耐えうるようにすべき'
            'ユニット区分用にイベント情報も追加()

            'イベントコード'
            xls.SetValue(1, 1, "イベント名称 : " + aEventVo.ShisakuKaihatsuFugo + " " + aList.ShisakuEventName)
            '工事指令No'
            xls.SetValue(1, 2, "工事指令No. :" + aList.ShisakuKoujiShireiNo)
            '抽出日時'
            Dim chusyutubi As String
            Dim chusyutujikan As String


            chusyutubi = Mid(aList.SaishinChusyutubi.ToString, 1, 4) + "/" + Mid(aList.SaishinChusyutubi.ToString, 5, 2) + "/" + Mid(aList.SaishinChusyutubi.ToString, 7, 2)

            '-----------------------------------------------------------
            '２次改修
            '   日付が取得できなければシステム日付を設定する。
            If aList.SaishinChusyutubi.ToString = "0" Then
                chusyutubi = DateTime.Now.ToString("yyyy/MM/dd")
            End If
            '-----------------------------------------------------------

            If aList.SaishinChusyutujikan.ToString.Length < 6 Then
                chusyutujikan = Mid(aList.SaishinChusyutujikan.ToString, 1, 1) + ":" + Mid(aList.SaishinChusyutujikan.ToString, 2, 2) + ":" + Mid(aList.SaishinChusyutujikan.ToString, 4, 2)
            Else
                chusyutujikan = Mid(aList.SaishinChusyutujikan.ToString, 1, 2) + ":" + Mid(aList.SaishinChusyutujikan.ToString, 3, 2) + ":" + Mid(aList.SaishinChusyutujikan.ToString, 5, 2)
            End If

            '-----------------------------------------------------------
            '２次改修
            '   時間が取得できなければシステム時間を設定する。
            If aList.SaishinChusyutujikan.ToString = "0" Then
                chusyutujikan = DateTime.Now.ToString("HH:mm:ss")
            End If
            '-----------------------------------------------------------

            '精査するYYYY/MM/DD HH:MM:SS
            xls.SetValue(6, 2, "抽出日時: " + chusyutubi + " " + chusyutujikan)

        End Sub

        ''' <summary>
        ''' Excel出力　シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aBuhinKaiteiList">訂正基本リスト</param>
        ''' <param name="aGousyaList">訂正号車リスト</param>
        ''' <param name="aList">リストコード</param>
        ''' <param name="aEventVo">イベント情報</param>
        ''' <param name="aBaseListVo">ベース車情報リスト</param>
        ''' <remarks></remarks>
        Public Sub setShinchotasuSheetBodyNEW(ByVal xls As ShisakuExcel, ByVal aBuhinKaiteiList As List(Of TShisakuBuhinEditKaiteiVoHelper), _
                                            ByVal aGousyaList As List(Of TShisakuBuhinEditGousyaKaiteiVo), _
                                            ByVal aList As TShisakuListcodeVo, _
                                            ByVal aEventVo As TShisakuEventVo, _
                                            ByVal aBaseListVo As List(Of TShisakuEventBaseVo))

            'タイトル部分の作成'
            Dim maxColumnNum As Integer = setShinchotastuTitleRow(xls, aGousyaList, aBaseListVo)

            'Indexを飛ばすために用意'
            Dim rowIndex As Integer = 0
            Dim Insuc As Integer = aBaseListVo.Count + 2
            Dim gousyaInsu As String = ""
            '同じ行か判断'
            Dim reFlag As Boolean = False
            Dim mergeFlag As Boolean = False
            Dim COL_INS_TOTAL As Integer

            Dim maxRowNumber As Integer = 0
            For index As Integer = 0 To aBuhinKaiteiList.Count - 1
                If Not index = 0 Then
                    If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNoHyoujiJun, aBuhinKaiteiList(index - 1).BuhinNoHyoujiJun) Then
                        If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNo, aBuhinKaiteiList(index - 1).BuhinNo) Then
                            If StringUtil.Equals(aBuhinKaiteiList(index).Flag, aBuhinKaiteiList(index - 1).Flag) Then
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
            Dim dataMatrix(maxRowNumber, maxColumnNum - 1) As String

            Dim unitKbnHash As New Hashtable
            Dim sb As New System.Text.StringBuilder

            For index As Integer = 0 To aBuhinKaiteiList.Count - 1

                If Not index = 0 Then
                    If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNoHyoujiJun, aBuhinKaiteiList(index - 1).BuhinNoHyoujiJun) Then
                        If StringUtil.Equals(aBuhinKaiteiList(index).BuhinNo, aBuhinKaiteiList(index - 1).BuhinNo) Then
                            If StringUtil.Equals(aBuhinKaiteiList(index).Flag, aBuhinKaiteiList(index - 1).Flag) Then
                                rowIndex = rowIndex - 1
                                mergeFlag = True
                            End If
                        End If
                    End If
                End If

                If mergeFlag Then
                    '員数のみ'
                    If aBuhinKaiteiList(index).InsuSuryo < 0 Then
                        dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                    ElseIf aBuhinKaiteiList(index).InsuSuryo = 0 Then
                        dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = ""
                    Else
                        Dim insu As String = dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1)

                        If StringUtil.Equals(insu, "**") Then
                            dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                        Else
                            Dim totalInsu As Integer = 0

                            If StringUtil.IsEmpty(insu) Then
                                If aBuhinKaiteiList(index).InsuSuryo = 0 Then
                                    dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = ""
                                Else
                                    dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = aBuhinKaiteiList(index).InsuSuryo
                                End If
                            Else
                                totalInsu = Integer.Parse(insu)
                                totalInsu = totalInsu + aBuhinKaiteiList(index).InsuSuryo
                                dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = totalInsu
                            End If
                        End If
                    End If
                    mergeFlag = False
                Else
                    Dim blockImpl As TehaichoMenuDao = New TehaichoMenuDaoImpl
                    'フラグ'
                    dataMatrix(rowIndex, COLUMN_FLAG - 1) = GetFlag(aBuhinKaiteiList(index))
                    '車種'
                    dataMatrix(rowIndex, COLUMN_SYASYU - 1) = ""
                    'ブロックNo'
                    dataMatrix(rowIndex, COLUMN_BLOCK_NO - 1) = aBuhinKaiteiList(index).ShisakuBlockNo
                    '工事No'
                    dataMatrix(rowIndex, COLUMN_KOUJI_NO - 1) = ""
                    '行ID'
                    dataMatrix(rowIndex, COLUMN_GYOU_ID - 1) = ""
                    '管理No'
                    '何もしない'
                    '専用マーク'
                    If SenyouCheck(aBuhinKaiteiList(index).BuhinNo, aList.ShisakuSeihinKbn) Then
                        dataMatrix(rowIndex, COLUMN_SENYOU_MARK - 1) = ""
                    Else
                        dataMatrix(rowIndex, COLUMN_SENYOU_MARK - 1) = "*"
                    End If
                    '履歴'
                    dataMatrix(rowIndex, COLUMN_RIREKI - 1) = ""
                    'レベル'
                    dataMatrix(rowIndex, COLUMN_LEVEL - 1) = aBuhinKaiteiList(index).Level.ToString

                    '2012/03/01 ハッシュから取得するように改修
                    'ユニット区分は試作ブロック情報から取得する。
                    With sb
                        .Remove(0, .Length)
                        .AppendLine(aBuhinKaiteiList(index).ShisakuEventCode)
                        .AppendLine(aBuhinKaiteiList(index).ShisakuBukaCode)
                        .AppendLine(aBuhinKaiteiList(index).ShisakuBlockNo)
                        .AppendLine("000")
                    End With
                    If Not unitKbnHash.ContainsKey(sb.ToString) Then
                        Dim wUnitKbnWK As String = blockImpl.FindByShisakuBlockNo(aBuhinKaiteiList(index).ShisakuEventCode, _
                                                                           aBuhinKaiteiList(index).ShisakuBukaCode, _
                                                                           aBuhinKaiteiList(index).ShisakuBlockNo, _
                                                                           "000")
                        unitKbnHash.Add(sb.ToString, wUnitKbnWK)
                    End If

                    'ユニット区分'
                    dataMatrix(rowIndex, COLUMN_UNIT_KBN - 1) = unitKbnHash(sb.ToString)


                    '部品番号'
                    If StringUtil.Equals(Left(aBuhinKaiteiList(index).BuhinNo, 1), "-") Then
                        Dim str As String
                        str = " " + Right(aBuhinKaiteiList(index).BuhinNo, aBuhinKaiteiList(index).BuhinNo.Length - 1)

                        dataMatrix(rowIndex, COLUMN_BUHIN_NO - 1) = str
                    Else
                        dataMatrix(rowIndex, COLUMN_BUHIN_NO - 1) = aBuhinKaiteiList(index).BuhinNo
                    End If

                    '改訂No'
                    dataMatrix(rowIndex, COLUMN_KAITEI_NO - 1) = aBuhinKaiteiList(index).BuhinNoKaiteiNo
                    '部品名称'
                    dataMatrix(rowIndex, COLUMN_BUHIN_NAME - 1) = aBuhinKaiteiList(index).BuhinName
                    '購担'
                    Dim BuhinEdittmp As TShisakuBuhinEditTmpVo = getBuhinEdittmp(aBuhinKaiteiList(index).BuhinNo)
                    dataMatrix(rowIndex, COLUMN_KOUTAN - 1) = BuhinEdittmp.Koutan
                    '取引先コード'
                    'dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_CODE - 1) = aBuhinKaiteiList(index).TorihikisakiCode
                    '取引先コードがNULLなら取引先コードを取得する'
                    If StringUtil.IsEmpty(aBuhinKaiteiList(index).MakerCode) Then
                        '取引先コード'
                        dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_CODE - 1) = BuhinEdittmp.MakerCode
                    Else
                        '取引先コード'
                        dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_CODE - 1) = aBuhinKaiteiList(index).MakerCode
                    End If

                    '号車の員数が空ならそのままいれる'
                    If aBuhinKaiteiList(index).InsuSuryo < 0 Then
                        dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                    ElseIf aBuhinKaiteiList(index).InsuSuryo = 0 Then
                        dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = ""
                    Else
                        Dim insu As String = dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1)

                        If StringUtil.Equals(insu, "**") Then
                            dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                        Else
                            Dim totalInsu As Integer = 0

                            If StringUtil.IsEmpty(insu) Then
                                If aBuhinKaiteiList(index).InsuSuryo = 0 Then
                                    dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = ""
                                Else
                                    dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = aBuhinKaiteiList(index).InsuSuryo
                                End If
                            Else
                                totalInsu = Integer.Parse(insu)
                                totalInsu = totalInsu + aBuhinKaiteiList(index).InsuSuryo
                                dataMatrix(rowIndex, COLUMN_INSU + aBuhinKaiteiList(index).ShisakuGousyaHyoujiJun - 1) = totalInsu
                            End If
                        End If
                    End If

                    Insuc = aBaseListVo.Count + 1
                    If Insuc < 40 Then
                        Insuc = 40
                    ElseIf Insuc = 40 Then
                        '40の場合にはプラス１してみる。
                        Insuc = Insuc + 1
                    End If

                    '納入場所'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    '供給セクション'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aBuhinKaiteiList(index).KyoukuSection
                    '集計コード'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aBuhinKaiteiList(index).ShukeiCode
                    'SIA集計コード'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    'CKD区分'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aBuhinKaiteiList(index).GencyoCkdKbn

                    '質量'
                    '０ではなくブランクを出力するように修正。　2011/02/28　By柳沼
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    'dataMatrix(rowIndex,COLUMN_INSU + EzUtil.Increment(Insuc)-1)= "0"

                    '設通No.'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""


                    '納期
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""

                    'ここから先で妙な現象が発生中'

                    '納入数'
                    'If aBuhinKaiteiList(index).TotalInsuSuryo.ToString < -1 Then
                    '    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = "**"
                    'Else
                    '    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aBuhinKaiteiList(index).TotalInsuSuryo.ToString
                    'End If
                    '合計員数数量を計算する'
                    '納入数'
                    COL_INS_TOTAL = COLUMN_INSU + EzUtil.Increment(Insuc) - 1


                    '手配記号'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = "F"

                    ''2015/06/19　追加
                    Insuc += 3


                    '備考欄'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aBuhinKaiteiList(index).Bikou
                    '訂正備考欄'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    '材料記号'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""

                    '出図予定日'
                    If aBuhinKaiteiList(index).ShutuzuYoteiDate = 0 Then
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    Else
                        Dim shukkayoteibi As String
                        shukkayoteibi = Mid(aBuhinKaiteiList(index).ShutuzuYoteiDate.ToString, 1, 4) + "/" + Mid(aBuhinKaiteiList(index).ShutuzuYoteiDate.ToString, 5, 2) + "/" + Mid(aBuhinKaiteiList(index).ShutuzuYoteiDate.ToString, 7, 2)
                        xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, shukkayoteibi)
                    End If
                End If

                rowIndex = rowIndex + 1

            Next
            '合計員数数量を計算する'
            For rowIndex2 As Integer = 0 To rowIndex - 1
                Dim totalInsuSuryo As Integer = 0
                Dim insuStr As String
                For columnIndex As Integer = 0 To aBaseListVo.Count

                    insuStr = dataMatrix(rowIndex2, COLUMN_INSU + columnIndex - 1)
                    If StringUtil.IsNotEmpty(insuStr) Then

                        If StringUtil.Equals(insuStr, "**") Then
                            totalInsuSuryo += 0
                        Else
                            totalInsuSuryo += 0
                            If Integer.Parse(insuStr) < 0 Then
                            ElseIf Integer.Parse(insuStr) = 0 Then

                            Else
                                totalInsuSuryo += Integer.Parse(insuStr)
                            End If
                        End If
                    Else
                        totalInsuSuryo += 0
                    End If
                Next
                If totalInsuSuryo > 0 Then
                    dataMatrix(rowIndex2, COL_INS_TOTAL) = CStr(totalInsuSuryo)
                Else
                    dataMatrix(rowIndex2, COL_INS_TOTAL) = "**"
                End If
            Next

            xls.CopyRange(0, START_ROW, UBound(dataMatrix, 2) - 1, START_ROW + UBound(dataMatrix), dataMatrix)

            '列の幅を自動調整する'
            xls.AutoFitCol(COLUMN_SYASYU, xls.EndCol())
        End Sub

        ''' <summary>
        ''' タイトル行の作成
        ''' </summary>
        ''' <param name="xls">目的のEXCELファイル</param>
        ''' <param name="aGousyaListVo">手配号車情報</param>
        ''' <remarks></remarks>
        Private Function setShinchotastuTitleRow(ByVal xls As ShisakuExcel, ByVal aGousyaListVo As List(Of TShisakuBuhinEditGousyaKaiteiVo), _
                                            ByVal aBaseListVo As List(Of TShisakuEventBaseVo)) As Integer

            '車種'
            'フラグ'
            xls.SetValue(COLUMN_FLAG, TITLE_ROW, "変更区分")

            xls.SetValue(COLUMN_SYASYU, TITLE_ROW, "車種")
            xls.SetValue(COLUMN_BLOCK_NO, TITLE_ROW, "ﾌﾞﾛｯｸ№")
            xls.SetValue(COLUMN_KOUJI_NO, TITLE_ROW, "工事№")
            xls.SetValue(COLUMN_GYOU_ID, TITLE_ROW, "行ID")
            xls.SetValue(COLUMN_KANRI_NO, TITLE_ROW, "管理№")
            xls.SetValue(COLUMN_SENYOU_MARK, TITLE_ROW, "専用ﾏｰｸ")
            xls.SetValue(COLUMN_RIREKI, TITLE_ROW, "履歴")
            xls.SetValue(COLUMN_LEVEL, TITLE_ROW, "ﾚﾍﾞﾙ")
            xls.SetValue(COLUMN_UNIT_KBN, TITLE_ROW, "ﾕﾆｯﾄ区分")
            xls.SetValue(COLUMN_BUHIN_NO, TITLE_ROW, "部品番号")
            xls.SetValue(COLUMN_KAITEI_NO, TITLE_ROW, "改訂№")
            xls.SetValue(COLUMN_BUHIN_NAME, TITLE_ROW, "部品名称")
            xls.SetValue(COLUMN_KOUTAN, TITLE_ROW, "購担")
            xls.SetValue(COLUMN_TORIHIKISAKI_CODE, TITLE_ROW, "取引先ｺｰﾄﾞ")
            xls.SetValue(COLUMN_DANDORI, TITLE_ROW, "段取り")
            xls.SetValue(COLUMN_KOSU, TITLE_ROW, "工数")
            InsuCount = 0

            '同じ号車はまとめて同じ列に表示する！'
            Dim count As Integer = 0
            For index As Integer = 0 To aBaseListVo.Count - 1
                '最初の号車'
                'ダミー以外を出力する'
                If Not StringUtil.Equals("DUMMY", aBaseListVo(index).ShisakuGousya) Then
                    InsuCount = InsuCount + 1
                    xls.SetValue(COLUMN_INSU + count, TITLE_ROW, "員数" + Right("0" + (InsuCount).ToString, 2))
                    xls.SetValue(COLUMN_INSU + count, INSU_ROW, aBaseListVo(index).ShisakuGousya)
                    xls.SetOrientation(COLUMN_INSU + count, INSU_ROW, COLUMN_INSU + count, INSU_ROW, ShisakuExcel.XlOrientation.xlVertical)
                    count = count + 1
                End If
            Next

            Dim InsuNo As Integer = InsuCount + 1

            'DUMMYを置くために１つ空の列を用意'
            'InsuNo = InsuNo + 1
            'InsuCount = InsuCount + 1
            xls.SetValue(COLUMN_INSU + InsuCount, TITLE_ROW, "員数" + Right("0" + (InsuNo).ToString, 2))
            xls.SetValue(COLUMN_INSU + InsuCount, INSU_ROW, "")
            xls.SetOrientation(COLUMN_INSU + InsuCount, INSU_ROW, COLUMN_INSU + InsuCount, INSU_ROW, ShisakuExcel.XlOrientation.xlVertical)

            'DUMMY列'
            InsuCount = InsuCount + 1
            InsuNo = InsuNo + 1
            xls.SetValue(COLUMN_INSU + InsuCount, TITLE_ROW, "員数" + Right("0" + (InsuNo).ToString, 2))
            xls.SetValue(COLUMN_INSU + InsuCount, INSU_ROW, "DUMMY")
            xls.SetOrientation(COLUMN_INSU + InsuCount, INSU_ROW, COLUMN_INSU + InsuCount, INSU_ROW, ShisakuExcel.XlOrientation.xlVertical)

            If InsuCount < 40 Then
                For dummyIndex As Integer = 0 To 40 - InsuNo
                    InsuNo = InsuNo + 1
                    InsuCount = InsuCount + 1
                    xls.SetValue(COLUMN_INSU + InsuCount, TITLE_ROW, "員数" + Right("0" + (InsuNo).ToString, 2))
                Next
            Else
                '40以上の場合にはプラス１してみる。
                InsuCount = InsuCount + 1
            End If

            'End If

            Dim Insu As Integer = InsuCount

            '納入場所'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "納入場所")
            '供給セクション'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "供給ｾｸｼｮﾝ")
            '集計コード'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "集計ｺｰﾄﾞ")
            '海外集計コード'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "SIA集計ｺｰﾄﾞ")
            'ＣＫＤ区分'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "CKD区分")
            '質量'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "質量")
            '設通No'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "設通№")
            '納期'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "納期")
            '納入数'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "納入数")
            '手配記号'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "手配記号")

            ''2015/06/19 追加
            '下請区分'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "下請区分")
            '事前調整(手配)'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "事前調整(手配)")
            '事前調整(予算)'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "事前調整(予算)")

            '備考欄'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "備考欄")
            '訂正備考欄'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "訂正備考欄")
            '材料記号'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "材料記号")
            '出図予定年月日'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "出図予定年月日")

            ''2015/06/19 追加
            '予算備考欄'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "予算備考欄")

            Return COLUMN_INSU + Insu

        End Function


        ''' <summary>
        ''' シートの設定(行の高さや列の幅等)
        ''' </summary>
        ''' <param name="xls">目的EXCELファイル</param>
        ''' <remarks></remarks>
        Private Sub setShinchotasuSheetSheetColumnWidth(ByVal xls As ShisakuExcel)
            '員数行の高さを自動調整'
            xls.AutoFitRow(TITLE_ROW, TITLE_ROW)
            '列の幅を自動調整'
            xls.AutoFitCol(COLUMN_SYASYU, xls.EndCol())
            'ウィンドウ枠の固定'
            'xls.FreezePanes(COLUMN_UNIT_KBN, TITLE_ROW + 1, True)

            'Ａ列、Ｆ列の幅を狭くします。　2011/02/28　By柳沼
            xls.SetColWidth(COLUMN_FLAG, COLUMN_FLAG, 6)   'Ａ列（フラグ）
            xls.SetColWidth(COLUMN_KANRI_NO, COLUMN_KANRI_NO, 10)   'Ｆ列（管理Ｎｏ）

        End Sub

        Private _HashBuhinEdittmp As New Hashtable
        Private Function getBuhinEdittmp(ByVal buhinNo As String) As TShisakuBuhinEditTmpVo
            If Not _HashBuhinEdittmp.Contains(buhinNo) Then
                Dim sakuseiImpl As TehaichoSakusei.Dao.TehaichoSakuseiDao = New TehaichoSakusei.Dao.TehaichoSakuseiDaoImpl
                Dim BuhinEdittmp As TShisakuBuhinEditTmpVo
                BuhinEdittmp = sakuseiImpl.FindByKoutanTorihikisaki(buhinNo)
                _HashBuhinEdittmp.Add(buhinNo, BuhinEdittmp)
            End If
            Return _HashBuhinEdittmp.Item(buhinNo)
        End Function

        Private _HashSenyouCheck As New Hashtable
        Private Function SenyouCheck(ByVal aBuhinNo As String, ByVal aShisakuSeihinKbn As String) As Boolean
            Dim sb As New System.Text.StringBuilder
            With sb
                .AppendLine(aBuhinNo)
                .AppendLine(aShisakuSeihinKbn)
            End With
            If Not _HashSenyouCheck.Contains(sb.ToString) Then
                Dim sakuseiImpl As TehaichoSakusei.Dao.TehaichoSakuseiDao = New TehaichoSakusei.Dao.TehaichoSakuseiDaoImpl
                _HashSenyouCheck.Add(sb.ToString, sakuseiImpl.FindBySenyouCheck(aBuhinNo, aShisakuSeihinKbn))
            End If
            Return _HashSenyouCheck.Item(sb.ToString)
        End Function

    End Class
End Namespace