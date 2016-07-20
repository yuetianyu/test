Imports EventSakusei.EventEdit.Logic
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports FarPoint.Win.Spread
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd

Namespace EventEdit.Ui
    Public Class SpdEventCopyObserver : Implements Observer
#Region "各列のTAG"
        Private Const TAG_EVENT_CODE As String = "EVENT_CODE"
        Private Const TAG_KAIHATSU_FUGO As String = "KAIHATSU_FUGO"
        Private Const TAG_EVENT As String = "EVENT"
        Private Const TAG_MT As String = "MT"
        Private Const TAG_EVENT_NAME As String = "EVENT_NAME"
        Private Const TAG_DAISU As String = "DAISU"
        Private Const TAG_HACHU As String = "HACHU"
        Private Const TAG_TENKAIBI As String = "TENKAIBI"
        Private Const TAG_SHIMEKIRIBI As String = "SHIMEKIRIBI"
        Private Const TAG_STATUS As String = "STATUS"
#End Region

        Private ReadOnly spread As FpSpread
        Private ReadOnly sheet As SheetView
        Private ReadOnly subject As EventEditCopy

        Public Sub New(ByVal spread As FpSpread, ByVal subject As EventEditCopy)
            Me.spread = spread
            If spread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            Me.sheet = spread.Sheets(0)
            Me.subject = subject

            subject.addObserver(Me)
        End Sub

        Public Sub Initialize()

            SpreadUtil.Initialize(spread)

            Dim index As Integer = 0

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EVENT_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAIHATSU_FUGO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EVENT
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MT
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EVENT_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_DAISU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_HACHU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TENKAIBI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHIMEKIRIBI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_STATUS

            For column As Integer = 0 To sheet.ColumnCount - 1
                sheet.Columns(column).Locked = True
            Next

            ' TODO subject.RecordSizeによってChangeフラグをonにしているが、、、、要熟考
            sheet.RowCount = subject.RecordSize

        End Sub

        Public Sub Update(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update
            If arg Is Nothing Then
                sheet.RowCount = subject.RecordSize
                For row As Integer = 0 To subject.RecordSize - 1
                    Update(observable, row)
                Next
            ElseIf IsNumeric(arg) Then
                Dim row As Integer = CType(arg, Integer)
                sheet.Cells(row, sheet.Columns(TAG_EVENT_CODE).Index).Value = subject.EventCode(row)
                sheet.Cells(row, sheet.Columns(TAG_KAIHATSU_FUGO).Index).Value = subject.KaihatsuFugo(row)
                sheet.Cells(row, sheet.Columns(TAG_EVENT).Index).Value = subject.EventPhaseName(row)
                sheet.Cells(row, sheet.Columns(TAG_MT).Index).Value = subject.UnitKbn(row)
                sheet.Cells(row, sheet.Columns(TAG_EVENT_NAME).Index).Value = subject.EventName(row)
                sheet.Cells(row, sheet.Columns(TAG_DAISU).Index).Value = subject.Daisu(row)
                sheet.Cells(row, sheet.Columns(TAG_HACHU).Index).Value = subject.HachuUmuName(row)
                sheet.Cells(row, sheet.Columns(TAG_TENKAIBI).Index).Value = subject.SekkeiTenkaiBi(row)
                sheet.Cells(row, sheet.Columns(TAG_SHIMEKIRIBI).Index).Value = subject.TeiseiShochiShimekiriBi(row)
                sheet.Cells(row, sheet.Columns(TAG_STATUS).Index).Value = subject.StatusName(row)

                'イベントだけ何故からセンターになるので左寄せ指定する。
                sheet.Cells(row, sheet.Columns(TAG_EVENT).Index).HorizontalAlignment = CellHorizontalAlignment.Left

            End If
        End Sub

    End Class
End Namespace