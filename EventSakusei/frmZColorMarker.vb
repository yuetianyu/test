Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports ShisakuCommon.Ui
Imports FarPoint.Win.Spread

''' <summary>
''' 色選択画面
''' </summary>
''' <remarks></remarks>
Public Class frmZColorMarker
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Me.New(Nothing)
    End Sub
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="aColor">戻り値の初期値</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal aColor As Color)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        'ShisakuFormUtil.Initialize(Me)
        ' Add any initialization after the InitializeComponent() call.
        _result = aColor

    End Sub
    Private _result As Color?
    Public ReadOnly Property Result() As Color?
        Get
            Return _result
        End Get
    End Property

    Private Sub Color01Pink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Color01Pink.Click
        _result = Color.Pink
        Close()
    End Sub

    Private Sub Color02Salmon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Color02Salmon.Click
        _result = Color.Salmon
        Close()
    End Sub

    Private Sub Color03Bisque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Color03Bisque.Click
        _result = Color.Bisque
        Close()
    End Sub

    Private Sub Color04LightSalmon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Color04LightSalmon.Click
        _result = Color.LightSalmon
        Close()
    End Sub

    Private Sub Color05LightYellow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Color05LightYellow.Click
        _result = Color.LightYellow
        Close()
    End Sub

    Private Sub Color06Yellow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Color06Yellow.Click
        _result = Color.Yellow
        Close()
    End Sub

    Private Sub Color07PaleGreen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Color07PaleGreen.Click
        _result = Color.PaleGreen
        Close()
    End Sub

    Private Sub Color08Lime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Color08Lime.Click
        _result = Color.Lime
        Close()
    End Sub

    Private Sub Color09Aquamarine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Color09Aquamarine.Click
        _result = Color.Aquamarine
        Close()
    End Sub

    Private Sub Color10Aqua_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Color10Aqua.Click
        _result = Color.Aqua
        Close()
    End Sub

    Private Sub Color11LightSteelBlue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Color11LightSteelBlue.Click
        _result = Color.LightSteelBlue
        Close()
    End Sub

    Private Sub Color12RoyalBlue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Color12RoyalBlue.Click
        _result = Color.RoyalBlue
        Close()
    End Sub

    Private Sub Color13Plum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Color13Plum.Click
        _result = Color.Plum
        Close()
    End Sub

    Private Sub Color14Fuchsia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Color14Fuchsia.Click
        _result = Color.Fuchsia
        Close()
    End Sub

    Private Sub Color15LightGray_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Color15LightGray.Click
        _result = Color.LightGray
        Close()
    End Sub

    Private Sub Color16Gray_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Color16Gray.Click
        _result = Color.Gray
        Close()
    End Sub

    Private Sub Color17White_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Color17White.Click
        'ホワイトを選択した場合カラーをクリアする。        _result = Nothing
        Close()
    End Sub

    ''' <summary>
    ''' ボタンの近くに（真下の辺り）表示する。
    ''' </summary>
    ''' <param name="activeSheet">色を設定するSpreadSheet</param>
    ''' <param name="button">ボタン</param>
    ''' <param name="adjustX"></param>
    ''' <param name="adjustY"></param>
    ''' <remarks></remarks>
    Public Shared Sub ShowUnderButton(ByVal activeSheet As SheetView, ByVal button As System.Windows.Forms.Button, Optional ByVal adjustX As Integer = 5, Optional ByVal adjustY As Integer = 55)

        If activeSheet.SelectionCount = 0 Then
            Return ''念の為
        End If

        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim ctrl As Control = button
        Do
            x += ctrl.Location.X
            y += ctrl.Location.Y
            ctrl = ctrl.Parent
        Loop While ctrl IsNot Nothing

        Dim colorFormLocation As Point = New Point(x + adjustX, y + adjustY)
        Using frm As New frmZColorMarker
            frm.Location = colorFormLocation
            frm.ShowDialog()

            Dim cr As FarPoint.Win.Spread.Model.CellRange = activeSheet.GetSelection(0)

            Dim row2 As Integer = cr.RowCount + cr.Row - 1
            Dim column2 As Integer = cr.ColumnCount + cr.Column - 1

            '2012/01/10 
            ''行選択した場合、カラム数取得に失敗する為、エラーになる
            Dim column As Integer = 0
            If cr.Column > 0 Then
                column = cr.Column
            Else
                column2 = activeSheet.ColumnCount - 1
            End If

            '2012/03/02'
            '列選択した場合行数取得に失敗する為、エラーになるので'
            'アルファベット欄とタイトル列には色を塗りたくないので'
            Dim row As Integer = 0
            If cr.Row > 0 Then
                row = cr.Row
            Else
                row2 = activeSheet.RowCount - 1
            End If

            If frm.Result Is Nothing Then
                activeSheet.Cells(row, column, row2, column2).ResetBackColor()
            Else
                activeSheet.Cells(row, column, row2, column2).BackColor = frm.Result
                'activeSheet.Cells(row, column, row2, column2).BackColor = frm.Result
                'activeSheet.Cells(cr.Row, column, row2, column2).BackColor = frm.Result
            End If

        End Using
    End Sub



End Class
