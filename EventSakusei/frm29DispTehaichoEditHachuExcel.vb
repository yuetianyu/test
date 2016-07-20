Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports ShisakuCommon.Ui
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.TehaichoMenu
Imports EventSakusei.TehaichoMenu.Dao
Imports EventSakusei.TehaichoMenu.Impl
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Ui.Valid

''' <summary>
''' 手配帳編集画面・発注データEXCEL出力
''' </summary>
''' <remarks></remarks>
Public Class frm29DispTehaichoEditHachuExcel

    Private shisakuListCode As String
    Private shisakuEventCode As String
    Private ReadOnly tehaicho As TeiseiTsuchiDao

    Public Sub New(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal kAiteiCode As String)

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()
        If kAiteiCode = "0" Then
            Me.Text = "訂正通知データEXCEL出力"
        Else
            Me.Text = "新調達データEXCEL出力"
        End If
        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        Me.shisakuEventCode = shisakuEventCode
        Me.shisakuListCode = shisakuListCode
        tehaicho = New TeiseiTsuchiDaoImpl
        Intialize()
        InitializeValidatorKaiteiNo()
        KaiteiNoLabelValues = GetLabelValues_KaiteiNo()

        If kAiteiCode = "0" Then
            Dim wCount As Integer = KaiteiNoLabelValues.Count
            KaiteiNoLabelValues.RemoveAt(wCount - 1)
        End If

        SetComboBox()

    End Sub

    Private Sub Intialize()
        ShisakuFormUtil.SettingDefaultProperty(cmbKaiteiNo)
    End Sub

    ''' <summary>
    ''' 改訂Noの表示値を更新する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComboBox()
        FormUtil.BindLabelValuesToComboBox(cmbKaiteiNo, KaiteiNoLabelValues, False)
        FormUtil.SetComboBoxSelectedValue(cmbKaiteiNo, KaiteiNo)
        cmbKaiteiNo.Text = KaiteiNoLabelValues(0).Value
    End Sub

    Private errorController As New ErrorController()

    Private Sub btnOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutput.Click

        errorController.ClearBackColor()
        Try
            validatorKaiteiNo.AssertValidate()
            '改訂Noを手配帳編集メニューに渡す'
            CType(Me.Owner, frm19DispTehaichoMenu).KaiteiNo = cmbKaiteiNo.Text
        Catch ex As IllegalInputException
            errorController.SetBackColorOnError(ex.ErrorControls)
            errorController.FocusAtFirstControl(ex.ErrorControls)
            ''エラーメッセージ
            ComFunc.ShowErrMsgBox(ex.Message)
            Return
        End Try
        frm29ParaModori = "OK"
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        CType(Me.Owner, frm19DispTehaichoMenu).CanselFlg = True
        Me.Close()
    End Sub

    Private Sub frm29DispTehaichoEditHachuExcel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub


    Private _KaiteiNo As String
    ''' <summary>改訂No</summary>
    ''' <returns>改訂No</returns>
    Public Property KaiteiNo() As String
        Get
            Return _KaiteiNo
        End Get
        Set(ByVal value As String)
            _KaiteiNo = value
            KaiteiNoLabelValues() = GetLabelValues_KaiteiNo()
        End Set
    End Property

    '' 改訂Noの選択値
    Private _KaiteiNoLabelValues As List(Of LabelValueVo)
    ''' <summary>改訂Noの選択値</summary>
    ''' <value>改訂Noの選択値</value>
    ''' <returns>改訂Noの選択値</returns>
    Public Property KaiteiNoLabelValues() As List(Of LabelValueVo)
        Get
            Return _KaiteiNoLabelValues
        End Get
        Set(ByVal value As List(Of LabelValueVo))
            _KaiteiNoLabelValues = value
        End Set
    End Property


    '選択された改訂Noを返す'
    Public Function GetKaiteiNo() As String
        Return cmbKaiteiNo.Text
    End Function

    Private Class KaiteiNoExtraction : Implements ILabelValueExtraction
        Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
            Dim vo As New TShisakuListcodeVo
            aLocator.IsA(vo).Label(vo.ShisakuListCodeKaiteiNo).Value(vo.ShisakuListCodeKaiteiNo)
        End Sub
    End Class

    Public Function GetLabelValues_KaiteiNo() As List(Of LabelValueVo)
        If KaiteiNo Is String.Empty Then
            Return New List(Of LabelValueVo)
        End If
        Dim vo As New TShisakuListcodeVo
        vo.ShisakuListCodeKaiteiNo = KaiteiNo
        Dim results As List(Of LabelValueVo) = _
            LabelValueExtracter(Of TShisakuListcodeVo).Extract(tehaicho.FindByKaiteiNoList(shisakuEventCode, shisakuListCode), New KaiteiNoExtraction)
        'results.Sort(New LabelValueComparer)
        Return results
    End Function


#Region "エラーチェック"

    Private validatorKaiteiNo As Validator

    Private Sub InitializeValidatorKaiteiNo()

        validatorKaiteiNo = New Validator

        Dim OldListCodeRequired As New Validator("改訂Noを入力してください")
        OldListCodeRequired.Add(cmbKaiteiNo).Required()

        validatorKaiteiNo.Add(OldListCodeRequired)
    End Sub

#End Region

End Class
