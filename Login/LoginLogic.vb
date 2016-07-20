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

Public Class LoginLogic

#Region " ユーザチェック "
    ''' <summary>
    '''ユーザマスタに存在チェック' 
    ''' </summary>
    ''' <param name="userId">ユーザID或いは社員番号</param>
    ''' <returns>
    ''' 検収通過ならRESULT.OK
    ''' 検収不通過ならRESULT.NG
    ''' </returns>
    ''' <remarks></remarks>
    Public Function checkUser(ByVal userId As String) As RESULT

        Dim dtResData As New DataTable()
        Using db As New SqlAccess(g_kanrihyoIni("KOSEI_DB"))
            db.Open()

            If Not userId = String.Empty Then
                db.AddParameter("@USER_ID", userId, DbType.AnsiString)
            Else : Return RESULT.NG
            End If
            db.Fill(DataSqlCommon.GetUserMasSql(), dtResData)
            If dtResData.Rows.Count > 0 Then
                Dim dataRow As DataRow = dtResData.Rows(0)
                Return RESULT.OK
            Else
                ComFunc.ShowErrMsgBox(SYSERR_00006)
                Return RESULT.NG
            End If
        End Using
    End Function

#End Region

#Region " 権限（ユーザ－別）チェック "
    ''' <summary>
    '''権限（ユーザ－別）に存在チェック
    ''' </summary>
    ''' <param name="userId">ユーザID或いは社員番号</param>
    ''' <returns>
    ''' 機能ID1は"MENU"、権限区分が"1"なら、LOGIN_AUTHORITY.USE_SEKKEI
    ''' 機能ID1は"MENU"、権限区分が"2"なら、LOGIN_AUTHORITY.USE_SISAKU
    ''' 機能ID1は"MENU"、権限区分が"0"なら、LOGIN_AUTHORITY.USE_NODATE
    ''' 他の場合はLOGIN_AUTHORITY.USE_NG
    ''' </returns>
    ''' <remarks></remarks>
    Public Function checkAuthorityUser(ByVal userId As String) As LOGIN_AUTHORITY

        Dim dtResData As New DataTable()
        Using db As New SqlAccess(g_kanrihyoIni("KOSEI_DB"))
            db.Open()

            If Not userId = String.Empty Then
                db.AddParameter("@USER_ID", userId, DbType.AnsiString)
                db.AddParameter("@KINO_ID_1", M_AUTHORITY_USER_KINO1ID, DbType.AnsiString)
            Else : Return LOGIN_AUTHORITY.USE_NG
            End If
            db.Fill(DataSqlCommon.GetMAuthorityUserSql(), dtResData)
            If dtResData.Rows.Count > 0 Then
                Dim dataRow As DataRow = dtResData.Rows(0)
                Dim kinoId1 = dataRow("KINO_ID_1")
                Dim AUTHORITY_KBN = dataRow("AUTHORITY_KBN")

                If kinoId1.Equals(M_AUTHORITY_USER_KINO1ID) Then
                    If AUTHORITY_KBN.Equals(M_AUTHORITY_USER_KINO2ID_SEKKEI) Then
                        Return LOGIN_AUTHORITY.USE_SEKKEI
                    ElseIf AUTHORITY_KBN.Equals(M_AUTHORITY_USER_KINO2ID_SISAKU) Then
                        Return LOGIN_AUTHORITY.USE_SISAKU
                    ElseIf AUTHORITY_KBN.Equals(M_AUTHORITY_USER_KINO2ID_SISAKU_1KA) Then
                        Return LOGIN_AUTHORITY.USE_SISAKU_1KA
                    ElseIf AUTHORITY_KBN.Equals(M_AUTHORITY_USER_KINO2ID_BLANK) Then
                        Return LOGIN_AUTHORITY.USE_NODATE
                    End If
                Else
                    Return LOGIN_AUTHORITY.USE_NG
                End If
            End If
            Return LOGIN_AUTHORITY.USE_NG
        End Using
    End Function

#End Region

#Region " 権限チェック "
    ''' <summary>
    ''' 権限に存在チェック
    ''' </summary>
    ''' <param name="userKbn">ユーザID或いは社員番号</param>
    ''' <returns>
    ''' 権限は"1"、メニュー大区分は"10"、competentは"1"なら、LOGIN_AUTHORITY.USE_SEKKEI
    ''' 権限は"1"、メニュー大区分は"10"、competentは"1"なら、LOGIN_AUTHORITY.USE_SISAKU
    ''' 他の場合はLOGIN_AUTHORITY.USE_NG
    ''' </returns>
    ''' <remarks></remarks>
    Public Function checkAuthority(ByVal userKbn As String) As LOGIN_AUTHORITY

        Dim dtResData As New DataTable()
        Using db As New SqlAccess(g_kanrihyoIni("KOSEI_DB"))
            db.Open()

            If Not userKbn = String.Empty Then
                db.AddParameter("@USER_ID", userKbn, DbType.AnsiString)
                db.AddParameter("@AUTHORITY_KBN", M_AUTHORITY_AUTHORITY_KBN, DbType.AnsiString)
                db.AddParameter("@MENU_DAI_KBN", M_AUTHORITY_MENU_DAI_KBN, DbType.AnsiString)
            Else : Return LOGIN_AUTHORITY.USE_NG
            End If
            db.Fill(DataSqlCommon.GetMAuthoritySql(), dtResData)
            If dtResData.Rows.Count > 0 Then
                Dim dataRow As DataRow = dtResData.Rows(0)
                Dim competent = dataRow("competent")
                If competent = M_AUTHORITY_MENU_KBN_SEKKEI Then
                    Return LOGIN_AUTHORITY.USE_SEKKEI
                ElseIf competent = M_AUTHORITY_MENU_KBN_SISAKU Then
                    Return LOGIN_AUTHORITY.USE_SISAKU
                End If
            Else
                ComFunc.ShowErrMsgBox(SYSERR_00007, "ユーザにはシステム")
            End If
            Return LOGIN_AUTHORITY.USE_NG
        End Using
    End Function

#End Region


#Region " Loginチェック "
    ''' <summary>
    ''' Loginの判断
    ''' </summary>
    ''' <param name="userId">ユーザID或いは社員番号</param>
    ''' <returns>
    ''' ユーザチェック不通過なら、LOGIN_AUTHORITY.USE_NG
    ''' 権限（ユーザ－別）チェックは
    ''' 　　　　　LOGIN_AUTHORITY.USE_SEKKEI
    ''' 　　或いはLOGIN_AUTHORITY.USE_SISAKU
    ''' 　　或いはLOGIN_AUTHORITY.USE_NODATE
    ''' なら、権限（ユーザ－別）チェックの結果をreturn
    ''' 権限チェックは
    ''' 　　　　　LOGIN_AUTHORITY.USE_SEKKEI
    ''' 　　或いはLOGIN_AUTHORITY.USE_SISAKU
    ''' なら、権限チェックはの結果ををreturn 
    ''' </returns>
    ''' <remarks></remarks>
    Public Function checkLogin(ByVal userId As String) As LOGIN_AUTHORITY

        Dim userCheckResult = Me.checkUser(userId)
        If Not userCheckResult = RESULT.OK Then
            'ユーザマスタに存在チェックエラー場合

            Return LOGIN_AUTHORITY.USE_NG
        End If

        Dim authorityUserCheckResult = Me.checkAuthorityUser(userId)
        Dim isSekkeiAuthority As Boolean = authorityUserCheckResult.Equals(LOGIN_AUTHORITY.USE_SEKKEI)
        Dim isSisakuAuthority As Boolean = authorityUserCheckResult.Equals(LOGIN_AUTHORITY.USE_SISAKU)
        Dim isBlankAuthority As Boolean = authorityUserCheckResult.Equals(LOGIN_AUTHORITY.USE_NODATE)
        Dim isSekkeui1Ka As Boolean = authorityUserCheckResult.Equals(LOGIN_AUTHORITY.USE_SISAKU_1KA)

        If isSekkeiAuthority Or isSisakuAuthority Or isBlankAuthority Or isSekkeui1Ka Then
            Return authorityUserCheckResult
        End If
        Dim authorityCheckResult = Me.checkAuthority(userId)
        If authorityCheckResult = LOGIN_AUTHORITY.USE_SEKKEI Or authorityCheckResult = LOGIN_AUTHORITY.USE_SISAKU Or authorityCheckResult = LOGIN_AUTHORITY.USE_SISAKU_1KA Then
            Return authorityCheckResult
        End If

    End Function

#End Region

#Region "  車系/開発符号マスター権限（ユーザ－別）チェック "
    ''' <summary>
    '''  車系/開発符号マスター権限（ユーザ－別）チェック 
    ''' </summary>
    ''' <param name="userId">ユーザID或いは社員番号</param>
    ''' <returns>
    ''' APP_NOは"1000"、機能ID1は"MASTER006"、機能ID2は"OPEN"、権限区分は"1"なら、RESULT.OK
    ''' データ取り込みエラーの場合はRESULT.NG
    ''' 他の場合はRESULT.NO_DATA
    ''' </returns>
    ''' <remarks></remarks>
    Public Function checkKurumaKengen(ByVal userId As String) As RESULT

        Dim dtResData As New DataTable()
        Using db As New SqlAccess(g_kanrihyoIni("KOSEI_DB"))
            db.Open()

            If Not userId = String.Empty Then
                db.AddParameter("@USER_ID", userId, DbType.AnsiString)
                db.AddParameter("@APP_NO", M_AUTHORITY_APP_NO, DbType.AnsiString)
                db.AddParameter("@KINO_ID_1", M_AUTHORITY_KINO_ID_1, DbType.AnsiString)
                db.AddParameter("@KINO_ID_2", M_AUTHORITY_KINO_ID_2, DbType.AnsiString)
                db.AddParameter("@AUTHORITY_KBN", M_AUTHORITY_USER_KBN, DbType.AnsiString)
            Else : Return RESULT.NG
            End If
            db.Fill(DataSqlCommon.GetKurumaKengenSql(), dtResData)
            If dtResData.Rows.Count > 0 Then
                Return RESULT.OK
            End If
            Return RESULT.NO_DATA
        End Using
    End Function
#End Region


    Public Shared Sub InitLogging()
        InitLogging(Assembly.GetExecutingAssembly.GetName.Name)
    End Sub

    Public Shared Sub InitLogging(ByVal name As String)
        'ログの初期化
        g_log.LogFile = System.IO.Path.Combine(ComFunc.GetAppPath, name & "Log.txt")
        g_log.FileOutputLevel = Logger.LOG_LEVELS.INFORMATION           'INFORMATION以上をログに出力
    End Sub

#Region " 部課名称を取得する。"
    ''' <summary>
    ''' 登録されたユーザーの部課名称を取得する
    ''' </summary>
    ''' <param name="userId">ユーザーID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCurrUserInfo(ByVal userId As String) As RESULT
        Dim dtUserInfo As New DataTable
        Dim bukaCode As String = ""
        Dim siteKbn As String = ""
        Dim bukaName As String = ""
        '登録した社員の名と課略称
        LoginInfo.Now.ShainName = ""
        LoginInfo.Now.BukaName = ""
        LoginInfo.Now.BukaRyakuName = ""
        '「社員外部インターフェース（E-BOM既存テーブル）」にユーザーIDが存在する場合、部課コードを取得する。
        '上記で存在しない場合、「社員マスター情報（E-BOM既存テーブル）」にユーザーＩＤが存在する場合、部課コードを取得する。
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.AddParameter("@SHAIN_NO", userId)
            db.Fill(DataSqlCommon.Get_ShainByNo_Sql, dtUserInfo)
        End Using

        If dtUserInfo.Rows.Count > 0 Then
            LoginInfo.Now.ShainName = dtUserInfo.Rows(0)("SHAIN_NAME")
            bukaCode = dtUserInfo.Rows(0)("BUKA_CODE")
            LoginInfo.Now.BukaCode = bukaCode
            siteKbn = dtUserInfo.Rows(0)("SITE_KBN")
        End If
        If Not (bukaCode = String.Empty And bukaCode.Length < 4) Then
            '社員マスタ．部課コードの左2桁＝課マスタ．部コード
            'AND 社員マスタ．サイトコード ＝ 課マスタ．サイトコード
            'AND 社員マスタ．部課コードの右2桁 ＝ 課マスタ．課コード
            'で、課マスタ．課略称名を取得。
            '部課略称を取得する。
            Dim dtBuka As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.AddParameter("@BU_CODE", bukaCode.Substring(0, 2))
                db.AddParameter("@KA_CODE", bukaCode.Substring(bukaCode.Length - 2))
                db.AddParameter("@SITE_KBN", siteKbn)
                db.Fill(DataSqlCommon.Get_KaRyakuName_Sql(bukaCode), dtBuka)
            End Using
            If dtBuka.Rows.Count > 0 Then
                LoginInfo.Now.BukaName = dtBuka.Rows(0)("KA_NAME")
                LoginInfo.Now.BukaRyakuName = dtBuka.Rows(0)("KA_RYAKU_NAME")
            End If
        End If
    End Function
#End Region

#Region "要承認データ有無をチェックする"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns>承認権限を持つ部課コード</returns>
    ''' <remarks></remarks>
    Public Shared Function IsExistsApprovalData(ByVal userId As String) As String
        Dim dtResData As New DataTable()
        Using db As New SqlAccess(g_kanrihyoIni("KOSEI_DB"))
            db.Open()

            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("	KINO_ID_3 ")
                .AppendLine("FROM ")
                .AppendLine("	" & MBOM_DB_NAME & ".dbo.M_AUTHORITY_USER ")
                .AppendLine("WHERE ")
                .AppendLine("	USER_ID = @USER_ID ")
                .AppendLine("	AND ")
                .AppendLine("	APP_NO = @APP_NO ")
                .AppendLine("	AND ")
                .AppendLine("	KINO_ID_1 = @KINO_ID_1 ")
                .AppendLine("	AND ")
                .AppendLine("	AUTHORITY_KBN = @AUTHORITY_KBN ")
            End With

            If Not userId = String.Empty Then
                db.AddParameter("@USER_ID", userId, DbType.AnsiString)
                db.AddParameter("@APP_NO", M_AUTHORITY_APP_NO, DbType.AnsiString)
                db.AddParameter("@KINO_ID_1", M_AUTHORITY_KINO_SHONIN, DbType.AnsiString)
                db.AddParameter("@AUTHORITY_KBN", M_AUTHORITY_USER_KBN, DbType.AnsiString)
            Else : Return Nothing
            End If
            db.Fill(sql.ToString, dtResData)
            If dtResData.Rows.Count > 0 Then
                Return dtResData.Rows(0)("KINO_ID_3")
            End If
            Return Nothing
        End Using

    End Function
#End Region

#Region "試作手配管理システムのバージョンを取得する"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns>バージョン</returns>
    ''' <remarks></remarks>
    Public Shared Function IsVersion() As String
        Dim dtResData As New DataTable()
        Using db As New SqlAccess(g_kanrihyoIni("KOSEI_DB"))
            db.Open()

            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("	VERSION ")
                .AppendLine("FROM ")
                .AppendLine("	" & MBOM_DB_NAME & ".dbo.M_VERSION ")
                .AppendLine("WHERE ")
                .AppendLine("	VERSION_CODE = 'VERSION' ")
            End With

            db.Fill(sql.ToString, dtResData)
            If dtResData.Rows.Count > 0 Then
                Return dtResData.Rows(0)("VERSION")
            End If
            Return Nothing
        End Using

    End Function
#End Region


End Class
