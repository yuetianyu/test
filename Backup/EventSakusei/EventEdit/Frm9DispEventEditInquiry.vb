Imports FarPoint.Win
Imports EventSakusei.EventEdit.Logic
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Util
Imports EventSakusei.EventEdit.Ui
Imports ShisakuCommon.Ui
Imports FarPoint.Win.Spread
Imports ShisakuCommon.Ui.Spd

Namespace EventEdit
    ''' <summary>
    ''' 部品構成表示画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm9DispEventEditInquiry : Implements Observer

        'Private Sub spdParts_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdOptionSpec.CellClick
        '    If e.Button = Windows.Forms.MouseButtons.Right Then
        '        '右クリックされたセルをアクティブセルに設定する.
        '        Dim ht As Spread.HitTestInformation = spdOptionSpec.HitTest(e.X, e.Y)
        '        Dim sheet As Spread.SheetView = spdOptionSpec.ActiveSheet
        '        Dim preRow As Integer = sheet.ActiveRowIndex
        '        Dim preCol As Integer = sheet.ActiveColumnIndex

        '        If ht.Type = Spread.HitTestType.Viewport Then
        '            Dim vi As Spread.ViewportHitTestInformation = ht.ViewportInfo

        '            If vi.Row > -1 And vi.Column > -1 Then
        '                '移動前のセル背景色リセット
        '                sheet.Cells(preRow, preCol).ResetBackColor()

        '                'アクティブセル設定
        '                sheet.SetActiveCell(vi.Row, vi.Column)

        '            End If
        '        End If
        '    End If
        'End Sub

        Private Sub BtnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBack.Click
            Me.Close()
        End Sub

        Private ReadOnly shisakuSoubiKbn As String
        Private ReadOnly subject As EventEditInquiry
        Private ReadOnly spreadDragDrop As EventSpreadDragDrop
        Private IsSuspendComboboxValueChanged As Boolean

        ''' <summary>
        ''' 「基本装備」を参照する照会画面を作成する
        ''' </summary>
        ''' <param name="spreadDrop">ドラッグ＆ドロップのドロップ先Spread</param>
        ''' <param name="aFormClosedEventHandler">FormClosedEventHandler</param>
        ''' <returns>新しいインスタンス</returns>
        ''' <remarks></remarks>
        Public Shared Function NewBasicInquiry(ByVal spreadDrop As FpSpread, ByVal aFormClosedEventHandler As FormClosedEventHandler) As Frm9DispEventEditInquiry
            Return New Frm9DispEventEditInquiry(spreadDrop, MShisakuSoubiVoHelper.ShisakuSoubiKbn.BASIC_OPTION, aFormClosedEventHandler)
        End Function
        ''' <summary>
        ''' 「特別装備」を参照する照会画面を作成する
        ''' </summary>
        ''' <param name="spreadDrop">ドラッグ＆ドロップのドロップ先Spread</param>
        ''' <param name="aFormClosedEventHandler">FormClosedEventHandler</param>
        ''' <returns>新しいインスタンス</returns>
        ''' <remarks></remarks>
        Public Shared Function NewSpecialInquiry(ByVal spreadDrop As FpSpread, ByVal aFormClosedEventHandler As FormClosedEventHandler) As Frm9DispEventEditInquiry
            Return New Frm9DispEventEditInquiry(spreadDrop, MShisakuSoubiVoHelper.ShisakuSoubiKbn.SPECIAL_OPTION, aFormClosedEventHandler)
        End Function

        Private Sub New(ByVal spreadDrop As FpSpread, ByVal shisakuSoubiKbn As String, ByVal aFormClosedEventHandler As FormClosedEventHandler)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)

            If aFormClosedEventHandler IsNot Nothing Then
                AddHandler Me.FormClosed, aFormClosedEventHandler
            End If

            Me.shisakuSoubiKbn = shisakuSoubiKbn
            Me.spreadDragDrop = New EventSpreadDragDrop(spdOptionSpec, spreadDrop)

            subject = New EventEditInquiry(shisakuSoubiKbn, New Rhac0120DaoImpl, New MShisakuSoubiDaoImpl)
            subject.addObserver(Me)

            InitializeControls()

            UpdateObserver(subject, Nothing)
        End Sub

        ''' <summary>
        ''' コントロールを初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitializeControls()
            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.SetDateTimeNow(Me.LblDateNow, Me.LblTimeNow)

            IsSuspendComboboxValueChanged = True
            Try
                ShisakuFormUtil.SettingDefaultProperty(CmbOptionSpec)
                FormUtil.BindLabelValuesToComboBox(CmbOptionSpec, subject.GetLabelValues_OptionSpec, True)
            Finally
                IsSuspendComboboxValueChanged = False
            End Try

            SpreadUtil.Initialize(spdOptionSpec)

            spdOptionSpec_Sheet1.RowCount = 0
        End Sub

        Public Sub UpdateObserver(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update
            spdOptionSpec_Sheet1.RowCount = subject.RecordSize
            For rowNo As Integer = 0 To spdOptionSpec_Sheet1.RowCount - 1
                spdOptionSpec_Sheet1.Cells(rowNo, 0).Value = subject.RecordOptionSpec(rowNo)
            Next
        End Sub

        Private Sub CmbOptionSpec_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbOptionSpec.SelectedValueChanged
            If subject IsNot Nothing And Not IsSuspendComboboxValueChanged Then
                subject.OptoinSpec = Convert.ToString(CmbOptionSpec.SelectedValue)
                UpdateObserver(subject, Nothing)
            End If
        End Sub

        Private Sub CmbOptionSpec_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbOptionSpec.TextChanged
            If subject IsNot Nothing And Not IsSuspendComboboxValueChanged Then
                subject.InputOptionSpec = CmbOptionSpec.Text
                UpdateObserver(subject, Nothing)
            End If
        End Sub

        Private Sub RdoEBom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdoEBom.Click
            If subject IsNot Nothing Then
                subject.IsSelectEBomMaster = True
                UpdateObserver(subject, Nothing)
            End If
        End Sub

        Private Sub RdoShisaku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdoShisaku.Click
            If subject IsNot Nothing Then
                subject.IsSelectEBomMaster = False
                UpdateObserver(subject, Nothing)
            End If
        End Sub
    End Class
End Namespace
