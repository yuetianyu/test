Namespace TehaichoEdit
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Frm20ShutuzuKaiteiRireki
        Inherits System.Windows.Forms.Form

        'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Windows フォーム デザイナで必要です。
        Private components As System.ComponentModel.IContainer

        'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
        'Windows フォーム デザイナを使用して変更できます。  
        'コード エディタを使って変更しないでください。
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm20ShutuzuKaiteiRireki))
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.LblCurrPGId = New System.Windows.Forms.Label
            Me.LblCurrBukaName = New System.Windows.Forms.Label
            Me.LblDateNow = New System.Windows.Forms.Label
            Me.LblCurrUserId = New System.Windows.Forms.Label
            Me.LblTimeNow = New System.Windows.Forms.Label
            Me.Label4 = New System.Windows.Forms.Label
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.btnBACK = New System.Windows.Forms.Button
            Me.btnHozon = New System.Windows.Forms.Button
            Me.spdShutuzuKaiteiRireki = New FarPoint.Win.Spread.FpSpread
            Me.spdShutuzuKaiteiRireki_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.lblBlockNo = New System.Windows.Forms.Label
            Me.aaaa = New System.Windows.Forms.Label
            Me.lblBuhinNo = New System.Windows.Forms.Label
            Me.Label2 = New System.Windows.Forms.Label
            Me.lblDaihyoBuhin = New System.Windows.Forms.Label
            Me.Label5 = New System.Windows.Forms.Label
            Me.btnHanei = New System.Windows.Forms.Button
            Me.lblGyoId = New System.Windows.Forms.Label
            Me.Label3 = New System.Windows.Forms.Label
            Me.Panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            CType(Me.spdShutuzuKaiteiRireki, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdShutuzuKaiteiRireki_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Panel1
            '
            resources.ApplyResources(Me.Panel1, "Panel1")
            Me.Panel1.BackColor = System.Drawing.Color.Blue
            Me.Panel1.Controls.Add(Me.LblCurrPGId)
            Me.Panel1.Controls.Add(Me.LblCurrBukaName)
            Me.Panel1.Controls.Add(Me.LblDateNow)
            Me.Panel1.Controls.Add(Me.LblCurrUserId)
            Me.Panel1.Controls.Add(Me.LblTimeNow)
            Me.Panel1.Controls.Add(Me.Label4)
            Me.Panel1.Name = "Panel1"
            '
            'LblCurrPGId
            '
            resources.ApplyResources(Me.LblCurrPGId, "LblCurrPGId")
            Me.LblCurrPGId.ForeColor = System.Drawing.Color.White
            Me.LblCurrPGId.Name = "LblCurrPGId"
            '
            'LblCurrBukaName
            '
            resources.ApplyResources(Me.LblCurrBukaName, "LblCurrBukaName")
            Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
            Me.LblCurrBukaName.Name = "LblCurrBukaName"
            '
            'LblDateNow
            '
            resources.ApplyResources(Me.LblDateNow, "LblDateNow")
            Me.LblDateNow.ForeColor = System.Drawing.Color.White
            Me.LblDateNow.Name = "LblDateNow"
            '
            'LblCurrUserId
            '
            resources.ApplyResources(Me.LblCurrUserId, "LblCurrUserId")
            Me.LblCurrUserId.ForeColor = System.Drawing.Color.White
            Me.LblCurrUserId.Name = "LblCurrUserId"
            '
            'LblTimeNow
            '
            resources.ApplyResources(Me.LblTimeNow, "LblTimeNow")
            Me.LblTimeNow.ForeColor = System.Drawing.Color.White
            Me.LblTimeNow.Name = "LblTimeNow"
            '
            'Label4
            '
            resources.ApplyResources(Me.Label4, "Label4")
            Me.Label4.ForeColor = System.Drawing.Color.Yellow
            Me.Label4.Name = "Label4"
            '
            'Panel2
            '
            resources.ApplyResources(Me.Panel2, "Panel2")
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Controls.Add(Me.btnBACK)
            Me.Panel2.Name = "Panel2"
            '
            'btnBACK
            '
            resources.ApplyResources(Me.btnBACK, "btnBACK")
            Me.btnBACK.Name = "btnBACK"
            Me.btnBACK.UseVisualStyleBackColor = True
            '
            'btnHozon
            '
            resources.ApplyResources(Me.btnHozon, "btnHozon")
            Me.btnHozon.BackColor = System.Drawing.Color.LightCyan
            Me.btnHozon.Name = "btnHozon"
            Me.btnHozon.UseVisualStyleBackColor = False
            '
            'spdShutuzuKaiteiRireki
            '
            resources.ApplyResources(Me.spdShutuzuKaiteiRireki, "spdShutuzuKaiteiRireki")
            Me.spdShutuzuKaiteiRireki.AllowDragDrop = True
            Me.spdShutuzuKaiteiRireki.AllowDragFill = True
            Me.spdShutuzuKaiteiRireki.AllowUserFormulas = True
            Me.spdShutuzuKaiteiRireki.BackColor = System.Drawing.SystemColors.Control
            Me.spdShutuzuKaiteiRireki.EditModeReplace = True
            Me.spdShutuzuKaiteiRireki.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdShutuzuKaiteiRireki.Name = "spdShutuzuKaiteiRireki"
            Me.spdShutuzuKaiteiRireki.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both
            Me.spdShutuzuKaiteiRireki.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdShutuzuKaiteiRireki_Sheet1})
            Me.spdShutuzuKaiteiRireki.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdShutuzuKaiteiRireki.VisualStyles = FarPoint.Win.VisualStyles.Off
            '
            'spdShutuzuKaiteiRireki_Sheet1
            '
            Me.spdShutuzuKaiteiRireki_Sheet1.Reset()
            Me.spdShutuzuKaiteiRireki_Sheet1.SheetName = "Sheet1"
            Me.spdShutuzuKaiteiRireki_Sheet1.ColumnCount = 6
            Me.spdShutuzuKaiteiRireki_Sheet1.RowCount = 3
            Me.spdShutuzuKaiteiRireki_Sheet1.ColumnHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Numbers
            Me.spdShutuzuKaiteiRireki_Sheet1.FrozenColumnCount = 1
            Me.spdShutuzuKaiteiRireki_Sheet1.FrozenRowCount = 2
            Me.spdShutuzuKaiteiRireki_Sheet1.Models = CType(resources.GetObject("spdShutuzuKaiteiRireki_Sheet1.Models"), FarPoint.Win.Spread.SheetView.DocumentModels)
            Me.spdShutuzuKaiteiRireki_Sheet1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(160, Byte), Integer))
            Me.spdShutuzuKaiteiRireki_Sheet1.StartingRowNumber = -1
            '
            'lblBlockNo
            '
            resources.ApplyResources(Me.lblBlockNo, "lblBlockNo")
            Me.lblBlockNo.ForeColor = System.Drawing.Color.Black
            Me.lblBlockNo.Name = "lblBlockNo"
            '
            'aaaa
            '
            resources.ApplyResources(Me.aaaa, "aaaa")
            Me.aaaa.Name = "aaaa"
            '
            'lblBuhinNo
            '
            resources.ApplyResources(Me.lblBuhinNo, "lblBuhinNo")
            Me.lblBuhinNo.ForeColor = System.Drawing.Color.Black
            Me.lblBuhinNo.Name = "lblBuhinNo"
            '
            'Label2
            '
            resources.ApplyResources(Me.Label2, "Label2")
            Me.Label2.Name = "Label2"
            '
            'lblDaihyoBuhin
            '
            resources.ApplyResources(Me.lblDaihyoBuhin, "lblDaihyoBuhin")
            Me.lblDaihyoBuhin.ForeColor = System.Drawing.Color.Black
            Me.lblDaihyoBuhin.Name = "lblDaihyoBuhin"
            '
            'Label5
            '
            resources.ApplyResources(Me.Label5, "Label5")
            Me.Label5.Name = "Label5"
            '
            'btnHanei
            '
            resources.ApplyResources(Me.btnHanei, "btnHanei")
            Me.btnHanei.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
            Me.btnHanei.Name = "btnHanei"
            Me.btnHanei.UseVisualStyleBackColor = False
            '
            'lblGyoId
            '
            resources.ApplyResources(Me.lblGyoId, "lblGyoId")
            Me.lblGyoId.ForeColor = System.Drawing.Color.Black
            Me.lblGyoId.Name = "lblGyoId"
            '
            'Label3
            '
            resources.ApplyResources(Me.Label3, "Label3")
            Me.Label3.Name = "Label3"
            '
            'Frm20ShutuzuKaiteiRireki
            '
            Me.AllowDrop = True
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.Controls.Add(Me.lblGyoId)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.btnHanei)
            Me.Controls.Add(Me.lblDaihyoBuhin)
            Me.Controls.Add(Me.Label5)
            Me.Controls.Add(Me.lblBuhinNo)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.lblBlockNo)
            Me.Controls.Add(Me.aaaa)
            Me.Controls.Add(Me.btnHozon)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.spdShutuzuKaiteiRireki)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.KeyPreview = True
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "Frm20ShutuzuKaiteiRireki"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.Panel2.ResumeLayout(False)
            CType(Me.spdShutuzuKaiteiRireki, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdShutuzuKaiteiRireki_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label38 As System.Windows.Forms.Label
        Friend WithEvents Label39 As System.Windows.Forms.Label
        Friend WithEvents Label40 As System.Windows.Forms.Label
        Friend WithEvents Label41 As System.Windows.Forms.Label
        Friend WithEvents Label42 As System.Windows.Forms.Label
        Friend WithEvents Label43 As System.Windows.Forms.Label
        Friend WithEvents Label44 As System.Windows.Forms.Label
        Friend WithEvents Label45 As System.Windows.Forms.Label
        Friend WithEvents Label46 As System.Windows.Forms.Label
        Friend WithEvents Label47 As System.Windows.Forms.Label
        Friend WithEvents Label48 As System.Windows.Forms.Label
        Friend WithEvents Label49 As System.Windows.Forms.Label
        Friend WithEvents Label50 As System.Windows.Forms.Label
        Friend WithEvents Label51 As System.Windows.Forms.Label
        Friend WithEvents Label52 As System.Windows.Forms.Label
        Friend WithEvents Label53 As System.Windows.Forms.Label
        Friend WithEvents Label54 As System.Windows.Forms.Label
        Friend WithEvents Label55 As System.Windows.Forms.Label
        Friend WithEvents Label56 As System.Windows.Forms.Label
        Friend WithEvents Label57 As System.Windows.Forms.Label
        Friend WithEvents Label58 As System.Windows.Forms.Label
        Friend WithEvents Label59 As System.Windows.Forms.Label
        Friend WithEvents Label60 As System.Windows.Forms.Label
        Friend WithEvents Label61 As System.Windows.Forms.Label
        Friend WithEvents Label62 As System.Windows.Forms.Label
        Friend WithEvents Label63 As System.Windows.Forms.Label
        Friend WithEvents Label64 As System.Windows.Forms.Label
        Friend WithEvents Label65 As System.Windows.Forms.Label
        Friend WithEvents Label66 As System.Windows.Forms.Label
        Friend WithEvents Label67 As System.Windows.Forms.Label
        Friend WithEvents Label68 As System.Windows.Forms.Label
        Friend WithEvents Label69 As System.Windows.Forms.Label
        Friend WithEvents Label70 As System.Windows.Forms.Label
        Friend WithEvents Label71 As System.Windows.Forms.Label
        Friend WithEvents Label72 As System.Windows.Forms.Label
        Friend WithEvents Label73 As System.Windows.Forms.Label
        Friend WithEvents Label74 As System.Windows.Forms.Label
        Friend WithEvents Label75 As System.Windows.Forms.Label
        Friend WithEvents Label76 As System.Windows.Forms.Label
        Friend WithEvents Label77 As System.Windows.Forms.Label
        Friend WithEvents Label78 As System.Windows.Forms.Label
        Friend WithEvents Label79 As System.Windows.Forms.Label
        Friend WithEvents Label80 As System.Windows.Forms.Label
        Friend WithEvents Label81 As System.Windows.Forms.Label
        Friend WithEvents Label82 As System.Windows.Forms.Label
        Friend WithEvents Label83 As System.Windows.Forms.Label
        Friend WithEvents Label84 As System.Windows.Forms.Label
        Friend WithEvents Label85 As System.Windows.Forms.Label
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents btnBACK As System.Windows.Forms.Button
        Friend WithEvents btnHozon As System.Windows.Forms.Button
        Friend WithEvents SEL02 As System.Windows.Forms.Button
        Friend WithEvents spdShutuzuKaiteiRireki As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdShutuzuKaiteiRireki_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
        Friend WithEvents LblDateNow As System.Windows.Forms.Label
        Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
        Friend WithEvents LblTimeNow As System.Windows.Forms.Label
        Friend WithEvents LblCurrPGId As System.Windows.Forms.Label
        Friend WithEvents lblBlockNo As System.Windows.Forms.Label
        Friend WithEvents aaaa As System.Windows.Forms.Label
        Friend WithEvents lblBuhinNo As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents lblDaihyoBuhin As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents btnHanei As System.Windows.Forms.Button
        Friend WithEvents lblGyoId As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label

    End Class

End Namespace