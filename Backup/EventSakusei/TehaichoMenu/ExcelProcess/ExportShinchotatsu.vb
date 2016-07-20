Imports ShisakuCommon
Imports EBom.Excel
Imports EBom.Data
Imports EBom.Common
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.TehaichoMenu.Dao
Imports EventSakusei.TehaichoMenu.Impl
Imports EventSakusei.TehaichoMenu.Vo

Namespace TehaichoMenu.Excel
    Public Class ExportShinchotatsu

        Private InsuCount As Integer
        Private maxColumnNum As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="eventCode"></param>
        ''' <param name="listCode"></param>
        ''' <param name="kaiteiNo"></param>
        ''' <param name="sw">出力制御フラグ（0:新調達への転送 1:履歴）</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal eventCode As String, ByVal listCode As String, ByVal kaiteiNo As String, ByVal sw As Integer)
            Dim systemDrive As String = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            Dim fileName As String

            Dim impl As TehaichoMenuDao = New TehaichoMenuDaoImpl
            'リストコード'
            Dim aList As New TShisakuListcodeVo
            aList = impl.FindByListCodeKaiteiNo(eventCode, listCode, kaiteiNo)
            'イベント情報'
            Dim aEventVo As New TShisakuEventVo
            aEventVo = impl.FindByUnitKbn(eventCode)

            Using sfd As New SaveFileDialog()
                sfd.FileName = ShisakuCommon.ShisakuGlobal.ExcelOutPut
                '2012/01/25
                '2012/01/21
                sfd.InitialDirectory = systemDrive
                'fileName = sfd.InitialDirectory + "\" + sfd.FileName
                '--------------------------------------------------
                'EXCEL出力先フォルダチェック
                ShisakuCommon.ShisakuComFunc.ExcelOutPutDirCheck()
                '--------------------------------------------------
                '[Excel出力系 L M]
                If sw = 0 Then
                    fileName = aEventVo.ShisakuKaihatsuFugo + aList.ShisakuEventName + " " + kaiteiNo + "新調達への転送.xls"
                Else
                    fileName = aEventVo.ShisakuKaihatsuFugo + aList.ShisakuEventName + " " + kaiteiNo + "新調達への転送履歴.xls"
                End If
                fileName = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + StringUtil.ReplaceInvalidCharForFileName(fileName)    '2012/02/08 Excel出力ディレクトリ指定対応
            End Using

            '基本リスト'
            Dim aKihonListVo As New List(Of TShisakuTehaiKihonVoHelper)
            '号車リスト'
            Dim aGousyaListVo As New List(Of TShisakuTehaiGousyaVo)
            'ベース車情報'
            Dim aBaseListVo As New List(Of TShisakuEventBaseVo)
            '取得処理を追加する'

            '-------------------------------------------------------------------------------------------
            ' 20150216 手配帳作成時のユニット区分がMの場合、員数０のデータも抽出する。
            aKihonListVo = impl.FindByTehaiKihonKaiteiNo(eventCode, listCode, kaiteiNo, aList.UnitKbn)
            '-------------------------------------------------------------------------------------------

            aGousyaListVo = impl.FindByTehaiGousyaKaiteiNo(eventCode, listCode, kaiteiNo)
            aBaseListVo = impl.FindByBase(eventCode, listCode)
            Dim i As Integer = 0
            For Each Vo As TShisakuEventBaseVo In aBaseListVo
                If Not StringUtil.Equals(Vo.ShisakuGousya, "DUMMY") Then
                    For Each KVo As TShisakuTehaiKihonVoHelper In aKihonListVo
                        If Not StringUtil.Equals(KVo.ShisakuGousya, "DUMMY") Then
                            If KVo.ShisakuGousyaHyoujiJun = Vo.HyojijunNo Then
                                If StringUtil.IsEmpty(KVo.Flag) Then
                                    KVo.ShisakuGousyaHyoujiJun = i
                                    KVo.Flag = "１"
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

            For Each KVo As TShisakuTehaiKihonVoHelper In aKihonListVo
                If StringUtil.Equals(KVo.ShisakuGousya, "DUMMY") Then
                    If StringUtil.IsEmpty(KVo.Flag) Then
                        KVo.ShisakuGousyaHyoujiJun = i
                        KVo.Flag = "１"
                    End If
                End If
            Next


            If Not ShisakuComFunc.IsFileOpen(ShisakuCommon.ShisakuGlobal.ExcelOutPut) Then

                Using xls As New ShisakuExcel(fileName)
                    xls.OpenBook(fileName)
                    xls.ClearWorkBook()
                    xls.SetFont("ＭＳ Ｐゴシック", 11)
                    setShinchotasuSheet(xls, aList, aKihonListVo, aGousyaListVo, aEventVo, aBaseListVo)
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
        ''' Excel出力　新調達シート
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aList">リストコード情報</param>
        ''' <param name="akihonList">訂正基本リスト</param>
        ''' <param name="aGousyaList">訂正号車リスト</param>
        ''' <param name="aEventVo">イベント情報</param>
        ''' <remarks></remarks>
        Private Sub setShinchotasuSheet(ByVal xls As ShisakuExcel, _
                                         ByVal aList As TShisakuListcodeVo, _
                                         ByVal akihonList As List(Of TShisakuTehaiKihonVoHelper), _
                                         ByVal aGousyaList As List(Of TShisakuTehaiGousyaVo), _
                                         ByVal aEventVo As TShisakuEventVo, _
                                         ByVal aBaseListVo As List(Of TShisakuEventBaseVo))
            xls.SetActiveSheet(1)

            SetColumnNo()

            setSheetHeard(xls, aList, aEventVo)

            'setSheetBody(xls, akihonList, aGousyaList, aList, aEventVo, aBaseListVo)
            setSheetBodyNEW(xls, akihonList, aGousyaList, aList, aEventVo, aBaseListVo)

            setSheetColumnWidth(xls)

        End Sub

        ''' <summary>
        ''' Excel出力　新調達シートのHeaderの部分
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
            xls.SetValue(1, 1, "イベント名称 : " + aEventVo.ShisakuKaihatsuFugo + " " + aList.ShisakuEventName)
            '工事指令No'
            xls.SetValue(1, 2, "工事指令No. :" + aList.ShisakuKoujiShireiNo)
            '抽出日時'
            Dim tensoubi As String
            Dim tensoujikan As String
            Dim aDate As New Date


            tensoubi = Mid(aList.ShisakuTensoubi.ToString, 1, 4) + "/" + Mid(aList.ShisakuTensoubi.ToString, 5, 2) + "/" + Mid(aList.ShisakuTensoubi.ToString, 7, 2)
            If Not aList.ShisakuTensoujikan.ToString.Length = 6 Then
                tensoujikan = Mid(aList.ShisakuTensoujikan.ToString, 1, 1) + ":" + Mid(aList.ShisakuTensoujikan.ToString, 2, 2) + ":" + Mid(aList.ShisakuTensoujikan.ToString, 4, 2)
            Else
                tensoujikan = Mid(aList.ShisakuTensoujikan.ToString, 1, 2) + ":" + Mid(aList.ShisakuTensoujikan.ToString, 3, 2) + ":" + Mid(aList.ShisakuTensoujikan.ToString, 5, 2)
            End If

            '精査するYYYY/MM/DD HH:MM:SS
            xls.SetValue(6, 2, "抽出日時: " + tensoubi + " " + tensoujikan)

        End Sub

        ''' <summary>
        ''' Excel出力　シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aKihonList">訂正基本リスト</param>
        ''' <param name="aGousyaList">訂正号車リスト</param>
        ''' <param name="aList">リストコード</param>
        ''' <param name="aEventVo">イベント情報</param>
        ''' <param name="aBaseListVo">ベース車情報リスト</param>
        ''' <remarks></remarks>
        Public Sub setSheetBody(ByVal xls As ShisakuExcel, ByVal aKihonList As List(Of TShisakuTehaiKihonVoHelper), _
                                            ByVal aGousyaList As List(Of TShisakuTehaiGousyaVo), _
                                            ByVal aList As TShisakuListcodeVo, _
                                            ByVal aEventVo As TShisakuEventVo, _
                                            ByVal aBaseListVo As List(Of TShisakuEventBaseVo))

            'タイトル部分の作成'
            setShinchotastuTitleRow(xls, aGousyaList, aBaseListVo)

            'Indexを飛ばすために用意'
            Dim rowIndex As Integer = 0
            Dim Insuc As Integer = aBaseListVo.Count
            Dim gousyaInsu As String = ""
            '同じ行か判断'
            Dim reFlag As Boolean = False

            For index As Integer = 0 To aKihonList.Count - 1

                If Not index = 0 Then
                    If StringUtil.Equals(aKihonList(index).BuhinNo, aKihonList(index - 1).BuhinNo) Then
                        If StringUtil.Equals(aKihonList(index).GyouId, aKihonList(index - 1).GyouId) Then
                            'If Not StringUtil.Equals(aKihonList(index - 1).ShisakuGousya, "DUMMY") Then
                            rowIndex = rowIndex - 1
                            reFlag = True
                            'End If
                        End If
                    End If
                End If

                If reFlag Then
                    reFlag = False
                    'If Not StringUtil.Equals(aKihonList(index).ShisakuGousya, "DUMMY") Then

                    '号車の員数が空ならそのままいれる'
                    gousyaInsu = xls.GetValue(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex)
                    If StringUtil.IsEmpty(gousyaInsu) Then
                        If aKihonList(index).InsuSuryo < 0 Then
                            xls.SetValue(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, "**")
                        ElseIf Not aKihonList(index).InsuSuryo = 0 Then
                            xls.SetValue(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, aKihonList(index).InsuSuryo)
                        End If
                    Else
                        'あれば計算させる'
                        If aKihonList(index).InsuSuryo < 0 Or StringUtil.Equals(gousyaInsu, "**") Then
                            xls.SetValue(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, "**")
                        ElseIf aKihonList(index).InsuSuryo = 0 Then
                            Dim insu As Integer = Integer.Parse(gousyaInsu)
                            insu = insu + aKihonList(index).InsuSuryo
                            xls.SetValue(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, insu)
                        End If
                    End If
                    'End If

                Else

                    'If Not StringUtil.Equals(aKihonList(index).ShisakuGousya, "DUMMY") Then




                    Dim kaihatsuFugo As New TShisakuEventBaseVo
                    Dim sakuseiImpl As TehaichoSakusei.Dao.TehaichoSakuseiDao = New TehaichoSakusei.Dao.TehaichoSakuseiDaoImpl
                    Dim blockImpl As TehaichoMenuDao = New TehaichoMenuDaoImpl

                    kaihatsuFugo = sakuseiImpl.FindByGousyaKaihatsuFugo(aKihonList(index).ShisakuEventCode, aKihonList(index).ShisakuGousyaHyoujiJun)
                    '車種'
                    xls.SetValue(COLUMN_SYASYU, START_ROW + rowIndex, aEventVo.ShisakuKaihatsuFugo)
                    'xls.SetValue(COLUMN_SYASYU, START_ROW + rowIndex, aEventVo.ShisakuKaihatsuFugo)
                    'ブロックNo'
                    xls.SetValue(COLUMN_BLOCK_NO, START_ROW + rowIndex, aKihonList(index).ShisakuBlockNo)
                    '工事No'
                    xls.SetValue(COLUMN_KOUJI_NO, START_ROW + rowIndex, aList.ShisakuKoujiNo)
                    '行ID'
                    xls.SetValue(COLUMN_GYOU_ID, START_ROW + rowIndex, aKihonList(index).GyouId)
                    '管理No'
                    '何もしない'
                    '専用マーク'
                    xls.SetValue(COLUMN_SENYOU_MARK, START_ROW + rowIndex, aKihonList(index).SenyouMark)
                    '履歴'
                    xls.SetValue(COLUMN_RIREKI, START_ROW + rowIndex, aKihonList(index).Rireki)
                    'レベル'
                    xls.SetValue(COLUMN_LEVEL, START_ROW + rowIndex, aKihonList(index).Level.ToString)


                    'ユニット区分は試作ブロック情報から取得する。 2011/02/28　By柳沼
                    Dim wUnitKbn As String = blockImpl.FindByShisakuBlockNo(aKihonList(index).ShisakuEventCode, _
                                                                       aKihonList(index).ShisakuBukaCode, _
                                                                       aKihonList(index).ShisakuBlockNo, _
                                                                       "000")

                    'ユニット区分'
                    xls.SetValue(COLUMN_UNIT_KBN, START_ROW + rowIndex, wUnitKbn)
                    'xls.SetValue(COLUMN_UNIT_KBN, START_ROW + rowIndex, aKihonList(index).UnitKbn)


                    '部品番号'
                    If StringUtil.Equals(Left(aKihonList(index).BuhinNo, 1), "-") Then
                        Dim str As String
                        str = " " + Right(aKihonList(index).BuhinNo, aKihonList(index).BuhinNo.Length - 1)

                        xls.SetValue(COLUMN_BUHIN_NO, START_ROW + rowIndex, str)
                    Else
                        xls.SetValue(COLUMN_BUHIN_NO, START_ROW + rowIndex, aKihonList(index).BuhinNo)
                    End If

                    '改訂No'
                    xls.SetValue(COLUMN_KAITEI_NO, START_ROW + rowIndex, aKihonList(index).BuhinNoKaiteiNo)
                    '取引先名称'
                    xls.SetValue(COLUMN_BUHIN_NAME, START_ROW + rowIndex, aKihonList(index).BuhinName)
                    '購担'
                    xls.SetValue(COLUMN_KOUTAN, START_ROW + rowIndex, aKihonList(index).Koutan)
                    '取引先コード'
                    xls.SetValue(COLUMN_TORIHIKISAKI_CODE, START_ROW + rowIndex, aKihonList(index).TorihikisakiCode)

                    '号車の員数が空ならそのままいれる'
                    gousyaInsu = xls.GetValue(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex)

                    If StringUtil.IsEmpty(gousyaInsu) Then
                        If aKihonList(index).InsuSuryo < 0 Then
                            xls.SetValue(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, "**")
                        Else
                            xls.SetValue(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, aKihonList(index).InsuSuryo)
                        End If
                    Else
                        'あれば計算させる'
                        If aKihonList(index).InsuSuryo < 0 Or StringUtil.Equals(gousyaInsu, "**") Then
                            xls.SetValue(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, "**")
                        Else
                            Dim insu As Integer = Integer.Parse(gousyaInsu)
                            insu = insu + aKihonList(index).InsuSuryo
                            xls.SetValue(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, insu)
                        End If
                    End If

                    Insuc = aBaseListVo.Count
                    If Insuc < 40 Then
                        Insuc = 40
                    Else
                        '40以上の場合にはプラス１してみる。
                        Insuc = Insuc + 1
                    End If

                    '納入場所'
                    xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, aKihonList(index).Nouba)
                    '供給セクション'
                    xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, aKihonList(index).KyoukuSection)
                    '集計コード'
                    xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, aKihonList(index).ShukeiCode)
                    'SIA集計コード'
                    xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, "")
                    'CKD区分'
                    xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, aKihonList(index).GencyoCkdKbn)

                    '質量'
                    '０ではなくブランクを出力するように修正。　2011/02/28　By柳沼
                    xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, "")
                    'xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, "0")

                    '設通No.'
                    xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, aKihonList(index).ShutuzuJisekiStsrDhstba)


                    '納期が０の場合、ブランクを出力するように修正。　　2011/02/28　By柳沼
                    If aKihonList(index).NounyuShijibi.ToString = 0 Then
                        '納期'
                        xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, "")
                    Else
                        '納期'
                        xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, aKihonList(index).NounyuShijibi.ToString)
                    End If

                    'ここから先で妙な現象が発生中'

                    '納入数'
                    If aKihonList(index).TotalInsuSuryo.ToString < -1 Then
                        xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, "**")
                    Else
                        xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, aKihonList(index).TotalInsuSuryo.ToString)
                    End If
                    '手配記号'
                    xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, aKihonList(index).TehaiKigou)
                    '備考欄'
                    xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, aKihonList(index).Bikou)
                    '訂正備考欄'
                    xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, "")
                    '材料記号'
                    If StringUtil.IsEmpty(aKihonList(index).KetugouNo) Then
                        xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, "")
                    Else
                        xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, aKihonList(index).KetugouNo.ToString)
                    End If

                    'xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, "")
                    '出図予定日'
                    If aKihonList(index).ShutuzuYoteiDate = 0 Then
                        xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, "")
                    Else
                        Dim shukkayoteibi As String
                        shukkayoteibi = Mid(aKihonList(index).ShutuzuYoteiDate.ToString, 1, 4) + "/" + Mid(aKihonList(index).ShutuzuYoteiDate.ToString, 5, 2) + "/" + Mid(aKihonList(index).ShutuzuYoteiDate.ToString, 7, 2)
                        xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, shukkayoteibi)
                    End If
                End If


                rowIndex = rowIndex + 1

            Next

            '列の幅を自動調整する'
            xls.AutoFitCol(COLUMN_SYASYU, xls.EndCol())
        End Sub
        ''' <summary>
        ''' Excel出力　シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aKihonList">訂正基本リスト</param>
        ''' <param name="aGousyaList">訂正号車リスト</param>
        ''' <param name="aList">リストコード</param>
        ''' <param name="aEventVo">イベント情報</param>
        ''' <param name="aBaseListVo">ベース車情報リスト</param>
        ''' <remarks></remarks>
        Public Sub setSheetBodyNEW(ByVal xls As ShisakuExcel, ByVal aKihonList As List(Of TShisakuTehaiKihonVoHelper), _
                                            ByVal aGousyaList As List(Of TShisakuTehaiGousyaVo), _
                                            ByVal aList As TShisakuListcodeVo, _
                                            ByVal aEventVo As TShisakuEventVo, _
                                            ByVal aBaseListVo As List(Of TShisakuEventBaseVo))

            'タイトル部分の作成'
            setShinchotastuTitleRow(xls, aGousyaList, aBaseListVo)

            'Indexを飛ばすために用意'
            Dim rowIndex As Integer = 0
            Dim Insuc As Integer = aBaseListVo.Count + 2
            Dim gousyaInsu As String = ""
            '同じ行か判断'
            Dim reFlag As Boolean = False

            Dim maxRowNumber As Integer = 0
            For index As Integer = 0 To aKihonList.Count - 1
                If Not index = 0 Then
                    If StringUtil.Equals(aKihonList(index).BuhinNo, aKihonList(index - 1).BuhinNo) Then
                        If StringUtil.Equals(aKihonList(index).GyouId, aKihonList(index - 1).GyouId) Then
                            '↓↓2014/10/06 酒井 ADD BEGIN
                            '補用品不具合展開
                            If StringUtil.Equals(aKihonList(index).ShisakuBukaCode, aKihonList(index - 1).ShisakuBukaCode) Then
                                If StringUtil.Equals(aKihonList(index).ShisakuBlockNo, aKihonList(index - 1).ShisakuBlockNo) Then
                                Else
                                    maxRowNumber = maxRowNumber + 1
                                End If
                            Else
                                maxRowNumber = maxRowNumber + 1
                            End If
                            '↑↑2014/10/06 酒井 ADD END
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

            For index As Integer = 0 To aKihonList.Count - 1

                If Not index = 0 Then
                    If StringUtil.Equals(aKihonList(index).BuhinNo, aKihonList(index - 1).BuhinNo) Then
                        If StringUtil.Equals(aKihonList(index).GyouId, aKihonList(index - 1).GyouId) Then
                            '↓↓2014/10/06 酒井 ADD BEGIN
                            '補用品不具合展開
                            If StringUtil.Equals(aKihonList(index).ShisakuBukaCode, aKihonList(index - 1).ShisakuBukaCode) Then
                                If StringUtil.Equals(aKihonList(index).ShisakuBlockNo, aKihonList(index - 1).ShisakuBlockNo) Then
                                    'If Not StringUtil.Equals(aKihonList(index - 1).ShisakuGousya, "DUMMY") Then
                                    rowIndex = rowIndex - 1
                                    reFlag = True
                                    'End If
                                End If
                            End If
                            '↑↑2014/10/06 酒井 ADD END
                        End If
                    End If
                End If

                If reFlag Then
                    reFlag = False
                    'If Not StringUtil.Equals(aKihonList(index).ShisakuGousya, "DUMMY") Then

                    '号車の員数が空ならそのままいれる'
                    gousyaInsu = dataMatrix(rowIndex, COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun - 1)
                    If StringUtil.IsEmpty(gousyaInsu) Then
                        If aKihonList(index).InsuSuryo < 0 Then
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                        ElseIf Not aKihonList(index).InsuSuryo = 0 Then
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun - 1) = aKihonList(index).InsuSuryo
                        End If
                    Else
                        'あれば計算させる'
                        If aKihonList(index).InsuSuryo < 0 Or StringUtil.Equals(gousyaInsu, "**") Then
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                        ElseIf aKihonList(index).InsuSuryo = 0 Then
                            Dim insu As Integer = Integer.Parse(gousyaInsu)
                            insu = insu + aKihonList(index).InsuSuryo
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun - 1) = insu
                        End If
                    End If
                    'End If

                Else

                    'If Not StringUtil.Equals(aKihonList(index).ShisakuGousya, "DUMMY") Then




                    'Dim kaihatsuFugo As New TShisakuEventBaseVo
                    Dim sakuseiImpl As TehaichoSakusei.Dao.TehaichoSakuseiDao = New TehaichoSakusei.Dao.TehaichoSakuseiDaoImpl
                    Dim blockImpl As TehaichoMenuDao = New TehaichoMenuDaoImpl

                    'kaihatsuFugo = sakuseiImpl.FindByGousyaKaihatsuFugo(aKihonList(index).ShisakuEventCode, aKihonList(index).ShisakuGousyaHyoujiJun)
                    '車種'
                    dataMatrix(rowIndex, COLUMN_SYASYU - 1) = aEventVo.ShisakuKaihatsuFugo
                    'dataMatrix(rowIndex,COLUMN_SYASYU-1)= aEventVo.ShisakuKaihatsuFugo
                    'ブロックNo'
                    dataMatrix(rowIndex, COLUMN_BLOCK_NO - 1) = aKihonList(index).ShisakuBlockNo
                    '工事No'
                    dataMatrix(rowIndex, COLUMN_KOUJI_NO - 1) = aList.ShisakuKoujiNo
                    '行ID'
                    dataMatrix(rowIndex, COLUMN_GYOU_ID - 1) = aKihonList(index).GyouId
                    '管理No'
                    '何もしない'
                    '専用マーク'
                    dataMatrix(rowIndex, COLUMN_SENYOU_MARK - 1) = aKihonList(index).SenyouMark
                    '履歴'
                    dataMatrix(rowIndex, COLUMN_RIREKI - 1) = aKihonList(index).Rireki
                    'レベル'
                    dataMatrix(rowIndex, COLUMN_LEVEL - 1) = aKihonList(index).Level.ToString

                    '2012/03/01 ハッシュから取得するように改修
                    'ユニット区分は試作ブロック情報から取得する。 2011/02/28　By柳沼
                    If Not unitKbnHash.ContainsKey(aKihonList(index).ShisakuEventCode & "-" & aKihonList(index).ShisakuBukaCode & "-" & aKihonList(index).ShisakuBlockNo & "-000") Then
                        Dim wUnitKbnWK As String = blockImpl.FindByShisakuBlockNo(aKihonList(index).ShisakuEventCode, _
                                                                           aKihonList(index).ShisakuBukaCode, _
                                                                           aKihonList(index).ShisakuBlockNo, _
                                                                           "000")
                        unitKbnHash.Add(aKihonList(index).ShisakuEventCode & "-" & aKihonList(index).ShisakuBukaCode & "-" & aKihonList(index).ShisakuBlockNo & "-000", wUnitKbnWK)
                    End If

                    'ユニット区分'
                    dataMatrix(rowIndex, COLUMN_UNIT_KBN - 1) = unitKbnHash(aKihonList(index).ShisakuEventCode & "-" & aKihonList(index).ShisakuBukaCode & "-" & aKihonList(index).ShisakuBlockNo & "-000")
                    'dataMatrix(rowIndex,COLUMN_UNIT_KBN-1)= aKihonList(index).UnitKbn


                    '部品番号'
                    If StringUtil.Equals(Left(aKihonList(index).BuhinNo, 1), "-") Then
                        Dim str As String
                        str = " " + Right(aKihonList(index).BuhinNo, aKihonList(index).BuhinNo.Length - 1)

                        dataMatrix(rowIndex, COLUMN_BUHIN_NO - 1) = str
                    Else
                        dataMatrix(rowIndex, COLUMN_BUHIN_NO - 1) = aKihonList(index).BuhinNo
                    End If

                    '改訂No'
                    dataMatrix(rowIndex, COLUMN_KAITEI_NO - 1) = aKihonList(index).BuhinNoKaiteiNo
                    '取引先名称'
                    dataMatrix(rowIndex, COLUMN_BUHIN_NAME - 1) = aKihonList(index).BuhinName
                    '購担'
                    dataMatrix(rowIndex, COLUMN_KOUTAN - 1) = aKihonList(index).Koutan
                    '取引先コード'
                    dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_CODE - 1) = aKihonList(index).TorihikisakiCode

                    '号車の員数が空ならそのままいれる'
                    gousyaInsu = dataMatrix(rowIndex, COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun - 1)

                    If StringUtil.IsEmpty(gousyaInsu) Then
                        If aKihonList(index).InsuSuryo < 0 Then
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                        Else
                            '-------------------------------------------------------------------------------------------------------------------
                            '20150216_員数＝０は出力しない。
                            If Not StringUtil.Equals(aKihonList(index).InsuSuryo, 0) Then
                                dataMatrix(rowIndex, COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun - 1) = aKihonList(index).InsuSuryo
                            End If
                            '-------------------------------------------------------------------------------------------------------------------
                        End If
                    Else
                        'あれば計算させる'
                        If aKihonList(index).InsuSuryo < 0 Or StringUtil.Equals(gousyaInsu, "**") Then
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                        Else
                            Dim insu As Integer = Integer.Parse(gousyaInsu)
                            insu = insu + aKihonList(index).InsuSuryo
                            '-------------------------------------------------------------------------------------------------------------------
                            '20150216_員数＝０は出力しない。
                            If Not StringUtil.Equals(aKihonList(index).InsuSuryo, 0) Then
                                dataMatrix(rowIndex, COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun - 1) = insu
                            End If
                            '-------------------------------------------------------------------------------------------------------------------
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
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonList(index).Nouba
                    '供給セクション'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonList(index).KyoukuSection
                    '集計コード'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonList(index).ShukeiCode
                    'SIA集計コード'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    'CKD区分'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonList(index).GencyoCkdKbn

                    '質量'
                    '０ではなくブランクを出力するように修正。　2011/02/28　By柳沼
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    'dataMatrix(rowIndex,COLUMN_INSU + EzUtil.Increment(Insuc)-1)= "0"

                    '設通No.'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonList(index).ShutuzuJisekiStsrDhstba


                    '納期が０の場合、ブランクを出力するように修正。　　2011/02/28　By柳沼
                    If aKihonList(index).NounyuShijibi.ToString = 0 Then
                        '納期'
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    Else
                        '納期'
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonList(index).NounyuShijibi.ToString
                    End If

                    'ここから先で妙な現象が発生中'

                    '納入数'
                    If aKihonList(index).TotalInsuSuryo.ToString < -1 Then
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = "**"
                    Else
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonList(index).TotalInsuSuryo.ToString
                    End If
                    '手配記号'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonList(index).TehaiKigou
                    '備考欄'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonList(index).Bikou
                    '訂正備考欄'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    '材料記号'
                    If StringUtil.IsEmpty(aKihonList(index).KetugouNo) Then
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    Else
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonList(index).KetugouNo.ToString
                    End If

                    'dataMatrix(rowIndex,COLUMN_INSU + EzUtil.Increment(Insuc)-1)= ""
                    '出図予定日'
                    If aKihonList(index).ShutuzuYoteiDate = 0 Then
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    Else
                        Dim shukkayoteibi As String
                        shukkayoteibi = Mid(aKihonList(index).ShutuzuYoteiDate.ToString, 1, 4) + "/" + Mid(aKihonList(index).ShutuzuYoteiDate.ToString, 5, 2) + "/" + Mid(aKihonList(index).ShutuzuYoteiDate.ToString, 7, 2)
                        xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, shukkayoteibi)
                    End If
                End If


                rowIndex = rowIndex + 1

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
        Private Sub setShinchotastuTitleRow(ByVal xls As ShisakuExcel, ByVal aGousyaListVo As List(Of TShisakuTehaiGousyaVo), _
                                            ByVal aBaseListVo As List(Of TShisakuEventBaseVo))

            '車種'
            xls.SetValue(COLUMN_SYASYU, TITLE_ROW, "車種")
            xls.SetValue(COLUMN_BLOCK_NO, TITLE_ROW, "ブロックNo")
            xls.SetValue(COLUMN_KOUJI_NO, TITLE_ROW, "工事No")
            xls.SetValue(COLUMN_GYOU_ID, TITLE_ROW, "行ID")
            xls.SetValue(COLUMN_KANRI_NO, TITLE_ROW, "管理No")
            xls.SetValue(COLUMN_SENYOU_MARK, TITLE_ROW, "専用マーク")
            xls.SetValue(COLUMN_RIREKI, TITLE_ROW, "履歴")
            xls.SetValue(COLUMN_LEVEL, TITLE_ROW, "レベル")
            xls.SetValue(COLUMN_UNIT_KBN, TITLE_ROW, "ユニット区分")
            xls.SetValue(COLUMN_BUHIN_NO, TITLE_ROW, "部品番号")
            xls.SetValue(COLUMN_KAITEI_NO, TITLE_ROW, "改訂No.")
            xls.SetValue(COLUMN_BUHIN_NAME, TITLE_ROW, "部品名称")
            xls.SetValue(COLUMN_KOUTAN, TITLE_ROW, "購担")
            xls.SetValue(COLUMN_TORIHIKISAKI_CODE, TITLE_ROW, "取引先コード")
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

            'If Not aGousyaListVo.Count = 0 Then
            '    '号車数'
            '    For index As Integer = 0 To aGousyaListVo.Count - 1
            '        If index > 0 Then
            '            If StringUtil.Equals(aGousyaListVo(index).ShisakuGousya, "DUMMY") Then
            '                'DUMMYを置くために１つ空の列を用意'
            '                InsuCount = InsuCount + 1
            '                xls.SetValue(COLUMN_INSU + InsuCount, TITLE_ROW, "員数" + Right("0" + (InsuCount).ToString, 2))
            '                xls.SetValue(COLUMN_INSU + InsuCount, INSU_ROW, "")
            '                xls.SetOrientation(COLUMN_INSU + InsuCount, INSU_ROW, COLUMN_INSU + InsuCount, INSU_ROW, ShisakuExcel.XlOrientation.xlVertical)

            '                'DUMMY列'
            '                InsuCount = InsuCount + 1
            '                xls.SetValue(COLUMN_INSU + InsuCount, TITLE_ROW, "員数" + Right("0" + (InsuCount).ToString, 2))
            '                xls.SetValue(COLUMN_INSU + InsuCount, INSU_ROW, aGousyaListVo(index).ShisakuGousya)
            '                xls.SetOrientation(COLUMN_INSU + InsuCount, INSU_ROW, COLUMN_INSU + InsuCount, INSU_ROW, ShisakuExcel.XlOrientation.xlVertical)
            '            ElseIf Not StringUtil.Equals(aGousyaListVo(index).ShisakuGousya) Then
            '                '２番目以降の号車'
            '                InsuCount = InsuCount + 1
            '                xls.SetValue(COLUMN_INSU + InsuCount, INSU_ROW, aGousyaListVo(index).ShisakuGousya)
            '                xls.SetValue(COLUMN_INSU + InsuCount, TITLE_ROW, "員数" + Right("0" + (InsuCount).ToString, 2))
            '                xls.SetOrientation(COLUMN_INSU + InsuCount, INSU_ROW, COLUMN_INSU + InsuCount, INSU_ROW, ShisakuExcel.XlOrientation.xlVertical)
            '            End If
            '        Else
            '            '最初の号車'
            '            InsuCount = InsuCount + 1
            '            xls.SetValue(COLUMN_INSU, TITLE_ROW, "員数" + Right("0" + (InsuCount).ToString, 2))
            '            xls.SetValue(COLUMN_INSU, INSU_ROW, aGousyaListVo(index).ShisakuGousya)
            '            xls.SetOrientation(COLUMN_INSU, INSU_ROW, COLUMN_INSU, INSU_ROW, ShisakuExcel.XlOrientation.xlVertical)
            '        End If
            '    Next

            'Dim dummyinsu As Integer = InsuCount


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
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "供給セクション")
            '集計コード'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "集計コード")
            '海外集計コード'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "海外集計コード")
            'ＣＫＤ区分'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "CKD区分")
            '質量'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "質量")
            '設通No'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "設通No")
            '納期'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "納期")
            '納入数'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "納入数")
            '手配記号'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "手配記号")
            '備考欄'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "備考欄")
            '訂正備考欄'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "訂正備考欄")
            '材料記号'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "材料記号")
            '出図予定年月日'
            xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insu), TITLE_ROW, "出図予定年月日")

            maxColumnNum = COLUMN_INSU + Insu

        End Sub

        ''' <summary>
        ''' シートの設定(行の高さや列の幅等)
        ''' </summary>
        ''' <param name="xls">目的EXCELファイル</param>
        ''' <remarks></remarks>
        Private Sub setSheetColumnWidth(ByVal xls As ShisakuExcel)
            '員数行の高さを自動調整'
            xls.AutoFitRow(TITLE_ROW, TITLE_ROW)
            '列の幅を自動調整'
            xls.AutoFitCol(COLUMN_SYASYU, xls.EndCol())
            'ウィンドウ枠の固定'
            'xls.FreezePanes(COLUMN_UNIT_KBN, TITLE_ROW + 1, True)

            'Ａ列、Ｆ列の幅を狭くします。　2011/02/28　By柳沼
            xls.SetColWidth(COLUMN_SYASYU, COLUMN_SYASYU, 8)   'Ａ列（車種）
            xls.SetColWidth(COLUMN_SENYOU_MARK, COLUMN_SENYOU_MARK, 14)   'Ｆ列（専用マーク）

        End Sub

        ''' <summary>
        ''' 基本情報に紐付く号車情報を検索
        ''' </summary>
        ''' <param name="akihonVo">手配基本情報</param>
        ''' <param name="aGousyaVo">手配号車情報</param>
        ''' <returns>基本情報に紐付いた号車情報であればTrue</returns>
        ''' <remarks></remarks>
        Private Function CheckGousyaKihon(ByVal akihonVo As TShisakuTehaiKihonVo, ByVal aGousyaVo As TShisakuTehaiGousyaVo) As Boolean
            If Not StringUtil.Equals(akihonVo.ShisakuBukaCode, aGousyaVo.ShisakuBukaCode) Then
                Return False
            End If
            If Not StringUtil.Equals(akihonVo.ShisakuBlockNo, aGousyaVo.ShisakuBlockNo) Then
                Return False
            End If
            If Not StringUtil.Equals(akihonVo.BuhinNoHyoujiJun, aGousyaVo.BuhinNoHyoujiJun) Then
                Return False
            End If
            If Not StringUtil.Equals(akihonVo.GyouId, aGousyaVo.GyouId) Then
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


#Region "明細部各列名の設定"
        '' 車種
        Private COLUMN_SYASYU As Integer
        '' ブロックNo
        Private COLUMN_BLOCK_NO As Integer
        '' 工事No
        Private COLUMN_KOUJI_NO As Integer
        '' 行ID.
        Private COLUMN_GYOU_ID As Integer
        '' 管理No
        Private COLUMN_KANRI_NO As Integer
        '' 専用マーク
        Private COLUMN_SENYOU_MARK As Integer
        '' 履歴
        Private COLUMN_RIREKI As Integer
        '' レベル
        Private COLUMN_LEVEL As Integer
        '' ユニット区分
        Private COLUMN_UNIT_KBN As Integer
        '' 部品番号
        Private COLUMN_BUHIN_NO As Integer
        '' 改訂No
        Private COLUMN_KAITEI_NO As Integer
        '' 部品名称
        Private COLUMN_BUHIN_NAME As Integer
        '' 購担
        Private COLUMN_KOUTAN As Integer
        '' 取引先コード
        Private COLUMN_TORIHIKISAKI_CODE As Integer
        '' 段取り
        Private COLUMN_DANDORI As Integer
        '' 工数
        Private COLUMN_KOSU As Integer
        '' 員数
        Private COLUMN_INSU As Integer


#End Region

#Region "各行の番号指定"

        Private TITLE_ROW As Integer = 4

        Private START_ROW As Integer = 5

        Private INSU_ROW As Integer = 3

#End Region

    End Class
End Namespace