Imports ShisakuCommon.Ui

''↓↓2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bo) (TES)張 ADD BEGIN
Public Class HoyouBuhinFrm41DispShisakuBuhinEdit30

    Public Sub New(ByVal lGousya)

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ShisakuFormUtil.Initialize(Me)

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        'lGousyaの号車名、初期チェック状態を、画面チェックリストに設定する。
        For Each gosya As String In lGousya
            Me.chkListBoxGosya.Items.Add(gosya, False)
        Next
    End Sub

End Class