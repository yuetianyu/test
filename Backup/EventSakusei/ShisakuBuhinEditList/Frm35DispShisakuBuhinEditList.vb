Imports ShisakuCommon.Ui


Namespace ShisakuBuhinEditList

    ''' <summary>
    ''' 試作部品表　編集一覧
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm35DispShisakuBuhinEditList

        Private m_BuhinEditList As ShisakuBuhinEditList.DispShisakuBuhinEditList = Nothing
        ''0は "編集モード" , 1は"改定編集モード"
        Public strMode As String = ""

        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.setTitleVersion(Me)

        End Sub

        Private Sub Frm35DispShisakuBuhinEditList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            'Testために、あとで定数設定します。

            'Try
            '    m_BuhinEditList = New ShisakuBuhinEditList.DispShisakuBuhinEditList(Me)

            '    'MasterLogin画面初期化()
            '    m_BuhinEditList.InitView(strMode)

            '    '初期起動時には以下のボタンを使用不可とする。
            '    btnCall.ForeColor = Color.Black
            '    btnCall.BackColor = Color.White
            '    btnCall.Enabled = False

            'Catch ex As Exception
            '    Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
            '    Throw
            'End Try
            m_BuhinEditList = New ShisakuBuhinEditList.DispShisakuBuhinEditList(Me)

            'MasterLogin画面初期化()
            m_BuhinEditList.InitView(strMode)

            '初期起動時には以下のボタンを使用不可とする。
            btnCall.ForeColor = Color.Black
            btnCall.BackColor = Color.White
            btnCall.Enabled = False


        End Sub

        ''' <summary>
        ''' 呼出ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCall.Click
            Me.Hide()
            m_BuhinEditList.InitSenIView(strMode)
            Me.Show()
            '---------------------------------------------------------------
            '２次改修で追加。
            '   排他処理でステータスが更新されていた場合、自画面も閉じる。
            '以下のパラメータの場合自分も閉じる。
            If frm37ParaModori = "close" Then
                frm37ParaModori = Nothing
                Me.Close()
            End If
            '---------------------------------------------------------------
        End Sub

        ''' <summary>
        ''' 戻るボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Me.Close()
        End Sub

        ''' <summary>
        ''' アプリケーション終了ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
            Application.Exit()
            System.Environment.Exit(0)
        End Sub

        ''' <summary>
        ''' ダブルクリック行のイベントコードを次画面へ引き渡す。
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdParts_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellDoubleClick
            ''
            If e.ColumnHeader Then
                Exit Sub
            End If
            Me.Hide()
            txtIbentoNo.Text = spdParts.ActiveSheet.GetText(e.Row, 0)
            m_BuhinEditList.InitSenIView(strMode)
            Me.Show()
            '---------------------------------------------------------------
            '２次改修で追加。
            '   排他処理でステータスが更新されていた場合、自画面も閉じる。
            '以下のパラメータの場合自分も閉じる。
            If frm37ParaModori = "close" Then
                frm37ParaModori = Nothing
                Me.Close()
            End If
            '---------------------------------------------------------------
        End Sub

        ''' <summary>
        ''' 時間動く機能
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        End Sub

        ''' <summary>
        ''' ユーザーがセル内でマウスの左ボタンを押したら　処理する。
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdParts_CellClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellClick
            If e.ColumnHeader Then
                Exit Sub
            End If
            txtIbentoNo.Text = spdParts.ActiveSheet.GetText(e.Row, 0)

            If Not txtIbentoNo.Text.Equals(String.Empty) Then
                SetCallUnlock()
            Else
                SetCallLock()
            End If

        End Sub
        ''' <summary>
        ''' 呼出しボタンを使用可とする
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetCallUnlock()
            btnCall.ForeColor = Color.Black
            btnCall.BackColor = Color.LightCyan
            btnCall.Enabled = True
        End Sub
        ''' <summary>
        ''' 呼出しボタンを使用不可とする
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetCallLock()
            btnCall.ForeColor = Color.Black
            btnCall.BackColor = Color.White
            btnCall.Enabled = False
        End Sub
    End Class

End Namespace