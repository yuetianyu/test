Imports EBom.Data
Imports EBom.Common
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Text

Namespace Db

    Public MustInherit Class ShisakuDbClient : Implements IDisposable

        'Protected Shared ReadOnly log As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        '' ロックタイムアウト(ms) (-1は無期限)
        Private _LockTimeout As Long = 10000

        Private connectString As String
        ''' <summary>DBコネクションを管理中か</summary>
        Private hasManagingDb As Boolean

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectString">コネクション文字列</param>
        ''' <remarks></remarks>
        Protected Sub New(ByVal connectString As String)
            Me.connectString = connectString
        End Sub

#Region "プロパティ"
        ''' <summary>ロックタイムアウト(ms) (-1は無期限)</summary>
        ''' <value>ロックタイムアウト(ms) (-1は無期限)</value>
        ''' <returns>ロックタイムアウト(ms) (-1は無期限)</returns>
        Public Property LockTimeout() As Long
            Get
                Return _LockTimeout
            End Get
            Set(ByVal value As Long)
                _LockTimeout = value
                If ExistDbOnThread() And hasManagingDb Then
                    InitializeDb(GetDbOnThread())
                End If
            End Set
        End Property
        ''' <summary>
        ''' トランザクション管理開始中かを返す
        ''' </summary>
        ''' <returns>開始中の場合、true</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IsBeginningTransaction() As Boolean
            Get
                If ExistDbOnThread() Then
                    Return GetDbOnThread().HasTransaction
                End If
                Return False
            End Get
        End Property
#End Region

        Private Sub InitializeDb(ByVal db As SqlAccess)
            If 0 <= _LockTimeout Then
                ExecuteSetting("SET LOCK_TIMEOUT " & CStr(_LockTimeout), db)
            End If
        End Sub

        ''' <summary>
        ''' SqlAccess を生成して返す
        ''' </summary>
        ''' <returns>新しい SqlAccess</returns>
        ''' <remarks></remarks>
        Private Function NewDbAccess() As SqlAccess
            Return NewDbAccess(False)
        End Function
        ''' <summary>
        ''' SqlAccess を生成して返す
        ''' </summary>
        ''' <param name="noInitialize">DBの初期化をしない場合、true</param>
        ''' <returns>新しい SqlAccess</returns>
        ''' <remarks></remarks>
        Private Function NewDbAccess(ByVal noInitialize As Boolean) As SqlAccess
            Dim result As SqlAccess = New SqlAccess(connectString)
            result.Open()
            If Not noInitialize Then
                InitializeDb(result)
            End If
            Return result
        End Function

        ''' <summary>
        ''' Select文を実行した結果を同名プロパティに保持した Object にして返す
        ''' </summary>
        ''' <typeparam name="T">結果の型</typeparam>
        ''' <param name="sql">SELECT文</param>
        ''' <returns>結果を同名プロパティに保持した Object</returns>
        ''' <remarks></remarks>
        Public Function QueryForObject(Of T)(ByVal sql As String) As T
            Return QueryForObject(Of T)(ReplaceDatabaseName(sql), Nothing)
        End Function
        ''' <summary>
        ''' Select文を実行した結果を同名プロパティに保持した Object にして返す
        ''' </summary>
        ''' <typeparam name="T">結果の型</typeparam>
        ''' <param name="sql">SELECT文</param>
        ''' <param name="parameter">埋め込み値をもつオブジェクト</param>
        ''' <returns>結果を同名プロパティに保持した Object</returns>
        ''' <remarks></remarks>
        Public Function QueryForObject(Of T)(ByVal sql As String, ByVal parameter As Object) As T
            Dim results As List(Of T) = QueryForList(Of T)(ReplaceDatabaseName(sql), parameter)
            If 0 < results.Count Then
                Return results(0)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Select文を実行した結果を同名プロパティに保持した Object の List にして返す
        ''' </summary>
        ''' <typeparam name="T">結果の型</typeparam>
        ''' <param name="sql">SELECT文</param>
        ''' <returns>結果を同名プロパティに保持した Object の List</returns>
        ''' <remarks></remarks>
        Public Function QueryForList(Of T)(ByVal sql As String) As List(Of T)
            Return QueryForList(Of T)(ReplaceDatabaseName(ReplaceDatabaseName(sql)), Nothing)
        End Function
        ''' <summary>
        ''' Select文を実行した結果を同名プロパティに保持した Object の List にして返す
        ''' </summary>
        ''' <typeparam name="T">結果の型</typeparam>
        ''' <param name="sql">SELECT文</param>
        ''' <param name="parameter">埋め込み値をもつオブジェクト</param>
        ''' <returns>結果を同名プロパティに保持した Object の List</returns>
        ''' <remarks></remarks>
        Public Function QueryForList(Of T)(ByVal sql As String, ByVal parameter As Object) As List(Of T)
            If ExistDbOnThread() Then
                GetDbOnThread().ClearParameters()
                Return QueryForList(Of T)(ReplaceDatabaseName(sql), parameter, GetDbOnThread())
            End If
            Using db As SqlAccess = NewDbAccess()
                Return QueryForList(Of T)(ReplaceDatabaseName(sql), parameter, db)
            End Using
        End Function
        ''' <summary>
        ''' Select文を実行した結果を同名プロパティに保持した Object の List にして返す
        ''' </summary>
        ''' <typeparam name="T">結果の型</typeparam>
        ''' <param name="sql">SELECT文</param>
        ''' <param name="parameter">埋め込み値をもつオブジェクト</param>
        ''' <param name="db">Open中の SqlAccess</param>
        ''' <returns>結果を同名プロパティに保持した Object の List</returns>
        ''' <remarks></remarks>
        Private Function QueryForList(Of T)(ByVal sql As String, ByVal parameter As Object, ByVal db As SqlAccess) As List(Of T)
            Dim helper As New DbAccessHelper(db)
            Return helper.QueryForList(Of T)(sql, parameter)
        End Function

        ''' <summary>
        ''' Insert処理を行う
        ''' </summary>
        ''' <param name="sql">Insert文</param>
        ''' <param name="parameter">埋め込み値をもつオブジェクト</param>
        ''' <returns>処理件数</returns>
        ''' <remarks></remarks>
        Public Function Insert(ByVal sql As String, Optional ByVal parameter As Object = Nothing) As Integer
            Return Execute(ReplaceDatabaseName(sql), parameter)
        End Function

        ''' <summary>
        ''' Update処理を行う
        ''' </summary>
        ''' <param name="sql">Update文</param>
        ''' <param name="parameter">埋め込み値をもつオブジェクト</param>
        ''' <returns>処理件数</returns>
        ''' <remarks></remarks>
        Public Function Update(ByVal sql As String, Optional ByVal parameter As Object = Nothing) As Integer
            Return Execute(ReplaceDatabaseName(sql), parameter)
        End Function

        ''' <summary>
        ''' Delete処理を行う
        ''' </summary>
        ''' <param name="sql">Delete文</param>
        ''' <param name="parameter">埋め込み値をもつオブジェクト</param>
        ''' <returns>処理件数</returns>
        ''' <remarks></remarks>
        Public Function Delete(ByVal sql As String, Optional ByVal parameter As Object = Nothing) As Integer
            Return Execute(ReplaceDatabaseName(sql), parameter)
        End Function

        ''' <summary>
        ''' DB設定を反映する
        ''' </summary>
        ''' <param name="sql">DB設定</param>
        ''' <param name="db">Open中の SqlAccess</param>
        ''' <remarks></remarks>
        Private Sub ExecuteSetting(ByVal sql As String, ByVal db As SqlAccess)
            Dim helper As New DbAccessHelper(db)
            helper.ExecuteSetting(sql)
        End Sub
        ''' <summary>
        ''' SQL処理を実行する
        ''' </summary>
        ''' <param name="sql">SQL文</param>
        ''' <param name="parameter">埋め込み値をもつオブジェクト</param>
        ''' <returns>実行結果</returns>
        ''' <remarks></remarks>
        Private Function Execute(ByVal sql As String, ByVal parameter As Object) As Integer
            If ExistDbOnThread() Then
                GetDbOnThread().ClearParameters()
                Return Execute(sql, parameter, GetDbOnThread())
            End If
            Using db As SqlAccess = NewDbAccess()
                Return Execute(sql, parameter, db)
            End Using
        End Function
        ''' <summary>
        ''' SQL処理を実行する
        ''' </summary>
        ''' <param name="sql">SQL文</param>
        ''' <param name="parameter">埋め込み値をもつオブジェクト</param>
        ''' <param name="db">処理の実行先（Open中の SqlAccess）</param>
        ''' <returns>実行結果</returns>
        ''' <remarks></remarks>
        Private Function Execute(ByVal sql As String, ByVal parameter As Object, ByVal db As SqlAccess) As Integer
            Dim helper As New DbAccessHelper(db)
            Return helper.Execute(sql, parameter)
        End Function

        Private Shared txPerThread As Hashtable = Hashtable.Synchronized(New Hashtable)
        Private Sub AddDbOnThread(ByVal db As SqlAccess)
            txPerThread.Add(System.Threading.Thread.CurrentThread, db)
        End Sub
        Private Sub RemoveDbOnThread()
            txPerThread.Remove(System.Threading.Thread.CurrentThread)
        End Sub
        Private Function ExistDbOnThread() As Boolean
            Return txPerThread.ContainsKey(System.Threading.Thread.CurrentThread)
        End Function
        Private Function GetDbOnThread() As SqlAccess
            Return txPerThread(System.Threading.Thread.CurrentThread)
        End Function

        Private escapeDb As SqlAccess
        Private Sub SuspendDbOnThread()
            escapeDb = GetDbOnThread()
            RemoveDbOnThread()
        End Sub
        Private Function IsSuspend() As Boolean
            Return escapeDb IsNot Nothing
        End Function
        Private Sub ResumeDbOnThread()
            AddDbOnThread(escapeDb)
        End Sub

        ''' <summary>
        ''' トランザクション管理を開始する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub BeginTransaction()
            If ExistDbOnThread() Then
                If hasManagingDb Then
                    ShisakuDbParameter.logDebug(GetDbOnThread().ToString & " - BeginTransaction")
                    GetDbOnThread().BeginTransaction()
                    Return
                Else
                    SuspendDbOnThread()
                End If
            End If
            '' DbAccessを作成して、BeginTransactionして、poolに格納して、返す
            Dim db As SqlAccess = NewDbAccess()
            AddDbOnThread(db)
            ShisakuDbParameter.logDebug(db.ToString & " - BeginTransaction")

            '
            'db.BeginTransaction()
            db.BeginTransaction(IsolationLevel.ReadUncommitted)

            hasManagingDb = True
        End Sub
        ''' <summary>
        ''' コミットする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Commit()
            If ExistDbOnThread() And hasManagingDb Then
                ShisakuDbParameter.logDebug(GetDbOnThread().ToString & " - Commit")
                GetDbOnThread().Commit()
            End If
        End Sub
        ''' <summary>
        ''' ロールバックする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Rollback()
            If ExistDbOnThread() And hasManagingDb Then
                ShisakuDbParameter.logDebug(GetDbOnThread().ToString & " - Rollback")
                GetDbOnThread().Rollback()
            End If
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Try
                If ExistDbOnThread() And hasManagingDb Then
                    Rollback()
                    Dim db As SqlAccess = GetDbOnThread()
                    RemoveDbOnThread()
                    ShisakuDbParameter.logDebug(db.ToString & " - Closing")
                    db.Close()
                End If
                GC.SuppressFinalize(Me)
            Finally
                If IsSuspend() Then
                    ResumeDbOnThread()
                End If
            End Try
        End Sub
        Private Function ReplaceDatabaseName(ByVal sql As String) As String
            Dim result As New StringBuilder(sql)
            With result
                .Replace("EBOM.dbo.", ComFunc.GetDatabaseName(g_kanrihyoIni("KOSEI_DB")) & ".dbo.")
                .Replace("RHACLIBF.dbo.", ComFunc.GetDatabaseName(g_kanrihyoIni("EBOM_DB")) & ".dbo.")
                .Replace("mBOM.dbo.", ComFunc.GetDatabaseName(g_kanrihyoIni("mBOM_DB")) & ".dbo.")
            End With
            Return result.ToString
        End Function
    End Class

End Namespace
