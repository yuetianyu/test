Imports EBom.Common
Imports ShisakuCommon.Ui

Namespace YosanEventList

    Public Class FrmPartsPriceSelect

#Region " プロパティ "
        Private _ResultOk As Boolean
        ''' <summary>
        ''' OKを返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ResultOk() As Boolean
            Get
                Return _ResultOk
            End Get
        End Property

        Public ReadOnly Property IsKokunai() As Boolean
            Get
                Return Me.rdoKokunai.Checked
            End Get
        End Property

        Public ReadOnly Property IsKaigai() As Boolean
            Get
                Return Me.rdoKaigai.Checked
            End Get
        End Property
#End Region

#Region " コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            'ヘッダー部分の初期設定
            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.setTitleVersion(Me)
        End Sub
#End Region

#Region " ボタン "
        ''' <summary>
        ''' 取込ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
            If Me.rdoKokunai.Checked = False AndAlso Me.rdoKaigai.Checked = False Then
                ComFunc.ShowErrMsgBox("国内／海外を選択してください。")
                Return
            End If

            _ResultOk = True
            Me.Close()
        End Sub

        ''' <summary>
        ''' 戻るボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            _ResultOk = False
            Me.Close()
        End Sub
#End Region

    End Class
End Namespace
