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

Public Class Frm20OrderExport

    '''<summary>初期化完了フラグ</summary>>
    Private _InitComplete As Boolean = False
    ''' <summary>画面制御ロジック</summary>
    Private _TehaiEditLogic As TehaichoEditLogic
    ''' <summary>試作イベントコード</summary>
    Private _ShisakuEventCode As String
    ''' <summary>出力データ</summary>
    Private _OutputList As List(Of TShisakuTehaiKihonVo)

    Private _ResultOk As Boolean

    Private _MakerCsvVos As New List(Of MakerCsvVo)
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

        '取引先CSVを読込
        _MakerCsvVos = New List(Of MakerCsvVo)
        _TehaiEditLogic.ReadMakerCsv(_MakerCsvVos, MAKER_CSV_FILE, True)
        If _MakerCsvVos.Count > 0 Then
            ' コンボボックス類初期化
            FormUtil.BindLabelValuesToComboBox(Me.cmbTorihikisaki, GetLabelValues_CompName(), True)
        End If

        '担当者CSVを読込
        _TantoCsvVos = New List(Of TantoCsvVo)
        _TehaiEditLogic.ReadTantoCsv(_TantoCsvVos, TANTO_CSV_FILE, True)
        If _TantoCsvVos.Count > 0 Then
            ' コンボボックス類初期化
            FormUtil.BindLabelValuesToComboBox(Me.cmbTantousha, GetLabelValues_tantoName(), True)
        End If

        '車種
        txtBasho.Text = "X1"
        
        '初期化完了
        _InitComplete = True

    End Sub

    Private Sub InitializeValidator()

        validator = New Validator

        Dim TorihikisakiRequired As New Validator("取引先を選択または入力してください。")
        TorihikisakiRequired.Add(cmbTorihikisaki).Required()
        Dim TantoushaRequired As New Validator("担当者を選択または入力してください。")
        TantoushaRequired.Add(cmbTantousha).Required()
        Dim BashoRequired As New Validator("納入場所を入力してください。")
        BashoRequired.Add(txtBasho).Required()

        validator.Add(TorihikisakiRequired)
        validator.Add(TantoushaRequired)
        validator.Add(BashoRequired)

    End Sub

#End Region

#Region "コンボボックス設定"

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
    Private Sub Frm20OrderExport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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
            cmbTorihikisaki.BackColor = Nothing
            cmbTantousha.BackColor = Nothing
            txtBasho.BackColor = Nothing
    
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

            '注文書出力
            If _TehaiEditLogic.OrderOutput(_OutputList, cmbTorihikisaki.Text, cmbTantousha.Text, txtBasho.Text) = False Then
                Return
            End If

            Cursor.Current = Cursors.Default
            _ResultOk = True
            Me.Close()
        Catch ex As ApplicationException
            _ResultOk = False
            msg = "注文書出力で問題が発生しました。既にファイルが開いている可能性があります。"
            ComFunc.ShowErrMsgBox(msg)
        Catch ex As Exception
            _ResultOk = False
            msg = String.Format("注文書出力で問題が発生しました(ERR={0})", ex.Message)
            ComFunc.ShowErrMsgBox(msg)
        End Try

    End Sub

#End Region

End Class