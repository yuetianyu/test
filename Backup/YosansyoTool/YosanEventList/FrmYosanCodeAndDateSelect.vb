Imports EBom.Common
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports YosansyoTool.YosanEventList.Logic

Namespace YosanEventList

    Public Class FrmYosanCodeAndDateSelect

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

        Public ReadOnly Property YyyyMm() As String
            Get
                Return txtYyyymm.Text.Replace("/", "")
            End Get
        End Property

        Public ReadOnly Property YosanCodeHireiMeta() As String
            Get
                'If StringUtil.IsNotEmpty(txtHireHi.Text) Then
                '    Return txtHireHi.Text.Trim
                'Else
                '    Return txtKoteHi.Text.Trim
                'End If
                Return txtHireHi_Meta.Text.Trim
            End Get
        End Property

        Public ReadOnly Property YosanCodeHireiKou() As String
            Get
                'If StringUtil.IsNotEmpty(txtHireHi.Text) Then
                '    Return txtHireHi.Text.Trim
                'Else
                '    Return txtKoteHi.Text.Trim
                'End If
                Return txtHireHi_Kou.Text.Trim
            End Get
        End Property

        Public ReadOnly Property YosanCodeHireiYusou() As String
            Get
                'If StringUtil.IsNotEmpty(txtHireHi.Text) Then
                '    Return txtHireHi.Text.Trim
                'Else
                '    Return txtKoteHi.Text.Trim
                'End If
                Return txtHireHi_Yusou.Text.Trim
            End Get
        End Property

        Public ReadOnly Property YosanCodeHireiIkan() As String
            Get
                'If StringUtil.IsNotEmpty(txtHireHi.Text) Then
                '    Return txtHireHi.Text.Trim
                'Else
                '    Return txtKoteHi.Text.Trim
                'End If
                Return txtHireHi_Ikan.Text.Trim
            End Get
        End Property

        Public ReadOnly Property YosanCodeHireiTrim() As String
            Get
                'If StringUtil.IsNotEmpty(txtHireHi.Text) Then
                '    Return txtHireHi.Text.Trim
                'Else
                '    Return txtKoteHi.Text.Trim
                'End If
                Return txtHireHi_Trim.Text.Trim
            End Get
        End Property

        Public ReadOnly Property YosanCodeKoteiTrim() As String
            Get
                'If StringUtil.IsNotEmpty(txtHireHi.Text) Then
                '    Return txtHireHi.Text.Trim
                'Else
                '    Return txtKoteHi.Text.Trim
                'End If
                Return txtKoteHi_Trim.Text.Trim
            End Get
        End Property

        Public ReadOnly Property YosanCodeKoteiMeta() As String
            Get
                'If StringUtil.IsNotEmpty(txtHireHi.Text) Then
                '    Return txtHireHi.Text.Trim
                'Else
                '    Return txtKoteHi.Text.Trim
                'End If
                Return txtKoteHi_Meta.Text.Trim
            End Get
        End Property

        Public ReadOnly Property Kubun() As String
            Get
                If StringUtil.IsNotEmpty(txtHireHi_Meta.Text) Then
                    Return DispYosanEventList.YOSAN_ZAIMU_HIREI
                Else
                    Return DispYosanEventList.YOSAN_ZAIMU_KOTEI
                End If
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

            ShisakuFormUtil.SettingDefaultProperty(txtYyyymm)

            ShisakuFormUtil.SettingDefaultProperty(txtHireHi_Meta)
            ShisakuFormUtil.SettingDefaultProperty(txtHireHi_Kou)
            ShisakuFormUtil.SettingDefaultProperty(txtHireHi_Yusou)
            ShisakuFormUtil.SettingDefaultProperty(txtHireHi_Ikan)
            ShisakuFormUtil.SettingDefaultProperty(txtHireHi_Trim)
            ShisakuFormUtil.SettingDefaultProperty(txtKoteHi_Trim)
            ShisakuFormUtil.SettingDefaultProperty(txtKoteHi_Meta)
            'デフォルトの工事指令№をセット
            txtHireHi_Meta.Text = "A,S"
            txtHireHi_Kou.Text = "Y,M"
            txtHireHi_Yusou.Text = "V"
            txtHireHi_Ikan.Text = "L,T,U"
            txtHireHi_Trim.Text = "Q,R,X,Z"

            'txtKoteHi_Trim.Text = "Q,R,X,Z"
            'txtKoteHi_Meta.Text = "A,S"
        End Sub
#End Region

#Region " ボタン "
        ''' <summary>
        ''' 取込ボタン処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
            '計上年月がブランクの場合
            If StringUtil.IsEmpty(txtYyyymm.Text) Then
                ComFunc.ShowErrMsgBox("計上年月を入力してください。")
                txtYyyymm.Focus()
                Return
            End If
            '計上年月が日付形式（YYYY/MM）ではない場合
            If txtYyyymm.Text.Split("/").Length < 2 OrElse IsDate(txtYyyymm.Text & "/01") = False Then
                ComFunc.ShowErrMsgBox("計上年月を正しく入力してください。")
                txtYyyymm.Focus()
                Return
            End If
            '比例費予算コード、固定費予算コードの両方がブランクの場合
            If StringUtil.IsEmpty(txtHireHi_Meta.Text) AndAlso _
                StringUtil.IsEmpty(txtHireHi_Kou.Text) AndAlso _
                StringUtil.IsEmpty(txtHireHi_Yusou.Text) AndAlso _
                StringUtil.IsEmpty(txtHireHi_Ikan.Text) AndAlso _
                StringUtil.IsEmpty(txtHireHi_Trim.Text) AndAlso _
                StringUtil.IsEmpty(txtHireHi_Trim.Text) AndAlso _
                StringUtil.IsEmpty(txtKoteHi_Meta.Text) Then
                ComFunc.ShowErrMsgBox("予算コードを入力してください。")
                txtHireHi_Meta.Focus()
                Return
            End If
            '予算コードが一ケタの半角英数文字＋","（カンマ）つづりで入力されていない場合
            If CheckYosanCode(txtHireHi_Meta.Text.Trim) = False Then
                ComFunc.ShowErrMsgBox("比例費の予算コード[メタル部品費]を正しく入力してください。")
                txtHireHi_Meta.Focus()
                Return
            End If
            If CheckYosanCode(txtHireHi_Kou.Text.Trim) = False Then
                ComFunc.ShowErrMsgBox("比例費の予算コード[鋼板材料]を正しく入力してください。")
                txtHireHi_Kou.Focus()
                Return
            End If
            If CheckYosanCode(txtHireHi_Yusou.Text.Trim) = False Then
                ComFunc.ShowErrMsgBox("比例費の予算コード[輸送費]を正しく入力してください。")
                txtHireHi_Yusou.Focus()
                Return
            End If
            If CheckYosanCode(txtHireHi_Ikan.Text.Trim) = False Then
                ComFunc.ShowErrMsgBox("比例費の予算コード[移管車＆生産部実績]を正しく入力してください。")
                txtHireHi_Ikan.Focus()
                Return
            End If
            If CheckYosanCode(txtHireHi_Trim.Text.Trim) = False Then
                ComFunc.ShowErrMsgBox("比例費の予算コード[トリム部品費]を正しく入力してください。")
                txtHireHi_Trim.Focus()
                Return
            End If
            If CheckYosanCode(txtKoteHi_Trim.Text.Trim) = False Then
                ComFunc.ShowErrMsgBox("固定費の予算コード[トリム型費]を正しく入力してください。")
                txtKoteHi_Trim.Focus()
                Return
            End If
            If CheckYosanCode(txtKoteHi_Meta.Text.Trim) = False Then
                ComFunc.ShowErrMsgBox("固定費の予算コード[メタル型費（治具）]を正しく入力してください。")
                txtKoteHi_Meta.Focus()
                Return
            End If
            '予算コードが比例費、固定費で同一文字が入力されている場合
            '   比例費、固定費共に入力されているときにチェックする。
            If StringUtil.IsNotEmpty(txtHireHi_Meta.Text.Trim) And StringUtil.IsNotEmpty(txtKoteHi_Meta.Text.Trim) Then
                If CheckYosanCodeHikaku(txtHireHi_Meta.Text.Trim, txtKoteHi_Meta.Text.Trim) = False Then
                    ComFunc.ShowErrMsgBox("予算コードが重複しています。ご確認下さい。")
                    txtHireHi_Meta.Focus()
                    Return
                End If
            End If
            If StringUtil.IsNotEmpty(txtHireHi_Trim.Text.Trim) And StringUtil.IsNotEmpty(txtKoteHi_Trim.Text.Trim) Then
                If CheckYosanCodeHikaku(txtHireHi_Trim.Text.Trim, txtKoteHi_Trim.Text.Trim) = False Then
                    ComFunc.ShowErrMsgBox("予算コードが重複しています。ご確認下さい。")
                    txtHireHi_Trim.Focus()
                    Return
                End If
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
        ''' 予算コードをチェック
        ''' </summary>
        ''' <param name="yosanCodes"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CheckYosanCode(ByVal yosanCodes As String) As Boolean
            Dim chars As Char() = yosanCodes.ToCharArray
            For Each c As Char In chars
                If (c < CChar("A") OrElse c > CChar("Z")) AndAlso c <> CChar(",") And _
                    (c < CChar("0") OrElse c > CChar("9")) AndAlso c <> CChar(",") Then
                    Return False
                End If
            Next

            Dim arrCode As String() = yosanCodes.Split(",")
            For i As Integer = 0 To arrCode.Length - 1
                If Not ShisakuComFunc.IsInLength(arrCode(i), 1) Then
                    Return False
                End If
            Next

            Return True
        End Function

        ''' <summary>
        ''' 予算コード（比例費、固定費で同一文字が存在するか）をチェック
        ''' </summary>
        ''' <param name="yosanCodeHirei">比例費の予算コード</param>
        ''' <param name="yosanCodeKotei">固定費の予算コード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CheckYosanCodeHikaku(ByVal yosanCodeHirei As String, ByVal yosanCodeKotei As String) As Boolean

            '比例費の予算コードを","で区切って配列にセット
            Dim arrCodeHirei As String() = yosanCodeHirei.Split(",")
            '固定費の予算コードを","を除外して変数にセット
            Dim arrCodeKotei As String = yosanCodeKotei.ToString.Replace(",", "")
            '同一文字チェック
            For i As Integer = 0 To arrCodeHirei.Length - 1
                If -1 <> arrCodeKotei.IndexOf(arrCodeHirei(i)) And StringUtil.IsNotEmpty(arrCodeHirei(i)) Then
                    Return False
                End If
            Next

            Return True
        End Function

        Private Sub txtYyyymm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtYyyymm.KeyPress
            If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> "/"c And e.KeyChar <> vbBack Then
                e.Handled = True
            End If
        End Sub

        Private Sub txtYyyymm_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtYyyymm.LostFocus

            Dim dt As DateTime
            If Not StringUtil.IsEmpty(txtYyyymm.Text) Then
                If txtYyyymm.Text.ToString.Length = 6 Then
                    txtYyyymm.Text = Mid(txtYyyymm.Text.ToString, 1, 4) & "/" & Mid(txtYyyymm.Text.ToString, 5, 2)
                End If
            End If
            If DateTime.TryParse(txtYyyymm.Text, dt) = False Then
                txtYyyymm.Text = ""
                txtYyyymm.Focus()
            End If
        End Sub

    End Class

End Namespace
