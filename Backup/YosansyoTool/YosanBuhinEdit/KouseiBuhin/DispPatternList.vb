Imports EBom.Common
Imports EBom.Common.mdlConstraint
Imports EBom.Data
Imports ShisakuCommon
Imports ShisakuCommon.Ui.Spd
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanBuhinEdit.KouseiBuhin

    Public Class DispPatternList
#Region " メンバー変数 "

        ''' <summary>ビュー</summary>
        Private m_view As Frm41KouseiBuhinSelector
        ''' <summary>FpSpread 共通</summary>
        Private m_spCom As SpreadCommon

        Public Const TAG_SELECT As String = "SELECT"
        Public Const TAG_PATTERN As String = "PATTERN"

#End Region

#Region " コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="f"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal f As Frm41KouseiBuhinSelector)
            m_view = f
            m_spCom = New SpreadCommon(m_view.spdPattern)
        End Sub
#End Region

#Region " ビュー初期化 "
        ''' <summary>
        ''' ビューの初期化        
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function InitView(ByVal patternListVos As List(Of TYosanBuhinEditPatternVo)) As RESULT

            ''Spreadの初期化
            SpreadUtil.Initialize(m_view.spdPattern)
            SetSpdColTag()
            SetSpdDataField()
            ''spreadにデータを設定する
            SetSpreadSource(patternListVos)

        End Function
#End Region

#Region " SPREADの列のデータフィールドを設定する "
        ''' <summary>
        ''' SPREADの列のデータフィールドを設定する        
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpdDataField()

            '列の項目を設定する           
            For i As Integer = 0 To m_view.spdKubun_Sheet1.ColumnCount - 1
                m_view.spdPattern_Sheet1.Columns(i).DataField = m_view.spdPattern_Sheet1.Columns(i).Tag
            Next
        End Sub
#End Region

#Region " SPREADの列のタグ値を設定する "
        ''' <summary>
        ''' SPREADの列のタグ値を設定する        
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpdColTag()
            '列の項目を設定する
            m_view.spdPattern_Sheet1.Columns(0).Tag = TAG_SELECT
            m_view.spdPattern_Sheet1.Columns(1).Tag = TAG_PATTERN
        End Sub
#End Region

#Region "SPREADで 列のセルの水平方向の配置を設定する。"
        Public Sub SetSpdColPro()
            'ラベル名と横幅変更
            m_spCom.GetColFromTag(TAG_PATTERN).Label = "パターン名"
            m_spCom.GetColFromTag(TAG_PATTERN).Width = 400

            m_spCom.GetColFromTag(TAG_SELECT).Label = "選択"
            m_spCom.GetColFromTag(TAG_SELECT).Width = 40

            m_spCom.GetColFromTag(TAG_SELECT).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_PATTERN).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            For i As Integer = 0 To m_view.spdPattern_Sheet1.ColumnCount - 1
                m_view.spdPattern_Sheet1.Columns(i).Resizable = False
            Next
            For i As Integer = 0 To m_view.spdpattern_Sheet1.RowCount - 1
                m_view.spdPattern_Sheet1.Rows(i).Resizable = False
            Next
        End Sub
#End Region

#Region "spreadのbind"
        Private Sub SetSpreadSource(ByVal patternListVos As List(Of TYosanBuhinEditPatternVo))

            'SPREADのデータソースを設定する
            '   パターン名分行を追加
            m_view.spdPattern_Sheet1.AddRows(0, patternListVos.Count)
            '　値をセット
            For i As Integer = 0 To patternListVos.Count - 1
                m_view.spdPattern_Sheet1.SetValue(i, m_spCom.GetColFromTag(TAG_SELECT).Index, False)
                m_view.spdPattern_Sheet1.SetValue(i, m_spCom.GetColFromTag(TAG_PATTERN).Index, patternListVos(i).PatternName.ToString)
            Next

            'SPREADの列のセルの水平方向の配置を再設定する。
            SetSpdColPro()
        End Sub
#End Region

    End Class
End Namespace
