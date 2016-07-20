Imports EBom.Common
Imports FarPoint.Win

Namespace Ui.Spd


    ''' <summary>
    ''' FpSpread 共通メソッド
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SpreadCommon

#Region " デリゲート宣言 "
        ''' <summary>
        ''' 変更イベントデリゲート
        ''' </summary>
        ''' <param name="rowIdx"></param>
        ''' <param name="colIdx"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Delegate Function OnChangeDelegate(ByVal rowIdx As Integer, ByVal colIdx As Integer) As RESULT
#End Region

#Region " 列挙型定義 "
        ''' <summary>Enterキー押下後のアクティブセル移動方向</summary>
        Public Enum MOVE_ENTER_DIRECTION
            ''' <summary>フォーカスを移動しません.</summary>
            NONE = 0

            ''' <summary>フォーカスをシートの同じ行の次の列のセルに移動します.</summary>
            NEXT_COLUMN = 1

            ''' <summary>フォーカスをシートの同じ行の次の列のセルに移動します.行の終わりに達したら, 折り返して次の行の先頭のセルに移動します.</summary>
            NEXT_COLUMN_WARP = 2

            ''' <summary>フォーカスをシートの同じ列の次ページの行のセルに移動します.</summary>
            NEXT_ROW = 3

            ''' <summary>フォーカスをシートの同じ列の次の行のセルに移動します.列の終わりに達したら, 折り返して次の列の先頭のセルに移動します.</summary>
            NEXT_ROW_WARP = 4

            ''' <summary>フォーカスをシートの同じ行の前の列のセルに移動します.</summary>
            PREVIOUS_COLUMN = 5

            ''' <summary>フォーカスをシートの同じ行の前の列のセルに移動します.行の始めに達したら, 折り返して前の行の一番右のセルに移動します.</summary>
            PREVIOUS_COLUMN_WARP = 6

            ''' <summary>フォーカスをシートの同じ列の前ページの行のセルに移動します.</summary>
            PREVIOUS_ROW = 7

            ''' <summary>フォーカスをシートの同じ列の前の行のセルに移動します。列の始めに達したら, 折り返して前の列の一番下のセルに移動します.</summary>
            PREVIOUS_ROW_WARP = 8
        End Enum
#End Region

#Region " メンバー変数 "
        ''' <summary>スプレッドコントロール</summary>
        Private m_sp As Spread.FpSpread

        ''' <summary>変更イベントデリゲート</summary>
        Private m_onChangeDelegate As OnChangeDelegate
#End Region

#Region " コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="spread"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal spread As Spread.FpSpread, Optional ByVal changeDelegate As OnChangeDelegate = Nothing)
            m_sp = spread
            m_onChangeDelegate = changeDelegate
        End Sub
#End Region

#Region " 列オブジェクト取得 "
        ''' <summary>
        ''' 列タグを元に列オブジェクトを取得します.
        ''' </summary>
        ''' <param name="tag">列タグ</param>
        ''' <returns>列オブジェクト</returns>
        ''' <remarks></remarks>
        Public Function GetColFromTag(ByVal tag As String) As Spread.Column
            Dim sheet As Spread.SheetView = m_sp.ActiveSheet
            Return sheet.GetColumnFromTag(Nothing, tag)
        End Function
#End Region

#Region " 列インデックス取得 "
        ''' <summary>
        ''' 列タグを元に列インデックスを取得します.
        ''' </summary>
        ''' <param name="tag">列タグ</param>
        ''' <returns>列インデックス</returns>
        ''' <remarks></remarks>
        Public Function GetColIdxFromTag(ByVal tag As String) As Integer
            Dim sheet As Spread.SheetView = m_sp.ActiveSheet
            Dim col As Spread.Column = sheet.Columns(tag)

            If col Is Nothing Then
                Return -1
            End If

            Return col.Index
        End Function
#End Region

#Region " セルオブジェクト取得 "
        ''' <summary>
        ''' セルオブジェクト取得
        ''' </summary>
        ''' <param name="tag"></param>
        ''' <param name="rowIdx"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetCell(ByVal tag As String, ByVal rowIdx As Integer) As Spread.Cell
            Dim colIdx As Integer = Me.GetColIdxFromTag(tag)

            If colIdx > -1 Then
                Return m_sp.ActiveSheet.Cells(rowIdx, colIdx)
            End If

            Throw New ShisakuException("TAG '" & tag & "' に相当するセルは見つかりません ")
        End Function
#End Region

#Region " 値取得 "
        ''' <summary>
        ''' 列タグ, 行インデックスを元に値を取得します.
        ''' </summary>
        ''' <param name="tag">列タグ</param>
        ''' <param name="rowIdx">行インデックス</param>
        ''' <returns>値(Object)</returns>
        ''' <remarks></remarks>
        Public Function GetValue(ByVal tag As String, ByVal rowIdx As Integer) As Object
            Dim colIdx As Integer = Me.GetColIdxFromTag(tag)

            If colIdx > -1 Then
                'モデルを使うと, コンボボックスの OnCloseUp 時に値が取得できないため,
                '通常通りの取得に戻す.
                Return m_sp.ActiveSheet.Cells(rowIdx, colIdx).Value
            End If

            Return Nothing
        End Function

        ''' <summary>
        ''' 列タグ, 行インデックスを元に値を文字列で取得します.
        ''' </summary>
        ''' <param name="tag">列タグ</param>
        ''' <param name="rowIdx">行インデックス</param>
        ''' <returns>値(String)</returns>
        ''' <remarks></remarks>
        Public Function GetStr(ByVal tag As String, ByVal rowIdx As Integer) As String
            Dim ret As Object = Me.GetValue(tag, rowIdx)

            If ret Is Nothing Then
                Return String.Empty
            End If

            Return ret.ToString()
        End Function

        Public Function GetActiveCellValue() As Object
            Dim sheet As Spread.SheetView = m_sp.ActiveSheet
            Dim actColIdx As Integer = sheet.ActiveColumnIndex
            Dim actRowIdx As Integer = sheet.ActiveRowIndex

            Return m_sp.ActiveSheet.Cells(actRowIdx, actColIdx).Value
        End Function

        Public Function GetActiveCellStr() As String
            Dim ret As Object = Me.GetActiveCellValue()

            If ret Is Nothing Then
                Return String.Empty
            End If

            Return ret.ToString()
        End Function
#End Region

#Region " 値設定 "
        ''' <summary>
        ''' 列タグ, 行インデックスを元に値を設定します.
        ''' </summary>
        ''' <param name="tag">列タグ</param>
        ''' <param name="rowIdx">行インデックス</param>
        ''' <param name="value">値</param>
        ''' <remarks></remarks>
        Public Sub SetValue(ByVal tag As String, ByVal rowIdx As Integer, ByVal value As Object)
            Dim colIdx As Integer = Me.GetColIdxFromTag(tag)

            If colIdx > -1 Then
                Dim model As Spread.Model.DefaultSheetDataModel = _
                        CType(m_sp.ActiveSheet.Models.Data, Spread.Model.DefaultSheetDataModel)
                model.SetValue(rowIdx, colIdx, value)
            End If
        End Sub
#End Region

#Region " コピー・切取処理 "
        ''' <summary>
        ''' コピーまたは切取処理
        ''' </summary>
        ''' <param name="cutting">切取の場合は True</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function OnClipText(Optional ByVal cutting As Boolean = False) As RESULT
            Dim sheet As Spread.SheetView = m_sp.ActiveSheet
            Dim copyStr As New System.Text.StringBuilder()

            copyStr.Remove(0, copyStr.Length)

            'TODO: 複数範囲選択できるアプリで使用する場合を考慮する必要あり.
            Dim cr As Spread.Model.CellRange = sheet.GetSelection(0)
            Dim startRowIdx As Integer = cr.Row
            Dim endRowIdx As Integer = cr.Row + (cr.RowCount - 1)
            Dim startColIdx As Integer = cr.Column
            Dim endColIdx As Integer = cr.Column + (cr.ColumnCount - 1)

            '列選択の場合

            If cr.Row = -1 Then
                startRowIdx = 0
                endRowIdx = sheet.Rows.Count - 1
            End If

            '行選択の場合

            If cr.Column = -1 Then
                startColIdx = 0
                endColIdx = sheet.Columns.Count - 1
            End If

            '選択行ループ

            For rowIdx As Integer = startRowIdx To endRowIdx
                '非表示行はコピーしない

                If Not sheet.Rows(rowIdx).Visible Then
                    Continue For
                End If

                '選択列ループ

                For colIdx As Integer = startColIdx To endColIdx
                    '非表示列はコピーしない

                    If Not sheet.Columns(colIdx).Visible Then
                        Continue For
                    End If

                    copyStr.Append(sheet.Cells(rowIdx, colIdx).Value)

                    '切取の場合

                    If cutting Then
                        If Not sheet.Columns(colIdx).Locked Then
                            sheet.Cells(rowIdx, colIdx).Value = String.Empty

                            'TODO: 動きおかしいかも....
                            m_onChangeDelegate.Invoke(rowIdx, colIdx)
                        End If
                    End If

                    '最終列以外は TAB で区切る
                    If Not colIdx = cr.Column + (cr.ColumnCount - 1) Then
                        copyStr.Append(vbTab)
                    End If
                Next colIdx

                '行は改行で区切る
                copyStr.AppendLine("")
            Next rowIdx

            If Not copyStr.ToString() = String.Empty Then
                Clipboard.SetText(copyStr.ToString())
            End If
        End Function
#End Region

#Region " 貼付処理 "
        ''' <summary>
        ''' 貼付処理
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function OnPaste() As RESULT
            Dim sheet As Spread.SheetView = m_sp.ActiveSheet
            Dim rowIdx As Integer = 0
            Dim colIdx As Integer = 0

            Using dt As New DataTable()
                Dim clipStr As String = Clipboard.GetText()     'クリップボードの値
                Dim firstRow As Boolean = True

                '行列選択された場合は最終行は改行が含まれているので, 文字列の最終が改行の場合は取り除く.
                If clipStr.Length - 1 > 0 Then
                    If Mid(clipStr, clipStr.Length - 1, 2) = vbCrLf Then
                        clipStr = Mid(clipStr, 1, clipStr.Length - 2)
                    End If
                End If

                'クリップボードの内容を取得

                For Each rowData As String In Split(clipStr, vbCrLf)
                    Dim newRow As DataRow = Nothing
                    colIdx = 0

                    For Each colData As String In Split(rowData, vbTab)
                        If firstRow Then
                            ComFunc.AddDataTableColumn(dt, colIdx.ToString(), GetType(String), String.Empty)
                        End If

                        If colIdx = 0 Then
                            newRow = dt.NewRow()
                        End If

                        newRow(colIdx) = colData
                        colIdx += 1
                    Next

                    dt.Rows.Add(newRow)
                    firstRow = False
                Next

                'TODO: 複数範囲選択できるアプリで使用する場合を考慮する必要あり.
                Dim cr As Spread.Model.CellRange = sheet.GetSelection(0)

                If Not cr Is Nothing Then
                    '範囲選択されている場合

                    If cr.RowCount Then
                        'TODO: EXCELのような仕様にする場合, 範囲選択と貼付範囲が異なるとエラーにする.
                    End If
                End If

                rowIdx = sheet.ActiveRowIndex
                colIdx = sheet.ActiveColumnIndex

                '貼付処理

                For rowCnt As Integer = 0 To dt.Rows.Count - 1
                    '最大行数を超えた場合は処理を抜ける.
                    If sheet.Rows.Count <= rowCnt + rowIdx Then
                        Exit For
                    End If

                    '表示されていない行は飛ばす.
                    If Not sheet.Rows(rowCnt + rowIdx).Visible Then
                        Continue For
                    End If

                    For colCnt As Integer = 0 To dt.Columns.Count - 1
                        '最大列数を超えた場合は処理を抜ける.
                        If sheet.Columns.Count <= colCnt + colIdx Then
                            Exit For
                        End If

                        '表示されていない列, ロックされている列は飛ばす

                        If Not sheet.Columns(colCnt + colIdx).Visible OrElse _
                           sheet.Columns(colCnt + colIdx).Locked Then

                            Continue For
                        End If

                        '値をセット
                        Dim model As Spread.Model.DefaultSheetDataModel = _
                                CType(m_sp.ActiveSheet.Models.Data, Spread.Model.DefaultSheetDataModel)
                        model.SetValue(rowCnt + rowIdx, colCnt + colIdx, dt.Rows(rowCnt).Item(colCnt).ToString())

                        '変更イベントが登録されていれば呼び出す.
                        If Not m_onChangeDelegate Is Nothing Then
                            m_onChangeDelegate.Invoke(rowCnt + rowIdx, colCnt + colIdx)
                        End If
                    Next
                Next
            End Using
        End Function
#End Region

#Region " Enterキー押下後のセルフォーカス移動方向設定 "
        ''' <summary>
        ''' Enterキー押下後のセルフォーカス移動方向設定
        ''' </summary>
        ''' <param name="value">移動方向</param>
        ''' <remarks></remarks>
        Public Sub SetMoveEnterDirection(ByVal value As MOVE_ENTER_DIRECTION)
            Dim im As New Spread.InputMap
            Dim action As Object = Nothing

            '変換
            Select Case value
                Case MOVE_ENTER_DIRECTION.NONE
                    action = Spread.SpreadActions.None
                Case MOVE_ENTER_DIRECTION.NEXT_COLUMN
                    action = Spread.SpreadActions.MoveToNextColumn
                Case MOVE_ENTER_DIRECTION.NEXT_COLUMN_WARP
                    action = Spread.SpreadActions.MoveToNextColumnWrap
                Case MOVE_ENTER_DIRECTION.NEXT_ROW
                    action = Spread.SpreadActions.MoveToNextRow
                Case MOVE_ENTER_DIRECTION.NEXT_ROW_WARP
                    action = Spread.SpreadActions.MoveToNextRowWrap
                Case MOVE_ENTER_DIRECTION.PREVIOUS_COLUMN
                    action = Spread.SpreadActions.MoveToPreviousColumn
                Case MOVE_ENTER_DIRECTION.PREVIOUS_COLUMN_WARP
                    action = Spread.SpreadActions.MoveToPreviousColumnWrap
                Case MOVE_ENTER_DIRECTION.PREVIOUS_ROW
                    action = Spread.SpreadActions.MoveToPreviousRow
                Case MOVE_ENTER_DIRECTION.PREVIOUS_ROW_WARP
                    action = Spread.SpreadActions.MoveToPreviousRowWrap
            End Select

            If Not action Is Nothing Then
                '非編集セルでの［Enter］キーを「次行へ移動」とします

                im = m_sp.GetInputMap(Spread.InputMapMode.WhenFocused)
                im.Put(New Spread.Keystroke(Keys.Enter, Keys.None), action)

                '編集中セルでの［Enter］キーを「次行へ移動」とします

                im = m_sp.GetInputMap(Spread.InputMapMode.WhenAncestorOfFocused)
                im.Put(New Spread.Keystroke(Keys.Enter, Keys.None), action)
            End If
        End Sub
#End Region

    End Class
End Namespace