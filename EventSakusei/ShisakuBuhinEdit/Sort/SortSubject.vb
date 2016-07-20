Imports ShisakuCommon.Util
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon


Namespace ShisakuBuhinEdit.Sort

    ''' <summary>
    ''' ソート機能
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SortSubject : Inherits Observable

        Private ColumnNameList As List(Of LabelValueVo)


        ''' <summary>
        ''' 初期設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

            '列データを用意する'
            SetColumnNameList()

            'キー列名'
            ColumnName1LabelValues = GetLabelValues_ColumnName1()
            ColumnName2LabelValues = GetLabelValues_ColumnName2()
            ColumnName3LabelValues = GetLabelValues_ColumnName3()

            SetChanged()
        End Sub

        Private Sub SetColumnNameList()
            'タイトル行の文字列を追加する'

            Dim lVo As New LabelValueVo
            ColumnNameList = New List(Of LabelValueVo)

            lVo.Label = ""
            lVo.Value = "0"
            ColumnNameList.Add(lVo)

            lVo = New LabelValueVo
            lVo.Label = "レベル"
            lVo.Value = "1"
            ColumnNameList.Add(lVo)

            lVo = New LabelValueVo
            lVo.Label = "取引先コード"
            lVo.Value = "2"
            ColumnNameList.Add(lVo)

            lVo = New LabelValueVo
            lVo.Label = "部品番号"
            lVo.Value = "3"
            ColumnNameList.Add(lVo)

            lVo = New LabelValueVo
            lVo.Label = "部品名称"
            lVo.Value = "4"
            ColumnNameList.Add(lVo)

            lVo = New LabelValueVo
            lVo.Label = "供給セクション"
            lVo.Value = "5"
            ColumnNameList.Add(lVo)

            lVo = New LabelValueVo
            lVo.Label = "出図予定日"
            lVo.Value = "6"
            ColumnNameList.Add(lVo)

            lVo = New LabelValueVo
            lVo.Label = "材質規格1"
            lVo.Value = "7"
            ColumnNameList.Add(lVo)

            lVo = New LabelValueVo
            lVo.Label = "材質規格2"
            lVo.Value = "8"
            ColumnNameList.Add(lVo)

            lVo = New LabelValueVo
            lVo.Label = "材質規格3"
            lVo.Value = "9"
            ColumnNameList.Add(lVo)

            lVo = New LabelValueVo
            lVo.Label = "材質メッキ"
            lVo.Value = "10"
            ColumnNameList.Add(lVo)

            lVo = New LabelValueVo
            lVo.Label = "板厚"
            lVo.Value = "11"
            ColumnNameList.Add(lVo)

            lVo = New LabelValueVo
            lVo.Label = "板厚u"
            lVo.Value = "12"
            ColumnNameList.Add(lVo)

            lVo = New LabelValueVo
            lVo.Label = "試作部品費"
            lVo.Value = "13"
            ColumnNameList.Add(lVo)

            lVo = New LabelValueVo
            lVo.Label = "試作型費"
            lVo.Value = "14"
            ColumnNameList.Add(lVo)
        End Sub


#Region "コンボボックスの処理"

        ''' <summary>
        ''' 最優先されるキー
        ''' </summary>
        ''' <remarks></remarks>
        Private Class ColumnName1Extraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim lVo As New LabelValueVo
                aLocator.IsA(lVo).Label(lVo.Label).Value(lVo.Value)
            End Sub
        End Class

        ''' <summary>
        ''' 2番目に優先されるキー
        ''' </summary>
        ''' <remarks></remarks>
        Private Class ColumnName2Extraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim lVo As New LabelValueVo
                aLocator.IsA(lVo).Label(lVo.Label).Value(lVo.Value)
            End Sub
        End Class

        ''' <summary>
        ''' 3番目に優先されるキー
        ''' </summary>
        ''' <remarks></remarks>
        Private Class ColumnName3Extraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim lVo As New LabelValueVo
                aLocator.IsA(lVo).Label(lVo.Label).Value(lVo.Value)
            End Sub
        End Class

        ''' <summary>
        ''' 最優先されるキー
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_ColumnName1() As List(Of LabelValueVo)
            If ColumnName1 Is String.Empty Then
                Return New List(Of LabelValueVo)
            End If
            Dim vo As New LabelValueVo
            vo.Label = ColumnName1
            Dim results As List(Of LabelValueVo) = _
                LabelValueExtracter(Of LabelValueVo).Extract(ColumnNameList, New ColumnName1Extraction)
            results.Sort(New LabelValueComparer)
            Return results
        End Function

        ''' <summary>
        ''' 2番目に優先されるキー
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_ColumnName2() As List(Of LabelValueVo)
            If ColumnName2 Is String.Empty Then
                Return New List(Of LabelValueVo)
            End If
            Dim vo As New LabelValueVo
            vo.Label = ColumnName2
            Dim results As List(Of LabelValueVo) = _
                LabelValueExtracter(Of LabelValueVo).Extract(ColumnNameList, New ColumnName2Extraction)
            results.Sort(New LabelValueComparer)
            Return results
        End Function

        ''' <summary>
        ''' 3番目に優先されるキー
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_ColumnName3() As List(Of LabelValueVo)
            If ColumnName3 Is String.Empty Then
                Return New List(Of LabelValueVo)
            End If
            Dim vo As New LabelValueVo
            vo.Label = ColumnName3
            Dim results As List(Of LabelValueVo) = _
                LabelValueExtracter(Of LabelValueVo).Extract(ColumnNameList, New ColumnName3Extraction)
            results.Sort(New LabelValueComparer)
            Return results
        End Function


#End Region

#Region "公開プロパティ"

        '' 最優先されるキーの列名
        Private _ColumnName1 As String
        '' 2番目に優先されるキーの列名
        Private _ColumnName2 As String
        '' 3番目に優先されるキーの列名
        Private _ColumnName3 As String
        '' 最優先されるキーの並び順
        Private _Order1 As Boolean
        '' 2番目に優先されるキーの並び順
        Private _Order2 As Boolean
        '' 3番目に優先されるキーの並び順
        Private _Order3 As Boolean

#End Region

#Region "公開プロパティの実装"

        ''' <summary>最優先されるキーの列名</summary>
        ''' <returns>最優先されるキーの列名</returns>
        Public Property ColumnName1() As String
            Get
                Return _ColumnName1
            End Get
            Set(ByVal value As String)

                If EzUtil.IsEqualIfNull(_ColumnName1, value) Then
                    Return
                End If
                _ColumnName1 = value
                SetChanged()

                ColumnName1LabelValues() = GetLabelValues_ColumnName1()
            End Set
        End Property

        ''' <summary>2番目に優先されるキーの列名</summary>
        ''' <returns>2番目に優先されるキーの列名</returns>
        Public Property ColumnName2() As String
            Get
                Return _ColumnName2
            End Get
            Set(ByVal value As String)

                If EzUtil.IsEqualIfNull(_ColumnName2, value) Then
                    Return
                End If
                _ColumnName2 = value
                SetChanged()

                ColumnName2LabelValues() = GetLabelValues_ColumnName2()
            End Set
        End Property

        ''' <summary>3番目に優先されるキーの列名</summary>
        ''' <returns>3番目に優先されるキーの列名</returns>
        Public Property ColumnName3() As String
            Get
                Return _ColumnName3
            End Get
            Set(ByVal value As String)

                If EzUtil.IsEqualIfNull(_ColumnName3, value) Then
                    Return
                End If
                _ColumnName3 = value
                SetChanged()

                ColumnName3LabelValues() = GetLabelValues_ColumnName3()
            End Set
        End Property


        ''' <summary>最優先されるキーの並び順</summary>
        ''' <value>最優先されるキーの並び順</value>
        ''' <returns>最優先されるキーの並び順</returns>
        Public Property Order1() As Boolean
            Get
                Return _Order1
            End Get
            Set(ByVal value As Boolean)
                _Order1 = value
            End Set
        End Property

        ''' <summary>2番目優先されるキーの並び順</summary>
        ''' <value>2番目されるキーの並び順</value>
        ''' <returns>2番目されるキーの並び順</returns>
        Public Property Order2() As Boolean
            Get
                Return _Order2
            End Get
            Set(ByVal value As Boolean)
                _Order2 = value
            End Set
        End Property

        ''' <summary>3番目優先されるキーの並び順</summary>
        ''' <value>3番目されるキーの並び順</value>
        ''' <returns>3番目されるキーの並び順</returns>
        Public Property Order3() As Boolean
            Get
                Return _Order3
            End Get
            Set(ByVal value As Boolean)
                _Order3 = value
            End Set
        End Property

#End Region

#Region "公開プロパティの選択値"
        '' 最優先されるキーの選択値
        Private _ColumnName1LabelValues As List(Of LabelValueVo)
        '' 2番目優先されるキーの選択値
        Private _ColumnName2LabelValues As List(Of LabelValueVo)
        '' 3番目優先されるキーの選択値
        Private _ColumnName3LabelValues As List(Of LabelValueVo)

        ''' <summary>最優先されるキーの選択値</summary>
        ''' <value>最優先されるキーの選択値</value>
        ''' <returns>最優先されるキーの選択値</returns>
        Public Property ColumnName1LabelValues() As List(Of LabelValueVo)
            Get
                Return _ColumnName1LabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _ColumnName1LabelValues = value
            End Set
        End Property

        ''' <summary>2番目優先されるキーの選択値</summary>
        ''' <value>2番目優先されるキーの選択値</value>
        ''' <returns>2番目優先されるキーの選択値</returns>
        Public Property ColumnName2LabelValues() As List(Of LabelValueVo)
            Get
                Return _ColumnName2LabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _ColumnName2LabelValues = value
            End Set
        End Property

        ''' <summary>3番目優先されるキーの選択値</summary>
        ''' <value>3番目優先されるキーの選択値</value>
        ''' <returns>3番目優先されるキーの選択値</returns>
        Public Property ColumnName3LabelValues() As List(Of LabelValueVo)
            Get
                Return _ColumnName3LabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _ColumnName3LabelValues = value
            End Set
        End Property

#End Region


    End Class

End Namespace

