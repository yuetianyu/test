Imports EventSakusei.XVLView.Logic

Public Class frmBodySelect

    '''========================================================================
    ''' <summary>ダイアログ戻り値クラス</summary>
    ''' ---------------------------------------------------------------------
    ''' = ◆ 備考
    ''' ---------------------------------------------------------------------
    ''' <remarks>
    '''      <para>ダイアログの戻り値として使用します。</para>
    '''      <para>戻り値としてNothing判定ができるため便宜的にクラスにしています。</para>
    '''      <para>戻り値を単純にString型とすると、NullとNothing判定が難しいのです。</para>
    ''' </remarks>
    ''' ---------------------------------------------------------------------
    Public Class EditDialogResult
        ''' <summary>編集されたテキスト</summary>
        Public _TextValue As String

        ''' <summary>コンストラクタ</summary>
        Sub New()
            Me._TextValue = vbNullString
        End Sub

    End Class

#Region "メンバ変数"

    Dim mSubject As BodySelectSubject

    Private _Result As Boolean

#End Region


    '''========================================================================
    ''' <summary>ダイアログ戻り値格納メンバ</summary>
    '''========================================================================
    Private d_MyEditText As EditDialogResult = Nothing


    ''' <summary>
    '''      <para>ダイアログ表示メソッド</para>
    ''' </summary>
    ''' <param name="avOwner">
    '''      <para>このダイアログのオーナーウィンドウ</para>
    ''' </param>
    ''' <returns>
    '''      <para>正常又はエラー場合、値が返ります。</para>
    '''      <para>キャンセルされたときNothingが返ります。</para>
    ''' </returns>
    ''' <remarks>
    '''      <para>元々あったShowメソッドを隠してしまいます</para>
    ''' </remarks>
    Public Shadows Function Show(Optional ByVal avOwner As System.Windows.Forms.IWin32Window = Nothing) As EditDialogResult

        ' ====== 編集テキスト用の新しいインスタンスを生成
        d_MyEditText = New EditDialogResult

        ' ====== 基底クラスのダイアログを開く(このクラスのShowDialogは殺している)
        If avOwner Is Nothing Then
            MyBase.ShowDialog()
        Else
            MyBase.ShowDialog(avOwner)
        End If

        ' ====== 編集された値を返す
        Return d_MyEditText
    End Function

    Public Sub New(ByVal aEventCode As String, ByVal aBodyName As String)

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        mSubject = New BodySelectSubject(aEventCode)

        'ボディ名コンボボックスにアイテムを設定する.
        If Not mSubject.setBodyTypeComboBox(cmbBody) Then
            result = False
            Exit Sub
        End If
        result = True

        'ボディ名が既に選択済みの場合に選択済みのボディー名を設定.
        cmbBody.Text = aBodyName.ToString

        '開発符号コンボボックスにアイテムを設定する.
        mSubject.SetItemToCmbKaihatuFugo(cmbKaihatuFugo)

        ''選択コンボボックスに値を追加
        ''   以下の値を設定する。 クラス化する。
        ''   １：設構書情報、２：生産情報
        'cmbBody.Items.Clear()
        'cmbBody.Items.Add("")
        'cmbBody.Items.Add("BNFAYACU4QX")
        'cmbBody.Items.Add("BS9AYGCU6QX")


        'ハンドラ設定
        AddHandler cmbKaihatuFugo.SelectedIndexChanged, AddressOf cmbKaihatuFugo_SelectedIndexChanged
        AddHandler cmbKaihatuFugo.TextUpdate, AddressOf cmbKaihatuFugo_SelectedIndexChanged

    End Sub

    ''' <summary>
    ''' OK
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        d_MyEditText._TextValue = cmbBody.Text
        Me.Close()
    End Sub

    ''' <summary>
    ''' キャンセル
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        d_MyEditText._TextValue = Nothing
        Me.Close()
    End Sub

    Private Sub cmbKaihatuFugo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        mSubject.KaihatsuFugo = cmbKaihatuFugo.Text

        'ボディ名のアイテム情報を更新する.
        mSubject.setBodyTypeComboBox(cmbBody)

        'ボディ選択値のクリア
        cmbBody.Text = ""
    End Sub

#Region "プロパティ"

    Public Property Result() As Boolean
        Get
            Return _Result
        End Get
        Set(ByVal value As Boolean)
            _Result = value
        End Set
    End Property


#End Region

End Class