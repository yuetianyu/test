Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports EventSakusei.ShisakuBuhinEdit.Ikkatsu
Imports ShisakuCommon.Ui

Namespace ShisakuBuhinEdit
    ''' <summary>
    ''' 部品構成表示画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm41DispShisakuBuhinEdit21
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ShisakuFormUtil.Initialize(Me)

        End Sub
        Private Sub btnSettei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettei.Click
            'Frm48DispShisakuBuhinEditIkkatsu.ShowDialog()
        End Sub
    End Class
End Namespace


