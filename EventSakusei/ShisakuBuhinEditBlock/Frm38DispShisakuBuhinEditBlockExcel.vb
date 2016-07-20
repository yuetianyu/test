Imports EBom.Common
Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinEditBlock.Export2Excel
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon



Namespace ShisakuBuhinEditBlock
    ''' <summary>
    ''' 試作部品表編集・改訂編集（ブロック）・指定ブロックEXCEL出力画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm38DispShisakuBuhinEditBlockExcel

        Friend strButtonFlag As String
        Friend strEventCode As String
        Friend strBukaCode As String
        Friend strBlockNo As String
        Private strBlockKaiteNo As String
        Private kaiteiNo As Integer
        Private _strSekkeika As String
        Private _blockNoList As List(Of String)

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="buttonFlag">ボタンフラグ</param>
        ''' <param name="strEventCode">イベントコード</param>
        ''' <param name="strBukaCode">部課コード</param>
        ''' <param name="strBlockNo">ブロックNo</param>
        ''' <param name="strSekkeika">設計課</param>
        ''' <param name="blockNoList">ブロックNoリスト</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal buttonFlag As String, _
                       ByVal strEventCode As String, ByVal strBukaCode As String, ByVal strBlockNo As String _
                       , ByVal strSekkeika As String, ByVal blockNoList As List(Of String))

            ' This call is required by the Windows Form Designer.
            Me._strSekkeika = strSekkeika
            Me._blockNoList = blockNoList

            InitializeComponent()
            ShisakuFormUtil.Initialize(Me)

            Dim getDate As New EditBlock2ExcelDaoImpl()

            Dim title2Vos As List(Of LabelValueVo) = _
                    getDate.FindKaiteiNoBy(strEventCode, strBukaCode, strBlockNo)

            '複数ブロック選択された場合はクリア？これは仕様的にOKか？
            '==>仕様的にOK
            If _blockNoList.Count > 1 Then
                title2Vos.Clear()
            End If


            '2012/03/02
            'ベースを出す出さないの判断をここで正しく行う
            '選択されたブロックの組み合わせにより条件を判断する
            Dim isContainBase As Boolean = False
            '2012/02/04 ベース情報を追加'
            For Each v As String In blockNoList
                If Not getDate.IsNewCreatedBlock(strEventCode, strBukaCode, v) Then
                    '対象ブロックが作成されたブロックだった場合
                    isContainBase = True
                End If
            Next

            '複数選択されているがベースが一件もなかった場合
            If blockNoList.Count > 1 Then
                If isContainBase = False Then
                    Me.RbtnRireki.Visible = False
                End If
            End If

            If isContainBase Then
                Dim lVo As New LabelValueVo
                lVo.Label = "ベース"
                lVo.Value = "000"
                title2Vos.Add(lVo)
            End If

            'If blockNoList.Count > 1 Then
            '    Dim lVo As New LabelValueVo
            '    lVo.Label = "ベース"
            '    lVo.Value = "000"
            '    title2Vos.Add(lVo)
            'End If

            kaiteiNo = 0
            For Each vo As LabelValueVo In title2Vos
                If kaiteiNo < Integer.Parse(vo.Value) Then
                    kaiteiNo = Integer.Parse(vo.Value)
                End If
            Next

            FormUtil.BindLabelValuesToComboBox(cmbKaiteiNo, title2Vos, False)
            '２次改修
            FormUtil.BindLabelValuesToComboBox(cmbHikakuKaiteiNo, title2Vos, False)

        End Sub

        ''' <summary>
        ''' 最新チェックボックス
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub RbtnNew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtnNew.CheckedChanged
            '最新を選択した場合、改訂ナンバー、比較先改訂ナンバーは非表示にする。
            RbtnCheckd()
        End Sub

        ''' <summary>
        ''' 履歴チェックボックス
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub RbtnRireki_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtnRireki.CheckedChanged
            '履歴を選択した場合、改訂ナンバーを表示する。
            RbtnCheckd()
            '改訂を設定する。
            cmbKaiteiNo.SelectedIndex = 0   '最新の改訂をセット()
        End Sub

        ''' <summary>
        ''' 差分チェックボックス
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub RbtnRbtnSabun_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtnSabun.CheckedChanged
            '差分を選択した場合、改訂ナンバー、比較先改訂ナンバーを表示する。
            '複数選択時は表示しない'
            RbtnCheckd()
            '改訂を設定する。
            '複数指定の場合1はセットできない'
            If _blockNoList.Count = 1 Then
                Try
                    cmbKaiteiNo.SelectedIndex = 1   'ひとつ前の改訂をセット
                Catch ex As Exception
                    cmbKaiteiNo.SelectedIndex = 0   '最新の改訂をセット
                End Try
                cmbHikakuKaiteiNo.SelectedIndex = 0 '最新の改訂をセット
            End If
        End Sub

        '---------------------------------------------
        '２次改修
        '   差分ボタンの処理を追加
        Private Sub RbtnCheckd()

            If RbtnRireki.Checked = True Then
                '複数か否かで下記の通り変更
                If _blockNoList.Count <> 1 Then
                    'テキストと横幅を変更
                    RbtnSabun.Text = "差分(ベースと最新の部品表)"
                    Panel5.Width = 302
                    '横幅も調整
                    Me.Size = New Size(529, 102)
                    '位置を変更
                    lblKaitei.Location = New Point(379, 9)
                    cmbKaiteiNo.Location = New Point(432, 6)
                Else
                    'テキストと横幅を変更
                    RbtnSabun.Text = "差分"
                    Panel5.Width = 182
                    '横幅も調整
                    Me.Size = New Size(429, 102)
                    '位置を変更
                    lblKaitei.Location = New Point(259, 9)
                    cmbKaiteiNo.Location = New Point(310, 6)
                End If

                lblKaitei.Visible = True
                cmbKaiteiNo.Visible = True
                lblHikakuKaiteiNo.Visible = False
                cmbHikakuKaiteiNo.Visible = False
                lblHikaku.Visible = False
                lblKaitei.Text = "改訂№："
                'コントロール位置も調整
                'cmbKaiteiNo.Location = New Point(310, 6)
            ElseIf RbtnSabun.Checked = True Then
                If _blockNoList.Count <> 1 Then
                    'テキストと横幅を変更
                    RbtnSabun.Text = "差分(ベースと最新の部品表)"
                    Panel5.Width = 302

                    lblKaitei.Visible = False
                    cmbKaiteiNo.Visible = False
                    lblHikakuKaiteiNo.Visible = False
                    cmbHikakuKaiteiNo.Visible = False
                    lblHikaku.Visible = False
                    '横幅も調整
                    Me.Size = New Size(400, 102)
                Else
                    'テキストと横幅を変更
                    RbtnSabun.Text = "差分"
                    Panel5.Width = 182

                    lblKaitei.Visible = True
                    cmbKaiteiNo.Visible = True
                    lblHikakuKaiteiNo.Visible = True
                    cmbHikakuKaiteiNo.Visible = True
                    lblHikaku.Visible = True
                    lblKaitei.Text = "比較元改訂№："
                    'コントロール位置も調整
                    cmbKaiteiNo.Location = New Point(342, 6)
                    '横幅も調整
                    Me.Size = New Size(429, 102)
                End If
            Else
                '複数か否かで下記の通り変更
                If _blockNoList.Count <> 1 Then
                    'テキストと横幅を変更
                    RbtnSabun.Text = "差分(ベースと最新の部品表)"
                    Panel5.Width = 302
                    '横幅も調整
                    Me.Size = New Size(400, 102)
                Else
                    'テキストと横幅を変更
                    RbtnSabun.Text = "差分"
                    Panel5.Width = 182
                    '横幅も調整
                    Me.Size = New Size(300, 102)
                End If

                lblKaitei.Visible = False
                cmbKaiteiNo.Visible = False
                lblHikakuKaiteiNo.Visible = False
                cmbHikakuKaiteiNo.Visible = False
                lblHikaku.Visible = False

            End If
        End Sub

        ''' <summary>
        ''' 出力ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutput.Click
            Dim isRireki As Boolean = False
            Dim isBase As Boolean = False
            Dim oldShisakuBlockNoKaiteiNo As String = ""
            If RbtnNew.Checked Then
                '最新ボタンがチェック常置亜'
                '最新の改訂Noを取得する'
                Dim kai As String = "000" + kaiteiNo.ToString
                strBlockKaiteNo = kai.Substring(kai.Length - 3)
            ElseIf RbtnRireki.Checked Then
                '履歴ボタンがチェック状態'
                strBlockKaiteNo = cmbKaiteiNo.SelectedValue
                If StringUtil.Equals(cmbKaiteiNo.Text, "ベース") Then
                    isBase = True
                End If
                isRireki = True
            Else
                '差分ボタンがチェック状態'
                strBlockKaiteNo = cmbKaiteiNo.SelectedValue
                oldShisakuBlockNoKaiteiNo = cmbHikakuKaiteiNo.SelectedValue
                If StringUtil.Equals(cmbKaiteiNo.Text, "ベース") Then
                    isBase = True
                End If
            End If


            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            If RbtnSabun.Checked Then
                ''
                Dim sabun As New ShisakuBuhinEditCondition(strEventCode, _
                                                           strBukaCode, _
                                                           strBlockNo, _
                                                           strBlockKaiteNo, _
                                                           oldShisakuBlockNoKaiteiNo, _
                                                           _blockNoList, _
                                                           isBase)
                sabun.ExportShisakuBuhinEditConditon()

            Else
                ''
                Dim test As New ShisakuBuhinEditBlock2Excel(strButtonFlag, strEventCode, strBukaCode, strBlockNo, strBlockKaiteNo, _strSekkeika, _blockNoList, isRireki, isBase)
            End If

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

        End Sub

        ''' <summary>
        ''' キャンセルボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.Close()
        End Sub

        Private Sub Frm38DispShisakuBuhinEditBlockExcel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            '初期値
            RbtnNew.Checked = True
            RbtnRireki.Checked = False
            RbtnSabun.Checked = False
            If _blockNoList.Count <> 1 Then
                '横幅も調整
                Me.Size = New Size(400, 102)
            Else
                '横幅も調整
                Me.Size = New Size(300, 102)
            End If
        End Sub

        '改訂のコンボボックスの値が変更になったら以下の処理を実施。
        Private Sub cmbKaiteiNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbKaiteiNo.TextChanged
            '差分チェックボックスが付いている場合のみ有効。
            If RbtnSabun.Checked = True Then
                '比較元改訂№＞比較先改訂№の場合。
                If cmbKaiteiNo.SelectedIndex < cmbHikakuKaiteiNo.SelectedIndex Then
                    'ひとつ上の改訂№をセットする。
                    If cmbKaiteiNo.SelectedIndex = 0 Then
                        cmbHikakuKaiteiNo.SelectedIndex = 0
                    Else
                        cmbHikakuKaiteiNo.SelectedIndex = cmbKaiteiNo.SelectedIndex - 1
                    End If
                End If
            End If
        End Sub

        '改訂のコンボボックスの値が変更になったら以下の処理を実施。
        Private Sub cmbHikakuKaiteiNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbHikakuKaiteiNo.TextChanged
            '差分チェックボックスが付いている場合のみ有効。
            If RbtnSabun.Checked = True Then
                '比較元改訂№＞比較先改訂№の場合。
                If cmbKaiteiNo.SelectedIndex < cmbHikakuKaiteiNo.SelectedIndex Then
                    'メッセージを表示して最新の改訂を設定する。
                    'MsgBox("比較元改訂№以降の改訂№を選択してください。", MsgBoxStyle.OkOnly, "[警告]")
                    cmbHikakuKaiteiNo.SelectedIndex = 0
                End If
            End If
        End Sub
    End Class
End Namespace
