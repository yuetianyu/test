'↓↓2014/10/16 酒井 ADD BEGIN

Imports EventSakusei.ShisakuBuhinEdit
Imports EventSakusei.TehaichoEdit

Public Class frm41SaiKakuninMin
    Private x As Integer
    Private y As Integer
    Public frm41 As Frm41DispShisakuBuhinEdit00
    Public frm20 As frm20DispTehaichoEdit

    Private Sub PictureBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        If Not frm41 Is Nothing Then
            frm41.UpdateBtnSai()
        End If
        If Not frm20 Is Nothing Then
            frm20.UpdateBtnSai()
        End If
        '↓↓2014/10/29 酒井 ADD BEGIN
        If Me.Left + Me.Width - frm41SaiKakunin.Width >= 0 Then
            frm41SaiKakunin.Location = New Point(Me.Left + Me.Width - frm41SaiKakunin.Width, Me.Top)
        Else
            frm41SaiKakunin.Location = New Point(0, Me.Top)
        End If
        '↑↑2014/10/29 酒井 ADD END
        frm41SaiKakunin.Show()
        Me.Close()
    End Sub

    Private Sub frm41SaiKakuninMin_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        Me.PictureBox2.BackColor = Color.Navy
    End Sub

    Private Sub frm41SaiKakuninMin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub frm41SaiKakuninMin_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
        Me.PictureBox2.BackColor = Color.Gray
    End Sub

    '    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
    'If e.Button = MouseButtons.Left Then
    'Me.Left = MousePosition.X - Me.Width / 2
    'Me.Top = MousePosition.Y - Me.Height / 2
    'End If
    'End Sub

    Private Sub frm41SaiKakuninMin_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

    End Sub

    Private Sub frm41SaiKakuninMin_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged


    End Sub

    Public Sub New()

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
    End Sub

    Private Sub PictureBox2_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox2.MouseMove
        'Formを小さくするため、BorderStyle=noneにし、ドラッグ移動を個別に組み込み
        If e.Button = MouseButtons.Left Then
            Me.Left = MousePosition.X - Me.Width / 2
            Me.Top = MousePosition.Y
        End If
    End Sub
End Class
'↑↑2014/10/16 酒井 ADD END
