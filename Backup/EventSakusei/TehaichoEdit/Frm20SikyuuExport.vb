Imports EventSakusei.TehaichoEdit.Dao
Imports System.IO
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Ui
Imports ShisakuCommon
Imports EBom.Common
Imports EventSakusei.TehaichoEdit
Imports Microsoft.Office.Interop.Word
Imports ShisakuCommon.Ui.Valid

Public Class Frm20SikyuuExport

    Private _shisakuEventCode As String
    ''' <summary>画面制御ロジック</summary>
    Private _TehaiEditLogic As TehaichoEditLogic
    '''<summary>初期化完了フラグ</summary>>
    Private _InitComplete As Boolean = False
    Private _ResultVo As New List(Of TShisakuTehaiKihonVo)
    ''' <summary>取引先コード</summary>
    Private _TorihikisakiCode As String

    Private _MakerCsvVos As New List(Of MakerCsvVo)

    Private _ResultOk As Boolean

    Private errorController As New ErrorController()

    Private validator As Validator

    Private WordApp As ApplicationClass
    Private WordDoc As Document
    Private isOpened As Boolean = False '判断word模版是否被占用


#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="shisakuEventCode">試作イベントコード</param>
    ''' <param name="outputList">出力用情報</param>
    ''' <param name="tehaiEditLogic">画面制御ロジック</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal shisakuEventCode As String, ByVal outputList As List(Of TShisakuTehaiKihonVo), ByVal tehaiEditLogic As TehaichoEditLogic)

        InitializeComponent()

        Me._ResultVo = outputList

        Me._shisakuEventCode = shisakuEventCode

        Me._TehaiEditLogic = tehaiEditLogic

        Me.Refresh()

        ShisakuFormUtil.Initialize(Me)

        '初期化メイン
        Initialize()

        InitializeValidator()

    End Sub

#End Region

#Region "フォームロード"
    ''' <summary>
    ''' フォームロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm20SikyuuExport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Focus()

    End Sub
#End Region

#Region "初期化メイン"
    ''' <summary>
    ''' 初期化メイン
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Initialize()

        Cursor.Current = Cursors.WaitCursor

        ''画面のPG-IDが表示されます。
        ShisakuFormUtil.setTitleVersion(Me)
        LblCurrPGId.Text = "PG-ID :" + PROGRAM_ID_20

        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        ShisakuFormUtil.SetIdAndBuka(LblCurrUserId, LblCurrBukaName)

        '取引先CSVを読込
        _MakerCsvVos = New List(Of MakerCsvVo)
        _TehaiEditLogic.ReadMakerCsv(_MakerCsvVos, MAKER_CSV_FILE, True)
        If _MakerCsvVos.Count > 0 Then
            ' コンボボックス類初期化
            FormUtil.BindLabelValuesToComboBox(Me.cmbTorihikisakiCode, GetLabelValues_CompName(), True)
        End If

        '「試作イベント情報」を検索
        Dim eventVo As TShisakuEventVo = _TehaiEditLogic.FindShisakuEventByKey(_shisakuEventCode)
        If eventVo IsNot Nothing Then
            '車種
            txtShisakuKaihatsuFugo.Text = eventVo.ShisakuKaihatsuFugo
        End If

        '件名にデータ支給、補足情報に2/2参照と表示する。
        txtContent.Text = "データ支給"
        txtHosoku.Text = "2/2参照"
        '支給理由に先開台車製作の為と表示する。
        txtRiyuu.Text = "先開台車製作の為"

        '初期化完了
        _InitComplete = True

    End Sub

    Private Sub InitializeValidator()

        validator = New Validator

        Dim TorihikisakiCodeRequired As New Validator("取引先を選択または入力してください。")
        TorihikisakiCodeRequired.Add(cmbTorihikisakiCode).Required()
        Dim MakerCodeRequired As New Validator("取引先情報を入力してください。")
        MakerCodeRequired.Add(txtMakerCode).Required()
        Dim DeptNameRequired As New Validator("取引先情報を入力してください。")
        DeptNameRequired.Add(txtDeptName).Required()
        Dim MakerNameRequired As New Validator("取引先情報を入力してください。")
        MakerNameRequired.Add(txtMakerName).Required()
        Dim ShisakuKaihatsuFugoRequired As New Validator("開発コード/車種を入力してください。")
        ShisakuKaihatsuFugoRequired.Add(txtShisakuKaihatsuFugo).Required()
        Dim ContentRequired As New Validator("内容を入力してください。")
        ContentRequired.Add(txtContent).Required()
        Dim HosokuRequired As New Validator("内容を入力してください。")
        HosokuRequired.Add(txtHosoku).Required()
        Dim RiyuuRequired As New Validator("支給理由・使用目的を入力してください。")
        RiyuuRequired.Add(txtRiyuu).Required()

        validator.Add(TorihikisakiCodeRequired)
        validator.Add(MakerCodeRequired)
        validator.Add(DeptNameRequired)
        validator.Add(MakerNameRequired)
        validator.Add(ShisakuKaihatsuFugoRequired)
        validator.Add(ContentRequired)
        validator.Add(HosokuRequired)
        validator.Add(RiyuuRequired)

    End Sub

#End Region

#Region "プロパティ"

#Region "初期化完了確認"
    ''' <summary>
    ''' 初期化完了確認
    ''' 
    ''' 初期化正常実行でTRUEを返す
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property InitComplete()
        Get
            Return _InitComplete
        End Get
    End Property
#End Region

    ''' <summary>
    ''' OKボタンでTRUEを返す
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ResultOk()
        Get
            Return _ResultOk
        End Get
    End Property

#End Region

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
    End Sub

    ''' <summary>
    ''' 戻るボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
        _ResultOk = False
        Me.Close()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim msg As String

        Try
            '各項目のチェックの前にデータを入れる'
            cmbTorihikisakiCode.BackColor = Nothing
            txtDeptName.BackColor = Nothing
            txtMakerCode.BackColor = Nothing
            txtMakerName.BackColor = Nothing
            txtShisakuKaihatsuFugo.BackColor = Nothing
            txtContent.BackColor = Nothing
            txtHosoku.BackColor = Nothing
            txtRiyuu.BackColor = Nothing

            errorController.ClearBackColor()

            Try
                validator.AssertValidate()
            Catch ex As IllegalInputException
                errorController.SetBackColorOnError(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)
                ''エラーメッセージ
                ComFunc.ShowErrMsgBox(ex.Message)

                Return
            End Try

            Dim contentStr As String = txtContent.Text
            Dim hosokuStr As String = txtHosoku.Text
            Dim riyuuStr As String = txtRiyuu.Text

            'データ支給依頼書出力
            If _TehaiEditLogic.SikyuuExport(cmbTorihikisakiCode.Text.Trim, txtDeptName.Text.Trim, txtMakerCode.Text.Trim, txtMakerName.Text.Trim, txtShisakuKaihatsuFugo.Text.Trim, contentStr, hosokuStr, riyuuStr, _ResultVo) = False Then
                Return
            End If

            Cursor.Current = Cursors.Default
            _ResultOk = True
            Me.Close()
        Catch ex As ApplicationException
            _ResultOk = False
            msg = "データ支給依頼書出力で問題が発生しました。既にファイルが開いている可能性があります。"
            ComFunc.ShowErrMsgBox(msg)
        Catch ex As Exception
            _ResultOk = False
            msg = String.Format("データ支給依頼書出力で問題が発生しました(ERR={0})", ex.Message)
            ComFunc.ShowErrMsgBox(msg)
        End Try
        
    End Sub

    Private Class CompNameExtraction : Implements ILabelValueExtraction
        Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
            Dim vo As New MakerCsvVo
            aLocator.IsA(vo).Label(vo.CompName).Value(vo.CompName)
        End Sub
    End Class

    Public Function GetLabelValues_CompName() As List(Of LabelValueVo)
        Dim results As List(Of LabelValueVo) = _
            LabelValueExtracter(Of MakerCsvVo).Extract(_MakerCsvVos, New CompNameExtraction)
        results.Sort(New LabelValueComparer)
        Return results
    End Function

    Private Sub cmbTorihikisakiCode_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTorihikisakiCode.SelectedIndexChanged
        For i As Integer = 0 To _MakerCsvVos.Count - 1
            If _MakerCsvVos(i).CompName.Equals(cmbTorihikisakiCode.Text) Then
                txtMakerCode.Text = _MakerCsvVos(i).MakerCode
                txtDeptName.Text = _MakerCsvVos(i).DeptName
                txtMakerName.Text = _MakerCsvVos(i).MakerName
            End If
        Next

    End Sub

End Class