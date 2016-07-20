Namespace ShisakuBuhinEdit.Logic
    Public Class ShisakuRule

        ''' <summary>
        ''' ルールに従いINSTL品番を通常品番にして返す
        ''' </summary>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <returns>通常品番</returns>
        ''' <remarks></remarks>
        Public Shared Function Conv0532Hinban(ByVal instlHinban As String) As String

            If instlHinban Is Nothing OrElse instlHinban.Length < 12 _
                OrElse instlHinban.Length >= 13 Then
                Return instlHinban
            End If
            Return instlHinban.Substring(0, 10) & "##" & instlHinban.Substring(12)
        End Function

        ''' <summary>
        ''' 色コード置換品番かを返す
        ''' </summary>
        ''' <param name="hinban">品番</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsHinbanReplacedColor(ByVal hinban As String) As Boolean
            'Return 12 <= hinban.Length AndAlso "##".Equals(hinban.Substring(10, 2))
            '2012/02/25  自身が色を持っている場合も存在するので'
            Return 12 = hinban.Length
        End Function
        ''' <summary>
        ''' 色コードを返す
        ''' </summary>
        ''' <param name="hinban">品番</param>
        ''' <returns>色コード</returns>
        ''' <remarks></remarks>
        Public Shared Function GetColorFromHinban(ByVal hinban As String) As String
            If hinban.Length < 12 Then
                Return Nothing
            End If
            Return hinban.Substring(10, 2)
        End Function
        ''' <summary>
        ''' 品番へ色コードを付与して返す
        ''' </summary>
        ''' <param name="hinban">品番</param>
        ''' <param name="colorCode">色コード</param>
        ''' <returns>品番（##→色コード）</returns>
        ''' <remarks></remarks>
        Public Shared Function ReplaceColor(ByVal hinban As String, ByVal colorCode As String) As String
            If 12 > hinban.Length Then
                Return hinban
            ElseIf hinban.Substring(10, 2) <> "##" Then
                Return hinban
            End If
            Return hinban.Substring(0, 10) & colorCode '色コードに置き換えて返す。
        End Function
    End Class
End Namespace