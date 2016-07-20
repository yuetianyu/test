Imports EBom.Common
Imports ShisakuCommon.Ui

Namespace ShisakuBuhinSakuseiList

    Public Class FrmDispEditModeSelect

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

        Public ReadOnly Property IsTehai() As Boolean
            Get
                Return Me.rdoBtnTehai.Checked()
            End Get
        End Property

        Public ReadOnly Property IsYosan() As Boolean
            Get
                Return Me.rdoBtnYosan.Checked
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
        ''' ＯＫボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
            If Me.rdoBtnTehai.Checked = False AndAlso Me.rdoBtnYosan.Checked = False Then
                ComFunc.ShowErrMsgBox("編集モードを選択してください。")
                Return
            End If

            _ResultOk = True
            Me.Close()
        End Sub

        ''' <summary>
        ''' ＣＡＮＣＥＬボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            _ResultOk = False
            Me.Close()
        End Sub
#End Region

    End Class
End Namespace
