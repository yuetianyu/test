Imports System.Reflection

Namespace Util.Grouping

    ''' <summary>
    ''' グループ化を行う
    ''' </summary>
    ''' <typeparam name="T">グループ化するVOの型</typeparam>
    ''' <remarks></remarks>
    Public Class VoGrouping(Of T)
        ''' <summary>
        ''' LocatorImplの管理を担うクラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class LocatorOwner
            Private ReadOnly rule As VoGroupingRule(Of T)
            Private ReadOnly keyProperties As Dictionary(Of Object, PropertyInfo)
            Private ReadOnly groupingInfos As New List(Of PropertyInfo)
            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="rule">グループ化のルール</param>
            ''' <remarks></remarks>
            Public Sub New(ByVal rule As VoGroupingRule(Of T))
                Me.rule = rule

                Dim aType As Type = GetType(T)
                Dim vo As T = CType(Activator.CreateInstance(aType), T)
                keyProperties = VoUtil.MarkVoAndGetKeyProperties(vo)

                rule.GroupRule(New LocatorImpl(Me), vo)
            End Sub
            ''' <summary>
            ''' LocatorImplのメッセージを貰う
            ''' </summary>
            ''' <param name="value">値</param>
            ''' <remarks></remarks>
            Friend Sub Message(ByVal value As Object)
                groupingInfos.Add(keyProperties(value))
            End Sub
            Friend Function MakeKey(ByVal vo As T) As String
                Dim results As New List(Of String)
                For Each info As PropertyInfo In groupingInfos
                    Dim value As Object = info.GetValue(vo, Nothing)
                    results.Add(IIf(value Is Nothing, "null", value))
                Next
                Return Join(results.ToArray, "_:_")
            End Function
            Friend Function MakeValue(ByVal vo As T) As T
                Dim aType As Type = GetType(T)
                Dim result As T = CType(Activator.CreateInstance(aType), T)
                For Each info As PropertyInfo In groupingInfos
                    info.SetValue(result, info.GetValue(vo, Nothing), Nothing)
                Next
                Return result
            End Function
        End Class
        ''' <summary>
        ''' グループ化する項目を指定させるクラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class LocatorImpl : Implements VoGroupingLocator
            Private ReadOnly owner As LocatorOwner
            Public Sub New(ByVal owner As LocatorOwner)
                Me.owner = owner
            End Sub
            ''' <summary>
            ''' グループ化する項目を指定する
            ''' </summary>
            ''' <param name="groupingField">グループ化する項目</param>
            ''' <returns>項目のLocatorインターフェース</returns>
            ''' <remarks></remarks>
            Public Function Prop(ByVal groupingField As Object) As VoGroupingLocator Implements VoGroupingLocator.Prop
                owner.Message(groupingField)
                Return Me
            End Function
        End Class

        Private owner As LocatorOwner
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="rule">グループ化のルール</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal rule As VoGroupingRule(Of T))
            owner = New LocatorOwner(rule)
        End Sub
        ''' <summary>
        ''' グループ化して返す
        ''' </summary>
        ''' <param name="dupulicateList">重複したList</param>
        ''' <returns>グループ化したList</returns>
        ''' <remarks></remarks>
        Public Function Grouping(ByVal dupulicateList As List(Of T)) As List(Of T)
            Dim results As New List(Of T)

            Dim indexes As New List(Of String)
            For Each vo As T In dupulicateList
                Dim key As String = owner.MakeKey(vo)
                If indexes.IndexOf(key) = -1 Then
                    indexes.Add(key)
                    results.Add(owner.MakeValue(vo))
                End If
            Next
            Return results
        End Function
    End Class
End Namespace