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

Namespace TehaichoExcel
    Public Class DispShisakuBuhinList
#Region " メンバー変数 "
        ''' <summary>ビュー</summary>
        Private m_view As Frm46DispShisakuBuhinList
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
        Public Sub New(ByVal f As Frm46DispShisakuBuhinList)
            m_view = f
            m_spCom = New SpreadCommon(m_view.spdParts)
        End Sub
#End Region

#Region " ビュー初期化 "
        ''' <summary>
        ''' ビューの初期化        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function InitView() As RESULT
            '初期起動時には選択ボタンを使用不可とする。
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

#Region "一覧をダブルクリックしたら　または　呼出しボタンを押したら「手配帳リスト出力画面」へ遷移する"
        Public Sub ToTehaichoList()
            m_view.Hide()
            Dim eventCode As String = m_view.txtIbentoNo.Text
            Dim frm46DispTehaichoExcel As New Frm46TehaichoExcel(eventCode)
            frm46DispTehaichoExcel.ShowDialog()
            m_view.Show()
        End Sub
#End Region

#Region " SPREADの列のデータフィールドを設定する "
        ''' <summary>
        ''' SPREADの列のデータフィールドを設定する        ''' </summary>
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
        ''' SPREADの列のタグ値を設定する        ''' </summary>
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
        ''' 選択ボタンを使用不可とする
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetSelectLock()
            m_view.btnSelect.ForeColor = Color.Black
            m_view.btnSelect.BackColor = Color.White
            m_view.btnSelect.Enabled = False
        End Sub
        ''' <summary>
        ''' 呼出しボタンを使用可とする
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetSelectUnlock()
            m_view.btnSelect.ForeColor = Color.Black
            m_view.btnSelect.BackColor = Color.LightCyan
            m_view.btnSelect.Enabled = True
        End Sub
#End Region

#Region "イベントコード設定して　呼出しボタンと削除ボタンの使用可かどうかを設定する"
        Public Sub SetGamenByEventCode()
            If m_view.txtIbentoNo.Text.Equals(String.Empty) Then
                SetSelectLock()
            Else
                SetSelectUnlock()
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

#Region "試作部品表のイベント情報を取得"
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
                .AppendLine("ORDER BY SHISAKU_EVENT_CODE,SHISAKU_KAIHATSU_FUGO ")
            End With
            Return sql.ToString()
        End Function
#End Region
    End Class
End Namespace
