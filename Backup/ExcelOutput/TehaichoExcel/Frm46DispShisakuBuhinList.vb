Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports ShisakuCommon
Imports ShisakuCommon.Ui

Namespace TehaichoExcel
    ''' <summary>
    ''' 部品構成表示画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm46DispShisakuBuhinList

#Region " メンバー変数 "
        ''' <summary>ロジック</summary>
        Private m_shisakuBuhinList As DispShisakuBuhinList = Nothing
#End Region
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)

        End Sub
        ''' <summary>
        ''' 画面初期化
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Frm46DispShisakuBuhinList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                m_shisakuBuhinList = New DispShisakuBuhinList(Me)
                m_shisakuBuhinList.InitView()
            Catch ex As Exception
                Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
                Throw
            End Try
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
            txtIbentoNo.Text = spdParts_Sheet1.GetText(e.Row, 0)
            m_shisakuBuhinList.SetGamenByEventCode()
        End Sub

        ''' <summary>
        ''' 呼出しボタンを押したら　処理する
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
            'イベントコードに値がある場合は、編集モードで「試作部品表作成メニュー」画面へ遷移する。
            m_shisakuBuhinList.ToTehaichoList()
        End Sub

        ''' <summary>
        ''' ダブルクリックしたら　処理する
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdParts_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellDoubleClick
            If e.ColumnHeader Then
                Exit Sub
            End If
            txtIbentoNo.Text = spdParts_Sheet1.GetText(e.Row, 0)
            m_shisakuBuhinList.ToTehaichoList()
        End Sub

        ''' <summary>
        ''' 戻るボタンを押したら　処理する
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Me.Close()
        End Sub

        ''' <summary>
        ''' アプリケーション終了ボタンを押したら　アプリケーションは終了する。
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
            Application.Exit()
            System.Environment.Exit(0)
        End Sub

        Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)

        End Sub
        '''' <summary>
        '''' Time Tick
        '''' </summary>
        '''' <param name="sender"></param>
        '''' <param name="e"></param>
        '''' <remarks></remarks>
        'Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        '    ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        'End Sub

        Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        End Sub
    End Class
End Namespace
