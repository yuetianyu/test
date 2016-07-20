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
    Public Class ExportTeiseiExcel

        'BODY部のループ処理に使用'
        Private InsuCount As Integer
        Private KoujiShireiNo As String

        ''↓↓2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
        Private maxColumnNum As Integer
        ''↑↑2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD END

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="eventCode"></param>
        ''' <param name="listCode"></param>
        ''' <param name="kaiteiNo"></param>
        ''' <param name="sw">出力タイプ判定フラグ（0:通常 1:履歴）</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal eventCode As String, ByVal listCode As String, ByVal kaiteiNo As String, ByVal sw As Integer)
            Dim systemDrive As String = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            Dim fileName As String

            Dim impl As TehaichoMenuDao = New TehaichoMenuDaoImpl
            Dim Teiseiimpl As TeiseiTsuchiDao = New TeiseiTsuchiDaoImpl
            'リストコード'
            Dim aList As New TShisakuListcodeVo
            '訂正基本リスト'
            Dim aKihonListVo As New List(Of TShisakuTehaiTeiseiKihonVoHelper)
            '最新の訂正号車リスト'
            Dim aGousyaListVo As New List(Of TShisakuTehaiTeiseiGousyaVo)
            '指定された改訂Noの訂正号車リスト'
            Dim aOldGousyaListVo As New List(Of TShisakuTehaiTeiseiGousyaVo)
            'イベント情報'
            Dim aEventVo As New TShisakuEventVo
            'ベース車情報'
            Dim aBaseListVo As New List(Of TShisakuEventBaseVo)

            'ファイル名に使用するイベント名を取得する為にここで取得
            aEventVo = impl.FindByUnitKbn(eventCode)

            Using sfd As New SaveFileDialog()
                sfd.FileName = ShisakuCommon.ShisakuGlobal.ExcelOutPut
                '2012/01/25
                '2012/01/21
                '--------------------------------------------------
                'EXCEL出力先フォルダチェック
                ShisakuCommon.ShisakuComFunc.ExcelOutPutDirCheck()
                '--------------------------------------------------
                '[Excel出力系 J K]
                sfd.InitialDirectory = systemDrive
                'fileName = sfd.InitialDirectory + "\" + sfd.FileName
                If sw = 0 Then
                    fileName = aEventVo.ShisakuKaihatsuFugo + aEventVo.ShisakuEventName + " " + kaiteiNo + " 訂正通知データ.xls"
                Else
                    fileName = aEventVo.ShisakuKaihatsuFugo + aEventVo.ShisakuEventName + " " + kaiteiNo + " 訂正通知データ履歴.xls"
                End If
                fileName = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + StringUtil.ReplaceInvalidCharForFileName(fileName)	'2012/02/08 Excel出力ディレクトリ指定対応
            End Using


            '取得処理を追加する'
            aKihonListVo = Teiseiimpl.FindByTeiseiKihon(eventCode, listCode, kaiteiNo)
            aGousyaListVo = Teiseiimpl.FindByTeiseiGousya(eventCode, listCode)
            aBaseListVo = impl.FindByBase(eventCode, listCode)
            aList = impl.FindByListCodeKaiteiNo(eventCode, listCode, kaiteiNo)
            KoujiShireiNo = aList.ShisakuKoujiShireiNo

            '表示順をExcel用に'
            Dim i As Integer = 0
            For Each Vo As TShisakuEventBaseVo In aBaseListVo
                If Not StringUtil.Equals(Vo.ShisakuGousya, "DUMMY") Then
                    For Each KVo As TShisakuTehaiTeiseiKihonVoHelper In aKihonListVo
                        If Not StringUtil.Equals(KVo.ShisakuGousya, "DUMMY") Then
                            If KVo.ShisakuGousyaHyoujiJun = Vo.HyojijunNo Then
                                If StringUtil.IsEmpty(KVo.MFlag) Then
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
            For Each KVo As TShisakuTehaiTeiseiKihonVoHelper In aKihonListVo
                If StringUtil.Equals(KVo.ShisakuGousya, "DUMMY") Then
                    If StringUtil.IsEmpty(KVo.MFlag) Then
                        KVo.ShisakuGousyaHyoujiJun = i
                        KVo.MFlag = "１"
                    End If
                End If
            Next

            ' ''↓↓2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
            ''基本リスト'
            'Dim aKihListVo As New List(Of TShisakuTehaiKihonVoHelper)
            ''号車リスト'
            'Dim aGouListVo As New List(Of TShisakuTehaiGousyaVo)

            ''取得処理を追加する'
            'aKihListVo = impl.FindByTehaiKihonKaiteiNo(eventCode, listCode, kaiteiNo)
            'aGouListVo = impl.FindByTehaiGousyaKaiteiNo(eventCode, listCode, kaiteiNo)
            'Dim j As Integer = 0
            'For Each Vo As TShisakuEventBaseVo In aBaseListVo
            '    If Not StringUtil.Equals(Vo.ShisakuGousya, "DUMMY") Then
            '        For Each KVo As TShisakuTehaiKihonVoHelper In aKihListVo
            '            If Not StringUtil.Equals(KVo.ShisakuGousya, "DUMMY") Then
            '                If KVo.ShisakuGousyaHyoujiJun = Vo.HyojijunNo Then
            '                    If StringUtil.IsEmpty(KVo.Flag) Then
            '                        KVo.ShisakuGousyaHyoujiJun = j
            '                        KVo.Flag = "１"
            '                    End If
            '                End If
            '            End If
            '        Next
            '        Vo.HyojijunNo = j
            '        j = j + 1
            '    End If
            'Next
            'j = j + 1

            'For Each KVo As TShisakuTehaiKihonVoHelper In aKihListVo
            '    If StringUtil.Equals(KVo.ShisakuGousya, "DUMMY") Then
            '        If StringUtil.IsEmpty(KVo.Flag) Then
            '            KVo.ShisakuGousyaHyoujiJun = i
            '            KVo.Flag = "１"
            '        End If
            '    End If
            'Next
            ' ''↑↑2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD END

            If Not ShisakuComFunc.IsFileOpen(ShisakuCommon.ShisakuGlobal.ExcelOutPut) Then

                Using xls As New ShisakuExcel(fileName)
                    xls.OpenBook(fileName)
                    xls.ClearWorkBook()
                    xls.SetFont("ＭＳ Ｐゴシック", 11)

                    setShinchotasuSheet(xls, aList, aKihonListVo, aGousyaListVo, aEventVo, aBaseListVo)

                    setTeiseiTsuchiSheet(xls, aList, aKihonListVo, _
                                         aGousyaListVo, _
                                         aEventVo, aBaseListVo, Teiseiimpl, _
                                         kaiteiNo)
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

        ''↓↓2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
        ''' <summary>
        ''' Excel出力　新調達シート
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aList">リストコード情報</param>
        ''' <param name="akihonTeiseiList">訂正基本リスト</param>
        ''' <param name="aGousyaTeiseiList">訂正号車リスト</param>
        ''' <param name="aEventVo">イベント情報</param>
        ''' <remarks></remarks>
        Private Sub setShinchotasuSheet(ByVal xls As ShisakuExcel, _
                                         ByVal aList As TShisakuListcodeVo, _
                                         ByVal akihonTeiseiList As List(Of TShisakuTehaiTeiseiKihonVoHelper), _
                                         ByVal aGousyaTeiseiList As List(Of TShisakuTehaiTeiseiGousyaVo), _
                                         ByVal aEventVo As TShisakuEventVo, _
                                         ByVal aBaseListVo As List(Of TShisakuEventBaseVo))
            xls.SetActiveSheet(1)

            xls.SetSheetName("新調達")

            SetColumnNo1()

            setSheetHeard1(xls, aList, aEventVo)

            'setSheetBody(xls, akihonList, aGousyaList, aList, aEventVo, aBaseListVo)
            setSheetBodyNEW1(xls, akihonTeiseiList, aGousyaTeiseiList, aList, aEventVo, aBaseListVo)

            setSheetColumnWidth1(xls)

        End Sub
        ''↑↑2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD END

        ''↓↓2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
        ''' <summary>
        ''' 各カラム名に数値を設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetColumnNo1()
            Dim column As Integer = 1

            '---------------------------------------
            '20150216_フラグを追加
            COLUMN_FLG = EzUtil.Increment(column)
            '---------------------------------------

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
        ''↑↑2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD END

        ''↓↓2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
        ''' <summary>
        ''' Excel出力　新調達シートのHeaderの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aList">リストコード情報</param>
        ''' <remarks></remarks>
        Public Sub setSheetHeard1(ByVal xls As ShisakuExcel, _
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

            Dim Chusyutsubi As String
            Dim Chusytsujikan As String

            Chusyutsubi = Mid(aList.TeiseiChusyutubi.ToString, 1, 4) + "/" + Mid(aList.TeiseiChusyutubi.ToString, 5, 2) + "/" + Mid(aList.TeiseiChusyutubi.ToString, 7, 2)

            If aList.TeiseiChusyutujikan.ToString.Length = 6 Then
                Chusytsujikan = Mid(aList.TeiseiChusyutujikan.ToString, 1, 2) + ":" + Mid(aList.TeiseiChusyutujikan.ToString, 3, 2) + ":" + Mid(aList.TeiseiChusyutujikan.ToString, 5, 2)
            Else
                Chusytsujikan = "0" + Mid(aList.TeiseiChusyutujikan.ToString, 1, 1) + ":" + Mid(aList.TeiseiChusyutujikan.ToString, 2, 2) + ":" + Mid(aList.TeiseiChusyutujikan.ToString, 4, 2)
            End If

            '精査するYYYY/MM/DD HH:MM:SS
            xls.SetValue(6, 2, "抽出日時: " + Chusyutsubi + " " + Chusytsujikan)

        End Sub
        ''↑↑2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD END

        ''↓↓2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
        ''' <summary>
        ''' Excel出力　シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aKihonTeiseiList">訂正基本リスト</param>
        ''' <param name="aGousyaTeiseiList">訂正号車リスト</param>
        ''' <param name="aList">リストコード</param>
        ''' <param name="aEventVo">イベント情報</param>
        ''' <param name="aBaseListVo">ベース車情報リスト</param>
        ''' <remarks></remarks>
        Public Sub setSheetBodyNEW1(ByVal xls As ShisakuExcel, ByVal aKihonTeiseiList As List(Of TShisakuTehaiTeiseiKihonVoHelper), _
                                            ByVal aGousyaTeiseiList As List(Of TShisakuTehaiTeiseiGousyaVo), _
                                            ByVal aList As TShisakuListcodeVo, _
                                            ByVal aEventVo As TShisakuEventVo, _
                                            ByVal aBaseListVo As List(Of TShisakuEventBaseVo))

            'タイトル部分の作成'
            setShinchotastuTitleRow1(xls, aGousyaTeiseiList, aBaseListVo)

            'Indexを飛ばすために用意'
            Dim rowIndex As Integer = 0
            Dim Insuc As Integer = aBaseListVo.Count + 2
            Dim gousyaInsu As String = ""
            '同じ行か判断'
            Dim reFlag As Boolean = False

            '--------------------------------------------------------------------------------------------------------------------------
            ' 20150216_２次元配列は訂正通知と同様に定義する。
            'Dim maxRowNumber As Integer = 0
            'For index As Integer = 0 To aKihonTeiseiList.Count - 1
            '    If Not index = 0 Then
            '        If StringUtil.Equals(aKihonTeiseiList(index).BuhinNo, aKihonTeiseiList(index - 1).BuhinNo) Then
            '            If StringUtil.Equals(aKihonTeiseiList(index).GyouId, aKihonTeiseiList(index - 1).GyouId) Then
            '                '↓↓2014/10/06 酒井 ADD BEGIN
            '                '補用品不具合展開
            '                If StringUtil.Equals(aKihonTeiseiList(index).ShisakuBukaCode, aKihonTeiseiList(index - 1).ShisakuBukaCode) Then
            '                    If StringUtil.Equals(aKihonTeiseiList(index).ShisakuBlockNo, aKihonTeiseiList(index - 1).ShisakuBlockNo) Then
            '                    Else
            '                        maxRowNumber = maxRowNumber + 1
            '                    End If
            '                Else
            '                    maxRowNumber = maxRowNumber + 1
            '                End If
            '                '↑↑2014/10/06 酒井 ADD END
            '            Else
            '                maxRowNumber = maxRowNumber + 1
            '            End If
            '        Else
            '            maxRowNumber = maxRowNumber + 1
            '        End If
            '    End If
            'Next
            '２次元配列の列数を算出
            Dim maxRowNumber As Integer = 0
            For index As Integer = 0 To aKihonTeiseiList.Count - 1
                If StringUtil.IsEmpty(aKihonTeiseiList(index).Flag) Then
                    Continue For
                End If

                If Not index = 0 Then
                    If StringUtil.Equals(aKihonTeiseiList(index).BuhinNo, aKihonTeiseiList(index - 1).BuhinNo) Then
                        If StringUtil.Equals(aKihonTeiseiList(index).Flag, aKihonTeiseiList(index - 1).Flag) Then
                        Else
                            maxRowNumber = maxRowNumber + 1
                        End If
                    Else
                        maxRowNumber = maxRowNumber + 1
                    End If
                End If
            Next
            '--------------------------------------------------------------------------------------------------------------------------

            '----------------------------------------------------------------
            '20150216_追加データと訂正通知データの区切り行「３行」分追加
            maxRowNumber = maxRowNumber + 3
            '----------------------------------------------------------------

            Dim dataMatrix(maxRowNumber, maxColumnNum - 1) As String

            Dim unitKbnHash As New Hashtable

            For index As Integer = 0 To aKihonTeiseiList.Count - 1

                reFlag = False
                ''フラグが追加(1)または変更後(3)のみ出力する。
                'If Not StringUtil.Equals(aKihonTeiseiList(index).Flag, "1") And _
                '   Not StringUtil.Equals(aKihonTeiseiList(index).Flag, "3") Then
                '    Continue For
                'End If
                'フラグが追加(1)のみ出力する。
                If Not StringUtil.Equals(aKihonTeiseiList(index).Flag, "1") Then
                    Continue For
                End If

                '　20150216_員数が無くても出力する。
                ''納入指示数がある場合出力する。(-1は出力対象)
                'If aKihonTeiseiList(index).TotalInsuSuryo Is Nothing Or aKihonTeiseiList(index).TotalInsuSuryo = 0 Then
                '    Continue For
                'End If

                '--------------------------------------------------------------------------------------------------------------------------
                ' 20150216_訂正通知と同様に定義する。
                'If Not index = 0 Then
                '    If StringUtil.Equals(aKihonTeiseiList(index).BuhinNo, aKihonTeiseiList(index - 1).BuhinNo) Then
                '        If StringUtil.Equals(aKihonTeiseiList(index).GyouId, aKihonTeiseiList(index - 1).GyouId) Then
                '            '↓↓2014/10/06 酒井 ADD BEGIN
                '            '補用品不具合展開
                '            If StringUtil.Equals(aKihonTeiseiList(index).ShisakuBukaCode, aKihonTeiseiList(index - 1).ShisakuBukaCode) Then
                '                If StringUtil.Equals(aKihonTeiseiList(index).ShisakuBlockNo, aKihonTeiseiList(index - 1).ShisakuBlockNo) Then
                '                    'If Not StringUtil.Equals(aKihonList(index - 1).ShisakuGousya, "DUMMY") Then
                '                    rowIndex = rowIndex - 1
                '                    reFlag = True
                '                    'End If
                '                End If
                '            End If
                '            '↑↑2014/10/06 酒井 ADD END
                '        End If
                '    End If
                'End If
                If StringUtil.IsEmpty(aKihonTeiseiList(index).Flag) Then
                    Continue For
                End If
                If Not index = 0 Then
                    If StringUtil.Equals(aKihonTeiseiList(index).BuhinNo, aKihonTeiseiList(index - 1).BuhinNo) Then
                        If StringUtil.Equals(aKihonTeiseiList(index).Flag, aKihonTeiseiList(index - 1).Flag) Then
                            rowIndex = rowIndex - 1
                            reFlag = True
                        End If
                    End If
                End If
                '--------------------------------------------------------------------------------------------------------------------------


                If reFlag Then
                    reFlag = False

                    '号車の員数が空ならそのままいれる'
                    gousyaInsu = dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1)
                    If StringUtil.IsEmpty(gousyaInsu) Then
                        If aKihonTeiseiList(index).InsuSuryo < 0 Then
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                        ElseIf Not aKihonTeiseiList(index).InsuSuryo = 0 Then
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1) = aKihonTeiseiList(index).InsuSuryo
                        End If
                    Else
                        'あれば計算させる'
                        If aKihonTeiseiList(index).InsuSuryo < 0 Or StringUtil.Equals(gousyaInsu, "**") Then
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                        ElseIf aKihonTeiseiList(index).InsuSuryo = 0 Then
                            Dim insu As Integer = Integer.Parse(gousyaInsu)
                            insu = insu + aKihonTeiseiList(index).InsuSuryo
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1) = insu
                        End If
                    End If

                Else

                    Dim sakuseiImpl As TehaichoSakusei.Dao.TehaichoSakuseiDao = New TehaichoSakusei.Dao.TehaichoSakuseiDaoImpl
                    Dim blockImpl As TehaichoMenuDao = New TehaichoMenuDaoImpl

                    '----------------------------------------------------------------------------------
                    ' 20150216_訂正通知同様、先頭列にフラグを出力
                    'フラグ'
                    dataMatrix(rowIndex, COLUMN_FLG - 1) = setFlg(aKihonTeiseiList(index).Flag)
                    '----------------------------------------------------------------------------------

                    '車種'
                    dataMatrix(rowIndex, COLUMN_SYASYU - 1) = aEventVo.ShisakuKaihatsuFugo
                    'ブロックNo'
                    dataMatrix(rowIndex, COLUMN_BLOCK_NO - 1) = aKihonTeiseiList(index).ShisakuBlockNo
                    '工事No'
                    dataMatrix(rowIndex, COLUMN_KOUJI_NO - 1) = aList.ShisakuKoujiNo
                    '行ID'
                    dataMatrix(rowIndex, COLUMN_GYOU_ID - 1) = aKihonTeiseiList(index).GyouId
                    '管理No'
                    '何もしない'
                    '専用マーク'
                    dataMatrix(rowIndex, COLUMN_SENYOU_MARK - 1) = aKihonTeiseiList(index).SenyouMark
                    '履歴'
                    dataMatrix(rowIndex, COLUMN_RIREKI - 1) = aKihonTeiseiList(index).Rireki
                    'レベル'
                    dataMatrix(rowIndex, COLUMN_LEVEL - 1) = aKihonTeiseiList(index).Level.ToString

                    '2012/03/01 ハッシュから取得するように改修
                    'ユニット区分は試作ブロック情報から取得する。 2011/02/28　By柳沼
                    If Not unitKbnHash.ContainsKey(aKihonTeiseiList(index).ShisakuEventCode & "-" & aKihonTeiseiList(index).ShisakuBukaCode & "-" & aKihonTeiseiList(index).ShisakuBlockNo & "-000") Then
                        Dim wUnitKbnWK As String = blockImpl.FindByShisakuBlockNo(aKihonTeiseiList(index).ShisakuEventCode, _
                                                                           aKihonTeiseiList(index).ShisakuBukaCode, _
                                                                           aKihonTeiseiList(index).ShisakuBlockNo, _
                                                                           "000")
                        unitKbnHash.Add(aKihonTeiseiList(index).ShisakuEventCode & "-" & aKihonTeiseiList(index).ShisakuBukaCode & "-" & aKihonTeiseiList(index).ShisakuBlockNo & "-000", wUnitKbnWK)
                    End If

                    'ユニット区分'
                    dataMatrix(rowIndex, COLUMN_UNIT_KBN - 1) = unitKbnHash(aKihonTeiseiList(index).ShisakuEventCode & "-" & aKihonTeiseiList(index).ShisakuBukaCode & "-" & aKihonTeiseiList(index).ShisakuBlockNo & "-000")

                    '部品番号'
                    If StringUtil.Equals(Left(aKihonTeiseiList(index).BuhinNo, 1), "-") Then
                        Dim str As String
                        str = " " + Right(aKihonTeiseiList(index).BuhinNo, aKihonTeiseiList(index).BuhinNo.Length - 1)

                        dataMatrix(rowIndex, COLUMN_BUHIN_NO - 1) = str
                    Else
                        dataMatrix(rowIndex, COLUMN_BUHIN_NO - 1) = aKihonTeiseiList(index).BuhinNo
                    End If

                    '改訂No'
                    dataMatrix(rowIndex, COLUMN_KAITEI_NO - 1) = aKihonTeiseiList(index).BuhinNoKaiteiNo
                    '取引先名称'
                    dataMatrix(rowIndex, COLUMN_BUHIN_NAME - 1) = aKihonTeiseiList(index).BuhinName
                    '購担'
                    dataMatrix(rowIndex, COLUMN_KOUTAN - 1) = aKihonTeiseiList(index).Koutan
                    '取引先コード'
                    dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_CODE - 1) = aKihonTeiseiList(index).TorihikisakiCode

                    '号車の員数が空ならそのままいれる'
                    gousyaInsu = dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1)

                    If StringUtil.IsEmpty(gousyaInsu) Then
                        If aKihonTeiseiList(index).InsuSuryo < 0 Then
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                        Else
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1) = aKihonTeiseiList(index).InsuSuryo
                        End If
                    Else
                        'あれば計算させる'
                        If aKihonTeiseiList(index).InsuSuryo < 0 Or StringUtil.Equals(gousyaInsu, "**") Then
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                        Else
                            Dim insu As Integer = Integer.Parse(gousyaInsu)
                            insu = insu + aKihonTeiseiList(index).InsuSuryo
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1) = insu
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
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).Nouba
                    '供給セクション'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).KyoukuSection
                    '集計コード'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).ShukeiCode
                    'SIA集計コード'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    'CKD区分'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).GencyoCkdKbn

                    '質量'
                    '０ではなくブランクを出力するように修正。　2011/02/28　By柳沼
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""

                    '設通No.'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).StsrDhstba

                    '納期が０の場合、ブランクを出力するように修正。　　2011/02/28　By柳沼
                    If aKihonTeiseiList(index).NounyuShijibi.ToString = 0 Then
                        '納期'
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    Else
                        '納期'
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).NounyuShijibi.ToString
                    End If

                    'ここから先で妙な現象が発生中'

                    '納入数'
                    If aKihonTeiseiList(index).TotalInsuSuryo.ToString < -1 Then
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = "**"
                    Else
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).TotalInsuSuryo.ToString
                    End If
                    '手配記号'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).TehaiKigou
                    '備考欄'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).Bikou
                    '訂正備考欄'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    '材料記号'
                    If StringUtil.IsEmpty(aKihonTeiseiList(index).KetugouNo) Then
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    Else
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).KetugouNo.ToString
                    End If

                    '出図予定日'
                    If aKihonTeiseiList(index).ShutuzuYoteiDate = 0 Then
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    Else
                        Dim shukkayoteibi As String
                        shukkayoteibi = Mid(aKihonTeiseiList(index).ShutuzuYoteiDate.ToString, 1, 4) + "/" + Mid(aKihonTeiseiList(index).ShutuzuYoteiDate.ToString, 5, 2) + "/" + Mid(aKihonTeiseiList(index).ShutuzuYoteiDate.ToString, 7, 2)
                        xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, shukkayoteibi)
                    End If
                End If

                rowIndex = rowIndex + 1

            Next

            '20150216_追加以外の変化点を２行空けて出力する。
            '２行の空白行を追加
            rowIndex = rowIndex + 2

            '区切り線
            dataMatrix(rowIndex, COLUMN_FLG - 1) = "----"

            '追加以外の変化点を出力
            rowIndex = rowIndex + 1

            For index As Integer = 0 To aKihonTeiseiList.Count - 1

                reFlag = False
                'フラグが追加(1)を除いて出力する。
                If StringUtil.Equals(aKihonTeiseiList(index).Flag, "1") Then
                    Continue For
                End If

                '--------------------------------------------------------------------------------------------------------------------------
                ' 20150216_訂正通知と同様に定義する。
                'If Not index = 0 Then
                '    If StringUtil.Equals(aKihonTeiseiList(index).BuhinNo, aKihonTeiseiList(index - 1).BuhinNo) Then
                '        If StringUtil.Equals(aKihonTeiseiList(index).GyouId, aKihonTeiseiList(index - 1).GyouId) Then
                '            '↓↓2014/10/06 酒井 ADD BEGIN
                '            '補用品不具合展開
                '            If StringUtil.Equals(aKihonTeiseiList(index).ShisakuBukaCode, aKihonTeiseiList(index - 1).ShisakuBukaCode) Then
                '                If StringUtil.Equals(aKihonTeiseiList(index).ShisakuBlockNo, aKihonTeiseiList(index - 1).ShisakuBlockNo) Then
                '                    'If Not StringUtil.Equals(aKihonList(index - 1).ShisakuGousya, "DUMMY") Then
                '                    rowIndex = rowIndex - 1
                '                    reFlag = True
                '                    'End If
                '                End If
                '            End If
                '            '↑↑2014/10/06 酒井 ADD END
                '        End If
                '    End If
                'End If
                If StringUtil.IsEmpty(aKihonTeiseiList(index).Flag) Then
                    Continue For
                End If
                If Not index = 0 Then
                    If StringUtil.Equals(aKihonTeiseiList(index).BuhinNo, aKihonTeiseiList(index - 1).BuhinNo) Then
                        If StringUtil.Equals(aKihonTeiseiList(index).Flag, aKihonTeiseiList(index - 1).Flag) Then
                            rowIndex = rowIndex - 1
                            reFlag = True
                        End If
                    End If
                End If
                '--------------------------------------------------------------------------------------------------------------------------

                If reFlag Then
                    reFlag = False

                    '号車の員数が空ならそのままいれる'
                    gousyaInsu = dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1)
                    If StringUtil.IsEmpty(gousyaInsu) Then
                        If aKihonTeiseiList(index).InsuSuryo < 0 Then
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                        ElseIf Not aKihonTeiseiList(index).InsuSuryo = 0 Then
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1) = aKihonTeiseiList(index).InsuSuryo
                        End If
                    Else
                        'あれば計算させる'
                        If aKihonTeiseiList(index).InsuSuryo < 0 Or StringUtil.Equals(gousyaInsu, "**") Then
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                        ElseIf aKihonTeiseiList(index).InsuSuryo = 0 Then
                            Dim insu As Integer = Integer.Parse(gousyaInsu)
                            insu = insu + aKihonTeiseiList(index).InsuSuryo
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1) = insu
                        End If
                    End If

                Else

                    Dim sakuseiImpl As TehaichoSakusei.Dao.TehaichoSakuseiDao = New TehaichoSakusei.Dao.TehaichoSakuseiDaoImpl
                    Dim blockImpl As TehaichoMenuDao = New TehaichoMenuDaoImpl

                    '----------------------------------------------------------------------------------
                    ' 20150216_訂正通知同様、先頭列にフラグを出力
                    'フラグ'
                    dataMatrix(rowIndex, COLUMN_FLG - 1) = setFlg(aKihonTeiseiList(index).Flag)
                    '----------------------------------------------------------------------------------
                    '車種'
                    dataMatrix(rowIndex, COLUMN_SYASYU - 1) = aEventVo.ShisakuKaihatsuFugo
                    'ブロックNo'
                    dataMatrix(rowIndex, COLUMN_BLOCK_NO - 1) = aKihonTeiseiList(index).ShisakuBlockNo
                    '工事No'
                    dataMatrix(rowIndex, COLUMN_KOUJI_NO - 1) = aList.ShisakuKoujiNo
                    '行ID'
                    dataMatrix(rowIndex, COLUMN_GYOU_ID - 1) = aKihonTeiseiList(index).GyouId
                    '管理No'
                    '何もしない'
                    '専用マーク'
                    dataMatrix(rowIndex, COLUMN_SENYOU_MARK - 1) = aKihonTeiseiList(index).SenyouMark
                    '履歴'
                    dataMatrix(rowIndex, COLUMN_RIREKI - 1) = aKihonTeiseiList(index).Rireki
                    'レベル'
                    dataMatrix(rowIndex, COLUMN_LEVEL - 1) = aKihonTeiseiList(index).Level.ToString

                    '2012/03/01 ハッシュから取得するように改修
                    'ユニット区分は試作ブロック情報から取得する。 2011/02/28　By柳沼
                    If Not unitKbnHash.ContainsKey(aKihonTeiseiList(index).ShisakuEventCode & "-" & aKihonTeiseiList(index).ShisakuBukaCode & "-" & aKihonTeiseiList(index).ShisakuBlockNo & "-000") Then
                        Dim wUnitKbnWK As String = blockImpl.FindByShisakuBlockNo(aKihonTeiseiList(index).ShisakuEventCode, _
                                                                           aKihonTeiseiList(index).ShisakuBukaCode, _
                                                                           aKihonTeiseiList(index).ShisakuBlockNo, _
                                                                           "000")
                        unitKbnHash.Add(aKihonTeiseiList(index).ShisakuEventCode & "-" & aKihonTeiseiList(index).ShisakuBukaCode & "-" & aKihonTeiseiList(index).ShisakuBlockNo & "-000", wUnitKbnWK)
                    End If

                    'ユニット区分'
                    dataMatrix(rowIndex, COLUMN_UNIT_KBN - 1) = unitKbnHash(aKihonTeiseiList(index).ShisakuEventCode & "-" & aKihonTeiseiList(index).ShisakuBukaCode & "-" & aKihonTeiseiList(index).ShisakuBlockNo & "-000")

                    '部品番号'
                    If StringUtil.Equals(Left(aKihonTeiseiList(index).BuhinNo, 1), "-") Then
                        Dim str As String
                        str = " " + Right(aKihonTeiseiList(index).BuhinNo, aKihonTeiseiList(index).BuhinNo.Length - 1)

                        dataMatrix(rowIndex, COLUMN_BUHIN_NO - 1) = str
                    Else
                        dataMatrix(rowIndex, COLUMN_BUHIN_NO - 1) = aKihonTeiseiList(index).BuhinNo
                    End If

                    '改訂No'
                    dataMatrix(rowIndex, COLUMN_KAITEI_NO - 1) = aKihonTeiseiList(index).BuhinNoKaiteiNo
                    '取引先名称'
                    dataMatrix(rowIndex, COLUMN_BUHIN_NAME - 1) = aKihonTeiseiList(index).BuhinName
                    '購担'
                    dataMatrix(rowIndex, COLUMN_KOUTAN - 1) = aKihonTeiseiList(index).Koutan
                    '取引先コード'
                    dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_CODE - 1) = aKihonTeiseiList(index).TorihikisakiCode

                    '号車の員数が空ならそのままいれる'
                    gousyaInsu = dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1)

                    If StringUtil.IsEmpty(gousyaInsu) Then
                        If aKihonTeiseiList(index).InsuSuryo < 0 Then
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                        Else
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1) = aKihonTeiseiList(index).InsuSuryo
                        End If
                    Else
                        'あれば計算させる'
                        If aKihonTeiseiList(index).InsuSuryo < 0 Or StringUtil.Equals(gousyaInsu, "**") Then
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                        Else
                            Dim insu As Integer = Integer.Parse(gousyaInsu)
                            insu = insu + aKihonTeiseiList(index).InsuSuryo
                            dataMatrix(rowIndex, COLUMN_INSU + aKihonTeiseiList(index).ShisakuGousyaHyoujiJun - 1) = insu
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
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).Nouba
                    '供給セクション'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).KyoukuSection
                    '集計コード'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).ShukeiCode
                    'SIA集計コード'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    'CKD区分'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).GencyoCkdKbn

                    '質量'
                    '０ではなくブランクを出力するように修正。　2011/02/28　By柳沼
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""

                    '設通No.'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).StsrDhstba

                    '納期が０の場合、ブランクを出力するように修正。　　2011/02/28　By柳沼
                    If aKihonTeiseiList(index).NounyuShijibi.ToString = 0 Then
                        '納期'
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    Else
                        '納期'
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).NounyuShijibi.ToString
                    End If

                    'ここから先で妙な現象が発生中'

                    '納入数'
                    If aKihonTeiseiList(index).TotalInsuSuryo.ToString < -1 Then
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = "**"
                    Else
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).TotalInsuSuryo.ToString
                    End If
                    '手配記号'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).TehaiKigou
                    '備考欄'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).Bikou
                    '訂正備考欄'
                    dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    '材料記号'
                    If StringUtil.IsEmpty(aKihonTeiseiList(index).KetugouNo) Then
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    Else
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = aKihonTeiseiList(index).KetugouNo.ToString
                    End If

                    '出図予定日'
                    If aKihonTeiseiList(index).ShutuzuYoteiDate = 0 Then
                        dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(Insuc) - 1) = ""
                    Else
                        Dim shukkayoteibi As String
                        shukkayoteibi = Mid(aKihonTeiseiList(index).ShutuzuYoteiDate.ToString, 1, 4) + "/" + Mid(aKihonTeiseiList(index).ShutuzuYoteiDate.ToString, 5, 2) + "/" + Mid(aKihonTeiseiList(index).ShutuzuYoteiDate.ToString, 7, 2)
                        xls.SetValue(COLUMN_INSU + EzUtil.Increment(Insuc), START_ROW + rowIndex, shukkayoteibi)
                    End If
                End If

                rowIndex = rowIndex + 1

            Next

            xls.CopyRange(0, START_ROW, UBound(dataMatrix, 2) - 1, START_ROW + UBound(dataMatrix), dataMatrix)

            '列の幅を自動調整する'
            xls.AutoFitCol(COLUMN_SYASYU, xls.EndCol())
        End Sub
        ''↑↑2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD END

        ''↓↓2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
        ''' <summary>
        ''' タイトル行の作成
        ''' </summary>
        ''' <param name="xls">目的のEXCELファイル</param>
        ''' <param name="aGousyaTeiseiListVo">手配号車情報</param>
        ''' <remarks></remarks>
        Private Sub setShinchotastuTitleRow1(ByVal xls As ShisakuExcel, ByVal aGousyaTeiseiListVo As List(Of TShisakuTehaiTeiseiGousyaVo), _
                                            ByVal aBaseListVo As List(Of TShisakuEventBaseVo))

            '--------------------------------------------------------------------
            '20150216_新調達シートの先頭列に訂正通知同様にフラグを出力する。
            xls.SetValue(COLUMN_FLG, TITLE_ROW, "フラグ")
            '--------------------------------------------------------------------
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
        ''↑↑2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD END

        ''↓↓2015/01/13 メタル改訂抽出修正) (TES)劉 CHG BEGIN
        ''' <summary>
        ''' Excel出力　訂正通知シート
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aList">リストコード情報</param>
        ''' <param name="akihonList">訂正基本リスト</param>
        ''' <param name="aGousyaList">訂正号車リスト</param>
        ''' <param name="aEventVo">イベント情報</param>
        ''' <param name="aBaseListVo">ベース車情報</param>
        ''' <param name="teiseiImpl">訂正通知Dao</param>
        ''' <remarks></remarks>
        Private Sub setTeiseiTsuchiSheet(ByVal xls As ShisakuExcel, _
                                         ByVal aList As TShisakuListcodeVo, _
                                         ByVal akihonList As List(Of TShisakuTehaiTeiseiKihonVoHelper), _
                                         ByVal aGousyaList As List(Of TShisakuTehaiTeiseiGousyaVo), _
                                         ByVal aEventVo As TShisakuEventVo, _
                                         ByVal aBaseListVo As List(Of TShisakuEventBaseVo), _
                                         ByVal teiseiImpl As TeiseiTsuchiDao, _
                                         ByVal kaiteiNo As String)
            'xls.SetActiveSheet(1)

            xls.SetActiveSheet(2)

            xls.SetSheetName("訂正通知データ")

            'シートのデフォルトの罫線を消す'
            'xls.SetLine(1, 1, 600, 65535, XlBordersIndex.xlEdgeTop, XlLineStyle.xlLineStyleNone, XlBorderWeight.xlHairline)
            'xls.SetLine(1, 1, 600, 65535, XlBordersIndex.xlEdgeBottom, XlLineStyle.xlLineStyleNone, XlBorderWeight.xlHairline)
            'xls.SetLine(1, 1, 600, 65535, XlBordersIndex.xlEdgeLeft, XlLineStyle.xlLineStyleNone, XlBorderWeight.xlHairline)
            'xls.SetLine(1, 1, 600, 65535, XlBordersIndex.xlEdgeRight, XlLineStyle.xlLineStyleNone, XlBorderWeight.xlHairline)
            'xls.SetLine(1, 1, 600, 65535, XlBordersIndex.xlInsideHorizontal, XlLineStyle.xlLineStyleNone, XlBorderWeight.xlHairline)
            'xls.SetLine(1, 1, 600, 65535, XlBordersIndex.xlInsideVertical, XlLineStyle.xlLineStyleNone, XlBorderWeight.xlHairline)

            SetColumnNo()

            setSheetHeard(xls, aList, aEventVo)

            setSheetBodyNEW(xls, akihonList, aGousyaList, aBaseListVo, teiseiImpl, kaiteiNo)

            setSheetColumnWidth(xls)
        End Sub
        ''↑↑2015/01/13 メタル改訂抽出修正) (TES)劉 CHG END

        ''' <summary>
        ''' Excel出力　新調達シートのHeaderの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aList">リストコード情報</param>
        ''' <param name="aEventVo">イベント情報</param>
        ''' <remarks></remarks>
        Public Sub setSheetHeard(ByVal xls As ShisakuExcel, _
                                  ByVal aList As TShisakuListcodeVo, _
                                  ByVal aEventVo As TShisakuEventVo)

            'TODO 仕様変更に耐えうるようにすべき'
            'ユニット区分用にイベント情報も追加()

            'リストコード'
            xls.SetValue(1, 1, "リストコード")
            xls.SetValue(3, 1, aList.ShisakuListCode)
            'リストコード改訂No'
            xls.SetValue(1, 2, "リストコード改訂No.")
            xls.SetValue(3, 2, aList.ShisakuListCodeKaiteiNo)
            'イベント名称'
            xls.SetValue(1, 3, "イベント名称")
            xls.SetValue(3, 3, aList.ShisakuEventName)
            '工事指令'
            xls.SetValue(1, 4, "工事指令")
            xls.SetValue(3, 4, aList.ShisakuKoujiKbn)
            '抽出日時'
            xls.SetValue(1, 5, "抽出日時")

            Dim Chusyutsubi As String
            Dim Chusytsujikan As String

            Chusyutsubi = Mid(aList.TeiseiChusyutubi.ToString, 1, 4) + "/" + Mid(aList.TeiseiChusyutubi.ToString, 5, 2) + "/" + Mid(aList.TeiseiChusyutubi.ToString, 7, 2)

            If aList.TeiseiChusyutujikan.ToString.Length = 6 Then
                Chusytsujikan = Mid(aList.TeiseiChusyutujikan.ToString, 1, 2) + ":" + Mid(aList.TeiseiChusyutujikan.ToString, 3, 2) + ":" + Mid(aList.TeiseiChusyutujikan.ToString, 5, 2)
            Else
                Chusytsujikan = "0" + Mid(aList.TeiseiChusyutujikan.ToString, 1, 1) + ":" + Mid(aList.TeiseiChusyutujikan.ToString, 2, 2) + ":" + Mid(aList.TeiseiChusyutujikan.ToString, 4, 2)
            End If


            '精査するYYYY/MM/DD HH:MM:SS
            xls.SetValue(3, 5, Chusyutsubi + " " + Chusytsujikan)
            '旧リストコード'
            xls.SetValue(5, 1, "旧リストコード:" + aList.OldListCode)

        End Sub

        ''' <summary>
        ''' Excel出力　シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aKihonList">訂正基本リスト</param>
        ''' <param name="aGosyaList">訂正号車リスト</param>
        ''' <param name="aBaseListVo">ベース車情報</param>
        ''' <param name="TeiseiImpl">訂正通知Dao</param>
        ''' <param name="kaiteiNo">指定の改訂No</param>
        ''' <remarks></remarks>
        Public Sub setSheetBody(ByVal xls As ShisakuExcel, ByVal aKihonList As List(Of TShisakuTehaiTeiseiKihonVoHelper), _
                                            ByVal aGosyaList As List(Of TShisakuTehaiTeiseiGousyaVo), _
                                            ByVal aBaseListVo As List(Of TShisakuEventBaseVo), _
                                            ByVal TeiseiImpl As TeiseiTsuchiDao, _
                                            ByVal kaiteiNo As String)

            'タイトル部分の作成'
            setShinchotastuTitleRow(xls, aBaseListVo)

            Dim rowIndex As Integer = 0
            Dim blockImpl As TeiseiTsuchiDao = New TeiseiTsuchiDaoImpl

            For index As Integer = 0 To aKihonList.Count - 1

                If StringUtil.IsEmpty(aKihonList(index).Flag) Then
                    Continue For
                End If

                If Not index = 0 Then
                    If StringUtil.Equals(aKihonList(index).BuhinNo, aKihonList(index - 1).BuhinNo) Then
                        If StringUtil.Equals(aKihonList(index).Flag, aKihonList(index - 1).Flag) Then
                            rowIndex = rowIndex - 1
                        End If
                    End If
                End If



                'フラグ'
                xls.SetValue(COLUMN_FLG, START_ROW + rowIndex, setFlg(aKihonList(index).Flag))
                '履歴'
                If StringUtil.Equals(aKihonList(index).Rireki, "*") Then
                    xls.SetValue(COLUMN_RIREKI, START_ROW + rowIndex, "＊")
                End If

                'キャンセル実績'
                '何もしない？'
                'ブロックNo'
                xls.SetValue(COLUMN_BLOCK_NO, START_ROW + rowIndex, aKihonList(index).ShisakuBlockNo)
                '行ID'
                xls.SetValue(COLUMN_GYOU_ID, START_ROW + rowIndex, aKihonList(index).GyouId)
                '専用マーク'
                xls.SetValue(COLUMN_SENYOU_MARK, START_ROW + rowIndex, aKihonList(index).SenyouMark)
                'レベル'
                xls.SetValue(COLUMN_LEVEL, START_ROW + rowIndex, aKihonList(index).Level.ToString)
                'ユニット区分'
                xls.SetValue(COLUMN_UNIT_KBN, START_ROW + rowIndex, aKihonList(index).UnitKbn)

                'ユニット区分は試作ブロック情報から取得する。 2011/04/01　By柳沼
                Dim wUnitKbn As String = blockImpl.FindByShisakuBlockNo(aKihonList(index).ShisakuEventCode, _
                                                                   aKihonList(index).ShisakuBukaCode, _
                                                                   aKihonList(index).ShisakuBlockNo, _
                                                                   "000")
                'ユニット区分'
                xls.SetValue(COLUMN_UNIT_KBN, START_ROW + rowIndex, wUnitKbn)

                '集計コード'
                xls.SetValue(COLUMN_SYUKEI_CODE, START_ROW + rowIndex, aKihonList(index).ShukeiCode)
                '手配記号'
                xls.SetValue(COLUMN_TEHAI_KIGOU, START_ROW + rowIndex, aKihonList(index).TehaiKigou)
                '購担'
                xls.SetValue(COLUMN_KOUTAN, START_ROW + rowIndex, aKihonList(index).Koutan)
                '取引先コード'
                xls.SetValue(COLUMN_TORIHIKISAKI_CODE, START_ROW + rowIndex, aKihonList(index).TorihikisakiCode)
                '取引先名称'
                xls.SetValue(COLUMN_TORIHIKISAKI_NAME, START_ROW + rowIndex, aKihonList(index).MakerCode)
                '部品番号'
                xls.SetValue(COLUMN_BUHIN_NO, START_ROW + rowIndex, aKihonList(index).BuhinNo)
                '試作区分'
                xls.SetValue(COLUMN_SHISAKU_KBN, START_ROW + rowIndex, aKihonList(index).BuhinNoKbn)
                '改訂No'
                xls.SetValue(COLUMN_KAITEI_NO, START_ROW + rowIndex, aKihonList(index).BuhinNoKaiteiNo)
                '枝番'
                xls.SetValue(COLUMN_EDABAN, START_ROW + rowIndex, aKihonList(index).EdaBan)
                '部品名称'
                xls.SetValue(COLUMN_BUHIN_NAME, START_ROW + rowIndex, aKihonList(index).BuhinName)
                '納入指示日'
                If aKihonList(index).NounyuShijibi Is Nothing Or aKihonList(index).NounyuShijibi = 0 Then
                    xls.SetValue(COLUMN_NOUNYU_SHIJIBI, START_ROW + rowIndex, "")
                Else
                    xls.SetValue(COLUMN_NOUNYU_SHIJIBI, START_ROW + rowIndex, aKihonList(index).NounyuShijibi.ToString)
                End If

                ''↓↓2015/01/14 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
                '出図実績日'
                xls.SetValue(COLUMN_SHUTUZU_JISEKI_DATE, START_ROW + rowIndex, aKihonList(index).ShutuzuJisekiDate)
                '最終織込設変情報・日付'
                xls.SetValue(COLUMN_SAISYU_SETSUHEN_DATE, START_ROW + rowIndex, aKihonList(index).SaisyuSetsuhenDate)
                ''↑↑2015/01/14 メタル改訂抽出追加_z) (TES)劉 ADD END

                '納入指示数'
                If aKihonList(index).TotalInsuSuryo Is Nothing Or aKihonList(index).TotalInsuSuryo < 0 Then
                    xls.SetValue(COLUMN_NOUNYU_SHIJISU, START_ROW + rowIndex, "0")
                Else
                    xls.SetValue(COLUMN_NOUNYU_SHIJISU, START_ROW + rowIndex, aKihonList(index).TotalInsuSuryo.ToString)
                End If

                '納入場所'
                xls.SetValue(COLUMN_NOUBA, START_ROW + rowIndex, aKihonList(index).Nouba)
                '供給セクション'
                xls.SetValue(COLUMN_KYOKU_SECTION, START_ROW + rowIndex, aKihonList(index).KyoukuSection)
                '再使用不可'
                xls.SetValue(COLUMN_SAISHIYOFUKA, START_ROW + rowIndex, aKihonList(index).Saishiyoufuka)
                '出図予定日'
                If aKihonList(index).ShutuzuYoteiDate Is Nothing Or aKihonList(index).ShutuzuYoteiDate = 0 Then
                    xls.SetValue(COLUMN_SHUTUZU_YOTEI_DATE, START_ROW + rowIndex, "")
                Else
                    Dim shukkayoteibi As String
                    shukkayoteibi = Mid(aKihonList(index).ShutuzuYoteiDate.ToString, 1, 4) + "/" + Mid(aKihonList(index).ShutuzuYoteiDate.ToString, 5, 2) + "/" + Mid(aKihonList(index).ShutuzuYoteiDate.ToString, 7, 2)
                    xls.SetValue(COLUMN_SHUTUZU_YOTEI_DATE, START_ROW + rowIndex, shukkayoteibi)
                End If

                '↓↓2014/09/24 酒井 ADD BEGIN
                xls.SetValue(COLUMN_TSUKURIKATA_SEISAKU, START_ROW + rowIndex, aKihonList(index).TsukurikataSeisaku)
                xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_1, START_ROW + rowIndex, aKihonList(index).TsukurikataKatashiyou1)
                xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_2, START_ROW + rowIndex, aKihonList(index).TsukurikataKatashiyou2)
                xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_3, START_ROW + rowIndex, aKihonList(index).TsukurikataKatashiyou3)
                xls.SetValue(COLUMN_TSUKURIKATA_TIGU, START_ROW + rowIndex, aKihonList(index).TsukurikataTigu)
                If aKihonList(index).TsukurikataNounyu Is Nothing Or aKihonList(index).TsukurikataNounyu = 0 Then
                    xls.SetValue(COLUMN_TSUKURIKATA_NOUNYU, START_ROW + rowIndex, "")
                Else
                    Dim nounyu As String
                    nounyu = Mid(aKihonList(index).TsukurikataNounyu.ToString, 1, 4) + "/" + Mid(aKihonList(index).TsukurikataNounyu.ToString, 5, 2) + "/" + Mid(aKihonList(index).TsukurikataNounyu.ToString, 7, 2)
                    xls.SetValue(COLUMN_TSUKURIKATA_NOUNYU, START_ROW + rowIndex, nounyu)
                End If
                xls.SetValue(COLUMN_TSUKURIKATA_KIBO, START_ROW + rowIndex, aKihonList(index).TsukurikataKibo)
                '↑↑2014/09/24 酒井 ADD END

                '材質'
                '材質・規格１'
                xls.SetValue(COLUMN_ZAISHITU_KIKAKU_1, START_ROW + rowIndex, aKihonList(index).ZaishituKikaku1)
                '材質・規格２'
                xls.SetValue(COLUMN_ZAISHITU_KIKAKU_2, START_ROW + rowIndex, aKihonList(index).ZaishituKikaku2)
                '材質・規格３'
                xls.SetValue(COLUMN_ZAISHITU_KIKAKU_3, START_ROW + rowIndex, aKihonList(index).ZaishituKikaku3)
                '材質・メッキ'
                xls.SetValue(COLUMN_ZAISHITU_MEKKI, START_ROW + rowIndex, aKihonList(index).ZaishituMekki)
                '板厚'
                '板厚数量'
                xls.SetValue(COLUMN_BANKO, START_ROW + rowIndex, aKihonList(index).ShisakuBankoSuryo)
                '板厚数量U'
                xls.SetValue(COLUMN_BANKO_U, START_ROW + rowIndex, aKihonList(index).ShisakuBankoSuryoU)


                ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
                '材料情報・製品長'
                xls.SetValue(COLUMN_MATERIAL_INFO_LENGTH, START_ROW + rowIndex, aKihonList(index).MaterialInfoLength)
                '材料情報・製品幅'
                xls.SetValue(COLUMN_MATERIAL_INFO_WIDTH, START_ROW + rowIndex, aKihonList(index).MaterialInfoWidth)

                ''↓↓2015/01/14 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
                '材料情報・発注対象'
                xls.SetValue(COLUMN_MATERIAL_INFO_ORDER_TARGET, START_ROW + rowIndex, aKihonList(index).MaterialInfoOrderTarget)
                '材料情報・発注対象最終更新年月日'
                xls.SetValue(COLUMN_MATERIAL_INFO_ORDER_TARGET_DATE, START_ROW + rowIndex, aKihonList(index).MaterialInfoOrderTargetDate)
                '材料情報・発注済'
                xls.SetValue(COLUMN_MATERIAL_INFO_ORDER_CHK, START_ROW + rowIndex, aKihonList(index).MaterialInfoOrderChk)
                '材料情報・発注済最終更新年月日'
                xls.SetValue(COLUMN_MATERIAL_INFO_ORDER_CHK_DATE, START_ROW + rowIndex, aKihonList(index).MaterialInfoOrderChkDate)
                ''↑↑2015/01/14 メタル改訂抽出追加_z) (TES)劉 ADD END

                'データ項目・改訂№'
                xls.SetValue(COLUMN_DATA_ITEM_KAITEI_NO, START_ROW + rowIndex, aKihonList(index).DataItemKaiteiNo)
                'データ項目・エリア名'
                xls.SetValue(COLUMN_DATA_ITEM_AREA_NAME, START_ROW + rowIndex, aKihonList(index).DataItemAreaName)
                'データ項目・セット名'
                xls.SetValue(COLUMN_DATA_ITEM_SET_NAME, START_ROW + rowIndex, aKihonList(index).DataItemSetName)
                'データ項目・改訂情報'
                xls.SetValue(COLUMN_DATA_ITEM_KAITEI_INFO, START_ROW + rowIndex, aKihonList(index).DataItemKaiteiInfo)
                ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END

                ''↓↓2015/01/14 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
                'データ項目・データ支給チェック欄'
                xls.SetValue(COLUMN_DATA_ITEM_DATA_PROVISION, START_ROW + rowIndex, aKihonList(index).DataItemDataProvision)
                'データ項目・データ支給チェック欄最終更新年月日'
                xls.SetValue(COLUMN_DATA_ITEM_DATA_PROVISION_DATE, START_ROW + rowIndex, aKihonList(index).DataItemDataProvisionDate)
                ''↑↑2015/01/14 メタル改訂抽出追加_z) (TES)劉 ADD END


                '試作部品費'
                xls.SetValue(COLUMN_SHISAKU_BUHIN_HI, START_ROW + rowIndex, aKihonList(index).ShisakuBuhinnHi)
                '試作型費'
                xls.SetValue(COLUMN_SHISAKU_KATA_HI, START_ROW + rowIndex, aKihonList(index).ShisakuKataHi)
                '備考'
                xls.SetValue(COLUMN_BIKOU, START_ROW + rowIndex, aKihonList(index).Bikou)

                '変更前かそうでないかで必要な号車情報が変わるので'
                'If StringUtil.Equals(aKihonList(index).Flag, "3") Then
                '    '変更前は古いほうの号車情報を使用'
                '    Dim aOldGousyaVo As New TShisakuTehaiTeiseiGousyaVo

                '    For gousyaIndex As Integer = 0 To aBaseListVo.Count - 1
                '        If StringUtil.Equals(aKihonList(index).ShisakuGousya, xls.GetValue(COLUMN_INSU + gousyaIndex, INSU_ROW)) Then
                '            aOldGousyaVo = TeiseiImpl.FindByOldTeiseiGousya(aKihonList(index).ShisakuEventCode, _
                '                                                            aKihonList(index).ShisakuListCode, _
                '                                                            kaiteiNo, _
                '                                                            aKihonList(index).ShisakuBukaCode, _
                '                                                            aKihonList(index).ShisakuBlockNo, _
                '                                                            aKihonList(index).BuhinNoHyoujiJun)


                '            xls.SetValue(COLUMN_INSU + gousyaIndex, START_ROW + rowIndex, aOldGousyaVo.InsuSuryo)
                '        End If
                '    Next
                'Else
                '    '変更前以外は最新の号車情報を使用(２重ループなので時間が掛かる、別の手段を探すべき)'
                '    For gousyaIndex As Integer = 0 To aBaseListVo.Count - 1
                '        '基本情報のINDEX番目に該当する号車情報を探す'
                '        If StringUtil.Equals(aKihonList(index).ShisakuGousya, xls.GetValue(COLUMN_INSU + gousyaIndex, INSU_ROW)) Then
                '            If CheckGousyaKihon(aKihonList(index), aGosyaList(gousyaIndex)) Then
                '                xls.SetValue(COLUMN_INSU + gousyaIndex, START_ROW + rowIndex, aKihonList(index).InsuSuryo)
                '            End If
                '        End If
                '    Next
                'End If

                '員数'
                '20111001 出力項目の配置修正

                If aKihonList(index).InsuSuryo < 0 Then
                    xls.SetValue(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, "**")
                    xls.SetAlliment(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, ShisakuExcel.XlHAlign.xlHAlignCenter)
                Else
                    xls.SetValue(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, aKihonList(index).InsuSuryo)
                    xls.SetAlliment(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, ShisakuExcel.XlHAlign.xlHAlignCenter)
                End If

                Dim column As Integer = InsuCount

                '親品番'
                xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, aKihonList(index).BuhinNoOya)

                'ここから先はAS400'
                'AS400用'
                Dim prpf02Vo As New AsPRPF02Vo

                prpf02Vo = TeiseiImpl.FindByBuhinFile(aKihonList(index).ShisakuEventCode, aKihonList(index).ShisakuListCode, _
                                                      aKihonList(index).ShisakuListCodeKaiteiNo, aKihonList(index).KetugouNo)

                If Not prpf02Vo Is Nothing Then
                    Dim orpf32Vos As New List(Of AsORPF32Vo)
                    Dim orpf57Vos As List(Of AsORPF57Vo)
                    Dim orpf60Vo As AsORPF60Vo = Nothing
                    Dim orpf61vo As AsORPF61Vo = Nothing

                    Dim orpf57Vo As AsORPF57Vo = Nothing
                    Dim orpf57Nokm7VO As AsORPF57Vo = Nothing
                    Dim orpf32Vo As AsORPF32Vo = Nothing
                    Dim orpf32Nokm7Vo As AsORPF32Vo = Nothing

                    orpf32Vos = TeiseiImpl.FindByORPF32(prpf02Vo.Gyoid, prpf02Vo.Kbba, prpf02Vo.Bnba, KoujiShireiNo)

                    For Each Temporpf32Vo As AsORPF32Vo In orpf32Vos
                        If Temporpf32Vo.Nokm = "5" Or Temporpf32Vo.Nokm = "6" Then
                            orpf32Vo = Temporpf32Vo
                        ElseIf Temporpf32Vo.Nokm = "7" Then
                            orpf32Nokm7Vo = Temporpf32Vo
                        End If
                    Next

                    '20110930 If Not ではないか？
                    If orpf32Vo Is Nothing Then
                        orpf57Vos = TeiseiImpl.FindByORPF57(prpf02Vo.OldListCode, prpf02Vo.Kbba, prpf02Vo.Gyoid)
                        If Not orpf57Vo Is Nothing Then
                            orpf61vo = TeiseiImpl.FindByORPF61(orpf57Vo.Grno, orpf57Vo.Srno)
                            '発行No.'
                            xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, orpf57Vo.Edono)
                            '発注年月日'
                            xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, orpf57Vo.Haym)
                            '発注区分'
                            xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, orpf61vo.Tanto)

                            '無い場合もある'
                            If orpf32Vo Is Nothing Then
                                orpf57Vos = TeiseiImpl.FindByORPF57(prpf02Vo.OldListCode, prpf02Vo.Kbba, prpf02Vo.Gyoid)
                                If Not orpf57Vo Is Nothing Then
                                    orpf61vo = TeiseiImpl.FindByORPF61(orpf57Vo.Grno, orpf57Vo.Srno)
                                End If
                            Else
                                orpf60Vo = TeiseiImpl.FindByORPF60(orpf32Vo.Sgisba, orpf32Vo.Kbba, orpf32Vo.Cmba, orpf32Vo.Nokm, orpf32Vo.Haym)
                            End If

                            SetAs400(orpf32Vo, orpf57Vo, orpf60Vo, orpf61vo, orpf32Nokm7Vo, orpf57Nokm7Vo)

                            ''↓↓2015/01/14 メタル改訂抽出修正) (TES)劉 CHG BEGIN
                            ''発行No.'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _HakkoNo)
                            ''発注年月日'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _HacyuDate)
                            ''同期'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _Doki)
                            ''分納'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _Bunno)
                            ''ネック'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _Neck)
                            ''暫定・欠品'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _Zank)
                            ''その他'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _Sonota)
                            ''備考'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _Biko)
                            ''納期回答1'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NokiKaito1)
                            ''納入予定数1'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NonyuYotei1)
                            ''納入区分'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NonyuKbn)
                            ''検収年月日'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _KensyuDate)
                            ''納入累計数'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NonyuTotal)
                            ''引取り検収年月日'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _HikitoriDate)
                            ''引取り累計数'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _HikitoriTotal)
                            ''納入実績マーク'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NonyuJissekiMark)
                            ''取消年月日'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _TorikeshiDate)
                            ''取消数2'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _TorikeshiTotal)
                            ''納期回答2'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NonyuYotei2)
                            ''納入予定数3'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NokiKaito2)
                            ''納期回答3'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NonyuYotei3)
                            ''納入予定数3'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NokiKaito3)
                            ''納期回答4'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NonyuYotei4)
                            ''納入予定数4'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NokiKaito4)
                            ''納期回答5'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NonyuYotei5)
                            ''納入予定数5'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NokiKaito5)
                            ''納期回答6'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NonyuYotei6)
                            ''納入予定数6'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NokiKaito6)
                            ''納期回答7'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NonyuYotei7)
                            ''納入予定数7'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NokiKaito7)
                            ''納期回答8'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NonyuYotei8)
                            ''納入予定数8'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _NokiKaito8)
                            ''処置'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _Syochi)
                            ''理由'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _Riyu)
                            ''対応'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _Taio)
                            ''部署'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _Busho)
                            ''設計担当者'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _SekkeiTantosya)
                            ''TEL'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _Tel)
                            ''暫定品納入日'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _ZanteiHinNonyubi)
                            ''正規扱いor後交換有り'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _SeikiOrKoukan)
                            ''設通No.(最新)'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _StsrNew)
                            ''設通No.(実績)'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _StsrJisseki)
                            ''出図予定日(最新)'                 
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _ShutuzuYoteiDate)
                            ''出図実績日'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _ShutuzuJissekiDate)
                            ''型'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _Kata)
                            ''工法'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _Koho)
                            ''メーカー見積り型費'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _MakerKatahi)
                            ''メーカー見積り部品日'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _MakerBuhinHi)
                            ''工事区分'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _KoujiKbn)
                            ''予算区分'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _YosanKbn)
                            ''手番'
                            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), START_ROW + rowIndex, _Teban)                          
                            ''↑↑2015/01/14 メタル改訂抽出修正) (TES)劉 CHG END
                            '備考２'
                            '購入単価'
                            '決定単価(新調達)'
                            '支給単価(新調達)'
                            '決定単価(経理)'
                            '支給単価(経理)'
                            '手番'
                            '日付1'
                            '日付2'
                            '備考1'
                            '備考2'
                            '備考10'
                        End If

                    End If
                End If

                'Continueで増えないようにする'
                rowIndex = rowIndex + 1
            Next
        End Sub

        ''' <summary>
        ''' Excel出力　シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="aKihonList">訂正基本リスト</param>
        ''' <param name="aGosyaList">訂正号車リスト</param>
        ''' <param name="aBaseListVo">ベース車情報</param>
        ''' <param name="TeiseiImpl">訂正通知Dao</param>
        ''' <param name="kaiteiNo">指定の改訂No</param>
        ''' <remarks></remarks>
        Public Sub setSheetBodyNEW(ByVal xls As ShisakuExcel, ByVal aKihonList As List(Of TShisakuTehaiTeiseiKihonVoHelper), _
                                            ByVal aGosyaList As List(Of TShisakuTehaiTeiseiGousyaVo), _
                                            ByVal aBaseListVo As List(Of TShisakuEventBaseVo), _
                                            ByVal TeiseiImpl As TeiseiTsuchiDao, _
                                            ByVal kaiteiNo As String)

            'タイトル部分の作成'
            setShinchotastuTitleRow(xls, aBaseListVo)

            Dim rowIndex As Integer = 0
            Dim blockImpl As TeiseiTsuchiDao = New TeiseiTsuchiDaoImpl

            '２次元配列の列数を算出
            Dim maxRowNumber As Integer = 0
            For index As Integer = 0 To aKihonList.Count - 1
                If StringUtil.IsEmpty(aKihonList(index).Flag) Then
                    Continue For
                End If

                If Not index = 0 Then
                    If StringUtil.Equals(aKihonList(index).BuhinNo, aKihonList(index - 1).BuhinNo) Then
                        If StringUtil.Equals(aKihonList(index).Flag, aKihonList(index - 1).Flag) Then
                        Else
                            maxRowNumber = maxRowNumber + 1
                        End If
                    Else
                        maxRowNumber = maxRowNumber + 1
                    End If
                End If
            Next
            Dim dataMatrix(maxRowNumber, 1000) As String


            Dim flag As Boolean

            For index As Integer = 0 To aKihonList.Count - 1
                flag = False
                If StringUtil.IsEmpty(aKihonList(index).Flag) Then
                    Continue For
                End If

                If Not index = 0 Then
                    If StringUtil.Equals(aKihonList(index).BuhinNo, aKihonList(index - 1).BuhinNo) Then
                        If StringUtil.Equals(aKihonList(index).Flag, aKihonList(index - 1).Flag) Then
                            rowIndex = rowIndex - 1
                            flag = True
                        End If
                    End If
                End If

                If flag Then
                    If aKihonList(index).InsuSuryo < 0 Then
                        dataMatrix(rowIndex, COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                        xls.SetAlliment(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, ShisakuExcel.XlHAlign.xlHAlignCenter)
                    ElseIf aKihonList(index).InsuSuryo > 0 Then
                        dataMatrix(rowIndex, COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun - 1) = aKihonList(index).InsuSuryo
                        xls.SetAlliment(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, ShisakuExcel.XlHAlign.xlHAlignCenter)
                    End If
                    rowIndex = rowIndex + 1
                    Continue For
                End If



                'フラグ'
                dataMatrix(rowIndex, COLUMN_FLG - 1) = setFlg(aKihonList(index).Flag)
                '履歴'
                If StringUtil.Equals(aKihonList(index).Rireki, "*") Then
                    dataMatrix(rowIndex, COLUMN_RIREKI - 1) = "＊"
                End If

                'キャンセル実績'
                '何もしない？'
                'ブロックNo'
                dataMatrix(rowIndex, COLUMN_BLOCK_NO - 1) = aKihonList(index).ShisakuBlockNo
                '行ID'
                dataMatrix(rowIndex, COLUMN_GYOU_ID - 1) = aKihonList(index).GyouId
                '専用マーク'
                dataMatrix(rowIndex, COLUMN_SENYOU_MARK - 1) = aKihonList(index).SenyouMark
                'レベル'
                dataMatrix(rowIndex, COLUMN_LEVEL - 1) = aKihonList(index).Level.ToString
                'ユニット区分'
                dataMatrix(rowIndex, COLUMN_UNIT_KBN - 1) = aKihonList(index).UnitKbn

                'ユニット区分は試作ブロック情報から取得する。 2011/04/01　By柳沼
                Dim wUnitKbn As String = blockImpl.FindByShisakuBlockNo(aKihonList(index).ShisakuEventCode, _
                                                                   aKihonList(index).ShisakuBukaCode, _
                                                                   aKihonList(index).ShisakuBlockNo, _
                                                                   "000")
                'ユニット区分'
                dataMatrix(rowIndex, COLUMN_UNIT_KBN - 1) = wUnitKbn

                '集計コード'
                dataMatrix(rowIndex, COLUMN_SYUKEI_CODE - 1) = aKihonList(index).ShukeiCode
                '手配記号'
                dataMatrix(rowIndex, COLUMN_TEHAI_KIGOU - 1) = aKihonList(index).TehaiKigou
                '購担'
                dataMatrix(rowIndex, COLUMN_KOUTAN - 1) = aKihonList(index).Koutan
                '取引先コード'
                dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_CODE - 1) = aKihonList(index).TorihikisakiCode
                '取引先名称'
                dataMatrix(rowIndex, COLUMN_TORIHIKISAKI_NAME - 1) = aKihonList(index).MakerCode
                '部品番号'
                dataMatrix(rowIndex, COLUMN_BUHIN_NO - 1) = aKihonList(index).BuhinNo
                '試作区分'
                dataMatrix(rowIndex, COLUMN_SHISAKU_KBN - 1) = aKihonList(index).BuhinNoKbn
                '改訂No'
                dataMatrix(rowIndex, COLUMN_KAITEI_NO - 1) = aKihonList(index).BuhinNoKaiteiNo
                '枝番'
                dataMatrix(rowIndex, COLUMN_EDABAN - 1) = aKihonList(index).EdaBan
                '部品名称'
                dataMatrix(rowIndex, COLUMN_BUHIN_NAME - 1) = aKihonList(index).BuhinName
                '納入指示日'
                If aKihonList(index).NounyuShijibi Is Nothing Or aKihonList(index).NounyuShijibi = 0 Then
                    dataMatrix(rowIndex, COLUMN_NOUNYU_SHIJIBI - 1) = ""
                Else
                    dataMatrix(rowIndex, COLUMN_NOUNYU_SHIJIBI - 1) = aKihonList(index).NounyuShijibi.ToString
                End If
                '出図予定日'
                If aKihonList(index).ShutuzuYoteiDate Is Nothing Or aKihonList(index).ShutuzuYoteiDate = 0 Then
                    dataMatrix(rowIndex, COLUMN_SHUTUZU_YOTEI_DATE - 1) = ""
                Else
                    Dim shukkayoteibi As String
                    shukkayoteibi = Mid(aKihonList(index).ShutuzuYoteiDate.ToString, 1, 4) + "/" + Mid(aKihonList(index).ShutuzuYoteiDate.ToString, 5, 2) + "/" + Mid(aKihonList(index).ShutuzuYoteiDate.ToString, 7, 2)
                    dataMatrix(rowIndex, COLUMN_SHUTUZU_YOTEI_DATE - 1) = shukkayoteibi
                End If

                ''↓↓2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
                '出図実績日'
                dataMatrix(rowIndex, COLUMN_SHUTUZU_JISEKI_DATE - 1) = aKihonList(index).ShutuzuJisekiDate

                dataMatrix(rowIndex, COLUMN_SHUTUZU_JISEKI_KAITEI_NO - 1) = aKihonList(index).ShutuzuJisekiKaiteiNo
                dataMatrix(rowIndex, COLUMN_SHUTUZU_JISEKI_STSR_DHSTBA - 1) = aKihonList(index).ShutuzuJisekiStsrDhstba

                '最終織込設変情報・日付'
                dataMatrix(rowIndex, COLUMN_SAISYU_SETSUHEN_DATE - 1) = aKihonList(index).SaisyuSetsuhenDate

                dataMatrix(rowIndex, COLUMN_SAISYU_SETSUHEN_KAITEI_NO - 1) = aKihonList(index).SaisyuSetsuhenKaiteiNo
                dataMatrix(rowIndex, COLUMN_SAISYU_SETSUHEN_STSR_DHSTBA - 1) = aKihonList(index).StsrDhstba

                ''↑↑2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD END

                '納入指示数'
                If aKihonList(index).TotalInsuSuryo Is Nothing Or aKihonList(index).TotalInsuSuryo < 0 Then
                    dataMatrix(rowIndex, COLUMN_NOUNYU_SHIJISU - 1) = "0"
                Else
                    dataMatrix(rowIndex, COLUMN_NOUNYU_SHIJISU - 1) = aKihonList(index).TotalInsuSuryo.ToString
                End If

                '納入場所'
                dataMatrix(rowIndex, COLUMN_NOUBA - 1) = aKihonList(index).Nouba
                '供給セクション'
                dataMatrix(rowIndex, COLUMN_KYOKU_SECTION - 1) = aKihonList(index).KyoukuSection
                '再使用不可'
                dataMatrix(rowIndex, COLUMN_SAISHIYOFUKA - 1) = aKihonList(index).Saishiyoufuka
                '↓↓2014/09/24 酒井 ADD BEGIN
                dataMatrix(rowIndex, COLUMN_TSUKURIKATA_SEISAKU - 1) = aKihonList(index).TsukurikataSeisaku
                dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU_1 - 1) = aKihonList(index).TsukurikataKatashiyou1
                dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU_2 - 1) = aKihonList(index).TsukurikataKatashiyou2
                dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KATASHIYOU_3 - 1) = aKihonList(index).TsukurikataKatashiyou3
                dataMatrix(rowIndex, COLUMN_TSUKURIKATA_TIGU - 1) = aKihonList(index).TsukurikataTigu
                If aKihonList(index).TsukurikataNounyu Is Nothing Or aKihonList(index).TsukurikataNounyu = 0 Then
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_NOUNYU - 1) = ""
                Else
                    Dim nounyu As String
                    nounyu = Mid(aKihonList(index).TsukurikataNounyu.ToString, 1, 4) + "/" + Mid(aKihonList(index).TsukurikataNounyu.ToString, 5, 2) + "/" + Mid(aKihonList(index).TsukurikataNounyu.ToString, 7, 2)
                    dataMatrix(rowIndex, COLUMN_TSUKURIKATA_NOUNYU - 1) = nounyu
                End If
                dataMatrix(rowIndex, COLUMN_TSUKURIKATA_KIBO - 1) = aKihonList(index).TsukurikataKibo
                '↑↑2014/09/24 酒井 ADD END
                '材質'
                '材質・規格１'
                dataMatrix(rowIndex, COLUMN_ZAISHITU_KIKAKU_1 - 1) = aKihonList(index).ZaishituKikaku1
                '材質・規格２'
                dataMatrix(rowIndex, COLUMN_ZAISHITU_KIKAKU_2 - 1) = aKihonList(index).ZaishituKikaku2
                '材質・規格３'
                dataMatrix(rowIndex, COLUMN_ZAISHITU_KIKAKU_3 - 1) = aKihonList(index).ZaishituKikaku3
                '材質・メッキ'
                dataMatrix(rowIndex, COLUMN_ZAISHITU_MEKKI - 1) = aKihonList(index).ZaishituMekki
                '板厚'
                '板厚数量'
                dataMatrix(rowIndex, COLUMN_BANKO - 1) = aKihonList(index).ShisakuBankoSuryo
                '板厚数量U'
                dataMatrix(rowIndex, COLUMN_BANKO_U - 1) = aKihonList(index).ShisakuBankoSuryoU


                ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
                '材料情報・製品長'
                dataMatrix(rowIndex, COLUMN_MATERIAL_INFO_LENGTH - 1) = aKihonList(index).MaterialInfoLength
                '材料情報・製品幅'
                dataMatrix(rowIndex, COLUMN_MATERIAL_INFO_WIDTH - 1) = aKihonList(index).MaterialInfoWidth

                dataMatrix(rowIndex, COLUMN_ZAIRYO_SUNPO_X - 1) = aKihonList(index).ZairyoSunpoX
                dataMatrix(rowIndex, COLUMN_ZAIRYO_SUNPO_Y - 1) = aKihonList(index).ZairyoSunpoY
                dataMatrix(rowIndex, COLUMN_ZAIRYO_SUNPO_Z - 1) = aKihonList(index).ZairyoSunpoZ
                dataMatrix(rowIndex, COLUMN_ZAIRYO_SUNPO_XY - 1) = aKihonList(index).ZairyoSunpoXy
                dataMatrix(rowIndex, COLUMN_ZAIRYO_SUNPO_XZ - 1) = aKihonList(index).ZairyoSunpoXz
                dataMatrix(rowIndex, COLUMN_ZAIRYO_SUNPO_YZ - 1) = aKihonList(index).ZairyoSunpoYz


                ''↓↓2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
                '材料情報・発注対象'
                dataMatrix(rowIndex, COLUMN_MATERIAL_INFO_ORDER_TARGET - 1) = aKihonList(index).MaterialInfoOrderTarget
                '材料情報・発注対象最終更新年月日'
                dataMatrix(rowIndex, COLUMN_MATERIAL_INFO_ORDER_TARGET_DATE - 1) = aKihonList(index).MaterialInfoOrderTargetDate
                '材料情報・発注済'
                dataMatrix(rowIndex, COLUMN_MATERIAL_INFO_ORDER_CHK - 1) = aKihonList(index).MaterialInfoOrderChk
                '材料情報・発注済最終更新年月日'
                dataMatrix(rowIndex, COLUMN_MATERIAL_INFO_ORDER_CHK_DATE - 1) = aKihonList(index).MaterialInfoOrderChkDate
                ''↑↑2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD END

                'データ項目・改訂№'
                dataMatrix(rowIndex, COLUMN_DATA_ITEM_KAITEI_NO - 1) = aKihonList(index).DataItemKaiteiNo
                'データ項目・エリア名'
                dataMatrix(rowIndex, COLUMN_DATA_ITEM_AREA_NAME - 1) = aKihonList(index).DataItemAreaName
                'データ項目・セット名'
                dataMatrix(rowIndex, COLUMN_DATA_ITEM_SET_NAME - 1) = aKihonList(index).DataItemSetName
                'データ項目・改訂情報'
                dataMatrix(rowIndex, COLUMN_DATA_ITEM_KAITEI_INFO - 1) = aKihonList(index).DataItemKaiteiInfo
                ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END

                ''↓↓2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
                'データ項目・データ支給チェック欄'
                dataMatrix(rowIndex, COLUMN_DATA_ITEM_DATA_PROVISION - 1) = aKihonList(index).DataItemDataProvision
                'データ項目・データ支給チェック欄最終更新年月日'
                dataMatrix(rowIndex, COLUMN_DATA_ITEM_DATA_PROVISION_DATE - 1) = aKihonList(index).DataItemDataProvisionDate
                ''↑↑2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD END

                '試作部品費'
                dataMatrix(rowIndex, COLUMN_SHISAKU_BUHIN_HI - 1) = aKihonList(index).ShisakuBuhinnHi
                '試作型費'
                dataMatrix(rowIndex, COLUMN_SHISAKU_KATA_HI - 1) = aKihonList(index).ShisakuKataHi
                '備考'
                dataMatrix(rowIndex, COLUMN_BIKOU - 1) = aKihonList(index).Bikou

                '変更前かそうでないかで必要な号車情報が変わるので'
                'If StringUtil.Equals(aKihonList(index).Flag, "3") Then
                '    '変更前は古いほうの号車情報を使用'
                '    Dim aOldGousyaVo As New TShisakuTehaiTeiseiGousyaVo

                '    For gousyaIndex As Integer = 0 To aBaseListVo.Count - 1
                '        If StringUtil.Equals(aKihonList(index).ShisakuGousya, xls.GetValue(COLUMN_INSU + gousyaIndex, INSU_ROW)) Then
                '            aOldGousyaVo = TeiseiImpl.FindByOldTeiseiGousya(aKihonList(index).ShisakuEventCode, _
                '                                                            aKihonList(index).ShisakuListCode, _
                '                                                            kaiteiNo, _
                '                                                            aKihonList(index).ShisakuBukaCode, _
                '                                                            aKihonList(index).ShisakuBlockNo, _
                '                                                            aKihonList(index).BuhinNoHyoujiJun)


                '            xls.SetValue(COLUMN_INSU + gousyaIndex, START_ROW + rowIndex, aOldGousyaVo.InsuSuryo)
                '        End If
                '    Next
                'Else
                '    '変更前以外は最新の号車情報を使用(２重ループなので時間が掛かる、別の手段を探すべき)'
                '    For gousyaIndex As Integer = 0 To aBaseListVo.Count - 1
                '        '基本情報のINDEX番目に該当する号車情報を探す'
                '        If StringUtil.Equals(aKihonList(index).ShisakuGousya, xls.GetValue(COLUMN_INSU + gousyaIndex, INSU_ROW)) Then
                '            If CheckGousyaKihon(aKihonList(index), aGosyaList(gousyaIndex)) Then
                '                xls.SetValue(COLUMN_INSU + gousyaIndex, START_ROW + rowIndex, aKihonList(index).InsuSuryo)
                '            End If
                '        End If
                '    Next
                'End If

                '員数'
                '20111001 出力項目の配置修正
                If aKihonList(index).InsuSuryo < 0 Then
                    dataMatrix(rowIndex, COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun - 1) = "**"
                    xls.SetAlliment(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, ShisakuExcel.XlHAlign.xlHAlignCenter)
                ElseIf aKihonList(index).InsuSuryo > 0 Then
                    dataMatrix(rowIndex, COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun - 1) = aKihonList(index).InsuSuryo
                    xls.SetAlliment(COLUMN_INSU + aKihonList(index).ShisakuGousyaHyoujiJun, START_ROW + rowIndex, ShisakuExcel.XlHAlign.xlHAlignCenter)
                End If

                Dim column As Integer = InsuCount

                '親品番'
                'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = aKihonList(index).BuhinNoOya

                ''ここから先が結構遅いのでは？？？
                'ここから先はAS400'
                'AS400用'
                If Not StringUtil.IsEmpty(aKihonList(index).KetugouNo) Then
                    Dim prpf02Vo As New AsPRPF02Vo

                    prpf02Vo = TeiseiImpl.FindByBuhinFile(aKihonList(index).ShisakuEventCode, aKihonList(index).ShisakuListCode, _
                                                          aKihonList(index).ShisakuListCodeKaiteiNo, aKihonList(index).KetugouNo)

                    If Not prpf02Vo Is Nothing Then
                        Dim orpf32Vos As New List(Of AsORPF32Vo)
                        Dim orpf57Vos As New List(Of AsORPF57Vo)
                        Dim orpf60Vo As AsORPF60Vo = Nothing
                        Dim orpf61vo As AsORPF61Vo = Nothing
                        Dim orpf57Vo As AsORPF57Vo = Nothing
                        Dim orpf57Nokm7VO As AsORPF57Vo = Nothing
                        Dim orpf32Vo As AsORPF32Vo = Nothing
                        Dim orpf32Nokm7Vo As AsORPF32Vo = Nothing

                        orpf32Vos = TeiseiImpl.FindByORPF32(prpf02Vo.Koba, prpf02Vo.Gyoid, prpf02Vo.Bnba, KoujiShireiNo)

                        For Each Temporpf32Vo As AsORPF32Vo In orpf32Vos
                            If Temporpf32Vo.Nokm = "5" Or Temporpf32Vo.Nokm = "6" Then
                                orpf32Vo = Temporpf32Vo
                            ElseIf Temporpf32Vo.Nokm = "7" Then
                                orpf32Nokm7Vo = Temporpf32Vo
                            End If
                        Next

                        '無い場合もある'
                        If orpf32Vo Is Nothing Then
                            orpf57Vos = TeiseiImpl.FindByORPF57(prpf02Vo.OldListCode, prpf02Vo.Kbba, prpf02Vo.Gyoid)

                            For Each Temporpf57Vo As AsORPF57Vo In orpf57Vos
                                If Temporpf57Vo.Nokm = "5" Or Temporpf57Vo.Nokm = "6" Then
                                    orpf57Vo = Temporpf57Vo
                                ElseIf Temporpf57Vo.Nokm = "7" Then
                                    orpf57Nokm7VO = Temporpf57Vo
                                End If
                            Next

                            If Not orpf57Vo Is Nothing Then
                                If Not orpf57Vo.Grno Is Nothing Then
                                    orpf61vo = TeiseiImpl.FindByORPF61(orpf57Vo.Grno, orpf57Vo.Srno)
                                End If
                            End If
                        Else
                            If Not orpf32Vo.Sgisba Is Nothing Then
                                orpf60Vo = TeiseiImpl.FindByORPF60(orpf32Vo.Sgisba, orpf32Vo.Kbba, orpf32Vo.Cmba, orpf32Vo.Nokm, orpf32Vo.Haym)
                            End If
                        End If

                        SetAs400(orpf32Vo, orpf57Vo, orpf60Vo, orpf61vo, orpf32Nokm7Vo, orpf57Nokm7VO)
                        column = column + 2

                        ''↓↓2015/01/12 メタル改訂抽出修正) (TES)劉 CHG BEGIN
                        ''発行No.'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _HakkoNo
                        ''発注年月日'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _HacyuDate
                        ''同期'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _Doki
                        ''分納'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _Bunno
                        ''ネック'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _Neck
                        ''暫定・欠品'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _Zank
                        ''その他'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _Sonota
                        ''備考'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _Biko
                        ''納期回答1'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NokiKaito1
                        ''納入予定数1'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NonyuYotei1
                        ''納入区分'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NonyuKbn
                        ''検収年月日'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _KensyuDate
                        ''納入累計数'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NonyuTotal
                        ''引取り検収年月日'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _HikitoriDate
                        ''引取り累計数'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _HikitoriTotal
                        ''納入実績マーク'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NonyuJissekiMark
                        ''取消年月日'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _TorikeshiDate
                        ''取消数2'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _TorikeshiTotal
                        ''納期回答2'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NonyuYotei2
                        ''納入予定数3'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NokiKaito2
                        ''納期回答3'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NonyuYotei3
                        ''納入予定数3'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NokiKaito3
                        ''納期回答4'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NonyuYotei4
                        ''納入予定数4'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NokiKaito4
                        ''納期回答5'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NonyuYotei5
                        ''納入予定数5'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NokiKaito5
                        ''納期回答6'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NonyuYotei6
                        ''納入予定数6'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NokiKaito6
                        ''納期回答7'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NonyuYotei7
                        ''納入予定数7'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NokiKaito7
                        ''納期回答8'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NonyuYotei8
                        ''納入予定数8'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _NokiKaito8
                        ''処置'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _Syochi
                        ''理由'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _Riyu
                        ''対応'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _Taio
                        ''部署'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _Busho
                        ''設計担当者'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _SekkeiTantosya
                        ''TEL'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _Tel
                        ''暫定品納入日'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _ZanteiHinNonyubi
                        ''正規扱いor後交換有り'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _SeikiOrKoukan
                        ''設通No.(最新)'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _StsrNew
                        ''設通No.(実績)'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _StsrJisseki
                        ''出図予定日(最新)'                 
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _ShutuzuYoteiDate
                        ''出図実績日'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _ShutuzuJissekiDate
                        ''型'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _Kata
                        ''工法'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _Koho
                        ''メーカー見積り型費'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _MakerKatahi
                        ''メーカー見積り部品日'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _MakerBuhinHi
                        ''工事区分'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _KoujiKbn
                        ''予算区分'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _YosanKbn
                        ''手番'
                        'dataMatrix(rowIndex, COLUMN_INSU + EzUtil.Increment(column) - 1) = _Teban
                        ''↑↑2015/01/12 メタル改訂抽出修正) (TES)劉 CHG END

                        '備考２'
                        '購入単価'
                        '決定単価(新調達)'
                        '支給単価(新調達)'
                        '決定単価(経理)'
                        '支給単価(経理)'
                        '手番'
                        '日付1'
                        '日付2'
                        '備考1'
                        '備考2'
                        '備考10'
                    End If
                End If

                'Continueで増えないようにする'
                rowIndex = rowIndex + 1
            Next
            xls.CopyRange(0, START_ROW, UBound(dataMatrix, 2) - 1, START_ROW + UBound(dataMatrix), dataMatrix)
        End Sub

        ''' <summary>
        ''' Excel出力タイトル行
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <param name="aBaseListVo"></param>
        ''' <remarks></remarks>
        Private Sub setShinchotastuTitleRow(ByVal xls As ShisakuExcel, ByVal aBaseListVo As List(Of TShisakuEventBaseVo))

            'フラグ'
            xls.SetValue(COLUMN_FLG, TITLE_ROW, "フラグ")
            xls.SetValue(COLUMN_RIREKI, TITLE_ROW, "履歴")
            xls.SetValue(COLUMN_CANCELJISSEKI, TITLE_ROW, "キャンセル実績")
            xls.SetValue(COLUMN_BLOCK_NO, TITLE_ROW, "ブロックNo")
            xls.SetValue(COLUMN_GYOU_ID, TITLE_ROW, "行ID")
            xls.SetValue(COLUMN_SENYOU_MARK, TITLE_ROW, "専用マーク")
            xls.SetValue(COLUMN_LEVEL, TITLE_ROW, "レベル")
            xls.SetValue(COLUMN_UNIT_KBN, TITLE_ROW, "ユニット区分")
            xls.SetValue(COLUMN_SYUKEI_CODE, TITLE_ROW, "集計コード")
            xls.SetValue(COLUMN_TEHAI_KIGOU, TITLE_ROW, "手配記号")
            xls.SetValue(COLUMN_KOUTAN, TITLE_ROW, "購担")
            xls.SetValue(COLUMN_TORIHIKISAKI_CODE, TITLE_ROW, "取引先コード")
            xls.SetValue(COLUMN_TORIHIKISAKI_NAME, TITLE_ROW, "取引先名称")
            xls.SetValue(COLUMN_BUHIN_NO, TITLE_ROW, "部品番号")
            xls.SetValue(COLUMN_SHISAKU_KBN, TITLE_ROW, "試作区分")
            xls.SetValue(COLUMN_KAITEI_NO, TITLE_ROW, "改訂No.")
            xls.SetValue(COLUMN_EDABAN, TITLE_ROW, "枝番")
            xls.SetValue(COLUMN_BUHIN_NAME, TITLE_ROW, "部品名称")
            xls.SetValue(COLUMN_NOUNYU_SHIJIBI, TITLE_ROW, "納入指示日")
            xls.SetValue(COLUMN_SHUTUZU_YOTEI_DATE, TITLE_ROW, "出図予定日")

            ''↓↓2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
            xls.SetValue(COLUMN_SHUTUZU_JISEKI_DATE, TITLE_ROW, "出図実績日")

            xls.SetValue(COLUMN_SHUTUZU_JISEKI_DATE, TITLE_ROW, "出図実績日")
            xls.SetValue(COLUMN_SHUTUZU_JISEKI_KAITEI_NO, TITLE_ROW, "出図実績・改訂№")
            xls.SetValue(COLUMN_SHUTUZU_JISEKI_STSR_DHSTBA, TITLE_ROW, "出図実績・設通№")

            xls.SetValue(COLUMN_SAISYU_SETSUHEN_DATE, TITLE_ROW, "最終織込設変情報・日付")
            xls.SetValue(COLUMN_SAISYU_SETSUHEN_KAITEI_NO, TITLE_ROW, "最終織込設変情報・改訂№")
            xls.SetValue(COLUMN_SAISYU_SETSUHEN_STSR_DHSTBA, TITLE_ROW, "最終織込設変情報・設通№")
            ''↑↑2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD END

            xls.SetValue(COLUMN_NOUNYU_SHIJISU, TITLE_ROW, "納入指示数")
            xls.SetValue(COLUMN_NOUBA, TITLE_ROW, "納入場所")
            xls.SetValue(COLUMN_KYOKU_SECTION, TITLE_ROW, "供給セクション")
            xls.SetValue(COLUMN_SAISHIYOFUKA, TITLE_ROW, "再使用不可")
            '↓↓2014/09/24 酒井 ADD BEGIN
            xls.MergeCells(COLUMN_TSUKURIKATA_SEISAKU, INSU_ROW, COLUMN_TSUKURIKATA_KIBO, INSU_ROW, True)
            xls.SetValue(COLUMN_TSUKURIKATA_SEISAKU, INSU_ROW, "作り方")
            xls.SetValue(COLUMN_TSUKURIKATA_SEISAKU, TITLE_ROW, "製作方法")
            xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_1, TITLE_ROW, "型仕様1")
            xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_2, TITLE_ROW, "型仕様2")
            xls.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_3, TITLE_ROW, "型仕様3")
            xls.SetValue(COLUMN_TSUKURIKATA_TIGU, TITLE_ROW, "治具")
            xls.SetValue(COLUMN_TSUKURIKATA_NOUNYU, TITLE_ROW, "納入見通し")
            xls.SetValue(COLUMN_TSUKURIKATA_KIBO, TITLE_ROW, "部品製作規模・概要")
            '↑↑2014/09/24 酒井 ADD END
            '材質'
            xls.MergeCells(COLUMN_ZAISHITU_KIKAKU_1, INSU_ROW, COLUMN_ZAISHITU_MEKKI, INSU_ROW, True)
            xls.SetValue(COLUMN_ZAISHITU_KIKAKU_1, INSU_ROW, "材質")
            xls.SetValue(COLUMN_ZAISHITU_KIKAKU_1, TITLE_ROW, "規格１")
            xls.SetValue(COLUMN_ZAISHITU_KIKAKU_2, TITLE_ROW, "規格２")
            xls.SetValue(COLUMN_ZAISHITU_KIKAKU_3, TITLE_ROW, "規格３")
            xls.SetValue(COLUMN_ZAISHITU_MEKKI, TITLE_ROW, "メッキ")
            '板厚'
            xls.MergeCells(COLUMN_BANKO, INSU_ROW, COLUMN_BANKO_U, INSU_ROW, True)
            xls.SetValue(COLUMN_BANKO, INSU_ROW, "板厚")
            xls.SetValue(COLUMN_BANKO, TITLE_ROW, "板厚")
            xls.SetValue(COLUMN_BANKO_U, TITLE_ROW, "u")


            ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
            '材料情報'
            xls.MergeCells(COLUMN_MATERIAL_INFO_LENGTH, INSU_ROW, COLUMN_MATERIAL_INFO_WIDTH, INSU_ROW, True)
            xls.SetValue(COLUMN_MATERIAL_INFO_LENGTH, INSU_ROW, "材料情報")
            '材料情報・製品長'
            xls.SetValue(COLUMN_MATERIAL_INFO_LENGTH, TITLE_ROW, "製品長")
            '材料情報・製品幅'
            xls.SetValue(COLUMN_MATERIAL_INFO_WIDTH, TITLE_ROW, "製品幅")

            xls.SetValue(COLUMN_ZAIRYO_SUNPO_X, TITLE_ROW, "材料寸法_X(mm)")
            xls.SetValue(COLUMN_ZAIRYO_SUNPO_Y, TITLE_ROW, "材料寸法_Y(mm)")
            xls.SetValue(COLUMN_ZAIRYO_SUNPO_Z, TITLE_ROW, "材料寸法_Z(mm)")
            xls.SetValue(COLUMN_ZAIRYO_SUNPO_XY, TITLE_ROW, "材料寸法_XY(mm)")
            xls.SetValue(COLUMN_ZAIRYO_SUNPO_XZ, TITLE_ROW, "材料寸法_XZ(mm)")
            xls.SetValue(COLUMN_ZAIRYO_SUNPO_YZ, TITLE_ROW, "材料寸法_YZ(mm)")

            ''↓↓2015/01/14 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
            '材料情報・発注対象'
            xls.SetValue(COLUMN_MATERIAL_INFO_ORDER_TARGET, TITLE_ROW, "発注対象")
            '材料情報・発注対象'
            xls.SetValue(COLUMN_MATERIAL_INFO_ORDER_TARGET_DATE, TITLE_ROW, "発注対象最終更新年月日")
            '材料情報・発注済'
            xls.SetValue(COLUMN_MATERIAL_INFO_ORDER_CHK, TITLE_ROW, "発注済")
            '材料情報・発注済最終更新年月日'
            xls.SetValue(COLUMN_MATERIAL_INFO_ORDER_CHK_DATE, TITLE_ROW, "発注済最終更新年月日")
            ''↑↑2015/01/14 メタル改訂抽出追加_z) (TES)劉 ADD END

            'データ項目'
            xls.MergeCells(COLUMN_DATA_ITEM_KAITEI_NO, INSU_ROW, COLUMN_DATA_ITEM_KAITEI_INFO, INSU_ROW, True)
            xls.SetValue(COLUMN_DATA_ITEM_KAITEI_NO, INSU_ROW, "データ項目")
            'データ項目・改訂№'
            xls.SetValue(COLUMN_DATA_ITEM_KAITEI_NO, TITLE_ROW, "改訂№")
            'データ項目・エリア名'
            xls.SetValue(COLUMN_DATA_ITEM_AREA_NAME, TITLE_ROW, "エリア名")
            'データ項目・セット名'
            xls.SetValue(COLUMN_DATA_ITEM_SET_NAME, TITLE_ROW, "セット名")
            'データ項目・改訂情報'
            xls.SetValue(COLUMN_DATA_ITEM_KAITEI_INFO, TITLE_ROW, "改訂情報")
            ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END

            ''↓↓2015/01/14 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
            'データ項目・データ支給チェック欄'
            xls.SetValue(COLUMN_DATA_ITEM_DATA_PROVISION, TITLE_ROW, "データ支給チェック欄")
            'データ項目・データ支給チェック欄最終更新年月日'
            xls.SetValue(COLUMN_DATA_ITEM_DATA_PROVISION_DATE, TITLE_ROW, "データ支給チェック欄最終更新年月日")
            ''↑↑2015/01/14 メタル改訂抽出追加_z) (TES)劉 ADD END


            '試作部品費(円)'
            xls.SetValue(COLUMN_SHISAKU_BUHIN_HI, TITLE_ROW, "試作部品費(円)")
            '試作型費(円)'
            xls.SetValue(COLUMN_SHISAKU_KATA_HI, TITLE_ROW, "試作型費(円)")

            '新試作手配システム'
            xls.SetValue(COLUMN_BIKOU, INSU_ROW, "新試作手配システム")
            '備考'
            xls.SetValue(COLUMN_BIKOU, TITLE_ROW, "備考")

            InsuCount = 0

            '同じ号車はまとめて同じ列に表示する！'
            If Not aBaseListVo.Count = 0 Then
                For index As Integer = 0 To aBaseListVo.Count - 1
                    If Not StringUtil.Equals(aBaseListVo(index).ShisakuGousya, "DUMMY") Then
                        xls.SetValue(COLUMN_INSU + InsuCount, INSU_ROW, aBaseListVo(index).ShisakuGousya)
                        xls.SetValue(COLUMN_INSU + InsuCount, TITLE_ROW, "員数" + (InsuCount + 1).ToString)
                        xls.SetOrientation(COLUMN_INSU + InsuCount, INSU_ROW, COLUMN_INSU + InsuCount, INSU_ROW, ShisakuExcel.XlOrientation.xlVertical)
                        InsuCount = InsuCount + 1
                    End If
                Next
                xls.SetValue(COLUMN_INSU + InsuCount, INSU_ROW, "")
                xls.SetValue(COLUMN_INSU + InsuCount, TITLE_ROW, "員数" + (InsuCount + 1).ToString)
                xls.SetOrientation(COLUMN_INSU + InsuCount, INSU_ROW, COLUMN_INSU + InsuCount, INSU_ROW, ShisakuExcel.XlOrientation.xlVertical)

                InsuCount = InsuCount + 1

                xls.SetValue(COLUMN_INSU + InsuCount, INSU_ROW, "DUMMY")
                xls.SetValue(COLUMN_INSU + InsuCount, TITLE_ROW, "員数" + (InsuCount + 1).ToString)
                xls.SetOrientation(COLUMN_INSU + InsuCount, INSU_ROW, COLUMN_INSU + InsuCount, INSU_ROW, ShisakuExcel.XlOrientation.xlVertical)
            End If


            Dim column As Integer = InsuCount + 1

            ''↓↓2015/01/14 メタル改訂抽出修正) (TES)劉 CHG BEGIN
            ''親品番'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "親品番")
            ''発行No.'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "発行No.")
            ''発注年月日'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "発注年月日")
            ''同期'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "同期")
            ''分納'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "分納")
            ''ネック'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "ネック")
            ''暫定・欠品'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "暫定・欠品")
            ''その他'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "その他")
            ''備考'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "備考")
            ''納期回答1'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納期回答1")
            ''納入予定数1'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納期予定数1")
            ''納入区分'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納入区分")
            ''検収年月日'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "検収年月日")
            ''納入累計数'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納入累計数")
            ''引取り検収年月日'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "引取り検収年月日")
            ''引取り累計数'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "引取り累計数")
            ''納入実績マーク'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納入実績マーク")
            ''取消年月日'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "取消年月日")
            ''取消数'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "取消数")
            ''納期回答2'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納期回答2")
            ''納入予定数2'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納期予定数2")
            ''納期回答3'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納期回答3")
            ''納入予定数3'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納期予定数3")
            ''納期回答4'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納期回答4")
            ''納入予定数4'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納期予定数4")
            ''納期回答5'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納期回答5")
            ''納入予定数5'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納期予定数5")
            ''納期回答6'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納期回答6")
            ''納入予定数6'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納期予定数6")
            ''納期回答7'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納期回答7")
            ''納入予定数7'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納期予定数7")
            ''納期回答8'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納期回答8")
            ''納入予定数8'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "納期予定数8")
            ''処置'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "処置")
            ''理由'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "理由")
            ''対応'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "対応")
            ''部署'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "部署")
            ''設計担当者'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "設計担当者")
            ''TEL'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "TEL")
            ''暫定品納入日'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "暫定品納入日")
            ''正規扱いor後交換有り'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "正規扱いor後交換有り")
            ''設通No.(最新)'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "設通No.(最新)")
            ''設通No.(実績)'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "設通No.(実績)")
            ''出図予定日(最新)'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "出図予定日(最新)")
            ''出図実績日'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "出図実績日")
            ''型'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "型")
            ''工法'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "工法")
            ''ﾒｰｶｰ見積り型費(円)'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "ﾒｰｶｰ見積り型費(円)")
            ''ﾒｰｶｰ見積り部品費(円)'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "ﾒｰｶｰ見積り部品費(円)")
            ''工事区分'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "工事区分")
            ''予算区分'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "予算区分")
            ''支給区分'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "支給区分")
            ''手番'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(column), TITLE_ROW, "手番")
            ''↑↑2015/01/14 メタル改訂抽出修正) (TES)劉 CHG END

            '設計書に無い項目'
            ''結合No.'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(InsuCount), TITLE_ROW, "結合No.")
            ''管理No.'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(InsuCount), TITLE_ROW, "管理No.")
            ''同期'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(InsuCount), TITLE_ROW, "同期")
            ''分納'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(InsuCount), TITLE_ROW, "検収年月日")
            ''ネック'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(InsuCount), TITLE_ROW, "検収年月日")
            ''暫定・欠品'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(InsuCount), TITLE_ROW, "検収年月日")
            ''その他'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(InsuCount), TITLE_ROW, "検収年月日")
            ''現行試作手配システム'
            'xls.SetValue(64, INSU_ROW, "現行試作手配システム")
            ''備考'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(InsuCount), TITLE_ROW, "備考")
            ''設通No.(計画)'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(InsuCount), TITLE_ROW, "設通No.(計画)")
            ''出図予定日(計画)'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(InsuCount), TITLE_ROW, "出図予定日(計画)")
            ''備考２'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(InsuCount), TITLE_ROW, "備考２")
            ''購入単価'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(InsuCount), TITLE_ROW, "購入単価")
            ''決定単価(新調達)'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(InsuCount), TITLE_ROW, "決定単価(新調達)")
            ''支給単価(新調達)'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(InsuCount), TITLE_ROW, "支給単価(新調達)")
            ''決定単価(経理)'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(InsuCount), TITLE_ROW, "決定単価(経理)")
            ''支給単価(経理)'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(InsuCount), TITLE_ROW, "支給単価(経理)")

            ''日付'
            'xls.SetValue(COLUMN_INSU + EzUtil.Increment(InsuCount), TITLE_ROW, "型")

        End Sub

        ''' <summary>
        ''' シートの列サイズ設定
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub setSheetColumnWidth(ByVal xls As ShisakuExcel)
            '員数行の高さを自動調整'
            xls.AutoFitRow(INSU_ROW, INSU_ROW)
            '列の幅を自動調整'
            xls.AutoFitCol(COLUMN_FLG, xls.EndCol())
            'ウィンドウ枠の固定'
            xls.FreezePanes(COLUMN_GYOU_ID, TITLE_ROW + 1, True)

        End Sub

        ''↓↓2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
        ''' <summary>
        ''' シートの設定(行の高さや列の幅等)
        ''' </summary>
        ''' <param name="xls">目的EXCELファイル</param>
        ''' <remarks></remarks>
        Private Sub setSheetColumnWidth1(ByVal xls As ShisakuExcel)
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
        ''↑↑2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD END

        '基本情報に紐付く号車情報を検索'
        ''' <summary>
        ''' 基本情報に紐付く号車情報を検索
        ''' </summary>
        ''' <param name="akihonVo"></param>
        ''' <param name="aGousyaVo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CheckGousyaKihon(ByVal akihonVo As TShisakuTehaiTeiseiKihonVo, ByVal aGousyaVo As TShisakuTehaiTeiseiGousyaVo) As Boolean
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
            If Not StringUtil.Equals(akihonVo.Flag, aGousyaVo.Flag) Then
                Return False
            End If
            Return True
        End Function

        'フラグによって表示する文字列を変更する'
        Private Function setFlg(ByVal Flg As String) As String
            Dim result As String
            Select Case Flg
                Case "1"
                    result = INSERT
                Case "2"
                    result = DELETE
                Case "3"
                    result = ACHANGE
                Case "4"
                    result = BCHANGE
                Case "5"
                    result = TEHAICHANGE
                Case Else
                    result = ""
            End Select
            Return result
        End Function

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
                _KensyuDate = Trim(StringUtil.Nvl(orpf32Vo.Noym))
                _NonyuTotal = Trim(StringUtil.Nvl(orpf32Vo.Noru))
                If Not orpf32Nokm7Vo Is Nothing Then
                    _HikitoriDate = Trim(StringUtil.Nvl(orpf32Nokm7Vo.Noym))
                    _HikitoriTotal = Trim(StringUtil.Nvl(orpf32Nokm7Vo.Noru))
                Else
                    _HikitoriDate = ""
                    _HikitoriTotal = ""
                End If
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
                _KensyuDate = Trim(StringUtil.Nvl(orpf57Vo.Noym))
                _NonyuTotal = Trim(StringUtil.Nvl(orpf57Vo.Noru))
                If Not orpf32Nokm7Vo Is Nothing Then
                    _HikitoriDate = Trim(StringUtil.Nvl(orpf57Nokm7Vo.Noym))
                    _HikitoriTotal = Trim(StringUtil.Nvl(orpf57Nokm7Vo.Noru))
                Else
                    _HikitoriDate = ""
                    _HikitoriTotal = ""
                End If
                _TorikeshiDate = Trim(StringUtil.Nvl(orpf57Vo.Tlym))
                _TorikeshiTotal = Trim(StringUtil.Nvl(orpf57Vo.Tlca))
                _NokiKaito2 = ""
                _NonyuYotei2 = ""
                _KoujiKbn = Trim(StringUtil.Nvl(orpf57Vo.Kokm))
                _YosanKbn = Trim(StringUtil.Nvl(orpf57Vo.Koba))

            End If

        End Sub

        '各列の番号指定'
        Private Sub SetColumnNo()
            Dim columnIndex As Integer = 1

            COLUMN_FLG = EzUtil.Increment(columnIndex)
            COLUMN_RIREKI = EzUtil.Increment(columnIndex)
            COLUMN_CANCELJISSEKI = EzUtil.Increment(columnIndex)
            COLUMN_BLOCK_NO = EzUtil.Increment(columnIndex)
            COLUMN_GYOU_ID = EzUtil.Increment(columnIndex)
            COLUMN_SENYOU_MARK = EzUtil.Increment(columnIndex)
            COLUMN_LEVEL = EzUtil.Increment(columnIndex)
            COLUMN_UNIT_KBN = EzUtil.Increment(columnIndex)
            COLUMN_SYUKEI_CODE = EzUtil.Increment(columnIndex)
            COLUMN_TEHAI_KIGOU = EzUtil.Increment(columnIndex)
            COLUMN_KOUTAN = EzUtil.Increment(columnIndex)
            COLUMN_TORIHIKISAKI_CODE = EzUtil.Increment(columnIndex)
            COLUMN_TORIHIKISAKI_NAME = EzUtil.Increment(columnIndex)
            COLUMN_BUHIN_NO = EzUtil.Increment(columnIndex)
            COLUMN_SHISAKU_KBN = EzUtil.Increment(columnIndex)
            COLUMN_KAITEI_NO = EzUtil.Increment(columnIndex)
            COLUMN_EDABAN = EzUtil.Increment(columnIndex)
            COLUMN_BUHIN_NAME = EzUtil.Increment(columnIndex)
            COLUMN_NOUNYU_SHIJIBI = EzUtil.Increment(columnIndex)

            COLUMN_SHUTUZU_YOTEI_DATE = EzUtil.Increment(columnIndex)
            ''↓↓2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
            '出図実績日'
            COLUMN_SHUTUZU_JISEKI_DATE = EzUtil.Increment(columnIndex)
            COLUMN_SHUTUZU_JISEKI_KAITEI_NO = EzUtil.Increment(columnIndex)
            COLUMN_SHUTUZU_JISEKI_STSR_DHSTBA = EzUtil.Increment(columnIndex)
            '最終織込設変情報・日付'
            COLUMN_SAISYU_SETSUHEN_DATE = EzUtil.Increment(columnIndex)
            COLUMN_SAISYU_SETSUHEN_KAITEI_NO = EzUtil.Increment(columnIndex)
            COLUMN_SAISYU_SETSUHEN_STSR_DHSTBA = EzUtil.Increment(columnIndex)
            ''↑↑2015/01/13 メタル改訂抽出追加_z) (TES)劉 ADD END

            COLUMN_NOUNYU_SHIJISU = EzUtil.Increment(columnIndex)
            COLUMN_NOUBA = EzUtil.Increment(columnIndex)
            COLUMN_KYOKU_SECTION = EzUtil.Increment(columnIndex)
            COLUMN_SAISHIYOFUKA = EzUtil.Increment(columnIndex)
            '↓↓2014/09/24 酒井 ADD BEGIN
            COLUMN_TSUKURIKATA_SEISAKU = EzUtil.Increment(columnIndex)
            COLUMN_TSUKURIKATA_KATASHIYOU_1 = EzUtil.Increment(columnIndex)
            COLUMN_TSUKURIKATA_KATASHIYOU_2 = EzUtil.Increment(columnIndex)
            COLUMN_TSUKURIKATA_KATASHIYOU_3 = EzUtil.Increment(columnIndex)
            COLUMN_TSUKURIKATA_TIGU = EzUtil.Increment(columnIndex)
            COLUMN_TSUKURIKATA_NOUNYU = EzUtil.Increment(columnIndex)
            COLUMN_TSUKURIKATA_KIBO = EzUtil.Increment(columnIndex)
            'COLUMN_BASE_BUHIN_FLG  = EzUtil.Increment(columnIndex)
            '↑↑2014/09/24 酒井 ADD END
            COLUMN_ZAISHITU_KIKAKU_1 = EzUtil.Increment(columnIndex)
            COLUMN_ZAISHITU_KIKAKU_2 = EzUtil.Increment(columnIndex)
            COLUMN_ZAISHITU_KIKAKU_3 = EzUtil.Increment(columnIndex)
            COLUMN_ZAISHITU_MEKKI = EzUtil.Increment(columnIndex)
            COLUMN_BANKO = EzUtil.Increment(columnIndex)
            COLUMN_BANKO_U = EzUtil.Increment(columnIndex)


            ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
            COLUMN_MATERIAL_INFO_LENGTH = EzUtil.Increment(columnIndex)
            COLUMN_MATERIAL_INFO_WIDTH = EzUtil.Increment(columnIndex)

            COLUMN_ZAIRYO_SUNPO_X = EzUtil.Increment(columnIndex)
            COLUMN_ZAIRYO_SUNPO_Y = EzUtil.Increment(columnIndex)
            COLUMN_ZAIRYO_SUNPO_Z = EzUtil.Increment(columnIndex)
            COLUMN_ZAIRYO_SUNPO_XY = EzUtil.Increment(columnIndex)
            COLUMN_ZAIRYO_SUNPO_XZ = EzUtil.Increment(columnIndex)
            COLUMN_ZAIRYO_SUNPO_YZ = EzUtil.Increment(columnIndex)

            ''↓↓2015/01/14 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
            '材料情報・発注対象'
            COLUMN_MATERIAL_INFO_ORDER_TARGET = EzUtil.Increment(columnIndex)
            '材料情報・発注対象最終更新年月日'
            COLUMN_MATERIAL_INFO_ORDER_TARGET_DATE = EzUtil.Increment(columnIndex)
            '材料情報・発注済'
            COLUMN_MATERIAL_INFO_ORDER_CHK = EzUtil.Increment(columnIndex)
            '材料情報・発注済最終更新年月日'
            COLUMN_MATERIAL_INFO_ORDER_CHK_DATE = EzUtil.Increment(columnIndex)
            ''↑↑2015/01/14 メタル改訂抽出追加_z) (TES)劉 ADD END

            COLUMN_DATA_ITEM_KAITEI_NO = EzUtil.Increment(columnIndex)
            COLUMN_DATA_ITEM_AREA_NAME = EzUtil.Increment(columnIndex)
            COLUMN_DATA_ITEM_SET_NAME = EzUtil.Increment(columnIndex)
            COLUMN_DATA_ITEM_KAITEI_INFO = EzUtil.Increment(columnIndex)
            ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END

            ''↓↓2015/01/14 メタル改訂抽出追加_z) (TES)劉 ADD BEGIN
            'データ項目・データ支給チェック欄'
            COLUMN_DATA_ITEM_DATA_PROVISION = EzUtil.Increment(columnIndex)
            'データ項目・データ支給チェック欄最終更新年月日'
            COLUMN_DATA_ITEM_DATA_PROVISION_DATE = EzUtil.Increment(columnIndex)
            ''↑↑2015/01/14 メタル改訂抽出追加_z) (TES)劉 ADD END

            COLUMN_SHISAKU_BUHIN_HI = EzUtil.Increment(columnIndex)
            COLUMN_SHISAKU_KATA_HI = EzUtil.Increment(columnIndex)
            COLUMN_BIKOU = EzUtil.Increment(columnIndex)
            COLUMN_INSU = EzUtil.Increment(columnIndex)

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


#Region "各種固定値"
        Private TEHAICHANGE As String = "変更(手配記号)"
        Private INSERT As String = "追加"
        Private DELETE As String = "削除"
        Private BCHANGE As String = "変更前"
        Private ACHANGE As String = "変更後"
#End Region

#Region "明細部各列の番号指定"
        '' フラグ
        Private COLUMN_FLG As Integer
        '' 履歴
        Private COLUMN_RIREKI As Integer
        '' キャンセル実績
        Private COLUMN_CANCELJISSEKI As Integer
        '' ブロックNo.
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
        Private COLUMN_SYUKEI_CODE As Integer
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
        '' 改訂No.
        Private COLUMN_KAITEI_NO As Integer
        '' 枝番
        Private COLUMN_EDABAN As Integer
        '' 部品名称
        Private COLUMN_BUHIN_NAME As Integer
        '' 納入指示日
        Private COLUMN_NOUNYU_SHIJIBI As Integer
        '' 出図予定日
        Private COLUMN_SHUTUZU_YOTEI_DATE As Integer
        '' 出図実績日'
        Private COLUMN_SHUTUZU_JISEKI_DATE As Integer
        '' '
        Private COLUMN_SHUTUZU_JISEKI_KAITEI_NO As Integer
        Private COLUMN_SHUTUZU_JISEKI_STSR_DHSTBA As Integer
        '最終織込設変情報・日付'
        Private COLUMN_SAISYU_SETSUHEN_DATE As Integer
        Private COLUMN_SAISYU_SETSUHEN_KAITEI_NO As Integer
        Private COLUMN_SAISYU_SETSUHEN_STSR_DHSTBA As Integer

        '' 納入指示数
        Private COLUMN_NOUNYU_SHIJISU As Integer
        '' 納入場所
        Private COLUMN_NOUBA As Integer
        '' 供給セクション
        Private COLUMN_KYOKU_SECTION As Integer
        '' 再使用不可
        Private COLUMN_SAISHIYOFUKA As Integer

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

        '材質'
        '' 材質規格１
        Private COLUMN_ZAISHITU_KIKAKU_1 As Integer
        '' 材質規格２
        Private COLUMN_ZAISHITU_KIKAKU_2 As Integer
        '' 材質規格３
        Private COLUMN_ZAISHITU_KIKAKU_3 As Integer
        '' 材質メッキ
        Private COLUMN_ZAISHITU_MEKKI As Integer
        '板厚'
        '' 板厚
        Private COLUMN_BANKO As Integer
        '' U
        Private COLUMN_BANKO_U As Integer

        ''↓↓2015/01/13 メタル追加_z) (TES)劉 ADD BEGIN
        '材料情報・発注対象'
        Private COLUMN_MATERIAL_INFO_ORDER_TARGET As Integer
        '材料情報・発注対象最終更新年月日'
        Private COLUMN_MATERIAL_INFO_ORDER_TARGET_DATE As Integer
        '材料情報・発注済'
        Private COLUMN_MATERIAL_INFO_ORDER_CHK As Integer
        '材料情報・発注済最終更新年月日'
        Private COLUMN_MATERIAL_INFO_ORDER_CHK_DATE As Integer
        'データ項目・データ支給チェック欄'
        Private COLUMN_DATA_ITEM_DATA_PROVISION
        'データ項目・データ支給チェック欄最終更新年月日		
        Private COLUMN_DATA_ITEM_DATA_PROVISION_DATE
        ''↑↑2015/01/13 メタル追加_z) (TES)劉 ADD END


        ''↓↓2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
        '' 材料情報・製品長
        Private COLUMN_MATERIAL_INFO_LENGTH As Integer
        '' 材料情報・製品幅
        Private COLUMN_MATERIAL_INFO_WIDTH As Integer


        Private COLUMN_ZAIRYO_SUNPO_X As Integer
        Private COLUMN_ZAIRYO_SUNPO_Y As Integer
        Private COLUMN_ZAIRYO_SUNPO_Z As Integer
        Private COLUMN_ZAIRYO_SUNPO_XY As Integer
        Private COLUMN_ZAIRYO_SUNPO_XZ As Integer
        Private COLUMN_ZAIRYO_SUNPO_YZ As Integer


        '' 材料情報・改訂№
        Private COLUMN_DATA_ITEM_KAITEI_NO As Integer
        '' 材料情報・エリア名
        Private COLUMN_DATA_ITEM_AREA_NAME As Integer
        '' 材料情報・セット名
        Private COLUMN_DATA_ITEM_SET_NAME As Integer
        '' 材料情報・改訂情報
        Private COLUMN_DATA_ITEM_KAITEI_INFO As Integer
        ''↑↑2014/12/29 メタル対応追加フィールド (DANIEL)柳沼 ADD END


        '' 試作部品費(円)
        Private COLUMN_SHISAKU_BUHIN_HI As Integer
        '' 試作型費(円)
        Private COLUMN_SHISAKU_KATA_HI As Integer
        '' 備考
        Private COLUMN_BIKOU As Integer
        '' 員数
        Private COLUMN_INSU As Integer

        ''↓↓2015/01/13 メタル追加_z) (TES)劉 ADD BEGIN
        '' 車種
        Private COLUMN_SYASYU As Integer
        '' 工事No
        Private COLUMN_KOUJI_NO As Integer
        '' 管理No
        Private COLUMN_KANRI_NO As Integer
        '' 段取り
        Private COLUMN_DANDORI As Integer
        '' 工数
        Private COLUMN_KOSU As Integer
        ''↑↑2015/01/13 メタル追加_z) (TES)劉 ADD END

#End Region

#Region "各行の番号指定"

        Private TITLE_ROW As Integer = 7

        Private START_ROW As Integer = 8

        Private INSU_ROW As Integer = 6

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


    End Class
End Namespace