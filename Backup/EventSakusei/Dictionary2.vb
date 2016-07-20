
'----------------　このクラスをコードの末尾に挿入して定義してください 
' Dictionary2: 2個のキーを持つ Dictionary コンテナ 
' USAGE: dim d as new Dictionary2(of キー1の型, キー2の型, 値の型)(デフォルト値) 
'  
' 使用例: 
' Dim d As New Dictionary2(Of Integer, Integer, String)("デフォルト") 
' d(0,0) = "文字列" 
' 初期化せずいきなり代入可能  
' 参考サイト：http://milkandlait.blogspot.jp/2013/06/net-2dictionary.html
<Serializable()> Public Class Dictionary2(Of TK1, TK2, TV)
    Inherits Dictionary(Of TK1, Dictionary(Of TK2, TV))
    Public DefaultValue As TV = Nothing
    ' --- 基底クラスのコンストラクタ
    Public Sub New(ByRef a1 As Dictionary(Of TK1, Dictionary(Of TK2, TV)))
        MyBase.New(a1)
    End Sub
    Public Sub New(ByRef a1 As IEqualityComparer(Of TK1))
        MyBase.New(a1)
    End Sub
    'Public Sub New(ByRef a1 As Int32)
    '   MyBase.New(a1)
    'End Sub
    Public Sub New(ByRef a1 As IDictionary(Of TK1, TV), ByRef a2 As IEqualityComparer(Of TK1))
        MyBase.New(a1, a2)
    End Sub
    Public Sub New(ByRef a1 As Int32, ByRef a2 As IEqualityComparer(Of TK1))
        MyBase.New(a1, a2)
    End Sub
    '---- シリアライズ
    Private Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, _
      ByVal context As System.Runtime.Serialization.StreamingContext)
        MyBase.New(info, context)
        DefaultValue = info.GetValue("DEFAULT", GetType(TV))
        ' 追加したメンバ
    End Sub
    Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, _
     ByVal context As System.Runtime.Serialization.StreamingContext)
        MyBase.GetObjectData(info, context)
        info.AddValue("DEFAULT", DefaultValue)
        ' 追加したメンバ
    End Sub
    ' --- 追加のコンストラクタ
    Public Sub New()
    End Sub
    Public Sub New(ByVal def As TV)
        DefaultValue = def
    End Sub
    ' --- 引数1個の Item() メソッド
    Default Shadows Property item(ByVal a1 As TK1) As Dictionary(Of TK2, TV)
        Get
            If (Not Me.ContainsKey(a1)) Then MyBase.Item(a1) = New Dictionary(Of TK2, TV)
            Return MyBase.Item(a1)
        End Get
        Set(ByVal value As Dictionary(Of TK2, TV))
            MyBase.Item(a1) = value
        End Set
    End Property
    ' --- 引数2個の Item() メソッド
    Default Shadows Property item(ByVal a1 As TK1, ByVal a2 As TK2) As TV
        <DebuggerHidden()> Get

            If (Not Me.ContainsKey(a1) AndAlso DefaultValue IsNot Nothing) Then
                MyBase.Item(a1) = New Dictionary(Of TK2, TV)
                Return DefaultValue
            End If

            If (Not MyBase.Item(a1).ContainsKey(a2) AndAlso DefaultValue IsNot Nothing) Then Return DefaultValue
            Return MyBase.Item(a1)(a2)
        End Get
        Set(ByVal Value As TV)
            If (Not Me.ContainsKey(a1)) Then MyBase.Item(a1) = New Dictionary(Of TK2, TV)
            MyBase.Item(a1)(a2) = Value
        End Set
    End Property





    Public Function ContainsKeys(ByVal a1 As TK1, ByVal a2 As TK2) As Boolean
        If (Me.ContainsKey(a1) AndAlso MyBase.Item(a1).ContainsKey(a2)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub Put(ByVal a1 As TK1, ByVal a2 As TK2, ByVal v As TV)
        If (Not Me.ContainsKey(a1)) Then
            'MyBase.Item(a1) = New Dictionary(Of TK2, TV)
            Dim dic2 As New Dictionary(Of TK2, TV)
            dic2.Add(a2, v)
            MyBase.Add(a1, dic2)


        ElseIf (Not MyBase.Item(a1).ContainsKey(a2)) Then
            'Dim dic1 As Dictionary(Of TK2, TV) = MyBase.Item(a1)
            'dic1.Add(a2, v)
            'MyBase.Item(a1) = dic1

            MyBase.Item(a1).Add(a2, v)
        Else
            MyBase.Item(a1)(a2) = v
        End If
    End Sub
End Class
