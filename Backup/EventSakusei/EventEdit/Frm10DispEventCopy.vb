Imports EventSakusei.EventEdit.Dao
Imports EventSakusei.EventEdit.Logic
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util
Imports EventSakusei.EventEdit.Ui

Namespace EventEdit
    Public Class Frm10DispEventCopy : Implements Observer
        Private _eventCode As String
        Public ReadOnly Property SelectedEventCode() As String
            Get
                Return _eventCode
            End Get
        End Property

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBack.Click
            Me.Close()
        End Sub

        Private Sub BtnWrite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnWrite.Click
            If frm01Kakunin.ConfirmOkCancel("イベントコピーを実施します。") = MsgBoxResult.Ok Then
                _eventCode = TxtEventCode.Text
                Me.Close()
            End If
        End Sub

        Private Sub frm10DispIbentoCopy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            spdEvent.FocusRenderer = New FarPoint.Win.Spread.MarqueeFocusIndicatorRenderer(Color.Blue, 1)
            '初期起動時には以下のボタンを使用不可とする。
            BtnWrite.ForeColor = Color.Black
            BtnWrite.BackColor = Color.White
            BtnWrite.Enabled = False
            TxtEventCode.Text = ""
        End Sub

        Private Sub spdParts_LeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles spdEvent.LeaveCell
            If e.NewRow = -1 OrElse e.NewColumn = -1 Then
                Return
            End If

            ' 選択範囲の情報を表示します。
            TxtEventCode.Text = spdEvent.ActiveSheet.GetText(e.NewRow, 0)

            'イベント入力時は書込みボタンを使用可能へ
            If TxtEventCode.Text <> "" Then
                '
                BtnWrite.ForeColor = Color.Black
                BtnWrite.BackColor = Color.LightCyan
                BtnWrite.Enabled = True
            Else
                '
                BtnWrite.ForeColor = Color.Black
                BtnWrite.BackColor = Color.White
                BtnWrite.Enabled = False
            End If
        End Sub


        Private ReadOnly subject As EventEditCopy
        Private ReadOnly copyObserver As SpdEventCopyObserver

        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.Initialize(Me)
            ' Add any initialization after the InitializeComponent() call.
            subject = New EventEditCopy(New EventEditCopyDaoImpl)
            subject.addObserver(Me)

            copyObserver = New SpdEventCopyObserver(spdEvent, subject)

            InitializeControls()
            copyObserver.Initialize()

            subject.notifyObservers()
        End Sub

        ''' <summary>
        ''' コントロールを初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitializeControls()

            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.SetDateTimeNow(Me.LblDateNow, Me.LblTimeNow)
            ShisakuFormUtil.SetIdAndBuka(Me.LblCurrUserId, Me.LblCurrBukaName)

            ShisakuFormUtil.SettingDefaultProperty(TxtEventCode)
            TxtEventCode.MaxLength = 12
        End Sub

        Public Sub UpdateObserver(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update
        End Sub
    End Class
End Namespace