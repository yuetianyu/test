Imports YosansyoTool.YosanEventList.Logic
Imports ShisakuCommon.Ui
Imports ShisakuCommon
Imports EBom.Common

Namespace YosanEventList

    Public Class FrmYosanEventEdit

#Region " メンバー "
        Private _ColumnTag As String
        Private _ColumnValue As String
#End Region

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
        ''' <summary>
        ''' 入力内容を返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ResultInput() As String
            Get
                Return Me.txtContent.Text
            End Get
        End Property
#End Region

#Region " コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal columnTag As String, ByVal columnValue As String)
            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            Me._ColumnTag = columnTag
            Me._ColumnValue = columnValue
        End Sub
#End Region

#Region "フォームロード"
        ''' <summary>
        ''' フォームロード
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub FrmYosanEventEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            'テキストボックス初期設定
            ShisakuFormUtil.SettingDefaultProperty(txtContent)

            'イベント名称
            If String.Equals(_ColumnTag, DispYosanEventList.TAG_YOSAN_EVENT_NAME) Then
                Me.lblContent.Text = "イベント名："
                '改行を不可にする。
                Me.txtContent.Multiline = False
                Me.txtContent.Height = 22
                '※桁数100桁まで
                'Me.txtContent.MaxLength = 100
                'IMEモード
                Me.txtContent.ImeMode = Windows.Forms.ImeMode.On
            End If
            '予算コード
            If String.Equals(_ColumnTag, DispYosanEventList.TAG_YOSAN_CODE) Then
                Me.lblContent.Text = "予算コード："
                '改行を不可にする。
                Me.txtContent.Multiline = False
                Me.txtContent.Height = 22
                '※桁数2桁まで
                Me.txtContent.MaxLength = 2
                'IMEモード
                Me.txtContent.ImeMode = Windows.Forms.ImeMode.Disable
            End If
            '主な変更概要
            If String.Equals(_ColumnTag, DispYosanEventList.TAG_YOSAN_MAIN_HENKO_GAIYO) Then
                Me.lblContent.Text = "主な　　　：" & vbLf & "変更概要"
                '改行を可能にする。
                Me.txtContent.Multiline = True
                '※３行まで
                Me.txtContent.Height = 52
                '桁数は２５６まで
                'Me.txtContent.MaxLength = 256
                'IMEモード
                Me.txtContent.ImeMode = Windows.Forms.ImeMode.On
            End If
            '造り方及び製作条件
            If String.Equals(_ColumnTag, DispYosanEventList.TAG_YOSAN_TSUKURIKATA_SEISAKUJYOKEN) Then
                Me.lblContent.Text = "造り方及び：" & vbLf & "製作条件"
                '改行を可能にする。
                Me.txtContent.Multiline = True
                '※３行まで
                Me.txtContent.Height = 52
                '桁数は２５６まで
                'Me.txtContent.MaxLength = 256
                'IMEモード
                Me.txtContent.ImeMode = Windows.Forms.ImeMode.On
            End If
            'その他
            If String.Equals(_ColumnTag, DispYosanEventList.TAG_YOSAN_SONOTA) Then
                Me.lblContent.Text = "その他　　："
                '改行を可能にする。
                Me.txtContent.Multiline = True
                '※３行まで
                Me.txtContent.Height = 52
                '桁数は２５６まで
                'Me.txtContent.MaxLength = 256
                'IMEモード
                Me.txtContent.ImeMode = Windows.Forms.ImeMode.On
            End If

            'テキストの内容表示
            Me.txtContent.Text = _ColumnValue
        End Sub
#End Region

#Region " ボタン "
        ''' <summary>
        ''' 更新ボタン処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
            'エラーチェック
            If Not Validator() Then
                Return
            End If

            _ResultOk = True
            Me.Close()
        End Sub

        ''' <summary>
        ''' 戻るボタン処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            _ResultOk = False
            Me.Close()
        End Sub
#End Region

        ''' <summary>
        ''' 登録／保存時の入力チェック
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function Validator() As Boolean

            txtContent.BackColor = Nothing

            If StringUtil.IsEmpty(Me.txtContent.Text) Then

                'イベント名称
                If String.Equals(_ColumnTag, DispYosanEventList.TAG_YOSAN_EVENT_NAME) Then
                    Me.txtContent.BackColor = Color.Red
                    Me.txtContent.Focus()
                    ComFunc.ShowErrMsgBox("イベント名称を入力して下さい。")
                    Return False
                End If
                '予算コード
                If String.Equals(_ColumnTag, DispYosanEventList.TAG_YOSAN_CODE) Then
                    Me.txtContent.BackColor = Color.Red
                    Me.txtContent.Focus()
                    ComFunc.ShowErrMsgBox("予算コードを入力して下さい。")
                    Return False
                End If

                ''主な変更概要
                'If String.Equals(_ColumnTag, DispYosanEventList.TAG_YOSAN_MAIN_HENKO_GAIYO) Then
                '    ComFunc.ShowErrMsgBox("主な変更概要を入力して下さい。")
                'End If
                ''造り方及び製作条件
                'If String.Equals(_ColumnTag, DispYosanEventList.TAG_YOSAN_TSUKURIKATA_SEISAKUJYOKEN) Then
                '    ComFunc.ShowErrMsgBox("造り方及び製作条件を入力して下さい。")
                'End If
                ''その他
                'If String.Equals(_ColumnTag, DispYosanEventList.TAG_YOSAN_SONOTA) Then
                '    ComFunc.ShowErrMsgBox("その他を入力して下さい。")
                'End If

            Else
                'イベント名称
                If String.Equals(_ColumnTag, DispYosanEventList.TAG_YOSAN_EVENT_NAME) Then
                    If DispYosanEventList.LenB(Me.txtContent.Text) > 100 Then
                        Me.txtContent.BackColor = Color.Red
                        Me.txtContent.Focus()
                        ''エラーメッセージ
                        ComFunc.ShowErrMsgBox("イベント名称は半角100文字、全角50文字以内で入力して下さい。")
                        Return False
                    End If
                End If
                '予算コード
                If String.Equals(_ColumnTag, DispYosanEventList.TAG_YOSAN_CODE) Then
                    If DispYosanEventList.LenB(Me.txtContent.Text) > 2 Then
                        Me.txtContent.BackColor = Color.Red
                        Me.txtContent.Focus()
                        ''エラーメッセージ
                        ComFunc.ShowErrMsgBox("予算コードは半角2文字以内で入力して下さい。")
                        Return False
                    End If
                End If
                '主な変更概要
                If String.Equals(_ColumnTag, DispYosanEventList.TAG_YOSAN_MAIN_HENKO_GAIYO) Then
                    If DispYosanEventList.LenB(Me.txtContent.Text) > 256 Then
                        Me.txtContent.BackColor = Color.Red
                        Me.txtContent.Focus()
                        ''エラーメッセージ
                        ComFunc.ShowErrMsgBox("主な変更概要は半角256文字、全角128文字以内で入力して下さい。")
                        Return False
                    End If
                End If
                '造り方及び製作条件
                If String.Equals(_ColumnTag, DispYosanEventList.TAG_YOSAN_TSUKURIKATA_SEISAKUJYOKEN) Then
                    If DispYosanEventList.LenB(Me.txtContent.Text) > 256 Then
                        Me.txtContent.BackColor = Color.Red
                        Me.txtContent.Focus()
                        ''エラーメッセージ
                        ComFunc.ShowErrMsgBox("造り方及び製作条件は半角256文字、全角128文字以内で入力して下さい。")
                        Return False
                    End If
                End If
                'その他
                If String.Equals(_ColumnTag, DispYosanEventList.TAG_YOSAN_SONOTA) Then
                    If DispYosanEventList.LenB(Me.txtContent.Text) > 256 Then
                        Me.txtContent.BackColor = Color.Red
                        Me.txtContent.Focus()
                        ''エラーメッセージ
                        ComFunc.ShowErrMsgBox("その他は半角256文字、全角128文字以内で入力して下さい。")
                        Return False
                    End If
                End If

            End If

            Return True

        End Function

    End Class

End Namespace
