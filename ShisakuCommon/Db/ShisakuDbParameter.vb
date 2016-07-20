Imports System.Reflection
Imports EBom.Data

Namespace Db
    Friend Class ShisakuDbParameter
        Private Shared ReadOnly log As log4net.ILog = log4net.LogManager.GetLogger(GetType(ShisakuDbParameter))
        Private Class DbParameterVo
            Friend paramName As String
            Friend value As Object
            Friend dbtype As DbType
            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="paramName">パラメータ名</param>
            ''' <param name="value">パラメータ値</param>
            ''' <param name="dbtype">パラメータ型</param>
            ''' <remarks></remarks>
            Friend Sub New(ByVal paramName As String, ByVal value As Object, ByVal dbtype As DbType)
                Me.paramName = paramName
                Me.value = value
                Me.dbtype = dbtype
            End Sub
            Public Overrides Function ToString() As String
                Return String.Format("paramName = {0}, value = {1}, dbtype = {2}", paramName, value, dbtype.ToString)
            End Function
        End Class

        Private parameterNames As New List(Of String)
        Private parameters As New List(Of DbParameterVo)

        ''' <summary>
        ''' パラメーターを追加する
        ''' </summary>
        ''' <param name="paramName">パラメーター名</param>
        ''' <param name="value">パラメーター値</param>
        ''' <remarks></remarks>
        Public Sub Add(ByVal paramName As String, ByVal value As Object)
            Call Add(paramName, value, Nothing)
        End Sub

        ''' <summary>
        ''' パラメーターを追加する
        ''' </summary>
        ''' <param name="paramName">パラメーター名</param>
        ''' <param name="value">パラメーター値</param>
        ''' <param name="dbtype">パラメータ型</param>
        ''' <remarks></remarks>
        Public Sub Add(ByVal paramName As String, ByVal value As Object, ByVal dbtype As DbType)
            parameterNames.Add(paramName)
            parameters.Add(New DbParameterVo(paramName, value, dbtype))
        End Sub

        ''' <summary>
        ''' パラメータを追加する
        ''' </summary>
        ''' <param name="parameterObj">パラメータオブジェクト</param>
        ''' <param name="containsNull">パラメータにNull値も追加する場合、true</param>
        ''' <remarks>同名プロパティのパラメータに値を設定する</remarks>
        Public Sub AddObject(ByVal parameterObj As Object, ByVal containsNull As Boolean)
            If parameterObj Is Nothing Then
                Return
            End If
            Dim aType As Type = parameterObj.GetType
            If containsNull Then
                For Each aProperty As PropertyInfo In aType.GetProperties()
                    Add(aProperty.Name, aProperty.GetValue(parameterObj, Nothing))
                Next
            Else
                For Each aProperty As PropertyInfo In aType.GetProperties()
                    Dim value = aProperty.GetValue(parameterObj, Nothing)
                    If value IsNot Nothing Then
                        Add(aProperty.Name, value)
                    End If
                Next
            End If
        End Sub

        ''' <summary>
        ''' パラメータを追加する
        ''' </summary>
        ''' <param name="parameterObj">パラメータオブジェクト</param>
        ''' <param name="onlyFields">パラメータに含めるプロパティ情報</param>
        ''' <remarks>同名プロパティのパラメータに値を設定する</remarks>
        Public Sub AddObject(ByVal parameterObj As Object, ByVal onlyFields As List(Of PropertyInfo))
            For Each aProperty As PropertyInfo In onlyFields
                Add(aProperty.Name, aProperty.GetValue(parameterObj, Nothing))
            Next
        End Sub

        ''' <summary>
        ''' パラメータをクリアする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Clear()
            parameterNames.Clear()
            parameters.Clear()
        End Sub

        ''' <summary>パラメータの件数</summary>
        ''' <returns>パラメータの件数</returns>
        Public ReadOnly Property Count() As Integer
            Get
                Return parameters.Count
            End Get
        End Property

        ''' <summary>
        ''' パラメータ名が含まれているかを返す
        ''' </summary>
        ''' <param name="paramName">パラメータ名</param>
        ''' <returns>含まれている場合、true</returns>
        ''' <remarks></remarks>
        Public Function ContainsName(ByVal paramName As String) As Boolean
            Return parameterNames.Contains(paramName)
        End Function

        ''' <summary>
        ''' パラメータを SqlAccess に追加設定する
        ''' </summary>
        ''' <param name="db">パラメータを追加する SqlAccess</param>
        ''' <remarks></remarks>
        Public Sub AddAllTo(ByVal db As SqlAccess)
            For Each param As DbParameterVo In parameters
                If param.dbtype = Nothing Then
                    logDebug(param.paramName & ":" & param.value)
                    db.AddParameter(param.paramName, param.value)
                Else
                    logDebug(param.paramName & ":" & param.value & " - " & param.dbtype)
                    db.AddParameter(param.paramName, param.value, param.dbtype)
                End If
            Next
        End Sub

        Public Sub AddAllTo(ByVal results As List(Of String))
            For Each vo As DbParameterVo In parameters
                results.Add(vo.ToString)
            Next
        End Sub

        Public Shared Sub logDebug(ByVal message As String)
            Debug.Print(DateTime.Now.ToString("HH:mm:ss,fff") & " " & message)
        End Sub
    End Class
End Namespace
