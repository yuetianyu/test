Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports ShisakuCommon
Imports ShisakuCommon.Ui

''' <summary>
''' 部品構成表示画面
''' </summary>
''' <remarks></remarks>

Public Class frm51KengenMaster02
#Region " メンバー変数 "
    ''' <summary>ロジック</summary>
    Private m_kengenMas As KengenMasert02 = Nothing
    Public MenuName As String
    Public isDataChanged As Boolean = False
    Public BukaCode As String
#End Region

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)

    End Sub

    ''' <summary>
    ''' 画面の初期化
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm51KengenMaster02_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.KeyPreview = True
            m_kengenMas = New KengenMasert02(Me)
            m_kengenMas.InitView()
            isDataChanged = False
            LblMessage.Text = ShisakuMsg.T0009
            cmbMENU.Focus()
        Catch ex As Exception
            Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
            Throw
        End Try
    End Sub
    ''' <summary>
    ''' アプリケーション終了を押す
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
        frm01Kakunin.lblKakunin.Text = "権限のメンテを終了します。"
        frm51Para = "btnEND"
        frm01Kakunin.ShowDialog()
        Select Case frm51ParaModori
            Case "E"
                Application.Exit()
        End Select
    End Sub
    ''' <summary>
    ''' 戻るボタンを押す
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
        If isDataChanged Then
            frm01Kakunin.lblKakunin.Text = "変更を更新せずに終了しますか？"
            frm51Para = "btnBACK"
            frm01Kakunin.ShowDialog()
            Select Case frm51ParaModori
                Case "B"
                    Me.Close()
            End Select
        Else
            Me.Close()
        End If

    End Sub
    ''' <summary>
    ''' 更新ボタンを押す
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCall.Click
        frm01Kakunin.lblKakunin.Text = "更新を実施しますか？"
        frm51Para = "btnCall"
        frm01Kakunin.ShowDialog()
        If frm51ParaModori = "1" Then
            '確認ボタンを押す
            m_kengenMas.UpdateButtonClick()
        End If

    End Sub
    ''' <summary>
    ''' time tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
    End Sub

    Private Sub spdInfo_TextTipFetch(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.TextTipFetchEventArgs) Handles spdInfo.TextTipFetch
        m_kengenMas.spdInfo_TextTipFetch(e)
    End Sub
    ''' <summary>
    ''' spread情報がチェッジ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub spdInfo_Change(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdInfo.Change
        isDataChanged = True
        '機能の再bind
        m_kengenMas.KinoBind(e)
    End Sub
    ''' <summary>
    ''' コンボボックスに　delete key press
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Controls_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbMENU.KeyDown
        ShisakuFormUtil.DelKeyDown(sender, e)
    End Sub
    ''' <summary>
    ''' spreadに　delete key press
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub spdInfo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdInfo.KeyDown
        m_kengenMas.spdInfo_KeyDown(sender, e)
    End Sub
    ''' <summary>
    ''' メニューの選択がチェッジ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbMENU_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMENU.SelectedValueChanged
        isDataChanged = True
        LblMessage.Text = ShisakuMsg.T0009
        m_kengenMas.MenuChange()
    End Sub
End Class