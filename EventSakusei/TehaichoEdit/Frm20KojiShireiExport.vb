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

Public Class Frm20KojiShireiExport

    '''<summary>初期化完了フラグ</summary>>
    Private _InitComplete As Boolean = False
    ''' <summary>画面制御ロジック</summary>
    Private _TehaiEditLogic As TehaichoEditLogic
    ''' <summary>試作イベントコード</summary>
    Private _ShisakuEventCode As String
    ''' <summary>出力データ</summary>
    Private _OutputList As List(Of TShisakuTehaiKihonVo)

    Private _ResultOk As Boolean

    Private _TantoCsvVos As List(Of TantoCsvVo)

    Private errorController As New ErrorController()
    Private validator As Validator


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

        Me._TehaiEditLogic = tehaiEditLogic

        Me._ShisakuEventCode = shisakuEventCode

        Me._OutputList = outputList

        ShisakuFormUtil.Initialize(Me)

        '初期化メイン
        Initialize()
        InitializeValidator()

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

        '担当者CSVを読込
        _TantoCsvVos = New List(Of TantoCsvVo)
        _TehaiEditLogic.ReadTantoCsv(_TantoCsvVos, TANTO_CSV_FILE, True)
        If _TantoCsvVos.Count > 0 Then
            ' コンボボックス類初期化
            FormUtil.BindLabelValuesToComboBox(Me.cmbTantousha, GetLabelValues_tantoName(), True)
        End If

        '「試作イベント情報」を検索
        Dim eventVo As TShisakuEventVo = _TehaiEditLogic.FindShisakuEventByKey(_ShisakuEventCode)
        If eventVo IsNot Nothing Then
            '車種
            txtCarType.Text = eventVo.ShisakuKaihatsuFugo
            '目的
            txtMokuteki.Text = eventVo.ShisakuEventName & Space(1) & "製作の為"
        End If

        '初期化完了
        _InitComplete = True

    End Sub

    Private Sub InitializeValidator()

        validator = New Validator

        Dim TantoushaRequired As New Validator("担当者名を選択または入力してください。")
        TantoushaRequired.Add(cmbTantousha).Required()
        Dim TelRequired As New Validator("担当者情報を入力してください。")
        TelRequired.Add(txtTel).Required()
        Dim StaffCodeRequired As New Validator("担当者情報を入力してください。")
        StaffCodeRequired.Add(txtStaffCode).Required()
        Dim KenmeiRequired As New Validator("件名を入力してください。")
        KenmeiRequired.Add(txtKenmei).Required()
        Dim CarTypeRequired As New Validator("車種を入力してください。")
        CarTypeRequired.Add(txtCarType).Required()
        Dim MokutekiRequired As New Validator("目的を入力してください。")
        MokutekiRequired.Add(txtMokuteki).Required()
        Dim KoujiNoRequired As New Validator("工事指令№を入力してください。")
        KoujiNoRequired.Add(txtKoujiNo).Required()
        'Dim KijiRequired As New Validator("記事を入力してください。")
        'KijiRequired.Add(txtKiji).Required()

        validator.Add(TantoushaRequired)
        validator.Add(TelRequired)
        validator.Add(StaffCodeRequired)
        validator.Add(KenmeiRequired)
        validator.Add(CarTypeRequired)
        validator.Add(MokutekiRequired)
        validator.Add(KoujiNoRequired)
        'validator.Add(KijiRequired)

    End Sub

#End Region

#Region "コンボボックス設定"

    Private Class TantoNameExtraction : Implements ILabelValueExtraction
        Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
            Dim vo As New TantoCsvVo
            aLocator.IsA(vo).Label(vo.Name).Value(vo.No)
        End Sub
    End Class

    Public Function GetLabelValues_tantoName() As List(Of LabelValueVo)
        Dim results As List(Of LabelValueVo) = _
            LabelValueExtracter(Of TantoCsvVo).Extract(_TantoCsvVos, New TantoNameExtraction)
        results.Sort(New LabelValueComparer)
        Return results
    End Function

#End Region

#Region "フォームロード"
    ''' <summary>
    ''' フォームロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm20KojiShireiExport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Focus()

    End Sub
#End Region

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
    End Sub

#Region "戻るボタン"

    ''' <summary>
    ''' 戻るボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        _ResultOk = False
        Me.Close()
    End Sub

#End Region

#Region "出力ボタン"

    ''' <summary>
    ''' 出力ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click

        Dim msg As String

        Try
            '各項目のチェックの前にデータを入れる'
            cmbTantousha.BackColor = Nothing
            txtTel.BackColor = Nothing
            txtStaffCode.BackColor = Nothing
            rdoBtnIrai.BackColor = Nothing
            rdoBtnShirei.BackColor = Nothing
            txtKenmei.BackColor = Nothing
            txtCarType.BackColor = Nothing
            txtMokuteki.BackColor = Nothing
            txtKoujiNo.BackColor = Nothing
            txtKiji.BackColor = Nothing

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

            '依頼部署ラジオボタン、指令部署ラジオボタンともにFalseの場合
            If rdoBtnIrai.Checked = False And rdoBtnShirei.Checked = False Then
                rdoBtnIrai.BackColor = Color.Red
                rdoBtnShirei.BackColor = Color.Red
                rdoBtnIrai.Focus()
                ''エラーメッセージ
                ComFunc.ShowErrMsgBox("依頼部署・指令部署の記述先選択を行って下さい。")

                Return
            End If

            '工事指令書出力
            Dim excelVo As KojiShireiExcelVo = MakeExcelVo()
            If _TehaiEditLogic.KoujiShireiOutput(_OutputList, excelVo) = False Then
                Return
            End If

            Cursor.Current = Cursors.Default
            _ResultOk = True
            Me.Close()
        Catch ex As ApplicationException
            _ResultOk = False
            msg = "工事指令書出力で問題が発生しました。既にファイルが開いている可能性があります。"
            ComFunc.ShowErrMsgBox(msg)
        Catch ex As Exception
            _ResultOk = False
            msg = String.Format("工事指令書出力で問題が発生しました(ERR={0})", ex.Message)
            ComFunc.ShowErrMsgBox(msg)
        End Try

    End Sub

#End Region

#Region "担当者コンボボックス変更"

    ''' <summary>
    ''' 担当者コンボボックス
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbTorihikisakiCode_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTantousha.SelectedIndexChanged
        Dim hasEquals As Boolean = False

        For index As Integer = 0 To _TantoCsvVos.Count - 1
            If _TantoCsvVos(index).Name.Equals(cmbTantousha.Text) Then
                txtTel.Text = _TantoCsvVos(index).Tel
                txtStaffCode.Text = _TantoCsvVos(index).No
                hasEquals = True
            End If
        Next
        '該当しない場合はブランクを設定
        If Not hasEquals Then
            txtTel.Text = ""
            txtStaffCode.Text = ""
        End If
    End Sub

#End Region

#Region "ラジオボタン変更"

    ''' <summary>
    ''' 依頼部署ラジオボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rdoBtnIrai_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoBtnIrai.CheckedChanged
        If rdoBtnIrai.Checked = True Then
            '件名テキストボックスがブランクだったら"内製品部得依頼"を設定する。
            If StringUtil.IsEmpty(txtKenmei.Text) Then
                txtKenmei.Text = "内製品部得依頼"
            End If
        End If
    End Sub

    ''' <summary>
    ''' 指令部署ラジオボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rdoBtnShirei_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoBtnShirei.CheckedChanged
        If rdoBtnShirei.Checked = True Then
            '件名テキストボックスがブランクだったら"部品購入依頼"を設定する。
            If StringUtil.IsEmpty(txtKenmei.Text) Then
                txtKenmei.Text = "部品購入依頼"
            End If
        End If
    End Sub

#End Region

    ''' <summary>
    ''' 画面の入力内容を出力Voに格納
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MakeExcelVo()
        Dim excelVo As New KojiShireiExcelVo

        excelVo.Tanto = cmbTantousha.Text
        excelVo.TantoNo = txtStaffCode.Text
        excelVo.TantoTel = txtTel.Text
        excelVo.IsIrai = rdoBtnIrai.Checked
        excelVo.IsShire = rdoBtnShirei.Checked
        excelVo.Kenmei = txtKenmei.Text
        excelVo.GoshaType = txtCarType.Text
        excelVo.Mokuteki = txtMokuteki.Text
        excelVo.KojiNo = txtKoujiNo.Text
        excelVo.Kiji = txtKiji.Text

        Return excelVo
    End Function

End Class