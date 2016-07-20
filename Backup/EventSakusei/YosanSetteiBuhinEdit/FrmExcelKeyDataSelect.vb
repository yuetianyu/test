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
Imports EventSakusei.YosanSetteiBuhinEdit
Imports ShisakuCommon

''' <summary>
''' 手配帳編集画面・過去発注情報引当
'''  
''' ※ 指定されたExcel一覧表の１行目の値をコンボボックスに設定する
''' 施雪晨
''' </summary>
''' <remarks></remarks>
Public Class FrmExcelKeyDataSelect

    ''' <summary>
    ''' 部品番号の列のindex
    ''' </summary>
    ''' <remarks></remarks>
    Private buhinColIndex As Integer
    Private KensakuList As New Dictionary(Of String, String)
    Private KoushinList As New Dictionary(Of String, String)
    Private OptionlList As New Dictionary(Of String, String)
    Private Const FILE_PATH As String = "\YOSAN_SETTEI_IMPORT.ini"



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

        Dim exlHead As List(Of LabelValueVo) = New List(Of LabelValueVo)

        '「選択されたセル」の行で、データが入力されている最右端の列番号を取得		
        For maxCol = activeSheet.ColumnCount - 1 To 0 Step -1
            value = activeSheet.Cells.Item(activeSheet.ActiveRowIndex, maxCol).Value
            '2016/01/22 kabasawa'
            '浮動小数点が読み込めないので修正'
            If StringUtil.IsNotEmpty(value) Then
                notEmptyFlg = True
                Exit For
            End If
        Next


        '（「選択されたセル」から上記の（最下端の行番号、最右端の列番号）を取込対象Rangeとする）																																																																																		
        '「Excelヘッダー項目」を取得する	
        '暫定処置。SpdBaseから動的に取得したい。
        Dim strComboxSpcColulmnIndex As String() = { _
                                                    "8", "28", "29", "30", "31", "118", "119", "120", "122"}



        Dim strComboxKey As String() = {NmSpdTagBase.TAG_YOSAN_BUHIN_NO, _
                                        NmSpdTagBase.TAG_YOSAN_KONKYO_KEISU_1, _
                                         NmSpdTagBase.TAG_YOSAN_KONKYO_KOUHOU, _
                                         NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI, _
                                         NmSpdTagBase.TAG_YOSAN_WARITUKE_KEISU_2, _
                                         NmSpdTagBase.TAG_YOSAN_WARITUKE_KATA_HI, _
                                         NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_TANKA, _
                                         NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI, _
                                         NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_KATA_HI}

        Dim strComboxValue As String() = {"部品番号", _
                                          "係数1", _
                                          "工法", _
                                          "割付予算_部品費(円)", _
                                          "係数2", "割付予算_型費(千円)", _
                                          "購入希望単価_購入希望単価(円)", _
                                          "購入希望単価_部品費(円)", _
                                          "購入希望単価_型費(千円)"}


        '検索項目用'
        'ブロック、供給セクション、取引先コードを追加'
        Dim strComboxSearchSpcColulmnIndex As String() = {"2", "8", "11", "12", "28", "29", "30", "31", "118", "119", "120", "122"}



        Dim strComboxSearchKey As String() = {NmSpdTagBase.TAG_YOSAN_BLOCK_NO, _
                                              NmSpdTagBase.TAG_YOSAN_BUHIN_NO, _
                                              NmSpdTagBase.TAG_YOSAN_MAKER_CODE, _
                                              NmSpdTagBase.TAG_YOSAN_KYOUKU_SECTION, _
                                              NmSpdTagBase.TAG_YOSAN_KONKYO_KEISU_1, _
                                              NmSpdTagBase.TAG_YOSAN_KONKYO_KOUHOU, _
                                              NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI, _
                                              NmSpdTagBase.TAG_YOSAN_WARITUKE_KEISU_2, _
                                              NmSpdTagBase.TAG_YOSAN_WARITUKE_KATA_HI, _
                                              NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_TANKA, _
                                              NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI, _
                                              NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_KATA_HI}

        Dim strComboxSearchValue As String() = {"ブロック№", _
                                                "部品番号", _
                                                "取引先コード", _
                                                "供給セクション", _
                                                "係数1", _
                                                "工法", _
                                                "割付予算_部品費(円)", _
                                                "係数2", "割付予算_型費(千円)", _
                                                "購入希望単価_購入希望単価(円)", _
                                                "購入希望単価_部品費(円)", _
                                                "購入希望単価_型費(千円)"}





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
        spdUpdate_Sheet1.Columns.Clear()
        spdUpdate_Sheet1.AddColumns(0, spdList.Count)
        '更新用'
        For i = 0 To spdList.Count - 1
            spdUpdate_Sheet1.Columns.Item(i).Width = 100
            spdUpdate_Sheet1.Columns(i).Tag = strComboxSpcColulmnIndex(i)
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
            If LockFlg Then
                'コンボの選択値を削除して
                spdUpdate_Sheet1.SetValue(0, i, "")
                '使用不可に設定する	
                spdUpdate_Sheet1.Columns(i).Locked = True
            End If
        Next
        For index = 0 To spdUpdate_Sheet1.Columns.Count - 1
            SpreadUtil.BindNewComboBoxToCell(spdUpdate_Sheet1, 0, spdUpdate_Sheet1.Columns(index).Tag, exlHead)
        Next


        Dim spdSearchList As List(Of String) = New List(Of String)
        For Each s As String In strComboxSearchValue
            spdSearchList.Add(s)
        Next
        '検索用'
        spdSearch_Sheet1.Columns.Clear()
        spdSearch_Sheet1.AddColumns(0, spdSearchList.Count)

        For i = 0 To spdSearchList.Count - 1

            spdSearch_Sheet1.Columns.Item(i).Width = 100
            spdSearch_Sheet1.Columns(i).Tag = strComboxSearchSpcColulmnIndex(i)
            spdSearch_Sheet1.SetValue(1, i, spdSearchList(i))
            Dim LockFlg As Boolean = False

            ''取込対象項目（index）が履歴、ブロック 、行ID、専用、レベル、部品番号、区分または合計員数の場合
            'Frm20ExcelKeyDataSelectの「検索キー項目の設定／EXCELヘッダー選択」.indexと「更新対象項目の設定／EXCELヘッダー選択」.indexの
            'コンボの選択値を削除して、使用不可に設定する
            If strComboxSearchKey(i).Equals("RIREKI") OrElse strComboxSearchKey(i).Equals("SHISAKU_BLOCK_NO") _
            OrElse strComboxSearchKey(i).Equals("GYOU_ID") OrElse strComboxSearchKey(i).Equals("SENYOU_MARK") _
            OrElse strComboxSearchKey(i).Equals("LEVEL") OrElse strComboxSearchKey(i).Equals("BUHIN_NO") _
            OrElse strComboxSearchKey(i).Equals("BUHIN_NO_KBN") OrElse strComboxSearchKey(i).Equals("TOTAL_INSU_SURYO") Then
                LockFlg = True
            End If
            If strComboxSearchKey(i).Equals(NmSpdTagBase.TAG_YOSAN_BUHIN_NO) Then
                ''↑↑2014/09/05 Ⅰ.9.過去発注情報引当機能_f) 酒井 ADD END
                buhinColIndex = i
            End If
            If strComboxSearchValue(i).Equals("部品番号表示順") Or strComboxSearchValue(i).Equals("部課コード") Then
                '使用不可に設定する	
                spdSearch_Sheet1.Columns(i).Visible = False
            End If
        Next



        For index = 0 To spdSearch_Sheet1.Columns.Count - 1
            SpreadUtil.BindNewComboBoxToCell(spdSearch_Sheet1, 0, spdSearch_Sheet1.Columns(index).Tag, exlHead)
        Next

        Dim progPath As String = My.Application.Info.DirectoryPath
        Dim filePath As String = progPath & FILE_PATH

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
                spdSearch_Sheet1.Cells(0, spdSearch_Sheet1.Columns(index.ToString).Index).Value = KensakuList(index)
                'spdSearch_Sheet1.Cells(0, spdSearch_Sheet1.Columns(index.ToString).Index).Text = activeSheet.Cells(activeSheet.ActiveRowIndex, KensakuList(index)).Text
                If Not spdUpdate_Sheet1.Columns(index) Is Nothing Then
                    spdUpdate_Sheet1.Columns(index).Locked = True
                End If
            End If

        Next
        For Each index As String In KoushinList.Keys
            If Not String.IsNullOrEmpty(KoushinList(index)) Then
                spdUpdate_Sheet1.Cells(0, spdUpdate_Sheet1.Columns(index.ToString).Index).Value = KoushinList(index)
                'spdUpdate_Sheet1.Cells(0, spdUpdate_Sheet1.Columns(index.ToString).Index).Text = activeSheet.Cells(activeSheet.ActiveRowIndex, KoushinList(index)).Text
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
        If KoushinList.Count = 0 OrElse spdSearch_Sheet1.Columns.Count < KoushinList.Keys.Count Then
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
    Private Sub FrmExcelKeyDataSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
                If spdUpdate_Sheet1.ColumnCount < e.Column Then
                    spdUpdate_Sheet1.Cells(0, e.Column).Locked = False
                    spdUpdate_Sheet1.Columns(e.Column).Locked = False
                End If

            End If
        Else

            Dim tag As String = spdSearch_Sheet1.Columns(e.Column).Tag.ToString

            If Not spdUpdate_Sheet1.Columns(tag) Is Nothing Then
                spdUpdate_Sheet1.Cells(0, spdUpdate_Sheet1.Columns(tag).Index).Text = ""
                spdUpdate_Sheet1.Cells(0, spdUpdate_Sheet1.Columns(tag).Index).Locked = True
                spdUpdate_Sheet1.Columns(spdUpdate_Sheet1.Columns(tag).Index).Locked = True

            End If

        End If

    End Sub

#End Region


    ''' <summary>
    ''' 検索コンボボックス用のリスト
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSearchColumns() As List(Of ColumnData)
        'タグは入手可能だが、シートから和名が入手できない。(２行だったり１行だったりするので)'

        'タグ名と和名のセット'
        '使う対象は手入力可能な箇所だけ。'
        Dim result As New List(Of ColumnData)

        Dim voBlock As New ColumnData
        voBlock.ColumnIndex = 2
        voBlock.ColumnTag = NmSpdTagBase.TAG_YOSAN_BLOCK_NO
        voBlock.ColumnName = "ブロック№"

        result.Add(voBlock)

        Dim voLevel As New ColumnData
        voLevel.ColumnIndex = 4
        voLevel.ColumnTag = NmSpdTagBase.TAG_YOSAN_LEVEL
        voLevel.ColumnName = "レベル"

        result.Add(voLevel)

        Dim voShukei As New ColumnData
        voShukei.ColumnIndex = 6
        voShukei.ColumnTag = NmSpdTagBase.TAG_YOSAN_SHUKEI_CODE
        voShukei.ColumnName = "国内集計"

        result.Add(voShukei)


        Dim voSiaShukei As New ColumnData
        voSiaShukei.ColumnIndex = 7
        voSiaShukei.ColumnTag = NmSpdTagBase.TAG_YOSAN_SIA_SHUKEI_CODE
        voSiaShukei.ColumnName = "海外集計"

        result.Add(voSiaShukei)

        Dim voBuhin As New ColumnData
        voBuhin.ColumnIndex = 8
        voBuhin.ColumnTag = NmSpdTagBase.TAG_YOSAN_BUHIN_NO
        voBuhin.ColumnName = "部品番号"

        result.Add(voBuhin)


        Dim voMaker As New ColumnData
        voMaker.ColumnIndex = 11
        voMaker.ColumnTag = NmSpdTagBase.TAG_YOSAN_MAKER_CODE
        voMaker.ColumnName = "取引先コード"

        result.Add(voMaker)

        Dim voKyokyu As New ColumnData
        voKyokyu.ColumnIndex = 12
        voKyokyu.ColumnTag = NmSpdTagBase.TAG_YOSAN_KYOUKU_SECTION
        voKyokyu.ColumnName = "供給セクション"

        result.Add(voKyokyu)

        Dim voKoutan As New ColumnData
        voKoutan.ColumnIndex = 13
        voKoutan.ColumnTag = NmSpdTagBase.TAG_YOSAN_KOUTAN
        voKoutan.ColumnName = "購坦"

        result.Add(voKoutan)

        Dim vo As New ColumnData
        vo.ColumnIndex = 28
        vo.ColumnTag = NmSpdTagBase.TAG_YOSAN_KONKYO_KEISU_1
        vo.ColumnName = "係数1"

        result.Add(vo)

        Dim vo2 As New ColumnData
        vo2.ColumnIndex = 31
        vo2.ColumnTag = NmSpdTagBase.TAG_YOSAN_WARITUKE_KEISU_2
        vo2.ColumnName = "係数2"

        result.Add(vo2)

        Dim vo3 As New ColumnData
        vo3.ColumnIndex = 29
        vo3.ColumnTag = NmSpdTagBase.TAG_YOSAN_KONKYO_KOUHOU
        vo3.ColumnName = "工法"

        result.Add(vo3)

        Dim vo4 As New ColumnData
        vo4.ColumnIndex = 30
        vo4.ColumnTag = NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI
        vo4.ColumnName = "割付予算_部品費(円)"

        result.Add(vo4)

        Dim vo5 As New ColumnData
        vo5.ColumnIndex = 118
        vo5.ColumnTag = NmSpdTagBase.TAG_YOSAN_WARITUKE_KATA_HI
        vo5.ColumnName = "割付予算_型費(千円)"

        result.Add(vo5)

        Dim vo6 As New ColumnData
        vo6.ColumnIndex = 119
        vo6.ColumnTag = NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_TANKA
        vo6.ColumnName = "購入希望単価_購入希望単価(円)"

        result.Add(vo6)

        Dim vo7 As New ColumnData
        vo7.ColumnIndex = 120
        vo7.ColumnTag = NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI
        vo7.ColumnName = "購入希望単価_部品費(円)"

        result.Add(vo7)

        Dim vo8 As New ColumnData
        vo8.ColumnIndex = 122
        vo8.ColumnTag = NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_KATA_HI
        vo8.ColumnName = "購入希望単価_型費(千円)"

        result.Add(vo8)


        'result.Add(NmSpdTagBase.TAG_AUD_FLAG, "フラグ")
        'result.Add(NmSpdTagBase.TAG_YOSAN_BUKA_CODE, "部課コード")
        'result.Add(NmSpdTagBase.TAG_YOSAN_BLOCK_NO, "ブロック№")
        'result.Add(NmSpdTagBase.TAG_YOSAN_GYOU_ID, "行ID")
        'result.Add(NmSpdTagBase.TAG_YOSAN_LEVEL, "レベル")
        'result.Add(NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN, "部品番号表示順")
        'result.Add(NmSpdTagBase.TAG_YOSAN_SHUKEI_CODE, "国内集計")
        'result.Add(NmSpdTagBase.TAG_YOSAN_SIA_SHUKEI_CODE, "海外集計")
        'result.Add(NmSpdTagBase.TAG_YOSAN_BUHIN_NO, "部品番号")
        'result.Add(NmSpdTagBase.TAG_YOSAN_BUHIN_NAME, "部品名称")
        'result.Add(NmSpdTagBase.TAG_YOSAN_INSU, "総数")
        'result.Add(NmSpdTagBase.TAG_YOSAN_MAKER_CODE, "取引先コード")
        'result.Add(NmSpdTagBase.TAG_YOSAN_KYOUKU_SECTION, "供給セクション")
        'result.Add(NmSpdTagBase.TAG_YOSAN_KOUTAN, "購坦")
        'result.Add(NmSpdTagBase.TAG_YOSAN_TEHAI_KIGOU, "手配記号")
        'result.Add(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_SEISAKU, "製作方法")
        'result.Add(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_1, "型仕様1")
        'result.Add(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_2, "型仕様2")
        'result.Add(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_3, "型仕様3")
        'result.Add(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_TIGU, "治具")
        'result.Add(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KIBO, "部品製作規模・概要")
        'result.Add(NmSpdTagBase.TAG_YOSAN_SHISAKU_BUHIN_HI, "試作部品費(円)")
        'result.Add(NmSpdTagBase.TAG_YOSAN_SHISAKU_KATA_HI, "試作型費(千円)")
        'result.Add(NmSpdTagBase.TAG_YOSAN_BUHIN_NOTE, "NOTE")
        'result.Add(NmSpdTagBase.TAG_YOSAN_BIKOU, "備考")
        'result.Add(NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN, "国外区分")
        'result.Add(NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI, "MIXｺｽﾄ部品費(円/ｾﾝﾄ)")
        'result.Add(NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI, "引用元情報")
        'result.Add(NmSpdTagBase.TAG_YOSAN_KONKYO_KEISU_1, "係数1")
        'result.Add(NmSpdTagBase.TAG_YOSAN_KONKYO_KOUHOU, "工法")
        'result.Add(NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI, "割付予算部品費(円)")
        'result.Add(NmSpdTagBase.TAG_YOSAN_WARITUKE_KEISU_2, "係数2")

        'result.Add(NmSpdTagBase.TAG_KAKO_1_RYOSAN_TANKA, "量産単価(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_1_WARITUKE_BUHIN_HI, "割付部品費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_1_WARITUKE_KATA_HI, "割付型費(千円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_1_WARITUKE_KOUHOU, "割付工法")
        'result.Add(NmSpdTagBase.TAG_KAKO_1_MAKER_BUHIN_HI, "ﾒｰｶｰ値部品費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_1_MAKER_KATA_HI, "ﾒｰｶｰ値型費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_1_MAKER_KOUHOU, "ﾒｰｶｰ値工法")
        'result.Add(NmSpdTagBase.TAG_KAKO_1_SHINGI_BUHIN_HI, "審議値部品費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_1_SHINGI_KATA_HI, "審議値型費(千円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_1_SHINGI_KOUHOU, "審議値工法")
        'result.Add(NmSpdTagBase.TAG_KAKO_1_KOUNYU_KIBOU_TANKA, "購入希望単価(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_1_KOUNYU_TANKA, "購入単価(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_1_SHIKYU_HIN, "支給品(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_1_KOUJI_SHIREI_NO, "工事指令№")
        'result.Add(NmSpdTagBase.TAG_KAKO_1_EVENT_NAME, "イベント名称")
        'result.Add(NmSpdTagBase.TAG_KAKO_1_HACHU_BI, "発注日")
        'result.Add(NmSpdTagBase.TAG_KAKO_1_KENSHU_BI, "検収日")

        'result.Add(NmSpdTagBase.TAG_KAKO_2_RYOSAN_TANKA, "量産単価(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_2_WARITUKE_BUHIN_HI, "割付部品費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_2_WARITUKE_KATA_HI, "割付型費(千円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_2_WARITUKE_KOUHOU, "割付工法")
        'result.Add(NmSpdTagBase.TAG_KAKO_2_MAKER_BUHIN_HI, "ﾒｰｶｰ値部品費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_2_MAKER_KATA_HI, "ﾒｰｶｰ値型費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_2_MAKER_KOUHOU, "ﾒｰｶｰ値工法")
        'result.Add(NmSpdTagBase.TAG_KAKO_2_SHINGI_BUHIN_HI, "審議値部品費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_2_SHINGI_KATA_HI, "審議値型費(千円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_2_SHINGI_KOUHOU, "審議値工法")
        'result.Add(NmSpdTagBase.TAG_KAKO_2_KOUNYU_KIBOU_TANKA, "購入希望単価(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_2_KOUNYU_TANKA, "購入単価(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_2_SHIKYU_HIN, "支給品(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_2_KOUJI_SHIREI_NO, "工事指令№")
        'result.Add(NmSpdTagBase.TAG_KAKO_2_EVENT_NAME, "イベント名称")
        'result.Add(NmSpdTagBase.TAG_KAKO_2_HACHU_BI, "発注日")
        'result.Add(NmSpdTagBase.TAG_KAKO_2_KENSHU_BI, "検収日")

        'result.Add(NmSpdTagBase.TAG_KAKO_3_RYOSAN_TANKA, "量産単価(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_3_WARITUKE_BUHIN_HI, "割付部品費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_3_WARITUKE_KATA_HI, "割付型費(千円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_3_WARITUKE_KOUHOU, "割付工法")
        'result.Add(NmSpdTagBase.TAG_KAKO_3_MAKER_BUHIN_HI, "ﾒｰｶｰ値部品費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_3_MAKER_KATA_HI, "ﾒｰｶｰ値型費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_3_MAKER_KOUHOU, "ﾒｰｶｰ値工法")
        'result.Add(NmSpdTagBase.TAG_KAKO_3_SHINGI_BUHIN_HI, "審議値部品費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_3_SHINGI_KATA_HI, "審議値型費(千円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_3_SHINGI_KOUHOU, "審議値工法")
        'result.Add(NmSpdTagBase.TAG_KAKO_3_KOUNYU_KIBOU_TANKA, "購入希望単価(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_3_KOUNYU_TANKA, "購入単価(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_3_SHIKYU_HIN, "支給品(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_3_KOUJI_SHIREI_NO, "工事指令№")
        'result.Add(NmSpdTagBase.TAG_KAKO_3_EVENT_NAME, "イベント名称")
        'result.Add(NmSpdTagBase.TAG_KAKO_3_HACHU_BI, "発注日")
        'result.Add(NmSpdTagBase.TAG_KAKO_3_KENSHU_BI, "検収日")


        'result.Add(NmSpdTagBase.TAG_KAKO_4_RYOSAN_TANKA, "量産単価(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_4_WARITUKE_BUHIN_HI, "割付部品費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_4_WARITUKE_KATA_HI, "割付型費(千円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_4_WARITUKE_KOUHOU, "割付工法")
        'result.Add(NmSpdTagBase.TAG_KAKO_4_MAKER_BUHIN_HI, "ﾒｰｶｰ値部品費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_4_MAKER_KATA_HI, "ﾒｰｶｰ値型費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_4_MAKER_KOUHOU, "ﾒｰｶｰ値工法")
        'result.Add(NmSpdTagBase.TAG_KAKO_4_SHINGI_BUHIN_HI, "審議値部品費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_4_SHINGI_KATA_HI, "審議値型費(千円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_4_SHINGI_KOUHOU, "審議値工法")
        'result.Add(NmSpdTagBase.TAG_KAKO_4_KOUNYU_KIBOU_TANKA, "購入希望単価(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_4_KOUNYU_TANKA, "購入単価(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_4_SHIKYU_HIN, "支給品(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_4_KOUJI_SHIREI_NO, "工事指令№")
        'result.Add(NmSpdTagBase.TAG_KAKO_4_EVENT_NAME, "イベント名称")
        'result.Add(NmSpdTagBase.TAG_KAKO_4_HACHU_BI, "発注日")
        'result.Add(NmSpdTagBase.TAG_KAKO_4_KENSHU_BI, "検収日")


        'result.Add(NmSpdTagBase.TAG_KAKO_5_RYOSAN_TANKA, "量産単価(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_5_WARITUKE_BUHIN_HI, "割付部品費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_5_WARITUKE_KATA_HI, "割付型費(千円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_5_WARITUKE_KOUHOU, "割付工法")
        'result.Add(NmSpdTagBase.TAG_KAKO_5_MAKER_BUHIN_HI, "ﾒｰｶｰ値部品費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_5_MAKER_KATA_HI, "ﾒｰｶｰ値型費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_5_MAKER_KOUHOU, "ﾒｰｶｰ値工法")
        'result.Add(NmSpdTagBase.TAG_KAKO_5_SHINGI_BUHIN_HI, "審議値部品費(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_5_SHINGI_KATA_HI, "審議値型費(千円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_5_SHINGI_KOUHOU, "審議値工法")
        'result.Add(NmSpdTagBase.TAG_KAKO_5_KOUNYU_KIBOU_TANKA, "購入希望単価(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_5_KOUNYU_TANKA, "購入単価(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_5_SHIKYU_HIN, "支給品(円)")
        'result.Add(NmSpdTagBase.TAG_KAKO_5_KOUJI_SHIREI_NO, "工事指令№")
        'result.Add(NmSpdTagBase.TAG_KAKO_5_EVENT_NAME, "イベント名称")
        'result.Add(NmSpdTagBase.TAG_KAKO_5_HACHU_BI, "発注日")
        'result.Add(NmSpdTagBase.TAG_KAKO_5_KENSHU_BI, "検収日")

        'result.Add(NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI_TOTAL, "割付予算部品費合計(円)")
        'result.Add(NmSpdTagBase.TAG_YOSAN_WARITUKE_KATA_HI, "割付予算型費(千円)")
        'result.Add(NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_TANKA, "購入希望単価(円)")
        'result.Add(NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI, "購入希望部品費(円)")
        'result.Add(NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL, "購入希望部品費合計(円)")
        'result.Add(NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_KATA_HI, "購入希望型費(千円)")
        'result.Add(NmSpdTagBase.TAG_HENKATEN, "変化点")



        Return result
    End Function

    ''' <summary>
    ''' 列の情報用クラス
    ''' </summary>
    ''' <remarks></remarks>
    Private Class ColumnData
        Private _ColumnIndex As Integer
        Private _ColumnTag As String
        Private _ColumnName As String

        ''' <summary>
        ''' 列位置
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ColumnIndex() As Integer
            Get
                Return _ColumnIndex
            End Get
            Set(ByVal value As Integer)
                _ColumnIndex = value
            End Set
        End Property

        ''' <summary>
        ''' 列タグ名
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ColumnTag() As String
            Get
                Return _ColumnTag
            End Get
            Set(ByVal value As String)
                _ColumnTag = value
            End Set
        End Property

        ''' <summary>
        ''' 列名
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ColumnName() As String
            Get
                Return _ColumnName
            End Get
            Set(ByVal value As String)
                _ColumnName = value
            End Set
        End Property



    End Class



End Class
