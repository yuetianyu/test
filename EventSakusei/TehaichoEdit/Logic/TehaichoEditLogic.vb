Imports System
Imports EBom.Data
Imports EBom.Common
Imports FarPoint.Win
Imports EventSakusei.TehaichoEdit.Dao
Imports System.Text.RegularExpressions
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports EventSakusei.TehaichoEdit.Logic
Imports EventSakusei.NokiIkkatuSettei
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
Imports FarPoint.Win.Spread
Imports ShisakuCommon.Util.OptionFilter
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Ui.Spd
Imports FarPoint.Win.Spread.CellType
Imports Microsoft.Office.Interop
Imports System.IO
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports ShisakuCommon.Db.EBom

Imports System.Text
Imports System.Net
Imports System.Net.Mail

Namespace TehaichoEdit

#Region "手配帳編集画面制御ロジック"

    ''' <summary>
    ''' 画面制御ロジック
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TehaichoEditLogic

#Region "プレイベート変数"

        Private Const _TITLE_BASE As String = "基本情報"
        Private Const _TITLE_GOUSYA As String = "号車情報"
        Private Const _DUMMY_NAME As String = "DUMMY"
        Private Const _BLANK_NAME As String = "　"

        '''<summary>シート取込バックアップ名称号車</summary>>
        Private Const _BACKUP_SHEET_NAME_GOUSYA As String = "BackupGousya"
        '''<summary>シート取込バックアップ名称号車</summary>>
        Private Const _BACKUP_SHEET_NAME_BASE As String = "BackupBase"
        '''<summary>納期一括設定画面</summary>>
        Private _frmNoukiIkkatsu As NokiIkkatuSettei.Frm21DispNokiIkkatuSettei = Nothing
        '''<summary>前画面引継ぎ項目</summary>>
        Private _shisakuEventCode As String
        Private _shisakuListCode As String
        '''<summary>ヘッダー情報格納</summary>>
        Private _headerSubject As TehaichoEditHeader
        ''' <summary>試作手帳編集表示フォーム</summary>
        Private _frmDispTehaiEdit As frm20DispTehaichoEdit
        ''' <summary>試作イベントデータテーブル</summary>
        Private _dtShikuEvent As DataTable

        ''' <summary>号車名称リストデータテーブル (NmTDColGousyaNameListで列名参照)</summary>
        Public _dtGousyaNameList As DataTable

        ''' <summary>スプレッドに対して編集が行われたブロックリストを格納 </summary>
        Private _listEditBlock As List(Of String()) = New List(Of String())
        ''' <summary>スクロール行同期管理 </summary>
        Private _rowNoScroll As Integer = -1

        '''<summary>メタルブロックExcelの取り込みデータ</summary>>
        Private _MetaruBlockList As List(Of TShisakuTehaiKihonVo) = New List(Of TShisakuTehaiKihonVo)

        '↓↓↓2015/01/09 設計メモ更新処理を追加 TES)張 ADD BEGIN
        '''<summary>設計メモExcelの取り込みデータ</summary>>
        Private _SekkeiMemoList As List(Of TShisakuTehaiKihonVo) = New List(Of TShisakuTehaiKihonVo)
        Private _SekkeiMemoBlockNo As String = Nothing
        '↑↑↑2015/01/09 設計メモ更新処理を追加 TES)張 ADD END

        ''↓↓2015/01/22 データ支給依頼書追加 TES)劉 ADD BEGIN
        Private WordApp As Object
        Private WordDoc As Word.Document
        Private isOpened As Boolean = False
        Private _torihikisakiCode As String
        Private _deptName As String
        Private _makerCode As String
        Private _makerName As String
        Private _shisakuKaihatsuFugo As String
        Private _content As String
        Private _hosoku As String
        Private _riyuu As String
        ''↑↑2015/01/22 データ支給依頼書追加 TES)劉 ADD END

        Private _ZairyoSunpoList As List(Of TShisakuTehaiKihonVo) = New List(Of TShisakuTehaiKihonVo)
        Private _ZairyoSunpoX As Decimal
        Private _ZairyoSunpoY As Decimal
        Private _ZairyoSunpoZ As Decimal
        Private _ZairyoSunpoXY As Decimal
        Private _ZairyoSunpoXZ As Decimal
        Private _ZairyoSunpoYZ As Decimal

#Region "EXCEL出力・取込情報"
        ''' <summary>Excel出力でのタイトル行数</summary>
        Private Const EXCEL_TITLE_LINE_COUNT As Integer = 4
        ''' <summary>Excel取込でのデータ開始位置 （Excelﾀｲﾄﾙ行数 + ｽﾌﾟﾚｯﾄﾞﾀｲﾄﾙ行 + 1 ?</summary>
        Private Const EXCEL_IMPORT_DATA_START_ROW As Integer = 10

        Private Const XLS_COL_LISTCODE As Integer = 5
        Private Const XLS_ROW_LISTCODE As Integer = 1
        Private Const XLS_COL_EVENTCODE As Integer = 5
        Private Const XLS_ROW_EVENTCODE As Integer = 2
        Private Const XLS_COL_DATE As Integer = 5
        Private Const XLS_ROW_DATE As Integer = 3

        'SPREAD - TAG 名称一覧行位置
        Private Const XLS_TAG_NAME_ROW As Integer = 4
        'リストア用バックアップ情報
        Private _backupShisakuListCode As String = Nothing
        Private _backupShisakuEventCode As String = Nothing
        Private _backupListEditBlock As List(Of String()) = Nothing

#End Region

#Region "材料手配リスト・取込情報"
        ''' <summary>ブロック</summary>
        Private Const METARU_BLOCK As Integer = 2
        ''' <summary>部品名称1</summary>
        Private Const METARU_BUHINNAME1 As Integer = 3
        ''' <summary>部品名称2</summary>
        Private Const METARU_BUHINNAME2 As Integer = 4
        ''' <summary>部品名称3</summary>
        Private Const METARU_BUHINNAME3 As Integer = 5
        ''' <summary>部品名称4</summary>
        Private Const METARU_BUHINNAME4 As Integer = 6
        ''' <summary>部品名称5</summary>
        Private Const METARU_BUHINNAME5 As Integer = 7
        ''' <summary>部品名称6</summary>
        Private Const METARU_BUHINNAME6 As Integer = 8
        ''' <summary>部品名称7</summary>
        Private Const METARU_BUHINNAME7 As Integer = 9
        ''' <summary>レベル</summary>
        Private Const METARU_LEVEL As Integer = 16
        ''' <summary>部品番号</summary>
        Private Const METARU_BUHINNO As Integer = 17
        ''' <summary>材質・規格１</summary>
        Private Const METARU_ZAISHITU_KIKAKU_1 As Integer = 18
        ''' <summary>材質・規格２</summary>
        Private Const METARU_ZAISHITU_KIKAKU_2 As Integer = 19
        ''' <summary>材質・規格３</summary>
        Private Const METARU_ZAISHITU_KIKAKU_3 As Integer = 20
        ''' <summary>材質・規格４</summary>
        Private Const METARU_ZAISHITU_KIKAKU_4 As Integer = 21
        ''' <summary>板厚</summary>
        Private Const METARU_SHISAKU_BANKO_SURYO As Integer = 23
        ''' <summary>板厚・ｕ</summary>
        Private Const METARU_SHISAKU_BANKO_SURYO_U As Integer = 24
        ''' <summary>材料情報</summary>
        Private Const METARU_MATERIAL As Integer = 26
        ''' <summary>データ項目・エリア名</summary>
        Private Const METARU_DATAITEMAREANAME As Integer = 27
        ''' <summary>データ項目・セット名</summary>
        Private Const METARU_DATAITEMSETNAME As Integer = 28

#End Region

        '↓↓↓2015/01/09 設計メモ更新処理を追加 TES)張 ADD BEGIN
#Region "設計メモ・取込情報"
        Private Const SEKKEI_MEMO_XLS_COL_MEMO_TITLE As Integer = 4
        Private Const SEKKEI_MEMO_XLS_ROW_MEMO_TITLE As Integer = 5
        Private Const SEKKEI_MEMO_XLS_COL_MEMO_DATA As Integer = 4
        Private Const SEKKEI_MEMO_XLS_ROW_MEMO_DATA As Integer = 6

        ''' <summary>部品欄sheet名</summary>
        Private Const SEKKEI_MEMO_BUHIN_DATA_SHEET_NAME As String = "部品欄"
        ''' <summary>部品欄のデータ開始行</summary>
        Private Const SEKKEI_MEMO_DATA_START_ROW As Integer = 5
        ''' <summary>部品欄のデータ開始列</summary>
        Private Const SEKKEI_MEMO_DATA_START_COLUMN As Integer = 2
        ''' <summary>部品欄の列のカウント</summary>
        Private Const SEKKEI_MEMO_ALL_COLUMN_COUNT As Integer = 12
        ''' <summary>部品欄のタイトル列</summary>
        Private Const SEKKEI_MEMO_TITLE_ROW_1 As Integer = 2
        Private Const SEKKEI_MEMO_TITLE_ROW_2 As Integer = 3
        Private Const SEKKEI_MEMO_TITLE_ROW_3 As Integer = 4

        ''' <summary>No</summary>
        Private Const SEKKEI_MEMO_COLUMN_NO As Integer = 2
        ''' <summary>品番</summary>
        Private Const SEKKEI_MEMO_COLUMN_BUHIN_NO As Integer = 3
        ''' <summary>部品名称</summary>
        Private Const SEKKEI_MEMO_COLUMN_BUHIN_NAME As Integer = 4
        ''' <summary>板厚</summary>
        Private Const SEKKEI_MEMO_COLUMN_BANKO As Integer = 5
        ''' <summary>材質</summary>
        Private Const SEKKEI_MEMO_COLUMN_ZAISHITU As Integer = 6
        ''' <summary>AREA</summary>
        Private Const SEKKEI_MEMO_COLUMN_AREA As Integer = 7
        ''' <summary>FILE</summary>
        Private Const SEKKEI_MEMO_COLUMN_FILE As Integer = 8
        ''' <summary>試作型</summary>
        Private Const SEKKEI_MEMO_COLUMN_SHISAKU As Integer = 9
        ''' <summary>折曲げ</summary>
        Private Const SEKKEI_MEMO_COLUMN_OREMAGE As Integer = 10
        ''' <summary>ﾍﾞｰｽ品改修</summary>
        Private Const SEKKEI_MEMO_COLUMN_BASE_KAISYUU As Integer = 11
        ''' <summary>ﾍﾞｰｽ車</summary>
        Private Const SEKKEI_MEMO_COLUMN_BASE_GOSYA As Integer = 12
        ''' <summary>ﾍﾞｰｽ品番</summary>
        Private Const SEKKEI_MEMO_COLUMN_BASE_HINBAN As Integer = 13
#End Region
        '↑↑↑2015/01/09 設計メモ更新処理を追加 TES)張 ADD BEGIN

#End Region


#Region "材料寸法・取込情報"

        ''' <summary>部品欄sheet名</summary>
        Private Const ZAIRYO_SUNPO_BUHIN_DATA_SHEET_NAME As String = "部品欄"
        ''' <summary>部品欄のデータ開始行</summary>
        Private Const ZAIRYO_SUNPO_DATA_START_ROW As Integer = 5
        ''' <summary>部品欄のデータ開始列</summary>
        Private Const ZAIRYO_SUNPO_DATA_START_COLUMN As Integer = 2
        ''' <summary>部品欄の列のカウント</summary>
        Private Const ZAIRYO_SUNPO_ALL_COLUMN_COUNT As Integer = 13
        ''' <summary>部品欄のタイトル列</summary>
        Private Const ZAIRYO_SUNPO_TITLE_ROW_1 As Integer = 2
        Private Const ZAIRYO_SUNPO_TITLE_ROW_2 As Integer = 3
        Private Const ZAIRYO_SUNPO_TITLE_ROW_3 As Integer = 4

        ''' <summary>No</summary>
        Private Const ZAIRYO_SUNPO_COLUMN_NO As Integer = 2
        ''' <summary>品番</summary>
        Private Const ZAIRYO_SUNPO_COLUMN_BUHIN_NO As Integer = 3
        ''' <summary>部品名称</summary>
        Private Const ZAIRYO_SUNPO_COLUMN_BUHIN_NAME As Integer = 4
        ''' <summary>板厚</summary>
        Private Const ZAIRYO_SUNPO_COLUMN_BANKO As Integer = 5
        ''' <summary>材質</summary>
        Private Const ZAIRYO_SUNPO_COLUMN_ZAISHITU As Integer = 6
        ''' <summary>AREA</summary>
        Private Const ZAIRYO_SUNPO_COLUMN_AREA As Integer = 7
        ''' <summary>FILE</summary>
        Private Const ZAIRYO_SUNPO_COLUMN_FILE As Integer = 8
        ''' <summary>寸法_X</summary>
        Private Const ZAIRYO_SUNPO_COLUMN_X As Integer = 9
        ''' <summary>寸法_Y</summary>
        Private Const ZAIRYO_SUNPO_COLUMN_Y As Integer = 10
        ''' <summary>寸法_Z</summary>
        Private Const ZAIRYO_SUNPO_COLUMN_Z As Integer = 11
        ''' <summary>寸法_XY</summary>
        Private Const ZAIRYO_SUNPO_COLUMN_XY As Integer = 12
        ''' <summary>寸法_XZ</summary>
        Private Const ZAIRYO_SUNPO_COLUMN_XZ As Integer = 13
        ''' <summary>寸法_YZ</summary>
        Private Const ZAIRYO_SUNPO_COLUMN_YZ As Integer = 14
#End Region


#Region "コンストラクタ"
        Public Sub New(ByVal aShisakuEventCode As String, _
                                ByVal aShisakuListCode As String, _
                                ByVal frm As frm20DispTehaichoEdit, _
                                ByVal frmNoukiIkkatsu As NokiIkkatuSettei.Frm21DispNokiIkkatuSettei)

            '前画面引継
            _shisakuEventCode = aShisakuEventCode
            _shisakuListCode = aShisakuListCode

            '手配帳編集画面
            _frmDispTehaiEdit = frm

            '納期一括設定
            _frmNoukiIkkatsu = frmNoukiIkkatsu

            '表示更新
            _frmDispTehaiEdit.Refresh()


        End Sub
#End Region

#Region "プロパティ"

#Region "試作イベントコード"
        ''' <summary>
        ''' 試作イベントコード
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ShisakuEventCode()
            Get
                Return _shisakuEventCode
            End Get
        End Property
#End Region

#Region "試作リストコード"
        ''' <summary>
        ''' 試作リストコード
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ShisakuListCode()
            Get
                Return _shisakuListCode
            End Get
        End Property
#End Region

#Region "タイトル基本"
        ''' <summary>
        ''' タイトル基本
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property TitleBase() As String
            Get
                Return _TITLE_BASE
            End Get
        End Property
#End Region

#Region "タイトル号車"
        ''' <summary>
        ''' タイトル号車
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property TitleGousya() As String
            Get
                Return _TITLE_GOUSYA
            End Get
        End Property

#End Region

#Region "表示スプレット判定(基本)"
        ''' <summary>
        ''' 表示スプレット判定(基本)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IsBaseSpread() As Boolean
            Get
                If _frmDispTehaiEdit.spdBase.Visible = True Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property
#End Region

#Region "表示スプレット判定(号車)"
        ''' <summary>
        ''' 表示スプレット判定(号車)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IsGousyaSpread() As Boolean
            Get
                If _frmDispTehaiEdit.spdGouSya.Visible = True Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property
#End Region

#Region "表示されているシートオブジェクトを返す"
        ''' <summary>
        ''' 表示されているシートを返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetVisibleSheet() As Spread.SheetView
            Get
                If IsBaseSpread Then
                    Return _frmDispTehaiEdit.spdBase_Sheet1
                Else
                    Return _frmDispTehaiEdit.spdGouSya_Sheet1
                End If
            End Get
        End Property
#End Region

#Region "表示されているスプレッドを返す"
        ''' <summary>
        ''' 表示されているスプレッドを返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetVisibleSpread() As Spread.FpSpread
            Get
                If IsBaseSpread Then
                    Return _frmDispTehaiEdit.spdBase
                Else
                    Return _frmDispTehaiEdit.spdGouSya
                End If
            End Get
        End Property
#End Region

#Region "非表示にされているスプレッドオブジェクトを返す"
        ''' <summary>
        ''' 非表示にされているスプレッドかを返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetHiddenSpread() As Spread.FpSpread
            Get
                If IsBaseSpread Then
                    Return _frmDispTehaiEdit.spdGouSya
                Else
                    Return _frmDispTehaiEdit.spdBase
                End If
            End Get
        End Property
#End Region

#Region "非表示シートを返す"
        ''' <summary>
        ''' 非示されているのシートを返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetHiddenSheet() As Spread.SheetView
            Get
                If IsBaseSpread Then
                    Return _frmDispTehaiEdit.spdGouSya_Sheet1
                Else
                    Return _frmDispTehaiEdit.spdBase_Sheet1
                End If
            End Get
        End Property
#End Region

#Region "編集対象ブロックNo関連"
        ''' <summary>
        '''  編集ブロック格納リスト添字
        ''' </summary>
        ''' <remarks></remarks>
        Private Enum EnmlistEdit
            bukacode = 0
            blockNo = 1
        End Enum
#Region "編集したブロックNoを設定"
        ''' <summary>
        ''' 編集したブロックNoを設定
        ''' </summary>
        ''' <param name="aNo"></param>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property listEditBlock(Optional ByVal aNo As Integer = 0) As String
            Get
                Return _listEditBlock(aNo)(EnmlistEdit.blockNo)
            End Get

            Set(ByVal aBlockNo As String)

                '取得ブロックNoが未格納の場合は追加
                If IsEditBlockNo(aBlockNo) = False Then

                    If aBlockNo Is Nothing = False AndAlso aBlockNo.Trim.Equals(String.Empty) = False Then

                        Dim bukaCode As String = TehaichoEditImpl.FindBukaCode(aBlockNo)
                        Dim strRec() As String = {bukaCode, aBlockNo}

                        _listEditBlock.Add(strRec)
                    End If
                End If

            End Set
        End Property
#End Region

#Region "編集部課コード取得"
        ''' <summary>
        ''' 編集部課コード取得
        ''' </summary>
        ''' <param name="aNo"></param>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property listEditBukaCode(ByVal aNo As Integer) As String
            Get

                Return _listEditBlock(aNo)(EnmlistEdit.bukacode)

            End Get
        End Property
#End Region

#Region "編集ブロックリストの件数を取得"
        ''' <summary>
        ''' 編集ブロックリストの件数を取得
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ListEditCount() As Integer
            Get
                Return _listEditBlock.Count
            End Get
        End Property

#End Region

#Region "編集対象ブロックNoか判定"
        ''' <summary>
        ''' 編集対象ブロックNoか判定
        ''' </summary>
        ''' <param name="aBlockNo">探すブロックNo</param>
        ''' <value></value>
        ''' <returns>存在した場合はTRUEを返す</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IsEditBlockNo(ByVal aBlockNo As String) As Boolean
            Get
                Dim result As Boolean = False

                If StringUtil.IsEmpty(aBlockNo) Then
                    result = True
                    Return result
                End If

                For i As Integer = 0 To ListEditCount - 1
                    If aBlockNo.Trim.Equals(listEditBlock(i)) Then
                        result = True
                    End If
                Next


                Return result
            End Get
        End Property
#End Region

#End Region

#Region "シート間行同期変数"
        ''' <summary>
        ''' シート間行同期変数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property rowNoScroll()
            Get
                Return _rowNoScroll
            End Get
            Set(ByVal value)
                _rowNoScroll = value
            End Set
        End Property

#End Region

#End Region

#Region "メソッド"

#Region "ヘッダー部初期化"
        ''' <summary>
        '''ヘッダー部を初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitializeHeader()
            Dim frm As TehaichoEdit.frm20DispTehaichoEdit = _frmDispTehaiEdit

            'ヘッダー部分の初期設定'
            _headerSubject = New Logic.TehaichoEditHeader(_shisakuEventCode, _shisakuListCode)
            ShisakuFormUtil.setTitleVersion(_frmDispTehaiEdit)

            ''画面のPG-IDが表示されます。
            frm.LblCurrPGId.Text = "PG-ID :" + PROGRAM_ID_20

            ShisakuFormUtil.SetDateTimeNow(frm.LblDateNow, frm.LblTimeNow)
            ShisakuFormUtil.SetIdAndBuka(frm.LblCurrUserId, frm.LblCurrBukaName)


            ShisakuFormUtil.SettingDefaultProperty(frm.txtKoujiNo)
            frm.txtKoujiNo.MaxLength = 3
            ShisakuFormUtil.SettingDefaultProperty(frm.cmbBlockNo)


            'ヘッダー情報更新
            UpdateHeader()


            '基本ボタン押下
            VisibleButton(frm.BtnBase)

            '表示更新
            _frmDispTehaiEdit.Refresh()

            '↓↓↓2015/02/13 手配帳作成時のユニット区分で、材料の表示/非表示を切り替える機能を追加 daniel) ADD BEGIN
            If StringUtil.IsNotEmpty(_headerSubject.metalMTKbn) Then
                If Not _headerSubject.metalMTKbn.Equals("M") Then
                    'ユニット区分（UNIT_KBN）が"T"の場合、手配帳編集スプレッドの材料情報を非表示する。
                    _frmDispTehaiEdit.SetZaishituColumnDisable()
                End If
            End If
            '↑↑↑2015/02/13 手配帳作成時のユニット区分を取得、材料の表示/非表示を切り替える機能を追加 daniel) ADD END

        End Sub

#End Region

#Region "スプレッド初期化"
        ''' <summary>
        ''' スプレッド初期化
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitializeSpread()

            '試作イベント情報取得
            InitDtShisakuEvent()
            '号車名称リスト初期化
            InitDtGoushaNameList()
            'スプレッド見出しに号車列名設定
            Dim dtImpGousyaList As DataTable = Nothing
            InitSpreadColGousyaName(dtImpGousyaList)
            '号車列に表示された空白列を編集不可に設定
            LockedBlankCol()
            '日付列初期設定
            InitSpreadColDefaultDate()
            '常時非表示列の設定
            SetHiddenCol()

        End Sub


#End Region

#Region "メタル対応２次・納入指示日による背景色の制御"
        '納入指示日列か返す
        Public Function ChkNounyuShijibiCell(ByVal aCol As Integer) As Boolean
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            If GetTagIdx(sheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI) = aCol Then
                Return True
            Else
                Return False
            End If
        End Function

        'オール１の場合背景色をグレー、以外は解除
        '   （変化点が削以外のとき有効）
        Public Sub NounyuShijibiRowColorChange(ByVal aRow As Integer)
            Dim sheet As FarPoint.Win.Spread.SheetView
            Dim hidSheet As FarPoint.Win.Spread.SheetView
            sheet = GetVisibleSheet
            hidSheet = GetHiddenSheet

            If sheet.Cells(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_HENKATEN)).Value <> "削" Then
                If sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)) = "1111/11/11" Then
                    sheet.Rows(aRow).BackColor = Color.Gray
                    hidSheet.Rows(aRow).BackColor = Color.Gray
                Else
                    sheet.Rows(aRow).BackColor = Nothing
                    hidSheet.Rows(aRow).BackColor = Nothing
                End If
            End If
        End Sub

#End Region


#Region "各セルにカーソルキーでの移動をセットする "

        Public Sub CellArrowKey()

            Dim tc As New FarPoint.Win.Spread.CellType.TextCellType 'テキストの場合
            Dim cm As New FarPoint.Win.Spread.CellType.ComboBoxCellType 'コンボボックスの場合

            '基本情報のカーソルキー移動の設定
            Dim sheetK As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            '各セルにカーソルキーでの移動をセットする 
            For rowIndex As Integer = 0 To sheetK.RowCount - 1
                For colIndex = 1 To sheetK.ColumnCount - 1
                    Select Case TypeName(sheetK.Cells(rowIndex, colIndex).CellType)
                        Case "TextCellType"
                            tc = sheetK.Cells(rowIndex, colIndex).CellType
                            tc.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.None
                            sheetK.Cells(rowIndex, colIndex).CellType = tc
                        Case "ComboBoxCellType"
                            cm = sheetK.Cells(rowIndex, colIndex).CellType
                            cm.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.None
                            sheetK.Cells(rowIndex, colIndex).CellType = cm
                    End Select
                Next
            Next

            '号車情報のカーソルキー移動の設定
            Dim sheetG As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1
            '各セルにカーソルキーでの移動をセットする 
            For rowIndex As Integer = 0 To sheetG.RowCount - 1
                For colIndex = 1 To sheetG.ColumnCount - 1
                    Select Case TypeName(sheetG.Cells(rowIndex, colIndex).CellType)
                        Case "TextCellType"
                            tc = sheetG.Cells(rowIndex, colIndex).CellType
                            tc.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.None
                            sheetG.Cells(rowIndex, colIndex).CellType = tc
                        Case "ComboBoxCellType"
                            cm = sheetG.Cells(rowIndex, colIndex).CellType
                            cm.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.None
                            sheetG.Cells(rowIndex, colIndex).CellType = cm
                    End Select
                Next
            Next

        End Sub

#End Region

#Region "ヘッダー部の表示値を更新する"

        ''' <summary>
        ''' ヘッダー部の表示値を更新する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub UpdateHeader()

            Dim frm As TehaichoEdit.frm20DispTehaichoEdit = _frmDispTehaiEdit

            'ブロックNo頭出しコンボボックス
            FormUtil.BindLabelValuesToComboBox(frm.cmbBlockNo, _headerSubject.BlockNoLabelValues, True)
            FormUtil.SetComboBoxSelectedValue(frm.cmbBlockNo, _headerSubject.blockNo)

            frm.lblListCode.Text = _headerSubject.shisakuListCode
            frm.lblIbentoName.Text = _headerSubject.kaihatsuFugo + "  " + _headerSubject.listEventName
            frm.lblKoujishireiNo.Text = _headerSubject.koujiShireiNo
            frm.lblMTkubun.Text = _headerSubject.MTKbn
            frm.lblSeihinKubun.Text = _headerSubject.seihinKbn
            frm.txtKoujiNo.Text = _headerSubject.yosanCode

            'リスト改訂Noが001以降の場合はExcel取込機能は使用不可
            Dim kaiteiNo As String = _headerSubject.shisakuListCodeKaiteiNo

            If IsNumeric(kaiteiNo) = True Then
                If Integer.Parse(kaiteiNo) >= 1 Then
                    frm.btnExcelImport.Enabled = False
                End If
            End If

        End Sub

#End Region

#Region "スプレッド読込専用列設定"
        ''' <summary>
        ''' スプレッド読込専用列設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub LockColSpread()
            Dim sheet As FarPoint.Win.Spread.SheetView

            sheet = _frmDispTehaiEdit.spdBase_Sheet1

            sheet.Columns(NmSpdTagBase.TAG_RIREKI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_GYOU_ID).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_TOTAL_INSU_SURYO).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_HENKATEN).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_AUTO_ORIKOMI_KAITEI_NO).Locked = True
            '最終織込設変情報
            sheet.Columns(NmSpdTagBase.TAG_SAISYU_SETSUHEN_DATE).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_SAISYU_SETSUHEN_STSR_DHSTBA).Locked = True
            '自動計算結果
            sheet.Columns(NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ).Locked = True

            sheet = _frmDispTehaiEdit.spdGouSya_Sheet1
            sheet.Columns(NmSpdTagGousya.TAG_RIREKI).Locked = True
            sheet.Columns(NmSpdTagGousya.TAG_GYOU_ID).Locked = True
            sheet.Columns(NmSpdTagGousya.TAG_INSU_SA).Locked = True

        End Sub

#End Region

#Region "スプレッド日付列の初期日付設定"
        ''' <summary>
        ''' スプレッド日付列の初期日付設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitSpreadColDefaultDate()
            Dim sheet As Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1

            '納入指示日付
            Dim nounyuDate As New Spread.CellType.DateTimeCellType

            nounyuDate.DropDownButton = True
            nounyuDate.DateDefault = Now
            sheet.Columns(NmSpdTagBase.TAG_NOUNYU_SHIJIBI).CellType = nounyuDate

            '図面出図予定日
            Dim yoteiDate As New Spread.CellType.DateTimeCellType

            yoteiDate.DropDownButton = True
            yoteiDate.DateDefault = Now
            sheet.Columns(NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE).CellType = yoteiDate

            '↓↓↓2014/12/26 メタル項目を追加 TES)張 ADD BEGIN
            '出図実績日
            Dim jisekiDate As New Spread.CellType.DateTimeCellType

            jisekiDate.DropDownButton = True
            jisekiDate.DateDefault = Now
            sheet.Columns(NmSpdTagBase.TAG_SHUTUZU_JISEKI_DATE).CellType = jisekiDate

            '最終織込設変情報・日付
            Dim setsuhenDate As New Spread.CellType.DateTimeCellType

            setsuhenDate.DropDownButton = True
            setsuhenDate.DateDefault = Now
            sheet.Columns(NmSpdTagBase.TAG_SAISYU_SETSUHEN_DATE).CellType = setsuhenDate
            '↑↑↑2014/12/26 メタル項目を追加 TES)張 ADD END
        End Sub
#End Region

#Region "基本・号車画面状態切替"
        ''' <summary>
        ''' 基本・号車画面状態切替
        ''' （渡されたボタンにより判定）
        ''' </summary>
        ''' <param name="btn"></param>
        ''' <remarks></remarks>
        Public Sub VisibleButton(ByVal btn As Button)

            '表示シート・非表示シート間で共通列のデータ同期を行う
            SheetDataSync()

            Dim frm As TehaichoEdit.frm20DispTehaichoEdit = _frmDispTehaiEdit

            Dim actRow As Integer = 0

            With _frmDispTehaiEdit

                .spdBase.Visible = False
                .spdGouSya.Visible = False

                .BtnBase.BackColor = Color.LightCyan
                .BtnBase.ForeColor = Color.Black
                .BtnGouSya.BackColor = Color.LightCyan
                .BtnGouSya.ForeColor = Color.Black

                If .BtnBase.Equals(btn) Then
                    '基本情報に切替
                    .LblTitle.Visible = True
                    .LblTitle.Text = _TITLE_BASE
                    btn.BackColor = Color.Yellow
                    btn.ForeColor = Color.Black
                    .spdBase.Visible = True
                    ''↓↓2014/08/04 Ⅰ.9.過去発注情報引当機能_h) (TES)施 ADD BEGIN
                    .過去発注情報引当ToolStripMenuItem.Enabled = True

                    ''↑↑2014/08/04 Ⅰ.9.過去発注情報引当機能_h) (TES)施 ADD END

                    'アクティブセル位置を調整1/25基本・号車でスクロール位置をあわせる処理を入れたためとりあえず不要に
                    .spdBase.SetViewportTopRow(0, rowNoScroll)
                    .spdBase.Focus()

                    '2011/02/28　柳沼修正
                    ' 号車情報のSPREADの選択セルの場所を特定します。
                    Dim sheet As Spread.SheetView = .spdGouSya.ActiveSheet
                    If rowNoScroll > sheet.ActiveRowIndex - 1 Then
                        ParaActRowIdx = rowNoScroll
                    Else
                        ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
                    End If

                    ' 基本情報SPREADへ号車情報と同じ行の部品番号へカーソルを設定する。
                    If ParaActRowIdx > 0 Then
                        Dim sheet2 As Spread.SheetView = .spdBase.ActiveSheet
                        .spdBase_Sheet1.SetActiveCell(ParaActRowIdx, GetTagIdx(sheet2, NmSpdTagGousya.TAG_BUHIN_NO))
                    End If

                ElseIf .BtnGouSya.Equals(btn) Then
                    '号車情報に切替
                    .LblTitle.Visible = True
                    .LblTitle.Text = _TITLE_GOUSYA
                    btn.BackColor = Color.Yellow
                    btn.ForeColor = Color.Black
                    .spdGouSya.Visible = True
                    ''↓↓2014/08/04 Ⅰ.9.過去発注情報引当機能_h) (TES)施 ADD BEGIN
                    .過去発注情報引当ToolStripMenuItem.Enabled = False
                    ''↑↑2014/08/04 Ⅰ.9.過去発注情報引当機能_h) (TES)施 ADD END

                    'アクティブセル位置を調整1/25基本・号車でスクロール位置をあわせる処理を入れたためとりあえず不要に
                    .spdGouSya.SetViewportTopRow(0, rowNoScroll)
                    .spdGouSya.Focus()

                    '2011/02/28　柳沼修正
                    ' 基本情報のSPREADの選択セルの場所を特定します。
                    Dim sheet As Spread.SheetView = .spdBase.ActiveSheet
                    If rowNoScroll > sheet.ActiveRowIndex - 1 Then
                        ParaActRowIdx = rowNoScroll
                    Else
                        ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
                    End If

                    ' 号車情報SPREADへ号車情報と同じ行の部品番号へカーソルを設定する。
                    If ParaActRowIdx > 0 Then
                        Dim sheet2 As Spread.SheetView = .spdGouSya.ActiveSheet
                        .spdGouSya_Sheet1.SetActiveCell(ParaActRowIdx, GetTagIdx(sheet2, NmSpdTagGousya.TAG_BUHIN_NO))
                    End If

                End If


            End With
        End Sub
#End Region

#Region "試作イベント情報取得"
        Private Sub InitDtShisakuEvent()

            '試作イベント情報を取得
            Dim dtEventInfo As DataTable = TehaichoEditImpl.FindPkShisakuEvent(_shisakuEventCode)

            '取得出来ない場合は抜ける
            If dtEventInfo.Rows.Count = 0 Then
                Throw New Exception("試作イベント情報の取得ができませんでした。処理を終了します。")
                Return
            End If

            _dtShikuEvent = dtEventInfo

        End Sub

#End Region

#Region "コンボボックス生成"

#Region "集計コード"
        ''' <summary>
        ''' 集計コード
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AddCbShukeiCode()

            '集計マスタからデータ取得
            Dim dtShukei As DataTable
            dtShukei = TehaichoEditImpl.FindAllSyukeiCodeInfo()

            '集計コードが取得出来なければ処理を上位に戻す
            If dtShukei.Rows.Count = 0 Then
                Return
            End If

            Dim cb As New Spread.CellType.ComboBoxCellType
            Dim keys As New List(Of String)
            Dim vals As New List(Of String)

            '' 矢印キー
            cb.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.Arrows
            '' 文字数
            cb.MaxLength = 1
            '' 入力時、前方一致
            cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cb.AutoCompleteSource = AutoCompleteSource.ListItems
            '' 小文字を大文字にする
            cb.CharacterCasing = CharacterCasing.Upper
            '' 半角英数記号(Asciiだと全角も入力出来る)
            cb.CharacterSet = CellType.CharacterSet.Ascii
            '' 編集可能にする
            cb.Editable = True

            For i As Integer = 0 To dtShukei.Rows.Count - 1
                keys.Add(dtShukei.Rows(i)(NmSyukei.SYUKEI_CODE))
                vals.Add(dtShukei.Rows(i)(NmSyukei.SYUKEI_NAME))
            Next

            '空白をセット
            keys.Add(String.Empty)
            vals.Add(String.Empty)

            'キーも表示も集計コードとする
            cb.ItemData = keys.ToArray
            cb.Items = keys.ToArray

            _frmDispTehaiEdit.spdBase_Sheet1.Columns(NmSpdTagBase.TAG_SHUKEI_CODE).CellType = cb

        End Sub

#End Region

#Region "手配記号"
        ''' <summary>
        '''  手配記号
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AddCbTehaiKigou()

            Dim cb As New Spread.CellType.ComboBoxCellType
            Dim keys As New List(Of String)
            Dim vals As New List(Of String)

            Dim dtTehaiKigou As New DataTable

            '' 矢印キー
            cb.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.Arrows
            '' 文字数
            cb.MaxLength = 1
            '' 入力時、前方一致
            cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cb.AutoCompleteSource = AutoCompleteSource.ListItems
            '' 小文字を大文字にする
            cb.CharacterCasing = CharacterCasing.Upper
            '' 半角英数記号(Asciiだと全角も入力出来る)
            cb.CharacterSet = CellType.CharacterSet.Ascii
            '' 編集可能にする
            cb.Editable = True


            dtTehaiKigou = TehaichoEditImpl.FindAllTehaiKigou()

            'データ未発見時
            If Not dtTehaiKigou.Rows.Count >= 1 Then
                ComFunc.ShowErrMsgBox("手配記号マスタ(AS_ARPF04)からデータが取得出来ませんでした")
            End If

            For i As Integer = 0 To dtTehaiKigou.Rows.Count - 1
                '仮の値を設定
                Dim tehaiKigou As String = dtTehaiKigou.Rows(i)(NmTDColTehaiKigou.TEAHAI_KIGOU).ToString.Trim

                If tehaiKigou.Equals(String.Empty) Then
                    tehaiKigou = " "
                End If

                keys.Add(tehaiKigou)
                vals.Add(String.Empty)
            Next

            'キー値を表示
            cb.ItemData = keys.ToArray
            cb.Items = keys.ToArray

            'スプレッドへ設定
            _frmDispTehaiEdit.spdBase_Sheet1.Columns(NmSpdTagBase.TAG_TEHAI_KIGOU).CellType = cb

        End Sub
#End Region

#Region "再使用不可（という）項目"
        ''' <summary>
        ''' 再使用不可（という）項目
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AddCbSaishiyouFuka()
            Dim cb As New Spread.CellType.ComboBoxCellType
            Dim keys As New List(Of String)

            '' 矢印キー
            cb.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.Arrows
            '' 文字数
            cb.MaxLength = 1
            '' 入力時、前方一致
            cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cb.AutoCompleteSource = AutoCompleteSource.ListItems
            '' 小文字を大文字にする
            cb.CharacterCasing = CharacterCasing.Upper
            '' 半角英数記号(Asciiだと全角も入力出来る)
            cb.CharacterSet = CellType.CharacterSet.Ascii
            '' 編集可能にする
            cb.Editable = True

            '仮の値を設定
            keys.Add("A")

            keys.Add(" ")

            'キーを表示設定に
            cb.ItemData = keys.ToArray
            cb.Items = keys.ToArray

            'スプレッドへ設定
            _frmDispTehaiEdit.spdBase_Sheet1.Columns(NmSpdTagBase.TAG_SAISHIYOUFUKA).CellType = cb

        End Sub

#End Region

        ''↓↓2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
#Region "作り方製作方法"
        ''' <summary>
        ''' 作り方製作方法
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AddCbTsukurikataSeisaku()

            '集計マスタからデータ取得
            Dim dtTsukurikata As DataTable
            dtTsukurikata = TehaichoEditImpl.FindAllTsukurikataSeisaku()

            '集計コードが取得出来なければ処理を上位に戻す
            If dtTsukurikata.Rows.Count = 0 Then
                Return
            End If

            Dim cb As New Spread.CellType.ComboBoxCellType
            Dim keys As New List(Of String)
            Dim vals As New List(Of String)

            '' 矢印キー
            cb.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.Arrows
            '' 文字数
            ''↓↓2014/08/27 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            'cb.MaxLength = 1
            cb.MaxLength = 32
            ''↑↑2014/08/27 Ⅰ.2.管理項目追加 酒井 ADD END
            '' 入力時、前方一致
            cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cb.AutoCompleteSource = AutoCompleteSource.ListItems
            '' 小文字を大文字にする
            cb.CharacterCasing = CharacterCasing.Upper
            '' 半角英数記号(Asciiだと全角も入力出来る)
            cb.CharacterSet = CellType.CharacterSet.Ascii
            '' 編集可能にする
            cb.Editable = True

            ''↓↓2014/09/03 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            '空白をセット
            keys.Add(String.Empty)
            vals.Add(String.Empty)
            ''↑↑2014/09/03 Ⅰ.2.管理項目追加 酒井 ADD END

            For i As Integer = 0 To dtTsukurikata.Rows.Count - 1
                keys.Add(dtTsukurikata.Rows(i)(NmTsukurikata.TSUKURIKATA_NAME))
                'vals.Add(dtTsukurikata.Rows(i)(NmTsukurikata.TSUKURIKATA_NAME))
            Next

            ''↓↓2014/09/03 Ⅰ.2.管理項目追加 酒井 DEL BEGIN
            '空白をセット
            'keys.Add(String.Empty)
            'vals.Add(String.Empty)
            ''↑↑2014/09/03 Ⅰ.2.管理項目追加 酒井 DEL END

            'キーも表示も集計コードとする
            cb.ItemData = keys.ToArray
            cb.Items = keys.ToArray

            _frmDispTehaiEdit.spdBase_Sheet1.Columns(NmSpdTagBase.TAG_TSUKURIKATA_SEISAKU).CellType = cb

        End Sub

#End Region
#Region "作り方型仕様方法"
        ''' <summary>
        ''' 作り方型仕様方法
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AddCbTsukurikataKatashiyou()

            '集計マスタからデータ取得
            Dim dtTsukurikata As DataTable
            dtTsukurikata = TehaichoEditImpl.FindAllTsukurikataKatashiyou()

            '集計コードが取得出来なければ処理を上位に戻す
            If dtTsukurikata.Rows.Count = 0 Then
                Return
            End If

            Dim cb As New Spread.CellType.ComboBoxCellType
            Dim keys As New List(Of String)
            Dim vals As New List(Of String)

            '' 矢印キー
            cb.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.Arrows
            '' 文字数
            ''↓↓2014/08/27 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            'cb.MaxLength = 1
            cb.MaxLength = 32
            ''↑↑2014/08/27 Ⅰ.2.管理項目追加 酒井 ADD END
            '' 入力時、前方一致
            cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cb.AutoCompleteSource = AutoCompleteSource.ListItems
            '' 小文字を大文字にする
            cb.CharacterCasing = CharacterCasing.Upper
            '' 半角英数記号(Asciiだと全角も入力出来る)
            cb.CharacterSet = CellType.CharacterSet.Ascii
            '' 編集可能にする
            cb.Editable = True

            ''↓↓2014/09/03 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            '空白をセット
            keys.Add(String.Empty)
            vals.Add(String.Empty)
            ''↑↑2014/09/03 Ⅰ.2.管理項目追加 酒井 ADD END

            For i As Integer = 0 To dtTsukurikata.Rows.Count - 1
                keys.Add(dtTsukurikata.Rows(i)(NmTsukurikata.TSUKURIKATA_NAME))
                'vals.Add(dtTsukurikata.Rows(i)(NmTsukurikata.TSUKURIKATA_NAME))
            Next

            ''↓↓2014/09/03 Ⅰ.2.管理項目追加 酒井 DEL BEGIN
            '空白をセット
            'keys.Add(String.Empty)
            'vals.Add(String.Empty)
            ''↑↑2014/09/03 Ⅰ.2.管理項目追加 酒井 DEL END

            'キーも表示も集計コードとする
            cb.ItemData = keys.ToArray
            cb.Items = keys.ToArray

            _frmDispTehaiEdit.spdBase_Sheet1.Columns(NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_1).CellType = cb
            _frmDispTehaiEdit.spdBase_Sheet1.Columns(NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_2).CellType = cb
            _frmDispTehaiEdit.spdBase_Sheet1.Columns(NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_3).CellType = cb

        End Sub

#End Region
#Region "作り方治具方法"
        ''' <summary>
        ''' 作り方治具方法
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AddCbTsukurikataTigu()

            '集計マスタからデータ取得
            Dim dtTsukurikata As DataTable
            dtTsukurikata = TehaichoEditImpl.FindAllTsukurikataTigu()

            '集計コードが取得出来なければ処理を上位に戻す
            If dtTsukurikata.Rows.Count = 0 Then
                Return
            End If

            Dim cb As New Spread.CellType.ComboBoxCellType
            Dim keys As New List(Of String)
            Dim vals As New List(Of String)

            '' 矢印キー
            cb.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.Arrows
            '' 文字数
            ''↓↓2014/08/27 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            'cb.MaxLength = 1
            cb.MaxLength = 32
            ''↑↑2014/08/27 Ⅰ.2.管理項目追加 酒井 ADD END
            '' 入力時、前方一致
            cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cb.AutoCompleteSource = AutoCompleteSource.ListItems
            '' 小文字を大文字にする
            cb.CharacterCasing = CharacterCasing.Upper
            '' 半角英数記号(Asciiだと全角も入力出来る)
            cb.CharacterSet = CellType.CharacterSet.Ascii
            '' 編集可能にする
            cb.Editable = True

            ''↓↓2014/09/03 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            '空白をセット
            keys.Add(String.Empty)
            vals.Add(String.Empty)
            ''↑↑2014/09/03 Ⅰ.2.管理項目追加 酒井 ADD END

            For i As Integer = 0 To dtTsukurikata.Rows.Count - 1
                keys.Add(dtTsukurikata.Rows(i)(NmTsukurikata.TSUKURIKATA_NAME))
                'vals.Add(dtTsukurikata.Rows(i)(NmTsukurikata.TSUKURIKATA_NO))
            Next

            ''↓↓2014/09/03 Ⅰ.2.管理項目追加 酒井 DEL BEGIN
            '空白をセット
            'keys.Add(String.Empty)
            'vals.Add(String.Empty)
            ''↑↑2014/09/03 Ⅰ.2.管理項目追加 酒井 DEL END

            'キーも表示も集計コードとする
            cb.ItemData = keys.ToArray
            cb.Items = keys.ToArray

            _frmDispTehaiEdit.spdBase_Sheet1.Columns(NmSpdTagBase.TAG_TSUKURIKATA_TIGU).CellType = cb

        End Sub

#End Region
        ''↑↑2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD END
#Region "新規ブロックNoリスト"
        ''' <summary>
        ''' 新規ブロックNoリスト
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitCbNewBlockList()
            Dim dtBlockList As New DataTable
            _frmDispTehaiEdit.cmbNewBlockNo.Items.Clear()
            dtBlockList = TehaichoEditImpl.FindAllListBlockNo()
            If dtBlockList.Rows.Count >= 1 Then
                'からデータを先頭に追加
                _frmDispTehaiEdit.cmbNewBlockNo.Items.Add(New ComboBoxItem(String.Empty, String.Empty))

                '取得件数分追加
                For i As Integer = 0 To dtBlockList.Rows.Count - 1

                    Dim blockNo As String = dtBlockList.Rows(i)(NmTDColRhac0080.BLOCK_NO_KINO).ToString

                    '追加
                    _frmDispTehaiEdit.cmbNewBlockNo.Items.Add(New ComboBoxItem(String.Empty, blockNo))

                Next

                'オートコンプリートモードの設定
                _frmDispTehaiEdit.cmbNewBlockNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                'コンボボックスのアイテムをオートコンプリートの選択候補とする
                _frmDispTehaiEdit.cmbNewBlockNo.AutoCompleteSource = AutoCompleteSource.ListItems

            End If

        End Sub

#End Region

#End Region

#Region "コンボボックス(ブロックNoの頭出し)"
        ''' <summary>
        ''' コンボボックス(ブロックNoの頭出し)
        ''' </summary>
        ''' <param name="aTitleName"></param>
        ''' <param name="aBlockNo"></param>
        ''' <remarks></remarks>
        Public Sub Spread_BlockNo_FocusChange(ByVal aTitleName As String, ByVal aBlockNo As String)

            Dim sheetBase As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim sheetGousya As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1

            Dim resRow As Integer = -1

            For i As Integer = GetTitleRowsIn(sheetBase) To sheetBase.RowCount - 1
                Dim spdBlockNo As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim

                If spdBlockNo.Equals(aBlockNo) AndAlso aBlockNo.Trim.Equals(String.Empty) = False Then
                    resRow = i
                    Exit For
                End If

            Next

            'ブロックが見つからない場合は先頭行をセット
            If resRow = -1 Then
                resRow = GetTitleRowsIn(sheetBase)
            End If

            'アクティブ位置を選択ブロックへ
            sheetBase.SetActiveCell(resRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO))
            sheetGousya.SetActiveCell(resRow, GetTagIdx(sheetGousya, NmSpdTagGousya.TAG_SHISAKU_BLOCK_NO))

            _frmDispTehaiEdit.spdBase.ShowActiveCell(Spread.VerticalPosition.Top, Spread.HorizontalPosition.Left)
            _frmDispTehaiEdit.spdGouSya.ShowActiveCell(Spread.VerticalPosition.Top, Spread.HorizontalPosition.Left)

        End Sub
#End Region

#Region "カラーセット"
        Public Shared Sub SetColor(ByVal spd As FarPoint.Win.Spread.FpSpread)
            Try
                ' 現在選択しているセル範囲を取得します。
                cr = spd.ActiveSheet.GetSelection(0)
                ' 指定した範囲内のテキストを取得します。
                s = spd.ActiveSheet.GetClip(cr.Row, cr.Column, cr.RowCount, cr.ColumnCount)
            Catch Exception As Exception
                'MessageBox.Show("選択されたセルがありません。")
                Exit Sub
            End Try

            'カラー定義
            Dim intROW As Integer
            Dim intCOLUMN As Integer
            Dim styleColor As New FarPoint.Win.Spread.NamedStyle()
            '範囲x,yを計算します。
            intROW = cr.RowCount + cr.Row - 1
            intCOLUMN = cr.ColumnCount + cr.Column - 1

            Select Case paraCOLOR
                Case "Pink"
                    spd.ActiveSheet.Cells.Item(cr.Row, cr.Column, intROW, intCOLUMN).BackColor = Color.Pink
                Case "Salmon"
                    spd.ActiveSheet.Cells.Item(cr.Row, cr.Column, intROW, intCOLUMN).BackColor = Color.Salmon
                Case "Bisque"
                    spd.ActiveSheet.Cells.Item(cr.Row, cr.Column, intROW, intCOLUMN).BackColor = Color.Bisque
                Case "LightSalmon"
                    spd.ActiveSheet.Cells.Item(cr.Row, cr.Column, intROW, intCOLUMN).BackColor = Color.LightSalmon
                Case "LightYellow"
                    spd.ActiveSheet.Cells.Item(cr.Row, cr.Column, intROW, intCOLUMN).BackColor = Color.LightYellow
                Case "Yellow"
                    spd.ActiveSheet.Cells.Item(cr.Row, cr.Column, intROW, intCOLUMN).BackColor = Color.Yellow
                Case "PaleGreen"
                    spd.ActiveSheet.Cells.Item(cr.Row, cr.Column, intROW, intCOLUMN).BackColor = Color.PaleGreen
                Case "Lime"
                    spd.ActiveSheet.Cells.Item(cr.Row, cr.Column, intROW, intCOLUMN).BackColor = Color.Lime
                Case "Aquamarine"
                    spd.ActiveSheet.Cells.Item(cr.Row, cr.Column, intROW, intCOLUMN).BackColor = Color.Aquamarine
                Case "Aqua"
                    spd.ActiveSheet.Cells.Item(cr.Row, cr.Column, intROW, intCOLUMN).BackColor = Color.Aqua
                Case "LightSteelBlue"
                    spd.ActiveSheet.Cells.Item(cr.Row, cr.Column, intROW, intCOLUMN).BackColor = Color.LightSteelBlue
                Case "RoyalBlue"
                    spd.ActiveSheet.Cells.Item(cr.Row, cr.Column, intROW, intCOLUMN).BackColor = Color.RoyalBlue
                Case "Plum"
                    spd.ActiveSheet.Cells.Item(cr.Row, cr.Column, intROW, intCOLUMN).BackColor = Color.Plum
                Case "Fuchsia"
                    spd.ActiveSheet.Cells.Item(cr.Row, cr.Column, intROW, intCOLUMN).BackColor = Color.Fuchsia
                Case "LightGray"
                    spd.ActiveSheet.Cells.Item(cr.Row, cr.Column, intROW, intCOLUMN).BackColor = Color.LightGray
                Case "Gray"
                    spd.ActiveSheet.Cells.Item(cr.Row, cr.Column, intROW, intCOLUMN).BackColor = Color.Gray
                Case Else
                    spd.ActiveSheet.Cells.Item(cr.Row, cr.Column, intROW, intCOLUMN).ResetBackColor()
            End Select
        End Sub

#End Region

#Region "号車列書式設定"
        ''' <summary>
        ''' 号車列書式設定
        ''' </summary>
        ''' <param name="aColNo"></param>
        ''' <remarks></remarks>
        Private Sub SetFormatGousyaCol(ByVal aColNo As Integer, Optional ByVal aCount As Integer = 1)
            Dim txtDataCell As New Spread.CellType.TextCellType
            Dim txtTitleCell As New Spread.CellType.TextCellType
            '
            Dim txtMTCell As New Spread.CellType.DateTimeCellType

            Dim sheet As SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1
            Dim setRow As Integer = GetTitleRowsIn(sheet) - 3

            For i As Integer = 0 To aCount - 1
                Dim setColNo As Integer = i + aColNo

                '列幅
                sheet.Columns(setColNo).Width = 25S
                'フォント
                sheet.Columns(setColNo).Font = New Font("MS UI Gothic", 9)

                '結合
                sheet.AddSpanCell(setRow, setColNo, 3, 1)
                'sheet.AddSpanCell(0, setColNo, 3, 1)

                'データ格納文字サイズ
                txtDataCell.MaxLength = 2
                sheet.Columns(setColNo).CellType = txtDataCell

                '見出しセル書式
                txtTitleCell.TextOrientation = TextOrientation.TextTopDown
                txtTitleCell.MaxLength = 255
                sheet.Cells(setRow, setColNo, setRow + 2, setColNo).CellType = txtTitleCell

                'セルロック
                sheet.Cells(0, setColNo, setRow + 3, setColNo).Locked = True
                'sheet.Cells(setRow, setColNo, setRow + 3, setColNo).Locked = True
                '縦書きに
                sheet.Cells(setRow, setColNo, setRow + 3, setColNo).VerticalAlignment = CellVerticalAlignment.Top

                '実線罫線
                sheet.Cells(setRow, setColNo, setRow, setColNo).Border = New FarPoint.Win.LineBorder(Color.Black, 1, False, False, False, True)
                '背景色
                sheet.Cells(setRow, setColNo, setRow + 3, setColNo).BackColor = SystemColors.Control
                '対象列全体を中央寄せ
                sheet.Columns(setColNo).VerticalAlignment = CellVerticalAlignment.Center
                sheet.Columns(setColNo).HorizontalAlignment = CellHorizontalAlignment.Center

                '横書きに、フォント、右寄せに
                txtMTCell.DropDownButton = False
                txtMTCell.DateDefault = Now
                txtMTCell.TextOrientation = TextOrientation.TextHorizontal
                txtMTCell.DateTimeFormat = DateTimeFormat.UserDefined
                txtMTCell.UserDefinedFormat = "MMdd"

                sheet.Cells(0, setColNo, 1, setColNo).CellType = txtMTCell
                sheet.Cells(0, setColNo, 1, setColNo).Font = New Font("MS UI Gothic", 7)
                sheet.Cells(0, setColNo, 1, setColNo).HorizontalAlignment = CellHorizontalAlignment.Right

            Next
        End Sub
#End Region

#Region "号車スプレッドの員数差列の右側に二重線を引く"
        ''' <summary>
        ''' 号車スプレッドの員数差の右に二重線を引く
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DoubleBorderInsuSa(ByVal aStartRow As Integer, ByVal aEndRow As Integer)

            Dim colInsuSa As Integer = GetTagIdx(_frmDispTehaiEdit.spdGouSya_Sheet1, NmSpdTagGousya.TAG_INSU_SA)
            Dim dblBder As FarPoint.Win.DoubleLineBorder = New FarPoint.Win.DoubleLineBorder(Color.Black, False, False, True, False)

            _frmDispTehaiEdit.spdGouSya_Sheet1.Cells(aStartRow, colInsuSa, aEndRow, colInsuSa).Border = dblBder

        End Sub
#End Region

#Region "号車列に表示された空白列を編集不可に設定"
        ''' <summary>
        ''' 号車列に表示された空白列を編集不可に設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub LockedBlankCol()
            Dim sheet As SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1
            Dim startGousyaCol As Integer = TehaichoEditNames.START_COLUMMN_GOUSYA_NAME

            For i As Integer = startGousyaCol To sheet.ColumnCount - 1
                '空白名称列判定
                Dim gousyaName As String = sheet.GetText(0, i).Trim

                'DUMMY列を探す
                If gousyaName.Equals(_DUMMY_NAME) Then
                    'DUMMYの前の列を空白列とみなし編集ロック
                    sheet.Columns(i - 1).Locked = True
                    Exit For
                End If

            Next

        End Sub
#End Region

#Region "号車列見出し設定"
        ''' <summary>
        ''' 号車列見出し設定
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitSpreadColGousyaName(ByVal aDtGousyaList As DataTable)

            'aDtGousyaListがNothingでなければ入れ替える。
            '　NotNothing時：EXCEL取込時
            If Not aDtGousyaList Is Nothing Then
                _dtGousyaNameList = aDtGousyaList
            End If

            '号車件数取得
            Dim gousyaCnt As Integer = 0
            gousyaCnt = _dtGousyaNameList.Rows.Count

            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1

            '開始号車列位置
            Dim stGousyaClmn As Integer = TehaichoEditNames.START_COLUMMN_GOUSYA_NAME

            '必要列数算出：号車名称件数 + 号車名称列開始スプレッド列位置
            Dim needColCnt As Integer = _dtGousyaNameList.Rows.Count + stGousyaClmn

            '必要列数挿入
            sheet.Columns.Add(stGousyaClmn, _dtGousyaNameList.Rows.Count)

            '列書式設定
            SetFormatGousyaCol(stGousyaClmn, _dtGousyaNameList.Rows.Count)

            ''列名拡張 85以上の列数に対応
            If needColCnt >= sheet.Columns.Count Then
                sheet.Columns.Count = needColCnt
            End If

            '列名を一旦クリアする
            For m As Integer = stGousyaClmn To sheet.Columns.Count - 1
                sheet.SetValue(0, m, String.Empty)
                sheet.Columns(m).Tag = String.Empty
            Next

            '号車を列見出しに割り当てる
            For i As Integer = 0 To gousyaCnt - 1
                sheet.SetValue(2, i + stGousyaClmn, _dtGousyaNameList.Rows(i)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME))
                sheet.Columns(i + stGousyaClmn).Tag = GetColNameShisakuGousya(i)

                'MT納入指示日を割り当てる
                Dim mDate As String = ConvDateInt8(IIf(IsDBNull(_dtGousyaNameList.Rows(i)(NmDTColGousyaNameList.TD_M_NOUNYU_SHIJIBI)), _
                                                       0, _dtGousyaNameList.Rows(i)(NmDTColGousyaNameList.TD_M_NOUNYU_SHIJIBI)))
                sheet.SetValue(0, i + stGousyaClmn, mDate)
                Dim tDate As String = ConvDateInt8(IIf(IsDBNull(_dtGousyaNameList.Rows(i)(NmDTColGousyaNameList.TD_T_NOUNYU_SHIJIBI)), _
                                                       0, _dtGousyaNameList.Rows(i)(NmDTColGousyaNameList.TD_T_NOUNYU_SHIJIBI)))
                sheet.SetValue(1, i + stGousyaClmn, tDate)

            Next

            '列数設定
            sheet.Columns.Count = _dtGousyaNameList.Rows.Count + stGousyaClmn

        End Sub

#End Region

#Region "号車列見出し設定（号車別納期設定用）"

#Region "スプレッド読込専用列設定"
        ''' <summary>
        ''' スプレッド読込専用列設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub LockColGousyaNoukiSetteiSpread(ByVal frm As frm20GousyaNoukiSettei)
            Dim sheet As FarPoint.Win.Spread.SheetView

            sheet = frm.spdGouSya_Sheet1

            sheet.Columns(NmSpdTagGousyaNoukiSettei.TAG_SHISAKU_GOUSYA).Locked = True

        End Sub

#End Region



        ''' <summary>
        ''' 号車列見出し設定（号車別納期設定用）
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitSpreadColGousyaNameNoukiSettei(ByVal aDtGousyaList As DataTable)

            'aDtGousyaListがNothingでなければ入れ替える。
            '　NotNothing時：EXCEL取込時
            If Not aDtGousyaList Is Nothing Then
                _dtGousyaNameList = aDtGousyaList
            End If

            '号車件数取得
            Dim gousyaCnt As Integer = 0
            gousyaCnt = _dtGousyaNameList.Rows.Count

            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1

            '開始号車列位置
            Dim stGousyaClmn As Integer = TehaichoEditNames.START_COLUMMN_GOUSYA_NAME

            '号車を列見出しに割り当てる
            For i As Integer = 0 To gousyaCnt - 1
                sheet.SetValue(2, i + stGousyaClmn, _dtGousyaNameList.Rows(i)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME))
                sheet.Columns(i + stGousyaClmn).Tag = GetColNameShisakuGousya(i)

                'MT納入指示日を割り当てる
                Dim mDate As String = ConvDateInt8(IIf(IsDBNull(_dtGousyaNameList.Rows(i)(NmDTColGousyaNameList.TD_M_NOUNYU_SHIJIBI)), _
                                                       0, _dtGousyaNameList.Rows(i)(NmDTColGousyaNameList.TD_M_NOUNYU_SHIJIBI)))
                sheet.SetValue(0, i + stGousyaClmn, mDate)
                Dim tDate As String = ConvDateInt8(IIf(IsDBNull(_dtGousyaNameList.Rows(i)(NmDTColGousyaNameList.TD_T_NOUNYU_SHIJIBI)), _
                                                       0, _dtGousyaNameList.Rows(i)(NmDTColGousyaNameList.TD_T_NOUNYU_SHIJIBI)))
                sheet.SetValue(1, i + stGousyaClmn, tDate)

            Next

            '編集対象ブロックを設定
            ExcelImpAddEditBlock()

        End Sub

#End Region

#Region "基本情報スプレッドデータ格納"
        ''' <summary>
        ''' 基本情報スプレッドデータ格納
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetSpreadBase() As Boolean

            Dim dtBase As DataTable = TehaichoEditImpl.FindAllBaseInfo _
                        (_shisakuEventCode, _shisakuListCode)

            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            'スプレッドを他の画面と同様に設定
            Dim spread As FarPoint.Win.Spread.FpSpread = _frmDispTehaiEdit.spdBase
            SpreadUtil.Initialize(spread)

            '取得したデータ件数によりスプレッドの総行数を拡張(+10は必要余裕行数）
            ''2015/09/22 変更 E.Ubukata 
            'If dtBase.Rows.Count >= sheet.RowCount + 10 Then
            If dtBase.Rows.Count >= sheet.RowCount Then
                sheet.RowCount = dtBase.Rows.Count + 10
            End If

            '全データ消去
            SpreadAllClear(sheet)

            '発注済のチェックボックスは使用不可（チェック不可）にする。
            sheet.Columns(GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)).Locked = True
            Dim oRow As Integer = startRow
            For Each dtRow As DataRow In dtBase.Rows

                'If Not dtRow(NmDTColBase.TD_SHISAKU_BLOCK_NO) = "840C" Then Continue For


                Dim rireki As String = IIf(IsDBNull(dtRow(NmDTColBase.TD_RIREKI)), String.Empty, dtRow(NmDTColBase.TD_RIREKI))
                '履歴の値によりセルの編集ロックを行う
                If rireki.Trim.Equals("*") Then
                    LockCellRirekiBase(oRow, True)
                End If

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_RIREKI)).Value = rireki
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_SHISAKU_BUKA_CODE)), String.Empty, dtRow(NmDTColBase.TD_SHISAKU_BUKA_CODE))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_SHISAKU_BLOCK_NO)), String.Empty, dtRow(NmDTColBase.TD_SHISAKU_BLOCK_NO))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_GYOU_ID)), String.Empty, dtRow(NmDTColBase.TD_GYOU_ID))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SENYOU_MARK)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_SENYOU_MARK)), String.Empty, dtRow(NmDTColBase.TD_SENYOU_MARK))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_LEVEL)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_LEVEL)), String.Empty, dtRow(NmDTColBase.TD_LEVEL))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN)), String.Empty, dtRow(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_BUHIN_NO)), String.Empty, dtRow(NmDTColBase.TD_BUHIN_NO))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_KBN)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_BUHIN_NO_KBN)), String.Empty, dtRow(NmDTColBase.TD_BUHIN_NO_KBN))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_KAITEI_NO)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_BUHIN_NO_KAITEI_NO)), String.Empty, dtRow(NmDTColBase.TD_BUHIN_NO_KAITEI_NO))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_EDA_BAN)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_EDA_BAN)), String.Empty, dtRow(NmDTColBase.TD_EDA_BAN))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NAME)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_BUHIN_NAME)), String.Empty, dtRow(NmDTColBase.TD_BUHIN_NAME).ToString.Trim)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUKEI_CODE)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_SHUKEI_CODE)), String.Empty, dtRow(NmDTColBase.TD_SHUKEI_CODE))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_TEHAI_KIGOU)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_TEHAI_KIGOU)), String.Empty, dtRow(NmDTColBase.TD_TEHAI_KIGOU))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KOUTAN)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_KOUTAN)), String.Empty, dtRow(NmDTColBase.TD_KOUTAN))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_TORIHIKISAKI_CODE)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_TORIHIKISAKI_CODE)), String.Empty, dtRow(NmDTColBase.TD_TORIHIKISAKI_CODE))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUBA)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_NOUBA)), String.Empty, dtRow(NmDTColBase.TD_NOUBA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KYOUKU_SECTION)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_KYOUKU_SECTION)), String.Empty, dtRow(NmDTColBase.TD_KYOUKU_SECTION))

                '納入指示日を文字型に変換(INT型8桁)
                Dim strNounyuShijibi As String = ConvDateInt8(IIf(IsDBNull(dtRow(NmDTColBase.TD_NOUNYU_SHIJIBI)), 0, dtRow(NmDTColBase.TD_NOUNYU_SHIJIBI)))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)).Value = strNounyuShijibi

                '合計員数-1は**表記とし、0値は０を設定(2011/02/03)
                Dim totalInsu As String = CnvIntStr(dtRow(NmDTColBase.TD_TOTAL_INSU_SURYO))

                '-1が来たら0を設定
                totalInsu = IIf(totalInsu.Equals("-1"), "0", totalInsu)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_TOTAL_INSU_SURYO)).Value = totalInsu

                '使用不可項目
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISHIYOUFUKA)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_SAISHIYOUFUKA)), String.Empty, dtRow(NmDTColBase.TD_SAISHIYOUFUKA))

                '出図予定日を文字型に変換(INT型8桁)
                Dim strShutuzuYoteiDate As String = ConvDateInt8(IIf(IsDBNull(dtRow(NmDTColBase.TD_SHUTUZU_YOTEI_DATE)), 0, dtRow(NmDTColBase.TD_SHUTUZU_YOTEI_DATE)))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE)).Value = strShutuzuYoteiDate

                '出図実績_日付
                Dim strShutuzuJisekiDate As String = ConvDateInt8(IIf(IsDBNull(dtRow(NmDTColBase.TD_SHUTUZU_JISEKI_DATE)), 0, dtRow(NmDTColBase.TD_SHUTUZU_JISEKI_DATE)))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_DATE)).Value = strShutuzuJisekiDate
                '出図実績_改訂№
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_SHUTUZU_JISEKI_KAITEI_NO)), String.Empty, dtRow(NmDTColBase.TD_SHUTUZU_JISEKI_KAITEI_NO))
                '出図実績_設通№
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_SHUTUZU_JISEKI_STSR_DHSTBA)), String.Empty, dtRow(NmDTColBase.TD_SHUTUZU_JISEKI_STSR_DHSTBA))
                '最終織込設変情報_日付
                Dim strSaisyuSetsuhenDate As String = ConvDateInt8(IIf(IsDBNull(dtRow(NmDTColBase.TD_SAISYU_SETSUHEN_DATE)), 0, dtRow(NmDTColBase.TD_SAISYU_SETSUHEN_DATE)))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_DATE)).Value = strSaisyuSetsuhenDate
                '最終織込設変情報_改訂№
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_SAISYU_SETSUHEN_KAITEI_NO)), String.Empty, dtRow(NmDTColBase.TD_SAISYU_SETSUHEN_KAITEI_NO))
                '最終織込設変情報_設通№
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_STSR_DHSTBA)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_SAISYU_SETSUHEN_STSR_DHSTBA)), String.Empty, dtRow(NmDTColBase.TD_SAISYU_SETSUHEN_STSR_DHSTBA))
                '材料寸法_X(mm)
                Dim strX As String = GetNumericDbField(dtRow(NmDTColBase.TD_ZAIRYO_SUNPO_X))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X)).Value = strX
                '材料寸法_Y(mm)
                Dim strY As String = GetNumericDbField(dtRow(NmDTColBase.TD_ZAIRYO_SUNPO_Y))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y)).Value = strY
                '材料寸法_Z(mm)
                Dim strZ As String = GetNumericDbField(dtRow(NmDTColBase.TD_ZAIRYO_SUNPO_Z))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z)).Value = strZ
                '材料寸法_X+Y(mm)
                Dim strXy As String = GetNumericDbField(dtRow(NmDTColBase.TD_ZAIRYO_SUNPO_XY))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY)).Value = strXy
                '材料寸法_X+Z(mm)
                Dim strXz As String = GetNumericDbField(dtRow(NmDTColBase.TD_ZAIRYO_SUNPO_XZ))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ)).Value = strXz
                '材料寸法_Y+Z(mm)
                Dim strYz As String = GetNumericDbField(dtRow(NmDTColBase.TD_ZAIRYO_SUNPO_YZ))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ)).Value = strYz

                '最大値の背景色を変更
                '   最大値のセルは太文字・背景色をグレーにする
                Dim sunpoXy As Double = 0
                If StringUtil.IsNotEmpty(dtRow(NmDTColBase.TD_ZAIRYO_SUNPO_XY)) Then
                    sunpoXy = CDec(dtRow(NmDTColBase.TD_ZAIRYO_SUNPO_XY))
                End If
                Dim sunpoXz As Double = 0
                If StringUtil.IsNotEmpty(dtRow(NmDTColBase.TD_ZAIRYO_SUNPO_XZ)) Then
                    sunpoXz = CDec(dtRow(NmDTColBase.TD_ZAIRYO_SUNPO_XZ))
                End If
                Dim sunpoYz As Double = 0
                If StringUtil.IsNotEmpty(dtRow(NmDTColBase.TD_ZAIRYO_SUNPO_YZ)) Then
                    sunpoYz = CDec(dtRow(NmDTColBase.TD_ZAIRYO_SUNPO_YZ))
                End If

                Dim sunpo(2) As Double
                sunpo(0) = sunpoXy
                sunpo(1) = sunpoXz
                sunpo(2) = sunpoYz
                Array.Sort(sunpo)
                Select Case sunpo(2)
                    Case 0
                        '最大値が0の場合スルー
                    Case sunpoXy
                        sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY), _
                                    oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY)).BackColor = Color.Yellow
                        sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY), _
                                    oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY)).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                    Case sunpoXz
                        sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ), _
                                    oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ)).BackColor = Color.Yellow
                        sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ), _
                                    oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ)).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                    Case sunpoYz
                        sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ), _
                                    oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ)).BackColor = Color.Yellow
                        sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ), _
                                    oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ)).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                End Select


                ''↓↓2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_TSUKURIKATA_SEISAKU)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_TSUKURIKATA_SEISAKU)), String.Empty, dtRow(NmDTColBase.TD_TSUKURIKATA_SEISAKU))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_1)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_TSUKURIKATA_KATASHIYOU_1)), String.Empty, dtRow(NmDTColBase.TD_TSUKURIKATA_KATASHIYOU_1))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_2)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_TSUKURIKATA_KATASHIYOU_2)), String.Empty, dtRow(NmDTColBase.TD_TSUKURIKATA_KATASHIYOU_2))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_3)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_TSUKURIKATA_KATASHIYOU_3)), String.Empty, dtRow(NmDTColBase.TD_TSUKURIKATA_KATASHIYOU_3))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_TSUKURIKATA_TIGU)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_TSUKURIKATA_TIGU)), String.Empty, dtRow(NmDTColBase.TD_TSUKURIKATA_TIGU))

                Dim strTSUKURIKATA_NOUNYU As String = ConvDateInt8(IIf(IsDBNull(dtRow(NmDTColBase.TD_TSUKURIKATA_NOUNYU)), 0, dtRow(NmDTColBase.TD_TSUKURIKATA_NOUNYU)))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_TSUKURIKATA_NOUNYU)).Value = strTSUKURIKATA_NOUNYU

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_TSUKURIKATA_KIBO)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_TSUKURIKATA_KIBO)), String.Empty, dtRow(NmDTColBase.TD_TSUKURIKATA_KIBO))
                ''↑↑2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD END


                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_ZAISHITU_KIKAKU_1)), String.Empty, dtRow(NmDTColBase.TD_ZAISHITU_KIKAKU_1))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_ZAISHITU_KIKAKU_2)), String.Empty, dtRow(NmDTColBase.TD_ZAISHITU_KIKAKU_2))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_ZAISHITU_KIKAKU_3)), String.Empty, dtRow(NmDTColBase.TD_ZAISHITU_KIKAKU_3))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_MEKKI)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_ZAISHITU_MEKKI)), String.Empty, dtRow(NmDTColBase.TD_ZAISHITU_MEKKI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_SHISAKU_BANKO_SURYO)), String.Empty, dtRow(NmDTColBase.TD_SHISAKU_BANKO_SURYO))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_SHISAKU_BANKO_SURYO_U)), String.Empty, dtRow(NmDTColBase.TD_SHISAKU_BANKO_SURYO_U))

                '部品費
                Dim strBuhinHi As String = GetNumericDbField(dtRow(NmDTColBase.TD_SHISAKU_BUHINN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BUHINN_HI)).Text = strBuhinHi
                '型費
                Dim strKatahi As String = GetNumericDbField(dtRow(NmDTColBase.TD_SHISAKU_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_KATA_HI)).Value = strKatahi

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_MAKER_CODE)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_MAKER_CODE)), String.Empty, dtRow(NmDTColBase.TD_MAKER_CODE))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_BIKOU)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_BIKOU)), String.Empty, dtRow(NmDTColBase.TD_BIKOU))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_OYA)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_BUHIN_NO_OYA)), String.Empty, dtRow(NmDTColBase.TD_BUHIN_NO_OYA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_KBN_OYA)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_BUHIN_NO_KBN_OYA)), String.Empty, dtRow(NmDTColBase.TD_BUHIN_NO_KBN_OYA))

                '変化点
                Dim henkaTen As String = dtRow(NmDTColBase.TD_HENKATEN).ToString.Trim
                If henkaTen Is Nothing Then
                    henkaTen = String.Empty
                ElseIf henkaTen.Equals("1") Then
                    henkaTen = "○"
                ElseIf henkaTen.Equals("2") Then
                    henkaTen = "△"
                End If
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_HENKATEN)).Value = henkaTen
                '自動織込み改訂No'
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_AUTO_ORIKOMI_KAITEI_NO)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_AUTO_ORIKOMI_KAITEI_NO)), String.Empty, dtRow(NmDTColBase.TD_AUTO_ORIKOMI_KAITEI_NO))

                '↓↓↓2014/12/26 メタル項目を追加 TES)張 ADD BEGIN
                '材料情報・製品長
                Dim strLength As String = GetNumericDbField(dtRow(NmDTColBase.TD_MATERIAL_INFO_LENGTH))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_LENGTH)).Value = strLength
                '材料情報・製品幅
                Dim strWidth As String = GetNumericDbField(dtRow(NmDTColBase.TD_MATERIAL_INFO_WIDTH))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_WIDTH)).Value = strWidth
                '材料情報・発注対象
                Dim strOrderTarget As String = IIf(IsDBNull(dtRow(NmDTColBase.TD_MATERIAL_INFO_ORDER_TARGET)), String.Empty, dtRow(NmDTColBase.TD_MATERIAL_INFO_ORDER_TARGET))
                Dim isOrderTarget As Boolean = False
                If StringUtil.IsNotEmpty(strOrderTarget) And strOrderTarget.Equals("1") Then
                    isOrderTarget = True
                End If
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET)).Value = isOrderTarget
                '材料情報・発注対象最終更新年月日
                Dim strOrderTargetDate As String = IIf(IsDBNull(dtRow(NmDTColBase.TD_MATERIAL_INFO_ORDER_TARGET_DATE)), String.Empty, dtRow(NmDTColBase.TD_MATERIAL_INFO_ORDER_TARGET_DATE))
                If StringUtil.IsNotEmpty(strOrderTargetDate) Then
                    strOrderTargetDate = CDate(strOrderTargetDate).ToString("yyyy-MM-dd")
                End If
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET)).Note = strOrderTargetDate
                '材料情報・発注済
                Dim strOrderChk As String = IIf(IsDBNull(dtRow(NmDTColBase.TD_MATERIAL_INFO_ORDER_CHK)), String.Empty, dtRow(NmDTColBase.TD_MATERIAL_INFO_ORDER_CHK))
                Dim isOrderChk As Boolean = False
                If StringUtil.IsNotEmpty(strOrderChk) And strOrderChk.Equals("1") Then
                    isOrderChk = True
                End If
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)).Value = isOrderChk
                '発注対象のチェックボックスのチェックを外した場合、発注済のチェックボックスは使用不可（チェック不可）にする。
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)).Locked = Not isOrderTarget
                '材料情報・発注済最終更新年月日
                Dim strOrderChkDate As String = IIf(IsDBNull(dtRow(NmDTColBase.TD_MATERIAL_INFO_ORDER_CHK_DATE)), String.Empty, dtRow(NmDTColBase.TD_MATERIAL_INFO_ORDER_CHK_DATE))
                If StringUtil.IsNotEmpty(strOrderChkDate) Then
                    strOrderChkDate = CDate(strOrderChkDate).ToString("yyyy-MM-dd")
                End If
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)).Note = strOrderChkDate
                'データ項目・改訂№
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_DATA_ITEM_KAITEI_NO)), String.Empty, dtRow(NmDTColBase.TD_DATA_ITEM_KAITEI_NO))
                'データ項目・エリア名
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_DATA_ITEM_AREA_NAME)), String.Empty, dtRow(NmDTColBase.TD_DATA_ITEM_AREA_NAME))
                'データ項目・セット名
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_DATA_ITEM_SET_NAME)), String.Empty, dtRow(NmDTColBase.TD_DATA_ITEM_SET_NAME))
                'データ項目・改訂情報
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_INFO)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_DATA_ITEM_KAITEI_INFO)), String.Empty, dtRow(NmDTColBase.TD_DATA_ITEM_KAITEI_INFO))
                'データ項目・データ支給チェック欄
                Dim strDataProvision As String = IIf(IsDBNull(dtRow(NmDTColBase.TD_DATA_ITEM_DATA_PROVISION)), String.Empty, dtRow(NmDTColBase.TD_DATA_ITEM_DATA_PROVISION))
                Dim isDataProvision As Boolean = False
                If StringUtil.IsNotEmpty(strDataProvision) And strDataProvision.Equals("1") Then
                    isDataProvision = True
                End If
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION)).Value = isDataProvision
                'データ項目・データ支給チェック欄最終更新年月日
                Dim strDataProvisionDate As String = IIf(IsDBNull(dtRow(NmDTColBase.TD_DATA_ITEM_DATA_PROVISION_DATE)), String.Empty, dtRow(NmDTColBase.TD_DATA_ITEM_DATA_PROVISION_DATE))
                If StringUtil.IsNotEmpty(strDataProvisionDate) Then
                    strDataProvisionDate = CDate(strDataProvisionDate).ToString("yyyy-MM-dd")
                End If
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION)).Note = strDataProvisionDate

                If henkaTen.Equals("3") Then
                    SetDeleteRowDisabled(sheet, oRow)
                End If
                '↑↑↑2014/12/26 メタル項目を追加 TES)張 ADD END

                '納入指示日がオール1の場合、グレーアウトする。(編集は可能)
                If dtRow(NmDTColBase.TD_NOUNYU_SHIJIBI) = 11111111 Then
                    sheet.Rows(oRow).BackColor = Color.Gray
                End If

                oRow += 1
            Next

            Return True
        End Function

#End Region

#Region "履歴用スプレッドセル編集ロック（基本情報)"
        ''' <summary>
        ''' 履歴用スプレッドセル編集ロック
        ''' </summary>
        ''' <param name="aRowNo"></param>
        ''' <param name="aLocked"></param>
        ''' <remarks></remarks>
        Private Sub LockCellRirekiBase(ByVal aRowNo As Integer, Optional ByVal aLocked As Boolean = True)
            Dim sheetBase As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim sheetGousha As SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1

            '専用
            sheetBase.Cells(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SENYOU_MARK)).Locked = aLocked

            'レベル
            sheetBase.Cells(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_LEVEL)).Locked = aLocked

            '部品番号
            sheetBase.Cells(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO)).Locked = aLocked

            '部品番号区分
            sheetBase.Cells(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KBN)).Locked = aLocked

            '集計コード
            sheetBase.Cells(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUKEI_CODE)).Locked = aLocked

            '部品名称
            sheetBase.Cells(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NAME)).Locked = aLocked

            '手配記号
            sheetBase.Cells(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TEHAI_KIGOU)).Locked = aLocked

            '購担
            sheetBase.Cells(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_KOUTAN)).Locked = aLocked

            '供給セクション
            sheetBase.Cells(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_KYOUKU_SECTION)).Locked = aLocked

            '納入指示日
            sheetBase.Cells(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)).Locked = aLocked

        End Sub
#End Region

#Region "履歴用スプレッドセル編集ロック（号車情報)"
        ''' <summary>
        ''' 履歴用スプレッドセル編集ロック（号車情報)
        ''' </summary>
        ''' <param name="aRowNo"></param>
        ''' <param name="aLocked"></param>
        ''' <remarks></remarks>
        Private Sub LockCellRirekiGosya(ByVal aRowNo As Integer, Optional ByVal aLocked As Boolean = True)

            Dim sheetGousha As SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1

            '専用
            sheetGousha.Cells(aRowNo, GetTagIdx(sheetGousha, NmSpdTagGousya.TAG_SENYOU_MARK)).Locked = aLocked

            'レベル
            sheetGousha.Cells(aRowNo, GetTagIdx(sheetGousha, NmSpdTagGousya.TAG_LEVEL)).Locked = aLocked

            '部品番号
            sheetGousha.Cells(aRowNo, GetTagIdx(sheetGousha, NmSpdTagGousya.TAG_BUHIN_NO)).Locked = aLocked

            '部品番号区分
            sheetGousha.Cells(aRowNo, GetTagIdx(sheetGousha, NmSpdTagGousya.TAG_BUHIN_NO_KBN)).Locked = aLocked

        End Sub
#End Region

#Region "履歴の設定有無によりコントロールの使用不可切替"

        ''' <summary>
        ''' 履歴の設定有無によりコントロールの使用不可切替
        ''' </summary>
        ''' <param name="aEnable"></param>
        ''' <remarks></remarks>
        Private Sub RirekiSwitchHeaderContorol(ByVal aEnable As Boolean, Optional ByVal aRowNo As Integer = 1)

            With _frmDispTehaiEdit

                '保存
                .btnHozon.Enabled = aEnable
                '戻る
                .btnBACK.Enabled = aEnable
                'Excel取込
                .btnExcelImport.Enabled = aEnable
                'Excel出力
                .btnExcelExport.Enabled = aEnable
                '基本ボタン
                .BtnBase.Enabled = aEnable

            End With

        End Sub

#End Region

#Region "号車情報スプレッドデータ格納"
        ''' <summary>
        ''' 号車情報スプレッドデータ格納
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetSpreadGousya() As Boolean

            '試作手配号車一覧取得
            Dim dtGousya As DataTable = TehaichoEditImpl.FindAllGousyaInfo _
                        (_shisakuEventCode, _shisakuListCode, _dtGousyaNameList)

            Dim sheetBase As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            'スプレッドを他の画面と同様に設定
            SpreadUtil.Initialize(_frmDispTehaiEdit.spdGouSya)

            '基本スプレッドと同じ行数に設定
            sheet.RowCount = _frmDispTehaiEdit.spdBase_Sheet1.RowCount

            '全データ消去
            SpreadAllClear(sheet)
            Dim oRow As Integer = startRow
            For Each dtRow As DataRow In dtGousya.Rows

                'If Not dtRow(NmDTColBase.TD_SHISAKU_BLOCK_NO) = "840C" Then Continue For

                '基本情報との共通項目
                Dim rireki As String = IIf(IsDBNull(dtRow(NmDTColBase.TD_RIREKI)), String.Empty, dtRow(NmDTColBase.TD_RIREKI))
                '履歴の値によりセルの編集ロックを行う
                If rireki.Trim.Equals("*") Then
                    LockCellRirekiGosya(oRow, True)
                End If
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagGousya.TAG_RIREKI)).Value = rireki
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagGousya.TAG_SHISAKU_BUKA_CODE)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_SHISAKU_BUKA_CODE)), String.Empty, dtRow(NmDTColBase.TD_SHISAKU_BUKA_CODE))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagGousya.TAG_SHISAKU_BLOCK_NO)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_SHISAKU_BLOCK_NO)), String.Empty, dtRow(NmDTColBase.TD_SHISAKU_BLOCK_NO))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagGousya.TAG_GYOU_ID)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_GYOU_ID)), String.Empty, dtRow(NmDTColBase.TD_GYOU_ID))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagGousya.TAG_SENYOU_MARK)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_SENYOU_MARK)), String.Empty, dtRow(NmDTColBase.TD_SENYOU_MARK))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagGousya.TAG_LEVEL)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_LEVEL)), String.Empty, dtRow(NmDTColBase.TD_LEVEL))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagGousya.TAG_BUHIN_NO_HYOUJI_JUN)).Value = IIf(IsDBNull(dtRow(NmDTColGousya.TD_BUHIN_NO_HYOUJI_JUN)), String.Empty, dtRow(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagGousya.TAG_BUHIN_NO)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_BUHIN_NO)), String.Empty, dtRow(NmDTColBase.TD_BUHIN_NO))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagGousya.TAG_BUHIN_NO_KBN)).Value = IIf(IsDBNull(dtRow(NmDTColBase.TD_BUHIN_NO_KBN)), String.Empty, dtRow(NmDTColBase.TD_BUHIN_NO_KBN))

                Dim gousyaInsuTotal As Integer = 0

                '号車変動列更新
                Dim j As Integer = 0
                For Each dtRowNm As DataRow In _dtGousyaNameList.Rows

                    'タグアクセス用
                    Dim tagGousyaName As String = GetColNameShisakuGousya(j)
                    '員数取得
                    Dim insu As String = dtRow(dtRowNm(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME))

                    If Not StringUtil.Equals(tagGousyaName, "DUMMY") Then
                        If insu.Equals("-1") Then
                            '員数を号車スプレッドにセット
                            sheet.Cells(oRow, GetTagIdx(sheet, tagGousyaName)).Value = "**"
                        Else
                            '0の場合は空白とする
                            If insu.Equals("0") Then
                                insu = String.Empty
                            End If
                            '員数を号車スプレッドにセット
                            sheet.Cells(oRow, GetTagIdx(sheet, tagGousyaName)).Value = insu
                        End If
                    Else

                        If insu.Equals("-1") Then
                            '員数を号車スプレッドにセット
                            sheet.Cells(oRow, GetTagIdx(sheet, "DUMMY")).Value = "**"
                        Else
                            '0の場合は空白とする
                            If insu.Equals("0") Then
                                insu = String.Empty
                            End If
                            '員数を号車スプレッドにセット
                            sheet.Cells(oRow, GetTagIdx(sheet, "DUMMY")).Value = insu
                        End If

                    End If

                    ''2015/09/28 移動 E.Ubukata
                    '' CalcInsuSa内で全カラムの員数を足しているので員数を全カラム出力してから計算すればよいのでループの外側へ移動
                    'Dim shRireki As String = sheet.GetText(oRow, GetTagIdx(sheet, NmSpdTagGousya.TAG_RIREKI)).Trim
                    ''履歴可否により員数差設定
                    'If shRireki.Equals("*") Then
                    '    ''員数差表示
                    '    CalcInsuSa(oRow, True)
                    'Else
                    '    ''員数差非表示・基本スプレッドに合計員数設定
                    '    CalcInsuSa(oRow, False)
                    'End If
                    j += 1
                Next

                '履歴可否により員数差設定
                If rireki.Trim.Equals("*") Then
                    ''員数差表示
                    CalcInsuSa(oRow, True)
                Else
                    ''員数差非表示・基本スプレッドに合計員数設定
                    CalcInsuSa(oRow, False)
                End If

                '納入指示日がオール1の場合、グレーアウトする。(編集は可能)
                If ConvInt8Date(sheetBase.Cells(oRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)).Value) = 11111111 Then
                    sheet.Rows(oRow).BackColor = Color.Gray
                End If

                oRow += 1
            Next

            ''員数差列
            'Dim colInsuSa As Integer = GetTagIdx(sheet, NmSpdTagGousya.TAG_INSU_SA)


            '員数差の右に二重線を引く
            DoubleBorderInsuSa(startRow, sheet.RowCount - 1)

            Return True

        End Function
#End Region


#Region "号車名リスト初期化"
        ''' <summary>
        ''' 号車名リスト初期化
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitDtGoushaNameList()

            '号車名称リストを取得
            Dim dtGousyaNameList As DataTable = TehaichoEditImpl.FindAllGousyaNameList(_shisakuEventCode, _shisakuListCode)

            '取得出来ない場合は抜ける
            If dtGousyaNameList.Rows.Count = 0 Then
                Throw New Exception("号車名称が取得出来ませんでした。処理を終了します。")

            End If

            Dim endRowNo As Integer = -1

            'DUMMY行の存在判定、無ければ空白行と共に設定
            'For i As Integer = 0 To dtGousyaNameList.Rows.Count - 1

            '    '2012/02/22 何故かCount-1ではなくCount回、回ろうとするので'... 行削除したらカウンタ-1しないとだめじゃね？
            '    If i <= dtGousyaNameList.Rows.Count - 1 Then
            '        Dim gousyaName As String = dtGousyaNameList.Rows(i)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME).ToString.Trim

            '        'DUMMY行はこの後空白を挿入するため面倒なので削除して空白と一緒に追加
            '        If gousyaName.Equals(_DUMMY_NAME) Then
            '            Dim remRow As DataRow = dtGousyaNameList.Rows(i)
            '            dtGousyaNameList.Rows.Remove(remRow)
            '            Continue For
            '        End If

            '        endRowNo = i
            '    End If
            'Next


            '2014/04/03 kabasawa'
            'ダミー列が複数だと先に進めないので'
            For i As Integer = dtGousyaNameList.Rows.Count - 1 To 0 Step -1
                Dim gousyaName As String = dtGousyaNameList.Rows(i)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME).ToString.Trim
                If gousyaName.Equals(_DUMMY_NAME) Then
                    Dim remRow As DataRow = dtGousyaNameList.Rows(i)
                    dtGousyaNameList.Rows.Remove(remRow)
                End If
            Next
            endRowNo = dtGousyaNameList.Rows.Count - 1


            '空白行とDUMMY行の追加
            Dim row As DataRow = dtGousyaNameList.NewRow

            '空白行追加
            endRowNo += 1
            row(NmDTColGousyaNameList.TD_SHISAKU_EVENT_CODE) = _headerSubject.shisakuEventCode
            row(NmDTColGousyaNameList.TD_HYOJIJUN_NO) = endRowNo.ToString
            row(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME) = _BLANK_NAME

            dtGousyaNameList.Rows.Add(row)

            'DUMMY追加
            endRowNo += 1
            row = dtGousyaNameList.NewRow
            row(NmDTColGousyaNameList.TD_SHISAKU_EVENT_CODE) = _headerSubject.shisakuEventCode
            row(NmDTColGousyaNameList.TD_HYOJIJUN_NO) = endRowNo.ToString
            row(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME) = _DUMMY_NAME
            dtGousyaNameList.Rows.Add(row)

            _dtGousyaNameList = dtGousyaNameList

        End Sub

#End Region

#Region "スプレッドのブロックグループの中で最大値を取得する"
        ''' <summary>
        ''' ブロックNoの中で最大値を取得する
        ''' 
        ''' ※取得出来なかった場合は-1を返す
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetSpreadMaxNoInnerBlock(ByVal aBlockNo As String, ByVal aTagName As String) As Integer

            '基本情報スプレッドをセット
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim workBlockNo As String = String.Empty
            Dim maxNo As Integer = -1
            Dim startDataRow As Integer = GetTitleRowsIn(sheet)
            Dim blockFindFlag As Boolean = False

            '該当ブロックNOの持つ最大行IDを取得する
            For i As Integer = startDataRow To sheet.Rows.Count - 1

                workBlockNo = sheet.Cells(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Text

                If Not workBlockNo.Equals(String.Empty) AndAlso aBlockNo = workBlockNo Then

                    Dim workNo As Integer = sheet.Cells(i, GetTagIdx(sheet, aTagName)).Value
                    '下行が大きいとは限らない為比較し最大値を採る
                    If workNo >= maxNo Then
                        maxNo = workNo
                    End If

                    blockFindFlag = True

                End If

            Next

            Return maxNo

        End Function

#End Region

#Region "テーブルとスプレッドの値を比較し最大行位置を返す"
        ''' <summary>
        ''' テーブルとスプレッドの値を比較し最大行位置を返す
        ''' </summary>
        ''' <param name="aBlockNo"></param>
        ''' <param name="aBukaCode"></param>
        ''' <param name="aMaxGyouId">存在する最大行IDを返す</param>
        ''' <param name="aMaxBuhinNoHyoujiJun">存在する最大部品表示順を返す</param>
        ''' <remarks></remarks>
        Private Sub GetMaxNo(ByVal aBukaCode As String, _
                                                    ByVal aBlockNo As String, _
                                                    ByRef aMaxGyouId As Integer, _
                                                    ByRef aMaxBuhinNoHyoujiJun As Integer)
            Dim dtBase As DataTable = Nothing

            '行IDの最大値を取得
            Dim maxSpreadGyouId As Integer = GetSpreadMaxNoInnerBlock(aBlockNo, NmSpdTagBase.TAG_GYOU_ID)
            '部品表示順の最大値を取得
            Dim maxSpreadBuhinHyoujiJun As Integer = GetSpreadMaxNoInnerBlock(aBlockNo, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN)

            '試作手配帳(基本)から最大行IDを取得
            dtBase = TehaichoEditImpl.FindMaxIDBaseInfo(_headerSubject.shisakuEventCode, _
                                                                                _shisakuListCode, _
                                                                                aBukaCode, _
                                                                                aBlockNo)
            Dim baseMaxGyouId As Integer = 0
            Dim baseMaxBuhinHyoujiJun As Integer = -1

            If dtBase.Rows.Count = 1 Then

                If Not dtBase.Rows(0)(NmDTColBase.TD_GYOU_ID) = Nothing Then
                    baseMaxGyouId = dtBase.Rows(0)(NmDTColBase.TD_GYOU_ID)
                End If

                If Not dtBase.Rows(0)(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN) = Nothing Then
                    baseMaxBuhinHyoujiJun = dtBase.Rows(0)(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN)
                End If

            Else
                '最大行IDがない場合は0を最大値とする
                baseMaxGyouId = 0
                baseMaxBuhinHyoujiJun = -1

            End If

            'スプレッド上とテーブルで最大行IDが大きい方を使用する
            If maxSpreadGyouId < baseMaxGyouId Then
                aMaxGyouId = baseMaxGyouId
            Else
                aMaxGyouId = maxSpreadGyouId
            End If

            'スプレッドとテーブルで最大部品表示順が大きい方を使用する
            If maxSpreadBuhinHyoujiJun < baseMaxBuhinHyoujiJun Then
                aMaxBuhinNoHyoujiJun = baseMaxBuhinHyoujiJun
            Else
                aMaxBuhinNoHyoujiJun = maxSpreadBuhinHyoujiJun
            End If
        End Sub

#End Region

#Region "行挿入処理"
        ''' <summary>
        ''' 行挿入処理
        ''' </summary>
        ''' <param name="aRowNo">行挿入位置</param>
        ''' <param name="aRowCount">挿入行数</param>
        ''' <remarks></remarks>
        Public Sub InsertSpread(ByVal aRowNo As Integer, Optional ByVal aRowCount As Integer = 1)
            Dim clickBlockNo As String = String.Empty
            Dim clickBukaCode As String = String.Empty

            '号車情報スプレッドも同じ処理をする
            Dim sheetGousya As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1
            '基本情報スプレッドをセット
            Dim sheetBase As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1

            Dim startRow As Integer = GetTitleRowsIn(sheetBase)

            'スプレッドの先頭行への行挿入は禁止
            If aRowNo.Equals(startRow) Then
                ComFunc.ShowInfoMsgBox("先頭行への行挿入は出来ません。")
                Exit Sub
            End If

            '※注意  ----- ブロックNoと部課コードの取得はクリック位置の上の行とする --------------
            Dim refRow As Integer = -1

            '上行が空白空白や一番上の行の場合を入れる？
            If aRowNo > startRow Then
                refRow = aRowNo - 1
                'ブロックNo取得 
                clickBlockNo = sheetBase.Cells(refRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Text
                '部課コード取得
                clickBukaCode = sheetBase.Cells(refRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)).Text
            Else
                'スプレッドへ行挿入と罫線引いて抜ける
                sheetBase.Rows.Add(aRowNo, aRowCount)
                sheetGousya.Rows.Add(aRowNo, aRowCount)
                '挿入した行の員数差の右に二重線を引く
                DoubleBorderInsuSa(aRowNo, aRowNo + aRowCount)
                Return
            End If

            'ブロックNoが未入力の行での挿入は処理を戻す
            If clickBlockNo.Equals(String.Empty) Then
                Return
            End If

            Dim maxGyouId As Integer = -1
            Dim maxBuhinHyoujiJun As Integer = -1

            '行ID及び部品表示順の最大値を取得
            GetMaxNo(clickBukaCode, clickBlockNo, maxGyouId, maxBuhinHyoujiJun)

            'スプレッドへ行挿入
            sheetBase.Rows.Add(aRowNo, aRowCount)
            sheetGousya.Rows.Add(aRowNo, aRowCount)

            '取得した値を空行に対してセットする
            SetBlockNoSpread(clickBukaCode, clickBlockNo, maxGyouId, maxBuhinHyoujiJun, aRowNo, aRowCount)

        End Sub
#End Region

#Region "追加行に対して、部課コード,ブロックNo,行ID,部品表示順をセットする"
        ''' <summary>
        ''' 追加行に対して、部課コード,ブロックNo,行ID,部品表示順をセットする
        ''' </summary>
        ''' <param name="aBukaCode"></param>
        ''' <param name="aBlockNo"></param>
        ''' <param name="aMaxGyouId"></param>
        ''' <param name="aMaxBuhinHyoujiJun"></param>
        ''' <param name="aRowNo"></param>
        ''' <param name="aRowCount"></param>
        ''' <remarks></remarks>
        Private Sub SetBlockNoSpread(ByVal aBukaCode As String, _
                                                        ByVal aBlockNo As String, _
                                                        ByVal aMaxGyouId As Integer, _
                                                        ByVal aMaxBuhinHyoujiJun As Integer, _
                                                        ByVal aRowNo As Integer, _
                                                        ByVal aRowCount As Integer)

            For i As Integer = 0 To aRowCount - 1
                Dim sheetBase As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
                Dim sheetGousya As SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1

                Dim strMaxGyouId As String = String.Format("{0:000}", aMaxGyouId + 1)
                Dim strMaxBuhinHyoujiJun As String = String.Format("{0:000}", aMaxBuhinHyoujiJun + 1)

                '挿入した行に行IDをセット
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_GYOU_ID), strMaxGyouId)
                sheetGousya.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_GYOU_ID), strMaxGyouId)

                '部品表示順をセットする
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN), strMaxBuhinHyoujiJun)
                sheetGousya.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_BUHIN_NO_HYOUJI_JUN), strMaxBuhinHyoujiJun)

                'ブロックNoをセットする
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO), aBlockNo)
                sheetGousya.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_SHISAKU_BLOCK_NO), aBlockNo)

                '部課コードをセットする
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE), aBukaCode)
                sheetGousya.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_SHISAKU_BUKA_CODE), aBukaCode)

                '挿入した行の員数差の右に二重線を引く
                DoubleBorderInsuSa(aRowNo, aRowNo + i)

                Dim startCol As Integer = GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)
                Dim endCol As Integer = GetTagIdx(sheetBase, NmSpdTagBase.TAG_GYOU_ID)

                '編集書式対応
                SetEditRowProc(True, aRowNo + i, startCol, 1, startCol + endCol - startCol)

                aMaxGyouId += 1
                aMaxBuhinHyoujiJun += 1

            Next
        End Sub
#End Region

#Region "新規ブロックNo挿入"
        ''' <summary>
        ''' 新規ブロックNo挿入
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InsertLineBlockNo()
            Dim sheet As SheetView = GetVisibleSheet
            Dim hidSheet As SheetView = GetHiddenSheet
            Dim insRow As Integer = -1
            Dim kaihatsuFugo As String = String.Empty

            '開発符号取得
            kaihatsuFugo = _headerSubject.kaihatsuFugo
            'ブロックNo取得
            Dim blockNo As String = _frmDispTehaiEdit.cmbNewBlockNo.Text.Trim.ToUpper

            '入力されていなければ戻す
            If blockNo.Equals(String.Empty) Then
                '正しいブロックNoかチェック
                'If IsBlockNo(blockNo) = False Then
                ComFunc.ShowInfoMsgBox("正しいブロックNoを入力後に挿入ボタンを押して下さい。")
                Exit Sub
                'End IF
            End If

            '部課コード取得
            Dim bukaCode As String = TehaichoEditImpl.FindBukaCode(blockNo)

            '挿入位置を取得
            insRow = GetBtmLineBlockNo(blockNo)

            Dim maxGyouId As Integer = -1
            Dim maxBuhinHyoujiJun As Integer = -1

            '行ID、部品表示の最大数を取得
            GetMaxNo(bukaCode, blockNo, maxGyouId, maxBuhinHyoujiJun)

            'スプレッドへ行挿入
            sheet.Rows.Add(insRow, 1)
            hidSheet.Rows.Add(insRow, 1)

            '取得した値を空行に対してセットする
            SetBlockNoSpread(bukaCode, blockNo, maxGyouId, maxBuhinHyoujiJun, insRow, 1)

            'アクティブ位置を調整(一番上に行位置がスクロールするとわかりずらいので上に１行表示する)
            '但し先頭行の場合には、自分の行を表示する。　2011/02/28　柳沼修正
            If insRow <= 3 Then
                sheet.SetActiveCell(insRow, sheet.ActiveColumn.Index)
            Else
                sheet.SetActiveCell(insRow - 1, sheet.ActiveColumn.Index)
            End If

            Dim spd As FpSpread = GetVisibleSpread

            spd.ShowActiveCell(Spread.VerticalPosition.Top, Spread.HorizontalPosition.Left)
            spd.ActiveSheet.AddSelection(insRow, -1, 1, -1)

        End Sub
        ''' <summary>
        ''' 指定されたブロックのグループの次のブロックの先頭位置を返す
        ''' 新規ブロックの場合はブロックの挿入位置を返す
        ''' </summary>
        ''' <param name="aBlockNo"></param>
        ''' <returns>※見つからなかった場合は-1を返す</returns>
        ''' <remarks></remarks>
        Private Function GetBtmLineBlockNo(ByVal aBlockNo As String) As Integer
            Dim findRow As Integer = -1
            Dim sheet As SheetView = GetVisibleSheet
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            For i As Integer = startRow To sheet.RowCount - 1
                Dim workBlock As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim

                '行位置保持
                findRow = i

                'ブロックNoの大小比較を行い、大きい値出現によりもう同じ値は見つからないのでその行を返す
                If aBlockNo.Trim < workBlock.Trim Then
                    Exit For
                End If

                '空白行ID発見で終了
                Dim gyouID As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID)).Trim
                If gyouID.Equals(String.Empty) Then
                    '行位置を保持
                    Exit For
                End If

            Next

            Return findRow

        End Function
#End Region

#Region "空行入力"
        ''' <summary>
        ''' 空行入力
        ''' </summary>
        ''' <param name="aRowNo"></param>
        ''' <remarks></remarks>
        Public Sub blankInput(ByVal aRowNo As Integer)
            Dim blockNo As String = String.Empty
            Dim bukaCode As String = String.Empty

            '号車情報スプレッドも同じ処理をする
            Dim sheetGousya As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1
            '基本情報スプレッドをセット
            Dim sheetBase As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1

            '対象行にセットすべきブロックNoを取得する
            Dim dtBase As DataTable = GetBlockNo(aRowNo)

            'ブロックNo取得
            blockNo = dtBase.Rows(0)(NmDTColBase.TD_SHISAKU_BLOCK_NO)
            '部課コード取得 
            bukaCode = dtBase.Rows(0)(NmDTColBase.TD_SHISAKU_BUKA_CODE)

            Dim maxGyouId As Integer = -1
            Dim maxBuhinHyoujiJun As Integer = -1

            '行ID及び部品表示順の最大値を取得
            GetMaxNo(bukaCode, blockNo, maxGyouId, maxBuhinHyoujiJun)

            '取得した値を空行に対してセットする
            SetBlockNoSpread(bukaCode, blockNo, maxGyouId, maxBuhinHyoujiJun, aRowNo, 1)

        End Sub

        ''' <summary>
        ''' ブロックNo取得
        ''' </summary>
        ''' <param name="arowNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetBlockNo(ByVal arowNo As String) As DataTable
            Dim dtResult As DataTable = Nothing
            Dim sheet As SheetView = _frmDispTehaiEdit.spdBase_Sheet1

            '試作手配帳基本のスキーマのみ取得
            dtResult = TehaichoEditImpl.FindAllBaseInfo(String.Empty, String.Empty)

            'とりあえずブロックNoを取得してみる
            Dim blockNo As String = sheet.GetText(arowNo, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim
            Dim bukaCode As String = String.Empty

            '指定行にブロックNoは有るか？
            If blockNo.Equals(String.Empty) = False Then
                'ある場合は部課コードを探す
                bukaCode = TehaichoEditImpl.FindBukaCode(blockNo)
            Else
                '上方向にブロックNoを探してみる
                Dim findRowNo As Integer = SearchBlockNo(arowNo)
                '見つかった行からブロックNoと部課コードを取得
                If findRowNo >= 0 Then
                    blockNo = sheet.GetText(findRowNo, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim
                    bukaCode = sheet.GetText(findRowNo, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)).Trim
                End If

            End If

            Dim dataRow As DataRow = dtResult.NewRow

            '取得できたブロックNo、部課コードをデータテーブルにセット
            dataRow(NmDTColBase.TD_SHISAKU_BLOCK_NO) = blockNo
            dataRow(NmDTColBase.TD_SHISAKU_BUKA_CODE) = bukaCode

            dtResult.Rows.Add(dataRow)

            Return dtResult
        End Function
        '''' <summary>
        '''' 同じブロックNoを探し部課コードを返す
        '''' </summary>
        '''' <param name="aBlockNo"></param>
        '''' <returns></returns>
        '''' <remarks></remarks>
        'Private Function GetBukaCode(ByVal aBlockNo As String) As String
        '    Dim sheet As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
        '    Dim startRow As Integer = GetTitleRowsIn(sheet)
        '    Dim findBukaCode As String = String.Empty

        '    For i As Integer = startRow To sheet.RowCount - 1
        '        Dim workBlockNo As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim

        '        '同じブロックNoか
        '        If aBlockNo.Trim.Equals(workBlockNo) Then
        '            findBukaCode = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)).Trim
        '            If findBukaCode.Equals(String.Empty) = False Then
        '                Exit For
        '            Else
        '                findBukaCode = String.Empty
        '            End If

        '        End If

        '    Next

        '    'スプレッドから見つからなければDBから探す！
        '    If findBukaCode.Equals(String.Empty) Then
        '        '部課コード取得
        '        findBukaCode = TehaichoEditImpl.FindBukaCode(aBlockNo)
        '    End If

        '    Return findBukaCode

        'End Function
        ''' <summary>
        ''' 上方向にブロックNoを探し見つかった行位置を返す
        ''' </summary>
        ''' <param name="aRowNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SearchBlockNo(ByVal aRowNo As Integer) As Integer
            Dim sheet As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            'タイトル行位置取得
            Dim topLimitRowNo As Integer = GetTitleRowsIn(sheet)
            Dim useRowNo As Integer = -1

            '上方向にブロックNoを探す
            For i As Integer = aRowNo - 1 To topLimitRowNo Step -1
                Dim findBlockNo As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim
                If findBlockNo.Equals(String.Empty) = False Then
                    useRowNo = i
                    Exit For
                End If
            Next

            Return useRowNo

        End Function

#End Region

#Region "選択範囲内から対象ブロックを編集リストに追加(行削除時に使用)"
        ''' <summary>
        ''' 選択範囲内に存在するブロックNoを保存対象ブロックNoリストに追加
        ''' </summary>
        ''' <param name="aSelect"></param>
        ''' <remarks></remarks>
        Public Sub AddEditBlockList(ByVal aSelect As Model.CellRange)
            Dim sheet As SheetView = GetVisibleSheet

            'ブロックNoを編集対象として記録
            For i As Integer = 0 To aSelect.RowCount - 1
                Dim blockNo As String = sheet.GetText(aSelect.Row + i, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim
                '編集ブロック格納(重複は排除される）
                listEditBlock = blockNo
            Next

        End Sub

#End Region

#Region "表示シート・非表示シート共通列データ同期"
        ''' <summary>
        ''' 表示シート・非表示シート共通列データ同期
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SheetDataSync()
            Dim sheetVisible As Spread.SheetView = GetVisibleSheet
            Dim sheetHidden As Spread.SheetView = GetHiddenSheet

            For i As Integer = GetTitleRowsIn(sheetVisible) To sheetVisible.RowCount - 1
                '行ID有無でデータ行末を確認
                If sheetVisible.GetText(i, GetTagIdx(sheetVisible, NmSpdTagBase.TAG_GYOU_ID)).Trim.Equals(String.Empty) Then
                    Exit For
                End If

                ''↓↓2015/01/06 行IDを追加 (TES)張 ADD BEGIN
                '行ID
                Dim vGyouId As String = sheetVisible.GetText(i, GetTagIdx(sheetVisible, NmSpdTagBase.TAG_GYOU_ID)).Trim
                Dim hGyouId As String = sheetHidden.GetText(i, GetTagIdx(sheetHidden, NmSpdTagGousya.TAG_GYOU_ID)).Trim
                If vGyouId.Equals(hGyouId) = False Then
                    sheetHidden.SetText(i, GetTagIdx(sheetHidden, NmSpdTagBase.TAG_GYOU_ID), vGyouId)
                End If
                ''↑↑2015/01/06 行IDを追加 (TES)張 ADD END

                'ブロック
                Dim vBlockNo As String = sheetVisible.GetText(i, GetTagIdx(sheetVisible, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim
                Dim hBlockNo As String = sheetHidden.GetText(i, GetTagIdx(sheetHidden, NmSpdTagGousya.TAG_SHISAKU_BLOCK_NO)).Trim
                If vBlockNo.Equals(hBlockNo) = False Then
                    sheetHidden.SetText(i, GetTagIdx(sheetHidden, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO), vBlockNo)
                End If

                '部課コード
                Dim vBukaCode As String = sheetVisible.GetText(i, GetTagIdx(sheetVisible, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)).Trim
                Dim hBukaCode As String = sheetHidden.GetText(i, GetTagIdx(sheetHidden, NmSpdTagGousya.TAG_SHISAKU_BUKA_CODE)).Trim
                If vBukaCode.Equals(hBukaCode) = False Then
                    sheetHidden.SetText(i, GetTagIdx(sheetHidden, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE), vBukaCode)
                End If

                '専用
                Dim vSenyou As String = sheetVisible.GetText(i, GetTagIdx(sheetVisible, NmSpdTagBase.TAG_SENYOU_MARK)).Trim
                Dim hSenyou As String = sheetHidden.GetText(i, GetTagIdx(sheetHidden, NmSpdTagGousya.TAG_SENYOU_MARK)).Trim
                If vSenyou.Equals(hSenyou) = False Then
                    sheetHidden.SetText(i, GetTagIdx(sheetHidden, NmSpdTagBase.TAG_SENYOU_MARK), vSenyou)
                End If

                'レベル
                Dim vLevel As String = sheetVisible.GetText(i, GetTagIdx(sheetVisible, NmSpdTagBase.TAG_LEVEL)).Trim
                Dim hLevel As String = sheetHidden.GetText(i, GetTagIdx(sheetHidden, NmSpdTagGousya.TAG_LEVEL)).Trim
                If vLevel.Equals(hLevel) = False Then
                    sheetHidden.SetText(i, GetTagIdx(sheetHidden, NmSpdTagBase.TAG_LEVEL), vLevel)
                End If

                '部品番号
                Dim vBuhinNo As String = sheetVisible.GetText(i, GetTagIdx(sheetVisible, NmSpdTagBase.TAG_BUHIN_NO)).Trim
                Dim hBuhinNo As String = sheetHidden.GetText(i, GetTagIdx(sheetHidden, NmSpdTagGousya.TAG_BUHIN_NO)).Trim
                If vBuhinNo.Equals(hBuhinNo) = False Then
                    sheetHidden.SetText(i, GetTagIdx(sheetHidden, NmSpdTagBase.TAG_BUHIN_NO), vBuhinNo)
                End If
                '区分
                Dim vBuhinNoKbn As String = sheetVisible.GetText(i, GetTagIdx(sheetVisible, NmSpdTagBase.TAG_BUHIN_NO_KBN)).Trim
                Dim hBuhinNoKbn As String = sheetHidden.GetText(i, GetTagIdx(sheetHidden, NmSpdTagGousya.TAG_BUHIN_NO_KBN)).Trim
                If vBuhinNoKbn.Equals(hBuhinNoKbn) = False Then
                    sheetHidden.SetText(i, GetTagIdx(sheetHidden, NmSpdTagBase.TAG_BUHIN_NO_KBN), vBuhinNoKbn)
                End If

                '2015/12/4 下記の行ロックは不要では？

                ''↓↓↓2014/12/26 メタル項目を追加 TES)張 ADD BEGIN
                'If sheetVisible.Rows(i).BackColor = Color.Gray Then
                '    sheetHidden.Rows(i).BackColor = Color.Gray
                '    sheetHidden.Rows(i).Locked = True
                'End If
                ''↑↑↑2014/12/26 メタル項目を追加 TES)張 ADD END

            Next
        End Sub

#End Region

#Region "員数入力値変換 (-1 →**)"
        ''' <summary>
        ''' 員数入力値変換
        ''' 
        ''' (-1 → **)
        ''' 
        ''' </summary>
        ''' <param name="aRow"></param>
        ''' <remarks></remarks>
        Public Sub ChangeInsuValue(ByVal aRow As Integer)
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1
            Dim startColNo As Integer = TehaichoEditNames.START_COLUMMN_GOUSYA_NAME

            For i As Integer = startColNo To startColNo + _dtGousyaNameList.Rows.Count - 1
                Dim strInsu As String = sheet.GetText(aRow, i).Trim

                '-1は**で置き換える
                If strInsu.Equals("-1") Then
                    sheet.SetText(aRow, i, "**")
                End If

            Next

        End Sub
#End Region

#Region "員数値の集計を行い員数差及び合計員数を設定する"
        ''' <summary>
        ''' 員数値の集計を行い員数差及び合計員数を設定する
        ''' </summary>
        ''' <param name="aRowNo"></param>
        ''' <param name="aRireki">履歴の場合はTRUEをセット</param>
        ''' <remarks></remarks>
        Public Sub CalcInsuSa(ByVal aRowNo As Integer, ByVal aRireki As Boolean)
            Dim sheetBase As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim sheetGousya As SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1

            Dim startColNo As Integer = TehaichoEditNames.START_COLUMMN_GOUSYA_NAME
            Dim endColNo As Integer = sheetGousya.Columns.Count
            Dim totalInsu As Integer = 0
            Dim mTotalInsu As Integer = 0

            '号車列を全てループし員数を合算
            For i As Integer = startColNo To startColNo + _dtGousyaNameList.Rows.Count - 1
                Dim strInsu As String = sheetGousya.GetText(aRowNo, i).Trim

                If strInsu.Equals("-1") OrElse strInsu.Equals("**") Then
                    '-1とかは無視'
                    If StringUtil.Equals(strInsu, "**") Then
                        mTotalInsu += -1
                    Else
                        mTotalInsu += CnvIntStr(strInsu)
                    End If
                    Continue For
                End If

                Dim insu As Integer = CnvIntStr(strInsu)
                totalInsu += insu
            Next

            '合計が0なら-1に変更する'
            If totalInsu = 0 Then
                If mTotalInsu < 0 Then
                    totalInsu = -1
                End If
            End If

            '基本情報スプレッドから員数合計を取得
            Dim strBaseTotal As String = sheetBase.GetText(aRowNo, GetTagIdx(_frmDispTehaiEdit.spdBase_Sheet1, NmSpdTagBase.TAG_TOTAL_INSU_SURYO))

            '号車員数と基本の合計員数を判定
            If totalInsu = -1 OrElse strBaseTotal.Equals("**") Then
                '員数合計値に-1が含まれる場合の処理

                If aRireki = False Then

                    '履歴基本スプレッド合計員数には０をセットする(2011/02/03変更)
                    'sheetBase.SetText(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TOTAL_INSU_SURYO), "**")

                    '履歴基本スプレッド合計員数には０をセットする(2011/02/22柳沼変更)
                    sheetBase.SetText(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TOTAL_INSU_SURYO), 0)

                Else
                    '履歴設定有りの場合は合計員数は変化しない
                End If

                '員数に-1が含まれる場合は員数差は空欄を設定しておく
                sheetGousya.SetText(aRowNo, GetTagIdx(sheetGousya, NmSpdTagGousya.TAG_INSU_SA), String.Empty)

            Else
                '員数合計値に-1が含まれない場合の処理

                '履歴の場合は員数差設定
                If aRireki = True Then
                    If IsNumeric(strBaseTotal) = True Then

                        '数値に変換
                        Dim baseTotal As Integer = CnvIntStr(strBaseTotal)
                        '差分計算(号車トータル員数とベース情報の合計値の差を計算する。)
                        Dim insuSa As Integer = totalInsu - baseTotal

                        If insuSa = 0 Then
                            '0は空白を設定
                            sheetGousya.SetValue(aRowNo, GetTagIdx(sheetGousya, NmSpdTagGousya.TAG_INSU_SA), String.Empty)

                        Else
                            '員数差設定
                            sheetGousya.SetValue(aRowNo, GetTagIdx(sheetGousya, NmSpdTagGousya.TAG_INSU_SA), insuSa)

                        End If
                    End If
                Else
                    '履歴以外では員数合計を基本の合計数量に設定
                    If totalInsu = 0 Then
                        '0はそのままセットする(2011/02/03変更)
                        sheetBase.SetText(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TOTAL_INSU_SURYO), "0")
                    Else
                        sheetBase.SetText(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TOTAL_INSU_SURYO), totalInsu)
                    End If

                End If

            End If

        End Sub

#End Region

#Region "Excel出力機能"

#Region "Excel出力機能(メイン処理)"
        ''' <summary>
        '''  Excel出力
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ExcelOutput(ByVal sExcel As Boolean)

            Dim fileName As String
            Dim impl As TehaichoHeaderDaoImpl
            impl = New TehaichoHeaderDaoImpl
            Dim aEventVo As New TShisakuEventVo
            aEventVo = impl.FindByEvent(_headerSubject.shisakuEventCode)

            '出力ファイル名生成
            Using sfd As New SaveFileDialog()
                sfd.FileName = ShisakuCommon.ShisakuGlobal.ExcelOutPut
                sfd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                '2012/01/25
                '2012/01/21
                '--------------------------------------------------
                'EXCEL出力先フォルダチェック
                ShisakuCommon.ShisakuComFunc.ExcelOutPutDirCheck()
                '--------------------------------------------------

                'PARMによってタイトルを変更
                Dim excelTaitle As String = String.Empty
                If StringUtil.Equals(sExcel, True) Then
                    excelTaitle = " 手配帳編集中 "
                Else
                    excelTaitle = " 手配帳編集中（出図履歴含む） "
                End If

                '[Excel出力系 F]
                'fileName = sfd.InitialDirectory + "\" + sfd.FileName
                fileName = aEventVo.ShisakuKaihatsuFugo + _headerSubject.listEventName + excelTaitle + Now.ToString("MMdd") + Now.ToString("HHmm") + ".xls"
                fileName = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + StringUtil.ReplaceInvalidCharForFileName(fileName)    '2012/02/08 Excel出力ディレクトリ指定対応
            End Using

            '出力処理へ
            SaveExcelFile(fileName, sExcel)

            _frmDispTehaiEdit.Refresh()

            Process.Start(fileName)

            _frmDispTehaiEdit.Refresh()

            ComFunc.ShowInfoMsgBox("Excel出力が完了しました", MessageBoxButtons.OK)

        End Sub
#End Region


#Region "EXCEL出力（出図履歴含む）機能"

        ''' <summary>
        ''' Excel取得Arrayから値を取得する
        ''' 
        ''' ※ ArrayのIndexは1～の為このメソッドで+1を調整する
        ''' 
        ''' </summary>
        ''' <param name="aArryValue"></param>
        ''' <param name="aArryTag">XLSの取得TAG一覧</param>
        ''' <param name="aTagName">Nmのスプレッドタグ名</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetXlsColumn( _
                            ByVal aArryValue As Array, _
                            ByVal aArryTag As Array, _
                            ByVal aTagName As String) As Integer

            Dim value As Integer = 0

            '対象のTAGがExcelファイル内のどの位置にあるか検索
            For i As Integer = 1 To aArryTag.GetLength(1)

                If Not aArryTag.GetValue(1, i) Is Nothing Then
                    If aTagName.Trim.Equals(aArryTag.GetValue(1, i).ToString) Then

                        value = i
                        Exit For

                    End If
                End If
            Next

            Return value
        End Function

        ''' <summary>
        ''' Excel出力　出図履歴含む機能
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub setShutuzuRireki(ByVal xls As ShisakuExcel)

            '
            Dim startXlsDataRow As Integer = EXCEL_IMPORT_DATA_START_ROW
            '全取得
            Dim xlsValue As Object = xls.GetValue(1, startXlsDataRow, xls.EndCol, xls.EndRow)
            Dim arryXls As Array = CType(xlsValue, Array)

            'Excel格納TAG行取得
            Dim xlsTag As Object = xls.GetValue(1, XLS_TAG_NAME_ROW, xls.EndCol, XLS_TAG_NAME_ROW)
            Dim arryTagRow As Array = CType(xlsTag, Array)

            '列№をセット
            Dim colShisakuBlockNo As Integer = GetXlsColumn(arryXls, arryTagRow, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)
            Dim colBuhinNo As Integer = GetXlsColumn(arryXls, arryTagRow, NmSpdTagBase.TAG_BUHIN_NO)
            Dim colShutuzuJisekiDate As Integer = GetXlsColumn(arryXls, arryTagRow, NmSpdTagBase.TAG_SHUTUZU_JISEKI_DATE)
            Dim colShutuzuJisekiKaiteiNo As Integer = GetXlsColumn(arryXls, arryTagRow, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO)
            Dim colShutuzuJisekiStsrDhstba As Integer = GetXlsColumn(arryXls, arryTagRow, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)
            Dim colComment As Integer = colShutuzuJisekiStsrDhstba + 1 '次列
            '挿入行カウント用
            Dim insTotalRows As Integer = 0
            'コメント列を挿入
            '   次列に挿入する
            xls.InsertColumn(colShutuzuJisekiStsrDhstba + 1)
            xls.MergeCells(colShutuzuJisekiDate, 5, colShutuzuJisekiStsrDhstba + 1, 8, True)
            xls.SetValue(colShutuzuJisekiStsrDhstba + 1, 9, "コメント")
            '
            For i As Integer = 0 To arryXls.GetLength(0) - 1
                'ブロックNoの空白でループ終了
                If GetXlsArray(arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO) Is Nothing Then
                    Exit For
                Else
                    ''試作ブロックNo
                    Dim shisakuBlockNo As String = GetXlsArray(arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)
                    ''部品番号
                    Dim BuhinNo As String = GetXlsArray(arryXls, arryTagRow, i, NmSpdTagBase.TAG_BUHIN_NO)

                    '出図履歴を取得
                    Dim shutuzuJisekiVos As List(Of TShisakuTehaiShutuzuJisekiVo) = TehaiShutuzuJiseki(shisakuBlockNo, BuhinNo, True)   'true:改訂№の降順指定
                    '出図手入力情報を取得
                    Dim shutuzuJisekiInputVo As TShisakuTehaiShutuzuJisekiInputVo = _
                                                TehaiShutuzuJisekiInput(shisakuBlockNo, BuhinNo, "", True)  '   true:MAX改訂№を検索する

                    '出図実績が無くて、手入力情報がある場合
                    If shutuzuJisekiVos.Count = 0 And StringUtil.IsNotEmpty(shutuzuJisekiInputVo) Then
                        '改訂№
                        xls.SetValue(colShutuzuJisekiKaiteiNo, startXlsDataRow + i + insTotalRows, shutuzuJisekiInputVo.ShutuzuJisekiKaiteiNo)
                        '受領日
                        xls.SetValue(colShutuzuJisekiDate, startXlsDataRow + i + insTotalRows, ConvDateInt8(shutuzuJisekiInputVo.ShutuzuJisekiJyuryoDate))
                        '設通№
                        xls.SetValue(colShutuzuJisekiStsrDhstba, startXlsDataRow + i + insTotalRows, shutuzuJisekiInputVo.ShutuzuJisekiStsrDhstba)
                        'コメント
                        xls.SetValue(colComment, startXlsDataRow + i + insTotalRows, shutuzuJisekiInputVo.Comment)
                    ElseIf shutuzuJisekiVos.Count <> 0 And StringUtil.IsEmpty(shutuzuJisekiInputVo) Then
                        Dim insRows As Integer = 0
                        '出図実績があり、手入力情報が無い場合
                        '出図履歴をINSERT
                        '   1：履歴無し、2以上：履歴有り
                        If shutuzuJisekiVos.Count > 1 Then
                            '対象行の次行から履歴数－１の行を挿入する。
                            xls.InsertRow(startXlsDataRow + i + 1 + insTotalRows, shutuzuJisekiVos.Count - 1)
                            'カウント
                            insRows = shutuzuJisekiVos.Count - 1
                        End If

                        '予算部品表選択情報分の部品表を出力する。
                        Dim j As Integer = 0
                        For Each vo As TShisakuTehaiShutuzuJisekiVo In shutuzuJisekiVos
                            '改訂№
                            xls.SetValue(colShutuzuJisekiKaiteiNo, startXlsDataRow + i + j + insTotalRows, vo.ShutuzuJisekiKaiteiNo)
                            '受領日
                            xls.SetValue(colShutuzuJisekiDate, startXlsDataRow + i + j + insTotalRows, ConvDateInt8(vo.ShutuzuJisekiJyuryoDate))
                            '設通№
                            xls.SetValue(colShutuzuJisekiStsrDhstba, startXlsDataRow + i + j + insTotalRows, vo.ShutuzuJisekiStsrDhstba)
                            'コメント
                            xls.SetValue(colComment, startXlsDataRow + i + j + insTotalRows, vo.Comment)
                            j += 1
                        Next
                        '挿入行カウント
                        insTotalRows += insRows
                    ElseIf shutuzuJisekiVos.Count <> 0 And StringUtil.IsNotEmpty(shutuzuJisekiInputVo) Then
                        Dim insRows As Integer = 0
                        '両方ある場合
                        Dim j As Integer = 0
                        '実績と手入力の改訂№を比較する。
                        '   手入力が最新の場合出力する。
                        If shutuzuJisekiVos.Item(0).ShutuzuJisekiKaiteiNo < shutuzuJisekiInputVo.ShutuzuJisekiKaiteiNo Then
                            '改訂№
                            xls.SetValue(colShutuzuJisekiKaiteiNo, startXlsDataRow + i + insTotalRows, shutuzuJisekiInputVo.ShutuzuJisekiKaiteiNo)
                            '受領日
                            xls.SetValue(colShutuzuJisekiDate, startXlsDataRow + i + insTotalRows, ConvDateInt8(shutuzuJisekiInputVo.ShutuzuJisekiJyuryoDate))
                            '設通№
                            xls.SetValue(colShutuzuJisekiStsrDhstba, startXlsDataRow + i + insTotalRows, shutuzuJisekiInputVo.ShutuzuJisekiStsrDhstba)
                            'コメント
                            xls.SetValue(colComment, startXlsDataRow + i + insTotalRows, shutuzuJisekiInputVo.Comment)
                            j += 1
                        End If

                        '対象行の次行から履歴数－１の行を挿入する。
                        '   Jを足す
                        xls.InsertRow(startXlsDataRow + i + 1 + insTotalRows, shutuzuJisekiVos.Count - 1 + j)
                        'カウント
                        insRows = shutuzuJisekiVos.Count - 1 + j

                        '予算部品表選択情報分の部品表を出力する。
                        For Each vo As TShisakuTehaiShutuzuJisekiVo In shutuzuJisekiVos
                            '改訂№
                            xls.SetValue(colShutuzuJisekiKaiteiNo, startXlsDataRow + i + j + insTotalRows, vo.ShutuzuJisekiKaiteiNo)
                            '受領日
                            xls.SetValue(colShutuzuJisekiDate, startXlsDataRow + i + j + insTotalRows, ConvDateInt8(vo.ShutuzuJisekiJyuryoDate))
                            '設通№
                            xls.SetValue(colShutuzuJisekiStsrDhstba, startXlsDataRow + i + j + insTotalRows, vo.ShutuzuJisekiStsrDhstba)
                            'コメント
                            xls.SetValue(colComment, startXlsDataRow + i + j + insTotalRows, vo.Comment)
                            j += 1
                        Next
                        '挿入行カウント
                        insTotalRows += insRows
                    End If

                End If

            Next

        End Sub

#End Region


#Region "Excel出力機能(Spreadの機能によりExcelファイルを出力)"
        ''' <summary>
        ''' Excel出力機能(Spreadの機能によりExcelファイルを出力)
        ''' </summary>
        ''' <param name="filename"></param>
        ''' <param name="sExcel">True:最新、False：出図履歴含む</param>
        ''' <remarks></remarks>
        Private Sub SaveExcelFile(ByVal filename As String, ByVal sExcel As Boolean)

            Dim fIO As New System.IO.FileInfo(filename)
            Dim goushaIndex As Integer
            Dim spreadBase As FpSpread = _frmDispTehaiEdit.spdBase
            _frmDispTehaiEdit.Refresh()

            '号車スプレッドを基本情報スプレッドに一旦統合
            goushaIndex = spreadBase.Sheets.Add(CopySheet(_frmDispTehaiEdit.spdGouSya_Sheet1))
            spreadBase.Sheets(goushaIndex).SheetName = _TITLE_GOUSYA
            _frmDispTehaiEdit.Refresh()

            Try
                '基本スプレッド出力
                If _frmDispTehaiEdit.spdBase.SaveExcel(filename) = False Then
                    Throw New Exception("基本情報Excel出力でエラーが発生しました")
                End If

                If Not ShisakuComFunc.IsFileOpen(filename) Then
                    Using xls As New ShisakuExcel(filename)

                        '号車引数列を基本情報シートの末列にコピー
                        InsertGousyaCol(xls)
                        xls.SetActiveSheet(1)
                        xls.SetSheetName(_TITLE_BASE)
                        Me.SetTitleLineExcel(xls, _TITLE_BASE)

                        '出図履歴含む処理を実行
                        '   sExcel:False＝出図履歴含む
                        If StringUtil.Equals(sExcel, False) Then
                            setShutuzuRireki(xls)
                        End If

                        '2012/02/02'
                        'A4横で印刷できるように変更'
                        xls.PrintPaper(filename, 1, "A4")
                        xls.PrintOrientation(filename, 1, 1, False)
                        '保存
                        xls.Save()
                        _frmDispTehaiEdit.Refresh()
                    End Using

                    '↓↓2014/10/17 酒井 ADD BEGIN
                    'マクロが組み込まれたテンプレートファイルに、作成したシートをコピーする。
                    'If System.IO.File.Exists(ExcelSaiTemplateFile) Then
                    '    System.IO.File.Copy(ExcelSaiTemplateFile, filename & "_tmp.xls")
                    '    Using xls As New ShisakuExcel(filename & "_tmp.xls")
                    '        xls.SheetCopy(filename, 1)
                    '        xls.DeleteSheetTrialManufacture(1, True)
                    '        xls.Save()
                    '    End Using
                    '    System.IO.File.Delete(filename)
                    '    System.IO.File.Move(filename & "_tmp.xls", filename)
                    'Else

                    Process.Start(filename)

                    ''2015/04/14 変更 E.Ubukata
                    ''  テンプレートが無くてもエラーを出力しないように変更
                    'Throw New Exception("テンプレートファイル" & ExcelSaiTemplateFile & "が存在しません")
                    'End If
                    '↑↑2014/10/17 酒井 ADD END
                End If

            Finally

                '一時的に統合した号車情報スプレッドを削除
                spreadBase.Sheets.Remove(spreadBase.Sheets(goushaIndex))

            End Try



        End Sub
#End Region

#Region "号車列を基本情報にコピーする"
        Private Sub InsertGousyaCol(ByVal xls As ShisakuExcel)
            Dim gousyaCol As Integer = TehaichoEditNames.START_COLUMMN_GOUSYA_NAME + 1
            Dim endCol As Integer = _frmDispTehaiEdit.spdBase_Sheet1.ColumnCount + 1
            Dim gousyaCnt As Integer = _dtGousyaNameList.Rows.Count - 1

            '号車列コピー
            xls.CopySheetColInsert(2, 1, gousyaCol, endCol, gousyaCnt)
            '号車シートは削除する
            xls.DeleteSheet(2)
            'アクティブセル設定
            xls.SetActiveCell(1, "J1")
            xls.SetActiveCell(1, "A1")

        End Sub
#End Region

#Region "Excel出力機能(Excelシートにタイトル行を追加)"
        ''' <summary>
        ''' Excel出力機能(Excelシートにタイトル行を追加)
        ''' 
        ''' 現状下記4行追加
        ''' 
        ''' リストコード
        ''' イベントコード
        ''' 抽出日付
        ''' SPREAD列TAG
        ''' 
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub SetTitleLineExcel(ByVal xls As ShisakuExcel, ByVal aSheetTitle As String)

            'シート保護を解除
            xls.UnProtectSheet()
            'タイトル行を追加
            xls.InsertRow(1, EXCEL_TITLE_LINE_COUNT)

            xls.SetFont("ＭＳ Ｐゴシック", 10)
            'リストコードセット
            xls.SetValue(1, 1, 1, 1, "リストコード")
            xls.SetValue(XLS_COL_LISTCODE, XLS_ROW_LISTCODE, _shisakuListCode)
            'イベントコード
            xls.SetValue(1, 2, 1, 2, "イベントコード")
            xls.SetValue(XLS_COL_EVENTCODE, XLS_ROW_EVENTCODE, _shisakuEventCode)
            '抽出日
            xls.SetValue(1, 3, "抽出日")
            xls.SetValue(XLS_COL_DATE, XLS_ROW_DATE, String.Format("{0}  {1}", _frmDispTehaiEdit.LblDateNow.Text, _frmDispTehaiEdit.LblTimeNow.Text))
            '行高調整
            xls.SetRowHeight(1, 3, 15)
            'セル結合
            xls.MergeCells(1, 1, 4, 1, True)
            xls.MergeCells(1, 2, 4, 2, True)
            xls.MergeCells(1, 3, 4, 3, True)

            'リストコード格納セル結合
            xls.MergeCells(XLS_COL_LISTCODE, XLS_ROW_LISTCODE, XLS_COL_LISTCODE + 3, XLS_ROW_LISTCODE, True)
            'イベントコード格納セル結合
            xls.MergeCells(XLS_COL_EVENTCODE, XLS_ROW_EVENTCODE, XLS_COL_EVENTCODE + 3, XLS_ROW_EVENTCODE, True)
            '抽出日格納セル結合
            xls.MergeCells(XLS_COL_DATE, XLS_ROW_DATE, XLS_COL_DATE + 3, XLS_ROW_DATE, True)

            '横位置調整
            xls.SetAlliment(XLS_COL_LISTCODE, XLS_ROW_LISTCODE, EBom.Excel.XlHAlign.xlHAlignLeft)
            xls.SetAlliment(XLS_COL_EVENTCODE, XLS_ROW_EVENTCODE, EBom.Excel.XlHAlign.xlHAlignLeft)
            xls.SetAlliment(XLS_COL_DATE, XLS_ROW_DATE, EBom.Excel.XlHAlign.xlHAlignLeft)

            Dim sheet As FarPoint.Win.Spread.SheetView = Nothing

            If aSheetTitle.Equals(_TITLE_BASE) Then
                '基本
                sheet = _frmDispTehaiEdit.spdBase_Sheet1

            ElseIf aSheetTitle.Equals(_TITLE_GOUSYA) Then
                '号車
                sheet = _frmDispTehaiEdit.spdGouSya_Sheet1
            Else
                Throw New Exception("存在しないタイトル名称が指定されている")
                Return
            End If

            'SPREADタグ名称をセットする
            For i As Integer = 0 To sheet.ColumnHeader.Columns.Count - 1
                xls.SetValue(i + 1, XLS_TAG_NAME_ROW, sheet.ColumnHeader.Columns(i).Tag)
            Next

            ''TAG行を非表示に
            xls.HiddenRow(True, XLS_TAG_NAME_ROW)

        End Sub

#End Region

#End Region

#Region "Excel取込機能"

#Region "Excel取込機能(メイン処理)"
        ''' <summary>
        '''Excel取込処理(メイン処理)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Import()

            'ファイルパス取得
            Dim fileName As String = ImportFileDialog()

            If fileName.Equals(String.Empty) Then
                Return
            End If

            Cursor.Current = Cursors.WaitCursor

            'シート名選択画面を表示して取込するシートを選択する。
            '   終了ボタン
            If subSheetSelect(fileName) = "" Then
                Exit Sub
            End If

            '取込み前バックアップはここでは実行しない。
            '' 取込前バックアップ
            'importBackup()

            'Excelファイからのインポート処理
            If importExcelFile(fileName) = False Then
                Return
            End If

            '編集対象ブロックを設定
            ExcelImpAddEditBlock()

            'Excelファイル上で削除された行を探索し編集対象ブロックに追加する(要するにExcel上で削除されると色々厄介なのです）
            Me.DataMatchDeleteRec()

            'クローズ処理イベント取得用クラス
            Dim closer As frm00Kakunin.IFormCloser = New CopyFormCloser(Me, fileName)

            '取込内容確認フォームを表示
            frm00Kakunin.ConfirmShow("EXCEL取込の確認", "EXCELの情報を確認してください。", _
                                           "EXCELのデータを反映しますか？", "EXCELを反映", "取込前に戻す", closer)
        End Sub

#End Region



#Region "Excel取込機能(シート選択)"
        Private Function subSheetSelect(ByVal fileName As String) As String

            '初期設定
            subSheetSelect = ""

            'ExcelSheet選択画面
            Try
                Using frmExcelSheetSelect As New frm20ExcelSheetSelect()

                    'ADO.NET の機能を使ってシート名を取得
                    'ADO でシート名を取得した場合は、Excelで表示している順番ではなくソート順になる。
                    'Dim xlsFileName As String = System.IO.Path.GetFullPath(fileName)
                    Dim con As System.Data.OleDb.OleDbConnection = New System.Data.OleDb.OleDbConnection( _
                          "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & fileName & _
                                ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
                    con.Open()
                    Dim sgt As DataTable = con.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, _
                                 New Object() {Nothing, Nothing, Nothing, "TABLE"})

                    For i As Integer = 0 To sgt.Rows.Count - 1
                        Dim sheetName As String = sgt.Rows(i)!TABLE_NAME.ToString
                        If sheetName.EndsWith("$") Or sheetName.EndsWith("'") Then

                            '余分な文字を取る。
                            sheetName = sheetName.Replace("$", "")
                            sheetName = sheetName.Replace("'", "")

                            '画面のリストボックスに値をセットする。
                            frmExcelSheetSelect.LBexcelSheetSelect.Items.Add(sheetName)
                        End If
                    Next i
                    con.Close()

                    If frmExcelSheetSelect.ShowDialog = Windows.Forms.DialogResult.OK Then
                        subSheetSelect = ExcelImportSheetName
                    Else
                        MsgBox("EXCEL取込を中断します。")
                        subSheetSelect = ""
                        Exit Function
                    End If
                End Using
            Catch
                MsgBox("ExcelSheet選択処理で問題が発生しました")
                Exit Function
            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Function

#End Region



#Region "Excel取込機能(編集ブロック追加)"
        Private Sub ExcelImpAddEditBlock()
            '編集ブロックリスト生成（削除行対策の為全ブロックを対象とする）
            Dim sheet As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            For i As Integer = startRow To sheet.RowCount - 1
                Dim blockNo As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim
                If blockNo.Equals(String.Empty) Then
                    Continue For
                End If

                listEditBlock = blockNo

            Next
        End Sub
#End Region

#Region "Excel取込機能(DBとのマッチングによる削除レコード探索)"
        ''' <summary>
        ''' Excel取込機能(DBとのマッチングによる削除レコード探索)
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub DataMatchDeleteRec()

            Dim dtTehaiBase As DataTable = Nothing
            Dim dtSpreadBase As DataTable = Nothing

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                'マッチング用DBデータテーブル取得
                dtTehaiBase = TehaichoEditImpl.FindAllBaseInfo(db, _shisakuEventCode, _shisakuListCode)
                'マッチング用スプレッドデータテーブル取得
                dtSpreadBase = GetDtSpread(db)

                'DBテーブルとスプレッドのマッチングを行い存在しなくなったデータを削除する
                For i As Integer = 0 To dtTehaiBase.Rows.Count - 1

                    Dim blockNo As String = dtTehaiBase.Rows(i)(NmDTColBase.TD_SHISAKU_BLOCK_NO).ToString

                    'スプレッド上を探索し対象データレコードが存在するか確認
                    If IsDeleteRow(db, dtTehaiBase.Rows(i), dtSpreadBase) = False Then
                        'スプレッド上に存在しない場合は削除されたと判断し保存時の編集処理ブロックとする
                        listEditBlock = blockNo
                    End If

                Next

            End Using
        End Sub
#End Region

#Region "EXCEL取込機能(確認フォームクローズ用クラス)"
        ''' <summary>
        ''' 確認フォームクローズ用
        ''' </summary>
        ''' <remarks></remarks>
        Private Class CopyFormCloser : Implements frm00Kakunin.IFormCloser
            Private _restoreFileName As String = Nothing
            Private _tehaichoLogic As TehaichoEditLogic = Nothing

            Public Sub New(ByVal aTehaichoLogic As TehaichoEditLogic, ByVal aRestoreFileName As String)
                _tehaichoLogic = aTehaichoLogic
                _restoreFileName = aRestoreFileName
            End Sub
            ''' <summary>
            ''' フォームを閉じる時の処理
            ''' </summary>
            ''' <param name="IsOk">OKが押された場合、true</param>
            ''' <remarks></remarks>
            Public Sub FormClose(ByVal IsOk As Boolean) Implements frm00Kakunin.IFormCloser.FormClose

                If IsOk Then
                    'バックアップを削除
                    _tehaichoLogic.importBacupRemove()
                    Return
                Else
                    'リストア処理
                    _tehaichoLogic.importRestore(_restoreFileName)
                End If

            End Sub
        End Class

#End Region

#Region "Excel取込機能(バックアップ削除)"
        ''' <summary>
        ''' Excel取込機能(バックアップ削除)
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub importBacupRemove()
            Dim spdBase As FpSpread = _frmDispTehaiEdit.spdBase
            Dim spdGousya As FpSpread = _frmDispTehaiEdit.spdGouSya

            'バックアップ基本シート削除
            spdBase.Sheets.Remove(spdBase.Sheets(1))
            'バックアップ号車シート削除
            spdGousya.Sheets.Remove(spdGousya.Sheets(1))

            '試作イベントコード・リストコードをクリア
            _backupShisakuListCode = Nothing
            _backupShisakuEventCode = Nothing

        End Sub
#End Region

#Region "Excel取込機能(バックアップ)"
        ''' <summary>
        ''' Excel取込機能(バックアップ)
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub importBackup()
            _frmDispTehaiEdit.Refresh()

            Dim spdBase As FpSpread = _frmDispTehaiEdit.spdBase
            Dim spdGousya As FpSpread = _frmDispTehaiEdit.spdGouSya

            '試作イベントコード・リストコードをバックアップ
            _backupShisakuListCode = _shisakuListCode
            _backupShisakuEventCode = _shisakuEventCode


            '基本スプレッドバックアップ
            Dim baseCopyIndex As Integer = -1
            baseCopyIndex = spdBase.Sheets.Add(CopySheet(_frmDispTehaiEdit.spdBase_Sheet1))
            spdBase.Sheets(baseCopyIndex).SheetName = _BACKUP_SHEET_NAME_BASE


            '号車スプレットバックアップ
            Dim gousyaCopyIndex As Integer = -1
            gousyaCopyIndex = spdGousya.Sheets.Add(CopySheet(_frmDispTehaiEdit.spdGouSya_Sheet1))
            spdGousya.Sheets(gousyaCopyIndex).SheetName = _BACKUP_SHEET_NAME_GOUSYA

            '編集ブロック情報バックアップ
            _backupListEditBlock = New List(Of String())

            For i As Integer = 0 To ListEditCount - 1
                _backupListEditBlock.Add(_listEditBlock(i))
            Next

            'バックアップシートの非表示
            spdBase.Sheets(baseCopyIndex).Visible = False
            spdGousya.Sheets(baseCopyIndex).Visible = False

            _frmDispTehaiEdit.Refresh()

        End Sub

#End Region

#Region "Excel取込機能(リストア)"
        ''' <summary>
        ''' Excel取込機能(リストア)
        ''' </summary>
        ''' <param name="aFileName"></param>
        ''' <remarks></remarks>
        Private Sub importRestore(ByVal aFileName As String)

            Dim spdBase As FpSpread = _frmDispTehaiEdit.spdBase
            Dim spdGousya As FpSpread = _frmDispTehaiEdit.spdGouSya

            'ヘッダー部リストア
            _shisakuListCode = _backupShisakuListCode
            _shisakuEventCode = _backupShisakuEventCode

            'ヘッダー初期化
            InitializeHeader()
            '号車列名使用データ取得
            InitDtGoushaNameList()

            'スプレッド情報リストア
            Dim befBaseName As String = _frmDispTehaiEdit.spdBase_Sheet1.SheetName
            Dim befGousyaName As String = _frmDispTehaiEdit.spdGouSya_Sheet1.SheetName

            '取込した基本シート削除
            spdBase.Sheets.Remove(spdBase.Sheets(0))
            'ﾊﾞｯｸｱｯﾌﾟ用シートをを正規データ用に変更
            spdBase.Sheets(0).SheetName = befBaseName
            '元シートのアドレスにアクセスされないように上書きしておく
            _frmDispTehaiEdit.spdBase_Sheet1 = spdBase.Sheets(0)
            '画面描画
            _frmDispTehaiEdit.Refresh()
            '取込した号車シート削除
            spdGousya.Sheets.Remove(spdGousya.Sheets(0))
            'ﾊﾞｯｸｱｯﾌﾟ用名称を正規データ用に変更
            spdGousya.Sheets(0).SheetName = befBaseName
            '元シートのアドレスにアクセスされないように上書きしておく
            _frmDispTehaiEdit.spdGouSya_Sheet1 = spdGousya.Sheets(0)

            '編集ブロック情報リストア
            _listEditBlock = New List(Of String())

            For i As Integer = 0 To _backupListEditBlock.Count - 1
                _listEditBlock.Add(_backupListEditBlock(i))
            Next

            '非表示を解除
            spdBase.Sheets(0).Visible = True
            spdGousya.Sheets(0).Visible = True

            _backupShisakuListCode = Nothing
            _backupShisakuEventCode = Nothing
            _backupShisakuListCode = Nothing

        End Sub
#End Region

#Region "Excel取込機能(ファイルダイアログ)"
        ''' <summary>
        ''' Excel取込機能(ファイルダイアログ)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ImportFileDialog() As String
            Dim fileName As String
            Dim systemDrive As String = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            Dim ofd As New OpenFileDialog()

            ' ファイル名を指定します
            ofd.FileName = ShisakuCommon.ShisakuGlobal.ExcelOutPut
            ofd.Title = "取込対象のファイルを選択してください"

            ' 起動ディレクトリを指定します
            ofd.InitialDirectory = systemDrive

            '  [ファイルの種類] ボックスに表示される選択肢を指定します
            ofd.Filter = "Excel files(*.xls)|*.xls"

            'ダイアログ選択有無
            If ofd.ShowDialog() = DialogResult.OK Then
                fileName = ofd.FileName
            Else
                Return String.Empty
            End If

            ofd.Dispose()

            Return fileName

        End Function
#End Region

#Region "Excel取込機能(ヘッダー設定)"
        ''' <summary>
        ''' Excel取込機能(ヘッダー設定)
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub importHeader(ByVal xls As ShisakuExcel)

            ''試作イベントコードとリストコードを取込んだ値に入れ替え
            '_shisakuListCode = xls.GetValue(XLS_COL_LISTCODE, XLS_ROW_LISTCODE)
            '_shisakuEventCode = xls.GetValue(XLS_COL_EVENTCODE, XLS_ROW_EVENTCODE)

            'ヘッダー部初期化
            InitializeHeader()

        End Sub
#End Region

#Region "Excel取込機能(Excelオープン)"
        ''' <summary>
        ''' Excel取込機能(Excelオープン)
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <remarks></remarks>
        Private Function importExcelFile(ByVal fileName As String) As Boolean
            Dim dtImpGousyaList As DataTable

            If Not ShisakuComFunc.IsFileOpen(fileName) Then

                Using xls As New ShisakuExcel(fileName)

                    'イベントコードが違う場合エラーを表示する。
                    If Not _shisakuEventCode = xls.GetValue(XLS_COL_EVENTCODE, XLS_ROW_EVENTCODE) Then
                        Dim errMsg As String = String.Format("取込元のイベントコード：" & xls.GetValue(XLS_COL_EVENTCODE, XLS_ROW_EVENTCODE))
                        'エラーメッセージ表示
                        ComFunc.ShowErrMsgBox("イベントコードがアンマッチです。EXCEL取込できません。" & vbLf & vbLf & errMsg)
                        Return False
                    End If
                    'リストコードが違う場合アラートを表示する。
                    If Not _shisakuListCode = xls.GetValue(XLS_COL_LISTCODE, XLS_ROW_LISTCODE) Then
                        Dim artMsg As String = String.Format("取込元のリストコード：" & xls.GetValue(XLS_COL_LISTCODE, XLS_ROW_LISTCODE))
                        Dim yesCheck As Long = 0
                        yesCheck = MsgBox("リストコードがアンマッチです。取込を続行しますか？" & vbLf & vbLf & _
                                          artMsg, MsgBoxStyle.YesNo, "[EXCEL取込]")
                        If yesCheck = MsgBoxResult.No Then
                            Return False
                        End If
                    End If

                    '号車情報取得
                    InitDtGoushaNameList()
                    'チェック用号車列データテーブル生成
                    dtImpGousyaList = GetXlsGousyaName(xls)

                    '列項目エラーチェック
                    If ImportColCheck(dtImpGousyaList, xls) = False Then
                        Return False
                    End If

                    '行データエラーチェック
                    If ImportRowCheck(xls) = False Then
                        Return False
                    End If

                    'ここで取込前のバックアップを実行する。
                    ' 取込前バックアップ
                    importBackup()

                    _frmDispTehaiEdit.Refresh()
                    'ヘッダーへ設定
                    importHeader(xls)
                    '号車列名使用データ取得
                    InitDtGoushaNameList()
                    'スプレッドへ設定(基本)
                    ReadExcelBase(xls)
                    'スプレッドへ設定(号車列名)
                    InitSpreadColGousyaName(dtImpGousyaList)
                    'スプレッドへ設定(号車データ)
                    ReadExcelGousya(xls)
                    '格納データチェック(行ID,表示順採番等)
                    ImportResultSetNo()

                End Using

            End If

            Return True

        End Function

#End Region

#Region "EXCEL取込機能(新規データ採番)"
        Private Sub ImportResultSetNo()

            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startSpdRow As Integer = GetTitleRowsIn(sheet)

            'スプレッド全行確認
            For i As Integer = startSpdRow To sheet.Rows.Count - 1
                'ブロックNo空白で抜ける
                Dim blockNo As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim
                If blockNo.Equals(String.Empty) Then
                    Exit For
                End If

                '部課コード取得
                Dim bukaCode As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)).Trim
                '行IDの最大値を取得
                Dim gyouId As Integer = GetSpreadMaxNoInnerBlock(blockNo, NmSpdTagBase.TAG_GYOU_ID)
                '最小値は0とし、SetblockNoSpreadで更に+1され001が格納される
                If gyouId = -1 Then
                    gyouId = 0
                End If
                '部品表示順の最大値を取得
                Dim hyoujiJun As Integer = GetSpreadMaxNoInnerBlock(blockNo, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN)
                '取得した値をセットする
                SetBlockNoSpread(bukaCode, blockNo, gyouId, hyoujiJun, i, 1)

            Next

        End Sub

#End Region

#Region "EXCEL取込機能(スプレッドへ設定(基本))"
        ''' <summary>
        ''' EXCEL取込機能(スプレッドへ設定(基本))
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub ReadExcelBase(ByVal xls As ShisakuExcel)

            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startXlsDataRow As Integer = EXCEL_IMPORT_DATA_START_ROW
            Dim startSpdRow As Integer = GetTitleRowsIn(sheet)

            '-------------------------------------------
            '２次改修
            '   取込するシートを選択可能にしてみた。
            xls.SetActiveSheet(ExcelImportSheetName)
            '-------------------------------------------

            '全取得
            Dim xlsValue As Object = xls.GetValue(1, startXlsDataRow, xls.EndCol, xls.EndRow)
            Dim arryXls As Array = CType(xlsValue, Array)

            'Excel格納TAG行取得
            Dim xlsTag As Object = xls.GetValue(1, XLS_TAG_NAME_ROW, xls.EndCol, XLS_TAG_NAME_ROW)
            Dim arryTagRow As Array = CType(xlsTag, Array)

            '取込Excelデータ件数によりスプレッドの総行数を拡張(+10は必要余裕行数）
            If xls.EndRow >= sheet.RowCount + 10 Then
                sheet.RowCount = xls.EndRow + 10
            End If

            '全データ消去
            SpreadAllClear(sheet)

            '発注済のチェックボックスは使用不可（チェック不可）にする。
            sheet.Columns(GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)).Locked = True

            '全ての行を編集ブロック対象とする（色等書式変更)
            SetEditRowProc(True, startSpdRow, 0, sheet.RowCount - startSpdRow, sheet.ColumnCount)

            For i As Integer = 0 To arryXls.GetLength(0) - 1

                'ブロックNoの空白でループ終了
                If GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO) Is Nothing Then
                    Exit For
                Else

                    ''試作ブロックNo
                    Dim shisakuBlockNo As String = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)
                    ''試作部課コード
                    Dim shisakuBukaCode As String = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)

                    '部品番号表示順(2011/02/20表示順を空欄にし再採番)
                    Dim BuhinNoHyoujiJun As ValueType = -999
                    sheet.SetValue(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN), BuhinNoHyoujiJun)

                    'ブロックNo設定
                    If shisakuBlockNo Is Nothing Then
                        shisakuBlockNo = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO), shisakuBlockNo)

                    '部課コード設定
                    If shisakuBukaCode Is Nothing Then
                        '部課コードがない場合は取得する
                        shisakuBukaCode = TehaichoEditImpl.FindBukaCode(shisakuBlockNo)
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE), shisakuBukaCode)

                    '履歴
                    Dim rireki As String = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_RIREKI)
                    If rireki Is Nothing Then
                        rireki = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_RIREKI), rireki)

                    '行ID(2011/02/20表示順を空欄にし再採番)
                    Dim GyouId As String = "-999"
                    sheet.SetValue(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID), GyouId)

                    '専用マーク
                    Dim SenyouMark = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SENYOU_MARK)
                    If SenyouMark Is Nothing Then
                        SenyouMark = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_SENYOU_MARK), SenyouMark)

                    'レベル
                    Dim Level = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_LEVEL)
                    If Level Is Nothing Then
                        Level = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_LEVEL), Level)

                    '部品番号
                    Dim BuhinNo = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_BUHIN_NO)
                    If BuhinNo Is Nothing Then
                        BuhinNo = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO), BuhinNo)

                    '部品番号試作区分
                    Dim BuhinKbn = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_BUHIN_NO_KBN)
                    If BuhinKbn Is Nothing Then
                        BuhinKbn = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_KBN), BuhinKbn)

                    '部品番号改訂
                    Dim BuhinKaitei = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_BUHIN_NO_KAITEI_NO)
                    If BuhinKaitei Is Nothing Then
                        BuhinKaitei = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_KAITEI_NO), BuhinKaitei)

                    '枝番
                    Dim BuhinEda = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_EDA_BAN)
                    If BuhinEda Is Nothing Then
                        BuhinEda = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_EDA_BAN), BuhinEda)

                    '部品名称
                    Dim BuhinName = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_BUHIN_NAME)
                    If BuhinName Is Nothing Then
                        BuhinName = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NAME), BuhinName)

                    '集計コード
                    Dim Shukei = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHUKEI_CODE)
                    If Shukei Is Nothing Then
                        Shukei = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUKEI_CODE), Shukei)

                    '手配記号
                    Dim TehaiKigou = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_TEHAI_KIGOU)
                    If TehaiKigou Is Nothing Then
                        TehaiKigou = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_TEHAI_KIGOU), TehaiKigou)

                    '購買担当
                    Dim KouTan = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_KOUTAN)
                    If KouTan Is Nothing Then
                        KouTan = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_KOUTAN), KouTan)

                    '取引先コード
                    Dim TorihkiCode = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_TORIHIKISAKI_CODE)
                    If TorihkiCode Is Nothing Then
                        TorihkiCode = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_TORIHIKISAKI_CODE), TorihkiCode)

                    '納場
                    Dim Nouba = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_NOUBA)
                    If Nouba Is Nothing Then
                        Nouba = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUBA), Nouba)

                    '供給セクション
                    Dim KyouKyu = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_KYOUKU_SECTION)
                    If KyouKyu Is Nothing Then
                        KyouKyu = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_KYOUKU_SECTION), KyouKyu)

                    '納入指示日(日付型)
                    Dim NounyuDate = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)
                    If NounyuDate Is Nothing AndAlso Not IsDate(NounyuDate) Then
                        NounyuDate = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI), NounyuDate)

                    '合計員数
                    Dim TotalInsu = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_TOTAL_INSU_SURYO)
                    If TotalInsu Is Nothing Then
                        TotalInsu = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_TOTAL_INSU_SURYO), TotalInsu)

                    '再使用不可
                    Dim ShiyouFuka = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SAISHIYOUFUKA)
                    If ShiyouFuka Is Nothing Then
                        ShiyouFuka = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISHIYOUFUKA), ShiyouFuka)

                    '出図予定日
                    Dim ShutuzuDate = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE)
                    If ShutuzuDate Is Nothing AndAlso Not IsDate(NounyuDate) Then
                        ShutuzuDate = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE), ShutuzuDate)

                    '出図実績_日付
                    Dim ShutuzuJisekiDate = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHUTUZU_JISEKI_DATE)
                    If ShutuzuJisekiDate Is Nothing AndAlso Not IsDate(ShutuzuJisekiDate) Then
                        ShutuzuJisekiDate = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_DATE), ShutuzuJisekiDate)
                    '出図実績_改訂№
                    Dim ShutuzuJisekiKaiteiNo = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO)
                    If ShutuzuJisekiKaiteiNo Is Nothing Then
                        ShutuzuJisekiKaiteiNo = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO), ShutuzuJisekiKaiteiNo)
                    '出図実績_設通№
                    Dim ShutuzuJisekiStsrDhstba = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)
                    If ShutuzuJisekiStsrDhstba Is Nothing Then
                        ShutuzuJisekiStsrDhstba = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA), ShutuzuJisekiStsrDhstba)
                    '最終織込設変情報_日付
                    Dim SaisyuSetsuhenDate = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SAISYU_SETSUHEN_DATE)
                    If SaisyuSetsuhenDate Is Nothing AndAlso Not IsDate(SaisyuSetsuhenDate) Then
                        SaisyuSetsuhenDate = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_DATE), SaisyuSetsuhenDate)
                    '最終織込設変情報_改訂№
                    Dim SaisyuSetsuhenKaiteiNo = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO)
                    If SaisyuSetsuhenKaiteiNo Is Nothing Then
                        SaisyuSetsuhenKaiteiNo = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO), SaisyuSetsuhenKaiteiNo)
                    '最終織込設変情報_設通№
                    Dim SaisyuSetsuhenStsrDhstba = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SAISYU_SETSUHEN_STSR_DHSTBA)
                    If SaisyuSetsuhenStsrDhstba Is Nothing Then
                        SaisyuSetsuhenStsrDhstba = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_STSR_DHSTBA), SaisyuSetsuhenStsrDhstba)
                    '材料寸法_X(mm)
                    Dim ZairyoSunpoX = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X)
                    If ZairyoSunpoX Is Nothing Then
                        ZairyoSunpoX = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X), ZairyoSunpoX)
                    '材料寸法_Y(mm)
                    Dim ZairyoSunpoY = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y)
                    If ZairyoSunpoY Is Nothing Then
                        ZairyoSunpoY = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y), ZairyoSunpoY)
                    '材料寸法_Z(mm)
                    Dim ZairyoSunpoZ = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z)
                    If ZairyoSunpoZ Is Nothing Then
                        ZairyoSunpoZ = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z), ZairyoSunpoZ)

                    ''材料寸法_X+Y(mm)
                    'Dim ZairyoSunpoXy = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY)
                    'If ZairyoSunpoXy Is Nothing Then
                    '    ZairyoSunpoXy = String.Empty
                    'End If
                    'sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY), ZairyoSunpoXy)
                    ''材料寸法_X+Z(mm)
                    'Dim ZairyoSunpoXz = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ)
                    'If ZairyoSunpoXz Is Nothing Then
                    '    ZairyoSunpoXz = String.Empty
                    'End If
                    'sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ), ZairyoSunpoXz)
                    ''材料寸法_Y+Z(mm)
                    'Dim ZairyoSunpoYz = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ)
                    'If ZairyoSunpoYz Is Nothing Then
                    '    ZairyoSunpoYz = String.Empty
                    'End If
                    'sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ), ZairyoSunpoYz)


                    Dim sunpoX As Decimal = 0
                    If StringUtil.IsNotEmpty(ZairyoSunpoX) Then
                        sunpoX = CDec(ZairyoSunpoX)
                    End If
                    Dim sunpoY As Decimal = 0
                    If StringUtil.IsNotEmpty(ZairyoSunpoY) Then
                        sunpoY = CDec(ZairyoSunpoY)
                    End If
                    Dim sunpoZ As Decimal = 0
                    If StringUtil.IsNotEmpty(ZairyoSunpoZ) Then
                        sunpoZ = CDec(ZairyoSunpoZ)
                    End If
                    '自動計算
                    Dim sunpoXy As Decimal = sunpoX + sunpoY
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY), GetNumericDbField(sunpoXy))
                    Dim sunpoXz As Decimal = sunpoX + sunpoZ
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ), GetNumericDbField(sunpoXz))
                    Dim sunpoYz As Decimal = sunpoY + sunpoZ
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ), GetNumericDbField(sunpoYz))

                    '最大値のセルは太文字・背景色をグレーにする
                    Dim sunpo(2) As Double
                    sunpo(0) = sunpoXy
                    sunpo(1) = sunpoXz
                    sunpo(2) = sunpoYz
                    Array.Sort(sunpo)
                    Select Case sunpo(2)
                        Case 0
                            '最大値が0の場合スルー
                        Case sunpoXy
                            sheet.Cells(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY), _
                                        startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY)).BackColor = Color.Yellow
                            sheet.Cells(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY), _
                                        startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY)).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                        Case sunpoXz
                            sheet.Cells(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ), _
                                        startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ)).BackColor = Color.Yellow
                            sheet.Cells(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ), _
                                        startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ)).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                        Case sunpoYz
                            sheet.Cells(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ), _
                                        startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ)).BackColor = Color.Yellow
                            sheet.Cells(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ), _
                                        startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ)).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                    End Select



                    '材質・規格1
                    Dim Zaishitsu1 = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1)
                    If Zaishitsu1 Is Nothing Then
                        Zaishitsu1 = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1), Zaishitsu1)

                    '材質・規格2
                    Dim Zaishitsu2 = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2)
                    If Zaishitsu2 Is Nothing Then
                        Zaishitsu2 = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2), Zaishitsu2)

                    '材質・規格3
                    Dim Zaishitsu3 = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3)
                    If Zaishitsu3 Is Nothing Then
                        Zaishitsu3 = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3), Zaishitsu3)

                    '材質・メッキ
                    Dim ZaiMekki = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_ZAISHITU_MEKKI)
                    If ZaiMekki Is Nothing Then
                        ZaiMekki = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_MEKKI), ZaiMekki)

                    '板厚
                    Dim Banko = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO)
                    If Banko Is Nothing Then
                        Banko = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO), Banko)

                    '板厚・ｕ
                    Dim BankoU = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U)
                    If BankoU Is Nothing Then
                        BankoU = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U), BankoU)

                    '↓↓↓2014/12/30 メタル項目を追加 TES)張 ADD BEGIN
                    '材料情報・製品長
                    Dim MaterialInfoLength = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_MATERIAL_INFO_LENGTH)
                    If MaterialInfoLength Is Nothing Then
                        MaterialInfoLength = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_LENGTH), MaterialInfoLength)
                    '材料情報・製品幅
                    Dim MaterialInfoWidth = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_MATERIAL_INFO_WIDTH)
                    If MaterialInfoWidth Is Nothing Then
                        MaterialInfoWidth = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_WIDTH), MaterialInfoWidth)
                    '材料情報・発注対象
                    Dim MaterialInfoOrderTarget = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET)
                    If MaterialInfoOrderTarget Is Nothing Then
                        MaterialInfoOrderTarget = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET), MaterialInfoOrderTarget)
                    '材料情報・発注済
                    Dim MaterialInfoOrderChk = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)
                    If MaterialInfoOrderChk Is Nothing Then
                        MaterialInfoOrderChk = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK), MaterialInfoOrderChk)
                    '発注対象のチェックボックスのチェックを外した場合、発注済のチェックボックスは使用不可（チェック不可）にする。
                    If MaterialInfoOrderTarget IsNot Nothing Then
                        sheet.Cells(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)).Locked = Not CBool(MaterialInfoOrderTarget)
                    Else
                        sheet.Cells(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)).Locked = True
                    End If
                    'データ項目・改訂№
                    Dim DataItemKaiteiNo = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO)
                    If DataItemKaiteiNo Is Nothing Then
                        DataItemKaiteiNo = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO), DataItemKaiteiNo)
                    'データ項目・エリア名
                    Dim DataItemAreaName = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME)
                    If DataItemAreaName Is Nothing Then
                        DataItemAreaName = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME), DataItemAreaName)
                    'データ項目・セット名
                    Dim DataItemSetName = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME)
                    If DataItemSetName Is Nothing Then
                        DataItemSetName = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME), DataItemSetName)
                    'データ項目・改訂情報
                    Dim DataItemKaiteiInfo = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_INFO)
                    If DataItemKaiteiInfo Is Nothing Then
                        DataItemKaiteiInfo = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_INFO), DataItemKaiteiInfo)
                    'データ項目・データ支給チェック欄
                    Dim DataItemDataProvision = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION)
                    If DataItemDataProvision Is Nothing Then
                        DataItemDataProvision = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION), DataItemDataProvision)
                    '↑↑↑2014/12/30 メタル項目を追加 TES)張 ADD END

                    '試作部品費（円）
                    Dim BuhinHi = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHISAKU_BUHINN_HI)
                    If BuhinHi Is Nothing Then
                        BuhinHi = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BUHINN_HI), BuhinHi)

                    '試作型費（千円）
                    Dim KataHi = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHISAKU_KATA_HI)
                    If KataHi Is Nothing Then
                        KataHi = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_KATA_HI), KataHi)

                    '取引先名称
                    Dim MakerCode = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_MAKER_CODE)
                    If MakerCode Is Nothing Then
                        MakerCode = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_MAKER_CODE), MakerCode)

                    '備考
                    Dim Bikou = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_BIKOU)
                    If Bikou Is Nothing Then
                        Bikou = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_BIKOU), Bikou)

                    '親部品
                    Dim BuhinOya = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_BUHIN_NO_OYA)
                    If BuhinOya Is Nothing Then
                        BuhinOya = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_OYA), BuhinOya)

                    '親部品試作区分
                    Dim BuhinOyaKbn = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_BUHIN_NO_KBN_OYA)
                    If BuhinOyaKbn Is Nothing Then
                        BuhinOyaKbn = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_KBN_OYA), BuhinOyaKbn)

                    '変化点
                    Dim HenkaTen = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_HENKATEN)
                    If HenkaTen Is Nothing Then
                        HenkaTen = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_HENKATEN), HenkaTen)




                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_TSUKURIKATA_SEISAKU), _
                                  getTextData(GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_TSUKURIKATA_SEISAKU)))

                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_1), _
                                  getTextData(GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_1)))

                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_2), _
                                  getTextData(GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_2)))

                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_3), _
                                 getTextData(GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_3)))

                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_TSUKURIKATA_TIGU), _
                                getTextData(GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_TSUKURIKATA_TIGU)))

                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_TSUKURIKATA_NOUNYU), _
                               getTextData(GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_TSUKURIKATA_NOUNYU)))

                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_TSUKURIKATA_KIBO), _
                              getTextData(GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_TSUKURIKATA_KIBO)))

                End If

            Next



        End Sub

        Private Function getTextData(ByVal str As String) As String
            If str Is Nothing Then
                Return String.Empty
            End If
            Return str
        End Function

#End Region

#Region "EXCEL取込機能(号車列名データテーブル生成)"
        ''' <summary>
        ''' EXCEL取込機能(号車列名データテーブル生成)
        ''' 
        ''' ※取込の処理では_dtGousyaNameListをExcelファイル
        '''      から生成する
        ''' 
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Function GetXlsGousyaName(ByVal xls As ShisakuExcel) As DataTable

            Dim dtGousya As New DataTable
            Dim sheetBase As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim titleRowCnt As Integer = GetTitleRowsIn(sheetBase)

            'データテーブルの属性情報のみ取得
            dtGousya = TehaichoEditImpl.FindAllGousyaNameList("ZZZZZZZZ", "ZZZZZZZZ")

            Dim intHyoujiCnt As Integer = 0

            '基本情報の末列以降に格納された号車情報をデータテーブルに格納
            For j As Integer = sheetBase.ColumnCount + 1 To xls.EndCol

                Dim row As DataRow = dtGousya.NewRow

                Dim mDate As String = ConvDateInt8(IIf(StringUtil.IsEmpty(xls.GetValue(j, EXCEL_IMPORT_DATA_START_ROW - titleRowCnt)), 0, _
                                                       xls.GetValue(j, EXCEL_IMPORT_DATA_START_ROW - titleRowCnt).ToString.Replace("/", "")))
                If mDate Is Nothing AndAlso Not IsDate(mDate) Then
                    mDate = String.Empty
                End If
                Dim tDate As String = ConvDateInt8(IIf(StringUtil.IsEmpty(xls.GetValue(j, EXCEL_IMPORT_DATA_START_ROW - titleRowCnt + 1)), 0, _
                                                       xls.GetValue(j, EXCEL_IMPORT_DATA_START_ROW - titleRowCnt + 1).ToString.Replace("/", "")))
                If tDate Is Nothing AndAlso Not IsDate(tDate) Then
                    tDate = String.Empty
                End If

                Dim gousyaName As String = xls.GetValue(j, EXCEL_IMPORT_DATA_START_ROW - titleRowCnt + 2)
                Dim gousyaHyoujiJun As String = intHyoujiCnt.ToString.Trim

                If gousyaName Is Nothing Then
                    row(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME) = String.Empty
                    gousyaName = String.Empty
                Else
                    row(NmDTColGousyaNameList.TD_HYOJIJUN_NO) = gousyaHyoujiJun
                    row(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME) = gousyaName.Trim
                    If StringUtil.IsNotEmpty(mDate) Then
                        row(NmDTColGousyaNameList.TD_M_NOUNYU_SHIJIBI) = mDate.Replace("/", "")
                    End If
                    If StringUtil.IsNotEmpty(tDate) Then
                        row(NmDTColGousyaNameList.TD_T_NOUNYU_SHIJIBI) = tDate.Replace("/", "")
                    End If

                    intHyoujiCnt += 1
                    dtGousya.Rows.Add(row)

                    'DUMMYまで来たら抜ける
                    If gousyaName.Equals(_DUMMY_NAME) Then
                        Exit For
                    End If
                End If
            Next

            Return dtGousya

        End Function
#End Region

#Region "EXCEL取込機能(スプレッドへ設定(号車))"
        ''' <summary>
        ''' EXCEL取込機能(スプレッドへ設定(号車))
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub ReadExcelGousya(ByVal xls As ShisakuExcel)
            Dim sheetGousya As SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1
            Dim sheetBase As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startXlsDataRow As Integer = EXCEL_IMPORT_DATA_START_ROW
            Dim startSpdRow As Integer = GetTitleRowsIn(sheetGousya)
            Dim startGousyaCol As Integer = TehaichoEditNames.START_COLUMMN_GOUSYA_NAME
            Dim titleRowCnt As Integer = GetTitleRowsIn(sheetBase)

            '全取得
            Dim xlsValue As Object = xls.GetValue(1, startXlsDataRow, xls.EndCol, xls.EndRow)
            Dim arryXls As Array = CType(xlsValue, Array)

            'Excel格納TAG行取得
            Dim xlsTag As Object = xls.GetValue(1, XLS_TAG_NAME_ROW, xls.EndCol, XLS_TAG_NAME_ROW)
            Dim arryTagRow As Array = CType(xlsTag, Array)

            '基本スプレッドと行数をあわせる
            sheetGousya.RowCount = sheetBase.RowCount


            '全データ消去
            SpreadAllClear(sheetGousya)

            '編集行とし書式設定
            SetEditRowProc(False, startSpdRow, 0, sheetGousya.RowCount - startSpdRow, sheetGousya.ColumnCount)

            For i As Integer = 0 To arryXls.GetLength(0) - 1

                'ブロックNoの空白でループ終了
                If GetXlsArray(sheetGousya, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO) Is Nothing Then
                    Exit For
                Else

                    '履歴
                    Dim rireki As String = GetXlsArray(sheetGousya, arryXls, arryTagRow, i, NmSpdTagBase.TAG_RIREKI)
                    If rireki Is Nothing Then
                        rireki = String.Empty
                    End If
                    sheetGousya.SetText(startSpdRow + i, GetTagIdx(sheetGousya, NmSpdTagBase.TAG_RIREKI), rireki)

                    ''試作部課コード
                    Dim shisakuBukaCode As String = GetXlsArray(sheetGousya, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)
                    If shisakuBukaCode Is Nothing Then
                        shisakuBukaCode = String.Empty
                    End If
                    sheetGousya.SetText(startSpdRow + i, GetTagIdx(sheetGousya, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE), shisakuBukaCode)

                    ''試作ブロックNo
                    Dim shisakuBlockNo As String = GetXlsArray(sheetGousya, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)
                    If shisakuBlockNo Is Nothing Then
                        shisakuBlockNo = String.Empty
                    End If
                    sheetGousya.SetText(startSpdRow + i, GetTagIdx(sheetGousya, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO), shisakuBlockNo)

                    '行ID
                    Dim GyouId As String = GetXlsArray(sheetGousya, arryXls, arryTagRow, i, NmSpdTagBase.TAG_GYOU_ID)
                    If GyouId Is Nothing Then
                        GyouId = String.Empty
                    End If
                    sheetGousya.SetText(startSpdRow + i, GetTagIdx(sheetGousya, NmSpdTagBase.TAG_GYOU_ID), GyouId)

                    '専用マーク
                    Dim SenyouMark = GetXlsArray(sheetGousya, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SENYOU_MARK)
                    If SenyouMark Is Nothing Then
                        SenyouMark = String.Empty
                    End If
                    sheetGousya.SetText(startSpdRow + i, GetTagIdx(sheetGousya, NmSpdTagBase.TAG_SENYOU_MARK), SenyouMark)

                    'レベル
                    Dim Level = GetXlsArray(sheetGousya, arryXls, arryTagRow, i, NmSpdTagBase.TAG_LEVEL)
                    If Level Is Nothing Then
                        Level = String.Empty
                    End If
                    sheetGousya.SetText(startSpdRow + i, GetTagIdx(sheetGousya, NmSpdTagBase.TAG_LEVEL), Level)

                    '部品番号表示順
                    Dim BuhinNoHyoujiJun = GetXlsArray(sheetGousya, arryXls, arryTagRow, i, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN)
                    If BuhinNoHyoujiJun Is Nothing Then
                        BuhinNoHyoujiJun = String.Empty
                    End If
                    sheetGousya.SetText(startSpdRow + i, GetTagIdx(sheetGousya, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN), BuhinNoHyoujiJun)

                    '部品番号
                    Dim BuhinNo = GetXlsArray(sheetGousya, arryXls, arryTagRow, i, NmSpdTagBase.TAG_BUHIN_NO)
                    If BuhinNo Is Nothing Then
                        BuhinNo = String.Empty
                    End If
                    sheetGousya.SetText(startSpdRow + i, GetTagIdx(sheetGousya, NmSpdTagBase.TAG_BUHIN_NO), BuhinNo)

                    '部品番号試作区分
                    Dim BuhinKbn = GetXlsArray(sheetGousya, arryXls, arryTagRow, i, NmSpdTagBase.TAG_BUHIN_NO_KBN)
                    If BuhinKbn Is Nothing Then
                        BuhinKbn = String.Empty
                    End If
                    sheetGousya.SetText(startSpdRow + i, GetTagIdx(sheetGousya, NmSpdTagBase.TAG_BUHIN_NO_KBN), BuhinKbn)

                    '基本情報の末列以降に格納された号車情報をデータテーブルに格納
                    For j As Integer = 0 To xls.EndCol

                        Dim gousyaName As String = ""

                        If xls.GetValue(j + sheetBase.ColumnCount + 1, EXCEL_IMPORT_DATA_START_ROW - titleRowCnt + 2) IsNot Nothing Then
                            gousyaName = xls.GetValue(j + sheetBase.ColumnCount + 1, EXCEL_IMPORT_DATA_START_ROW - titleRowCnt + 2).ToString.Trim
                        End If

                        Dim gousyaInsu = arryXls.GetValue(i + 1, j + sheetBase.ColumnCount + 1)

                        '同じ号車列を探す
                        For k As Integer = 0 To sheetGousya.ColumnCount - TehaichoEditNames.START_COLUMMN_GOUSYA_NAME - 1
                            Dim spdGousyaName As String = sheetGousya.GetText(2, k + TehaichoEditNames.START_COLUMMN_GOUSYA_NAME)

                            If gousyaName.Equals(spdGousyaName) Then
                                sheetGousya.SetText(startSpdRow + i, startGousyaCol + k, gousyaInsu)
                                Exit For
                            End If
                        Next

                        'DUMMYまで来たら抜ける
                        If gousyaName.Equals(_DUMMY_NAME) Then
                            Exit For
                        End If

                    Next

                    '履歴可否により員数差設定
                    If rireki.Equals("*") Then
                        ''員数差表示
                        CalcInsuSa(startSpdRow + i, True)
                    Else
                        ''員数差非表示・基本スプレッドに合計員数設定
                        CalcInsuSa(startSpdRow + i, False)
                    End If

                End If

            Next

            '二重線を員数差の
            DoubleBorderInsuSa(startSpdRow, sheetGousya.RowCount - 1)

        End Sub
#End Region

#Region "Exce取込機能(Arrayから値取得)"

        ''' <summary>
        ''' Excel取得Arrayから値を取得する
        ''' 
        ''' ※ ArrayのIndexは1～の為このメソッドで+1を調整する
        ''' 
        ''' </summary>
        ''' <param name="aSheet">対象シート名</param>
        ''' <param name="aArryValue"></param>
        ''' <param name="aArryTag">XLSの取得TAG一覧</param>
        ''' <param name="aRowNo">0～のIndex</param>
        ''' <param name="aTagName">Nmのスプレッドタグ名</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetXlsArray(ByVal aSheet As FarPoint.Win.Spread.SheetView, _
                            ByVal aArryValue As Array, _
                            ByVal aArryTag As Array, _
                            ByVal aRowNo As Integer, _
                            ByVal aTagName As String) As Object

            Dim value As Object = Nothing

            '対象のTAGがExcelファイル内のどの位置にあるか検索
            For i As Integer = 1 To aArryTag.GetLength(1)

                If Not aArryTag.GetValue(1, i) Is Nothing Then
                    If aTagName.Trim.Equals(aArryTag.GetValue(1, i).ToString) Then

                        value = aArryValue.GetValue(aRowNo + 1, i)

                    End If
                End If
            Next

            Return value
        End Function

        ''' <summary>
        ''' Excel取得Arrayから値を取得する
        ''' 
        ''' ※ ArrayのIndexは1～の為このメソッドで+1を調整する
        ''' 
        ''' </summary>
        ''' <param name="aArryValue"></param>
        ''' <param name="aArryTag">XLSの取得TAG一覧</param>
        ''' <param name="aRowNo">0～のIndex</param>
        ''' <param name="aTagName">Nmのスプレッドタグ名</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetXlsArray( _
                            ByVal aArryValue As Array, _
                            ByVal aArryTag As Array, _
                            ByVal aRowNo As Integer, _
                            ByVal aTagName As String) As Object

            Dim value As Object = Nothing

            '対象のTAGがExcelファイル内のどの位置にあるか検索
            For i As Integer = 1 To aArryTag.GetLength(1)

                If Not aArryTag.GetValue(1, i) Is Nothing Then
                    If aTagName.Trim.Equals(aArryTag.GetValue(1, i).ToString) Then

                        value = aArryValue.GetValue(aRowNo + 1, i)

                    End If
                End If
            Next

            Return value
        End Function

#End Region

#Region "Excel取込機能(Excel列チェック)"

        Private Function ImportRowCheck(ByVal xls As ShisakuExcel) As Boolean
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startSpdRow As Integer = GetTitleRowsIn(sheet)
            Dim startXlsDataRow As Integer = EXCEL_IMPORT_DATA_START_ROW
            Dim dtBlockNo As New DataTable

            dtBlockNo.Columns.Add("BLOCK_NO", GetType(String))
            dtBlockNo.PrimaryKey = New DataColumn() {dtBlockNo.Columns(0)}

            '全取得
            Dim xlsValue As Object = xls.GetValue(1, startXlsDataRow, xls.EndCol, xls.EndRow)
            Dim arryXls As Array = CType(xlsValue, Array)

            'Excel格納TAG行取得
            Dim xlsTag As Object = xls.GetValue(1, XLS_TAG_NAME_ROW, xls.EndCol, XLS_TAG_NAME_ROW)
            Dim arryTagRow As Array = CType(xlsTag, Array)

            Dim listBlockNo As New List(Of String)

            If arryXls.GetLength(0) <= 0 Then
                Throw New Exception("取込対象のデータが1件も有りません。")
            End If

            'ブロックNoチェック用
            Dim workBlockNo As String = GetXlsArray(sheet, arryXls, arryTagRow, 0, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO).ToString.Trim
            Dim rowData As DataRow = dtBlockNo.NewRow
            rowData("BLOCK_NO") = workBlockNo
            dtBlockNo.Rows.Add(rowData)

            'Excelデータ行チェック
            For i As Integer = 0 To arryXls.GetLength(0) - 1

                'ブロックNoセット無しでループ終了
                If GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO) Is Nothing Then
                    Exit For
                End If

                '試作ブロックNoチェック
                Dim shisakuBlockNo As String = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO).ToString

                '飛び番号ブロックチェック
                If workBlockNo.Equals(shisakuBlockNo) = False Then
                    Dim rowWorkData As DataRow = dtBlockNo.NewRow
                    rowWorkData("BLOCK_NO") = shisakuBlockNo
                    Try
                        dtBlockNo.Rows.Add(rowWorkData)
                    Catch ex As System.Data.ConstraintException
                        Throw New Exception(String.Format("ブロックNo:{0}が {1}行目付近で重複出現しています。", shisakuBlockNo, i + EXCEL_IMPORT_DATA_START_ROW))
                    End Try
                End If

                workBlockNo = shisakuBlockNo

            Next

            Return True

        End Function

#End Region

#Region "Excel取込機能(Excel列チェック)"
        ''' <summary>
        ''' Excel取込機能(Excel列チェック)
        ''' </summary>
        ''' <param name="aDtGousyaList"></param>
        ''' <param name="xls"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ImportColCheck(ByVal aDtGousyaList As DataTable, ByVal xls As ShisakuExcel) As Boolean

            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim errList As System.Text.StringBuilder = New System.Text.StringBuilder

            'Excel格納TAG行取得
            Dim xlsValue As Object = xls.GetValue(1, XLS_TAG_NAME_ROW, xls.EndCol, XLS_TAG_NAME_ROW)
            Dim arryTagRow As Array = CType(xlsValue, Array)

            'スプレッド列の必須列存在チェックループ
            For i As Integer = 0 To sheet.ColumnCount - 1

                Dim spdTagName As String = sheet.ColumnHeader.Columns(i).Tag.ToString.Trim
                Dim findFlag As Boolean = False

                'Excel列ループ
                For j As Integer = 1 To arryTagRow.GetLength(1)
                    If arryTagRow.GetValue(1, j) Is Nothing Then
                        Continue For
                    End If

                    Dim xlsTagName As String = arryTagRow.GetValue(1, j).ToString.Trim

                    '比較合致で抜ける
                    If spdTagName.Equals(xlsTagName) = True Then
                        findFlag = True
                        Exit For
                    End If

                Next

                '未発見は格納し最後に表示
                If findFlag = False Then
                    errList.Append(spdTagName & vbCrLf)
                End If

            Next

            ''号車列名称の不要な空欄チェック
            'For i As Integer = sheet.ColumnCount + 1 To xls.EndCol

            '    Dim xlsGousyaValue As Object = arryTagRow.GetValue(1, i)

            '    If xlsGousyaValue Is Nothing Then
            '        Dim errMsg As String = String.Format("Excel取込で問題が発生しました。{0} 不要な空欄列が存在します。{1}({2}列付近)" _
            '                                             , vbCrLf, vbCrLf & vbCrLf, i)
            '        'エラーメッセージ表示
            '        ComFunc.ShowErrMsgBox(errMsg)
            '        Return False
            '    End If
            'Next

            'エラー表示
            If errList.ToString.Equals(String.Empty) = False Then
                Dim errMsg As String = String.Format("Excel取込で問題が発生しました。{0} 取込に必要な列が存在しない可能性が有ります。{1}({2})" _
                                                     , vbCrLf, vbCrLf & vbCrLf, errList.ToString)
                'エラーメッセージ表示
                ComFunc.ShowErrMsgBox(errMsg)

                Return False
            End If

            Dim newEventCode As String = xls.GetValue(XLS_COL_EVENTCODE, XLS_ROW_EVENTCODE)
            'Dim newListCode As String = xls.GetValue(XLS_COL_LISTCODE, XLS_ROW_LISTCODE)
            Dim newListCode As String = _shisakuListCode
            '号車名称リストを取得
            Dim dtNewGousyaList As DataTable = TehaichoEditImpl.FindAllGousyaNameList(newEventCode, newListCode)

            '取得出来ない場合は抜ける
            If dtNewGousyaList.Rows.Count = 0 Then
                Throw New Exception("号車名称が取得出来ませんでした。処理を終了します。")
                Return False
            End If

            Try
                Dim newGousyaName As String = String.Empty
                Dim xlsGousyaName As String = String.Empty

                'DBとEXCELの号車数が不一致の場合はThrowする。
                '   ダミー列、空白列は除く
                Dim lngCountDt As Long = 0
                For j As Integer = 0 To dtNewGousyaList.Rows.Count - 1
                    If StringUtil.IsNotEmpty(dtNewGousyaList.Rows(j)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME)) Then
                        'ダミー列はカウントしない。
                        If dtNewGousyaList.Rows(j)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME) <> "DUMMY" Then
                            lngCountDt = lngCountDt + 1
                        End If
                    End If
                Next
                Dim lngCount As Long = 0
                For j As Integer = 0 To aDtGousyaList.Rows.Count - 1
                    If StringUtil.IsNotEmpty(aDtGousyaList.Rows(j)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME)) Then
                        'ダミー列はカウントしない。
                        If aDtGousyaList.Rows(j)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME) <> "DUMMY" Then
                            lngCount = lngCount + 1
                        End If
                    End If
                Next
                If lngCountDt <> lngCount Then
                    Throw New Exception(String.Format("取込対象のExcelと号車数が一致していません。", newGousyaName))
                End If

                '号車名称チェック
                For i As Integer = 0 To dtNewGousyaList.Rows.Count - 1

                    Dim findFlag As Boolean = False
                    Dim lngGousyaCount As Long = 0
                    For j As Integer = 0 To aDtGousyaList.Rows.Count - 1
                        newGousyaName = dtNewGousyaList.Rows(i)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME).ToString
                        xlsGousyaName = aDtGousyaList.Rows(j)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME)

                        '空の列は異常な列とみなす
                        If xlsGousyaName Is Nothing Then
                            xlsGousyaName = String.Empty
                        End If

                        If newGousyaName.Trim.Equals(xlsGousyaName.Trim) Then
                            findFlag = True
                            '一致した号車の数をカウントする。
                            lngGousyaCount = lngGousyaCount + 1
                        End If

                    Next

                    '号車がダブっていたらThrowする。
                    If lngGousyaCount > 1 Then
                        Throw New Exception(String.Format("取込対象のExcelの号車がダブっています。", newGousyaName))
                    End If

                    '号車がヒットしなかったらThrowする。
                    If findFlag = False Then
                        Throw New Exception(String.Format("取込対象のExcelに必要な号車名称が存在しません。(必須列：{0})", newGousyaName))
                    End If

                Next

            Finally

                dtNewGousyaList.Dispose()

            End Try


            Return True

        End Function

#End Region

#End Region

#Region "納期一括設定"

#Region "納期一括設定(支給部品日付)"
        ''' <summary>
        ''' 支給部品日付一括設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetShikyuBuhinDate()
            Dim eSheet As Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(eSheet)
            Dim shikyuBuhinDate As String = Nothing
            Dim genchoKbn As Boolean = _frmNoukiIkkatsu.GetIsGencho

            '支給部品日付取得
            If _frmNoukiIkkatsu.GetIsKokunai Then
                '国内
                shikyuBuhinDate = _frmNoukiIkkatsu.GetShikyuBuhinDate(False)
            Else
                '現調
                shikyuBuhinDate = _frmNoukiIkkatsu.GetShikyuBuhinDate(True)
            End If

            '支給部品日付が格納されていなければ処理しないで戻る
            If shikyuBuhinDate.Equals(String.Empty) = True Then
                Return
            End If

            For i As Integer = startRow To eSheet.RowCount - 1
                'ブロックNoが入力されていない行で終了
                If eSheet.GetText(i, GetTagIdx(eSheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim.Equals(String.Empty) Then
                    Exit For
                End If

                '納期日付がセット済みなら次行へ
                If eSheet.GetText(i, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)).Trim.Equals(String.Empty) = False Then
                    Continue For
                End If

                '手配記号を取得
                Dim tehaiKigou As String = eSheet.GetText(i, GetTagIdx(eSheet, NmSpdTagBase.TAG_TEHAI_KIGOU)).Trim

                '国内
                If genchoKbn = False AndAlso (tehaiKigou.Equals("A") OrElse tehaiKigou.Equals("D")) Then

                    eSheet.SetText(i, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI), shikyuBuhinDate)
                    '編集書式設定
                    eSheet.Cells(i, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)).BackColor = Color.AliceBlue
                    SetEditRowProc(True, i, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI))

                ElseIf genchoKbn = True AndAlso tehaiKigou.Equals("B") Then
                    '現調品
                    eSheet.SetText(i, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI), shikyuBuhinDate)
                    eSheet.Cells(i, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)).BackColor = Color.AliceBlue
                    SetEditRowProc(True, i, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI))

                End If

            Next

        End Sub
#End Region

#Region "納期一括設定(取引先)"
        ''' <summary>
        ''' 納期一括設定実行(取引先)
        ''' 未確認
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetNoukiMaker()

            Dim eSheet As Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(eSheet)

            '取引先納期一括設定情報格納データテーブル取得
            Dim dtMakerNouki As DataTable = _frmNoukiIkkatsu.GetDtMakerNouki

            '一括設定情報データテーブル全ループ
            For i As Integer = 0 To dtMakerNouki.Rows.Count - 1

                'スプレッド全行ループ
                For j As Integer = startRow To eSheet.RowCount - 1
                    'ブロックNoが入力されていない行で終了
                    If eSheet.GetText(j, GetTagIdx(eSheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim.Equals(String.Empty) Then
                        Exit For
                    End If

                    '納期日付がセット済みなら次行へ
                    If eSheet.GetText(j, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)).Trim.Equals(String.Empty) = False Then
                        Continue For
                    End If

                    '手配記号を取得(スプレッドから）
                    Dim tehaiKigou As String = eSheet.GetText(j, GetTagIdx(eSheet, NmSpdTagBase.TAG_TEHAI_KIGOU)).Trim
                    '該当手配コード判定用
                    Dim setFlag As Boolean = False
                    '国内・現調品判定情報
                    Dim genchoKbn As Boolean = _frmNoukiIkkatsu.GetIsGencho

                    If genchoKbn = False AndAlso (tehaiKigou.Equals("J") = False AndAlso tehaiKigou.Equals("B") = False AndAlso tehaiKigou.Equals("F") = False) Then
                        '国内 (手配記号=J以外 かつ 手配記号=B以外かつ手配記号=F以外)
                        setFlag = True
                    ElseIf genchoKbn = True AndAlso (tehaiKigou.Equals("J") = True) Then
                        '現調品(手配記号=J )
                        setFlag = True
                    End If

                    '手配記号が対象コードか
                    If setFlag = True Then
                        '取引コード取得(一括設定画面から)
                        Dim makerDt As String = dtMakerNouki.Rows(i)(NmDtMaker.MAKERCODE)
                        '取引コード取得(スプレッド上)
                        Dim makerSp As String = eSheet.GetText(j, GetTagIdx(eSheet, NmSpdTagBase.TAG_TORIHIKISAKI_CODE)).Trim

                        '同じ取引コードか
                        If makerDt.Equals(makerSp) = True Then
                            '納入指示日にセット
                            eSheet.SetText(j, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI), dtMakerNouki.Rows(i)(NmDtMaker.NOUKI_SHITEIBI))
                            'セット確認用
                            eSheet.Cells(j, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)).BackColor = Color.Yellow
                            '編集書式設定
                            SetEditRowProc(True, j, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI))
                        End If

                    End If

                Next
            Next
        End Sub
#End Region

#Region "納期一括設定(購入品)"
        ''' <summary>
        ''' 納期一括設定(購入品)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetKounyuNouki()
            Dim eSheet As Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(eSheet)

            '購入品納期一括設定情報格納データテーブル取得
            Dim dtKonyuNouki As DataTable = _frmNoukiIkkatsu.GetDtKonyuNouki

            '一括設定情報データテーブル全ループ
            For i As Integer = 0 To dtKonyuNouki.Rows.Count - 1

                For j As Integer = startRow To eSheet.RowCount - 1

                    'スプレッド対象行のブロックNo取得
                    Dim spdBlockNo As String = eSheet.GetText(j, GetTagIdx(eSheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim

                    'ブロックNoが入力されていない行で終了
                    If spdBlockNo.Equals(String.Empty) Then
                        Exit For
                    End If

                    '納期日付がセット済みなら次行へ
                    If eSheet.GetText(j, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)).Trim.Equals(String.Empty) = False Then
                        Continue For
                    End If

                    '手配記号を取得
                    Dim tehaiKigou As String = eSheet.GetText(j, GetTagIdx(eSheet, NmSpdTagBase.TAG_TEHAI_KIGOU)).Trim
                    '該当手配コード判定用
                    Dim setFlag As Boolean = False
                    '国内・現調品判定情報
                    Dim genchoKbn As Boolean = _frmNoukiIkkatsu.GetIsGencho

                    If genchoKbn = False AndAlso (tehaiKigou.Equals(String.Empty) = True OrElse tehaiKigou.Equals("M") = True) Then
                        '国内 (手配記号=空白　または 手配記号=M)
                        setFlag = True
                    ElseIf genchoKbn = True AndAlso tehaiKigou.Equals("J") = True Then
                        '現調品(手配記号=J もしくは 手配記号=B)
                        setFlag = True
                    Else
                        '該当しないデータは次へ
                        Continue For
                    End If

                    '正規化検索用ブロック文字列取得
                    Dim regBlock As String = GetRegulurBlock(dtKonyuNouki.Rows(i)(NmDtKonyu.BLOCK))

                    'スプレッドのブロックNoが該当す一括設定のブロック範囲に該当するか
                    If Regex.IsMatch(spdBlockNo, regBlock) Then

                        '専用品・共用品チェック付き
                        If _frmNoukiIkkatsu.GetIsSenyouKyouyou = True Then

                            '専用記号取得
                            Dim senyouMark As String = eSheet.GetText(j, GetTagIdx(eSheet, NmSpdTagBase.TAG_SENYOU_MARK)).Trim
                            If senyouMark.Equals("*") AndAlso dtKonyuNouki.Rows(i)(NmDtKonyu.SENYOU_NOUKI).trim.Equals(String.Empty) = False Then

                                '専用品納期を納入指示日にセット
                                eSheet.SetText(j, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI), dtKonyuNouki.Rows(i)(NmDtKonyu.SENYOU_NOUKI))
                                'セット確認用
                                eSheet.Cells(j, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)).BackColor = Color.BlueViolet
                                '編集書式設定
                                SetEditRowProc(True, j, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI))

                            ElseIf senyouMark.Trim.Equals(String.Empty) = True AndAlso dtKonyuNouki.Rows(i)(NmDtKonyu.KYOYOU_NOUKI).Equals(String.Empty) = False Then
                                '専用記号が空白でかつ共用品日付が入力されている

                                '共用品納期納入指示日にセット
                                eSheet.SetText(j, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI), dtKonyuNouki.Rows(i)(NmDtKonyu.KYOYOU_NOUKI))
                                'セット確認用
                                eSheet.Cells(j, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)).BackColor = Color.DarkKhaki
                                '編集書式設定
                                SetEditRowProc(True, j, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI))
                            End If
                        Else
                            '専用品・共用品チェックが無い場合

                            '納期納入指示日にセット
                            eSheet.SetText(j, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI), dtKonyuNouki.Rows(i)(NmDtKonyu.NOUKI))
                            'セット確認用
                            eSheet.Cells(j, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)).BackColor = Color.LawnGreen
                            '編集書式設定
                            SetEditRowProc(True, j, GetTagIdx(eSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI))
                        End If
                    End If

                Next

            Next

        End Sub

#Region " 該当ブロック判定の為の正規表現を取得"
        ''' <summary>
        '''  該当ブロック判定の為の正規表現を取得
        ''' 
        ''' ブロックは以下の形式で格納されている
        '''  #1000
        '''  #2000
        '''       ・
        '''       ・
        ''' #9000
        ''' 一括設定
        ''' </summary>
        ''' <param name="aBlock"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetRegulurBlock(ByVal aBlock As String) As String
            Dim regChar As String = Nothing

            If aBlock.Substring(0, 1).Equals("#") Then
                regChar = String.Format("{0}{1}", "^", aBlock.Substring(1, 1))

            ElseIf aBlock.Equals("一括設定") Then
                regChar = String.Format("{0}", "^")  '全て通過する
            Else
                regChar = "BAD"                              '何も通過させない
            End If

            Return regChar
        End Function

#End Region

#End Region

#End Region

#Region "手配情報付加"

#Region "手配情報付加(部品番号に対しての設定及び図面設通番号取得設定)"
        ''' <summary>
        ''' 手配情報付加(部品番号に対しての設定)
        ''' </summary>
        ''' <param name="aEbomDb"></param>
        ''' <param name="aKouseiDb"></param>
        ''' <remarks></remarks>
        Public Sub TehaiFuka_RelatedBuhinNo(ByVal aEbomDb As SqlAccess, ByVal aKouseiDb As SqlAccess)
            Dim sheetGousya As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1
            Dim sheetBase As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheetBase)
            Dim kaihatsuFugo As String = _headerSubject.kaihatsuFugo

            For i As Integer = startRow To sheetBase.RowCount - 1
                'ブロックNoが入力されていない行で終了
                If sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim.Equals(String.Empty) Then
                    Exit For
                End If

                Dim buhinNo As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO)).Trim
                Dim seihinKbn As String = _headerSubject.seihinKbn

                '部品番号が空白の場合は次行へ
                If buhinNo.Equals(String.Empty) Then
                    Continue For
                End If

                '手配帳付加部品番号に対しての取得
                Dim tehaiFuka As New TehaichoFuka(aEbomDb, aKouseiDb, kaihatsuFugo, buhinNo, seihinKbn)

                '専用マーク設定
                Dim spreadMark As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SENYOU_MARK))

                '専用マークが空白の場合のみ編集書式をセット 空白なら見えないし、値がセットされる場合のみに意味が有るのでこれでいいはず
                If spreadMark Is Nothing = True OrElse spreadMark.Trim.Equals(String.Empty) = True Then
                    '書式セット
                    SetEditRowProc(IsBaseSpread, i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SENYOU_MARK))
                End If

                sheetBase.SetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SENYOU_MARK), tehaiFuka.SenyouMark)
                sheetGousya.SetText(i, GetTagIdx(sheetGousya, NmSpdTagGousya.TAG_SENYOU_MARK), tehaiFuka.SenyouMark)

                '購入担当取得
                Dim kouTan As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_KOUTAN)).Trim
                '取引先コード取得
                Dim torihiki As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TORIHIKISAKI_CODE)).Trim

                '両方ブランクの場合はDBの値を使用する
                If kouTan.Equals(String.Empty) AndAlso torihiki.Equals(String.Empty) Then
                    Dim tblKoutan As String = tehaiFuka.koutan
                    Dim tblTorihiki As String = tehaiFuka.Torihikisaki
                    Dim torihikiName As String = TehaichoEditImpl.FindPkRhac0610(tblTorihiki).Trim

                    '設定された値のセルに編集用書式を設定
                    SetEditRowProc(True, i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_KOUTAN))
                    SetEditRowProc(True, i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TORIHIKISAKI_CODE))
                    SetEditRowProc(True, i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_MAKER_CODE))

                    sheetBase.SetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_KOUTAN), tblKoutan)
                    sheetBase.SetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TORIHIKISAKI_CODE), tblTorihiki)
                    sheetBase.SetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_MAKER_CODE), torihikiName)
                End If

                '図面設通番号取得
                Dim settsuNo As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)).Trim
                'ブランクならDBから取得した値をセットする
                If settsuNo.Equals(String.Empty) Then
                    '設定された値のセルに編集用書式を設定
                    SetEditRowProc(True, i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA))
                    '値設定
                    sheetBase.SetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA), tehaiFuka.ZumenSettsu)
                End If

            Next

        End Sub
#End Region

#Region "手配情報付加(手配記号に対して)"
        ''' <summary>
        ''' 手配情報付加(手配記号に対して)
        ''' </summary>
        ''' <param name="aFrmFuka"></param>
        ''' <remarks></remarks>
        Public Sub TehaiFuka_RelatedTehaiSymbol(ByVal aFrmFuka As frm20DispTehaichoEditFuka, ByVal aEbomDb As SqlAccess, ByVal aKouseiDb As SqlAccess)

            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)
            Dim seihinKbn As String = _headerSubject.seihinKbn

            '基本スプレッド全行ループ
            For i As Integer = startRow To sheet.RowCount - 1

                '行IDが入力されていない行で終了
                If sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID)).Trim.Equals(String.Empty) Then
                    Exit For
                End If

                Dim tehaiKigou As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_TEHAI_KIGOU)).Trim
                Dim oyaBuhinNo As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_OYA)).Trim

                Dim newNouba As String = Nothing
                Dim newKyouKyuSection As String = Nothing


                Select Case tehaiKigou
                    Case NmTehaiConst.NmConst_TehaiKigou.F
                        'F
                        newNouba = String.Empty
                        newKyouKyuSection = String.Empty

                    Case String.Empty
                        '空白の場合は"空白"と格納されているため定数との比較となる
                        newNouba = NmTehaiConst.NmConst_Nouba.X1
                        newKyouKyuSection = NmTehaiConst.NmConst_KyoukyuSection.NO_9SH10

                    Case NmTehaiConst.NmConst_TehaiKigou.A
                        'A
                        '親レベルの取引コードを取得する
                        newKyouKyuSection = GetPrntTorihikiCode(sheet, i)
                        newNouba = NmTehaiConst.NmConst_Nouba.A0

                    Case NmTehaiConst.NmConst_TehaiKigou.D
                        'D
                        '親レベルの取引コードを取得する
                        newKyouKyuSection = GetPrntTorihikiCode(sheet, i)
                        newNouba = NmTehaiConst.NmConst_Nouba.X1

                    Case NmTehaiConst.NmConst_TehaiKigou.J
                        'J
                        newKyouKyuSection = NmTehaiConst.NmConst_KyoukyuSection.NO_9SH10
                        newNouba = NmTehaiConst.NmConst_Nouba.US

                    Case NmTehaiConst.NmConst_TehaiKigou.B
                        'B
                        '親レベルの取引コードを取得する
                        newKyouKyuSection = GetPrntTorihikiCode(sheet, i)
                        newNouba = NmTehaiConst.NmConst_Nouba.US

                    Case NmTehaiConst.NmConst_TehaiKigou.M
                        'M
                        newKyouKyuSection = NmTehaiConst.NmConst_KyoukyuSection.NO_9SS00
                        newNouba = NmTehaiConst.NmConst_Nouba.X1

                End Select

                Dim nouba As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUBA)).Trim
                Dim kyouKyuSection As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_KYOUKU_SECTION)).Trim

                '納場設定 値が未入力場合にのみ設定
                If nouba.Equals(String.Empty) Then
                    '編集書式セット
                    SetEditRowProc(True, i, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUBA))
                    sheet.SetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUBA), newNouba)
                End If

                '供給セクション設定　値が未入力場合にのみ設定
                If kyouKyuSection.Equals(String.Empty) Then
                    '編集書式セット
                    SetEditRowProc(True, i, GetTagIdx(sheet, NmSpdTagBase.TAG_KYOUKU_SECTION))
                    sheet.SetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_KYOUKU_SECTION), newKyouKyuSection)
                End If

            Next

        End Sub

#End Region

#Region "親レベルの取引先コードを取得"
        ''' <summary>
        ''' 親レベルの取引先コードを取得
        ''' 
        ''' ※取得出来ない場合はEmptyを返す
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetPrntTorihikiCode(ByVal aSheet As FarPoint.Win.Spread.SheetView, ByVal aRowNo As Integer) As String
            Dim torihikiCode As String = String.Empty
            Dim prntRow As Integer = -1
            Dim buhinNoOya As String
            Dim sheet As SheetView = _frmDispTehaiEdit.spdBase_Sheet1

            buhinNoOya = sheet.GetText(aRowNo, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_OYA)).Trim

            '部品番号(親)が無いと探せない'
            If Not StringUtil.IsEmpty(buhinNoOya) Then
                prntRow = GetRowBuhinNoOya(aSheet, aRowNo, buhinNoOya)

                If prntRow >= 0 Then
                    torihikiCode = aSheet.GetText(prntRow, GetTagIdx(aSheet, NmSpdTagBase.TAG_TORIHIKISAKI_CODE)).Trim
                End If

                If Not StringUtil.IsEmpty(torihikiCode) Then
                    torihikiCode = torihikiCode + "0"
                    Return torihikiCode
                Else
                    Return torihikiCode
                End If
            Else
                '部品番号(親)がいないので自身と同じブロックかつ、自身のレベルよりも上位かつ、直近の部品番号の取引先コードを使用'
                prntRow = GetRowBuhinNoOyaKari(aSheet, aRowNo)

                If prntRow >= 0 Then
                    torihikiCode = aSheet.GetText(prntRow, GetTagIdx(aSheet, NmSpdTagBase.TAG_TORIHIKISAKI_CODE)).Trim
                End If

                If Not StringUtil.IsEmpty(torihikiCode) Then
                    torihikiCode = torihikiCode + "0"
                    Return torihikiCode
                Else
                    Return torihikiCode
                End If

            End If



        End Function
#End Region

#Region "手配帳付加"
        ''' <summary>
        ''' 手配帳付加
        ''' </summary>
        ''' <param name="aFrmFuka"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function TeahiFuka(ByVal aFrmFuka As frm20DispTehaichoEditFuka) As Boolean

            Using ebomDb As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                ebomDb.Open()

                Using kouseidb As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
                    kouseidb.Open()

                    '部品番号に対して購担・取引先コードを設定
                    TehaiFuka_RelatedBuhinNo(ebomDb, kouseidb)
                    '手配記号に対して付加情報を設定
                    TehaiFuka_RelatedTehaiSymbol(aFrmFuka, ebomDb, kouseidb)

                    Return True

                End Using

            End Using

        End Function
#End Region

#End Region

#Region "部品番号変更入力処理"
        ''' <summary>
        ''' 部品番号変更入力処理
        ''' </summary>
        ''' <param name="aRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function changeBuhinNo(ByVal aRow As Integer) As Boolean
            Dim sheetBase As Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim sheetGousya As Spread.SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1

            Dim bukaCode As String = sheetBase.GetText(aRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)).Trim
            Dim blockNo As String = sheetBase.GetText(aRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim
            Dim tehaidate As ShisakuDate = New ShisakuDate

            Dim buhinstruct As New TehaichoEditBuhinStructure(_shisakuEventCode, _shisakuListCode, blockNo, bukaCode, tehaidate)

            Dim buhinNo As String = sheetBase.GetText(aRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO)).Trim
            Dim buhinNoKbn As String = sheetBase.GetText(aRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KBN)).Trim
            Dim strlevel As String = sheetBase.GetText(aRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_LEVEL)).Trim
            Dim intLevel As Integer = -1

            If strlevel.Equals(String.Empty) Then
                intLevel = 0
            Else
                intLevel = Integer.Parse(strlevel)
            End If

            '手配帳では部品構成取得は不要
            '２０１１・０３・３１　森さんより

            '部品構成取得
            'Dim newBuhinMatrix As BuhinKoseiMatrix = buhinstruct.GetKouseiMatrix(buhinNo, buhinNoKbn, intLevel)

            'If newBuhinMatrix Is Nothing OrElse newBuhinMatrix.Records.Count = 0 Then

            '    Return False

            'End If

            'If newBuhinMatrix.Records.Count >= 2 Then
            '    'スプレッド行追加(編集部品番号の下の行から追加)
            '    InsertSpread(aRow + 1, newBuhinMatrix.Records.Count - 1)
            'End If

            ''員数の格納された行リスト
            'Dim lstInsu As List(Of Integer) = newBuhinMatrix.GetInputInsuColumnIndexes

            'Dim destIndex As Integer = 0

            'For Each srcIndex As Integer In newBuhinMatrix.GetInputRowIndexes()
            '    With newBuhinMatrix(srcIndex)
            '        'レベル
            '        sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_LEVEL), .Level.ToString)
            '        sheetGousya.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_LEVEL), .Level.ToString)

            '        '部品番号
            '        sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO), .BuhinNo)
            '        sheetGousya.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO), .BuhinNo)

            '        '部品番号試作区分
            '        sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KBN), .BuhinNoKbn)
            '        sheetGousya.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KBN), .BuhinNoKbn)

            '        '部品改訂番号
            '        sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KAITEI_NO), .BuhinNoKaiteiNo.ToString)

            '        '部品枝番
            '        sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_EDA_BAN), .EdaBan)

            '        '部品名称
            '        sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NAME), .BuhinName)
            '        '集計コード
            '        If .ShukeiCode.Equals(String.Empty) Then
            '            sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUKEI_CODE), .SiaShukeiCode)
            '        Else
            '            sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUKEI_CODE), .ShukeiCode)
            '        End If

            '        '取引先コード
            '        sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TORIHIKISAKI_CODE), .MakerCode)
            '        sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SAISHIYOUFUKA), .Saishiyoufuka)
            '        sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE), .ShutuzuYoteiDate)
            '        sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1), .ZaishituKikaku1)
            '        sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2), .ZaishituKikaku2)
            '        sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3), .ZaishituKikaku3)
            '        sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_ZAISHITU_MEKKI), .ZaishituMekki)
            '        sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO), .ShisakuBankoSuryo)
            '        sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U), .ShisakuBankoSuryoU)

            '        sheetBase.SetValue(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUHINN_HI), .ShisakuBuhinHi)
            '        sheetBase.SetValue(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_KATA_HI), .ShisakuKataHi)
            '        sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_MAKER_CODE), .MakerName)
            '        sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BIKOU), .Bikou)

            '        'If lstInsu.Count <= 0 Then
            '        '    Continue For
            '        'End If

            '        'Dim totalInsu As Integer = 0

            '        'For k As Integer = 0 To lstInsu.Count - 1
            '        '    '号車員数合計
            '        '    totalInsu += newBuhinMatrix.InsuSuryo(srcIndex, lstInsu(k))
            '        '    '号車に対応した員数を設定
            '        '    '
            '        'Next

            '        'sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TOTAL_INSU_SURYO), newBuhinMatrix.InsuSuryo(srcIndex, 0))
            '        '親レコードから部品番号を取得
            '        Dim prntRecord As BuhinKoseiRecordVo = newBuhinMatrix.GetParentRecord(srcIndex, 0)

            '        If Not prntRecord Is Nothing Then
            '            sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_OYA), prntRecord.BuhinNo)
            '            sheetBase.SetText(aRow + srcIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KBN), prntRecord.BuhinNoKbn)
            '        End If

            '        '        Record(rowIndex + destIndex).Level = .Level
            '        '        Record(rowIndex + destIndex).ShukeiCode = .ShukeiCode
            '        '        Record(rowIndex + destIndex).SiaShukeiCode = .SiaShukeiCode
            '        '        Record(rowIndex + destIndex).GencyoCkdKbn = .GencyoCkdKbn
            '        '        Record(rowIndex + destIndex).MakerCode = .MakerCode
            '        '        Record(rowIndex + destIndex).MakerName = .MakerName
            '        '        Record(rowIndex + destIndex).BuhinNo = .BuhinNo
            '        '        Record(rowIndex + destIndex).BuhinName = .BuhinName
            '        '    End With
            '        '    destIndex += 1


            '    End With



            'Next
        End Function


#End Region

#Region "変更されたセルの列に対応した処理を行う"
        ''' <summary>
        ''' 変更されたセルの列に対応した処理を行う
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ChangeSpreadValueReflect(ByVal aRow As Integer, ByVal aCol As Integer, ByVal aColTag As String, ByVal aTextValue As String, ByVal aSupportValue As String)
            Dim sheet As FarPoint.Win.Spread.SheetView
            Dim hidSheet As FarPoint.Win.Spread.SheetView

            sheet = GetVisibleSheet
            hidSheet = GetHiddenSheet

            '空白除去( "* "この種の問題対応)
            aTextValue = aTextValue.Trim

            '見出し行からのイベント処理はスルー
            If aRow < GetTitleRowsIn(sheet) Then
                Exit Sub
            End If

            '編集されたセルは太文字・青文字にする
            SetEditRowProc(IsBaseSpread, aRow, aCol)

            '空行への入力(ブロックNo,部課コード,行ID,部品表示順のセット)
            Dim wGyouId As String = sheet.GetText(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID)).Trim
            '行IDの未入力で空行と判定
            If wGyouId.Equals(String.Empty) Then
                blankInput(aRow)
            End If

            '変更されたセルの列に対応した処理を行う
            Select Case aColTag

                Case NmSpdTagBase.TAG_SHISAKU_BUKA_CODE
                    '部課コード
                    If IsBaseSpread Then
                        hidSheet.SetValue(aRow, GetTagIdx(hidSheet, NmSpdTagGousya.TAG_SHISAKU_BUKA_CODE), aTextValue)
                    Else
                        hidSheet.SetValue(aRow, GetTagIdx(hidSheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE), aTextValue)
                    End If

                Case NmSpdTagBase.TAG_SENYOU_MARK
                    '専用マーク

                    If IsBaseSpread Then
                        '空文字を入れようとした場合には半角スペースで置き換える
                        If aTextValue.Equals(String.Empty) Then
                            aTextValue = " "
                            sheet.SetValue(aRow, GetTagIdx(hidSheet, NmSpdTagBase.TAG_SENYOU_MARK), aTextValue)
                        End If
                        hidSheet.SetValue(aRow, GetTagIdx(hidSheet, NmSpdTagGousya.TAG_SENYOU_MARK), aTextValue)

                    Else
                        '空文字を入れようとした場合には半角スペースで置き換える
                        If aTextValue.Equals(String.Empty) Then
                            aTextValue = " "
                            sheet.SetValue(aRow, GetTagIdx(hidSheet, NmSpdTagGousya.TAG_SENYOU_MARK), aTextValue)
                        End If
                        sheet.SetValue(aRow, GetTagIdx(hidSheet, NmSpdTagGousya.TAG_SENYOU_MARK), aTextValue)
                        hidSheet.SetValue(aRow, GetTagIdx(hidSheet, NmSpdTagBase.TAG_SENYOU_MARK), aTextValue)
                    End If

                Case NmSpdTagBase.TAG_LEVEL
                    'レベル
                    If IsBaseSpread Then
                        hidSheet.SetValue(aRow, GetTagIdx(hidSheet, NmSpdTagGousya.TAG_LEVEL), aTextValue)
                    Else
                        hidSheet.SetValue(aRow, GetTagIdx(hidSheet, NmSpdTagBase.TAG_LEVEL), aTextValue)
                    End If

                    '親部品番号・区分設定
                    'SetBuhinNoOya(aRow)

                Case NmSpdTagBase.TAG_BUHIN_NO
                    '入力サポート欄から呼ばれた場合、部品番号の構成取得処理はスルーさせる。
                    '一文字変更毎にチェックが入り遅いので。　2011/03/09　柳沼
                    If StringUtil.IsEmpty(aSupportValue) Then
                        '部品番号
                        If IsBaseSpread Then
                            hidSheet.SetValue(aRow, GetTagIdx(hidSheet, NmSpdTagGousya.TAG_BUHIN_NO), aTextValue)
                        Else
                            hidSheet.SetValue(aRow, GetTagIdx(hidSheet, NmSpdTagBase.TAG_BUHIN_NO), aTextValue)
                        End If

                        '部品構成取得処理へ
                        If changeBuhinNo(aRow) = True Then
                            '部品設定へ
                            System.Console.WriteLine("部品発見")

                        End If
                    End If
                Case NmSpdTagBase.TAG_BUHIN_NO_KBN
                    '部品区分
                    If IsBaseSpread Then
                        hidSheet.SetValue(aRow, GetTagIdx(hidSheet, NmSpdTagGousya.TAG_BUHIN_NO_KBN), aTextValue)
                    Else
                        hidSheet.SetValue(aRow, GetTagIdx(hidSheet, NmSpdTagBase.TAG_BUHIN_NO_KBN), aTextValue)
                    End If

                Case NmSpdTagBase.TAG_TORIHIKISAKI_CODE

                    If IsBaseSpread Then

                        '取引先コード
                        If aTextValue.Length = 4 Then
                            Dim newmakerName As String = TehaichoEditImpl.FindPkRhac0610(aTextValue).Trim
                            Dim befMakerName As String = sheet.GetText(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_MAKER_CODE)).Trim

                            '取引先名称が取得出来かつ、スプレッドの値が空欄であれば該当項目にセット
                            If newmakerName.Equals(String.Empty) = False AndAlso befMakerName.Equals(String.Empty) = True Then
                                '原因は不明だがこのSetValueを行うと入力補助欄の取引先コードが4桁～3桁に減らしセットされてしまう
                                '対処として呼び出したFrmのTxtInputイベントで元の値を確保しておき、桁落ちした文字列を置換える処理を
                                'いれている
                                sheet.SetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_MAKER_CODE), newmakerName)

                            End If

                        End If
                    End If
                Case NmSpdTagBase.TAG_NOUNYU_SHIJIBI
                    '納入指示日
                    Dim valNounyu As String = sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI))

                    'Nullや空文字の場合
                    If valNounyu Is Nothing OrElse valNounyu.ToString.Trim.Equals(String.Empty) Then
                        sheet.SetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI), String.Empty)
                    Else
                        Dim dateNounyu As Date = sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI))

                        'カレンダーから入力された値から時刻を取り除き格納する
                        If IsDate(dateNounyu) = True Then
                            sheet.SetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI), Format(dateNounyu, "yyyy/MM/dd"))
                        End If
                    End If
                    'オール１の場合背景色をグレー、以外は解除
                    '   （変化点が削以外のとき有効）
                    NounyuShijibiRowColorChange(aRow)

                Case NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE
                    '出図予定日
                    Dim valShutsuzu As String = sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE))

                    ''Nullや空文字の場合
                    If valShutsuzu Is Nothing OrElse valShutsuzu.ToString.Trim.Equals(String.Empty) Then
                        sheet.SetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE), String.Empty)
                    Else
                        Dim dateShutsuzu As Date = sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE))

                        'カレンダーから入力された値から時刻を取り除き格納する
                        If IsDate(dateShutsuzu) = True Then
                            sheet.SetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE), Format(dateShutsuzu, "yyyy/MM/dd"))
                        End If
                    End If
                    '↓↓↓2014/12/29 メタル項目を追加 TES)張 ADD BEGIN
                Case NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1
                    '材質・規格１
                    Dim valZaishituKikaku1 As String = sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1))
                    '抽出した「試作手配帳情報（基本情報）」の材質・規格１と入力した材質・規格１を比較して値が不一致の場合、該当行の材料情報・発注済のチェックを外す。
                    Dim tehaichoVo As TShisakuTehaiKihonVo = FindByTShisakuTehaiKihonVo(sheet, aRow)
                    If tehaichoVo IsNot Nothing Then
                        If Not StringUtil.Equals(valZaishituKikaku1, tehaichoVo.ZaishituKikaku1) Then
                            sheet.SetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK), False)
                        End If
                    End If
                Case NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2
                    '材質・規格２
                    Dim valZaishituKikaku2 As String = sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2))
                    '抽出した「試作手配帳情報（基本情報）」の材質・規格２と入力した材質・規格２を比較して値が不一致の場合、該当行の材料情報・発注済のチェックを外す。
                    Dim tehaichoVo As TShisakuTehaiKihonVo = FindByTShisakuTehaiKihonVo(sheet, aRow)
                    If tehaichoVo IsNot Nothing Then
                        If Not StringUtil.Equals(valZaishituKikaku2, tehaichoVo.ZaishituKikaku2) Then
                            sheet.SetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK), False)
                        End If
                    End If
                Case NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3
                    '材質・規格３
                    Dim valZaishituKikaku3 As String = sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3))
                    '抽出した「試作手配帳情報（基本情報）」の材質・規格３と入力した材質・規格３を比較して値が不一致の場合、該当行の材料情報・発注済のチェックを外す。
                    Dim tehaichoVo As TShisakuTehaiKihonVo = FindByTShisakuTehaiKihonVo(sheet, aRow)
                    If tehaichoVo IsNot Nothing Then
                        If Not StringUtil.Equals(valZaishituKikaku3, tehaichoVo.ZaishituKikaku3) Then
                            sheet.SetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK), False)
                        End If
                    End If
                Case NmSpdTagBase.TAG_ZAISHITU_MEKKI
                    '材質・メッキ
                    Dim valZaishituMekki As String = sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_MEKKI))
                    '抽出した「試作手配帳情報（基本情報）」の材質・メッキと入力した材質・メッキを比較して値が不一致の場合、該当行の材料情報・発注済のチェックを外す。
                    Dim tehaichoVo As TShisakuTehaiKihonVo = FindByTShisakuTehaiKihonVo(sheet, aRow)
                    If tehaichoVo IsNot Nothing Then
                        If Not StringUtil.Equals(valZaishituMekki, tehaichoVo.ZaishituMekki) Then
                            sheet.SetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK), False)
                        End If
                    End If
                Case NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO
                    '板厚
                    Dim valShisakuBankoSuryo As String = sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO))
                    '抽出した「試作手配帳情報（基本情報）」の板厚と入力した板厚を比較して値が不一致の場合、該当行の材料情報・発注済のチェックを外す。
                    Dim tehaichoVo As TShisakuTehaiKihonVo = FindByTShisakuTehaiKihonVo(sheet, aRow)
                    If tehaichoVo IsNot Nothing Then
                        If Not StringUtil.Equals(valShisakuBankoSuryo, tehaichoVo.ShisakuBankoSuryo) Then
                            sheet.SetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK), False)
                        End If
                    End If
                Case NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U
                    '板厚・ｕ
                    Dim valShisakuBankoSuryoU As String = sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U))
                    '抽出した「試作手配帳情報（基本情報）」の板厚・ｕと入力した板厚・ｕを比較して値が不一致の場合、該当行の材料情報・発注済のチェックを外す。
                    Dim tehaichoVo As TShisakuTehaiKihonVo = FindByTShisakuTehaiKihonVo(sheet, aRow)
                    If tehaichoVo IsNot Nothing Then
                        If Not StringUtil.Equals(valShisakuBankoSuryoU, tehaichoVo.ShisakuBankoSuryoU) Then
                            sheet.SetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK), False)
                        End If
                    End If
                Case NmSpdTagBase.TAG_DATA_ITEM_SET_NAME
                    'データ項目・セット名
                    Dim valDataItemSetName As String = sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME))
                    '抽出した「試作手配帳情報（基本情報）」のデータ項目・セット名と入力したデータ項目・セット名を比較して値が不一致の場合、該当行のデータ項目・データ支給チェック欄のチェックを外す。
                    Dim tehaichoVo As TShisakuTehaiKihonVo = FindByTShisakuTehaiKihonVo(sheet, aRow)
                    If tehaichoVo IsNot Nothing Then
                        If Not StringUtil.Equals(valDataItemSetName, tehaichoVo.DataItemSetName) Then
                            sheet.SetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION), False)
                        End If
                    End If
                    '↑↑↑2014/12/29 メタル項目を追加 TES)張 ADD END

                Case NmSpdTagBase.TAG_ZAIRYO_SUNPO_X, _
                     NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y, _
                     NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z

                    '初期
                    sheet.Cells(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY), _
                                aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY)).BackColor = Nothing
                    sheet.Cells(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ), _
                                aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ)).BackColor = Nothing
                    sheet.Cells(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ), _
                                aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ)).BackColor = Nothing
                    '
                    sheet.Cells(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY), _
                                aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY)).Font = New Font("MS UI Gothic", 9, FontStyle.Regular)
                    sheet.Cells(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ), _
                                aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ)).Font = New Font("MS UI Gothic", 9, FontStyle.Regular)
                    sheet.Cells(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ), _
                                aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ)).Font = New Font("MS UI Gothic", 9, FontStyle.Regular)
                    '
                    Dim sunpoX As Decimal = 0
                    If StringUtil.IsNotEmpty(sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X))) Then
                        sunpoX = CDec(sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X)))
                    End If
                    Dim sunpoY As Decimal = 0
                    If StringUtil.IsNotEmpty(sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y))) Then
                        sunpoY = CDec(sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y)))
                    End If
                    Dim sunpoZ As Decimal = 0
                    If StringUtil.IsNotEmpty(sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z))) Then
                        sunpoZ = CDec(sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z)))
                    End If
                    '自動計算
                    Dim sunpoXy As Decimal = sunpoX + sunpoY
                    sheet.SetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY), sunpoXy)
                    Dim sunpoXz As Decimal = sunpoX + sunpoZ
                    sheet.SetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ), sunpoXz)
                    Dim sunpoYz As Decimal = sunpoY + sunpoZ
                    sheet.SetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ), sunpoYz)

                    '最大値のセルは太文字・背景色をグレーにする
                    Dim sunpo(2) As Double
                    sunpo(0) = sunpoXy
                    sunpo(1) = sunpoXz
                    sunpo(2) = sunpoYz
                    Array.Sort(sunpo)
                    Select Case sunpo(2)
                        Case 0
                            '最大値が0の場合スルー
                        Case sunpoXy
                            sheet.Cells(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY), _
                                        aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY)).BackColor = Color.Yellow
                            sheet.Cells(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY), _
                                        aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY)).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                        Case sunpoXz
                            sheet.Cells(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ), _
                                        aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ)).BackColor = Color.Yellow
                            sheet.Cells(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ), _
                                        aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ)).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                        Case sunpoYz
                            sheet.Cells(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ), _
                                        aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ)).BackColor = Color.Yellow
                            sheet.Cells(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ), _
                                        aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ)).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                    End Select
            End Select

            '非表示スプレッドもアクティブセル位置を表示スプレッドと合わせる
            'アクティブ位置を選択ブロックへ

            '1/25スクロール位置を両スプレッドで合わせる為に以下行は不要
            'hidSheet.SetActiveCell(aRow, hidSheet.ActiveColumn.Index)
            'hidSpread.ShowActiveCell(Spread.VerticalPosition.Top, Spread.HorizontalPosition.Left)

        End Sub

        '↓↓↓2014/12/29 メタル項目を追加 TES)張 ADD BEGIN
        ''' <summary>
        ''' 入力行のキーにより、試作手配帳情報（基本情報）を取得する
        ''' </summary>
        ''' <param name="sheet"></param>
        ''' <param name="aRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function FindByTShisakuTehaiKihonVo(ByVal sheet As FarPoint.Win.Spread.SheetView, ByVal aRow As Integer) As TShisakuTehaiKihonVo
            Dim blockNo As String = sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO))
            Dim bukaCode As String = sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE))
            Dim buhinHyoujiJun As String = sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN))
            Dim tehaichoDao As TShisakuTehaiKihonDao = New TShisakuTehaiKihonDaoImpl

            Return tehaichoDao.FindByPk(_headerSubject.shisakuEventCode, _
                                        _headerSubject.shisakuListCode, _
                                        _headerSubject.shisakuListCodeKaiteiNo, _
                                        bukaCode, _
                                        blockNo, _
                                        CInt(buhinHyoujiJun))
        End Function
        '↑↑↑2014/12/29 メタル項目を追加 TES)張 ADD END

#Region "親部品番号・親部品番号区分設定"
        ''' <summary>
        ''' 親部品番号・親部品番号区分設定
        ''' </summary>
        ''' <param name="aRow"></param>
        ''' <remarks></remarks>
        Private Sub SetBuhinNoOya(ByVal aRow As Integer)
            Dim sheetBase As FarPoint.Win.Spread.SheetView
            Dim visiSheet As FarPoint.Win.Spread.SheetView
            visiSheet = GetVisibleSheet
            sheetBase = _frmDispTehaiEdit.spdBase_Sheet1

            Dim oyaLevelRowNo As Integer = TehaichoEditLogic.GetRowParentLevel(visiSheet, aRow)
            Dim oyaBuhinNo As String = String.Empty
            Dim oyaBuhinNoKbn As String = String.Empty

            '表示スプレッドから親部品番号を取得
            If oyaLevelRowNo >= 0 Then
                oyaBuhinNo = visiSheet.GetText(oyaLevelRowNo, GetTagIdx(visiSheet, NmSpdTagBase.TAG_BUHIN_NO))
                oyaBuhinNoKbn = visiSheet.GetText(oyaLevelRowNo, GetTagIdx(visiSheet, NmSpdTagBase.TAG_BUHIN_NO_KBN))
            End If

            '編集されたセルは太文字・青文字にする
            SetEditRowProc(True, aRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_OYA))
            SetEditRowProc(True, aRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KBN_OYA))

            '親部品・区分値設定
            sheetBase.SetText(aRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_OYA), oyaBuhinNo)
            sheetBase.SetText(aRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KBN_OYA), oyaBuhinNoKbn)


        End Sub
#End Region

#End Region

#Region "基本・号車両対象セルに背景色を付ける"
        ''' <summary>
        '''  基本・号車両対象セルに背景色を付ける
        ''' </summary>
        ''' <param name="aRow"></param>
        ''' <param name="aTagName"></param>
        ''' <param name="aColor"></param>
        ''' <remarks></remarks>
        Private Sub SetColorErrorCheck(ByVal aRow As Integer, ByVal aTagName As String, ByVal aColor As Color)
            Dim sheetBase As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim sheetGousya As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1

            sheetBase.Cells(aRow, GetTagIdx(sheetBase, aTagName)).BackColor = aColor
            sheetGousya.Cells(aRow, GetTagIdx(sheetGousya, aTagName)).BackColor = aColor

        End Sub

#End Region

#Region "保存機能"

#Region "保存前入力データチェック"
        ''' <summary>
        '''  保存前入力データチェック
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SaveDataCheck() As Boolean
            Dim sheetBase As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim sheetGousya As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheetBase)

            'エラー対象セル
            Dim errCell As Spread.Cell = Nothing
            Dim errFlag As Boolean = False
            Dim firstErrCell As Spread.Cell = Nothing
            '空行後入力判定用
            Dim empRowNo As Integer = -1


            '試作手配基本スプレッドループ
            For i As Integer = startRow To sheetBase.RowCount - startRow - 1
                '基本スプレッドのエラー色を戻す
                For j As Integer = 0 To sheetBase.ColumnCount - 1
                    sheetBase.Cells(i, j).BackColor = Nothing
                Next
                '号車スプレッドのエラー色を戻す
                For j As Integer = 0 To sheetGousya.ColumnCount - 1
                    sheetGousya.Cells(i, j).BackColor = Nothing
                Next

                '行ID有無でデータ行終了判定を行う

                Dim gyoID As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_GYOU_ID)).ToString.Trim
                If gyoID.Equals(String.Empty) = True Then
                    empRowNo = i
                    Continue For

                End If

                '空行が存在した後にデータ格納行を見つけたらエラー
                If empRowNo >= startRow Then
                    sheetBase.Cells(empRowNo, 0, empRowNo, sheetBase.ColumnCount - 1).BackColor = Color.Red
                    sheetGousya.Cells(empRowNo, 0, empRowNo, sheetGousya.ColumnCount - 1).BackColor = Color.Red
                    errCell = sheetGousya.Cells(i, GetTagIdx(sheetGousya, NmSpdTagGousya.TAG_INSU_SA))
                    errFlag = True
                End If

                '部課コード必須確認
                Dim bukaCode As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)).Trim

                If bukaCode.Equals(String.Empty) Then

                    SetColorErrorCheck(i, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE, Color.Red)
                    errCell = sheetBase.Cells(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE))
                    errFlag = True

                End If

                'ブロックNo入力形式
                Dim blockNo As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).ToString.Trim

                '2011/02/03 ブロックNo形式チェックは解除
                'If IsBlockNo(blockNo) = False Then
                '    SetColorErrorCheck(i, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO, Color.Red)
                '    errCell = sheetBase.Cells(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO))
                '    errFlag = True
                If blockNo.Equals(String.Empty) Then
                    SetColorErrorCheck(i, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO, Color.Red)
                    errFlag = True

                End If

                '専用マーク入力形式
                Dim senyouMark As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SENYOU_MARK)).Trim

                If senyouMark.Equals(String.Empty) = False AndAlso senyouMark.Equals("*") = False Then
                    SetColorErrorCheck(i, NmSpdTagBase.TAG_SENYOU_MARK, Color.Red)
                    errCell = sheetBase.Cells(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SENYOU_MARK))
                    errFlag = True
                Else
                    SetColorErrorCheck(i, NmSpdTagBase.TAG_SENYOU_MARK, Nothing)
                End If

                '部品番号(とりあえず入力有無のみ
                Dim buhinNo As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO)).Trim

                If buhinNo.Equals(String.Empty) Then

                    SetColorErrorCheck(i, NmSpdTagBase.TAG_BUHIN_NO, Color.Red)
                    errCell = sheetBase.Cells(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO))
                    errFlag = True

                End If

                'レベル
                Dim level As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_LEVEL)).Trim

                '空白でなく数値に変換できるか
                If level.Equals(String.Empty) = True OrElse IsNumeric(level) = False Then
                    SetColorErrorCheck(i, NmSpdTagBase.TAG_LEVEL, Color.Red)
                    errCell = sheetBase.Cells(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_LEVEL))
                    errFlag = True

                End If

                '入力文字数チェック(これは遅いだろう）
                For k As Integer = 0 To sheetBase.ColumnCount - 1
                    If CheckCellLength(sheetBase, i, k) = False Then

                        sheetBase.Cells(i, k).BackColor = Color.Red

                        '部品番号区分列までは号車列も色を変える
                        If k <= GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KBN) Then
                            sheetGousya.Cells(i, k).BackColor = Color.Red
                        End If

                        errCell = sheetBase.Cells(i, k)
                        errFlag = True
                    End If
                Next

                '員数差入力内容チェック
                Dim rireki As String = sheetGousya.GetText(i, GetTagIdx(sheetGousya, NmSpdTagBase.TAG_RIREKI)).Trim
                Dim insuSa As String = sheetGousya.GetText(i, GetTagIdx(sheetGousya, NmSpdTagGousya.TAG_INSU_SA)).Trim

                If rireki.Equals("*") Then
                    If Not insuSa.Equals(String.Empty) AndAlso Not insuSa.Equals("**") Then
                        sheetGousya.Cells(i, GetTagIdx(sheetGousya, NmSpdTagGousya.TAG_INSU_SA)).BackColor = Color.Red
                        errCell = sheetGousya.Cells(i, GetTagIdx(sheetGousya, NmSpdTagGousya.TAG_INSU_SA))
                        errFlag = True
                    End If
                End If

                '号車シート員数入力チェック
                Dim startCol As Integer = TehaichoEditNames.START_COLUMMN_GOUSYA_NAME

                For j As Integer = startCol To startCol + _dtGousyaNameList.Rows.Count - 1

                    Dim strInsu As String = sheetGousya.GetText(i, j).Trim

                    '員数値入力内容チェック
                    If Not strInsu.Equals("**") = True AndAlso Not IsNumeric(strInsu) = True AndAlso strInsu.Equals(String.Empty) = False Then
                        sheetGousya.Cells(i, j).BackColor = Color.Red
                        errCell = sheetGousya.Cells(i, j)
                        errFlag = True

                    End If

                    '↓↓2014/10/06 酒井 ADD BEGIN
                    '補用品不具合展開
                    If strInsu.Equals("0") = True Then
                        sheetGousya.Cells(i, j).BackColor = Color.Red
                        errCell = sheetGousya.Cells(i, j)
                        errFlag = True

                    End If
                    '↑↑2014/10/06 酒井 ADD END
                Next

                '最初のエラーをアクティブセルにする為に保持
                If firstErrCell Is Nothing AndAlso Not errCell Is Nothing Then
                    firstErrCell = errCell
                End If

            Next

            'エラー存在有無
            If errFlag = True Then

                'エラー位置をアクティブにする
                sheetBase.SetActiveCell(firstErrCell.Row.Index, errCell.Column.Index)
                sheetGousya.SetActiveCell(firstErrCell.Row.Index, errCell.Column.Index)

                _frmDispTehaiEdit.spdBase.ShowActiveCell(Spread.VerticalPosition.Top, Spread.HorizontalPosition.Left)
                _frmDispTehaiEdit.spdGouSya.ShowActiveCell(Spread.VerticalPosition.Top, Spread.HorizontalPosition.Center)

                ComFunc.ShowInfoMsgBox("入力値にエラーが有るため保存を中止しました。")
                Return False
            End If

            Return True

        End Function

#End Region

#Region "保存機能全体処理"

        ''' <summary>
        ''' 保存機能
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' 
        Public Function Save() As Boolean

            Dim watch As New Stopwatch()

            watch.Start()

            '入力データチェック
            If SaveDataCheck() = False Then
                Return False
            End If

            watch.Stop()
            Console.WriteLine(String.Format("入力データチェック-実行時間 : {0}ms", watch.ElapsedMilliseconds))
            watch.Reset()
            watch.Start()

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                '－トランザクション開始－
                db.BeginTransaction()

                '試作リストコード情報更新
                If SaveShisakuListCode(db) = False Then
                    db.Rollback()
                    Return False
                End If

                watch.Stop()
                Console.WriteLine(String.Format("試作リストコード情報更新-実行時間 : {0}ms", watch.ElapsedMilliseconds))
                watch.Reset()

                watch.Start()

                '試作手配帳更新
                If SaveShisakuTehaichBase(db) = False Then
                    db.Rollback()
                    Return False
                End If

                watch.Stop()
                Console.WriteLine(String.Format("試作手配帳更新-実行時間 : {0}ms", watch.ElapsedMilliseconds))
                watch.Reset()

                watch.Start()

                '試作手配号車更新
                If SaveShisakuTehaiGousya(db) = False Then
                    Return False
                End If


                watch.Stop()
                Console.WriteLine(String.Format("試作手配号車更新-実行時間 : {0}ms", watch.ElapsedMilliseconds))
                watch.Reset()

                watch.Start()


                db.Commit()

                watch.Stop()
                Console.WriteLine(String.Format("コミット-実行時間 : {0}ms", watch.ElapsedMilliseconds))
                watch.Reset()

            End Using

            Return True

        End Function

#End Region

#Region "保存機能(試作リストコード)"
        ''' <summary>
        ''' 保存機能(試作リストコード)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SaveShisakuListCode(ByVal aDb As SqlAccess) As Boolean

            Dim result As Boolean
            result = TehaichoEditImpl.UpdShisakuList(aDb, _
                                                    _frmDispTehaiEdit.txtKoujiNo.Text.Trim, _
                                                    _headerSubject.shisakuEventCode, _
                                                    _headerSubject.shisakuListCode, _
                                                    _headerSubject.shisakuListCodeKaiteiNo _
                                                    )


            Return result

        End Function
#End Region

#Region "保存機能(試作手配帳)"

#Region "保存機能(試作手配帳全体処理)"
        ''' <summary>
        ''' 保存機能(メインループ)
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SaveShisakuTehaichBase(ByVal aDb As SqlAccess) As Boolean
            Dim aDate As New ShisakuDate
            Dim sheetBase As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim sheetGousya As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheetBase)

            Dim watch As New Stopwatch()

            watch.Start()

            'マッチング用DBデータテーブル取得
            Dim dtTehaiBase As DataTable = TehaichoEditImpl.FindAllBaseInfo _
                        (aDb, _shisakuEventCode, _shisakuListCode)
            'マッチング用スプレッドデータテーブル取得
            Dim dtSpreadBase As DataTable = GetDtSpread(aDb)


            watch.Stop()
            Console.WriteLine(String.Format("   マッチング用データテーブル取得-実行時間 : {0}ms", watch.ElapsedMilliseconds))
            watch.Reset()

            watch.Start()



            'DBテーブルとスプレッドのマッチングを行い存在しなくなったデータを削除する
            For i As Integer = 0 To dtTehaiBase.Rows.Count - 1

                Dim blockNo As String = dtTehaiBase.Rows(i)(NmDTColBase.TD_SHISAKU_BLOCK_NO).ToString
                '編集対象ブロックNoで無ければ次行へ
                If IsEditBlockNo(blockNo) = False Then
                    Continue For
                End If

                'スプレッド上を探索し対象データレコードが存在するか確認
                If FindDeleteRecord(aDb, dtTehaiBase.Rows(i), dtSpreadBase) = False Then
                    Return False
                End If

            Next

            watch.Stop()
            Console.WriteLine(String.Format("  マッチングを行い存在しなくなったデータを削除する-実行時間 : {0}ms", watch.ElapsedMilliseconds))
            watch.Reset()

            watch.Start()



            '試作手配基本を順番に読み取り編集したブロックの場合、追加か更新を行う
            For i As Integer = 0 To dtSpreadBase.Rows.Count - 1
                '編集対象ブロックNoで無ければ次行へ
                '2011/06/27 柳沼　コピペした場合、編集対象ブロック№が立たない。
                '更新されない現象があるので以下を一時的にコメントにする。
                'Dim blockNo As String = dtSpreadBase.Rows(i)(NmDTColBase.TD_SHISAKU_BLOCK_NO).ToString.Trim
                'If IsEditBlockNo(blockNo) = False Then
                '    Continue For
                'End If

                '主キー検索により処理判定を行う
                If FindDbUpdateBase(aDb, dtSpreadBase, i, dtTehaiBase) = False Then
                    Return False
                End If

            Next


            watch.Stop()
            Console.WriteLine(String.Format("  試作手配基本を追加か更新を行う-実行時間 : {0}ms", watch.ElapsedMilliseconds))
            watch.Reset()

            watch.Start()

            '試作手配帳基本-UNIT-KBN SEIHINKBN更新
            If SaveShisakuTehaiBaseUnitKbnSeihinKbn(aDb) = False Then
                Return False
            End If

            watch.Stop()
            Console.WriteLine(String.Format("  -UNIT-KBN SEIHINKBN更新-実行時間 : {0}ms", watch.ElapsedMilliseconds))
            watch.Reset()

            Return True

        End Function
#End Region

#Region "スプレッドのキー項目をデータテーブルに格納"
        ''' <summary>
        ''' スプレッドのキー項目をデータテーブルに格納
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetDtSpread(ByVal aDb As SqlAccess) As DataTable
            Dim sheet As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            'データ型を取得
            Dim dtResult As DataTable = TehaichoEditImpl.FindAllBaseInfo(aDb, "Z9999", "ZZZZZZ")
            dtResult.Rows.Clear()

            For i As Integer = startRow To sheet.RowCount - 1
                Dim row As DataRow = dtResult.NewRow

                '基本情報スプレッド側
                Dim blockNo As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO))
                If blockNo.Equals(String.Empty) Then
                    Continue For
                End If

                row(NmDTColBase.TD_SHISAKU_BLOCK_NO) = blockNo
                row(NmDTColBase.TD_SHISAKU_BUKA_CODE) = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE))
                row(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN) = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN))
                row(NmDTColBase.TD_BUHIN_NO) = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO))
                dtResult.Rows.Add(row)

            Next

            Return dtResult
        End Function
#End Region

#Region "ブロックNo出現回数取得(ソート順算出)"
        ''' <summary>
        ''' ブロックNo出現回数取得(ソート順算出)
        ''' </summary>
        ''' <param name="aDtSpreadBase">スプレッド情報格納データテーブル</param>
        ''' <param name="aIdxNo">探索終着行位置</param>
        ''' <param name="aBlockNo">対象ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetBlockNoFindCount(ByVal aDtSpreadBase As DataTable, ByVal aIdxNo As Integer, ByVal aBlockNo As String) As Integer
            Dim findCnt As Integer = 0
            Dim sheet As SheetView = _frmDispTehaiEdit.spdBase_Sheet1

            For i As Integer = 0 To aIdxNo
                Dim workBlockNo As String = aDtSpreadBase.Rows(i)(NmDTColBase.TD_SHISAKU_BLOCK_NO).ToString.Trim
                'ブロックNo出現回数をカウント
                If aBlockNo.Trim.Equals(workBlockNo) Then
                    findCnt += 1
                End If
            Next

            Return findCnt

        End Function
#End Region

#Region "試作手配帳基本-UNIT-KBN SEIHINKBN更新"
        ''' <summary>
        ''' 試作手配帳基本-UNIT-KBN SEIHINKBN更新
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SaveShisakuTehaiBaseUnitKbnSeihinKbn(ByVal aDb As SqlAccess) As Boolean
            Dim result As Boolean = False
            '試作手配基本のUNIT_KBNとSHISAKU_SEIHIN_KBNを更新する
            result = TehaichoEditImpl.UpdShisakuTehaiBaseEventListKey( _
                                                                                                    aDb, _
                                                                                                    _dtShikuEvent.Rows(0)(NmTdColShisakuEvent.UNIT_KBN), _
                                                                                                     _headerSubject.seihinKbn, _
                                                                                                     _shisakuEventCode, _
                                                                                                     _shisakuListCode, _
                                                                                                      _headerSubject.shisakuListCodeKaiteiNo)


            Return result


        End Function

#End Region

#Region "試作手配帳基本 主キー検索により更新処理判定"
        ''' <summary>
        ''' 試作手配帳基本 主キー検索により更新処理判定
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <param name="aDtSpreadBase">対象スプレッド行の情報を格納したDataRow</param>
        ''' <param name="aIdxNo">スプレッド行位置</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' 
        Private Function FindDbUpdateBase(ByVal aDb As SqlAccess, ByVal aDtSpreadBase As DataTable, ByVal aIdxNo As Integer, ByVal aDtBase As DataTable) As Boolean
            Dim findFlag As Boolean = False

            Dim blockNo As String = aDtSpreadBase.Rows(aIdxNo)(NmDTColBase.TD_SHISAKU_BLOCK_NO)
            Dim bukaCode As String = aDtSpreadBase.Rows(aIdxNo)(NmDTColBase.TD_SHISAKU_BUKA_CODE)
            Dim buhinHyoujiJun As String = aDtSpreadBase.Rows(aIdxNo)(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN)
            Dim sortJun As Integer = GetBlockNoFindCount(aDtSpreadBase, aIdxNo, blockNo)
            Dim sheet As SheetView = GetVisibleSheet
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            Dim dtResult As DataTable = TehaichoEditImpl.FindPkBaseInfo(aDb, _
                                                                                    _headerSubject.shisakuEventCode, _
                                                                                    _headerSubject.shisakuListCode, _
                                                                                    _headerSubject.shisakuListCodeKaiteiNo, _
                                                                                    bukaCode, _
                                                                                    blockNo, _
                                                                                    buhinHyoujiJun)
            If dtResult.Rows.Count = 1 Then
                findFlag = True
            Else
                findFlag = False
            End If

            'Dbテーブル上に該当データを探す
            'For i As Integer = 0 To aDtBase.Rows.Count - 1
            '    Dim tBukacode As String = aDtBase.Rows(i)(NmDTColBase.TD_SHISAKU_BUKA_CODE)
            '    Dim tBlockNo As String = aDtBase.Rows(i)(NmDTColBase.TD_SHISAKU_BLOCK_NO)
            '    Dim tBuhinNoHyoujiJun As String = aDtBase.Rows(i)(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN)

            '    If bukaCode.Equals(tBukacode) AndAlso blockNo.Equals(tBlockNo) AndAlso buhinHyoujiJun.Equals(tBuhinNoHyoujiJun) Then
            '        findFlag = True
            '        Exit For
            '    End If
            'Next

            'スプレッド行位置取得
            Dim spdRow As Integer = aIdxNo + GetTitleRowsIn(_frmDispTehaiEdit.spdBase_Sheet1)

            If findFlag = True Then
                '更新
                '↓↓↓2014/12/29 メタル項目を追加 TES)張 CHG BEGIN
                Dim tehaichoDao As TShisakuTehaiKihonDao = New TShisakuTehaiKihonDaoImpl
                Dim tehaichoVo As TShisakuTehaiKihonVo = tehaichoDao.FindByPk(_headerSubject.shisakuEventCode, _
                                                                              _headerSubject.shisakuListCode, _
                                                                              _headerSubject.shisakuListCodeKaiteiNo, _
                                                                              bukaCode, _
                                                                              blockNo, _
                                                                              CInt(buhinHyoujiJun))
                'If TehaichoEditImpl.UpdTehaiBase(_headerSubject, aDb, _frmDispTehaiEdit.spdBase_Sheet1, spdRow, sortJun) = False Then
                If TehaichoEditImpl.UpdTehaiBase(_headerSubject, aDb, _frmDispTehaiEdit.spdBase_Sheet1, spdRow, sortJun, tehaichoVo) = False Then
                    Return False
                End If
                '↑↑↑2014/12/29 メタル項目を追加 TES)張 CHG END

            Else
                '追加
                Dim unitKbn As String = _dtShikuEvent.Rows(0)(NmTdColShisakuEvent.UNIT_KBN)
                If TehaichoEditImpl.InsTehaiBase(_headerSubject, unitKbn, aDb, _frmDispTehaiEdit.spdBase_Sheet1, spdRow, sortJun) = False Then
                    Return False
                End If

            End If

            Return True

        End Function

#End Region

#Region "試作手配帳基本 DELETE-INSERT処理"
        ''' <summary>
        ''' 試作手配帳基本 DELETE-INSERT処理
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SaveShisakuTehaiBaseDeleteInsert(ByVal aDb As SqlAccess) As Boolean

            Dim sheetBase As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheetBase)

            '編集ブロックNoのDelete処理（試作イベント、リストコード、改訂コード、ブロックNoにより削除を行う）
            For i As Integer = 0 To ListEditCount - 1

                Dim blockNo As String = listEditBlock(i)
                Dim bukacode As String = listEditBukaCode(i)

                Dim resultCnt As Integer = TehaichoEditImpl.DelTehaichoBaseBlockNo( _
                                                                         aDb, _
                                                                          _headerSubject.shisakuEventCode, _
                                                                          _headerSubject.shisakuListCode, _
                                                                          _headerSubject.shisakuListCodeKaiteiNo, _
                                                                          bukacode, _
                                                                          blockNo)

            Next

            Dim sortJun As Integer = 0
            Dim workBlockNo As String = sheetBase.GetText(startRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).ToString.Trim

            '試作手配基本追加スプレッド全ループ
            For i As Integer = startRow To sheetBase.RowCount - 1

                Dim bukaCode As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)).Trim
                Dim blockNo As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim
                Dim buhinHyoujiJun As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN)).Trim

                '編集対象ブロックNoで無ければ次行へ
                If IsEditBlockNo(blockNo) = False Then
                    Continue For
                End If

                If blockNo.Equals(String.Empty) = True Then
                    Exit For
                ElseIf blockNo.Equals(workBlockNo) = False Then
                    sortJun = 0
                End If

                'Insert処理
                Dim unitKbn As String = _dtShikuEvent.Rows(0)(NmTdColShisakuEvent.UNIT_KBN)
                If TehaichoEditImpl.InsTehaiBase(_headerSubject, unitKbn, aDb, sheetBase, i, sortJun) = False Then
                    '結果異常
                    Return False
                End If

                workBlockNo = blockNo
                sortJun += 1

            Next

            Return True
        End Function

#End Region

#Region "試作手配帳号車 DELETE-INSERT処理"
        ''' <summary>
        ''' 試作手配帳号車 DELETE-INSERT処理
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SaveShisakuTehaiGousya(ByVal aDb As SqlAccess) As Boolean

            Dim sheetGousha As SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheetGousha)

            '編集ブロックNoのDelete処理（試作イベント、リストコード、改訂コード、ブロックNoにより削除を行う）
            'カウンターは-しない'
            For i As Integer = 0 To ListEditCount - 1

                Dim bukacode As String = listEditBukaCode(i)
                Dim blockNo As String = listEditBlock(i)

                Dim resultCnt As Integer = TehaichoEditImpl.DelTehaichoGousyaBlockNo( _
                                                                         aDb, _
                                                                          _headerSubject.shisakuEventCode, _
                                                                          _headerSubject.shisakuListCode, _
                                                                          _headerSubject.shisakuListCodeKaiteiNo, _
                                                                          bukacode, _
                                                                          blockNo)

            Next

            Dim mtNounyu As Integer = 0

            Dim sortJun As Integer = 0
            Dim workBlockNo As String = sheetGousha.GetText(startRow, GetTagIdx(sheetGousha, NmSpdTagGousya.TAG_SHISAKU_BLOCK_NO)).ToString.Trim

            '試作手配号車追加スプレッド号車列全ープ
            For i As Integer = startRow To sheetGousha.RowCount - 1

                Dim bukaCode As String = sheetGousha.GetText(i, GetTagIdx(sheetGousha, NmSpdTagGousya.TAG_SHISAKU_BUKA_CODE)).Trim
                Dim blockNo As String = sheetGousha.GetText(i, GetTagIdx(sheetGousha, NmSpdTagGousya.TAG_SHISAKU_BLOCK_NO)).Trim
                Dim buhinHyoujiJun As String = sheetGousha.GetText(i, GetTagIdx(sheetGousha, NmSpdTagGousya.TAG_BUHIN_NO_HYOUJI_JUN)).Trim

                '編集対象ブロックNoで無ければ次行へ
                If IsEditBlockNo(blockNo) = False Then
                    Continue For
                End If

                If blockNo.Equals(String.Empty) = True Then
                    Exit For
                ElseIf blockNo.Equals(workBlockNo) = False Then
                    sortJun = 0
                End If

                'Insert処理
                If TehaichoEditImpl.InsTehaiGousya(_headerSubject, aDb, sheetGousha, i, sortJun, _dtGousyaNameList) = False Then
                    '結果異常
                    Return False
                End If

                workBlockNo = blockNo
                sortJun += 1

                If mtNounyu = 0 Then
                    'ここで部課コード、ブロック№、行IDがブランクの号車情報のMT納入区分へ更新を行う。
                    If TehaichoEditImpl.UpdTehaiGousya(_headerSubject, aDb, _dtGousyaNameList) = False Then
                        '結果異常
                        Return False
                    End If
                End If

                mtNounyu += 1
            Next

            Return True
        End Function

#End Region

#Region "行削除されたレコードをテーブルから削除"
        ''' <summary>
        ''' 行削除されたレコードをテーブルから削除
        ''' </summary>
        ''' <param name="aTableDataRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function FindDeleteRecord(ByVal aDb As SqlAccess, ByVal aTableDataRow As DataRow, ByVal aDtSpreadBase As DataTable) As Boolean

            '削除された行か確認する
            Dim findFlag As Boolean = IsDeleteRow(aDb, aTableDataRow, aDtSpreadBase)

            'スプレッド上にDB取得データレコードが存在しないため削除を行う
            If findFlag = False Then

                'DELETE 試作手配(基本) -号車情報はDELETE-INSERTで行う
                If TehaichoEditImpl.DelTehaichoBasebuhinNo(aDb, aTableDataRow) = False Then
                    Return False
                End If

            End If

            Return True

        End Function

#End Region

#End Region

#End Region

#Region "データテーブルからスプレッドのデータを検索し有無を確認する"
        ''' <summary>
        ''' 探そうとしているデータが存在するか確認
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <param name="aTableDataRow"></param>
        ''' <param name="aDtSpreadBase"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function IsDeleteRow(ByVal aDb As SqlAccess, ByVal aTableDataRow As DataRow, ByVal aDtSpreadBase As DataTable) As Boolean
            Dim findFlag As Boolean = False

            'スプレッド上を探索し対象データレコードが存在するか確認
            For j As Integer = 0 To aDtSpreadBase.Rows.Count - 1
                'DB側
                Dim blockNo_a As String = aTableDataRow(NmDTColBase.TD_SHISAKU_BLOCK_NO)
                Dim buhinNoHyoujiJun_a As String = aTableDataRow(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN)
                Dim bukaCode_a As String = aTableDataRow(NmDTColBase.TD_SHISAKU_BUKA_CODE)

                '基本情報スプレッド側
                Dim blockNo_b As String = aDtSpreadBase.Rows(j)(NmDTColBase.TD_SHISAKU_BLOCK_NO)
                Dim buhinNoHyoujiJun_b As String = aDtSpreadBase.Rows(j)(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN)
                Dim bukaCode_b As String = aDtSpreadBase.Rows(j)(NmDTColBase.TD_SHISAKU_BUKA_CODE)

                '合致する行を探索する
                If blockNo_a.Equals(blockNo_b) _
                        AndAlso buhinNoHyoujiJun_a.Equals(buhinNoHyoujiJun_b) _
                        AndAlso bukaCode_a.Equals(bukaCode_b) Then

                    findFlag = True
                    Exit For
                End If

            Next

            Return findFlag

        End Function

#End Region

#Region "クローズ処理"
        ''' <summary>
        ''' クローズ処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub close()

            If Not _dtGousyaNameList Is Nothing Then
                _dtGousyaNameList.Dispose()
            End If

        End Sub
#End Region

        '↓↓2014/12/30 メタルブロックExcel取込を追加 TES)張 ADD BEGIN

        ''' <summary>
        ''' 変更点が”削”にする
        ''' </summary>
        ''' <param name="sheet"></param>
        ''' <param name="rowIndex"></param>
        ''' <remarks></remarks>
        Private Sub SetDeleteRowDisabled(ByVal sheet As SheetView, ByVal rowIndex As Integer)
            '試作リストコード改訂№が000以外の場合、変化点に"削"を更新
            sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_HENKATEN)).Value = "削"
            '背景色をグレーにする
            sheet.Rows(rowIndex).BackColor = Color.Gray
            '該当行をロック
            For colIndex As Integer = 0 To sheet.Columns.Count - 1
                sheet.Cells(rowIndex, colIndex).Locked = True
            Next
        End Sub

#Region "メタルブロックExcel取込機能"
#Region "メタルブロックExcel取込機能(メイン処理)"
        ''' <summary>
        ''' メタルブロックExcel取込機能(メイン処理)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub MetaruBlockImport()
            'ファイルパス取得
            Dim fileName As String = ImportFileDialog()

            If fileName.Equals(String.Empty) Then
                Return
            End If
            Cursor.Current = Cursors.WaitCursor

            'Excelファイからのインポート処理
            If MetaruBlockImportExcelFile(fileName) = False Then
                Return
            End If

            '   処理中画面表示
            Dim SyorichuForm1 As New frm03Syorichu
            SyorichuForm1.Label4.Text = "確認"
            SyorichuForm1.lblKakunin.Text = ""
            SyorichuForm1.lblKakunin2.Text = "処理中・・・"
            SyorichuForm1.lblKakunin3.Text = ""
            SyorichuForm1.Execute()
            SyorichuForm1.Show()

            MetaruBlockImportUpdate()

            '編集対象ブロックを設定
            ExcelImpAddEditBlock()

            'Excelファイル上で削除された行を探索し編集対象ブロックに追加する(要するにExcel上で削除されると色々厄介なのです）
            Me.DataMatchDeleteRec()

            Application.DoEvents()
            '   処理中画面非表示
            SyorichuForm1.Close()

            'クローズ処理イベント取得用クラス
            Dim closer As frm00Kakunin.IFormCloser = New CopyFormCloser(Me, fileName)

            '取込内容確認フォームを表示
            frm00Kakunin.ConfirmShow("EXCEL取込の確認", "EXCELの情報を確認してください。", _
                                           "EXCELのデータを反映しますか？", "EXCELを反映", "取込前に戻す", closer)
        End Sub
#End Region

#Region "メタルブロック更新時の処理"
        ''' <summary>
        ''' メタルブロック更新時の処理
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub MetaruBlockImportUpdate()
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startSpdRow As Integer = GetTitleRowsIn(sheet)
            Dim kaihatsuFugo As String = _headerSubject.kaihatsuFugo
            Dim seihinKbn As String = _headerSubject.seihinKbn

            'ここで取込前のバックアップを実行する。
            ' 取込前バックアップ
            importBackup()

            _frmDispTehaiEdit.Refresh()

            If _MetaruBlockList.Count > 0 Then
                Dim dataCount As Integer

                For rowIndex As Integer = startSpdRow To sheet.RowCount - 1
                    If StringUtil.IsEmpty(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO))) Then
                        Exit For
                    End If

                    '部品番号異なるのフラグ
                    Dim isSameBuhin As Boolean = True
                    '発注済フラグ
                    Dim isHatchuCheck As Boolean = True
                    '部品毎の差分
                    For excelIndex As Integer = 0 To _MetaruBlockList.Count - 1
                        '同一ブロックの場合
                        If StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)), _MetaruBlockList(excelIndex).ShisakuBlockNo) Then
                            'レベルと部品番号は同じ
                            If StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_LEVEL)), _MetaruBlockList(excelIndex).Level.Value.ToString) And _
                               StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO)), _MetaruBlockList(excelIndex).BuhinNo) Then
                                '削除なのデータが有る
                                If String.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_HENKATEN)), "削") Then
                                    Continue For
                                End If
                                'Excelのブロック情報表示
                                '材質・規格１
                                If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).ZaishituKikaku1) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1)), _MetaruBlockList(excelIndex).ZaishituKikaku1) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1), _MetaruBlockList(excelIndex).ZaishituKikaku1)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1))
                                        isHatchuCheck = False
                                    End If
                                End If
                                '材質・規格２
                                If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).ZaishituKikaku2) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2)), _MetaruBlockList(excelIndex).ZaishituKikaku2) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2), _MetaruBlockList(excelIndex).ZaishituKikaku2)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2))
                                        isHatchuCheck = False
                                    End If
                                End If
                                '材質・規格３
                                If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).ZaishituKikaku3) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3)), _MetaruBlockList(excelIndex).ZaishituKikaku3) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3), _MetaruBlockList(excelIndex).ZaishituKikaku3)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3))
                                        isHatchuCheck = False
                                    End If
                                End If
                                '材質・メッキ
                                If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).ZaishituMekki) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_MEKKI)), _MetaruBlockList(excelIndex).ZaishituMekki) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_MEKKI), _MetaruBlockList(excelIndex).ZaishituMekki)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_MEKKI))
                                        isHatchuCheck = False
                                    End If
                                End If
                                '板厚
                                If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).ShisakuBankoSuryo) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO)), _MetaruBlockList(excelIndex).ShisakuBankoSuryo) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO), _MetaruBlockList(excelIndex).ShisakuBankoSuryo)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO))
                                        isHatchuCheck = False
                                    End If
                                End If
                                '板厚・ｕ
                                If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).ShisakuBankoSuryoU) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U)), _MetaruBlockList(excelIndex).ShisakuBankoSuryoU) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U), _MetaruBlockList(excelIndex).ShisakuBankoSuryoU)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U))
                                        isHatchuCheck = False
                                    End If
                                End If
                                '材料情報・製品長
                                If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).MaterialInfoLength) Then
                                    If Not sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_LENGTH)) = _MetaruBlockList(excelIndex).MaterialInfoLength.Value.ToString Then
                                        sheet.SetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_LENGTH), _MetaruBlockList(excelIndex).MaterialInfoLength.Value)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_LENGTH))
                                        isHatchuCheck = False
                                    End If
                                End If
                                '材料情報・製品幅
                                If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).MaterialInfoWidth) Then
                                    If Not sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_WIDTH)) = _MetaruBlockList(excelIndex).MaterialInfoWidth.Value.ToString Then
                                        sheet.SetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_WIDTH), _MetaruBlockList(excelIndex).MaterialInfoWidth.Value)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_WIDTH))
                                        isHatchuCheck = False
                                    End If
                                End If
                                '発注済
                                If Not isHatchuCheck Then
                                    sheet.SetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK), False)
                                End If
                                'データ項目・改訂№	
                                If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).DataItemKaiteiNo) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO)), _MetaruBlockList(excelIndex).DataItemKaiteiNo) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO), _MetaruBlockList(excelIndex).DataItemKaiteiNo)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO))
                                    End If
                                End If
                                '項目・エリア名
                                If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).DataItemAreaName) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME)), UCase(_MetaruBlockList(excelIndex).DataItemAreaName)) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME), _MetaruBlockList(excelIndex).DataItemAreaName)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME))
                                    End If
                                End If
                                'データ項目・セット名
                                If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).DataItemSetName) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME)), UCase(_MetaruBlockList(excelIndex).DataItemSetName)) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME), _MetaruBlockList(excelIndex).DataItemSetName)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME))
                                        'データ項目・データ支給チェック欄
                                        sheet.SetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION), False)
                                    End If
                                End If

                                isSameBuhin = True
                                Exit For
                            Else
                                isSameBuhin = False
                            End If
                        End If
                    Next
                    '不要な行を削除
                    If Not isSameBuhin Then
                        If String.Equals(_headerSubject.shisakuListCodeKaiteiNo, "000") Then
                            '試作リストコード改訂№が000の場合、該当行を削除
                            sheet.RemoveRows(rowIndex, 1)
                            '号車シートも削除
                            _frmDispTehaiEdit.spdGouSya_Sheet1.RemoveRows(rowIndex, 1)
                            rowIndex = rowIndex - 1
                        Else
                            '該当行の変更点が”削”にする
                            SetDeleteRowDisabled(sheet, rowIndex)
                        End If
                    End If
                Next

                '新しい行を追加
                For excelIndex As Integer = 0 To _MetaruBlockList.Count - 1
                    Dim isAdd As Boolean = True
                    Dim hasSameBlock As Boolean = False
                    Dim insertIndex As Integer
                    dataCount = 0
                    For rowIndex As Integer = startSpdRow To sheet.RowCount - 1
                        If StringUtil.IsEmpty(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO))) Then
                            dataCount = rowIndex
                            Exit For
                        End If
                        If StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)), _MetaruBlockList(excelIndex).ShisakuBlockNo) Then
                            insertIndex = rowIndex
                            hasSameBlock = True
                            If StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_LEVEL)), _MetaruBlockList(excelIndex).Level.Value.ToString) And _
                               StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO)), _MetaruBlockList(excelIndex).BuhinNo) Then
                                '削除なのデータが有る
                                If String.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_HENKATEN)), "削") Then
                                    Continue For
                                End If
                                isAdd = False
                                Exit For
                            End If
                        End If
                    Next
                    If isAdd Then
                        If hasSameBlock Then
                            insertIndex = insertIndex + 1
                        Else
                            insertIndex = dataCount
                        End If
                        sheet.AddRows(insertIndex, 1)
                        '号車シートも追加
                        _frmDispTehaiEdit.spdGouSya_Sheet1.AddRows(insertIndex, 1)
                        DoubleBorderInsuSa(insertIndex, insertIndex)
                        SetEditRowProc(True, insertIndex, 0, 1, sheet.ColumnCount)

                        '発注対象のチェックボックスのチェックを外した場合、発注済のチェックボックスは使用不可（チェック不可）にする。
                        sheet.Cells(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)).Locked = True
                        'ブロック
                        If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).ShisakuBlockNo) Then
                            sheet.SetText(insertIndex, sheet.Columns(NmSpdTagBase.TAG_SHISAKU_BLOCK_NO).Index, _MetaruBlockList(excelIndex).ShisakuBlockNo)
                        End If
                        'レベル
                        If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).Level) Then
                            sheet.SetText(insertIndex, sheet.Columns(NmSpdTagBase.TAG_LEVEL).Index, _MetaruBlockList(excelIndex).Level)
                        End If
                        '部品番号
                        If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).BuhinNo) Then
                            sheet.SetText(insertIndex, sheet.Columns(NmSpdTagBase.TAG_BUHIN_NO).Index, _MetaruBlockList(excelIndex).BuhinNo)
                        End If
                        '材質・規格１
                        If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).ZaishituKikaku1) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1), _MetaruBlockList(excelIndex).ZaishituKikaku1)
                        End If
                        '材質・規格２
                        If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).ZaishituKikaku2) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2), _MetaruBlockList(excelIndex).ZaishituKikaku2)
                        End If
                        '材質・規格３
                        If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).ZaishituKikaku3) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3), _MetaruBlockList(excelIndex).ZaishituKikaku3)
                        End If
                        '材質・メッキ
                        If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).ZaishituMekki) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_MEKKI), _MetaruBlockList(excelIndex).ZaishituMekki)
                        End If
                        '板厚
                        If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).ShisakuBankoSuryo) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO), _MetaruBlockList(excelIndex).ShisakuBankoSuryo)
                        End If
                        '板厚・ｕ
                        If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).ShisakuBankoSuryoU) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U), _MetaruBlockList(excelIndex).ShisakuBankoSuryoU)
                        End If
                        '材料情報・製品長
                        If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).MaterialInfoLength) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_LENGTH), _MetaruBlockList(excelIndex).MaterialInfoLength)
                        End If
                        '材料情報・製品幅
                        If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).MaterialInfoWidth) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_WIDTH), _MetaruBlockList(excelIndex).MaterialInfoWidth)
                        End If
                        'データ項目・改訂№	
                        If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).DataItemKaiteiNo) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO), _MetaruBlockList(excelIndex).DataItemKaiteiNo)
                        End If
                        '項目・エリア名
                        If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).DataItemAreaName) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME), _MetaruBlockList(excelIndex).DataItemAreaName)
                        End If
                        'データ項目・セット名
                        If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).DataItemSetName) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME), _MetaruBlockList(excelIndex).DataItemSetName)
                        End If
                        '部品名称
                        If StringUtil.IsNotEmpty(_MetaruBlockList(excelIndex).BuhinName) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NAME), _MetaruBlockList(excelIndex).BuhinName)
                        End If
                        '合計員数
                        sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_TOTAL_INSU_SURYO), 0)
                        '部課コード
                        Dim r0080Vo As Rhac0080Vo
                        Dim SekkeiBlockDao As New SekkeiBlockDaoImpl
                        r0080Vo = SekkeiBlockDao.FindTantoBushoByBlock(kaihatsuFugo, _MetaruBlockList(excelIndex).ShisakuBlockNo)
                        If r0080Vo IsNot Nothing Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE), r0080Vo.TantoBusho)
                        Else
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE), "ZZZZ")
                        End If

                        '手配帳付加部品番号に対しての取得
                        Using ebomDb As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                            ebomDb.Open()

                            Using kouseidb As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
                                kouseidb.Open()

                                '部品番号が空白の場合は次行へ
                                If _MetaruBlockList(excelIndex).BuhinNo Is Nothing Then
                                    Continue For
                                End If
                                If _MetaruBlockList(excelIndex).BuhinNo.Equals(String.Empty) Then
                                    Continue For
                                End If
                                Dim tehaiFuka As New TehaichoFuka(ebomDb, kouseidb, kaihatsuFugo, _MetaruBlockList(excelIndex).BuhinNo, seihinKbn)
                                '専用
                                If StringUtil.IsNotEmpty(tehaiFuka.SenyouMark) Then
                                    sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SENYOU_MARK), tehaiFuka.SenyouMark)
                                End If
                                '購坦
                                If StringUtil.IsNotEmpty(tehaiFuka.SenyouMark) Then
                                    sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KOUTAN), tehaiFuka.koutan)
                                End If
                                '取引先コード
                                '取引先名称
                                If StringUtil.IsNotEmpty(tehaiFuka.Torihikisaki) Then
                                    sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_TORIHIKISAKI_CODE), tehaiFuka.Torihikisaki)
                                    sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_KATA_HI), TehaichoEditImpl.FindPkRhac0610(tehaiFuka.Torihikisaki.Trim))
                                End If
                                '図面設通№	
                                If StringUtil.IsNotEmpty(tehaiFuka.ZumenSettsu) Then
                                    sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA), tehaiFuka.ZumenSettsu)
                                End If
                            End Using
                        End Using
                    End If
                Next

                '行IDを設定
                Dim gyouId As Integer = 0
                Dim strBlock As String = String.Empty
                dataCount = 0
                For rowIndex As Integer = startSpdRow To sheet.RowCount - 1
                    If StringUtil.IsEmpty(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO))) Then
                        dataCount = rowIndex
                        Exit For
                    End If

                    If String.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)), strBlock) Then
                        gyouId = gyouId + 1
                    Else
                        gyouId = 1
                    End If
                    strBlock = sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO))
                    If Not String.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID)), gyouId.ToString("000")) Then
                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID), gyouId.ToString("000"))
                        '部品番号表示順に行ID-1の値をセットする。By柳沼（行IDは001から、部品番号表示順は0から・・）
                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN), gyouId - 1)
                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID))
                    End If
                Next

                sheet.RowCount = dataCount + 10 - startSpdRow
                _frmDispTehaiEdit.spdGouSya_Sheet1.RowCount = sheet.RowCount

            End If
        End Sub
#End Region

#Region "メタルブロックExcel取込機能(Excelオープン)"
        ''' <summary>
        ''' メタルブロックExcel取込機能(Excelオープン)
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <remarks></remarks>
        Private Function MetaruBlockImportExcelFile(ByVal fileName As String) As Boolean
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim kaihatsuFugo As String = _headerSubject.kaihatsuFugo
            Dim seihinKbn As String = _headerSubject.seihinKbn
            _MetaruBlockList = New List(Of TShisakuTehaiKihonVo)
            If ShisakuComFunc.IsFileOpen(fileName) Then
                Return False
            End If
            Using xls As New ShisakuExcel(fileName)
                '列項目位置チェック、桁数チェック
                If Not MetaruBlockImportDataCheck(_MetaruBlockList, xls) Then
                    'エラーメッセージ表示
                    _MetaruBlockList.Clear()
                    'Dim errMsg As String = "メタルブロック更新用のEXCELではありません。ご確認ください。"
                    Dim errMsg As String = "指定されたEXCELファイルに不正な個所があります。ご確認ください。"
                    ComFunc.ShowErrMsgBox(errMsg)
                    xls.Save()
                    Return False
                End If
            End Using
            Return True
        End Function
#End Region

#Region "Excel取込機能(列項目位置チェック)"
        ''' <summary>
        ''' 列項目位置チェック、桁数チェック
        ''' </summary>
        ''' <param name="aVolist"></param>
        ''' <param name="xls"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MetaruBlockImportDataCheck(ByVal aVolist As List(Of TShisakuTehaiKihonVo), ByVal xls As ShisakuExcel) As Boolean
            Dim rtnVal As Boolean = True
            Const MsgCol As Integer = 29


            'データ無しの場合
            Dim xlsEndRow As Integer = 0
            For i As Integer = xls.EndRow To 8 Step -1
                If StringUtil.IsNotEmpty(xls.GetValue(METARU_BLOCK, i)) Then
                    xlsEndRow = i
                    Exit For
                End If
            Next
            If xlsEndRow = 0 Then
                ComFunc.ShowErrMsgBox("行数が0です。")
                Return False
            End If

            '列数不満の場合
            Dim xlsEndCol As Integer = 0
            For i As Integer = xls.EndCol To METARU_BLOCK Step -1
                If StringUtil.IsNotEmpty(xls.GetValue(i, 3)) Then
                    xlsEndCol = i
                    Exit For
                End If
            Next
            If xlsEndCol < METARU_DATAITEMSETNAME Then
                ComFunc.ShowErrMsgBox("列数が不正です。")
                Return False
            End If

            Dim gyouId As Integer = 0
            Dim lastBlockNo As String = String.Empty
            For RowIndex As Integer = 8 To xlsEndRow
                Dim Vo As New TShisakuTehaiKihonVo
                'ブロック№
                Dim strBlock = ""
                If StringUtil.IsNotEmpty(xls.GetValue(METARU_BLOCK, RowIndex)) Then
                    strBlock = xls.GetValue(METARU_BLOCK, RowIndex).ToString.Trim
                End If
                If StringUtil.IsNotEmpty(strBlock) Then
                    'If Not ShisakuComFunc.IsInLength(strBlock, 4) And Not StrErrCheck(strBlock) Then
                    If Not ShisakuComFunc.IsInLength(strBlock, 4) Then
                        xls.SetValue(MsgCol, RowIndex, xls.GetValue(MsgCol, RowIndex) & " Error=レベル(4)")
                        xls.SetBackColor(METARU_BLOCK, RowIndex, METARU_BLOCK, RowIndex, RGB(255, 0, 0))
                    End If
                    Vo.ShisakuBlockNo = strBlock
                Else
                    Continue For
                End If

                '行ID
                If String.Equals(lastBlockNo, strBlock) Then
                    gyouId = gyouId + 1
                Else
                    gyouId = 1
                End If
                Vo.GyouId = (gyouId).ToString("000")
                lastBlockNo = strBlock
                'レベル
                Dim Level As Integer
                If StringUtil.IsNotEmpty(xls.GetValue(METARU_LEVEL, RowIndex)) And IsNumeric(xls.GetValue(METARU_LEVEL, RowIndex)) Then
                    Level = StringUtil.ToInteger(xls.GetValue(METARU_LEVEL, RowIndex).ToString.Trim)
                    If Not ShisakuComFunc.IsInLength(Level.ToString, 4) Then
                        xls.SetValue(MsgCol, RowIndex, xls.GetValue(MsgCol, RowIndex) & " Error=レベル(4)")
                        xls.SetBackColor(METARU_LEVEL, RowIndex, METARU_LEVEL, RowIndex, RGB(255, 0, 0))
                        rtnVal = False
                    End If
                    '部品名称
                    Dim BuhinName As String = ""
                    If StringUtil.IsNotEmpty(xls.GetValue(Level + 3, RowIndex)) Then
                        BuhinName = xls.GetValue(Level + 3, RowIndex).ToString.Trim
                        'If Not ShisakuComFunc.IsInLength(BuhinName, 20) Or Not StrErrCheck(BuhinName) Then
                        If Not ShisakuComFunc.IsInLength(BuhinName, 20) Then
                            xls.SetValue(MsgCol, RowIndex, xls.GetValue(MsgCol, RowIndex) & " Error=部品名称(20)")
                            xls.SetBackColor(Level + 3, RowIndex, Level + 3, RowIndex, RGB(255, 0, 0))
                            rtnVal = False
                        End If
                        Vo.BuhinName = BuhinName
                    End If
                    Vo.Level = Level
                End If
                '部品番号
                Dim BuhinNo As String = ""
                If StringUtil.IsNotEmpty(xls.GetValue(METARU_BUHINNO, RowIndex)) Then
                    BuhinNo = xls.GetValue(METARU_BUHINNO, RowIndex).ToString.Trim
                    'If Not ShisakuComFunc.IsInLength(BuhinNo, 15) Or Not StrErrCheck(BuhinNo) Then
                    If Not ShisakuComFunc.IsInLength(BuhinNo, 15) Then
                        xls.SetValue(MsgCol, RowIndex, xls.GetValue(MsgCol, RowIndex) & " Error>部品番号(15)")
                        xls.SetBackColor(METARU_BUHINNO, RowIndex, METARU_BUHINNO, RowIndex, RGB(255, 0, 0))
                        rtnVal = False
                    End If
                    Vo.BuhinNo = BuhinNo
                End If
                '材質・規格１
                Dim ZaishituKikaku1 As String = ""
                If StringUtil.IsNotEmpty(xls.GetValue(METARU_ZAISHITU_KIKAKU_1, RowIndex)) Then
                    ZaishituKikaku1 = xls.GetValue(METARU_ZAISHITU_KIKAKU_1, RowIndex).ToString.Trim
                    'If Not ShisakuComFunc.IsInLength(ZaishituKikaku1, 4) Or Not StrErrCheck(ZaishituKikaku1) Then
                    If Not ShisakuComFunc.IsInLength(ZaishituKikaku1, 4) Then
                        xls.SetValue(MsgCol, RowIndex, xls.GetValue(MsgCol, RowIndex) & " Error>材質・規格１(4)")
                        xls.SetBackColor(METARU_ZAISHITU_KIKAKU_1, RowIndex, METARU_ZAISHITU_KIKAKU_1, RowIndex, RGB(255, 0, 0))
                        rtnVal = False
                        Return False
                    End If
                    Vo.ZaishituKikaku1 = ZaishituKikaku1
                End If
                '材質・規格２
                Dim ZaishituKikaku2 As String = ""
                If StringUtil.IsNotEmpty(xls.GetValue(METARU_ZAISHITU_KIKAKU_2, RowIndex)) Then
                    ZaishituKikaku2 = xls.GetValue(METARU_ZAISHITU_KIKAKU_2, RowIndex).ToString.Trim
                    'If Not ShisakuComFunc.IsInLength(ZaishituKikaku2, 4) Or Not StrErrCheck(ZaishituKikaku2) Then
                    If Not ShisakuComFunc.IsInLength(ZaishituKikaku2, 4) Then
                        xls.SetValue(MsgCol, RowIndex, xls.GetValue(MsgCol, RowIndex) & " Error>材質・規格２(4)")
                        xls.SetBackColor(METARU_ZAISHITU_KIKAKU_2, RowIndex, METARU_ZAISHITU_KIKAKU_2, RowIndex, RGB(255, 0, 0))
                        rtnVal = False
                    End If
                    Vo.ZaishituKikaku2 = ZaishituKikaku2
                End If
                '材質・規格３
                Dim ZaishituKikaku3 As String = ""
                If StringUtil.IsNotEmpty(xls.GetValue(METARU_ZAISHITU_KIKAKU_3, RowIndex)) Then
                    ZaishituKikaku3 = xls.GetValue(METARU_ZAISHITU_KIKAKU_3, RowIndex).ToString.Trim
                    'If Not ShisakuComFunc.IsInLength(ZaishituKikaku3, 2) Or Not StrErrCheck(ZaishituKikaku3) Then
                    If Not ShisakuComFunc.IsInLength(ZaishituKikaku3, 2) Then
                        xls.SetValue(MsgCol, RowIndex, xls.GetValue(MsgCol, RowIndex) & " Error>材質・規格３(2)")
                        xls.SetBackColor(METARU_ZAISHITU_KIKAKU_3, RowIndex, METARU_ZAISHITU_KIKAKU_3, RowIndex, RGB(255, 0, 0))
                        rtnVal = False
                    End If
                    Vo.ZaishituKikaku3 = ZaishituKikaku3
                End If
                '材質・規格４
                Dim ZaishituMekki As String = ""
                If StringUtil.IsNotEmpty(xls.GetValue(METARU_ZAISHITU_KIKAKU_4, RowIndex)) Then
                    ZaishituMekki = xls.GetValue(METARU_ZAISHITU_KIKAKU_4, RowIndex).ToString.Trim
                    'If Not ShisakuComFunc.IsInLength(ZaishituMekki, 6) Or Not StrErrCheck(ZaishituMekki) Then
                    If Not ShisakuComFunc.IsInLength(ZaishituMekki, 6) Then
                        xls.SetValue(MsgCol, RowIndex, xls.GetValue(MsgCol, RowIndex) & " Error>材質・規格４(6)")
                        xls.SetBackColor(METARU_ZAISHITU_KIKAKU_4, RowIndex, METARU_ZAISHITU_KIKAKU_4, RowIndex, RGB(255, 0, 0))
                        rtnVal = False
                    End If
                    Vo.ZaishituMekki = ZaishituMekki
                End If
                '板厚
                Dim BankoSuryo As String = ""
                If StringUtil.IsNotEmpty(xls.GetValue(METARU_SHISAKU_BANKO_SURYO, RowIndex)) Then
                    BankoSuryo = xls.GetValue(METARU_SHISAKU_BANKO_SURYO, RowIndex).ToString.Trim
                    'If Not ShisakuComFunc.IsInLength(BankoSuryo, 5) Or Not StrErrCheck(BankoSuryo) Then
                    If Not ShisakuComFunc.IsInLength(BankoSuryo, 5) Then
                        xls.SetValue(MsgCol, RowIndex, xls.GetValue(MsgCol, RowIndex) & " Error>板厚(5)")
                        xls.SetBackColor(METARU_SHISAKU_BANKO_SURYO, RowIndex, METARU_SHISAKU_BANKO_SURYO, RowIndex, RGB(255, 0, 0))
                        rtnVal = False
                    End If
                    Vo.ShisakuBankoSuryo = BankoSuryo
                End If
                '板厚・ｕ
                Dim BankoSuryoU As String = ""
                If StringUtil.IsNotEmpty(xls.GetValue(METARU_SHISAKU_BANKO_SURYO_U, RowIndex)) Then
                    BankoSuryoU = xls.GetValue(METARU_SHISAKU_BANKO_SURYO_U, RowIndex).ToString.Trim
                    'If Not ShisakuComFunc.IsInLength(BankoSuryoU, 1) Or Not StrErrCheck(BankoSuryoU) Then
                    If Not ShisakuComFunc.IsInLength(BankoSuryoU, 1) Then
                        xls.SetValue(MsgCol, RowIndex, xls.GetValue(MsgCol, RowIndex) & " Error>板厚・ｕ(1)")
                        xls.SetBackColor(METARU_SHISAKU_BANKO_SURYO_U, RowIndex, METARU_SHISAKU_BANKO_SURYO_U, RowIndex, RGB(255, 0, 0))
                        rtnVal = False
                    End If
                    Vo.ShisakuBankoSuryoU = BankoSuryoU
                End If
                '材料情報
                Dim Material As String = ""
                If StringUtil.IsNotEmpty(xls.GetValue(METARU_MATERIAL, RowIndex)) Then
                    Material = xls.GetValue(METARU_MATERIAL, RowIndex).ToString.Trim
                    If Material.IndexOf("X") > 0 Then
                        Dim Materials() As String = Material.Split("X")
                        '製品長
                        If Not ShisakuComFunc.IsInLength(Materials(0), 4) Or Not IsNumeric(Materials(0)) Then
                            xls.SetValue(MsgCol, RowIndex, xls.GetValue(MsgCol, RowIndex) & " Error>製品長(4)")
                            xls.SetBackColor(METARU_MATERIAL, RowIndex, METARU_MATERIAL, RowIndex, RGB(255, 0, 0))
                            rtnVal = False
                        End If
                        '製品幅
                        If Not ShisakuComFunc.IsInLength(Materials(1), 4) Or Not IsNumeric(Materials(1)) Then
                            xls.SetValue(MsgCol, RowIndex, xls.GetValue(MsgCol, RowIndex) & " Error>製品幅(4)")
                            xls.SetBackColor(METARU_MATERIAL, RowIndex, METARU_MATERIAL, RowIndex, RGB(255, 0, 0))
                            rtnVal = False
                        End If
                        Vo.MaterialInfoLength = StringUtil.ToInteger(Materials(0).Trim)
                        Vo.MaterialInfoWidth = StringUtil.ToInteger(Materials(1).Trim)
                    End If
                End If
                'データ項目・エリア名
                Dim DataItemAreaName As String = ""
                If StringUtil.IsNotEmpty(xls.GetValue(METARU_DATAITEMAREANAME, RowIndex)) Then
                    DataItemAreaName = xls.GetValue(METARU_DATAITEMAREANAME, RowIndex).ToString.Trim
                    If Not ShisakuComFunc.IsInLength(DataItemAreaName, 256) Then
                        xls.SetValue(MsgCol, RowIndex, xls.GetValue(MsgCol, RowIndex) & " Error>データ項目・エリア名(256)")
                        xls.SetBackColor(METARU_DATAITEMAREANAME, RowIndex, METARU_DATAITEMAREANAME, RowIndex, RGB(255, 0, 0))
                        rtnVal = False
                    End If
                    Vo.DataItemAreaName = DataItemAreaName
                End If
                'データ項目・改訂№
                Dim DataItemSetName As String = ""
                If StringUtil.IsNotEmpty(xls.GetValue(METARU_DATAITEMSETNAME, RowIndex)) Then
                    DataItemSetName = xls.GetValue(METARU_DATAITEMSETNAME, RowIndex).ToString.Trim
                    If DataItemSetName.IndexOf("_") > 0 Then
                        Dim DataItem() As String = DataItemSetName.Split("_")
                        If DataItem.Length > 3 Then
                            'If ShisakuComFunc.IsInLength(DataItem(3), 5) Or StrErrCheck(DataItem(3)) Then
                            If ShisakuComFunc.IsInLength(DataItem(3), 5) Then
                                If StringUtil.Equals(DataItem(3), "99-99") Then
                                    Vo.DataItemKaiteiNo = ""
                                Else
                                    Vo.DataItemKaiteiNo = DataItem(3)
                                End If
                            End If
                        End If
                    End If
                    'データ項目・セット名
                    If Not ShisakuComFunc.IsInLength(DataItemSetName, 256) Then
                        xls.SetValue(MsgCol, RowIndex, xls.GetValue(MsgCol, RowIndex) & " Error>データ項目・セット名(256)")
                        xls.SetBackColor(DataItemSetName, RowIndex, DataItemSetName, RowIndex, RGB(255, 0, 0))
                        rtnVal = False
                    End If
                    Vo.DataItemSetName = DataItemSetName
                End If
                aVolist.Add(Vo)
            Next
            Return rtnVal
        End Function

#End Region

#End Region
        '↑↑2014/12/30 メタルブロックExcel取込を追加 TES)張 ADD BEGIN

        '↓↓↓2014/12/30 材料手配リスト作成を追加 TES)張 ADD BEGIN
#Region " 材料手配リスト作成機能"
        ''' <summary>
        ''' 「材料情報・発注対象最終更新年月日」がブランク（NULL）以外のデータをカウントする
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CountTargetData() As Integer
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)
            Dim result As Integer = 0

            For rowIndex As Integer = startRow To sheet.RowCount - 1
                '手配帳編集画面にて発注対象フラグに☑が付いていてのデータを抽出
                If sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET)) = True And _
                    sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)) = False And _
                    sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_HENKATEN)) <> "削" Then
                    If StringUtil.IsNotEmpty(sheet.GetNote(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET))) Then
                        result = result + 1
                    End If
                End If
            Next

            Return result
        End Function

        ''' <summary>
        ''' 材料手配リスト作成(メイン処理)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ZairyoListOutput(ByVal zumiFlg As Boolean)
            'ファイルパス取得
            Dim fileName As String = OutputFileDialog(ShisakuCommon.ShisakuGlobal.ExcelZairyoListOutPut, ShisakuCommon.ShisakuGlobal.ExcelZairyoListOutPutDir)

            If fileName.Equals(String.Empty) Then
                Return
            End If

            Cursor.Current = Cursors.WaitCursor

            '出力処理へ
            SaveZairyoListExcelFile(fileName, zumiFlg)

            _frmDispTehaiEdit.Refresh()

            Process.Start(fileName)

            _frmDispTehaiEdit.Refresh()

            '「材料情報・発注対象最終更新年月日」を更新
            If UpdateTargetDate() = False Then
                Return
            End If

            ComFunc.ShowInfoMsgBox("材料手配リストの出力が完了しました。", MessageBoxButtons.OK)

        End Sub

        ''' <summary>
        ''' ファイル保存用ダイアログを開く
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function OutputFileDialog(ByVal file As String, ByVal dir As String) As String
            Dim fileName As String
            Dim sfd As New SaveFileDialog()

            ' ファイル名を指定します
            sfd.FileName = file
            sfd.Title = "出力対象のファイルを選択してください"

            ' 起動ディレクトリを指定します
            sfd.InitialDirectory = dir

            '  [ファイルの種類] ボックスに表示される選択肢を指定します
            sfd.Filter = "Excel files(*.xls)|*.xls"

            'ダイアログ選択有無
            If sfd.ShowDialog() = DialogResult.OK Then
                fileName = sfd.FileName
            Else
                Return String.Empty
            End If

            sfd.Dispose()

            Return fileName

        End Function

        ''' <summary>
        ''' 材料手配リスト作成(ファイルを出力)
        ''' </summary>
        ''' <param name="filename"></param>
        ''' <remarks></remarks>
        Private Sub SaveZairyoListExcelFile(ByVal filename As String, ByVal zumiFlg As Boolean)

            If (FileIO.FileSystem.FileExists(filename)) Then
                FileIO.FileSystem.DeleteFile(filename)
            End If

            'テンプレートファイルをコーピ―する
            FileCopy(ShisakuCommon.ShisakuGlobal.ExcelZairyoListTemplate, filename)

            If Not ShisakuComFunc.IsFileOpen(filename) Then
                Using xls As New ShisakuExcel(filename)
                    xls.SetActiveSheet(1)
                    '出力
                    WriteExcel(xls, zumiFlg)
                    '保存
                    xls.Save()
                    xls.Dispose()
                    _frmDispTehaiEdit.Refresh()
                End Using
            End If
        End Sub

        ''' <summary>
        ''' 材料手配リスト作成(ファイルを出力)
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub WriteExcel(ByVal xls As ShisakuExcel, ByVal zumiFlg As Boolean)
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)
            Dim index As Integer = 0

            For rowIndex As Integer = startRow To sheet.RowCount - 1
                '手配帳編集画面にて発注対象フラグに☑が付いていて発注済フラグが未チェックのデータを出力する。
                '  変化点が削のデータも対象外。
                If sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET)) = True And _
                   sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)) = False And _
                   sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_HENKATEN)) <> "削" Then
                    '出力済含まないを選択した場合、出力済としてカウントしたデータを除いたデータを出力
                    If Not zumiFlg Then
                        If StringUtil.IsNotEmpty(sheet.GetNote(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET))) Then
                            Continue For
                        End If
                    End If
                    '５００行超えば場合
                    If index >= ZairyoListExcel.EXCEL_MAX_ROW - ZairyoListExcel.EXCEL_DATA_START_ROW + 1 Then
                        xls.CopyInsertRow(index + ZairyoListExcel.EXCEL_DATA_START_ROW - 1, index + ZairyoListExcel.EXCEL_DATA_START_ROW, 1)
                        xls.SetValue(ZairyoListExcel.COLUMN_NO, index + ZairyoListExcel.EXCEL_DATA_START_ROW, ZairyoListExcel.EXCEL_DATA_MAX_COLUMN, index + ZairyoListExcel.EXCEL_DATA_START_ROW, String.Empty)
                    End If
                    '№
                    xls.SetValue(ZairyoListExcel.COLUMN_NO, index + ZairyoListExcel.EXCEL_DATA_START_ROW, (index + 1).ToString)
                    '日付
                    Dim aDate As New ShisakuDate
                    xls.SetValue(ZairyoListExcel.COLUMN_DATE, index + ZairyoListExcel.EXCEL_DATA_START_ROW, Format(aDate.CurrentDateTime, "MM/dd"))
                    '担当者
                    xls.SetValue(ZairyoListExcel.COLUMN_TANTOSHA, index + ZairyoListExcel.EXCEL_DATA_START_ROW, LoginInfo.Now.ShainName)
                    'ｲﾍﾞﾝﾄ
                    xls.SetValue(ZairyoListExcel.COLUMN_EVENT, index + ZairyoListExcel.EXCEL_DATA_START_ROW, _headerSubject.kaihatsuFugo + "  " + _headerSubject.listEventName)
                    '板厚
                    xls.SetValue(ZairyoListExcel.COLUMN_BANKO_SURYO, index + ZairyoListExcel.EXCEL_DATA_START_ROW, sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO)))
                    'u
                    xls.SetValue(ZairyoListExcel.COLUMN_BANKO_SURYO_U, index + ZairyoListExcel.EXCEL_DATA_START_ROW, sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U)))
                    '材質
                    xls.SetValue(ZairyoListExcel.COLUMN_ZAISHITU, index + ZairyoListExcel.EXCEL_DATA_START_ROW, _
                                 sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1)) & _
                                 sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2)) & _
                                 sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3)) & _
                                 sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_MEKKI)))
                    '部品幅
                    If StringUtil.IsNotEmpty(sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_WIDTH))) Then
                        xls.SetValue(ZairyoListExcel.COLUMN_BUHIN_WIDTH, index + ZairyoListExcel.EXCEL_DATA_START_ROW, CInt(sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_WIDTH))))
                    End If
                    '部品長
                    If StringUtil.IsNotEmpty(sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_LENGTH))) Then
                        xls.SetValue(ZairyoListExcel.COLUMN_BUHIN_LENGTH, index + ZairyoListExcel.EXCEL_DATA_START_ROW, CInt(sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_LENGTH))))
                    End If
                    '支給メーカー
                    xls.SetValue(ZairyoListExcel.COLUMN_PROVISION_MAKER, index + ZairyoListExcel.EXCEL_DATA_START_ROW, sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_TORIHIKISAKI_CODE)))
                    '部品名称
                    xls.SetValue(ZairyoListExcel.COLUMN_BUHIN_NAME, index + ZairyoListExcel.EXCEL_DATA_START_ROW, sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NAME)))
                    '試作リストコード
                    xls.SetValue(ZairyoListExcel.COLUMN_SHISAKU_LIST_CODE, index + ZairyoListExcel.EXCEL_DATA_START_ROW, _headerSubject.shisakuListCode)
                    '試作ブロック№
                    xls.SetValue(ZairyoListExcel.COLUMN_SHISAKU_BLOCK_NO, index + ZairyoListExcel.EXCEL_DATA_START_ROW, sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)))
                    'レベル
                    xls.SetValue(ZairyoListExcel.COLUMN_LEVEL, index + ZairyoListExcel.EXCEL_DATA_START_ROW, sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_LEVEL)))
                    '部品番号
                    xls.SetValue(ZairyoListExcel.COLUMN_BUHIN_NO, index + ZairyoListExcel.EXCEL_DATA_START_ROW, sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO)))

                    index = index + 1
                End If
            Next
        End Sub

        ''' <summary>
        ''' 材料情報・発注対象最終更新年月日の更新
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function UpdateTargetDate() As Boolean
            Try
                Dim watch As New Stopwatch()
                Dim tehaichoDao As TShisakuTehaiKihonDao = New TShisakuTehaiKihonDaoImpl
                Dim tehaichoVo As TShisakuTehaiKihonVo = Nothing
                Dim aDate As New ShisakuDate

                Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
                Dim startRow As Integer = GetTitleRowsIn(sheet)

                Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                    db.Open()
                    '－トランザクション開始－
                    db.BeginTransaction()

                    watch.Start()

                    For rowIndex As Integer = startRow To sheet.RowCount - 1
                        '手配帳編集画面にて発注対象フラグに☑が付いていてのデータを抽出
                        '  変化点に削が付いているデータは対象外
                        If sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET)) = True And _
                            sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)) = False And _
                            sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_HENKATEN)) <> "削" Then
                            tehaichoVo = tehaichoDao.FindByPk(_headerSubject.shisakuEventCode, _
                                                              _headerSubject.shisakuListCode, _
                                                              _headerSubject.shisakuListCodeKaiteiNo, _
                                                              sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)), _
                                                              sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)), _
                                                              CInt(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN))))
                            '更新日が既にセットされている場合は日付をセットしない。
                            If StringUtil.IsEmpty(tehaichoVo.MaterialInfoOrderTargetDate) Then
                                tehaichoVo.MaterialInfoOrderTargetDate = aDate.CurrentDateDbFormat
                                tehaichoVo.UpdatedUserId = LoginInfo.Now.UserId
                                tehaichoVo.UpdatedDate = aDate.CurrentDateDbFormat
                                tehaichoVo.UpdatedTime = aDate.CurrentTimeDbFormat
                                Dim resCnt As Integer = tehaichoDao.UpdateByPk(tehaichoVo)

                                If resCnt <> 1 Then
                                    Throw New Exception()
                                End If
                                '手配帳編集画面の基本情報SPREADにもここでセットすればOK
                                sheet.SetNote(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET), CDate(aDate.CurrentDateDbFormat).ToString("yyyy-MM-dd"))
                                'sheet.SetNote(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET), CDate(aDate.CurrentDateDbFormat).ToString("MM/dd"))
                            End If
                        End If
                    Next

                    watch.Stop()
                    Console.WriteLine(String.Format("材料情報・発注対象最終更新年月日の更新-実行時間 : {0}ms", watch.ElapsedMilliseconds))
                    watch.Reset()

                    watch.Start()
                    db.Commit()
                    watch.Stop()
                    Console.WriteLine(String.Format("コミット-実行時間 : {0}ms", watch.ElapsedMilliseconds))
                    watch.Reset()
                End Using

                'For rowIndex As Integer = startRow To sheet.RowCount - 1
                '    '手配帳編集画面にて発注対象フラグに☑が付いていてのデータを抽出
                '    If sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET)) = True And _
                '        sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)) = False Then
                '        sheet.SetNote(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET), CDate(aDate.CurrentDateDbFormat).ToString("MM/dd"))
                '    End If
                'Next

                Return True

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("材料情報・発注対象最終更新年月日の更新中にエラーが発生しました。")
                Return False
            End Try
        End Function

        Private Class ZairyoListExcel

            Public Const EXCEL_DATA_START_ROW As Integer = 5
            Public Const EXCEL_DATA_MAX_COLUMN As Integer = 80
            Public Const EXCEL_MAX_ROW As Integer = 500

            '工事指令書
            Public Const COLUMN_KOUJI_SHIREI_SHO As Integer = 1
            '発注済
            Public Const COLUMN_ORDER_CHK As Integer = 2
            '鋼板材
            Public Const COLUMN_HAGANE_ITAZAI As Integer = 3
            '№
            Public Const COLUMN_NO As Integer = 4
            '日付
            Public Const COLUMN_DATE As Integer = 5
            '担当者
            Public Const COLUMN_TANTOSHA As Integer = 6
            '追加削除変更
            Public Const COLUMN_ADD_DEL_UPD As Integer = 7
            'ｲﾍﾞﾝﾄ
            Public Const COLUMN_EVENT As Integer = 8
            '材料ﾒｰｶ
            Public Const COLUMN_ZAIRYO_MAKER As Integer = 9
            '工事指令
            Public Const COLUMN_KOUJI_SHIREI As Integer = 10
            '行ID
            Public Const COLUMN_ROW_ID As Integer = 11
            '板厚
            Public Const COLUMN_BANKO_SURYO As Integer = 12
            'u
            Public Const COLUMN_BANKO_SURYO_U As Integer = 13
            '材質
            Public Const COLUMN_ZAISHITU As Integer = 14
            '材料ｺｰﾄﾞ
            Public Const COLUMN_ZAIRYO_CODE As Integer = 15
            '部品幅
            Public Const COLUMN_BUHIN_WIDTH As Integer = 16
            '部品長
            Public Const COLUMN_BUHIN_LENGTH As Integer = 17
            '製作台数
            Public Const COLUMN_SEISAKU_TAISU As Integer = 18
            '手配幅
            Public Const COLUMN_TEHAI_WIDTH As Integer = 19
            '手配長
            Public Const COLUMN_TEHAI_LENGTH As Integer = 20
            '手配枚数
            Public Const COLUMN_TEHAI_MAISU As Integer = 26
            '納期(メーカ回答）納期ﾘﾐｯﾄ差異あればピンク網掛け
            Public Const COLUMN_NOUKI As Integer = 27
            '重量鉄
            Public Const COLUMN_JYURYOU_TETU As Integer = 28
            '重量アルミ
            Public Const COLUMN_JYURYOU_ARUMI As Integer = 29
            '支給メーカー
            Public Const COLUMN_PROVISION_MAKER As Integer = 30
            '部品名称
            Public Const COLUMN_BUHIN_NAME As Integer = 31
            '備考
            Public Const COLUMN_BIKO As Integer = 36
            'アラート
            Public Const COLUMN_ALERT As Integer = 76
            '試作リストコード
            Public Const COLUMN_SHISAKU_LIST_CODE As Integer = 77
            '試作ブロック№
            Public Const COLUMN_SHISAKU_BLOCK_NO As Integer = 78
            'レベル
            Public Const COLUMN_LEVEL As Integer = 79
            '部品番号
            Public Const COLUMN_BUHIN_NO As Integer = 80

        End Class
#End Region
        '↑↑↑2014/12/30 材料手配リスト作成を追加 TES)張 ADD END

        '↓↓↓2015/01/09 設計メモ更新処理を追加 TES)張 ADD BEGIN
#Region "設計メモ更新機能"

#Region "設計メモ更新機能(メイン処理)"
        ''' <summary>
        ''' 設計メモ更新機能(メイン処理)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SekkeiMemoImport()
            'ファイルパス取得
            Dim fileName As String = ImportFileDialog()

            If fileName.Equals(String.Empty) Then
                Return
            End If
            Cursor.Current = Cursors.WaitCursor

            'Excelファイからのインポート処理
            If SekkeiMemoImportExcelFile(fileName) = False Then
                Return
            End If

            'エラーが無ければアラートチェックを行う。
            If SekkeiMemoBlockNoCheck(_SekkeiMemoBlockNo) = False Then
                ComFunc.ShowInfoMsgBox("新規ブロックの取り込みを行います。" & vbLf & vbLf & _
                                       "ブロック№：" & _SekkeiMemoBlockNo)
            End If

            '   処理中画面表示
            Dim SyorichuForm1 As New frm03Syorichu
            SyorichuForm1.Label4.Text = "確認"
            SyorichuForm1.lblKakunin.Text = ""
            SyorichuForm1.lblKakunin2.Text = "処理中・・・"
            SyorichuForm1.lblKakunin3.Text = ""
            SyorichuForm1.Execute()
            SyorichuForm1.Show()

            Application.DoEvents()

            'ここで取込前のバックアップを実行する。
            ' 取込前バックアップ
            importBackup()
            _frmDispTehaiEdit.Refresh()

            'EXCELの情報を画面へ設定する。
            SekkeiMemoUpdate()

            '編集対象ブロックを設定
            ExcelImpAddEditBlock()

            'Excelファイル上で削除された行を探索し編集対象ブロックに追加する(要するにExcel上で削除されると色々厄介なのです）
            Me.DataMatchDeleteRec()

            '処理中画面非表示
            SyorichuForm1.Close()

            'クローズ処理イベント取得用クラス
            Dim closer As frm00Kakunin.IFormCloser = New CopyFormCloser(Me, fileName)

            '取込内容確認フォームを表示
            frm00Kakunin.ConfirmShow("EXCEL取込の確認", "EXCELの情報を確認してください。", _
                                           "EXCELのデータを反映しますか？", "EXCELを反映", "取込前に戻す", closer)
        End Sub
#End Region

#Region "設計メモ更新機能(Excelオープン)"
        ''' <summary>
        ''' 設計メモ更新機能(Excelオープン)
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SekkeiMemoImportExcelFile(ByVal fileName As String) As Boolean
            _SekkeiMemoList = New List(Of TShisakuTehaiKihonVo)

            Dim errMsg1 As String = "設計メモのEXCELではありません。ご確認ください。"
            Dim errMsg2 As String = "ブロックが指定されていません。"
            Dim errMsg3 As String = "設計メモデータがありません。"

            If Not ShisakuComFunc.IsFileOpen(fileName) Then
                Using xls As New ShisakuExcel(fileName)
                    xls.SetActiveSheet(1)

                    '１シート目のD列、5行目が"ブロック"か？
                    If Not String.Equals(xls.GetValue(SEKKEI_MEMO_XLS_COL_MEMO_TITLE, SEKKEI_MEMO_XLS_ROW_MEMO_TITLE), "ブロック") Then
                        'エラーメッセージ表示
                        ComFunc.ShowErrMsgBox(errMsg1)
                        Return False
                    End If

                    '１シート目のD列、6行目に値が無い。
                    _SekkeiMemoBlockNo = xls.GetValue(SEKKEI_MEMO_XLS_COL_MEMO_DATA, SEKKEI_MEMO_XLS_ROW_MEMO_DATA)
                    If StringUtil.IsEmpty(_SekkeiMemoBlockNo) Then
                        'エラーメッセージ表示
                        ComFunc.ShowErrMsgBox(errMsg2)
                        Return False
                    End If

                    Try
                        '部品欄という名のシートがあるか？
                        xls.SetActiveSheet(SEKKEI_MEMO_BUHIN_DATA_SHEET_NAME)

                        '部品欄シートの5行目以降にデータが無い。
                        If BuhinSheetDataCheck(xls) = False Then
                            'エラーメッセージ表示
                            ComFunc.ShowErrMsgBox(errMsg3)
                            Return False
                        End If

                        '部品欄シートの列タイトルが合っているか？
                        If BuhinSheetColumnsCheck(_SekkeiMemoList, _SekkeiMemoBlockNo, xls) = False Then
                            'エラーメッセージ表示
                            ComFunc.ShowErrMsgBox(errMsg1)
                            Return False
                        End If
                    Catch ex As Exception
                        'エラーメッセージ表示
                        ComFunc.ShowErrMsgBox(errMsg1)
                        Return False
                    End Try
                End Using
            End If

            Return True
        End Function
#End Region

#Region "Excel取込機能(Excelチェック)"
        ''' <summary>
        ''' データが有るかどうかのチェック
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function BuhinSheetDataCheck(ByVal xls As ShisakuExcel) As Boolean

            Dim xlsEndRow As Integer = 0
            For i As Integer = xls.EndRow To SEKKEI_MEMO_DATA_START_ROW Step -1
                If StringUtil.IsNotEmpty(xls.GetValue(SEKKEI_MEMO_COLUMN_NO, i)) Then
                    xlsEndRow = i
                    Exit For
                End If
            Next
            If xlsEndRow = 0 Then
                Return False
            End If

            Return True

        End Function

        ''' <summary>
        ''' 列項目位置チェック、桁数チェック
        ''' </summary>
        ''' <param name="aVolist"></param>
        ''' <param name="blockNo"></param>
        ''' <param name="xls"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function BuhinSheetColumnsCheck(ByVal aVolist As List(Of TShisakuTehaiKihonVo), ByVal blockNo As String, ByVal xls As ShisakuExcel) As Boolean

            '列数不満の場合
            Dim xlsEndCol As Integer = 0
            For i As Integer = xls.EndCol To SEKKEI_MEMO_DATA_START_COLUMN Step -1
                If StringUtil.IsNotEmpty(xls.GetValue(i, SEKKEI_MEMO_TITLE_ROW_1)) Or _
                   StringUtil.IsNotEmpty(xls.GetValue(i, SEKKEI_MEMO_TITLE_ROW_2)) Or _
                   StringUtil.IsNotEmpty(xls.GetValue(i, SEKKEI_MEMO_TITLE_ROW_3)) Then
                    xlsEndCol = i
                    Exit For
                End If
            Next
            If xlsEndCol < SEKKEI_MEMO_ALL_COLUMN_COUNT + SEKKEI_MEMO_DATA_START_COLUMN - 1 Then
                Return False
            End If

            '各列項目チェック
            For rowIndex As Integer = SEKKEI_MEMO_DATA_START_ROW To xls.EndRow
                Dim Vo As New TShisakuTehaiKihonVo

                'ブロック
                Vo.ShisakuBlockNo = blockNo
                ''行ID
                If StringUtil.IsEmpty(xls.GetValue(SEKKEI_MEMO_COLUMN_NO, rowIndex)) Then
                    Continue For
                End If
                '部品番号
                If StringUtil.IsNotEmpty(xls.GetValue(SEKKEI_MEMO_COLUMN_BUHIN_NO, rowIndex)) Then
                    Dim buhinNo As String = xls.GetValue(SEKKEI_MEMO_COLUMN_BUHIN_NO, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(buhinNo) Then
                        If Not ShisakuComFunc.IsInLength(buhinNo, 15) Then
                            Return False
                        End If
                        Vo.BuhinNo = buhinNo
                    End If
                End If
                '部品名称
                If StringUtil.IsNotEmpty(xls.GetValue(SEKKEI_MEMO_COLUMN_BUHIN_NAME, rowIndex)) Then
                    Dim buhinName As String = xls.GetValue(SEKKEI_MEMO_COLUMN_BUHIN_NAME, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(buhinName) Then
                        If Not ShisakuComFunc.IsInLength(buhinName, 20) Then
                            Return False
                        End If
                        Vo.BuhinName = buhinName
                    End If
                End If
                '材質
                If StringUtil.IsNotEmpty(xls.GetValue(SEKKEI_MEMO_COLUMN_ZAISHITU, rowIndex)) Then
                    Dim zaishitu As String = xls.GetValue(SEKKEI_MEMO_COLUMN_ZAISHITU, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(zaishitu) Then
                        Dim zaishituLen As Integer = zaishitu.Length
                        '材質・規格１
                        If zaishituLen >= 3 Then
                            Vo.ZaishituKikaku1 = zaishitu.Substring(0, 3)
                        End If
                        '材質・規格２
                        If zaishituLen >= 6 Then
                            Vo.ZaishituKikaku2 = zaishitu.Substring(3, 3)
                        End If
                        '材質・規格３
                        If zaishituLen >= 7 Then
                            Vo.ZaishituKikaku3 = zaishitu.Substring(6, 1)
                        End If
                        '材質・メッキ
                        If zaishituLen >= 8 Then
                            Dim mekki As String = zaishitu.Substring(7)
                            If mekki.StartsWith("#") Then
                                mekki = mekki.Remove(0, 1)
                            End If
                            If Not ShisakuComFunc.IsInLength(mekki, 6) Then
                                Return False
                            End If
                            If StringUtil.IsNotEmpty(mekki) Then
                                Vo.ZaishituMekki = mekki
                            End If
                        End If
                    End If
                End If
                '板厚
                If StringUtil.IsNotEmpty(xls.GetValue(SEKKEI_MEMO_COLUMN_BANKO, rowIndex)) Then
                    Dim banko As String = xls.GetValue(SEKKEI_MEMO_COLUMN_BANKO, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(banko) Then
                        Dim bankoLen As Integer = banko.Length
                        '板厚
                        Dim bankoWithoutU As String = banko.Replace("u", "")
                        If Not ShisakuComFunc.IsInLength(bankoWithoutU, 5) Then
                            Return False
                        End If
                        Vo.ShisakuBankoSuryo = bankoWithoutU
                        '板厚・ｕ
                        If banko.IndexOf("u") > 0 Then
                            Vo.ShisakuBankoSuryoU = "u"
                        End If
                    End If
                End If
                'データ項目・改訂№
                If StringUtil.IsNotEmpty(xls.GetValue(SEKKEI_MEMO_COLUMN_FILE, rowIndex)) Then
                    Dim dataItemSetName As String = xls.GetValue(SEKKEI_MEMO_COLUMN_FILE, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(dataItemSetName) Then
                        If dataItemSetName.IndexOf("_") > 0 Then
                            Dim dataItems() As String = dataItemSetName.Split("_")
                            If dataItems.Length > 3 Then
                                '"_"が3つ目の次から4つ目の"_"の間までの値
                                If ShisakuComFunc.IsInLength(dataItems(3), 5) Then
                                    If StringUtil.Equals(dataItems(3), "99-99") Then
                                        Vo.DataItemKaiteiNo = ""
                                    Else
                                        Vo.DataItemKaiteiNo = dataItems(3)
                                    End If
                                End If
                            End If
                        End If
                        'データ項目・セット名
                        If Not ShisakuComFunc.IsInLength(dataItemSetName, 256) Then
                            Return False
                        End If
                        Vo.DataItemSetName = dataItemSetName
                    End If
                End If
                'データ項目・エリア名
                If StringUtil.IsNotEmpty(xls.GetValue(SEKKEI_MEMO_COLUMN_AREA, rowIndex)) Then
                    Dim dataItemAreaName As String = xls.GetValue(SEKKEI_MEMO_COLUMN_AREA, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(dataItemAreaName) Then
                        If Not ShisakuComFunc.IsInLength(dataItemAreaName, 256) Then
                            Return False
                        End If
                        Vo.DataItemAreaName = dataItemAreaName
                    End If
                End If

                aVolist.Add(Vo)
            Next

            Return True

        End Function

#End Region

#Region "設計メモ更新機能(改訂№の最大とブロック№で検索)"
        ''' <summary>
        ''' 改訂№の最大とブロック№で検索
        ''' </summary>
        ''' <param name="blockNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SekkeiMemoBlockNoCheck(ByVal blockNo As String) As Boolean
            Dim result As Boolean = False

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                'マッチング用DBデータテーブル取得
                Dim dtTehaiBase As DataTable = TehaichoEditImpl.FindAllBaseInfo(db, _shisakuEventCode, _shisakuListCode)

                For i As Integer = 0 To dtTehaiBase.Rows.Count - 1
                    If String.Equals(blockNo, dtTehaiBase.Rows(i)(NmDTColBase.TD_SHISAKU_BLOCK_NO).ToString) Then
                        result = True
                        Exit For
                    End If
                Next
            End Using

            Return result
        End Function
#End Region

#Region "設計メモ更新機能(スプレッドへ設定)"
        Private Sub SekkeiMemoUpdate()

            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startSpdRow As Integer = GetTitleRowsIn(sheet)
            Dim kaihatsuFugo As String = _headerSubject.kaihatsuFugo
            Dim seihinKbn As String = _headerSubject.seihinKbn

            If _SekkeiMemoList.Count > 0 Then
                For rowIndex As Integer = startSpdRow To sheet.RowCount - 1
                    If StringUtil.IsEmpty(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO))) Then
                        Exit For
                    End If

                    '部品番号異なるのフラグ
                    Dim isSameBuhin As Boolean = True
                    '発注済フラグ
                    Dim isHatchuCheck As Boolean = True
                    '部品毎の差分
                    For excelIndex As Integer = 0 To _SekkeiMemoList.Count - 1
                        '同一ブロックの場合
                        If StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)), _SekkeiMemoList(excelIndex).ShisakuBlockNo) Then
                            '品番は同じ
                            If StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO)), _SekkeiMemoList(excelIndex).BuhinNo) Then
                                '削除なのデータが有る
                                If String.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_HENKATEN)), "削") Then
                                    Continue For
                                End If

                                'Excelのブロック情報表示
                                '部品名称
                                If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).BuhinName) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NAME)), _SekkeiMemoList(excelIndex).BuhinName) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NAME), _SekkeiMemoList(excelIndex).BuhinName)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NAME))
                                    End If
                                End If
                                '材質・規格１
                                If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).ZaishituKikaku1) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1)), _SekkeiMemoList(excelIndex).ZaishituKikaku1) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1), _SekkeiMemoList(excelIndex).ZaishituKikaku1)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1))
                                        isHatchuCheck = False
                                    End If
                                End If
                                '材質・規格２
                                If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).ZaishituKikaku2) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2)), _SekkeiMemoList(excelIndex).ZaishituKikaku2) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2), _SekkeiMemoList(excelIndex).ZaishituKikaku2)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2))
                                        isHatchuCheck = False
                                    End If
                                End If
                                '材質・規格３
                                If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).ZaishituKikaku3) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3)), _SekkeiMemoList(excelIndex).ZaishituKikaku3) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3), _SekkeiMemoList(excelIndex).ZaishituKikaku3)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3))
                                        isHatchuCheck = False
                                    End If
                                End If
                                '材質・メッキ
                                If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).ZaishituMekki) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_MEKKI)), _SekkeiMemoList(excelIndex).ZaishituMekki) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_MEKKI), _SekkeiMemoList(excelIndex).ZaishituMekki)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_MEKKI))
                                        isHatchuCheck = False
                                    End If
                                End If
                                '板厚
                                If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).ShisakuBankoSuryo) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO)), _SekkeiMemoList(excelIndex).ShisakuBankoSuryo) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO), _SekkeiMemoList(excelIndex).ShisakuBankoSuryo)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO))
                                        isHatchuCheck = False
                                    End If
                                End If
                                '板厚・ｕ
                                'If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).ShisakuBankoSuryoU) Then
                                If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U)), _SekkeiMemoList(excelIndex).ShisakuBankoSuryoU) Then
                                    sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U), _SekkeiMemoList(excelIndex).ShisakuBankoSuryoU)
                                    SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U))
                                    isHatchuCheck = False
                                End If
                                'End If
                                '発注済
                                If Not isHatchuCheck Then
                                    sheet.SetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK), False)
                                End If
                                'データ項目・改訂№	
                                If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).DataItemKaiteiNo) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO)), _SekkeiMemoList(excelIndex).DataItemKaiteiNo) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO), _SekkeiMemoList(excelIndex).DataItemKaiteiNo)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO))
                                    End If
                                End If
                                '項目・エリア名
                                If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).DataItemAreaName) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME)), UCase(_SekkeiMemoList(excelIndex).DataItemAreaName)) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME), _SekkeiMemoList(excelIndex).DataItemAreaName)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME))
                                    End If
                                End If
                                'データ項目・セット名
                                If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).DataItemSetName) Then
                                    If Not StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME)), UCase(_SekkeiMemoList(excelIndex).DataItemSetName)) Then
                                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME), _SekkeiMemoList(excelIndex).DataItemSetName)
                                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME))
                                        'データ項目・データ支給チェック欄
                                        sheet.SetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION), False)
                                    End If
                                End If

                                isSameBuhin = True
                                Exit For
                            Else
                                isSameBuhin = False
                            End If
                        End If
                    Next
                    '不要な行を削除
                    'If Not isSameBuhin Then
                    '    If String.Equals(_headerSubject.shisakuListCodeKaiteiNo, "000") Then
                    '        '試作リストコード改訂№が000の場合、該当行を削除
                    '        sheet.RemoveRows(rowIndex, 1)
                    '        '号車シートも削除
                    '        _frmDispTehaiEdit.spdGouSya_Sheet1.RemoveRows(rowIndex, 1)
                    '        rowIndex = rowIndex - 1
                    '    Else
                    '        '該当行の変更点が”削”にする
                    '        SetDeleteRowDisabled(sheet, rowIndex)
                    '    End If
                    'End If
                Next

                '最新の行IDを設定
                Dim gyouId As Integer = 0
                Dim strBlock As String = String.Empty
                Dim dataCount As Integer
                For rowIndex As Integer = startSpdRow To sheet.RowCount - 1
                    If StringUtil.IsEmpty(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO))) Then
                        dataCount = rowIndex
                        Exit For
                    End If

                    If String.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)), strBlock) Then
                        gyouId = gyouId + 1
                    Else
                        gyouId = 1
                    End If
                    If Not String.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID)), gyouId.ToString("000")) Then
                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID), gyouId.ToString("000"))
                        '部品番号表示順に行IDと同じ値をセットする。
                        sheet.SetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN), gyouId.ToString)
                        SetEditRowProc(True, rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID))
                    End If
                    strBlock = sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO))
                Next

                '新しい行を追加
                For excelIndex As Integer = 0 To _SekkeiMemoList.Count - 1
                    Dim isAdd As Boolean = True
                    Dim hasSameBlock As Boolean = False
                    Dim insertIndex As Integer
                    '部品番号表示順
                    Dim buhinBangoHyojiJun As Integer = 0

                    For rowIndex As Integer = startSpdRow To sheet.RowCount - 1
                        If StringUtil.IsEmpty(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO))) Then
                            Exit For
                        End If
                        If StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)), _SekkeiMemoList(excelIndex).ShisakuBlockNo) Then
                            insertIndex = rowIndex
                            gyouId = CInt(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID)))
                            '部品番号表示順
                            buhinBangoHyojiJun = CInt(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN)))

                            hasSameBlock = True
                            If StringUtil.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO)), _SekkeiMemoList(excelIndex).BuhinNo) Then
                                '削除なのデータが有る
                                If String.Equals(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_HENKATEN)), "削") Then
                                    Continue For
                                End If
                                isAdd = False
                                Exit For
                            End If
                        End If
                    Next
                    If isAdd Then
                        If hasSameBlock Then
                            insertIndex = insertIndex + 1
                        Else
                            insertIndex = dataCount
                        End If
                        sheet.AddRows(insertIndex, 1)
                        dataCount = dataCount + 1
                        '号車シートも追加
                        _frmDispTehaiEdit.spdGouSya_Sheet1.AddRows(insertIndex, 1)
                        DoubleBorderInsuSa(insertIndex, insertIndex)
                        SetEditRowProc(True, insertIndex, 0, 1, sheet.ColumnCount)

                        '発注対象のチェックボックスのチェックを外した場合、発注済のチェックボックスは使用不可（チェック不可）にする。
                        sheet.Cells(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)).Locked = True
                        'ブロック
                        If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).ShisakuBlockNo) Then
                            sheet.SetText(insertIndex, sheet.Columns(NmSpdTagBase.TAG_SHISAKU_BLOCK_NO).Index, _SekkeiMemoList(excelIndex).ShisakuBlockNo)
                        End If
                        If String.Equals(sheet.GetText(insertIndex - 1, sheet.Columns(NmSpdTagBase.TAG_SHISAKU_BLOCK_NO).Index), sheet.GetText(insertIndex, sheet.Columns(NmSpdTagBase.TAG_SHISAKU_BLOCK_NO).Index)) Then
                            '行ID
                            sheet.SetText(insertIndex, sheet.Columns(NmSpdTagBase.TAG_GYOU_ID).Index, (gyouId + 1).ToString("000"))
                            '部品番号表示順
                            sheet.SetText(insertIndex, sheet.Columns(NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN).Index, (buhinBangoHyojiJun + 1).ToString("000"))
                        Else
                            '行ID
                            sheet.SetText(insertIndex, sheet.Columns(NmSpdTagBase.TAG_GYOU_ID).Index, "001")
                            '部品番号表示順
                            sheet.SetText(insertIndex, sheet.Columns(NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN).Index, "000")
                        End If
                        '部品番号
                        If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).BuhinNo) Then
                            sheet.SetText(insertIndex, sheet.Columns(NmSpdTagBase.TAG_BUHIN_NO).Index, _SekkeiMemoList(excelIndex).BuhinNo)
                        End If
                        '部品名称
                        If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).BuhinName) Then
                            sheet.SetText(insertIndex, sheet.Columns(NmSpdTagBase.TAG_BUHIN_NAME).Index, _SekkeiMemoList(excelIndex).BuhinName)
                        End If
                        '材質・規格１
                        If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).ZaishituKikaku1) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1), _SekkeiMemoList(excelIndex).ZaishituKikaku1)
                        End If
                        '材質・規格２
                        If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).ZaishituKikaku2) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2), _SekkeiMemoList(excelIndex).ZaishituKikaku2)
                        End If
                        '材質・規格３
                        If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).ZaishituKikaku3) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3), _SekkeiMemoList(excelIndex).ZaishituKikaku3)
                        End If
                        '材質・メッキ
                        If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).ZaishituMekki) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_MEKKI), _SekkeiMemoList(excelIndex).ZaishituMekki)
                        End If
                        '板厚
                        If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).ShisakuBankoSuryo) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO), _SekkeiMemoList(excelIndex).ShisakuBankoSuryo)
                        End If
                        '板厚・ｕ
                        If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).ShisakuBankoSuryoU) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U), _SekkeiMemoList(excelIndex).ShisakuBankoSuryoU)
                        End If
                        'データ項目・改訂№	
                        If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).DataItemKaiteiNo) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO), _SekkeiMemoList(excelIndex).DataItemKaiteiNo)
                        End If
                        '項目・エリア名
                        If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).DataItemAreaName) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME), _SekkeiMemoList(excelIndex).DataItemAreaName)
                        End If
                        'データ項目・セット名
                        If StringUtil.IsNotEmpty(_SekkeiMemoList(excelIndex).DataItemSetName) Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME), _SekkeiMemoList(excelIndex).DataItemSetName)
                        End If
                        '部課コード
                        Dim r0080Vo As Rhac0080Vo
                        Dim SekkeiBlockDao As New SekkeiBlockDaoImpl
                        r0080Vo = SekkeiBlockDao.FindTantoBushoByBlock(kaihatsuFugo, _SekkeiMemoList(excelIndex).ShisakuBlockNo)
                        If r0080Vo IsNot Nothing Then
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE), r0080Vo.TantoBusho)
                        Else
                            sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE), "ZZZZ")
                        End If

                        '合計員数
                        sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_TOTAL_INSU_SURYO), 0)

                        '手配帳付加部品番号に対しての取得
                        Using ebomDb As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                            ebomDb.Open()

                            Using kouseidb As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
                                kouseidb.Open()

                                '部品番号が空白の場合は次行へ
                                If _SekkeiMemoList(excelIndex).BuhinNo Is Nothing Then
                                    Continue For
                                End If
                                If _SekkeiMemoList(excelIndex).BuhinNo.Equals(String.Empty) Then
                                    Continue For
                                End If
                                Dim tehaiFuka As New TehaichoFuka(ebomDb, kouseidb, kaihatsuFugo, _SekkeiMemoList(excelIndex).BuhinNo, seihinKbn)
                                '専用
                                If StringUtil.IsNotEmpty(tehaiFuka.SenyouMark) Then
                                    sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SENYOU_MARK), tehaiFuka.SenyouMark)
                                End If
                                '購坦
                                If StringUtil.IsNotEmpty(tehaiFuka.SenyouMark) Then
                                    sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KOUTAN), tehaiFuka.koutan)
                                End If
                                '取引先コード
                                '取引先名称
                                If StringUtil.IsNotEmpty(tehaiFuka.Torihikisaki) Then
                                    sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_TORIHIKISAKI_CODE), tehaiFuka.Torihikisaki)
                                    sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_KATA_HI), TehaichoEditImpl.FindPkRhac0610(tehaiFuka.Torihikisaki.Trim))
                                End If
                                '図面設通№	
                                If StringUtil.IsNotEmpty(tehaiFuka.ZumenSettsu) Then
                                    sheet.SetText(insertIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA), tehaiFuka.ZumenSettsu)
                                End If
                            End Using
                        End Using
                    End If
                Next

                sheet.RowCount = dataCount + 10 - startSpdRow
                _frmDispTehaiEdit.spdGouSya_Sheet1.RowCount = sheet.RowCount
            End If

        End Sub
#End Region

#End Region
        '↑↑↑2015/01/09 設計メモ更新処理を追加 TES)張 ADD END

#End Region




#Region "材料寸法取得機能"

#Region "材料寸法取得機能(メイン処理)"
        ''' <summary>
        ''' 材料寸法取得機能(メイン処理)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ZairyoSunpoImport(ByVal buhinNo As String, ByVal areaName As String, ByVal setName As String, _
                                     ByVal actRow As Integer, ByVal actCol As Integer)

            'ファイルパス取得
            Dim fileName As String = ZairyoSunpoDir & ZairyoSunpoFile

            If fileName.Equals(String.Empty) Then
                Return
            End If
            Cursor.Current = Cursors.WaitCursor

            'Excelファイからのインポート処理
            If ZairyoSunpoImportExcelFile(fileName) = False Then
                Return
            End If

            'エラーが無ければアラートチェックを行う。
            If ZairyoSunpoCheck(buhinNo, areaName, setName) = False Then
                ComFunc.ShowErrMsgBox("選択した部品番号、エリア名、セット名にマッチする寸法が存在しません。" & vbLf & vbLf & _
                                       "部品番号：" & buhinNo & vbLf & _
                                       "エリア名：" & areaName & vbLf & _
                                       "セット名：" & setName)
                Return
            End If

            'EXCELの情報を画面へ設定する。
            ZairyoSunpoUpdate(actRow, actCol)

            '編集対象ブロックを設定
            ExcelImpAddEditBlock()

        End Sub
#End Region

#Region "材料寸法取得機能(Excelオープン)"
        ''' <summary>
        ''' 材料寸法取得機能(Excelオープン)
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ZairyoSunpoImportExcelFile(ByVal fileName As String) As Boolean
            _ZairyoSunpoList = New List(Of TShisakuTehaiKihonVo)

            Dim errMsg1 As String = "材料寸法のEXCELではありません。ご確認ください。"
            Dim errMsg2 As String = "部品番号が指定されていません。"
            Dim errMsg3 As String = "材料寸法データがありません。"

            If Not ShisakuComFunc.IsFileOpen(fileName) Then
                Using xls As New ShisakuExcel(fileName)
                    xls.SetActiveSheet(1)

                    Try
                        '部品欄という名のシートがあるか？
                        xls.SetActiveSheet(ZAIRYO_SUNPO_BUHIN_DATA_SHEET_NAME)

                        '部品欄シートの5行目以降にデータが無い。
                        If ZairyoSunpoBuhinSheetDataCheck(xls) = False Then
                            'エラーメッセージ表示
                            ComFunc.ShowErrMsgBox(errMsg3)
                            Return False
                        End If

                        '部品欄シートの列タイトルが合っているか？
                        If ZairyoSunpoBuhinSheetColumnsCheck(_ZairyoSunpoList, xls) = False Then
                            'エラーメッセージ表示
                            ComFunc.ShowErrMsgBox(errMsg1)
                            Return False
                        End If
                    Catch ex As Exception
                        'エラーメッセージ表示
                        ComFunc.ShowErrMsgBox(errMsg1)
                        Return False
                    End Try
                End Using
            End If

            Return True
        End Function
#End Region

#Region "Excel取込機能(Excelチェック)"
        ''' <summary>
        ''' データが有るかどうかのチェック
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ZairyoSunpoBuhinSheetDataCheck(ByVal xls As ShisakuExcel) As Boolean

            Dim xlsEndRow As Integer = 0
            For i As Integer = xls.EndRow To ZAIRYO_SUNPO_DATA_START_ROW Step -1
                If StringUtil.IsNotEmpty(xls.GetValue(ZAIRYO_SUNPO_COLUMN_NO, i)) Then
                    xlsEndRow = i
                    Exit For
                End If
            Next
            If xlsEndRow = 0 Then
                Return False
            End If

            Return True

        End Function

        ''' <summary>
        ''' 列項目位置チェック、桁数チェック
        ''' </summary>
        ''' <param name="aVolist"></param>
        ''' <param name="xls"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ZairyoSunpoBuhinSheetColumnsCheck(ByVal aVolist As List(Of TShisakuTehaiKihonVo), ByVal xls As ShisakuExcel) As Boolean

            '列数不満の場合
            Dim xlsEndCol As Integer = 0
            For i As Integer = xls.EndCol To ZAIRYO_SUNPO_DATA_START_COLUMN Step -1
                If StringUtil.IsNotEmpty(xls.GetValue(i, ZAIRYO_SUNPO_TITLE_ROW_1)) Or _
                   StringUtil.IsNotEmpty(xls.GetValue(i, ZAIRYO_SUNPO_TITLE_ROW_2)) Or _
                   StringUtil.IsNotEmpty(xls.GetValue(i, ZAIRYO_SUNPO_TITLE_ROW_3)) Then
                    xlsEndCol = i
                    Exit For
                End If
            Next
            If xlsEndCol < ZAIRYO_SUNPO_ALL_COLUMN_COUNT + ZAIRYO_SUNPO_DATA_START_COLUMN - 1 Then
                Return False
            End If

            '各列項目チェック
            For rowIndex As Integer = ZAIRYO_SUNPO_DATA_START_ROW To xls.EndRow
                Dim Vo As New TShisakuTehaiKihonVo

                '行ID
                If StringUtil.IsEmpty(xls.GetValue(ZAIRYO_SUNPO_COLUMN_NO, rowIndex)) Then
                    Continue For
                End If
                '部品番号
                If StringUtil.IsNotEmpty(xls.GetValue(ZAIRYO_SUNPO_COLUMN_BUHIN_NO, rowIndex)) Then
                    Dim buhinNo As String = xls.GetValue(ZAIRYO_SUNPO_COLUMN_BUHIN_NO, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(buhinNo) Then
                        If Not ShisakuComFunc.IsInLength(buhinNo, 15) Then
                            Return False
                        End If
                        Vo.BuhinNo = buhinNo.TrimEnd
                    End If
                End If
                '部品名称
                If StringUtil.IsNotEmpty(xls.GetValue(ZAIRYO_SUNPO_COLUMN_BUHIN_NAME, rowIndex)) Then
                    Dim buhinName As String = xls.GetValue(ZAIRYO_SUNPO_COLUMN_BUHIN_NAME, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(buhinName) Then
                        If Not ShisakuComFunc.IsInLength(buhinName, 20) Then
                            Return False
                        End If
                        Vo.BuhinName = buhinName.TrimEnd
                    End If
                End If
                '材質
                If StringUtil.IsNotEmpty(xls.GetValue(ZAIRYO_SUNPO_COLUMN_ZAISHITU, rowIndex)) Then
                    Dim zaishitu As String = xls.GetValue(ZAIRYO_SUNPO_COLUMN_ZAISHITU, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(zaishitu) Then
                        Dim zaishituLen As Integer = zaishitu.Length
                        '材質・規格１
                        If zaishituLen >= 3 Then
                            Vo.ZaishituKikaku1 = zaishitu.Substring(0, 3)
                        End If
                        '材質・規格２
                        If zaishituLen >= 6 Then
                            Vo.ZaishituKikaku2 = zaishitu.Substring(3, 3)
                        End If
                        '材質・規格３
                        If zaishituLen >= 7 Then
                            Vo.ZaishituKikaku3 = zaishitu.Substring(6, 1)
                        End If
                        '材質・メッキ
                        If zaishituLen >= 8 Then
                            Dim mekki As String = zaishitu.Substring(7)
                            If mekki.StartsWith("#") Then
                                mekki = mekki.Remove(0, 1)
                            End If
                            If Not ShisakuComFunc.IsInLength(mekki, 6) Then
                                Return False
                            End If
                            If StringUtil.IsNotEmpty(mekki) Then
                                Vo.ZaishituMekki = mekki
                            End If
                        End If
                    End If
                End If
                '板厚
                If StringUtil.IsNotEmpty(xls.GetValue(ZAIRYO_SUNPO_COLUMN_BANKO, rowIndex)) Then
                    Dim banko As String = xls.GetValue(ZAIRYO_SUNPO_COLUMN_BANKO, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(banko) Then
                        Dim bankoLen As Integer = banko.Length
                        '板厚
                        Dim bankoWithoutU As String = banko.Replace("u", "")
                        If Not ShisakuComFunc.IsInLength(bankoWithoutU, 5) Then
                            Return False
                        End If
                        Vo.ShisakuBankoSuryo = bankoWithoutU
                        '板厚・ｕ
                        If banko.IndexOf("u") > 0 Then
                            Vo.ShisakuBankoSuryoU = "u"
                        End If
                    End If
                End If
                'データ項目・改訂№
                If StringUtil.IsNotEmpty(xls.GetValue(ZAIRYO_SUNPO_COLUMN_FILE, rowIndex)) Then
                    Dim dataItemSetName As String = xls.GetValue(ZAIRYO_SUNPO_COLUMN_FILE, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(dataItemSetName) Then
                        If dataItemSetName.IndexOf("_") > 0 Then
                            Dim dataItems() As String = dataItemSetName.Split("_")
                            If dataItems.Length > 3 Then
                                '"_"が3つ目の次から4つ目の"_"の間までの値
                                If ShisakuComFunc.IsInLength(dataItems(3), 5) Then
                                    If StringUtil.Equals(dataItems(3), "99-99") Then
                                        Vo.DataItemKaiteiNo = ""
                                    Else
                                        Vo.DataItemKaiteiNo = dataItems(3)
                                    End If
                                End If
                            End If
                        End If
                        'データ項目・セット名
                        If Not ShisakuComFunc.IsInLength(dataItemSetName, 256) Then
                            Return False
                        End If
                        Vo.DataItemSetName = dataItemSetName.ToUpper.TrimEnd
                    End If
                End If
                'データ項目・エリア名
                If StringUtil.IsNotEmpty(xls.GetValue(ZAIRYO_SUNPO_COLUMN_AREA, rowIndex)) Then
                    Dim dataItemAreaName As String = xls.GetValue(ZAIRYO_SUNPO_COLUMN_AREA, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(dataItemAreaName) Then
                        If Not ShisakuComFunc.IsInLength(dataItemAreaName, 256) Then
                            Return False
                        End If
                        Vo.DataItemAreaName = dataItemAreaName.ToUpper.TrimEnd
                    End If
                End If
                'X
                If StringUtil.IsNotEmpty(xls.GetValue(ZAIRYO_SUNPO_COLUMN_X, rowIndex)) Then
                    Dim dataItemX As String = xls.GetValue(ZAIRYO_SUNPO_COLUMN_X, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(dataItemX) Then
                        '桁数チェック
                        If Not ShisakuComFunc.IsInLength(dataItemX, 8) Then
                            Return False
                        End If
                        Dim d As Decimal
                        '数値チェック
                        If Not Decimal.TryParse(dataItemX, d) Then
                            Return False
                        End If
                        Vo.ZairyoSunpoX = CDec(dataItemX)
                    End If
                End If
                'Y
                If StringUtil.IsNotEmpty(xls.GetValue(ZAIRYO_SUNPO_COLUMN_Y, rowIndex)) Then
                    Dim dataItemY As String = xls.GetValue(ZAIRYO_SUNPO_COLUMN_Y, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(dataItemY) Then
                        '桁数チェック
                        If Not ShisakuComFunc.IsInLength(dataItemY, 8) Then
                            Return False
                        End If
                        Dim d As Decimal
                        '数値チェック
                        If Not Decimal.TryParse(dataItemY, d) Then
                            Return False
                        End If
                        Vo.ZairyoSunpoY = CDec(dataItemY)
                    End If
                End If
                'Z
                If StringUtil.IsNotEmpty(xls.GetValue(ZAIRYO_SUNPO_COLUMN_Z, rowIndex)) Then
                    Dim dataItemZ As String = xls.GetValue(ZAIRYO_SUNPO_COLUMN_Z, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(dataItemZ) Then
                        '桁数チェック
                        If Not ShisakuComFunc.IsInLength(dataItemZ, 8) Then
                            Return False
                        End If
                        Dim d As Decimal
                        '数値チェック
                        If Not Decimal.TryParse(dataItemZ, d) Then
                            Return False
                        End If
                        Vo.ZairyoSunpoZ = CDec(dataItemZ)
                    End If
                End If
                'XY
                If StringUtil.IsNotEmpty(xls.GetValue(ZAIRYO_SUNPO_COLUMN_XY, rowIndex)) Then
                    Dim dataItemXY As String = xls.GetValue(ZAIRYO_SUNPO_COLUMN_XY, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(dataItemXY) Then
                        '桁数チェック
                        If Not ShisakuComFunc.IsInLength(dataItemXY, 8) Then
                            Return False
                        End If
                        Dim d As Decimal
                        '数値チェック
                        If Not Decimal.TryParse(dataItemXY, d) Then
                            Return False
                        End If
                        Vo.ZairyoSunpoXy = CDec(dataItemXY)
                    End If
                End If
                'XZ
                If StringUtil.IsNotEmpty(xls.GetValue(ZAIRYO_SUNPO_COLUMN_XZ, rowIndex)) Then
                    Dim dataItemXZ As String = xls.GetValue(ZAIRYO_SUNPO_COLUMN_XZ, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(dataItemXZ) Then
                        '桁数チェック
                        If Not ShisakuComFunc.IsInLength(dataItemXZ, 8) Then
                            Return False
                        End If
                        Dim d As Decimal
                        '数値チェック
                        If Not Decimal.TryParse(dataItemXZ, d) Then
                            Return False
                        End If
                        Vo.ZairyoSunpoXz = CDec(dataItemXZ)
                    End If
                End If
                'YZ
                If StringUtil.IsNotEmpty(xls.GetValue(ZAIRYO_SUNPO_COLUMN_YZ, rowIndex)) Then
                    Dim dataItemYZ As String = xls.GetValue(ZAIRYO_SUNPO_COLUMN_YZ, rowIndex).ToString.Trim
                    If StringUtil.IsNotEmpty(dataItemYZ) Then
                        '桁数チェック
                        If Not ShisakuComFunc.IsInLength(dataItemYZ, 8) Then
                            Return False
                        End If
                        Dim d As Decimal
                        '数値チェック
                        If Not Decimal.TryParse(dataItemYZ, d) Then
                            Return False
                        End If
                        Vo.ZairyoSunpoYz = CDec(dataItemYZ)
                    End If
                End If

                aVolist.Add(Vo)
            Next

            Return True

        End Function

#End Region

#Region "材料寸法更新機能(部品番号、データエリア名、データセット名で検索)"
        ''' <summary>
        ''' 部品番号、データエリア名、データセット名で検索
        ''' </summary>
        ''' <param name="buhinNo"></param>
        ''' <param name="areaName"></param>
        ''' <param name="setName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ZairyoSunpoCheck(ByVal buhinNo As String, ByVal areaName As String, ByVal setName As String) As Boolean
            Dim result As Boolean = False

            _ZairyoSunpoX = 0
            _ZairyoSunpoY = 0
            _ZairyoSunpoZ = 0
            _ZairyoSunpoXY = 0
            _ZairyoSunpoXZ = 0
            _ZairyoSunpoYZ = 0

            For excelIndex As Integer = 0 To _ZairyoSunpoList.Count - 1

                If StringUtil.Equals(_ZairyoSunpoList(excelIndex).BuhinNo, buhinNo) And _
                    StringUtil.Equals(_ZairyoSunpoList(excelIndex).DataItemAreaName, areaName) And _
                    StringUtil.Equals(_ZairyoSunpoList(excelIndex).DataItemSetName, setName) Then

                    '寸法を保持
                    _ZairyoSunpoX = _ZairyoSunpoList(excelIndex).ZairyoSunpoX
                    _ZairyoSunpoY = _ZairyoSunpoList(excelIndex).ZairyoSunpoY
                    _ZairyoSunpoZ = _ZairyoSunpoList(excelIndex).ZairyoSunpoZ

                    result = True

                End If

            Next

            Return result
        End Function
#End Region

#Region "材料寸法更新機能(スプレッドへ設定)"
        Private Sub ZairyoSunpoUpdate(ByVal actRow As Integer, ByVal actCol As Integer)

            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1

            '材料寸法_X(mm)
            'If StringUtil.IsNotEmpty(sheet.GetText(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X))) And _
            '   Not StringUtil.Equals(sheet.GetText(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X)), "0.00") Then
            '    '
            sheet.SetText(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X), _ZairyoSunpoX)
            '編集されたセルは太文字・青文字にする
            sheet.Cells(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X), _
                        actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X)).ForeColor = Color.Blue
            sheet.Cells(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X), _
                        actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X)).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
            'Else
            '_ZairyoSunpoX = CDec(sheet.GetText(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X)))
            'End If
            '材料寸法_Y(mm)
            'If StringUtil.IsNotEmpty(sheet.GetText(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y))) And _
            '   Not StringUtil.Equals(sheet.GetText(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y)), "0.00") Then
            '    '
            sheet.SetText(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y), _ZairyoSunpoY)
            '編集されたセルは太文字・青文字にする
            sheet.Cells(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y), _
                        actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y)).ForeColor = Color.Blue
            sheet.Cells(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y), _
                        actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y)).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
            'Else
            '_ZairyoSunpoY = CDec(sheet.GetText(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y)))
            'End If
            '材料寸法_Z(mm)
            'If StringUtil.IsNotEmpty(sheet.GetText(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z))) And _
            '   Not StringUtil.Equals(sheet.GetText(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z)), "0.00") Then
            '    '
            sheet.SetText(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z), _ZairyoSunpoZ)
            '編集されたセルは太文字・青文字にする
            sheet.Cells(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z), _
                        actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z)).ForeColor = Color.Blue
            sheet.Cells(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z), _
                        actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z)).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
            'Else
            '_ZairyoSunpoZ = CDec(sheet.GetText(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z)))
            'End If

            '自動計算
            Dim sunpoXy As Decimal = _ZairyoSunpoX + _ZairyoSunpoY
            sheet.SetText(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY), GetNumericDbField(sunpoXy))
            Dim sunpoXz As Decimal = _ZairyoSunpoX + _ZairyoSunpoZ
            sheet.SetText(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ), GetNumericDbField(sunpoXz))
            Dim sunpoYz As Decimal = _ZairyoSunpoY + _ZairyoSunpoZ
            sheet.SetText(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ), GetNumericDbField(sunpoYz))

            '最大値のセルは太文字・背景色をグレーにする
            Dim sunpo(2) As Double
            sunpo(0) = sunpoXy
            sunpo(1) = sunpoXz
            sunpo(2) = sunpoYz
            Array.Sort(sunpo)
            Select Case sunpo(2)
                Case 0
                    '最大値が0の場合スルー
                Case sunpoXy
                    sheet.Cells(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY), _
                                actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY)).BackColor = Color.Yellow
                    sheet.Cells(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY), _
                                actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY)).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                Case sunpoXz
                    sheet.Cells(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ), _
                                actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ)).BackColor = Color.Yellow
                    sheet.Cells(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ), _
                                actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ)).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                Case sunpoYz
                    sheet.Cells(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ), _
                                actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ)).BackColor = Color.Yellow
                    sheet.Cells(actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ), _
                                actRow, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ)).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
            End Select

        End Sub
#End Region

#End Region


#Region "号車発注展開機能"

#Region "号車展開処理"
        ''' <summary>
        ''' 号車展開処理
        ''' </summary>
        ''' <param name="aRowNo">行位置</param>
        ''' <param name="aRowCount">行数</param>
        ''' <remarks></remarks>
        Public Sub GousyaTenkaiSpread(ByVal aRowNo As Integer, ByVal unitKbn As String, Optional ByVal aRowCount As Integer = 1)
            Dim clickBlockNo As String = String.Empty
            Dim clickBukaCode As String = String.Empty

            '号車情報スプレッドも同じ処理をする
            Dim sheetGousya As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1
            '基本情報スプレッドをセット
            Dim sheetBase As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1

            Dim startRow As Integer = GetTitleRowsIn(sheetBase)

            ''スプレッドの先頭行への行挿入は禁止
            'If aRowNo.Equals(startRow) Then
            '    ComFunc.ShowInfoMsgBox("先頭行への行挿入は出来ません。")
            '    Exit Sub
            'End If

            Dim rowCnt As Integer = 0

            '選択した行リストの１行目から選択した行まで
            For row As Integer = aRowNo To aRowNo + aRowCount - 1

                'ブロックNo取得 
                clickBlockNo = sheetBase.Cells(row + rowCnt, _
                                               GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Text
                '部課コード取得
                clickBukaCode = sheetBase.Cells(row + rowCnt, _
                                                GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)).Text

                Dim intGousyaCol As Integer = TehaichoEditNames.START_COLUMMN_GOUSYA_NAME
                Dim hshGoushaHyoujiJun As New Hashtable
                Dim gousyaCnt As Integer = 0
                Dim insertRows As Integer = 0

                For col As Integer = 0 To _dtGousyaNameList.Rows.Count - 1

                    '号車シートから表示順NOと員数と納入指示日を取得する。
                    Dim hyoujiJun As Integer = Integer.Parse(_dtGousyaNameList.Rows(col)(NmDTColGousyaNameList.TD_HYOJIJUN_NO))
                    Dim strinsu As String = sheetGousya.GetText(row + rowCnt, intGousyaCol + col)
                    '号車も取得する。(#がある場合、#以降の文字を取得する）
                    Dim gousyaName As String = _dtGousyaNameList.Rows(col)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME)
                    If 0 < gousyaName.IndexOf("#") Then
                        gousyaName = Right(gousyaName, gousyaName.IndexOf("#"))
                    End If
                    '号車がブランクまたはDUMMYの場合はスキップ
                    If StringUtil.IsEmpty(gousyaName) Or StringUtil.Equals(gousyaName, "DUMMY") Then
                        Continue For
                    End If
                    '員数が入っている列数をカウント
                    If StringUtil.IsNotEmpty(strinsu) Then
                        insertRows += 1
                    End If
                Next

                '員数が入っている場合処理続行
                If insertRows > 0 Then
                    'スプレッドへ行挿入と罫線引いて抜ける
                    sheetBase.Rows.Add(row + rowCnt + 1, insertRows)
                    sheetGousya.Rows.Add(row + rowCnt + 1, insertRows)
                    '----------------------------------------------------------------------------
                    For col As Integer = 0 To _dtGousyaNameList.Rows.Count - 1

                        '号車シートから表示順NOと員数と納入指示美日を取得する。
                        Dim hyoujiJun As Integer = Integer.Parse(_dtGousyaNameList.Rows(col)(NmDTColGousyaNameList.TD_HYOJIJUN_NO))
                        Dim strinsu As String = sheetGousya.GetText(row + rowCnt, intGousyaCol + col)

                        '号車も取得する。(#がある場合、#以降の文字を取得する）
                        Dim gousyaName As String = _dtGousyaNameList.Rows(col)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME)
                        If 0 < gousyaName.IndexOf("#") Then
                            gousyaName = Right(gousyaName, gousyaName.IndexOf("#"))
                        End If

                        '号車がブランクまたはDUMMYの場合はスキップ
                        If StringUtil.IsEmpty(gousyaName) Or StringUtil.Equals(gousyaName, "DUMMY") Then
                            Continue For
                        End If

                        '員数が入っていたなら号車展開を実行
                        '   １．行を挿入
                        '   ２．挿入した基本情報へ基本情報をセット。部品番号：号車を付加。納入指示日：員数位置の指示日を付加。
                        '   ３．挿入した号車情報へ基本情報をセット。員数はヒットした列にのみセット。
                        If StringUtil.IsNotEmpty(strinsu) Then

                            gousyaCnt += 1

                            'unitKbnにより納入指示日を切り替えてセット
                            Dim nounyuShijibi As Integer = 0
                            If unitKbn = "T" Then
                                nounyuShijibi = _dtGousyaNameList.Rows(col)(NmDTColGousyaNameList.TD_T_NOUNYU_SHIJIBI)
                            Else
                                nounyuShijibi = _dtGousyaNameList.Rows(col)(NmDTColGousyaNameList.TD_M_NOUNYU_SHIJIBI)
                            End If

                            '行ID及び部品表示順の最大値を取得
                            Dim maxGyouId As Integer = -1
                            Dim maxBuhinHyoujiJun As Integer = -1
                            '
                            GetMaxNo(clickBukaCode, clickBlockNo, maxGyouId, maxBuhinHyoujiJun)

                            '取得した値を空行に対してセットする
                            SetGousyaTenkaiSpread(row + rowCnt, _
                                                  gousyaName, _
                                                  nounyuShijibi, _
                                                  strinsu, _
                                                  intGousyaCol + col, _
                                                  clickBukaCode, _
                                                  clickBlockNo, _
                                                  maxGyouId, _
                                                  maxBuhinHyoujiJun, _
                                                  row + rowCnt + gousyaCnt, _
                                                  1)

                        End If
                    Next
                    '----------------------------------------------------------------------------
                    '親行の背景色を変更する
                    '   納入指示日をリセット、手配記号は"F"にする
                    SetGousyaTenkaiSpread(row + rowCnt)

                    '号車で追加した行を保持
                    rowCnt = rowCnt + gousyaCnt
                End If

                'クリア
                gousyaCnt = 0

            Next


            '行IDを設定
            Dim gyouId As Integer = 0
            Dim strBlock As String = String.Empty
            Dim dataCount As Integer = 0
            For rowIndex As Integer = startRow To sheetBase.RowCount - 1
                If StringUtil.IsEmpty(sheetBase.GetText(rowIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO))) Then
                    dataCount = rowIndex
                    Exit For
                End If

                If String.Equals(sheetBase.GetText(rowIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)), strBlock) Then
                    gyouId = gyouId + 1
                Else
                    gyouId = 1
                End If
                strBlock = sheetBase.GetText(rowIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO))
                If Not String.Equals(sheetBase.GetText(rowIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_GYOU_ID)), gyouId.ToString("000")) Then
                    '   ベース情報
                    sheetBase.SetText(rowIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_GYOU_ID), gyouId.ToString("000"))
                    '部品番号表示順に行ID-1の値をセットする。By柳沼（行IDは001から、部品番号表示順は0から・・）
                    sheetBase.SetText(rowIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN), gyouId - 1)
                    SetEditRowProc(True, rowIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_GYOU_ID))
                    '   号車情報
                    sheetGousya.SetText(rowIndex, GetTagIdx(sheetGousya, NmSpdTagGousya.TAG_GYOU_ID), gyouId.ToString("000"))
                    '部品番号表示順に行ID-1の値をセットする。By柳沼（行IDは001から、部品番号表示順は0から・・）
                    sheetGousya.SetText(rowIndex, GetTagIdx(sheetGousya, NmSpdTagGousya.TAG_BUHIN_NO_HYOUJI_JUN), gyouId - 1)
                    SetEditRowProc(True, rowIndex, GetTagIdx(sheetGousya, NmSpdTagGousya.TAG_GYOU_ID))
                End If
            Next


        End Sub
#End Region

#Region "号車発注展開が可能かチェック"

        ''' <summary>
        ''' 号車発注展開が可能かチェック
        ''' １．号車納期設定がまだ
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FunctionGousyaTenkaiCheck(ByVal unitKbn As String) As Boolean

            '号車件数取得
            Dim gousyaCnt As Integer = 0
            gousyaCnt = _dtGousyaNameList.Rows.Count

            '号車に納期設定がされているか？
            For i As Integer = 0 To gousyaCnt - 1

                'ブランク、DUMMYは除く
                If StringUtil.IsNotEmpty(_dtGousyaNameList.Rows(i)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME)) And _
                   Not StringUtil.Equals(_dtGousyaNameList.Rows(i)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME), "DUMMY") Then

                    If StringUtil.Equals(unitKbn, "M") Then
                        If _dtGousyaNameList.Rows(i)(NmDTColGousyaNameList.TD_M_NOUNYU_SHIJIBI) = 0 Then
                            ComFunc.ShowErrMsgBox("号車別にメタル納期を設定後、再度実行してください。")
                            Return False
                        End If
                    Else
                        If _dtGousyaNameList.Rows(i)(NmDTColGousyaNameList.TD_T_NOUNYU_SHIJIBI) = 0 Then
                            ComFunc.ShowErrMsgBox("号車別にトリム納期を設定後、再度実行してください。")
                            Return False
                        End If
                    End If
                End If

            Next

            Return True

        End Function

#End Region

#Region "追加行に対して、親の情報と,行ID,部品表示順をセットする"
        ''' <summary>
        ''' 追加行に対して、親の情報と,行ID,部品表示順をセットする
        ''' </summary>
        ''' <param name="aOyaRow"></param>
        ''' <param name="gousyaName"></param>
        ''' <param name="nounyuShijibi"></param>
        ''' <param name="strinsu"></param>
        ''' <param name="insuCol"></param>
        ''' <param name="aBukaCode"></param>
        ''' <param name="aBlockNo"></param>
        ''' <param name="aMaxGyouId"></param>
        ''' <param name="aMaxBuhinHyoujiJun"></param>
        ''' <param name="aRowNo"></param>
        ''' <param name="aRowCount"></param>
        ''' <remarks></remarks>
        Private Sub SetGousyaTenkaiSpread(ByVal aOyaRow As Integer, _
                                          ByVal gousyaName As String, _
                                          ByVal nounyuShijibi As Integer, _
                                          ByVal strInsu As String, _
                                          ByVal insuCol As Integer, _
                                          ByVal aBukaCode As String, _
                                          ByVal aBlockNo As String, _
                                          ByVal aMaxGyouId As Integer, _
                                          ByVal aMaxBuhinHyoujiJun As Integer, _
                                          ByVal aRowNo As Integer, _
                                          ByVal aRowCount As Integer)

            For i As Integer = 0 To aRowCount - 1
                Dim sheetBase As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
                Dim sheetGousya As SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1

                Dim strMaxGyouId As String = String.Format("{0:000}", aMaxGyouId + 1)
                Dim strMaxBuhinHyoujiJun As String = String.Format("{0:000}", aMaxBuhinHyoujiJun + 1)

                '挿入した行に行IDをセット
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_GYOU_ID), strMaxGyouId)
                sheetGousya.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_GYOU_ID), strMaxGyouId)

                '部品表示順をセットする
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN), strMaxBuhinHyoujiJun)
                sheetGousya.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_BUHIN_NO_HYOUJI_JUN), strMaxBuhinHyoujiJun)

                'ブロックNoをセットする
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO), aBlockNo)
                sheetGousya.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_SHISAKU_BLOCK_NO), aBlockNo)

                '部課コードをセットする
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE), aBukaCode)
                sheetGousya.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_SHISAKU_BUKA_CODE), aBukaCode)

                '親のデータをセットする。
                '   履歴
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_RIREKI), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_RIREKI)))
                sheetGousya.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_RIREKI), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_RIREKI)))
                '   専用マーク
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SENYOU_MARK), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SENYOU_MARK)))
                sheetGousya.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SENYOU_MARK), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_SENYOU_MARK)))
                '   レベル
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_LEVEL), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_LEVEL)))
                sheetGousya.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_LEVEL), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_LEVEL)))

                '   部品番号
                Dim buhinNo As String = sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO))
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO), buhinNo)
                sheetGousya.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO), buhinNo)


                '   部品番号試作区分
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KBN), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KBN)))
                sheetGousya.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KBN), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_BUHIN_NO_KBN)))

                '   部品番号改訂№
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KAITEI_NO), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KAITEI_NO)))
                '   枝番
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_EDA_BAN), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_EDA_BAN)))
                '   部品名称
                Dim buhinName As String = _
                                Left(sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NAME)) & gousyaName, 20)
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NAME), buhinName)
                '   集計コード
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUKEI_CODE), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUKEI_CODE)))
                ''   現調CKD区分
                'sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_GENCYO_CKD_KBN), _
                '                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_GENCYO_CKD_KBN)))
                '   手配記号
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TEHAI_KIGOU), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TEHAI_KIGOU)))
                '   購担
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_KOUTAN), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_KOUTAN)))
                '   取引先コード
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TORIHIKISAKI_CODE), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TORIHIKISAKI_CODE)))
                '   納場
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_NOUBA), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_NOUBA)))
                '   供給セクション
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_KYOUKU_SECTION), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_KYOUKU_SECTION)))

                '   納入指示日
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_NOUNYU_SHIJIBI), _
                                   ConvDateInt8(nounyuShijibi))

                '   合計員数
                '**の場合、0にする。
                If StringUtil.Equals(strInsu, "**") Then
                    sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TOTAL_INSU_SURYO), "0")
                Else
                    sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TOTAL_INSU_SURYO), strInsu)
                End If

                '   再使用不可
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SAISHIYOUFUKA), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SAISHIYOUFUKA)))
                '   出図予定日
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE)))
                '   出図実績_日付
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_JISEKI_DATE), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_JISEKI_DATE)))
                '   出図実績_改訂№
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO)))
                '   出図実績_設通№
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)))
                '   最終織込設変情報_日付
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SAISYU_SETSUHEN_DATE), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SAISYU_SETSUHEN_DATE)))
                '   最終織込設変情報_改訂№
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO)))
                '   最終織込設変情報_設通№
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SAISYU_SETSUHEN_STSR_DHSTBA), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SAISYU_SETSUHEN_STSR_DHSTBA)))
                '   試作部品費（円）
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUHINN_HI), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUHINN_HI)))
                '   試作型費（千円）
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_KATA_HI), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_KATA_HI)))
                '   取引先名称
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_MAKER_CODE), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_MAKER_CODE)))
                '   備考
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BIKOU), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BIKOU)))

                '号車シート
                '   号車シートの所定の列へ員数をセット
                sheetGousya.SetValue(aRowNo + i, insuCol, strInsu)

                '挿入した行の員数差の右に二重線を引く
                DoubleBorderInsuSa(aRowNo, aRowNo + i)

                Dim startCol As Integer = GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)
                Dim endCol As Integer = GetTagIdx(sheetBase, NmSpdTagBase.TAG_GYOU_ID)

                '編集書式対応
                SetEditRowProc(True, aRowNo + i, startCol, 1, startCol + endCol - startCol)

                aMaxGyouId += 1
                aMaxBuhinHyoujiJun += 1

            Next
        End Sub
#End Region

#Region "親の行に対して、背景色を変更する"
        ''' <summary>
        ''' 親の行に対して、背景色を変更する。
        ''' </summary>
        ''' <param name="aOyaRow"></param>
        ''' <remarks></remarks>
        Private Sub SetGousyaTenkaiSpread(ByVal aOyaRow As Integer)

            Dim sheetBase As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim sheetGousya As SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1

            sheetBase.Rows(aOyaRow).BackColor = Color.Gray
            sheetGousya.Rows(aOyaRow).BackColor = Color.Gray

            '手配記号に"F"をセット
            sheetBase.SetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TEHAI_KIGOU), "F")
            '納入指示日をリセット
            sheetBase.SetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_NOUNYU_SHIJIBI), ConvDateInt8(11111111))

        End Sub
#End Region



#End Region



#Region "納期発注展開機能"

#Region "号車納期展開処理"
        ''' <summary>
        ''' 号車納期展開処理
        ''' </summary>
        ''' <param name="aRowNo">行位置</param>
        ''' <param name="aRowCount">行数</param>
        ''' <remarks></remarks>
        Public Sub GousyaNoukiTenkaiSpread(ByVal aRowNo As Integer, ByVal unitKbn As String, Optional ByVal aRowCount As Integer = 1)
            Dim clickBlockNo As String = String.Empty
            Dim clickBukaCode As String = String.Empty

            '号車情報スプレッドも同じ処理をする
            Dim sheetGousya As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1
            '基本情報スプレッドをセット
            Dim sheetBase As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1

            Dim startRow As Integer = GetTitleRowsIn(sheetBase)

            ''スプレッドの先頭行への行挿入は禁止
            'If aRowNo.Equals(startRow) Then
            '    ComFunc.ShowInfoMsgBox("先頭行への行挿入は出来ません。")
            '    Exit Sub
            'End If

            Dim rowCnt As Integer = 0

            '選択した行リストの１行目から選択した行まで
            For row As Integer = aRowNo To aRowNo + aRowCount - 1

                'ブロックNo取得 
                clickBlockNo = sheetBase.Cells(row + rowCnt, _
                                               GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Text
                '部課コード取得
                clickBukaCode = sheetBase.Cells(row + rowCnt, _
                                                GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)).Text

                Dim intGousyaCol As Integer = TehaichoEditNames.START_COLUMMN_GOUSYA_NAME
                Dim hshGoushaHyoujiJun As New Hashtable
                Dim gousyaCnt As Integer = 0
                Dim insertRows As Integer = 0

                '----------------------------------------------------------------------------
                For col As Integer = 0 To _dtGousyaNameList.Rows.Count - 1

                    '号車も取得する。(#がある場合、#以降の文字を取得する）
                    Dim gousyaName As String = _dtGousyaNameList.Rows(col)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME)
                    If 0 < gousyaName.IndexOf("#") Then
                        gousyaName = Right(gousyaName, gousyaName.IndexOf("#"))
                    End If
                    '号車がブランクまたはDUMMYの場合はスキップ
                    If StringUtil.IsEmpty(gousyaName) Or StringUtil.Equals(gousyaName, "DUMMY") Then
                        Continue For
                    End If

                    '号車シートから表示順NOと員数と納入指示美日を取得する。
                    Dim hyoujiJun As Integer = Integer.Parse(_dtGousyaNameList.Rows(col)(NmDTColGousyaNameList.TD_HYOJIJUN_NO))
                    Dim strinsu As String = sheetGousya.GetText(row + rowCnt, intGousyaCol + col)

                    '員数が入っていたなら号車展開を実行
                    '   １．行を挿入
                    '   ２．挿入した基本情報へ基本情報をセット。部品番号：号車を付加。納入指示日：員数位置の指示日を付加。
                    '   ３．挿入した号車情報へ基本情報をセット。員数はヒットした列にのみセット。
                    If StringUtil.IsNotEmpty(strinsu) Then

                        'unitKbnにより納入指示日を切り替えてセット
                        Dim nounyuShijibi As Integer = 0
                        If unitKbn = "T" Then
                            nounyuShijibi = _dtGousyaNameList.Rows(col)(NmDTColGousyaNameList.TD_T_NOUNYU_SHIJIBI)
                        Else
                            nounyuShijibi = _dtGousyaNameList.Rows(col)(NmDTColGousyaNameList.TD_M_NOUNYU_SHIJIBI)
                        End If

                        '同じ納入指示日のデータがあるか？あればフラグを立てる
                        Dim insUpdFlg As String = String.Empty
                        Dim mergeRow As Integer = 0
                        If gousyaCnt > 0 Then
                            For i As Integer = row + rowCnt + 1 To gousyaCnt + row + rowCnt + 1
                                Dim baseNounyuShijibi As Integer = _
                                    ConvInt8Date(sheetBase.GetValue(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)))
                                If StringUtil.Equals(baseNounyuShijibi, nounyuShijibi) Then
                                    insUpdFlg = "Upd"
                                    mergeRow = i
                                    Exit For
                                End If
                            Next
                        End If

                        If StringUtil.IsNotEmpty(insUpdFlg) Then
                            '取得した値を該当品番に対してセットする
                            SetGousyaNoukiTenkaiSpread(row + rowCnt, _
                                                  gousyaName, _
                                                  nounyuShijibi, _
                                                  strinsu, _
                                                  intGousyaCol + col, _
                                                  clickBukaCode, _
                                                  clickBlockNo, _
                                                  0, _
                                                  0, _
                                                  mergeRow, _
                                                  insUpdFlg)
                        Else
                            '行ID及び部品表示順の最大値を取得
                            Dim maxGyouId As Integer = -1
                            Dim maxBuhinHyoujiJun As Integer = -1
                            GetMaxNo(clickBukaCode, clickBlockNo, maxGyouId, maxBuhinHyoujiJun)
                            '
                            gousyaCnt += 1
                            '取得した値を空行に対してセットする
                            SetGousyaNoukiTenkaiSpread(row + rowCnt, _
                                                  gousyaName, _
                                                  nounyuShijibi, _
                                                  strinsu, _
                                                  intGousyaCol + col, _
                                                  clickBukaCode, _
                                                  clickBlockNo, _
                                                  maxGyouId, _
                                                  maxBuhinHyoujiJun, _
                                                  row + rowCnt + gousyaCnt, _
                                                  insUpdFlg)
                        End If

                    End If
                Next
                '----------------------------------------------------------------------------
                '親行の背景色を変更する
                '   納入指示日をリセット、手配記号は"F"にする
                SetGousyaTenkaiSpread(row + rowCnt)

                '号車で追加した行を保持
                rowCnt = rowCnt + gousyaCnt

                'クリア
                gousyaCnt = 0

            Next


            '行IDを設定
            Dim gyouId As Integer = 0
            Dim strBlock As String = String.Empty
            Dim dataCount As Integer = 0
            For rowIndex As Integer = startRow To sheetBase.RowCount - 1
                If StringUtil.IsEmpty(sheetBase.GetText(rowIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO))) Then
                    dataCount = rowIndex
                    Exit For
                End If

                If String.Equals(sheetBase.GetText(rowIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)), strBlock) Then
                    gyouId = gyouId + 1
                Else
                    gyouId = 1
                End If
                strBlock = sheetBase.GetText(rowIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO))
                If Not String.Equals(sheetBase.GetText(rowIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_GYOU_ID)), gyouId.ToString("000")) Then
                    '   ベース情報
                    sheetBase.SetText(rowIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_GYOU_ID), gyouId.ToString("000"))
                    '部品番号表示順に行ID-1の値をセットする。By柳沼（行IDは001から、部品番号表示順は0から・・）
                    sheetBase.SetText(rowIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN), gyouId - 1)
                    SetEditRowProc(True, rowIndex, GetTagIdx(sheetBase, NmSpdTagBase.TAG_GYOU_ID))
                    '   号車情報
                    sheetGousya.SetText(rowIndex, GetTagIdx(sheetGousya, NmSpdTagGousya.TAG_GYOU_ID), gyouId.ToString("000"))
                    '部品番号表示順に行ID-1の値をセットする。By柳沼（行IDは001から、部品番号表示順は0から・・）
                    sheetGousya.SetText(rowIndex, GetTagIdx(sheetGousya, NmSpdTagGousya.TAG_BUHIN_NO_HYOUJI_JUN), gyouId - 1)
                    SetEditRowProc(True, rowIndex, GetTagIdx(sheetGousya, NmSpdTagGousya.TAG_GYOU_ID))
                End If
            Next


        End Sub
#End Region

#Region "追加行に対して、親の情報と,行ID,部品表示順をセットする"
        ''' <summary>
        ''' 追加行に対して、親の情報と,行ID,部品表示順をセットする
        ''' </summary>
        ''' <param name="aOyaRow"></param>
        ''' <param name="gousyaName"></param>
        ''' <param name="nounyuShijibi"></param>
        ''' <param name="strinsu"></param>
        ''' <param name="insuCol"></param>
        ''' <param name="aBukaCode"></param>
        ''' <param name="aBlockNo"></param>
        ''' <param name="aMaxGyouId"></param>
        ''' <param name="aMaxBuhinHyoujiJun"></param>
        ''' <param name="aRowNo"></param>
        ''' <param name="updFlg"></param>
        ''' <remarks></remarks>
        Private Sub SetGousyaNoukiTenkaiSpread(ByVal aOyaRow As Integer, _
                                          ByVal gousyaName As String, _
                                          ByVal nounyuShijibi As Integer, _
                                          ByVal strInsu As String, _
                                          ByVal insuCol As Integer, _
                                          ByVal aBukaCode As String, _
                                          ByVal aBlockNo As String, _
                                          ByVal aMaxGyouId As Integer, _
                                          ByVal aMaxBuhinHyoujiJun As Integer, _
                                          ByVal aRowNo As Integer, _
                                          ByVal updFlg As String)

            Dim sheetBase As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim sheetGousya As SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1
            Dim strMaxGyouId As String = String.Format("{0:000}", aMaxGyouId + 1)
            Dim strMaxBuhinHyoujiJun As String = String.Format("{0:000}", aMaxBuhinHyoujiJun + 1)

            '納期でマージ
            If StringUtil.IsNotEmpty(updFlg) Then

                '   部品名称
                Dim buhinName As String = _
                                Left(sheetBase.GetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NAME)) & gousyaName, 20)
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NAME), buhinName)

                '   合計員数
                '**の場合、0にする。
                If StringUtil.Equals(strInsu, "**") Then
                    sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TOTAL_INSU_SURYO), "0")
                Else
                    Dim totalInsu As Integer = sheetBase.GetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TOTAL_INSU_SURYO))
                    sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TOTAL_INSU_SURYO), totalInsu + CInt(strInsu))
                End If

                '号車シート
                '   号車シートの所定の列へ員数をセット
                sheetGousya.SetValue(aRowNo, insuCol, strInsu)

            Else

                'スプレッドへ行挿入と罫線引いて抜ける
                sheetBase.Rows.Add(aRowNo, 1)
                sheetGousya.Rows.Add(aRowNo, 1)

                '挿入した行に行IDをセット
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_GYOU_ID), strMaxGyouId)
                sheetGousya.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_GYOU_ID), strMaxGyouId)

                '部品表示順をセットする
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN), strMaxBuhinHyoujiJun)
                sheetGousya.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_BUHIN_NO_HYOUJI_JUN), strMaxBuhinHyoujiJun)

                'ブロックNoをセットする
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO), aBlockNo)
                sheetGousya.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_SHISAKU_BLOCK_NO), aBlockNo)

                '部課コードをセットする
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE), aBukaCode)
                sheetGousya.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_SHISAKU_BUKA_CODE), aBukaCode)

                '親のデータをセットする。
                '   履歴
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_RIREKI), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_RIREKI)))
                sheetGousya.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_RIREKI), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_RIREKI)))
                '   専用マーク
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SENYOU_MARK), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SENYOU_MARK)))
                sheetGousya.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SENYOU_MARK), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_SENYOU_MARK)))
                '   レベル
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_LEVEL), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_LEVEL)))
                sheetGousya.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_LEVEL), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_LEVEL)))

                '   部品番号
                Dim buhinNo As String = sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO))
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO), buhinNo)
                sheetGousya.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO), buhinNo)


                '   部品番号試作区分
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KBN), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KBN)))
                sheetGousya.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KBN), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagGousya.TAG_BUHIN_NO_KBN)))

                '   部品番号改訂№
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KAITEI_NO), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_KAITEI_NO)))
                '   枝番
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_EDA_BAN), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_EDA_BAN)))
                '   部品名称
                Dim buhinName As String = _
                                Left(sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NAME)) & gousyaName, 20)
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NAME), buhinName)
                '   集計コード
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUKEI_CODE), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUKEI_CODE)))
                ''   現調CKD区分
                'sheetBase.SetValue(aRowNo , GetTagIdx(sheetBase, NmSpdTagBase.TAG_GENCYO_CKD_KBN), _
                '                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_GENCYO_CKD_KBN)))
                '   手配記号
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TEHAI_KIGOU), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TEHAI_KIGOU)))
                '   購担
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_KOUTAN), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_KOUTAN)))
                '   取引先コード
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TORIHIKISAKI_CODE), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TORIHIKISAKI_CODE)))
                '   納場
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_NOUBA), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_NOUBA)))
                '   供給セクション
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_KYOUKU_SECTION), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_KYOUKU_SECTION)))

                '   納入指示日
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_NOUNYU_SHIJIBI), _
                                   ConvDateInt8(nounyuShijibi))

                '   合計員数
                '**の場合、0にする。
                If StringUtil.Equals(strInsu, "**") Then
                    sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TOTAL_INSU_SURYO), "0")
                Else
                    sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_TOTAL_INSU_SURYO), strInsu)
                End If

                '   再使用不可
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SAISHIYOUFUKA), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SAISHIYOUFUKA)))
                '   出図予定日
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE)))
                '   出図実績_日付
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_JISEKI_DATE), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_JISEKI_DATE)))
                '   出図実績_改訂№
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO)))
                '   出図実績_設通№
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)))
                '   最終織込設変情報_日付
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SAISYU_SETSUHEN_DATE), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SAISYU_SETSUHEN_DATE)))
                '   最終織込設変情報_改訂№
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO)))
                '   最終織込設変情報_設通№
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SAISYU_SETSUHEN_STSR_DHSTBA), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SAISYU_SETSUHEN_STSR_DHSTBA)))
                '   試作部品費（円）
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUHINN_HI), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUHINN_HI)))
                '   試作型費（千円）
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_KATA_HI), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_KATA_HI)))
                '   取引先名称
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_MAKER_CODE), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_MAKER_CODE)))
                '   備考
                sheetBase.SetValue(aRowNo, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BIKOU), _
                                   sheetBase.GetValue(aOyaRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BIKOU)))

                '号車シート
                '   号車シートの所定の列へ員数をセット
                sheetGousya.SetValue(aRowNo, insuCol, strInsu)

                '挿入した行の員数差の右に二重線を引く
                DoubleBorderInsuSa(aRowNo, aRowNo)

                Dim startCol As Integer = GetTagIdx(sheetBase, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)
                Dim endCol As Integer = GetTagIdx(sheetBase, NmSpdTagBase.TAG_GYOU_ID)

                '編集書式対応
                SetEditRowProc(True, aRowNo, startCol, 1, startCol + endCol - startCol)

            End If

        End Sub
#End Region

#End Region




#Region "部品表EXCEL出力（最新）機能"

#Region "スプレッド読込専用列設定"
        ''' <summary>
        ''' スプレッド読込専用列設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub LockColGousyaGroupSpread(ByVal frm As frm20BuhinhyoExcelOutput)
            Dim sheet As FarPoint.Win.Spread.SheetView

            sheet = frm.spdGouSya_Sheet1

            sheet.Columns(NmSpdTagGousyaBuhinhyoExcel.TAG_SHISAKU_GOUSYA).Locked = True

        End Sub

#End Region

#Region "グループのコンボボックスを作成"

        ''' <summary>
        ''' グループのコンボボックスを作成
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetGousyaGroupCombo(ByVal f As frm20BuhinhyoExcelOutput)
            Try
                'グループコンボボックスに値を追加
                Dim dtGroup As DataTable = GetGroupData()
                Dim vos As New List(Of LabelValueVo)
                'ALLを単独で追加
                vos.Add(New LabelValueVo("ALL", "ALL"))
                '
                For idx As Integer = 0 To dtGroup.Rows.Count - 1
                    Dim vo As New LabelValueVo
                    vo.Label = dtGroup.Rows(idx).Item(0)
                    vo.Value = vo.Label
                    vos.Add(vo)
                Next
                FormUtil.BindLabelValuesToComboBox(f.cmbGroup, vos, True)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' T_SHISAKU_TEHAI_GOUSYA_GROUPよりグループを設定する。
        ''' </summary>
        ''' <returns>全データテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetGroupData() As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                db.Fill(GetGroupSql, dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' グループを取得するSQL
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetGroupSql() As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("      SHISAKU_GOUSYA_GROUP")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA_GROUP GP ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = '" & _shisakuEventCode & "'")
                .AppendLine(" AND SHISAKU_LIST_CODE = '" & _shisakuListCode & "'")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO =   ")
                .AppendLine("(  ")
                .AppendLine("    SELECT MAX(CONVERT(INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ))) AS SHISAKU_LIST_CODE_KAITEI_NO   ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE AS MAX_LIST WITH (NOLOCK, NOWAIT)  ")
                .AppendLine("    WHERE  MAX_LIST.SHISAKU_EVENT_CODE = GP.SHISAKU_EVENT_CODE ")
                .AppendLine("    AND  MAX_LIST.SHISAKU_LIST_CODE   = GP.SHISAKU_LIST_CODE ")
                .AppendLine("   ")
                .AppendLine(")    ")
                .AppendLine("GROUP BY ")
                .AppendLine("      SHISAKU_GOUSYA_GROUP ")
                .AppendLine("ORDER BY ")
                .AppendLine("      SHISAKU_GOUSYA_GROUP ")
            End With

            Return sql.ToString()
        End Function

#End Region

#Region "グループのコンボボックスを画面のSPREADから作成"

        ''' <summary>
        ''' グループのコンボボックスを画面のSPREADから作成
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpreadGousyaGroupCombo(ByVal f As frm20BuhinhyoExcelOutput)
            Try

                Dim sheet As FarPoint.Win.Spread.SheetView = f.spdGouSya_Sheet1
                Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(sheet)

                Dim vos As New List(Of LabelValueVo)
                'ALLを単独で追加
                vos.Add(New LabelValueVo("ALL", "ALL"))
                '
                'スプレッドループ
                For i As Integer = startRow To sheet.RowCount - startRow - 1

                    'グループ
                    Dim valGroup As String = sheet.GetValue(i, _
                                                            sheet.Columns(NmSpdTagGousyaBuhinhyoExcel.TAG_SHISAKU_GOUSYA_GROUP).Index)
                    If StringUtil.IsNotEmpty(valGroup) Then
                        '重複チェック
                        Dim j As Integer = 0
                        For Each checkVo As LabelValueVo In vos
                            If StringUtil.Equals(checkVo.Value, valGroup) Then
                                j += 1
                            End If
                        Next
                        If j = 0 Then
                            Dim vo As New LabelValueVo
                            vo.Label = valGroup
                            vo.Value = vo.Label
                            vos.Add(vo)
                        End If
                    End If
                Next
                FormUtil.BindLabelValuesToComboBox(f.cmbGroup, vos, True)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region


#Region "部品表EXCEL出力グループ情報"
        ''' <summary>
        ''' グループ情報検索
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function TehaiGousyaGroup(ByVal gousyaName As String) As TShisakuTehaiGousyaGroupVo
            Dim impl As TehaichoHeaderDao = New TehaichoHeaderDaoImpl

            Dim gousyaGroupVo As TShisakuTehaiGousyaGroupVo = _
            impl.FindGousyaGroupBy(gousyaName, _headerSubject.shisakuEventCode, _
                                                    _headerSubject.shisakuListCode, _
                                                    _headerSubject.shisakuListCodeKaiteiNo)
            Return gousyaGroupVo
        End Function

        ''' <summary>
        ''' グループ情報追加
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AddGousyaGroup(ByVal f As frm20BuhinhyoExcelOutput)
            Try
                Dim sheet As FarPoint.Win.Spread.SheetView = f.spdGouSya_Sheet1
                Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(sheet)
                Dim aDate As New ShisakuDate

                Using db As New EBomDbClient
                    db.BeginTransaction()
                    Try
                        Dim groupDao As TShisakuTehaiGousyaGroupDao = New TShisakuTehaiGousyaGroupDaoImpl

                        'デリート
                        Dim delVo As New TShisakuTehaiGousyaGroupVo
                        delVo.ShisakuEventCode = _headerSubject.shisakuEventCode
                        delVo.ShisakuListCode = _headerSubject.shisakuListCode
                        delVo.ShisakuListCodeKaiteiNo = _headerSubject.shisakuListCodeKaiteiNo
                        groupDao.DeleteBy(delVo)

                        '
                        'スプレッドループ
                        For i As Integer = startRow To sheet.RowCount - startRow - 1

                            '号車
                            Dim valGousya As String = sheet.GetValue(i, _
                                                                    sheet.Columns(NmSpdTagGousyaBuhinhyoExcel.TAG_SHISAKU_GOUSYA).Index)
                            'グループ
                            Dim valGroup As String = sheet.GetValue(i, _
                                                                    sheet.Columns(NmSpdTagGousyaBuhinhyoExcel.TAG_SHISAKU_GOUSYA_GROUP).Index)
                            If StringUtil.IsNotEmpty(valGroup) Then

                                'インサート
                                Dim insertVo As New TShisakuTehaiGousyaGroupVo
                                insertVo.ShisakuEventCode = _headerSubject.shisakuEventCode
                                insertVo.ShisakuListCode = _headerSubject.shisakuListCode
                                insertVo.ShisakuListCodeKaiteiNo = _headerSubject.shisakuListCodeKaiteiNo
                                insertVo.ShisakuGousya = valGousya
                                insertVo.ShisakuGousyaGroup = valGroup

                                insertVo.CreatedUserId = LoginInfo.Now.UserId
                                insertVo.CreatedDate = aDate.CurrentDateDbFormat
                                insertVo.CreatedTime = aDate.CurrentTimeDbFormat
                                insertVo.UpdatedUserId = LoginInfo.Now.UserId
                                insertVo.UpdatedDate = aDate.CurrentDateDbFormat
                                insertVo.UpdatedTime = aDate.CurrentTimeDbFormat

                                groupDao.InsertBy(insertVo)

                            End If
                        Next

                    Catch ex As Exception
                        db.Rollback()
                        MsgBox("試作手配帳情報（号車グループ情報）を更新出来ませんでした。")
                    End Try
                    db.Commit()
                End Using

            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region



#End Region





#Region "最新と織込みの差"


        ''' <summary>
        ''' 出図実績の最新と織込みの差情報検索
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetTehaiShutuzuJisekiOrikomiInfo() As List(Of TShisakuTehaiKihonVo)
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            Dim _ResultList As New List(Of TShisakuTehaiKihonVo)
            For i As Integer = startRow To sheet.RowCount - 1

                '「最新出図実績：改訂№」≠「最終織込設変情報：改訂№」を基本情報から取得する。
                '   最新出図実績の改訂№が無いデータは除く
                If StringUtil.IsEmpty(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO)).Trim) Or _
                    StringUtil.IsEmpty(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO)).Trim) Or _
                    StringUtil.Equals(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO)).Trim, _
                    sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO)).Trim) Then
                    Continue For
                End If

                Dim addVo As New TShisakuTehaiKihonVo

                '変化点が削のデータは除く
                'If StringUtil.IsNotEmpty(makerCode) Then
                If sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_HENKATEN)).Trim <> "削" Then

                    'ブロック
                    addVo.ShisakuBlockNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim
                    '行ＩＤ
                    addVo.GyouId = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID)).Trim
                    'レベル
                    addVo.Level = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_LEVEL)).Trim
                    '部品番号
                    addVo.BuhinNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO)).Trim
                    '部品名称
                    addVo.BuhinName = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NAME)).Trim
                    '集計コード
                    addVo.ShukeiCode = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUKEI_CODE)).Trim
                    '手配記号
                    addVo.TehaiKigou = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_TEHAI_KIGOU)).Trim
                    '購坦
                    addVo.Koutan = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_KOUTAN)).Trim
                    '取引先コード
                    addVo.TorihikisakiCode = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_TORIHIKISAKI_CODE)).Trim
                    '納場
                    addVo.Nouba = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUBA)).Trim
                    '供給セクション
                    addVo.KyoukuSection = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_KYOUKU_SECTION)).Trim
                    '納入指示日
                    addVo.NounyuShijibi = ConvInt8Date(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)).Trim)
                    '合計員数
                    addVo.TotalInsuSuryo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_TOTAL_INSU_SURYO)).Trim
                    '出図予定日
                    addVo.ShutuzuYoteiDate = ConvInt8Date(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE)).Trim)


                    '出図実績_改訂№
                    addVo.ShutuzuJisekiKaiteiNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO)).Trim
                    '出図実績_日付
                    addVo.ShutuzuJisekiDate = ConvInt8Date(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_DATE)).Trim)
                    '出図実績_設通№
                    addVo.ShutuzuJisekiStsrDhstba = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)).Trim

                    '最終織込設変情報_改訂№
                    addVo.SaisyuSetsuhenKaiteiNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO)).Trim
                    '最終織込設変情報_日付
                    addVo.SaisyuSetsuhenDate = ConvInt8Date(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_DATE)).Trim)
                    '最終織込設変情報_設通№
                    addVo.StsrDhstba = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_STSR_DHSTBA)).Trim

                    'セット
                    _ResultList.Add(addVo)

                End If

            Next

            Return _ResultList
        End Function



        ''' <summary>
        ''' 出図実績の最新情報検索
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function TehaiShutuzuJisekiKaitei(ByVal blockNo As String, _
                                                 ByVal buhinNo As String, _
                                                 ByVal kaiteiNo As String) As TShisakuTehaiShutuzuJisekiVo

            Dim impl As TehaichoHeaderDao = New TehaichoHeaderDaoImpl

            Dim shutuzuJisekiVo As TShisakuTehaiShutuzuJisekiVo = _
            impl.FindShutuzuJisekiKaiteiBy(_headerSubject.shisakuEventCode, _
                                           _headerSubject.shisakuListCode, _
                                           _headerSubject.shisakuListCodeKaiteiNo, _
                                           blockNo, buhinNo, kaiteiNo)
            Return shutuzuJisekiVo
        End Function




        ''' <summary>
        ''' 出図実績の最新と織込みの差情報検索
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function TehaiShutuzuJisekiOrikomiSa(ByVal blockNo As String, _
                                                    ByVal gyouId As String, _
                                                    ByVal buhinNo As String) As TShisakuTehaiShutuzuOrikomiVo
            Dim impl As TehaichoHeaderDao = New TehaichoHeaderDaoImpl

            Dim shutuzuOrikomiVo As TShisakuTehaiShutuzuOrikomiVo = _
            impl.FindShutuzuJisekiOrikomiSaBy(_headerSubject.shisakuEventCode, _
                                                    _headerSubject.shisakuListCode, _
                                                    _headerSubject.shisakuListCodeKaiteiNo, _
                                                    blockNo, gyouId, buhinNo)
            Return shutuzuOrikomiVo
        End Function


        ''' <summary>
        ''' 「最終織込情報」画面と基本情報へ更新
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function UpdateDataSaishinOrikomi(ByVal selectedVos As List(Of TShisakuTehaiKihonVo)) As Boolean
            Try
                Dim watch As New Stopwatch()
                Dim aDate As New ShisakuDate

                Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
                Dim startRow As Integer = GetTitleRowsIn(sheet)

                'Dim tehaichoDao As TShisakuTehaiKihonDao = New TShisakuTehaiKihonDaoImpl
                'Dim tehaichoVo As TShisakuTehaiKihonVo = Nothing
                'Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                '    db.Open()
                '    '－トランザクション開始－
                '    db.BeginTransaction()

                '    watch.Start()

                For selIndex As Integer = 0 To selectedVos.Count - 1
                    For rowIndex As Integer = startRow To sheet.RowCount - 1
                        '同一行の場合
                        If String.Equals(sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)), selectedVos(selIndex).ShisakuBlockNo) And _
                           String.Equals(sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID)), selectedVos(selIndex).GyouId) Then

                            '最終織込設変の改訂№が不一致の場合更新する
                            If Not StringUtil.Equals(sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO)), selectedVos(selIndex).SaisyuSetsuhenKaiteiNo) Then

                                'tehaichoVo = tehaichoDao.FindByPk(_headerSubject.shisakuEventCode, _
                                '                                  _headerSubject.shisakuListCode, _
                                '                                  _headerSubject.shisakuListCodeKaiteiNo, _
                                '                                  sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)), _
                                '                                  sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)), _
                                '                                  CInt(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN))))
                                ''最終織込設変情報をセット
                                'If StringUtil.IsNotEmpty(tehaichoVo) Then
                                '    If selectedVos(selIndex).ShisakuBlockNo <> tehaichoVo.ShisakuBlockNo Or _
                                '        selectedVos(selIndex).GyouId <> tehaichoVo.GyouId Then
                                '        ComFunc.ShowErrMsgBox("ブロック№と行IDが不一致です。ご確認ください。")
                                '        '
                                '        Return False
                                '    End If

                                '    tehaichoVo.SaisyuSetsuhenKaiteiNo = selectedVos(selIndex).SaisyuSetsuhenKaiteiNo
                                '    tehaichoVo.SaisyuSetsuhenStsrDhstba = selectedVos(selIndex).SaisyuSetsuhenStsrDhstba
                                '    tehaichoVo.SaisyuSetsuhenDate = selectedVos(selIndex).SaisyuSetsuhenDate
                                '    '更新日をセット
                                '    tehaichoVo.UpdatedUserId = LoginInfo.Now.UserId
                                '    tehaichoVo.UpdatedDate = aDate.CurrentDateDbFormat
                                '    tehaichoVo.UpdatedTime = aDate.CurrentTimeDbFormat
                                '    Dim resCnt As Integer = tehaichoDao.UpdateByPk(tehaichoVo)
                                '    If resCnt <> 1 Then
                                '        Throw New Exception()
                                '    End If
                                'End If

                                '最終織込設変情報へセット
                                sheet.SetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO), _
                                               selectedVos(selIndex).SaisyuSetsuhenKaiteiNo)
                                sheet.SetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_STSR_DHSTBA), _
                                               selectedVos(selIndex).StsrDhstba)
                                sheet.SetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_DATE), _
                                               ConvDateInt8(selectedVos(selIndex).SaisyuSetsuhenDate))

                            End If

                        End If
                    Next
                Next

                'watch.Stop()
                'Console.WriteLine(String.Format("最新と織込差の更新-実行時間 : {0}ms", watch.ElapsedMilliseconds))
                'watch.Reset()

                'watch.Start()
                'db.Commit()
                'watch.Stop()
                'Console.WriteLine(String.Format("コミット-実行時間 : {0}ms", watch.ElapsedMilliseconds))
                'watch.Reset()
                'End Using

                'Return True

                'セット後、保存する。
                If Save() = True Then
                    Return True
                End If
                '
                Return False

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("最新と織込差の更新中にエラーが発生しました。")
                Return False
            End Try
        End Function




        ''' <summary>
        ''' 出図織込み情報更新
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub UpdShutuzuOrikomi(ByVal f As Frm20ShutuzuOrikomi)
            Try
                Dim sheet As FarPoint.Win.Spread.SheetView = f.spdShutuzuOrikomi_Sheet1
                Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(sheet)
                Dim aDate As New ShisakuDate

                Using db As New EBomDbClient
                    db.BeginTransaction()
                    Try
                        Dim orikomiDao As TShisakuTehaiShutuzuOrikomiDao = New TShisakuTehaiShutuzuOrikomiDaoImpl

                        '
                        'スプレッドループ
                        For i As Integer = startRow To sheet.RowCount - 1

                            '確定
                            Dim valkakutei As String = sheet.GetValue(i, _
                                                                    sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_KAKUTEI).Index)
                            If StringUtil.Equals(valkakutei, "True") Then
                                valkakutei = "1"
                            Else
                                valkakutei = "0"
                            End If
                            'ブロック№
                            Dim valBlockNo As String = sheet.GetValue(i, _
                                                                    sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_SHISAKU_BLOCK_NO).Index)
                            '行ID
                            Dim valGyouId As String = sheet.GetValue(i, _
                                                                    sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_GYOU_ID).Index)
                            '部品番号
                            Dim valBuhinNo As String = sheet.GetValue(i, _
                                                                    sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_BUHIN_NO).Index)
                            '代表品番
                            Dim valDaihyoHinban As String = sheet.GetValue(i, _
                                                                    sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_KATA_DAIHYOU_BUHIN_NO).Index)
                            '最新出図_改訂№
                            Dim valNewShutuzuJisekiKaiteiNo As String = sheet.GetValue(i, _
                                                                    sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_NEW_SHUTUZU_JISEKI_KAITEI_NO).Index)
                            '最新出図_設通№
                            Dim valNewShutuzuJisekiStsrDhstba As String = sheet.GetValue(i, _
                                                                    sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_NEW_SHUTUZU_JISEKI_STSR_DHSTBA).Index)
                            '最新出図_受領日
                            Dim valNewShutuzuJisekiJyuryoDate As Integer = ConvInt8Date(sheet.GetValue(i, _
                                                                    sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_NEW_SHUTUZU_JISEKI_JYURYO_DATE).Index))
                            '最新出図_件名
                            Dim valNewShutuzuKenmei As String = sheet.GetValue(i, _
                                                                    sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_NEW_SHUTUZU_KENMEI).Index)
                            '最終織込設変_改訂№
                            Dim valLastShutuzuJisekiKaiteiNo As String = sheet.GetValue(i, _
                                                                    sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_JISEKI_KAITEI_NO).Index)
                            '最終織込設変_設通№
                            Dim valLastShutuzuJisekiStsrDhstba As String = sheet.GetValue(i, _
                                                                    sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_JISEKI_STSR_DHSTBA).Index)
                            '最終織込設変_受領日
                            Dim valLastShutuzuJisekiJyuryoDate As Integer = ConvInt8Date(sheet.GetValue(i, _
                                                                    sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_JISEKI_JYURYO_DATE).Index))
                            '最終織込設変_件名
                            Dim valLastShutuzuKenmei As String = sheet.GetValue(i, _
                                                                    sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_KENMEI).Index)

                            '
                            Dim updBeforeVo As New TShisakuTehaiShutuzuOrikomiVo
                            updBeforeVo = orikomiDao.FindByPk(_headerSubject.shisakuEventCode, _
                                                              _headerSubject.shisakuListCode, _
                                                              _headerSubject.shisakuListCodeKaiteiNo, _
                                                              valBlockNo, valGyouId, valBuhinNo)
                            '
                            If StringUtil.IsNotEmpty(updBeforeVo) Then
                                '値をVOへセット
                                updBeforeVo.Kakutei = valkakutei
                                updBeforeVo.NewShutuzuJisekiKaiteiNo = valNewShutuzuJisekiKaiteiNo
                                updBeforeVo.NewShutuzuJisekiStsrDhstba = valNewShutuzuJisekiStsrDhstba
                                updBeforeVo.NewShutuzuJisekiJyuryoDate = valNewShutuzuJisekiJyuryoDate
                                updBeforeVo.NewShutuzuKenmei = valNewShutuzuKenmei
                                '
                                updBeforeVo.LastShutuzuJisekiKaiteiNo = valLastShutuzuJisekiKaiteiNo
                                updBeforeVo.LastShutuzuJisekiStsrDhstba = valLastShutuzuJisekiStsrDhstba
                                updBeforeVo.LastShutuzuJisekiJyuryoDate = valLastShutuzuJisekiJyuryoDate
                                updBeforeVo.LastShutuzuKenmei = valLastShutuzuKenmei
                                '
                                updBeforeVo.UpdatedUserId = LoginInfo.Now.UserId
                                updBeforeVo.UpdatedDate = aDate.CurrentDateDbFormat
                                updBeforeVo.UpdatedTime = aDate.CurrentTimeDbFormat
                                '
                                orikomiDao.UpdateByPk(updBeforeVo)
                            Else
                                Dim addVo As New TShisakuTehaiShutuzuOrikomiVo
                                '
                                addVo.ShisakuEventCode = _headerSubject.shisakuEventCode
                                addVo.ShisakuListCode = _headerSubject.shisakuListCode
                                addVo.ShisakuListCodeKaiteiNo = _headerSubject.shisakuListCodeKaiteiNo
                                addVo.ShisakuBlockNo = valBlockNo
                                addVo.GyouId = valGyouId
                                addVo.BuhinNo = valBuhinNo
                                addVo.KataDaihyouBuhinNo = valDaihyoHinban.Trim
                                '
                                addVo.Kakutei = valkakutei
                                addVo.NewShutuzuJisekiKaiteiNo = valNewShutuzuJisekiKaiteiNo
                                addVo.NewShutuzuJisekiStsrDhstba = valNewShutuzuJisekiStsrDhstba
                                addVo.NewShutuzuJisekiJyuryoDate = valNewShutuzuJisekiJyuryoDate
                                addVo.NewShutuzuKenmei = valNewShutuzuKenmei
                                '
                                addVo.LastShutuzuJisekiKaiteiNo = valLastShutuzuJisekiKaiteiNo
                                addVo.LastShutuzuJisekiStsrDhstba = valLastShutuzuJisekiStsrDhstba
                                addVo.LastShutuzuJisekiJyuryoDate = valLastShutuzuJisekiJyuryoDate
                                addVo.LastShutuzuKenmei = valLastShutuzuKenmei
                                '
                                addVo.CreatedUserId = LoginInfo.Now.UserId
                                addVo.CreatedDate = aDate.CurrentDateDbFormat
                                addVo.CreatedTime = aDate.CurrentTimeDbFormat
                                addVo.UpdatedUserId = LoginInfo.Now.UserId
                                addVo.UpdatedDate = aDate.CurrentDateDbFormat
                                addVo.UpdatedTime = aDate.CurrentTimeDbFormat
                                '
                                orikomiDao.InsertBy(addVo)
                            End If
                        Next

                    Catch ex As Exception
                        db.Rollback()
                        MsgBox("試作手配出図織込情報を更新出来ませんでした。")
                    End Try
                    db.Commit()
                End Using

            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#Region "スプレッド読込専用列設定"
        ''' <summary>
        ''' スプレッド読込専用列設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub LockColShutuzuOrikomiSpread(ByVal frm As Frm20ShutuzuOrikomi)
            Dim sheet As FarPoint.Win.Spread.SheetView

            sheet = frm.spdShutuzuOrikomi_Sheet1

            sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_SHISAKU_BLOCK_NO).Locked = True
            sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_GYOU_ID).Locked = True
            sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_BUHIN_NO).Locked = True
            sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_KATA_DAIHYOU_BUHIN_NO).Locked = True
            sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_NEW_SHUTUZU_JISEKI_KAITEI_NO).Locked = True
            sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_NEW_SHUTUZU_JISEKI_STSR_DHSTBA).Locked = True
            sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_NEW_SHUTUZU_JISEKI_JYURYO_DATE).Locked = True
            sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_NEW_SHUTUZU_KENMEI).Locked = True

            sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_JISEKI_KAITEI_NO).Locked = True
            sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_JISEKI_STSR_DHSTBA).Locked = True
            sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_JISEKI_JYURYO_DATE).Locked = True
            sheet.Columns(NmSpdTagShutuzuOrikomiSa.TAG_LAST_SHUTUZU_KENMEI).Locked = True

        End Sub

#End Region


#End Region





#Region "出図取込確認ダイアログ"


        ''' <summary>
        ''' 「最新出図情報」と基本情報へ更新
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function UpdateDataSaishinShutuzu(ByVal selectedVos As List(Of TShisakuTehaiShutuzuJisekiVo)) As Boolean
            Try
                Dim watch As New Stopwatch()
                Dim aDate As New ShisakuDate

                Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
                Dim startRow As Integer = GetTitleRowsIn(sheet)

                'Dim tehaichoDao As TShisakuTehaiKihonDao = New TShisakuTehaiKihonDaoImpl
                'Dim tehaichoVo As TShisakuTehaiKihonVo = Nothing
                'Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                '    db.Open()
                '    '－トランザクション開始－
                '    db.BeginTransaction()

                '    watch.Start()

                '部品番号重複チェック用
                Dim b_blockNo As String = String.Empty
                Dim b_buhinNo As String = String.Empty

                For selIndex As Integer = 0 To selectedVos.Count - 1

                    '最終改訂№の情報のみ更新する。
                    If (StringUtil.IsEmpty(b_blockNo) Or StringUtil.IsEmpty(b_buhinNo)) _
                        Or (Not StringUtil.Equals(b_blockNo, selectedVos(selIndex).ShisakuBlockNo) Or _
                            Not StringUtil.Equals(b_buhinNo, selectedVos(selIndex).BuhinNo)) Then

                        For rowIndex As Integer = startRow To sheet.RowCount - 1

                            '同一行の場合
                            If String.Equals(sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)), selectedVos(selIndex).ShisakuBlockNo) And _
                               String.Equals(sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO)), selectedVos(selIndex).BuhinNo) Then

                                'tehaichoVo = tehaichoDao.FindByPk(_headerSubject.shisakuEventCode, _
                                '                                      _headerSubject.shisakuListCode, _
                                '                                      _headerSubject.shisakuListCodeKaiteiNo, _
                                '                                      sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)), _
                                '                                      sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)), _
                                '                                      CInt(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN))))
                                ''最終織込設変情報をセット
                                'tehaichoVo.ShutuzuJisekiKaiteiNo = selectedVos(selIndex).ShutuzuJisekiKaiteiNo
                                'tehaichoVo.ShutuzuJisekiStsrDhstba = selectedVos(selIndex).ShutuzuJisekiStsrDhstba
                                'tehaichoVo.ShutuzuJisekiDate = selectedVos(selIndex).ShutuzuJisekiJyuryoDate
                                ''更新日をセット
                                'tehaichoVo.UpdatedUserId = LoginInfo.Now.UserId
                                'tehaichoVo.UpdatedDate = aDate.CurrentDateDbFormat
                                'tehaichoVo.UpdatedTime = aDate.CurrentTimeDbFormat
                                'Dim resCnt As Integer = tehaichoDao.UpdateByPk(tehaichoVo)

                                'If resCnt <> 1 Then
                                '    Throw New Exception()
                                'End If

                                '最新出図実績情報へセット
                                sheet.SetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO), _
                                               selectedVos(selIndex).ShutuzuJisekiKaiteiNo)
                                sheet.SetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA), _
                                               selectedVos(selIndex).ShutuzuJisekiStsrDhstba)
                                sheet.SetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_DATE), _
                                               ConvDateInt8(selectedVos(selIndex).ShutuzuJisekiJyuryoDate))

                            End If
                        Next

                    End If
                    '部品№を保持
                    b_blockNo = selectedVos(selIndex).ShisakuBlockNo
                    b_buhinNo = selectedVos(selIndex).BuhinNo
                Next

                '    watch.Stop()
                '    Console.WriteLine(String.Format("最新と織込差の更新-実行時間 : {0}ms", watch.ElapsedMilliseconds))
                '    watch.Reset()

                '    watch.Start()
                '    db.Commit()
                '    watch.Stop()
                '    Console.WriteLine(String.Format("コミット-実行時間 : {0}ms", watch.ElapsedMilliseconds))
                '    watch.Reset()
                'End Using

                'セット後、保存する。
                If Save() = True Then
                    Return True
                End If

                Return False

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("最新と織込差の更新中にエラーが発生しました。")
                Return False
            End Try
        End Function




#Region "スプレッド読込専用列設定"
        ''' <summary>
        ''' スプレッド読込専用列設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub LockColShutuzuTorikomiSpread(ByVal frm As Frm20ShutuzuTorikomi)
            Dim sheet As FarPoint.Win.Spread.SheetView

            sheet = frm.spdShutuzuTorikomi_Sheet1

            sheet.Columns(NmSpdTagShutuzuJiseki.TAG_SHISAKU_BLOCK_NO).Locked = True
            sheet.Columns(NmSpdTagShutuzuJiseki.TAG_BUHIN_NO).Locked = True
            sheet.Columns(NmSpdTagShutuzuJiseki.TAG_KATA_DAIHYOU_BUHIN_NO).Locked = True
            sheet.Columns(NmSpdTagShutuzuJiseki.TAG_NEW_SHUTUZU_JISEKI_KAITEI_NO).Locked = True
            sheet.Columns(NmSpdTagShutuzuJiseki.TAG_NEW_SHUTUZU_JISEKI_STSR_DHSTBA).Locked = True
            sheet.Columns(NmSpdTagShutuzuJiseki.TAG_NEW_SHUTUZU_JISEKI_JYURYO_DATE).Locked = True
            sheet.Columns(NmSpdTagShutuzuJiseki.TAG_NEW_SHUTUZU_KENMEI).Locked = True

        End Sub

#End Region


#End Region




#Region "出図改訂履歴"


        ''' <summary>
        ''' 出図実績情報検索
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function TehaiShutuzuJiseki(ByVal blockNo As String, ByVal buhinNo As String, Optional ByVal descJun As Boolean = True) As List(Of TShisakuTehaiShutuzuJisekiVo)
            Dim impl As TehaichoHeaderDao = New TehaichoHeaderDaoImpl

            Dim shutuzuJisekiVos As List(Of TShisakuTehaiShutuzuJisekiVo) = _
            impl.FindShutuzuJisekiBy(_headerSubject.shisakuEventCode, _
                                                    _headerSubject.shisakuListCode, _
                                                    _headerSubject.shisakuListCodeKaiteiNo, _
                                                    blockNo, buhinNo, _
                                                    descJun)    '改訂№昇順で
            Return shutuzuJisekiVos
        End Function

        ''' <summary>
        ''' 出図実績手入力情報検索
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function TehaiShutuzuJisekiInput(ByVal blockNo As String, _
                                                ByVal buhinNo As String, _
                                                ByVal kaiteiNo As String, _
                                                ByVal maxKaitei As Boolean) As TShisakuTehaiShutuzuJisekiInputVo

            Dim impl As TehaichoHeaderDao = New TehaichoHeaderDaoImpl

            Dim shutuzuJisekiInputVos As TShisakuTehaiShutuzuJisekiInputVo = _
            impl.FindShutuzuJisekiInputBy(_headerSubject.shisakuEventCode, _
                                                    _headerSubject.shisakuListCode, _
                                                    _headerSubject.shisakuListCodeKaiteiNo, _
                                                    blockNo, buhinNo, _
                                                    kaiteiNo, _
                                                    maxKaitei)    '改訂№で
            Return shutuzuJisekiInputVos
        End Function


        ''' <summary>
        ''' 出図実績情報更新
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub UpdShutuzuKaiteiRireki(ByVal f As Frm20ShutuzuKaiteiRireki, ByVal blockNo As String, ByVal buhinNo As String)
            Try
                Dim sheet As FarPoint.Win.Spread.SheetView = f.spdShutuzuKaiteiRireki_Sheet1
                Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(sheet)
                Dim aDate As New ShisakuDate
                Dim aInputRow As Integer = 0

                Using db As New EBomDbClient
                    db.BeginTransaction()
                    Try
                        '最終行は手入力行
                        aInputRow = sheet.RowCount - 1

                        If startRow < aInputRow Then
                            Dim jisekiDao As TShisakuTehaiShutuzuJisekiDao = New TShisakuTehaiShutuzuJisekiDaoImpl
                            '
                            'スプレッドループ
                            '   最終行は手入力行なので除外する。
                            'For i As Integer = startRow To sheet.RowCount - 1
                            For i As Integer = startRow To sheet.RowCount - 2

                                '改訂№
                                Dim valKaiteiNo As String = sheet.GetValue(i, _
                                                                        sheet.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_KAITEI_NO).Index)
                                'コメント
                                Dim valComment As String = sheet.GetValue(i, _
                                                                        sheet.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_COMMENT).Index)

                                '
                                Dim updBeforeVo As New TShisakuTehaiShutuzuJisekiVo
                                updBeforeVo = jisekiDao.FindByPk(_headerSubject.shisakuEventCode, _
                                                                  _headerSubject.shisakuListCode, _
                                                                  _headerSubject.shisakuListCodeKaiteiNo, _
                                                                  blockNo, buhinNo, valKaiteiNo)
                                '
                                If StringUtil.IsNotEmpty(updBeforeVo) Then
                                    '
                                    updBeforeVo.Comment = valComment
                                    '
                                    'updBeforeVo.CreatedUserId = LoginInfo.Now.UserId
                                    'updBeforeVo.CreatedDate = aDate.CurrentDateDbFormat
                                    'updBeforeVo.CreatedTime = aDate.CurrentTimeDbFormat
                                    updBeforeVo.UpdatedUserId = LoginInfo.Now.UserId
                                    updBeforeVo.UpdatedDate = aDate.CurrentDateDbFormat
                                    updBeforeVo.UpdatedTime = aDate.CurrentTimeDbFormat

                                    jisekiDao.UpdateByPk(updBeforeVo)

                                End If
                            Next
                        End If


                        '手入力行を更新する。
                        Dim jisekiInputDao As TShisakuTehaiShutuzuJisekiInputDao = New TShisakuTehaiShutuzuJisekiInputDaoImpl
                        '
                        '改訂№
                        Dim valInputKaiteiNo As String = sheet.GetValue(aInputRow, _
                                                                sheet.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_KAITEI_NO).Index)
                        '設通№
                        Dim valInputDhstba As String = sheet.GetValue(aInputRow, _
                                                                sheet.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_STSR_DHSTBA).Index)
                        '受領日
                        Dim valInputjyuryoDate As Integer = ConvInt8Date(sheet.GetValue(aInputRow, _
                                                                sheet.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_JYURYO_DATE).Index))
                        '件名
                        Dim valInputKenmei As String = sheet.GetValue(aInputRow, _
                                                                sheet.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_KENMEI).Index)
                        'コメント
                        Dim valInputComment As String = sheet.GetValue(aInputRow, _
                                                                sheet.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_COMMENT).Index)
                        '
                        Dim updInputBeforeVo As New TShisakuTehaiShutuzuJisekiInputVo
                        updInputBeforeVo = jisekiInputDao.FindByPk(_headerSubject.shisakuEventCode, _
                                                          _headerSubject.shisakuListCode, _
                                                          _headerSubject.shisakuListCodeKaiteiNo, _
                                                          blockNo, buhinNo, valInputKaiteiNo)
                        '同一キーが存在した場合、更新
                        If StringUtil.IsNotEmpty(updInputBeforeVo) Then
                            '
                            updInputBeforeVo.ShutuzuJisekiStsrDhstba = valInputDhstba
                            updInputBeforeVo.ShutuzuJisekiJyuryoDate = valInputjyuryoDate
                            updInputBeforeVo.ShutuzuKenmei = valInputKenmei
                            updInputBeforeVo.Comment = valInputComment
                            '
                            updInputBeforeVo.UpdatedUserId = LoginInfo.Now.UserId
                            updInputBeforeVo.UpdatedDate = aDate.CurrentDateDbFormat
                            updInputBeforeVo.UpdatedTime = aDate.CurrentTimeDbFormat
                            '
                            jisekiInputDao.UpdateByPk(updInputBeforeVo)
                        Else    '同一キーが存在しない場合、追加
                            Dim insVo As New TShisakuTehaiShutuzuJisekiInputVo
                            insVo.ShisakuEventCode = _headerSubject.shisakuEventCode
                            insVo.ShisakuListCode = _headerSubject.shisakuListCode
                            insVo.ShisakuListCodeKaiteiNo = _headerSubject.shisakuListCodeKaiteiNo
                            insVo.ShisakuBlockNo = blockNo
                            insVo.BuhinNo = buhinNo
                            '
                            insVo.ShutuzuJisekiKaiteiNo = valInputKaiteiNo
                            insVo.ShutuzuJisekiStsrDhstba = valInputDhstba
                            insVo.ShutuzuJisekiJyuryoDate = valInputjyuryoDate
                            insVo.ShutuzuKenmei = valInputKenmei
                            insVo.Comment = valInputComment
                            '
                            insVo.CreatedUserId = LoginInfo.Now.UserId
                            insVo.CreatedDate = aDate.CurrentDateDbFormat
                            insVo.CreatedTime = aDate.CurrentTimeDbFormat
                            '
                            insVo.UpdatedUserId = LoginInfo.Now.UserId
                            insVo.UpdatedDate = aDate.CurrentDateDbFormat
                            insVo.UpdatedTime = aDate.CurrentTimeDbFormat
                            '
                            jisekiInputDao.InsertBy(insVo)
                        End If

                    Catch ex As Exception
                        db.Rollback()
                        MsgBox("試作手配出図実績情報を更新出来ませんでした。")
                    End Try
                    db.Commit()
                End Using

            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#Region "スプレッド読込専用列設定"
        ''' <summary>
        ''' スプレッド読込専用列設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub LockColShutuzuKaiteiRirekiSpread(ByVal frm As Frm20ShutuzuKaiteiRireki)
            Dim sheet As FarPoint.Win.Spread.SheetView

            sheet = frm.spdShutuzuKaiteiRireki_Sheet1

            sheet.Columns(NmSpdTagShutuzuKaiteiRireki.TAG_SHUTUZU_JISEKI_KAITEI_NO).Locked = True

        End Sub

#End Region


#End Region






#Region "図面設通ログオン"

        ''' <summary>
        ''' 図面設通ログオンする
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <param name="userID">ユーザーID</param>
        ''' <remarks></remarks>
        Public Sub SettsuLogon(ByVal BuhinNo As String, ByVal userID As String)

            '2015/12/23 kabasawa'
            '部品番号のまま図面設通ログオンするとタイトル図面以外取得できないので部品番号に紐付く最新の図面番号を取得する。'
            'Dim zVos As New List(Of Rhac0533Vo)
            '第２引数は何もしてない。'
            'zVos = ZumenCheck(BuhinNo, 0)
            'Dim zumenNo As String = ""
            'If Not zVos Is Nothing Then
            '    If StringUtil.IsNotEmpty(zVos(0).ZumenNo) Then
            '        zumenNo = zVos(0).ZumenNo
            '    End If
            'Else
            '    Exit Sub
            'End If

            'Dim logon As New SettsuLogonController(userID, zumenNo.Trim)
            Dim logon As New SettsuLogonController(userID, BuhinNo.Trim)

            '本番サーバかそれ以外かで比較判定を行い、本番であればTrue、
            If UCase(ComFunc.GetServer(g_kanrihyoIni("EBOM_DB"))) = "FGNT-SQL1" Then
                logon.settsuLogon(True)
            Else
                logon.settsuLogon(True)
                'logon.settsuLogon(False)
            End If

        End Sub

#Region "設通ログオン"

        ''' <summary>
        ''' 
        ''' 「設通ログオン」の制御と、動作を担うクラス
        ''' </summary>
        ''' <remarks></remarks>
        ''' 
        Private Class SettsuLogonController

#Region "定数定義"

            Private Const SETTSU_ZUBAN_LENGTH As Long = 13    '図番桁数
            Private Const SETTSU_USER_ID_LENGTH As Long = 10     'ﾕｰｻﾞｰID桁数

            Private Const ZUBAN_SPACE As String = " "
            Private Const ZUBAN_SHARP As String = "#"

            Private Const URL_SPACE As String = "%20"
            Private Const URL_SHARP As String = "%23"

            '図面設通ログオンURL（本番URL)
            Private Const SETTSU_LOGON_URL_REAL As String _
            = "http://zumen-settu.gkh.subaru-fhi.co.jp:122/FHI/ZSCL70GKH2?"

            '図面設通ログオンURL（テスト環境URL)
            Private Const SETTSU_LOGON_URL_FAKE As String _
            = "http://zumen-settu.gkh.subaru-fhi.co.jp:9898/FHI/ZSCL70GKH2?"

            '質量原価管理表ログオンURL
            Private Const SITSURYO_GENKA_LOGON_URL As String _
            = "http://zumen-settu.gkh.subaru-fhi.co.jp:122/RHAB/RHAB241GKH?"

#End Region

#Region "変数定義"

            Private _userId As String
            Private _zumenNoArg As String

#End Region

#Region "公開メソッド"

            ''' <summary>
            ''' コンストラクター
            ''' </summary>
            ''' <param name="userId"></param>
            ''' <param name="zumenNoArg"></param>
            ''' <remarks></remarks>
            Public Sub New(ByVal userId As String, ByVal zumenNoArg As String)
                _userId = userId
                _zumenNoArg = zumenNoArg
            End Sub

            ''' <summary>
            ''' 図面設通へのログオン処理
            ''' </summary>
            ''' <remarks></remarks>
            Public Sub settsuLogon(ByVal IsReal As Boolean)

                Dim zumenNo As Object = _zumenNoArg
                If IsEmpty(zumenNo) Then
                    MsgBox(String.Format("{0} が無いので、実行できません。", "文字列"), vbInformation)
                    Return
                End If

                'SETTSU_LOGON_URL()
                Dim settsuLogonUrl As String = Nothing

                '引数により、切替
                If IsReal Then
                    '本番用
                    settsuLogonUrl = SETTSU_LOGON_URL_REAL
                Else
                    'テスト環境用
                    settsuLogonUrl = SETTSU_LOGON_URL_FAKE
                End If

                'settsuLogonUrl = SETTSU_LOGON_URL_REAL
                Dim url As String = settsuLogonUrl & makeUserId(_userId) & makeTitleZuban(zumenNo.ToString)
                url &= "1" '⇒群馬　'g_siteInfo
                url &= MakeArgIpAddress()

                Dim exec As New Executer
                exec.Exec(url)

            End Sub

            ''' <summary>
            ''' 質量原価管理表ログオン処理
            ''' </summary>
            ''' <remarks>
            ''' 注意：業務上公開されていない機能なので
            ''' 使用時にはお客様への確認を行ってください。
            ''' </remarks>
            Public Sub shitsuryoGenkaLogon()

                Dim zumenNo As Object = _zumenNoArg
                If IsEmpty(zumenNo) Then
                    MsgBox(String.Format("{0} が無いので、実行できません。", "文字列"), vbInformation)
                    Return
                End If

                'SITSURYO_GENKA_LOGON_URL()
                Dim shitsuryoLogonUrl As String = SITSURYO_GENKA_LOGON_URL
                Dim url As String = shitsuryoLogonUrl & makeUserId(_userId) & makeTitleZuban(zumenNo.ToString)
                url &= "1" '⇒群馬　'g_siteInfo
                url &= MakeArgIpAddress()

                Dim exec As New Executer
                exec.Exec(url)

            End Sub

#End Region

#Region "ファンクション"

            ''' <summary>
            ''' 図面設通ｻﾌﾞｼｽﾃﾑをｺｰﾙする為のﾕｰｻﾞｰID文字列生成
            ''' </summary>
            ''' <param name="userId"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Private Function makeUserId(ByVal userId As String) As String

                'ｽﾍﾟｰｽ入り6文字のﾕｰｻﾞｰIDを作る
                Dim userLengthStr As String = Strings.Left((Trim(userId) & Space(SETTSU_USER_ID_LENGTH)), SETTSU_USER_ID_LENGTH)

                '半角ｽﾍﾟｰｽの置き換え
                Return encodeUrl(userLengthStr)
            End Function

            ''' <summary>
            ''' 図面設通ｻﾌﾞｼｽﾃﾑをｺｰﾙする為の図番文字列生成
            ''' </summary>
            ''' <param name="titleZuban"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Private Function makeTitleZuban(ByVal titleZuban As String) As String

                'ｽﾍﾟｰｽ入りの13桁図番を作る
                Dim zubanLengthStr As String = Strings.Left((Trim(titleZuban) & Space(SETTSU_ZUBAN_LENGTH)), SETTSU_ZUBAN_LENGTH)

                Return encodeUrl(zubanLengthStr)

            End Function

            ''' <summary>
            ''' IPアドレスが、172.21.10.110だった場合
            ''' 999.999.999.999 －　172.021.010.110 = 827.978.989.889 のピリオド抜きを返す
            ''' </summary>
            ''' <returns>IPアドレス引数</returns>
            ''' <remarks></remarks>
            Private Function MakeArgIpAddress() As String
                Dim ipAddresses As String() = NetworkUtil.GetIpAddressesAtMyComputerAsString
                For Each ipAddress As String In ipAddresses
                    If Not NetworkUtil.IsIPv4(ipAddress) Then
                        Continue For
                    End If
                    Dim result As New List(Of String)
                    Dim strings As String() = Split(ipAddress, ".")
                    For Each addr As String In strings
                        result.Add(Format("000", 999 - CInt(addr)))
                    Next
                    Return Join(result.ToArray, "")
                Next
                Return Repeat("9"c, 12)
            End Function

            ''' <summary>
            ''' URLのエンコード処理
            ''' </summary>
            ''' <param name="encodeeStr"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Private Shared Function encodeUrl(ByVal encodeeStr As String) As String

                ' 半角ｽﾍﾟｰｽの置き換え
                Dim replacedSpace As String = Replace(encodeeStr, ZUBAN_SPACE, URL_SPACE)

                ' "#" 文字の置き換え
                Return Replace(replacedSpace, ZUBAN_SHARP, URL_SHARP)
            End Function

            ''' <summary>
            ''' 値を整形する
            ''' </summary>
            ''' <param name="formatString">整形書式</param>
            ''' <param name="value">値</param>
            ''' <returns>整形した値</returns>
            ''' <remarks></remarks>
            Public Shared Function Format(ByVal formatString As String, ByVal value As Object) As String
                Dim sb As New StringBuilder
                Return String.Format(sb.Append("{0:").Append(formatString).Append("}").ToString, value)
            End Function

            ''' <summary>
            ''' 文字を繰り返して返す
            ''' </summary>
            ''' <param name="c">文字</param>
            ''' <param name="count">繰り返し数</param>
            ''' <returns>繰り返した文字列</returns>
            ''' <remarks></remarks>
            Public Shared Function Repeat(ByVal c As Char, ByVal count As Integer) As String
                Dim sb As New StringBuilder
                sb.Append(c, count)
                Return sb.ToString
            End Function

            ''' <summary>
            ''' Empty値、またはNull値か、を返す
            ''' </summary>
            ''' <param name="str">判定する文字列</param>
            ''' <returns>Empty値かNull値の場合、true</returns>
            ''' <remarks></remarks>
            Private Function IsEmpty(ByVal str As Object) As Boolean
                Return str Is Nothing OrElse str.ToString.Trim.Length = 0
            End Function

#End Region

        End Class

#End Region

#Region "ネットワーク"


        ''' <summary>
        ''' ネットワーク関連のユーティリティ
        ''' </summary>
        ''' <remarks></remarks>
        ''' 

        Private Class NetworkUtil
            ''' <summary>
            ''' 処理中のマシンのIPアドレスを返す
            ''' </summary>
            ''' <returns>IPアドレス</returns>
            ''' <remarks></remarks>
            Public Shared Function GetIpAddressesAtMyComputer() As IPAddress()
                Return Dns.GetHostAddresses(Dns.GetHostName)
            End Function

            ''' <summary>
            ''' 処理中のマシンのIPアドレス（文字列）を返す
            ''' </summary>
            ''' <returns>IPアドレス（文字列）</returns>
            ''' <remarks></remarks>
            Public Shared Function GetIpAddressesAtMyComputerAsString() As String()
                Dim result As New List(Of String)
                For Each ip As IPAddress In GetIpAddressesAtMyComputer()
                    result.Add(ip.ToString)
                Next
                Return result.ToArray
            End Function

            ''' <summary>
            ''' IPv6か？を返す
            ''' </summary>
            ''' <param name="ip">IPアドレス</param>
            ''' <returns>判定結果</returns>
            ''' <remarks></remarks>
            Public Shared Function IsIPv6(ByVal ip As IPAddress) As Boolean
                If ip Is Nothing Then
                    Return False
                End If
                Return ip.IsIPv6LinkLocal OrElse ip.IsIPv6Multicast OrElse ip.IsIPv6SiteLocal
            End Function

            ''' <summary>
            ''' IPv4か？を返す
            ''' </summary>
            ''' <param name="ip">IPアドレス</param>
            ''' <returns>判定結果</returns>
            ''' <remarks></remarks>
            Public Shared Function IsIPv4(ByVal ip As IPAddress) As Boolean
                If ip Is Nothing Then
                    Return False
                End If
                Return Not IsIPv6(ip)
            End Function

            ''' <summary>
            ''' IPv4か？を返す
            ''' </summary>
            ''' <param name="ipAddress">IPアドレス</param>
            ''' <returns>判定結果</returns>
            ''' <remarks></remarks>
            Public Shared Function IsIPv4(ByVal ipAddress As String) As Boolean
                If ipAddress Is Nothing OrElse ipAddress.ToString.Trim.Length = 0 Then
                    Return False
                End If
                Dim addresses As String() = Split(ipAddress, ".")
                If addresses.Length <> 4 Then
                    Return False
                End If
                For Each address As String In addresses
                    If Not IsNumeric(address) OrElse CInt(address) < 0 OrElse 255 < CInt(address) Then
                        Return False
                    End If
                Next
                Return True
            End Function

            ''' <summary>
            ''' メールを送信する
            ''' </summary>
            ''' <param name="smtpServerHostName">SMTPサーバ名</param>
            ''' <param name="smtpServerPortNo">ポート番号</param>
            ''' <param name="fromAddress">メールアドレス（From）</param>
            ''' <param name="toAddresses">メールアドレス（To）</param>
            ''' <param name="smtpAuthUserName">認証ユーザ</param>
            ''' <param name="smtpAuthPassword">認証パスワード</param>
            ''' <param name="enableSSL">SSLの使用有無</param>
            ''' <param name="timeoutMillis">タイムアウト（ミリ秒）</param>
            ''' <param name="ccAddresses">メールアドレス（Cc）</param>
            ''' <param name="bccAddresses">メールアドレス（Bcc）</param>
            ''' <param name="priority">優先度</param>
            ''' <param name="subject">件名</param>
            ''' <param name="body">本文</param>
            ''' <param name="isBodyHtml">HTMLかどうか</param>
            ''' <param name="attachFiles">添付ファイル</param>
            ''' <param name="subjectEncoding">エンコード（件名）</param>
            ''' <param name="bodyEncoding">エンコード（本文）</param>
            ''' <remarks></remarks>
            Public Shared Sub SendMail(ByVal smtpServerHostName As String, ByVal smtpServerPortNo As Integer, _
                                       ByVal fromAddress As String, ByVal toAddresses As String(), ByVal subject As String, ByVal body As String, _
                                       Optional ByVal smtpAuthUserName As String = Nothing, Optional ByVal smtpAuthPassword As String = Nothing, _
                                       Optional ByVal enableSSL As Boolean = False, Optional ByVal timeoutMillis As Integer = 100000, _
                                       Optional ByVal ccAddresses As String() = Nothing, Optional ByVal bccAddresses As String() = Nothing, _
                                       Optional ByVal priority As MailPriority = MailPriority.Normal, Optional ByVal isBodyHtml As Boolean = False, _
                                       Optional ByVal attachFiles As FileInfo() = Nothing, _
                                       Optional ByVal subjectEncoding As String = "UTF-8", Optional ByVal bodyEncoding As String = "UTF-8")

                'SMTPサーバを設定
                Dim aSmtpClient As New SmtpClient(smtpServerHostName, smtpServerPortNo)

                'SMTP認証の設定
                If smtpAuthUserName IsNot Nothing AndAlso smtpAuthPassword IsNot Nothing Then
                    aSmtpClient.Credentials = New NetworkCredential(smtpAuthUserName, smtpAuthPassword)
                End If

                aSmtpClient.EnableSsl = enableSSL
                aSmtpClient.Timeout = timeoutMillis

                'メールの設定
                Using mail As New MailMessage

                    mail.From = New MailAddress(fromAddress)

                    If toAddresses IsNot Nothing Then
                        For Each addr As String In toAddresses
                            mail.To.Add(addr)
                        Next
                    End If

                    If ccAddresses IsNot Nothing Then
                        For Each addr As String In ccAddresses
                            mail.CC.Add(addr)
                        Next
                    End If

                    If bccAddresses IsNot Nothing Then
                        For Each addr As String In bccAddresses
                            mail.Bcc.Add(addr)
                        Next
                    End If

                    mail.Priority = priority

                    mail.Subject = subject
                    mail.Body = body
                    mail.IsBodyHtml = isBodyHtml

                    '添付ファイル
                    If attachFiles IsNot Nothing Then
                        For Each f As FileInfo In attachFiles
                            mail.Attachments.Add(New Attachment(f.FullName))
                        Next
                    End If
                    mail.SubjectEncoding = Encoding.GetEncoding(subjectEncoding)
                    mail.BodyEncoding = Encoding.GetEncoding(bodyEncoding)

                    'メールを送信
                    aSmtpClient.Send(mail)
                End Using
            End Sub

        End Class


#End Region

#Region "外部プログラム実行クラス"


        ''' <summary>
        ''' 外部プログラム実行クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class Executer

            ''' <summary>Window表示する外部アプリの場合、true</summary>
            Private _IsShowWindowApp As Boolean

            Private _IsWaitForExit As Boolean = True

            ''' <summary>実行時の表示Window状態</summary>
            Public ProcessWindowStyle As ProcessWindowStyle = ProcessWindowStyle.Hidden

            ''' <summary>プロンプト処理の場合、true  ※初期値true</summary>
            Public IsPrompt As Boolean = True

            ''' <summary>Window表示する外部アプリの場合、true</summary>
            Public Property IsShowWindowApp() As Boolean
                Get
                    Return _IsShowWindowApp
                End Get
                Set(ByVal value As Boolean)
                    _IsShowWindowApp = value
                    If _IsShowWindowApp Then
                        ProcessWindowStyle = Diagnostics.ProcessWindowStyle.Normal
                        IsWaitForExit = False
                        IsPrompt = False
                    Else
                        ProcessWindowStyle = Diagnostics.ProcessWindowStyle.Hidden
                        IsWaitForExit = True
                    End If
                End Set
            End Property

            ''' <summary>Excelアドインファイルの場合、true</summary>
            Public Property IsExcelXla() As Boolean
                Get
                    Return IsShowWindowApp
                End Get
                Set(ByVal value As Boolean)
                    IsShowWindowApp = value
                End Set
            End Property

            ''' <summary>実行終了まで待機する場合、true  ※初期値true</summary>
            Public Property IsWaitForExit() As Boolean
                Get
                    Return _IsWaitForExit
                End Get
                Set(ByVal value As Boolean)
                    _IsWaitForExit = value
                End Set
            End Property

            ''' <summary>
            ''' 外部プログラム実行
            ''' </summary>
            ''' <param name="execName">実行プログラムフルパス指定</param>
            ''' <param name="processArgs">実行時引数</param>
            ''' <remarks></remarks>
            Public Sub Exec(ByVal execName As String, _
                                 Optional ByVal processArgs As String = "")

                Dim hProcess As New ProcessStartInfo

                '実行exe名
                hProcess.FileName = execName

                If processArgs <> "" Then

                    '起動引数
                    hProcess.Arguments = processArgs.Replace(vbCrLf, Space(1)).Trim
                End If

                'ウインドウ作成
                hProcess.CreateNoWindow = IsPrompt

                'shell起動
                hProcess.UseShellExecute = True 'False

                'エラーダイアログ表示
                hProcess.ErrorDialog = True

                '画面状態
                hProcess.WindowStyle = Me.ProcessWindowStyle
                ' Minimizedだと、それにフォーカスが移動してしまい、Asyncで実行していると、処理後にBackgroundUiからフォーカスが外れる事があるので。

                'プロセス開始
                Dim runProcess As Process = Process.Start(hProcess)

                If runProcess Is Nothing Then
                    ' ファイルやURLだけが、execNameに指定された場合（exeを直接実行しない場合）
                    Return
                End If
                Try
                    If IsWaitForExit Then
                        'ウエイト
                        runProcess.WaitForExit()

                        If runProcess.ExitCode <> 0 Then
                            Throw New InvalidOperationException("exe起動に失敗.  > " & execName & " " & processArgs)
                        End If
                    End If

                Finally
                    'クローズ
                    runProcess.Close()

                    '初期化
                    runProcess.Dispose()
                End Try
            End Sub


        End Class


#End Region

#End Region

#Region "図面設通ｻﾌﾞｼｽﾃﾑ呼出(未使用？)"

        Private Const ZUBAN_SPACE As String = " "
        Private Const SETTSU_SPACE As String = "%20"


        ''' <summary>
        ''' 図面設通ｻﾌﾞｼｽﾃﾑ呼出
        ''' </summary>
        ''' <remarks></remarks>
        Friend Function LogonZumen(ByVal BuhinNo As String, ByVal rowIndex As Integer) As String
            '2012/10/22 kabasawa 図面設通サブシステム呼出'

            '選択された行のMIX品番'
            Dim result As Boolean = False
            If StringUtil.IsEmpty(BuhinNo) Then
                Return ECS_DATA_LINK
            End If

            '存在チェック'

            Dim zumenNo As List(Of Rhac0533Vo)

            zumenNo = ZumenCheck(BuhinNo, rowIndex)
            '図面の存在チェック'
            If zumenNo Is Nothing Then
                Return ECS_DATA_LINK
            End If

            If zumenNo(0).ZumenNo = "" Then
                Return ECS_DATA_LINK
            End If

            'サイト区分取得
            Dim g_siteinfo As String = ""
            g_siteinfo = GetSiteInfo(LoginInfo.Now.UserId)

            '存在すればログインする'
            Dim url As String = ""
            url = url & "http://zumen-settu.gkh.subaru-fhi.co.jp:9898/FHI/ZSCL70GKH2?"

            url = url & makeUserId(LoginInfo.Now.UserId)
            url = url & makeTitleZuban(zumenNo(0).ZumenNo)
            url = url & g_siteinfo
            ' ▼▼ 2010/12/06 ADD START 図面設通サブシステムURL変更 ▼▼
            url = url & makeIpCnvParam()
            ' ▲▲ 2010/12/06 ADD E N D 図面設通サブシステムURL変更 ▲▲

            'リンク先が取得できなかった場合
            If Trim$(url) = "" Then
                MsgBox("リンク先が取得できませんでした。", MsgBoxStyle.OkOnly)
                'With ShutuzuOrikomiSheet
                '    .Cells(rowIndex, .Columns(OrikomiSijiSpreadUtil.TAG_ECS_LASTEST).Index).Note = "リンク先が取得できませんでした。"
                'End With
                Return ECS_DATA_LINK
            End If
            Return url
        End Function
        '三鷹DBには存在していない情報なのでどうする？'

        ''' <summary>
        ''' サイト区分の取得
        ''' </summary>
        ''' <param name="userId">ユーザーID</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetSiteInfo(ByVal userId As String) As String

            Dim siteInfo As String = ""

            siteInfo = dao.GetSiteInfo(userId).SiteInfo

            Return siteInfo
        End Function


        Private dao As TehaichoHeaderDao

        ''' <summary>
        ''' タイトル図面の取得
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ZumenCheck(ByVal buhinNo As String, ByVal rowIndex As Integer) As List(Of Rhac0533Vo)

            'Dim zumenNo As String = ""
            Dim zumenNo As List(Of Rhac0533Vo)
            Try
                Dim partsCount As Integer = 0

                'Using editDb As New PartsStructEditDb(DEFAULT_CONNECT_RDBMS)
                dao = (New TehaichoHeaderDaoImpl)
                zumenNo = dao.FindByT_ZUMEN(buhinNo)

                'End Using
                'T_ZUMENに存在しなかった場合'
                'If zumenNo.Equals(String.Empty) Then
                If zumenNo.Count = 0 Then
                    'With ShutuzuOrikomiSheet
                    '    .Cells(rowIndex, .Columns(OrikomiSijiSpreadUtil.TAG_ECS_LASTEST).Index).Note = buhinNo & "にはタイトル図番の設定がありません。"
                    'End With
                    MsgBox(buhinNo & "にはタイトル図番の設定がありません。", MsgBoxStyle.OkOnly)
                    Return Nothing
                End If
                If zumenNo(0).ZumenNo = "" Then
                    MsgBox(buhinNo & "にはタイトル図番の設定がありません。", MsgBoxStyle.OkOnly)
                    'With ShutuzuOrikomiSheet
                    '    .Cells(rowIndex, .Columns(OrikomiSijiSpreadUtil.TAG_ECS_LASTEST).Index).Note = buhinNo & "にはタイトル図番の設定がありません。"
                    'End With
                    Return Nothing
                End If

                Return zumenNo

            Catch ex As Exception
                ComFunc.ShowErrMsgBox(SYSERR_00001 _
                                    , ex.Message _
                                    )
                g_log.WriteException(ex)
                Return Nothing
            End Try

        End Function

        ''' <summary>
        ''' 図面設通用ユーザーID作成
        ''' </summary>
        ''' <param name="userId">ユーザーID</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function makeUserId(ByVal userId As String) As String

            Dim userLengthStr As String
            Dim replaceSpace As String
            Dim SETTSU_USER_ID_LENGTH As Integer = 10
            Dim ZUBAN_SPACE As String = " "
            Dim SETTSU_SPACE As String = "%20"

            ' 2010/12/06 図面設通サブシステムURL変更 (ユーザーID：6文字 ⇒ 10文字)
            'ｽﾍﾟｰｽ入り10文字のﾕｰｻﾞｰIDを作る
            userLengthStr = Strings.Left$((Trim$(userId) & _
           Space$(SETTSU_USER_ID_LENGTH)), SETTSU_USER_ID_LENGTH)
            '半角ｽﾍﾟｰｽの置き換え
            replaceSpace = Replace(userLengthStr, ZUBAN_SPACE, SETTSU_SPACE)

            makeUserId = replaceSpace

        End Function

        ''' <summary>
        ''' 図面設通ログイン用タイトル図面番号作成
        ''' </summary>
        ''' <param name="titleZuban"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function makeTitleZuban(ByVal titleZuban As String) As String

            Dim replaceSpace As String
            Dim replaceSharp As String
            Dim zubanLengthStr As String
            Dim SETTSU_ZUBAN_LENGTH As Integer = 13
            Dim ZUBAN_SHARP = "#"
            Dim SETTSU_SHARP As String = "%23"

            'ｽﾍﾟｰｽ入りの13桁図番を作る
            zubanLengthStr = Strings.Left((Trim(titleZuban) & _
                                           Space$(SETTSU_ZUBAN_LENGTH)), SETTSU_ZUBAN_LENGTH)

            '半角ｽﾍﾟｰｽの置き換え
            replaceSpace = Replace(zubanLengthStr, ZUBAN_SPACE, SETTSU_SPACE)

            '#文字列の置き換え
            replaceSharp = Replace(replaceSpace, ZUBAN_SHARP, SETTSU_SHARP)

            makeTitleZuban = replaceSharp

        End Function

        ''' <summary>
        ''' IPアドレスを使用したパラメータ文字列の生成
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function makeIpCnvParam() As String

            Const stdNum As Long = 999

            Dim strIpAddrList As String               'IPアドレス文字列リスト
            Dim strIpAddrArray() As String            'IPアドレス文字列配列
            Dim strIpAddr As String                   'IPアドレス文字列
            Dim strIpAddrBlock() As String            'IPアドレスのブロックの文字列
            Dim numIpAddrBlock As Integer             'IPアドレスのブロックの数値
            Dim paramStr As String                    'パラメータ文字列
            'Dim i As Long                             'ループカウンタ

            strIpAddrList = GetipAddress()

            strIpAddrArray = Split(strIpAddrList, "/")

            ' IPアドレスが複数設定されていた場合、最初のものを取得
            strIpAddr = strIpAddrArray(0)

            strIpAddrBlock = strIpAddr.Split("."c)

            ' IPアドレスのブロックが4つでなかった場合、空文字を返却
            If UBound(strIpAddrBlock) <> 3 Then
                makeIpCnvParam = ""
            End If

            ' パラメータ文字列の生成
            paramStr = ""
            For index As Integer = 0 To UBound(strIpAddrBlock)

                numIpAddrBlock = CInt(strIpAddrBlock(index))
                paramStr = paramStr & CStr(stdNum - numIpAddrBlock)

            Next

            makeIpCnvParam = paramStr

        End Function

        ''' <summary>
        ''' IPアドレスの取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetipAddress() As String

            'ここからIPAdress取得'
            'ログインユーザ名
            Dim userName As String = SystemInformation.UserName
            'ドメイン名
            Dim userDomainName As String = SystemInformation.UserDomainName
            'コンピュータ名
            Dim machineName As String = SystemInformation.ComputerName
            'ネットワーク接続状況
            Dim existsNetworkAccess As Boolean = SystemInformation.Network
            Dim hostName As String = System.Net.Dns.GetHostName
            'ホスト名を指定して、インターネットホストアドレス情報を取得
            Dim ipHost As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(hostName)

            '注意！！OSによってIPアドレスの位置が異なる！！'
            'XP以前の場合'
            'ipHost.AddressList(0) IPV4アドレス'
            'Vista/7の場合'
            'ipHost.AddressList(0) IPV6アドレス'
            'ipHost.AddressList(1) IPV4アドレス'

            'IPv4アドレス
            Dim ipv4Address As System.Net.IPAddress
            'IPv4アドレスの取得
            If ipHost.AddressList.Length > 1 Then
                ipv4Address = ipHost.AddressList(1)
            Else
                ipv4Address = ipHost.AddressList(0)
            End If

            Return ipv4Address.ToString

        End Function

        Private Function StringTrim(ByVal strObj As Object) As String

            If StringUtil.IsEmpty(strObj) Then
                Return ""
            Else
                Return (strObj).trim
            End If
        End Function

#End Region





#Region "ツールバーボタン"

#Region "ボタン（コピー）"
        ''' <summary>
        ''' ボタン（コピー）
        ''' 
        ''' キーボードでCTRL + c を押した事にし
        ''' この後KeyDownイベントに流す
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ToolCopy()

            Dim spd As FpSpread = GetVisibleSpread

            spd.Focus()

            System.Threading.Thread.Sleep(10)
            System.Windows.Forms.SendKeys.Flush()

            ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！
            System.Windows.Forms.SendKeys.Send("^c")

        End Sub

#End Region

#Region "ボタン（切り取り）"
        ''' <summary>
        ''' ボタン（切り取り）
        ''' 
        ''' キーボードでCTRL + Xを押した事にし
        ''' この後KeyDownイベントに流す
        ''' 
        ''' 
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ToolCut()

            Dim spd As FpSpread = GetVisibleSpread

            spd.Focus()

            System.Threading.Thread.Sleep(10)

            System.Windows.Forms.SendKeys.Flush()
            ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！
            System.Windows.Forms.SendKeys.Send("^x")

        End Sub

#End Region

#Region "ボタン（貼りつけ）"
        ''' <summary>
        ''' ボタン（貼りつけ）
        ''' 
        ''' キーボードでCTRL + vを押した事にし
        ''' この後KeyDownイベントに流す
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ToolPaste(ByVal aSheet As Spread.SheetView)

            Dim spd As FpSpread = GetVisibleSpread

            spd.Focus()

            System.Windows.Forms.SendKeys.Flush()
            System.Threading.Thread.Sleep(10)
            ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！
            System.Windows.Forms.SendKeys.Send("^v")


        End Sub
#End Region

#Region "ボタン（列範囲表示・非表示切替）"
        ''' <summary>
        ''' 列範囲表示・非表示切替
        ''' </summary>
        ''' <param name="aVisible"></param>
        ''' <remarks></remarks>
        Public Sub ToolColVisibleSwitch(ByVal aVisible As Boolean)
            Try
                Dim sheet As SheetView = _frmDispTehaiEdit.spdBase_Sheet1

                Dim startCol As Integer = -1
                Dim endCol As Integer = -1

                If IsBaseSpread Then
                    sheet = _frmDispTehaiEdit.spdBase_Sheet1
                Else
                    sheet = _frmDispTehaiEdit.spdGouSya_Sheet1
                End If

                '基本情報・号車情報対象列範囲を取得
                cr = sheet.GetSelection(0)
                startCol = cr.Column
                endCol = cr.Column + cr.ColumnCount - 1

                '選択しているアクティブ列の示列を非表示にします。
                For i As Integer = startCol To endCol
                    sheet.Columns(i).Visible = aVisible
                Next

                '常時非表示列の設定
                SetHiddenCol()


            Catch Exception As Exception
                MessageBox.Show("選択されたセルがありません。")
                Exit Sub
            End Try
        End Sub

#End Region

#Region "ボタン（全列表示）"
        ''' <summary>
        ''' ボタン（全列表示）
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ToolStripAllVisible()

            Dim sheet As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim sheetGousya As SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1
            Dim startGousya As Integer = TehaichoEditNames.START_COLUMMN_GOUSYA_NAME
            Dim endCol As Integer = -1

            '選択しているアクティブ列の示列を非表示にします。

            If IsBaseSpread Then
                sheet = _frmDispTehaiEdit.spdBase_Sheet1
                endCol = sheet.ColumnCount - 1
            Else
                sheet = _frmDispTehaiEdit.spdGouSya_Sheet1
                endCol = startGousya + _dtGousyaNameList.Rows.Count - 1
            End If

            For i As Integer = 0 To endCol
                sheet.Columns(i).Visible = True
            Next

            '常時非表示列の設定
            SetHiddenCol()

        End Sub

#End Region

#Region "ボタン（Undo）"
        ''' <summary>
        ''' ボタン（Undo）
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ToolUndo()
            Try
                Dim spread As Spread.FpSpread = GetVisibleSpread

                _frmDispTehaiEdit.Activate()
                _frmDispTehaiEdit.spdBase.Focus()

                ' 元に戻すことが出来るか確認します。。
                If spread.UndoManager.CanUndo = True Then
                    spread.UndoManager.Undo()
                End If
            Catch Exception As Exception
                MessageBox.Show("元に戻す事が出来ません。")
                Exit Sub
            End Try
        End Sub
#End Region

#Region "ボタン（Redo）"
        ''' <summary>
        ''' ボタン（Redo）
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ToolRedo()
            Try
                Dim spread As Spread.FpSpread = GetVisibleSpread

                ' やり直しが出来るか確認します。
                If spread.UndoManager.CanRedo = True Then
                    spread.UndoManager.Redo()
                End If
            Catch Exception As Exception
                MessageBox.Show("やり直しが出来ません。")
                Exit Sub
            End Try
        End Sub
#End Region

#Region "常に非表示列とする列"
        ''' <summary>
        ''' 常に非表示列とする列
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetHiddenCol()
            Dim sheetBase As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim sheetGousya As SheetView = _frmDispTehaiEdit.spdGouSya_Sheet1

            '部課コード列
            sheetBase.Columns(NmSpdTagBase.TAG_SHISAKU_BUKA_CODE).Visible = False
            sheetGousya.Columns(NmSpdTagGousya.TAG_SHISAKU_BUKA_CODE).Visible = False

            '部品番号表示順列
            sheetBase.Columns(NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN).Visible = False
            sheetGousya.Columns(NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN).Visible = False

        End Sub
#End Region

#Region "ボタン（フィルタ解除）"
        ''' <summary>
        ''' フィルタ解除
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ResetFilter()

            Dim startRow As Integer = GetTitleRowsIn(_frmDispTehaiEdit.spdBase_Sheet1)

            '基本情報列タイトル色戻し
            For i As Integer = 0 To _frmDispTehaiEdit.spdBase_Sheet1.ColumnCount - 1
                _frmDispTehaiEdit.spdBase_Sheet1.Cells(0, i, 2, i).ForeColor = Nothing
            Next

            '号車情報列タイトル色戻し
            For i As Integer = 0 To _frmDispTehaiEdit.spdGouSya_Sheet1.ColumnCount - 1
                _frmDispTehaiEdit.spdGouSya_Sheet1.Cells(0, i, 2, i).ForeColor = Nothing
            Next

            '行フィルタ解除
            For i As Integer = startRow To _frmDispTehaiEdit.spdBase_Sheet1.Rows.Count - 1
                _frmDispTehaiEdit.spdGouSya_Sheet1.Rows(i).Visible = True
                _frmDispTehaiEdit.spdGouSya_Sheet1.RowHeader.Rows(i).ForeColor = Nothing
                _frmDispTehaiEdit.spdBase_Sheet1.Rows(i).Visible = True
                _frmDispTehaiEdit.spdBase_Sheet1.RowHeader.Rows(i).ForeColor = Nothing
            Next

            'スプレッドに対してのコピーショートカットキー（Ctrl +X)を無効に（コード上でコピーを処理する為必要な処置)
            'スプレッドに対してのコピーショートカットキー（Ctrl +C)を無効に（コード上でコピーを処理する為必要な処置)
            'スプレッドに対してのコピーショートカットキー（Ctrl +V)を無効に（コード上でコピーを処理する為必要な処置)
            'Dim spreadVisible As FpSpread = GetVisibleSpread
            'Dim imSpread As New FarPoint.Win.Spread.InputMap
            spreadVisible = GetVisibleSpread
            'imSpread = FarPoint.Win.Spread.InputMap
            imSpread.Clear()


        End Sub

#End Region


        Private spreadVisible As FpSpread
        Private imSpread As FarPoint.Win.Spread.InputMap

#Region "ボタン（フィルタ設定処理）"
        ''' <summary>
        ''' フィルタ設定処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetFilter()

            Dim resultRow As Integer() = Nothing
            Dim colNo As Integer

            Dim sheetVisible As SheetView = GetVisibleSheet
            Dim sheetHidden As SheetView = GetHiddenSheet
            Dim startRow As Integer = GetTitleRowsIn(sheetVisible)

            Dim optionFilter As OptionFilter = New OptionFilter(sheetVisible, "ここに名称をセットしても無意味？", startRow)

            'フィルタ対処列取得
            colNo = sheetVisible.ActiveColumn.Index
            optionFilter.ShowInDropDown(colNo, Nothing)

            'フィルタ実行
            resultRow = optionFilter.Filter(colNo)

            '全て取得していればキャンセルとみなす
            If resultRow.Length = sheetVisible.Rows.Count Then
                Exit Sub
            End If

            '列見出しを青にする
            sheetVisible.Cells(0, colNo, 2, colNo).ForeColor = Color.Blue

            '共通列であれば非表示シートも列タイトル色変更
            If colNo <= GetTagIdx(sheetVisible, NmSpdTagBase.TAG_BUHIN_NO_KBN) Then
                sheetHidden.Cells(0, colNo, 1, colNo).ForeColor = Color.Blue
            End If

            For i As Integer = startRow To sheetVisible.Rows.Count - 1

                Dim findFlag As Boolean = False

                'フィルタ条件該当データの中に現在ループ中のスプレッド行が該当するか判定
                For j As Integer = 0 To resultRow.Length - 1
                    If resultRow(j) = i Then
                        findFlag = True
                        Exit For
                    End If
                Next

                If findFlag = False Then
                    sheetVisible.Rows(i).Visible = False
                    sheetVisible.RowHeader.Rows(i).ForeColor = Nothing
                    sheetHidden.Rows(i).Visible = False
                    sheetHidden.RowHeader.Rows(i).ForeColor = Nothing
                Else
                    sheetVisible.RowHeader.Rows(i).ForeColor = Color.Blue
                    sheetHidden.RowHeader.Rows(i).ForeColor = Color.Blue
                End If
            Next

            'スプレッドに対してのコピーショートカットキー（Ctrl +X)を無効に（コード上でコピーを処理する為必要な処置)
            spreadVisible = GetVisibleSpread
            'imSpread = FarPoint.Win.Spread.InputMap
            'Dim spreadVisible As FpSpread = GetVisibleSpread
            'Dim imSpread As New FarPoint.Win.Spread.InputMap
            imSpread = spreadVisible.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imSpread.Put(New FarPoint.Win.Spread.Keystroke(Keys.X, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)
            'スプレッドに対してのコピーショートカットキー（Ctrl +C)を無効に（コード上でコピーを処理する為必要な処置)
            imSpread = spreadVisible.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imSpread.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)
            'スプレッドに対してのコピーショートカットキー（Ctrl +V)を無効に（コード上でコピーを処理する為必要な処置)
            imSpread = spreadVisible.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imSpread.Put(New FarPoint.Win.Spread.Keystroke(Keys.V, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)

        End Sub

#End Region

#Region "ボタン（行高を縮小）"
        ''' <summary>
        ''' ボタン（行高を拡大）
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub RowHeightExp()
            Dim size As Single = _frmDispTehaiEdit.spdBase_Sheet1.Rows(0).Height

            If size >= 2 Then
                size -= 2
                _frmDispTehaiEdit.spdBase_Sheet1.Rows(0).Height = size
                _frmDispTehaiEdit.spdGouSya_Sheet1.Rows(0).Height = size
            End If

        End Sub
#End Region

#Region "ボタン（行高を拡大）"
        ''' <summary>
        ''' ボタン（行高を縮小）
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub RowHeightReduce()

            Dim size As Single = _frmDispTehaiEdit.spdBase_Sheet1.Rows(0).Height

            size += 2
            _frmDispTehaiEdit.spdBase_Sheet1.Rows(0).Height = size
            _frmDispTehaiEdit.spdGouSya_Sheet1.Rows(0).Height = size

        End Sub
#End Region

#End Region

#Region "Sharedメソッド"

#Region "スプレッド編集行対応(保存対象ブロックNoとして記録・対象セルの色等書式設定"
        ''' <summary>
        ''' スプレッド編集行対応
        ''' 
        ''' ・保存対象ブロックNoとして記録
        ''' ・対象セルの色等書式設定
        ''' 
        ''' </summary>
        ''' <param name="isBase">表示スプレッドが基本の場合はTRUEをセットする</param>
        ''' <param name="aRow">行位置</param>
        ''' <param name="aCol">列位置</param>
        ''' <param name="aRowCount">行件数</param>
        ''' <param name="aColCount">列件数</param>
        ''' <remarks></remarks>
        Public Sub SetEditRowProc(ByVal isBase As Boolean, _
                                                    ByVal aRow As Integer, _
                                                    ByVal aCol As Integer, _
                                            Optional ByVal aRowCount As Integer = 1, _
                                            Optional ByVal aColCount As Integer = 1)

            Dim sheet As SheetView = Nothing
            Dim hidSheet As SheetView = Nothing

            If isBase = True Then
                sheet = _frmDispTehaiEdit.spdBase_Sheet1
                hidSheet = _frmDispTehaiEdit.spdGouSya_Sheet1
            Else
                sheet = _frmDispTehaiEdit.spdGouSya_Sheet1
                hidSheet = _frmDispTehaiEdit.spdBase_Sheet1
            End If

            'セル編集モード時にコピーした場合、以下を行う。
            If aRowCount = 0 Then
                aRowCount = 1
            End If

            '行コピー時に以下を行う'
            If aCol = -1 Then
                aCol = 0
            End If


            '編集行のブロックNoを保存対象として記録する
            For i As Integer = 0 To aRowCount - 1
                Dim blockNo As String = sheet.GetText(aRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim
                listEditBlock() = blockNo
            Next

            '編集されたセルは太文字・青文字にする
            sheet.Cells(aRow, aCol, aRow + aRowCount - 1, aCol + aColCount - 1).ForeColor = Color.Blue
            sheet.Cells(aRow, aCol, aRow + aRowCount - 1, aCol + aColCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

            '基本・号車共通列を書式設定
            If aCol <= GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_KBN) Then
                Dim endCol As Integer = aCol + aColCount - 1
                '共通列を超える場合は共通列の最大位置にする
                If endCol > GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_KBN) Then
                    endCol = GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_KBN)
                End If

                '同期対象スプレッドの文字種も変更 
                If hidSheet.Rows.Count >= aRow + aRowCount - 1 Then
                    hidSheet.Cells(aRow, aCol, aRow + aRowCount - 1, endCol).ForeColor = Color.Blue
                    hidSheet.Cells(aRow, aCol, aRow + aRowCount - 1, endCol).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                End If
            End If

        End Sub
#End Region

#Region "上位レベルを検索し親部品番号の行位置を返す"
        ''' <summary>
        ''' 上位レベルを検索し親部品番号の行位置を返す
        ''' 
        ''' 検索不一致、空白、F品番の場合は-1を返す
        ''' 
        ''' </summary>
        ''' <param name="aSheet">対象スプレッド</param>
        ''' <param name="aRow">検索始点行</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetRowParentLevel(ByVal aSheet As FarPoint.Win.Spread.SheetView, ByVal aRow As Integer) As Integer


            Dim rowEnd As Integer = GetTitleRowsIn(aSheet)
            Dim strLevel As String = aSheet.GetText(aRow, GetTagIdx(aSheet, NmSpdTagBase.TAG_LEVEL)).Trim

            '文字種判定
            If IsNumeric(strLevel) = False Then
                Return -1
            End If

            Dim startLevel As Integer = Integer.Parse(strLevel)

            'F品番チェック
            If startLevel = 0 Then
                Return -1
            End If

            Dim startBlockNo As String = aSheet.GetText(aRow, GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim
            Dim resultRow As Integer = -1

            For i As Integer = aRow - 1 To rowEnd Step -1

                Dim chkLevel As String = aSheet.GetText(i, GetTagIdx(aSheet, NmSpdTagBase.TAG_LEVEL)).Trim
                Dim intChkLevel As Integer = -1
                Dim chkBlockNo As String = aSheet.GetText(i, GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim

                If IsNumeric(chkLevel) = False Then
                    Return -1
                End If

                intChkLevel = Integer.Parse(chkLevel)

                'ブロックNoが変わったら抜ける
                If startBlockNo.Equals(chkBlockNo) = False Then
                    Exit For
                End If

                '元レベルより1小さいレベルを探す
                If (startLevel - 1) = intChkLevel Then
                    resultRow = i
                    Exit For
                ElseIf startLevel > (intChkLevel + 1) Then
                    '2以上小さいレベルの出現の場合は-1を返す
                    Return -1
                End If

            Next

            Return resultRow

        End Function
#End Region

#Region "自身の行の部品番号(親)が存在する行を探す"
        ''' <summary>
        ''' 上位レベルを検索し親部品番号の行位置を返す
        ''' 
        ''' 検索不一致、空白、F品番の場合は-1を返す
        ''' 
        ''' </summary>
        ''' <param name="aSheet">対象スプレッド</param>
        ''' <param name="aRow">検索始点行</param>
        ''' <param name="buhinNoOya">親部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetRowBuhinNoOya(ByVal aSheet As FarPoint.Win.Spread.SheetView, ByVal aRow As Integer, ByVal buhinNoOya As String) As Integer

            Dim rowEnd As Integer = GetTitleRowsIn(aSheet)
            Dim strLevel As String = aSheet.GetText(aRow, GetTagIdx(aSheet, NmSpdTagBase.TAG_LEVEL)).Trim

            '文字種判定
            If IsNumeric(strLevel) = False Then
                Return -1
            End If

            Dim startLevel As Integer = Integer.Parse(strLevel)

            'F品番チェック
            If startLevel = 0 Then
                Return -1
            End If

            Dim startBlockNo As String = aSheet.GetText(aRow, GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim
            Dim resultRow As Integer = -1

            For i As Integer = aRow - 1 To rowEnd Step -1

                Dim chkLevel As String = aSheet.GetText(i, GetTagIdx(aSheet, NmSpdTagBase.TAG_LEVEL)).Trim
                Dim intChkLevel As Integer = -1
                Dim chkBlockNo As String = aSheet.GetText(i, GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim

                If IsNumeric(chkLevel) = False Then
                    Return -1
                End If

                intChkLevel = Integer.Parse(chkLevel)

                'ブロックNoが変わったら抜ける
                If startBlockNo.Equals(chkBlockNo) = False Then
                    Exit For
                End If

                '元レベルより1小さいレベルを探す
                If (startLevel - 1) = intChkLevel Then
                    '部品番号(親)に該当するかチェック'
                    If StringUtil.Equals(aSheet.GetText(i, GetTagIdx(aSheet, NmSpdTagBase.TAG_BUHIN_NO)).Trim, buhinNoOya.Trim) Then
                        resultRow = i
                        Exit For
                    End If
                ElseIf startLevel > (intChkLevel + 1) Then
                    '2以上小さいレベルの出現の場合は-1を返す
                    Return -1
                End If

            Next

            Return resultRow

        End Function


#End Region

#Region "自給品消しこみなどで親が存在しない場合、仮の親を探す"

        ''' <summary>
        ''' 自給品消しこみなどで親が存在しない場合、仮の親を探す
        ''' 
        ''' 検索不一致、空白、F品番の場合は-1を返す
        ''' 
        ''' </summary>
        ''' <param name="aSheet">対象スプレッド</param>
        ''' <param name="aRow">検索始点行</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetRowBuhinNoOyaKari(ByVal aSheet As FarPoint.Win.Spread.SheetView, ByVal aRow As Integer) As Integer


            Dim rowEnd As Integer = GetTitleRowsIn(aSheet)
            '自身のレベル取得'
            Dim strLevel As String = aSheet.GetText(aRow, GetTagIdx(aSheet, NmSpdTagBase.TAG_LEVEL)).Trim

            '文字種判定
            If IsNumeric(strLevel) = False Then
                Return -1
            End If

            Dim startLevel As Integer = Integer.Parse(strLevel)

            'F品番チェック
            If startLevel = 0 Then
                Return -1
            End If
            '自身のブロックNo取得'
            Dim startBlockNo As String = aSheet.GetText(aRow, GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim

            Dim resultRow As Integer = -1

            '自身の行-1スタートで上の行を見ていく'
            For i As Integer = aRow - 1 To rowEnd Step -1

                Dim chkLevel As String = aSheet.GetText(i, GetTagIdx(aSheet, NmSpdTagBase.TAG_LEVEL)).Trim
                Dim intChkLevel As Integer = -1
                Dim chkBlockNo As String = aSheet.GetText(i, GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim

                If IsNumeric(chkLevel) = False Then
                    Return -1
                End If

                intChkLevel = Integer.Parse(chkLevel)

                'ブロックNoが変わったら抜ける
                If startBlockNo.Equals(chkBlockNo) = False Then
                    Exit For
                End If

                '元レベルより小さいレベルを探す
                If startLevel > intChkLevel Then
                    resultRow = i
                    Exit For
                End If

            Next

            Return resultRow

        End Function

#End Region

#Region "Spreadのシートにタイトルをもつ場合のタイトル行数を返す"
        ''' <summary>
        '''Spreadのシートにタイトルをもつ場合のタイトル行数を返す
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <returns>タイトル行数</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTitleRowsIn(ByVal spreadSheet As FarPoint.Win.Spread.SheetView) As Integer
            If 1 < spreadSheet.StartingRowNumber Then
                Throw New InvalidOperationException("想定外の値です. sheet.StartingRowNumber=" & CStr(spreadSheet.StartingRowNumber))
            End If
            Return 1 - spreadSheet.StartingRowNumber
        End Function
#End Region

#Region " 列インデックス取得 "
        ''' <summary>
        ''' 列タグを元に列インデックスを取得します.
        ''' </summary>
        ''' <param name="sheet">対象シート</param>
        ''' <param name="tag">列タグ</param>
        ''' <returns>列インデックス</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTagIdx(ByVal sheet As Spread.SheetView, ByVal tag As String) As Integer

            Dim col As Spread.Column = sheet.Columns(tag)

            If col Is Nothing Then
                Return -1
            End If

            Return col.Index
        End Function

#End Region

#Region "対象シートの全範囲消去"
        ''' <summary>
        ''' 対象シートの全範囲消去
        ''' </summary>
        ''' <param name="sheet"></param>
        ''' <remarks></remarks>
        Public Shared Sub SpreadAllClear(ByVal sheet As FarPoint.Win.Spread.SheetView)
            Dim row As Integer = sheet.RowCount
            Dim col As Integer = sheet.Columns.Count

            'タイトルの次行からが開始位置
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            If sheet Is Nothing Then
                Throw New Exception("消去指示されたスプレッドシートはNothingが格納されている")
                Return
            End If

            sheet.ClearRange(startRow, 0, row, col, False)

        End Sub
#End Region

#Region "数値8桁で格納された日付をYYYY/MM/DD形式に変換"
        ''' <summary>
        ''' 数値8桁で格納された日付をYYYY/MM/DD形式に変換
        ''' 
        '''  ※　8桁未満数値の場合はString.Emptyを返す
        '''  ※  8桁で変換不可の場合はThrow 
        ''' 
        ''' </summary>
        ''' <param name="aIntDate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConvDateInt8(ByVal aIntDate As Integer) As String
            Dim strDate As String = aIntDate.ToString.Trim

            '変換対象外判定(99999999はとりあえず除外)
            If strDate.Equals(String.Empty) OrElse strDate.Equals("0") OrElse strDate.Equals("99999999") OrElse strDate.Length <= 7 Then
                Return String.Empty
            End If

            strDate = String.Format("{0}/{1}/{2}", strDate.Substring(0, 4), strDate.Substring(4, 2), strDate.Substring(6, 2))

            If IsDate(strDate) = False Then
                Throw New Exception(String.Format("日付型に変換できない8桁数値が見つかりました。管理者に連絡してください。(対象数値:{0})", aIntDate.ToString))
            End If

            Return strDate
        End Function
#End Region

#Region "文字型(YYYY/MM/DD)をInt8桁に変換 "
        ''' <summary>
        ''' 文字型(YYYY/MM/DD)をInt8桁に変換
        ''' </summary>
        ''' <param name="aStrDate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConvInt8Date(ByVal aStrDate As String) As Integer
            Dim wDate As Date = Nothing
            Dim intDate As Integer = Nothing

            If IsDate(aStrDate) Then
                wDate = Date.Parse(aStrDate)
                intDate = wDate.ToString("yyyyMMdd")
            Else
                intDate = 0
            End If

            Return intDate
        End Function
#End Region

#Region "スプレッドにTAGでアクセスする場合の号車列TAG名称を生成"
        ''' <summary>
        ''' スプレッドにTAGでアクセスする場合の号車列TAG名称を生成
        '''  
        ''' </summary>
        ''' <param name="colNo">何番目の号車名称かをセット</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetColNameShisakuGousya(ByVal colNo As Integer) As String

            Return TehaichoEditNames.PREFIX_GOUSHA_TAG & String.Format("{0:#00}", colNo)

        End Function

#End Region

#Region "文字列を数値に変換"
        ''' <summary>
        ''' 文字列を数値に変換
        ''' 
        ''' ※変換不能時は0を返す
        ''' 
        ''' </summary>
        ''' <param name="aStrValue"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CnvIntStr(ByVal aStrValue As String) As Integer

            If IsNumeric(aStrValue.Trim) = False Then
                Return 0
            Else
                Return Integer.Parse(aStrValue)
            End If

        End Function
#End Region

#Region "テーブル取得後の数値型項目の検査"
        ''' <summary>
        ''' テーブル取得後の数値型項目の検査
        ''' 
        ''' 0または0.00の場合は空をセットする
        ''' 
        ''' </summary>
        ''' <param name="aObject"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetNumericDbField(ByVal aObject As Object) As String
            Dim workString As String = String.Empty

            If IsDBNull(aObject) = True Then
                workString = String.Empty
            ElseIf aObject.ToString.Trim.Equals("0") Then
                workString = String.Empty
            ElseIf aObject.ToString.Trim.Equals("0.00") Then
                workString = String.Empty
            Else
                workString = aObject.ToString
            End If

            Return workString

        End Function

#End Region

#Region "ブロックNo形式の判定"
        ''' <summary>
        ''' ブロックNo形式の判定
        ''' </summary>
        ''' <param name="aBlockNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function IsBlockNo(ByVal aBlockNo As String) As Boolean

            Dim strSearchPatn As String = "[0-9][0-9][0-9][a-zA-Z]"

            If Regex.IsMatch(aBlockNo, strSearchPatn) Then
                Return True
            Else
                Return False
            End If

        End Function

#End Region

#Region "LenBメソッド"
        ''' <summary>
        '''     指定された文字列のバイト数を返します。</summary>
        ''' <param name="stTarget">
        '''     バイト数取得の対象となる文字列。</param>
        ''' <returns>
        '''     半角 1 バイト、全角 2 バイトでカウントされたバイト数。</returns>

        Public Shared Function LenB(ByVal stTarget As String) As Integer
            Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(stTarget)
        End Function

#End Region

#Region "スプレッドシートのコピー"
        ''' <summary>
        ''' スプレッドシートのコピー
        ''' </summary>
        ''' <param name="sheet"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CopySheet(ByVal sheet As FarPoint.Win.Spread.SheetView) As FarPoint.Win.Spread.SheetView

            Dim newSheet As FarPoint.Win.Spread.SheetView = Nothing

            If Not IsNothing(sheet) Then

                newSheet = FarPoint.Win.Serializer.LoadObjectXml(GetType(FarPoint.Win.Spread.SheetView), FarPoint.Win.Serializer.GetObjectXml(sheet, "CopySheet"), "CopySheet")

            End If

            Return newSheet

        End Function

#End Region

#Region "入力文字数チェック"
        ''' <summary>
        ''' シートのアクティブセルの文字数をチェック
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Overloads Function CheckCellLength(ByVal aSheet As FarPoint.Win.Spread.SheetView) As Boolean

            Dim actRow As Integer = aSheet.ActiveRowIndex
            Dim actCol As Integer = aSheet.ActiveColumnIndex
            Dim tCell As FarPoint.Win.Spread.CellType.TextCellType = aSheet.GetCellType(actRow, actCol)
            Dim errCell As Spread.Cell = Nothing

            'テキスト型以外は対象外
            If Not TypeOf (aSheet.GetCellType(actRow, actCol)) Is FarPoint.Win.Spread.CellType.TextCellType Then
                Return True
            End If

            Dim cellValue As String = aSheet.GetText(actRow, actCol)

            '文字数制限チェック
            If TehaichoEditLogic.LenB(cellValue.ToString) > tCell.MaxLength Then
                aSheet.Cells(actRow, actCol).BackColor = Color.Red
                Return False
            Else
                aSheet.Cells(actRow, actCol).BackColor = Nothing
            End If

            Return True
        End Function
        ''' <summary>
        ''' 指定されたセルの文字数をチェック
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Overloads Function CheckCellLength(ByVal aSheet As FarPoint.Win.Spread.SheetView, ByVal aRow As Integer, ByVal aCol As Integer) As Boolean

            'テキスト型以外は対象外
            If Not TypeOf (aSheet.GetCellType(aRow, aCol)) Is FarPoint.Win.Spread.CellType.TextCellType Then
                Return True
            End If

            Dim tCell As FarPoint.Win.Spread.CellType.TextCellType = aSheet.GetCellType(aRow, aCol)
            Dim errCell As Spread.Cell = Nothing

            Dim cellValue As String = aSheet.GetText(aRow, aCol)

            '文字数制限チェック
            If TehaichoEditLogic.LenB(cellValue.ToString) > tCell.MaxLength Then
                aSheet.Cells(aRow, aCol).BackColor = Color.Red
                Return False
            End If

            Return True
        End Function


#End Region

#Region "クリップボードの内容をstring()型のリストに格納し返す"
        Public Shared Function GetClipbordList() As List(Of String())
            Dim listStr As New List(Of String())

            'システムクリップボードにあるデータを取得します
            Dim iData As IDataObject = Clipboard.GetDataObject()

            Dim strRow() As String

            'テキスト形式データの判断
            If iData.GetDataPresent(DataFormats.Text) = False Then
                Return Nothing
            Else

                Console.WriteLine(CType(iData.GetData(DataFormats.Text), String))
                strRow = CType(iData.GetData(DataFormats.Text), String).Split(vbCrLf)

            End If

            For i As Integer = 0 To strRow.Length - 1
                Dim strChar() As String = strRow(i).Split(vbTab)
                listStr.Add(strChar)
            Next

            Return listStr

        End Function

#End Region

#Region "保存対象書式有無のセル配列を返す"
        ''' <summary>
        ''' 保存対象書式有無のセル配列を返す
        ''' </summary>
        ''' <param name="aSheet">対象シートをセットする</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetEditCellInfo(ByVal aSheet As SheetView) As List(Of Boolean())

            Dim selection As FarPoint.Win.Spread.Model.CellRange = aSheet.GetSelection(0)
            Dim listBln As New List(Of Boolean())

            '行見出し選択から来た場合は処理を戻す
            If selection.ColumnCount < 0 Then
                Return listBln
            End If

            'コピーだけはコードで行う（Undoも関係ない）
            For i As Integer = 0 To selection.RowCount - 1

                Dim blnTbl() As Boolean = Nothing
                ReDim Preserve blnTbl(selection.ColumnCount - 1)

                For j As Integer = 0 To selection.ColumnCount - 1
                    Dim objFont As System.Drawing.Font = aSheet.Cells(selection.Row + i, selection.Column + j).Font

                    If Not objFont Is Nothing AndAlso objFont.Bold = True Then
                        blnTbl(j) = True
                    Else
                        blnTbl(j) = False
                    End If

                Next
                listBln.Add(blnTbl)
            Next

            Return listBln

        End Function

#End Region

#Region "コピーの時に一時的に書式を設定する、また設定した書式を元に戻す"
        ''' <summary>
        ''' コピーの時に一時的に書式を設定する、また設定した書式を元に戻す
        ''' </summary>
        ''' <param name="aSheet">対象シート</param>
        ''' <param name="alistBln">書式を全て保存対象書式にするときは指定しない</param>
        ''' <remarks></remarks>
        Public Shared Sub SetUndoCellFormat(ByVal aSheet As SheetView, Optional ByVal alistBln As List(Of Boolean()) = Nothing)

            Dim selection As FarPoint.Win.Spread.Model.CellRange = aSheet.GetSelection(0)
            '行選択での編集操作は戻す
            If selection.Column < 0 Then
                Return
            End If

            '無い場合は全て保存対象編集書式とするため全てTrueをセット
            If alistBln Is Nothing Then
                alistBln = New List(Of Boolean())

                For i As Integer = 0 To selection.RowCount - 1

                    Dim blnTbl() As Boolean = Nothing
                    ReDim Preserve blnTbl(selection.ColumnCount - 1)

                    For j As Integer = 0 To selection.ColumnCount - 1
                        blnTbl(j) = True
                    Next
                    alistBln.Add(blnTbl)
                Next

            End If

            '受け取ったListの内容で書式を設定
            For i As Integer = 0 To selection.RowCount - 1
                For j As Integer = 0 To selection.ColumnCount - 1

                    If alistBln(i)(j) = False Then
                        aSheet.Cells(selection.Row + i, selection.Column + j).ForeColor = Nothing
                        aSheet.Cells(selection.Row + i, selection.Column + j).Font = Nothing
                    Else
                        aSheet.Cells(selection.Row + i, selection.Column + j).ForeColor = Color.Blue
                        aSheet.Cells(selection.Row + i, selection.Column + j).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                    End If

                Next
            Next

        End Sub
#End Region
#End Region

#Region "内部クラス"

#Region "セル情報格納"
        Public Class CellInfo
            Private _startCol As Integer = -1
            Private _startRow As Integer = -1
            Private _colCount As Integer = -1
            Private _rowCount As Integer = -1
            Private _cells As Spread.Cells = Nothing

#Region "コンストラクタ"
            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="aStartRow">開始行</param>
            ''' <param name="aStartCol">開始列列</param>
            ''' <param name="aCells">対象Cellsオブジェクト</param>
            ''' <remarks></remarks>
            Public Sub New(ByVal aStartRow As Integer, ByVal aStartCol As Integer, _
                                 ByVal aRowCount As Integer, ByVal aColCount As Integer, ByVal aCells As Spread.Cells)

                _startRow = aStartRow
                _startCol = aStartCol
                _rowCount = aRowCount
                _colCount = aColCount
                _cells = aCells

            End Sub
#End Region

#Region "プロパティ"
            Public ReadOnly Property getRowCount() As Integer
                Get
                    Return 1
                End Get
            End Property
#End Region
        End Class
#End Region

#Region "セルコピペUNDO情報格納クラス"
        Public Class CellEditManage
            '直前編集情報
            Private _editInfo As CellInfo
            'Undo情報格納
            Private _undoInfo As CellInfo
            'Redo情報格納
            Private _redoInfo As CellInfo

            Public Enum EDIT_TYPE
                CUT = 0
                COPY
                PASTE
                UNDO
                REDO
            End Enum

            Public Sub New()

            End Sub

#Region "切取情報格納"
            ''' <summary>
            ''' 切取情報格納
            ''' </summary>
            ''' <param name="aCellRange">対象範囲をCellRange型でセット</param>
            ''' <param name="aSheet">対象シートをセット</param>
            ''' <remarks></remarks>
            Public Sub EditCut(ByVal aCellRange As Model.CellRange, ByVal aSheet As SheetView)
                Dim startRow As Integer = aCellRange.Row
                Dim startCol As Integer = aCellRange.Column
                Dim countRow As Integer = aCellRange.RowCount
                Dim countCol As Integer = aCellRange.ColumnCount
                Dim cells As Spread.Cell = aSheet.Cells(startRow, startCol, startRow + countRow - 1, startCol + countCol - 1)

            End Sub
#End Region
        End Class
#End Region

#Region "ブロックNoコンボボックス制御クラス"
        ''' <summary>
        '''ブロックNoコンボボックス制御クラス
        ''' </summary>
        ''' <remarks></remarks>
        Public Class ComboBoxItem
            Private _id As String = ""
            Private _name As String = ""

            'コンストラクタ
            Public Sub New(ByVal id As String, ByVal name As String)
                _id = id
                _name = name
            End Sub

            '実際の値
            Public ReadOnly Property Id() As String
                Get
                    Return _id
                End Get
            End Property

            '表示名称
            '（このプロパティはこのサンプルでは使わないのでなくても良い）
            Public ReadOnly Property Name() As String
                Get
                    Return _name
                End Get
            End Property

            'オーバーライドしたメソッド
            'これがコンボボックスに表示される
            Public Overrides Function ToString() As String
                Return _name
            End Function

        End Class

#End Region

#End Region


#Region "文字列のエラーチェック"
        '↓↓2014/12/30 メタル項目を追加 TES)張 ADD BEGIN
        '''' <summary>
        '''' 文字列のエラーチェック
        '''' </summary>
        '''' <param name="str">キャメル記法の文字列</param>
        '''' <returns>半角英数および-,#,@以外の文字が含まれていた場合False</returns>
        '''' <remarks></remarks>
        'Private Function StrErrCheck(ByVal str As String) As Boolean
        '    If str = String.Empty Then
        '        Return False
        '    End If
        '    '一文字ずつチェックする'
        '    Dim chars() As Char = str.ToCharArray
        '    Dim result As Boolean = True
        '    For i As Integer = 0 To chars.Length - 1
        '        If "A" <= chars(i) And chars(i) <= "Z" Then
        '            result = True
        '        ElseIf "a" <= chars(i) And chars(i) <= "z" Then
        '            result = True
        '        ElseIf "0" <= chars(i) And chars(i) <= "9" Then
        '            result = True
        '        ElseIf chars(i) = "-" OrElse chars(i) = "#" OrElse chars(i) = "@" OrElse chars(i) = "." OrElse chars(i) = "_" OrElse chars(i) = " " Then
        '            result = True
        '        Else
        '            Return False
        '        End If
        '    Next
        '    Return result
        'End Function
        '↑↑2014/12/30 メタル項目を追加 TES)張 ADD BEGIN
#End Region

        '↓↓↓2015/01/08 ツールバーの材質表示/非表示を追加 TES)張 ADD BEGIN
        ''' <summary>
        ''' 材質を表示する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ZaishituColumnVisible()
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAISHITU_MEKKI).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_MATERIAL_INFO_LENGTH).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_MATERIAL_INFO_WIDTH).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_DATA_ITEM_SET_NAME).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_DATA_ITEM_KAITEI_INFO).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION).Index, True)
            '
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAIRYO_SUNPO_X).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ).Index, True)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ).Index, True)
        End Sub

        ''' <summary>
        ''' 材質を非表示にする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ZaishituColumnDisable()
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAISHITU_MEKKI).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_MATERIAL_INFO_LENGTH).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_MATERIAL_INFO_WIDTH).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_DATA_ITEM_SET_NAME).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_DATA_ITEM_KAITEI_INFO).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION).Index, False)
            '
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAIRYO_SUNPO_X).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ).Index, False)
            sheet.SetColumnVisible(sheet.Columns(NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ).Index, False)
        End Sub
        '↑↑↑2015/01/08 ツールバーの材質表示/非表示を追加 TES)張 ADD END

        ''↓↓2015/01/14 データ支給依頼書をを追加 TES)劉 ADD BEGIN
        Public Function GetDataProvisionOutputInfo() As List(Of TShisakuTehaiKihonVo)
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            Dim _ResultList As New List(Of TShisakuTehaiKihonVo)
            For i As Integer = startRow To sheet.RowCount - 1
                Dim AreaName As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME)).Trim
                Dim SetName As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME)).Trim
                Dim DataProvision As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION)).Trim
                Dim addVo As New TShisakuTehaiKihonVo
                'If StringUtil.IsNotEmpty(AreaName) AndAlso StringUtil.IsNotEmpty(SetName) AndAlso DataProvision.Equals("False") Then
                If StringUtil.IsNotEmpty(AreaName) AndAlso StrConv(AreaName, VbStrConv.Narrow) <> "-" AndAlso DataProvision.Equals("False") AndAlso _
                   sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_HENKATEN)).Trim <> "削" Then

                    'ブロック
                    addVo.ShisakuBlockNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim
                    '行ＩＤ
                    addVo.GyouId = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID)).Trim
                    'レベル
                    addVo.Level = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_LEVEL)).Trim
                    '部品番号
                    addVo.BuhinNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO)).Trim
                    '部品名称
                    addVo.BuhinName = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NAME)).Trim
                    '集計コード
                    addVo.ShukeiCode = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUKEI_CODE)).Trim
                    '手配記号
                    addVo.TehaiKigou = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_TEHAI_KIGOU)).Trim
                    '購坦
                    addVo.Koutan = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_KOUTAN)).Trim
                    '取引先コード
                    addVo.TorihikisakiCode = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_TORIHIKISAKI_CODE)).Trim
                    '納場
                    addVo.Nouba = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUBA)).Trim
                    '供給セクション
                    addVo.KyoukuSection = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_KYOUKU_SECTION)).Trim
                    '納入指示日
                    addVo.NounyuShijibi = ConvInt8Date(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)).Trim)
                    '合計員数
                    addVo.TotalInsuSuryo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_TOTAL_INSU_SURYO)).Trim
                    '出図予定日
                    addVo.ShutuzuYoteiDate = ConvInt8Date(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE)).Trim)

                    '出図実績_日付
                    addVo.ShutuzuJisekiDate = ConvInt8Date(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_DATE)).Trim)
                    '出図実績_改訂№
                    addVo.ShutuzuJisekiKaiteiNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO)).Trim
                    '出図実績_設通№
                    addVo.ShutuzuJisekiStsrDhstba = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)).Trim
                    '最終織込設変情報_日付
                    addVo.SaisyuSetsuhenDate = ConvInt8Date(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_DATE)).Trim)
                    '最終織込設変情報_改訂№
                    addVo.SaisyuSetsuhenKaiteiNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO)).Trim
                    '最終織込設変情報_設通№
                    addVo.StsrDhstba = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_STSR_DHSTBA)).Trim

                    '材料寸法_X(mm)
                    Dim ZairyoSunpoX As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X)).Trim
                    If IsNumeric(ZairyoSunpoX) Then
                        addVo.ZairyoSunpoX = CDec(ZairyoSunpoX)
                    End If
                    '材料寸法_Y(mm)
                    Dim ZairyoSunpoY As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y)).Trim
                    If IsNumeric(ZairyoSunpoY) Then
                        addVo.ZairyoSunpoY = CDec(ZairyoSunpoY)
                    End If
                    '材料寸法_Z(mm)
                    Dim ZairyoSunpoZ As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z)).Trim
                    If IsNumeric(ZairyoSunpoZ) Then
                        addVo.ZairyoSunpoZ = CDec(ZairyoSunpoZ)
                    End If
                    '材料寸法_X+Y(mm)
                    Dim ZairyoSunpoXy As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY)).Trim
                    If IsNumeric(ZairyoSunpoXy) Then
                        addVo.ZairyoSunpoXy = CDec(ZairyoSunpoXy)
                    End If
                    '材料寸法_X+Z(mm)
                    Dim ZairyoSunpoXz As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ)).Trim
                    If IsNumeric(ZairyoSunpoXz) Then
                        addVo.ZairyoSunpoXz = CDec(ZairyoSunpoXz)
                    End If
                    '材料寸法_Y+Z(mm)
                    Dim ZairyoSunpoYz As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ)).Trim
                    If IsNumeric(ZairyoSunpoYz) Then
                        addVo.ZairyoSunpoYz = CDec(ZairyoSunpoYz)
                    End If

                    '材質・規格１
                    addVo.ZaishituKikaku1 = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1)).Trim
                    '材質・規格２
                    addVo.ZaishituKikaku2 = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2)).Trim
                    '材質・規格３
                    addVo.ZaishituKikaku3 = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3)).Trim
                    '材質・メッキ
                    addVo.ZaishituMekki = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_MEKKI)).Trim
                    '板厚
                    addVo.ShisakuBankoSuryo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO)).Trim
                    '板厚・ｕ
                    addVo.ShisakuBankoSuryoU = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U)).Trim
                    '材料情報・製品長
                    Dim materialinfoLength As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_LENGTH)).Trim
                    If IsNumeric(materialinfoLength) Then
                        addVo.MaterialInfoLength = CInt(materialinfoLength)
                    End If
                    '材料情報・製品幅
                    Dim materialinfoWidth As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_WIDTH)).Trim
                    If IsNumeric(materialinfoWidth) Then
                        addVo.MaterialInfoWidth = CInt(materialinfoWidth)
                    End If
                    '材料情報・発注対象
                    addVo.MaterialInfoOrderTarget = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET)).Trim
                    '材料情報・発注済
                    addVo.MaterialInfoOrderChk = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)).Trim
                    'データ項目・改訂№
                    addVo.DataItemKaiteiNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO)).Trim
                    'データ項目・エリア名
                    addVo.DataItemAreaName = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME)).Trim
                    'データ項目・セット名
                    addVo.DataItemSetName = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME)).Trim
                    'データ項目・改訂情報
                    addVo.DataItemKaiteiInfo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_INFO)).Trim
                    'データ項目・データ支給チェック欄
                    addVo.DataItemDataProvision = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION)).Trim
                    '取引先名称
                    addVo.MakerCode = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_MAKER_CODE)).Trim
                    '備考
                    addVo.Bikou = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_BIKOU)).Trim

                    _ResultList.Add(addVo)
                End If
            Next

            Return _ResultList
        End Function

        ''' <summary>
        ''' 「データ項目・データ支給チェック欄」、「データ項目・データ支給チェック欄最終更新年月日」を更新
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function UpdateDataProvision(ByVal selectedVos As List(Of TShisakuTehaiKihonVo)) As Boolean
            Try
                Dim watch As New Stopwatch()
                Dim tehaichoDao As TShisakuTehaiKihonDao = New TShisakuTehaiKihonDaoImpl
                Dim tehaichoVo As TShisakuTehaiKihonVo = Nothing
                Dim aDate As New ShisakuDate

                Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
                Dim startRow As Integer = GetTitleRowsIn(sheet)

                Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                    db.Open()
                    '－トランザクション開始－
                    db.BeginTransaction()

                    watch.Start()

                    For selIndex As Integer = 0 To selectedVos.Count - 1
                        For rowIndex As Integer = startRow To sheet.RowCount - 1
                            '同一行の場合
                            If String.Equals(sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)), selectedVos(selIndex).ShisakuBlockNo) And _
                               String.Equals(sheet.GetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID)), selectedVos(selIndex).GyouId) Then
                                tehaichoVo = tehaichoDao.FindByPk(_headerSubject.shisakuEventCode, _
                                                                  _headerSubject.shisakuListCode, _
                                                                  _headerSubject.shisakuListCodeKaiteiNo, _
                                                                  sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)), _
                                                                  sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)), _
                                                                  CInt(sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN))))
                                tehaichoVo.DataItemDataProvision = "1"
                                tehaichoVo.DataItemDataProvisionDate = aDate.CurrentDateDbFormat
                                tehaichoVo.UpdatedUserId = LoginInfo.Now.UserId
                                tehaichoVo.UpdatedDate = aDate.CurrentDateDbFormat
                                tehaichoVo.UpdatedTime = aDate.CurrentTimeDbFormat
                                Dim resCnt As Integer = tehaichoDao.UpdateByPk(tehaichoVo)

                                If resCnt <> 1 Then
                                    Throw New Exception()
                                End If
                                'データ支給チェック欄更新
                                sheet.SetValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION), True)
                                sheet.SetNote(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION), CDate(aDate.CurrentDateDbFormat).ToString("yyyy-MM-dd"))
                                'sheet.SetNote(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION), CDate(aDate.CurrentDateDbFormat).ToString("MM/dd"))
                            End If
                        Next
                    Next

                    watch.Stop()
                    Console.WriteLine(String.Format("データ項目・データ支給チェック欄の更新-実行時間 : {0}ms", watch.ElapsedMilliseconds))
                    watch.Reset()

                    watch.Start()
                    db.Commit()
                    watch.Stop()
                    Console.WriteLine(String.Format("コミット-実行時間 : {0}ms", watch.ElapsedMilliseconds))
                    watch.Reset()
                End Using

                Return True
            Catch ex As Exception
                ComFunc.ShowErrMsgBox("データ項目・データ支給チェック欄の更新中にエラーが発生しました。")
                Return False
            End Try
        End Function
        ''↑↑2015/01/14 データ支給依頼書を追加 TES)劉 ADD END

        ''↓↓↓2015/01/20 工事指令書を追加 TES)張 ADD BEGIN
        Public Function GetKoujiShireiOutputInfo() As List(Of TShisakuTehaiKihonVo)
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            Dim _ResultList As New List(Of TShisakuTehaiKihonVo)
            For i As Integer = startRow To sheet.RowCount - 1
                'Dim makerCode As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_TORIHIKISAKI_CODE)).Trim

                If StringUtil.IsEmpty(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim) Or _
                   StringUtil.IsEmpty(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_LEVEL)).Trim) Then
                    Exit For
                End If

                Dim addVo As New TShisakuTehaiKihonVo

                '変化点が削のデータは除く
                'If StringUtil.IsNotEmpty(makerCode) Then
                If sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_HENKATEN)).Trim <> "削" Then

                    'ブロック
                    addVo.ShisakuBlockNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim
                    '行ＩＤ
                    addVo.GyouId = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID)).Trim
                    'レベル
                    addVo.Level = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_LEVEL)).Trim
                    '部品番号
                    addVo.BuhinNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO)).Trim
                    '部品名称
                    addVo.BuhinName = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NAME)).Trim
                    '集計コード
                    addVo.ShukeiCode = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUKEI_CODE)).Trim
                    '手配記号
                    addVo.TehaiKigou = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_TEHAI_KIGOU)).Trim
                    '購坦
                    addVo.Koutan = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_KOUTAN)).Trim
                    '取引先コード
                    addVo.TorihikisakiCode = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_TORIHIKISAKI_CODE)).Trim
                    '納場
                    addVo.Nouba = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUBA)).Trim
                    '供給セクション
                    addVo.KyoukuSection = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_KYOUKU_SECTION)).Trim
                    '納入指示日
                    addVo.NounyuShijibi = ConvInt8Date(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)).Trim)
                    '合計員数
                    addVo.TotalInsuSuryo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_TOTAL_INSU_SURYO)).Trim
                    '出図予定日
                    addVo.ShutuzuYoteiDate = ConvInt8Date(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE)).Trim)

                    '出図実績_日付
                    addVo.ShutuzuJisekiDate = ConvInt8Date(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_DATE)).Trim)
                    '出図実績_改訂№
                    addVo.ShutuzuJisekiKaiteiNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO)).Trim
                    '出図実績_設通№
                    addVo.ShutuzuJisekiStsrDhstba = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)).Trim

                    '最終織込設変情報_日付
                    addVo.SaisyuSetsuhenDate = ConvInt8Date(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_DATE)).Trim)
                    '最終織込設変情報_改訂№
                    addVo.SaisyuSetsuhenKaiteiNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO)).Trim
                    '最終織込設変情報_設通№
                    addVo.StsrDhstba = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_STSR_DHSTBA)).Trim

                    '材料寸法_X(mm)
                    Dim ZairyoSunpoX As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X)).Trim
                    If IsNumeric(ZairyoSunpoX) Then
                        addVo.ZairyoSunpoZ = CDec(ZairyoSunpoX)
                    End If
                    '材料寸法_Y(mm)
                    Dim ZairyoSunpoY As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y)).Trim
                    If IsNumeric(ZairyoSunpoY) Then
                        addVo.ZairyoSunpoY = CDec(ZairyoSunpoY)
                    End If
                    '材料寸法_Z(mm)
                    Dim ZairyoSunpoZ As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z)).Trim
                    If IsNumeric(ZairyoSunpoZ) Then
                        addVo.ZairyoSunpoZ = CDec(ZairyoSunpoZ)
                    End If
                    '材料寸法_X+Y(mm)
                    Dim ZairyoSunpoXy As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY)).Trim
                    If IsNumeric(ZairyoSunpoXy) Then
                        addVo.ZairyoSunpoXy = CDec(ZairyoSunpoXy)
                    End If
                    '材料寸法_X+Z(mm)
                    Dim ZairyoSunpoXz As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ)).Trim
                    If IsNumeric(ZairyoSunpoXz) Then
                        addVo.ZairyoSunpoXz = CDec(ZairyoSunpoXz)
                    End If
                    '材料寸法_Y+Z(mm)
                    Dim ZairyoSunpoYz As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ)).Trim
                    If IsNumeric(ZairyoSunpoYz) Then
                        addVo.ZairyoSunpoYz = CDec(ZairyoSunpoYz)
                    End If

                    '材質・規格１
                    addVo.ZaishituKikaku1 = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1)).Trim
                    '材質・規格２
                    addVo.ZaishituKikaku2 = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2)).Trim
                    '材質・規格３
                    addVo.ZaishituKikaku3 = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3)).Trim
                    '材質・メッキ
                    addVo.ZaishituMekki = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_MEKKI)).Trim
                    '板厚
                    addVo.ShisakuBankoSuryo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO)).Trim
                    '板厚・ｕ
                    addVo.ShisakuBankoSuryoU = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U)).Trim
                    '材料情報・製品長
                    Dim materialinfoLength As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_LENGTH)).Trim
                    If IsNumeric(materialinfoLength) Then
                        addVo.MaterialInfoLength = CInt(materialinfoLength)
                    End If
                    '材料情報・製品幅
                    Dim materialinfoWidth As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_WIDTH)).Trim
                    If IsNumeric(materialinfoWidth) Then
                        addVo.MaterialInfoWidth = CInt(materialinfoWidth)
                    End If
                    '材料情報・発注対象
                    addVo.MaterialInfoOrderTarget = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET)).Trim
                    '材料情報・発注済
                    addVo.MaterialInfoOrderChk = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)).Trim
                    'データ項目・改訂№
                    addVo.DataItemKaiteiNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO)).Trim
                    'データ項目・エリア名
                    addVo.DataItemAreaName = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME)).Trim
                    'データ項目・セット名
                    addVo.DataItemSetName = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME)).Trim
                    'データ項目・改訂情報
                    addVo.DataItemKaiteiInfo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_INFO)).Trim
                    'データ項目・データ支給チェック欄
                    addVo.DataItemDataProvision = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION)).Trim
                    '取引先名称
                    addVo.MakerCode = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_MAKER_CODE)).Trim
                    '備考
                    addVo.Bikou = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_BIKOU)).Trim

                    _ResultList.Add(addVo)

                End If

            Next

            Return _ResultList
        End Function
        ''↑↑↑2015/01/20 工事指令書を追加 TES)張 ADD END

        ''↓↓↓2015/01/20 注文書を追加 TES)張 ADD BEGIN
        Public Function GetOrderOutputInfo() As List(Of TShisakuTehaiKihonVo)
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            Dim _ResultList As New List(Of TShisakuTehaiKihonVo)
            For i As Integer = startRow To sheet.RowCount - 1
                If StringUtil.IsEmpty(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim) Or _
                   StringUtil.IsEmpty(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_LEVEL)).Trim) Then
                    Exit For
                End If
                Dim addVo As New TShisakuTehaiKihonVo
                '変化点が削のデータは除く
                If sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_HENKATEN)).Trim <> "削" Then

                    'ブロック
                    addVo.ShisakuBlockNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim
                    '行ＩＤ
                    addVo.GyouId = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID)).Trim
                    'レベル
                    addVo.Level = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_LEVEL)).Trim
                    '部品番号
                    addVo.BuhinNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO)).Trim
                    '部品名称
                    addVo.BuhinName = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NAME)).Trim
                    '集計コード
                    addVo.ShukeiCode = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUKEI_CODE)).Trim
                    '手配記号
                    addVo.TehaiKigou = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_TEHAI_KIGOU)).Trim
                    '購坦
                    addVo.Koutan = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_KOUTAN)).Trim
                    '取引先コード
                    addVo.TorihikisakiCode = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_TORIHIKISAKI_CODE)).Trim
                    '納場
                    addVo.Nouba = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUBA)).Trim
                    '供給セクション
                    addVo.KyoukuSection = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_KYOUKU_SECTION)).Trim
                    '納入指示日
                    addVo.NounyuShijibi = ConvInt8Date(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)).Trim)
                    '合計員数
                    addVo.TotalInsuSuryo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_TOTAL_INSU_SURYO)).Trim
                    '出図予定日
                    addVo.ShutuzuYoteiDate = ConvInt8Date(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE)).Trim)

                    '出図実績_日付
                    addVo.ShutuzuJisekiDate = ConvInt8Date(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_DATE)).Trim)
                    '出図実績_改訂№
                    addVo.ShutuzuJisekiKaiteiNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO)).Trim
                    '出図実績_設通№
                    addVo.ShutuzuJisekiStsrDhstba = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)).Trim

                    '最終織込設変情報_日付
                    addVo.SaisyuSetsuhenDate = ConvInt8Date(sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_DATE)).Trim)
                    '最終織込設変情報_改訂№
                    addVo.SaisyuSetsuhenKaiteiNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO)).Trim
                    '最終織込設変情報_設通№
                    addVo.StsrDhstba = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_STSR_DHSTBA)).Trim

                    '材料寸法_X(mm)
                    Dim ZairyoSunpoX As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X)).Trim
                    If IsNumeric(ZairyoSunpoX) Then
                        addVo.ZairyoSunpoX = CDec(ZairyoSunpoX)
                    End If
                    '材料寸法_Y(mm)
                    Dim ZairyoSunpoY As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y)).Trim
                    If IsNumeric(ZairyoSunpoY) Then
                        addVo.ZairyoSunpoY = CDec(ZairyoSunpoY)
                    End If
                    '材料寸法_Z(mm)
                    Dim ZairyoSunpoZ As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z)).Trim
                    If IsNumeric(ZairyoSunpoZ) Then
                        addVo.ZairyoSunpoZ = CDec(ZairyoSunpoZ)
                    End If
                    '材料寸法_X+Y(mm)
                    Dim ZairyoSunpoXy As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY)).Trim
                    If IsNumeric(ZairyoSunpoXy) Then
                        addVo.ZairyoSunpoXy = CDec(ZairyoSunpoXy)
                    End If
                    '材料寸法_X+Z(mm)
                    Dim ZairyoSunpoXz As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ)).Trim
                    If IsNumeric(ZairyoSunpoXz) Then
                        addVo.ZairyoSunpoXz = CDec(ZairyoSunpoXz)
                    End If
                    '材料寸法_Y+Z(mm)
                    Dim ZairyoSunpoYz As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ)).Trim
                    If IsNumeric(ZairyoSunpoYz) Then
                        addVo.ZairyoSunpoYz = CDec(ZairyoSunpoYz)
                    End If

                    '材質・規格１
                    addVo.ZaishituKikaku1 = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1)).Trim
                    '材質・規格２
                    addVo.ZaishituKikaku2 = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2)).Trim
                    '材質・規格３
                    addVo.ZaishituKikaku3 = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3)).Trim
                    '材質・メッキ
                    addVo.ZaishituMekki = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_ZAISHITU_MEKKI)).Trim
                    '板厚
                    addVo.ShisakuBankoSuryo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO)).Trim
                    '板厚・ｕ
                    addVo.ShisakuBankoSuryoU = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U)).Trim
                    '材料情報・製品長
                    Dim materialinfoLength As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_LENGTH)).Trim
                    If IsNumeric(materialinfoLength) Then
                        addVo.MaterialInfoLength = CInt(materialinfoLength)
                    End If
                    '材料情報・製品幅
                    Dim materialinfoWidth As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_WIDTH)).Trim
                    If IsNumeric(materialinfoWidth) Then
                        addVo.MaterialInfoWidth = CInt(materialinfoWidth)
                    End If
                    '材料情報・発注対象
                    addVo.MaterialInfoOrderTarget = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET)).Trim
                    '材料情報・発注済
                    addVo.MaterialInfoOrderChk = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)).Trim
                    'データ項目・改訂№
                    addVo.DataItemKaiteiNo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO)).Trim
                    'データ項目・エリア名
                    addVo.DataItemAreaName = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME)).Trim
                    'データ項目・セット名
                    addVo.DataItemSetName = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME)).Trim
                    'データ項目・改訂情報
                    addVo.DataItemKaiteiInfo = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_INFO)).Trim
                    'データ項目・データ支給チェック欄
                    addVo.DataItemDataProvision = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION)).Trim
                    '取引先名称
                    addVo.MakerCode = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_MAKER_CODE)).Trim
                    '備考
                    addVo.Bikou = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_BIKOU)).Trim

                    _ResultList.Add(addVo)

                End If

            Next

            Return _ResultList
        End Function
        ''↑↑↑2015/01/20 注文書を追加 TES)張 ADD END

        ''↓↓↓2015/01/22 TES)張 ADD BEGIN
#Region "取引先CSVファイルの内容を読み込み"
        ''' <summary>
        ''' 取引先CSVファイルの内容を読み込み, データテーブルを生成します.
        ''' </summary>
        ''' <param name="fileName">読込みを行うCSVファイル(フルパス)</param>
        ''' <param name="isHeader">ヘッダー行があるか？</param>
        ''' <remarks></remarks>
        Public Sub ReadMakerCsv(ByVal makerCsvVos As List(Of MakerCsvVo), ByVal fileName As String, ByVal isHeader As Boolean)
            Dim csv = New CsvData(fileName, True)
            Dim tantoData As DataTable = csv.GetTable()

            For index = 1 To tantoData.Rows.Count - 1
                Dim csvVo As New MakerCsvVo
                csvVo.CompName = tantoData.Rows(index)(1).ToString
                csvVo.MakerCode = tantoData.Rows(index)(2).ToString
                csvVo.DeptName = tantoData.Rows(index)(3).ToString.Replace("""", "")
                csvVo.MakerName = tantoData.Rows(index)(4).ToString
                makerCsvVos.Add(csvVo)
            Next
        End Sub
#End Region

#Region "担当者CSVファイルの内容を読み込み"
        ''' <summary>
        ''' 担当者CSVファイルの内容を読み込み, データテーブルを生成します.
        ''' </summary>
        ''' <param name="fileName">読込みを行うCSVファイル(フルパス)</param>
        ''' <param name="isHeader">ヘッダー行があるか？</param>
        ''' <remarks></remarks>
        Public Sub ReadTantoCsv(ByVal tantoCsvVos As List(Of TantoCsvVo), ByVal fileName As String, ByVal isHeader As Boolean)
            Dim csv = New CsvData(fileName, True)
            Dim tantoData As DataTable = csv.GetTable()

            For index = 1 To tantoData.Rows.Count - 1
                Dim csvVo As New TantoCsvVo
                csvVo.No = tantoData.Rows(index)(0).ToString
                csvVo.Tel = tantoData.Rows(index)(1).ToString
                csvVo.Name = tantoData.Rows(index)(2).ToString
                tantoCsvVos.Add(csvVo)
            Next
        End Sub
#End Region

#Region "試作イベント情報を取得"
        ''' <summary>
        ''' 試作イベント情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindShisakuEventByKey(ByVal shisakuEventCode As String) As TShisakuEventVo
            Dim dao As TShisakuEventDao = New TShisakuEventDaoImpl
            Return dao.FindByPk(shisakuEventCode)
        End Function
#End Region

#Region "工事指令書を出力"
        ''' <summary>
        ''' 工事指令書を出力（メイン処理）
        ''' </summary>
        ''' <remarks></remarks>
        Public Function KoujiShireiOutput(ByVal outputVos As List(Of TShisakuTehaiKihonVo), ByVal excelVo As KojiShireiExcelVo) As Boolean
            'ファイルパス取得
            Dim fileName As String = OutputFileDialog(ShisakuCommon.ShisakuGlobal.ExcelKoujiShireiOutput, _
                                                      ShisakuCommon.ShisakuGlobal.ExcelKoujiShireiOutputDir)
            If fileName.Equals(String.Empty) Then
                Return False
            End If

            Cursor.Current = Cursors.WaitCursor

            '出力ファイル存在の場合、削除を行う
            If (FileIO.FileSystem.FileExists(fileName)) Then
                FileIO.FileSystem.DeleteFile(fileName)
            End If
            If (FileIO.FileSystem.FileExists(fileName & "_tmp.xls")) Then
                FileIO.FileSystem.DeleteFile(fileName & "_tmp.xls")
            End If

            'テンプレートファイルをコーピ―する
            FileCopy(ShisakuCommon.ShisakuGlobal.ExcelKoujiShireiTemplate, fileName & "_tmp.xls")

            Dim perPageRow As Integer = KojiShireExcel.PER_PAGE_ROW
            Dim pageCount As Integer
            'ページ数を取得
            If outputVos.Count > perPageRow Then
                If outputVos.Count Mod perPageRow > 0 Then
                    pageCount = Math.Floor(outputVos.Count / perPageRow) + 1
                Else
                    pageCount = CInt(outputVos.Count / perPageRow)
                End If
            Else
                pageCount = 1
            End If

            Using xls As New ShisakuExcel(fileName & "_tmp.xls")
                xls.SetActiveSheet(1)
                '共通内容出力
                WriteKoujiShireiFooter(xls, outputVos, excelVo, pageCount)
                '保存
                xls.Save()
                xls.Dispose()
            End Using

            FileCopy(fileName & "_tmp.xls", fileName)
            Using xls As New ShisakuExcel(fileName)
                For index = 1 To pageCount - 1
                    xls.SheetCopy(fileName & "_tmp.xls", index)
                Next
                '詳細内容出力
                WriteKoujiShireiData(xls, outputVos)
                '保存
                xls.Save()
                xls.Dispose()
            End Using
            'ワークファイルを削除
            FileIO.FileSystem.DeleteFile(fileName & "_tmp.xls")

            Process.Start(fileName)

            Return True

        End Function

        ''' <summary>
        ''' 工事指令書Excelの内部クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class KojiShireExcel
            Public Const PER_PAGE_ROW As Integer = 9
            Public Const START_DATA_ROW As Integer = 12
            Public Const END_DATA_ROW As Integer = 20

            '工事担当
            Public Const KOJI_TANTO_ROW_INDEX As Integer = 22
            Public Const KOJI_TANTO_COLUMN_INDEX As Integer = 1
            '件名
            Public Const KENMEI_ROW_INDEX As Integer = 22
            Public Const KENMEI_COLUMN_INDEX As Integer = 10
            '車種
            Public Const GOSHA_TYPE_ROW_INDEX As Integer = 21
            Public Const GOSHA_TYPE_COLUMN_INDEX As Integer = 33
            '目的
            Public Const MOKUTEKI_ROW_INDEX As Integer = 23
            Public Const MOKUTEKI_COLUMN_INDEX As Integer = 10
            '記事
            Public Const KIJI_ROW_INDEX As Integer = 25
            Public Const KIJI_COLUMN_INDEX As Integer = 10
            '製区
            Public Const SEIHIN_KBN_ROW_INDEX As Integer = 27
            Public Const SEIHIN_KBN_COLUMN_INDEX As Integer = 31
            '工事区分
            Public Const KOJI_KBN_ROW_INDEX As Integer = 27
            Public Const KOJI_KBN_COLUMN_INDEX As Integer = 34
            '予算区分
            Public Const YOSAN_KBN_ROW_INDEX As Integer = 27
            Public Const YOSAN_KBN_COLUMN_INDEX As Integer = 38

            '依頼部署
            '部
            Public Const IRAI_BU_ROW_INDEX As Integer = 21
            Public Const IRAI_BU_COLUMN_INDEX As Integer = 53
            '課
            Public Const IRAI_KA_ROW_INDEX As Integer = 21
            Public Const IRAI_KA_COLUMN_INDEX As Integer = 58
            '係
            Public Const IRAI_KEI_ROW_INDEX As Integer = 21
            Public Const IRAI_KEI_COLUMN_INDEX As Integer = 63
            '従業員名
            Public Const IRAI_NAME_ROW_INDEX As Integer = 22
            Public Const IRAI_NAME_COLUMN_INDEX As Integer = 64
            'TEL
            Public Const IRAI_TEL_ROW_INDEX As Integer = 22
            Public Const IRAI_TEL_COLUMN_INDEX As Integer = 67
            '従業員№
            Public Const IRAI_NO_ROW_INDEX As Integer = 23
            Public Const IRAI_NO_COLUMN_INDEX As Integer = 63
            '20:
            Public Const IRAI_DATE_ROW_INDEX As Integer = 24
            Public Const IRAI_DATE_COLUMN_INDEX As Integer = 53
            '工事指令№
            Public Const IRAI_SHIRE_NO_ROW_INDEX As Integer = 25
            Public Const IRAI_SHIRE_NO_COLUMN_INDEX As Integer = 54

            '指令部署
            '部
            Public Const SHIRE_BU_ROW_INDEX As Integer = 21
            Public Const SHIRE_BU_COLUMN_INDEX As Integer = 71
            '課
            Public Const SHIRE_KA_ROW_INDEX As Integer = 21
            Public Const SHIRE_KA_COLUMN_INDEX As Integer = 75
            '係
            Public Const SHIRE_KEI_ROW_INDEX As Integer = 21
            Public Const SHIRE_KEI_COLUMN_INDEX As Integer = 80
            '従業員名
            Public Const SHIRE_NAME_ROW_INDEX As Integer = 22
            Public Const SHIRE_NAME_COLUMN_INDEX As Integer = 80
            'TEL
            Public Const SHIRE_TEL_ROW_INDEX As Integer = 22
            Public Const SHIRE_TEL_COLUMN_INDEX As Integer = 83
            '従業員№
            Public Const SHIRE_NO_ROW_INDEX As Integer = 23
            Public Const SHIRE_NO_COLUMN_INDEX As Integer = 80
            '20:
            Public Const SHIRE_DATE_ROW_INDEX As Integer = 24
            Public Const SHIRE_DATE_COLUMN_INDEX As Integer = 71
            '工事指令№
            Public Const SHIRE_SHIRE_NO_ROW_INDEX As Integer = 25
            Public Const SHIRE_SHIRE_NO_COLUMN_INDEX As Integer = 72

            'ページ数
            Public Const PAGE_INDEX_ROW_INDEX As Integer = 27
            Public Const PAGE_INDEX_COLUMN_INDEX As Integer = 83
            '総ページ数
            Public Const PAGE_COUNT_ROW_INDEX As Integer = 28
            Public Const PAGE_COUNT_COLUMN_INDEX As Integer = 84

            '購担
            Public Const KOUTAN_COLUMN_INDEX As Integer = 7
            '取引先
            Public Const TORIHIKISAKI_COLUMN_INDEX As Integer = 9
            '部品番号
            Public Const BUHIN_NO_COLUMN_INDEX As Integer = 13
            '部品名称
            Public Const BUHIN_NAME_COLUMN_INDEX As Integer = 27
            '納場
            Public Const NOUBA_COLUMN_INDEX As Integer = 43
            '供給セクション
            Public Const KYOUKU_SECTION_COLUMN_INDEX As Integer = 45
            '納期
            Public Const NOUKI_COLUMN_INDEX As Integer = 48
            '製作数
            Public Const SEISAKU_COUNT_COLUMN_INDEX As Integer = 54
        End Class

        ''' <summary>
        ''' 工事担当を取得（購担をグループ化）
        ''' </summary>
        ''' <param name="outputVos"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetKojiTanto(ByVal outputVos As List(Of TShisakuTehaiKihonVo)) As String
            Dim strVos As New List(Of String)
            Dim kojiTanto As String = String.Empty

            For Each vo As TShisakuTehaiKihonVo In outputVos
                If StringUtil.IsNotEmpty(vo.Koutan) AndAlso Not strVos.Contains(vo.Koutan) Then
                    strVos.Add(vo.Koutan)
                End If
            Next
            If strVos.Count > 0 Then
                For Each key As String In strVos
                    kojiTanto = kojiTanto & "," & key
                Next
                Return kojiTanto.Substring(1)
            Else
                Return String.Empty
            End If
        End Function

        ''' <summary>
        ''' 工事指令書を出力（フッター出力）
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub WriteKoujiShireiFooter(ByVal xls As ShisakuExcel, ByVal outputVos As List(Of TShisakuTehaiKihonVo), _
                                           ByVal excelVo As KojiShireiExcelVo, ByVal pageCount As Integer)
            '工事担当
            Dim kojiTanto As String = GetKojiTanto(outputVos)
            xls.SetValue(KojiShireExcel.KOJI_TANTO_COLUMN_INDEX, KojiShireExcel.KOJI_TANTO_ROW_INDEX, kojiTanto)
            '件名
            xls.SetValue(KojiShireExcel.KENMEI_COLUMN_INDEX, KojiShireExcel.KENMEI_ROW_INDEX, excelVo.Kenmei)
            '車種
            xls.SetValue(KojiShireExcel.GOSHA_TYPE_COLUMN_INDEX, KojiShireExcel.GOSHA_TYPE_ROW_INDEX, excelVo.GoshaType)
            '目的
            xls.SetValue(KojiShireExcel.MOKUTEKI_COLUMN_INDEX, KojiShireExcel.MOKUTEKI_ROW_INDEX, excelVo.Mokuteki)
            '記事
            xls.SetValue(KojiShireExcel.KIJI_COLUMN_INDEX, KojiShireExcel.KIJI_ROW_INDEX, excelVo.Kiji)
            '製区
            xls.SetValue(KojiShireExcel.SEIHIN_KBN_COLUMN_INDEX, KojiShireExcel.SEIHIN_KBN_ROW_INDEX, _headerSubject.seihinKbn)
            '工事区分
            xls.SetValue(KojiShireExcel.KOJI_KBN_COLUMN_INDEX, KojiShireExcel.KOJI_KBN_ROW_INDEX, _headerSubject.koujiKbn)
            '予算区分
            xls.SetValue(KojiShireExcel.YOSAN_KBN_COLUMN_INDEX, KojiShireExcel.KOJI_KBN_ROW_INDEX, _headerSubject.yosanCode)

            Dim nowDate As New ShisakuDate
            Dim strDate As String = nowDate.CurrentDateDbFormat
            '依頼部署の場合
            If excelVo.IsIrai Then
                '部
                xls.SetValue(KojiShireExcel.IRAI_BU_COLUMN_INDEX, KojiShireExcel.IRAI_BU_ROW_INDEX, "試作")
                '課
                xls.SetValue(KojiShireExcel.IRAI_KA_COLUMN_INDEX, KojiShireExcel.IRAI_KA_ROW_INDEX, "試作計画１")
                '係
                xls.SetValue(KojiShireExcel.IRAI_KEI_COLUMN_INDEX, KojiShireExcel.IRAI_KEI_ROW_INDEX, "メタル手配")
                '従業員名
                xls.SetValue(KojiShireExcel.IRAI_NAME_COLUMN_INDEX, KojiShireExcel.IRAI_NAME_ROW_INDEX, excelVo.Tanto)
                'TEL
                xls.SetValue(KojiShireExcel.IRAI_TEL_COLUMN_INDEX, KojiShireExcel.IRAI_TEL_ROW_INDEX, excelVo.TantoTel)
                '従業員№
                If StringUtil.IsNotEmpty(excelVo.TantoNo) Then
                    xls.SetValue(KojiShireExcel.IRAI_NO_COLUMN_INDEX, KojiShireExcel.IRAI_NO_ROW_INDEX, excelVo.TantoNo.Substring(0, 1))
                    xls.SetValue(KojiShireExcel.IRAI_NO_COLUMN_INDEX + 1, KojiShireExcel.IRAI_NO_ROW_INDEX, excelVo.TantoNo.Substring(1, 1))
                    xls.SetValue(KojiShireExcel.IRAI_NO_COLUMN_INDEX + 2, KojiShireExcel.IRAI_NO_ROW_INDEX, excelVo.TantoNo.Substring(2, 1))
                    xls.SetValue(KojiShireExcel.IRAI_NO_COLUMN_INDEX + 3, KojiShireExcel.IRAI_NO_ROW_INDEX, excelVo.TantoNo.Substring(3, 1))
                    xls.SetValue(KojiShireExcel.IRAI_NO_COLUMN_INDEX + 4, KojiShireExcel.IRAI_NO_ROW_INDEX, excelVo.TantoNo.Substring(4, 1))
                    xls.SetValue(KojiShireExcel.IRAI_NO_COLUMN_INDEX + 5, KojiShireExcel.IRAI_NO_ROW_INDEX, excelVo.TantoNo.Substring(5, 1))
                End If
                '20:
                xls.SetValue(KojiShireExcel.IRAI_DATE_COLUMN_INDEX, KojiShireExcel.IRAI_DATE_ROW_INDEX, strDate.Substring(0, 2))
                xls.SetValue(KojiShireExcel.IRAI_DATE_COLUMN_INDEX + 4, KojiShireExcel.IRAI_DATE_ROW_INDEX, strDate.Substring(2, 1))
                xls.SetValue(KojiShireExcel.IRAI_DATE_COLUMN_INDEX + 6, KojiShireExcel.IRAI_DATE_ROW_INDEX, strDate.Substring(3, 1))
                xls.SetValue(KojiShireExcel.IRAI_DATE_COLUMN_INDEX + 8, KojiShireExcel.IRAI_DATE_ROW_INDEX, strDate.Substring(5, 1))
                xls.SetValue(KojiShireExcel.IRAI_DATE_COLUMN_INDEX + 10, KojiShireExcel.IRAI_DATE_ROW_INDEX, strDate.Substring(6, 1))
                xls.SetValue(KojiShireExcel.IRAI_DATE_COLUMN_INDEX + 12, KojiShireExcel.IRAI_DATE_ROW_INDEX, strDate.Substring(8, 1))
                xls.SetValue(KojiShireExcel.IRAI_DATE_COLUMN_INDEX + 14, KojiShireExcel.IRAI_DATE_ROW_INDEX, strDate.Substring(9, 1))
                '工事指令№
                If StringUtil.IsNotEmpty(excelVo.KojiNo) Then
                    'xls.SetValue(KojiShireExcel.IRAI_SHIRE_NO_COLUMN_INDEX, KojiShireExcel.IRAI_SHIRE_NO_ROW_INDEX, excelVo.KojiNo)
                    xls.SetValue(KojiShireExcel.IRAI_SHIRE_NO_COLUMN_INDEX, KojiShireExcel.IRAI_SHIRE_NO_ROW_INDEX, excelVo.KojiNo.Substring(0, 1))
                    xls.SetValue(KojiShireExcel.IRAI_SHIRE_NO_COLUMN_INDEX + 2, KojiShireExcel.IRAI_SHIRE_NO_ROW_INDEX, excelVo.KojiNo.Substring(1, 1))
                    xls.SetValue(KojiShireExcel.IRAI_SHIRE_NO_COLUMN_INDEX + 4, KojiShireExcel.IRAI_SHIRE_NO_ROW_INDEX, excelVo.KojiNo.Substring(2, 1))
                    xls.SetValue(KojiShireExcel.IRAI_SHIRE_NO_COLUMN_INDEX + 6, KojiShireExcel.IRAI_SHIRE_NO_ROW_INDEX, excelVo.KojiNo.Substring(3, 1))
                    xls.SetValue(KojiShireExcel.IRAI_SHIRE_NO_COLUMN_INDEX + 8, KojiShireExcel.IRAI_SHIRE_NO_ROW_INDEX, excelVo.KojiNo.Substring(4, 1))
                    xls.SetValue(KojiShireExcel.IRAI_SHIRE_NO_COLUMN_INDEX + 11, KojiShireExcel.IRAI_SHIRE_NO_ROW_INDEX, excelVo.KojiNo.Substring(5, 1))
                    xls.SetValue(KojiShireExcel.IRAI_SHIRE_NO_COLUMN_INDEX + 13, KojiShireExcel.IRAI_SHIRE_NO_ROW_INDEX, excelVo.KojiNo.Substring(6, 1))
                End If
            End If
            '指令部署の場合
            If excelVo.IsShire Then
                '部
                xls.SetValue(KojiShireExcel.SHIRE_BU_COLUMN_INDEX, KojiShireExcel.SHIRE_BU_ROW_INDEX, "試作")
                '課
                xls.SetValue(KojiShireExcel.SHIRE_KA_COLUMN_INDEX, KojiShireExcel.SHIRE_KA_ROW_INDEX, "試作計画１")
                '係
                xls.SetValue(KojiShireExcel.SHIRE_KEI_COLUMN_INDEX, KojiShireExcel.SHIRE_KEI_ROW_INDEX, "メタル手配")
                '従業員名
                xls.SetValue(KojiShireExcel.SHIRE_NAME_COLUMN_INDEX, KojiShireExcel.SHIRE_NAME_ROW_INDEX, excelVo.Tanto)
                'TEL
                xls.SetValue(KojiShireExcel.SHIRE_TEL_COLUMN_INDEX, KojiShireExcel.SHIRE_TEL_ROW_INDEX, excelVo.TantoTel)
                '従業員№
                If StringUtil.IsNotEmpty(excelVo.TantoNo) Then
                    xls.SetValue(KojiShireExcel.SHIRE_NO_COLUMN_INDEX, KojiShireExcel.SHIRE_NO_ROW_INDEX, excelVo.TantoNo.Substring(0, 1))
                    xls.SetValue(KojiShireExcel.SHIRE_NO_COLUMN_INDEX + 1, KojiShireExcel.SHIRE_NO_ROW_INDEX, excelVo.TantoNo.Substring(1, 1))
                    xls.SetValue(KojiShireExcel.SHIRE_NO_COLUMN_INDEX + 2, KojiShireExcel.SHIRE_NO_ROW_INDEX, excelVo.TantoNo.Substring(2, 1))
                    xls.SetValue(KojiShireExcel.SHIRE_NO_COLUMN_INDEX + 3, KojiShireExcel.SHIRE_NO_ROW_INDEX, excelVo.TantoNo.Substring(3, 1))
                    xls.SetValue(KojiShireExcel.SHIRE_NO_COLUMN_INDEX + 4, KojiShireExcel.SHIRE_NO_ROW_INDEX, excelVo.TantoNo.Substring(4, 1))
                    xls.SetValue(KojiShireExcel.SHIRE_NO_COLUMN_INDEX + 5, KojiShireExcel.SHIRE_NO_ROW_INDEX, excelVo.TantoNo.Substring(5, 1))
                End If
                '20:
                xls.SetValue(KojiShireExcel.SHIRE_DATE_COLUMN_INDEX, KojiShireExcel.SHIRE_DATE_ROW_INDEX, strDate.Substring(0, 2))
                xls.SetValue(KojiShireExcel.SHIRE_DATE_COLUMN_INDEX + 3, KojiShireExcel.SHIRE_DATE_ROW_INDEX, strDate.Substring(2, 1))
                xls.SetValue(KojiShireExcel.SHIRE_DATE_COLUMN_INDEX + 5, KojiShireExcel.SHIRE_DATE_ROW_INDEX, strDate.Substring(3, 1))
                xls.SetValue(KojiShireExcel.SHIRE_DATE_COLUMN_INDEX + 7, KojiShireExcel.SHIRE_DATE_ROW_INDEX, strDate.Substring(5, 1))
                xls.SetValue(KojiShireExcel.SHIRE_DATE_COLUMN_INDEX + 9, KojiShireExcel.SHIRE_DATE_ROW_INDEX, strDate.Substring(6, 1))
                xls.SetValue(KojiShireExcel.SHIRE_DATE_COLUMN_INDEX + 11, KojiShireExcel.SHIRE_DATE_ROW_INDEX, strDate.Substring(8, 1))
                xls.SetValue(KojiShireExcel.SHIRE_DATE_COLUMN_INDEX + 13, KojiShireExcel.SHIRE_DATE_ROW_INDEX, strDate.Substring(9, 1))
                '工事指令№
                If StringUtil.IsNotEmpty(excelVo.KojiNo) Then
                    'xls.SetValue(KojiShireExcel.SHIRE_SHIRE_NO_COLUMN_INDEX, KojiShireExcel.SHIRE_SHIRE_NO_ROW_INDEX, excelVo.KojiNo)
                    xls.SetValue(KojiShireExcel.SHIRE_SHIRE_NO_COLUMN_INDEX, KojiShireExcel.SHIRE_SHIRE_NO_ROW_INDEX, excelVo.KojiNo.Substring(0, 1))
                    xls.SetValue(KojiShireExcel.SHIRE_SHIRE_NO_COLUMN_INDEX + 2, KojiShireExcel.SHIRE_SHIRE_NO_ROW_INDEX, excelVo.KojiNo.Substring(1, 1))
                    xls.SetValue(KojiShireExcel.SHIRE_SHIRE_NO_COLUMN_INDEX + 4, KojiShireExcel.SHIRE_SHIRE_NO_ROW_INDEX, excelVo.KojiNo.Substring(2, 1))
                    xls.SetValue(KojiShireExcel.SHIRE_SHIRE_NO_COLUMN_INDEX + 6, KojiShireExcel.SHIRE_SHIRE_NO_ROW_INDEX, excelVo.KojiNo.Substring(3, 1))
                    xls.SetValue(KojiShireExcel.SHIRE_SHIRE_NO_COLUMN_INDEX + 8, KojiShireExcel.SHIRE_SHIRE_NO_ROW_INDEX, excelVo.KojiNo.Substring(4, 1))
                    xls.SetValue(KojiShireExcel.SHIRE_SHIRE_NO_COLUMN_INDEX + 10, KojiShireExcel.SHIRE_SHIRE_NO_ROW_INDEX, excelVo.KojiNo.Substring(5, 1))
                    xls.SetValue(KojiShireExcel.SHIRE_SHIRE_NO_COLUMN_INDEX + 12, KojiShireExcel.SHIRE_SHIRE_NO_ROW_INDEX, excelVo.KojiNo.Substring(6, 1))
                End If
            End If

            '総ページ数
            xls.SetValue(KojiShireExcel.PAGE_COUNT_COLUMN_INDEX, KojiShireExcel.PAGE_COUNT_ROW_INDEX, pageCount)

        End Sub

        ''' <summary>
        ''' 工事指令書を出力（データ出力）
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub WriteKoujiShireiData(ByVal xls As ShisakuExcel, ByVal outputVos As List(Of TShisakuTehaiKihonVo))
            Dim perPageRow As Integer = KojiShireExcel.PER_PAGE_ROW
            Dim startDataRow As Integer = KojiShireExcel.START_DATA_ROW
            Dim pageIndex As Integer = 1
            Dim dataIndex As Integer = 0

            xls.SetActiveSheet(pageIndex)
            'ページ数
            xls.SetValue(KojiShireExcel.PAGE_INDEX_COLUMN_INDEX, KojiShireExcel.PAGE_INDEX_ROW_INDEX, pageIndex)

            For Each vo As TShisakuTehaiKihonVo In outputVos
                '改ページ
                If dataIndex = perPageRow * pageIndex Then
                    pageIndex = pageIndex + 1
                    xls.SetActiveSheet(pageIndex)
                    'ページ数
                    xls.SetValue(KojiShireExcel.PAGE_INDEX_COLUMN_INDEX, KojiShireExcel.PAGE_INDEX_ROW_INDEX, pageIndex)
                End If
                '購担
                xls.SetValue(KojiShireExcel.KOUTAN_COLUMN_INDEX, startDataRow + dataIndex Mod perPageRow, vo.Koutan)
                '取引先
                xls.SetValue(KojiShireExcel.TORIHIKISAKI_COLUMN_INDEX, startDataRow + dataIndex Mod perPageRow, vo.TorihikisakiCode)
                '部品番号
                xls.SetValue(KojiShireExcel.BUHIN_NO_COLUMN_INDEX, startDataRow + dataIndex Mod perPageRow, vo.BuhinNo)
                '部品名称
                xls.SetValue(KojiShireExcel.BUHIN_NAME_COLUMN_INDEX, startDataRow + dataIndex Mod perPageRow, vo.BuhinName)
                '納場
                xls.SetValue(KojiShireExcel.NOUBA_COLUMN_INDEX, startDataRow + dataIndex Mod perPageRow, vo.Nouba)
                '供給セクション
                xls.SetValue(KojiShireExcel.KYOUKU_SECTION_COLUMN_INDEX, startDataRow + dataIndex Mod perPageRow, vo.KyoukuSection)
                '納期
                xls.SetValue(KojiShireExcel.NOUKI_COLUMN_INDEX, startDataRow + dataIndex Mod perPageRow, ConvDateInt8(vo.NounyuShijibi))
                '製作数
                xls.SetValue(KojiShireExcel.SEISAKU_COUNT_COLUMN_INDEX, startDataRow + dataIndex Mod perPageRow, vo.TotalInsuSuryo.ToString)

                dataIndex = dataIndex + 1
            Next

        End Sub
#End Region

#Region "データ支給依頼書を出力"
        ''' <summary>
        ''' データ支給依頼書を出力（メイン処理）
        ''' </summary>
        ''' <remarks></remarks>
        Public Function SikyuuExport(ByVal TorihikisakiCode As String, ByVal deptName As String, ByVal makerCode As String, ByVal makerName As String, ByVal shisakuKaihatsuFugo As String, ByVal content As String, ByVal hosoku As String, ByVal riyuu As String, ByVal _selectVo As List(Of TShisakuTehaiKihonVo)) As Boolean

            _torihikisakiCode = TorihikisakiCode
            _deptName = deptName
            _makerCode = makerCode
            _makerName = makerName
            _shisakuKaihatsuFugo = shisakuKaihatsuFugo
            _content = content
            _hosoku = hosoku
            _riyuu = riyuu

            Dim newFileName As String = _torihikisakiCode + "_" + LoginInfo.Now.UserId + "_" + Now.ToString("yyyyMMdd") + "_" + Now.ToString("HHmmss") + ".doc"

            Dim filePath As String = WordOutPutDir + "\" + _torihikisakiCode + "\" + newFileName

            If Directory.Exists(WordOutPutDir + "\" + _torihikisakiCode) = False Then
                Directory.CreateDirectory(WordOutPutDir + "\" + _torihikisakiCode)
            End If

            Dim fileName As String = OutputWordDialog(newFileName, filePath)
            If fileName.Equals(String.Empty) Then
                Return False
            End If
            Cursor.Current = Cursors.WaitCursor

            '出力処理へ

            '出力ファイル存在の場合、削除を行う
            If (FileIO.FileSystem.FileExists(fileName)) Then
                FileIO.FileSystem.DeleteFile(fileName)
            End If

            'テンプレートファイルをコーピ―する
            FileCopy(ShisakuCommon.ShisakuGlobal.WordInPut, fileName)

            OpenWord(fileName, filePath, _selectVo)

            Process.Start(fileName)

            Return True

        End Function

        ''' <summary>
        ''' ファイル保存用ダイアログを開く
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function OutputWordDialog(ByVal file As String, ByVal dir As String) As String
            Dim fileName As String
            Dim sfd As New SaveFileDialog()

            ' ファイル名を指定します
            sfd.FileName = file
            sfd.Title = "出力対象のファイルを選択してください"

            ' 起動ディレクトリを指定します
            sfd.InitialDirectory = dir

            '  [ファイルの種類] ボックスに表示される選択肢を指定します
            sfd.Filter = "Word files(*.doc)|*.doc"

            'ダイアログ選択有無
            If sfd.ShowDialog() = DialogResult.OK Then
                fileName = sfd.FileName
            Else
                Return String.Empty
            End If

            sfd.Dispose()

            Return fileName

        End Function

        Private Sub OpenWord(ByVal FileName As String, ByVal filePath As String, ByVal _selectList As List(Of TShisakuTehaiKihonVo)) '打开指定word文档 

            WordDoc = New Word.Document
            Dim wTable As Word.Table
            Dim Missing As Object = Type.Missing
            WordApp = CreateObject("Word.Application")
            WordApp.Visible = False

            Dim oTemplate As Object = CType(FileName, Object)

            Try

                WordDoc = WordApp.Documents.Open(oTemplate, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing)
                isOpened = True
                WordDoc.Activate()

                WordDoc.Tables.Item(3).Cell(8, 4).HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
                WordDoc.Tables.Item(3).Cell(8, 4).Height = 40

                WordDoc.Tables.Item(3).Cell(10, 2).HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
                WordDoc.Tables.Item(3).Cell(10, 2).Height = 80

                '进行'申請日の年'查找替换
                With WordApp.Selection.Find
                    .ClearFormatting()
                    .Text = "SIN_Y"
                    .Replacement.ClearFormatting()
                    .Replacement.Text = Now.ToString("yyyy")
                    .Execute(Replace:=Word.WdReplace.wdReplaceAll, Forward:=True, Wrap:=Word.WdContinue.wdContinueList)
                End With
                '进行'申請日の月'查找替换
                With WordApp.Selection.Find
                    .ClearFormatting()
                    .Text = "SIN_M"
                    .Replacement.ClearFormatting()
                    .Replacement.Text = Now.ToString("MM")
                    .Execute(Replace:=Word.WdReplace.wdReplaceAll, Forward:=True, Wrap:=Word.WdContinue.wdContinueList)
                End With
                '进行'申請日の日'查找替换
                With WordApp.Selection.Find
                    .ClearFormatting()
                    .Text = "SIN_D"
                    .Replacement.ClearFormatting()
                    .Replacement.Text = Now.ToString("dd")
                    .Execute(Replace:=Word.WdReplace.wdReplaceAll, Forward:=True, Wrap:=Word.WdContinue.wdContinueList)
                End With
                '进行'提供希望日の年'查找替换
                With WordApp.Selection.Find
                    .ClearFormatting()
                    .Text = "TEI_Y"
                    .Replacement.ClearFormatting()
                    .Replacement.Text = Now.ToString("yyyy")
                    .Execute(Replace:=Word.WdReplace.wdReplaceAll, Forward:=True, Wrap:=Word.WdContinue.wdContinueList)
                End With
                '进行'提供希望日の月'查找替换
                With WordApp.Selection.Find
                    .ClearFormatting()
                    .Text = "TEI_M"
                    .Replacement.ClearFormatting()
                    .Replacement.Text = Now.ToString("MM")
                    .Execute(Replace:=Word.WdReplace.wdReplaceAll, Forward:=True, Wrap:=Word.WdContinue.wdContinueList)
                End With
                '进行'提供希望日の日'查找替换
                With WordApp.Selection.Find
                    .ClearFormatting()
                    .Text = "TEI_D"
                    .Replacement.ClearFormatting()
                    .Replacement.Text = Now.ToString("dd")
                    .Execute(Replace:=Word.WdReplace.wdReplaceAll, Forward:=True, Wrap:=Word.WdContinue.wdContinueList)
                End With

                '进行'提供先の担当者'查找替换
                With WordApp.Selection.Find
                    .ClearFormatting()
                    .Text = "TORIHIKISAKI_TANTO_NAME"
                    .Replacement.ClearFormatting()
                    .Replacement.Text = _makerName
                    .Execute(Replace:=Word.WdReplace.wdReplaceAll, Forward:=True, Wrap:=Word.WdContinue.wdContinueList)
                End With
                '进行'申請部署の担当者'查找替换
                With WordApp.Selection.Find
                    .ClearFormatting()
                    .Text = "TANTO_NAME"
                    .Replacement.ClearFormatting()
                    .Replacement.Text = LoginInfo.Now.ShainName
                    .Execute(Replace:=Word.WdReplace.wdReplaceAll, Forward:=True, Wrap:=Word.WdContinue.wdContinueList)
                End With
                '进行会社名查找替换
                With WordApp.Selection.Find
                    .ClearFormatting()
                    .Text = "TORIHIKISAKI_NAME"
                    .Replacement.ClearFormatting()
                    .Replacement.Text = _torihikisakiCode
                    .Execute(Replace:=Word.WdReplace.wdReplaceAll, Forward:=True, Wrap:=Word.WdContinue.wdContinueList)
                End With
                '进行ﾒｰｶｰｺｰﾄﾞ查找替换
                With WordApp.Selection.Find
                    .ClearFormatting()
                    .Text = "TORIHIKISAKI_CODE"
                    .Replacement.ClearFormatting()
                    .Replacement.Text = _makerCode
                    .Execute(Replace:=Word.WdReplace.wdReplaceAll, Forward:=True, Wrap:=Word.WdContinue.wdContinueList)
                End With
                '进行担当部署查找替换
                With WordApp.Selection.Find
                    .ClearFormatting()
                    .Text = "TORIHIKISAKI_TANTO_BUSYO"
                    .Replacement.ClearFormatting()
                    .Replacement.Text = _deptName
                    .Execute(Replace:=Word.WdReplace.wdReplaceAll, Forward:=True, Wrap:=Word.WdContinue.wdContinueList)
                End With

                '进行開発ｺｰﾄﾞ／車種查找替换
                With WordApp.Selection.Find
                    .ClearFormatting()
                    .Text = "KAIHATSU_CD"
                    .Replacement.ClearFormatting()
                    .Replacement.Text = _shisakuKaihatsuFugo
                    .Execute(Replace:=Word.WdReplace.wdReplaceAll, Forward:=True, Wrap:=Word.WdContinue.wdContinueList)
                End With
                '进行内容・件名查找替换
                With WordApp.Selection.Find
                    .ClearFormatting()
                    .Text = "NAIYO_KENMEI"
                    .Replacement.ClearFormatting()
                    .Replacement.Text = _content.Substring(0, _content.Length)
                    .Execute(Replace:=Word.WdReplace.wdReplaceAll, Forward:=True, Wrap:=Word.WdContinue.wdContinueList)
                End With
                '进行内容・補足情報查找替换
                With WordApp.Selection.Find
                    .ClearFormatting()
                    .Text = "NAIYO_HOSOKU"
                    .Replacement.ClearFormatting()
                    .Replacement.Text = _hosoku.Substring(0, _hosoku.Length)
                    .Execute(Replace:=Word.WdReplace.wdReplaceAll, Forward:=True, Wrap:=Word.WdContinue.wdContinueList)
                End With
                '进行支給理由・使用目的查找替换
                With WordApp.Selection.Find
                    .ClearFormatting()
                    .Text = "SHIKYU_RIYU"
                    .Replacement.ClearFormatting()
                    .Replacement.Text = _riyuu.Substring(0, _riyuu.Length)
                    .Execute(Replace:=Word.WdReplace.wdReplaceAll, Forward:=True, Wrap:=Word.WdContinue.wdContinueList)
                End With

                wTable = WordDoc.Tables.Item(4)
                Dim pageCount As Integer = 0
                Dim startPos As String = WordDoc.Bookmarks("tag_start").Range.Start
                Dim endPos As String = WordDoc.Bookmarks("tag_end").Range.Start

                For i As Integer = 1 To _selectList.Count
                    If i Mod 20 = 0 AndAlso _selectList.Count Mod 20 <> 0 Then

                        WordApp.ActiveDocument.Range(startPos, endPos).Copy()
                        Dim nextPage As Object = Word.WdUnits.wdStory
                        WordApp.Selection.EndKey(nextPage, Missing)
                        Dim pBreak As Object = Word.WdBreakType.wdPageBreak
                        WordApp.Selection.InsertBreak(pBreak)
                        WordApp.Selection.PasteAndFormat(0)

                        pageCount = pageCount + 1

                    End If
                Next

                Dim perPageRow As Integer = 20
                Dim startDataRow As Integer = 3
                Dim pageIndex As Integer = 1
                Dim dataIndex As Integer = 0

                For Each vo As TShisakuTehaiKihonVo In _selectList
                    If dataIndex = perPageRow * pageIndex Then
                        pageIndex = pageIndex + 1
                    End If

                    WordDoc.Tables.Item(4 + (2 * (pageIndex - 1))).Cell(startDataRow + dataIndex Mod perPageRow, 2).Range.Text = vo.DataItemAreaName + " / " + vo.DataItemSetName

                    dataIndex = dataIndex + 1
                Next

                For i As Integer = 1 To pageCount

                    For j As Integer = 0 To 19
                        WordDoc.Tables.Item(4 + (2 * i)).Cell(3 + j, 1).Range.Text = i * 20 + j + 1
                    Next

                Next

                For i As Integer = _selectList.Count - pageCount * 20 To 19
                    WordDoc.Tables.Item(4 + (2 * pageCount)).Cell(3 + i, 2).Range.Text = ""
                Next

                WordDoc.Save()

                WordDoc.SaveAs(filePath)

                WordDoc.Close()
                Quit()

                WordApp = Nothing

                GC.SuppressFinalize(Me)

            Catch ex As Exception

                Quit()
                isOpened = False
                Throw New Exception(ex.Message)
            End Try

        End Sub

        Public Sub Quit()

            Dim missing As Object = System.Reflection.Missing.Value
            WordApp.Application.Quit(missing, missing, missing)
            isOpened = False
        End Sub

#End Region

#Region "注文書を出力"
        ''' <summary>
        ''' 注文書を出力（メイン処理）
        ''' </summary>
        ''' <remarks></remarks>
        Public Function OrderOutput(ByVal outputVos As List(Of TShisakuTehaiKihonVo), ByVal torihikisaki As String, ByVal tantosha As String, ByVal basho As String) As Boolean
            'ファイルパス取得
            Dim fileName As String = OutputFileDialog(ShisakuCommon.ShisakuGlobal.ExcelOrderOutput, _
                                                      ShisakuCommon.ShisakuGlobal.ExcelOrderOutputDir)
            If fileName.Equals(String.Empty) Then
                Return False
            End If

            Cursor.Current = Cursors.WaitCursor

            '出力ファイル存在の場合、削除を行う
            If (FileIO.FileSystem.FileExists(fileName)) Then
                FileIO.FileSystem.DeleteFile(fileName)
            End If

            'テンプレートファイルをコーピ―する
            FileCopy(ShisakuCommon.ShisakuGlobal.ExcelOrderTemplate, fileName)

            Dim nowDate As New ShisakuDate
            Dim strDate As String = nowDate.CurrentDateDbFormat

            Dim pageCount As Integer = GetOrderOutputPageCount(outputVos)

            Using xls As New ShisakuExcel(fileName)
                xls.SetActiveSheet(2)
                '先頭シート出力
                WriteOrderSheetFirst(xls, outputVos, torihikisaki, tantosha, basho, strDate, pageCount)

                If outputVos.Count > OrderExcel.FIRST_PAGE_ROW_COUNT Then
                    xls.SetActiveSheet(1)
                    '次ページ出力
                    WriteOrderSheetNext(xls, outputVos, torihikisaki, basho, strDate, pageCount)
                Else
                    '１つページのみ出力
                    xls.DeleteSheetTrialManufacture(1, True)
                End If

                '保存
                xls.Save()
                xls.Dispose()
            End Using

            Process.Start(fileName)

            Return True

        End Function

        ''' <summary>
        ''' 注文書Excelの内部クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class OrderExcel
            '先頭ページ
            Public Const FIRST_PAGE_ROW_COUNT As Integer = 15
            Public Const FIRST_PAGE_START_DATA_ROW As Integer = 14
            '取引先名
            Public Const FIRST_MAKER_ROW_INDEX As Integer = 2
            Public Const FIRST_MAKER_COLUMN_INDEX As Integer = 3
            '頁№
            Public Const FIRST_PAGE_ROW_INDEX As Integer = 2
            Public Const FIRST_PAGE_COLUMN_INDEX As Integer = 15
            '発行日
            Public Const FIRST_DATE_ROW_INDEX As Integer = 3
            Public Const FIRST_DATE_COLUMN_INDEX As Integer = 13
            '作成者
            Public Const FIRST_NAME_ROW_INDEX As Integer = 9
            Public Const FIRST_NAME_COLUMN_INDEX As Integer = 15

            '次ページ
            Public Const NEXT_PAGE_ROW_COUNT As Integer = 17
            Public Const NEXT_PAGE_START_DATA_ROW As Integer = 8
            '取引先名
            Public Const NEXT_MAKER_ROW_INDEX As Integer = 3
            Public Const NEXT_MAKER_COLUMN_INDEX As Integer = 3
            '頁№
            Public Const NEXT_PAGE_ROW_INDEX As Integer = 3
            Public Const NEXT_PAGE_COLUMN_INDEX As Integer = 15
            '発行日
            Public Const NEXT_DATE_ROW_INDEX As Integer = 4
            Public Const NEXT_DATE_COLUMN_INDEX As Integer = 14

            '№
            Public Const NO_COLUMN_INDEX As Integer = 2
            '工事名
            Public Const KOJI_NAME_COLUMN_INDEX As Integer = 3
            '部品名称/受託業務名
            Public Const BUHIN_NAME_COLUMN_INDEX As Integer = 4
            '部品番号(識別)
            Public Const BUHIN_NO_COLUMN_INDEX As Integer = 6
            '規　格　　　寸　度
            Public Const KIKAKU_COLUMN_INDEX As Integer = 8
            '数量
            Public Const QUATITY_COLUMN_INDEX As Integer = 9
            '単価(円)
            Public Const TANKA_COLUMN_INDEX As Integer = 10
            '要求　　　納入日
            Public Const NOUNYUU_DATE_COLUMN_INDEX As Integer = 11
            '納入　　　場所
            Public Const NOUNYUU_PLACE_COLUMN_INDEX As Integer = 12
            '検査完了　期日
            Public Const KANRYOU_DATE_COLUMN_INDEX As Integer = 13
            '備考
            Public Const BIKO_COLUMN_INDEX As Integer = 14

        End Class

        ''' <summary>
        ''' 注文書出力の総ページ数を取得
        ''' </summary>
        ''' <param name="outputVos"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetOrderOutputPageCount(ByVal outputVos As List(Of TShisakuTehaiKihonVo)) As Integer
            Dim fstPageRowCount As Integer = OrderExcel.FIRST_PAGE_ROW_COUNT
            Dim nextPageRowCount As Integer = OrderExcel.NEXT_PAGE_ROW_COUNT
            Dim dataCount As Integer = outputVos.Count
            Dim pageCount As Integer

            'ページ数を取得
            If dataCount > fstPageRowCount Then
                Dim remainDataCount As Integer = dataCount - fstPageRowCount
                If remainDataCount > nextPageRowCount Then
                    If remainDataCount Mod nextPageRowCount > 0 Then
                        pageCount = remainDataCount / nextPageRowCount + 2
                    Else
                        pageCount = remainDataCount / nextPageRowCount + 1
                    End If
                Else
                    pageCount = 2
                End If
            Else
                pageCount = 1
            End If

            Return pageCount
        End Function

        ''' <summary>
        ''' 和暦年を取得
        ''' </summary>
        ''' <param name="nowDate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetWarekiYear(ByVal nowDate As String) As String
            Dim nowYear As Integer
            Dim baseYear As Integer = 1989

            nowYear = CInt(nowDate.Substring(0, 4)) - baseYear + 1

            Return "平成" & nowYear.ToString & "年"
        End Function

        ''' <summary>
        ''' 注文書を出力（先頭ページ出力）
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub WriteOrderSheetFirst(ByVal xls As ShisakuExcel, ByVal outputVos As List(Of TShisakuTehaiKihonVo), _
                                         ByVal torihikisaki As String, ByVal tantosha As String, ByVal basho As String, _
                                         ByVal nowDate As String, ByVal pageCount As Integer)
            Dim startDataRow As Integer = OrderExcel.FIRST_PAGE_START_DATA_ROW
            'システム日付
            Dim sysDate As String = GetWarekiYear(nowDate) & Format(CDate(nowDate), "MM月dd日")

            'ヘッダー部
            '取引先名
            xls.SetValue(OrderExcel.FIRST_MAKER_COLUMN_INDEX, OrderExcel.FIRST_MAKER_ROW_INDEX, torihikisaki)
            '頁№
            xls.SetValue(OrderExcel.FIRST_PAGE_COLUMN_INDEX, OrderExcel.FIRST_PAGE_ROW_INDEX, "1／" & pageCount.ToString)
            '発行日
            xls.SetValue(OrderExcel.FIRST_DATE_COLUMN_INDEX, OrderExcel.FIRST_DATE_ROW_INDEX, sysDate)
            '作成者
            Dim len As Integer = tantosha.IndexOf(" ")
            If len <= 0 Then
                len = tantosha.IndexOf("　")
            End If
            If len <= 0 Then
                len = tantosha.Length
            End If
            xls.SetValue(OrderExcel.FIRST_NAME_COLUMN_INDEX, OrderExcel.FIRST_NAME_ROW_INDEX, tantosha.Substring(0, len))

            'データ部
            Dim outputCount As Integer
            If pageCount = 1 Then
                outputCount = outputVos.Count
            Else
                outputCount = OrderExcel.FIRST_PAGE_ROW_COUNT
            End If

            For index As Integer = 0 To outputCount - 1
                '№
                xls.SetValue(OrderExcel.NO_COLUMN_INDEX, startDataRow + index, index + 1)
                '工事名
                xls.SetValue(OrderExcel.KOJI_NAME_COLUMN_INDEX, startDataRow + index, _headerSubject.koujiShireiNo & outputVos(index).ShisakuBlockNo)
                '部品名称/受託業務名
                xls.SetValue(OrderExcel.BUHIN_NAME_COLUMN_INDEX, startDataRow + index, outputVos(index).BuhinName)
                '部品番号(識別)
                xls.SetValue(OrderExcel.BUHIN_NO_COLUMN_INDEX, startDataRow + index, outputVos(index).BuhinNo)
                '規　格　　　寸　度
                xls.SetValue(OrderExcel.KIKAKU_COLUMN_INDEX, startDataRow + index, String.Empty)
                '数量
                xls.SetValue(OrderExcel.QUATITY_COLUMN_INDEX, startDataRow + index, outputVos(index).TotalInsuSuryo)
                '単価(円)
                xls.SetValue(OrderExcel.TANKA_COLUMN_INDEX, startDataRow + index, String.Empty)
                '要求納入日
                Dim nounyuuDate As String = ConvDateInt8(outputVos(index).NounyuShijibi)
                If StringUtil.IsNotEmpty(nounyuuDate) Then
                    xls.SetValue(OrderExcel.NOUNYUU_DATE_COLUMN_INDEX, startDataRow + index, Format(CDate(nounyuuDate), "MM/dd"))
                Else
                    xls.SetValue(OrderExcel.NOUNYUU_DATE_COLUMN_INDEX, startDataRow + index, String.Empty)
                End If
                '納入場所
                xls.SetValue(OrderExcel.NOUNYUU_PLACE_COLUMN_INDEX, startDataRow + index, basho)
                '検査完了期日
                xls.SetValue(OrderExcel.KANRYOU_DATE_COLUMN_INDEX, startDataRow + index, String.Empty)
                '備考
                xls.SetValue(OrderExcel.BIKO_COLUMN_INDEX, startDataRow + index, String.Empty)
            Next

            'シート名
            xls.SetSheetName("1頁")
        End Sub

        ''' <summary>
        ''' 注文書を出力（次ページ出力）
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub WriteOrderSheetNext(ByVal xls As ShisakuExcel, ByVal outputVos As List(Of TShisakuTehaiKihonVo), _
                                        ByVal torihikisaki As String, ByVal basho As String, _
                                        ByVal nowDate As String, ByVal pageCount As Integer)
            Dim startDataRow As Integer = OrderExcel.NEXT_PAGE_START_DATA_ROW
            Dim perPageCount As Integer = OrderExcel.NEXT_PAGE_ROW_COUNT
            Dim pageIndex As Integer = 2
            'システム日付
            Dim sysDate As String = GetWarekiYear(nowDate) & Format(CDate(nowDate), "MM月dd日")

            'ヘッダー部
            '取引先名
            xls.SetValue(OrderExcel.NEXT_MAKER_COLUMN_INDEX, OrderExcel.NEXT_MAKER_ROW_INDEX, torihikisaki)
            '頁№
            xls.SetValue(OrderExcel.NEXT_PAGE_COLUMN_INDEX, OrderExcel.NEXT_PAGE_ROW_INDEX, "2／" & pageCount.ToString)
            '発行日
            xls.SetValue(OrderExcel.NEXT_DATE_COLUMN_INDEX, OrderExcel.NEXT_DATE_ROW_INDEX, sysDate)

            'シート名
            xls.SetSheetName("2頁")

            'データ部
            Dim rowIndex As Integer = 0
            For index As Integer = OrderExcel.FIRST_PAGE_ROW_COUNT To outputVos.Count - 1
                If rowIndex = perPageCount Then
                    'シートコピー
                    xls.SheetCopy(ShisakuCommon.ShisakuGlobal.ExcelOrderTemplate, pageIndex - 1)
                    xls.SetActiveSheet(pageIndex)
                    pageIndex = pageIndex + 1

                    'ヘッダー部
                    '取引先名
                    xls.SetValue(OrderExcel.NEXT_MAKER_COLUMN_INDEX, OrderExcel.NEXT_MAKER_ROW_INDEX, torihikisaki)
                    '頁№
                    xls.SetValue(OrderExcel.NEXT_PAGE_COLUMN_INDEX, OrderExcel.NEXT_PAGE_ROW_INDEX, pageIndex.ToString & "／" & pageCount.ToString)
                    '発行日
                    xls.SetValue(OrderExcel.NEXT_DATE_COLUMN_INDEX, OrderExcel.NEXT_DATE_ROW_INDEX, sysDate)

                    'シート名
                    xls.SetSheetName(pageIndex.ToString & "頁")

                    rowIndex = 0
                End If

                '№
                xls.SetValue(OrderExcel.NO_COLUMN_INDEX, startDataRow + rowIndex, index + 1)
                '工事名
                xls.SetValue(OrderExcel.KOJI_NAME_COLUMN_INDEX, startDataRow + rowIndex, _headerSubject.koujiShireiNo & outputVos(index).ShisakuBlockNo)
                '部品名称/受託業務名
                xls.SetValue(OrderExcel.BUHIN_NAME_COLUMN_INDEX, startDataRow + rowIndex, outputVos(index).BuhinName)
                '部品番号(識別)
                xls.SetValue(OrderExcel.BUHIN_NO_COLUMN_INDEX, startDataRow + rowIndex, outputVos(index).BuhinNo)
                '規　格　　　寸　度
                xls.SetValue(OrderExcel.KIKAKU_COLUMN_INDEX, startDataRow + rowIndex, String.Empty)
                '数量
                xls.SetValue(OrderExcel.QUATITY_COLUMN_INDEX, startDataRow + rowIndex, outputVos(index).TotalInsuSuryo)
                '単価(円)
                xls.SetValue(OrderExcel.TANKA_COLUMN_INDEX, startDataRow + rowIndex, String.Empty)
                '要求納入日
                Dim nounyuuDate As String = ConvDateInt8(outputVos(index).NounyuShijibi)
                If StringUtil.IsNotEmpty(nounyuuDate) Then
                    xls.SetValue(OrderExcel.NOUNYUU_DATE_COLUMN_INDEX, startDataRow + rowIndex, Format(CDate(nounyuuDate), "MM/dd"))
                Else
                    xls.SetValue(OrderExcel.NOUNYUU_DATE_COLUMN_INDEX, startDataRow + rowIndex, String.Empty)
                End If
                '納入場所
                xls.SetValue(OrderExcel.NOUNYUU_PLACE_COLUMN_INDEX, startDataRow + rowIndex, basho)
                '検査完了期日
                xls.SetValue(OrderExcel.KANRYOU_DATE_COLUMN_INDEX, startDataRow + rowIndex, String.Empty)
                '備考
                xls.SetValue(OrderExcel.BIKO_COLUMN_INDEX, startDataRow + rowIndex, String.Empty)

                rowIndex = rowIndex + 1
            Next
        End Sub

#End Region
        ''↑↑↑2015/01/22 TES)張 ADD END

#Region "JAVAで図面情報を取得"

        ''' <summary>
        ''' JAVAで図面情報を取得する
        ''' </summary>
        ''' <remarks></remarks>
        Public Function UpdateZumen()
            '新規列が不明なのでまずはそのまま'

            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet) 'シートの開始行位置

            Dim buhinNoList As New List(Of String)  'Java用の部品番号リスト(重複番号無し)
            Dim buhinNoAndBlockNoList As New List(Of TShisakuTehaiKihonVo) '行番号で部品とブロックを格納

            '部品番号のリストを作成'

            For rowIndex As Integer = startRow To sheet.RowCount - 1
                Dim buhinNo As String = sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO)).Trim
                Dim blockNo As String = sheet.GetText(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim

                If StringUtil.IsNotEmpty(buhinNo) Then
                    Dim addVo As New TShisakuTehaiKihonVo
                    addVo.BuhinNo = buhinNo
                    addVo.ShisakuBlockNo = blockNo

                    buhinNoAndBlockNoList.Add(addVo)

                    If Not buhinNoList.Contains(buhinNo) Then
                        buhinNoList.Add(buhinNo)
                    End If

                End If
            Next

            '部品番号のリストから図面設通情報を取得'
            Dim dao As TehaichoHeaderDao = New TehaichoHeaderDaoImpl
            Dim zList As List(Of Zspf10Vo) = dao.FindByBuhin(buhinNoList)


            '既に登録されているかチェックして未登録情報のみ登録する。'
            Dim _ResultList As New List(Of TShisakuTehaiShutuzuJisekiVo)
            _ResultList = MakeRegisteredDataList(zList, buhinNoAndBlockNoList)

            Return _ResultList

            '下記は一旦コメントアウト

            ''既に登録されているかチェックして未登録情報のみ登録する。'
            ''更新処理は行う？その条件は？'
            ''一気に書かせてみる'
            'InsertTShisakuTehaiShutuzuJisseki(MakeRegisteredDataList(zList, buhinNoAndBlockNoList))

        End Function

        ''' <summary>
        ''' 図面情報登録処理
        ''' </summary>
        ''' <param name="registeredDataList">登録用情報リスト</param>
        ''' <remarks></remarks>
        Public Sub InsertTShisakuTehaiShutuzuJisseki(ByVal registeredDataList As List(Of TShisakuTehaiShutuzuJisekiVo))

            If Not registeredDataList Is Nothing Then
                'キー違反が起きない前提でなら高速対応可能'
                Using sqlConn As New SqlClient.SqlConnection(NitteiDbComFunc.GetConnectString)
                    sqlConn.Open()
                    Using tr As SqlClient.SqlTransaction = sqlConn.BeginTransaction

                        Using addData As DataTable = NitteiDbComFunc.ConvListToDataTable(registeredDataList)
                            Using bulkCopy As SqlClient.SqlBulkCopy = New SqlClient.SqlBulkCopy(sqlConn, SqlClient.SqlBulkCopyOptions.KeepIdentity, tr)
                                'マッピングが必要
                                NitteiDbComFunc.SetColumnMappings(bulkCopy, addData)

                                'タイムアウト制限
                                bulkCopy.BulkCopyTimeout = 0  ' in seconds
                                '書き込み先指定
                                bulkCopy.DestinationTableName = MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_JISEKI"
                                'ここで書き込み
                                bulkCopy.WriteToServer(addData)
                                bulkCopy.Close()
                            End Using
                        End Using
                        tr.Commit()
                    End Using
                End Using
            End If


        End Sub

        ''' <summary>
        ''' 登録用データを作成して返す。
        ''' </summary>
        ''' <param name="zList">図面情報リスト</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MakeRegisteredDataList(ByVal zList As List(Of Zspf10Vo), _
                                                ByVal buhinNoAndBlockNoList As List(Of TShisakuTehaiKihonVo)) As List(Of TShisakuTehaiShutuzuJisekiVo)
            Dim result As New List(Of TShisakuTehaiShutuzuJisekiVo)
            Dim aDate As New ShisakuDate
            Dim keys As New List(Of String)

            '登録用データ'
            Dim dic As New Dictionary(Of String, List(Of Zspf10Vo))
            For Each vo As Zspf10Vo In zList
                If Not dic.ContainsKey(vo.Gzzcp) Then
                    dic.Add(vo.Gzzcp, New List(Of Zspf10Vo))
                End If
                dic(vo.Gzzcp).Add(vo)
            Next

            '比較用データ'
            Dim dao As TehaichoHeaderDao = New TehaichoHeaderDaoImpl
            Dim bDic As New Dictionary(Of String, Dictionary(Of String, Zspf10Vo))
            Dim bList As List(Of TShisakuTehaiShutuzuJisekiVo) = dao.FindByTShisakuTehaiShutuzuJisseki(_shisakuEventCode, _shisakuListCode, _headerSubject.shisakuListCodeKaiteiNo)

            For Each vo As TShisakuTehaiShutuzuJisekiVo In bList

                Dim key As String = EzUtil.MakeKey(vo.ShisakuEventCode, _
                                                   vo.ShisakuListCode, _
                                                   vo.ShisakuListCodeKaiteiNo, _
                                                   vo.ShisakuBlockNo, _
                                                   vo.BuhinNo, _
                                                   vo.ShutuzuJisekiKaiteiNo)
                '重複させない'
                If Not keys.Contains(key) Then
                    '登録用情報がすでに登録済みなら登録対象外とする'
                    keys.Add(key)
                End If
            Next



            For Each vo As TShisakuTehaiKihonVo In buhinNoAndBlockNoList
                If dic.ContainsKey(vo.BuhinNo) Then

                    For Each zVo As Zspf10Vo In dic(vo.BuhinNo)
                        Dim addVo As New TShisakuTehaiShutuzuJisekiVo
                        addVo.ShisakuEventCode = _shisakuEventCode                              '試作イベントコード
                        addVo.ShisakuListCode = _shisakuListCode                                '試作リストコード
                        addVo.ShisakuListCodeKaiteiNo = _headerSubject.shisakuListCodeKaiteiNo  '試作リストコード改訂№
                        addVo.ShisakuBlockNo = vo.ShisakuBlockNo                                '試作ブロック№
                        addVo.BuhinNo = vo.BuhinNo                                              '部品番号

                        addVo.ShutuzuJisekiKaiteiNo = zVo.Tzkdbap                   '図面改訂№

                        addVo.KataDaihyouBuhinNo = zVo.Tzzmbap                      'タイトル図面
                        addVo.ShutuzuJisekiJyuryoDate = zVo.Kxjrdt                  '受領日
                        'addVo.ShutuzuJisekiStsrDhstba = zVo.Dhstba                  '設通№
                        addVo.ShutuzuJisekiStsrDhstba = zVo.Stsr & " " & zVo.Dhstba '設通シリーズ 設通No
                        addVo.ShutuzuKenmei = zVo.Kdri                              '件名
                        addVo.Comment = ""                                                      'コメント
                        addVo.CreatedUserId = LoginInfo.Now.UserId
                        addVo.CreatedDate = aDate.CurrentDateDbFormat
                        addVo.CreatedTime = aDate.CurrentTimeDbFormat
                        addVo.UpdatedUserId = LoginInfo.Now.UserId
                        addVo.UpdatedDate = aDate.CurrentDateDbFormat
                        addVo.UpdatedTime = aDate.CurrentTimeDbFormat


                        Dim key As String = EzUtil.MakeKey(addVo.ShisakuEventCode, _
                                   addVo.ShisakuListCode, _
                                   addVo.ShisakuListCodeKaiteiNo, _
                                   addVo.ShisakuBlockNo, _
                                   addVo.BuhinNo, _
                                   addVo.ShutuzuJisekiKaiteiNo)
                        '重複させない'
                        If Not keys.Contains(key) Then
                            '登録用情報がすでに登録済みなら登録対象外'
                            result.Add(addVo)
                            keys.Add(key)
                        End If

                    Next
                End If
            Next
            Return result
        End Function

#End Region

    End Class

#End Region

End Namespace
