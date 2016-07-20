Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo

Namespace EventEdit.Logic
    Public Class EventEditInquiry : Inherits Observable
        Private ReadOnly shisakuSoubiKbn As String
        Private ReadOnly aRhac0120Dao As Rhac0120Dao
        Private ReadOnly mSoubiDao As MShisakuSoubiDao

        Public Sub New(ByVal shisakuSoubiKbn As String, ByVal aRhac0120Dao As Rhac0120Dao, ByVal mSoubiDao As MShisakuSoubiDao)
            Me.shisakuSoubiKbn = shisakuSoubiKbn
            Me.aRhac0120Dao = aRhac0120Dao
            Me.mSoubiDao = mSoubiDao

            '' 初期値
            _isSelectEBomMaster = True
        End Sub

#Region "プロパティ値"
        ' マスター選択(EBomを選択か？)
        Private _isSelectEBomMaster As Boolean
        ' 装備仕様
        Private _optoinSpec As String
        ' 装備仕様(入力値)
        Private _inputOptionSpec As String
#End Region

#Region "プロパティGet/Set"
        ''' <summary>マスター選択(EBomを選択か？)</summary>
        ''' <value>マスター選択(EBomを選択か？)</value>
        ''' <returns>マスター選択(EBomを選択か？)</returns>
        Public Property IsSelectEBomMaster() As Boolean
            Get
                Return _isSelectEBomMaster
            End Get
            Set(ByVal value As Boolean)
                _isSelectEBomMaster = value
                setChanged()
            End Set
        End Property

        ''' <summary>装備仕様</summary>
        ''' <value>装備仕様</value>
        ''' <returns>装備仕様</returns>
        Public Property OptoinSpec() As String
            Get
                Return _optoinSpec
            End Get
            Set(ByVal value As String)
                _optoinSpec = value
            End Set
        End Property

        ''' <summary>装備仕様(入力値)</summary>
        ''' <value>装備仕様(入力値)</value>
        ''' <returns>装備仕様(入力値)</returns>
        Public Property InputOptionSpec() As String
            Get
                Return _inputOptionSpec
            End Get
            Set(ByVal value As String)
                If _inputOptionSpec Is Nothing And value Is Nothing Then
                    Return
                End If
                If _inputOptionSpec Is Nothing OrElse Not _inputOptionSpec.Equals(value) Then
                    _inputOptionSpec = value
                    setChanged()
                End If
            End Set
        End Property

        Public ReadOnly Property RecordSize() As Integer
            Get
                Return GetLabelValues_Searched.Count
            End Get
        End Property
        Public ReadOnly Property RecordOptionSpec(ByVal rowNo) As String
            Get
                Return GetLabelValues_Searched(rowNo).Label
            End Get
        End Property
#End Region

        Protected Overrides Sub setChanged()
            MyBase.setChanged()
            ClearSearchedLabelValues()
        End Sub

        Private SearchedLabelValues As List(Of LabelValueVo)
        Private Sub ClearSearchedLabelValues()
            SearchedLabelValues = Nothing
        End Sub
        Private Function GetLabelValues_Searched() As List(Of LabelValueVo)
            If SearchedLabelValues Is Nothing Then
                Dim allVos As List(Of LabelValueVo) = GetLabelValues_All()
                If StringUtil.IsEmpty(InputOptionSpec) Then
                    SearchedLabelValues = allVos
                Else
                    SearchedLabelValues = New List(Of LabelValueVo)
                    For Each vo As LabelValueVo In allVos
                        If vo.Label.StartsWith(InputOptionSpec) Then
                            SearchedLabelValues.Add(vo)
                        End If
                    Next
                End If
            End If
            Return SearchedLabelValues
        End Function

        Private Class Rhac0120Extraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim vo As New Rhac0120Vo
                aLocator.IsA(vo).Label(vo.ShiyosobiName).Value(vo.ShiyosobiCode)
            End Sub
        End Class

        Private Class Rhac0120Comparable : Implements IComparer(Of Rhac0120Vo)
            Public Function Compare(ByVal x As ShisakuCommon.Db.EBom.Vo.Rhac0120Vo, ByVal y As ShisakuCommon.Db.EBom.Vo.Rhac0120Vo) As Integer Implements System.Collections.Generic.IComparer(Of ShisakuCommon.Db.EBom.Vo.Rhac0120Vo).Compare
                If x.DisplayOrder Is Nothing AndAlso y.DisplayOrder Is Nothing Then
                    Return x.ShiyosobiCode.CompareTo(y.ShiyosobiCode)
                ElseIf x.DisplayOrder Is Nothing Then
                    Return -1
                ElseIf y.DisplayOrder Is Nothing Then
                    Return 1
                End If
                If x.DisplayOrder < y.DisplayOrder Then
                    Return -1
                ElseIf x.DisplayOrder > y.DisplayOrder Then
                    Return 1
                End If
                Return x.ShiyosobiCode.CompareTo(y.ShiyosobiCode)
            End Function
        End Class

        Private Class ShisakuSoubiExtraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim vo As New MShisakuSoubiVo
                aLocator.IsA(vo).Label(vo.ShisakuRetuKoumokuName).Value(vo.ShisakuRetuKoumokuCode)
            End Sub
        End Class

        Private _ebomOptionSpec As List(Of LabelValueVo)
        Private _shisakuOptionSpec As List(Of LabelValueVo)
        Private Function GetLabelValues_All() As List(Of LabelValueVo)
            If IsSelectEBomMaster Then
                If _ebomOptionSpec Is Nothing Then
                    Dim allVos As List(Of Rhac0120Vo) = aRhac0120Dao.FindByAll()
                    allVos.Sort(New Rhac0120Comparable)
                    _ebomOptionSpec = _
                        LabelValueExtracter(Of Rhac0120Vo).Extract(allVos, New Rhac0120Extraction)
                End If
                Return _ebomOptionSpec
            Else
                If _shisakuOptionSpec Is Nothing Then
                    Dim param As New MShisakuSoubiVo
                    param.ShisakuSoubiKbn = shisakuSoubiKbn
                    Dim allVos As List(Of MShisakuSoubiVo) = mSoubiDao.FindBy(param)
                    _shisakuOptionSpec = _
                        LabelValueExtracter(Of MShisakuSoubiVo).Extract(allVos, New ShisakuSoubiExtraction)
                    _shisakuOptionSpec.Sort(New LabelValueComparer)
                End If
                Return _shisakuOptionSpec
            End If
        End Function

        Public Function GetLabelValues_OptionSpec() As List(Of LabelValueVo)
            Return GetLabelValues_All()
        End Function
    End Class
End Namespace