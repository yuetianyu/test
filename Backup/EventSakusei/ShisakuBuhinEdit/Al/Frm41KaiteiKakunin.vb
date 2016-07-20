Imports EventSakusei.ShisakuBuhinEdit

''' <summary>
''' 改訂コピー：展開内容反映確認
''' </summary>
''' <remarks>  2014/08/04 Ⅰ.11.改訂戻し機能 e) (TES)施 ADD </remarks>
Public Class Frm41KaiteiKakunin


    '↓↓2014/09/24 酒井 ADD BEGIN
    'Private _frm As Frm41DispShisakuBuhinEdit00
    Public _frm As Frm41DispShisakuBuhinEdit00
    '↑↑2014/09/24 酒井 ADD END
    Public Sub New(ByRef frm As Frm41DispShisakuBuhinEdit00)
        '↓↓2014/09/24 酒井 ADD BEGIN
        '_frm = frm
        '↑↑2014/09/24 酒井 ADD END
        InitializeComponent()
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub

    ''↓↓2014/08/05 Ⅰ.11.改訂戻し機能 w) (TES)施 ADD BEGIN
    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If Not (_frm Is Nothing) Then
            _frm.KaiteiCopyCancel()
        End If
        Me.Close()
        ''↑↑2014/08/05 Ⅰ.11.改訂戻し機能 w) (TES)施 ADDEND	
    End Sub

    ''↓↓2014/08/05 Ⅰ.11.改訂戻し機能 v) (TES)施 ADD BEGIN
    Private Sub BtnKakutei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKakutei.Click
        Me.Close()
    End Sub
    ''↑↑2014/08/05 Ⅰ.11.改訂戻し機能 v) (TES)施 ADDEND	
End Class