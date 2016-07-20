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

Namespace KouseiBuhin
    Public Class DispBlockList
#Region " メンバー変数 "
        ''' <summary>ビュー</summary>
        Private m_view As HoyouBuhinFrm41KouseiBuhinSelector
        ''' <summary>FpSpread 共通</summary>
        Private m_spCom As SpreadCommon
        ''' <summary>列が自動ソートされた、列の自動フィルタリングが実行された条件</summary>
        Private filterAndSort As KubunListFilterAndSortVo

        Private Const TAG_BLOCK As String = "BLOCK"
        Private Const TAG_BLOCK_NAME As String = "BLOCK_NAME"

#End Region

#Region " コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="f"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal f As HoyouBuhinFrm41KouseiBuhinSelector)
            m_view = f
            m_spCom = New SpreadCommon(m_view.spdBlock)
            filterAndSort = New KubunListFilterAndSortVo()
        End Sub
#End Region

#Region " ビュー初期化 "
        ''' <summary>
        ''' ビューの初期化        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function InitView(ByVal kaihatsuFugo As String, ByVal shisakuEvent As String, ByVal shisakuEventName As String) As RESULT

            ''Spreadの初期化
            SpreadUtil.Initialize(m_view.spdBlock)
            SetSpdColTag()
            SetSpdDataField()
            ''spreadにデータを設定する
            SetSpreadSource(kaihatsuFugo, shisakuEvent, shisakuEventName)

        End Function
#End Region

#Region "列が自動ソートされたら　処理する "
        Public Sub AutoSortedColumn(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.AutoSortedColumnEventArgs)
            filterAndSort.SortItem = m_view.spdBlock_Sheet1.Columns(e.Column).Tag
            filterAndSort.SortAscending = e.Ascending
        End Sub
#End Region

#Region "列の自動フィルタリングが実行されたら　処理する "
        Public Sub AutoFilteredColumn(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.AutoFilteredColumnEventArgs)
            Select Case m_view.spdBlock_Sheet1.Columns(e.Column).Tag
                Case TAG_BLOCK
                    filterAndSort.Kubun = FilterStringFormat(e.FilterString)
                Case TAG_BLOCK_NAME
                    filterAndSort.KubunName = FilterStringFormat(e.FilterString)
            End Select
        End Sub
        Public Function FilterStringFormat(ByVal filterString As String) As String
            If filterString.Equals(m_view.spdBlock_Sheet1.RowFilter.NonBlanksString) Then
                Return m_view.spdBlock_Sheet1.RowFilter.NonBlanksString
            End If
            If filterString.Equals(m_view.spdBlock_Sheet1.RowFilter.AllString) Then
                Return m_view.spdBlock_Sheet1.RowFilter.AllString
            End If
            Return filterString
        End Function
#End Region

#Region " SPREADの列のデータフィールドを設定する "
        ''' <summary>
        ''' SPREADの列のデータフィールドを設定する        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpdDataField()

            '列の項目を設定する           
            For i As Integer = 0 To m_view.spdBlock_Sheet1.ColumnCount - 1
                m_view.spdBlock_Sheet1.Columns(i).DataField = m_view.spdBlock_Sheet1.Columns(i).Tag
            Next
        End Sub
#End Region

#Region " SPREADの列のタグ値を設定する "
        ''' <summary>
        ''' SPREADの列のタグ値を設定する        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpdColTag()
            '列の項目を設定する           
            m_view.spdBlock_Sheet1.Columns(0).Tag = TAG_BLOCK
            m_view.spdBlock_Sheet1.Columns(1).Tag = TAG_BLOCK_NAME
        End Sub
#End Region

#Region "SPREADで 列のセルの水平方向の配置を設定する。"
        Public Sub SetSpdColPro()
            m_spCom.GetColFromTag(TAG_BLOCK).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            m_spCom.GetColFromTag(TAG_BLOCK_NAME).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            For i As Integer = 0 To m_view.spdBlock_Sheet1.ColumnCount - 1
                m_view.spdBlock_Sheet1.Columns(i).Resizable = False
            Next
            For i As Integer = 0 To m_view.spdBlock_Sheet1.RowCount - 1
                m_view.spdBlock_Sheet1.Rows(i).Resizable = False
            Next
        End Sub
#End Region

#Region " spreadでデータを取得する "
        Public Function GetIchiranList(ByVal kaihatsuFugo As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetBuhinSakuseiList(kaihatsuFugo), dtData)
            End Using
            Return dtData
        End Function
        Public Function GetIchiranShisakuListFromC(ByVal kaihatsuFugo As String, ByVal shisakuEventCode As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetBuhinSakuseiShisakuListFromC(kaihatsuFugo, shisakuEventCode), dtData)
            End Using
            Return dtData
        End Function
        Public Function GetIchiranShisakuListFromN(ByVal kaihatsuFugo As String, ByVal shisakuEventName As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetBuhinSakuseiShisakuListFromN(kaihatsuFugo, shisakuEventName), dtData)
            End Using
            Return dtData
        End Function
#End Region

#Region "spreadのbind"
        Private Sub SetSpreadSource(ByVal kaihatsuFugo As String, _
                                    ByVal shisakuEventCode As String, _
                                    ByVal shisakuEventName As String)
            If StringUtil.IsEmpty(shisakuEventCode) And StringUtil.IsEmpty(shisakuEventName) Then
                'SPREADのデータソースを設定する
                m_view.spdBlock_Sheet1.DataSource = GetIchiranList(kaihatsuFugo)
            ElseIf StringUtil.IsNotEmpty(shisakuEventCode) Then
                'SPREADのデータソースを設定する
                m_view.spdBlock_Sheet1.DataSource = GetIchiranShisakuListFromC(kaihatsuFugo, shisakuEventCode)
            ElseIf StringUtil.IsNotEmpty(shisakuEventName) Then
                'SPREADのデータソースを設定する
                m_view.spdBlock_Sheet1.DataSource = GetIchiranShisakuListFromN(kaihatsuFugo, shisakuEventName)
            End If

            'SPREADの列のセルの水平方向の配置を再設定する。
            SetSpdColPro()
        End Sub
#End Region

#Region "spreadのbind（あいまい）"
        Public Sub SetSpreadSourceAimai(ByVal kaihatsuFugo As String, ByVal blockName As String)
            'SPREADのデータソースを設定する
            m_view.spdBlock_Sheet1.DataSource = GetIchiranListAimai(kaihatsuFugo, blockname)
            'SPREADの列のセルの水平方向の配置を再設定する。
            SetSpdColPro()
        End Sub
        Public Function GetIchiranListAimai(ByVal kaihatsuFugo As String, ByVal blockName As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetBuhinSakuseiListAimai(kaihatsuFugo, blockName), dtData)
            End Using
            Return dtData
        End Function
#End Region


#Region "ブロック一覧"
        ''' <summary>
        ''' 画面でデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetBuhinSakuseiList(ByVal kaihatsuFugo As String) As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    T004.BLOCK_NO AS BLOCK_NO, ")
                .AppendLine("    T004.BLOCK_NAME AS BLOCK_NAME ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0080 AS T008 ")
                .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 AS T004 ON T008.BLOCK_NO_KINO = T004.BLOCK_NO ")
                .AppendLine("WHERE ")
                .AppendLine("    T008.KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                .AppendLine("AND T008.HAISI_DATE = 99999999 ")
                .AppendLine("GROUP BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
                .AppendLine("ORDER BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
            End With
            Return sql.ToString()
        End Function

        ''' <summary>
        ''' 画面でデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetBuhinSakuseiShisakuListFromC(ByVal kaihatsuFugo As String, _
                                                               ByVal shisakuEventCode As String) As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    T004.BLOCK_NO AS BLOCK_NO, ")
                .AppendLine("    T004.BLOCK_NAME AS BLOCK_NAME ")
                .AppendLine("FROM ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL AS INSTL ")
                .AppendLine("    INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT AS EVENT ON INSTL.SHISAKU_EVENT_CODE = EVENT.SHISAKU_EVENT_CODE ")
                .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 AS T004 ON INSTL.SHISAKU_BLOCK_NO = T004.BLOCK_NO ")
                .AppendLine("WHERE ")
                .AppendLine("    EVENT.SHISAKU_KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                .AppendLine("AND EVENT.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' ")
                .AppendLine(" AND INSTL.SHISAKU_BLOCK_NO_KAITEI_NO=  ")
                .AppendLine(" (  ")
                .AppendLine("     SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine("     FROM   ")
                .AppendLine("	        " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT)  ")
                .AppendLine("     WHERE  ")
                .AppendLine("	        SHISAKU_EVENT_CODE=INSTL.SHISAKU_EVENT_CODE ")
                .AppendLine("     AND SHISAKU_BUKA_CODE=INSTL.SHISAKU_BUKA_CODE ")
                .AppendLine("     AND SHISAKU_BLOCK_NO=INSTL.SHISAKU_BLOCK_NO ")
                .AppendLine(" ) ")
                .AppendLine("GROUP BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
                .AppendLine("ORDER BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
            End With
            Return sql.ToString()
        End Function
        ''' <summary>
        ''' 画面でデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetBuhinSakuseiShisakuListFromN(ByVal kaihatsuFugo As String, ByVal shisakuEventName As String) As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    T004.BLOCK_NO AS BLOCK_NO, ")
                .AppendLine("    T004.BLOCK_NAME AS BLOCK_NAME ")
                .AppendLine("FROM ")
                .AppendLine(MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL AS INSTL ")
                .AppendLine("    INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT AS EVENT ON INSTL.SHISAKU_EVENT_CODE = EVENT.SHISAKU_EVENT_CODE ")
                .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 AS T004 ON INSTL.SHISAKU_BLOCK_NO = T004.BLOCK_NO ")
                .AppendLine("WHERE ")
                .AppendLine("    EVENT.SHISAKU_KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                .AppendLine("AND EVENT.SHISAKU_EVENT_NAME = '" & shisakuEventName & "' ")
                .AppendLine(" AND INSTL.SHISAKU_BLOCK_NO_KAITEI_NO=  ")
                .AppendLine(" (  ")
                .AppendLine("     SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine("     FROM   ")
                .AppendLine("	        " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT)  ")
                .AppendLine("     WHERE  ")
                .AppendLine("	        SHISAKU_EVENT_CODE=INSTL.SHISAKU_EVENT_CODE ")
                .AppendLine("     AND SHISAKU_BUKA_CODE=INSTL.SHISAKU_BUKA_CODE ")
                .AppendLine("     AND SHISAKU_BLOCK_NO=INSTL.SHISAKU_BLOCK_NO ")
                .AppendLine(" ) ")
                .AppendLine("GROUP BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
                .AppendLine("ORDER BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
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
        ''' <summary>
        ''' 画面でデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetBuhinSakuseiListAimai(ByVal kaihatsuFugo As String, ByVal blockName As String) As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT  ")
                .AppendLine("    T004.BLOCK_NO AS BLOCK_NO, ")
                .AppendLine("    T004.BLOCK_NAME AS BLOCK_NAME ")
                .AppendLine("FROM ")
                .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0080 AS T008 ")
                .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 AS T004 ON T008.BLOCK_NO_KINO = T004.BLOCK_NO ")
                .AppendLine("WHERE ")
                .AppendLine("    T008.KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                .AppendLine("AND T008.HAISI_DATE = 99999999 ")
                If Not blockName = String.Empty Then
                    .AppendFormat("    AND T004.BLOCK_NAME LIKE '{0}%'", blockName)
                End If
                .AppendLine("GROUP BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
                .AppendLine("ORDER BY ")
                .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
            End With
            Return sql.ToString()
        End Function
#End Region
    End Class
End Namespace
