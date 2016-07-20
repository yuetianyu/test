
Imports System.Reflection

Namespace Db.Impl
    ''' <summary>
    ''' 1テーブルのためのCRUDを提供するDAO
    ''' </summary>
    ''' <typeparam name="T">テーブルに対応するVO</typeparam>
    ''' <remarks>実装クラスは、[テーブル名]＆"Dao" という命名規則に従う事</remarks>
    Public MustInherit Class DaoEachTableImpl(Of T) : Implements DaoEachTable(Of T)

        Private ForUpdate As Boolean
        ''' <summary>
        ''' 新しい ShisakuDbClient のインスタンスを生成して返す
        ''' </summary>
        ''' <returns>新しい ShisakuDbClient のインスタンス</returns>
        ''' <remarks></remarks>
        Protected MustOverride Function NewDbClient() As ShisakuDbClient

        Private Shared Sub AssertParameterIsNotNull(ByVal parameter As Object, ByVal parameterName As String)
            If parameter Is Nothing Then
                Throw New ArgumentException("No specified " & parameterName & ".")
            End If
        End Sub

        Protected Overridable Function GetTableName() As String
            Dim suffixLength As Integer
            If Me.GetType.Name.EndsWith("Dao") Then
                suffixLength = 3
            ElseIf Me.GetType.Name.EndsWith("DaoImpl") Then
                suffixLength = 7
            Else
                Throw New NotSupportedException(Me.GetType.Name & " から、テーブル名を導出できません.")
            End If
            Return StringUtil.DecamelizeIgnoreNumber(Left(Me.GetType.Name, Len(Me.GetType.Name) - suffixLength))
        End Function

        ''' <summary>
        ''' テーブル値を全件取得する
        ''' </summary>
        ''' <returns>結果を要素の同名プロパティに保持したList</returns>
        ''' <remarks></remarks>
        Public Function FindByAll() As List(Of T) Implements DaoEachTable(Of T).FindByAll
            Return FindBy(Nothing)
        End Function

        Protected Function FindByPkMain(ByVal ParamArray values() As Object) As T
            Dim pkClause As T = MakePkVo(values)
            Dim results As List(Of T) = FindByMain(pkClause, GetPkPropertyInfos())
            If results.Count = 0 Then
                Return Nothing
            End If
            Return results(0)
        End Function

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="clause">検索条件</param>
        ''' <returns>結果を要素の同名プロパティに保持したList</returns>
        ''' <remarks></remarks>
        Public Function FindBy(ByVal clause As T) As List(Of T) Implements DaoEachTable(Of T).FindBy
            Return FindByMain(clause, Nothing)
        End Function

        Private Function FindByMain(ByVal clause As T, ByVal onlyFields As List(Of PropertyInfo)) As List(Of T)
            Dim db As ShisakuDbClient = NewDbClient()
            Dim WhereClause As String
            If onlyFields Is Nothing Then
                WhereClause = SqlUtil.MakeWhereClause(clause)
            Else
                WhereClause = SqlUtil.MakeWhereClauseOnly(clause, onlyFields)
            End If
            If ForUpdate And Not db.IsBeginningTransaction Then
                Throw New InvalidOperationException("ForUpdateを実行する時は、トランザクション管理を開始して下さい.")
            End If
            Dim sql As String = "SELECT * FROM " & GetDBTableName() _
                    & IIf(ForUpdate, " WITH (UPDLOCK,NOWAIT)", "") & WhereClause
            Return db.QueryForList(Of T)(sql, clause)
        End Function

        ''' <summary>
        ''' 該当する件数を返す
        ''' </summary>
        ''' <param name="clause">検索条件</param>
        ''' <returns>件数</returns>
        ''' <remarks></remarks>
        Public Function CountBy(ByVal clause As T) As Integer Implements DaoEachTable(Of T).CountBy
            Dim db As ShisakuDbClient = NewDbClient()
            Dim WhereClause As String = SqlUtil.MakeWhereClause(clause)
            Dim sql As String = "SELECT COUNT(*) FROM " & GetDBTableName() & WhereClause
            Return db.QueryForObject(Of Integer)(sql, clause)
        End Function

        ''' <summary>
        ''' レコードを追加する
        ''' </summary>
        ''' <param name="value">追加する値</param>
        ''' <returns>追加した件数</returns>
        ''' <remarks></remarks>
        Public Function InsertBy(ByVal value As T) As Integer Implements DaoEachTable(Of T).InsertBy
            AssertParameterIsNotNull(value, "value")
            Dim db As ShisakuDbClient = NewDbClient()
            Dim InsertValues As String = SqlUtil.MakeInsertInto(value)
            Dim sql As String = "INSERT INTO   " & GetDBTableName() & " " & " WITH (UPDLOCK) " & InsertValues
            Return db.Insert(sql, value)
        End Function

        ''' <summary>
        ''' 該当レコードを更新する
        ''' </summary>
        ''' <param name="clauseValue">検索条件（PK項目）と、更新値（その他項目）</param>
        ''' <returns>更新件数</returns>
        ''' <remarks></remarks>
        Public Function UpdateByPk(ByVal clauseValue As T) As Integer Implements DaoEachTable(Of T).UpdateByPk
            AssertParameterIsNotNull(clauseValue, "clauseValue")
            Dim db As ShisakuDbClient = NewDbClient()
            Dim sql As String = "UPDATE " & GetDBTableName() & " SET " & SqlUtil.MakeUpdateSetWhere(clauseValue, GetPkPropertyInfos())
            Return db.Update(sql, clauseValue)
        End Function

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="clause">検索条件</param>
        ''' <returns>結果を要素の同名プロパティに保持したList</returns>
        ''' <remarks></remarks>
        Public Function DeleteBy(ByVal clause As T) As Integer Implements DaoEachTable(Of T).DeleteBy
            Return DeleteByMain(clause, Nothing)
        End Function

        Protected Function DeleteByPkMain(ByVal ParamArray values() As Object) As Integer
            Dim pkClause As T = MakePkVo(values)
            Return DeleteByMain(pkClause, GetPkPropertyInfos())
        End Function

        Private Function DeleteByMain(ByVal clause As T, ByVal onlyFields As List(Of PropertyInfo)) As Integer
            AssertParameterIsNotNull(clause, "clause")
            Dim db As ShisakuDbClient = NewDbClient()
            Dim WhereClause As String
            If onlyFields Is Nothing Then
                WhereClause = SqlUtil.MakeWhereClause(clause)
            Else
                WhereClause = SqlUtil.MakeWhereClauseOnly(clause, onlyFields)
            End If
            Dim sql As String = "DELETE FROM " & GetDBTableName() & WhereClause
            Return db.Delete(sql, clause)
        End Function

        ''' <summary>
        ''' PrimaryKey設定(テーブル)インターフェース
        ''' </summary>
        ''' <typeparam name="E"></typeparam>
        ''' <remarks></remarks>
        Protected Interface PkTable(Of E)
            ''' <summary>
            ''' テーブルVOを宣言する
            ''' </summary>
            ''' <param name="aTableVo">テーブルVO</param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Function IsA(ByVal aTableVo As E) As PkInfoField
        End Interface
        ''' <summary>
        ''' PrimaryKey設定(フィールド)インターフェース
        ''' </summary>
        ''' <remarks></remarks>
        Protected Interface PkInfoField
            ''' <summary>
            ''' PrimaryKeyのプロパティを宣言する
            ''' </summary>
            ''' <param name="aField">PrimaryKeyに設定するプロパティ</param>
            ''' <returns>PrimaryKey設定インターフェース</returns>
            ''' <remarks></remarks>
            Function PkField(ByVal aField As Object) As PkInfoField
        End Interface
        Private Class PkTableImpl : Implements PkTable(Of T)
            Private map As New Dictionary(Of Object, PropertyInfo)
            Public pkFields As New List(Of PropertyInfo)
            Public Function IsA(ByVal aTableVo As T) As PkInfoField Implements PkTable(Of T).IsA
                map = VoUtil.MarkVoAndGetKeyProperties(aTableVo)
                Return New PkInfoFieldImpl(Me)
            End Function
            Friend Sub Message(ByVal aField As Object)
                pkFields.Add(map(aField))
            End Sub
        End Class
        Private Class PkInfoFieldImpl : Implements PkInfoField
            Private _table As PkTableImpl
            Public Sub New(ByVal _table As PkTableImpl)
                Me._table = _table
            End Sub
            Public Function PkField(ByVal aField As Object) As PkInfoField Implements PkInfoField.PkField
                _table.Message(aField)
                Return Me
            End Function
        End Class

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected MustOverride Sub SettingPkField(ByVal table As PkTable(Of T))

        Private _pkFieldNames As List(Of PropertyInfo)
        Private Function GetPkPropertyInfos() As List(Of PropertyInfo)
            If _pkFieldNames Is Nothing Then
                Dim info As New PkTableImpl
                SettingPkField(info)
                _pkFieldNames = info.pkFields
            End If
            Return _pkFieldNames
        End Function

        Public Function MakePkVo(ByVal ParamArray values() As Object) As T Implements DaoEachTable(Of T).MakePkVo
            If GetPkPropertyInfos.Count <> values.Length Then
                Throw New ArgumentException("PKの数が一致しません.")
            End If

            Dim aType As Type = GetType(T)
            Dim result As T = CType(Activator.CreateInstance(aType), T)
            For i As Integer = 0 To values.Length - 1
                GetPkPropertyInfos.Item(i).SetValue(result, values(i), Nothing)
            Next
            Return result
        End Function

        Public Sub SetForUpdate(ByVal ForUpdate As Boolean) Implements DaoEachTable(Of T).SetForUpdate
            Me.ForUpdate = ForUpdate
        End Sub

        ''' <summary>
        ''' テーブル名の取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetDBTableName() As String
            '各テーブル名によって前につくDB名が変更される'
            'RHACLIBF・・・EBOM'
            'T_SHISAKU・・・mBOM'
            'T_HOYOU・・・mBOM'
            'AS_・・・mBOM'
            'M_・・・mBOM'
            'M_USER・・・EBOM'
            'M_AUTHORITY_USER・・・EBOM'
            'M_AUTHORITY・・・EBOM'
            'RHAC・・・RHACLIBF'
            'T_SEISAKU・・・mBOM'

            Dim TableName As String = GetTableName()
            Dim NewTableName As String = ""

            If TableName.IndexOf("T_HOYOU") < 0 Then
                If TableName.IndexOf("T_YOSAN") < 0 Then
                    If TableName.IndexOf("T_SHISAKU") < 0 Then
                        If TableName.IndexOf("AS_") < 0 Then
                            If TableName.IndexOf("M_USER") < 0 Then
                                If TableName.IndexOf("M_AUTHORITY") < 0 Then
                                    If TableName.IndexOf("M_") < 0 Then
                                        If TableName.IndexOf("RHAC") < 0 Then
                                            If TableName.IndexOf("T_SEISAKU") < 0 Then
                                                'その他は不明'
                                                NewTableName = TableName
                                            Else
                                                'T_SEISAKU'
                                                NewTableName = MBOM_DB_NAME + ".dbo." + TableName
                                            End If
                                        Else
                                            NewTableName = RHACLIBF_DB_NAME + ".dbo." + TableName
                                        End If
                                    Else
                                        NewTableName = MBOM_DB_NAME + ".dbo." + TableName
                                    End If
                                Else
                                    'M_AUTHORITYかM_AUTHORITY_USER'
                                    NewTableName = EBOM_DB_NAME + ".dbo." + TableName
                                End If
                            Else
                                'M_USER'
                                NewTableName = EBOM_DB_NAME + ".dbo." + TableName
                            End If
                        Else
                            'AS_'
                            NewTableName = MBOM_DB_NAME + ".dbo." + TableName
                        End If
                    Else
                        'T_SHISAKU'
                        NewTableName = MBOM_DB_NAME + ".dbo." + TableName
                    End If
                Else
                    'T_YOSAN
                    NewTableName = MBOM_DB_NAME + ".dbo." + TableName
                End If
            Else
                'T_HOYOU'
                NewTableName = MBOM_DB_NAME + ".dbo." + TableName

            End If

                Return NewTableName
        End Function

    End Class
End Namespace