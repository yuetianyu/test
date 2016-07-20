Imports EBom.Common.mdlConstraint
Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports EBom.Data
Imports ShisakuCommon
Imports System.Text
Imports System.Xml
Imports System.Collections.Generic
Imports System.Collections
Imports System.IO


Public Class LoginMaster02

#Region " ログインマスタ初期化 "
    ''' <summary>
    ''' ログインマスタ初期化
    ''' </summary>
    ''' <param name="userId">ユーザID或いは社員番号</param>
    ''' <returns>
    ''' M_SHISAKU_LOGIN、RHAC0650、RHAC2130、RHAC0660にユーザIDによって関係有るレコード
    ''' </returns>
    ''' <remarks></remarks>
    Public Function getById(ByVal userId As String) As DataRow

        Dim dtResData As New DataTable()
        Using db As New SqlAccess(g_kanrihyoIni("EBOM_DB"))
            db.Open()

            If Not userId = String.Empty Then
                db.AddParameter("@SEKKEI_SHAIN_NO", userId, DbType.AnsiString)
            Else : Return Nothing
            End If
            db.Fill(DataSqlCommon.GetLoginMastUpdateInitlSql(), dtResData)
            If dtResData.Rows.Count > 0 Then
                Dim dataRow As DataRow = dtResData.Rows(0)
                Return dataRow
            Else
                Return Nothing
            End If
        End Using
    End Function

#End Region

#Region " ログインマスタ初期化2 "
    ''' <summary>
    ''' ログインマスタ初期化2
    ''' </summary>
    ''' <param name="userId">ユーザID或いは社員番号</param>
    ''' <returns>M_SHISAKU_LOGINにユーザIDによって関係有るレコード</returns>
    ''' <remarks></remarks>
    Public Function getLoginMasterById(ByVal userId As String) As DataRow

        Dim dtResData As New DataTable()
        Using db As New SqlAccess(g_kanrihyoIni("EBOM_DB"))
            db.Open()
            db.AddParameter("@SEKKEI_SHAIN_NO", userId, DbType.AnsiString)
            Try
                db.Fill(DataSqlCommon.GetLoginSql(), dtResData)
                If dtResData.Rows.Count > 0 Then
                    Dim dataRow As DataRow = dtResData.Rows(0)
                    Return dataRow
                Else : Return Nothing
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region " パスワード初期化 "
    ''' <summary>
    ''' ユーザIDによってログインマストの更新画面にpsdのtextBoxの初期値を付けます
    ''' </summary>
    ''' <param name="userId">userId、ユーザID</param>
    ''' <param name="curForm">curForm、ログインマストの更新画面</param>
    ''' <remarks></remarks>
    Public Sub setPsd(ByVal userId As String, ByVal curForm As frm5LoginMaster02)

        Dim login = getLoginMasterById(userId)
        If Not login Is Nothing Then
            curForm.lblPsd.Text = login("PASSWORD")
            curForm.lblPsdKakunin.Text = login("PASSWORD")
        End If
    End Sub

#End Region

#Region " 設定の選択項目 "
    Public Function getSetteiName(ByVal setteiId As String) As String
        Select Case setteiId
            Case "0"
                Return "なし"
            Case "1"
                Return "あり"
        End Select
        Return Nothing
    End Function

#End Region

#Region " 設定の選択項目反面 "
    Public Function getSetteiId(ByVal setteiName As String) As String
        Select Case setteiName
            Case "なし"
                Return "0"
            Case "あり"
                Return "1"
        End Select
        Return Nothing
    End Function

#End Region

#Region " ログインマスタ存在チェック "
    ''' <summary>
    '''  ログインマスタ存在チェック 
    ''' </summary>
    ''' <param name="userId">ユーザID或いは社員番号</param>
    ''' <returns>
    ''' データ処理順調、レコードが有るなら、RESULT.OK
    ''' データ取り込みエラーの場合はRESULT.NG
    ''' 他の場合はRESULT.NO_DATA
    ''' </returns>
    ''' <remarks></remarks>
    Public Function isExist(ByVal userId As String) As RESULT

        Dim dtResData As New DataTable()
        Using db As New SqlAccess(g_kanrihyoIni("EBOM_DB"))
            db.Open()

            If Not userId = String.Empty Then
                db.AddParameter("@SEKKEI_SHAIN_NO", userId, DbType.AnsiString)
            Else : Return RESULT.NG
            End If
            db.Fill(DataSqlCommon.GetLoginMastUpdateInitlSql(), dtResData)
            If dtResData.Rows.Count > 0 Then
                Dim dataRow As DataRow = dtResData.Rows(0)
                Return RESULT.OK
            Else
                Return RESULT.NO_DATA
            End If
        End Using
    End Function

#End Region

#Region " ログインマスタUpdate "
    ''' <summary>
    ''' ログインマスタUpdate
    ''' </summary>
    ''' <param name="userId">ユーザID</param>
    ''' <param name="psd">パスワード</param>
    ''' <param name="menuSet">メニュー設定</param>
    ''' <param name="kengenSet">権限設定</param>
    ''' <returns>
    ''' DBに順調に更新なら、RESULT.OK
    ''' </returns>
    ''' <remarks></remarks>
    Public Function update(ByVal userId As String, ByVal psd As String, ByVal menuSet As String, ByVal kengenSet As String) As RESULT
        Dim updatedDateTime As New ShisakuDate
        Dim updatedDate = updatedDateTime.CurrentDateDbFormat
        Dim updatedTime = updatedDateTime.CurrentTimeDbFormat

        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            db.BeginTransaction()

            db.AddParameter("@SEKKEI_SHAIN_NO", userId, DbType.AnsiString)
            db.AddParameter("@PASSWORD", psd, DbType.AnsiString)
            db.AddParameter("@SHISAKU_MENU_SETTEI", menuSet, DbType.AnsiString)
            db.AddParameter("@KENGEN_SETTEI", kengenSet, DbType.AnsiString)
            db.AddParameter("@UPDATED_USER_ID", LoginInfo.Now.UserId, DbType.AnsiString)
            db.AddParameter("@UPDATED_DATE", updatedDate, DbType.AnsiString)
            db.AddParameter("@UPDATED_TIME", updatedTime, DbType.AnsiString)

            db.ExecuteNonQuery(DataSqlCommon.GetLoginMastUpdateSql())
            db.Commit()
            Return RESULT.OK
        End Using
    End Function

#End Region

#Region " ログインマスタAdd "
    ''' <summary>
    ''' ログインマスタAdd
    ''' </summary>
    ''' <param name="userId">ユーザID</param>
    ''' <param name="psd">パスワード</param>
    ''' <param name="userKbn">ユーザ区分</param>
    ''' <param name="menuSet">メニュー設定</param>
    ''' <param name="kengenSet">権限設定</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal userId As String, ByVal psd As String, ByVal userKbn As String, ByVal menuSet As String, ByVal kengenSet As String) As RESULT
        Dim updatedDateTime As New ShisakuDate
        Dim updatedDate = updatedDateTime.CurrentDateDbFormat
        Dim updatedTime = updatedDateTime.CurrentTimeDbFormat

        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            db.BeginTransaction()

            db.AddParameter("@SEKKEI_SHAIN_NO", userId, DbType.AnsiString)
            db.AddParameter("@PASSWORD", psd, DbType.AnsiString)
            db.AddParameter("@USER_KBN", userKbn, DbType.AnsiString)
            db.AddParameter("@SHISAKU_MENU_SETTEI", menuSet, DbType.AnsiString)
            db.AddParameter("@KENGEN_SETTEI", kengenSet, DbType.AnsiString)
            db.AddParameter("@CREATED_USER_ID", LoginInfo.Now.UserId, DbType.AnsiString)
            db.AddParameter("@CREATED_DATE", updatedDate, DbType.AnsiString)
            db.AddParameter("@CREATED_TIME", updatedTime, DbType.AnsiString)
            db.AddParameter("@UPDATED_USER_ID", LoginInfo.Now.UserId, DbType.AnsiString)
            db.AddParameter("@UPDATED_DATE", updatedDate, DbType.AnsiString)
            db.AddParameter("@UPDATED_TIME", updatedTime, DbType.AnsiString)

            db.ExecuteNonQuery(DataSqlCommon.GetLoginMastInsertSql())
            db.Commit()
            Return RESULT.OK
        End Using
    End Function

#End Region

End Class
