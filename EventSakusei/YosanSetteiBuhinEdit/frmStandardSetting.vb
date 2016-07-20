Imports FarPoint.Win.Spread.CellType
Imports FarPoint.Win

Namespace YosanSetteiBuhinEdit

    ''' <summary>
    ''' 標準設定表
    ''' </summary>
    ''' <remarks></remarks>
    Public Class frmStandardSetting

#Region "コンストラクタ"

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal list As List(Of String()))

            ' この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent()

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            EBomSpread1_Sheet1.RowCount = list.Count


            For i As Integer = 0 To list.Count - 1
                EBomSpread1_Sheet1.Cells(i, 0).Value = list(i)(0)
                EBomSpread1_Sheet1.Cells(i, 1).Value = list(i)(1)
                'ツールチップ'
                EBomSpread1_Sheet1.Cells(i, 1).Note = list(i)(1)
                EBomSpread1_Sheet1.Cells(i, 1).NoteStyle = FarPoint.Win.Spread.NoteStyle.PopupNote
            Next


        End Sub

#End Region


        ''' <summary>
        ''' 戻るボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Me.Close()
        End Sub

        Private Sub EBomSpread1_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles EBomSpread1.CellDoubleClick
            _Value = EBomSpread1_Sheet1.Cells(e.Row, 1).Value
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

        Private _Value As String

        Public Property Value() As String
            Get
                Return _Value
            End Get
            Set(ByVal value As String)
                _Value = value
            End Set
        End Property


    End Class

End Namespace
