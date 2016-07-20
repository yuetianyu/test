Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util

''↓↓2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bt) (TES)張 ADD BEGIN
Namespace ShisakuBuhinEdit.Selector

    Public Class HoyouBuhinFrm41HikakuResultSelector

        Private _resultOk As Boolean
        Private _ResultVos As List(Of HoyouBuhinfrm41HikakuResultSelectorVo)

        Private Const COLUMN_COUNT As Integer = 14
#Region "各COLUMNのINDEX定義"
        Private Const COLUMN_SELECTED_INDEX As Integer = 0
        Private Const COLUMN_FLAG_INDEX As Integer = 1
        Private Const COLUMN_KUBUN_INDEX As Integer = 2
        Private Const COLUMN_BUHIN_NO_INDEX As Integer = 3
        Private Const COLUMN_BUHIN_NAME_INDEX As Integer = 4
        Private Const COLUMN_INSU_INDEX As Integer = 5
        Private Const COLUMN_LEVEL_INDEX As Integer = 6
        Private Const COLUMN_SHUKEI_CODE_INDEX As Integer = 7
        Private Const COLUMN_SIA_SHUKEI_CODE_INDEX As Integer = 8
        Private Const COLUMN_GENCHO_KUBUN_INDEX As Integer = 9
        Private Const COLUMN_MAKER_CODE_INDEX As Integer = 10
        Private Const COLUMN_SHISAKU_KUBUN_INDEX As Integer = 11
        Private Const COLUMN_KAITEI_NO_INDEX As Integer = 12
        Private Const COLUMN_EDABAN_INDEX As Integer = 13
#End Region

#Region "各フラグの定義"
        Private Const FLAG_ADD As String = "A"
        Private Const FLAG_CHANGE As String = "C"
        Private Const FLAG_DELECT As String = "D"
#End Region

        Public Sub New(ByVal lstResult As List(Of HoyouBuhinfrm41HikakuResultSelectorVo))

            ' この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent()

            ShisakuFormUtil.Initialize(Me)

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            Me._ResultVos = lstResult

            Me.spdResult_Sheet1.RowCount = lstResult.Count

            Dim rowIndex As Integer = 0

            For Each resultSelectorVo As HoyouBuhinfrm41HikakuResultSelectorVo In lstResult
                '選択
                If resultSelectorVo.Flag = FLAG_ADD Then
                    spdResult_Sheet1.Rows(rowIndex).BackColor = Color.Orange
                    spdResult_Sheet1.Cells(rowIndex, COLUMN_SELECTED_INDEX).Value = True
                ElseIf resultSelectorVo.Flag = FLAG_CHANGE Then
                    spdResult_Sheet1.Rows(rowIndex).BackColor = Color.Khaki
                    spdResult_Sheet1.Cells(rowIndex, COLUMN_SELECTED_INDEX).Value = False
                ElseIf resultSelectorVo.Flag = FLAG_DELECT Then
                    spdResult_Sheet1.Rows(rowIndex).BackColor = Color.Gray
                    spdResult_Sheet1.Cells(rowIndex, COLUMN_SELECTED_INDEX).Value = False
                Else
                    spdResult_Sheet1.Rows(rowIndex).BackColor = Color.White
                    spdResult_Sheet1.Cells(rowIndex, COLUMN_SELECTED_INDEX).Value = False
                End If
                'フラグ
                Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_FLAG_INDEX).Value = resultSelectorVo.Flag
                '区分
                Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_KUBUN_INDEX).Value = resultSelectorVo.Kubun
                If resultSelectorVo.MotoGamen.Equals("Frm41DispShisakuBuhinEdit20") Then
                    '部品番号
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_BUHIN_NO_INDEX).Value = resultSelectorVo.BuhinKoseiRecordVo.BuhinNo
                    '部品名称
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_BUHIN_NAME_INDEX).Value = resultSelectorVo.BuhinKoseiRecordVo.BuhinName
                    '員数
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_INSU_INDEX).Value = resultSelectorVo.Insu
                    'レベル
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_LEVEL_INDEX).Value = resultSelectorVo.BuhinKoseiRecordVo.Level
                    '国内集計
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_SHUKEI_CODE_INDEX).Value = resultSelectorVo.BuhinKoseiRecordVo.ShukeiCode
                    '海外集計
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_SIA_SHUKEI_CODE_INDEX).Value = resultSelectorVo.BuhinKoseiRecordVo.SiaShukeiCode
                    '原調区分
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_GENCHO_KUBUN_INDEX).Value = resultSelectorVo.BuhinKoseiRecordVo.GencyoCkdKbn
                    '取引先コード
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_MAKER_CODE_INDEX).Value = resultSelectorVo.BuhinKoseiRecordVo.MakerCode
                    '試作区分
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_SHISAKU_KUBUN_INDEX).Value = resultSelectorVo.BuhinKoseiRecordVo.BuhinNoKbn
                    '改訂
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_KAITEI_NO_INDEX).Value = resultSelectorVo.BuhinKoseiRecordVo.BuhinNoKaiteiNo
                    '枝番
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_EDABAN_INDEX).Value = resultSelectorVo.BuhinKoseiRecordVo.EdaBan
                Else
                    '部品番号
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_BUHIN_NO_INDEX).Value = resultSelectorVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNo
                    '部品名称
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_BUHIN_NAME_INDEX).Value = resultSelectorVo.HoyouBuhinBuhinKoseiRecordVo.BuhinName
                    '員数
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_INSU_INDEX).Value = resultSelectorVo.Insu
                    'レベル
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_LEVEL_INDEX).Value = resultSelectorVo.HoyouBuhinBuhinKoseiRecordVo.Level
                    '国内集計
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_SHUKEI_CODE_INDEX).Value = resultSelectorVo.HoyouBuhinBuhinKoseiRecordVo.ShukeiCode
                    '海外集計
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_SIA_SHUKEI_CODE_INDEX).Value = resultSelectorVo.HoyouBuhinBuhinKoseiRecordVo.SiaShukeiCode
                    '原調区分
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_GENCHO_KUBUN_INDEX).Value = resultSelectorVo.HoyouBuhinBuhinKoseiRecordVo.GencyoCkdKbn
                    '取引先コード
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_MAKER_CODE_INDEX).Value = resultSelectorVo.HoyouBuhinBuhinKoseiRecordVo.MakerCode
                    '試作区分
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_SHISAKU_KUBUN_INDEX).Value = resultSelectorVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKbn
                    '改訂
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_KAITEI_NO_INDEX).Value = resultSelectorVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKaiteiNo
                    '枝番
                    Me.spdResult.ActiveSheet.Cells(rowIndex, COLUMN_EDABAN_INDEX).Value = resultSelectorVo.HoyouBuhinBuhinKoseiRecordVo.EdaBan
                End If

                rowIndex = rowIndex + 1
            Next
        End Sub

        ''' <summary></summary>
        ''' <returns></returns>
        Public ReadOnly Property ResultOk() As Boolean
            Get
                Return _resultOk
            End Get
        End Property

        ''' <summary></summary>
        ''' <returns></returns>
        Public ReadOnly Property ResultVos() As List(Of HoyouBuhinfrm41HikakuResultSelectorVo)
            Get
                Return _ResultVos
            End Get
        End Property

        ''' <summary>
        ''' 反映ボタン処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnHanei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHanei.Click
            Me._resultOk = True
            For rowIndex As Integer = 0 To Me._ResultVos.Count - 1
                If spdResult_Sheet1.Cells(rowIndex, COLUMN_SELECTED_INDEX).Value = True Then
                    _ResultVos.Item(rowIndex).CheckedKbn = True
                Else
                    _ResultVos.Item(rowIndex).CheckedKbn = False
                End If
            Next
            Me.Close()
        End Sub

        ''' <summary>
        ''' キャンセルボタン処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me._resultOk = False
            Me.Close()
        End Sub

        ''' <summary>
        ''' 比較先を全選択ボタン処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnCheckSaki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckSaki.Click
            For rowIndex As Integer = 0 To spdResult_Sheet1.RowCount - 1
                If String.Equals(spdResult_Sheet1.Cells(rowIndex, COLUMN_FLAG_INDEX).Value, "") Then
                    spdResult_Sheet1.Cells(rowIndex, COLUMN_SELECTED_INDEX).Value = True
                ElseIf String.Equals(spdResult_Sheet1.Cells(rowIndex, COLUMN_FLAG_INDEX).Value, "A") Then
                    spdResult_Sheet1.Cells(rowIndex, COLUMN_SELECTED_INDEX).Value = True
                ElseIf String.Equals(spdResult_Sheet1.Cells(rowIndex, COLUMN_FLAG_INDEX).Value, "C") And _
                       String.Equals(spdResult_Sheet1.Cells(rowIndex, COLUMN_KUBUN_INDEX).Value, "比較先") Then
                    spdResult_Sheet1.Cells(rowIndex, COLUMN_SELECTED_INDEX).Value = True
                Else
                    spdResult_Sheet1.Cells(rowIndex, COLUMN_SELECTED_INDEX).Value = False
                End If
            Next

        End Sub

        ''' <summary>
        ''' 比較元を全選択ボタン処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnCheckMoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckMoto.Click
            For rowIndex As Integer = 0 To spdResult_Sheet1.RowCount - 1
                If String.Equals(spdResult_Sheet1.Cells(rowIndex, COLUMN_FLAG_INDEX).Value, "") Then
                    spdResult_Sheet1.Cells(rowIndex, COLUMN_SELECTED_INDEX).Value = True
                ElseIf String.Equals(spdResult_Sheet1.Cells(rowIndex, COLUMN_FLAG_INDEX).Value, "D") Then
                    spdResult_Sheet1.Cells(rowIndex, COLUMN_SELECTED_INDEX).Value = True
                ElseIf String.Equals(spdResult_Sheet1.Cells(rowIndex, COLUMN_FLAG_INDEX).Value, "C") And _
                       String.Equals(spdResult_Sheet1.Cells(rowIndex, COLUMN_KUBUN_INDEX).Value, "比較元") Then
                    spdResult_Sheet1.Cells(rowIndex, COLUMN_SELECTED_INDEX).Value = True
                Else
                    spdResult_Sheet1.Cells(rowIndex, COLUMN_SELECTED_INDEX).Value = False
                End If
            Next

        End Sub

        ''' <summary>
        ''' 全て解除ボタン処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnCheckClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckClear.Click
            For rowIndex As Integer = 0 To spdResult_Sheet1.RowCount - 1
                spdResult_Sheet1.Cells(rowIndex, COLUMN_SELECTED_INDEX).Value = False
            Next

        End Sub
    End Class
End Namespace
