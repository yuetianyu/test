Imports ShisakuCommon
Imports EBom.Common
Imports EBom.Common.mdlConstraint
Imports EBom.Data
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd
Imports ExcelOutput.ShisakuBuhinExcel.Excel

Namespace ShisakuBuhinExcel
    Public Class DispShisakuBuhinExcel
#Region " メンバー変数 "
        ''' <summary>ビュー</summary>
        Private m_view As Frm44ShisakuBuhinExcel
        ''' <summary>FpSpread 共通</summary>
        Private m_spCom As SpreadCommon
        ''' <summary>Excel出力タイトル</summary>
        Private lstExcelTitle As New List(Of String)
        ''' <summary>Excel出力項目</summary>
        Private lstExcelItem As New List(Of String)

        Private Const TAG_SHISAKU_EVENT_CODE As String = "SHISAKU_EVENT_CODE"
        Private Const TAG_SHISAKU_KAIHATSU_FUGO As String = "SHISAKU_KAIHATSU_FUGO"
        Private Const TAG_SHISAKU_EVENT_PHASE_NAME As String = "SHISAKU_EVENT_PHASE_NAME"
        Private Const TAG_UNIT_KBN As String = "UNIT_KBN"
        Private Const TAG_SHISAKU_EVENT_NAME As String = "SHISAKU_EVENT_NAME"
        Private Const TAG_DAISUU As String = "DAISUU"
        Private Const TAG_HACHU As String = "HACHU"
        Private Const TAG_SEKKEI_TENKAIBI As String = "SEKKEI_TENKAIBI"
        Private Const TAG_KAITEI_SHOCHI_SHIMEKIRIBI As String = "KAITEI_SYOCHI_SHIMEKIRIBI"
        Private Const TAG_STATUS_NAME As String = "STATUS_NAME"
        Private Const TAG_STATUS As String = "STATUS"

#End Region

#Region " コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="f"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal f As Frm44ShisakuBuhinExcel)
            m_view = f
            m_spCom = New SpreadCommon(m_view.spdParts)
        End Sub
#End Region

#Region " ビュー初期化 "
        ''' <summary>
        ''' ビューの初期化
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function InitView() As RESULT
            '初期起動時にはEXCEL出力ボタンを使用不可とする。
            ShisakuFormUtil.setTitleVersion(m_view)
            ShisakuFormUtil.SetIdAndBuka(m_view.LblLoginUserId, m_view.LblLoginBukaName)
            'Spreadの初期化
            SpreadUtil.Initialize(m_view.spdParts)
            SetSpdColTag()
            SetSpdDataField()
            'spreadにデータを設定する
            SetSpreadSource()

        End Function
#End Region

#Region "EXCEL出力ボタンを押したら　処理する。"
        ''' <summary>
        ''' EXCELボタンを押す。
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ExcelBtnClick(ByVal shisakuEventCode As String)

            '以下のEXCELは「試作部品作成一覧」のものです。
            '後で実装しましょう。

            Dim excel As New ExportShisakuBuhinExcel(shisakuEventCode)

            'Dim spdExcel As New FarPoint.Win.Spread.FpSpread()
            'spdExcel.Sheets.Count = 1
            ''EXCEl列タイトルと項目を取得する
            'SetTitleAndItemList()
            'Dim sheetExcel As FarPoint.Win.Spread.SheetView = spdExcel.Sheets(0)
            'sheetExcel.ColumnCount = lstExcelTitle.Count
            'sheetExcel.AutoGenerateColumns = False
            ''列タグの設定
            'SetSheetTag(sheetExcel)
            ''列タイトルの設定
            'SetSheetTitle(sheetExcel)
            ''データフィールドの設定
            'SetSheetDataField(sheetExcel)
            ''データの取得
            'Dim dtExcelData As DataTable = GetExcelData()
            'sheetExcel.DataSource = dtExcelData
            'SetSpdExcelColPro(spdExcel)
            'ExcelCommon.SaveExcelFile("試作部品表作成一覧", spdExcel, "ShisakuBuhinSakuseiList")
        End Sub
        ''' <summary>
        '''　列タグを設定する
        ''' </summary>
        ''' <param name="sheetExcel"></param>
        ''' <remarks></remarks>
        Private Sub SetSheetTag(ByVal sheetExcel As FarPoint.Win.Spread.SheetView)
            For i As Integer = 0 To sheetExcel.ColumnCount - 1
                sheetExcel.Columns(i).Tag = lstExcelItem(i)
            Next
        End Sub
        ''' <summary>
        ''' 列タイトルを設定する
        ''' </summary>
        ''' <param name="sheetExcel"></param>
        ''' <remarks></remarks>
        Private Sub SetSheetTitle(ByVal sheetExcel As FarPoint.Win.Spread.SheetView)
            For i As Integer = 0 To sheetExcel.ColumnCount - 1
                sheetExcel.ColumnHeader.Columns(i).Label = lstExcelTitle(i)
            Next
        End Sub
        ''' <summary>
        ''' データフィールドを設定する
        ''' </summary>
        ''' <param name="sheetExcel"></param>
        ''' <remarks></remarks>
        Private Sub SetSheetDataField(ByVal sheetExcel As FarPoint.Win.Spread.SheetView)
            For i As Integer = 0 To sheetExcel.ColumnCount - 1
                sheetExcel.Columns(i).DataField = lstExcelItem(i)
            Next
        End Sub
        ''' <summary>
        ''' Excel出力用データを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetExcelData() As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetBuhinSakuseiListExcel(), dtData)
            End Using
            Dim col As New DataColumn
            col.ColumnName = "KOUBAN"
            dtData.Columns.Add(col)
            Dim kouban As Integer = 1
            For i As Integer = 0 To dtData.Rows.Count - 1
                If Not i = 0 AndAlso Not dtData.Rows(i)("SHISAKU_EVENT_CODE").Equals(dtData.Rows(i - 1)("SHISAKU_EVENT_CODE")) Then
                    kouban += 1
                End If
                dtData.Rows(i)("KOUBAN") = kouban
            Next
            Return dtData
        End Function
        ''' <summary>
        ''' EXCEl列タイトルと項目を取得する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetTitleAndItemList()
            '1
            lstExcelTitle.Add("項番")
            lstExcelItem.Add("KOUBAN")
            '2
            lstExcelTitle.Add("イベントコード")
            lstExcelItem.Add("SHISAKU_EVENT_CODE")
            '3
            lstExcelTitle.Add("開発符号")
            lstExcelItem.Add("SHISAKU_KAIHATSU_FUGO")
            '4
            lstExcelTitle.Add("イベント")
            lstExcelItem.Add("SHISAKU_EVENT_PHASE_NAME")
            '5
            lstExcelTitle.Add("ユニット区分")
            lstExcelItem.Add("UNIT_KBN")
            '6
            lstExcelTitle.Add("イベント名称")
            lstExcelItem.Add("SHISAKU_EVENT_NAME")
            '7
            lstExcelTitle.Add("台数")
            lstExcelItem.Add("DAISUU")
            '8
            lstExcelTitle.Add("発注")
            lstExcelItem.Add("HACHU")
            '9
            lstExcelTitle.Add("設計展開日")
            lstExcelItem.Add("SEKKEI_TENKAIBI")
            '10
            lstExcelTitle.Add("訂正処置〆切日")
            lstExcelItem.Add("SHIMEKIRIBI")
            '11
            lstExcelTitle.Add("状態")
            lstExcelItem.Add("STATUS_NAME")
            '12
            lstExcelTitle.Add("リストコード")
            lstExcelItem.Add("SHISAKU_LIST_CODE")
            '13
            lstExcelTitle.Add("グループ")
            lstExcelItem.Add("SHISAKU_GROUP_NO")
            '14
            lstExcelTitle.Add("工事指令No.")
            lstExcelItem.Add("SHISAKU_KOUJI_SHIREI_NO")
            '15
            lstExcelTitle.Add("イベント名称")
            lstExcelItem.Add("SHISAKU_EVENT_NAME2")
            '16
            lstExcelTitle.Add("台数")
            lstExcelItem.Add("SHISAKU_DAISU")
            '17
            lstExcelTitle.Add("工事区分")
            lstExcelItem.Add("SHISAKU_KOUJI_KBN")
            '18
            lstExcelTitle.Add("製品区分")
            lstExcelItem.Add("SHISAKU_SEIHIN_KBN")
            '19
            lstExcelTitle.Add("工事No.")
            lstExcelItem.Add("SHISAKU_KOUJI_NO")
            '20
            lstExcelTitle.Add("改訂")
            lstExcelItem.Add("SHISAKU_LIST_CODE_KAITEI_NO")
            '21
            lstExcelTitle.Add("メモ")
            lstExcelItem.Add("SHISAKU_MEMO")
        End Sub
        ''' <summary>
        ''' 列のセルの水平方向の配置を設定する。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetSpdExcelColPro(ByVal spdExcel As FarPoint.Win.Spread.FpSpread)
            Dim horiLeft() As String = New String() {lstExcelItem(1), lstExcelItem(2), lstExcelItem(3), _
                                                     lstExcelItem(4), lstExcelItem(5), lstExcelItem(6), _
                                                     lstExcelItem(7), lstExcelItem(10), lstExcelItem(11), _
                                                     lstExcelItem(12), lstExcelItem(13), lstExcelItem(14), _
                                                     lstExcelItem(15), lstExcelItem(17), lstExcelItem(18), lstExcelItem(20)}
            Dim horiRight() As String = New String() {lstExcelItem(0), lstExcelItem(8), lstExcelItem(9), lstExcelItem(16), lstExcelItem(19)}
            Dim spExcelCom = New SpreadCommon(spdExcel)
            For i As Integer = 0 To horiLeft.Length - 1
                spExcelCom.GetColFromTag(horiLeft(i)).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Next
            For i As Integer = 0 To horiRight.Length - 1
                spExcelCom.GetColFromTag(horiRight(i)).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            Next
        End Sub
#End Region

#Region "一覧をダブルクリック　または　EXCEL出力ボタンを押したら部品表のEXCELを出力する。"
        Public Sub ToSakuseiMenuUpdateMode()

            'Excel出力ボタンクリック時の処理を実装する。

            '以下のEXCELは「試作部品作成一覧」のものです。
            '後で実装しましょう。

            Dim spdExcel As New FarPoint.Win.Spread.FpSpread()
            spdExcel.Sheets.Count = 1
            'EXCEl列タイトルと項目を取得する
            SetTitleAndItemList()
            Dim sheetExcel As FarPoint.Win.Spread.SheetView = spdExcel.Sheets(0)
            sheetExcel.ColumnCount = lstExcelTitle.Count
            sheetExcel.AutoGenerateColumns = False
            '列タグの設定
            SetSheetTag(sheetExcel)
            '列タイトルの設定
            SetSheetTitle(sheetExcel)
            'データフィールドの設定
            SetSheetDataField(sheetExcel)
            'データの取得
            Dim dtExcelData As DataTable = GetExcelData()
            sheetExcel.DataSource = dtExcelData
            SetSpdExcelColPro(spdExcel)
            ExcelCommon.SaveExcelFile("試作部品表作成一覧 " + Now.ToString("MMdd") + Now.ToString("HHmm"), spdExcel, "ShisakuBuhinSakuseiList")
        End Sub
#End Region

#Region " SPREADの列のデータフィールドを設定する "
        ''' <summary>
        ''' SPREADの列のデータフィールドを設定する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpdDataField()

            '列の項目を設定する
            For i As Integer = 0 To m_view.spdParts_Sheet1.ColumnCount - 1
                m_view.spdParts_Sheet1.Columns(i).DataField = m_view.spdParts_Sheet1.Columns(i).Tag
            Next
        End Sub
#End Region

#Region " SPREADの列のタグ値を設定する "
        ''' <summary>
        ''' SPREADの列のタグ値を設定する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpdColTag()
            '列の項目を設定する
            m_view.spdParts_Sheet1.Columns(0).Tag = TAG_SHISAKU_EVENT_CODE
            m_view.spdParts_Sheet1.Columns(1).Tag = TAG_SHISAKU_KAIHATSU_FUGO
            m_view.spdParts_Sheet1.Columns(2).Tag = TAG_SHISAKU_EVENT_PHASE_NAME
            m_view.spdParts_Sheet1.Columns(3).Tag = TAG_UNIT_KBN
            m_view.spdParts_Sheet1.Columns(4).Tag = TAG_SHISAKU_EVENT_NAME
            m_view.spdParts_Sheet1.Columns(5).Tag = TAG_DAISUU
            m_view.spdParts_Sheet1.Columns(6).Tag = TAG_HACHU
            m_view.spdParts_Sheet1.Columns(7).Tag = TAG_SEKKEI_TENKAIBI
            m_view.spdParts_Sheet1.Columns(8).Tag = TAG_KAITEI_SHOCHI_SHIMEKIRIBI
            m_view.spdParts_Sheet1.Columns(9).Tag = TAG_STATUS_NAME
            m_view.spdParts_Sheet1.Columns(10).Tag = TAG_STATUS
        End Sub
#End Region
        
#Region "SPREADで 列のセルの水平方向の配置を設定する。行と列のサイズを変更できないことを設定する。"
        Public Sub SetSpdColPro()
            m_spCom.GetColFromTag(TAG_SHISAKU_EVENT_CODE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_SHISAKU_KAIHATSU_FUGO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_SHISAKU_EVENT_PHASE_NAME).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_UNIT_KBN).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_SHISAKU_EVENT_NAME).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_DAISUU).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_HACHU).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_SEKKEI_TENKAIBI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            m_spCom.GetColFromTag(TAG_KAITEI_SHOCHI_SHIMEKIRIBI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            m_spCom.GetColFromTag(TAG_STATUS_NAME).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            For i As Integer = 0 To m_view.spdParts_Sheet1.ColumnCount - 1
                m_view.spdParts_Sheet1.Columns(i).Resizable = False
            Next
            For i As Integer = 0 To m_view.spdParts_Sheet1.RowCount - 1
                m_view.spdParts_Sheet1.Rows(i).Resizable = False
            Next
        End Sub
#End Region

#Region " spreadでデータを取得する "
        Public Function GetIchiranList() As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetBuhinSakuseiList(), dtData)
            End Using
            Return dtData
        End Function
#End Region

#Region "ボタンを使用可または使用不可とする。"
        ''' <summary>
        ''' EXCEL出力ボタンを使用不可とする
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetExcelLock()
            m_view.btnExcelExport.ForeColor = Color.Black
            m_view.btnExcelExport.BackColor = Color.White
            m_view.btnExcelExport.Enabled = False
        End Sub
        ''' <summary>
        ''' EXCEL出力ボタンを使用可とする
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetExcelUnlock()
            m_view.btnExcelExport.ForeColor = Color.Black
            m_view.btnExcelExport.BackColor = Nothing
            m_view.btnExcelExport.Enabled = True
        End Sub
#End Region

#Region "イベントコード設定して　呼出しボタンと削除ボタンの使用可かどうかを設定する"
        Public Sub SetGamenByEventCode()
            If m_view.txtIbentoNo.Text.Equals(String.Empty) Then
                SetExcelLock()
            Else
                SetExcelUnlock()
            End If
        End Sub
#End Region

#Region "spreadのbind"
        Private Sub SetSpreadSource()
            'SPREADのデータソースを設定する
            m_view.spdParts_Sheet1.DataSource = GetIchiranList()
            'filterAndSort再初期化
            'filterAndSort = New ShisakuBuhinSakuseiListFilterAndSortVo()
            'SPREADの列のセルの水平方向の配置を再設定する。
            SetSpdColPro()
            m_view.txtIbentoNo.Text = ""
            SetGamenByEventCode()
        End Sub
#End Region

#Region "試作部品作成一覧"
        ''' <summary>
        ''' 画面でデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetBuhinSakuseiList() As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("    SHISAKU_EVENT_CODE, ")
                .AppendLine("    SHISAKU_KAIHATSU_FUGO, ")
                .AppendLine("    SHISAKU_EVENT_PHASE_NAME, ")
                .AppendLine("    UNIT_KBN, ")
                .AppendLine("    SHISAKU_EVENT_NAME, ")
                .AppendLine("    LTRIM(STR(ISNULL(SEISAKUDAISU_KANSEISYA,0)))+'+'+LTRIM(STR(ISNULL(SEISAKUDAISU_WB,0)))  AS DAISUU, ")
                '発注
                .AppendLine("    HACHU_UMU, ")
                '発注の有無が'1'の時、「有」と表示する
                .AppendLine("    (CASE WHEN HACHU_UMU = 1 THEN '有'  ELSE '' END) AS HACHU, ")
                'YYYY/MM/DD 
                .AppendFormat("    ({0})  AS SEKKEI_TENKAIBI, ", DataSqlCommon.IntToDateFormatSql("SEKKEI_TENKAIBI"))
                'YYYY/MM/DD 
                .AppendFormat("    ({0})  AS KAITEI_SYOCHI_SHIMEKIRIBI, ", DataSqlCommon.IntToDateFormatSql("KAITEI_SYOCHI_SHIMEKIRIBI"))
                '状態
                .AppendLine("    STATUS, ")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS.SHISAKU_STATUS_NAME AS STATUS_NAME ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT ")
                .AppendLine("   LEFT JOIN " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS ")
                .AppendLine("    ON " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS.SHISAKU_STATUS_CODE=" & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.STATUS ")
                .AppendLine("WHERE STATUS <> '' AND STATUS <> '22' ")
                .AppendLine("ORDER BY SHISAKU_EVENT_CODE,SHISAKU_KAIHATSU_FUGO ")
            End With
            Return sql.ToString()
        End Function
        ''' <summary>
        ''' excel出力のデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetBuhinSakuseiListExcel() As String
            Dim sqlWhere As New System.Text.StringBuilder()
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.SHISAKU_EVENT_CODE, ")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.SHISAKU_KAIHATSU_FUGO, ")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.SHISAKU_EVENT_PHASE_NAME, ")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.UNIT_KBN, ")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.SHISAKU_EVENT_NAME, ")
                .AppendLine("    LTRIM(STR(ISNULL(SEISAKUDAISU_KANSEISYA,0)))+'+'+LTRIM(STR(ISNULL(SEISAKUDAISU_WB,0)))  AS DAISUU, ")
                '発注
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.HACHU_UMU, ")
                '発注の有無が'1'の時、「有」と表示する
                .AppendLine("    (CASE WHEN HACHU_UMU = 1 THEN '有'  ELSE '' END) AS HACHU, ")
                'YYYY/MM/DD 
                .AppendFormat("    ({0})  AS SEKKEI_TENKAIBI, ", DataSqlCommon.IntToDateFormatSql("" & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.SEKKEI_TENKAIBI"))
                'YYYY/MM/DD 
                .AppendFormat("    ({0})  AS kAITEI_SYOCHI_SHIMEKIRIBI, ", DataSqlCommon.IntToDateFormatSql("" & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.KAITEI_SYOCHI_SHIMEKIRIBI"))
                '状態
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.STATUS, ")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS.SHISAKU_STATUS_NAME AS STATUS_NAME, ")
                'T_SHISAKU_LISTCODE
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_LIST_CODE,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_GROUP_NO,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_KOUJI_SHIREI_NO,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_EVENT_NAME AS SHISAKU_EVENT_NAME2,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_DAISU,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_KOUJI_KBN,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_SEIHIN_KBN,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_KOUJI_NO,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_LIST_CODE_KAITEI_NO,")
                .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_MEMO ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT ")
                .AppendLine("   LEFT JOIN " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS ")
                .AppendLine("    ON " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS.SHISAKU_STATUS_CODE=" & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.STATUS ")
                .AppendLine("   LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE ")
                .AppendLine("    ON " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE.SHISAKU_EVENT_CODE=" & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT.SHISAKU_EVENT_CODE ")
                'WHERE
                .AppendLine(sqlWhere.ToString)
            End With
            Return sql.ToString()
        End Function
        Public Shared Sub AppendWhere(ByRef sqlWhere As System.Text.StringBuilder, ByVal item As String, ByVal value As String)

            If Not value = String.Empty AndAlso Not value.Equals(SpreadUtil.FILTER_NON_BLANKS_STRING) Then
                If sqlWhere.Length > 0 Then
                    sqlWhere.AppendFormat(" AND ")
                Else
                    sqlWhere.AppendFormat(" WHERE")
                End If
                If value.Equals(SpreadUtil.FILTER_ALL_STRING) Then
                    sqlWhere.AppendFormat(" ({0} IS NOT NULL AND {0}<>'' )", item)
                ElseIf value.Equals(SpreadUtil.FILTER_BLANKS_STRING) Then
                    sqlWhere.AppendFormat(" ({0} IS  NULL OR {0}='' )", item)
                Else
                    sqlWhere.AppendFormat(" {0}='{1}'", item, value)
                End If
            End If
        End Sub
#End Region
    End Class
End Namespace
