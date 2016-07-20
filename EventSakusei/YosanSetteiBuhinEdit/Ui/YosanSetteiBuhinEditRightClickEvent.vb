Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.Model
Imports EventSakusei.YosanSetteiBuhinEdit

Namespace YosanSetteiBuhinEdit.Ui
    ''' <summary>
    ''' セルを右クリックした時の動作を担うクラス
    ''' </summary>
    ''' <remarks>ShisakuSpreadUtil より利用可能</remarks>
    Friend Class YosanSetteiBuhinEditRightClickEvent
        Private ReadOnly spread As FpSpread
        Private ReadOnly sheet As SheetView
        Private ReadOnly inputSupport As YosanSetteiBuhinEditInputSupport
        'Private ReadOnly contextMenu As ContextMenuStrip
        Private _frmYosanSetteiBuhinEdit As frmDispYosanSetteiBuhinEdit

        Public Sub New(ByVal spread As FpSpread)
            Me.New(spread, Nothing, Nothing)
        End Sub

        Public Sub New(ByVal aSpread As FpSpread, ByVal afrmYosanSetteiBuhinEdit As frmDispYosanSetteiBuhinEdit, _
                                ByVal inputSupport As YosanSetteiBuhinEditInputSupport)
            Me.spread = aSpread

            _frmYosanSetteiBuhinEdit = afrmYosanSetteiBuhinEdit

            If spread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            Me.sheet = spread.Sheets(0)

            Me.inputSupport = inputSupport
        End Sub
        Public Sub Spread_CellClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)
            '開始データ行位置の取得
            Dim startRow As Integer = YosanSetteiBuhinEditLogic.GetTitleRowsIn(sheet)
            Dim selection As CellRange = sheet.GetSelection(0)

            '全く選択されていない場合は抜ける
            If selection Is Nothing Then
                CellEditMenuChange(False)
                RowEditMenuChange(False)
                Return
            End If

            If e.Button = Windows.Forms.MouseButtons.Right Then
                '右クリックされたセルをアクティブセルに設定する.
                If e.RowHeader Then
                    If e.Row >= startRow Then

                        'ショートカットメニューを行編集使用可に
                        '2012/02/01 行選択時にコピー、切り取り、貼り付け処理を可能に変更'
                        'CellEditMenuChange(False)
                        CellEditMenuChange(True)
                        RowEditMenuChange(True)

                    Else
                        'タイトル行ではメニュー使用不可に
                        CellEditMenuChange(False)
                        RowEditMenuChange(False)
                    End If

                    If sheet.GetSelections.Length = 1 Then

                        If ShisakuCommon.Ui.Spd.SpreadUtil.IsSelectedRow(selection) AndAlso selection.Row <= e.Row And e.Row <= selection.Row + selection.RowCount - 1 Then
                            Return
                        End If
                    End If

                    sheet.ClearSelection()
                    sheet.AddSelection(e.Row, -1, 1, -1)

                    Return
                ElseIf e.ColumnHeader Then

                    '列タイトルで右クリックコンテキストを外す
                    CellEditMenuChange(False)
                    RowEditMenuChange(False)

                    If sheet.GetSelections.Length = 1 Then

                        If ShisakuCommon.Ui.Spd.SpreadUtil.IsSelectedColumn(selection) _
                                AndAlso selection.Column <= e.Column And e.Column <= selection.Column + selection.ColumnCount - 1 Then
                            Return
                        End If
                    End If
                    sheet.ClearSelection()
                    sheet.AddSelection(-1, e.Column, -1, 1)
                    Return
                Else

                    If e.Row < startRow Then
                        '見出しはショートカット使用不可に
                        CellEditMenuChange(False)
                        RowEditMenuChange(False)
                    Else
                        '列選択、行選択の場合セルをクリックされても編集メニューは表示しない
                        If (selection.Column = -1 AndAlso selection.ColumnCount - 1) _
                        OrElse (selection.Row = -1 AndAlso selection.RowCount = -1) Then

                            CellEditMenuChange(False)
                            RowEditMenuChange(False)

                        Else

                            'セル編集用に切替
                            CellEditMenuChange(True)
                            RowEditMenuChange(False)
                        End If
                    End If

                    If sheet.GetSelections.Length = 1 Then
                        If selection.Row <= e.Row And e.Row <= selection.Row + selection.RowCount - 1 AndAlso selection.Column <= e.Column And e.Column <= selection.Column + selection.ColumnCount - 1 Then
                            Return
                        End If
                    End If

                End If
            End If
        End Sub

        Private Sub CellEditMenuChange(ByVal aIsEnable As Boolean)
            Dim flag As Boolean

            Dim wFilter As String = FilterCheck()

            If wFilter = "F" Then
                flag = False
            Else
                If aIsEnable = True Then
                    flag = True
                Else
                    flag = False
                End If
            End If

            _frmYosanSetteiBuhinEdit.ToolMenuCopy.Enabled = flag
            '2012/02/21 協議の結果一時封印'
            '_frmYosanSetteiBuhinEdit.ToolMenuCut.Enabled = flag
            _frmYosanSetteiBuhinEdit.ToolMenuPaste.Enabled = flag


        End Sub
        Private Sub RowEditMenuChange(ByVal aIsEnable As Boolean)
            Dim flag As Boolean
            Dim wFilter As String = FilterCheck()

            If wFilter = "F" Then
                flag = False
            Else
                If aIsEnable = True Then
                    flag = True
                Else
                    flag = False
                End If
            End If

            _frmYosanSetteiBuhinEdit.ToolMenuRowDelete.Enabled = flag
            _frmYosanSetteiBuhinEdit.ToolMenuRowInsert.Enabled = flag
        End Sub

#Region "フィルタリング中かチェックする"
        ''' <summary>
        ''' フィルタ解除
        ''' </summary>
        ''' <remarks></remarks>
        Public Function FilterCheck()

            Dim rtnFilter As String = Nothing

            '基本情報列タイトルの色をチェック
            For i As Integer = 0 To sheet.ColumnCount - 1

                '青色か？
                If sheet.Cells(0, i, 1, i).ForeColor = Color.Blue Then
                    rtnFilter = "F"
                End If

            Next

            Return rtnFilter

        End Function

#End Region
    End Class
End Namespace