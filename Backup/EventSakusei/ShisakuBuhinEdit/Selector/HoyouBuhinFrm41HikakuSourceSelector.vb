Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic

''↓↓2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bs) (TES)張 ADD
Namespace ShisakuBuhinEdit.Selector

    Public Class HoyouBuhinFrm41HikakuSourceSelector

        Private _resultOk As Boolean
        Private _MotoText As String
        Private _SakiText As String
        Private _SelectedStatus As Integer
        Private koseiSubject As HoyouBuhinBuhinEditKoseiSubject

        Public Sub New(ByVal koseiSubject As HoyouBuhinBuhinEditKoseiSubject)

            ' この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent()

            ShisakuFormUtil.Initialize(Me)

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            Me.koseiSubject = koseiSubject

            Me.txtMoto.Enabled = False

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
        Public ReadOnly Property MotoText() As String
            Get
                Return _MotoText
            End Get
        End Property

        ''' <summary></summary>
        ''' <returns></returns>
        Public ReadOnly Property SakiText() As String
            Get
                Return _SakiText
            End Get
        End Property

        ''' <summary></summary>
        ''' <returns></returns>
        Public ReadOnly Property SelectedStatus() As Integer
            Get
                Return _SelectedStatus
            End Get
        End Property

        ''' <summary>
        ''' 戻るボタン処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Me._resultOk = False
            Me.Close()
        End Sub

        ''' <summary>
        ''' 検索ボタン処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

            Dim isFinalMoto As Boolean = False
            Dim isFinalSaki As Boolean = False

            Me._MotoText = txtMoto.Text
            Me._SakiText = txtSaki.Text

            If Me.rdoBtnBase.Checked = True Then
                'ベース部品表チェックの場合
                Me._SelectedStatus = 0
            ElseIf Me.rdoBtnEbom.Checked = True Then
                'E-BOM情報チェックの場合
                Me._SelectedStatus = 1

                If CheckFinalBuhin(_MotoText) = False Then
                    MsgBox("比較元にはFINAL品番を入力してください。")
                    Exit Sub
                End If
            End If

            If CheckFinalBuhin(_SakiText) = False Then
                MsgBox("比較先にはFINAL品番を入力してください。")
                Exit Sub
            End If

            Me._resultOk = True

            Me.Close()

        End Sub

        ''' <summary>
        ''' Final品番とするかどうかを判断
        ''' </summary>
        ''' <param name="buhinNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CheckFinalBuhin(ByVal buhinNo As String) As Boolean

            If buhinNo Is Nothing Or String.IsNullOrEmpty(buhinNo) = True Then
                Return False
            End If

            Dim result As Rhac2210Vo = koseiSubject.GetFinalBuhinNo(buhinNo)
            Return result IsNot Nothing
        End Function

        Private Sub rdoBtnBase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoBtnBase.CheckedChanged
            Me.txtMoto.Enabled = False
        End Sub

        Private Sub rdoBtnEbom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoBtnEbom.CheckedChanged
            Me.txtMoto.Enabled = True
        End Sub

    End Class

End Namespace