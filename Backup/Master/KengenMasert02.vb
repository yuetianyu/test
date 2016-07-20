Imports ShisakuCommon
Imports EBom.Common
Imports EBom.Common.mdlConstraint
Imports EBom.Data
Imports System.Reflection
Imports System.Text
Imports FarPoint.Win.Spread.CellType
Imports FarPoint.Win
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd
''' <summary>
''' 権限マスター一覧
''' </summary>
''' <remarks></remarks>
Public Class KengenMasert02
#Region " メンバー変数 "
    ''' <summary>ビュー</summary>
    Private m_view As frm51KengenMaster02
    ''' <summary>FpSpread 列項目</summary>
    Private items As String()
    ''' <summary>FpSpread 共通</summary>
    Private m_spCom As SpreadCommon
    ''' <summary>承認用ポップアップ部課機能追加用</summary>
    Const m_Shonin = "SHONIN"
    ''' <summary>日程管理ツールの権限用</summary>
    Const m_Nittei = "NITTEI"
    ''' <summary>オーダーシートの権限用</summary>
    Const m_OrderSheet = "ORDERSHEET"
#End Region

#Region " コンストラクタ "
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="f"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal f As frm51KengenMaster02)
        m_view = f
        m_spCom = New SpreadCommon(m_view.spdInfo)
        items = New String() {"KINO_ID_1", "KINO_ID_2", "AUTHORITY_KBN"}
    End Sub
#End Region

#Region " ビュー初期化 "
    ''' <summary>
    ''' ビューの初期化
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitView()
        ShisakuFormUtil.setTitleVersion(m_view)
        ShisakuFormUtil.SetDateTimeNow(m_view.LblDateNow, m_view.LblTimeNow)
        ShisakuFormUtil.SetIdAndBuka(m_view.LblCurrUserId, m_view.LblCurrBukaName)
        m_view.spdInfo_Sheet1.SetRowSizeable(m_view.spdInfo_Sheet1.Rows.Count, False)
        m_view.spdInfo_Sheet1.SetColumnSizeable(m_view.spdInfo_Sheet1.ColumnCount, False)
        'メニューとシステム使用権限のデータの設定
        SetUserInit()
        SetValueInit()
    End Sub
#End Region
#Region " 更新 "
    ''' <summary>
    ''' 更新ボタンを押す
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UpdateButtonClick()
        'バックカラー再設定

        If m_view.cmbMENU.Visible Then
            ShisakuFormUtil.initlColor(m_view.cmbMENU)
        End If
        If m_view.spdInfo.Visible Then
            Dim rowIndex As Integer
            For rowIndex = 0 To m_view.spdInfo_Sheet1.RowCount - 1
                ShisakuFormUtil.initlColor(m_spCom.GetCell(items(0), rowIndex))
                ShisakuFormUtil.initlColor(m_spCom.GetCell(items(1), rowIndex))
                ShisakuFormUtil.initlColor(m_spCom.GetCell(items(2), rowIndex))
            Next
        End If
        'チェック
        If Not UpdateCheck() = RESULT.OK Then
            Exit Sub
        End If
        'DBに更新する
        Update()
        m_view.Close()

    End Sub
#End Region

#Region " spread tip "
    Public Sub spdInfo_TextTipFetch(ByVal e As FarPoint.Win.Spread.TextTipFetchEventArgs)
        Dim col As Spread.Column = m_view.spdInfo_Sheet1.Columns(e.Column)
        Select Case col.Tag
            Case items(1)
                e.TipText = "ユーザーに付与する権限を指定してください。"
                e.ShowTip = True
            Case Else
                e.ShowTip = False
        End Select
    End Sub
#End Region

#Region " 「画面」項目を選択したら、機能bindを更新する "
    ''' <summary>
    ''' 「画面」項目を選択したら、機能bindを更新する
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub KinoBind(ByVal e As FarPoint.Win.Spread.ChangeEventArgs)
        If m_view.spdInfo_Sheet1.Columns(e.Column).Tag = items(0) Then
            Dim program As String = m_view.spdInfo_Sheet1.Cells(e.Row, e.Column).Value
            Dim cmbKinoType As New ComboBoxCellType()
            '2012/01/24
            '承認ポップアップ用に場合分け
            If ShisakuComFunc.GetRelativeValue(ShisakuGlobal.KINO_NAME, ShisakuGlobal.KINO_ID, program) = m_Shonin Then
                cmbKinoType.Items = GetKINOForApprove(0)
                m_spCom.GetCell(items(1), e.Row).CellType = cmbKinoType
            Else
                cmbKinoType.Items = GetKINO(program)
                m_spCom.GetCell(items(1), e.Row).CellType = cmbKinoType
            End If
        End If
    End Sub
#End Region

#Region " メニューをチェッジしたら システム使用権限の内容をクリアする "
    ''' <summary>
    ''' メニューをチェッジしたら システム使用権限の内容をクリアする 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub MenuChange()
        Dim i As Integer
        For i = 0 To m_view.spdInfo_Sheet1.RowCount - 1
            m_spCom.SetValue(items(0), i, "")
            m_spCom.SetValue(items(1), i, "")
            m_spCom.SetValue(items(2), i, "")
        Next
        If m_view.Visible Then
            'システム使用権限の「画面」のbind
            Dim cmbProgramType As New ComboBoxCellType()
            cmbProgramType.Items = GetProgram(m_view.cmbMENU.Text)
            m_view.spdInfo_Sheet1.Columns(m_spCom.GetColIdxFromTag(items(0))).CellType = cmbProgramType
        End If

    End Sub
#End Region

#Region " 更新ボタンを押して　チェックする "
    ''' <summary>
    ''' 更新ボタンを押して　チェックする
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateCheck() As RESULT
        'システム使用権限一覧の画面、機能、権限が全て選択されていない場合、エラーメッセージを表示する。
        '※エラー箇所へカーソルを設定し、バックカラーは赤。
        If m_view.spdInfo.Visible Then
            Dim errorCellCount As Integer
            Dim rowIndex As Integer
            Dim lstValue As New ArrayList
            Dim isDup As Boolean = False
            For rowIndex = 0 To m_view.spdInfo_Sheet1.RowCount - 1
                Dim currValue As String
                '2012/01/24
                '承認ポップアップ用
                If Not m_spCom.GetValue(items(0), rowIndex) = String.Empty Then
                    If ShisakuComFunc.GetRelativeValue(ShisakuGlobal.KINO_NAME, ShisakuGlobal.KINO_ID, m_spCom.GetValue(items(0), rowIndex)) = m_Shonin Then
                        currValue = m_spCom.GetValue(items(0), rowIndex)
                    Else
                        currValue = m_spCom.GetValue(items(0), rowIndex) + "^" + m_spCom.GetValue(items(1), rowIndex)
                    End If
                Else
                    currValue = m_spCom.GetValue(items(0), rowIndex) + "^" + m_spCom.GetValue(items(1), rowIndex)
                End If
                Dim selCount As Integer = 0
                If Not (m_spCom.GetValue(items(0), rowIndex) = String.Empty And _
                         m_spCom.GetValue(items(1), rowIndex) = String.Empty And m_spCom.GetValue(items(2), rowIndex) = String.Empty) Then
                    If m_spCom.GetValue(items(0), rowIndex) = String.Empty Then
                        errorCellCount = errorCellCount + 1
                        ShisakuFormUtil.onErro(m_spCom.GetCell(items(0), rowIndex))
                    End If
                    If m_spCom.GetValue(items(1), rowIndex) = String.Empty Then
                        errorCellCount = errorCellCount + 1
                        ShisakuFormUtil.onErro(m_spCom.GetCell(items(1), rowIndex))
                    End If
                    If m_spCom.GetValue(items(2), rowIndex) = String.Empty Then
                        errorCellCount = errorCellCount + 1
                        ShisakuFormUtil.onErro(m_spCom.GetCell(items(2), rowIndex))
                    End If
                    If lstValue.Contains(currValue) Then
                        isDup = True
                    End If
                    lstValue.Add(currValue)
                End If
            Next
            If errorCellCount > 0 Then
                ComFunc.ShowErrMsgBox(ShisakuMsg.E0018)
                Return RESULT.NG
            End If
            If isDup Then
                ComFunc.ShowErrMsgBox(E0024)
                Return RESULT.NG
            End If
        End If
        Return RESULT.OK
    End Function

#End Region

#Region " DBへ更新 "
    ''' <summary>
    ''' DBへ更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Update()
        'spread中データを取得する
        Dim rowIndex As Integer = -1
        Dim programId As String
        Dim kinoId As String
        Dim kengenId As String
        Dim programName As String
        Dim kinoName As String
        Dim kengenName As String
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
            db.Open()
            db.BeginTransaction()
            '該当するユーザーのデータを削除する
            programId = "MENU"
            kinoId = ""
            kengenName = m_view.cmbMENU.Text
            kengenId = ShisakuComFunc.GetRelativeValue(ShisakuGlobal.PROGRAM_NAME_MENU, _
                                                                       New String() {"0", "1", "2", "3"}, kengenName)

            Dim shisakuDate As New ShisakuDate()
            Dim dateNow As String = shisakuDate.CurrentDateDbFormat
            Dim timeNow As String = shisakuDate.CurrentTimeDbFormat
            If m_view.cmbMENU.Visible Then
                '削除する。
                db.AddParameter("@USER_ID", m_view.lblUserCode.Text, DbType.AnsiString)
                db.AddParameter("@KINO_ID_1", programId, DbType.AnsiString)
                db.ExecuteNonQuery(DataSqlCommon.DelAuthorityUserByIdSql) 'ユーザー情報を全削除
                'メニューのデータを登録する
                db.AddParameter("@USER_ID", m_view.lblUserCode.Text, DbType.AnsiString)
                db.AddParameter("@KINO_ID_1", programId, DbType.AnsiString)
                db.AddParameter("@KINO_ID_2", kinoId, DbType.AnsiString)
                db.AddParameter("@AUTHORITY_KBN", kengenId, DbType.AnsiString)
                db.AddParameter("@CREATED_USER_ID", LoginInfo.Now.UserId, DbType.AnsiString)
                db.AddParameter("@CREATED_DATE", dateNow, DbType.AnsiString)
                db.AddParameter("@CREATED_TIME", timeNow, DbType.AnsiString)
                db.AddParameter("@UPDATED_USER_ID", LoginInfo.Now.UserId, DbType.AnsiString)
                db.AddParameter("@UPDATED_DATE", dateNow, DbType.AnsiString)
                db.AddParameter("@UPDATED_TIME", timeNow, DbType.AnsiString)
                db.ExecuteNonQuery(DataSqlCommon.InsAuthorityUserMenuSql)
            End If
            If m_view.spdInfo.Visible Then
                '削除する。
                db.AddParameter("@USER_ID", m_view.lblUserCode.Text, DbType.AnsiString)
                db.AddParameter("@KINO_ID_1", programId, DbType.AnsiString)
                db.ExecuteNonQuery(DataSqlCommon.DelAuthorityUserByNotMenuSql) 'ユーザー情報を全削除
                'システム使用権限のデータを登録する
                For Each oneRow As FarPoint.Win.Spread.Row In m_view.spdInfo_Sheet1.Rows
                    rowIndex = rowIndex + 1
                    programName = m_spCom.GetValue(items(0), rowIndex)
                    If programName = String.Empty Then
                        Continue For
                    End If

                    kinoName = m_spCom.GetValue(items(1), rowIndex)
                    kengenName = m_spCom.GetValue(items(2), rowIndex)
                    kengenId = ShisakuComFunc.GetRelativeValue(ShisakuGlobal.KENGEN_NAME, _
                                                                               ShisakuGlobal.KENGEN_ID, kengenName)
                    '2012/01/24 承認アラート機能
                    '承認の列だった場合
                    Dim strShonin As String
                    strShonin = ShisakuComFunc.GetRelativeValue(ShisakuGlobal.KINO_NAME, _
                                                                               ShisakuGlobal.KINO_ID, programName)
                    If strShonin = m_Shonin Then
                        db.AddParameter("@USER_ID", m_view.lblUserCode.Text, DbType.AnsiString)
                        db.AddParameter("@NAME1", programName, DbType.AnsiString)
                        db.AddParameter("@NAME2", programName, DbType.AnsiString)
                        db.AddParameter("@BUKA_CODE", GetKINOForApprove(1, kinoName)(0), DbType.AnsiString)
                        db.AddParameter("@AUTHORITY_KBN", kengenId, DbType.AnsiString)
                        db.AddParameter("@CREATED_USER_ID", LoginInfo.Now.UserId, DbType.AnsiString)
                        db.AddParameter("@CREATED_DATE", dateNow, DbType.AnsiString)
                        db.AddParameter("@CREATED_TIME", timeNow, DbType.AnsiString)
                        db.AddParameter("@UPDATED_USER_ID", LoginInfo.Now.UserId, DbType.AnsiString)
                        db.AddParameter("@UPDATED_DATE", dateNow, DbType.AnsiString)
                        db.AddParameter("@UPDATED_TIME", timeNow, DbType.AnsiString)

                        db.ExecuteNonQuery(DataSqlCommon.InsAuthorityUserSql(kengenName))
                    ElseIf strShonin = m_Nittei Then
                        '2014/02/05 日程管理ツール権限用更新を追加
                        db.AddParameter("@USER_ID", m_view.lblUserCode.Text, DbType.AnsiString)
                        db.AddParameter("@NAME1", programName, DbType.AnsiString)
                        db.AddParameter("@NAME2", kinoName, DbType.AnsiString)
                        db.AddParameter("@AUTHORITY_KBN", kengenId, DbType.AnsiString)
                        db.AddParameter("@CREATED_USER_ID", LoginInfo.Now.UserId, DbType.AnsiString)
                        db.AddParameter("@CREATED_DATE", dateNow, DbType.AnsiString)
                        db.AddParameter("@CREATED_TIME", timeNow, DbType.AnsiString)
                        db.AddParameter("@UPDATED_USER_ID", LoginInfo.Now.UserId, DbType.AnsiString)
                        db.AddParameter("@UPDATED_DATE", dateNow, DbType.AnsiString)
                        db.AddParameter("@UPDATED_TIME", timeNow, DbType.AnsiString)
                        '
                        db.ExecuteNonQuery(DataSqlCommon.InsAuthorityUserSql("", "NITTEI"))
                    ElseIf strShonin = m_OrderSheet Then
                        '2015/02/05 オーダーシート権限用更新を追加
                        db.AddParameter("@USER_ID", m_view.lblUserCode.Text, DbType.AnsiString)
                        db.AddParameter("@NAME1", programName, DbType.AnsiString)
                        db.AddParameter("@NAME2", kinoName, DbType.AnsiString)
                        db.AddParameter("@AUTHORITY_KBN", kengenId, DbType.AnsiString)
                        db.AddParameter("@CREATED_USER_ID", LoginInfo.Now.UserId, DbType.AnsiString)
                        db.AddParameter("@CREATED_DATE", dateNow, DbType.AnsiString)
                        db.AddParameter("@CREATED_TIME", timeNow, DbType.AnsiString)
                        db.AddParameter("@UPDATED_USER_ID", LoginInfo.Now.UserId, DbType.AnsiString)
                        db.AddParameter("@UPDATED_DATE", dateNow, DbType.AnsiString)
                        db.AddParameter("@UPDATED_TIME", timeNow, DbType.AnsiString)
                        '
                        db.ExecuteNonQuery(DataSqlCommon.InsAuthorityUserSql("", "ORDERSHEET"))
                    Else
                        db.AddParameter("@USER_ID", m_view.lblUserCode.Text, DbType.AnsiString)
                        db.AddParameter("@NAME1", programName, DbType.AnsiString)
                        db.AddParameter("@NAME2", kinoName, DbType.AnsiString)
                        db.AddParameter("@AUTHORITY_KBN", kengenId, DbType.AnsiString)
                        db.AddParameter("@CREATED_USER_ID", LoginInfo.Now.UserId, DbType.AnsiString)
                        db.AddParameter("@CREATED_DATE", dateNow, DbType.AnsiString)
                        db.AddParameter("@CREATED_TIME", timeNow, DbType.AnsiString)
                        db.AddParameter("@UPDATED_USER_ID", LoginInfo.Now.UserId, DbType.AnsiString)
                        db.AddParameter("@UPDATED_DATE", dateNow, DbType.AnsiString)
                        db.AddParameter("@UPDATED_TIME", timeNow, DbType.AnsiString)

                        db.ExecuteNonQuery(DataSqlCommon.InsAuthorityUserSql)
                    End If
                Next
            End If
            db.Commit()
        End Using
    End Sub
#End Region

#Region " SPREAD初期化を設定する "
    ''' <summary>
    ''' SPREAD初期化を設定する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSpreadInit()
        'spread 初期化行数
        m_view.spdInfo_Sheet1.Rows.Add(0, 8)
        'コンボボックス型セルの設定を行います。
        'プログラム名
        Dim cmbProgramType As New ComboBoxCellType()
        m_view.spdInfo_Sheet1.Columns(0).CellType = cmbProgramType
        '機能ID01名
        Dim cmbKinoType As New ComboBoxCellType()
        cmbKinoType.Items = GetKINO("")
        m_view.spdInfo_Sheet1.Columns(1).CellType = cmbKinoType
        '権限
        Dim cmbKengenType As New ComboBoxCellType()
        cmbKengenType.Items = ShisakuGlobal.KENGEN_NAME
        m_view.spdInfo_Sheet1.Columns(2).CellType = cmbKengenType
        m_view.spdInfo_Sheet1.Columns(0).DataField = items(0)
        m_view.spdInfo_Sheet1.Columns(1).DataField = items(1)
        m_view.spdInfo_Sheet1.Columns(2).DataField = items(2)
        m_view.spdInfo_Sheet1.Columns(0).Tag = items(0)
        m_view.spdInfo_Sheet1.Columns(1).Tag = items(1)
        m_view.spdInfo_Sheet1.Columns(2).Tag = items(2)
        m_view.spdInfo_Sheet1.SetActiveCell(0, 1)
        m_view.spdInfo_Sheet1.SetActiveCell(0, 0)
    End Sub
#End Region

#Region " メニューとSPREADで画面コンボボックスの値を取得する "
    ''' <summary>
    ''' メニュープログラム名を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMenuProgram() As String()
        Dim dtData As New DataTable
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            Dim sql As New StringBuilder
            db.Fill(DataSqlCommon.Get_PROGRAM_MENU_Sql(), dtData)
        End Using
        Dim retStrs(dtData.Rows.Count) As String
        retStrs(0) = ""
        Dim rowIndex As Integer = 1
        For Each oneRow As DataRow In dtData.Rows
            retStrs(rowIndex) = oneRow("SHISAKU_PROGRAM_NAME_1")
        Next
        Return retStrs
    End Function
    ''' <summary>
    ''' メニュー以外プログラム名を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetProgram(ByVal menuName As String) As String()
        Dim dtData As New DataTable
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            Dim sql As New StringBuilder
            db.Fill(DataSqlCommon.Get_PROGRAM_Sql(menuName), dtData)
        End Using
        Dim retStrs(dtData.Rows.Count) As String
        retStrs(0) = ""
        Dim rowIndex As Integer = 1
        For Each oneRow As DataRow In dtData.Rows
            retStrs(rowIndex) = oneRow("SHISAKU_PROGRAM_NAME_1")
            rowIndex = rowIndex + 1
        Next
        Return retStrs
    End Function
    ''' <summary>
    ''' 機能のデータを取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetKINO(ByVal programName As String) As String()
        If programName = String.Empty Then
            Return New String() {""}
        End If
        Dim dtData As New DataTable
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            Dim sql As New StringBuilder
            db.AddParameter("@SHISAKU_PROGRAM_NAME_1", programName, DbType.AnsiString)
            db.Fill(DataSqlCommon.Get_KINO_Sql(), dtData)
        End Using
        Dim retStrs(dtData.Rows.Count) As String
        retStrs(0) = ""
        Dim rowIndex As Integer = 1
        For Each oneRow As DataRow In dtData.Rows
            retStrs(rowIndex) = oneRow("SHISAKU_KINO_NAME_1")
            rowIndex = rowIndex + 1
        Next
        Return retStrs
    End Function
    Public Shared Function GetBUKAMASTERSQL() As String
        Dim sql As String = "" _
            & "SELECT " _
            & "	SHISAKU_BUKA_CODE," _
            & "	CASE ISNULL(B.KA_RYAKU_NAME,'')" _
            & "	WHEN '' THEN D.SHISAKU_BUKA_CODE" _
            & "	ELSE B.KA_RYAKU_NAME" _
            & "	END AS KA_RYAKU_NAME" _
            & " FROM " _
            & "(" _
            & "	SELECT SHISAKU_BUKA_CODE FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT GROUP BY SHISAKU_BUKA_CODE" _
            & ") D LEFT OUTER JOIN " _
            & "(SELECT BU_CODE,KA_CODE,KA_RYAKU_NAME FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 WITH (NOLOCK, NOWAIT) WHERE SITE_KBN = '1') B" _
            & " ON D.SHISAKU_BUKA_CODE = B.BU_CODE + B.KA_CODE"

        Return sql

    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sw">0:コードから名称　1:名称からコード</param>
    ''' <param name="strBukaInfo">部課コードまたは部課名称</param>
    ''' <returns>部課コードまたは部課名称の配列</returns>
    ''' <remarks></remarks>
    Private Function GetKINOForApprove(ByVal sw As String, Optional ByVal strBukaInfo As String = "") As String()
        '2012/01/24 承認アラート機能
        Dim sql As String = GetBUKAMASTERSQL()
        Dim sql_add As String = ""

        Dim dtData As New DataTable
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
            db.Open()
            If strBukaInfo <> "" Then
                If sw = 0 Then
                    'コードから名前を取得
                    db.AddParameter("@SHISAKU_BUKA_CODE", strBukaInfo, DbType.AnsiString)
                    sql = sql & "   WHERE SHISAKU_BUKA_CODE = @SHISAKU_BUKA_CODE  "
                Else
                    '名前からコードを取得
                    'db.AddParameter("@KA_RYAKU_NAME", strBukaInfo, DbType.AnsiString)
                    'sql = sql & "   WHERE KA_RYAKU_NAME = @KA_RYAKU_NAME  "
                    db.AddParameter("@KA_RYAKU_NAME", strBukaInfo, DbType.AnsiString)
                    db.AddParameter("@SHISAKU_BUKA_CODE", strBukaInfo, DbType.AnsiString)
                    sql = sql & "   WHERE (KA_RYAKU_NAME = @KA_RYAKU_NAME OR SHISAKU_BUKA_CODE = @SHISAKU_BUKA_CODE) "
                End If
                sql_add = "   AND SHISAKU_BUKA_CODE <> '7221'  "
                sql = sql & sql_add
            End If
            If sql_add = "" Then
                sql_add = "   WHERE SHISAKU_BUKA_CODE <> '7221'  "
                sql = sql & sql_add
            End If
            '------------------------------------------------
            '２次改修
            '   部課コード対策。
            '   並び順を変える。
            'sql = sql & " ORDER BY SHISAKU_BUKA_CODE "
            sql = sql & " ORDER BY KA_RYAKU_NAME "
            '------------------------------------------------
            db.Fill(sql.ToString, dtData)
        End Using

        '------------------------------------------------
        '２次改修
        '   部課コード対策。
        '   重複データを除外。
        Dim viw As New DataView(dtData)
        Dim isDistinct As Boolean = True
        'Dim dtFilter As DataTable = viw.ToTable(isDistinct, "KA_RYAKU_NAME")
        dtData = viw.ToTable(isDistinct, "KA_RYAKU_NAME")
        '------------------------------------------------


        If strBukaInfo <> "" Then
            Dim retStrs(dtData.Rows.Count) As String
            Dim rowIndex As Integer = 0
            For Each oneRow As DataRow In dtData.Rows
                '------------------------------------------------
                '２次改修
                '   部課コード対策。
                retStrs(0) = oneRow("KA_RYAKU_NAME")
                'If sw = 0 Then
                '    retStrs(0) = oneRow("KA_RYAKU_NAME")
                'Else
                '    retStrs(0) = oneRow("SHISAKU_BUKA_CODE")
                'End If
                '------------------------------------------------
                rowIndex = rowIndex + 1
            Next
            Return retStrs
        Else
            Dim retStrs(dtData.Rows.Count) As String
            retStrs(0) = ""
            Dim rowIndex As Integer = 1
            For Each oneRow As DataRow In dtData.Rows
                '------------------------------------------------
                '２次改修
                '   部課コード対策。
                retStrs(rowIndex) = oneRow("KA_RYAKU_NAME")
                'If sw = 0 Then
                '    retStrs(rowIndex) = oneRow("KA_RYAKU_NAME")
                'Else
                '    retStrs(rowIndex) = oneRow("SHISAKU_BUKA_CODE")
                'End If
                '------------------------------------------------
                rowIndex = rowIndex + 1
            Next
            Return retStrs
        End If

    End Function

#End Region

#Region " 「メニュー」コンボボックスの初期化 "
    ''' <summary>
    ''' 「メニュー」コンボボックスの初期化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMenuBind()
        m_view.cmbMENU.DataSource = ShisakuGlobal.PROGRAM_NAME_MENU
        If m_view.lblUserKubun.Text.Equals(ShisakuGlobal.UserKubunCd(0)) Then
            m_view.cmbMENU.Text = ShisakuGlobal.PROGRAM_NAME_MENU(1)
        ElseIf m_view.lblUserKubun.Equals(ShisakuGlobal.UserKubunCd(1)) Then
            m_view.cmbMENU.Text = ShisakuGlobal.PROGRAM_NAME_MENU(2)
        ElseIf m_view.lblUserKubun.Equals(ShisakuGlobal.UserKubunCd(2)) Then
            m_view.cmbMENU.Text = ShisakuGlobal.PROGRAM_NAME_MENU(3)
        Else
            m_view.cmbMENU.Text = ShisakuGlobal.PROGRAM_NAME_MENU(0)
        End If
    End Sub
#End Region

#Region " 該当するユーザーのメニューとシステム使用権限を設定する。 "
    ''' <summary>
    ''' 該当するユーザーのメニューとシステム使用権限を設定する。
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUserInit()
        'メニューとシステム使用権限不可の時　更新ボタンが使用不可
        'アドミンユーザー判断
        If Not LoginInfo.Now.LoginAuthority = USERMAST_AUTHORITY.ADMIN Then
            Dim dtUser As DataTable = GetUserLoginInfo()
            'メニュー権限を持つ判断
            If dtUser.Rows.Count < 1 Then
                m_view.lblMENU.Visible = False
                m_view.lblShiyouKengen.Visible = False
                m_view.cmbMENU.Visible = False
                m_view.spdInfo.Visible = False
            Else
                If Not dtUser.Rows(0)("SHISAKU_MENU_SETTEI") = KENGEN.USE_OK Then
                    m_view.lblMENU.Visible = False
                    m_view.cmbMENU.Visible = False
                End If
                If Not dtUser.Rows(0)("KENGEN_SETTEI") = KENGEN.USE_OK Then
                    m_view.lblShiyouKengen.Visible = False
                    m_view.spdInfo.Visible = False
                End If
            End If
        End If
    End Sub
#End Region

#Region " 該当するユーザーのログインマスター情報 "
    ''' <summary>
    ''' 指定のユーザーのログインマスター情報
    ''' </summary>
    ''' <returns>指定ユーザーのログイン情報のデータテーブル</returns>
    ''' <remarks></remarks>
    Private Function GetUserLoginInfo() As DataTable
        Dim dtData As New DataTable
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            Dim sql As New StringBuilder
            db.AddParameter("@SEKKEI_SHAIN_NO", LoginInfo.Now.UserId, DbType.AnsiString)
            db.Fill(DataSqlCommon.GetLoginSql(), dtData)
        End Using
        Return dtData
    End Function
#End Region

#Region " 初期化ユーザーの権限（ユーザー別） "
    ''' <summary>
    ''' ユーザーの権限情報を取得する
    ''' </summary>
    ''' <param name="isMenu"></param>
    ''' <returns>指定ユーザーの権限情報のデータテーブル</returns>
    ''' <remarks></remarks>
    Private Function GetDataListInfo(ByVal isMenu As Boolean) As DataTable
        Dim dtData As New DataTable
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
            db.Open()
            Dim sql As New StringBuilder
            db.AddParameter("@USER_ID", m_view.lblUserCode.Text, DbType.AnsiString)
            db.Fill(DataSqlCommon.Get_AUTHORITY_USER_Sql(isMenu), dtData)
        End Using
        Return dtData
    End Function
#End Region
#Region " データはspreadまたはメニューコンボボックスへ設定する "
    ''' <summary>
    ''' データはspreadまたはメニューコンボボックスへ設定する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValueInit()
        If m_view.cmbMENU.Visible Then
            'メニューbind
            SetMenuBind()
            'メニュー値を設定する
            m_view.cmbMENU.Text = m_view.MenuName
        End If
        If m_view.spdInfo.Visible Then

            '権限があるので更新ボタンを表示する。
            m_view.btnCall.Visible = True

            SetSpreadInit()
            'システム使用権限の「画面」のbind
            Dim cmbProgramType As New ComboBoxCellType()
            cmbProgramType.Items = GetProgram(m_view.cmbMENU.Text)
            m_view.spdInfo_Sheet1.Columns(0).CellType = cmbProgramType
            Dim dtData As DataTable = GetDataListInfo(False)
            Dim rowIndex As Integer
            For Each oneRow As DataRow In dtData.Rows
                Dim programId As String = oneRow("KINO_ID_1")
                Dim programName As String = oneRow("NAME1")
                If m_view.spdInfo.Visible Then
                    '2012/01/24 承認アラート機能の為、場合分けを追加
                    If programId = m_Shonin Then
                        Dim cmbKinoType As New ComboBoxCellType
                        cmbKinoType.Items = GetKINOForApprove(0)

                        m_view.spdInfo_Sheet1.Cells(rowIndex, 1).CellType = cmbKinoType
                        Dim kinoName As String = GetKINOForApprove(0, oneRow("KINO_ID_3"))(0)

                        Dim kengenName As String = ShisakuComFunc.GetRelativeValue(ShisakuGlobal.KENGEN_ID, _
                                                                                    ShisakuGlobal.KENGEN_NAME, oneRow(items(2)))
                        m_spCom.SetValue(items(0), rowIndex, programName)
                        m_spCom.SetValue(items(1), rowIndex, kinoName)
                        m_spCom.SetValue(items(2), rowIndex, kengenName)
                    ElseIf programId = m_Nittei Then
                        Dim cmbKinoType As New ComboBoxCellType
                        cmbKinoType.Items = GetKINO(programName)

                        m_view.spdInfo_Sheet1.Cells(rowIndex, 1).CellType = cmbKinoType
                        Dim kinoName As String = ShisakuComFunc.GetRelativeValue(ShisakuGlobal.NITTEI_ID, _
                                                                                    ShisakuGlobal.NITTEI_NAME, oneRow("KINO_ID_2"))
                        Dim kengenName As String = ShisakuComFunc.GetRelativeValue(ShisakuGlobal.KENGEN_ID, _
                                                                                    ShisakuGlobal.KENGEN_NAME, oneRow(items(2)))
                        m_spCom.SetValue(items(0), rowIndex, programName)
                        m_spCom.SetValue(items(1), rowIndex, kinoName)
                        m_spCom.SetValue(items(2), rowIndex, kengenName)
                    ElseIf programId = m_OrderSheet Then
                        Dim cmbKinoType As New ComboBoxCellType
                        cmbKinoType.Items = GetKINO(programName)

                        m_view.spdInfo_Sheet1.Cells(rowIndex, 1).CellType = cmbKinoType
                        Dim kinoName As String = ShisakuComFunc.GetRelativeValue(ShisakuGlobal.ORDERSHEET_ID, _
                                                                                    ShisakuGlobal.ORDERSHEET_NAME, oneRow("KINO_ID_2"))
                        Dim kengenName As String = ShisakuComFunc.GetRelativeValue(ShisakuGlobal.KENGEN_ID, _
                                                                                    ShisakuGlobal.KENGEN_NAME, oneRow(items(2)))
                        m_spCom.SetValue(items(0), rowIndex, programName)
                        m_spCom.SetValue(items(1), rowIndex, kinoName)
                        m_spCom.SetValue(items(2), rowIndex, kengenName)
                    Else
                        Dim cmbKinoType As New ComboBoxCellType
                        cmbKinoType.Items = GetKINO(programName)
                        m_view.spdInfo_Sheet1.Cells(rowIndex, 1).CellType = cmbKinoType
                        Dim kinoName As String = oneRow("NAME2")
                        Dim kengenName As String = ShisakuComFunc.GetRelativeValue(ShisakuGlobal.KENGEN_ID, _
                                                                                    ShisakuGlobal.KENGEN_NAME, oneRow(items(2)))
                        m_spCom.SetValue(items(0), rowIndex, programName)
                        m_spCom.SetValue(items(1), rowIndex, kinoName)
                        m_spCom.SetValue(items(2), rowIndex, kengenName)
                    End If
                    rowIndex = rowIndex + 1
                End If
            Next
        Else
            '権限がないので更新ボタンを表示する。
            m_view.btnCall.Visible = False
        End If

    End Sub
#End Region

#Region "spread で　delete KeyDown"
    ''' <summary>
    ''' spread で　delete KeyDown 選択セールがブランクを設定する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub spdInfo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyValue = Keys.Delete Then

            Dim sheet As FarPoint.Win.Spread.SheetView = DirectCast(sender, FarPoint.Win.Spread.FpSpread).ActiveSheet
            If sheet.SelectionCount = 0 Then
                DirectCast(sender, FarPoint.Win.Spread.FpSpread).ActiveSheet.ActiveCell.Value = ""

            End If
            For selIndex As Integer = 0 To sheet.SelectionCount - 1
                '複数範囲選択場合
                Dim cr As Spread.Model.CellRange = sheet.GetSelection(selIndex)
                Dim startRowIdx As Integer = cr.Row
                Dim endRowIdx As Integer = cr.Row + (cr.RowCount - 1)
                Dim startColIdx As Integer = cr.Column
                Dim endColIdx As Integer = cr.Column + (cr.ColumnCount - 1)
                '列選択の場合
                If cr.Row = -1 Then
                    startRowIdx = 0
                    endRowIdx = sheet.Rows.Count - 1
                End If
                '行選択の場合
                If cr.Column = -1 Then
                    startColIdx = 0
                    endColIdx = sheet.Columns.Count - 1
                End If
                For rowIdx As Integer = startRowIdx To endRowIdx
                    For colIdx As Integer = startColIdx To endColIdx
                        If Not sheet.Cells(rowIdx, colIdx).Value = String.Empty Then
                            sheet.Cells(rowIdx, colIdx).Value = ""
                            If m_view.spdInfo_Sheet1.Columns(colIdx).Tag = items(0) Then
                                Dim program As String = m_view.spdInfo_Sheet1.Cells(rowIdx, colIdx).Value
                                Dim cmbKinoType As New ComboBoxCellType()
                                cmbKinoType.Items = GetKINO(program)
                                m_spCom.GetCell(items(1), rowIdx).CellType = cmbKinoType
                                m_view.isDataChanged = True
                            End If
                        End If
                    Next colIdx
                Next rowIdx
            Next
        End If
    End Sub
#End Region
End Class
