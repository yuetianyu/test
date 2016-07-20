Imports FarPoint.Win.Spread
Imports FarPoint.Win
Imports FarPoint
Imports EBom.Data
Imports EBom.Common
Imports EBom.Common.mdlConstraint
Imports ShisakuCommon

Namespace NokiIkkatuSettei

#Region "名称クラス"

    ''' <summary>
    ''' 購入品スプレッドタグ名称
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmSpKonyuTag
        Public Const TAG_BLOCK As String = "TAG_BLOCK"
        Public Const TAG_NOUKI As String = "TAG_NOUKI"
        Public Const TAG_SENYOU_NOUKI As String = "TAG_SENYOU_NOUKI"
        Public Const TAG_KYOUYOU_NOUKI As String = "TAG_KYOUYOU_NOUKI"
    End Class
    ''' <summary>
    ''' 取引先スプレッドタグ名称
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NmSpMaker
        Public Const TAG_NOUKI_SHITEIBI As String = "TAG_NOUKI_SHITEIBI"
        Public Const TAG_MAKER As String = "TAG_MAKER"
    End Class

    Public Class NmDtMaker
        '取引先納期格納
        Public Const MAKERCODE As String = "MAKER_CODE"
        Public Const NOUKI_SHITEIBI As String = "NOUKI_SHITEIBI"
        Public Const GENCHO_KBN As String = "GENCHO_KBN"

    End Class
    Public Class NmDtKonyu
        '購入先納期格納
        Public Const BLOCK As String = "BLOCK"
        Public Const NOUKI As String = "NOUKI"
        Public Const SENYOU_NOUKI As String = "SENYOU_NOUKI"
        Public Const KYOYOU_NOUKI As String = "KYOUNOU_NOUKI"
        Public Const GENCHO_KBN As String = "GENCHO_KBN"
    End Class

#End Region

    ''' <summary>
    ''' 納期一括設定
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Public Class NokiIkkatsuSetteiLogic
#Region "プライベート変数"


        Private _frmDispNokiIkkatsuSettei As Frm21DispNokiIkkatuSettei
        Private _spdMaker As Spread.FpSpread
        Private _spdMakerGencho As Spread.FpSpread
        Private _spdKounyu As FpSpread
        Private _spdKounyuGencho As FpSpread
        Private _dtMakerMaster As DataTable
        Private _comboCellMaker As New Spread.CellType.ComboBoxCellType

        '取引先指定格納
        Private _dtMakerNouki As DataTable
        '購入品
        Private _dtKounyuNouki As DataTable
#End Region

#Region "定数"
        Private Const SPD_MAKER_ROW_COUNT As Integer = 20
        Public Const BLK_1000 As String = "#1000"
        Public Const BLK_2000 As String = "#2000"
        Public Const BLK_3000 As String = "#3000"
        Public Const BLK_4000 As String = "#4000"
        Public Const BLK_5000 As String = "#5000"
        Public Const BLK_6000 As String = "#6000"
        Public Const BLK_7000 As String = "#7000"
        Public Const BLK_8000 As String = "#8000"
        Public Const BLK_9000 As String = "#9000"
        Public Const BLK_ALL As String = "一括設定"
#End Region

#Region "コンストラクタ"
        ''' <summary>
        ''' 画面情報を受けるコンストラクタ
        ''' </summary>
        ''' <param name="aFrmDispNokiIkkatsuSettei"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal aFrmDispNokiIkkatsuSettei As Frm21DispNokiIkkatuSettei)

            _frmDispNokiIkkatsuSettei = aFrmDispNokiIkkatsuSettei
            _spdMaker = _frmDispNokiIkkatsuSettei.spdMaker
            _spdMakerGencho = _frmDispNokiIkkatsuSettei.spdMakerGencho
            _spdKounyu = _frmDispNokiIkkatsuSettei.spdKonyu
            _spdKounyuGencho = _frmDispNokiIkkatsuSettei.spdKonyuGencho

        End Sub

#End Region

#Region "初期化"

#Region "初期化実行"

        Public Sub Initialize()

            _frmDispNokiIkkatsuSettei.Refresh()
            InitSpread()
            initDataTable()
            initControl()

        End Sub

#End Region

#Region "スプレッド初期化"
        ''' <summary>
        '''スプレッド初期化 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitSpread()

            InitMakerComboBox()
            InitSpreadKounyu()
            InitSpuredColDefaultDate(False)
            InitSpuredColDefaultDate(True)

        End Sub

#End Region

#Region "購入品納期スプレッド初期化"
        ''' <summary>
        ''' 購入品納期スプレッド初期化
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitSpreadKounyu()
            '専用品、共用品別設定をOFF
            SetSpSenyouKyouyou_Off(False)
            SetSpSenyouKyouyou_Off(True)

            'ブロック表示初期設定
            Dim kSheet As Spread.SheetView = _spdKounyu.Sheets(0)
            Dim gSheet As Spread.SheetView = _spdKounyuGencho.Sheets(0)

            kSheet.SetText(0, 0, BLK_1000)
            kSheet.SetText(1, 0, BLK_2000)
            kSheet.SetText(2, 0, BLK_3000)
            kSheet.SetText(3, 0, BLK_4000)
            kSheet.SetText(4, 0, BLK_5000)
            kSheet.SetText(5, 0, BLK_6000)
            kSheet.SetText(6, 0, BLK_7000)
            kSheet.SetText(7, 0, BLK_8000)
            kSheet.SetText(8, 0, BLK_9000)
            kSheet.SetText(9, 0, BLK_ALL)

            gSheet.SetText(0, 0, BLK_1000)
            gSheet.SetText(1, 0, BLK_2000)
            gSheet.SetText(2, 0, BLK_3000)
            gSheet.SetText(3, 0, BLK_4000)
            gSheet.SetText(4, 0, BLK_5000)
            gSheet.SetText(5, 0, BLK_6000)
            gSheet.SetText(6, 0, BLK_7000)
            gSheet.SetText(7, 0, BLK_8000)
            gSheet.SetText(8, 0, BLK_9000)
            gSheet.SetText(9, 0, BLK_ALL)
        End Sub
#End Region

#Region "スプレッド日付列の初期日付設定"
        ''' <summary>
        ''' スプレッド日付列の初期日付設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitSpuredColDefaultDate(ByVal aGencho As Boolean)
            Dim sheetMaker As Spread.SheetView = _frmDispNokiIkkatsuSettei.spdMaker_Sheet1
            Dim sheetKounyu As Spread.SheetView = _frmDispNokiIkkatsuSettei.spdKonyu_Sheet1

            If aGencho = False Then
                sheetMaker = _frmDispNokiIkkatsuSettei.spdMaker_Sheet1
                sheetKounyu = _frmDispNokiIkkatsuSettei.spdKonyu_Sheet1
            Else
                sheetMaker = _frmDispNokiIkkatsuSettei.spdMakerGencho_Sheet1
                sheetKounyu = _frmDispNokiIkkatsuSettei.spdKonyuGencho_Sheet1
            End If

            '取引先指定納期
            Dim makerNoukiDate As New Spread.CellType.DateTimeCellType

            makerNoukiDate.DropDownButton = True
            makerNoukiDate.DateDefault = Now
            sheetMaker.Columns(NmSpMaker.TAG_NOUKI_SHITEIBI).CellType = makerNoukiDate

            '購入品指定納期
            Dim kounyuDate As New Spread.CellType.DateTimeCellType

            '購入品納期
            kounyuDate.DropDownButton = True
            kounyuDate.DateDefault = Now
            sheetKounyu.Columns(NmSpKonyuTag.TAG_NOUKI).CellType = kounyuDate

            Dim senyouDate As New Spread.CellType.DateTimeCellType

            '専用品納期
            senyouDate.DropDownButton = True
            senyouDate.DateDefault = Now
            sheetKounyu.Columns(NmSpKonyuTag.TAG_SENYOU_NOUKI).CellType = senyouDate


            Dim kyouyouDate As New Spread.CellType.DateTimeCellType

            '共用品納期
            kyouyouDate.DropDownButton = True
            kyouyouDate.DateDefault = Now
            sheetKounyu.Columns(NmSpKonyuTag.TAG_KYOUYOU_NOUKI).CellType = kyouyouDate

        End Sub
#End Region

#Region "コントロール類初期化"

        ''' <summary>
        ''' コントロール類初期化
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub initControl()

            _frmDispNokiIkkatsuSettei.dtpShikyuBuhinDay.Text = String.Empty
            _frmDispNokiIkkatsuSettei.dtpShikyuBuhinDayGencho.Text = String.Empty
            _frmDispNokiIkkatsuSettei.dtpShikyuBuhinDay.CustomFormat = "yyyy/MM/dd"
            _frmDispNokiIkkatsuSettei.dtpShikyuBuhinDayGencho.CustomFormat = "yyyy/MM/dd"

        End Sub

#End Region

#Region "'データテーブル初期化"
        ''' <summary>
        '''データテーブル初期化
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub initDataTable()

            _dtMakerNouki = New DataTable
            _dtKounyuNouki = New DataTable

            '取引先テーブル
            ComFunc.AddDataTableColumn(_dtMakerNouki, NmDtMaker.MAKERCODE, System.Type.GetType("System.String"))
            ComFunc.AddDataTableColumn(_dtMakerNouki, NmDtMaker.NOUKI_SHITEIBI, System.Type.GetType("System.String"))
            ComFunc.AddDataTableColumn(_dtMakerNouki, NmDtMaker.GENCHO_KBN, System.Type.GetType("System.Boolean"))

            '購入先テーブル
            ComFunc.AddDataTableColumn(_dtKounyuNouki, NmDtKonyu.BLOCK, System.Type.GetType("System.String"))
            ComFunc.AddDataTableColumn(_dtKounyuNouki, NmDtKonyu.NOUKI, System.Type.GetType("System.String"))
            ComFunc.AddDataTableColumn(_dtKounyuNouki, NmDtKonyu.SENYOU_NOUKI, System.Type.GetType("System.String"))
            ComFunc.AddDataTableColumn(_dtKounyuNouki, NmDtKonyu.KYOYOU_NOUKI, System.Type.GetType("System.String"))
            ComFunc.AddDataTableColumn(_dtKounyuNouki, NmDtKonyu.GENCHO_KBN, System.Type.GetType("System.Boolean"))

        End Sub
#End Region

#Region "コンボボックス初期化(取引先マスタ)"
        ''' <summary>
        '''コンボボックス初期化(取引先マスタ)
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitMakerComboBox()

            '取引先マスタ取得
            FindAllMakerTable()

            'セルから取得する値はItemDataとします
            _spdMaker.Sheets(0).RowCount = SPD_MAKER_ROW_COUNT
            _spdMakerGencho.Sheets(0).RowCount = SPD_MAKER_ROW_COUNT


            '取引先マスタコンボボックス
            For i As Integer = 0 To SPD_MAKER_ROW_COUNT - 1
                _spdMaker.Sheets(0).Cells(i, 0).CellType = _comboCellMaker
                _spdMakerGencho.Sheets(0).Cells(i, 0).CellType = _comboCellMaker
            Next



        End Sub
#End Region

#Region "取引先マスタ取得"
        ''' <summary>
        '''  取引先マスタ取得
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub FindAllMakerTable()
            Dim dtData As New DataTable

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetSqlAllMakerMaster(), dtData)
            End Using

            If dtData.Rows.Count = 0 Then
                ComFunc.ShowInfoMsgBox("取引先マスタからデータが取得出来ませんでした(RHAC0610)")
                Return
            End If

            'コンボボックス設定用オブジェクト
            Dim comboCell As New FarPoint.Win.Spread.CellType.ComboBoxCellType
            comboCell.Editable = True
            comboCell.MaxLength = 4
            comboCell.Items = New String(dtData.Rows.Count) {}
            comboCell.ItemData = New String(dtData.Rows.Count) {}

            'オートコンプリート設定
            comboCell.AutoSearch = FarPoint.Win.AutoSearch.SingleGreaterThan
            comboCell.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            comboCell.AutoCompleteSource = AutoCompleteSource.ListItems

            '先頭は空白
            comboCell.Items(0) = String.Empty
            comboCell.ItemData(0) = String.Empty

            For i As Integer = 0 To dtData.Rows.Count - 1
                comboCell.Items(i + 1) = dtData.Rows(i)("MAKER_CODE")
                If IsDBNull(dtData.Rows(i)("MAKER_NAME")) = False Then
                    comboCell.ItemData(i + 1) = dtData.Rows(i)("MAKER_NAME")
                Else
                    comboCell.ItemData(i + 1) = String.Empty
                End If

            Next

            _dtMakerMaster = dtData
            _comboCellMaker = comboCell

        End Sub
#End Region

#End Region

#Region "プロパティ"

        ''' <summary>
        '''取引先納期取得 
        ''' NmDtMakerをCOL名称として使用
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetMakerNouki(ByVal aGenchoKbn As Boolean) As DataTable
            Get
                '取引先納期取得
                _frmDispNokiIkkatsuSettei.Refresh()
                GetSpdDataMakerNouki(aGenchoKbn)
                Return _dtMakerNouki
            End Get
        End Property
        ''' <summary>
        ''' 購入品納期取得
        ''' NmSpKonyuTagをCOL名称として使用
        ''' </summary>
        ''' <param name="aGenchoKbn"></param>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetKounyuNouki(ByVal chkBetsuSettei As Boolean, ByVal aGenchoKbn As Boolean) As DataTable
            Get
                '購入品納期取得
                GetSpdDataKounyu(chkBetsuSettei, aGenchoKbn)
                Return _dtKounyuNouki
            End Get
        End Property
        ''' <summary>
        ''' エラー有無を返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetIsError(ByVal aGencho As Boolean) As Boolean
            Get
                If CheckSpreadMakerNouki(aGencho) = False Then
                    Return False
                Else
                    Return True
                End If
            End Get
        End Property
        ''' <summary>
        ''' 支給部品日付取得
        ''' </summary>
        ''' <param name="aGencho"></param>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetShikyuBuhinDate(ByVal aGencho As Boolean) As String
            Get
                Dim dteWork As Date = Nothing
                Dim result As String = String.Empty
                '国内
                If aGencho = False Then
                    If _frmDispNokiIkkatsuSettei.chkShikyuBuhin.Checked = True Then
                        dteWork = _frmDispNokiIkkatsuSettei.dtpShikyuBuhinDay.Value
                        result = dteWork.ToShortDateString
                    End If
                Else
                    '現調品
                    If _frmDispNokiIkkatsuSettei.chkShikyuBuhinGencho.Checked = True Then
                        dteWork = _frmDispNokiIkkatsuSettei.dtpShikyuBuhinDayGencho.Value
                        result = dteWork.ToShortDateString
                    End If
                End If

                Return result

            End Get
        End Property
#Region "国内・現調判定"
        Public ReadOnly Property IsGencho()
            Get
                Dim gencho As Boolean
                'タブコントロールのアクティブ位置により判定
                If _frmDispNokiIkkatsuSettei.TabControl1.SelectedIndex = 1 Then
                    gencho = True
                Else
                    gencho = False
                End If

                Return gencho
            End Get
        End Property
#End Region
#End Region

#Region "メソッド類"

#Region "取引先マスタ参照"
        ''' <summary>
        ''' 取引先マスタ参照
        ''' </summary>
        ''' <param name="aMakerCode"></param>
        ''' <returns>取得成否</returns>
        ''' <remarks></remarks>
        Private Function FindMakerMaster(ByVal aMakerCode As String) As Boolean
            Dim dtData As New DataTable

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.AddParameter("@MAKER_CODE", aMakerCode)
                db.Fill(GetSqlFindMakerMaster(aMakerCode), dtData)
            End Using

            If dtData.Rows.Count = 0 Then
                Return False
            End If

            Return True
        End Function

#End Region

#Region "専用品、共用品別設定ONをスプレッドに反映"
        ''' <summary>
        ''' 専用品、共用品別設定ONをスプレッドに反映
        ''' </summary>
        ''' <param name="aGenchoKbn">現調品ならtrue</param>
        ''' <remarks></remarks>
        Public Sub SetSpSenyouKyouyou_On(Optional ByVal aGenchoKbn As Boolean = False)
            '国内
            If aGenchoKbn = False Then
                _spdKounyu.Sheets(0).Columns(NmSpKonyuTag.TAG_NOUKI).Visible = False
                _spdKounyu.Sheets(0).Columns(NmSpKonyuTag.TAG_SENYOU_NOUKI).Visible = True
                _spdKounyu.Sheets(0).Columns(NmSpKonyuTag.TAG_KYOUYOU_NOUKI).Visible = True
            Else
                '現調
                _spdKounyuGencho.Sheets(0).Columns(NmSpKonyuTag.TAG_NOUKI).Visible = False
                _spdKounyuGencho.Sheets(0).Columns(NmSpKonyuTag.TAG_SENYOU_NOUKI).Visible = True
                _spdKounyuGencho.Sheets(0).Columns(NmSpKonyuTag.TAG_KYOUYOU_NOUKI).Visible = True
            End If
        End Sub
#End Region

#Region " 専用品、共用品別設定OFFをスプレッドに反映"
        ''' <summary>
        ''' 専用品、共用品別設定OFFをスプレッドに反映
        ''' </summary>
        ''' <param name="aGenchoKbn">現調品ならtrue</param>
        ''' <remarks></remarks>
        Public Sub SetSpSenyouKyouyou_Off(Optional ByVal aGenchoKbn As Boolean = False)
            '国内
            If aGenchoKbn = False Then
                _spdKounyu.Sheets(0).Columns(NmSpKonyuTag.TAG_NOUKI).Visible = True
                _spdKounyu.Sheets(0).Columns(NmSpKonyuTag.TAG_SENYOU_NOUKI).Visible = False
                _spdKounyu.Sheets(0).Columns(NmSpKonyuTag.TAG_KYOUYOU_NOUKI).Visible = False
            Else
                '現調
                _spdKounyuGencho.Sheets(0).Columns(NmSpKonyuTag.TAG_NOUKI).Visible = True
                _spdKounyuGencho.Sheets(0).Columns(NmSpKonyuTag.TAG_SENYOU_NOUKI).Visible = False
                _spdKounyuGencho.Sheets(0).Columns(NmSpKonyuTag.TAG_KYOUYOU_NOUKI).Visible = False
            End If
        End Sub

#End Region

#Region "SQL取得(取引マスタ)"

        ''' <summary>
        ''' SQL取得(取引マスタ)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetSqlAllMakerMaster() As String
            Dim sql As New System.Text.StringBuilder()
            '取引先未決データは除外
            With sql
                .AppendLine("SELECT ")
                .AppendLine(" MAKER_CODE ")
                .AppendLine(",MAKER_NAME ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0610")
                .AppendLine(" WHERE ")
                .AppendLine(" MAKER_CODE<>'....' ")
            End With

            Return sql.ToString
        End Function

        ''' <summary>
        ''' SQL取得(取引マスタ-取引先コード条件)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetSqlFindMakerMaster(ByVal aMakerCode As String) As String
            Dim sql As New System.Text.StringBuilder()

            With sql
                .AppendLine("SELECT ")
                .AppendLine(" MAKER_CODE ")
                .AppendLine(",MAKER_NAME ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0610")
                .AppendLine(" WHERE ")
                .AppendLine(" MAKER_CODE=@MAKER_CODE ")

            End With

            Return sql.ToString
        End Function
#End Region

#Region "取引スプレッド取得"

        ''' <summary>
        ''' 取引スプレッド取得
        ''' </summary>
        ''' <param name="aGencho">現調品=true</param>
        ''' <remarks></remarks>
        Private Sub GetSpdDataMakerNouki(Optional ByVal aGencho As Boolean = False)
            '入力チェック
            If CheckSpreadMakerNouki(aGencho) = False Then
                Return
            End If

            Dim sheet As Spread.SheetView
            '国内
            If aGencho = False Then
                sheet = _spdMaker.Sheets(0)
            Else
                '現調品
                sheet = _spdMakerGencho.Sheets(0)
            End If

            'データテーブルをクリアしておく
            _dtMakerNouki.Clear()

            '
            For i As Integer = 0 To sheet.RowCount - 1
                Dim makerCode As String = sheet.GetText(i, GetColIdxFromTag(sheet, NmSpMaker.TAG_MAKER)).Trim
                Dim noukiShiteibi As String = sheet.GetText(i, GetColIdxFromTag(sheet, NmSpMaker.TAG_NOUKI_SHITEIBI)).Trim
                Dim row As DataRow = _dtMakerNouki.NewRow

                '取引コード、納期共にセットされていれば格納
                If Not makerCode.Equals(String.Empty) AndAlso _
                   Not noukiShiteibi.Equals(String.Empty) Then

                    row(NmDtMaker.MAKERCODE) = makerCode
                    row(NmDtMaker.NOUKI_SHITEIBI) = noukiShiteibi
                    row(NmDtMaker.GENCHO_KBN) = aGencho

                    _dtMakerNouki.Rows.Add(row)

                End If

            Next

        End Sub

#End Region

#Region "チェックスプレッド取引納期"
        ''' <summary>
        ''' チェックスプレッド取引納期
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CheckSpreadMakerNouki(ByVal aGencho As Boolean) As Boolean
            Dim sheet As Spread.SheetView

            '国内
            If aGencho = False Then
                sheet = _spdMaker.Sheets(0)
            Else
                '現調品
                sheet = _spdMakerGencho.Sheets(0)
            End If

            'エラー対象セル
            Dim errCell As Spread.Cell = Nothing
            Dim errFlag As Boolean = True

            For i As Integer = 0 To sheet.RowCount - 1
                Dim maker As String = sheet.GetText(i, GetColIdxFromTag(sheet, NmSpMaker.TAG_MAKER)).Trim
                Dim nouki As String = sheet.GetText(i, GetColIdxFromTag(sheet, NmSpMaker.TAG_NOUKI_SHITEIBI)).Trim
                Dim errMsg As String = String.Empty

                'エラー書式一旦消去
                sheet.Cells(i, GetColIdxFromTag(sheet, NmSpMaker.TAG_MAKER)).BackColor = Nothing
                sheet.Cells(i, GetColIdxFromTag(sheet, NmSpMaker.TAG_MAKER)).Note = Nothing
                sheet.Cells(i, GetColIdxFromTag(sheet, NmSpMaker.TAG_NOUKI_SHITEIBI)).BackColor = Nothing
                sheet.Cells(i, GetColIdxFromTag(sheet, NmSpMaker.TAG_NOUKI_SHITEIBI)).Note = Nothing


                If Not maker.Equals(String.Empty) AndAlso Not nouki.Equals(String.Empty) Then
                    '同じ取引コードの存在チェック
                    If CheckMakerCode(maker, aGencho) = False Then
                        errMsg = "同じ取引先のデータが有ります。"
                        errCell = sheet.Cells(i, GetColIdxFromTag(sheet, NmSpMaker.TAG_MAKER))
                    End If

                    ''取引先・納期指定日入力有
                    'If FindMakerMaster(maker) = False Then
                    '    errMsg = "取引先マスタに未登録です。"
                    '    errCell = sheet.Cells(i, GetColIdxFromTag(sheet, NmSpMaker.TAG_MAKER))
                    'End If

                ElseIf maker.Equals(String.Empty) AndAlso Not nouki.Equals(String.Empty) Then
                    '取引コード無し 納期指定有り

                    errCell = sheet.Cells(i, GetColIdxFromTag(sheet, NmSpMaker.TAG_MAKER))
                    errMsg = "取引先を入力してください。"

                ElseIf Not maker.Equals(String.Empty) AndAlso nouki.Equals(String.Empty) Then
                    '取引コード有  納期指定無し
                    errMsg = "納期指定日を入力してください。"
                    errCell = sheet.Cells(i, GetColIdxFromTag(sheet, NmSpMaker.TAG_NOUKI_SHITEIBI))

                Else
                    '取引コード無し　納期指定なしは無視

                End If

                'エラー書式を設定
                If Not errCell Is Nothing Then
                    '背景色赤
                    errCell.BackColor = Color.Red
                    '初回エラーセルアクティブセルセット
                    If errFlag = True Then
                        sheet.SetActiveCell(i, GetColIdxFromTag(sheet, errCell.Tag))
                        errFlag = False
                    End If
                End If

                errCell = Nothing
            Next

            Return errFlag

        End Function
#End Region

#Region "同じ取引コードチェック"
        ''' <summary>
        ''' 同じ取引コードチェック
        ''' </summary>
        ''' <param name="aMakerCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CheckMakerCode(ByVal aMakerCode As String, Optional ByVal aGencho As Boolean = False) As Boolean
            Dim sheetM As Spread.SheetView = Nothing

            '国内
            If aGencho = False Then
                sheetM = _spdMaker.Sheets(0)
            Else
                sheetM = _spdMakerGencho.Sheets(0)
            End If

            Dim findCount As Integer = 0

            For i As Integer = 0 To sheetM.RowCount - 1
                Dim makerCodeDt As String = sheetM.GetText(i, GetColIdxFromTag(sheetM, NmSpMaker.TAG_MAKER))

                '存在有無
                If aMakerCode.Equals(makerCodeDt) = True Then
                    findCount += 1
                End If

            Next

            '2件以上で重複有り
            If findCount >= 2 Then
                Return False
            End If

            Return True
        End Function
#End Region

#Region "チェックスプレッド購入品納期"
        ''' <summary>
        ''' チェックスプレッド購入品納期
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CheckSpreadKounyuNouki() As Boolean
            '
        End Function
#End Region

#Region "購入品スプレッド取得"
        ''' <summary>
        ''' 購入品スプレッド取得
        ''' </summary>
        ''' <param name="aGencho">現調品=true</param>
        ''' <remarks></remarks>
        Private Sub GetSpdDataKounyu(ByVal aSenyouSel As Boolean, ByVal aGencho As Boolean)
            Dim sheet As Spread.SheetView

            '国内
            If aGencho = False Then
                sheet = _spdKounyu.Sheets(0)
            Else
                '現調品
                sheet = _spdKounyuGencho.Sheets(0)
            End If

            'データテーブルをクリアしておく
            _dtKounyuNouki.Clear()
            '
            For i As Integer = 0 To sheet.RowCount - 1
                Dim block As String = sheet.GetText(i, GetColIdxFromTag(sheet, NmSpKonyuTag.TAG_BLOCK)).Trim
                Dim nouki As String = sheet.GetText(i, GetColIdxFromTag(sheet, NmSpKonyuTag.TAG_NOUKI)).Trim
                Dim senyouNouki As String = sheet.GetText(i, GetColIdxFromTag(sheet, NmSpKonyuTag.TAG_SENYOU_NOUKI)).Trim
                Dim kyouyouNouki As String = sheet.GetText(i, GetColIdxFromTag(sheet, NmSpKonyuTag.TAG_KYOUYOU_NOUKI)).Trim
                Dim row As DataRow = _dtKounyuNouki.NewRow

                '共通
                If aSenyouSel = False Then
                    If Not nouki.Equals(String.Empty) Then
                        row(NmDtKonyu.BLOCK) = block
                        row(NmDtKonyu.NOUKI) = nouki
                        row(NmDtKonyu.SENYOU_NOUKI) = String.Empty
                        row(NmDtKonyu.KYOYOU_NOUKI) = String.Empty
                        row(NmDtKonyu.GENCHO_KBN) = aGencho
                        _dtKounyuNouki.Rows.Add(row)
                    End If
                Else
                    '専用・共用品別
                    If Not senyouNouki.Equals(String.Empty) OrElse _
                       Not kyouyouNouki.Equals(String.Empty) Then
                        row(NmDtKonyu.BLOCK) = block
                        row(NmDtKonyu.NOUKI) = String.Empty
                        row(NmDtKonyu.SENYOU_NOUKI) = senyouNouki
                        row(NmDtKonyu.KYOYOU_NOUKI) = kyouyouNouki
                        row(NmDtKonyu.GENCHO_KBN) = aGencho
                        _dtKounyuNouki.Rows.Add(row)
                    End If
                End If

            Next

        End Sub

#End Region

#Region " 列インデックス取得 "
        ''' <summary>
        ''' 列タグを元に列インデックスを取得します.
        ''' </summary>
        ''' <param name="sheet">対象シート</param>
        ''' <param name="tag">列タグ</param>
        ''' <returns>列インデックス</returns>
        ''' <remarks></remarks>
        Public Function GetColIdxFromTag(ByVal sheet As Spread.SheetView, ByVal tag As String) As Integer

            Dim col As Spread.Column = sheet.Columns(tag)

            If col Is Nothing Then
                Return -1
            End If

            Return col.Index
        End Function

#End Region

#Region "行挿入"
        ''' <summary>
        ''' 行挿入
        ''' </summary>
        ''' <param name="rowNo">挿入行位置</param>
        ''' <remarks></remarks>
        Public Sub InsertRow(ByVal rowNo As Integer, ByVal aGencho As Boolean)

            '国内
            If aGencho = False Then
                _spdMaker.Sheets(0).Rows.Add(rowNo, 1)
                _spdMaker.Sheets(0).Cells(rowNo, 0).CellType = _comboCellMaker
            Else
                '現調品
                _spdMakerGencho.Sheets(0).Rows.Add(rowNo, 1)
                _spdMakerGencho.Sheets(0).Cells(rowNo, 0).CellType = _comboCellMaker
            End If

        End Sub
#End Region

#Region "行削除"
        ''' <summary>
        ''' 行削除
        ''' </summary>
        ''' <param name="aRowNo"></param>
        ''' <param name="aGencho"></param>
        ''' <remarks></remarks>
        Public Sub DeleteRow(ByVal aRowNo As Integer, Optional ByVal aGencho As Boolean = False)

            '行削除
            If aGencho = False Then
                _spdMaker.Sheets(0).Rows.Remove(aRowNo, 1)
            Else
                _spdMakerGencho.Sheets(0).Rows.Remove(aRowNo, 1)
            End If

        End Sub

#End Region

#End Region
    End Class

End Namespace
