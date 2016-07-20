Imports EventSakusei.YosanSetteiBuhinEdit.Dao
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Ui
Imports ShisakuCommon

Public Class frmPurchaseRequestDialog

    Private dao As YosanSetteiBuhinEditHeaderDao

    Private _GenchoEventCode As String  '現調イベントコード
    Private _PhaseNo As String          'フェーズ№
    Private _LayoutNo As Integer        '割付予算番号
    Private _IsAll As Boolean           '全てのセルが対象ならtrue

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        dao = New YosanSetteiBuhinEditHeaderDaoImpl

        '現調のイベントコード設定'
        FormUtil.BindLabelValuesToComboBox(Me.cmbGenchoEventCode, GetLabelValues_EventCode(), True)

        '初期値設定'
        GenchoEventCode = ""
        PhaseNo = ""

    End Sub

#Region "イベント"

    ''' <summary>
    ''' OKボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        'ラジオボタンから設定'
        If rbPartsPrice.Checked Then
            LayoutNo = 0
        End If
        If rbYosanLayout.Checked Then
            LayoutNo = 1
        End If
        If rbPastBuyParts.Checked Then
            LayoutNo = 2
        End If
        If rbYosanLayoutMulti.Checked Then
            LayoutNo = 3
        End If
        If rbGenchoSystem.Checked Then
            LayoutNo = 4
            If StringUtil.IsEmpty(cmbGenchoEventCode.SelectedValue) Then
                MsgBox("イベントコードを指定してください。")
                Exit Sub
            End If

            If StringUtil.IsEmpty(cmbPhase.SelectedValue) Then
                MsgBox("フェーズを指定してください。")
                Exit Sub
            End If
            GenchoEventCode = cmbGenchoEventCode.SelectedValue
            PhaseNo = cmbPhase.SelectedValue
        End If

        If rbIsBlankCell.Checked Then
            IsAll = False
        Else
            IsAll = True
        End If


        Me.DialogResult = Windows.Forms.DialogResult.OK

        Me.Close()
    End Sub

    ''' <summary>
    ''' キャンセルボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' 試作開発管理表ラジオボタンチェック時の動作
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbGenchoSystem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbGenchoSystem.CheckedChanged
        If rbGenchoSystem.Checked Then
            'チェックされたならイベントコードとフェーズを解禁'
            cmbGenchoEventCode.Enabled = True
            cmbPhase.Enabled = True
        Else
            cmbGenchoEventCode.Enabled = False
            cmbPhase.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 現調イベントコードコンボボックスの値変更
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbGenchoEventCode_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGenchoEventCode.SelectedValueChanged
        'フェーズを設定する'
        FormUtil.BindLabelValuesToComboBox(Me.cmbPhase, GetLabelValues_Phase(cmbGenchoEventCode.SelectedValue), True)
    End Sub



#End Region

#Region "現調イベントコードのコンボボックス設定"

    Private Class EventCodeExtraction : Implements ILabelValueExtraction
        Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
            Dim vo As New TFuncEventPhaseVo
            aLocator.IsA(vo).Label(vo.GenchoEventCode).Value(vo.GenchoEventCode)
        End Sub
    End Class

    Private Function GetLabelValues_EventCode() As List(Of LabelValueVo)
        Dim results As List(Of LabelValueVo) = _
            LabelValueExtracter(Of TFuncEventPhaseVo).Extract(dao.FindByGenchoEventCode, New EventCodeExtraction)
        results.Sort(New LabelValueComparer)
        Return results
    End Function

#End Region

#Region "フェーズのコンボボックス設定"

    Private Class PhaseExtraction : Implements ILabelValueExtraction
        Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
            Dim vo As New TFuncEventPhaseVo
            aLocator.IsA(vo).Label(vo.PhaseName).Value(vo.PhaseNo)
        End Sub
    End Class

    Private Function GetLabelValues_Phase(ByVal genchoEventCode As String) As List(Of LabelValueVo)
        Dim results As List(Of LabelValueVo) = _
            LabelValueExtracter(Of TFuncEventPhaseVo).Extract(dao.FindByPhase(genchoEventCode), New PhaseExtraction)
        results.Sort(New LabelValueComparer)
        Return results
    End Function

#End Region

#Region "プロパティ"

    ''' <summary>
    ''' 現調イベントコード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GenchoEventCode() As String
        Get
            Return _GenchoEventCode
        End Get
        Set(ByVal value As String)
            _GenchoEventCode = value
        End Set
    End Property

    ''' <summary>
    ''' フェーズ№
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PhaseNo() As String
        Get
            Return _PhaseNo
        End Get
        Set(ByVal value As String)
            _PhaseNo = value
        End Set
    End Property

    ''' <summary>
    ''' 割付予算番号
    ''' </summary>
    ''' <value></value>
    ''' <returns>0:パーツプライス値スライド,1:割付予算値スライド,2:過去購入部品優先度1(購入希望単価),3:割付予算×係数2,4:試作開発管理表</returns>
    ''' <remarks></remarks>
    Public Property LayoutNo() As Integer
        Get
            Return _LayoutNo
        End Get
        Set(ByVal value As Integer)
            _LayoutNo = value
        End Set
    End Property

    ''' <summary>
    ''' 設定範囲
    ''' </summary>
    ''' <value></value>
    ''' <returns>全てのセルならTRUE</returns>
    ''' <remarks></remarks>
    Public Property IsAll() As Boolean
        Get
            Return _IsAll
        End Get
        Set(ByVal value As Boolean)
            _IsAll = value
        End Set
    End Property


#End Region



End Class