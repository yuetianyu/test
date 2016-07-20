Imports System
Imports EBom.Data
Imports EBom.Common
Imports FarPoint.Win
Imports EventSakusei.YosanSetteiBuhinEdit.Dao
Imports System.Text.RegularExpressions
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports EventSakusei.YosanSetteiBuhinEdit.Logic
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
Imports FarPoint.Win.Spread.Model


Namespace YosanSetteiBuhinEdit

#Region "手配帳編集画面制御ロジック"

    ''' <summary>
    ''' 画面制御ロジック
    ''' </summary>
    ''' <remarks></remarks>
    Public Class YosanSetteiBuhinEditLogic

#Region "プレイベート変数"

        Private Const _TITLE_BASE As String = "基本情報"
        Private Const _BLANK_NAME As String = "　"

        '''<summary>シート取込バックアップ名称号車</summary>>
        Private Const _BACKUP_SHEET_NAME_BASE As String = "BackupBase"
        '''<summary>前画面引継ぎ項目</summary>>
        Private _shisakuEventCode As String
        Private _shisakuListCode As String
        '''<summary>ヘッダー情報格納</summary>>
        Private _headerSubject As YosanSetteiBuhinEditHeader
        ''' <summary>試作手帳編集表示フォーム</summary>
        Private _frmDispTehaiEdit As frmDispYosanSetteiBuhinEdit
        ''' <summary>試作イベントデータテーブル</summary>
        Private _dtShikuEvent As DataTable
        ''' <summary>スプレッドに対して編集が行われたブロックリストを格納 </summary>
        Private _listEditBlock As List(Of String()) = New List(Of String())
        ''' <summary>スクロール行同期管理 </summary>
        Private _rowNoScroll As Integer = -1

        Private aDate As ShisakuDate

        Private _RirekiVos As List(Of TYosanSetteiBuhinRirekiVo)
        ''' <summary>行と履歴Vo </summary>
        Private _RirekiDic As Dictionary(Of Integer, Dictionary(Of String, List(Of TYosanSetteiBuhinRirekiVo)))


#Region "EXCEL出力・取込情報"
        ''' <summary>Excel出力でのタイトル行数</summary>
        Private Const EXCEL_TITLE_LINE_COUNT As Integer = 4
        ''' <summary>Excel取込でのデータ開始位置 （Excelﾀｲﾄﾙ行数 + ｽﾌﾟﾚｯﾄﾞﾀｲﾄﾙ行 + 1 ?</summary>
        Private Const EXCEL_IMPORT_DATA_START_ROW As Integer = 8

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

#End Region

#Region "コンストラクタ"
        Public Sub New(ByVal aShisakuEventCode As String, _
                                ByVal aShisakuListCode As String, _
                                ByVal frm As frmDispYosanSetteiBuhinEdit)

            '前画面引継
            _shisakuEventCode = aShisakuEventCode
            _shisakuListCode = aShisakuListCode

            '手配帳編集画面
            _frmDispTehaiEdit = frm

            '表示更新
            _frmDispTehaiEdit.Refresh()
            _RirekiVos = New List(Of TYosanSetteiBuhinRirekiVo)
            _RirekiDic = New Dictionary(Of Integer, Dictionary(Of String, List(Of TYosanSetteiBuhinRirekiVo)))
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

                        Dim bukaCode As String = YosanSetteiBuhinEditImpl.FindBukaCode(aBlockNo)
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
            Dim frm As YosanSetteiBuhinEdit.frmDispYosanSetteiBuhinEdit = _frmDispTehaiEdit

            'ヘッダー部分の初期設定'
            _headerSubject = New Logic.YosanSetteiBuhinEditHeader(_shisakuEventCode, _shisakuListCode)
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

            '表示更新
            _frmDispTehaiEdit.Refresh()

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
            '常時非表示列の設定
            SetHiddenCol()
            HiddenPastParchase(False)

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

        End Sub

#End Region

#Region "ヘッダー部の表示値を更新する"

        ''' <summary>
        ''' ヘッダー部の表示値を更新する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub UpdateHeader()

            Dim frm As YosanSetteiBuhinEdit.frmDispYosanSetteiBuhinEdit = _frmDispTehaiEdit

            'ブロックNo頭出しコンボボックス
            FormUtil.BindLabelValuesToComboBox(frm.cmbBlockNo, _headerSubject.BlockNoLabelValues, True)
            FormUtil.SetComboBoxSelectedValue(frm.cmbBlockNo, _headerSubject.blockNo)

            frm.lblListCode.Text = _headerSubject.shisakuListCode
            frm.lblIbentoName.Text = _headerSubject.kaihatsuFugo + "  " + _headerSubject.listEventName
            frm.lblKoujishireiNo.Text = _headerSubject.koujiShireiNo
            frm.lblMTkubun.Text = _headerSubject.MTKbn
            frm.lblSeihinKubun.Text = _headerSubject.seihinKbn
            frm.txtKoujiNo.Text = _headerSubject.yosanCode

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

            'ロックしない
            sheet.Columns(NmSpdTagBase.TAG_AUD_FLAG).Locked = False
            sheet.Columns(NmSpdTagBase.TAG_YOSAN_BLOCK_NO).Locked = False
            sheet.Columns(NmSpdTagBase.TAG_YOSAN_LEVEL).Locked = False
            sheet.Columns(NmSpdTagBase.TAG_YOSAN_SHUKEI_CODE).Locked = False
            sheet.Columns(NmSpdTagBase.TAG_YOSAN_INSU).Locked = False

            '下記の列をロックする
            sheet.Columns(NmSpdTagBase.TAG_YOSAN_GYOU_ID).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_HENKATEN).Locked = True

            sheet.Columns(NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI).Locked = True

            sheet.Columns(NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_TANKA).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI_TOTAL).Locked = True
            '
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_RYOSAN_TANKA).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_WARITUKE_BUHIN_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_WARITUKE_KATA_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_WARITUKE_KOUHOU).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_MAKER_BUHIN_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_MAKER_KATA_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_MAKER_KOUHOU).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_SHINGI_BUHIN_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_SHINGI_KATA_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_SHINGI_KOUHOU).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_KOUNYU_KIBOU_TANKA).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_KOUNYU_TANKA).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_SHIKYU_HIN).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_KOUJI_SHIREI_NO).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_EVENT_NAME).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_HACHU_BI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_KENSHU_BI).Locked = True
            '
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_RYOSAN_TANKA).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_WARITUKE_BUHIN_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_WARITUKE_KATA_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_WARITUKE_KOUHOU).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_MAKER_BUHIN_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_MAKER_KATA_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_MAKER_KOUHOU).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_SHINGI_BUHIN_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_SHINGI_KATA_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_SHINGI_KOUHOU).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_KOUNYU_KIBOU_TANKA).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_KOUNYU_TANKA).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_SHIKYU_HIN).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_KOUJI_SHIREI_NO).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_EVENT_NAME).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_HACHU_BI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_KENSHU_BI).Locked = True
            '
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_RYOSAN_TANKA).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_WARITUKE_BUHIN_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_WARITUKE_KATA_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_WARITUKE_KOUHOU).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_MAKER_BUHIN_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_MAKER_KATA_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_MAKER_KOUHOU).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_SHINGI_BUHIN_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_SHINGI_KATA_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_SHINGI_KOUHOU).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_KOUNYU_KIBOU_TANKA).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_KOUNYU_TANKA).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_SHIKYU_HIN).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_KOUJI_SHIREI_NO).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_EVENT_NAME).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_HACHU_BI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_KENSHU_BI).Locked = True
            '
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_RYOSAN_TANKA).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_WARITUKE_BUHIN_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_WARITUKE_KATA_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_WARITUKE_KOUHOU).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_MAKER_BUHIN_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_MAKER_KATA_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_MAKER_KOUHOU).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_SHINGI_BUHIN_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_SHINGI_KATA_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_SHINGI_KOUHOU).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_KOUNYU_KIBOU_TANKA).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_KOUNYU_TANKA).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_SHIKYU_HIN).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_KOUJI_SHIREI_NO).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_EVENT_NAME).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_HACHU_BI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_KENSHU_BI).Locked = True
            '
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_RYOSAN_TANKA).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_WARITUKE_BUHIN_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_WARITUKE_KATA_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_WARITUKE_KOUHOU).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_MAKER_BUHIN_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_MAKER_KATA_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_MAKER_KOUHOU).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_SHINGI_BUHIN_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_SHINGI_KATA_HI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_SHINGI_KOUHOU).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_KOUNYU_KIBOU_TANKA).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_KOUNYU_TANKA).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_SHIKYU_HIN).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_KOUJI_SHIREI_NO).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_EVENT_NAME).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_HACHU_BI).Locked = True
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_KENSHU_BI).Locked = True

        End Sub

#End Region

#Region "試作イベント情報取得"
        Private Sub InitDtShisakuEvent()

            '試作イベント情報を取得
            Dim dtEventInfo As DataTable = YosanSetteiBuhinEditImpl.FindPkShisakuEvent(_shisakuEventCode)

            '取得出来ない場合は抜ける
            If dtEventInfo.Rows.Count = 0 Then
                Throw New Exception("試作イベント情報の取得ができませんでした。処理を終了します。")
                Return
            End If

            _dtShikuEvent = dtEventInfo

        End Sub

#End Region

#Region "コンボボックス生成"

#Region "作り方製作方法"
        ''' <summary>
        ''' 作り方製作方法
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AddCbTsukurikataSeisaku()

            '集計マスタからデータ取得
            Dim dtTsukurikata As DataTable
            dtTsukurikata = YosanSetteiBuhinEditImpl.FindAllTsukurikataSeisaku()

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
            'cb.MaxLength = 1
            cb.MaxLength = 32
            '' 入力時、前方一致
            cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cb.AutoCompleteSource = AutoCompleteSource.ListItems
            '' 小文字を大文字にする
            cb.CharacterCasing = CharacterCasing.Upper
            '' 半角英数記号(Asciiだと全角も入力出来る)
            cb.CharacterSet = CellType.CharacterSet.Ascii
            '' 編集可能にする
            cb.Editable = True

            '空白をセット
            keys.Add(String.Empty)
            vals.Add(String.Empty)

            For i As Integer = 0 To dtTsukurikata.Rows.Count - 1
                keys.Add(dtTsukurikata.Rows(i)(NmTsukurikata.TSUKURIKATA_NAME))
            Next

            'キーも表示も集計コードとする
            cb.ItemData = keys.ToArray
            cb.Items = keys.ToArray

            _frmDispTehaiEdit.spdBase_Sheet1.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_SEISAKU).CellType = cb

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
            dtTsukurikata = YosanSetteiBuhinEditImpl.FindAllTsukurikataKatashiyou()

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
            'cb.MaxLength = 1
            cb.MaxLength = 32
            '' 入力時、前方一致
            cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cb.AutoCompleteSource = AutoCompleteSource.ListItems
            '' 小文字を大文字にする
            cb.CharacterCasing = CharacterCasing.Upper
            '' 半角英数記号(Asciiだと全角も入力出来る)
            cb.CharacterSet = CellType.CharacterSet.Ascii
            '' 編集可能にする
            cb.Editable = True

            '空白をセット
            keys.Add(String.Empty)
            vals.Add(String.Empty)

            For i As Integer = 0 To dtTsukurikata.Rows.Count - 1
                keys.Add(dtTsukurikata.Rows(i)(NmTsukurikata.TSUKURIKATA_NAME))
            Next

            'キーも表示も集計コードとする
            cb.ItemData = keys.ToArray
            cb.Items = keys.ToArray

            _frmDispTehaiEdit.spdBase_Sheet1.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_1).CellType = cb
            _frmDispTehaiEdit.spdBase_Sheet1.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_2).CellType = cb
            _frmDispTehaiEdit.spdBase_Sheet1.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_3).CellType = cb

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
            dtTsukurikata = YosanSetteiBuhinEditImpl.FindAllTsukurikataTigu()

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
            cb.MaxLength = 32
            '' 入力時、前方一致
            cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cb.AutoCompleteSource = AutoCompleteSource.ListItems
            '' 小文字を大文字にする
            cb.CharacterCasing = CharacterCasing.Upper
            '' 半角英数記号(Asciiだと全角も入力出来る)
            cb.CharacterSet = CellType.CharacterSet.Ascii
            '' 編集可能にする
            cb.Editable = True

            '空白をセット
            keys.Add(String.Empty)
            vals.Add(String.Empty)

            For i As Integer = 0 To dtTsukurikata.Rows.Count - 1
                keys.Add(dtTsukurikata.Rows(i)(NmTsukurikata.TSUKURIKATA_NAME))
            Next

            'キーも表示も集計コードとする
            cb.ItemData = keys.ToArray
            cb.Items = keys.ToArray

            _frmDispTehaiEdit.spdBase_Sheet1.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_TIGU).CellType = cb

        End Sub

#End Region

#Region "新規ブロックNoリスト"
        ''' <summary>
        ''' 新規ブロックNoリスト
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitCbNewBlockList()
            Dim dtBlockList As New DataTable
            _frmDispTehaiEdit.cmbNewBlockNo.Items.Clear()
            dtBlockList = YosanSetteiBuhinEditImpl.FindAllListBlockNo()
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
        ''' <param name="aBlockNo"></param>
        ''' <remarks></remarks>
        Public Sub Spread_BlockNo_FocusChange(ByVal aBlockNo As String)

            Dim sheetBase As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1

            Dim resRow As Integer = -1

            For i As Integer = GetTitleRowsIn(sheetBase) To sheetBase.RowCount - 1
                Dim spdBlockNo As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Trim

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
            sheetBase.SetActiveCell(resRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_BLOCK_NO))

            _frmDispTehaiEdit.spdBase.ShowActiveCell(Spread.VerticalPosition.Top, Spread.HorizontalPosition.Left)

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

#Region "基本情報スプレッドデータ格納"
        ''' <summary>
        ''' 基本情報スプレッドデータ格納
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetSpreadBase() As Boolean

            Dim dtBase As DataTable = YosanSetteiBuhinEditImpl.FindAllBaseInfo _
                        (_shisakuEventCode, _shisakuListCode)

            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            'スプレッドを他の画面と同様に設定
            Dim spread As FarPoint.Win.Spread.FpSpread = _frmDispTehaiEdit.spdBase
            SpreadUtil.Initialize(spread)

            '取得したデータ件数によりスプレッドの総行数を拡張(+10は必要余裕行数）
            If dtBase.Rows.Count >= sheet.RowCount Then
                sheet.RowCount = dtBase.Rows.Count + 10
            End If

            '全データ消去
            SpreadAllClear(sheet)

            '履歴情報設定'
            Dim dao As YosanSetteiBuhinEditHeaderDao = New YosanSetteiBuhinEditHeaderDaoImpl
            _RirekiVos = dao.FindByTYosanSetteiBuhinRireki(_shisakuEventCode, _shisakuListCode)
            Dim dic As New Dictionary(Of String, Dictionary(Of String, List(Of TYosanSetteiBuhinRirekiVo)))
            For Each vo As TYosanSetteiBuhinRirekiVo In _RirekiVos
                Dim key As String = EzUtil.MakeKey(vo.YosanBlockNo, vo.YosanBuhinNo, vo.BuhinNoHyoujiJun)

                If Not dic.ContainsKey(key) Then
                    dic.Add(key, New Dictionary(Of String, List(Of TYosanSetteiBuhinRirekiVo)))
                End If

                If Not dic(key).ContainsKey(vo.ColumnName) Then
                    dic(key).Add(vo.ColumnName, New List(Of TYosanSetteiBuhinRirekiVo))
                End If

                dic(key)(vo.ColumnName).Add(vo)
            Next

            Dim oRow As Integer = startRow
            For Each dtRow As DataRow In dtBase.Rows

                'Dim rireki As String = IIf(IsDBNull(dtRow(NmDTColBase.TD_RIREKI)), String.Empty, dtRow(NmDTColBase.TD_RIREKI))
                ''履歴の値によりセルの編集ロックを行う
                'If rireki.Trim.Equals("*") Then
                '    LockCellRirekiBase(oRow, True)
                'End If
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_AUD_FLAG)).Locked = True

                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_AUD_FLAG)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_AUD_FLAG)), _
                                                String.Empty, dtRow(NmDTColBase.TD_AUD_FLAG))

                '削除扱いならグレーにする'
                If StringUtil.Equals(sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_AUD_FLAG)).Value, "D") Then
                    sheet.Rows(oRow).BackColor = Color.Gray
                    sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_AUD_FLAG)).Locked = False
                End If
                '追加扱いならAquaにする'
                If StringUtil.Equals(sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_AUD_FLAG)).Value, "A") Then
                    sheet.Rows(oRow).BackColor = Color.Aqua
                End If



                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_BUKA_CODE)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_BUKA_CODE)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_BUKA_CODE))
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_BLOCK_NO)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_BLOCK_NO))
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_GYOU_ID)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_GYOU_ID)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_GYOU_ID))
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_LEVEL)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_LEVEL)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_LEVEL))
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN)), _
                                                String.Empty, dtRow(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN))

                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_SHUKEI_CODE)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_SHUKEI_CODE)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_SHUKEI_CODE))
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_SIA_SHUKEI_CODE)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_SIA_SHUKEI_CODE)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_SIA_SHUKEI_CODE))
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_BUHIN_NO)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_BUHIN_NO))
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_BUHIN_NAME)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_BUHIN_NAME)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_BUHIN_NAME).ToString.Trim)
                '合計員数-1は**表記とし、0値は０を設定
                Dim totalInsu As String = CnvIntStr(dtRow(NmDTColBase.TD_YOSAN_INSU))
                '-1が来たら**を設定
                totalInsu = IIf(totalInsu.Equals("-1"), "**", totalInsu)

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_INSU)).Value = totalInsu
                '
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_MAKER_CODE)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_MAKER_CODE)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_MAKER_CODE))
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_KYOUKU_SECTION)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_KYOUKU_SECTION)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_KYOUKU_SECTION))
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_KOUTAN)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_KOUTAN)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_KOUTAN))
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_TEHAI_KIGOU)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_TEHAI_KIGOU)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_TEHAI_KIGOU))
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_SEISAKU)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_TSUKURIKATA_SEISAKU)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_TSUKURIKATA_SEISAKU))
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_1)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_TSUKURIKATA_KATASHIYOU_1)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_TSUKURIKATA_KATASHIYOU_1))
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_2)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_TSUKURIKATA_KATASHIYOU_2)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_TSUKURIKATA_KATASHIYOU_2))
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_3)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_TSUKURIKATA_KATASHIYOU_3)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_TSUKURIKATA_KATASHIYOU_3))
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_TIGU)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_TSUKURIKATA_TIGU)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_TSUKURIKATA_TIGU))
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KIBO)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_TSUKURIKATA_KIBO)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_TSUKURIKATA_KIBO))
                '設計情報_試作部品費（円）
                Dim strBuhinHi As String = GetNumericDbField(dtRow(NmDTColBase.TD_YOSAN_SHISAKU_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_SHISAKU_BUHIN_HI)).Text = strBuhinHi
                '設計情報_試作型費（千円）
                Dim strKatahi As String = GetNumericDbField(dtRow(NmDTColBase.TD_YOSAN_SHISAKU_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_SHISAKU_KATA_HI)).Value = strKatahi
                '設計情報_部品ノート
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_BUHIN_NOTE)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_BUHIN_NOTE)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_BUHIN_NOTE))
                '設計情報_備考
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_BIKOU)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_BIKOU)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_BIKOU))
                '部品費根拠_国外区分
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_KONKYO_KOKUGAI_KBN)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_KONKYO_KOKUGAI_KBN))
                '部品費根拠_MIXコスト部品費(円/ｾﾝﾄ)
                Dim strKonkyoMixBuhinHi As String = GetNumericDbField(dtRow(NmDTColBase.TD_YOSAN_KONKYO_MIX_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI)).Value = strKonkyoMixBuhinHi
                '部品費根拠_引用元MIX値部品費
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI))
                '部品費根拠_係数１
                Dim strYosanKonkyoKeisu1 As String = GetNumericDbField(dtRow(NmDTColBase.TD_YOSAN_KONKYO_KEISU_1))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KEISU_1)).Value = strYosanKonkyoKeisu1
                '部品費根拠_工法
                sheet.Cells(oRow, GetTagIdx(sheet, _
                                            NmSpdTagBase.TAG_YOSAN_KONKYO_KOUHOU)).Value = _
                                            IIf(IsDBNull(dtRow(NmDTColBase.TD_YOSAN_KONKYO_KOUHOU)), _
                                                String.Empty, dtRow(NmDTColBase.TD_YOSAN_KONKYO_KOUHOU))
                '割付予算_部品費(円)
                Dim strYosanWaritukeBuhinHi As String = GetNumericDbField(dtRow(NmDTColBase.TD_YOSAN_WARITUKE_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI)).Value = strYosanWaritukeBuhinHi
                '割付予算_係数２
                Dim strYosanWaritukeKeisu2 As String = GetNumericDbField(dtRow(NmDTColBase.TD_YOSAN_WARITUKE_KEISU_2))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_KEISU_2)).Value = strYosanWaritukeKeisu2

                '過去購入部品1
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_RYOSAN_TANKA)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_1_RYOSAN_TANKA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_WARITUKE_BUHIN_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_1_WARITUKE_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_WARITUKE_KATA_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_1_WARITUKE_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_WARITUKE_KOUHOU)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_1_WARITUKE_KOUHOU)), String.Empty, dtRow(NmDTColBase.TD_KAKO_1_WARITUKE_KOUHOU))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_MAKER_BUHIN_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_1_MAKER_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_MAKER_KATA_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_1_MAKER_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_MAKER_KOUHOU)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_1_MAKER_KOUHOU)), String.Empty, dtRow(NmDTColBase.TD_KAKO_1_MAKER_KOUHOU))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_SHINGI_BUHIN_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_1_SHINGI_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_SHINGI_KATA_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_1_SHINGI_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_SHINGI_KOUHOU)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_1_SHINGI_KOUHOU)), String.Empty, dtRow(NmDTColBase.TD_KAKO_1_SHINGI_KOUHOU))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_KOUNYU_KIBOU_TANKA)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_1_KOUNYU_KIBOU_TANKA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_KOUNYU_TANKA)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_1_KOUNYU_TANKA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_SHIKYU_HIN)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_1_SHIKYU_HIN))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_KOUJI_SHIREI_NO)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_1_KOUJI_SHIREI_NO)), String.Empty, dtRow(NmDTColBase.TD_KAKO_1_KOUJI_SHIREI_NO))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_EVENT_NAME)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_1_EVENT_NAME)), String.Empty, dtRow(NmDTColBase.TD_KAKO_1_EVENT_NAME))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_HACHU_BI)).Value = _
                                YosanSetteiBuhinEditLogic.ConvDateInt8(IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_1_HACHU_BI)), 0, _
                                                                           dtRow(NmDTColBase.TD_KAKO_1_HACHU_BI)))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_KENSHU_BI)).Value = _
                                YosanSetteiBuhinEditLogic.ConvDateInt8(IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_1_KENSHU_BI)), 0, _
                                                                           dtRow(NmDTColBase.TD_KAKO_1_KENSHU_BI)))

                '過去購入部品2
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_RYOSAN_TANKA)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_2_RYOSAN_TANKA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_WARITUKE_BUHIN_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_2_WARITUKE_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_WARITUKE_KATA_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_2_WARITUKE_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_WARITUKE_KOUHOU)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_2_WARITUKE_KOUHOU)), String.Empty, dtRow(NmDTColBase.TD_KAKO_2_WARITUKE_KOUHOU))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_MAKER_BUHIN_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_2_MAKER_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_MAKER_KATA_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_2_MAKER_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_MAKER_KOUHOU)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_2_MAKER_KOUHOU)), String.Empty, dtRow(NmDTColBase.TD_KAKO_2_MAKER_KOUHOU))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_SHINGI_BUHIN_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_2_SHINGI_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_SHINGI_KATA_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_2_SHINGI_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_SHINGI_KOUHOU)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_2_SHINGI_KOUHOU)), String.Empty, dtRow(NmDTColBase.TD_KAKO_2_SHINGI_KOUHOU))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_KOUNYU_KIBOU_TANKA)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_2_KOUNYU_KIBOU_TANKA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_KOUNYU_TANKA)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_2_KOUNYU_TANKA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_SHIKYU_HIN)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_2_SHIKYU_HIN))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_KOUJI_SHIREI_NO)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_2_KOUJI_SHIREI_NO)), String.Empty, dtRow(NmDTColBase.TD_KAKO_2_KOUJI_SHIREI_NO))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_EVENT_NAME)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_2_EVENT_NAME)), String.Empty, dtRow(NmDTColBase.TD_KAKO_2_EVENT_NAME))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_HACHU_BI)).Value = _
                                YosanSetteiBuhinEditLogic.ConvDateInt8(IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_2_HACHU_BI)), 0, _
                                                                           dtRow(NmDTColBase.TD_KAKO_2_HACHU_BI)))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_KENSHU_BI)).Value = _
                                YosanSetteiBuhinEditLogic.ConvDateInt8(IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_2_KENSHU_BI)), 0, _
                                                                           dtRow(NmDTColBase.TD_KAKO_2_KENSHU_BI)))

                '過去購入部品3
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_RYOSAN_TANKA)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_3_RYOSAN_TANKA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_WARITUKE_BUHIN_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_3_WARITUKE_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_WARITUKE_KATA_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_3_WARITUKE_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_WARITUKE_KOUHOU)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_3_WARITUKE_KOUHOU)), String.Empty, dtRow(NmDTColBase.TD_KAKO_3_WARITUKE_KOUHOU))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_MAKER_BUHIN_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_3_MAKER_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_MAKER_KATA_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_3_MAKER_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_MAKER_KOUHOU)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_3_MAKER_KOUHOU)), String.Empty, dtRow(NmDTColBase.TD_KAKO_3_MAKER_KOUHOU))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_SHINGI_BUHIN_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_3_SHINGI_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_SHINGI_KATA_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_3_SHINGI_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_SHINGI_KOUHOU)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_3_SHINGI_KOUHOU)), String.Empty, dtRow(NmDTColBase.TD_KAKO_3_SHINGI_KOUHOU))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_KOUNYU_KIBOU_TANKA)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_3_KOUNYU_KIBOU_TANKA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_KOUNYU_TANKA)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_3_KOUNYU_TANKA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_SHIKYU_HIN)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_3_SHIKYU_HIN))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_KOUJI_SHIREI_NO)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_3_KOUJI_SHIREI_NO)), String.Empty, dtRow(NmDTColBase.TD_KAKO_3_KOUJI_SHIREI_NO))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_EVENT_NAME)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_3_EVENT_NAME)), String.Empty, dtRow(NmDTColBase.TD_KAKO_3_EVENT_NAME))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_HACHU_BI)).Value = _
                                YosanSetteiBuhinEditLogic.ConvDateInt8(IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_3_HACHU_BI)), 0, _
                                                                           dtRow(NmDTColBase.TD_KAKO_3_HACHU_BI)))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_KENSHU_BI)).Value = _
                                YosanSetteiBuhinEditLogic.ConvDateInt8(IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_3_KENSHU_BI)), 0, _
                                                                           dtRow(NmDTColBase.TD_KAKO_3_KENSHU_BI)))

                '過去購入部品4
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_RYOSAN_TANKA)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_4_RYOSAN_TANKA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_WARITUKE_BUHIN_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_4_WARITUKE_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_WARITUKE_KATA_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_4_WARITUKE_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_WARITUKE_KOUHOU)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_4_WARITUKE_KOUHOU)), String.Empty, dtRow(NmDTColBase.TD_KAKO_4_WARITUKE_KOUHOU))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_MAKER_BUHIN_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_4_MAKER_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_MAKER_KATA_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_4_MAKER_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_MAKER_KOUHOU)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_4_MAKER_KOUHOU)), String.Empty, dtRow(NmDTColBase.TD_KAKO_4_MAKER_KOUHOU))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_SHINGI_BUHIN_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_4_SHINGI_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_SHINGI_KATA_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_4_SHINGI_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_SHINGI_KOUHOU)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_4_SHINGI_KOUHOU)), String.Empty, dtRow(NmDTColBase.TD_KAKO_4_SHINGI_KOUHOU))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_KOUNYU_KIBOU_TANKA)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_4_KOUNYU_KIBOU_TANKA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_KOUNYU_TANKA)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_4_KOUNYU_TANKA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_SHIKYU_HIN)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_4_SHIKYU_HIN))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_KOUJI_SHIREI_NO)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_4_KOUJI_SHIREI_NO)), String.Empty, dtRow(NmDTColBase.TD_KAKO_4_KOUJI_SHIREI_NO))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_EVENT_NAME)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_4_EVENT_NAME)), String.Empty, dtRow(NmDTColBase.TD_KAKO_4_EVENT_NAME))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_HACHU_BI)).Value = _
                                YosanSetteiBuhinEditLogic.ConvDateInt8(IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_4_HACHU_BI)), 0, _
                                                                           dtRow(NmDTColBase.TD_KAKO_4_HACHU_BI)))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_KENSHU_BI)).Value = _
                                YosanSetteiBuhinEditLogic.ConvDateInt8(IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_4_KENSHU_BI)), 0, _
                                                                           dtRow(NmDTColBase.TD_KAKO_4_KENSHU_BI)))

                '過去購入部品5
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_RYOSAN_TANKA)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_5_RYOSAN_TANKA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_WARITUKE_BUHIN_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_5_WARITUKE_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_WARITUKE_KATA_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_5_WARITUKE_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_WARITUKE_KOUHOU)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_5_WARITUKE_KOUHOU)), String.Empty, dtRow(NmDTColBase.TD_KAKO_5_WARITUKE_KOUHOU))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_MAKER_BUHIN_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_5_MAKER_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_MAKER_KATA_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_5_MAKER_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_MAKER_KOUHOU)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_5_MAKER_KOUHOU)), String.Empty, dtRow(NmDTColBase.TD_KAKO_5_MAKER_KOUHOU))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_SHINGI_BUHIN_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_5_SHINGI_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_SHINGI_KATA_HI)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_5_SHINGI_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_SHINGI_KOUHOU)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_5_SHINGI_KOUHOU)), String.Empty, dtRow(NmDTColBase.TD_KAKO_5_SHINGI_KOUHOU))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_KOUNYU_KIBOU_TANKA)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_5_KOUNYU_KIBOU_TANKA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_KOUNYU_TANKA)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_5_KOUNYU_TANKA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_SHIKYU_HIN)).Value = GetNumericDbField(dtRow(NmDTColBase.TD_KAKO_5_SHIKYU_HIN))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_KOUJI_SHIREI_NO)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_5_KOUJI_SHIREI_NO)), String.Empty, dtRow(NmDTColBase.TD_KAKO_5_KOUJI_SHIREI_NO))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_EVENT_NAME)).Value = _
                IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_5_EVENT_NAME)), String.Empty, dtRow(NmDTColBase.TD_KAKO_5_EVENT_NAME))

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_HACHU_BI)).Value = _
                                YosanSetteiBuhinEditLogic.ConvDateInt8(IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_5_HACHU_BI)), 0, _
                                                                           dtRow(NmDTColBase.TD_KAKO_5_HACHU_BI)))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_KENSHU_BI)).Value = _
                                YosanSetteiBuhinEditLogic.ConvDateInt8(IIf(IsDBNull(dtRow(NmDTColBase.TD_KAKO_5_KENSHU_BI)), 0, _
                                                                           dtRow(NmDTColBase.TD_KAKO_5_KENSHU_BI)))



                '割付予算_部品費合計(円)
                Dim strYosanWaritukeBuhinHiTotal As String = GetNumericDbField(dtRow(NmDTColBase.TD_YOSAN_WARITUKE_BUHIN_HI_TOTAL))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI_TOTAL)).Value = strYosanWaritukeBuhinHiTotal
                '割付予算_型費(千円)
                Dim strYosanWaritukeKataHi As String = GetNumericDbField(dtRow(NmDTColBase.TD_YOSAN_WARITUKE_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_KATA_HI)).Value = strYosanWaritukeKataHi
                '購入希望_購入希望単価(円)
                Dim strYoaanKounyuKibouTanka As String = GetNumericDbField(dtRow(NmDTColBase.TD_YOSAN_KOUNYU_KIBOU_TANKA))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_TANKA)).Value = strYoaanKounyuKibouTanka
                '購入希望_部品費(円)
                Dim strYosanKounyuKibouBuhinHi As String = GetNumericDbField(dtRow(NmDTColBase.TD_YOSAN_KOUNYU_KIBOU_BUHIN_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI)).Value = strYosanKounyuKibouBuhinHi

                '購入希望_部品費合計(円)
                Dim strYosanKounyuKibouBuhinHiTotal As String = GetNumericDbField(dtRow(NmDTColBase.TD_YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL)).Value = strYosanKounyuKibouBuhinHiTotal



                '購入希望_型費(円)
                Dim strYosanKounyuKibouKataHi As String = GetNumericDbField(dtRow(NmDTColBase.TD_YOSAN_KOUNYU_KIBOU_KATA_HI))
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_KATA_HI)).Value = strYosanKounyuKibouKataHi
                '変化点
                Dim henkaTen As String = dtRow(NmDTColBase.TD_HENKATEN).ToString.Trim
                If henkaTen Is Nothing Then
                    henkaTen = String.Empty
                ElseIf henkaTen.Equals("1") Then
                    henkaTen = "○"
                ElseIf henkaTen.Equals("2") Then
                    henkaTen = "△"
                End If
                If henkaTen.Equals("3") Then

                End If

                '履歴情報を設定'
                Dim blockNo As String = sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Value
                Dim buhinNo As String = sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Value
                Dim buhinNoHyoujijun As String = sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN)).Value

                Dim key As String = EzUtil.MakeKey(blockNo, buhinNo, buhinNoHyoujijun)

                If dic.ContainsKey(key) Then
                    For Each tag As String In dic(key).Keys
                        Dim strMsg As String = ""
                        Dim j As Integer = 0
                        Dim strBefore As String = ""
                        Dim strAfter As String = ""

                        For Each rirekiVo As TYosanSetteiBuhinRirekiVo In dic(key)(tag)

                            If StringUtil.IsNotEmpty(rirekiVo.Before) Then
                                strBefore = rirekiVo.Before
                            Else
                                strBefore = "空白"
                            End If
                            If StringUtil.IsNotEmpty(rirekiVo.After) Then
                                strAfter = rirekiVo.After
                            Else
                                strAfter = "空白"
                            End If

                            If j > 0 Then
                                strMsg += vbCrLf & vbCrLf
                            End If
                            '更新日、変更前、変更後をセット
                            strMsg += Right(rirekiVo.UpdateBi, 5) & " : " _
                                    & strBefore & " ⇒ " & strAfter

                            '1を加算
                            j += 1

                            If Not _RirekiDic.ContainsKey(oRow) Then
                                _RirekiDic.Add(oRow, New Dictionary(Of String, List(Of TYosanSetteiBuhinRirekiVo)))
                            End If
                            If Not _RirekiDic(oRow).ContainsKey(rirekiVo.ColumnName) Then
                                _RirekiDic(oRow).Add(rirekiVo.ColumnName, New List(Of TYosanSetteiBuhinRirekiVo))
                            End If
                            _RirekiDic(oRow)(rirekiVo.ColumnName).Add(rirekiVo)

                        Next
                        sheet.Cells(oRow, GetTagIdx(sheet, tag)).NoteStyle = NoteStyle.PopupNote
                        sheet.Cells(oRow, GetTagIdx(sheet, tag)).Note = strMsg

                    Next

                End If


                oRow += 1
            Next

            Return True
        End Function

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

                workBlockNo = sheet.Cells(i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Text

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
            Dim maxSpreadGyouId As Integer = GetSpreadMaxNoInnerBlock(aBlockNo, NmSpdTagBase.TAG_YOSAN_GYOU_ID)
            '部品表示順の最大値を取得
            Dim maxSpreadBuhinHyoujiJun As Integer = GetSpreadMaxNoInnerBlock(aBlockNo, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN)

            '試作手配帳(基本)から最大行IDを取得
            dtBase = YosanSetteiBuhinEditImpl.FindMaxIDBaseInfo(_headerSubject.shisakuEventCode, _
                                                                                _shisakuListCode, _
                                                                                aBukaCode, _
                                                                                aBlockNo)
            Dim baseMaxGyouId As Integer = 0
            Dim baseMaxBuhinHyoujiJun As Integer = -1

            If dtBase.Rows.Count = 1 Then

                If Not dtBase.Rows(0)(NmDTColBase.TD_YOSAN_GYOU_ID) = Nothing Then
                    baseMaxGyouId = dtBase.Rows(0)(NmDTColBase.TD_YOSAN_GYOU_ID)
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
                clickBlockNo = sheetBase.Cells(refRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Text
                '部課コード取得
                clickBukaCode = sheetBase.Cells(refRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_BUKA_CODE)).Text
            Else
                'スプレッドへ行挿入と罫線引いて抜ける
                sheetBase.Rows.Add(aRowNo, aRowCount)
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

                Dim strMaxGyouId As String = String.Format("{0:000}", aMaxGyouId + 1)
                Dim strMaxBuhinHyoujiJun As String = String.Format("{0:000}", aMaxBuhinHyoujiJun + 1)

                '挿入した行に行IDをセット
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_GYOU_ID), strMaxGyouId)

                '部品表示順をセットする
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN), strMaxBuhinHyoujiJun)

                'ブロックNoをセットする
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_BLOCK_NO), aBlockNo)

                '部課コードをセットする
                sheetBase.SetValue(aRowNo + i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_BUKA_CODE), aBukaCode)

                Dim startCol As Integer = GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_BUKA_CODE)
                Dim endCol As Integer = GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_GYOU_ID)

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
            Dim sheet As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
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
            Dim bukaCode As String = YosanSetteiBuhinEditImpl.FindBukaCode(blockNo)

            '挿入位置を取得
            insRow = GetBtmLineBlockNo(blockNo)

            Dim maxGyouId As Integer = -1
            Dim maxBuhinHyoujiJun As Integer = -1

            '行ID、部品表示の最大数を取得
            GetMaxNo(bukaCode, blockNo, maxGyouId, maxBuhinHyoujiJun)

            'スプレッドへ行挿入
            sheet.Rows.Add(insRow, 1)

            '取得した値を空行に対してセットする
            SetBlockNoSpread(bukaCode, blockNo, maxGyouId, maxBuhinHyoujiJun, insRow, 1)

            'アクティブ位置を調整(一番上に行位置がスクロールするとわかりずらいので上に１行表示する)
            '但し先頭行の場合には、自分の行を表示する。　2011/02/28　柳沼修正
            If insRow <= 3 Then
                sheet.SetActiveCell(insRow, sheet.ActiveColumn.Index)
            Else
                sheet.SetActiveCell(insRow - 1, sheet.ActiveColumn.Index)
            End If

            Dim spd As FpSpread = _frmDispTehaiEdit.spdBase

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
            Dim sheet As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            For i As Integer = startRow To sheet.RowCount - 1
                Dim workBlock As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Trim

                '行位置保持
                findRow = i

                'ブロックNoの大小比較を行い、大きい値出現によりもう同じ値は見つからないのでその行を返す
                If aBlockNo.Trim < workBlock.Trim Then
                    Exit For
                End If

                '空白行ID発見で終了
                Dim gyouID As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_GYOU_ID)).Trim
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

            '基本情報スプレッドをセット
            Dim sheetBase As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1

            '対象行にセットすべきブロックNoを取得する
            Dim dtBase As DataTable = GetBlockNo(aRowNo)

            'ブロックNo取得
            blockNo = dtBase.Rows(0)(NmDTColBase.TD_YOSAN_BLOCK_NO)
            '部課コード取得 
            bukaCode = dtBase.Rows(0)(NmDTColBase.TD_YOSAN_BUKA_CODE)

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
            dtResult = YosanSetteiBuhinEditImpl.FindAllBaseInfo(String.Empty, String.Empty)

            'とりあえずブロックNoを取得してみる
            Dim blockNo As String = sheet.GetText(arowNo, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Trim
            Dim bukaCode As String = String.Empty

            '指定行にブロックNoは有るか？
            If blockNo.Equals(String.Empty) = False Then
                'ある場合は部課コードを探す
                bukaCode = YosanSetteiBuhinEditImpl.FindBukaCode(blockNo)
            Else
                '上方向にブロックNoを探してみる
                Dim findRowNo As Integer = SearchBlockNo(arowNo)
                '見つかった行からブロックNoと部課コードを取得
                If findRowNo >= 0 Then
                    blockNo = sheet.GetText(findRowNo, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Trim
                    bukaCode = sheet.GetText(findRowNo, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUKA_CODE)).Trim
                End If

            End If

            Dim dataRow As DataRow = dtResult.NewRow

            '取得できたブロックNo、部課コードをデータテーブルにセット
            dataRow(NmDTColBase.TD_YOSAN_BLOCK_NO) = blockNo
            dataRow(NmDTColBase.TD_YOSAN_BUKA_CODE) = bukaCode

            dtResult.Rows.Add(dataRow)

            Return dtResult
        End Function

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
                Dim findBlockNo As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Trim
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
            Dim sheet As SheetView = _frmDispTehaiEdit.spdBase_Sheet1

            'ブロックNoを編集対象として記録
            For i As Integer = 0 To aSelect.RowCount - 1
                Dim blockNo As String = sheet.GetText(aSelect.Row + i, YosanSetteiBuhinEditLogic.GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Trim
                '編集ブロック格納(重複は排除される）
                listEditBlock = blockNo
            Next

        End Sub

#End Region

#Region "Excel出力機能"

#Region "Excel出力機能(メイン処理)"
        ''' <summary>
        '''  Excel出力
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ExcelOutput()

            Dim fileName As String
            Dim impl As YosanSetteiBuhinEditHeaderDaoImpl
            impl = New YosanSetteiBuhinEditHeaderDaoImpl
            Dim aEventVo As New TShisakuEventVo
            aEventVo = impl.FindByEvent(_headerSubject.shisakuEventCode)

            '出力ファイル名生成
            Using sfd As New SaveFileDialog()
                sfd.FileName = ShisakuCommon.ShisakuGlobal.ExcelOutPut
                sfd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                '--------------------------------------------------
                'EXCEL出力先フォルダチェック
                ShisakuCommon.ShisakuComFunc.ExcelOutPutDirCheck()
                '--------------------------------------------------
                '[Excel出力系 F]
                'fileName = sfd.InitialDirectory + "\" + sfd.FileName
                fileName = aEventVo.ShisakuKaihatsuFugo + _headerSubject.listEventName + " 予算設定部品表編集中 " + Now.ToString("MMdd") + Now.ToString("HHmm") + ".xls"
                fileName = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + StringUtil.ReplaceInvalidCharForFileName(fileName)    '2012/02/08 Excel出力ディレクトリ指定対応
            End Using

            '出力処理へ
            SaveExcelFile(fileName)

            _frmDispTehaiEdit.Refresh()

            Process.Start(fileName)

            _frmDispTehaiEdit.Refresh()

            ComFunc.ShowInfoMsgBox("Excel出力が完了しました", MessageBoxButtons.OK)

        End Sub
#End Region

#Region "Excel出力機能(Spreadの機能によりExcelファイルを出力)"
        ''' <summary>
        ''' Excel出力機能(Spreadの機能によりExcelファイルを出力)
        ''' </summary>
        ''' <param name="filename"></param>
        ''' <remarks></remarks>
        Private Sub SaveExcelFile(ByVal filename As String)

            Dim fIO As New System.IO.FileInfo(filename)
            Dim spreadBase As FpSpread = _frmDispTehaiEdit.spdBase
            _frmDispTehaiEdit.Refresh()


            Dim startRow As Integer = GetTitleRowsIn(spreadBase.ActiveSheet)


            Try
                '基本スプレッド出力
                If _frmDispTehaiEdit.spdBase.SaveExcel(filename) = False Then
                    Throw New Exception("基本情報Excel出力でエラーが発生しました")
                End If

                If Not ShisakuComFunc.IsFileOpen(filename) Then
                    Using xls As New ShisakuExcel(filename)

                        xls.SetActiveSheet(1)
                        xls.SetSheetName(_TITLE_BASE)
                        Me.SetTitleLineExcel(xls, _TITLE_BASE)

                        'テスト'
                        SetExcelComment(xls)

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

            End Try



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
                Using frmExcelSheetSelect As New frmExcelSheetSelect()

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
                Dim blockNo As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Trim
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
                dtTehaiBase = YosanSetteiBuhinEditImpl.FindAllBaseInfo(db, _shisakuEventCode, _shisakuListCode)
                'マッチング用スプレッドデータテーブル取得
                dtSpreadBase = GetDtSpread(db)

                'DBテーブルとスプレッドのマッチングを行い存在しなくなったデータを削除する
                For i As Integer = 0 To dtTehaiBase.Rows.Count - 1

                    Dim blockNo As String = dtTehaiBase.Rows(i)(NmDTColBase.TD_YOSAN_BLOCK_NO).ToString

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
            Private _tehaichoLogic As YosanSetteiBuhinEditLogic = Nothing

            Public Sub New(ByVal aTehaichoLogic As YosanSetteiBuhinEditLogic, ByVal aRestoreFileName As String)
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

            'バックアップ基本シート削除
            spdBase.Sheets.Remove(spdBase.Sheets(1))

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

            '試作イベントコード・リストコードをバックアップ
            _backupShisakuListCode = _shisakuListCode
            _backupShisakuEventCode = _shisakuEventCode

            '基本スプレッドバックアップ
            Dim baseCopyIndex As Integer = -1
            baseCopyIndex = spdBase.Sheets.Add(CopySheet(_frmDispTehaiEdit.spdBase_Sheet1))
            spdBase.Sheets(baseCopyIndex).SheetName = _BACKUP_SHEET_NAME_BASE

            '編集ブロック情報バックアップ
            _backupListEditBlock = New List(Of String())

            For i As Integer = 0 To ListEditCount - 1
                _backupListEditBlock.Add(_listEditBlock(i))
            Next

            'バックアップシートの非表示
            spdBase.Sheets(baseCopyIndex).Visible = False

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

            'ヘッダー部リストア
            _shisakuListCode = _backupShisakuListCode
            _shisakuEventCode = _backupShisakuEventCode

            'ヘッダー初期化
            InitializeHeader()

            'スプレッド情報リストア
            Dim befBaseName As String = _frmDispTehaiEdit.spdBase_Sheet1.SheetName

            '取込した基本シート削除
            spdBase.Sheets.Remove(spdBase.Sheets(0))
            'ﾊﾞｯｸｱｯﾌﾟ用シートをを正規データ用に変更
            spdBase.Sheets(0).SheetName = befBaseName
            '元シートのアドレスにアクセスされないように上書きしておく
            _frmDispTehaiEdit.spdBase_Sheet1 = spdBase.Sheets(0)
            '画面描画
            _frmDispTehaiEdit.Refresh()

            '編集ブロック情報リストア
            _listEditBlock = New List(Of String())

            For i As Integer = 0 To _backupListEditBlock.Count - 1
                _listEditBlock.Add(_backupListEditBlock(i))
            Next

            '非表示を解除
            spdBase.Sheets(0).Visible = True

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

                    '列項目エラーチェック
                    If ImportColCheck(xls) = False Then
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
                    'スプレッドへ設定(基本)
                    ReadExcelBase(xls)
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
                Dim blockNo As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Trim
                If blockNo.Equals(String.Empty) Then
                    Exit For
                End If

                '部課コード取得
                Dim bukaCode As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUKA_CODE)).Trim
                '行IDの最大値を取得
                Dim gyouId As Integer = GetSpreadMaxNoInnerBlock(blockNo, NmSpdTagBase.TAG_YOSAN_GYOU_ID)
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
            If xls.EndRow >= sheet.RowCount Then
                sheet.RowCount = xls.EndRow + 10
            End If

            '全データ消去
            SpreadAllClear(sheet)

            '全ての行を編集ブロック対象とする（色等書式変更)
            SetEditRowProc(True, startSpdRow, 0, sheet.RowCount - startSpdRow, sheet.ColumnCount)

            For i As Integer = 0 To arryXls.GetLength(0) - 1

                'ブロックNoの空白でループ終了
                If GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_BLOCK_NO) Is Nothing Then
                    Exit For
                Else

                    ''試作ブロックNo
                    Dim shisakuBlockNo As String = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)
                    ''試作部課コード
                    Dim shisakuBukaCode As String = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_BUKA_CODE)

                    '部品番号表示順
                    Dim BuhinNoHyoujiJun As ValueType = -999
                    sheet.SetValue(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN), BuhinNoHyoujiJun)

                    'ブロックNo設定
                    If shisakuBlockNo Is Nothing Then
                        shisakuBlockNo = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO), shisakuBlockNo)

                    '部課コード設定
                    If shisakuBukaCode Is Nothing Then
                        '部課コードがない場合は取得する
                        shisakuBukaCode = YosanSetteiBuhinEditImpl.FindBukaCode(shisakuBlockNo)
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUKA_CODE), shisakuBukaCode)

                    'フラグ
                    Dim AudFlag As String = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_AUD_FLAG)
                    If AudFlag Is Nothing Then
                        AudFlag = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_AUD_FLAG), AudFlag)

                    '行ID
                    Dim GyouId As String = "-999"
                    sheet.SetValue(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_GYOU_ID), GyouId)

                    'レベル
                    Dim Level = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_LEVEL)
                    If Level Is Nothing Then
                        Level = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_LEVEL), Level)

                    '国内集計
                    Dim ShukeiCode = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_SHUKEI_CODE)
                    If ShukeiCode Is Nothing Then
                        ShukeiCode = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_SHUKEI_CODE), ShukeiCode)

                    '海外集計
                    Dim SiaShukeiCode = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_SIA_SHUKEI_CODE)
                    If SiaShukeiCode Is Nothing Then
                        SiaShukeiCode = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_SIA_SHUKEI_CODE), SiaShukeiCode)

                    '部品番号
                    Dim BuhinNo = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)
                    If BuhinNo Is Nothing Then
                        BuhinNo = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO), BuhinNo)

                    '部品名称
                    Dim BuhinName = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_BUHIN_NAME)
                    If BuhinName Is Nothing Then
                        BuhinName = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NAME), BuhinName)

                    '合計員数
                    Dim YosanInsu = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_INSU)
                    If YosanInsu Is Nothing Then
                        YosanInsu = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_INSU), YosanInsu)

                    '取引先コード
                    Dim MakerCode = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_MAKER_CODE)
                    If makercode Is Nothing Then
                        MakerCode = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_MAKER_CODE), MakerCode)

                    '供給セクション
                    Dim KyoukuSection = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_KYOUKU_SECTION)
                    If KyoukuSection Is Nothing Then
                        KyoukuSection = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KYOUKU_SECTION), KyoukuSection)

                    '購担
                    Dim KouTan = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_KOUTAN)
                    If KouTan Is Nothing Then
                        KouTan = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUTAN), KouTan)

                    '設計情報_作り方・製作方法
                    Dim TukurikataSeisaku = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_SEISAKU)
                    If TukurikataSeisaku Is Nothing Then
                        TukurikataSeisaku = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_SEISAKU), TukurikataSeisaku)

                    '設計情報_作り方・型仕様_1
                    Dim TukurikataKatashiyou1 = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_1)
                    If TukurikataKatashiyou1 Is Nothing Then
                        TukurikataKatashiyou1 = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_1), TukurikataKatashiyou1)

                    '設計情報_作り方・型仕様_2
                    Dim TukurikataKatashiyou2 = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_2)
                    If TukurikataKatashiyou2 Is Nothing Then
                        TukurikataKatashiyou2 = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_2), TukurikataKatashiyou2)

                    '設計情報_作り方・型仕様_3
                    Dim TukurikataKatashiyou3 = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_3)
                    If TukurikataKatashiyou3 Is Nothing Then
                        TukurikataKatashiyou3 = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_3), TukurikataKatashiyou3)

                    '設計情報_作り方・治具
                    Dim TsukurikataTigu = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_TIGU)
                    If TsukurikataTigu Is Nothing Then
                        TsukurikataTigu = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_TIGU), TsukurikataTigu)

                    '設計情報_作り方・部品製作規模・概要
                    Dim TsukurikataKibo = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KIBO)
                    If TsukurikataKibo Is Nothing Then
                        TsukurikataKibo = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KIBO), TsukurikataKibo)

                    '設計情報_試作部品費（円）
                    Dim ShisakuBuhinHi = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_SHISAKU_BUHIN_HI)
                    If ShisakuBuhinHi Is Nothing Then
                        ShisakuBuhinHi = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_SHISAKU_BUHIN_HI), ShisakuBuhinHi)

                    '設計情報_試作型費（千円）
                    Dim ShisakuKataHi = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_SHISAKU_KATA_HI)
                    If ShisakuKataHi Is Nothing Then
                        ShisakuKataHi = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_SHISAKU_KATA_HI), ShisakuKataHi)

                    '設計情報_部品ノート
                    Dim BuhinNote = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_BUHIN_NOTE)
                    If BuhinNote Is Nothing Then
                        BuhinNote = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NOTE), BuhinNote)

                    '設計情報_備考
                    Dim Bikou = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_BIKOU)
                    If Bikou Is Nothing Then
                        bikou = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BIKOU), bikou)

                    '部品費根拠_国外区分
                    Dim KonkyoKokugaiKbn = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN)
                    If KonkyoKokugaiKbn Is Nothing Then
                        KonkyoKokugaiKbn = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN), KonkyoKokugaiKbn)

                    '部品費根拠_MIXコスト部品費(円/ｾﾝﾄ)
                    Dim KonkyoMixBuhinHi = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI)
                    If KonkyoMixBuhinHi Is Nothing Then
                        KonkyoMixBuhinHi = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI), KonkyoMixBuhinHi)

                    '部品費根拠_引用元MIX値部品費
                    Dim KonkyoInyouMixBuhinHi = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI)
                    If KonkyoInyouMixBuhinHi Is Nothing Then
                        KonkyoInyouMixBuhinHi = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI), KonkyoInyouMixBuhinHi)

                    '部品費根拠_係数１
                    Dim KonkyoKeisu1 = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_KONKYO_KEISU_1)
                    If KonkyoKeisu1 Is Nothing Then
                        KonkyoKeisu1 = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KEISU_1), KonkyoKeisu1)

                    '部品費根拠_工法
                    Dim KonkyoKouhou = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_KONKYO_KOUHOU)
                    If KonkyoKouhou Is Nothing Then
                        KonkyoKouhou = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KOUHOU), KonkyoKouhou)

                    '割付予算_部品費(円)
                    Dim WaritukeBuhinHi = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI)
                    If WaritukeBuhinHi Is Nothing Then
                        WaritukeBuhinHi = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI), WaritukeBuhinHi)

                    '割付予算_係数２
                    Dim WaritukeKeisu2 = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_WARITUKE_KEISU_2)
                    If WaritukeKeisu2 Is Nothing Then
                        WaritukeKeisu2 = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_KEISU_2), WaritukeKeisu2)

                    '割付予算_部品費合計(円)
                    Dim WaritukeBuhinHiTotal = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI_TOTAL)
                    If WaritukeBuhinHiTotal Is Nothing Then
                        WaritukeBuhinHiTotal = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI_TOTAL), WaritukeBuhinHiTotal)

                    '割付予算_型費(千円)
                    Dim WaritukeKataHi = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_WARITUKE_KATA_HI)
                    If WaritukeKataHi Is Nothing Then
                        WaritukeKataHi = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_KATA_HI), WaritukeKataHi)

                    '購入希望_購入希望単価(円)
                    Dim KounyuKibouTanka = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_TANKA)
                    If KounyuKibouTanka Is Nothing Then
                        KounyuKibouTanka = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_TANKA), KounyuKibouTanka)

                    '購入希望_部品費(円)
                    Dim KounyuKibouBuhinHi = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI)
                    If KounyuKibouBuhinHi Is Nothing Then
                        KounyuKibouBuhinHi = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI), KounyuKibouBuhinHi)

                    '購入希望_部品費合計(円)
                    Dim KounyuKibouBuhinHiTotal = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL)
                    If KounyuKibouBuhinHiTotal Is Nothing Then
                        KounyuKibouBuhinHiTotal = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL), KounyuKibouBuhinHiTotal)

                    '購入希望_型費(円)
                    Dim KounyuKibouKataHi = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_KATA_HI)
                    If KounyuKibouKataHi Is Nothing Then
                        KounyuKibouKataHi = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_KATA_HI), KounyuKibouKataHi)

                    '変化点
                    Dim HenkaTen = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_HENKATEN)
                    If HenkaTen Is Nothing Then
                        HenkaTen = String.Empty
                    End If
                    sheet.SetText(startSpdRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_HENKATEN), HenkaTen)

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
            Dim workBlockNo As String = GetXlsArray(sheet, arryXls, arryTagRow, 0, NmSpdTagBase.TAG_YOSAN_BLOCK_NO).ToString.Trim
            Dim rowData As DataRow = dtBlockNo.NewRow
            rowData("BLOCK_NO") = workBlockNo
            dtBlockNo.Rows.Add(rowData)

            'Excelデータ行チェック
            For i As Integer = 0 To arryXls.GetLength(0) - 1

                'ブロックNoセット無しでループ終了
                If GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_BLOCK_NO) Is Nothing Then
                    Exit For
                End If

                '試作ブロックNoチェック
                Dim shisakuBlockNo As String = GetXlsArray(sheet, arryXls, arryTagRow, i, NmSpdTagBase.TAG_YOSAN_BLOCK_NO).ToString

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
        ''' <param name="xls"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ImportColCheck(ByVal xls As ShisakuExcel) As Boolean

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

            'エラー表示
            If errList.ToString.Equals(String.Empty) = False Then
                Dim errMsg As String = String.Format("Excel取込で問題が発生しました。{0} 取込に必要な列が存在しない可能性が有ります。{1}({2})" _
                                                     , vbCrLf, vbCrLf & vbCrLf, errList.ToString)
                'エラーメッセージ表示
                ComFunc.ShowErrMsgBox(errMsg)

                Return False
            End If

            Return True

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

            Dim bukaCode As String = sheetBase.GetText(aRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_BUKA_CODE)).Trim
            Dim blockNo As String = sheetBase.GetText(aRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Trim
            Dim tehaidate As ShisakuDate = New ShisakuDate

            Dim buhinstruct As New YosanSetteiBuhinEditStructure(_shisakuEventCode, _shisakuListCode, blockNo, bukaCode, tehaidate)

            Dim buhinNo As String = sheetBase.GetText(aRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Trim
            Dim strlevel As String = sheetBase.GetText(aRow, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_LEVEL)).Trim
            Dim intLevel As Integer = -1

            If strlevel.Equals(String.Empty) Then
                intLevel = 0
            Else
                intLevel = Integer.Parse(strlevel)
            End If

        End Function


#End Region

#Region "変更されたセルの列に対応した処理を行う"
        ''' <summary>
        ''' 変更されたセルの列に対応した処理を行う
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ChangeSpreadValueReflect(ByVal aRow As Integer, ByVal aCol As Integer, ByVal aColTag As String, ByVal aTextValue As String, ByVal aSupportValue As String)
            Dim sheet As FarPoint.Win.Spread.SheetView

            sheet = _frmDispTehaiEdit.spdBase_Sheet1

            '空白除去( "* "この種の問題対応)
            aTextValue = aTextValue.Trim

            '見出し行からのイベント処理はスルー
            If aRow < GetTitleRowsIn(sheet) Then
                Exit Sub
            End If

            '編集されたセルは太文字・青文字にする
            SetEditRowProc(IsBaseSpread, aRow, aCol)

            '空行への入力(ブロックNo,部課コード,行ID,部品表示順のセット)
            Dim wGyouId As String = sheet.GetText(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_GYOU_ID)).Trim
            '行IDの未入力で空行と判定
            If wGyouId.Equals(String.Empty) Then
                blankInput(aRow)
            End If

            '2016/01/21 kabasawa'
            '割付予算部品費の自動計算'
            If aCol = GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI) _
            OrElse aCol = GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_INSU) _
            OrElse aCol = GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI) _
            OrElse aCol = GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_KATA_HI)  Then
                '割付部品費か総数のときだけ'
                AutoMixPrice(aRow)
            End If


        End Sub

        ''' <summary>
        ''' 入力行のキーにより、試作手配帳情報（基本情報）を取得する
        ''' </summary>
        ''' <param name="sheet"></param>
        ''' <param name="aRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function FindByTShisakuTehaiKihonVo(ByVal sheet As FarPoint.Win.Spread.SheetView, ByVal aRow As Integer) As TYosanSetteiBuhinVo
            Dim blockNo As String = sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO))
            Dim bukaCode As String = sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUKA_CODE))
            Dim buhinHyoujiJun As String = sheet.GetValue(aRow, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN))
            Dim tehaichoDao As TYosanSetteiBuhinDao = New TYosanSetteiBuhinDaoImpl

            Return tehaichoDao.FindByPk(_headerSubject.shisakuEventCode, _
                                        _headerSubject.shisakuListCode, _
                                        bukaCode, _
                                        blockNo, _
                                        CInt(buhinHyoujiJun))
        End Function

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

            sheetBase.Cells(aRow, GetTagIdx(sheetBase, aTagName)).BackColor = aColor

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
            Dim startRow As Integer = GetTitleRowsIn(sheetBase)

            'エラー対象セル
            Dim errCell As Spread.Cell = Nothing
            Dim errFlag As Boolean = False
            Dim firstErrCell As Spread.Cell = Nothing
            '空行後入力判定用
            Dim empRowNo As Integer = -1

            '2016/01/08 kabasawa'
            'ブロック№、レベル、部品番号、供給セクションが同一の行は禁止する'
            Dim blbkList As New List(Of String)  'ブロック№と部品番号と供給セクションのキー


            '試作手配基本スプレッドループ
            For i As Integer = startRow To sheetBase.RowCount - startRow - 1
                '基本スプレッドのエラー色を戻す
                For j As Integer = 0 To sheetBase.ColumnCount - 1
                    sheetBase.Cells(i, j).BackColor = Nothing
                Next

                '行ID有無でデータ行終了判定を行う

                Dim gyoID As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_GYOU_ID)).ToString.Trim
                If gyoID.Equals(String.Empty) = True Then
                    empRowNo = i
                    Continue For

                End If

                '部課コード必須確認
                Dim bukaCode As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_BUKA_CODE)).Trim

                If bukaCode.Equals(String.Empty) Then

                    SetColorErrorCheck(i, NmSpdTagBase.TAG_YOSAN_BUKA_CODE, Color.Red)
                    errCell = sheetBase.Cells(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_BUKA_CODE))
                    errFlag = True

                End If

                'ブロックNo入力形式
                Dim blockNo As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).ToString.Trim

                If blockNo.Equals(String.Empty) Then
                    SetColorErrorCheck(i, NmSpdTagBase.TAG_YOSAN_BLOCK_NO, Color.Red)
                    errFlag = True

                End If

                'フラグ入力形式
                Dim audFlag As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_AUD_FLAG)).Trim

                If audFlag.Equals(String.Empty) = False AndAlso _
                    (audFlag.Equals("A") = False And _
                     audFlag.Equals("C") = False And _
                     audFlag.Equals("D") = False) Then
                    SetColorErrorCheck(i, NmSpdTagBase.TAG_AUD_FLAG, Color.Red)
                    errCell = sheetBase.Cells(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_AUD_FLAG))
                    errFlag = True
                Else
                    SetColorErrorCheck(i, NmSpdTagBase.TAG_AUD_FLAG, Nothing)
                End If

                '部品番号(とりあえず入力有無のみ
                Dim buhinNo As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Trim

                If buhinNo.Equals(String.Empty) Then

                    SetColorErrorCheck(i, NmSpdTagBase.TAG_YOSAN_BUHIN_NO, Color.Red)
                    errCell = sheetBase.Cells(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_BUHIN_NO))
                    errFlag = True

                End If

                'レベル
                Dim level As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_LEVEL)).Trim

                '空白でなく数値に変換できるか
                If level.Equals(String.Empty) = True OrElse IsNumeric(level) = False Then
                    SetColorErrorCheck(i, NmSpdTagBase.TAG_YOSAN_LEVEL, Color.Red)
                    errCell = sheetBase.Cells(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_LEVEL))
                    errFlag = True
                End If

                '総数
                Dim insu As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_INSU)).Trim
                If insu.Equals(String.Empty) = True OrElse insu.Equals("**") = False And IsNumeric(insu) = False Then
                    SetColorErrorCheck(i, NmSpdTagBase.TAG_YOSAN_INSU, Color.Red)
                    errCell = sheetBase.Cells(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_INSU))
                    errFlag = True
                End If


                '供給セクション取得'
                Dim kyokyuSection As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_KYOUKU_SECTION)).Trim
                Dim shukeiCode As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_SHUKEI_CODE)).ToString.Trim
                Dim siaShukeiCode As String = sheetBase.GetText(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_SIA_SHUKEI_CODE)).ToString.Trim

                'ブロック№、部品番号、供給セクションが同一の情報が存在しているか'
                Dim key As String = EzUtil.MakeKey(blockNo, level, buhinNo, shukeiCode, siaShukeiCode, kyokyuSection)
                If blbkList.Contains(key) Then
                    SetColorErrorCheck(i, NmSpdTagBase.TAG_YOSAN_BLOCK_NO, Color.Red)
                    SetColorErrorCheck(i, NmSpdTagBase.TAG_YOSAN_LEVEL, Color.Red)
                    SetColorErrorCheck(i, NmSpdTagBase.TAG_YOSAN_BUHIN_NO, Color.Red)
                    SetColorErrorCheck(i, NmSpdTagBase.TAG_YOSAN_SHUKEI_CODE, Color.Red)
                    SetColorErrorCheck(i, NmSpdTagBase.TAG_YOSAN_SIA_SHUKEI_CODE, Color.Red)
                    SetColorErrorCheck(i, NmSpdTagBase.TAG_YOSAN_KYOUKU_SECTION, Color.Red)
                    errCell = sheetBase.Cells(i, GetTagIdx(sheetBase, NmSpdTagBase.TAG_YOSAN_BLOCK_NO))
                    errFlag = True
                Else
                    blbkList.Add(key)
                End If


                '入力文字数チェック(これは遅いだろう）
                For k As Integer = 0 To sheetBase.ColumnCount - 1
                    If CheckCellLength(sheetBase, i, k) = False Then

                        sheetBase.Cells(i, k).BackColor = Color.Red

                        errCell = sheetBase.Cells(i, k)
                        errFlag = True
                    End If
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

                _frmDispTehaiEdit.spdBase.ShowActiveCell(Spread.VerticalPosition.Top, Spread.HorizontalPosition.Left)

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

            aDate = New ShisakuDate

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

            Using db As New System.Data.SqlClient.SqlConnection(NitteiDbComFunc.GetConnectString)
                db.Open()
                '－トランザクション開始－
                Using trans As System.Data.SqlClient.SqlTransaction = db.BeginTransaction
                    '試作リストコード情報更新
                    If SaveShisakuListCode(db, trans) = False Then
                        trans.Rollback()
                        Return False
                    End If

                    watch.Stop()
                    Console.WriteLine(String.Format("試作リストコード情報更新-実行時間 : {0}ms", watch.ElapsedMilliseconds))
                    watch.Reset()

                    watch.Start()

                    '試作手配帳更新
                    If SaveShisakuTehaichBase(db, trans) = False Then
                        trans.Rollback()
                        Return False
                    End If

                    watch.Stop()
                    Console.WriteLine(String.Format("試作手配帳更新-実行時間 : {0}ms", watch.ElapsedMilliseconds))
                    watch.Reset()

                    watch.Stop()
                    Console.WriteLine(String.Format("試作手配号車更新-実行時間 : {0}ms", watch.ElapsedMilliseconds))
                    watch.Reset()

                    watch.Start()

                    trans.Commit()

                    watch.Stop()
                    Console.WriteLine(String.Format("コミット-実行時間 : {0}ms", watch.ElapsedMilliseconds))
                    watch.Reset()
                End Using

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
        Private Function SaveShisakuListCode(ByVal aDb As System.Data.SqlClient.SqlConnection, ByVal trans As SqlClient.SqlTransaction) As Boolean

            Dim result As Boolean
            result = YosanSetteiBuhinEditImpl.UpdShisakuList(aDb, _
                                                     trans, _
                                                    _frmDispTehaiEdit.txtKoujiNo.Text.Trim, _
                                                    _headerSubject.shisakuEventCode, _
                                                    _headerSubject.shisakuListCode)


            Return result

        End Function
#End Region

#Region "保存機能(予算設定部品表)"

#Region "保存機能(予算設定部品表全体処理)"
        ''' <summary>
        ''' 保存機能(メインループ)
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SaveShisakuTehaichBase(ByVal aDb As SqlClient.SqlConnection, ByVal trans As SqlClient.SqlTransaction) As Boolean
            Dim sheetBase As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheetBase)

            Dim watch As New Stopwatch()

            watch.Start()

            'マッチング用DBデータテーブル取得
            Dim dtTehaiBase As DataTable = YosanSetteiBuhinEditImpl.FindAllBaseInfo(aDb, trans, _shisakuEventCode, _shisakuListCode)
            'マッチング用スプレッドデータテーブル取得
            Dim dtSpreadBase As DataTable = GetDtSpread(aDb, trans, sheetBase, startRow)


            watch.Stop()
            Console.WriteLine(String.Format("   マッチング用データテーブル取得-実行時間 : {0}ms", watch.ElapsedMilliseconds))
            watch.Reset()

            watch.Start()



            ''DBテーブルとスプレッドのマッチングを行い存在しなくなったデータを削除する
            'For i As Integer = 0 To dtTehaiBase.Rows.Count - 1

            '    Dim blockNo As String = dtTehaiBase.Rows(i)(NmDTColBase.TD_YOSAN_BLOCK_NO).ToString
            '    '編集対象ブロックNoで無ければ次行へ
            '    If IsEditBlockNo(blockNo) = False Then
            '        Continue For
            '    End If

            '    'スプレッド上を探索し対象データレコードが存在するか確認
            '    If FindDeleteRecord(aDb, trans, dtTehaiBase.Rows(i), dtSpreadBase) = False Then
            '        Return False
            '    End If

            'Next

            'watch.Stop()
            'Console.WriteLine(String.Format("  マッチングを行い存在しなくなったデータを削除する-実行時間 : {0}ms", watch.ElapsedMilliseconds))
            'watch.Reset()

            watch.Start()

            ''インサートを行う情報のリスト
            Dim vosBase As New List(Of TYosanSetteiBuhinVo)

            '試作手配基本を順番に読み取り編集したブロックの場合、追加か更新を行う
            Dim sortJun As New Hashtable
            For i As Integer = 0 To dtSpreadBase.Rows.Count - 1

                '処理レコードにブロックNoが存在しない場合はスルー
                If StringUtil.IsEmpty(_frmDispTehaiEdit.spdBase_Sheet1.GetText(i + startRow, YosanSetteiBuhinEditLogic.GetTagIdx(_frmDispTehaiEdit.spdBase_Sheet1, NmSpdTagBase.TAG_YOSAN_BLOCK_NO))) Then
                    Continue For
                End If

                'ブロックNo内での位置（ソート順）を取得
                Dim blockNo As String = dtSpreadBase.Rows(i)(NmDTColBase.TD_YOSAN_BLOCK_NO)
                If Not sortJun.Contains(blockNo) Then
                    sortJun.Add(blockNo, 1)
                Else
                    sortJun.Item(blockNo) += 1
                End If

                ''スプレッド上の情報をリストへ登録
                vosBase.Add(SpdToBaseVo(aDb, trans, dtSpreadBase, i, startRow, dtTehaiBase, sortJun.Item(blockNo)))

            Next

            watch.Stop()
            Console.WriteLine(String.Format("  予算設定部品表を追加か更新を行う-実行時間 : {0}ms", watch.ElapsedMilliseconds))
            watch.Reset()

            watch.Start()

            ''リストのDelete&Insert
            If YosanSetteiBuhinEditImpl.BulkYosanSetteiBuhin(aDb, trans, vosBase) = False Then
                Return False
            End If
            watch.Stop()
            Console.WriteLine(String.Format("  -UNIT-KBN SEIHINKBN更新-実行時間 : {0}ms", watch.ElapsedMilliseconds))
            watch.Reset()


            RegistRireki(_RirekiVos)
            watch.Stop()
            Console.WriteLine(String.Format("   予算設定履歴登録-実行時間 : {0}ms", watch.ElapsedMilliseconds))
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
            Dim dtResult As DataTable = YosanSetteiBuhinEditImpl.FindAllBaseInfo(aDb, "Z9999", "ZZZZZZ")
            dtResult.Rows.Clear()

            For i As Integer = startRow To sheet.RowCount - 1
                Dim row As DataRow = dtResult.NewRow

                '基本情報スプレッド側
                Dim blockNo As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO))
                If blockNo.Equals(String.Empty) Then
                    Continue For
                End If

                row(NmDTColBase.TD_YOSAN_BLOCK_NO) = blockNo
                row(NmDTColBase.TD_YOSAN_BUKA_CODE) = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUKA_CODE))
                row(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN) = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN))
                row(NmDTColBase.TD_YOSAN_BUHIN_NO) = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO))
                dtResult.Rows.Add(row)

            Next

            Return dtResult
        End Function

        ''' <summary>
        ''' スプレッドのキー項目をデータテーブルに格納
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetDtSpread(ByVal aDb As SqlClient.SqlConnection, ByVal trans As SqlClient.SqlTransaction, _
                                     ByVal sheet As SheetView, ByVal startRow As Integer) As DataTable
            'データ型を取得
            Dim dtResult As DataTable = YosanSetteiBuhinEditImpl.FindAllBaseInfo(aDb, trans, "Z9999", "ZZZZZZ")
            dtResult.Rows.Clear()

            For i As Integer = startRow To sheet.RowCount - 1
                Dim row As DataRow = dtResult.NewRow

                '基本情報スプレッド側
                Dim blockNo As String = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO))
                If blockNo.Equals(String.Empty) Then
                    Continue For
                End If

                row(NmDTColBase.TD_YOSAN_BLOCK_NO) = blockNo
                row(NmDTColBase.TD_YOSAN_BUKA_CODE) = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUKA_CODE))
                row(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN) = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN))
                row(NmDTColBase.TD_YOSAN_BUHIN_NO) = sheet.GetText(i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO))
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
                Dim workBlockNo As String = aDtSpreadBase.Rows(i)(NmDTColBase.TD_YOSAN_BLOCK_NO).ToString.Trim
                'ブロックNo出現回数をカウント
                If aBlockNo.Trim.Equals(workBlockNo) Then
                    findCnt += 1
                End If
            Next

            Return findCnt

        End Function
#End Region


#Region "行削除されたレコードをテーブルから削除"
        ''' <summary>
        ''' 行削除されたレコードをテーブルから削除
        ''' </summary>
        ''' <param name="aTableDataRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function FindDeleteRecord(ByVal aDb As SqlClient.SqlConnection, ByVal trans As SqlClient.SqlTransaction, _
                                          ByVal aTableDataRow As DataRow, ByVal aDtSpreadBase As DataTable) As Boolean

            '削除された行か確認する
            Dim findFlag As Boolean = IsDeleteRow(aTableDataRow, aDtSpreadBase)

            'スプレッド上にDB取得データレコードが存在しないため削除を行う
            If findFlag = False Then

                'DELETE 試作手配(基本) -号車情報はDELETE-INSERTで行う
                If YosanSetteiBuhinEditImpl.DelTehaichoBasebuhinNo(aDb, trans, aTableDataRow) = False Then
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
                Dim blockNo_a As String = aTableDataRow(NmDTColBase.TD_YOSAN_BLOCK_NO)
                Dim buhinNoHyoujiJun_a As String = aTableDataRow(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN)
                Dim bukaCode_a As String = aTableDataRow(NmDTColBase.TD_YOSAN_BUKA_CODE)

                '基本情報スプレッド側
                Dim blockNo_b As String = aDtSpreadBase.Rows(j)(NmDTColBase.TD_YOSAN_BLOCK_NO)
                Dim buhinNoHyoujiJun_b As String = aDtSpreadBase.Rows(j)(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN)
                Dim bukaCode_b As String = aDtSpreadBase.Rows(j)(NmDTColBase.TD_YOSAN_BUKA_CODE)

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

        ''' <summary>
        ''' 探そうとしているデータが存在するか確認
        ''' </summary>
        ''' <param name="aTableDataRow"></param>
        ''' <param name="aDtSpreadBase"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function IsDeleteRow(ByVal aTableDataRow As DataRow, ByVal aDtSpreadBase As DataTable) As Boolean
            Dim findFlag As Boolean = False

            'スプレッド上を探索し対象データレコードが存在するか確認
            For j As Integer = 0 To aDtSpreadBase.Rows.Count - 1
                'DB側
                Dim blockNo_a As String = aTableDataRow(NmDTColBase.TD_YOSAN_BLOCK_NO)
                Dim buhinNoHyoujiJun_a As String = aTableDataRow(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN)
                Dim bukaCode_a As String = aTableDataRow(NmDTColBase.TD_YOSAN_BUKA_CODE)

                '基本情報スプレッド側
                Dim blockNo_b As String = aDtSpreadBase.Rows(j)(NmDTColBase.TD_YOSAN_BLOCK_NO)
                Dim buhinNoHyoujiJun_b As String = aDtSpreadBase.Rows(j)(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN)
                Dim bukaCode_b As String = aDtSpreadBase.Rows(j)(NmDTColBase.TD_YOSAN_BUKA_CODE)

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

        End Sub
#End Region

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

            Dim spd As FpSpread = _frmDispTehaiEdit.spdBase

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

            Dim spd As FpSpread = _frmDispTehaiEdit.spdBase

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

            Dim spd As FpSpread = _frmDispTehaiEdit.spdBase

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

                sheet = _frmDispTehaiEdit.spdBase_Sheet1

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
            Dim endCol As Integer = -1

            '選択しているアクティブ列の示列を非表示にします。

            sheet = _frmDispTehaiEdit.spdBase_Sheet1
            endCol = sheet.ColumnCount - 1

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
                Dim spread As Spread.FpSpread = _frmDispTehaiEdit.spdBase

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
                Dim spread As Spread.FpSpread = _frmDispTehaiEdit.spdBase

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

            '部課コード列
            sheetBase.Columns(NmSpdTagBase.TAG_YOSAN_BUKA_CODE).Visible = False

            '部品番号表示順列
            sheetBase.Columns(NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN).Visible = False

            '変化点
            sheetBase.Columns(NmSpdTagBase.TAG_HENKATEN).Visible = False

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

            '行フィルタ解除
            For i As Integer = startRow To _frmDispTehaiEdit.spdBase_Sheet1.Rows.Count - 1
                _frmDispTehaiEdit.spdBase_Sheet1.Rows(i).Visible = True
                _frmDispTehaiEdit.spdBase_Sheet1.RowHeader.Rows(i).ForeColor = Nothing
            Next

            'スプレッドに対してのコピーショートカットキー（Ctrl +X)を無効に（コード上でコピーを処理する為必要な処置)
            'スプレッドに対してのコピーショートカットキー（Ctrl +C)を無効に（コード上でコピーを処理する為必要な処置)
            'スプレッドに対してのコピーショートカットキー（Ctrl +V)を無効に（コード上でコピーを処理する為必要な処置)
            'Dim spreadVisible As FpSpread = GetVisibleSpread
            'Dim imSpread As New FarPoint.Win.Spread.InputMap
            spreadVisible = _frmDispTehaiEdit.spdBase
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

            Dim sheetVisible As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
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
                Else
                    sheetVisible.RowHeader.Rows(i).ForeColor = Color.Blue
                End If
            Next

            'スプレッドに対してのコピーショートカットキー（Ctrl +X)を無効に（コード上でコピーを処理する為必要な処置)
            spreadVisible = _frmDispTehaiEdit.spdBase
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

            sheet = _frmDispTehaiEdit.spdBase_Sheet1

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
                Dim blockNo As String = sheet.GetText(aRow + i, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Trim
                listEditBlock() = blockNo
            Next

            '編集されたセルは太文字・青文字にする
            sheet.Cells(aRow, aCol, aRow + aRowCount - 1, aCol + aColCount - 1).ForeColor = Color.Blue
            sheet.Cells(aRow, aCol, aRow + aRowCount - 1, aCol + aColCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

            '基本・号車共通列を書式設定
            If aCol <= GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUTAN) Then
                Dim endCol As Integer = aCol + aColCount - 1
                '共通列を超える場合は共通列の最大位置にする
                If endCol > GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUTAN) Then
                    endCol = GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUTAN)
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
            Dim strLevel As String = aSheet.GetText(aRow, GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_LEVEL)).Trim

            '文字種判定
            If IsNumeric(strLevel) = False Then
                Return -1
            End If

            Dim startLevel As Integer = Integer.Parse(strLevel)

            'F品番チェック
            If startLevel = 0 Then
                Return -1
            End If

            Dim startBlockNo As String = aSheet.GetText(aRow, GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Trim
            Dim resultRow As Integer = -1

            For i As Integer = aRow - 1 To rowEnd Step -1

                Dim chkLevel As String = aSheet.GetText(i, GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_LEVEL)).Trim
                Dim intChkLevel As Integer = -1
                Dim chkBlockNo As String = aSheet.GetText(i, GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Trim

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
            Dim strLevel As String = aSheet.GetText(aRow, GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_LEVEL)).Trim

            '文字種判定
            If IsNumeric(strLevel) = False Then
                Return -1
            End If

            Dim startLevel As Integer = Integer.Parse(strLevel)

            'F品番チェック
            If startLevel = 0 Then
                Return -1
            End If

            Dim startBlockNo As String = aSheet.GetText(aRow, GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Trim
            Dim resultRow As Integer = -1

            For i As Integer = aRow - 1 To rowEnd Step -1

                Dim chkLevel As String = aSheet.GetText(i, GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_LEVEL)).Trim
                Dim intChkLevel As Integer = -1
                Dim chkBlockNo As String = aSheet.GetText(i, GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Trim

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
                    If StringUtil.Equals(aSheet.GetText(i, GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Trim, buhinNoOya.Trim) Then
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
            Dim strLevel As String = aSheet.GetText(aRow, GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_LEVEL)).Trim

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
            Dim startBlockNo As String = aSheet.GetText(aRow, GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Trim

            Dim resultRow As Integer = -1

            '自身の行-1スタートで上の行を見ていく'
            For i As Integer = aRow - 1 To rowEnd Step -1

                Dim chkLevel As String = aSheet.GetText(i, GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_LEVEL)).Trim
                Dim intChkLevel As Integer = -1
                Dim chkBlockNo As String = aSheet.GetText(i, GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Trim

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
        Private Shared _hshGetTagIdx As New Hashtable
        ''' <summary>
        ''' 列タグを元に列インデックスを取得します.
        ''' </summary>
        ''' <param name="sheet">対象シート</param>
        ''' <param name="tag">列タグ</param>
        ''' <returns>列インデックス</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTagIdx(ByVal sheet As Spread.SheetView, ByVal tag As String) As Integer
            Dim key As New System.Text.StringBuilder
            With key
                .AppendLine(sheet.SheetName)
                .AppendLine(tag)
            End With

            If Not _hshGetTagIdx.Contains(key.ToString) Then
                Dim col As Spread.Column = sheet.Columns(tag)

                If col Is Nothing Then
                    Return -1
                End If
                _hshGetTagIdx.Add(key.ToString, col.Index)
            End If

            Return _hshGetTagIdx.Item(key.ToString)
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

            Return YosanSetteiBuhinEditNames.PREFIX_GOUSHA_TAG & String.Format("{0:#00}", colNo)

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
        ''' 0の場合は空をセットする
        ''' 
        ''' </summary>
        ''' <param name="aObject"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetNumericDbField(ByVal aObject As Object) As String
            Dim workString As String = String.Empty

            If IsDBNull(aObject) = True Then
                workString = String.Empty
            ElseIf StringUtil.IsEmpty(aObject) Then
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
            If YosanSetteiBuhinEditLogic.LenB(cellValue.ToString) > tCell.MaxLength Then
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
            If YosanSetteiBuhinEditLogic.LenB(cellValue.ToString) > tCell.MaxLength Then
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

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

#Region "MIX値最新化"

        ''' <summary>
        ''' 画面のコスト
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ReadMixCost()

            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)
            Dim buhinNoList As New List(Of String)

            For rowIndex As Integer = startRow To sheet.RowCount - 1
                Dim buhinNo As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Value()
                If StringUtil.IsNotEmpty(buhinNo) Then
                    If Not buhinNoList.Contains(buhinNo.Trim) Then
                        buhinNoList.Add(buhinNo.Trim)
                    End If

                    'If buhinNo.Length = 12 Then
                    '    Dim buhinNo10 As String = Left(buhinNo, 10)
                    '    If Not buhinNoList.Contains(buhinNo10) Then
                    '        buhinNoList.Add(buhinNo10)
                    '    End If
                    'End If

                End If
            Next

            'MIXコストを取得'
            Dim dao As YosanSetteiBuhinEditHeaderDao = New YosanSetteiBuhinEditHeaderDaoImpl

            'パーツプライスリスト作成'
            'AS400から取得'
            'こちらは海外コスト×100済みで取得しているので計算しない'
            Dim vos As List(Of ASPartsPriceListVo) = dao.FindByPartsPrice(buhinNoList)

            Dim dic As New Dictionary(Of String, ASPartsPriceListVo)

            For Each vo As ASPartsPriceListVo In vos
                If Not dic.ContainsKey(vo.BuhinNo) Then
                    dic.Add(vo.BuhinNo, vo)
                End If
            Next



            'AS400設定'
            For rowIndex As Integer = startRow To sheet.RowCount - 1
                Dim buhinNo As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Value()

                If StringUtil.IsNotEmpty(buhinNo) Then

                    If dic.ContainsKey(buhinNo) Then

                        Dim kokugaiKbn As String = ""
                        Dim mixCost As String = ""
                        Dim inyoumoto As String = "パーツプライス(AS400)"

                        If StringUtil.IsEmpty(dic(buhinNo).KPrice) Then
                            If StringUtil.IsEmpty(dic(buhinNo).SiaPrice) Then
                                Continue For
                            Else
                                kokugaiKbn = "US"
                                mixCost = dic(buhinNo).SiaPrice
                            End If
                        Else
                            kokugaiKbn = "JP"
                            mixCost = dic(buhinNo).KPrice
                            If StringUtil.IsNotEmpty(dic(buhinNo).Mark) Then
                                Dim a As String = ""
                            End If
                            If StringUtil.Equals(dic(buhinNo).Mark, "K") Then
                                inyoumoto = "パーツプライス(仮単価)"
                            End If
                        End If

                        '0なら無視する'
                        If IsNumeric(mixCost) Then
                            If Decimal.Parse(mixCost) = 0 Then
                                Continue For
                            End If
                        End If


                        'どちらかあるので消す'
                        buhinNoList.Remove(buhinNo)

                        Dim key As String = EzUtil.MakeKey(kokugaiKbn, mixCost, inyoumoto)

                        Dim key2 As String = EzUtil.MakeKey(sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN)).Value, _
                                                            sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI)).Value, _
                                                            sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI)).Value)

                        If Not StringUtil.Equals(key, key2) Then
                            '部品費根拠_国外区分
                            SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN), kokugaiKbn)
                            '部品費根拠_MIXコスト部品費(円/ｾﾝﾄ)
                            SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI), mixCost)
                            '部品費根拠_引用元MIX値部品費
                            SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI), inyoumoto)
                        End If
                    Else
                        'If buhinNo.Length = 12 Then
                        '    Dim buhinNo10 As String = Left(buhinNo, 10)

                        '    If dic.ContainsKey(buhinNo10) Then
                        '        Dim kokugaiKbn As String = ""
                        '        Dim mixCost As String = ""
                        '        Dim inyoumoto As String = "パーツプライス(AS400)"

                        '        If StringUtil.IsEmpty(dic(buhinNo10).KPrice) Then
                        '            If StringUtil.IsEmpty(dic(buhinNo10).SiaPrice) Then
                        '                Continue For
                        '            Else
                        '                kokugaiKbn = "US"
                        '                mixCost = dic(buhinNo10).SiaPrice
                        '            End If
                        '        Else
                        '            kokugaiKbn = "JP"
                        '            mixCost = dic(buhinNo10).KPrice
                        '            If StringUtil.IsNotEmpty(dic(buhinNo10).Mark) Then
                        '                Dim a As String = ""
                        '            End If
                        '            If StringUtil.Equals(dic(buhinNo10).Mark, "K") Then
                        '                inyoumoto = "パーツプライス(仮単価)"
                        '            End If
                        '        End If

                        '        '0なら無視する'
                        '        If IsNumeric(mixCost) Then
                        '            If Decimal.Parse(mixCost) = 0 Then
                        '                Continue For
                        '            End If
                        '        End If


                        '        'どちらかあるので消す'
                        '        buhinNoList.Remove(buhinNo10)

                        '        Dim key As String = EzUtil.MakeKey(kokugaiKbn, mixCost, inyoumoto)

                        '        Dim key2 As String = EzUtil.MakeKey(sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN)).Value, _
                        '                                            sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI)).Value, _
                        '                                            sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI)).Value)

                        '        If Not StringUtil.Equals(key, key2) Then
                        '            '部品費根拠_国外区分
                        '            SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN), kokugaiKbn)
                        '            '部品費根拠_MIXコスト部品費(円/ｾﾝﾄ)
                        '            SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI), mixCost)
                        '            '部品費根拠_引用元MIX値部品費
                        '            SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI), inyoumoto)
                        '        End If

                        '    End If

                        'End If

                    End If
                End If
            Next
            '残りの部品のMIXコストを探索する'
            'パーツプライスは開発符号無視で最新改訂を取得する'
            ReadRhac2110(dao, buhinNoList)

        End Sub

#End Region

#Region "統合開発管理表の要領でMIX値を取得する"

        ''' <summary>
        ''' パーツプライスを設定
        ''' </summary>
        ''' <param name="dao"></param>
        ''' <param name="buhinNoList"></param>
        ''' <remarks></remarks>
        Private Sub ReadRhac2110(ByVal dao As YosanSetteiBuhinEditHeaderDao, _
                                 ByVal buhinNoList As List(Of String))

            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            'カラーコードを探す必要がある・・・が、カラーコードも開発符号依存している。'
            '試作は試作で色を設定しているので、色付きままで探索。'
            Dim vos As List(Of Rhac2110Vo) = dao.FindLatestLowPartsPriceBy(buhinNoList)
            '12桁だけ##にして再探索'
            Dim buhinNoList2 As New List(Of String)
            For Each buhinNo As String In buhinNoList
                If buhinNo.Length = 12 Then
                    If Not StringUtil.Equals(Right(buhinNo, 2), "##") Then
                        buhinNoList2.Add(Left(buhinNo, 10) & "##")
                    End If
                End If
            Next

            Dim vos2 As New List(Of Rhac2110Vo)
            vos2 = dao.FindLatestLowPartsPriceBy(buhinNoList2)


            Dim dic As New Dictionary(Of String, ASPartsPriceListVo)

            For Each vo As Rhac2110Vo In vos
                vo.BuhinNo = vo.BuhinNo.Trim
                If Not dic.ContainsKey(vo.BuhinNo) Then
                    Dim addVo As New ASPartsPriceListVo
                    addVo.BuhinNo = vo.BuhinNo
                    dic.Add(vo.BuhinNo, addVo)
                End If

                If StringUtil.Equals(vo.TsukaKbn, "Y") Then
                    If StringUtil.Equals(vo.TankaKeiyakuKbn, "G") Then
                        '国内パーツプライス'
                        dic(vo.BuhinNo).KPrice = vo.KeiyakuTankaKei - vo.YuusyouSikyuBuhinhi
                    Else
                        '国内パーツプライス(CKD)'
                        'CKDは現調区分がCの時のみだが・・・'
                        dic(vo.BuhinNo).KPrice = vo.KeiyakuTankaKei - vo.YuusyouSikyuBuhinhi
                    End If
                Else
                    '海外パーツプライス'
                    dic(vo.BuhinNo).SiaPrice = vo.KeiyakuTankaKei - vo.YuusyouSikyuBuhinhi
                    '円にする'
                    '×100しない'
                    dic(vo.BuhinNo).SiaPrice = dic(vo.BuhinNo).SiaPrice
                End If
            Next

            For Each vo As Rhac2110Vo In vos2
                vo.BuhinNo = vo.BuhinNo.Trim
                If Not dic.ContainsKey(vo.BuhinNo) Then
                    Dim addVo As New ASPartsPriceListVo
                    addVo.BuhinNo = vo.BuhinNo
                    dic.Add(vo.BuhinNo, addVo)
                End If

                If StringUtil.Equals(vo.TsukaKbn, "Y") Then
                    If StringUtil.Equals(vo.TankaKeiyakuKbn, "G") Then
                        '国内パーツプライス'
                        dic(vo.BuhinNo).KPrice = vo.KeiyakuTankaKei - vo.YuusyouSikyuBuhinhi
                    Else
                        '国内パーツプライス(CKD)'
                        'CKDは現調区分がCの時のみだが・・・'
                        dic(vo.BuhinNo).KPrice = vo.KeiyakuTankaKei - vo.YuusyouSikyuBuhinhi
                    End If
                Else
                    '海外パーツプライス'
                    dic(vo.BuhinNo).SiaPrice = vo.KeiyakuTankaKei - vo.YuusyouSikyuBuhinhi
                    '円にする'
                    '×100しない'
                    dic(vo.BuhinNo).SiaPrice = dic(vo.BuhinNo).SiaPrice
                End If
            Next


            For rowIndex As Integer = startRow To sheet.RowCount - 1
                Dim buhinNo As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Value()

                If StringUtil.IsNotEmpty(buhinNo) Then
                    buhinNo = buhinNo.Trim
                    '上で設定されているならいない'
                    If Not buhinNoList.Contains(buhinNo) Then
                        Continue For
                    End If

                    Dim originBuhinNo As String = ""

                    If dic.ContainsKey(buhinNo) Then
                        originBuhinNo = buhinNo
                    Else
                        If buhinNo.Length = 12 Then
                            originBuhinNo = Left(buhinNo, 10) & "##"
                        End If
                    End If

                    If Not dic.ContainsKey(originBuhinNo) Then
                        Continue For
                    End If

                    Dim kokugaiKbn As String = ""
                    Dim mixCost As String = ""
                    Dim inyoumoto As String = ""
                    If StringUtil.IsEmpty(dic(originBuhinNo).KPrice) Then
                        If StringUtil.IsEmpty(dic(originBuhinNo).SiaPrice) Then
                            Continue For
                        Else
                            kokugaiKbn = "US"
                            mixCost = dic(originBuhinNo).SiaPrice
                            inyoumoto = "パーツプライス(ｾﾝﾄ)"
                        End If
                    Else
                        kokugaiKbn = "JP"
                        mixCost = dic(originBuhinNo).KPrice
                        inyoumoto = "パーツプライス(円/個)"
                    End If

                    '0なら無視する'
                    If IsNumeric(mixCost) Then
                        If Decimal.Parse(mixCost) = 0.0 Then
                            Continue For
                        End If
                    End If

                    'どちらかあるので消す'

                    buhinNoList.Remove(buhinNo)


                    Dim key As String = EzUtil.MakeKey(kokugaiKbn, mixCost, inyoumoto)

                    Dim key2 As String = EzUtil.MakeKey(sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN)).Value, _
                                                        sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI)).Value, _
                                                        sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI)).Value)

                    If Not StringUtil.Equals(key, key2) Then
                        '部品費根拠_国外区分
                        SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN), kokugaiKbn)
                        '部品費根拠_MIXコスト部品費(円/ｾﾝﾄ)
                        SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI), mixCost)
                        '部品費根拠_引用元MIX値部品費
                        SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI), inyoumoto)
                    End If



                End If
            Next
            ReadRhac0580(dao, buhinNoList)
        End Sub

        ''' <summary>
        ''' 部品費絶対額を設定
        ''' </summary>
        ''' <param name="dao"></param>
        ''' <param name="buhinNoList"></param>
        ''' <remarks></remarks>
        Private Sub ReadRhac0580(ByVal dao As YosanSetteiBuhinEditHeaderDao, ByVal buhinNoList As List(Of String))
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            Dim vos As List(Of Rhac0580Vo) = dao.FindLatestCostItBy(buhinNoList)
            Dim dic As New Dictionary(Of String, ASPartsPriceListVo)

            '12桁だけ##にして再探索'
            Dim buhinNoList2 As New List(Of String)
            For Each buhinNo As String In buhinNoList
                If buhinNo.Length = 12 Then
                    If Not StringUtil.Equals(Right(buhinNo, 2), "##") Then
                        buhinNoList2.Add(Left(buhinNo, 10) & "##")
                    End If
                End If
            Next

            Dim vos2 As New List(Of Rhac0580Vo)
            vos2 = dao.FindLatestCostItBy(buhinNoList2)

            For Each vo As Rhac0580Vo In vos
                vo.BuhinNo = vo.BuhinNo.Trim
                If Not dic.ContainsKey(vo.BuhinNo) Then
                    Dim addVo As New ASPartsPriceListVo
                    addVo.BuhinNo = vo.BuhinNo
                    dic.Add(vo.BuhinNo, addVo)
                End If

                If StringUtil.Equals(vo.GenkaKbn, "019") Then
                    dic(vo.BuhinNo).KPrice = vo.GenkaKingaku
                ElseIf StringUtil.Equals(vo.GenkaKbn, "119") Then
                    '×100しない
                    dic(vo.BuhinNo).SiaPrice = vo.GenkaKingaku
                End If

            Next

            For Each vo As Rhac0580Vo In vos2
                vo.BuhinNo = vo.BuhinNo.Trim
                If Not dic.ContainsKey(vo.BuhinNo) Then
                    Dim addVo As New ASPartsPriceListVo
                    addVo.BuhinNo = vo.BuhinNo
                    dic.Add(vo.BuhinNo, addVo)
                End If

                If StringUtil.Equals(vo.GenkaKbn, "019") Then
                    dic(vo.BuhinNo).KPrice = vo.GenkaKingaku
                ElseIf StringUtil.Equals(vo.GenkaKbn, "119") Then
                    '×100しない
                    dic(vo.BuhinNo).SiaPrice = vo.GenkaKingaku
                End If

            Next





            For rowIndex As Integer = startRow To sheet.RowCount - 1
                Dim buhinNo As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Value()

                If StringUtil.IsNotEmpty(buhinNo) Then
                    buhinNo = buhinNo.Trim
                    '上で設定されているならいない'
                    If Not buhinNoList.Contains(buhinNo) Then
                        Continue For
                    End If

                    Dim originBuhinNo As String = ""

                    If dic.ContainsKey(buhinNo) Then
                        originBuhinNo = buhinNo
                    Else
                        If buhinNo.Length = 12 Then
                            originBuhinNo = Left(buhinNo, 10) & "##"
                        End If
                    End If

                    If Not dic.ContainsKey(originBuhinNo) Then
                        Continue For
                    End If

                    Dim kokugaiKbn As String = ""
                    Dim mixCost As String = ""
                    Dim inyoumoto As String = ""
                    If StringUtil.IsEmpty(dic(originBuhinNo).KPrice) Then
                        If StringUtil.IsEmpty(dic(originBuhinNo).SiaPrice) Then
                            Continue For
                        Else
                            kokugaiKbn = "US"
                            mixCost = dic(originBuhinNo).SiaPrice
                            inyoumoto = "質量原価表部品費絶対額(ｾﾝﾄ)"
                        End If
                    Else
                        kokugaiKbn = "JP"
                        mixCost = dic(originBuhinNo).KPrice
                        inyoumoto = "質量原価表部品費絶対額(円/個)"
                    End If

                    '0なら無視する'
                    If IsNumeric(mixCost) Then
                        If Decimal.Parse(mixCost) = 0 Then
                            Continue For
                        End If
                    End If

                    'どちらかあるので消す'
                    buhinNoList.Remove(buhinNo)

                    Dim key As String = EzUtil.MakeKey(kokugaiKbn, mixCost, inyoumoto)

                    Dim key2 As String = EzUtil.MakeKey(sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN)).Value, _
                                                        sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI)).Value, _
                                                        sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI)).Value)

                    If Not StringUtil.Equals(key, key2) Then
                        '部品費根拠_国外区分
                        SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN), kokugaiKbn)
                        '部品費根拠_MIXコスト部品費(円/ｾﾝﾄ)
                        SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI), mixCost)
                        '部品費根拠_引用元MIX値部品費
                        SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI), inyoumoto)
                    End If

                End If
            Next
            '部品設計値を設定'
            ReadBuhinhi(dao, buhinNoList)
        End Sub

        ''' <summary>
        ''' 部品設計値を設定
        ''' </summary>
        ''' <param name="dao"></param>
        ''' <param name="buhinNoList"></param>
        ''' <remarks></remarks>
        Private Sub ReadBuhinhi(ByVal dao As YosanSetteiBuhinEditHeaderDao, ByVal buhinNoList As List(Of String))
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            'BRAKUのT_VALUE_DEVを利用する'
            '設計値は開発符号単位と共用の２種類が存在する。'
            Dim vos As List(Of Rhac0580Vo) = dao.FindLatestSekkeichiBy(buhinNoList)
            Dim dic As New Dictionary(Of String, ASPartsPriceListVo)
            Dim vos2 As New List(Of Rhac0580Vo)

            '12桁だけ##にして再探索'
            Dim buhinNoList2 As New List(Of String)
            For Each buhinNo As String In buhinNoList
                If buhinNo.Length = 12 Then
                    If Not StringUtil.Equals(Right(buhinNo, 2), "##") Then
                        buhinNoList2.Add(Left(buhinNo, 10) & "##")
                    End If
                End If
            Next

            vos2 = dao.FindLatestSekkeichiBy(buhinNoList2)

            For Each vo As Rhac0580Vo In vos
                vo.BuhinNo = vo.BuhinNo.Trim
                If Not dic.ContainsKey(vo.BuhinNo) Then
                    Dim addVo As New ASPartsPriceListVo
                    addVo.BuhinNo = vo.BuhinNo
                    dic.Add(vo.BuhinNo, addVo)
                End If
                'decimalに変換'
                If StringUtil.Equals(vo.GenkaKbn, "S_G_SHISAN_BUHINHI") Then
                    dic(vo.BuhinNo).KPrice = vo.GenkaKingaku
                ElseIf StringUtil.Equals(vo.GenkaKbn, "S_G_SIA_SHISAN_BUHINHI") Then
                    '×100しない
                    dic(vo.BuhinNo).SiaPrice = vo.GenkaKingaku
                End If

            Next

            For Each vo As Rhac0580Vo In vos2
                vo.BuhinNo = vo.BuhinNo.Trim
                If Not dic.ContainsKey(vo.BuhinNo) Then
                    Dim addVo As New ASPartsPriceListVo
                    addVo.BuhinNo = vo.BuhinNo
                    dic.Add(vo.BuhinNo, addVo)
                End If
                'decimalに変換'
                If StringUtil.Equals(vo.GenkaKbn, "S_G_SHISAN_BUHINHI") Then
                    dic(vo.BuhinNo).KPrice = vo.GenkaKingaku
                ElseIf StringUtil.Equals(vo.GenkaKbn, "S_G_SIA_SHISAN_BUHINHI") Then
                    '×100しない
                    dic(vo.BuhinNo).SiaPrice = vo.GenkaKingaku
                End If

            Next

            For rowIndex As Integer = startRow To sheet.RowCount - 1
                Dim buhinNo As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Value()

                If StringUtil.IsNotEmpty(buhinNo) Then
                    buhinNo = buhinNo.Trim
                    If Not buhinNoList.Contains(buhinNo) Then
                        Continue For
                    End If
                    Dim originBuhinNo As String = ""
                    If dic.ContainsKey(buhinNo) Then
                        originBuhinNo = buhinNo
                    Else
                        If buhinNo.Length = 12 Then
                            originBuhinNo = Left(buhinNo, 10) & "##"
                        End If
                    End If

                    If Not dic.ContainsKey(originBuhinNo) Then
                        Continue For
                    End If


                    Dim kokugaiKbn As String = ""
                    Dim mixCost As String = ""
                    Dim inyoumoto As String = ""
                    If StringUtil.IsEmpty(dic(originBuhinNo).KPrice) Then
                        If StringUtil.IsEmpty(dic(originBuhinNo).SiaPrice) Then
                            Continue For
                        Else
                            kokugaiKbn = "US"
                            mixCost = dic(originBuhinNo).SiaPrice
                            inyoumoto = "設計試算部品費(ｾﾝﾄ)"
                        End If
                    Else
                        kokugaiKbn = "JP"
                        mixCost = dic(originBuhinNo).KPrice
                        inyoumoto = "設計試算部品費(円/個)"
                    End If

                    '0なら無視する'
                    If IsNumeric(mixCost) Then
                        If Decimal.Parse(mixCost) = 0 Then
                            Continue For
                        End If
                    End If

                    'どちらかあるので消す'
                    buhinNoList.Remove(buhinNo)

                    Dim key As String = EzUtil.MakeKey(kokugaiKbn, mixCost, inyoumoto)

                    Dim key2 As String = EzUtil.MakeKey(sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN)).Value, _
                                                        sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI)).Value, _
                                                        sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI)).Value)

                    If Not StringUtil.Equals(key, key2) Then
                        '部品費根拠_国外区分
                        SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN), kokugaiKbn)
                        '部品費根拠_MIXコスト部品費(円/ｾﾝﾄ)
                        SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI), mixCost)
                        '部品費根拠_引用元MIX値部品費
                        SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI), inyoumoto)
                    End If



                End If
            Next
        End Sub

#End Region

#Region "選択状態の部品を返す。"

        ''' <summary>
        ''' 選択状態の部品を返す
        ''' </summary>
        ''' <param name="rowIndexList">行リスト</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetSelectionBuhin(ByVal rowIndexList As List(Of Integer)) As List(Of String)
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)
            Dim buhinNoList As New List(Of String)

            For Each rowIndex As Integer In rowIndexList
                Dim buhinNo As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Value()
                If StringUtil.IsNotEmpty(buhinNo) Then
                    If Not buhinNoList.Contains(buhinNo) Then
                        buhinNoList.Add(buhinNo)
                    End If
                End If
            Next

            Return buhinNoList
        End Function

#End Region

#Region "選択状態の行を返す。"

        ''' <summary>
        ''' 選択状態の行を返す
        ''' </summary>
        ''' <param name="searchStartRow">検索開始行</param>
        ''' <param name="rowCount">検索行数</param>
        ''' <returns>選択行のリスト</returns>
        ''' <remarks></remarks>
        Private Function GetSelectionRows(ByVal searchStartRow As Integer, ByVal rowCount As Integer) As List(Of Integer)
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)
            Dim result As New List(Of Integer)

            If searchStartRow = -1 Then
                If rowCount = -1 Then
                    '列選択状態なので全行探索'
                    For rowIndex As Integer = startRow To sheet.RowCount - 1
                        Dim buhinNo As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Value()
                        If StringUtil.IsNotEmpty(buhinNo) Then
                            result.Add(rowIndex)
                        End If
                    Next
                Else
                    'どういう状況？'
                    Dim a As String = ""

                End If
            Else
                If rowCount = -1 Then
                    'ありえない？'
                    Dim a As String = ""
                End If

                '検索開始行からカウント数分検索(ただし、部品番号行以前はスルー)'
                For counter As Integer = 0 To rowCount - 1
                    Dim rowIndex As Integer = searchStartRow + counter
                    If rowIndex < startRow Then
                        Continue For
                    End If
                    result.Add(rowIndex)
                Next
            End If

            Return result
        End Function

#End Region

#Region "単価自動設定"

#Region "パーツプライスを設定する(購入希望も同一)"

        ''' <summary>
        ''' パーツプライスを指定列に設定する。(購入希望も同一)
        ''' </summary>
        ''' <param name="isAll">全てのセルが対象ならTRUE</param>
        ''' <remarks></remarks>
        Public Sub SetPartsPrice(ByVal isAll As Boolean)
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)
            Dim selectRange() As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelections
            Dim column As Integer = -1

            Dim rowIndexes As New List(Of Integer)

            '何かしら選択されている状態'
            If selectRange.Length > 0 Then
                If selectRange.Length = 1 Then
                    '選択されている列が割付予算か購入希望ならOK'
                    If selectRange(0).Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI).Index _
                    OrElse selectRange(0).Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI).Index Then
                        'どっちかならOK'
                        column = selectRange(0).Column
                    End If

                    rowIndexes = GetSelectionRows(selectRange(0).Row, selectRange(0).RowCount)
                Else
                    '飛び飛びは無し'

                End If
            End If

            For Each rowIndex As Integer In rowIndexes
                If rowIndex < startRow Then
                    Continue For
                End If

                Dim inyoumoto As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI)).Value
                'パーツプライスという文言が含まれていたらスライド'
                If inyoumoto.IndexOf("パーツプライス") > -1 Then
                    If isAll Then
                        SetCellValue(rowIndex, column, sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI)).Value)
                        AutoMixPrice(rowIndex)
                    Else
                        If StringUtil.IsEmpty(sheet.Cells(rowIndex, column).Value) Then
                            SetCellValue(rowIndex, column, sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI)).Value)
                            AutoMixPrice(rowIndex)
                        End If
                    End If
                End If
            Next

        End Sub

#End Region

#Region "MIXコスト×係数1を設定する"

        ''' <summary>
        ''' 画面のコストと係数
        ''' </summary>
        ''' <param name="isAll">全てのセルが対象ならTRUE</param>
        ''' <remarks></remarks>
        Public Sub SetMixCostMultiplication(ByVal isAll As Boolean)
            'MIX値に入力されている値と係数1の乗算結果を部品費に設定する。'
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)
            Dim selectRange() As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelections
            Dim rowIndexes As New List(Of Integer)
            Dim column As Integer = -1

            '何かしら選択されている状態'
            If selectRange.Length > 0 Then
                If selectRange.Length = 1 Then
                    '選択されている列が割付予算か購入希望ならOK'
                    If selectRange(0).Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI).Index Then
                        'どっちかならOK'
                        column = selectRange(0).Column
                    End If

                    rowIndexes = GetSelectionRows(selectRange(0).Row, selectRange(0).RowCount)
                Else
                    '飛び飛びは無し'

                End If
            End If


            '選択行を回していく'
            For Each rowIndex As Integer In rowIndexes
                If rowIndex < startRow Then
                    Continue For
                End If

                'MIXコストの値'
                Dim sMixCost As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI)).Value
                '係数1の値'
                Dim sKeisu As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KEISU_1)).Value

                '計算結果'
                Dim partsPrice As Decimal = 0.0

                If IsNumeric(sMixCost) AndAlso IsNumeric(sKeisu) Then
                    partsPrice = Decimal.Parse(sMixCost) * Decimal.Parse(sKeisu)
                End If

                If isAll Then
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI), partsPrice)
                    AutoMixPrice(rowIndex)
                Else
                    If StringUtil.IsEmpty(sheet.Cells(rowIndex, column).Value) Then
                        SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI), partsPrice)
                        AutoMixPrice(rowIndex)
                    End If
                End If
            Next
        End Sub
#End Region

#Region "過去購入部品優先度1"

        ''' <summary>
        ''' 過去購入部品から優先度1の価格を取得して設定
        ''' </summary>
        ''' <param name="isAll">全てのセルが対象ならTRUE</param>
        ''' <remarks></remarks>
        Public Sub SetBuyParts(ByVal isAll As Boolean)
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)
            Dim selectRange() As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelections
            Dim column As Integer = -1

            Dim rowIndexes As New List(Of Integer)

            '何かしら選択されている状態'
            If selectRange.Length > 0 Then
                If selectRange.Length = 1 Then
                    '選択されている列が割付予算か購入希望ならOK'
                    If selectRange(0).Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI).Index _
                    OrElse selectRange(0).Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI).Index Then
                        'どっちかならOK'
                        column = selectRange(0).Column
                    End If

                    rowIndexes = GetSelectionRows(selectRange(0).Row, selectRange(0).RowCount)
                Else
                    '飛び飛びは無し'

                End If
            End If

            For Each rowIndex As Integer In rowIndexes
                If rowIndex < startRow Then
                    Continue For
                End If

                Dim value As String = ""
                If column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI).Index Then
                    '過去購入部品優先度1の割付値'
                    value = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_WARITUKE_BUHIN_HI)).Value

                Else
                    '過去購入部品優先度1の購入希望単価'
                    value = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_KOUNYU_KIBOU_TANKA)).Value
                End If


                If isAll Then
                    SetCellValue(rowIndex, column, value)
                    AutoMixPrice(rowIndex)
                Else
                    If StringUtil.IsEmpty(sheet.Cells(rowIndex, column).Value) Then
                        SetCellValue(rowIndex, column, value)
                        AutoMixPrice(rowIndex)
                    End If
                End If

            Next

        End Sub
#End Region

#Region "試作開発管理表"

        ''' <summary>
        ''' 試作開発管理表から情報を取得して設定
        ''' </summary>
        ''' <param name="genchoEventCode">現調イベントコード</param>
        ''' <param name="phaseNo">フェーズ№</param>
        ''' <param name="isAll">全てのセルが対象ならTRUE</param>
        ''' <remarks></remarks>
        Public Sub ReadGencho(ByVal genchoEventCode As String, ByVal phaseNo As String, ByVal isAll As Boolean)
            'MIX値に入力されている値と係数1の乗算結果を部品費に設定する。'
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)
            Dim selectRange() As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelections
            Dim rowIndexes As New List(Of Integer)
            Dim column As Integer = -1

            '何かしら選択されている状態'
            If selectRange.Length > 0 Then
                If selectRange.Length = 1 Then
                    '選択されている列が割付予算か購入希望ならOK'
                    If selectRange(0).Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI).Index _
                    OrElse selectRange(0).Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI).Index Then
                        'どっちかならOK'
                        column = selectRange(0).Column
                    End If

                    rowIndexes = GetSelectionRows(selectRange(0).Row, selectRange(0).RowCount)
                Else
                    '飛び飛びは無し'

                End If
            End If

            '飛行機のテーブルから取得'
            'ダイアログからイベントコードを取得'
            'まずは適当にもらってくる'
            Dim dao As YosanSetteiBuhinEditHeaderDao = New YosanSetteiBuhinEditHeaderDaoImpl

            Dim gList As List(Of TFuncBuhinShisakuVo) = dao.FindByTFuncBuhinShisaku(genchoEventCode, phaseNo)

            '部品一致で探索するので同一部品はどうなる？'
            Dim dic As New Dictionary(Of String, String)

            For Each vo As TFuncBuhinShisakuVo In gList
                If Not dic.ContainsKey(vo.BuhinNo) Then
                    If column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI).Index Then
                        '割付部品費を設定'
                        dic.Add(vo.BuhinNo, StringUtil.ToString(vo.YosanBuhinSeisakuHi))
                    ElseIf selectRange(0).Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI).Index Then
                        '発注値を設定'
                        dic.Add(vo.BuhinNo, StringUtil.ToString(vo.YosanHachuChi))
                    End If
                End If
            Next

            For Each rowIndex As Integer In rowIndexes
                Dim buhinNo As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Value()
                If StringUtil.IsNotEmpty(buhinNo) Then
                    '割付予算_部品費(円)か購入希望単価
                    If dic.ContainsKey(buhinNo) Then
                        If isAll Then
                            SetCellValue(rowIndex, column, dic(buhinNo))
                            AutoMixPrice(rowIndex)
                        Else
                            If StringUtil.IsEmpty(sheet.Cells(rowIndex, column).Value) Then
                                SetCellValue(rowIndex, column, dic(buhinNo))
                                AutoMixPrice(rowIndex)
                            End If
                        End If
                    End If
                End If
            Next
        End Sub

#End Region

#Region "割付予算値スライド"

        ''' <summary>
        ''' 割付予算値を指定列に設定する。
        ''' </summary>
        ''' <param name="isAll">全てのセルが対象ならTRUE</param>
        ''' <remarks></remarks>
        Public Sub SetYosanLayout(ByVal isAll As Boolean)
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)
            Dim selectRange() As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelections
            Dim column As Integer = -1

            Dim rowIndexes As New List(Of Integer)

            '何かしら選択されている状態'
            If selectRange.Length > 0 Then
                If selectRange.Length = 1 Then
                    '選択されている列が割付予算か購入希望ならOK'
                    If selectRange(0).Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI).Index Then
                        'どっちかならOK'
                        column = selectRange(0).Column
                    End If

                    rowIndexes = GetSelectionRows(selectRange(0).Row, selectRange(0).RowCount)
                Else
                    '飛び飛びは無し'

                End If
            End If

            For Each rowIndex As Integer In rowIndexes
                If rowIndex < startRow Then
                    Continue For
                End If

                If isAll Then
                    SetCellValue(rowIndex, column, sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI)).Value)
                Else
                    If StringUtil.IsEmpty(sheet.Cells(rowIndex, column).Value) Then
                        SetCellValue(rowIndex, column, sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI)).Value)
                    End If

                End If

            Next

        End Sub

#End Region

#Region "割付予算×係数2"

        ''' <summary>
        ''' 割付予算部品費×係数2を指定列に設定する。
        ''' </summary>
        ''' <param name="isAll">全てのセルが対象ならTRUE</param>
        ''' <remarks></remarks>
        Public Sub SetYosanLayoutMulti(ByVal isAll As Boolean)
            'MIX値に入力されている値と係数1の乗算結果を部品費に設定する。'
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)
            Dim selectRange() As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelections
            Dim rowIndexes As New List(Of Integer)
            Dim column As Integer = -1

            '何かしら選択されている状態'
            If selectRange.Length > 0 Then
                If selectRange.Length = 1 Then
                    '選択されている列が割付予算か購入希望ならOK'
                    If selectRange(0).Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI).Index Then
                        'どっちかならOK'
                        column = selectRange(0).Column
                    End If

                    rowIndexes = GetSelectionRows(selectRange(0).Row, selectRange(0).RowCount)
                Else
                    '飛び飛びは無し'

                End If
            End If


            '選択行を回していく'
            For Each rowIndex As Integer In rowIndexes
                If rowIndex < startRow Then
                    Continue For
                End If

                'MIXコストの値'
                Dim sMixCost As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI)).Value
                '係数1の値'
                Dim sKeisu As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_KEISU_2)).Value

                '計算結果'
                Dim partsPrice As Decimal = 0.0

                If IsNumeric(sMixCost) AndAlso IsNumeric(sKeisu) Then
                    partsPrice = Decimal.Parse(sMixCost) * Decimal.Parse(sKeisu)
                End If

                If isAll Then
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI), partsPrice)
                Else
                    If StringUtil.IsEmpty(sheet.Cells(rowIndex, column).Value) Then
                        SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI), partsPrice)

                    End If
                End If
            Next
        End Sub

#End Region

#Region "ファンクションマスタから設定"

        Private Const UNKNOWN_MAKER_CODE As String = "ZZZZ"


        ''' <summary>
        ''' ファンクションマスタから設定
        ''' </summary>
        ''' <param name="isAll">全てのセルが対象ならTRUE</param>
        ''' <remarks></remarks>
        Public Sub SetFunctionCost(ByVal isAll As Boolean)
            '部品番号上５桁と取引先コードが一致した場合、単価を設定する。
            '例外としてマスタ側に取引先コード「ZZZZ」が存在した場合、取引先コードが不一致の箇所に単価を設定する'
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)
            Dim selectRange() As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelections
            Dim rowIndexes As New List(Of Integer)
            Dim column As Integer = -1

            '何かしら選択されている状態'
            If selectRange.Length > 0 Then
                If selectRange.Length = 1 Then
                    '選択されている列が割付予算ならOK'
                    If selectRange(0).Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI).Index Then
                        'どっちかならOK'
                        column = selectRange(0).Column
                    End If

                    rowIndexes = GetSelectionRows(selectRange(0).Row, selectRange(0).RowCount)
                Else
                    '飛び飛びは無し'

                End If
            End If


            'ファンクションマスタから情報取得'
            'まずは適当にもらってくる'
            Dim dao As YosanSetteiBuhinEditHeaderDao = New YosanSetteiBuhinEditHeaderDaoImpl

            Dim aList As List(Of TYosanSetteiBuhinFunctionVo) = dao.FindByTYosanSetteiBuhinFunction()

            Dim dic As New Dictionary(Of String, Dictionary(Of String, TYosanSetteiBuhinFunctionVo))

            For Each vo As TYosanSetteiBuhinFunctionVo In aList

                If Not dic.ContainsKey(vo.YosanFunctionHinban) Then
                    dic.Add(vo.YosanFunctionHinban, New Dictionary(Of String, TYosanSetteiBuhinFunctionVo))
                End If
                If Not dic(vo.YosanFunctionHinban).ContainsKey(vo.YosanMakerCode) Then
                    dic(vo.YosanFunctionHinban).Add(vo.YosanMakerCode, vo)
                End If
            Next

            '選択行を回していく'
            For Each rowIndex As Integer In rowIndexes
                If rowIndex < startRow Then
                    Continue For
                End If

                Dim buhinNo As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Value
                If buhinNo.Length < 5 Then
                    '5桁未満は対象外'
                    Continue For
                End If

                Dim functionNo As String = Left(buhinNo, 5) '部品番号上5桁がファンクション

                If dic.ContainsKey(functionNo) Then

                    Dim tanka As Decimal
                    Dim makerCode As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_MAKER_CODE)).Value

                    'メーカーコード一致するか？'
                    If dic(functionNo).ContainsKey(makerCode) Then
                        tanka = dic(functionNo)(makerCode).YosanTanka
                    Else
                        '一致しない場合は不明メーカーコードで代用'
                        If dic(functionNo).ContainsKey(UNKNOWN_MAKER_CODE) Then
                            tanka = dic(functionNo)(UNKNOWN_MAKER_CODE).YosanTanka
                        Else
                            '設定自体されていないならスルー'
                            Continue For
                        End If
                    End If

                    If isAll Then
                        SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI), tanka)
                        AutoMixPrice(rowIndex)
                    Else
                        If StringUtil.IsEmpty(sheet.Cells(rowIndex, column).Value) Then
                            SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI), tanka)
                            AutoMixPrice(rowIndex)
                        End If
                    End If

                End If

            Next
        End Sub
#End Region


#Region "セルに情報を設定"

        ''' <summary>
        ''' セルの情報を設定(色変更付き)
        ''' </summary>
        ''' <param name="row"></param>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Private Sub SetCellValue(ByVal row As Integer, ByVal column As Integer, ByVal value As Object)
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            If sheet.Rows(row).Visible Then
                sheet.Cells(row, column).Value = value
                SetEditRowProc(True, row, column)
            End If
        End Sub

#End Region

#End Region

#Region "シートのソート機能"

        ''' <summary>
        ''' シートのソート機能
        ''' </summary>
        ''' <param name="columnIndex">列</param>
        ''' <param name="isOrder">昇順ならtrue</param>
        ''' <remarks></remarks>
        Public Sub SheetSort(ByVal columnIndex As Integer, ByVal isOrder As Boolean)
            Dim sheetBase As SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheetBase)

            'スプレッドのソート機能を利用したほうが早いけど結合セルが崩れる。'
            Dim s(0) As FarPoint.Win.Spread.SortInfo
            s(0) = New FarPoint.Win.Spread.SortInfo(columnIndex, isOrder)
            'sheetBase.SortRows(startRow, sheetBase.RowCount - startRow - 1, s)


            'ソート結果貼り付け用のシートを用意'
            Dim sortSheet As New FarPoint.Win.Spread.SheetView()
            sortSheet.RowCount = sheetBase.RowCount
            sortSheet.ColumnCount = sheetBase.ColumnCount

            Try

                'そのままコピーしたシート用意'
                Dim NewSheet As New FarPoint.Win.Spread.SheetView
                FarPoint.Win.Serializer.SetObjectXml(DirectCast(NewSheet, FarPoint.Win.ISerializeSupport), FarPoint.Win.Serializer.GetObjectXml(DirectCast(sheetBase, FarPoint.Win.ISerializeSupport), "temp"), "temp")
                NewSheet.SheetName = "NewSheet"
                'ソートさせる(これで結合セルは全滅)'
                'NewSheet.SortRows(startRow, NewSheet.RowCount - startRow, s)   
                NewSheet.SortRange(startRow, 0, NewSheet.RowCount - startRow, NewSheet.ColumnCount, True, s)
                Dim copyData As Object(,) = NewSheet.GetArray(startRow, 0, NewSheet.RowCount - 1, NewSheet.ColumnCount - 1)

                '元のシートの情報をクリアする'
                SpreadAllClear(sheetBase)

                sheetBase.SetArray(startRow, 0, copyData)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try





            'For rowIndex As Integer = startRow To sheetBase.RowCount - 1
            '    '本来の行位置取得'
            '    Dim originRowIndex As Integer = Integer.Parse(l(rowIndex).Split("\\")(1))

            '    'クリップボードにコピーする'
            '    'sheetBase.ClipboardCopy(New FarPoint.Win.Spread.Model.CellRange(0, -1, 1, -1))
            '    'ソート結果に張り付ける。'


            'Next


        End Sub

#End Region

#Region "最新化"

        ''' <summary>
        ''' 比較して最新化
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Compare()
            Dim comp As New YosanCompareLatest(_shisakuEventCode, _shisakuListCode)
            '比較実行'
            comp.Compare()
            '比較結果受け取り'
            Dim vos As New List(Of TYosanSetteiBuhinVo)
            vos = comp.CompareList

            '履歴受け取り'
            _RirekiVos = comp.HistoryList

            '変更結果のブロックリストを受け取る'
            Dim blockList As New List(Of String)
            blockList = comp.ChangeBlockList

            '保存はしない。'
            '変更されたブロックだけ削除して変更箇所を登録'
            'RegistNewest(vos, blockList)

            '履歴の登録'
            'RegistRireki(historyVos)

            '画面設定'
            SetSpreadCompare(vos, _RirekiVos)

            '変化点探索+'
            Dim dic As New Dictionary(Of String, Dictionary(Of String, List(Of TYosanSetteiBuhinRirekiVo)))

            For Each vo As TYosanSetteiBuhinRirekiVo In _RirekiVos
                Dim key As String = EzUtil.MakeKey(vo.YosanBlockNo, vo.YosanBuhinNo, vo.BuhinNoHyoujiJun)

                If Not dic.ContainsKey(key) Then
                    dic.Add(key, New Dictionary(Of String, List(Of TYosanSetteiBuhinRirekiVo)))
                End If

                If Not dic(key).ContainsKey(vo.ColumnName) Then
                    dic(key).Add(vo.ColumnName, New List(Of TYosanSetteiBuhinRirekiVo))
                End If

                dic(key)(vo.ColumnName).Add(vo)
            Next

            '比較結果を画面に設定。'
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            For rowIndex As Integer = startRow To sheet.RowCount - 1

                '削除扱いならグレーにする'
                If StringUtil.Equals(sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_AUD_FLAG)).Value, "D") Then
                    sheet.Rows(rowIndex).BackColor = Color.Gray
                End If

                '追加扱いならグレーにする'
                If StringUtil.Equals(sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_AUD_FLAG)).Value, "A") Then
                    sheet.Rows(rowIndex).BackColor = Color.Aqua
                End If

                Dim blockNo As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Value
                Dim buhinNo As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Value
                Dim buhinNoHyoujiJun As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN)).Value

                Dim key As String = EzUtil.MakeKey(blockNo, buhinNo, buhinNoHyoujiJun)

                If dic.ContainsKey(key) Then
                    'sheet.coumnscountだと長いので'
                    For columnIndex As Integer = 0 To GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BIKOU)
                        Dim tag As String = sheet.Columns(columnIndex).Tag.ToString

                        If dic(key).ContainsKey(tag) Then
                            If StringUtil.Equals(sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_AUD_FLAG)).Value, "C") Then
                                sheet.Cells(rowIndex, GetTagIdx(sheet, tag), rowIndex, GetTagIdx(sheet, tag)).ForeColor = Color.Blue
                                sheet.Cells(rowIndex, GetTagIdx(sheet, tag), rowIndex, GetTagIdx(sheet, tag)).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                            End If
                        End If

                    Next

                End If
            Next

        End Sub

        ''' <summary>
        ''' 画面に張り付ける
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetSpreadCompare(ByVal compareVos As List(Of TYosanSetteiBuhinVo), ByVal historyVos As List(Of TYosanSetteiBuhinRirekiVo))
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1


            'スプレッドを他の画面と同様に設定
            Dim spread As FarPoint.Win.Spread.FpSpread = _frmDispTehaiEdit.spdBase
            SpreadUtil.Initialize(spread)

            '取得したデータ件数によりスプレッドの総行数を拡張(+10は必要余裕行数）
            If compareVos.Count >= sheet.RowCount Then
                sheet.RowCount = compareVos.Count + 10
            End If

            Dim startRow As Integer = GetTitleRowsIn(sheet)

            '全データ消去
            SpreadAllClear(sheet)
            _RirekiDic = New Dictionary(Of Integer, Dictionary(Of String, List(Of TYosanSetteiBuhinRirekiVo)))

            Dim dic As New Dictionary(Of String, Dictionary(Of String, List(Of TYosanSetteiBuhinRirekiVo)))
            For Each vo As TYosanSetteiBuhinRirekiVo In historyVos
                Dim key As String = EzUtil.MakeKey(vo.YosanBlockNo, vo.YosanBuhinNo, vo.BuhinNoHyoujiJun)

                If Not dic.ContainsKey(key) Then
                    dic.Add(key, New Dictionary(Of String, List(Of TYosanSetteiBuhinRirekiVo)))
                End If

                If Not dic(key).ContainsKey(vo.ColumnName) Then
                    dic(key).Add(vo.ColumnName, New List(Of TYosanSetteiBuhinRirekiVo))
                End If

                dic(key)(vo.ColumnName).Add(vo)
            Next

            Dim oRow As Integer = startRow
            For Each vo As TYosanSetteiBuhinVo In compareVos

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_AUD_FLAG)).Value = vo.AudFlag

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_AUD_FLAG)).Locked = True

                '削除扱いならグレーにする'
                If StringUtil.Equals(sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_AUD_FLAG)).Value, "D") Then
                    sheet.Rows(oRow).BackColor = Color.Gray
                    sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_AUD_FLAG)).Locked = False
                End If

                '追加扱いならグレーにする'
                If StringUtil.Equals(sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_AUD_FLAG)).Value, "A") Then
                    sheet.Rows(oRow).BackColor = Color.Aqua
                End If

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUKA_CODE)).Value = vo.YosanBukaCode
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Value = vo.YosanBlockNo

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_GYOU_ID)).Value = vo.YosanGyouId
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_LEVEL)).Value = vo.YosanLevel

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN)).Value = vo.BuhinNoHyoujiJun

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_SHUKEI_CODE)).Value = vo.YosanShukeiCode

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_SIA_SHUKEI_CODE)).Value = vo.YosanSiaShukeiCode

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Value = vo.YosanBuhinNo

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NAME)).Value = vo.YosanBuhinName

                '合計員数-1は**表記とし、0値は０を設定
                Dim totalInsu As String = CnvIntStr(vo.YosanInsu)

                '-1が来たら**を設定
                totalInsu = IIf(totalInsu.Equals("-1"), "**", totalInsu)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_INSU)).Value = totalInsu
                '
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_MAKER_CODE)).Value = vo.YosanMakerCode

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KYOUKU_SECTION)).Value = vo.YosanKyoukuSection

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUTAN)).Value = vo.YosanKoutan

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_TEHAI_KIGOU)).Value = vo.YosanTehaiKigou

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_SEISAKU)).Value = vo.YosanTsukurikataSeisaku

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_1)).Value = vo.YosanTsukurikataKatashiyou1

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_2)).Value = vo.YosanTsukurikataKatashiyou2

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_3)).Value = vo.YosanTsukurikataKatashiyou3

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_TIGU)).Value = vo.YosanTsukurikataTigu

                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KIBO)).Value = vo.YosanTsukurikataKibo

                '設計情報_試作部品費（円）
                Dim strBuhinHi As String = GetNumericDbField(vo.YosanShisakuBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_SHISAKU_BUHIN_HI)).Text = strBuhinHi
                '設計情報_試作型費（千円）
                Dim strKatahi As String = GetNumericDbField(vo.YosanShisakuKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_SHISAKU_KATA_HI)).Value = strKatahi

                '設計情報_部品ノート
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NOTE)).Value = vo.YosanBuhinNote

                '設計情報_備考
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BIKOU)).Value = vo.YosanBikou

                '部品費根拠_国外区分
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN)).Value = vo.YosanKonkyoKokugaiKbn

                '部品費根拠_MIXコスト部品費(円/ｾﾝﾄ)
                Dim strKonkyoMixBuhinHi As String = GetNumericDbField(vo.YosanKonkyoMixBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI)).Value = strKonkyoMixBuhinHi

                '部品費根拠_引用元MIX値部品費
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI)).Value = vo.YosanKonkyoInyouMixBuhinHi

                '部品費根拠_係数１
                Dim strYosanKonkyoKeisu1 As String = GetNumericDbField(vo.YosanKonkyoKeisu1)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KEISU_1)).Value = strYosanKonkyoKeisu1

                '部品費根拠_工法
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KONKYO_KOUHOU)).Value = vo.YosanKonkyoKouhou

                '割付予算_部品費(円)
                Dim strYosanWaritukeBuhinHi As String = GetNumericDbField(vo.YosanWaritukeBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI)).Value = strYosanWaritukeBuhinHi

                '割付予算_係数２
                Dim strYosanWaritukeKeisu2 As String = GetNumericDbField(vo.YosanWaritukeKeisu2)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_KEISU_2)).Value = strYosanWaritukeKeisu2

                '過去購入部品1
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_RYOSAN_TANKA)).Value = GetNumericDbField(vo.Kako1RyosanTanka)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_WARITUKE_BUHIN_HI)).Value = GetNumericDbField(vo.Kako1WaritukeBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_WARITUKE_KATA_HI)).Value = GetNumericDbField(vo.Kako1WaritukeKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_WARITUKE_KOUHOU)).Value = vo.Kako1WaritukeKouhou
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_MAKER_BUHIN_HI)).Value = GetNumericDbField(vo.Kako1MakerBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_MAKER_KATA_HI)).Value = GetNumericDbField(vo.Kako1MakerKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_MAKER_KOUHOU)).Value = vo.Kako1MakerKouhou
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_SHINGI_BUHIN_HI)).Value = GetNumericDbField(vo.Kako1ShingiBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_SHINGI_KATA_HI)).Value = GetNumericDbField(vo.Kako1ShingiKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_SHINGI_KOUHOU)).Value = vo.Kako1ShingiKouhou
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_KOUNYU_KIBOU_TANKA)).Value = GetNumericDbField(vo.Kako1KounyuKibouTanka)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_KOUNYU_TANKA)).Value = GetNumericDbField(vo.Kako1KounyuTanka)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_SHIKYU_HIN)).Value = GetNumericDbField(vo.Kako1ShikyuHin)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_KOUJI_SHIREI_NO)).Value = vo.Kako1KoujiShireiNo
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_EVENT_NAME)).Value = vo.Kako1EventName
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_HACHU_BI)).Value = DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako1HachuBi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_KENSHU_BI)).Value = DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako1KenshuBi)

                '過去購入部品2
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_RYOSAN_TANKA)).Value = GetNumericDbField(vo.Kako2RyosanTanka)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_WARITUKE_BUHIN_HI)).Value = GetNumericDbField(vo.Kako2WaritukeBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_WARITUKE_KATA_HI)).Value = GetNumericDbField(vo.Kako2WaritukeKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_WARITUKE_KOUHOU)).Value = vo.Kako2WaritukeKouhou
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_MAKER_BUHIN_HI)).Value = GetNumericDbField(vo.Kako2MakerBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_MAKER_KATA_HI)).Value = GetNumericDbField(vo.Kako2MakerKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_MAKER_KOUHOU)).Value = vo.Kako2MakerKouhou
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_SHINGI_BUHIN_HI)).Value = GetNumericDbField(vo.Kako2ShingiBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_SHINGI_KATA_HI)).Value = GetNumericDbField(vo.Kako2ShingiKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_SHINGI_KOUHOU)).Value = vo.Kako2ShingiKouhou
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_KOUNYU_KIBOU_TANKA)).Value = GetNumericDbField(vo.Kako2KounyuKibouTanka)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_KOUNYU_TANKA)).Value = GetNumericDbField(vo.Kako2KounyuTanka)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_SHIKYU_HIN)).Value = GetNumericDbField(vo.Kako2ShikyuHin)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_KOUJI_SHIREI_NO)).Value = vo.Kako2KoujiShireiNo
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_EVENT_NAME)).Value = vo.Kako2EventName
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_HACHU_BI)).Value = DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako2HachuBi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_KENSHU_BI)).Value = DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako2KenshuBi)

                '過去購入部品3
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_RYOSAN_TANKA)).Value = GetNumericDbField(vo.Kako3RyosanTanka)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_WARITUKE_BUHIN_HI)).Value = GetNumericDbField(vo.Kako3WaritukeBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_WARITUKE_KATA_HI)).Value = GetNumericDbField(vo.Kako3WaritukeKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_WARITUKE_KOUHOU)).Value = vo.Kako3WaritukeKouhou
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_MAKER_BUHIN_HI)).Value = GetNumericDbField(vo.Kako3MakerBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_MAKER_KATA_HI)).Value = GetNumericDbField(vo.Kako3MakerKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_MAKER_KOUHOU)).Value = vo.Kako3MakerKouhou
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_SHINGI_BUHIN_HI)).Value = GetNumericDbField(vo.Kako3ShingiBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_SHINGI_KATA_HI)).Value = GetNumericDbField(vo.Kako3ShingiKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_SHINGI_KOUHOU)).Value = vo.Kako3ShingiKouhou
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_KOUNYU_KIBOU_TANKA)).Value = GetNumericDbField(vo.Kako3KounyuKibouTanka)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_KOUNYU_TANKA)).Value = GetNumericDbField(vo.Kako3KounyuTanka)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_SHIKYU_HIN)).Value = GetNumericDbField(vo.Kako3ShikyuHin)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_KOUJI_SHIREI_NO)).Value = vo.Kako3KoujiShireiNo
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_EVENT_NAME)).Value = vo.Kako3EventName
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_HACHU_BI)).Value = DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako3HachuBi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_KENSHU_BI)).Value = DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako3KenshuBi)



                '過去購入部品4
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_RYOSAN_TANKA)).Value = GetNumericDbField(vo.Kako4RyosanTanka)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_WARITUKE_BUHIN_HI)).Value = GetNumericDbField(vo.Kako4WaritukeBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_WARITUKE_KATA_HI)).Value = GetNumericDbField(vo.Kako4WaritukeKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_WARITUKE_KOUHOU)).Value = vo.Kako4WaritukeKouhou
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_MAKER_BUHIN_HI)).Value = GetNumericDbField(vo.Kako4MakerBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_MAKER_KATA_HI)).Value = GetNumericDbField(vo.Kako4MakerKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_MAKER_KOUHOU)).Value = vo.Kako4MakerKouhou
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_SHINGI_BUHIN_HI)).Value = GetNumericDbField(vo.Kako4ShingiBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_SHINGI_KATA_HI)).Value = GetNumericDbField(vo.Kako4ShingiKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_SHINGI_KOUHOU)).Value = vo.Kako4ShingiKouhou
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_KOUNYU_KIBOU_TANKA)).Value = GetNumericDbField(vo.Kako4KounyuKibouTanka)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_KOUNYU_TANKA)).Value = GetNumericDbField(vo.Kako4KounyuTanka)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_SHIKYU_HIN)).Value = GetNumericDbField(vo.Kako4ShikyuHin)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_KOUJI_SHIREI_NO)).Value = vo.Kako4KoujiShireiNo
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_EVENT_NAME)).Value = vo.Kako4EventName
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_HACHU_BI)).Value = DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako4HachuBi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_KENSHU_BI)).Value = DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako4KenshuBi)

                '過去購入部品5
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_RYOSAN_TANKA)).Value = GetNumericDbField(vo.Kako5RyosanTanka)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_WARITUKE_BUHIN_HI)).Value = GetNumericDbField(vo.Kako5WaritukeBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_WARITUKE_KATA_HI)).Value = GetNumericDbField(vo.Kako5WaritukeKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_WARITUKE_KOUHOU)).Value = vo.Kako5WaritukeKouhou
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_MAKER_BUHIN_HI)).Value = GetNumericDbField(vo.Kako5MakerBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_MAKER_KATA_HI)).Value = GetNumericDbField(vo.Kako5MakerKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_MAKER_KOUHOU)).Value = vo.Kako5MakerKouhou
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_SHINGI_BUHIN_HI)).Value = GetNumericDbField(vo.Kako5ShingiBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_SHINGI_KATA_HI)).Value = GetNumericDbField(vo.Kako5ShingiKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_SHINGI_KOUHOU)).Value = vo.Kako5ShingiKouhou
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_KOUNYU_KIBOU_TANKA)).Value = GetNumericDbField(vo.Kako5KounyuKibouTanka)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_KOUNYU_TANKA)).Value = GetNumericDbField(vo.Kako5KounyuTanka)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_SHIKYU_HIN)).Value = GetNumericDbField(vo.Kako5ShikyuHin)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_KOUJI_SHIREI_NO)).Value = vo.Kako5KoujiShireiNo
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_EVENT_NAME)).Value = vo.Kako5EventName
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_HACHU_BI)).Value = DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako5HachuBi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_KENSHU_BI)).Value = DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako5KenshuBi)

                '割付予算_部品費合計(円)
                Dim strYosanWaritukeBuhinHiTotal As String = GetNumericDbField(vo.YosanWaritukeBuhinHiTotal)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI_TOTAL)).Value = strYosanWaritukeBuhinHiTotal
                '割付予算_型費(千円)
                Dim strYosanWaritukeKataHi As String = GetNumericDbField(vo.YosanWaritukeKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_KATA_HI)).Value = strYosanWaritukeKataHi
                '購入希望_購入希望単価(円)
                Dim strYoaanKounyuKibouTanka As String = GetNumericDbField(vo.YosanKounyuKibouTanka)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_TANKA)).Value = strYoaanKounyuKibouTanka
                '購入希望_部品費(円)
                Dim strYosanKounyuKibouBuhinHi As String = GetNumericDbField(vo.YosanKounyuKibouBuhinHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI)).Value = strYosanKounyuKibouBuhinHi

                '購入希望_部品費合計(円)
                Dim strYosanKounyuKibouBuhinHiTotal As String = GetNumericDbField(vo.YosanKounyuKibouBuhinHiTotal)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL)).Value = strYosanKounyuKibouBuhinHiTotal

                '購入希望_型費(円)
                Dim strYosanKounyuKibouKataHi As String = GetNumericDbField(vo.YosanKounyuKibouKataHi)
                sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_KATA_HI)).Value = strYosanKounyuKibouKataHi
                '変化点
                Dim henkaTen As String = vo.Henkaten
                If henkaTen Is Nothing Then
                    henkaTen = String.Empty
                ElseIf henkaTen.Equals("1") Then
                    henkaTen = "○"
                ElseIf henkaTen.Equals("2") Then
                    henkaTen = "△"
                End If
                If henkaTen.Equals("3") Then

                End If

                '履歴情報を設定'
                Dim blockNo As String = sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO)).Value
                Dim buhinNo As String = sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Value
                Dim buhinNoHyoujijun As String = sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN)).Value

                Dim key As String = EzUtil.MakeKey(blockNo, buhinNo, buhinNoHyoujijun)

                If dic.ContainsKey(key) Then

                    For Each tag As String In dic(key).Keys
                        Dim strMsg As String = ""
                        Dim j As Integer = 0
                        Dim strBefore As String = ""
                        Dim strAfter As String = ""

                        For Each rirekiVo As TYosanSetteiBuhinRirekiVo In dic(key)(tag)

                            If StringUtil.IsNotEmpty(rirekiVo.Before) Then
                                strBefore = rirekiVo.Before
                            Else
                                strBefore = "空白"
                            End If
                            If StringUtil.IsNotEmpty(rirekiVo.After) Then
                                strAfter = rirekiVo.After
                            Else
                                strAfter = "空白"
                            End If

                            If j > 0 Then
                                strMsg += vbCrLf & vbCrLf
                            End If
                            '更新日、変更前、変更後をセット
                            strMsg += Right(rirekiVo.UpdateBi, 5) & " : " _
                                    & strBefore & " ⇒ " & strAfter

                            '1を加算
                            j += 1

                            If Not _RirekiDic.ContainsKey(oRow) Then
                                _RirekiDic.Add(oRow, New Dictionary(Of String, List(Of TYosanSetteiBuhinRirekiVo)))
                            End If
                            If Not _RirekiDic(oRow).ContainsKey(rirekiVo.ColumnName) Then
                                _RirekiDic(oRow).Add(rirekiVo.ColumnName, New List(Of TYosanSetteiBuhinRirekiVo))
                            End If
                            _RirekiDic(oRow)(rirekiVo.ColumnName).Add(rirekiVo)

                        Next
                        sheet.Cells(oRow, GetTagIdx(sheet, tag)).NoteStyle = NoteStyle.PopupNote
                        sheet.Cells(oRow, GetTagIdx(sheet, tag)).Note = strMsg

                        If StringUtil.Equals(vo.AudFlag, "C") Then
                            sheet.Cells(oRow, GetTagIdx(sheet, tag), oRow, GetTagIdx(sheet, tag)).ForeColor = Color.Blue
                            sheet.Cells(oRow, GetTagIdx(sheet, tag), oRow, GetTagIdx(sheet, tag)).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                        End If

                    Next

                End If

                oRow += 1
            Next


        End Sub

#Region "最新化結果登録"

        ''' <summary>
        ''' 最新化結果登録
        ''' </summary>
        ''' <param name="vos">比較結果情報</param>
        ''' <param name="blockNoList">削除するブロックのリスト</param>
        ''' <remarks></remarks>
        Private Sub RegistNewest(ByVal vos As List(Of TYosanSetteiBuhinVo), ByVal blockNoList As List(Of String))

            '削除'
            Dim dao As YosanSetteiBuhinEditHeaderDao = New YosanSetteiBuhinEditHeaderDaoImpl
            For Each blockNo As String In blockNoList
                dao.DeleteByTYosanSetteiBuhin(_shisakuEventCode, _shisakuListCode, blockNo)
            Next

            Using sqlConn As New SqlClient.SqlConnection(NitteiDbComFunc.GetConnectString)
                sqlConn.Open()
                Using tr As SqlClient.SqlTransaction = sqlConn.BeginTransaction

                    Using addData As DataTable = NitteiDbComFunc.ConvListToDataTable(vos)
                        Using bulkCopy As SqlClient.SqlBulkCopy = New SqlClient.SqlBulkCopy(sqlConn, SqlClient.SqlBulkCopyOptions.KeepIdentity, tr)
                            'マッピングが必要
                            NitteiDbComFunc.SetColumnMappings(bulkCopy, addData)

                            'タイムアウト制限
                            bulkCopy.BulkCopyTimeout = 0  ' in seconds
                            '書き込み先指定
                            bulkCopy.DestinationTableName = MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN"
                            'ここで書き込み
                            bulkCopy.WriteToServer(addData)
                            bulkCopy.Close()
                        End Using
                    End Using

                    tr.Commit()
                End Using
            End Using
        End Sub


#End Region

#Region "履歴情報登録"

        ''' <summary>
        ''' 履歴情報登録
        ''' </summary>
        ''' <param name="vos"></param>
        ''' <remarks></remarks>
        Private Sub RegistRireki(ByVal vos As List(Of TYosanSetteiBuhinRirekiVo))

            Dim dao As TYosanSetteiBuhinRirekiDao = New TYosanSetteiBuhinRirekiDaoImpl

            For Each vo As TYosanSetteiBuhinRirekiVo In vos
                dao.DeleteByPk(vo.ShisakuEventCode, vo.YosanListCode, vo.YosanBukaCode, vo.YosanBlockNo, vo.YosanBuhinNo, vo.BuhinNoHyoujiJun, vo.ColumnId, vo.ColumnName, vo.UpdateBi, vo.UpdateJikan)
            Next

            Using sqlConn As New SqlClient.SqlConnection(NitteiDbComFunc.GetConnectString)
                sqlConn.Open()
                Using tr As SqlClient.SqlTransaction = sqlConn.BeginTransaction

                    Using addData As DataTable = NitteiDbComFunc.ConvListToDataTable(vos)
                        Using bulkCopy As SqlClient.SqlBulkCopy = New SqlClient.SqlBulkCopy(sqlConn, SqlClient.SqlBulkCopyOptions.KeepIdentity, tr)
                            'マッピングが必要
                            NitteiDbComFunc.SetColumnMappings(bulkCopy, addData)

                            'タイムアウト制限
                            bulkCopy.BulkCopyTimeout = 0  ' in seconds
                            '書き込み先指定
                            bulkCopy.DestinationTableName = MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_RIREKI"
                            'ここで書き込み
                            bulkCopy.WriteToServer(addData)
                            bulkCopy.Close()
                        End Using
                    End Using

                    tr.Commit()
                End Using
            End Using

        End Sub


#End Region

#End Region


#Region "過去購入部品取得"

        ''' <summary>
        ''' 過去購入部品取得
        ''' </summary>
        ''' <param name="flag">0:○発注最新順（発注日降順)1:○検収最新順（検収日降順）2:○コスト低順（検収金額昇順）</param>
        ''' <remarks></remarks>
        Public Sub PastPurchase(ByVal flag As String)
            '比較結果を画面に設定。'
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)
            Dim buhinNoList As New List(Of String)
            For rowIndex As Integer = startRow To sheet.RowCount - 1
                Dim buhinNo As String = sheet.Cells(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Value
                If StringUtil.IsNotEmpty(buhinNo) Then
                    '-付きはスペースに変換して探索'
                    If StringUtil.Equals(Left(buhinNo, 1), "-") Then
                        buhinNo = " " & Right(buhinNo, buhinNo.Length - 1)
                        buhinNo = RTrim(buhinNo)
                    End If

                    If Not buhinNoList.Contains(buhinNo) Then
                        buhinNoList.Add(buhinNo)
                    End If
                End If
            Next

            Dim dao As YosanSetteiBuhinEditHeaderDao = New YosanSetteiBuhinEditHeaderDaoImpl
            Dim result1 As New List(Of TYosanSetteiBuhinVo)
            result1 = dao.FindByPastPurchase(buhinNoList, flag)

            'Dim result2 As New List(Of TYosanSetteiBuhinVo)
            'result2 = dao.FindByPastPurchase(buhinNoList, "1")

            'Dim result3 As New List(Of TYosanSetteiBuhinVo)
            'result3 = dao.FindByPastPurchase(buhinNoList, "2")


            Dim dic1 As New Dictionary(Of String, List(Of TYosanSetteiBuhinVo))

            For Each vo As TYosanSetteiBuhinVo In result1
                If StringUtil.Equals(Left(vo.YosanBuhinNo, 1), " ") Then
                    vo.YosanBuhinNo = "-" & LTrim(vo.YosanBuhinNo)
                End If


                If Not dic1.ContainsKey(vo.YosanBuhinNo) Then
                    dic1.Add(vo.YosanBuhinNo, New List(Of TYosanSetteiBuhinVo))
                End If

                dic1(vo.YosanBuhinNo).Add(vo)
            Next

            For oRow As Integer = startRow To sheet.RowCount - 1
                Dim buhinNo As String = sheet.Cells(oRow, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_BUHIN_NO)).Value
                If StringUtil.IsNotEmpty(buhinNo) Then
                    If dic1.ContainsKey(buhinNo) Then
                        For index As Integer = 0 To dic1(buhinNo).Count - 1
                            SetKakoTanka(sheet, oRow, index + 1, dic1(buhinNo)(index))
                        Next
                    End If
                End If
            Next
        End Sub


        Private Sub SetKakoTanka(ByVal sheet As SheetView, ByVal rowIndex As Integer, ByVal index As Integer, ByVal vo As TYosanSetteiBuhinVo)

            Select Case index
                Case 1
                    '過去購入部品1
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_RYOSAN_TANKA), vo.Kako1RyosanTanka)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_WARITUKE_BUHIN_HI), vo.Kako1WaritukeBuhinHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_WARITUKE_KATA_HI), vo.Kako1WaritukeKataHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_WARITUKE_KOUHOU), vo.Kako1WaritukeKouhou)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_MAKER_BUHIN_HI), vo.Kako1MakerBuhinHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_MAKER_KATA_HI), vo.Kako1MakerKataHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_MAKER_KOUHOU), vo.Kako1MakerKouhou)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_SHINGI_BUHIN_HI), vo.Kako1ShingiBuhinHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_SHINGI_KATA_HI), vo.Kako1ShingiKataHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_SHINGI_KOUHOU), vo.Kako1ShingiKouhou)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_KOUNYU_KIBOU_TANKA), vo.Kako1KounyuKibouTanka)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_KOUNYU_TANKA), vo.Kako1KounyuTanka)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_SHIKYU_HIN), vo.Kako1ShikyuHin)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_KOUJI_SHIREI_NO), vo.Kako1KoujiShireiNo)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_EVENT_NAME), vo.Kako1EventName)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_HACHU_BI), DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako1HachuBi))
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_1_KENSHU_BI), DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako1KenshuBi))



                Case 2
                    '過去購入部品2
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_RYOSAN_TANKA), vo.Kako1RyosanTanka)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_WARITUKE_BUHIN_HI), vo.Kako1WaritukeBuhinHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_WARITUKE_KATA_HI), vo.Kako1WaritukeKataHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_WARITUKE_KOUHOU), vo.Kako1WaritukeKouhou)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_MAKER_BUHIN_HI), vo.Kako1MakerBuhinHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_MAKER_KATA_HI), vo.Kako1MakerKataHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_MAKER_KOUHOU), vo.Kako1MakerKouhou)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_SHINGI_BUHIN_HI), vo.Kako1ShingiBuhinHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_SHINGI_KATA_HI), vo.Kako1ShingiKataHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_SHINGI_KOUHOU), vo.Kako1ShingiKouhou)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_KOUNYU_KIBOU_TANKA), vo.Kako1KounyuKibouTanka)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_KOUNYU_TANKA), vo.Kako1KounyuTanka)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_SHIKYU_HIN), vo.Kako1ShikyuHin)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_KOUJI_SHIREI_NO), vo.Kako1KoujiShireiNo)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_EVENT_NAME), vo.Kako1EventName)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_HACHU_BI), DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako1HachuBi))
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_2_KENSHU_BI), DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako1KenshuBi))


                Case 3
                    '過去購入部品3
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_RYOSAN_TANKA), vo.Kako1RyosanTanka)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_WARITUKE_BUHIN_HI), vo.Kako1WaritukeBuhinHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_WARITUKE_KATA_HI), vo.Kako1WaritukeKataHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_WARITUKE_KOUHOU), vo.Kako1WaritukeKouhou)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_MAKER_BUHIN_HI), vo.Kako1MakerBuhinHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_MAKER_KATA_HI), vo.Kako1MakerKataHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_MAKER_KOUHOU), vo.Kako1MakerKouhou)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_SHINGI_BUHIN_HI), vo.Kako1ShingiBuhinHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_SHINGI_KATA_HI), vo.Kako1ShingiKataHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_SHINGI_KOUHOU), vo.Kako1ShingiKouhou)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_KOUNYU_KIBOU_TANKA), vo.Kako1KounyuKibouTanka)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_KOUNYU_TANKA), vo.Kako1KounyuTanka)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_SHIKYU_HIN), vo.Kako1ShikyuHin)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_KOUJI_SHIREI_NO), vo.Kako1KoujiShireiNo)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_EVENT_NAME), vo.Kako1EventName)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_HACHU_BI), DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako1HachuBi))
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_3_KENSHU_BI), DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako1KenshuBi))


                Case 4
                    '過去購入部品4
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_RYOSAN_TANKA), vo.Kako1RyosanTanka)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_WARITUKE_BUHIN_HI), vo.Kako1WaritukeBuhinHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_WARITUKE_KATA_HI), vo.Kako1WaritukeKataHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_WARITUKE_KOUHOU), vo.Kako1WaritukeKouhou)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_MAKER_BUHIN_HI), vo.Kako1MakerBuhinHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_MAKER_KATA_HI), vo.Kako1MakerKataHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_MAKER_KOUHOU), vo.Kako1MakerKouhou)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_SHINGI_BUHIN_HI), vo.Kako1ShingiBuhinHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_SHINGI_KATA_HI), vo.Kako1ShingiKataHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_SHINGI_KOUHOU), vo.Kako1ShingiKouhou)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_KOUNYU_KIBOU_TANKA), vo.Kako1KounyuKibouTanka)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_KOUNYU_TANKA), vo.Kako1KounyuTanka)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_SHIKYU_HIN), vo.Kako1ShikyuHin)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_KOUJI_SHIREI_NO), vo.Kako1KoujiShireiNo)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_EVENT_NAME), vo.Kako1EventName)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_HACHU_BI), DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako1HachuBi))
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_4_KENSHU_BI), DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako1KenshuBi))

                Case 5
                    '過去購入部品5
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_RYOSAN_TANKA), vo.Kako1RyosanTanka)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_WARITUKE_BUHIN_HI), vo.Kako1WaritukeBuhinHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_WARITUKE_KATA_HI), vo.Kako1WaritukeKataHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_WARITUKE_KOUHOU), vo.Kako1WaritukeKouhou)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_MAKER_BUHIN_HI), vo.Kako1MakerBuhinHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_MAKER_KATA_HI), vo.Kako1MakerKataHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_MAKER_KOUHOU), vo.Kako1MakerKouhou)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_SHINGI_BUHIN_HI), vo.Kako1ShingiBuhinHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_SHINGI_KATA_HI), vo.Kako1ShingiKataHi)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_SHINGI_KOUHOU), vo.Kako1ShingiKouhou)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_KOUNYU_KIBOU_TANKA), vo.Kako1KounyuKibouTanka)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_KOUNYU_TANKA), vo.Kako1KounyuTanka)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_SHIKYU_HIN), vo.Kako1ShikyuHin)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_KOUJI_SHIREI_NO), vo.Kako1KoujiShireiNo)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_EVENT_NAME), vo.Kako1EventName)
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_HACHU_BI), DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako1HachuBi))
                    SetCellValue(rowIndex, GetTagIdx(sheet, NmSpdTagBase.TAG_KAKO_5_KENSHU_BI), DateUtil.ConvYyyymmddToSlashYyyymmdd(vo.Kako1KenshuBi))

                Case Else
                    Dim a As String = ""
            End Select



        End Sub

#End Region

#Region "過去購入部品列の表示と非表示を変更"

        ''' <summary>
        ''' 過去購入部品列の表示と非表示を変更
        ''' </summary>
        ''' <param name="isVisible"></param>
        ''' <remarks></remarks>
        Public Sub HiddenPastParchase(ByVal isVisible As Boolean)
            Dim sheet As SheetView = _frmDispTehaiEdit.spdBase_Sheet1

            '過去購入部品列の表示と非表示を変更
            '１'
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_RYOSAN_TANKA).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_WARITUKE_BUHIN_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_WARITUKE_KATA_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_WARITUKE_KOUHOU).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_MAKER_BUHIN_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_MAKER_KATA_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_MAKER_KOUHOU).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_SHINGI_BUHIN_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_SHINGI_KATA_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_SHINGI_KOUHOU).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_KOUNYU_KIBOU_TANKA).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_KOUNYU_TANKA).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_SHIKYU_HIN).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_KOUJI_SHIREI_NO).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_EVENT_NAME).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_HACHU_BI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_1_KENSHU_BI).Visible = isVisible
            '2'
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_RYOSAN_TANKA).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_WARITUKE_BUHIN_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_WARITUKE_KATA_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_WARITUKE_KOUHOU).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_MAKER_BUHIN_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_MAKER_KATA_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_MAKER_KOUHOU).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_SHINGI_BUHIN_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_SHINGI_KATA_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_SHINGI_KOUHOU).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_KOUNYU_KIBOU_TANKA).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_KOUNYU_TANKA).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_SHIKYU_HIN).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_KOUJI_SHIREI_NO).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_EVENT_NAME).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_HACHU_BI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_2_KENSHU_BI).Visible = isVisible
            '3'
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_RYOSAN_TANKA).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_WARITUKE_BUHIN_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_WARITUKE_KATA_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_WARITUKE_KOUHOU).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_MAKER_BUHIN_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_MAKER_KATA_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_MAKER_KOUHOU).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_SHINGI_BUHIN_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_SHINGI_KATA_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_SHINGI_KOUHOU).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_KOUNYU_KIBOU_TANKA).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_KOUNYU_TANKA).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_SHIKYU_HIN).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_KOUJI_SHIREI_NO).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_EVENT_NAME).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_HACHU_BI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_3_KENSHU_BI).Visible = isVisible
            '4'
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_RYOSAN_TANKA).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_WARITUKE_BUHIN_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_WARITUKE_KATA_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_WARITUKE_KOUHOU).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_MAKER_BUHIN_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_MAKER_KATA_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_MAKER_KOUHOU).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_SHINGI_BUHIN_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_SHINGI_KATA_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_SHINGI_KOUHOU).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_KOUNYU_KIBOU_TANKA).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_KOUNYU_TANKA).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_SHIKYU_HIN).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_KOUJI_SHIREI_NO).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_EVENT_NAME).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_HACHU_BI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_4_KENSHU_BI).Visible = isVisible
            '5'
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_RYOSAN_TANKA).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_WARITUKE_BUHIN_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_WARITUKE_KATA_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_WARITUKE_KOUHOU).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_MAKER_BUHIN_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_MAKER_KATA_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_MAKER_KOUHOU).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_SHINGI_BUHIN_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_SHINGI_KATA_HI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_SHINGI_KOUHOU).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_KOUNYU_KIBOU_TANKA).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_KOUNYU_TANKA).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_SHIKYU_HIN).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_KOUJI_SHIREI_NO).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_EVENT_NAME).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_HACHU_BI).Visible = isVisible
            sheet.Columns(NmSpdTagBase.TAG_KAKO_5_KENSHU_BI).Visible = isVisible


        End Sub

#End Region

#Region "部品費端数処理"

        ''' <summary>
        ''' 部品費端数処理
        ''' </summary>
        ''' <param name="pros">0:切り上げ 1:切り捨て 2:四捨五入</param>
        ''' <param name="unit">桁</param>
        ''' <param name="flag">0：そのまま、1：0になるものは無視、2：キャンセル</param>
        ''' <remarks></remarks>
        Public Sub Fraction(ByVal pros As String, ByVal unit As String, ByVal flag As Integer)

            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)
            Dim selectRange() As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelections
            Dim rowIndexes As New List(Of Integer)
            Dim column As Integer = -1

            '何かしら選択されている状態'
            If selectRange.Length > 0 Then
                If selectRange.Length = 1 Then

                    If selectRange(0).Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI).Index _
                    OrElse selectRange(0).Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI).Index Then
                        'どっちかならOK'
                        column = selectRange(0).Column
                    End If

                    rowIndexes = GetSelectionRows(selectRange(0).Row, selectRange(0).RowCount)
                Else
                    '飛び飛びは無し'

                End If
            End If


            '選択行を回していく'
            For Each rowIndex As Integer In rowIndexes
                If rowIndex < startRow Then
                    Continue For
                End If

                'MIXコストの値'
                Dim Cost As String = sheet.Cells(rowIndex, column).Value
                Dim result As String = ""
                If StringUtil.IsNotEmpty(Cost) Then
                    If IsNumeric(Cost) Then
                        Dim value As Decimal = Decimal.Parse(Cost)  'decimalにする'
                        Dim figure As Decimal = Integer.Parse(unit)  '対象桁

                        Select Case pros
                            '少数点以下にして処理してから戻す'
                            Case "0"
                                '切り上げ'
                                result = StringUtil.ToString(Math.Ceiling(value / figure) * figure)
                            Case "1"
                                '切り捨て'
                                result = StringUtil.ToString(Math.Floor(value / figure) * figure)
                            Case "2"
                                '四捨五入'
                                result = StringUtil.ToString(Math.Round(value / figure, MidpointRounding.AwayFromZero) * figure)
                        End Select

                    End If
                End If

                '0無視の場合'
                If flag = 1 Then
                    If result = 0 Then
                        Continue For
                    End If
                End If

                '計算結果'
                If sheet.Rows(rowIndex).Visible Then
                    sheet.Cells(rowIndex, column).Value = result
                End If

                AutoMixPrice(rowIndex)
            Next


        End Sub

        ''' <summary>
        ''' 部品費端数処理前に確認する処理
        ''' </summary>
        ''' <param name="pros">0:切り上げ 1:切り捨て 2:四捨五入</param>
        ''' <param name="unit">桁</param>
        ''' <remarks>処理中に0になる行がいたらfalse</remarks>
        Public Function FractionTest(ByVal pros As String, ByVal unit As String) As Boolean

            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1
            Dim startRow As Integer = GetTitleRowsIn(sheet)
            Dim selectRange() As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelections
            Dim rowIndexes As New List(Of Integer)
            Dim column As Integer = -1

            '何かしら選択されている状態'
            If selectRange.Length > 0 Then
                If selectRange.Length = 1 Then

                    If selectRange(0).Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI).Index _
                    OrElse selectRange(0).Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI).Index Then
                        'どっちかならOK'
                        column = selectRange(0).Column
                    End If

                    rowIndexes = GetSelectionRows(selectRange(0).Row, selectRange(0).RowCount)
                Else
                    '飛び飛びは無し'

                End If
            End If


            '選択行を回していく'
            For Each rowIndex As Integer In rowIndexes
                If rowIndex < startRow Then
                    Continue For
                End If

                'MIXコストの値'
                Dim Cost As String = sheet.Cells(rowIndex, column).Value
                Dim result As String = ""
                If StringUtil.IsNotEmpty(Cost) Then
                    If IsNumeric(Cost) Then
                        Dim value As Decimal = Decimal.Parse(Cost)  'decimalにする'
                        Dim figure As Decimal = Integer.Parse(unit)  '対象桁

                        Select Case pros
                            '少数点以下にして処理してから戻す'
                            Case "0"
                                '切り上げ'
                                result = StringUtil.ToString(Math.Ceiling(value / figure) * figure)
                            Case "1"
                                '切り捨て'
                                result = StringUtil.ToString(Math.Floor(value / figure) * figure)
                            Case "2"
                                '四捨五入'
                                result = StringUtil.ToString(Math.Round(value / figure, MidpointRounding.AwayFromZero) * figure)
                        End Select

                    End If
                End If

                '計算結果'
                If sheet.Rows(rowIndex).Visible Then
                    If result = 0 Then
                        Return False
                    End If

                End If
            Next

            Return True
        End Function


#End Region

#Region "割付予算部品費合計の自動計算"

        ''' <summary>
        ''' 割付予算部品費合計の自動計算
        ''' </summary>
        ''' <param name="row"></param>
        ''' <remarks></remarks>
        Public Sub AutoMixPrice(ByVal row As Integer)
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1

            Dim insu As String = sheet.Cells(row, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_INSU)).Value
            Dim partsPrice As String = sheet.Cells(row, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI)).Value

            Dim buhinhi As String = sheet.Cells(row, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI)).Value
            Dim katahi As String = sheet.Cells(row, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_KATA_HI)).Value

            If Not IsNumeric(buhinhi) Then
                buhinhi = "0"
            End If
            If Not IsNumeric(katahi) Then
                katahi = "0"
            End If


            If IsNumeric(insu) AndAlso IsNumeric(partsPrice) Then
                Dim result As Decimal = Decimal.Parse(partsPrice) * Integer.Parse(insu)
                '変化ないならスルー'
                Dim originValue As String = sheet.Cells(row, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI_TOTAL)).Value
                If StringUtil.IsEmpty(originValue) Then
                    originValue = "0"
                End If
                If IsNumeric(originValue) Then
                    If originValue <> result Then
                        If sheet.Rows(row).Visible Then
                            SetCellValue(row, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI_TOTAL), result)
                        End If

                    End If
                End If



            End If

            '購入希望単価:部品費合計'
            If IsNumeric(insu) AndAlso IsNumeric(buhinhi) Then
                Dim result As Decimal = Decimal.Parse(buhinhi) * Integer.Parse(insu)

                '変化ないならスルー'
                Dim originValue As String = sheet.Cells(row, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL)).Value
                If StringUtil.IsEmpty(originValue) Then
                    originValue = "0"
                End If
                If IsNumeric(originValue) Then
                    If originValue <> result Then
                        If sheet.Rows(row).Visible Then
                            SetCellValue(row, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL), result)
                        End If

                    End If
                End If


            End If


            '購入希望単価:購入希望単価'
            If IsNumeric(insu) AndAlso IsNumeric(buhinhi) AndAlso IsNumeric(katahi) Then
                Dim result As Decimal = 0
                '千倍する'
                Dim katahiCost As Decimal = Decimal.Parse(katahi) * 1000
                '型費/総数'
                result = katahiCost / Integer.Parse(insu)
                '+部品費'
                result = result + Decimal.Parse(buhinhi)


                '変化ないならスルー'
                Dim originValue As String = sheet.Cells(row, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_TANKA)).Value

                If StringUtil.IsEmpty(originValue) Then
                    originValue = "0"
                End If

                If IsNumeric(originValue) Then
                    If originValue <> result Then
                        If sheet.Rows(row).Visible Then
                            SetCellValue(row, GetTagIdx(sheet, NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_TANKA), result)
                        End If

                    End If
                End If


            End If



        End Sub


#End Region

#Region "予算設定部部品表 主キー検索により更新処理判定しVOを作成"
        ''' <summary>
        ''' 予算設定部品表 主キー検索により更新処理判定しVOを作成
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <param name="trans"></param>
        ''' <param name="aDtSpreadBase">対象スプレッド行の情報を格納したDataRow</param>
        ''' <param name="aIdxNo">スプレッド行位置</param>
        ''' <param name="startRow">スプレッド開始行位置</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SpdToBaseVo(ByVal aDb As SqlClient.SqlConnection, ByVal trans As SqlClient.SqlTransaction, _
                                     ByVal aDtSpreadBase As DataTable, ByVal aIdxNo As Integer, ByVal startRow As Integer, _
                                     ByVal aDtBase As DataTable, ByVal sortJun As Integer) As TYosanSetteiBuhinVo

            Dim rtnVo As New TYosanSetteiBuhinVo
            Dim aRow As Integer = aIdxNo + startRow
            Dim aSheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1

            Dim voKihon As TYosanSetteiBuhinVo = YosanSetteiBuhinEditImpl.FindPkBaseInfo(aDb, _
                                                                                  trans, _
                                                        _headerSubject.shisakuEventCode, _
                                                        _headerSubject.shisakuListCode, _
                                                        aDtSpreadBase.Rows(aIdxNo)(NmDTColBase.TD_YOSAN_BUKA_CODE), _
                                                        aDtSpreadBase.Rows(aIdxNo)(NmDTColBase.TD_YOSAN_BLOCK_NO), _
                                                        aDtSpreadBase.Rows(aIdxNo)(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN))


            'スプレッド行位置取得

            With rtnVo
                '条件項目
                .ShisakuEventCode = _headerSubject.shisakuEventCode
                .YosanListCode = _headerSubject.shisakuListCode
                .YosanBukaCode = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_BUKA_CODE))
                .YosanBlockNo = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_BLOCK_NO))
                .BuhinNoHyoujiJun = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN))
                '
                .YosanSortJun = sortJun
                '
                .AudFlag = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_AUD_FLAG))
                .YosanGyouId = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_GYOU_ID))
                .YosanLevel = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_LEVEL))
                .YosanShukeiCode = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_SHUKEI_CODE))
                .YosanSiaShukeiCode = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_SIA_SHUKEI_CODE))
                .YosanBuhinNo = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_BUHIN_NO))
                .YosanBuhinName = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_BUHIN_NAME))
                '員数
                Dim strInsu As String = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_YOSAN_INSU))
                If strInsu.Equals(String.Empty) = False Then
                    '**なら-1に変換する。
                    If strInsu.Equals("**") = True Then
                        strInsu = "-1"
                    Else
                        If IsNumeric(strInsu) = False Then
                            Throw New Exception(String.Format("員数項目の数値変換で問題が発生しました。:{0}", strInsu))
                        End If
                    End If
                Else
                    strInsu = "0"
                End If
                .YosanInsu = Integer.Parse(strInsu)
                '
                .YosanMakerCode = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                          NmSpdTagBase.TAG_YOSAN_MAKER_CODE))
                .YosanKyoukuSection = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_KYOUKU_SECTION))
                .YosanKoutan = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_KOUTAN))
                .YosanTehaiKigou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_TEHAI_KIGOU))
                .YosanTsukurikataSeisaku = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_SEISAKU))
                .YosanTsukurikataKatashiyou1 = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_1))
                .YosanTsukurikataKatashiyou2 = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_2))
                .YosanTsukurikataKatashiyou3 = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_3))
                .YosanTsukurikataTigu = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_TIGU))
                .YosanTsukurikataKibo = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KIBO))
                '部品費
                Dim strBuhinHi As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                     NmSpdTagBase.TAG_YOSAN_SHISAKU_BUHIN_HI))
                If strBuhinHi Is Nothing = False AndAlso strBuhinHi.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strBuhinHi) = False Then
                        Throw New Exception(String.Format("部品費項目の数値変換で問題が発生しました。:{0}", strBuhinHi))
                    End If
                Else
                    strBuhinHi = "0"
                End If
                .YosanShisakuBuhinHi = Integer.Parse(strBuhinHi)
                '型費
                Dim strKataHi As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_YOSAN_SHISAKU_KATA_HI))
                If strKataHi Is Nothing = False AndAlso strKataHi.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strKataHi) = False Then
                        Throw New Exception(String.Format("型費項目の数値変換で問題が発生しました。:{0}", strKataHi))
                    End If
                Else
                    strKataHi = "0"
                End If
                .YosanShisakuKataHi = Integer.Parse(strKataHi)
                '
                .YosanBuhinNote = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_BUHIN_NOTE))
                .YosanBikou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_BIKOU))
                .YosanKonkyoKokugaiKbn = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN))
                '部品費根拠_MIXコスト部品費(円/ｾﾝﾄ)
                Dim strKonkyoMixBuhinHi As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI))
                If strKonkyoMixBuhinHi Is Nothing = False AndAlso strKonkyoMixBuhinHi.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strKonkyoMixBuhinHi) = False Then
                        Throw New Exception(String.Format("部品費根拠_MIXコスト部品費(円/ｾﾝﾄ)項目の数値変換で問題が発生しました。:{0}", strKonkyoMixBuhinHi))
                    End If
                Else
                    strKonkyoMixBuhinHi = "0"
                End If
                .YosanKonkyoMixBuhinHi = Decimal.Parse(strKonkyoMixBuhinHi)
                '
                .YosanKonkyoInyouMixBuhinHi = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI))
                '部品費根拠_係数１
                Dim strKonkyoKeisu1 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_YOSAN_KONKYO_KEISU_1))
                If strKonkyoKeisu1 Is Nothing = False AndAlso strKonkyoKeisu1.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strKonkyoKeisu1) = False Then
                        Throw New Exception(String.Format("部品費根拠_係数１項目の数値変換で問題が発生しました。:{0}", strKonkyoKeisu1))
                    End If
                Else
                    strKonkyoKeisu1 = "0"
                End If
                .YosanKonkyoKeisu1 = Decimal.Parse(strKonkyoKeisu1)
                '
                .YosanKonkyoKouhou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_YOSAN_KONKYO_KOUHOU))
                '割付予算_部品費(円)
                Dim strWaritukeBuhinHi As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI))
                If strWaritukeBuhinHi Is Nothing = False AndAlso strWaritukeBuhinHi.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strWaritukeBuhinHi) = False Then
                        Throw New Exception(String.Format("割付予算_部品費(円)項目の数値変換で問題が発生しました。:{0}", strWaritukeBuhinHi))
                    End If
                Else
                    strWaritukeBuhinHi = "0"
                End If
                .YosanWaritukeBuhinHi = Decimal.Parse(strWaritukeBuhinHi)
                '割付予算_係数２
                Dim strWaritukeKeisu2 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_YOSAN_WARITUKE_KEISU_2))
                If strWaritukeKeisu2 Is Nothing = False AndAlso strWaritukeKeisu2.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strWaritukeKeisu2) = False Then
                        Throw New Exception(String.Format("割付予算_係数２項目の数値変換で問題が発生しました。:{0}", strWaritukeKeisu2))
                    End If
                Else
                    strWaritukeKeisu2 = "0"
                End If
                .YosanWaritukeKeisu2 = Decimal.Parse(strWaritukeKeisu2)
                '割付予算_部品費合計(円)
                Dim strWaritukeBuhinHiTotal As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI_TOTAL))
                If strWaritukeBuhinHiTotal Is Nothing = False AndAlso strWaritukeBuhinHiTotal.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strWaritukeBuhinHiTotal) = False Then
                        Throw New Exception(String.Format("割付予算_部品費合計(円)項目の数値変換で問題が発生しました。:{0}", strWaritukeBuhinHiTotal))
                    End If
                Else
                    strWaritukeBuhinHiTotal = "0"
                End If
                .YosanWaritukeBuhinHiTotal = Decimal.Parse(strWaritukeBuhinHiTotal)
                '割付予算_型費(千円)
                Dim strWaritukeKataHi As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_YOSAN_WARITUKE_KATA_HI))
                If strWaritukeKataHi Is Nothing = False AndAlso strWaritukeKataHi.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strWaritukeKataHi) = False Then
                        Throw New Exception(String.Format("割付予算_型費(千円)項目の数値変換で問題が発生しました。:{0}", strWaritukeKataHi))
                    End If
                Else
                    strWaritukeKataHi = "0"
                End If
                .YosanWaritukeKataHi = Decimal.Parse(strWaritukeKataHi)
                '購入希望_購入希望単価(円)
                Dim strKounyuKibouTanka As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_TANKA))
                If strKounyuKibouTanka Is Nothing = False AndAlso strKounyuKibouTanka.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strKounyuKibouTanka) = False Then
                        Throw New Exception(String.Format("購入希望_購入希望単価(円)項目の数値変換で問題が発生しました。:{0}", strKounyuKibouTanka))
                    End If
                Else
                    strKounyuKibouTanka = "0"
                End If
                .YosanKounyuKibouTanka = Decimal.Parse(strKounyuKibouTanka)
                '購入希望_部品費(円)
                Dim strKounyuKibouBuhinHi As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI))
                If strKounyuKibouBuhinHi Is Nothing = False AndAlso strKounyuKibouBuhinHi.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strKounyuKibouBuhinHi) = False Then
                        Throw New Exception(String.Format("購入希望_部品費(円)項目の数値変換で問題が発生しました。:{0}", strKounyuKibouBuhinHi))
                    End If
                Else
                    strKounyuKibouBuhinHi = "0"
                End If
                .YosanKounyuKibouBuhinHi = Decimal.Parse(strKounyuKibouBuhinHi)
                '購入希望_部品費合計(円)
                Dim strKounyuKibouBuhinHiTotal As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL))
                If strKounyuKibouBuhinHiTotal Is Nothing = False AndAlso strKounyuKibouBuhinHiTotal.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strKounyuKibouBuhinHiTotal) = False Then
                        Throw New Exception(String.Format("購入希望_部品費合計(円)項目の数値変換で問題が発生しました。:{0}", strKounyuKibouBuhinHiTotal))
                    End If
                Else
                    strKounyuKibouBuhinHiTotal = "0"
                End If
                .YosanKounyuKibouBuhinHiTotal = Decimal.Parse(strKounyuKibouBuhinHiTotal)
                '購入希望_型費(円)
                Dim strKounyuKibouKataHi As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_KATA_HI))
                If strKounyuKibouKataHi Is Nothing = False AndAlso strKounyuKibouKataHi.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strKounyuKibouKataHi) = False Then
                        Throw New Exception(String.Format("購入希望_型費(円)項目の数値変換で問題が発生しました。:{0}", strKounyuKibouKataHi))
                    End If
                Else
                    strKounyuKibouKataHi = "0"
                End If
                .YosanKounyuKibouKataHi = Decimal.Parse(strKounyuKibouKataHi)
                '--------------------------------------------------------------------
                '①量産単価（円）
                Dim str01 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_1_RYOSAN_TANKA))
                If str01 Is Nothing = False AndAlso str01.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str01) = False Then
                        Throw New Exception(String.Format("①量産単価（円）項目の数値変換で問題が発生しました。:{0}", str01))
                    End If
                Else
                    str01 = "0"
                End If
                .Kako1RyosanTanka = Decimal.Parse(str01)
                '①割付部品費（円）
                Dim str02 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_1_WARITUKE_BUHIN_HI))
                If str02 Is Nothing = False AndAlso str02.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str02) = False Then
                        Throw New Exception(String.Format("①割付部品費（円）項目の数値変換で問題が発生しました。:{0}", str02))
                    End If
                Else
                    str02 = "0"
                End If
                .Kako1WaritukeBuhinHi = Decimal.Parse(str02)
                '①割付型費（千円）
                Dim str03 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_1_WARITUKE_KATA_HI))
                If str03 Is Nothing = False AndAlso str03.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str03) = False Then
                        Throw New Exception(String.Format("①割付型費（千円）項目の数値変換で問題が発生しました。:{0}", str03))
                    End If
                Else
                    str03 = "0"
                End If
                .Kako1WaritukeKataHi = Decimal.Parse(str03)
                '①割付工法
                .Kako1WaritukeKouhou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_1_WARITUKE_KOUHOU))
                '①ﾒｰｶｰ値部品費（円）
                Dim str04 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_1_MAKER_BUHIN_HI))
                If str04 Is Nothing = False AndAlso str04.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str04) = False Then
                        Throw New Exception(String.Format("①ﾒｰｶｰ値部品費（円）項目の数値変換で問題が発生しました。:{0}", str04))
                    End If
                Else
                    str04 = "0"
                End If
                .Kako1MakerBuhinHi = Decimal.Parse(str04)
                '①ﾒｰｶｰ値型費（千円）
                Dim str05 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_1_MAKER_KATA_HI))
                If str05 Is Nothing = False AndAlso str05.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str05) = False Then
                        Throw New Exception(String.Format("①ﾒｰｶｰ値型費（千円）項目の数値変換で問題が発生しました。:{0}", str05))
                    End If
                Else
                    str05 = "0"
                End If
                .Kako1MakerKataHi = Decimal.Parse(str05)
                '①ﾒｰｶｰ値工法
                .Kako1MakerKouhou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_1_MAKER_KOUHOU))
                '①審議値部品費（円）
                Dim str06 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_1_SHINGI_BUHIN_HI))
                If str06 Is Nothing = False AndAlso str06.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str06) = False Then
                        Throw New Exception(String.Format("①審議値部品費（円）項目の数値変換で問題が発生しました。:{0}", str06))
                    End If
                Else
                    str06 = "0"
                End If
                .Kako1ShingiBuhinHi = Decimal.Parse(str06)
                '①審議値型費（千円）
                Dim str07 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_1_SHINGI_KATA_HI))
                If str07 Is Nothing = False AndAlso str07.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str07) = False Then
                        Throw New Exception(String.Format("①審議値型費（千円）項目の数値変換で問題が発生しました。:{0}", str07))
                    End If
                Else
                    str07 = "0"
                End If
                .Kako1ShingiKataHi = Decimal.Parse(str07)
                '①審議値工法
                .Kako1ShingiKouhou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_1_SHINGI_KOUHOU))
                '①購入希望単価（円）
                Dim str08 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_1_KOUNYU_KIBOU_TANKA))
                If str08 Is Nothing = False AndAlso str08.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str08) = False Then
                        Throw New Exception(String.Format("①購入希望単価（円）項目の数値変換で問題が発生しました。:{0}", str08))
                    End If
                Else
                    str08 = "0"
                End If
                .Kako1KounyuKibouTanka = Decimal.Parse(str08)
                '①購入単価（円）
                Dim str09 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_1_KOUNYU_TANKA))
                If str09 Is Nothing = False AndAlso str09.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str09) = False Then
                        Throw New Exception(String.Format("①購入単価（円）項目の数値変換で問題が発生しました。:{0}", str09))
                    End If
                Else
                    str09 = "0"
                End If
                .Kako1KounyuTanka = Decimal.Parse(str09)
                '①支給品（円）
                Dim str10 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_1_SHIKYU_HIN))
                If str10 Is Nothing = False AndAlso str10.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str10) = False Then
                        Throw New Exception(String.Format("①支給品（円）項目の数値変換で問題が発生しました。:{0}", str10))
                    End If
                Else
                    str10 = "0"
                End If
                .Kako1ShikyuHin = Decimal.Parse(str10)
                '①工事指令№
                .Kako1KoujiShireiNo = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_1_KOUJI_SHIREI_NO))
                '①イベント名称
                .Kako1EventName = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_1_EVENT_NAME))
                '①発注日
                Dim int01 As Integer
                int01 = YosanSetteiBuhinEditLogic.ConvInt8Date(aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_KAKO_1_HACHU_BI)))
                .Kako1HachuBi = int01
                '①検収日
                Dim int02 As Integer
                int02 = YosanSetteiBuhinEditLogic.ConvInt8Date(aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_KAKO_1_KENSHU_BI)))
                .Kako1KenshuBi = int02
                '--------------------------------------------------------------------
                '--------------------------------------------------------------------
                '②量産単価（円）
                Dim str201 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_2_RYOSAN_TANKA))
                If str201 Is Nothing = False AndAlso str201.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str201) = False Then
                        Throw New Exception(String.Format("②量産単価（円）項目の数値変換で問題が発生しました。:{0}", str201))
                    End If
                Else
                    str201 = "0"
                End If
                .Kako2RyosanTanka = Decimal.Parse(str201)
                '②割付部品費（円）
                Dim str202 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_2_WARITUKE_BUHIN_HI))
                If str202 Is Nothing = False AndAlso str202.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str202) = False Then
                        Throw New Exception(String.Format("②割付部品費（円）項目の数値変換で問題が発生しました。:{0}", str202))
                    End If
                Else
                    str202 = "0"
                End If
                .Kako2WaritukeBuhinHi = Decimal.Parse(str202)
                '②割付型費（千円）
                Dim str203 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_2_WARITUKE_KATA_HI))
                If str203 Is Nothing = False AndAlso str203.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str203) = False Then
                        Throw New Exception(String.Format("②割付型費（千円）項目の数値変換で問題が発生しました。:{0}", str203))
                    End If
                Else
                    str203 = "0"
                End If
                .Kako2WaritukeKataHi = Decimal.Parse(str203)
                '②割付工法
                .Kako2WaritukeKouhou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_2_WARITUKE_KOUHOU))
                Dim str205 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_2_MAKER_KATA_HI))
                If str205 Is Nothing = False AndAlso str205.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str205) = False Then
                        Throw New Exception(String.Format("②ﾒｰｶｰ値型費（千円）項目の数値変換で問題が発生しました。:{0}", str205))
                    End If
                Else
                    str205 = "0"
                End If
                .Kako2MakerKataHi = Decimal.Parse(str205)
                '②ﾒｰｶｰ値工法
                .Kako2MakerKouhou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_2_MAKER_KOUHOU))
                '②審議値部品費（円）
                Dim str206 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_2_SHINGI_BUHIN_HI))
                If str206 Is Nothing = False AndAlso str206.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str206) = False Then
                        Throw New Exception(String.Format("②審議値部品費（円）項目の数値変換で問題が発生しました。:{0}", str206))
                    End If
                Else
                    str206 = "0"
                End If
                .Kako2ShingiBuhinHi = Decimal.Parse(str206)
                '②審議値型費（千円）
                Dim str207 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_2_SHINGI_KATA_HI))
                If str207 Is Nothing = False AndAlso str207.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str07) = False Then
                        Throw New Exception(String.Format("②審議値型費（千円）項目の数値変換で問題が発生しました。:{0}", str207))
                    End If
                Else
                    str207 = "0"
                End If
                .Kako2ShingiKataHi = Decimal.Parse(str207)
                '②審議値工法
                .Kako2ShingiKouhou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_2_SHINGI_KOUHOU))
                '②購入希望単価（円）
                Dim str208 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_2_KOUNYU_KIBOU_TANKA))
                If str208 Is Nothing = False AndAlso str208.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str208) = False Then
                        Throw New Exception(String.Format("②購入希望単価（円）項目の数値変換で問題が発生しました。:{0}", str208))
                    End If
                Else
                    str208 = "0"
                End If
                .Kako2KounyuKibouTanka = Decimal.Parse(str208)
                '②購入単価（円）
                Dim str209 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_2_KOUNYU_TANKA))
                If str209 Is Nothing = False AndAlso str209.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str209) = False Then
                        Throw New Exception(String.Format("②購入単価（円）項目の数値変換で問題が発生しました。:{0}", str209))
                    End If
                Else
                    str209 = "0"
                End If
                .Kako2KounyuTanka = Decimal.Parse(str209)
                '②支給品（円）
                Dim str210 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_2_SHIKYU_HIN))
                If str210 Is Nothing = False AndAlso str210.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str210) = False Then
                        Throw New Exception(String.Format("②支給品（円）項目の数値変換で問題が発生しました。:{0}", str210))
                    End If
                Else
                    str210 = "0"
                End If
                .Kako2ShikyuHin = Decimal.Parse(str210)
                '②工事指令№
                .Kako2KoujiShireiNo = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_2_KOUJI_SHIREI_NO))
                '②イベント名称
                .Kako2EventName = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_2_EVENT_NAME))
                '②発注日
                Dim int201 As Integer
                int201 = YosanSetteiBuhinEditLogic.ConvInt8Date(aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_KAKO_2_HACHU_BI)))
                .Kako2HachuBi = int201
                '②検収日
                Dim int202 As Integer
                int202 = YosanSetteiBuhinEditLogic.ConvInt8Date(aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_KAKO_2_KENSHU_BI)))
                .Kako2KenshuBi = int202
                '--------------------------------------------------------------------
                '--------------------------------------------------------------------
                '③量産単価（円）
                Dim str301 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_3_RYOSAN_TANKA))
                If str301 Is Nothing = False AndAlso str301.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str301) = False Then
                        Throw New Exception(String.Format("③量産単価（円）項目の数値変換で問題が発生しました。:{0}", str301))
                    End If
                Else
                    str301 = "0"
                End If
                .Kako3RyosanTanka = Decimal.Parse(str301)
                '③割付部品費（円）
                Dim str302 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_3_WARITUKE_BUHIN_HI))
                If str302 Is Nothing = False AndAlso str302.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str302) = False Then
                        Throw New Exception(String.Format("③割付部品費（円）項目の数値変換で問題が発生しました。:{0}", str302))
                    End If
                Else
                    str302 = "0"
                End If
                .Kako3WaritukeBuhinHi = Decimal.Parse(str302)
                '③割付型費（千円）
                Dim str303 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_3_WARITUKE_KATA_HI))
                If str303 Is Nothing = False AndAlso str303.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str303) = False Then
                        Throw New Exception(String.Format("③割付型費（千円）項目の数値変換で問題が発生しました。:{0}", str303))
                    End If
                Else
                    str303 = "0"
                End If
                .Kako3WaritukeKataHi = Decimal.Parse(str303)
                '③割付工法
                .Kako3WaritukeKouhou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_3_WARITUKE_KOUHOU))
                '③ﾒｰｶｰ値部品費（円）
                Dim str304 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_3_MAKER_BUHIN_HI))
                If str304 Is Nothing = False AndAlso str304.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str304) = False Then
                        Throw New Exception(String.Format("③ﾒｰｶｰ値部品費（円）項目の数値変換で問題が発生しました。:{0}", str304))
                    End If
                Else
                    str304 = "0"
                End If
                .Kako3MakerBuhinHi = Decimal.Parse(str304)
                '③ﾒｰｶｰ値型費（千円）
                Dim str305 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_3_MAKER_KATA_HI))
                If str305 Is Nothing = False AndAlso str305.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str305) = False Then
                        Throw New Exception(String.Format("③ﾒｰｶｰ値型費（千円）項目の数値変換で問題が発生しました。:{0}", str305))
                    End If
                Else
                    str305 = "0"
                End If
                .Kako3MakerKataHi = Decimal.Parse(str305)
                '③ﾒｰｶｰ値工法
                .Kako3MakerKouhou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_3_MAKER_KOUHOU))
                '③審議値部品費（円）
                Dim str306 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_3_SHINGI_BUHIN_HI))
                If str306 Is Nothing = False AndAlso str306.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str306) = False Then
                        Throw New Exception(String.Format("③審議値部品費（円）項目の数値変換で問題が発生しました。:{0}", str306))
                    End If
                Else
                    str306 = "0"
                End If
                .Kako3ShingiBuhinHi = Decimal.Parse(str306)
                '③審議値型費（千円）
                Dim str307 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_3_SHINGI_KATA_HI))
                If str307 Is Nothing = False AndAlso str307.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str307) = False Then
                        Throw New Exception(String.Format("③審議値型費（千円）項目の数値変換で問題が発生しました。:{0}", str307))
                    End If
                Else
                    str307 = "0"
                End If
                .Kako3ShingiKataHi = Decimal.Parse(str307)
                '③審議値工法
                .Kako3ShingiKouhou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_3_SHINGI_KOUHOU))
                '③購入希望単価（円）
                Dim str308 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_3_KOUNYU_KIBOU_TANKA))
                If str308 Is Nothing = False AndAlso str308.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str308) = False Then
                        Throw New Exception(String.Format("③購入希望単価（円）項目の数値変換で問題が発生しました。:{0}", str308))
                    End If
                Else
                    str308 = "0"
                End If
                .Kako3KounyuKibouTanka = Decimal.Parse(str308)
                '③購入単価（円）
                Dim str309 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_3_KOUNYU_TANKA))
                If str309 Is Nothing = False AndAlso str309.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str309) = False Then
                        Throw New Exception(String.Format("③購入単価（円）項目の数値変換で問題が発生しました。:{0}", str309))
                    End If
                Else
                    str309 = "0"
                End If
                .Kako3KounyuTanka = Decimal.Parse(str309)
                '③支給品（円）
                Dim str310 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_3_SHIKYU_HIN))
                If str310 Is Nothing = False AndAlso str310.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str310) = False Then
                        Throw New Exception(String.Format("③支給品（円）項目の数値変換で問題が発生しました。:{0}", str310))
                    End If
                Else
                    str310 = "0"
                End If
                .Kako3ShikyuHin = Decimal.Parse(str310)
                '③工事指令№
                .Kako3KoujiShireiNo = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_3_KOUJI_SHIREI_NO))
                '③イベント名称
                .Kako3EventName = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_3_EVENT_NAME))
                '③発注日
                Dim int301 As Integer
                int301 = YosanSetteiBuhinEditLogic.ConvInt8Date(aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_KAKO_3_HACHU_BI)))
                .Kako3HachuBi = int301
                '③検収日
                Dim int302 As Integer
                int302 = YosanSetteiBuhinEditLogic.ConvInt8Date(aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_KAKO_3_KENSHU_BI)))
                .Kako3KenshuBi = int302
                '--------------------------------------------------------------------
                '--------------------------------------------------------------------
                '④量産単価（円）
                Dim str401 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_4_RYOSAN_TANKA))
                If str401 Is Nothing = False AndAlso str401.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str401) = False Then
                        Throw New Exception(String.Format("④量産単価（円）項目の数値変換で問題が発生しました。:{0}", str401))
                    End If
                Else
                    str401 = "0"
                End If
                .Kako4RyosanTanka = Decimal.Parse(str401)
                '④割付部品費（円）
                Dim str402 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_4_WARITUKE_BUHIN_HI))
                If str402 Is Nothing = False AndAlso str402.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str402) = False Then
                        Throw New Exception(String.Format("④割付部品費（円）項目の数値変換で問題が発生しました。:{0}", str402))
                    End If
                Else
                    str402 = "0"
                End If
                .Kako4WaritukeBuhinHi = Decimal.Parse(str402)
                '④割付型費（千円）
                Dim str403 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_4_WARITUKE_KATA_HI))
                If str403 Is Nothing = False AndAlso str403.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str403) = False Then
                        Throw New Exception(String.Format("④割付型費（千円）項目の数値変換で問題が発生しました。:{0}", str403))
                    End If
                Else
                    str403 = "0"
                End If
                .Kako4WaritukeKataHi = Decimal.Parse(str403)
                '④割付工法
                .Kako4WaritukeKouhou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_4_WARITUKE_KOUHOU))
                '④ﾒｰｶｰ値部品費（円）
                Dim str404 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_4_MAKER_BUHIN_HI))
                If str404 Is Nothing = False AndAlso str404.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str404) = False Then
                        Throw New Exception(String.Format("④ﾒｰｶｰ値部品費（円）項目の数値変換で問題が発生しました。:{0}", str404))
                    End If
                Else
                    str404 = "0"
                End If
                .Kako4MakerBuhinHi = Decimal.Parse(str404)
                '④ﾒｰｶｰ値型費（千円）
                Dim str405 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_4_MAKER_KATA_HI))
                If str405 Is Nothing = False AndAlso str405.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str405) = False Then
                        Throw New Exception(String.Format("④ﾒｰｶｰ値型費（千円）項目の数値変換で問題が発生しました。:{0}", str405))
                    End If
                Else
                    str405 = "0"
                End If
                .Kako4MakerKataHi = Decimal.Parse(str405)
                '④ﾒｰｶｰ値工法
                .Kako4MakerKouhou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_4_MAKER_KOUHOU))
                '④審議値部品費（円）
                Dim str406 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_4_SHINGI_BUHIN_HI))
                If str406 Is Nothing = False AndAlso str406.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str406) = False Then
                        Throw New Exception(String.Format("④審議値部品費（円）項目の数値変換で問題が発生しました。:{0}", str406))
                    End If
                Else
                    str406 = "0"
                End If
                .Kako4ShingiBuhinHi = Decimal.Parse(str406)
                '④審議値型費（千円）
                Dim str407 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_4_SHINGI_KATA_HI))
                If str407 Is Nothing = False AndAlso str407.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str407) = False Then
                        Throw New Exception(String.Format("④審議値型費（千円）項目の数値変換で問題が発生しました。:{0}", str407))
                    End If
                Else
                    str407 = "0"
                End If
                .Kako4ShingiKataHi = Decimal.Parse(str407)
                '④審議値工法
                .Kako4ShingiKouhou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_4_SHINGI_KOUHOU))
                '④購入希望単価（円）
                Dim str408 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_4_KOUNYU_KIBOU_TANKA))
                If str408 Is Nothing = False AndAlso str408.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str408) = False Then
                        Throw New Exception(String.Format("④購入希望単価（円）項目の数値変換で問題が発生しました。:{0}", str408))
                    End If
                Else
                    str408 = "0"
                End If
                .Kako4KounyuKibouTanka = Decimal.Parse(str408)
                '④購入単価（円）
                Dim str409 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_4_KOUNYU_TANKA))
                If str409 Is Nothing = False AndAlso str409.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str409) = False Then
                        Throw New Exception(String.Format("④購入単価（円）項目の数値変換で問題が発生しました。:{0}", str409))
                    End If
                Else
                    str409 = "0"
                End If
                .Kako4KounyuTanka = Decimal.Parse(str409)
                '④支給品（円）
                Dim str410 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_4_SHIKYU_HIN))
                If str410 Is Nothing = False AndAlso str410.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str410) = False Then
                        Throw New Exception(String.Format("④支給品（円）項目の数値変換で問題が発生しました。:{0}", str410))
                    End If
                Else
                    str410 = "0"
                End If
                .Kako4ShikyuHin = Decimal.Parse(str410)
                '④工事指令№
                .Kako4KoujiShireiNo = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_4_KOUJI_SHIREI_NO))
                '④イベント名称
                .Kako4EventName = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_4_EVENT_NAME))
                '④発注日
                Dim int401 As Integer
                int401 = YosanSetteiBuhinEditLogic.ConvInt8Date(aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_KAKO_4_HACHU_BI)))
                .Kako4HachuBi = int401
                '④検収日
                Dim int402 As Integer
                int402 = YosanSetteiBuhinEditLogic.ConvInt8Date(aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_KAKO_4_KENSHU_BI)))
                .Kako4KenshuBi = int402
                '--------------------------------------------------------------------
                '--------------------------------------------------------------------
                '⑤量産単価（円）
                Dim str501 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_5_RYOSAN_TANKA))
                If str501 Is Nothing = False AndAlso str501.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str501) = False Then
                        Throw New Exception(String.Format("⑤量産単価（円）項目の数値変換で問題が発生しました。:{0}", str501))
                    End If
                Else
                    str501 = "0"
                End If
                .Kako5RyosanTanka = Decimal.Parse(str501)
                '⑤割付部品費（円）
                Dim str502 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_5_WARITUKE_BUHIN_HI))
                If str502 Is Nothing = False AndAlso str502.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str502) = False Then
                        Throw New Exception(String.Format("⑤割付部品費（円）項目の数値変換で問題が発生しました。:{0}", str502))
                    End If
                Else
                    str502 = "0"
                End If
                .Kako5WaritukeBuhinHi = Decimal.Parse(str502)
                '⑤割付型費（千円）
                Dim str503 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_5_WARITUKE_KATA_HI))
                If str503 Is Nothing = False AndAlso str503.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str503) = False Then
                        Throw New Exception(String.Format("⑤割付型費（千円）項目の数値変換で問題が発生しました。:{0}", str503))
                    End If
                Else
                    str503 = "0"
                End If
                .Kako5WaritukeKataHi = Decimal.Parse(str503)
                '⑤割付工法
                .Kako5WaritukeKouhou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_5_WARITUKE_KOUHOU))
                '⑤ﾒｰｶｰ値部品費（円）
                Dim str504 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_5_MAKER_BUHIN_HI))
                If str504 Is Nothing = False AndAlso str504.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str504) = False Then
                        Throw New Exception(String.Format("⑤ﾒｰｶｰ値部品費（円）項目の数値変換で問題が発生しました。:{0}", str504))
                    End If
                Else
                    str504 = "0"
                End If
                .Kako5MakerBuhinHi = Decimal.Parse(str504)
                '⑤ﾒｰｶｰ値型費（千円）
                Dim str505 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_5_MAKER_KATA_HI))
                If str505 Is Nothing = False AndAlso str505.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str505) = False Then
                        Throw New Exception(String.Format("⑤ﾒｰｶｰ値型費（千円）項目の数値変換で問題が発生しました。:{0}", str505))
                    End If
                Else
                    str505 = "0"
                End If
                .Kako5MakerKataHi = Decimal.Parse(str505)
                '⑤ﾒｰｶｰ値工法
                .Kako5MakerKouhou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_5_MAKER_KOUHOU))
                '⑤審議値部品費（円）
                Dim str506 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_5_SHINGI_BUHIN_HI))
                If str506 Is Nothing = False AndAlso str506.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str506) = False Then
                        Throw New Exception(String.Format("⑤審議値部品費（円）項目の数値変換で問題が発生しました。:{0}", str506))
                    End If
                Else
                    str506 = "0"
                End If
                .Kako5ShingiBuhinHi = Decimal.Parse(str506)
                '⑤審議値型費（千円）
                Dim str507 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_5_SHINGI_KATA_HI))
                If str507 Is Nothing = False AndAlso str507.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str507) = False Then
                        Throw New Exception(String.Format("⑤審議値型費（千円）項目の数値変換で問題が発生しました。:{0}", str507))
                    End If
                Else
                    str507 = "0"
                End If
                .Kako5ShingiKataHi = Decimal.Parse(str507)
                '⑤審議値工法
                .Kako5ShingiKouhou = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_5_SHINGI_KOUHOU))
                '⑤購入希望単価（円）
                Dim str508 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_5_KOUNYU_KIBOU_TANKA))
                If str508 Is Nothing = False AndAlso str508.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str508) = False Then
                        Throw New Exception(String.Format("⑤購入希望単価（円）項目の数値変換で問題が発生しました。:{0}", str508))
                    End If
                Else
                    str508 = "0"
                End If
                .Kako5KounyuKibouTanka = Decimal.Parse(str508)
                '⑤購入単価（円）
                Dim str509 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_5_KOUNYU_TANKA))
                If str509 Is Nothing = False AndAlso str509.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str509) = False Then
                        Throw New Exception(String.Format("⑤購入単価（円）項目の数値変換で問題が発生しました。:{0}", str509))
                    End If
                Else
                    str509 = "0"
                End If
                .Kako5KounyuTanka = Decimal.Parse(str509)
                '⑤支給品（円）
                Dim str510 As String = aSheet.GetValue(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                                    NmSpdTagBase.TAG_KAKO_5_SHIKYU_HIN))
                If str510 Is Nothing = False AndAlso str510.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(str510) = False Then
                        Throw New Exception(String.Format("⑤支給品（円）項目の数値変換で問題が発生しました。:{0}", str510))
                    End If
                Else
                    str510 = "0"
                End If
                .Kako5ShikyuHin = Decimal.Parse(str510)
                '⑤工事指令№
                .Kako5KoujiShireiNo = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_5_KOUJI_SHIREI_NO))
                '⑤イベント名称
                .Kako5EventName = aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, _
                                                                                          NmSpdTagBase.TAG_KAKO_5_EVENT_NAME))
                '⑤発注日
                Dim int501 As Integer
                int501 = YosanSetteiBuhinEditLogic.ConvInt8Date(aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_KAKO_5_HACHU_BI)))
                .Kako5HachuBi = int501
                '⑤検収日
                Dim int502 As Integer
                int502 = YosanSetteiBuhinEditLogic.ConvInt8Date(aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_KAKO_5_KENSHU_BI)))
                .Kako5KenshuBi = int502
                '--------------------------------------------------------------------
                .UpdatedUserId = LoginInfo.Now.UserId
                .UpdatedDate = aDate.CurrentDateDbFormat
                .UpdatedTime = aDate.CurrentTimeDbFormat
            End With


            If voKihon IsNot Nothing Then
                '更新
                With rtnVo
                    '.AudFlag = voKihon.AudFlag
                    .AudBi = voKihon.AudBi

                    '変化点
                    If String.Equals(aSheet.GetText(aRow, YosanSetteiBuhinEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_HENKATEN)), "削") Then
                        .Henkaten = "3"
                    Else
                        .Henkaten = voKihon.Henkaten
                    End If
                    .CreatedUserId = voKihon.CreatedUserId
                    .CreatedDate = voKihon.CreatedDate
                    .CreatedTime = voKihon.CreatedTime

                End With

            Else
                ''追加
                With rtnVo

                    .CreatedUserId = LoginInfo.Now.UserId
                    .CreatedDate = aDate.CurrentDateDbFormat
                    .CreatedTime = aDate.CurrentTimeDbFormat

                End With

            End If

            Return rtnVo

        End Function
#End Region

#Region "おまけ(エクセルにコメントを出力する機能)"

        ''' <summary>
        ''' おまけ(エクセルにコメントを出力する機能)
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <remarks></remarks>
        Private Sub SetExcelComment(ByVal xls As ShisakuExcel)
            '2016/03/02 kabasawa'
            'コメント出せるか実験'
            'Spread7.0だとSpreadの機能で出力できるけど3.0Jだとコメントは出力できない仕様...'
            'とりあえず一致するものだけ'

            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispTehaiEdit.spdBase_Sheet1

            For Each rowIndex As Integer In _RirekiDic.Keys
                Dim setRow As Integer = rowIndex + 5

                Dim buhinNo As String = sheet.Cells(rowIndex, sheet.Columns(NmSpdTagBase.TAG_YOSAN_BUHIN_NO).Index).Value
                If StringUtil.IsEmpty(buhinNo) Then
                    Continue For
                End If


                For Each tag As String In _RirekiDic(rowIndex).Keys
                    Dim comment As String = ""
                    Dim j As Integer = 0
                    Dim strBefore As String = ""
                    Dim strAfter As String = ""

                    For Each vo As TYosanSetteiBuhinRirekiVo In _RirekiDic(rowIndex)(tag)
                        If Not StringUtil.Equals(buhinNo.Trim, vo.YosanBuhinNo.Trim) Then
                            Exit For
                        End If

                        If j > 0 Then
                            comment += vbCrLf & vbCrLf
                        End If


                        If StringUtil.IsNotEmpty(vo.Before) Then
                            strBefore = vo.Before
                        Else
                            strBefore = "空白"
                        End If
                        If StringUtil.IsNotEmpty(vo.After) Then
                            strAfter = vo.After
                        Else
                            strAfter = "空白"
                        End If


                        '更新日、変更前、変更後をセット
                        comment += Right(vo.UpdateBi, 5) & " : " _
                                & strBefore & " ⇒ " & strAfter

                        '1を加算
                        j += 1
                    Next
                    xls.AddComment(sheet.Columns(tag).Index + 1, setRow, comment)
                Next
            Next
            'ここまで'

        End Sub


#End Region


    End Class

#End Region

End Namespace
