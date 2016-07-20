Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo

Namespace EventEdit.Logic
    Public Class EventEditBaseCarKaiteiVo : Inherits TShisakuEventBaseKaiteiVo

        ' 仕様情報№の選択値
        Private _BaseShiyoujyouhouNoLabelValues As List(Of LabelValueVo)
        ' アプライド№と型式の選択値元データ
        Private _AppliedNoKatashikiFugo7s As List(Of Rhac0230Vo)
        ' アプライド№の選択値
        Private _BaseAppliedNoLabelValues As List(Of LabelValueVo)
        ' 型式の選択値
        Private _BaseKatashikiLabelValues As List(Of LabelValueVo)
        ' 仕向の選択値
        Private _BaseShimukeLabelValues As List(Of LabelValueVo)
        ' OPの選択値
        Private _BaseOpLabelValues As List(Of LabelValueVo)
        ' 外装色の選択値
        Private _BaseGaisousyokuLabelValues As List(Of LabelValueVo)
        ' 内装色の選択値
        Private _BaseNaisousyokuLabelValues As List(Of LabelValueVo)
        ' 試作ベース号車の選択値
        Private _ShisakuBaseGousyaLabelValues As List(Of LabelValueVo)

        ''' <summary>仕様情報№の選択値</summary>
        ''' <value>仕様情報№の選択値</value>
        ''' <returns>仕様情報№の選択値</returns>
        Public Property BaseShiyoujyouhouNoLabelValues() As List(Of LabelValueVo)
            Get
                Return _BaseShiyoujyouhouNoLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _BaseShiyoujyouhouNoLabelValues = value
            End Set
        End Property

        ''' <summary>アプライド№と型式の選択値元データ</summary>
        ''' <value>アプライド№と型式の選択値元データ</value>
        ''' <returns>アプライド№と型式の選択値元データ</returns>
        Public Property AppliedNoKatashikiFugo7s() As List(Of Rhac0230Vo)
            Get
                Return _AppliedNoKatashikiFugo7s
            End Get
            Set(ByVal value As List(Of Rhac0230Vo))
                _AppliedNoKatashikiFugo7s = value
            End Set
        End Property

        ''' <summary>アプライド№の選択値</summary>
        ''' <value>アプライド№の選択値</value>
        ''' <returns>アプライド№の選択値</returns>
        Public Property BaseAppliedNoLabelValues() As List(Of LabelValueVo)
            Get
                Return _BaseAppliedNoLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _BaseAppliedNoLabelValues = value
            End Set
        End Property

        ''' <summary>型式の選択値</summary>
        ''' <value>型式の選択値</value>
        ''' <returns>型式の選択値</returns>
        Public Property BaseKatashikiLabelValues() As List(Of LabelValueVo)
            Get
                Return _BaseKatashikiLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _BaseKatashikiLabelValues = value
            End Set
        End Property

        ''' <summary>仕向の選択値</summary>
        ''' <value>仕向の選択値</value>
        ''' <returns>仕向の選択値</returns>
        Public Property BaseShimukeLabelValues() As List(Of LabelValueVo)
            Get
                Return _BaseShimukeLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _BaseShimukeLabelValues = value
            End Set
        End Property

        ''' <summary>OPの選択値</summary>
        ''' <value>OPの選択値</value>
        ''' <returns>OPの選択値</returns>
        Public Property BaseOpLabelValues() As List(Of LabelValueVo)
            Get
                Return _BaseOpLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _BaseOpLabelValues = value
            End Set
        End Property

        ''' <summary>外装色の選択値</summary>
        ''' <value>外装色の選択値</value>
        ''' <returns>外装色の選択値</returns>
        Public Property BaseGaisousyokuLabelValues() As List(Of LabelValueVo)
            Get
                Return _BaseGaisousyokuLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _BaseGaisousyokuLabelValues = value
            End Set
        End Property

        ''' <summary>内装色の選択値</summary>
        ''' <value>内装色の選択値</value>
        ''' <returns>内装色の選択値</returns>
        Public Property BaseNaisousyokuLabelValues() As List(Of LabelValueVo)
            Get
                Return _BaseNaisousyokuLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _BaseNaisousyokuLabelValues = value
            End Set
        End Property

        ''' <summary>試作ベース号車の選択値</summary>
        ''' <value>試作ベース号車の選択値</value>
        ''' <returns>試作ベース号車の選択値</returns>
        Public Property ShisakuBaseGousyaLabelValues() As List(Of LabelValueVo)
            Get
                Return _ShisakuBaseGousyaLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _ShisakuBaseGousyaLabelValues = value
            End Set
        End Property
    End Class
End Namespace