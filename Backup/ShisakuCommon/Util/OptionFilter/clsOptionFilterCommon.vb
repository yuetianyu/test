Imports FarPoint.Win
Imports ShisakuCommon.Ui.Spd

Namespace Util.OptionFilter

    ''' <summary>フィルタ条件</summary>
    Public Enum FILTER_CONDITION
        ''' <summary>未選択</summary>
        NONE = -1
        ''' <summary>と等しい</summary>
        EQUALS = 0
        ''' <summary>と等しくない</summary>
        NOT_EQUALS = 1
        ''' <summary>より大きい</summary>
        BIGGER = 2
        ''' <summary>以上</summary>
        MORE_THAN = 3
        ''' <summary>より小さい</summary>
        SMALLER = 4
        ''' <summary>以下</summary>
        LESS_THAN = 5
        ''' <summary>で始まる</summary>
        BEGIN_TO = 6
        ''' <summary>で始まらない</summary>
        NOT_BEGIN_TO = 7
        ''' <summary>で終わる</summary>
        FINISH_TO = 8
        ''' <summary>で終わらない</summary>
        NOT_FINISH_TO = 9
        ''' <summary>を含む</summary>
        INCLUDES = 10
        ''' <summary>を含まない</summary>
        NOT_INCLUDES = 11
    End Enum

    ''' <summary>
    ''' オプションフィルタ共通
    ''' </summary>
    ''' <remarks></remarks>
    Public Module OptionFilterCommon

#Region " 定数 "
        ''' <summary>最大条件指定数</summary>
        Public Const MAX_FILTER_CONDITION As Integer = 5

        ''' <summary>論理演算AND</summary>
        Public Const CONDITION_AND As String = "AND"

        ''' <summary>論理演算OR</summary>
        Public Const CONDITION_OR As String = "OR"
#End Region

#Region " オプションフィルタ追加 "
        ''' <summary>
        ''' オプションフィルタを追加
        ''' </summary>
        ''' <param name="sheet">オプションフィルタを追加する対象のシート</param>
        ''' <param name="w_colIdx">オプションフィルタを追加する列インデックス</param>
        ''' <remarks></remarks>
        Public Sub SetOptionFilter(ByVal sheet As Spread.SheetView, ByVal w_colIdx As Integer, ByVal w_Head As Integer)
            ' フィルターを設定
            Dim hideRowFilter As New Spread.HideRowFilter(sheet)
            hideRowFilter.AllString = SpreadUtil.FILTER_ALL_STRING
            hideRowFilter.NonBlanksString = SpreadUtil.FILTER_NON_BLANKS_STRING
            sheet.RowFilter = hideRowFilter
            'フィルターを設定
            sheet.ColumnHeaderAutoFilterIndex = 1
            ' フィルターに列を追加
            Dim fcd As Spread.FilterColumnDefinition

            '全ての列へオプションフィルタを追加
            For colidx As Integer = 0 To w_colIdx

                'オートフィルタが有効になっていない場合, 有効にする。
                If Not sheet.Columns(colidx).AllowAutoFilter Then
                    sheet.Columns(colidx).AllowAutoFilter = True
                End If

                'デフォルトを変更
                fcd = New Spread.FilterColumnDefinition(colidx, Spread.FilterListBehavior.NonBlank)
                hideRowFilter.AddColumn(fcd)

                'フィルタ定義オブジェクトを取得
                Dim filterDef As Spread.FilterColumnDefinition = _
                    sheet.RowFilter.GetFilterColumnDefinition(colidx)
                filterDef.Filters.Add(New OptionFilter(sheet, "(フィルタオプション)", w_Head))
            Next

        End Sub
#End Region

#Region " 検索条件列挙に応じた名称 "
        ''' <summary>
        ''' 検索条件列挙に応じた名称
        ''' </summary>
        ''' <param name="index"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetOptionFilterConditionName(ByVal index As FILTER_CONDITION) As String
            Select Case index
                Case FILTER_CONDITION.NONE
                    GetOptionFilterConditionName = String.Empty
                Case FILTER_CONDITION.EQUALS
                    GetOptionFilterConditionName = "と等しい"
                Case FILTER_CONDITION.NOT_EQUALS
                    GetOptionFilterConditionName = "と等しくない"
                Case FILTER_CONDITION.BIGGER
                    GetOptionFilterConditionName = "より大きい"
                Case FILTER_CONDITION.MORE_THAN
                    GetOptionFilterConditionName = "以上"
                Case FILTER_CONDITION.SMALLER
                    GetOptionFilterConditionName = "より小さい"
                Case FILTER_CONDITION.LESS_THAN
                    GetOptionFilterConditionName = "以下"
                Case FILTER_CONDITION.BEGIN_TO
                    GetOptionFilterConditionName = "で始まる"
                Case FILTER_CONDITION.NOT_BEGIN_TO
                    GetOptionFilterConditionName = "で始まらない"
                Case FILTER_CONDITION.FINISH_TO
                    GetOptionFilterConditionName = "で終わる"
                Case FILTER_CONDITION.NOT_FINISH_TO
                    GetOptionFilterConditionName = "で終わらない"
                Case FILTER_CONDITION.INCLUDES
                    GetOptionFilterConditionName = "を含む"
                Case FILTER_CONDITION.NOT_INCLUDES
                    GetOptionFilterConditionName = "を含まない"
                Case Else
                    GetOptionFilterConditionName = ""
            End Select
        End Function
#End Region

#Region " フィルタのキャンセル "
        ''' <summary>
        ''' フィルタのキャンセル
        ''' </summary>
        ''' <param name="sheet">オプションフィルタを追加する対象のシート</param>
        ''' <param name="w_colIdx">オプションフィルタを追加する列インデックス</param>
        ''' <remarks></remarks>
        Public Sub SetFilterCancel(ByVal sheet As Spread.SheetView, ByVal w_colIdx As Integer, ByVal w_Head As Integer)

            ' フィルターを設定
            Dim hideRowFilter As New Spread.HideRowFilter(sheet)
            hideRowFilter.AllString = SpreadUtil.FILTER_ALL_STRING
            hideRowFilter.NonBlanksString = SpreadUtil.FILTER_NON_BLANKS_STRING
            sheet.RowFilter = hideRowFilter
            'フィルターを設定
            sheet.ColumnHeaderAutoFilterIndex = 1
            ' フィルターに列を追加
            Dim fcd As Spread.FilterColumnDefinition

            '全ての列へオプションフィルタを追加
            For colidx As Integer = 0 To w_colIdx

                'オートフィルタが有効になっていない場合, 有効にする。
                If Not sheet.Columns(colidx).AllowAutoFilter Then
                    sheet.Columns(colidx).AllowAutoFilter = True
                End If

                'デフォルトを変更
                fcd = New Spread.FilterColumnDefinition(colidx, Spread.FilterListBehavior.NonBlank)
                hideRowFilter.AddColumn(fcd)

                'フィルタ定義オブジェクトを取得
                Dim filterDef As Spread.FilterColumnDefinition = _
                    sheet.RowFilter.GetFilterColumnDefinition(colidx)
                filterDef.Filters.Add(New OptionFilter(sheet, "(フィルタオプション)", w_Head))
            Next

        End Sub
#End Region

    End Module

End Namespace


