Imports EBom.Common
Imports EBom.Common.mdlConstraint
Imports EBom.Data
Imports ShisakuCommon
Imports ShisakuCommon.Ui.Spd

Namespace YosanBuhinEdit.KouseiBuhin
    Public Class DispBuhinList
#Region " メンバー変数 "
        ''' <summary>ビュー</summary>
        Private m_view As Frm41KouseiBuhinSelector
        ''' <summary>FpSpread 共通</summary>
        Private m_spCom As SpreadCommon
        ''' <summary>列が自動ソートされた、列の自動フィルタリングが実行された条件</summary>
        Private filterAndSort As BuhinListFilterAndSortVo

        Private Const TAG_BLOCK_NO As String = "BLOCK_NO"
        Private Const TAG_LEVEL As String = "LEVEL"
        Private Const TAG_SHUKEI_CODE As String = "SHUKEI_CODE"
        Private Const TAG_SIA_SHUKEI_CODE As String = "SIA_SHUKEI_CODE"
        Private Const TAG_TENKAI As String = "TENKAI"
        Private Const TAG_SELECT As String = "SELECT"
        Private Const TAG_BUHIN_NO As String = "BUHIN_NO"
        Private Const TAG_INSU As String = "INSU"
        Private Const TAG_BUHIN_NAME As String = "BUHIN_NAME"
        Private Const TAG_BUHIN_NOTE As String = "BUHIN_NOTE"
        Private Const TAG_KOUSEI As String = "KOUSEI"
        Private Const TAG_KYOKYU As String = "KYOKYU"
        Private Const TAG_3D As String = "3D"

#End Region

#Region " コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="f"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal f As Frm41KouseiBuhinSelector)
            m_view = f
            m_spCom = New SpreadCommon(m_view.spdBuhin)
            filterAndSort = New BuhinListFilterAndSortVo()
        End Sub
#End Region

#Region " ビュー初期化 "
        ''' <summary>
        ''' ビューの初期化        
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function InitView() As RESULT

            ''Spreadの初期化
            SpreadUtil.Initialize(m_view.spdBuhin)
            SetSpdColTag()
            SetSpdDataField()
            '
            SetSpdColPro()

        End Function
#End Region

#Region "列が自動ソートされたら　処理する "
        Public Sub AutoSortedColumn(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.AutoSortedColumnEventArgs)
            filterAndSort.SortItem = m_view.spdBuhin_Sheet1.Columns(e.Column).Tag
            filterAndSort.SortAscending = e.Ascending
        End Sub
#End Region

#Region "列の自動フィルタリングが実行されたら　処理する "
        Public Sub AutoFilteredColumn(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.AutoFilteredColumnEventArgs)
            Select Case m_view.spdBuhin_Sheet1.Columns(e.Column).Tag
                Case TAG_BLOCK_NO
                    filterAndSort.BlockNo = FilterStringFormat(e.FilterString)
                Case TAG_LEVEL
                    filterAndSort.Level = FilterStringFormat(e.FilterString)
                Case TAG_SHUKEI_CODE
                    filterAndSort.ShukeiCode = FilterStringFormat(e.FilterString)
                Case TAG_SIA_SHUKEI_CODE
                    filterAndSort.SiaShukeiCode = FilterStringFormat(e.FilterString)
                Case TAG_BUHIN_NO
                    filterAndSort.BuhinNo = FilterStringFormat(e.FilterString)
                Case TAG_INSU
                    filterAndSort.Insu = FilterStringFormat(e.FilterString)
                Case TAG_BUHIN_NAME
                    filterAndSort.BuhinName = FilterStringFormat(e.FilterString)
                Case TAG_BUHIN_NOTE
                    filterAndSort.BuhinNote = FilterStringFormat(e.FilterString)
            End Select
        End Sub
        Public Function FilterStringFormat(ByVal filterString As String) As String
            If filterString.Equals(m_view.spdBuhin_Sheet1.RowFilter.NonBlanksString) Then
                Return m_view.spdBuhin_Sheet1.RowFilter.NonBlanksString
            End If
            If filterString.Equals(m_view.spdBuhin_Sheet1.RowFilter.AllString) Then
                Return m_view.spdBuhin_Sheet1.RowFilter.AllString
            End If
            Return filterString
        End Function
#End Region

#Region " SPREADの列のデータフィールドを設定する "
        ''' <summary>
        ''' SPREADの列のデータフィールドを設定する       
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpdDataField()

            '列の項目を設定する           
            For i As Integer = 0 To m_view.spdBuhin_Sheet1.ColumnCount - 1
                m_view.spdBuhin_Sheet1.Columns(i).DataField = m_view.spdBuhin_Sheet1.Columns(i).Tag
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
            '
            '   ここの、宣言はセル位置を示す
            '
            Dim _KouseiBuhinSelectorLogic As New Logic.KouseiBuhinSelectorSubject("", m_view)


            '
            '   列タグを設定するに当たり、即値をやめた
            '
            With _KouseiBuhinSelectorLogic

                m_view.spdBuhin_Sheet1.Columns(.spd_Buhin_Col_BlockNo).Tag = TAG_BLOCK_NO
                m_view.spdBuhin_Sheet1.Columns(.spd_Buhin_Col_Level).Tag = TAG_LEVEL
                m_view.spdBuhin_Sheet1.Columns(.spd_Buhin_Col_ShukeiCode).Tag = TAG_SHUKEI_CODE
                m_view.spdBuhin_Sheet1.Columns(.spd_Buhin_Col_SiaShukeiCode).Tag = TAG_SIA_SHUKEI_CODE
                m_view.spdBuhin_Sheet1.Columns(.spd_Buhin_Col_Tenkai).Tag = TAG_TENKAI
                m_view.spdBuhin_Sheet1.Columns(.spd_Buhin_Col_Select).Tag = TAG_SELECT
                m_view.spdBuhin_Sheet1.Columns(.spd_Buhin_Col_BuhinNo).Tag = TAG_BUHIN_NO
                m_view.spdBuhin_Sheet1.Columns(.spd_Buhin_Col_Insu).Tag = TAG_INSU
                m_view.spdBuhin_Sheet1.Columns(.spd_Buhin_Col_BuhinName).Tag = TAG_BUHIN_NAME
                m_view.spdBuhin_Sheet1.Columns(.spd_Buhin_Col_Note).Tag = TAG_BUHIN_NOTE
                m_view.spdBuhin_Sheet1.Columns(.spd_Buhin_Col_SelectionMethod).Tag = TAG_KOUSEI
                m_view.spdBuhin_Sheet1.Columns(.spd_Buhin_Col_KyoKyu).Tag = TAG_KYOKYU

                m_view.spdBuhin_Sheet1.Columns(.spd_Buhin_Col_3DUMU).Tag = TAG_3D
            End With

        End Sub
#End Region

#Region "SPREADで 列のセルの水平方向の配置を設定する。"
        Public Sub SetSpdColPro()
            m_spCom.GetColFromTag(TAG_BLOCK_NO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_LEVEL).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_SHUKEI_CODE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_SIA_SHUKEI_CODE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_BUHIN_NO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_INSU).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_SHUKEI_CODE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_BUHIN_NAME).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_BUHIN_NOTE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_3D).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center

            For i As Integer = 0 To m_view.spdBuhin_Sheet1.RowCount - 1
                m_view.spdBuhin_Sheet1.Rows(i).Resizable = False
            Next
        End Sub
#End Region

#Region " spreadでデータを取得する "
        Public Function GetSpdDaiKubunList(ByVal kaihatsuFugo As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetDaiKubunList(kaihatsuFugo), dtData)
            End Using
            Return dtData
        End Function
        Public Function GetspdBuhinList(ByVal kaihatsuFugo As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetKubunList(kaihatsuFugo), dtData)
            End Using
            Return dtData
        End Function
#End Region

#Region "spreadのbind"
        Private Sub SetSpreadSource(ByVal kaihatsuFugo As String, ByVal selectKubun As String)

            If StringUtil.Equals(selectKubun, HOYOU_SELECT_EBOM_SYSTEM_DAI) Then
                'SPREADのデータソースを設定する
                m_view.spdBuhin_Sheet1.DataSource = GetSpdDaiKubunList(kaihatsuFugo)
            Else
                'SPREADのデータソースを設定する
                m_view.spdBuhin_Sheet1.DataSource = GetspdBuhinList(kaihatsuFugo)
            End If

            'SPREADの列のセルの水平方向の配置を再設定する。
            SetSpdColPro()
        End Sub
#End Region

#Region "大区分一覧"
        ''' <summary>
        ''' 画面でデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetDaiKubunList(ByVal kaihatsuFugo As String) As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    T117.SYSTEM_DAIKBN_ID AS ID_FIELD,  ")
                .AppendLine("    T117.SYSTEM_DAIKBN_NAME AS VALUE_FIELD ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC1170 AS T117 ")
                .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1180 AS T118 ON T117.SYSTEM_DAIKBN_ID=T118.SYSTEM_DAIKBN_ID ")
                .AppendLine("WHERE ")
                .AppendLine("    T118.KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                .AppendLine("ORDER BY ")
                .AppendLine("   T117.SYSTEM_DAIKBN_ID ASC ")
            End With
            Return sql.ToString()
        End Function
#End Region

#Region "区分一覧"
        ''' <summary>
        ''' 画面でデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetKubunList(ByVal kaihatsuFugo As String) As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    T114.SYSTEM_KBN_ID AS ID_FIELD, ")
                .AppendLine("    T113.SYSTEM_KBN_NAME AS VALUE_FIELD ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC1140 AS T114 ")
                .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1130 AS T113 ON T114.SYSTEM_KBN_ID=T113.SYSTEM_KBN_ID ")
                .AppendLine("WHERE ")
                .AppendLine("    T114.KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                .AppendLine("ORDER BY ")
                .AppendLine("   T114.SYSTEM_KBN_ID ASC ")
            End With
            Return sql.ToString()
        End Function
#End Region

    End Class
End Namespace
