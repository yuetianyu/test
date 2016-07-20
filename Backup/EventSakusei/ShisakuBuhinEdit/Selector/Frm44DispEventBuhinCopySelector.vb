Imports EBom.Common
Imports EventSakusei.ShisakuBuhinEdit.Selector.Logic
Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports EventSakusei.ShisakuBuhinEdit.Al.Logic
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util
Imports EventSakusei.Soubi
Imports EventSakusei.ShisakuBuhinEdit.Selector.Ui
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Selector.Dao

Namespace ShisakuBuhinEdit.Selector
    Public Class Frm44DispEventBuhinCopySelector : Implements Observer

        Public result As MsgBoxResult
        Public selectedShisakuEventCode As String
        Private blockNo As String
        Private notEventCode As String
        Private EventCopySubject As EventBuhinCopySubject
        Private ReadOnly aInputWatcher As InputWatcher
        Private IsSuspendTehaiValueChanged As Boolean

        Public Sub UpdateObserver(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update
            'If observable Is headerSubject Then
            '    UpdateBaseComplete(observable, arg)
            'Else
            '    subject.notifyObservers(arg)
            '    subject.BasicOptionSubject.notifyObservers(arg)
            '    subject.SpecialOptionSubject.notifyObservers(arg)
            'End If
            'subject.BaseCarSubject.notifyObservers(arg)
            'subject.CompleteCarSubject.notifyObservers(arg)
            'subject.BasicOptionSubject.notifyObservers(arg)
            'subject.SpecialOptionSubject.notifyObservers(arg)
            IsSuspendTehaiValueChanged = True
            Try
                FormUtil.BindLabelValuesToComboBox(cmbKaihatsuFugo, EventCopySubject.KaihatsuFugoLabelValues, False)
                FormUtil.SetComboBoxSelectedValue(cmbKaihatsuFugo, EventCopySubject.KaihatsuFugo)
            Finally
                IsSuspendTehaiValueChanged = False
            End Try


        End Sub

        Public Sub New(ByVal blockNo As String, ByVal notEventCode As String)

            selectedShisakuEventCode = ""
            Me.blockNo = blockNo
            Me.notEventCode = notEventCode

            EventCopySubject = New EventBuhinCopySubject(notEventCode, blockNo)

            ' この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent()

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.setTitleVersion(Me)
            EventCopySubject.AddObserver(Me)

            aInputWatcher = New InputWatcher

            InitializeWatcher()

            EventCopySubject.NotifyObservers()

            aInputWatcher.Clear()

            Dim dao As New DispEventBuhinCopySelectorDaoImpl
            Dim eventVos As New List(Of TShisakuEventVo)
            eventVos = dao.FindEvent(blockNo, notEventCode)
            Dim i As Integer = 0
            If eventVos.Count = 0 Then
                _eventCount = False
            Else
                _eventCount = True
            End If

        End Sub

        Private Sub Initialize()
            '開発符号の初期化'
            ShisakuFormUtil.SettingDefaultProperty(cmbKaihatsuFugo)
        End Sub



        ''' <summary>
        ''' コンボボックスの監視
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitializeWatcher()
            aInputWatcher.Add(cmbKaihatsuFugo)
        End Sub

        Private Sub Frm44DispEventBuhinCopySelector_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            Dim dao As New DispEventBuhinCopySelectorDaoImpl
            Dim eventVos As New List(Of TShisakuEventVo)
            eventVos = dao.FindByEventKaiHatsuFugo(blockNo, notEventCode, EventCopySubject.KaihatsuFugo)
            Dim i As Integer = 0
            If eventVos.Count = 0 Then
                _eventCount = False
                Me.btnOK.Enabled = False
            Else
                _eventCount = True
                selectedShisakuEventCode = eventVos(0).ShisakuEventCode
            End If

            For Each eventVo As TShisakuEventVo In eventVos
                Me.spdEvent_Sheet1.RowCount = i + 1
                Me.spdEvent_Sheet1.Cells(i, 0).Value = eventVo.ShisakuEventCode
                Me.spdEvent_Sheet1.Cells(i, 1).Value = eventVo.ShisakuKaihatsuFugo
                Me.spdEvent_Sheet1.Cells(i, 2).Value = eventVo.ShisakuEventPhaseName
                Me.spdEvent_Sheet1.Cells(i, 3).Value = eventVo.UnitKbn
                Me.spdEvent_Sheet1.Cells(i, 4).Value = eventVo.ShisakuEventName

                i = i + 1
            Next

        End Sub

        Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click

            If selectedShisakuEventCode = "" Then
                MsgBox("コピーするイベントを選択して下さい。")
                Return
            End If
            result = MsgBoxResult.Ok
            Me.Close()
        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            result = MsgBoxResult.Cancel
            Me.Close()
        End Sub

        Private Sub spdEvent_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdEvent.CellClick

            selectedShisakuEventCode = Me.spdEvent_Sheet1.Cells(e.Row, 0).Value.ToString

        End Sub

#Region "公開プロパティ"
        'イベント情報の存在チェック'
        Private _eventCount As Boolean

        Public ReadOnly Property EventCount() As Boolean
            Get
                Return _eventCount
            End Get
        End Property


#End Region



        ''' <summary>
        ''' コンボボックスの処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbKaihatsuFugo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbKaihatsuFugo.SelectedIndexChanged
            If Not EventCopySubject Is Nothing Then
                EventCopySubject.KaihatsuFugo = cmbKaihatsuFugo.SelectedValue
            End If

            Dim dao As New DispEventBuhinCopySelectorDaoImpl
            Dim eventVos As New List(Of TShisakuEventVo)
            eventVos = dao.FindByEventKaiHatsuFugo(blockNo, notEventCode, EventCopySubject.KaihatsuFugo)
            Dim i As Integer = 0
            If eventVos.Count = 0 Then
                _eventCount = False
                Me.btnOK.Enabled = False
            Else
                _eventCount = True
                selectedShisakuEventCode = eventVos(0).ShisakuEventCode
            End If

            For Each eventVo As TShisakuEventVo In eventVos
                Me.spdEvent_Sheet1.RowCount = i + 1
                Me.spdEvent_Sheet1.Cells(i, 0).Value = eventVo.ShisakuEventCode
                Me.spdEvent_Sheet1.Cells(i, 1).Value = eventVo.ShisakuKaihatsuFugo
                Me.spdEvent_Sheet1.Cells(i, 2).Value = eventVo.ShisakuEventPhaseName
                Me.spdEvent_Sheet1.Cells(i, 3).Value = eventVo.UnitKbn
                Me.spdEvent_Sheet1.Cells(i, 4).Value = eventVo.ShisakuEventName

                i = i + 1
            Next

        End Sub

    End Class

End Namespace