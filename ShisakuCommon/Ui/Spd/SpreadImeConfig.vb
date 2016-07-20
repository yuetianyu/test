Imports FarPoint.Win.Spread.CellType
Imports FarPoint.Win.Spread

Namespace Ui.Spd


    ''' <summary>
    ''' Spreadの CellType.CharacterSet = CharacterSet.AllIME など、IMEが指定されたセルのIME制御
    ''' </summary>
    ''' <remarks>KanjiOnlyIME/AllIMEの指定だけでは、一文字目が全角文字で入力出来ない</remarks>
    Public Class SpreadImeConfig

        Private _ImeOnCharacterSets As New List(Of CharacterSet)
        Private imeOnTags As New List(Of String)
        Private imeOffTags As New List(Of String)
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="spread">Spread</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal spread As FpSpread)
            ' プロパティの初期値設定
            _ImeOnCharacterSets.Add(CharacterSet.AllIME)
            _ImeOnCharacterSets.Add(CharacterSet.KanjiOnlyIME)

            Dim anInner As New Inner(spread, _ImeOnCharacterSets, imeOnTags, imeOffTags)
            AddHandler spread.LeaveCell, AddressOf anInner.Spread_OnLeaveCell_IME
            AddHandler spread.EditModeOn, AddressOf anInner.Spread_EditModeOn_IMEoff
        End Sub

        ''' <summary>
        ''' 初期状態を IME on にする CharacterSet を返す
        ''' </summary>
        ''' <remarks>初期値は、AllIME/KanjiOnlyIME</remarks>
        Public Function GetCharacterSetsImeOn() As CharacterSet()
            Return _ImeOnCharacterSets.ToArray
        End Function
        ''' <summary>
        ''' 初期状態を IME on にしたい CharacterSet を追加する
        ''' </summary>
        ''' <param name="aCharacterSet">CharacterSet</param>
        ''' <remarks></remarks>
        Public Sub AddCharacterSetImeOn(ByVal aCharacterSet As CharacterSet)
            _ImeOnCharacterSets.Add(aCharacterSet)
        End Sub
        ''' <summary>
        ''' 初期状態を IME on にしたい CharacterSet を除去する
        ''' </summary>
        ''' <param name="aCharacterSet">CharacterSet</param>
        ''' <remarks></remarks>
        Public Sub RemoveCharacterSetImeOn(ByVal aCharacterSet As CharacterSet)
            _ImeOnCharacterSets.Remove(aCharacterSet)
        End Sub

        ''' <summary>
        ''' 初期状態を IME on にしたい列TAGを追加する
        ''' </summary>
        ''' <param name="tag">列TAG</param>
        ''' <remarks></remarks>
        Public Sub AddTagImeOn(ByVal tag As String)
            imeOnTags.Add(tag)
        End Sub
        ''' <summary>
        ''' 初期状態を IME on にしたい列TAGを追加する
        ''' </summary>
        ''' <param name="tags">列TAG</param>
        ''' <remarks></remarks>
        Public Sub AddTagsImeOn(ByVal ParamArray tags As String())
            imeOnTags.AddRange(tags)
        End Sub
        ''' <summary>
        ''' AllIME だけど初期状態は IME off にしたい列TAGを追加する
        ''' </summary>
        ''' <param name="tag">列TAG</param>
        ''' <remarks></remarks>
        Public Sub AddTagAllImeOff(ByVal tag As String)
            imeOffTags.Add(tag)
        End Sub
        ''' <summary>
        ''' AllIME だけど初期状態は IME off にしたい列TAGを追加する
        ''' </summary>
        ''' <param name="tags">列TAG</param>
        ''' <remarks></remarks>
        Public Sub AddTagsAllImeOff(ByVal ParamArray tags As String())
            imeOffTags.AddRange(tags)
        End Sub
        Private Class Inner
            Private spread As FpSpread
            Private sheet As SheetView
            Private ImeOnCharacterSets As List(Of CharacterSet)
            Private imeOnTags As List(Of String)
            Private imeOffTags As List(Of String)
            Public Sub New(ByVal spread As FpSpread, ByVal ImeOnCharacterSets As List(Of CharacterSet), ByVal imeOnTags As List(Of String), ByVal imeOffTags As List(Of String))
                Me.spread = spread
                If spread.Sheets.Count = 0 Then
                    Throw New ArgumentException
                End If
                Me.sheet = spread.Sheets(0)
                Me.ImeOnCharacterSets = ImeOnCharacterSets
                Me.imeOnTags = imeOnTags
                Me.imeOffTags = imeOffTags
            End Sub
            ''' <summary>
            ''' Spread.LeaveCellEventHandler IME制御
            ''' </summary>
            ''' <param name="sender">LeaveCellEventHandlerに従う</param>
            ''' <param name="e">LeaveCellEventHandlerに従う</param>
            ''' <remarks></remarks>
            Public Sub Spread_OnLeaveCell_IME(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)
                If IsCellImeOn(sheet.GetStyleInfo(e.NewRow, e.NewColumn), Convert.ToString(sheet.Columns(e.NewColumn).Tag)) Then
                    spread.ImeMode = Windows.Forms.ImeMode.Hiragana
                Else
                    spread.ImeMode = Windows.Forms.ImeMode.Disable
                End If
            End Sub

            Private Function IsCellImeOn(ByVal aStyleInfo As StyleInfo, ByVal tag As String) As Boolean
                If aStyleInfo.Locked Then
                    Return False
                End If
                If 0 <= imeOffTags.IndexOf(tag) Then
                    Return False
                End If
                If 0 <= imeOnTags.IndexOf(tag) Then
                    Return True
                End If
                If TypeOf aStyleInfo.CellType Is TextCellType Then
                    Dim aTextCellType As TextCellType = DirectCast(aStyleInfo.CellType, TextCellType)
                    If 0 <= ImeOnCharacterSets.IndexOf(aTextCellType.CharacterSet) Then
                        Return True
                    End If
                End If
                Return False
            End Function

            ''' <summary>
            ''' spread.EditModeOn IME制御
            ''' </summary>
            ''' <param name="sender">EditModeOnに従う</param>
            ''' <param name="e">EditModeOnに従う</param>
            ''' <remarks></remarks>
            Public Sub Spread_EditModeOn_IMEoff(ByVal sender As System.Object, ByVal e As System.EventArgs)
                If 0 <= imeOffTags.IndexOf(Convert.ToString(sheet.ActiveColumn.Tag)) Then
                    spread.EditingControl.ImeMode = ImeMode.Off
                End If
            End Sub
        End Class
    End Class
End Namespace