Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.SourceSelector.Logic
    ''' <summary>
    ''' 部品構成呼出呼出し元抽出画面の各レコードを表すクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SourceSelectorVo
        ' ベースにする品番
        Private _baseHinban As String
        ' ベースにする区分
        Private _baseHinbanKbn As String
        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(3)) (TES)張 ADD BEGIN
        ' ベースにするデータ区分
        Private _baseDataKbn As String
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(3)) (TES)張 ADD END
        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(3)) (TES)張 ADD BEGIN
        ' ベースにするデータ区分
        Private _baseInstlFlg As String
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(3)) (TES)張 ADD END

        ' ベースにする区分の選択値
        Private _baseHinbanKbnLabelValues As List(Of LabelValueVo)
        ' ベースにする区分の試作設計ブロックINSTL情報
        Private _baseHinbanKbnInstlVos As List(Of TShisakuSekkeiBlockInstlVo)
        ' 構成の情報
        Private _structureResult As StructureResult

        ''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
        ' 構成の情報（ベース車専用化向け）
        Private _structureResult4Base As StructureResult
        ''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
        '呼出し元
        Private _yobidashiMoto As String
        '呼出し元の選択値
        Private _yobidashiMotoLabelValues As List(Of LabelValueVo)
        ' 構成の情報
        Private _structureResults As List(Of LabelValueVo)

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
                If value Is Nothing Then
                    value = ""
                End If
                _baseHinbanKbn = value
            End Set
        End Property

        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(12)) (TES)張 ADD BEGIN
        ''' <summary>ベースにするデータ区分</summary>
        ''' <value>ベースにするデータ区分</value>
        ''' <returns>ベースにするデータ区分</returns>
        Public Property BaseDataKbn() As String
            Get
                Return _baseDataKbn
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then
                    value = ""
                End If
                _baseDataKbn = value
            End Set
        End Property
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥V(12)) (TES)張 ADD END
        '↓↓2014/10/09 酒井 ADD BEGIN
        Public Property BaseInstlFlg() As String
            Get
                Return _BaseInstlFlg
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then
                    value = ""
                End If
                _BaseInstlFlg = value
            End Set
        End Property

        '↑↑2014/10/09 酒井 ADD END
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

        ''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
        ' 構成の情報（ベース車専用化向け）        
        Public Property StructureResult4Base() As StructureResult
            Get
                Return _structureResult4Base
            End Get
            Set(ByVal value As StructureResult)
                _structureResult4Base = value
            End Set
        End Property
        ''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
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