Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports EBom.Excel
Imports System.IO
Imports System.Text
Imports Microsoft.VisualBasic.FileIO
Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.CellType
Imports ShisakuCommon.Ui.Spd
Imports ShisakuCommon.Util.LabelValue
Imports EventSakusei.TehaichoEdit
''' <summary>
''' 手配帳編集画面・過去発注情報引当
'''  
''' ※ 指定されたExcel一覧表の１行目の値をコンボボックスに設定する
''' 施雪晨
''' </summary>
''' <remarks></remarks>
Public Class Frm20ExcelKeyDataSelect

    ''' <summary>
    ''' 部品番号の列のindex
    ''' </summary>
    ''' <remarks></remarks>
    Private buhinColIndex As Integer
    Private KensakuList As New Dictionary(Of String, String)
    Private KoushinList As New Dictionary(Of String, String)
    Private OptionlList As New Dictionary(Of String, String)

#Region "コンストラクタ"
    Public Sub New()

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="selectExcel">選択されたExcel</param>
    ''' <param name="activeSheet">選択されたセル</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal selectExcel As Stream, ByVal activeSheet As SheetView, ByRef spdBase As FpSpread)
        InitializeComponent()
        Dim maxCol As Integer
        Dim value As Object
        Dim notEmptyFlg As Boolean = False

        '「選択されたセル」の行で、データが入力されている最右端の列番号を取得		
        For maxCol = activeSheet.ColumnCount - 1 To 0 Step -1
            value = activeSheet.Cells.Item(activeSheet.ActiveRowIndex, maxCol).Value
            If Not (value Is Nothing) AndAlso value.Trim.Length > 0 Then
                notEmptyFlg = True
                Exit For
            End If
        Next

        '（「選択されたセル」から上記の（最下端の行番号、最右端の列番号）を取込対象Rangeとする）																																																																																		
        '「Excelヘッダー項目」を取得する	
        Dim exlHead As List(Of LabelValueVo) = New List(Of LabelValueVo)
        Dim strComboxKey As String() = {"RIREKI", "SHISAKU_BLOCK_NO", "GYOU_ID", "SENYOU_MARK", "LEVEL", _
                                        "BUHIN_NO", "BUHIN_NO_KBN", "BUHIN_NO_KAITEI_NO", "EDA_BAN", "BUHIN_NAME", _
                                        "SHUKEI_CODE", "TEHAI_KIGOU", "KOUTAN", "TORIHIKISAKI_CODE", "NOUBA", "KYOUKU_SECTION", "NOUNYU_SHIJIBI", _
                                        "TOTAL_INSU_SURYO", "SAISHIYOUFUKA", "SHUTUZU_YOTEI_DATE", _
                                        "SHUTUZU_JISEKI_DATE", "SAISYU_SETSUHEN_DATE", "SAISYU_SETSUHEN_KAITEI_NO", _
                                        "SHUTUZU_JISEKI_STSR_DHSTBA", _
                                        "TSUKURIKATA_SEISAKU", "TSUKURIKATA_KATASHIYOU_1", "TSUKURIKATA_KATASHIYOU_2", "TSUKURIKATA_KATASHIYOU_3", "TSUKURIKATA_TIGU", "TSUKURIKATA_NOUNYU", "TSUKURIKATA_KIBO", _
                                        "ZAISHITU_KIKAKU_1", "ZAISHITU_KIKAKU_2", "ZAISHITU_KIKAKU_3", "ZAISHITU_MEKKI", _
                                        "SHISAKU_BANKO_SURYO", "SHISAKU_BANKO_SURYO_U", _
                                        "MATERIAL_INFO_LENGTH", "MATERIAL_INFO_WIDTH", "MATERIAL_INFO_ORDER_TARGET", "MATERIAL_INFO_ORDER_CHK", _
                                        "DATA_ITEM_KAITEI_NO", "DATA_ITEM_AREA_NAME", "DATA_ITEM_SET_NAME", "DATA_ITEM_KAITEI_INFO", "DATA_ITEM_DATA_PROVISION", _
                                        "SHISAKU_BUHINN_HI", "SHISAKU_KATA_HI", "MAKER_CODE", "BIKOU", "BUHIN_NO_OYA", "BUHIN_NO_KBN_OYA", "HENKATEN", "AUTO_ORIKOMI_KAITEI_NO"}
        Dim strComboxValue As String() = {"履歴", "ブロック", "行ID", "専用", "レベル", "部品番号", "区分", "改訂", "枝番", "部品名称", "集計コード", "手配記号", "購担", _
                                          "取引先コード", "納場", "供給セクション", "納入指示日", "合計員数", "再使用不可", "出図予定日", _
                                          "出図実績日", "日付", "改訂№", _
                                          "図面設通№", _
                                          "製作方法", "型仕様1", "型仕様2", "型仕様3", "治具", "納入見通し", "部品製作規模・概要", _
                                          "材質1", "材質2", _
                                          "材質3", "材質4", "板厚1", "板厚2", _
                                          "製品長", "製品幅", "発注対象", "発注済", _
                                          "改訂№", "エリア名", "セット名", "改訂情報", "データ支給チェック欄", _
                                          "試作部品費（円）", "試作型費（千円）", "取引先名称", "備考", "親部品", "親部品試作区分", "変化点", "自動織込み改訂No"}
        '暫定処置。SpdBaseから動的に取得したい。
        Dim strComboxSpcColulmnIndex As String() = { _
                                                    "0", "2", "3", "4", "5", "7", "8", "9", _
                                                    "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", _
                                                    "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", _
                                                    "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", _
                                                    "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", _
                                                    "50", "51", "52", "53", "54", "55" _
                                                    }
        '↑↑↑2014/12/30 メタル項目を追加 TES)張 CHG END

        Dim i As Integer
        Dim value2 As String
        'For i = activeSheet.ActiveColumnIndex To maxCol
        For i = 0 To maxCol
            value2 = getColumnString(i) & ":" & activeSheet.Cells(activeSheet.ActiveRowIndex, i).Value

            '取込対象項目（index）が履歴、ブロック 、行ID、専用、レベル、部品番号、区分または合計員数の場合
            If Not String.IsNullOrEmpty(value2) Then
                exlHead.Add(New LabelValueVo(value2, i))
            End If
        Next
        spdSearch.EditMode = False


        'Frm20DispTehaichoEditのスプレッドの1行目の値「取込対象項目」を取得する		
        Dim spdList As List(Of String) = New List(Of String)
        For Each s As String In strComboxValue
            spdList.Add(s)
        Next
        spdSearch_Sheet1.Columns.Clear()
        spdSearch_Sheet1.AddColumns(0, spdList.Count)
        spdUpdate_Sheet1.Columns.Clear()
        spdUpdate_Sheet1.AddColumns(0, spdList.Count)
        For i = 0 To spdList.Count - 1
            spdSearch_Sheet1.Columns.Item(i).Width = 100
            spdUpdate_Sheet1.Columns.Item(i).Width = 100
            spdSearch_Sheet1.Columns(i).Tag = strComboxSpcColulmnIndex(i)
            spdUpdate_Sheet1.Columns(i).Tag = strComboxSpcColulmnIndex(i)
            spdSearch_Sheet1.SetValue(1, i, spdList(i))
            spdUpdate_Sheet1.SetValue(1, i, spdList(i))
            Dim LockFlg As Boolean = False

            ''取込対象項目（index）が履歴、ブロック 、行ID、専用、レベル、部品番号、区分または合計員数の場合
            'Frm20ExcelKeyDataSelectの「検索キー項目の設定／EXCELヘッダー選択」.indexと「更新対象項目の設定／EXCELヘッダー選択」.indexの
            'コンボの選択値を削除して、使用不可に設定する
            If strComboxKey(i).Equals("RIREKI") OrElse strComboxKey(i).Equals("SHISAKU_BLOCK_NO") _
            OrElse strComboxKey(i).Equals("GYOU_ID") OrElse strComboxKey(i).Equals("SENYOU_MARK") _
            OrElse strComboxKey(i).Equals("LEVEL") OrElse strComboxKey(i).Equals("BUHIN_NO") _
            OrElse strComboxKey(i).Equals("BUHIN_NO_KBN") OrElse strComboxKey(i).Equals("TOTAL_INSU_SURYO") Then
                LockFlg = True
            End If
            If strComboxKey(i).Equals("BUHIN_NO") Then
                ''↑↑2014/09/05 Ⅰ.9.過去発注情報引当機能_f) 酒井 ADD END
                buhinColIndex = i
            End If
            If LockFlg Then
                'コンボの選択値を削除して
                spdUpdate_Sheet1.SetValue(0, i, "")
                '使用不可に設定する	
                spdUpdate_Sheet1.Columns(i).Locked = True
            End If
            If strComboxValue(i).Equals("部品番号表示順") Or strComboxValue(i).Equals("部課コード") Then
                '使用不可に設定する	
                spdSearch_Sheet1.Columns(i).Visible = False
                '使用不可に設定する	
                spdUpdate_Sheet1.Columns(i).Visible = False
            End If
        Next
        For index = 0 To spdSearch_Sheet1.Columns.Count - 1
            SpreadUtil.BindNewComboBoxToCell(spdSearch_Sheet1, 0, spdSearch_Sheet1.Columns(index).Tag, exlHead)
            SpreadUtil.BindNewComboBoxToCell(spdUpdate_Sheet1, 0, spdSearch_Sheet1.Columns(index).Tag, exlHead)
        Next

        Dim progPath As String = My.Application.Info.DirectoryPath
        Dim filePath As String = progPath & "\SHISAKU_TEHAI_IMPORT.ini"

        '設定ファイル（C:\SHISAKU_TEHAI_IMPORT.ini仮）が存在しない場合
        If Not FileIO.FileSystem.FileExists(filePath) Then Exit Sub
        Dim rowdata As New List(Of String)
        'INIファイル読み込み
        Using parser = New TextFieldParser(filePath, Encoding.GetEncoding("SHIFT_JIS"))
            parser.TextFieldType = FieldType.Delimited     '区切り形式

            parser.SetDelimiters(",")              '区切り文字

            parser.HasFieldsEnclosedInQuotes = False       'ダブルクォーテーション中のカンマは無視

            parser.TrimWhiteSpace = True                   '空白を取り除く

            '行を読み込み
            ''↓↓2014/09/05 Ⅰ.9.過去発注情報引当機能_f) 酒井 ADD BEGIN
            Dim index As Integer = 0
            While Not parser.EndOfData()
                rowdata.Add(parser.ReadFields(0))      '行データ
                If rowdata.Count < 0 Then
                    Return
                End If
                index = index + 1
            End While
        End Using
        Dim StartKensaku As Integer = 0
        Dim StartKoushin As Integer = 0
        Dim StartOption As Integer = 0
        For index = 0 To rowdata.Count - 1
            If rowdata(index).IndexOf("[") >= 0 AndAlso rowdata(index).Trim().Length > 0 Then
                Dim StrSections = rowdata(index).Trim()
                Select Case StrSections
                    Case "[KENSAKU]"
                        StartKensaku = index + 1
                    Case "[KOUSHIN]"
                        StartKoushin = index + 1
                    Case "[OPTION]"
                        StartOption = index + 1
                    Case Else
                        Continue For
                End Select
            End If
        Next
        If StartKensaku < StartKoushin Then
            For index = StartKensaku To StartKoushin - 2
                If rowdata(index).Trim().IndexOf("=") >= 0 Then
                    Dim strList() As String = rowdata(index).Trim().Split("=")
                    KensakuList.Add(strList(0), strList(1))
                End If
            Next
        End If
        If StartKoushin < StartOption Then
            For index = StartKoushin To StartOption - 2
                If rowdata(index).Trim().IndexOf("=") >= 0 Then
                    Dim strList() As String = rowdata(index).Trim().Split("=")
                    KoushinList.Add(strList(0), strList(1))
                End If
            Next
            For index = StartOption To rowdata.Count - 1
                If rowdata(index).Trim().IndexOf("=") >= 0 Then
                    Dim strList() As String = rowdata(index).Trim().Split("=")
                    OptionlList.Add(strList(0), strList(1))
                End If
            Next
        End If
        For Each index As String In KensakuList.Keys
            If Not String.IsNullOrEmpty(KensakuList(index)) Then
                spdSearch_Sheet1.Cells(0, spdSearch_Sheet1.Columns(index.ToString).Index).Text = activeSheet.Cells(activeSheet.ActiveRowIndex, KensakuList(index)).Text
                spdUpdate_Sheet1.Columns(index).Locked = True
            End If

        Next
        For Each index As String In KoushinList.Keys
            If Not String.IsNullOrEmpty(KoushinList(index)) Then
                spdUpdate_Sheet1.Cells(0, spdUpdate_Sheet1.Columns(index.ToString).Index).Text = activeSheet.Cells(activeSheet.ActiveRowIndex, KoushinList(index)).Text
            End If
        Next
        'For Each Index As String In OptionlList.Keys
        '    If Index = "TORIKOMISAKI_KUUHAKU" Then
        '        If Integer.Parse(OptionlList(Index)) = 0 Then
        '            cbxTORIKOMISAKI_KUUHAKU.Checked = False
        '        Else
        '            cbxTORIKOMISAKI_KUUHAKU.Checked = True
        '        End If
        '    Else
        '        If Integer.Parse(OptionlList(Index)) = 0 Then
        '            cbxTORIKOMIMOTO_KUUHAKU.Checked = False
        '        Else
        '            cbxTORIKOMIMOTO_KUUHAKU.Checked = True
        '        End If
        '    End If
        'Next
        '画面項目数全てのキーが[KENSAKU][KOUSHIN]セクションに存在しない場合、値がEXCELの項目数を超えている場合、キー、セクションが不足している場合
        If KoushinList.Count = 0 OrElse spdSearch_Sheet1.Columns.Count < KoushinList.Keys.Count OrElse spdSearch_Sheet1.Columns.Count > KoushinList.Keys.Count Then
            MsgBox("前回の設定内容を復元できませんでした。初期状態で表示します。")
            Exit Sub
        End If
    End Sub
#End Region

    Private Function getColumnString(ByVal col As Integer) As String
        Dim st() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}
        Dim rtn As String = ""
        If Math.Truncate(col / 26) >= 1 Then
            rtn = st(Math.Truncate(col / 26) - 1)
        End If
        rtn &= st(col Mod 26)
        Return rtn
    End Function

#Region "イベント"

#Region "フォームロード"
    ''' <summary>
    ''' フォームロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm20ExcelKeyDataSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '下記処理を行わないと画面表示直後にSpreadの左端のプルダウンが選択できない
        spdSearch_Sheet1.SetActiveCell(1, 0)
        spdSearch_Sheet1.SetActiveCell(0, 0)
    End Sub
#End Region

#Region "ボタン(戻る)"

    ''' <summary>
    ''' 戻るボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

#Region "ボタン（取り込み実行）"
    ''' <summary>
    '''　取り込み実行ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnJikkou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJikkou.Click
        ' 「検索キー項目の設定／EXCELヘッダー選択」の部品番号コンボが選択状態でない場合
        If spdSearch_Sheet1.Cells(0, buhinColIndex).Value = "" Then
            MsgBox("部品番号は検索キーとして必ず選択してください")
            Exit Sub
        End If
        ''↓↓2014/09/05 Ⅰ.9.過去発注情報引当機能 i) 酒井 ADD BEGIN
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
        ''↑↑2014/09/05 Ⅰ.9.過去発注情報引当機能_i) 酒井 ADD END   

    End Sub
#End Region



#End Region

#Region "「検索キー項目の設定」_Change"
    Private Sub 検索キー項目の設定_Chang(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles spdSearch.ComboCloseUp

        If spdSearch_Sheet1.Cells(0, e.Column).Text = "" Then
            If Not ( _
            spdSearch_Sheet1.Cells(1, e.Column).Text = "履歴" _
            Or spdSearch_Sheet1.Cells(1, e.Column).Text = "ブロック" _
            Or spdSearch_Sheet1.Cells(1, e.Column).Text = "行ID" _
            Or spdSearch_Sheet1.Cells(1, e.Column).Text = "専用" _
            Or spdSearch_Sheet1.Cells(1, e.Column).Text = "レベル" _
            Or spdSearch_Sheet1.Cells(1, e.Column).Text = "部品番号" _
            Or spdSearch_Sheet1.Cells(1, e.Column).Text = "区分" _
            Or spdSearch_Sheet1.Cells(1, e.Column).Text = "合計員数" _
            ) Then
                spdUpdate_Sheet1.Cells(0, e.Column).Locked = False
                spdUpdate_Sheet1.Columns(e.Column).Locked = False
            End If
        Else
            spdUpdate_Sheet1.Cells(0, e.Column).Text = ""
            spdUpdate_Sheet1.Cells(0, e.Column).Locked = True
            spdUpdate_Sheet1.Columns(e.Column).Locked = True
        End If

    End Sub

#End Region

End Class
