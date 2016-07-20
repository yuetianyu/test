Imports ShisakuCommon
Imports ShisakuCommon.Ui

Public Class frm03Syorichu

#Region "起動"
    ''' <summary>
    ''' 起動
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Execute()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)
        ShisakuFormUtil.setTitleVersion(Me)
    End Sub
#End Region

End Class