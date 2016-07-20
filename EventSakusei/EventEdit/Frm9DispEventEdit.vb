Imports ShisakuCommon.Ui.Access
Imports ShisakuCommon.Db
Imports EBom.Common
Imports ShisakuCommon.Util
Imports ShisakuCommon
Imports EventSakusei.EventEdit.Export2Excel
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Valid
Imports FarPoint.Win
Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.Model
Imports ShisakuCommon.Ui.Spd
Imports System.Text
Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.ShisakuComFunc
Imports EventSakusei.EventEdit.ExcelOutput
Imports EventSakusei.ExportShisakuEventInfoExcel.Dao
Imports EventSakusei.EventEdit.Vo
Imports EventSakusei.ShisakuBuhinMenu.Dao

Namespace EventEdit

    ''' <summary>
    ''' イベント情報登録・編集
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm9DispEventEdit : Implements Observer
        ''' <summary>基本装備スプレッドの表示列</summary>
        Public Const BASIC_OPTION_COLUMN_COUNT As Integer = 100
        ''' <summary>特別装備スプレッドの表示列</summary>
        Public Const SPECIAL_OPTION_COLUMN_COUNT As Integer = 300

        Private IsSuspendEventValueChanged As Boolean

        'Private Sub spdParts_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts3.CellClick
        '    Try
        '        If e.Button = Windows.Forms.MouseButtons.Right Then
        '            '右クリックされたセルをアクティブセルに設定する.
        '            Dim ht As Spread.HitTestInformation = spdParts3.HitTest(e.X, e.Y)
        '            Dim sheet As Spread.SheetView = spdParts3.ActiveSheet
        '            Dim preRow As Integer = sheet.ActiveRowIndex
        '            Dim preCol As Integer = sheet.ActiveColumnIndex

        '            If ht.Type = Spread.HitTestType.Viewport Then
        '                Dim vi As Spread.ViewportHitTestInformation = ht.ViewportInfo

        '                If vi.Row > -1 And vi.Column > -1 Then
        '                    '移動前のセル背景色リセット
        '                    sheet.Cells(preRow, preCol).ResetBackColor()

        '                    'アクティブセル設定
        '                    sheet.SetActiveCell(vi.Row, vi.Column)

        '                End If
        '            End If
        '        End If

        '    Catch ex As Exception
        '    End Try
        'End Sub

        'Private Sub Frm9DispEventEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '    'ステータスを表示する。
        '    'txtStatus.Text = Frm8DispShisakuBuhinMenu.lblStatus.Text
        '    'txtDBStatus.Text = frm9ParaDBStatus
        '    '設計展開後はコピーボタンの使用及び入力項目を制限する。
        '    If txtStatus.Text <> "" Then
        '        btnCopy.ForeColor = Color.Black
        '        btnCopy.BackColor = Color.White
        '        btnCopy.Enabled = False
        '        '   
        '        cmbKaihatufugo.Enabled = False
        '        cmbEventPhaseName.Enabled = False
        '        txtEventName.Enabled = False
        '        cmbUnitKbn.Enabled = False
        '        txtEventName.Enabled = False

        '        spdBaseCar.Enabled = False
        '    Else
        '        btnCopy.ForeColor = Color.Black
        '        btnCopy.BackColor = Color.Moccasin
        '        btnCopy.Enabled = True
        '        '
        '        cmbKaihatufugo.Enabled = True
        '        cmbEventPhaseName.Enabled = True
        '        txtEventName.Enabled = True
        '        cmbUnitKbn.Enabled = True
        '        txtEventName.Enabled = True
        '        '
        '        spdBaseCar.Enabled = True
        '    End If

        '    VisibleButton(BtnBaseCar)
        'End Sub
        '↓↓2014/10/29 酒井 ADD BEGIN
        'Ver6_2 1.95以降の修正内容の展開

        ' 2014/07/14 セルの文字列長制限をバイト単位で制限するためのロジックです
        Dim enc As System.Text.Encoding
        Dim WithEvents datamodelSpdBasicOption As FarPoint.Win.Spread.Model.DefaultSheetDataModel
        Dim WithEvents datamodelSpdSpecialOption As FarPoint.Win.Spread.Model.DefaultSheetDataModel
        Private Sub Frm9DispEventEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            enc = System.Text.Encoding.GetEncoding("shift-jis")
            datamodelSpdBasicOption = CType(spdBasicOption_Sheet1.Models.Data, FarPoint.Win.Spread.Model.DefaultSheetDataModel)
            datamodelSpdSpecialOption = CType(spdSpecialOption_Sheet1.Models.Data, FarPoint.Win.Spread.Model.DefaultSheetDataModel)

        End Sub
        Private Sub datamodelSpdBasicOption_Changed(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs) Handles datamodelSpdBasicOption.Changed
            If e.Row >= 0 And e.Row <= 2 Then
                Dim tCell As CellType.TextCellType = spdBasicOption_Sheet1.GetCellType(e.Row, e.Column)
                Dim s As String = datamodelSpdSpecialOption.GetValue(e.Row, e.Column)
                If s IsNot Nothing AndAlso enc.GetByteCount(s) > tCell.MaxLength Then
                    Dim chrs As Char() = enc.GetChars(enc.GetBytes(s), 0, tCell.MaxLength)
                    If Not Char.IsLetter(chrs(chrs.Length - 1)) Then
                        Array.Resize(chrs, chrs.Length - 1)
                    End If
                    datamodelSpdSpecialOption.SetValue(e.Row, e.Column, String.Concat(chrs))
                End If
            End If
        End Sub
        Private Sub datamodelSpdSpecialOption_Changed(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs) Handles datamodelSpdSpecialOption.Changed
            'If TypeOf spdSpecialOption_Sheet1.GetCellType(e.Row, e.Column) Is CellType.TextCellType Then
            If e.Row >= 0 And e.Row <= 2 Then
                Dim tCell As CellType.TextCellType = spdSpecialOption_Sheet1.GetCellType(e.Row, e.Column)
                Dim s As String = datamodelSpdSpecialOption.GetValue(e.Row, e.Column)
                If s IsNot Nothing AndAlso enc.GetByteCount(s) > tCell.MaxLength Then
                    Dim chrs As Char() = enc.GetChars(enc.GetBytes(s), 0, tCell.MaxLength)
                    If Not Char.IsLetter(chrs(chrs.Length - 1)) Then
                        Array.Resize(chrs, chrs.Length - 1)
                    End If
                    datamodelSpdSpecialOption.SetValue(e.Row, e.Column, String.Concat(chrs))
                End If
            End If
        End Sub
        '↑↑2014/10/29 酒井 ADD END
        Private Sub BtnBaseCar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBaseCar.Click
            VisibleButton(BtnBaseCar)
            '2012/01/09
            ForceSetActiveSpreadColor(GetActiveSheet(), 0)
        End Sub

        Private Sub BtnCompleteCar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCompleteCar.Click
            VisibleButton(BtnCompleteCar)
            '2012/01/09
            ForceSetActiveSpreadColor(GetActiveSheet(), 0)
        End Sub

        Private Sub VisibleButton(ByVal btn As Button)
            Me.spdBaseCar.Visible = False
            Me.spdCompleteCar.Visible = False
            Me.spdBasicOption.Visible = False
            Me.spdSpecialOption.Visible = False
            Me.spdBaseTenkaiCar.Visible = False
            Me.spdReferenceCar.Visible = False
            ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 e) (TES)施 ADD BEGIN
            ''↓↓2014/08/20 Ⅰ.5.EBOM差分出力 e) 酒井 ADD BEGIN
            'Me.spdEbomKanshi_Sheet1.Visible = False
            Me.spdEbomKanshi.Visible = False
            ''↑↑2014/08/20 Ⅰ.5.EBOM差分出力 e) 酒井 ADD END
            ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 e) (TES)施 ADD END

            Me.BtnBaseCar.BackColor = Color.LightCyan
            Me.BtnBaseCar.ForeColor = Color.Black
            Me.BtnCompleteCar.BackColor = Color.LightCyan
            Me.BtnCompleteCar.ForeColor = Color.Black
            Me.BtnBasicOption.BackColor = Color.LightCyan
            Me.BtnBasicOption.ForeColor = Color.Black
            Me.BtnSpecialOption.BackColor = Color.LightCyan
            Me.BtnSpecialOption.ForeColor = Color.Black
            Me.BtnBaseCarTenkai.BackColor = Color.LightCyan
            Me.BtnBaseCarTenkai.ForeColor = Color.Black
            ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 e) (TES)施 ADD BEGIN
            Me.btnEbomKanshi.BackColor = Color.LightCyan
            Me.btnEbomKanshi.ForeColor = Color.Black
            ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 e) (TES)施 ADD END
            Me.BtnOptionInquiry.Visible = False

            '製作一覧ボタンを表示
            '   発行№があり、新規作成モードか、発行№があり、設計展開前なら表示する。
            If StringUtil.IsNotEmpty(seisakuHakouNo) And IsAddMode() = True Or _
               StringUtil.IsNotEmpty(seisakuHakouNo) And Not headerSubject.IsSekkeiTenkaiIkou Then
                BtnSeisakuIchiranReference.Visible = True
                '製作一覧ボタンを表示する場合には
                '保存、コピー、EXCELなどのボタン押せない。
                btnSave.Visible = False
                btnCopy.Visible = False
                '   EXCELボタンは使用できる。
                'btnExcelExport.Visible = False
                'btnExcelImport.Visible = False
            Else
                BtnSeisakuIchiranReference.Visible = False
            End If

            If BtnBaseCar.Equals(btn) Then
                'ボタンのラベルを変更
                LblTitle.Visible = True
                LblTitle.Text = Me.BtnBaseCar.Text  'ボタンのテキストを設定する。
                Me.spdBaseCar.Visible = True
                btn.BackColor = Color.Yellow
                btn.ForeColor = Color.Black
                '行位置同期
                Me.spdBaseCar.SetViewportTopRow(0, RowNoScroll(True))
                'ElseIf BtnReference.Equals(btn) Then
                '    LblTitle.Visible = True
                '    LblTitle.Text = "【参考】ベース車情報"
                '    Me.spdReferenceCar.Visible = True
                '    '行位置同期
                '    Me.spdReferenceCar.SetViewportTopRow(0, RowNoScroll(True))
            ElseIf BtnBaseCarTenkai.Equals(btn) Then
                LblTitle.Visible = True
                LblTitle.Text = Me.BtnBaseCarTenkai.Text  'ボタンのテキストを設定する。
                Me.spdBaseTenkaiCar.Visible = True
                btn.BackColor = Color.Yellow
                btn.ForeColor = Color.Black
                '行位置同期
                Me.spdBaseTenkaiCar.SetViewportTopRow(0, RowNoScroll(True))
            ElseIf BtnCompleteCar.Equals(btn) Then
                LblTitle.Visible = True
                LblTitle.Text = "完成車情報"
                Me.spdCompleteCar.Visible = True
                btn.BackColor = Color.Yellow
                btn.ForeColor = Color.Black
                '行位置同期
                Me.spdCompleteCar.SetViewportTopRow(0, RowNoScroll(True))

            ElseIf BtnBasicOption.Equals(btn) Then
                LblTitle.Visible = True
                LblTitle.Text = "基本装備仕様"
                Me.spdBasicOption.Visible = True
                btn.BackColor = Color.Yellow
                btn.ForeColor = Color.Black
                Me.BtnOptionInquiry.Visible = True
                '行位置同期
                Me.spdBasicOption.SetViewportTopRow(0, RowNoScroll(False))

            ElseIf BtnSpecialOption.Equals(btn) Then
                LblTitle.Visible = True
                LblTitle.Text = "特別装備仕様"
                Me.spdSpecialOption.Visible = True
                btn.BackColor = Color.Yellow
                btn.ForeColor = Color.Black
                Me.BtnOptionInquiry.Visible = True
                '行位置同期
                Me.spdSpecialOption.SetViewportTopRow(0, RowNoScroll(False))
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 e) (TES)施 ADD BEGIN
            ElseIf btnEbomKanshi.Equals(btn) Then
                LblTitle.Visible = True
                LblTitle.Text = "設変監視情報"
                ''↓↓2014/08/20 Ⅰ.5.EBOM差分出力 e) 酒井 ADD BEGIN
                'Me.spdEbomKanshi_Sheet1.Visible = True
                Me.spdEbomKanshi.Visible = True
                ''↑↑2014/08/20 Ⅰ.5.EBOM差分出力 e) 酒井 ADD END
                btn.BackColor = Color.Yellow
                btn.ForeColor = Color.Black
                '行位置同期																																					
                Me.spdEbomKanshi.SetViewportTopRow(0, RowNoScroll(True))


                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 e) (TES)施 ADD END
            End If
        End Sub

        Private Sub BtnBasicOption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBasicOption.Click
            VisibleButton(BtnBasicOption)
            '2012/01/09
            ForceSetActiveSpreadColor(GetActiveSheet(), 0)
        End Sub

        Private Sub BtnSpecialOption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSpecialOption.Click
            VisibleButton(BtnSpecialOption)
            '2012/01/09
            ForceSetActiveSpreadColor(GetActiveSheet(), 0)
        End Sub

        Private Sub BtnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
            If Not aInputWatcher.WasUpdate Then
                Me.Close()
                Return
            End If
            If frm01Kakunin.ConfirmOkCancel("変更を更新せずに終了しますか？") = MsgBoxResult.Ok Then
                If subject.RegistError Then
                    _WasSaveRegister = False
                End If
                Me.Close()
                Return
            End If

            ''保存していない時だけ、警告を表示する。
            ''※項目を一つでも修正したら、保存済みフラグをクリアする予定。
            ''※何も変更していない場合は、警告は無し。
            'If txtDBStatus.Text <> "保存済み" Then
            '    If frm01Kakunin.ConfirmOkCancel("変更を更新せずに終了しますか？") = MsgBoxResult.Ok Then
            '        Me.Close()
            '    End If
            'Else
            '    Me.Close()
            'End If
        End Sub

        ''' <summary>
        ''' 装備仕様照会画面を表示する
        ''' </summary>
        ''' <param name="sender">Clickイベントに従う</param>
        ''' <param name="e">Clickイベントに従う</param>
        ''' <remarks></remarks>
        Private Sub BtnOptionInquiry_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOptionInquiry.Click

            EnabledButtonForInquiry(True)

            Dim frm As Frm9DispEventEditInquiry
            If IsActiveBasicOption() Then
                frm = Frm9DispEventEditInquiry.NewBasicInquiry(spdBasicOption, AddressOf Inquiry_FormClosed)
            ElseIf IsActiveSpecialOption() Then
                frm = Frm9DispEventEditInquiry.NewSpecialInquiry(spdSpecialOption, AddressOf Inquiry_FormClosed)
            Else
                Throw New InvalidOperationException("基本装備も特別装備も表示されていないのに、装備仕様照会画面を開こうとしています.")
            End If
            frm.Show()

        End Sub

        ''' <summary>
        ''' 装備仕様照会画面の終了時に行う処理
        ''' </summary>
        ''' <param name="sender">FormClosedEventhandlerに従う</param>
        ''' <param name="e">FormClosedEventhandlerに従う</param>
        ''' <remarks></remarks>
        Private Sub Inquiry_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)
            EnabledButtonForInquiry(False)
        End Sub

        ''' <summary>
        ''' 装備仕様照会画面の表示前後にボタン使用可否を制御する
        ''' </summary>
        ''' <param name="beforeShow">表示前の使用可否制御なら、true</param>
        ''' <remarks></remarks>
        Private Sub EnabledButtonForInquiry(ByVal beforeShow As Boolean)
            BtnBaseCar.Enabled = Not beforeShow
            BtnBaseCarTenkai.Enabled = Not beforeShow
            ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 m) (TES)施 ADD BEGIN
            btnEbomKanshi.Enabled = Not beforeShow
            ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 m) (TES)施 ADD END
            'BtnReference.Enabled = Not beforeShow
            BtnCompleteCar.Enabled = Not beforeShow
            BtnBasicOption.Enabled = Not beforeShow
            BtnSpecialOption.Enabled = Not beforeShow
            BtnOptionInquiry.Enabled = Not beforeShow

            btnExcelExport.Enabled = Not beforeShow
            If headerSubject.Enabled.BtnExcelImportEnabled Then
                btnExcelImport.Enabled = Not beforeShow
            End If

            btnRegister.Enabled = Not beforeShow
            btnSave.Enabled = Not beforeShow
            If headerSubject.Enabled.BtnCopyEnabled Then
                btnCopy.Enabled = Not beforeShow
            End If
        End Sub

        Private Function IsActiveBaseCar() As Boolean
            Return spdBaseCar.Visible
        End Function
        Private Function IsActiveCompleteCar() As Boolean
            Return spdCompleteCar.Visible
        End Function
        Private Function IsActiveSpecialOption() As Boolean
            Return spdSpecialOption.Visible
        End Function
        Private Function IsActiveBasicOption() As Boolean
            Return spdBasicOption.Visible
        End Function
        Private Function IsActiveBaseTenkaiCar() As Boolean
            Return spdBaseTenkaiCar.Visible
        End Function
        ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 g) (TES)施 ADD BEGIN
        Private Function IsActiveEbomKanshi() As Boolean
            Return spdEbomKanshi.Visible
        End Function
        ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 g) (TES)施 ADD END

        'Private Function IsActiveReferenceCar() As Boolean
        '    Return spdReferenceCar.Visible
        'End Function

        ''' <summary>
        ''' 現在表示されているSpreadのSheetを返す
        ''' </summary>
        ''' <returns>表示されているSheet</returns>
        ''' <remarks></remarks>
        Private Function GetActiveSheet() As FarPoint.Win.Spread.SheetView
            If IsActiveBaseCar() Then
                Return spdBaseCar_Sheet1
            ElseIf IsActiveCompleteCar() Then
                Return spdCompleteCar_Sheet1
            ElseIf IsActiveBasicOption() Then
                Return spdBasicOption_Sheet1
            ElseIf IsActiveSpecialOption() Then
                Return spdSpecialOption_Sheet1
            ElseIf IsActiveBaseTenkaiCar() Then
                Return spdBaseTenkaiCar_Sheet1
                'ElseIf IsActiveReferenceCar() Then
                '    Return spdReferenceCar_Sheet1
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力f) (TES)施 ADD BEGIN
            ElseIf IsActiveEbomKanshi() Then
                Return spdEbomKanshi_Sheet1
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 f) (TES)施 ADD END
            End If
            Throw New NotImplementedException("未対応の状態です.")
        End Function
        ''' <summary>
        ''' 現在表示されているSpreadのObserverを返す
        ''' </summary>
        ''' <returns>表示されているObserver</returns>
        ''' <remarks></remarks>
        Private Function GetActiveSpreadObserver() As Frm9SpdObserver
            If IsActiveBaseCar() Then
                Return baseCarObserver
            ElseIf IsActiveCompleteCar() Then
                Return completeCarObserver
            ElseIf IsActiveBasicOption() Then
                Return basicOptionObserver
            ElseIf IsActiveSpecialOption() Then
                Return specialOptionObserver
            ElseIf IsActiveBaseTenkaiCar() Then
                Return baseTenkaiCarObserver
                'ElseIf IsActiveReferenceCar() Then
                '    Return referenceCarObserver
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力n) (TES)施 ADD BEGIN
            ElseIf IsActiveEbomKanshi() Then
                Return ebomKanshiObserver

                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 n) (TES)施 ADD END
            End If
            Throw New NotImplementedException("未対応の状態です.")
        End Function

        Private _WasSaveRegister As Boolean
        ''' <summary>
        ''' 保存、または、登録をしたかを返す
        ''' </summary>
        ''' <returns>保存・登録をした場合、true</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WasSaveRegister() As Boolean
            Get
                Return _WasSaveRegister
            End Get
        End Property

        Private validatorHeaderSave As Validator
        ''' <summary>
        ''' 保存処理の入力チェックを初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitializeValidatorHeaderSave()

            validatorHeaderSave = New Validator()

            validatorHeaderSave.Add(cmbKaihatufugo, "開発符号").Required()
            validatorHeaderSave.Add(cmbEventPhaseName, "イベント").Required()
            validatorHeaderSave.Add(cmbUnitKbn, "ユニット区分").Required()
        End Sub

        Private validatorHeaderRegister As Validator
        ''' <summary>
        ''' 登録処理の入力チェックを初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitializeValidatorHeaderRegister()

            validatorHeaderRegister = New Validator()

            Dim headerRequired As New Validator("ヘッダー情報を全て入力してください。")
            headerRequired.Add(cmbKaihatufugo).Required()
            headerRequired.Add(cmbEventPhaseName).Required()
            headerRequired.Add(txtEventName).Required()
            headerRequired.Add(cmbUnitKbn).Required()
            headerRequired.Add(dtpSeisakuJikiFrom).Required()
            headerRequired.Add(dtpSeisakuJikiTo).Required()
            headerRequired.Add(txtHakouNo).Required()
            headerRequired.Add(txtHakouNoKai).Required()
            headerRequired.Add(cmbHachuUmu).Required()
            headerRequired.Add(cmbJikyuUmu).Required()          '2012/01/10

            validatorHeaderRegister.Add(headerRequired)

            'validatorHeaderRegister.Add(cmbKaihatufugo, "開発符号").Required(requiredMsg)
            'validatorHeaderRegister.Add(cmbEventPhaseName, "イベント").Required(requiredMsg)
            'validatorHeaderRegister.Add(txtEventName, "イベント名称").Required(requiredMsg)
            'validatorHeaderRegister.Add(cmbUnitKbn, "ユニット区分").Required(requiredMsg)
            'validatorHeaderRegister.Add(dtpSeisakuJikiFrom, "制作時期From").Required(requiredMsg)
            'validatorHeaderRegister.Add(dtpSeisakuJikiTo, "制作時期To").Required(requiredMsg)
            Dim jikiFrom As ControlAccessor(Of DateTime) = validatorHeaderRegister.Add(dtpSeisakuJikiFrom).Accessor
            Dim jikiTo As ControlAccessor(Of DateTime) = validatorHeaderRegister.Add(dtpSeisakuJikiTo).Accessor
            validatorHeaderRegister.Collaborate.FromTo(Of DateTime)(jikiFrom, jikiTo, "製作時期へ正しい範囲を入力してください。")
            'validatorHeaderRegister.Add(txtHakouNo, "制作一覧発行No").Required(requiredMsg)
            validatorHeaderRegister.Add(txtHakouNoKai, "制作一覧発行No改").Numeric()
            'validatorHeaderRegister.Add(cmbHachuUmu, "発注有無").Required(requiredMsg)
        End Sub
        Private errorController As New ErrorController()
        Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
            Try

                errorController.ClearBackColor()

                validatorHeaderRegister.AssertValidate()

                'SPREADのエラーチェック前にも表示値を更新してみる。
                UpdateObserver(subject, Nothing)

                baseCarObserver.AssertValidateRegister()
                completeCarObserver.AssertValidateRegister()
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 o) (TES)施 ADD BEGIN
                ebomKanshiObserver.AssertValidateRegister()

                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 o) (TES)施 ADD END

                ''2012/03/13 装備仕様に同じ名称があるかどうかをチェック
                If Not basicOptionObserver.AssertValidateRegister() Then
                    Return
                End If
                If Not specialOptionObserver.AssertValidateRegister() Then
                    Return
                End If


                'データが一件も登録されていないときのエラーを追加
                Dim ht As New Hashtable
                Dim duplicateGoshaRowNos(spdBaseCar_Sheet1.RowCount) As Integer
                Dim j As Integer = 0
                Dim wGousyaCount As Integer = 0
                For rowNo As Integer = 3 To spdBaseCar_Sheet1.RowCount - 1
                    spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).ResetBackColor()
                    '号車チェックします。
                    If Not StringUtil.IsEmpty( _
                           spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value) Then
                        '2012/02/08　重複した号車のチェック
                        If ht.ContainsKey(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value) Then
                            'エラーメッセージ
                            duplicateGoshaRowNos(j) = rowNo
                            j = j + 1
                            duplicateGoshaRowNos(j) = Integer.Parse(ht(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value))
                            j = j + 1
                        Else
                            '2012/02/08　重複した号車のチェック
                            ht.Add(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value, rowNo)
                        End If
                        '号車の数をカウントする。
                        wGousyaCount += 1
                    End If
                Next
                If wGousyaCount = 0 Then
                    'エラーメッセージ
                    ComFunc.ShowErrMsgBox("号車が１件も登録されていません")
                    Return
                End If
                If j > 0 Then
                    For i As Integer = 0 To j - 1
                        spdBaseCar_Sheet1.Cells(duplicateGoshaRowNos(i), spdBaseCar_Sheet1.Columns("GOSHA").Index).BackColor = ERROR_COLOR
                    Next
                    ComFunc.ShowErrMsgBox("同じ名称を持つ号車が２件以上登録されています")
                    Return
                End If
            Catch ex As IllegalInputException
                errorController.SetBackColorOnError(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)
                ''エラーメッセージ
                ComFunc.ShowErrMsgBox(ex.Message)
                Return
            End Try
            If frm01Kakunin.ConfirmOkCancel("登録を実行しますか？") <> MsgBoxResult.Ok Then
                Return
            End If
            Try
                '通常登録処理
                subject.Register()

                '設計展開以降のイベントならメール送信画面を表示
                If headerSubject.IsSekkeiTenkaiIkou Then

                    'ここで差分EXCELを作る処理を行う。
                    Dim strFileName As String = ""
                    Dim strMsg As String = "前回登録時点からの変更点を抽出します。" & vbLf & _
                                           "エクセルファイルの保存先を選択してください。" & vbLf & _
                                           "尚、設計への変更点お知らせメールを作成します。"
                    strFileName = EventExcelOutput("［登録］", strMsg)

                    If StringUtil.IsNotEmpty(strFileName) Then
                        Call autoMailSend(strFileName)
                    End If

                    'ここで登録時のデータを更新する。
                    subject.RegisterKaitei()

                    '作った差分EXCELはメールへ添付する。
                    '　保存先はどこにする？試作手配システムのデフォルトフォルダにするか？
                    '   「D:\新試作手配システム\Excel出力一時フォルダ」
                    'メール送信画面表示
                    '   お知らせ通知有の時だけ表示する。
                    '   フラグは見ない。2/13森さんと確認。
                    'If StringUtil.Equals(headerSubject.InfoMailFlg, "1") Then
                    'End If
                Else
                    'ここで登録時のデータを更新する。
                    subject.RegisterKaitei()
                End If

            Catch ex As TableExclusionException
                ComFunc.ShowInfoMsgBox(ex.Message)
            Finally
                _WasSaveRegister = True ' 排他例外だったとしても、誰かが更新したから
            End Try

            If Not subject.RegistError Then
                Me.Close()
            End If

        End Sub

        Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

            Try
                errorController.ClearBackColor()

                validatorHeaderSave.AssertValidate()
            Catch ex As IllegalInputException
                errorController.SetBackColorOnError(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)
                ''エラーメッセージ
                ComFunc.ShowErrMsgBox(ex.Message)
                Return
            End Try

            ''2013/07/01 保存時にも装備仕様に同じ名称があるかどうかをチェック
            If Not basicOptionObserver.AssertValidateRegister() Then
                Return
            End If
            If Not specialOptionObserver.AssertValidateRegister() Then
                Return
            End If

            If frm01Kakunin.ConfirmOkCancel("保存を実行しますか？") <> MsgBoxResult.Ok Then
                Return
            End If
            Try
                ShisakuCommon.Ui.CursorUtil.SetWaitCursor(1000)
                subject.Save()

                If Not subject.RegistError Then

                    'ここで差分EXCELを作る処理を行う。
                    '   設計展開以降のイベントなら差分EXCELを出力
                    If headerSubject.IsSekkeiTenkaiIkou Then
                        Dim strFileName As String = ""
                        Dim strMsg As String = "前回登録時点からの変更点を抽出します。" & vbLf & _
                                               "エクセルファイルの保存先を選択してください。"
                        strFileName = EventExcelOutput("［保存］", strMsg)
                    End If

                    baseCarObserver.ReInitialize()
                    ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 p) (TES)施 ADD BEGIN
                    ebomKanshiObserver.ReInitialize()
                    ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 p) (TES)施 ADD END
                    headerSubject.NotifyObservers() ''ヘッダー再表示(データ区分名)
                    aInputWatcher.Clear()
                Else

                End If
            Catch ex As TableExclusionException
                ComFunc.ShowInfoMsgBox(ex.Message)
            Finally
                _WasSaveRegister = True ' 排他例外だったとしても、誰かが更新したから
            End Try
            If Not subject.RegistError Then

                ComFunc.ShowInfoMsgBox("保存が完了しました。")

                'Me.Close()
                '登録ボタンを表示する。
                btnRegister.Visible = True
            End If
        End Sub

        ''' <summary>
        ''' Excelファイルに出力
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnEXCELimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelExport.Click
            Try
                Cursor.Current = Cursors.WaitCursor

                '「参考」データを取得
                'subject.ReferenceCarSubject.Apply()
                'subject.ReferenceCarSubject.NotifyObservers()

                Dim getSeisakuIchiranHd As New SeisakuIchiranDaoImpl
                Dim tSeisakuHakouHdVo As New TSeisakuHakouHdVo
                tSeisakuHakouHdVo = getSeisakuIchiranHd.GetSeisakuIchiranHd(seisakuHakouNo, seisakuHakouNoKaiteiNo)

                Dim export2Excel As New EventEdit2Excel(subject, LoginInfo.Now, _
                                                        spdCompleteCar_Sheet1.Cells(2, spdCompleteCar_Sheet1.Columns("EG_MEMO_1").Index).Value, _
                                                        spdCompleteCar_Sheet1.Cells(2, spdCompleteCar_Sheet1.Columns("EG_MEMO_2").Index).Value, _
                                                        spdCompleteCar_Sheet1.Cells(2, spdCompleteCar_Sheet1.Columns("TM_MEMO_1").Index).Value, _
                                                        spdCompleteCar_Sheet1.Cells(2, spdCompleteCar_Sheet1.Columns("TM_MEMO_2").Index).Value)
                export2Excel.Execute()

                '一旦イベントを上げてみる。
                System.Windows.Forms.Application.DoEvents()

                'ここ追加してみた。
                export2Excel.OpenExcelOnScreen()
            Catch ex As Exception
                MsgBox("Excelの書込みに失敗しました。", MsgBoxStyle.Information, "エラー")
            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Sub
        ' Excelファイルへシートのデータを保存します。
        Sub SaveExcelFile(ByVal filename As String)

            Dim ret As Boolean

            'Label1.Text = filename
            'Label1.Text += "にExcelファイルのエクスポートを実行しました。"

            '' Execlファイルへデータを保存します。
            'Try
            ' Excelファイルへエクスポートします。
            ret = spdCompleteCar.SaveExcel(filename)
            '    Label1.ForeColor = Color.Black

            '    If ret = False Then
            '        Label1.ForeColor = Color.Red
            '        Label1.Text = "エラー：ファイルへ保存できません。ファイルパス：" & filename
            '    End If

            'Catch ex As Exception
            '    ' エラーが発生した場合にメッセージを表示します。
            '    Label1.ForeColor = Color.Red
            '    Label1.Text = ex.Message.ToString
            'End Try

        End Sub
        ' Spread XML ファイルへシートのデータを保存します。
        Sub SaveSpreadFile(ByVal filename As String)

            Dim ret As Boolean

            'Label1.Text = filename
            'Label1.Text += "にSpread XML ファイルのエクスポートを実行しました。"

            '' Spread XML ファイルへデータを保存します。
            'Try
            ret = spdCompleteCar.Save(filename, False)
            'Label1.ForeColor = Color.Black

            'If ret = False Then
            '    Label1.ForeColor = Color.Red
            '    Label1.Text = "エラー：ファイルへ保存できません。ファイルパス：" & filename
            'End If

            'Catch ex As Exception
            '    ' エラーが発生した場合にメッセージを表示します。
            '    Label1.ForeColor = Color.Red
            '    Label1.Text = ex.Message.ToString
            'End Try

        End Sub

        Private Class ExcelImportCloser : Inherits CopyFormCloser
            Public Sub New(ByVal nowSubject As Logic.EventEdit, _
                           ByVal bakSubject As Logic.EventEdit, _
                           ByVal supersedeSubject As SupersedeSubjectDelegate)
                MyBase.New(nowSubject, bakSubject, supersedeSubject)
            End Sub
        End Class

        ''' <summary>
        ''' Excelファイルから入力
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnExcelImport_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelImport.Click

            ' 無理矢理 setChanged を動かさせる. TODO 他のスマートな方法を探す
            subject.IsViewerMode = Not subject.IsViewerMode

            Dim bakSubject As Logic.EventEdit = subject

            Dim newSubject As Logic.EventEdit = New Logic.EventEdit(subject.ShisakuEventCode, _
                                                                    LoginInfo.Now, _
                                                                    True, _
                                                                    subject.SeisakuHakouNo, _
                                                                    subject.SeisakuHakouNoKaiteiNo, _
                                                                    bakSubject.HeaderSubject)
            'Try
            Dim executed As Boolean = False
            Dim import2EventEdit As New ImportFromExcel.Excel2EventEdit(newSubject)
            executed = import2EventEdit.Execute()
            'Try
            '    Dim import2EventEdit As New ImportFromExcel.Excel2EventEdit(newSubject)
            '    executed = import2EventEdit.Execute()
            'Catch ex As Exception
            '    MsgBox("Excelの読み込みに失敗しました。", MsgBoxStyle.Information, "エラー")
            'End Try


            If (executed) Then
                SupersedeSubject(newSubject)
                Dim closer As New ExcelImportCloser(newSubject, bakSubject, AddressOf SupersedeSubject)
                frm00Kakunin.ConfirmShow("EXCEL取込の確認", _
                                         "EXCELの情報を確認してください。", _
                                         "EXCELデータを反映しますか？", _
                                         "EXCELを反映", _
                                         "取込前に戻す", closer)
            End If
            'Catch ex As Exception
            'ComFunc.ShowErrMsgBox(E0029)
            'Me.SupersedeSubject(bakSubject)
            'End Try

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

        End Sub

        ' Excelファイルよりシートのデータを読み込みます。
        Sub OpenExcelFile(ByVal filename As String)

            'Dim ret As Boolean
            Dim newfilepath As String

            newfilepath = pathname & filename
            'Label1.Text = newfilepath
            'Label1.Text += "からExcelファイルのインポートを実行しました。"

            ' Execlファイルよりデータを読み込みます。
            'Try
            ' Excelファイルからインポートを行います。

            'ret = spdParts2.OpenExcel(newfilepath)

            'Label1.ForeColor = Color.Black

            'If ret = False Then
            '    Label1.ForeColor = Color.Red
            '    Label1.Text = "エラー：ファイルを開けません。ファイルパス：" & newfilepath
            'End If

            'Catch ex As Exception
            '    ' エラーが発生した場合にメッセージを表示します。
            '    Label1.ForeColor = Color.Red
            '    Label1.Text = ex.Message.ToString
            'End Try

        End Sub

        Private Delegate Sub SupersedeSubjectDelegate(ByVal newSubject As Logic.EventEdit)

        Private Class CopyFormCloser : Implements frm00Kakunin.IFormCloser
            Private ReadOnly nowSubject As Logic.EventEdit
            Private ReadOnly bakSubject As Logic.EventEdit
            Private ReadOnly supersedeSubject As SupersedeSubjectDelegate
            Public Sub New(ByVal nowSubject As Logic.EventEdit, ByVal bakSubject As Logic.EventEdit, ByVal supersedeSubject As SupersedeSubjectDelegate)
                Me.bakSubject = bakSubject
                Me.nowSubject = nowSubject
                '' 外側のクラスのインスタンスメソッドを呼べないから、Delegateで。
                Me.supersedeSubject = supersedeSubject
            End Sub
            ''' <summary>
            ''' フォームを閉じる時の処理
            ''' </summary>
            ''' <param name="IsOk">OKが押された場合、true</param>
            ''' <remarks></remarks>
            Public Sub FormClose(ByVal IsOk As Boolean) Implements frm00Kakunin.IFormCloser.FormClose
                If IsOk Then
                    nowSubject.IsViewerMode = False
                    nowSubject.NotifyObservers()
                    Return
                End If
                supersedeSubject(bakSubject)
            End Sub
        End Class

        Private Sub BtnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click

            Using frm As New Frm10DispEventCopy
                ' 無理矢理 setChanged を動かさせる. TODO 他のスマートな方法を探す
                subject.IsViewerMode = Not subject.IsViewerMode

                frm.ShowDialog()
                If frm.SelectedEventCode Is Nothing Then
                    Return
                End If

                Dim bakSubject As Logic.EventEdit = subject

                '' コピー情報を画面に表示
                Dim newSubject As Logic.EventEdit = New Logic.EventEdit(frm.SelectedEventCode, _
                                                                        LoginInfo.Now, _
                                                                        True, _
                                                                        Nothing, _
                                                                        Nothing, _
                                                                        bakSubject.HeaderSubject)
                SupersedeSubject(newSubject)

                Dim closer As New CopyFormCloser(newSubject, bakSubject, AddressOf SupersedeSubject)
                frm00Kakunin.ConfirmShow("コピー後の確認", _
                                         "コピーの情報を確認してください。", _
                                         "コピーのデータを反映しますか？", _
                                         "コピーを反映", _
                                         "コピー前に戻す", closer)
            End Using

        End Sub

        ''' <summary>
        ''' Subjectを差し替える
        ''' </summary>
        ''' <param name="newSubject">新しいSubject</param>
        ''' <remarks></remarks>
        Private Sub SupersedeSubject(ByVal newSubject As Logic.EventEdit)

            subject = newSubject
            headerSubject = subject.HeaderSubject
            subject.AddObserver(Me)
            headerSubject.AddObserver(Me)

            baseCarObserver.SupersedeSubject(subject.BaseCarSubject)
            ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 q) (TES)施 ADD BEGIN
            ebomKanshiObserver.SupersedeSubject(subject.EbomKanshiSubject)
            ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 q) (TES)施 ADD END
            completeCarObserver.SupersedeSubject(subject.CompleteCarSubject)
            basicOptionObserver.SupersedeSubject(subject.BasicOptionSubject)
            specialOptionObserver.SupersedeSubject(subject.SpecialOptionSubject)
            baseTenkaiCarObserver.SupersedeSubject(subject.BaseTenkaiCarSubject)
            'referenceCarObserver.SupersedeSubject(subject.ReferenceCarSubject)

            ' 無理矢理 setChanged を動かさせる. TODO 他のスマートな方法を探す
            subject.IsViewerMode = Not subject.IsViewerMode

            subject.ApplyBaseCarShubetsuGosha()
            subject.NotifyObservers()
        End Sub

        Private Sub btnCOLOR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCOLOR.Click

            Dim cr As FarPoint.Win.Spread.Model.CellRange = GetActiveSheet.GetSelection(0)
            Dim row As Integer = 0
            'SPREADが選択されていなければスルー。
            If StringUtil.IsNotEmpty(cr) Then
                If cr.Row < 2 Then
                    row = 3
                Else
                    row = cr.Row
                End If
                ''「色パレット」表示ボタンの近くに（真下の辺り）表示する。
                'frmZColorMarker.ShowUnderButton(GetActiveSheet(), row, btnCOLOR)
                frmZColorMarker.ShowUnderButton(GetActiveSheet(), btnCOLOR)
                '2012/01/09
                ForceSetActiveSpreadColor(GetActiveSheet(), 0)
            End If

        End Sub

        Private Sub BtnReference_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReference.Click
            VisibleButton(BtnReference)
            '2012/01/09
            ForceSetActiveSpreadColor(GetActiveSheet(), 0)
        End Sub

        Private Sub btnColorCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnColorCLEAR.Click
            GetActiveSpreadObserver.ClearSheetBackColor()
            '2012/01/09
            ForceSetActiveSpreadColor(GetActiveSheet(), 0)
        End Sub

        '2012/01/09
        ''' <summary>
        ''' 行背景色を強制的にグレーに変更する(種別が "D" の場合)
        ''' </summary>
        ''' <param name="activeSheet">色を設定するSpreadSheet</param>
        ''' <param name="targetColumn">種別のカラム位置</param>
        ''' <remarks></remarks>
        Private Sub ForceSetActiveSpreadColor(ByVal activeSheet As SheetView, ByVal targetColumn As Long)
            Const DEF_TYPE_D As String = "D"
            Const DEF_TYPE_H As String = "種別"
            Dim nRow As Long
            Dim nColumn As Long = 0
            Dim nColumn2 As Long = 0
            Dim IsHeader As Boolean = False
            Dim dataStartRow As Long

            If activeSheet.Equals(spdBaseCar_Sheet1) Or _
                activeSheet.Equals(spdCompleteCar_Sheet1) Or _
                  activeSheet.Equals(spdEbomKanshi_Sheet1) Or _
                 activeSheet.Equals(spdBaseTenkaiCar_Sheet1) Then
                'activeSheet.Equals(spdReferenceCar_Sheet1) Then
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力ｈ) (TES)施 ADD BEGIN
                'activeSheet.Equals(spdEbomKanshi_Sheet1) Or _
                '↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ｈ) (TES)施 ADD END
                dataStartRow = 2
            Else
                dataStartRow = 0
            End If
            For nRow = dataStartRow To activeSheet.RowCount - 1
                If activeSheet.Cells(nRow, targetColumn, nRow, targetColumn).Text = DEF_TYPE_H Then
                    IsHeader = True
                Else
                    If IsHeader = True Then
                        If activeSheet.ColumnCount - 1 > 0 Then
                            nColumn2 = activeSheet.ColumnCount - 1
                        End If
                        If activeSheet.Cells(nRow, targetColumn, nRow, targetColumn).Text = DEF_TYPE_D Then
                            activeSheet.Cells(nRow, nColumn, nRow, nColumn2).BackColor = Color.DimGray
                            activeSheet.Cells(nRow, nColumn, nRow, nColumn2).ForeColor = Color.LightGray
                        Else
                            'TODO : 色の判断以外で行えないか？
                            If activeSheet.Cells(nRow, nColumn, nRow, nColumn2).BackColor = Color.DimGray Then
                                activeSheet.Cells(nRow, nColumn, nRow, nColumn2).ResetBackColor()
                                activeSheet.Cells(nRow, nColumn, nRow, nColumn2).ResetForeColor()
                            End If
                        End If
#If DEBUG Then
                        Debug.Print("  (" & nRow & "," & ")=[" & activeSheet.Cells(nRow, 0).Value & "]")
                        Debug.Print("  (" & nRow & "," & ")=[" & activeSheet.Cells(nRow, 1).Value & "]")
                        Debug.Print("  (" & nRow & "," & ")=[" & activeSheet.Cells(nRow, 2).Value & "]")
                        Debug.Print("  (" & nRow & "," & ")=[" & activeSheet.Cells(nRow, 3).Value & "]")

#End If

                    End If
                End If
            Next
        End Sub

        Private ReadOnly baseCarObserver As SpdBaseCarObserver
        Private ReadOnly completeCarObserver As SpdCompleteCarObserver
        Private ReadOnly basicOptionObserver As SpdOptionObserver
        Private ReadOnly specialOptionObserver As SpdOptionObserver
        'Private ReadOnly referenceCarObserver As SpdReferenceCarObserver
        Private ReadOnly baseTenkaiCarObserver As SpdBaseTenkaiCarObserver
        ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 r) (TES)施 ADD BEGIN
        Private ReadOnly ebomKanshiObserver As SpdEbomKanshiObserver
        ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 r) (TES)施 ADD END

        Private subject As Logic.EventEdit
        Private headerSubject As Logic.EventEditHeader
        Private ReadOnly inputSupport As ShisakuInputSupport
        Private ReadOnly aInputWatcher As InputWatcher
        Private ReadOnly enabler As New ShisakuEnabler

        'ベース車情報
        Private seisakuHakouNo As String
        Private seisakuHakouNoKaiteiNo As String

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Me.New(Nothing, Nothing, Nothing)
        End Sub

        ''' <summary>
        ''' コンストラクタ（編集モード）
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="seisakuHakouNo">製作一覧発行№</param>
        ''' <param name="seisakuHakouNoKaiteiNo">製作一覧改訂№</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, ByVal seisakuHakouNo As String, ByVal seisakuHakouNoKaiteiNo As String)

            '処理中画面表示
            Dim SyorichuForm As New frm03Syorichu
            SyorichuForm.Execute()
            SyorichuForm.Show()

            Application.DoEvents()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)

            Me.seisakuHakouNo = seisakuHakouNo
            Me.seisakuHakouNoKaiteiNo = seisakuHakouNoKaiteiNo

            '変数クリア
            ParaBasicOptionCol = 0
            ParaBasicOptionFlg = ""
            ParaOptionCol = 0
            ParaOptionFlg = ""

            subject = New Logic.EventEdit(shisakuEventCode, LoginInfo.Now, False, seisakuHakouNo, seisakuHakouNoKaiteiNo)
            headerSubject = subject.HeaderSubject
            subject.AddObserver(Me)
            headerSubject.AddObserver(Me)

            baseCarObserver = New SpdBaseCarObserver(spdBaseCar, subject.BaseCarSubject)
            completeCarObserver = New SpdCompleteCarObserver(spdCompleteCar, subject.CompleteCarSubject)
            '設計展開用
            baseTenkaiCarObserver = New SpdBaseTenkaiCarObserver(spdBaseTenkaiCar, subject.BaseTenkaiCarSubject)

            ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 s) (TES) ADD BEGIN
            ebomKanshiObserver = New SpdEbomKanshiObserver(spdEbomKanshi, subject.EbomKanshiSubject)
            ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 s) (TES)施 ADD END
            Dim maxBasicColumn As Integer = 0
            For Each index As Integer In subject.BasicOptionSubject.GetInputTitleNameColumnNos
                If maxBasicColumn < index Then
                    maxBasicColumn = index
                End If
            Next

            'ここで初期値が多いとエラーが発生?'
            If BASIC_OPTION_COLUMN_COUNT < maxBasicColumn Then
                'そのままだとエラーなので'
                basicOptionObserver = New SpdOptionObserver(spdBasicOption, subject.BasicOptionSubject, maxBasicColumn + 1)
            Else
                basicOptionObserver = New SpdOptionObserver(spdBasicOption, subject.BasicOptionSubject, BASIC_OPTION_COLUMN_COUNT)
            End If

            Dim maxSpecialColumn As Integer = 0
            For Each sindex As Integer In subject.SpecialOptionSubject.GetInputTitleNameColumnNos
                If maxSpecialColumn < sindex Then
                    maxSpecialColumn = sindex
                End If
            Next
            'ここで初期値が多いとエラーが発生？
            If SPECIAL_OPTION_COLUMN_COUNT < maxSpecialColumn Then
                'そのままだとエラーなので'
                specialOptionObserver = New SpdOptionObserver(spdSpecialOption, subject.SpecialOptionSubject, maxSpecialColumn + 1)
            Else
                specialOptionObserver = New SpdOptionObserver(spdSpecialOption, subject.SpecialOptionSubject, SPECIAL_OPTION_COLUMN_COUNT)
            End If


            'referenceCarObserver = New SpdReferenceCarObserver(spdReferenceCar, subject.ReferenceCarSubject)


            ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 s) (TES)施 ADD BEGIN
            inputSupport = New ShisakuInputSupport(txtInputSupport, spdBaseCar, spdBaseTenkaiCar, spdReferenceCar, spdCompleteCar, spdBasicOption, spdSpecialOption, spdEbomKanshi)
            ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 s) (TES)施 ADD END
            aInputWatcher = New InputWatcher

            InitializeWatcher()
            InitializeHeader()
            InitializeSpread()

            InitializeValidatorHeaderRegister()
            InitializeValidatorHeaderSave()

            subject.ApplyBaseCarShubetsuGosha()

            subject.NotifyObservers()

            'ボタンのラベル及び位置を変更
            '   設計展開前後で名称を入れ替える。
            If headerSubject.IsSekkeiTenkaiIkou Then
                Me.BtnBaseCar.Text = "製作一覧ベース車"
                Me.BtnBaseCarTenkai.Text = "部品表ベース車"
                Me.BtnBaseCar.Location = New Point(110, 70)
                Me.BtnBaseCarTenkai.Location = New Point(4, 70)
            Else
                Me.BtnBaseCar.Text = "部品表ベース車"
                Me.BtnBaseCarTenkai.Text = "製作一覧ベース車"
                Me.BtnBaseCar.Location = New Point(4, 70)
                Me.BtnBaseCarTenkai.Location = New Point(110, 70)
            End If

            VisibleButton(BtnBaseCar)

            aInputWatcher.Clear()

            '処理中画面非表示
            SyorichuForm.Close()
        End Sub

        Private Sub InitializeWatcher()

            aInputWatcher.Add(cmbKaihatufugo)
            aInputWatcher.Add(cmbEventPhaseName)
            aInputWatcher.Add(txtEventName)
            aInputWatcher.Add(cmbUnitKbn)
            aInputWatcher.Add(dtpSeisakuJikiFrom)
            aInputWatcher.Add(dtpSeisakuJikiTo)
            aInputWatcher.Add(txtHakouNo)
            aInputWatcher.Add(txtHakouNoKai)
            aInputWatcher.Add(cmbHachuUmu)
            aInputWatcher.Add(cmbJikyuUmu)      '2012/01/07
            aInputWatcher.Add(spdBaseCar)
            aInputWatcher.Add(spdCompleteCar)
            aInputWatcher.Add(spdBasicOption)
            aInputWatcher.Add(spdSpecialOption)
            aInputWatcher.Add(spdEbomKanshi)

        End Sub

        Public ReadOnly Property ShisakuEventCode() As String
            Get
                Return subject.ShisakuEventCode
            End Get
        End Property

        ''' <summary>
        ''' 表示値を更新する
        ''' </summary>
        ''' <param name="observable">呼び出し元のObservable</param>
        ''' <param name="arg">引数</param>
        ''' <remarks></remarks>
        Public Sub UpdateObserver(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update
            If observable Is headerSubject Then
                UpdateHeader(observable, arg)
            Else
                headerSubject.NotifyObservers(arg)
                subject.BaseCarSubject.NotifyObservers(arg)
                subject.CompleteCarSubject.NotifyObservers(arg)
                subject.BasicOptionSubject.NotifyObservers(arg)
                subject.SpecialOptionSubject.NotifyObservers(arg)
                subject.BaseTenkaiCarSubject.NotifyObservers(arg)
                subject.EbomKanshiSubject.NotifyObservers(arg)

            End If
        End Sub

        ''' <summary>
        ''' ヘッダー部の表示値を更新する
        ''' </summary>
        ''' <param name="observable">呼び出し元Observable</param>
        ''' <param name="arg">引数</param>
        ''' <remarks></remarks>
        Private Sub UpdateHeader(ByVal observable As Observable, ByVal arg As Object)
            IsSuspendEventValueChanged = True
            Try
                FormUtil.BindLabelValuesToComboBox(cmbKaihatufugo, headerSubject.ShisakuKaihatsuFugoLabelValues, True)
                'マスタに存在する値だけセットされれば良いのか？ 
                FormUtil.SetComboBoxSelectedValue(cmbKaihatufugo, headerSubject.ShisakuKaihatsuFugo)

                FormUtil.BindLabelValuesToComboBox(cmbEventPhaseName, headerSubject.ShisakuEventPhaseNameLabelValues, True)
                'マスタに存在する値だけセットされれば良いのか？ 
                FormUtil.SetComboBoxSelectedValue(cmbEventPhaseName, headerSubject.ShisakuEventPhaseName)

                txtEventName.Text = headerSubject.ShisakuEventName

                FormUtil.BindLabelValuesToComboBox(cmbUnitKbn, headerSubject.UnitKbnLabelValues, True)
                FormUtil.SetComboBoxSelectedValue(cmbUnitKbn, headerSubject.UnitKbn)

                dtpSeisakuJikiFrom.Value = headerSubject.SeisakujikiFrom
                dtpSeisakuJikiTo.Value = headerSubject.SeisakujikiTo
                'コピーがうまく言ってない 樺澤 '
                txtKanseisya.Text = headerSubject.SeisakudaisuKanseisya
                txtWb.Text = headerSubject.SeisakudaisuWb
                txtChushi.Text = headerSubject.SeisakudaisuChushi
                txtHakouNo.Text = headerSubject.SeisakuichiranHakouNo
                txtHakouNoKai.Text = headerSubject.SeisakuichiranHakouNoKai

                FormUtil.BindLabelValuesToComboBox(cmbHachuUmu, headerSubject.HachuUmuLabelValues, True)
                FormUtil.SetComboBoxSelectedValue(cmbHachuUmu, headerSubject.HachuUmu)

                '2011/01/07
                FormUtil.BindLabelValuesToComboBox(cmbJikyuUmu, headerSubject.JikyuUmuLabelValues, True)
                FormUtil.SetComboBoxSelectedValue(cmbJikyuUmu, headerSubject.JikyuUmu)

                txtStatus.Text = headerSubject.StatusName
                txtDBStatus.Text = headerSubject.DataKbnName

                txtSyozoku.Text = LoginInfo.Now.BukaRyakuName
                txtTantosya.Text = LoginInfo.Now.ShainName

                '登録ボタンが使用できるか？
                If StringUtil.Equals(headerSubject.SaveFlg, "1") Then
                    '但し製作一覧の改訂情報を参照した場合には「保存済み」であっても使用できない。
                    If StringUtil.IsEmpty(seisakuHakouNo) Then
                        btnRegister.Visible = True
                    Else
                        btnRegister.Visible = False
                    End If
                Else
                    btnRegister.Visible = False
                End If

                LockHeaderIfViewerChange()
            Finally
                IsSuspendEventValueChanged = False
            End Try
        End Sub

        Private backIsViewer As Boolean?
        Private Sub LockHeaderIfViewerChange()

            If headerSubject.IsViewerMode Then
                enabler.SettingEnabled(cmbKaihatufugo, False)
                enabler.SettingEnabled(cmbEventPhaseName, False)
                enabler.SettingEnabled(txtEventName, False)
                enabler.SettingEnabled(cmbUnitKbn, False)
                enabler.SettingEnabled(dtpSeisakuJikiFrom, False)
                enabler.SettingEnabled(dtpSeisakuJikiTo, False)
                enabler.SettingEnabled(txtHakouNo, False)
                enabler.SettingEnabled(txtHakouNoKai, False)
                enabler.SettingEnabled(cmbHachuUmu, False)

                enabler.SettingEnabled(btnCOLOR, False)
                enabler.SettingEnabled(btnColorCLEAR, False)
                enabler.SettingEnabled(btnExcelExport, False)
                enabler.SettingEnabled(btnExcelImport, False)
                enabler.SettingEnabled(btnBACK, False)
                enabler.SettingEnabled(btnRegister, False)
                enabler.SettingEnabled(btnSave, False)
                enabler.SettingEnabled(btnCopy, False)

                enabler.SettingEnabled(BtnSeisakuIchiranReference, False)
            Else
                enabler.SettingEnabled(cmbKaihatufugo, headerSubject.Enabled.ShisakuKaihatsuFugoEnabled)
                enabler.SettingEnabled(cmbEventPhaseName, headerSubject.Enabled.ShisakuEventPhaseNameEnabled)
                enabler.SettingEnabled(txtEventName, headerSubject.Enabled.ShisakuEventNameEnabled)
                enabler.SettingEnabled(cmbUnitKbn, headerSubject.Enabled.UnitKbnEnabled)
                enabler.SettingEnabled(dtpSeisakuJikiFrom, True)
                enabler.SettingEnabled(dtpSeisakuJikiTo, True)
                enabler.SettingEnabled(txtHakouNo, True)
                enabler.SettingEnabled(txtHakouNoKai, True)
                enabler.SettingEnabled(cmbHachuUmu, True)

                enabler.SettingEnabled(btnCOLOR, True)
                enabler.SettingEnabled(btnColorCLEAR, True)
                enabler.SettingEnabled(btnExcelExport, True)
                enabler.SettingEnabled(btnExcelImport, headerSubject.Enabled.BtnExcelImportEnabled)
                enabler.SettingEnabled(btnBACK, True)
                enabler.SettingEnabled(btnRegister, True)
                enabler.SettingEnabled(btnSave, True)
                enabler.SettingEnabled(btnCopy, headerSubject.Enabled.BtnCopyEnabled)

                enabler.SettingEnabled(BtnSeisakuIchiranReference, True)
            End If

            '2012/01/10
            If headerSubject.SekkeiTenkaibi = "" Then
                enabler.SettingEnabled(cmbJikyuUmu, True)
            Else
                enabler.SettingEnabled(cmbJikyuUmu, False)
            End If

            'End If
        End Sub

        ''' <summary>
        ''' スプレッドを初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitializeSpread()

            baseCarObserver.Initialize()
            completeCarObserver.Initialize()
            basicOptionObserver.Initialize()
            specialOptionObserver.Initialize()
            baseTenkaiCarObserver.Initialize()
            ebomKanshiObserver.Initialize()

        End Sub

        ''' <summary>
        ''' ヘッダー部を初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitializeHeader()

            ShisakuFormUtil.setTitleVersion(Me)
            ''画面のPG-IDが表示されます。
            LblCurrPGId.Text = "PG-ID :" + PROGRAM_ID_09

            ShisakuFormUtil.SetDateTimeNow(Me.LblDateNow, Me.LblTimeNow)
            ShisakuFormUtil.SetIdAndBuka(Me.LblCurrUserId, Me.LblCurrBukaName)

            ShisakuFormUtil.SettingDefaultProperty(cmbKaihatufugo)
            ShisakuFormUtil.SettingDefaultProperty(cmbEventPhaseName)
            ShisakuFormUtil.SettingDefaultProperty(txtEventName)
            txtEventName.MaxLength = 50
            'txtEventName.ImeMode = Windows.Forms.ImeMode.On
            ShisakuFormUtil.SettingDefaultProperty(cmbUnitKbn)
            ShisakuFormUtil.SettingDefaultProperty(txtHakouNo)
            txtHakouNo.MaxLength = 20
            ShisakuFormUtil.SettingDefaultProperty(txtHakouNoKai)
            txtHakouNoKai.MaxLength = 2

            ShisakuFormUtil.SettingDefaultProperty(cmbHachuUmu)
            ShisakuFormUtil.SettingDefaultProperty(cmbJikyuUmu)     '2012/01/07
            ShisakuFormUtil.SettingDefaultProperty(txtInputSupport)
        End Sub

#Region "Subject(Header)へ変更を通知"
        Private Sub cmbKaihatufugo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbKaihatufugo.SelectedValueChanged
            If headerSubject Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            headerSubject.ShisakuKaihatsuFugo = cmbKaihatufugo.SelectedValue
            headerSubject.NotifyObservers()
        End Sub

        Private Sub cmbEventPhaseName_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEventPhaseName.SelectedValueChanged
            If headerSubject Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            headerSubject.ShisakuEventPhaseName = cmbEventPhaseName.SelectedValue
            headerSubject.NotifyObservers()
        End Sub

        Private Sub txtEventName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEventName.LostFocus
            headerSubject.ShisakuEventName = txtEventName.Text
            headerSubject.NotifyObservers()
        End Sub

        Private Sub cmbUnitKbn_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnitKbn.SelectedValueChanged
            If headerSubject Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            headerSubject.UnitKbn = cmbUnitKbn.SelectedValue
            headerSubject.NotifyObservers()
        End Sub

        Private Sub dtpSeisakuJikiFrom_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpSeisakuJikiFrom.ValueChanged
            If headerSubject Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            headerSubject.SeisakujikiFrom = dtpSeisakuJikiFrom.Value
            headerSubject.NotifyObservers()
        End Sub

        Private Sub dtpSeisakuJikiTo_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpSeisakuJikiTo.ValueChanged
            If headerSubject Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            headerSubject.SeisakujikiTo = dtpSeisakuJikiTo.Value
            headerSubject.NotifyObservers()
        End Sub

        Private Sub txtHakouNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHakouNo.LostFocus
            headerSubject.SeisakuichiranHakouNo = txtHakouNo.Text
            headerSubject.NotifyObservers()
        End Sub

        Private Sub txtHakouNoKai_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHakouNoKai.LostFocus
            headerSubject.SeisakuichiranHakouNoKai = txtHakouNoKai.Text
            headerSubject.NotifyObservers()
        End Sub

        Private Sub cmbHachuUmu_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbHachuUmu.SelectedValueChanged
            If headerSubject Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            headerSubject.HachuUmu = cmbHachuUmu.SelectedValue
            headerSubject.NotifyObservers()
        End Sub

        '2012/01/07
        Private Sub cmbJikyuUmu_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbJikyuUmu.SelectedValueChanged
            If headerSubject Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            headerSubject.JikyuUmu = cmbJikyuUmu.SelectedValue
            headerSubject.NotifyObservers()
        End Sub
#End Region

#Region "画面右上時計動く機能"
        Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        End Sub
#End Region


        ''' <summary>
        ''' 完成車情報フォーカスイン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdCompleteCar_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdCompleteCar.GotFocus
            'spdCompleteCar.ImeMode = Windows.Forms.ImeMode.Off
        End Sub

        ''' <summary>
        ''' 完成車情報フォーカスアウト
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdCompleteCar_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdCompleteCar.LostFocus
            'spdCompleteCar.ImeMode = Windows.Forms.ImeMode.Off
        End Sub

        ''' <summary>
        ''' 完成車情報表示変更
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdCompleteCar_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdCompleteCar.VisibleChanged
            If spdCompleteCar.Visible Then
                inputSupport.ForceSetActiveSpread(spdCompleteCar)
            End If
        End Sub

        Private Sub spdBaseCar_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdBaseCar.CellClick

            If e.ColumnHeader Then
                '参考情報の表示／非表示
                If (e.Row = 0) Then
                    '
                    baseCarObserver.referenceCarInfo(e.Column)

                End If
            End If

        End Sub

        ''' <summary>
        ''' ベース車情報表示変更
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdBaseCar_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdBaseCar.VisibleChanged
            If spdBaseCar.Visible Then
                inputSupport.ForceSetActiveSpread(spdBaseCar)
            End If
        End Sub

        ''' <summary>
        ''' 完成車情報キー押下
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdCompleteCar_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdCompleteCar.KeyDown

            Dim downKey As Object
            Dim sheet As Spread.SheetView = Me.spdCompleteCar_Sheet1
            Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)

            downKey = e.KeyCode()

            Select Case downKey

                Case Keys.C
                    'コントロールキーとCキーが押された
                    If (e.Modifiers And Keys.Control) = Keys.Control Then

                        '書式バックアップ
                        Dim listBln As List(Of Boolean()) = GetEditCellInfo(sheet)
                        '書式を一時的に全て保存編集対象にする
                        SetUndoCellFormat(sheet)
                        ''コピー
                        'sheet.ClipboardCopy()

                        ' 選択範囲を取得
                        Dim cr As FarPoint.Win.Spread.Model.CellRange() = spdCompleteCar.ActiveSheet.GetSelections()
                        If cr.Length > 0 Then
                            Dim data As [String] = Nothing
                            If cr(0).Row = -1 Then
                                ' 列単位で選択されている場合
                                For i As Integer = 0 To spdCompleteCar.ActiveSheet.RowCount - 1
                                    If spdCompleteCar.ActiveSheet.GetRowVisible(i) = True Then
                                        data += spdCompleteCar.ActiveSheet.GetClipValue(i, cr(0).Column, 1, cr(0).ColumnCount)
                                    End If
                                Next
                            Else
                                Dim count As Integer = 0
                                ' セル単位で選択されている場合
                                For i As Integer = 0 To cr(0).RowCount - 1
                                    If spdCompleteCar.ActiveSheet.GetRowVisible(cr(0).Row + i) = True Then
                                        If spdCompleteCar.ActiveSheet.GetRowVisible(cr(0).Row + i) = True Then
                                            'If count < 1 Then
                                            '    count = count + 1
                                            '    data += spdCompleteCar.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                            'Else
                                            '    MsgBox("複数の行に渡ってのコピーをすることはできません")
                                            '    SetUndoCellFormat(sheet, listBln)
                                            '    Return
                                            'End If
                                            data += spdCompleteCar.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                        End If
                                    End If
                                Next
                            End If

                            ' クリップボードに設定します
                            Clipboard.SetData(DataFormats.Text, data)
                        End If

                        '書式を戻す
                        SetUndoCellFormat(sheet, listBln)

                    End If

                Case Keys.V
                    '行選択時は無効にする。
                    If selection Is Nothing Then
                        Return
                    End If

                    If (selection.Column = -1 AndAlso selection.ColumnCount - 1) Then
                        e.Handled = True
                    Else
                        'コントロールキーとVキーが押された
                        If (e.Modifiers And Keys.Control) = Keys.Control Then

                            Dim listClip As New List(Of String())

                            listClip = GetClipbordList()

                            If Not listClip Is Nothing Then

                                Dim rowCount As Integer = listClip.Count - 1
                                Dim colCount As Integer = listClip(0).Length

                                '単一コピーの場合'
                                If rowCount = 1 Then
                                    For col As Integer = 0 To selection.ColumnCount - 1
                                        For rowindex As Integer = 0 To selection.RowCount - 1
                                            If Not Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Locked Then
                                                '隠された行にはペーストしない'
                                                If Me.spdCompleteCar_Sheet1.Rows(selection.Row + rowindex).Visible Then

                                                    '列のタグを取得'
                                                    Dim a As String = Me.spdCompleteCar_Sheet1.Columns(selection.Column + col).Tag

                                                    'タグによって半角のみか全角のみか判断'
                                                    Select Case a
                                                        Case "KATASHIKI"
                                                            '型式は半角のみ受け付ける'
                                                            If SpellCheck(listClip(0)(0)) Then
                                                                Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                            End If
                                                        Case "OP"
                                                            'OPは半角のみ受け付ける'
                                                            If SpellCheck(listClip(0)(0)) Then
                                                                Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                            End If
                                                        Case "HANDLE"
                                                            'ハンドルは半角のみ受け付ける'
                                                            If SpellCheck(listClip(0)(0)) Then
                                                                Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                            End If
                                                        Case "SHADAI_NO"
                                                            '車台Noは半角のみ受け付ける'
                                                            If SpellCheck(listClip(0)(0)) Then
                                                                Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                            End If
                                                        Case "GAISO_SHOKU"
                                                            '外装色は半角のみ受け付ける'
                                                            If SpellCheck(listClip(0)(0)) Then
                                                                Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                            End If
                                                        Case "NAISO_SHOKU"
                                                            '内装色は半角のみ受け付ける'
                                                            If SpellCheck(listClip(0)(0)) Then
                                                                Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                            End If
                                                        Case "GROUP"
                                                            'グループは半角のみ受け付ける'
                                                            If SpellCheck(listClip(0)(0)) Then
                                                                Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                            End If
                                                        Case "KOSHI_NO"
                                                            '工指Noは半角のみ受け付ける'
                                                            '2012/03/09 工指Noのペーストは禁止'
                                                            'If SpellCheck(listClip(0)(0)) Then
                                                            '    Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                            'End If
                                                        Case "EG_KATASHIKI"
                                                            'EG型式は半角のみ受け付ける'
                                                            If SpellCheck(listClip(0)(0)) Then
                                                                Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                            End If
                                                        Case "EG_HAIKIRYO"
                                                            'EG排気量は半角のみ受け付ける'
                                                            If SpellCheck(listClip(0)(0)) Then
                                                                Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                            End If
                                                        Case "EG_SYSTEM"
                                                            'EGシステムは半角のみ受け付ける'
                                                            If SpellCheck(listClip(0)(0)) Then
                                                                Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                            End If
                                                        Case "EG_KAKYUKI"
                                                            'EG過給機は半角のみ受け付ける'
                                                            If SpellCheck(listClip(0)(0)) Then
                                                                Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                            End If
                                                        Case "EG_KAKYUKI"
                                                            'EG過給機は半角のみ受け付ける'
                                                            If SpellCheck(listClip(0)(0)) Then
                                                                Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                            End If
                                                        Case "TM_KUDO"
                                                            'TM駆動は半角のみ受け付ける'
                                                            If SpellCheck(listClip(0)(0)) Then
                                                                Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                            End If
                                                        Case "TM_HENSOKUKI"
                                                            'TM変速機は半角のみ受け付ける'
                                                            If SpellCheck(listClip(0)(0)) Then
                                                                Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                            End If
                                                        Case "TM_FUKU_HENSOKUKI"
                                                            'TM副変速機は半角のみ受け付ける'
                                                            If SpellCheck(listClip(0)(0)) Then
                                                                Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                            End If
                                                        Case Else
                                                            Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)

                                                    End Select

                                                End If
                                            End If
                                        Next
                                    Next
                                ElseIf rowCount > 1 Then
                                    '複数コピーの場合'
                                    For col As Integer = 0 To selection.ColumnCount - 1
                                        For rowindex As Integer = 0 To selection.RowCount - 1
                                            If Not Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Locked Then
                                                '隠された行にはペーストしない'
                                                If Me.spdCompleteCar_Sheet1.Rows(selection.Row + rowindex).Visible Then
                                                    Me.spdCompleteCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                Else
                                                    '非表示なら'


                                                End If
                                            End If
                                        Next
                                    Next
                                End If

                                'セル編集モード時にコピーした場合、以下を行う。
                                If rowCount = 0 Then
                                    rowCount = 1
                                End If

                                '行選択時
                                If selection.Column = -1 Then

                                    If Not StringUtil.IsEmpty(headerSubject.StatusName) Then
                                        '貼りつけ対象のセルを編集済みとし書式を設定する
                                        Me.spdCompleteCar_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                                        Me.spdCompleteCar_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).ForeColor = Color.Blue
                                    End If

                                Else

                                    If (selection.Row + rowCount) >= sheet.RowCount - 1 Then
                                        EBom.Common.ComFunc.ShowInfoMsgBox("貼り付けしようとしている範囲がスプレッド表の最大行を超えています")
                                        Return
                                    End If

                                    If Not StringUtil.IsEmpty(headerSubject.StatusName) Then
                                        '貼りつけ対象のセルを編集済みとし書式を設定する
                                        Me.spdCompleteCar_Sheet1.Cells(selection.Row, selection.Column, selection.Row + rowCount - 1, _
                                                               selection.Column + colCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

                                        Me.spdCompleteCar_Sheet1.Cells(selection.Row, selection.Column, selection.Row + rowCount - 1, _
                                                        selection.Column + colCount - 1).ForeColor = Color.Blue
                                    End If

                                End If

                            End If

                        End If

                    End If

                Case Keys.Delete
                    '行選択・列選択ではDeleteは無効に
                    If Not selection Is Nothing Then
                        If (selection.Column = -1 AndAlso selection.ColumnCount - 1) Then
                            e.Handled = True
                        End If

                        If (selection.Row = -1 AndAlso selection.RowCount - 1) Then
                            e.Handled = True
                        End If
                    Else
                        e.Handled = True
                    End If
            End Select

        End Sub

        ''' <summary>
        ''' ベース車情報キー押下
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdBaseCar_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdBaseCar.KeyDown

            Dim downKey As Object
            Dim sheet As Spread.SheetView = Me.spdBaseCar_Sheet1
            Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)

            downKey = e.KeyCode()

            Select Case downKey

                Case Keys.C
                    '設計展開以降のイベントはKeyDownを無効に'
                    If Not headerSubject.IsSekkeiTenkaiIkou Then
                        'コントロールキーとCキーが押された
                        If (e.Modifiers And Keys.Control) = Keys.Control Then

                            '書式バックアップ
                            Dim listBln As List(Of Boolean()) = GetEditCellInfo(sheet)
                            '書式を一時的に全て保存編集対象にする
                            SetUndoCellFormat(sheet)
                            ''コピー
                            'sheet.ClipboardCopy()

                            ' 選択範囲を取得
                            Dim cr As FarPoint.Win.Spread.Model.CellRange() = spdBaseCar.ActiveSheet.GetSelections()
                            If cr.Length > 0 Then
                                Dim data As [String] = Nothing
                                Dim count As Integer = 0
                                If cr(0).Row = -1 Then
                                    ' 列単位で選択されている場合
                                    For i As Integer = 0 To spdBaseCar.ActiveSheet.RowCount - 1
                                        If spdBaseCar.ActiveSheet.GetRowVisible(i) = True Then
                                            data += spdBaseCar.ActiveSheet.GetClipValue(i, cr(0).Column, 1, cr(0).ColumnCount)
                                        End If
                                    Next
                                Else
                                    ' セル単位で選択されている場合
                                    For i As Integer = 0 To cr(0).RowCount - 1
                                        If spdBaseCar.ActiveSheet.GetRowVisible(cr(0).Row + i) = True Then

                                            'If count < 1 Then
                                            '    count = count + 1
                                            '    data += spdBaseCar.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                            'Else
                                            '    MsgBox("複数の行に渡ってのコピーをすることはできません")
                                            '    SetUndoCellFormat(sheet, listBln)
                                            '    Return
                                            'End If
                                            data += spdBaseCar.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                        End If
                                    Next
                                End If

                                ' クリップボードに設定します
                                Clipboard.SetData(DataFormats.Text, data)
                            End If

                            '書式を戻す
                            SetUndoCellFormat(sheet, listBln)

                        End If
                    End If

                Case Keys.V
                    '設計展開以降のイベントはKeyDownを無効に'
                    If Not headerSubject.IsSekkeiTenkaiIkou Then
                        '行選択時は無効にする。
                        If Not selection Is Nothing Then
                            If (selection.Column = -1 AndAlso selection.ColumnCount - 1) Then
                                e.Handled = True
                            Else

                                'コントロールキーとVキーが押された
                                If (e.Modifiers And Keys.Control) = Keys.Control Then

                                    Dim listClip As New List(Of String())

                                    listClip = GetClipbordList()

                                    If Not listClip Is Nothing Then

                                        Dim rowCount As Integer = listClip.Count - 1
                                        Dim colCount As Integer = listClip(0).Length

                                        '単一コピーの場合'
                                        If rowCount = 1 Then
                                            For col As Integer = 0 To selection.ColumnCount - 1
                                                For rowindex As Integer = 0 To selection.RowCount - 1
                                                    If Not Me.spdBaseCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Locked Then
                                                        '隠された行にはペーストしない'
                                                        If Me.spdBaseCar_Sheet1.Rows(selection.Row + rowindex).Visible Then
                                                            Me.spdBaseCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                        End If
                                                    End If
                                                Next
                                            Next
                                        ElseIf rowCount > 1 Then
                                            '複数コピーの場合'
                                            For col As Integer = 0 To selection.ColumnCount - 1
                                                For rowindex As Integer = 0 To selection.RowCount - 1
                                                    If Not Me.spdBaseCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Locked Then
                                                        '隠された行にはペーストしない'
                                                        If Me.spdBaseCar_Sheet1.Rows(selection.Row + rowindex).Visible Then
                                                            Me.spdBaseCar_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                        Else
                                                            '非表示なら'


                                                        End If
                                                    End If
                                                Next
                                            Next
                                        End If

                                        'セル編集モード時にコピーした場合、以下を行う。
                                        If rowCount = 0 Then
                                            rowCount = 1
                                        End If

                                        '行選択時
                                        If selection.Column = -1 Then

                                            If Not StringUtil.IsEmpty(headerSubject.StatusName) Then
                                                '貼りつけ対象のセルを編集済みとし書式を設定する
                                                Me.spdBaseCar_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                                                Me.spdBaseCar_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).ForeColor = Color.Blue
                                            End If

                                        Else

                                            If (selection.Row + rowCount) >= sheet.RowCount - 1 Then
                                                EBom.Common.ComFunc.ShowInfoMsgBox("貼り付けしようとしている範囲がスプレッド表の最大行を超えています")
                                                Return
                                            End If

                                            If Not StringUtil.IsEmpty(headerSubject.StatusName) Then
                                                '貼りつけ対象のセルを編集済みとし書式を設定する
                                                Me.spdBaseCar_Sheet1.Cells(selection.Row, selection.Column, selection.Row + rowCount - 1, _
                                                                       selection.Column + colCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

                                                Me.spdBaseCar_Sheet1.Cells(selection.Row, selection.Column, selection.Row + rowCount - 1, _
                                                                selection.Column + colCount - 1).ForeColor = Color.Blue
                                            End If
                                        End If
                                    End If

                                End If
                            End If
                        Else

                        End If

                    End If

                Case Keys.Delete
                    '設計展開以降のイベントはKeyDownを無効に'
                    If Not headerSubject.IsSekkeiTenkaiIkou Then
                        '行選択・列選択ではDeleteは無効に
                        If Not selection Is Nothing Then
                            If (selection.Column = -1 AndAlso selection.ColumnCount - 1) Then
                                e.Handled = True
                            End If

                            If (selection.Row = -1 AndAlso selection.RowCount - 1) Then
                                e.Handled = True
                            End If
                        End If
                    Else
                        e.Handled = True
                    End If
            End Select

        End Sub

        ''' <summary>
        ''' 参照車情報キー押下
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdReferenceCar_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdReferenceCar.KeyDown
            Dim selection As FarPoint.Win.Spread.Model.CellRange = spdReferenceCar_Sheet1.GetSelection(0)
            Dim downKey As Object

            downKey = e.KeyCode()

            Select Case downKey

                Case Keys.Delete
                    '行選択・列選択ではDeleteは無効に
                    If (selection.Column = -1 AndAlso selection.ColumnCount - 1) Then
                        e.Handled = True
                    End If

                    If (selection.Row = -1 AndAlso selection.RowCount - 1) Then
                        e.Handled = True
                    End If

            End Select
        End Sub

        ''' <summary>
        ''' 特別装備キー押下
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdSpecialOption_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdSpecialOption.KeyDown

            Dim downKey As Object
            Dim sheet As Spread.SheetView = Me.spdSpecialOption_Sheet1
            Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)

            downKey = e.KeyCode()

            Select Case downKey

                Case Keys.C
                    'コントロールキーとCキーが押された
                    If (e.Modifiers And Keys.Control) = Keys.Control Then

                        '書式バックアップ
                        Dim listBln As List(Of Boolean()) = GetEditCellInfo(sheet)
                        '書式を一時的に全て保存編集対象にする
                        SetUndoCellFormat(sheet)
                        ''コピー
                        'sheet.ClipboardCopy()

                        ' 選択範囲を取得
                        Dim cr As FarPoint.Win.Spread.Model.CellRange() = spdSpecialOption.ActiveSheet.GetSelections()
                        If cr.Length > 0 Then
                            Dim data As [String] = Nothing
                            If cr(0).Row = -1 Then
                                ' 列単位で選択されている場合
                                For i As Integer = 0 To spdSpecialOption.ActiveSheet.RowCount - 1
                                    If spdSpecialOption.ActiveSheet.GetRowVisible(i) = True Then
                                        data += spdSpecialOption.ActiveSheet.GetClipValue(i, cr(0).Column, 1, cr(0).ColumnCount)
                                    End If
                                Next
                            Else
                                Dim count As Integer = 0
                                ' セル単位で選択されている場合
                                For i As Integer = 0 To cr(0).RowCount - 1
                                    If spdSpecialOption.ActiveSheet.GetRowVisible(cr(0).Row + i) = True Then
                                        If spdSpecialOption.ActiveSheet.GetRowVisible(cr(0).Row + i) = True Then
                                            'If count < 1 Then
                                            '    count = count + 1
                                            '    data += spdSpecialOption.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                            'Else
                                            '    MsgBox("複数の行に渡ってのコピーをすることはできません")
                                            '    SetUndoCellFormat(sheet, listBln)
                                            '    Return
                                            'End If
                                            data += spdSpecialOption.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                        End If
                                    End If
                                Next

                            End If

                            ' クリップボードに設定します
                            Clipboard.SetData(DataFormats.Text, data)
                        End If

                        '書式を戻す
                        SetUndoCellFormat(sheet, listBln)

                    End If

                Case Keys.V
                    '行選択時は無効にする。
                    If (selection.Column = -1 AndAlso selection.ColumnCount - 1) Then
                        e.Handled = True
                    Else
                        'コントロールキーとVキーが押された
                        If (e.Modifiers And Keys.Control) = Keys.Control Then

                            Dim listClip As New List(Of String())

                            listClip = GetClipbordList()

                            If Not listClip Is Nothing Then

                                Dim rowCount As Integer = listClip.Count - 1
                                Dim colCount As Integer = listClip(0).Length

                                '単一コピーの場合'
                                If rowCount = 1 Then
                                    For col As Integer = 0 To selection.ColumnCount - 1
                                        For rowindex As Integer = 0 To selection.RowCount - 1
                                            If selection.Column + col > 1 Then
                                                If Not Me.spdSpecialOption_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Locked Then
                                                    '隠された行にはペーストしない'
                                                    If Me.spdSpecialOption_Sheet1.Rows(selection.Row + rowindex).Visible Then
                                                        Me.spdSpecialOption_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                    End If
                                                End If

                                            End If
                                        Next
                                    Next
                                ElseIf rowCount > 1 Then
                                    '複数コピーの場合'
                                    For col As Integer = 0 To selection.ColumnCount - 1
                                        For rowindex As Integer = 0 To selection.RowCount - 1
                                            If selection.Column + col > 1 Then
                                                If Not Me.spdSpecialOption_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Locked Then
                                                    '隠された行にはペーストしない'
                                                    If Me.spdSpecialOption_Sheet1.Rows(selection.Row + rowindex).Visible Then
                                                        Me.spdSpecialOption_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                    Else
                                                        '非表示なら'


                                                    End If
                                                End If

                                            End If
                                        Next
                                    Next
                                End If




                                'セル編集モード時にコピーした場合、以下を行う。
                                If rowCount = 0 Then
                                    rowCount = 1
                                End If

                                '行選択時
                                If selection.Column = -1 Then

                                    If Not StringUtil.IsEmpty(headerSubject.StatusName) Then
                                        '貼りつけ対象のセルを編集済みとし書式を設定する
                                        Me.spdSpecialOption_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                                        Me.spdSpecialOption_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).ForeColor = Color.Blue
                                    End If

                                Else
                                    If (selection.Row + rowCount) >= sheet.RowCount - 1 Then
                                        EBom.Common.ComFunc.ShowInfoMsgBox("貼り付けしようとしている範囲がスプレッド表の最大行を超えています")
                                        Return
                                    End If

                                    If Not StringUtil.IsEmpty(headerSubject.StatusName) Then
                                        '貼りつけ対象のセルを編集済みとし書式を設定する
                                        Me.spdSpecialOption_Sheet1.Cells(selection.Row, selection.Column, selection.Row + rowCount - 1, _
                                                               selection.Column + colCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

                                        Me.spdSpecialOption_Sheet1.Cells(selection.Row, selection.Column, selection.Row + rowCount - 1, _
                                                        selection.Column + colCount - 1).ForeColor = Color.Blue
                                    End If
                                End If
                            End If

                        End If

                    End If

                Case Keys.Delete
                    '行選択・列選択ではDeleteは無効に
                    If Not selection Is Nothing Then
                        If (selection.Column = -1 AndAlso selection.ColumnCount - 1) Then
                            e.Handled = True
                        End If

                        If (selection.Row = -1 AndAlso selection.RowCount - 1) Then
                            e.Handled = True
                        End If
                    Else
                        If sheet.ActiveColumnIndex = -1 OrElse sheet.ActiveRowIndex = -1 Then
                            e.Handled = True
                        End If
                    End If
            End Select
        End Sub

        ''' <summary>
        ''' 【設計展開用】ベース車情報表示変更
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdBaseTenkaiCar_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdBaseTenkaiCar.VisibleChanged
            If spdBaseTenkaiCar.Visible Then
                inputSupport.ForceSetActiveSpread(spdBaseTenkaiCar)
            End If
        End Sub

        ''' <summary>
        ''' 基本装備キー押下
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdBasicOption_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdBasicOption.KeyDown


            Dim downKey As Object
            Dim sheet As Spread.SheetView = Me.spdBasicOption_Sheet1
            Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)

            downKey = e.KeyCode()

            Select Case downKey

                Case Keys.C
                    'コントロールキーとCキーが押された
                    If (e.Modifiers And Keys.Control) = Keys.Control Then

                        '書式バックアップ
                        Dim listBln As List(Of Boolean()) = GetEditCellInfo(sheet)
                        '書式を一時的に全て保存編集対象にする
                        SetUndoCellFormat(sheet)
                        ''コピー
                        'sheet.ClipboardCopy()

                        ' 選択範囲を取得
                        Dim cr As FarPoint.Win.Spread.Model.CellRange() = spdBasicOption.ActiveSheet.GetSelections()
                        If cr.Length > 0 Then
                            Dim data As [String] = Nothing
                            If cr(0).Row = -1 Then
                                ' 列単位で選択されている場合
                                For i As Integer = 0 To spdBasicOption.ActiveSheet.RowCount - 1
                                    If spdBasicOption.ActiveSheet.GetRowVisible(i) = True Then
                                        data += spdBasicOption.ActiveSheet.GetClipValue(i, cr(0).Column, 1, cr(0).ColumnCount)
                                    End If
                                Next
                            Else
                                Dim count As Integer = 0
                                ' セル単位で選択されている場合
                                For i As Integer = 0 To cr(0).RowCount - 1
                                    If spdBasicOption.ActiveSheet.GetRowVisible(cr(0).Row + i) = True Then
                                        If spdBasicOption.ActiveSheet.GetRowVisible(cr(0).Row + i) = True Then
                                            'If count < 1 Then
                                            '    count = count + 1
                                            '    data += spdBasicOption.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                            'Else
                                            '    MsgBox("複数の行に渡ってのコピーをすることはできません")
                                            '    SetUndoCellFormat(sheet, listBln)
                                            '    Return
                                            'End If
                                            data += spdBasicOption.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                        End If
                                    End If
                                Next
                            End If

                            ' クリップボードに設定します
                            Clipboard.SetData(DataFormats.Text, data)
                        End If

                        '書式を戻す
                        SetUndoCellFormat(sheet, listBln)

                    End If

                Case Keys.V
                    '行選択時は無効にする。
                    If Not selection Is Nothing Then
                        If (selection.Column = -1 AndAlso selection.ColumnCount - 1) Then
                            e.Handled = True
                        Else
                            'コントロールキーとVキーが押された
                            If (e.Modifiers And Keys.Control) = Keys.Control Then

                                Dim listClip As New List(Of String())

                                listClip = GetClipbordList()

                                If Not listClip Is Nothing Then

                                    Dim rowCount As Integer = listClip.Count - 1
                                    Dim colCount As Integer = listClip(0).Length

                                    'セル編集モード時にコピーした場合、以下を行う。
                                    If rowCount = 0 Then
                                        rowCount = 1
                                    End If

                                    '単一コピーの場合'
                                    If rowCount = 1 Then
                                        For col As Integer = 0 To selection.ColumnCount - 1
                                            For rowindex As Integer = 0 To selection.RowCount - 1
                                                If selection.Column + col > 1 Then
                                                    If Not Me.spdBasicOption_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Locked Then
                                                        '隠された行にはペーストしない'
                                                        If Me.spdBasicOption_Sheet1.Rows(selection.Row + rowindex).Visible Then
                                                            Me.spdBasicOption_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        Next
                                    ElseIf rowCount > 1 Then
                                        '複数コピーの場合'
                                        For col As Integer = 0 To selection.ColumnCount - 1
                                            For rowindex As Integer = 0 To selection.RowCount - 1
                                                If selection.Column + col Then
                                                    If Not Me.spdBasicOption_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Locked Then
                                                        '隠された行にはペーストしない'
                                                        If Me.spdBasicOption_Sheet1.Rows(selection.Row + rowindex).Visible Then
                                                            Me.spdBasicOption_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                        Else
                                                            '非表示なら'


                                                        End If
                                                    End If
                                                End If
                                            Next
                                        Next
                                    End If

                                    '行選択時
                                    If selection.Column = -1 Then

                                        If Not StringUtil.IsEmpty(headerSubject.StatusName) Then
                                            '貼りつけ対象のセルを編集済みとし書式を設定する
                                            Me.spdBasicOption_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                                            Me.spdBasicOption_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).ForeColor = Color.Blue
                                        End If

                                    Else
                                        If (selection.Row + rowCount) >= sheet.RowCount - 1 Then
                                            EBom.Common.ComFunc.ShowInfoMsgBox("貼り付けしようとしている範囲がスプレッド表の最大行を超えています")
                                            Return
                                        End If

                                        If Not StringUtil.IsEmpty(headerSubject.StatusName) Then
                                            '貼りつけ対象のセルを編集済みとし書式を設定する
                                            Me.spdBasicOption_Sheet1.Cells(selection.Row, selection.Column, selection.Row + rowCount - 1, _
                                                                   selection.Column + colCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

                                            Me.spdBasicOption_Sheet1.Cells(selection.Row, selection.Column, selection.Row + rowCount - 1, _
                                                            selection.Column + colCount - 1).ForeColor = Color.Blue
                                        End If
                                    End If
                                End If

                            End If
                        End If

                    Else
                        If Not StringUtil.IsEmpty(headerSubject.StatusName) Then
                            '貼りつけ対象のセルを編集済みとし書式を設定する
                            Me.spdBasicOption_Sheet1.Cells(sheet.ActiveRowIndex, sheet.ActiveColumnIndex, sheet.ActiveRowIndex, _
                                                   sheet.ActiveColumnIndex).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

                            Me.spdBasicOption_Sheet1.Cells(sheet.ActiveRowIndex, sheet.ActiveColumnIndex, sheet.ActiveRowIndex, _
                                                   sheet.ActiveColumnIndex).ForeColor = Color.Blue
                        End If
                    End If

                Case Keys.Delete
                    '行選択・列選択ではDeleteは無効に
                    If (selection.Column = -1 AndAlso selection.ColumnCount - 1) Then
                        e.Handled = True
                    End If

                    If (selection.Row = -1 AndAlso selection.RowCount - 1) Then
                        e.Handled = True
                    End If

            End Select
        End Sub

        ''' <summary>
        ''' 基本装備表示変更
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdBasicOption_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdBasicOption.VisibleChanged
            If spdBasicOption.Visible Then
                inputSupport.ForceSetActiveSpread(spdBasicOption)
            End If
        End Sub

        ''' <summary>
        ''' 特別装備表示変更
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdSpecialOption_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdSpecialOption.VisibleChanged
            If spdSpecialOption.Visible Then
                inputSupport.ForceSetActiveSpread(spdSpecialOption)
            End If
        End Sub

        ''' <summary>
        ''' ベース車情報変更
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdBaseCar_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdBaseCar.Change

            Dim sheet As Spread.SheetView = spdBaseCar.ActiveSheet

            If Me.cmbKaihatufugo.Enabled = False Then
                ' 選択セルの場所を特定します。
                ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
                ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

                ' 該当セルの文字色、文字太を変更する。
                sheet.SetStyleInfo(ParaActRowIdx, ParaActColIdx, CreateNewStyle())
                sheet.Cells(ParaActRowIdx, ParaActColIdx).Locked = False        '2012/01/09
            End If

            '2012/02/11
            '2012/01/09
            If e.Column = 0 Then
                SetActiveSpreadColor(sheet, sheet.ActiveRowIndex, 0)
            End If

        End Sub

        ''' <summary>
        ''' 完成車情報変更
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdCompleteCar_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdCompleteCar.Change

            Dim sheet As Spread.SheetView = spdCompleteCar.ActiveSheet

            If Me.cmbKaihatufugo.Enabled = False Then
                ' 選択セルの場所を特定します。
                ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
                ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

                ' 該当セルの文字色、文字太を変更する。
                sheet.SetStyleInfo(ParaActRowIdx, ParaActColIdx, CreateNewStyle())
                sheet.Cells(ParaActRowIdx, ParaActColIdx).Locked = False
            End If

            '2012/02/11
            '2012/01/09
            If e.Column = 0 Then
                SetActiveSpreadColor(sheet, sheet.ActiveRowIndex, 0)
            End If

        End Sub

        ''' <summary>
        ''' 基本装備変更
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdBasicOption_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdBasicOption.Change

            Dim sheet As Spread.SheetView = spdBasicOption.ActiveSheet

            If Me.cmbKaihatufugo.Enabled = False Then
                ' 選択セルの場所を特定します。
                ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
                ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

                ' 該当セルの文字色、文字太を変更する。
                sheet.SetStyleInfo(ParaActRowIdx, ParaActColIdx, CreateNewStyle())
                sheet.Cells(ParaActRowIdx, ParaActColIdx).Locked = False
            End If

            '2012/02/11
            '2012/01/09
            If e.Column = 0 Then
                SetActiveSpreadColor(sheet, sheet.ActiveRowIndex, 0)
            End If

        End Sub

        ''' <summary>
        ''' 特別装備変更
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdSpecialOption_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdSpecialOption.Change

            Dim sheet As Spread.SheetView = spdSpecialOption.ActiveSheet

            If Me.cmbKaihatufugo.Enabled = False Then
                ' 選択セルの場所を特定します。
                ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
                ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

                ' 該当セルの文字色、文字太を変更する。
                sheet.SetStyleInfo(ParaActRowIdx, ParaActColIdx, CreateNewStyle())
                sheet.Cells(ParaActRowIdx, ParaActColIdx).Locked = False
            End If

            '2012/02/11
            '2012/01/09
            If e.Column = 0 Then
                SetActiveSpreadColor(sheet, sheet.ActiveRowIndex, 0)
            End If

        End Sub

        '2012/01/09
        ''' <summary>
        ''' 行背景色をグレーに変更する(種別が "D" の場合のみ)
        ''' </summary>
        ''' <param name="activeSheet">色を設定するSpreadSheet</param>
        ''' <param name="targetRow">対象の行位置</param>
        ''' <param name="targetColumn">種別のカラム位置</param>
        ''' <remarks></remarks>
        Private Sub SetActiveSpreadColor(ByVal activeSheet As SheetView, ByVal targetRow As Long, ByVal targetColumn As Long)
            Const DEF_TYPE_D As String = "D"
            Dim nColumn As Long = 0
            Dim nColumn2 As Long = 0

            If activeSheet.ColumnCount - 1 > 0 Then
                nColumn2 = activeSheet.ColumnCount - 1
            End If

            If activeSheet.Cells(targetRow, targetColumn, targetRow, targetColumn).Text = DEF_TYPE_D Then
                activeSheet.Cells(targetRow, nColumn, targetRow, nColumn2).BackColor = Color.DimGray       'ディムグレー
                activeSheet.Cells(targetRow, nColumn, targetRow, nColumn2).ForeColor = Color.LightGray     '文字色も薄くグレー
            Else
                activeSheet.Cells(targetRow, nColumn, targetRow, nColumn2).ResetBackColor()
                activeSheet.Cells(targetRow, nColumn, targetRow, nColumn2).ResetForeColor()
            End If
        End Sub

        ''' <summary>
        ''' 文字列チェック
        ''' </summary>
        ''' <param name="value">対象文字列</param>
        ''' <returns>ALL半角ならTrue</returns>
        ''' <remarks></remarks>
        Private Function SpellCheck(ByVal value As String) As Boolean
            '文字列の長さ
            Dim valueLength As Integer = value.Length
            Dim Enc As Encoding = Encoding.GetEncoding("Shift_JIS")

            For i As Integer = 0 To valueLength - 1
                Dim c As String = Mid(value, i + 1, 1)

                '半角か全角かチェック'
                If Not StringUtil.IsEmpty(c) Then
                    If Enc.GetByteCount(c) = 1 Then

                    Else
                        Return False
                    End If
                End If
            Next
            Return True
        End Function

        ''' <summary>
        ''' フォント色を青色に、文字を太くする。
        ''' </summary>
        ''' <remarks></remarks>
        Private Function CreateNewStyle() As Spread.StyleInfo
            Dim styleinfo As New Spread.StyleInfo
            styleinfo.ForeColor = Color.Blue '青色に
            styleinfo.Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
            Return styleinfo
        End Function

#Region "スプレッド間スクロール位置同期処理"
        ''' <summary>スクロール行同期管理メンバ変数 </summary>
        Private RowNoScroll_Info As Integer = -1
        Private RowNoScroll_Siyou As Integer = -1

#Region "車情報スプレッドと装備仕様スプレッドで行位置が1行ずれるための調整"
        ''' <summary>
        ''' 車情報スプレッドと装備仕様スプレッドで行位置が1行ずれるための調整
        ''' </summary>
        ''' <param name="IsInfo">完成車情報・参考・基本車情報はTRUEそれ以外はFALSEを入れて取得</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function RowNoScroll(ByVal IsInfo As Boolean) As Integer
            Dim result As Integer = -1

            If IsInfo = True Then
                If RowNoScroll_Info >= 0 Then
                    result = RowNoScroll_Info
                Else
                    '大、中、小の区分にして調整は不要
                    '　変更前　車情報：３行、装備仕様情報２行
                    '　変更後　車情報：３行、装備仕様情報３行　元のマージを解除して１行追加（大、中、小で３行使用）
                    result = RowNoScroll_Siyou
                    'result = RowNoScroll_Siyou + 1
                End If
            Else
                If RowNoScroll_Siyou >= 0 Then
                    result = RowNoScroll_Siyou
                Else
                    '大、中、小の区分にして調整は不要
                    '　変更前　車情報：３行、装備仕様情報２行
                    '　変更後　車情報：３行、装備仕様情報３行　元のマージを解除して１行追加（大、中、小で３行使用）
                    result = RowNoScroll_Info
                    'result = RowNoScroll_Info - 1
                End If
            End If

            Return result

        End Function

#End Region

#Region "スクロール位置変更イベント"
        ''' <summary>
        ''' スクロール位置変更イベント_ベース車・参考・完成車情報スプレッド用(各スプレッド間スクロール行位置同期用)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdInfo_TopChange(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.TopChangeEventArgs) Handles spdBaseCar.TopChange _
                                                                                                                            , spdCompleteCar.TopChange _
                                                                                                                            , spdBaseTenkaiCar.TopChange _
                                                                                                                            , spdEbomKanshi.TopChange
            ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 z) (TES)施 add START
            ', spdEbomKanshi.TopChange
            ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 z) (TES)施 add END
            ', spdReferenceCar.TopChange

            '大、中、小の区分にして調整は不要
            '　変更前　車情報：３行、装備仕様情報２行
            '　変更後　車情報：３行、装備仕様情報３行　元のマージを解除して１行追加（大、中、小で３行使用）
            RowNoScroll_Siyou = e.NewTop
            'RowNoScroll_Siyou = -1
            RowNoScroll_Info = e.NewTop
        End Sub

        ''' <summary>
        ''' スクロール位置変更イベント_装備仕様車スプレッド用(各スプレッド間スクロール行位置同期用)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdSiyou_TopChange(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.TopChangeEventArgs) Handles spdBasicOption.TopChange _
                                                                                                                        , spdSpecialOption.TopChange

            '大、中、小の区分にして調整は不要
            '　変更前　車情報：３行、装備仕様情報２行
            '　変更後　車情報：３行、装備仕様情報３行　元のマージを解除して１行追加（大、中、小で３行使用）
            RowNoScroll_Info = e.NewTop
            'RowNoScroll_Info = -1
            RowNoScroll_Siyou = e.NewTop

        End Sub
#End Region

#End Region

#Region "右ショートカットメニュー(切取り)"
        ''' <summary>
        ''' 右ショートカットメニュー(切取り)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolMenuCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolMenuCut.Click
            ToolCut()
        End Sub

#End Region

#Region "右ショートカットメニュー(コピー)"
        ''' <summary>
        ''' 右ショートカットメニュー(コピー)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolMenuCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolMenuCopy.Click
            ToolCopy()
        End Sub

#End Region

#Region "右ショートカットメニュー(貼付け)"
        ''' <summary>
        ''' 右ショートカットメニュー(貼付け)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolMenuPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolMenuPaste.Click
            ToolPaste()
        End Sub

#End Region

#Region "切取りイベント処理"
        ''' <summary>
        ''' 切取りイベント処理
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ToolCut()
            Try

                Dim spd As FpSpread = GetVisibleSpread

                If spd.ActiveSheet.GetSelection(0) Is Nothing Then
                    ComFunc.ShowInfoMsgBox("セル範囲が指定されていません。")
                    Exit Sub
                End If

                ToolCutHontai()

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("切取りが正常に行えませんでした。")
            End Try
        End Sub
#End Region

#Region "コピーイベント処理"
        ''' <summary>
        ''' コピーイベント処理
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ToolCopy()
            Try

                Dim spd As FpSpread = GetVisibleSpread

                If spd.ActiveSheet.GetSelection(0) Is Nothing Then
                    ComFunc.ShowInfoMsgBox("セル範囲が指定されていません。")
                    Exit Sub
                End If

                ToolCopyHontai()

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("コピーが正常に行えませんでした。")
            End Try

        End Sub

#End Region

#Region "貼りつけイベント処理"

        Private Sub ToolPaste()
            Try

                Dim spd As FpSpread = GetVisibleSpread

                If spd.ActiveSheet.GetSelection(0) Is Nothing Then
                    ComFunc.ShowInfoMsgBox("セル範囲が指定されていません。")
                    Exit Sub
                End If

                ToolPasteHontai(GetVisibleSheet)

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("貼りつけが正常に行えませんでした。")
            End Try
        End Sub
#End Region

#Region "ボタン（コピー）"
        ''' <summary>
        ''' ボタン（コピー）
        ''' 
        ''' キーボードでCTRL + c を押した事にし
        ''' この後KeyDownイベントに流す
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ToolCopyHontai()

            Dim spd As FpSpread = GetVisibleSpread

            spd.Focus()

            System.Threading.Thread.Sleep(10)
            System.Windows.Forms.SendKeys.Flush()

            ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！
            System.Windows.Forms.SendKeys.Send("^c")

        End Sub

#End Region

#Region "ボタン（切り取り）"
        ''' <summary>
        ''' ボタン（切り取り）
        ''' 
        ''' キーボードでCTRL + Xを押した事にし
        ''' この後KeyDownイベントに流す
        ''' 
        ''' 
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ToolCutHontai()

            Dim spd As FpSpread = GetVisibleSpread

            spd.Focus()

            System.Threading.Thread.Sleep(10)

            System.Windows.Forms.SendKeys.Flush()
            ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！
            System.Windows.Forms.SendKeys.Send("^x")
            ''カットした後に同じ箇所をDELETEしてみる。
            'System.Windows.Forms.SendKeys.Send("{DELETE}")

        End Sub

#End Region

#Region "ボタン（貼りつけ）"
        ''' <summary>
        ''' ボタン（貼りつけ）
        ''' 
        ''' キーボードでCTRL + vを押した事にし
        ''' この後KeyDownイベントに流す
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ToolPasteHontai(ByVal aSheet As Spread.SheetView)

            Dim spd As FpSpread = GetVisibleSpread

            spd.Focus()

            System.Windows.Forms.SendKeys.Flush()
            System.Threading.Thread.Sleep(10)
            ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！
            System.Windows.Forms.SendKeys.Send("^v")


        End Sub
#End Region

#Region "表示されているスプレッドを返す"
        ''' <summary>
        ''' 表示されているスプレッドを返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetVisibleSpread() As Spread.FpSpread
            Get
                If Me.spdBaseCar.Visible = True Then
                    Return Me.spdBaseCar
                ElseIf Me.spdCompleteCar.Visible = True Then
                    Return Me.spdCompleteCar
                ElseIf Me.spdBasicOption.Visible = True Then
                    Return Me.spdBasicOption
                ElseIf Me.spdSpecialOption.Visible = True Then
                    Return Me.spdSpecialOption
                Else
                    Return Me.spdBaseTenkaiCar
                    'Return Me.spdReferenceCar
                End If
            End Get
        End Property
#End Region

#Region "表示されているシートオブジェクトを返す"
        ''' <summary>
        ''' 表示されているシートを返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property GetVisibleSheet() As Spread.SheetView
            Get
                If Me.spdBaseCar.Visible = True Then
                    Return Me.spdBaseCar_Sheet1
                ElseIf Me.spdCompleteCar.Visible = True Then
                    Return Me.spdCompleteCar_Sheet1
                ElseIf Me.spdBasicOption.Visible = True Then
                    Return Me.spdBasicOption_Sheet1
                ElseIf Me.spdSpecialOption.Visible = True Then
                    Return Me.spdSpecialOption_Sheet1
                Else
                    Return Me.spdBaseTenkaiCar_Sheet1
                    'Return Me.spdReferenceCar_Sheet1
                End If
            End Get
        End Property
#End Region

        Private Sub 挿入ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 列挿入ToolStripMenuItem.Click

            '----------------------------------------------------------------------------------------------
            '２次改修
            '   設計展開以降、行の挿入は出来ない。
            If Not headerSubject.IsSekkeiTenkaiIkou Then

                'アクティブになっていれば'
                If spdBasicOption.Visible Then
                    If spdBasicOption_Sheet1.SelectionCount <> 1 Then
                        Return
                    Else
                        Dim selection As CellRange = spdBasicOption_Sheet1.GetSelection(0)

                        If SpreadUtil.IsSelectedColumn(selection) Then
                            basicOptionObserver.InsertColumns(selection.Column, selection.ColumnCount)
                        End If
                    End If
                End If

                If spdSpecialOption.Visible Then
                    If spdSpecialOption_Sheet1.SelectionCount <> 1 Then
                        Return
                    Else
                        Dim selection As CellRange = spdSpecialOption_Sheet1.GetSelection(0)

                        If SpreadUtil.IsSelectedColumn(selection) Then
                            specialOptionObserver.InsertColumns(selection.Column, selection.ColumnCount)
                        End If
                    End If
                End If

            End If
            '----------------------------------------------------------------------------------------------

        End Sub

        Private Sub 削除ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 列貼り付けToolStripMenuItem.Click

            '----------------------------------------------------------------------------------------------
            '２次改修
            '   設計展開以降、列の削除は出来ない。
            If Not headerSubject.IsSekkeiTenkaiIkou Then

                'アクティブになっているほうに'
                If spdBasicOption.Visible Then
                    If spdBasicOption_Sheet1.SelectionCount <> 1 Then
                        Return
                    Else
                        Dim selection As CellRange = spdBasicOption_Sheet1.GetSelection(0)

                        If SpreadUtil.IsSelectedColumn(selection) Then
                            basicOptionObserver.RemoveColumns(selection.Column, selection.ColumnCount)
                        End If
                    End If
                End If

                If spdSpecialOption.Visible Then
                    If spdSpecialOption_Sheet1.SelectionCount <> 1 Then
                        Return
                    Else
                        Dim selection As CellRange = spdSpecialOption_Sheet1.GetSelection(0)

                        If SpreadUtil.IsSelectedColumn(selection) Then
                            specialOptionObserver.RemoveColumns(selection.Column, selection.ColumnCount)
                        End If
                    End If
                End If

            End If
            '----------------------------------------------------------------------------------------------

        End Sub


#Region "クリップボードの内容をstring()型のリストに格納し返す"
        Public Shared Function GetClipbordList() As List(Of String())
            Dim listStr As New List(Of String())

            'システムクリップボードにあるデータを取得します
            Dim iData As IDataObject = Clipboard.GetDataObject()

            Dim strRow() As String

            'テキスト形式データの判断
            If iData.GetDataPresent(DataFormats.Text) = False Then
                Return Nothing
            Else

                Console.WriteLine(CType(iData.GetData(DataFormats.Text), String))
                strRow = CType(iData.GetData(DataFormats.Text), String).Split(vbCrLf)

            End If

            For i As Integer = 0 To strRow.Length - 1
                Dim strChar() As String = strRow(i).Split(vbTab)
                listStr.Add(strChar)
            Next

            Return listStr

        End Function

#End Region

#Region "編集書済式有無のセル配列を返す"
        ''' <summary>
        ''' 編集済書式有無のセル配列を返す
        ''' </summary>
        ''' <param name="aSheet">対象シートをセットする</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetEditCellInfo(ByVal aSheet As SheetView) As List(Of Boolean())

            Dim selection As FarPoint.Win.Spread.Model.CellRange = aSheet.GetSelection(0)
            Dim listBln As New List(Of Boolean())

            If Not selection Is Nothing Then
                For i As Integer = 0 To selection.RowCount - 1

                    Dim blnTbl() As Boolean = Nothing
                    Dim colCnt As Integer = 0
                    Dim col As Integer = 0
                    If selection.ColumnCount = -1 Then
                        colCnt = aSheet.ColumnCount
                        col = 0
                    Else
                        colCnt = selection.ColumnCount
                        col = selection.Column
                    End If

                    ReDim Preserve blnTbl(colCnt - 1)

                    For j As Integer = 0 To colCnt - 1
                        Dim objFont As System.Drawing.Font = aSheet.Cells(selection.Row + i, col + j).Font

                        '太字Cellを編集済セルと判定
                        If Not objFont Is Nothing AndAlso objFont.Bold = True Then
                            blnTbl(j) = True
                        Else
                            blnTbl(j) = False
                        End If

                    Next
                    listBln.Add(blnTbl)
                Next
            Else
                Dim blnTbl() As Boolean = Nothing
                Dim colCnt As Integer = 0
                Dim col As Integer = 0
                colCnt = 1
                col = aSheet.ActiveColumnIndex
                ReDim Preserve blnTbl(colCnt - 1)

                For j As Integer = 0 To colCnt - 1
                    Dim objFont As System.Drawing.Font = aSheet.Cells(aSheet.ActiveRowIndex, col + j).Font

                    '太字Cellを編集済セルと判定
                    If Not objFont Is Nothing AndAlso objFont.Bold = True Then
                        blnTbl(j) = True
                    Else
                        blnTbl(j) = False
                    End If

                Next
                listBln.Add(blnTbl)
            End If



            Return listBln

        End Function

#End Region

#Region "コピーの時に一時的に編集済書式を設定する、また設定した書式を元に戻す"
        ''' <summary>
        ''' コピーの時に一時的に書式を設定する、また設定した書式を元に戻す
        ''' 
        ''' この処理はCTRL+cでの貼りつけの場合書式と値がコピーされてしまうため、単純操作では
        ''' 貼付け先に編集済みの書式が設定出来ません。
        ''' この問題に対応する為に、編集済み書式をCTRL+Cを送信する前に設定し、
        ''' 送信後に元の書式にするという対応が必要になります。
        ''' 
        ''' そもそも、こんな面倒な事が必要な原因は
        ''' 
        ''' コードで"spdParts_Sheet1.ClipboardPaste"と単純に記述されて実行された操作は
        ''' Undo操作が一切対象外になるという事が原因です。
        ''' 
        ''' Undoを行うにはキーボードからCTRL+Xなどの操作をコードから行う必要があり
        ''' SendKeyの様なコードが記述されています。
        ''' 
        ''' 
        ''' </summary>
        ''' <param name="aSheet">対象シート</param>
        ''' <param name="alistBln">書式を全て編集済書式にするときは指定しない</param>
        ''' <remarks></remarks>
        Public Shared Sub SetUndoCellFormat(ByVal aSheet As SheetView, Optional ByVal alistBln As List(Of Boolean()) = Nothing)

            Dim selection As FarPoint.Win.Spread.Model.CellRange = aSheet.GetSelection(0)
            Dim colCnt As Integer = 0
            Dim col As Integer = 0

            If Not selection Is Nothing Then
                If selection.ColumnCount = -1 Then
                    colCnt = aSheet.ColumnCount
                    col = 0
                Else
                    colCnt = selection.ColumnCount
                    col = selection.Column
                End If
                '無い場合は全て保存対象編集書式とするため全てTrueをセット
                If alistBln Is Nothing Then
                    alistBln = New List(Of Boolean())

                    For i As Integer = 0 To selection.RowCount - 1

                        Dim blnTbl() As Boolean = Nothing
                        ReDim Preserve blnTbl(colCnt - 1)

                        For j As Integer = 0 To colCnt - 1
                            blnTbl(j) = True
                        Next
                        alistBln.Add(blnTbl)
                    Next

                End If

                '受け取ったListの内容で書式を設定
                For i As Integer = 0 To selection.RowCount - 1
                    For j As Integer = 0 To selection.ColumnCount - 1

                        If alistBln(i)(j) = False Then
                            aSheet.Cells(selection.Row + i, selection.Column + j).ForeColor = Nothing
                            aSheet.Cells(selection.Row + i, selection.Column + j).Font = Nothing
                        Else
                            aSheet.Cells(selection.Row + i, selection.Column + j).ForeColor = Color.Blue
                            aSheet.Cells(selection.Row + i, selection.Column + j).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                        End If

                    Next
                Next
            Else
                colCnt = 1
                col = aSheet.ActiveColumnIndex
                '無い場合は全て保存対象編集書式とするため全てTrueをセット
                If alistBln Is Nothing Then
                    alistBln = New List(Of Boolean())


                    Dim blnTbl() As Boolean = Nothing
                    ReDim Preserve blnTbl(colCnt - 1)

                    For j As Integer = 0 To colCnt - 1
                        blnTbl(j) = True
                    Next
                    alistBln.Add(blnTbl)

                End If

                '受け取ったListの内容で書式を設定

                If alistBln(0)(0) = False Then
                    aSheet.Cells(aSheet.ActiveRowIndex, aSheet.ActiveColumnIndex).ForeColor = Nothing
                    aSheet.Cells(aSheet.ActiveRowIndex, aSheet.ActiveColumnIndex).Font = Nothing
                Else
                    aSheet.Cells(aSheet.ActiveRowIndex, aSheet.ActiveColumnIndex).ForeColor = Color.Blue
                    aSheet.Cells(aSheet.ActiveRowIndex, aSheet.ActiveColumnIndex).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                End If

            End If






        End Sub
#End Region

        Private Sub spdBaseCar_EditModeOn(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdBaseCar.EditModeOn
            Dim sheet As Spread.SheetView = spdBaseCar_Sheet1
            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            'IMEを使用不可能にする。
            spdBaseCar.ImeMode = Windows.Forms.ImeMode.Disable
        End Sub
        Private Sub spdBaseCar_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdBaseCar.EditModeOff
            spdBaseCar.EditingControl.ImeMode = Windows.Forms.ImeMode.NoControl
            spdBaseCar.ImeMode = Windows.Forms.ImeMode.NoControl
        End Sub
        Private Sub spdCompleteCar_EditModeOn(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdCompleteCar.EditModeOn
            Dim sheet As Spread.SheetView = spdCompleteCar_Sheet1
            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            If ParaActColIdx = sheet.Columns(SpdCompleteCarObserver.TAG_GRADE).Index _
              OrElse ParaActColIdx = sheet.Columns(SpdCompleteCarObserver.TAG_SHIKEN_MOKUTEKI).Index _
              OrElse ParaActColIdx = sheet.Columns(SpdCompleteCarObserver.TAG_EG_MEMO_1).Index _
              OrElse ParaActColIdx = sheet.Columns(SpdCompleteCarObserver.TAG_EG_MEMO_2).Index _
              OrElse ParaActColIdx = sheet.Columns(SpdCompleteCarObserver.TAG_TM_MEMO_1).Index _
              OrElse ParaActColIdx = sheet.Columns(SpdCompleteCarObserver.TAG_TM_MEMO_2).Index _
              OrElse ParaActColIdx = sheet.Columns(SpdCompleteCarObserver.TAG_GAISO_SHOKU_NAME).Index _
              OrElse ParaActColIdx = sheet.Columns(SpdCompleteCarObserver.TAG_NAISO_SHOKU_NAME).Index _
              OrElse ParaActColIdx = sheet.Columns(SpdCompleteCarObserver.TAG_SHIYOU_MOKUTEKI).Index _
              OrElse ParaActColIdx = sheet.Columns(SpdCompleteCarObserver.TAG_SHIYO_BUSHO).Index _
              OrElse ParaActColIdx = sheet.Columns(SpdCompleteCarObserver.TAG_SEISAKU_HOUHOU).Index _
              OrElse ParaActColIdx = sheet.Columns(SpdCompleteCarObserver.TAG_SHISAKU_MEMO).Index _
              OrElse ParaActColIdx = sheet.Columns(SpdCompleteCarObserver.TAG_TM_KUDO).Index _
              OrElse ParaActColIdx = sheet.Columns(SpdCompleteCarObserver.TAG_TM_HENSOKUKI).Index _
              OrElse ParaActColIdx = sheet.Columns(SpdCompleteCarObserver.TAG_SHAGATA).Index Then
                'IMEを使用可能にする。
                spdCompleteCar.EditingControl.ImeMode = Windows.Forms.ImeMode.NoControl
                spdCompleteCar.ImeMode = Windows.Forms.ImeMode.NoControl
            Else
                'IMEを使用不可能にする。
                spdCompleteCar.EditingControl.ImeMode = Windows.Forms.ImeMode.Disable
                spdCompleteCar.ImeMode = Windows.Forms.ImeMode.Disable
            End If
        End Sub
        Private Sub spdCompleteCar_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdCompleteCar.EditModeOff
            spdCompleteCar.EditingControl.ImeMode = Windows.Forms.ImeMode.NoControl
            spdCompleteCar.ImeMode = Windows.Forms.ImeMode.NoControl
        End Sub
        Private Sub spdBasicOption_EditModeOn(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdBasicOption.EditModeOn
            Dim sheet As Spread.SheetView = spdBasicOption_Sheet1
            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            'IMEを使用不可能にする。
            spdBasicOption.ImeMode = Windows.Forms.ImeMode.NoControl
        End Sub
        Private Sub spdBasicOption_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdBasicOption.EditModeOff
            spdBasicOption.EditingControl.ImeMode = Windows.Forms.ImeMode.NoControl
            spdBasicOption.ImeMode = Windows.Forms.ImeMode.NoControl
        End Sub
        Private Sub spdSpecialOption_EditModeOn(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdSpecialOption.EditModeOn
            Dim sheet As Spread.SheetView = spdSpecialOption_Sheet1
            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            'IMEを使用不可能にする。
            spdSpecialOption.ImeMode = Windows.Forms.ImeMode.NoControl
        End Sub
        Private Sub spdSpecialOption_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdSpecialOption.EditModeOff
            spdSpecialOption.EditingControl.ImeMode = Windows.Forms.ImeMode.NoControl
            spdSpecialOption.ImeMode = Windows.Forms.ImeMode.NoControl
        End Sub

        Private Sub BtnSeisakuIchiranReference_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSeisakuIchiranReference.Click

            '製作一覧情報セット前にもSPREAD表示値を更新してみる。
            UpdateObserver(subject, Nothing)

            'ベース車情報セット
            Call setBaseSheet()
            '完成車情報セット
            Call setKanseiSheet()
            '基本装備仕様情報セット
            Call setBasicSheet()
            '特別装備仕様情報セット
            Call setSpecialSheet()

            '更新後クリアする。
            seisakuHakouNo = ""
            seisakuHakouNoKaiteiNo = ""
            'ボタンを非表示
            BtnSeisakuIchiranReference.Visible = False

            '保存、コピー、EXCELなどのボタンを押せるようにする。
            btnSave.Visible = True
            btnCopy.Visible = True
            btnExcelExport.Visible = True
            btnExcelImport.Visible = True
        End Sub

        Private Function IsAddMode() As Boolean
            Return StringUtil.IsEmpty(ShisakuEventCode)
        End Function

        'ベース車情報セット
        Private Sub setBaseSheet()
            '製作一覧HD情報取得
            'Dim strGousya As String
            Dim getSeisakuIchiranHd As New SeisakuIchiranDaoImpl
            Dim tSeisakuHakouHdVo As New TSeisakuHakouHdVo
            Dim kanseiList As New List(Of TSeisakuIchiranKanseiVo)
            Dim wbList As New List(Of TSeisakuIchiranWbVo)
            Dim getSobiKaitei As New EventEditBaseCarDaoImpl


            tSeisakuHakouHdVo = getSeisakuIchiranHd.GetSeisakuIchiranHd(seisakuHakouNo, seisakuHakouNoKaiteiNo)

            Dim Ichiran = New SeisakuIchiranDaoImpl
            'ベース車情報
            kanseiList = Ichiran.GetTSeisakuIchiranKansei(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            '完成車情報
            wbList = Ichiran.GetTSeisakuIchiranWb(seisakuHakouNo, seisakuHakouNoKaiteiNo)


            For rowNo As Integer = 3 To spdBaseCar_Sheet1.RowCount - 1

                If StringUtil.IsEmpty(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value) Then Continue For

                '号車が入力されている列が対象
                '   種別はブランク（完成車情報）
                If StringUtil.IsNotEmpty(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value) And _
                    StringUtil.IsEmpty(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("SHUBETSU").Index).Value) Then

                    ''2015/08/14 変更 E.Ubukata Ver.2.11.0
                    '' 検索条件が変更されないのでループ内で何度もDBアクセスする必要がないためループ外へ移動
                    'Dim Ichiran = New SeisakuIchiranDaoImpl

                    '初期設定
                    Dim baseSeisakuIchiran = New TSeisakuIchiranBaseVo

                    '製作一覧から設定
                    Dim strGousya As String = ""

                    ''2015/08/14 変更 E.Ubukata Ver.2.11.0
                    '' 検索条件が変更されないのでループ内で何度もDBアクセスする必要がないためループ外へ移動
                    ''ベース車情報
                    'kanseiList = Ichiran.GetTSeisakuIchiranKansei(seisakuHakouNo, seisakuHakouNoKaiteiNo)

                    '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                    '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                    Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                       spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value)
                    For Each voSeisakuKansei As TSeisakuIchiranKanseiVo In kanseiList

                        '2014/02/14
                        '   製作一覧上でWが付いている場合があるので下記の処理に変更する。
                        'Dim strSeisakuGousya As String = voSeisakuKansei.Gousya.PadLeft(4, "0")
                        Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                           voSeisakuKansei.Gousya)

                        '製作一覧の号車が試作イベントの号車を含むなら
                        If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                            strGousya = voSeisakuKansei.Gousya
                            Exit For
                        End If
                    Next

                    'ベース車情報
                    baseSeisakuIchiran = Ichiran.GetTSeisakuIchiranBaseGousya(seisakuHakouNo, seisakuHakouNoKaiteiNo, strGousya)

                    If StringUtil.IsNotEmpty(baseSeisakuIchiran) Then

                        ''2015/09/25 変更 E.Ubukata Ver.2.11.3
                        '' 試作イベント情報が存在する場合はeBom側の情報を表示せずイベントと号車を表示する
                        If StringUtil.IsEmpty(baseSeisakuIchiran.ShisakuEvent) Then
                            'アプライド№を７ケタ型式から取得
                            Dim GetValue = getSobiKaitei.FindSobiKaitei(baseSeisakuIchiran.KaihatsuFugo, _
                                            baseSeisakuIchiran.ShiyoujyouhouNo, _
                                            baseSeisakuIchiran.KatashikiScd7)
                            With spdBaseCar_Sheet1
                                .Cells(rowNo, .Columns("KAIHATSU_FUGO").Index).Value = baseSeisakuIchiran.KaihatsuFugo
                                .Cells(rowNo, .Columns("SHIYO_JOHO_NO").Index).Value = baseSeisakuIchiran.ShiyoujyouhouNo

                                If StringUtil.IsNotEmpty(GetValue) Then
                                    .Cells(rowNo, .Columns("APPLIED_NO").Index).Value = GetValue.AppliedNo
                                End If

                                .Cells(rowNo, .Columns("KATASHIKI").Index).Value = baseSeisakuIchiran.KatashikiScd7
                                .Cells(rowNo, .Columns("SHIMUKE").Index).Value = baseSeisakuIchiran.KatashikiShimuke
                                .Cells(rowNo, .Columns("OP").Index).Value = Trim(baseSeisakuIchiran.KatashikiOp)
                                .Cells(rowNo, .Columns("GAISO_SHOKU").Index).Value = baseSeisakuIchiran.Gaisousyoku
                                .Cells(rowNo, .Columns("GAISO_SHOKU_NAME").Index).Value = baseSeisakuIchiran.GaisousyokuName
                                .Cells(rowNo, .Columns("NAISO_SHOKU").Index).Value = baseSeisakuIchiran.Naisousyoku
                                .Cells(rowNo, .Columns("NAISO_SHOKU_NAME").Index).Value = baseSeisakuIchiran.NaisousyokuName
                                '参考情報
                                .Cells(rowNo, .Columns("SEISAKU_SYASYU").Index).Value = baseSeisakuIchiran.Syasyu
                                .Cells(rowNo, .Columns("SEISAKU_GRADE").Index).Value = baseSeisakuIchiran.Grade
                                .Cells(rowNo, .Columns("SEISAKU_SHIMUKE").Index).Value = baseSeisakuIchiran.Shimuke
                                .Cells(rowNo, .Columns("SEISAKU_HANDORU").Index).Value = baseSeisakuIchiran.Handoru & "HD"   'HDを付ける。
                                .Cells(rowNo, .Columns("SEISAKU_EG_HAIKIRYOU").Index).Value = baseSeisakuIchiran.EgHaikiryou
                                .Cells(rowNo, .Columns("SEISAKU_EG_KATASHIKI").Index).Value = baseSeisakuIchiran.EgKatashiki

                                If StringUtil.Equals(baseSeisakuIchiran.EgKakyuuki, "ﾀｰﾎﾞ") Then
                                    .Cells(rowNo, .Columns("SEISAKU_EG_KAKYUUKI").Index).Value = "B"
                                Else
                                    .Cells(rowNo, .Columns("SEISAKU_EG_KAKYUUKI").Index).Value = baseSeisakuIchiran.EgKakyuuki
                                End If
                                .Cells(rowNo, .Columns("SEISAKU_TM_KUDOU").Index).Value = baseSeisakuIchiran.TmKudou
                                .Cells(rowNo, .Columns("SEISAKU_TM_HENSOKUKI").Index).Value = baseSeisakuIchiran.TmHensokuki
                                .Cells(rowNo, .Columns("SEISAKU_SYATAI_NO").Index).Value = baseSeisakuIchiran.SyataiNo
                            End With

                            'ベース参考情報
                            With spdBaseTenkaiCar_Sheet1
                                .Cells(rowNo, .Columns("KAIHATSU_FUGO").Index).Value = baseSeisakuIchiran.KaihatsuFugo
                                .Cells(rowNo, .Columns("SHIYO_JOHO_NO").Index).Value = baseSeisakuIchiran.ShiyoujyouhouNo
                                If StringUtil.IsNotEmpty(GetValue) Then
                                    .Cells(rowNo, .Columns("APPLIED_NO").Index).Value = GetValue.AppliedNo
                                End If
                                .Cells(rowNo, .Columns("KATASHIKI").Index).Value = baseSeisakuIchiran.KatashikiScd7
                                .Cells(rowNo, .Columns("SHIMUKE").Index).Value = baseSeisakuIchiran.KatashikiShimuke
                                .Cells(rowNo, .Columns("OP").Index).Value = Trim(baseSeisakuIchiran.KatashikiOp)
                                .Cells(rowNo, .Columns("GAISO_SHOKU").Index).Value = baseSeisakuIchiran.Gaisousyoku
                                .Cells(rowNo, .Columns("GAISO_SHOKU_NAME").Index).Value = baseSeisakuIchiran.GaisousyokuName
                                .Cells(rowNo, .Columns("NAISO_SHOKU").Index).Value = baseSeisakuIchiran.Naisousyoku
                                .Cells(rowNo, .Columns("NAISO_SHOKU_NAME").Index).Value = baseSeisakuIchiran.NaisousyokuName
                            End With
                        Else
                            'TODO:今後の課題 製作一覧のイベントととの連携が現在されていない
                            With spdBaseCar_Sheet1
                                .Cells(rowNo, .Columns("BASE_EVENT_CODE").Index).Value = ""
                                .Cells(rowNo, .Columns("BASE_GOSHA").Index).Value = ""
                                .Cells(rowNo, .Columns("SEISAKU_SYATAI_NO").Index).Value = baseSeisakuIchiran.SyataiNo
                            End With
                        End If



                    End If
                End If

                '号車が入力されている列が対象
                '   種別はＷ（ＷＢ車情報）
                If StringUtil.IsNotEmpty(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value) And _
                    StringUtil.Equals(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("SHUBETSU").Index).Value, "W") Then

                    ''2015/08/14 変更 E.Ubukata Ver.2.11.0
                    '' 検索条件が変更されないのでループ内で何度もDBアクセスする必要がないためループ外へ移動
                    'Dim Ichiran = New SeisakuIchiranDaoImpl

                    '初期設定
                    Dim baseSeisakuIchiran = New TSeisakuIchiranWbVo

                    '製作一覧から設定
                    Dim strGousya As String = ""

                    ''2015/08/14 変更 E.Ubukata Ver.2.11.0
                    '' 検索条件が変更されないのでループ内で何度もDBアクセスする必要がないためループ外へ移動
                    ''完成車情報
                    'wbList = Ichiran.GetTSeisakuIchiranWb(seisakuHakouNo, seisakuHakouNoKaiteiNo)

                    '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                    '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                    Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                       spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value)
                    For Each voSeisakuWb As TSeisakuIchiranWbVo In wbList

                        '2014/02/14
                        '   製作一覧上でWが付いている場合があるので下記の処理に変更する。
                        'Dim strSeisakuGousya As String = voSeisakuWb.Gousya.PadLeft(4, "0")
                        Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                           voSeisakuWb.Gousya)

                        '製作一覧の号車が試作イベントの号車を含むなら
                        If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                            strGousya = voSeisakuWb.Gousya
                            Exit For
                        End If
                    Next


                    'ＷＢ車情報
                    baseSeisakuIchiran = Ichiran.GetTSeisakuIchiranWbGousya(seisakuHakouNo, seisakuHakouNoKaiteiNo, strGousya)

                    If StringUtil.IsNotEmpty(baseSeisakuIchiran) Then
                        ''2015/09/25 変更 E.Ubukata Ver.2.11.3
                        '' 試作イベント情報が存在する場合はeBom側の情報を表示せずイベントと号車を表示する
                        'アプライド№を７ケタ型式から取得
                        Dim GetValue = getSobiKaitei.FindSobiKaitei(baseSeisakuIchiran.KaihatsuFugo, _
                                                                    baseSeisakuIchiran.ShiyoujyouhouNo, _
                                                                    baseSeisakuIchiran.KatashikiScd7)
                        With spdBaseCar_Sheet1

                            .Cells(rowNo, .Columns("KAIHATSU_FUGO").Index).Value = baseSeisakuIchiran.KaihatsuFugo
                            .Cells(rowNo, .Columns("SHIYO_JOHO_NO").Index).Value = baseSeisakuIchiran.ShiyoujyouhouNo

                            If StringUtil.IsNotEmpty(GetValue) Then
                                .Cells(rowNo, .Columns("APPLIED_NO").Index).Value = GetValue.AppliedNo
                            End If

                            .Cells(rowNo, .Columns("KATASHIKI").Index).Value = baseSeisakuIchiran.KatashikiScd7
                            .Cells(rowNo, .Columns("SHIMUKE").Index).Value = baseSeisakuIchiran.KatashikiShimuke
                            .Cells(rowNo, .Columns("OP").Index).Value = Trim(baseSeisakuIchiran.KatashikiOp)
                            .Cells(rowNo, .Columns("GAISO_SHOKU").Index).Value = baseSeisakuIchiran.Gaisousyoku
                            .Cells(rowNo, .Columns("GAISO_SHOKU_NAME").Index).Value = baseSeisakuIchiran.GaisousyokuName
                            .Cells(rowNo, .Columns("NAISO_SHOKU").Index).Value = baseSeisakuIchiran.Naisousyoku
                            .Cells(rowNo, .Columns("NAISO_SHOKU_NAME").Index).Value = baseSeisakuIchiran.NaisousyokuName
                            '参考情報
                            .Cells(rowNo, .Columns("SEISAKU_SYASYU").Index).Value = baseSeisakuIchiran.Syasyu
                            .Cells(rowNo, .Columns("SEISAKU_GRADE").Index).Value = baseSeisakuIchiran.Grade
                            .Cells(rowNo, .Columns("SEISAKU_SHIMUKE").Index).Value = baseSeisakuIchiran.Shimuke
                            .Cells(rowNo, .Columns("SEISAKU_HANDORU").Index).Value = baseSeisakuIchiran.Handoru & "HD"   'HDを付ける。
                            .Cells(rowNo, .Columns("SEISAKU_EG_HAIKIRYOU").Index).Value = baseSeisakuIchiran.EgHaikiryou
                            .Cells(rowNo, .Columns("SEISAKU_TM_HENSOKUKI").Index).Value = baseSeisakuIchiran.TmHensokuki

                        End With

                        'ベース参考情報
                        With spdBaseTenkaiCar_Sheet1
                            .Cells(rowNo, .Columns("KAIHATSU_FUGO").Index).Value = baseSeisakuIchiran.KaihatsuFugo
                            .Cells(rowNo, .Columns("SHIYO_JOHO_NO").Index).Value = baseSeisakuIchiran.ShiyoujyouhouNo
                            If StringUtil.IsNotEmpty(GetValue) Then
                                .Cells(rowNo, .Columns("APPLIED_NO").Index).Value = GetValue.AppliedNo
                            End If
                            .Cells(rowNo, .Columns("KATASHIKI").Index).Value = baseSeisakuIchiran.KatashikiScd7
                            .Cells(rowNo, .Columns("SHIMUKE").Index).Value = baseSeisakuIchiran.KatashikiShimuke
                            .Cells(rowNo, .Columns("OP").Index).Value = Trim(baseSeisakuIchiran.KatashikiOp)
                            .Cells(rowNo, .Columns("GAISO_SHOKU").Index).Value = baseSeisakuIchiran.Gaisousyoku
                            .Cells(rowNo, .Columns("GAISO_SHOKU_NAME").Index).Value = baseSeisakuIchiran.GaisousyokuName
                            .Cells(rowNo, .Columns("NAISO_SHOKU").Index).Value = baseSeisakuIchiran.Naisousyoku
                            .Cells(rowNo, .Columns("NAISO_SHOKU_NAME").Index).Value = baseSeisakuIchiran.NaisousyokuName
                        End With

                    End If
                End If
            Next
        End Sub

        '完成車情報セット
        Private Sub setKanseiSheet()
            '製作一覧HD情報取得
            'Dim strGousya As String
            Dim getSeisakuIchiranHd As New SeisakuIchiranDaoImpl
            Dim tSeisakuHakouHdVo As New TSeisakuHakouHdVo

            Dim kanseiList As New List(Of TSeisakuIchiranKanseiVo)
            Dim wbList As New List(Of TSeisakuIchiranWbVo)

            tSeisakuHakouHdVo = getSeisakuIchiranHd.GetSeisakuIchiranHd(seisakuHakouNo, seisakuHakouNoKaiteiNo)

            'E/G、T/Mのメモらヘッダー
            If StringUtil.IsNotEmpty(tSeisakuHakouHdVo) Then
                With spdCompleteCar_Sheet1
                    If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiEgMemo1) Then
                        .Cells(2, .Columns("EG_MEMO_1").Index).Value = tSeisakuHakouHdVo.KanseiEgMemo1
                    End If
                    If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiEgMemo2) Then
                        .Cells(2, .Columns("EG_MEMO_2").Index).Value = tSeisakuHakouHdVo.KanseiEgMemo2
                    End If
                    If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiTmMemo1) Then
                        .Cells(2, .Columns("TM_MEMO_1").Index).Value = tSeisakuHakouHdVo.KanseiTmMemo1
                    End If
                    If StringUtil.IsNotEmpty(tSeisakuHakouHdVo.KanseiTmMemo2) Then
                        .Cells(2, .Columns("TM_MEMO_2").Index).Value = tSeisakuHakouHdVo.KanseiTmMemo2
                    End If
                End With
            End If


            Dim Ichiran = New SeisakuIchiranDaoImpl
            '完成車情報
            kanseiList = Ichiran.GetTSeisakuIchiranKansei(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            'ＷＢ車情報
            wbList = Ichiran.GetTSeisakuIchiranWb(seisakuHakouNo, seisakuHakouNoKaiteiNo)

            For rowNo As Integer = 3 To spdCompleteCar_Sheet1.RowCount - 1

                If StringUtil.IsEmpty(spdCompleteCar_Sheet1.Cells(rowNo, spdCompleteCar_Sheet1.Columns("GOSHA").Index).Value) Then Continue For

                '号車が入力されている列が対象
                '   種別はブランク（完成車情報）
                If StringUtil.IsNotEmpty(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value) And _
                    StringUtil.IsEmpty(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("SHUBETSU").Index).Value) Then

                    '初期設定
                    'Dim Ichiran = New SeisakuIchiranDaoImpl
                    Dim kanseiSeisakuIchiran = New TSeisakuIchiranKanseiVo

                    '製作一覧から設定
                    Dim strGousya As String = ""
                    ''完成車情報
                    'kanseiList = Ichiran.GetTSeisakuIchiranKansei(seisakuHakouNo, seisakuHakouNoKaiteiNo)
                    '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                    '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                    Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                       spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value)
                    For Each voSeisakuKansei As TSeisakuIchiranKanseiVo In kanseiList

                        '2014/02/14
                        '   製作一覧上でWが付いている場合があるので下記の処理に変更する。
                        'Dim strSeisakuGousya As String = voSeisakuKansei.Gousya.PadLeft(4, "0")
                        Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                           voSeisakuKansei.Gousya)

                        '製作一覧の号車が試作イベントの号車を含むなら
                        If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                            strGousya = voSeisakuKansei.Gousya
                            Exit For
                        End If
                    Next

                    '完成車情報
                    kanseiSeisakuIchiran = Ichiran.GetTSeisakuIchiranKanseiGousya(seisakuHakouNo, seisakuHakouNoKaiteiNo, strGousya)

                    If StringUtil.IsNotEmpty(kanseiSeisakuIchiran) Then
                        With spdCompleteCar_Sheet1
                            .Cells(rowNo, .Columns("SHAGATA").Index).Value = kanseiSeisakuIchiran.Syasyu
                            .Cells(rowNo, .Columns("GRADE").Index).Value = kanseiSeisakuIchiran.Grade
                            .Cells(rowNo, .Columns("SHIMUKECHI_SHIMUKE").Index).Value = kanseiSeisakuIchiran.Shimuke
                            If StringUtil.Equals(kanseiSeisakuIchiran.Handoru, "L") Or _
                                StringUtil.Equals(kanseiSeisakuIchiran.Handoru, "R") Then
                                .Cells(rowNo, .Columns("HANDLE").Index).Value = kanseiSeisakuIchiran.Handoru & "HD" 'HDを付ける。
                            Else
                                .Cells(rowNo, .Columns("HANDLE").Index).Value = ""
                            End If

                            '型式は取込不要
                            .Cells(rowNo, .Columns("EG_KATASHIKI").Index).Value = ""
                            .Cells(rowNo, .Columns("EG_HAIKIRYO").Index).Value = kanseiSeisakuIchiran.EgHaikiryou
                            .Cells(rowNo, .Columns("EG_SYSTEM").Index).Value = kanseiSeisakuIchiran.EgKatashiki
                            If StringUtil.Equals(kanseiSeisakuIchiran.EgKakyuuki, "ﾀｰﾎﾞ") Then
                                .Cells(rowNo, .Columns("EG_KAKYUKI").Index).Value = "B"
                            Else
                                .Cells(rowNo, .Columns("EG_KAKYUKI").Index).Value = kanseiSeisakuIchiran.EgKakyuuki
                            End If
                            .Cells(rowNo, .Columns("EG_MEMO_1").Index).Value = kanseiSeisakuIchiran.EgEgName
                            .Cells(rowNo, .Columns("EG_MEMO_2").Index).Value = kanseiSeisakuIchiran.EgIss
                            .Cells(rowNo, .Columns("TM_KUDO").Index).Value = kanseiSeisakuIchiran.TmKudou
                            .Cells(rowNo, .Columns("TM_HENSOKUKI").Index).Value = kanseiSeisakuIchiran.TmHensokuki
                            .Cells(rowNo, .Columns("TM_FUKU_HENSOKUKI").Index).Value = ""
                            .Cells(rowNo, .Columns("TM_MEMO_1").Index).Value = kanseiSeisakuIchiran.TmTmName
                            .Cells(rowNo, .Columns("TM_MEMO_2").Index).Value = kanseiSeisakuIchiran.TmRdGiahi
                            .Cells(rowNo, .Columns("KATASHIKI").Index).Value = kanseiSeisakuIchiran.KatashikiScd7
                            .Cells(rowNo, .Columns("SHIMUKE").Index).Value = kanseiSeisakuIchiran.KatashikiShimuke
                            .Cells(rowNo, .Columns("OP").Index).Value = kanseiSeisakuIchiran.KatashikiOp
                            .Cells(rowNo, .Columns("GAISO_SHOKU").Index).Value = kanseiSeisakuIchiran.Gaisousyoku
                            .Cells(rowNo, .Columns("GAISO_SHOKU_NAME").Index).Value = kanseiSeisakuIchiran.GaisousyokuName
                            .Cells(rowNo, .Columns("NAISO_SHOKU").Index).Value = kanseiSeisakuIchiran.Naisousyoku
                            .Cells(rowNo, .Columns("NAISO_SHOKU_NAME").Index).Value = kanseiSeisakuIchiran.NaisousyokuName
                            .Cells(rowNo, .Columns("SHADAI_NO").Index).Value = kanseiSeisakuIchiran.SyataiNo
                            .Cells(rowNo, .Columns("SHIYOU_MOKUTEKI").Index).Value = kanseiSeisakuIchiran.ShiyouMokuteki
                            .Cells(rowNo, .Columns("SHIKEN_MOKUTEKI").Index).Value = kanseiSeisakuIchiran.SyuyoukakuninKoumoku
                            .Cells(rowNo, .Columns("SHIYO_BUSHO").Index).Value = kanseiSeisakuIchiran.ShiyouBusyo
                            .Cells(rowNo, .Columns("GROUP").Index).Value = kanseiSeisakuIchiran.SeisakuGroup
                            .Cells(rowNo, .Columns("SEISAKU_JUNJYO").Index).Value = kanseiSeisakuIchiran.SeisakuJunjyo
                            '日付ならセット
                            If IsDate(kanseiSeisakuIchiran.KanseiKibouBi) Then
                                Dim dt As DateTime = DateTime.Parse(StrConv(kanseiSeisakuIchiran.KanseiKibouBi, VbStrConv.Narrow))
                                .Cells(rowNo, .Columns("KANSEIBI").Index).Value = dt
                            End If
                            .Cells(rowNo, .Columns("KOSHI_NO").Index).Value = ""
                            .Cells(rowNo, .Columns("SEISAKU_HOUHOU_KBN").Index).Value = kanseiSeisakuIchiran.SeisakuHouhouKbn
                            .Cells(rowNo, .Columns("SEISAKU_HOUHOU").Index).Value = kanseiSeisakuIchiran.SeisakuHouhou
                            .Cells(rowNo, .Columns("SHISAKU_MEMO").Index).Value = kanseiSeisakuIchiran.Memo
                        End With
                    End If
                End If

                '号車が入力されている列が対象
                '   種別はＷ（ＷＢ車情報）
                If StringUtil.IsNotEmpty(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value) And _
                    StringUtil.Equals(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("SHUBETSU").Index).Value, "W") Then

                    '初期設定
                    'Dim Ichiran = New SeisakuIchiranDaoImpl
                    Dim kanseiSeisakuIchiran = New TSeisakuIchiranWbVo

                    '製作一覧から設定
                    Dim strGousya As String = ""
                    ''ＷＢ車情報
                    'wbList = Ichiran.GetTSeisakuIchiranWb(seisakuHakouNo, seisakuHakouNoKaiteiNo)
                    '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                    '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                    Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                       spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value)
                    For Each voSeisakuWb As TSeisakuIchiranWbVo In wbList

                        '2014/02/14
                        '   製作一覧上でWが付いている場合があるので下記の処理に変更する。
                        'Dim strSeisakuGousya As String = voSeisakuWb.Gousya.PadLeft(4, "0")
                        Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                           voSeisakuWb.Gousya)

                        '製作一覧の号車が試作イベントの号車を含むなら
                        If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                            strGousya = voSeisakuWb.Gousya
                            Exit For
                        End If
                    Next

                    'ＷＢ車情報
                    kanseiSeisakuIchiran = Ichiran.GetTSeisakuIchiranWbGousya(seisakuHakouNo, seisakuHakouNoKaiteiNo, strGousya)

                    If StringUtil.IsNotEmpty(kanseiSeisakuIchiran) Then
                        With spdCompleteCar_Sheet1
                            .Cells(rowNo, .Columns("SHAGATA").Index).Value = kanseiSeisakuIchiran.Syasyu
                            .Cells(rowNo, .Columns("GRADE").Index).Value = kanseiSeisakuIchiran.Grade
                            .Cells(rowNo, .Columns("SHIMUKECHI_SHIMUKE").Index).Value = kanseiSeisakuIchiran.Shimuke
                            If StringUtil.Equals(kanseiSeisakuIchiran.Handoru, "L") Or _
                                StringUtil.Equals(kanseiSeisakuIchiran.Handoru, "R") Then
                                .Cells(rowNo, .Columns("HANDLE").Index).Value = kanseiSeisakuIchiran.Handoru & "HD" 'HDを付ける。
                            Else
                                .Cells(rowNo, .Columns("HANDLE").Index).Value = ""
                            End If
                            .Cells(rowNo, .Columns("EG_KATASHIKI").Index).Value = ""
                            .Cells(rowNo, .Columns("EG_HAIKIRYO").Index).Value = kanseiSeisakuIchiran.EgHaikiryou
                            .Cells(rowNo, .Columns("EG_SYSTEM").Index).Value = ""
                            .Cells(rowNo, .Columns("EG_KAKYUKI").Index).Value = ""
                            .Cells(rowNo, .Columns("EG_MEMO_1").Index).Value = ""
                            .Cells(rowNo, .Columns("EG_MEMO_2").Index).Value = ""
                            .Cells(rowNo, .Columns("TM_KUDO").Index).Value = ""
                            .Cells(rowNo, .Columns("TM_HENSOKUKI").Index).Value = kanseiSeisakuIchiran.TmHensokuki
                            .Cells(rowNo, .Columns("TM_FUKU_HENSOKUKI").Index).Value = ""
                            .Cells(rowNo, .Columns("TM_MEMO_1").Index).Value = ""
                            .Cells(rowNo, .Columns("TM_MEMO_2").Index).Value = ""
                            .Cells(rowNo, .Columns("KATASHIKI").Index).Value = kanseiSeisakuIchiran.KatashikiScd7
                            .Cells(rowNo, .Columns("SHIMUKE").Index).Value = kanseiSeisakuIchiran.KatashikiShimuke
                            .Cells(rowNo, .Columns("OP").Index).Value = kanseiSeisakuIchiran.KatashikiOp
                            .Cells(rowNo, .Columns("GAISO_SHOKU").Index).Value = kanseiSeisakuIchiran.Gaisousyoku
                            .Cells(rowNo, .Columns("GAISO_SHOKU_NAME").Index).Value = kanseiSeisakuIchiran.GaisousyokuName
                            .Cells(rowNo, .Columns("NAISO_SHOKU").Index).Value = kanseiSeisakuIchiran.Naisousyoku
                            .Cells(rowNo, .Columns("NAISO_SHOKU_NAME").Index).Value = kanseiSeisakuIchiran.NaisousyokuName
                            .Cells(rowNo, .Columns("SHADAI_NO").Index).Value = kanseiSeisakuIchiran.SyataiNo
                            .Cells(rowNo, .Columns("SHIYOU_MOKUTEKI").Index).Value = kanseiSeisakuIchiran.ShiyouMokuteki
                            .Cells(rowNo, .Columns("SHIKEN_MOKUTEKI").Index).Value = kanseiSeisakuIchiran.SyuyoukakuninKoumoku
                            .Cells(rowNo, .Columns("SHIYO_BUSHO").Index).Value = kanseiSeisakuIchiran.ShiyouBusyo
                            .Cells(rowNo, .Columns("GROUP").Index).Value = kanseiSeisakuIchiran.SeisakuGroup
                            .Cells(rowNo, .Columns("SEISAKU_JUNJYO").Index).Value = kanseiSeisakuIchiran.SeisakuJunjyo

                            '日付ならセット
                            If IsDate(kanseiSeisakuIchiran.KanseiKibouBi) Then
                                Dim dt As DateTime = DateTime.Parse(StrConv(kanseiSeisakuIchiran.KanseiKibouBi, VbStrConv.Narrow))
                                .Cells(rowNo, .Columns("KANSEIBI").Index).Value = dt
                            End If

                            .Cells(rowNo, .Columns("KOSHI_NO").Index).Value = ""
                            .Cells(rowNo, .Columns("SEISAKU_HOUHOU_KBN").Index).Value = ""
                            .Cells(rowNo, .Columns("SEISAKU_HOUHOU").Index).Value = ""
                            .Cells(rowNo, .Columns("SHISAKU_MEMO").Index).Value = kanseiSeisakuIchiran.Memo
                        End With
                    End If
                End If
            Next
        End Sub

        '基本装備仕様情報セット
        Private Sub setBasicSheet()

            '初期設定
            Dim BasicOptionSeisakuIchiran = New List(Of TSeisakuIchiranOpkoumokuVo)
            Dim Ichiran = New SeisakuIchiranDaoImpl
            Dim OPList As New List(Of TSeisakuIchiranOpkoumokuVo)
            Dim columntCnt As Integer = 2
            'OP情報(完成車／ＷＢ車でグループ化)
            OPList = Ichiran.GetTSeisakuIchiranOpkoumoku(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            '列情報をセットする。
            For Each vo As TSeisakuIchiranOpkoumokuVo In OPList
                spdBasicOption_Sheet1.Cells(0, columntCnt).Value = vo.KaihatsuFugo
                spdBasicOption_Sheet1.Cells(1, columntCnt).Value = vo.OpSpecCode
                spdBasicOption_Sheet1.Cells(2, columntCnt).Value = vo.OpName
                columntCnt = columntCnt + 1
            Next

            '製作一覧HD情報
            Dim getSeisakuIchiranHd As New SeisakuIchiranDaoImpl
            Dim tSeisakuHakouHdVo As New TSeisakuHakouHdVo
            tSeisakuHakouHdVo = getSeisakuIchiranHd.GetSeisakuIchiranHd(seisakuHakouNo, seisakuHakouNoKaiteiNo)

            '製作一覧HD情報取得
            'Dim strGousya As String
            Dim strSyubetu As String

            Dim kanseiList As New List(Of TSeisakuIchiranKanseiVo)
            Dim wbList As New List(Of TSeisakuIchiranWbVo)

            Dim strDai, strVoDai As String
            Dim strChu, strVoChu As String
            Dim strSho, strVoSho As String
            '
            'ＷＢ車情報
            wbList = Ichiran.GetTSeisakuIchiranWb(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            '完成車情報
            kanseiList = Ichiran.GetTSeisakuIchiranKansei(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            For rowNo As Integer = 3 To spdBasicOption_Sheet1.RowCount - 1

                If StringUtil.IsEmpty(spdBasicOption_Sheet1.Cells(rowNo, spdBasicOption_Sheet1.Columns("GOSHA").Index).Value) Then Continue For

                '号車が入力されている列が対象
                '   種別はブランク（完成車情報）
                If StringUtil.IsNotEmpty(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value) Then

                    '製作一覧から設定
                    Dim strGousya As String = ""

                    If StringUtil.Equals(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("SHUBETSU").Index).Value, "W") Then
                        strSyubetu = "W"
                        ''ＷＢ車情報
                        'wbList = Ichiran.GetTSeisakuIchiranWb(seisakuHakouNo, seisakuHakouNoKaiteiNo)
                        '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                        '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                        Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                           spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value)
                        For Each voSeisakuWb As TSeisakuIchiranWbVo In wbList

                            '2014/02/14
                            '   製作一覧上でWが付いている場合があるので下記の処理に変更する。
                            'Dim strSeisakuGousya As String = voSeisakuWb.Gousya.PadLeft(4, "0")
                            Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                               voSeisakuWb.Gousya)

                            '製作一覧の号車が試作イベントの号車を含むなら
                            If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                                strGousya = voSeisakuWb.Gousya
                                Exit For
                            End If
                        Next
                    Else
                        strSyubetu = "C"
                        ''完成車情報
                        'kanseiList = Ichiran.GetTSeisakuIchiranKansei(seisakuHakouNo, seisakuHakouNoKaiteiNo)
                        '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                        '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                        Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                           spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value)

                        For Each voSeisakuKansei As TSeisakuIchiranKanseiVo In kanseiList
                            '2014/02/14
                            '   製作一覧上でWが付いている場合があるので下記の処理に変更する。
                            'Dim strSeisakuGousya As String = voSeisakuKansei.Gousya.PadLeft(4, "0")
                            Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                               voSeisakuKansei.Gousya)
                            '製作一覧の号車が試作イベントの号車を含むなら
                            If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                                strGousya = voSeisakuKansei.Gousya
                                Exit For
                            End If
                        Next
                    End If

                    '基本装備仕様情報
                    BasicOptionSeisakuIchiran = Ichiran.GetTSeisakuIchiranOpkoumokuGousya( _
                                                    seisakuHakouNo, seisakuHakouNoKaiteiNo, strSyubetu, strGousya)

                    If StringUtil.IsNotEmpty(BasicOptionSeisakuIchiran) Then

                        For Each vo As TSeisakuIchiranOpkoumokuVo In BasicOptionSeisakuIchiran
                            '該当箇所の適用に値をセット
                            For columnNo As Integer = 2 To spdBasicOption_Sheet1.ColumnCount - 1

                                '変数に退避
                                If StringUtil.IsEmpty(spdBasicOption_Sheet1.Cells(0, columnNo).Value) Then
                                    strDai = Nothing
                                Else
                                    strDai = spdBasicOption_Sheet1.Cells(0, columnNo).Value
                                End If
                                If StringUtil.IsEmpty(spdBasicOption_Sheet1.Cells(1, columnNo).Value) Then
                                    strChu = Nothing
                                Else
                                    strChu = spdBasicOption_Sheet1.Cells(1, columnNo).Value
                                End If
                                If StringUtil.IsEmpty(spdBasicOption_Sheet1.Cells(2, columnNo).Value) Then
                                    strSho = Nothing
                                Else
                                    strSho = spdBasicOption_Sheet1.Cells(2, columnNo).Value
                                End If
                                '
                                If StringUtil.IsEmpty(vo.KaihatsuFugo) Then
                                    strVoDai = Nothing
                                Else
                                    strVoDai = vo.KaihatsuFugo
                                End If
                                If StringUtil.IsEmpty(vo.OpSpecCode) Then
                                    strVoChu = Nothing
                                Else
                                    strVoChu = vo.OpSpecCode
                                End If
                                If StringUtil.IsEmpty(vo.OpName) Then
                                    strVoSho = Nothing
                                Else
                                    strVoSho = vo.OpName
                                End If

                                If StringUtil.Equals(strDai, strVoDai) And _
                                    StringUtil.Equals(strChu, strVoChu) And _
                                    StringUtil.Equals(strSho, strVoSho) Then
                                    spdBasicOption_Sheet1.Cells(rowNo, columnNo).Value = vo.Tekiyou
                                End If
                            Next

                        Next

                    End If

                End If
            Next

        End Sub

        '特別装備仕様情報セット
        Private Sub setSpecialSheet()

            '初期設定
            Dim SpecialOptionSeisakuIchiran = New List(Of TSeisakuTokubetuOrikomiVo)
            Dim SpecialOptionSeisakuIchiranWB = New List(Of TSeisakuWbSoubiShiyouVo)
            Dim TOKUBETUList As New List(Of TSeisakuTokubetuOrikomiVo)
            Dim TOKUBETUListWB As New List(Of TSeisakuWbSoubiShiyouVo)
            Dim Ichiran = New SeisakuIchiranDaoImpl
            Dim columntCnt As Integer = 2
            '特別織込み
            TOKUBETUList = Ichiran.GetTSeisakuIchiranTokubetu(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            '列情報をセットする。
            For Each vo As TSeisakuTokubetuOrikomiVo In TOKUBETUList
                spdSpecialOption_Sheet1.Cells(0, columntCnt).Value = vo.DaiKbnName
                spdSpecialOption_Sheet1.Cells(1, columntCnt).Value = vo.ChuKbnName
                spdSpecialOption_Sheet1.Cells(2, columntCnt).Value = vo.ShoKbnName
                columntCnt = columntCnt + 1
            Next
            '特別織込み（ＷＢ車）
            TOKUBETUListWB = Ichiran.GetTSeisakuIchiranTokubetuWB(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            '列情報をセットする。
            For Each vo As TSeisakuWbSoubiShiyouVo In TOKUBETUListWB
                spdSpecialOption_Sheet1.Cells(0, columntCnt).Value = "W " & vo.DaiKbnName   'WB車と分けるためにヘッダーに付ける
                spdSpecialOption_Sheet1.Cells(1, columntCnt).Value = vo.ChuKbnName
                spdSpecialOption_Sheet1.Cells(2, columntCnt).Value = vo.ShoKbnName
                columntCnt = columntCnt + 1
            Next

            '製作一覧HD情報
            Dim getSeisakuIchiranHd As New SeisakuIchiranDaoImpl
            Dim tSeisakuHakouHdVo As New TSeisakuHakouHdVo
            tSeisakuHakouHdVo = getSeisakuIchiranHd.GetSeisakuIchiranHd(seisakuHakouNo, seisakuHakouNoKaiteiNo)

            Dim kanseiList As New List(Of TSeisakuIchiranKanseiVo)
            Dim wbList As New List(Of TSeisakuIchiranWbVo)


            '製作一覧HD情報取得
            'Dim strGousya As String

            Dim strDai, strVoDai As String
            Dim strChu, strVoChu As String
            Dim strSho, strVoSho As String

            '完成車情報
            kanseiList = Ichiran.GetTSeisakuIchiranKansei(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            '完成車
            For rowNo As Integer = 3 To spdSpecialOption_Sheet1.RowCount - 1

                If StringUtil.IsEmpty(spdSpecialOption_Sheet1.Cells(rowNo, spdSpecialOption_Sheet1.Columns("GOSHA").Index).Value) Then Continue For

                '号車が入力されている列が対象
                '   種別はブランク（完成車情報）
                If StringUtil.IsNotEmpty(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value) And _
                    StringUtil.IsEmpty(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("SHUBETSU").Index).Value) Then

                    '製作一覧から設定
                    Dim strGousya As String = ""
                    ''完成車情報
                    'kanseiList = Ichiran.GetTSeisakuIchiranKansei(seisakuHakouNo, seisakuHakouNoKaiteiNo)
                    '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                    '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                    Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                       spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value)
                    For Each voSeisakuKansei As TSeisakuIchiranKanseiVo In kanseiList

                        '2014/02/14
                        '   製作一覧上でWが付いている場合があるので下記の処理に変更する。
                        'Dim strSeisakuGousya As String = voSeisakuKansei.Gousya.PadLeft(4, "0")
                        Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                           voSeisakuKansei.Gousya)

                        '製作一覧の号車が試作イベントの号車を含むなら
                        If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                            strGousya = voSeisakuKansei.Gousya
                            Exit For
                        End If
                    Next

                    '特別装備仕様情報
                    SpecialOptionSeisakuIchiran = Ichiran.GetTSeisakuIchiranTokubetuGousya( _
                                                    seisakuHakouNo, seisakuHakouNoKaiteiNo, strGousya)

                    If StringUtil.IsNotEmpty(SpecialOptionSeisakuIchiran) Then

                        For Each vo As TSeisakuTokubetuOrikomiVo In SpecialOptionSeisakuIchiran


                            '該当箇所の適用に値をセット
                            For columnNo As Integer = 2 To spdSpecialOption_Sheet1.ColumnCount - 1

                                '変数に退避
                                If StringUtil.IsEmpty(spdSpecialOption_Sheet1.Cells(0, columnNo).Value) Then
                                    strDai = Nothing
                                Else
                                    strDai = spdSpecialOption_Sheet1.Cells(0, columnNo).Value
                                End If
                                If StringUtil.IsEmpty(spdSpecialOption_Sheet1.Cells(1, columnNo).Value) Then
                                    strChu = Nothing
                                Else
                                    strChu = spdSpecialOption_Sheet1.Cells(1, columnNo).Value
                                End If
                                If StringUtil.IsEmpty(spdSpecialOption_Sheet1.Cells(2, columnNo).Value) Then
                                    strSho = Nothing
                                Else
                                    strSho = spdSpecialOption_Sheet1.Cells(2, columnNo).Value
                                End If
                                '
                                If StringUtil.IsEmpty(vo.DaiKbnName) Then
                                    strVoDai = Nothing
                                Else
                                    strVoDai = vo.DaiKbnName
                                End If
                                If StringUtil.IsEmpty(vo.ChuKbnName) Then
                                    strVoChu = Nothing
                                Else
                                    strVoChu = vo.ChuKbnName
                                End If
                                If StringUtil.IsEmpty(vo.ShoKbnName) Then
                                    strVoSho = Nothing
                                Else
                                    strVoSho = vo.ShoKbnName
                                End If
                                '比較
                                If StringUtil.Equals(strDai, strVoDai) And _
                                    StringUtil.Equals(strChu, strVoChu) And _
                                    StringUtil.Equals(strSho, strVoSho) Then
                                    spdSpecialOption_Sheet1.Cells(rowNo, columnNo).Value = vo.Tekiyou
                                End If
                            Next

                        Next

                    End If

                End If
            Next

            'ＷＢ車情報
            wbList = Ichiran.GetTSeisakuIchiranWb(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            'ＷＢ車
            For rowNo As Integer = 3 To spdSpecialOption_Sheet1.RowCount - 1

                If StringUtil.IsEmpty(spdSpecialOption_Sheet1.Cells(rowNo, spdSpecialOption_Sheet1.Columns("GOSHA").Index).Value) Then Continue For

                '号車が入力されている列が対象
                '   種別はブランク（完成車情報）
                If StringUtil.IsNotEmpty(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value) And _
                    StringUtil.Equals(spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("SHUBETSU").Index).Value, "W") Then

                    '製作一覧から設定
                    Dim strGousya As String = ""
                    ''ＷＢ車情報
                    'wbList = Ichiran.GetTSeisakuIchiranWb(seisakuHakouNo, seisakuHakouNoKaiteiNo)
                    '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                    '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                    Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                       spdBaseCar_Sheet1.Cells(rowNo, spdBaseCar_Sheet1.Columns("GOSHA").Index).Value)
                    For Each voSeisakuWb As TSeisakuIchiranWbVo In wbList

                        '2014/02/14
                        '   製作一覧上でWが付いている場合があるので下記の処理に変更する。
                        'Dim strSeisakuGousya As String = voSeisakuWb.Gousya.PadLeft(4, "0")
                        Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                           voSeisakuWb.Gousya)

                        '製作一覧の号車が試作イベントの号車を含むなら
                        If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                            strGousya = voSeisakuWb.Gousya
                            Exit For
                        End If
                    Next

                    '特別装備仕様情報
                    SpecialOptionSeisakuIchiranWB = Ichiran.GetTSeisakuIchiranTokubetuGousyaWB( _
                                                    seisakuHakouNo, seisakuHakouNoKaiteiNo, strGousya)

                    If StringUtil.IsNotEmpty(SpecialOptionSeisakuIchiranWB) Then

                        For Each vo As TSeisakuWbSoubiShiyouVo In SpecialOptionSeisakuIchiranWB
                            '該当箇所の適用に値をセット
                            For columnNo As Integer = 2 To spdSpecialOption_Sheet1.ColumnCount - 1

                                '変数に退避
                                If StringUtil.IsEmpty(spdSpecialOption_Sheet1.Cells(0, columnNo).Value) Then
                                    strDai = Nothing
                                Else
                                    strDai = spdSpecialOption_Sheet1.Cells(0, columnNo).Value
                                End If
                                If StringUtil.IsEmpty(spdSpecialOption_Sheet1.Cells(1, columnNo).Value) Then
                                    strChu = Nothing
                                Else
                                    strChu = spdSpecialOption_Sheet1.Cells(1, columnNo).Value
                                End If
                                If StringUtil.IsEmpty(spdSpecialOption_Sheet1.Cells(2, columnNo).Value) Then
                                    strSho = Nothing
                                Else
                                    strSho = spdSpecialOption_Sheet1.Cells(2, columnNo).Value
                                End If
                                '
                                If StringUtil.IsEmpty(vo.DaiKbnName) Then
                                    strVoDai = Nothing
                                Else
                                    strVoDai = vo.DaiKbnName
                                End If
                                If StringUtil.IsEmpty(vo.ChuKbnName) Then
                                    strVoChu = Nothing
                                Else
                                    strVoChu = vo.ChuKbnName
                                End If
                                If StringUtil.IsEmpty(vo.ShoKbnName) Then
                                    strVoSho = Nothing
                                Else
                                    strVoSho = vo.ShoKbnName
                                End If
                                '比較
                                If StringUtil.Equals(strDai, "W " & strVoDai) And _
                                    StringUtil.Equals(strChu, strVoChu) And _
                                    StringUtil.Equals(strSho, strVoSho) Then
                                    spdSpecialOption_Sheet1.Cells(rowNo, columnNo).Value = vo.Tekiyou
                                End If
                            Next

                        Next

                    End If

                End If
            Next

        End Sub

#Region "送信用画面作成"

        Private Sub autoMailSend(ByVal strFileName As String)

            '宛先情報を取得
            Dim MailList As List(Of SendMailUserAddressVo)
            Dim SendList As List(Of SendMailUserAddressVo)
            Dim MailUser As New SendMailUserAddressVo
            'ログインユーザーのメールアドレスを取得
            MailUser = getMailAddressLoginUser()
            '宛先のメールアドレスを取得
            Dim sendDao As SeisakuIchiranDao = New SeisakuIchiranDaoImpl()
            SendList = sendDao.GetShisakuSekkeiBlockTanto(subject.ShisakuEventCode)
            'メッセージ作成
            Dim dspMsg As String = ""
            'メールタイトル
            Dim title As String = ""
            'メール宛先
            Dim sendName As String = ""

            '下記の全てのシートで変更箇所が無い場合、
            'メッセージを切り替える。
            '   ベース車情報
            '   完成車情報
            '   基本装備仕様情報
            '   特別装備仕様情報
            If StringUtil.IsEmpty(baseUpdateFlg) And _
                StringUtil.IsEmpty(kanseiUpdateFlg) And _
                StringUtil.IsEmpty(basicUpdateFlg) And _
                StringUtil.IsEmpty(specialUpdateFlg) Then

                'タイトル
                title = "★試作部品表 変更なし★＜ "

                'メッセージ指定
                dspMsg = "変更箇所はありません。" + vbLf + vbLf
                dspMsg = dspMsg + "≪対象イベント：" + subject.HeaderSubject.ShisakuKaihatsuFugo + _
                                  " - " + subject.HeaderSubject.ShisakuEventName + "≫" + vbLf
                dspMsg = dspMsg + "≪イベントコード：" + subject.ShisakuEventCode + "≫" + vbLf + vbLf

            Else
                'タイトル
                title = "★試作部品表 変更内容確認のお願い★＜ "
                '宛先指定
                sendName = MailUser.UserName()
                'メッセージ指定
                dspMsg = "いつもお世話になっております。" + vbLf + vbLf
                dspMsg = dspMsg + "添付ファイル網掛け部に変更が入りました。" + vbLf
                dspMsg = dspMsg + "新試作手配システムにて試作部品表　設計メンテ内容の見直しをお願いいたします。" + vbLf + vbLf
                dspMsg = dspMsg + "≪対象イベント：" + subject.HeaderSubject.ShisakuKaihatsuFugo + _
                                  " - " + subject.HeaderSubject.ShisakuEventName + "≫" + vbLf
                dspMsg = dspMsg + "≪イベントコード：" + subject.ShisakuEventCode + "≫" + vbLf + vbLf
                dspMsg = dspMsg + "【変更内容】" + vbLf
                'ベース車情報
                If StringUtil.IsNotEmpty(baseUpdateFlg) Then
                    dspMsg = dspMsg + baseUpdateMsg + vbLf + vbLf
                End If
                '完成車情報
                If StringUtil.IsNotEmpty(kanseiUpdateFlg) Then
                    dspMsg = dspMsg + kanseiUpdateMsg + vbLf + vbLf
                End If
                '基本装備仕様情報
                If StringUtil.IsNotEmpty(basicUpdateFlg) Then
                    dspMsg = dspMsg + basicUpdateMsg + vbLf + vbLf
                End If
                '特別装備仕様情報
                If StringUtil.IsNotEmpty(specialUpdateFlg) Then
                    dspMsg = dspMsg + specialUpdateMsg + vbLf + vbLf
                End If
            End If
            '
            'OutlookApplicationで送信ウインドウを開く
            Dim Ws, oMsg
            Ws = CreateObject("Outlook.Application") 'アプリケーションオブジェクト
            oMsg = Ws.CreateItem(0) '---------------------------メールオブジェクト
            oMsg.To = sendName  '------------------------------------- 宛先アドレス
            'メールアドレス取得
            Dim strBccAddress As String = ""
            If SendList.Count <> 0 Then
                'メールアドレス取得
                MailList = getMailAddressList(SendList)
                If StringUtil.Equals(MailList, Nothing) Then
                    'MsgBox("データなし")
                ElseIf MailList.Count <> 0 Then
                    For i As Integer = 0 To MailList.Count - 1
                        If i > 0 Then strBccAddress = strBccAddress + ";"
                        strBccAddress = strBccAddress + MailList.Item(i).UserName
                    Next
                End If
            End If
            oMsg.BCC = strBccAddress
            'タイトルに開発符号とイベント名称を設定する。
            oMsg.Subject = title & subject.HeaderSubject.ShisakuKaihatsuFugo & _
                                                              " - " & subject.HeaderSubject.ShisakuEventName & " ＞"
            oMsg.Body = dspMsg '------------------------------- 本文
            oMsg.Attachments.Add(strFileName) '-------------- 添付ファイル
            oMsg.Display() '------------（添付は設定有）---- 編集画面表示→［送信］

            '以下はMSMAPIを利用した旧バージョン。
            '　PCによってはエラーが出る事が有るので使用しない。
            ' 参照の追加で MSMAPI を追加する
            '   複数の送信先を指定する場合には
            '   「RecipIndex」にIndexを設定する。
            '   ※３名へ送信する場合・・・　一人目：１、二人目：２、三人目：３と言うように設定
            'Try
            '    MsgBox("MailUser.UserName" + MailUser.UserName)

            '    Dim intIndex As Integer = 1
            '    MsgBox("New MSMAPI.MAPISessionの前")
            '    Dim mapisettion As New MSMAPI.MAPISession
            '    MsgBox("mapisettion.SignOn()の前")
            '    mapisettion.SignOn()
            '    MsgBox("mapisettion.SignOn()のあと")
            '    Dim mapimessages As New MSMAPI.MAPIMessages
            '    With mapimessages
            '        .SessionID = mapisettion.SessionID

            '        MsgBox("SessionID " + .SessionID)

            '        .Compose()
            '        MsgBox(".Compose ")

            '        .RecipIndex = 0
            '        .RecipType = 1

            '        MsgBox("RecipType " + .RecipType)

            '        .RecipDisplayName = MailUser.UserName
            '        MsgBox("RecipDisplayName " + .RecipDisplayName)

            '        '.RecipAddress = MailUser.MailAddress 'Index=0、Type=1でTOアドレス
            '        'メールアドレス取得
            '        If SendList.Count <> 0 Then
            '            'メールアドレス取得
            '            MailList = getMailAddressList(SendList)
            '            If StringUtil.Equals(MailList, Nothing) Then
            '                MsgBox("データなし")
            '            ElseIf MailList.Count <> 0 Then
            '                For i As Integer = 0 To MailList.Count - 1
            '                    .RecipIndex = intIndex
            '                    .RecipType = 3
            '                    .RecipDisplayName = MailList.Item(i).UserName
            '                    '.RecipAddress = MailList.Item(i).MailAddress    'Index=1、Type=3でBCCアドレス
            '                    intIndex = intIndex + 1
            '                Next
            '            End If
            '        End If
            '        .MsgSubject = "【イベント改訂のお知らせ】≪イベントコード： " & subject.ShisakuEventCode & " ≫"
            '        .MsgNoteText = dspMsg
            '        .AttachmentPathName = strFileName

            '        MsgBox(mapimessages)

            '        ' 手動送信
            '        .Send(True)
            '    End With

            '    mapisettion.SignOff()
            'Catch ex As Exception
            '    MsgBox(ex)
            '    ' MS Outlookの場合、送信せずにキャンセルされた場合は例外が発生する・・・？
            'End Try

        End Sub

#End Region

#Region " メールアドレス取得（自分自身） "

        Private Function getMailAddressLoginUser() As SendMailUserAddressVo

            'ユーザーメール情報
            Dim loginUser As New SendMailUserAddressVo

            Try

                Dim objConnection, objCommand, objRecordSet
                '職番が存在するかチェックする。
                'ユーザー名（職番）でドメインを取得してDCを取得します。
                objConnection = CreateObject("ADODB.Connection")
                objConnection.Open("Provider=ADsDSOObject;")

                objCommand = CreateObject("ADODB.Command")
                objCommand.ActiveConnection = objConnection
                '対象ドメインを設定
                objCommand.CommandText = _
                "<LDAP://dc=gkh,dc=auto3,dc=subaru,dc=net>;(&(objectCategory=User)" & _
                 "(samAccountName=" & LoginInfo.Now.UserId & "));" & _
                 "mail,displayName," & _
                 "description," & _
                 "samAccountName;subtree"

                objRecordSet = objCommand.Execute

                If objRecordSet.RecordCount <> 0 Then
                    'メールアドレス情報をVOへ保持
                    loginUser.EventCode = subject.ShisakuEventCode
                    loginUser.UserId = Me.LblCurrUserId.Text
                    loginUser.UserName = objRecordSet.fields("displayname").value
                    loginUser.MailAddress = objRecordSet.Fields("mail").Value
                End If

                Return loginUser

            Catch ex As Exception
                Return Nothing
            End Try

        End Function

#End Region

#Region " メールアドレス取得（ＡＤ） "

        Private Function getMailAddressList(ByVal userIdList As List(Of SendMailUserAddressVo)) As List(Of SendMailUserAddressVo)

            '初期設定
            Dim i As Integer

            'ユーザーメール情報
            Dim mailList As New List(Of SendMailUserAddressVo)

            Try
                'ユーザー情報毎
                For i = 0 To userIdList.Count - 1

                    Dim objConnection, objCommand, objRecordSet

                    '職番が存在するかチェックする。
                    'ユーザー名（職番）でドメインを取得してDCを取得します。
                    objConnection = CreateObject("ADODB.Connection")
                    objConnection.Open("Provider=ADsDSOObject;")

                    objCommand = CreateObject("ADODB.Command")
                    objCommand.ActiveConnection = objConnection
                    '対象ドメインを設定
                    objCommand.CommandText = _
                    "<LDAP://dc=gkh,dc=auto3,dc=subaru,dc=net>;(&(objectCategory=User)" & _
                     "(samAccountName=" & userIdList(i).UserId & "));" & _
                     "mail,displayName," & _
                     "description," & _
                     "samAccountName;subtree"

                    objRecordSet = objCommand.Execute

                    If objRecordSet.RecordCount <> 0 Then
                        'メールアドレス情報をVOへ保持
                        Dim vo As New SendMailUserAddressVo
                        vo.EventCode = subject.ShisakuEventCode
                        vo.UserId = userIdList(i).UserId
                        vo.UserName = objRecordSet.fields("displayname").value
                        vo.MailAddress = objRecordSet.Fields("mail").Value
                        '挿入
                        mailList.Add(vo)
                    End If

                Next

                Return mailList

            Catch ex As Exception
                Return Nothing
            End Try

        End Function

#End Region

        Private Sub BtnBaseCarTenkai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBaseCarTenkai.Click
            VisibleButton(BtnBaseCarTenkai)
            '2012/01/09
            ForceSetActiveSpreadColor(GetActiveSheet(), 0)
        End Sub

#Region "保存先選択機能(フォルダ選択ダイアログ)"
        ''' <summary>
        ''' 保存先選択機能(ファイルダイアログ)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SelectFolderDialog(ByVal strMsg As String) As String
            Dim FolderName As String
            Dim systemDrive As String = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            Dim fbd As New FolderBrowserDialog()

            ' デフォルトフォルダを指定します
            fbd.SelectedPath = systemDrive

            '説明文を指定
            fbd.Description = strMsg

            'ダイアログ選択有無
            If fbd.ShowDialog() = DialogResult.OK Then
                FolderName = fbd.SelectedPath
                '--------------------------------------------------
                'EXCEL出力先フォルダチェック
                ShisakuCommon.ShisakuComFunc.ExcelOutPutDirCheck()
                '--------------------------------------------------
            Else
                'Nothingを返す。
                Return Nothing
            End If

            fbd.Dispose()

            Return FolderName

        End Function
#End Region



        ''' <summary>
        '''  Excel編集書式出力
        ''' </summary>
        ''' <remarks></remarks>
        Private Function ShisakuEventExcelOutput(ByVal fileName As String) As Boolean

            '---------------------------------------
            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor
            '---------------------------------------
            Try
                'エクセル出力'
                If Not ShisakuComFunc.IsFileOpen(fileName) Then
                    Using xls As New ShisakuExcel(fileName)

                        ' 設計展開時のベース車情報
                        Dim baseTenkaiList As New List(Of TShisakuEventBaseVo)
                        ' 完成車情報
                        Dim kanseiList As New List(Of TShisakuEventKanseiVo)
                        ' ベース車情報
                        Dim baseList As New List(Of TShisakuEventBaseSeisakuIchiranVo)
                        ' 基本装備仕様情報
                        Dim basicList As New List(Of EventSoubiVo)
                        ' 特別装備仕様情報
                        Dim specialList As New List(Of EventSoubiVo)
                        ' 基本装備仕様情報（タイトル）
                        Dim basicTitelList As New List(Of EventSoubiVo)
                        ' 特別装備仕様情報（タイトル）
                        Dim specialTitelList As New List(Of EventSoubiVo)

                        '差異比較用
                        ' 完成車情報比較用
                        Dim HkanseiList As New List(Of TShisakuEventKanseiKaiteiVo)
                        ' ベース車情報比較用
                        Dim HbaseList As New List(Of TShisakuEventBaseKaiteiVo)
                        ' 基本装備仕様情報比較用
                        Dim HbasicList As New List(Of EventSoubiVo)
                        ' 特別装備仕様情報比較用
                        Dim HspecialList As New List(Of EventSoubiVo)
                        ' 基本装備仕様情報比較用（タイトル）
                        Dim HbasicTitelList As New List(Of EventSoubiVo)
                        ' 特別装備仕様情報比較用（タイトル）
                        Dim HspecialTitelList As New List(Of EventSoubiVo)

                        '初期設定
                        Dim eventInfo = New ExportShisakuEventInfoExcelDaoImpl
                        ''   イベント情報
                        'eventVO = eventInfo.GetEvent(ShisakuEventCode)
                        '   完成車情報
                        kanseiList = eventInfo.GetKansei(ShisakuEventCode)
                        '   ベース車情報
                        baseList = eventInfo.GetBase(ShisakuEventCode)
                        '   基本装備仕様情報
                        basicList = eventInfo.GetSoubi(ShisakuEventCode, "1")
                        '   特別装備仕様情報
                        specialList = eventInfo.GetSoubi(ShisakuEventCode, "2")
                        '   基本装備仕様情報（タイトル）
                        basicTitelList = eventInfo.GetSoubiTitle(ShisakuEventCode, "1")
                        '   特別装備仕様情報（タイトル）
                        specialTitelList = eventInfo.GetSoubiTitle(ShisakuEventCode, "2")
                        '   設計展開時のベース車情報
                        baseTenkaiList = eventInfo.GetBaseTenkai(ShisakuEventCode)

                        '   ベース車情報比較元
                        HbaseList = eventInfo.GetBaseKaitei(ShisakuEventCode)
                        '   完成車情報比較元
                        HkanseiList = eventInfo.GetKanseiKaitei(ShisakuEventCode)
                        '   基本装備仕様情報比較元
                        HbasicList = eventInfo.GetSoubiKaitei(ShisakuEventCode, "1")
                        '   特別装備仕様情報比較元
                        HspecialList = eventInfo.GetSoubiKaitei(ShisakuEventCode, "2")
                        '   基本装備仕様情報比較元（タイトル）
                        HbasicTitelList = eventInfo.GetSoubiTitleKaitei(ShisakuEventCode, "1")
                        '   特別装備仕様情報比較元（タイトル）
                        HspecialTitelList = eventInfo.GetSoubiTitleKaitei(ShisakuEventCode, "2")

                        Dim COMP_ExcelHensyu As New ExcelOutput.ExportShisakuEventInfoExcel(subject, _
                                                                    kanseiList, baseList, basicList, specialList, _
                                                                    basicTitelList, specialTitelList, _
                                                                    HkanseiList, HbaseList, HbasicList, HspecialList, _
                                                                    HbasicTitelList, HspecialTitelList, _
                                                                    ShisakuEventCode, baseTenkaiList, _
                                                        spdCompleteCar_Sheet1.Cells(2, spdCompleteCar_Sheet1.Columns("EG_MEMO_1").Index).Value, _
                                                        spdCompleteCar_Sheet1.Cells(2, spdCompleteCar_Sheet1.Columns("EG_MEMO_2").Index).Value, _
                                                        spdCompleteCar_Sheet1.Cells(2, spdCompleteCar_Sheet1.Columns("TM_MEMO_1").Index).Value, _
                                                        spdCompleteCar_Sheet1.Cells(2, spdCompleteCar_Sheet1.Columns("TM_MEMO_2").Index).Value)
                        COMP_ExcelHensyu.Execute(xls)

                        '部品表ベース車シートをA3横で印刷できるように変更'
                        xls.SetActiveSheet(1)
                        xls.PrintPaper(fileName, 1, "A3")
                        xls.PrintOrientation(fileName, 1, 1, False)
                        '製作一覧ベース車シートをA3横で印刷できるように変更'
                        xls.SetActiveSheet(2)
                        xls.PrintPaper(fileName, 1, "A3")
                        xls.PrintOrientation(fileName, 1, 1, False)
                        '完成車シートをA3横で印刷できるように変更'
                        xls.SetActiveSheet(3)
                        xls.PrintPaper(fileName, 1, "A3")
                        xls.PrintOrientation(fileName, 1, 1, False)
                        '基本装備仕様シートをA3横で印刷できるように変更'
                        xls.SetActiveSheet(4)
                        xls.PrintPaper(fileName, 1, "A3")
                        xls.PrintOrientation(fileName, 1, 1, False)
                        '特別装備仕様シートをA3横で印刷できるように変更'
                        xls.SetActiveSheet(5)
                        xls.PrintPaper(fileName, 1, "A3")
                        xls.PrintOrientation(fileName, 1, 1, False)
                        '保存
                        xls.Save()
                        ''EXCELを開く
                        'Process.Start(fileName)
                    End Using

                End If

            Catch ex As ApplicationException
                ComFunc.ShowErrMsgBox("出力で問題が発生しました。既にファイルが開いている可能性があります。")
                Return False
            Catch ex As Exception
                ComFunc.ShowErrMsgBox("出力で問題が発生しました。" & vbLf & vbLf & ex.Message)
                Return False
            Finally
                Cursor.Current = Cursors.Default
            End Try
            Return True

        End Function


        ''' <summary>
        ''' EXCEL出力SUB
        ''' </summary>
        Private Function EventExcelOutput(ByVal strHead As String, ByVal strMsg As String) As String
            '登録済データが存在すれば処理続行
            '初期設定
            Dim strFileName As String = ""
            Dim eventInfo = New ExportShisakuEventInfoExcelDaoImpl
            ' ベース車情報比較用
            Dim HbaseList As New List(Of TShisakuEventBaseKaiteiVo)
            '   ベース車情報比較元
            HbaseList = eventInfo.GetBaseKaitei(ShisakuEventCode)
            If HbaseList.Count > 0 Then
                '保存先を指定する。
                '   デフォルトは発行用ファイル作成で指定したフォルダ。変更も可能）
                Dim strHozonsaki As String = SelectFolderDialog(strMsg)
                If StringUtil.IsNotEmpty(strHozonsaki) Then
                    '保存先
                    '   末尾が\か？
                    If cRight(strHozonsaki, 1) <> "\" Then
                        strHozonsaki = strHozonsaki & "\"
                    End If
                    '   保存先のフォルダとファイル名をセット

                    'デフォルトファイル名
                    'txtEventName.Textはファイル名に使用できない文字を変換する。
                    strFileName = strHozonsaki & _
                                                strHead & cmbKaihatufugo.Text & _
                                                StringUtil.ReplaceInvalidCharForFileName(txtEventName.Text) & "(" & _
                                                ShisakuEventCode & ")" & CLng(Now.ToString("yyMMdd")) & ".xls"

                    If System.IO.File.Exists(strFileName) Then
                        For i As Integer = 1 To 50
                            'txtEventName.Textはファイル名に使用できない文字を変換する。
                            strFileName = strHozonsaki & _
                                strHead & cmbKaihatufugo.Text & _
                                StringUtil.ReplaceInvalidCharForFileName(txtEventName.Text) & "(" & _
                                ShisakuEventCode & ")" & CLng(Now.ToString("yyMMdd")) & "_" & i & ".xls"
                            'ファイルの存在チェック
                            If Not System.IO.File.Exists(strFileName) Then
                                Exit For
                            End If
                        Next
                    End If

                    If ShisakuEventExcelOutput(strFileName) = False Then
                        ComFunc.ShowInfoMsgBox("出力を中断しました。", MessageBoxButtons.OK)
                        Return strFileName
                    End If
                End If
            End If
            Return strFileName
        End Function

        Private Sub BtnEbomKanshi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEbomKanshi.Click
            VisibleButton(btnEbomKanshi)
            ForceSetActiveSpreadColor(GetActiveSheet(), 0)
        End Sub

        Private Sub spdEbomKanshi_CellClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdEbomKanshi.CellClick
            If e.ColumnHeader Then
                '参考情報の表示／非表示																																	
                If (e.Row = 0) Then

                    '  baseCarObserver.referenceCarInfo(e.Column)

                End If
            End If
        End Sub

        Private Sub spdEbomKanshi_VisibleChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles spdEbomKanshi.VisibleChanged
            If spdEbomKanshi.Visible Then
                inputSupport.ForceSetActiveSpread(spdEbomKanshi)
            End If
        End Sub


        Private Sub spdEbomKanshi_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdEbomKanshi.KeyDown
            Dim downKey As Object
            Dim sheet As Spread.SheetView = Me.spdEbomKanshi_Sheet1
            Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)

            downKey = e.KeyCode()

            Select Case downKey

                Case Keys.C
                    '設計展開以降のイベントはKeyDownを無効に'
                    If Not headerSubject.IsSekkeiTenkaiIkou Then
                        'コントロールキーとCキーが押された
                        If (e.Modifiers And Keys.Control) = Keys.Control Then

                            '書式バックアップ
                            Dim listBln As List(Of Boolean()) = GetEditCellInfo(sheet)
                            '書式を一時的に全て保存編集対象にする
                            SetUndoCellFormat(sheet)
                            ''コピー
                            'sheet.ClipboardCopy()

                            ' 選択範囲を取得
                            Dim cr As FarPoint.Win.Spread.Model.CellRange() = spdEbomKanshi.ActiveSheet.GetSelections()
                            If cr.Length > 0 Then
                                Dim data As [String] = Nothing
                                Dim count As Integer = 0
                                If cr(0).Row = -1 Then
                                    ' 列単位で選択されている場合
                                    For i As Integer = 0 To spdEbomKanshi.ActiveSheet.RowCount - 1
                                        If spdEbomKanshi.ActiveSheet.GetRowVisible(i) = True Then
                                            data += spdEbomKanshi.ActiveSheet.GetClipValue(i, cr(0).Column, 1, cr(0).ColumnCount)
                                        End If
                                    Next
                                Else
                                    ' セル単位で選択されている場合
                                    For i As Integer = 0 To cr(0).RowCount - 1
                                        If spdEbomKanshi.ActiveSheet.GetRowVisible(cr(0).Row + i) = True Then

                                            'If count < 1 Then
                                            '    count = count + 1
                                            '    data += spdEbomKanshi.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                            'Else
                                            '    MsgBox("複数の行に渡ってのコピーをすることはできません")
                                            '    SetUndoCellFormat(sheet, listBln)
                                            '    Return
                                            'End If
                                            data += spdEbomKanshi.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                        End If
                                    Next
                                End If

                                ' クリップボードに設定します
                                Clipboard.SetData(DataFormats.Text, data)
                            End If

                            '書式を戻す
                            SetUndoCellFormat(sheet, listBln)

                        End If
                    End If

                Case Keys.V
                    '設計展開以降のイベントはKeyDownを無効に'
                    If Not headerSubject.IsSekkeiTenkaiIkou Then
                        '行選択時は無効にする。
                        If Not selection Is Nothing Then
                            If (selection.Column = -1 AndAlso selection.ColumnCount - 1) Then
                                e.Handled = True
                            Else

                                'コントロールキーとVキーが押された
                                If (e.Modifiers And Keys.Control) = Keys.Control Then

                                    Dim listClip As New List(Of String())

                                    listClip = GetClipbordList()

                                    If Not listClip Is Nothing Then

                                        Dim rowCount As Integer = listClip.Count - 1
                                        Dim colCount As Integer = listClip(0).Length

                                        '単一コピーの場合'
                                        If rowCount = 1 Then
                                            For col As Integer = 0 To selection.ColumnCount - 1
                                                For rowindex As Integer = 0 To selection.RowCount - 1
                                                    If Not Me.spdEbomKanshi_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Locked Then
                                                        '隠された行にはペーストしない'
                                                        If Me.spdEbomKanshi_Sheet1.Rows(selection.Row + rowindex).Visible Then
                                                            Me.spdEbomKanshi_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                        End If
                                                    End If
                                                Next
                                            Next
                                        ElseIf rowCount > 1 Then
                                            '複数コピーの場合'
                                            For col As Integer = 0 To selection.ColumnCount - 1
                                                For rowindex As Integer = 0 To selection.RowCount - 1
                                                    If Not Me.spdEbomKanshi_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Locked Then
                                                        '隠された行にはペーストしない'
                                                        If Me.spdEbomKanshi_Sheet1.Rows(selection.Row + rowindex).Visible Then
                                                            Me.spdEbomKanshi_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                        Else
                                                            '非表示なら'


                                                        End If
                                                    End If
                                                Next
                                            Next
                                        End If

                                        'セル編集モード時にコピーした場合、以下を行う。
                                        If rowCount = 0 Then
                                            rowCount = 1
                                        End If

                                        '行選択時
                                        If selection.Column = -1 Then

                                            If Not StringUtil.IsEmpty(headerSubject.StatusName) Then
                                                '貼りつけ対象のセルを編集済みとし書式を設定する
                                                Me.spdEbomKanshi_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                                                Me.spdEbomKanshi_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).ForeColor = Color.Blue
                                            End If

                                        Else

                                            If (selection.Row + rowCount) >= sheet.RowCount - 1 Then
                                                EBom.Common.ComFunc.ShowInfoMsgBox("貼り付けしようとしている範囲がスプレッド表の最大行を超えています")
                                                Return
                                            End If

                                            If Not StringUtil.IsEmpty(headerSubject.StatusName) Then
                                                '貼りつけ対象のセルを編集済みとし書式を設定する
                                                Me.spdEbomKanshi_Sheet1.Cells(selection.Row, selection.Column, selection.Row + rowCount - 1, _
                                                                       selection.Column + colCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

                                                Me.spdEbomKanshi_Sheet1.Cells(selection.Row, selection.Column, selection.Row + rowCount - 1, _
                                                                selection.Column + colCount - 1).ForeColor = Color.Blue
                                            End If
                                        End If
                                    End If

                                End If
                            End If
                        Else

                        End If

                    End If

                Case Keys.Delete
                    '設計展開以降のイベントはKeyDownを無効に'
                    If Not headerSubject.IsSekkeiTenkaiIkou Then
                        '行選択・列選択ではDeleteは無効に
                        If Not selection Is Nothing Then
                            If (selection.Column = -1 AndAlso selection.ColumnCount - 1) Then
                                e.Handled = True
                            End If

                            If (selection.Row = -1 AndAlso selection.RowCount - 1) Then
                                e.Handled = True
                            End If
                        End If
                    Else
                        e.Handled = True
                    End If
            End Select

        End Sub

        Private Sub spdEbomKanshi_Change(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdEbomKanshi.Change
            Dim sheet As Spread.SheetView = spdEbomKanshi.ActiveSheet

            If Me.cmbKaihatufugo.Enabled = False Then
                ' 選択セルの場所を特定します。
                ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
                ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

                ' 該当セルの文字色、文字太を変更する。
                sheet.SetStyleInfo(ParaActRowIdx, ParaActColIdx, CreateNewStyle())
                sheet.Cells(ParaActRowIdx, ParaActColIdx).Locked = False        '2012/01/09
            End If

            '2012/02/11
            '2012/01/09
            If e.Column = 0 Then
                SetActiveSpreadColor(sheet, sheet.ActiveRowIndex, 0)
            End If

        End Sub

        Private Sub spdEbomKanshi_EditModeOn(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles spdEbomKanshi.EditModeOn
            Dim sheet As Spread.SheetView = spdEbomKanshi_Sheet1
            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            'IMEを使用不可能にする。
            spdEbomKanshi.ImeMode = Windows.Forms.ImeMode.Disable
        End Sub

        Private Sub spdEbomKanshi_EditModeOff(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles spdEbomKanshi.EditModeOff
            spdEbomKanshi.EditingControl.ImeMode = Windows.Forms.ImeMode.NoControl
            spdEbomKanshi.ImeMode = Windows.Forms.ImeMode.NoControl
        End Sub


    End Class
End Namespace
