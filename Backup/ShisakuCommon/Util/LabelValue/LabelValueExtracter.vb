Imports System.Reflection

Namespace Util.LabelValue
    ''' <summary>
    ''' 抽出メソッド
    ''' </summary>
    ''' <param name="aLocator">VOの Label と Value を管理するインターフェース</param>
    ''' <remarks></remarks>
    Public Delegate Sub LabelValueExtraction(ByVal aLocator As LabelValueLocator)

    ''' <summary>
    ''' List(Of Vo)からLabelとValueを抽出するクラス
    ''' </summary>
    ''' <typeparam name="T">Voの型</typeparam>
    ''' <remarks></remarks>
    Public Class LabelValueExtracter(Of T)

        Private ReadOnly _aList As List(Of T)
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="aList">抽出もととなる List</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal aList As List(Of T))
            _aList = aList
        End Sub

        ''' <summary>
        ''' Label と Value を抽出して返す
        ''' </summary>
        ''' <param name="extraction">抽出メソッド（のアドレス）</param>
        ''' <returns>抽出した Label と Value の List</returns>
        ''' <remarks></remarks>
        Public Function Extract(ByVal extraction As LabelValueExtraction) As List(Of LabelValueVo)
            Dim results As New List(Of LabelValueVo)
            Dim aLocator As New LocatorImpl
            extraction(aLocator)
            If aLocator.labelProperty Is Nothing Then
                Throw New ArgumentNullException("LabelValueExtraction でラベル項目が設定されていない")
            End If
            If aLocator.valueProperty Is Nothing Then
                Throw New ArgumentNullException("LabelValueExtraction で値項目が設定されていない")
            End If
            For Each vo As T In _aList
                Dim result As New LabelValueVo
                results.Add(result)
                result.Label = aLocator.labelProperty.GetValue(vo, Nothing)
                result.Value = aLocator.valueProperty.GetValue(vo, Nothing)
            Next
            Return ExcludeDuplicate(results)
        End Function

        ''' <summary>
        ''' Label と Value を抽出して返す
        ''' </summary>
        ''' <param name="extraction">抽出メソッドを実装したインスタンス</param>
        ''' <returns>抽出した Label と Value の List</returns>
        ''' <remarks></remarks>
        Public Function Extract(ByVal extraction As ILabelValueExtraction) As List(Of LabelValueVo)
            Return Extract(AddressOf extraction.Extraction)
        End Function

        ''' <summary>
        ''' Label と Value を抽出して返す
        ''' </summary>
        ''' <param name="aList">抽出もととなる List</param>
        ''' <param name="extraction">抽出メソッドを実装したインスタンス</param>
        ''' <returns>抽出した Label と Value の List</returns>
        ''' <remarks></remarks>
        Public Shared Function Extract(ByVal aList As List(Of T), _
                                       ByVal extraction As ILabelValueExtraction) As List(Of LabelValueVo)
            Dim extracter As New LabelValueExtracter(Of T)(aList)
            Return extracter.Extract(extraction)
        End Function

        ''' <summary>
        ''' 重複データを除外する
        ''' </summary>
        ''' <param name="LabelValues">LabelとValueをもつList</param>
        ''' <returns>重複を除外したList</returns>
        ''' <remarks></remarks>
        Private Shared Function ExcludeDuplicate(ByVal LabelValues As List(Of LabelValueVo)) As List(Of LabelValueVo)
            Dim indexes As New List(Of String)
            Dim results As New List(Of LabelValueVo)
            For Each vo As LabelValueVo In LabelValues
                Dim key As String = IIf(vo.Label Is Nothing, "null", vo.Label) & "_:_" & IIf(vo.Value Is Nothing, "null", vo.Value)
                If indexes.IndexOf(key) = -1 Then
                    indexes.Add(key)
                    results.Add(vo)
                End If
            Next
            Return results
        End Function

        Private Class LocatorImpl : Implements LabelValueLocator
            Private markingProperties As Dictionary(Of Object, PropertyInfo)
            Friend labelProperty As PropertyInfo
            Friend valueProperty As PropertyInfo
            ''' <summary>
            ''' Label と Value を設定する VO にマーキングをする
            ''' </summary>
            ''' <param name="vo">VOのインスタンス</param>
            ''' <returns>Label と Value を設定するMessengerインターフェース</returns>
            ''' <remarks></remarks>
            Public Function IsA(ByVal vo As Object) As LabelValueMessenger Implements LabelValueLocator.IsA
                markingProperties = VoUtil.MarkVoAndGetKeyProperties(vo)
                Return New MessengerImpl(Me)
            End Function
            ''' <summary>
            ''' Labelプロパティである事を通知する
            ''' </summary>
            ''' <param name="aField">Labelプロパティ</param>
            ''' <remarks></remarks>
            Friend Sub MessageLabel(ByVal aField As Object)
                AssertContainsKey(aField)
                labelProperty = markingProperties(aField)
            End Sub
            ''' <summary>
            ''' Valueプロパティである事を通知
            ''' </summary>
            ''' <param name="aField">Valueプロパティ</param>
            ''' <remarks></remarks>
            Friend Sub MessageValue(ByVal aField As Object)
                AssertContainsKey(aField)
                valueProperty = markingProperties(aField)
            End Sub
            ''' <summary>
            ''' マーキング済みのプロパティであることを保証する
            ''' </summary>
            ''' <param name="aField">検査するプロパティ</param>
            ''' <remarks></remarks>
            Private Sub AssertContainsKey(ByVal aField As Object)
                If Not markingProperties.ContainsKey(aField) Then
                    Throw New ArgumentNullException("#IsA()メソッド引数のプロパティを設定してください")
                End If
            End Sub
        End Class

        Private Class MessengerImpl : Implements LabelValueMessenger
            Private _Locator As LocatorImpl
            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="aLocator">LabelとValueのプロパティを管理するインターフェース</param>
            ''' <remarks></remarks>
            Friend Sub New(ByVal aLocator As LocatorImpl)
                _Locator = aLocator
            End Sub

            ''' <summary>
            ''' Labelプロパティである事を設定する
            ''' </summary>
            ''' <param name="labelProperty">Labelプロパティ</param>
            ''' <returns>Label と Value を設定するMessengerインターフェース</returns>
            ''' <remarks></remarks>
            Public Function Label(ByVal labelProperty As Object) As LabelValueMessenger Implements LabelValueMessenger.Label
                _Locator.MessageLabel(labelProperty)
                Return Me
            End Function

            ''' <summary>
            ''' Valueプロパティである事を設定する
            ''' </summary>
            ''' <param name="valueProperty">Valueプロパティ</param>
            ''' <returns>Label と Value を設定するMessengerインターフェース</returns>
            ''' <remarks></remarks>
            Public Function Value(ByVal valueProperty As Object) As LabelValueMessenger Implements LabelValueMessenger.Value
                _Locator.MessageValue(valueProperty)
                Return Me
            End Function
        End Class
    End Class
End Namespace