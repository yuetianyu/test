Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Ikkatsu.Logic
    ''' <summary>
    ''' 部品構成呼出一括設定の各レコードを表すクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditIkkatsuVo
        ' INSTL品番
        Private _instlHinban As String
        ' 区分
        Private _instlHinbanKbn As String
        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(2)) (TES)張 ADD BEGIN
        ' データ区分
        Private _instlDataKbn As String
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(2)) (TES)張 ADD END
        '↓↓2014/10/20 酒井 ADD BEGIN
        Private _baseInstlFlg As String
        '↑↑2014/10/20 酒井 ADD END
        ' ベースにする品番
        Private _baseHinban As String
        ' ベースにする区分
        Private _baseHinbanKbn As String
        ' ベースにする区分の選択値
        Private _baseHinbanKbnLabelValues As List(Of LabelValueVo)
        ' ベースにする区分の試作設計ブロックINSTL情報
        Private _baseHinbanKbnInstlVos As List(Of TShisakuSekkeiBlockInstlVo)
        ' 構成の情報
        Private _structureResult As StructureResult
        '呼出し元
        Private _yobidashiMoto As String
        '呼出し元の選択値
        Private _yobidashiMotoLabelValues As List(Of LabelValueVo)
        ' 構成の情報
        Private _structureResults As List(Of LabelValueVo)

        ''' <summary>INSTL品番</summary>
        ''' <value>INSTL品番</value>
        ''' <returns>INSTL品番</returns>
        Public Property InstlHinban() As String
            Get
                Return _instlHinban
            End Get
            Set(ByVal value As String)
                _instlHinban = value
            End Set
        End Property

        ''' <summary>区分</summary>
        ''' <value>区分</value>
        ''' <returns>区分</returns>
        Public Property InstlHinbanKbn() As String
            Get
                Return _instlHinbanKbn
            End Get
            Set(ByVal value As String)
                _instlHinbanKbn = value
            End Set
        End Property


        ''' <summary>データ区分</summary>
        ''' <value>データ区分</value>
        ''' <returns>データ区分</returns>
        Public Property InstlDataKbn() As String
            Get
                Return _instlDataKbn
            End Get
            Set(ByVal value As String)
                _instlDataKbn = value
            End Set
        End Property
        '↓↓2014/10/20 酒井 ADD BEGIN
        Public Property BaseInstlFlg() As String
            Get
                Return _baseInstlFlg
            End Get
            Set(ByVal value As String)
                _baseInstlFlg = value
            End Set
        End Property
        '↑↑2014/10/20 酒井 ADD END

        ''' <summary>ベースにする品番</summary>
        ''' <value>ベースにする品番</value>
        ''' <returns>ベースにする品番</returns>
        Public Property BaseHinban() As String
            Get
                Return _baseHinban
            End Get
            Set(ByVal value As String)
                _baseHinban = value
            End Set
        End Property

        ''' <summary>ベースにする区分</summary>
        ''' <value>ベースにする区分</value>
        ''' <returns>ベースにする区分</returns>
        Public Property BaseHinbanKbn() As String
            Get
                Return _baseHinbanKbn
            End Get
            Set(ByVal value As String)
                _baseHinbanKbn = value
            End Set
        End Property

        ''' <summary>ベースにする区分の選択値</summary>
        ''' <value>ベースにする区分の選択値</value>
        ''' <returns>ベースにする区分の選択値</returns>
        Public Property BaseHinbanKbnLabelValues() As List(Of LabelValueVo)
            Get
                Return _baseHinbanKbnLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _baseHinbanKbnLabelValues = value
            End Set
        End Property

        ''' <summary>ベースにする区分の試作設計ブロックINSTL情報</summary>
        ''' <value>ベースにする区分の試作設計ブロックINSTL情報</value>
        ''' <returns>ベースにする区分の試作設計ブロックINSTL情報</returns>
        Public Property BaseHinbanKbnInstlVos() As List(Of TShisakuSekkeiBlockInstlVo)
            Get
                Return _baseHinbanKbnInstlVos
            End Get
            Set(ByVal value As List(Of TShisakuSekkeiBlockInstlVo))
                _baseHinbanKbnInstlVos = value
            End Set
        End Property

        ''' <summary>構成の情報</summary>
        ''' <value>構成の情報</value>
        ''' <returns>構成の情報</returns>
        Public Property StructureResult() As StructureResult
            Get
                Return _structureResult
            End Get
            Set(ByVal value As StructureResult)
                _structureResult = value
            End Set
        End Property
        ''' <summary>
        ''' 呼出し元
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property YobidashiMoto() As String
            Get
                Return _yobidashiMoto
            End Get
            Set(ByVal value As String)
                _yobidashiMoto = value
            End Set
        End Property
        ''' <summary>
        ''' 呼出し元の選択値
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property YobidashiMotoLabelValues() As List(Of LabelValueVo)
            Get
                Return _yobidashiMotoLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _yobidashiMotoLabelValues = value
            End Set
        End Property

    End Class
End Namespace